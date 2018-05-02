using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using Interface.Clinical;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;
using System.Data;

namespace BusinessProcess.Clinical
{
    class BFreeText : ProcessBase, IFreeText
    {
        public DataSet GetFreeText(int patientID, int VisitID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, VisitID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_GetFreeTextData", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
        public int Save_Update_FreeText(int Ptn_pk, int Visit_Id, string FreeTextXml, int location_id, int userID)
        {
            int retval = 0;          
            ClsObject FreeText = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                FreeText.Connection = this.Connection;
                FreeText.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();               
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, Visit_Id.ToString());               
                ClsUtility.AddParameters("@FreeTextXml", SqlDbType.VarChar, FreeTextXml.ToString());
                ClsUtility.AddParameters("@location_id", SqlDbType.Int, location_id.ToString());
                ClsUtility.AddParameters("@CreatedBy", SqlDbType.Int, userID.ToString()); 
                retval = (int)FreeText.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_FreeText", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
            return retval;
        }
    }
}
