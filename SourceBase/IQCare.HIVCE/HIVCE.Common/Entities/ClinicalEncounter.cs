using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HIVCE.Common.Entities
{

    [Serializable]
    [DataContract]
    public class CodeDeCodeTables
    {
        [DataMember(Name = "CId", Order = 1)]
        public int CodeId { get; set; }
        [DataMember(Name = "CN", Order = 2)]
        public String CodeName { get; set; }
        [DataMember(Name = "DId", Order = 3)]
        public int DeCodeId { get; set; }
        [DataMember(Name = "DN", Order = 4)]
        public string DeCodeName { get; set; }
    }

    [Serializable]
    [DataContract]
    public class District
    {
        [DataMember(Name = "Id", Order = 1)]
        public int ID { get; set; }
        [DataMember(Name = "Name", Order = 2)]
        public String Name { get; set; }
        [DataMember(Name = "SrNo", Order = 3)]
        public int? SrNo { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Facility
    {
        [DataMember(Name = "Id", Order = 1)]
        public int ID { get; set; }
        [DataMember(Name = "Name", Order = 2)]
        public String Name { get; set; }
        [DataMember(Name = "SrNo", Order = 3)]
        public int? SrNo { get; set; }
        [DataMember(Name = "MFL", Order = 4)]
        public string MFLCode { get; set; }
        [DataMember(Name = "CId", Order = 5)]
        public int? CountryId { get; set; }
        [DataMember(Name = "CN", Order = 6)]
        public string CountryName { get; set; }

    }

    [Serializable]
    [DataContract]
    public class UserDesignation
    {
        [DataMember(Name = "UserID", Order = 1)]
        public int UserID { get; set; }
        [DataMember(Name = "UserName", Order = 2)]
        public String UserName { get; set; }
        [DataMember(Name = "Designation", Order = 3)]
        public string Designation { get; set; }
        [DataMember(Name = "DeleteFlag", Order = 4)]
        public int? DeleteFlag { get; set; }
    }

    [Serializable]
    [DataContract(Name = "CE")]
    public class ClinicalEncounter
    {

        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VT", Order = 3)]
        public List<CodeDeCodeTables> VisitTypes = new List<CodeDeCodeTables>();

        [DataMember(Name = "CRS", Order = 4)]
        public List<CodeDeCodeTables> ContractRelations = new List<CodeDeCodeTables>();

        [DataMember(Name = "DL", Order = 5)]
        public List<District> Districts = new List<District>();

        [DataMember(Name = "FL", Order = 6)]
        public List<Facility> FacilityList = new List<Facility>();

        [DataMember(Name = "OV", Order = 7)]
        public OrdVisit OrdVisit = new OrdVisit();

        [DataMember(Name = "IE", Order = 8)]
        public InitialEvaluation InitialEvaluation = new InitialEvaluation();

        [DataMember(Name = "PV", Order = 9)]
        public PatientVitals PatientVitals = new PatientVitals();

        [DataMember(Name = "HIV", Order = 10)]
        public HIVCare HIVCare = new HIVCare();

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
        public List<CodeDeCodeTables> AleardySavedTabs = new List<CodeDeCodeTables>();

        [DataMember(Name = "Regimen", Order = 18)]
        public List<MultiTableList> Regimen = new List<MultiTableList>();

        [DataMember(Name = "WHOS", Order = 19)]
        public List<MultiTableList> WHOStage = new List<MultiTableList>();

        [DataMember(Name = "PRef", Order = 20)]
        public List<MultiTableList> PatientReferred = new List<MultiTableList>();

        [DataMember(Name = "RF", Order = 21)]
        public int? ReferredFrom { get; set; }

        [DataMember(Name = "RFO", Order = 22)]
        public string ReferredFromOther { get; set; }

        [DataMember(Name = "AD", Order = 23)]
        public DateTime? AppDate { get; set; }

        [DataMember(Name = "AR", Order = 24)]
        public string AppReason { get; set; }

        [DataMember(Name = "MGTS", Order = 25)]
        public int MgtSignature { get; set; }

        [DataMember(Name = "PUR", Order = 26)]
        public List<CodeDeCodeTables> Purpose = new List<CodeDeCodeTables>();
    }

    [Serializable]
    [DataContract(Name = "CET")]
    public class Triage
    {

        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "OV", Order = 4)]
        public OrdVisit OrdVisit = new OrdVisit();

        [DataMember(Name = "IE", Order = 5)]
        public InitialEvaluation InitialEvaluation = new InitialEvaluation();

        [DataMember(Name = "PV", Order = 6)]
        public PatientVitals PatientVitals = new PatientVitals();

        [DataMember(Name = "HIV", Order = 7)]
        public HIVCare HIVCare = new HIVCare();

        [DataMember(Name = "TS", Order = 8)]
        public int Signature { get; set; }

        [DataMember(Name = "FN", Order = 9)]
        public string FormName { get; set; }

        [DataMember(Name = "RF", Order = 10)]
        public int ReferredFrom { get; set; }

        [DataMember(Name = "RFO", Order = 11)]
        public string ReferredFromOther { get; set; }

    }

    [Serializable]
    [DataContract(Name = "CETST")]
    public class TSTriage
    {

        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "OV", Order = 4)]
        public OrdVisit OrdVisit = new OrdVisit();

        [DataMember(Name = "FN", Order = 5)]
        public string FormName { get; set; }

        [DataMember(Name = "NAD", Order = 6)]
        public DateTime? NextAppointmentDate { get; set; }

        [DataMember(Name = "Purpose", Order = 7)]
        public int Purpose { get; set; }

        [DataMember(Name = "MGTS", Order = 8)]
        public int MgtSignature { get; set; }

    }

    [Serializable]
    [DataContract(Name = "OV")]
    public class OrdVisit
    {
        [DataMember(Name = "VId", Order = 1)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_Pk { get; set; }

        [DataMember(Name = "LId", Order = 3)]
        public int LocationId { get; set; }

        [DataMember(Name = "VD", Order = 4)]
        public DateTime? VisitDate { get; set; }

        [DataMember(Name = "VTId", Order = 5)]
        public int VisitTypeId { get; set; }

        [DataMember(Name = "CD", Order = 6)]
        public DateTime? CreatedDate { get; set; }

        [DataMember(Name = "UD", Order = 7)]
        public DateTime? UpdatedDate { get; set; }

        [DataMember(Name = "TOV", Order = 8)]
        public int TypeOfVisit { get; set; }

        [DataMember(Name = "SG", Order = 8)]
        public int Signature { get; set; }
    }

    [Serializable]
    [DataContract(Name = "IE")]
    public class InitialEvaluation
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }

        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_Pk { get; set; }

        [DataMember(Name = "VID", Order = 3)]
        public int Visit_Pk { get; set; }

        [DataMember(Name = "LId", Order = 4)]
        public int LocationId { get; set; }

        [DataMember(Name = "HIVSG", Order = 5)]
        public string HIVSupportgroup { get; set; }

        [DataMember(Name = "HIVSGM", Order = 6)]
        public string HIVSupportGroupMembership { get; set; }

        [DataMember(Name = "Menarche", Order = 7)]
        public string Menarche { get; set; }

        [DataMember(Name = "ACG", Order = 8)]
        public string AccompaniedByCaregiver { get; set; }

        [DataMember(Name = "CGR", Order = 9)]
        public string CaregiverRelationship { get; set; }

    }

    [Serializable]
    [DataContract(Name = "PV")]
    public class PatientVitals
    {
        [DataMember(Name = "VId", Order = 1)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "BPD", Order = 2)]
        public string BPDiastolic { get; set; }

        [DataMember(Name = "BPS", Order = 3)]
        public string BPSystolic { get; set; }

        [DataMember(Name = "TEMP", Order = 4)]
        public string TEMP { get; set; }

        [DataMember(Name = "RR", Order = 5)]
        public string RR { get; set; }

        [DataMember(Name = "HR", Order = 6)]
        public string HR { get; set; }

        [DataMember(Name = "HC", Order = 7)]
        public string HeadCircumference { get; set; }

        [DataMember(Name = "Height", Order = 8)]
        public string Height { get; set; }

        [DataMember(Name = "Weight", Order = 9)]
        public string Weight { get; set; }

        [DataMember(Name = "MUAC", Order = 10)]
        public string MUAC { get; set; }

        [DataMember(Name = "WFA", Order = 11)]
        public string Weightforage { get; set; }

        [DataMember(Name = "WFH", Order = 12)]
        public string Weightforheight { get; set; }

        [DataMember(Name = "BMIz", Order = 13)]
        public string BMIz { get; set; }

        [DataMember(Name = "NC", Order = 14)]
        public string NurseComments { get; set; }

        [DataMember(Name = "SP", Order = 15)]
        public string SPO2 { get; set; }

    }

    [Serializable]
    [DataContract(Name = "HIV")]
    public class HIVCare
    {
        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "HIVED", Order = 4)]
        public DateTime? HIVCareEnrollmentDate { get; set; }

        [DataMember(Name = "HIVD", Order = 5)]
        public DateTime? DateHIVDiagnosis { get; set; }

        [DataMember(Name = "HIVDV", Order = 6)]
        public int? HIVDiagnosisVerified { get; set; }

        //[DataMember(Name = "HIVCW", Order = 7)]
        //public string HIVCareWhere { get; set; }

        [DataMember(Name = "ARTTD", Order = 8)]
        public DateTime? ARTTransferInDate { get; set; }

        [DataMember(Name = "ARTTF", Order = 9)]
        public int ARTTransferInFrom { get; set; }

        [DataMember(Name = "DIST", Order = 10)]
        public int District { get; set; }

        [DataMember(Name = "UID", Order = 11)]
        public int UserId { get; set; }

        [DataMember(Name = "ARTSD", Order = 12)]
        public DateTime? ARTStartDate { get; set; }

        [DataMember(Name = "TRAN", Order = 13)]
        public int? TransferIn { get; set; }

        [DataMember(Name = "HIVCPD", Order = 12)]
        public DateTime? ConfirmHIVPosDate { get; set; }

        [DataMember(Name = "ARTS", Order = 13)]
        public ARTStart ARTStart = new ARTStart();

        [DataMember(Name = "MFL", Order = 14)]
        public int MFLCode { get; set; }
    }

    [Serializable]
    [DataContract(Name = "CEPC")]
    public class PresentingComplaint
    {

        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "CC", Order = 3)]
        public List<CodeDeCodeTables> ChronicCondition = new List<CodeDeCodeTables>();

        [DataMember(Name = "PC", Order = 4)]
        public List<CodeDeCodeTables> PresentingComplaints = new List<CodeDeCodeTables>();

        [DataMember(Name = "PS", Order = 5)]
        public List<CodeDeCodeTables> PregnancyStatus = new List<CodeDeCodeTables>();

        [DataMember(Name = "FPM", Order = 6)]
        public List<CodeDeCodeTables> FamilyPlanningMethods = new List<CodeDeCodeTables>();

        [DataMember(Name = "RNFP", Order = 7)]
        public List<CodeDeCodeTables> ReasonNotFamilyPlanning = new List<CodeDeCodeTables>();

        [DataMember(Name = "RFA", Order = 8)]
        public List<CodeDeCodeTables> ReasonsForAmenorrohea = new List<CodeDeCodeTables>();

        [DataMember(Name = "OBSGYN", Order = 9)]
        public OBSnGYNHistory OBSnGYNHistory = new OBSnGYNHistory();

        [DataMember(Name = "PCT", Order = 10)]
        public List<MultiSelectTable> PCT = new List<MultiSelectTable>();

        [DataMember(Name = "CDT", Order = 11)]
        public List<MultiSelectTable> CDT = new List<MultiSelectTable>();

        [DataMember(Name = "VDRL_RH", Order = 12)]
        public List<CodeDeCodeTables> VDRL_RH = new List<CodeDeCodeTables>();

    }

    [Serializable]
    [DataContract(Name = "OBSnGYN")]
    public class OBSnGYNHistory
    {
        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "Menarche", Order = 4)]
        public int Menarche { get; set; }

        [DataMember(Name = "HPVV", Order = 5)]
        public int HPVvaccine { get; set; }

        [DataMember(Name = "CCS", Order = 6)]
        public int CervicalCancerScreened { get; set; }

        [DataMember(Name = "CCSD", Order = 7)]
        public DateTime? CervicalCancerScreenedDate { get; set; }

        [DataMember(Name = "LMP", Order = 8)]
        public DateTime? LMP { get; set; }

        [DataMember(Name = "Pregnant", Order = 9)]
        public int Pregnant { get; set; }

        [DataMember(Name = "EDD", Order = 10)]
        public DateTime? EDD { get; set; }

        [DataMember(Name = "DOD", Order = 11)]
        public DateTime? DateofDelivery { get; set; }

        [DataMember(Name = "DOIA", Order = 12)]
        public DateTime? DateofInducedAbortion { get; set; }

        [DataMember(Name = "DOM", Order = 13)]
        public DateTime? DateofMiscarriage { get; set; }

        [DataMember(Name = "Amenorrhoea", Order = 14)]
        public int Amenorrhoea { get; set; }

        [DataMember(Name = "BS", Order = 15)]
        public int BreastStatus { get; set; }

        [DataMember(Name = "FPM", Order = 16)]
        public int FamilyPlanningMethod { get; set; }

        [DataMember(Name = "FPS", Order = 17)]
        public int FamilyPlanningStatus { get; set; }

        [DataMember(Name = "NFP", Order = 18)]
        public int NoFamilyPlanning { get; set; }

        [DataMember(Name = "Gravidae", Order = 19)]
        public int Gravidae { get; set; }

        [DataMember(Name = "Parity", Order = 20)]
        public int Parity { get; set; }

        [DataMember(Name = "ANC", Order = 27)]
        public int ANC { get; set; }

        [DataMember(Name = "VDRLTD", Order = 21)]
        public DateTime? VDRLTestDate { get; set; }

        [DataMember(Name = "VDRLTR", Order = 22)]
        public int? VDRLTestResult { get; set; }

        [DataMember(Name = "HbTD", Order = 23)]
        public DateTime? HbTestDate { get; set; }

        [DataMember(Name = "HbTR", Order = 24)]
        public int? HbTestResult { get; set; }

        [DataMember(Name = "RhesusTD", Order = 25)]
        public DateTime? RhesusTestDate { get; set; }

        [DataMember(Name = "RhesusTR", Order = 26)]
        public int? RhesusTestResult { get; set; }

        [DataMember(Name = "OPC", Order = 27)]
        public string OtherPresentingComplaints { get; set; }

        [DataMember(Name = "PAD", Order = 27)]
        public string PreviousAdmissionDiagnosis { get; set; }

    }

    [Serializable]
    [DataContract(Name = "MST")]
    public class MultiSelectTable
    {
        [DataMember(Name = "Ptn_pk", Order = 1)]
        public int Ptn_pk { get; set; }
        [DataMember(Name = "VId", Order = 2)]
        public int Visit_Pk { get; set; }
        [DataMember(Name = "ValId", Order = 3)]
        public int ValueID { get; set; }
        [DataMember(Name = "FN", Order = 4)]
        public string FieldName { get; set; }
        [DataMember(Name = "OSD", Order = 5)]
        public DateTime? OnSetDate { get; set; }
        [DataMember(Name = "NF", Order = 6)]
        public int? NumericField { get; set; }
        [DataMember(Name = "NT", Order = 7)]
        public string Notes { get; set; }
        [DataMember(Name = "VNT", Order = 8)]
        public string VNotes { get; set; }
        [DataMember(Name = "ValN", Order = 9)]
        public string ValueName { get; set; }
    }

    [Serializable]
    [DataContract(Name = "MTL")]
    public class MultiTableList
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "NM", Order = 2)]
        public string Name { get; set; }
        [DataMember(Name = "CNM", Order = 3)]
        public string CName { get; set; }
    }

    [Serializable]
    [DataContract(Name = "DRM")]
    public class DrugRegimen
    {
        [DataMember(Name = "DID", Order = 1)]
        public int DrugId { get; set; }
        [DataMember(Name = "DN", Order = 2)]
        public string DrugName { get; set; }
        [DataMember(Name = "DTID", Order = 3)]
        public int DrugTypeId { get; set; }
        [DataMember(Name = "DTNM", Order = 4)]
        public string DrugTypeName { get; set; }
        [DataMember(Name = "GA", Order = 5)]
        public string GenericAbbrevation { get; set; }
        [DataMember(Name = "IC", Order = 6)]
        public int? ItemCode { get; set; }
    }

    [Serializable]
    [DataContract(Name = "TS")]
    public class TannersStaging
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_Pk { get; set; }
        [DataMember(Name = "LID", Order = 3)]
        public int LocationId { get; set; }
        [DataMember(Name = "LSO", Order = 4)]
        public DateTime? LastScreenedOn { get; set; }
        [DataMember(Name = "NSO", Order = 5)]
        public DateTime? NextScreenedOn { get; set; }
        [DataMember(Name = "SD", Order = 6)]
        public DateTime? StagingDate { get; set; }
        [DataMember(Name = "Gender", Order = 7)]
        public int? Gender { get; set; }
        [DataMember(Name = "BOG", Order = 8)]
        public int BreastsOrGenitals { get; set; }
        [DataMember(Name = "PH", Order = 9)]
        public int PublicHair { get; set; }
        [DataMember(Name = "UID", Order = 10)]
        public int UserId { get; set; }
        [DataMember(Name = "DF", Order = 11)]
        public int DeleteFlag { get; set; }
        [DataMember(Name = "CD", Order = 12)]
        public DateTime CreateDate { get; set; }
        [DataMember(Name = "UD", Order = 13)]
        public DateTime UpdateDate { get; set; }
        [DataMember(Name = "VID", Order = 14)]
        public int Visit_Pk { get; set; }
    }

    [Serializable]
    [DataContract(Name = "PART")]
    public class PriorART
    {
        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_Pk { get; set; }

        [DataMember(Name = "LID", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VID", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "PID", Order = 4)]
        public int? PusposeId { get; set; }

        [DataMember(Name = "Regimen", Order = 5)]
        public string Regimen { get; set; }

        [DataMember(Name = "LDU", Order = 6)]
        public DateTime? LastDateUsed { get; set; }

        [DataMember(Name = "UID", Order = 7)]
        public int Userid { get; set; }

        [DataMember(Name = "CD", Order = 8)]
        public DateTime? CreateDate { get; set; }

        [DataMember(Name = "UD", Order = 9)]
        public DateTime? UpdateDate { get; set; }

        [DataMember(Name = "RId", Order = 10)]
        public int? Regimenid { get; set; }
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
    [DataContract(Name = "AD")]
    public class AllergiesDetail
    {
        [DataMember(Name = "id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_Pk { get; set; }
        [DataMember(Name = "ATD", Order = 3)]
        public string AllergyTypeDesc { get; set; }
        [DataMember(Name = "ATID", Order = 4)]
        public string AllergyTypeID { get; set; }
        [DataMember(Name = "AllerDesc", Order = 5)]
        public string AllergenDesc { get; set; }
        [DataMember(Name = "Allergen", Order = 6)]
        public string Allergen { get; set; }
        [DataMember(Name = "OAller", Order = 7)]
        public string OtherAllergen { get; set; }
        [DataMember(Name = "TR", Order = 8)]
        public string TypeReaction { get; set; }
        [DataMember(Name = "SDesc", Order = 9)]
        public string SeverityDesc { get; set; }
        [DataMember(Name = "STID", Order = 10)]
        public int? SevrityTypeID { get; set; }
        [DataMember(Name = "DAller", Order = 11)]
        public DateTime? DateAllergy { get; set; }
        [DataMember(Name = "Comment", Order = 12)]
        public string Comment { get; set; }
        [DataMember(Name = "UID", Order = 14)]
        public int? UserID { get; set; }
        [DataMember(Name = "DF", Order = 15)]
        public int? DeleteFlag { get; set; }
        [DataMember(Name = "CD", Order = 16)]
        public DateTime? CreateDate { get; set; }
        [DataMember(Name = "UD", Order = 17)]
        public DateTime? UpdateDate { get; set; }
    }

    [Serializable]
    [DataContract(Name = "FD")]
    public class FamilyDetail
    {
        [DataMember(Name = "id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_Pk { get; set; }

        [DataMember(Name = "RFN", Order = 3)]
        public string RFirstName { get; set; }
        [DataMember(Name = "RLN", Order = 4)]
        public string RLastName { get; set; }
        [DataMember(Name = "SID", Order = 5)]
        public int SexId { get; set; }
        [DataMember(Name = "SDes", Order = 6)]
        public string SexDesc { get; set; }
        [DataMember(Name = "AY", Order = 7)]
        public int? AgeYear { get; set; }
        [DataMember(Name = "AM", Order = 8)]
        public int? AgeMonth { get; set; }
        [DataMember(Name = "RD", Order = 9)]
        public DateTime? RelationshipDate { get; set; }
        [DataMember(Name = "RID", Order = 10)]
        public int? RelationshipId { get; set; }

        [DataMember(Name = "RDes", Order = 11)]
        public string RelationshipDesc { get; set; }

        [DataMember(Name = "RNo", Order = 12)]
        public string RegistrationNo { get; set; }

        [DataMember(Name = "FNO", Order = 13)]
        public string FileNo { get; set; }
        [DataMember(Name = "FNO1", Order = 14)]
        public string FileNo1 { get; set; }
        [DataMember(Name = "HIVSID", Order = 15)]
        public int? HIVStatusId { get; set; }

        [DataMember(Name = "LHIVTD", Order = 16)]
        public DateTime? LastHIVTestDate { get; set; }

        [DataMember(Name = "HIVSDes", Order = 17)]
        public string HIVStatusDesc { get; set; }

        [DataMember(Name = "HIVCSID", Order = 18)]
        public int? HIVCareStatusId { get; set; }

        [DataMember(Name = "HIVCSDes", Order = 19)]
        public string HIVCareStatusDesc { get; set; }

        [DataMember(Name = "IsRegistered", Order = 20)]
        public string IsRegistered { get; set; }

        [DataMember(Name = "RefId", Order = 21)]
        public int? Referenceid { get; set; }

    }

    [Serializable]
    [DataContract(Name = "PWP")]
    public class PWPDetails
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_Pk { get; set; }
        [DataMember(Name = "VID", Order = 3)]
        public int Visit_id { get; set; }
        [DataMember(Name = "LID", Order = 4)]
        public int LocationID { get; set; }
        [DataMember(Name = "UID", Order = 5)]
        public int? UserId { get; set; }
        [DataMember(Name = "CD", Order = 6)]
        public DateTime? CreateDate { get; set; }
        [DataMember(Name = "UD", Order = 7)]
        public DateTime? UpdateDate { get; set; }
        [DataMember(Name = "SA6M", Order = 8)]
        public int? SexuallyActiveLast6Months { get; set; }
        [DataMember(Name = "SO", Order = 9)]
        public int? SexualOrientation { get; set; }
        [DataMember(Name = "HIVSSP", Order = 10)]
        public int? HIVstatusOfsexualPartner { get; set; }
        [DataMember(Name = "P6M", Order = 11)]
        public int? PartnersIn6Months { get; set; }
        [DataMember(Name = "PG", Order = 12)]
        public int? PartnersGender { get; set; }
    }


    [Serializable]
    [DataContract(Name = "GridData")]
    public class GridData
    {
        [DataMember(Name = "Ptn_pk", Order = 1)]
        public int Ptn_pk { get; set; }
        [DataMember(Name = "LID", Order = 2)]
        public int LocationID { get; set; }
        [DataMember(Name = "VID", Order = 3)]
        public int Visit_pk { get; set; }
        [DataMember(Name = "Section", Order = 4)]
        public string Section { get; set; }
        [DataMember(Name = "TTID", Order = 5)]
        public int TypeofTestId { get; set; }
        [DataMember(Name = "TT", Order = 6)]
        public string TypeofTest { get; set; }
        [DataMember(Name = "RID", Order = 7)]
        public int ResultId { get; set; }
        [DataMember(Name = "Result", Order = 8)]
        public string Result { get; set; }
        [DataMember(Name = "DT", Order = 9)]
        public DateTime Date { get; set; }
        [DataMember(Name = "CM", Order = 10)]
        public string Comments { get; set; }
        [DataMember(Name = "IsAchieved", Order = 11)]
        public int? IsAchieved { get; set; }

    }

    [Serializable]
    [DataContract(Name = "AddHX")]
    public class AddtionalHx
    {

        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "LTM", Order = 4)]
        public int LongTermMedications { get; set; }

        [DataMember(Name = "Exami", Order = 5)]
        public string Examinations { get; set; }

        [DataMember(Name = "HMeds", Order = 6)]
        public int HerbalMeds { get; set; }

        [DataMember(Name = "RFHMed", Order = 7)]
        public string ReasonForHerbalMedicine { get; set; }

        [DataMember(Name = "MS", Order = 8)]
        public List<MultiTableList> MilestoneAssessed = new List<MultiTableList>();

        [DataMember(Name = "IP", Order = 9)]
        public List<MultiTableList> ImmunisationPeriod = new List<MultiTableList>();

        [DataMember(Name = "IG", Order = 10)]
        public List<MultiTableList> ImmunisationGiven = new List<MultiTableList>();

        [DataMember(Name = "IS", Order = 11)]
        public List<MultiTableList> ImmunisationStatus = new List<MultiTableList>();

        [DataMember(Name = "LTML", Order = 12)]
        public List<MultiTableList> LongTermMedicationsList = new List<MultiTableList>();

        [DataMember(Name = "PARTP", Order = 13)]
        public List<MultiTableList> PriorARTPurpose = new List<MultiTableList>();

        [DataMember(Name = "WHOS", Order = 14)]
        public List<MultiTableList> WHOStage = new List<MultiTableList>();

        [DataMember(Name = "ENVA", Order = 15)]
        public List<MultiTableList> environmentalAllergen = new List<MultiTableList>();

        [DataMember(Name = "Severity", Order = 16)]
        public List<MultiTableList> Severity = new List<MultiTableList>();

        [DataMember(Name = "AT", Order = 17)]
        public List<MultiTableList> AllergyType = new List<MultiTableList>();

        [DataMember(Name = "HIVS", Order = 18)]
        public List<MultiTableList> HIVStatus = new List<MultiTableList>();

        [DataMember(Name = "Gender", Order = 19)]
        public List<MultiTableList> Gender = new List<MultiTableList>();

        [DataMember(Name = "SO", Order = 20)]
        public List<MultiTableList> SexualOrientation = new List<MultiTableList>();

        [DataMember(Name = "PHIVS", Order = 21)]
        public List<MultiTableList> PartnerHIVStatus = new List<MultiTableList>();

        [DataMember(Name = "HS", Order = 22)]
        public List<MultiTableList> HighRisk = new List<MultiTableList>();

        [DataMember(Name = "DRS", Order = 23)]
        public List<DrugRegimen> DrugRegimens = new List<DrugRegimen>();

        [DataMember(Name = "Allergen", Order = 24)]
        public List<MultiTableList> Allergen = new List<MultiTableList>();

        [DataMember(Name = "HIVCS", Order = 25)]
        public List<MultiTableList> HivCareStatus = new List<MultiTableList>();

        [DataMember(Name = "RT", Order = 26)]
        public List<MultiTableList> RelationshipType = new List<MultiTableList>();

        [DataMember(Name = "TS", Order = 27)]
        public List<TannersStaging> TannersStaging = new List<TannersStaging>();

        [DataMember(Name = "PART", Order = 28)]
        public List<PriorART> PriorART = new List<PriorART>();

        //[DataMember(Name = "ARTS", Order = 29)]
        //public List<ARTStart> ARTStart = new List<ARTStart>();

        [DataMember(Name = "AllergiesD", Order = 30)]
        public List<AllergiesDetail> AllergiesDetail = new List<AllergiesDetail>();

        [DataMember(Name = "FD", Order = 31)]
        public List<FamilyDetail> FamilyDetails = new List<FamilyDetail>();

        [DataMember(Name = "SH", Order = 32)]
        public List<PWPDetails> SexualHistory = new List<PWPDetails>();

        [DataMember(Name = "LTMR", Order = 33)]
        public List<MultiTableList> LongTermMedicationReason = new List<MultiTableList>();

        [DataMember(Name = "FOODA", Order = 34)]
        public List<MultiTableList> foodAllergen = new List<MultiTableList>();

        [DataMember(Name = "LTMnHR", Order = 35)]
        public List<MultiSelectTable> LTMnHR = new List<MultiSelectTable>();

        [DataMember(Name = "MnI", Order = 36)]
        public List<GridData> MilestoneImmunization = new List<GridData>();

        [DataMember(Name = "RC", Order = 37)]
        public List<CodeDeCodeTables> RegimenCodes = new List<CodeDeCodeTables>();

        [DataMember(Name = "RCV", Order = 38)]
        public int RegimenCodeValue { get; set; }

    }


    #region AdditionakHX
    [Serializable]
    [DataContract]
    public class MH
    {

        [DataMember(Name = "MA")]
        public string MA { get; set; }

        [DataMember(Name = "MAT")]
        public string MAT { get; set; }

        [DataMember(Name = "DA")]
        public DateTime DA { get; set; }

        [DataMember(Name = "displayDate")]
        public string displayDate { get; set; }

        [DataMember(Name = "IsA")]
        public int IsA { get; set; }

        [DataMember(Name = "IsAval")]
        public string IsAval { get; set; }

        [DataMember(Name = "ST")]
        public string ST { get; set; }

        [DataMember(Name = "STA")]
        public string STA { get; set; }

        [DataMember(Name = "CM")]
        public string CM { get; set; }
    }

    [Serializable]
    [DataContract]
    public class IH
    {

        [DataMember(Name = "IP")]
        public string IP { get; set; }

        [DataMember(Name = "IPT")]
        public string IPT { get; set; }

        [DataMember(Name = "IG")]
        public string IG { get; set; }

        [DataMember(Name = "IGT")]
        public string IGT { get; set; }

        [DataMember(Name = "DT")]
        public DateTime DT { get; set; }

        [DataMember(Name = "displayDate")]
        public string displayDate { get; set; }
    }

    [Serializable]
    [DataContract]
    public class TSH
    {

        [DataMember(Name = "SD")]
        public DateTime SD { get; set; }

        [DataMember(Name = "displayDate")]
        public string displayDate { get; set; }

        [DataMember(Name = "Gender")]
        public string Gender { get; set; }

        [DataMember(Name = "BOG")]
        public string BOG { get; set; }

        [DataMember(Name = "PH")]
        public string PH { get; set; }
    }

    [Serializable]
    [DataContract]
    public class CLTM
    {

        [DataMember(Name = "FN")]
        public string FN { get; set; }

        [DataMember(Name = "ValId")]
        public string ValId { get; set; }

        [DataMember(Name = "ValT")]
        public string ValT { get; set; }

        [DataMember(Name = "OSD")]
        public DateTime OSD { get; set; }

        [DataMember(Name = "NT")]
        public string NT { get; set; }

        [DataMember(Name = "NTT")]
        public string NTT { get; set; }

        [DataMember(Name = "displayDate")]
        public string displayDate { get; set; }

        [DataMember(Name = "VIDON")]
        public string ValIdOtherNote { get; set; }
    }

    [Serializable]
    [DataContract]
    public class PAH
    {

        [DataMember(Name = "PID")]
        public string PID { get; set; }

        [DataMember(Name = "PT")]
        public string PT { get; set; }

        [DataMember(Name = "RId")]
        public string RId { get; set; }

        [DataMember(Name = "RT")]
        public string RT { get; set; }

        [DataMember(Name = "Regimen")]
        public string Regimen { get; set; }

        [DataMember(Name = "LDU")]
        public DateTime LDU { get; set; }

        [DataMember(Name = "displayDate")]
        public string displayDate { get; set; }
    }

    [Serializable]
    [DataContract]
    public class AllerH
    {

        [DataMember(Name = "ATID")]
        public string ATID { get; set; }

        [DataMember(Name = "ATD")]
        public string ATD { get; set; }

        [DataMember(Name = "Allergen")]
        public string Allergen { get; set; }

        [DataMember(Name = "AllerDesc")]
        public string AllerDesc { get; set; }

        [DataMember(Name = "AllerTID")]
        public string AllerTID { get; set; }

        [DataMember(Name = "OAller")]
        public string OAller { get; set; }

        [DataMember(Name = "STID")]
        public string STID { get; set; }

        [DataMember(Name = "SDesc")]
        public string SDesc { get; set; }

        [DataMember(Name = "DAller")]
        public DateTime DAller { get; set; }

        [DataMember(Name = "displayDAller")]
        public string displayDAller { get; set; }

        [DataMember(Name = "TR")]
        public string TR { get; set; }

        [DataMember(Name = "Comment")]
        public string Comment { get; set; }
    }

    [Serializable]
    [DataContract]
    public class HIVFM
    {
        [DataMember(Name = "id")]
        public string id { get; set; }

        [DataMember(Name = "RE")]
        public string RE { get; set; }

        [DataMember(Name = "RET")]
        public string RET { get; set; }

        [DataMember(Name = "FN")]
        public string FN { get; set; }

        [DataMember(Name = "LN")]
        public string LN { get; set; }

        [DataMember(Name = "Sex")]
        public string Sex { get; set; }

        [DataMember(Name = "SexT")]
        public string SexT { get; set; }

        [DataMember(Name = "Age")]
        public string Age { get; set; }

        [DataMember(Name = "RT")]
        public string RT { get; set; }

        [DataMember(Name = "RTT")]
        public string RTT { get; set; }

        [DataMember(Name = "HS")]
        public string HS { get; set; }

        [DataMember(Name = "HST")]
        public string HST { get; set; }

        [DataMember(Name = "LHTD")]
        public DateTime? LHTD { get; set; }

        [DataMember(Name = "diaplayLHTD")]
        public string diaplayLHTD { get; set; }

        [DataMember(Name = "HCS")]
        public string HCS { get; set; }

        [DataMember(Name = "HCST")]
        public string HCST { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DBAdditionalHx
    {

        [DataMember(Name = "Ptn_Pk")]
        public int Ptn_Pk { get; set; }

        [DataMember(Name = "LId")]
        public int LocationId { get; set; }

        [DataMember(Name = "VId")]
        public int Visit_Id { get; set; }

        [DataMember(Name = "MH")]
        public List<MH> MH = new List<MH>();

        [DataMember(Name = "IH")]
        public List<IH> IH = new List<IH>();

        [DataMember(Name = "NHN")]
        public string NHN { get; set; }

        [DataMember(Name = "TSLS")]
        public DateTime? TSLS { get; set; }

        [DataMember(Name = "TSNS")]
        public DateTime? TSNS { get; set; }

        [DataMember(Name = "TSH")]
        public List<TSH> TSH = new List<TSH>();

        [DataMember(Name = "CLTMF")]
        public int CLTMF { get; set; }

        [DataMember(Name = "CLTM")]
        public List<CLTM> CLTM = new List<CLTM>();

        [DataMember(Name = "PAMF")]
        public int PAMF { get; set; }

        [DataMember(Name = "PAMN")]
        public string PAMN { get; set; }

        [DataMember(Name = "PAF")]
        public int PAF { get; set; }

        [DataMember(Name = "PAH")]
        public List<PAH> PAH = new List<PAH>();

        [DataMember(Name = "ASAFRD")]
        public DateTime? ASAFRD { get; set; }

        [DataMember(Name = "ASAFR")]
        public string ASAFR { get; set; }

        [DataMember(Name = "ASAFW")]
        public string ASAFW { get; set; }

        [DataMember(Name = "ASAFH")]
        public string ASAFH { get; set; }

        [DataMember(Name = "ASAFBMI")]
        public string ASAFBMI { get; set; }

        [DataMember(Name = "ASAFWS")]
        public string ASAFWS { get; set; }

        [DataMember(Name = "AllerHF")]
        public int AllerHF { get; set; }

        [DataMember(Name = "AllerH")]
        public List<AllerH> AllerH = new List<AllerH>();

        [DataMember(Name = "HIVFMF")]
        public int HIVFMF { get; set; }

        [DataMember(Name = "HIVFM")]
        public List<HIVFM> HIVFM = new List<HIVFM>();

        [DataMember(Name = "SHF")]
        public int SHF { get; set; }

        [DataMember(Name = "PNO")]
        public string PNO { get; set; }

        [DataMember(Name = "PS")]
        public string PS { get; set; }

        [DataMember(Name = "PG")]
        public string PG { get; set; }

        [DataMember(Name = "SO")]
        public string SO { get; set; }

        [DataMember(Name = "HRB")]
        public List<HRB> HRB = new List<HRB>();

        [DataMember(Name = "RCV")]
        public int RegimenCodeValue { get; set; }
    }

    [Serializable]
    [DataContract]
    public class HRB
    {

        [DataMember(Name = "id")]
        public int id { get; set; }
    }

    #endregion

    #region Screening

    [Serializable]
    [DataContract]
    public class TBScreening
    {

        [DataMember(Name = "id")]
        public int id { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DBScreeningData
    {
        [DataMember(Name = "Ptn_Pk")]
        public int Ptn_Pk { get; set; }

        [DataMember(Name = "LId")]
        public int LocationId { get; set; }

        [DataMember(Name = "VId")]
        public int Visit_Id { get; set; }

        [DataMember(Name = "TBS")]
        public List<TBScreening> TBScreenings = new List<TBScreening>();

        [DataMember(Name = "TBSDisplay")]
        public List<MultiSelectTable> TBScreeningDisplay = new List<MultiSelectTable>();

        [DataMember(Name = "TBF")]
        public int? TbFindings { get; set; }

        [DataMember(Name = "TBR")]
        public int? TBRegimen { get; set; }

        [DataMember(Name = "TBT")]
        public int? TBTreatment { get; set; }

        [DataMember(Name = "RxD")]
        public int? RxDuration { get; set; }

        [DataMember(Name = "SS")]
        public int? SputumSmear { get; set; }

        [DataMember(Name = "GE")]
        public int? GeneExpert { get; set; }

        [DataMember(Name = "SDST")]
        public int? SputumDST { get; set; }

        [DataMember(Name = "CXRR")]
        public int? CXRResults { get; set; }

        [DataMember(Name = "IPTA")]
        public int? IPTAdherence { get; set; }

        [DataMember(Name = "IPTC")]
        public int? IPTContraindication { get; set; }

        [DataMember(Name = "IPTD")]
        public int? IPTDiscontinued { get; set; }

        [DataMember(Name = "IsTBCS")]
        public int? IsTBContactsScreened { get; set; }

        [DataMember(Name = "IPT")]
        public int? IPT { get; set; }

        //[DataMember(Name = "IPTN")]
        //public string IPTName { get; set; }


        [DataMember(Name = "TBSD")]
        public DateTime? TBStartDate { get; set; }

        [DataMember(Name = "SSD")]
        public DateTime? SputumSmearDate { get; set; }

        [DataMember(Name = "GED")]
        public DateTime? GeneExpertDate { get; set; }

        [DataMember(Name = "SDSTD")]
        public DateTime? SputumDSTDate { get; set; }

        [DataMember(Name = "CXD")]
        public DateTime? ChestXrayDate { get; set; }

        [DataMember(Name = "TBD")]
        public DateTime? TissueBiopsyDate { get; set; }

        [DataMember(Name = "IPTSD")]
        public DateTime? IPTStartDate { get; set; }

        [DataMember(Name = "IPTED")]
        public DateTime? IPTEndDate { get; set; }

        [DataMember(Name = "rbCX")]
        public int? rbChestXray { get; set; }

        [DataMember(Name = "rbTB")]
        public int? rbTissueBiopsy { get; set; }

        [DataMember(Name = "ADS")]
        public List<AlcoholDepressionScreeningObj> AlcoholDepressionScreening = new List<AlcoholDepressionScreeningObj>();

        [DataMember(Name = "Collection")]
        public List<CodeDeCodeTables> Collections = new List<CodeDeCodeTables>();

        [DataMember(Name = "EFIPT")]
        public int? EligibleForIPT { get; set; }

        [DataMember(Name = "IPTWPYCU")]
        public int? IPTWPYellowColoredUrine { get; set; }

        [DataMember(Name = "IPTWPNB")]
        public int? IPTWPNumbnessBurning { get; set; }

        [DataMember(Name = "IPTWPYE")]
        public int? IPTWPYellownessEyes { get; set; }

        [DataMember(Name = "IPTWPT")]
        public int? IPTWPTenderness { get; set; }

        [DataMember(Name = "ORDec")]
        public string OtherReasonDeclinedIPT { get; set; }

        [DataMember(Name = "ORDis")]
        public string OtherReasonDiscontinuedIPT { get; set; }
    }


    #endregion

    #region Systemic Review

    [Serializable]
    [DataContract]
    public class DBMultiSelectSelcted
    {

        [DataMember(Name = "id")]
        public int id { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DBSystemicReviewData
    {

        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_Pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "GE", Order = 4)]
        public List<DBMultiSelectSelcted> GeneralExamination = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "Skin", Order = 5)]
        public List<DBMultiSelectSelcted> SkinExamination = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "ENT", Order = 6)]
        public List<DBMultiSelectSelcted> ENTExamination = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "Chest", Order = 7)]
        public List<DBMultiSelectSelcted> ChestExamination = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "CVS", Order = 8)]
        public List<DBMultiSelectSelcted> CVSExamination = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "Abdomen", Order = 9)]
        public List<DBMultiSelectSelcted> AbdomenExamination = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "CNS", Order = 10)]
        public List<DBMultiSelectSelcted> CNSExamination = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "RSC", Order = 11)]
        public string ReviewSystemComments { get; set; }

        [DataMember(Name = "NA", Order = 12)]
        public int NutritionalAssessment { get; set; }

        [DataMember(Name = "NAC", Order = 13)]
        public string NutritionalAssessmentComments { get; set; }

        [DataMember(Name = "NC", Order = 14)]
        public int NutritionCounseling { get; set; }

        [DataMember(Name = "WSS", Order = 15)]
        public List<WHOStage> WHOStages = new List<WHOStage>();

        [DataMember(Name = "WS", Order = 16)]
        public int WHOStage { get; set; }

        [DataMember(Name = "Collection", Order = 17)]
        public List<CodeDeCodeTables> Collections = new List<CodeDeCodeTables>();

        [DataMember(Name = "GEU", Order = 18)]
        public List<DBMultiSelectSelcted> GenitoUrinary = new List<DBMultiSelectSelcted>();
    }

    [Serializable]
    [DataContract]
    public class WHOStage
    {
        [DataMember(Name = "ValId")]
        public int ValueId { get; set; }

        [DataMember(Name = "FN")]
        public string FieldName { get; set; }

        [DataMember(Name = "DF1")]
        public DateTime? DateField1 { get; set; }

        [DataMember(Name = "DF2")]
        public DateTime? DateField2 { get; set; }

        [DataMember(Name = "ValN")]
        public string ValueName { get; set; }
    }
    #endregion

    [Serializable]
    [DataContract]
    public class ICD
    {

        [DataMember(Name = "CID", Order = 1)]
        public int ChapterId { get; set; }

        [DataMember(Name = "CHN", Order = 2)]
        public string ChapterName { get; set; }

        [DataMember(Name = "BID", Order = 3)]
        public int BlockId { get; set; }

        [DataMember(Name = "BC", Order = 4)]
        public string BlockCode { get; set; }

        [DataMember(Name = "BN", Order = 5)]
        public string BlockName { get; set; }

        [DataMember(Name = "SBID", Order = 6)]
        public int SubBlockId { get; set; }

        [DataMember(Name = "SBC", Order = 7)]
        public string SubBlockCode { get; set; }

        [DataMember(Name = "SBN", Order = 8)]
        public string SubBlockName { get; set; }

        [DataMember(Name = "ICDId", Order = 9)]
        public int ICDId { get; set; }

        [DataMember(Name = "ICDC", Order = 10)]
        public string ICDCode { get; set; }

        [DataMember(Name = "ICDN", Order = 11)]
        public string ICDName { get; set; }



    }


    [Serializable]
    [DataContract(Name = "MGT")]
    public class Management
    {

        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "ARTS", Order = 4)]
        public DateTime? ARTStart { get; set; }

        [DataMember(Name = "REGS", Order = 5)]
        public DateTime? RegimenStart { get; set; }

        [DataMember(Name = "ARTSM", Order = 6)]
        public int ArtStartinMonth { get; set; }

        [DataMember(Name = "REGSM", Order = 7)]
        public int ArtRegimeninMonth { get; set; }

        [DataMember(Name = "DDV", Order = 8)]
        public List<CodeDeCodeTables> DropdownValues = new List<CodeDeCodeTables>();

        [DataMember(Name = "ICD", Order = 9)]
        public List<ICD> ICDDropdownValues = new List<ICD>();

        [DataMember(Name = "NAD", Order = 10)]
        public DateTime? NextAppointmentDate { get; set; }

        [DataMember(Name = "PUR", Order = 11)]
        public List<CodeDeCodeTables> Purpose = new List<CodeDeCodeTables>();

        [DataMember(Name = "SIG", Order = 12)]
        public List<UserDesignation> Signature = new List<UserDesignation>();

        [DataMember(Name = "ARVSE", Order = 13)]
        public int ARVSideEffects { get; set; }

        [DataMember(Name = "MPA", Order = 14)]
        public MAdherence PatientAdherence = new MAdherence();

        [DataMember(Name = "CTXWPF", Order = 15)]
        public int CTXWhyPoorFair { get; set; }

        [DataMember(Name = "CTXA", Order = 16)]
        public int CTXAdherence { get; set; }

        [DataMember(Name = "ARVA", Order = 17)]
        public int ARVAdherence { get; set; }

        [DataMember(Name = "ARVWPF", Order = 18)]
        public int ARVWhyPoorFair { get; set; }

        [DataMember(Name = "TP", Order = 19)]
        public int TherapyPlan { get; set; }

        [DataMember(Name = "TRC", Order = 20)]
        public int TherapyReasonCode { get; set; }

        [DataMember(Name = "ARTCRMDT", Order = 21)]
        public int ARTChangeReasonDiscussbyMDT { get; set; }

        [DataMember(Name = "ARTCRMDTC", Order = 36)]
        public string ARTChangeReasonDiscussbyMDTComments { get; set; }

        [DataMember(Name = "ARVED", Order = 22)]
        public DateTime? ARVEndDate { get; set; }

        [DataMember(Name = "ET", Order = 23)]
        public int EligibleThrough { get; set; }

        [DataMember(Name = "SSTI", Order = 24)]
        public int ScreenedForSTI { get; set; }

        [DataMember(Name = "PN", Order = 25)]
        public int PartnerNotify { get; set; }

        [DataMember(Name = "KPL", Order = 26)]
        public List<DBMultiSelectSelcted> KeyPopulationList = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "PWPL", Order = 27)]
        public List<DBMultiSelectSelcted> PWPList = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "ICDGV", Order = 28)]
        public List<ICD> ICDGridValues = new List<ICD>();

        [DataMember(Name = "AREM", Order = 32)]
        public string AreaReminders { get; set; }

        [DataMember(Name = "PRD", Order = 29)]
        public string PatientRefDesc { get; set; }

        [DataMember(Name = "STES", Order = 33)]
        public List<DBMultiSelectSelcted> ShortTermEffects = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "LTES", Order = 34)]
        public List<DBMultiSelectSelcted> LongTermEffects = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "AD", Order = 30)]
        public DateTime? AppDate { get; set; }

        [DataMember(Name = "AR", Order = 31)]
        public string AppReason { get; set; }

        [DataMember(Name = "Comment", Order = 35)]
        public string Comment { get; set; }

        [DataMember(Name = "RC", Order = 36)]
        public List<CodeDeCodeTables> RegimenCodes = new List<CodeDeCodeTables>();

        [DataMember(Name = "RCV", Order = 31)]
        public int RegimenCodeValue { get; set; }

        [DataMember(Name = "RVID", Order = 37)]
        public int RegimenVisitId { get; set; }

        [DataMember(Name = "PC", Order = 38)]
        public List<CodeDeCodeTables> PatientClassifications = new List<CodeDeCodeTables>();

        [DataMember(Name = "ARTRM", Order = 39)]
        public List<CodeDeCodeTables> ARTRefillModels = new List<CodeDeCodeTables>();

        [DataMember(Name = "Appointments", Order = 40)]
        public List<Appointment> Appointments = new List<Appointment>();

        [DataMember(Name = "Classification", Order = 41)]
        public int? PatientClassification { get; set; }

        [DataMember(Name = "ISEDC", Order = 42)]
        public int? IsEnrolDifferenciatedCare { get; set; }

        [DataMember(Name = "ARTRefillModel", Order = 43)]
        public int? ARTRefillModel { get; set; }



    }

    #region ManagementDB
    [Serializable]
    [DataContract(Name = "MDB")]
    public class ManagementDB
    {
        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "IsASE", Order = 4)]
        public int ARTSideEffect { get; set; }

        [DataMember(Name = "STSE", Order = 5)]
        public List<DBMultiSelectSelcted> SortTermSideEffect = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "LTSE", Order = 6)]
        public List<DBMultiSelectSelcted> LongTermSideEffect = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "MADE", Order = 6)]
        public MAdherence MAdherence = new MAdherence();

        [DataMember(Name = "CTXA", Order = 7)]
        public int CTXAdhere { get; set; }

        [DataMember(Name = "CTXW", Order = 8)]
        public int CTXWhypoorfair { get; set; }

        [DataMember(Name = "ARVA", Order = 9)]
        public int ARVAdhere { get; set; }

        [DataMember(Name = "ARVW", Order = 10)]
        public int ARVWhypoorfair { get; set; }

        [DataMember(Name = "SI", Order = 11)]
        public int SubsituationInterruption { get; set; }

        [DataMember(Name = "EART", Order = 12)]
        public int EligibleART { get; set; }

        [DataMember(Name = "CRR", Order = 13)]
        public int ChangeRegimenReason { get; set; }

        [DataMember(Name = "SRR", Order = 14)]
        public int StopRegimenReason { get; set; }

        [DataMember(Name = "ARTDE", Order = 15)]
        public DateTime? ARTDateEnded { get; set; }

        [DataMember(Name = "TC", Order = 16)]
        public int ThreapyChange { get; set; }

        [DataMember(Name = "KP", Order = 17)]
        public int KeyPopulation { get; set; }

        [DataMember(Name = "KPO", Order = 18)]
        public int KeyPopulationOption { get; set; }

        [DataMember(Name = "STIS", Order = 19)]
        public int STIScreening { get; set; }

        [DataMember(Name = "PN", Order = 20)]
        public int PartnerNotified { get; set; }

        [DataMember(Name = "PWP", Order = 21)]
        public List<DBMultiSelectSelcted> PWP = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "AREM", Order = 22)]
        public string AreaReminders { get; set; }

        [DataMember(Name = "AREF", Order = 23)]
        public string AreaReferrals { get; set; }

        [DataMember(Name = "NAD", Order = 24)]
        public DateTime? NextAppointmentDate { get; set; }

        [DataMember(Name = "Purpose", Order = 25)]
        public int Purpose { get; set; }

        [DataMember(Name = "MGTS", Order = 26)]
        public int MgtSignature { get; set; }

        [DataMember(Name = "ICDV", Order = 27)]
        public List<ICD> ICDValues = new List<ICD>();

        [DataMember(Name = "ARTCRMDT", Order = 28)]
        public int ARTChangeReasonDiscussbyMDT { get; set; }

        [DataMember(Name = "TCC", Order = 30)]
        public string ARTChangeReasonDiscussbyMDTComments { get; set; }

        [DataMember(Name = "ICDC", Order = 29)]
        public string ICDC { get; set; }

        [DataMember(Name = "RCV", Order = 31)]
        public int RegimenCodeValue { get; set; }

        [DataMember(Name = "Classification", Order = 32)]
        public int? PatientClassification { get; set; }

        [DataMember(Name = "ISEDC", Order = 33)]
        public int? IsEnrolDifferenciatedCare { get; set; }

        [DataMember(Name = "ARTRefillModel", Order = 34)]
        public int? ARTRefillModel { get; set; }

        [DataMember(Name = "Appointments", Order = 35)]
        public List<Appointment> Appointments = new List<Appointment>();

    }

    [Serializable]
    [DataContract(Name = "MAD")]
    public class MAdherence
    {
        [DataMember(Name = "PAMId", Order = 1)]
        public int PAM_Id { get; set; }
        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_pk { get; set; }
        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "ISFM", Order = 4)]
        public int IsForgotMed { get; set; }

        [DataMember(Name = "ISCM", Order = 5)]
        public int IsCarelessMed { get; set; }

        [DataMember(Name = "ISWTM", Order = 6)]
        public int IsWorseTakingMed { get; set; }

        [DataMember(Name = "ISFBM", Order = 7)]
        public int IsFeelBetterMed { get; set; }

        [DataMember(Name = "MMAS4S", Order = 8)]
        public string MMAS4Score { get; set; }

        [DataMember(Name = "MMAS4R", Order = 9)]
        public string MMAS4Rating { get; set; }

        [DataMember(Name = "ISYM", Order = 10)]
        public int IsYesterdayMed { get; set; }

        [DataMember(Name = "ISSUC", Order = 11)]
        public int IsSymptomUnderControl { get; set; }

        [DataMember(Name = "ISSTP", Order = 12)]
        public int IsStickingTreatmentPlan { get; set; }

        [DataMember(Name = "RM", Order = 13)]
        public int RememberingMedications { get; set; }

        [DataMember(Name = "MMAS8S", Order = 14)]
        public string MMAS8Score { get; set; }

        [DataMember(Name = "MMAS8R", Order = 15)]
        public string MMAS8Rating { get; set; }

        [DataMember(Name = "MMAS8SG", Order = 16)]
        public string MMAS8Suggestion { get; set; }

        [DataMember(Name = "MGTSIG", Order = 17)]
        public int MGTSIG { get; set; }
    }

    #endregion


    #region ZScore
    [Serializable]
    [DataContract]
    public class ZScore
    {
        [DataMember(Name = "L", Order = 1)]
        public double L { get; set; }

        [DataMember(Name = "M", Order = 1)]
        public double M { get; set; }

        [DataMember(Name = "S", Order = 1)]
        public double S { get; set; }
    }

    [Serializable]
    [DataContract]
    public class ZScoreDetails
    {
        [DataMember(Name = "WFA", Order = 1)]
        public ZScore WFA = new ZScore();

        [DataMember(Name = "WFH", Order = 2)]
        public ZScore WFH = new ZScore();

        [DataMember(Name = "BMIz", Order = 3)]
        public ZScore BMIz = new ZScore();
    }

    #endregion

    [Serializable]
    [DataContract]
    public class Appointment
    {

        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }

        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "ANAD", Order = 4)]
        public DateTime? AppointmentDate { get; set; }

        [DataMember(Name = "APurpose", Order = 5)]
        public int? Purpose { get; set; }

        [DataMember(Name = "PurposeN", Order = 6)]
        public string PurposeName { get; set; }

        [DataMember(Name = "AT", Order = 7)]
        public int? AppointmentType { get; set; }

        [DataMember(Name = "SA", Order = 8)]
        public int? ServiceArea { get; set; }

        [DataMember(Name = "SAN", Order = 9)]
        public string ServiceAreaName { get; set; }

        [DataMember(Name = "Desc", Order = 10)]
        public string Description { get; set; }

        [DataMember(Name = "Status", Order = 11)]
        public string Status { get; set; }

    }

    [Serializable]
    [DataContract(Name = "RE")]
    public class RefillEncounter
    {

        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VT", Order = 3)]
        public List<CodeDeCodeTables> VisitTypes = new List<CodeDeCodeTables>();

        [DataMember(Name = "OV", Order = 4)]
        public OrdVisit OrdVisit = new OrdVisit();

        [DataMember(Name = "PV", Order = 5)]
        public PatientVitals PatientVitals = new PatientVitals();

        [DataMember(Name = "RC", Order = 6)]
        public List<CodeDeCodeTables> RegimenCodes = new List<CodeDeCodeTables>();

        [DataMember(Name = "PC", Order = 7)]
        public List<CodeDeCodeTables> PresentingComplaints = new List<CodeDeCodeTables>();

        [DataMember(Name = "PS", Order = 8)]
        public List<CodeDeCodeTables> PregnancyStatus = new List<CodeDeCodeTables>();

        [DataMember(Name = "FPMS", Order = 9)]
        public List<CodeDeCodeTables> FamilyPlanningMethods = new List<CodeDeCodeTables>();

        [DataMember(Name = "RNFP", Order = 10)]
        public List<CodeDeCodeTables> ReasonNotFamilyPlanning = new List<CodeDeCodeTables>();

        [DataMember(Name = "DDV", Order = 11)]
        public List<CodeDeCodeTables> DropdownValues = new List<CodeDeCodeTables>();

        [DataMember(Name = "PUR", Order = 12)]
        public List<CodeDeCodeTables> Purpose = new List<CodeDeCodeTables>();

        [DataMember(Name = "Appointments", Order = 13)]
        public List<Appointment> Appointments = new List<Appointment>();

        [DataMember(Name = "RCV", Order = 14)]
        public int RegimenCodeValue { get; set; }

        [DataMember(Name = "RVID", Order = 15)]
        public int RegimenVisitId { get; set; }

        [DataMember(Name = "VId", Order = 16)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "PCT", Order = 17)]
        public List<MultiSelectTable> PCT = new List<MultiSelectTable>();

        [DataMember(Name = "LMP", Order = 18)]
        public DateTime? LMP { get; set; }

        [DataMember(Name = "Pregnant", Order = 19)]
        public int Pregnant { get; set; }

        [DataMember(Name = "BS", Order = 20)]
        public int BreastStatus { get; set; }

        [DataMember(Name = "FPM", Order = 21)]
        public int FamilyPlanningMethod { get; set; }

        [DataMember(Name = "FPS", Order = 22)]
        public int FamilyPlanningStatus { get; set; }

        [DataMember(Name = "NFP", Order = 23)]
        public int NoFamilyPlanning { get; set; }

        [DataMember(Name = "PWPL", Order = 24)]
        public List<DBMultiSelectSelcted> PWPList = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "ISFM", Order = 25)]
        public int IsForgotMed { get; set; }

        [DataMember(Name = "NMD", Order = 26)]
        public int NoMissedDoses { get; set; }

        [DataMember(Name = "Drugs", Order = 27)]
        public string Drugs { get; set; }

        [DataMember(Name = "AREM", Order = 28)]
        public string AreaReminders { get; set; }

        [DataMember(Name = "OPC", Order = 29)]
        public string OtherPresentingComplaints { get; set; }
    }

    [Serializable]
    [DataContract(Name = "REDB")]
    public class RefillEncounterDB
    {

        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "OV", Order = 4)]
        public OrdVisit OrdVisit = new OrdVisit();

        [DataMember(Name = "PV", Order = 5)]
        public PatientVitals PatientVitals = new PatientVitals();

        [DataMember(Name = "PCT", Order = 6)]
        public List<MultiSelectTable> PCT = new List<MultiSelectTable>();

        [DataMember(Name = "LMP", Order = 7)]
        public DateTime? LMP { get; set; }

        [DataMember(Name = "Pregnant", Order = 8)]
        public int Pregnant { get; set; }

        [DataMember(Name = "BS", Order = 9)]
        public int BreastStatus { get; set; }

        [DataMember(Name = "FPM", Order = 10)]
        public int FamilyPlanningMethod { get; set; }

        [DataMember(Name = "FPS", Order = 11)]
        public int FamilyPlanningStatus { get; set; }

        [DataMember(Name = "NFP", Order = 12)]
        public int NoFamilyPlanning { get; set; }

        [DataMember(Name = "PWP", Order = 13)]
        public List<DBMultiSelectSelcted> PWP = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "ISFM", Order = 14)]
        public int IsForgotMed { get; set; }

        [DataMember(Name = "NMD", Order = 15)]
        public int NoMissedDoses { get; set; }

        [DataMember(Name = "Drugs", Order = 16)]
        public string Drugs { get; set; }

        [DataMember(Name = "AREM", Order = 17)]
        public string AreaReminders { get; set; }

        [DataMember(Name = "Appointments", Order = 18)]
        public List<Appointment> Appointments = new List<Appointment>();

        [DataMember(Name = "OPC", Order = 19)]
        public string OtherPresentingComplaints { get; set; }
    }


    [Serializable]
    [DataContract(Name = "OICD")]
    public class OIChronicDiseaseData
    {

        [DataMember(Name = "CDT", Order = 1)]
        public List<MultiSelectTable> CDT = new List<MultiSelectTable>();

        [DataMember(Name = "WHOS", Order = 2)]
        public List<WHOStage> WHOStages = new List<WHOStage>();

        [DataMember(Name = "WS", Order = 3)]
        public int WHOStage { get; set; }

        [DataMember(Name = "WSN", Order = 4)]
        public string WHOStageName { get; set; }

        [DataMember(Name = "ICDGV", Order = 5)]
        public List<ICD> ICDGridValues = new List<ICD>();

        [DataMember(Name = "Collection", Order = 6)]
        public List<CodeDeCodeTables> Collections = new List<CodeDeCodeTables>();
    }


}

