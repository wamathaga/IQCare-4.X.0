using System;
using System.Data;
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

        public DataSet GetNAFDACDataByPatient(int PatientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                ClsObject NAFDACInfo = new ClsObject();
                return (DataSet)NAFDACInfo.ReturnObject(ClsUtility.theParams, "pr_NAFDAC_GetNAFDACDataByPatient", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveNAFDACData(int ptn_Pk, string description, string dateReactionStart, string dateReactionStop, string outcomeReaction, string admitDuetoADR,
            string prolongedADR, string durationOfAdmission, string reactionTreatment, int signature, int drugPK, int suspectedDrug, string indicationForUse,
            string dosage, string route, string dateStarted, string dateStopped, int concomitantMedicine, string reasonForUse, int userId, int deleteFlag,
            string nafdacNo, int genericID, int batchID, string outcomeReactionDesc)
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

                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_Pk.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, deleteFlag.ToString());
                ClsUtility.AddParameters("@description", SqlDbType.VarChar, description.ToString());
                ClsUtility.AddParameters("@outcomeReaction", SqlDbType.VarChar, outcomeReaction.ToString());
                ClsUtility.AddParameters("@outcomeReactionDesc", SqlDbType.VarChar, outcomeReactionDesc.ToString());
                ClsUtility.AddParameters("@dateReactionStart", SqlDbType.VarChar, dateReactionStart.ToString());
                ClsUtility.AddParameters("@dateReactionStop", SqlDbType.VarChar, dateReactionStop.ToString());
                ClsUtility.AddParameters("@admitDuetoADR", SqlDbType.VarChar, admitDuetoADR.ToString());
                ClsUtility.AddParameters("@prolongedADR", SqlDbType.VarChar, prolongedADR.ToString());
                ClsUtility.AddParameters("@durationOfAdmission", SqlDbType.VarChar, durationOfAdmission.ToString());
                ClsUtility.AddParameters("@reactionTreatment", SqlDbType.VarChar, reactionTreatment.ToString());
                ClsUtility.AddParameters("@drugPK", SqlDbType.VarChar, drugPK.ToString());
                ClsUtility.AddParameters("@genericID", SqlDbType.VarChar, genericID.ToString());
                ClsUtility.AddParameters("@batchID", SqlDbType.VarChar, batchID.ToString());
                ClsUtility.AddParameters("@suspectedDrug", SqlDbType.VarChar, suspectedDrug.ToString());
                ClsUtility.AddParameters("@nafdacNo", SqlDbType.VarChar, nafdacNo.ToString());
                ClsUtility.AddParameters("@indicationForUse", SqlDbType.VarChar, indicationForUse.ToString());
                ClsUtility.AddParameters("@dosage", SqlDbType.VarChar, dosage.ToString());
                ClsUtility.AddParameters("@route", SqlDbType.VarChar, route.ToString());
                ClsUtility.AddParameters("@dateStarted", SqlDbType.VarChar, dateStarted.ToString());
                ClsUtility.AddParameters("@dateStopped", SqlDbType.VarChar, dateStopped.ToString());
                ClsUtility.AddParameters("@concomitantMedicine", SqlDbType.VarChar, concomitantMedicine.ToString());
                ClsUtility.AddParameters("@reasonForUse", SqlDbType.VarChar, reasonForUse.ToString());
                ClsUtility.AddParameters("@signature", SqlDbType.VarChar, signature.ToString());

                retval = (int)FamilyInfo.ReturnObject(ClsUtility.theParams, "Pr_NAFDAC_SaveNAFDACData", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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

        public int DeleteNAFDACDataByDrug(int Id, int @UserId)
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

                theAffectedRows = (int)DeleteFamilyInfo.ReturnObject(ClsUtility.theParams, "Pr_NAFDAC_DeleteNAFDACDataByDrug", ClsDBUtility.ObjectEnum.ExecuteNonQuery);


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
