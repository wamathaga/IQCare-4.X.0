using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.ServiceProcess;
//for auto backup service
using System.Threading;
using Application.BusinessProcess;
using Application.Common;

namespace RemServer.Service
{
    public class IQCareService : ServiceBase
    {
        private System.ComponentModel.Container theContainer = null;
        private static EventLog theLog = new EventLog();
        public static string theSRV_Name = "IQCare";
        SqlConnection cnBKTest;
        public DateTime dtBackupTime;
        public string strBackupDrive;
        public string constr;
        System.Threading.Timer oTimer;
        private static List<Facility> _facilities;

        public IQCareService()
        {
            this.ServiceName = theSRV_Name;
        }
        private Utility clsUtil;
        /// <summary>
        /// Gets the facility list.
        /// </summary>
        /// <value>
        /// The facility list.
        /// </value>
        private List<Facility> FacilityList
        {
            get
            {
                if (_facilities == null)
                {
                    FacilityDetails();
                }
                return _facilities;
            }
        }
        static void Main()
        {
            try
            {
                //theLog.Log = "IQCare";
                //theLog.MaximumKilobytes = 4096;
                theLog.Source = theSRV_Name;
                theLog.WriteEntry(string.Format("{0} Initializing", theSRV_Name));
                ServiceBase.Run(new IQCareService());
            }
            catch (Exception err)
            {
                theLog.WriteEntry(err.ToString());
            }
        }

