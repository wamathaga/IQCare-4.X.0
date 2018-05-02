using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Interface.Clinical;
using Interface.Security;
using Application.Presentation;
using Application.Common;
using Application.Interface;


public partial class ClinicalForms_ClinicalHomeHeaderFooter : System.Web.UI.MasterPage
{
    #region "User Functions"

    #region "Custom Form Load in Menu"

    string ObjFactoryParameter = "BusinessProcess.Clinical.BCustomForm, BusinessProcess.Clinical";
    string ModuleId = "";
    int PtnPMTCTStatus;
    int PtnARTStatus;
    string PMTCTNos = "";
    string ARTNos = "";
    public int PatientId = 0;
    //private void Load_MenuPartial(int PatientId, string Status,int CountryId)
    //{
    //    ICustomForm CustomFormMgr = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
    //    DataSet theDS = CustomFormMgr.GetFormName(1, CountryId);
    //    foreach (DataRow dr in theDS.Tables[0].Rows)
    //    {
    //        //string theURL = string.Format("{0}&PatientId={1}&FormID={2}&sts={3}", "../ClinicalForms/frmClinical_CustomForm.aspx?name=Add", PatientId.ToString(), dr["FeatureID"].ToString(), Status);
    //        string theURL = string.Format("{0}", "../ClinicalForms/frmClinical_CustomForm.aspx?");

    //        if (Status == "0")
    //            divPMTCT.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + dr["FormID"] + "' onClick=fnSetformID('" + dr["FeatureID"].ToString() + "'); HRef=" + theURL + " runat='server'>" + dr["FeatureName"] + "</a>"));
    //        else
    //            divPMTCT.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + dr["FormID"] + "' onClick=fnSetformID('" + dr["FeatureID"].ToString() + "'); runat='server'>" + dr["FeatureName"] + "</a>"));
    //    }

    //}

    private void Load_MenuRegistration()
    {
        int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
        string theURL = "";


        IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        DataSet theDS = PatientHome.GetTechnicalAreaandFormName(ModuleId);
        //if (theDS.Tables[0].Rows[0]["ModuleName"].ToString() == "HIVCARE-STATIC FORM")
        //{
        //    mnuPMTCTEnrol.Visible = false;
        //}
        //else { mnuEnrolment.Visible = false; }

        if (ModuleId == 2)
        {
            mnuPMTCTEnrol.Visible = true;
            mnuEnrolment.Visible = true;
        }
        else
        {
            mnuPMTCTEnrol.Visible = true;
            mnuEnrolment.Visible = false;
        }

    }

