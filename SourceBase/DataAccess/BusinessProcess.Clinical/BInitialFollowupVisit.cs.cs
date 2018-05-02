using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Clinical
{
   public class BInitialFollowupVisit: ProcessBase,IinitialFollowupVisit
    {

       public DataSet GetInitialFollowupVisitData(int patientID, int locationID)
       {
           lock (this)
           {
               ClsUtility.Init_Hashtable();
               ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
               ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
               ClsObject VisitManager = new ClsObject();
               return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetInitialFollowupVisitData", ClsDBUtility.ObjectEnum.DataSet);
           }
       }
       public DataSet GetInitialFollowupVisitInfo(int patientID, int locationID, int visitID)
       {
           lock (this)
           {
               ClsUtility.Init_Hashtable();
               ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
               ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
               ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
               ClsObject VisitManager = new ClsObject();
               return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetInitialFollowupVisitInfo", ClsDBUtility.ObjectEnum.DataSet);
           }
       }
       public DataSet SaveUpdateInitialFollowupVisitData(Hashtable hashTable, DataSet dataSet, bool isUpdate, DataTable theCustomDataDT)
       {
           try
           {
               DataSet theDS;
               int visitID;
               this.Connection = DataMgr.GetConnection();
               this.Transaction = DataMgr.BeginTransaction(this.Connection);
               ClsUtility.Init_Hashtable();
               ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
               ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
               ClsUtility.AddParameters("@dataQuality", SqlDbType.Int, hashTable["dataQuality"].ToString());
               ClsUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["UserID"].ToString());

               //Appointment Scheduling
               if (hashTable["visitDate"].ToString() != "")
                   ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, hashTable["visitDate"].ToString());
               ClsUtility.AddParameters("@TypeofVisit", SqlDbType.Int, hashTable["TypeofVisit"].ToString());
               ClsUtility.AddParameters("@Scheduled", SqlDbType.Int, hashTable["Scheduled"].ToString());
               ClsUtility.AddParameters("@treatmentSupporterName", SqlDbType.VarChar, hashTable["treatmentSupporterName"].ToString());
               ClsUtility.AddParameters("@treatmentSupporterContact", SqlDbType.VarChar, hashTable["treatmentSupporterContact"].ToString());


               //Clinical Status
               ClsUtility.AddParameters("@Temp", SqlDbType.Decimal, hashTable["physTemp"].ToString());
               ClsUtility.AddParameters("@height", SqlDbType.Decimal, hashTable["height"].ToString());
               ClsUtility.AddParameters("@weight", SqlDbType.Decimal, hashTable["weight"].ToString());
               ClsUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, hashTable["BPSystolic"].ToString());
               ClsUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, hashTable["BPDiastolic"].ToString());
               ClsUtility.AddParameters("@HR", SqlDbType.Decimal, hashTable["HR"].ToString());
               ClsUtility.AddParameters("@RR", SqlDbType.Decimal, hashTable["RR"].ToString());
               ClsUtility.AddParameters("@Headcircumference", SqlDbType.Decimal, hashTable["Headcircumference"].ToString());

               if (hashTable["WeightForAge"].ToString() != "")
                   ClsUtility.AddParameters("@WeightForAge", SqlDbType.Int, hashTable["WeightForAge"].ToString());
               if (hashTable["WeightForHeight"].ToString() != "")
                   ClsUtility.AddParameters("@WeightForHeight", SqlDbType.Int, hashTable["WeightForHeight"].ToString());
               if (hashTable["BMIz"].ToString() != "")
                   ClsUtility.AddParameters("@BMIz", SqlDbType.Int, hashTable["BMIz"].ToString());
               //Pegnancy
               ClsUtility.AddParameters("@pregnant", SqlDbType.Int, hashTable["pregnant"].ToString());
               if (hashTable["pregnant"].ToString() == "89")
               {
                   if (hashTable["EDD"].ToString() != "")
                       ClsUtility.AddParameters("@EDD", SqlDbType.VarChar, hashTable["EDD"].ToString());
                   ClsUtility.AddParameters("@ANCNo", SqlDbType.Int, hashTable["ANCNo"].ToString());
                   ClsUtility.AddParameters("@ReferredtoPMTCT", SqlDbType.Int, hashTable["ReferredtoPMTCT"].ToString());
               }
               if (hashTable["pregnant"].ToString() == "91")
               {
                   if (hashTable["DateofInducedAbortion"].ToString() != "")
                       ClsUtility.AddParameters("@DateofInducedAbortion", SqlDbType.VarChar, hashTable["DateofInducedAbortion"].ToString());
               }
               if (hashTable["pregnant"].ToString() == "92")
               {
                   if (hashTable["DateofMiscarriage"].ToString() != "")
                       ClsUtility.AddParameters("@DateofMiscarriage", SqlDbType.VarChar, hashTable["DateofMiscarriage"].ToString());
               }

               //Family planning
               ClsUtility.AddParameters("@familyPlanningStatus", SqlDbType.Int, hashTable["familyPlanningStatus"].ToString());
               if (hashTable.ContainsKey("NoFamilyPlanning"))
               {
                   ClsUtility.AddParameters("@NoFamilyPlanning", SqlDbType.Int, hashTable["NoFamilyPlanning"].ToString());
               }


               //TB
               ClsUtility.AddParameters("@TBStatus", SqlDbType.Int, hashTable["TBStatus"].ToString());
               if (hashTable.ContainsKey("TBStartDate"))
               {
                   if (hashTable["TBStartDate"].ToString() != "")
                       ClsUtility.AddParameters("@TBRxStart", SqlDbType.VarChar, hashTable["TBStartDate"].ToString());
               }
               if (hashTable.ContainsKey("TBTreatmentNo"))
               {
                   ClsUtility.AddParameters("@TBRegNumber", SqlDbType.VarChar, hashTable["TBTreatmentNo"].ToString());
               }

               //Nutritional Problems
               ClsUtility.AddParameters("@nutritionalProblem", SqlDbType.Int, hashTable["nutritionalProblem"].ToString());

               //WHO Stage
               ClsUtility.AddParameters("@WHOStage", SqlDbType.Int, hashTable["WHOStage"].ToString());

               //pharmacy
               ClsUtility.AddParameters("@CotrimoxazoleAdhere", SqlDbType.Int, hashTable["CotrimoxazoleAdhere"].ToString());
               ClsUtility.AddParameters("@ARVDrugsAdhere", SqlDbType.Int, hashTable["ARVDrugsAdhere"].ToString());
               ClsUtility.AddParameters("@WhyPooFair", SqlDbType.Int, hashTable["WhyPooFair"].ToString());
               if (hashTable.ContainsKey("reasonARVDrugsPoorFairOther"))
               {
                   ClsUtility.AddParameters("@reasonARVDrugsPoorFairOther", SqlDbType.VarChar, hashTable["reasonARVDrugsPoorFairOther"].ToString());
               }

               //Subsitutions/Interruption
               ClsUtility.AddParameters("@TherapyPlan", SqlDbType.Int, hashTable["TherapyPlan"].ToString());
               ClsUtility.AddParameters("@TherapyReasonCode", SqlDbType.Int, hashTable["TherapyReasonCode"].ToString());
               ClsUtility.AddParameters("@TherapyOther", SqlDbType.VarChar, hashTable["TherapyOther"].ToString());
               if (hashTable["PrescribedARVStartDate"].ToString() != "")
                   ClsUtility.AddParameters("@PrescribedARVStartDate", SqlDbType.VarChar, hashTable["PrescribedARVStartDate"].ToString());


               ClsUtility.AddParameters("@numOfDaysHospitalized", SqlDbType.VarChar, hashTable["numOfDaysHospitalized"].ToString());
               ClsUtility.AddParameters("@nutritionalSupport", SqlDbType.Int, hashTable["nutritionalSupport"].ToString());
               ClsUtility.AddParameters("@infantFeedingOption", SqlDbType.Int, hashTable["infantFeedingOption"].ToString());

               ClsUtility.AddParameters("@attendingClinician", SqlDbType.Int, hashTable["attendingClinician"].ToString());
               if (hashTable["Datenextappointment"].ToString() != "")
                   ClsUtility.AddParameters("@Datenextappointment", SqlDbType.VarChar, hashTable["Datenextappointment"].ToString());



               ClsObject VisitManager = new ClsObject();
               VisitManager.Connection = this.Connection;
               VisitManager.Transaction = this.Transaction;
               if (!isUpdate)
               {
                   // DataSet tempDataSet;
                   theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveInitialFollowupVisit", ClsDBUtility.ObjectEnum.DataSet);
                   // visitID = (int)tempDataSet.Tables[0].Rows[0]["visitID"];
                   visitID = (int)theDS.Tables[0].Rows[0]["visitID"];

                   //Family Planning Methods
                   for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["UserID"].ToString());
                       ClsUtility.AddParameters("@familyPlanningMethodID", SqlDbType.Int, dataSet.Tables[0].Rows[i]["familyPlanningMethodID"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareFamilyPlanning", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Potential Side Effects
                   for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@potentialSideEffectID", SqlDbType.Int, dataSet.Tables[1].Rows[i]["potentialSideEffectID"].ToString());
                       ClsUtility.AddParameters("@potentialSideEffectOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[1].Rows[i]["potentialSideEffect_Other"]))) ? "" : dataSet.Tables[1].Rows[i]["potentialSideEffect_Other"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCarePotentialSideEffect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //New OIs Problems
                   for (int i = 0; i < dataSet.Tables[2].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@newOIsProblemID", SqlDbType.Int, dataSet.Tables[2].Rows[i]["newOIsProblemID"].ToString());
                       ClsUtility.AddParameters("@TBStatus", SqlDbType.Int, hashTable["TBStatus"].ToString());
                       if (hashTable.ContainsKey("TBTreatmentNo"))
                       {
                           ClsUtility.AddParameters("@TBRegNumber", SqlDbType.VarChar, hashTable["TBTreatmentNo"].ToString());
                       }
                       ClsUtility.AddParameters("@newOIsProblemIDOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[2].Rows[i]["newOIsProblemID_Other"]))) ? "" : dataSet.Tables[2].Rows[i]["newOIsProblemID_Other"].ToString());
                       ClsUtility.AddParameters("@nutritionalProblem", SqlDbType.Int, hashTable["nutritionalProblem"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareNewOIsProblem", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }

                   //Referred To
                   for (int i = 0; i < dataSet.Tables[3].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@referredTo", SqlDbType.Int, dataSet.Tables[3].Rows[i]["referredToID"].ToString());
                       ClsUtility.AddParameters("@referredToOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[3].Rows[i]["referredToOtherID_Other"]))) ? "" : dataSet.Tables[3].Rows[i]["referredToOtherID_Other"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareARTReferredTo", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }

                   //Positive Prevention At Risk Population
                   for (int i = 0; i < dataSet.Tables[4].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@RiskPopulationID", SqlDbType.Int, dataSet.Tables[4].Rows[i]["RiskPopulationID"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareAtRiskPopulation", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Positive Prevention At Risk Population Services
                   for (int i = 0; i < dataSet.Tables[5].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@PopulationServiceID", SqlDbType.Int, dataSet.Tables[5].Rows[i]["PopulationServiceID"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareAtRiskPopulationServices", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Prevention with positives (PwP)
                   for (int i = 0; i < dataSet.Tables[6].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@pwpID", SqlDbType.Int, dataSet.Tables[6].Rows[i]["pwpID"].ToString());
                       ClsUtility.AddParameters("@pwpID_Other", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[6].Rows[i]["pwpID_Other"]))) ? "" : dataSet.Tables[6].Rows[i]["pwpID_Other"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCarePreventionwithpositives", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }


                   for (Int32 i = 0; i < theCustomDataDT.Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       string theQuery = theCustomDataDT.Rows[i]["Query"].ToString();
                       theQuery = theQuery.Replace("#99#", hashTable["patientID"].ToString());
                       theQuery = theQuery.Replace("#88#", hashTable["locationID"].ToString());
                       theQuery = theQuery.Replace("#77#", visitID.ToString());
                       theQuery = theQuery.Replace("#66#", "'" + hashTable["visitDate"].ToString() + "'");
                       ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                       int RowsAffected = (Int32)VisitManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }

               }
               else
               {
                   visitID = Convert.ToInt32(hashTable["visitID"].ToString());
                   ClsUtility.AddParameters("@visitID", SqlDbType.Int, hashTable["visitID"].ToString());
                   //ClsUtility.AddParameters("@createDate", SqlDbType.DateTime, hashTable["createDate"].ToString());
                   theDS = (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_UpdateInitialFollowupVisit", ClsDBUtility.ObjectEnum.DataSet);


                   for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@UserId", SqlDbType.Int, hashTable["UserID"].ToString());
                       ClsUtility.AddParameters("@familyPlanningMethodID", SqlDbType.Int, dataSet.Tables[0].Rows[i]["familyPlanningMethodID"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareFamilyPlanning", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }


                   //Potential Side Effects


                   for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@potentialSideEffectID", SqlDbType.Int, dataSet.Tables[1].Rows[i]["potentialSideEffectID"].ToString());
                       ClsUtility.AddParameters("@potentialSideEffectOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[1].Rows[i]["potentialSideEffect_Other"]))) ? "" : dataSet.Tables[1].Rows[i]["potentialSideEffect_Other"].ToString());
                       //ClsUtility.AddParameters("@createDate", SqlDbType.DateTime, hashTable["createDate"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCarePotentialSideEffect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //New OIs Problems
                   for (int i = 0; i < dataSet.Tables[2].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@newOIsProblemID", SqlDbType.Int, dataSet.Tables[2].Rows[i]["newOIsProblemID"].ToString());
                       ClsUtility.AddParameters("@TBStatus", SqlDbType.Int, hashTable["TBStatus"].ToString());
                       if (hashTable.ContainsKey("TBTreatmentNo"))
                       {
                           ClsUtility.AddParameters("@TBRegNumber", SqlDbType.VarChar, hashTable["TBTreatmentNo"].ToString());
                       }
                       ClsUtility.AddParameters("@newOIsProblemIDOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[2].Rows[i]["newOIsProblemID_Other"]))) ? "" : dataSet.Tables[2].Rows[i]["newOIsProblemID_Other"].ToString());
                       ClsUtility.AddParameters("@nutritionalProblem", SqlDbType.Int, hashTable["nutritionalProblem"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareNewOIsProblem", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Referred To
                   for (int i = 0; i < dataSet.Tables[3].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@referredTo", SqlDbType.Int, dataSet.Tables[3].Rows[i]["referredToID"].ToString());
                       ClsUtility.AddParameters("@referredToOther", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[3].Rows[i]["referredToOtherID_Other"]))) ? "" : dataSet.Tables[3].Rows[i]["referredToOtherID_Other"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareARTReferredTo", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Positive Prevention At Risk Population
                   for (int i = 0; i < dataSet.Tables[4].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@RiskPopulationID", SqlDbType.Int, dataSet.Tables[4].Rows[i]["RiskPopulationID"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareAtRiskPopulation", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Positive Prevention At Risk Population Services
                   for (int i = 0; i < dataSet.Tables[5].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@PopulationServiceID", SqlDbType.Int, dataSet.Tables[5].Rows[i]["PopulationServiceID"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCareAtRiskPopulationServices", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   //Prevention with positives (PwP)
                   for (int i = 0; i < dataSet.Tables[6].Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       ClsUtility.AddParameters("@patientID", SqlDbType.Int, hashTable["patientID"].ToString());
                       ClsUtility.AddParameters("@locationID", SqlDbType.Int, hashTable["locationID"].ToString());
                       ClsUtility.AddParameters("@visitID", SqlDbType.Int, visitID.ToString());
                       ClsUtility.AddParameters("@pwpID", SqlDbType.Int, dataSet.Tables[6].Rows[i]["pwpID"].ToString());
                       ClsUtility.AddParameters("@pwpID_Other", SqlDbType.VarChar, (string.IsNullOrEmpty(Convert.ToString(dataSet.Tables[6].Rows[i]["pwpID_Other"]))) ? "" : dataSet.Tables[6].Rows[i]["pwpID_Other"].ToString());
                       int temp = (int)VisitManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveHIVCarePreventionwithpositives", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                   }
                   for (Int32 i = 0; i < theCustomDataDT.Rows.Count; i++)
                   {
                       ClsUtility.Init_Hashtable();
                       string theQuery = theCustomDataDT.Rows[i]["Query"].ToString();
                       theQuery = theQuery.Replace("#99#", hashTable["patientID"].ToString());
                       theQuery = theQuery.Replace("#88#", hashTable["locationID"].ToString());
                       theQuery = theQuery.Replace("#77#", visitID.ToString());
                       theQuery = theQuery.Replace("#66#", "'" + hashTable["visitDate"].ToString() + "'");
                       ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                       int RowsAffected = (Int32)VisitManager.ReturnObject(ClsUtility.theParams, "pr_General_Dynamic_Insert", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
       public int DeleteInitialFollowupVisitForm(string FormName, int OrderNo, int PatientId, int UserID)
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
       public DataSet GetExistInitialFollowupVisitbydate(int PatientID, string VisitdByDate, int locationID)
       {

           ClsUtility.Init_Hashtable();
           ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
           ClsUtility.AddParameters("@VisitDate", SqlDbType.VarChar, VisitdByDate.ToString());
           ClsUtility.AddParameters("@location", SqlDbType.Int, locationID.ToString());
           ClsObject VisitManager = new ClsObject();
           return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_InitialFollowupVisit_DateValidate_Constella", ClsDBUtility.ObjectEnum.DataSet);
       }
       
    }
}
