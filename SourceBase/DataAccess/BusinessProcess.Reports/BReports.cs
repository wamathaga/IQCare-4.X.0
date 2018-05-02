using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using Interface.Reports;
using System.Xml;


namespace BusinessProcess.Reports
{
    public class BReports : ProcessBase, IReports
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Sanjay Rana
        // Written Date      : 06th Oct 2006
        // Modification Date : 06th Mar 2008
        // Description       : 
        //
        /// /////////////////////////////////////////////////////////////////
        /// 
        #region "Constructor"
        public BReports()
        {

        }
        #endregion

        #region "Reports Method"

        public DataSet IQTouchGetPatientVisitSummary(int PatientId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@ptn_pk", SqlDbType.Int, PatientId.ToString());
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "sp_PASDP_PatientVisitSummaryReport", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }

        }
        public DataSet IQTouchGetPatientSummary(int PatientId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@ptn_pk", SqlDbType.Int, PatientId.ToString());
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "sp_PASDP_PaediatricPatientSummary", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// GetPatientDetails: Get Patient Details.
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        /// 

        public DataSet GetPatientDetails(Int32 PatientID, DateTime StartDate, DateTime EndDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());
                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_Patient_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }

        }
        /// <summary>
        /// GetDrugARVPickup : Get Patient Drug ARV Pickup Information.
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataSet GetDrugARVPickup(Int32 PatientID, string StartDate, string EndDate, string SatelliteID, string CountryID, string PosID, int LocationID)
        {
            lock (this)
            {
                try
                {

                    ClsUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                    ClsUtility.AddParameters("@StartDate", SqlDbType.VarChar, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.VarChar, EndDate.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    ClsUtility.AddParameters("@SatelliteId", SqlDbType.VarChar, SatelliteID);
                    ClsUtility.AddParameters("@CountryId", SqlDbType.VarChar, CountryID);
                    ClsUtility.AddParameters("@PosId", SqlDbType.VarChar, PosID);
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    return (DataSet)ReportManager.ReturnObjectNewImpl(ClsUtility.theParams, "pr_Reports_ARVDrugPickup_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        /// <summary>
        /// GetAllPatientDrugARVPickup : Get All Patient Drug ARV Pickup Information.
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataSet GetAllPatientDrugARVPickup(int @LocationID)
        //public DataSet GetAllPatientDrugARVPickup()
        {
            lock (this)
            {
                try
                {

                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    //ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    //ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_ARVDrugPickup_AllPatients_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        /// <summary>
        /// GetMissARVPickup : Get All Patient Missed ARV Pickup Information.
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        /// This is function is no more in use - Jayant - 25-Aug-2008
        //public DataSet GetDrugARVPickupTillDate(Int32 PatientID, int LocationID) 
        //{
        //    try
        //    {
        //        ClsUtility.Init_Hashtable();
        //        ClsObject ReportManager = new ClsObject();
        //        ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());
        //        ClsUtility.AddParameters("@tillDate", SqlDbType.Int, "1");
        //        ClsUtility.AddParameters("@LocationID", SqlDbType.Int, "0");
        //        ClsUtility.AddParameters("@SatelliteId", SqlDbType.VarChar, "");
        //        ClsUtility.AddParameters("@CountryId", SqlDbType.VarChar, "");
        //        ClsUtility.AddParameters("@PosId", SqlDbType.VarChar, "");
        //        return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_ARVDrugPickup_Constella", ClsDBUtility.ObjectEnum.DataSet);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public DataSet EnrollmentNoCheck(string PatientId, string LocationID, string CountryID, string PosID, string SatelliteID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    ClsUtility.AddParameters("@PatientId", SqlDbType.VarChar, PatientId);
                    ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID);
                    ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, CountryID);
                    ClsUtility.AddParameters("@PosID", SqlDbType.VarChar, PosID);
                    ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, SatelliteID);
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_EnrollmentNo_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet GetMissARVPickup(DateTime StartDate, string LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    ClsUtility.AddParameters("@DefaulterAsOnDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID.ToString());
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_MissARVPickup_AllPatients_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        //Deepika
        public DataSet GetPatientEnrollMonth(DateTime Startdate, DateTime Enddate, String LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, Startdate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, Enddate.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID);
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetPatiEnrollMonth_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// GetARVCollectionClients : Get Patients ARV Collection Information.
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
        public DataSet GetARVCollectionClients(int PatientID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());


                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetARVCollectionClients_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// GetUpcomingARVPickPatients : Get Upcoming ARV Pick Patients.
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>

        public DataSet GetUpcomingARVPickPatients(DateTime StartDate, DateTime EndDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();


                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());



                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetUpcomingARVPickPatients_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }

        }

        /// <summary>
        /// GetMisARVAppointClients : Get Patients Missed ARV Appoint.
        /// </summary>
        /// <param name="SType"></param>
        /// <param name="SDate"></param>
        /// <returns></returns>

        public DataSet GetMisARVAppointClients(String SType, DateTime SDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();


                    ClsUtility.AddParameters("@SType", SqlDbType.VarChar, SType.ToString());
                    ClsUtility.AddParameters("@SDate", SqlDbType.DateTime, SDate.ToString());



                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetMisARVAppointClients_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }

            }

        }

        public DataSet GetMisARVPickPatients(DateTime StartDate, DateTime EndDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();


                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());



                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetMisARVPickPatients_Constella", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }

        }
        /// <summary>
        /// GetNewPatients :Get New Patients.
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataSet GetNewPatients(DateTime StartDate, DateTime EndDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_NewPatients_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetUserDetail(DateTime StartDate, DateTime EndDate, String UserId, int LocationID,int ModuleID)
    {
        lock (this)
        {
            try
            {
                ClsUtility.Init_Hashtable();

                ClsObject ReportManager = new ClsObject();

                ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID.ToString());
                ClsUtility.AddParameters("@ModuleID", SqlDbType.VarChar, ModuleID.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetUserDetail_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }

            catch
            {
                throw;
            }
        }
   
    }

        /// <summary>
        /// GetPregnantPatients : Get Pregnant Patients.
        /// </summary>
        /// <param name="Pregnant"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataSet GetPregnantPatients(Int32 Pregnant, DateTime StartDate, DateTime EndDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@Pregnant", SqlDbType.Int, Pregnant.ToString());
                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_PregnantPatients_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }

        }
        /// <summary>
        /// GetPatientProfileAndHistory : Get Patient Profile And History.
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public DataSet GetPatientProfileAndHistory(int PatientId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                    // ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                    ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_PatientProfile_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }
        // 10th April 2008
        public DataTable GetMonthlyNACAReportData(DateTime DateOrderedFrom, DateTime DateOrderedTo, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@DateOrderedFrom", SqlDbType.DateTime, DateOrderedFrom.ToString());
                    ClsUtility.AddParameters("@DateOrderedTo", SqlDbType.DateTime, DateOrderedTo.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataTable)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_MonthlyNACAReport_Constella", ClsDBUtility.ObjectEnum.DataTable);

                }
                catch
                {
                    throw;
                }
            }
        }

        public DataSet GetNACPMonthlyReportData(int MonthId, int Year, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    ClsUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_NACPMonthlyReport_Constella", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetNACPCohortMonthlyReport(int MonthId, int Year, string LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    ClsUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID.ToString());
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_NACP_CohortAnalysisReport_Constella", ClsDBUtility.ObjectEnum.DataSet);

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_NACP_SixCohortFinalAnalysisReport_Constella", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetNACPSixCohortMonthlyReport(int MonthId, int Year, string LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    ClsUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID.ToString());


                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_NACP_SixCohortFinalAnalysisReport_Constella", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetNACPQuarterlyReportData(int QuarterId, int QuarterYear, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@QuarterId", SqlDbType.DateTime, QuarterId.ToString());
                    ClsUtility.AddParameters("@QuarterYear", SqlDbType.DateTime, QuarterYear.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_NACPQuarterlyReport_Constella", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }


        public DataSet GetLosttoFollowupPatientReport(int @LocationID,string SystemId)
        {
            lock (this)
            {
                try
                {

                    ClsUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                    ClsUtility.AddParameters("@SystemId", SqlDbType.VarChar, SystemId.ToString());
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_LosttoFollowupPatientReport_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public DataSet GetTBStatusbyAgeandSex(DateTime StartDate, DateTime EndDate, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetTBStatusbyageandsex_Constella", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetTotalNoTBPatientwithARVwithoutARV(String StartDate, String EndDate, String LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@DateFrom", SqlDbType.VarChar, StartDate);
                    ClsUtility.AddParameters("@DateTo", SqlDbType.VarChar, EndDate);
                    ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationID);
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_NoofTBBeforeAfterARV_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch { throw; }
            }

        }

        public DataSet GetARVRegimenforAdultandChild(DateTime StartDate, DateTime EndDate, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetARVRegimenforAdultandChild_Constella", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetPatientsnotvisitedrecently(DateTime StartDate, DateTime EndDate, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_ GetPatientsnotvisitedrecently _Constella", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetGeographicalPatientsDistribution(int @LocationID)
        {
            lock (this)
            {
                try
                {

                    ClsUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetGeographicalPatientsDistribution", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public DataSet GetPtnotvisitedrecentlyUnknown(string StartDate, string EndDate, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    ClsUtility.AddParameters("@StartDate", SqlDbType.VarChar, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.VarChar, EndDate.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetPtnotvisitedrecentlyUnknown_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public DataSet GetARVRegimenReport(DateTime StartDate, DateTime EndDate, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetARVRegimenReport_Constella", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetARVCohortReport(int StartDate, int EndDate, int StartDateYear, int EndDateYear, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();
                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    ClsUtility.AddParameters("@StartDateYear", SqlDbType.DateTime, StartDateYear.ToString());
                    ClsUtility.AddParameters("@EndDateYear", SqlDbType.DateTime, EndDateYear.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());


                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetARVCohortReport_Constella", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }

        public DataSet GetNonArtPatient(DateTime StartDate, DateTime EndDate, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_Non_ARTPatientReport_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch { throw; }
            }

        }
        /// <summary>
        /// GetCDSReportData : Get CDC Report Data.
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="QuarterId"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public DataSet GetCDSReportData(DateTime StartDate, DateTime EndDate, int LocationId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();

                ClsObject ReportManager = new ClsObject();

                ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());

                //ClsUtility.AddParameters("@Quarter", SqlDbType.Int,QuarterId.ToString());
                //ClsUtility.AddParameters("@Year", SqlDbType.Int, Year.ToString());

                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetCDCReportData1_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
        public DataSet GetPMTCTTrack10ReportData(DateTime StartDate, DateTime EndDate, int LocationId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();

                ClsObject ReportManager = new ClsObject();

                ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());

                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_PMTCTTrackReport_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
 }


        public DataSet GetCDSReportQuarterDate(int QtrID, int QtrYear)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();

                ClsObject ReportManager = new ClsObject();

                ClsUtility.AddParameters("@QuarterID", SqlDbType.Int, QtrID.ToString());
                ClsUtility.AddParameters("@QuarterYear", SqlDbType.Int, QtrYear.ToString());

                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetCDCQuarterDate", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
        //     ==================== Functions for Custom Reports ==================
        /// <summary>
        /// Method for Get All Fields Groups
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllFieldGroups(int SystemID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();

                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReports_GetFieldGroups", ClsDBUtility.ObjectEnum.DataSet);
            }

        }

        /// <summary>
        /// Method for Get all Custom Report Category
        /// </summary>
        /// <returns></returns>

        public DataSet GetAllCategory()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReportsGetAllCategory", ClsDBUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetReportQuarter()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_admin_GetReportQuarterList_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }

        }

        /// <summary>
        /// Method for get all fields of a specific FieldsGroup
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public DataSet GetFields(int GroupId, int SystemID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@GroupId", SqlDbType.Int, GroupId.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReports_GetFieldGroups", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
        /// <summary>
        /// Method for get specific Custom report data
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        public DataSet GetCustomReportData(int ReportId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReportGetReportData_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Method for get Custom Report List
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public DataSet GetReportList(int CategoryId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@CategoryId", SqlDbType.Int, CategoryId.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReportGetReportList_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }

        }

        /// <summary>
        /// Method for get selection values of column
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public DataTable GetDropDownValueForField(int FieldId, string FieldName, string viewName, int SystemID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();
                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, FieldId.ToString());
                    ClsUtility.AddParameters("@Field", SqlDbType.VarChar, FieldName);
                    ClsUtility.AddParameters("@ViewName", SqlDbType.VarChar, viewName);
                    ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemID.ToString());

                    return (DataTable)ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReportsGetDropDownValue", ClsDBUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    throw;
                }
            }

        }
        //public DataTable ParseSQLStatement(string sqlstr)
        public String ParseSQLStatement(string sqlstr)
        {
            lock (this)
            {
                try
                {
                    ClsObject CustomReports = new ClsObject();

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@QryString", SqlDbType.NVarChar, sqlstr);

                    DataTable dt1 = (DataTable)CustomReports.ReturnObject(ClsUtility.theParams, "pr_General_SQL_Parse", ClsDBUtility.ObjectEnum.DataTable);

                    if (dt1.Rows.Count == 0)
                    {
                        return ("No Records");
                    }
                    else
                    {
                        return ("Valid SQL");
                    }
                }
                catch (SqlException sqlEx)
                {
                    return sqlEx.Message.ToString();
                }
                catch (Exception ex)
                {
                    //throw ex;
                    return ex.Message.ToString();
                }
            }
        }
        /// <summary>
        /// Method for Get Custom Report
        /// </summary>
        /// <param name="ReportQuery"></param>
        /// <returns></returns>
        public DataSet GetCustomReport(Int32 ReportId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    ClsUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString());
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetCustomReport_Constella", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch (SqlException sqlEx)
                {
                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        /// <summary>
        /// Method for get report title
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        ////public String GetReportTitle(int ReportId)
        ////{
        ////    DataTable dtReportTitle = new DataTable(); 
        ////    try
        ////    {
        ////        ClsUtility.Init_Hashtable();
        ////        ClsObject ReportManager = new ClsObject();
        ////        ClsUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString() );
        ////        dtReportTitle=(DataTable)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetCustomReportTitle_Constella", ClsDBUtility.ObjectEnum.DataTable);
        ////        return (dtReportTitle.Rows[0][0].ToString());
        ////    }
        ////    catch
        ////    {
        ////        throw;
        ////    }

        ////}
        /// <summary>
        /// Method for Get Query of a Specific Custom Report
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>

        ////public String GetReportQuery(int ReportId)
        ////{
        ////    DataTable dtReportQuery = new DataTable();
        ////    try
        ////    {
        ////        ClsUtility.Init_Hashtable();

        ////        ClsObject ReportManager = new ClsObject();

        ////        ClsUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString() );


        ////        dtReportQuery = (DataTable)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetCustomReportQuery_Constella", ClsDBUtility.ObjectEnum.DataTable);

        ////        return (dtReportQuery.Rows[0][0].ToString());
        ////    }
        ////    catch
        ////    {
        ////        throw;
        ////    }

        ////}
        /// <summary>
        /// Method for get (Count) number of columns in a Custom Report
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        ////public int GetReportColumnCount(int ReportId)
        ////{
        ////    DataTable dtReportColumnCount = new DataTable();
        ////    try
        ////    {
        ////        ClsUtility.Init_Hashtable();

        ////        ClsObject ReportManager = new ClsObject();

        ////        ClsUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString() );


        ////        dtReportColumnCount = (DataTable)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetCustomReportColumnCount_Constella", ClsDBUtility.ObjectEnum.DataTable);

        ////        return Convert.ToInt32((dtReportColumnCount.Rows[0][0].ToString()));
        ////    }
        ////    catch
        ////    {
        ////        throw;
        ////    }

        ////}

        //
        public int DeleteCustomReport(int ReportId)
        {

            ClsObject ReportManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                ReportManager.Connection = this.Connection;
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ReportManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString());
                Int32 RowsAffected = (Int32)ReportManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_DeleteCustomReport_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                return RowsAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Method for Save and Update Custom Report
        /// </summary>
        /// <param name="dsReportDetails"></param>
        /// <param name="intflag"></param>
        /// <returns></returns>

        public int SaveCustomReport(DataSet dsReportDetails, int intflag)
        {
            ClsObject ReportManager = new ClsObject();
            try
            {
                int ReportId = 0;
                int FieldId = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);


                ReportManager.Connection = this.Connection;
                ReportManager.Transaction = this.Transaction;


                DataRow dr_mstReport = dsReportDetails.Tables["dtMstReport"].Rows[0];
                int retval = 0;
                ClsUtility.Init_Hashtable();
                DataRow theDR;



                ClsUtility.AddParameters("@CategoryId", SqlDbType.Int, dr_mstReport["CategoryId"].ToString());
                ClsUtility.AddParameters("@CategoryName", SqlDbType.VarChar, dr_mstReport["CategoryName"].ToString());
                ClsUtility.AddParameters("@ReportName", SqlDbType.VarChar, dr_mstReport["ReportName"].ToString());
                ClsUtility.AddParameters("@Description", SqlDbType.VarChar, dr_mstReport["Description"].ToString());
                ClsUtility.AddParameters("@Condition", SqlDbType.VarChar, dr_mstReport["Condition"].ToString());
                ClsUtility.AddParameters("@RptType", SqlDbType.VarChar, dr_mstReport["RptType"].ToString());

                if (intflag == 0) // Saving New Record
                {
                    theDR = (DataRow)ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReport_SaveCustomReport", ClsDBUtility.ObjectEnum.DataRow); // Return ReportId
                    ReportId = Convert.ToInt32(theDR[0]);
                }
                else // Updating Custom report 
                {
                    ClsUtility.AddParameters("@ReportId", SqlDbType.Int, dr_mstReport["ReportId"].ToString());
                    ReportId = Convert.ToInt32(dr_mstReport["ReportId"]);
                    ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReport_UpdateCustomReport", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString());
                    ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReports_DeleteReportFieldsFilters", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                ClsUtility.Init_Hashtable();
                foreach (DataRow drFields in dsReportDetails.Tables["dtlReportFields"].Rows)
                {

                    ClsUtility.AddParameters("@ReportId", SqlDbType.Int, ReportId.ToString());
                    ClsUtility.AddParameters("@GroupId", SqlDbType.Int, drFields["GroupID"].ToString());
                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, drFields["FieldId"].ToString());
                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, drFields["FieldName"].ToString());
                    ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, drFields["FieldLabel"].ToString());
                    ClsUtility.AddParameters("@AggregateFunction", SqlDbType.VarChar, drFields["AggregateFunction"].ToString());
                    ClsUtility.AddParameters("@IsDisplay", SqlDbType.Bit, drFields["IsDisplay"].ToString());
                    ClsUtility.AddParameters("@Sequence", SqlDbType.SmallInt, drFields["Sequence"].ToString());
                    ClsUtility.AddParameters("@Sort", SqlDbType.VarChar, drFields["Sort"].ToString());
                    ClsUtility.AddParameters("@ViewName", SqlDbType.VarChar, drFields["ViewName"].ToString());

                    theDR = (DataRow)ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReports_SaveReportFields", ClsDBUtility.ObjectEnum.DataRow); // Return ReportFiledId
                    FieldId = Convert.ToInt32(theDR[0]);
                    ClsUtility.Init_Hashtable();
                    foreach (DataRow drFilter in dsReportDetails.Tables["dtlReportFilter"].Rows)
                    {
                        if (Convert.ToInt32(drFields["FieldId"]) == Convert.ToInt32(drFilter["LinkFieldId"]) && Convert.ToInt32(drFields["Sequence"]) == Convert.ToInt32(drFilter["PanelId"]))
                        {
                            ClsUtility.AddParameters("@ReportFieldId", SqlDbType.Int, FieldId.ToString());
                            ClsUtility.AddParameters("@Operator", SqlDbType.VarChar, drFilter["Operator"].ToString());
                            ClsUtility.AddParameters("@FilterValue", SqlDbType.VarChar, drFilter["FilterValue"].ToString());
                            ClsUtility.AddParameters("@AndOr", SqlDbType.VarChar, drFilter["AndOr"].ToString());
                            ClsUtility.AddParameters("@Sequence", SqlDbType.SmallInt, drFilter["Sequence"].ToString());
                            ClsUtility.AddParameters("@Operator1", SqlDbType.VarChar, drFilter["Operator1"].ToString());
                            ClsUtility.AddParameters("@FilterValue1", SqlDbType.VarChar, drFilter["FilterValue1"].ToString());
                            ClsUtility.AddParameters("@AndOr1", SqlDbType.VarChar, drFilter["AndOr1"].ToString());

                            ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReports_SaveReportFilters", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                            ClsUtility.Init_Hashtable();
                        }
                    }
                }

                DataMgr.CommitTransaction(ReportManager.Transaction);
                return ReportId;
            }
            catch (SqlException sqlEx)
            {
                DataMgr.RollBackTransation(ReportManager.Transaction);
                throw sqlEx;
            }
        }

        /// <summary>
        /// Mehot for get a category name 
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        public DataTable GetCategory(string CategoryName)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@CategoryName", SqlDbType.VarChar, CategoryName.ToString().Trim());

                    return (DataTable)ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReports_GetCategory_Constella", ClsDBUtility.ObjectEnum.DataTable);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public DataTable GetUsers()
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    //ClsUtility.AddParameters("@CategoryName", SqlDbType.VarChar, CategoryName.ToString().Trim());

                    return (DataTable)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetUserID_Constella", ClsDBUtility.ObjectEnum.DataTable);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        /// <summary>
        /// Method for get report title in a catetory
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <param name="ReportName"></param>
        /// <returns></returns>
        public DataTable GetReport(string CategoryName, string ReportName)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@CategoryName", SqlDbType.VarChar, CategoryName.ToString().Trim());
                    ClsUtility.AddParameters("@ReportName", SqlDbType.VarChar, ReportName.ToString().Trim());

                    return (DataTable)ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReports_GetReport_Constella", ClsDBUtility.ObjectEnum.DataTable);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        
        /// <summary>
        /// Method for get possible joins of two views.
        /// </summary>
        /// <param name="View1"></param>
        /// <param name="View2"></param>
        /// <returns></returns>

        public DataTable GetCustomReportJoin(string View1, string View2, Int16 Loc)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    ClsUtility.AddParameters("@View1", SqlDbType.VarChar, View1);
                    ClsUtility.AddParameters("@View2", SqlDbType.VarChar, View2);
                    ClsUtility.AddParameters("@Loc", SqlDbType.Int, Loc.ToString());
                    return (DataTable)ReportManager.ReturnObject(ClsUtility.theParams, "pr_CustomReports_GetJoin_Constella", ClsDBUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    throw;
                }
            }

        }
        /// <summary>
        /// Get All the data for HIV exposed infants Reports
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="LocationId"></param>
        /// <returns></returns>
        public DataSet GetExposedInfantsData(DateTime StartDate, DateTime EndDate, int LocationId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_ExposedInfantsTrack1_Futures", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }

        }
        /// <summary>
        /// Get All the data for MR report nigeria
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="LocationId"></param>
        /// <returns></returns>
        public DataSet GetMRReportData(DateTime StartDate, DateTime EndDate, int LocationId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject ReportManager = new ClsObject();
                    ClsUtility.AddParameters("@StartDate_", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate_", SqlDbType.DateTime, EndDate.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_MRReport_Futures", ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }

        }
        public DataSet GetOGACData(DateTime StartDate, DateTime EndDate, int LocationId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();

                ClsObject ReportManager = new ClsObject();

                ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());


                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_OGAC_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }

        }


        public DataTable CheckEnrollmentValidity(string enrollmentNumber)
        {
            lock (this)
            {
                ClsObject ReportManager = new ClsObject();
                try
                {
                    this.Connection = DataMgr.GetConnection();
                    ReportManager.Connection = this.Connection;
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    ReportManager.Transaction = this.Transaction;

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@enrollmentNumber", SqlDbType.Int, enrollmentNumber.ToString());
                    return (DataTable)ReportManager.ReturnObject(ClsUtility.theParams, "Pr_Reports_CheckValidEnrollmentId_Constella", ClsDBUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }


        public DataSet GetUgandaMOHMonthlyReport(DateTime StartDate, DateTime EndDate, int LocationId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, StartDate.ToString());
                    ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, EndDate.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());


                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetUgandaMOHMonthlyReport_Futures", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }

        public DataSet GetTanzaniaPMTCTMonthlyMoHReport(int MonthId, int Year, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    ClsUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetTanzaniaPMTCTMonthlyMoHReport_Futures", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetKenyaMonthlyReport(int MonthId, int Year, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    ClsUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetKenya711BMonthlyReport_Futures", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        public DataSet GetNNRIMSFacilityMonthlyReport(int MonthId, int Year, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    ClsUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());

                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_NNRIMSFacilityMonthlyReport_Futures", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// GetBornToLive:To get all the data for Born To live Pmtct report
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="UserId"></param>
        /// <param name="LocationID"></param>
        /// <returns></returns>
        public DataSet GetBornToLive(int MonthId, int Year, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    ClsUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_BornToLive_Futures", ClsDBUtility.ObjectEnum.DataSet);
                }

                catch
                {
                    throw;
                }
            }

        }
        public DataSet GetPatientNascop(int MonthId, int Year, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsObject ReportManager = new ClsObject();

                    ClsUtility.AddParameters("@MonthId", SqlDbType.DateTime, MonthId.ToString());
                    ClsUtility.AddParameters("@Year", SqlDbType.DateTime, Year.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_NASCOP_Futures", ClsDBUtility.ObjectEnum.DataSet);
                }

                catch
                {
                    throw;
                }
            }
        }
        //-----------QueryBuilderReports-----------------------
        public DataTable GetReportsCategory()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject theQB = new ClsObject();
                return (DataTable)theQB.ReturnObject(ClsUtility.theParams, "pr_GetReportsCategory_Futures", ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetCustomReports(Int32 CategoryId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject theQB = new ClsObject();
                ClsUtility.AddParameters("@CategoryId", SqlDbType.Int, CategoryId.ToString());
                return (DataTable)theQB.ReturnObject(ClsUtility.theParams, "pr_GetCustomReports_Futures", ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        public DataSet ReturnQueryResult(string theQuery)
        {
            lock (this)
            {
                ClsObject theQB = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                return (DataSet)theQB.ReturnObject(ClsUtility.theParams, "pr_General_SQL_Parse", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
         

        /// <summary>
        /// get blue cart info by ptnpk
        /// added on 7 jully 2011
        /// </summary>
        /// <param name="patientid"></param>
        /// <returns></returns>
        public DataSet GetbluecartIEFUinfo(Int32 patientid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject theQB = new ClsObject();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@Key", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                return (DataSet)theQB.ReturnObject(ClsUtility.theParams, "pr_Reports_GetKenyaMOHCard_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }

       }

        public DataSet GetFacilityPatientsCostPerMonth(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                ClsUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetFacilityPatientsCostPerMonth", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityAvgCD4CostPerPatient(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                ClsUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetFacilityAvgCD4CostPerPatient", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityAvgExcludingCD4CostPerPatient(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                ClsUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetFacilityAvgExcludingCD4CostPerPatient", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityTotalAvgCostofARVandOIPerPatientPerMonth(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                ClsUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetFacilityTotalAvgCostofARVandOIPerPatientPerMonth", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityCumulAvgCostofARVandOIPerPatientPerMonth(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                ClsUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetFacilityCumulAvgCostofARVandOIPerPatientPerMonth", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityTotalCostLostToFollowup(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                ClsUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetFacilityTotalCostLostToFollowup", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityCumTotalCostLostToFollowup(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                ClsUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetFacilityCumTotalCostLostToFollowup", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityAvgCostCovByProgramAndPatient(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                ClsUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetFacilityAvgCostCovByProgramAndPatient", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetFacilityArvAvgCostCovByProgramAndPatient(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                ClsUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetFacilityArvAvgCostCovByProgramAndPatient", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetFacilityCumCostCovByProgramAndPatient(DateTime TransactionstartDate, DateTime TransactionEndDate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@TransactionStartDt", SqlDbType.DateTime, TransactionstartDate.ToString());
                ClsUtility.AddParameters("@TransactionEndDt", SqlDbType.DateTime, TransactionEndDate.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_GetFacilityCumCostCovByProgramAndPatient", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientDebitNoteTotalCostByMonth(Int32 PatientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatientDebitNoteTotalCostByMonth", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        #endregion
        #region "Billing Module"
        SqlDbType GetSqlDBTypeFromstring(string paramType)
        {
            SqlDbType dbtype;
            switch (paramType.ToLower())
            {
                case "nvarchar":
                case "varchar":
                case "string":
                case "text":
                    dbtype = SqlDbType.VarChar;
                    break;
                case "int":
                case "int32":
                case "int64":
                case "int16":
                case "whole number":
                    dbtype = SqlDbType.Int;
                    break;
                case "datetime":
                case "datetime2":
                case "date":
                    dbtype = SqlDbType.DateTime;
                    break;
                case "decimal":
                case "numeric":
                case "float":
                case "decimal number":
                    dbtype = SqlDbType.Decimal;
                    break;
                default:
                    dbtype = SqlDbType.VarChar;
                    break;

            }
            return dbtype;
        }
        /// <summary>
        /// Returns the query result.
        /// </summary>
        /// <param name="theQuery">The query.</param>
        /// <param name="paramTable">The parameter table.</param>
        /// <returns></returns>
        public DataSet ReturnQueryResult(string theQuery, string paramTable)
        {
            lock (this)
            {
                ClsObject theQB = new ClsObject();
                ClsUtility.Init_Hashtable();

                System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
                xml.LoadXml(paramTable);
                XmlElement documentElement = xml.DocumentElement;
                XmlNodeList x = documentElement.SelectNodes("//parameter");//xml.SelectNodes("parameters");
                XmlNodeList x1 = xml.SelectNodes("parameter");

                foreach (XmlNode node in x)
                {
                    string pName = node["name"].InnerXml;
                    string pValue = node["value"].InnerXml;
                    string pType = node["type"].InnerXml;                    
                    SqlDbType sqlDBType = GetSqlDBTypeFromstring(pType);                   
                    ClsUtility.AddParameters(pName, sqlDBType, pValue);
                }
              
                return theQB.ReturnObject(ClsUtility.theParams, theQuery, ClsDBUtility.ObjectEnum.DataSet) as DataSet;
                
            }
        }
        public DataTable GetQueryBuilderReportParameters(string Report_ID)
        {
            // string queryText = "";
            ClsObject theQB = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Report_ID", SqlDbType.Int, Report_ID.ToString());
            DataTable dataTable = (DataTable)theQB.ReturnObject(ClsUtility.theParams,
            @"Select P.ParameterName, P.ParameterDataType As DataType From dbo.MST_QueryBuilderParameters P Where P.ReportID= @Report_ID",
            ClsDBUtility.ObjectEnum.DataTable);
            //  queryText = dataRow[0].ToString();

            return dataTable;

        }
        public DataTable GetQueryBuilderReportQuery(string Report_ID)
        {
            // string queryText = "";
            ClsObject theQB = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Report_ID", SqlDbType.Int, Report_ID.ToString());
            DataTable dataTable = (DataTable)theQB.ReturnObject(ClsUtility.theParams,
            @"Select	Min(ReportQuery) ReportQuery,
		ReportName,
		Count(P.ParameterName) As HasParameters
From dbo.mst_QueryBuilderReports R
Left Outer Join dbo.MST_QueryBuilderParameters P
	On R.ReportId = P.ReportID And R.ReportId = @Report_ID
Where R.ReportId = @Report_ID
Group By R.ReportName",
            ClsDBUtility.ObjectEnum.DataTable);            

            return dataTable;

        }
        #endregion
    }
}
