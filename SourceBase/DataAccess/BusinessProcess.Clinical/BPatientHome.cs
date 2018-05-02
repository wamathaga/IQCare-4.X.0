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
    public class BPatientHome : ProcessBase,IPatientHome
    {
        #region "Constuctor"
        public BPatientHome()
        {
        }
        #endregion

        public DataSet GetPatientDetails(int PatientId, int SystemId, int ModuleId)
        {
            lock (this)
            {
                ClsObject PatientManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@SystemId", SqlDbType.Int, SystemId.ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                return (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_PatientDetails_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet IQTouchGetPatientDetails(int PatientId)
        {
            lock (this)
            {
                ClsObject PatientManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_PASDP_PatientDetails", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientHistory(int PatientId)
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientHistory_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet IQTouchGetPatientHistory(int PatientId)
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_IQTouchClinical_GetPatientHistory", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientLabHistory(int PatientId)
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ptn_pk", SqlDbType.Int, PatientId.ToString());
                return (DataSet)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_PASDP_PatientLabResult", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetLinkedForms_FormLinking(int ModuleID, int FeatureID)
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleID.ToString());
                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, FeatureID.ToString());
                return (DataSet)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetLinkedForms_FormLinking", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
      
        public DataTable GetPatientVisitDetail(int PatientID)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());

                //ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationID.ToString());
                return (DataTable)PatientVisitMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_PatientVisitDetails_Constella", ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet ReActivatePatient(int PatientId, Int32 ModId)
        {
            lock (this)
            {
                ClsObject ReActivatePtnMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@Mod", SqlDbType.Int, ModId.ToString());
                return (DataSet)ReActivatePtnMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_ReActivatePatient_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet ReActivateTouchExceptionPatient(int PatientId, Int32 ModId, bool IsTouchException)
        {
            lock (this)
            {
                ClsObject ReActivatePtnMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@Mod", SqlDbType.Int, ModId.ToString());
                string exception = (!IsTouchException) ? "0": "1";
                ClsUtility.AddParameters("@IsTouchException", SqlDbType.Int, exception);
                return (DataSet)ReActivatePtnMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_ReActivatePatient_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataTable GetPharmacyID(int PatientId, int LocationId, int VisitId)
        {
            lock (this)
            {
                ClsObject PharmacyMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());

                return (DataTable)PharmacyMgr.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPharmacyId_Constella", ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        public DataSet GetTechnicalAreaandFormName(int ModuleId)
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                return (DataSet)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetTechnicalAreaandFormName_Future", ClsDBUtility.ObjectEnum.DataSet);
            }
        
        }

        public DataSet GetTechnicalAreaIndicators(int ModuleId, int PatientId)
        { 
            try
            {
                ClsObject PatientHistory = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                return (DataSet)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetTechnicalAreaIndicators_Future", ClsDBUtility.ObjectEnum.DataSet);

            }

            catch (Exception ex)
            {
                //throw ex;
                return null;
            }

            
        }

        public DataSet GetTechnicalAreaIdentifierFuture(int ModuleId, int Ptn_pk)
        {
            try
            {
                ClsObject PatientHistory = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
                return (DataSet)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetTechnicalAreaIdentifier_Future", ClsDBUtility.ObjectEnum.DataSet);

            }

            catch (Exception ex)
            {
                //throw ex;
                return null;
            }


        }

        



        

        //public DataSet GetPatientDetailsCTC(string patientid, int VisitID)
        //{
        //    ClsUtility.Init_Hashtable();
        //    ClsUtility.AddParameters("@patientid", SqlDbType.Int, patientid.ToString());
        //    ClsUtility.AddParameters("@visitID", SqlDbType.Int, VisitID.ToString());
        //    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
        //    ClsObject UserManager = new ClsObject();
        //    return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_PatientDetailsCTC_Constella", ClsDBUtility.ObjectEnum.DataSet);
        //}

        public DataTable GetPatientDebitNoteSummary(int PatientID)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());

                return (DataTable)PatientVisitMgr.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatientDebitNoteSummary_Futures", ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetPatientDebitNoteOpenItems(int patientID, DateTime start, DateTime end)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@Start", SqlDbType.DateTime, start.ToString("dd-MMM-yyyy"));
                ClsUtility.AddParameters("@End", SqlDbType.DateTime, end.ToString("dd-MMM-yyyy"));

                return (DataTable)PatientVisitMgr.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatientDebitNoteOpenItems_Futures", ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet GetPatientDebitNoteDetails(int billId,int PatientId)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@billid", SqlDbType.Int, billId.ToString());
                ClsUtility.AddParameters("@ptn_pk", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                return (DataSet)PatientVisitMgr.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetPatientDebitNoteDetails_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }


        public int CreateDebitNote(int patientID,int locationID, int userID, DateTime start, DateTime end)
        {
            lock (this)
            {
                ClsObject PatientVisitMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                ClsUtility.AddParameters("@userID", SqlDbType.Int, userID.ToString());
                ClsUtility.AddParameters("@Start", SqlDbType.DateTime, start.ToString("dd-MMM-yyyy"));
                ClsUtility.AddParameters("@End", SqlDbType.DateTime, end.ToString("dd-MMM-yyyy"));

                DataRow row = (DataRow)PatientVisitMgr.ReturnObject(ClsUtility.theParams, "Pr_Clinical_CreateDebitNote_Futures", ClsDBUtility.ObjectEnum.DataRow);
                int billid = Convert.ToInt32(row["BillId"]);
                return billid;
            }
        }

        /**************************************/
        //John Macharia
        //5th Sep 2012
        /*************************************/
        public DataSet GetPatientSummaryInformation(int PatientId, int ModuleId)
        {
            lock (this)
            {
                ClsObject PatientManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ModuleId.ToString());
                ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                return (DataSet)PatientManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientSummaryInfo_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        //John End
        #region "Billing Module"
        public DataTable GetPatientWaitList(int PatientId)
        {
            lock (this)
            {
                ClsObject PatientManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());


                return (DataTable)PatientManager.ReturnObject(ClsUtility.theParams, "pr_WaitingList_GetPatientsWaitingList", ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public void SavePatientWaitList(DataTable WaitingList, int ModuleID, int UserID, int PatientID)
        {

            System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
            foreach (DataRow row in WaitingList.Rows)
            {

                sbItems.Append("<row>");
                sbItems.Append(String.Format("<WaitingListID>{0}</WaitingListID>", row["WaitingListID"]));
                sbItems.Append(String.Format("<ListID>{0}</ListID>", row["ListID"]));
                sbItems.Append(String.Format("<Priority>{0}</Priority>", row["Priority"]));
                sbItems.Append(String.Format("<RowStatus>{0}</RowStatus>", row["RowStatus"]));
                sbItems.Append(String.Format("<WaitingFor>{0}</WaitingFor>", row["WaitingFor"]));


                sbItems.Append("</row>");
            }
            sbItems.Append("</root>");
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@ItemsList", SqlDbType.Xml, sbItems.ToString());
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@ModuleID", SqlDbType.Int, ModuleID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsObject BillManager = new ClsObject();
                BillManager.ReturnObject(ClsUtility.theParams, "pr_WaitingList_SavePatientsWaitingList", ClsDBUtility.ObjectEnum.ExecuteNonQuery);


            }
        }
        #endregion

    }
    
}
