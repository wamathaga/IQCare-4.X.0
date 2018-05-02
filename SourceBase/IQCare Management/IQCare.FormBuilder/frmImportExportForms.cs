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
using Interface.Security;
using Application.Presentation;

namespace IQCare.FormBuilder
{
    public partial class frmImportExportForms : Form
    {
        DataSet objDsFormDetails = new DataSet();
        /////Datatables for Changes in Form Version//////////////
        DataTable dtmstformversion;
        DataTable dtTabchanges;
        DataTable dtSectionchanges;
        DataTable dtFieldChanges;
        DataTable dtConFieldchanges;
        DataSet DSFormVerTables;
        /////////////////////////////////////////////////////
        public frmImportExportForms()
        {
            InitializeComponent();
        }

        private void frmImportExportForms_Load(object sender, EventArgs e)
        {
            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //set css end 
            DataSet dsModule;
            DataTable dtAddAll;
            IManageForms objManageForms;
            objManageForms = (IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder");
            dsModule = objManageForms.GetPublishedModuleList();
            dtAddAll = dsModule.Tables[0];
            DataRow theDR = dtAddAll.NewRow();
            theDR["ModuleName"] = "All";
            theDR["ModuleId"] = 0;
            dtAddAll.Rows.InsertAt(theDR, 0);
            BindFunctions objBindControls = new BindFunctions();
            objBindControls.Win_BindCombo(cmbTechArea, dtAddAll, "ModuleName", "ModuleId");
            cmbTechArea.SelectedIndex = 0;
            ddlFormType.SelectedIndex = 0;
            ShowForms();
            SetRights();

        }
        public void SetRights()
        {
            if (GblIQCare.HasFunctionRight(ApplicationAccess.FBImportExportForm, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnExport.Enabled = false;
                btnImport.Enabled = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.FBImportExportForm, FunctionAccess.Update, GblIQCare.dtUserRight) == false)
            {
                btnExport.Enabled = false;
                btnImport.Enabled = false;
            }
           
        }
        public void ShowForms()
        {
            IImportExportForms objFormDetail;
            int iHeight;
            int iTechArea;

            chkLstBoxForms.Items.Clear();

            if (cmbTechArea.SelectedValue.ToString() == "System.Data.DataRowView")
                iTechArea = Convert.ToInt32(((System.Data.DataRowView)(cmbTechArea.SelectedValue)).Row.ItemArray[0]);
            else
                iTechArea = Convert.ToInt32(cmbTechArea.SelectedValue);

            objFormDetail = (IImportExportForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BImportExportForms,BusinessProcess.FormBuilder");
            if (ddlFormType.Text.ToString() == "" || ddlFormType.SelectedItem.ToString() == "Forms")
            {
                objDsFormDetails = objFormDetail.GetAllFormDetail("1", iTechArea.ToString(), Convert.ToInt16(GblIQCare.AppCountryId), "");
            }
            else
            {
                objDsFormDetails = objFormDetail.GetAllFormDetail("1", iTechArea.ToString(), Convert.ToInt16(GblIQCare.AppCountryId), "Home Page");
            }

            if (objDsFormDetails.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < objDsFormDetails.Tables[0].Rows.Count; i++)
                {
                    chkLstBoxForms.Items.Add(objDsFormDetails.Tables[0].Rows[i]["FormName"].ToString());

                }

                iHeight = 21 * objDsFormDetails.Tables[0].Rows.Count;
                chkLstBoxForms.Size = new System.Drawing.Size(403, iHeight);
                chkLstBoxForms.Visible = true;
            }
            else
            {
                chkLstBoxForms.Visible = false;
            }

        }

        private void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkLstBoxForms.Items.Count; i++)
            {
                if (chkCheckAll.Checked)
                    chkLstBoxForms.SetItemChecked(i, true);
                else
                    chkLstBoxForms.SetItemChecked(i, false);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (chkLstBoxForms.CheckedItems.Count > 0)
            {
                DataSet dsCollectDataToSave = new DataSet();

                IImportExportForms objExportFormDetails;
                objExportFormDetails = (IImportExportForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BImportExportForms,BusinessProcess.FormBuilder");
                for (int i = 0; i < chkLstBoxForms.CheckedItems.Count; i++)
                {
                    if (ddlFormType.Text.ToString() == "" || ddlFormType.SelectedItem.ToString() == "Forms")
                    {
                        objDsFormDetails = objExportFormDetails.GetImportExportFormDetail(chkLstBoxForms.CheckedItems[i].ToString());
                        if (dsCollectDataToSave.Tables.Count == 0)
                        {
                            ////mst_feature
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[0].Copy());
                            ////mst_section
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[1].Copy());
                            ////lnk_forms
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[2].Copy());
                            ////Select List Val only for custom fields
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[3].Copy());
                            ////Business Rule only for custom fields
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[4].Copy());
                            ////mst_module-tech area
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[5].Copy());
                            ////module identifier
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[6].Copy());
                            ////Conditional Fields
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[7].Copy());
                            ////Select List Val only for Conditional custom fields
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[8].Copy());
                            ////Business Rule only for Conditional custom fields
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[9].Copy());
                            ////APPAdmin for Version Check
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[10].Copy());
                            ////Field ICD Code Linking Table
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[11].Copy());
                            ////Master Table for Tabs
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[12].Copy());
                            ////Linking Table for Tabs and Section Id
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[13].Copy());
                            ////Special form linking
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[14].Copy());
                            ////Form Verioning
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[15].Copy());

                        }
                        else
                        {
                            foreach (DataRow dr in objDsFormDetails.Tables[0].Rows)
                            {
                                dsCollectDataToSave.Tables[0].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[1].Rows)
                            {
                                dsCollectDataToSave.Tables[1].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[2].Rows)
                            {
                                dsCollectDataToSave.Tables[2].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[3].Rows)
                            {
                                dsCollectDataToSave.Tables[3].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[4].Rows)
                            {
                                dsCollectDataToSave.Tables[4].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[5].Rows)
                            {
                                dsCollectDataToSave.Tables[5].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[6].Rows)
                            {
                                dsCollectDataToSave.Tables[6].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[7].Rows)
                            {
                                dsCollectDataToSave.Tables[7].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[8].Rows)
                            {
                                dsCollectDataToSave.Tables[8].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[9].Rows)
                            {
                                dsCollectDataToSave.Tables[9].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[10].Rows)
                            {
                                dsCollectDataToSave.Tables[10].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[11].Rows)
                            {
                                dsCollectDataToSave.Tables[11].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[12].Rows)
                            {
                                dsCollectDataToSave.Tables[12].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[13].Rows)
                            {
                                dsCollectDataToSave.Tables[13].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[14].Rows)
                            {
                                dsCollectDataToSave.Tables[14].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[15].Rows)
                            {
                                dsCollectDataToSave.Tables[15].ImportRow(dr);
                            }

                        }

                    }
                    else //export only home pages
                    {
                        objDsFormDetails = objExportFormDetails.GetImportExportHomeFormDetail(chkLstBoxForms.CheckedItems[i].ToString());

                        if (dsCollectDataToSave.Tables.Count == 0)
                        {
                            ////mst_feature
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[0].Copy());
                            ////mst_section
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[1].Copy());
                            ////lnk_forms
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[2].Copy());
                            //////Select List Val only for custom fields
                            //dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[3].Copy());
                            //////Business Rule only for custom fields
                            //dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[4].Copy());
                            //////mst_module-tech area
                            //dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[5].Copy());
                            //////module identifier
                            //dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[6].Copy());

                        }
                        else
                        {
                            foreach (DataRow dr in objDsFormDetails.Tables[0].Rows)
                            {
                                dsCollectDataToSave.Tables[0].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[1].Rows)
                            {
                                dsCollectDataToSave.Tables[1].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[2].Rows)
                            {
                                dsCollectDataToSave.Tables[2].ImportRow(dr);
                            }

                        }
                    }
                }
                FileDialog oDialog = new SaveFileDialog();
                oDialog.DefaultExt = "xml";
                oDialog.FileName = "Export Form-" + DateTime.Today.ToString("ddMMMyyyy") + ".xml";
                oDialog.Filter = "Form (*.xml)|*.xml";
                if (oDialog.ShowDialog() == DialogResult.OK)
                {
                    string strFilename = oDialog.FileName;
                    dsCollectDataToSave.WriteXml(strFilename);
                }
            }
            else
            {
                IQCareWindowMsgBox.ShowWindow("SelectForm", this);
                txtFileName.Focus();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FileDialog oDialog = new OpenFileDialog();
            oDialog.DefaultExt = "xml";
            oDialog.Filter = "Form (*.xml)|*.xml";
            if (oDialog.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = oDialog.FileName;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {

                int iRes = 0;
                bool isUpdate = false, isVersionUpdate = false;
                if (txtFileName.Text.ToString() != "")
                {
                    DataSet dsCollectDataToSave = new DataSet();
                    dsCollectDataToSave.ReadXml(txtFileName.Text.ToString());
                    IImportExportForms objImportFormDetails;
                    objImportFormDetails = (IImportExportForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BImportExportForms,BusinessProcess.FormBuilder");
                    if (ddlFormType.Text.ToString() == "" || ddlFormType.SelectedItem.ToString() == "Forms")
                    {
                        string strVerName = "";
                        if (dsCollectDataToSave.Tables.Count < 11)
                        {
                            IQCareWindowMsgBox.ShowWindow("ImportFormsCheckVersion", this);
                            return;

                        }
                        else if (dsCollectDataToSave.Tables.Count > 10)
                        {
                            strVerName = dsCollectDataToSave.Tables[10].Rows[0]["AppVer"].ToString();
                            if (GblIQCare.AppVersion.ToString() != strVerName)
                            {
                                IQCareWindowMsgBox.ShowWindow("ImportFormsCheckVersion", this);
                                return;
                            }

                        }
                        for (int i = 0; i < dsCollectDataToSave.Tables[0].Rows.Count; i++)
                        {
                            if (dsCollectDataToSave.Tables[0].Rows[i]["FeatureId"].ToString() == "126")
                            {
                                iRes = ImportExportDBUpdates(dsCollectDataToSave, false, false, objImportFormDetails);
                            }
                            else
                            {
                                DataSet DSExitingForm = objImportFormDetails.GetImportExportFormDetail(dsCollectDataToSave.Tables[0].Rows[i]["FeatureName"].ToString());

                                if (DSExitingForm.Tables[0].Rows.Count > 0)
                                {
                                    if (!CheckFormVersion(dsCollectDataToSave, DSExitingForm))
                                    {
                                        //mark true if same form found with no changes
                                        isUpdate = true;
                                        int result = DateTime.Compare(Convert.ToDateTime(DSExitingForm.Tables[15].Rows[0]["VersionDate"]), Convert.ToDateTime(dsCollectDataToSave.Tables[15].Rows[0]["VersionDate"]));
                                        if ((Convert.ToDecimal(dsCollectDataToSave.Tables[15].Rows[0]["VersionName"]) <= Convert.ToDecimal(DSExitingForm.Tables[15].Rows[0]["VersionName"])) && (result < 0))
                                        {
                                            //mark isVersionUpdate = true form with changes found to update version info
                                            isVersionUpdate = true;
                                            isUpdate = false;
                                            DialogResult msgconfirm = IQCareWindowMsgBox.ShowWindow(string.Format("There are field changes on the form.{0} The electronic form version will update upon saving.", Environment.NewLine), "*", "", this);
                                            if (msgconfirm == DialogResult.OK)
                                                iRes = ImportExportDBUpdates(dsCollectDataToSave, isUpdate, isVersionUpdate, objImportFormDetails);
                                            else
                                                return;
                                        }
                                        else
                                        {
                                            if (isUpdate)
                                                iRes = ImportExportDBUpdates(dsCollectDataToSave, isUpdate, isVersionUpdate, objImportFormDetails);
                                        }
                                    }
                                }
                                else
                                    iRes = ImportExportDBUpdates(dsCollectDataToSave, isUpdate, isVersionUpdate, objImportFormDetails);
                            }
                        }
                        
                    }
                    else
                        iRes = objImportFormDetails.ImportHomeForms(dsCollectDataToSave, GblIQCare.AppUserId, System.Convert.ToInt32(GblIQCare.AppCountryId));

                    if (iRes == 1)
                    {
                        IQCareWindowMsgBox.ShowWindow("ImportSuccess", this);
                        txtFileName.Text = "";
                    }
                }
                else
                {
                    IQCareWindowMsgBox.ShowWindow("BrowseFile", this);
                    txtFileName.Focus();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        private bool CheckFormVersion(DataSet NewData, DataSet dsUpdateMode)
        {
            if (dsUpdateMode.Tables.Count > 0)
            {
                var qrySection1 = dsUpdateMode.Tables[1].AsEnumerable().Select(a => new { SectionId = a["SectionId"].ToString() });
                var qrySection2 = NewData.Tables[1].AsEnumerable().Select(b => new { SectionId = b["SectionId"].ToString() });

                var exceptAB = qrySection1.Except(qrySection2);
                var exceptBA = qrySection2.Except(qrySection1);
                dtTabchanges = clsCommon.ManageVersionTab();
                dtSectionchanges = clsCommon.ManageVersionSection();
                dtFieldChanges = clsCommon.ManageVersionField();
                dtConFieldchanges = clsCommon.ManageVersionConField();
                DataRow dr;
                foreach (var section in exceptAB)
                {
                    var TabIdsQuery = NewData.Tables[12].AsEnumerable()
                                            .Where(c => c["SectionId"] == section.SectionId)
                                            .Select(c => new
                                            {
                                                TabId = c["TabId"]

                                            }).FirstOrDefault();
                    dr = dtSectionchanges.NewRow();
                    dr["SectionId"] = section.SectionId;
                    dr["FunctionId"] = 3;
                    dr["TabId"] = TabIdsQuery.TabId;
                    dtSectionchanges.Rows.Add(dr);

                }

                foreach (var section in exceptBA)
                {
                    dr = null;
                    var TabIdsQuery = NewData.Tables[12].AsEnumerable()
                        .Where(c => c["SectionId"].ToString() == section.SectionId.ToString()).Select(c => new
                        {
                            TabId = c["TabId"]

                        }).FirstOrDefault();
                    dr = dtSectionchanges.NewRow();
                    dr["SectionId"] = section.SectionId;
                    dr["FunctionId"] = 4;
                    dr["TabId"] = TabIdsQuery.TabId;
                    dtSectionchanges.Rows.Add(dr);

                }

                var qryTab1 = dsUpdateMode.Tables[12].AsEnumerable().Select(a => new { TabId = a["TabId"].ToString() });
                var qryTab2 = NewData.Tables[12].AsEnumerable().Select(b => new { TabId = b["TabId"].ToString() });

                var exceptTabAB = qryTab1.Except(qryTab2);
                var exceptTabBA = qryTab2.Except(qryTab1);

                foreach (var tab in exceptTabAB)
                {
                    dr = null;
                    dr = dtTabchanges.NewRow();
                    dr["TabId"] = tab.TabId;
                    dr["FunctionId"] = 3;
                    dtTabchanges.Rows.Add(dr);

                }
                foreach (var tab in exceptTabBA)
                {
                    dr = null;
                    dr = dtTabchanges.NewRow();
                    dr["TabId"] = tab.TabId;
                    dr["FunctionId"] = 4;
                    dtTabchanges.Rows.Add(dr);

                }


                var qryField1 = dsUpdateMode.Tables[2].AsEnumerable().Select(a => new { FieldId = a["FieldId"].ToString() });
                var qryField2 = NewData.Tables[2].AsEnumerable().Select(b => new { FieldId = b["FieldId"].ToString() });

                var exceptFieldAB = qryField2.Except(qryField1);
                var exceptFieldBA = qryField1.Except(qryField2);

                foreach (var field in exceptFieldAB)
                {
                    dr = null;
                    var FieldIdsQuery = NewData.Tables[2].AsEnumerable()
                                            .Where(c => c["FieldId"].ToString() == field.FieldId)
                                            .Select(c => new
                                            {
                                                SectionId = c["SectionId"]

                                            }).FirstOrDefault();
                    string sectionId = FieldIdsQuery.SectionId.ToString();
                    var FieldTabsQuery = NewData.Tables[12].AsEnumerable()
                                            .Where(d => d["SectionID"].ToString() == sectionId.ToString())
                                            .Select(d => new
                                            {
                                                TabId = d["TabId"]

                                            }).FirstOrDefault();
                    dr = dtFieldChanges.NewRow();
                    dr["FieldId"] = field.FieldId;
                    dr["FunctionId"] = 4;
                    dr["TabId"] = FieldTabsQuery.TabId;
                    dr["SectionId"] = FieldIdsQuery.SectionId;
                    dtFieldChanges.Rows.Add(dr);

                }

                foreach (var field in exceptFieldBA)
                {
                    dr = null;
                    var FieldIdsQuery = NewData.Tables[2].AsEnumerable()
                                            .Where(c => c["FieldId"].ToString() == field.FieldId)
                                            .Select(c => new
                                            {
                                                SectionId = c["SectionId"]

                                            }).FirstOrDefault();
                    var FieldTabsQuery = NewData.Tables[12].AsEnumerable()
                                            .Where(c => c["SectionId"].ToString() == FieldIdsQuery.SectionId)
                                            .Select(c => new
                                            {
                                                TabId = c["TabId"]

                                            }).FirstOrDefault();
                    dr = dtFieldChanges.NewRow();
                    dr["FieldId"] = field.FieldId;
                    dr["FunctionId"] = 3;
                    dr["TabId"] = FieldTabsQuery.TabId;
                    dr["SectionId"] = FieldIdsQuery.SectionId;
                    dtFieldChanges.Rows.Add(dr);

                }

                ////////////////////// Conditional Field Check /////////////////////////////////
                var qryConField1 = dsUpdateMode.Tables[7].AsEnumerable()
                                   .Select(a => new { FieldId = a["FieldId"].ToString(), ConFieldId = a["ConditionalFieldId"].ToString(), fieldpredefined = a["fieldpredefined"].ToString(), Conditionalfieldpredefined = a["ConditionalFieldPredefined"].ToString() });
                var qryConField2 = NewData.Tables[7].AsEnumerable()
                                    .Select(b => new { FieldId = b["FieldId"].ToString(), ConFieldId = b["ConditionalFieldId"].ToString(), fieldpredefined = b["fieldpredefined"].ToString(), Conditionalfieldpredefined = b["ConditionalFieldPredefined"].ToString() });

                var exceptConFieldAB = qryConField2.Except(qryConField1);
                var exceptConFieldBA = qryConField1.Except(qryConField2);

                foreach (var conditional in exceptConFieldAB)
                {
                    dr = null;
                    dr = dtConFieldchanges.NewRow();
                    dr["ConFieldId"] = conditional.ConFieldId;
                    dr["ConPredefined"] = conditional.Conditionalfieldpredefined;
                    dr["FieldId"] = conditional.FieldId;
                    dr["Predefined"] = conditional.fieldpredefined;
                    dr["FunctionId"] = 3;
                    dtConFieldchanges.Rows.Add(dr);

                }

                foreach (var conditional in exceptConFieldBA)
                {
                    dr = null;
                    dr = dtConFieldchanges.NewRow();
                    dr["ConFieldId"] = conditional.ConFieldId;
                    dr["ConPredefined"] = conditional.Conditionalfieldpredefined;
                    dr["FieldId"] = conditional.FieldId;
                    dr["Predefined"] = conditional.fieldpredefined;
                    dr["FunctionId"] = 4;
                    dtConFieldchanges.Rows.Add(dr);

                }
                ////////////////////////////////////////////////////////////////////////////////
                if (dtTabchanges.Rows.Count > 0 || dtSectionchanges.Rows.Count > 0 || dtFieldChanges.Rows.Count > 0 || dtConFieldchanges.Rows.Count > 0)
                    return false;
                else
                {
                    DialogResult dr_result = MessageBox.Show(this, "Selected form already exists. Would like to overwrite the existing form. Click YES to proceed", "Duplicate Form Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dr_result == DialogResult.Yes)
                        return false;
                    else
                        return true;
                }
                
            }
            return true;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            chkLstBoxForms.Items.Clear();
            ShowForms();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cmbTechArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowForms();
        }

        private void ddlFormType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowForms();
        }

        private int ImportExportDBUpdates(DataSet dsCollectDataToSave, bool isUpdate, bool isVersionUpdate, IImportExportForms objImportFormDetails)
        {
            if (dsCollectDataToSave.Tables.Count < 0)
                return -1;


            IIQCareSystem objIIQCareSystem = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem,BusinessProcess.Security");
            int iRes = 0;
            decimal vernumber = Convert.ToDecimal(dsCollectDataToSave.Tables[15].Rows[0]["VersionName"]);

            DataSet DSFormVerTables = new DataSet();
            dtmstformversion = clsCommon.CreateMstVersionTable();
            DataRow drfrmver = dtmstformversion.NewRow();
            String dtchng;
            if (dsCollectDataToSave.Tables[15].Rows[0]["VersionDate"] != null)
                dtchng = DateTime.Parse(dsCollectDataToSave.Tables[15].Rows[0]["VersionDate"].ToString()).ToString("dd-MMM-yyyy");
            else
                dtchng = objIIQCareSystem.SystemDate().ToString("dd-MMM-yyyy");
            drfrmver["VerId"] = 0;
            drfrmver["VersionDate"] = dtchng.ToString();
            drfrmver["UserId"] = GblIQCare.AppUserId;
            //version will not update if same form is imported
            if (isUpdate && !isVersionUpdate)
            {
                drfrmver["VersionName"] = vernumber.ToString();

            }
            else if (!isUpdate && isVersionUpdate)
            {
                //increment version if any changes found on form
                vernumber = vernumber + Convert.ToDecimal(0.1);
                dtchng = DateTime.Parse(dsCollectDataToSave.Tables[15].Rows[0]["VersionDate"].ToString()).ToString("dd-MMM-yyyy");
                drfrmver["VersionName"] = vernumber.ToString();
                dtmstformversion = clsCommon.CreateMstVersionTable();
                dtTabchanges = clsCommon.ManageVersionTab();
                dtSectionchanges = clsCommon.ManageVersionSection();
                dtFieldChanges = clsCommon.ManageVersionField();
                dtConFieldchanges = clsCommon.ManageVersionConField();
            }
            else
            {
                //dtchng = objIIQCareSystem.SystemDate();
                drfrmver["VersionDate"] = dtchng.ToString();
                drfrmver["VersionName"] = vernumber.ToString();
                dtTabchanges = clsCommon.ManageVersionTab();
                dtSectionchanges = clsCommon.ManageVersionSection();
                dtFieldChanges = clsCommon.ManageVersionField();
                dtConFieldchanges = clsCommon.ManageVersionConField();
            }
            dtmstformversion.Rows.Add(drfrmver);

            DSFormVerTables.Tables.Add(dtmstformversion);
            DSFormVerTables.Tables.Add(dtTabchanges);
            DSFormVerTables.Tables.Add(dtSectionchanges);
            DSFormVerTables.Tables.Add(dtFieldChanges);
            DSFormVerTables.Tables.Add(dtConFieldchanges);
            iRes = objImportFormDetails.ImportForms(dsCollectDataToSave, GblIQCare.AppUserId, System.Convert.ToInt32(GblIQCare.AppCountryId), DSFormVerTables,GblIQCare.AppLocationId);
            return iRes;
        }

    }
}
