using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract]
    public class CodeDeCode
    {   
        [DataMember(Name = "CId", Order = 1)]
        public int CodeId { get; set; }
        [DataMember(Name = "CN", Order = 2)]
        public String CodeName { get; set; }
        [DataMember(Name = "DId", Order = 3)]
        public int DeCodeId { get; set; }
        [DataMember(Name = "DN", Order = 4)]
        public string DeCodeName { get; set; }
    }
}
