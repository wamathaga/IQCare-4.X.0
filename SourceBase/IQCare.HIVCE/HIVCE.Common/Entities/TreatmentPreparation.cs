using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HIVCE.Common.Entities
{
    [Serializable]
    [DataContract]
    public class TreatmentPreparation
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_pk { get; set; }
        [DataMember(Name = "HIVIT", Order = 3)]
        public DateTime? HIVIT { get; set; }
        [DataMember(Name = "HIVR", Order = 4)]
        public DateTime? HIVR { get; set; }
        [DataMember(Name = "VLIT", Order = 5)]
        public DateTime? ViralLoadIT { get; set; }
        [DataMember(Name = "VLR", Order = 6)]
        public DateTime? ViralLoadR { get; set; }
        [DataMember(Name = "CD4IT", Order = 7)]
        public DateTime? CD4IT { get; set; }
        [DataMember(Name = "CD4R", Order = 8)]
        public DateTime? CD4R { get; set; }
        [DataMember(Name = "ANIT", Order = 9)]
        public DateTime? AntiretroviralIT { get; set; }
        [DataMember(Name = "ANR", Order = 10)]
        public DateTime? AntiretroviralR { get; set; }
        [DataMember(Name = "TFIT", Order = 11)]
        public DateTime? TreatmentFailureIT { get; set; }
        [DataMember(Name = "TFR", Order = 12)]
        public DateTime? TreatmentFailureR { get; set; }
        [DataMember(Name = "ARTSEIT", Order = 13)]
        public DateTime? ARTSEIT { get; set; }
        [DataMember(Name = "ARTSER", Order = 14)]
        public DateTime? ARTSER { get; set; }
        [DataMember(Name = "ADIT", Order = 15)]
        public DateTime? AdherenceIT { get; set; }
        [DataMember(Name = "ADR", Order = 16)]
        public DateTime? AdherenceR { get; set; }
        [DataMember(Name = "OMIT", Order = 17)]
        public DateTime? OtherMedicationsIT { get; set; }
        [DataMember(Name = "OMR", Order = 18)]
        public DateTime? OtherMedicationsR { get; set; }
        [DataMember(Name = "NIT", Order = 19)]
        public DateTime? NutritionIT { get; set; }
        [DataMember(Name = "NR", Order = 20)]
        public DateTime? NutritionR { get; set; }
        [DataMember(Name = "FUIT", Order = 21)]
        public DateTime? FollowUpIT { get; set; }
        [DataMember(Name = "FUR", Order = 22)]
        public DateTime? FollowUpR { get; set; }
        [DataMember(Name = "ARA", Order = 23)]
        public int? ARTReadinessAssessment { get; set; }
        [DataMember(Name = "MPIT", Order = 24)]
        public DateTime? ManagementPlanIT { get; set; }
        [DataMember(Name = "MPR", Order = 25)]
        public DateTime? ManagementPlanR { get; set; }
        [DataMember(Name = "Notes", Order = 26)]
        public string Notes { get; set; }
    }
}
