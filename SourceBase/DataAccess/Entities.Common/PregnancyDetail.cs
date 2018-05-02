using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "Pregnancy")]
    public class PregnancyDetail
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }

        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "YOB", Order = 4)]
        public int? YearofBaby { get; set; }

        [DataMember(Name = "POD", Order = 5)]
        public string PlaceOfDelivery { get; set; }

        [DataMember(Name = "MID", Order = 6)]
        public int? MaturityId { get; set; }

        [DataMember(Name = "MName", Order = 7)]
        public string Maturity { get; set; }

        [DataMember(Name = "LH", Order = 8)]
        public decimal? LabourHour { get; set; }

        [DataMember(Name = "MODID", Order = 9)]
        public int? ModeOfDeliveryId { get; set; }

        [DataMember(Name = "MOD", Order = 10)]
        public string ModeOfDelivery { get; set; }

        [DataMember(Name = "GID", Order = 11)]
        public int? GenderId { get; set; }

        [DataMember(Name = "Gender", Order = 12)]
        public string Gender { get; set; }

        [DataMember(Name = "FID", Order = 13)]
        public int? FateId { get; set; }

        [DataMember(Name = "Fate", Order = 14)]
        public string Fate { get; set; }

        [DataMember(Name = "CD", Order = 15)]
        public DateTime? CreatedDate { get; set; }

        [DataMember(Name = "UD", Order = 16)]
        public DateTime? UpdatedDate { get; set; }

        [DataMember(Name = "UId", Order = 17)]
        public int? UserId { get; set; }

        [DataMember(Name = "BW", Order = 18)]
        public decimal? Birthweight { get; set; }
    }
}
