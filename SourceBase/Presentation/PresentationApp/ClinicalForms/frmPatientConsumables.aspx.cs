using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.SCM;
using System.Data;
using Application.Presentation;
using AjaxControlToolkit;
using Interface.Clinical;
using System.Configuration;
using System.Web.UI.HtmlControls;
using Interface.Administration;
using Application.Common;

namespace IQCare.Web.ClinicalForms
{
    public partial class PatientConsumables : LogPage
    {

        #region "variable declaration"
        /// <summary>
        /// controls visibility 
        /// </summary>
        public string sVid = "";
        /// <summary>
        /// main dataset
        /// </summary>
        DataTable dtData = null;
        /// <summary>
        /// The session_ key
        /// </summary>
        private readonly string Session_Key = "CONSTXYW";
        /// <summary>
        /// The is error
        /// </summary>
        bool isError = false;
        /// <summary>
        /// The has cost center
        /// </summary>
        //  bool hasCostCenter = false;

        enum FormRenderMode
        {
            Clinical = 1,
            Billing = 2

        }
        #endregion
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
                //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Patient Consumables";
                //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Patient Consumables";
                (Master.FindControl("pnlExtruder") as Panel).Visible = false;
                //if (base.Session["TechnicalAreaId"] == null)
                //{
                //    (Master.FindControl("levelTwoNavigationUserControl1").FindControl("patientLevelMenu")).Visible = false;
                //    (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "________________";

                //} 
                string theUrl = "";
                if (this.Request.QueryString["mode"] == "billing")
                {
                    CurrentMode = FormRenderMode.Billing;
                    base.Session["TechnicalAreaId"] = null;
                    this.BindDropdown();
                    theUrl = string.Format("{0}?FormName={1}&mnuClicked={2}", "~/Billing/frmFindPatient.aspx", "Consumables", "Consumables");
                }
                else
                {
                    CurrentMode = FormRenderMode.Clinical;
                    theUrl = string.Format("{0}?FormName={1}&mnuClicked={2}", "..//frmFindAddPatient.aspx", "Consumables", "Consumables");
                }
                if (base.Session["PatientId"] == null || base.Session["PatientId"].ToString() == "" || base.Session["PatientId"].ToString() == "0")
                {
                    CurrentMode = FormRenderMode.Billing;
                    Response.Redirect(theUrl, true);
                }
                this.SelectedDate = this.calendarConsumables.SelectedDate = DateTime.Today;
                this.PopulateItems(DateTime.Today);
                this.SetConsumableItemTypeID();


            }
            //hasCostCenter = (base.Session["TechnicalAreaId"] != null);
            //else
            //{
            //    if ((base.Session["TechnicalAreaId"] == null) && (ddlCostCenter.SelectedIndex == -1 || ddlCostCenter.SelectedValue == ""))
            //    {

            //    }
            //}

        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.TemplateControl.Error" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnError(EventArgs e)
        {
            // base.OnError(e);
            Exception lastError = base.Server.GetLastError();
            this.showErrorMessage(ref lastError);
        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //divError.Visible = isError;
            // this.BindGrid();

            Session[this.Session_Key] = this.dtData;

            panelBilling.Visible = (this.CurrentMode == FormRenderMode.Billing);
            gridConsumables.FooterRow.Visible = (this.CurrentMode == FormRenderMode.Billing && ddlCostCenter.SelectedIndex > 0)
                || ((this.CurrentMode == FormRenderMode.Clinical) && base.Session["TechnicalAreaId"] != null);

            if ((this.CurrentMode == FormRenderMode.Billing))
            {
                this.SetStyle();
                this.PatientDetails();
            }
        }

        /// <summary>
        /// Sets the style.
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
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            calendarConsumables.PreRender += new EventHandler(calendarConsumables_PreRender);
            calendarConsumables.SelectionChanged += new EventHandler(calendarConsumables_SelectionChanged);
            calendarConsumables.VisibleMonthChanged += new MonthChangedEventHandler(calendarConsumables_VisibleMonthChanged);
            calendarConsumables.DayRender += new DayRenderEventHandler(calendarConsumables_DayRender);
            //repeaterConsumables.ItemDataBound += new RepeaterItemEventHandler(repeaterConsumables_ItemDataBound);
            //repeaterConsumables.ItemCommand += new RepeaterCommandEventHandler(repeaterConsumables_ItemCommand);

