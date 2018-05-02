using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IQTouchKNHExpress
    {
       // int IQTouchSaveExpressFormDetail();
        DataTable IQTouchGetKnhExpressData(BIQTouchExpressFields expressFrmFields);
        int IQTouchSaveExpressDetails(List<BIQTouchExpressFields> lstobjExpressFields);

    }

    [Serializable()]
    public class BIQTouchExpressFields
    {
        public string Flag { get; set; }
        public int ID { get; set; }
        public int PtnPk{get;set;}
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public int ChildAccompaniedByCaregiver { get; set; }
        public int TreatmentSupporterRelationship { get; set; }
        public decimal Temperature { get; set; }
        public decimal RespirationRate { get; set; }
        public decimal HeartRate { get; set; }
        public decimal SystolicBloodPressure { get; set; }
        public decimal DiastolicBloodPressure { get; set; }
        public int MedicalCondition { get; set; }
        public int SpecificMedicalCondition { get; set; }
        public int OnFollowUp { get; set; }
        public DateTime LastFollowUpDate { get; set; }
        public int PreviousAdmission { get; set; }
        public string PreviousAdmissionDiagnosis { get; set; }
        public DateTime PreviousAdmissionStart { get; set; }
        public DateTime PreviousAdmissionEnd { get; set; }
        public int TBAssessmentIcf { get; set; }
        public int TBFindings { get; set; }
        public string RegimenPrescribedFup { get; set; }
        public int LabEvaluationPeads { get; set; }
        public int SpecifyLabEvaluation { get; set; }
        public int OIProphylaxis { get; set; }
        public string OtherOIProphylaxis { get; set; }
        public string TreatmentPlan { get; set; }
        public int PwPMessagesGiven { get; set; }
        public int CondomsIssued { get; set; }
        public string ReasonfornotIssuingCondoms { get; set; }
        public int IntentionOfPregnancy { get; set; }
        public int DiscussedDualContraception { get; set; }
        public int DiscussedFertilityOption { get; set; }
        public int OnFP { get; set; }
        public int FPmethod { get; set; }
        public int CervicalCancerScreened { get; set; }
        public int ReferredForCervicalCancerScreening { get; set; }
        public int CervicalCancerScreeningResults { get; set; }
        public DateTime NextApointmentDate { get; set; }
        public int RegimenPrescribed { get; set; }
        public string OtherRegimenPrescribed { get; set; }
        public int LatestViralLoad { get; set; }
        public DateTime LatestViralLoadDate { get; set; }
        public int ResultsCervicalCancer { get; set; }
        public int ReasonCTXpresribed { get; set; }
        public DateTime VisitDate { get; set; }
        public int Signature {get;set;}
        public decimal Height { get; set; }
        public decimal Weight { get; set; }

        

    }
    public static class ConverTotValue
    {
        public static object NullToString(object objVal)
        {
            object retObj;
            if (objVal == null)
            {
                retObj = "";
            }
            else
            {
                retObj = objVal;
            }

            return retObj;
        }
        public static object NullToInt(object objVal)
        {
            object retObj;
            if (objVal == null)
            {
                retObj = 0;
            }
            else
            {
                retObj = objVal;
            }

            return retObj;
        }
        public static object NullToBoolean(object objVal)
        {
            object retObj=DBNull.Value;
            if (objVal != null)
                retObj = Convert.ToBoolean(objVal);
            return retObj;
        }

        public static object NullToDate(object objVal)
        {
             object retObj;
             DateTime dtvar;
             if (objVal != "1900")
             {
                 retObj = objVal;
             }
             else
             {
                 retObj = null;

             }
            
            

            return retObj;
        }

    }


  

}


