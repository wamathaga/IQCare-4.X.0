using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using System.Data;
using DataAccess.Entity;
using DataAccess.Common;
using HIVCE.Common.Entities;
using Application.Common;
using BusinessProcess.Clinical;
using Interface.Clinical;

namespace HIVCE.DataAccess
{
    public class DBClinicalEncounter : ProcessBase
    {

        public DataSet GetData(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.GetData() Method Called.");
            DataSet ds = new DataSet();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitPK.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ds = (DataSet)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_GetClinicalEncounter", ClsDBUtility.ObjectEnum.DataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return ds;
        }

        public DataSet GetPresentingComplaints(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.GetPresentingComplaints() Method Called.");
            DataSet ds = new DataSet();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitPK.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ds = (DataSet)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_GetPresentingComplaints", ClsDBUtility.ObjectEnum.DataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return ds;
        }

        public DataSet GetAddtionalHx(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.GetAddtionalHx() Method Called.");
            DataSet ds = new DataSet();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitPK.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ds = (DataSet)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_GetAddtionalHx", ClsDBUtility.ObjectEnum.DataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return ds;
        }

        public DataSet GetScreeningData(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.GetScreeningData() Method Called.");
            DataSet ds = new DataSet();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitPK.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ds = (DataSet)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_GetScreeningData", ClsDBUtility.ObjectEnum.DataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return ds;
        }

        public DataSet GetSystemicReviewData(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.GetSystemicReviewData() Method Called.");
            DataSet ds = new DataSet();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitPK.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ds = (DataSet)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_GetSystemicReviewData", ClsDBUtility.ObjectEnum.DataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return ds;
        }

        public DataSet GetManagement(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.GetManagement() Method Called.");
            DataSet ds = new DataSet();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitPK.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                ds = (DataSet)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_GetManagementxData", ClsDBUtility.ObjectEnum.DataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return ds;
        }

        public int SaveUpdateTriage(Triage obj, int userId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.SaveUpdateTriage() method called");
            bool flag = true;
            int visitId = 0;
            ClsObject clsObjTP = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObjTP.Connection = this.Connection;
                clsObjTP.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, obj.LocationId.ToString());
                ClsUtility.AddParameters("@Visitdate", SqlDbType.DateTime, obj.OrdVisit.VisitDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss"));
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                ClsUtility.AddParameters("@VisitTypeId", SqlDbType.Int, obj.OrdVisit.VisitTypeId.ToString());
                ClsUtility.AddParameters("@HIVSupportgroup", SqlDbType.VarChar, obj.InitialEvaluation.HIVSupportgroup.ToString());
                ClsUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, obj.InitialEvaluation.HIVSupportGroupMembership.ToString());
                ClsUtility.AddParameters("@Menarche", SqlDbType.VarChar, obj.InitialEvaluation.Menarche.ToString());
                ClsUtility.AddParameters("@AccompaniedByCaregiver", SqlDbType.VarChar, obj.InitialEvaluation.AccompaniedByCaregiver.ToString());
                ClsUtility.AddParameters("@CaregiverRelationship", SqlDbType.VarChar, obj.InitialEvaluation.CaregiverRelationship.ToString());
                ClsUtility.AddParameters("@BPDiastolic", SqlDbType.Int, obj.PatientVitals.BPDiastolic.ToString());
                ClsUtility.AddParameters("@BPSystolic", SqlDbType.Int, obj.PatientVitals.BPSystolic.ToString());
                ClsUtility.AddParameters("@TEMP", SqlDbType.Float, string.IsNullOrEmpty(obj.PatientVitals.TEMP) != true ? obj.PatientVitals.TEMP.ToString() : "0");
                ClsUtility.AddParameters("@RR", SqlDbType.Int, obj.PatientVitals.RR.ToString());
                ClsUtility.AddParameters("@HR", SqlDbType.Int, obj.PatientVitals.HR.ToString());
                ClsUtility.AddParameters("@Headcircumference", SqlDbType.Float, string.IsNullOrEmpty(obj.PatientVitals.HeadCircumference) != true ? obj.PatientVitals.HeadCircumference.ToString() : "0");
                ClsUtility.AddParameters("@height", SqlDbType.Int, obj.PatientVitals.Height.ToString());
                ClsUtility.AddParameters("@weight", SqlDbType.Int, obj.PatientVitals.Weight.ToString());
                ClsUtility.AddParameters("@MUAC", SqlDbType.Int, obj.PatientVitals.MUAC.ToString());
                ClsUtility.AddParameters("@weightforage", SqlDbType.Int, obj.PatientVitals.Weightforage.ToString());
                ClsUtility.AddParameters("@weightforheight", SqlDbType.Int, obj.PatientVitals.Weightforheight.ToString());
                ClsUtility.AddParameters("@BMIz", SqlDbType.Int, obj.PatientVitals.BMIz.ToString());
                ClsUtility.AddParameters("@NurseComments", SqlDbType.VarChar, obj.PatientVitals.NurseComments.ToString());
                ClsUtility.AddParameters("@HIVCareEnrollmentDate", SqlDbType.DateTime, obj.OrdVisit.VisitDate.ToString());
                ClsUtility.AddParameters("@DateHIVDiagnosis", SqlDbType.DateTime, obj.HIVCare.DateHIVDiagnosis.ToString());
                ClsUtility.AddParameters("@HIVDiagnosisVerified", SqlDbType.Int, obj.HIVCare.HIVDiagnosisVerified.ToString());
                //ClsUtility.AddParameters("@HIVCareWhere", SqlDbType.VarChar, obj.HIVCare.HIVCareWhere.ToString());
                ClsUtility.AddParameters("@ARTTransferInDate", SqlDbType.DateTime, obj.HIVCare.ARTTransferInDate.ToString());
                ClsUtility.AddParameters("@ARTTransferInFrom", SqlDbType.Int, obj.HIVCare.ARTTransferInFrom.ToString());
                ClsUtility.AddParameters("@FromDistrict", SqlDbType.Int, obj.HIVCare.District.ToString());
                ClsUtility.AddParameters("@ARTStartDate", SqlDbType.DateTime, obj.HIVCare.ARTStartDate.ToString());
                ClsUtility.AddParameters("@TransferIn", SqlDbType.Int, obj.HIVCare.TransferIn.ToString());
                ClsUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.DateTime, obj.HIVCare.ConfirmHIVPosDate.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, obj.Signature.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, obj.FormName.ToString());
                ClsUtility.AddParameters("@Spo2", SqlDbType.VarChar, obj.PatientVitals.SPO2.ToString());

