using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "ANCVD")]
    public class VisitDetails
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "Ptn_pk")]
        public int? Ptn_pk { get; set; }
        [DataMember(Name = "VID")]
        public int? VisitId { get; set; }
        [DataMember(Name = "LID")]
        public int? Locationid { get; set; }
        [DataMember(Name = "FVD")]
        public DateTime? FirstVisitDate { get; set; }
        [DataMember(Name = "IPTP")]
        public int? IPTP { get; set; }
        [DataMember(Name = "IPTPDG")]
        public DateTime? IPTPDateGiven { get; set; }
        [DataMember(Name = "ITN")]
        public int? InsectisideTreatedNet { get; set; }
        [DataMember(Name = "ITNDG")]
        public DateTime? InsectisideTreatedNetDT { get; set; }
        [DataMember(Name = "Dewormed")]
        public int? Dewormed { get; set; }
        [DataMember(Name = "DewormedDG")]
        public DateTime? DewormedDateGiven { get; set; }
        [DataMember(Name = "IAF")]
        public int? IronAndFolate { get; set; }
        [DataMember(Name = "IAFDG")]
        public DateTime? IronAndFolateDateGiven { get; set; }
        [DataMember(Name = "IFC")]
        public int? InfantFeedingCounselling { get; set; }
        [DataMember(Name = "EBF")]
        public int? ExclusiveBreastfeeding { get; set; }
        [DataMember(Name = "HIVIFO")]
        public int? HIVIInfantFeedingOption { get; set; }
        [DataMember(Name = "MDIF")]
        public int? MothersDecisionIF { get; set; }
        [DataMember(Name = "ER")]
        public int? ExclusiveReplacement { get; set; }
        [DataMember(Name = "POD")]
        public string PlaceOfDelivery { get; set; }
        [DataMember(Name = "PODD")]
        public DateTime? PODDate { get; set; }
        [DataMember(Name = "MOD")]
        public int? ModeOfDelivery { get; set; }
        [DataMember(Name = "BL")]
        public int? BloodLoss { get; set; }
        [DataMember(Name = "AS1M")]
        public string ApgarScore1Min { get; set; }
        [DataMember(Name = "AS5M")]
        public string ApgarScore5Min { get; set; }
        [DataMember(Name = "AS10M")]
        public string ApgarScore10Min { get; set; }
        [DataMember(Name = "RD")]
        public int? ResusitationDone { get; set; }
        [DataMember(Name = "CD")]
        public DateTime? CreateDate { get; set; }
        [DataMember(Name = "UID")]
        public int? UserId { get; set; }
        [DataMember(Name = "UD")]
        public DateTime? UpdatedDate { get; set; }

        [DataMember(Name = "TTV")]
        public int? TetanusVaccine { get; set; }
        [DataMember(Name = "TTVR")]
        public string TetanusVaccineReason { get; set; }

    }
}
