using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;
using Application.Common;

namespace BusinessProcess.FormBuilder
{
    public class BFormBuilder : ProcessBase, IFormBuilder
    {
        public DataTable SystemDate()
        {
            lock (this)
            {
                string theSQL = "select convert(date, getdate()) as [Date], convert(varchar(8), convert(time, getdate())) as [Time]";
                ClsObject DateManager = new ClsObject();
                ClsUtility.Init_Hashtable();

                DataTable theCurrentDt = (DataTable)DateManager.ReturnObject(ClsUtility.theParams, theSQL, ClsDBUtility.ObjectEnum.DataTable);
                return theCurrentDt;
            }
        }
        public DataSet GetFormDetail(Int32 iFeatureId)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                DataSet dsRes;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@iFeatureId", SqlDbType.Int, iFeatureId.ToString());
                //dsRes = (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_FetchUpdateFormDetail_Futures", ClsDBUtility.ObjectEnum.DataSet);
                if (iFeatureId == 0)
                {
                    dsRes = (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_FetchPharmacyFormStaticFieldDetail_Futures", ClsDBUtility.ObjectEnum.DataSet);
                }
                else
                {
                    dsRes = (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_FetchUpdateFormDetail_Futures", ClsDBUtility.ObjectEnum.DataSet);
                }
                return dsRes;
            }
        }


