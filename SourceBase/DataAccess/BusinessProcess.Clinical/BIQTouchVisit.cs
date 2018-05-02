using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;
using BusinessProcess.Administration;
using Interface.Administration;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace BusinessProcess.Clinical
{
    public class BIQTouchVisit : ProcessBase, IIQTouchVisit
    {
        #region "Constructor"
        public BIQTouchVisit()
        {
        }
        #endregion

        public DataSet GetVisitDetails(string PatientID, string LocationID, string UserID, string VisitID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID);
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationID);
                ClsUtility.AddParameters("@VisitID", SqlDbType.Int, VisitID);
                ClsObject RecordMgr = new ClsObject();
                DataSet regDT = (DataSet)RecordMgr.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Get", ClsDBUtility.ObjectEnum.DataSet);
                return regDT;
            }

        }
        
        private static Object thisLock = new Object();
        public int SaveVisitDetails(objVisit theVisit, bool IsUpdate = false) //string PatientID,  string LocationID, string UserID,
        {
            lock (thisLock)
            {
                return (CallSaveVisitDetails(theVisit, IsUpdate));
            }
        }


        private int CallSaveVisitDetails(objVisit theVisit, bool IsUpdate = false)
        {
            ClsObject TheVisit = new ClsObject();
            //System.Threading.Thread.Sleep(10000);
            int NewVisitID = 0; int theRowAffected = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                TheVisit.Connection = this.Connection;
                TheVisit.Transaction = this.Transaction;

                //first save the single fields
                ClsUtility.Init_Hashtable();
                if (IsUpdate)
                    ClsUtility.AddParameters("@OldVisitID", SqlDbType.Int, theVisit.OldVisitID.ToString());
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                ClsUtility.AddParameters("@Scheduled", SqlDbType.Int, theVisit.Scheduled.ToString());
                ClsUtility.AddParameters("@IQtouchVisitType", SqlDbType.Int, theVisit.VisitType.ToString());
                ClsUtility.AddParameters("@Present", SqlDbType.Int, theVisit.Present.ToString());
                ClsUtility.AddParameters("@SupporterName", SqlDbType.VarChar, theVisit.CGName);
                ClsUtility.AddParameters("@TreatmentSupporterContact", SqlDbType.VarChar, theVisit.CGPhoneNumber);
                ClsUtility.AddParameters("@MUAC", SqlDbType.Int, theVisit.MUAC.ToString());
                ClsUtility.AddParameters("@Pregnancystatus", SqlDbType.Int, theVisit.PregnantYN.ToString());
                ClsUtility.AddParameters("@Admittedtohospital", SqlDbType.Int, theVisit.AdmittedtoHospital.ToString());
                ClsUtility.AddParameters("@HospitalizedNumberofdays", SqlDbType.Int, theVisit.NumDaysHosp.ToString());
                ClsUtility.AddParameters("@HospitalName", SqlDbType.Int, theVisit.WhereHosp.ToString());
                ClsUtility.AddParameters("@Dischargediagnosis", SqlDbType.VarChar, theVisit.DischargeDiagnosis);
                ClsUtility.AddParameters("@Dischargenote", SqlDbType.VarChar, theVisit.DischargeNote);
                ClsUtility.AddParameters("@DevelopmentalScreening", SqlDbType.Int, theVisit.DevScreening.ToString());
                ClsUtility.AddParameters("@TannerStage", SqlDbType.Int, theVisit.TannerStage.ToString());
                ClsUtility.AddParameters("@SexuallyActive", SqlDbType.Int, theVisit.SexuallyActiveYN.ToString());
                ClsUtility.AddParameters("@Protectedsex", SqlDbType.Int, theVisit.ProtectedSexYN.ToString());
                ClsUtility.AddParameters("@NewTBContact", SqlDbType.Int, theVisit.NewTBContactYN.ToString());
                ClsUtility.AddParameters("@ContactSensitiveTB", SqlDbType.Int, theVisit.SensitivityTBYN.ToString());
                ClsUtility.AddParameters("@ContactTBTreatment", SqlDbType.Int, theVisit.TreatmentYN.ToString());
                ClsUtility.AddParameters("@ContactDailyInjection", SqlDbType.Int, theVisit.DailyInjectionsYN.ToString());
                //ClsUtility.AddParameters("@ContactTBTreatmentRcvd", SqlDbType.Int, "0");//theVisit.Treatment.ToString());
                ClsUtility.AddParameters("@ContactTBTreatmentRcvd", SqlDbType.Int, theVisit.FormOfTreatment.ToString());
                ClsUtility.AddParameters("@ContactOtherTBProphylaxis", SqlDbType.VarChar, theVisit.ContactOtherTBProphylaxis);
                ClsUtility.AddParameters("@Disease_pk", SqlDbType.Int, "0");
                ClsUtility.AddParameters("@FamilyPlanningMethod", SqlDbType.VarChar, theVisit.FamilyPlanning.ToString());
                ClsUtility.AddParameters("@OtherFPmethods", SqlDbType.VarChar, theVisit.FamilyPlanningOther);
                ClsUtility.AddParameters("@Temp", SqlDbType.Decimal, theVisit.Temp.ToString());
                ClsUtility.AddParameters("@RR", SqlDbType.Decimal, theVisit.RespRate.ToString());
                ClsUtility.AddParameters("@HR", SqlDbType.Decimal, theVisit.Pulse.ToString());
                ClsUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, theVisit.BPDiast.ToString());
                ClsUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, theVisit.BPSyst.ToString());
                ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, theVisit.Weight.ToString());
                ClsUtility.AddParameters("@Height", SqlDbType.Decimal, theVisit.Height.ToString());
                ClsUtility.AddParameters("@Headcircumference", SqlDbType.Decimal, theVisit.HeadCirc.ToString());
                ClsUtility.AddParameters("@TBRxStartDate", SqlDbType.VarChar, theVisit.TBRxStartDate.ToString());
                ClsUtility.AddParameters("@TBRxEndDate", SqlDbType.VarChar, theVisit.TBRxEndDate.ToString());
                ClsUtility.AddParameters("@TBStatus", SqlDbType.Int, theVisit.TBStatus.ToString());
                ClsUtility.AddParameters("@StillTreatement", SqlDbType.Int, theVisit.StillOnTreatment.ToString());
                ClsUtility.AddParameters("@NewSensitiveInformation", SqlDbType.Int, theVisit.NewSensitivityInfoYN.ToString());
                ClsUtility.AddParameters("@PatientTBTreatmentRcvd", SqlDbType.Int, theVisit.PatientTBTreatment.ToString());
                ClsUtility.AddParameters("@PatientOtherTBProphylaxis", SqlDbType.VarChar, theVisit.PatientOtherTBProphylaxis);
                ClsUtility.AddParameters("@WHOStage", SqlDbType.Int, theVisit.ClinicalStage.ToString());
                ClsUtility.AddParameters("@ClinicalNotes", SqlDbType.VarChar, theVisit.ClinicalNotes);
                //To be included after UAT
                //ClsUtility.AddParameters("@AdverseEventOther", SqlDbType.VarChar, theVisit
                //ClsUtility.AddParameters("@AdverseEventSeverityID", SqlDbType.Int, theVisit
                ClsUtility.AddParameters("@CorrectlyDispensed", SqlDbType.Int, theVisit.DispensedYN.ToString());
                ClsUtility.AddParameters("@NotDispensedNote", SqlDbType.VarChar, theVisit.ReasonNotDispensed.ToString());
                ClsUtility.AddParameters("@CotrimoxazoleAdhere", SqlDbType.Int, theVisit.CTXAdherance.ToString());
                ClsUtility.AddParameters("@ARVAdhere", SqlDbType.Int, theVisit.ARVAdherance.ToString());
                ClsUtility.AddParameters("@ARTenddate", SqlDbType.VarChar, theVisit.ARTEndDate.ToString());
                ClsUtility.AddParameters("@FeedingOption", SqlDbType.Int, theVisit.FeedingPractice.ToString());
                ClsUtility.AddParameters("@NutritionalSupport", SqlDbType.Int, theVisit.NutrionalSupport.ToString());
                ClsUtility.AddParameters("@NutritionalProblem", SqlDbType.Int, theVisit.NutritionalProblems.ToString());

                string therapyReasons = string.Empty;
                if (theVisit.ChangeRegimenReasons.Count > 0)
                {
                    foreach (var item in theVisit.ChangeRegimenReasons)
                    {
                        if (therapyReasons.Length > 0)
                        {
                            therapyReasons += "|" + item.ChangeReasonID.ToString();
                        }
                        else
                        {
                            therapyReasons += item.ChangeReasonID.ToString();
                        }

                    }
                    ClsUtility.AddParameters("@TherapyOther", SqlDbType.VarChar, theVisit.ChangeReasonOther);
                }
                else if (theVisit.StopRegimenReasons.Count > 0)
                {
                    foreach (var item in theVisit.StopRegimenReasons)
                    {
                        if (therapyReasons.Length > 0)
                        {
                            therapyReasons += "|" + item.StopReasonID.ToString();
                        }
                        else
                        {
                            therapyReasons += item.StopReasonID.ToString();
                        }

                    }
                    ClsUtility.AddParameters("@TherapyOther", SqlDbType.VarChar, theVisit.StopReasonOther);
                }
                else
                {
                    ClsUtility.AddParameters("@TherapyOther", SqlDbType.VarChar, string.Empty);
                }

                ClsUtility.AddParameters("@TherapyReasons", SqlDbType.VarChar, therapyReasons);
                ClsUtility.AddParameters("@TherapyPlan", SqlDbType.Int, theVisit.SubsInterruptions.ToString());




                ClsUtility.AddParameters("@DisclosureID", SqlDbType.Int, theVisit.LevelofDisclosure.ToString());
                ClsUtility.AddParameters("@DisclosureChild", SqlDbType.Int, theVisit.DisclosedToChild.ToString());
                //ClsUtility.AddParameters("@PatientRefID", SqlDbType.Int, theVisit.ReferredToServiceList[0].RefferredID.ToString());
                //ClsUtility.AddParameters("@PatientRefDesc", SqlDbType.VarChar, theVisit.RefferredToOther);
                ClsUtility.AddParameters("@PatientExitReason", SqlDbType.Int, theVisit.TransferOut.ToString());
                ClsUtility.AddParameters("@AppDate", SqlDbType.VarChar, theVisit.NextAppointmentDate);
                ClsUtility.AddParameters("@EmployeeID", SqlDbType.Int, theVisit.UserID.ToString());
                ClsUtility.AddParameters("@SignatureID", SqlDbType.Int, theVisit.UserID.ToString());
                ClsUtility.AddParameters("@VisitDate", SqlDbType.VarChar, theVisit.VisitDate);
                ClsUtility.AddParameters("@AdverseEventYN", SqlDbType.Int, theVisit.AdverseEventYN.ToString());

                ClsUtility.AddDirectionParameter("@idNEW", SqlDbType.Int, ParameterDirection.Output);

                NewVisitID = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                if (NewVisitID == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["MessageText"] = "Error saving the Visit. Please contact the Administrator";
                    AppException.Create("#C1", theMsg);

                }

                DataMgr.CommitTransaction(this.Transaction);
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                //save TB Treatment
                if (IsUpdate)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theVisit.OldVisitID.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                    theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Update_TBTreatment", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    DataMgr.CommitTransaction(this.Transaction);
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                }
                if (theVisit.Treatment.Count > 0)
                {
                    foreach (var item in theVisit.Treatment)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        ClsUtility.AddParameters("@Drug", SqlDbType.VarChar, item.ToString());
                        ClsUtility.AddParameters("@IsPatient", SqlDbType.Int, "0");
                        theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_TBTreatment", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }

                //then save the multi selects
                if (IsUpdate)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theVisit.OldVisitID.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                    theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Update_TBSensitivityList", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    DataMgr.CommitTransaction(this.Transaction);
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                }
                if (theVisit.NewSensitvityList.Count > 0)
                {
                    foreach (var item in theVisit.NewSensitvityList)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        ClsUtility.AddParameters("@Drug", SqlDbType.VarChar, item.RegimenID.ToString());
                        ClsUtility.AddParameters("@Sensitivity", SqlDbType.VarChar, item.SensitivityYN.ToString());
                        ClsUtility.AddParameters("@Resistance", SqlDbType.VarChar, item.ResistanceYN.ToString());
                        ClsUtility.AddParameters("@IsPatient", SqlDbType.Int, "1");
                        theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_TBSensitivityList", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }

                if (theVisit.SensitvityList.Count > 0)
                {
                    foreach (var item in theVisit.SensitvityList)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        ClsUtility.AddParameters("@Drug", SqlDbType.VarChar, item.RegimenID.ToString());
                        ClsUtility.AddParameters("@Sensitivity", SqlDbType.VarChar, item.SensitivityYN.ToString());
                        ClsUtility.AddParameters("@Resistance", SqlDbType.VarChar, item.ResistanceYN.ToString());
                        ClsUtility.AddParameters("@IsPatient", SqlDbType.Int, "0");
                        theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_TBSensitivityList", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }


                if (IsUpdate)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theVisit.OldVisitID.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                    theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Update_PhysicalFindings", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    DataMgr.CommitTransaction(this.Transaction);
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                }
                if (theVisit.PhysicalFindings.Count > 0)
                {
                    foreach (var item in theVisit.PhysicalFindings)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        ClsUtility.AddParameters("@SymptomID", SqlDbType.Int, item.SymptomID.ToString());
                        ClsUtility.AddParameters("@SymptomDescription", SqlDbType.VarChar, item.SymptomDescription);
                        theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_PhysicalFindings", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }


                if (IsUpdate)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theVisit.OldVisitID.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                    theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Update_AdverseEvent", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    DataMgr.CommitTransaction(this.Transaction);
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                }
                if (theVisit.AdverseEvents.Count > 0)
                {
                    foreach (var item in theVisit.AdverseEvents)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        ClsUtility.AddParameters("@AdverseEventID", SqlDbType.Int, item.AdverseEventID.ToString());
                        ClsUtility.AddParameters("@AdverseEventDescription", SqlDbType.VarChar, item.AdverseEventDescription);
                        ClsUtility.AddParameters("@AdverseEventSeverityID", SqlDbType.Int, item.AdvereEventSeverityID.ToString());
                        ClsUtility.AddParameters("@AdverseEventOther", SqlDbType.VarChar, item.AdverseEventOther);

                        theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_AdverseEvent", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }


                if (IsUpdate)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theVisit.OldVisitID.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                    theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Update_WhyARVAdherances", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    DataMgr.CommitTransaction(this.Transaction);
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                }
                if (theVisit.WhyARVAdherances.Count > 0)
                {
                    foreach (var item in theVisit.WhyARVAdherances)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        ClsUtility.AddParameters("@MissedReasonID", SqlDbType.Int, item.ARVAdheranceID.ToString());
                        ClsUtility.AddParameters("@Other_Desc", SqlDbType.VarChar, theVisit.OtherARVReason.ToString());
                        theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_WhyARVAdherances", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }


                if (IsUpdate)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theVisit.OldVisitID.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                    theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Update_ReferredToServiceList", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    DataMgr.CommitTransaction(this.Transaction);
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                }
                if (theVisit.ReferredToServiceList.Count > 0)
                {
                    foreach (var item in theVisit.ReferredToServiceList)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        ClsUtility.AddParameters("@PatientRefID", SqlDbType.Int, item.RefferredID.ToString());
                        ClsUtility.AddParameters("@PatientRefDesc", SqlDbType.VarChar, theVisit.RefferredToOther);
                        theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_Visit_Add_ReferredToServiceList", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }


                if (IsUpdate)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theVisit.OldVisitID.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                    theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "pr_IQTouch_Clinical_Visit_Update_TBDiagnosis", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    DataMgr.CommitTransaction(this.Transaction);
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                }
                if (theVisit.TBDiagnosisList != null && theVisit.TBDiagnosisList.Count > 0)
                {
                    foreach (var item in theVisit.TBDiagnosisList)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theVisit.PatientID.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, NewVisitID.ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theVisit.LocationID.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theVisit.UserID.ToString());
                        ClsUtility.AddParameters("@DiagnosisMade", SqlDbType.Int, item.TBDiagnosisID.ToString());
                        theRowAffected = (int)TheVisit.ReturnObject(ClsUtility.theParams, "pr_IQTouch_Clinical_Visit_Add_TBDiagnosis", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        DataMgr.CommitTransaction(this.Transaction);
                        this.Connection = DataMgr.GetConnection();
                        this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    }
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }


            catch (Exception e)
            {
                DataMgr.RollBackTransation(this.Transaction);
                BErrorLogging ErrorMan = new BErrorLogging();
                ErrorMan.LogError("DataAccess", e.Message, ErrorType.Error);
                throw e;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return theRowAffected;
        }
    }
}
