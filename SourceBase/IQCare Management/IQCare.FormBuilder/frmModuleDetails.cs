using System;
using System.Collections;
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
using System.Configuration;
using System.Web;

    namespace IQCare.FormBuilder
    {
    public partial class frmModuleDetails : Form
    {
        string strModuleName;
        DataSet objDsIdentifierDetails = new DataSet();
        DataTable dtSubmit = new DataTable();
        int intIdentifierid = 0;
        int intUpdateFlag = 0;
        int intOldIdentifierrow = 0;
        int intlabel = 0;
        string strOldModuleName = "";
        int intOldModuleId = 0;
        Form theForm;
        public frmModuleDetails()
        {
            InitializeComponent();
        }

        private void frmModuleDetails_Load(object sender, EventArgs e)
        {

            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            cmbFieldType.Enabled = true;
            txtstartingnumber.Visible = false;
            lblstartingnumber.Visible = false;
            ClearBusinessRules();
            if (GblIQCare.ModuleId != 0)
            {
                txtModuleName.Text = GblIQCare.ModuleName;
                if (GblIQCare.PharmacyFlag == 1)
                {
                    chkHivCareTrmt.Checked = true;
                }
                else
                {
                    chkHivCareTrmt.Checked = false;
                }
            }
            if (GblIQCare.UpdateFlag != 0)
            {
                txtModuleName.ReadOnly = true;
                txtFieldName.ReadOnly = true;
                btnSumit.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                txtModuleName.ReadOnly = false;
                txtFieldName.ReadOnly = false;
                btnSumit.Enabled = true;
                //btnSave.Enabled = true;
            }
            if ((GblIQCare.UpdateFlag != 0) && (GblIQCare.ModuleName == "SMART ART FORM"))
            {
                txtModuleName.ReadOnly = true;
                txtFieldName.ReadOnly = false;
                btnSumit.Enabled = true;
                //btnSave.Enabled = true;
            }
            //Jayant 472	IQCare 3.7.0 
            if (GblIQCare.IdentifierFlag == 1)
            {
                btnSave.Enabled = true;
            }
            BindGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form theForm;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmModule, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
            this.Close();
        }

        public void ShowGrid(DataTable dt)
        {
            try
            {
                dgwFieldDetails.Columns.Clear();
                dgwFieldDetails.DataSource = null;
                DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                col1.HeaderText = "Identifier Name";
                col1.DataPropertyName = "IdentifierName";
                col1.Width = 400;
                col1.ReadOnly = true;

                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = "Identifier Label";
                col2.DataPropertyName = "Label";
                col2.Width = 250;
                col2.ReadOnly = true;

                DataGridViewCheckBoxColumn col3 = new DataGridViewCheckBoxColumn();
                col3.HeaderText = "Select";
                col3.DataPropertyName = "Selected";
                col3.Width = 120;
                if (GblIQCare.UpdateFlag == 1)
                {
                    col3.ReadOnly = true;

                }
                else
                {
                    col3.ReadOnly = false;
                }

                if ((GblIQCare.UpdateFlag == 1) && (GblIQCare.ModuleName == "SMART ART FORM"))
                {
                    col3.ReadOnly = false;
                }
                DataGridViewCheckBoxColumn col7 = new DataGridViewCheckBoxColumn();
                col7.HeaderText = "Required";
                col7.DataPropertyName = "Required";
                col7.Width = 120;
                col7.ReadOnly = false;

                


                DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
                col4.HeaderText = "";
                col4.DataPropertyName = "ID";
                col4.Width = -1;
                col4.ReadOnly = true;
                col4.Visible = false;

                DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
                col5.HeaderText = "";
                col5.DataPropertyName = "FieldType";
                col5.Width = -1;
                col5.ReadOnly = true;
                col5.Visible = false;

                DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
                col6.HeaderText = "";
                col6.DataPropertyName = "UpdateFlag";
                col6.Width = -1;
                col6.ReadOnly = true;
                col6.Visible = false;

                dgwFieldDetails.AutoGenerateColumns = false;
                dgwFieldDetails.AllowUserToAddRows = false;

                dgwFieldDetails.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                dgwFieldDetails.DefaultCellStyle.Font.Size.Equals(12);
                dgwFieldDetails.ColumnHeadersDefaultCellStyle.Font.Bold.Equals(true);

                dgwFieldDetails.Columns.Add(col1);
                dgwFieldDetails.Columns.Add(col2);
                dgwFieldDetails.Columns.Add(col3);
                dgwFieldDetails.Columns.Add(col4);
                dgwFieldDetails.Columns.Add(col5);
                dgwFieldDetails.Columns.Add(col6);
                dgwFieldDetails.Columns.Add(col7);
                dgwFieldDetails.DataSource = dt;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }
        public void ClearBusinessRules()
        {
            GblIQCare.dtServiceBusinessValues.Clear();
            GblIQCare.dtServiceBusinessValues.Columns.Clear();
            GblIQCare.dtServiceBusinessValues.Rows.Clear();
        }
        public void BindGrid()
        {
            try
            {
                IModule objIdentifier;
                BindFunctions BindMgr = new BindFunctions();
                strModuleName = "";
                if (GblIQCare.ModuleName != "SMART ART FORM")
                {
                    strModuleName = txtModuleName.Text.Replace(" ", "");
                }
                else
                {
                    strModuleName = txtModuleName.Text;

                }

                objIdentifier = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");
                objDsIdentifierDetails = objIdentifier.GetModuleIdentifier(GblIQCare.ModuleId);
                if (objDsIdentifierDetails.Tables[0].Rows.Count > 0)
                {
                    BindFunctions theBind = new BindFunctions();
                    theBind.Win_BindCombo(cmbFieldType, objDsIdentifierDetails.Tables[0], "Name", "ControlId");
                    dtSubmit = objDsIdentifierDetails.Tables[1];
                }

                if (objDsIdentifierDetails.Tables[1].Rows.Count > 0)
                {
                    dtSubmit = objDsIdentifierDetails.Tables[1];
                    ClearGrid();
                    ShowGrid(objDsIdentifierDetails.Tables[1]);
                }
                if (objDsIdentifierDetails.Tables[2].Rows.Count > 0)
                {
                    btnservicebusinessrules.BackColor = System.Drawing.Color.Green;
                    GblIQCare.dtServiceBusinessValues = objDsIdentifierDetails.Tables[2];
                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void ClearGrid()
        {
            dgwFieldDetails.Columns.Clear();
            dgwFieldDetails.Rows.Clear();
            dgwFieldDetails.Refresh();

        }

        public void SaveUpdateModule()
        {
            string Anthem = "Star Spangled Banner";
            Anthem = Anthem.Replace(" ", "");

            Hashtable theHT = new Hashtable();
            theHT.Clear();
            IModule objIdentifier;
            strModuleName = "";
            strModuleName = txtModuleName.Text;

            Int32 pharmacyFlag = 0;
            if (chkHivCareTrmt.Checked)
            {
                pharmacyFlag = 1;
            }

            //if (GblIQCare.ModuleName != "SMART ART FORM")
            //{
            //    strModuleName = txtModuleName.Text.Replace(" ", "");
            //}
            //else
            //{
            //    strModuleName = txtModuleName.Text;

            //}

            //By Akhil - To prevent duplicate service name from Form - 3rd April 2014
            IModule objModuleDetail;
            objModuleDetail = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule,BusinessProcess.FormBuilder");
            DataSet objDsModuleDetails = objModuleDetail.GetModuleDetail();
            string s = txtModuleName.Text.Trim();

            if (objDsModuleDetails.Tables != null && objDsModuleDetails.Tables[0].Rows.Count > 0)
            {
                DataRow[] dr = objDsModuleDetails.Tables[0].Select("ModuleName = '" + s + "'");
                if (GblIQCare.ModuleId == 0)
                {
                    if (dr.Length > 0)
                    {
                        IQCareWindowMsgBox.ShowWindow("ModuleExist", this);
                        return;
                    }
                }

            }

            if ((GblIQCare.ModuleId == 0) && (intOldModuleId != 0) && (strOldModuleName == strModuleName))
            {
                theHT.Add("ModuleId", intOldModuleId);
            }
            else
            {
                strOldModuleName = strModuleName;
                theHT.Add("ModuleId", GblIQCare.ModuleId);
            }

            theHT.Add("ModuleName", strModuleName);
            theHT.Add("Status", 1);
            theHT.Add("DeleteFlag", 0);
            theHT.Add("UserID", GblIQCare.AppUserId);

            theHT.Add("PharmacyFlag", pharmacyFlag);

            objIdentifier = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");
            Int32 intExistModuleId = objIdentifier.SaveUpdateModuleDetail(theHT, dtSubmit, GblIQCare.dtServiceBusinessValues);
            intOldModuleId = intExistModuleId;
            if (intExistModuleId == 0)
            {
                IQCareWindowMsgBox.ShowWindow("ModuleExist", this);
                return;
            }
            else
            {
                Form theForm;
                theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmModule, IQCare.FormBuilder"));
                theForm.MdiParent = this.MdiParent;
                theForm.Left = 0;
                theForm.Top = 0;
                theForm.Focus();
                theForm.Show();
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((GblIQCare.UpdateFlag == 1) && (GblIQCare.ModuleName != "SMART ART FORM"))
            {
                IQCareWindowMsgBox.ShowWindow("UpdateModule", this);
                return;
            }
            int intIdentifierno = 0;
            int intAutoPopulatedidentifier = 0;
            if (txtModuleName.Text != "")
            {
                dtSubmit = (DataTable)dgwFieldDetails.DataSource;
                for (int i = 0; i < dtSubmit.Rows.Count; i++)
                {
                    if (dtSubmit.Rows[i]["Selected"].ToString() == "True")
                    {
                        intIdentifierno += 1;
                    }
                    if (dtSubmit.Rows[i]["Selected"].ToString() == "True" && dtSubmit.Rows[i]["FieldType"].ToString() == "17")
                    {
                        intAutoPopulatedidentifier += 1;
                    }
                }

                if (intIdentifierno > 4)
                {
                    IQCareWindowMsgBox.ShowWindow("IdentifierNo", this);
                    return;
                }
                if (intIdentifierno == 0 && GblIQCare.Identifier < 1)
                {
                    IQCareWindowMsgBox.ShowWindow("ModuleSave", this);
                    return;
                }
                if (intAutoPopulatedidentifier > 1)
                {
                    IQCareWindowMsgBox.ShowWindow("moreautopopulateidentifer", this);
                    return;
                }
                SaveUpdateModule();
            }
            else
            {
                IQCareWindowMsgBox.ShowWindow("ModuleRequired", this);
                return;
            }
        }

        private void btnSumit_Click(object sender, EventArgs e)
        {
            if ((txtFieldName.Text.Trim() == "") || Convert.ToInt32(cmbFieldType.SelectedValue) == 0 || (txtModuleName.Text == "" || txtlabel.Text == ""))
            {
                IQCareWindowMsgBox.ShowWindow("IdentifierTypeRequired", this);
                return;
            }
            if (cmbFieldType.SelectedValue.ToString() == "17")
            {
                if (txtstartingnumber.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("autopopulateidentifer", this);
                    return;
                }
            }
            ///
            DataView theDV = new DataView(dtSubmit);
            theDV.RowFilter = "IdentifierName='"+txtFieldName.Text.Trim().Replace(" ","_")+"'";
            if(theDV.Count>0)
            {
                theDV[0]["IdentifierName"] = txtFieldName.Text.Trim().Replace(" ","_");
                theDV[0]["Label"] = txtlabel.Text.Trim();
            }
            else
            {
                DataRow dr = dtSubmit.NewRow();
                dr["IdentifierName"] = txtFieldName.Text.Trim().Replace(" ","_");
                dr["Label"] = txtlabel.Text.Trim();
                dr["ModuleID"] = GblIQCare.ModuleId;
                dr["Selected"] = "True";
                dr["ID"] = 0;
                dr["FieldType"] = cmbFieldType.SelectedValue;
                dr["UpdateFlag"] = 0;
                if (txtstartingnumber.Text != "")
                {
                    dr["autopopulatenumber"] = txtstartingnumber.Text;
                }
                dtSubmit.Rows.Add(dr);            
            }

            cmbFieldType.Enabled = true;
            txtFieldName.Text = "";
            txtlabel.Text = "";
            cmbFieldType.SelectedValue = 0;
            intIdentifierid = 0;
            txtstartingnumber.Text = "";
            txtstartingnumber.Visible = false;
            lblstartingnumber.Visible = false;

    }

        private void dgwFieldDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                intOldIdentifierrow = e.RowIndex;
                intlabel = e.RowIndex;
                txtFieldName.Text = dgwFieldDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtlabel.Text = dgwFieldDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
                cmbFieldType.SelectedValue = Convert.ToInt32(dgwFieldDetails.Rows[e.RowIndex].Cells[4].Value.ToString());
                intIdentifierid = Convert.ToInt32(dgwFieldDetails.Rows[e.RowIndex].Cells[3].Value.ToString());
                intUpdateFlag = Convert.ToInt32(dgwFieldDetails.Rows[e.RowIndex].Cells[5].Value.ToString());

                if (Convert.ToInt32(intUpdateFlag) == 1)
                {
                    txtFieldName.ReadOnly = true;
                    intUpdateFlag = 0;
                }
                else
                {
                    txtFieldName.ReadOnly = false;
                }
                cmbFieldType.Enabled = false;
            }
        }

        private void txtModuleName_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strRestrictCharList = "[]{}\\|'\";:?/><,~!@#$%^&*()_-+=";

            if (strRestrictCharList.IndexOf(e.KeyChar) >= 0)
            {
                e.Handled = true;
            }
    }

        private void txtFieldName_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strRestrictCharList = "[]{}\\|'\";:?/><.,~!@#$%^&*()_-+=";

            if (strRestrictCharList.IndexOf(e.KeyChar) >= 0)
            {
                e.Handled = true;
            }

        }

       
        private void txtFieldName_Leave(object sender, EventArgs e)
        {
            txtlabel.Text = txtFieldName.Text;
        }

        private void chkHivCareTrmt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFieldType.SelectedValue.ToString() == "17")
            {
                txtstartingnumber.Visible = true;
                lblstartingnumber.Visible = true;
            }
            else
            {
                txtstartingnumber.Visible = false;
                lblstartingnumber.Visible = false;
            }
        }

        private void txtstartingnumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strRestrictCharList = "0123456789\b";

            if (strRestrictCharList.IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }
        }

        private void btnservicebusinessrules_Click(object sender, EventArgs e)
        {
            if (txtModuleName.Text != "")
            {
                GblIQCare.strserviceareaname = txtModuleName.Text;
            }
            else
            {
                IQCareWindowMsgBox.ShowWindow("ModuleRequired", this);
                return;
            }
            if (theForm == null)
            {
                GblIQCare.iFormMode = 0;
                theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmServiceBusinessRule, IQCare.FormBuilder"));
                theForm.Left = 0;
                theForm.Top = 0;
                theForm.FormClosed += new FormClosedEventHandler(theForm_FormClosed);
                theForm.Show();
                

                
            }

            
           
            
        }

        void theForm_FormClosed(object sender, EventArgs e)
        {
            theForm = null;
        }

    }

    }