        public bool CheckDuplicate(string strSearchTable, string strSearchColumn, string strSearchValue, String iDeleteFlagCheck, int iModuleId)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                DataTable dtRes;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@strSearchTable", SqlDbType.VarChar, strSearchTable);
                ClsUtility.AddParameters("@strSearchColumn1", SqlDbType.VarChar, strSearchColumn);
                ClsUtility.AddParameters("@strSearchValue1", SqlDbType.VarChar, strSearchValue.Replace("'", ""));
                ClsUtility.AddParameters("@strSearchColumn2", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@strSearchValue2", SqlDbType.VarChar, "");
                ClsUtility.AddParameters("@iDeleteFlagCheck", SqlDbType.Int, iDeleteFlagCheck);
                ClsUtility.AddParameters("@iModuleId", SqlDbType.Int, iModuleId.ToString());
                //dtRes = (DataTable)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_DuplicateValue_Futures", ClsDBUtility.ObjectEnum.DataTable);
                dtRes = (DataTable)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_DuplicateValue_Futures", ClsDBUtility.ObjectEnum.DataTable);
                if (dtRes.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool CheckDuplicate(string strSearchTable, string strSearchColumn1, string strSearchValue1, string strSearchColumn2, string strSearchValue2, String iDeleteFlagCheck, int iModuleId)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                DataTable dtRes;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@strSearchTable", SqlDbType.VarChar, strSearchTable);
                ClsUtility.AddParameters("@strSearchColumn1", SqlDbType.VarChar, strSearchColumn1);
                ClsUtility.AddParameters("@strSearchValue1", SqlDbType.VarChar, strSearchValue1);
                ClsUtility.AddParameters("@strSearchColumn2", SqlDbType.VarChar, strSearchColumn2);
                ClsUtility.AddParameters("@strSearchValue2", SqlDbType.VarChar, strSearchValue2);
                ClsUtility.AddParameters("@iDeleteFlagCheck", SqlDbType.Int, iDeleteFlagCheck);
                ClsUtility.AddParameters("@iModuleId", SqlDbType.Int, iModuleId.ToString());
                //dtRes = (DataTable)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_DuplicateValue_Futures", ClsDBUtility.ObjectEnum.DataTable);
                dtRes = (DataTable)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_DuplicateValue_Futures", ClsDBUtility.ObjectEnum.DataTable);
                if (dtRes.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public int RetrieveMaxId(String strSearchIn)
        {
            lock (this)
            {
                ClsObject objClsObj = new ClsObject();
                DataRow drRes;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@strTableName", SqlDbType.VarChar, strSearchIn);
                //drRes=(DataRow)objClsObj.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_FetchMaxValue_Futures", ClsDBUtility.ObjectEnum.DataRow);
                drRes = (DataRow)objClsObj.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_FetchMaxValue_Futures", ClsDBUtility.ObjectEnum.DataRow);
                return System.Convert.ToInt32(drRes[0].ToString());
            }
        }
        public int SaveFormDetail(DataSet dsSaveFormData, DataTable dtFieldDetails, DataTable dtbusinessRules, DataSet DSFormVer)
        {

            //try
            //{
            this.Connection = DataMgr.GetConnection();
            this.Transaction = DataMgr.BeginTransaction(this.Connection);

            ClsObject FormDetail = new ClsObject();
            FormDetail.Connection = this.Connection;
            FormDetail.Transaction = this.Transaction;
            int theRowAffected = 0;
            DataRow theDR;
            int iFeatureId;
            int iRegFlag;
            string Pharmacy = "";
            string FeatureName = "";
            int iSectionId;
            string strTableName = string.Empty;
            //save mst_feature data
            ClsUtility.Init_Hashtable();
            if (dsSaveFormData.Tables[0].Rows[0]["InsertUpdateStatus"].ToString() == "I")
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, "0");
            else
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["FeatureId"].ToString());

            ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, dsSaveFormData.Tables[0].Rows[0]["FeatureName"].ToString().Replace("'", ""));
            ClsUtility.AddParameters("@ReportFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["ReportFlag"].ToString());
            ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["DeleteFlag"].ToString());
            ClsUtility.AddParameters("@AdminFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["AdminFlag"].ToString());
            //ClsUtility.AddParameters("@OptionalFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["OptionalFlag"].ToString());
            ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["SystemId"].ToString());
            ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["UserID"].ToString());
            ClsUtility.AddParameters("@Published", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["Published"].ToString());
            ClsUtility.AddParameters("@CountryId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["CountryId"].ToString());
            ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["ModuleId"].ToString());
            ClsUtility.AddParameters("@MultiVisit", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["MultiVisit"].ToString());
            //theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_SaveMstFeature_Futures", ClsDBUtility.ObjectEnum.DataRow);
            theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveMstFeature_Futures", ClsDBUtility.ObjectEnum.DataRow);
            iFeatureId = System.Convert.ToInt32(theDR[0].ToString());
            iRegFlag = System.Convert.ToInt32(theDR[2].ToString());
            Pharmacy = theDR[1].ToString();
            if (iFeatureId != 0)
            {
                if (dtbusinessRules.Rows.Count == 0)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                    ClsUtility.AddParameters("@BusRuleid", SqlDbType.Int, "1");
                    ClsUtility.AddParameters("@value", SqlDbType.Int, "1");
                    ClsUtility.AddParameters("@value1", SqlDbType.Int, "1");
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, "1");
                    ClsUtility.AddParameters("@setType", SqlDbType.Int, "1");
                    ClsUtility.AddParameters("@counter", SqlDbType.Int, "0");
                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_DeleteFormBusinessRules", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                for (int i = 0; i < dtbusinessRules.Rows.Count; i++)
                {

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                    ClsUtility.AddParameters("@BusRuleid", SqlDbType.Int, dtbusinessRules.Rows[i]["BusRuleId"].ToString());
                    ClsUtility.AddParameters("@value", SqlDbType.Int, dtbusinessRules.Rows[i]["Value"].ToString());
                    ClsUtility.AddParameters("@value1", SqlDbType.Int, dtbusinessRules.Rows[i]["Value1"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["UserID"].ToString());
                    ClsUtility.AddParameters("@setType", SqlDbType.Int, dtbusinessRules.Rows[i]["SetType"].ToString());
                    ClsUtility.AddParameters("@counter", SqlDbType.Int, i.ToString());
                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_SaveUpdateFormBusinessRules", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }
            }


            string[] strFeatureName = new string[10];
            strFeatureName = dsSaveFormData.Tables[0].Rows[0]["FeatureName"].ToString().Split(' ');
            for (int j = 0; j < strFeatureName.Length; j++)
            {
                if (j > 0)
                    strTableName += "_" + strFeatureName[j];
                else
                    strTableName += strFeatureName[j];


            }
            FeatureName = strTableName;
            strTableName = "DTL_FBCUSTOMFIELD_" + strTableName;
            //save mst_section data

            //foreach (DataRow drFormData in dsSaveFormData.Tables[1])
            for (int i = 0; i < dsSaveFormData.Tables[1].Rows.Count; i++)
            {
                if (dsSaveFormData.Tables[1].Rows[i]["DeleteFlag"].ToString() == "0")
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["SectionId"].ToString());
                    ClsUtility.AddParameters("@SectionName", SqlDbType.VarChar, dsSaveFormData.Tables[1].Rows[i]["SectionName"].ToString());
                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["Sequence"].ToString());
                    ClsUtility.AddParameters("@CustomFlag", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["CustomFlag"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["DeleteFlag"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["UserId"].ToString());
                    //ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["FeatureId"].ToString());
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                    ClsUtility.AddParameters("@IsGridView", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["IsGridView"].ToString());
                    //theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_SaveMstSection_Futures", ClsDBUtility.ObjectEnum.DataRow);
                    theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveMstSection_Futures", ClsDBUtility.ObjectEnum.DataRow);
                }
            }

