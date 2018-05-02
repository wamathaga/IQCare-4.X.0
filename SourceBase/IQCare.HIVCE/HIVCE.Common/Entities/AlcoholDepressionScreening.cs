using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HIVCE.Common.Entities
{
    [Serializable]
    [DataContract]
    public class AlcoholDepressionScreeningObj
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_pk { get; set; }
        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "ISFD", Order = 4)]
        public int IsFeelingDown { get; set; }
        [DataMember(Name = "ISLI", Order = 5)]
        public int IsLittleInterest { get; set; }
        [DataMember(Name = "PHQLI", Order = 6)]
        public int PHQLittleInterest { get; set; }
        [DataMember(Name = "PHQFD", Order = 7)]
        public int PHQFeelingDown { get; set; }
        [DataMember(Name = "PHQTS", Order = 8)]
        public int PHQTroubleSleep { get; set; }
        [DataMember(Name = "PHQTLE", Order = 9)]
        public int PHQTiredLittleEnergy { get; set; }
        [DataMember(Name = "PHQA", Order = 10)]
        public int PHQAppetite { get; set; }
        [DataMember(Name = "PHQYD", Order = 11)]
        public int PHQYourselfDown { get; set; }
        [DataMember(Name = "PHQTC", Order = 12)]
        public int PHQTroubleConcentrating { get; set; }
        [DataMember(Name = "PHQMS", Order = 13)]
        public int PHQMovingSlowly { get; set; }
        [DataMember(Name = "PHQFR", Order = 14)]
        public int PHQFidgetyRestless { get; set; }
        [DataMember(Name = "PHQHY", Order = 15)]
        public int PHQHurtingYourself { get; set; }
        [DataMember(Name = "PHQT", Order = 16)]
        public int PHQTotal { get; set; }
        [DataMember(Name = "PHQDS", Order = 17)]
        public string PHQDepressionSeverity { get; set; }
        [DataMember(Name = "PHQR", Order = 18)]
        public string PHQRecommended { get; set; }
        [DataMember(Name = "SGBQ1", Order = 19)]
        public int SGBQ1 { get; set; }
        [DataMember(Name = "SGBQ2", Order = 20)]
        public int SGBQ2 { get; set; }
        [DataMember(Name = "SGBQ3", Order = 21)]
        public int SGBQ3 { get; set; }
        [DataMember(Name = "SGBQ4", Order = 22)]
        public int SGBQ4 { get; set; }
        [DataMember(Name = "SGBQ5", Order = 23)]
        public int SGBQ5 { get; set; }
        [DataMember(Name = "DHIVS", Order = 24)]
        public int DisclosureHIVStatus { get; set; }
        [DataMember(Name = "DS", Order = 25)]
        public int DisclosureStatus { get; set; }
        [DataMember(Name = "DTO", Order = 26)]
        public int DisclosureTo { get; set; }
        [DataMember(Name = "CAL", Order = 27)]
        public int CrafftAlcohol { get; set; }
        [DataMember(Name = "CS", Order = 28)]
        public int CrafftSmoke { get; set; }
        [DataMember(Name = "CAH", Order = 29)]
        public int CrafftAnythingHigh { get; set; }
        [DataMember(Name = "CAC", Order = 30)]
        public int CrafftC { get; set; }
        [DataMember(Name = "CAR", Order = 31)]
        public int CrafftR { get; set; }
        [DataMember(Name = "CAA", Order = 32)]
        public int CrafftA { get; set; }
        [DataMember(Name = "CAF", Order = 33)]
        public int CrafftF1 { get; set; }
        [DataMember(Name = "CAFF", Order = 34)]
        public int CrafftF2 { get; set; }
        [DataMember(Name = "CAT", Order = 35)]
        public int CrafftT { get; set; }
        [DataMember(Name = "CSC", Order = 36)]
        public int CrafftScore { get; set; }
        [DataMember(Name = "CR", Order = 37)]
        public string CrafftRisk { get; set; }
        [DataMember(Name = "CAIDA", Order = 38)]
        public int CageAIDAlcohol { get; set; }
        [DataMember(Name = "CAIDD", Order = 39)]
        public int CageAIDDrugs { get; set; }
        [DataMember(Name = "CAIDS", Order = 40)]
        public int CageAIDSmoke { get; set; }
        [DataMember(Name = "CAIDQ1", Order = 41)]
        public int CageAIDQ1 { get; set; }
        [DataMember(Name = "CAIDQ2", Order = 42)]
        public int CageAIDQ2 { get; set; }
        [DataMember(Name = "CAIDQ3", Order = 43)]
        public int CageAIDQ3 { get; set; }
        [DataMember(Name = "CAIDQ4", Order = 44)]
        public int CageAIDQ4 { get; set; }
        [DataMember(Name = "CAIDSC", Order = 45)]
        public int CageAIDScore { get; set; }
        [DataMember(Name = "CAIDR", Order = 46)]
        public string CageAIDRisk { get; set; }
        [DataMember(Name = "CAIDSS", Order = 47)]
        public int CageAIDStopSmoking { get; set; }
        [DataMember(Name = "CD", Order = 48)]
        public DateTime? CreatedDate { get; set; }
        [DataMember(Name = "UD", Order = 49)]
        public DateTime? UpdatedDate { get; set; }
        [DataMember(Name = "Notes", Order = 50)]
        public string Notes { get; set; }

    }
}
