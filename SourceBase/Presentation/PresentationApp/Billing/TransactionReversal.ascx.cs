﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.Clinical;
using Interface.SCM;

namespace IQCare.Web.Billing
{
    public partial class TransactionReversal : System.Web.UI.UserControl
    {
        #region "Subscriber Properties"
        /// <summary>
        /// Gets or sets a value indicating whether to show approval reject column.
        /// </summary>
        /// <value>
        ///   <c>true</c> if Approval otherwise, <c>false</c>.
        /// </value>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Whether to display approval/reject action buttons")]
        [System.ComponentModel.Bindable(true)]
        public bool IsApprovalMode
        {
            private get
            {
                if (this.HApprovalMode.Value == "")
                    this.HApprovalMode.Value = "FALSE";
                return bool.Parse(this.HApprovalMode.Value);

            }
            set
            {
                this.HApprovalMode.Value = value.ToString().ToUpper();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can refund.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can refund; otherwise, <c>false</c>.
        /// </value>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Whether the logged on user can refund")]
        [System.ComponentModel.Bindable(true)]
        public bool CanRefund
        {
            private get
            {
                if (this.HCanRefund.Value == "")
                    this.HCanRefund.Value = "FALSE";
                return bool.Parse(this.HCanRefund.Value);
                // return false;
            }
            set
            {
                this.HCanRefund.Value = value.ToString().ToUpper();
            }
        }
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        [System.ComponentModel.DefaultValue("0")]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Patient ID")]
        [System.ComponentModel.Bindable(true)]
        public int PatientID
        {
            private get
            {
                if (this.HPatientID.Value == "")
                    this.HPatientID.Value = "0";
                return int.Parse(this.HPatientID.Value);

            }
            set
            {
                this.HPatientID.Value = value.ToString().ToUpper();
            }
        }
        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        [System.ComponentModel.DefaultValue("0")]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Location ID")]
        [System.ComponentModel.Bindable(true)]
        public int LocationID
        {
            private get
            {
                if (this.HLocationID.Value == "")
                    this.HLocationID.Value = "0";
                return int.Parse(this.HLocationID.Value);

            }
            set
            {
                this.HLocationID.Value = value.ToString().ToUpper();
            }
        }
        [System.ComponentModel.DefaultValue("openReceiptPage")]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Print JS Method Name")]
        [System.ComponentModel.Bindable(true)]
        public string PrintReceiptJSMethod
        {
            private get
            {
                if (this.HPrintMethodName.Value == "")
                    this.HPrintMethodName.Value = "";
                return (this.HPrintMethodName.Value);

            }
            set
            {
                this.HPrintMethodName.Value = value.ToString().Trim();
            }
        }
        /// <summary>
        /// Gets or sets the print receipt URL.
        /// </summary>
        /// <value>
        /// The print receipt URL.
        /// </value>
        /// 
        [System.ComponentModel.DefaultValue("./frmBilling_Reciept.aspx")]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Print JS Method Name")]
        [System.ComponentModel.Bindable(true)]
        public string PrintReceiptURL
        {
            private get
            {
                if (this.HPrintURL.Value == "")
                    this.HPrintURL.Value = "";
                return (this.HPrintURL.Value);

            }
            set
            {
                this.HPrintURL.Value = value.ToString().Trim();
            }
        }

        #endregion

        #region Subscriber events
        /// <summary>
        /// Occurs when [row command].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when a row is clicked.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler RowCommand;

        /// <summary>
        /// Occurs when [notify command].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when a notifcation need to be sent.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler NotifyCommand;
        /// <summary>
        /// Occurs when [error occurred].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when an error occurs.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler ErrorOccurred;
        #endregion

        ///// <summary>
        ///// The is error
        ///// </summary>
        //bool isError = false;
        /// <summary>
        /// The s approval
        /// </summary>
        protected string sApproval;
        protected string NeedShowPatient
        {
            get
            {
                return this.PatientID == 0 ? "" : "none";
            }
        }

        #region "PageLifeCycle events"
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // this.PopulateReversals();
            }
        }
        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            // divError.Visible = this.isError;
        }

