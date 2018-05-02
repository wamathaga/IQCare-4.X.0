using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "OrdVisit")]
    public class OrdVisit
    {
        [DataMember(Name = "VId", Order = 1)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_Pk { get; set; }

        [DataMember(Name = "LId", Order = 3)]
        public int LocationId { get; set; }

        [DataMember(Name = "VD", Order = 4)]
        public DateTime? VisitDate { get; set; }

        [DataMember(Name = "VTId", Order = 5)]
        public int VisitTypeId { get; set; }

        [DataMember(Name = "CD", Order = 6)]
        public DateTime? CreatedDate { get; set; }

        [DataMember(Name = "UD", Order = 7)]
        public DateTime? UpdatedDate { get; set; }

        [DataMember(Name = "TOV", Order = 8)]
        public int TypeOfVisit { get; set; }

        [DataMember(Name = "SG", Order = 8)]
        public int Signature { get; set; }
    }
}
