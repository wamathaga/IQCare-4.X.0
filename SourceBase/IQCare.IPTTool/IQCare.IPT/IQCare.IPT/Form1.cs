using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IQCare.IPT.Common;
using System.Configuration;
using Application.Common;
using Application.Presentation;
using System.Data.SqlClient;
using DataAccess.Base;

namespace IQCare.IPT
{
    public partial class frmIPTRapidTool : Form
    {
        DataSet moduleIdentifier = new DataSet();
        DataTable selectedServices = new DataTable();
        DataSet dsPatientDetails = new DataSet();
        int patientId = 0;
        string gOtherReasonDiscontinuedIPT = String.Empty, gOtherReasonDeclinedIPT = String.Empty;
        public frmIPTRapidTool()
        {
            InitializeComponent();
            LoadDetails();
        }

        private void LoadDetails()
        {
            try
            {
                object conn;

                //SqlConnection cnn = null;
                //cnn = (SqlConnection)DatatTableUtil.CheckConnection();
                //if (cnn.State == ConnectionState.Open)
                //{
                IIPTDetails iiptdetails;
                DataSet ds = new DataSet();
                iiptdetails = new BIPTDetails();
                ds = iiptdetails.GetInitialDetails(
                    Convert.ToInt32(ConfigurationSettings.AppSettings["AppLocationId"].ToString()));

                moduleIdentifier = ds;
                BindDetails(ds);
                //}
                //else
                //{
                //    MessageBox.Show("Database is not available, please check your connection string.", "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
        }

        private void BindDetails(DataSet ds)
        {
            try
            {
                var exceptionList = new List<int> { 206, 207, 7 };
                IEnumerable<DataRow> filterdata = (from inv in ds.Tables[0].AsEnumerable()
                                                   where !exceptionList.Contains(inv.Field<int>("ModuleID"))
                                                   select inv);

                DataTable dt = filterdata.CopyToDataTable();
                selectedServices = dt;

                var query1 = (from inv in selectedServices.AsEnumerable()
                              select new
                                      {
                                          ModuleID = inv["ModuleID"],
                                          ModuleName = inv["ModuleName"]
                                      }).Distinct();

                DataTable dtService = DatatTableUtil.LINQResultToDataTable(query1);


                cbService.DisplayMember = "ModuleName";
                cbService.ValueMember = "ModuleID";
                cbService.DataSource = dtService;

                //DatatTableUtil.BindCombo(cbService, dtService, "ModuleName", "ModuleID");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
        }

        private void cbService_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            int selectedIndex = cmb.SelectedIndex;
            int selectedValue = (int)cmb.SelectedValue;

            var exceptionList = new List<int> { 206, 207, 7 };
            var query = (from inv in selectedServices.AsEnumerable()
                         where inv.Field<int>("ModuleID") == selectedValue
                         select new
                                 {
                                     IdentifierID = inv["IdentifierID"],
                                     IdentifierName = inv["IdentifierName"]
                                 });

            DataTable dt = DatatTableUtil.LINQResultToDataTable(query);

            DataRow theDR = dt.NewRow();
            theDR["IdentifierName"] = "All";
            theDR["IdentifierID"] = 9999;
            dt.Rows.InsertAt(theDR, 0);
            theDR = dt.NewRow();
            theDR["IdentifierName"] = "IQNumber";
            theDR["IdentifierID"] = 10000;
            dt.Rows.InsertAt(theDR, 1);

            cbIdentifier.DataSource = null;
            cbIdentifier.Items.Clear();

            cbIdentifier.DisplayMember = "IdentifierName";
            cbIdentifier.ValueMember = "IdentifierID";
            cbIdentifier.DataSource = dt;

            //DatatTableUtil.BindCombo(cbIdentifier, dt, "IdentifierName", "IdentifierID");
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                List<Patient> patients = new List<Patient>();
                dgvPatients.DataSource = patients;
                IIPTDetails iiptdetails;
                DataSet ds = new DataSet();
                iiptdetails = new BIPTDetails();
                ds = iiptdetails.GetPatientDetails(Convert.ToInt32(ConfigurationSettings.AppSettings["AppLocationId"].ToString())
                                                    , Convert.ToInt32(this.cbService.SelectedValue)
                                                    , this.cbIdentifier.SelectedValue.ToString()
                                                    , this.textBox1.Text);

                //Convert.ToInt32(ConfigurationSettings.AppSettings["AppLocationId"].ToString()));

                //moduleIdentifier = ds;
                //BindDetails(ds);
                patients = ds.Tables[0].ToList<Patient>();

                dgvPatients.DataSource = patients;

                // Resize the DataGridView columns to fit the newly loaded content.
                dgvPatients.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                //dgvPatients.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
        }

        private void dgvPatients_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dgvPatients.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                //DataRow myRow = (row.DataBoundItem as DataRowView).Row;
                // Do something with your DataRow

                ResetControls();

                dsPatientDetails = new DataSet();

                Patient cust = row.DataBoundItem as Patient;

                GetPatientIPTDetails(cust);
            }
        }

