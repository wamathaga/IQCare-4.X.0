using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IQTouchKNHAdultIE
    {
        // int IQTouchSaveExpressFormDetail();
        DataTable IQTouchGetKnhAdultIEData(BIQTouchAdultIE adultIEFields);
        int IQTouchSaveAdultIE(List<BIQTouchAdultIE> lstadultIEFields);

    }
    [Serializable()]
    public class BIQTouchAdultIE
    {
        public string Flag { get; set; }
        public int ID { get; set; }
        public int PtnPk { get; set; }
        public int LocationId { get; set; }
        public int VisitPk { get; set; }
        public int UserId { get; set; }

        public decimal Temperature { get; set; }
        public decimal RespirationRate { get; set; }
        public decimal HeartRate { get; set; }
        public decimal SystolicBloodPressure { get; set; }
        public decimal DiastolicBloodPressure { get; set; }
        public int DiagnosisConfirmed { get; set; }
        public DateTime ConfirmHIVPosDate { get; set; }
        public int ChildAccompaniedByCaregiver { get; set; }
        public int TreatmentSupporterRelationship { get; set; }
        public int HealthEducation { get; set; }
        public int DisclosureStatus { get; set; }
        public int SchoolingStatus { get; set; }
        public int HIVSupportgroup { get; set; }
        public int PatientReferredFrom { get; set; }
        public string NursesComments { get; set; }
        public int PresentingComplaints { get; set; }
        public int LMPassessmentValid { get; set; }
        public DateTime LMPDate { get; set; }
        public int LMPNotaccessedReason { get; set; }
        public DateTime EDD { get; set; }
        public string OtherDiseaseName { get; set; }
        public DateTime OtherDiseaseDate { get; set; }
        public string OtherDiseaseTreatment { get; set; }
        public int SchoolPerfomance { get; set; }
        public int TBAssessmentICF { get; set; }
        public int TBFindings { get; set; }
        public int TBresultsAvailable { get; set; }
        public int SputumSmear { get; set; }
        public DateTime SputumSmearDate { get; set; }
        public int ChestXRay { get; set; }
        public DateTime ChestXRayDate { get; set; }
        public int TissueBiopsy { get; set; }
        public DateTime TissueBiopsyDate { get; set; }
        public int CXR { get; set; }
        public string OtherCXR { get; set; }
        public int TBTypePeads { get; set; }
        public int PeadsTBPatientType { get; set; }
        public int TBPlan { get; set; }
        public string TBPlanOther { get; set; }
        public int TBRegimen { get; set; }
        public DateTime TBRegimenStartDate { get; set; }
        public DateTime TBRegimenEndDate { get; set; }
        public int TBTreatmentOutcomesPeads { get; set; }
        public int NoTB { get; set; }
        public int TBReason { get; set; }
        public DateTime INHStartDate { get; set; }
        public DateTime INHEndDate { get; set; }
        public DateTime PyridoxineStartDate { get; set; }
        public DateTime PyridoxineEndDate { get; set; }
        public int SuspectTB { get; set; }
        public DateTime StopINHDate { get; set; }
        public int ContactsScreenedForTB { get; set; }
        public string TBNotScreenedSpecify { get; set; }
        public int LongTermMedications { get; set; }
        public DateTime SulfaTMPDate { get; set; }
        public DateTime HormonalContraceptivesDate { get; set; }
        public DateTime AntihypertensivesDate { get; set; }
        public DateTime HypoglycemicsDate { get; set; }
        public DateTime AntifungalsDate { get; set; }
        public DateTime AnticonvulsantsDate { get; set; }
        public string OtherLongTermMedications { get; set; }
        public DateTime OtherCurrentLongTermMedications { get; set; }
        public int HIVRelatedHistory { get; set; }
        public decimal InitialCD4 { get; set; }
        public decimal InitialCD4Percent { get; set; }
        public DateTime InitialCD4Date { get; set; }
        public decimal HighestCD4Ever { get; set; }
        public decimal HighestCD4Percent { get; set; }
        public DateTime HighestCD4EverDate { get; set; }
        public decimal CD4atARTInitiation { get; set; }
        public DateTime CD4atARTInitiationDate { get; set; }
        public decimal CD4AtARTInitiationPercent { get; set; }
        public decimal MostRecentCD4 { get; set; }
        public decimal MostRecentCD4Percent { get; set; }
        public DateTime MostRecentCD4Date { get; set; }
        public decimal PreviousViralLoad { get; set; }
        public DateTime PreviousViralLoadDate { get; set; }
        public string OtherHIVRelatedHistory { get; set; }
        public int ARVExposure { get; set; }
        public DateTime PMTC1StartDate { get; set; }
        public string PMTC1Regimen { get; set; }
        public string PEP1Regimen { get; set; }
        public DateTime PEP1StartDate { get; set; }
        public string HAART1Regimen { get; set; }
        public DateTime HAART1StartDate { get; set; }
        public string Impression { get; set; }
        public int Diagnosis { get; set; }
        public string HIVRelatedOI { get; set; }
        public string NonHIVRelatedOI { get; set; }
        public int WHOStageIConditions { get; set; }
        public int WHOStageIIConditions { get; set; }
        public int WHOStageIIIConditions { get; set; }
        public int WHOStageIVConditions { get; set; }
        public int InitiationWHOstage { get; set; }
        public int WHOStage { get; set; }
        public int WABStage { get; set; }
        public int TannerStaging { get; set; }
        public int Mernarche { get; set; }
        public string SpecifyAntibioticAllery { get; set; }
        public int DrugAllergiesToxicitiesPaeds { get; set; }
        public string OtherDrugAllergy { get; set; }
        public int ARVSideEffects { get; set; }
        public int ShortTermEffects { get; set; }
        public string OtherShortTermEffects { get; set; }
        public int LongTermEffects { get; set; }
        public string OtherLongtermEffects { get; set; }
        public string WorkUpPlan { get; set; }
        public int LabEvaluationPeads { get; set; }
        public int SpecifyLabEvaluation { get; set; }
        public int Counselling { get; set; }
        public string OtherCounselling { get; set; }
        public int WardAdmission { get; set; }
        public string ReferToSpecialistClinic { get; set; }
        public string TransferOut { get; set; }
        public int ARTTreatmentPlanPeads { get; set; }
        public int SwitchReason { get; set; }
        public int StartART { get; set; }
        public int ARTEligibilityCriteria { get; set; }
        public string OtherARTEligibilityCriteria { get; set; }
        public int SubstituteRegimen { get; set; }
        public int NumberDrugsSubstituted { get; set; }
        public int StopTreatment { get; set; }
        public int StopTreatmentCodes { get; set; }
        public int RegimenPrescribed { get; set; }
        public string OtherRegimenPrescribed { get; set; }
        public int OIProphylaxis { get; set; }
        public int ReasonCTXPrescribed { get; set; }
        public string OtherTreatment { get; set; }
        public int SexualActiveness { get; set; }
        public int SexualOrientation { get; set; }
        public int HighRisk { get; set; }
        public int KnowSexualPartnerHIVStatus { get; set; }
        public int ParnerHIVStatus { get; set; }
        public int GivenPWPMessages { get; set; }
        public int SaferSexImportanceExplained { get; set; }
        public int UnsafeSexImportanceExplained { get; set; }
        public int PDTDone { get; set; }
        public int Pregnant { get; set; }
        public int PMTCTOffered { get; set; }
        public int IntentionOfPregnancy { get; set; }
        public int DiscussedFertilityOptions { get; set; }
        public int DiscussedDualContraception { get; set; }
        public int CondomsIssued { get; set; }
        public string CondomNotIssued { get; set; }
        public int STIScreened { get; set; }
        public int VaginalDischarge { get; set; }
        public int UrethralDischarge { get; set; }
        public int GenitalUlceration { get; set; }
        public string STITreatmentPlan { get; set; }
        public int OnFP { get; set; }
        public int FPMethod { get; set; }
        public int CervicalCancerScreened { get; set; }
        public int CervicalCancerScreeningResults { get; set; }
        public int ReferredForCervicalCancerScreening { get; set; }
        public int HPVOffered { get; set; }
        public int OfferedHPVVaccine { get; set; }
        public DateTime HPVDoseDate { get; set; }
        public int RefferedToFupF { get; set; }
        public string SpecifyOtherRefferedTo { get; set; }
        public int SignatureID { get; set; }
        public DateTime VisitDate { get; set; }
        public decimal Height {get;set; }
        public decimal Weight {get;set; }
        public int ValueID {get;set; }
        public int FieldID {get;set; }
        public string Diseasename {get;set; }
        public DateTime DiagnosisDate { get; set; }
        public string Treatment {get;set; }
        public int SectionID {get;set; }
        public int ConditionId {get;set; }
        public string OtherCondition {get;set; }
        public DateTime CurrentDate { get; set; }
        public DateTime Historic { get; set; }

    }


}
