using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "ClinicalSummary")]
    public class ClinicalSummary
    {
        [DataMember(Name = "ClinicalSummary_id")]
        public int ClinicalSummary_id { get; set; }
        [DataMember(Name = "Form_filling_date")]
        public string Form_filling_date { get; set; }
        [DataMember(Name = "CliniciansName")]
        public string CliniciansName { get; set; }
        [DataMember(Name = "Telephone")]
        public string Telephone { get; set; }
        [DataMember(Name = "Email")]
        public string Email { get; set; }
        [DataMember(Name = "PrimaryReason")]
        public string PrimaryReason { get; set; }
        [DataMember(Name = "ClinicalEvaluation")]
        public string ClinicalEvaluation { get; set; }
        [DataMember(Name = "NumberOfAdherence")]
        public int NumberOfAdherence { get; set; }
        [DataMember(Name = "NumberOfHomeVisit")]
        public int NumberOfHomeVisit { get; set; }
        [DataMember(Name = "SupportStructures")]
        public string SupportStructures { get; set; }
        [DataMember(Name = "EvidenceOfAdherence")]
        public string EvidenceOfAdherence { get; set; }
        [DataMember(Name = "NumberOfDOTS")]
        public int NumberOfDOTS { get; set; }
        [DataMember(Name = "PoorAdherence")]
        public string PoorAdherence { get; set; }
        [DataMember(Name = "EvaluationForOtherText")]
        public string EvaluationForOtherText { get; set; }
        [DataMember(Name = "EvaluationForOtherVal")]
        public string EvaluationForOtherVal { get; set; }
        [DataMember(Name = "CommentOnTreatment")]
        public string CommentOnTreatment { get; set; }
        [DataMember(Name = "HasDrugResistance")]
        public string HasDrugResistance { get; set; }
        [DataMember(Name = "HasFacilityMultidisciplinary")]
        public string HasFacilityMultidisciplinary { get; set; }
        [DataMember(Name = "MDTMembers")]
        public string MDTMembers { get; set; }

        [DataMember(Name = "MFLName")]
        public string MFLName { get; set; }
        [DataMember(Name = "PatientEnrollmentID")]
        public string PatientEnrollmentID { get; set; }
        [DataMember(Name = "DOB")]
        public string DOB { get; set; }
        [DataMember(Name = "EnrollmentDate")]
        public string EnrollmentDate { get; set; }
        [DataMember(Name = "Sex")]
        public string Sex { get; set; }
        [DataMember(Name = "CurrentWeight")]
        public decimal CurrentWeight { get; set; }
        [DataMember(Name = "CurrentHeight")]
        public decimal CurrentHeight { get; set; }
        [DataMember(Name = "Ptn_pk")]
        public decimal Ptn_pk { get; set; }
        [DataMember(Name = "Visit_Id")]
        public decimal Visit_Id { get; set; }
        [DataMember(Name = "location_id")]
        public decimal location_id { get; set; }

        [DataMember(Name = "ARTRegimensLaboratory")]
        public List<ARTRegimensLaboratory> AllARTRegimensLaboratory = new List<ARTRegimensLaboratory>();

        [DataMember(Name = "RegimenChanges")]
        public List<RegimenChanges> AllRegimenChanges = new List<RegimenChanges>();

        [DataMember(Name = "OIS")]
        public List<OIS> AllOIS = new List<OIS>();

        [DataMember(Name = "ChronicConditions")]
        public List<ChronicConditions> AllChronicConditions = new List<ChronicConditions>();

        [DataMember(Name = "Anthropoemetric")]
        public List<Anthropoemetric> AllAnthropoemetric = new List<Anthropoemetric>();


    }
}
