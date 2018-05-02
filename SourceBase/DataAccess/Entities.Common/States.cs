using System;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "States")]
    public class States
    {
        [DataMember(Name ="StateId")]
        public int StateID { get; set; }

        [DataMember(Name = "StateName")]
        public string StateName { get; set; }
    }
}
