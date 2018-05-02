using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Interface.Administration;
using Application.Presentation;
using Application.Common;
using IQCare.CustomConfig;
using System.Web.Configuration;
using System.Configuration;
using Interface.SCM;
using Entities.Billing;

namespace IQCare.Web.Billing
{
    public partial class BillingAdmin_PaymentType : LogPage
    {
        AuthenticationManager Authentication = new AuthenticationManager();
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Authentication.HasFeatureRight(ApplicationAccess.BillingConfiguration, (DataTable)Session["UserRight"]))
                {
                    string theUrl = string.Format("{0}", "./frmLogin.aspx");
                    //Response.Redirect(theUrl);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
                if (Page.IsPostBack != true)
                {

                    lblHeader.Text = "Payment Type";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Billing Administration >> ";
                    //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Payment Type";
                    this.PopulatePaymentType();
                }
                btnCancel.OnClientClick = "javascript:window.location.href='../frmFacilityHome.aspx';return false;";
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanging event of the grdCustom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSelectEventArgs"/> instance containing the event data.</param>
        protected void gridPaymentType_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = gridPaymentType.PageIndex;
            int thePageSize = gridPaymentType.PageSize;

            GridViewRow gridRow = gridPaymentType.Rows[e.NewSelectedIndex];
            gridPaymentType.SelectedIndex = e.NewSelectedIndex;
            errorLabel.Text = "";

            //this.SerialNumber = gridRow.Cells[0].Text.Trim();
            Label lblStatus = gridRow.FindControl("labelStatus") as Label;
            string stStatus = lblStatus.Text.Trim().ToUpper();
            rblStatus.SelectedIndex = stStatus == "ACTIVE" ? 0 : 1;
            this.CurrentPayMethodID = this.gridPaymentType.SelectedDataKey["ID"].ToString();
            this.buttonSubmit.Text = "Update";
            this.buttonSubmit.CommandName = "UPDATE";

            this.PopulatePaymentPlugins();
            Label lblHandler = gridRow.FindControl("labelHandler") as Label;
            if (lblHandler != null)
            {
                this.CurrentHandler = lblHandler.Text.Trim();
            }

            this.CurrentPayMethodName = this.gridPaymentType.SelectedDataKey["Name"].ToString();
            textDescription.Text = PaymentConfigHelper.PayElementDescription(this.CurrentPayMethodName);
            this.prevTypeCode.Value = this.textPaymentTypeCode.Text = PaymentConfigHelper.PayElementCode(this.CurrentPayMethodName);
            if (CurrentPayMethodName.ToLower().Trim() == "cash" || CurrentPayMethodName.ToLower().Trim() == "deposit") disableTypeEditing();//disable editing for cash and Deposit

            this.paymentTypePopup.Show();
        }
        private void disableTypeEditing()
        {
            textDescription.Enabled = false;
            textPaymentTypeName.Enabled = false;
            ddlHandler.Enabled = false;
        }

