using System;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Security;
using System.Collections.Specialized;
using Application.Common;
using System.Configuration;
namespace BusinessProcess.Security
{
    public class BIQCareSystem : ProcessBase, IIQCareSystem
    {
        #region "Constructor"
        public BIQCareSystem()
        {
        }
        #endregion

        public DataSet GetSystemCache()
        {
            lock (this)
            {
                ClsObject theCacheManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                DataSet theDS = (DataSet)theCacheManager.ReturnObject(ClsUtility.theParams, "pr_CreateLocalCache_AllMasters_constella", ClsDBUtility.ObjectEnum.DataSet);
                theDS.Tables[0].TableName = "Mst_Provider";
                theDS.Tables[1].TableName = "Mst_District";
                theDS.Tables[2].TableName = "Mst_Reason";
                theDS.Tables[3].TableName = "Mst_Education";
                theDS.Tables[4].TableName = "Mst_Designation";
                theDS.Tables[5].TableName = "Mst_Employee";
                theDS.Tables[6].TableName = "Mst_Occupation";
                theDS.Tables[7].TableName = "Mst_Province";
                theDS.Tables[8].TableName = "Mst_Village";
                theDS.Tables[9].TableName = "Mst_Code";
                theDS.Tables[10].TableName = "Mst_HIVAIDSCareTypes";
                theDS.Tables[11].TableName = "Mst_ARTSponsor";
                theDS.Tables[12].TableName = "Mst_HivDisease";
                theDS.Tables[13].TableName = "Mst_Assessment";
                theDS.Tables[14].TableName = "Mst_Symptom";
                theDS.Tables[15].TableName = "Mst_Decode";
                theDS.Tables[16].TableName = "Mst_Feature";
                theDS.Tables[17].TableName = "Mst_Function";
                theDS.Tables[18].TableName = "Mst_HivDisclosure";
                theDS.Tables[19].TableName = "Mst_Frequency";
                theDS.Tables[20].TableName = "Mst_Strength";
                theDS.Tables[21].TableName = "Mst_FrequencyUnits";
                theDS.Tables[22].TableName = "Mst_Drug";
                theDS.Tables[23].TableName = "Mst_Generic";
                theDS.Tables[24].TableName = "Mst_DrugType";
                theDS.Tables[25].TableName = "Mst_LabTest";
                theDS.Tables[26].TableName = "Lnk_TestParameter";
                theDS.Tables[27].TableName = "Lnk_LabValue";
                theDS.Tables[28].TableName = "Lnk_ParameterResult";
                theDS.Tables[29].TableName = "Mst_LPTF";
                theDS.Tables[30].TableName = "Mst_StoppedReason";
                theDS.Tables[31].TableName = "Mst_Facility";
                theDS.Tables[32].TableName = "Mst_HIVCareStatus";
                theDS.Tables[33].TableName = "Mst_RelationshipType";
                theDS.Tables[34].TableName = "Mst_Ward";
                theDS.Tables[35].TableName = "Mst_Division";
                theDS.Tables[36].TableName = "Mst_TBStatus";
                theDS.Tables[37].TableName = "Mst_ARVStatus";
                theDS.Tables[38].TableName = "Mst_CouncellingType";
                theDS.Tables[39].TableName = "Mst_CouncellingTopic";
                theDS.Tables[40].TableName = "Mst_LostFollowreason";
                theDS.Tables[41].TableName = "Mst_Regimen";
                theDS.Tables[42].TableName = "LabTestOrder";
                theDS.Tables[43].TableName = "mst_ReferredFrom";
                theDS.Tables[44].TableName = "Mst_PatientLabPeriod";
                theDS.Tables[45].TableName = "Mst_PMTCTDecode";
                theDS.Tables[46].TableName = "Mst_Module";
                theDS.Tables[47].TableName = "Mst_ModDecode";
                theDS.Tables[48].TableName = "Mst_ARVSideEffects";
                theDS.Tables[49].TableName = "Mst_ModCode";
                theDS.Tables[50].TableName = "Mst_DrugSchedule";
                theDS.Tables[51].TableName = "Mst_Store";
                theDS.Tables[52].TableName = "Mst_Supplier";
                theDS.Tables[53].TableName = "mst_Donor";
                theDS.Tables[54].TableName = "Mst_Program";
                theDS.Tables[55].TableName = "Mst_Batch";
                theDS.Tables[56].TableName = "Mst_Countries";
                theDS.Tables[57].TableName = "Mst_Town";
                theDS.Tables[58].TableName = "VWDiseaseSymptom";
                theDS.Tables[59].TableName = "VW_ICDList";
                theDS.Tables[60].TableName = "mst_RegimenLine";
                theDS.Tables[61].TableName = "mst_Store";
                theDS.Tables[62].TableName = "mst_BlueCode";
                theDS.Tables[63].TableName = "mst_BlueDecode";
                theDS.Tables[65].TableName = "Mst_FormBuilderTab";
                theDS.Tables[66].TableName = "VW_UserDesignationTransaction";
                try
                {
                    theDS.Tables[67].TableName = "QueryBuilderReports";
                }
                catch { }
                theDS.Tables[68].TableName = "mst_LabDepartment";
                theDS.Tables[69].TableName = "mst_Countries";
                return theDS;
            }
        }