        /// <summary>
        /// Binds a data source to the invoked server control and all its child controls with an option to raise the <see cref="E:System.Web.UI.Control.DataBinding" /> event.
        /// </summary>
        /// <param name="raiseOnDataBinding">true if the <see cref="E:System.Web.UI.Control.DataBinding" /> event is raised; otherwise, false.</param>
        protected override void DataBind(bool raiseOnDataBinding)
        {
            base.DataBind(raiseOnDataBinding);
        }
        #endregion

        #region "Data Methods"
        /// <summary>
        /// Rebinds this instance.
        /// </summary>
        public void Rebind()
        {
            radioShowOptions.SelectedIndex = 0;
            this.PopulateReversals();
        }
        /// <summary>
        /// Populates the patient details.
        /// </summary>
        void PopulatePatientDetails(int patientID)
        {
            IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataTable theDT = ptnMgr.GetPatientRecord(patientID);
            //Session["ClientInfo"] = theDS;
            if (theDT.Rows.Count > 0)
            {
                lblname.Text = String.Format("{0} {1} {2}", theDT.Rows[0]["Firstname"], theDT.Rows[0]["Middlename"], theDT.Rows[0]["Lastname"]);
                lblFacilityID.Text = theDT.Rows[0]["PatientFacilityID"].ToString();
            }
            ptnMgr = null;
        }
        /// <summary>
        /// Populates the patient reversals.
        /// </summary>
        void PopulateReversals()
        {
            try
            {

                string reversalRef = "";
                string showOption = (radioShowOptions.SelectedIndex > -1) ? radioShowOptions.SelectedValue : "ALL";
                DataTable dtReversal = new DataTable("dtReversals");
                if (this.IsApprovalMode || this.PatientID == 0)
                {
                    dtReversal = this.BillingManager.GetReversalRequests(this.LocationID, "", showOption);
                    this.gridReversal.DataSource = dtReversal;
                }
                else if (this.PatientID > 0)
                {
                    int patientID = this.PatientID;
                    dtReversal = this.BillingManager.GetReversalRequests(this.LocationID, reversalRef, showOption, patientID);
                    this.gridReversal.DataSource = dtReversal;
                }

                this.gridReversal.DataBind();
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }

        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the RowCommand event of the gridReversal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridReversal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex;
            rowIndex = Int32.Parse(e.CommandArgument.ToString());
            this.BillID = int.Parse(gridReversal.DataKeys[rowIndex]["billid"].ToString());
            this.TransactionID = int.Parse(gridReversal.DataKeys[rowIndex]["transactionid"].ToString());
            this.ReversalID = int.Parse(gridReversal.DataKeys[rowIndex]["transactionreversalid"].ToString());
            GridViewRow row = (gridReversal.Rows[rowIndex]);
            if (e.CommandName == "Expand")
            {
                return;
            }
            else if (e.CommandName == "Approve" || e.CommandName == "Reject")
            {
                labelParamTitle.Text = e.CommandName + " Reversal Request";
                btnApprove.Visible = divCashRefund.Visible = (e.CommandName == "Approve");
                this.sApproval = (e.CommandName == "Approve") ? "" : "none";
                btnReject.Visible = (e.CommandName == "Reject");
                iconReject.Visible = (e.CommandName == "Reject");//Written by Rahmat as on 18-Apr-2017
                textReason.Text = "";
                parameterPopup.Show();
                this.PopulateReversals();
                this.OnRowCommand(this, new CommandEventArgs("RowClick", rowIndex));
            }
            else if (e.CommandName == "Refund")
            {
                int reversalId = int.Parse(gridReversal.DataKeys[rowIndex]["transactionreversalid"].ToString());

                this.BillingManager.RefundCash(reversalId, this.UserID);
               
                string theUrl = string.Format("{0}?ReceiptTrxCode={1}&RePrint=false&TranXORType=17", this.PrintReceiptURL, this.TransactionID);
                string urlParam = String.Format("{0}('{1}');", this.PrintReceiptJSMethod, theUrl);
                this.NotifyAction("Refund Successful", "Transaction Refund", false,urlParam);
                this.PopulateReversals();
                this.OnRowCommand(this, new CommandEventArgs("RowClick", rowIndex));





            }
            else if (e.CommandName == "PrintRefund") //todo designer the refund receipt
            {

                this.OnRowCommand(this, new CommandEventArgs("RowClick", rowIndex));
            }
            else if ("BillDetails" == e.CommandName)
            {
                DataTable dt = this.BillingManager.GetBillItems(this.BillID);
                DataTable dtBill = this.BillingManager.GetBillDetails(this.BillID);
                gridBillItems.DataSource = dt;
                gridBillItems.DataBind();

                int thisPatientId = int.Parse(dtBill.Rows[0]["PatientID"].ToString());
                labelBillAmount.Text = DataBinder.Eval(dtBill.DefaultView[0], "BillAmount", "{0:N}");
                labelUnPaidAmount.Text = DataBinder.Eval(dtBill.DefaultView[0], "UnpaidAmount", "{0:N}");
                labelBillDate.Text = DataBinder.Eval(dtBill.DefaultView[0], "BillDate", "{0:dd-MMM-yyyy HH:mm}");
                //Convert.ToDateTime(dtBill.Rows[0]["BillDate"]).ToString("dd-MMM-yyyy HH:mm");
                labelBilledBy.Text = dtBill.Rows[0]["CreatedBy"].ToString();
                labelBillNumber.Text = dtBill.Rows[0]["BillNumber"].ToString();
                labelBillStatus.Text = dtBill.Rows[0]["BillStatus"].ToString();
                if (this.PatientID == 0)
                    this.PopulatePatientDetails(thisPatientId);
                billDetailsPopup.Show();
            }
        }
        /// <summary>
        /// Handles the RowCreated event of the gridReversal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridReversal_RowCreated(object sender, GridViewRowEventArgs e)
        {

            gridReversal.Columns[9].Visible = this.IsApprovalMode;
            gridReversal.Columns[10].Visible = this.CanRefund;

        }
        /// <summary>
        /// Handles the RowDataBound event of the gridReversal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridReversal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;


