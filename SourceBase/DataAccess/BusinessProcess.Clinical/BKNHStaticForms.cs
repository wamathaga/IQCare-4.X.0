using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;
using Application.Common;
using System.Data;
using System.Collections;

namespace BusinessProcess.Clinical
{
    public class BKNHStaticForms : ProcessBase, IKNHStaticForms
    {
        public DataSet GetExistKNHStaticFormbydate(int PatientID, string VisitdByDate, int locationID,int Visittype)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                ClsUtility.AddParameters("@VisitDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}",VisitdByDate.ToString()));
                ClsUtility.AddParameters("@location", SqlDbType.Int, locationID.ToString());
                ClsUtility.AddParameters("@Visittype", SqlDbType.Int, Visittype.ToString());
                ClsObject VisitManager = new ClsObject();
                return (DataSet)VisitManager.ReturnObject(ClsUtility.theParams, "pr_GetExistingVisitingDate", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet SaveUpdateExpressFormTriageTab(Hashtable theHT, DataTable dt, DataTable referredTo)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, theHT["locationID"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                if (theHT["ptnAccByCareGiver"].ToString() != "")
                    ClsUtility.AddParameters("@ptnaccbycare", SqlDbType.Int, theHT["ptnAccByCareGiver"].ToString());
                ClsUtility.AddParameters("@caregiverrelationship", SqlDbType.Int, theHT["careGiverRelationship"].ToString());
                if (theHT["temp"] != null)
                    ClsUtility.AddParameters("@temp", SqlDbType.Decimal, theHT["temp"].ToString());
                if (theHT["rr"] != null)
                    ClsUtility.AddParameters("@rr", SqlDbType.Decimal, theHT["rr"].ToString());
                if (theHT["hr"] != null)
                    ClsUtility.AddParameters("@hr", SqlDbType.Decimal, theHT["hr"].ToString());
                ClsUtility.AddParameters("@systolic", SqlDbType.Decimal, theHT["BPSystolic"].ToString());
                ClsUtility.AddParameters("@diastolic", SqlDbType.Decimal, theHT["BPDiastolic"].ToString());
                ClsUtility.AddParameters("@height", SqlDbType.Decimal, theHT["height"].ToString());
                ClsUtility.AddParameters("@weight", SqlDbType.Decimal, theHT["weight"].ToString());
                ClsUtility.AddParameters("@OtherMedCond", SqlDbType.VarChar, theHT["OtherMedicalCondition"].ToString());
                if (theHT["areYouOnFollowUp"].ToString() != "")
                    ClsUtility.AddParameters("@onfollowup", SqlDbType.Int, theHT["areYouOnFollowUp"].ToString());
                ClsUtility.AddParameters("@lastfollowup", SqlDbType.VarChar, theHT["lastFollowUp"].ToString());
                ClsUtility.AddParameters("@visitDate", SqlDbType.VarChar, theHT["visitDate"].ToString());
                ClsUtility.AddParameters("@startTime", SqlDbType.DateTime, theHT["startTime"].ToString());

                ClsUtility.AddParameters("@NurseComments", SqlDbType.DateTime, theHT["NurseComments"].ToString());
                ClsUtility.AddParameters("@SpecilistReferral", SqlDbType.DateTime, theHT["SpecilistReferral"].ToString());
                ClsUtility.AddParameters("@OtherReferral", SqlDbType.DateTime, theHT["OtherReferral"].ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_Express_Form_TriageTab", ClsDBUtility.ObjectEnum.DataSet);

                int visitID;
                //int updateFlag;
                if (Convert.ToInt32(theHT["visitID"].ToString()) == 0)
                {
                    visitID = Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    //updateFlag = 0;
                }
                else
                {
                    visitID = Convert.ToInt32(theHT["visitID"].ToString());
                    //updateFlag = 1;
                }

                //Pre Existing Medical Condition
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, dt.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "SpecificMedicalCondition");
                        int save = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        //updateFlag = 0;
                    }
                }

