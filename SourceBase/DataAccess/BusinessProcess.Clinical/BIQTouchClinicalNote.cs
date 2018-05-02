using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using Interface.Clinical;
using DataAccess.Common;
using System.Data;
using DataAccess.Entity;

namespace BusinessProcess.Clinical
{
    public class BIQTouchClinicalNote : ProcessBase, IIQTouchClinicalNote
    {
        public DataTable GetClinicalNote(string PatientID, string NoteID)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@patientid", SqlDbType.Int, PatientID);
            ClsUtility.AddParameters("@NoteID", SqlDbType.Int, NoteID);
            ClsObject GetRecs = new ClsObject();
            DataTable regDT = (DataTable)GetRecs.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_NonClinicalNote_Get", ClsDBUtility.ObjectEnum.DataTable);
            return regDT;
        }

        public int SaveClinicalnote(string PatientID, string NoteDate, string Note, string LocationId, string UserId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsUtility.Init_Hashtable();

                //Patient info Params
                ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, PatientID);
                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationId);
                ClsUtility.AddParameters("@NoteDate", SqlDbType.VarChar, NoteDate);
                ClsUtility.AddParameters("@Note", SqlDbType.VarChar, Note);
                ClsUtility.AddParameters("@UserId", SqlDbType.VarChar, UserId);

                ClsObject RegMan = new ClsObject();
                RegMan.Connection = this.Connection;
                RegMan.Transaction = this.Transaction;
                int RecsAffected = (int)RegMan.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_NonClinicalNote_Add", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return RecsAffected;
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

        public int EditClinicalnote(string PatientID, string NoteID, string NoteDate, string Note, string LocationId, string UserId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsUtility.Init_Hashtable();

                //Patient info Params
                ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, PatientID);
                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, LocationId);
                ClsUtility.AddParameters("@NoteID", SqlDbType.VarChar, NoteID);
                ClsUtility.AddParameters("@NoteDate", SqlDbType.VarChar, NoteDate);
                ClsUtility.AddParameters("@Note", SqlDbType.VarChar, Note);
                ClsUtility.AddParameters("@UserId", SqlDbType.VarChar, UserId);


                ClsObject RegMan = new ClsObject();
                RegMan.Connection = this.Connection;
                RegMan.Transaction = this.Transaction;
                int RecsAffected = (int)RegMan.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_NonClinicalNote_Update", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return RecsAffected;
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