                ClsUtility.AddParameters("@MFLCode", SqlDbType.VarChar, obj.HIVCare.MFLCode.ToString());
                ClsUtility.AddParameters("@FirstlineRegimen", SqlDbType.VarChar, obj.HIVCare.ARTStart.FirstLineRegimen.ToString());
                ClsUtility.AddParameters("@CurrentRegimen", SqlDbType.VarChar, obj.HIVCare.ARTStart.CurrentRegimen.ToString());
                ClsUtility.AddParameters("@AnotherWeight", SqlDbType.VarChar, obj.HIVCare.ARTStart.Weight.ToString());
                ClsUtility.AddParameters("@AnotherHeight", SqlDbType.VarChar, obj.HIVCare.ARTStart.Height.ToString());
                ClsUtility.AddParameters("@AnotherMUAC", SqlDbType.VarChar, obj.HIVCare.ARTStart.MUAC.ToString());
                ClsUtility.AddParameters("@AnotherCD4Count", SqlDbType.VarChar, obj.HIVCare.ARTStart.CD4Count.ToString());
                ClsUtility.AddParameters("@BaselineViralLoad", SqlDbType.VarChar, obj.HIVCare.ARTStart.BaselineViralLoad.ToString());
                ClsUtility.AddParameters("@BaselineViralLoadDate", SqlDbType.DateTime, obj.HIVCare.ARTStart.BaselineViralLoadDate.ToString());
                ClsUtility.AddParameters("@AnotherWHOStage", SqlDbType.Int, obj.HIVCare.ARTStart.WhoStage.ToString());

                ClsUtility.AddParameters("@ReferredFrom", SqlDbType.Int, obj.ReferredFrom.ToString());
                ClsUtility.AddParameters("@ReferredFromOther", SqlDbType.VarChar, obj.ReferredFromOther.ToString());

