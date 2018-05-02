using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "PatientAdherence")]
    public class PatientAdherence
    {

        [DataMember(Name = "PAM_Id", Order = 1)]
        public int PAM_Id { get; set; }

        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_Pk { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "LId", Order = 4)]
        public int LocationId { get; set; }

        [DataMember(Name = "TVD")]
        public string TravelVisitDescription { get; set; }

        [DataMember(Name = "HIVSAD")]
        public string HIVStatusArtDiscussion { get; set; }

        [DataMember(Name = "SS")]
        public int? SupportSystem { get; set; }

        [DataMember(Name = "SSD")]
        public string SupportSystemDetails { get; set; }

        [DataMember(Name = "HIVBD")]
        public string HIVStatusBotherDetails { get; set; }

        [DataMember(Name = "PTD")]
        public string PeopleTreatmentDetails { get; set; }

        [DataMember(Name = "SI")]
        public string StigmaInterfering { get; set; }

        [DataMember(Name = "RB")]
        public string ReligiousBeliefs { get; set; }

        [DataMember(Name = "ROS")]
        public int? ReferredOtherServices { get; set; }

        [DataMember(Name = "AA")]
        public int? AppointmentsAttend { get; set; }

        [DataMember(Name = "EAR")]
        public string ExperienceAppointReorganized { get; set; }

        [DataMember(Name = "Total")]
        public int Total { get; set; }

        [DataMember(Name = "DS")]
        public string DepressionSeverity { get; set; }

        [DataMember(Name = "REC")]
        public string Recommended { get; set; }
    }

    [Serializable]
    [DataContract(Name = "PatientAdherenceBarrier")]
    public class PatientAdherenceBarrier : PatientAdherence
    {
        [DataMember(Name = "AHIVS")]
        public int AcceptedHIVStatus { get; set; }

        [DataMember(Name = "ISAA")]
        public int IsAgeAppropriate { get; set; }

        [DataMember(Name = "ROT")]
        public int RiskOfTransmission { get; set; }

        [DataMember(Name = "UART")]
        public int UnderstandART { get; set; }

        [DataMember(Name = "ARTSE")]
        public int ARTSideEffect { get; set; }

        [DataMember(Name = "AB")]
        public int AdherenceBenefits { get; set; }

        [DataMember(Name = "CNA")]
        public int ConsequencesNonAdherence { get; set; }

        [DataMember(Name = "TD")]
        public string TypicalDay { get; set; }

        [DataMember(Name = "MIT")]
        public string MedicinesIntake { get; set; }

        [DataMember(Name = "PCG")]
        public string PrimartCareGiver { get; set; }

        [DataMember(Name = "LW")]
        public string LiveWith { get; set; }

        [DataMember(Name = "CINR")]
        public string ChangesInRelationship { get; set; }

    }

    [Serializable]
    [DataContract(Name = "PatientAdherenceCounseling")]
    public class PatientAdherenceCounseling : PatientAdherence
    {
        [DataMember(Name = "ACM")]
        public int? AssignedCaseManager { get; set; }

        [DataMember(Name = "UDL")]
        public List<UserDesignation> Users = new List<UserDesignation>();

        [DataMember(Name = "CD")]
        public List<CodeDeCode> DropdownValues = new List<CodeDeCode>();

        [DataMember(Name = "AST")]
        public List<CodeDeCode> AleardySavedTabs = new List<CodeDeCode>();

        [DataMember(Name = "AD")]
        public DateTime? AssignedDate { get; set; }

        [DataMember(Name = "TI")]
        public int? TherapyImplemented { get; set; }

        [DataMember(Name = "CS")]
        public int? CurrentSession { get; set; }

        [DataMember(Name = "FFD")]
        public DateTime? FormFilledDate { get; set; }

        [DataMember(Name = "APER")]
        public string AdherencePercent { get; set; }

        [DataMember(Name = "PAMId")]
        public int PAM_Id { get; set; }

        [DataMember(Name = "ISFM")]
        public int IsForgotMed { get; set; }

        [DataMember(Name = "ISCM")]
        public int IsCarelessMed { get; set; }

        [DataMember(Name = "ISWTM")]
        public int IsWorseTakingMed { get; set; }

        [DataMember(Name = "ISFBM")]
        public int IsFeelBetterMed { get; set; }

        [DataMember(Name = "MMAS4S")]
        public string MMAS4Score { get; set; }

        [DataMember(Name = "MMAS4R")]
        public string MMAS4Rating { get; set; }

        [DataMember(Name = "ISYM")]
        public int IsYesterdayMed { get; set; }

        [DataMember(Name = "ISSUC")]
        public int IsSymptomUnderControl { get; set; }

        [DataMember(Name = "ISSTP")]
        public int IsStickingTreatmentPlan { get; set; }

        [DataMember(Name = "RM")]
        public int RememberingMedications { get; set; }

        [DataMember(Name = "MMAS8S")]
        public string MMAS8Score { get; set; }

        [DataMember(Name = "MMAS8R")]
        public string MMAS8Rating { get; set; }

        [DataMember(Name = "MMAS8SG")]
        public string MMAS8Suggestion { get; set; }

        [DataMember(Name = "MGTSIG")]
        public int MGTSIG { get; set; }

        [DataMember(Name = "PUVL")]
        public int? PatientUnderstandVL { get; set; }

        [DataMember(Name = "PFAR")]
        public string PatientFeelingAboutResult { get; set; }

        [DataMember(Name = "PTAHVL")]
        public string PatientThinkAboutHighVL { get; set; }

        [DataMember(Name = "EHTD")]
        public string ExplainedHowTakeDrug { get; set; }

        [DataMember(Name = "PDR")]
        public string PatientDailyRoutine { get; set; }

        [DataMember(Name = "SE")]
        public string SideEffect { get; set; }

        [DataMember(Name = "DSTD")]
        public string DifficultSituationTakeDrug { get; set; }

        [DataMember(Name = "TDED")]
        public string TakeDrugEveryDay { get; set; }

        [DataMember(Name = "PM")]
        public string PatientMotivation { get; set; }

        [DataMember(Name = "SOI")]
        public string SourceOfIncome { get; set; }

        [DataMember(Name = "CGC")]
        public string ChallengesGettingClinic { get; set; }

        [DataMember(Name = "BHV")]
        public int? BenefitHomeVisit { get; set; }

        [DataMember(Name = "APLAN")]
        public string AdherencePlan { get; set; }

        [DataMember(Name = "FUD")]
        public DateTime? FollowupDate { get; set; }

        [DataMember(Name = "MV")]
        public int? MultiVisit { get; set; }

    }
}
