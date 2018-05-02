using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;

namespace BusinessProcess.Clinical
{
    class BNigeriaARTCard:ProcessBase,INigeriaARTCard
    {
        public DataSet SaveUpdateNigeriaAdultIEClinicalHistoryData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature)
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
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, hashTable["visitDate"].ToString());
                ClsUtility.AddParameters("@OtherPresentingComplaints", SqlDbType.VarChar, hashTable["OtherPresentingComplaints"].ToString());
                ClsUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
                ClsUtility.AddParameters("@MedicalHistoryAdditionalComplaints", SqlDbType.VarChar, hashTable["MedicalHistoryAdditionalComplaints"].ToString());
                ClsUtility.AddParameters("@MedicalHistoryLastHistory", SqlDbType.VarChar, hashTable["MedicalHistoryLastHistory"].ToString());
                ClsUtility.AddParameters("@MedicalHistoryFamilyHistory", SqlDbType.VarChar, hashTable["MedicalHistoryFamilyHistory"].ToString());
                ClsUtility.AddParameters("@MedicalHistoryHospitalization", SqlDbType.VarChar, hashTable["MedicalHistoryHospitalization"].ToString());

                ClsUtility.AddParameters("@Pregnant", SqlDbType.Int, hashTable["Pregnant"].ToString());
                ClsUtility.AddParameters("@LMPDate", SqlDbType.VarChar, hashTable["LMPDate"].ToString());
                ClsUtility.AddParameters("@EDDDate", SqlDbType.VarChar, hashTable["EDDDate"].ToString());
                ClsUtility.AddParameters("@GestationalAge", SqlDbType.Int, hashTable["GestationalAge"].ToString());

                
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_Nigeria_AdultIE_ClinicalHistoryTab", ClsDBUtility.ObjectEnum.DataSet);
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
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["Other_notes"].ToString());
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
        public DataSet SaveUpdateNigeriaAdultIEHIVHistoryData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, DataTable dtPriorART)
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
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, hashTable["visitDate"].ToString()); 
                ClsUtility.AddParameters("@PrevARVExposure", SqlDbType.Int, hashTable["PrevARVExposure"].ToString());
                ClsUtility.AddParameters("@ARTTransferinFrom", SqlDbType.Int, hashTable["ARTTransferinFrom"].ToString());
                ClsUtility.AddParameters("@durationcarefrom", SqlDbType.VarChar, hashTable["durationcarefrom"].ToString());
                ClsUtility.AddParameters("@durationcareto", SqlDbType.VarChar, hashTable["durationcareto"].ToString());
                ClsUtility.AddParameters("@EntryType", SqlDbType.Int, hashTable["EntryType"].ToString());
                ClsUtility.AddParameters("@LatestCD4", SqlDbType.VarChar, hashTable["LatestCD4"].ToString());
                ClsUtility.AddParameters("@LatestCD4Date", SqlDbType.VarChar, hashTable["LatestCD4Date"].ToString());
                ClsUtility.AddParameters("@LowestCD4", SqlDbType.Int, hashTable["LowestCD4"].ToString());
                ClsUtility.AddParameters("@LowestCD4Date", SqlDbType.DateTime, hashTable["LowestCD4Date"].ToString());
                ClsUtility.AddParameters("@LatestViralLoad", SqlDbType.Int, hashTable["LatestViralLoad"].ToString());
                ClsUtility.AddParameters("@LatestViralLoadDate", SqlDbType.DateTime, hashTable["LatestViralLoadDate"].ToString());
                ClsUtility.AddParameters("@ComplaintOther", SqlDbType.VarChar, hashTable["ComplaintOther"].ToString());
                ClsUtility.AddParameters("@ServiceEntry", SqlDbType.Int, hashTable["ServiceEntry"].ToString());
                ClsUtility.AddParameters("@ParticipatedAdhernce", SqlDbType.Int, hashTable["ParticipatedAdhernce"].ToString());
                ClsUtility.AddParameters("@MissedArv3days", SqlDbType.Int, hashTable["MissedArv3days"].ToString());
                ClsUtility.AddParameters("@ReasomMissedARV", SqlDbType.Int, hashTable["ReasomMissedARV"].ToString());
                ClsUtility.AddParameters("@TreatmentIntrupted", SqlDbType.Int, hashTable["TreatmentIntrupted"].ToString());
                //ClsUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
                ClsUtility.AddParameters("@IntrupptedDate", SqlDbType.VarChar, hashTable["IntrupptedDate"].ToString());
                ClsUtility.AddParameters("@intrpdays", SqlDbType.Int, hashTable["intrpdays"].ToString());
                ClsUtility.AddParameters("@ReasonInterrupted", SqlDbType.Int, hashTable["ReasonInterrupted"].ToString());
                ClsUtility.AddParameters("@Treatmentstopped", SqlDbType.Int, hashTable["Treatmentstopped"].ToString());
                ClsUtility.AddParameters("@StopedReasonDate", SqlDbType.VarChar, hashTable["StopedReasonDate"].ToString());
                ClsUtility.AddParameters("@stoppeddays", SqlDbType.Int, hashTable["stoppeddays"].ToString());
                ClsUtility.AddParameters("@StopedReason", SqlDbType.Int, hashTable["StopedReason"].ToString());
                ClsUtility.AddParameters("@Otherdisclosed", SqlDbType.VarChar, hashTable["Otherdisclosed"].ToString());
                ClsUtility.AddParameters("@hivdiscussed", SqlDbType.VarChar, hashTable["hivdiscussed"].ToString());
                ClsUtility.AddParameters("@supportgroup", SqlDbType.Int, hashTable["supportgroup"].ToString());
                ClsUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, hashTable["OtherShortTermEffects"].ToString());
                ClsUtility.AddParameters("@OtherLongtermEffects", SqlDbType.Int, hashTable["OtherLongtermEffects"].ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_Nigeria_AdultIE_HIVHistoryTab", ClsDBUtility.ObjectEnum.DataSet);
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
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["Other_notes"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Prior ART
                for (int i = 0; i < dtPriorART.Rows.Count; i++)
                {
                    if (dtPriorART.Rows[i]["ID"].ToString() != "")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, dtPriorART.Rows[i]["ID"].ToString());
                        ClsUtility.AddParameters("@NumericFiled", SqlDbType.Int, dtPriorART.Rows[i]["NumericField"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "NigeriaPriorART");
                        ClsUtility.AddParameters("@DateField1", SqlDbType.VarChar, dtPriorART.Rows[i]["DateField1"].ToString());
                        ClsUtility.AddParameters("@DateField2", SqlDbType.VarChar, dtPriorART.Rows[i]["DateField2"].ToString());
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
        public DataSet SaveUpdateNigeriaAdultIEExaminationData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature)
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
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, hashTable["visitDate"].ToString());
                if (hashTable["Temp"].ToString() != "")
                {
                    ClsUtility.AddParameters("@Temp", SqlDbType.Decimal, hashTable["Temp"].ToString());
                }
                if (hashTable["RR"].ToString() != "")
                {
                    ClsUtility.AddParameters("@RR", SqlDbType.Decimal, hashTable["RR"].ToString());
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
                
                ClsUtility.AddParameters("@OtherNigeriaPEGeneral", SqlDbType.VarChar, hashTable["OtherNigeriaPEGeneral"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPESkin", SqlDbType.VarChar, hashTable["OtherNigeriaPESkin"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPEHeadEyeEnt", SqlDbType.VarChar, hashTable["OtherNigeriaPEHeadEyeEnt"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPECardiovascular", SqlDbType.VarChar, hashTable["OtherNigeriaPECardiovascular"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPEBreast", SqlDbType.VarChar, hashTable["OtherNigeriaPEBreast"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPEGenitalia", SqlDbType.VarChar, hashTable["OtherNigeriaPEGenitalia"].ToString());
                ClsUtility.AddParameters("@txtOtherNigeriaPERespiratory", SqlDbType.VarChar, hashTable["txtOtherNigeriaPERespiratory"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPEGastrointestinal", SqlDbType.VarChar, hashTable["OtherNigeriaPEGastrointestinal"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPENeurological", SqlDbType.VarChar, hashTable["OtherNigeriaPENeurological"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPEMentalstatus", SqlDbType.VarChar, hashTable["OtherNigeriaPEMentalstatus"].ToString());
                ClsUtility.AddParameters("@OtherAdditionaldetailedfindings", SqlDbType.VarChar, hashTable["OtherAdditionaldetailedfindings"].ToString());
                ClsUtility.AddParameters("@Assessment", SqlDbType.Int, hashTable["Assessment"].ToString());
                ClsUtility.AddParameters("@AssessmentDescription", SqlDbType.VarChar, hashTable["AssessmentDesc"].ToString());
                ClsUtility.AddParameters("@WHOStage", SqlDbType.Int, hashTable["WHOStage"].ToString()); 
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_Nigeria_AdultIE_ExamTab", ClsDBUtility.ObjectEnum.DataSet);
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
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["Other_notes"].ToString());
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
        public DataSet SaveUpdateNigeriaAdultIEManagementData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature)
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
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, hashTable["visitDate"].ToString());
                ClsUtility.AddParameters("@LabEvaluation", SqlDbType.Int, hashTable["LabEvaluation"].ToString());
                ClsUtility.AddParameters("@LabReview", SqlDbType.VarChar, hashTable["LabReview"].ToString());
                ClsUtility.AddParameters("@OtherReferrals", SqlDbType.VarChar, hashTable["OtherReferrals"].ToString());
                ClsUtility.AddParameters("@Regimen", SqlDbType.Int, hashTable["Regimen"].ToString());
                ClsUtility.AddParameters("@ARVTherapyPlan", SqlDbType.Int, hashTable["ARVTherapyPlan"].ToString());
                ClsUtility.AddParameters("@OtherARVChangePlan", SqlDbType.DateTime, hashTable["OtherARVChangePlan"].ToString());

                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_Nigeria_AdultIE_MgtTab", ClsDBUtility.ObjectEnum.DataSet);
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
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["Other_notes"].ToString());
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
        public DataSet GetNigeriaAdultIEDetails(int ptn_pk, int visitpk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_NigeriaAdultIE", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        #region "Paeditric IE"
        public DataSet SaveUpdateNigeriaPaedIEClinicalHistoryData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, DataTable dtPriorART)
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
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, hashTable["visitDate"].ToString());                
                ClsUtility.AddParameters("@MedicalHistoryAdditionalComplaints", SqlDbType.VarChar, hashTable["MedicalHistoryAdditionalComplaints"].ToString());
                ClsUtility.AddParameters("@MedicalHistoryLastHistory", SqlDbType.VarChar, hashTable["MedicalHistoryLastHistory"].ToString());
                ClsUtility.AddParameters("@DevelopmentAsses", SqlDbType.Int, hashTable["DevelopmentAssesment"].ToString());
                ClsUtility.AddParameters("@ImmunAge", SqlDbType.Int, hashTable["ImmunizationComplete"].ToString());
                ClsUtility.AddParameters("@PrevHIV", SqlDbType.Int, hashTable["PreviousCareHIV"].ToString());
                ClsUtility.AddParameters("@FeedMode", SqlDbType.Int, hashTable["FeedMode"].ToString());
                ClsUtility.AddParameters("@PrevExpoPMTCT", SqlDbType.Int, hashTable["PrevARVExposurePMTCT"].ToString());
                ClsUtility.AddParameters("@PrevExpoPMTCTMonths", SqlDbType.VarChar, hashTable["PrevARVExposurePMTCTMonths"].ToString());
                ClsUtility.AddParameters("@PrevExpoPMTCTDrugs", SqlDbType.VarChar, hashTable["PrevARVExposurePMTCTDrugs"].ToString());
                ClsUtility.AddParameters("@currentmedicationother", SqlDbType.VarChar, hashTable["ComplaintOther"].ToString());
                ClsUtility.AddParameters("@ServiceEntry", SqlDbType.Int, hashTable["ServiceEntry"].ToString());
                ClsUtility.AddParameters("@ParticipatedAdhernce", SqlDbType.Int, hashTable["ParticipatedAdhernce"].ToString());
                ClsUtility.AddParameters("@MissedArv3days", SqlDbType.Int, hashTable["MissedArv3days"].ToString());
                ClsUtility.AddParameters("@ReasomMissedARV", SqlDbType.Int, hashTable["ReasomMissedARV"].ToString());
                ClsUtility.AddParameters("@TreatmentIntrupted", SqlDbType.Int, hashTable["TreatmentIntrupted"].ToString());               
                ClsUtility.AddParameters("@IntrupptedDate", SqlDbType.VarChar, hashTable["IntrupptedDate"].ToString());
                ClsUtility.AddParameters("@intrpdays", SqlDbType.Int, hashTable["intrpdays"].ToString());
                ClsUtility.AddParameters("@ReasonInterrupted", SqlDbType.Int, hashTable["ReasonInterrupted"].ToString());
                ClsUtility.AddParameters("@Treatmentstopped", SqlDbType.Int, hashTable["Treatmentstopped"].ToString());
                ClsUtility.AddParameters("@StopedReasonDate", SqlDbType.VarChar, hashTable["StopedReasonDate"].ToString());
                ClsUtility.AddParameters("@stoppeddays", SqlDbType.Int, hashTable["stoppeddays"].ToString());
                ClsUtility.AddParameters("@StopedReason", SqlDbType.Int, hashTable["StopedReason"].ToString());
                ClsUtility.AddParameters("@Otherdisclosed", SqlDbType.VarChar, hashTable["Otherdisclosed"].ToString());
                ClsUtility.AddParameters("@hivdiscussed", SqlDbType.VarChar, hashTable["hivdiscussed"].ToString());
                ClsUtility.AddParameters("@supportgroup", SqlDbType.Int, hashTable["supportgroup"].ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_Nigeria_PaedIE_ClinicalHistoryTab", ClsDBUtility.ObjectEnum.DataSet);
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
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["Other_notes"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Prior ART
                for (int i = 0; i < dtPriorART.Rows.Count; i++)
                {
                    if (dtPriorART.Rows[i]["ID"].ToString() != "")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, dtPriorART.Rows[i]["ID"].ToString());
                        ClsUtility.AddParameters("@NumericFiled", SqlDbType.Int, dtPriorART.Rows[i]["NumericField"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "NigeriaPriorART");
                        ClsUtility.AddParameters("@DateField1", SqlDbType.VarChar, dtPriorART.Rows[i]["DateField1"].ToString());
                        ClsUtility.AddParameters("@DateField2", SqlDbType.VarChar, dtPriorART.Rows[i]["DateField2"].ToString());
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
        
        public DataSet SaveUpdateNigeriaPaedIEExaminationData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature)
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
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, hashTable["visitDate"].ToString());
                if (hashTable["Temp"].ToString() != "")
                {
                    ClsUtility.AddParameters("@Temp", SqlDbType.Decimal, hashTable["Temp"].ToString());
                }
                if (hashTable["RR"].ToString() != "")
                {
                    ClsUtility.AddParameters("@RR", SqlDbType.Decimal, hashTable["RR"].ToString());
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

                ClsUtility.AddParameters("@MUAC", SqlDbType.Int, hashTable["MUAC"].ToString());
                if (hashTable["SurfaceArea"].ToString() != "")
                {
                    ClsUtility.AddParameters("@SurfaceArea", SqlDbType.Decimal, hashTable["SurfaceArea"].ToString());
                }               

                ClsUtility.AddParameters("@OtherNigeriaPEGeneral", SqlDbType.VarChar, hashTable["OtherNigeriaPEGeneral"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPESkin", SqlDbType.VarChar, hashTable["OtherNigeriaPESkin"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPEHeadEyeEnt", SqlDbType.VarChar, hashTable["OtherNigeriaPEHeadEyeEnt"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPECardiovascular", SqlDbType.VarChar, hashTable["OtherNigeriaPECardiovascular"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPEBreast", SqlDbType.VarChar, hashTable["OtherNigeriaPEBreast"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPEGenitalia", SqlDbType.VarChar, hashTable["OtherNigeriaPEGenitalia"].ToString());
                ClsUtility.AddParameters("@txtOtherNigeriaPERespiratory", SqlDbType.VarChar, hashTable["txtOtherNigeriaPERespiratory"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPEGastrointestinal", SqlDbType.VarChar, hashTable["OtherNigeriaPEGastrointestinal"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPENeurological", SqlDbType.VarChar, hashTable["OtherNigeriaPENeurological"].ToString());
                ClsUtility.AddParameters("@OtherNigeriaPEMentalstatus", SqlDbType.VarChar, hashTable["OtherNigeriaPEMentalstatus"].ToString());
                ClsUtility.AddParameters("@OtherAdditionaldetailedfindings", SqlDbType.VarChar, hashTable["OtherAdditionaldetailedfindings"].ToString());
                ClsUtility.AddParameters("@Assessment", SqlDbType.Int, hashTable["Assessment"].ToString());
                ClsUtility.AddParameters("@WHOStage", SqlDbType.Int, hashTable["WHOStage"].ToString());
                
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_Nigeria_PaedIE_ExamTab", ClsDBUtility.ObjectEnum.DataSet);
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
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["Other_notes"].ToString());
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
        public DataSet SaveUpdateNigeriaPaedIEManagementData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature)
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
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, hashTable["visitDate"].ToString());
                ClsUtility.AddParameters("@LabEvaluation", SqlDbType.Int, hashTable["LabEvaluation"].ToString());
                ClsUtility.AddParameters("@LabReview", SqlDbType.VarChar, hashTable["LabReview"].ToString());
                ClsUtility.AddParameters("@OtherReferrals", SqlDbType.VarChar, hashTable["OtherReferrals"].ToString());
                ClsUtility.AddParameters("@Regimen", SqlDbType.Int, hashTable["Regimen"].ToString());
                ClsUtility.AddParameters("@ARVTherapyPlan", SqlDbType.Int, hashTable["ARVTherapyPlan"].ToString());
                ClsUtility.AddParameters("@OtherARVChangePlan", SqlDbType.DateTime, hashTable["OtherARVChangePlan"].ToString());

                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_Nigeria_PaedIE_MgtTab", ClsDBUtility.ObjectEnum.DataSet);
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
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["Other_notes"].ToString());
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
        public DataSet GetNigeriaPaedIEDetails(int ptn_pk, int visitpk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_NigeriaPaedIE", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        #endregion
        #region "Initial Visit"
        public DataSet SaveUpdateInitialVisitData(Hashtable hashTable, int DataQuality, int signature)
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
                ClsUtility.AddParameters("@CareEntry", SqlDbType.Int, hashTable["CareEntry"].ToString());
                ClsUtility.AddParameters("@OtherCareEntry", SqlDbType.VarChar, hashTable["OtherCareEntry"].ToString());
                ClsUtility.AddParameters("@ConfirmHIVDate", SqlDbType.VarChar, hashTable["ConfirmHIVDate"].ToString());
                ClsUtility.AddParameters("@ModeHIVTest", SqlDbType.Int, hashTable["ModeHIVTest"].ToString());
                ClsUtility.AddParameters("@TestLocation", SqlDbType.VarChar, hashTable["TestLocation"].ToString());
                ClsUtility.AddParameters("@PriorART", SqlDbType.Int, hashTable["PriorART"].ToString());
                ClsUtility.AddParameters("@ElligibleDate", SqlDbType.VarChar, hashTable["ElligibleDate"].ToString());
                ClsUtility.AddParameters("@WhyElligible", SqlDbType.Int, hashTable["WhyElligible"].ToString());
                ClsUtility.AddParameters("@AdhCounslingDate", SqlDbType.VarChar, hashTable["AdhCounslingDate"].ToString());
                ClsUtility.AddParameters("@DateTransferedIn", SqlDbType.VarChar, hashTable["DateTransferedIn"].ToString());
                ClsUtility.AddParameters("@FacilityTransferFrom", SqlDbType.Int, hashTable["FacilityTransferFrom"].ToString());

                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_Nigeria_InitialVisit", ClsDBUtility.ObjectEnum.DataSet);
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
        public DataSet GetNigeriaInitialVisitDetails(int ptn_pk, int visitpk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_NigeriaInitialVisit", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataTable GetNigeriaInitialVisitId(int ptn_pk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                return (DataTable)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_NigeriaInitialVisitID", ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        #endregion
        public DataSet GetNigeriaPriorARTDetails(int ptn_pk, int visitpk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_NigeriaPriorART", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
    }
}
