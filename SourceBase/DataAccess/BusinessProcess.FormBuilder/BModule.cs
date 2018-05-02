using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;
using Application.Common;

namespace BusinessProcess.FormBuilder
{
    public class BModule : ProcessBase, IModule
    {
        public DataSet GetModuleDetail()
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_GetModuleIdentifier_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetModuleIdentifier(Int32 ModuleId)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_GetModuleIdentificationDetails_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
         public int StatusUpdate(Hashtable ht)
        {
            int RowsEffected = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ModuleMgr = new ClsObject();
                ModuleMgr.Connection = this.Connection;
                ModuleMgr.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ht["ModuleID"].ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.Int, ht["Status"].ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, ht["DeleteFlag"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                RowsEffected = (Int32)ModuleMgr.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_StatusUpdate_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
            return RowsEffected;

        }

        public int SaveUpdateModuleDetail(Hashtable ht, DataTable dt,DataTable dtbusinessrules)
        {
            int ModuleID;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ModuleMgr = new ClsObject();
                ModuleMgr.Connection = this.Connection;
                ModuleMgr.Transaction = this.Transaction;

                
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ht["ModuleId"].ToString());
                ClsUtility.AddParameters("@ModuleName", SqlDbType.VarChar, ht["ModuleName"].ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.Int, ht["Status"].ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, ht["DeleteFlag"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());

                ClsUtility.AddParameters("@PharmacyFlag", SqlDbType.Int, ht["PharmacyFlag"].ToString());

                DataTable theDT = (DataTable)ModuleMgr.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_SaveUpdateModule_Constella", ClsDBUtility.ObjectEnum.DataTable);

                ModuleID = Convert.ToInt32(theDT.Rows[0][0]);
                if (ModuleID != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //if (dt.Rows[i]["Selected"].ToString() == "True")
                        //{
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleID.ToString());
                        ClsUtility.AddParameters("@FieldID", SqlDbType.Int, dt.Rows[i]["Id"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dt.Rows[i]["IdentifierName"].ToString());
                        ClsUtility.AddParameters("@FieldType", SqlDbType.Int, dt.Rows[i]["FieldType"].ToString());
                        ClsUtility.AddParameters("@Identifierchecked", SqlDbType.VarChar, dt.Rows[i]["Selected"].ToString());
                        ClsUtility.AddParameters("@Reqiuredchecked", SqlDbType.VarChar, dt.Rows[i]["Required"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                        ClsUtility.AddParameters("@Label", SqlDbType.VarChar, dt.Rows[i]["Label"].ToString());
                        ClsUtility.AddParameters("@autopopulatenumber", SqlDbType.Int, dt.Rows[i]["autopopulatenumber"].ToString());
                        
                        Int32 NoRowsEffected = (Int32)ModuleMgr.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_SaveUpdateModuleIdentification_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        //}
                    }
                }
                if (ModuleID != 0)
                {
                    if (dtbusinessrules.Rows.Count == 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleID.ToString());
                        ClsUtility.AddParameters("@BusRuleid", SqlDbType.Int, "1");
                        ClsUtility.AddParameters("@value", SqlDbType.Int, "1");
                        ClsUtility.AddParameters("@value1", SqlDbType.Int, "1");
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, "1");
                        ClsUtility.AddParameters("@setType", SqlDbType.Int, "1");
                        ClsUtility.AddParameters("@counter", SqlDbType.Int, "0");

                        Int32 NoRowsEffected = (Int32)ModuleMgr.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_DeleteModuleBusinessRules", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    for (int i = 0; i < dtbusinessrules.Rows.Count; i++)
                    {
                        //if (dt.Rows[i]["Selected"].ToString() == "True")
                        //{
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleID.ToString());
                        ClsUtility.AddParameters("@BusRuleid", SqlDbType.Int, dtbusinessrules.Rows[i]["BusRuleId"].ToString());
                        ClsUtility.AddParameters("@value", SqlDbType.Int, dtbusinessrules.Rows[i]["Value"].ToString());
                        ClsUtility.AddParameters("@value1", SqlDbType.Int, dtbusinessrules.Rows[i]["Value1"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                        ClsUtility.AddParameters("@setType", SqlDbType.Int, dtbusinessrules.Rows[i]["SetType"].ToString());
                        ClsUtility.AddParameters("@counter", SqlDbType.Int, i.ToString());

                        Int32 NoRowsEffected = (Int32)ModuleMgr.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_SaveUpdateModuleBusinessRules", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        //}
                    }
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
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return ModuleID;
        }

        public void DeleteModule(Int32 ModuleId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ModuleMgr = new ClsObject();
                ModuleMgr.Connection = this.Connection;
                ModuleMgr.Transaction = this.Transaction;

                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());
                Int32 NoRowsEffected = (Int32)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_DeleteModule_Future", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

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
}