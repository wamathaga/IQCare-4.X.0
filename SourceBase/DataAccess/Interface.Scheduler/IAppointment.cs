using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Scheduler
{
    public interface IAppointment
    {
        DataSet GetAppointmentStatus();
        DataSet GetAppointmentGrid(int AppStatus, string FromDate, string ToDate, int LocationID);
        DataTable CheckAppointmentExistance(int PatientId, int LocationId, String AppDate, int ReasonId, int visitId);
        //update by-nidhi
        //Desc- changing datetime to string
        //int SaveAppointment(int PatientId, int LocationId, DateTime AppDate, int AppReasonId, int AppProviderId, int UserId, DateTime CreateDate);
        int SaveAppointment(int PatientId, int LocationId, String AppDate, int AppReasonId, int AppProviderId, int UserId, String CreateDate);
        DataSet  GetPatientppointmentDetails(int PatientId, int LocationId, int VisitId);
        //update by-nidhi
        //Desc- changing datetime to string
        //int UpdatePatientppointmentDetails(int PatientId, int LocationId, int VisitId, DateTime AppDate, int AppReasonId,int UserId, int AppProviderId, DateTime UpdationDate);
        int UpdatePatientppointmentDetails(int PatientId, int LocationId, int VisitId, String AppDate, int AppReasonId, int UserId, int AppProviderId, String Updationdate);
        int DeletePatientAppointmentDetails(int PatientId, int LocationId, int VisitId);
        #region "Modified13June07(1)"
        DataSet GetAppointmentReasons(int Id);
        DataSet GetEmployees(int Id);
        #endregion
    }
}