                DataTable ReturnDT = new DataTable();

                ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveUpdateTriage", ClsDBUtility.ObjectEnum.DataTable);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                visitId = (int)ReturnDT.Rows[0]["VisitId"];
                return visitId;
            }
            catch (Exception ex)
            {
                DataMgr.RollBackTransation(this.Transaction);
                flag = false;
                throw ex;

            }

            finally
            {

                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataTable SaveUpdatePresentingComplaints(PresentingComplaint obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.SaveUpdatePresentingComplaints() method called");
            bool flag = true;
            ClsObject clsObjTP = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObjTP.Connection = this.Connection;
                clsObjTP.Transaction = this.Transaction;
                string dtVal = DBNull.Value.ToString();

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());
                ClsUtility.AddParameters("@Menarche", SqlDbType.Int, obj.OBSnGYNHistory.Menarche.ToString());
                ClsUtility.AddParameters("@HPVvaccine", SqlDbType.Int, obj.OBSnGYNHistory.HPVvaccine.ToString());
                ClsUtility.AddParameters("@CervicalCancerScreened", SqlDbType.Int, obj.OBSnGYNHistory.CervicalCancerScreened.ToString());
                if (obj.OBSnGYNHistory.CervicalCancerScreenedDate.HasValue)
                {
                    dtVal = obj.OBSnGYNHistory.CervicalCancerScreenedDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@CervicalCancerScreenedDate", SqlDbType.DateTime, dtVal);
                dtVal = DBNull.Value.ToString();
                if (obj.OBSnGYNHistory.LMP.HasValue)
                {
                    dtVal = obj.OBSnGYNHistory.LMP.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@LMP", SqlDbType.DateTime, dtVal);
                ClsUtility.AddParameters("@Pregnant", SqlDbType.Int, obj.OBSnGYNHistory.Pregnant.ToString());
                dtVal = DBNull.Value.ToString();
                if (obj.OBSnGYNHistory.EDD.HasValue)
                {
                    dtVal = obj.OBSnGYNHistory.EDD.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@EDD", SqlDbType.DateTime, dtVal);
                dtVal = DBNull.Value.ToString();
                if (obj.OBSnGYNHistory.DateofDelivery.HasValue)
                {
                    dtVal = obj.OBSnGYNHistory.DateofDelivery.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@DateofDelivery", SqlDbType.DateTime, dtVal);
                dtVal = DBNull.Value.ToString();
                if (obj.OBSnGYNHistory.DateofInducedAbortion.HasValue)
                {
                    dtVal = obj.OBSnGYNHistory.DateofInducedAbortion.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@DateofInducedAbortion", SqlDbType.DateTime, dtVal);
                dtVal = DBNull.Value.ToString();
                if (obj.OBSnGYNHistory.DateofMiscarriage.HasValue)
                {
                    dtVal = obj.OBSnGYNHistory.DateofMiscarriage.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@DateofMiscarriage", SqlDbType.DateTime, dtVal);
                ClsUtility.AddParameters("@Amenorrhoea", SqlDbType.Int, obj.OBSnGYNHistory.Amenorrhoea.ToString());
                ClsUtility.AddParameters("@BreastStatus", SqlDbType.Int, obj.OBSnGYNHistory.BreastStatus.ToString());
                ClsUtility.AddParameters("@FamilyPlanningMethod", SqlDbType.Int, obj.OBSnGYNHistory.FamilyPlanningMethod.ToString());
                ClsUtility.AddParameters("@FamilyPlanningStatus", SqlDbType.Int, obj.OBSnGYNHistory.FamilyPlanningStatus.ToString());
                ClsUtility.AddParameters("@NoFamilyPlanning", SqlDbType.Int, obj.OBSnGYNHistory.NoFamilyPlanning.ToString());
                ClsUtility.AddParameters("@Gravidae", SqlDbType.Int, obj.OBSnGYNHistory.Gravidae.ToString());
                ClsUtility.AddParameters("@Parity", SqlDbType.Int, obj.OBSnGYNHistory.Parity.ToString());

                dtVal = DBNull.Value.ToString();
                if (obj.OBSnGYNHistory.VDRLTestDate.HasValue)
                {
                    dtVal = obj.OBSnGYNHistory.VDRLTestDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@VDRLTD", SqlDbType.DateTime, dtVal);
                ClsUtility.AddParameters("@VDRLTR", SqlDbType.Int, obj.OBSnGYNHistory.VDRLTestResult.ToString());

                dtVal = DBNull.Value.ToString();
                if (obj.OBSnGYNHistory.HbTestDate.HasValue)
                {
                    dtVal = obj.OBSnGYNHistory.HbTestDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@HbTD", SqlDbType.DateTime, dtVal);
                ClsUtility.AddParameters("@HbTR", SqlDbType.Int, obj.OBSnGYNHistory.HbTestResult.ToString());

                dtVal = DBNull.Value.ToString();
                if (obj.OBSnGYNHistory.RhesusTestDate.HasValue)
                {
                    dtVal = obj.OBSnGYNHistory.RhesusTestDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@RhesusTD", SqlDbType.DateTime, dtVal);
                ClsUtility.AddParameters("@RhesusTR", SqlDbType.Int, obj.OBSnGYNHistory.RhesusTestResult.ToString());

                ClsUtility.AddParameters("@OtherPresentingComplaints", SqlDbType.VarChar, obj.OBSnGYNHistory.OtherPresentingComplaints.ToString());
                ClsUtility.AddParameters("@PreviousAdmissionDiagnosis", SqlDbType.VarChar, obj.OBSnGYNHistory.PreviousAdmissionDiagnosis.ToString());


                DataTable ReturnDT = new DataTable();

                ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SavePresentingComplaints", ClsDBUtility.ObjectEnum.DataTable);

                int deleteAll = 1;
                foreach (MultiSelectTable mst in obj.CDT)
                {

                    ClsUtility.Init_Hashtable();
                    int vid = mst.Visit_Pk;
                    if (vid == 0)
                    {
                        vid = obj.Visit_Id;
                    }
                    ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                    ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, vid.ToString());
                    ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.ValueID.ToString());
                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, mst.FieldName.ToString());
                    dtVal = DBNull.Value.ToString();
                    if (mst.OnSetDate.HasValue)
                    {
                        dtVal = mst.OnSetDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                    }
                    ClsUtility.AddParameters("@OnSetDate", SqlDbType.DateTime, dtVal);
                    ClsUtility.AddParameters("@IsActive", SqlDbType.Int, mst.NumericField.ToString());

                    ReturnDT = new DataTable();

                    ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                    if (deleteAll == 1)
                        deleteAll = 0;

                }

                deleteAll = 1;
                foreach (MultiSelectTable mst in obj.PCT)
                {

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                    ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                    ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.ValueID.ToString());
                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, mst.FieldName.ToString());
                    ClsUtility.AddParameters("@OnSetDate", SqlDbType.DateTime, mst.OnSetDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString());
                    ClsUtility.AddParameters("@IsActive", SqlDbType.Int, mst.NumericField.ToString());

                    ReturnDT = new DataTable();

                    ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                    if (deleteAll == 1)
                        deleteAll = 0;

                }



                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return ReturnDT;
            }
            catch (Exception ex)
            {
                DataMgr.RollBackTransation(this.Transaction);
                flag = false;
                throw ex;

            }

            finally
            {

                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataTable SaveUpdateAdditionalHx(DBAdditionalHx obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.SaveUpdateAdditionalHx() method called");
            bool flag = true;
            ClsObject clsObj = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObj.Connection = this.Connection;
                clsObj.Transaction = this.Transaction;
                string dtVal = DBNull.Value.ToString();

                DataTable ReturnDT = new DataTable();

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());

                ClsUtility.AddParameters("@NeonatalHistoryNotes", SqlDbType.VarChar, obj.NHN.ToString());
                ClsUtility.AddParameters("@ReasonForHerbalMedicine", SqlDbType.VarChar, obj.PAMN.ToString());

                //ClsUtility.AddParameters("@AnotherRegimenStartDate", SqlDbType.DateTime, obj.ASAFRD.ToString());
                //ClsUtility.AddParameters("@AnotherRegimen", SqlDbType.VarChar, obj.ASAFR.ToString());
                //ClsUtility.AddParameters("@AnotherWeight", SqlDbType.VarChar, obj.ASAFW.ToString());
                //ClsUtility.AddParameters("@AnotherHeight", SqlDbType.VarChar, obj.ASAFH.ToString());
                //ClsUtility.AddParameters("@AnotherWHOStage", SqlDbType.Int, obj.ASAFWS.ToString());

                ClsUtility.AddParameters("@SexuallyActiveLast6Months", SqlDbType.Int, obj.SHF.ToString());
                ClsUtility.AddParameters("@SexualOrientation", SqlDbType.Int, obj.SO.ToString());
                ClsUtility.AddParameters("@HIVstatusOfsexualPartner", SqlDbType.Int, obj.PS.ToString());
                ClsUtility.AddParameters("@PartnersIn6Months", SqlDbType.Int, obj.PNO.ToString());
                ClsUtility.AddParameters("@PartnersGender", SqlDbType.Int, obj.PG.ToString());


                ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveUpdateAdditionalHx", ClsDBUtility.ObjectEnum.DataTable);



                /* Milestones */
                foreach (MH mst in obj.MH)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, obj.Ptn_Pk.ToString());
                    ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, obj.Visit_Id.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, obj.LocationId.ToString());
                    ClsUtility.AddParameters("@Section", SqlDbType.VarChar, "Milestone");

                    ClsUtility.AddParameters("@TypeofTestId", SqlDbType.Int, mst.MA.ToString());
                    ClsUtility.AddParameters("@TypeofTest", SqlDbType.VarChar, mst.MAT.ToString());

                    ClsUtility.AddParameters("@ResultId", SqlDbType.Int, mst.ST.ToString());
                    ClsUtility.AddParameters("@Result", SqlDbType.VarChar, mst.STA.ToString());

                    ClsUtility.AddParameters("@Date", SqlDbType.VarChar, mst.DA.ToString("yyyy-MM-dd hh:mm:ss").ToString());
                    ClsUtility.AddParameters("@Comments", SqlDbType.VarChar, mst.CM.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());
                    int temp = (int)clsObj.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SaveGridViewData", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }

                /* Immunization History */
                foreach (IH mst in obj.IH)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, obj.Ptn_Pk.ToString());
                    ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, obj.Visit_Id.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, obj.LocationId.ToString());
                    ClsUtility.AddParameters("@Section", SqlDbType.VarChar, "Immunization");

                    ClsUtility.AddParameters("@TypeofTestId", SqlDbType.Int, mst.IP.ToString());
                    ClsUtility.AddParameters("@TypeofTest", SqlDbType.VarChar, mst.IPT.ToString());

                    ClsUtility.AddParameters("@ResultId", SqlDbType.Int, mst.IG.ToString());
                    ClsUtility.AddParameters("@Result", SqlDbType.VarChar, mst.IGT.ToString());

                    ClsUtility.AddParameters("@Date", SqlDbType.VarChar, mst.DT.ToString("yyyy-MM-dd hh:mm:ss").ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());
                    int temp = (int)clsObj.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SaveGridViewData", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }

                /* Tanners Staging */
                int deleteAll = 1;
                foreach (TSH mst in obj.TSH)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, obj.LocationId.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                    if (obj.TSLS.HasValue)
                    {
                        ClsUtility.AddParameters("@LastScreenedOn", SqlDbType.VarChar, obj.TSLS.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString());
                    }
                    if (obj.TSNS.HasValue)
                    {
                        ClsUtility.AddParameters("@NextScreenedOn", SqlDbType.VarChar, obj.TSNS.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString());
                    }
                    ClsUtility.AddParameters("@StagingDate", SqlDbType.VarChar, mst.SD.ToString("yyyy-MM-dd hh:mm:ss").ToString());
                    //ClsUtility.AddParameters("@Gender", SqlDbType.Int, mst.Gender.ToString());
                    ClsUtility.AddParameters("@BreastsOrGenitals", SqlDbType.Int, mst.BOG.ToString());
                    ClsUtility.AddParameters("@PublicHair", SqlDbType.Int, mst.PH.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                    ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                    int temp = (int)clsObj.ReturnObject(ClsUtility.theParams, "pr_HIVCE_SaveTannersStagingData", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }

                /* Current long Term Medication */
                deleteAll = 1;
                foreach (CLTM mst in obj.CLTM)
                {

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                    ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                    ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.ValId.ToString());
                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, mst.FN.ToString());
                    ClsUtility.AddParameters("@OnSetDate", SqlDbType.DateTime, mst.OSD.ToString("yyyy-MM-dd hh:mm:ss").ToString());
                    ClsUtility.AddParameters("@IsActive", SqlDbType.Int, mst.NT.ToString());
                    if (!string.IsNullOrEmpty(mst.NTT))
                    {
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.Int, mst.NTT.ToString());
                    }

                    if (!string.IsNullOrEmpty(mst.ValIdOtherNote))
                    {
                        ClsUtility.AddParameters("@ValueIdOtherNotes", SqlDbType.VarChar, mst.ValIdOtherNote.ToString());
                    }


                    ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                    if (deleteAll == 1)
                        deleteAll = 0;

                }

                /* Prior ART */
                foreach (PAH mst in obj.PAH)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                    ClsUtility.AddParameters("@VisitId", SqlDbType.Int, obj.Visit_Id.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, obj.LocationId.ToString());
                    ClsUtility.AddParameters("@PurposeId", SqlDbType.Int, mst.PID.ToString());
                    ClsUtility.AddParameters("@Regimen", SqlDbType.Int, mst.Regimen.ToString());
                    ClsUtility.AddParameters("@RegLastUsed", SqlDbType.VarChar, mst.LDU.ToString("yyyy-MM-dd hh:mm:ss").ToString());
                    ClsUtility.AddParameters("@RegimenId", SqlDbType.VarChar, mst.RId.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());
                    int retval = (Int32)clsObj.ReturnObjectNewImpl(ClsUtility.theParams, "pr_Clinical_SavePatientBlueCardPriorART_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                /* Allergies Details */
                deleteAll = 1;
                foreach (AllerH mst in obj.AllerH)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());

                    ClsUtility.AddParameters("@vAllergyType", SqlDbType.VarChar, mst.ATID.ToString());

                    if (mst.ATID.ToString() == "211") // For vAllergyType = Other
                    {
                        ClsUtility.AddParameters("@vAllergen", SqlDbType.VarChar, string.Empty);
                        ClsUtility.AddParameters("@votherAllergen", SqlDbType.VarChar, mst.OAller.ToString());
                    }
                    else if (mst.ATID.ToString() == "207") // For vAllergyType = Other
                    {
                        ClsUtility.AddParameters("@vAllergen", SqlDbType.VarChar, mst.Allergen.ToString());
                        ClsUtility.AddParameters("@votherAllergen", SqlDbType.VarChar, string.Empty);
                    }
                    else
                    {
                        ClsUtility.AddParameters("@vAllergen", SqlDbType.VarChar, mst.AllerTID.ToString());
                        ClsUtility.AddParameters("@votherAllergen", SqlDbType.VarChar, string.Empty);
                    }

                    ClsUtility.AddParameters("@vTypeReaction", SqlDbType.VarChar, mst.TR.ToString());
                    ClsUtility.AddParameters("@vSeverity", SqlDbType.VarChar, mst.STID.ToString());
                    ClsUtility.AddParameters("@vDataAllergy", SqlDbType.VarChar, mst.DAller.ToString());
                    ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                    ClsUtility.AddParameters("@AllergiesComment", SqlDbType.VarChar, mst.Comment);

                    int retval = (Int32)clsObj.ReturnObjectNewImpl(ClsUtility.theParams, "Pr_HIVCE_SaveAllergiesDetails", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    if (deleteAll == 1)
                        deleteAll = 0;
                }

                /* Family History */
                foreach (HIVFM mst in obj.HIVFM)
                {

                    //        public int SaveFamilyInfo(int Id, int Ptn_Pk, string RFirstName, string RLastName, int Sex, int AgeYear,
                    //            int AgeMonth, int RelationshipType,
                    //int HivStatus, int HivCareStatus, int UserId, int DeleteFlag, int ReferenceId, string RegistrationNo, 
                    //            DateTime RelationshipDate, string LastHIVTestDate,
                    //int? LocationID = null, bool EnableHIVstatus = false)

                    string LastHIVTestDate = string.Empty;
                    if (mst.LHTD.HasValue)
                    {
                        LastHIVTestDate = mst.diaplayLHTD;
                    }
                    //IFamilyInfo ifamilyinfo = new BFamilyInfo();
                    int id = Convert.ToInt32(mst.id) > 0 ? Convert.ToInt32(mst.id) : -1;

                    /*
                    ifamilyinfo.SaveFamilyInfo(id, obj.Ptn_Pk, mst.FN.ToString(), mst.LN.ToString(),
                        Convert.ToInt32(mst.Sex), Convert.ToInt32(mst.Age), 0
                        , Convert.ToInt32(mst.RE)
                        , Convert.ToInt32(mst.HS)
                        , Convert.ToInt32(mst.HCS)
                        , userId
                        , 0
                        , obj.Ptn_Pk
                        , String.Empty
                        , (DateTime?)null
                        , LastHIVTestDate
                        , obj.LocationId
                        , false);
                        int Id, int Ptn_Pk, string RFirstName, string RLastName, int Sex, int AgeYear, int AgeMonth, int RelationshipType;
                        int HivStatus, int HivCareStatus, int UserId, int DeleteFlag, int ReferenceId, string RegistrationNo, DateTime? RelationshipDate, string LastHIVTestDate,
                        int? LocationID = null, bool EnableHIVstatus = false;
                    */
                    bool EnableHIVstatus = false;

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, id.ToString());
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                    ClsUtility.AddParameters("@RFirstName", SqlDbType.VarChar, mst.FN.ToString());
                    ClsUtility.AddParameters("@RLastName", SqlDbType.VarChar, mst.LN.ToString());
                    ClsUtility.AddParameters("@Sex", SqlDbType.Int, Convert.ToInt32(mst.Sex).ToString());
                    ClsUtility.AddParameters("@AgeYear", SqlDbType.Int, Convert.ToInt32(mst.Age).ToString());
                    ClsUtility.AddParameters("@AgeMonth", SqlDbType.Int, "0");
                    ClsUtility.AddParameters("@RelationshipType", SqlDbType.Int, Convert.ToInt32(mst.RT).ToString());
                    ClsUtility.AddParameters("@HivStatus", SqlDbType.Int, Convert.ToInt32(mst.HS).ToString());
                    ClsUtility.AddParameters("@LastHIVTestDate", SqlDbType.VarChar, LastHIVTestDate);
                    ClsUtility.AddParameters("@HivCareStatus", SqlDbType.Int, Convert.ToInt32(mst.HCS).ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                    if (mst.RE == "0")
                    {
                        ClsUtility.AddParameters("@ReferenceId", SqlDbType.Int, "-1");
                    }
                    else
                    {
                        ClsUtility.AddParameters("@ReferenceId", SqlDbType.Int, obj.Ptn_Pk.ToString());
                    }

                    ClsUtility.AddParameters("@RegistrationNo", SqlDbType.VarChar, String.Empty);
                    ClsUtility.AddParameters("@RelationshipDate", SqlDbType.DateTime, DBNull.Value.ToString());

                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, obj.LocationId.ToString());
                    ClsUtility.AddParameters("@EnableHIVstatus", SqlDbType.Bit, EnableHIVstatus.ToString());
                    ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    int retval = (Int32)clsObj.ReturnObject(ClsUtility.theParams, "Pr_Clinical_SaveFamilyInfo_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }

                /* Sexual History > High Risk Behaviour */
                deleteAll = 1;
                foreach (HRB mst in obj.HRB)
                {
                    if (mst.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.id.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "HighRisk");

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }

                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return ReturnDT;
            }
            catch (Exception ex)
            {
                DataMgr.RollBackTransation(this.Transaction);
                flag = false;
                throw ex;

            }

            finally
            {

                clsObj = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataTable SaveUpdateScreeningData(DBScreeningData obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.SaveUpdateScreeningData() method called");
            bool flag = true;
            ClsObject clsObj = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObj.Connection = this.Connection;
                clsObj.Transaction = this.Transaction;
                string dtVal = DBNull.Value.ToString();

                DataTable ReturnDT = new DataTable();

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());

                ClsUtility.AddParameters("@TbFindings", SqlDbType.Int, obj.TbFindings.ToString());
                ClsUtility.AddParameters("@TBRegimen", SqlDbType.Int, obj.TBRegimen.ToString());
                ClsUtility.AddParameters("@TBTreatment", SqlDbType.Int, obj.TBTreatment.ToString());
                ClsUtility.AddParameters("@RxDuration", SqlDbType.Int, obj.RxDuration.ToString());
                ClsUtility.AddParameters("@SputumSmear", SqlDbType.Int, obj.SputumSmear.ToString());
                ClsUtility.AddParameters("@GeneExpert", SqlDbType.Int, obj.GeneExpert.ToString());
                ClsUtility.AddParameters("@SputumDST", SqlDbType.Int, obj.SputumDST.ToString());
                ClsUtility.AddParameters("@CXRResults", SqlDbType.Int, obj.CXRResults.ToString());
                ClsUtility.AddParameters("@IPT", SqlDbType.Int, obj.IPT.ToString());
                ClsUtility.AddParameters("@IPTAdherence", SqlDbType.Int, obj.IPTAdherence.ToString());
                ClsUtility.AddParameters("@IPTContraindication", SqlDbType.Int, obj.IPTContraindication.ToString());
                ClsUtility.AddParameters("@IPTDiscontinued", SqlDbType.Int, obj.IPTDiscontinued.ToString());
                ClsUtility.AddParameters("@IsTBContactsScreened", SqlDbType.Int, obj.IsTBContactsScreened.ToString());
                ClsUtility.AddParameters("@rbChestXray", SqlDbType.Int, obj.rbChestXray.ToString());
                ClsUtility.AddParameters("@rbTissueBiopsy", SqlDbType.Int, obj.rbTissueBiopsy.ToString());
                ClsUtility.AddParameters("@TBStartDate ", SqlDbType.VarChar, obj.TBStartDate.HasValue == true ? obj.TBStartDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                ClsUtility.AddParameters("@SputumSmearDate ", SqlDbType.VarChar, obj.SputumSmearDate.HasValue == true ? obj.SputumSmearDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                ClsUtility.AddParameters("@GeneExpertDate ", SqlDbType.VarChar, obj.GeneExpertDate.HasValue == true ? obj.GeneExpertDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                ClsUtility.AddParameters("@SputumDSTDate ", SqlDbType.VarChar, obj.SputumDSTDate.HasValue == true ? obj.SputumDSTDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                ClsUtility.AddParameters("@ChestXrayDate ", SqlDbType.VarChar, obj.ChestXrayDate.HasValue == true ? obj.ChestXrayDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                ClsUtility.AddParameters("@TissueBiopsyDate ", SqlDbType.VarChar, obj.TissueBiopsyDate.HasValue == true ? obj.TissueBiopsyDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                ClsUtility.AddParameters("@IPTStartDate ", SqlDbType.VarChar, obj.IPTStartDate.HasValue == true ? obj.IPTStartDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                ClsUtility.AddParameters("@IPTEndDate ", SqlDbType.VarChar, obj.IPTEndDate.HasValue == true ? obj.IPTEndDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                ClsUtility.AddParameters("@EligibleForIPT", SqlDbType.Int, obj.EligibleForIPT.ToString());

                ClsUtility.AddParameters("@IPTWPYellowColoredUrine", SqlDbType.Int, obj.IPTWPYellowColoredUrine.ToString());
                ClsUtility.AddParameters("@IPTWPNumbnessBurning", SqlDbType.Int, obj.IPTWPNumbnessBurning.ToString());
                ClsUtility.AddParameters("@IPTWPYellownessEyes", SqlDbType.Int, obj.IPTWPYellownessEyes.ToString());
                ClsUtility.AddParameters("@IPTWPTenderness", SqlDbType.Int, obj.IPTWPTenderness.ToString());

                ClsUtility.AddParameters("@OtherReasonDeclinedIPT", SqlDbType.VarChar, obj.OtherReasonDeclinedIPT.ToString());
                ClsUtility.AddParameters("@OtherReasonDiscontinuedIPT", SqlDbType.VarChar, obj.OtherReasonDiscontinuedIPT.ToString());


                ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveUpdateScreeningData", ClsDBUtility.ObjectEnum.DataTable);

                int deleteAll = 1;
                foreach (TBScreening mst in obj.TBScreenings)
                {
                    if (mst.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.id.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "TBAssessmentICF");

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                DBAlcoholDepressionScreening ads = new DBAlcoholDepressionScreening();
                obj.AlcoholDepressionScreening.FirstOrDefault().Ptn_pk = obj.Ptn_Pk;
                obj.AlcoholDepressionScreening.FirstOrDefault().Visit_Id = obj.Visit_Id;
                ads.SaveUpdateAlcoholDepressionScreening(obj.AlcoholDepressionScreening.FirstOrDefault());

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return ReturnDT;
            }
            catch (Exception ex)
            {
                DataMgr.RollBackTransation(this.Transaction);
                flag = false;
                throw ex;

            }

            finally
            {

                clsObj = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataTable SaveUpdateSystemicReviewData(DBSystemicReviewData obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.SaveUpdateSystemicReviewData() method called");
            bool flag = true;
            ClsObject clsObj = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObj.Connection = this.Connection;
                clsObj.Transaction = this.Transaction;
                string dtVal = DBNull.Value.ToString();

                DataTable ReturnDT = new DataTable();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                ClsUtility.AddParameters("@ReviewSystemComments", SqlDbType.VarChar, obj.ReviewSystemComments.ToString());
                ClsUtility.AddParameters("@NutritionalAssessment", SqlDbType.VarChar, obj.NutritionalAssessment.ToString());
                ClsUtility.AddParameters("@NutritionalAssessmentComments", SqlDbType.VarChar, obj.NutritionalAssessmentComments.ToString());
                ClsUtility.AddParameters("@WHOStage", SqlDbType.Int, obj.WHOStage.ToString());
                ClsUtility.AddParameters("@NutritionCounseling", SqlDbType.Int, obj.NutritionCounseling.ToString());



                ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveUpdateSystemicReviewData", ClsDBUtility.ObjectEnum.DataTable);

                /* GeneralExamination */
                int deleteAll = 1;
                foreach (DBMultiSelectSelcted mst in obj.GeneralExamination)
                {
                    if (mst.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.id.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "GeneralConditions");

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                /* SkinExamination */
                deleteAll = 1;
                foreach (DBMultiSelectSelcted mst in obj.SkinExamination)
                {
                    if (mst.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.id.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "SkinConditions");

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                /* ENTExamination */
                deleteAll = 1;
                foreach (DBMultiSelectSelcted mst in obj.ENTExamination)
                {
                    if (mst.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.id.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "ENTConditions");

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                /* ChestExamination */
                deleteAll = 1;
                foreach (DBMultiSelectSelcted mst in obj.ChestExamination)
                {
                    if (mst.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.id.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "ChestLungsConditions");

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                /* CVSExamination */
                deleteAll = 1;
                foreach (DBMultiSelectSelcted mst in obj.CVSExamination)
                {
                    if (mst.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.id.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "CardiovascularConditions");

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                /* AbdomenExamination */
                deleteAll = 1;
                foreach (DBMultiSelectSelcted mst in obj.AbdomenExamination)
                {
                    if (mst.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.id.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "AbdomenConditions");

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                /* CNSExamination */
                deleteAll = 1;
                foreach (DBMultiSelectSelcted mst in obj.CNSExamination)
                {
                    if (mst.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.id.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "CNSConditions");

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                /* GenitalUrinaryConditions */
                deleteAll = 1;
                foreach (DBMultiSelectSelcted mst in obj.GenitoUrinary)
                {
                    if (mst.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.id.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "GenitalUrinaryConditions");

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                /* WHOStageIConditions */
                deleteAll = 1;
                foreach (WHOStage mst in obj.WHOStages.Where(o => o.FieldName == "WHOStageIConditions").ToList())
                {
                    if (mst.ValueId != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.ValueId.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, mst.FieldName);
                        ClsUtility.AddParameters("@OnSetDate", SqlDbType.VarChar, mst.DateField1.HasValue == true ? mst.DateField1.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                        ClsUtility.AddParameters("@DateField2", SqlDbType.VarChar, mst.DateField2.HasValue == true ? mst.DateField2.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                /* WHOStageIIConditions */
                deleteAll = 1;
                foreach (WHOStage mst in obj.WHOStages.Where(o => o.FieldName == "WHOStageIIConditions").ToList())
                {
                    if (mst.ValueId != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.ValueId.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, mst.FieldName);
                        ClsUtility.AddParameters("@OnSetDate", SqlDbType.VarChar, mst.DateField1.HasValue == true ? mst.DateField1.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                        ClsUtility.AddParameters("@DateField2", SqlDbType.VarChar, mst.DateField2.HasValue == true ? mst.DateField2.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                /* WHOStageIIICoditions */
                deleteAll = 1;
                foreach (WHOStage mst in obj.WHOStages.Where(o => o.FieldName == "WHOStageIIICoditions").ToList())
                {
                    if (mst.ValueId != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.ValueId.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, mst.FieldName);
                        ClsUtility.AddParameters("@OnSetDate", SqlDbType.VarChar, mst.DateField1.HasValue == true ? mst.DateField1.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                        ClsUtility.AddParameters("@DateField2", SqlDbType.VarChar, mst.DateField2.HasValue == true ? mst.DateField2.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                /* WHOStageIVConditions */
                deleteAll = 1;
                foreach (WHOStage mst in obj.WHOStages.Where(o => o.FieldName == "WHOStageIVConditions").ToList())
                {
                    if (mst.ValueId != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_Pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.ValueId.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, mst.FieldName);
                        ClsUtility.AddParameters("@OnSetDate", SqlDbType.VarChar, mst.DateField1.HasValue == true ? mst.DateField1.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                        ClsUtility.AddParameters("@DateField2", SqlDbType.VarChar, mst.DateField2.HasValue == true ? mst.DateField2.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }



                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return ReturnDT;
            }
            catch (Exception ex)
            {
                DataMgr.RollBackTransation(this.Transaction);
                flag = false;
                throw ex;

            }

            finally
            {

                clsObj = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataTable SaveUpdateManagementxData(ManagementDB obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.SaveUpdateManagementxData() method called");
            bool flag = true;
            ClsObject clsObj = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObj.Connection = this.Connection;
                clsObj.Transaction = this.Transaction;
                string dtVal = DBNull.Value.ToString();

                DataTable ReturnDT = new DataTable();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());

                ClsUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, obj.ARTSideEffect.ToString());

                ClsUtility.AddParameters("@ForgetMedicineSinceLastVisit", SqlDbType.Int, obj.MAdherence.IsForgotMed.ToString());
                ClsUtility.AddParameters("@CarelessAboutTakingMedicine", SqlDbType.Int, obj.MAdherence.IsCarelessMed.ToString());
                ClsUtility.AddParameters("@FeelWorseStopTakingMedicine", SqlDbType.Int, obj.MAdherence.IsWorseTakingMed.ToString());
                ClsUtility.AddParameters("@FeelBetterStopTakingMedicine", SqlDbType.Int, obj.MAdherence.IsFeelBetterMed.ToString());
                ClsUtility.AddParameters("@TakeMedicineYesterday", SqlDbType.Int, obj.MAdherence.IsYesterdayMed.ToString());
                ClsUtility.AddParameters("@SymptomsUnderControl_StopTakingMedicine", SqlDbType.Int, obj.MAdherence.IsSymptomUnderControl.ToString());
                ClsUtility.AddParameters("@UnderPresureStickingYourTreatmentPlan", SqlDbType.Int, obj.MAdherence.IsStickingTreatmentPlan.ToString());
                ClsUtility.AddParameters("@RememberingMedications", SqlDbType.Int, obj.MAdherence.RememberingMedications.ToString());
                ClsUtility.AddParameters("@MMAS4_Score", SqlDbType.VarChar, obj.MAdherence.MMAS4Score.ToString());
                ClsUtility.AddParameters("@MMAS8_Score", SqlDbType.VarChar, obj.MAdherence.MMAS8Score.ToString());
                ClsUtility.AddParameters("@MMAS4_AdherenceRating", SqlDbType.VarChar, obj.MAdherence.MMAS4Rating.ToString());
                ClsUtility.AddParameters("@MMAS8_AdherenceRating", SqlDbType.VarChar, obj.MAdherence.MMAS8Rating.ToString());
                ClsUtility.AddParameters("@ReferToCounselor", SqlDbType.VarChar, obj.MAdherence.MMAS8Suggestion.ToString());

                ClsUtility.AddParameters("@CTXWhyPoorFair", SqlDbType.Int, obj.CTXWhypoorfair.ToString());
                ClsUtility.AddParameters("@CTXAdherence", SqlDbType.Int, obj.CTXAdhere.ToString());
                ClsUtility.AddParameters("@ARVAdherence", SqlDbType.Int, obj.ARVAdhere.ToString());
                ClsUtility.AddParameters("@ARVWhyPoorFair", SqlDbType.Int, obj.ARVWhypoorfair.ToString());
                ClsUtility.AddParameters("@TherapyPlan", SqlDbType.Int, obj.SubsituationInterruption.ToString());
                ClsUtility.AddParameters("@RegimenId", SqlDbType.Int, obj.RegimenCodeValue.ToString());
                if (obj.SubsituationInterruption == 98) // Change Regimen
                {
                    ClsUtility.AddParameters("@TherapyReasonCode", SqlDbType.Int, obj.ChangeRegimenReason.ToString());
                }
                else if (obj.SubsituationInterruption == 99) // Change Regimen
                {
                    ClsUtility.AddParameters("@TherapyReasonCode", SqlDbType.Int, obj.StopRegimenReason.ToString());
                }
                else
                {
                    ClsUtility.AddParameters("@TherapyReasonCode", SqlDbType.Int, "0");
                }

                ClsUtility.AddParameters("@EligibleThrough", SqlDbType.Int, obj.EligibleART.ToString());
                ClsUtility.AddParameters("@ChangeRegimenReasondiscussbyMDT", SqlDbType.Int, obj.ThreapyChange.ToString());
                ClsUtility.AddParameters("@ChangeRegimenReasondiscussbyMDTComments", SqlDbType.VarChar, obj.ARTChangeReasonDiscussbyMDTComments.ToString());
                ClsUtility.AddParameters("@DateARTEnded", SqlDbType.Int, obj.ARTDateEnded.HasValue == true ? obj.ARTDateEnded.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());

                ClsUtility.AddParameters("@ScreenedForSTI", SqlDbType.Int, obj.STIScreening.ToString());
                ClsUtility.AddParameters("@PartnerNotify", SqlDbType.Int, obj.PartnerNotified.ToString());

                ClsUtility.AddParameters("@WorkUpPlan", SqlDbType.VarChar, obj.AreaReminders.ToString());
                ClsUtility.AddParameters("@PatientRefDesc", SqlDbType.VarChar, obj.AreaReferrals.ToString());
                ClsUtility.AddParameters("@AppDate", SqlDbType.Int, obj.NextAppointmentDate.HasValue == true ? obj.NextAppointmentDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                ClsUtility.AddParameters("@AppReason", SqlDbType.VarChar, obj.Purpose.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, obj.MgtSignature.ToString());
                ClsUtility.AddParameters("@PatientClassification", SqlDbType.Int, obj.PatientClassification.ToString());
                ClsUtility.AddParameters("@IsEnrolDifferenciatedCare", SqlDbType.Int, obj.IsEnrolDifferenciatedCare.ToString());
                ClsUtility.AddParameters("@ARTRefillModel", SqlDbType.Int, obj.ARTRefillModel.ToString());
                ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveUpdateManagementxData", ClsDBUtility.ObjectEnum.DataTable);


                int deleteAll = 1;
                foreach (DBMultiSelectSelcted mst in obj.SortTermSideEffect)
                {
                    if (mst.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.id.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "ShortTermEffects");

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                deleteAll = 1;
                foreach (DBMultiSelectSelcted mst in obj.LongTermSideEffect)
                {
                    if (mst.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.id.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "LongTermEffects");

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                deleteAll = 1;
                foreach (ICD icd in obj.ICDValues)
                {
                    if (icd.ICDId != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, obj.LocationId.ToString());
                        ClsUtility.AddParameters("@BlockId", SqlDbType.Int, icd.BlockId.ToString());
                        ClsUtility.AddParameters("@ICDCodeId", SqlDbType.Int, icd.ICDId.ToString());
                        ClsUtility.AddParameters("@SubBlockId", SqlDbType.Int, icd.SubBlockId.ToString());
                        ClsUtility.AddParameters("@Comments", SqlDbType.VarChar, obj.ICDC.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());

                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveICD10GridValues", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }


                int temp = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, obj.LocationId.ToString());
                ClsUtility.AddParameters("@visitID", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@RiskPopulationID", SqlDbType.Int, obj.KeyPopulationOption.ToString());
                temp = (int)clsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareAtRiskPopulation", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                temp = 0;
                foreach (DBMultiSelectSelcted mss in obj.PWP)
                {
                    if (mss.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, obj.Ptn_pk.ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, obj.LocationId.ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@pwpID", SqlDbType.Int, mss.id.ToString());
                        ClsUtility.AddParameters("@pwpID_Other", SqlDbType.VarChar, string.Empty);
                        temp = (int)clsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCarePreventionwithpositives", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                foreach (Appointment mst in obj.Appointments)
                {
                    if (mst.Id == 0)
                    {
                        ClsUtility.Init_Hashtable();

                        ClsUtility.AddParameters("@PatientId", SqlDbType.Int, obj.Ptn_pk.ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                        ClsUtility.AddParameters("@AppDate", SqlDbType.DateTime, mst.AppointmentDate.HasValue == true ? mst.AppointmentDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                        ClsUtility.AddParameters("@AppReasonId", SqlDbType.Int, mst.Purpose.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());
                        ClsUtility.AddParameters("@AppType", SqlDbType.Int, mst.AppointmentType.ToString());
                        ClsUtility.AddParameters("@ServiceArea", SqlDbType.Int, mst.ServiceArea.ToString());
                        ClsUtility.AddParameters("@Description", SqlDbType.VarChar, mst.Description.ToString());


                        ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveAppointment", ClsDBUtility.ObjectEnum.DataTable);
                    }
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return ReturnDT;
            }
            catch (Exception ex)
            {
                DataMgr.RollBackTransation(this.Transaction);
                flag = false;
                throw ex;

            }

            finally
            {

                clsObj = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int SaveUpdateTSTriage(TSTriage obj, int userId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.SaveUpdateTSTriage() method called");
            bool flag = true;
            int visitId = 0;
            ClsObject clsObjTP = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObjTP.Connection = this.Connection;
                clsObjTP.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, obj.LocationId.ToString());
                ClsUtility.AddParameters("@Visitdate", SqlDbType.DateTime, obj.OrdVisit.VisitDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss"));
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                ClsUtility.AddParameters("@VisitTypeId", SqlDbType.Int, obj.OrdVisit.VisitTypeId.ToString());

                ClsUtility.AddParameters("@AppDate", SqlDbType.Int, obj.NextAppointmentDate.HasValue == true ? obj.NextAppointmentDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                ClsUtility.AddParameters("@AppReason", SqlDbType.Int, obj.Purpose.ToString());
                ClsUtility.AddParameters("@Signature", SqlDbType.Int, obj.MgtSignature.ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, obj.FormName.ToString());

                DataTable ReturnDT = new DataTable();

                ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveUpdateTSTriage", ClsDBUtility.ObjectEnum.DataTable);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                visitId = (int)ReturnDT.Rows[0]["VisitId"];
                return visitId;
            }
            catch (Exception ex)
            {
                DataMgr.RollBackTransation(this.Transaction);
                flag = false;
                throw ex;

            }

            finally
            {

                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }


        public DataSet GetRefillEncounter(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.GetRefillEncounter() Method Called.");
            DataSet ds = new DataSet();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitPK.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ds = (DataSet)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_GetRefillEncounter", ClsDBUtility.ObjectEnum.DataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return ds;
        }

        public DataTable SaveRefillEncounterData(RefillEncounterDB obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.SaveRefillEncounterData() method called");
            bool flag = true;
            int visitId = 0;
            ClsObject clsObjTP = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObjTP.Connection = this.Connection;
                clsObjTP.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, obj.LocationId.ToString());
                ClsUtility.AddParameters("@Visitdate", SqlDbType.DateTime, obj.OrdVisit.VisitDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss"));
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());
                ClsUtility.AddParameters("@VisitTypeId", SqlDbType.Int, obj.OrdVisit.VisitTypeId.ToString());
                ClsUtility.AddParameters("@BPDiastolic", SqlDbType.Int, obj.PatientVitals.BPDiastolic.ToString());
                ClsUtility.AddParameters("@BPSystolic", SqlDbType.Int, obj.PatientVitals.BPSystolic.ToString());
                ClsUtility.AddParameters("@TEMP", SqlDbType.Float, string.IsNullOrEmpty(obj.PatientVitals.TEMP) != true ? obj.PatientVitals.TEMP.ToString() : "0");
                ClsUtility.AddParameters("@RR", SqlDbType.Int, obj.PatientVitals.RR.ToString());
                ClsUtility.AddParameters("@HR", SqlDbType.Int, obj.PatientVitals.HR.ToString());
                ClsUtility.AddParameters("@Headcircumference", SqlDbType.Float, string.IsNullOrEmpty(obj.PatientVitals.HeadCircumference) != true ? obj.PatientVitals.HeadCircumference.ToString() : "0");
                ClsUtility.AddParameters("@height", SqlDbType.Int, obj.PatientVitals.Height.ToString());
                ClsUtility.AddParameters("@weight", SqlDbType.Int, obj.PatientVitals.Weight.ToString());
                ClsUtility.AddParameters("@MUAC", SqlDbType.Int, obj.PatientVitals.MUAC.ToString());

                ClsUtility.AddParameters("@Spo2", SqlDbType.VarChar, obj.PatientVitals.SPO2.ToString());
                string dtVal = DBNull.Value.ToString();
                if (obj.LMP.HasValue)
                {
                    dtVal = obj.LMP.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@LMP", SqlDbType.DateTime, dtVal);
                ClsUtility.AddParameters("@Pregnant", SqlDbType.Int, obj.Pregnant.ToString());
                ClsUtility.AddParameters("@BreastStatus", SqlDbType.Int, obj.BreastStatus.ToString());
                ClsUtility.AddParameters("@FamilyPlanningMethod", SqlDbType.Int, obj.FamilyPlanningMethod.ToString());
                ClsUtility.AddParameters("@FamilyPlanningStatus", SqlDbType.Int, obj.FamilyPlanningStatus.ToString());
                ClsUtility.AddParameters("@NoFamilyPlanning", SqlDbType.Int, obj.NoFamilyPlanning.ToString());
                ClsUtility.AddParameters("@ForgetMedicineSinceLastVisit", SqlDbType.Int, obj.IsForgotMed.ToString());
                ClsUtility.AddParameters("@NoMissedDoses", SqlDbType.Int, obj.NoMissedDoses.ToString());
                ClsUtility.AddParameters("@Drugs", SqlDbType.Int, obj.Drugs.ToString());

                ClsUtility.AddParameters("@WorkUpPlan", SqlDbType.Int, obj.AreaReminders.ToString());
                ClsUtility.AddParameters("@OtherPresentingComplaints", SqlDbType.VarChar, obj.OtherPresentingComplaints.ToString());

                DataTable ReturnDT = new DataTable();

                ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveRefillEncounter", ClsDBUtility.ObjectEnum.DataTable);
                visitId = (int)ReturnDT.Rows[0]["VisitId"];
                // return visitId;
                obj.Visit_Id = visitId;
                int temp = 0;
                foreach (DBMultiSelectSelcted mss in obj.PWP)
                {
                    if (mss.id != 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientID", SqlDbType.Int, obj.Ptn_pk.ToString());
                        ClsUtility.AddParameters("@locationID", SqlDbType.Int, obj.LocationId.ToString());
                        ClsUtility.AddParameters("@visitID", SqlDbType.Int, obj.Visit_Id.ToString());
                        ClsUtility.AddParameters("@pwpID", SqlDbType.Int, mss.id.ToString());
                        ClsUtility.AddParameters("@pwpID_Other", SqlDbType.VarChar, string.Empty);
                        temp = (int)clsObjTP.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCarePreventionwithpositives", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                foreach (Appointment mst in obj.Appointments)
                {
                    if (mst.Id == 0)
                    {
                        ClsUtility.Init_Hashtable();

                        ClsUtility.AddParameters("@PatientId", SqlDbType.Int, obj.Ptn_pk.ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                        ClsUtility.AddParameters("@AppDate", SqlDbType.DateTime, mst.AppointmentDate.HasValue == true ? mst.AppointmentDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                        ClsUtility.AddParameters("@AppReasonId", SqlDbType.Int, mst.Purpose.ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());
                        ClsUtility.AddParameters("@AppType", SqlDbType.Int, mst.AppointmentType.ToString());
                        ClsUtility.AddParameters("@ServiceArea", SqlDbType.Int, mst.ServiceArea.ToString());
                        ClsUtility.AddParameters("@Description", SqlDbType.VarChar, mst.Description.ToString());


                        ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveAppointment", ClsDBUtility.ObjectEnum.DataTable);
                    }
                }

                int deleteAll = 1;
                foreach (MultiSelectTable mst in obj.PCT)
                {

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                    ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                    ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.ValueID.ToString());
                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, mst.FieldName.ToString());
                    ClsUtility.AddParameters("@OnSetDate", SqlDbType.DateTime, mst.OnSetDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString());
                    ClsUtility.AddParameters("@IsActive", SqlDbType.Int, mst.NumericField.ToString());

                    ReturnDT = new DataTable();

                    ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                    if (deleteAll == 1)
                        deleteAll = 0;

                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return ReturnDT;

            }
            catch (Exception ex)
            {
                DataMgr.RollBackTransation(this.Transaction);
                flag = false;
                throw ex;

            }

            finally
            {

                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataSet GetOIChronicDiseaseData(int ptn_pk,  int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBClinicalEncounter.GetOIChronicDiseaseData() Method Called.");
            DataSet ds = new DataSet();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                ds = (DataSet)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_GetOIChronicDiseaseData", ClsDBUtility.ObjectEnum.DataSet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return ds;
        }


    }


}