        private void InitializeComponent()
        {
            theContainer = new System.ComponentModel.Container();
            this.ServiceName = "IQCare";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (theContainer != null)
                {
                    theContainer.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        protected override void OnStart(string[] args)
        {
            try
            {

                Process theProc = Process.GetCurrentProcess();
                string Config = theProc.MainModule.FileName;
                Config = Config + ".config";

                #region "Connection Parameters"
                Utility clsUtil = new Utility();
                constr = clsUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
                if (constr.Trim() == "")
                {
                    frmConnection frm = new frmConnection();
                    frm.ShowDialog();
                    constr = ((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"];
                    if (constr == "")
                    {
                        Environment.Exit(1);
                    }
                }
                #endregion
                DoDelayedTasks();
                RemotingConfiguration.Configure(Config);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(BusinessServerFactory), "BusinessProcess.rem", WellKnownObjectMode.Singleton);
                //theLog.WriteEntry(string.Format("{0} Started", theSRV_Name));
            }
            catch (Exception err)
            {
                theLog.WriteEntry(err.Message + err.StackTrace);
            }
        }

        /// <summary>
        /// This process will pick the backup time and backdrive from database.
        /// </summary>
        public void DoDelayedTasks()
        {
            cnBKTest = new SqlConnection(constr);
            const Int32 iTIME_INTERVAL = 50000; //    ' 50 seconds.
            TimerCallback timerDelegate = new TimerCallback(DBEntry);
            oTimer = new System.Threading.Timer(timerDelegate, null, 0, iTIME_INTERVAL);
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                //theLog.WriteEntry(string.Format("{0} Stopped.", theSRV_Name));
            }
            catch (Exception err)
            {
                theLog.WriteEntry(err.Message);
            }
        }
        /// <summary>
        /// Iqs the tools refresh.
        /// </summary>
        private void IQToolsRefresh()
        {
            try
            {
                string strIQToolsConnection = "";
                string strIQToolsInit = "";
                DateTime dateNextRefreshDate = DateTime.Now;
                if (clsUtil == null) clsUtil = new Utility();
                bool isError=false;
                try
                {
                    strIQToolsConnection = clsUtil.Decrypt(ConfigurationManager.AppSettings.Get("IQToolsConnectionString"));                    
                }
                catch (Exception e1)
                {
                    theLog.WriteEntry(e1.Message + e1.StackTrace + " IQToolsConnectionString");
                    isError = true;
                }
               try
                {
                    strIQToolsInit = clsUtil.Decrypt(ConfigurationManager.AppSettings["IQToolsInitializationProcedures"]);                    
                }
               catch (Exception e1)
               {
                   theLog.WriteEntry(e1.Message + e1.StackTrace + " IQToolsInitializationProcedures");
                   isError = true;
               }
                //if (ConfigurationManager.AppSettings["IQToolsNextRefreshDateTime"] != null)
                //{
                    try
                    {
                        //XmlConvert.ToDateTime()
                        dateNextRefreshDate = Convert.ToDateTime(ConfigurationManager.AppSettings["IQToolsNextRefreshDateTime"]);
                    }
                    catch (Exception e1)
                    {
                        theLog.WriteEntry(e1.Message + e1.StackTrace +" IQToolsNextRefreshDateTime" );
                        isError = true;
                    }
                //}
                    if (isError) throw new Exception("Errocurred when parsing the config file");
                if (dateNextRefreshDate <= DateTime.Now && strIQToolsConnection != "")
                {
                    //set next update time
                    //XmlNode nodeIQToolsRefreshTime = doc.SelectSingleNode("//appSettings/add[@key='IQToolsNextRefreshDateTime']");
                    //if (nodeIQToolsRefreshTime != null)
                    //{
                    int refreshInterval = 120;
                    //if(refreshIntervalText.v)
                    // XmlNode nodeIQToolsRefreshInterval = doc.SelectSingleNode("//appSettings/add[@key='IQToolsRefreshInterval']");

                    int.TryParse(ConfigurationManager.AppSettings["IQToolsRefreshInterval"], out refreshInterval);


                    string strIQToolsRefreshTime = (DateTime.Now.AddMinutes(refreshInterval)).ToString("yyyy-MM-dd HH:mm:ss");
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["IQToolsNextRefreshDateTime"].Value = strIQToolsRefreshTime;
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");

                    SqlConnection connectionIQtools = new SqlConnection(strIQToolsConnection);
                    connectionIQtools.Open();
                    SqlCommand command;

                    string[] procedures = strIQToolsInit.Split(';');
                    string facilityName = FacilityList[0].Name;
                    foreach (string procedure in procedures)
                    {
                        try
                        {
                            command = new SqlCommand(procedure, connectionIQtools);
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 0;
                            SqlCommandBuilder.DeriveParameters(command);
                            command.Parameters["@EMR"].Value = "IQCare";
                            command.Parameters["@FacilityName"].Value = facilityName;
                            if (command.Parameters.Contains("@PatientPK"))
                                command.Parameters["@PatientPK"].Value = 0;
                            if (command.Parameters.Contains("@EMRVersion"))
                                command.Parameters["@EMRVersion"].Value = "3.6";
                            if (command.Parameters.Contains("@VisitPK"))
                                command.Parameters["@VisitPK"].Value = 0;
                            command.ExecuteNonQuery();
                        }
                        catch (Exception e2)
                        {
                            theLog.WriteEntry(e2.Message + e2.StackTrace + procedure);
                        }
                    }

                    connectionIQtools.Close();
                    connectionIQtools.Dispose();
                }
            }
            catch (Exception e0)
            {
                theLog.WriteEntry(e0.Message + e0.StackTrace);
            }
        }


        /// <summary>
        /// For automated backup service.
        /// This function takes the backup when server time matches with user specified time.
        /// </summary>
        /// <param name="Message"></param>

        public void DBEntry(Object Message)
        {
            try
            {
                SqlCommand cmdTest;
                cmdTest = new SqlCommand("pr_SystemAdmin_GetBackupTime_Constella", cnBKTest);
                cmdTest.CommandType = CommandType.StoredProcedure;
               // int theTimeOut = Convert.ToInt32(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["CommandTimeOut"]);
                int theTimeOut = int.Parse(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                cmdTest.CommandTimeout = theTimeOut;
                cnBKTest.Open();
                SqlDataReader readerBackupDetail;
                readerBackupDetail = cmdTest.ExecuteReader();
                if (readerBackupDetail.HasRows)
                {
                    readerBackupDetail.Read();
                    if (readerBackupDetail["BackupTime"].ToString() != "" || readerBackupDetail.IsDBNull(0) != true)
                        dtBackupTime = (DateTime)readerBackupDetail["BackupTime"];
                    if (readerBackupDetail["BackupDrive"].ToString() != "" || readerBackupDetail.IsDBNull(1) != true)
                        strBackupDrive = (string)readerBackupDetail["BackupDrive"];

                }
                cnBKTest.Close();

                if (dtBackupTime.ToString("hh:mm") == DateTime.Now.ToString("hh:mm"))
                {
                    //this.RequestAdditionalTime(50000);
                    cmdTest = new SqlCommand("pr_SystemAdmin_Backup_Constella", cnBKTest);
                    cmdTest.CommandType = CommandType.StoredProcedure;
                    cmdTest.Parameters.Add(new SqlParameter("@FileName", SqlDbType.VarChar, 500));
                    cmdTest.Parameters["@FileName"].Value = strBackupDrive + "\\IQCareDBBackup";
                    cmdTest.Parameters.Add("@Deidentified", SqlDbType.Int).Value = 0;
                    cmdTest.Parameters.Add("@LocationId", SqlDbType.Int).Value = 0;
                    cmdTest.Parameters.Add("@dbKey", SqlDbType.VarChar).Value = ApplicationAccess.DBSecurity.ToString();
                    cmdTest.CommandTimeout = theTimeOut;
                    cnBKTest.Open();
                    cmdTest.ExecuteNonQuery();
                }
                cnBKTest.Close();

                /*
                 * Commented by Gaurav & Suggested by Joseph 
                 * Purpose: Update appoinments based on date
                 * Date: 23 Sept 2014
                 */
                try
                {
                    if (ConfigurationManager.AppSettings["AppointmentNextUpdate"] != null)
                    {
                        DateTime dateNextRefreshDate = Convert.ToDateTime(ConfigurationManager.AppSettings["AppointmentNextUpdate"]);
                        if (dateNextRefreshDate <= DateTime.Now)
                        {
                            //update the AppointmentNextUpdate - to avoid run on subsequent poll
                            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                            config.AppSettings.Settings["AppointmentNextUpdate"].Value = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                            config.Save(ConfigurationSaveMode.Modified);
                            ConfigurationManager.RefreshSection("appSettings");
                            //update appointment
                            foreach (Facility f in FacilityList)
                            {
                                UpdateAppointment(f.ID, dateNextRefreshDate.ToString("yyyy-MMM-dd"));
                            }


                        }
                    }
                }
                catch (Exception err)
                {
                    cnBKTest.Close();
                    theLog.WriteEntry(err.Message + err.StackTrace);
                }
            }
            catch (Exception err)
            {
                cnBKTest.Close();
                theLog.WriteEntry(err.Message);
            }
            IQToolsRefresh();
        }

        /// <summary>
        /// Updates the appointment.
        /// </summary>
        private void UpdateAppointment(int facilityID, string _dateTime)
        {
            //*******Update appointment status priviously missed, missed, careended and met from pending*******//pr_Scheduler_UpdateAppointmentStatusMissedAndMet_Constella                
            using (SqlCommand command = new SqlCommand("pr_Scheduler_UpdateAppointmentStatusMissedAndMet_Constella", cnBKTest))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@locationid", facilityID));
                command.Parameters.Add(new SqlParameter("@Currentdate", _dateTime));
                if (cnBKTest.State == ConnectionState.Closed)
                    cnBKTest.Open();
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Facilities the name.
        /// </summary>
        /// <returns></returns>
        private void FacilityDetails()
        {
            if (_facilities == null)
            {
                SqlCommand cmdTest;
                cnBKTest = new SqlConnection(this.constr);
                cmdTest = new SqlCommand(@"Select F.FacilityID, F.FacilityName From dbo.mst_Facility F Where F.DeleteFlag = 0 Or F.DeleteFlag Is Null Order By 1", cnBKTest);
                cnBKTest.Open();
                IDataReader dr = cmdTest.ExecuteReader(CommandBehavior.Default);
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();
                _facilities = new List<Facility>();
                foreach (DataRow row in dt.Rows)
                {
                    _facilities.Add(new Facility { ID = int.Parse(row["FacilityID"].ToString()), Name = row["FacilityName"].ToString() });
                }

            }
            // return _facilities;
        }
    }
    class Facility
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; internal set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; internal set; }
    }
}
