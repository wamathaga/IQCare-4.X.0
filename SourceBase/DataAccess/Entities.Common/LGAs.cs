using System;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "LGAs")]
    public class LGAs
    {
        [DataMember(Name = "LGAId")]
        public int LgaID { get; set; }

        [DataMember(Name = "LGAName")]
        public string LgaName { get; set; }
    }
}
