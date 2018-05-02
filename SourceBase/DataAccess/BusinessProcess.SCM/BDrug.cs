using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.SCM;
using Application.Common;
using System.Collections;

namespace BusinessProcess.SCM
{
    public class BDrug : ProcessBase, IDrug
    {
        #region "Constructor"
        public BDrug()
        {
        }
        #endregion

        public DataSet GetPharmacyDispenseMasters(Int32 thePatientId, Int32 theStoreId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePatientId.ToString());
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, theStoreId.ToString());
                ClsObject theManager = new ClsObject();
                return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetPharmacyDispenseMasters_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPriorPrescription(Int32 thePatientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePatientId.ToString());
                ClsObject theManager = new ClsObject();
                return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_GetPharmacypriorPrescription_Web", ClsDBUtility.ObjectEnum.DataSet);
            }
        }


        public DataSet CheckDispencedDate(Int32 thePatientId, Int32 LocationID, DateTime theDispDate, Int32 theOrderId)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePatientId.ToString());
            ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationID.ToString());
            ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, theDispDate.ToString());
            ClsUtility.AddParameters("@OrderId", SqlDbType.Int, theOrderId.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_CheckDispencedDate_Futures", ClsDBUtility.ObjectEnum.DataSet);
        }
        public DataTable SavePharmacyDispense(Int32 thePatientId, Int32 theLocationId, Int32 theStoreId, Int32 theUserId, DateTime theDispDate,
            Int32 theOrderType, Int32 theProgramId, string theRegimen, Int32 theOrderId, DataTable theDT, DateTime PharmacyRefillDate)
        {
            DataTable thePharmacyDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject theManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePatientId.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theLocationId.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, theUserId.ToString());
                ClsUtility.AddParameters("@DispensedByDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", theDispDate).ToString());
                ClsUtility.AddParameters("@OrderType", SqlDbType.Int, theOrderType.ToString());
                ClsUtility.AddParameters("@ProgramId", SqlDbType.Int, theProgramId.ToString());
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, theStoreId.ToString());
                ClsUtility.AddParameters("@Regimen", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                ClsUtility.AddParameters("@OrderId", SqlDbType.Int, theOrderId.ToString());
                ClsUtility.AddParameters("@PharmacyRefillAppDate", SqlDbType.VarChar, PharmacyRefillDate.ToString("dd-MMM-yyyy"));
                thePharmacyDT = (DataTable)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePharmacyDispenseOrder_Futures", ClsDBUtility.ObjectEnum.DataTable);

                foreach (DataRow theDR in theDT.Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePatientId.ToString());
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, theStoreId.ToString());
                    ClsUtility.AddParameters("@VisitId", SqlDbType.Int, thePharmacyDT.Rows[0]["VisitId"].ToString());
                    ClsUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, thePharmacyDT.Rows[0]["Ptn_Pharmacy_Pk"].ToString());
                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDR["ItemId"].ToString());
                    ClsUtility.AddParameters("@StrengthId", SqlDbType.Int, theDR["StrengthId"].ToString());
                    ClsUtility.AddParameters("@FrequencyId", SqlDbType.Int, theDR["FrequencyId"].ToString());
                    ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Int, Convert.ToInt32(theDR["QtyDisp"]).ToString());
                    ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDR["Prophylaxis"].ToString());
                    ClsUtility.AddParameters("@BatchId", SqlDbType.Int, theDR["BatchId"].ToString());
                    ClsUtility.AddParameters("@CostPrice", SqlDbType.Decimal, theDR["CostPrice"].ToString() != "" ? theDR["CostPrice"].ToString() : "0");
                    if (theDR["BatchNo"].ToString().Contains("("))
                    {
                        ClsUtility.AddParameters("@BatchNo", SqlDbType.VarChar, theDR["BatchNo"].ToString().Substring(0, theDR["BatchNo"].ToString().IndexOf('(')));
                    }
                    else
                    {
                        ClsUtility.AddParameters("@BatchNo", SqlDbType.VarChar, theDR["BatchNo"].ToString());
                    }
                    ClsUtility.AddParameters("@Margin", SqlDbType.Decimal, theDR["Margin"].ToString() != "" ? theDR["Margin"].ToString() : "0");
                    ClsUtility.AddParameters("@SellingPrice", SqlDbType.Decimal, theDR["SellingPrice"].ToString() != "" ? theDR["SellingPrice"].ToString() : "0");
                    ClsUtility.AddParameters("@BillAmount", SqlDbType.Decimal, theDR["BillAmount"].ToString() != "" ? theDR["BillAmount"].ToString() : "0");
                    ClsUtility.AddParameters("@ExpiryDate", SqlDbType.VarChar, theDR["ExpiryDate"].ToString());
                    ClsUtility.AddParameters("@DispensingUnit", SqlDbType.Int, theDR["DispensingUnitId"].ToString());
                    ClsUtility.AddParameters("@DispensedByDate", SqlDbType.VarChar, theDispDate.ToString("dd-MMM-yyyy"));
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theLocationId.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                    ClsUtility.AddParameters("@DataStatus", SqlDbType.Int, theDR["DataStatus"].ToString());
                    ClsUtility.AddParameters("@PrescribeOrderedQuantity", SqlDbType.Decimal, theDR["OrderedQuantity"].ToString() != "" ? theDR["OrderedQuantity"].ToString() : "0");
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, theDR["Dose"].ToString() != "" ? theDR["Dose"].ToString() : "0");
                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, theDR["Duration"].ToString() != "" ? theDR["Duration"].ToString() : "0");
                    ClsUtility.AddParameters("@PrintPrescriptionStatus", SqlDbType.Int, theDR["PrintPrescriptionStatus"].ToString());
                    ClsUtility.AddParameters("@PatientInstructions", SqlDbType.VarChar, theDR["PatientInstructions"].ToString());
                    Int32 theRowCount = (Int32)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePharmacyDispenseOrderDetail_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }

                //    return thePharmacyDT;

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
            return thePharmacyDT;
        }

        public DataTable GetPharmacyExistingRecord(Int32 thePatientId, Int32 theStoreId)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePatientId.ToString());
            ClsUtility.AddParameters("@StoreId", SqlDbType.Int, theStoreId.ToString());
            ClsObject theManager = new ClsObject();
            return (DataTable)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_GetExistingPharmacyDispense_Futures", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataSet GetPharmacyExistingRecordDetails(Int32 theOrderId)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, theOrderId.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPharmacyOrderDetail_Futures", ClsDBUtility.ObjectEnum.DataSet);
        }
        public DataSet GetPharmacyPrescriptionDetails(int PharmacyID, int PatientId, int IQCareFlag)
        {
            ClsObject PharmacyManager = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, PharmacyID.ToString());
            ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientId.ToString());
            ClsUtility.AddParameters("@IQCareFlag", SqlDbType.Int, IQCareFlag.ToString());
            ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
            return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPharmacyPrescription_Futures", ClsDBUtility.ObjectEnum.DataSet);

        }
        public DataSet GetPharmacyDetailsByDespenced(Int32 theOrderId)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, theOrderId.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPharmacyDetailsByDispenced_Futures", ClsDBUtility.ObjectEnum.DataSet);
        }
        public DataSet GetDrugTypeID(Int32 ItemID)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, ItemID.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_GetDrugTypeId_futures", ClsDBUtility.ObjectEnum.DataSet);
        }
        public DataSet SaveArtData(Int32 PatientID, DateTime dispencedDate)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientID.ToString());
            ClsUtility.AddParameters("@dispencedDate", SqlDbType.VarChar, dispencedDate.ToString("dd-MMM-yyyy"));
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveUpdateArtData_Futures", ClsDBUtility.ObjectEnum.DataSet);
        }
        public void SavePharmacyReturn(Int32 thePatientId, Int32 theLocationId, Int32 theStoreId, DateTime theReturnDate, Int32 theUserId, Int32 thePharmacyId, DataTable theDT)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject theManager = new ClsObject();

                foreach (DataRow theDR in theDT.Rows)
                {
                    if (Convert.ToInt32(theDR["ReturnQty"]) > 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, thePatientId.ToString());
                        ClsUtility.AddParameters("@StoreId", SqlDbType.Int, theStoreId.ToString());
                        ClsUtility.AddParameters("@VisitId", SqlDbType.Int, theDR["visitId"].ToString());
                        ClsUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, thePharmacyId.ToString());
                        ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDR["ItemId"].ToString());
                        ClsUtility.AddParameters("@StrengthId", SqlDbType.Int, theDR["StrengthId"].ToString());
                        ClsUtility.AddParameters("@FrequencyId", SqlDbType.Int, theDR["FrequencyId"].ToString());
                        ClsUtility.AddParameters("@ReturnQuantity", SqlDbType.Int, theDR["ReturnQty"].ToString());
                        ClsUtility.AddParameters("@ReturnReason", SqlDbType.Int, theDR["ReturnReason"].ToString());
                        ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDR["Prophylaxis"].ToString());
                        ClsUtility.AddParameters("@BatchId", SqlDbType.Int, theDR["BatchId"].ToString());
                        ClsUtility.AddParameters("@CostPrice", SqlDbType.Decimal, theDR["CostPrice"].ToString());
                        ClsUtility.AddParameters("@Margin", SqlDbType.Decimal, theDR["Margin"].ToString());
                        ClsUtility.AddParameters("@SellingPrice", SqlDbType.Decimal, theDR["SellingPrice"].ToString());
                        ClsUtility.AddParameters("@BillAmount", SqlDbType.Decimal, theDR["BillAmount"].ToString());
                        ClsUtility.AddParameters("@ExpiryDate", SqlDbType.VarChar, theDR["ExpiryDate"].ToString());
                        ClsUtility.AddParameters("@DispensingUnit", SqlDbType.Int, theDR["DispensingUnitId"].ToString());
                        ClsUtility.AddParameters("@ReturnDate", SqlDbType.VarChar, theReturnDate.ToString("dd-MMM-yyyy"));
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theLocationId.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theUserId.ToString());
                        Int32 theRowCount = (Int32)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePharmacyReturnDetail_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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

        }

        public DataSet SaveHivTreatementPharmacyField(Int32 theOrderId, string weight, string height, int Program, int PeriodTaken, int Provider, int RegimenLine, int RegimenCode, DateTime NxtAppDate, int Reason)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@OrderID", SqlDbType.Int, theOrderId.ToString());
            ClsUtility.AddParameters("@weight", SqlDbType.VarChar, weight.ToString());
            ClsUtility.AddParameters("@height", SqlDbType.VarChar, height.ToString());
            ClsUtility.AddParameters("@Programe", SqlDbType.Int, Program.ToString());
            ClsUtility.AddParameters("@Periodtaken", SqlDbType.Int, PeriodTaken.ToString());
            ClsUtility.AddParameters("@Provider", SqlDbType.Int, Provider.ToString());
            ClsUtility.AddParameters("@RegimenLine", SqlDbType.Int, RegimenLine.ToString());
            ClsUtility.AddParameters("@RegimenCode", SqlDbType.Int, RegimenCode.ToString());
            ClsUtility.AddParameters("@NxtAppDate", SqlDbType.VarChar, NxtAppDate.ToString("dd-MMM-yyyy"));
            ClsUtility.AddParameters("@Region", SqlDbType.Int, Reason.ToString());

            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveUpdateHivTreatementPharmacyField_Futures", ClsDBUtility.ObjectEnum.DataSet);
        }

        public DataTable GetPersonDispensingDrugs(string UserName)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@UserName", SqlDbType.VarChar, UserName.ToString());
                ClsObject theManager = new ClsObject();
                return (DataTable)theManager.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetPersonDispensingDrugs", ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable CheckPaperlessClinic(int LocationID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsObject theManager = new ClsObject();
                return (DataTable)theManager.ReturnObject(ClsUtility.theParams, "Pr_SCM_CheckPaperlessClinic", ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public void SaveUpdateIPTData(Int32 PatientID, int visitId, DateTime INHStartDate, DateTime INHEndDate)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
            ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, PatientID.ToString());
            ClsUtility.AddParameters("@INHStartDate", SqlDbType.VarChar, INHStartDate.ToString("dd-MMM-yyyy"));
            ClsUtility.AddParameters("@INHEndDate", SqlDbType.VarChar, INHEndDate.ToString("dd-MMM-yyyy"));
            ClsObject theManager = new ClsObject();
            Int32 theRowCount = (Int32)theManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_IPTDetails", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
        }

        //KK. 19-Feb-2015
        public DataSet GetPharmacyVitals(int PatientID)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@ptn_pk", SqlDbType.Int, PatientID.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPharmacyVitals", ClsDBUtility.ObjectEnum.DataSet);
        }

        public DataSet GetPharmacyDrugList_Web(int StoreID, string scmOn, string serviceArea)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreID.ToString());
            ClsUtility.AddParameters("@SCMOn", SqlDbType.VarChar, scmOn.ToString());
            ClsUtility.AddParameters("@serviceArea", SqlDbType.VarChar, serviceArea.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPharmacyDrugList_Web", ClsDBUtility.ObjectEnum.DataSet);
        }

        public DataTable ReturnDatatableQuery(string theQuery)
        {
            lock (this)
            {
                ClsObject theQB = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                return (DataTable)theQB.ReturnObject(ClsUtility.theParams, "pr_General_SQLTable_Parse", ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable SavePharmacyDispense_Web(Int32 PatientId, Int32 LocationId, Int32 StoreId, Int32 UserId, int dispensedBy, string DispDate,
          Int32 OrderType, Int32 ProgramId, string theRegimen, Int32 OrderId, DataTable theDT, string PharmacyRefillDate, Int32 DataStatus,
            int orderedBy, string orderDate, string deleteScript, int regimenLine = 0, int regimenCode = 0, int therapyPlan = 0
            , int patientClassification = 0, int isEnrolDifferenciatedCare = 0)
        {
            string Morning, Midday, Evening, Night;
            DataTable thePharmacyDT = new DataTable();
            //Added by VY for regimen
            #region "Regimen"

            //string theRegimen = "";
            string thetmpRegimen = "";
            for (int i = 0; i < theDT.Rows.Count; i++)
            {

                if (theDT.Rows[i]["GenericAbbrevation"].ToString() != "")
                {
                    if (thetmpRegimen == "")
                    {
                        thetmpRegimen = theDT.Rows[i]["GenericAbbrevation"].ToString();
                    }
                    else
                    {
                        thetmpRegimen = String.Format("{0}/{1}", thetmpRegimen, theDT.Rows[i]["GenericAbbrevation"]);
                    }
                }
                thetmpRegimen = thetmpRegimen.Trim();


            }

            foreach (string s in thetmpRegimen.Split('/'))
            {
                if (theRegimen == "" && s != "")
                    theRegimen = theRegimen + s;
                else if (theRegimen != "" && s != "")
                    if (!theRegimen.Contains(s))
                        theRegimen = theRegimen + "/" + s;
            }
            #endregion
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject theManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, dispensedBy.ToString());
                if (DispDate.ToString() != "")
                    ClsUtility.AddParameters("@DispensedByDate", SqlDbType.VarChar, DispDate.ToString());
                ClsUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                ClsUtility.AddParameters("@ProgramId", SqlDbType.Int, ProgramId.ToString());
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                ClsUtility.AddParameters("@Regimen", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@OrderId", SqlDbType.Int, OrderId.ToString());
                ClsUtility.AddParameters("@AppointmentDate", SqlDbType.VarChar, PharmacyRefillDate.ToString());
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, orderedBy.ToString());
                if (orderDate.ToString() != "")
                    ClsUtility.AddParameters("@OrderDate", SqlDbType.VarChar, orderDate.ToString());
                if (deleteScript != "")
                    ClsUtility.AddParameters("@deleteScript", SqlDbType.VarChar, deleteScript);
                ClsUtility.AddParameters("@RegimenLine", SqlDbType.Int, regimenLine.ToString());
                ClsUtility.AddParameters("@RegimenCode", SqlDbType.Int, regimenCode.ToString());
                ClsUtility.AddParameters("@TherapyPlan", SqlDbType.Int, therapyPlan.ToString());

                ClsUtility.AddParameters("@PatientClassification", SqlDbType.Int, patientClassification.ToString());
                ClsUtility.AddParameters("@IsEnrolDifferenciatedCare", SqlDbType.Int, isEnrolDifferenciatedCare.ToString());

                thePharmacyDT = (DataTable)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePharmacyDispenseOrder_Web", ClsDBUtility.ObjectEnum.DataTable);


                foreach (DataRow theDR in theDT.Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientId.ToString());
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    ClsUtility.AddParameters("@VisitId", SqlDbType.Int, thePharmacyDT.Rows[0]["VisitId"].ToString());
                    ClsUtility.AddParameters("@Ptn_Pharmacy_Pk", SqlDbType.Int, thePharmacyDT.Rows[0]["Ptn_Pharmacy_Pk"].ToString());
                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDR["DrugId"].ToString());

                    ClsUtility.AddParameters("@MorningDose", SqlDbType.Decimal, Morning = (theDR["Morning"].ToString() == "") ? "0" : theDR["Morning"].ToString());
                    ClsUtility.AddParameters("@MiddayDose", SqlDbType.Decimal, Midday = (theDR["Midday"].ToString() == "") ? "0" : theDR["Midday"].ToString());
                    ClsUtility.AddParameters("@EveningDose", SqlDbType.Decimal, Evening = (theDR["Evening"].ToString() == "") ? "0" : theDR["Evening"].ToString());
                    ClsUtility.AddParameters("@NightDose", SqlDbType.Decimal, Night = (theDR["Night"].ToString() == "") ? "0" : theDR["Night"].ToString());

                    ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Int, "0");

                    ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDR["Prophylaxis"].ToString() == "True" ? "1" : "0");

                    ClsUtility.AddParameters("@BatchId", SqlDbType.Int, theDR["BatchId"].ToString());

                    if (theDR["BatchNo"].ToString().Contains("("))
                    {
                        ClsUtility.AddParameters("@BatchNo", SqlDbType.VarChar, theDR["BatchNo"].ToString().Substring(0, theDR["BatchNo"].ToString().IndexOf('(')));
                    }
                    else
                    {
                        ClsUtility.AddParameters("@BatchNo", SqlDbType.VarChar, theDR["BatchNo"].ToString());
                    }

                    ClsUtility.AddParameters("@ExpiryDate", SqlDbType.VarChar, theDR["ExpiryDate"].ToString());
                    ClsUtility.AddParameters("@DispensingUnit", SqlDbType.Int, theDR["DispensingUnitId"].ToString());
                    if (DispDate.ToString() != "")
                        ClsUtility.AddParameters("@DispensedByDate", SqlDbType.VarChar, DispDate.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@DataStatus", SqlDbType.Int, DataStatus.ToString());

                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, theDR["Duration"].ToString() != "" ? theDR["Duration"].ToString() : "0");
                    ClsUtility.AddParameters("@PrescribeOrderedQuantity", SqlDbType.Decimal, theDR["QtyPrescribed"].ToString() != "" ? theDR["QtyPrescribed"].ToString() : "0");
                    ClsUtility.AddParameters("@PillCount", SqlDbType.VarChar, theDR["PillCount"].ToString() == "" ? "0" : theDR["PillCount"].ToString());
                    ClsUtility.AddParameters("@PrintPrescriptionStatus", SqlDbType.Int, theDR["PrintPrescriptionStatus"].ToString() == "True" ? "1" : "0");
                    ClsUtility.AddParameters("@PatientInstructions", SqlDbType.VarChar, theDR["Instructions"].ToString());
                    ClsUtility.AddParameters("@Comments", SqlDbType.VarChar, theDR["Comments"].ToString());

                    Int32 theRowCount = (Int32)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePharmacyDispenseOrderDetail_Web", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    //Save details to dtl_PatientPharmacyOrderpartialDispense table
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientId.ToString());
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, thePharmacyDT.Rows[0]["Ptn_Pharmacy_Pk"].ToString());
                    ClsUtility.AddParameters("@drug_pk", SqlDbType.Int, theDR["DrugId"].ToString());
                    ClsUtility.AddParameters("@batchid", SqlDbType.Int, theDR["BatchId"].ToString());
                    ClsUtility.AddParameters("@ExpiryDate", SqlDbType.VarChar, theDR["ExpiryDate"].ToString());
                    ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Int, theDR["QtyDispensed"].ToString() == "" ? "0" : theDR["QtyDispensed"].ToString());
                    //ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDR["RefillQty"].ToString() == "" ? "0" : theDR["RefillQty"].ToString());
                    ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, dispensedBy.ToString());
                    if (DispDate.ToString() != "")
                        ClsUtility.AddParameters("@DispensedByDate", SqlDbType.VarChar, DispDate.ToString());
                    ClsUtility.AddParameters("@PrintPrescriptionStatus", SqlDbType.Int, theDR["PrintPrescriptionStatus"].ToString() == "True" ? "1" : "0");
                    ClsUtility.AddParameters("@comments", SqlDbType.Int, theDR["comments"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    DataTable dt = (DataTable)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePharmacyPartialDispense_Web", ClsDBUtility.ObjectEnum.DataTable);
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
            return thePharmacyDT;
        }

        public DataSet GetPharmacyExistingRecordDetails_Web(Int32 VisitID)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@visit_id", SqlDbType.Int, VisitID.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPharmacyOrderDetail_web", ClsDBUtility.ObjectEnum.DataSet);
        }

        public DataSet GetPharmacyDrugHistory_Web(int PatientID)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PatientID.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPharmacyDrugHistory_Web", ClsDBUtility.ObjectEnum.DataSet);
        }

        public void SavePharmacyRefill_Web(DataTable dt, int iserId, int dispensedBy, string DispensedByDate, string deleteScript)
        {
            DataTable thePharmacyDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject theManager = new ClsObject();

                foreach (DataRow theDR in dt.Rows)
                {

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, theDR["orderId"].ToString());
                    ClsUtility.AddParameters("@drug_pk", SqlDbType.Int, theDR["DrugId"].ToString());
                    ClsUtility.AddParameters("@batchid", SqlDbType.Int, theDR["BatchId"].ToString());
                    //ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDR["RefillQty"].ToString() == "" ? "0" : theDR["RefillQty"].ToString());
                    ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDR["QtyDispensed"].ToString() == "" ? "0" : theDR["QtyDispensed"].ToString());
                    ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, dispensedBy.ToString());
                    if (DispensedByDate.ToString() != "")
                        ClsUtility.AddParameters("@DispensedByDate", SqlDbType.VarChar, DispensedByDate.ToString());
                    ClsUtility.AddParameters("@PrintPrescriptionStatus", SqlDbType.Int, theDR["PrintPrescriptionStatus"].ToString() == "True" ? "1" : "0");
                    ClsUtility.AddParameters("@comments", SqlDbType.Int, theDR["comments"].ToString());
                    if (deleteScript != "")
                        ClsUtility.AddParameters("@deleteScript", SqlDbType.VarChar, deleteScript);

                    theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePharmacyPartialDispense_Web", ClsDBUtility.ObjectEnum.DataTable);
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

        public DataSet MarkOrderAsFullyDispensed(Int32 orderID, string Reason)
        {
            DataSet dataset = new DataSet();

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject theManager = new ClsObject();

                theManager.Connection = this.Connection;
                theManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, orderID.ToString());
                ClsUtility.AddParameters("@Reason", SqlDbType.VarChar, Reason);

                dataset =  (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePharmacyMarkOrderFullyDispensed_Web", ClsDBUtility.ObjectEnum.DataSet);

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
            return dataset;
        }

        public void LockpatientForDispensing(int PatientId, int OrderId, string UserName, string StartDate, bool LockPatient)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@ptn_pk", SqlDbType.Int, PatientId.ToString());
            ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, OrderId.ToString());
            ClsUtility.AddParameters("@UserName", SqlDbType.VarChar, UserName);
            ClsUtility.AddParameters("@StartDate", SqlDbType.VarChar, StartDate.ToString());
            ClsUtility.AddParameters("@LockPatient", SqlDbType.Bit, LockPatient.ToString());
            ClsObject theManager = new ClsObject();
            theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveLockpatientForDispensing", ClsDBUtility.ObjectEnum.DataSet);
        }


        //Ken. 04-May-2015
        public DataSet GetDrugBatchDetails(int DrugID, int StoreID)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Drug_id", SqlDbType.Int, DrugID.ToString());
            ClsUtility.AddParameters("@StoreID", SqlDbType.Int, StoreID.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_GetDrugBatchDetails", ClsDBUtility.ObjectEnum.DataSet);
        }

        public DataSet GetSelectedDrugDetails(int DrugID, int StoreID)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Drug_id", SqlDbType.Int, DrugID.ToString());
            ClsUtility.AddParameters("@StoreID", SqlDbType.Int, StoreID.ToString());
            ClsObject theManager = new ClsObject();
            return (DataSet)theManager.ReturnObject(ClsUtility.theParams, "pr_SCM_GetSelectedDrugDetails", ClsDBUtility.ObjectEnum.DataSet);
        }
        //


    }
}
