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
    public class BIQTouchKNHExpress : ProcessBase, IQTouchKNHExpress
    {
        public DataTable IQTouchGetKnhExpressData(BIQTouchExpressFields expressFrmFields)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Flag", SqlDbType.Int, ConverTotValue.NullToInt(expressFrmFields.Flag).ToString());
            ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(expressFrmFields.PtnPk).ToString());
            ClsUtility.AddParameters("@LocationId", SqlDbType.Int, ConverTotValue.NullToInt(expressFrmFields.LocationId).ToString());
            ClsUtility.AddParameters("@VisitPk", SqlDbType.Int, ConverTotValue.NullToInt(expressFrmFields.ID).ToString());  // ID here Visit PK

            ClsObject GetRecs = new ClsObject();
            DataTable regDT = (DataTable)GetRecs.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_GetKNHExpress", ClsDBUtility.ObjectEnum.DataTable);
            return regDT;
        }

        public int IQTouchSaveExpressDetails(List<BIQTouchExpressFields> lstobjExpressFields)
        {
            ClsObject expressManagerTest = new ClsObject();
            int theRowAffected = 0;
            int totalRowInserted = 0;
            
         
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                expressManagerTest.Connection = this.Connection;
                expressManagerTest.Transaction = this.Transaction;
                if (lstobjExpressFields.Count > 0)
                {
                    foreach (var objExpressFields in lstobjExpressFields)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.ID).ToString());
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.PtnPk).ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.LocationId).ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.UserId).ToString());
                        ClsUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.ChildAccompaniedByCaregiver).ToString());
                        ClsUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.TreatmentSupporterRelationship).ToString());
                        ClsUtility.AddParameters("@Temperature", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.Temperature).ToString());
                        ClsUtility.AddParameters("@RespirationRate", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.RespirationRate).ToString());
                        ClsUtility.AddParameters("@HeartRate", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.HeartRate).ToString());
                        ClsUtility.AddParameters("@SystolicBloodPressure", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.SystolicBloodPressure).ToString());
                        ClsUtility.AddParameters("@DiastolicBloodPressure", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.DiastolicBloodPressure).ToString());
                        ClsUtility.AddParameters("@MedicalCondition", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.MedicalCondition).ToString());
                        ClsUtility.AddParameters("@SpecificMedicalCondition", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.SpecificMedicalCondition).ToString());
                        ClsUtility.AddParameters("@OnFollowUp", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.OnFollowUp).ToString());
                        if (objExpressFields.LastFollowUpDate.Year.ToString() != "1900")
                        {
                            ClsUtility.AddParameters("@LastFollowUpDate", SqlDbType.DateTime, objExpressFields.LastFollowUpDate.ToString());
                        }
                        ClsUtility.AddParameters("@PreviousAdmission", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.PreviousAdmission).ToString());
                        ClsUtility.AddParameters("@PreviousAdmissionDiagnosis", SqlDbType.VarChar, ConverTotValue.NullToString(objExpressFields.PreviousAdmissionDiagnosis).ToString());

                        if (objExpressFields.PreviousAdmissionStart.Year.ToString() != "1900")
                        {
                            ClsUtility.AddParameters("@PreviousAdmissionStart", SqlDbType.DateTime, objExpressFields.PreviousAdmissionStart.ToString());
                        }
                        if (objExpressFields.PreviousAdmissionEnd.Year.ToString() != "1900")
                        {
                            ClsUtility.AddParameters("@PreviousAdmissionEnd", SqlDbType.DateTime, objExpressFields.PreviousAdmissionEnd.ToString());
                        }
                        ClsUtility.AddParameters("@TBAssessmentICF", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.TBAssessmentIcf).ToString());
                        ClsUtility.AddParameters("@TBFindings", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.TBFindings).ToString());
                        ClsUtility.AddParameters("@RegimenPrescribedFUP", SqlDbType.VarChar, ConverTotValue.NullToString(objExpressFields.RegimenPrescribedFup).ToString());
                        ClsUtility.AddParameters("@LabEvaluationPeads", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.LabEvaluationPeads).ToString());
                        ClsUtility.AddParameters("@SpecifyLabEvaluation", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.SpecifyLabEvaluation).ToString());
                        ClsUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.OIProphylaxis).ToString());
                        ClsUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, ConverTotValue.NullToString(objExpressFields.OtherOIProphylaxis).ToString());
                        ClsUtility.AddParameters("@TreatmentPlan", SqlDbType.VarChar, ConverTotValue.NullToString(objExpressFields.TreatmentPlan).ToString());
                        ClsUtility.AddParameters("@PwPMessagesGiven", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.PwPMessagesGiven).ToString());
                        ClsUtility.AddParameters("@CondomsIssued", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.CondomsIssued).ToString());
                        ClsUtility.AddParameters("@ReasonfornotIssuingCondoms", SqlDbType.VarChar, ConverTotValue.NullToString(objExpressFields.ReasonfornotIssuingCondoms).ToString());
                        ClsUtility.AddParameters("@IntentionOfPregnancy", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.IntentionOfPregnancy).ToString());
                        ClsUtility.AddParameters("@DiscussedDualContraception", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.DiscussedDualContraception).ToString());
                        ClsUtility.AddParameters("@DiscussedFertilityOption", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.DiscussedFertilityOption).ToString());
                        ClsUtility.AddParameters("@OnFP", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.OnFP).ToString());
                        ClsUtility.AddParameters("@FPmethod", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.FPmethod).ToString());
                        ClsUtility.AddParameters("@CervicalCancerScreened", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.CervicalCancerScreened).ToString());
                        ClsUtility.AddParameters("@ReferredForCervicalCancerScreening", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.ReferredForCervicalCancerScreening).ToString());
                        ClsUtility.AddParameters("@CervicalCancerScreeningResults", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.CervicalCancerScreeningResults).ToString());

                        ClsUtility.AddParameters("@RegimenPrescribed", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.RegimenPrescribed).ToString());
                        ClsUtility.AddParameters("@OtherRegimenPrescribed", SqlDbType.VarChar, ConverTotValue.NullToInt(objExpressFields.OtherRegimenPrescribed).ToString());

                        ClsUtility.AddParameters("@ResultsCervicalCancer", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.ResultsCervicalCancer).ToString());
                        ClsUtility.AddParameters("@ReasonCTXpresribed", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.ReasonCTXpresribed).ToString());
                        ClsUtility.AddParameters("@Flag", SqlDbType.VarChar, ConverTotValue.NullToString(objExpressFields.Flag).ToString());
                        if (objExpressFields.VisitDate.Year.ToString() != "1900")
                        {
                            ClsUtility.AddParameters("@VisitDate", SqlDbType.DateTime, objExpressFields.VisitDate.ToString());
                        }
                        ClsUtility.AddParameters("@SignatureID", SqlDbType.Int, ConverTotValue.NullToInt(objExpressFields.Signature).ToString());
                        ClsUtility.AddParameters("@Height", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.Height).ToString());
                        ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, ConverTotValue.NullToInt(objExpressFields.Weight).ToString());

                        theRowAffected = (int)expressManagerTest.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Clinical_AddKNHExpress", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        totalRowInserted = totalRowInserted + theRowAffected;
                    }
                }

               

                
                

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return totalRowInserted;
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
