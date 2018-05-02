using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    
    public interface IKNHAdultIE
    {
        // int IQTouchSaveExpressFormDetail();
        DataTable GetKnhAdultIEData(BIQAdultIE adultIEFields);
        DataTable SaveAdultIE(List<BIQAdultIE> lstadultIEFields, StringBuilder strMultiselect);
        DataSet GetKnhAdultIEFormData(BIQAdultIE adultIEFields);
        DataTable SaveAdultIE_TriageTab(List<BIQAdultIE> lstadultIEFields, StringBuilder strMultiselect);
        DataTable SaveAdultIE_CATab(List<BIQAdultIE> lstadultIEFields, StringBuilder strMultiselect);
        DataTable SaveAdultIE_ExamTab(List<BIQAdultIE> lstadultIEFields, StringBuilder strMultiselect);
        DataTable SaveAdultIE_MgtTab(List<BIQAdultIE> lstadultIEFields, StringBuilder strMultiselect);
        DataTable GetPatientVisitIdAdultIE(int PatientId, int Visittype);
        DataTable GetDataTable(string Mastertablename, string fieldname);
    }
    [Serializable()]
     public class BIQAdultIE
     {
        public BIQAdultIE()
        {
        VisitDate = "01-Jan-1900";
        UnsafeLMPDate = Convert.ToDateTime("01-Jan-1900");
        LMPDate = Convert.ToDateTime("01-Jan-1900");
        EDD = Convert.ToDateTime("01-Jan-1900");
        OtherDiseaseDate = Convert.ToDateTime("01-Jan-1900");
        SputumSmearDate = Convert.ToDateTime("01-Jan-1900");
        ChestXRayDate = Convert.ToDateTime("01-Jan-1900");
        TissueBiopsyDate = Convert.ToDateTime("01-Jan-1900");
        TBRegimenStartDate = Convert.ToDateTime("01-Jan-1900");
        TBRegimenEndDate = Convert.ToDateTime("01-Jan-1900");
        INHStartDate = Convert.ToDateTime("01-Jan-1900");
        INHEndDate = Convert.ToDateTime("01-Jan-1900");
        PyridoxineStartDate = Convert.ToDateTime("01-Jan-1900");
        PyridoxineEndDate = Convert.ToDateTime("01-Jan-1900");
        StopINHDate = Convert.ToDateTime("01-Jan-1900");
        Cotrimoxazole = Convert.ToDateTime("01-Jan-1900");
        HormonalContraceptivesDate = Convert.ToDateTime("01-Jan-1900");
        AntihypertensivesDate = Convert.ToDateTime("01-Jan-1900");
        HypoglycemicsDate = Convert.ToDateTime("01-Jan-1900");
        Fluconazole = Convert.ToDateTime("01-Jan-1900");
        AnticonvulsantsDate = Convert.ToDateTime("01-Jan-1900");
        OtherCurrentLongTermMedications = Convert.ToDateTime("01-Jan-1900");
        InitialCD4Date = Convert.ToDateTime("01-Jan-1900");
        HighestCD4EverDate = Convert.ToDateTime("01-Jan-1900");
        CD4atARTInitiationDate = Convert.ToDateTime("01-Jan-1900");
        MostRecentCD4Date = Convert.ToDateTime("01-Jan-1900");
        PreviousViralLoadDate = Convert.ToDateTime("01-Jan-1900");
        PMTC1StartDate = Convert.ToDateTime("01-Jan-1900");
        PEP1StartDate = Convert.ToDateTime("01-Jan-1900");
        HAART1StartDate = Convert.ToDateTime("01-Jan-1900");
        Mernarchedate = Convert.ToDateTime("01-Jan-1900");
        HPVDoseDate = Convert.ToDateTime("01-Jan-1900");
        DiagnosisDate = Convert.ToDateTime("01-Jan-1900");
        CurrentDate = Convert.ToDateTime("01-Jan-1900");
        Historic = Convert.ToDateTime("01-Jan-1900");
        ddlweightforage = 0;
        //Triage-Vital sign controls
        Temperature = 0;
        RespirationRate= 0;
        HeartRate= 0;
        SystolicBloodPressure= 0;
        DiastolicBloodPressure= 0;
        Height= 0;
        Weight= 0;
        txtheadcircumference= 0;
        ddlweightforage= 0;
        txtweightforheight = 0;
        //Triage-HIV Care and Support Evaluation
        DiagnosisConfirmed = 0;
        ConfirmHIVPosDate = "";
        ChildAccompaniedByCaregiver = 0;
        TreatmentSupporterRelationship = 0;
        HealthEducation = 0;
        DisclosureStatus= 0;
        reasonnotdisclosed =0;
        otherdisclosurestatus ="";
        SchoolingStatus= 0;
        Highestlevelattained = 0;
        HIVSupportgroup= 0;
        supportgroupmembership= "";
        PatientReferredFrom = 0;
        NursesComments = "";
        //ClinicalHistory-Presenting complaints
        PresentingComplaints = 0;
        otherspecifiedcomplaints = "";
        Additionalcomplaints= "";

        //ClinicalHistory-Medical History
        RespiratoryDiseaseName = "";
        RespiratoryDiseaseDate = Convert.ToDateTime("01-Jan-1900"); ;
        RespiratoryDiseaseTreatment = "";
        CardiovascularDiseaseName = "";
        CardiovascularDiseaseDate = Convert.ToDateTime("01-Jan-1900"); ;
        CardiovascularDiseaseTreatment = "";
        GastroIntestinalDiseaseName = "";
        GastroIntestinalDiseaseDate = Convert.ToDateTime("01-Jan-1900"); ;
        GastroIntestinalDiseaseTreatment = "";
        NervousDiseaseName= "";
        NervousDiseaseDate = Convert.ToDateTime("01-Jan-1900"); ;
        NervousDiseaseTreatment= "";
        DermatologyDiseaseName= "";
        DermatologyDiseaseDate = Convert.ToDateTime("01-Jan-1900"); ;
        DermatologyDiseaseTreatment = "";
        MusculoskeletalDiseaseName = "";
        MusculoskeletalDiseaseDate = Convert.ToDateTime("01-Jan-1900"); ;
        MusculoskeletalDiseaseTreatment = "";
        PsychiatricDiseaseName = "";
        PsychiatricDiseaseDate = Convert.ToDateTime("01-Jan-1900"); ;
        PsychiatricDiseaseTreatment = "";
        HematologicalDiseaseName = "";
        HematologicalDiseaseDate = Convert.ToDateTime("01-Jan-1900"); ;
        HematologicalDiseaseTreatment = "";
        GenitalUrinaryDiseaseName = "";
        GenitalUrinaryDiseaseDate = Convert.ToDateTime("01-Jan-1900"); ;
        GenitalUrinaryDiseaseTreatment = "";
        OphthamologyDiseaseName = "";
        OphthamologyDiseaseDate = Convert.ToDateTime("01-Jan-1900"); ;
        OphthamologyDiseaseTreatment = "";
        ENTDiseaseName = "";
        ENTDiseaseDate = Convert.ToDateTime("01-Jan-1900"); ;
        ENTDiseaseTreatment = "";

        //ClinicalHistory-Other Medical History
        OtherMedicalHistory = 0;
        LMPassessmentValid= 0;
        LMPNotaccessedReason = 0;
        OtherDiseaseName = "";
        OtherDiseaseTreatment = "";
        SchoolPerfomance = 0;
        MedHistoryFP = 0;
        MedHistoryLastFP = "";

        //TB Screening-TBAssessment
        TBAssessementICF = 0;
        TBFindings = 0;
        TBresultsAvailable = 0;
        SputumSmear = 0;
        ChestXRay = 0;
        TissueBiopsy = 0;
        CXR = 0;
        OtherCXR = "";
        TBType = 0;
        TBPatientType = 0;
        TBPlan = 0;
        TBPlanOther = "";
        TBRegimen = 0;
        TBRegimenother = "";
        TBTreatmentOutcomes = 0;

        //TB Screening-IPT
        IPT = 0;
        NoTBSign = 0;
        pyridoxine = 0;
        adherenceassessed = 0;
        dosesmissed = 0;
        adherencereferred = 0;

        //TB Screening-DiscontinueIPT
        DiscontinueIPT = 0;
        SuspectTB = 0;
        ContactsScreenedForTB = 0;
        TBNotScreenedSpecify = "";

        //Examination-Current Long Term Medications
        CurrentLongtermmedications= 0;
        LongTermMedications= 0;
        OtherLongTermMedications = "";
         
        //Examination-Additional medicinal condition notes
        Physicalexamination = 0;
        Additionalmedicalconditionnotes ="";

        //Examination-HIVRelatedTests
        HIVRelatedTest = 0;
        HIVRelatedHistory = 0;
        InitialCD4 = 0;
        InitialCD4Percent = 0;
        HighestCD4Ever = 0;
        HighestCD4Percent = 0;
        CD4atARTInitiation= 0;
        CD4AtARTInitiationPercent = 0;
        MostRecentCD4 = 0;
        MostRecentCD4Percent= 0;
        PreviousViralLoad= 0;
        OtherHIVRelatedHistory = "";

        //Examination-ARVExposure
        ARVExposureYesNo = 0;
        ARVExposure = 0;
        PMTC1Regimen = "";
        PEP1Regimen= "";
        HAART1Regimen = "";
        Impression= "";
        ARVExposerdosesmissed = 0;
        ARVExposerdelaydoses = 0;

        //Examination-Diagnosis 
        Diagnosis= 0;
        HIVRelatedOI= "";
        NonHIVRelatedOI= "";

        //Examination-WHOStaging
        WHOStaging= 0;
        WHOStageIConditions = 0;
        WHOStageIIConditions= 0;
        WHOStageIIIConditions= 0;
        WHOStageIVConditions= 0;
        //Examination-StagingInitialEvaluation
        InitialEvaluation= 0;
        InitiationWHOstage= 0;
        WHOStage= 0;
        WABStage= 0;
        TannerStaging = 0;
        Mernarche = 0;
        //Management-Drug Allergy and Toxicities
        DrugAllergyToxicity= 0;
        SpecifyAntibioticAllery= "";
        ARVDrugAllergy = "";
        OtherDrugAllergy = "";
        //Management-ARV Side effects
        ARVSideEffect = 0;
        AnyARVSideEffects = 0;
        ShortTermEffects = 0;
        OtherShortTermEffects= "";
        LongTermEffects= 0;
        OtherLongtermEffects = "";
        WorkUpPlan= "";
        //Management-Lab Evaluation
        LabEvaluation= 0;
        LabEvaluationsub= 0;
        //Management-Plan
        Plan = 0;
        WardAdmission= 0;
        ReferToSpecialistClinic= "";
        TransferOut= "";
        //Management-ART TreatmentPlan
        ARTTreatment= 0;
        ARTTreatmentPlan= 0;
        SwitchReason= 0;
        StartART= 0;
        ARTEligibilityCriteria = 0;
        OtherARTEligibilityCriteria="";
        SubstituteRegimen= 0;
        NumberDrugsSubstituted= 0;
        StopTreatment= 0;
        StopTreatmentCodes= 0;
        RegimenPrescribed= 0;
        OtherRegimenPrescribed= "";
        //Management-OI Treatment
        OITreatment= 0;
        OIProphylaxis = 0;
        ReasonCoTrimoxPrescribed = 0;
        OtherTreatment= "";
        //Prev with +Ves-Sexually Assessment
        SexualAssessment = 0;
        SexualActiveness = 0;
        SexualOrientation= 0;
        HighRisk = 0;
        KnowSexualPartnerHIVStatus = 0;
        PartnerHIVStatus = 0;
        //Prev with +Ves-PWP Interventions
        PWPinterventions= 0;
        GivenPWPMessages= 0;
        SaferSexImportanceExplained = 0;
        UnsafeSexImportanceExplained= 0;
        UnsafeLMPReason= 0;
        PDTDone= 0;
        ClientPregnant= 0;
        PMTCTOffered = 0;
        IntentionOfPregnancy = 0;
        DiscussedFertilityOptions= 0;
        DiscussedDualContraception= 0;
        CondomsIssued = 0;
        ReasonCondomNotIssued = "";
        STIScreened = 0;
        VaginalDischarge = 0;
        UrethralDischarge = 0;
        GenitalUlceration = 0;
        STITreatmentPlan= "";
        OnFP = 0;
        FPMethod = 0;
        CervicalCancerScreened = 0;
        CervicalCancerScreeningResults= 0;
        ReferredForCervicalCancerScreening = 0;
        HPVOffered = 0;
        OfferedHPVVaccine = 0;
        RefferedToFupF = 0;
        SpecifyOtherRefferedTo = "";
        SignatureID = 0;
        AdultIEFeatureID = 0;
        }

         public string Flag { get; set; }
         public int ID { get; set; }
         public int PtnPk { get; set; }
         public int LocationId { get; set; }
         public int VisitPk { get; set; }
         public string VisitDate { get; set; }
         public int UserId { get; set; }
         //Triage-Vital sign controls
         public decimal Temperature { get; set; }
         public decimal RespirationRate { get; set; }
         public decimal HeartRate { get; set; }
         public decimal SystolicBloodPressure { get; set; }
         public decimal DiastolicBloodPressure { get; set; }
         public decimal Height { get; set; }
         public decimal Weight { get; set; }
         public decimal txtheadcircumference { get; set; }
         public int ddlweightforage { get; set;}
         public decimal txtweightforheight { get; set; }
         //Triage-HIV Care and Support Evaluation
         public int DiagnosisConfirmed { get; set; }
         public String ConfirmHIVPosDate { get; set; }
         public int ChildAccompaniedByCaregiver { get; set; }
         public int TreatmentSupporterRelationship { get; set; }
         public int HealthEducation { get; set; }
         public int DisclosureStatus { get; set; }
         public int reasonnotdisclosed { get; set; }
         public string otherdisclosurestatus { get; set; }
         public int SchoolingStatus { get; set; }
         public int Highestlevelattained { get; set; }
         public int HIVSupportgroup { get; set; }
         public string supportgroupmembership { get; set; }
         public int PatientReferredFrom { get; set; }
         public string OtherPatientReferredFrom { get; set; }
         public string NursesComments { get; set; }
        
        
        //ClinicalHistory-Presenting complaints
         public int PresentingComplaints { get; set; }
         public string otherspecifiedcomplaints { get; set; }
         public string Additionalcomplaints { get; set; }

         //ClinicalHistory-Medical History
         public string RespiratoryDiseaseName { get; set; }
         public DateTime RespiratoryDiseaseDate { get; set; }
         public string RespiratoryDiseaseTreatment { get; set; }
         public string CardiovascularDiseaseName { get; set; }
         public DateTime CardiovascularDiseaseDate { get; set; }
         public string CardiovascularDiseaseTreatment { get; set; }
         public string GastroIntestinalDiseaseName { get; set; }
         public DateTime GastroIntestinalDiseaseDate { get; set; }
         public string GastroIntestinalDiseaseTreatment { get; set; }
         public string NervousDiseaseName { get; set; }
         public DateTime NervousDiseaseDate { get; set; }
         public string NervousDiseaseTreatment { get; set; }
         public string DermatologyDiseaseName { get; set; }
         public DateTime DermatologyDiseaseDate { get; set; }
         public string DermatologyDiseaseTreatment { get; set; }
         public string MusculoskeletalDiseaseName { get; set; }
         public DateTime MusculoskeletalDiseaseDate { get; set; }
         public string MusculoskeletalDiseaseTreatment { get; set; }
         public string PsychiatricDiseaseName { get; set; }
         public DateTime PsychiatricDiseaseDate { get; set; }
         public string PsychiatricDiseaseTreatment { get; set; }
         public string HematologicalDiseaseName { get; set; }
         public DateTime HematologicalDiseaseDate { get; set; }
         public string HematologicalDiseaseTreatment { get; set; }
         public string GenitalUrinaryDiseaseName { get; set; }
         public DateTime GenitalUrinaryDiseaseDate { get; set; }
         public string GenitalUrinaryDiseaseTreatment { get; set; }
         public string OphthamologyDiseaseName { get; set; }
         public DateTime OphthamologyDiseaseDate { get; set; }
         public string OphthamologyDiseaseTreatment { get; set; }
         public string ENTDiseaseName { get; set; }
         public DateTime ENTDiseaseDate { get; set; }
         public string ENTDiseaseTreatment { get; set; }

         //ClinicalHistory-Other Medical History
         public int OtherMedicalHistory { get; set; }
         public int LMPassessmentValid { get; set; }
         public DateTime LMPDate { get; set; }
         public int LMPNotaccessedReason { get; set; }
         public DateTime EDD { get; set; }
         public string OtherDiseaseName { get; set; }
         public DateTime OtherDiseaseDate { get; set; }
         public string OtherDiseaseTreatment { get; set; }
         public int SchoolPerfomance { get; set; }
         public int MedHistoryFP { get; set; }
         public string MedHistoryLastFP { get; set; } 

         //TB Screening-TBAssessment
         public int TBAssessementICF { get; set; }
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
         public int TBType { get; set; }
         public int TBPatientType { get; set; }
         public int TBPlan { get; set; }
         public string TBPlanOther { get; set; }
         public int TBRegimen { get; set; }
         public string TBRegimenother { get; set; }
         public DateTime TBRegimenStartDate { get; set; }
         public DateTime TBRegimenEndDate { get; set; }
         public int TBTreatmentOutcomes { get; set; }

         //TB Screening-IPT
         public int IPT { get; set; }
         public int NoTBSign { get; set; }
         public DateTime INHStartDate { get; set; }
         public DateTime INHEndDate { get; set; }
         public int pyridoxine { get; set; }
         public DateTime PyridoxineStartDate { get; set; }
         public DateTime PyridoxineEndDate { get; set; }
         public int adherenceassessed {get; set; }
         public int dosesmissed { get; set; }
         public int adherencereferred { get; set; }

         //TB Screening-DiscontinueIPT
         public int DiscontinueIPT { get; set; }
         public int SuspectTB { get; set; }
         public DateTime StopINHDate { get; set; }
         public int ContactsScreenedForTB { get; set; }
         public string TBNotScreenedSpecify { get; set; }

         //Examination-Current Long Term Medications
         public int CurrentLongtermmedications { get; set; }
         public int LongTermMedications { get; set; }
         public DateTime Cotrimoxazole { get; set; }
         public DateTime HormonalContraceptivesDate { get; set; }
         public DateTime AntihypertensivesDate { get; set; }
         public DateTime HypoglycemicsDate { get; set; }
         public DateTime Fluconazole { get; set; }
         public DateTime AnticonvulsantsDate { get; set; }
         public DateTime OtherCurrentLongTermMedications { get; set; }
         public string OtherLongTermMedications { get; set; }
         
         //Examination-Additional medicinal condition notes
         public int Physicalexamination { get; set; }
         public string Additionalmedicalconditionnotes { get; set; }

        ///Physical Examination Notes////
         public string OtherGeneralConditions { get; set; }
         public string OtherAbdomenConditions { get; set; }
         public string OtherCardiovascularConditions { get; set; }
         public string OtherOralCavityConditions { get; set; }
         public string OtherGenitourinaryConditions { get; set; }
         public string OtherCNSConditions { get; set; }
         public string OtherChestLungsConditions { get; set; }
         public string OtherSkinConditions { get; set; }

         //Examination-HIVRelatedTests
         public int HIVRelatedTest { get; set; }
         public int HIVRelatedHistory { get; set; }
         public decimal InitialCD4 { get; set; }
         public decimal InitialCD4Percent { get; set; }
         public DateTime InitialCD4Date { get; set; }
         public decimal HighestCD4Ever { get; set; }
         public decimal HighestCD4Percent { get; set; }
         public DateTime HighestCD4EverDate { get; set; }
         public decimal CD4atARTInitiation { get; set; }
         public decimal CD4AtARTInitiationPercent { get; set; }
         public DateTime CD4atARTInitiationDate { get; set; }
         public decimal MostRecentCD4 { get; set; }
         public decimal MostRecentCD4Percent { get; set; }
         public DateTime MostRecentCD4Date { get; set; }
         public decimal PreviousViralLoad { get; set; }
         public DateTime PreviousViralLoadDate { get; set; }
         public string OtherHIVRelatedHistory { get; set; }

        //Examination-ARVExposure
         public int ARVExposureYesNo { get; set; }
         public int ARVExposure { get; set; }
         public DateTime PMTC1StartDate { get; set; }
         public string PMTC1Regimen { get; set; }
         public string PEP1Regimen { get; set; }
         public DateTime PEP1StartDate { get; set; }
         public string HAART1Regimen { get; set; }
         public DateTime HAART1StartDate { get; set; }
         public string Impression { get; set; }
         public int ARVExposerdosesmissed { get; set; }
         public int ARVExposerdelaydoses { get; set; }

         //Examination-Diagnosis 
         public int Diagnosis { get; set; }
         public string HIVRelatedOI { get; set; }
         public string NonHIVRelatedOI { get; set; }

         //Examination-WHOStaging
         public int WHOStaging { get; set; }
         public int WHOStageIConditions { get; set; }
         public int WHOStageIIConditions { get; set; }
         public int WHOStageIIIConditions { get; set; }
         public int WHOStageIVConditions { get; set; }
        
        //Examination-StagingInitialEvaluation
         public int ProgressionInWHOstage { get; set; }
         public string SpecifyWHOprogression { get; set; }
         public int InitialEvaluation { get; set; }
         public int InitiationWHOstage { get; set; }
         public int WHOStage { get; set; }
         public int WABStage { get; set; }
         public int TannerStaging { get; set; }
         public int Mernarche { get; set; }
         public DateTime Mernarchedate { get; set; }

         //Management-Drug Allergy and Toxicities
         public int DrugAllergyToxicity { get; set; }
         public string SpecifyAntibioticAllery { get; set; }
         public string ARVDrugAllergy { get; set; }
         public string OtherDrugAllergy { get; set; }
         //Management-ARV Side effects
         public int ARVSideEffect { get; set; }
         public int AnyARVSideEffects { get; set; }
         public int ShortTermEffects { get; set; }
         public string OtherShortTermEffects { get; set; }
         public int LongTermEffects { get; set; }
         public string OtherLongtermEffects { get; set; }
         public string WorkUpPlan { get; set; }
         //Management-Lab Evaluation
         public int LabEvaluation { get; set; }
         public int LabEvaluationsub { get; set; }
         public string OtherLabReview { get; set; }
         //Management-Plan
         public int Plan { get; set; }
         public int WardAdmission { get; set; }
         public string ReferToSpecialistClinic { get; set; }
         public string TransferOut { get; set; }
         //Management-ART TreatmentPlan
         public int ARTTreatment { get; set; }
         public int ARTTreatmentPlan { get; set; }
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
         public string OtherARTChangeCode { get; set; }
         public string OtherARTStopCode { get; set; }
         //Management-OI Treatment
         public int OITreatment { get; set; }
         public int OIProphylaxis { get; set; }
         public int ReasonCoTrimoxPrescribed { get; set; }
         public int ReasonFluconazolePrescribed { get; set; }
         public string OtherTreatment { get; set; }

         //Prev with +Ves-Sexually Assessment
         public int SexualAssessment { get; set; }
         public int SexualActiveness { get; set; }
         public int SexualOrientation { get; set; }
         public int HighRisk { get; set; }
         public int KnowSexualPartnerHIVStatus { get; set; }
         public int PartnerHIVStatus { get; set; }
         //Prev with +Ves-PWP Interventions
         public int PWPinterventions { get; set; }
         public int GivenPWPMessages { get; set; }
         public int SaferSexImportanceExplained { get; set; }
         public int UnsafeSexImportanceExplained { get; set; }
         public DateTime UnsafeLMPDate { get; set; }
         public int UnsafeLMPReason { get; set; }
         public int PDTDone { get; set; }
         public int ClientPregnant { get; set; }
         public int PMTCTOffered { get; set; }
         public int IntentionOfPregnancy { get; set; }
         public int DiscussedFertilityOptions { get; set; }
         public int DiscussedDualContraception { get; set; }
         public int CondomsIssued { get; set; }
         public string ReasonCondomNotIssued { get; set; }
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
         //Signature
         public int SignatureID { get; set; }

         //FeatureID
         public int AdultIEFeatureID { get; set; }

         public int ValueID { get; set; }
         public int FieldID { get; set; }
         public string Diseasename { get; set; }
         public DateTime DiagnosisDate { get; set; }
         public string Treatment { get; set; }
         public int SectionID { get; set; }
         public int ConditionId { get; set; }
         public string OtherCondition { get; set; }
         public DateTime CurrentDate { get; set; }
         public DateTime Historic { get; set; }
         public string FieldName { get; set; }

         //All AdultIE Multiselects-Presenting complaints, TBAssessmentICF,IPT-Stopreason, ReviewChecklist
         //Physical Examination-General,Cardiovarscular,Oral Cavity,Genitourinary,CNS,Chest/Lungs,Skin,Abdomen,
         //Diagnosis-Diagnosis and current illness, Drug Allergy and Toxicities,Counselling,
         //Switch Regimen
         //High risk 
         public List<AdultIEMultiselect> AdultIEMultiSelect{ get; set; }
     }
    [Serializable()]
        public class AdultIEMultiselect
        {
            public AdultIEMultiselect()
            {
                DateField1 = "01-Jan-1990";
                DateField2 = "01-Jan-1990";
                Notes = "";
                NumericField = 0;
                UserId = 1;
                FieldId = 0;
            }
            public int ValueID { get; set; }
            public int FieldId { get; set; }
            public string FieldName { get; set; }
            public string DateField1 { get; set; }
            public string DateField2 { get; set; }
            public string Notes { get; set; }
            public int NumericField { get; set; }
            public int UserId { get; set; }
            }

}
