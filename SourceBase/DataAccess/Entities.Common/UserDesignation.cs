using System;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "UserDesignation")]
    public class UserDesignation
    {
        [DataMember(Name = "UserID", Order = 1)]
        public int UserID { get; set; }
        [DataMember(Name = "UserName", Order = 2)]
        public String UserName { get; set; }
        [DataMember(Name = "Designation", Order = 3)]
        public string Designation { get; set; }
        [DataMember(Name = "DeleteFlag", Order = 4)]
        public int? DeleteFlag { get; set; }
    }
}
