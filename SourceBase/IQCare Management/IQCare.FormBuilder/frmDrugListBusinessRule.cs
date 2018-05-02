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
using Interface.FormBuilder;
using Interface.SCM;
using System.Collections;

namespace IQCare.FormBuilder
{
    public partial class frmDrugListBusinessRule : Form
    {
        DataSet dsBusinessRule = new DataSet();
        public frmDrugListBusinessRule()
        {
            InitializeComponent();
        }

        private void frmDrugListBusinessRule_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            BindItemList(0);
            if (GblIQCare.blnCustomPharmacyPage)
            {
                btnSubmit.Enabled = false;
            }
            try
            {
                IFieldDetail objFieldDetail;
                objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                dsBusinessRule = objFieldDetail.GetBusinessDrugList(GblIQCare.iFieldId);
                for (int i = 0; i < chkItemList.Items.Count; i++)
                {
                    foreach (DataRow theDRDrugId in dsBusinessRule.Tables[0].Rows)
                    {
                        if (Convert.ToInt32(((System.Data.DataRowView)(chkItemList.Items[i])).Row.ItemArray[0]) == Convert.ToInt32(theDRDrugId["DrugId"]))
                        {
                            chkItemList.SetItemChecked(i, true);
                        }
                    }
                }
            }
            catch
            {

            }
        }

        /*private void BindItemsubTypeDropdown(string ItemTypeId)
        {
            try
            {
                IQCareUtils theUtils = new IQCareUtils();
                ddlItemType.DataSource = null;
                IMasterList objSubitemType = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                DataTable theDT = objSubitemType.GetSubItemType();
                BindFunctions theBind = new BindFunctions();
                DataView theDV = new DataView(theDT);
                theDV.RowFilter = "ItemTypeId =" + ItemTypeId;
                theBind.Win_BindCombo(ddlItemType, theDV.ToTable(), "DrugTypeName", "drugTypeID");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        */
        private void BindItemList(int subitemId)
        {
            try
            {
            chkItemList.DataSource = null;
            IMasterList objItemlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataSet theDS = objItemlist.GetItemList(subitemId);
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCheckListBox(chkItemList, theDS.Tables[0], "ItemName", "ItemID");
            if (chkItemList.Items.Count == 0)
                chkAll.Checked = false;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            IEnumerator myEnumerator = chkItemList.CheckedIndices.GetEnumerator();
            int y;
            while (myEnumerator.MoveNext() != false)
            {
                y = (int)myEnumerator.Current;
                chkItemList.SetItemChecked(y, false);
            }
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < 24; i++)
                {
                    this.chkItemList.SetItemChecked(i, true);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkItemList.CheckedItems.Count < 1)
                {
                    MessageBox.Show("Please Select atleast one drug");
                    return;
                }
                DataTable Drugtable = new DataTable();
                Drugtable.Columns.Add("FieldName", typeof(string));
                Drugtable.Columns.Add("DrugId", typeof(string));
                Drugtable.Columns.Add("DrugTypeId", typeof(string));
                foreach (object obj in chkItemList.CheckedItems)
                {
                    DataRow theDR = Drugtable.NewRow();
                    theDR["FieldName"] = GblIQCare.strFieldName;
                    theDR["DrugId"] = ((System.Data.DataRowView)(obj)).Row.ItemArray[0];
                    theDR["DrugTypeId"] = ((System.Data.DataRowView)(obj)).Row.ItemArray[2];
                    Drugtable.Rows.Add(theDR);
                }
                if (GblIQCare.objhashBusinessRule.Contains(GblIQCare.gblRowIndex))
                    GblIQCare.objhashBusinessRule[GblIQCare.gblRowIndex] = Drugtable;
                else
                    GblIQCare.objhashBusinessRule.Add(GblIQCare.gblRowIndex, Drugtable);
                GblIQCare.blnBusinessRuleChange = true;
                GblIQCare.dtTempValue = Drugtable.Copy();
                if (GblIQCare.dtTempValue.Rows.Count == 0)
                {
                    GblIQCare.strCount = "Zero";
                }
                this.Close();

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        
    }
}