        /// <summary>
        /// Handles the Sorting event of the grdCustom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void gridPaymentType_Sorting(object sender, GridViewSortEventArgs e)
        {
            //Init_Form(); 
            GridView theGD = (GridView)sender;
            IQCareUtils SortManager = new IQCareUtils();
            DataView theDV;
            if (ViewState["gridSortDirection"].ToString() == "Asc")
            {
                theDV = SortManager.GridSort((DataTable)this.ViewState["gridSource"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
                ViewState["gridSortDirection"] = "Desc";
            }
            else
            {
                theDV = SortManager.GridSort((DataTable)this.ViewState["gridSource"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
                ViewState["gridSortDirection"] = "Asc";
            }

            theGD.Columns.Clear();
            theGD.DataSource = theDV;

        }

        /// <summary>
        /// Handles the RowDataBound event of the grdCustom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridPaymentType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PaymentMethod rowItem = ((PaymentMethod)e.Row.DataItem);
                if (rowItem.Locked)
                {

                    e.Row.Attributes.Add("onmouseover", "this.style.cursor='help';");
                    string theScript = "NotifyMessage('This is a system generated method and cannot be modified.');return false;";
                    e.Row.Attributes.Add("onclick", theScript);

                }
                else
                {
                    e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';");
                    e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                    // Page.ClientScript.GetPostBackClientHyperlink(grdPatienBill, "Select$" + e.Row.RowIndex);
                    e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackClientHyperlink(gridPaymentType, "Select$" + e.Row.RowIndex.ToString()));
                }

            }
        }
        /// <summary>
        /// Clears the controls.
        /// </summary>
        void ClearControls()
        {
            this.CurrentHandler = "";
            this.CurrentPayMethodID = "-1";
            errorLabel.Text = this.textDescription.Text = this.textPaymentTypeCode.Text = this.textPaymentTypeName.Text = "";
            this.prevhandlerName.Value = this.prevPaymentName.Value = this.prevTypeCode.Value = "";
            rblStatus.SelectedIndex = 0;
            this.buttonSubmit.Text = "Save";
            this.buttonSubmit.CommandName = "NEW";
            this.PopulatePaymentPlugins();
        }
        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.ClearControls();
            this.PopulatePaymentPlugins();
            this.paymentTypePopup.Show();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl = "frmAdmin_PMTCT_CustomItems.aspx";
            Response.Redirect(theUrl);
        }
        #region "User Functions"

        /// <summary>
        /// Populates the payment plugins.
        /// </summary>
        private void PopulatePaymentPlugins()
        {
            List<string> _handlers = PaymentConfigHelper.Handlers();
            ddlHandler.ClearSelection();
            ddlHandler.Items.Clear();
            ddlHandler.Items.Add(new ListItem("Select...", ""));
            foreach (string handlerName in _handlers)
            {

                string controlName = PaymentConfigHelper.HandlerControlName(handlerName);
                string description = PaymentConfigHelper.HandlerDescription(handlerName);
                ddlHandler.Items.Add(new ListItem(handlerName, handlerName));

            }
        }


        /// <summary>
        /// Populates the type of the payment.
        /// </summary>
        void PopulatePaymentType()
        {
            IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
            List<PaymentMethod> paymentMethods = BillingManager.GetPaymentMethods("");

            PaymentConfigHelper.RefreshSection();
            if (paymentMethods != null)
            {
                int i = 0;
                for (i = 0; i < paymentMethods.Count(); i++)//PaymentMethod e in paymentMethods)
                {
                    PaymentMethod e = paymentMethods[i];
                    string name = e.Name;

                    if (PaymentConfigHelper.PaymethodExists(name))
                    {
                        try
                        {
                            string _thishandler = PaymentConfigHelper.GetHandlerNameByPayMethod(name);
                            e.MethodDescription = PaymentConfigHelper.PayElementDescription(name);
                            if (!string.IsNullOrEmpty(_thishandler))
                            {
                                e.Handler = _thishandler;
                                e.HandlerDescription = PaymentConfigHelper.HandlerDescription(_thishandler);
                                e.ControlName = PaymentConfigHelper.HandlerControlName(_thishandler);
                            }

                        }
                        catch { }

                    }
                }
                gridPaymentType.DataSource = paymentMethods;
                gridPaymentType.DataBind();
            }


        }

        /// <summary>
        /// Handles the Click event of the buttonSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {

            string paymentName = textPaymentTypeName.Text;
            string handlerName = ddlHandler.SelectedItem.Value;

            if (textPaymentTypeName.Text == "")
            {
                this.NotifyAction("Name of the payment method is required", "Validation", true);
                this.paymentTypePopup.Show();
                return;
            }
            if (ddlHandler.SelectedIndex < 1)
            {

                this.NotifyAction("Please select the plugin for this type of payment", "Validation", true);
                this.paymentTypePopup.Show();
                return;
            }
       /*     if (txtSeqNo.Text == "")
            {
                this.NotifyAction("Priority is required", "Validation", true);
                this.paymentTypePopup.Show();
                return;
            }*/


