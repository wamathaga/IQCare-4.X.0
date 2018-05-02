using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Interface.FormBuilder;
using Application.Common;
using Application.Presentation;

   namespace IQCare.FormBuilder
    {
    public partial class frmModule : Form
    {
        DataSet objDsModuleDetails = new DataSet();
        int intModuleId = 0;
        String Status = "";

      public frmModule()
        {
            InitializeComponent();
        }

        private void frmModule_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            BindGrid();

            if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageTechnicalArea, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnCreate.Enabled = false;
            }

            if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageTechnicalArea, FunctionAccess.Update, GblIQCare.dtUserRight) == false)
            {
                dgwModuleDetails.Enabled = false;
            }

            if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageTechnicalArea, FunctionAccess.Delete, GblIQCare.dtUserRight) == false)
            {
                btnCreate.Enabled = false;
            }

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            GblIQCare.UpdateFlag = 0;
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmModuleDetails, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            GblIQCare.ModuleId = 0;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
            this.Close();
        }

        private void BindGrid()
        {
            //Jayant - 01/05/2015
            IModule objModuleDetail;
            objModuleDetail = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule,BusinessProcess.FormBuilder");
            objDsModuleDetails = objModuleDetail.GetModuleDetail();
            if (objDsModuleDetails.Tables[0].Rows.Count > 0)
            {
                DataView theDVModule = new DataView(objDsModuleDetails.Tables[0]);
                theDVModule.RowFilter = "ModuleName <> 'RECORDS' AND ModuleName <> 'Pharmacy Dispense'";
                ShowGrid(theDVModule.ToTable());
            }
        }

        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwModuleDetails.DataSource = null;
                DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                col1.HeaderText = "Service";
                col1.DataPropertyName = "ModuleName";
                col1.Width = 200;
                col1.ReadOnly = true;

                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = "Patient Identifier";
                col2.DataPropertyName = "PatientIdentifier";
                col2.Width = 500;
                col2.ReadOnly = true;

                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col3.HeaderText = "Status";
                col3.DataPropertyName = "Status";
                col3.Width = 85;
                col3.ReadOnly = true;

                DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
                col4.HeaderText = "ModuleId";
                col4.DataPropertyName = "ModuleId";
                col4.Width = 0;
                col4.Visible = false;

                DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
                col5.HeaderText = "UpdateFlag";
                col5.DataPropertyName = "UpdateFlag";
                col5.Width = 0;
                col5.Visible = false;

                DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
                col6.HeaderText = "Identifier";
                col6.DataPropertyName = "Identifier";
                col6.Width = 0;
                col6.Visible = false;

                DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
                col7.HeaderText = "PharmacyFlag";
                col7.DataPropertyName = "PharmacyFlag";
                col7.Width = 0;
                col7.Visible = false;

                dgwModuleDetails.Columns.Add(col1);
                dgwModuleDetails.Columns.Add(col2);
                dgwModuleDetails.Columns.Add(col3);
                dgwModuleDetails.Columns.Add(col4);
                dgwModuleDetails.Columns.Add(col5);
                dgwModuleDetails.Columns.Add(col6);
                dgwModuleDetails.Columns.Add(col7);

                dgwModuleDetails.DataSource = theDT;

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void dgwModuleDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                DataTable tbl1 = (DataTable)dgwModuleDetails.DataSource;
                if (e.RowIndex > tbl1.Rows.Count)
                    return;
                GblIQCare.ModuleId = Convert.ToInt32(dgwModuleDetails.Rows[e.RowIndex].Cells[0].Value);
                GblIQCare.ModuleName = dgwModuleDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
                GblIQCare.UpdateFlag = Convert.ToInt32(dgwModuleDetails.Rows[e.RowIndex].Cells[4].Value);
                GblIQCare.Identifier = Convert.ToInt32(dgwModuleDetails.Rows[e.RowIndex].Cells[5].Value);
                GblIQCare.PharmacyFlag = Convert.ToInt32(dgwModuleDetails.Rows[e.RowIndex].Cells[6].Value);
                if (dgwModuleDetails.Rows[e.RowIndex].Cells[3].Value.ToString() == "Published")
                {
                    IQCareWindowMsgBox.ShowWindow("PublicTechenicalArea", this);
                    return;
                }

                Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmModuleDetails, IQCare.FormBuilder"));
                theForm.MdiParent = this.MdiParent;
                theForm.Left = 0;
                theForm.Top = 0;
                this.Close();
                theForm.Show();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgwModuleDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (GblIQCare.UpdateFlag == 1 || intModuleId == 3)
            {
                IQCareWindowMsgBox.ShowWindow("SelectedModule", this);
                return;
            }

            if (Status == "Published")
            {
                IQCareWindowMsgBox.ShowWindow("SelectedModule", this);
                return;
            }

            IModule objIdentifier;
            if (intModuleId != 0)
            {
                DialogResult strResult;
                strResult = MessageBox.Show("Do you want to delete this record ?", "IQCare Managemant", MessageBoxButtons.YesNo);
                if (strResult.ToString() == "Yes")
                {
                    objIdentifier = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");
                    objIdentifier.DeleteModule(intModuleId);
                    IQCareWindowMsgBox.ShowWindow("ModuleDelete", this);
                }
                BindGrid();
                return;
            }
         }

        private void dgwModuleDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int intUpdateflag;

            MsgBuilder theBuilder = new MsgBuilder();

            if (e.RowIndex != -1)
            {
                intModuleId = Convert.ToInt32(dgwModuleDetails.Rows[e.RowIndex].Cells[0].Value);
                GblIQCare.ModuleId = intModuleId;
                GblIQCare.UpdateFlag = Convert.ToInt32(dgwModuleDetails.Rows[e.RowIndex].Cells[4].Value);
                Status = Convert.ToString(dgwModuleDetails.Rows[e.RowIndex].Cells[3].Value);
            }

            if (e.ColumnIndex == -1)
            {
                return;
            }
            if (GblIQCare.UpdateFlag == 1)
            {
                return;
            }


            if (dgwModuleDetails.Columns[e.ColumnIndex].HeaderText == "Status")
            {

                if ((Status == "Published") || (Status == "Un-Published"))
                {
                    IModule objIdentifier;
                    DialogResult strResult;

                    if (Status == "Published")
                    {
                        theBuilder.DataElements["PgStatus"] = "UnPublish";
                    }
                    else
                    {
                        theBuilder.DataElements["PgStatus"] = "Publish";
                    }
                    strResult = IQCareWindowMsgBox.ShowWindowConfirm("Publish", theBuilder, this);

                    if (strResult == DialogResult.No)
                    {
                        return;
                    }

                    if (strResult.ToString() == "Yes")
                    {
                        if (Status == "Published")
                        {
                            Hashtable theHT = new Hashtable();
                            theHT.Add("ModuleID", intModuleId);
                            theHT.Add("Status", 1);
                            theHT.Add("DeleteFlag", 0);
                            theHT.Add("UserID", GblIQCare.AppUserId);
                            DataTable theDT = new DataTable();
                            objIdentifier = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");
                            Int32 intExistModuleId = objIdentifier.StatusUpdate(theHT);
                            BindGrid();

                        }
                        if (Status == "Un-Published")
                        {
                            if (objDsModuleDetails.Tables[0].Rows.Count > 0)
                            {
                                IQCareUtils Util=new IQCareUtils();
                                DataView theModDV = new DataView(objDsModuleDetails.Tables[0]);
                                theModDV.RowFilter = "ModuleId=" + intModuleId;
                                DataTable theModDT = Util.CreateTableFromDataView(theModDV);
                                if (theModDT.Rows[0]["PatientIdentifier"].ToString() != "")
                                {
                                    Hashtable theHT = new Hashtable();
                                    theHT.Add("ModuleID", intModuleId);
                                    theHT.Add("Status", 2);
                                    theHT.Add("DeleteFlag", 0);
                                    theHT.Add("UserID", GblIQCare.AppUserId);
                                    DataTable theDT = new DataTable();
                                    objIdentifier = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");
                                    Int32 intExistModuleId = objIdentifier.StatusUpdate(theHT);
                                    BindGrid();
                                }
                                else
                                {
                                    IQCareWindowMsgBox.ShowWindow("Link/Add atleast one Identifier with this Module", "", "", this);
                                    return;

                                }
                            }

                        }

                    }

                }

            }

       }

        private void dgwModuleDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            {
                try
                {
                    Color ClrRed = Color.FromArgb(204, 0, 0);
                    string strPubValue = "";
                    if (e.ColumnIndex != -1 && e.RowIndex != -1)
                    {
                        if (dgwModuleDetails.Columns[e.ColumnIndex].HeaderText.Equals("Status"))
                        {
                            if (e.Value == null || e.Value == "")
                            {
                                strPubValue = "0";
                            }
                            else
                            {
                                strPubValue = e.Value.ToString();
                            }
                        }
                        else if (dgwModuleDetails.Rows.Count >= 1)
                        {
                            if (dgwModuleDetails.Rows[e.RowIndex].Cells[4].Value == "")
                            {
                                strPubValue = "0";
                            }
                            else
                                strPubValue = dgwModuleDetails.Rows[e.RowIndex].Cells[4].Value.ToString();
                        }
                    }


                    switch (strPubValue)
                    {
                        case "Un-Published":
                            dgwModuleDetails.Rows[e.RowIndex].Cells[0].Style.BackColor = System.Drawing.Color.Red;
                            dgwModuleDetails.Rows[e.RowIndex].Cells[1].Style.BackColor = System.Drawing.Color.Red;
                            dgwModuleDetails.Rows[e.RowIndex].Cells[2].Style.BackColor = System.Drawing.Color.Red;
                            dgwModuleDetails.Rows[e.RowIndex].Cells[3].Style.BackColor = System.Drawing.Color.Red;
                            dgwModuleDetails.Rows[e.RowIndex].Cells[4].Style.BackColor = System.Drawing.Color.Red;
                            break;
                        case "Published":
                            dgwModuleDetails.Rows[e.RowIndex].Cells[0].Style.BackColor = System.Drawing.Color.Green;
                            dgwModuleDetails.Rows[e.RowIndex].Cells[1].Style.BackColor = System.Drawing.Color.Green;
                            dgwModuleDetails.Rows[e.RowIndex].Cells[2].Style.BackColor = System.Drawing.Color.Green;
                            dgwModuleDetails.Rows[e.RowIndex].Cells[3].Style.BackColor = System.Drawing.Color.Green;
                            dgwModuleDetails.Rows[e.RowIndex].Cells[4].Style.BackColor = System.Drawing.Color.Green;
                            break;
                    }
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
    }



        





