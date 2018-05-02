using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Application.Presentation;
using Interface.Clinical;
using Interface.SCM;
using Application.Common;

namespace IQCare.Web.Billing
{
    public partial class frmBilling_PayBillByItems : LogPage
    {
        bool isError = false;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Billing >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Pay Bill";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
            Init_page();

        }
        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            divError.Visible = this.isError;
            this.SetStyle();
        }
        /// </summary>
        void SetStyle()
        {
            HtmlGenericControl facilityBanner = (Master.FindControl("facilityBanner") as HtmlGenericControl);
            if (facilityBanner != null) facilityBanner.Style.Add("display", "inline");

            HtmlGenericControl patientBanner = (Master.FindControl("patientBanner") as HtmlGenericControl);
            if (patientBanner != null) patientBanner.Style.Add("display", "none");
            HtmlGenericControl username1 = (Master.FindControl("username1") as HtmlGenericControl);
            if (username1 != null)
                username1.Attributes["class"] = "usernameLevel1"; //Style.Add("display", "inline");
            HtmlGenericControl currentdate1 = (Master.FindControl("currentdate1") as HtmlGenericControl);
            if (currentdate1 != null) currentdate1.Attributes["class"] = "currentdateLevel1"; //Style.Add("display", "inline");
            HtmlGenericControl facilityName = (Master.FindControl("facilityName") as HtmlGenericControl);
            if (facilityName != null) facilityName.Attributes["class"] = "facilityLevel1"; //Style.Add("display", "inline");
            //userNameLevel2.Style.Add("display", "none");
            //currentDateLevel2.Style.Add("display", "none");
            HtmlGenericControl imageFlipLevel2 = (Master.FindControl("imageFlipLevel2") as HtmlGenericControl);
            if (imageFlipLevel2 != null) imageFlipLevel2.Style.Add("display", "none");
            //facilityLevel2.Style.Add("display", "none");
            HtmlGenericControl level2Navigation = (Master.FindControl("level2Navigation") as HtmlGenericControl);
            if (level2Navigation != null) level2Navigation.Style.Add("display", "none");
        }
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int UserID
        {
            get
            {
                return Convert.ToInt32(Session["AppUserId"]);
            }
        }

        /// <summary>
        /// Gets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        int PatientID
        {
            get
            {
                return Convert.ToInt32(base.Session["PatientId"]);
            }
        }
        /// <summary>
        /// Gets the bill identifier.
        /// </summary>
        /// <value>
        /// The bill identifier.
        /// </value>
        int BillID
        {
            get
            {
                return Convert.ToInt32(base.Session["BillID"]);
            }
        }
        /// <summary>
        /// Gets or sets the amount due.
        /// </summary>
        /// <value>
        /// The amount due.
        /// </value>
        Decimal AmountDue
        {
            get
            {
                return decimal.Parse(this.HDAmountDue.Value);
            }
            set
            {
                this.HDAmountDue.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the bill amount.
        /// </summary>
        /// <value>
        /// The bill amount.
        /// </value>
        Decimal BillAmount
        {
            get
            {
                return decimal.Parse(this.HDBillAmount.Value);
            }
            set
            {
                this.HDBillAmount.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="frmBilling_ReverseBill"/> is debug.
        /// </summary>
        /// <value>
        ///   <c>true</c> if debug; otherwise, <c>false</c>.
        /// </value>
        bool Debug
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings.Get("DEBUG").ToLower().Equals("true");
            }
        }
        /// <summary>
        /// Populates the patient details.
        /// </summary>
        void PopulatePatientDetails()
        {
            IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            
            DataSet theDS = ptnMgr.GetPatientRegistration(int.Parse(Session["PatientId"].ToString()), 12);
                       
            //Session["ClientInfo"] = theDS;
            if (theDS.Tables[0].Rows.Count > 0)
            {
                lblname.Text = String.Format("{0} {1} {2}", theDS.Tables[0].Rows[0]["Firstname"], theDS.Tables[0].Rows[0]["Middlename"], theDS.Tables[0].Rows[0]["Lastname"]);
                lblsex.Text = theDS.Tables[0].Rows[0]["sex"].ToString();
                lbldob.Text = theDS.Tables[0].Rows[0]["dob"].ToString();
                lblIQno.Text = theDS.Tables[0].Rows[0]["IQNumber"].ToString();
            }
            ptnMgr = null;
        }
        /// <summary>
        /// Populates the bill transactions.
        /// </summary>
        void PopulateBillTransactions()
        {
            IBilling BManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
            DataTable dtTransaction = BManager.GetBillTransactions(BillID);
            gridBillTransaction.DataSource = dtTransaction;
            gridBillTransaction.DataBind();
            //buttonPrintReceipt.Enabled = false;
            //if (dtTransaction.Rows.Count > 0)
            //{
            //    string theUrl = "./frmBilling_Reciept.aspx";
            //    buttonPrintReceipt.OnClientClick = "openReportPage('" + theUrl + "'); return false;";
            //    buttonPrintReceipt.Enabled = true;
            //}
        }
        /// <summary>
        /// Populates the bill items.
        /// </summary>
        void PopulateBillItems()
        {
            IBilling BManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
            DataTable dtItems = BManager.GetBillItems(this.BillID);
            DataView theDV = new DataView(dtItems);
            theDV.RowFilter = "PaymentStatus=0";          

            grdPayItems.DataSource =   theDV.ToTable();
            grdPayItems.DataBind();
        }
        /// <summary>
        /// Init_pages this instance.
        /// </summary>
        private void Init_page()
        {
            try
            {
                IBilling BManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
                DataTable dtBill = BManager.GetBillDetails(this.BillID);
                lblTotalBill.InnerText = dtBill.Rows[0]["BillAmount"].ToString();
                this.AmountDue = Convert.ToDecimal(dtBill.Rows[0]["UnpaidAmount"].ToString());
                if (this.AmountDue == 0)
                {
                  string  theUrl = "./frmBillingFindAddBill.aspx";
                    Response.Redirect(theUrl, false);
                }
                this.BillAmount = Convert.ToDecimal(dtBill.Rows[0]["BillAmount"].ToString());
                labelAmountDue.InnerText = textAmountToPay.Text = this.AmountDue.ToString();

                this.rgAmountToPay.MaximumValue = this.AmountDue.ToString();
                this.rgAmountToPay.MinimumValue = "0";
                this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", this.AmountDue);
                this.PopulateBillItems();
                this.PopulateBillTransactions();
                this.PopulatePaymentMode();
                this.PopulatePatientDetails();
                tblCompute.Visible = true;

                tblFinish.Visible = false;
                buttonCompute.Enabled = false;

            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }


        }
        /// <summary>
        /// Initializes the items to pay.
        /// </summary>
        /// <returns></returns>
        private DataTable initializeItemsToPay()
        {

            DataTable theDT = new DataTable();
            theDT.Columns.Add("BillItemID", typeof(Int32));
            theDT.Columns.Add("PatientID", typeof(Int32));
            theDT.Columns.Add("BillItemDate", typeof(DateTime));
            theDT.Columns.Add("Amount", typeof(Decimal));
            theDT.Columns["Amount"].DefaultValue = 0;
            theDT.Columns.Add("PayItem", typeof(Boolean));
            theDT.Columns.Add("PaymentStatus", typeof(Int32));
            return theDT;
        }
        /// <summary>
        /// Initializes the pay payment object.
        /// </summary>
        /// <returns></returns>
        private DataTable initializePayPaymentObject()
        {

            DataTable theDT = new DataTable();
            theDT.Columns.Add("BillID", typeof(Int32));
            theDT.Columns.Add("PaymentType", typeof(Int32));
            theDT.Columns.Add("PaymentName", typeof(String));
            theDT.Columns.Add("RefNo", typeof(String));
            theDT.Columns.Add("Amount", typeof(Decimal));
            theDT.Columns["Amount"].DefaultValue = 0;
            theDT.Columns.Add("TenderedAmount", typeof(Decimal));
            theDT.Columns["TenderedAmount"].DefaultValue = 0;
            return theDT;
        }



        /// <summary>
        /// Populates the payment mode.
        /// </summary>
        void PopulatePaymentMode()
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            using (DataSet theDSXML = new DataSet())
            {
                /// DataTable theDT;
                theDSXML.ReadXml(MapPath("~\\XMLFiles\\AllMasters.con"));

                DataView theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=212";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);

                    BindManager.BindCombo(ddlPaymentMode, theDT, "Name", "ID");
                    // ddlPaymentMode.SelectedValue = grdPayBill.DataKeys[e.Row.RowIndex].Values[1].ToString();
                    theDV.Dispose();
                    theDT.Clear();
                }

            }
        }


        /// <summary>
        /// Handles the Click event of the btnFinish control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnFinish_Click(object sender, EventArgs e)
        {

           /* try
            {
                DataTable paymentInfo = (DataTable)Session["paymentInformation"];

                IBilling BManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");

                DataTable itemsPaid = (DataTable)base.Session["ItemsToPay"];
                DataTable table = BManager.SavePatientBillPayments(this.PatientID, paymentInfo, itemsPaid, this.UserID);
               // Session["transactionID"] = transactionID;
                //Reciept print
                string theUrl;
                base.Session.Remove("ItemsToPay");
                base.Session.Remove("paymentInformation");
                base.Session.Remove("totalbill");
                DataRow row = table.Rows[0];

                string transactionID = row["TransactionID"].ToString();
                string transactionRef = row["TransactionReference"].ToString();

                if (ckbPrintReciept.Checked)
                {

                    theUrl = string.Format("./frmBilling_Reciept.aspx?ReceiptTrxCode={0}&RePrint=false", transactionID);
                    String theOrdScript;
                    theOrdScript = "<script language='javascript' id='PrintReciept'>\n";
                    theOrdScript += "window.location.href = './frmBilling_ClientBill.aspx';\n";
                    theOrdScript += "window.open('" + theUrl + "','Receipt','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes');\n";
                    theOrdScript += "</script>\n";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PrintReciept", theOrdScript);

                }

                else
                {

                    this.textAmountToPay.Text = this.textTenderedAmount.Text = this.textReferenceNo.Text = "";

                    this.labelAmountDue.InnerText = this.lblChange.InnerText = this.lblPaid.InnerText = "";
                    this.NotifyAction("Payment succeeded", "Pay Bill", false);
                    this.NotifyAction("Payment transaction succeeded : Reference " + transactionRef + " Successfully ..", string.Format("Transaction Generated {0} ", transactionRef), false);
                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
            */

        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            base.Session.Remove("paymentInformation");
            btnFinish.Enabled = false;
            this.labelAmountDue.InnerText = AmountDue.ToString();
            lblChange.InnerText = lblPaid.InnerText = textTenderedAmount.Text = "";
            textAmountToPay.Text = AmountDue.ToString();
            textReferenceNo.Text = "";
            ddlPaymentMode.SelectedIndex = -1;
            this.Init_page();
        }
        /// <summary>
        /// Computes the paid amount.
        /// </summary>
        /// <param name="tenderedAmount">The total paid.</param>
        private void computePaidAmount(Decimal tenderedAmount, Decimal amountToPay)
        {

           

            Decimal amountDue = amountToPay - tenderedAmount;
            if (amountDue < 0)//means the payment is enough calculate change and set amount due to 0
                amountDue = 0;

            lblPaid.InnerText = tenderedAmount.ToString();
            labelPaymentType.InnerText = ddlPaymentMode.SelectedItem.Text;
            labelAmountDue.InnerText = amountDue.ToString();
            labelAmountTopay.InnerText = amountToPay.ToString();


            Decimal changeDue = tenderedAmount - amountToPay;

            if (changeDue < 0)//amount tendered is not enough to cover cost of bill. cannot proceed to finish
            {
                //lblChange.InnerText = "Amount Due:";
                lblChange.InnerText = "0";
                btnFinish.Enabled = false;


            }
            else
            {
                if (ddlPaymentMode.SelectedItem.Text != "Cash")//confirm that payment type is cash. change can only be given to cash payments
                    lblChange.InnerText = "0";
                else
                    lblChange.InnerText = (Math.Abs(changeDue)).ToString();
                tblFinish.Visible = true;
                btnFinish.Enabled = true;
                tblCompute.Visible = false;

            }


        }
        /// <summary>
        /// Handles the Click event of the buttonCompute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonCompute_Click(object sender, EventArgs e)
        {

            valid = true;
            isValidEntry = validateTextBox(textTenderedAmount);

            isValidEntry = validateDropDownList(ddlPaymentMode);

            if (ddlPaymentMode.SelectedItem.Text != "Cash")
                isValidEntry = validateTextBox(textReferenceNo);


            if (!isValidEntry)
            {

                return;
            }
            DataTable itemsToPay = this.initializeItemsToPay();
            decimal _total = 0;
            foreach (GridViewRow gridRow in this.grdPayItems.Rows)
            {
                if (gridRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = gridRow.FindControl("chkBxItem") as CheckBox;

                    if (chk != null && chk.Checked)
                    {
                        int billItemId = int.Parse(grdPayItems.DataKeys[gridRow.RowIndex].Values["billitemid"].ToString());
                        int patientID = int.Parse(grdPayItems.DataKeys[gridRow.RowIndex].Values["patientid"].ToString());
                        decimal amount = decimal.Parse(grdPayItems.DataKeys[gridRow.RowIndex].Values["amount"].ToString());
                        DateTime billItemDate = DateTime.Parse(grdPayItems.DataKeys[gridRow.RowIndex].Values["billitemdate"].ToString());
                        itemsToPay.Rows.Add(new object[]
                       {
                        billItemId,
                        patientID,
                        billItemDate,
                        amount,
                        true,
                        0
                       });
                        itemsToPay.AcceptChanges();
                        _total += amount;
                    }
                }
            };
            base.Session["ItemsToPay"] = itemsToPay;
            //  DataTable itemsToPay = this.initializeItemsToPay();
            decimal _amountToPay = 0;
            decimal _amountTendered = 0;

            _amountToPay = _total;
            _amountTendered = Decimal.Parse(textTenderedAmount.Text);
            DataTable theDT = this.initializePayPaymentObject();

            //if ((int)theDT.Rows[0][0] == 0)
            //    theDT.Rows.RemoveAt(0);
            DataRow theDR = theDT.NewRow();
            theDR.SetField("BillID", this.BillID);

            theDR.SetField("PaymentType", ddlPaymentMode.SelectedValue);
            theDR.SetField("PaymentName", ddlPaymentMode.SelectedItem.Text);
            theDR.SetField("RefNo", textReferenceNo.Text);
            theDR.SetField("Amount", _amountToPay);//Session["totalbill"]);
            theDR.SetField("TenderedAmount", _amountTendered);

            theDT.Rows.Add(theDR);
            base.Session["paymentInformation"] = theDT;


            this.computePaidAmount(_amountTendered, _amountToPay);

        }
        /// <summary>
        /// The valid
        /// </summary>
        private bool valid = true;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid entry.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is valid entry; otherwise, <c>false</c>.
        /// </value>
        private bool isValidEntry//checks whether inputed text is valid if any is invalid it will always return false
        {
            get
            {
                return valid;
            }
            set
            {
                valid = valid == false ? false : value;
            }
        }

        /// <summary>
        /// Validates the text box.
        /// </summary>
        /// <param name="inputbox">The inputbox.</param>
        /// <returns></returns>
        private bool validateTextBox(TextBox inputbox)
        {
            if (inputbox.Text.Trim() == "" || inputbox.Text.Trim() == "0")
            {
                inputbox.BorderColor = System.Drawing.Color.Red;
                return false;
            }
            inputbox.BorderColor = System.Drawing.Color.White;
            return true;

        }
        /// <summary>
        /// Validates the drop down list.
        /// </summary>
        /// <param name="inputbox">The inputbox.</param>
        /// <returns></returns>
        private bool validateDropDownList(DropDownList inputbox)
        {
            if (inputbox.SelectedItem.Text.Trim() == "Select")
            {
                inputbox.BorderColor = System.Drawing.Color.Red;
                inputbox.BackColor = System.Drawing.Color.Orange;
                return false;
            }
            inputbox.BorderColor = System.Drawing.Color.White;
            inputbox.BackColor = System.Drawing.Color.White;

            return true;

        }

        /// <summary>
        /// Gets the checked items total.
        /// </summary>
        void GetCheckedItemsTotal()
        {
            decimal _total = 0;
            int selectedCount = 0;
            foreach (GridViewRow gridRow in this.grdPayItems.Rows)
            {
                if (gridRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = gridRow.FindControl("chkBxItem") as CheckBox;

                    if (chk != null && chk.Checked)
                    {
                        decimal amount = decimal.Parse(grdPayItems.DataKeys[gridRow.RowIndex].Values["amount"].ToString());
                        _total += amount;
                        selectedCount += 1;
                    }
                }
            }
            buttonCompute.Enabled = selectedCount > 0;
            if (selectedCount > 0)
            {

                labelAmountDue.InnerText = textAmountToPay.Text = _total.ToString();
                rgAmountToPay.MaximumValue = _total.ToString();

            }
            else
            {
                labelAmountDue.InnerText = textAmountToPay.Text = AmountDue.ToString();
                rgAmountToPay.MaximumValue = AmountDue.ToString();
            }
            CheckBox chkHeader = grdPayItems.HeaderRow.FindControl("chkBxHeader") as CheckBox;
            if (chkHeader != null)
                chkHeader.Checked = selectedCount == grdPayItems.Rows.Count;


        }
        ///// <summary>
        ///// Handles the CheckedChanged event of the chkBxItem control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void chkBxItem_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            bool isChecked = ckb.Checked;
            GridViewRow row = (GridViewRow)(ckb).NamingContainer;
            btnFinish.Enabled = false;
            lblPaid.InnerText = lblChange.InnerText = "";

            //decimal _total = 
            this.GetCheckedItemsTotal();
            tblCompute.Visible = true;

            tblFinish.Visible = false;

          
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkBxHeader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void chkBxHeader_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = ((CheckBox)sender).Checked;
            decimal _total = 0;


            foreach (GridViewRow row in grdPayItems.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkItem = row.FindControl("chkBxItem") as CheckBox;

                    if (chkItem != null) chkItem.Checked = isChecked;
                    if (isChecked)
                    {
                        decimal amount = decimal.Parse(grdPayItems.DataKeys[row.RowIndex].Values["amount"].ToString());
                        _total += amount;

                    }


                }
            }
            // lblTotalBill.InnerText = _total.ToString();
            rgAmountToPay.MaximumValue = _total >= 1 ? _total.ToString() : this.AmountDue.ToString();
            buttonCompute.Enabled = isChecked;
        }

        /// <summary>
        /// Handles the Click event of the btn_close control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btn_close_Click(object sender, EventArgs e)
        {
            base.Session.Remove("ItemsToPay");
            base.Session.Remove("paymentInformation");
            base.Session.Remove("totalbill");
            base.Session.Remove("transactionID");
            base.Session.Remove("BillID");
            string theUrl = "./frmBilling_ClientBill.aspx";
            Response.Redirect(theUrl, false);
        }
        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void showErrorMessage(ref Exception ex)
        {
            this.isError = true;
            //if (this.Debug)
            //{
            //    lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            //}
            //else
            //{
            //    lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team";
            //    this.isError = this.divError.Visible = true;
            //    Exception lastError = ex;
            //    lastError.Data.Add("Domain", "Patient Consumeable Issueance Form");
            //    Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
            //    logger.LogError(ex);

            //}
            CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            if (Session["PatientId"] == null || Convert.ToInt32(Session["PatientId"]) != 0)
            {
                this.NotifyAction("Application has an issue, Please contact Administrator!", "Application Error", true, "window.location.href='../frmFindAddCustom.aspx?srvNm=" + Session["TechnicalAreaName"] + "&mod=0'");
                //Response.Write("<script>alert('Application has an issue, Please contact Administrator!') ; window.location.href='../frmFindAddCustom.aspx?srvNm=" + Session["TechnicalAreaName"] + "&mod=0'</script>");
            }
            else
            {
                if (Session["TechnicalAreaId"] != null || Convert.ToInt16(Session["TechnicalAreaId"]) != 0)
                {
                    this.NotifyAction("Application has an issue, Please contact Administrator!", "Application Error", true, "window.location.href='../frmFacilityHome.aspx';");
                    //Response.Write("<script>alert('Application has an issue, Please contact Administrator!') ; window.location.href='../frmFacilityHome.aspx'</script>");

                }
                else
                {

                    this.NotifyAction("Application has an issue, Please contact Administrator!", "Application Error", true, "window.location.href='../frmLogin.aspx';");
                    //Response.Write("<script>alert('Application has an issue, Please contact Administrator!') ; window.location.href='../frmLogin.aspx'</script>");
                }
            }
            ex = null;
            
        }

        /// <summary>
        /// Handles the RowDataBound event of the gridBillTransaction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridBillTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                string tranStatus = row["TransactionStatus"].ToString();

                string _transactionID = row["TransactionID"].ToString();
                LinkButton printButton = e.Row.FindControl("receiptPrint") as LinkButton;
                if (printButton != null)
                {
                    if (tranStatus == "Paid")
                    {
                        string urlParam = String.Format("openReportPage('./frmBilling_Reciept.aspx?ReceiptTrxCode={0}&RePrint=false');return false;", _transactionID);

                        printButton.OnClientClick = urlParam;
                    }
                    else
                    {
                        printButton.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the RowCommand event of the gridBillTransaction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridBillTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int _transactionID;
            int index;

            //int index = Int32.Parse(e.CommandArgument.ToString());
            //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //GridView myGrid = (GridView)sender;

            if (e.CommandName == "Reverse")
            {

                //index = Convert.ToInt32(e.CommandArgument);
                //  GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                GridView transactionGrid = (GridView)gvr.NamingContainer;
                index = gvr.RowIndex;

                labelReceipt.Text = gvr.Cells[0].Text;
                //GridViewRow row = (transactionGrid.Rows[index]);
                transactionGrid.SelectedIndex = index;
                _transactionID = int.Parse(transactionGrid.SelectedDataKey.Values["TransactionID"].ToString());
                HTransactionID.Value = _transactionID.ToString();
                this.mpeReverse.Show();
                return;
            }
        }
        /// <summary>
        /// Handles the Click event of the btnOkAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOkAction_Click(object sender, EventArgs e)
        {
          //  this.Init_page();
        }
        /// <summary>
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        void NotifyAction(string strMessage, string strTitle, bool errorFlag, string onOkScript = "")
        {
            // ConfirmModalPopupExtender.Hide();
            // this.mpe1.Hide();
            lblNoticeInfo.Text = strMessage;
            lblNotice.Text = strTitle;
            lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
            lblNoticeInfo.Font.Bold = true;
            imgNotice.ImageUrl = (errorFlag) ? "~/Common/images/mb_hand.gif" : "~/Common/images/mb_information.gif";
            btnOkAction.OnClientClick = "";
            if (onOkScript != "" && errorFlag == true)
            {
                btnOkAction.OnClientClick = onOkScript;
            }
            this.notifyPopupExtender.Show();
        }
        /// <summary>
        /// Notifies the action.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="labelNotice">The label notice.</param>
        /// <param name="NoticeInfo">The notice information.</param>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        void NotifyAction(ref ModalPopupExtender control, Label labelNotice, Label NoticeInfo, string strMessage, string strTitle, bool errorFlag)
        {
            // ConfirmModalPopupExtender.Hide();
            // this.mpe1.Hide();
            NoticeInfo.Text = strMessage;
            labelNotice.Text = strTitle;
            NoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
            NoticeInfo.Font.Bold = true;
            // imgNotice.ImageUrl = (errorFlag) ? "~/Common/images/mb_hand.gif" : "~/Common/images/mb_information.gif";
            control.Show();
        }
        /// <summary>
        /// Handles the Click event of the btnActionOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void RequestReversal(object sender, EventArgs e)
        {
            if (textReason.Text.Trim() != "")
            {
                try
                {
                    IBilling bMrg = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling,BusinessProcess.SCM");
                    List<int> ItemToReverse = new List<int>();
                    bMrg.RequestTransactionReversal(Convert.ToInt32(HTransactionID.Value), this.UserID, textReason.Text.Trim(), DateTime.Now, ItemToReverse);
                    mpeReverse.Hide();
                    this.NotifyAction(ref mpeReversalNotify, labelReverseTitle, labelReversalInfo, "Request to Reverse Receipt Number " + labelReceipt.Text + " Successfully ..", string.Format("Reverse {0} ", labelReceipt.Text), false);

                }
                catch (Exception ex)
                {
                    this.showErrorMessage(ref ex);
                    this.NotifyAction(ref mpeReversalNotify, labelReverseTitle, labelReversalInfo, "Request to Reverse Receipt Number " + labelReceipt.Text + " Failed ..<br />" + ex.Message, string.Format("Failed Reverse {0} ", labelReceipt.Text), true);
                }

            };

        }
        /// <summary>
        /// Handles the Click event of the buttonReversalOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonReversalOK_Click(object sender, EventArgs e)
        {
            this.PopulateBillTransactions();
        }
    }
}
