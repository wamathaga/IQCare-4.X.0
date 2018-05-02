using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract]
    public class ZScoreDetails
    {
        [DataMember(Name = "BMIz", Order = 3)]
        public ZScore BMIz;
        [DataMember(Name = "WFA", Order = 1)]
        public ZScore WFA;
        [DataMember(Name = "WFH", Order = 2)]
        public ZScore WFH;
        
    }
}
