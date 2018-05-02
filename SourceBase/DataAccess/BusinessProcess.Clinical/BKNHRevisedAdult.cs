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
    public class BKNHRevisedAdult : ProcessBase, IKNHRevisedAdult
    {
         public DataTable GetDropdownFieldDetails(int featureId)
         {
             lock (this)
             {
                 ClsObject PatientHistory = new ClsObject();
                 ClsUtility.Init_Hashtable();
                 ClsUtility.AddParameters("@featureId", SqlDbType.Int, featureId.ToString());
                 return (DataTable)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_KNH_Getdropdownfieldlist", ClsDBUtility.ObjectEnum.DataTable);
             }
         }
         public DataSet SaveUpdateKNHRevisedAdultFollowupData(Hashtable hashTable, DataTable dtMultiSelectValues,int signature,int UserId)
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
                 ClsUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                 ClsUtility.AddParameters("@visitdate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                ClsUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, hashTable["ChildAccompaniedByCaregiver"].ToString());
                ClsUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, hashTable["TreatmentSupporterRelationship"].ToString());
                ClsUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, hashTable["DisclosureStatus"].ToString());
                ClsUtility.AddParameters("@HealthEducation", SqlDbType.Int, hashTable["HealthEducation"].ToString());
                ClsUtility.AddParameters("@ReasonNotDisclosed", SqlDbType.VarChar, hashTable["ReasonNotDisclosed"].ToString());
                ClsUtility.AddParameters("@OtherDisclosureReason", SqlDbType.VarChar, hashTable["OtherDisclosureReason"].ToString());
                ClsUtility.AddParameters("@SchoolingStatus", SqlDbType.Int, hashTable["SchoolingStatus"].ToString());
                ClsUtility.AddParameters("@HighestLevelAttained", SqlDbType.Int, hashTable["HighestLevelAttained"].ToString());
                ClsUtility.AddParameters("@HIVSupportGroup", SqlDbType.Int, hashTable["HIVSupportGroup"].ToString());
                ClsUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, hashTable["HIVSupportGroupMembership"].ToString());
                ClsUtility.AddParameters("@AddressChanged", SqlDbType.Int, hashTable["AddressChanged"].ToString());
                ClsUtility.AddParameters("@AddressChange", SqlDbType.VarChar, hashTable["AddressChange"].ToString());
                ClsUtility.AddParameters("@PhoneNoChange", SqlDbType.VarChar, hashTable["PhoneNoChange"].ToString());
                ClsUtility.AddParameters("@NursesComments", SqlDbType.VarChar, hashTable["NursesComments"].ToString());
                ClsUtility.AddParameters("@CurrentlyOnHAART", SqlDbType.Int, hashTable["CurrentlyOnHAART"].ToString());
                ClsUtility.AddParameters("@CurrentARTRegimenLine", SqlDbType.Int, hashTable["CurrentARTRegimenLine"].ToString());
                ClsUtility.AddParameters("@CurrentARTRegimen", SqlDbType.Int, hashTable["CurrentARTRegimen"].ToString());
                ClsUtility.AddParameters("@OtherARTRegimen", SqlDbType.VarChar, hashTable["OtherARTRegimen"].ToString());
                ClsUtility.AddParameters("@CurrentARTRegimenDate", SqlDbType.DateTime, hashTable["CurrentARTRegimenDate"].ToString());
                ClsUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, hashTable["SchoolPerfomance"].ToString());
                ClsUtility.AddParameters("@OtherPresentingComplaints", SqlDbType.VarChar, hashTable["OtherPresentingComplaints"].ToString());
                ClsUtility.AddParameters("@MedicalCondition", SqlDbType.Int, hashTable["MedicalCondition"].ToString());
                ClsUtility.AddParameters("@CurrentSurgicalCondition", SqlDbType.VarChar, hashTable["CurrentSurgicalCondition"].ToString());
                ClsUtility.AddParameters("@PreviousSurgicalCondition", SqlDbType.VarChar, hashTable["PreviousSurgicalCondition"].ToString());
                ClsUtility.AddParameters("@PreExistingMedicalConditionsFUP", SqlDbType.Int, hashTable["PreExistingMedicalConditionsFUP"].ToString());
                ClsUtility.AddParameters("@Antihypertensives", SqlDbType.VarChar, hashTable["Antihypertensives"].ToString());
                ClsUtility.AddParameters("@Anticonvulsants", SqlDbType.VarChar, hashTable["Anticonvulsants"].ToString());
                ClsUtility.AddParameters("@Hypoglycemics", SqlDbType.VarChar, hashTable["Hypoglycemics"].ToString());
                ClsUtility.AddParameters("@RadiotherapyChemotherapy", SqlDbType.VarChar, hashTable["RadiotherapyChemotherapy"].ToString());
                ClsUtility.AddParameters("@PreviousAdmission", SqlDbType.Int, hashTable["PreviousAdmission"].ToString());
                ClsUtility.AddParameters("@PreviousAdmissionDiagnosis", SqlDbType.VarChar, hashTable["PreviousAdmissionDiagnosis"].ToString());
                ClsUtility.AddParameters("@PreviousAdmissionStart", SqlDbType.DateTime, hashTable["PreviousAdmissionStart"].ToString());
                ClsUtility.AddParameters("@PreviousAdmissionEnd", SqlDbType.DateTime, hashTable["PreviousAdmissionEnd"].ToString());
                ClsUtility.AddParameters("@HIVAssociatedConditionsPeads", SqlDbType.Int, hashTable["HIVAssociatedConditionsPeads"].ToString());
                ClsUtility.AddParameters("@TBFindings", SqlDbType.Int, hashTable["TBFindings"].ToString());
                ClsUtility.AddParameters("@TBresultsAvailable", SqlDbType.Int, hashTable["TBresultsAvailable"].ToString());
                ClsUtility.AddParameters("@SputumSmear", SqlDbType.Int, hashTable["SputumSmear"].ToString());
                ClsUtility.AddParameters("@SputumSmearDate", SqlDbType.DateTime, hashTable["SputumSmearDate"].ToString());
                ClsUtility.AddParameters("@TissueBiopsy", SqlDbType.Int, hashTable["TissueBiopsy"].ToString());
                ClsUtility.AddParameters("@TissueBiopsyDate", SqlDbType.DateTime, hashTable["TissueBiopsyDate"].ToString());
                ClsUtility.AddParameters("@ChestXRay", SqlDbType.Int, hashTable["ChestXRay"].ToString());
                ClsUtility.AddParameters("@ChestXRayDate", SqlDbType.DateTime, hashTable["ChestXRayDate"].ToString());
                ClsUtility.AddParameters("@CXR", SqlDbType.Int, hashTable["CXR"].ToString());
                ClsUtility.AddParameters("@OtherCXR", SqlDbType.VarChar, hashTable["OtherCXR"].ToString());
                ClsUtility.AddParameters("@TBTypePeads", SqlDbType.Int, hashTable["TBTypePeads"].ToString());
                ClsUtility.AddParameters("@PeadsTBPatientType", SqlDbType.Int, hashTable["PeadsTBPatientType"].ToString());
                ClsUtility.AddParameters("@TBPlan", SqlDbType.Int, hashTable["TBPlan"].ToString());
                ClsUtility.AddParameters("@OtherTBPlan", SqlDbType.VarChar, hashTable["OtherTBPlan"].ToString());
                ClsUtility.AddParameters("@TBRegimen", SqlDbType.Int, hashTable["TBRegimen"].ToString());
                ClsUtility.AddParameters("@OtherTBRegimen", SqlDbType.VarChar, hashTable["OtherTBRegimen"].ToString());
                ClsUtility.AddParameters("@TBRegimenStartDate", SqlDbType.DateTime, hashTable["TBRegimenStartDate"].ToString());
                ClsUtility.AddParameters("@TBRegimenEndDate", SqlDbType.DateTime, hashTable["TBRegimenEndDate"].ToString());
                ClsUtility.AddParameters("@TBTreatmentOutcomesPeads", SqlDbType.Int, hashTable["TBTreatmentOutcomesPeads"].ToString());
                ClsUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, hashTable["ARVSideEffects"].ToString());
                ClsUtility.AddParameters("@Specifyothershorttermeffects", SqlDbType.VarChar, hashTable["Specifyothershorttermeffects"].ToString());
                ClsUtility.AddParameters("@listlongtermeffect", SqlDbType.Int, hashTable["listlongtermeffect"].ToString());
                ClsUtility.AddParameters("@ReviewedPreviousResults", SqlDbType.Int, hashTable["ReviewedPreviousResults"].ToString());
                ClsUtility.AddParameters("@ResultsReviewComments", SqlDbType.VarChar, hashTable["ResultsReviewComments"].ToString());
                ClsUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, hashTable["HIVRelatedOI"].ToString());
                ClsUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.VarChar, hashTable["NonHIVRelatedOI"].ToString());
                if (hashTable["ProgressionInWHOstage"] != null)
                {
                    ClsUtility.AddParameters("@ProgressionInWHOstage", SqlDbType.Int, hashTable["ProgressionInWHOstage"].ToString());
                }
                ClsUtility.AddParameters("@SpecifyWHOprogression", SqlDbType.VarChar, hashTable["SpecifyWHOprogression"].ToString());
                ClsUtility.AddParameters("@MissedDosesFUP", SqlDbType.Int, hashTable["MissedDosesFUP"].ToString());
                ClsUtility.AddParameters("@MissedDosesFUPspecify", SqlDbType.VarChar, hashTable["MissedDosesFUPspecify"].ToString());
                ClsUtility.AddParameters("@DelaysInTakingMedication", SqlDbType.Int, hashTable["DelaysInTakingMedication"].ToString());
                ClsUtility.AddParameters("@SpecifyARVallergy", SqlDbType.VarChar, hashTable["SpecifyARVallergy"].ToString());
                ClsUtility.AddParameters("@SpecifyAntibioticAllery", SqlDbType.VarChar, hashTable["SpecifyAntibioticAllery"].ToString());
                ClsUtility.AddParameters("@OtherDrugAllergy", SqlDbType.VarChar, hashTable["OtherDrugAllergy"].ToString());
                ClsUtility.AddParameters("@WorkUpPlan", SqlDbType.VarChar, hashTable["WorkUpPlan"].ToString());
                ClsUtility.AddParameters("@OtherCounselling", SqlDbType.VarChar, hashTable["OtherCounselling"].ToString());
                ClsUtility.AddParameters("@SubstituteRegimenDrug", SqlDbType.VarChar, hashTable["SubstituteRegimenDrug"].ToString());
                ClsUtility.AddParameters("@ARTTreatmentPlan", SqlDbType.Int, hashTable["ARTTreatmentPlan"].ToString());
                ClsUtility.AddParameters("@SpecifyotherARTchangereason", SqlDbType.VarChar, hashTable["SpecifyotherARTchangereason"].ToString());
                ClsUtility.AddParameters("@2ndLineRegimenSwitch", SqlDbType.Int, hashTable["2ndLineRegimenSwitch"].ToString());
                ClsUtility.AddParameters("@RegimenPrescribed", SqlDbType.Int, hashTable["RegimenPrescribed"].ToString());
                ClsUtility.AddParameters("@OtherRegimenPrescribed", SqlDbType.VarChar, hashTable["OtherRegimenPrescribed"].ToString());
                ClsUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, hashTable["OIProphylaxis"].ToString());
                ClsUtility.AddParameters("@ReasonCTXpresribed", SqlDbType.Int, hashTable["ReasonCTXpresribed"].ToString());
                ClsUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, hashTable["OtherOIProphylaxis"].ToString());
                ClsUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, hashTable["OtherTreatment"].ToString());
                ClsUtility.AddParameters("@SexuallyActive", SqlDbType.Int, hashTable["SexuallyActive"].ToString());
                ClsUtility.AddParameters("@SexualOrientation", SqlDbType.Int, hashTable["SexualOrientation"].ToString());
                ClsUtility.AddParameters("@KnowSexualPartnerHIVStatus", SqlDbType.Int, hashTable["KnowSexualPartnerHIVStatus"].ToString());
                ClsUtility.AddParameters("@PartnerHIVStatus", SqlDbType.Int, hashTable["PartnerHIVStatus"].ToString());
                ClsUtility.AddParameters("@LMPassessed", SqlDbType.Int, hashTable["LMPassessed"].ToString());
                ClsUtility.AddParameters("@LMPDate", SqlDbType.DateTime, hashTable["LMPDate"].ToString());
                ClsUtility.AddParameters("@LMPNotaccessedReason", SqlDbType.Int, hashTable["LMPNotaccessedReason"].ToString());
                ClsUtility.AddParameters("@PDTdonet", SqlDbType.Int, hashTable["PDTdonet"].ToString());
                ClsUtility.AddParameters("@pregnant", SqlDbType.Int, hashTable["pregnant"].ToString());
                ClsUtility.AddParameters("@PMTCToffered", SqlDbType.Int, hashTable["PMTCToffered"].ToString());
                ClsUtility.AddParameters("@EDD", SqlDbType.DateTime, hashTable["EDD"].ToString());
                ClsUtility.AddParameters("@GivenPWPMessages", SqlDbType.Int, hashTable["GivenPWPMessages"].ToString());
                ClsUtility.AddParameters("@UnsafeSexImportanceExplained", SqlDbType.Int, hashTable["UnsafeSexImportanceExplained"].ToString());
                ClsUtility.AddParameters("@CondomsIssued", SqlDbType.Int, hashTable["CondomsIssued"].ToString());
                ClsUtility.AddParameters("@ReasonfornotIssuingCondoms", SqlDbType.VarChar, hashTable["ReasonfornotIssuingCondoms"].ToString());
                ClsUtility.AddParameters("@IntentionOfPregnancy", SqlDbType.Int, hashTable["IntentionOfPregnancy"].ToString());
                ClsUtility.AddParameters("@DiscussedFertilityOption", SqlDbType.Int, hashTable["DiscussedFertilityOption"].ToString());
                ClsUtility.AddParameters("@DiscussedDualContraception", SqlDbType.Int, hashTable["DiscussedDualContraception"].ToString());
                ClsUtility.AddParameters("@OnFP", SqlDbType.Int, hashTable["OnFP"].ToString());
                ClsUtility.AddParameters("@FPmethod", SqlDbType.Int, hashTable["FPmethod"].ToString());
                ClsUtility.AddParameters("@CervicalCancerEverScreened", SqlDbType.Int, hashTable["CervicalCancerEverScreened"].ToString());
                if (hashTable["CervicalCancerScreeningResults"] != null)
                {
                    ClsUtility.AddParameters("@CervicalCancerScreeningResults", SqlDbType.Int, hashTable["CervicalCancerScreeningResults"].ToString());
                }
                if (hashTable["ReferredForCervicalCancerScreening"] != null)
                {
                    ClsUtility.AddParameters("@ReferredForCervicalCancerScreening", SqlDbType.Int, hashTable["ReferredForCervicalCancerScreening"].ToString());
                }
                ClsUtility.AddParameters("@HPVOffered", SqlDbType.Int, hashTable["HPVOffered"].ToString());
                ClsUtility.AddParameters("@OfferedHPVaccine", SqlDbType.Int, hashTable["OfferedHPVaccine"].ToString());
                ClsUtility.AddParameters("@HPVDoseDate", SqlDbType.DateTime, hashTable["HPVDoseDate"].ToString());
                ClsUtility.AddParameters("@STIscreened", SqlDbType.Int, hashTable["STIscreened"].ToString());
                ClsUtility.AddParameters("@UrethralDischarge", SqlDbType.Int, hashTable["UrethralDischarge"].ToString());
                ClsUtility.AddParameters("@VaginalDischarge", SqlDbType.Int, hashTable["VaginalDischarge"].ToString());
                ClsUtility.AddParameters("@GenitalUlceration", SqlDbType.Int, hashTable["GenitalUlceration"].ToString());
                ClsUtility.AddParameters("@STItreatmentPlan", SqlDbType.VarChar, hashTable["STItreatmentPlan"].ToString());
                ClsUtility.AddParameters("@WardAdmission", SqlDbType.Int, hashTable["WardAdmission"].ToString());
                ClsUtility.AddParameters("@ReferToSpecialistClinic", SqlDbType.VarChar, hashTable["ReferToSpecialistClinic"].ToString());
                ClsUtility.AddParameters("@TransferOut", SqlDbType.VarChar, hashTable["TransferOut"].ToString());
                ClsUtility.AddParameters("@SpecifyOtherReferredTo", SqlDbType.VarChar, hashTable["SpecifyOtherReferredTo"].ToString());
                ClsUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, hashTable["OtherGeneralConditions"].ToString());
                ClsUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, hashTable["OtherAbdomenConditions"].ToString());
                ClsUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, hashTable["OtherCardiovascularConditions"].ToString());
                ClsUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, hashTable["OtherOralCavityConditions"].ToString());
                ClsUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, hashTable["OtherGenitourinaryConditions"].ToString());
                ClsUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, hashTable["OtherCNSConditions"].ToString());
                ClsUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, hashTable["OtherChestLungsConditions"].ToString());
                ClsUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, hashTable["OtherSkinConditions"].ToString());
                ClsUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());


                 
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
                     ClsUtility.AddParameters("WeightForHeight", SqlDbType.Decimal, hashTable["WeightForHeight"].ToString());
                 }
                 ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                 ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                 ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                 ClsObject VisitManager = new ClsObject();
                 VisitManager.Connection = this.Connection;

                 VisitManager.Transaction = this.Transaction;

                 // DataSet tempDataSet;
                 theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_RevisedFollowup_FORM", ClsDBUtility.ObjectEnum.DataSet);
                 visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                 //Pre Existing Medical Condition
                 for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                 {
                     ClsUtility.Init_Hashtable();
                     ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                     ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                     ClsUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                     ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                     ClsUtility.AddParameters("@OtherNotes", SqlDbType.Int, dtMultiSelectValues.Rows[i]["OtherNotes"].ToString());
                     ClsUtility.AddParameters("@DateField1", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                     int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
         public DataSet GetKNHRevisedAdultDetails(int ptn_pk, int visitpk)
         {
             lock (this)
             {
                 ClsObject BusinessRule = new ClsObject();
                 ClsUtility.Init_Hashtable();
                 ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                 ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                 return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_KNH_RevisedAdult_Data", ClsDBUtility.ObjectEnum.DataSet);
             }
         }
         public DataSet SaveUpdateKNHRevisedFollowupData_TriageTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId,string ModuleId)
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
                 ClsUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                 ClsUtility.AddParameters("@visitdate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));
                 ClsUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, hashTable["ChildAccompaniedByCaregiver"].ToString());
                 ClsUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, hashTable["TreatmentSupporterRelationship"].ToString());
                 ClsUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, hashTable["DisclosureStatus"].ToString());
                 ClsUtility.AddParameters("@HealthEducation", SqlDbType.Int, hashTable["HealthEducation"].ToString());
                 ClsUtility.AddParameters("@ReasonNotDisclosed", SqlDbType.VarChar, hashTable["ReasonNotDisclosed"].ToString());
                 ClsUtility.AddParameters("@OtherDisclosureReason", SqlDbType.VarChar, hashTable["OtherDisclosureReason"].ToString());
                 ClsUtility.AddParameters("@SchoolingStatus", SqlDbType.Int, hashTable["SchoolingStatus"].ToString());
                 ClsUtility.AddParameters("@HighestLevelAttained", SqlDbType.Int, hashTable["HighestLevelAttained"].ToString());
                 ClsUtility.AddParameters("@HIVSupportGroup", SqlDbType.Int, hashTable["HIVSupportGroup"].ToString());
                 ClsUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, hashTable["HIVSupportGroupMembership"].ToString());
                 ClsUtility.AddParameters("@AddressChanged", SqlDbType.Int, hashTable["AddressChanged"].ToString());
                 ClsUtility.AddParameters("@AddressChange", SqlDbType.VarChar, hashTable["AddressChange"].ToString());
                 ClsUtility.AddParameters("@PhoneNoChange", SqlDbType.VarChar, hashTable["PhoneNoChange"].ToString());
                 ClsUtility.AddParameters("@NursesComments", SqlDbType.VarChar, hashTable["NursesComments"].ToString());
                 ClsUtility.AddParameters("@ReferSpecClinic", SqlDbType.VarChar, hashTable["ReferSpecClinic"].ToString());
                 ClsUtility.AddParameters("@ReferOther", SqlDbType.VarChar, hashTable["ReferOther"].ToString()); 

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
                 
                 ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                 ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                 ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                 ClsUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                 ClsUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());
                 ClsObject VisitManager = new ClsObject();
                 VisitManager.Connection = this.Connection;

                 VisitManager.Transaction = this.Transaction;

                 // DataSet tempDataSet;
                 theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_RevisedFollowup_FORM_Triage", ClsDBUtility.ObjectEnum.DataSet);
                 visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                 //Pre Existing Medical Condition
                 for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                 {
                     ClsUtility.Init_Hashtable();
                     ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                     ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                     ClsUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                     ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                     ClsUtility.AddParameters("@OtherNotes", SqlDbType.Int, dtMultiSelectValues.Rows[i]["OtherNotes"].ToString());
                     ClsUtility.AddParameters("@DateField1", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                     int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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

         public DataSet SaveUpdateKNHRevisedFollowupData_CATab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId, string ModuleId)
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
                 ClsUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                 ClsUtility.AddParameters("@visitdate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));                
                 ClsUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, hashTable["SchoolPerfomance"].ToString());
                 ClsUtility.AddParameters("@OtherPresentingComplaints", SqlDbType.VarChar, hashTable["OtherPresentingComplaints"].ToString());
                 ClsUtility.AddParameters("@AdditonalPresentingComplaints", SqlDbType.VarChar, hashTable["OtherAdditionPresentingComplaints"].ToString());
                 //ClsUtility.AddParameters("@MedicalCondition", SqlDbType.Int, hashTable["MedicalCondition"].ToString());
                 ClsUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.Int, hashTable["OtherMedicalCondition"].ToString());
                 ClsUtility.AddParameters("@CurrentSurgicalCondition", SqlDbType.VarChar, hashTable["CurrentSurgicalCondition"].ToString());
                 ClsUtility.AddParameters("@PreviousSurgicalCondition", SqlDbType.VarChar, hashTable["PreviousSurgicalCondition"].ToString());
                 ClsUtility.AddParameters("@PreExistingMedicalConditionsFUP", SqlDbType.Int, hashTable["PreExistingMedicalConditionsFUP"].ToString());
                 ClsUtility.AddParameters("@Antihypertensives", SqlDbType.VarChar, hashTable["Antihypertensives"].ToString());
                 ClsUtility.AddParameters("@Anticonvulsants", SqlDbType.VarChar, hashTable["Anticonvulsants"].ToString());
                 ClsUtility.AddParameters("@Hypoglycemics", SqlDbType.VarChar, hashTable["Hypoglycemics"].ToString());
                 ClsUtility.AddParameters("@RadiotherapyChemotherapy", SqlDbType.VarChar, hashTable["RadiotherapyChemotherapy"].ToString());
                 ClsUtility.AddParameters("@Othercurrentlongmedication", SqlDbType.VarChar, hashTable["OtherCurrentLongtermMedication"].ToString());
                 ClsUtility.AddParameters("@PreviousAdmission", SqlDbType.Int, hashTable["PreviousAdmission"].ToString());
                 ClsUtility.AddParameters("@PreviousAdmissionDiagnosis", SqlDbType.VarChar, hashTable["PreviousAdmissionDiagnosis"].ToString());
                 ClsUtility.AddParameters("@PreviousAdmissionStart", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["PreviousAdmissionStart"].ToString()));
                 ClsUtility.AddParameters("@PreviousAdmissionEnd", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["PreviousAdmissionEnd"].ToString()));
                 ClsUtility.AddParameters("@HIVAssociatedConditionsPeads", SqlDbType.Int, hashTable["HIVAssociatedConditionsPeads"].ToString());
                 
                 ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                 ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                 ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                 ClsUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                 ClsUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());
                 ClsObject VisitManager = new ClsObject();
                 VisitManager.Connection = this.Connection;

                 VisitManager.Transaction = this.Transaction;

                 // DataSet tempDataSet;
                 theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_RevisedFollowup_FORM_CATab", ClsDBUtility.ObjectEnum.DataSet);
                 visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                 //Pre Existing Medical Condition
                 for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                 {
                     ClsUtility.Init_Hashtable();
                     ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                     ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                     ClsUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                     ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                     ClsUtility.AddParameters("@OtherNotes", SqlDbType.Int, dtMultiSelectValues.Rows[i]["OtherNotes"].ToString());
                     ClsUtility.AddParameters("@DateField1", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                     int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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

         public DataSet SaveUpdateKNHRevisedFollowupData_ExamTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId, string ModuleId)
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
                 ClsUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                 ClsUtility.AddParameters("@visitdate", SqlDbType.VarChar,String.Format("{0:dd-MMM-yyyy}", hashTable["visitDate"].ToString()));                
                 
                 ClsUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, hashTable["OtherGeneralConditions"].ToString());
                 ClsUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, hashTable["OtherAbdomenConditions"].ToString());
                 ClsUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, hashTable["OtherCardiovascularConditions"].ToString());
                 ClsUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, hashTable["OtherOralCavityConditions"].ToString());
                 ClsUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, hashTable["OtherGenitourinaryConditions"].ToString());
                 ClsUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, hashTable["OtherCNSConditions"].ToString());
                 ClsUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, hashTable["OtherChestLungsConditions"].ToString());
                 ClsUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, hashTable["OtherSkinConditions"].ToString());
                 ClsUtility.AddParameters("@Additionalphysexamnotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());
                 
                 ClsUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, hashTable["ARVSideEffects"].ToString());
                 ClsUtility.AddParameters("@Specifyothershorttermeffects", SqlDbType.VarChar, hashTable["Specifyothershorttermeffects"].ToString());
                 ClsUtility.AddParameters("@OtherLongtermEffects", SqlDbType.Int, hashTable["OtherLongtermEffects"].ToString());
                 ClsUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());                 
                 ClsUtility.AddParameters("@ReviewedPreviousResults", SqlDbType.Int, hashTable["ReviewedPreviousResults"].ToString());
                 ClsUtility.AddParameters("@ResultsReviewComments", SqlDbType.VarChar, hashTable["ResultsReviewComments"].ToString());
                 ClsUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, hashTable["HIVRelatedOI"].ToString());
                 ClsUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.VarChar, hashTable["NonHIVRelatedOI"].ToString());
                 if (hashTable["ProgressionInWHOstage"] != null)
                 {
                     ClsUtility.AddParameters("@ProgressionInWHOstage", SqlDbType.Int, hashTable["ProgressionInWHOstage"].ToString());
                 }
                 ClsUtility.AddParameters("@SpecifyWHOprogression", SqlDbType.VarChar, hashTable["SpecifyWHOprogression"].ToString());
                 ClsUtility.AddParameters("@WABStage", SqlDbType.Int, hashTable["WABStage"].ToString());
                 ClsUtility.AddParameters("@CurrentWHOStage", SqlDbType.Int, hashTable["CurrentWHOStage"].ToString());
                 ClsUtility.AddParameters("@Menarche", SqlDbType.Int, hashTable["Menarche"].ToString());
                 ClsUtility.AddParameters("@MenarcheDate", SqlDbType.DateTime, hashTable["MenarcheDate"].ToString());
                 ClsUtility.AddParameters("@TannerStaging", SqlDbType.Int, hashTable["TannerStaging"].ToString());
                 if (hashTable["PatientFUStatus"] != null)
                 {
                     ClsUtility.AddParameters("@PatientFUStatus", SqlDbType.Int, hashTable["PatientFUStatus"].ToString());
                 }
                 ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                 ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                 ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                 ClsUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                 ClsUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());
                 ClsObject VisitManager = new ClsObject();
                 VisitManager.Connection = this.Connection;
                 VisitManager.Transaction = this.Transaction;

                 // DataSet tempDataSet;
                 theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_RevisedFollowup_FORM_ExamTab", ClsDBUtility.ObjectEnum.DataSet);
                 visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                 //Pre Existing Medical Condition
                 for (int i = 0; i < dtMultiSelectValues.Rows.Count; i++)
                 {
                     ClsUtility.Init_Hashtable();
                     ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                     ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                     ClsUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                     ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                     ClsUtility.AddParameters("@OtherNotes", SqlDbType.Int, dtMultiSelectValues.Rows[i]["OtherNotes"].ToString());
                     ClsUtility.AddParameters("@DateField1", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                     int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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

         public DataSet SaveUpdateKNHRevisedFollowupData_MgtTab(Hashtable hashTable, DataTable dtMultiSelectValues, int DataQuality, int signature, int UserId, string ModuleId)
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
                 ClsUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                 ClsUtility.AddParameters("@visitdate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",hashTable["visitDate"].ToString()));
                 
                 ClsUtility.AddParameters("@MissedDosesFUP", SqlDbType.Int, hashTable["MissedDosesFUP"].ToString());
                 ClsUtility.AddParameters("@MissedDosesFUPspecify", SqlDbType.VarChar, hashTable["MissedDosesFUPspecify"].ToString());
                 ClsUtility.AddParameters("@DelaysInTakingMedication", SqlDbType.Int, hashTable["DelaysInTakingMedication"].ToString());                
                 ClsUtility.AddParameters("@ReferCounsellor", SqlDbType.Int, hashTable["DelaysMedReferConsul"].ToString()); 
                
                 ClsUtility.AddParameters("@WorkUpPlan", SqlDbType.VarChar, hashTable["WorkUpPlan"].ToString());
                 ClsUtility.AddParameters("@ReviewLabDiagtest", SqlDbType.VarChar, hashTable["SpecifyLabEvaluation"].ToString());
                 ClsUtility.AddParameters("@ARTTreatmentPlan", SqlDbType.Int, hashTable["ARTTreatmentPlan"].ToString());
                 ClsUtility.AddParameters("@OtherEligiblethorugh", SqlDbType.Int, hashTable["OtherEligiblethorugh"].ToString());
                 ClsUtility.AddParameters("@OtherARTStopCode", SqlDbType.VarChar, hashTable["OtherARTStopCode"].ToString());
                 ClsUtility.AddParameters("@NumberDrugsSubstituted", SqlDbType.Int, hashTable["NumberDrugsSubstituted"].ToString());
                 ClsUtility.AddParameters("@SpecifyotherARTchangereason", SqlDbType.VarChar, hashTable["SpecifyotherARTchangereason"].ToString());                
                 ClsUtility.AddParameters("@2ndLineRegimenSwitch", SqlDbType.Int, hashTable["2ndLineRegimenSwitch"].ToString());                 
                 ClsUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, hashTable["OIProphylaxis"].ToString());
                 ClsUtility.AddParameters("@Fluconazole", SqlDbType.Int, hashTable["Fluconazole"].ToString());
                 ClsUtility.AddParameters("@ReasonCTXpresribed", SqlDbType.Int, hashTable["ReasonCTXpresribed"].ToString());
                 ClsUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, hashTable["OtherOIProphylaxis"].ToString());
                 ClsUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, hashTable["OtherTreatment"].ToString());

                 ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                 ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                 ClsUtility.AddParameters("@signature", SqlDbType.Int, signature.ToString());
                 ClsUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                 ClsUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());
                 ClsObject VisitManager = new ClsObject();
                 VisitManager.Connection = this.Connection;

                 VisitManager.Transaction = this.Transaction;

                 // DataSet tempDataSet;
                 theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_RevisedFollowup_FORM_MgtTab", ClsDBUtility.ObjectEnum.DataSet);
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
                     ClsUtility.Init_Hashtable();
                     ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                     ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                     ClsUtility.AddParameters("@ID", SqlDbType.Int, dtMultiSelectValues.Rows[i]["ID"].ToString());
                     ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["FieldName"].ToString());
                     ClsUtility.AddParameters("@OtherNotes", SqlDbType.Int, dtMultiSelectValues.Rows[i]["OtherNotes"].ToString());
                     ClsUtility.AddParameters("@DateField1", SqlDbType.VarChar, dtMultiSelectValues.Rows[i]["DateField1"].ToString());
                     int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_line", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
    }
}
