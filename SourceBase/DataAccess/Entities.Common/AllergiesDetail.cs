using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
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
}
