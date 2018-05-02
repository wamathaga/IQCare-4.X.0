using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "LabR")]
    public class LabResult
    {
        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "VId", Order = 2)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "PId", Order = 3)]
        public int ParameterId { get; set; }

        [DataMember(Name = "STN", Order = 4)]
        public string SubTestName { get; set; }

        [DataMember(Name = "TD", Order = 5)]
        public DateTime? TestDate { get; set; }

        [DataMember(Name = "Result", Order = 6)]
        public string Result { get; set; }

        [DataMember(Name = "Order", Order = 7)]
        public int Order { get; set; }

        [DataMember(Name = "TR", Order = 8)]
        public decimal? TestResults { get; set; }

        [DataMember(Name = "TRD", Order = 9)]
        public String TestResultDescription { get; set; }

        [DataMember(Name = "RRD", Order = 10)]
        public String ResultReportDate { get; set; }

        [DataMember(Name = "OT", Order = 11)]
        public Boolean OrderToday { get; set; }

        [DataMember(Name = "LTID", Order = 12)]
        public int LabTestID { get; set; }

        [DataMember(Name = "OTS", Order = 13)]
        public string OrderTodayString { get; set; }

    }
}
