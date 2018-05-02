using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "PrEP")]
    public class PrEP
    {

        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VT", Order = 3)]
        public List<CodeDeCode> VisitTypes = new List<CodeDeCode>();

        [DataMember(Name = "HIV", Order = 4)]
        public HIVCare EntryPoint = new HIVCare();

        [DataMember(Name = "DL", Order = 5)]
        public List<District> Districts = new List<District>();

        [DataMember(Name = "FL", Order = 6)]
        public List<Facility> FacilityList = new List<Facility>();

        [DataMember(Name = "OV", Order = 7)]
        public OrdVisit OrdVisit = new OrdVisit();

        [DataMember(Name = "PV", Order = 9)]
        public PatientVitals PatientVitals = new PatientVitals();

        [DataMember(Name = "DIST", Order = 10)]
        public int District { get; set; }

        [DataMember(Name = "UID", Order = 11)]
        public int UserId { get; set; }

        [DataMember(Name = "UDL", Order = 11)]
        public List<UserDesignation> UserDesignation = new List<UserDesignation>();

        [DataMember(Name = "L", Order = 12)]
        public string L { get; set; }

        [DataMember(Name = "M", Order = 13)]
        public string M { get; set; }

        [DataMember(Name = "S", Order = 14)]
        public string S { get; set; }

        [DataMember(Name = "EIVID", Order = 15)]
        public int ExistingInitialVisitId { get; set; }

        [DataMember(Name = "EIVD", Order = 16)]
        public DateTime? ExistingInitialVisitDate { get; set; }

        [DataMember(Name = "AST", Order = 17)]
        public List<CodeDeCode> AleardySavedTabs = new List<CodeDeCode>();

        [DataMember(Name = "Regimen", Order = 18)]
        public List<MultiTableList> Regimen = new List<MultiTableList>();

        [DataMember(Name = "PRef", Order = 20)]
        public List<MultiTableList> PatientReferred = new List<MultiTableList>();

        [DataMember(Name = "RF", Order = 21)]
        public int? ReferredFrom { get; set; }

        [DataMember(Name = "RFO", Order = 22)]
        public string ReferredFromOther { get; set; }

        [DataMember(Name = "MGTS", Order = 25)]
        public int MgtSignature { get; set; }

        [DataMember(Name = "PUR", Order = 26)]
        public List<CodeDeCode> Purpose = new List<CodeDeCode>();

        [DataMember(Name = "LR", Order = 27)]
        public List<LabResult> LabResult = new List<LabResult>();

        [DataMember(Name = "KPL", Order = 26)]
        public List<DBMultiSelectSelcted> KeyPopulationList;

        [DataMember(Name = "DDV", Order = 30)]
        public List<CodeDeCode> DropDownValues = new List<CodeDeCode>();

        [DataMember(Name = "SPN", Order = 32)]
        public List<CodeDeCode> StartPrEPNo = new List<CodeDeCode>();

        [DataMember(Name = "PADH", Order = 32)]
        public List<CodeDeCode> PrEPAdherence = new List<CodeDeCode>();

        [DataMember(Name = "WPF", Order = 33)]
        public List<CodeDeCode> WhyPoorFair = new List<CodeDeCode>();

        [DataMember(Name = "PS", Order = 34)]
        public List<CodeDeCode> PrEPStart = new List<CodeDeCode>();

        [DataMember(Name = "RGMN", Order = 35)]
        public List<CodeDeCode> RegimenCode = new List<CodeDeCode>();

        [DataMember(Name = "REGRESON", Order = 36)]
        public List<CodeDeCode> RegimenReason = new List<CodeDeCode>();

        [DataMember(Name = "APTR", Order = 37)]
        public List<CodeDeCode> AppointmentReason = new List<CodeDeCode>();

        [DataMember(Name = "INIT", Order = 38)]
        public PrEPInitiation objPrEPInition = new PrEPInitiation();

        [DataMember(Name = "CC", Order = 39)]
        public List<CodeDeCode> ChronicCondition = new List<CodeDeCode>();

        [DataMember(Name = "FP", Order = 40)]
        public List<CodeDeCode> FamilyPlanningMethods = new List<CodeDeCode>();

        [DataMember(Name = "PREG", Order = 41)]
        public List<CodeDeCode> PregnancyStatus = new List<CodeDeCode>();

        [DataMember(Name = "RNFP", Order = 42)]
        public List<CodeDeCode> ReasonNotFamilyPlanning = new List<CodeDeCode>();

        [DataMember(Name = "RFA", Order = 43)]
        public List<CodeDeCode> ReasonsForAmenorrohea = new List<CodeDeCode>();

        [DataMember(Name = "VDRL", Order = 44)]
        public List<CodeDeCode> VDRL_RH = new List<CodeDeCode>();

        [DataMember(Name = "OBSGYN", Order = 45)]
        public OBSnGYNHistory OBSnGYNHistory = new OBSnGYNHistory();

        [DataMember(Name = "CDT", Order = 46)]
        public List<MultiSelectTable> CDT = new List<MultiSelectTable>();

        [DataMember(Name = "APT", Order = 46)]
        public List<Appointment> NxtAPT = new List<Appointment>();

       
    }

    [Serializable]
    [DataContract(Name = "PT")]
    public class PrEPTriage
    {

        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "Sch", Order = 5)]
        public int Schedule { get; set; }

        [DataMember(Name = "School", Order = 9)]
        public int School { get; set; }

        [DataMember(Name = "OV", Order = 4)]
        public OrdVisit OrdVisit = new OrdVisit();

        [DataMember(Name = "PV", Order = 6)]
        public PatientVitals PatientVitals = new PatientVitals();

        [DataMember(Name = "HIV", Order = 7)]
        public HIVCare HIVCare = new HIVCare();

        [DataMember(Name = "TS", Order = 8)]
        public int Signature { get; set; }


        [DataMember(Name = "FN", Order = 10)]
        public string FormName { get; set; }

        //[DataMember(Name = "RFO", Order = 11)]
        //public string ReferredFromOther { get; set; }

    }

    [Serializable]
    [DataContract(Name = "HIV")]
    public class HIVCare
    {
        [DataMember(Name = "ARTSD", Order = 12)]
        public DateTime? ARTStartDate { get; set; }
        [DataMember(Name = "ARTTD", Order = 8)]
        public DateTime? ARTTransferInDate { get; set; }
        [DataMember(Name = "ARTTF", Order = 9)]
        public int ARTTransferInFrom { get; set; }
        [DataMember(Name = "HIVCPD", Order = 12)]
        public DateTime? ConfirmHIVPosDate { get; set; }
        [DataMember(Name = "HIVD", Order = 5)]
        public DateTime? DateHIVDiagnosis { get; set; }
        [DataMember(Name = "DIST", Order = 10)]
        public int District { get; set; }
        [DataMember(Name = "HIVED", Order = 4)]
        public DateTime? HIVCareEnrollmentDate { get; set; }
        [DataMember(Name = "HIVDV", Order = 6)]
        public int? HIVDiagnosisVerified { get; set; }
        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }
        [DataMember(Name = "MFL", Order = 14)]
        public int MFLCode { get; set; }
        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }
        [DataMember(Name = "TRAN", Order = 13)]
        public int? TransferIn { get; set; }
        [DataMember(Name = "UID", Order = 11)]
        public int UserId { get; set; }
        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }
    }

    [Serializable]
    [DataContract(Name = "ARTS")]
    public class ARTStart
    {
        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_Pk { get; set; }

        [DataMember(Name = "LID", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VID", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "FLRD", Order = 4)]
        public DateTime? FirstLineRegimenDate { get; set; }

        [DataMember(Name = "FLR", Order = 5)]
        public string FirstLineRegimen { get; set; }

        [DataMember(Name = "CR", Order = 6)]
        public string CurrentRegimen { get; set; }

        [DataMember(Name = "WE", Order = 7)]
        public string Weight { get; set; }

        [DataMember(Name = "HED", Order = 8)]
        public string Height { get; set; }

        [DataMember(Name = "WS", Order = 9)]
        public string WhoStage { get; set; }

        [DataMember(Name = "CD4", Order = 10)]
        public string CD4Count { get; set; }

        [DataMember(Name = "BLVL", Order = 11)]
        public string BaselineViralLoad { get; set; }

        [DataMember(Name = "BLVLD", Order = 12)]
        public DateTime? BaselineViralLoadDate { get; set; }

        [DataMember(Name = "MUAC", Order = 13)]
        public int? MUAC { get; set; }

    }

    [Serializable]
    [DataContract(Name = "PrEPI")]
    public class PrEPInitiation
    {
        [DataMember(Name = "LBID", Order = 1)]
        public int LabID { get; set; }

        [DataMember(Name = "OD", Order = 2)]
        public bool OrderToday { get; set; }

        [DataMember(Name = "HIVR", Order = 3)]
        public int HIVTestResult { get; set; }

        [DataMember(Name = "ISC", Order = 4)]
        public bool IsSampleCollected { get; set; }

        [DataMember(Name = "RCCC", Order = 5)]
        public bool ReferredToCCC { get; set; }

        [DataMember(Name = "PP", Order = 6)]
        public bool PreviousPrep { get; set; }

        [DataMember(Name = "WTSP", Order = 7)]
        public bool WillingToStartPrep { get; set; }

        [DataMember(Name = "IWTSPN", Order = 8)]
        public int IfWillingToStartPrepNo { get; set; }

        [DataMember(Name = "IWTSPY", Order = 9)]
        public int IfWillingToStartPrepYes { get; set; }

        [DataMember(Name = "PA", Order = 10)]
        public int PrepAdherence { get; set; }

        [DataMember(Name = "PILC", Order = 11)]
        public int PillCount { get; set; }

        [DataMember(Name = "WPFS", Order = 12)]
        public int WhyPoorFairSingle { get; set; }

        [DataMember(Name = "PEPS", Order = 13)]
        public int PrepStatus { get; set; }

        [DataMember(Name = "PR", Order = 14)]
        public int PrepRegimen { get; set; }

        [DataMember(Name = "PRDR", Order = 15)]
        public int PrepRegimenDiscountinueReason { get; set; }

        [DataMember(Name = "CI", Order = 16)]
        public bool CondomIssued { get; set; }

        [DataMember(Name = "ACD", Order = 17)]
        public bool AdherenceCounsellingDone { get; set; }

        [DataMember(Name = "PPT", Order = 18)]
        public bool PrescribedPrepToday { get; set; }

        [DataMember(Name = "WP", Order = 19)]
        public string WorkPlan { get; set; }

        [DataMember(Name = "VId", Order = 20)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "Ptn_Pk", Order = 21)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 22)]
        public int LocationId { get; set; }

        [DataMember(Name = "HBV", Order = 23)]
        public int HepatitisBVaccine { get; set; }

        [DataMember(Name = "LRINI", Order = 27)]
        public List<LabResult> LabResultInitiation = new List<LabResult>();

        [DataMember(Name = "NAPPT", Order = 28)]
        public List<Appointment> NextAppointment = new List<Appointment>();
    }

}
