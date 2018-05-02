using System;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Security;

namespace BusinessProcess.Security
{
    public class BUser : ProcessBase,IUser 
    {

        #region "Constructor"
        public BUser()
        {
        }
        #endregion

        #region "Application Settings"

        public DataTable GetFacilityList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject FacilityManager = new ClsObject();
                return (DataTable)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetFacilityCmbList_Constella", ClsDBUtility.ObjectEnum.DataTable);
            }
        }


        public DataSet GetFacilitySettings(int SystemId)
        {
            lock (this)
            {
                try
                {

                ClsObject FacilityManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectFacility_Constella", ClsDBUtility.ObjectEnum.DataSet);

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        #endregion

        #region "Login Functions"

        public DataSet GetUserCredentials(string UserName,int LocationId, int SystemId)
        {
            lock (this)
            {
                ClsObject LoginManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LoginName", SqlDbType.VarChar, UserName);
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataSet)LoginManager.ReturnObject(ClsUtility.theParams, "Pr_Security_UserLogin_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable GetEmployeeDetails()
        {
            lock (this)
            {
                ClsObject LoginManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataTable)LoginManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetEmployeeDetails_Constella", ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        public int UpdateAppointmentStatus(string Currentdate,int locationid )
        {
            lock (this)
            {
                ClsObject LoginManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Currentdate", SqlDbType.VarChar, Currentdate.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, locationid.ToString());
                return (int)LoginManager.ReturnObject(ClsUtility.theParams, "pr_Scheduler_UpdateAppointmentStatusMissedAndMet_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
            }
        }

        public DataSet GetAuditData()
        {
            lock (this)
            {
                ClsObject LoginManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)LoginManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetAuditTrailData_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
      #endregion
    }
}
