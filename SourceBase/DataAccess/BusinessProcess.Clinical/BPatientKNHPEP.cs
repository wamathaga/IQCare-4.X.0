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
    public class BPatientKNHPEP : ProcessBase, IPatientKNHPEP
    {
        public DataSet GetDetails()
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_KNH_PEP", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDetailsPaediatric_IE()
        {
            lock (this)
            {
                ClsObject PatientHistory = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)PatientHistory.ReturnObject(ClsUtility.theParams, "pr_clinical_Get_Paediatric_IE", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetKNHPEPDetails(int ptn_pk, int visitpk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_Clinical_get_KNH_PEP_Data", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPaediatric_IE(int ptn_pk, int visitpk)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitpk.ToString());
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_Clinical_get_PaediatricIE", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet SaveUpdateKNHPEPData(Hashtable hashTable, DataTable PreExistingMedicalConditions, DataTable ShortTermEffects, DataTable LongTermEffects, string tabname)
        {
            try
            {
                DataSet theDS;
                int visitID;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                ClsUtility.AddParameters("@visitdate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                ClsUtility.AddParameters("@tabname", SqlDbType.VarChar, tabname);
                if (tabname == "Triage")
                {
                    //ClsUtility.AddParameters("@starttime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                    if (hashTable["LMP"].ToString() != "")
                    {
                        ClsUtility.AddParameters("@LMP", SqlDbType.DateTime, hashTable["LMP"].ToString());
                    }
                    ClsUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, hashTable["ChildAccompaniedByCaregiver"].ToString());
                    ClsUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, hashTable["TreatmentSupporterRelationship"].ToString());
                    ClsUtility.AddParameters("@PatientRefferedOrNot", SqlDbType.Int, hashTable["PatientRefferedOrNot"].ToString());
                    ClsUtility.AddParameters("@YesSpecify", SqlDbType.VarChar, hashTable["YesSpecify"].ToString());
                    ClsUtility.AddParameters("@OtherPreExistingMedicalConditions", SqlDbType.VarChar, hashTable["OtherPreExistingMedicalConditions"].ToString());
                    ClsUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
                    ClsUtility.AddParameters("@TimeToAccessDose", SqlDbType.Int, hashTable["TimeToAccessDose"].ToString());
                    if (hashTable["Temp"].ToString() != "")
                    {
                        ClsUtility.AddParameters("@Temp", SqlDbType.Decimal, hashTable["Temp"].ToString());
                    }
                    if (hashTable["RR"].ToString() != "")
                    {
                        ClsUtility.AddParameters("@RR", SqlDbType.Decimal, hashTable["RR"].ToString());
                    }
                    if (hashTable["HR"].ToString() != "")
                    {
                        ClsUtility.AddParameters("@HR", SqlDbType.Decimal, hashTable["HR"].ToString());
                    }
                    if (hashTable["height"].ToString() != "")
                    {
                        ClsUtility.AddParameters("@height", SqlDbType.Decimal, hashTable["height"].ToString());
                    }
                    if (hashTable["weight"].ToString() != "")
                    {
                        ClsUtility.AddParameters("@weight", SqlDbType.Decimal, hashTable["weight"].ToString());
                    }
                    if (hashTable["BPDiastolic"].ToString() != "")
                    {
                        ClsUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, hashTable["BPDiastolic"].ToString());
                    }
                    if (hashTable["BPSystolic"].ToString() != "")
                    {
                        ClsUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, hashTable["BPSystolic"].ToString());
                    }
                }
                else if (tabname == "Clinical Assessment")
                {
                    ClsUtility.AddParameters("@MedicalHistoryAdditionalNotes", SqlDbType.VarChar, hashTable["MedicalHistoryAdditionalNotes"].ToString());
                    ClsUtility.AddParameters("@OccupationalPEP", SqlDbType.Int, hashTable["OccupationalPEP"].ToString());
                    ClsUtility.AddParameters("@OtherOccupationalPEP", SqlDbType.VarChar, hashTable["OtherOccupationalPEP"].ToString());
                    ClsUtility.AddParameters("@BodyFluidInvolved", SqlDbType.Int, hashTable["BodyFluidInvolved"].ToString());
                    ClsUtility.AddParameters("@OtherBodyFluidInvolved", SqlDbType.VarChar, hashTable["OtherBodyFluidInvolved"].ToString());
                    ClsUtility.AddParameters("@NonOccupational", SqlDbType.Int, hashTable["NonOccupational"].ToString());
                    ClsUtility.AddParameters("@OtherNonOccupationalPEP", SqlDbType.VarChar, hashTable["OtherNonOccupationalPEP"].ToString());
                    ClsUtility.AddParameters("@SexualAssault", SqlDbType.Int, hashTable["SexualAssault"].ToString());
                    ClsUtility.AddParameters("@OtherSexualAssault", SqlDbType.VarChar, hashTable["OtherSexualAssault"].ToString());
                    ClsUtility.AddParameters("@ActionAfterPEP", SqlDbType.Int, hashTable["ActionAfterPEP"].ToString());
                    ClsUtility.AddParameters("@PEPRegimen", SqlDbType.Int, hashTable["PEPRegimen"].ToString());
                    ClsUtility.AddParameters("@OtherPEPRegimen", SqlDbType.VarChar, hashTable["OtherPEPRegimen"].ToString());
                    ClsUtility.AddParameters("@DaysPEPDispensed", SqlDbType.Int, hashTable["DaysPEPDispensed"].ToString());
                    ClsUtility.AddParameters("@PEPDispensedInVisit", SqlDbType.Int, hashTable["PEPDispensedInVisit"].ToString());
                    //ClsUtility.AddParameters("@DrugAllergyToxicities", SqlDbType.Int, hashTable["DrugAllergyToxicities"].ToString());
                    //ClsUtility.AddParameters("@DrugAllergyToxicitySelect", SqlDbType.Int, hashTable["DrugAllergyToxicitySelect"].ToString());
                    //ClsUtility.AddParameters("@OtherDrugAllergyToxicity", SqlDbType.VarChar, hashTable["OtherDrugAllergyToxicity"].ToString());
                    //ClsUtility.AddParameters("@OtherDurgAllergy", SqlDbType.VarChar, hashTable["OtherDurgAllergy"].ToString());
                    ClsUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, hashTable["ARVSideEffects"].ToString());
                    ClsUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, hashTable["OtherLongtermEffects"].ToString());
                    ClsUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, hashTable["OtherShortTermEffects"].ToString());
                    ClsUtility.AddParameters("@MissedDoses", SqlDbType.Int, hashTable["MissedDoses"].ToString());
                    ClsUtility.AddParameters("@VomitedDoses", SqlDbType.Int, hashTable["VomitedDoses"].ToString());
                    ClsUtility.AddParameters("@DelayedDoses", SqlDbType.Int, hashTable["DelayedDoses"].ToString());
                    ClsUtility.AddParameters("@DosesMissedPEP", SqlDbType.Int, hashTable["DosesMissedPEP"].ToString());
                    ClsUtility.AddParameters("@DosesVomited", SqlDbType.Int, hashTable["DosesVomited"].ToString());
                    ClsUtility.AddParameters("@DosesDelayed", SqlDbType.Int, hashTable["DosesDelayed"].ToString());
                    //ClsUtility.AddParameters("@LabEvaluation", SqlDbType.Int, hashTable["LabEvaluation"].ToString());
                    ClsUtility.AddParameters("@LabEvaluationDiagnosticInput", SqlDbType.VarChar, hashTable["LabEvaluationDiagnosticInput"].ToString());
                    ClsUtility.AddParameters("@Elisa", SqlDbType.Int, hashTable["Elisa"].ToString());
                    ClsUtility.AddParameters("@HIVStatusClient", SqlDbType.Int, hashTable["HIVStatusClient"].ToString());
                    ClsUtility.AddParameters("@HepatitisBStatusForClient", SqlDbType.Int, hashTable["HepatitisBStatusForClient"].ToString());
                    ClsUtility.AddParameters("@HepatitisCStatusForClient", SqlDbType.Int, hashTable["HepatitisCStatusForClient"].ToString());
                    ClsUtility.AddParameters("@HIVStatusSource", SqlDbType.Int, hashTable["HIVStatusSource"].ToString());
                    ClsUtility.AddParameters("@HepatitisBStatusSource", SqlDbType.Int, hashTable["HepatitisBStatusSource"].ToString());
                    ClsUtility.AddParameters("@HepatitisCStatusSource", SqlDbType.Int, hashTable["HepatitisCStatusSource"].ToString());
                    ClsUtility.AddParameters("@HBVVaccine", SqlDbType.Int, hashTable["HBVVaccine"].ToString());
                    ClsUtility.AddParameters("@DisclosurePlanDiscussed", SqlDbType.Int, hashTable["DisclosurePlanDiscussed"].ToString());
                    ClsUtility.AddParameters("@SaferSexImportanceExplained", SqlDbType.Int, hashTable["SaferSexImportanceExplained"].ToString());
                    ClsUtility.AddParameters("@AdherenceExplained", SqlDbType.Int, hashTable["AdherenceExplained"].ToString());
                    ClsUtility.AddParameters("@CondomsIssued", SqlDbType.Int, hashTable["CondomsIssued"].ToString());
                    ClsUtility.AddParameters("@ReasonfornotIssuingCondoms", SqlDbType.VarChar, hashTable["ReasonfornotIssuingCondoms"].ToString());


                    ClsUtility.AddParameters("@CurrentPEPregimenstartdate", SqlDbType.DateTime, hashTable["CurrentPEPregimenstartdate"].ToString());

                }
                //ClsUtility.AddParameters("@signature", SqlDbType.Int, hashTable["signature"].ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                ClsUtility.AddParameters("@StartTime", SqlDbType.DateTime, hashTable["starttime"].ToString());

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                // DataSet tempDataSet;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PEP_FORM", ClsDBUtility.ObjectEnum.DataSet);
                visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                //Pre Existing Medical Condition
                if (tabname == "Triage")
                {
                    for (int i = 0; i < PreExistingMedicalConditions.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, PreExistingMedicalConditions.Rows[i]["ID"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "PreExistingMedicalConditions");
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_PreExistingMedicalConditions", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                if (tabname == "Clinical Assessment")
                {
                    //LabEvaluationsSpecify
                    //for (int i = 0; i < LabEvaluationsSpecify.Rows.Count; i++)
                    //{
                    //    ClsUtility.Init_Hashtable();
                    //    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    //    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                    //    ClsUtility.AddParameters("@ID", SqlDbType.Int, LabEvaluationsSpecify.Rows[i]["ID"].ToString());
                    //    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "LabEvaluationsSpecify");
                    //    int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_dtl_LabEvaluationsSpecify", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    //}
                    //ShortTermEffects
                    for (int i = 0; i < ShortTermEffects.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, ShortTermEffects.Rows[i]["ID"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "ShortTermEffects");
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_dtl_ShortTermEffects", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    //LongTermEffects
                    for (int i = 0; i < LongTermEffects.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, LongTermEffects.Rows[i]["ID"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "LongTermEffects");
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_dtl_LongTermEffects", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }






                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return theDS;
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

        public DataSet getVisitIdByPatient(int patient_Id)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientId", SqlDbType.Int, patient_Id.ToString());
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "getVisitIdByPatient", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet SaveUpdatePaediatricIE_TriageTab(Hashtable hashTable, DataTable tblMultiselect)
        {
            try
            {
                DataSet theDS;
                int visitID;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                ClsUtility.AddParameters("@visitdate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                ClsUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["startTime"].ToString());
                ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, hashTable["userID"].ToString());
                ClsUtility.AddParameters("@ChildAccompaniedBy", SqlDbType.VarChar, hashTable["ChildAccompaniedBy"].ToString());
                ClsUtility.AddParameters("@ChildDiagnosisConfirmed", SqlDbType.Int, hashTable["ChildDiagnosisConfirmed"].ToString());
                ClsUtility.AddParameters("@PrimaryCareGiver", SqlDbType.VarChar, hashTable["PrimaryCareGiver"].ToString());
                if (!String.IsNullOrEmpty(hashTable["ConfirmHIVPosDate"].ToString()))
                {
                    ClsUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.VarChar, hashTable["ConfirmHIVPosDate"].ToString());
                }
                ClsUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, hashTable["DisclosureStatus"].ToString());

                if (!String.IsNullOrEmpty(hashTable["FatherAlive"].ToString()))
                {
                    ClsUtility.AddParameters("@FatherAlive", SqlDbType.Bit, hashTable["FatherAlive"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["ChildReferred"].ToString()))
                {
                    ClsUtility.AddParameters("@ChildReferred", SqlDbType.Int, hashTable["ChildReferred"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["CurrentlyOnHAART"].ToString()))
                {
                    ClsUtility.AddParameters("@CurrentlyOnHAART", SqlDbType.Int, hashTable["CurrentlyOnHAART"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["CurrentlyOnCTX"].ToString()))
                {
                    ClsUtility.AddParameters("@CurrentlyOnCTX", SqlDbType.Int, hashTable["CurrentlyOnCTX"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["MotherAlive"].ToString()))
                {
                    ClsUtility.AddParameters("@MotherAlive", SqlDbType.Bit, hashTable["MotherAlive"].ToString());
                }
                ClsUtility.AddParameters("@SchoolingStatus", SqlDbType.Int, hashTable["SchoolingStatus"].ToString());
                if (!String.IsNullOrEmpty(hashTable["HealthEducation"].ToString()))
                {
                    ClsUtility.AddParameters("@HealthEducation", SqlDbType.Int, hashTable["HealthEducation"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["HIVSupportGroup"].ToString()))
                {
                    ClsUtility.AddParameters("@HIVSupportGroup", SqlDbType.Int, hashTable["HIVSupportGroup"].ToString());
                }
                ClsUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, hashTable["HIVSupportGroupMembership"].ToString());
                ClsUtility.AddParameters("@CurrentARTRegimenLine", SqlDbType.Int, hashTable["CurrentARTRegimenLine"].ToString());
                ClsUtility.AddParameters("@CurrentARTRegimen", SqlDbType.Int, hashTable["CurrentARTRegimen"].ToString());
                if (!String.IsNullOrEmpty(hashTable["CurrentARTRegimenDate"].ToString()))
                {
                    ClsUtility.AddParameters("@CurrentARTRegimenDate", SqlDbType.VarChar, hashTable["CurrentARTRegimenDate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["DateOfDeathMother"].ToString()))
                {
                    ClsUtility.AddParameters("@DateOfDeathMother", SqlDbType.VarChar, hashTable["DateOfDeathMother"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["DateOfDeathFather"].ToString()))
                {
                    ClsUtility.AddParameters("@DateOfDeathFather", SqlDbType.VarChar, hashTable["DateOfDeathFather"].ToString());
                }
                ClsUtility.AddParameters("@ChildReferredFrom", SqlDbType.VarChar, hashTable["ChildReferredFrom"].ToString());
                ClsUtility.AddParameters("@ReasonNotDisclosed", SqlDbType.VarChar, hashTable["ReasonNotDisclosed"].ToString());
                ClsUtility.AddParameters("@OtherDisclosureReason", SqlDbType.VarChar, hashTable["OtherDisclosureReason"].ToString());
                ClsUtility.AddParameters("@HighestLevelAttained", SqlDbType.Int, hashTable["HighestLevelAttained"].ToString());
                //vital sign
                if (!String.IsNullOrEmpty(hashTable["Temperature"].ToString()))
                {
                    ClsUtility.AddParameters("@Temperature", SqlDbType.Decimal, hashTable["Temperature"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["RespirationRate"].ToString()))
                {
                    ClsUtility.AddParameters("@RespirationRate", SqlDbType.Decimal, hashTable["RespirationRate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["HeartRate"].ToString()))
                {
                    ClsUtility.AddParameters("@HeartRate", SqlDbType.Decimal, hashTable["HeartRate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["Height"].ToString()))
                {
                    ClsUtility.AddParameters("@Height", SqlDbType.Decimal, hashTable["Height"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["Weight"].ToString()))
                {
                    ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, hashTable["Weight"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["DiastolicBloodPressure"].ToString()))
                {
                    ClsUtility.AddParameters("@DiastolicBloodPressure", SqlDbType.Decimal, hashTable["DiastolicBloodPressure"].ToString());
                }
                if (hashTable["SystolicBloodPressure"].ToString() != "")
                {
                    ClsUtility.AddParameters("@SystolicBloodPressure", SqlDbType.Decimal, hashTable["SystolicBloodPressure"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["BMI"].ToString()))
                {
                    ClsUtility.AddParameters("@BMI", SqlDbType.Decimal, hashTable["BMI"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["HeadCircumference"].ToString()))
                {
                    ClsUtility.AddParameters("@HeadCircumference", SqlDbType.Decimal, hashTable["HeadCircumference"].ToString());
                }
                if (hashTable["WeightForAge"].ToString() != "")
                    ClsUtility.AddParameters("@WeightForAge", SqlDbType.Int, hashTable["WeightForAge"].ToString());
                if (hashTable["WeightForHeight"].ToString() != "")
                    ClsUtility.AddParameters("@WeightForHeight", SqlDbType.Int, hashTable["WeightForHeight"].ToString());
                if (hashTable["BMIz"].ToString() != "")
                    ClsUtility.AddParameters("@BMIz", SqlDbType.Int, hashTable["BMIz"].ToString());
                ClsUtility.AddParameters("@NursesComments", SqlDbType.VarChar, hashTable["NursesComments"].ToString());
                ClsUtility.AddParameters("@PatientReferredOtherSpecialistClinic", SqlDbType.VarChar, hashTable["PatientReferredOtherSpecialistClinic"].ToString());
                ClsUtility.AddParameters("@PatientReferredOtherSpecify", SqlDbType.VarChar, hashTable["PatientReferredOtherSpecify"].ToString());

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;

                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_Paediatric_Initial_Evaluation_Form_TriageTab", ClsDBUtility.ObjectEnum.DataSet);
                if (theDS.Tables[0].Rows.Count > 0)
                    visitID = Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit_Id"]);
                else
                    visitID = Convert.ToInt32(hashTable["visitID"].ToString());


                /*
                * Delete code for multi select 
                * By: Gaurav
                */
                if (tblMultiselect.Rows.Count > 0)
                {
                    if (tblMultiselect.Rows[0]["FieldName"].ToString() != "")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[0]["FieldName"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "[pr_Clinical_Delete_Multiselect_Paediatric_IE]", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //Pre Existing 
                for (int i = 0; i < tblMultiselect.Rows.Count; i++)
                {
                    if (tblMultiselect.Rows[i]["FieldName"].ToString() != "")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, tblMultiselect.Rows[i]["ID"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[i]["FieldName"].ToString());
                        if (tblMultiselect.Rows[i]["DateField1"].ToString() != "")
                        {
                            ClsUtility.AddParameters("@datefield1", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField1"].ToString());
                        }
                        if (tblMultiselect.Rows[i]["DateField2"].ToString() != "")
                        {
                            ClsUtility.AddParameters("@datefield2", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField2"].ToString());
                        }
                        ClsUtility.AddParameters("@NumericField", SqlDbType.Int, tblMultiselect.Rows[i]["NumericField"].ToString());
                        ClsUtility.AddParameters("@other", SqlDbType.VarChar, tblMultiselect.Rows[i]["Other_Notes"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_Paediatric_IE", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return theDS;
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

        private void DeleteMultipleSelect(int Ptn_pk, int Visit_Pk, string FieldName)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, Visit_Pk.ToString());
                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, FieldName.ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_Paediatric_IE", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
            }
            catch
            {
            }
        }


        public DataSet SaveUpdatePaediatricIE_ClinicalHistoryTab(Hashtable hashTable, DataTable tblMultiselect)
        {
            try
            {
                DataSet theDS;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                ClsUtility.AddParameters("@visitdate", SqlDbType.VarChar, hashTable["visitDate"].ToString());
                ClsUtility.AddParameters("@StartTime", SqlDbType.VarChar, hashTable["startTime"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, hashTable["userID"].ToString());
                //------Presenting Complaints
                ClsUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
                ClsUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, hashTable["SchoolPerfomance"].ToString());
                ClsUtility.AddParameters("@OtherPresentingComplaints", SqlDbType.VarChar, hashTable["OtherPresentingComplaints"].ToString());
                //---Medical history (Disease, diagnosis and treatment)
                ClsUtility.AddParameters("@MedicalHistory", SqlDbType.Int, hashTable["MedicalHistory"].ToString());
                ClsUtility.AddParameters("@OtherMedicalHistorySpecify", SqlDbType.VarChar, hashTable["OtherMedicalHistorySpecify"].ToString());
                if (!String.IsNullOrEmpty(hashTable["PreviousAdmission"].ToString()))
                {
                    ClsUtility.AddParameters("@PreviousAdmission", SqlDbType.Int, hashTable["PreviousAdmission"].ToString());
                }
                ClsUtility.AddParameters("@PreviousAdmissionDiagnosis", SqlDbType.VarChar, hashTable["PreviousAdmissionDiagnosis"].ToString());
                if (!String.IsNullOrEmpty(hashTable["PreviousAdmissionStart"].ToString()))
                {
                    ClsUtility.AddParameters("@PreviousAdmissionStart", SqlDbType.VarChar, hashTable["PreviousAdmissionStart"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["PreviousAdmissionEnd"].ToString()))
                {
                    ClsUtility.AddParameters("@PreviousAdmissionEnd", SqlDbType.VarChar, hashTable["PreviousAdmissionEnd"].ToString());
                }
                ClsUtility.AddParameters("@OtherChronicCondition", SqlDbType.VarChar, hashTable["OtherChronicCondition"].ToString());
                //------TB History
                if (!String.IsNullOrEmpty(hashTable["TBHistory"].ToString()))
                {
                    ClsUtility.AddParameters("@TBHistory", SqlDbType.Int, hashTable["TBHistory"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["TBrxCompleteDate"].ToString()))
                {
                    ClsUtility.AddParameters("@TBrxCompleteDate", SqlDbType.VarChar, hashTable["TBrxCompleteDate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["TBRetreatmentDate"].ToString()))
                {
                    ClsUtility.AddParameters("@TBRetreatmentDate", SqlDbType.VarChar, hashTable["TBRetreatmentDate"].ToString());
                }
                //--------Immunisation Status
                ClsUtility.AddParameters("@ImmunisationStatus", SqlDbType.Int, hashTable["ImmunisationStatus"].ToString());

                if (!String.IsNullOrEmpty(hashTable["PMTCT1StartDate"].ToString()))
                {
                    ClsUtility.AddParameters("@PMTCT1StartDate", SqlDbType.VarChar, hashTable["PMTCT1StartDate"].ToString());
                }
                ClsUtility.AddParameters("@PMTCT1Regimen", SqlDbType.VarChar, hashTable["PMTCT1Regimen"].ToString());
                ClsUtility.AddParameters("@PEP1Regimen", SqlDbType.VarChar, hashTable["PEP1Regimen"].ToString());
                if (hashTable["PEP1StartDate"].ToString() != "")
                {
                    ClsUtility.AddParameters("@PEP1StartDate", SqlDbType.VarChar, hashTable["PEP1StartDate"].ToString());
                }
                ClsUtility.AddParameters("@HAART1Regimen", SqlDbType.VarChar, hashTable["HAART1Regimen"].ToString());
                if (!String.IsNullOrEmpty(hashTable["HAART1StartDate"].ToString()))
                {
                    ClsUtility.AddParameters("@HAART1StartDate", SqlDbType.VarChar, hashTable["HAART1StartDate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["InitialCD4"].ToString()))
                {
                    ClsUtility.AddParameters("@InitialCD4", SqlDbType.Decimal, hashTable["InitialCD4"].ToString());
                }
                if (hashTable["InitialCD4Date"].ToString() != "")
                {
                    ClsUtility.AddParameters("@InitialCD4Date", SqlDbType.VarChar, hashTable["InitialCD4Date"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["HighestCD4Ever"].ToString()))
                {
                    ClsUtility.AddParameters("@HighestCD4Ever", SqlDbType.Decimal, hashTable["HighestCD4Ever"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["HighestCD4EverDate"].ToString()))
                {
                    ClsUtility.AddParameters("@HighestCD4EverDate", SqlDbType.VarChar, hashTable["HighestCD4EverDate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["CD4atARTInitiation"].ToString()))
                {
                    ClsUtility.AddParameters("@CD4atARTInitiation", SqlDbType.Decimal, hashTable["CD4atARTInitiation"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["CD4atARTInitiationDate"].ToString()))
                {
                    ClsUtility.AddParameters("@CD4atARTInitiationDate", SqlDbType.VarChar, hashTable["CD4atARTInitiationDate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["MostRecentCD4"].ToString()))
                {
                    ClsUtility.AddParameters("@MostRecentCD4", SqlDbType.Decimal, hashTable["MostRecentCD4"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["MostRecentCD4Date"].ToString()))
                {
                    ClsUtility.AddParameters("@MostRecentCD4Date", SqlDbType.VarChar, hashTable["MostRecentCD4Date"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["PreviousViralLoad"].ToString()))
                {
                    ClsUtility.AddParameters("@PreviousViralLoad", SqlDbType.Decimal, hashTable["PreviousViralLoad"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["PreviousViralLoadDate"].ToString()))
                {
                    ClsUtility.AddParameters("@PreviousViralLoadDate", SqlDbType.VarChar, hashTable["PreviousViralLoadDate"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["InitialCD4Percent"].ToString()))
                {
                    ClsUtility.AddParameters("@InitialCD4Percent", SqlDbType.Decimal, hashTable["InitialCD4Percent"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["HighestCD4Percent"].ToString()))
                {
                    ClsUtility.AddParameters("@HighestCD4Percent", SqlDbType.Decimal, hashTable["HighestCD4Percent"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["CD4AtARTInitiationPercent"].ToString()))
                {
                    ClsUtility.AddParameters("@CD4AtARTInitiationPercent", SqlDbType.Decimal, hashTable["CD4AtARTInitiationPercent"].ToString());
                }
                if (!String.IsNullOrEmpty(hashTable["MostRecentCD4Percent"].ToString()))
                {
                    ClsUtility.AddParameters("@MostRecentCD4Percent", SqlDbType.Decimal, hashTable["MostRecentCD4Percent"].ToString());
                }
                ClsUtility.AddParameters("@OtherHIVRelatedHistory", SqlDbType.VarChar, hashTable["OtherHIVRelatedHistory"].ToString());

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_Paediatric_Initial_Evaluation_Form_ClinicalHistoryTab", ClsDBUtility.ObjectEnum.DataSet);

                /*
                 * Delete code for multi select 
                 * By: Gaurav
                 */
                if (tblMultiselect.Rows.Count > 0)
                {
                    if (tblMultiselect.Rows[0]["FieldName"].ToString() != "")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[0]["FieldName"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "[pr_Clinical_Delete_Multiselect_Paediatric_IE]", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }


                for (int i = 0; i < tblMultiselect.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(tblMultiselect.Rows[i]["FieldName"].ToString()))
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, tblMultiselect.Rows[i]["ID"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[i]["FieldName"].ToString());
                        if (tblMultiselect.Rows[i]["DateField1"].ToString() != "")
                        {
                            ClsUtility.AddParameters("@datefield1", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField1"].ToString());
                        }
                        if (tblMultiselect.Rows[i]["DateField2"].ToString() != "")
                        {
                            ClsUtility.AddParameters("@datefield2", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField2"].ToString());
                        }
                        ClsUtility.AddParameters("@NumericField", SqlDbType.Int, tblMultiselect.Rows[i]["NumericField"].ToString());
                        ClsUtility.AddParameters("@other", SqlDbType.VarChar, tblMultiselect.Rows[i]["Other_Notes"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_Paediatric_IE", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDS;
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

        public DataSet SaveUpdatePaediatricIE_ExaminationTab(Hashtable hashTable, DataTable tblMultiselect)
        {
            try
            {
                DataSet theDS;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                ClsUtility.AddParameters("@visitdate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                ClsUtility.AddParameters("@startTime", SqlDbType.VarChar, hashTable["startTime"].ToString());
                ClsUtility.AddParameters("@userID", SqlDbType.Int, hashTable["userID"].ToString());
                if (hashTable["OtherCurrentLongTermMedications"].ToString() != "")
                {
                    ClsUtility.AddParameters("@OtherCurrentLongTermMedications", SqlDbType.DateTime, hashTable["OtherCurrentLongTermMedications"].ToString());
                }
                //-------------------Physical Exam
                ClsUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());
                ClsUtility.AddParameters("@LabEvaluationDiagnosticInput", SqlDbType.VarChar, hashTable["LabEvaluationDiagnosticInput"].ToString());
                ClsUtility.AddParameters("@HAARTImpression", SqlDbType.Int, hashTable["HAARTImpression"].ToString());
                ClsUtility.AddParameters("@Diagnosis", SqlDbType.Int, hashTable["Diagnosis"].ToString());
                ClsUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, hashTable["OtherGeneralConditions"].ToString());
                ClsUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, hashTable["OtherAbdomenConditions"].ToString());
                ClsUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, hashTable["OtherCardiovascularConditions"].ToString());
                ClsUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, hashTable["OtherOralCavityConditions"].ToString());
                ClsUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, hashTable["OtherGenitourinaryConditions"].ToString());
                ClsUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, hashTable["OtherCNSConditions"].ToString());
                ClsUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, hashTable["OtherChestLungsConditions"].ToString());
                ClsUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, hashTable["OtherSkinConditions"].ToString());
                ClsUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, hashTable["HIVRelatedOI"].ToString());
                ClsUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.VarChar, hashTable["NonHIVRelatedOI"].ToString());
                ClsUtility.AddParameters("@HAARTexperienced", SqlDbType.VarChar, hashTable["HAARTexperienced"].ToString());
                ClsUtility.AddParameters("@OtherHAARTImpression", SqlDbType.VarChar, hashTable["OtherHAARTImpression"].ToString());
                //-----------------Developmental milestones
                if (!String.IsNullOrEmpty(hashTable["MilestoneAppropriate"].ToString()))
                {
                    ClsUtility.AddParameters("@MilestoneAppropriate", SqlDbType.Int, hashTable["MilestoneAppropriate"].ToString());
                }
                ClsUtility.AddParameters("@ResonMilestoneInappropriate", SqlDbType.VarChar, hashTable["ResonMilestoneInappropriate"].ToString());
                //----------------Tests and labs
                ClsUtility.AddParameters("@LabEvaluationPeads", SqlDbType.Int, hashTable["LabEvaluationPeads"].ToString());
                //--------Staging at initial evaluation
                //ClsUtility.AddParameters("@InitiationWHOstage", SqlDbType.Int, hashTable["InitiationWHOstage"].ToString());
                //ClsUtility.AddParameters("@HIVAssociatedConditionsPeads", SqlDbType.Int, hashTable["HIVAssociatedConditionsPeads"].ToString());
                ClsUtility.AddParameters("@PeadiatricNutritionAssessment", SqlDbType.Int, hashTable["PeadiatricNutritionAssessment"].ToString());
                ClsUtility.AddParameters("@WABStage", SqlDbType.Int, hashTable["WABStage"].ToString());
                ClsUtility.AddParameters("@TannerStaging", SqlDbType.Int, hashTable["TannerStaging"].ToString());
                if (hashTable["Menarche"].ToString() != "NULL")
                {
                    ClsUtility.AddParameters("@Menarche", SqlDbType.Int, hashTable["Menarche"].ToString());
                }
                if (hashTable["MenarcheDate"].ToString() != "")
                {
                    ClsUtility.AddParameters("@MenarcheDate", SqlDbType.DateTime, hashTable["MenarcheDate"].ToString());
                }

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_Paediatric_Initial_Evaluation_Form_ExaminationTab", ClsDBUtility.ObjectEnum.DataSet);

                /*
                * Delete code for multi select 
                * By: Gaurav
                */
                if (tblMultiselect.Rows.Count > 0)
                {
                    if (tblMultiselect.Rows[0]["FieldName"].ToString() != "")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[0]["FieldName"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "[pr_Clinical_Delete_Multiselect_Paediatric_IE]", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                for (int i = 0; i < tblMultiselect.Rows.Count; i++)
                {
                    if (tblMultiselect.Rows[i]["FieldName"].ToString() != "")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, tblMultiselect.Rows[i]["ID"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[i]["FieldName"].ToString());
                        if (tblMultiselect.Rows[i]["DateField1"].ToString() != "")
                        {
                            ClsUtility.AddParameters("@datefield1", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField1"].ToString());
                        }
                        if (tblMultiselect.Rows[i]["DateField2"].ToString() != "")
                        {
                            ClsUtility.AddParameters("@datefield2", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField2"].ToString());
                        }
                        ClsUtility.AddParameters("@NumericField", SqlDbType.Int, tblMultiselect.Rows[i]["NumericField"].ToString());
                        ClsUtility.AddParameters("@other", SqlDbType.VarChar, tblMultiselect.Rows[i]["Other_Notes"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_Paediatric_IE", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDS;
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

        public DataSet SaveUpdatePaediatricIE_ManagementTab(Hashtable hashTable, DataTable tblMultiselect)
        {
            try
            {
                DataSet theDS;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                //-----------------Drug Allergies Toxicities
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                ClsUtility.AddParameters("@visitdate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                ClsUtility.AddParameters("@startTime", SqlDbType.VarChar, hashTable["startTime"].ToString());
                ClsUtility.AddParameters("@userID", SqlDbType.Int, hashTable["userID"].ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                //-----------------Treatment
                ClsUtility.AddParameters("@WorkUpPlan", SqlDbType.VarChar, hashTable["WorkUpPlan"].ToString());
                ClsUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, hashTable["OIProphylaxis"].ToString());
                ClsUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, hashTable["OtherTreatment"].ToString());
                ClsUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, hashTable["OtherOIProphylaxis"].ToString());
                ClsUtility.AddParameters("@ReasonFluconazolepresribed", SqlDbType.Int, hashTable["ReasonFluconazolepresribed"].ToString());
                ClsUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, hashTable["OtherLongtermEffects"].ToString());
                ClsUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, hashTable["OtherShortTermEffects"].ToString());
                ClsUtility.AddParameters("@treatmentPlan", SqlDbType.Int, hashTable["treatmentPlan"].ToString());
                ClsUtility.AddParameters("@Noofdrugssubstituted", SqlDbType.Int, hashTable["Noofdrugssubstituted"].ToString());
                ClsUtility.AddParameters("@reasonforswitchto2ndlineregimen", SqlDbType.Int, hashTable["reasonforswitchto2ndlineregimen"].ToString());
                ClsUtility.AddParameters("@specifyOtherEligibility", SqlDbType.VarChar, hashTable["specifyOtherEligibility"].ToString());
                ClsUtility.AddParameters("@specifyotherARTchangereason", SqlDbType.VarChar, hashTable["specifyotherARTchangereason"].ToString());
                ClsUtility.AddParameters("@specifyOtherStopCode", SqlDbType.VarChar, hashTable["specifyOtherStopCode"].ToString());
                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;
                VisitManager.Transaction = this.Transaction;
                theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_Paediatric_Initial_Evaluation_Form_ManagementTab", ClsDBUtility.ObjectEnum.DataSet);

                /*
                * Delete code for multi select 
                * By: Gaurav
                */
                if (tblMultiselect.Rows.Count > 0)
                {
                    if (tblMultiselect.Rows[0]["FieldName"].ToString() != "")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[0]["FieldName"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "[pr_Clinical_Delete_Multiselect_Paediatric_IE]", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                for (int i = 0; i < tblMultiselect.Rows.Count; i++)
                {
                    if (tblMultiselect.Rows[i]["FieldName"].ToString() != "")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, tblMultiselect.Rows[i]["ID"].ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[i]["FieldName"].ToString());
                        if (tblMultiselect.Rows[i]["DateField1"].ToString() != "")
                        {
                            ClsUtility.AddParameters("@datefield1", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField1"].ToString());
                        }
                        if (tblMultiselect.Rows[i]["DateField2"].ToString() != "")
                        {
                            ClsUtility.AddParameters("@datefield2", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField2"].ToString());
                        }
                        ClsUtility.AddParameters("@NumericField", SqlDbType.Int, tblMultiselect.Rows[i]["NumericField"].ToString());
                        ClsUtility.AddParameters("@other", SqlDbType.VarChar, tblMultiselect.Rows[i]["Other_Notes"].ToString());
                        int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_Paediatric_IE", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDS;
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
        public DataSet SaveUpdateKNHPEPTriage(Hashtable hashTable, DataTable PreExistingMedicalConditions, DataTable referredTo)
        {
            try
            {
                DataSet theDS;
                int visitID;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["userID"].ToString());
                ClsUtility.AddParameters("@visitdate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
                if (hashTable["LMP"].ToString() != "")
                {
                    ClsUtility.AddParameters("@LMP", SqlDbType.DateTime, hashTable["LMP"].ToString());
                }
                if (hashTable["ChildAccompaniedByCaregiver"].ToString() != "")
                    ClsUtility.AddParameters("@ChildAccompaniedByCaregiver", SqlDbType.Int, hashTable["ChildAccompaniedByCaregiver"].ToString());
                ClsUtility.AddParameters("@TreatmentSupporterRelationship", SqlDbType.Int, hashTable["TreatmentSupporterRelationship"].ToString());
                if (hashTable["PatientRefferedOrNot"].ToString() != "")
                    ClsUtility.AddParameters("@PatientRefferedOrNot", SqlDbType.Int, hashTable["PatientRefferedOrNot"].ToString());
                ClsUtility.AddParameters("@YesSpecify", SqlDbType.VarChar, hashTable["YesSpecify"].ToString());
                ClsUtility.AddParameters("@OtherPreExistingMedicalConditions", SqlDbType.VarChar, hashTable["OtherPreExistingMedicalConditions"].ToString());
                ClsUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
                ClsUtility.AddParameters("@TimeToAccessDose", SqlDbType.Int, hashTable["TimeToAccessDose"].ToString());
                if (hashTable["Temp"].ToString() != "")
                    ClsUtility.AddParameters("@Temp", SqlDbType.Decimal, hashTable["Temp"].ToString());
                if (hashTable["RR"].ToString() != "")
                    ClsUtility.AddParameters("@RR", SqlDbType.Decimal, hashTable["RR"].ToString());
                if (hashTable["HR"].ToString() != "")
                    ClsUtility.AddParameters("@HR", SqlDbType.Decimal, hashTable["HR"].ToString());
                if (hashTable["height"].ToString() != "")
                    ClsUtility.AddParameters("@Height", SqlDbType.Decimal, hashTable["height"].ToString());
                if (hashTable["weight"].ToString() != "")
                    ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, hashTable["weight"].ToString());
                if (hashTable["BPDiastolic"].ToString() != "")
                    ClsUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, hashTable["BPDiastolic"].ToString());
                if (hashTable["BPSystolic"].ToString() != "")
                    ClsUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, hashTable["BPSystolic"].ToString());

                ClsUtility.AddParameters("@StartTime", SqlDbType.DateTime, hashTable["starttime"].ToString());
                ClsUtility.AddParameters("@NurseComments", SqlDbType.VarChar, hashTable["NurseComments"].ToString());
                ClsUtility.AddParameters("@SpecialistClinicReferral", SqlDbType.VarChar, hashTable["SpecialistClinicReferral"].ToString());
                ClsUtility.AddParameters("@OtherReferral", SqlDbType.VarChar, hashTable["OtherReferral"].ToString());
                ClsUtility.AddParameters("@HeadCircumference", SqlDbType.Decimal, hashTable["HeadCircumference"].ToString());
                if (!string.IsNullOrEmpty(hashTable["WeightForAge"].ToString()))
                    ClsUtility.AddParameters("@WeightForAge", SqlDbType.Int, hashTable["WeightForAge"].ToString());
                if (!string.IsNullOrEmpty(hashTable["WeightForHeight"].ToString()))
                    ClsUtility.AddParameters("@WeightForHeight", SqlDbType.Int, hashTable["WeightForHeight"].ToString());

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                theDS = (DataSet)VisitManager.ReturnObjectNewImpl(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PEP_Traige", ClsDBUtility.ObjectEnum.DataSet);
                if (hashTable["visitID"].ToString() == "0")
                {
                    visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];
                }
                else
                {
                    visitID = Convert.ToInt32(hashTable["visitID"].ToString());
                }

                for (int i = 0; i < PreExistingMedicalConditions.Rows.Count; i++)
                {
                    //ClsUtility.Init_Hashtable();
                    //ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    //ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                    //ClsUtility.AddParameters("@ID", SqlDbType.Int, PreExistingMedicalConditions.Rows[i]["ID"].ToString());
                    //ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "PreExistingMedicalConditions");
                    //int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_PreExistingMedicalConditions", ClsDBUtility.ObjectEnum.ExecuteNonQuery);


                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                    ClsUtility.AddParameters("@ValueID", SqlDbType.Int, PreExistingMedicalConditions.Rows[i]["value"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["userID"].ToString());
                    ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "PreExistingMedicalConditions");
                    int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                for (int i = 0; i < referredTo.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                    ClsUtility.AddParameters("@ValueID", SqlDbType.Int, referredTo.Rows[i]["value"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["userID"].ToString());
                    ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "RefferedToFUpF");
                    int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return theDS;
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

        public DataSet SaveUpdateKNHPEPCA(Hashtable hashTable, DataTable ShortTermEffects, DataTable LongTermEffects)
        {
            try
            {
                DataSet theDS;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["userID"].ToString());
                //ClsUtility.AddParameters("@starttime", SqlDbType.VarChar, hashTable["starttime"].ToString());
                ClsUtility.AddParameters("@MedicalHistoryAdditionalNotes", SqlDbType.VarChar, hashTable["MedicalHistoryAdditionalNotes"].ToString());
                ClsUtility.AddParameters("@OccupationalPEP", SqlDbType.Int, hashTable["OccupationalPEP"].ToString());
                ClsUtility.AddParameters("@OtherOccupationalPEP", SqlDbType.VarChar, hashTable["OtherOccupationalPEP"].ToString());
                ClsUtility.AddParameters("@BodyFluidInvolved", SqlDbType.Int, hashTable["BodyFluidInvolved"].ToString());
                ClsUtility.AddParameters("@OtherBodyFluidInvolved", SqlDbType.VarChar, hashTable["OtherBodyFluidInvolved"].ToString());
                ClsUtility.AddParameters("@NonOccupational", SqlDbType.Int, hashTable["NonOccupational"].ToString());
                ClsUtility.AddParameters("@OtherNonOccupationalPEP", SqlDbType.VarChar, hashTable["OtherNonOccupationalPEP"].ToString());
                ClsUtility.AddParameters("@SexualAssault", SqlDbType.Int, hashTable["SexualAssault"].ToString());
                ClsUtility.AddParameters("@OtherSexualAssault", SqlDbType.VarChar, hashTable["OtherSexualAssault"].ToString());
                ClsUtility.AddParameters("@ActionAfterPEP", SqlDbType.Int, hashTable["ActionAfterPEP"].ToString());
                ClsUtility.AddParameters("@PEPRegimen", SqlDbType.Int, hashTable["PEPRegimen"].ToString());
                ClsUtility.AddParameters("@OtherPEPRegimen", SqlDbType.VarChar, hashTable["OtherPEPRegimen"].ToString());
                ClsUtility.AddParameters("@DaysPEPDispensed", SqlDbType.Int, hashTable["DaysPEPDispensed"].ToString());
                ClsUtility.AddParameters("@PEPDispensedInVisit", SqlDbType.Int, hashTable["PEPDispensedInVisit"].ToString());
                ClsUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, hashTable["ARVSideEffects"].ToString());
                ClsUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, hashTable["OtherLongtermEffects"].ToString());
                ClsUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, hashTable["OtherShortTermEffects"].ToString());
                if (hashTable["MissedDoses"].ToString() != "")
                    ClsUtility.AddParameters("@MissedDoses", SqlDbType.Int, hashTable["MissedDoses"].ToString());
                if (hashTable["VomitedDoses"].ToString() != "")
                    ClsUtility.AddParameters("@VomitedDoses", SqlDbType.Int, hashTable["VomitedDoses"].ToString());
                if (hashTable["DelayedDoses"].ToString() != "")
                    ClsUtility.AddParameters("@DelayedDoses", SqlDbType.Int, hashTable["DelayedDoses"].ToString());
                ClsUtility.AddParameters("@DosesMissedPEP", SqlDbType.Int, hashTable["DosesMissedPEP"].ToString());
                ClsUtility.AddParameters("@DosesVomited", SqlDbType.Int, hashTable["DosesVomited"].ToString());
                ClsUtility.AddParameters("@DosesDelayed", SqlDbType.Int, hashTable["DosesDelayed"].ToString());
                ClsUtility.AddParameters("@LabEvaluationDiagnosticInput", SqlDbType.VarChar, hashTable["LabEvaluationDiagnosticInput"].ToString());
                ClsUtility.AddParameters("@Elisa", SqlDbType.Int, hashTable["Elisa"].ToString());
                ClsUtility.AddParameters("@HIVStatusClient", SqlDbType.Int, hashTable["HIVStatusClient"].ToString());
                ClsUtility.AddParameters("@HepatitisBStatusForClient", SqlDbType.Int, hashTable["HepatitisBStatusForClient"].ToString());
                ClsUtility.AddParameters("@HepatitisCStatusForClient", SqlDbType.Int, hashTable["HepatitisCStatusForClient"].ToString());
                ClsUtility.AddParameters("@HIVStatusSource", SqlDbType.Int, hashTable["HIVStatusSource"].ToString());
                ClsUtility.AddParameters("@HepatitisBStatusSource", SqlDbType.Int, hashTable["HepatitisBStatusSource"].ToString());
                ClsUtility.AddParameters("@HepatitisCStatusSource", SqlDbType.Int, hashTable["HepatitisCStatusSource"].ToString());
                if (hashTable["HBVVaccine"].ToString() != "")
                    ClsUtility.AddParameters("@HBVVaccine", SqlDbType.Int, hashTable["HBVVaccine"].ToString());
                if (hashTable["DisclosurePlanDiscussed"].ToString() != "")
                    ClsUtility.AddParameters("@DisclosurePlanDiscussed", SqlDbType.Int, hashTable["DisclosurePlanDiscussed"].ToString());
                if (hashTable["SaferSexImportanceExplained"].ToString() != "")
                    ClsUtility.AddParameters("@SaferSexImportanceExplained", SqlDbType.Int, hashTable["SaferSexImportanceExplained"].ToString());
                if (hashTable["AdherenceExplained"].ToString() != "")
                    ClsUtility.AddParameters("@AdherenceExplained", SqlDbType.Int, hashTable["AdherenceExplained"].ToString());
                if (hashTable["CondomsIssued"].ToString() != "")
                    ClsUtility.AddParameters("@CondomsIssued", SqlDbType.Int, hashTable["CondomsIssued"].ToString());
                ClsUtility.AddParameters("@ReasonfornotIssuingCondoms", SqlDbType.VarChar, hashTable["ReasonfornotIssuingCondoms"].ToString());


                ClsUtility.AddParameters("@CurrentPEPregimenstartdate", SqlDbType.DateTime, hashTable["CurrentPEPregimenstartdate"].ToString());
                ClsUtility.AddParameters("@CurrentPEPregimenEnddate", SqlDbType.DateTime, hashTable["CurrentPEPregimenEnddate"].ToString());
                ClsUtility.AddParameters("@StartTime", SqlDbType.DateTime, hashTable["starttime"].ToString());

                ClsObject VisitManager = new ClsObject();
                VisitManager.Connection = this.Connection;

                VisitManager.Transaction = this.Transaction;

                theDS = (DataSet)VisitManager.ReturnObjectNewImpl(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_PEP_CA", ClsDBUtility.ObjectEnum.DataSet);
                //visitID = (int)theDS.Tables[0].Rows[0]["Visit_Id"];

                for (int i = 0; i < ShortTermEffects.Rows.Count; i++)
                {
                    //ClsUtility.Init_Hashtable();
                    //ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    //ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                    //ClsUtility.AddParameters("@ID", SqlDbType.Int, ShortTermEffects.Rows[i]["ID"].ToString());
                    //ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "ShortTermEffects");
                    //int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_dtl_ShortTermEffects", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                    ClsUtility.AddParameters("@ValueID", SqlDbType.Int, ShortTermEffects.Rows[i]["value"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["userID"].ToString());
                    ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "ShortTermEffects");
                    int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                //LongTermEffects
                for (int i = 0; i < LongTermEffects.Rows.Count; i++)
                {
                    //ClsUtility.Init_Hashtable();
                    //ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    //ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                    //ClsUtility.AddParameters("@ID", SqlDbType.Int, LongTermEffects.Rows[i]["ID"].ToString());
                    //ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "LongTermEffects");
                    //int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_dtl_LongTermEffects", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
                    ClsUtility.AddParameters("@ValueID", SqlDbType.Int, LongTermEffects.Rows[i]["value"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["userID"].ToString());
                    ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "LongTermEffects");
                    int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                return theDS;
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
        public DataTable GetSignature(int featureId, int visit_pk)
        {

            lock (this)
            {
                DataTable theDT;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@featureId", SqlDbType.Int, featureId.ToString());
                ClsUtility.AddParameters("@visitPk", SqlDbType.Int, visit_pk.ToString());
                theDT = (DataTable)ClsObj.ReturnObject(ClsUtility.theParams, "pr_PaediatricIE_Get_KNH_Signature", ClsDBUtility.ObjectEnum.DataTable);
                return theDT;
            }
        }
        public DataTable GetTabID(int featureId)
        {
            lock (this)
            {
                DataTable theDT;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@featureId", SqlDbType.Int, featureId.ToString());
                theDT = (DataTable)ClsObj.ReturnObject(ClsUtility.theParams, "pr_PaediatricIE_Get_KNH_TabId", ClsDBUtility.ObjectEnum.DataTable);
                return theDT;
            }
        }

        //private void insertMultiSelectValues(DataTable tblMultiselect,string patientId, string visitId)
        //{
        //    ClsObject VisitManager = new ClsObject();
        //    VisitManager.Connection = this.Connection;
        //    VisitManager.Transaction = this.Transaction;
        //    //Pre Existing Medical Condition
        //    for (int i = 0; i < tblMultiselect.Rows.Count; i++)
        //    {
        //        if (tblMultiselect.Rows[i]["FieldName"].ToString() != "")
        //        {
        //            ClsUtility.Init_Hashtable();
        //            ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, patientId);
        //            ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitId);
        //            ClsUtility.AddParameters("@ID", SqlDbType.Int, tblMultiselect.Rows[i]["ID"].ToString());
        //            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[i]["FieldName"].ToString());
        //            if (tblMultiselect.Rows[i]["DateField1"].ToString() != "")
        //            {
        //                ClsUtility.AddParameters("@datefield1", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField1"].ToString());
        //            }
        //            if (tblMultiselect.Rows[i]["DateField2"].ToString() != "")
        //            {
        //                ClsUtility.AddParameters("@datefield2", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField2"].ToString());
        //            }
        //            ClsUtility.AddParameters("@NumericField", SqlDbType.Int, tblMultiselect.Rows[i]["NumericField"].ToString());
        //            ClsUtility.AddParameters("@other", SqlDbType.VarChar, tblMultiselect.Rows[i]["Other_Notes"].ToString());
        //            int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_Paediatric_IE", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
        //        }
        //    }
        //}
    }
}


//+++++++++++++++++++++++++++++++++++++
//public DataSet SaveUpdatePaediatric_IE(Hashtable hashTable, DataTable tblMultiselect, string tabname)
//{
//    try
//    {
//        DataSet theDS;
//        int visitID;
//        this.Connection = DataMgr.GetConnection();
//        this.Transaction = DataMgr.BeginTransaction(this.Connection);
//        ClsUtility.Init_Hashtable();
//        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
//        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, hashTable["visitID"].ToString());
//        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, hashTable["locationID"].ToString());
//        ClsUtility.AddParameters("@visitdate", SqlDbType.DateTime, hashTable["visitDate"].ToString());
//        ClsUtility.AddParameters("@signature", SqlDbType.Int, hashTable["signature"].ToString());
//        ClsUtility.AddParameters("@DataQlty", SqlDbType.Int, hashTable["qltyFlag"].ToString());
//        ClsUtility.AddParameters("@tabname", SqlDbType.VarChar, tabname);
//        if (tabname == "Triage")
//        {
//            //section client information

//            ClsUtility.AddParameters("@ChildAccompaniedBy", SqlDbType.VarChar, hashTable["ChildAccompaniedBy"].ToString());
//            ClsUtility.AddParameters("@ChildDiagnosisConfirmed", SqlDbType.Int, hashTable["ChildDiagnosisConfirmed"].ToString());
//            ClsUtility.AddParameters("@PrimaryCareGiver", SqlDbType.VarChar, hashTable["PrimaryCareGiver"].ToString());
//            if (hashTable["ConfirmHIVPosDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@ConfirmHIVPosDate", SqlDbType.DateTime, hashTable["ConfirmHIVPosDate"].ToString());
//            }
//            ClsUtility.AddParameters("@DisclosureStatus", SqlDbType.Int, hashTable["DisclosureStatus"].ToString());
//            ClsUtility.AddParameters("@FatherAlive2", SqlDbType.Int, hashTable["FatherAlive2"].ToString());
//            ClsUtility.AddParameters("@ChildReferred", SqlDbType.Int, hashTable["ChildReferred"].ToString());
//            ClsUtility.AddParameters("@CurrentlyOnHAART", SqlDbType.Int, hashTable["CurrentlyOnHAART"].ToString());
//            ClsUtility.AddParameters("@CurrentlyOnCTX", SqlDbType.Int, hashTable["CurrentlyOnCTX"].ToString());
//            ClsUtility.AddParameters("@MotherAlive2", SqlDbType.Int, hashTable["MotherAlive2"].ToString());
//            ClsUtility.AddParameters("@SchoolingStatus", SqlDbType.Int, hashTable["SchoolingStatus"].ToString());
//            ClsUtility.AddParameters("@HealthEducation", SqlDbType.Int, hashTable["HealthEducation"].ToString());
//            ClsUtility.AddParameters("@HIVSupportGroup", SqlDbType.Int, hashTable["HIVSupportGroup"].ToString());
//            ClsUtility.AddParameters("@HIVSupportGroupMembership", SqlDbType.VarChar, hashTable["HIVSupportGroupMembership"].ToString());
//            ClsUtility.AddParameters("@CurrentARTRegimenLine", SqlDbType.Int, hashTable["CurrentARTRegimenLine"].ToString());
//            ClsUtility.AddParameters("@CurrentARTRegimen", SqlDbType.Int, hashTable["CurrentARTRegimen"].ToString());
//            if (hashTable["CurrentARTRegimenDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@CurrentARTRegimenDate", SqlDbType.DateTime, hashTable["CurrentARTRegimenDate"].ToString());
//            }
//            if (hashTable["DateOfDeathMother"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@DateOfDeathMother", SqlDbType.DateTime, hashTable["DateOfDeathMother"].ToString());
//            }
//            if (hashTable["DateOfDeathFather"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@DateOfDeathFather", SqlDbType.DateTime, hashTable["DateOfDeathFather"].ToString());
//            }
//            ClsUtility.AddParameters("@ChildReferredFrom", SqlDbType.VarChar, hashTable["ChildReferredFrom"].ToString());
//            ClsUtility.AddParameters("@ReasonNotDisclosed", SqlDbType.VarChar, hashTable["ReasonNotDisclosed"].ToString());
//            ClsUtility.AddParameters("@OtherDisclosureReason", SqlDbType.VarChar, hashTable["OtherDisclosureReason"].ToString());
//            ClsUtility.AddParameters("@HighestLevelAttained", SqlDbType.Int, hashTable["HighestLevelAttained"].ToString());
//            //vital sign
//            if (hashTable["Temperature"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@Temperature", SqlDbType.Decimal, hashTable["Temperature"].ToString());
//            }
//            if (hashTable["RespirationRate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@RespirationRate", SqlDbType.Decimal, hashTable["RespirationRate"].ToString());
//            }
//            if (hashTable["HeartRate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@HeartRate", SqlDbType.Decimal, hashTable["HeartRate"].ToString());
//            }
//            if (hashTable["Height"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@Height", SqlDbType.Decimal, hashTable["Height"].ToString());
//            }
//            if (hashTable["Weight"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, hashTable["Weight"].ToString());
//            }
//            if (hashTable["DiastolicBloodPressure"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@DiastolicBloodPressure", SqlDbType.Decimal, hashTable["DiastolicBloodPressure"].ToString());
//            }
//            if (hashTable["SystolicBloodPressure"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@SystolicBloodPressure", SqlDbType.Decimal, hashTable["SystolicBloodPressure"].ToString());
//            }
//            if (hashTable["BMI"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@BMI", SqlDbType.Decimal, hashTable["BMI"].ToString());
//            }
//            if (hashTable["HeadCircumference"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@HeadCircumference", SqlDbType.Decimal, hashTable["HeadCircumference"].ToString());
//            }

//            ClsUtility.AddParameters("@WeightForAge", SqlDbType.Int, hashTable["WeightForAge"].ToString());
//            ClsUtility.AddParameters("@WeightForHeight", SqlDbType.Int, hashTable["WeightForHeight"].ToString());
//            ClsUtility.AddParameters("@NursesComments", SqlDbType.VarChar, hashTable["NursesComments"].ToString());
//        }
//        if (tabname == "Clinical History")
//        {
//            //------Presenting Complaints
//            ClsUtility.AddParameters("@PresentingComplaintsAdditionalNotes", SqlDbType.VarChar, hashTable["PresentingComplaintsAdditionalNotes"].ToString());
//            ClsUtility.AddParameters("@SchoolPerfomance", SqlDbType.Int, hashTable["SchoolPerfomance"].ToString());
//            ClsUtility.AddParameters("@OtherPresentingComplaints", SqlDbType.VarChar, hashTable["OtherPresentingComplaints"].ToString());
//            //---Medical history (Disease, diagnosis and treatment)
//            ClsUtility.AddParameters("@MedicalHistory", SqlDbType.Int, hashTable["MedicalHistory"].ToString());
//            ClsUtility.AddParameters("@OtherMedicalHistorySpecify", SqlDbType.VarChar, hashTable["OtherMedicalHistorySpecify"].ToString());
//            ClsUtility.AddParameters("@PreviousAdmission", SqlDbType.Int, hashTable["PreviousAdmission"].ToString());
//            ClsUtility.AddParameters("@PreviousAdmissionDiagnosis", SqlDbType.VarChar, hashTable["PreviousAdmissionDiagnosis"].ToString());
//            if (hashTable["PreviousAdmissionStart"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@PreviousAdmissionStart", SqlDbType.DateTime, hashTable["PreviousAdmissionStart"].ToString());
//            }
//            if (hashTable["PreviousAdmissionEnd"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@PreviousAdmissionEnd", SqlDbType.DateTime, hashTable["PreviousAdmissionEnd"].ToString());
//            }
//            ClsUtility.AddParameters("@OtherChronicCondition", SqlDbType.VarChar, hashTable["OtherChronicCondition"].ToString());
//            //------TB History
//            ClsUtility.AddParameters("@TBHistory", SqlDbType.Int, hashTable["TBHistory"].ToString());
//            if (hashTable["TBrxCompleteDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@TBrxCompleteDate", SqlDbType.DateTime, hashTable["TBrxCompleteDate"].ToString());
//            }
//            if (hashTable["TBRetreatmentDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@TBRetreatmentDate", SqlDbType.DateTime, hashTable["TBRetreatmentDate"].ToString());
//            }
//            //--------Immunisation Status
//            ClsUtility.AddParameters("@ImmunisationStatus", SqlDbType.Int, hashTable["ImmunisationStatus"].ToString());
//            //-----------ARV history
//            //Update By - Nidhi Bisht
//            //Update Date- 8 May,2014
//            //Desc- Not required
//            //ClsUtility.AddParameters("@ARVExposure", SqlDbType.Int, hashTable["ARVExposure"].ToString());
//            //ClsUtility.AddParameters("@HIVRelatedHistory", SqlDbType.Int, hashTable["HIVRelatedHistory"].ToString());
//            if (hashTable["PMTCT1StartDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@PMTCT1StartDate", SqlDbType.DateTime, hashTable["PMTCT1StartDate"].ToString());
//            }
//            ClsUtility.AddParameters("@PMTCT1Regimen", SqlDbType.VarChar, hashTable["PMTCT1Regimen"].ToString());
//            ClsUtility.AddParameters("@PEP1Regimen", SqlDbType.VarChar, hashTable["PEP1Regimen"].ToString());
//            if (hashTable["PEP1StartDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@PEP1StartDate", SqlDbType.DateTime, hashTable["PEP1StartDate"].ToString());
//            }
//            ClsUtility.AddParameters("@HAART1Regimen", SqlDbType.VarChar, hashTable["HAART1Regimen"].ToString());
//            if (hashTable["HAART1StartDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@HAART1StartDate", SqlDbType.DateTime, hashTable["HAART1StartDate"].ToString());
//            }
//            if (hashTable["InitialCD4"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@InitialCD4", SqlDbType.Decimal, hashTable["InitialCD4"].ToString());
//            }
//            if (hashTable["InitialCD4Date"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@InitialCD4Date", SqlDbType.DateTime, hashTable["InitialCD4Date"].ToString());
//            }
//            if (hashTable["HighestCD4Ever"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@HighestCD4Ever", SqlDbType.Decimal, hashTable["HighestCD4Ever"].ToString());
//            }
//            if (hashTable["HighestCD4EverDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@HighestCD4EverDate", SqlDbType.DateTime, hashTable["HighestCD4EverDate"].ToString());
//            }
//            if (hashTable["CD4atARTInitiation"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@CD4atARTInitiation", SqlDbType.Decimal, hashTable["CD4atARTInitiation"].ToString());
//            }
//            if (hashTable["CD4atARTInitiationDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@CD4atARTInitiationDate", SqlDbType.DateTime, hashTable["CD4atARTInitiationDate"].ToString());
//            }
//            if (hashTable["MostRecentCD4"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@MostRecentCD4", SqlDbType.Decimal, hashTable["MostRecentCD4"].ToString());
//            }
//            if (hashTable["MostRecentCD4Date"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@MostRecentCD4Date", SqlDbType.DateTime, hashTable["MostRecentCD4Date"].ToString());
//            }
//            if (hashTable["PreviousViralLoad"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@PreviousViralLoad", SqlDbType.Decimal, hashTable["PreviousViralLoad"].ToString());
//            }
//            if (hashTable["PreviousViralLoadDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@PreviousViralLoadDate", SqlDbType.DateTime, hashTable["PreviousViralLoadDate"].ToString());
//            }
//            if (hashTable["InitialCD4Percent"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@InitialCD4Percent", SqlDbType.Decimal, hashTable["InitialCD4Percent"].ToString());
//            }
//            if (hashTable["HighestCD4Percent"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@HighestCD4Percent", SqlDbType.Decimal, hashTable["HighestCD4Percent"].ToString());
//            }
//            if (hashTable["CD4AtARTInitiationPercent"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@CD4AtARTInitiationPercent", SqlDbType.Decimal, hashTable["CD4AtARTInitiationPercent"].ToString());
//            }
//            if (hashTable["MostRecentCD4Percent"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@MostRecentCD4Percent", SqlDbType.Decimal, hashTable["MostRecentCD4Percent"].ToString());
//            }
//            ClsUtility.AddParameters("@OtherHIVRelatedHistory", SqlDbType.VarChar, hashTable["OtherHIVRelatedHistory"].ToString());
//        }
//        if (tabname == "TB Screening")
//        {

//            //TB Screening ICF(2 signs & 2 symptoms - TB likely)
//            ClsUtility.AddParameters("@TBAssessed", SqlDbType.Int, hashTable["TBAssessed"].ToString());
//            ClsUtility.AddParameters("@SputumSmear", SqlDbType.Int, hashTable["SputumSmear"].ToString());
//            ClsUtility.AddParameters("@ChestXRay", SqlDbType.Int, hashTable["ChestXRay"].ToString());
//            ClsUtility.AddParameters("@TissueBiopsy", SqlDbType.Int, hashTable["TissueBiopsy"].ToString());
//            ClsUtility.AddParameters("@CXR", SqlDbType.Int, hashTable["CXR"].ToString());
//            ClsUtility.AddParameters("@TBFindings", SqlDbType.Int, hashTable["TBFindings"].ToString());
//            ClsUtility.AddParameters("@TissueBiopsyResults", SqlDbType.VarChar, hashTable["TissueBiopsyResults"].ToString());
//            ClsUtility.AddParameters("@TissueBiopsyTest", SqlDbType.Int, hashTable["TissueBiopsyTest"].ToString());
//            //--------------TB Evaluation and Treatment Plan
//            ClsUtility.AddParameters("@TBTypePeads", SqlDbType.Int, hashTable["TBTypePeads"].ToString());
//            ClsUtility.AddParameters("@PeadsTBPatientType", SqlDbType.Int, hashTable["PeadsTBPatientType"].ToString());
//            ClsUtility.AddParameters("@TBPlan", SqlDbType.Int, hashTable["TBPlan"].ToString());
//            ClsUtility.AddParameters("@TBRegimen", SqlDbType.Int, hashTable["TBRegimen"].ToString());
//            if (hashTable["TBRegimenStartDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@TBRegimenStartDate", SqlDbType.DateTime, hashTable["TBRegimenStartDate"].ToString());
//            }
//            if (hashTable["TBRegimenEndDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@TBRegimenEndDate", SqlDbType.DateTime, hashTable["TBRegimenEndDate"].ToString());
//            }
//            ClsUtility.AddParameters("@TBTreatmentOutcomesPeads", SqlDbType.Int, hashTable["TBTreatmentOutcomesPeads"].ToString());
//            ClsUtility.AddParameters("@OtherTBRegimen", SqlDbType.VarChar, hashTable["OtherTBRegimen"].ToString());
//            ClsUtility.AddParameters("@OtherTBPlan", SqlDbType.VarChar, hashTable["OtherTBPlan"].ToString());
//            //----------IPT (Patients with no signs and symptoms)
//            ClsUtility.AddParameters("@NoTB", SqlDbType.Int, hashTable["NoTB"].ToString());
//            ClsUtility.AddParameters("@TBAdherenceAssessed", SqlDbType.Int, hashTable["TBAdherenceAssessed"].ToString());
//            ClsUtility.AddParameters("@MissedTBdoses", SqlDbType.Int, hashTable["MissedTBdoses"].ToString());
//            if (hashTable["INHStartDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@INHStartDate", SqlDbType.DateTime, hashTable["INHStartDate"].ToString());
//            }
//            if (hashTable["INHEndDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@INHEndDate", SqlDbType.DateTime, hashTable["INHEndDate"].ToString());
//            }
//            if (hashTable["PyridoxineStartDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@PyridoxineStartDate", SqlDbType.DateTime, hashTable["PyridoxineStartDate"].ToString());
//            }
//            if (hashTable["PyridoxineEndDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@PyridoxineEndDate", SqlDbType.DateTime, hashTable["PyridoxineEndDate"].ToString());
//            }
//            ClsUtility.AddParameters("@OtherTBsideEffects", SqlDbType.VarChar, hashTable["OtherTBsideEffects"].ToString());
//            ClsUtility.AddParameters("@ReferredForAdherence", SqlDbType.Int, hashTable["ReferredForAdherence"].ToString());
//            ClsUtility.AddParameters("@ReminderIPT", SqlDbType.Int, hashTable["ReminderIPT"].ToString());
//            //-------Confirmed or TB suspected
//            ClsUtility.AddParameters("@SuspectTB", SqlDbType.Int, hashTable["SuspectTB"].ToString());
//            ClsUtility.AddParameters("@ContactsScreenedForTB", SqlDbType.Int, hashTable["ContactsScreenedForTB"].ToString());
//            ClsUtility.AddParameters("@TBnotScreenedSpecify", SqlDbType.VarChar, hashTable["TBnotScreenedSpecify"].ToString());
//            if (hashTable["StopINHDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@StopINHDate", SqlDbType.DateTime, hashTable["StopINHDate"].ToString());
//            }
//        }
//        if (tabname == "Examination")
//        {

//            //----------Long term medications
//            ClsUtility.AddParameters("@LongTermMedications", SqlDbType.Int, hashTable["LongTermMedications"].ToString());
//            if (hashTable["SulfaTMPDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@SulfaTMPDate", SqlDbType.DateTime, hashTable["SulfaTMPDate"].ToString());
//            }
//            if (hashTable["AntifungalsDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@AntifungalsDate", SqlDbType.DateTime, hashTable["AntifungalsDate"].ToString());
//            }
//            if (hashTable["AnticonvulsantsDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@AnticonvulsantsDate", SqlDbType.DateTime, hashTable["AnticonvulsantsDate"].ToString());
//            }
//            ClsUtility.AddParameters("@OtherLongTermMedications", SqlDbType.VarChar, hashTable["OtherLongTermMedications"].ToString());
//            if (hashTable["OtherCurrentLongTermMedications"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@OtherCurrentLongTermMedications", SqlDbType.DateTime, hashTable["OtherCurrentLongTermMedications"].ToString());
//            }
//            //-------------------Physical Exam
//            ClsUtility.AddParameters("@OtherMedicalConditionNotes", SqlDbType.VarChar, hashTable["OtherMedicalConditionNotes"].ToString());
//            ClsUtility.AddParameters("@LabEvaluationDiagnosticInput", SqlDbType.VarChar, hashTable["LabEvaluationDiagnosticInput"].ToString());
//            ClsUtility.AddParameters("@HAARTImpression", SqlDbType.Int, hashTable["HAARTImpression"].ToString());
//            ClsUtility.AddParameters("@Diagnosis", SqlDbType.Int, hashTable["Diagnosis"].ToString());
//            ClsUtility.AddParameters("@OtherGeneralConditions", SqlDbType.VarChar, hashTable["OtherGeneralConditions"].ToString());
//            ClsUtility.AddParameters("@OtherAbdomenConditions", SqlDbType.VarChar, hashTable["OtherAbdomenConditions"].ToString());
//            ClsUtility.AddParameters("@OtherCardiovascularConditions", SqlDbType.VarChar, hashTable["OtherCardiovascularConditions"].ToString());
//            ClsUtility.AddParameters("@OtherOralCavityConditions", SqlDbType.VarChar, hashTable["OtherOralCavityConditions"].ToString());
//            ClsUtility.AddParameters("@OtherGenitourinaryConditions", SqlDbType.VarChar, hashTable["OtherGenitourinaryConditions"].ToString());
//            ClsUtility.AddParameters("@OtherCNSConditions", SqlDbType.VarChar, hashTable["OtherCNSConditions"].ToString());
//            ClsUtility.AddParameters("@OtherChestLungsConditions", SqlDbType.VarChar, hashTable["OtherChestLungsConditions"].ToString());
//            ClsUtility.AddParameters("@OtherSkinConditions", SqlDbType.VarChar, hashTable["OtherSkinConditions"].ToString());
//            ClsUtility.AddParameters("@HIVRelatedOI", SqlDbType.VarChar, hashTable["HIVRelatedOI"].ToString());
//            ClsUtility.AddParameters("@NonHIVRelatedOI", SqlDbType.VarChar, hashTable["NonHIVRelatedOI"].ToString());
//            ClsUtility.AddParameters("@HAARTexperienced", SqlDbType.VarChar, hashTable["HAARTexperienced"].ToString());
//            ClsUtility.AddParameters("@OtherHAARTImpression", SqlDbType.VarChar, hashTable["OtherHAARTImpression"].ToString());
//            //-----------------Developmental milestones
//            ClsUtility.AddParameters("@MilestoneAppropriate", SqlDbType.Int, hashTable["MilestoneAppropriate"].ToString());
//            ClsUtility.AddParameters("@ResonMilestoneInappropriate", SqlDbType.VarChar, hashTable["ResonMilestoneInappropriate"].ToString());
//            //----------------Tests and labs
//            ClsUtility.AddParameters("@LabEvaluationPeads", SqlDbType.Int, hashTable["LabEvaluationPeads"].ToString());
//            //--------Staging at initial evaluation
//            ClsUtility.AddParameters("@InitiationWHOstage", SqlDbType.Int, hashTable["InitiationWHOstage"].ToString());
//            ClsUtility.AddParameters("@HIVAssociatedConditionsPeads", SqlDbType.Int, hashTable["HIVAssociatedConditionsPeads"].ToString());
//            ClsUtility.AddParameters("@PeadiatricNutritionAssessment", SqlDbType.Int, hashTable["PeadiatricNutritionAssessment"].ToString());
//            ClsUtility.AddParameters("@WABStage", SqlDbType.Int, hashTable["WABStage"].ToString());
//            ClsUtility.AddParameters("@TannerStaging", SqlDbType.Int, hashTable["TannerStaging"].ToString());
//            ClsUtility.AddParameters("@Menarche", SqlDbType.Int, hashTable["Menarche"].ToString());
//            if (hashTable["MenarcheDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@MenarcheDate", SqlDbType.DateTime, hashTable["MenarcheDate"].ToString());
//            }
//        }
//        if (tabname == "Management")
//        {

//            //-----------------Drug Allergies Toxicities
//            ClsUtility.AddParameters("@SpecifyARVallergy", SqlDbType.VarChar, hashTable["SpecifyARVallergy"].ToString());
//            ClsUtility.AddParameters("@OtherDrugAllergy", SqlDbType.VarChar, hashTable["OtherDrugAllergy"].ToString());
//            ClsUtility.AddParameters("@SpecifyAntibioticAllery", SqlDbType.VarChar, hashTable["SpecifyAntibioticAllery"].ToString());
//            //-----------------Treatment
//            //ClsUtility.AddParameters("@ARVSideEffects", SqlDbType.Int, hashTable["ARVSideEffects"].ToString());
//            ClsUtility.AddParameters("@WorkUpPlan", SqlDbType.VarChar, hashTable["WorkUpPlan"].ToString());
//            // ClsUtility.AddParameters("@ARTtreatmentPlanPeads", SqlDbType.Int, hashTable["ARTtreatmentPlanPeads"].ToString());
//            //ClsUtility.AddParameters("@StartART", SqlDbType.Int, hashTable["StartART"].ToString());
//            //ClsUtility.AddParameters("@SubstituteRegimen", SqlDbType.Int, hashTable["SubstituteRegimen"].ToString());
//            //ClsUtility.AddParameters("@StopTreatment", SqlDbType.Int, hashTable["StopTreatment"].ToString());
//            // ClsUtility.AddParameters("@RegimenPrescribed", SqlDbType.Int, hashTable["RegimenPrescribed"].ToString());
//            ClsUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, hashTable["OIProphylaxis"].ToString());
//            ClsUtility.AddParameters("@OtherTreatment", SqlDbType.VarChar, hashTable["OtherTreatment"].ToString());
//            ClsUtility.AddParameters("@OtherOIProphylaxis", SqlDbType.VarChar, hashTable["OtherOIProphylaxis"].ToString());
//            //ClsUtility.AddParameters("@OtherRegimenPrescribed", SqlDbType.VarChar, hashTable["OtherRegimenPrescribed"].ToString());
//            //ClsUtility.AddParameters("@NumberDrugsSubstituted", SqlDbType.Int, hashTable["NumberDrugsSubstituted"].ToString());
//            //ClsUtility.AddParameters("@ReasonCTXpresribed", SqlDbType.Int, hashTable["ReasonCTXpresribed"].ToString());
//            ClsUtility.AddParameters("@ReasonFluconazolepresribed", SqlDbType.Int, hashTable["ReasonFluconazolepresribed"].ToString());
//            ClsUtility.AddParameters("@OtherLongtermEffects", SqlDbType.VarChar, hashTable["OtherLongtermEffects"].ToString());
//            ClsUtility.AddParameters("@OtherShortTermEffects", SqlDbType.VarChar, hashTable["OtherShortTermEffects"].ToString());
//        }
//        if (tabname == "PrevWith")
//        {

//            //-----------------------Sexuality Assesment
//            ClsUtility.AddParameters("@SexualActiveness", SqlDbType.Int, hashTable["SexualActiveness"].ToString());
//            ClsUtility.AddParameters("@ChildHIVStatusDisclosed", SqlDbType.Int, hashTable["ChildHIVStatusDisclosed"].ToString());
//            ClsUtility.AddParameters("@PartnerHIVStatus", SqlDbType.Int, hashTable["PartnerHIVStatus"].ToString());
//            ClsUtility.AddParameters("@LMPassessmentValid", SqlDbType.Int, hashTable["LMPassessmentValid"].ToString());
//            ClsUtility.AddParameters("@PDTdone", SqlDbType.Int, hashTable["PDTdone"].ToString());
//            ClsUtility.AddParameters("@PMTCToffered", SqlDbType.Int, hashTable["PMTCToffered"].ToString());
//            if (hashTable["EDD"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@EDD", SqlDbType.DateTime, hashTable["EDD"].ToString());
//            }
//            if (hashTable["LMP"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@LMP", SqlDbType.DateTime, hashTable["LMP"].ToString());
//            }
//            ClsUtility.AddParameters("@LMPNotaccessedReason", SqlDbType.Int, hashTable["LMPNotaccessedReason"].ToString());
//            ClsUtility.AddParameters("@SexualOrientation", SqlDbType.Int, hashTable["SexualOrientation"].ToString());
//            //---------PWP Interventions
//            ClsUtility.AddParameters("@GivenPWPMessages", SqlDbType.Int, hashTable["GivenPWPMessages"].ToString());
//            ClsUtility.AddParameters("@SaferSexImportanceExplained", SqlDbType.Int, hashTable["SaferSexImportanceExplained"].ToString());
//            ClsUtility.AddParameters("@CondomsIssued", SqlDbType.Int, hashTable["CondomsIssued"].ToString());
//            ClsUtility.AddParameters("@IntentionOfPregnancy", SqlDbType.Int, hashTable["IntentionOfPregnancy"].ToString());
//            ClsUtility.AddParameters("@OnFP", SqlDbType.Int, hashTable["OnFP"].ToString());
//            ClsUtility.AddParameters("@CervicalCancerScreened", SqlDbType.Int, hashTable["CervicalCancerScreened"].ToString());
//            ClsUtility.AddParameters("@HPVOffered", SqlDbType.Int, hashTable["HPVOffered"].ToString());
//            ClsUtility.AddParameters("@TreatmentPlan", SqlDbType.VarChar, hashTable["TreatmentPlan"].ToString());
//            ClsUtility.AddParameters("@Counselling", SqlDbType.Int, hashTable["Counselling"].ToString());
//            ClsUtility.AddParameters("@HPVvaccine", SqlDbType.Int, hashTable["HPVvaccine"].ToString());
//            ClsUtility.AddParameters("@ContactTracing", SqlDbType.Int, hashTable["ContactTracing"].ToString());
//            ClsUtility.AddParameters("@TransitionPreparation", SqlDbType.Int, hashTable["TransitionPreparation"].ToString());
//            ClsUtility.AddParameters("@STIscreenedPeads", SqlDbType.Int, hashTable["STIscreenedPeads"].ToString());
//            ClsUtility.AddParameters("@WardAdmission", SqlDbType.Int, hashTable["WardAdmission"].ToString());
//            ClsUtility.AddParameters("@ReferToSpecialistClinic", SqlDbType.VarChar, hashTable["ReferToSpecialistClinic"].ToString());
//            ClsUtility.AddParameters("@TransferOut", SqlDbType.VarChar, hashTable["TransferOut"].ToString());

//            ClsUtility.AddParameters("@OtherCounselling", SqlDbType.VarChar, hashTable["OtherCounselling"].ToString());
//            ClsUtility.AddParameters("@ReasonfornotIssuingCondoms", SqlDbType.VarChar, hashTable["ReasonfornotIssuingCondoms"].ToString());
//            ClsUtility.AddParameters("@DiscussedDualContraception", SqlDbType.Int, hashTable["DiscussedDualContraception"].ToString());
//            ClsUtility.AddParameters("@DiscussedFertilityOption", SqlDbType.Int, hashTable["DiscussedFertilityOption"].ToString());
//            ClsUtility.AddParameters("@FPmethod", SqlDbType.Int, hashTable["FPmethod"].ToString());
//            ClsUtility.AddParameters("@ReferredForCervicalCancerScreening", SqlDbType.Int, hashTable["ReferredForCervicalCancerScreening"].ToString());
//            ClsUtility.AddParameters("@CervicalCancerScreeningResults", SqlDbType.Int, hashTable["CervicalCancerScreeningResults"].ToString());
//            ClsUtility.AddParameters("@UrethralDischarge", SqlDbType.Int, hashTable["UrethralDischarge"].ToString());
//            ClsUtility.AddParameters("@VaginalDischarge", SqlDbType.Int, hashTable["VaginalDischarge"].ToString());
//            ClsUtility.AddParameters("@GenitalUlceration", SqlDbType.Int, hashTable["GenitalUlceration"].ToString());
//            ClsUtility.AddParameters("@STItreatmentPlan", SqlDbType.VarChar, hashTable["STItreatmentPlan"].ToString());
//            if (hashTable["HPVDoseDate"].ToString() != "")
//            {
//                ClsUtility.AddParameters("@HPVDoseDate", SqlDbType.DateTime, hashTable["HPVDoseDate"].ToString());
//            }
//            ClsUtility.AddParameters("@OfferedHPVaccine", SqlDbType.Int, hashTable["OfferedHPVaccine"].ToString());

//        }

//        ClsObject VisitManager = new ClsObject();
//        VisitManager.Connection = this.Connection;
//        VisitManager.Transaction = this.Transaction;

//        // DataSet tempDataSet;
//        theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_Paediatric_Initial_Evaluation_Form", ClsDBUtility.ObjectEnum.DataSet);
//        visitID = Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit_Id"]);

//        //Pre Existing Medical Condition
//        for (int i = 0; i < tblMultiselect.Rows.Count; i++)
//        {
//            if (tblMultiselect.Rows[i]["FieldName"].ToString() != "")
//            {
//                ClsUtility.Init_Hashtable();
//                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, hashTable["patientID"].ToString());
//                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
//                ClsUtility.AddParameters("@ID", SqlDbType.Int, tblMultiselect.Rows[i]["ID"].ToString());
//                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, tblMultiselect.Rows[i]["FieldName"].ToString());
//                if (tblMultiselect.Rows[i]["DateField1"].ToString() != "")
//                {
//                    ClsUtility.AddParameters("@datefield1", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField1"].ToString());
//                }
//                if (tblMultiselect.Rows[i]["DateField2"].ToString() != "")
//                {
//                    ClsUtility.AddParameters("@datefield2", SqlDbType.DateTime, tblMultiselect.Rows[i]["DateField2"].ToString());
//                }
//                ClsUtility.AddParameters("@NumericField", SqlDbType.Int, tblMultiselect.Rows[i]["NumericField"].ToString());
//                ClsUtility.AddParameters("@other", SqlDbType.VarChar, tblMultiselect.Rows[i]["Other_Notes"].ToString());
//                int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_Save_Multiselect_Paediatric_IE", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
//            }
//        }


//        DataMgr.CommitTransaction(this.Transaction);
//        DataMgr.ReleaseConnection(this.Connection);

//        return theDS;
//    }
//    catch
//    {
//        DataMgr.RollBackTransation(this.Transaction);
//        throw;
//    }
//    finally
//    {
//        if (this.Connection != null)
//            DataMgr.ReleaseConnection(this.Connection);
//    }
//}
//Created by- Nidhi Bisht
//Desc- to get the patient's visit id in PED IE forms coz this is single visit form 