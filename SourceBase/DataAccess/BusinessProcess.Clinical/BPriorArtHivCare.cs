using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Clinical
{
    public class BPriorArtHivCare : ProcessBase, IPriorArtHivCare
    {
        #region "Constructor"
        public BPriorArtHivCare()
        {
        }
        #endregion

        public DataTable GetPatient_No_Of_IE(int patientid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientid.ToString());
                ClsObject IEManager = new ClsObject();
                return (DataTable)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatient_No_of_IE_Constella", ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        //public DataSet GetPatient_No_Of_VisitDate(int patientid, DateTime visitdate, int visittype)
        //{
        //    ClsUtility.Init_Hashtable();
        //    ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientid.ToString());
        //    ClsUtility.AddParameters("@HIVvisitdate", SqlDbType.Int, visitdate.ToString());
        //    ClsUtility.AddParameters("@visittype", SqlDbType.Int, visittype.ToString());
        //    ClsObject IEManager = new ClsObject();
        //    return (DataSet)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatient_No_of_VisitDateConstella", ClsDBUtility.ObjectEnum.DataSet);
        //}

        public DataSet GetPatientPriorArtHIVCare(int patientid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetIE_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetAllDropDowns()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject IEManager = new ClsObject();
                return (DataSet)IEManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetInitialEvaluationDropDowns_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetCurrentDate()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject IEDate = new ClsObject();
                return (DataSet)IEDate.ReturnObject(ClsUtility.theParams, "clinic_getdate", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public int Update_DataQuality(int patientid, int visitpk, int dataquality, int locationid)
        {

            ClsObject IEManager = new ClsObject();
            int retval = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                IEManager.Connection = this.Connection;
                IEManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, visitpk.ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.Int, dataquality.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, locationid.ToString());
                retval = (int)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Update_DataQuality_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                IEManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return retval;
        }

        public DataSet SavePriorArtHivCare(Hashtable ht, int intflag, int DataQualityFlag, DataTable thedtDynamicDrugMedical, DataTable theCustomFieldData)
        {
            ClsObject IEManager = new ClsObject();
            DataSet theDS;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                IEManager.Connection = this.Connection;
                IEManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                ClsUtility.AddParameters("@Visit_typeID", SqlDbType.Int, ht["VisitTypeID"].ToString());
                ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, ht["VisitPKID"].ToString());


                ClsUtility.AddParameters("@HIVvisitdate", SqlDbType.VarChar, Convert.ToDateTime(ht["visitdate"]).ToString());

                ClsUtility.AddParameters("@PriorArt", SqlDbType.VarChar, ht["PriorArt"].ToString());
                ClsUtility.AddParameters("@PEP", SqlDbType.VarChar, ht["PEP"].ToString());
                ClsUtility.AddParameters("@PEPStartDate", SqlDbType.VarChar, ht["PEPStartDate"].ToString());
                ClsUtility.AddParameters("@PEPWhere", SqlDbType.VarChar, ht["PEPWhere"].ToString());
                ClsUtility.AddParameters("@PEPARVs", SqlDbType.VarChar, ht["PEPARVs"].ToString());
                ClsUtility.AddParameters("@PMTCTOnly", SqlDbType.VarChar, ht["PMTCTOnly"].ToString());
                ClsUtility.AddParameters("@PMTCTStartDate", SqlDbType.VarChar, ht["PMTCTStartDate"].ToString());
                ClsUtility.AddParameters("@PMTCTWhere", SqlDbType.VarChar, ht["PMTCTWhere"].ToString());
                ClsUtility.AddParameters("@PMTCTARVs", SqlDbType.VarChar, ht["PMTCTARVs"].ToString());
                ClsUtility.AddParameters("@EarlierArvNotTransfer", SqlDbType.VarChar, ht["EarlierArvNotTransfer"].ToString());
                ClsUtility.AddParameters("@EarlierArvStartDate", SqlDbType.VarChar, ht["EarlierArvStartDate"].ToString());
                ClsUtility.AddParameters("@EarlierArvWhere", SqlDbType.VarChar, ht["EarlierArvWhere"].ToString());
                ClsUtility.AddParameters("@EarlierArvNotTransferArv", SqlDbType.VarChar, ht["EarlierArvNotTransferArv"].ToString());
                ClsUtility.AddParameters("@HIVConfirmHIVPosDate", SqlDbType.VarChar, ht["HIVConfirmHIVPosDate"].ToString());
                ClsUtility.AddParameters("@HivTestType", SqlDbType.VarChar, ht["HivTestType"].ToString());
                ClsUtility.AddParameters("@HIVCareWhere", SqlDbType.VarChar, ht["HIVCareWhere"].ToString());
                ClsUtility.AddParameters("@HIVEligibleDate", SqlDbType.VarChar, ht["HIVEligibleDate"].ToString());
                ClsUtility.AddParameters("@HIVClincialWHOStage", SqlDbType.VarChar, ht["HIVClincialWHOStage"].ToString());
                ClsUtility.AddParameters("@HIVPreTranfrinfrom", SqlDbType.VarChar, ht["HIVPreTranfrinfrom"].ToString());

                //if (nullableDate.HasValue)
                //    datePrm.Value = nullableDate.Value;
                //else
                //    datePrm.Value = DBNull.Value; 


                string hivPerCD = "-1";
                if (ht["HIVPrevARVsCD4"].ToString() == "")
                {
                    hivPerCD = "-1";
                }
                else
                {
                    hivPerCD = ht["HIVPrevARVsCD4"].ToString();
                }
                string hivPerCDPer = "-1";
                if (ht["HIVPrevARVsCD4Percent"].ToString() == "")
                {
                    hivPerCDPer = "-1";
                }
                else
                {
                    hivPerCDPer = ht["HIVPrevARVsCD4Percent"].ToString();
                }

                ClsUtility.AddParameters("@HIVPrevARVsCD4", SqlDbType.VarChar, hivPerCD.ToString());
                ClsUtility.AddParameters("@HIVPrevARVsCD4Percent", SqlDbType.VarChar, hivPerCDPer.ToString());

                ClsUtility.AddParameters("@HIVReadyDate", SqlDbType.VarChar, ht["HIVReadyDate"].ToString());
                ClsUtility.AddParameters("@HIVPresumptiveDiagnosis", SqlDbType.VarChar, ht["HIVPresumptiveDiagnosis"].ToString());
                ClsUtility.AddParameters("@HIVPcrInfant", SqlDbType.VarChar, ht["HIVPcrInfant"].ToString());

                ClsUtility.AddParameters("@DataQuality", SqlDbType.VarChar, DataQualityFlag.ToString());
                ClsUtility.AddParameters("@userID", SqlDbType.VarChar, ht["UserID"].ToString());
                //     ClsUtility.AddParameters("@createdate", SqlDbType.VarChar, ht["CreateDate"].ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, ht["Flag"].ToString());
                theDS = (DataSet)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdatePriorArtHIVClinic_Constella", ClsDBUtility.ObjectEnum.DataSet);

                if (thedtDynamicDrugMedical != null)
                {
                    for (int i = 0; i < thedtDynamicDrugMedical.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());

                        ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());

                        ClsUtility.AddParameters("@AutoID", SqlDbType.Int, thedtDynamicDrugMedical.Rows[i]["AutoID"].ToString());
                        ClsUtility.AddParameters("@DrugAllergies", SqlDbType.VarChar, thedtDynamicDrugMedical.Rows[i]["DrugAllergies"].ToString());
                        ClsUtility.AddParameters("@TypeOfReaction", SqlDbType.VarChar, thedtDynamicDrugMedical.Rows[i]["TypeOfReaction"].ToString());
                        ClsUtility.AddParameters("@DateOfAlergy", SqlDbType.VarChar, thedtDynamicDrugMedical.Rows[i]["DateOfAllergy"].ToString());
                        ClsUtility.AddParameters("@RelevantMedicalCondition", SqlDbType.VarChar, thedtDynamicDrugMedical.Rows[i]["RelevantMedicalCondition"].ToString());


                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());

                        //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                        int retvalcomplaint = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdatePriorArt_DrugAllegies_Recantmedical_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", ht["patientid"].ToString());
                    theQuery = theQuery.Replace("#88#", ht["locationid"].ToString());
                    theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["visitdate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                ////////////////////////////////
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                IEManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return theDS;
        }

        #region "20-06-2007 -1 Jayant"
        public DataSet SaveInitialEvaluation(Hashtable ht, int none, int notDocumented, int AssoCondnone, int AssoCondnotDocumented, DataSet theDS_IE, ArrayList AssessmentAL, int VisitIE, string AssessmentDescription1, string AssessmentDescription2, int intflag, int DataQualityFlag, DataTable theCustomFieldData, string ClinicalNotes)
        {
            ClsObject IEManager = new ClsObject();
            DataSet theDS;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                IEManager.Connection = this.Connection;
                IEManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                ClsUtility.AddParameters("@Visit_typeID", SqlDbType.Int, ht["VisitTypeID"].ToString());
                ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, ht["VisitPKID"].ToString());
                ClsUtility.AddParameters("@HIVvisitdate", SqlDbType.VarChar, Convert.ToDateTime(ht["visitdate"]).ToString());
                ClsUtility.AddParameters("@HIVDiagnosisdate", SqlDbType.VarChar, ht["HIVDiagnosisdate"].ToString());

                ClsUtility.AddParameters("@diagnosisverified", SqlDbType.Int, ht["diagnosisverified"].ToString());
                ClsUtility.AddParameters("@disclosed", SqlDbType.Int, ht["disclosed"].ToString());
                ClsUtility.AddParameters("@lmp", SqlDbType.VarChar, ht["lmp"].ToString());
                ClsUtility.AddParameters("@Pregnant", SqlDbType.Int, ht["Pregnant"].ToString());
                ClsUtility.AddParameters("@Delivered", SqlDbType.VarChar, ht["Delivered"].ToString());
                ClsUtility.AddParameters("@DelDate", SqlDbType.VarChar, ht["DelDate"].ToString());
                ClsUtility.AddParameters("@EDDDate", SqlDbType.VarChar, ht["EDDDate"].ToString());
                ClsUtility.AddParameters("@flagsulfa", SqlDbType.VarChar, ht["flagsulfa"].ToString());
                ClsUtility.AddParameters("@sulfaallergyID", SqlDbType.VarChar, ht["allergy_Sulfa_ID"].ToString());
                ClsUtility.AddParameters("@otherallergyID", SqlDbType.VarChar, ht["allergy_Other_ID"].ToString());
                ClsUtility.AddParameters("@allergynameother", SqlDbType.VarChar, ht["allergynameother"].ToString());
                ClsUtility.AddParameters("@longTermMedsSulfa", SqlDbType.VarChar, ht["longTermMedsSulfa"].ToString());
                ClsUtility.AddParameters("@longTermMedsSulfaDesc", SqlDbType.VarChar, ht["longTermMedsSulfaDesc"].ToString());
                ClsUtility.AddParameters("@longTermTBMed", SqlDbType.VarChar, ht["longTermTBMed"].ToString());
                ClsUtility.AddParameters("@longTermTBMedDesc", SqlDbType.VarChar, ht["longTermTBMedDesc"].ToString());
                ClsUtility.AddParameters("@longTermMedsOther1", SqlDbType.VarChar, ht["longTermMedsOther1"].ToString());
                ClsUtility.AddParameters("@longTermMedsOther1Desc", SqlDbType.VarChar, ht["longTermMedsOther1Desc"].ToString());
                ClsUtility.AddParameters("@longTermMedsOther2", SqlDbType.VarChar, ht["longTermMedsOther2"].ToString());
                ClsUtility.AddParameters("@longTermMedsOther2Desc", SqlDbType.VarChar, ht["longTermMedsOther2Desc"].ToString());

                ClsUtility.AddParameters("@PrevLowestCD4None", SqlDbType.VarChar, ht["PrevLowestCD4None"].ToString());
                ClsUtility.AddParameters("@PrevLowestCD4NotDocumented", SqlDbType.VarChar, ht["PrevLowestCD4NotDocumented"].ToString());
                ClsUtility.AddParameters("@PrevLowestCD4", SqlDbType.VarChar, ht["PrevLowestCD4"].ToString());
                ClsUtility.AddParameters("@PrevLowestCD4Percent", SqlDbType.VarChar, ht["PrevLowestCD4Percent"].ToString());

                ClsUtility.AddParameters("@PrevARVsCD4None", SqlDbType.VarChar, ht["PrevARVsCD4None"].ToString());
                ClsUtility.AddParameters("@PrevARVsCD4NotDocumented", SqlDbType.VarChar, ht["PrevARVsCD4NotDocumented"].ToString());
                ClsUtility.AddParameters("@PrevARVsCD4", SqlDbType.VarChar, ht["PrevARVsCD4"].ToString());
                ClsUtility.AddParameters("@VisitID_IE", SqlDbType.VarChar, VisitIE.ToString());
                ClsUtility.AddParameters("@PrevARVsCD4Percent", SqlDbType.VarChar, ht["PrevARVsCD4Percent"].ToString());

                ClsUtility.AddParameters("@PrevMostRecentCD4None", SqlDbType.VarChar, ht["PrevMostRecentCD4None"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentCD4NotDocumented", SqlDbType.VarChar, ht["PrevMostRecentCD4NotDocumented"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentCD4", SqlDbType.VarChar, ht["PrevMostRecentCD4"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentCD4Percent", SqlDbType.VarChar, ht["PrevMostRecentCD4Percent"].ToString());

                ClsUtility.AddParameters("@PrevMostRecentViralLoadNone", SqlDbType.VarChar, ht["PrevMostRecentViralLoadNone"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentViralLoadNotDocumented", SqlDbType.VarChar, ht["PrevMostRecentViralLoadNotDocumented"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentViralLoad", SqlDbType.VarChar, ht["PrevMostRecentViralLoad"].ToString());

                ClsUtility.AddParameters("@PrevARVExposureNone", SqlDbType.VarChar, ht["PrevARVExposureNone"].ToString());
                ClsUtility.AddParameters("@PrevARVExposureNotDocumented", SqlDbType.VarChar, ht["PrevARVExposureNotDocumented"].ToString());
                ClsUtility.AddParameters("@PrevARVExposure", SqlDbType.VarChar, ht["PrevARVExposure"].ToString());
                ClsUtility.AddParameters("@CurrentART", SqlDbType.VarChar, ht["CurrentART"].ToString());
                ClsUtility.AddParameters("@PrevSingleDoseNVP", SqlDbType.VarChar, ht["PrevSingleDoseNVP"].ToString());

                ClsUtility.AddParameters("@PrevARVRegimen", SqlDbType.VarChar, ht["PrevARVRegimen"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen1Name", SqlDbType.VarChar, ht["PrevARVRegimen1Name"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen1Months", SqlDbType.VarChar, ht["PrevARVRegimen1Months"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen2Name", SqlDbType.VarChar, ht["PrevARVRegimen2Name"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen2Months", SqlDbType.VarChar, ht["PrevARVRegimen2Months"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen3Name", SqlDbType.VarChar, ht["PrevARVRegimen3Name"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen3Months", SqlDbType.VarChar, ht["PrevARVRegimen3Months"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen4Name", SqlDbType.VarChar, ht["PrevARVRegimen4Name"].ToString());
                ClsUtility.AddParameters("@PrevARVRegimen4Months", SqlDbType.VarChar, ht["PrevARVRegimen4Months"].ToString());

                ClsUtility.AddParameters("@Temp", SqlDbType.VarChar, ht["Temp"].ToString());
                ClsUtility.AddParameters("@RR", SqlDbType.VarChar, ht["RR"].ToString());
                ClsUtility.AddParameters("@HR", SqlDbType.VarChar, ht["HR"].ToString());
                ClsUtility.AddParameters("@BPDiastolic", SqlDbType.VarChar, ht["BPDiastolic"].ToString());
                ClsUtility.AddParameters("@BPSystolic", SqlDbType.VarChar, ht["BPSystolic"].ToString());
                ClsUtility.AddParameters("@Height", SqlDbType.VarChar, ht["Height"].ToString());
                ClsUtility.AddParameters("@Weight", SqlDbType.VarChar, ht["Weight"].ToString());
                ClsUtility.AddParameters("@Pain", SqlDbType.VarChar, ht["Pain"].ToString());
                ClsUtility.AddParameters("@WABStage", SqlDbType.VarChar, ht["WABStage"].ToString());
                ClsUtility.AddParameters("@WHOStage", SqlDbType.VarChar, ht["WHOStage"].ToString());
                ClsUtility.AddParameters("@ARVtherapyPlan", SqlDbType.VarChar, ht["ARVtherapyPlan"].ToString());
                ClsUtility.AddParameters("@ARVTherapyReasonCode", SqlDbType.VarChar, ht["ARVTherapyReasonCode"].ToString());
                ClsUtility.AddParameters("@TherapyOther", SqlDbType.VarChar, ht["ARVTherapyReasonOther"].ToString());
                ClsUtility.AddParameters("@signatureid", SqlDbType.VarChar, ht["Signatureid"].ToString());
                ClsUtility.AddParameters("@userID", SqlDbType.VarChar, ht["UserID"].ToString());
                ClsUtility.AddParameters("@DataQuality", SqlDbType.VarChar, DataQualityFlag.ToString());

                ClsUtility.AddParameters("@PrevSingleDoseNVPDate1", SqlDbType.VarChar, ht["txtprevSingleDoseNVPDate1"].ToString());
                ClsUtility.AddParameters("@PrevSingleDoseNVPDate2", SqlDbType.VarChar, ht["txtprevSingleDoseNVPDate2"].ToString());
                ClsUtility.AddParameters("@currentARTStartDate", SqlDbType.VarChar, ht["currentARTStartDate"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentViralLoadDate", SqlDbType.VarChar, ht["PrevMostRecentViralLoadDate"].ToString());
                ClsUtility.AddParameters("@PrevARVsCD4Date", SqlDbType.VarChar, ht["PrevARVsCD4Date"].ToString());
                ClsUtility.AddParameters("@PrevLowestCD4Date", SqlDbType.VarChar, ht["PrevLowestCD4Date"].ToString());
                ClsUtility.AddParameters("@longTermTBStartDate", SqlDbType.VarChar, ht["longTermTBStartDate"].ToString());
                ClsUtility.AddParameters("@PrevMostRecentCD4Date", SqlDbType.VarChar, ht["PrevMostRecentCD4Date"].ToString());
                ClsUtility.AddParameters("@AppExist", SqlDbType.VarChar, ht["AppExist"].ToString());
                ClsUtility.AddParameters("@VisitIDApp", SqlDbType.VarChar, Convert.ToString(ht["VisitIDApp"]));
                ClsUtility.AddParameters("@appdate", SqlDbType.VarChar, Convert.ToString(ht["appdate"]));
                ClsUtility.AddParameters("@appreason", SqlDbType.VarChar, Convert.ToString(ht["appreason"]));
                ClsUtility.AddParameters("@ClinicalNotes", SqlDbType.VarChar, ClinicalNotes);
                ClsUtility.AddParameters("@createdate", SqlDbType.VarChar, ht["CreateDate"].ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, ht["Flag"].ToString());
                theDS = (DataSet)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateIE_Constella", ClsDBUtility.ObjectEnum.DataSet);

                if (Convert.ToInt32(ht["AppExist"].ToString()) == 1)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@Visit_pkAppID", SqlDbType.Int, ht["VisitIDApp"].ToString());
                    ClsUtility.AddParameters("@signatureid", SqlDbType.BigInt, ht["Signatureid"].ToString());
                    int RowsAffected = (int)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateIEAppointmentSignature_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                /**Disclose To**/
                for (int i = 0; i < theDS_IE.Tables[0].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@disclosureid", SqlDbType.Int, theDS_IE.Tables[0].Rows[i]["DisclosureID"].ToString());
                    ClsUtility.AddParameters("@HIVDisclosureOther", SqlDbType.VarChar, theDS_IE.Tables[0].Rows[i]["DisclosureOther"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    int retvaldisclose = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateDiscloseIE_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }
                /** Presenting Complaints **/
                for (int i = 0; i < theDS_IE.Tables[1].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_IE.Tables[1].Rows[i]["PresentComplaintsID"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalcomplaint = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                /****TBScreening*****/
                for (int i = 0; i < theDS_IE.Tables[5].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@Symptomid", SqlDbType.Int, theDS_IE.Tables[5].Rows[i]["TBScreeningID"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalcomplaint = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                /* MediHistoryManager*/
                //None
                if (none == 95)
                {
                    Boolean DiseasePresent = false;
                    String DiseaseYear = "1900";
                    String SpDisease = "None";
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@MedHistDiseaseID", SqlDbType.Int, none.ToString());
                    ClsUtility.AddParameters("@MediHisDiseasePresent", SqlDbType.Bit, DiseasePresent.ToString());
                    ClsUtility.AddParameters("@MedHistDiseaseYear", SqlDbType.VarChar, DiseaseYear);
                    ClsUtility.AddParameters("@MedHistSpecifyDisease", SqlDbType.VarChar, SpDisease);
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalMedHistory = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateMedicalHistoryIE_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                //Not Documented
                else if (notDocumented == 94)
                {
                    Boolean DiseasePresent = false;
                    String DiseaseYear = "1900";
                    String SpDisease = "Notdocumented";
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@MedHistDiseaseID", SqlDbType.Int, notDocumented.ToString());
                    ClsUtility.AddParameters("@MediHisDiseasePresent", SqlDbType.Bit, DiseasePresent.ToString());
                    ClsUtility.AddParameters("@MedHistDiseaseYear", SqlDbType.VarChar, DiseaseYear);
                    ClsUtility.AddParameters("@MedHistSpecifyDisease", SqlDbType.VarChar, SpDisease);
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalMedHistory = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateMedicalHistoryIE_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }
                else if (none == 0 && notDocumented == 0)
                {
                    for (int i = 0; i < theDS_IE.Tables[2].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                        ClsUtility.AddParameters("@MedHistDiseaseID", SqlDbType.Int, theDS_IE.Tables[2].Rows[i]["MedHistoryID"].ToString());
                        ClsUtility.AddParameters("@MediHisDiseasePresent", SqlDbType.Bit, theDS_IE.Tables[2].Rows[i]["MediHisDiseasePresent"].ToString());
                        ClsUtility.AddParameters("@MedHistDiseaseYear", SqlDbType.VarChar, theDS_IE.Tables[2].Rows[i]["YearDiseasePresent"].ToString());
                        ClsUtility.AddParameters("@MedHistSpecifyDisease", SqlDbType.VarChar, theDS_IE.Tables[2].Rows[i]["SpecifyDiseasePresent"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                        ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                        //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                        int retvalMedHistory = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateMedicalHistoryIE_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                /* Associate Condition Left */
                //Associated Assocond - None
                if (AssoCondnone == 97)
                {
                    Boolean DiseasePresent = false;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, AssoCondnone.ToString());
                    ClsUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, DiseasePresent.ToString());
                    ClsUtility.AddParameters("@HIVAssocDiseaseYear", SqlDbType.VarChar, "");
                    ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, "");
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalleft = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateHIVAssoConditionIE_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }
                //Associated Assocond - Not Documented
                if (AssoCondnotDocumented == 96)
                {

                    Boolean DiseasePresent = false;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, AssoCondnotDocumented.ToString());
                    ClsUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, DiseasePresent.ToString());
                    ClsUtility.AddParameters("@HIVAssocDiseaseYear", SqlDbType.VarChar, "");
                    ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, "");
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalleft = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateHIVAssoConditionIE_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }
                //HIV Associated Conditions
                if (AssoCondnone == 0 && AssoCondnotDocumented == 0)
                {
                    //Left Side Items.
                    for (int i = 0; i < theDS_IE.Tables[3].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS_IE.Tables[3].Rows[i]["chkHIVAssoCondID1"].ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, theDS_IE.Tables[3].Rows[i]["HIVAssoDiseasePresent1"].ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseaseYear", SqlDbType.VarChar, theDS_IE.Tables[3].Rows[i]["HIVAssocCondYear1"].ToString());
                        ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, "");
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                        ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                        //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                        int retvalleft = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateHIVAssoConditionIE_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    //Right Side Items
                    for (int i = 0; i < theDS_IE.Tables[4].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                        ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS_IE.Tables[4].Rows[i]["chkHIVAssoCondid2"].ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, theDS_IE.Tables[4].Rows[i]["HIVAssoDiseasePresent2"].ToString());
                        ClsUtility.AddParameters("@HIVAssocDiseaseYear", SqlDbType.VarChar, theDS_IE.Tables[4].Rows[i]["HIVAssocCondYear2"].ToString());
                        ClsUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, theDS_IE.Tables[4].Rows[i]["HIVAssoDiseaseDesc"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                        ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                        //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                        int retvalright = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateHIVAssoConditionIE_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                /* Saving Assessment Details */
                for (int i = 0; i < AssessmentAL.Count; i++)
                {

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, ht["patientid"].ToString());
                    ClsUtility.AddParameters("@locationid", SqlDbType.Int, ht["locationid"].ToString());
                    ClsUtility.AddParameters("@AssessmentID", SqlDbType.Int, AssessmentAL[i].ToString());
                    ClsUtility.AddParameters("@Description1", SqlDbType.VarChar, AssessmentDescription1.ToString());
                    ClsUtility.AddParameters("@Description2", SqlDbType.VarChar, AssessmentDescription2.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                    ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //ClsUtility.AddParameters("@createdate", SqlDbType.DateTime, ht["CreateDate"].ToString());
                    int retvalAssessnent = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateAssessment_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////

                //ClsObject theCustomManager = new ClsObject();
                //Generating VisitID from IE Form
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", ht["patientid"].ToString());
                    theQuery = theQuery.Replace("#88#", ht["locationid"].ToString());
                    theQuery = theQuery.Replace("#77#", theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    theQuery = theQuery.Replace("#66#", "'" + ht["visitdate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                ////////////////////////////////
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                IEManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return theDS;
        }
        #endregion
        //public DataSet GetInitialEvaluationVisitDate(int patientid)
        //{
        //    ClsUtility.Init_Hashtable();
        //    ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
        //    ClsObject UserManager = new ClsObject();
        //    return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetIEVisitDate_Constella", ClsDBUtility.ObjectEnum.DataSet);
        //}

        public DataSet GetPriorArtHivCareUpdate(int visitpk, int patientid, int locationID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@Visit_pkID", SqlDbType.Int, visitpk.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPriorARTHivClincialUpdate_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetClinicalDate(int patientid, int visittype)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@visittype", SqlDbType.Int, visittype.ToString());
                ClsObject PatientEnrolManager = new ClsObject();
                return (DataSet)PatientEnrolManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_CheckClinicalDate_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetARTStatus(int patientID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                ClsObject PatientARTStatus = new ClsObject();
                return (DataSet)PatientARTStatus.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetARTStatus_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetPregnantStatus(int patientID, string VisitDate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@VisitDate", SqlDbType.VarChar, VisitDate.ToString());
                ClsObject PatientARTStatus = new ClsObject();
                return (DataSet)PatientARTStatus.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPregnantStatus_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
        public DataSet GetAppointment(int patientID, int locationID, DateTime AppDate, int AppReason)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, locationID.ToString());
                ClsUtility.AddParameters("@AppDate", SqlDbType.VarChar, AppDate.ToString());
                ClsUtility.AddParameters("@AppReason", SqlDbType.Int, AppReason.ToString());
                ClsObject PatientAppStatus = new ClsObject();
                return (DataSet)PatientAppStatus.ReturnObjectNewImpl(ClsUtility.theParams, "pr_clinical_Appointment_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }


        public int SaveExposedInfant(int Id, int Ptn_Pk, int ExposedInfantId, string FirstName, string LastName, DateTime DOB, string FeedingPractice3mos,
           string CTX2mos, string HIVTestType, string HIVResult, string FinalStatus, DateTime? DeathDate)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                int theRowAffected = 0;
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk.ToString());
                ClsUtility.AddParameters("@ExposedInfantId", SqlDbType.Int, ExposedInfantId.ToString());
                ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, FirstName.ToString());
                ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, LastName.ToString());
                //ClsUtility.AddParameters("@DOB", SqlDbType.DateTime, DOB.ToString());
                //ClsUtility.AddParameters("@FeedingPractice3mos", SqlDbType.VarChar, FeedingPractice3mos.ToString());
                //ClsUtility.AddParameters("@CTX2mos", SqlDbType.VarChar, CTX2mos.ToString());
                //ClsUtility.AddParameters("@HIVResult", SqlDbType.VarChar, HIVResult.ToString());
                //ClsUtility.AddParameters("@HIVTestType", SqlDbType.VarChar, HIVTestType.ToString());
                //ClsUtility.AddParameters("@FinalStatus", SqlDbType.VarChar, FinalStatus.ToString());
                //ClsUtility.AddParameters("@DeathDate", SqlDbType.DateTime, DeathDate == null ? null : DeathDate.ToString());
                theRowAffected = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveExposedInfant", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                //  int retvalcomplaint = (Int32)IEManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdatePriorArt_DrugAllegies_Recantmedical_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving Custom Field. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return Convert.ToInt32(theRowAffected);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
        }

        #region "Kenya Blue Card'

        public DataTable Save_Update_ARVHistory(Hashtable theHT, DataTable theDT, DataTable theCustomFieldData)
        {
            ClsObject ARTHistoryMgr = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ARTHistoryMgr.Connection = this.Connection;
                ARTHistoryMgr.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, theHT["PatientId"].ToString());
                ClsUtility.AddParameters("@VisitId", SqlDbType.Int, theHT["VisitId"].ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                ClsUtility.AddParameters("@TransferInDate", SqlDbType.VarChar, theHT["TransferInDate"].ToString());
                ClsUtility.AddParameters("@TransferInFrom", SqlDbType.VarChar, theHT["ddfacility"].ToString());
                ClsUtility.AddParameters("@dddistrict", SqlDbType.Int, theHT["dddistrict"].ToString());
                ClsUtility.AddParameters("@DateARTStarted", SqlDbType.VarChar, theHT["DateARTStarted"].ToString());
                ClsUtility.AddParameters("@PriorART", SqlDbType.Int, theHT["priorART"].ToString());
                ClsUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.VarChar, theHT["ConfirmHIVPosDate"].ToString());
                ClsUtility.AddParameters("@Where", SqlDbType.VarChar, theHT["Where"].ToString());
                ClsUtility.AddParameters("@EnrolledinHIVCare", SqlDbType.VarChar, theHT["EnrolledinHIVCare"].ToString());
                ClsUtility.AddParameters("@WHOStage", SqlDbType.Int, theHT["WHOStage"].ToString());
                ClsUtility.AddParameters("@DrugAllergy", SqlDbType.Int, theHT["AreaAllergy"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserID"].ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, theHT["qltyFlag"].ToString());



                DataTable ReturnDT = new DataTable();
                if (Convert.ToInt32(theHT["VisitId"]) > 0)
                {
                    ReturnDT = (DataTable)ARTHistoryMgr.ReturnObjectNewImpl(ClsUtility.theParams, "pr_Clinical_UpdateARTHistory_Futures", ClsDBUtility.ObjectEnum.DataTable);
                }
                else
                {
                    ReturnDT = (DataTable)ARTHistoryMgr.ReturnObjectNewImpl(ClsUtility.theParams, "pr_Clinical_SaveARTHistory_Futures", ClsDBUtility.ObjectEnum.DataTable);
                }

                if (theDT.Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@VisitId", SqlDbType.Int, ReturnDT.Rows[0]["VisitId"].ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        ClsUtility.AddParameters("@PurposeId", SqlDbType.Int, theDR["PurposeId"].ToString());
                        ClsUtility.AddParameters("@Regimen", SqlDbType.Int, theDR["Regimen"].ToString());
                        ClsUtility.AddParameters("@RegLastUsed", SqlDbType.VarChar, theDR["RegLastUsed"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserID"].ToString());
                        int retval = (Int32)ARTHistoryMgr.ReturnObjectNewImpl(ClsUtility.theParams, "pr_Clinical_SavePatientBlueCardPriorART_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        if (retval == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving. Try Again..";
                            AppException.Create("#C1", theMsg);
                        }
                    }
                }



                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", theHT["PatientId"].ToString());
                    theQuery = theQuery.Replace("#88#", theHT["LocationId"].ToString());
                    theQuery = theQuery.Replace("#77#", ReturnDT.Rows[0]["VisitId"].ToString());
                    //theQuery = theQuery.Replace("#66#", "02/06/2013");
                    theQuery = theQuery.Replace("#66#", "'" + theHT["visitdate"].ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)ARTHistoryMgr.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }




                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return ReturnDT;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;

            }

            finally
            {

                ARTHistoryMgr = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);


            }


        }

        
        
        public DataSet GetARTHistoryData(int PatientId, int VisitId, int LocationId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsObject ARTHistoryMgr = new ClsObject();
                return (DataSet)ARTHistoryMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetARTHistoryData_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
        

        #endregion

        #region "Green Card"
        public DataTable Save_Update_GreenCardForm(Hashtable theHT, DataTable theDT)
        {
            ClsObject ARTHistoryMgr = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ARTHistoryMgr.Connection = this.Connection;
                ARTHistoryMgr.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, theHT["PatientId"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["VisitId"].ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                ClsUtility.AddParameters("@visitdate", SqlDbType.VarChar, theHT["visitdate"].ToString());
                ClsUtility.AddParameters("@TransferInFrom", SqlDbType.VarChar, theHT["TransferInFrom"].ToString());
                ClsUtility.AddParameters("@TransferInDate", SqlDbType.VarChar, theHT["TransferInDate"].ToString());
                ClsUtility.AddParameters("@TransferInDistrictId", SqlDbType.Int, theHT["TransferInDistrictId"].ToString());
                ClsUtility.AddParameters("@TransferInFacilityId", SqlDbType.Int, theHT["TransferInFacilityId"].ToString());
                ClsUtility.AddParameters("@TransferMFLCode", SqlDbType.VarChar, theHT["TransferMFLCode"].ToString());
                ClsUtility.AddParameters("@TransferInCountryId", SqlDbType.Int, theHT["TransferInCountryId"].ToString());
                ClsUtility.AddParameters("@ARTStartDate", SqlDbType.VarChar, theHT["ARTStartDate"].ToString());
                ClsUtility.AddParameters("@BaseWHOStage", SqlDbType.Int, theHT["BaseWHOStage"].ToString());
                ClsUtility.AddParameters("@BaseCD4", SqlDbType.Int, theHT["BaseCD4"].ToString());
                ClsUtility.AddParameters("@BaseHepB", SqlDbType.Int, theHT["BaseHepB"].ToString());
                ClsUtility.AddParameters("@BasePregnancy", SqlDbType.Int, theHT["BasePregnancy"].ToString());
                ClsUtility.AddParameters("@BaseBreastFeeding", SqlDbType.Int, theHT["BaseBreastFeeding"].ToString());
                ClsUtility.AddParameters("@BaseTBHIV", SqlDbType.Int, theHT["BaseTBHIV"].ToString());
                ClsUtility.AddParameters("@BaseDiscordant", SqlDbType.Int, theHT["BaseDiscordant"].ToString());
                ClsUtility.AddParameters("@BaseWeight", SqlDbType.Decimal, theHT["BaseWeight"].ToString());
                ClsUtility.AddParameters("@BaseHeight", SqlDbType.Decimal, theHT["BaseHeight"].ToString());
                ClsUtility.AddParameters("@BaseMUAC", SqlDbType.Decimal, theHT["BaseMUAC"].ToString());
                ClsUtility.AddParameters("@ARTRegimen", SqlDbType.VarChar, theHT["TransferInCountryId"].ToString());
                ClsUtility.AddParameters("@BaseViralLoad", SqlDbType.Decimal, theHT["BaseViralLoad"].ToString());
                ClsUtility.AddParameters("@BaseViralLoadDate", SqlDbType.VarChar, theHT["BaseViralLoadDate"].ToString());
                ClsUtility.AddParameters("@TransferOutDate", SqlDbType.VarChar, theHT["TransferOutDate"].ToString());
                ClsUtility.AddParameters("@DeathDate", SqlDbType.VarChar, theHT["DeathDate"].ToString());
                ClsUtility.AddParameters("@PartnerDisclosure", SqlDbType.Int, theHT["PartnerDisclosure"].ToString());
                ClsUtility.AddParameters("@PartnerDisclosureDate", SqlDbType.VarChar, theHT["PartnerDisclosureDate"].ToString());
                ClsUtility.AddParameters("@AdolPartialDisclosureDate", SqlDbType.VarChar, theHT["AdolPartialDisclosureDate"].ToString());
                ClsUtility.AddParameters("@AdolFullDisclosureDate", SqlDbType.VarChar, theHT["AdolFullDisclosureDate"].ToString());
                ClsUtility.AddParameters("@INHStartDate", SqlDbType.VarChar, theHT["INHStartDate"].ToString());
                ClsUtility.AddParameters("@INHCompletion", SqlDbType.Int, theHT["INHCompletion"].ToString());
                ClsUtility.AddParameters("@INHCompletionDate", SqlDbType.VarChar, theHT["INHCompletionDate"].ToString());
                ClsUtility.AddParameters("@Measles", SqlDbType.Int, theHT["Measles"].ToString());
                ClsUtility.AddParameters("@IPV", SqlDbType.Int, theHT["IPV"].ToString());
                ClsUtility.AddParameters("@Penta", SqlDbType.Int, theHT["Penta"].ToString());
                ClsUtility.AddParameters("@PCV", SqlDbType.Int, theHT["PCV"].ToString());
                ClsUtility.AddParameters("@BCG", SqlDbType.Int, theHT["BCG"].ToString());
                ClsUtility.AddParameters("@HBV", SqlDbType.Int, theHT["HBV"].ToString());

                ClsUtility.AddParameters("@Flu", SqlDbType.Int, theHT["Flu"].ToString());
                ClsUtility.AddParameters("@HepatitisB", SqlDbType.Int, theHT["HepatitisB"].ToString());
                ClsUtility.AddParameters("@Tetanus", SqlDbType.Int, theHT["Tetanus"].ToString());
                ClsUtility.AddParameters("@Pertussis", SqlDbType.Int, theHT["Pertussis"].ToString());
                ClsUtility.AddParameters("@Haemophilus", SqlDbType.Int, theHT["Haemophilus"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserID"].ToString());




                DataTable ReturnDT = new DataTable();

                ReturnDT = (DataTable)ARTHistoryMgr.ReturnObjectNewImpl(ClsUtility.theParams, "pr_SaveUpdate_GreenCard_FORM", ClsDBUtility.ObjectEnum.DataTable);

                if (theDT.Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@VisitId", SqlDbType.Int, ReturnDT.Rows[0]["Visit_Id"].ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        ClsUtility.AddParameters("@PurposeId", SqlDbType.Int, theDR["PurposeId"].ToString());
                        ClsUtility.AddParameters("@Regimen", SqlDbType.Int, theDR["Regimen"].ToString());
                        ClsUtility.AddParameters("@RegLastUsed", SqlDbType.VarChar, theDR["RegLastUsed"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserID"].ToString());
                        int retval = (Int32)ARTHistoryMgr.ReturnObjectNewImpl(ClsUtility.theParams, "pr_Clinical_SavePatientBlueCardPriorART_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        if (retval == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving. Try Again..";
                            AppException.Create("#C1", theMsg);
                        }
                    }
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return ReturnDT;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;

            }

            finally
            {

                ARTHistoryMgr = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);


            }


        }
        public DataSet GetGreenCardData(int PatientId, int VisitId, int LocationId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsObject ARTHistoryMgr = new ClsObject();
                return (DataSet)ARTHistoryMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetGreenCardData", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
        
        public DataSet SaveUpdateGreenCardFollowupData_TriageTab(Hashtable hashTable,int DataQuality, int signature, int UserId)
        {
            try
            {
                DataSet theDS;
                int visitID;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", hashTable["visitDate"].ToString()));
                ClsUtility.AddParameters("@NursesComments", SqlDbType.VarChar, hashTable["NursesComments"].ToString());

                if (hashTable["Temp"].ToString() != "")
                {
                    ClsUtility.AddParameters("@Temp", SqlDbType.Decimal, hashTable["Temp"].ToString());
                }
                if (hashTable["RR"].ToString() != "")
                {
                    ClsUtility.AddParameters("@RR", SqlDbType.Decimal, hashTable["RR"].ToString());
                }
                if (hashTable["HR"].ToString() != "")
                {
                    ClsUtility.AddParameters("@HR", SqlDbType.Decimal, hashTable["HR"].ToString());
                }
                if (hashTable["height"].ToString() != "")
                {
                    ClsUtility.AddParameters("@height", SqlDbType.Decimal, hashTable["height"].ToString());
                }
                if (hashTable["weight"].ToString() != "")
                {
                    ClsUtility.AddParameters("@weight", SqlDbType.Decimal, hashTable["weight"].ToString());
                }
                if (hashTable["BPDiastolic"].ToString() != "")
                {
                    ClsUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, hashTable["BPDiastolic"].ToString());
                }
                if (hashTable["BPSystolic"].ToString() != "")
                {
                    ClsUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, hashTable["BPSystolic"].ToString());
                }
                if (hashTable["HeadCircumference"].ToString() != "")
                {
                    ClsUtility.AddParameters("@HeadCircumference", SqlDbType.Decimal, hashTable["HeadCircumference"].ToString());
                }
                if (hashTable["WeightForAge"].ToString() != "")
                {
                    ClsUtility.AddParameters("@WeightForAge", SqlDbType.Decimal, hashTable["WeightForAge"].ToString());
                }
                if (hashTable["WeightForHeight"].ToString() != "")
                {
                    ClsUtility.AddParameters("WeightForHeight", SqlDbType.Int, hashTable["WeightForHeight"].ToString());
                }
                
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_SaveUpdate_GreenCardForm_TriageTab", ClsDBUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];



                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return theDS;
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
        public DataSet SaveUpdateGreenCardFollowupData_PresComplaintTab(Hashtable hashTable,int DataQuality, int signature, int UserId)
        {
            try
            {
                DataSet theDS;
                int visitID;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", hashTable["visitDate"].ToString()));
                ClsUtility.AddParameters("@lmp", SqlDbType.VarChar, hashTable["lmp"].ToString());
                ClsUtility.AddParameters("@Pregnant", SqlDbType.Int, hashTable["Pregnant"].ToString());
                ClsUtility.AddParameters("@Delivered", SqlDbType.VarChar, hashTable["Delivered"].ToString());
                ClsUtility.AddParameters("@DelDate", SqlDbType.VarChar, hashTable["DelDate"].ToString());
                ClsUtility.AddParameters("@EDDDate", SqlDbType.VarChar, hashTable["EDDDate"].ToString());
                //Family planning
                ClsUtility.AddParameters("@familyPlanningStatus", SqlDbType.Int, hashTable["familyPlanningStatus"].ToString());
                if (hashTable.ContainsKey("NoFamilyPlanning"))
                {
                    ClsUtility.AddParameters("@NoFamilyPlanning", SqlDbType.Int, hashTable["NoFamilyPlanning"].ToString());
                }
                ClsUtility.AddParameters("@CervCancer", SqlDbType.Int, hashTable["CervCancer"].ToString());
                ClsUtility.AddParameters("@CervCancerDate", SqlDbType.VarChar, hashTable["CervCancerDate"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_SaveUpdate_GreenCardForm_PresComplaintTab", ClsDBUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];



                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return theDS;
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
        public DataSet SaveUpdateGreenCardFollowupData_AddHxTab(int patientID, int VisitID, int LocationID,string visitdate, DataTable theDT, DataTable theDSchklist, int DataQuality, int signature, int UserId)
        {
            int retval = 0;
            DataSet theDS;
            ClsObject KNHPMTCTHEI = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", visitdate.ToString()));

                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());


                // DataSet tempDataSet;
                theDS = (DataSet)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_SaveUpdate_GreenCardForm_AddHxTab", ClsDBUtility.ObjectEnum.DataSet);
                VisitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];
                //Immunization
                if (theDSchklist != null && theDSchklist.Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDSchklist.Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@Section", SqlDbType.VarChar, theDR["Section"].ToString());

                        ClsUtility.AddParameters("@TypeofTestId", SqlDbType.Int, theDR["TypeofTestId"].ToString());
                        ClsUtility.AddParameters("@TypeofTest", SqlDbType.VarChar, theDR["TypeofTest"].ToString());

                        ClsUtility.AddParameters("@ResultId", SqlDbType.Int, theDR["ResultId"].ToString());
                        ClsUtility.AddParameters("@Result", SqlDbType.VarChar, theDR["Result"].ToString());

                        ClsUtility.AddParameters("@Date", SqlDbType.VarChar, theDR["Date"].ToString());
                        ClsUtility.AddParameters("@Comments", SqlDbType.VarChar, theDR["Comments"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                        int temp = (int)KNHPMTCTHEI.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SaveGridViewData", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                if (theDT.Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, patientID.ToString());
                        ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitID.ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationID.ToString());
                        ClsUtility.AddParameters("@PurposeId", SqlDbType.Int, theDR["PurposeId"].ToString());
                        ClsUtility.AddParameters("@Regimen", SqlDbType.Int, theDR["Regimen"].ToString());
                        ClsUtility.AddParameters("@RegLastUsed", SqlDbType.VarChar, theDR["RegLastUsed"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                        retval = (Int32)KNHPMTCTHEI.ReturnObjectNewImpl(ClsUtility.theParams, "pr_Clinical_SavePatientBlueCardPriorART_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        if (retval == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving. Try Again..";
                            AppException.Create("#C1", theMsg);
                        }
                    }

                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return theDS;
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

        public DataSet SaveUpdateKNHPeadraticFollowupData_SysReviewTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
        {
            try
            {
                DataSet theDS;
                int visitID;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", hashTable["visitDate"].ToString()));                
                ClsUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, hashTable["OtherGeneralConditions"].ToString());
                ClsUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, hashTable["OtherAbdomenConditions"].ToString());
                ClsUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, hashTable["OtherCardiovascularConditions"].ToString());
                ClsUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, hashTable["OtherOralCavityConditions"].ToString());
                ClsUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, hashTable["OtherGenitourinaryConditions"].ToString());
                ClsUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, hashTable["OtherCNSConditions"].ToString());
                ClsUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, hashTable["OtherChestLungsConditions"].ToString());
                ClsUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, hashTable["OtherSkinConditions"].ToString());
                ClsUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());          
                ClsUtility.AddParameters("@WABStage", SqlDbType.Int, hashTable["WABStage"].ToString());
                ClsUtility.AddParameters("@CurrentWHOStage", SqlDbType.Int, hashTable["CurrentWHOStage"].ToString());
                ClsUtility.AddParameters("@Menarche", SqlDbType.Int, hashTable["Menarche"].ToString());
                ClsUtility.AddParameters("@MenarcheDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", hashTable["MenarcheDate"].ToString()));
                ClsUtility.AddParameters("@TannerStaging", SqlDbType.Int, hashTable["TannerStaging"].ToString());                
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_GreenCardFollowup_SysRewTab", ClsDBUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                //Pre Existing Medical Condition
                for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                {
                    if (dtMultiSelectValues.Rows[i]["ID"].ToString() != "")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["Other_Notes"].ToString());
                        ClsUtility.AddParameters("@DateField1", SqlDbType.DateTime, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return theDS;
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
        public DataSet SaveUpdateGreenCardFollowupData_MgtTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
        {
            try
            {
                DataSet theDS;
                int visitID;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", hashTable["visitDate"].ToString()));                
                ClsUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, hashTable["Specifyothershorttermeffects"].ToString());
                ClsUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, hashTable["OtherLongtermEffects"].ToString());
                //pharmacy
                ClsUtility.AddParameters("@CotrimoxazoleAdhere", SqlDbType.Int, hashTable["CotrimoxazoleAdhere"].ToString());
                ClsUtility.AddParameters("@ARVDrugsAdhere", SqlDbType.Int, hashTable["ARVDrugsAdhere"].ToString());
                ClsUtility.AddParameters("@WhyPooFair", SqlDbType.Int, hashTable["WhyPooFair"].ToString());
                if (hashTable.ContainsKey("reasonARVDrugsPoorFairOther"))
                {
                    ClsUtility.AddParameters("@reasonARVDrugsPoorFairOther", SqlDbType.VarChar, hashTable["reasonARVDrugsPoorFairOther"].ToString());
                }                            
                
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_GreenCardFollowupFORM_MgtTab", ClsDBUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

               
                //Pre Existing Medical Condition
                for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                {
                    if (dtMultiSelectValues.Rows[i]["ID"].ToString() != "")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["Other_Notes"].ToString());
                        ClsUtility.AddParameters("@DateField1", SqlDbType.DateTime, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return theDS;
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
        public DataSet GetGreenCardFollowup(int patientID, int VisitID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_GetGreenCardFollowupData", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
        #endregion

        #region "Delete  Form"
        public int DeleteHIVCareEncounterForms(string FormName, int OrderNo, int PatientId, int UserID)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteForm = new ClsObject();
                DeleteForm.Connection = this.Connection;
                DeleteForm.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theAffectedRows;
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
        #endregion

    }
}
