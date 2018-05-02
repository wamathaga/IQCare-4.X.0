using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Interface.FormBuilder;
using Application.Presentation;

namespace IQCare.FormBuilder
{

    public partial class frmICD10Selector : Form
    {
        TreeNode ICDCode = new TreeNode();
        TreeNode ICDSubBlockCode = new TreeNode();
        TreeNode ICDBlockCode = new TreeNode();
        TreeNode ICDChapterCode = new TreeNode();
        DataTable theDTICD10 = new DataTable();
        
        public frmICD10Selector()
        {
            InitializeComponent();
        }

        private void frmICD10Selector_Load(object sender, EventArgs e)
        {
            if (GblIQCare.blnICD10multiselect)
            {
                BindTreeView();
                GblIQCare.blnOtherICD10 = false;
            }
            if (GblIQCare.blnOtherICD10)
            {
                BindTreeViewOther();
                GblIQCare.blnICD10multiselect = false;
            }
            CreateColumntheDTICD10();
            this.BringToFront();
            if (GblIQCare.iFieldId != 0)
            {
                GetICD10Values();
            }

        }

        private void BindTreeView()
        {
            IFieldDetail objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
            DataSet theDSTV = new DataSet();
            if (GblIQCare.dsTreeView == null)
            {
                theDSTV = objFieldDetail.GetICDList();
                GblIQCare.dsTreeView = theDSTV;
            }
            else
            {
                theDSTV = GblIQCare.dsTreeView;
            }

            foreach (DataRow theDR1 in theDSTV.Tables[0].Rows)
            {
                ICDChapterCode = new TreeNode();
                ICDChapterCode.Text = Convert.ToString(theDR1["ChapterName"].ToString().Replace("<br/>", ""));
                ICDChapterCode.Tag = Convert.ToString(theDR1["ChapterCode"]);
                foreach (DataRow theDR2 in theDSTV.Tables[1].Rows)
                {
                    if (Convert.ToInt32(theDR1["ChapterId"]) == Convert.ToInt32(theDR2["ChapterId"]))
                    {
                        ICDBlockCode = new TreeNode();
                        ICDBlockCode.Text = theDR2["BlockCode"] + " " + theDR2["BlockName"].ToString().Replace("<br/>", "");
                        ICDBlockCode.Tag = theDR2["BlockId"].ToString();
                        ICDChapterCode.Nodes.Add(ICDBlockCode);
                        foreach (DataRow theDR3 in theDSTV.Tables[2].Rows)
                        {
                            if (Convert.ToInt32(theDR2["BlockId"]) == Convert.ToInt32(theDR3["BlockId"]))
                            {
                                ICDSubBlockCode = new TreeNode();
                                ICDSubBlockCode.Text = theDR3["SubBlockCode"] + " " + theDR3["SubBlockName"].ToString().Replace("<br/>", "");
                                ICDSubBlockCode.Tag = theDR3["SubBlockId"].ToString();
                                ICDBlockCode.Nodes.Add(ICDSubBlockCode);
                                foreach (DataRow theDR4 in theDSTV.Tables[3].Rows)
                                {
                                    if (Convert.ToInt32(theDR3["SubBlockId"]) == Convert.ToInt32(theDR4["SubBlockId"]))
                                    {
                                        ICDCode = new TreeNode();
                                        ICDCode.Text = theDR4["ICDCode"] + " " + theDR4["ICDCodeName"].ToString().Replace("<br/>", "");
                                        ICDCode.Tag = theDR4["Id"].ToString();
                                        ICDSubBlockCode.Nodes.Add(ICDCode);

                                    }

                                }
                            }
                        }
                    }
                }
                TVICD10.Nodes.Add(ICDChapterCode);
            }
            TVICD10.ShowLines = true;
            TVICD10.CollapseAll();

        }

