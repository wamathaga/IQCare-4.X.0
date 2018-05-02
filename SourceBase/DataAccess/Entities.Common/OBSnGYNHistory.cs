using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
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
}
