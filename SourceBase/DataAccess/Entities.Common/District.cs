using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract]
    public class District
    {
        [DataMember(Name = "Id", Order = 1)]
        public int ID { get; set; }
        [DataMember(Name = "Name", Order = 2)]
        public String Name { get; set; }
        [DataMember(Name = "SrNo", Order = 3)]
        public int? SrNo { get; set; }
    }
}
