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
    public class BKNHPeadraticFollowup: ProcessBase,IKNHPeadraticFollowup
    {
        public DataSet SaveUpdateKNHPeadraticFollowupData(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature)
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
                ClsUtility.AddParameters("@visitDate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                ClsUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, hashTable["ChildAccompaniedByCaregiver"].ToString());
                ClsUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, hashTable["TreatmentSupporterRelationship"].ToString());
                ClsUtility.AddParameters("@AddressChanged", SqlDbType.Int, hashTable["AddressChanged"].ToString());
                ClsUtility.AddParameters("@AddressChange", SqlDbType.VarChar, hashTable["AddressChange"].ToString());
                ClsUtility.AddParameters("@PhoneNoChange", SqlDbType.VarChar, hashTable["PhoneNoChange"].ToString());
                ClsUtility.AddParameters("@PrimaryCareGiver", SqlDbType.VarChar, hashTable["PrimaryCareGiver"].ToString());
                ClsUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, hashTable["DisclosureStatus"].ToString());
                ClsUtility.AddParameters("@ReasonNotDisclosed", SqlDbType.VarChar, hashTable["ReasonNotDisclosed"].ToString());
                ClsUtility.AddParameters("@OtherDisclosureReason", SqlDbType.VarChar, hashTable["OtherDisclosureReason"].ToString());
                ClsUtility.AddParameters("@HighestLevelAttained", SqlDbType.Int, hashTable["HighestLevelAttained"].ToString());
                ClsUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, hashTable["HIVSupportGroupMembership"].ToString());
                ClsUtility.AddParameters("@HealthEducation", SqlDbType.Int, hashTable["HealthEducation"].ToString());
                ClsUtility.AddParameters("@FatherAlive2", SqlDbType.Int, hashTable["FatherAlive2"].ToString());
                ClsUtility.AddParameters("@DateOfDeathFather", SqlDbType.DateTime, hashTable["DateOfDeathFather"].ToString());
                ClsUtility.AddParameters("@MotherAlive2", SqlDbType.Int, hashTable["MotherAlive2"].ToString());
                ClsUtility.AddParameters("@DateOfDeathMother", SqlDbType.DateTime, hashTable["DateOfDeathMother"].ToString());
                ClsUtility.AddParameters("@MedicalHistory", SqlDbType.Int, hashTable["MedicalHistory"].ToString());
                ClsUtility.AddParameters("@OtherMedicalHistorySpecify", SqlDbType.VarChar, hashTable["OtherMedicalHistorySpecify"].ToString());
                
                ClsUtility.AddParameters("@OtherChronicCondition", SqlDbType.VarChar, hashTable["OtherChronicCondition"].ToString());
                //ClsUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
                ClsUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, hashTable["SchoolPerfomance"].ToString());
                ClsUtility.AddParameters("@ImmunisationStatus", SqlDbType.Int, hashTable["ImmunisationStatus"].ToString());
                ClsUtility.AddParameters("@TBHistory", SqlDbType.Int, hashTable["TBHistory"].ToString());
                ClsUtility.AddParameters("@TBrxCompleteDate", SqlDbType.DateTime, hashTable["TBrxCompleteDate"].ToString());
                ClsUtility.AddParameters("@TBRetreatmentDate", SqlDbType.DateTime, hashTable["TBRetreatmentDate"].ToString());
                ClsUtility.AddParameters("@TissueBiopsyTest", SqlDbType.Int, hashTable["TissueBiopsyTest"].ToString());
                ClsUtility.AddParameters("@TBFindings", SqlDbType.Int, hashTable["TBFindings"].ToString());
                ClsUtility.AddParameters("@SputumSmear", SqlDbType.Int, hashTable["SputumSmear"].ToString());
                ClsUtility.AddParameters("@TissueBiopsy", SqlDbType.Int, hashTable["TissueBiopsy"].ToString());
                ClsUtility.AddParameters("@ChestXRay", SqlDbType.Int, hashTable["ChestXRay"].ToString());
                ClsUtility.AddParameters("@CXR", SqlDbType.Int, hashTable["CXR"].ToString());
                ClsUtility.AddParameters("@OtherCXR", SqlDbType.VarChar, hashTable["OtherCXR"].ToString());
                ClsUtility.AddParameters("@TissueBiopsyResults", SqlDbType.VarChar, hashTable["TissueBiopsyResults"].ToString());
                ClsUtility.AddParameters("@TBTypePeads", SqlDbType.Int, hashTable["TBTypePeads"].ToString());
                ClsUtility.AddParameters("@PeadsTBPatientType", SqlDbType.Int, hashTable["PeadsTBPatientType"].ToString());
                ClsUtility.AddParameters("@TBPlan", SqlDbType.Int, hashTable["TBPlan"].ToString());
                ClsUtility.AddParameters("@OtherTBPlan", SqlDbType.VarChar, hashTable["OtherTBPlan"].ToString());
                ClsUtility.AddParameters("@TBRegimen", SqlDbType.Int, hashTable["TBRegimen"].ToString());
                ClsUtility.AddParameters("@OtherTBRegimen", SqlDbType.VarChar, hashTable["OtherTBRegimen"].ToString());
                ClsUtility.AddParameters("@TBRegimenStartDate", SqlDbType.DateTime, hashTable["TBRegimenStartDate"].ToString());
                ClsUtility.AddParameters("@TBRegimenEndDate", SqlDbType.DateTime, hashTable["TBRegimenEndDate"].ToString());
                ClsUtility.AddParameters("@TBTreatmentOutcomesPeads", SqlDbType.Int, hashTable["TBTreatmentOutcomesPeads"].ToString());
                ClsUtility.AddParameters("@NoTB", SqlDbType.Int, hashTable["NoTB"].ToString());
                ClsUtility.AddParameters("@ReminderIPT", SqlDbType.Int, hashTable["ReminderIPT"].ToString());
                ClsUtility.AddParameters("@INHStartDate", SqlDbType.DateTime, hashTable["INHStartDate"].ToString());
                ClsUtility.AddParameters("@INHEndDate", SqlDbType.DateTime, hashTable["INHEndDate"].ToString());
                ClsUtility.AddParameters("@PyridoxineStartDate", SqlDbType.DateTime, hashTable["PyridoxineStartDate"].ToString());
                ClsUtility.AddParameters("@PyridoxineEndDate", SqlDbType.DateTime, hashTable["PyridoxineEndDate"].ToString());
                ClsUtility.AddParameters("@TBAdherenceAssessed", SqlDbType.Int, hashTable["TBAdherenceAssessed"].ToString());
                ClsUtility.AddParameters("@ReferredForAdherence", SqlDbType.Int, hashTable["ReferredForAdherence"].ToString());
                ClsUtility.AddParameters("@OtherTBsideEffects", SqlDbType.VarChar, hashTable["OtherTBsideEffects"].ToString());
                ClsUtility.AddParameters("@StopINH", SqlDbType.Int, hashTable["StopINH"].ToString());
                ClsUtility.AddParameters("@StopINHDate", SqlDbType.DateTime, hashTable["StopINHDate"].ToString());
                ClsUtility.AddParameters("@ContactsScreenedForTB", SqlDbType.Int, hashTable["ContactsScreenedForTB"].ToString());
                ClsUtility.AddParameters("@TBnotScreenedSpecify", SqlDbType.VarChar, hashTable["TBnotScreenedSpecify"].ToString());
                ClsUtility.AddParameters("@LongTermMedications", SqlDbType.Int, hashTable["LongTermMedications"].ToString());
                ClsUtility.AddParameters("@MultivitaminsDate", SqlDbType.DateTime, hashTable["MultivitaminsDate"].ToString());
                ClsUtility.AddParameters("@SulfaTMPDate", SqlDbType.DateTime, hashTable["SulfaTMPDate"].ToString());
                ClsUtility.AddParameters("@TBRxDate", SqlDbType.DateTime, hashTable["TBRxDate"].ToString());
                ClsUtility.AddParameters("@HormonalContraceptivesDate", SqlDbType.DateTime, hashTable["HormonalContraceptivesDate"].ToString());
                ClsUtility.AddParameters("@AntifungalsDate", SqlDbType.DateTime, hashTable["AntifungalsDate"].ToString());
                ClsUtility.AddParameters("@AnticonvulsantsDate", SqlDbType.DateTime, hashTable["AnticonvulsantsDate"].ToString());
                ClsUtility.AddParameters("@OtherLongTermMedications", SqlDbType.VarChar, hashTable["OtherLongTermMedications"].ToString());
                ClsUtility.AddParameters("@OtherCurrentLongTermMedications", SqlDbType.VarChar, hashTable["OtherCurrentLongTermMedications"].ToString());
                //ClsUtility.AddParameters("@MilestoneAppropriate", SqlDbType.Int, hashTable["MilestoneAppropriate"].ToString());
                ClsUtility.AddParameters("@ResonMilestoneInappropriate", SqlDbType.VarChar, hashTable["ResonMilestoneInappropriate"].ToString());
                ClsUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, hashTable["OtherGeneralConditions"].ToString());
                ClsUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, hashTable["OtherAbdomenConditions"].ToString());
                ClsUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, hashTable["OtherCardiovascularConditions"].ToString());
                ClsUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, hashTable["OtherOralCavityConditions"].ToString());
                ClsUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, hashTable["OtherGenitourinaryConditions"].ToString());
                ClsUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, hashTable["OtherCNSConditions"].ToString());
                ClsUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, hashTable["OtherChestLungsConditions"].ToString());
                ClsUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, hashTable["OtherSkinConditions"].ToString());
                ClsUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());
                ClsUtility.AddParameters("@ProgressionInWHOstage", SqlDbType.VarChar, hashTable["ProgressionInWHOstage"].ToString());
                ClsUtility.AddParameters("@SpecifyWHOprogression", SqlDbType.VarChar, hashTable["SpecifyWHOprogression"].ToString());
                ClsUtility.AddParameters("@CurrentlyOnHAART", SqlDbType.Int, hashTable["CurrentlyOnHAART"].ToString());
                ClsUtility.AddParameters("@CurrentARTRegimenLine", SqlDbType.Int, hashTable["CurrentARTRegimenLine"].ToString());
                ClsUtility.AddParameters("@CurrentARTRegimen", SqlDbType.Int, hashTable["CurrentARTRegimen"].ToString());
                ClsUtility.AddParameters("@OtherARTRegimen", SqlDbType.VarChar, hashTable["OtherARTRegimen"].ToString());
                ClsUtility.AddParameters("@CurrentARTRegimenDate", SqlDbType.DateTime, hashTable["CurrentARTRegimenDate"].ToString());
                ClsUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, hashTable["OIProphylaxis"].ToString());
                ClsUtility.AddParameters("@ReasonCTXpresribed", SqlDbType.Int, hashTable["ReasonCTXpresribed"].ToString());
                ClsUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, hashTable["OtherOIProphylaxis"].ToString());
                ClsUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, hashTable["OtherTreatment"].ToString());
                ClsUtility.AddParameters("@MissedDosesFUP", SqlDbType.Int, hashTable["MissedDosesFUP"].ToString());
                ClsUtility.AddParameters("@MissedDosesFUPspecify", SqlDbType.VarChar, hashTable["MissedDosesFUPspecify"].ToString());
                ClsUtility.AddParameters("@DelaysInTakingMedication", SqlDbType.Int, hashTable["DelaysInTakingMedication"].ToString());
                ClsUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, hashTable["ARVSideEffects"].ToString());
                ClsUtility.AddParameters("@Specifyothershorttermeffects", SqlDbType.VarChar, hashTable["Specifyothershorttermeffects"].ToString());
                ClsUtility.AddParameters("@listlongtermeffect", SqlDbType.VarChar, hashTable["listlongtermeffect"].ToString());
                ClsUtility.AddParameters("@HAARTImpression", SqlDbType.Int, hashTable["HAARTImpression"].ToString());
                ClsUtility.AddParameters("@HAARTexperienced", SqlDbType.Int, hashTable["HAARTexperienced"].ToString());
                ClsUtility.AddParameters("@OtherHAARTImpression", SqlDbType.VarChar, hashTable["OtherHAARTImpression"].ToString());
                ClsUtility.AddParameters("@ReviewedPreviousResults", SqlDbType.Int, hashTable["ReviewedPreviousResults"].ToString());
                ClsUtility.AddParameters("@ResultsReviewComments", SqlDbType.VarChar, hashTable["ResultsReviewComments"].ToString());
                ClsUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, hashTable["HIVRelatedOI"].ToString());
                ClsUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.VarChar, hashTable["NonHIVRelatedOI"].ToString());
                ClsUtility.AddParameters("@LabEvaluationPeads", SqlDbType.Int, hashTable["LabEvaluationPeads"].ToString());
                ClsUtility.AddParameters("@OtherCounselling", SqlDbType.VarChar, hashTable["OtherCounselling"].ToString());
                ClsUtility.AddParameters("@AdditionalPsychosocialAssessment", SqlDbType.VarChar, hashTable["AdditionalPsychosocialAssessment"].ToString());
                ClsUtility.AddParameters("@ARTTreatmentPlan", SqlDbType.Int, hashTable["ARTTreatmentPlan"].ToString());
                ClsUtility.AddParameters("@SubstituteRegimenDrug", SqlDbType.VarChar, hashTable["SubstituteRegimenDrug"].ToString());
                ClsUtility.AddParameters("@SpecifyotherARTchangereason", SqlDbType.VarChar, hashTable["SpecifyotherARTchangereason"].ToString());
                ClsUtility.AddParameters("@2ndLineRegimenSwitch", SqlDbType.Int, hashTable["2ndLineRegimenSwitch"].ToString());
                ClsUtility.AddParameters("@RegimenPrescribed", SqlDbType.Int, hashTable["RegimenPrescribed"].ToString());
                ClsUtility.AddParameters("@OtherRegimenPrescribed", SqlDbType.VarChar, hashTable["OtherRegimenPrescribed"].ToString());
                ClsUtility.AddParameters("@rdoSexualOrientation", SqlDbType.Int, hashTable["rdoSexualOrientation"].ToString());
                ClsUtility.AddParameters("@SexualOrientation", SqlDbType.Int, hashTable["SexualOrientation"].ToString());
                ClsUtility.AddParameters("@KnowSexualPartnerHIVStatus", SqlDbType.Int, hashTable["KnowSexualPartnerHIVStatus"].ToString());
                ClsUtility.AddParameters("@PartnerHIVStatus", SqlDbType.Int, hashTable["PartnerHIVStatus"].ToString());
                ClsUtility.AddParameters("@LMPassessed", SqlDbType.Int, hashTable["LMPassessed"].ToString());
                ClsUtility.AddParameters("@LMPDate", SqlDbType.DateTime, hashTable["LMPDate"].ToString());
                ClsUtility.AddParameters("@LMPNotaccessedReason", SqlDbType.Int, hashTable["LMPNotaccessedReason"].ToString());
                ClsUtility.AddParameters("@pregnant", SqlDbType.Int, hashTable["pregnant"].ToString());
                ClsUtility.AddParameters("@EDD", SqlDbType.DateTime, hashTable["EDD"].ToString());
                ClsUtility.AddParameters("@GivenPWPMessages", SqlDbType.Int, hashTable["GivenPWPMessages"].ToString());
                ClsUtility.AddParameters("@UnsafeSexImportanceExplained", SqlDbType.Int, hashTable["UnsafeSexImportanceExplained"].ToString());
                ClsUtility.AddParameters("@CondomsIssued", SqlDbType.Int, hashTable["CondomsIssued"].ToString());
                ClsUtility.AddParameters("@ReasonfornotIssuingCondoms", SqlDbType.VarChar, hashTable["ReasonfornotIssuingCondoms"].ToString());
                ClsUtility.AddParameters("@STIscreenedPeads", SqlDbType.Int, hashTable["STIscreenedPeads"].ToString());
                ClsUtility.AddParameters("@UrethralDischarge", SqlDbType.Int, hashTable["UrethralDischarge"].ToString());
                ClsUtility.AddParameters("@VaginalDischarge", SqlDbType.Int, hashTable["VaginalDischarge"].ToString());
                ClsUtility.AddParameters("@GenitalUlceration", SqlDbType.Int, hashTable["GenitalUlceration"].ToString());
                ClsUtility.AddParameters("@STItreatmentPlan", SqlDbType.VarChar, hashTable["STItreatmentPlan"].ToString());
                ClsUtility.AddParameters("@CervicalCancerScreened", SqlDbType.Int, hashTable["CervicalCancerScreened"].ToString());
                ClsUtility.AddParameters("@CervicalCancerScreeningResults", SqlDbType.Int, hashTable["CervicalCancerScreeningResults"].ToString());
                ClsUtility.AddParameters("@ReferredForCervicalCancerScreening", SqlDbType.Int, hashTable["ReferredForCervicalCancerScreening"].ToString());
                ClsUtility.AddParameters("@HPVOffered", SqlDbType.Int, hashTable["HPVOffered"].ToString());
                ClsUtility.AddParameters("@OfferedHPVaccine", SqlDbType.Int, hashTable["OfferedHPVaccine"].ToString());
                ClsUtility.AddParameters("@HPVDoseDate", SqlDbType.DateTime, hashTable["HPVDoseDate"].ToString());
                ClsUtility.AddParameters("@OtherPwPInteventions", SqlDbType.VarChar, hashTable["OtherPwPInteventions"].ToString());
                ClsUtility.AddParameters("@WardAdmission", SqlDbType.Int, hashTable["WardAdmission"].ToString());
                ClsUtility.AddParameters("@ReferredTo", SqlDbType.Int, hashTable["ReferredTo"].ToString());
                ClsUtility.AddParameters("@SpecifyOtherReferredTo", SqlDbType.Int, hashTable["SpecifyOtherReferredTo"].ToString());
                ClsUtility.AddParameters("@ScheduledAppointment", SqlDbType.VarChar, hashTable["ScheduledAppointment"].ToString());
                ClsUtility.AddParameters("@Otherappointmentreason", SqlDbType.VarChar, hashTable["Otherappointmentreason"].ToString());


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

                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PaediatricFollowup_FORM", ClsDBUtility.ObjectEnum.DataSet);
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
        public DataSet SaveUpdateKNHPeadraticFollowupData_TriageTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
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
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));
                ClsUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, hashTable["ChildAccompaniedByCaregiver"].ToString());
                ClsUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, hashTable["TreatmentSupporterRelationship"].ToString());               
                ClsUtility.AddParameters("@PrimaryCareGiver", SqlDbType.VarChar, hashTable["PrimaryCareGiver"].ToString());
                ClsUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, hashTable["DisclosureStatus"].ToString());
                ClsUtility.AddParameters("@ReasonNotDisclosed", SqlDbType.VarChar, hashTable["ReasonNotDisclosed"].ToString());
                ClsUtility.AddParameters("@OtherDisclosureReason", SqlDbType.VarChar, hashTable["OtherDisclosureReason"].ToString());
                ClsUtility.AddParameters("@FatherAlive2", SqlDbType.Int, hashTable["FatherAlive2"].ToString());
                ClsUtility.AddParameters("@DateOfDeathFather", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["DateOfDeathFather"].ToString()));
                ClsUtility.AddParameters("@MotherAlive2", SqlDbType.Int, hashTable["MotherAlive2"].ToString());
                ClsUtility.AddParameters("@DateOfDeathMother", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["DateOfDeathMother"].ToString()));
                ClsUtility.AddParameters("@HealthEducation", SqlDbType.Int, hashTable["HealthEducation"].ToString());
                ClsUtility.AddParameters("@SchoolingStatus", SqlDbType.Int, hashTable["SchoolingStatus"].ToString());
                ClsUtility.AddParameters("@HighestLevelAttained", SqlDbType.Int, hashTable["HighestLevelAttained"].ToString());
                ClsUtility.AddParameters("@HIVSupportGroup", SqlDbType.Int, hashTable["HIVSupportGroup"].ToString());
                ClsUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, hashTable["HIVSupportGroupMembership"].ToString());
                ClsUtility.AddParameters("@AddressChanged", SqlDbType.Int, hashTable["AddressChanged"].ToString());
                ClsUtility.AddParameters("@AddressChange", SqlDbType.VarChar, hashTable["AddressChange"].ToString());
                ClsUtility.AddParameters("@PhoneNoChange", SqlDbType.VarChar, hashTable["PhoneNoChange"].ToString());
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
                ClsUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PaediatricFollow_FORM_TriageTab", ClsDBUtility.ObjectEnum.DataSet);
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
        public DataSet SaveUpdateKNHPeadraticFollowupData_CATab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
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
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));
                ClsUtility.AddParameters("@MedicalHistory", SqlDbType.Int, hashTable["MedicalHistory"].ToString());
                ClsUtility.AddParameters("@OtherChronicCondition", SqlDbType.VarChar, hashTable["OtherChronicCondition"].ToString());
                ClsUtility.AddParameters("@OtherMedicalHistorySpecify", SqlDbType.VarChar, hashTable["OtherMedicalHistorySpecify"].ToString());
                ClsUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
                ClsUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, hashTable["SchoolPerfomance"].ToString());
                ClsUtility.AddParameters("@ImmunisationStatus", SqlDbType.Int, hashTable["ImmunisationStatus"].ToString());
                ClsUtility.AddParameters("@TBHistory", SqlDbType.Int, hashTable["TBHistory"].ToString());
                ClsUtility.AddParameters("@TBrxCompleteDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["TBrxCompleteDate"].ToString()));
                ClsUtility.AddParameters("@TBRetreatmentDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["TBRetreatmentDate"].ToString()));
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                ClsUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PaediatricFollowup_FORM_CATab", ClsDBUtility.ObjectEnum.DataSet);
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
        public DataSet SaveUpdateKNHPeadraticFollowupData_ExamTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
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
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));
                ClsUtility.AddParameters("@OtherCurrentLongTermMedications", SqlDbType.VarChar, hashTable["OtherCurrentLongTermMedications"].ToString());
                ClsUtility.AddParameters("@MilestoneAppropriate", SqlDbType.Int, hashTable["MilestoneAppropriate"].ToString());
                ClsUtility.AddParameters("@ResonMilestoneInappropriate", SqlDbType.VarChar, hashTable["ResonMilestoneInappropriate"].ToString());
                ClsUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, hashTable["OtherGeneralConditions"].ToString());
                ClsUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, hashTable["OtherAbdomenConditions"].ToString());
                ClsUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, hashTable["OtherCardiovascularConditions"].ToString());
                ClsUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, hashTable["OtherOralCavityConditions"].ToString());
                ClsUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, hashTable["OtherGenitourinaryConditions"].ToString());
                ClsUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, hashTable["OtherCNSConditions"].ToString());
                ClsUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, hashTable["OtherChestLungsConditions"].ToString());
                ClsUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, hashTable["OtherSkinConditions"].ToString());
                ClsUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());
                ClsUtility.AddParameters("@ProgressionInWHOstage", SqlDbType.Int, hashTable["ProgressionInWHOstage"].ToString());
                ClsUtility.AddParameters("@SpecifyWHOprogression", SqlDbType.VarChar, hashTable["SpecifyWHOprogression"].ToString());
                ClsUtility.AddParameters("@WABStage", SqlDbType.Int, hashTable["WABStage"].ToString());
                ClsUtility.AddParameters("@CurrentWHOStage", SqlDbType.Int, hashTable["CurrentWHOStage"].ToString());
                ClsUtility.AddParameters("@Menarche", SqlDbType.Int, hashTable["Menarche"].ToString());
                ClsUtility.AddParameters("@MenarcheDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["MenarcheDate"].ToString()));
                ClsUtility.AddParameters("@TannerStaging", SqlDbType.Int, hashTable["TannerStaging"].ToString());
                ClsUtility.AddParameters("@Impression", SqlDbType.Int, hashTable["Impression"].ToString());
                ClsUtility.AddParameters("@OtherImpression", SqlDbType.VarChar, hashTable["OtherImpression"].ToString());
                ClsUtility.AddParameters("@reviewprevresult", SqlDbType.Int, hashTable["reviewprevresult"].ToString());
                ClsUtility.AddParameters("@additonalinformation", SqlDbType.Int, hashTable["additonalinformation"].ToString());
                ClsUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, hashTable["HIVRelatedOI"].ToString());
                ClsUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.Int, hashTable["NonHIVRelatedOI"].ToString());
                
                
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                ClsUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PaediatricFollowup_FORM_ExamTab", ClsDBUtility.ObjectEnum.DataSet);
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
        public DataSet SaveUpdateKNHPeadraticFollowupData_MgtTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId)
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
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));
                ClsUtility.AddParameters("@MissedDosesFUP", SqlDbType.Int, hashTable["MissedDosesFUP"].ToString());
                ClsUtility.AddParameters("@MissedDosesFUPspecify", SqlDbType.VarChar, hashTable["MissedDosesFUPspecify"].ToString());
                ClsUtility.AddParameters("@DelaysInTakingMedication", SqlDbType.Int, hashTable["DelaysInTakingMedication"].ToString());
                ClsUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, hashTable["Specifyothershorttermeffects"].ToString());
                ClsUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, hashTable["OtherLongtermEffects"].ToString());
                ClsUtility.AddParameters("@SpecifyLabEvaluation", SqlDbType.VarChar, hashTable["SpecifyLabEvaluation"].ToString());
                ClsUtility.AddParameters("@ARTTreatmentPlan", SqlDbType.Int, hashTable["ARTTreatmentPlan"].ToString());
                ClsUtility.AddParameters("@OtherARTStopCode", SqlDbType.VarChar, hashTable["OtherARTStopCode"].ToString());
                ClsUtility.AddParameters("@OtherEligiblethorugh", SqlDbType.VarChar, hashTable["OtherEligiblethorugh"].ToString());
                ClsUtility.AddParameters("@NumberDrugsSubstituted", SqlDbType.Int, hashTable["NumberDrugsSubstituted"].ToString());
                ClsUtility.AddParameters("@SpecifyotherARTchangereason", SqlDbType.VarChar, hashTable["SpecifyotherARTchangereason"].ToString());
                ClsUtility.AddParameters("@2ndLineRegimenSwitch", SqlDbType.Int, hashTable["2ndLineRegimenSwitch"].ToString());
                ClsUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, hashTable["OIProphylaxis"].ToString());
                ClsUtility.AddParameters("@ReasonCTXpresribed", SqlDbType.Int, hashTable["ReasonCTXpresribed"].ToString());
                ClsUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, hashTable["OtherOIProphylaxis"].ToString());
                ClsUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, hashTable["OtherTreatment"].ToString());                
                ClsUtility.AddParameters("@Fluconazole", SqlDbType.Int, hashTable["Fluconazole"].ToString());
                ClsUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());                
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, DataQuality.ToString());
                ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PaediatricFollowup_FORM_MgtTab", ClsDBUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                ClsUtility.AddParameters("@TherapyPlan", SqlDbType.Int, hashTable["ARTTreatmentPlan"].ToString());
                ClsUtility.AddParameters("@Noofdrugssubstituted", SqlDbType.Int, hashTable["NumberDrugsSubstituted"].ToString());
                ClsUtility.AddParameters("@reasonforswitchto2ndlineregimen", SqlDbType.Int, hashTable["2ndLineRegimenSwitch"].ToString());
                ClsUtility.AddParameters("@specifyOtherEligibility", SqlDbType.Int, hashTable["OtherEligiblethorugh"].ToString());
                ClsUtility.AddParameters("@specifyotherARTchangereason", SqlDbType.Int, hashTable["SpecifyotherARTchangereason"].ToString());
                ClsUtility.AddParameters("@specifyOtherStopCode", SqlDbType.Int, hashTable["OtherARTStopCode"].ToString());

                DataSet theARTDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_ARVTherapy", ClsDBUtility.ObjectEnum.DataSet);

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
        public DataSet GetKNHPeadtricFollowupDetails(int ptn_pk, int visitpk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_PaediatricFollowup", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetKNHPeadtricFollowupAutoPopulating(int ptn_pk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_KNH_Paed_followup_Form_Autopopulating_data", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
    }
}