    private void Load_MenuCreateNewForm()
    {
        int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
        DataSet theDS = (DataSet)ViewState["AddForms"];
        foreach (DataRow theDR in theDS.Tables[1].Rows)
        {
            if (Convert.ToInt32(theDR["Featureid"]) != 71)
            {
                string theURL = "", theLabTest = "";
                if (Convert.ToInt32(theDR["FeatureId"]) == 3)
                    //theURL = string.Format("{0}", "../Pharmacy/frmPharmacy_Adult.aspx?Prog=''");
                    theURL = string.Format("{0}", "../Pharmacy/frmPharmacyForm.aspx?Prog=''");
                else if (Convert.ToInt32(theDR["FeatureId"]) == 4)
                    //theURL = string.Format("{0}", "../Pharmacy/frmPharmacy_Paediatric.aspx?Prog=''");
                    theURL = string.Format("{0}", "../Pharmacy/frmPharmacyForm.aspx?Prog=''");
                else if (Convert.ToInt32(theDR["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "0")
                    theURL = string.Format("{0}sts={1}", "../Laboratory/frmLabOrder.aspx?", lblpntStatus.Text);
                else if (Convert.ToInt32(theDR["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "1")
                {
                    theURL = string.Format("{0}&sts={1}", "../Laboratory/frmLabOrderTests.aspx?name=Add", lblpntStatus.Text);
                    theLabTest = "window.open('" + theURL + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');return false;";
                }
                else if (theDR["FeatureName"].ToString() == "Care Termination")
                    theURL = string.Format("{0}", "../Scheduler/frmScheduler_ContactCareTracking.aspx?");
                else
                    theURL = string.Format("{0}", "../ClinicalForms/frmClinical_CustomForm.aspx?");

                if (ModuleId.ToString() == "1")
                {
                    divPMTCT.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + theDR["FeatureID"].ToString() + "' onClick=fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=" + theURL + " runat='server' "));
                    if (lblpntStatus.Text == "1")
                        divPMTCT.Controls.Add(new LiteralControl("Disabled='true'"));
                    divPMTCT.Controls.Add(new LiteralControl(" >" + theDR["FeatureName"].ToString() + "</a>"));
                }
                else if (ModuleId.ToString() == "2")
                {
                    ClinicID.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + theDR["FeatureID"].ToString() + "'  runat='server' "));
                    if (lblpntStatus.Text != "1")
                    {
                        ClinicID.Controls.Add(new LiteralControl("onClick=fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=" + theURL + ""));
                    }
                    ClinicID.Controls.Add(new LiteralControl(" >" + theDR["FeatureName"].ToString() + "</a>"));
                }
                else if (ModuleId.ToString() == "202")
                {
                    divUgandaBlueCard.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + theDR["FeatureID"].ToString() + "'  runat='server' "));
                    if (lblpntStatus.Text != "1")
                    {
                        if (theLabTest != "")
                        {
                            divUgandaBlueCard.Controls.Add(new LiteralControl("onClick=" + theLabTest + ", fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=#"));
                        }
                        else { divUgandaBlueCard.Controls.Add(new LiteralControl("onClick=fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=" + theURL + "")); }
                    }
                    divUgandaBlueCard.Controls.Add(new LiteralControl(" >" + theDR["FeatureName"].ToString() + "</a>"));
                }

                else if (ModuleId.ToString() == "203")
                {
                    divKenyaBlueCard.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + theDR["FeatureID"].ToString() + "'  runat='server' "));
                    if (lblpntStatus.Text != "1")
                    {
                        if (theLabTest != "")
                        {
                            divKenyaBlueCard.Controls.Add(new LiteralControl("onClick=" + theLabTest + ", fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=#"));
                        }
                        else { divKenyaBlueCard.Controls.Add(new LiteralControl("onClick=fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=" + theURL + "")); }
                    }
                    divKenyaBlueCard.Controls.Add(new LiteralControl(" >" + theDR["FeatureName"].ToString() + "</a>"));
                }
                else
                {
                    if (Convert.ToInt32(theDR["FeatureId"]) == 5 && Session["PaperLess"].ToString() == "1")
                    {
                        mnuLabOrderDynm.Visible = true;
                        mnuLabOrderDynm.HRef = theURL;
                        mnuLabOrderDynm.Attributes.Add("onclick", "window.open('" + theURL + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');return false;");
                    }
                    else
                    {
                        DivDynModule.Controls.Add(new LiteralControl("<a class ='menuitem2' id ='mnu" + theDR["FeatureID"].ToString() + "' onClick=fnSetformID('" + theDR["FeatureID"].ToString() + "'); HRef=" + theURL + " runat='server'"));
                        if (lblpntStatus.Text == "1")
                            DivDynModule.Controls.Add(new LiteralControl("Disabled='true'"));
                        DivDynModule.Controls.Add(new LiteralControl(" >" + theDR["FeatureName"].ToString() + "</a>"));
                    }
                }
            }
        }

        if (ModuleId.ToString() == "1")
        {
            divPMTCT.Visible = true;
            DivDynModule.Visible = false;
            ClinicID.Visible = false;
            divUgandaBlueCard.Visible = false;
        }
        else if (ModuleId.ToString() == "2")
        {
            divPMTCT.Visible = false;
            DivDynModule.Visible = false;
            ClinicID.Visible = true;
            divUgandaBlueCard.Visible = false;
        }

        else if (ModuleId.ToString() == "202")
        {
            divPMTCT.Visible = false;
            DivDynModule.Visible = false;
            ClinicID.Visible = false;
            divUgandaBlueCard.Visible = true;
        }

