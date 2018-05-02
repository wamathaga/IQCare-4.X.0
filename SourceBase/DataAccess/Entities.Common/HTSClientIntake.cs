using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "HTSClientIntake")]
    public class HTSClientIntake
    {
        //Common client section

        [DataMember(Name = "CIFID")]
        public int CIFID { get; set; }
        [DataMember(Name = "UserID")]
        public int UserID { get; set; }

        [DataMember(Name = "ReferFrom")]
        public int? ReferFrom { get; set; }
        [DataMember(Name = "Setting")]
        public int? Setting { get; set; }
        [DataMember(Name = "TypeOfSession")]
        public int? TypeOfSession { get; set; }
        [DataMember(Name = "IsClientIsIndex")]
        public int? IsClientIsIndex { get; set; }
        [DataMember(Name = "IndexTestingType")]
        public int? IndexTestingType { get; set; }
        [DataMember(Name = "IndexClientID")]
        public string IndexClientID { get; set; }
        
        [DataMember(Name = "FirstTimeVisit")]
        public int? FirstTimeVisit { get; set; }
        [DataMember(Name = "StateOfResidence")]
        public int? StateOfResidence { get; set; }
        [DataMember(Name = "LGAOfResidence")]
        public int? LGAOfResidence { get; set; }
        [DataMember(Name = "NoOfChildLessThan5")]
        public int? NoOfChildLessThan5 { get; set; }
        [DataMember(Name = "NoOfWives")]
        public int? NoOfWives { get; set; }

         //Pre-Test Information - KNOWLEDGE ASSESSMENT
        [DataMember(Name = "PrevTestedHIVNeg")]
        public int? PrevTestedHivNeg { get; set; }
        [DataMember(Name = "ClientPreg")]
        public int? ClientPregnant { get; set; }
        [DataMember(Name = "InformHivTransRoute")]
        public int? InformHivTransRoute { get; set; }
        [DataMember(Name = "InformHivTransRiskFactor")]
        public int? InformHivTransRiskFactor { get; set; }
        [DataMember(Name = "InformHivTransMethod")]
        public int? InformHivTransMethod { get; set; }
        [DataMember(Name = "InformHivPossiResults")]
        public int? InformHivPossiResults { get; set; }
        [DataMember(Name = "InformConsentHIVTesting")]
        public int? InformConsentHIVTesting { get; set; }

        //Pre-Test Information - HIV Risk ASSESSMENT
        [DataMember(Name = "EvrHadSex")]
        public int? EvrHadSex { get; set; }
        [DataMember(Name = "BlodTransIn3Mnth")]
        public int? BlodTransIn3Mnth { get; set; }
        [DataMember(Name = "SexWithCasPartnr")]
        public int? SexWithCasPartnr { get; set; }
        [DataMember(Name = "SexWithRegPartnr")]
        public int? SexWithRegPartnr { get; set; }
        [DataMember(Name = "LastStiDone")]
        public int? LastStiDone { get; set; }
        [DataMember(Name = "OnceAMonthSex")]
        public int? OnceAMonthSex { get; set; }

        //Pre-Test Information - Clinical TB ASSESSMENT
        [DataMember(Name = "CurrentCough")]
        public int? CurrentCough { get; set; }
        [DataMember(Name = "WeightLoss")]
        public int? WeightLoss { get; set; }
        [DataMember(Name = "Fever")]
        public int? Fever { get; set; }
        [DataMember(Name = "NightSweats")]
        public int? NightSweats { get; set; }

        //Pre-Test Information - Syndromic STI Screening
        [DataMember(Name = "FemaleVaginalDischarge")]
        public int? FemaleVaginalDischarge { get; set; }
        [DataMember(Name = "FemaleAbdominalPain")]
        public int? FemaleAbdominalPain { get; set; }
        [DataMember(Name = "MaleUrethalDischarge")]
        public int? MaleUrethalDischarge { get; set; }
        [DataMember(Name = "MaleScrotalSweling")]
        public int? MaleScrotalSweling { get; set; }
        [DataMember(Name = "GenitalSore")]
        public int? GenitalSore { get; set; }


        //SEROLOGY REQUEST 
        [DataMember(Name = "InitialHIVTest")]
        public int? InitialHivTest { get; set; }
        [DataMember(Name = "ConfirmatoryHIVTest")]
        public int? ConfirmatoryHivTest { get; set; }
        [DataMember(Name = "SyphilisTest")]
        public int? SyphilisTest { get; set; }
        [DataMember(Name = "HepatitisB")]
        public int? HepatitisB { get; set; }
        [DataMember(Name = "HepatitisC")]
        public int? HepatitisC { get; set; }


        //POST TEST COUNSELING 
        [DataMember(Name = "HIVTestResult")]
        public int? HivTestResult { get; set; }
        [DataMember(Name = "NotPrevTested")]
        public int? NotPrevTested { get; set; }
        [DataMember(Name = "PrevTestNeg")]
        public int? PrevTestNeg { get; set; }
        [DataMember(Name = "PrevTestPosInHivCare")]
        public int? PrevTestPosInHivCare { get; set; }
        [DataMember(Name = "PrevTestPosNotInHivCare")]
        public int? PrevTestPosNotInHivCare { get; set; }
        [DataMember(Name = "HIVReqResFormSigned")]
        public int? HIVReqResFormSigned { get; set; }
        [DataMember(Name = "HIVReqResFormWithCI")]
        public int? HIVReqResFormWithCI { get; set; }
        [DataMember(Name = "ClientRecHivTestResult")]
        public int? ClientRecHivTestResult { get; set; }
        [DataMember(Name = "PostTestConslDone")]
        public int? PostTestConslDone { get; set; }


        [DataMember(Name = "RiskReducPlan")]
        public int? RiskReducPlan { get; set; }
        [DataMember(Name = "PostTestDiscloPlan")]
        public int? PostTestDiscloPlan { get; set; }
        [DataMember(Name = "BringPartForHIVTest")]
        public int? BringPartForHIVTest { get; set; }
        [DataMember(Name = "BrngChildForHIVTest")]
        public int? BrngChildForHivTest { get; set; }
        [DataMember(Name = "FPAndDualConcInfo")]
        public int? FPAndDualConcInfo { get; set; }
        [DataMember(Name = "PartnerUseFPMethd")]
        public int? PartnerUseFPMethd { get; set; }
        [DataMember(Name = "PrntUseCondomAsFPM")]
        public int? PrntUseCondomAsFPM { get; set; }
        [DataMember(Name = "CondomUseDemo")]
        public int? CondomUseDemo { get; set; }
        [DataMember(Name = "CondomsProvided")]
        public int? CondomsProvided { get; set; }
        [DataMember(Name = "ClientRefferedToOther")]
        public int? ClientRefferedToOther { get; set; }

        [DataMember(Name = "SyphilisTestResults")]
        public int? SyphilisTestResults { get; set; }

        [DataMember(Name = "RequestedBy")]
        public int? RequestedBy { get; set; }
        [DataMember(Name = "TestedBy")]
        public int? TestedBy { get; set; }
        [DataMember(Name = "CheckedBy")]
        public int? CheckedBy { get; set; }

        [DataMember(Name = "RequestedDate")]
        public DateTime? RequestedDate { get; set; }
        [DataMember(Name = "TestedDate")]
        public DateTime? TestedDate { get; set; }
        [DataMember(Name = "CheckedDate")]
        public DateTime? CheckedDate { get; set; }

        [DataMember(Name = "PatientID")]
        public int PatientId { get; set; }
        [DataMember(Name = "PatientName")]
        public String PatientName { get; set; }
        [DataMember(Name = "DOV")]
        public DateTime? DateOfVisit { get; set; }
        [DataMember(Name = "PatientTele")]
        public int? PatientTele { get; set; }
        [DataMember(Name = "PatientAddress")]
        public string PatientAddress { get; set; }
        [DataMember(Name = "PatientCode")]
        public string PatientCode { get; set; }
        [DataMember(Name = "PatientGender")]
        public int? PatientGender { get; set; }
        [DataMember(Name = "PatientState")]
        public int? PatientState { get; set; }
        [DataMember(Name = "PatientLga")]
        public int? PatientLga { get; set; }
        [DataMember(Name = "PatientMaritalStat")]
        public int? PatientMaritalStat { get; set; }

        [DataMember(Name = "Comments")]
        public string Comments { get; set; }

        [DataMember(Name = "States")]
        public List<States> AllStates = new List<States>();
        
        [DataMember(Name = "LGAs")]
        public List<LGAs> AllLGAs = new List<LGAs>();

        [DataMember(Name = "UserList")]
        public List<UserDesignation> UserList = new List<UserDesignation>();
    }
}
