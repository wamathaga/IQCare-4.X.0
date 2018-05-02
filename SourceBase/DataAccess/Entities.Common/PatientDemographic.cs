using System;
using System.Runtime.Serialization;


namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "PatientDemographic")]
    public class PatientDemographic
    {
        [DataMember(Name = "PatientID", Order = 1)]
        public int PatientId { get; set; }
        [DataMember(Name = "PatientName", Order = 2)]
        public String PatientName { get; set; }
        [DataMember(Name = "DOV", Order = 3)]
        public int? DateOfVisit { get; set; }
        [DataMember(Name = "PatientTele", Order = 4)]
        public string PatientTele { get; set; }
        [DataMember(Name = "PatientAddress", Order = 5)]
        public int? PatientAddress { get; set; }
        [DataMember(Name = "PatientCode", Order = 6)]
        public string PatientCode { get; set; }
        [DataMember(Name = "PatientGender", Order = 7)]
        public string PatientGender { get; set; }
        [DataMember(Name = "PatientState", Order = 8)]
        public string PatientState { get; set; }
        [DataMember(Name = "PatientLGA", Order = 9)]
        public string PatientLga { get; set; }
        [DataMember(Name = "PatientMaritalStat", Order = 10)]
        public string PatientMaritalStat { get; set; }
        [DataMember(Name = "NoOfChildLessThan5", Order = 11)]
        public string NoOfChildLessThan5 { get; set; }
        [DataMember(Name = "NoOfWives", Order = 12)]
        public string NoOfWives { get; set; }

    }
}
