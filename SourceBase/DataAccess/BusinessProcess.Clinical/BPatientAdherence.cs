using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using DataAccess.Base;
using Interface.Clinical;
using DataAccess.Common;
using System.Data;
using DataAccess.Entity;
using Entities.Common;
using Application.Common;

namespace BusinessProcess.Clinical
{
    [Serializable()]
    public class BPatientAdherence : ProcessBase, IPatientAdherence
    {
        //ClsDBUtility oUtility = new ClsDBUtility();
        public BPatientAdherence()
        {

        }
        #region Enhance Adherence Counselling
        public PatientAdherenceCounseling GetEnhanceAdherenceCounsellingDataByPatient(int ptn_pk, int locationID, int visitPK, int sessionID)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BPatientAdherence.GetPatientDataforEnhanceAdherence() Method Called.");
            PatientAdherenceCounseling patientAdherenceCounseling = new PatientAdherenceCounseling();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Location_Id", SqlDbType.Int, locationID.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitPK.ToString());
                ClsUtility.AddParameters("@Session_Id", SqlDbType.Int, sessionID.ToString());
                ds = (DataSet)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_Adherence_GetEnhanceAdherenceCounsellingDataByPatient", ClsDBUtility.ObjectEnum.DataSet);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //string.IsNullOrEmpty(dt.Rows[0]["UserId"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["UserId"].ToString()) : 0;
                    patientAdherenceCounseling.PAM_Id = string.IsNullOrEmpty(dt.Rows[0]["PAM_Id"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["PAM_Id"].ToString()) : 0;
                    patientAdherenceCounseling.Ptn_Pk = string.IsNullOrEmpty(dt.Rows[0]["Ptn_pk"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["Ptn_pk"].ToString()) : 0;
                    patientAdherenceCounseling.Visit_Id = string.IsNullOrEmpty(dt.Rows[0]["Visit_Id"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["Visit_Id"].ToString()) : 0;
                    patientAdherenceCounseling.LocationId = string.IsNullOrEmpty(dt.Rows[0]["Location_Id"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["Location_Id"].ToString()) : 0;

                    patientAdherenceCounseling.IsForgotMed = string.IsNullOrEmpty(dt.Rows[0]["ForgetMedicineSinceLastVisit"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["ForgetMedicineSinceLastVisit"].ToString()) : 0;
                    patientAdherenceCounseling.IsCarelessMed = string.IsNullOrEmpty(dt.Rows[0]["CarelessAboutTakingMedicine"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["CarelessAboutTakingMedicine"].ToString()) : 0;
                    patientAdherenceCounseling.IsWorseTakingMed = string.IsNullOrEmpty(dt.Rows[0]["FeelWorseStopTakingMedicine"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["FeelWorseStopTakingMedicine"].ToString()) : 0;
                    patientAdherenceCounseling.IsFeelBetterMed = string.IsNullOrEmpty(dt.Rows[0]["FeelBetterStopTakingMedicine"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["FeelBetterStopTakingMedicine"].ToString()) : 0;
                    patientAdherenceCounseling.MMAS4Score = string.IsNullOrEmpty(dt.Rows[0]["MMAS4_Score"].ToString()) == false ? dt.Rows[0]["MMAS4_Score"].ToString() : "";
                    patientAdherenceCounseling.MMAS4Rating = string.IsNullOrEmpty(dt.Rows[0]["MMAS4_AdherenceRating"].ToString()) == false ? dt.Rows[0]["MMAS4_AdherenceRating"].ToString() : "";
                    patientAdherenceCounseling.IsYesterdayMed = string.IsNullOrEmpty(dt.Rows[0]["TakeMedicineYesterday"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["TakeMedicineYesterday"].ToString()) : 0;
                    patientAdherenceCounseling.IsSymptomUnderControl = string.IsNullOrEmpty(dt.Rows[0]["SymptomsUnderControl_StopTakingMedicine"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["SymptomsUnderControl_StopTakingMedicine"].ToString()) : 0;
                    patientAdherenceCounseling.IsStickingTreatmentPlan = string.IsNullOrEmpty(dt.Rows[0]["UnderPresureStickingYourTreatmentPlan"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["UnderPresureStickingYourTreatmentPlan"].ToString()) : 0;
                    patientAdherenceCounseling.RememberingMedications = string.IsNullOrEmpty(dt.Rows[0]["RememberingMedications"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["RememberingMedications"].ToString()) : 0;
                    patientAdherenceCounseling.MMAS8Score = string.IsNullOrEmpty(dt.Rows[0]["MMAS8_Score"].ToString()) == false ? dt.Rows[0]["MMAS8_Score"].ToString() : "";
                    patientAdherenceCounseling.MMAS8Rating = string.IsNullOrEmpty(dt.Rows[0]["MMAS8_AdherenceRating"].ToString()) == false ? dt.Rows[0]["MMAS8_AdherenceRating"].ToString() : "";

                    patientAdherenceCounseling.AssignedCaseManager = string.IsNullOrEmpty(dt.Rows[0]["Signature"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["Signature"].ToString()) : 0;
                    patientAdherenceCounseling.AssignedDate = string.IsNullOrEmpty(dt.Rows[0]["CaseAssignDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["CaseAssignDate"].ToString()) : (DateTime?)null;
                    patientAdherenceCounseling.TherapyImplemented = string.IsNullOrEmpty(dt.Rows[0]["TherapyImplemented"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["TherapyImplemented"].ToString()) : 0;
                    patientAdherenceCounseling.CurrentSession = string.IsNullOrEmpty(dt.Rows[0]["CurrentSession"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["CurrentSession"].ToString()) : 1;
                    patientAdherenceCounseling.FormFilledDate = string.IsNullOrEmpty(dt.Rows[0]["SessionFillDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["SessionFillDate"].ToString()) : (DateTime?)null;
                    patientAdherenceCounseling.AdherencePercent = string.IsNullOrEmpty(dt.Rows[0]["AdherencePercent"].ToString()) == false ? dt.Rows[0]["AdherencePercent"].ToString() : string.Empty;
                    patientAdherenceCounseling.PatientUnderstandVL = string.IsNullOrEmpty(dt.Rows[0]["UnderstandVL"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["UnderstandVL"].ToString()) : 0;
                    patientAdherenceCounseling.PatientFeelingAboutResult = dt.Rows[0]["FeelConcerningResult"].ToString();
                    patientAdherenceCounseling.PatientThinkAboutHighVL = dt.Rows[0]["CauseOfHighVL"].ToString();
                    patientAdherenceCounseling.HIVStatusArtDiscussion = dt.Rows[0]["HIVStatusArtDiscussion"].ToString();
                    patientAdherenceCounseling.ExplainedHowTakeDrug = dt.Rows[0]["BehaviouralBarriers"].ToString();
                    patientAdherenceCounseling.PatientDailyRoutine = dt.Rows[0]["PatientDailyRoutines"].ToString();
                    patientAdherenceCounseling.TravelVisitDescription = dt.Rows[0]["TravelVisitDescription"].ToString();
                    patientAdherenceCounseling.SideEffect = dt.Rows[0]["SideEffects"].ToString();
                    patientAdherenceCounseling.DifficultSituationTakeDrug = dt.Rows[0]["SituationsTakeDrug"].ToString();
                    patientAdherenceCounseling.TakeDrugEveryDay = dt.Rows[0]["EmotionalBarriers"].ToString();
                    patientAdherenceCounseling.PatientMotivation = dt.Rows[0]["Motivation"].ToString();
                    patientAdherenceCounseling.SupportSystem = string.IsNullOrEmpty(dt.Rows[0]["SupportSystem"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["SupportSystem"].ToString()) : 0;
                    patientAdherenceCounseling.SupportSystemDetails = dt.Rows[0]["SupportSystemDetails"].ToString();
                    patientAdherenceCounseling.SourceOfIncome = dt.Rows[0]["SourceOfIncome"].ToString();
                    patientAdherenceCounseling.ChallengesGettingClinic = dt.Rows[0]["ChallengesGettingClinic"].ToString();
                    patientAdherenceCounseling.HIVStatusBotherDetails = dt.Rows[0]["HIVStatusBotherDetails"].ToString();
                    patientAdherenceCounseling.PeopleTreatmentDetails = dt.Rows[0]["PeopleTreatmentDetails"].ToString();
                    patientAdherenceCounseling.StigmaInterfering = dt.Rows[0]["StigmaInterfering"].ToString();
                    patientAdherenceCounseling.ReligiousBeliefs = dt.Rows[0]["ReligiousBeliefs"].ToString();
                    patientAdherenceCounseling.ReferredOtherServices = string.IsNullOrEmpty(dt.Rows[0]["ReferredOtherServices"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["ReferredOtherServices"].ToString()) : 0;
                    patientAdherenceCounseling.AppointmentsAttend = string.IsNullOrEmpty(dt.Rows[0]["AppointmentsAttend"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["AppointmentsAttend"].ToString()) : 0;
                    patientAdherenceCounseling.ExperienceAppointReorganized = dt.Rows[0]["ExperienceAppointReorganized"].ToString();
                    patientAdherenceCounseling.BenefitHomeVisit = string.IsNullOrEmpty(dt.Rows[0]["BenefitHomeVisit"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["BenefitHomeVisit"].ToString()) : 0;
                    patientAdherenceCounseling.AdherencePlan = dt.Rows[0]["AdherencePlan"].ToString();
                    patientAdherenceCounseling.FollowupDate = string.IsNullOrEmpty(dt.Rows[0]["FollowupDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["FollowupDate"].ToString()) : (DateTime?)null;
                    patientAdherenceCounseling.Total = string.IsNullOrEmpty(dt.Rows[0]["Total"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["Total"].ToString()) : 0;
                    patientAdherenceCounseling.DepressionSeverity = dt.Rows[0]["DepressionSeverity"].ToString();
                    patientAdherenceCounseling.Recommended = dt.Rows[0]["Recommended"].ToString();

                }
                if (ds.Tables[1] != null)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        patientAdherenceCounseling.AleardySavedTabs = (from dtable in ds.Tables[1].AsEnumerable()
                                                                       select new CodeDeCode()
                                                                       {
                                                                           CodeId = dtable.Field<int>("featureid"),
                                                                           CodeName = dtable.Field<string>("featurename"),
                                                                           DeCodeId = dtable.Field<int>("tabid"),
                                                                           DeCodeName = dtable.Field<string>("tabname")
                                                                       }).ToList();
                    }
                }

                if (ds.Tables[2] != null)
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {

                        patientAdherenceCounseling.MultiVisit = string.IsNullOrEmpty(ds.Tables[2].Rows[0]["MultiVisit"].ToString()) == false ?
                            Convert.ToInt32(ds.Tables[2].Rows[0]["MultiVisit"].ToString()) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BPatientAdherence.GetPatientDataforEnhanceAdherence() Method:" + ex.ToString());
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return patientAdherenceCounseling;
        }

        public int SaveUpdateEnhanceAdherenceCounsellingSessions(PatientAdherenceCounseling obj, int userID)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BPatientAdherence.SaveUpdateEnhanceAdherenceCounsellingSessions() method called");
            bool flag = true;
            int visitId = 0;
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@Location_Id", SqlDbType.Int, obj.LocationId.ToString());
                ClsUtility.AddParameters("@AssignedCaseManager", SqlDbType.Int, obj.AssignedCaseManager.ToString());
                ClsUtility.AddParameters("@CurrentSession", SqlDbType.Int, obj.CurrentSession.ToString());
                ClsUtility.AddParameters("@ExperienceAppointReorganized", SqlDbType.VarChar, obj.ExperienceAppointReorganized.ToString());
                ClsUtility.AddParameters("@BenefitHomeVisit", SqlDbType.Int, obj.BenefitHomeVisit.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());

                if (obj.CurrentSession == 1)
                {
                    ClsUtility.AddParameters("@AssignedDate", SqlDbType.DateTime, obj.AssignedDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss"));
                    ClsUtility.AddParameters("@TherapyImplemented", SqlDbType.Int, obj.TherapyImplemented.ToString());
                    ClsUtility.AddParameters("@PatientFeelingAboutResult", SqlDbType.VarChar, obj.PatientFeelingAboutResult.ToString());
                    ClsUtility.AddParameters("@PatientThinkAboutHighVL", SqlDbType.VarChar, obj.PatientThinkAboutHighVL.ToString());
                    ClsUtility.AddParameters("@HIVStatusArtDiscussion", SqlDbType.VarChar, obj.HIVStatusArtDiscussion.ToString());
                    ClsUtility.AddParameters("@ExplainedHowTakeDrug", SqlDbType.VarChar, obj.ExplainedHowTakeDrug.ToString());
                    ClsUtility.AddParameters("@TravelVisitDescription", SqlDbType.VarChar, obj.TravelVisitDescription.ToString());
                    ClsUtility.AddParameters("@SideEffect", SqlDbType.VarChar, obj.SideEffect.ToString());
                    ClsUtility.AddParameters("@DifficultSituationTakeDrug", SqlDbType.VarChar, obj.DifficultSituationTakeDrug.ToString());
                    ClsUtility.AddParameters("@TakeDrugEveryDay", SqlDbType.VarChar, obj.TakeDrugEveryDay.ToString());
                    ClsUtility.AddParameters("@PatientMotivation", SqlDbType.VarChar, obj.PatientMotivation.ToString());
                    ClsUtility.AddParameters("@SupportSystem", SqlDbType.Int, obj.SupportSystem.ToString());
                    ClsUtility.AddParameters("@SupportSystemDetails", SqlDbType.VarChar, obj.SupportSystemDetails.ToString());
                    ClsUtility.AddParameters("@SourceOfIncome", SqlDbType.VarChar, obj.SourceOfIncome.ToString());
                    ClsUtility.AddParameters("@ChallengesGettingClinic", SqlDbType.VarChar, obj.ChallengesGettingClinic.ToString());
                    ClsUtility.AddParameters("@HIVStatusBotherDetails", SqlDbType.VarChar, obj.HIVStatusBotherDetails.ToString());
                    ClsUtility.AddParameters("@PeopleTreatmentDetails", SqlDbType.VarChar, obj.PeopleTreatmentDetails.ToString());
                    ClsUtility.AddParameters("@StigmaInterfering", SqlDbType.VarChar, obj.StigmaInterfering.ToString());
                    ClsUtility.AddParameters("@ReligiousBeliefs", SqlDbType.VarChar, obj.ReligiousBeliefs.ToString());
                }

                if (obj.CurrentSession != 5)
                {
                    ClsUtility.AddParameters("@ForgetMedicineSinceLastVisit", SqlDbType.Int, obj.IsForgotMed.ToString());
                    ClsUtility.AddParameters("@CarelessAboutTakingMedicine", SqlDbType.Int, obj.IsCarelessMed.ToString());
                    ClsUtility.AddParameters("@FeelWorseStopTakingMedicine", SqlDbType.Int, obj.IsWorseTakingMed.ToString());
                    ClsUtility.AddParameters("@FeelBetterStopTakingMedicine", SqlDbType.Int, obj.IsFeelBetterMed.ToString());
                    ClsUtility.AddParameters("@TakeMedicineYesterday", SqlDbType.Int, obj.IsYesterdayMed.ToString());
                    ClsUtility.AddParameters("@SymptomsUnderControl_StopTakingMedicine", SqlDbType.Int, obj.IsSymptomUnderControl.ToString());
                    ClsUtility.AddParameters("@UnderPresureStickingYourTreatmentPlan", SqlDbType.Int, obj.IsStickingTreatmentPlan.ToString());
                    ClsUtility.AddParameters("@RememberingMedications", SqlDbType.Int, obj.RememberingMedications.ToString());
                    ClsUtility.AddParameters("@MMAS4_Score", SqlDbType.VarChar, obj.MMAS4Score.ToString());
                    ClsUtility.AddParameters("@MMAS8_Score", SqlDbType.VarChar, obj.MMAS8Score.ToString());
                    ClsUtility.AddParameters("@MMAS4_AdherenceRating", SqlDbType.VarChar, obj.MMAS4Rating.ToString());
                    ClsUtility.AddParameters("@MMAS8_AdherenceRating", SqlDbType.VarChar, obj.MMAS8Rating.ToString());
                    ClsUtility.AddParameters("@ReferToCounselor", SqlDbType.VarChar, obj.MMAS8Suggestion.ToString());
                    ClsUtility.AddParameters("@FormFilledDate", SqlDbType.DateTime, obj.FormFilledDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss"));
                    ClsUtility.AddParameters("@AdherencePercent", SqlDbType.Int, obj.AdherencePercent.ToString());
                    ClsUtility.AddParameters("@PatientUnderstandVL", SqlDbType.Int, obj.PatientUnderstandVL.ToString());
                    ClsUtility.AddParameters("@PatientDailyRoutine", SqlDbType.VarChar, obj.PatientDailyRoutine.ToString());
                    ClsUtility.AddParameters("@ReferredOtherServices", SqlDbType.Int, obj.ReferredOtherServices.ToString());
                    ClsUtility.AddParameters("@AppointmentsAttend", SqlDbType.Int, obj.AppointmentsAttend.ToString());
                    ClsUtility.AddParameters("@AdherencePlan", SqlDbType.VarChar, obj.AdherencePlan.ToString());
                    ClsUtility.AddParameters("@FollowupDate", SqlDbType.DateTime, obj.FollowupDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                DataTable dt = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_Adherence_SaveUpdateEnhanceAdherenceCounsellingSessions", ClsDBUtility.ObjectEnum.DataTable);
                visitId = (int)dt.Rows[0]["VisitId"];
                return visitId;
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BPatientAdherence.SaveUpdateEnhanceAdherenceCounsellingSessions() Method:" + ex.ToString());
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        #endregion

        #region Assessing Barriers to Adherence

        public PatientAdherenceBarrier GetPatientAdherenceBarriers(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BPatientAdherence.GetPatientAdherenceBarriers() Method Called.");
            PatientAdherenceBarrier patientadherencebarrier = new PatientAdherenceBarrier();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                DataTable dt = new DataTable();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitPK.ToString());
                ClsUtility.AddParameters("@Locationid", SqlDbType.Int, locationId.ToString());

                dt = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_Adherence_GetPatientAdherenceBarriers", ClsDBUtility.ObjectEnum.DataTable);

                if (dt.Rows.Count > 0)
                {
                    //string.IsNullOrEmpty(dt.Rows[0]["UserId"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["UserId"].ToString()) : 0;
                    patientadherencebarrier.PAM_Id = string.IsNullOrEmpty(dt.Rows[0]["PAM_Id"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["PAM_Id"].ToString()) : 0;
                    patientadherencebarrier.Ptn_Pk = string.IsNullOrEmpty(dt.Rows[0]["Ptn_pk"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["Ptn_pk"].ToString()) : 0;
                    patientadherencebarrier.Visit_Id = string.IsNullOrEmpty(dt.Rows[0]["Visit_Id"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["Visit_Id"].ToString()) : 0;
                    patientadherencebarrier.LocationId = string.IsNullOrEmpty(dt.Rows[0]["Location_Id"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["Location_Id"].ToString()) : 0;

                    patientadherencebarrier.AcceptedHIVStatus = string.IsNullOrEmpty(dt.Rows[0]["AcceptedHIVStatus"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["AcceptedHIVStatus"].ToString()) : 0;
                    patientadherencebarrier.IsAgeAppropriate = string.IsNullOrEmpty(dt.Rows[0]["IsAgeAppropriate"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["IsAgeAppropriate"].ToString()) : 0;
                    patientadherencebarrier.RiskOfTransmission = string.IsNullOrEmpty(dt.Rows[0]["RiskOfTransmission"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["RiskOfTransmission"].ToString()) : 0;
                    patientadherencebarrier.UnderstandART = string.IsNullOrEmpty(dt.Rows[0]["UnderstandART"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["UnderstandART"].ToString()) : 0;
                    patientadherencebarrier.ARTSideEffect = string.IsNullOrEmpty(dt.Rows[0]["ARTSideEffect"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["ARTSideEffect"].ToString()) : 0;
                    patientadherencebarrier.AdherenceBenefits = string.IsNullOrEmpty(dt.Rows[0]["AdherenceBenefits"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["AdherenceBenefits"].ToString()) : 0;
                    patientadherencebarrier.ConsequencesNonAdherence = string.IsNullOrEmpty(dt.Rows[0]["ConsequencesNonAdherence"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["ConsequencesNonAdherence"].ToString()) : 0;
                    patientadherencebarrier.TypicalDay = dt.Rows[0]["TypicalDay"].ToString();
                    patientadherencebarrier.MedicinesIntake = dt.Rows[0]["MedicinesIntake"].ToString();
                    patientadherencebarrier.TravelVisitDescription = dt.Rows[0]["TravelVisitDescription"].ToString();
                    patientadherencebarrier.PrimartCareGiver = dt.Rows[0]["PrimartCareGiver"].ToString();
                    patientadherencebarrier.LiveWith = dt.Rows[0]["LiveWith"].ToString();
                    patientadherencebarrier.HIVStatusArtDiscussion = dt.Rows[0]["HIVStatusArtDiscussion"].ToString();
                    patientadherencebarrier.SupportSystem = string.IsNullOrEmpty(dt.Rows[0]["SupportSystem"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["SupportSystem"].ToString()) : 0;
                    patientadherencebarrier.SupportSystemDetails = dt.Rows[0]["SupportSystemDetails"].ToString();
                    patientadherencebarrier.ChangesInRelationship = dt.Rows[0]["ChangesInRelationship"].ToString();
                    patientadherencebarrier.HIVStatusBotherDetails = dt.Rows[0]["HIVStatusBotherDetails"].ToString();
                    patientadherencebarrier.PeopleTreatmentDetails = dt.Rows[0]["PeopleTreatmentDetails"].ToString();
                    patientadherencebarrier.StigmaInterfering = dt.Rows[0]["StigmaInterfering"].ToString();
                    patientadherencebarrier.ReligiousBeliefs = dt.Rows[0]["ReligiousBeliefs"].ToString();
                    patientadherencebarrier.ReferredOtherServices = string.IsNullOrEmpty(dt.Rows[0]["ReferredOtherServices"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["ReferredOtherServices"].ToString()) : 0;
                    patientadherencebarrier.AppointmentsAttend = string.IsNullOrEmpty(dt.Rows[0]["AppointmentsAttend"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["AppointmentsAttend"].ToString()) : 0;
                    patientadherencebarrier.ExperienceAppointReorganized = dt.Rows[0]["ExperienceAppointReorganized"].ToString();
                    patientadherencebarrier.Total = string.IsNullOrEmpty(dt.Rows[0]["Total"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["Total"].ToString()) : 0;
                    patientadherencebarrier.DepressionSeverity = dt.Rows[0]["DepressionSeverity"].ToString();
                    patientadherencebarrier.Recommended = dt.Rows[0]["Recommended"].ToString();

                    /*
                    patientadherencebarrier = (from row in dt.AsEnumerable()
                                               select new PatientAdherenceBarrier()
                        {
                            PAM_Id = row.Field<int>("PAM_Id"),
                            Ptn_Pk = row.Field<int>("Ptn_pk"),
                            Visit_Id = row.Field<int>("Visit_Id"),
                            LocationId = row.Field<int>("Location_Id"),

                            AcceptedHIVStatus = row.Field<int>("AcceptedHIVStatus"),
                            IsAgeAppropriate = row.Field<int>("IsAgeAppropriate"),
                            RiskOfTransmission = row.Field<int>("RiskOfTransmission"),
                            UnderstandART = row.Field<int>("UnderstandART"),
                            ARTSideEffect = row.Field<int>("ARTSideEffect"),
                            AdherenceBenefits = row.Field<int>("AdherenceBenefits"),
                            ConsequencesNonAdherence = row.Field<int>("ConsequencesNonAdherence"),
                            TypicalDay = row.Field<string>("TypicalDay"),
                            MedicinesIntake = row.Field<string>("MedicinesIntake"),
                            TravelVisitDescription = row.Field<string>("TravelVisitDescription"),
                            PrimartCareGiver = row.Field<string>("PrimartCareGiver"),
                            LiveWith = row.Field<string>("LiveWith"),
                            HIVStatusArtDiscussion = row.Field<string>("HIVStatusArtDiscussion"),
                            SupportSystem = row.Field<int?>("SupportSystem"),
                            SupportSystemDetails = row.Field<string>("SupportSystemDetails"),
                            ChangesInRelationship = row.Field<string>("ChangesInRelationship"),
                            HIVStatusBotherDetails = row.Field<string>("HIVStatusBotherDetails"),
                            PeopleTreatmentDetails = row.Field<string>("PeopleTreatmentDetails"),
                            StigmaInterfering = row.Field<string>("StigmaInterfering"),
                            ReligiousBeliefs = row.Field<string>("ReligiousBeliefs"),
                            ReferredOtherServices = row.Field<int?>("ReferredOtherServices"),
                            AppointmentsAttend = row.Field<int?>("AppointmentsAttend"),
                            ExperienceAppointReorganized = row.Field<string>("ExperienceAppointReorganized"),
                            Total = row.Field<int>("Total"),
                            DepressionSeverity = row.Field<string>("DepressionSeverity"),
                            Recommended = row.Field<string>("Recommended")
                        }).FirstOrDefault();
                     * 
                     */
                }
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BPatientAdherence.GetPatientAdherenceBarriers() Method:" + ex.ToString());
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return patientadherencebarrier;
        }

        public bool SaveUpdatePatientAdherenceBarriers(PatientAdherenceBarrier obj, int userId, out int visitId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BPatientAdherence.SaveUpdatePatientAdherenceBarriers() method called");
            bool flag = true;
            visitId = 0;
            ClsObject clsObjTP = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObjTP.Connection = this.Connection;
                clsObjTP.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@Location_Id", SqlDbType.Int, obj.LocationId.ToString());
                ClsUtility.AddParameters("@AcceptedHIVStatus", SqlDbType.Int, obj.AcceptedHIVStatus.ToString());
                ClsUtility.AddParameters("@IsAgeAppropriate", SqlDbType.Int, obj.IsAgeAppropriate.ToString());
                ClsUtility.AddParameters("@RiskOfTransmission", SqlDbType.Int, obj.RiskOfTransmission.ToString());
                ClsUtility.AddParameters("@UnderstandART", SqlDbType.Int, obj.UnderstandART.ToString());
                ClsUtility.AddParameters("@ARTSideEffect", SqlDbType.Int, obj.ARTSideEffect.ToString());
                ClsUtility.AddParameters("@AdherenceBenefits", SqlDbType.Int, obj.AdherenceBenefits.ToString());
                ClsUtility.AddParameters("@ConsequencesNonAdherence", SqlDbType.Int, obj.ConsequencesNonAdherence.ToString());
                ClsUtility.AddParameters("@TypicalDay", SqlDbType.VarChar, obj.TypicalDay.ToString());
                ClsUtility.AddParameters("@MedicinesIntake", SqlDbType.VarChar, obj.MedicinesIntake.ToString());
                ClsUtility.AddParameters("@TravelVisitDescription", SqlDbType.VarChar, obj.TravelVisitDescription.ToString());
                ClsUtility.AddParameters("@PrimartCareGiver", SqlDbType.VarChar, obj.PrimartCareGiver.ToString());
                ClsUtility.AddParameters("@LiveWith", SqlDbType.VarChar, obj.LiveWith.ToString());
                ClsUtility.AddParameters("@HIVStatusArtDiscussion", SqlDbType.VarChar, obj.HIVStatusArtDiscussion.ToString());
                ClsUtility.AddParameters("@SupportSystem", SqlDbType.Int, obj.SupportSystem.ToString());
                ClsUtility.AddParameters("@SupportSystemDetails", SqlDbType.VarChar, obj.SupportSystemDetails.ToString());
                ClsUtility.AddParameters("@ChangesInRelationship", SqlDbType.VarChar, obj.ChangesInRelationship.ToString());
                ClsUtility.AddParameters("@HIVStatusBotherDetails", SqlDbType.VarChar, obj.HIVStatusBotherDetails.ToString());
                ClsUtility.AddParameters("@PeopleTreatmentDetails", SqlDbType.VarChar, obj.PeopleTreatmentDetails.ToString());
                ClsUtility.AddParameters("@StigmaInterfering", SqlDbType.VarChar, obj.StigmaInterfering.ToString());
                ClsUtility.AddParameters("@ReligiousBeliefs", SqlDbType.VarChar, obj.ReligiousBeliefs.ToString());
                ClsUtility.AddParameters("@ReferredOtherServices", SqlDbType.Int, obj.ReferredOtherServices.ToString());
                ClsUtility.AddParameters("@AppointmentsAttend", SqlDbType.Int, obj.AppointmentsAttend.ToString());
                ClsUtility.AddParameters("@ExperienceAppointReorganized", SqlDbType.VarChar, obj.ExperienceAppointReorganized.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());

                DataTable ReturnDT = new DataTable();

                ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_Adherence_UpdatePatientAdherenceBarriers", ClsDBUtility.ObjectEnum.DataTable);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                visitId = (int)ReturnDT.Rows[0]["VisitId"];
                if (visitId > 0)
                    flag = true;
                else
                    flag = false;
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BPatientAdherence.SaveUpdatePatientAdherenceBarriers() Method:" + ex.ToString());
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return flag;
        }

        #endregion
    }




}
