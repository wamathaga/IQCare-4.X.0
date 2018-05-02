using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;
using Application.Common;
using System.Collections;
using DataAccess.Base;
using System.Runtime.InteropServices;


namespace BusinessProcess.FormBuilder
{
    [Serializable]
    public class BImportExportForms : ProcessBase, IImportExportForms
    {

        public DataSet GetAllFormDetail(string strFormStatus, string strTechArea, Int32 CountryId, string frmFormType)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FormStatus", SqlDbType.VarChar, strFormStatus);
                ClsUtility.AddParameters("@TechArea", SqlDbType.VarChar, strTechArea);
                ClsUtility.AddParameters("@CountryId", SqlDbType.VarChar, CountryId.ToString());
                if (frmFormType == "")
                {
                    return (DataSet)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ManageForm_GetAllFormDetail_Futures", ClsDBUtility.ObjectEnum.DataSet);
                }
                else
                {
                    return (DataSet)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ManageForm_GetAllHomeFormDetail_Futures", ClsDBUtility.ObjectEnum.DataSet);
                }
            }
        }

        public DataSet GetImportExportFormDetail(String strFeatureName)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                DataSet dsRes;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, strFeatureName.ToString());
                dsRes = (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_FetchFormsDetail_Futures", ClsDBUtility.ObjectEnum.DataSet);
                return dsRes;
            }
        }

        public DataSet GetImportExportHomeFormDetail(String strFeatureName)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                DataSet dsRes;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, strFeatureName.ToString());
                dsRes = (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_FetchHomeFormsDetail_Futures", ClsDBUtility.ObjectEnum.DataSet);
                return dsRes;
            }
        }

        public int ImportForms(DataSet dsImportForms, int iUserId, int iCountryId, DataSet DSFormVer,int FacilityId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FormDetail = new ClsObject();
                FormDetail.Connection = this.Connection;
                FormDetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                int theRowAffected_SpLnkForms = 0;
                DataRow theDR;

                int iNewFeatureId; //this variable will be used to store featureid for all new rows
                int iNewModuleId = 0;
                int iNewSectionId;
                int iNewFieldId = 0;
                int iNewConFieldId = 0;
                int iNewTabId = 0;
                //string istrselectlstModecodeId = string.Empty;
                string strTableName = string.Empty;
                for (int j = 0; j < dsImportForms.Tables[5].Rows.Count; j++)
                {
                    if (dsImportForms.Tables[5].Rows[j].ItemArray[0].ToString() != "0")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dsImportForms.Tables[5].Rows[j]["ModuleId"].ToString());
                        ClsUtility.AddParameters("@ModuleName", SqlDbType.VarChar, dsImportForms.Tables[5].Rows[j]["ModuleName"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, iUserId.ToString());
                        theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportModules_Futures", ClsDBUtility.ObjectEnum.DataRow);
                        iNewModuleId = System.Convert.ToInt32(theDR[0].ToString());

                        DataView dvModuleFieldDV = new DataView();
                        dvModuleFieldDV = dsImportForms.Tables[6].DefaultView;
                        DataTable dtModuleField = new DataTable();
                        dvModuleFieldDV.RowFilter = "ModuleId=" + dsImportForms.Tables[5].Rows[j]["ModuleId"].ToString();
                        dtModuleField = dvModuleFieldDV.ToTable();
                        for (int k = 0; k < dtModuleField.Rows.Count; k++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, iNewModuleId.ToString());
                            ClsUtility.AddParameters("@FieldId", SqlDbType.VarChar, dsImportForms.Tables[6].Rows[k]["Id"].ToString());
                            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[6].Rows[k]["FieldName"].ToString());
                            ClsUtility.AddParameters("@FieldType", SqlDbType.Int, dsImportForms.Tables[6].Rows[k]["FieldType"].ToString());
                            if (dsImportForms.Tables[6].Columns.Contains("label"))
                            {
                                ClsUtility.AddParameters("@Label", SqlDbType.VarChar, (dsImportForms.Tables[6].Rows[k]["label"].ToString() == null ? dsImportForms.Tables[6].Rows[k]["FieldName"].ToString() : dsImportForms.Tables[6].Rows[k]["label"].ToString()));
                            }
                            else
                                ClsUtility.AddParameters("@Label", SqlDbType.VarChar, dsImportForms.Tables[6].Rows[k]["FieldName"].ToString());
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, iUserId.ToString());
                            theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportModulesIdentifier_Futures", ClsDBUtility.ObjectEnum.DataRow);
                            //iNewModuleId = System.Convert.ToInt32(theDR[0].ToString());
                        }
                    }
                }
                for (int i = 0; i < dsImportForms.Tables[0].Rows.Count; i++)
                {

                    string[] strFeatureName = new string[10];
                    strFeatureName = dsImportForms.Tables[0].Rows[i]["FeatureName"].ToString().Split(' ');
                    strTableName = "";
                    for (int j = 0; j < strFeatureName.Length; j++)
                    {
                        if (j > 0)
                            strTableName += "_" + strFeatureName[j];
                        else
                            strTableName += strFeatureName[j];

                    }
                    string strgridFeaturename = strTableName.ToString();
                    strTableName = "DTL_FBCUSTOMFIELD_" + strTableName;

                    //for modules and its identifiers
                    //modules-tech area's
                    ///Get New Module Id
                    if (dsImportForms.Tables[5].Rows[i].ItemArray[0].ToString() != "0")
                    {
                        //DataRow[] foundRows = dsImportForms.Tables[5].Select("ModuleId=" + dsImportForms.Tables[0].Rows[i]["ModuleId"]);
                        DataView theFiltModDV = new DataView(dsImportForms.Tables[5]);
                        DataTable dtFiltMod = new DataTable();
                        theFiltModDV.RowFilter = "ModuleId =" + dsImportForms.Tables[0].Rows[i]["ModuleId"];
                        dtFiltMod = theFiltModDV.ToTable();
                        string strModName = dtFiltMod.Rows[0]["ModuleName"].ToString();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ModuleName", SqlDbType.VarChar, strModName.ToString());
                        theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_GetNewModuleId_Futures", ClsDBUtility.ObjectEnum.DataRow);
                        iNewModuleId = System.Convert.ToInt32(theDR[0].ToString());
                    }

                    //save mst_feature data
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString());
                    ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, dsImportForms.Tables[0].Rows[i]["FeatureName"].ToString());
                    ClsUtility.AddParameters("@ReportFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["ReportFlag"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["DeleteFlag"].ToString());
                    ClsUtility.AddParameters("@AdminFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["AdminFlag"].ToString());
                    //ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["UserID"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                    ClsUtility.AddParameters("@OptionalFlag", SqlDbType.Int, (dsImportForms.Tables[0].Columns.Contains("OptionalFlag")) ? dsImportForms.Tables[0].Rows[i]["OptionalFlag"].ToString() : "");
                    ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["SystemId"].ToString());
                    ClsUtility.AddParameters("@Published", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["Published"].ToString());
                    //ClsUtility.AddParameters("@CountryId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["CountryId"].ToString());
                    ClsUtility.AddParameters("@CountryId", SqlDbType.Int, iCountryId.ToString());
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, iNewModuleId.ToString());
                    if (dsImportForms.Tables[0].Columns.Contains("MultiVisit"))
                    {
                        ClsUtility.AddParameters("@MultiVisit", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["MultiVisit"].ToString());
                    }
                    else
                    {
                        ClsUtility.AddParameters("@MultiVisit", SqlDbType.Int, "1");
                    }


                    theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportForm_Futures", ClsDBUtility.ObjectEnum.DataRow);
                    iNewFeatureId = System.Convert.ToInt32(theDR[0].ToString());
                    if (iNewFeatureId.ToString() == "126")
                    {
                        #region "Import Patient Registration"
                        DataView theRegSecDV = new DataView(dsImportForms.Tables[1]);
                        DataTable dtRegSection = new DataTable();
                        theRegSecDV.RowFilter = "FeatureId =" + dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString();
                        dtRegSection = theRegSecDV.ToTable();
                        //foreach (DataRow drFormData in dsSaveFormData.Tables[1])
                        for (int j = 0; j < dtRegSection.Rows.Count; j++)
                        {
                            if (dtRegSection.Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString())
                            {
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dtRegSection.Rows[j]["SectionId"].ToString());
                                ClsUtility.AddParameters("@SectionName", SqlDbType.VarChar, dtRegSection.Rows[j]["SectionName"].ToString());
                                ClsUtility.AddParameters("@Seq", SqlDbType.Int, dtRegSection.Rows[j]["Seq"].ToString());
                                ClsUtility.AddParameters("@CustomFlag", SqlDbType.Int, dtRegSection.Rows[j]["CustomFlag"].ToString());
                                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dtRegSection.Rows[j]["DeleteFlag"].ToString());
                                ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                //ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString());
                                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                ClsUtility.AddParameters("@IsGridView", SqlDbType.Int, (dtRegSection.Columns.Contains("IsGridView")) ? dtRegSection.Rows[j]["IsGridView"].ToString() : "0");
                                // ClsUtility.AddParameters("@IsGridView", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["IsGridView"].ToString());
                                theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportSection_Futures", ClsDBUtility.ObjectEnum.DataRow);
                                iNewSectionId = System.Convert.ToInt32(theDR[0].ToString());

                                #region "Update/Insert Fields"
                                DataView thelnkDV = new DataView(dsImportForms.Tables[2]);
                                DataTable dtlnkField = new DataTable();
                                thelnkDV.RowFilter = "FeatureId =" + dtRegSection.Rows[j]["FeatureId"].ToString();
                                dtlnkField = thelnkDV.ToTable();

                                for (int x = 0; x < dtlnkField.Rows.Count; x++)
                                {
                                    if (dtRegSection.Rows[j]["FeatureId"].ToString() == dtlnkField.Rows[x]["FeatureId"].ToString() && dtRegSection.Rows[j]["SectionId"].ToString() == dtlnkField.Rows[x]["SectionId"].ToString())
                                    {
                                        //store comma separated select list val for field
                                        string strSelectLstVal = string.Empty;
                                        if (dsImportForms.Tables.Count > 3)
                                        {
                                            if (dsImportForms.Tables[3].Rows[0][0].ToString() != "0")
                                            {
                                                for (int l = 0; l < dsImportForms.Tables[3].Rows.Count; l++)
                                                {
                                                    if (dsImportForms.Tables[3].Rows[l]["FeatureId"].ToString() == dtlnkField.Rows[x]["FeatureId"].ToString() && dsImportForms.Tables[3].Rows[l]["FieldId"].ToString() == dtlnkField.Rows[x]["FieldId"].ToString() && dsImportForms.Tables[3].Rows[l]["SectionId"].ToString() == dtlnkField.Rows[x]["SectionId"].ToString())
                                                    {
                                                        if (strSelectLstVal == "")
                                                            strSelectLstVal = dsImportForms.Tables[3].Rows[l]["ListVal"].ToString();
                                                        else
                                                            strSelectLstVal = strSelectLstVal + ";" + dsImportForms.Tables[3].Rows[l]["ListVal"].ToString();
                                                    }
                                                }

                                            }
                                        }

                                        //busrule id and val comma separated value, e.g. BusRuleId-Value(val used in case of min and max)
                                        string strBusRuleIdVal = string.Empty;
                                        if (dsImportForms.Tables.Count > 4)
                                        {
                                            if (dsImportForms.Tables[4].Rows[0][0].ToString() != "0")
                                            {
                                                for (int m = 0; m < dsImportForms.Tables[4].Rows.Count; m++)
                                                {
                                                    if (dsImportForms.Tables[4].Rows[m]["FieldId"].ToString() == dtlnkField.Rows[x]["FieldId"].ToString() && dsImportForms.Tables[4].Rows[m]["Predefined"].ToString() == dtlnkField.Rows[x]["Predefined"].ToString())
                                                    {
                                                        if (strBusRuleIdVal == "")
                                                            strBusRuleIdVal = dsImportForms.Tables[4].Rows[m]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[4].Rows[m]["Value"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value"].ToString() : "Null");
                                                        else
                                                            strBusRuleIdVal = strBusRuleIdVal + "," + dsImportForms.Tables[4].Rows[m]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[4].Rows[m]["Value"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value"].ToString() : "Null");
                                                    }
                                                }
                                            }
                                        }

                                        ClsUtility.Init_Hashtable();
                                        ClsUtility.AddParameters("@Id", SqlDbType.Int, dtlnkField.Rows[x]["Id"].ToString());
                                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                        ClsUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dtlnkField.Rows[x]["FieldId"].ToString());
                                        ClsUtility.AddParameters("@FieldName", SqlDbType.Int, dtlnkField.Rows[x]["FieldName"].ToString());
                                        ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dtlnkField.Rows[x]["FieldLabel"].ToString());
                                        ClsUtility.AddParameters("@ControlId", SqlDbType.Int, (dtlnkField.Columns.Contains("ControlId")) ? dtlnkField.Rows[x]["ControlId"].ToString() : "");
                                        ClsUtility.AddParameters("@SelectListVal", SqlDbType.VarChar, strSelectLstVal);
                                        ClsUtility.AddParameters("@BusRuleIdValAll", SqlDbType.VarChar, strBusRuleIdVal);
                                        ClsUtility.AddParameters("@Seq", SqlDbType.Int, dtlnkField.Rows[x]["Seq"].ToString());
                                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dtlnkField.Rows[x]["Predefined"].ToString());
                                        if (iNewFeatureId == 126)
                                        {
                                            ClsUtility.AddParameters("@PatientRegistration", SqlDbType.Int, "1");
                                        }
                                        ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, FacilityId.ToString());
                                        //theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                        DataTable theFieldDT = (DataTable)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsDBUtility.ObjectEnum.DataTable);
                                        if (theFieldDT.Rows.Count > 1)
                                        {
                                            DataView dvFieldDV = new DataView();
                                            dvFieldDV = theFieldDT.DefaultView;
                                            DataTable dtField = new DataTable();
                                            dvFieldDV.RowFilter = "FormFieldID <>'0'";
                                            dtField = dvFieldDV.ToTable();
                                            iNewFieldId = System.Convert.ToInt32(dtField.Rows[0][0].ToString());

                                        }
                                        else
                                            iNewFieldId = System.Convert.ToInt32(theFieldDT.Rows[0][0].ToString());
                                        if (dtlnkField.Rows[x]["Predefined"].ToString() != "1")
                                        {

                                            ClsUtility.Init_Hashtable();
                                            ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                                            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtlnkField.Rows[x]["FieldName"].ToString());
                                            ClsUtility.AddParameters("@DataType", SqlDbType.Int, dtlnkField.Rows[x]["ControlId"].ToString());
                                            ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dtlnkField.Rows[x]["Predefined"].ToString());
                                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dtlnkField.Rows[x]["FieldId"].ToString());
                                            theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                        }
                                        ///////Import GridView Control-Create Table//////////////////////////////////

                                        if (dtRegSection.Rows[j]["IsGridView"].ToString() == "1" && dtlnkField.Rows[x]["Predefined"].ToString() != "1")
                                        {
                                            string strTableNameSection = "DTL_CUSTOMFORM_" + dtRegSection.Rows[j]["SectionName"].ToString() + "_" + strgridFeaturename;
                                            ClsUtility.Init_Hashtable();
                                            ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableNameSection);
                                            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtlnkField.Rows[x]["FieldName"].ToString());
                                            ClsUtility.AddParameters("@DataType", SqlDbType.Int, dtlnkField.Rows[x]["ControlId"].ToString());
                                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dtlnkField.Rows[x]["FieldId"].ToString());
                                            theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreationGridView_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                        }

                                        ////////Import Feild ICDCode Linking////////////////////////////////////////////
                                        if ((dsImportForms.Tables[11].Rows[0].ItemArray[0].ToString() != "0") && (dtlnkField.Rows[x]["ControlId"].ToString() == "16"))
                                        {
                                            DataView dvFilteredRow = new DataView();
                                            dvFilteredRow = dsImportForms.Tables[11].DefaultView;
                                            DataTable dtRow = new DataTable();
                                            dvFilteredRow.RowFilter = "FieldId='" + dtlnkField.Rows[x]["FieldId"].ToString() + "'";
                                            dtRow = dvFilteredRow.ToTable();
                                            if (dtRow.Rows.Count > 0)
                                            {
                                                if (dtlnkField.Rows[x]["FieldId"].ToString() == iNewFieldId.ToString())
                                                {
                                                    ClsUtility.Init_Hashtable();
                                                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dtlnkField.Rows[x]["FieldId"].ToString());
                                                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_DeleteFieldICDCode_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                                }
                                                for (int q = 0; q < dtRow.Rows.Count; q++)
                                                {
                                                    ClsUtility.Init_Hashtable();
                                                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, iNewFieldId.ToString());
                                                    ClsUtility.AddParameters("@BlockId", SqlDbType.Int, dtRow.Rows[q]["BlockId"].ToString());
                                                    ClsUtility.AddParameters("@SubBlockId", SqlDbType.Int, dtRow.Rows[q]["SubBlockId"].ToString());
                                                    ClsUtility.AddParameters("@CodeId", SqlDbType.Int, dtRow.Rows[q]["CodeId"].ToString());
                                                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, dtRow.Rows[q]["UserId"].ToString());
                                                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                                                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dtRow.Rows[q]["Predefined"].ToString());
                                                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveICD10CodeItems_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                                                }
                                            }


                                        }
                                        ///////////////////////////////////////////////////
                                        #region "Update/Insert Conditional Fields"
                                        if (dsImportForms.Tables.Count > 7)
                                        {
                                            if (dsImportForms.Tables[7].Rows.Count > 0)
                                            {
                                                if (dsImportForms.Tables[7].Rows[0][0].ToString() != "")
                                                {
                                                    if (dsImportForms.Tables[7].Rows[0][0].ToString() != "0")
                                                    {
                                                        for (int n = 0; n < dsImportForms.Tables[7].Rows.Count; n++)
                                                        {
                                                            if (dtRegSection.Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[n]["FeatureId"].ToString() && dtRegSection.Rows[j]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["SectionId"].ToString())
                                                            {
                                                                //store comma separated select list val for field
                                                                string strConSelectLstVal = string.Empty;
                                                                if (dsImportForms.Tables.Count > 8)
                                                                {
                                                                    if (dsImportForms.Tables[8].Rows[0][0].ToString() != "0")
                                                                    {
                                                                        for (int l = 0; l < dsImportForms.Tables[8].Rows.Count; l++)
                                                                        {
                                                                            if (dsImportForms.Tables[8].Rows[l]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[n]["FeatureId"].ToString() && dsImportForms.Tables[8].Rows[l]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString() && dsImportForms.Tables[8].Rows[l]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldSectionId"].ToString())
                                                                            {
                                                                                if (strConSelectLstVal == "")
                                                                                    strConSelectLstVal = dsImportForms.Tables[8].Rows[l]["ListVal"].ToString();
                                                                                else
                                                                                    strConSelectLstVal = strConSelectLstVal + ";" + dsImportForms.Tables[8].Rows[l]["ListVal"].ToString();
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                //busrule id and val comma separated value, e.g. BusRuleId-Value(val used in case of min and max)
                                                                string strConBusRuleIdVal = string.Empty;
                                                                if (dsImportForms.Tables.Count > 9)
                                                                {
                                                                    if (dsImportForms.Tables[9].Rows[0][0].ToString() != "0")
                                                                    {
                                                                        for (int z = 0; z < dsImportForms.Tables[9].Rows.Count; z++)
                                                                        {
                                                                            if (dsImportForms.Tables[9].Rows[z]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString() && dsImportForms.Tables[9].Rows[z]["Predefined"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString())
                                                                            {
                                                                                if (strConBusRuleIdVal == "")
                                                                                    strConBusRuleIdVal = dsImportForms.Tables[9].Rows[z]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[9].Rows[z]["Value"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value1") && dsImportForms.Tables[9].Rows[z]["Value1"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value1"].ToString() : "Null");
                                                                                else
                                                                                    strConBusRuleIdVal = strConBusRuleIdVal + "," + dsImportForms.Tables[9].Rows[z]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[9].Columns.Contains("Value") && dsImportForms.Tables[9].Rows[z]["Value"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[9].Columns.Contains("Value1") && dsImportForms.Tables[9].Rows[z]["Value1"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value1"].ToString() : "Null");
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                if (dtlnkField.Rows[x]["fieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["FieldId"].ToString() && dtlnkField.Rows[x]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["SectionId"].ToString() && dtlnkField.Rows[x]["featureId"].ToString() == dsImportForms.Tables[7].Rows[n]["featureId"].ToString())
                                                                {
                                                                    ClsUtility.Init_Hashtable();
                                                                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                                                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                                                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, iNewFieldId.ToString());
                                                                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["FieldName"].ToString());
                                                                    ClsUtility.AddParameters("@ConFieldId", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString());
                                                                    ClsUtility.AddParameters("@ConFieldName", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldName"].ToString());
                                                                    ClsUtility.AddParameters("@ConFieldLabel", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ConditionalFieldLabel"].ToString());
                                                                    ClsUtility.AddParameters("@ControlId", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ControlId"].ToString());
                                                                    ClsUtility.AddParameters("@ConControlId", SqlDbType.Int, (dsImportForms.Tables[7].Columns.Contains("ConditionalFieldControlId")) ? dsImportForms.Tables[7].Rows[n]["ConditionalFieldControlId"].ToString() : "");
                                                                    ClsUtility.AddParameters("@ConSelectListVal", SqlDbType.VarChar, strConSelectLstVal);
                                                                    ClsUtility.AddParameters("@ConBusRuleIdValAll", SqlDbType.VarChar, strConBusRuleIdVal);
                                                                    ClsUtility.AddParameters("@ConSeq", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldSequence"].ToString());
                                                                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                                                    ClsUtility.AddParameters("@ConPredefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString());
                                                                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["fieldpredefined"].ToString());
                                                                    ClsUtility.AddParameters("@ConSectionId", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldSectionId"].ToString());
                                                                    ClsUtility.AddParameters("@ModdecodeName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["Mod"].ToString());
                                                                    ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["SystemId"].ToString());
                                                                    //theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                                                    if (iNewFeatureId == 126)
                                                                    {
                                                                        theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportRegistrationConditionalField_Futures", ClsDBUtility.ObjectEnum.DataRow);
                                                                    }
                                                                    else
                                                                    {
                                                                        theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportConditionalField_Futures", ClsDBUtility.ObjectEnum.DataRow);
                                                                    }
                                                                    iNewConFieldId = System.Convert.ToInt32(theDR[0].ToString());

                                                                    if (dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString() != "1")
                                                                    {

                                                                        ClsUtility.Init_Hashtable();
                                                                        ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                                                                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ConditionalFieldName"].ToString());
                                                                        ClsUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldControlId"].ToString());
                                                                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString());
                                                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, iNewConFieldId.ToString());
                                                                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                                                    }
                                                                }


                                                            }
                                                        }
                                                    }
                                                }//0 closed
                                            }
                                        }
                                        #endregion

                                    }//feature id and section id if condition closes here

                                }
                                #endregion

                            }
                        }

                        #endregion
                    }
                    else
                    {
                        //Inserting New tabs for Feature 
                        for (int tab = 0; tab < dsImportForms.Tables[12].Rows.Count; tab++)
                        {
                            if (dsImportForms.Tables[12].Rows[tab]["FeatureId"].ToString() == dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString())
                            {
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@TabId", SqlDbType.Int, dsImportForms.Tables[12].Rows[tab]["TabId"].ToString());
                                ClsUtility.AddParameters("@TabName", SqlDbType.VarChar, dsImportForms.Tables[12].Rows[tab]["TabName"].ToString());
                                ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[12].Rows[tab]["Seq"].ToString());
                                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[12].Rows[tab]["DeleteFlag"].ToString());
                                ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());

                                theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportTabs_Futures", ClsDBUtility.ObjectEnum.DataRow);
                                iNewTabId = System.Convert.ToInt32(theDR[0].ToString());

                            }
                            //foreach (DataRow drFormData in dsSaveFormData.Tables[1])
                            for (int j = 0; j < dsImportForms.Tables[1].Rows.Count; j++)
                            {
                                if (dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString())
                                {
                                    ClsUtility.Init_Hashtable();
                                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["SectionId"].ToString());
                                    ClsUtility.AddParameters("@SectionName", SqlDbType.VarChar, dsImportForms.Tables[1].Rows[j]["SectionName"].ToString());
                                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["Seq"].ToString());
                                    ClsUtility.AddParameters("@CustomFlag", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["CustomFlag"].ToString());
                                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["DeleteFlag"].ToString());
                                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                    //ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString());
                                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                    ClsUtility.AddParameters("@IsGridView", SqlDbType.Int, (dsImportForms.Tables[1].Columns.Contains("IsGridView")) ? dsImportForms.Tables[1].Rows[j]["IsGridView"].ToString() : "0");
                                    // ClsUtility.AddParameters("@IsGridView", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["IsGridView"].ToString());
                                    theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportSection_Futures", ClsDBUtility.ObjectEnum.DataRow);
                                    iNewSectionId = System.Convert.ToInt32(theDR[0].ToString());
                                    for (int ts = 0; ts < dsImportForms.Tables[13].Rows.Count; ts++)
                                    {
                                        if (dsImportForms.Tables[13].Rows[ts]["SectionId"].ToString() == dsImportForms.Tables[1].Rows[j]["SectionId"].ToString() && dsImportForms.Tables[12].Rows[tab]["TabId"].ToString() == dsImportForms.Tables[13].Rows[ts]["TabId"].ToString())
                                        {
                                            ClsUtility.Init_Hashtable();
                                            ClsUtility.AddParameters("@TabId", SqlDbType.Int, iNewTabId.ToString());
                                            ClsUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                            theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkFormTabSection_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);


                                        }
                                    }
                                    #region "Update/Insert Fields"
                                    for (int x = 0; x < dsImportForms.Tables[2].Rows.Count; x++)
                                    {
                                        if (dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[2].Rows[x]["FeatureId"].ToString() && dsImportForms.Tables[1].Rows[j]["SectionId"].ToString() == dsImportForms.Tables[2].Rows[x]["SectionId"].ToString())
                                        {
                                            //store comma separated select list val for field
                                            string strSelectLstVal = string.Empty;
                                            if (dsImportForms.Tables.Count > 3)
                                            {
                                                if (dsImportForms.Tables[3].Rows[0][0].ToString() != "0")
                                                {
                                                    for (int l = 0; l < dsImportForms.Tables[3].Rows.Count; l++)
                                                    {
                                                        if (dsImportForms.Tables[3].Rows[l]["FeatureId"].ToString() == dsImportForms.Tables[2].Rows[x]["FeatureId"].ToString() && dsImportForms.Tables[3].Rows[l]["FieldId"].ToString() == dsImportForms.Tables[2].Rows[x]["FieldId"].ToString() && dsImportForms.Tables[3].Rows[l]["SectionId"].ToString() == dsImportForms.Tables[2].Rows[x]["SectionId"].ToString())
                                                        {
                                                            if (strSelectLstVal == "")
                                                                strSelectLstVal = dsImportForms.Tables[3].Rows[l]["ListVal"].ToString();
                                                            else
                                                                strSelectLstVal = strSelectLstVal + ";" + dsImportForms.Tables[3].Rows[l]["ListVal"].ToString();
                                                        }
                                                    }

                                                }
                                            }

                                            //busrule id and val comma separated value, e.g. BusRuleId-Value(val used in case of min and max)
                                            string strBusRuleIdVal = string.Empty;
                                            if (dsImportForms.Tables.Count > 4)
                                            {
                                                if (dsImportForms.Tables[4].Rows[0][0].ToString() != "0")
                                                {
                                                    for (int m = 0; m < dsImportForms.Tables[4].Rows.Count; m++)
                                                    {
                                                        if (dsImportForms.Tables[4].Rows[m]["FieldId"].ToString() == dsImportForms.Tables[2].Rows[x]["FieldId"].ToString() && dsImportForms.Tables[4].Rows[m]["Predefined"].ToString() == dsImportForms.Tables[2].Rows[x]["Predefined"].ToString())
                                                        {
                                                            if (strBusRuleIdVal == "")
                                                                strBusRuleIdVal = dsImportForms.Tables[4].Rows[m]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[4].Rows[m]["Value"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value1") && dsImportForms.Tables[4].Rows[m]["Value1"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value1"].ToString() : "Null");
                                                            else
                                                                strBusRuleIdVal = strBusRuleIdVal + "," + dsImportForms.Tables[4].Rows[m]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[4].Rows[m]["Value"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value1") && dsImportForms.Tables[4].Rows[m]["Value1"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value1"].ToString() : "Null");
                                                        }
                                                    }
                                                }
                                            }

                                            ClsUtility.Init_Hashtable();
                                            ClsUtility.AddParameters("@Id", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["Id"].ToString());
                                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                            ClsUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldId"].ToString());
                                            ClsUtility.AddParameters("@FieldName", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldName"].ToString());
                                            ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[x]["FieldLabel"].ToString());
                                            ClsUtility.AddParameters("@ControlId", SqlDbType.Int, (dsImportForms.Tables[2].Columns.Contains("ControlId")) ? dsImportForms.Tables[2].Rows[x]["ControlId"].ToString() : "");
                                            ClsUtility.AddParameters("@SelectListVal", SqlDbType.VarChar, strSelectLstVal);
                                            ClsUtility.AddParameters("@BusRuleIdValAll", SqlDbType.VarChar, strBusRuleIdVal);
                                            ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["Seq"].ToString());
                                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                            ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["Predefined"].ToString());
                                            ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, FacilityId.ToString());
                                            if (iNewFeatureId == 126)
                                            {
                                                ClsUtility.AddParameters("@PatientRegistration", SqlDbType.Int, "1");
                                            }
                                            //theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                            DataTable theFieldDT = (DataTable)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsDBUtility.ObjectEnum.DataTable);
                                            if (theFieldDT.Rows.Count > 1)
                                            {
                                                DataView dvFieldDV = new DataView();
                                                dvFieldDV = theFieldDT.DefaultView;
                                                DataTable dtField = new DataTable();
                                                dvFieldDV.RowFilter = "FormFieldID <>'0'";
                                                dtField = dvFieldDV.ToTable();
                                                iNewFieldId = System.Convert.ToInt32(dtField.Rows[0][0].ToString());

                                            }
                                            else
                                                iNewFieldId = System.Convert.ToInt32(theFieldDT.Rows[0][0].ToString());
                                            if (dsImportForms.Tables[2].Rows[x]["Predefined"].ToString() != "1")
                                            {

                                                ClsUtility.Init_Hashtable();
                                                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                                                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[x]["FieldName"].ToString());
                                                ClsUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["ControlId"].ToString());
                                                ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["Predefined"].ToString());
                                                ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldId"].ToString());
                                                theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                            }
                                            ///////Import GridView Control-Create Table//////////////////////////////////

                                            if (dsImportForms.Tables[1].Rows[j]["IsGridView"].ToString() == "1" && dsImportForms.Tables[2].Rows[x]["Predefined"].ToString() != "1")
                                            {
                                                string strTableNameSection = "DTL_CUSTOMFORM_" + dsImportForms.Tables[1].Rows[j]["SectionName"].ToString() + "_" + strgridFeaturename;
                                                ClsUtility.Init_Hashtable();
                                                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableNameSection);
                                                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[x]["FieldName"].ToString());
                                                ClsUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["ControlId"].ToString());
                                                ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldId"].ToString());
                                                theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreationGridView_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                            }

                                            ////////Import Feild ICDCode Linking////////////////////////////////////////////
                                            if ((dsImportForms.Tables[11].Rows[0].ItemArray[0].ToString() != "0") && (dsImportForms.Tables[2].Rows[x]["ControlId"].ToString() == "16"))
                                            {
                                                DataView dvFilteredRow = new DataView();
                                                dvFilteredRow = dsImportForms.Tables[11].DefaultView;
                                                DataTable dtRow = new DataTable();
                                                dvFilteredRow.RowFilter = "FieldId='" + dsImportForms.Tables[2].Rows[x]["FieldId"].ToString() + "'";
                                                dtRow = dvFilteredRow.ToTable();
                                                if (dtRow.Rows.Count > 0)
                                                {
                                                    if (dsImportForms.Tables[2].Rows[x]["FieldId"].ToString() == iNewFieldId.ToString())
                                                    {
                                                        ClsUtility.Init_Hashtable();
                                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldId"].ToString());
                                                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_DeleteFieldICDCode_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                                    }
                                                    for (int q = 0; q < dtRow.Rows.Count; q++)
                                                    {
                                                        ClsUtility.Init_Hashtable();
                                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, iNewFieldId.ToString());
                                                        ClsUtility.AddParameters("@BlockId", SqlDbType.Int, dtRow.Rows[q]["BlockId"].ToString());
                                                        ClsUtility.AddParameters("@SubBlockId", SqlDbType.Int, dtRow.Rows[q]["SubBlockId"].ToString());
                                                        ClsUtility.AddParameters("@Chapterid", SqlDbType.Int, dtRow.Rows[q]["Chapterid"].ToString());
                                                        ClsUtility.AddParameters("@CodeId", SqlDbType.Int, dtRow.Rows[q]["CodeId"].ToString());
                                                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, dtRow.Rows[q]["UserId"].ToString());
                                                        ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                                                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dtRow.Rows[q]["Predefined"].ToString());
                                                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveICD10CodeItems_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                                                    }
                                                }


                                            }
                                            ///////////////////////////////////////////////////
                                            #region "Update/Insert Conditional Fields"
                                            if (dsImportForms.Tables.Count > 7)
                                            {
                                                if (dsImportForms.Tables[7].Rows.Count > 0)
                                                {
                                                    if (dsImportForms.Tables[7].Rows[0][0].ToString() != "")
                                                    {
                                                        if (dsImportForms.Tables[7].Rows[0][0].ToString() != "0")
                                                        {
                                                            for (int n = 0; n < dsImportForms.Tables[7].Rows.Count; n++)
                                                            {
                                                                if (dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[n]["FeatureId"].ToString() && dsImportForms.Tables[1].Rows[j]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["SectionId"].ToString())
                                                                {
                                                                    //store comma separated select list val for field
                                                                    string strConSelectLstVal = string.Empty;
                                                                    if (dsImportForms.Tables.Count > 8)
                                                                    {
                                                                        if (dsImportForms.Tables[8].Rows[0][0].ToString() != "0")
                                                                        {
                                                                            for (int l = 0; l < dsImportForms.Tables[8].Rows.Count; l++)
                                                                            {
                                                                                if (dsImportForms.Tables[8].Rows[l]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[n]["FeatureId"].ToString() && dsImportForms.Tables[8].Rows[l]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString() && dsImportForms.Tables[8].Rows[l]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldSectionId"].ToString())
                                                                                {
                                                                                    if (strConSelectLstVal == "")
                                                                                        strConSelectLstVal = dsImportForms.Tables[8].Rows[l]["ListVal"].ToString();
                                                                                    else
                                                                                        strConSelectLstVal = strConSelectLstVal + ";" + dsImportForms.Tables[8].Rows[l]["ListVal"].ToString();
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    //busrule id and val comma separated value, e.g. BusRuleId-Value(val used in case of min and max)
                                                                    string strConBusRuleIdVal = string.Empty;
                                                                    if (dsImportForms.Tables.Count > 9)
                                                                    {
                                                                        if (dsImportForms.Tables[9].Rows[0][0].ToString() != "0")
                                                                        {
                                                                            for (int z = 0; z < dsImportForms.Tables[9].Rows.Count; z++)
                                                                            {
                                                                                if (dsImportForms.Tables[9].Rows[z]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString() && dsImportForms.Tables[9].Rows[z]["Predefined"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString())
                                                                                {
                                                                                    if (strConBusRuleIdVal == "")
                                                                                        strConBusRuleIdVal = dsImportForms.Tables[9].Rows[z]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[9].Rows[z]["Value"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value1") && dsImportForms.Tables[9].Rows[z]["Value1"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value1"].ToString() : "Null");
                                                                                    else
                                                                                        strConBusRuleIdVal = strConBusRuleIdVal + "," + dsImportForms.Tables[9].Rows[z]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[9].Columns.Contains("Value") && dsImportForms.Tables[9].Rows[z]["Value"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[9].Columns.Contains("Value1") && dsImportForms.Tables[9].Rows[z]["Value1"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value1"].ToString() : "Null");
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    if (dsImportForms.Tables[2].Rows[x]["fieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["FieldId"].ToString() && dsImportForms.Tables[2].Rows[x]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["SectionId"].ToString() && dsImportForms.Tables[2].Rows[x]["featureId"].ToString() == dsImportForms.Tables[7].Rows[n]["featureId"].ToString())
                                                                    {
                                                                        ClsUtility.Init_Hashtable();
                                                                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                                                        ClsUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, iNewFieldId.ToString());
                                                                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["FieldName"].ToString());
                                                                        ClsUtility.AddParameters("@ConFieldId", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString());
                                                                        ClsUtility.AddParameters("@ConFieldName", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldName"].ToString());
                                                                        ClsUtility.AddParameters("@ConFieldLabel", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ConditionalFieldLabel"].ToString());
                                                                        ClsUtility.AddParameters("@ControlId", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ControlId"].ToString());
                                                                        ClsUtility.AddParameters("@ConControlId", SqlDbType.Int, (dsImportForms.Tables[7].Columns.Contains("ConditionalFieldControlId")) ? dsImportForms.Tables[7].Rows[n]["ConditionalFieldControlId"].ToString() : "");
                                                                        ClsUtility.AddParameters("@ConSelectListVal", SqlDbType.VarChar, strConSelectLstVal);
                                                                        ClsUtility.AddParameters("@ConBusRuleIdValAll", SqlDbType.VarChar, strConBusRuleIdVal);
                                                                        ClsUtility.AddParameters("@ConSeq", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldSequence"].ToString());
                                                                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                                                        ClsUtility.AddParameters("@ConPredefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString());
                                                                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["fieldpredefined"].ToString());
                                                                        ClsUtility.AddParameters("@ConSectionId", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldSectionId"].ToString());
                                                                        ClsUtility.AddParameters("@ModdecodeName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["Mod"].ToString());
                                                                        ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["SystemId"].ToString());
                                                                        //theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                                                        if (iNewFeatureId == 126)
                                                                        {
                                                                            theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportRegistrationConditionalField_Futures", ClsDBUtility.ObjectEnum.DataRow);
                                                                        }
                                                                        else
                                                                        {
                                                                            theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportConditionalField_Futures", ClsDBUtility.ObjectEnum.DataRow);
                                                                        }
                                                                        iNewConFieldId = System.Convert.ToInt32(theDR[0].ToString());

                                                                        if (dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString() != "1")
                                                                        {

                                                                            ClsUtility.Init_Hashtable();
                                                                            ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                                                                            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ConditionalFieldName"].ToString());
                                                                            ClsUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldControlId"].ToString());
                                                                            ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString());
                                                                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, iNewConFieldId.ToString());
                                                                            theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                                                        }
                                                                    }


                                                                }
                                                            }
                                                        }
                                                    }//0 closed
                                                }
                                            }
                                            #endregion

                                        }//feature id and section id if condition closes here

                                    }
                                    #endregion

                                }
                            }
                        }
                    }

                    //john - special form linking
                    for (int k = 0; k < dsImportForms.Tables[14].Rows.Count; k++)
                    {
                        if (dsImportForms.Tables[14].Rows[k]["moduleid"].ToString() != "0" && dsImportForms.Tables[14].Rows[k]["featureid"].ToString() != "0")
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dsImportForms.Tables[14].Rows[k]["moduleid"].ToString());
                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[14].Rows[k]["featureid"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                            ClsUtility.AddParameters("@ModuleName", SqlDbType.VarChar, dsImportForms.Tables[14].Rows[k]["modulename"].ToString());
                            theRowAffected_SpLnkForms = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_spLnkForms_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                        }
                    }

                    if (DSFormVer != null)
                    {
                        if (DSFormVer.Tables.Count > 0)
                        {
                            ///Save Update Form Version Masters
                            if (DSFormVer.Tables[0].Rows.Count > 0)
                            {
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@VerId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["VerId"].ToString());
                                ClsUtility.AddParameters("@VersionName", SqlDbType.Decimal, DSFormVer.Tables[0].Rows[0]["VersionName"].ToString());
                                ClsUtility.AddParameters("@VersionDate", SqlDbType.VarChar, DSFormVer.Tables[0].Rows[0]["VersionDate"].ToString());
                                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                ClsUtility.AddParameters("@UserId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["UserId"].ToString());
                                DataRow theDRVer = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveFormVersion_Futures", ClsDBUtility.ObjectEnum.DataRow);
                                if (DSFormVer.Tables[1].Rows.Count > 0 && theDRVer.ItemArray.Any())
                                {
                                    foreach (DataRow theDRdetails in DSFormVer.Tables[1].Rows)
                                    {
                                        ClsUtility.Init_Hashtable();
                                        ClsUtility.AddParameters("@VerId", SqlDbType.Int, theDRVer["VersionId"].ToString());
                                        ClsUtility.AddParameters("@TabId", SqlDbType.Decimal, theDRdetails["TabId"].ToString());
                                        ClsUtility.AddParameters("@FunctionId", SqlDbType.Int, theDRdetails["FunctionId"].ToString());
                                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["UserId"].ToString());
                                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveFormVersionDetails_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                    }
                                }
                                if (DSFormVer.Tables[2].Rows.Count > 0 && theDRVer.ItemArray.Any())
                                {
                                    foreach (DataRow theDRdetails in DSFormVer.Tables[2].Rows)
                                    {
                                        ClsUtility.Init_Hashtable();
                                        ClsUtility.AddParameters("@VerId", SqlDbType.Int, theDRVer["VersionId"].ToString());
                                        ClsUtility.AddParameters("@TabId", SqlDbType.Decimal, theDRdetails["TabId"].ToString());
                                        ClsUtility.AddParameters("@SectionId", SqlDbType.Decimal, theDRdetails["SectionId"].ToString());
                                        ClsUtility.AddParameters("@FunctionId", SqlDbType.Int, theDRdetails["FunctionId"].ToString());
                                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["UserId"].ToString());
                                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveFormVersionDetails_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                    }
                                }
                                if (DSFormVer.Tables[3].Rows.Count > 0 && theDRVer.ItemArray.Any())
                                {
                                    foreach (DataRow theDRdetails in DSFormVer.Tables[3].Rows)
                                    {
                                        ClsUtility.Init_Hashtable();
                                        ClsUtility.AddParameters("@VerId", SqlDbType.Int, theDRVer["VersionId"].ToString());
                                        ClsUtility.AddParameters("@TabId", SqlDbType.Decimal, theDRdetails["TabId"].ToString());
                                        ClsUtility.AddParameters("@SectionId", SqlDbType.Decimal, theDRdetails["SectionId"].ToString());
                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Decimal, theDRdetails["FieldId"].ToString());
                                        ClsUtility.AddParameters("@FunctionId", SqlDbType.Int, theDRdetails["FunctionId"].ToString());
                                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["UserId"].ToString());
                                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveFormVersionDetails_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                    }
                                }
                                if (DSFormVer.Tables[4].Rows.Count > 0 && theDRVer.ItemArray.Any())
                                {
                                    foreach (DataRow theDRdetails in DSFormVer.Tables[4].Rows)
                                    {
                                        ClsUtility.Init_Hashtable();
                                        ClsUtility.AddParameters("@VerId", SqlDbType.Int, theDRVer["VersionId"].ToString());
                                        ClsUtility.AddParameters("@Predefined", SqlDbType.Decimal, theDRdetails["Predefined"].ToString());
                                        ClsUtility.AddParameters("@ConPredefined", SqlDbType.Decimal, theDRdetails["ConPredefined"].ToString());
                                        ClsUtility.AddParameters("@ConFieldId", SqlDbType.Decimal, theDRdetails["ConFieldId"].ToString());
                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Decimal, theDRdetails["FieldId"].ToString());
                                        ClsUtility.AddParameters("@FunctionId", SqlDbType.Int, theDRdetails["FunctionId"].ToString());
                                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["UserId"].ToString());
                                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveFormConditionalVersionDetails_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                                    }
                                }
                            }
                        }
                    }
                }
                

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                //return iFeatureId;
                return 1;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
        }


        //import home forms
        public int ImportHomeForms(DataSet dsImportForms, int iUserId, int iCountryId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FormDetail = new ClsObject();
                FormDetail.Connection = this.Connection;
                FormDetail.Transaction = this.Transaction;

                DataRow theDR;

                int iNewFeatureId; //this variable will be used to store featureid for all new rows

                string strTableName = string.Empty;

                for (int i = 0; i < dsImportForms.Tables[0].Rows.Count; i++)
                {

                    //string[] strFeatureName = new string[10];
                    //strFeatureName = dsImportForms.Tables[0].Rows[i]["FeatureName"].ToString().Split(' ');
                    //strTableName = "";
                    //for (int j = 0; j < strFeatureName.Length; j++)
                    //{
                    //    if (j > 0)
                    //        strTableName += "_" + strFeatureName[j];
                    //    else
                    //        strTableName += strFeatureName[j];

                    //}
                    //strTableName = "DTL_FBCUSTOMFIELD_" + strTableName;

                    //save mst_feature data
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString());
                    ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, dsImportForms.Tables[0].Rows[i]["FeatureName"].ToString());
                    ClsUtility.AddParameters("@ReportFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["ReportFlag"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["DeleteFlag"].ToString());
                    ClsUtility.AddParameters("@AdminFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["AdminFlag"].ToString());
                    //ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["UserID"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                    ClsUtility.AddParameters("@OptionalFlag", SqlDbType.Int, (dsImportForms.Tables[0].Columns.Contains("OptionalFlag")) ? dsImportForms.Tables[0].Rows[i]["OptionalFlag"].ToString() : "");
                    ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["SystemId"].ToString());
                    ClsUtility.AddParameters("@Published", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["Published"].ToString());
                    //ClsUtility.AddParameters("@CountryId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["CountryId"].ToString());
                    ClsUtility.AddParameters("@CountryId", SqlDbType.Int, iCountryId.ToString());
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["ModuleId"].ToString());
                    if (dsImportForms.Tables[0].Columns.Contains("MultiVisit"))
                        ClsUtility.AddParameters("@MultiVisit", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["MultiVisit"].ToString());

                    theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportHomeForm_Futures", ClsDBUtility.ObjectEnum.DataRow);
                    iNewFeatureId = System.Convert.ToInt32(theDR[0].ToString());

                    //for home page and its dtl
                    //
                    for (int j = 0; j < dsImportForms.Tables[1].Rows.Count; j++)
                    {
                        int iHomePageId;
                        ClsUtility.Init_Hashtable();

                        ClsUtility.AddParameters("@HomePageName", SqlDbType.VarChar, dsImportForms.Tables[1].Rows[j]["Name"].ToString());
                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                        theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportMstHomePage_Futures", ClsDBUtility.ObjectEnum.DataRow);
                        iHomePageId = System.Convert.ToInt32(theDR[0].ToString());

                        for (int k = 0; k < dsImportForms.Tables[2].Rows.Count; k++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@HomePageId", SqlDbType.Int, iHomePageId.ToString());
                            ClsUtility.AddParameters("@IndicatorName", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[k]["IndicatorName"].ToString());
                            if (dsImportForms.Tables[2].Columns.Contains("Query"))
                                ClsUtility.AddParameters("@Query", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[k]["Query"].ToString());

                            ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[2].Rows[k]["Seq"].ToString());
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, iUserId.ToString());
                            theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportdtlHomePage_Futures", ClsDBUtility.ObjectEnum.DataRow);
                            //iNewModuleId = System.Convert.ToInt32(theDR[0].ToString());
                        }
                    }
                }



                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                //return iFeatureId;
                return 1;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
        }

    }
}
