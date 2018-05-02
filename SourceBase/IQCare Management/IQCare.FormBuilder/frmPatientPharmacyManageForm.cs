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
    public partial class frmPatientPharmacyManageForm : Form
    {
        Image imgPublished;
        Image imgUnpublished;
        Image imgInprocess;
        string strPubValue;
        string strFormName;
        string strFormNameDb;
        string strPublished;
        int iColIndex;
        DataSet objDsFormDetails = new DataSet();
        Form theForm;

        public frmPatientPharmacyManageForm()
        {
            InitializeComponent();
        }
        private void frmPatientPharmacyManageForm_Load(object sender, EventArgs e)
        {
            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //set css end
            cmbFormStatus.SelectedIndex = 3;
        }
        private void EnableControl()
        {
            btnEdit.Enabled = true;
            SetRights();
        }
        public void SetRights()
        {
            //form level permission set
            if (GblIQCare.HasFunctionRight(ApplicationAccess.PatientRegistration, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnAdd.Enabled = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.PatientRegistration, FunctionAccess.Update, GblIQCare.dtUserRight) == false)
            {
                btnEdit.Enabled = false;
            }

        }
        private void DisableControl()
        {
            btnEdit.Enabled = false;
        }
        public void ShowGrid(DataTable dt)
        {
            try
            {
                dgwFormDetails.DataSource = null;
                DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                col1.HeaderText = "Form Name";
                col1.DataPropertyName = "FormName";
                col1.Width = 275;
                col1.ReadOnly = true;

                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = "Service Area";
                col2.DataPropertyName = "ServiceArea";
                col2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                col2.Width = 221;
                col2.ReadOnly = true;

                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col3.HeaderText = "Updated By";
                col3.DataPropertyName = "UpdatedBy";
                col3.Width = 150;
                col3.ReadOnly = true;

                DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
                col4.HeaderText = "Last Updated";
                col4.DataPropertyName = "LastUpdate";
                col4.Width = 150;
                col4.ReadOnly = true;

                DataGridViewImageColumn col5 = new DataGridViewImageColumn();
                col5.HeaderText = "Published";
                col5.Width = 110;
                col5.ReadOnly = false;

                DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
                col6.HeaderText = "PublishedValue";
                col6.DataPropertyName = "Published";
                col6.Width = 0;
                col6.ReadOnly = false;

                dgwFormDetails.AutoGenerateColumns = false;
                dgwFormDetails.AllowUserToAddRows = false;

                dgwFormDetails.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                dgwFormDetails.DefaultCellStyle.Font.Size.Equals(12);
                dgwFormDetails.ColumnHeadersDefaultCellStyle.Font.Bold.Equals(true);

                dgwFormDetails.Columns.Add(col1);
                dgwFormDetails.Columns.Add(col2);
                dgwFormDetails.Columns.Add(col3);
                dgwFormDetails.Columns.Add(col4);
                dgwFormDetails.Columns.Add(col5);
                dgwFormDetails.Columns.Add(col6);
                dgwFormDetails.DataSource = dt;
                dgwFormDetails.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        public void BindGrid()
        {
            try
            {
                IManageForms objFormDetail;
                objFormDetail = (IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder");
                objDsFormDetails = objFormDetail.GetPharmacyFormDetail(cmbFormStatus.SelectedIndex.ToString(), Convert.ToInt16(GblIQCare.AppCountryId));
                if (objDsFormDetails.Tables[0].Rows.Count > 0)
                {
                    ClearGrid();
                    string strGetPath = GblIQCare.GetPath();
                    imgPublished = Image.FromFile(strGetPath + "\\Published1.bmp");
                    imgUnpublished = Image.FromFile(strGetPath + "\\Unpublished11.bmp");
                    imgInprocess = Image.FromFile(strGetPath + "\\Inprocess11.bmp");
                    ShowGrid(objDsFormDetails.Tables[0]);
                }
                if (objDsFormDetails.Tables[1].Rows.Count > 0 && Convert.ToInt32(objDsFormDetails.Tables[1].Rows[0]["Exist"]) > 0)
                {
                    //btnAdd.Enabled = false;
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
            dgwFormDetails.Columns.Clear();
            dgwFormDetails.Rows.Clear();
            dgwFormDetails.Refresh();
        }

        private void dgwFormDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Color ClrRed = Color.FromArgb(204, 0, 0);
                Color ClrPink = Color.FromArgb(255, 204, 204);
                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    if (dgwFormDetails.Columns[e.ColumnIndex].HeaderText.Equals("PublishedValue"))
                    {
                        if ((e.Value == null) || (e.Value.ToString() == ""))
                        {
                            strPubValue = "0";
                        }
                        else
                        {
                            strPubValue = e.Value.ToString();
                        }

                        switch (strPubValue)
                        {
                            case "0":
                                dgwFormDetails.Rows[e.RowIndex].Cells[0].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells[1].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells[2].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells[3].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells[4].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells[4].Value = imgInprocess;
                                break;
                            case "1":
                                dgwFormDetails.Rows[e.RowIndex].Cells[0].Style.BackColor = ClrRed;
                                dgwFormDetails.Rows[e.RowIndex].Cells[1].Style.BackColor = ClrRed;
                                dgwFormDetails.Rows[e.RowIndex].Cells[2].Style.BackColor = ClrRed;
                                dgwFormDetails.Rows[e.RowIndex].Cells[3].Style.BackColor = ClrRed;
                                dgwFormDetails.Rows[e.RowIndex].Cells[4].Style.BackColor = ClrRed;
                                dgwFormDetails.Rows[e.RowIndex].Cells[4].Value = imgUnpublished;
                                break;
                            case "2":
                                dgwFormDetails.Rows[e.RowIndex].Cells[0].Style.BackColor = System.Drawing.Color.Green;
                                dgwFormDetails.Rows[e.RowIndex].Cells[1].Style.BackColor = System.Drawing.Color.Green;
                                dgwFormDetails.Rows[e.RowIndex].Cells[2].Style.BackColor = System.Drawing.Color.Green;
                                dgwFormDetails.Rows[e.RowIndex].Cells[3].Style.BackColor = System.Drawing.Color.Green;
                                dgwFormDetails.Rows[e.RowIndex].Cells[4].Style.BackColor = System.Drawing.Color.Green;
                                dgwFormDetails.Rows[e.RowIndex].Cells[4].Value = imgPublished;
                                break;
                            default:
                                dgwFormDetails.Rows[e.RowIndex].Cells[0].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells[1].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells[2].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells[3].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells[4].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells[4].Value = imgInprocess;
                                break;
                        }
                    }

                }
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void dgwFormDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.PatientRegistration, FunctionAccess.Update, GblIQCare.dtUserRight) == true)
                {
                    EnableControl();
                    if (e.ColumnIndex != -1 && e.RowIndex != -1)
                    {
                        strFormName = "";
                        strFormNameDb = dgwFormDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
                        strFormName = strFormNameDb.Replace(" ", "_");
                        for (int i = 0; i < objDsFormDetails.Tables[0].Rows.Count; i++)
                        {
                            if (strFormNameDb == objDsFormDetails.Tables[0].Rows[i]["FormName"].ToString())
                            {
                                GblIQCare.iFormBuilderId = Convert.ToInt32(objDsFormDetails.Tables[0].Rows[i]["FormId"]);
                                strPublished = objDsFormDetails.Tables[0].Rows[i][4].ToString();
                                break;
                            }
                        }
                    }
                    if (strPublished == "2")
                    {
                        DisableControl();
                    }
                    else
                    {
                        EnableControl();
                    }
                    for (int i = 0; i < objDsFormDetails.Tables[0].Rows.Count; i++)
                    {
                        if (strFormNameDb == objDsFormDetails.Tables[0].Rows[i]["FormName"].ToString())
                        {
                            if (objDsFormDetails.Tables[0].Rows[i]["Published"].ToString() == "2")
                            {
                                IQCareWindowMsgBox.ShowWindow("PMTCTPublishedForm", this);
                                return;
                            }
                            else
                            {
                                theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmPatientPharmacyFormBuilder, IQCare.FormBuilder"));
                                theForm.MdiParent = this.MdiParent;
                                theForm.Left = 0;
                                theForm.Top = 0;
                                theForm.Show();
                                this.Close();
                            }

                        }
                    }
                }
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void dgwFormDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.PatientRegistration, FunctionAccess.Update, GblIQCare.dtUserRight) == true)
                {
                    EnableControl();

                    if (e.RowIndex != -1)
                    {
                        strFormName = "";
                        strFormNameDb = dgwFormDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
                        strFormName = strFormNameDb.Replace(" ", "_");
                        for (int i = 0; i < objDsFormDetails.Tables[0].Rows.Count; i++)
                        {
                            if (strFormNameDb == objDsFormDetails.Tables[0].Rows[i]["FormName"].ToString())
                            {
                                GblIQCare.iFormBuilderId = Convert.ToInt32(objDsFormDetails.Tables[0].Rows[i]["FormId"]);
                                strPublished = objDsFormDetails.Tables[0].Rows[i][4].ToString();
                                break;
                            }

                        }
                        if (strPublished == "2")
                        {
                            DisableControl();
                        }
                        else
                        {
                            EnableControl();
                        }
                        if (e.ColumnIndex == -1)
                        {
                            iColIndex = 0;
                        }
                        else
                        {
                            iColIndex = e.ColumnIndex;
                        }
                        if (dgwFormDetails.Columns[iColIndex].HeaderText == "Published")
                        {

                            if (strPublished == "0" || strPublished == "1")
                            {
                                IManageForms objFormDetail;
                                objFormDetail = (IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder");
                                DialogResult strResult;
                                strResult = MessageBox.Show("Do you want to Published this form", "Yes/No", MessageBoxButtons.YesNo);
                                if (strResult.ToString() == "Yes")
                                {
                                    string strValue;
                                    strValue = "2";
                                    int rowstate = objFormDetail.ResetFormStatus(GblIQCare.iFormBuilderId, strValue, GblIQCare.AppUserId);
                                    dgwFormDetails.DataSource = null;
                                    BindGrid();
                                }

                            }
                            else
                            {
                                IManageForms objFormDetail;
                                objFormDetail = (IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder");
                                DialogResult strResult;
                                strResult = MessageBox.Show("Do you want to unpublish this form?", "Yes/No", MessageBoxButtons.YesNo);
                                if (strResult.ToString() == "Yes")
                                {
                                    string strValue;
                                    strValue = "1";
                                    int rowstate = objFormDetail.ResetFormStatus(GblIQCare.iFormBuilderId, strValue, GblIQCare.AppUserId);
                                    dgwFormDetails.DataSource = null;
                                    BindGrid();
                                    EnableControl();

                                }

                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void cmbFormStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgwFormDetails.DataSource = null;
            BindGrid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                //strFormNameDb = "Patient Pharmacy";
                GblIQCare.iFormBuilderType = "Pharmacy";
                if (strFormNameDb == null)
                {
                    IQCareWindowMsgBox.ShowWindow("PMTCTSelectAtleastOne", this);
                    return;
                }
                else
                {
                    theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmPreview, IQCare.FormBuilder"));
                    theForm.MdiParent = this.MdiParent;
                    theForm.Left = 0;
                    theForm.Top = 0;
                    theForm.Show();
                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < objDsFormDetails.Tables[0].Rows.Count; i++)
                {
                    if ("Pharmacy" == objDsFormDetails.Tables[0].Rows[i]["FormName"].ToString())
                    {
                        if (objDsFormDetails.Tables[0].Rows[i]["Published"].ToString() == "2")
                        {
                            IQCareWindowMsgBox.ShowWindow("PMTCTPublishedForm", this);
                            return;
                        }
                        else
                        {
                            GblIQCare.iFormBuilderId = 72;
                            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmPatientPharmacyFormBuilder, IQCare.FormBuilder"));
                            theForm.MdiParent = this.MdiParent;
                            theForm.Left = 0;
                            theForm.Top = 0;
                            theForm.Show();
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GblIQCare.iFormBuilderId = 0;
            GblIQCare.iManageCareEnded = 3;
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmPatientPharmacyFormBuilder, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            this.Close();
            theForm.Show();
        }
        
    }
}
