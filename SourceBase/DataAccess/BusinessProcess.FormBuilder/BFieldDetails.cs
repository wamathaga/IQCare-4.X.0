using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;
using Application.Common;


namespace BusinessProcess.FormBuilder
{
    public class BFieldDetails : ProcessBase, IFieldDetail
    {

        public DataSet GetDrugType()
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "Pr_BusinessRule_GetDrugType_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetConditionalformInfo(int FeatureId)
        {
            lock (this)
            {
                ClsObject Conditionalform = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                return (DataSet)Conditionalform.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetConditionalformInfo_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetBusinessRule()
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "Pr_BusinessRule_GetBusinessRule_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetBusinessDrugList(int FieldId)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FieldId", SqlDbType.Int, FieldId.ToString().Replace("8888", ""));
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "Pr_Business_GetDruglist_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }



        public DataSet GetCustomFields(string strFieldName, int iModuleId, int flag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, strFieldName);
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, iModuleId.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_GetCustomFields_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetCustomFields(string strFieldName, int iModuleId, int flag, int IsGridView)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, strFieldName);
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, iModuleId.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                ClsUtility.AddParameters("@isGridView", SqlDbType.Int, IsGridView.ToString());
                return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_GetCustomFields_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDuplicateCustomFields(int id, string fieldName, int ModuleId, int flag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ID", SqlDbType.Int, id.ToString());
                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, fieldName);
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_GetDuplicateCustomFields_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }

        }


        public DataSet CheckPredefineField(int fieldID)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ID", SqlDbType.Int, fieldID.ToString());

                return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_GetPredefineFields_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet CheckCustomFields(int fieldID)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ID", SqlDbType.Int, fieldID.ToString());

                return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_GetCustomFieldsDetails_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public int ResetCustomFieldRules(int fieldID, int flag, int predefine, string FieldName)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                int theRowAffected = 0;
                ClsUtility.AddParameters("@ID", SqlDbType.Int, fieldID.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                ClsUtility.AddParameters("@Predefined", SqlDbType.Int, predefine.ToString());
                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, FieldName);
                theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_SaveUpdateCustomFields_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                return theRowAffected;
            }
        }
        public int DeleteCustomField(int fieldID, int flag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                int theRowAffected = 0;
                ClsUtility.AddParameters("@ID", SqlDbType.Int, fieldID.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_DeleteCustomFields_Future", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Deleting Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                return theRowAffected;
            }
        }
        public int SaveUpdateCusomtField(int ID, string FieldName, int ControlID, int DeleteFlag, int UserID, int CareEnd, int flag, string SelectList,
            DataTable business, int Predefined, int SystemID, DataTable dtconditionalFields, DataTable dtICD10Fields, DataSet dsFormVersionFields,int FacilityId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject CustomField = new ClsObject();
                CustomField.Connection = this.Connection;
                CustomField.Transaction = this.Transaction;
                int theRowAffected = 0;
                DataRow theDR;



                /************   Delete Previous Business Rule **********/
                if (ID != 0 && flag != 4)
                {

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                    if (ControlID == 19)
                    {
                        theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_Delete_FormBuilderFieldDruglist_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else
                    {
                        theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_Delete_FieldBusinessRule_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Updating Custom Field. Try Again..";
                        AppException.Create("#C1", theMsg);
                    }

                }
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, FieldName);
                ClsUtility.AddParameters("@ControlID", SqlDbType.Int, ControlID.ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@CareEnd", SqlDbType.Int, CareEnd.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                ClsUtility.AddParameters("@SelectList", SqlDbType.VarChar, SelectList);
                ClsUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                ClsUtility.AddParameters("@SystemID", SqlDbType.Int, SystemID.ToString());
                ClsUtility.AddParameters("@FacilityID", SqlDbType.Int, FacilityId.ToString());

                theDR = (DataRow)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_SaveUpdateCustomFields_Futures", ClsDBUtility.ObjectEnum.DataRow);
                int FieldID = Convert.ToInt32(theDR[0].ToString());
                if (FieldID == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theBL);
                }

                /************************Add Business Rule*************************/
                if (ControlID == 19)
                {
                    for (int i = 0; i < business.Rows.Count; i++)
                    {

                        if (FieldName == business.Rows[i]["FieldName"].ToString())
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@FieldID", SqlDbType.Int, FieldID.ToString());
                            ClsUtility.AddParameters("@DrugId", SqlDbType.Int, business.Rows[i]["DrugId"].ToString());
                            ClsUtility.AddParameters("@DrugTypeId", SqlDbType.Int, business.Rows[i]["DrugTypeId"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                            ClsUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                            theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SavePharmacyDrugList_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                            if (theRowAffected == 0)
                            {
                                MsgBuilder theMsg = new MsgBuilder();
                                theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                                AppException.Create("#C1", theMsg);

                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < business.Rows.Count; i++)
                    {

                        if (FieldName == business.Rows[i]["FieldName"].ToString())
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@FieldID", SqlDbType.Int, FieldID.ToString());
                            ClsUtility.AddParameters("@BusRuleID", SqlDbType.Int, business.Rows[i]["BusRuleId"].ToString());
                            ClsUtility.AddParameters("@Value", SqlDbType.VarChar, business.Rows[i]["Value"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                            ClsUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                            //12may2011
                            ClsUtility.AddParameters("@Value1", SqlDbType.VarChar, business.Rows[i]["Value1"].ToString());
                            theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_SaveBusinessRules_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                            if (theRowAffected == 0)
                            {
                                MsgBuilder theMsg = new MsgBuilder();
                                theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                                AppException.Create("#C1", theMsg);

                            }
                        }
                    }
                }

                /**************************Add Conditional Fields*************************************/
                int Rec = 0;

                if (dtconditionalFields != null && dtconditionalFields.Rows.Count == 0)
                {
                    if (CareEnd == 0)
                    {
                        ClsUtility.Init_Hashtable();
                        string theTSQL = "";
                        if (Predefined == 1)
                        {
                            theTSQL = "delete from dbo.lnk_conditionalfields where ConFieldId =" + ID.ToString().Replace("9999", "");
                        }
                        else if (Predefined == 0)
                        {
                            theTSQL = "delete from dbo.lnk_conditionalfields where ConFieldId =" + ID.ToString().Replace("8888", "");
                        }
                        Int32 theRow = (Int32)CustomField.ReturnObject(ClsUtility.theParams, theTSQL, ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else if (CareEnd == 2)
                    {
                        ClsUtility.Init_Hashtable();
                        string theTSQL = "";
                        if (Predefined == 1)
                        {
                            theTSQL = "delete from dbo.lnk_PatientRegconditionalfields where ConFieldId =" + ID.ToString().Replace("9999", "");
                        }
                        else if (Predefined == 0)
                        {
                            theTSQL = "delete from dbo.lnk_PatientRegconditionalfields where ConFieldId =" + ID.ToString().Replace("8888", "");
                        }
                        Int32 theRow = (Int32)CustomField.ReturnObject(ClsUtility.theParams, theTSQL, ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    else
                    {
                        ClsUtility.Init_Hashtable();

                        string theTSQL = "";
                        if (Predefined == 1)
                        {
                            theTSQL = "delete from dbo.lnk_CareEndConditionalFields where ConFieldId =" + ID.ToString().Replace("9999", "");
                        }
                        else if (Predefined == 0)
                        {
                            theTSQL = "delete from dbo.lnk_CareEndConditionalFields where ConFieldId =" + ID.ToString().Replace("8888", "");
                        }
                        //string theTSQL = "delete from dbo.lnk_CareEndConditionalFields where ConFieldId =" + ID.ToString();
                        Int32 theRow = (Int32)CustomField.ReturnObject(ClsUtility.theParams, theTSQL, ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }
                foreach (DataRow theDRCon in dtconditionalFields.Rows)
                {
                    ClsUtility.Init_Hashtable();
                    Rec = Rec + 1;
                    if (theDRCon["ConPredefine"].ToString() == "1" && CareEnd == 0)
                        ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("9999", ""));
                    else if (theDRCon["ConPredefine"].ToString() == "1" && CareEnd == 1)
                        ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("9999", ""));
                    else if (theDRCon["ConPredefine"].ToString() == "1" && CareEnd == 2)
                        ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("9999", ""));

                    if (theDRCon["ConPredefine"].ToString() == "0" && CareEnd == 0)
                        ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("8888", ""));
                    else if (theDRCon["ConPredefine"].ToString() == "0" && CareEnd == 1)
                        ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("8888", ""));
                    else if (theDRCon["ConPredefine"].ToString() == "0" && CareEnd == 2)
                        ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRCon["ConfieldId"].ToString().Replace("8888", ""));
                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, theDRCon["SectionId"].ToString());
                    if (CareEnd == 1)
                    {
                        //ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString());
                        if (theDRCon["Predefined"].ToString() == "1")
                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString().Replace("9999", ""));
                        else
                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString().Replace("8888", ""));
                    }
                    else
                    {
                        if (theDRCon["Predefined"].ToString() == "1")
                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString().Replace("9999", ""));
                        else
                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRCon["FieldId"].ToString().Replace("8888", ""));

                    }

                    ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, theDRCon["FieldLabel"].ToString());
                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, theDRCon["Predefined"].ToString());
                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, Rec.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, theDRCon["SectionName"].ToString());
                    ClsUtility.AddParameters("@Conpredefine", SqlDbType.Int, theDRCon["Conpredefine"].ToString());
                    if (Rec == 1)
                        ClsUtility.AddParameters("@Delete", SqlDbType.Int, "1");
                    ClsUtility.AddParameters("@CareEnd", SqlDbType.Int, CareEnd.ToString());
                    theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SavelnkConditionalForm_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                int Deleted = 0;
                foreach (DataRow theDRCon in dtICD10Fields.Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, FieldID.ToString());
                    ClsUtility.AddParameters("@BlockId", SqlDbType.Int, theDRCon["BlockId"].ToString().Replace("'", ""));
                    ClsUtility.AddParameters("@SubBlockId", SqlDbType.Int, theDRCon["SubBlockId"].ToString().Replace("'", ""));
                    ClsUtility.AddParameters("@CodeId", SqlDbType.Int, theDRCon["CodeId"].ToString().Replace("'", ""));
                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, Predefined.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, theDRCon["Deleteflag"].ToString());
                    ClsUtility.AddParameters("@Chapterid", SqlDbType.Int, theDRCon["ChapterId"].ToString().Replace("'", ""));
                    theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveICD10CodeItems_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    Deleted = 1;
                }

                /**************************************************************************************/
                /**************************Add Form Version Conditional Fields *************************************/
                if (dsFormVersionFields != null && dsFormVersionFields.Tables.Count > 0 && CareEnd==0)
                {
                    ///Save Update Form Version Masters
                    if (dsFormVersionFields.Tables[0].Rows.Count > 0 && dsFormVersionFields.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow drfield in dsFormVersionFields.Tables[0].Rows)
                        {
                            decimal vername = Convert.ToDecimal(dsFormVersionFields.Tables[0].Rows[0]["VersionName"]) + Convert.ToDecimal(0.1);
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@VerId", SqlDbType.Int, "0");
                            ClsUtility.AddParameters("@VersionName", SqlDbType.Decimal, vername.ToString());
                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, drfield["FeatureId"].ToString());
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                            DataRow theDRVer = (DataRow)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveFormVersion_Futures", ClsDBUtility.ObjectEnum.DataRow);

                            foreach (DataRow theDRdetails in dsFormVersionFields.Tables[1].Rows)
                            {
                                
                                    
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@VerId", SqlDbType.Int, theDRVer["VersionId"].ToString());
                                ClsUtility.AddParameters("@Predefined", SqlDbType.Decimal, theDRdetails["Predefined"].ToString());
                                ClsUtility.AddParameters("@ConPredefined", SqlDbType.Decimal, theDRdetails["ConPredefined"].ToString());
                                if (theDRdetails["ConPredefined"].ToString() == "1")
                                    ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRdetails["ConfieldId"].ToString().Replace("9999", ""));
                                else if (theDRdetails["ConPredefined"].ToString() == "0")
                                    ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, theDRdetails["ConfieldId"].ToString().Replace("8888", ""));
                                if (theDRdetails["Predefined"].ToString() == "1")
                                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRdetails["FieldId"].ToString().Replace("9999", ""));
                                else if (theDRdetails["Predefined"].ToString() == "0")
                                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, theDRdetails["FieldId"].ToString().Replace("8888", ""));
                                ClsUtility.AddParameters("@FunctionId", SqlDbType.Int, theDRdetails["FunctionId"].ToString());
                                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, drfield["FeatureId"].ToString());
                                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                                theRowAffected = (int)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveFormConditionalVersionDetails_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                            }
                            
                        }
                    }
                }
                /**************************************************************************************/
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return FieldID;
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

        public DataSet GetConditionalFieldslist(Int32 Codeid, int FID, int flag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@CId", SqlDbType.Int, Codeid.ToString());
                ClsUtility.AddParameters("@FID", SqlDbType.Int, FID.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetConditionalFieldslist_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetConditionalFieldsDetails(Int32 ConfieldID, Int32 CareEndconFlag)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ConfieldID", SqlDbType.Int, ConfieldID.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, CareEndconFlag.ToString());
                return (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_GetConditionalFields_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        ////public int SaveConditionalFields(DataTable dtconditionalFields)
        ////{
        ////    ClsObject conditionalfields = new ClsObject();

        ////    int theRowAffected = 0;

        ////     for (int i = 0; i <= dtconditionalFields.Rows.Count - 1; i++)
        ////        {
        ////            ClsUtility.Init_Hashtable();

        ////             if(dtconditionalFields.Rows[i]["id"].ToString()=="0")
        ////             {
        ////                    ClsUtility.AddParameters("@Id", SqlDbType.Int, dtconditionalFields.Rows[i]["id"].ToString());
        ////                    ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, dtconditionalFields.Rows[i]["ConfieldId"].ToString());
        ////                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dtconditionalFields.Rows[i]["SectionId"].ToString());
        ////                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dtconditionalFields.Rows[i]["FieldId"].ToString());
        ////                    ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dtconditionalFields.Rows[i]["FieldLabel"].ToString());
        ////                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dtconditionalFields.Rows[i]["Predefined"].ToString());
        ////                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, (i + 1).ToString());
        ////                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, dtconditionalFields.Rows[i]["UserId"].ToString());

        ////                    theRowAffected = (int)conditionalfields.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SavelnkConditionalForm_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

        ////             }
        ////             else
        ////             {
        ////                    ClsUtility.AddParameters("@Id", SqlDbType.Int, dtconditionalFields.Rows[i]["id"].ToString());
        ////                    ClsUtility.AddParameters("@ConfieldId", SqlDbType.Int, dtconditionalFields.Rows[i]["ConfieldId"].ToString());
        ////                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dtconditionalFields.Rows[i]["SectionId"].ToString());
        ////                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dtconditionalFields.Rows[i]["FieldId"].ToString());
        ////                    ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dtconditionalFields.Rows[i]["FieldLabel"].ToString());
        ////                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dtconditionalFields.Rows[i]["Predefined"].ToString());
        ////                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, (i + 1).ToString());
        ////                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, dtconditionalFields.Rows[i]["UserId"].ToString());

        ////                    theRowAffected = (int)conditionalfields.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SavelnkConditionalForm_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

        ////             }

        ////     }

        ////    //if (theRowAffected == 0)
        ////    //{
        ////    //    MsgBuilder theMsg = new MsgBuilder();
        ////    //    theMsg.DataElements["MessageText"] = "Error in Saving ConditionalFields. Try Again..";
        ////    //    AppException.Create("#C1", theMsg);

        ////    //}

        ////    return theRowAffected;
        ////}

        public int SaveModDeCode(DataTable dtModDeCode)
        {
            lock (this)
            {
                ClsObject conditionalfields = new ClsObject();

                int theRowAffected = 0;

                for (int i = 0; i <= dtModDeCode.Rows.Count - 1; i++)
                {
                    ClsUtility.Init_Hashtable();
                    if (dtModDeCode.Rows[i]["FieldID"].ToString() != "0")
                    {
                        ClsUtility.AddParameters("@FieldID", SqlDbType.Int, dtModDeCode.Rows[i]["FieldID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, dtModDeCode.Rows[i]["CodeName"].ToString());
                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dtModDeCode.Rows[i]["Predefined"].ToString());
                        ClsUtility.AddParameters("@Index", SqlDbType.Int, (i + 1).ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, dtModDeCode.Rows[i]["UserID"].ToString());
                        ClsUtility.AddParameters("@SystemID", SqlDbType.Int, dtModDeCode.Rows[i]["SystemID"].ToString());

                        theRowAffected = (int)conditionalfields.ReturnObject(ClsUtility.theParams, "Pr_PMTCT_SaveModDeCode_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }

                }

                //if (theRowAffected == 0)
                //{
                //    MsgBuilder theMsg = new MsgBuilder();
                //    theMsg.DataElements["MessageText"] = "Error in Saving ConditionalFields. Try Again..";
                //    AppException.Create("#C1", theMsg);

                //}

                return theRowAffected;
            }
        }
        #region "Treeview of ICD10 List"
        public DataSet GetICDList()
        {
            lock (this)
            {
                ClsObject ICD10Manager = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)ICD10Manager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetICD10List_Features", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetICD10Values(int FieldId)
        {
            lock (this)
            {
                ClsObject ICD10Manager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FieldId", SqlDbType.Int, FieldId.ToString());
                return (DataSet)ICD10Manager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetICD10Values_Features", ClsDBUtility.ObjectEnum.DataSet);
            }
        }


        #endregion

    }
}

