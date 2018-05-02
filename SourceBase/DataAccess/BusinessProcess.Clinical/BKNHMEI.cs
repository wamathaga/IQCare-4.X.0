using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using Interface.Clinical;
using DataAccess.Common;
using System.Data;
using DataAccess.Entity;
using System.Collections;

namespace BusinessProcess.Clinical
{
    class BKNHMEI : ProcessBase, IKNHMEI
    {

        public DataSet GetKNHMEI_Data(int PatientId, int VisitId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_KNH_GetPMTCTMEIPatientData", ClsDBUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetKNHMEI_LabResult(int PatientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, PatientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_KNH_GetPMTCTMEIPatientLabResult", ClsDBUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet SaveUpdateKNHMEI_TriageTab(Hashtable theHT, DataSet theDS, String Tab)
        {
            ClsObject KNHMEIManager = new ClsObject();
            DataSet retval = new DataSet();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                KNHMEIManager.Connection = this.Connection;
                KNHMEIManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                switch (Tab)
                {
                    case "Triage":
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@VisitDate", SqlDbType.VarChar, theHT["VisitDate"].ToString());
                        ClsUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        ClsUtility.AddParameters("@LMP", SqlDbType.VarChar, theHT["LMP"].ToString());
                        ClsUtility.AddParameters("@EDD", SqlDbType.VarChar, theHT["EDD"].ToString());
                        ClsUtility.AddParameters("@Parity", SqlDbType.Int, theHT["Parity"].ToString());
                        ClsUtility.AddParameters("@Gravidae", SqlDbType.Int, theHT["Gravidae"].ToString());
                        ClsUtility.AddParameters("@Gestation", SqlDbType.VarChar, theHT["Gestation"].ToString());
                        ClsUtility.AddParameters("@VisitNumber", SqlDbType.Int, theHT["VisitNumber"].ToString());
                        ClsUtility.AddParameters("@Temp", SqlDbType.Decimal, theHT["Temp"].ToString());
                        ClsUtility.AddParameters("@RR", SqlDbType.Decimal, theHT["RR"].ToString());
                        ClsUtility.AddParameters("@HR", SqlDbType.Decimal, theHT["HR"].ToString());
                        ClsUtility.AddParameters("@BPSys", SqlDbType.Decimal, theHT["BPSys"].ToString());
                        ClsUtility.AddParameters("@BPDys", SqlDbType.Decimal, theHT["BPDys"].ToString());
                        ClsUtility.AddParameters("@Height", SqlDbType.Decimal, theHT["Height"].ToString());
                        ClsUtility.AddParameters("@Weight", SqlDbType.Decimal, theHT["Weight"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        ClsUtility.AddParameters("@tetanustoxoid", SqlDbType.Int, theHT["tetanustoxoid"].ToString());
                        ClsUtility.AddParameters("@TetanusVaccineDose", SqlDbType.Int, theHT["tetanustoxoidVaccine"].ToString());
                        ClsUtility.AddParameters("@TetanusVaccineReason", SqlDbType.Int, theHT["tetanustoxoidVaccineNo"].ToString());
                        ClsUtility.AddParameters("@Mernarche", SqlDbType.Int, theHT["Mernarche"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateKNHMEITriage_Futures", ClsDBUtility.ObjectEnum.DataSet);
                        break;

                    case "HTC":
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        ClsUtility.AddParameters("@PrevHIVStatus", SqlDbType.Int, theHT["PrevHIVStatus"].ToString());
                        ClsUtility.AddParameters("@PrevPHIVTesting", SqlDbType.Int, theHT["PrevPHIVTesting"].ToString());
                        ClsUtility.AddParameters("@LastHIVTest", SqlDbType.VarChar, theHT["LastHIVTest"].ToString());
                        ClsUtility.AddParameters("@PreTestCounseling", SqlDbType.Int, theHT["PreTestCounseling"].ToString());
                        ClsUtility.AddParameters("@PostTestCounseling", SqlDbType.Int, theHT["PostTestCounseling"].ToString());
                        ClsUtility.AddParameters("@HIVTestingToday", SqlDbType.Int, theHT["HIVTestingToday"].ToString());
                        ClsUtility.AddParameters("@HIVTestingTodayDone", SqlDbType.Int, theHT["HIVTestingTodayDone"].ToString());
                        ClsUtility.AddParameters("@FinalHIVResult", SqlDbType.Int, theHT["FinalHIVResult"].ToString());
                        ClsUtility.AddParameters("@Patientaccompaniedpartner", SqlDbType.Int, theHT["Patientaccompaniedpartner"].ToString());
                        ClsUtility.AddParameters("@partnerpretestcounselling", SqlDbType.Int, theHT["partnerpretestcounselling"].ToString());
                        ClsUtility.AddParameters("@partnerFinalHIVResult", SqlDbType.Int, theHT["partnerFinalHIVResult"].ToString());
                        ClsUtility.AddParameters("@partnerPostTestcounselling", SqlDbType.Int, theHT["partnerPostTestcounselling"].ToString());
                        ClsUtility.AddParameters("@CoupleDiscordant", SqlDbType.Int, theHT["CoupleDiscordant"].ToString());
                        ClsUtility.AddParameters("@HIVTestdonetopartner", SqlDbType.Int, theHT["HIVTestdonetopartner"].ToString());
                        ClsUtility.AddParameters("@PartnersDNAPCRresult", SqlDbType.Int, theHT["PartnersDNAPCRresult"].ToString());
                        ClsUtility.AddParameters("@familyinformationFilled", SqlDbType.Int, theHT["familyinformationFilled"].ToString());
                        ClsUtility.AddParameters("@membersofthefamilybeentested", SqlDbType.Int, theHT["membersofthefamilybeentested"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateKNHMEIHTC_Futures", ClsDBUtility.ObjectEnum.DataSet);
                        break;

                    case "Profile":
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        ClsUtility.AddParameters("@HMHealth", SqlDbType.Int, theHT["HMHealth"].ToString());
                        ClsUtility.AddParameters("@OtherHMHealth", SqlDbType.VarChar, theHT["OtherHMHealth"].ToString());
                        ClsUtility.AddParameters("@CMHealth", SqlDbType.Int, theHT["CMHealth"].ToString());
                        ClsUtility.AddParameters("@OtherCMHealth", SqlDbType.VarChar, theHT["OtherCMHealth"].ToString());
                        ClsUtility.AddParameters("@ExperienceanyGBV", SqlDbType.Int, theHT["ExperienceanyGBV"].ToString());
                        ClsUtility.AddParameters("@HIVSubstanceAbused", SqlDbType.Int, theHT["HIVSubstanceAbused"].ToString());
                        ClsUtility.AddParameters("@Preferedmodeofdelivery", SqlDbType.Int, theHT["Preferedmodeofdelivery"].ToString());
                        ClsUtility.AddParameters("@PreferedSiteDelivery", SqlDbType.VarChar, theHT["PreferedSiteDelivery"].ToString());
                        ClsUtility.AddParameters("@PreferedSiteDeliveryAdditionalnotes", SqlDbType.VarChar, theHT["PreferedSiteDeliveryAdditionalnotes"].ToString());
                        //ClsUtility.AddParameters("@YrofDelivery", SqlDbType.Int, theHT["YrofDelivery"].ToString());
                        //ClsUtility.AddParameters("@PlaceofDelivery", SqlDbType.VarChar, theHT["PlaceofDelivery"].ToString());
                        //ClsUtility.AddParameters("@Maturityweeks", SqlDbType.Int, theHT["Maturityweeks"].ToString());
                        //ClsUtility.AddParameters("@Labourduratioin", SqlDbType.Decimal, theHT["Labourduratioin"].ToString());
                        //ClsUtility.AddParameters("@ModeofDelivery", SqlDbType.Int, theHT["ModeofDelivery"].ToString());
                        //ClsUtility.AddParameters("@GenderofBaby", SqlDbType.Int, theHT["GenderofBaby"].ToString());
                        //ClsUtility.AddParameters("@FateofBaby", SqlDbType.Int, theHT["FateofBaby"].ToString());
                                             
                       
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateKNHMEIProfile_Futures", ClsDBUtility.ObjectEnum.DataSet);
                        break;

                    case "ClinicalReview":
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        ClsUtility.AddParameters("@MaternalBloodGroup", SqlDbType.Int, theHT["MaternalBloodGroup"].ToString());
                        ClsUtility.AddParameters("@PartnersBloodGroup", SqlDbType.Int, theHT["PartnersBloodGroup"].ToString());
                        ClsUtility.AddParameters("@HistoryBloodTransfusion", SqlDbType.Int, theHT["HistoryBloodTransfusion"].ToString());
                        ClsUtility.AddParameters("@BloodTransfusiondate", SqlDbType.VarChar, theHT["BloodTransfusiondt"].ToString());
                        ClsUtility.AddParameters("@HistoryOfTwins", SqlDbType.Int, theHT["HistoryOfTwins"].ToString());
                        ClsUtility.AddParameters("@Presentingcomplaints", SqlDbType.VarChar, theHT["Presentingcomplaints"].ToString());
                        ClsUtility.AddParameters("@GeneralAppearance", SqlDbType.VarChar, theHT["GeneralAppearance"].ToString());
                        ClsUtility.AddParameters("@CVS", SqlDbType.VarChar, theHT["CVS"].ToString());
                        ClsUtility.AddParameters("@RS", SqlDbType.VarChar, theHT["RS"].ToString());
                        ClsUtility.AddParameters("@Breasts", SqlDbType.VarChar, theHT["Breasts"].ToString());
                        ClsUtility.AddParameters("@Abdomen", SqlDbType.VarChar, theHT["Abdomen"].ToString());
                        ClsUtility.AddParameters("@VaginalExamination", SqlDbType.VarChar, theHT["VaginalExamination"].ToString());
                        ClsUtility.AddParameters("@discharge", SqlDbType.VarChar, theHT["discharge"].ToString());
                        ClsUtility.AddParameters("@Pallor", SqlDbType.VarChar, theHT["Pallor"].ToString());
                        ClsUtility.AddParameters("@Maturity", SqlDbType.Decimal, theHT["Maturity"].ToString());
                        ClsUtility.AddParameters("@FundalHeight", SqlDbType.VarChar, theHT["FundalHeight"].ToString());
                        ClsUtility.AddParameters("@Presentation", SqlDbType.VarChar, theHT["Presentation"].ToString());
                        ClsUtility.AddParameters("@FoetalHeartRate", SqlDbType.VarChar, theHT["FoetalHeartRate"].ToString());
                        ClsUtility.AddParameters("@Oedema", SqlDbType.VarChar, theHT["Oedema"].ToString());
                        ClsUtility.AddParameters("@Motheratrisk", SqlDbType.Int, theHT["Motheratrisk"].ToString());
                        ClsUtility.AddParameters("@OtherMotheratrisk", SqlDbType.VarChar, theHT["OtherMotheratrisk"].ToString());
                        ClsUtility.AddParameters("@Plan", SqlDbType.VarChar, theHT["Plan"].ToString());
                        ClsUtility.AddParameters("@AppointmentDate", SqlDbType.VarChar, theHT["AppointmentDate"].ToString());
                        ClsUtility.AddParameters("@Admittedtoward", SqlDbType.Int, theHT["Admittedtoward"].ToString());
                        ClsUtility.AddParameters("@DiagnosisandPlanWardAdmitted", SqlDbType.Int, theHT["DiagnosisandPlanWardAdmitted"].ToString());
                        ClsUtility.AddParameters("@ProgressionInWHOstage", SqlDbType.Int, theHT["ProgressionInWHOstage"].ToString());
                        //ClsUtility.AddParameters("@Currentwhostage", SqlDbType.Int, theHT["Currentwhostage"].ToString());
                        //ClsUtility.AddParameters("@WABStage", SqlDbType.Int, theHT["WABStage"].ToString());
                        //ClsUtility.AddParameters("@Mernarche", SqlDbType.Int, theHT["Mernarche"].ToString());
                        //ClsUtility.AddParameters("@MernarcheDate", SqlDbType.VarChar, theHT["MernarcheDate"].ToString());
                        //ClsUtility.AddParameters("@tannerstaging", SqlDbType.Int, theHT["tannerstaging"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        ClsUtility.AddParameters("@RhesusFactor", SqlDbType.Int, theHT["RhesusFactor"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateKNHMEIClinicalReview_Futures", ClsDBUtility.ObjectEnum.DataSet);
                        break;

                    case "PMTCT":
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        ClsUtility.AddParameters("@MothercurrentlyonARV", SqlDbType.Int, theHT["MothercurrentlyonARV"].ToString());
                        ClsUtility.AddParameters("@SpecifyCurrentRegmn", SqlDbType.Int, theHT["SpecifyCurrentRegmn"].ToString());
                        ClsUtility.AddParameters("@SpecifyCurrentRegmnother", SqlDbType.Int, theHT["SpecifyCurrentRegmnother"].ToString());
                        ClsUtility.AddParameters("@mthroncotrimoxazole", SqlDbType.Int, theHT["mthroncotrimoxazole"].ToString());
                        ClsUtility.AddParameters("@MotherCurrentlyonmultivitamins", SqlDbType.Int, theHT["MotherCurrentlyonmultivitamins"].ToString());
                        ClsUtility.AddParameters("@MotherAdherenceAssessmentdone", SqlDbType.Int, theHT["MotherAdherenceAssessmentdone"].ToString());
                        ClsUtility.AddParameters("@Missedanydoses", SqlDbType.Int, theHT["Missedanydoses"].ToString());
                        ClsUtility.AddParameters("@Noofdosesmissed", SqlDbType.Decimal, theHT["Noofdosesmissed"].ToString());
                        ClsUtility.AddParameters("@NofHomevisits", SqlDbType.Int, theHT["NofHomevisits"].ToString());
                        ClsUtility.AddParameters("@PrioritiseHomeVisit", SqlDbType.Int, theHT["PrioritiseHomeVisit"].ToString());
                        ClsUtility.AddParameters("@DOT", SqlDbType.Decimal, theHT["DOT"].ToString());
                        ClsUtility.AddParameters("@disclosedHIVStatus", SqlDbType.Int, theHT["disclosedHIVStatus"].ToString());
                        ClsUtility.AddParameters("@CondomsIssuedYes", SqlDbType.Int, theHT["CondomsIssuedYes"].ToString());
                        ClsUtility.AddParameters("@AdditionalPWPNotes", SqlDbType.VarChar, theHT["AdditionalPWPNotes"].ToString());
                        ClsUtility.AddParameters("@PwpMessageGiven", SqlDbType.Int, theHT["PwpMessageGiven"].ToString());
                        ClsUtility.AddParameters("@ARVRegimen", SqlDbType.Int, theHT["ARVRegimen"].ToString());
                        ClsUtility.AddParameters("@InfantNVPissued", SqlDbType.Int, theHT["InfantNVPissued"].ToString());
                        ClsUtility.AddParameters("@CTX", SqlDbType.Int, theHT["CTX"].ToString());
                        ClsUtility.AddParameters("@CTXOther", SqlDbType.VarChar, theHT["CTXOther"].ToString());
                        ClsUtility.AddParameters("@otherMgmt", SqlDbType.VarChar, theHT["otherMgmt"].ToString());
                        ClsUtility.AddParameters("@PMTCTAppDate", SqlDbType.VarChar, theHT["PMTCTAppDate"].ToString());
                        ClsUtility.AddParameters("@AdmittedtowardPMTCT", SqlDbType.Int, theHT["AdmittedtowardPMTCT"].ToString());
                        ClsUtility.AddParameters("@WardAdmitted", SqlDbType.Int, theHT["WardAdmitted"].ToString());
                        //TB Finding
                        ClsUtility.AddParameters("@TBFindings", SqlDbType.Int, theHT["TBFindings"].ToString());
                        ClsUtility.AddParameters("@ContactsScreenedForTB", SqlDbType.Int, theHT["ContactsScreenedForTB"].ToString());
                        ClsUtility.AddParameters("@SpecifyWhyContactNotScreenedForTB", SqlDbType.VarChar, theHT["txtSpecifyWhyContactNotScreenedForTB"].ToString());
                        ClsUtility.AddParameters("@PatientReferredForTreatment", SqlDbType.Int, theHT["PatientReferredForTreatment"].ToString());

                        ClsUtility.AddParameters("@IPT", SqlDbType.Int, theHT["IPT"].ToString());
                        ClsUtility.AddParameters("@ReasonDeclinedIPT", SqlDbType.Int, theHT["ReasonDeclinedIPT"].ToString());
                        ClsUtility.AddParameters("@OtherReasonDeclinedIPT", SqlDbType.VarChar, theHT["OtherReasonDeclinedIPT"].ToString());
                        if (!String.IsNullOrEmpty(theHT["PyridoxineEndDate"].ToString()))
                            ClsUtility.AddParameters("@PyridoxineEndDate", SqlDbType.VarChar, theHT["PyridoxineEndDate"].ToString());
                        if (!String.IsNullOrEmpty(theHT["INHStartDate"].ToString()))
                            ClsUtility.AddParameters("@INHStartDate", SqlDbType.VarChar, theHT["INHStartDate"].ToString());
                        if (!String.IsNullOrEmpty(theHT["INHEndDate"].ToString()))
                            ClsUtility.AddParameters("@INHEndDate", SqlDbType.VarChar, theHT["INHEndDate"].ToString());
                        if (!String.IsNullOrEmpty(theHT["PyridoxineStartDate"].ToString()))
                            ClsUtility.AddParameters("@PyridoxineStartDate", SqlDbType.VarChar, theHT["PyridoxineStartDate"].ToString());
                        if (theHT["AdherenceAddressed"] != null)
                            ClsUtility.AddParameters("@AdherenceAddressed", SqlDbType.Int, theHT["AdherenceAddressed"].ToString());
                        if (theHT["missedAnyDoses"] != null)
                            ClsUtility.AddParameters("@AnyMissedDoses", SqlDbType.Int, theHT["missedAnyDoses"].ToString());
                        if (theHT["ReferredForAdherence"] != null)
                            ClsUtility.AddParameters("@ReferredForAdherence", SqlDbType.Int, theHT["ReferredForAdherence"].ToString());
                        ClsUtility.AddParameters("@OtherTBSideEffects", SqlDbType.VarChar, theHT["SpecifyOtherTBSideEffects"].ToString());
                        //ClsUtility.AddParameters("@tetanustoxoid", SqlDbType.Int, theHT["tetanustoxoid"].ToString());
                        //ClsUtility.AddParameters("@TetanusVaccineDose", SqlDbType.Int, theHT["tetanustoxoidVaccine"].ToString());
                        //ClsUtility.AddParameters("@TetanusVaccineReason", SqlDbType.Int, theHT["tetanustoxoidVaccineNo"].ToString());
                        ClsUtility.AddParameters("@Currentwhostage", SqlDbType.Int, theHT["Currentwhostage"].ToString());
                        ClsUtility.AddParameters("@WABStage", SqlDbType.Int, theHT["WABStage"].ToString());
                        //ClsUtility.AddParameters("@Mernarche", SqlDbType.Int, theHT["Mernarche"].ToString());
                        ClsUtility.AddParameters("@MernarcheDate", SqlDbType.VarChar, theHT["MernarcheDate"].ToString());
                        ClsUtility.AddParameters("@tannerstaging", SqlDbType.Int, theHT["tannerstaging"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdateKNHMEIPMTCT_Futures", ClsDBUtility.ObjectEnum.DataSet);
                        break;
                }

                //VitalSign Patient Refer To
                if (theDS.Tables["dtVS_Rt"] != null && theDS.Tables["dtVS_Rt"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["dtVS_Rt"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        if (theHT["visitPk"].ToString() == "0" && retval.Tables[0].Rows.Count > 0)
                            ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, retval.Tables[0].Rows[0]["VisitId"].ToString());
                        else
                            ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "VitalSign");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "");
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //GBV Experience
                if (theDS.Tables["GBVExperience"] != null && theDS.Tables["GBVExperience"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["GBVExperience"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["GBVExperienced"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "ExperiencedGBV");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["GBVExperienced_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Substance
                if (theDS.Tables["Substance"] != null && theDS.Tables["Substance"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["Substance"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["SubstanceID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "ExperiencedSubstanceAbuse");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Substance_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Referral
                if (theDS.Tables["Referral"] != null && theDS.Tables["Referral"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["Referral"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ReferralID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "ReferralANC");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Referral_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Prev Pregnancies
                if (theDS.Tables["dtPrevpreg"] != null && theDS.Tables["dtPrevpreg"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["dtPrevpreg"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@YearofBaby", SqlDbType.Int, theDR["YearofBaby"].ToString());
                        ClsUtility.AddParameters("@PlaceOfDelivery", SqlDbType.VarChar, theDR["PlaceOfDelivery"].ToString());
                        ClsUtility.AddParameters("@Maturity", SqlDbType.VarChar, theDR["MaturityId"].ToString());
                        ClsUtility.AddParameters("@LabourHour", SqlDbType.VarChar, theDR["LabourHour"].ToString());
                        ClsUtility.AddParameters("@ModeOfDelivery", SqlDbType.VarChar, theDR["ModeOfDeliveryId"].ToString());
                        ClsUtility.AddParameters("@Gender", SqlDbType.VarChar, theDR["GenderId"].ToString());
                        ClsUtility.AddParameters("@Fate", SqlDbType.VarChar, theDR["FateId"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveKNHMEIPregPregnancies_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //HistoricalIllness
                if (theDS.Tables["HistoricalIllness"] != null && theDS.Tables["HistoricalIllness"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["HistoricalIllness"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["HistoryChronicIllnessID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "ChronicIllnessHistory");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["HistoryChronicIllness_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //Reasonmissdeddose
                if (theDS.Tables["Reasonmissdeddose"] != null && theDS.Tables["Reasonmissdeddose"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["Reasonmissdeddose"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ReasonmissdeddoseID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "AdherenceCodes");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Reasonmissdeddose_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //AdherenceBarriers
                if (theDS.Tables["AdherenceBarriers"] != null && theDS.Tables["AdherenceBarriers"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["AdherenceBarriers"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["BarriertoadherenceID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "AdherenceBarriers");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["Barriertoadherence_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //DisclosedHIVStatusTo
                if (theDS.Tables["DisclosedHIVStatusTo"] != null && theDS.Tables["DisclosedHIVStatusTo"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["DisclosedHIVStatusTo"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["HIVStatusID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "DisclosedHIVStatusTo");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["HIVStatus_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //WHOStage
                if (theDS.Tables["WHOStage"] != null && theDS.Tables["WHOStage"].Rows.Count > 0)
                {

                    foreach (DataRow theDR in theDS.Tables["WHOStage"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ValueID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, theDR["FieldName"].ToString());
                        ClsUtility.AddParameters("@DateField1", SqlDbType.VarChar, theDR["Date1"].ToString());
                        ClsUtility.AddParameters("@DateField2", SqlDbType.VarChar, theDR["Date2"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTMEIWHOStage_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //ARTPreparation
                if (theDS.Tables["ARTPreparation"] != null && theDS.Tables["ARTPreparation"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["ARTPreparation"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ARTPreparationID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "ARTPreparation");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, theDR["ARTPreparation_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                //TBAssessment
                if (theDS.Tables["TBAssessment"] != null && theDS.Tables["TBAssessment"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["TBAssessment"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString().ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "TBAssessmentICF");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "");
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                //IPT Stop Reason
                if (theDS.Tables["TBStopReason"] != null && theDS.Tables["TBStopReason"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["TBStopReason"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int IPTStop = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_IPTStopReason", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }


                //TB Review Checklist
                if (theDS.Tables["TBReview"] != null && theDS.Tables["TBReview"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["TBReview"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int IPTStop = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_TBReviewCheckList", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }

                if (theDS.Tables["TBSignsOfHepatitis"] != null && theDS.Tables["TBSignsOfHepatitis"].Rows.Count > 0)
                {
                    foreach (DataRow theDR in theDS.Tables["TBSignsOfHepatitis"].Rows)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@ValueID", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        ClsUtility.AddParameters("@fieldName", SqlDbType.Int, "SignsOfHepatitis");
                        int SignsHepatitis = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

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
                KNHMEIManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return retval;

        }

        public int SaveKNHMEILabResult(DataTable theDT, int userId, int PatientId, int VisitId)
        {
            ClsObject KNHMEIManager = new ClsObject();
            int retlab = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                KNHMEIManager.Connection = this.Connection;
                KNHMEIManager.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@patientid", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitId.ToString());
                retlab = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_DeleteKNHMEILabResult_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                foreach (DataRow theDR in theDT.Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, PatientId.ToString());
                    ClsUtility.AddParameters("@LabVisitID", SqlDbType.Int, theDR["LabVisitId"].ToString());
                    ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, VisitId.ToString());
                    ClsUtility.AddParameters("@ParameterId", SqlDbType.Int, theDR["ParameterID"].ToString());
                    ClsUtility.AddParameters("@PrevResult", SqlDbType.VarChar, theDR["PrevResult"].ToString());
                    ClsUtility.AddParameters("@PrevResultDate", SqlDbType.VarChar, theDR["PrevResultDate"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());
                    retlab = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_SaveKNHMEILabResult_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                KNHMEIManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
            return retlab;

        }

        public DataSet GetKNHMEIData_Autopopulate(int PatientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, PatientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetAutopopulateDataKNHMEI_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }

        }
    }

}