                string transactionID = rowView["TransactionID"].ToString();

                string approvalStatus = rowView["ApprovalStatus"].ToString();
                if (approvalStatus == "Pending")
                {

                }
                else
                {

                }
                string reasonText = rowView["ReversalReason"].ToString().Trim();
                Label lblReason = (Label)e.Row.FindControl("labelReversalReason");
                string minReason = reasonText;
                if (lblReason != null && reasonText.Length > 15)
                {
                    minReason = reasonText.Substring(0, 15);
                }
                lblReason.Text = minReason;
                Button printButton = e.Row.FindControl("refundPrint") as Button;
                if (printButton != null)
                {
                    string theUrl = string.Format("{0}?ReceiptTrxCode={1}&RePrint=false&TranXORType=17", this.PrintReceiptURL, transactionID);
                    string urlParam = String.Format("{0}('{1}');return false;", this.PrintReceiptJSMethod, theUrl);

                    printButton.OnClientClick = urlParam;
                }
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the radioShowOptions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void radioShowOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopulateReversals();
        }
        /// <summary>
        /// Handles the Click event of the btnApprove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                this.BillingManager.ApproveRejectTransactionReversal(this.ReversalID, this.TransactionID, true, this.UserID, textReason.Text.Trim(), DateTime.Now, (rblOptionRefund.SelectedValue == "Yes"));
                this.NotifyAction("Reversal has been successful", "Bill Reversal", false);
                parameterPopup.Hide();
                this.PopulateReversals();
            }
            catch (Exception ex)
            {
                this.NotifyAction("Reversal has not been successful", "Bill Reversal", true);
                parameterPopup.Hide();
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Handles the Click event of the btnReject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                this.BillingManager.ApproveRejectTransactionReversal(this.ReversalID, this.TransactionID, false, this.UserID, textReason.Text.Trim(), DateTime.Now);
                this.NotifyAction("Reversal has been Rejected", "Bill Reversal", false);
                parameterPopup.Hide();
                this.PopulateReversals();
            }
            catch (Exception ex)
            {
                this.NotifyAction("Reversal Rejection has not been successful", "Bill Reversal", true);
                parameterPopup.Hide();
                this.showErrorMessage(ref ex);
            }
        }

        #endregion

        #region "Wired Events"
        /// <summary>
        /// Called when [notify required].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnNotifyRequired(object sender, CommandEventArgs e)
        {
            if (this.NotifyCommand != null)
            {
                this.NotifyCommand(sender, e);
            }
        }
        /// <summary>
        /// Called when [error occured].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnErrorOccured(object sender, CommandEventArgs e)
        {
            if (this.ErrorOccurred != null)
            {
                this.ErrorOccurred(sender, e);
            }
        }
        /// <summary>
        /// Called when [patient selected].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnRowCommand(object sender, CommandEventArgs e)
        {
            if (this.RowCommand != null)
            {
                this.RowCommand(sender, e);
            }
        }
        #endregion

        #region Display Methods
        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void showErrorMessage(ref Exception ex)
        {

            this.OnErrorOccured(this, new CommandEventArgs("Error", ex));
        }
        /// <summary>
        /// Shows the reversal notes.
        /// </summary>
        /// <param name="approvalStatus">The approval status.</param>
        /// <returns></returns>
        protected string ShowReversalNotes(object approvalStatus)
        {
            if (approvalStatus.ToString().ToLower() == "pending") return "none";
            else return "";
        }
        /// <summary>
        /// Shows the approval/Reject buttons.
        /// </summary>
        /// <param name="approvalStatus">The approval status.</param>
        /// <returns></returns>
        protected string ShowApproval(object approvalStatus)
        {
            if (approvalStatus.ToString().ToLower() == "pending") return "";
            else return "none";
        }
        /// <summary>
        /// Shows the hide refund.
        /// </summary>
        /// <param name="approvalStatus">The approval status.</param>
        /// <param name="refunded">if set to <c>true</c> [refunded].</param>
        /// <returns></returns>
        protected string ShowHideRefund(object approvalStatus, object refunded)
        {


            if (refunded.ToString().ToLower() == "false" && approvalStatus.ToString().ToLower() == "approved") return "";
            else return "none";
        }
        /// <summary>
        /// Shows the reson notes.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <returns></returns>
        protected string ShowResonNotes(object reason)
        {
            if (reason.ToString().Length > 15) return "";
            else return "none";
        }
        /// <summary>
        /// Shows the print refund button only if refund is true.
        /// </summary>
        /// <param name="refunded">The refunded.</param>
        /// <returns></returns>
        protected string ShowPrintRefund(object refunded)
        {
            if (refunded.ToString().ToLower() == "false") return "none";
            else return "";
        }
        /// <summary>
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        void NotifyAction(string strMessage, string strTitle, bool errorFlag,string onOkScript="")
        {

            lblNoticeInfo.Text = strMessage;
            lblNotice.Text = strTitle;
            lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
            lblNoticeInfo.Font.Bold = true;

            btnOkAction.OnClientClick = "";
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Message", strMessage));
            list.Add(new KeyValuePair<string, string>("Title", strTitle));
            list.Add(new KeyValuePair<string, string>("errorFlag", errorFlag.ToString().ToLower()));
            if (onOkScript != "")
            {
                list.Add(new KeyValuePair<string, string>("OkScript", onOkScript));
                btnOkAction.OnClientClick = onOkScript;
            } 
            this.notifyPopupExtender.Show();
            this.OnNotifyRequired(this, new CommandEventArgs("Notify", list));
        }

        #endregion

        #region MyProperties
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        int TransactionID
        {
            get
            {
                return int.Parse(HTranID.Value);
            }
            set
            {
                HTranID.Value = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the reversal identifier.
        /// </summary>
        /// <value>
        /// The reversal identifier.
        /// </value>
        int ReversalID
        {
            get
            {
                return int.Parse(HReversalId.Value);
            }
            set
            {
                HReversalId.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the bill identifier.
        /// </summary>
        /// <value>
        /// The bill identifier.
        /// </value>
        int BillID
        {
            get
            {
                return int.Parse(HBillId.Value);
            }
            set
            {
                HBillId.Value = value.ToString();
            }
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
                return Convert.ToInt32(base.Session["AppUserId"]);
            }
        }
        /// <summary>
        /// Gets the billing manager.
        /// </summary>
        /// <value>
        /// The billing manager.
        /// </value>
        IBilling BillingManager
        {
            get
            {

                return (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");

            }
        }
        #endregion

    }
}