                if (referredTo.Rows.Count > 0)
                {
                    for (int i = 0; i < referredTo.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitID.ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, referredTo.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "RefferedToFUpF");
                        int save = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        //updateFlag = 0;
                    }
                }

                return theDS;
            }
        }


        public DataSet GetExpressFormData(int ptn_pk, int visit_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());


                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_KNH_Express_Form_data", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetExpressFormAutoPopulatingData(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                
                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_KNH_Express_Form_Autopopulating_data", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetPEPFormAutoPopulatingData(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_KNH_PEP_Form_Autopopulating_data", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetTBScreeningAutoPopulatingData(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_KNH_TBScreening_Autopopulating_data", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetPwPAutoPopulatingData(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_KNH_PwP_Autopopulating_data", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet CheckIfPreviuosTabSaved(string tabName, int visit_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@tabName", SqlDbType.VarChar, tabName.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());


                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_CheckIfPreviousTabIsSaved", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataTable GetTabID(string tabName)
        {
            lock (this)
            {
                DataTable theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@tabName", SqlDbType.VarChar, tabName.ToString());
                //ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());


                theDS = (DataTable)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetTabID", ClsDBUtility.ObjectEnum.DataTable);

                return theDS;
            }
        }

        public DataSet CheckIfTabSaved(int tabID, int visit_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@tabID", SqlDbType.Int, tabID.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());


                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_CheckIfTabSaved", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetExtruderData(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_ExtruderVitals", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetLatestWHOStage(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_WHOStage", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetTBScreeningFormData(int ptn_pk, int visit_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_KNH_TBScreening_UserControl", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetPwPFormData(int ptn_pk, int visit_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_KNH_PwP_UserControl", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public string GetSignature(string tabName, int visit_pk)
        {
            
            lock (this)
            {
                string signature = "";
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TabName", SqlDbType.VarChar, tabName.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visit_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_KNH_Signature", ClsDBUtility.ObjectEnum.DataSet);

                if (theDS.Tables[0].Rows.Count > 0)
                {
                    signature = theDS.Tables[0].Rows[0][0].ToString();
                }
                return signature;
            }
        }

        public DataSet SaveUpdateARVTherapy(Hashtable theHT)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, theHT["userID"].ToString());
                ClsUtility.AddParameters("@locationID", SqlDbType.Int, theHT["locationID"].ToString());
                ClsUtility.AddParameters("@TherapyPlan", SqlDbType.Int, theHT["treatmentPlan"].ToString());
                ClsUtility.AddParameters("@Noofdrugssubstituted", SqlDbType.Int, theHT["noOfDrugsSubstituted"].ToString());
                ClsUtility.AddParameters("@reasonforswitchto2ndlineregimen", SqlDbType.Int, theHT["reasonForSwitchTo2ndLineRegimen"].ToString());
                ClsUtility.AddParameters("@specifyOtherEligibility", SqlDbType.Int, theHT["specifyOtherEligibility"].ToString());
                ClsUtility.AddParameters("@specifyotherARTchangereason", SqlDbType.Int, theHT["specifyotherARTchangereason"].ToString());
                ClsUtility.AddParameters("@specifyOtherStopCode", SqlDbType.Int, theHT["specifyOtherStopCode"].ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_ARVTherapy", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetLastRegimenDispensed(int ptn_pk)
        {
           
            DataSet theDS;
            ClsObject ClsObj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

            theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetLastRegimensDispensed", ClsDBUtility.ObjectEnum.DataSet);

            return theDS;
            
        }

        public DataSet GetPatientDrugHistory(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Pharmacy_GetPatientDrugHistory", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }


        public DataSet useExpressFormRules(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_UseExpressFormRules", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }


        public DataSet checkDuplicateVisit(string visitDate, int visitType, int ptnPk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Visit_date", SqlDbType.VarChar, visitDate.ToString());
                ClsUtility.AddParameters("@Visit_type", SqlDbType.Int, visitType.ToString());
                ClsUtility.AddParameters("@ptn_pk", SqlDbType.Int, ptnPk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_CheckDuplicateVisit", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet SaveUpdateExpressFormClinicalAssessmentTab(Hashtable theHT, DataTable ARVShortTermEffects, DataTable ARVLongTermEffects, DataTable Eligiblethrough, DataTable ARTchangecode, DataTable ARTstopcode)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, theHT["locationID"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                //ClsUtility.AddParameters("@regimenPrescribed", SqlDbType.Int, regimenPrescribed.ToString());
                //ClsUtility.AddParameters("@otherRegimenPrescribed", SqlDbType.VarChar, otherRegimenPrescribed.ToString());
                if (theHT["missedAnyDoses"].ToString() != "")
                    ClsUtility.AddParameters("@missedAnyDoses", SqlDbType.Int, theHT["missedAnyDoses"].ToString());
                ClsUtility.AddParameters("@specifyWhyDosesMissed", SqlDbType.Int, theHT["specifyWhyDosesMissed"].ToString());
                if (theHT["delayedTakingMedication"].ToString() != "")
                    ClsUtility.AddParameters("@delayedTakingMedication", SqlDbType.Int, theHT["delayedTakingMedication"].ToString());
                ClsUtility.AddParameters("@labEvaluation", SqlDbType.Int, theHT["labEvaluation"].ToString());
                ClsUtility.AddParameters("@LabReviewOtherTests", SqlDbType.VarChar, theHT["txtLabReviewOtherTests"].ToString());
                ClsUtility.AddParameters("@OIProphylaxis", SqlDbType.Int, theHT["OIProphylaxis"].ToString());
                ClsUtility.AddParameters("@cotrimoxazolePrescribed", SqlDbType.Int, theHT["cotrimoxazolePrescribed"].ToString());
                ClsUtility.AddParameters("@fluconazolePrescribed", SqlDbType.Int, theHT["FluconazolePrescribed"].ToString());
                ClsUtility.AddParameters("@specifyOtherOIProphylaxis", SqlDbType.VarChar, theHT["specifyOtherOIProphylaxis"].ToString());
                ClsUtility.AddParameters("@Plan", SqlDbType.VarChar, theHT["plan"].ToString());
                ClsUtility.AddParameters("@PwPMessageGiven", SqlDbType.Int, theHT["PwPMessageGiven"].ToString());
                if (theHT["issuedWithCondoms"].ToString() != "")
                    ClsUtility.AddParameters("@issuedWithCondoms", SqlDbType.Int, theHT["issuedWithCondoms"].ToString());
                ClsUtility.AddParameters("@reasonCondomsNotIssued", SqlDbType.VarChar, theHT["reasonCondomsNotIssued"].ToString());
                if (theHT["pregIntBeforeNxtVist"].ToString() != "")
                    ClsUtility.AddParameters("@pregIntBeforeNxtVist", SqlDbType.Int, theHT["pregIntBeforeNxtVist"].ToString());
                if (theHT["fertilityOptions"].ToString() != "")
                    ClsUtility.AddParameters("@fertilityOptions", SqlDbType.Int, theHT["fertilityOptions"].ToString());
                if (theHT["dualContraception"].ToString() != "")
                    ClsUtility.AddParameters("@dualContraception", SqlDbType.Int, theHT["dualContraception"].ToString());
                if (theHT["otherFPMethod"].ToString() != "")
                    ClsUtility.AddParameters("@otherFPMethod", SqlDbType.Int, theHT["otherFPMethod"].ToString());
                ClsUtility.AddParameters("@specifyOtherFPMethod", SqlDbType.Int, theHT["specifyOtherFPMethod"].ToString());
                if (theHT["screenedForCancer"].ToString() != "")
                    ClsUtility.AddParameters("@screenedForCancer", SqlDbType.Int, theHT["screenedForCancer"].ToString());
                ClsUtility.AddParameters("@CaCervixScreeningResults", SqlDbType.Int, theHT["caCervixScreeningResults"].ToString());
                if (theHT["referredForCervicalScreening"].ToString() != "")
                    ClsUtility.AddParameters("@referredForCervicalScreening", SqlDbType.Int, theHT["referredForCervicalScreening"].ToString());
                ClsUtility.AddParameters("@startTime", SqlDbType.DateTime, theHT["startTime"].ToString());

                ClsUtility.AddParameters("@treatmentPlan", SqlDbType.Int, theHT["treatmentPlan"].ToString());
                ClsUtility.AddParameters("@Noofdrugssubstituted", SqlDbType.Int, theHT["Noofdrugssubstituted"].ToString());
                ClsUtility.AddParameters("@reasonforswitchto2ndlineregimen", SqlDbType.Int, theHT["reasonforswitchto2ndlineregimen"].ToString());
                ClsUtility.AddParameters("@specifyOtherEligibility", SqlDbType.VarChar, theHT["specifyOtherEligibility"].ToString());
                ClsUtility.AddParameters("@specifyotherARTchangereason", SqlDbType.VarChar, theHT["specifyotherARTchangereason"].ToString());
                ClsUtility.AddParameters("@specifyOtherStopCode", SqlDbType.VarChar, theHT["specifyOtherStopCode"].ToString());

                ClsUtility.AddParameters("@otherShortTermEffects", SqlDbType.VarChar, theHT["OtherShortTermSideEffect"].ToString());
                ClsUtility.AddParameters("@otherLongTermEffects", SqlDbType.VarChar, theHT["OtherLongTermSideEffect"].ToString());


                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_KNH_Express_Form_CATab", ClsDBUtility.ObjectEnum.DataSet);

                if (ARVShortTermEffects.Rows.Count > 0)
                {
                    for (int i = 0; i < ARVShortTermEffects.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, ARVShortTermEffects.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "ShortTermEffects");
                        int save = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (ARVLongTermEffects.Rows.Count > 0)
                {
                    for (int i = 0; i < ARVLongTermEffects.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, ARVLongTermEffects.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "LongTermEffects");
                        int save = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (Eligiblethrough.Rows.Count > 0)
                {
                    for (int i = 0; i < Eligiblethrough.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, Eligiblethrough.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "ARTEligibility");
                        int save = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (ARTchangecode.Rows.Count > 0)
                {
                    for (int i = 0; i < ARTchangecode.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, ARTchangecode.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "ARTchangecode");
                        int save = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (ARTstopcode.Rows.Count > 0)
                {
                    for (int i = 0; i < ARTstopcode.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, ARTstopcode.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "ARTstopcode");
                        int save = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                return theDS;
            }
        }

        public DataSet SaveUpdateTBScreening(Hashtable theHT, DataTable TBAssessment, DataTable IPTStopReason, DataTable ReviewCheckList, DataTable SignsOfHepatitis)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, theHT["locationID"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                ClsUtility.AddParameters("@TBFindings", SqlDbType.Int, theHT["TBFindings"].ToString());
                ClsUtility.AddParameters("@TBAvailableResults", SqlDbType.Int, theHT["availableTBResults"].ToString());
                ClsUtility.AddParameters("@SputumSmear", SqlDbType.Int, theHT["SputumSmear"].ToString());
                ClsUtility.AddParameters("@SputumDST", SqlDbType.Int, theHT["SputumDST"].ToString());
                ClsUtility.AddParameters("@GeneExpert", SqlDbType.Int, theHT["GeneExpert"].ToString());
                if (theHT["chestXRay"] != null)
                    if (!String.IsNullOrEmpty(theHT["chestXRay"].ToString()))
                    ClsUtility.AddParameters("@ChestXRay", SqlDbType.Int, theHT["chestXRay"].ToString());
                if (theHT["tissueBiopsy"] != null)
                    ClsUtility.AddParameters("@TissueBiopsy", SqlDbType.Int, theHT["tissueBiopsy"].ToString());
                //Updated Date-19,Jun 2014
                //Updated By-Nidhi
                if (!String.IsNullOrEmpty(theHT["GeneExpertDate"].ToString()))
                    ClsUtility.AddParameters("@GeneExpertDate", SqlDbType.VarChar, theHT["GeneExpertDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["sputumSmearDate"].ToString()))
                    ClsUtility.AddParameters("@SputumSmearDate", SqlDbType.VarChar, theHT["sputumSmearDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["SputumDSTDate"].ToString()))
                    ClsUtility.AddParameters("@SputumDSTDate", SqlDbType.VarChar, theHT["SputumDSTDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["TBRegimenStartDate"].ToString()))
                    ClsUtility.AddParameters("@TBRegimenStartDate", SqlDbType.VarChar, theHT["TBRegimenStartDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["TBRegimenEndDate"].ToString()))
                    ClsUtility.AddParameters("@TBRegimenEndDate", SqlDbType.VarChar, theHT["TBRegimenEndDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["chestXRayDate"].ToString()))
                    ClsUtility.AddParameters("@ChestXRayDate", SqlDbType.VarChar, theHT["chestXRayDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["PyridoxineEndDate"].ToString()))
                    ClsUtility.AddParameters("@PyridoxineEndDate", SqlDbType.VarChar, theHT["PyridoxineEndDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["INHStartDate"].ToString()))
                    ClsUtility.AddParameters("@INHStartDate", SqlDbType.VarChar, theHT["INHStartDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["INHEndDate"].ToString()))
                    ClsUtility.AddParameters("@INHEndDate", SqlDbType.VarChar, theHT["INHEndDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["PyridoxineStartDate"].ToString()))
                    ClsUtility.AddParameters("@PyridoxineStartDate", SqlDbType.VarChar, theHT["PyridoxineStartDate"].ToString());

                if (!String.IsNullOrEmpty(theHT["TissueBiopsyDate"].ToString()))
                    ClsUtility.AddParameters("@TissueBiopsyDate", SqlDbType.VarChar, theHT["TissueBiopsyDate"].ToString());
                
                ClsUtility.AddParameters("@CXRResults", SqlDbType.Int, theHT["CXRResults"].ToString());
                ClsUtility.AddParameters("@OtherCXR", SqlDbType.VarChar, theHT["OtherCXRResults"].ToString());
                ClsUtility.AddParameters("@TBClassification", SqlDbType.Int, theHT["TBClassification"].ToString());
                ClsUtility.AddParameters("@PatientClassification", SqlDbType.Int, theHT["PatientClassification"].ToString());
                ClsUtility.AddParameters("@TBPlan", SqlDbType.Int, theHT["TBPLan"].ToString());
                ClsUtility.AddParameters("@OtherTBPlan", SqlDbType.VarChar, theHT["OtherTBPlan"].ToString());
                ClsUtility.AddParameters("@TBRegimen", SqlDbType.Int, theHT["TBRegimen"].ToString());
                ClsUtility.AddParameters("@OtherTBRegimen", SqlDbType.VarChar, theHT["OtherTBRegimen"].ToString());

                ClsUtility.AddParameters("@TBTreatmentOutcome", SqlDbType.Int, theHT["TBTreatment"].ToString());
                ClsUtility.AddParameters("@OtherTBTreatmentOutcome", SqlDbType.VarChar, theHT["OtherTBTreatment"].ToString());
                ClsUtility.AddParameters("@IPT", SqlDbType.Int, theHT["IPT"].ToString());


                if (theHT["AdherenceAddressed"] != null)
                    ClsUtility.AddParameters("@AdherenceAddressed", SqlDbType.Int, theHT["AdherenceAddressed"].ToString());
                if (theHT["missedAnyDoses"] != null)
                    ClsUtility.AddParameters("@AnyMissedDoses", SqlDbType.Int, theHT["missedAnyDoses"].ToString());
                if (theHT["ReferredForAdherence"] != null)
                    ClsUtility.AddParameters("@ReferredForAdherence", SqlDbType.Int, theHT["ReferredForAdherence"].ToString());
                ClsUtility.AddParameters("@OtherTBSideEffects", SqlDbType.VarChar, theHT["SpecifyOtherTBSideEffects"].ToString());
                if (theHT["ContactsScreenedForTB"] != null)
                    ClsUtility.AddParameters("@ContactsScreenedForTB", SqlDbType.Int, theHT["ContactsScreenedForTB"].ToString());
                ClsUtility.AddParameters("@IfNoSpecifyWhy", SqlDbType.VarChar, theHT["SpecifyWhyContactNotScreenedForTB"].ToString());
                ClsUtility.AddParameters("@startTime", SqlDbType.VarChar, theHT["startTime"].ToString());
                ClsUtility.AddParameters("@FacilityPatientReferredTo", SqlDbType.Int, theHT["FacilityPatientReferredTo"].ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, theHT["FormName"].ToString());
                ClsUtility.AddParameters("@ReasonDeclinedIPT", SqlDbType.Int, theHT["ReasonDeclinedIPT"].ToString());
                ClsUtility.AddParameters("@OtherReasonDeclinedIPT", SqlDbType.VarChar, theHT["OtherReasonDeclinedIPT"].ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_TBScreening_UserControl", ClsDBUtility.ObjectEnum.DataSet);
                //TB Assessment
                if (TBAssessment.Rows.Count > 0)
                {
                    for (int i = 0; i < TBAssessment.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, TBAssessment.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        ClsUtility.AddParameters("@age", SqlDbType.Float, theHT["age"].ToString());
                        int TBAssess = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_TBAssessment", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                /**/

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, theHT["locationID"].ToString());
                ClsUtility.AddParameters("@IsCough", SqlDbType.Int, theHT["IsCough"].ToString());
                ClsUtility.AddParameters("@IsFever", SqlDbType.Int, theHT["IsFever"].ToString());
                ClsUtility.AddParameters("@IsFailureThriveOrPoorWeightGain", SqlDbType.Int, theHT["IsFailureThriveOrPoorWeightGain"].ToString());
                ClsUtility.AddParameters("@IsLethargy", SqlDbType.Int, theHT["IsLethargy"].ToString());
                ClsUtility.AddParameters("@IsContact", SqlDbType.Int, theHT["IsContact"].ToString());
                ClsUtility.AddParameters("@IsNoticeableWeightLoss", SqlDbType.Int, theHT["IsNoticeableWeightLoss"].ToString());
                ClsUtility.AddParameters("@IsNightSweats", SqlDbType.Int, theHT["IsNightSweats"].ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_TBScreeningFindingScreening", ClsDBUtility.ObjectEnum.DataSet);

                /**/

                //IPT Stop Reason
                if (IPTStopReason.Rows.Count > 0)
                {
                    for (int i = 0; i < IPTStopReason.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, IPTStopReason.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        int IPTStop = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_IPTStopReason", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }


                //TB Review Checklist
                if (ReviewCheckList.Rows.Count > 0)
                {
                    for (int i = 0; i < ReviewCheckList.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, ReviewCheckList.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        int IPTStop = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_TBReviewCheckList", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (SignsOfHepatitis.Rows.Count > 0)
                {
                    for (int i = 0; i < SignsOfHepatitis.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, SignsOfHepatitis.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "SignsOfHepatitis");
                        int SignsHepatitis = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }

                return theDS;
            }
        }

        public DataSet SaveUpdatePwP(Hashtable theHT, DataTable HighRisk, DataTable TransitionPreparation, DataTable ReferredTo, DataTable Counselling)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, theHT["locationID"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                if (theHT["SexualActiveness"].ToString() != "")
                    ClsUtility.AddParameters("@SexuallyActiveLast6Months", SqlDbType.Int, theHT["SexualActiveness"].ToString());
                ClsUtility.AddParameters("@SexualOrientation", SqlDbType.Int, theHT["SexualOrientation"].ToString());
                if (theHT["KnowSexualPartnerHIVStatus"].ToString() != "")
                    ClsUtility.AddParameters("@DisclosedStatusToSexualPartner", SqlDbType.Int, theHT["KnowSexualPartnerHIVStatus"].ToString());
                ClsUtility.AddParameters("@HIVstatusOfsexualPartner", SqlDbType.Int, theHT["PartnerHIVStatus"].ToString());
                if (theHT["GivenPWPMessages"].ToString() != "")
                    ClsUtility.AddParameters("@PwPMessagesGiven", SqlDbType.Int, theHT["GivenPWPMessages"].ToString());
                if (theHT["SaferSexImportanceExplained"].ToString() != "")
                    ClsUtility.AddParameters("@ImpOfSafeSexExplained", SqlDbType.Int, theHT["SaferSexImportanceExplained"].ToString());
                if (theHT["LMP"].ToString() != "")
                    ClsUtility.AddParameters("@LMPAssessed", SqlDbType.Int, theHT["LMP"].ToString());
                if (theHT["LMPDate"].ToString() != "")
                    ClsUtility.AddParameters("@LMPDate", SqlDbType.VarChar, theHT["LMPDate"].ToString());
                ClsUtility.AddParameters("@ReasonLMPNotAssessed", SqlDbType.Int, theHT["ReasonLMP"].ToString());
                if (theHT["PDTDone"].ToString() != "")
                    ClsUtility.AddParameters("@PregnancyTestDone", SqlDbType.Int, theHT["PDTDone"].ToString());
                if (theHT["ClientPregnant"].ToString() != "")
                    ClsUtility.AddParameters("@clientPregnant", SqlDbType.Int, theHT["ClientPregnant"].ToString());
                if (theHT["PMTCTOffered"].ToString() != "")
                    ClsUtility.AddParameters("@referredToPMTCT", SqlDbType.Int, theHT["PMTCTOffered"].ToString());
                if (!String.IsNullOrEmpty(theHT["EDD"].ToString()))
                    ClsUtility.AddParameters("@EDD", SqlDbType.VarChar, theHT["EDD"].ToString());
                if (theHT["IntentionOfPregnancy"].ToString() != "")
                    ClsUtility.AddParameters("@IntendToBePregnantBeforeNextVisit", SqlDbType.Int, theHT["IntentionOfPregnancy"].ToString());
                if (theHT["DiscussedFertilityOptions"].ToString() != "")
                    ClsUtility.AddParameters("@DiscussedFertilityOptions", SqlDbType.Int, theHT["DiscussedFertilityOptions"].ToString());
                if (theHT["DiscussedDualContraception"].ToString() != "")
                    ClsUtility.AddParameters("@discussedDualContraception", SqlDbType.Int, theHT["DiscussedDualContraception"].ToString());
                if (theHT["CondomsIssued"].ToString() != "")
                    ClsUtility.AddParameters("@condomsIssued", SqlDbType.Int, theHT["CondomsIssued"].ToString());
                ClsUtility.AddParameters("@ReasonCondomNoIssued", SqlDbType.VarChar, theHT["CondomNotIssued"].ToString());
                if (theHT["STIScreened"].ToString() != "")
                    ClsUtility.AddParameters("@ScreenedForSTI", SqlDbType.Int, theHT["STIScreened"].ToString());
                ClsUtility.AddParameters("@UrethralDischarge", SqlDbType.Int, theHT["UrethralDischarge"].ToString());
                ClsUtility.AddParameters("@VaginalDischarge", SqlDbType.Int, theHT["VaginalDischarge"].ToString());
                ClsUtility.AddParameters("@GenitalUlceration", SqlDbType.Int, theHT["GenitalUlceration"].ToString());
                ClsUtility.AddParameters("@STITreatment", SqlDbType.VarChar, theHT["STITreatmentPlan"].ToString());
                ClsUtility.AddParameters("@OtherSTITreatment", SqlDbType.VarChar, theHT["OtherSTITreatmentPlan"].ToString());
                if (theHT["OnFP"].ToString() != "")
                    ClsUtility.AddParameters("@OnFPMethod", SqlDbType.Int, theHT["OnFP"].ToString());
                ClsUtility.AddParameters("@SpecifyFPMethod", SqlDbType.Int, theHT["FPMethod"].ToString());
                ClsUtility.AddParameters("@referredForFP", SqlDbType.Int, theHT["ReferredFP"].ToString());
                if (theHT["CervicalCancerScreened"].ToString() != "")
                    ClsUtility.AddParameters("@screenedForCervicalCancer", SqlDbType.Int, theHT["CervicalCancerScreened"].ToString());
                ClsUtility.AddParameters("@CacervixScreeningResults", SqlDbType.Int, theHT["CervicalCancerScreeningResults"].ToString());
                if (theHT["ReferredForCervicalCancerScreening"].ToString() != "")
                    ClsUtility.AddParameters("@referredForCaScreening", SqlDbType.Int, theHT["ReferredForCervicalCancerScreening"].ToString());
                if (theHT["HPVOffered"].ToString() != "")
                    ClsUtility.AddParameters("@HPVOffered", SqlDbType.Int, theHT["HPVOffered"].ToString());
                ClsUtility.AddParameters("@HPVVaccineOffered", SqlDbType.Int, theHT["OfferedHPVVaccine"].ToString());
                if (!String.IsNullOrEmpty(theHT["HPVDoseDate"].ToString()))
                    ClsUtility.AddParameters("@HPVDoseDate", SqlDbType.VarChar, theHT["HPVDoseDate"].ToString());
                if (theHT["WardAdmission"].ToString() != "")
                    ClsUtility.AddParameters("@WardAdmission", SqlDbType.Int, theHT["WardAdmission"].ToString());
                ClsUtility.AddParameters("@specifyOtherReferredTo", SqlDbType.VarChar, theHT["SpecifyOtherRefferedTo"].ToString());
                ClsUtility.AddParameters("@specifySpecialistClinic", SqlDbType.VarChar, theHT["ReferToSpecialistClinic"].ToString());
                ClsUtility.AddParameters("@OtherCounselling", SqlDbType.VarChar, theHT["OtherCounselling"].ToString());
                if (theHT["TCA"].ToString() != "")
                    ClsUtility.AddParameters("@TCA", SqlDbType.Int, theHT["TCA"].ToString());
                ClsUtility.AddParameters("@startTime", SqlDbType.VarChar, theHT["startTime"].ToString());
                ClsUtility.AddParameters("@FormName", SqlDbType.VarChar, theHT["FormName"].ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_PwP_UserControl", ClsDBUtility.ObjectEnum.DataSet);

                if (HighRisk.Rows.Count > 0)
                {
                    for (int i = 0; i < HighRisk.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, HighRisk.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "HighRisk");
                        int SignsHepatitis = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }

                if (TransitionPreparation.Rows.Count > 0)
                {
                    for (int i = 0; i < TransitionPreparation.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, TransitionPreparation.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "TransitionPreparation");
                        int SignsHepatitis = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }

                if (ReferredTo.Rows.Count > 0)
                {
                    for (int i = 0; i < ReferredTo.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, ReferredTo.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "RefferedToFUpF");
                        int SignsHepatitis = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }

                if (Counselling.Rows.Count > 0)
                {
                    for (int i = 0; i < Counselling.Rows.Count; i++)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["patientID"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitID"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, Counselling.Rows[i]["value"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["userID"].ToString());
                        ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "counselling");
                        int SignsHepatitis = (int)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                    }
                }

                return theDS;
            }
        }
        public DataTable GetPatientFeatures(int Ptn_pk)
        {
            ClsObject ClsObj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
            DataTable dtfeatures = (DataTable)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientFeatures", ClsDBUtility.ObjectEnum.DataTable);
            return dtfeatures;

        }

        public DataSet GetZScoreValues(int Ptn_pk, string gender, string height)
        {
            ClsObject ClsObj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
            ClsUtility.AddParameters("@sex", SqlDbType.VarChar, gender.ToString());
            ClsUtility.AddParameters("@height", SqlDbType.VarChar, height.ToString());
            DataSet dsZScore = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_ZScores", ClsDBUtility.ObjectEnum.DataSet);
            return dsZScore;

        }

        public DataSet GetZScoreNewImplementation(int ptn_pk, string gender, string height)
        {
            ClsObject ClsObj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
            ClsUtility.AddParameters("@sex", SqlDbType.VarChar, gender.ToString());
            ClsUtility.AddParameters("@height", SqlDbType.VarChar, height.ToString());
            DataSet dsZScore = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetZScoresDetails", ClsDBUtility.ObjectEnum.DataSet);
            return dsZScore;

        }

        public DataSet GetAdultFollowUpFormAutoPopulatingData(int ptn_pk)
        {
            lock (this)
            {
                DataSet theDS;
                ClsObject ClsObj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());

                theDS = (DataSet)ClsObj.ReturnObject(ClsUtility.theParams, "pr_Clinical_Get_KNH_Adult_followup_Form_Autopopulating_data", ClsDBUtility.ObjectEnum.DataSet);

                return theDS;
            }
        }

        public DataSet GetAlerts(int PatientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
                ClsObject FamilyInfo = new ClsObject();
                return (DataSet)FamilyInfo.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetAlerts", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

    }
   
}
