using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Application.Common;
using Entities.Common;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using HIVCE.Common;
using Interface.Clinical;

namespace BusinessProcess.Clinical
{
    public class BClinicalSummary : ProcessBase, IClinicalSummaryForm
    {
        ClsDBUtility oUtility = new ClsDBUtility();
        public ClinicalSummary GetClinicalSummaryData(int ptn_pk, int Visit_Id, int LocationID)
        {
            // oUtility = new ClsDBUtility();
            oUtility.Init_Hashtable();

            oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(ptn_pk).ToString());
            oUtility.AddParameters("@Visit_Id", SqlDbType.Int, ConverTotValue.NullToInt(Visit_Id).ToString());
            oUtility.AddParameters("@LocationID", SqlDbType.Int, ConverTotValue.NullToInt(LocationID).ToString());

            ClsObject GetRecs = new ClsObject();
            DataSet dsClinicalSummary = (DataSet)GetRecs.ReturnObject(oUtility.theParams, "pr_GetClinicalSummaryData", ClsDBUtility.ObjectEnum.DataSet);

            ClinicalSummary objClinicalSummary = new ClinicalSummary();
            try
            {
                if (dsClinicalSummary != null)
                {
                    if (dsClinicalSummary.Tables[0].Rows.Count > 0)
                    {
                        objClinicalSummary.MFLName = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["MFLName"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["MFLName"]) : "0";
                        objClinicalSummary.PatientEnrollmentID = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["PatientEnrollmentID"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["PatientEnrollmentID"]) : "0";
                        objClinicalSummary.DOB = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["DOB"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["DOB"]) : "0";
                        objClinicalSummary.EnrollmentDate = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["EnrollmentDate"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["EnrollmentDate"]) : "0";

                        objClinicalSummary.Sex = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["Sex"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["Sex"]) : "0";
                        objClinicalSummary.CurrentWeight = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["CurrentWeight"].ToString()) == false
                            ? Convert.ToDecimal(dsClinicalSummary.Tables[0].Rows[0]["CurrentWeight"]) : 0;
                        objClinicalSummary.CurrentHeight = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["CurrentHeight"].ToString()) == false
                            ? Convert.ToDecimal(dsClinicalSummary.Tables[0].Rows[0]["CurrentHeight"]) : 0;
                        objClinicalSummary.Form_filling_date = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["Form_filling_date"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["Form_filling_date"]) : "";
                        objClinicalSummary.CliniciansName = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["CliniciansName"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["CliniciansName"]) : "0";
                        objClinicalSummary.Telephone = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["Telephone"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["Telephone"]) : "0";
                        objClinicalSummary.Email = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["Email"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["Email"]) : "0";
                        objClinicalSummary.PrimaryReason = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["PrimaryReason"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["PrimaryReason"]) : "0";
                        objClinicalSummary.ClinicalEvaluation = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["ClinicalEvaluation"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["ClinicalEvaluation"]) : "0";
                        objClinicalSummary.NumberOfAdherence = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["NumberOfAdherence"].ToString()) == false
                            ? Convert.ToInt32(dsClinicalSummary.Tables[0].Rows[0]["NumberOfAdherence"]) : 0;
                        objClinicalSummary.NumberOfHomeVisit = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["NumberOfHomeVisit"].ToString()) == false
                            ? Convert.ToInt32(dsClinicalSummary.Tables[0].Rows[0]["NumberOfHomeVisit"]) : 0;
                        objClinicalSummary.SupportStructures = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["SupportStructures"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["SupportStructures"]) : "0";
                        objClinicalSummary.EvidenceOfAdherence = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["EvidenceOfAdherence"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["EvidenceOfAdherence"]) : "0";
                        objClinicalSummary.NumberOfDOTS = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["NumberOfDOTS"].ToString()) == false
                            ? Convert.ToInt32(dsClinicalSummary.Tables[0].Rows[0]["NumberOfDOTS"]) : 0;
                        objClinicalSummary.PoorAdherence = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["PoorAdherence"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["PoorAdherence"]) : "0";
                        objClinicalSummary.EvaluationForOtherText = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["EvaluationForOtherText"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["EvaluationForOtherText"]) : "0";
                        objClinicalSummary.EvaluationForOtherVal = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["EvaluationForOtherVal"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["EvaluationForOtherVal"]) : "0";
                        objClinicalSummary.CommentOnTreatment = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["CommentOnTreatment"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["CommentOnTreatment"]) : "0";
                        objClinicalSummary.HasDrugResistance = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["HasDrugResistance"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["HasDrugResistance"]) : "0";
                        objClinicalSummary.HasFacilityMultidisciplinary = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["HasFacilityMultidisciplinary"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["HasFacilityMultidisciplinary"]) : "0";
                        objClinicalSummary.MDTMembers = string.IsNullOrEmpty(dsClinicalSummary.Tables[0].Rows[0]["MDTMembers"].ToString()) == false
                            ? Convert.ToString(dsClinicalSummary.Tables[0].Rows[0]["MDTMembers"]) : "0";
                    }
                    if (dsClinicalSummary.Tables[1].Rows.Count > 0)
                    {
                        objClinicalSummary.AllARTRegimensLaboratory = new List<ARTRegimensLaboratory>();
                        foreach (DataRow row in dsClinicalSummary.Tables[1].Rows)
                        {
                            ARTRegimensLaboratory ObjArtRegimen = new ARTRegimensLaboratory();
                            ObjArtRegimen.Date = Convert.ToString(row["ReportDate"]);
                            ObjArtRegimen.CD4 = Convert.ToString(row["CD4"]);
                            ObjArtRegimen.HB = Convert.ToString(row["HB"]);
                            ObjArtRegimen.EGFR = Convert.ToString(row["GFR"]);
                            ObjArtRegimen.ViralLoad = Convert.ToString(row["ViralLoad"]);                            
                            objClinicalSummary.AllARTRegimensLaboratory.Add(ObjArtRegimen);
                        }
                    }
                    if (dsClinicalSummary.Tables[2].Rows.Count > 0)
                    {
                        objClinicalSummary.AllRegimenChanges = new List<RegimenChanges>();
                        foreach (DataRow row in dsClinicalSummary.Tables[2].Rows)
                        {
                            RegimenChanges ObjRegimenChange = new RegimenChanges();
                            ObjRegimenChange.Date = Convert.ToString(row["Date"]);
                            ObjRegimenChange.Regimen = Convert.ToString(row["Regimen"]);
                            ObjRegimenChange.Switch = Convert.ToString(row["Switch"]);
                            objClinicalSummary.AllRegimenChanges.Add(ObjRegimenChange);
                        }
                    }
                    if (dsClinicalSummary.Tables[3].Rows.Count > 0)
                    {
                        objClinicalSummary.AllOIS = new List<OIS>();
                        foreach (DataRow row in dsClinicalSummary.Tables[3].Rows)
                        {
                            OIS ObjOIS = new OIS();
                            ObjOIS.Date = Convert.ToString(row["Date"]);
                            ObjOIS.OISData = Convert.ToString(row["OIS"]);
                            objClinicalSummary.AllOIS.Add(ObjOIS);
                        }
                    }
                    if (dsClinicalSummary.Tables[4].Rows.Count > 0)
                    {
                        objClinicalSummary.AllChronicConditions = new List<ChronicConditions>();
                        foreach (DataRow row in dsClinicalSummary.Tables[4].Rows)
                        {
                            ChronicConditions ObjChronicConditions = new ChronicConditions();
                            ObjChronicConditions.Date = Convert.ToString(row["Date"]);
                            ObjChronicConditions.Condition = Convert.ToString(row["Condition"]);
                            objClinicalSummary.AllChronicConditions.Add(ObjChronicConditions);
                        }
                    }
                    if (dsClinicalSummary.Tables[5].Rows.Count > 0)
                    {
                        objClinicalSummary.AllAnthropoemetric = new List<Anthropoemetric>();
                        foreach (DataRow row in dsClinicalSummary.Tables[5].Rows)
                        {
                            Anthropoemetric ObjChronicConditions = new Anthropoemetric();
                            ObjChronicConditions.Date = Convert.ToString(row["Date"]);
                            ObjChronicConditions.Height = Convert.ToString(row["Height"]);
                            ObjChronicConditions.Weight = Convert.ToString(row["Weight"]);
                            ObjChronicConditions.BMIZ = Convert.ToString(row["BMIZ"]);
                            objClinicalSummary.AllAnthropoemetric.Add(ObjChronicConditions);
                        }
                    }
                }
                
            }

            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR,
                    "BClinicalSummary.GetClinicalSummaryData() Method:" + ex.ToString());
            }
            return objClinicalSummary;
        }
        public int SaveUpdateClinicalSummaryData(ClinicalSummary obj, int userID)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BPatientAdherence.SaveUpdateClinicalSummaryData() method called");
            bool flag = true;
            int visitId = 0;
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@location_id", SqlDbType.Int, obj.location_id.ToString());
                ClsUtility.AddParameters("@Form_filling_date", SqlDbType.Int, obj.Form_filling_date);
                ClsUtility.AddParameters("@CliniciansName", SqlDbType.VarChar, obj.CliniciansName.ToString());
                ClsUtility.AddParameters("@Telephone", SqlDbType.VarChar, obj.Telephone.ToString());
                ClsUtility.AddParameters("@Email", SqlDbType.VarChar, obj.Email.ToString());
                ClsUtility.AddParameters("@PrimaryReason", SqlDbType.VarChar, obj.PrimaryReason.ToString());
                ClsUtility.AddParameters("@ClinicalEvaluation", SqlDbType.VarChar, obj.ClinicalEvaluation.ToString());
                ClsUtility.AddParameters("@NumberOfAdherence", SqlDbType.Int, obj.NumberOfAdherence.ToString());
                ClsUtility.AddParameters("@NumberOfHomeVisit", SqlDbType.Int, obj.NumberOfHomeVisit.ToString());
                ClsUtility.AddParameters("@SupportStructures", SqlDbType.VarChar, obj.SupportStructures.ToString());
                ClsUtility.AddParameters("@EvidenceOfAdherence", SqlDbType.VarChar, obj.EvidenceOfAdherence.ToString());
                ClsUtility.AddParameters("@NumberOfDOTS", SqlDbType.Int, obj.NumberOfDOTS.ToString());
                ClsUtility.AddParameters("@PoorAdherence", SqlDbType.VarChar, obj.PoorAdherence.ToString());
                ClsUtility.AddParameters("@EvaluationForOtherText", SqlDbType.VarChar, obj.EvaluationForOtherText.ToString());
                ClsUtility.AddParameters("@EvaluationForOtherVal", SqlDbType.VarChar, obj.EvaluationForOtherVal.ToString());
                ClsUtility.AddParameters("@CommentOnTreatment", SqlDbType.VarChar, obj.CommentOnTreatment.ToString());
                ClsUtility.AddParameters("@HasDrugResistance", SqlDbType.VarChar, obj.HasDrugResistance.ToString());
                ClsUtility.AddParameters("@HasFacilityMultidisciplinary", SqlDbType.VarChar, obj.HasFacilityMultidisciplinary.ToString());
                ClsUtility.AddParameters("@MDTMembers", SqlDbType.VarChar, obj.MDTMembers.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());

                visitId = (int)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_SaveUpdateClinicalSummaryData", ClsDBUtility.ObjectEnum.ExecuteNonQuery);               
                return visitId;
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BPatientAdherence.SaveUpdateClinicalSummaryData() Method:" + ex.ToString());
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
    }
}
