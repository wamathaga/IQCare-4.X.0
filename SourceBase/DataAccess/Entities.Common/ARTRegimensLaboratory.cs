using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "ARTRegimensLaboratory")]
    public class ARTRegimensLaboratory
    {        
        [DataMember(Name = "Date")]
        public string Date { get; set; }
        [DataMember(Name = "CD4")]
        public string CD4 { get; set; }
        [DataMember(Name = "HB")]
        public string HB { get; set; }
        [DataMember(Name = "EGFR")]
        public string EGFR { get; set; }
        [DataMember(Name = "ViralLoad")]
        public string ViralLoad { get; set; }
        [DataMember(Name = "WeightZScore")]
        public string WeightZScore { get; set; }
        [DataMember(Name = "ARVRegimen")]
        public string ARVRegimen { get; set; }
        [DataMember(Name = "ReasonForSwitch")]
        public string ReasonForSwitch { get; set; }
        [DataMember(Name = "NewOI")]
        public string NewOI { get; set; }        
    }

    [Serializable]
    [DataContract(Name = "RegimenChanges")]
    public class RegimenChanges
    {
        [DataMember(Name = "Date")]
        public string Date { get; set; }
        [DataMember(Name = "Regimen")]
        public string Regimen { get; set; }
        [DataMember(Name = "Switch")]
        public string Switch { get; set; }      
    }
   
    [Serializable]
    [DataContract(Name = "OIS")]
    public class OIS
    {
        [DataMember(Name = "Date")]
        public string Date { get; set; }
        [DataMember(Name = "OIS")]
        public string OISData { get; set; }        
    }
   
    [Serializable]
    [DataContract(Name = "ChronicConditions")]
    public class ChronicConditions
    {
        [DataMember(Name = "Date")]
        public string Date { get; set; }
        [DataMember(Name = "Condition")]
        public string Condition { get; set; }
    }
  
    [Serializable]
    [DataContract(Name = "Anthropoemetric")]
    public class Anthropoemetric
    {
        [DataMember(Name = "Date")]
        public string Date { get; set; }
        [DataMember(Name = "Height")]
        public string Height { get; set; }
        [DataMember(Name = "Weight")]
        public string Weight { get; set; }
        [DataMember(Name = "BMIZ")]
        public string BMIZ { get; set; }

    }
}
