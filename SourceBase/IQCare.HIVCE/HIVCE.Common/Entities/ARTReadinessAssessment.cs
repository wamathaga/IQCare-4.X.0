using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HIVCE.Common.Entities
{
    [Serializable]
    [DataContract]
    public class ARTReadinessAssessment
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_pk { get; set; }
        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }
        [DataMember(Name = "PQ1", Order = 4)]
        public int? PsychosocialQ1 { get; set; }
        [DataMember(Name = "PQ2", Order = 5)]
        public int? PsychosocialQ2 { get; set; }
        [DataMember(Name = "PQ3", Order = 6)]
        public int? PsychosocialQ3 { get; set; }
        [DataMember(Name = "PQ4", Order = 7)]
        public int? PsychosocialQ4 { get; set; }
        [DataMember(Name = "PQ5", Order = 8)]
        public int? PsychosocialQ5 { get; set; }
        [DataMember(Name = "PQ6", Order = 9)]
        public int? PsychosocialQ6 { get; set; }
        [DataMember(Name = "PQ7", Order = 10)]
        public int? PsychosocialQ7 { get; set; }
        [DataMember(Name = "PQ8", Order = 11)]
        public int? PsychosocialQ8 { get; set; }
        [DataMember(Name = "PQ9", Order = 12)]
        public int? PsychosocialQ9 { get; set; }
        [DataMember(Name = "PQ10", Order = 13)]
        public int? PsychosocialQ10 { get; set; }
        [DataMember(Name = "SQ1", Order = 14)]
        public int? SupportQ1 { get; set; }
        [DataMember(Name = "SQ2", Order = 15)]
        public int? SupportQ2 { get; set; }
        [DataMember(Name = "SQ3", Order = 16)]
        public int? SupportQ3 { get; set; }
        [DataMember(Name = "SQ4", Order = 17)]
        public int? SupportQ4 { get; set; }
        [DataMember(Name = "SQ5", Order = 18)]
        public int? SupportQ5 { get; set; }

    }
}
