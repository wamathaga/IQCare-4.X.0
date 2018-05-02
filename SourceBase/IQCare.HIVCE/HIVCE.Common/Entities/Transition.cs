using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HIVCE.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Transition
    {

        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_pk { get; set; }
        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }
        [DataMember(Name = "G1Q1", Order = 4)]
        public int? G1Q1 { get; set; }
        [DataMember(Name = "G1Q2", Order = 5)]
        public int? G1Q2 { get; set; }
        [DataMember(Name = "G1Q3", Order = 6)]
        public int? G1Q3 { get; set; }
        [DataMember(Name = "G1Q4", Order = 7)]
        public int? G1Q4 { get; set; }
        [DataMember(Name = "G2Q1", Order = 8)]
        public int? G2Q1 { get; set; }
        [DataMember(Name = "G2Q1D", Order = 9)]
        public DateTime? G2Q1Date { get; set; }
        [DataMember(Name = "G2Q2", Order = 10)]
        public int? G2Q2 { get; set; }
        [DataMember(Name = "G2Q2D", Order = 11)]
        public DateTime? G2Q2Date { get; set; }
        [DataMember(Name = "G2Q3", Order = 12)]
        public int? G2Q3 { get; set; }
        [DataMember(Name = "G2Q3D", Order = 13)]
        public DateTime? G2Q3Date { get; set; }
        [DataMember(Name = "G3Q1", Order = 14)]
        public int? G3Q1 { get; set; }
        [DataMember(Name = "G3Q1D", Order = 15)]
        public DateTime? G3Q1Date { get; set; }
        [DataMember(Name = "G3Q2", Order = 16)]
        public int? G3Q2 { get; set; }
        [DataMember(Name = "G3Q2D", Order = 17)]
        public DateTime? G3Q2Date { get; set; }
        [DataMember(Name = "G4Q1", Order = 18)]
        public int? G4Q1 { get; set; }
        [DataMember(Name = "G4Q1D", Order = 19)]
        public DateTime? G4Q1Date { get; set; }
        [DataMember(Name = "G4Q2", Order = 20)]
        public int? G4Q2 { get; set; }
        [DataMember(Name = "G4Q2D", Order = 21)]
        public DateTime? G4Q2Date { get; set; }
        [DataMember(Name = "G4Q3", Order = 22)]
        public int? G4Q3 { get; set; }
        [DataMember(Name = "G4Q3D", Order = 23)]
        public DateTime? G4Q3Date { get; set; }
        [DataMember(Name = "G4Q4", Order = 24)]
        public int? G4Q4 { get; set; }
        [DataMember(Name = "G4Q4D", Order = 25)]
        public DateTime? G4Q4Date { get; set; }
    }
}
