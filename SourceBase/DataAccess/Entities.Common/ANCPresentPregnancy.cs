using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "ANCPP")]
    public class ANCPresentPregnancy
    {
        [DataMember(Name = "id")]
        public int? Id { get; set; }
        [DataMember(Name = "Ptn_pk")]
        public int? Ptn_pk { get; set; }
        [DataMember(Name = "VID")]
        public int? VisitId { get; set; }
        [DataMember(Name = "LID")]
        public int? Locationid { get; set; }
        [DataMember(Name = "M")]
        public string Maturity { get; set; }
        [DataMember(Name = "Lie")]
        public string Lie { get; set; }
        [DataMember(Name = "FH")]
        public string FundalHeight { get; set; }
        [DataMember(Name = "FM")]
        public string FoetalMovement { get; set; }
        [DataMember(Name = "P")]
        public string Presentation { get; set; }
        [DataMember(Name = "FHR")]
        public string FoetalHeartRate { get; set; }
        [DataMember(Name = "CD")]
        public string CreatedDate { get; set; }
        [DataMember(Name = "UID")]
        public int? UserId { get; set; }

    }
}
