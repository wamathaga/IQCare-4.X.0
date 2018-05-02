using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Clinical
{
    public class BNigeriaARTCareSummary:ProcessBase,INigeriaARTCareSummary
    {

        public int DeleteARTCareSummaryForm(string FormName, int OrderNo, int PatientId, int UserID)
        {
            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteForm = new ClsObject();
                DeleteForm.Connection = this.Connection;
                DeleteForm.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                theAffectedRows = (int)DeleteForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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

        public DataSet GetPatientARTCareSummary(int patientid, int LocationId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetARTCareSummaryNigeria", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveUpdatePatientARTCareSummary(int patientID, int VisitID, int LocationID, Hashtable ht, int userID, int DataQualityFlag)
        {
            int retval = 0;
            DataSet theDS;
            ClsObject ARTCareSummaryMgr = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ARTCareSummaryMgr.Connection = this.Connection;
                ARTCareSummaryMgr.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                ClsUtility.AddParameters("@visitdate", SqlDbType.DateTime, ht["visitdate"].ToString());
                //ClsUtility.AddParameters("@CohortMonth", SqlDbType.VarChar, ht["CohortMonth"].ToString());
                //ClsUtility.AddParameters("@CohortYear", SqlDbType.VarChar, ht["CohortYear"].ToString());
                ClsUtility.AddParameters("@OtherfacilityRegimen", SqlDbType.VarChar, ht["OtherfacilityRegimen"].ToString());
                ClsUtility.AddParameters("@OtherfacilityRegimenStartDate", SqlDbType.DateTime, ht["OtherfacilityRegimenStartDate"].ToString());
                ClsUtility.AddParameters("@OtherfacilityWHOStage", SqlDbType.Int, ht["OtherfacilityWHOStage"].ToString());
                ClsUtility.AddParameters("@OtherfacilityCD4", SqlDbType.Decimal, ht["OtherfacilityCD4"].ToString());
                ClsUtility.AddParameters("@OtherfacilityCD4Percent", SqlDbType.Decimal, ht["OtherfacilityCD4Percent"].ToString());
                ClsUtility.AddParameters("@OtherfacilityWeight", SqlDbType.Decimal, ht["OtherfacilityrWeight"].ToString());
                ClsUtility.AddParameters("@OtherfacilityHeight", SqlDbType.Decimal, ht["OtherfacilityHeight"].ToString());
                ClsUtility.AddParameters("@OtherfacilityClinicalStage", SqlDbType.Int, ht["OtherfacilityClinicalStage"].ToString());
                ClsUtility.AddParameters("@dataquality", SqlDbType.Int, DataQualityFlag.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                theDS = (DataSet)ARTCareSummaryMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateARTCareSummaryNigeria", ClsDBUtility.ObjectEnum.DataSet);
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
