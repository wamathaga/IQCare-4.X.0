using System;
using System.Data;

namespace Interface.Clinical
{
    public interface IPatientHome
    {
        DataSet GetPatientDetails(int PatientId, int SystemId,int ModuleId);
        DataSet GetPatientHistory(int PatientId);
        DataSet IQTouchGetPatientHistory(int PatientId);
        DataTable GetPatientVisitDetail(int PatientID);
        DataSet ReActivatePatient(int PatientId, Int32 ModId);
        DataSet ReActivateTouchExceptionPatient(int PatientId, Int32 ModId, bool IsTouchException);
        DataTable GetPharmacyID(int PatientId, int LocationId, int VisitId);
        DataSet GetTechnicalAreaandFormName(int ModuleId);
        DataSet GetTechnicalAreaIndicators(int ModuleId, int PatientId);
        DataSet GetTechnicalAreaIdentifierFuture(int ModuleId,int Ptn_Pk);
        DataTable GetPatientDebitNoteSummary(int PatientID);
        DataTable GetPatientDebitNoteOpenItems(int PatientID,DateTime start, DateTime end);
        DataSet GetPatientDebitNoteDetails(int billId,int PatientId);
        int CreateDebitNote(int PatientID,int locationID, int userID, DateTime start, DateTime end);
        DataSet GetPatientSummaryInformation(int PatientId, int ModuleId);
        DataSet IQTouchGetPatientDetails(int PatientId);
        DataSet GetLinkedForms_FormLinking(int ModuleID, int FeatureID);
        DataSet GetPatientLabHistory(int PatientId);
        DataTable GetPatientWaitList(int PatientId);
        void SavePatientWaitList(DataTable WaitingList, int ModuleID, int UserID, int PatientID);
      }
}
