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
using Application.Common;
using Entities.Common;
using HIVCE.Common;

namespace BusinessProcess.Clinical
{
    [Serializable()]
    public class BKNHMEI : ProcessBase, IKNHMEI
    {
        public BKNHMEI()
        {

        }

        public DataSet GetKNHMEI_Data(int PatientId, int VisitId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, PatientId.ToString());
                ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_KNH_GetPMTCTMEIPatientData",
                    ClsDBUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetKNHMEI_LabResult(int PatientId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, PatientId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_KNH_GetPMTCTMEIPatientLabResult",
                    ClsDBUtility.ObjectEnum.DataSet);
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
                        ClsUtility.AddParameters("@TetanusVaccineDose", SqlDbType.Int,
                            theHT["tetanustoxoidVaccine"].ToString());
                        ClsUtility.AddParameters("@TetanusVaccineReason", SqlDbType.Int,
                            theHT["tetanustoxoidVaccineNo"].ToString());
                        ClsUtility.AddParameters("@Mernarche", SqlDbType.Int, theHT["Mernarche"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_Clinical_SaveUpdateKNHMEITriage_Futures", ClsDBUtility.ObjectEnum.DataSet);
                        break;

                    case "HTC":
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        ClsUtility.AddParameters("@PrevHIVStatus", SqlDbType.Int, theHT["PrevHIVStatus"].ToString());
                        ClsUtility.AddParameters("@PrevPHIVTesting", SqlDbType.Int,
                            theHT["PrevPHIVTesting"].ToString());
                        ClsUtility.AddParameters("@LastHIVTest", SqlDbType.VarChar, theHT["LastHIVTest"].ToString());
                        ClsUtility.AddParameters("@PreTestCounseling", SqlDbType.Int,
                            theHT["PreTestCounseling"].ToString());
                        ClsUtility.AddParameters("@PostTestCounseling", SqlDbType.Int,
                            theHT["PostTestCounseling"].ToString());
                        ClsUtility.AddParameters("@HIVTestingToday", SqlDbType.Int,
                            theHT["HIVTestingToday"].ToString());
                        ClsUtility.AddParameters("@HIVTestingTodayDone", SqlDbType.Int,
                            theHT["HIVTestingTodayDone"].ToString());
                        ClsUtility.AddParameters("@FinalHIVResult", SqlDbType.Int, theHT["FinalHIVResult"].ToString());
                        ClsUtility.AddParameters("@Patientaccompaniedpartner", SqlDbType.Int,
                            theHT["Patientaccompaniedpartner"].ToString());
                        ClsUtility.AddParameters("@partnerpretestcounselling", SqlDbType.Int,
                            theHT["partnerpretestcounselling"].ToString());
                        ClsUtility.AddParameters("@partnerFinalHIVResult", SqlDbType.Int,
                            theHT["partnerFinalHIVResult"].ToString());
                        ClsUtility.AddParameters("@partnerPostTestcounselling", SqlDbType.Int,
                            theHT["partnerPostTestcounselling"].ToString());
                        ClsUtility.AddParameters("@CoupleDiscordant", SqlDbType.Int,
                            theHT["CoupleDiscordant"].ToString());
                        ClsUtility.AddParameters("@HIVTestdonetopartner", SqlDbType.Int,
                            theHT["HIVTestdonetopartner"].ToString());
                        ClsUtility.AddParameters("@PartnersDNAPCRresult", SqlDbType.Int,
                            theHT["PartnersDNAPCRresult"].ToString());
                        ClsUtility.AddParameters("@familyinformationFilled", SqlDbType.Int,
                            theHT["familyinformationFilled"].ToString());
                        ClsUtility.AddParameters("@membersofthefamilybeentested", SqlDbType.Int,
                            theHT["membersofthefamilybeentested"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_Clinical_SaveUpdateKNHMEIHTC_Futures", ClsDBUtility.ObjectEnum.DataSet);
                        break;

                    case "Profile":
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        ClsUtility.AddParameters("@HMHealth", SqlDbType.Int, theHT["HMHealth"].ToString());
                        ClsUtility.AddParameters("@OtherHMHealth", SqlDbType.VarChar,
                            theHT["OtherHMHealth"].ToString());
                        ClsUtility.AddParameters("@CMHealth", SqlDbType.Int, theHT["CMHealth"].ToString());
                        ClsUtility.AddParameters("@OtherCMHealth", SqlDbType.VarChar,
                            theHT["OtherCMHealth"].ToString());
                        ClsUtility.AddParameters("@ExperienceanyGBV", SqlDbType.Int,
                            theHT["ExperienceanyGBV"].ToString());
                        ClsUtility.AddParameters("@HIVSubstanceAbused", SqlDbType.Int,
                            theHT["HIVSubstanceAbused"].ToString());
                        ClsUtility.AddParameters("@Preferedmodeofdelivery", SqlDbType.Int,
                            theHT["Preferedmodeofdelivery"].ToString());
                        ClsUtility.AddParameters("@PreferedSiteDelivery", SqlDbType.VarChar,
                            theHT["PreferedSiteDelivery"].ToString());
                        ClsUtility.AddParameters("@PreferedSiteDeliveryAdditionalnotes", SqlDbType.VarChar,
                            theHT["PreferedSiteDeliveryAdditionalnotes"].ToString());
                        //ClsUtility.AddParameters("@YrofDelivery", SqlDbType.Int, theHT["YrofDelivery"].ToString());
                        //ClsUtility.AddParameters("@PlaceofDelivery", SqlDbType.VarChar, theHT["PlaceofDelivery"].ToString());
                        //ClsUtility.AddParameters("@Maturityweeks", SqlDbType.Int, theHT["Maturityweeks"].ToString());
                        //ClsUtility.AddParameters("@Labourduratioin", SqlDbType.Decimal, theHT["Labourduratioin"].ToString());
                        //ClsUtility.AddParameters("@ModeofDelivery", SqlDbType.Int, theHT["ModeofDelivery"].ToString());
                        //ClsUtility.AddParameters("@GenderofBaby", SqlDbType.Int, theHT["GenderofBaby"].ToString());
                        //ClsUtility.AddParameters("@FateofBaby", SqlDbType.Int, theHT["FateofBaby"].ToString());


                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_Clinical_SaveUpdateKNHMEIProfile_Futures", ClsDBUtility.ObjectEnum.DataSet);
                        break;

                    case "ClinicalReview":
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        ClsUtility.AddParameters("@MaternalBloodGroup", SqlDbType.Int,
                            theHT["MaternalBloodGroup"].ToString());
                        ClsUtility.AddParameters("@PartnersBloodGroup", SqlDbType.Int,
                            theHT["PartnersBloodGroup"].ToString());
                        ClsUtility.AddParameters("@HistoryBloodTransfusion", SqlDbType.Int,
                            theHT["HistoryBloodTransfusion"].ToString());
                        ClsUtility.AddParameters("@BloodTransfusiondate", SqlDbType.VarChar,
                            theHT["BloodTransfusiondt"].ToString());
                        ClsUtility.AddParameters("@HistoryOfTwins", SqlDbType.Int, theHT["HistoryOfTwins"].ToString());
                        ClsUtility.AddParameters("@Presentingcomplaints", SqlDbType.VarChar,
                            theHT["Presentingcomplaints"].ToString());
                        ClsUtility.AddParameters("@GeneralAppearance", SqlDbType.VarChar,
                            theHT["GeneralAppearance"].ToString());
                        ClsUtility.AddParameters("@CVS", SqlDbType.VarChar, theHT["CVS"].ToString());
                        ClsUtility.AddParameters("@RS", SqlDbType.VarChar, theHT["RS"].ToString());
                        ClsUtility.AddParameters("@Breasts", SqlDbType.VarChar, theHT["Breasts"].ToString());
                        ClsUtility.AddParameters("@Abdomen", SqlDbType.VarChar, theHT["Abdomen"].ToString());
                        ClsUtility.AddParameters("@VaginalExamination", SqlDbType.VarChar,
                            theHT["VaginalExamination"].ToString());
                        ClsUtility.AddParameters("@discharge", SqlDbType.VarChar, theHT["discharge"].ToString());
                        ClsUtility.AddParameters("@Pallor", SqlDbType.VarChar, theHT["Pallor"].ToString());
                        ClsUtility.AddParameters("@Maturity", SqlDbType.Decimal, theHT["Maturity"].ToString());
                        ClsUtility.AddParameters("@FundalHeight", SqlDbType.VarChar, theHT["FundalHeight"].ToString());
                        ClsUtility.AddParameters("@Presentation", SqlDbType.VarChar, theHT["Presentation"].ToString());
                        ClsUtility.AddParameters("@FoetalHeartRate", SqlDbType.VarChar,
                            theHT["FoetalHeartRate"].ToString());
                        ClsUtility.AddParameters("@Oedema", SqlDbType.VarChar, theHT["Oedema"].ToString());
                        ClsUtility.AddParameters("@Motheratrisk", SqlDbType.Int, theHT["Motheratrisk"].ToString());
                        ClsUtility.AddParameters("@OtherMotheratrisk", SqlDbType.VarChar,
                            theHT["OtherMotheratrisk"].ToString());
                        ClsUtility.AddParameters("@Plan", SqlDbType.VarChar, theHT["Plan"].ToString());
                        ClsUtility.AddParameters("@AppointmentDate", SqlDbType.VarChar,
                            theHT["AppointmentDate"].ToString());
                        ClsUtility.AddParameters("@Admittedtoward", SqlDbType.Int, theHT["Admittedtoward"].ToString());
                        ClsUtility.AddParameters("@DiagnosisandPlanWardAdmitted", SqlDbType.Int,
                            theHT["DiagnosisandPlanWardAdmitted"].ToString());
                        ClsUtility.AddParameters("@ProgressionInWHOstage", SqlDbType.Int,
                            theHT["ProgressionInWHOstage"].ToString());
                        //ClsUtility.AddParameters("@Currentwhostage", SqlDbType.Int, theHT["Currentwhostage"].ToString());
                        //ClsUtility.AddParameters("@WABStage", SqlDbType.Int, theHT["WABStage"].ToString());
                        //ClsUtility.AddParameters("@Mernarche", SqlDbType.Int, theHT["Mernarche"].ToString());
                        //ClsUtility.AddParameters("@MernarcheDate", SqlDbType.VarChar, theHT["MernarcheDate"].ToString());
                        //ClsUtility.AddParameters("@tannerstaging", SqlDbType.Int, theHT["tannerstaging"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        ClsUtility.AddParameters("@RhesusFactor", SqlDbType.Int, theHT["RhesusFactor"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_Clinical_SaveUpdateKNHMEIClinicalReview_Futures", ClsDBUtility.ObjectEnum.DataSet);
                        break;

                    case "PMTCT":
                        ClsUtility.AddParameters("@patientid", SqlDbType.Int, theHT["PatientId"].ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, theHT["LocationId"].ToString());
                        ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@FieldVisitType", SqlDbType.Int, theHT["FieldVisitType"].ToString());
                        ClsUtility.AddParameters("@MothercurrentlyonARV", SqlDbType.Int,
                            theHT["MothercurrentlyonARV"].ToString());
                        ClsUtility.AddParameters("@SpecifyCurrentRegmn", SqlDbType.Int,
                            theHT["SpecifyCurrentRegmn"].ToString());
                        ClsUtility.AddParameters("@SpecifyCurrentRegmnother", SqlDbType.Int,
                            theHT["SpecifyCurrentRegmnother"].ToString());
                        ClsUtility.AddParameters("@mthroncotrimoxazole", SqlDbType.Int,
                            theHT["mthroncotrimoxazole"].ToString());
                        ClsUtility.AddParameters("@MotherCurrentlyonmultivitamins", SqlDbType.Int,
                            theHT["MotherCurrentlyonmultivitamins"].ToString());
                        ClsUtility.AddParameters("@MotherAdherenceAssessmentdone", SqlDbType.Int,
                            theHT["MotherAdherenceAssessmentdone"].ToString());
                        ClsUtility.AddParameters("@Missedanydoses", SqlDbType.Int, theHT["Missedanydoses"].ToString());
                        ClsUtility.AddParameters("@Noofdosesmissed", SqlDbType.Decimal,
                            theHT["Noofdosesmissed"].ToString());
                        ClsUtility.AddParameters("@NofHomevisits", SqlDbType.Int, theHT["NofHomevisits"].ToString());
                        ClsUtility.AddParameters("@PrioritiseHomeVisit", SqlDbType.Int,
                            theHT["PrioritiseHomeVisit"].ToString());
                        ClsUtility.AddParameters("@DOT", SqlDbType.Decimal, theHT["DOT"].ToString());
                        ClsUtility.AddParameters("@disclosedHIVStatus", SqlDbType.Int,
                            theHT["disclosedHIVStatus"].ToString());
                        ClsUtility.AddParameters("@CondomsIssuedYes", SqlDbType.Int,
                            theHT["CondomsIssuedYes"].ToString());
                        ClsUtility.AddParameters("@AdditionalPWPNotes", SqlDbType.VarChar,
                            theHT["AdditionalPWPNotes"].ToString());
                        ClsUtility.AddParameters("@PwpMessageGiven", SqlDbType.Int,
                            theHT["PwpMessageGiven"].ToString());
                        ClsUtility.AddParameters("@ARVRegimen", SqlDbType.Int, theHT["ARVRegimen"].ToString());
                        ClsUtility.AddParameters("@InfantNVPissued", SqlDbType.Int,
                            theHT["InfantNVPissued"].ToString());
                        ClsUtility.AddParameters("@CTX", SqlDbType.Int, theHT["CTX"].ToString());
                        ClsUtility.AddParameters("@CTXOther", SqlDbType.VarChar, theHT["CTXOther"].ToString());
                        ClsUtility.AddParameters("@otherMgmt", SqlDbType.VarChar, theHT["otherMgmt"].ToString());
                        ClsUtility.AddParameters("@PMTCTAppDate", SqlDbType.VarChar, theHT["PMTCTAppDate"].ToString());
                        ClsUtility.AddParameters("@AdmittedtowardPMTCT", SqlDbType.Int,
                            theHT["AdmittedtowardPMTCT"].ToString());
                        ClsUtility.AddParameters("@WardAdmitted", SqlDbType.Int, theHT["WardAdmitted"].ToString());
                        //TB Finding
                        ClsUtility.AddParameters("@TBFindings", SqlDbType.Int, theHT["TBFindings"].ToString());
                        ClsUtility.AddParameters("@ContactsScreenedForTB", SqlDbType.Int,
                            theHT["ContactsScreenedForTB"].ToString());
                        ClsUtility.AddParameters("@SpecifyWhyContactNotScreenedForTB", SqlDbType.VarChar,
                            theHT["txtSpecifyWhyContactNotScreenedForTB"].ToString());
                        ClsUtility.AddParameters("@PatientReferredForTreatment", SqlDbType.Int,
                            theHT["PatientReferredForTreatment"].ToString());

                        ClsUtility.AddParameters("@IPT", SqlDbType.Int, theHT["IPT"].ToString());
                        ClsUtility.AddParameters("@ReasonDeclinedIPT", SqlDbType.Int,
                            theHT["ReasonDeclinedIPT"].ToString());
                        ClsUtility.AddParameters("@OtherReasonDeclinedIPT", SqlDbType.VarChar,
                            theHT["OtherReasonDeclinedIPT"].ToString());
                        if (!String.IsNullOrEmpty(theHT["PyridoxineEndDate"].ToString()))
                            ClsUtility.AddParameters("@PyridoxineEndDate", SqlDbType.VarChar,
                                theHT["PyridoxineEndDate"].ToString());
                        if (!String.IsNullOrEmpty(theHT["INHStartDate"].ToString()))
                            ClsUtility.AddParameters("@INHStartDate", SqlDbType.VarChar,
                                theHT["INHStartDate"].ToString());
                        if (!String.IsNullOrEmpty(theHT["INHEndDate"].ToString()))
                            ClsUtility.AddParameters("@INHEndDate", SqlDbType.VarChar, theHT["INHEndDate"].ToString());
                        if (!String.IsNullOrEmpty(theHT["PyridoxineStartDate"].ToString()))
                            ClsUtility.AddParameters("@PyridoxineStartDate", SqlDbType.VarChar,
                                theHT["PyridoxineStartDate"].ToString());
                        if (theHT["AdherenceAddressed"] != null)
                            ClsUtility.AddParameters("@AdherenceAddressed", SqlDbType.Int,
                                theHT["AdherenceAddressed"].ToString());
                        if (theHT["missedAnyDoses"] != null)
                            ClsUtility.AddParameters("@AnyMissedDoses", SqlDbType.Int,
                                theHT["missedAnyDoses"].ToString());
                        if (theHT["ReferredForAdherence"] != null)
                            ClsUtility.AddParameters("@ReferredForAdherence", SqlDbType.Int,
                                theHT["ReferredForAdherence"].ToString());
                        ClsUtility.AddParameters("@OtherTBSideEffects", SqlDbType.VarChar,
                            theHT["SpecifyOtherTBSideEffects"].ToString());
                        //ClsUtility.AddParameters("@tetanustoxoid", SqlDbType.Int, theHT["tetanustoxoid"].ToString());
                        //ClsUtility.AddParameters("@TetanusVaccineDose", SqlDbType.Int, theHT["tetanustoxoidVaccine"].ToString());
                        //ClsUtility.AddParameters("@TetanusVaccineReason", SqlDbType.Int, theHT["tetanustoxoidVaccineNo"].ToString());
                        ClsUtility.AddParameters("@Currentwhostage", SqlDbType.Int,
                            theHT["Currentwhostage"].ToString());
                        ClsUtility.AddParameters("@WABStage", SqlDbType.Int, theHT["WABStage"].ToString());
                        //ClsUtility.AddParameters("@Mernarche", SqlDbType.Int, theHT["Mernarche"].ToString());
                        ClsUtility.AddParameters("@MernarcheDate", SqlDbType.VarChar,
                            theHT["MernarcheDate"].ToString());
                        ClsUtility.AddParameters("@tannerstaging", SqlDbType.Int, theHT["tannerstaging"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, theHT["UserId"].ToString());
                        retval = (DataSet)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_Clinical_SaveUpdateKNHMEIPMTCT_Futures", ClsDBUtility.ObjectEnum.DataSet);
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
                            ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int,
                                retval.Tables[0].Rows[0]["VisitId"].ToString());
                        else
                            ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, theHT["visitPk"].ToString());
                        ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["ID"].ToString());
                        ClsUtility.AddParameters("@CodeName", SqlDbType.VarChar, "VitalSign");
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar, "");
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar,
                            theDR["GBVExperienced_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        ClsUtility.AddParameters("@PlaceOfDelivery", SqlDbType.VarChar,
                            theDR["PlaceOfDelivery"].ToString());
                        ClsUtility.AddParameters("@Maturity", SqlDbType.VarChar, theDR["MaturityId"].ToString());
                        ClsUtility.AddParameters("@LabourHour", SqlDbType.VarChar, theDR["LabourHour"].ToString());
                        ClsUtility.AddParameters("@ModeOfDelivery", SqlDbType.VarChar,
                            theDR["ModeOfDeliveryId"].ToString());
                        ClsUtility.AddParameters("@Gender", SqlDbType.VarChar, theDR["GenderId"].ToString());
                        ClsUtility.AddParameters("@Fate", SqlDbType.VarChar, theDR["FateId"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_Clinical_SaveKNHMEIPregPregnancies_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar,
                            theDR["HistoryChronicIllness_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar,
                            theDR["Reasonmissdeddose_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar,
                            theDR["Barriertoadherence_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_KNHPMTCTMEIWHOStage_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        ClsUtility.AddParameters("@OtherNotes", SqlDbType.VarChar,
                            theDR["ARTPreparation_Other"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHT["UserId"].ToString());
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        int temp = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_KNHPMTCTHEI_SavecheckedlistItems", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        int IPTStop = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_Clinical_SaveUpdate_IPTStopReason", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        int IPTStop = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_Clinical_SaveUpdate_TBReviewCheckList", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                        int SignsHepatitis = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                            "pr_Clinical_SaveUpdate_MultiSelect", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

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
                retlab = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                    "pr_Clinical_DeleteKNHMEILabResult_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

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
                    retlab = (int)KNHMEIManager.ReturnObject(ClsUtility.theParams,
                        "pr_Clinical_SaveKNHMEILabResult_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
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
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams,
                    "pr_Clinical_GetAutopopulateDataKNHMEI_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetKNHANC_Data(int VisitId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@VisitId", SqlDbType.Int, VisitId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)UserManager.ReturnObject(ClsUtility.theParams, "pr_KNH_GetPMTCTANCLastVisitID",
                    ClsDBUtility.ObjectEnum.DataSet);
            }

        }

        public ANC GetANCData(int ptn_pk, int visitPK, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BKNHMEI.GetANCData() method called");
            ANC anc = new ANC();
            DataSet data = new DataSet();
            ClsObject clsObjTP = new ClsObject();

            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitPK.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                data = (DataSet)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_ANC_GetProfileData",
                    ClsDBUtility.ObjectEnum.DataSet);

                /* Chronic Condition*/
                if (data.Tables[0].Rows.Count > 0)
                {
                    anc.ChronicCondition = (from dt in data.Tables[0].AsEnumerable()
                                            select new CodeDeCodeTables()
                                            {
                                                DeCodeId = dt.Field<int>("Id"),
                                                DeCodeName = dt.Field<string>("NAME")
                                            }).ToList();
                }

                if (data.Tables[1].Rows.Count > 0)
                {
                    anc.environmentalAllergen = (from dt in data.Tables[1].AsEnumerable()
                            .Where(o => o.Field<string>("CName") == "Environmental").ToList()
                                                 select new MultiTableList()
                                                 {
                                                     Id = dt.Field<int>("id"),
                                                     Name = dt.Field<string>("NAME")
                                                 }).ToList();

                    anc.foodAllergen = (from dt in data.Tables[1].AsEnumerable()
                            .Where(o => o.Field<string>("CName") == "Food").ToList()
                                        select new MultiTableList()
                                        {
                                            Id = dt.Field<int>("id"),
                                            Name = dt.Field<string>("NAME")
                                        }).ToList();

                    anc.Severity = (from dt in data.Tables[1].AsEnumerable()
                            .Where(o => o.Field<string>("CName") == "Severity").ToList()
                                    select new MultiTableList()
                                    {
                                        Id = dt.Field<int>("id"),
                                        Name = dt.Field<string>("NAME")
                                    }).ToList();
                }

                /* AllergyType*/
                if (data.Tables[2].Rows.Count > 0)
                {
                    anc.AllergyType = (from dt in data.Tables[2].AsEnumerable()
                                       select new MultiTableList()
                                       {
                                           Id = dt.Field<int>("codeid"),
                                           Name = dt.Field<string>("NAME")
                                       }).ToList();
                }

                /*Allergen*/
                if (data.Tables[3].Rows.Count > 0)
                {
                    anc.Allergen = (from dt in data.Tables[3].AsEnumerable()
                                    select new MultiTableList()
                                    {
                                        Id = dt.Field<int>("drug_pk"),
                                        Name = dt.Field<string>("drugname")
                                    }).ToList();
                }

                if (data.Tables[4].Rows.Count > 0)
                {
                    anc.Collections = (from dt in data.Tables[4].AsEnumerable().ToList()
                                       select new MultiTableList()
                                       {
                                           Id = dt.Field<int>("id"),
                                           Name = dt.Field<string>("NAME"),
                                           CName = dt.Field<string>("CName"),
                                       }).ToList();
                }

                /* Ord Visit */
                if (data.Tables[5].Rows.Count > 0)
                {
                    anc.OrdVisit = new OrdVisit();
                    foreach (DataRow row in data.Tables[5].Rows)
                    {
                        anc.OrdVisit.Visit_Id = Convert.ToInt32(row["Visit_Id"]);
                        anc.OrdVisit.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"]);
                        anc.OrdVisit.LocationId = Convert.ToInt32(row["LocationID"]);
                        anc.OrdVisit.VisitDate = string.IsNullOrEmpty(row["visitdate"].ToString()) == false
                            ? Convert.ToDateTime(row["visitdate"].ToString())
                            : (DateTime?)null;
                        anc.OrdVisit.VisitTypeId = Convert.ToInt32(row["visittype"]);
                        anc.OrdVisit.CreatedDate = string.IsNullOrEmpty(row["CreateDate"].ToString()) == false
                            ? Convert.ToDateTime(row["CreateDate"].ToString())
                            : (DateTime?)null;
                        anc.OrdVisit.UpdatedDate = string.IsNullOrEmpty(row["updatedate"].ToString()) == false
                            ? Convert.ToDateTime(row["updatedate"].ToString())
                            : (DateTime?)null;
                        anc.OrdVisit.TypeOfVisit = Convert.ToInt32(row["TypeOfVisit"]);
                        anc.OrdVisit.Signature = string.IsNullOrEmpty(row["Signature"].ToString()) == false
                            ? Convert.ToInt32(Convert.ToInt32(row["Signature"]))
                            : 0;
                        anc.HistoryBloodTransfusion =
                            string.IsNullOrEmpty(row["HistoryBloodTransfusion"].ToString()) == false
                                ? Convert.ToInt32(row["HistoryBloodTransfusion"].ToString())
                                : 0;
                        anc.SurgicalHistory = row["SurgicalHistory"].ToString();


                    }
                }

                DataTable dtData = new DataTable();

                if (data.Tables[6].Rows.Count > 0)
                {
                    dtData = data.Tables[6];

                    anc.OBSnGYNHistory.Ptn_pk = Convert.ToInt32(dtData.Rows[0]["Ptn_Pk"].ToString());
                    anc.OBSnGYNHistory.LocationId = Convert.ToInt32(dtData.Rows[0]["LocationId"].ToString());

                    if (dtData.Rows[0]["Menarche"].ToString().Length > 0)
                    {
                        anc.OBSnGYNHistory.Menarche = Convert.ToInt32(dtData.Rows[0]["Menarche"]);
                    }

                }

                if (data.Tables[7].Rows.Count > 0)
                {
                    dtData = data.Tables[7];
                    if (dtData.Rows[0]["LMP"].ToString().Length > 0)
                    {
                        anc.OBSnGYNHistory.LMP = Convert.ToDateTime(dtData.Rows[0]["LMP"].ToString());
                    }
                    if (dtData.Rows[0]["EDD"].ToString().Length > 0)
                    {
                        anc.OBSnGYNHistory.EDD = Convert.ToDateTime(dtData.Rows[0]["EDD"].ToString());
                    }
                }

                if (data.Tables[8].Rows.Count > 0)
                {
                    dtData = data.Tables[8];
                    if (dtData.Rows[0]["Gravidae"].ToString().Length > 0)
                    {
                        anc.OBSnGYNHistory.Gravidae = Convert.ToInt32(dtData.Rows[0]["Gravidae"].ToString());
                    }
                    if (dtData.Rows[0]["Parity"].ToString().Length > 0)
                    {
                        anc.OBSnGYNHistory.Parity = Convert.ToInt32(dtData.Rows[0]["Parity"].ToString());
                    }

                    if (dtData.Rows[0]["GestAge"].ToString().Length > 0)
                    {
                        anc.GestAge = dtData.Rows[0]["GestAge"].ToString();
                    }
                }

                /*Allergies   */
                if (data.Tables[9].Rows.Count > 0)
                {
                    anc.AllergiesDetail = new List<AllergiesDetail>();
                    foreach (DataRow row in data.Tables[9].Rows)
                    {
                        AllergiesDetail dt = new AllergiesDetail();
                        dt.Id = Convert.ToInt32(row["id"].ToString());
                        dt.Ptn_Pk = Convert.ToInt32(row["Ptn_Pk"].ToString());
                        dt.AllergyTypeDesc = row["AllergyTypeDesc"].ToString();
                        dt.AllergyTypeID = row["AllergyTypeID"].ToString();
                        dt.Allergen = row["Allergen"].ToString();
                        dt.AllergenDesc = row["AllergenDesc"].ToString();
                        dt.OtherAllergen = row["OtherAllergen"].ToString();
                        dt.TypeReaction = row["TypeReaction"].ToString();
                        dt.SeverityDesc = row["severityDesc"].ToString();
                        dt.SevrityTypeID = Convert.ToInt32(row["SevrityTypeID"].ToString());
                        dt.DateAllergy = string.IsNullOrEmpty(row["DateAllergy"].ToString()) == false
                            ? Convert.ToDateTime(row["DateAllergy"].ToString())
                            : DateTime.Now;
                        dt.Comment = row["AllergiesComment"].ToString();
                        dt.UserID = Convert.ToInt32(row["UserID"].ToString());
                        dt.DeleteFlag = Convert.ToInt32(row["DeleteFlag"].ToString());
                        dt.CreateDate = Convert.ToDateTime(row["CreateDate"].ToString());
                        dt.UpdateDate = string.IsNullOrEmpty(row["UpdateDate"].ToString()) == false
                            ? Convert.ToDateTime(row["UpdateDate"].ToString())
                            : DateTime.Now;
                        anc.AllergiesDetail.Add(dt);
                    }

                }

                /* Chronic Condition*/
                if (data.Tables[10].Rows.Count > 0)
                {
                    anc.FamilyHistoryOf = (from dt in data.Tables[10].AsEnumerable()
                                           select new CodeDeCodeTables()
                                           {
                                               DeCodeId = dt.Field<int>("Id"),
                                               DeCodeName = dt.Field<string>("NAME")
                                           }).ToList();
                }

                if (data.Tables[11].Rows.Count > 0)
                {

                    DataView theCodeDV = new DataView(data.Tables[11]);
                    theCodeDV.RowFilter = "FieldName = 'chroniccondition'";
                    DataTable selectedTable = (DataTable)SerializerUtil.CreateTableFromDataView(theCodeDV);

                    if (selectedTable.Rows.Count > 0)
                    {
                        anc.CDT = (from dt in selectedTable.AsEnumerable()
                                   select new MultiSelectTable()
                                   {
                                       Ptn_pk = dt.Field<int>("Ptn_pk"),
                                       Visit_Pk = dt.Field<int>("Visit_Pk"),
                                       ValueID = dt.Field<int>("ValueID"),
                                       FieldName = dt.Field<string>("FieldName"),
                                       OnSetDate = dt.Field<DateTime?>("OnSetDate"),
                                       NumericField = dt.Field<int>("NumericField")
                                   }).ToList();
                    }
                }

                /*Previous Pregnancy   */
                if (data.Tables[12].Rows.Count > 0)
                {
                    anc.PregnancyDetails = new List<PregnancyDetail>();

                    foreach (DataRow row in data.Tables[12].Rows)
                    {
                        PregnancyDetail pd = new PregnancyDetail();

                        pd.Id = Convert.ToInt32(row["ID"].ToString());
                        pd.Ptn_pk = Convert.ToInt32(row["ptn_pk"].ToString());
                        pd.Visit_Id = Convert.ToInt32(row["visit_Pk"].ToString());
                        pd.YearofBaby = string.IsNullOrEmpty(row["YearofBaby"].ToString()) == false
                            ? Convert.ToInt32(row["YearofBaby"].ToString())
                            : 0;
                        pd.PlaceOfDelivery = row["PlaceOfDelivery"].ToString();
                        pd.MaturityId = string.IsNullOrEmpty(row["MaturityId"].ToString()) == false
                            ? Convert.ToInt32(row["MaturityId"].ToString())
                            : 0;
                        pd.Maturity = row["Maturity"].ToString();
                        pd.LabourHour = string.IsNullOrEmpty(row["LabourHour"].ToString()) == false
                            ? Convert.ToDecimal(row["LabourHour"].ToString())
                            : (decimal?)0.0;
                        pd.ModeOfDeliveryId = string.IsNullOrEmpty(row["ModeOfDeliveryId"].ToString()) == false
                            ? Convert.ToInt32(row["ModeOfDeliveryId"].ToString())
                            : 0;
                        pd.ModeOfDelivery = row["ModeOfDelivery"].ToString();
                        pd.GenderId = string.IsNullOrEmpty(row["GenderId"].ToString()) == false
                            ? Convert.ToInt32(row["GenderId"].ToString())
                            : 0;
                        pd.Gender = row["Gender"].ToString();
                        pd.FateId = string.IsNullOrEmpty(row["FateId"].ToString()) == false
                            ? Convert.ToInt32(row["FateId"].ToString())
                            : 0;
                        pd.Fate = row["Fate"].ToString();
                        pd.CreatedDate = string.IsNullOrEmpty(row["CreateDate"].ToString()) == false
                            ? Convert.ToDateTime(row["CreateDate"].ToString())
                            : (DateTime?)null;
                        pd.UpdatedDate = string.IsNullOrEmpty(row["UpdateDate"].ToString()) == false
                            ? Convert.ToDateTime(row["UpdateDate"].ToString())
                            : (DateTime?)null;
                        pd.UserId = string.IsNullOrEmpty(row["UserId"].ToString()) == false
                            ? Convert.ToInt32(row["UserId"].ToString())
                            : 0;
                        pd.Birthweight = string.IsNullOrEmpty(row["Birthweight"].ToString()) == false
                            ? Convert.ToDecimal(row["Birthweight"].ToString())
                            : (decimal?)0.0;

                        anc.PregnancyDetails.Add(pd);
                    }

                }

                if (data.Tables[13].Rows.Count > 0)
                {
                    anc.SelectedFamilyHistoryOf = (from dt in data.Tables[13].AsEnumerable().ToList()
                                                   select new DBMultiSelectSelcted()
                                                   {
                                                       id = dt.Field<int>("ValueID")
                                                   }).ToList();
                }

            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BKNHMEI.GetANCData() Method:" + ex.ToString());
            }
            return anc;
        }


        public bool SaveUpdateProfile(ANC obj, int userId, int locationId, out int visitId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "ANC.SaveUpdateProfile() method called");
            bool flag = true;
            visitId = 0;
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
                ClsUtility.AddParameters("@VisitDate", SqlDbType.DateTime,
                    obj.OrdVisit.VisitDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss"));

                ClsUtility.AddParameters("@FieldVisitType", SqlDbType.Int, obj.OrdVisit.VisitTypeId.ToString());

                ClsUtility.AddParameters("@Menarche", SqlDbType.Int, obj.OBSnGYNHistory.Menarche.ToString());

                dtVal = DBNull.Value.ToString();
                if (obj.OBSnGYNHistory.LMP.HasValue)
                {
                    dtVal = obj.OBSnGYNHistory.LMP.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@LMP", SqlDbType.DateTime, dtVal);
                dtVal = DBNull.Value.ToString();
                if (obj.OBSnGYNHistory.EDD.HasValue)
                {
                    dtVal = obj.OBSnGYNHistory.EDD.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@EDD", SqlDbType.DateTime, dtVal);

                ClsUtility.AddParameters("@Gravidae", SqlDbType.Int, obj.OBSnGYNHistory.Gravidae.ToString());
                ClsUtility.AddParameters("@Parity", SqlDbType.Int, obj.OBSnGYNHistory.Parity.ToString());
                ClsUtility.AddParameters("@Gestation", SqlDbType.VarChar, obj.GestAge.ToString());

                ClsUtility.AddParameters("@SurgicalHistory", SqlDbType.Int, obj.SurgicalHistory.ToString());
                ClsUtility.AddParameters("@HistoryBloodTransfusion", SqlDbType.Int,
                    obj.HistoryBloodTransfusion.ToString());


                DataTable ReturnDT = new DataTable();

                ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_ANC_SaveProfileData",
                    ClsDBUtility.ObjectEnum.DataTable);

                visitId = (int)ReturnDT.Rows[0]["VisitId"];
                obj.Visit_Id = visitId;

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

                    ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_SaveMultiSelectline",
                        ClsDBUtility.ObjectEnum.DataTable);
                    if (deleteAll == 1)
                        deleteAll = 0;

                }

                /* Allergies Details */
                deleteAll = 1;
                foreach (AllerH mst in obj.AllerH)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_pk.ToString());
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

                    int retval = (Int32)clsObjTP.ReturnObjectNewImpl(ClsUtility.theParams,
                        "Pr_HIVCE_SaveAllergiesDetails", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    if (deleteAll == 1)
                        deleteAll = 0;
                }

                deleteAll = 1;
                foreach (DBMultiSelectSelcted mst in obj.SelectedFamilyHistoryOf)
                {
                    if (mst.id != 0)
                    {
                        int vid = obj.Visit_Id;
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@DeleteAll", SqlDbType.Int, deleteAll.ToString());
                        ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                        ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, vid.ToString());
                        ClsUtility.AddParameters("@ID", SqlDbType.Int, mst.id.ToString());
                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, "FamilyHistoryOf");

                        ReturnDT = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams,
                            "Pr_HIVCE_SaveMultiSelectline", ClsDBUtility.ObjectEnum.DataTable);
                        if (deleteAll == 1)
                            deleteAll = 0;
                    }
                }

                //Pregnancies

                foreach (PregnancyDetail mst in obj.PregnancyDetails)
                {
                    int vid = mst.Visit_Id;
                    if (vid == 0)
                    {
                        vid = obj.Visit_Id;
                    }

                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@patientid", SqlDbType.Int, obj.Ptn_pk.ToString());
                    ClsUtility.AddParameters("@Visit_ID", SqlDbType.Int, vid.ToString());
                    ClsUtility.AddParameters("@YearofBaby", SqlDbType.Int, mst.YearofBaby.ToString());
                    ClsUtility.AddParameters("@PlaceOfDelivery", SqlDbType.VarChar, mst.PlaceOfDelivery.ToString());
                    ClsUtility.AddParameters("@Maturity", SqlDbType.VarChar, mst.MaturityId.ToString());
                    ClsUtility.AddParameters("@LabourHour", SqlDbType.VarChar, mst.LabourHour.ToString());
                    ClsUtility.AddParameters("@ModeOfDelivery", SqlDbType.VarChar, mst.ModeOfDeliveryId.ToString());
                    ClsUtility.AddParameters("@Gender", SqlDbType.VarChar, mst.GenderId.ToString());
                    ClsUtility.AddParameters("@Fate", SqlDbType.VarChar, mst.FateId.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userId.ToString());
                    ClsUtility.AddParameters("@BirthWeight", SqlDbType.Int, mst.Birthweight.ToString());
                    int temp = (int)clsObjTP.ReturnObject(ClsUtility.theParams,
                        "pr_Clinical_SaveKNHMEIPregPregnancies_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);

                if (visitId > 0)
                    flag = true;
                else
                    flag = false;
            }
            catch (Exception ex)
            {
                DataMgr.RollBackTransation(this.Transaction);
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BKNHMEI.SaveUpdateProfile() Method:" + ex.ToString());

            }

            finally
            {

                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

            return flag;
        }

        public ANCScreeningTests GetANCScreeningTestsData(int ptn_pk, int locationId, int visitId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BKNHMEI.GetANCScreeningTestsCData() method called");
            ANCScreeningTests ancsr = new ANCScreeningTests();
            DataSet data = new DataSet();
            ClsObject clsObjTP = new ClsObject();

            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, ptn_pk.ToString());
                data = (DataSet)clsObjTP.ReturnObject(ClsUtility.theParams, "pr_KNH_GetPMTCTMEIPatientLabResult",
                    ClsDBUtility.ObjectEnum.DataSet);

                ancsr.Collections = new List<MultiTableList>();

                ancsr.Ptn_pk = ptn_pk;
                ancsr.LocationId = locationId;
                ancsr.Visit_Id = visitId;

                if (data.Tables[0].Rows.Count > 0)
                {
                    ancsr.LabResults = new List<LabResult>();

                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        LabResult lr = new LabResult();
                        lr.Ptn_pk = Convert.ToInt32(row["Ptn_Pk"]);
                        lr.Visit_Id = Convert.ToInt32(row["VisitId"]);
                        lr.ParameterId = Convert.ToInt32(row["ParameterID"]);
                        lr.SubTestName = row["SubTestName"].ToString();
                        lr.TestDate = string.IsNullOrEmpty(row["TestDate"].ToString()) == false
                            ? Convert.ToDateTime(row["TestDate"].ToString())
                            : (DateTime?)null;
                        lr.Result = row["Result"].ToString();
                        lr.Order = Convert.ToInt32(row["Order"]);
                        lr.TestResults = string.IsNullOrEmpty(row["TestResults"].ToString()) == false
                            ? Convert.ToInt32(Convert.ToInt32(row["TestResults"]))
                            : (decimal?)0.0;
                        lr.TestResultDescription = row["TestResults1"].ToString();
                        ancsr.LabResults.Add(lr);
                    }
                }

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitId.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                data = (DataSet)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_ANC_GetScreeningTestsData",
                    ClsDBUtility.ObjectEnum.DataSet);


                if (data.Tables[0].Rows.Count > 0)
                {

                    ancsr.Collections = (from dtColl in data.Tables[0].AsEnumerable().ToList()
                                         select new MultiTableList()
                                         {
                                             Id = dtColl.Field<int>("id"),
                                             Name = dtColl.Field<string>("NAME"),
                                             CName = dtColl.Field<string>("CName"),
                                         }).ToList();
                }

                ancsr.HIVTesting = new HIVTesting();
                DataTable dt = new DataTable();

                if (data.Tables[1].Rows.Count > 0)
                {

                    dt = data.Tables[1];

                    ancsr.HIVTesting.Visit_Id = Convert.ToInt32(dt.Rows[0]["Visit_pk"]);
                    ancsr.HIVTesting.Ptn_pk = Convert.ToInt32(dt.Rows[0]["Ptn_pk"]);
                    ancsr.HIVTesting.LocationId = Convert.ToInt32(dt.Rows[0]["LocationID"]);
                    ancsr.HIVTesting.VisitNumber = string.IsNullOrEmpty(dt.Rows[0]["VisitNumber"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["VisitNumber"].ToString()) : 0;
                    ancsr.HIVTesting.HIVTestingToday = string.IsNullOrEmpty(dt.Rows[0]["HIVTestingToday"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["HIVTestingToday"].ToString()) : 0;
                    ancsr.HIVTesting.HIVTestingTodayDone = string.IsNullOrEmpty(dt.Rows[0]["HIVTestingTodayDone"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["HIVTestingTodayDone"].ToString()) : 0;
                    ancsr.HIVTesting.HIVTestingPoint2 = string.IsNullOrEmpty(dt.Rows[0]["HIVTestingPoint2"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["HIVTestingPoint2"].ToString()) : 0;
                    ancsr.HIVTesting.PretestCounselling = string.IsNullOrEmpty(dt.Rows[0]["PretestCounselling"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["PretestCounselling"].ToString()) : 0;
                    ancsr.HIVTesting.FinalHIVResult = string.IsNullOrEmpty(dt.Rows[0]["FinalHIVResult"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["FinalHIVResult"].ToString()) : 0;
                    ancsr.HIVTesting.PartnerTestedHIV = string.IsNullOrEmpty(dt.Rows[0]["PartnerTestedHIV"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["PartnerTestedHIV"].ToString()) : 0;
                    ancsr.HIVTesting.PostTestCounselling = string.IsNullOrEmpty(dt.Rows[0]["PostTestCounselling"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["PostTestCounselling"].ToString()) : 0;
                    ancsr.HIVTesting.PartnerInvolvement = string.IsNullOrEmpty(dt.Rows[0]["PartnerInvolvement"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["PartnerInvolvement"].ToString()) : 0;
                    ancsr.HIVTesting.PartnerPreTestCounselling = string.IsNullOrEmpty(dt.Rows[0]["PartnerPreTestCounselling"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["PartnerPreTestCounselling"].ToString()) : 0;
                    ancsr.HIVTesting.FinalHIVResultPartner = string.IsNullOrEmpty(dt.Rows[0]["FinalHIVResultPartner"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["FinalHIVResultPartner"].ToString()) : 0;
                    ancsr.HIVTesting.PartnerPostTestCounselling = string.IsNullOrEmpty(dt.Rows[0]["PartnerPostTestCounselling"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["PartnerPostTestCounselling"].ToString()) : 0;
                    ancsr.HIVTesting.DiscordantCouple = string.IsNullOrEmpty(dt.Rows[0]["DiscordantCouple"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["DiscordantCouple"].ToString()) : 0;
                    ancsr.HIVTesting.PCRpartner = string.IsNullOrEmpty(dt.Rows[0]["PCRpartner"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["PCRpartner"].ToString()) : 0;
                    ancsr.HIVTesting.DeleteFlag = string.IsNullOrEmpty(dt.Rows[0]["DeleteFlag"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["DeleteFlag"].ToString()) : 0;
                    ancsr.HIVTesting.UserID = string.IsNullOrEmpty(dt.Rows[0]["UserID"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["UserID"].ToString()) : 0;
                    ancsr.HIVTesting.CreatedDate = string.IsNullOrEmpty(dt.Rows[0]["CreateDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["CreateDate"].ToString()) : (DateTime?)null;
                    ancsr.HIVTesting.UpdatedDate = string.IsNullOrEmpty(dt.Rows[0]["UpdateDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["UpdateDate"].ToString()) : (DateTime?)null;
                }

                dt = new DataTable();
                if (data.Tables[2].Rows.Count > 0)
                {
                    dt = data.Tables[2];

                    ancsr.HIVTesting.MotherHIVTestResult = string.IsNullOrEmpty(dt.Rows[0]["MotherHIVTestResult"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["MotherHIVTestResult"].ToString()) : 0;
                    ancsr.HIVTesting.MotherHIVTestDate = string.IsNullOrEmpty(dt.Rows[0]["MotherHIVTestDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["MotherHIVTestDate"].ToString()) : (DateTime?)null;
                }


            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BKNHMEI.GetANCScreeningTestsCData() Method:" + ex.ToString());
            }
            return ancsr;
        }

        public bool SaveUpdateScreeningTestsData(ANCScreeningTests obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BKNHMEI.SaveUpdateScreeningTestsData() method called");
            bool flag = true;
            ClsObject clsObj = new ClsObject();
            DataTable ReturnDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObj.Connection = this.Connection;
                clsObj.Transaction = this.Transaction;


                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationId.ToString());
                ClsUtility.AddParameters("@HIVTestingToday", SqlDbType.Int, obj.HIVTesting.HIVTestingToday.Value.ToString());

                ClsUtility.AddParameters("@HIVTestingTodayDone", SqlDbType.Int, obj.HIVTesting.HIVTestingTodayDone.Value.ToString());
                ClsUtility.AddParameters("@HIVTestingPoint2", SqlDbType.Int, obj.HIVTesting.HIVTestingPoint2.Value.ToString());
                ClsUtility.AddParameters("@PretestCounselling", SqlDbType.Int, obj.HIVTesting.PretestCounselling.Value.ToString());
                ClsUtility.AddParameters("@FinalHIVResult", SqlDbType.Int, obj.HIVTesting.FinalHIVResult.Value.ToString());
                ClsUtility.AddParameters("@PartnerTestedHIV", SqlDbType.Int, obj.HIVTesting.PartnerTestedHIV.Value.ToString());
                ClsUtility.AddParameters("@PostTestCounselling", SqlDbType.Int, obj.HIVTesting.PostTestCounselling.Value.ToString());
                ClsUtility.AddParameters("@PartnerInvolvement", SqlDbType.Int, obj.HIVTesting.PartnerInvolvement.Value.ToString());
                ClsUtility.AddParameters("@PartnerPreTestCounselling", SqlDbType.Int, obj.HIVTesting.PartnerPreTestCounselling.Value.ToString());
                ClsUtility.AddParameters("@FinalHIVResultPartner", SqlDbType.Int, obj.HIVTesting.FinalHIVResultPartner.Value.ToString());
                ClsUtility.AddParameters("@PartnerPostTestCounselling", SqlDbType.Int, obj.HIVTesting.PartnerPostTestCounselling.Value.ToString());
                ClsUtility.AddParameters("@DiscordantCouple", SqlDbType.Int, obj.HIVTesting.DiscordantCouple.Value.ToString());
                ClsUtility.AddParameters("@PCRpartner", SqlDbType.Int, obj.HIVTesting.PCRpartner.Value.ToString());
                ClsUtility.AddParameters("@MotherHIVTestResult", SqlDbType.Int, obj.HIVTesting.MotherHIVTestResult.Value.ToString());

                string dtVal = DBNull.Value.ToString();
                if (obj.HIVTesting.MotherHIVTestDate.HasValue)
                {
                    dtVal = obj.HIVTesting.MotherHIVTestDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                int delFlag = 0;
                if (obj.HIVTesting.DeleteFlag.HasValue)
                {
                    delFlag = obj.HIVTesting.DeleteFlag.Value;
                }
                ClsUtility.AddParameters("@MotherHIVTestDate", SqlDbType.DateTime, dtVal);

                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, delFlag.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());

                ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_ANC_SaveUpdateScreeningTestsData", ClsDBUtility.ObjectEnum.DataTable);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }
            catch (Exception ex)
            {
                DataMgr.RollBackTransation(this.Transaction);
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BKNHMEI.SaveUpdateScreeningTestsData() Method:" + ex.ToString());
                //throw ex;
            }
            finally
            {

                clsObj = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            //return ReturnDT;
            return flag;
        }

        public ANCVisitDetails GetANCVisitDetailsData(int ptn_pk, int locationId, int visitId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BKNHMEI.GetANCVisitDetailsData() method called");
            ANCVisitDetails ancvd = new ANCVisitDetails();
            DataSet data = new DataSet();
            ClsObject clsObjTP = new ClsObject();

            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitId.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, locationId.ToString());
                data = (DataSet)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_ANC_GetVisitDetailsData",
                    ClsDBUtility.ObjectEnum.DataSet);

                ancvd.Collections = new List<MultiTableList>();

                ancvd.Ptn_pk = ptn_pk;
                ancvd.LocationId = locationId;
                ancvd.Visit_Id = visitId;

                DataTable dt = new DataTable();
                ancvd.VisitDetails = new VisitDetails();

                if (data.Tables[0].Rows.Count > 0)
                {

                    dt = data.Tables[0];

                    ancvd.VisitDetails.FirstVisitDate = string.IsNullOrEmpty(dt.Rows[0]["FirstVisitDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["FirstVisitDate"].ToString()) : (DateTime?)null;
                    ancvd.VisitDetails.IPTP = string.IsNullOrEmpty(dt.Rows[0]["IPTP"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["IPTP"].ToString()) : 0;
                    ancvd.VisitDetails.IPTPDateGiven = string.IsNullOrEmpty(dt.Rows[0]["IPTPDateGiven"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["IPTPDateGiven"].ToString()) : (DateTime?)null;
                    ancvd.VisitDetails.InsectisideTreatedNet = string.IsNullOrEmpty(dt.Rows[0]["InsectisideTreatedNet"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["InsectisideTreatedNet"].ToString()) : 0;
                    ancvd.VisitDetails.InsectisideTreatedNetDT = string.IsNullOrEmpty(dt.Rows[0]["InsectisideTreatedNetDT"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["InsectisideTreatedNetDT"].ToString()) : (DateTime?)null;
                    ancvd.VisitDetails.Dewormed = string.IsNullOrEmpty(dt.Rows[0]["Dewormed"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["Dewormed"].ToString()) : 0;
                    ancvd.VisitDetails.DewormedDateGiven = string.IsNullOrEmpty(dt.Rows[0]["DewormedDateGiven"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["DewormedDateGiven"].ToString()) : (DateTime?)null;
                    ancvd.VisitDetails.IronAndFolate = string.IsNullOrEmpty(dt.Rows[0]["IronAndFolate"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["IronAndFolate"].ToString()) : 0;
                    ancvd.VisitDetails.IronAndFolateDateGiven = string.IsNullOrEmpty(dt.Rows[0]["IronAndFolateDateGiven"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["IronAndFolateDateGiven"].ToString()) : (DateTime?)null;
                    ancvd.VisitDetails.InfantFeedingCounselling = string.IsNullOrEmpty(dt.Rows[0]["InfantFeedingCounselling"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["InfantFeedingCounselling"].ToString()) : 0;
                    ancvd.VisitDetails.ExclusiveBreastfeeding = string.IsNullOrEmpty(dt.Rows[0]["ExclusiveBreastfeeding"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["ExclusiveBreastfeeding"].ToString()) : 0;
                    ancvd.VisitDetails.HIVIInfantFeedingOption = string.IsNullOrEmpty(dt.Rows[0]["HIVIInfantFeedingOption"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["HIVIInfantFeedingOption"].ToString()) : 0;
                    ancvd.VisitDetails.MothersDecisionIF = string.IsNullOrEmpty(dt.Rows[0]["MothersDecisionIF"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["MothersDecisionIF"].ToString()) : 0;
                    ancvd.VisitDetails.ExclusiveReplacement = string.IsNullOrEmpty(dt.Rows[0]["ExclusiveReplacement"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["ExclusiveReplacement"].ToString()) : 0;
                    ancvd.VisitDetails.PlaceOfDelivery = dt.Rows[0]["PlaceOfDelivery"].ToString();
                    ancvd.VisitDetails.PODDate = string.IsNullOrEmpty(dt.Rows[0]["PODDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["PODDate"].ToString()) : (DateTime?)null;
                    ancvd.VisitDetails.ModeOfDelivery = string.IsNullOrEmpty(dt.Rows[0]["ModeOfDelivery"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["ModeOfDelivery"].ToString()) : 0;
                    ancvd.VisitDetails.BloodLoss = string.IsNullOrEmpty(dt.Rows[0]["BloodLoss"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["BloodLoss"].ToString()) : 0;
                    ancvd.VisitDetails.ApgarScore1Min = dt.Rows[0]["ApgarScore1Min"].ToString();
                    ancvd.VisitDetails.ApgarScore5Min = dt.Rows[0]["ApgarScore5Min"].ToString();
                    ancvd.VisitDetails.ApgarScore10Min = dt.Rows[0]["ApgarScore10Min"].ToString();
                    ancvd.VisitDetails.ResusitationDone = string.IsNullOrEmpty(dt.Rows[0]["ResusitationDone"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["ResusitationDone"].ToString()) : 0;
                    ancvd.VisitDetails.UserId = string.IsNullOrEmpty(dt.Rows[0]["UserID"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["UserID"].ToString()) : 0;
                    ancvd.VisitDetails.CreateDate = string.IsNullOrEmpty(dt.Rows[0]["CreateDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["CreateDate"].ToString()) : (DateTime?)null;
                    ancvd.VisitDetails.UpdatedDate = string.IsNullOrEmpty(dt.Rows[0]["UpdatedDate"].ToString()) == false ? Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString()) : (DateTime?)null;

                }

                dt = new DataTable();
                if (data.Tables[1].Rows.Count > 0)
                {
                    dt = data.Tables[1];

                    ancvd.VisitDetails.TetanusVaccine = string.IsNullOrEmpty(dt.Rows[0]["TetanusVaccine"].ToString()) == false ? Convert.ToInt32(dt.Rows[0]["TetanusVaccine"].ToString()) : 0;
                    ancvd.VisitDetails.TetanusVaccineReason = dt.Rows[0]["TetanusVaccineReason"].ToString();
                }

                ancvd.VisitAnthropometric = new List<ANCVisitAnthropometric>();
                if (data.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow row in data.Tables[2].Rows)
                    {
                        ANCVisitAnthropometric va = new ANCVisitAnthropometric();
                        va.Ptn_pk = Convert.ToInt32(row["Ptn_pk"]);
                        va.VisitId = Convert.ToInt32(row["VisitId"]);
                        va.VisitDetailDate = string.IsNullOrEmpty(row["VisitDetailDate"].ToString()) == false ? Convert.ToDateTime(row["VisitDetailDate"].ToString()) : (DateTime?)null;
                        va.PregnancyState= string.IsNullOrEmpty(row["PregnancyState"].ToString()) == false ? row["PregnancyState"].ToString() : string.Empty;
                        va.VisitNumber = row["VisitNumber"].ToString();
                        va.Height = row["Height"].ToString();
                        va.Weight = row["Weight"].ToString();
                        va.MUAC = row["MUAC"].ToString();
                        va.BPDiastolic = string.IsNullOrEmpty(row["BPDiastolic"].ToString()) == false ? Convert.ToDecimal(row["BPDiastolic"].ToString()) : (decimal?)0.0;
                        va.BPSystolic = string.IsNullOrEmpty(row["BPSystolic"].ToString()) == false ? Convert.ToDecimal(row["BPSystolic"].ToString()) : (decimal?)0.0;
                        va.CreatedDate = string.IsNullOrEmpty(row["CreatedDate"].ToString()) == false ? Convert.ToDateTime(row["CreatedDate"].ToString()) : (DateTime?)null;
                        va.UserId = string.IsNullOrEmpty(dt.Rows[0]["UserId"].ToString()) == false ? Convert.ToInt32(row["UserId"].ToString()) : 0;

                        ancvd.VisitAnthropometric.Add(va);
                    }
                }

                ancvd.PresentPregnancy = new List<ANCPresentPregnancy>();
                if (data.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow row in data.Tables[3].Rows)
                    {
                        ANCPresentPregnancy pp = new ANCPresentPregnancy();
                        pp.Ptn_pk = Convert.ToInt32(row["Ptn_pk"]);
                        pp.VisitId = Convert.ToInt32(row["VisitId"]);
                        pp.Maturity = row["Maturity"].ToString();
                        pp.Lie = row["Lie"].ToString();
                        pp.FundalHeight = row["FundalHeight"].ToString();
                        pp.FoetalMovement = row["FoetalMovement"].ToString();
                        pp.Presentation = row["Presentation"].ToString();
                        pp.FoetalHeartRate = row["FoetalHeartRate"].ToString();
                        pp.CreatedDate = row["CreatedDate"].ToString();
                        pp.UserId = string.IsNullOrEmpty(dt.Rows[0]["UserId"].ToString()) == false
                            ? Convert.ToInt32(row["UserId"].ToString())
                            : 0;
                        ancvd.PresentPregnancy.Add(pp);
                    }
                }

                /* Purpose */
                if (data.Tables[4].Rows.Count > 0)
                {
                    List<CodeDeCodeTables> lst = new List<CodeDeCodeTables>();

                    lst = (from dt1 in data.Tables[4].AsEnumerable()
                        select new CodeDeCodeTables()
                        {
                            DeCodeId = dt1.Field<int>("ID"),
                            DeCodeName = dt1.Field<string>("NAME"),
                            CodeName = "PUR"
                        }).ToList();

                    ancvd.Purpose.AddRange(lst);
                }

                /* Appointments*/
                if (data.Tables[5].Rows.Count > 0)
                {

                    ancvd.Appointments = new List<Appointment>();

                    int id = 1;
                    foreach (DataRow row in data.Tables[5].Rows)
                    {
                        Appointment app = new Appointment();
                        app.Id = id;
                        app.Ptn_pk = Convert.ToInt32(row["Ptn_Pk"].ToString());

                        app.LocationId = Convert.ToInt32(row["LocationId"].ToString());
                        app.Visit_Id = Convert.ToInt32(row["Visit_pk"].ToString());

                        app.Status = row["STATUS"].ToString();

                        app.AppointmentDate = string.IsNullOrEmpty(row["Appdate"].ToString()) == false ? Convert.ToDateTime(row["Appdate"].ToString()) : (DateTime?)null;
                        app.Purpose = string.IsNullOrEmpty(row["PurPoseID"].ToString()) == false
                            ? Convert.ToInt32(row["PurPoseID"].ToString())
                            : (int?)null;
                        app.PurposeName = row["Purpose"].ToString();
                        app.AppointmentType = string.IsNullOrEmpty(row["AppointmentType"].ToString()) == false
                            ? Convert.ToInt32(row["AppointmentType"].ToString())
                            : (int?)null;
                        app.ServiceArea = string.IsNullOrEmpty(row["ServiceArea"].ToString()) == false
                            ? Convert.ToInt32(row["ServiceArea"].ToString())
                            : (int?)null;
                        app.ServiceAreaName = row["ServiceAreaName"].ToString();
                        app.Description = row["Description"].ToString();

                        ancvd.Appointments.Add(app);
                        id++;
                    }

                }
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BKNHMEI.GetANCVisitDetailsData() Method:" + ex.ToString());
            }
            return ancvd;
        }

        public bool SaveUpdateVisitDetailsData(ANCVisitDetails obj, int userId, int locationId)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BKNHMEI.SaveUpdateVisitDetailsData() method called");
            bool flag = true;
            ClsObject clsObj = new ClsObject();
            DataTable ReturnDT = new DataTable();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObj.Connection = this.Connection;
                clsObj.Transaction = this.Transaction;


                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationId.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());

                string dtVal = DBNull.Value.ToString();
                if (obj.VisitDetails.FirstVisitDate.HasValue)
                {
                    dtVal = obj.VisitDetails.FirstVisitDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@FirstVisitDate", SqlDbType.DateTime, dtVal);

                ClsUtility.AddParameters("@IPTP", SqlDbType.Int, obj.VisitDetails.IPTP.ToString());
                dtVal = DBNull.Value.ToString();
                if (obj.VisitDetails.IPTPDateGiven.HasValue)
                {
                    dtVal = obj.VisitDetails.IPTPDateGiven.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@IPTPDateGiven", SqlDbType.DateTime, dtVal);

                ClsUtility.AddParameters("@InsectisideTreatedNet", SqlDbType.Int, obj.VisitDetails.InsectisideTreatedNet.ToString());
                dtVal = DBNull.Value.ToString();
                if (obj.VisitDetails.InsectisideTreatedNetDT.HasValue)
                {
                    dtVal = obj.VisitDetails.InsectisideTreatedNetDT.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@InsectisideTreatedNetDT", SqlDbType.DateTime, dtVal);
                ClsUtility.AddParameters("@Dewormed", SqlDbType.Int, obj.VisitDetails.Dewormed.ToString());
                dtVal = DBNull.Value.ToString();
                if (obj.VisitDetails.DewormedDateGiven.HasValue)
                {
                    dtVal = obj.VisitDetails.DewormedDateGiven.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@DewormedDateGiven", SqlDbType.DateTime, dtVal);
                ClsUtility.AddParameters("@IronAndFolate", SqlDbType.Int, obj.VisitDetails.IronAndFolate.ToString());
                dtVal = DBNull.Value.ToString();
                if (obj.VisitDetails.IronAndFolateDateGiven.HasValue)
                {
                    dtVal = obj.VisitDetails.IronAndFolateDateGiven.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@IronAndFolateDateGiven", SqlDbType.DateTime, dtVal);
                ClsUtility.AddParameters("@InfantFeedingCounselling", SqlDbType.Int, obj.VisitDetails.InfantFeedingCounselling.ToString());
                ClsUtility.AddParameters("@ExclusiveBreastfeeding", SqlDbType.Int, obj.VisitDetails.ExclusiveBreastfeeding.ToString());
                ClsUtility.AddParameters("@HIVIInfantFeedingOption", SqlDbType.Int, obj.VisitDetails.HIVIInfantFeedingOption.ToString());
                ClsUtility.AddParameters("@MothersDecisionIF", SqlDbType.Int, obj.VisitDetails.MothersDecisionIF.ToString());
                ClsUtility.AddParameters("@ExclusiveReplacement", SqlDbType.Int, obj.VisitDetails.ExclusiveReplacement.ToString());
                ClsUtility.AddParameters("@PlaceOfDelivery", SqlDbType.VarChar, obj.VisitDetails.PlaceOfDelivery.ToString());
                dtVal = DBNull.Value.ToString();
                if (obj.VisitDetails.PODDate.HasValue)
                {
                    dtVal = obj.VisitDetails.PODDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                }
                ClsUtility.AddParameters("@PODDate", SqlDbType.DateTime, dtVal);
                ClsUtility.AddParameters("@ModeOfDelivery", SqlDbType.Int, obj.VisitDetails.ModeOfDelivery.ToString());
                ClsUtility.AddParameters("@BloodLoss", SqlDbType.Int, obj.VisitDetails.BloodLoss.ToString());
                ClsUtility.AddParameters("@ApgarScore1Min", SqlDbType.VarChar, obj.VisitDetails.ApgarScore1Min.ToString());
                ClsUtility.AddParameters("@ApgarScore5Min", SqlDbType.VarChar, obj.VisitDetails.ApgarScore5Min.ToString());
                ClsUtility.AddParameters("@ApgarScore10Min", SqlDbType.VarChar, obj.VisitDetails.ApgarScore10Min.ToString());
                ClsUtility.AddParameters("@ResusitationDone", SqlDbType.Int, obj.VisitDetails.ResusitationDone.ToString());
                ClsUtility.AddParameters("@TetanusVaccine", SqlDbType.Int, obj.VisitDetails.TetanusVaccine.ToString());
                ClsUtility.AddParameters("@TetanusVaccineReason", SqlDbType.VarChar, obj.VisitDetails.TetanusVaccineReason.ToString());



                ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_ANC_UpdateVisitDetails", ClsDBUtility.ObjectEnum.DataTable);


                foreach (ANCVisitAnthropometric va in obj.VisitAnthropometric)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationId.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());

                    dtVal = DBNull.Value.ToString();
                    if (va.VisitDetailDate.HasValue)
                    {
                        dtVal = va.VisitDetailDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                    }
                    
                    ClsUtility.AddParameters("@VisitDetailDate", SqlDbType.DateTime, dtVal);
                    ClsUtility.AddParameters("@PregnancyState", SqlDbType.VarChar, va.PregnancyState.ToString());
                    ClsUtility.AddParameters("@VisitNumber", SqlDbType.VarChar, va.VisitNumber.ToString());
                    ClsUtility.AddParameters("@Height", SqlDbType.VarChar, va.Height.ToString());
                    ClsUtility.AddParameters("@Weight", SqlDbType.VarChar, va.Weight.ToString());
                    ClsUtility.AddParameters("@MUAC", SqlDbType.VarChar, va.MUAC.ToString());
                    
                    ClsUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, va.BPDiastolic.ToString());
                    ClsUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, va.BPSystolic.ToString());
                    ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_ANC_UpdateVisitAnthropometric", ClsDBUtility.ObjectEnum.DataTable);

                }

                foreach (ANCPresentPregnancy pp in obj.PresentPregnancy)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationId.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());

                    ClsUtility.AddParameters("@Maturity", SqlDbType.VarChar, pp.Maturity.ToString());
                    ClsUtility.AddParameters("@Lie", SqlDbType.VarChar, pp.Lie.ToString());
                    ClsUtility.AddParameters("@FundalHeight", SqlDbType.VarChar, pp.FundalHeight.ToString());
                    ClsUtility.AddParameters("@FoetalMovement", SqlDbType.VarChar, pp.FoetalMovement.ToString());
                    ClsUtility.AddParameters("@Presentation", SqlDbType.VarChar, pp.Presentation.ToString());
                    ClsUtility.AddParameters("@FoetalHeartRate", SqlDbType.VarChar, pp.FoetalHeartRate.ToString());

                    ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_ANC_UpdatePresentPregnancy", ClsDBUtility.ObjectEnum.DataTable);

                }

                foreach (Appointment pa in obj.Appointments)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                    ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, obj.Visit_Id.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationId.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userId.ToString());

                    dtVal = DBNull.Value.ToString();
                    if (pa.AppointmentDate.HasValue)
                    {
                        dtVal = pa.AppointmentDate.Value.Date.ToString("yyyy-MM-dd hh:mm:ss").ToString();
                    }
                    ClsUtility.AddParameters("@appDate", SqlDbType.DateTime, dtVal);
                    ClsUtility.AddParameters("@purpose", SqlDbType.Int, pa.Purpose.ToString());
                    ClsUtility.AddParameters("@status", SqlDbType.Int, "12");
                    ClsUtility.AddParameters("@service", SqlDbType.Int, pa.ServiceArea.ToString());
                    ClsUtility.AddParameters("@desc", SqlDbType.Int, pa.Description.ToString());

                    ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "Pr_ANC_UpdateAppointment", ClsDBUtility.ObjectEnum.DataTable);

                }
                
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }
            catch (Exception ex)
            {
                DataMgr.RollBackTransation(this.Transaction);
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BKNHMEI.SaveUpdateVisitDetailsData() Method:" + ex.ToString());
                //throw ex;
            }
            finally
            {

                clsObj = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            //return ReturnDT;
            return flag;
        }

    }
}