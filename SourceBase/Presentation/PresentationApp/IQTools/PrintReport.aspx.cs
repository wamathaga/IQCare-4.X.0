using System;
using System.Xml;
using Interface.Reports;
using Application.Presentation;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Schema;
using System.IO;
using System.Text;
using System.Collections;
using Application.Common;


namespace IQCare.Web.Reports
{
    public partial class PrintReport : LogPage
    {
        /// <summary>
        /// The XML document
        /// </summary>
        private XmlDocument xmlDoc;
        /// <summary>
        /// The XSL document
        /// </summary>
        private XmlDocument xslDoc;      
        /// <summary>
        /// The string  report_ identifier
        /// </summary>
        private string strReport_ID = "";
        DateTime datefrom ;
        DateTime dateTo;
        int cd4CutOff;
        /// <summary>
        /// The reports
        /// </summary>
        IReportIQTools theReports = (IReportIQTools)ObjectFactory.CreateInstance("BusinessProcess.Reports.IQToolsReport, BusinessProcess.Reports");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx",true);
            }
            XmlDocument xmlDoc = null;
            // Get the ID of the action
            if (base.Session["PRINT_REPORT_ID"] != null)
            {
                try
                {
                    strReport_ID = base.Session["PRINT_REPORT_ID"].ToString();
                    datefrom = Convert.ToDateTime(base.Session["REPOR_DATE_RANGE_FROM"]);
                    dateTo = Convert.ToDateTime(base.Session["REPOR_DATE_RANGE_TO"]);
                    cd4CutOff = Convert.ToInt32(base.Session["REPORT_CD4_CUTOFF"]);

                    theReports.RunReport(Convert.ToInt16(strReport_ID), datefrom, dateTo, cd4CutOff);
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(theReports.GetReportData());

                
               // XmlSchemaInference inference = new XmlSchemaInference();

      //  MemoryStream  stream =   new MemoryStream(Encoding.UTF8.GetBytes(xmlDoc.OuterXml.ToString()));
      //XmlTextReader reader  = new XmlTextReader(stream);
      //XmlSchemaSet schemaSet = inference.InferSchema(reader);

      ////XmlSchema schema = 
      //          IEnumerator en = schemaSet.Schemas().GetEnumerator();
      //          en.MoveNext();
      //          XmlSchema schema = (XmlSchema)en.Current;

      //          FileStream file = new FileStream(Server.MapPath("new.xsd"), FileMode.Create, FileAccess.ReadWrite);
      //          XmlTextWriter xwriter = new XmlTextWriter(file, new UTF8Encoding());
      //          xwriter.Formatting = Formatting.Indented;
      //          schema.Write(xwriter);

                xslDoc = new XmlDocument();

 
               xslDoc.LoadXml(theReports.ReportXslTemplate);

              //  xslDoc.Save(Server.MapPath("\\" + strReport_ID.ToString() + "xsl.xml"));

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xslDoc.NameTable);
                nsmgr.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");

                XPathNavigator xslNav = xslDoc.CreateNavigator();

                XslCompiledTransform trans = new XslCompiledTransform();

                Response.Clear();
               Response.ContentType = "text/html; charset=windows-1252";

                XsltSettings xslSessings = new XsltSettings();
                xslSessings.EnableScript = true;

                trans.Load(xslNav, xslSessings, new XmlUrlResolver());

                trans.Transform(xmlDoc.CreateNavigator(), null, Response.OutputStream);
               
                Response.Flush();
                }
                catch (Exception exp)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "Please verify the IQTools connection. Please make sure you have IQTools configured properly.";
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                }
            }
            else return;

            
        }
    }
}