using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HIVCE.Common
{
    [Serializable]
    [DataContract]
    public class ResponseType
    {
        [DataMember(Name = "Success", Order = 1)]
        public string Success { get; set; }
        [DataMember(Name = "PK", Order = 2)]
        public int PrimaryKey { get; set; }
        [DataMember(Name = "EM", Order = 3)]
        public string ErrorMessage { get; set; }
    }
}
