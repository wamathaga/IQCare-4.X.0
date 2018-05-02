using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Interface.Clinical;
using Application.Presentation;
using Application.Common;
using Interface.Pharmacy;
using Interface.Laboratory;
 

namespace PresentationApp.AdminForms
{
    public partial class frmAdmin_PredefineDrugList : LogPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx",true);
            }
            if (this.IsPostBack != true)
            {
                ViewState["ListName"] = Request.QueryString["LstName"].ToString();
                if (Request.QueryString["TableName"] == "PreDefinedDruglist")
                {
                    DataSet thePharmacyDS = new DataSet();
                    IPediatric PediatricManager;
                    PediatricManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
                    thePharmacyDS = PediatricManager.GetPediatricFields(1);
                    Session["DrugData"] = thePharmacyDS.Tables[0];
                    BindList();
                }
                if (Request.QueryString["TableName"] == "PreDefinedLablist")
                {
                    lblHeader.Text = "Predefined Lab List";
                    DataSet theLabDS = new DataSet();
                    ILabFunctions LabManager;
                    LabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                    theLabDS = LabManager.GetLabs();
                    Session["DrugData"] = theLabDS.Tables[0];
                    BindLabList();
                }

            }
        }

        public void BindList()
        {
            IQCareUtils theUtils = new IQCareUtils();
            BindFunctions theBind = new BindFunctions();
            DataView theDV;
            theDV = new DataView((DataTable)Session["DrugData"]);
            DataTable theDT = theUtils.CreateTableFromDataView(theDV);
            if (theDT != null)
            {
                DataView theDV1 = new DataView(theDT);
                theDV1.Sort = "DrugName Asc";
                theDT = theUtils.CreateTableFromDataView(theDV1);
            }
            if (theDT != null)
            {
              Session["DrugTable"] = theDT;
              theBind.BindList(lstDrugList, theDT, "DrugName", "drug_pk");
            }

           
           
                DataSet thePharmacyDS = new DataSet();
                IPediatric PediatricManager;
                PediatricManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
                thePharmacyDS = PediatricManager.GetPreDefinedDruglist();
                if (thePharmacyDS.Tables[0].Rows.Count > 0)
                {
                    Session["SelectedData"] = thePharmacyDS.Tables[0];
                    theBind.BindList(lstSelectedDrug, thePharmacyDS.Tables[0], "DrugName", "drug_pk");
                    DataTable theDT1 = (DataTable)Session["DrugTable"];
                    foreach (DataRow r in thePharmacyDS.Tables[0].Rows)
                    {
                        DataRow[] theDR1 = theDT1.Select("drug_pk=" + r[0].ToString());
                        if(theDR1.Length >0)
                            theDT1.Rows.Remove(theDR1[0]);

                    }
                    Session["DrugTable"] = theDT1;
                    lstDrugList.Items.Clear();
                    theBind.BindList(lstDrugList, theDT1, "DrugName", "drug_pk");
                }
                else
                {
                    Session["SelectedData"] = CreateSelectedTable();
                }
            
        }
        public void BindLabList()
        {
            IQCareUtils theUtils = new IQCareUtils();
            BindFunctions theBind = new BindFunctions();
            DataView theDV;
            theDV = new DataView((DataTable)Session["DrugData"]);
            DataTable theDT = theUtils.CreateTableFromDataView(theDV);
            if (theDT != null)
            {
                DataView theDV1 = new DataView(theDT);
                theDV1.Sort = "SubTestName";
                theDT = theUtils.CreateTableFromDataView(theDV1);
            }
            if (theDT != null)
            {
                Session["DrugTable"] = theDT;
                theBind.BindList(lstDrugList, theDT, "SubTestName", "SubTestId");
            }
            DataSet theLabDS = new DataSet();
            ILabFunctions LabManager;
            LabManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            theLabDS = LabManager.GetPreDefinedLablist(Convert.ToInt32(Session["SystemId"]));

            if (theLabDS.Tables[0].Rows.Count > 0)
            {
                Session["SelectedData"] = theLabDS.Tables[0];
                theBind.BindList(lstSelectedDrug, theLabDS.Tables[0], "SubTestName", "SubTestId");
                DataTable theDT1 = (DataTable)Session["DrugTable"];
                foreach (DataRow r in theLabDS.Tables[0].Rows)
                {
                    DataRow[] theDR1 = theDT1.Select("SubTestId=" + r[1].ToString());
                    if (theDR1.Length > 0)
                    {
                        theDT1.Rows.Remove(theDR1[0]);
                    }
                }
                Session["DrugTable"] = theDT1;
                lstDrugList.Items.Clear();
                theBind.BindList(lstDrugList, theDT1, "SubTestName", "SubTestId");
            }
            else
            {
                Session["SelectedData"] = CreateLabSelectedTable();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            IPediatric PediatricManager;
            PediatricManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
            DataTable dtsave = CreateSaveTable();
            int i = 1;
            foreach (ListItem item in lstSelectedDrug.Items)
            {
                DataRow theDR;
                int idx = lstSelectedDrug.Items.IndexOf(item);
                theDR = dtsave.NewRow();
                string str = item.Text;
                theDR[0] = item.Value;
                theDR[1] = idx+1;
                theDR["SystemId"] = Convert.ToInt32(Session["SystemId"]);
                dtsave.Rows.Add(theDR);
                i++;
            }

            if (dtsave.Rows.Count> -1)
            {
                PediatricManager.SavePredefineList(Request.QueryString["TableName"].ToString(), dtsave, Convert.ToInt32(Session["AppUserId"]));
            }
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();
            IQCareMsgBox.Show("CustomMasterSave", theBuilder, this);
            Cancel();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string lstname = "";
            if (Request.QueryString["TableName"] == "PreDefinedLablist")
            {
                lstname = "Predefined Laboratory List";
            }
            else
                lstname = "Predefined Drug List";
            Session["SelectedData"] = null;
            Session["DrugTable"] = null;
            string theUrl = string.Format("{0}?TableName={1}&LstName={2}", "frmAdmin_PredefineDrugList.aspx", Request.QueryString["TableName"].ToString(), lstname.ToString());
            Response.Redirect(theUrl);
            
        }
        private void Cancel()
        {
            string lstname = "";
            if (Request.QueryString["TableName"] == "PreDefinedLablist")
            {
                lstname = "Predefined Laboratory List";
            }
            else
                lstname = "Predefined Drug List";
            Session["SelectedData"] = null;
            Session["DrugTable"] = null;
            string url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", "./frmAdmin_CustomList.aspx", Request.QueryString["TableName"].ToString(), "0", lstname.ToString(), "0", "1", "99", "0");
            Response.Redirect(url);
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        private DataTable CreateSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("drug_pk", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theDT.Columns.Add("genericid", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("drugtypeid", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("GenericAbbrevation", System.Type.GetType("System.String"));
            return theDT;
        }
        private DataTable CreateSaveTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("ID", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("SRNO", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("SystemId", System.Type.GetType("System.Int32"));
            return theDT;
        }
        private DataTable CreateLabSelectedTable()
        {
            DataTable theDT = new DataTable();
            //theDT.Columns.Add("Id", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("SubTestId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("SubTestName", System.Type.GetType("System.String"));
            theDT.Columns.Add("SRNo", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Systemid", System.Type.GetType("System.Int32"));
            return theDT;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            BindFunctions theBind = new BindFunctions();
            if (Request.QueryString["TableName"] == "PreDefinedDruglist")
            {
                if ((Convert.ToInt32(lstDrugList.Items.Count) > 0) && lstDrugList.SelectedIndex > -1)
                {
                    DataRow theDR;
                    DataTable theDT = (DataTable)Session["SelectedData"];
                    theDR = theDT.NewRow();
                    theDR[0] = Convert.ToInt32(lstDrugList.SelectedValue);
                    theDR[1] = lstDrugList.SelectedItem.Text;

                    DataTable theDT1 = (DataTable)Session["DrugTable"];
                    DataRow[] theDR1 = theDT1.Select("drug_pk=" + lstDrugList.SelectedValue);


                    theDT.Rows.Add(theDR);
                    theBind.BindList(lstSelectedDrug, theDT, "DrugName", "drug_pk");
                    Session["SelectedData"] = theDT;

                    theDT1.Rows.Remove(theDR1[0]);

                    lstDrugList.DataSource = theDT1;
                    lstDrugList.DataBind();
                    Session["DrugTable"] = theDT1;
                }
            }
            if (Request.QueryString["TableName"] == "PreDefinedLablist")
            {
                if ((Convert.ToInt32(lstDrugList.Items.Count) > 0) && lstDrugList.SelectedIndex > -1)
                {
                    DataRow theDR;
                    DataTable theDT = (DataTable)Session["SelectedData"];
                    theDR = theDT.NewRow();
                    theDR["SubTestId"] = Convert.ToInt32(lstDrugList.SelectedValue);
                    theDR["SubTestName"] = lstDrugList.SelectedItem.Text;

                    DataTable theDT1 = (DataTable)Session["DrugTable"];
                    DataRow[] theDR1 = theDT1.Select("SubTestId=" + lstDrugList.SelectedValue);

                    theDT.Rows.Add(theDR);
                    theBind.BindList(lstSelectedDrug, theDT, "SubTestName", "SubTestId");
                    Session["SelectedData"] = theDT;

                    theDT1.Rows.Remove(theDR1[0]);

                    lstDrugList.DataSource = theDT1;
                    lstDrugList.DataBind();
                    Session["DrugTable"] = theDT1;
                }
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["TableName"] == "PreDefinedDruglist")
            {
                if ((Convert.ToInt32(lstSelectedDrug.Items.Count) > 0) && lstSelectedDrug.SelectedIndex > -1)
                {
                    DataRow theDR;
                    DataTable theDT = (DataTable)Session["DrugTable"];
                    theDR = theDT.NewRow();
                    theDR[0] = Convert.ToInt32(lstSelectedDrug.SelectedValue);
                    theDR[1] = lstSelectedDrug.SelectedItem.Text;

                    DataTable theDT1 = (DataTable)Session["SelectedData"];
                    DataRow[] theDR1 = theDT1.Select("drug_pk=" + lstSelectedDrug.SelectedValue);
                    theDR[2] = theDR1[0][2];
                    theDR[3] = theDR1[0][3];
                    theDT.Rows.Add(theDR);

                    IQCareUtils theUtils = new IQCareUtils();
                    DataView theDV = theUtils.GridSort(theDT, "DrugName", "asc");
                    theDT = theUtils.CreateTableFromDataView(theDV);
                    lstDrugList.DataSource = theDT;
                    lstDrugList.DataBind();
                    Session["DrugTable"] = theDT;

                    theDT1.Rows.Remove(theDR1[0]);
                    lstSelectedDrug.DataSource = theDT1;
                    lstSelectedDrug.DataBind();
                    Session["SelectedData"] = theDT1;
                }
            }
            if (Request.QueryString["TableName"] == "PreDefinedLablist")
            {
                if ((Convert.ToInt32(lstSelectedDrug.Items.Count) > 0) && lstSelectedDrug.SelectedIndex>-1)
                {
                    DataRow theDR;
                    DataTable theDT = (DataTable)Session["DrugTable"];
                    theDR = theDT.NewRow();
                    theDR[0] = Convert.ToInt32(lstSelectedDrug.SelectedValue);
                    theDR[1] = lstSelectedDrug.SelectedItem.Text;

                    DataTable theDT1 = (DataTable)Session["SelectedData"];
                    DataRow[] theDR1 = theDT1.Select("SubTestId=" + lstSelectedDrug.SelectedValue);
                    //theDR[2] = theDR1[0][2];
                    //theDR[3] = theDR1[0][3];
                    theDT.Rows.Add(theDR);

                    IQCareUtils theUtils = new IQCareUtils();
                    DataView theDV = theUtils.GridSort(theDT, "SubTestId", "desc");
                    theDT = theUtils.CreateTableFromDataView(theDV);
                    lstDrugList.DataSource = theDT;
                    lstDrugList.DataBind();
                    Session["DrugTable"] = theDT;

                    theDT1.Rows.Remove(theDR1[0]);
                    lstSelectedDrug.DataSource = theDT1;
                    lstSelectedDrug.DataBind();
                    Session["SelectedData"] = theDT1;
                }
            }
        }
        
        protected void btn_Up_Click(object sender, ImageClickEventArgs e)
        {
            //this.lstSelectedDrug is ListBox
            if (this.lstSelectedDrug.SelectedIndex == -1 ||
                 this.lstSelectedDrug.SelectedIndex == 0)
                return;

            ListItem item, aboveItem;
            int itemIndex, aboveItemIndex;
            itemIndex = this.lstSelectedDrug.SelectedIndex;
            aboveItemIndex = this.lstSelectedDrug.SelectedIndex - 1;
            item = (ListItem)this.lstSelectedDrug.Items[itemIndex];
            aboveItem = (ListItem)this.lstSelectedDrug.Items[aboveItemIndex];

            this.lstSelectedDrug.Items.RemoveAt(aboveItemIndex);
            this.lstSelectedDrug.Items.Insert(itemIndex, aboveItem);
        }

        protected void btn_down_Click(object sender, ImageClickEventArgs e)
        {
            //this.lstSelectedDrug is ListBox
            if (this.lstSelectedDrug.SelectedIndex == -1 ||
                             this.lstSelectedDrug.SelectedIndex >= this.lstSelectedDrug.Items.Count)
                return;

            ListItem item, belowItem;
            int itemIndex, belowItemIndex;
            itemIndex = this.lstSelectedDrug.SelectedIndex;
            belowItemIndex = this.lstSelectedDrug.SelectedIndex + 1;
            if (belowItemIndex >= this.lstSelectedDrug.Items.Count)
                return;
            item = (ListItem)this.lstSelectedDrug.Items[itemIndex];
            belowItem = (ListItem)this.lstSelectedDrug.Items[belowItemIndex];

            this.lstSelectedDrug.Items.RemoveAt(itemIndex);
            this.lstSelectedDrug.Items.Insert(belowItemIndex, item);
            this.lstSelectedDrug.SelectedIndex = belowItemIndex; 
        }
    }
}