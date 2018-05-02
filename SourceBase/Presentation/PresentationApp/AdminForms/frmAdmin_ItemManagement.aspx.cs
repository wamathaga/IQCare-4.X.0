using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Interface.Administration;
using Application.Common;
using Application.Presentation;
using System.Collections.Generic;
using Interface.SCM;
using AjaxControlToolkit;
using System.Web.Script.Serialization;

namespace PresentationApp.AdminForms
{
    public partial class frmAdmin_ItemManagement : LogPage
    {
        #region "Variables"
        DataSet theMasterDS = new DataSet();
        DataTable theGenericTable = new DataTable();
        #endregion

        protected string ShowHideDetailColumn(object hasDetails)
        {
            bool has = hasDetails.ToString().ToLower().Trim() == "true";
            return has ? "" : "none";
        }
        protected void Page_Init(object Sender, EventArgs e)
        {
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx", true);
            }
            Init_Page();
           
        }
        private void Init_Page()
        {
            if (Page.IsPostBack != true)
            {
                GetMasters();                
               
            }
        }
        ArrayList arraylist1 = new ArrayList();
        ArrayList arraylist2 = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.GetPostBackEventReference(this, string.Empty);
            
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx", true);
            }
            
            BindFunctions BindManager = new BindFunctions();
            if (!IsPostBack)
            {
                Session["PatientId"] = 0;
                this.PopulateItemType();
                AddAttribute();
                ViewState["GenericMaster"] = theMasterDS.Tables[1];
                ViewState["StrengthMaster"] = theMasterDS.Tables[2];
                ViewState["ExistGenericDT"] = theGenericTable;
                ddlVolumeUnit.Enabled = false;
                
            }
            else
            {
                if (ViewState["SelGeneric"] != null)
                {
                    BindManager.BindList(lstGeneric, (DataTable)ViewState["SelGeneric"], "Name", "Id");
                }
                if (ViewState["SelStrength"] != null)
                {
                    BindManager.BindList(lstStrength, (DataTable)ViewState["SelStrength"], "Name", "Id");
                } 
            }
        }
        private void AddAttribute()
        {
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Administration >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Item Management";
            IQWebUtils Utils = new IQWebUtils();
            Utils.HidePatientLevelheader();
            txtMornDose.Attributes.Add("onkeyup", "chkDecimal('" + txtMornDose.ClientID + "');");
            txtMidDayDose.Attributes.Add("onkeyup", "chkDecimal('" + txtMidDayDose.ClientID + "');");
            txtEvenDose.Attributes.Add("onkeyup", "chkDecimal('" + txtEvenDose.ClientID + "');");
            txtNightDose.Attributes.Add("onkeyup", "chkDecimal('" + txtNightDose.ClientID + "');");
            txtpurchaseqty.Attributes.Add("onkeyup", "chkDecimal('" + txtpurchaseqty.ClientID + "');");
            txtPurUnitPrice.Attributes.Add("onkeyup", "chkDecimal('" + txtPurUnitPrice.ClientID + "');");
            txtDispMargin.Attributes.Add("onkeyup", "chkDecimal('" + txtDispMargin.ClientID + "');");
            txtDispUnitPrice.Attributes.Add("onkeyup", "chkDecimal('" + txtDispUnitPrice.ClientID + "');");
            txtDispMargin.Attributes.Add("onblur", "calculateselling();");
            txtDispUnitPrice.Attributes.Add("onblur", "calculateselling();");
            
           
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> SearchItems(string prefixText)
        {
            List<string> Itemsdetail = new List<string>();
            List<Items> lstItemsDetail = GetItems(prefixText);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            foreach (Items c in lstItemsDetail)
            {
                Itemsdetail.Add(AutoCompleteExtender.CreateAutoCompleteItem(c.ItemName, serializer.Serialize(c)));
            }

            return Itemsdetail;

        }

        public static List<Items> GetItems(string prefixText)
        {
            List<string> ar = new List<string>();
            IItemMaster _iMGR = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
            //filling data from database
            int? consumableItemTypeID = null;
            if (System.Web.HttpContext.Current.Session["ConsumableTypeID"] != null)
            {
                consumableItemTypeID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ConsumableTypeID"].ToString());
            }
            // int.TryParse(System.Web.HttpContext.Current.Session["ConsumableTypeID"].ToString(), out consumableItemTypeID);
            DataTable dataTable = _iMGR.FindItems(prefixText, null, null, DateTime.Now, false);
            List<Items> custItem = new List<Items>();
            //string[] custItem = new string[dataTable.Rows.Count+1];
            int i=0;
            foreach (DataRow theRow in dataTable.Rows)//.Select("ItemTypeName <> 'Consumables'"))
            {
                
                Items item = new Items();                
                item.ItemId = (int)theRow["ItemID"];                
                item.ItemName = (string)theRow["ItemName"];
                custItem.Add(item);                
                
            }
            return custItem;
        }

        [System.Web.Services.WebMethod]
        public static string SendParameters(string name)
        {
            return string.Format("Name: {0}", name);
        }
        #region "Item Configuration"

        private void PopulateItemType()
        {
            IMasterList objItemCommonlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            String TableName = "Mst_Decode";
            DataTable DTItemlist = new DataTable();
            DTItemlist = objItemCommonlist.GetItemTypeList();
            BindFunctions BindManager = new BindFunctions();
            DataSet theDS = objItemCommonlist.GetItemDetails(Convert.ToInt32(0));
            if (DTItemlist.Rows.Count > 0)
            {
                BindManager.BindCombo((DropDownList)ddlItemType, DTItemlist, "Name", "ID");
            }
            

            BindManager.BindCombo(ddlpurchaseunit, theDS.Tables[3].Copy(), "Name", "Id");
            BindManager.BindCombo(ddlmanufaturer, theDS.Tables[4], "Name", "Id");
            BindManager.BindCombo(ddldispensingunit, theDS.Tables[3].Copy(), "Name", "Id");
            BindManager.BindCombo(ddlVolumeUnit, theDS.Tables[6].Copy(), "Name", "Id");         
                
        }

        [System.Web.Services.WebMethod]
        public static ArrayList PopulateItemSubType(int ItemTypeId)
        {
            ArrayList list = new ArrayList();
            IMasterList objItemCommonlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataTable DTSubItemlist = new DataTable();
            DTSubItemlist = objItemCommonlist.GetFilteredSubItemType(ItemTypeId);
            //DTSubItemlist.Columns[0].ColumnName = "SubItemTypeId";
            //DTSubItemlist.Columns[1].ColumnName = "SubTypeName";
            DTSubItemlist.AcceptChanges();
            foreach (DataRow sdr in DTSubItemlist.Rows)
            {
                list.Add(new ListItem(
               sdr["SubTypeName"].ToString(),
               sdr["SubItemTypeId"].ToString()
                ));
            }
                    
          return list;
                
        }
        public DataTable PopulateItemSubTypeDT(int ItemTypeId)
        {           
            IMasterList objItemCommonlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataTable DTSubItemlist = new DataTable();
            DTSubItemlist = objItemCommonlist.GetFilteredSubItemType(ItemTypeId);
            //DTSubItemlist.Columns[0].ColumnName = "SubItemTypeId";
            //DTSubItemlist.Columns[1].ColumnName = "SubTypeName";
            DTSubItemlist.AcceptChanges();

            return DTSubItemlist;

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (ddlItemType.SelectedIndex > 0)
            {
                BindFunctions BindManager = new BindFunctions();
                DataTable theSubDT = PopulateItemSubTypeDT(Convert.ToInt32(ddlItemType.SelectedValue));
                BindManager.BindCombo(ddlItemSubType, theSubDT, "SubTypeName", "SubItemTypeId");
                //ScriptManager.RegisterStartupScript(this, GetType(), "PopulateItemType", "PopulateContinents('" + theDS.Tables[0].Rows[0]["DrugSubTypeId"].ToString() + "');", true);
                if (hdsubtype.Value == "" || hdsubtype.Value == null)
                    ddlItemSubType.SelectedValue = "0";
                else
                    ddlItemSubType.SelectedValue = hdsubtype.Value.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "EnableDisablePharma", "EnableDisablePharma('" + Convert.ToInt32(ddlItemType.SelectedValue) + "');", true);
                //if (hdCustID.Value == string.Empty || hdCustID.Value == null)
                //{                    
                //    ScriptManager.RegisterStartupScript(this, GetType(), "EnableDisablePharma", "EnableDisablePharma('" + Convert.ToInt32(ddlItemType.SelectedValue) + "');", true);
                //}
            }
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (lstAvailable.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstAvailable.Items.Count; i++)
                {
                    if (lstAvailable.Items[i].Selected)
                    {
                        if (!arraylist1.Contains(lstAvailable.Items[i]))
                        {
                            arraylist1.Add(lstAvailable.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < arraylist1.Count; i++)
                {
                    if (!lstSelected.Items.Contains(((ListItem)arraylist1[i])))
                    {
                        lstSelected.Items.Add(((ListItem)arraylist1[i]));
                    }
                    lstAvailable.Items.Remove(((ListItem)arraylist1[i]));
                }
                lstSelected.SelectedIndex = -1;
            }
            else
            {
                //lbltxt.Visible = true;
                //lbltxt.Text = "Please select atleast one in Listbox1 to move";
            }
            mp1.Show();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSelected.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstSelected.Items.Count; i++)
                {
                    if (lstSelected.Items[i].Selected)
                    {
                        if (!arraylist2.Contains(lstSelected.Items[i]))
                        {
                            arraylist2.Add(lstSelected.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < arraylist2.Count; i++)
                {
                    if (!lstAvailable.Items.Contains(((ListItem)arraylist2[i])))
                    {
                        lstAvailable.Items.Add(((ListItem)arraylist2[i]));
                    }
                    lstSelected.Items.Remove(((ListItem)arraylist2[i]));
                }
                lstAvailable.SelectedIndex = -1;
            }
            else
            {
                //lbltxt.Visible = true;
                //lbltxt.Text = "Please select atleast one in Listbox2 to move";
            }
            mp1.Show();
        }
        protected void txtautoItemName_TextChanged(object sender, EventArgs e)
        {

            try
            {
                int DrugId;
                if (hdCustID.Value != "")
                {
                    if ((Convert.ToInt32(hdCustID.Value) != 0))
                    {
                        DrugId = Convert.ToInt32(hdCustID.Value);
                        Bind_Controls(DrugId);
                        txtsearchitem.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
        }

        #region "Class for Item"
        public new class Items
        {
            protected int _ItemId;
            public int ItemId
            {
                get { return _ItemId; }
                set { _ItemId = value; }
            }

            protected int _avlqty;
            public int AvlQty
            {
                get { return _avlqty; }
                set { _avlqty = value; }
            }

            protected string _itemName;
            public string ItemName
            {
                get { return _itemName; }
                set { _itemName = value; }
            }


        }
        #endregion

        #endregion
        private DataTable MakeSelectedGenericTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("Drug_pk", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("ID", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Name", System.Type.GetType("System.String"));
            theDT.Columns.Add("Abbrevation", System.Type.GetType("System.String"));
            return theDT;
        }
        private void Bind_Controls(int ItemId)
        {
            BindFunctions BindManager = new BindFunctions();
            IMasterList theMasterList = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList, BusinessProcess.SCM");
            DataSet theDS = theMasterList.GetItemDetails(ItemId);
            
            
            if (theDS.Tables[0].Rows.Count > 0)
            {                
                if (theDS.Tables[0].Rows[0].IsNull("FDACode") == true)
                    txtItemCode.Text = "";
                else
                    txtItemCode.Text = theDS.Tables[0].Rows[0]["FDACode"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("ItemTypeID") == true)
                    ddlItemType.SelectedValue = "0";
                else
                    ddlItemType.SelectedValue = theDS.Tables[0].Rows[0]["ItemTypeID"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("DrugSubTypeId") != true)
                    hdsubtype.Value = theDS.Tables[0].Rows[0]["DrugSubTypeId"].ToString();
                
                DataTable theSubDT = PopulateItemSubTypeDT(Convert.ToInt32(ddlItemType.SelectedValue));
                BindManager.BindCombo(ddlItemSubType, theSubDT, "SubTypeName", "SubItemTypeID");
                ddlItemSubType.Attributes.Add("onchange", "ItemSubTypeChange('" + ddlItemSubType.ClientID + "');");
                //ScriptManager.RegisterStartupScript(this, GetType(), "PopulateItemType", "PopulateContinents('" + theDS.Tables[0].Rows[0]["DrugSubTypeId"].ToString() + "');", true);
                if (theDS.Tables[0].Rows[0].IsNull("DrugSubTypeId") == true)
                    ddlItemSubType.SelectedValue = "0";
                else
                {
                    ddlItemSubType.SelectedValue = theDS.Tables[0].Rows[0]["DrugSubTypeId"].ToString();
                    hdsubtype.Value = theDS.Tables[0].Rows[0]["DrugSubTypeId"].ToString();
                }


                
                //$('#mycontrolId').val(myvalue).attr("selected", "selected");
                if (theDS.Tables[0].Rows[0].IsNull("RxNorm") == true)
                    txtRxNorm.Text = "";
                else
                    txtRxNorm.Text = theDS.Tables[0].Rows[0]["RxNorm"].ToString();

                if (theDS.Tables[0].Rows[0].IsNull("MorDose") == true || theDS.Tables[0].Rows[0]["MorDose"].ToString() == "0")
                    txtMornDose.Text = "";
                else
                    txtMornDose.Text = theDS.Tables[0].Rows[0]["MorDose"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("MidDose") == true || theDS.Tables[0].Rows[0]["MidDose"].ToString() == "0")
                    txtMidDayDose.Text = "";
                else
                    txtMidDayDose.Text = theDS.Tables[0].Rows[0]["MidDose"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("EvenDose") == true || theDS.Tables[0].Rows[0]["EvenDose"].ToString() == "0")
                    txtEvenDose.Text = "";
                else
                    txtEvenDose.Text = theDS.Tables[0].Rows[0]["EvenDose"].ToString();

                if (theDS.Tables[0].Rows[0].IsNull("NightDose") == true || theDS.Tables[0].Rows[0]["NightDose"].ToString()=="0")
                    txtNightDose.Text = "";
                else
                    txtNightDose.Text = theDS.Tables[0].Rows[0]["NightDose"].ToString();

                
                txtTradeName.Text = theDS.Tables[0].Rows[0]["ItemName"].ToString();
                txtTradeName.Enabled = false;
                
                if (theDS.Tables[0].Rows[0].IsNull("PurchaseUnit") == true)
                    ddlpurchaseunit.SelectedValue = "0";
                else
                    ddlpurchaseunit.SelectedValue = theDS.Tables[0].Rows[0]["PurchaseUnit"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("QtyPerPurchaseUnit") == true)
                    txtpurchaseqty.Text = "";
                else
                    txtpurchaseqty.Text = theDS.Tables[0].Rows[0]["QtyPerPurchaseUnit"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("PurchaseUnitPrice") == true)
                    txtPurUnitPrice.Text = "";
                else
                    txtPurUnitPrice.Text = theDS.Tables[0].Rows[0]["PurchaseUnitPrice"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("Manufacturer") == true)
                    ddlmanufaturer.SelectedValue = "0";
                else
                    ddlmanufaturer.SelectedValue = theDS.Tables[0].Rows[0]["Manufacturer"].ToString();

                if (theDS.Tables[0].Rows[0].IsNull("DispensingUnit") == true)
                    ddldispensingunit.SelectedValue = "0";
                else
                    ddldispensingunit.SelectedValue = theDS.Tables[0].Rows[0]["DispensingUnit"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("DispensingUnitPrice") == true)
                    txtDispUnitPrice.Text = "";
                else
                    txtDispUnitPrice.Text = theDS.Tables[0].Rows[0]["DispensingUnitPrice"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("DispensingMargin") == true)
                    txtDispMargin.Text = "0";
                else
                    txtDispMargin.Text = theDS.Tables[0].Rows[0]["DispensingMargin"].ToString();
                if ((theDS.Tables[0].Rows[0].IsNull("EffectiveDate") == true) || (Convert.ToDateTime(theDS.Tables[0].Rows[0]["EffectiveDate"]).ToString("dd-MMM-yyyy")=="01-Jan-1900"))
                {
                }
                else
                {
                    txtEffectiveDate.Text = Convert.ToDateTime(theDS.Tables[0].Rows[0]["EffectiveDate"]).ToString("dd-MMM-yyyy");
                }
                if (theDS.Tables[0].Rows[0].IsNull("SellingUnitPrice") == true)
                    txtsellingprice.Text = "";
                else
                    txtsellingprice.Text = theDS.Tables[0].Rows[0]["SellingUnitPrice"].ToString();

                
                if (theDS.Tables[0].Rows[0].IsNull("ItemInstructions") == true)
                    txtinstructions.Text = "";
                else
                    txtinstructions.Text = theDS.Tables[0].Rows[0]["ItemInstructions"].ToString();

                if (theDS.Tables[0].Rows[0].IsNull("syrup") == true)
                    chksyrup.Checked=false;
                else if (theDS.Tables[0].Rows[0]["syrup"].ToString() == "1")
                {
                    chksyrup.Checked = true;
                    ddlVolumeUnit.Enabled = true;
                }

                if (theDS.Tables[0].Rows[0].IsNull("VolUnit") == true)
                    ddlVolumeUnit.SelectedValue = "0";
                else
                    ddlVolumeUnit.SelectedValue = theDS.Tables[0].Rows[0]["VolUnit"].ToString();
                               
                CalculateSellingPrice();
                collapsepanels();               
                
                
            }
            if (theDS.Tables[7].Rows.Count > 0)
            {
                
                string GenericAbbv = "";
                foreach (DataRow DRow in theDS.Tables[7].Rows)
                {
                    if (DRow["Abbrevation"].ToString() != "")
                    {
                        if (GenericAbbv == "")
                        {
                            GenericAbbv = DRow["Abbrevation"].ToString();
                        }
                        else
                        {
                            GenericAbbv = GenericAbbv + '/' + DRow["Abbrevation"].ToString();
                        }
                    }
                    DataTable theSelectedGeneric = MakeSelectedGenericTable();
                    DataRow theDR1 = theSelectedGeneric.NewRow();
                    theDR1["Drug_pk"] = ItemId.ToString();
                    theDR1["Id"] = DRow["ID"];
                    theDR1["Name"] = DRow["Name"];
                    theDR1["Abbrevation"] = DRow["Abbrevation"];
                    theSelectedGeneric.Rows.Add(theDR1);
                    ViewState["SelGeneric"] = theSelectedGeneric;

                    DataTable DT1 = (DataTable)ViewState["SelGeneric"];
                    theGenericTable = DT1;
                    foreach (DataRow theDR2 in DT1.Rows)
                    {
                        DataTable theDT2 = (DataTable)ViewState["GenericMaster"];
                        DataRow[] DR2 = theDT2.Select("GenericID = " + theDR1["Id"].ToString());
                        if (DR2.Length > 0)
                            theDT2.Rows.Remove(DR2[0]);
                        ViewState["GenericMaster"] = theDT2;
                    }
                    txtDrugAbbre.Text = GenericAbbv.ToString();
                }
                BindManager.BindList(lstGeneric, theDS.Tables[7], "Name", "Id");
            }
            if (theDS.Tables[8].Rows.Count > 0)
            {              
                
                BindManager.BindList(lstStrength, theDS.Tables[8], "Name", "Id");
                DataTable SelStrength = theDS.Tables[8];
                //SelStrength.Columns.Add("Abbrevation", System.Type.GetType("System.String"));
                ViewState["SelStrength"] = SelStrength;
                //////// StrengthMaster /////////
                DataTable DT = (DataTable)ViewState["SelStrength"];
                foreach (DataRow theDR in DT.Rows)
                {
                    DataTable theDT = (DataTable)ViewState["StrengthMaster"];
                    DataRow[] DR = theDT.Select("StrengthId = '" + theDR["Id"].ToString() + "'");
                    if (DR.Length > 0)
                        theDT.Rows.Remove(DR[0]);
                    ViewState["StrengthMaster"] = theDT;
                }
            }
            if (theDS.Tables[0].Rows[0]["ItemTypeID"].ToString() == "300")
            {
                lstGeneric.Enabled = false;
                btnAddGeneric.Enabled = false;
                lstStrength.Enabled = false;
                btnAddDose.Enabled = false;
            }
            if (theDS.Tables[9] !=null)
            {
                if (theDS.Tables[9].Rows.Count > 0)
                {
                    if (theDS.Tables[9].Rows[0]["BatchId"].ToString() != "0")
                    {
                        gridItemBatchList.DataSource = theDS.Tables[9];
                        gridItemBatchList.DataBind();
                        btnbatchsummary.Visible = true;
                    }                   
                }
                else
                    btnbatchsummary.Visible = false;
                
            }
            //ScriptManager.RegisterStartupScript(this, GetType(), "PopulateItmSubT", "PopulateItmSubT('" + theDS.Tables[0].Rows[0]["DrugSubTypeId"].ToString() + "');", true);

        }
        private void collapsepanels()
        {
            CPENigeriaMedical.Collapsed = false;
            CPENigeriaMedical.ClientState = false.ToString().ToLower();
            CPESCM.Collapsed = false;
            CPESCM.ClientState = false.ToString().ToLower();
            CPEPricing.Collapsed = false;
            CPEPricing.ClientState = false.ToString().ToLower();
        }
        private void CalculateSellingPrice()
        {
            if (txtDispMargin.Text == ".")
                return;

            decimal theDispUnitPrice = 0;
            decimal theMargin = 0;

            if (txtDispUnitPrice.Text == "")
                theDispUnitPrice = 0;
            else
                theDispUnitPrice = Convert.ToDecimal(txtDispUnitPrice.Text);
            if (txtDispMargin.Text == "")
                theMargin = 0;
            else
                theMargin = Convert.ToDecimal(txtDispMargin.Text);
            if (theDispUnitPrice == 0)
                txtsellingprice.Text = "0";
            else if (theMargin == 0)
                txtsellingprice.Text = theDispUnitPrice.ToString();
            else
                txtsellingprice.Text = Math.Round((theDispUnitPrice + (theMargin / 100) * theDispUnitPrice), 2).ToString();
            
        }
        private void RemoveColumn(DataTable tbl)
        {            
            var keepColNames = new List<String>() { "ID","Name" };
            var allColumns = tbl.Columns.Cast<DataColumn>();
            var allColNames = allColumns.Select(c => c.ColumnName);
            var removeColNames = allColNames.Except(keepColNames);
            var colsToRemove = from r in removeColNames
                               join c in allColumns on r equals c.ColumnName
                               select c;
            while (colsToRemove.Any())
                tbl.Columns.Remove(colsToRemove.First());
        }
        private void GetMasters()
        {
            IDrugMst DrugManager;
            try
            {

                DataSet theDSXML = new DataSet();
                theDSXML.ReadXml(MapPath("..\\XMLFiles\\DrugMasters.con"));
                if (theDSXML.Tables["Mst_DrugType"] != null)//10Mar08 -- put conditios
                {
                    DataView theDrugTypeView = new DataView(theDSXML.Tables["Mst_DrugType"].Copy());
                    theDrugTypeView.Sort = "DrugTypeName asc";
                    theMasterDS.Tables.Add(theDrugTypeView.ToTable()); // table 0

                    DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                    DataSet theDS = new DataSet();
                    theDS = (DataSet)DrugManager.GetAllDropDowns();//pr_Admin_GetDrugDropDowns_Constella //all GenID,GenName,GenAbbr,DrugTypeID,DelFlag

                    
                    theMasterDS.Tables.Add(theDS.Tables[0].Copy()); // get list of all generics // table 1

                    theMasterDS.Tables.Add(theDSXML.Tables["Mst_Strength"].Copy()); // table 2
                    theMasterDS.Tables.Add(theDSXML.Tables["Mst_Frequency"].Copy()); // table 3
                    theMasterDS.Tables.Add(theDSXML.Tables["Mst_DrugSchedule"].Copy()); // table 4
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);

            }
            finally
            {
                DrugManager = null;
            }
        }

        protected void btnAddNewItem_Click(object sender, EventArgs e)
        {
            hdCustID.Value = string.Empty;
            hdFlag.Value = string.Empty;
            hdsubtype.Value = string.Empty;
            hdStrGenID.Value = string.Empty;
            ClearFields();
            collapsepanels();

        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataTable thedt = new DataTable();
            thedt.Columns.Add("Id", System.Type.GetType("System.Int32"));
            thedt.Columns.Add("Name", System.Type.GetType("System.String"));
            string values = hdStrGenID.Value;
            if(hdStrGenID.Value !=null || hdStrGenID.Value !=string.Empty || hdStrGenID.Value !="")
            {
                if (values.Length > 0 )
                {
                    values = values.Remove(values.Length - 1);
                    string[] result = values.Split(new string[] { "," }, StringSplitOptions.None);
                    foreach (string s in result)
                    {
                        string[] newresult = s.Split(new string[] { "-" }, StringSplitOptions.None);
                        DataRow theDR = thedt.NewRow();
                        theDR["Id"] = newresult[1];
                        theDR["Name"] = newresult[0];
                        thedt.Rows.Add(theDR);
                    }

                    //if (lstSelected.Items.Count > 0)
                    //{
                    //    foreach (ListItem lstItem in lstSelected.Items)
                    //    {
                    //        DataRow theDR = thedt.NewRow();
                    //        theDR["Id"] = lstItem.Value;
                    //        theDR["Name"] = lstItem.Value;
                    //        thedt.Rows.Add(theDR);
                    //    }
                    //}
                    BindFunctions theBind = new BindFunctions();
                    if (hdFlag.Value == "Generic")
                    {
                        ViewState["SelGeneric"] = (DataTable)thedt;
                        theBind.BindList(lstGeneric, (DataTable)ViewState["SelGeneric"], "Name", "Id");
                        //--remove selected Generic from from ViewState["StrengthMaster"]
                        if (ViewState["SelGeneric"] != null)
                        {
                            DataTable dtt = new DataTable();
                            DataTable dtGenMaster = (DataTable)ViewState["GenericMaster"];
                            DataTable dtGenSelected = (DataTable)ViewState["SelGeneric"];

                            var dtUpdate = new HashSet<int>(dtGenSelected.AsEnumerable()
                            .Select(row => row.Field<int>("Id")));

                            var varUnmatched = from all in dtGenMaster.AsEnumerable()
                                               let id = all.Field<int>("GenericId")
                                               where !dtUpdate.Contains(id)
                                               select all;
                            DataTable dtInsert = varUnmatched.CopyToDataTable();


                            ViewState["GenericMaster"] = dtInsert;
                        }
                    }
                    else if (hdFlag.Value == "Strength")
                    {
                        ViewState["SelStrength"] = (DataTable)thedt;
                        theBind.BindList(lstStrength, (DataTable)ViewState["SelStrength"], "Name", "Id");
                        //--remove selected strength from from ViewState["StrengthMaster"]
                        if (ViewState["SelStrength"] != null)
                        {
                            DataTable dtStrenthMaster = (DataTable)ViewState["StrengthMaster"];
                            DataTable dtStrnSelected = (DataTable)ViewState["SelStrength"];

                            var dtUpdate = new HashSet<int>(dtStrnSelected.AsEnumerable()
                            .Select(row => row.Field<int>("Id")));

                            var varUnmatched = from all in dtStrenthMaster.AsEnumerable()
                                               let id = all.Field<int>("StrengthId")
                                               where !dtUpdate.Contains(id)
                                               select all;
                            DataTable dtStrenInsert = varUnmatched.CopyToDataTable();

                            ViewState["StrengthMaster"] = dtStrenInsert;
                        }
                    }
                }
            }
            string scriptString = "<script language='javascript'> " +
         "window.opener.__doPostBack('Submit_OnClick',''); </script>";
            if (!Page.ClientScript.IsClientScriptBlockRegistered(scriptString))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                                                   "script", scriptString);
            }
            mp1.Hide();
        }
        protected void btnAddDose_Click(object sender, EventArgs e)
        {
            hdFlag.Value = "Strength";
            BindFunctions BindManager = new BindFunctions();
            BindManager.BindList(lstAvailable, (DataTable)ViewState["StrengthMaster"], "StrengthName", "StrengthId");
            BindManager.BindList(lstSelected, (DataTable)ViewState["SelStrength"], "Name", "Id");
            lblGenStrPopup.Text = "Strength Selection";
            mp1.Show();
        }
        private Boolean FieldValidation()
        {
            if (ddlItemType.SelectedValue == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                EventArgs e = new EventArgs();
                theBuilder.DataElements["Control"] = "Item Type";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                ddlItemType.Focus();
                return false;
            }
            if ((hdsubtype.Value == "0") || (hdsubtype.Value == string.Empty))
            {
                if (ddlItemSubType.SelectedIndex == 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    EventArgs e = new EventArgs();
                    theBuilder.DataElements["Control"] = "Item Sub Type";
                    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                    ddlItemSubType.Focus();
                    return false;
                }
                else
                {
                    hdsubtype.Value = Convert.ToString(ddlItemSubType.SelectedValue.Trim());
                }
            }
            if (txtTradeName.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                EventArgs e = new EventArgs();
                theBuilder.DataElements["Control"] = "Trade Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtTradeName.Focus();
                return false;
            }
             
            if (ddlItemType.SelectedValue == "300")
            {
                if (lstGeneric.Items.Count == 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    EventArgs e = new EventArgs();
                    theBuilder.DataElements["Control"] = "Generic List";
                    IQCareMsgBox.Show("BlankList", theBuilder, this);
                    ddlItemType.Focus();
                    return false;
                }
                if (lstStrength.Items.Count == 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    EventArgs e = new EventArgs();
                    theBuilder.DataElements["Control"] = "Strength List";
                    IQCareMsgBox.Show("BlankList", theBuilder, this);
                    ddlItemType.Focus();
                    return false;
                }
                
            }
            if (txtEffectiveDate.Text=="")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                EventArgs e = new EventArgs();
                theBuilder.DataElements["Control"] = "Effective Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtTradeName.Focus();
                return false;
            }
            IQCareMsgBox.HideMessage(this);
            return true;
        }
        private void ClearFields()
        {
           ddlItemType.SelectedValue = "0";
           ddlItemSubType.SelectedValue = "0";
           txtItemCode.Text = "";
           txtRxNorm.Text = "";
           txtinstructions.Text = "";
           if (txtTradeName.Enabled == false)
               txtTradeName.Enabled = true;
           txtTradeName.Text = "";           
           txtDrugAbbre.Text = "";

           txtMornDose.Text = "";
           txtMidDayDose.Text = "";
           txtEvenDose.Text = "";
           txtNightDose.Text = "";

           ddlmanufaturer.SelectedValue = "0";
           chksyrup.Checked = false;
           ddlVolumeUnit.SelectedValue = "0";
           ddlpurchaseunit.SelectedValue = "0";
           txtpurchaseqty.Text = "";
           txtPurUnitPrice.Text = "";
           txtDispMargin.Text = "";
           txtDispUnitPrice.Text = "";
           ddldispensingunit.SelectedValue = "0";
           txtsellingprice.Text = "";
           txtEffectiveDate.Text = "";

           lstGeneric.Items.Clear();
           lstStrength.Items.Clear();
           ViewState["SelGeneric"] = null;
           ViewState["SelStrength"] = null;
        }
        private Hashtable HtParameters()
        {            
            Hashtable theHT = new Hashtable();
            try
            {
                    theHT.Add("ItemID", Session["ItemId"]);                
                
                    #region "Item Identifier"
                    
                    theHT.Add("ItemType", this.ddlItemType.SelectedValue.ToString());
                    if(hdsubtype.Value.ToString() !="")
                        theHT.Add("ItemSubType", this.hdsubtype.Value.ToString());
                    else
                        theHT.Add("ItemSubType", "0");
                    theHT.Add("ItemCode", this.txtItemCode.Text);
                    theHT.Add("RxNorm", this.txtRxNorm.Text);
                    theHT.Add("ItemName", this.txtTradeName.Text);
                    theHT.Add("DrugAbbre", this.txtDrugAbbre.Text);
                    #region "Doses"

                    theHT.Add("MorningDose", this.txtMornDose.Text);
                    theHT.Add("MiddayDose", this.txtMidDayDose.Text);
                    theHT.Add("EveningDose", this.txtEvenDose.Text);
                    theHT.Add("NightDose", this.txtNightDose.Text);
                    #endregion
                    #endregion
                    #region "Supply Chain Information"

                    theHT.Add("Manufacturer", this.ddlmanufaturer.SelectedValue.ToString());                    
                    theHT.Add("ItemInstruction", this.txtinstructions.Text);
                    int SyrupPowder = chksyrup.Checked ? 1 : 0 ;
                    theHT.Add("SyrupPowder", SyrupPowder);
                    if (SyrupPowder == 1)
                    {
                        theHT.Add("VolumeUnit", this.ddlVolumeUnit.SelectedValue.ToString());
                    }
                    else
                        theHT.Add("VolumeUnit", "0");
                    theHT.Add("PurchaseUnit", this.ddlpurchaseunit.SelectedValue.ToString());
                    if (this.txtpurchaseqty.Text != "")
                    {
                        theHT.Add("PurchaseQuantity", this.txtpurchaseqty.Text);
                    }
                    else
                    {
                        theHT.Add("PurchaseQuantity", "0");
                    }
                    
                    

                    #endregion
                    #region "Pricing" 
                    if (this.txtPurUnitPrice.Text != "")
                    {
                        theHT.Add("PurchaseUnitPrice", this.txtPurUnitPrice.Text);
                    }
                    else
                    {
                        theHT.Add("PurchaseUnitPrice", "0");
                    }   
                    
                    
                    if (this.txtDispMargin.Text != "")
                    {
                        theHT.Add("DispMargin", this.txtDispMargin.Text.ToString());
                    }
                    else
                    {
                        theHT.Add("DispMargin", "0");
                    }
                    if (this.txtDispUnitPrice.Text != "")
                    {
                        theHT.Add("DispUnitPrice", this.txtDispUnitPrice.Text);
                    }
                    else
                    {
                        theHT.Add("DispUnitPrice", "0");
                    }
                    if (this.txtsellingprice.Text != "")
                    {
                        theHT.Add("SellingPrice", this.txtsellingprice.Text);
                    }
                    else
                    {
                        theHT.Add("SellingPrice", "0");
                    }
                    theHT.Add("DispensingUnit", this.ddldispensingunit.SelectedValue.ToString());
                    theHT.Add("EffectiveDate", this.txtEffectiveDate.Text);                    

                    #endregion                    
                
                return theHT;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (FieldValidation())
            {
                if (hdCustID.Value == null || hdCustID.Value == "")
                    hdCustID.Value = "0";
                DataTable theStrengthDT = new DataTable();
                DataTable theGenericDT = new DataTable();
                if (ViewState["SelStrength"] != null)
                {
                    theStrengthDT = (DataTable)ViewState["SelStrength"];
                }
                if (ViewState["SelGeneric"] != null)
                {
                    theGenericDT = (DataTable)ViewState["SelGeneric"];
                }
                if(theGenericDT.Columns.Count>2)
                    RemoveColumn(theGenericDT);
                

                Hashtable theHT = HtParameters();
                IDrugMst DrugManager = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");
                int roweffected = DrugManager.SaveUpdateWebDrugDetails(Convert.ToInt32(hdCustID.Value), theHT, theGenericDT, Convert.ToInt32(Session["AppUserId"]), theStrengthDT);
                if (roweffected > 0)
                {
                    ClearFields();
                    closeWindow();
                    
                }

            }
        }
        private void closeWindow()
        {
            IQCareMsgBox.NotifyAction("Item saved successfully. Do you want to close?", "Item Management", false, this, "window.location.href='../frmFacilityHome.aspx';");
            
        }
        protected void btnAddGeneric_Click1(object sender, EventArgs e)
        {
            hdFlag.Value = "Generic";
            BindFunctions BindManager = new BindFunctions();
            BindManager.BindList(lstAvailable, (DataTable)ViewState["GenericMaster"], "GenericName", "GenericId");
            BindManager.BindList(lstSelected, (DataTable)ViewState["SelGeneric"], "Name", "Id");
            lblGenStrPopup.Text = "Generic Selection";
            mp1.Show();
            
        }

        protected void ddlItemSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdsubtype.Value = Convert.ToString(ddlItemSubType.SelectedValue.Trim());
        }
    }
}
