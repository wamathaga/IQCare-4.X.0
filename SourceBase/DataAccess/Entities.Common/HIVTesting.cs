using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "HIVTest")]
    public class HIVTesting
    {
        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "VId", Order = 2)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VN", Order = 2)]
        public int? VisitNumber { get; set; }

        [DataMember(Name = "HIVTT", Order = 2)]
        public int? HIVTestingToday { get; set; }

        [DataMember(Name = "HIVTTD", Order = 2)]
        public int? HIVTestingTodayDone { get; set; }

        [DataMember(Name = "HIVTP2", Order = 2)]
        public int? HIVTestingPoint2 { get; set; }

        [DataMember(Name = "PRC", Order = 2)]
        public int? PretestCounselling { get; set; }

        [DataMember(Name = "HIVFR", Order = 2)]
        public int? FinalHIVResult { get; set; }

        [DataMember(Name = "PHIVT", Order = 2)]
        public int? PartnerTestedHIV { get; set; }

        [DataMember(Name = "POTC", Order = 2)]
        public int? PostTestCounselling { get; set; }

        [DataMember(Name = "PI", Order = 2)]
        public int? PartnerInvolvement { get; set; }

        [DataMember(Name = "PPRTC", Order = 2)]
        public int? PartnerPreTestCounselling { get; set; }

        [DataMember(Name = "PHIVFR", Order = 2)]
        public int? FinalHIVResultPartner { get; set; }
        
        [DataMember(Name = "PPOTC", Order = 2)]
        public int? PartnerPostTestCounselling { get; set; }

        [DataMember(Name = "DC", Order = 2)]
        public int? DiscordantCouple { get; set; }

        [DataMember(Name = "PPCR", Order = 2)]
        public int? PCRpartner { get; set; }
        
        [DataMember(Name = "HIVTR", Order = 2)]
        public int? MotherHIVTestResult { get; set; }

        [DataMember(Name = "HIVTRD", Order = 7)]
        public DateTime? MotherHIVTestDate { get; set; }

        [DataMember(Name = "DF", Order = 2)]
        public int? DeleteFlag { get; set; }

        [DataMember(Name = "UId", Order = 2)]
        public int? UserID { get; set; }

        [DataMember(Name = "CD", Order = 6)]
        public DateTime? CreatedDate { get; set; }

        [DataMember(Name = "UD", Order = 7)]
        public DateTime? UpdatedDate { get; set; }

    }
}