        private bool ValidatePatient(DataSet ds, int patientId)
        {
            bool result = true;
            int eligibleforIPT = 0;
            string tbFindings = string.Empty;

            if (ds.Tables[2].Rows.Count > 0)
            {
                IEnumerable<DataRow> filterdata1 =
                   (from fdata in ds.Tables[2].AsEnumerable()
                    where fdata.Field<Int32>("Ptn_Pk") == patientId
                    select fdata);

                foreach (DataRow drow in filterdata1)
                {
                    if (!DBNull.Value.Equals(drow["TBFindings"]))
                    {
                        tbFindings = drow["TBFindings"].ToString();
                    }
                    if (!DBNull.Value.Equals(drow["EligibleForIPT"]))
                    {
                        eligibleforIPT = Convert.ToInt32(drow["EligibleForIPT"].ToString());
                    }
                }
                //if (eligibleforIPT == 1)
                //{
                DataView theCodeDV = new DataView(ds.Tables[0]);
                theCodeDV.RowFilter = "CodeName in ('TB Status','TBFindings') and DeCodeId = '" + tbFindings + "'";
                DataTable theCodeDT = (DataTable)DatatTableUtil.CreateTableFromDataView(theCodeDV);
                string strTbFinding = theCodeDT.AsEnumerable().FirstOrDefault().Field<string>("DeCodeName").ToString();
                //if (GetTBStatusName(strTbFinding) == "Negative TB Screen")
                //{
                IEnumerable<DataRow> drTBbAssessment = (from fdata in ds.Tables[1].AsEnumerable()
                                                        where fdata.Field<Int32>("Ptn_Pk") == patientId
                                                        select fdata);
                bool isNoneExits = false;
                foreach (DataRow drow in drTBbAssessment)
                {
                    if (!DBNull.Value.Equals(drow["NAME"]))
                    {
                        if (drow["NAME"].ToString().ToLower() == "none")
                        {
                            isNoneExits = true;
                        }

                    }
                }

                //if (!isNoneExits)
                //{
                //    result = false;
                //}
                //}
                //else
                //{
                //    result = false;
                //}
                //}
                //else
                //{
                //    result = false;
                //}
            }
            else
            {
                result = false;
            }
            return result;
        }

        private void ResetControls()
        {
            this.txtTBFinding.Text = "";
            this.dtIPTStartDate.Value = DateTime.Now;
            this.dtIPTEndDate.Value = DateTime.Now;

            this.chkEligibleForIPT.Checked = false;
            this.cbIPTContraindication.SelectedValue = 0;
            this.cbIPTDiscontinued.SelectedValue = 0;

            this.dtIPTEndDate.Enabled = false;
            this.cbIPTContraindication.Enabled = false;
            this.cbIPTDiscontinued.Enabled = false;
            this.btnSave.Enabled = true;

            this.rbCompletedIPT.Checked = false;
            this.rbDeclinedIpt.Checked = false;
            this.rbDiscontinued.Checked = false;
            this.txtOtherReasonContraindication.Text = "";
            this.txtOtherReasonDiscontinued.Text = "";

        }

