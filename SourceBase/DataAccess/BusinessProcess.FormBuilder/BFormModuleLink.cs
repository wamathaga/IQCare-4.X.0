﻿using System;
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
    public class BFormModuleLink : ProcessBase, IFormModuleLink

    {
        public DataSet GetFormModuleLinkDetail(Int32 ModuleId)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleID", SqlDbType.VarChar, ModuleId.ToString());

                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_GetFormModuleLinkIdentifier_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet FormModuleLinking(int ModuleId, int CountryID)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleID", SqlDbType.VarChar, ModuleId.ToString());
                ClsUtility.AddParameters("@CountryID", SqlDbType.VarChar, CountryID.ToString());

                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_FormModuleLinking", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public void SaveUpdateFormModuleLinkDetail(int intModuleID, ArrayList list, int userId)
        {
            int intDeleteFlag = 1;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject ModuleMgr = new ClsObject();
                ModuleMgr.Connection = this.Connection;
                ModuleMgr.Transaction = this.Transaction;
                
                for (int i = 0; i < list.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, intModuleID.ToString());
                    ClsUtility.AddParameters("@FormID", SqlDbType.Int, list[i].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, intDeleteFlag.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                    Int32 NoRowsEffected = (Int32)ModuleMgr.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_SaveUpdateFormModuleLink_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    intDeleteFlag = 0;

                }
                if (list.Count == 0)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, intModuleID.ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, intDeleteFlag.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                    Int32 NoRowsEffected = (Int32)ModuleMgr.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_SaveUpdateFormModuleLink_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    intDeleteFlag = 0;
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
        }
       
    }
}
