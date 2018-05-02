using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "Facility")]
    public class Facility
    {
        [DataMember(Name = "CId", Order = 5)]
        public int? CountryId { get; set; }
        [DataMember(Name = "CN", Order = 6)]
        public string CountryName { get; set; }
        [DataMember(Name = "Id", Order = 1)]
        public int ID { get; set; }
        [DataMember(Name = "MFL", Order = 4)]
        public string MFLCode { get; set; }
        [DataMember(Name = "Name", Order = 2)]
        public string Name { get; set; }
        [DataMember(Name = "SrNo", Order = 3)]
        public int? SrNo { get; set; }

    }
}
