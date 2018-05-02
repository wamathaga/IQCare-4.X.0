using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "PV")]
    public class PatientVitals
    {
        [DataMember(Name = "VId", Order = 1)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "BPD", Order = 2)]
        public string BPDiastolic { get; set; }

        [DataMember(Name = "BPS", Order = 3)]
        public string BPSystolic { get; set; }

        [DataMember(Name = "TEMP", Order = 4)]
        public string TEMP { get; set; }

        [DataMember(Name = "RR", Order = 5)]
        public string RR { get; set; }

        [DataMember(Name = "HR", Order = 6)]
        public string HR { get; set; }

        [DataMember(Name = "HC", Order = 7)]
        public string HeadCircumference { get; set; }

        [DataMember(Name = "Height", Order = 8)]
        public string Height { get; set; }

        [DataMember(Name = "Weight", Order = 9)]
        public string Weight { get; set; }

        [DataMember(Name = "MUAC", Order = 10)]
        public string MUAC { get; set; }

        [DataMember(Name = "WFA", Order = 11)]
        public string Weightforage { get; set; }

        [DataMember(Name = "WFH", Order = 12)]
        public string Weightforheight { get; set; }

        [DataMember(Name = "BMIz", Order = 13)]
        public string BMIz { get; set; }

        [DataMember(Name = "NC", Order = 14)]
        public string NurseComments { get; set; }

        [DataMember(Name = "SP", Order = 15)]
        public string SPO2 { get; set; }


    }
}
