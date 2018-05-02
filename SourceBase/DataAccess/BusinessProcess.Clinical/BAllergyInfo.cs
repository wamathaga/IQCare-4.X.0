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
using Interface.Clinical;

namespace BusinessProcess.Clinical
{
    public class BAllergyInfo : ProcessBase, IAllergyInfo
    {

        public int SaveAllergyInfo(int Id, int Ptn_Pk, string AllergyType, string Allergen, string otherAllergen, string severity, string typeReaction, int UserId, int DeleteFlag, string dataAllergy)
        {
            ClsObject FamilyInfo = new ClsObject();
            int retval = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                FamilyInfo.Connection = this.Connection;
                FamilyInfo.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_Pk.ToString());
                ClsUtility.AddParameters("@vAllergyType", SqlDbType.VarChar, AllergyType.ToString());
                ClsUtility.AddParameters("@vAllergen", SqlDbType.VarChar, Allergen.ToString());
                ClsUtility.AddParameters("@votherAllergen", SqlDbType.VarChar, otherAllergen.ToString());
                ClsUtility.AddParameters("@vTypeReaction", SqlDbType.VarChar, typeReaction.ToString());
                ClsUtility.AddParameters("@vSeverity", SqlDbType.VarChar, severity.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString()); 
                ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                //if (dataAllergy.DayOfYear != 1)
                //{
                    ClsUtility.AddParameters("@vDataAllergy", SqlDbType.VarChar, dataAllergy.ToString());
                //}
                retval = (int)FamilyInfo.ReturnObject(ClsUtility.theParams, "Pr_Clinical_SaveAllergyInfo", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

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
                FamilyInfo = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return retval;
        }

    
        public DataSet GetAllAllergyData(int PatientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsObject FamilyInfo = new ClsObject();
                return (DataSet)FamilyInfo.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetAllAllergyData", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
     
        public int DeleteAllergyInfo(int Id, int @UserId)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteFamilyInfo = new ClsObject();
                DeleteFamilyInfo.Connection = this.Connection;
                DeleteFamilyInfo.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());

                theAffectedRows = (int)DeleteFamilyInfo.ReturnObject(ClsUtility.theParams, "Pr_Clinical_DeleteAllergyInfo", ClsDBUtility.ObjectEnum.ExecuteNonQuery);


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
}