        private void GetPatientIPTDetails(Patient cust)
        {
            try
            {
                IIPTDetails iiptdetails;
                DataSet ds = new DataSet();
                iiptdetails = new BIPTDetails();
                patientId = cust.PatientId;
                ds = iiptdetails.GetPatientIPTDetails(Convert.ToInt32(ConfigurationSettings.AppSettings["AppLocationId"].ToString())
                    , cust.PatientId);



                #region DataManupulation
                dsPatientDetails = ds;

                IEnumerable<DataRow> drTBbAssessment = (from fdata in ds.Tables[1].AsEnumerable()
                                                        where fdata.Field<Int32>("Ptn_Pk") == cust.PatientId
                                                        select fdata);



                List<CodeDeCodeTables> iptadherence = new List<CodeDeCodeTables>();
                List<CodeDeCodeTables> iptcontraindication = new List<CodeDeCodeTables>();
                List<CodeDeCodeTables> iptdiscontinued = new List<CodeDeCodeTables>();
                List<CodeDeCodeTables> tbstatus = new List<CodeDeCodeTables>();

                iptadherence = (from dt in ds.Tables[0].AsEnumerable().Where(o => o.Field<string>("CodeName") == "IPTAdherence").ToList()
                                select new CodeDeCodeTables()
                                {
                                    CodeId = dt.Field<int>("CodeId"),
                                    CodeName = dt.Field<string>("CodeName"),
                                    DeCodeId = dt.Field<int>("DeCodeId"),
                                    DeCodeName = dt.Field<string>("DeCodeName")
                                }).ToList();

                iptcontraindication = (from dt in ds.Tables[0].AsEnumerable().Where(o => o.Field<string>("CodeName") == "IPTContraindication").ToList()
                                       select new CodeDeCodeTables()
                                       {
                                           CodeId = dt.Field<int>("CodeId"),
                                           CodeName = dt.Field<string>("CodeName"),
                                           DeCodeId = dt.Field<int>("DeCodeId"),
                                           DeCodeName = dt.Field<string>("DeCodeName")
                                       }).ToList();

                iptdiscontinued = (from dt in ds.Tables[0].AsEnumerable().Where(o => o.Field<string>("CodeName") == "IPTDiscontinued").ToList()
                                   select new CodeDeCodeTables()
                                   {
                                       CodeId = dt.Field<int>("CodeId"),
                                       CodeName = dt.Field<string>("CodeName"),
                                       DeCodeId = dt.Field<int>("DeCodeId"),
                                       DeCodeName = dt.Field<string>("DeCodeName")
                                   }).ToList();


                tbstatus = (from dt in ds.Tables[0].AsEnumerable().Where(o => o.Field<string>("CodeName") == "TB Status").ToList()
                            select new CodeDeCodeTables()
                            {
                                CodeId = dt.Field<int>("CodeId"),
                                CodeName = dt.Field<string>("CodeName"),
                                DeCodeId = dt.Field<int>("DeCodeId"),
                                DeCodeName = dt.Field<string>("DeCodeName")
                            }).ToList();

                DataTable dtResult = DatatTableUtil.LINQResultToDataTable(iptcontraindication);
                DataRow theDR = dtResult.NewRow();
                theDR["DeCodeName"] = "Select";
                theDR["DeCodeId"] = 0;
                dtResult.Rows.InsertAt(theDR, 0);

                cbIPTContraindication.DataSource = null;
                cbIPTContraindication.Items.Clear();

                cbIPTContraindication.DisplayMember = "DeCodeName";
                cbIPTContraindication.ValueMember = "DeCodeId";
                cbIPTContraindication.DataSource = dtResult;

                dtResult = DatatTableUtil.LINQResultToDataTable(iptcontraindication);
                DataRow theDR1 = dtResult.NewRow();
                theDR1["DeCodeName"] = "Select";
                theDR1["DeCodeId"] = 0;
                dtResult.Rows.InsertAt(theDR1, 0);

                //cbIPTContraindication.DataSource = null;
                //cbIPTContraindication.Items.Clear();

                //cbIPTContraindication.DisplayMember = "DeCodeName";
                //cbIPTContraindication.ValueMember = "DeCodeId";
                //cbIPTContraindication.DataSource = dtResult;

                dtResult = DatatTableUtil.LINQResultToDataTable(iptdiscontinued);
                DataRow theDR2 = dtResult.NewRow();
                theDR2["DeCodeName"] = "Select";
                theDR2["DeCodeId"] = 0;
                dtResult.Rows.InsertAt(theDR2, 0);

                cbIPTDiscontinued.DataSource = null;
                cbIPTDiscontinued.Items.Clear();

                cbIPTDiscontinued.DisplayMember = "DeCodeName";
                cbIPTDiscontinued.ValueMember = "DeCodeId";
                cbIPTDiscontinued.DataSource = dtResult;

                this.cbIPTContraindication.SelectedValue = 0;

                this.cbIPTDiscontinued.SelectedValue = 0;

                string tbFindings = string.Empty;
                string dtStartDate = string.Empty;
                string dtEndDate = string.Empty;
                int eligibleforIPT = 0;
                this.chkEligibleForIPT.Checked = false;
                int ddlIPTContraindication = 0, ddlIPTAdherence = 0, ddlIPTDiscontinued = 0;
                string strIPT = string.Empty, otherReasonDiscontinuedIPT = String.Empty,otherReasonDeclinedIPT = String.Empty;

                #endregion

                #region Data Extract
                IEnumerable<DataRow> filterdata1 =
                (from fdata in ds.Tables[2].AsEnumerable()
                 where fdata.Field<Int32>("Ptn_Pk") == cust.PatientId
                 select fdata).OrderBy(o => o.Field<DateTime>("CreateDate"));

                foreach (DataRow drow in filterdata1)
                {
                    if (!DBNull.Value.Equals(drow["TBFindings"]))
                    {
                        tbFindings = drow["TBFindings"].ToString();
                    }
                    if (!DBNull.Value.Equals(drow["INHStartDate"]))
                    {
                        dtStartDate = DateTime.Parse(drow["INHStartDate"].ToString()).ToString();
                    }
                    if (!DBNull.Value.Equals(drow["INHEndDate"]))
                    {
                        dtEndDate = DateTime.Parse(drow["INHEndDate"].ToString()).ToString();
                    }
                    if (!DBNull.Value.Equals(drow["EligibleForIPT"]))
                    {
                        eligibleforIPT = Convert.ToInt32(drow["EligibleForIPT"].ToString());
                    }

                    if (!DBNull.Value.Equals(drow["IPTAdherence"]))
                    {
                        ddlIPTAdherence = Convert.ToInt32(drow["IPTAdherence"].ToString());
                    }

                    if (!DBNull.Value.Equals(drow["IPTContraindication"]))
                    {
                        ddlIPTContraindication = Convert.ToInt32(drow["IPTContraindication"].ToString());
                    }

                    if (!DBNull.Value.Equals(drow["IPTDiscontinued"]))
                    {
                        ddlIPTDiscontinued = Convert.ToInt32(drow["IPTDiscontinued"].ToString());
                    }
                    if (!DBNull.Value.Equals(drow["IPT"]))
                    {
                        strIPT = drow["IPT"].ToString();
                    }
                    if (!DBNull.Value.Equals(drow["OtherReasonDiscontinuedIPT"]))
                    {
                        otherReasonDiscontinuedIPT = drow["OtherReasonDiscontinuedIPT"].ToString();
                    }
                    if (!DBNull.Value.Equals(drow["OtherReasonDeclinedIPT"]))
                    {
                        otherReasonDeclinedIPT = drow["OtherReasonDeclinedIPT"].ToString();
                    }

                }

                #endregion

                #region Assign data

                String[] names = drTBbAssessment.Select(o => o["NAME"].ToString()).ToArray();
                this.txtTBAssessment.Text = string.Join(",", names);


                if (!string.IsNullOrEmpty(tbFindings))
                {

                    DataView theCodeDV = new DataView(ds.Tables[0]);
                    theCodeDV.RowFilter = "CodeName in ('TB Status','TBFindings') and DeCodeId = '" + tbFindings + "'";
                    DataTable theCodeDT = (DataTable)DatatTableUtil.CreateTableFromDataView(theCodeDV);

                    //string strTbFinding = tbstatus.Where(o => o.DeCodeId == Convert.ToInt32(tbFindings)).FirstOrDefault().DeCodeName;
                    string strTbFinding = theCodeDT.AsEnumerable().FirstOrDefault().Field<string>("DeCodeName").ToString();
                    this.txtTBFinding.Text = GetTBStatusName(strTbFinding);
                }

                if (eligibleforIPT == 1)
                {
                    this.chkEligibleForIPT.Checked = true;
                }

                this.dtIPTEndDate.Enabled = false;
                this.cbIPTContraindication.Enabled = false;
                //this.cbIPTContraindication.Enabled = false;
                this.cbIPTDiscontinued.Enabled = false;
                this.chkEligibleForIPT.Enabled = false;
                this.rbCompletedIPT.Enabled = false;
                this.rbDeclinedIpt.Enabled = false;
                this.rbDiscontinued.Enabled = false;

                //if (ValidatePatient(ds, cust.PatientId))
                //{
                this.chkEligibleForIPT.Enabled = false;
                this.rbCompletedIPT.Enabled = true;
                this.rbDeclinedIpt.Enabled = true;
                this.rbDiscontinued.Enabled = true;

                if (!string.IsNullOrEmpty(dtStartDate))
                {
                    this.dtIPTStartDate.Format = DateTimePickerFormat.Custom;
                    this.dtIPTStartDate.CustomFormat = "dd-MMM-yyyy";
                    this.dtIPTStartDate.Value = DateTime.Parse(dtStartDate.ToString());
                }

                if (!string.IsNullOrEmpty(dtEndDate))
                {
                    this.dtIPTEndDate.Format = DateTimePickerFormat.Custom;
                    this.dtIPTEndDate.CustomFormat = "dd-MMM-yyyy";
                    this.dtIPTEndDate.Value = DateTime.Parse(dtEndDate.ToString());
                }


                this.cbIPTContraindication.SelectedValue = ddlIPTContraindication;
                this.cbIPTDiscontinued.SelectedValue = ddlIPTDiscontinued;

                if (ddlIPTContraindication > 0)
                {
                    this.txtOtherReasonContraindication.Text = otherReasonDeclinedIPT;
                }

                if (ddlIPTDiscontinued > 0)
                {
                    this.txtOtherReasonDiscontinued.Text = otherReasonDiscontinuedIPT;
                }
                
                bool isValidForUpdate = true;
                if (!string.IsNullOrEmpty(strIPT))
                {

                    DataView theCodeDV = new DataView(ds.Tables[0]);
                    theCodeDV.RowFilter = "CodeName in ('IPT') and DeCodeId = '" + strIPT + "'";
                    DataTable theCodeDT = (DataTable)DatatTableUtil.CreateTableFromDataView(theCodeDV);
                    if (theCodeDT.Rows.Count > 0)
                    {
                        string str = theCodeDT.AsEnumerable().FirstOrDefault().Field<string>("DeCodeName").ToString();

                        switch (str.Trim().ToLower())
                        {
                            case "completed ipt":
                                this.rbCompletedIPT.Checked = true;
                                this.dtIPTEndDate.Enabled = true;
                                isValidForUpdate = false;
                                break;
                            case "discontinued":
                                this.rbDiscontinued.Checked = true;
                                this.dtIPTEndDate.Enabled = true;
                                this.cbIPTDiscontinued.Enabled = true;
                                isValidForUpdate = false;
                                break;
                            case "declined ipt":
                                this.rbDeclinedIpt.Checked = true;
                                this.cbIPTContraindication.Enabled = true;
                                isValidForUpdate = false;
                                break;

                        }
                    }
                }


                if (!string.IsNullOrEmpty(dtStartDate.ToString()) &&
                    !string.IsNullOrEmpty(dtEndDate.ToString()))
                {
                    if (!isValidForUpdate)
                    {
                        MessageBox.Show("Patient is not eligible for IPT update.", "IQCare.IPT", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        this.btnSave.Enabled = false;
                    }
                }
                else
                {
                    this.btnSave.Enabled = true;

                }


                //this.dtIPTEndDate.SelectedDate = DateTime.Parse(inMyString);
                //}

                //else
                //{
                //    MessageBox.Show("Patient is not eligible for IPT update.", "IQCare.IPT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    this.btnSave.Enabled = false;
                //}
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
        }

        #region Old Code
        //private void GetPatientIPTDetails_old(Patient cust)
        //{
        //    try
        //    {
        //        IIPTDetails iiptdetails;
        //        DataSet ds = new DataSet();
        //        iiptdetails = new BIPTDetails();
        //        patientId = cust.PatientId;
        //        ds = iiptdetails.GetPatientIPTDetails(Convert.ToInt32(ConfigurationSettings.AppSettings["AppLocationId"].ToString())
        //                                            , cust.PatientId);



        //        #region DataManupulation
        //        dsPatientDetails = ds;

        //        IEnumerable<DataRow> drTBbAssessment = (from fdata in ds.Tables[1].AsEnumerable()
        //                                                where fdata.Field<Int32>("Ptn_Pk") == cust.PatientId
        //                                                select fdata);



        //        List<CodeDeCodeTables> iptadherence = new List<CodeDeCodeTables>();
        //        List<CodeDeCodeTables> iptcontraindication = new List<CodeDeCodeTables>();
        //        List<CodeDeCodeTables> iptdiscontinued = new List<CodeDeCodeTables>();
        //        List<CodeDeCodeTables> tbstatus = new List<CodeDeCodeTables>();

        //        iptadherence = (from dt in ds.Tables[0].AsEnumerable().Where(o => o.Field<string>("CodeName") == "IPTAdherence").ToList()
        //                        select new CodeDeCodeTables()
        //                        {
        //                            CodeId = dt.Field<int>("CodeId"),
        //                            CodeName = dt.Field<string>("CodeName"),
        //                            DeCodeId = dt.Field<int>("DeCodeId"),
        //                            DeCodeName = dt.Field<string>("DeCodeName")
        //                        }).ToList();

        //        iptcontraindication = (from dt in ds.Tables[0].AsEnumerable().Where(o => o.Field<string>("CodeName") == "IPTContraindication").ToList()
        //                               select new CodeDeCodeTables()
        //                               {
        //                                   CodeId = dt.Field<int>("CodeId"),
        //                                   CodeName = dt.Field<string>("CodeName"),
        //                                   DeCodeId = dt.Field<int>("DeCodeId"),
        //                                   DeCodeName = dt.Field<string>("DeCodeName")
        //                               }).ToList();

        //        iptdiscontinued = (from dt in ds.Tables[0].AsEnumerable().Where(o => o.Field<string>("CodeName") == "IPTDiscontinued").ToList()
        //                           select new CodeDeCodeTables()
        //                           {
        //                               CodeId = dt.Field<int>("CodeId"),
        //                               CodeName = dt.Field<string>("CodeName"),
        //                               DeCodeId = dt.Field<int>("DeCodeId"),
        //                               DeCodeName = dt.Field<string>("DeCodeName")
        //                           }).ToList();


        //        tbstatus = (from dt in ds.Tables[0].AsEnumerable().Where(o => o.Field<string>("CodeName") == "TB Status").ToList()
        //                    select new CodeDeCodeTables()
        //                    {
        //                        CodeId = dt.Field<int>("CodeId"),
        //                        CodeName = dt.Field<string>("CodeName"),
        //                        DeCodeId = dt.Field<int>("DeCodeId"),
        //                        DeCodeName = dt.Field<string>("DeCodeName")
        //                    }).ToList();

        //        DataTable dtResult = DatatTableUtil.LINQResultToDataTable(iptcontraindication);
        //        DataRow theDR = dtResult.NewRow();
        //        theDR["DeCodeName"] = "Select";
        //        theDR["DeCodeId"] = 0;
        //        dtResult.Rows.InsertAt(theDR, 0);

        //        cbIPTContraindication.DataSource = null;
        //        cbIPTContraindication.Items.Clear();

        //        cbIPTContraindication.DisplayMember = "DeCodeName";
        //        cbIPTContraindication.ValueMember = "DeCodeId";
        //        cbIPTContraindication.DataSource = dtResult;

        //        dtResult = DatatTableUtil.LINQResultToDataTable(iptcontraindication);
        //        DataRow theDR1 = dtResult.NewRow();
        //        theDR1["DeCodeName"] = "Select";
        //        theDR1["DeCodeId"] = 0;
        //        dtResult.Rows.InsertAt(theDR1, 0);

        //        //cbIPTContraindication.DataSource = null;
        //        //cbIPTContraindication.Items.Clear();

        //        //cbIPTContraindication.DisplayMember = "DeCodeName";
        //        //cbIPTContraindication.ValueMember = "DeCodeId";
        //        //cbIPTContraindication.DataSource = dtResult;

        //        dtResult = DatatTableUtil.LINQResultToDataTable(iptdiscontinued);
        //        DataRow theDR2 = dtResult.NewRow();
        //        theDR2["DeCodeName"] = "Select";
        //        theDR2["DeCodeId"] = 0;
        //        dtResult.Rows.InsertAt(theDR2, 0);

        //        cbIPTDiscontinued.DataSource = null;
        //        cbIPTDiscontinued.Items.Clear();

        //        cbIPTDiscontinued.DisplayMember = "DeCodeName";
        //        cbIPTDiscontinued.ValueMember = "DeCodeId";
        //        cbIPTDiscontinued.DataSource = dtResult;

        //        string tbFindings = string.Empty;
        //        string dtStartDate = string.Empty;
        //        string dtEndDate = string.Empty;
        //        int eligibleforIPT = 0;
        //        this.chkEligibleForIPT.Checked = false;
        //        int ddlIPTContraindication = 0, ddlIPTAdherence = 0, ddlIPTDiscontinued = 0;
        //        string strIPT = string.Empty;

        //        #endregion

        //        if (ds.Tables[2].Rows.Count > 0)
        //        {
        //            #region Data Extract
        //            IEnumerable<DataRow> filterdata1 =
        //               (from fdata in ds.Tables[2].AsEnumerable()
        //                where fdata.Field<Int32>("Ptn_Pk") == cust.PatientId
        //                select fdata);

        //            foreach (DataRow drow in filterdata1)
        //            {
        //                if (!DBNull.Value.Equals(drow["TBFindings"]))
        //                {
        //                    tbFindings = drow["TBFindings"].ToString();
        //                }
        //                if (!DBNull.Value.Equals(drow["INHStartDate"]))
        //                {
        //                    dtStartDate = DateTime.Parse(drow["INHStartDate"].ToString()).ToString();
        //                }
        //                if (!DBNull.Value.Equals(drow["INHEndDate"]))
        //                {
        //                    dtEndDate = DateTime.Parse(drow["INHEndDate"].ToString()).ToString();
        //                }
        //                if (!DBNull.Value.Equals(drow["EligibleForIPT"]))
        //                {
        //                    eligibleforIPT = Convert.ToInt32(drow["EligibleForIPT"].ToString());
        //                }

        //                if (!DBNull.Value.Equals(drow["IPTAdherence"]))
        //                {
        //                    ddlIPTAdherence = Convert.ToInt32(drow["IPTAdherence"].ToString());
        //                }

        //                if (!DBNull.Value.Equals(drow["IPTContraindication"]))
        //                {
        //                    ddlIPTContraindication = Convert.ToInt32(drow["IPTContraindication"].ToString());
        //                }

        //                if (!DBNull.Value.Equals(drow["IPTDiscontinued"]))
        //                {
        //                    ddlIPTDiscontinued = Convert.ToInt32(drow["IPTDiscontinued"].ToString());
        //                }
        //                if (!DBNull.Value.Equals(drow["IPT"]))
        //                {
        //                    strIPT = drow["IPT"].ToString();
        //                }

        //            }

        //            #endregion

        //            #region Assign data

        //            String[] names = drTBbAssessment.Select(o => o["NAME"].ToString()).ToArray();
        //            this.txtTBAssessment.Text = string.Join(",", names);


        //            if (!string.IsNullOrEmpty(tbFindings))
        //            {

        //                DataView theCodeDV = new DataView(ds.Tables[0]);
        //                theCodeDV.RowFilter = "CodeName in ('TB Status','TBFindings') and DeCodeId = '" + tbFindings + "'";
        //                DataTable theCodeDT = (DataTable)DatatTableUtil.CreateTableFromDataView(theCodeDV);

        //                //string strTbFinding = tbstatus.Where(o => o.DeCodeId == Convert.ToInt32(tbFindings)).FirstOrDefault().DeCodeName;
        //                string strTbFinding = theCodeDT.AsEnumerable().FirstOrDefault().Field<string>("DeCodeName").ToString();
        //                this.txtTBFinding.Text = GetTBStatusName(strTbFinding);
        //            }

        //            if (eligibleforIPT == 1)
        //            {
        //                this.chkEligibleForIPT.Checked = true;
        //            }

        //            this.dtIPTEndDate.Enabled = false;
        //            this.cbIPTContraindication.Enabled = false;
        //            //this.cbIPTContraindication.Enabled = false;
        //            this.cbIPTDiscontinued.Enabled = false;
        //            this.chkEligibleForIPT.Enabled = false;
        //            this.rbCompletedIPT.Enabled = false;
        //            this.rbDeclinedIpt.Enabled = false;
        //            this.rbDiscontinued.Enabled = false;

        //            if (ValidatePatient(ds, cust.PatientId))
        //            {
        //                this.chkEligibleForIPT.Enabled = false;
        //                this.rbCompletedIPT.Enabled = true;
        //                this.rbDeclinedIpt.Enabled = true;
        //                this.rbDiscontinued.Enabled = true;

        //                if (!string.IsNullOrEmpty(dtStartDate))
        //                {
        //                    this.dtIPTStartDate.Format = DateTimePickerFormat.Custom;
        //                    this.dtIPTStartDate.CustomFormat = "dd-MMM-yyyy";
        //                    this.dtIPTStartDate.Value = DateTime.Parse(dtStartDate.ToString());
        //                }

        //                if (!string.IsNullOrEmpty(dtEndDate))
        //                {
        //                    this.dtIPTEndDate.Format = DateTimePickerFormat.Custom;
        //                    this.dtIPTEndDate.CustomFormat = "dd-MMM-yyyy";
        //                    this.dtIPTEndDate.Value = DateTime.Parse(dtEndDate.ToString());
        //                }


        //                this.cbIPTContraindication.SelectedValue = ddlIPTAdherence;
        //                //this.cbIPTContraindication.SelectedValue = ddlIPTContraindication;
        //                this.cbIPTDiscontinued.SelectedValue = ddlIPTDiscontinued;

        //                if (!string.IsNullOrEmpty(strIPT))
        //                {

        //                    DataView theCodeDV = new DataView(ds.Tables[0]);
        //                    theCodeDV.RowFilter = "CodeName in ('IPT') and DeCodeId = '" + strIPT + "'";
        //                    DataTable theCodeDT = (DataTable)DatatTableUtil.CreateTableFromDataView(theCodeDV);
        //                    string str = theCodeDT.AsEnumerable().FirstOrDefault().Field<string>("DeCodeName").ToString();

        //                    switch (str.Trim().ToLower())
        //                    {
        //                        case "completed ipt":
        //                            this.rbCompletedIPT.Checked = true;
        //                            this.dtIPTEndDate.Enabled = true;
        //                            break;
        //                        case "discontinued":
        //                            this.rbDiscontinued.Checked = true;
        //                            this.dtIPTEndDate.Enabled = true;
        //                            this.cbIPTDiscontinued.Enabled = true;
        //                            break;
        //                        case "declined ipt":
        //                            this.rbDeclinedIpt.Checked = true;
        //                            this.cbIPTContraindication.Enabled = true;
        //                            break;

        //                    }
        //                }

        //                //this.dtIPTEndDate.SelectedDate = DateTime.Parse(inMyString);
        //            }

        //            else
        //            {
        //                MessageBox.Show("Patient is not eligible for IPT update.", "IQCare.IPT", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                this.btnSave.Enabled = false;
        //            }
        //            #endregion
        //        }
        //        else
        //        {
        //            MessageBox.Show("Patient is not eligible for IPT update.", "IQCare.IPT", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            this.btnSave.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
        //    }
        //}
        #endregion

        private string GetTBStatusName(string name)
        {
            string strName;

            switch (name.Trim().ToLower())
            {
                case "no signs":
                    strName = "Negative TB Screen";
                    break;
                case "suspect":
                    strName = "Presumptive TB";
                    break;
                case "tb rx":
                    strName = "Client on TB Treatment";
                    break;
                default:
                    strName = name;
                    break;
            }

            return strName;
        }

        private void rbCompletedIPT_CheckedChanged(object sender, EventArgs e)
        {
            this.dtIPTEndDate.Enabled = true;
            this.cbIPTContraindication.Enabled = false;
            this.cbIPTDiscontinued.Enabled = false;

        }

        private void rbDiscontinued_CheckedChanged(object sender, EventArgs e)
        {
            this.dtIPTEndDate.Enabled = true;
            this.cbIPTContraindication.Enabled = false;
            this.cbIPTDiscontinued.Enabled = true;

        }

        private void rbDefaulted_CheckedChanged(object sender, EventArgs e)
        {
            this.dtIPTEndDate.Enabled = false;
            this.cbIPTContraindication.Enabled = true;
            this.cbIPTDiscontinued.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = (DataSet)dsPatientDetails;

                string intIPT = "0";
                string decodeName = string.Empty;
                DateTime? dtIPTEndDate = null;
                DateTime? dtIPTStartDate = null;

                string ddlIPTContraindication = string.Empty, ddlIPTAdherence = string.Empty, ddlIPTDiscontinued = string.Empty;
                DataView theCodeDV = new DataView(ds.Tables[0]);

                DataView dvPatient = new DataView(ds.Tables[2]);
                dvPatient.RowFilter = "Ptn_Pk = '" + patientId + "'";
                DataTable dtPatient = (DataTable)DatatTableUtil.CreateTableFromDataView(dvPatient);
                bool validateFlag = true;
                //ddlIPTAdherence = this.cbIPTContraindication.SelectedValue.ToString();
                ddlIPTContraindication = this.cbIPTContraindication.SelectedValue.ToString();
                ddlIPTDiscontinued = this.cbIPTDiscontinued.SelectedValue.ToString();

                if (this.rbDiscontinued.Checked)
                {
                    if (this.cbIPTDiscontinued.SelectedValue.ToString() == "0")
                    {
                        validateFlag = false;
                    }
                }
                else if (this.rbDeclinedIpt.Checked)
                {
                    if (this.cbIPTContraindication.SelectedValue.ToString() == "0")
                    {
                        validateFlag = false;
                    }
                }

                if (validateFlag)
                {

                    if (this.rbCompletedIPT.Checked)
                    {
                        decodeName = "Completed IPT";
                    }
                    else if (this.rbDiscontinued.Checked)
                    {
                        decodeName = "Discontinued";
                    }
                    else if (this.rbDeclinedIpt.Checked)
                    {
                        decodeName = "Declined IPT";
                    }

                    if (string.IsNullOrEmpty(decodeName))
                    {
                        decodeName = "Start IPT";
                    }

                    theCodeDV.RowFilter = "CodeName in ('IPT') and DeCodeName = '" + decodeName + "'";
                    DataTable theCodeDT = (DataTable)DatatTableUtil.CreateTableFromDataView(theCodeDV);

                    intIPT = theCodeDT.AsEnumerable().FirstOrDefault().Field<int>("DeCodeId").ToString();


                    if (this.rbCompletedIPT.Checked || this.rbDiscontinued.Checked)
                    {
                        dtIPTEndDate = this.dtIPTEndDate.Value;
                    }
                    dtIPTStartDate = this.dtIPTStartDate.Value;
                    int visitId = 0;
                    if (dtPatient.Rows.Count > 0)
                    {
                        visitId = Convert.ToInt32(dtPatient.AsEnumerable().FirstOrDefault().Field<int>("Visit_Pk")
                            .ToString());
                    }
                    IIPTDetails iiptdetails;
                    iiptdetails = new BIPTDetails();
                    DataTable dt =
                        iiptdetails.SavePatientIPTDetails(
                            Convert.ToInt32(ConfigurationSettings.AppSettings["AppLocationId"].ToString())
                            , patientId
                            , visitId
                            , intIPT
                            , string.Empty
                            , ddlIPTContraindication
                            , ddlIPTDiscontinued
                            , dtIPTEndDate
                            , dtIPTStartDate
                            , 1
                            ,this.txtOtherReasonContraindication.Text
                            ,this.txtOtherReasonDiscontinued.Text
                        );

                    MessageBox.Show("IPT details saved successfully.", "IQCare", MessageBoxButtons.OK);

                    DataGridViewSelectedRowCollection rows = dgvPatients.SelectedRows;
                    foreach (DataGridViewRow row in rows)
                    {
                        ResetControls();

                        dsPatientDetails = new DataSet();

                        Patient cust = row.DataBoundItem as Patient;

                        GetPatientIPTDetails(cust);
                    }
                }
                else
                {
                    MessageBox.Show("Please select an option from the list.", "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChangeConnectionStr_Click(object sender, EventArgs e)
        {
            var frmConnectionString = new ConfigureConnectionString();
            frmConnectionString.ShowDialog();
        }

        private void cbIPTContraindication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbIPTContraindication.Text.ToLower().Contains("other"))
            {
                this.txtOtherReasonContraindication.ReadOnly = false;
            }
            else
            {
                this.txtOtherReasonContraindication.ReadOnly = true;
            }

        }

        private void cbIPTDiscontinued_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbIPTDiscontinued.Text.ToLower().Contains("other"))
            {
                this.txtOtherReasonDiscontinued.ReadOnly = false;
            }
            else
            {
                this.txtOtherReasonDiscontinued.ReadOnly = true;
            }
        }

    }
}