            gridConsumables.RowCommand += new GridViewCommandEventHandler(gridConsumables_RowCommand);
            gridConsumables.RowCancelingEdit += new GridViewCancelEditEventHandler(gridConsumables_RowCancelingEdit);
            gridConsumables.RowDataBound += new GridViewRowEventHandler(gridConsumables_RowDataBound);
            gridConsumables.RowDeleting += new GridViewDeleteEventHandler(gridConsumables_RowDeleting);
            gridConsumables.RowEditing += new GridViewEditEventHandler(gridConsumables_RowEditing);
            gridConsumables.RowUpdating += new GridViewUpdateEventHandler(gridConsumables_RowUpdating);


        }
        /// <summary>
        /// Handles the RowUpdating event of the gridConsumables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewUpdateEventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void gridConsumables_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label lblUpdateDate = (Label)gridConsumables.Rows[e.RowIndex].FindControl("lblEditDate");
                // TextBox txtUpdateDescription = (TextBox)gridConsumables.Rows[e.RowIndex].FindControl("txtEditDescription");
                TextBox txtUpdateQuantity = (TextBox)gridConsumables.Rows[e.RowIndex].FindControl("txtEditQuantity");
                // DropDownList ddlUpdatePaymentMode = (DropDownList)gridConsumables.Rows[e.RowIndex].FindControl("ddlEditPaymentMode");
                TextBox txtEdititemId = (TextBox)gridConsumables.Rows[e.RowIndex].FindControl("txtEdititemId");

                Label lblUpdateUnitPrice = (Label)gridConsumables.Rows[e.RowIndex].FindControl("lblEditUnitPrice");
                Label labelItemName = (Label)gridConsumables.Rows[e.RowIndex].FindControl("labelItemName");
                string itemName = labelItemName.Text;
                float sellingPrice = float.Parse(lblUpdateUnitPrice.Text);
                string billItemID = gridConsumables.DataKeys[e.RowIndex].Values["billitemid"].ToString();
                int itemID = int.Parse(gridConsumables.DataKeys[e.RowIndex].Values["item_pk"].ToString());
                //txtUpdateDescription.Text.Trim() == "" ||
                if (txtUpdateQuantity.Text.Trim() == "" || txtUpdateQuantity.Text.Trim() == "0")
                    return;
                int quantity = int.Parse(txtUpdateQuantity.Text.Trim());
                int itemType = int.Parse(gridConsumables.DataKeys[e.RowIndex].Values["ItemType"].ToString());

                #region ToReDO
                IConsumable _consumablemManager = (IConsumable)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BConsumable, BusinessProcess.Clinical");

                _consumablemManager.ItemID = itemID;
                _consumablemManager.ItemTypeID = itemType;
                _consumablemManager.SellingPrice = sellingPrice;
                _consumablemManager.IssueConsumable(itemID, itemType, itemName, sellingPrice, this.PatientID, this.LocationID, this.SelectedDate, this.UserID, quantity, this.ModuleID);
                #endregion

                gridConsumables.EditIndex = -1;
                this.PopulateItems(this.SelectedDate, true);
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Handles the RowEditing event of the gridConsumables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewEditEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void gridConsumables_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridConsumables.EditIndex = e.NewEditIndex;
        }

        /// <summary>
        /// Handles the RowDeleting event of the gridConsumables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewDeleteEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void gridConsumables_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (gridConsumables.DataKeys[e.RowIndex].Values[0].ToString() != "")
            {


                int billItemID = int.Parse(this.gridConsumables.DataKeys[e.RowIndex].Values[0].ToString());
                IConsumable _consumablemManager = (IConsumable)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BConsumable, BusinessProcess.Clinical");
                _consumablemManager.RemoveConsumable(this.UserID, billItemID);

            }
            this.PopulateItems(this.SelectedDate);
        }

        /// <summary>
        /// Handles the RowDataBound event of the gridConsumables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void gridConsumables_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                // Retrieve the serviceStatus value for the current row.
                //if service/item has been given then it becomes uneditable
                /*
                String serviceStatus = rowView["ServiceStatus"].ToString();
                if (serviceStatus == "1")
                {
                    LinkButton btnEdit = (LinkButton)e.Row.FindControl("linkEdit");
                    btnEdit.Visible = false;
                    //disable the delete button too
                    e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
                }*/
            }
        }

        /// <summary>
        /// Handles the RowCancelingEdit event of the gridConsumables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCancelEditEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void gridConsumables_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridConsumables.EditIndex = -1;
        }

        /// <summary>
        /// Handles the RowCommand event of the gridConsumables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        void gridConsumables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddItem"))
                {

                    TextBox textDescription = (TextBox)gridConsumables.FooterRow.FindControl("txtNewDescription");
                    TextBox textQuantity = (TextBox)gridConsumables.FooterRow.FindControl("txtNewQuantity");


                    Label labelUnitPrice = (Label)gridConsumables.FooterRow.FindControl("lblNewUnitPrice");
                    //TextBox txtNewitemId = (TextBox)grdCurrentBill.FooterRow.FindControl("txtNewitemId");
                    // TextBox txtNewItemType = (TextBox)grdCurrentBill.FooterRow.FindControl("txtNewItemType");
                    if (textDescription.Text.Trim() == "" || textQuantity.Text.Trim() == "" || textQuantity.Text.Trim() == "0")
                        return;
                    int quantity = int.Parse(textQuantity.Text);

                    #region ToRedo

                    IConsumable _consumablemManager = (IConsumable)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BConsumable, BusinessProcess.Clinical");
                    _consumablemManager.ItemName = textDescription.Text.Trim();
                    _consumablemManager.ItemID = this.ItemID;
                    _consumablemManager.ItemTypeID = this.ItemTypeID;

                    _consumablemManager.SellingPrice = float.Parse(labelUnitPrice.Text);
                    //string h = HSelectedDate.Value;
                    _consumablemManager.IssueConsumable(this.ItemID, this.ItemTypeID, textDescription.Text.Trim(), float.Parse(labelUnitPrice.Text), this.PatientID, this.LocationID, this.SelectedDate, this.UserID, quantity, this.ModuleID, true);

                    #endregion
                    this.PopulateItems(this.SelectedDate);
                }

            }
            catch (Exception ex)
            {
                this.isError = true;
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Handles the VisibleMonthChanged event of the calendarConsumables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MonthChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void calendarConsumables_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            try
            {

                calendarConsumables.VisibleDate = new DateTime(e.NewDate.Year, e.NewDate.Month, 1);
                DateTime calendarDate = this.calendarConsumables.SelectedDate = this.calendarConsumables.VisibleDate;
                this.SelectedDate = calendarDate;
                this.PopulateItems(calendarDate, true);
                //this.updatePanelGrid.Update();

            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Handles the PreRender event of the calendarConsumables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void calendarConsumables_PreRender(object sender, EventArgs e)
        {
            calendarConsumables.Enabled = true;
        }
        /// <summary>
        /// Handles the SelectionChanged event of the calendarConsumables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void calendarConsumables_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                IQCareMsgBox.HideMessage(this);
                this.SelectedDate = this.calendarConsumables.VisibleDate = this.calendarConsumables.SelectedDate;

                TimeSpan difference = SelectedDate - Convert.ToDateTime(Application["AppCurrentDate"]);
                double days = difference.TotalDays;

                if (days > 0 )
                {                    
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "Cannot make bills for future dates.";
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                    BindGrid();
                    return;
                }
                else
                {
                    IQCareMsgBox.HideMessage(this);
                    //load the consumables for the day
                    this.PopulateItems(this.SelectedDate, true);
                }
                // divComponent.Update();
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Handles the DayRender event of the calendarConsumables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DayRenderEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void calendarConsumables_DayRender(object sender, DayRenderEventArgs e)
        {
            e.Cell.Style.Add("cursor", "hand");
            e.Cell.Attributes.Add("onClick", e.SelectUrl);
        }
        /// <summary>
        /// Handles the TextChanged event of the textQuantity control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void textQuantity_TextChanged(object sender, EventArgs e)
        {
            int value;
            Int32.TryParse(((TextBox)sender).Text, out value);
            if (value < 1) value = 1;
        }
        /// <summary>
        /// Handles the Click event of the buttonSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void buttonSubmit_Click(object sender, EventArgs e)
        {
            //  throw new Exception("Method Not Implemented");
        }
        /// <summary>
        /// Patients the details.
        /// </summary>
        void PatientDetails()
        {
            DataTable theDT;
            /* if (base.Session["PatientInformation"] == null)
             {*/
            IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            theDT = ptnMgr.GetPatientRecord(Convert.ToInt32(Session["PatientId"]));
            ptnMgr = null;
            /*  }
              else
              {
                  theDT = (DataTable)base.Session["PatientInformation"];
              }*/
            if (theDT.Rows.Count > 0)
            {
                lblname.Text = String.Format("{0} {1} {2}", theDT.Rows[0]["Firstname"], theDT.Rows[0]["Middlename"], theDT.Rows[0]["Lastname"]);
                //   lblsex.Text = theDT.Rows[0]["sex"].ToString();
                lblsex.Text = (theDT.Rows[0]["sex"].ToString() == "16") ? "Male" : "Female";
                lbldob.Text = theDT.Rows[0]["Age"].ToString() + " years";
                lblFacilityID.Text = theDT.Rows[0]["PatientFacilityID"].ToString();
            }
        }
        /// <summary>
        /// Sets the consumable item type identifier.
        /// </summary>
        void SetConsumableItemTypeID()
        {
            IItemMaster bMgr = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
            //  IItemMaster bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling,BusinessProcess.SCM");
            Session["ConsumableTypeID"] = bMgr.GetItemTypeIDByName("Consumables").ToString();
        }
        /// <summary>
        /// Searches the consumable items.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> SearchConsumableItems(string prefixText, int count)
        {
            List<string> ar = new List<string>();

            int consumableItemTypeID = -1;
            int.TryParse(System.Web.HttpContext.Current.Session["ConsumableTypeID"].ToString(), out consumableItemTypeID);
            DateTime issueDate = DateTime.Now;
            DateTime.TryParse(System.Web.HttpContext.Current.Session["SelectedDate"].ToString(), out issueDate);
            int? SCMFlag = null;
            if (System.Web.HttpContext.Current.Session["SCMModule"] != null)
            {
                SCMFlag = 1;
            }
            IItemMaster _iMGR = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
            //DataTable dataTable = _iMGR.FindItems(prefixText, consumableItemTypeID, null, DateTime.Parse(issueDate.ToString("yyyy-MM-dd")), false);
            DataTable dataTable = _iMGR.FindItems(prefixText, consumableItemTypeID, null, DateTime.Parse(issueDate.ToString("dd-MMM-yyyy")), false, SCMFlag); //Bug ID 158...
            string custItem = string.Empty;

            foreach (DataRow theRow in dataTable.Rows)
            {
                custItem = AutoCompleteExtender.CreateAutoCompleteItem(theRow["ItemName"].ToString(), String.Format("{0};{1};{2}", theRow["ItemID"], theRow["ItemTypeID"], theRow["SellingPrice"]));

                ar.Add(custItem);
            }

            return ar;

        }

        /// <summary>
        /// Populates the items.
        /// </summary>
        void PopulateItems(DateTime issueDate, bool parForce = true)
        {
            try
            {
                TimeSpan difference = issueDate - Convert.ToDateTime(Application["AppCurrentDate"]);
                double days = difference.TotalDays;

                if (days <= 0)
                {
                    if (parForce || base.Session[this.Session_Key] == null)
                    {
                        IConsumable _consumablemManager = (IConsumable)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BConsumable, BusinessProcess.Clinical");
                        this.dtData = _consumablemManager.GetPatientConsumableByDate(this.PatientID, issueDate);
                        dtData.TableName = "Consumables";
                    }
                    else
                    {
                        this.dtData = (DataTable)Session[this.Session_Key];
                    }


                    
                }
                this.BindGrid();
            }
            catch (Exception ex)
            {
                isError = true;
                showErrorMessage(ref ex);
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
                return Convert.ToInt32(base.Session["PatientId"].ToString());
            }
        }
        /// <summary>
        /// Gets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        int ModuleID
        {
            get
            {
                if (this.CurrentMode == FormRenderMode.Billing)
                {
                    return int.Parse(ddlCostCenter.SelectedValue);
                }
                else
                {

                    return Convert.ToInt32(base.Session["TechnicalAreaId"]);
                }
            }
        }

        /// <summary>
        /// Gets or sets the form mode.
        /// </summary>
        /// <value>
        /// The form mode.
        FormRenderMode CurrentMode
        {
            get
            {
                if (this.ViewState["FormMode"] == null)
                {
                    this.ViewState["FormMode"] = FormRenderMode.Clinical;
                }
                return (FormRenderMode)this.ViewState["FormMode"];
            }
            set
            {
                ViewState["FormMode"] = value;
            }
        }
        /// <summary>
        /// Gets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        int LocationID
        {
            get
            {
                return Convert.ToInt32(base.Session["AppLocationId"].ToString());
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
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        int ItemID
        {
            get
            {
                return int.Parse(HItemID.Value.Trim());
            }
            set
            {
                HItemID.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the item type identifier.
        /// </summary>
        /// <value>
        /// The item type identifier.
        /// </value>
        int ItemTypeID
        {
            get
            {
                return int.Parse(HItemTypeID.Value.Trim());
            }
            set
            {
                HItemTypeID.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="PatientConsumables"/> is debug.
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
        /// Gets or sets the selected date.
        /// </summary>
        /// <value>
        /// The selected date.
        /// </value>
        DateTime SelectedDate
        {
            get
            {
                return Convert.ToDateTime(this.HSelectedDate.Value);
            }
            set
            {
                HSelectedDate.Value = value.ToString("dd-MMM-yyyy");
                Session["SelectedDate"] = value.ToString("dd-MMM-yyyy");
            }
        }


        /// <summary>
        /// Binds the repeater.
        /// </summary>
        void BindGrid()
        {
            if (dtData != null)
            {
                if (this.dtData.Rows.Count > 0)
                {
                    this.dtData.DefaultView.Sort = "IssueDate ASC";
                    gridConsumables.DataSource = this.dtData;
                    gridConsumables.DataBind();
                    gridConsumables.Rows[0].Visible = true;
                }
                else
                {
                    DataTable dtTemp = this.dtData.Clone();
                    dtTemp.Rows.Add(dtTemp.NewRow());
                    gridConsumables.DataSource = dtTemp;
                    gridConsumables.DataBind();
                    gridConsumables.Rows[0].Visible = false;
                }
            }
            else
            {
                gridConsumables.DataSource = null;
                gridConsumables.DataBind();
            }
            //  gridConsumables.Visible = (this.dtData.Rows.Count > 0);
        }
        void UnBindGrid()
        {
            
                DataTable dtTemp = this.dtData.Clone();
                dtTemp.Rows.Add(dtTemp.NewRow());
                gridConsumables.DataSource = dtTemp;
                gridConsumables.DataBind();
                gridConsumables.Rows[0].Visible = false;
           
            
        }
        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void showErrorMessage(ref Exception ex)
        {
            this.isError = true;
            MsgBuilder theBuilder1 = new MsgBuilder();            
            if (this.Debug)
            {
                theBuilder1.DataElements["MessageText"] = ex.Message + ex.StackTrace + ex.Source;
                //lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            }
            else
            {

                //lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team";
                //this.isError = this.divError.Visible = true;
                theBuilder1.DataElements["MessageText"] = "An error has occured within IQCARE during processing. Please contact the support team";
                Exception lastError = ex;


                lastError.Data.Add("Domain", "Patient Consumeable Issueance Form");
                //Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                //logger.LogError(ex);


            }
            //IQCareMsgBox.ShowforUpdatePanel("FillLabResults", this);
            IQCareMsgBox.Show("#C1", theBuilder1, this);

        }
        /// <summary>
        /// Binds the dropdown.
        /// </summary>
        void BindDropdown()
        {
            //if (base.Session["TechnicalAreaId"] == null)
            //{
            BindFunctions BindManager = new BindFunctions();
            IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet DSModules = ptnMgr.GetModuleNames(Convert.ToInt32(Session["AppLocationId"]));

            DataView theModDV = new DataView(DSModules.Tables[0]);
            theModDV.RowFilter = "ModuleId NOT IN(206,207)";
            IQCareUtils theModUtils = new IQCareUtils();

            DataTable theDT = new DataTable();
            theDT = theModUtils.CreateTableFromDataView(theModDV);
            //theDT = DSModules.Tables[0];

            if (theDT.Rows.Count > 0)
            {
                BindManager.BindCombo(ddlCostCenter, theDT, "ModuleName", "ModuleID");
                ptnMgr = null;
            }
            ///}
        }
        /// <summary>
        /// Handles the textChanged event of the txtEditDescription control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void txtEditDescription_textChanged(object sender, EventArgs e)
        {
            int ItemId, itemType;
            decimal itemPrice;
            if (HItemName.Value != "")
            {
                String[] itemCodes = HItemName.Value.Split(';');

                {
                    ItemId = Convert.ToInt32(itemCodes[0]);
                    itemType = Convert.ToInt32(itemCodes[1]);
                    itemPrice = Convert.ToDecimal(itemCodes[2]);
                    GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
                    Label lblNewUnitPrice = (Label)row.FindControl("lblEditUnitPrice");
                    //  Label lblNewAmountPrice = (Label)row.FindControl("lblEditAmount");
                    // TextBox txtNewQuantity = (TextBox)row.FindControl("txtEditQuantity");


                    lblNewUnitPrice.Text = itemPrice.ToString();
                    //  lblNewAmountPrice.Text =( itemPrice * int.Parse(txtNewQuantity.Text)).ToString();

                    HItemID.Value = ItemId.ToString();
                    HItemName.Value = itemType.ToString();
                    HItemTypeID.Value = itemType.ToString();

                }
            }
            else
            {
                ((TextBox)sender).Text = "";
                HItemName.Value = "";
            }
        }

        /// <summary>
        /// Handles the textChanged event of the txtNewDescription control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void txtNewDescription_textChanged(object sender, EventArgs e)
        {
            //  IQCareUtils theUtils = new IQCareUtils();
            // DataView theAutoDV;
            // DataView theExistsDV;
            // DataTable theDT = (DataTable)Session["billingInformation"];


            int ItemId, itemType;
            decimal itemPrice;
            if (HItemName.Value != "")
            {
                try
                {
                    String[] itemCodes = HItemName.Value.Split(';');
                    if (itemCodes.Length == 3)
                    {
                        ItemId = Convert.ToInt32(itemCodes[0]);
                        itemType = Convert.ToInt32(itemCodes[1]);
                        //////IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling,BusinessProcess.SCM");//Marked by Jayant for SCM Price changes
                        //////itemPrice = bMgr.GetItemPrice(ItemId, itemType, DateTime.Parse(this.SelectedDate.ToString("dd-MMM-yyyy"))); // Bug ID 158.....
                        //if (itemPrice == 0.00M)
                        //{
                        //    ((TextBox)sender).Text = "";
                        //    HItemName.Value = "";
                        //}
                        itemPrice = Convert.ToDecimal(itemCodes[2]);
                        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
                        Label lblNewUnitPrice = (Label)row.FindControl("lblNewUnitPrice");
                        Label lblNewAmountPrice = (Label)row.FindControl("lblNewAmountPrice");
                        TextBox txtNewQuantity = (TextBox)row.FindControl("txtNewQuantity");

                        // TextBox txtnewItemId = (TextBox)row.FindControl("txtNewitemId");
                        // TextBox txtNewItemType = (TextBox)row.FindControl("txtNewItemType");

                        //txtnewItemId.Text = ItemId.ToString();
                        //txtNewItemType.Text = itemType.ToString();
                        lblNewUnitPrice.Text = itemPrice.ToString();
                        //  lblNewAmountPrice.Text =( itemPrice * int.Parse(txtNewQuantity.Text)).ToString();

                        HItemID.Value = ItemId.ToString();
                        HItemName.Value = itemType.ToString();
                        HItemTypeID.Value = itemType.ToString();

                    }
                }
                catch (Exception ex)
                {
                    showErrorMessage(ref ex);
                    ((TextBox)sender).Text = "";
                    HItemName.Value = "";
                }
            }
            else
            {
                ((TextBox)sender).Text = "";
                HItemName.Value = "";
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the txtNewQuantity control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void txtNewQuantity_TextChanged(object sender, EventArgs e)
        {
            int value=1;
            Int32.TryParse(((TextBox)sender).Text, out value);
            if (value < 1) value = 1;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlCostCenter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlCostCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gridConsumables.FooterRow.Visible = hasCostCenter || ddlCostCenter.SelectedIndex > 0;
        }

        /// <summary>
        /// Handles the Click event of the btnFind control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnFind_Click(object sender, EventArgs e)
        {
            base.Session.Remove("PatientId");
            string theUrl;
            if (this.CurrentMode == FormRenderMode.Clinical)
            {
                theUrl = string.Format("{0}?FormName={1}&mnuClicked={2}&mode=clinical", "..//frmFindAddPatient.aspx", "Consumables", "Consumables");
            }
            else
            {
                theUrl = string.Format("{0}?FormName={1}mnuClicked={2}&mode=billing", "~/ClinicalForms/frmPatientConsumables.aspx", "Consumables", "Consumables");
            }
            Response.Redirect(theUrl, false);
        }



    }
}
