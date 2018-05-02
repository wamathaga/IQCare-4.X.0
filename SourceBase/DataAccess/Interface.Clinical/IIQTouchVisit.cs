using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    #region Visit Object
    [Serializable]
    public class objVisit
    {
        public objVisit()
        {
            SensitvityList = new List<SensitivityResistance>();
            NewSensitvityList = new List<SensitivityResistance>();
            PhysicalFindings = new List<PhysicalFinding>();
            WhyARVAdherances = new List<WhyARVAdherance>();
            ChangeRegimenReasons = new List<ChangeRegimenReason>();
            StopRegimenReasons = new List<StopRegimenReason>();
            ReferredToServiceList = new List<ReferredTo>();
            AdverseEvents = new List<AdverseEvent>();
            Treatment = new List<int>();
            TBRxStartDate = string.Empty;
            TBRxEndDate = string.Empty;
            ARTEndDate = string.Empty;
            NextAppointmentDate = string.Empty;
            Scheduled = 2;
            SexuallyActiveYN = 2;
            NewTBContactYN = 2;
            SensitivityTBYN = 2;
            TreatmentYN = 2;
            DailyInjectionsYN = 2;
            AdverseEventYN = 2;
            DisclosedToChild = 2;
            TransferOut = 2;
            DispensedYN = 2;
            OldVisitID = 0;
        }
        public int PatientID { get; set; }
        public string LocationID { get; set; }
        public string UserID { get; set; }
        public string VisitDate { get; set; }
        public int OldVisitID { get; set; }
        public int Scheduled { get; set; }
        public int VisitType { get; set; }
        public int Present { get; set; }
        public string CGName { get; set; }
        public string CGPhoneNumber { get; set; }
        public int AdmittedtoHospital { get; set; }
        public int NumDaysHosp { get; set; }
        public int WhereHosp { get; set; }
        public string DischargeDiagnosis { get; set; }
        public string DischargeNote { get; set; }
        public string DurationStartART { get; set; }
        public string DurationCurrentRegimen { get; set; }
        public decimal Temp { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal BMI { get; set; }
        public int RespRate { get; set; }
        public int Pulse { get; set; }
        public int BPSyst { get; set; }
        public int BPDiast { get; set; }
        public decimal HeadCirc { get; set; }
        public decimal MUAC { get; set; }
        public int DevScreening { get; set; }
        public int TannerStage { get; set; }
        public int SexuallyActiveYN { get; set; }
        public int PregnantYN { get; set; }
        public int ProtectedSexYN { get; set; }
        public int FamilyPlanning { get; set; }
        public string FamilyPlanningOther { get; set; }
        public int NewTBContactYN { get; set; }
        public int SensitivityTBYN { get; set; }
        public List<SensitivityResistance> SensitvityList { get; set; }
        public int TreatmentYN { get; set; }
        public List<int> Treatment { get; set; }
        public int DailyInjectionsYN { get; set; }
        public int FormOfTreatment { get; set; }
        public string ContactOtherTBProphylaxis { get; set; }
        public int TBStatus { get; set; }
        public string TBRxStartDate { get; set; }
        public int StillOnTreatment { get; set; }
        public string TBRxEndDate { get; set; }
        //public int TBRxDiagnosis { get; set; }
        public List<TBRxDiagnosis> TBDiagnosisList { get; set; }
        public int NewSensitivityInfoYN { get; set; }
        public List<SensitivityResistance> NewSensitvityList { get; set; }
        public int PatientTBTreatment { get; set; }
        public string PatientOtherTBProphylaxis { get; set; }
        public List<PhysicalFinding> PhysicalFindings { get; set; }
        //public string OtherFindings { get; set; }
        public int ClinicalStage { get; set; }
        public string ClinicalNotes { get; set; }
        public int DispensedYN { get; set; }
        public string ReasonNotDispensed { get; set; }
        public int CTXAdherance { get; set; }
        public int ARVAdherance { get; set; }
        public List<WhyARVAdherance> WhyARVAdherances { get; set;}
        public string OtherARVReason { get; set; }
        public int SubsInterruptions { get; set; }
        public List<ChangeRegimenReason> ChangeRegimenReasons { get; set; }
        public string ChangeReasonOther { get; set; }
        public List<StopRegimenReason> StopRegimenReasons { get; set; }
        public string StopReasonOther { get; set; }
        public string ARTEndDate { get; set; }
        public int FeedingPractice { get; set; }
        public int NutritionalProblems { get; set; }
        public int NutrionalSupport { get; set; }
        public int DisclosedToChild { get; set; }
        public int LevelofDisclosure { get; set; }
        public List<ReferredTo> ReferredToServiceList { get; set; }
        public string RefferredToOther { get; set; }
        public int TransferOut { get; set; }
        public string NextAppointmentDate { get; set; }
        public int Signature { get; set; }
        public int AdverseEventYN { get; set; }
        public List<AdverseEvent> AdverseEvents { get; set; }


        [Serializable]
        public class AdverseEvent
        {
            public int AdverseEventID { get; set; }
            public string AdverseEventOther { get; set; }
            public int AdvereEventSeverityID { get; set; }
            public string AdverseEventDescription { get; set; }
        }
        [Serializable]
        public class SensitivityResistance
        {
            public int RegimenID { get; set; }
            public int SensitivityYN { get; set; }
            public int ResistanceYN { get; set; }
        }
        [Serializable]
        public class PhysicalFinding
        {
            public int SymptomID { get; set; }
            public string SymptomDescription { get; set; }
        }

        [Serializable]
        public class TBRxDiagnosis
        {
            public int TBDiagnosisID { get; set; }
        }

        [Serializable]
        public class WhyARVAdherance
        {
            public int ARVAdheranceID { get; set; }
        }
        [Serializable]
        public class ChangeRegimenReason
        {
            public int ChangeReasonID { get; set; }
        }
        [Serializable]
        public class StopRegimenReason
        {
            public int StopReasonID { get; set; }
        }
        [Serializable]
        public class ReferredTo
        {
            public int RefferredID { get; set; }
        }
    }
    #endregion

    public interface IIQTouchVisit
    {
        DataSet GetVisitDetails(string PatientID, string LocationID, string UserID, string VisitID);
        int SaveVisitDetails(objVisit theVisit, bool IsUpdate = false); //string PatientID, 
        
    }
}
