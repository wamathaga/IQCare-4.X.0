using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using Interface.Scheduler;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;


namespace BusinessProcess.Scheduler
{
    public class BAppointment : ProcessBase, IAppointment
    {
        public BAppointment()
        {
        }

        public DataSet GetAppointmentStatus()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject AppointmentStatus = new ClsObject();
                return (DataSet)AppointmentStatus.ReturnObject(ClsUtility.theParams, "pr_Scheduler_SelectAppStatus_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        #region "Modified13June07(1)"
        public DataSet GetAppointmentReasons(int Id)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject AppointmentReasons = new ClsObject();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());

                return (DataSet)AppointmentReasons.ReturnObject(ClsUtility.theParams, "pr_Scheduler_SelectAppReason_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        
        public DataSet GetEmployees(int Id)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject Employees = new ClsObject();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());

                return (DataSet)Employees.ReturnObject(ClsUtility.theParams, "pr_Admin_GetEmployeeDetails_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        #endregion
        public DataTable CheckAppointmentExistance(int PatientId, int LocationId, String AppDate,int ReasonId,int visitId)
        {
            lock (this)
            {
                try
                {
                    DataTable theDt;

                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject SaveAppointment = new ClsObject();
                    SaveAppointment.Connection = this.Connection;
                    SaveAppointment.Transaction = this.Transaction;

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    ClsUtility.AddParameters("@AppDate", SqlDbType.VarChar, AppDate.ToString());
                    ClsUtility.AddParameters("@ReasonId", SqlDbType.Int, ReasonId.ToString());
                    ClsUtility.AddParameters("@visitId", SqlDbType.Int, visitId.ToString());

                    theDt = (DataTable)SaveAppointment.ReturnObject(ClsUtility.theParams, "pr_Scheduler_CheckAppointmentExistance_Constella", ClsDBUtility.ObjectEnum.DataTable);

                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);
                    return theDt;
                }
                catch
                {
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }

        public DataSet GetAppointmentGrid(int AppStatus, string FromDate, string ToDate, int LocationID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject AppointmentManager = new ClsObject();

                ClsUtility.AddParameters("@AppStatus", SqlDbType.Int, AppStatus.ToString());
                ClsUtility.AddParameters("@FromDate", SqlDbType.VarChar, FromDate.ToString());
                ClsUtility.AddParameters("@ToDate", SqlDbType.VarChar, ToDate.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);


                return (DataSet)AppointmentManager.ReturnObjectNewImpl(ClsUtility.theParams, "pr_Scheduler_AppointmentList_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveAppointment(int PatientId,int LocationId, String AppDate, int AppReasonId, int AppProviderId, int UserId,  String CreateDate)
        {
            lock (this)
            {
                try
                {
                    int theAffectedRows = 0;
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject SaveAppointment = new ClsObject();
                    SaveAppointment.Connection = this.Connection;
                    SaveAppointment.Transaction = this.Transaction;

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    ClsUtility.AddParameters("@AppDate", SqlDbType.VarChar, AppDate.ToString());
                    ClsUtility.AddParameters("@AppReasonId", SqlDbType.Int, AppReasonId.ToString());
                    ClsUtility.AddParameters("@AppProviderId", SqlDbType.Int, AppProviderId.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@CreateDate", SqlDbType.VarChar, CreateDate.ToString());

                    theAffectedRows = (int)SaveAppointment.ReturnObject(ClsUtility.theParams, "pr_Scheduler_SaveAppointment_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);
                    return theAffectedRows;
                }
                catch
                {
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }

        public DataSet GetPatientppointmentDetails(int PatientId, int LocationId, int VisitId)
        {
            lock (this)
            {
                try
                {
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject SaveAppointment = new ClsObject();
                    SaveAppointment.Connection = this.Connection;
                    SaveAppointment.Transaction = this.Transaction;

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    return (DataSet)SaveAppointment.ReturnObject(ClsUtility.theParams, "pr_Scheduler_GetPatientAppointmentDetails_Constella", ClsDBUtility.ObjectEnum.DataSet);

                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);

                }
                catch
                {
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }

        public int UpdatePatientppointmentDetails(int PatientId, int LocationId, int VisitId, String AppDate, int AppReasonId,int UserId, int AppProviderId, String Updationdate)
        {
            lock (this)
            {
                try
                {
                    int theAffectedRows = 0;
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject SaveAppointment = new ClsObject();
                    SaveAppointment.Connection = this.Connection;
                    SaveAppointment.Transaction = this.Transaction;

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                    ClsUtility.AddParameters("@AppDate", SqlDbType.VarChar, AppDate.ToString());
                    ClsUtility.AddParameters("@AppReasonId", SqlDbType.Int, AppReasonId.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@AppProviderId", SqlDbType.Int, AppProviderId.ToString());
                    ClsUtility.AddParameters("@Updationdate", SqlDbType.VarChar, Updationdate.ToString());


                    theAffectedRows = (int)SaveAppointment.ReturnObject(ClsUtility.theParams, "pr_Scheduler_UpdatePatientAppointmentDetails_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);
                    return theAffectedRows;
                }
                catch
                {
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }

        public int DeletePatientAppointmentDetails(int PatientId, int LocationId, int VisitId)
        {
            lock (this)
            {
                try
                {
                    int theAffectedRows = 0;
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);

                    ClsObject SaveAppointment = new ClsObject();
                    SaveAppointment.Connection = this.Connection;
                    SaveAppointment.Transaction = this.Transaction;

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());

                    theAffectedRows = (int)SaveAppointment.ReturnObject(ClsUtility.theParams, "pr_Scheduler_DeletePatientAppointmentDetails_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);
                    return theAffectedRows;
                }
                catch
                {
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }



//*****************************************************************************************//
        //  public DataSet SearchResultAppointStatus()
        // {
        //   ClsUtility.Init_Hashtable();
        //   ClsObject SearchResultAppointStatus = new ClsObject();
        // return (DataSet)SearchResultAppointStatus.ReturnObject(ClsUtility.theParams, "pr_Scheduler_Search_PatientAppointment_Constella", ClsDBUtility.ObjectEnum.DataSet);
        //     }

        public DataSet SearchPatientAppointment(string LName, string FName, int PatientID, string HospitalID, DateTime DOB, int Sex, int AppStatus)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject SchedulerMgr = new ClsObject();
                SchedulerMgr.Connection = this.Connection;
                SchedulerMgr.Transaction = this.Transaction;

                 ClsUtility.AddParameters("@LastName", SqlDbType.VarChar, LName);
                 ClsUtility.AddParameters("@FirstName", SqlDbType.VarChar, FName);
                 ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                 ClsUtility.AddParameters("@HospitalID", SqlDbType.VarChar, HospitalID);
                 ClsUtility.AddParameters("@DOB", SqlDbType.DateTime, DOB.ToString());
                 ClsUtility.AddParameters("@Sex", SqlDbType.Int, Sex.ToString());
                 ClsUtility.AddParameters("@AppStatus", SqlDbType.Int, AppStatus.ToString());

                DataSet SchedulerDR;
                SchedulerDR = (DataSet)SchedulerMgr.ReturnObject(ClsUtility.theParams, "pr_Scheduler_Search_PatientAppointment_Constella", ClsDBUtility.ObjectEnum.DataSet);
                
                return SchedulerDR; 
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                {
                    DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
  
    }
}