        public void RefreshReportingTables(int Drop)
        {
            lock (this)
            {
                ClsObject RptTableFactory = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@dbKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsUtility.AddParameters("@Drop", SqlDbType.Int, Drop.ToString());
                int theNoRows = (int)RptTableFactory.ReturnObject(ClsUtility.theParams, "Pr_CustomReports_CreateReportTables_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
            }
        }

        public DateTime SystemDate()
        {
            lock (this)
            {
                string theSQL = "Select Getdate()";
                ClsObject DateManager = new ClsObject();
                ClsUtility.Init_Hashtable();

                DateTime theCurrentDt = (DateTime)((DataRow)DateManager.ReturnObject(ClsUtility.theParams, theSQL, ClsDBUtility.ObjectEnum.DataRow))[0];
                return theCurrentDt;
            }
        }

        public int DataBaseBackup(string Path, int Location, int Deidentified)
        {
            try
            {
                if (System.IO.Directory.Exists(Path))
                {

                    ClsObject DataManager = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FileName", SqlDbType.VarChar, Path);
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, Location.ToString());
                    ClsUtility.AddParameters("@Deidentified", SqlDbType.Int, Deidentified.ToString());
                    ClsUtility.AddParameters("@dbKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                    return (Int32)DataManager.ReturnObject(ClsUtility.theParams, "pr_SystemAdmin_Backup_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                else
                {
                    //throw new System.IO.DirectoryNotFoundException("Invalid drive. Please enter a valid path.");
                    System.IO.Directory.CreateDirectory(Path);
                    ClsObject DataManager = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FileName", SqlDbType.VarChar, Path);
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, Location.ToString());
                    ClsUtility.AddParameters("@Deidentified", SqlDbType.Int, Deidentified.ToString());
                    ClsUtility.AddParameters("@dbKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity.ToString());
                    return (Int32)DataManager.ReturnObject(ClsUtility.theParams, "pr_SystemAdmin_Backup_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }
            }
            catch (System.IO.DirectoryNotFoundException exd)
            {
                throw exd;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public DataSet GetBackupFiles(string Path)
        {
            lock (this)
            {
                ClsObject DataManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Path", SqlDbType.VarChar, Path);
                return (DataSet)DataManager.ReturnObject(ClsUtility.theParams, "pr_SystemAdmin_GetBackupFiles_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetBackupSets(string Path)
        {
            lock (this)
            {
                ClsObject DataManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Path", SqlDbType.VarChar, Path);
                return (DataSet)DataManager.ReturnObject(ClsUtility.theParams, "pr_SystemAdmin_GetBackupSets_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public int RestoreDataBase(string Path, int FileNo)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DataManager = new ClsObject();
                this.Connection = DataMgr.GetConnection_Master();
                DataManager.Connection = this.Connection;
                ClsUtility.AddParameters("@Path", SqlDbType.VarChar, Path);
                ClsUtility.AddParameters("@Position", SqlDbType.Int, FileNo.ToString());
                return (Int32)DataManager.ReturnObject(ClsUtility.theParams, "pr_SystemAdmin_RestoreDB_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
            }

        }

        public string GetServerInstance()
        {
            lock (this)
            {
                Utility objUtil = new Utility();
                string constr = objUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
                return constr;
            }
        }

        public DataTable GetIQCareSystems(int theFlag)
        {
            lock (this)
            {
                ClsObject SystemManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, theFlag.ToString());
                return (DataTable)SystemManager.ReturnObject(ClsUtility.theParams, "pr_System_IQCareSystemName_Constella", ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet GetMySQLAuditTrailData()
        {
            ClsObject MySQLManager = new ClsObject();
            ClsUtility.Init_Hashtable();
            return (DataSet)MySQLManager.ReturnMySQLObject(ClsUtility.theParams, "GetAuditTrails", ClsDBUtility.ObjectEnum.DataSet);
        }

        public DataSet GetVisitForms()
        {
            ClsObject VisitManager = new ClsObject();
            ClsUtility.Init_Hashtable();
            return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectVisitType", ClsDBUtility.ObjectEnum.DataSet);
        }

    }
}
