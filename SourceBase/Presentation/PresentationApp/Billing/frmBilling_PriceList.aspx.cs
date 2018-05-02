using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using System.Data;
using System.Configuration;
using Interface.Administration;
using Application.Presentation;
using Interface.SCM;
using Entities.Billing;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Collections;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Hosting;

namespace IQCare.Web.Billing
{
    public partial class frmBilling_PriceList : LogPage
    {
        bool isError = false;

        /// <summary>
        /// The items per page
        /// </summary>
        int ItemsPerPage = 100;

        /// <summary>
        /// The authentication
        /// </summary>
        AuthenticationManager Authentication = new AuthenticationManager();
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
                return Convert.ToInt32(Session["AppLocationId"]);
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance can change price.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can change price; otherwise, <c>false</c>.
        /// </value>
        bool CanChangePrice
        {
            get
            {

                return HPerm.Value.Equals("true");
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
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>
        /// The index of the page.
        /// </value>
        int PageIndex
        {
            get
            {
                int index = 1;
                int.TryParse(HPageIndex.Value, out index);
                return index;
            }
            set
            {
                this.HPageIndex.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the type of the selected item.
        /// </summary>
        /// <value>
        /// The type of the selected item.
        /// </value>
        int SelectedItemType
        {
            get
            {
                int item = 0;
                int.TryParse(HItemType.Value, out item);
                return item;

            }
            set
            {
                this.HItemType.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        string SearchText
        {
            get
            {
                return this.HSearchText.Value;
            }
            set
            {
                this.HSearchText.Value = value;
            }
        }
        /// <summary>
        /// Gets or sets the page count.
        /// </summary>
        /// <value>
        /// The page count.
        /// </value>
        int PageCount
        {
            get
            {
                int pages = 1;
                int.TryParse(HPages.Value, out pages);
                return pages;
            }
            set
            {
                this.HPages.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [show priced only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show priced only]; otherwise, <c>false</c>.
        /// </value>
        bool ShowPricedOnly
        {
            get
            {
                bool flag = false;
                bool.TryParse(this.HPriced.Value.ToLower(), out flag);
                return flag;
            }
            set
            {
                this.HPriced.Value = value ? "true" : "false";
            }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="frmAdmissionHome"/> is debug.
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
        /// <summary>
        /// Gets or sets the show in edit.
        /// </summary>
        /// <value>
        /// The show in edit.
        /// </value>
        protected string ShowInEdit()
        {
            return this.CanChangePrice ? "" : "none";
        }
        /// <summary>
        /// Hides the edit.
        /// </summary>
        /// <returns></returns>
        protected string HideEdit()
        {
            return this.CanChangePrice ? "none" : "";
        }
        /// <summary>
        /// Injects the script.
        /// </summary>
        void InjectScript()
        {
            //calendarButtonExtender.E = DateTime.Now;
            string ClientIDType = ddlItemType.ClientID;
            //  string pageID = this.UniqueID;
            string scriptItem = @"function beforePostBack(){var val = $('#" + ClientIDType + @"').val();  if(val=='-1' ){return false;}return true; };";


            string scriptPastDates = @" function disable_past_dates(sender,args){
                                                    var senderDate = new Date(sender._selectedDate);senderDate.setHours(0,0,0,0)
                                                    var nowDate =new Date();  nowDate.setHours(0,0,0,0);
                                                    if(senderDate < nowDate){
                                                        NotifyMessage('You cannot select a day before today'); 
                                                        sender._selectedDate=new Date();sender._textbox.set_Value(sender._selectedDate.format(sender._format));    }}";

            btnFilter.OnClientClick = "javascript:return beforePostBack();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "_pastdates", scriptPastDates, true);
            ScriptManager.RegisterStartupScript(btnFilter, btnFilter.GetType(), "_NoPostBackInVain", scriptItem, true);

        }
        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void showErrorMessage(ref Exception ex)
        {
            this.isError = true;
            //if (this.Debug)
            //{
            //    lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            //}
            //else
            //{
            //    lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team.  ";
            //    this.isError = this.divError.Visible = true;
            //    Exception lastError = ex;
            //    lastError.Data.Add("Domain", "Price List");
            //    try
            //    {
            //        //Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
            //        //logger.LogError(ex);
            //    }
            //    catch
            //    {

            //    }
            //}
            //notifyPopupExtender.Hide();
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
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        /// <param name="onOkScript">The on ok script.</param>
        void NotifyAction(string strMessage, string strTitle, bool errorFlag, string onOkScript = "")
        {

            lblNoticeInfo.Text = strMessage;
            lblNotice.Text = strTitle;
            lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.Black;
            lblNoticeInfo.Font.Bold = true;
            btnOkAction.OnClientClick = "";
            //var list = new List<KeyValuePair<string, string>>();
            //list.Add(new KeyValuePair<string, string>("Message", strMessage));
            //list.Add(new KeyValuePair<string, string>("Title", strTitle));
            //list.Add(new KeyValuePair<string, string>("errorFlag", errorFlag.ToString().ToLower()));
            if (onOkScript != "" && errorFlag == true)
            {
                //list.Add(new KeyValuePair<string, string>("OkScript", onOkScript));
                btnOkAction.OnClientClick = onOkScript;
            }
            this.notifyPopupExtender.Show();

        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.HPerm.Value = (Authentication.HasFeatureRight(ApplicationAccess.BillingConfiguration, (DataTable)Session["UserRight"]) == true).ToString().ToLower();
            if (!this.IsPostBack)
            {
                this.PopulateItemType();
                textPriceListDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            this.InjectScript();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Master.PageScriptManager.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(PageScriptManager_AsyncPostBackError);
        }

        /// <summary>
        /// Handles the AsyncPostBackError event of the PageScriptManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AsyncPostBackErrorEventArgs"/> instance containing the event data.</param>
        void PageScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            string message = e.Exception.Message;
            Master.PageScriptManager.AsyncPostBackErrorMessage = message;
        }
        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            btnClose.OnClientClick = "javascript:window.location.href='../frmFacilityHome.aspx'; return false;";
            divError.Visible = this.isError;

        }
        /// <summary>
        /// Handles the Click event of the btnFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.btnFilter.Focus();
            if (ddlItemType.SelectedIndex < 1) return;

            this.SelectedItemType = int.Parse(ddlItemType.SelectedValue);
            this.SearchText = textSearchText.Text;
            this.ShowPricedOnly = rblOption.SelectedValue == "Yes";
            this.PageIndex = 1;
            this.PopulatePriceList();

        }

        /// <summary>
        /// Items the type changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ItemTypeChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btnOkAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOkAction_Click(object sender, EventArgs e)
        {
            divGridComponent.Update();
        }

        /// <summary>
        /// Handles the RowCommand event of the gridPriceList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridPriceList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        /// <summary>
        /// Handles the RowDataBound event of the gridPriceList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridPriceList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                AjaxControlToolkit.CalendarExtender control = e.Row.FindControl("calendarButtonExtender") as AjaxControlToolkit.CalendarExtender;
                //if (control != null) AjaxControlToolkit.ScriptObjectBuilder.RegisterCssReferences(control);

                SaleItem rowView = (SaleItem)e.Row.DataItem;
                /*  if (rowView.SellingPrice.HasValue)
                  {
                      Label lblPrice = e.Row.FindControl("labelPrice") as Label;
                      TextBox txtPrice = e.Row.FindControl("textPrice") as TextBox;
                      lblPrice.Text = txtPrice.Text = rowView.SellingPrice.Value.ToString("{0:N}");
                  }
                  if (rowView.PriceDate.HasValue)
                  {
                      Label lblPriceDate = e.Row.FindControl("labelPriceDate") as Label;
                      TextBox txtPriceDate = e.Row.FindControl("textPriceDate") as TextBox;
                      lblPriceDate.Text = txtPriceDate.Text = rowView.PriceDate.Value.ToString("dd-MMM-yyyy");
                  }
                  if (rowView.VersionStamp.HasValue)
                  {
                      HiddenField hStamp = e.Row.FindControl("hdVersionStamp") as HiddenField;
                      hStamp.Value = rowView.VersionStamp.Value.ToString();
                  }

                  */

                string str = "Item";
                if (!rowView.PricedPerItem.Value) str = "Dose";
                Label lbl = e.Row.FindControl("labelPriceType") as Label;
                TextBox txtPrice = e.Row.FindControl("textPrice") as TextBox;
                TextBox txtPriceIns = e.Row.FindControl("textInsPrice") as TextBox;

                if (lbl != null) lbl.Text = str;
                DropDownList ddl = e.Row.FindControl("ddlPriceType") as DropDownList;
                if (ddl != null) ddl.SelectedValue = str;
                CheckBox chk = e.Row.FindControl("chkDelete") as CheckBox;
                if (chk != null)
                {
                    chk.Checked = !rowView.Active;
                    chk.Enabled = (rowView.SellingPrice.HasValue);
                }
                string priceChange = @"$find('" + control.ClientID + "')._textbox.set_Value('" + DateTime.Now.ToString("dd-MMM-yyyy") + "');";
                txtPrice.Attributes.Add("onChange", priceChange);
                ddl.Attributes.Add("onChange", priceChange);
                txtPriceIns.Attributes.Add("onChange", priceChange);
            }
        }

        /// <summary>
        /// Handles the RowCreated event of the gridPriceList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridPriceList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            gridPriceList.Columns[3].Visible = this.ddlItemType.SelectedItem.Text == "Pharmaceuticals";
        }

        /// <summary>
        /// Handles the Click event of the buttonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                int itemTypeID = this.SelectedItemType;
                // Get the data from the db for conflict checking
                Pager page = new Pager() { PageCount = this.ItemsPerPage, PageIndex = this.PageIndex };
                ResultSet<SaleItem> resultSet = this.GetPriceList(this.SelectedItemType, this.SearchText, this.ShowPricedOnly, page);

                List<SaleItem> currentSet = resultSet.Items;
                List<SaleItem> submittedSet = new List<SaleItem>();
                foreach (GridViewRow gridRow in gridPriceList.Rows)
                {
                    //ItemID,ItemTypeID,VersionStamp

                    int itemId = Convert.ToInt32(gridPriceList.DataKeys[gridRow.RowIndex].Values["ItemID"].ToString());
                    int itemTypeId = Convert.ToInt32(gridPriceList.DataKeys[gridRow.RowIndex].Values["ItemTypeID"].ToString());
                    UInt64? _version = null;
                    string vst = "";

                    if (gridPriceList.DataKeys[gridRow.RowIndex].Values["VersionStamp"] != null)
                        vst = (gridPriceList.DataKeys[gridRow.RowIndex].Values["VersionStamp"].ToString());
                    //else
                    //{
                    //    this.NotifyAction("Please enter a valid date.", "Price Configuration", false);
                    //    return;
                    //}


                    if (vst != "")
                        _version = Convert.ToUInt64(vst);

                    TextBox txtPrice = gridRow.FindControl("textPrice") as TextBox;
                    if (txtPrice.Text.Trim() == "") continue;

                    TextBox txtPriceIns = gridRow.FindControl("textInsPrice") as TextBox;
                    if (txtPriceIns.Text.Trim() == "") continue;

                    Label lblPrice = gridRow.FindControl("labelPrice") as Label;
                    TextBox txtPriceDate = gridRow.FindControl("textPriceDate") as TextBox;
                    Label lblPriceIns = gridRow.FindControl("labelInsPrice") as Label;

                    Label lblPriceDate = gridRow.FindControl("labelPriceDate") as Label;
                    Label lblPriceType = gridRow.FindControl("labelPriceType") as Label;

                    DropDownList ddlPriceMode = null;
                    bool pricePerItem = true;
                    bool priceModeChanged = false;
                    if (this.ddlItemType.SelectedItem.Text == "Pharmaceuticals")
                    {
                        ddlPriceMode = gridRow.FindControl("ddlPriceType") as DropDownList;
                        pricePerItem = ddlPriceMode.SelectedValue == "Item";
                        priceModeChanged = ddlPriceMode.SelectedValue != lblPriceType.Text;
                    }
                    CheckBox chk = gridRow.FindControl("chkDelete") as CheckBox;
                    bool active = true;
                    if (chk != null)
                    {
                        if (chk.Enabled)
                            active = !chk.Checked;
                    }

                    Decimal? newPrice;
                    newPrice = Decimal.Parse(txtPrice.Text);

                    Decimal? newpriceIns;
                    newpriceIns = Decimal.Parse(txtPriceIns.Text);

                    DateTime dtDateTime;
                    string tmp = txtPriceDate.Text.ToString();

                    if (!DateTime.TryParse(tmp, out dtDateTime))
                        tmp = tmp.Replace('_', ' ').Replace('-', ' ').Trim();


                    DateTime? newPriceDate;

                    newPriceDate = string.IsNullOrEmpty(tmp.ToString()) ? DateTime.Now : DateTime.Parse(tmp);

                    bool hasChanged = false;
                    bool priceChanged = txtPrice.Text != lblPrice.Text;
                    bool InsurancePriceChanged = txtPriceIns.Text != lblPriceIns.Text;
                    bool priceDateChanged = lblPriceDate.Text != txtPriceDate.Text;

                    if ((priceChanged || priceModeChanged || InsurancePriceChanged) && !priceDateChanged)
                    {
                        hasChanged = false;
                    }
                    else if ((priceChanged || priceModeChanged || InsurancePriceChanged) && priceDateChanged)
                    {
                        hasChanged = true;
                    }
                    if (hasChanged)
                    {
                        submittedSet.Add(new SaleItem
                        {
                            ItemID = itemId,
                            ItemTypeID = itemTypeId,
                            VersionStamp = _version,
                            PriceDate = newPriceDate,
                            SellingPrice = newPrice,
                            SellingPriceIns = newpriceIns,
                            PricedPerItem = pricePerItem,
                            Active = active
                        });
                    }
                    // var _it = currentSet
                    //    .Where(fr => fr.ItemID == itemId && fr.ItemTypeID == itemTypeId ).FirstOrDefault();


                }
                //get only those items that has  been updated
                var changedSet = (from cs in currentSet
                                  join ss in submittedSet on cs.ItemID equals ss.ItemID
                                  where cs.ItemTypeID == ss.ItemTypeID && cs.VersionStamp == ss.VersionStamp
                                  &&
                                  (cs.SellingPrice != ss.SellingPrice || cs.PriceDate != ss.PriceDate || cs.PricedPerItem != ss.PricedPerItem || cs.Active != ss.Active || cs.SellingPriceIns != ss.SellingPriceIns)
                                  select new SaleItem
                                  {
                                      ItemID = cs.ItemID,
                                      ItemTypeID = cs.ItemTypeID,
                                      PriceDate = ss.PriceDate,
                                      PricedPerItem = ss.PricedPerItem,
                                      SellingPrice = ss.SellingPrice,
                                      SellingPriceIns = ss.SellingPriceIns,
                                      VersionStamp = cs.VersionStamp,
                                      Active = ss.Active
                                  }
                     ).ToList<SaleItem>();
                int itemCount = changedSet.Count;
                string resultMessage = "No items were updated";
                if (itemCount > 0)
                {
                    int result = BillingManager.SavePriceList(changedSet, this.UserID);
                    if (result == itemCount) resultMessage = "All items have been saved successfully";
                    else if (result == 0) resultMessage = "No items were updated";
                    else if (result < itemCount && result > 0) resultMessage = "Some items were not saved";

                }
                this.PopulatePriceList();
                this.NotifyAction(resultMessage, "Price Configuration", false);
                return;
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonCancel_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the Changed event of the PageSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.PageIndex = pageIndex;
            this.PopulatePriceList();
        }
        /// <summary>
        /// Populates the type of the item.
        /// </summary>
        void PopulateItemType()
        {
            try
            {
                IItemMaster objProgramlist = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
                DataTable theDT = objProgramlist.GetItemTypes;
                DataView theDV = new DataView(theDT);
                theDV.RowFilter = "DeleteFlag= 0 And (ItemName = 'Billables' OR ItemName = 'Consumables' OR ItemName = 'Pharmaceuticals' OR ItemName = 'Lab Tests' OR ItemName = 'Visit Type' OR ItemName= 'Ward Admission' ) ";
                //theDV.RowFilter = "DeleteFlag= 0 And (ItemName = 'Billables' OR ItemName = 'Lab Tests' OR ItemName = 'Visit Type' OR ItemName= 'Ward Admission' ) ";
                theDV.Sort = "ItemName Asc";
                ddlItemType.DataTextField = "ItemName";
                ddlItemType.DataValueField = "ItemTypeID";
                ddlItemType.DataSource = theDV.ToTable();
                ddlItemType.DataBind();
                ddlItemType.Items.Insert(0, new ListItem("Select", "-1"));
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Gets the price list.
        /// </summary>
        /// <param name="itemTypeID">The item type identifier.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="WithPriceOnly">if set to <c>true</c> [with price only].</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        ResultSet<SaleItem> GetPriceList(int itemTypeID, string searchText, bool _withPriceOnly, Pager page)
        {
            ResultSet<SaleItem> resultSet = BillingManager.GetPriceList(itemTypeID, null, searchText, _withPriceOnly, page);

            return resultSet;
        }
        /// <summary>
        /// Populates the price list.
        /// </summary>
        void PopulatePriceList()
        {
            // List<SaleItem> GetPriceList(int ItemTypeID, string ItemName="", bool WithPriceOnly=false);
            if (ddlItemType.SelectedIndex < 1) return;
            try
            {
                int itemTypeID = this.SelectedItemType;
                Pager page = new Pager() { PageCount = this.ItemsPerPage, PageIndex = this.PageIndex };
                ResultSet<SaleItem> resultSet = this.GetPriceList(itemTypeID, this.SearchText, this.ShowPricedOnly, page);
                List<SaleItem> items = resultSet.Items;
                gridPriceList.DataSource = items;
                gridPriceList.DataBind();

                this.PageCount = resultSet.Count;
                this.PopulatePager(this.PageIndex);
                labelNote.Text = String.Format("Items Available: Only a maximum of {0} records can be displayed.", this.ItemsPerPage);
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Populates the pager.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        /// <param name="currentPage">The current page.</param>
        private void PopulatePager(int currentPage)
        {

            int pageCount = (int)this.PageCount;
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                pages.Add(new ListItem("First", "1", currentPage > 1));
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
       
        /// <summary>
        /// Handles the Click event of the btnPrint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
              XmlDocument xmlDoc = new XmlDocument();
        /// <summary>
        /// The XSL document
        /// </summary>
          XmlDocument xslDoc;
            try
            {
                if (Authentication.HasFeatureRight(ApplicationAccess.BillingConfiguration, (DataTable)Session["UserRight"]) == true)
                {

                    xmlDoc = new XmlDocument();
                    string strfile = Server.MapPath("~/Billing/PriceList_1_0.xsl");

                    byte[] fileData = File.ReadAllBytes(strfile);

                    string strOut_XSL;                    

                    strOut_XSL = XmlEncodingBOM.GetBOMString(fileData);

                    xslDoc = new XmlDocument();

                    xslDoc.LoadXml(strOut_XSL);

                    // xmlDoc.LoadXml(docX.ToString());
                    DateTime? printPriceDate;

                    printPriceDate = textPriceListDate.Text.Trim() == "" ? DateTime.Now : DateTime.Parse(textPriceListDate.Text);
                    xmlDoc.LoadXml(BillingManager.GetPriceListXML(Session["AppLocation"].ToString(), Session["AppUserName"].ToString(),printPriceDate));
                    //Create an XML declaration. 
                    XmlDeclaration xmldecl;
                    xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    //Add the new node to the document.
                    XmlElement root = xmlDoc.DocumentElement;
                    xmlDoc.InsertBefore(xmldecl, root);

                    Hashtable ht =  new Hashtable() { {"data", xmlDoc.InnerXml}, {"style", strOut_XSL} };

                    Session["ReportData"] = ht;
                    string strScript = @"<script language=""javascript"" type=""text/javascript"">      
                        var w = screen.width - 60; var h = screen.height - 60; var winprops = ""location=no,scrollbars=yes,resizable=yes,status=no"";
                        var frmwin = window.open(""PrintPriceList.aspx?print=true"", ""PrintPriceList"", winprops);
                        if (parseInt(navigator.appVersion) >= 4) {
                            frmwin.window.focus();
                        }   </script>";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintReport", strScript, false);
                   // Page.ClientScript.RegisterClientScriptBlock(this.btnPrint.GetType(), "PrintReport", strScript, false);

                    //XmlNamespaceManager nsmgr = new XmlNamespaceManager(xslDoc.NameTable);
                    //nsmgr.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");

                    //XPathNavigator xslNav = xslDoc.CreateNavigator();

                    //XslCompiledTransform trans = new XslCompiledTransform();

                    //Response.Clear();
                    //Response.ContentType = "text/html; charset=UTF-8";

                    //XsltSettings xslSessings = new XsltSettings();
                    //xslSessings.EnableScript = true;

                    //trans.Load(xslNav, xslSessings, new XmlUrlResolver());

                    //trans.Transform(xmlDoc.CreateNavigator(), null, Response.OutputStream);

                    //Response.Flush();
                }
                else
                {
                    Response.Redirect("~/frmLogin.aspx?error=true");
                }
            }
            catch (Exception ex)
            {
                showErrorMessage(ref ex);
            }
        }

       
    }
}