        private void BindTreeViewOther()
        {
            IFieldDetail objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
            DataSet theDSTV = new DataSet();
            if (GblIQCare.dsTreeView == null)
            {
                theDSTV = objFieldDetail.GetICDList();
                GblIQCare.dsTreeView = theDSTV;
            }
            else
            {
                theDSTV = GblIQCare.dsTreeView;
            }

            foreach (DataRow theDR1 in theDSTV.Tables[0].Rows)
            {
                TVICD10.CheckBoxes = true;
                ICDChapterCode = new TreeNode();
                ICDChapterCode.Text = Convert.ToString(theDR1["ChapterName"].ToString().Replace("<br/>", ""));
                ICDChapterCode.Tag = Convert.ToString(theDR1["ChapterCode"]);
                TVICD10.Nodes.Add(ICDChapterCode);
            }
            TVICD10.ShowLines = true;
            TVICD10.CollapseAll();

        }
        private void GetICD10Values()
        {
            IFieldDetail objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
            DataSet theDSTV = objFieldDetail.GetICD10Values(GblIQCare.iFieldId);
           
            if (GblIQCare.blnOtherICD10)
            {
                DataRow[] rows = theDSTV.Tables[0].Select("Chapterid>0");
                foreach (DataRow theDRValue in rows)
                {
                    theDTICD10.Rows.Add(theDRValue[0], theDRValue[1], theDRValue[2], theDRValue[3], theDRValue[4]);
                    theDTICD10.AcceptChanges();
                    CheckTreeview(Convert.ToInt32(theDRValue[4]));
                }

                lbICD10.DataSource = theDTICD10;
                lbICD10.DisplayMember = "ICD10Name";
            }
            if (GblIQCare.blnICD10multiselect)
            {
                DataRow[] rows = theDSTV.Tables[0].Select("Chapterid=0");
                foreach (DataRow theDRValue in rows)
                {
                    theDTICD10.Rows.Add(theDRValue[0], theDRValue[1], theDRValue[2], theDRValue[3], 0);
                    theDTICD10.AcceptChanges();
                }

                lbICD10.DataSource = theDTICD10;
                lbICD10.DisplayMember = "ICD10Name";
            }
            
        }
        public void CheckTreeview(int id)
        {
            foreach (TreeNode child in TVICD10.Nodes)
            {
                int childid = Convert.ToInt32(child.Tag);
                if (childid == id)
                {
                    child.Checked = true;
                }
            }
        }
        private void CreateColumntheDTICD10()
        {
            theDTICD10 = new DataTable();
            theDTICD10.Columns.Add("BlockId", typeof(int));
            theDTICD10.Columns.Add("SubBlockId", typeof(int));
            theDTICD10.Columns.Add("CodeId", typeof(int));
            theDTICD10.Columns.Add("ICD10Name", typeof(string));
            theDTICD10.Columns.Add("Deleteflag", typeof(int));
            theDTICD10.Columns.Add("ChapterId", typeof(int));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (GblIQCare.blnOtherICD10)
            {
                foreach (TreeNode child in TVICD10.Nodes)
                {
                    if (child.Checked)
                    {
                        int Id = Convert.ToInt32(child.Tag);
                        string Name = Convert.ToString(child.Text).Replace("'", "''");
                        Name = Name.Replace("''", "'");
                        if (theDTICD10.Rows.Count > 0)
                        {
                            DataRow[] row = theDTICD10.Select("ICD10Name='" + Name + "' and Deleteflag=0");
                            if (row.Length > 0)
                            {
                                IQCareWindowMsgBox.ShowWindow("DuplicateICD10", this);
                                return;
                            }
                        }
                        theDTICD10.Rows.Add(0, 0, 0, Name, 0, Id);
                        theDTICD10.AcceptChanges();
                        DataView dv = new DataView(theDTICD10);
                        dv.RowFilter = "Deleteflag=0";
                        lbICD10.DataSource = dv;
                        lbICD10.DisplayMember = "ICD10Name";
                    }
                }
            }
            if (GblIQCare.blnICD10multiselect)
            {
                if (TVICD10.SelectedNode != null)
                {
                    int Id = Convert.ToInt32(TVICD10.SelectedNode.Tag);
                    string Name = Convert.ToString(TVICD10.SelectedNode.Text).Replace("'", "''");

                    if (theDTICD10.Rows.Count > 0)
                    {
                        DataRow[] row = theDTICD10.Select("ICD10Name='" + Name + "' and Deleteflag=0");
                        if (row.Length > 0)
                        {
                            IQCareWindowMsgBox.ShowWindow("DuplicateICD10", this);
                            return;
                        }
                    }

                    int number = TVICD10.SelectedNode.Level;
                    Name = Name.Replace("''", "'");
                    switch (number)
                    {
                        case 1:
                            theDTICD10.Rows.Add(Id, 0, 0, Name, 0, 0);
                            theDTICD10.AcceptChanges();
                            break;
                        case 2:
                            theDTICD10.Rows.Add(0, Id, 0, Name, 0, 0);
                            theDTICD10.AcceptChanges();
                            break;
                        case 3:
                            theDTICD10.Rows.Add(0, 0, Id, Name, 0, 0);
                            theDTICD10.AcceptChanges();
                            break;
                        case 0:
                            if (GblIQCare.blnOtherICD10)
                            {
                                theDTICD10.Rows.Add(0, 0, 0, Name, 0, Id);
                                theDTICD10.AcceptChanges();
                            }
                            break;
                    }

                    DataView dv = new DataView(theDTICD10);
                    dv.RowFilter = "Deleteflag=0";
                    lbICD10.DataSource = dv;
                    lbICD10.DisplayMember = "ICD10Name";
                }
            }
        }
       
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbICD10.SelectedItem != null)
            {
                
                DataRow[] rows = theDTICD10.Select("ICD10Name='" + ((System.Data.DataRowView)(lbICD10.SelectedItem)).Row.ItemArray[3].ToString().Replace("'","''") + "'");
                rows[0]["Deleteflag"] = 1;
                theDTICD10.AcceptChanges();
                DataView dv = new DataView(theDTICD10);
                dv.RowFilter="Deleteflag=0";
                lbICD10.DataSource = dv;
                lbICD10.DisplayMember = "ICD10Name";
            }
        }
        public DataTable ChangeTable(DataRow[] r)
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("BlockId", typeof(int));
            theDT.Columns.Add("SubBlockId", typeof(int));
            theDT.Columns.Add("CodeId", typeof(int));
            theDT.Columns.Add("ICD10Name", typeof(string));
            theDT.Columns.Add("Deleteflag", typeof(int));
            theDT.Columns.Add("ChapterId", typeof(int));
            foreach (DataRow rw in r)
            {

            }
            return theDT;

        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            
            GblIQCare.dtICD10Code = theDTICD10.Copy();
            this.Close();

        }
        public DataTable BindICD10Table(DataTable dt)
        {
            IFieldDetail objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
            DataSet theDSTV = new DataSet();
            if (GblIQCare.dsTreeView == null)
            {
                theDSTV = objFieldDetail.GetICDList();
                GblIQCare.dsTreeView = theDSTV;
            }
            else
            {
                theDSTV = GblIQCare.dsTreeView;
            }
            DataTable theDTICD10 = new DataTable();
            theDTICD10.Columns.Add("BlockId", typeof(int));
            theDTICD10.Columns.Add("SubBlockId", typeof(int));
            theDTICD10.Columns.Add("CodeId", typeof(int));
            theDTICD10.Columns.Add("ICD10Name", typeof(string));
            theDTICD10.Columns.Add("Deleteflag", typeof(int));
            theDTICD10.Columns.Add("ChapterId", typeof(int));
            foreach (DataRow r in dt.Rows)
            {
                
                foreach (DataRow theDR1 in theDSTV.Tables[0].Rows)
                {

                    foreach (DataRow theDR2 in theDSTV.Tables[1].Rows)
                    {
                        if (Convert.ToInt32(theDR1["ChapterId"]) == Convert.ToInt32(r["ChapterId"]))
                        {

                            theDTICD10.Rows.Add(Convert.ToInt32(theDR2["BlockId"].ToString()), 0, 0, theDR2["BlockName"].ToString().Replace("'", "''"), 0);
                            theDTICD10.AcceptChanges();

                        }
                    }

                }
            }
            return theDTICD10;
        }
        private void FindNodeInHierarchy(TreeNodeCollection nodes, string strSearchValue)
        {
            Boolean m_bNodeFound = false;
            for (int iCount = 0; iCount < nodes.Count; iCount++)
            {
                if (nodes[iCount].Text.ToUpper().Contains(strSearchValue.ToUpper()))
                {
                    TVICD10.SelectedNode = nodes[iCount];
                    TVICD10.Select();
                    m_bNodeFound = true;
                    return;
                }
                else
                {
                    m_bNodeFound = false;
                }
               
                
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            FindNodeInHierarchy(TVICD10.Nodes, txtSearch.Text);
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmICD10Selector_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmICD10Selector_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
 
    }

   

}
