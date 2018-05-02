using System;
using System.Data;
using System.Data.SqlClient;
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using System.Collections;
namespace BusinessProcess.Administration
{
    public class BFacility : ProcessBase, IFacilitySetup
    {
        #region "Constructor"
        public BFacility()
        {
        }
        #endregion

        public DataSet GetFacilityList(int SystemId, int FeatureId, int ModuleId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsObject FacilityManager = new ClsObject();
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetFacilityList_Constella", ClsDBUtility.ObjectEnum.DataSet);
                FacilityManager = null;
            }
        }

        public DataSet GetSystemBasedLabels(int SystemId, int FeatureId, int ModuleId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureId.ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsObject FacilityManager = new ClsObject();
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_SystemAdmin_GetSystemBasedLabels_Constella", ClsDBUtility.ObjectEnum.DataSet);
                FacilityManager = null;
            }
        }

        public DataSet GetFacility(int SystemId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject FacilityManager = new ClsObject();
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectFacility_Constella", ClsDBUtility.ObjectEnum.DataSet);
                FacilityManager = null;
            }
        }

        public int SaveNewFacility(string FacilityName, string CountryID, string PosID, string SatelliteID, string NationalID, int ProvinceId, int DistrictId, string image, int currency, int AppGracePeriod, string dateformat, DateTime PepFarStartDate, int SystemId, int thePreferred, int Paperless, int UserID, DataTable dtModule, Hashtable ht, DataTable dtStore = null)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FacilityManager = new ClsObject();
                FacilityManager.Connection = this.Connection;
                FacilityManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FacilityName", SqlDbType.VarChar, FacilityName);
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, CountryID.ToString());
                ClsUtility.AddParameters("@PosID", SqlDbType.VarChar, PosID.ToString());
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, SatelliteID.ToString());
                ClsUtility.AddParameters("@NationalID", SqlDbType.VarChar, NationalID.ToString());
                ClsUtility.AddParameters("@ProvinceId", SqlDbType.Int, ProvinceId.ToString());
                ClsUtility.AddParameters("@DistrictId", SqlDbType.Int, DistrictId.ToString());
                ClsUtility.AddParameters("@image", SqlDbType.VarChar, image);
                ClsUtility.AddParameters("@currency", SqlDbType.Int, currency.ToString());
                ClsUtility.AddParameters("@AppGracePeriod", SqlDbType.Int, AppGracePeriod.ToString());
                ClsUtility.AddParameters("@dateformat", SqlDbType.VarChar, dateformat.ToString());
                ClsUtility.AddParameters("@PepFarStartDate", SqlDbType.DateTime, PepFarStartDate.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@Preferred", SqlDbType.Int, thePreferred.ToString());
                ClsUtility.AddParameters("@Paperless", SqlDbType.Int, Paperless.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@FacilityLogo", SqlDbType.VarChar, ht["FacilityLogo"].ToString());
                ClsUtility.AddParameters("@FacilityAddress", SqlDbType.VarChar, ht["FacilityAddress"].ToString());
                ClsUtility.AddParameters("@FacilityTel", SqlDbType.VarChar, ht["FacilityTel"].ToString());
                ClsUtility.AddParameters("@FacilityCell", SqlDbType.VarChar, ht["FacilityCell"].ToString());
                ClsUtility.AddParameters("@FacilityFax", SqlDbType.VarChar, ht["FacilityFax"].ToString());
                ClsUtility.AddParameters("@FacilityEmail", SqlDbType.VarChar, ht["FacilityEmail"].ToString());
                ClsUtility.AddParameters("@FacilityURL", SqlDbType.VarChar, ht["FacilityURL"].ToString());
                ClsUtility.AddParameters("@FacilityFooter", SqlDbType.VarChar, ht["FacilityFootertext"].ToString());
                ClsUtility.AddParameters("@FacilityTemplate", SqlDbType.Int, ht["Facilitytemplate"].ToString());
                ClsUtility.AddParameters("@StrongPassword", SqlDbType.Int, ht["StrongPassword"].ToString());
                ClsUtility.AddParameters("@ExpirePaswordFlag", SqlDbType.Int, ht["ExpirePaswordFlag"].ToString());
                ClsUtility.AddParameters("@ExpirePaswordDays", SqlDbType.VarChar, ht["ExpirePaswordDays"].ToString());
                ClsUtility.AddParameters("@DateConstraint", SqlDbType.VarChar, ht["DateConstraint"].ToString());
                ClsUtility.AddParameters("@Billing", SqlDbType.Int, ht["Billing"].ToString());
                ClsUtility.AddParameters("@PMSCM", SqlDbType.Int, ht["PMSCM"].ToString());

                ClsUtility.AddParameters("@Wards", SqlDbType.Int, ht["Wards"].ToString());
                ClsUtility.AddParameters("@LMIS", SqlDbType.Int, ht["LMIS"].ToString());
               
                
                Int32 RowsAffected = (Int32)FacilityManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_InsertFacility_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected <= 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                    //Exception ex = AppException.Create("#C1", theBL);
                    //throw ex;
                    AppException.Create("#C1", theBL);
                }

                if (RowsAffected > 0)
                {
                    for (int i = 0; i < dtModule.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@FacilityID", SqlDbType.Int, "99999");
                        ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dtModule.Rows[i]["ModuleId"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                        ClsUtility.AddParameters("@Flag", SqlDbType.Int, "0");
                        int retval = (int)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SaveModule_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                        if (RowsAffected < 0)
                        {
                            MsgBuilder theBL = new MsgBuilder();
                            theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                            //Exception ex = AppException.Create("#C1", theBL);
                            //throw ex;
                            AppException.Create("#C1", theBL);
                        }

                    }

                    // Saving Stores
                    if (dtStore != null && dtStore.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtStore.Rows.Count; i++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@FacilityID", SqlDbType.Int, "99999");
                            ClsUtility.AddParameters("@StoreId", SqlDbType.Int, dtStore.Rows[i]["StoreId"].ToString());
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                            ClsUtility.AddParameters("@Flag", SqlDbType.Int, "0");
                            int retval = (int)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SaveStores_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                            if (RowsAffected < 0)
                            {
                                MsgBuilder theBL = new MsgBuilder();
                                theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                                //Exception ex = AppException.Create("#C1", theBL);
                                //throw ex;
                                AppException.Create("#C1", theBL);
                            }

                        }
                    }

                }
                FacilityManager = null;
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int UpdateFacility(int FacilityId, string FacilityName, string CountryID, string PosID, string SatelliteID, string NationalID, int ProvinceId, int DistrictId, string image, int currency, int AppGracePeriod, string dateformat, DateTime PepFarStartDate, int Status, int SystemId, int thePreferred, int Paperless, int UserID, DataTable dtModule, Hashtable ht, DataTable dtStore = null)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FacilityManager = new ClsObject();
                FacilityManager.Connection = this.Connection;
                FacilityManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FacilityName", SqlDbType.VarChar, FacilityName);
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, CountryID.ToString());
                ClsUtility.AddParameters("@PosID", SqlDbType.VarChar, PosID.ToString());
                ClsUtility.AddParameters("@SatelliteID", SqlDbType.VarChar, SatelliteID.ToString());
                ClsUtility.AddParameters("@NationalID", SqlDbType.VarChar, NationalID.ToString());
                ClsUtility.AddParameters("@ProvinceId", SqlDbType.Int, ProvinceId.ToString());
                ClsUtility.AddParameters("@DistrictId", SqlDbType.Int, DistrictId.ToString());
                ClsUtility.AddParameters("@image", SqlDbType.VarChar, image.ToString());
                ClsUtility.AddParameters("@currency", SqlDbType.Int, currency.ToString());
                ClsUtility.AddParameters("@AppGracePeriod", SqlDbType.Int, AppGracePeriod.ToString());
                ClsUtility.AddParameters("@dateformat", SqlDbType.VarChar, dateformat.ToString());
                ClsUtility.AddParameters("@PepFarStartDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", PepFarStartDate).ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.Int, Status.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@Preferred", SqlDbType.Int, thePreferred.ToString());
                ClsUtility.AddParameters("@Paperless", SqlDbType.Int, Paperless.ToString());
                ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, FacilityId.ToString());
                ClsUtility.AddParameters("@FacilityLogo", SqlDbType.VarChar, ht["FacilityLogo"].ToString());
                ClsUtility.AddParameters("@FacilityAddress", SqlDbType.VarChar, ht["FacilityAddress"].ToString());
                ClsUtility.AddParameters("@FacilityTel", SqlDbType.VarChar, ht["FacilityTel"].ToString());
                ClsUtility.AddParameters("@FacilityCell", SqlDbType.VarChar, ht["FacilityCell"].ToString());
                ClsUtility.AddParameters("@FacilityFax", SqlDbType.VarChar, ht["FacilityFax"].ToString());
                ClsUtility.AddParameters("@FacilityEmail", SqlDbType.VarChar, ht["FacilityEmail"].ToString());
                ClsUtility.AddParameters("@FacilityURL", SqlDbType.VarChar, ht["FacilityURL"].ToString());
                ClsUtility.AddParameters("@FacilityFooter", SqlDbType.VarChar, ht["FacilityFootertext"].ToString());
                ClsUtility.AddParameters("@FacilityTemplate", SqlDbType.Int, ht["Facilitytemplate"].ToString());
                ClsUtility.AddParameters("@StrongPassword", SqlDbType.Int, ht["StrongPassword"].ToString());
                ClsUtility.AddParameters("@ExpirePaswordFlag", SqlDbType.Int, ht["ExpirePaswordFlag"].ToString());
                ClsUtility.AddParameters("@ExpirePaswordDays", SqlDbType.VarChar, ht["ExpirePaswordDays"].ToString());
                ClsUtility.AddParameters("@DateConstraint", SqlDbType.VarChar, ht["DateConstraint"].ToString());
                ClsUtility.AddParameters("@Billing", SqlDbType.Int, ht["Billing"].ToString());
                ClsUtility.AddParameters("@PMSCM", SqlDbType.Int, ht["PMSCM"].ToString());

                ClsUtility.AddParameters("@Wards", SqlDbType.Int, ht["Wards"].ToString());
                ClsUtility.AddParameters("@LMIS", SqlDbType.Int, ht["LMIS"].ToString());

                int RowsAffected = (Int32)FacilityManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateFacility_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                if (RowsAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                    AppException.Create("#C1", theBL);
                }

                int DeleteFlag = 0;
                if (DeleteFlag == 0)
                {
                    string theSQL = string.Format("delete from lnk_FacilityModule where FacilityId = {0}", FacilityId);
                    ClsUtility.Init_Hashtable();
                    int Rows = (int)FacilityManager.ReturnObject(ClsUtility.theParams, theSQL, ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    DeleteFlag = 1;
                }
                for (int i = 0; i < dtModule.Rows.Count; i++)
                {

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, FacilityId.ToString());
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dtModule.Rows[i]["ModuleID"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@Flag", SqlDbType.Int, "1");
                    int RowsAffModule = (int)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SaveModule_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    if (RowsAffModule == 0)
                    {
                        MsgBuilder theBL = new MsgBuilder();
                        theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                        AppException.Create("#C1", theBL);
                    }
                }
                DeleteFlag = 0;
                if (DeleteFlag == 0 && dtStore != null)
                {
                    string theSQL = string.Format("delete from lnk_FacilityStore where FacilityId = {0}", FacilityId);
                    ClsUtility.Init_Hashtable();
                    int Rows = (int)FacilityManager.ReturnObject(ClsUtility.theParams, theSQL, ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    DeleteFlag = 1;
                }
                if (dtStore != null && dtStore.Rows.Count > 0)
                {
                    for (int j = 0; j < dtStore.Rows.Count; j++)
                    {

                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, FacilityId.ToString());
                        ClsUtility.AddParameters("@StoreId", SqlDbType.Int, dtStore.Rows[j]["StoreID"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                        ClsUtility.AddParameters("@Flag", SqlDbType.Int, "1");
                        int RowsAffStore = (int)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SaveStores_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                        if (RowsAffStore == 0)
                        {
                            MsgBuilder theBL = new MsgBuilder();
                            theBL.DataElements["MessageText"] = "Error in Saving Facility record. Try Again..";
                            AppException.Create("#C1", theBL);
                        }

                    }
                }


                FacilityManager = null;
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return RowsAffected;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int SaveBackupSetup(string theDrive, string theTime)
        {
            //DateTime theBackupDatetime = theTime;
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@BackupDrive", SqlDbType.VarChar, theDrive.ToString());
                ClsUtility.AddParameters("@BackUpTime", SqlDbType.VarChar, theTime.ToString());
                ClsObject BackupManager = new ClsObject();
                return (Int32)BackupManager.ReturnObjectNewImpl(ClsUtility.theParams, "Pr_Admin_UpdateBackupSetup_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                BackupManager = null;
            }
        }

        public DataTable GetBackupSetup()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject BackupManager = new ClsObject();
                return (DataTable)BackupManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_GetBackupSetup_Constella", ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        public DataSet GetModuleName()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject FacilityManager = new ClsObject();
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetModuleName_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetDrugStores()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject FacilityManager = new ClsObject();
                return (DataSet)FacilityManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetStores_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

    }
}
