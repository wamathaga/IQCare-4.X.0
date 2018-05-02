using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using System.Data;
using Application.Common;
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
/////////////////////////////////////////////////////////////////////
// Code Written By   : Rakhi Tyagi
// Written Date      : 1 Sept 2006
// Modification Date : 30 Oct 2006
// Description       : Add/Edit UserGroup 
// Modification Date : 16 Feb 2007
/// /////////////////////////////////////////////////////////////////


namespace BusinessProcess.Administration
{
    public class BUserRole : ProcessBase, IUserRole
    {

        #region "Constructor"

        public BUserRole()
        {
        }

        #endregion

        #region Get UserRole List
        public DataSet GetUserRoleList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserRoleManager = new ClsObject();
                return (DataSet)UserRoleManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectGroup_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
        #endregion

        #region Get UserGroupFeature List
        public DataSet GetUserGroupFeatureList(Int32 theSID,Int32 theFID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserRoleManager = new ClsObject();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, theSID.ToString());
                ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, theFID.ToString());
                return (DataSet)UserRoleManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectFeature_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
        #endregion

        #region Get UserGroupFeatureList By ID
        public DataSet GetUserGroupFeatureListByID(int UserID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("GroupID", SqlDbType.Int, UserID.ToString());
                ClsObject UserRoleManager = new ClsObject();
                return (DataSet)UserRoleManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectUserGroupDetailByID_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
            

        }
        #endregion

        #region Add New UserGroupFeature

        public int SaveUserGroupDetail(int GroupID, String Groupname, DataSet theDS, int UserID, int Flag, int EnrollmentFlag, int PreCareEnd, int EditIdentifiers)
        {
            ClsObject UserGroupManager = new ClsObject();
            
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                UserGroupManager.Connection = this.Connection;
                UserGroupManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                DataRow theDR ;
                ClsUtility.AddParameters("@GID", SqlDbType.Int, GroupID.ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, Flag.ToString());
                ClsUtility.AddParameters("@GroupName", SqlDbType.VarChar, Groupname);
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@PerEnrollment", SqlDbType.Int, EnrollmentFlag.ToString());
                ClsUtility.AddParameters("@PerCareEnd", SqlDbType.Int, PreCareEnd.ToString());
                ClsUtility.AddParameters("@EditIdentifiers", SqlDbType.Int, EditIdentifiers.ToString());
                theDR = (DataRow)UserGroupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveUserGroup_Detail_Constella", ClsDBUtility.ObjectEnum.DataRow);
                int GroupId = Convert.ToInt32(theDR[0].ToString());
                if (GroupId != 0)
                {
                    for (int i = 0; i < theDS.Tables[0].Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@FacilityID", SqlDbType.Int, theDS.Tables[0].Rows[i]["FacilityID"].ToString());
                        ClsUtility.AddParameters("@ModuleID", SqlDbType.Int, theDS.Tables[0].Rows[i]["ModuleID"].ToString());
                        ClsUtility.AddParameters("@FeatureID", SqlDbType.Int, theDS.Tables[0].Rows[i]["FeatureID"].ToString());
                        ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, theDS.Tables[0].Rows[i]["FeatureName"].ToString());
                        ClsUtility.AddParameters("@TabID", SqlDbType.Int, theDS.Tables[0].Rows[i]["TabID"].ToString());
                        ClsUtility.AddParameters("@FunctionID", SqlDbType.Int, theDS.Tables[0].Rows[i]["FunctionID"].ToString());
                        ClsUtility.AddParameters("@GroupID", SqlDbType.Int, GroupId.ToString());
                        int RowsAffected = (int)UserGroupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveFacilityServiceUserGroupFunction_Detail", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        if (RowsAffected == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving UserGroupRole. Try Again..";
                            AppException.Create("#C1", theMsg);
                        }

                    }
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return Convert.ToInt32(theDR[0]);
              

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                UserGroupManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        #endregion

        #region "Update UserGroup"
        public void UpdateUserGroup(int GroupId, String Groupname, DataSet theDS, int UserID, int Flag, int EnrollmentFlag, int PreCareEnd,int EditIdentifiers)
        {
            ClsObject UserGroupManager = new ClsObject();
            DataRow theDR;
            try
            {
                int theRowsAffected = 0;
                ClsUtility.Init_Hashtable();
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                UserGroupManager.Connection = this.Connection;
                UserGroupManager.Transaction = this.Transaction;

                #region "Update UserGroup"

                ClsUtility.AddParameters("@GID", SqlDbType.Int, GroupId.ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, Flag.ToString());
                ClsUtility.AddParameters("@GroupName", SqlDbType.VarChar, Groupname);
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@PerEnrollment", SqlDbType.Int, EnrollmentFlag.ToString());
                ClsUtility.AddParameters("@PerCareEnd", SqlDbType.Int, PreCareEnd.ToString());
                ClsUtility.AddParameters("@EditIdentifiers", SqlDbType.Int, EditIdentifiers.ToString());
                theDR = (DataRow)UserGroupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveUserGroup_Detail_Constella", ClsDBUtility.ObjectEnum.DataRow);
                if (theDR[0].ToString() == "")
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating UserGroup. Try Again..";
                    AppException.Create("#C1", theMsg);
                }

                #endregion

                /************   Delete Previous Records **********/

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Original_GroupID", SqlDbType.Int, GroupId.ToString());
                
                theRowsAffected = (int)UserGroupManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteGroupFeature_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowsAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating UserGroupRole. Try Again..";
                    AppException.Create("#C1", theMsg);

                }
                #region "Insert Records"
                for (int i = 0; i < theDS.Tables[0].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FacilityID", SqlDbType.Int, theDS.Tables[0].Rows[i]["FacilityID"].ToString());
                    ClsUtility.AddParameters("@ModuleID", SqlDbType.Int, theDS.Tables[0].Rows[i]["ModuleID"].ToString());
                    ClsUtility.AddParameters("@FeatureID", SqlDbType.Int, theDS.Tables[0].Rows[i]["FeatureID"].ToString());
                    ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, theDS.Tables[0].Rows[i]["FeatureName"].ToString());
                    ClsUtility.AddParameters("@TabID", SqlDbType.Int, theDS.Tables[0].Rows[i]["TabID"].ToString());
                    ClsUtility.AddParameters("@FunctionID", SqlDbType.Int, theDS.Tables[0].Rows[i]["FunctionID"].ToString());
                    ClsUtility.AddParameters("@GroupID", SqlDbType.Int, GroupId.ToString());

                    int RowsAffected = (int)UserGroupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveFacilityServiceUserGroupFunction_Detail", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    if (RowsAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving UserGroupRole. Try Again..";
                        AppException.Create("#C1", theMsg);

                    }

                }
                #endregion
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
                UserGroupManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        #endregion

        public DataSet GetSavedData()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserRoleManager = new ClsObject();
                return (DataSet)UserRoleManager.ReturnObject(ClsUtility.theParams, "pr_GetFacilityServiceData", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
 
    }  

}


