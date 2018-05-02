using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Administration
{
    public class BUser : ProcessBase,Iuser 
    {
        #region "Constructor"
        public BUser()
        {
        }
        #endregion

        #region "User List"

        public DataSet GetUserList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetUserList_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "Create User"
        public DataSet FillDropDowns()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetDropDownData_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveNewUser(bool isEmployee, string FName, string LName, string UserName, string Password, string Email, string Phone, int UserId, int Designation, Hashtable UserGroup, Hashtable Store) 
        {
            ClsObject UserManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                                
                UserManager.Connection = this.Connection;
                UserManager.Transaction = this.Transaction;

                Utility theUtil = new Utility();
                Password = theUtil.Encrypt(Password);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@isemp", SqlDbType.Bit, isEmployee.ToString());
                ClsUtility.AddParameters("@fname", SqlDbType.VarChar, FName);
                ClsUtility.AddParameters("@lname", SqlDbType.VarChar, LName);
                ClsUtility.AddParameters("@username", SqlDbType.VarChar, UserName);
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, Password);
                ClsUtility.AddParameters("@email", SqlDbType.VarChar, Email);
                ClsUtility.AddParameters("@phone", SqlDbType.VarChar, Phone);
                ClsUtility.AddParameters("@userid", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@Designation", SqlDbType.Int, Designation.ToString());
                DataRow theDR;
                theDR = (DataRow)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveNewUser_Constella", ClsDBUtility.ObjectEnum.DataRow);
                if (Convert.ToInt32(theDR[0]) == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving User Record. Try Again..";
                    AppException.Create("#C1", theBL);
                    return Convert.ToInt32(theDR[0]); 
                }

                #region "Insert Groups and Stores"
                int i = 1;
                for (i = 1; i <= UserGroup.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@UserId",SqlDbType.Int, theDR[0].ToString());
                    ClsUtility.AddParameters("@GroupId",SqlDbType.Int,UserGroup[i].ToString());
                    ClsUtility.AddParameters("@OperatorId", SqlDbType.Int, UserId.ToString());
                    UserManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertUserGroup_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                int k = 1;
                for (k = 1; k <= Store.Count; k++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, theDR[0].ToString());
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, Store[k].ToString());
                    UserManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertStoreUser_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                #endregion

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
                UserManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection); 
            }
        }

        public DataSet GetUserRecord(int UserId)
        {
            lock (this)
            {
                ClsObject UserManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectUser_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public void UpdateUserRecord(bool isEmployee, string FName, string LName, string UserName, string Password, string Email, string Phone, int UserId, int OperatorId, int Designation, Hashtable UserGroup, Hashtable Store)
        {
            ClsObject UserManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                int RowsAffected = 0;

                Utility theUtil = new Utility();
                Password = theUtil.Encrypt(Password);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@IsEmployee", SqlDbType.Bit, isEmployee.ToString());
                ClsUtility.AddParameters("@UserLastName",SqlDbType.VarChar,LName);
                ClsUtility.AddParameters("@UserFirstName",SqlDbType.VarChar,FName);
                ClsUtility.AddParameters("@username", SqlDbType.VarChar, UserName);
                ClsUtility.AddParameters("@Password",SqlDbType.VarChar,Password);
                ClsUtility.AddParameters("@Email", SqlDbType.VarChar, Email);
                ClsUtility.AddParameters("@Phone", SqlDbType.VarChar, Phone);
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@OperatorID",SqlDbType.Int,OperatorId.ToString());
                ClsUtility.AddParameters("@Designation", SqlDbType.Int, Designation.ToString());
               // ClsUtility.AddParameters("@EmpId", SqlDbType.Int, EmpId.ToString());
                RowsAffected = (int)UserManager.ReturnObject(ClsUtility.theParams, "pr_Admin_UpdateUser_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected < 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Updating User Record. Try Again..";
                    AppException.Create("#C1", theBL);
                }

                #region "Update User Groups and Store User"
                string theSQL = string.Format("Delete from Lnk_UserGroup where UserId = {0}", UserId);
                ClsUtility.Init_Hashtable();
                RowsAffected = (int)UserManager.ReturnObject(ClsUtility.theParams, theSQL, ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                int i = 1;
                for (i = 1; i <= UserGroup.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@GroupId", SqlDbType.Int, UserGroup[i].ToString());
                    ClsUtility.AddParameters("@OperatorId", SqlDbType.Int, UserId.ToString());
                    UserManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertUserGroup_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                theSQL = string.Format("Delete from Lnk_StoreUser where UserId = {0}", UserId);
                ClsUtility.Init_Hashtable();
                RowsAffected = (int)UserManager.ReturnObject(ClsUtility.theParams, theSQL, ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                int k = 1;
                for (k = 1; k <= Store.Count; k++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, Store[k].ToString());
                    UserManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertStoreUser_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                UserManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int DeleteUserRecord(int UserId)
        {
            ClsObject UserManager = new ClsObject();
            int theAffectedRow = 0;
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                theAffectedRow = (int)UserManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteUser_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                return theAffectedRow;
                UserManager = null;
            }
            catch
            {
                throw;
            }
            finally
            {
                UserManager = null;
                
            }

        }
       #endregion

        #region "ptrn_lock"
        public void SaveUserLock(int UserId, int locationID, string code, string lastURL)
        {
            ClsObject UserManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                int RowsAffected = 0;

                Utility theUtil = new Utility();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                ClsUtility.AddParameters("@code", SqlDbType.VarChar, code);
                ClsUtility.AddParameters("@lastURL", SqlDbType.VarChar, lastURL);

                RowsAffected = (int)UserManager.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_ptrnLock_Update", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected < 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Updating User Record. Try Again..";
                    AppException.Create("#C1", theBL);
                }

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
                UserManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataSet GetUserLock(int UserId)
        {
            lock (this)
            {
                ClsObject UserManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_ptrnLock_Get", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        #endregion
    }
}
