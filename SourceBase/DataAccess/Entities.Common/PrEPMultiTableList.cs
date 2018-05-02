using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "MTL")]
    public class PrEPMultiTableList
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "NM", Order = 2)]
        public string Name { get; set; }
        [DataMember(Name = "CNM", Order = 3)]
        public string CName { get; set; }
    }
}
