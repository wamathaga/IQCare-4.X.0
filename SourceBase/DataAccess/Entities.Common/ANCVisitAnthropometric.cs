using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "ANCVA")]
    public class ANCVisitAnthropometric
    {
        [DataMember(Name = "id")]
        public int? Id { get; set; }
        [DataMember(Name = "Ptn_pk")]
        public int? Ptn_pk { get; set; }
        [DataMember(Name = "VID")]
        public int? VisitId { get; set; }
        [DataMember(Name = "LID")]
        public int? Locationid { get; set; }
        [DataMember(Name = "VDD")]
        public DateTime? VisitDetailDate { get; set; }
        [DataMember(Name = "PS")]
        public string PregnancyState { get; set; }
        [DataMember(Name = "VN")]
        public string VisitNumber { get; set; }
        [DataMember(Name = "Height")]
        public string Height { get; set; }
        [DataMember(Name = "Weight")]
        public string Weight { get; set; }
        [DataMember(Name = "MUAC")]
        public string MUAC { get; set; }
        [DataMember(Name = "BPD")]
        public decimal? BPDiastolic { get; set; }
        [DataMember(Name = "BPS")]
        public decimal? BPSystolic { get; set; }
        [DataMember(Name = "CD")]
        public DateTime? CreatedDate { get; set; }
        [DataMember(Name = "UID")]
        public int? UserId { get; set; }

    }
}
