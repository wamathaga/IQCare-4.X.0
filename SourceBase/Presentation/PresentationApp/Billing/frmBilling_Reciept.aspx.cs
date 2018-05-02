using System;
using System.Configuration;
using System.Data;
using System.IO;
using Application.Presentation;
using CrystalDecisions.CrystalReports.Engine;
using Interface.SCM;
using Entities.Billing;

namespace IQCare.Web.Billing
{
    public partial class frmBilling_Reciept : LogPage
    {
        ReportDocument rptDocument;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
          init_page();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Unload" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            this.UnLoadReport();
        }
        /// <summary>
        /// Gets or sets the un load report.
        /// </summary>
        /// <value>
        /// The un load report.
        /// </value>
        void UnLoadReport()
        {
             try
            {
                rptDocument.Dispose();
                this.rptDocument = null;
            }
            catch { }
        }
        /// <summary>
        /// Gets a value indicating whether [print on thermal].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [print on thermal]; otherwise, <c>false</c>.
        /// </value>
        bool PrintOnThermal
        {
            get
            {
                string printOnthermal = ConfigurationManager.AppSettings.Get("ReceiptOnThermal").ToLower();
                if (printOnthermal != null)
                    return printOnthermal.Equals("true");
                else return false;
            }
        }

        /// <summary>
        /// Init_pages this instance.
        /// </summary>
        private void init_page()
        {
       
            ReceiptType defaultType = ReceiptType.BillPayment;
            if (Request.QueryString.Count > 0)
            {
               
                if (this.Request.QueryString["ReceiptTrxCode"] != null)
                {
                    //Application.Common.Utility objUtil = new Application.Common.Utility();
                    Session["transactionID"] = this.Request.QueryString["ReceiptTrxCode"];// objUtil.Decrypt(this.Request.QueryString["ReceiptTrxCode"]);
                }
                if (this.Request.QueryString["TranXORType"] != null )
                {
                    string transOX =this.Request.QueryString["TranXORType"].ToString();
                    if (transOX == "9")
                        defaultType = ReceiptType.DepositTransaction;
                    else if (transOX == "17")
                        defaultType = ReceiptType.ReversalTransaction;
                }
            }
            IBilling BManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");

            DataSet theDataSet = BManager.GetReceipt(Convert.ToInt32(Session["transactionID"]), Convert.ToInt32(Session["AppLocationId"]),defaultType);
            
                this.rptDocument = new ReportDocument();
              

            DataTable theDT = theDataSet.Tables[1];
            if (this.PrintOnThermal)
            {
                rptDocument.Load(MapPath("..\\Reports\\Billing\\rptBillingRecieptThermal.rpt"));
            }
            else
            {
                rptDocument.Load(MapPath("..\\Reports\\Billing\\rptBillingReciept.rpt"));                
            }

            String facilityName = (String)theDT.Rows[0]["FacilityName"];

            String dupl;
            if (Request.QueryString["reprint"] != null && Request.QueryString["RePrint"] == "true")
                dupl = "DUPLICATE";
            else
                dupl = "";

            rptDocument.SetDataSource(theDataSet);
            rptDocument.SetParameterValue("FacilityName", facilityName);
            rptDocument.SetParameterValue("Currency", "KES");
            rptDocument.SetParameterValue("DuplicateReceipt", dupl);
            if (!this.PrintOnThermal)
            {
                String facilityLogo = (String)theDT.Rows[0]["FacilityLogo"];
                string f = GblIQCare.GetPath() + facilityLogo;
                string p = Server.MapPath("~/Images/" + facilityLogo);
                rptDocument.SetParameterValue("PicturePath", p);
            }
        
            
            billingRptViewer.EnableParameterPrompt = false;
            billingRptViewer.ReportSource = rptDocument;       
            billingRptViewer.DataBind();

        }
        private byte[] getLogo(String facilityLogo)
        {

            // define the filestream object to read the image
            FileStream fs = default(FileStream);
            // define te binary reader to read the bytes of image
            BinaryReader br = default(BinaryReader);
            // check the existance of image
            if (File.Exists(GblIQCare.GetPath() + facilityLogo))
            {
                // open image in file stream
                fs = new FileStream(GblIQCare.GetPath() + facilityLogo, FileMode.Open);

                // initialise the binary reader from file streamobject
                br = new BinaryReader(fs);
                // define the byte array of filelength
                byte[] imgbyte = new byte[fs.Length + 1];
                // read the bytes from the binary reader
                imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                br.Close();
                // close the binary reader
                fs.Close();
                // close the file stream
                return imgbyte;
            }
            return null;
        }

        /// <summary>
        /// Handles the Unload event of the billingRptViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void billingRptViewer_Unload(object sender, EventArgs e)
        {
            this.UnLoadReport();
        }
    }
}
