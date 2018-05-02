using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.SCM;
using Entities.Billing;

namespace IQCare.Web.Billing
{
    public partial class PayBillCorporate : System.Web.UI.UserControl, IPayment
    {
        #region variables
        /// <summary>
        /// The valid
        /// </summary>
        private bool valid = true;

        #endregion

        #region properties
        /// <summary>
        /// Gets the _ items to pay.
        /// </summary>
        /// <value>
        /// The _ items to pay.
        /// </value>
        List<BillItem> _ItemsToPay
        {
            get
            {
                return (List<BillItem>)ItemForPay.DynamicInvoke();
            }
        }
        /// <summary>
        /// //checks whether inputed text is valid if any is invalid it will always return false
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is valid entry; otherwise, <c>false</c>.
        /// </value>
        private bool isValidEntry
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
        /// Gets or sets the payment method identifier.
        /// </summary>
        /// <value>
        /// The payment method identifier.
        /// </value>
        int PaymentMethodID
        {
            get
            {
                return int.Parse(this.HPayMethodID.Value);
            }

            set
            {
                this.HPayMethodID.Value = value.ToString();

            }
        }
        /// <summary>
        /// Gets or sets the name of the payment method.
        /// </summary>
        /// <value>
        /// The name of the payment method.
        /// </value>
        string PaymentMethodName
        {
            get
            {
                return this.HPayMethodName.Value;
            }

            set
            {
                this.HPayMethodName.Value = value.ToString();

            }
        }
        #endregion

        #region "Wired Events"
        /// <summary>
        /// Called when [cancel compute].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnCancelCompute(object sender, CommandEventArgs e)
        {
            if (this.CancelCompute != null)
            {
                this.CancelCompute(sender, e);
            }
        }
        /// <summary>
        /// Called when [pay execute].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnPayExecute(object sender, CommandEventArgs e)
        {
            if (this.ExecutePayment != null)
            {
                this.ExecutePayment(sender, e);
            }
        }
        /// <summary>
        /// Called when [pay complete].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnPayComplete(object sender, CommandEventArgs e)
        {
            if (this.PayComplete != null)
            {
                this.PayComplete(sender, e);
            }
        }
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
        #endregion

        #region IPayment

