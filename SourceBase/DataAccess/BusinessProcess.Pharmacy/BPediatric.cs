using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Interface.Pharmacy;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using System.Collections.Generic;

namespace BusinessProcess.Pharmacy
{
    class BPediatric : ProcessBase, IPediatric
    {
        #region "Constructor"
        public BPediatric()
        {
        }
        #endregion

        #region "Get Pediatric Fields"

        public DataSet GetPediatricFields(int PatientID)
        {
            lock (this)
            {
                ClsObject PediatricManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetPediatricDetails_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public int SavePredefineList(string name,DataTable dt,int UserId)
        {
            int theRowAffected = 0;
            lock (this)
            {
                
                ClsObject PediatricManager = new ClsObject();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@name", SqlDbType.VarChar, name);
                    ClsUtility.AddParameters("@id", SqlDbType.Int, dt.Rows[i]["ID"].ToString());
                    ClsUtility.AddParameters("@srno", SqlDbType.Int, dt.Rows[i]["SRNO"].ToString());
                    ClsUtility.AddParameters("@row", SqlDbType.Int, i.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dt.Rows[i]["SystemId"].ToString());
                    theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_SaveUpdate_PredefineList", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                if (dt.Rows.Count == 0)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@name", SqlDbType.VarChar, name);
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@row", SqlDbType.Int, "0".ToString());
                    ClsUtility.AddParameters("@SystemId", SqlDbType.Int, "1".ToString());
                    theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_SaveUpdate_PredefineList", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
            }
            return theRowAffected;
        }
        public DataSet GetPreDefinedDruglist()
        {
            lock (this)
            {
                ClsObject PediatricManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetPreDefinedDruglist", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetDrugGenericDetails(int PatientID)
        {
            lock (this)
            {
                ClsObject PediatricManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetDrugGenericDetails", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet IQTouchGetPharmacyDetails(int PatientID)
        {
            lock (this)
            {
                ClsObject PediatricManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                return (DataSet)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientHistory_PharmacyTouch", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetExistPharmacyForm(int PatientID, DateTime OrderedByDate)
        {
            lock (this)
            {
                ClsObject PediatricManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                return (DataSet)PediatricManager.ReturnObjectNewImpl(ClsUtility.theParams, "pr_Pharmacy_AgeValidate_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetExistPharmacyFormDespensedbydate(int PatientID, DateTime DispensedByDate)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, @DispensedByDate.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_DateValidate_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        #endregion

        #region "Paediatric List"

        public DataSet GetExistPaediatricDetails(int PharmacyID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PharmacyID", SqlDbType.Int, PharmacyID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetExistPaediatricDetails_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetPatientRecordformStatus(int PatientID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetPatientRecordformStatus", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientContinueARVDetails(int PatientId)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetContinueARVDetails_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
        
        #endregion

        #region "Save Paediatric Details"
        public int SaveUpdatePaediatricDetail(int patientID, int PharmacyID, int LocationID, int RegimenLine, string PharmacyNotes, DataTable theDT, DataSet theDrgMst, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int Signature, int EmployeeID, int OrderType, int VisitType, int UserID, decimal Height, decimal Weight, int FDC, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken, int flag, int SCMFlag, DateTime AppntDate, int AppntReason)
        {
            ClsObject PediatricManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PediatricManager.Connection = this.Connection;
                PediatricManager.Transaction = this.Transaction;
                DataRow theDR;
                int theRowAffected = 0;
                /************   Delete Previous Records **********/
                if (flag == 2)
                {
                    //ClsUtility.Init_Hashtable();
                    //ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    //theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    //if (theRowAffected == 0)
                    //{
                    //    MsgBuilder theMsg = new MsgBuilder();
                    //    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    //    AppException.Create("#C1", theMsg);
                    //}
                }
                #region "Regimen"

                string theRegimen = "";
                string thetmpRegimen = "";
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    if (Convert.ToInt32(theDT.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(theDrgMst.Tables[23]);
                        theDV.RowFilter = "Drug_Pk = " + theDT.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (thetmpRegimen == "")
                            {
                                thetmpRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                thetmpRegimen = thetmpRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        thetmpRegimen = thetmpRegimen.Trim();
                    }
                    else
                    {
                        DataView theDV = new DataView(theDrgMst.Tables[4]);
                        theDV.RowFilter = "GenericId = " + theDT.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (thetmpRegimen == "")
                            {
                                thetmpRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                thetmpRegimen = thetmpRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        thetmpRegimen = thetmpRegimen.Trim();
                    }
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
               
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                if (DispensedByDate.Year.ToString() != "1900")
                {
                    ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                }
                if (flag == 2)
                {
                    if (DispensedByDate.Year.ToString() != "1900")
                    {
                        ClsUtility.AddParameters("@ReportedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                    }
                }
                //ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                ClsUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                ClsUtility.AddParameters("@VisitType", SqlDbType.Int, VisitType.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddParameters("@RegimenLine", SqlDbType.Int, RegimenLine.ToString());
                ClsUtility.AddParameters("@PharmacyNotes", SqlDbType.Int, PharmacyNotes.ToString());
                ClsUtility.AddParameters("@Height", SqlDbType.Decimal, Height.ToString());
                ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, Weight.ToString());
                ClsUtility.AddParameters("@FDC", SqlDbType.Int, FDC.ToString());
                ClsUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                ClsUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                ClsUtility.AddParameters("@PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());
                ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                if (AppntDate.Year.ToString() != "1900")
                {
                    ClsUtility.AddParameters("@AppntDate", SqlDbType.DateTime, AppntDate.ToString());
                }

                ClsUtility.AddParameters("@AppntReason", SqlDbType.Int, AppntReason.ToString());

                theDR = (DataRow)PediatricManager.ReturnObjectNewImpl(ClsUtility.theParams, "pr_Pharmacy_SaveUpdatePediatric_Constella", ClsDBUtility.ObjectEnum.DataRow);

                PharmacyID = Convert.ToInt32(theDR[0].ToString());
                if (PharmacyID == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return PharmacyID;

                }
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();

                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDT.Rows[i]["DrugID"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, theDT.Rows[i]["Strengthid"].ToString());
                    //ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                    //ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, theDT.Rows[i]["Frequencyid"].ToString());
                    //ClsUtility.AddParameters("@SingleDose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@morning", SqlDbType.Decimal, theDT.Rows[i]["Morning"].ToString());
                    ClsUtility.AddParameters("@noon", SqlDbType.Decimal, theDT.Rows[i]["Noon"].ToString());
                    ClsUtility.AddParameters("@evening", SqlDbType.Decimal, theDT.Rows[i]["Evening"].ToString());
                    ClsUtility.AddParameters("@night", SqlDbType.Decimal, theDT.Rows[i]["Night"].ToString()); 
                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, theDT.Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyPrescribed"].ToString());
                    if (theDT.Rows[i]["QtyDispensed"].ToString() == "")
                    {
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, "0");
                    }
                    else
                    {
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyDispensed"].ToString());
                    }
                    ClsUtility.AddParameters("@Finance", SqlDbType.Int, theDT.Rows[i]["Financed"].ToString());
                    ClsUtility.AddParameters("@GenericId", SqlDbType.Int, theDT.Rows[i]["Genericid"].ToString());
                    ClsUtility.AddParameters("@TBRegimenID", SqlDbType.Int, theDT.Rows[i]["TBRegimenId"].ToString());
                    ClsUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, theDT.Rows[i]["TreatmentPhase"].ToString());
                    ClsUtility.AddParameters("@TrMonth", SqlDbType.Int, theDT.Rows[i]["TrMonth"].ToString());
                    ClsUtility.AddParameters("@UnitId", SqlDbType.Int, theDT.Rows[i]["Unitid"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    //ClsUtility.AddParameters("@TotDailyDose", SqlDbType.Decimal, theDT.Rows[i]["TotDailyDose"].ToString());
                    ClsUtility.AddParameters("@flag", SqlDbType.Int, flag.ToString());
                    ClsUtility.AddParameters("@SCMflag", SqlDbType.Int, SCMFlag.ToString());
                    ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDT.Rows[i]["Prophylaxis"].ToString());
                    ClsUtility.AddParameters("@DrugSchedule", SqlDbType.Int, theDT.Rows[i]["DrugSchedule"].ToString());
                    ClsUtility.AddParameters("@PrintPrescriptionStatus", SqlDbType.Int, theDT.Rows[i]["PrintPrescriptionStatus"].ToString());
                    ClsUtility.AddParameters("@PatientInstructions", SqlDbType.Int, theDT.Rows[i]["PatientInstructions"].ToString());
                    theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SavePatientPediatric_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving PharmacyDetails. Try Again..";
                        AppException.Create("#C1", theMsg);

                    }
                }
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {

                    ClsUtility.Init_Hashtable();

                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyID.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return PharmacyID;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PediatricManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
 
        }


        public int SavePaediatricDetail(int patientID, DataTable theDT, DataSet theDrgMst, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int Signature, int EmployeeID, int LocationID, int OrderType, int VisitType, int UserID, decimal Height, decimal Weight, int FDC, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken)
        {
            ClsObject PediatricManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PediatricManager.Connection = this.Connection;
                PediatricManager.Transaction = this.Transaction;
                DataRow theDR;

                #region "Regimen"

                string theRegimen = "";

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    if (Convert.ToInt32(theDT.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(theDrgMst.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + theDT.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                    else
                    {
                        DataView theDV = new DataView(theDrgMst.Tables[4]);
                        theDV.RowFilter = "GenericId = " + theDT.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                }

                #endregion
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                if (DispensedByDate.Year.ToString() != "1900")
                {
                    ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                }
                //ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                ClsUtility.AddParameters("@OrderType", SqlDbType.Int, OrderType.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                ClsUtility.AddParameters("@VisitType", SqlDbType.Int, VisitType.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddParameters("@Height", SqlDbType.Decimal, Height.ToString());
                ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, Weight.ToString());
                ClsUtility.AddParameters("@FDC", SqlDbType.Int, FDC.ToString());
                ClsUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                ClsUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                ClsUtility.AddParameters("@PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());



                theDR = (DataRow)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SavePediatric_Constella", ClsDBUtility.ObjectEnum.DataRow);

                int PharmacyID = Convert.ToInt32(theDR[0].ToString());
                if (PharmacyID == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                    AppException.Create("#C1", theMsg);
                    return PharmacyID;

                }
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();

                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDT.Rows[i]["DrugID"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, theDT.Rows[i]["Strengthid"].ToString());
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, theDT.Rows[i]["Frequencyid"].ToString());
                    ClsUtility.AddParameters("@SingleDose", SqlDbType.Decimal, theDT.Rows[i]["SingleDose"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, theDT.Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyPrescribed"].ToString());
                    if (theDT.Rows[i]["QtyDispensed"].ToString() == "")
                    {
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, "0");
                    }
                    else
                    {
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyDispensed"].ToString());
                    }
                    ClsUtility.AddParameters("@Finance", SqlDbType.Int, theDT.Rows[i]["Financed"].ToString());
                    ClsUtility.AddParameters("@GenericId", SqlDbType.Int, theDT.Rows[i]["Genericid"].ToString());
                    ClsUtility.AddParameters("@TBRegimenID", SqlDbType.Int, theDT.Rows[i]["TBRegimenId"].ToString());
                    ClsUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, theDT.Rows[i]["TreatmentPhase"].ToString());
                    ClsUtility.AddParameters("@TrMonth", SqlDbType.Int, theDT.Rows[i]["TrMonth"].ToString());
                    ClsUtility.AddParameters("@UnitId", SqlDbType.Int, theDT.Rows[i]["Unitid"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    ClsUtility.AddParameters("@TotDailyDose", SqlDbType.Decimal, theDT.Rows[i]["TotDailyDose"].ToString());
                    ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDT.Rows[i]["Prophylaxis"].ToString());
                    theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SavePatientPediatric_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving PharmacyDetails. Try Again..";
                        AppException.Create("#C1", theMsg);

                    }
                }
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {

                    ClsUtility.Init_Hashtable();

                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyID.ToString());
                    theQuery = theQuery.Replace("#44#", "'"+OrderedByDate.ToString()+"'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return PharmacyID;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction); 
                throw;
            }
            finally
            {
                PediatricManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int IQTouchSaveUpdatePharmacy(List<IPharmacyFields> iPharmacyFields)
        {
            ClsObject PediatricManager = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PediatricManager.Connection = this.Connection;
                PediatricManager.Transaction = this.Transaction;
                //DataRow theDR;
                DataSet theDS;
                int theRowAffected = 0;
                string theRegimen = "";
                int PharmacyID = 0;
                int ptn_pk = 0;
                foreach (var Value in iPharmacyFields)
                {
                    ptn_pk = Value.Ptn_pk;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Value.Ptn_pk.ToString());
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, Value.ptn_pharmacy_pk.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, Value.LocationID.ToString());
                    ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, Value.OrderedBy.ToString());
                    if (Value.OrderedByDate != null)
                    {
                        ClsUtility.AddParameters("@OrderedByDate", SqlDbType.VarChar, Value.OrderedByDate.ToString());
                    }
                    ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, Value.DispensedBy.ToString());
                    if (Value.DispensedByDate != null)
                    {
                        ClsUtility.AddParameters("@DispensedByDate", SqlDbType.VarChar, Value.DispensedByDate.ToString());
                        if (Value.flag == 2)
                        {
                            ClsUtility.AddParameters("@ReportedByDate", SqlDbType.VarChar, Value.DispensedByDate.ToString());
                        }
                    }
                    //if (Value.DispensedByDate.Year.ToString() != "1")
                    //{
                    //    ClsUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, Value.DispensedByDate.ToString());
                    //}
                    //if (Value.flag == 2)
                    //{
                    //    if (Value.DispensedByDate.Year.ToString() != "1")
                    //    {
                    //        ClsUtility.AddParameters("@ReportedByDate", SqlDbType.DateTime, Value.DispensedByDate.ToString());
                    //    }
                    //}

                    ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, Value.EmployeeId.ToString());
                    ClsUtility.AddParameters("@VisitType", SqlDbType.Int, Value.VisitType.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, Value.userid.ToString());
                    ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                    ClsUtility.AddParameters("@RegimenLine", SqlDbType.Int, Value.RegimenLine.ToString());
                    ClsUtility.AddParameters("@PharmacyNotes", SqlDbType.Int, Value.PharmacyNotes.ToString());
                    ClsUtility.AddParameters("@Height", SqlDbType.Decimal, Value.Height.ToString());
                    ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, Value.Weight.ToString());
                    ClsUtility.AddParameters("@flag", SqlDbType.Int, Value.flag.ToString());
                    ClsUtility.AddParameters("@ProgID", SqlDbType.Int, Value.TreatmentProgram.ToString());
                    ClsUtility.AddParameters("@ProviderID", SqlDbType.Int, Value.Drugprovider.ToString());
                    ClsUtility.AddParameters("@PeriodTaken", SqlDbType.Int, Value.PeriodTaken.ToString());
                    ClsUtility.AddParameters("@AppntReason", SqlDbType.Int, Value.AppntReason.ToString());
                    if (Value.PharmacyRefillDate != null)
                    {
                        ClsUtility.AddParameters("@AppntDate", SqlDbType.VarChar, Value.PharmacyRefillDate.ToString());
                    }
                    //if (Value.PharmacyRefillDate.Year.ToString() != "1")
                    //{
                    //    ClsUtility.AddParameters("@AppntDate", SqlDbType.DateTime, Value.PharmacyRefillDate.ToString());
                    //}


                    theDS = (DataSet)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SaveUpdatePharmacyTouch", ClsDBUtility.ObjectEnum.DataSet);

                    PharmacyID = Convert.ToInt32(theDS.Tables[0].Rows[0][0].ToString());
                    if (PharmacyID == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving PatientPharmacy Records. Try Again..";
                        AppException.Create("#C1", theMsg);
                        return PharmacyID;

                    }

                    foreach (var ValueDrug in Value.Druginfo)
                    {
                        if (Convert.ToInt32(ValueDrug.GenericId) == 0)
                        {
                            DataView theDV = new DataView(theDS.Tables[1]);
                            theDV.RowFilter = "Drug_Pk = " + ValueDrug.Drug_Pk + " and DrugTypeId = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                            if (theDV.Count > 0)
                            {
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                                }
                            }
                            theRegimen = theRegimen.Trim();
                        }
                      
                    }

                    foreach (var ValueDrug in Value.Druginfo)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                        ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, ValueDrug.Drug_Pk.ToString());
                        ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, ValueDrug.FrequencyID.ToString());
                        ClsUtility.AddParameters("@SingleDose", SqlDbType.Decimal, ValueDrug.SingleDose.ToString());
                        ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, ValueDrug.Duration.ToString());
                        ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, ValueDrug.OrderedQuantity.ToString());
                        if (ValueDrug.DispensedQuantity.ToString() == "")
                        {
                            ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, "0");
                        }
                        else
                        {
                            ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, ValueDrug.DispensedQuantity.ToString());
                        }
                        ClsUtility.AddParameters("@GenericId", SqlDbType.Int, ValueDrug.GenericId.ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, Value.userid.ToString());
                        ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                        ClsUtility.AddParameters("@Original_ptn_pharmacy_pk", SqlDbType.Int, Value.ptn_pharmacy_pk_old.ToString());
                        ClsUtility.AddParameters("@flag", SqlDbType.Int, Value.flag.ToString());
                        ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, ValueDrug.Prophylaxis.ToString());
                        ClsUtility.AddParameters("@Refill", SqlDbType.Int, ValueDrug.refill.ToString());
                        if (ValueDrug.RefillExpiration.Year.ToString() != "1")
                        {
                            ClsUtility.AddParameters("@RefillExpirationdate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", ValueDrug.RefillExpiration));
                        }
                        theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SavePatientPharmacyTouch", ClsDBUtility.ObjectEnum.ExecuteNonQuery);




                        if (theRowAffected == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving PharmacyDetails. Try Again..";
                            AppException.Create("#C1", theMsg);

                        }
                    }
                    if (Value.flag == 2)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                        ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                        ClsUtility.AddParameters("@LocationID", SqlDbType.Int, Value.LocationID.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, Value.userid.ToString());
                        ClsUtility.AddParameters("@Regimen", SqlDbType.NVarChar, theRegimen.ToString());
                        theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SaveRegimenTouch", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return PharmacyID;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PediatricManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        #endregion

        #region "Update Paediatric Details"


        public int UpdatePaediatricDetail(int patientID, int LocationID, int PharmacyID, DataTable theDT, DataSet theDrgMst, int OrderedBy, int DispensedBy, int Signature, int EmployeeID, int OrderType, int UserID, decimal Height, decimal Weight, int FDC, int ProgID, int ProviderID, DateTime OrderedByDate, DateTime ReportedByDate, DataTable theCustomFieldData, int PeriodTaken)
         {
            ClsObject PediatricManager = new ClsObject();
            try
            {
                int theRowAffected = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                PediatricManager.Connection = this.Connection;
                PediatricManager.Transaction = this.Transaction;
                

                /************   Delete Previous Records **********/

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }


                #region "Regimen"

                string theRegimen = "";

                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    if (Convert.ToInt32(theDT.Rows[i]["GenericId"]) == 0)
                    {
                        DataView theDV = new DataView(theDrgMst.Tables[0]);
                        theDV.RowFilter = "Drug_Pk = " + theDT.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];    
                        if (theDV.Count > 0)
                        {
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                    else
                    {
                        DataView theDV = new DataView(theDrgMst.Tables[4]);
                        theDV.RowFilter = "GenericId = " + theDT.Rows[i]["GenericId"] + " and DrugTypeID = 37";     
                        if (theDV.Count > 0)
                        {
                            if (theRegimen == "")
                            {
                                theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                            }
                            else
                            {
                                theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                }

                #endregion




                /************  Insert Paediatric Details ***********/

                ClsUtility.Init_Hashtable();
                
                ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                ClsUtility.AddParameters("@OrderedBy", SqlDbType.Int, OrderedBy.ToString());
                ClsUtility.AddParameters("@DispensedBy", SqlDbType.Int, DispensedBy.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, Signature.ToString());
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                ClsUtility.AddParameters("@RegimenType", SqlDbType.VarChar, theRegimen);
                ClsUtility.AddParameters("@Height", SqlDbType.Decimal, Height.ToString());
                ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, Weight.ToString());
                ClsUtility.AddParameters("@ProgID", SqlDbType.Int, ProgID.ToString());
                ClsUtility.AddParameters("@ProviderID", SqlDbType.Int, ProviderID.ToString());
                ClsUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                if (ReportedByDate.Year.ToString() != "1900")
                {
                    ClsUtility.AddParameters("@ReportedByDate", SqlDbType.DateTime, ReportedByDate.ToString());
                }
                
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("PeriodTaken", SqlDbType.Int, PeriodTaken.ToString());

                theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_UpdatePediatric_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                if (theRowAffected == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error in Updating Patient Pharmacy Details. Try Again..";
                    AppException.Create("#C1", theMsg);
                }



                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();

                    ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDT.Rows[i]["DrugID"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, theDT.Rows[i]["Strengthid"].ToString());
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, theDT.Rows[i]["Frequencyid"].ToString());
                    ClsUtility.AddParameters("@SingleDose", SqlDbType.Decimal, theDT.Rows[i]["SingleDose"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, theDT.Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyPrescribed"].ToString());
                    if (theDT.Rows[i]["QtyDispensed"].ToString() == "")
                    {
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, "0");
                    }
                    else
                    {
                        ClsUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDT.Rows[i]["QtyDispensed"].ToString());
                    }
                    ClsUtility.AddParameters("@Finance", SqlDbType.Int, theDT.Rows[i]["Financed"].ToString());
                    ClsUtility.AddParameters("@GenericId", SqlDbType.Int, theDT.Rows[i]["Genericid"].ToString());
                    ClsUtility.AddParameters("@TBRegimenID", SqlDbType.Int, theDT.Rows[i]["TBRegimenId"].ToString());
                    ClsUtility.AddParameters("@TreatmentPhase", SqlDbType.VarChar, theDT.Rows[i]["TreatmentPhase"].ToString());
                    ClsUtility.AddParameters("@TrMonth", SqlDbType.Int, theDT.Rows[i]["TrMonth"].ToString());
                    ClsUtility.AddParameters("@UnitId", SqlDbType.Int, theDT.Rows[i]["Unitid"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    ClsUtility.AddParameters("@TotDailyDose", SqlDbType.Decimal, theDT.Rows[i]["TotDailyDose"].ToString());
                    ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, theDT.Rows[i]["Prophylaxis"].ToString());
                    theRowAffected = (int)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_SavePatientPediatric_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving Pharmacy Details. Try Again..";
                        AppException.Create("#C1", theMsg);
                    
                    }
                }

                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////

                //ClsObject theCustomManager = new ClsObject();
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", patientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#55#", PharmacyID.ToString());
                    theQuery = theQuery.Replace("#44#", "'" + OrderedByDate.ToString() + "'");
                    ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)PediatricManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;

            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                PediatricManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
      
        #endregion

        #region "Delete Pediatric Form"
        public int DeletePediatricForms(string FormName, int OrderNo, int PatientId, int UserID)
        {

            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeletePediatricForm = new ClsObject();
                DeletePediatricForm.Connection = this.Connection;
                DeletePediatricForm.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                ClsUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeletePediatricForm.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);


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
        #endregion

        #region added by Akhil

        #endregion
        public DataSet SaveUpdate_CustomPharmacy(String Insert, DataSet DS, int UserId)
        {
            DataSet theDS = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject CustomMgrSave = new ClsObject();
                CustomMgrSave.Connection = this.Connection;
                CustomMgrSave.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Insert", SqlDbType.VarChar, Insert.ToString());
                theDS = (DataSet)CustomMgrSave.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveCustomForm_Constella", ClsDBUtility.ObjectEnum.DataSet);
                int PharmacyID = Convert.ToInt32(theDS.Tables[1].Rows[0]["PharmacyID"]);
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    ClsUtility.AddParameters("@Drug_Id", SqlDbType.Int, DS.Tables[0].Rows[i]["DrugId"].ToString());
                    ClsUtility.AddParameters("@GenericID", SqlDbType.Int, DS.Tables[0].Rows[i]["GenericId"].ToString());
                    ClsUtility.AddParameters("@Dose", SqlDbType.Decimal, DS.Tables[0].Rows[i]["Dose"].ToString());
                    ClsUtility.AddParameters("@FrequencyID", SqlDbType.Int, DS.Tables[0].Rows[i]["Frequency"].ToString());
                    ClsUtility.AddParameters("@Duration", SqlDbType.Decimal, DS.Tables[0].Rows[i]["Duration"].ToString());
                    ClsUtility.AddParameters("@StrengthID", SqlDbType.Int, "0");
                    ClsUtility.AddParameters("@QtyPrescribed", SqlDbType.Decimal, DS.Tables[0].Rows[i]["QtyPrescribed"].ToString());
                    ClsUtility.AddParameters("@Prophylaxis", SqlDbType.Int, DS.Tables[0].Rows[i]["Prophylaxis"].ToString());
                    ClsUtility.AddParameters("@Instructions", SqlDbType.VarChar, DS.Tables[0].Rows[i]["Instructions"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                    ClsUtility.AddParameters("@Flag", SqlDbType.Int, "1".ToString());
                    int retvaldisclose = (Int32)CustomMgrSave.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveCustomFormPharmacy_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    ClsUtility.AddParameters("@Drug_Id", SqlDbType.Int, DS.Tables[1].Rows[i]["DrugId"].ToString());
                    ClsUtility.AddParameters("@Refill", SqlDbType.Int, DS.Tables[1].Rows[i]["Refill"].ToString());
                    ClsUtility.AddParameters("@RefillExpiration", SqlDbType.Int, DS.Tables[1].Rows[i]["RefillExpiration"].ToString());
                    ClsUtility.AddParameters("@Flag", SqlDbType.Int, "2".ToString());
                    int retvaldisclose = (Int32)CustomMgrSave.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveCustomFormPharmacy_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                if (DS.Tables[2].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[2].Rows.Count; i++)
                    {
                        if (Convert.ToString(DS.Tables[2].Rows[i]["QtyDispensed"]) != "")
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                            ClsUtility.AddParameters("@Drug_Id", SqlDbType.Int, DS.Tables[2].Rows[i]["DrugId"].ToString());
                            //ClsUtility.AddParameters("@QtyPrescribed", SqlDbType.Decimal, DS.Tables[2].Rows[i]["QtyPrescribed"].ToString());
                            ClsUtility.AddParameters("@QtyDispensed", SqlDbType.Decimal, DS.Tables[2].Rows[i]["QtyDispensed"].ToString());
                            ClsUtility.AddParameters("@BatchNo", SqlDbType.Int, DS.Tables[2].Rows[i]["BatchNo"].ToString());
                            ClsUtility.AddParameters("@ExpiryDate", SqlDbType.VarChar, DS.Tables[2].Rows[i]["ExpiryDate"].ToString());
                            ClsUtility.AddParameters("@SellPrice", SqlDbType.Decimal, DS.Tables[2].Rows[i]["SellPrice"].ToString());
                            ClsUtility.AddParameters("@BillAmount", SqlDbType.Decimal, DS.Tables[2].Rows[i]["BillAmount"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserId.ToString());
                            ClsUtility.AddParameters("@Flag", SqlDbType.Int, "3".ToString());
                            int retvaldisclose = (Int32)CustomMgrSave.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveCustomFormPharmacy_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        }
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

            return theDS;



        }

        public DataSet GetPharmacyDetailforLabelPrint(int PatientId, int VisitId)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)PharmacyManager.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetPharmacyDetailforPrint_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
    }
}
