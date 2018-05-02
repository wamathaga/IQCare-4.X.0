using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;

namespace BusinessProcess.Clinical
{
    public class BNigeriaARTCareVisitation : ProcessBase,INigeriaARTCareVisitation
    {
        public int DeleteARTCareVisitationForm(string FormName, int OrderNo, int PatientId, int UserID)
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
        public DataSet GetNigeriaPatientARTCareVisitation(int patientID, int VisitID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Nigeria_GetARTCareVisitationPatientData", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
        public int Save_Update_ARTCareVisitation(int patientID, int VisitID, int LocationID, Hashtable ht, DataSet theDSchklist, int userID, int DataQualityFlag)
        {
            int retval = 0;
            DataSet theDS;
            ClsObject ARTCareVisitation = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ARTCareVisitation.Connection = this.Connection;
                ARTCareVisitation.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());

                ClsUtility.AddParameters("@ACVVisitDate", SqlDbType.VarChar, ht["ACVVisitDate"].ToString());
                ClsUtility.AddParameters("@ACVchkschedule", SqlDbType.Int, ht["ACVchkschedule"].ToString());
                ClsUtility.AddParameters("@ACVARTStartNoofMonth", SqlDbType.Int, ht["ACVARTStart"].ToString());
                ClsUtility.AddParameters("@ACVCurrentRegimenNoofMonth", SqlDbType.Int, ht["ACVCurrentRegimen"].ToString());
                ClsUtility.AddParameters("@ACVPhysWeight", SqlDbType.Decimal, ht["ACVPhysWeight"].ToString());
                ClsUtility.AddParameters("@ACTPhysHeight", SqlDbType.Decimal, ht["ACTPhysHeight"].ToString());
                ClsUtility.AddParameters("@ACVBPSystolic", SqlDbType.Int, ht["ACVBPSystolic"].ToString());
                ClsUtility.AddParameters("@ACVBPDiastolic", SqlDbType.Int, ht["ACVBPDiastolic"].ToString());
                ClsUtility.AddParameters("@ACVPregnant", SqlDbType.Int, ht["ACVPregnant"].ToString());
                ClsUtility.AddParameters("@ACVEDDDate", SqlDbType.VarChar, ht["ACVEDDDate"].ToString());

                ClsUtility.AddParameters("@ACVChkonPMTCT", SqlDbType.Int, ht["ACVChkonPMTCT"].ToString());
                ClsUtility.AddParameters("@ACVFamilyPlanningStatus", SqlDbType.Int, ht["ACVFamilyPlanningStatus"].ToString());
                ClsUtility.AddParameters("@ACVFunctionalStatus", SqlDbType.Int, ht["ACVFunctionalStatus"].ToString());
                ClsUtility.AddParameters("@ACVWHOStage", SqlDbType.Int, ht["ACVWHOStage"].ToString());
                ClsUtility.AddParameters("@ACVTBStatus", SqlDbType.Int, ht["ACVTBStatus"].ToString());
                ClsUtility.AddParameters("@ACVTBCardNo", SqlDbType.VarChar, ht["ACVTBCardNo"].ToString());
                ClsUtility.AddParameters("@ACVARVDrugAdherence", SqlDbType.Int, ht["ACVARVDrugAdherence"].ToString());
                ClsUtility.AddParameters("@ACVCotrimoxazoleAdherence", SqlDbType.Int, ht["ACVCotrimoxazoleAdherence"].ToString());
                ClsUtility.AddParameters("@ACVINH", SqlDbType.Int, ht["ACVINH"].ToString());
                ClsUtility.AddParameters("@ACVddlsubsitution", SqlDbType.Int, ht["ACVddlsubsitution"].ToString());
                ClsUtility.AddParameters("@ACVTherapyChangeCode", SqlDbType.Int, ht["ACVTherapyChangeStopCode"].ToString());
                ClsUtility.AddParameters("@ACVTherapyChangeStopCodeOtheName", SqlDbType.VarChar, ht["ACVTherapyChangeStopCodeOtheName"].ToString());
                ClsUtility.AddParameters("@ACVARTEnddate", SqlDbType.VarChar, ht["ACVARTEnddate"].ToString());
                ClsUtility.AddParameters("@ACVNumofDaysHospitalised", SqlDbType.Int, ht["ACVNumofDaysHospitalised"].ToString());
                ClsUtility.AddParameters("@TCA", SqlDbType.Int, ht["TCA"].ToString());
                ClsUtility.AddParameters("@dataquality", SqlDbType.Int, DataQualityFlag.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, ht["Signature"].ToString());

                theDS = (DataSet)ARTCareVisitation.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateNigeriaARTCareVisitation_Futures", ClsDBUtility.ObjectEnum.DataSet);
                ////////////////////////////////
                VisitID = Convert.ToInt32(theDS.Tables[0].Rows[0]["VisitId"]);
                retval = VisitID;
                //Family Planning 
                foreach (DataRow theDR in theDSchklist.Tables[0].Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                    ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["FamilyPlanningId"].ToString());
                    ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "FamilyPlanningMethods-Nigeria");
                    ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["FamilyPlanning_Other"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                    int temp = (int)ARTCareVisitation.ReturnObject(ClsUtility.theParams, "pr_Nigeria_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                //Other OIs/Problems
                foreach (DataRow theDR in theDSchklist.Tables[1].Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                    ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["OIOtherProblemID"].ToString());
                    ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "OtherOIs/OtherProblems-Nigeria");
                    ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["OIProblem_Other"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                    int temp = (int)ARTCareVisitation.ReturnObject(ClsUtility.theParams, "pr_Nigeria_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                //Noted Side Effects
                foreach (DataRow theDR in theDSchklist.Tables[2].Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                    ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["NotedSideEffectId"].ToString());
                    ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "NotedSideEffects-Nigeria");
                    ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["NotedSide_Other"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                    int temp = (int)ARTCareVisitation.ReturnObject(ClsUtility.theParams, "pr_Nigeria_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                
                //ARV Adherence
                foreach (DataRow theDR in theDSchklist.Tables[3].Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                    ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ARVwhypoorfairId"].ToString());
                    ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "ARVWhy-Nigeria");
                    ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["ARVwhypoorfair_Other"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                    int temp = (int)ARTCareVisitation.ReturnObject(ClsUtility.theParams, "pr_Nigeria_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                //Cotrimoxable Adherence
                foreach (DataRow theDR in theDSchklist.Tables[4].Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                    ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["CotrimoxazolewhypoorfairId"].ToString());
                    ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "CotrimWhy-Nigeria");
                    ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Cotrimoxazolewhypoorfair_Other"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                    int temp = (int)ARTCareVisitation.ReturnObject(ClsUtility.theParams, "pr_Nigeria_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                //INH Adherence
                foreach (DataRow theDR in theDSchklist.Tables[5].Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                    ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["INHwhypoorfairId"].ToString());
                    ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "INHWhy-Nigeria");
                    ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["INHwhypoorfair_Other"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                    int temp = (int)ARTCareVisitation.ReturnObject(ClsUtility.theParams, "pr_Nigeria_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                //Referrals
                foreach (DataRow theDR in theDSchklist.Tables[6].Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientID.ToString());
                    ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitID.ToString());
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ReferredtoId"].ToString());
                    ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "ReferredTo-Nigeria");
                    ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Referredto_Other"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                    int temp = (int)ARTCareVisitation.ReturnObject(ClsUtility.theParams, "pr_Nigeria_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
            return retval;
        }

    }
}