        #region "Subscriber Properties"
        /// <summary>
        /// Gets or sets the amount due.
        /// </summary>
        /// <value>
        /// The amount due.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public decimal AmountDue
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
        /// Gets or sets the amount to pay.
        /// </summary>
        /// <value>
        /// The amount to pay.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public decimal AmountToPay
        {
            get
            {
                return decimal.Parse(this.HDAmountToPay.Value);
            }
            set
            {
                this.HDAmountToPay.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the bill amount.
        /// </summary>
        /// <value>
        /// The bill amount.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public decimal BillAmount
        {
            get
            {
                return decimal.Parse(this.HBillAmount.Value);
            }
            set
            {
                this.HBillAmount.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the bill identifier.
        /// </summary>
        /// <value>
        /// The bill identifier.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public int BillID
        {
            get
            {
                return Convert.ToInt32(HBillID.Value);
            }
            set
            {
                this.HBillID.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the bill location identifier.
        /// </summary>
        /// <value>
        /// The bill location identifier.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public int BillLocationID
        {
            get
            {
                return Convert.ToInt32(HLocationID.Value);
            }
            set
            {
                HLocationID.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the bill pay option.
        /// </summary>
        /// <value>
        /// The bill pay option.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public BillPaymentOptions BillPayOption
        {
            get
            {
                return (BillPaymentOptions)Convert.ToInt32(this.HPayMode.Value);
            }
            set
            {
                this.HPayMode.Value = Convert.ToInt32(value).ToString();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has transaction.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has transaction; otherwise, <c>false</c>.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public bool HasTransaction
        {
            get
            {
                return HTran.Value == "TRUE";
            }
            set
            {
                HTran.Value = value.ToString().ToUpper();
            }
        }
        /// <summary>
        /// Gets or sets the payment mode.
        /// </summary>
        /// <value>
        /// The payment mode.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public PaymentMethod PaymentMode
        {
            get
            {
                return new PaymentMethod() { ID = this.PaymentMethodID, Name = this.PaymentMethodName };

            }
            set
            {
                this.PaymentMethodID = value.ID.Value;
                this.PaymentMethodName = value.Name;
            }
        }       
        /// <summary>
        /// Gets or sets the item for pay.
        /// </summary>
        /// <value>
        /// The item for pay.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public Delegate ItemForPay
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public int PatientID
        {
            get
            {
                return Convert.ToInt32(HPatientID.Value);
            }
            set
            {
                this.HPatientID.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public int UserID
        {
            get
            {
                return Convert.ToInt32(HUserID.Value);
            }
            set
            {
                this.HUserID.Value = value.ToString();
            }
        }
        #endregion

        #region Subscriber events
        /// <summary>
        /// Occurs when [cancel compute].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when compute balance is canceled.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler CancelCompute;
        /// <summary>
        /// Occurs when [pay complete].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when finish payment events completes.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler PayComplete;
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

        /// <summary>
        /// Occurs when [execute payment].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised to execute payment.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler ExecutePayment;

        #endregion

        #region Compute | Fetch | Clear Data
        /// <summary>
        /// Rebinds this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Rebind()
        {
            panelCompute.Visible = true;
            //tblFinish.Visible = false;
            panelFinish.Visible = false;
            this.Populate();
        }
        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Clear()
        {
            base.Session.Remove("paymentInformation");
            btnFinish.Enabled = false;
            this.labelAmountDue.InnerText = AmountDue.ToString();
            lblChange.InnerText = lblPaid.InnerText = "";
            lblAmountToPay.Text = textAmountToPay.Text = AmountDue.ToString();
            textReferenceNo.Text = "";           
            panelCompute.Visible = true;
            panelFinish.Visible = false;
            base.Session.Remove("ItemsToPay");
        }
        /// <summary>
        /// Computes this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Compute()
        {
            valid = true;

            string strPaymentMode = this.PaymentMethodName;


           // isValidEntry = validateDropDownList(ddlAccounts);


            isValidEntry = validateTextBox(textReferenceNo);
            if (!isValidEntry)
            {

                return;
            }
            //  DataTable itemsToPay = this.initializeItemsToPay();
            decimal _amountToPay = 0;
            decimal _amountTendered = 0;

            _amountToPay = Decimal.Parse(textAmountToPay.Text);

            BillPaymentInfo payObject = new BillPaymentInfo()
            {
                BillID = this.BillID,
                LocationID = this.BillLocationID,
                AmountPayable = _amountToPay,
                Amount = _amountToPay,
                TenderedAmount = _amountTendered,
                ReferenceNumber = textReferenceNo.Text.Trim(),
                Deposit = false,
                PaymentMode = new PaymentMethod() { ID = this.PaymentMethodID, Name = this.PaymentMethodName },
                ItemsToPay = null
            };
            base.Session["paymentInformation"] = payObject;
            {
                lblPaid.InnerText = _amountTendered.ToString();
                labelPaymentType.InnerText = this.PaymentMethodName;
                labelAmountTopay.InnerText = _amountToPay.ToString();

                List<BillItem> itemsList = this._ItemsToPay;

                decimal _totalToPay = _amountToPay;

                if (itemsList != null && itemsList.Count > 0)
                    _totalToPay = itemsList.Sum(i => i.Amount);

                if (!this.HasTransaction && _totalToPay == _amountToPay)
                {
                    payObject.ItemsToPay = itemsList;
                }
                else
                {
                    if (this.BillPayOption == BillPaymentOptions.SelectItem)
                    {
                        payObject.ItemsToPay = itemsList;
                    }
                }
                if (this.AllowPartialPayment) _totalToPay = _amountToPay;

                base.Session["paymentInformation"] = payObject;
                {
                    lblChange.InnerText = "0";                   
                    panelFinish.Visible = true;
                    btnFinish.Enabled = true;             
                    panelCompute.Visible = false;
                    //amount of bill pending after this transaction
                    Decimal amountAfterThisTransaction = this.AmountDue - _amountToPay;
                    labelAmountDue.InnerText = amountAfterThisTransaction.ToString();

                }
            };

        }
        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Validate()
        {
            
        }
        #endregion

        #endregion

        #region event handlers
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Binds a data source to the invoked server control and all its child controls with an option to raise the <see cref="E:System.Web.UI.Control.DataBinding" /> event.
        /// </summary>
        /// <param name="raiseOnDataBinding">true if the <see cref="E:System.Web.UI.Control.DataBinding" /> event is raised; otherwise, false.</param>
        protected override void DataBind(bool raiseOnDataBinding)
        {
            base.DataBind(raiseOnDataBinding);

        }
        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {


        }
        /// <summary>
        /// Handles the Click event of the buttonCompute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonCompute_Click(object sender, EventArgs e)
        {
            this.Compute();
        }
        /// <summary>
        /// Handles the Click event of the buttonStepBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonStepBack_Click(object sender, EventArgs e)
        {
            this.Clear();
            this.OnCancelCompute(this, new CommandEventArgs("CancelCompute", BillID));
        }
        /// <summary>
        /// Handles the Click event of the btnFinish control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                BillPaymentInfo paymentInfo = (BillPaymentInfo)Session["paymentInformation"];

                IBilling BManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
                //  DataTable _items = (DataTable)base.Session["ItemsToPay"];
                //  paymentInfo.Rows[0]["PrintReceipt"] = ckbPrintReciept.Checked;

                paymentInfo.PrintReceipt = ckbPrintReciept.Checked;
                Session["paymentInformation"] = paymentInfo;

                this.OnPayExecute(this, new CommandEventArgs("Execute", paymentInfo));

            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Clear();
        }
        /// <summary>
        /// Handles the Click event of the btnOkAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOkAction_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region private methods
        /// <summary>
        /// Initializes the pay payment object.
        /// </summary>
        /// <returns></returns>
        private DataTable initializePayPaymentObject()
        {

            DataTable theDT = new DataTable();
            theDT.Columns.Add("BillID", typeof(Int32));
            theDT.Columns.Add("LocationID", typeof(Int32));
            theDT.Columns["LocationID"].DefaultValue = this.BillLocationID;
            theDT.Columns.Add("PaymentType", typeof(Int32));
            theDT.Columns.Add("PaymentName", typeof(String));
            theDT.Columns.Add("RefNo", typeof(String));
            theDT.Columns.Add("Amount", typeof(Decimal));
            theDT.Columns["Amount"].DefaultValue = 0;
            theDT.Columns.Add("TenderedAmount", typeof(Decimal));
            theDT.Columns["TenderedAmount"].DefaultValue = 0;
            theDT.Columns.Add("IsDeposit", typeof(Boolean));
            theDT.Columns["IsDeposit"].DefaultValue = false;
            theDT.Columns.Add("PrintReceipt", typeof(Boolean));
            theDT.Columns["PrintReceipt"].DefaultValue = true;
            return theDT;
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
                valid = false;
                return false;
            }
            inputbox.BorderColor = System.Drawing.Color.White;
            valid = true;
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
                valid = false;
                return false;
            }
            inputbox.BorderColor = System.Drawing.Color.White;
            inputbox.BackColor = System.Drawing.Color.White;
            valid = true;
            return true;

        }
        /// <summary>
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        void NotifyAction(string strMessage, string strTitle, bool errorFlag)
        {

            lblNoticeInfo.Text = strMessage;
            lblNotice.Text = strTitle;
            lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
            lblNoticeInfo.Font.Bold = true;
            this.notifyPopupExtender.Show();
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Message", strMessage));
            list.Add(new KeyValuePair<string, string>("Title", strTitle));
            list.Add(new KeyValuePair<string, string>("errorFlag", errorFlag.ToString().ToLower()));
            this.OnNotifyRequired(this, new CommandEventArgs("Notify", list));
        }
        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void showErrorMessage(ref Exception ex)
        {

            this.OnErrorOccured(this, new CommandEventArgs("Error", ex));
        }
        /// <summary>
        /// Gets a value indicating whether [allow partial payment].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow partial payment]; otherwise, <c>false</c>.
        /// </value>
        bool AllowPartialPayment
        {
            get
            {
                return (this.BillPayOption == BillPaymentOptions.FullBill || !this.HasTransaction) && (this.AmountDue == this.AmountToPay);
            }
        }
        /// <summary>
        /// Populates this instance.
        /// </summary>
        void Populate()
        {

            lblTotalBill.InnerText = this.BillAmount.ToString();
            buttonCompute.Visible = btnFinish.Visible = !(this.AmountToPay == 0);
            rgAmountToPay.Enabled = !(this.AmountToPay == 0);
            labelAmountOutstanding.Text = labelAmountDue.InnerText = this.AmountDue.ToString();

            lblAmountToPay.Text = textAmountToPay.Text = this.AmountToPay.ToString();

            this.rgAmountToPay.MaximumValue = this.AmountDue.ToString();
            this.rgAmountToPay.MinimumValue = "0";
            this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", this.AmountDue);

            labelPaymentMode.Text = this.PaymentMethodName;

            if (this.AllowPartialPayment)
            {
                lblAmountToPay.Visible = false;
                rgAmountToPay.Enabled = textAmountToPay.Visible = true;

            }
            else
            {
                lblAmountToPay.Visible = true;
                rgAmountToPay.Enabled = textAmountToPay.Visible = false;
            }

            //this.CashDepositRules();

            this.Validate();
        }

        void LoadAccounts()
        {

        }

        #endregion
      
    }
}