        else if (ModuleId.ToString() == "203")
        {
            divPMTCT.Visible = false;
            DivDynModule.Visible = false;
            ClinicID.Visible = false;
            divUgandaBlueCard.Visible = false;
            divKenyaBlueCard.Visible = true;
        }
        else
        {
            divPMTCT.Visible = false;
            DivDynModule.Visible = true;
            ClinicID.Visible = false;
            divUgandaBlueCard.Visible = false;
        }
    }



    #endregion


    private void Init_Menu()
    {
        IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        int ModuleId = Convert.ToInt32(Session["TechnicalAreaId"]);
        DataSet theDS = PatientHome.GetTechnicalAreaandFormName(ModuleId);
        ViewState["AddForms"] = theDS;

        if (Convert.ToInt32(Session["PatientId"]) != 0)
            PatientId = Convert.ToInt32(Session["PatientId"]);

        if (PatientId == 0)
            PatientId = Convert.ToInt32(Session["PatientId"]);

        if (Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }

        lblversion.Text = AuthenticationManager.AppVersion;
        lblrelDate.Text = AuthenticationManager.ReleaseDate;

        DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
        if (dtPatientInfo != null && dtPatientInfo.Rows.Count > 0)
        {
            PMTCTNos = dtPatientInfo.Rows[0]["ANCNumber"].ToString() + dtPatientInfo.Rows[0]["PMTCTNumber"].ToString() + dtPatientInfo.Rows[0]["AdmissionNumber"].ToString() + dtPatientInfo.Rows[0]["OutpatientNumber"].ToString();
            ARTNos = dtPatientInfo.Rows[0]["PatientEnrollmentId"].ToString();
        }
        ////DataTable theDT1 = (DataTable)Session["AppModule"];
        ////DataView theDV = new DataView(theDT1);

        //################  Master Settings ###################
        string UserID = "";
        if (Session["AppUserID"].ToString() != null)
            UserID = Session["AppUserId"].ToString();
        if (Session["AppUserName"].ToString() != null)
            lblUserName.Text = Session["AppUserName"].ToString();
        if (Session["AppLocation"].ToString() != null)
            lblLocation.Text = Session["AppLocation"].ToString();

        IIQCareSystem AdminManager;
        AdminManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        if (Session["AppDateFormat"].ToString() != null)
        {
            lblDate.Text = AdminManager.SystemDate().ToString(Session["AppDateFormat"].ToString());
        }

        //######################################################

        string theUrl;
        //////if (lblpntStatus.Text == "0")
        //////{
        if (Session["PtnPrgStatus"] != null)
        {
            DataTable theStatusDT = (DataTable)Session["PtnPrgStatus"];
            DataTable theCEntedStatusDT = (DataTable)Session["CEndedStatus"];
            string PatientExitReason = string.Empty;
            string PMTCTCareEnded = string.Empty;
            string CareEnded = string.Empty;
            if (theCEntedStatusDT.Rows.Count > 0)
            {
                PatientExitReason = Convert.ToString(theCEntedStatusDT.Rows[0]["PatientExitReason"]);
                PMTCTCareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["PMTCTCareEnded"]);
                CareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["CareEnded"]);
            }


            //if ((theStatusDT.Rows[0]["PMTCTStatus"].ToString() == "PMTCT Care Ended") || (Session["PMTCTPatientStatus"]!= null && Session["PMTCTPatientStatus"].ToString() == "1"))
            if ((Convert.ToString(theStatusDT.Rows[0]["PMTCTStatus"]) == "PMTCT Care Ended") || (PatientExitReason == "93" && PMTCTCareEnded == "1"))
            {
                PtnPMTCTStatus = 1;
                Session["PMTCTPatientStatus"] = 1;
            }
            else
            {
                PtnPMTCTStatus = 0;
                Session["PMTCTPatientStatus"] = 0;
                //LoggedInUser.PatientStatus = 0;
            }
            //if ((theStatusDT.Rows[0]["ART/PalliativeCare"].ToString() == "Care Ended") || (Session["HIVPatientStatus"]!= null && Session["HIVPatientStatus"].ToString() == "1"))
            if ((Convert.ToString(theStatusDT.Rows[0]["ART/PalliativeCare"]) == "Care Ended") || (PatientExitReason == "93" && CareEnded == "1"))
            {
                PtnARTStatus = 1;
                Session["HIVPatientStatus"] = 1;
            }
            else
            {
                PtnARTStatus = 0;
                Session["HIVPatientStatus"] = 0;
            }
        }
        //////}
        //else
        //{
        //    if (Session["PtnPrgStatus"] != null)
        //    {
        //        DataTable theStatusDT = (DataTable)Session["PtnPrgStatus"];
        //        if (theStatusDT.Rows[0]["PMTCTStatus"].ToString() == "PMTCT Care Ended")
        //        {
        //            PtnPMTCTStatus = 1;
        //            Session["PMTCTPatientStatus"] = 1;

        //        }
        //        else
        //        {
        //            PtnPMTCTStatus = 0;
        //            Session["PMTCTPatientStatus"] = 0;

        //        }
        //        if (theStatusDT.Rows[0]["ART/PalliativeCare"].ToString() == "Care Ended")
        //        {
        //            PtnARTStatus = 1;
        //            Session["HIVPatientStatus"] = 1;

        //        }
        //        else
        //        {
        //            PtnARTStatus = 0;
        //            Session["HIVPatientStatus"] = 0;

        //        }
        //    }


        //}

        if (lblpntStatus.Text == "0" && (PtnARTStatus == 0 || PtnPMTCTStatus == 0))
        //if (PtnARTStatus == 0 || PtnPMTCTStatus == 0)
        {
            if (PtnARTStatus == 0)
            {
                //########### Initial Evaluation ############
                //theUrl = string.Format("{0}&sts={1}", "../ClinicalForms/frmClinical_InitialEvaluation.aspx?name=Add", PtnARTStatus);
                theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_InitialEvaluation.aspx");
                mnuInitEval.HRef = theUrl;
                //########### ART-FollowUp ############
                //string theUrl18 = string.Format("{0}&sts={1}", "../ClinicalForms/frmClinical_ARTFollowup.aspx?name=Add", PtnARTStatus);
                string theUrl18 = string.Format("{0}", "../ClinicalForms/frmClinical_ARTFollowup.aspx");
                mnuFollowupART.HRef = theUrl18;
                //########### Non-ART Follow-Up #########
                string theUrl1 = string.Format("{0}", "../ClinicalForms/frmClinical_NonARTFollowUp.aspx");
                Session.Remove("ExixstDS1");
                mnuNonARTFollowUp.HRef = theUrl1;
                ////########### HIV Care/ART Encounter #########
                //string theUrl2 = string.Format("{0}", "../ClinicalForms/frmClinical_HIVCareARTCardEncounter.aspx");
                //mnuHIVCareARTEncounter.HRef = theUrl2;
                //########### Contact Tracking ############        
                //theUrl = string.Format("{0}", "../Scheduler/frmScheduler_ContactCareTracking.aspx?");
                //mnuContactCare1.HRef = theUrl;
                //########### Patient Record ############ 
                theUrl = string.Format("{0}&sts={1}", "../ClinicalForms/frmClinical_PatientRecordCTC.aspx?name=Add", PtnARTStatus);
                //mnuPatientRecord.HRef = theUrl;
                //########### Adult Pharmacy ############
                //LoggedInUser.Program = "ART";
                //LoggedInUser.PatientPharmacyId = 0;
                theUrl = string.Format("{0}", "../Pharmacy/frmPharmacyForm.aspx?Prog=ART");
                //theUrl = string.Format("{0}", "../Pharmacy/frmPharmacy_Adult.aspx?Prog=ART");
                mnuAdultPharmacy.HRef = theUrl;
                //########### Pediatric Pharmacy ############        
                //theUrl = string.Format("{0}", "../Pharmacy/frmPharmacy_Paediatric.aspx?Prog=ART");
                //mnuPaediatricPharmacy.HRef = theUrl;
                ////########### Pharmacy CTC###############
                theUrl = string.Format("{0}", "../Pharmacy/frmPharmacy_CTC.aspx?Prog=ART");
                //mnuPharmacyCTC.HRef = theUrl;
                //########### Laboratory ############
                theUrl = string.Format("{0}sts={1}", "../Laboratory/frmLabOrder.aspx?", PtnARTStatus);
                string theUrlLabOrder = string.Format("{0}&sts={1}", "../Laboratory/frmLabOrderTests.aspx?name=Add", PtnARTStatus);
                mnuLabOrder.HRef = theUrl;
                mnuOrderLabTest.HRef = theUrlLabOrder;
                mnuOrderLabTest.Attributes.Add("onclick", "window.open('" + theUrlLabOrder + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');return false;");
                //########### Home Visit ############
                theUrl = string.Format("{0}", "../Scheduler/frmScheduler_HomeVisit.aspx");
                mnuHomeVisit.HRef = theUrl;
            }

            if (PtnPMTCTStatus == 0)
            {
                //########### Contact Tracking ############        
                theUrl = string.Format("{0}Module={1}", "../Scheduler/frmScheduler_ContactCareTracking.aspx?", "PMTCT");
                //mnuContactCarePMTCT.HRef = theUrl;

                //####### Adult Pharmacy PMTCT ##########
                //LoggedInUser.Program = "PMTCT";
                //LoggedInUser.PatientPharmacyId = 0;
                theUrl = string.Format("{0}", "../Pharmacy/frmPharmacyForm.aspx?Prog=PMTCT");
                mnuAdultPharmacyPMTCT.HRef = theUrl;

                //###########Paediatric Pharmacy PMTCT#################
                //theUrl = string.Format("{0}", "../Pharmacy/frmPharmacy_Paediatric.aspx?Prog=PMTCT");
                //mnuPaediatricPharmacyPMTCT.HRef = theUrl;

                //########### Pharmacy PMTCT CTC###############
                theUrl = string.Format("{0}", "../Pharmacy/frmPharmacy_CTC.aspx?Prog=PMTCT");
                //mnuPharmacyPMTCTCTC.HRef = theUrl;

                //########### Laboratory ############
                string theUrlPMTCT = string.Format("{0}sts={1}", "../Laboratory/frmLabOrder.aspx?", PtnPMTCTStatus);
                string theUrlPMTCTLabOrder = string.Format("{0}sts={1}", "../Laboratory/frmLabOrderTests.aspx?", PtnPMTCTStatus);
                mnuLabOrderPMTCT.HRef = theUrlPMTCT;
                mnuOrderLabTestPMTCT.HRef = theUrlPMTCTLabOrder;
                mnuOrderLabTestPMTCT.Attributes.Add("onclick", "window.open('" + theUrlPMTCTLabOrder + "','','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=500,scrollbars=yes');return false;");

            }
        }

        #region "Common Forms"
        theUrl = string.Format("{0}&mnuClicked={1}&sts={2}", "../AdminForms/frmAdmin_DeletePatient.aspx?name=Add", "DeletePatient", lblpntStatus.Text);
        mnuAdminDeletePatient.HRef = theUrl;

        //######## Meetu 08 Sep 2009 End########//
        //####### Delete Form #############
        theUrl = string.Format("{0}?sts={1}", "../ClinicalForms/frmClinical_DeleteForm.aspx", lblpntStatus.Text);
        mnuClinicalDeleteForm.HRef = theUrl;

        //####### Delete Patient  #############
        //theUrl = string.Format("{0}?mnuClicked={1}&sts={2}", "../frmFindAddPatient.aspx?name=Add", "DeletePatient", lblpntStatus.Text);
        theUrl = string.Format("{0}?mnuClicked={1}&sts={2}&PatientID={3}", "../AdminForms/frmAdmin_DeletePatient.aspx?name=Add", "DeletePatient", lblpntStatus.Text, PatientId.ToString());
        mnuAdminDeletePatient.HRef = theUrl;

        //##### Patient Transfer #######
        //theUrl = string.Format("{0}&PatientId={1}&sts={2}", "../ClinicalForms/frmClinical_Transfer.aspx?name=Add", PatientId.ToString(), lblpntStatus.Text);
        theUrl = string.Format("{0}&sts={1}", "../ClinicalForms/frmClinical_Transfer.aspx?name=Add", lblpntStatus.Text);

        mnuPatientTranfer.HRef = theUrl;

        //########### Existing Forms ############
        theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_History.aspx");
        mnuExistingForms.HRef = theUrl;

        //########### ARV-Pickup Report ############
        theUrl = string.Format("{0}&SatelliteID={1}&CountryID={2}&PosID={3}&sts={4}", "../Reports/frmReport_PatientARVPickup.aspx?name=Add", Session["AppSatelliteId"], Session["AppCountryId"], Session["AppPosID"], lblpntStatus.Text);
        mnuDrugPickUp.HRef = theUrl;

        //########### PatientProfile ############
        theUrl = string.Format("{0}&ReportName={1}&sts={2}", "../Reports/frmReportViewer.aspx?name=Add", "PatientProfile", lblpntStatus.Text);
        mnuPatientProfile.HRef = theUrl;

        //########### ARV-Pickup Report ############
        theUrl = string.Format("{0}&SatelliteID={1}&CountryID={2}&PosID={3}&sts={4}", "../Reports/frmReportDebitNote.aspx?name=Add", Session["AppSatelliteId"], Session["AppCountryId"], Session["AppPosID"], lblpntStatus.Text);
        mnuDebitNote.HRef = theUrl;

        ////////########### Patient Blue Cart############
        //////theUrl = string.Format("{0}&PatientId={1}&ReportName={2}&sts={3}", "../Reports/frmPatientBlueCart.aspx?name=Add", PatientId.ToString(), "PatientProfile", lblpntStatus.Text);
        //////mnupatientbluecart.HRef = theUrl;


        //###### PatientHome #############
        ////theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", PatientId.ToString());
        theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_Home.aspx");
        mnuPatienHome.HRef = theUrl;

        //###### Scheduler #############
        theUrl = string.Format("{0}&LocationId={1}&FormName={2}&sts={3}", "../Scheduler/frmScheduler_AppointmentHistory.aspx?name=Add", Session["AppLocationId"].ToString(), "PatientHome", lblpntStatus.Text);
        mnuScheduleAppointment.HRef = theUrl;

        //####### Additional Forms - Family Information #######
        theUrl = string.Format("{0}", "../ClinicalForms/frmFamilyInformation.aspx?name=Add");
        mnuFamilyInformation.HRef = theUrl;

        //####### Patient Classification #######
        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_PatientClassificationCTC.aspx?name=Add");
        mnuPatientClassification.HRef = theUrl;

        //####### Follow-up Education #######
        theUrl = string.Format("{0}", "../ClinicalForms/frmFollowUpEducationCTC.aspx?name=Add");
        mnuFollowupEducation.HRef = theUrl;

        //####### Exposed Infant #############
        theUrl = string.Format("{0}", "../ClinicalForms/frmExposedInfantEnrollment.aspx");
        mnuExposedInfant.HRef = theUrl;
        #endregion
        theUrl = string.Format("{0}", "../ClinicalForms/frm_PriorArt_HivCare.aspx");
        mnuPriorARTHIVCare.HRef = theUrl;
        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_ARTCare.aspx");
        mnuARTCare.HRef = theUrl;

        //########### HIV Care/ART Encounter #########
        string theUrl2 = string.Format("{0}", "../ClinicalForms/frmClinical_HIVCareARTCardEncounter.aspx");
        mnuHIVCareARTEncounter.HRef = theUrl2;

        //########### Kenya Blue Card #########
        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_InitialFollowupVisit.aspx");
        mnuARTVisit.HRef = theUrl;

        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_ARVTherapy.aspx");
        mnuARTTherapy.HRef = theUrl;

        //theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmClinical_ARTHistory.aspx", PatientId.ToString());
        //mnuARTTherapy.HRef = theUrl;

        theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_ARTHistory.aspx");
        mnuARTHistory.HRef = theUrl;

        //########### Patient Enrollment ############
        //Added - Jayanta Kr. Das - 16-02-07
        DataTable theDT = new DataTable();
        if (PatientId != 0)
        {
            //### Patient Enrolment ######
            string theUrl1 = "";
            if (ARTNos != null && ARTNos == "")
            {
                if (Session["SystemId"].ToString() == "1" && PtnARTStatus == 0)
                {
                    ////theUrl = string.Format("{0}&patientid={1}&locationid={2}&sts={3}", "../ClinicalForms/frmClinical_Enrolment.aspx?name=Add", PatientId.ToString(), Session["AppLocationId"].ToString(), PtnARTStatus);
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_Enrolment.aspx");
                    mnuEnrolment.HRef = theUrl;
                }
                else if (PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}&locationid={1}&sts={2}", "../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Add", Session["AppLocationId"].ToString(), PtnARTStatus);
                    mnuEnrolment.HRef = theUrl;
                }
                if (PtnPMTCTStatus == 0)
                {
                    ////theUrl1 = string.Format("{0}&patientid={1}&locationid={2}&sts={3}", "../ClinicalForms/frmClinical_EnrolmentPMTCT.aspx?name=Edit", PatientId.ToString(), Session["AppLocationId"].ToString(), PtnPMTCTStatus);
                    //theUrl1 = string.Format("{0}", "../frmPatientRegistration.aspx"); //JAYANT 25/4/2012
                    theUrl1 = string.Format("{0}", "../frmPatientCustomRegistration.aspx");
                    mnuPMTCTEnrol.HRef = theUrl1;
                }
            }

            else if (PMTCTNos != null && PMTCTNos == "")
            {
                if (PtnPMTCTStatus == 0)
                {
                    ////theUrl1 = string.Format("{0}&patientid={1}&locationid={2}&sts={3}", "../ClinicalForms/frmClinical_EnrolmentPMTCT.aspx?name=Add", PatientId.ToString(), Session["AppLocationId"].ToString(), PtnPMTCTStatus);
                    //theUrl1 = string.Format("{0}", "../frmPatientRegistration.aspx");
                    theUrl1 = string.Format("{0}", "../frmPatientCustomRegistration.aspx");
                    mnuPMTCTEnrol.HRef = theUrl1;
                }

                if (Session["SystemId"].ToString() == "1" && PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_Enrolment.aspx");
                    mnuEnrolment.HRef = theUrl;
                }
                else if (PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}&patientid={1}&locationid={2}&sts={3}", "../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Edit", PatientId.ToString(), Session["AppLocationId"].ToString(), PtnARTStatus);
                    mnuEnrolment.HRef = theUrl;
                }
            }
            else
            {
                //if (PtnPMTCTStatus == 0)
                //{
                ////theUrl1 = string.Format("{0}&patientid={1}&locationid={2}&sts={3}", "../ClinicalForms/frmClinical_EnrolmentPMTCT.aspx?name=Edit", PatientId.ToString(), Session["AppLocationId"].ToString(), PtnPMTCTStatus);
                //theUrl1 = string.Format("{0}", "../frmPatientRegistration.aspx");
                theUrl1 = string.Format("{0}", "../frmPatientCustomRegistration.aspx");
                mnuPMTCTEnrol.HRef = theUrl1;
                //}

                if (Session["SystemId"].ToString() == "1" && PtnARTStatus == 0)
                {
                    ////theUrl = string.Format("{0}&patientid={1}&locationid={2}&sts={3}", "../ClinicalForms/frmClinical_Enrolment.aspx?name=Edit", PatientId.ToString(), Session["AppLocationId"].ToString(), PtnARTStatus);
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_Enrolment.aspx");
                    mnuEnrolment.HRef = theUrl;
                }
                else if (PtnARTStatus == 0)
                {
                    theUrl = string.Format("{0}&locationid={1}&sts={2}", "../ClinicalForms/frmClinical_PatientRegistrationCTC.aspx?name=Edit", Session["AppLocationId"].ToString(), PtnARTStatus);
                    mnuEnrolment.HRef = theUrl;
                }

            }

        }
        //Load_MenuPartial(PatientId, PtnPMTCTStatus.ToString(), Convert.ToInt32(Session["AppCurrency"].ToString()));
    }
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SetPatientId_Session()
    {
        HttpContext.Current.Session["PatientVisitId"] = 0;
        HttpContext.Current.Session["ServiceLocationId"] = 0;
        HttpContext.Current.Session["LabId"] = 0;
        HttpContext.Current.Session["PatientVisitIdhiv"] = 0;
    }

    //Dynamic Forms
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SetDynamic_Session(string id)
    {
        HttpContext.Current.Session["PatientVisitId"] = 0;
        HttpContext.Current.Session["ServiceLocationId"] = 0;
        HttpContext.Current.Session["FeatureID"] = id;
        HttpContext.Current.Session["PatientVisitIdhiv"] = 0;
    }

    //RTyagi..19Feb 07..
    private void AuthenticationRights()
    {
        DataView theDV = new DataView((DataTable)Session["UserRight"]);
        string ModuleId = "0," + Session["TechnicalAreaId"].ToString();
        theDV.RowFilter = "ModuleId in (" + ModuleId + ")";
        DataTable theDT = new DataTable();
        theDT = theDV.ToTable();

        //// Registration Based Menu///////

        //if (ARTNos != null && ARTNos == "")
        //    //tdART.Visible = false;
        //if (PMTCTNos != null && PMTCTNos == "")
        //tdPMTCT.Visible = false;
        ///////////////////////////////////
        /////////PaperLess Clinic//////////
        if (Session["PaperLess"].ToString() == "1")
        {
            mnuOrderLabTest.Visible = true;
            mnuOrderLabTestPMTCT.Visible = true;
            mnuLabOrderPMTCT.Visible = false;
            mnuLabOrder.Visible = false;
        }
        else
        {
            mnuLabOrderPMTCT.Visible = true;
            mnuLabOrder.Visible = true;
            mnuOrderLabTest.Visible = false;
            mnuOrderLabTestPMTCT.Visible = false;

        }
        ////////////////////////////////////


        AuthenticationManager Authentication = new AuthenticationManager();

        if (Authentication.HasFeatureRight(ApplicationAccess.AdultPharmacy, theDT) == false)
        {
            mnuAdultPharmacy.Visible = false;
            mnuAdultPharmacyPMTCT.Visible = false;
        }

        if (Authentication.HasFeatureRight(ApplicationAccess.ARTFollowup, theDT) == false)
        {
            mnuFollowupART.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.CareTracking, theDT) == false)
        {
            // mnuContactCare1.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.Enrollment, theDT) == false)
        {
            mnuEnrolment.Visible = false;
        }
        //if (Authentication.HasFeatureRight(ApplicationAccess.PMTCTEnrollment, theDT) == false)
        //{
        //    mnuPMTCTEnrol.Visible = false;
        //}
        if (Authentication.HasFeatureRight(ApplicationAccess.HomeVisit, theDT) == false)
        {
            mnuHomeVisit.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.InitialEvaluation, theDT) == false)
        {
            mnuInitEval.Visible = false;
        }

        if (Authentication.HasFeatureRight(ApplicationAccess.Laboratory, theDT) == false)
        {
            mnuLabOrder.Visible = false;
            mnuLabOrderPMTCT.Visible = false;
        }

        if (Authentication.HasFeatureRight(ApplicationAccess.NonARTFollowup, theDT) == false)
        {
            mnuNonARTFollowUp.Visible = false;
        }

        //if (Authentication.HasFeatureRight(ApplicationAccess.PaediatricPharmacy, theDT) == false)
        //{
        //    mnuPaediatricPharmacy.Visible = false;
        //    mnuPaediatricPharmacyPMTCT.Visible = false;
        //}


        if (Authentication.HasFeatureRight(ApplicationAccess.DeleteForm, theDT) == false)
        {
            mnuClinicalDeleteForm.Visible = false;
        }


        if (Authentication.HasFeatureRight(ApplicationAccess.PatientARVPickup, theDT) == false)
        {
            ReportID.Visible = false;
        }

        if (Authentication.HasFeatureRight(ApplicationAccess.Schedular, theDT) == false)
        {
            mnuScheduleAppointment.Visible = false;
        }

        if (Authentication.HasFeatureRight(ApplicationAccess.AdultPharmacy, theDT) == false && Authentication.HasFeatureRight(ApplicationAccess.ARTFollowup, theDT) == false && Authentication.HasFeatureRight(ApplicationAccess.CareTracking, theDT) == false
            && Authentication.HasFeatureRight(ApplicationAccess.Enrollment, theDT) == false && Authentication.HasFeatureRight(ApplicationAccess.HomeVisit, theDT) == false && Authentication.HasFeatureRight(ApplicationAccess.Laboratory, theDT) == false
            && Authentication.HasFeatureRight(ApplicationAccess.Laboratory, theDT) == false && Authentication.HasFeatureRight(ApplicationAccess.NonARTFollowup, theDT) == false && Authentication.HasFeatureRight(ApplicationAccess.PaediatricPharmacy, theDT) == false)
        {
            ClinicID.Visible = false;
        }

        /******** Admin menus *********/
        if (Authentication.HasFeatureRight(ApplicationAccess.UserAdministration, theDT) == false)
        {
            mnuAdminUser.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.UserGroupAdministration, theDT) == false)
        {
            mnuAdminUserGroup.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.DeletePatient, theDT) == false)
        {
            mnuAdminDeletePatient.Visible = false;
        }

        if (Authentication.HasFeatureRight(ApplicationAccess.FacilitySetup, theDT) == false)
        {
            mnuAdminFacility.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.DonorReports, theDT) == false)
        {
            mnuAdminDonorReport.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.CustomReports, theDT) == false)
        {
            mnuAdminCustomReport.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.FacilityReports, theDT) == false)
        {
            mnuAdminFacilityReport.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.ConfigureCustomFields, theDT) == false)
        {
            mnuAdminCustomConfig.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.Schedular, theDT) == false)
        {
            mnuSchedular.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.SchedularAppointment, theDT) == false)
        {
            mnuScheduleAppointment.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.FamilyInfo, theDT) == false)
        {
            mnuFamilyInformation.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.PatientClassification, theDT) == false)
        {
            mnuPatientClassification.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.FollowupEducation, theDT) == false)
        {
            mnuFollowupEducation.Visible = false;
        }
        else
        {
            DataSet theDS = (DataSet)ViewState["AddForms"];
            DataView theFormDV = new DataView(theDS.Tables[1]);
            theFormDV.RowFilter = "FeatureId=" + ApplicationAccess.FollowupEducation.ToString();
            if (theFormDV.Count < 1)
                mnuFollowupEducation.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.PatientRecord, theDT) == false)
        {
            //mnuPatientRecord.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.Pharmacy, theDT) == false)
        {
            // mnuPharmacyCTC.Visible = false;
            //mnuPharmacyPMTCTCTC.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.Transfer, theDT) == false)
        {
            mnuPatientTranfer.Visible = false;
        }
        if (Authentication.HasFeatureRight(ApplicationAccess.OrderLabTest, theDT) == false)
        {
            mnuOrderLabTest.Visible = false;
            mnuOrderLabTestPMTCT.Visible = false;
        }
    }
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        this.Page.Error += new EventHandler(Page_Error);
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
        Response.Write("<script>alert('Application has some issue, we are re-directing you to Login page.') ; location.href='../frmLogin.aspx'</script>");
        Server.ClearError();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(ClinicalForms_ClinicalHomeHeaderFooter));
            //lblTitle.InnerText = "International Quality Care Patient Management and Monitoring System [" + Session["AppLocation"].ToString() + "]";
            string url = Request.RawUrl.ToString();
            Application["PrvFrm"] = url;
            Init_Menu();
            Load_MenuRegistration();
            Load_MenuCreateNewForm();

            //RTyagi..19Feb 07..
            AuthenticationRights();

        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }

    }
}
