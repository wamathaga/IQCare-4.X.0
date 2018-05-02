using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.SCM;


namespace IQCare.SCM
{
    public partial class frmStoreMaster : Form
    {
        DataTable DTItemlist = new DataTable();
        int PId, MaxPId;
        string StoreId, StoreName;
        public frmStoreMaster()
        {
            InitializeComponent();
            txtStoreID.Select();
        }

        private void BindDropdown()
        {
            ddlStatus.Items.Clear();
            ddlStatus.Items.Add("Active");
            ddlStatus.Items.Add("InActive");
            ddlStatus.SelectedIndex = 0;

            ddlPurchasingStore.Items.Clear();
            ddlPurchasingStore.Items.Add("No");
            ddlPurchasingStore.Items.Add("Yes");
            ddlPurchasingStore.SelectedIndex = 0;
            cmbDispensing.SelectedIndex = 0;

        }

        private Boolean Validation_Form()
        {
            try
            {
                if (txtStoreID.Text == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Store ID";
                    IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                    txtStoreID.Focus();
                    return false;
                }

                 if (txtStoreName.Text == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Store Name";
                    IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                    txtStoreID.Focus();
                    return false;
                }
                 DataTable theDT = (DataTable)dgwStoreName.DataSource;
                 DataView theDV = new DataView(theDT);
                 theDV.RowFilter = "Srno='" + PId + "'";
                 if (theDV.Count == 0)
                 {
                     foreach (DataRow theDR in theDT.Rows)
                     {
                         if (txtStoreID.Text.ToUpper() == Convert.ToString(theDR["StoreId"]).ToUpper())
                         {
                             IQCareWindowMsgBox.ShowWindow("StoreIdduplicate", this);
                             txtStoreID.Focus();
                             return false;
                         }

                         else if (txtStoreName.Text.ToUpper() == Convert.ToString(theDR["Name"]).ToUpper())
                         {
                             IQCareWindowMsgBox.ShowWindow("StoreNameduplicate", this);
                             txtStoreName.Focus();
                             return false;
                         }
                     }
                 }

                 //if ((ddlPurchasingStore.SelectedIndex != 0) && (cmbDispensing.SelectedIndex != 0))
                 //{
                     if ((ddlPurchasingStore.SelectedItem =="Yes") && (cmbDispensing.SelectedItem=="Yes"))
                     {
                         IQCareWindowMsgBox.ShowWindow("PurchasingDispensingSameStore", this);
                         return false;
                     }
                 //}

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
            return true;
        }


        public void SetRights()
        {
            //Form level authentication
            if (GblIQCare.HasFunctionRight(ApplicationAccess.Store, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }
        }

        private void Clear_Form()
        {
            txtStoreID.Text = "";
            txtStoreName.Text = "";
            ddlPurchasingStore.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            cmbDispensing.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            txtStoreID.Focus();
        }

        private void Init_Form()
        {
            IMasterList objItemCommonlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            String TableName = "Mst_" + GblIQCare.ItemTableName;
            DTItemlist = objItemCommonlist.GetCommonItemList(GblIQCare.ItemCategoryId, TableName);
            if (DTItemlist.Rows.Count > 0)
            {
                if (TableName != "Mst_Drugtype")
                {
                    MaxPId = Convert.ToInt32(DTItemlist.Select("SRNo=MAX(SRNo)")[0].ItemArray[3]);
                }
            }
            ShowGrid(DTItemlist);
            Clear_Form();
        }

        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwStoreName.Columns.Clear();
                dgwStoreName.DataSource = null;
                ID.DataPropertyName = "Id";
                SRNo.DataPropertyName = "SRNo";
                ColStoreID.DataPropertyName = "StoreId";
                ColStoreName.DataPropertyName = "Name";
                ColPurchasingStore.DataPropertyName = "CentralStore";
                ColDispensingStore.DataPropertyName = "DispensingStore";
                ColStatus.DataPropertyName = "Status";
                ID.Visible = false;
                SRNo.Visible = false;//deepika
                dgwStoreName.Columns.Add(ID);
                dgwStoreName.Columns.Add(ColStoreID);
                dgwStoreName.Columns.Add(ColStoreName);
                dgwStoreName.Columns.Add(ColPurchasingStore);
                dgwStoreName.Columns.Add(ColDispensingStore);
                dgwStoreName.Columns.Add(ColStatus);
                dgwStoreName.Columns.Add(SRNo);

                dgwStoreName.AutoGenerateColumns = false;
                dgwStoreName.DataSource = theDT;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form theForm = new Form();
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList, IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            this.Close();
        }

        private void frmStoreMaster_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            BindDropdown();
            Init_Form();
            SetRights();
           
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Validation_Form() != false)
            {
                DTItemlist = (DataTable)dgwStoreName.DataSource;
                //DataView theDV = new DataView(DTItemlist);
                //theDV.RowFilter = "SRNo='" + PId + "'";
                DataView theDV = new DataView(DTItemlist);
                theDV.RowFilter = "Srno='" + PId + "'";
                DataTable TmpDT = (DataTable)dgwStoreName.DataSource;
                DataView TmpDV = new DataView(TmpDT);
                theDV.RowFilter = "Srno='" + PId + "'";

                //if (theDV.Count > 0)
                //{
                   
                //        theDV[0]["StoreId"] = txtStoreID.Text;
                //        theDV[0]["Name"] = txtStoreName.Text;
                //        theDV[0]["CentralStore"] = ddlPurchasingStore.SelectedItem;
                //        theDV[0]["DispensingStore"] = cmbDispensing.SelectedItem;
                //        theDV[0]["Status"] = ddlStatus.SelectedItem;
                   
                //}
                if (theDV.Count > 0)
                {
                    if (StoreId != txtStoreID.Text)
                    {
                        if (StoreName != txtStoreName.Text)
                        {
                            TmpDV.RowFilter = "Name='" + txtStoreName.Text + "'";
                            if (TmpDV.Count > 0)
                            {
                                IQCareWindowMsgBox.ShowWindow("StoreNameduplicate", this);
                                txtStoreID.Focus();
                                return;
                            }
                        }

                        TmpDV.RowFilter = "StoreId='" + txtStoreID.Text + "'";
                        if (TmpDV.Count > 0)
                        {

                            IQCareWindowMsgBox.ShowWindow("StoreIdduplicate", this);
                            txtStoreID.Focus();
                            return;
                        }
                        theDV[0]["StoreId"] = txtStoreID.Text;
                        theDV[0]["Name"] = txtStoreName.Text;
                        theDV[0]["CentralStore"] = ddlPurchasingStore.SelectedItem;
                        theDV[0]["DispensingStore"] = cmbDispensing.SelectedItem;
                        theDV[0]["Status"] = ddlStatus.SelectedItem;

                    }
                    else
                    {
                        if (StoreName != txtStoreName.Text)
                        {
                            TmpDV.RowFilter = "Name='" + txtStoreName.Text + "'";
                            if (TmpDV.Count > 0)
                            {
                                IQCareWindowMsgBox.ShowWindow("StoreNameduplicate", this);
                                txtStoreID.Focus();
                                return;
                            }
                        }
                        theDV[0]["StoreId"] = txtStoreID.Text;
                        theDV[0]["Name"] = txtStoreName.Text;
                        theDV[0]["CentralStore"] = ddlPurchasingStore.SelectedItem;
                        theDV[0]["DispensingStore"] = cmbDispensing.SelectedItem;
                        theDV[0]["Status"] = ddlStatus.SelectedItem;
                    }

                }
                else
                {
                   
                        MaxPId = MaxPId + 1;
                        DataRow theDRow = DTItemlist.NewRow();
                        theDRow["StoreId"] = txtStoreID.Text;
                        theDRow["Name"] = txtStoreName.Text;
                        theDRow["CentralStore"] = ddlPurchasingStore.SelectedItem;
                        theDRow["DispensingStore"] = cmbDispensing.SelectedItem;
                        theDRow["Status"] = ddlStatus.SelectedItem;
                        theDRow["SRNo"] = MaxPId;
                        DTItemlist.Rows.Add(theDRow);
                   
                }
                DTItemlist.AcceptChanges();
                ShowGrid(DTItemlist);
                Clear_Form();
                PId = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                String TableName = "Mst_" + GblIQCare.ItemTableName;
                IMasterList objItemlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                int retrows = objItemlist.SaveUpdateStore(DTItemlist, GblIQCare.ItemCategoryId, TableName, GblIQCare.AppUserId);
                IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
                Init_Form();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void dgwStoreName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    if (dgwStoreName.Rows[e.RowIndex].Cells["SRNo"].Value.ToString() != "")
                    {
                        PId = Convert.ToInt32(dgwStoreName.Rows[e.RowIndex].Cells["SRNo"].Value.ToString());
                    }
                    txtStoreID.Text = dgwStoreName.Rows[e.RowIndex].Cells[1].Value.ToString();
                    StoreId = txtStoreID.Text;
                    txtStoreName.Text = dgwStoreName.Rows[e.RowIndex].Cells[2].Value.ToString();
                    StoreName = txtStoreName.Text;
                    ddlPurchasingStore.SelectedItem = dgwStoreName.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cmbDispensing.SelectedItem = dgwStoreName.Rows[e.RowIndex].Cells[4].Value.ToString();
                    ddlStatus.SelectedItem = dgwStoreName.Rows[e.RowIndex].Cells[5].Value.ToString();
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Selected = true;
                }
                catch (Exception err)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                    IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                }
            }


        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Index >= 1)
                {
                    DTItemlist = (DataTable)dgwStoreName.DataSource;
                    string Id = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[0].Value.ToString();
                    string StoreId = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[1].Value.ToString();
                    string StrName = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[2].Value.ToString();
                    string StrCentralStore = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[3].Value.ToString();
                    string StrDispensingStore = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[4].Value.ToString();
                    string StrStatus = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[5].Value.ToString();
                  
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[0].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[0].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[1].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[1].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[2].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[2].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[3].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[3].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[4].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[4].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[5].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[5].Value;

                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[0].Value = Id;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[1].Value = StoreId;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[2].Value = StrName;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[3].Value = StrCentralStore;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[4].Value = StrDispensingStore;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[5].Value = StrStatus;
                    this.dgwStoreName.CurrentCell = this.dgwStoreName[2, dgwStoreName.CurrentCell.RowIndex - 1];
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Selected = true;
                    DTItemlist = (DataTable)dgwStoreName.DataSource;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Index < dgwStoreName.Rows.Count - 1)
                {
                    DTItemlist = (DataTable)dgwStoreName.DataSource;
                    string Id = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[0].Value.ToString();
                    string StoreId = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[1].Value.ToString();
                    string StrName = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[2].Value.ToString();
                    string StrCentralStore = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[3].Value.ToString();
                    string StrStatus = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[4].Value.ToString();

                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[0].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[0].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[1].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[1].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[2].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[2].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[3].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[3].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[4].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[4].Value;

                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[0].Value = Id;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[1].Value = StoreId;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[2].Value = StrName;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[3].Value = StrCentralStore;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[4].Value = StrStatus;
                    this.dgwStoreName.CurrentCell = this.dgwStoreName[2, dgwStoreName.CurrentCell.RowIndex + 1];
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Selected = true;
                    DTItemlist = (DataTable)dgwStoreName.DataSource;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        public bool checkValidation(DataTable theDT)
        {
            bool ret = true;
            for (int i = 0; i < theDT.Rows.Count; i++)
            {
                if (Convert.ToString(theDT.Rows[i]["StoreID"]).ToLower() == Convert.ToString(txtStoreID.Text).ToLower())
                {
                    MsgBuilder theBuilder = new MsgBuilder(); 
                    theBuilder.DataElements["Control"] = Convert.ToString(txtStoreID.Text);
                    IQCareWindowMsgBox.ShowWindowConfirm("DuplicateStoreID", theBuilder, this);
                    ret = false;
                    return ret;
                }
                if (Convert.ToString(theDT.Rows[i]["Name"]).ToLower() == Convert.ToString(txtStoreName.Text).ToLower())
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = Convert.ToString(txtStoreName.Text);
                    IQCareWindowMsgBox.ShowWindowConfirm("DuplicateStoreName", theBuilder, this);
                    ret = false;
                    return ret;
                }
            }
            return ret;
        }
    }
}