            //save lnk_form data
            //foreach (DataRow drFormData in dsSaveFormData.Tables[2])
            for (int i = 0; i < dsSaveFormData.Tables[2].Rows.Count; i++)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Id"].ToString());
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                //ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FeatureId"].ToString());
                if (Pharmacy.Contains("Pharmacy_") && iRegFlag == 2 && Convert.ToInt32(dsSaveFormData.Tables[2].Rows[i]["SectionId"]) == 0)
                {
                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, theDR[0].ToString());
                }
                else
                {
                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["SectionId"].ToString());
                }
                ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString());
                ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dsSaveFormData.Tables[2].Rows[i]["FieldLabel"].ToString());
                ClsUtility.AddParameters("@AdditionalInformation", SqlDbType.VarChar, dsSaveFormData.Tables[2].Rows[i]["AdditionalInformation"].ToString());
                ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Sequence"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["UserId"].ToString());
                ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString());
                //theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_SaveLnkForm_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveLnkForm_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                DataView dvFilteredRow = new DataView();
                dvFilteredRow = dtFieldDetails.DefaultView;
                DataTable dtRow = new DataTable();

                if (dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString() == "71" && dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString() == "1")
                    dvFilteredRow.RowFilter = "ID='71' and predefine=" + dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString();
                else
                    dvFilteredRow.RowFilter = "ID='" + dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString() + "' and predefine=" + dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString();

                dtRow = dvFilteredRow.ToTable();

                /* ----------Added by Paritosh -Implementing Grid View------------*/
                DataView dvFilteredRowGridView = new DataView();
                dvFilteredRowGridView = dsSaveFormData.Tables[1].DefaultView;
                dvFilteredRowGridView.RowFilter = "SectionId = " + dsSaveFormData.Tables[2].Rows[i]["SectionId"].ToString();
                if (dvFilteredRowGridView[0]["IsGridView"].ToString() == "1")
                {
                    string strTableNameSection = "DTL_CUSTOMFORM_" + dvFilteredRowGridView[0]["SectionName"].ToString() + "_" + FeatureName;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableNameSection);
                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtRow.Rows[0]["FieldName"].ToString());
                    ClsUtility.AddParameters("@DataType", SqlDbType.Int, dtRow.Rows[0]["ControlId"].ToString());
                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString());
                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreationGridView_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                /* ----------  end             ------------*/

                if (dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString() != "71" && dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString() != "1")
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtRow.Rows[0]["FieldName"].ToString());
                    ClsUtility.AddParameters("@DataType", SqlDbType.Int, dtRow.Rows[0]["ControlId"].ToString());
                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString());
                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString());
                    //theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_CustomTableCreation_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                /*  if (dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString() != "71" && dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString() != "0")
                  {
                      ClsUtility.Init_Hashtable();
                      ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                      ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtRow.Rows[0]["FieldName"].ToString());
                      ClsUtility.AddParameters("@DataType", SqlDbType.Int, dtRow.Rows[0]["ControlId"].ToString());
                      ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString());
                      ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString());
                      theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                  }*/

            }

            //Delete fields selected from remove field in formbuilder while in update mode
            for (int i = 0; i < dsSaveFormData.Tables[3].Rows.Count; i++)
            {
                string Tblname;

                if (iFeatureId == 126)
                {
                    Tblname = strTableName;
                }
                else if (iFeatureId == System.Convert.ToInt32(dsSaveFormData.Tables[0].Rows[0]["FeatureId"]) && iRegFlag == 2)
                {
                    Tblname = strTableName;
                }
                else
                {

                    if (dsSaveFormData.Tables[3].Rows[i]["IsGridView"].ToString() == "1")
                    {
                        Tblname = "DTL_CUSTOMFORM_" + dsSaveFormData.Tables[3].Rows[i]["SectionName"].ToString() + "_" + FeatureName;
                    }
                    else
                    {
                        Tblname = strTableName;
                    }
                }



                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, dsSaveFormData.Tables[3].Rows[i]["Id"].ToString());
                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsSaveFormData.Tables[3].Rows[i]["FieldName"].ToString());
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, Tblname);
                //theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_RemoveFieldInFormBuilder_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_RemoveFieldInFormBuilder_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
            }

            //Delete sections selected from remove section in formbuilder while in update mode
            if (dsSaveFormData.Tables.Count > 4)
            {

                if (dsSaveFormData.Tables.Contains("MstTab"))// || dsSaveFormData.Tables[5].TableName == "MstTab")
                {
                    for (int i = 0; i < dsSaveFormData.Tables["MstTab"].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@TabID", SqlDbType.Int, dsSaveFormData.Tables["MstTab"].Rows[i]["TabID"].ToString());
                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                        ClsUtility.AddParameters("@TabName", SqlDbType.VarChar, dsSaveFormData.Tables["MstTab"].Rows[i]["TabName"].ToString());
                        ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsSaveFormData.Tables["MstTab"].Rows[i]["seq"].ToString());
                        ClsUtility.AddParameters("@Signature", SqlDbType.Int, dsSaveFormData.Tables["MstTab"].Rows[i]["Signature"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables["MstTab"].Rows[i]["UserId"].ToString());
                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveMstTab_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (dsSaveFormData.Tables.Contains("LnkSectionTab"))// || dsSaveFormData.Tables[6].TableName == "LnkSectionTab")
                {

                    for (int i = 0; i < dsSaveFormData.Tables["LnkSectionTab"].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, dsSaveFormData.Tables["LnkSectionTab"].Rows[i]["ID"].ToString());
                        ClsUtility.AddParameters("@TabID", SqlDbType.Int, dsSaveFormData.Tables["LnkSectionTab"].Rows[i]["TabID"].ToString());
                        ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables["LnkSectionTab"].Rows[i]["SectionId"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables["LnkSectionTab"].Rows[i]["UserId"].ToString());
                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveLnkTabSection_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (dsSaveFormData.Tables.Contains("DeleteSection"))
                {
                    for (int i = 0; i < dsSaveFormData.Tables["DeleteSection"].Rows.Count; i++)
                    {

                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables["DeleteSection"].Rows[i]["SectionId"].ToString());
                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                        ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                        //theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_RemoveSectionAndFieldInFormBuilder_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_RemoveSectionAndFieldInFormBuilder_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                if (dsSaveFormData.Tables.Contains("DeleteTab"))
                {
                    for (int i = 0; i < dsSaveFormData.Tables["DeleteTab"].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@TabID", SqlDbType.Int, dsSaveFormData.Tables["DeleteTab"].Rows[i]["TabId"].ToString());
                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_RemoveTabInFormBuilder_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
            }
            if (object.Equals(DSFormVer, null) == false && DSFormVer.Tables.Count > 0)
            {
                ///Save Update Form Version Masters
                if (DSFormVer.Tables[0].Rows.Count > 0)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@VerId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["VerId"].ToString());
                    ClsUtility.AddParameters("@VersionName", SqlDbType.Decimal, DSFormVer.Tables[0].Rows[0]["VersionName"].ToString());
                    ClsUtility.AddParameters("@VersionDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",DSFormVer.Tables[0].Rows[0]["VersionDate"]).ToString());
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["UserId"].ToString());
                    ClsUtility.AddParameters("@PaperVersion", SqlDbType.VarChar, DSFormVer.Tables[0].Rows[0]["PaperVersion"].ToString());
                    DataRow theDRVer = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveFormVersion_Futures", ClsDBUtility.ObjectEnum.DataRow);
                    if (DSFormVer.Tables[1].Rows.Count > 0 && theDRVer.ItemArray.Any())
                    {
                        foreach (DataRow theDRdetails in DSFormVer.Tables[1].Rows)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@VerId", SqlDbType.Int, theDRVer["VersionId"].ToString());
                            ClsUtility.AddParameters("@TabId", SqlDbType.Decimal, theDRdetails["TabId"].ToString());
                            ClsUtility.AddParameters("@FunctionId", SqlDbType.Int, theDRdetails["FunctionId"].ToString());
                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
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
                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
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
                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, DSFormVer.Tables[0].Rows[0]["UserId"].ToString());
                            theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveFormVersionDetails_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }
                }
            }


            DataMgr.CommitTransaction(this.Transaction);
            DataMgr.ReleaseConnection(this.Connection);
            return iFeatureId;
            // }
            //catch
            //{
            //    DataMgr.RollBackTransation(this.Transaction);
            //     throw;
            //}
            //finally
            //{
            //    if (this.Connection != null)
            //        DataMgr.ReleaseConnection(this.Connection);

            //}
        }

        public int SaveCustomRegistrationFormDetail(DataSet dsSaveFormData, DataTable dtFieldDetails)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FormDetail = new ClsObject();
                FormDetail.Connection = this.Connection;
                FormDetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                DataRow theDR;
                int iFeatureId;
                string strTableName = string.Empty;
                //save mst_feature data
                ClsUtility.Init_Hashtable();
                if (dsSaveFormData.Tables[0].Rows[0]["InsertUpdateStatus"].ToString() == "I")
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, "0");
                else
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["FeatureId"].ToString());

                ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, dsSaveFormData.Tables[0].Rows[0]["FeatureName"].ToString().Replace("'", ""));
                ClsUtility.AddParameters("@ReportFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["ReportFlag"].ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["DeleteFlag"].ToString());
                ClsUtility.AddParameters("@AdminFlag", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["AdminFlag"].ToString());

                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["SystemId"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["UserID"].ToString());
                ClsUtility.AddParameters("@Published", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["Published"].ToString());
                ClsUtility.AddParameters("@CountryId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["CountryId"].ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["ModuleId"].ToString());
                ClsUtility.AddParameters("@MultiVisit", SqlDbType.Int, dsSaveFormData.Tables[0].Rows[0]["MultiVisit"].ToString());

                theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveMstFeature_Futures", ClsDBUtility.ObjectEnum.DataRow);
                iFeatureId = System.Convert.ToInt32(theDR[0].ToString());

                string[] strFeatureName = new string[10];
                strFeatureName = dsSaveFormData.Tables[0].Rows[0]["FeatureName"].ToString().Split(' ');
                for (int j = 0; j < strFeatureName.Length; j++)
                {
                    if (j > 0)
                        strTableName += "_" + strFeatureName[j];
                    else
                        strTableName += strFeatureName[j];

                }
                for (int i = 0; i < dsSaveFormData.Tables[1].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["SectionId"].ToString());
                    ClsUtility.AddParameters("@SectionName", SqlDbType.VarChar, dsSaveFormData.Tables[1].Rows[i]["SectionName"].ToString());
                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["Sequence"].ToString());
                    ClsUtility.AddParameters("@CustomFlag", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["CustomFlag"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["DeleteFlag"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[1].Rows[i]["UserId"].ToString());
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                    theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveMstSection_Futures", ClsDBUtility.ObjectEnum.DataRow);
                }

                //save lnk_form data
                for (int i = 0; i < dsSaveFormData.Tables[2].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Id"].ToString());
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["SectionId"].ToString());
                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString());
                    ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dsSaveFormData.Tables[2].Rows[i]["FieldLabel"].ToString());
                    ClsUtility.AddParameters("@AdditionalInformation", SqlDbType.VarChar, dsSaveFormData.Tables[2].Rows[i]["AdditionalInformation"].ToString());
                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Sequence"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["UserId"].ToString());
                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString());
                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveLnkForm_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    DataView dvFilteredRow = new DataView();
                    dvFilteredRow = dtFieldDetails.DefaultView;
                    DataTable dtRow = new DataTable();

                    if (dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString().Contains("7100000") == true)
                        dvFilteredRow.RowFilter = "ID='71' and predefine=" + dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString();
                    else
                        dvFilteredRow.RowFilter = "ID='" + dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString() + "' and predefine=" + dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString();

                    dtRow = dvFilteredRow.ToTable();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtRow.Rows[0]["FieldName"].ToString());
                    ClsUtility.AddParameters("@DataType", SqlDbType.Int, dtRow.Rows[0]["ControlId"].ToString());
                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["Predefined"].ToString());
                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsSaveFormData.Tables[2].Rows[i]["FieldId"].ToString());
                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_PatientRegistrationCustomTableCreation_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }

                //Delete fields selected from remove field in formbuilder while in update mode
                for (int i = 0; i < dsSaveFormData.Tables[3].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, dsSaveFormData.Tables[3].Rows[i]["Id"].ToString());
                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsSaveFormData.Tables[3].Rows[i]["FieldName"].ToString());
                    ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_RemoveFieldInFormBuilder_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                //Delete sections selected from remove section in formbuilder while in update mode
                if (dsSaveFormData.Tables.Count > 4)
                {
                    for (int i = 0; i < dsSaveFormData.Tables[4].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dsSaveFormData.Tables[4].Rows[i]["SectionId"].ToString());
                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iFeatureId.ToString());
                        ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_RemoveSectionAndFieldInFormBuilder_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return iFeatureId;
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
        public int UpdateFormDetailSeq(DataTable dtFieldDetails)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FormDetail = new ClsObject();
                FormDetail.Connection = this.Connection;
                FormDetail.Transaction = this.Transaction;
                int theRowAffected = 0;


                for (int i = 0; i < dtFieldDetails.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureName", SqlDbType.Int, dtFieldDetails.Rows[i]["FeatureName"].ToString());
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.VarChar, dtFieldDetails.Rows[i]["FeatureId"].ToString());
                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, dtFieldDetails.Rows[i]["Seq"].ToString());
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dtFieldDetails.Rows[i]["ModuleId"].ToString());
                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_UpdateFormDetailSeq_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;
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