            try
            {
                bool modify = false;
                if (PaymentConfigHelper.PaymethodExists(paymentName))
                {
                    modify = true;
                }

                if (this.CurrentHandler == "" || !modify)
                {

                    PaymentConfigHelper.AddPayElement(handlerName, textPaymentTypeName.Text, textPaymentTypeName.Text.Trim().ToUpper(), textDescription.Text);
                    PaymentConfigHelper.RefreshSection();
                }
                else
                {
                    PaymentConfigHelper.ModifyPayElement(this.CurrentHandler, this.CurrentPayMethodName, handlerName, textPaymentTypeName.Text, textPaymentTypeCode.Text, textDescription.Text);
                    PaymentConfigHelper.RefreshSection();
                }

                //this.PersistValues(buttonSubmit.CommandName);

                IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");

                PaymentMethod thisMethod = new PaymentMethod() { Name = paymentName, Locked = false, Active = rblStatus.SelectedValue == "1" };
                if (buttonSubmit.CommandName == "UPDATE")
                {
                    thisMethod.ID = Convert.ToInt32(this.CurrentPayMethodID);
                }

                BillingManager.SavePaymentMethod(thisMethod, this.UserID);

                if (buttonSubmit.CommandName == "NEW")
                {
                    this.NotifyAction("New payment method addedd successfully", string.Format("{0} {1} ", "Adding Payment method", paymentName), false);

                }
                else
                {
                    this.NotifyAction("Payment method updated successfully", string.Format("{0} {1} ", "Updating Payment method", paymentName), false);

                }
                this.PopulatePaymentType();
                return;
            }
            catch (Exception ex)
            {
                this.NotifyAction(ex.Message.ToString(), "Error Occured", true);
                return;

            }
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
            lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
            lblNoticeInfo.Font.Bold = true;
            imgNotice.ImageUrl = (errorFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
            btnOkAction.OnClientClick = "";
            if (onOkScript != "" && errorFlag == true)
            {
                btnOkAction.OnClientClick = onOkScript;
            }
            this.notifyPopupExtender.Show();
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="BillingAdmin_PaymentType"/> is debug.
        /// </summary>
        /// <value>
        ///   <c>true</c> if debug; otherwise, <c>false</c>.
        /// </value>
        bool Debug
        {
            get
            {
                bool _debug = true;
                bool.TryParse(ConfigurationManager.AppSettings.Get("DEBUG").ToLower(), out _debug);
                return _debug;
            }
        }
        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void showErrorMessage(ref Exception ex)
        {
            //this.isError = true;
            //if (this.Debug)
            //{
            //    lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            //}
            //else
            //{
            //    lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team";
            //    this.divError.Visible = true;
            //    Exception lastError = ex;
            //    lastError.Data.Add("Domain", "Payment method administration");
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
        /// Gets the current pay method identifier.
        /// </summary>
        /// <value>
        /// The current pay method identifier.
        /// </value>
        string CurrentPayMethodID
        {
            get
            {
                return currentID.Value.Trim();
            }
            set
            {
                this.currentID.Value = value;
            }
        }
        /// <summary>
        /// Gets the name of the current pay method.
        /// </summary>
        /// <value>
        /// The name of the current pay method.
        /// </value>
        string CurrentPayMethodName
        {
            get
            {
                return this.prevPaymentName.Value.Trim();
            }
            set
            {
                this.prevPaymentName.Value = this.textPaymentTypeName.Text = value;
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
                return Convert.ToInt32(Session["AppUserId"]);
            }
        }
        /// <summary>
        /// Gets the current handler.
        /// </summary>
        /// <value>
        /// The current handler.
        /// </value>
        string CurrentHandler
        {
            get
            {
                return prevhandlerName.Value.Trim();
            }
            set
            {
                prevhandlerName.Value = value;
                ddlHandler.ClearSelection();
                if (value.ToString() == "-1")
                {
                    ddlHandler.SelectedIndex = -1;
                }
                else
                {
                    ListItem item = ddlHandler.Items.FindByValue(value);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
            }
        }
        
        #endregion

        /// <summary>
        /// Handles the Click event of the btnOkAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOkAction_Click(object sender, EventArgs e)
        {
            this.notifyPopupExtender.Hide();
            //this.PopulatePaymentType();
        }
    }
}