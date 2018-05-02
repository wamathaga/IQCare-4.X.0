using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract]
    public class ZScore
    {
        [DataMember(Name = "L", Order = 1)]
        public double L { get; set; }
        [DataMember(Name = "M", Order = 1)]
        public double M { get; set; }
        [DataMember(Name = "S", Order = 1)]
        public double S { get; set; }
    }
}
