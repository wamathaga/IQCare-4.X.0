using System;
using System.Globalization;
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
using Application.Common;
using Application.Presentation;
using Interface.Security;
using Interface.Administration;

public partial class frmPatient_History : BasePage
{
    public string PId, PtnSts, DQ;
    int LocationID;

    private void FormIQCare(DataSet theDS)
    {
        IPatientHome PatientManager;
        PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        int tmpYear = 0;
        int tmpMonth = 0;
        TreeNode root = new TreeNode();
        TreeNode theMRoot = new TreeNode();
        bool flagyear = true;
        int PtnPMTCTStatus = 0;
        int PtnARTStatus = 0;
        if (PtnSts == "0" || PtnSts == "")
        {
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
                if ((theStatusDT.Rows[0]["PMTCTStatus"].ToString() == "PMTCT Care Ended") || (PatientExitReason == "93" && PMTCTCareEnded == "1"))
                    PtnPMTCTStatus = 1;
                else
                    PtnPMTCTStatus = 0;
                if ((theStatusDT.Rows[0]["ART/PalliativeCare"].ToString() == "Care Ended") || (PatientExitReason == "93" && CareEnded == "1"))
                    PtnARTStatus = 1;
                else
                    PtnARTStatus = 0;
            }
        }
        else
        {
            PtnPMTCTStatus = 1;
            PtnARTStatus = 1;
        }

        foreach (DataRow theDR in theDS.Tables[1].Rows)
        {

            if (((DateTime)theDR["TranDate"]).Year != 1900)
            {
                DQ = "";
                if (tmpYear != ((DateTime)theDR["TranDate"]).Year)
                {
                    root = new TreeNode();
                    root.Text = ((DateTime)theDR["TranDate"]).Year.ToString();
                    root.Value = "";
                    if (flagyear)
                    {
                        root.Expand();
                        flagyear = false;
                    }
                    else
                    {
                        root.Collapse();
                    }
                    TreeViewExisForm.Nodes.Add(root);
                    tmpYear = ((DateTime)theDR["TranDate"]).Year;
                    tmpMonth = 0;
                }

                if (tmpYear == ((DateTime)theDR["TranDate"]).Year && tmpMonth != ((DateTime)theDR["TranDate"]).Month)
                {
                    theMRoot = new TreeNode();
                    theMRoot.Text = ((DateTime)theDR["TranDate"]).ToString("MMMM");
                    theMRoot.Value = "";
                    root.ChildNodes.Add(theMRoot);
                    tmpMonth = ((DateTime)theDR["TranDate"]).Month;
                }

                if (theDR["DataQuality"].ToString() == "1")
                {
                    DQ = "Data Quality Done";

                }

                if (tmpYear == ((DateTime)theDR["TranDate"]).Year && tmpMonth == ((DateTime)theDR["TranDate"]).Month)
                {
                    this.LocationID = Convert.ToInt32(theDS.Tables[0].Rows[0]["LocationID"].ToString());
                    TreeNode theFrmRoot = new TreeNode();
                    theFrmRoot.Text = theDR["FormName"].ToString() + " ( " + ((DateTime)theDR["TranDate"]).ToString(Session["AppDateFormat"].ToString()) + " )";
                    if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || theDR["FormName"].ToString() == "Patient Registration")
                    {
                        if (DQ != "")
                        {
                            theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                            Session["FormStatus"] = "Completed";
                        }
                        else
                        {
                            theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(theDR["Module"]) > 2)
                        {

                            DataSet formLinking = PatientManager.GetLinkedForms_FormLinking(Convert.ToInt32(Session["TechnicalAreaId"]), Convert.ToInt32(theDR["FeatureID"]));

                            if (formLinking.Tables[0].Rows.Count > 0)
                            {
                                if (DQ != "")
                                {
                                    theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                    Session["FormStatus"] = "Completed";
                                }
                                else
                                {
                                    theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                                }
                            }
                            else
                            {
                                theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                                theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                                theFrmRoot.SelectAction = TreeNodeSelectAction.None;
                            }
                        }
                        else if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"])
                            || (theDR["FormName"].ToString() == "Pharmacy")
                            || (theDR["FormName"].ToString() == "Order Labs")                            
                            || (theDR["FormName"].ToString() == "Paediatric Pharmacy")
                            || (theDR["FormName"].ToString() == "Clinical Encounter")
                            || (theDR["FormName"].ToString() == "Refill Encounter")
                            || (theDR["FormName"].ToString() == "Adherence Barriers")
                            || (theDR["FormName"].ToString() == "Enhance Adherence Counselling")
                            )
                        {
                            if ((theDR["FormName"].ToString() == "Pharmacy") || (theDR["FormName"].ToString() == "Order Labs")
                                || (theDR["FormName"].ToString() == "Paediatric Pharmacy"))
                            {
                                if (theDR["CAUTION"].ToString() == "1")
                                {
                                    theFrmRoot.ImageUrl = "~/images/caution.png";
                                }
                                else if (theDR["CAUTION"].ToString() == "2")
                                {
                                    theFrmRoot.ImageUrl = "~/images/partial.png";
                                }
                                else
                                {
                                    theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                    Session["FormStatus"] = "Completed";
                                }
                            }

                            if (theDR["FormName"].ToString() == "Clinical Encounter")
                            {
                                if (theDR["DataQuality"].ToString() == "1")
                                {
                                    theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                    Session["FormStatus"] = "Completed";
                                }
                                else
                                {
                                    theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                                }
                            }
                            if (theDR["FormName"].ToString() == "Refill Encounter")
                            {
                                if (theDR["DataQuality"].ToString() == "1")
                                {
                                    theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                    Session["FormStatus"] = "Completed";
                                }
                                else
                                {
                                    theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                                }
                            }
                            if (theDR["FormName"].ToString() == "Adherence Barriers")
                            {
                                if (theDR["DataQuality"].ToString() == "1")
                                {
                                    theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                    Session["FormStatus"] = "Completed";
                                }
                                else
                                {
                                    theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                                }
                            }
                            if (theDR["FormName"].ToString() == "Enhance Adherence Counselling")
                            {
                                if (theDR["DataQuality"].ToString() == "1")
                                {
                                    theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                    Session["FormStatus"] = "Completed";
                                }
                                else
                                {
                                    theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                                }
                            }
                            /*if (Session["Paperless"].ToString() == "1")
                            {
                                

                            }


                            else
                            {
                                if (DQ != "")
                                {
                                    theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                }
                                else
                                {
                                    theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                                }
                            }*/
                        }
                        else
                        {
                            theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                            theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                            theFrmRoot.SelectAction = TreeNodeSelectAction.None;
                        }
                    }

                    //if (theFrmRoot.Text.Contains("HIVCE -"))
                    //{
                    //    if (theFrmRoot.Text.Contains("HIVCE - ART"))
                    //    {
                    //        theFrmRoot.NavigateUrl = "~/HIVCE/ARTReadinessAssessment.aspx";
                    //    }
                    //    if (theFrmRoot.Text.Contains("HIVCE - Transition"))
                    //    {
                    //        theFrmRoot.NavigateUrl = "~/HIVCE/Transition.aspx";
                    //    }

                    //}
                    //else
                    //{
                    theFrmRoot.NavigateUrl = "";
                    //}



                    theFrmRoot.Value = Convert.ToInt32(PId) + "%" + theDR["OrderNo"].ToString() + "%" + theDR["LocationID"].ToString() + "%" + PtnARTStatus + "%" + theDR["Module"].ToString() + "%" + theDR["FormName"].ToString();
                    theMRoot.ChildNodes.Add(theFrmRoot);
                }
            }

        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Patient History";
        //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Existing Forms";

        (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = (Session["PatientStatus"] != null) ? Convert.ToString(Session["PatientStatus"]) : Convert.ToString(Request.QueryString["sts"]);

        #region "Refresh Patient Records"
        IPatientHome PManager;
        PManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
        System.Data.DataSet thePDS = PManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
        //System.Data.DataSet thePDS = PManager.GetPatientDetails(Convert.ToInt32(Request.QueryString["PatientId"]), Convert.ToInt32(Session["SystemId"]));

        Session["PatientInformation"] = thePDS.Tables[0];
        #endregion

        IPatientHome PatientManager;
        PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");

        try
        {
            if (!IsPostBack)
            {
                IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
                //theFacilityDS = FacilityMaster.GetFacilityList(Convert.ToInt32(Session["SystemId"]), 1001);
                //theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), 1001, 0);
                //ViewState["FacilityDS"] = theFacilityDS;
                //SetPageLabels();
                //******************************************************//
                if (Session["PatientId"] == null || Convert.ToString(Session["PatientId"]) == "0")
                {
                    Session["PatientId"] = Request.QueryString["PatientId"];  //remove it after session of patient set on find add when patient selected from grid.
                }

                PId = Convert.ToString(Session["PatientId"]);
                PtnSts = Convert.ToString(Session["PatientStatus"]); //Request.QueryString["sts"].ToString();
                if (Session["PatientId"] != null && Convert.ToInt32(Session["PatientId"]) != 0)
                {
                    PId = Session["PatientId"].ToString();
                }
                if (Session["PatientStatus"] != null)
                {
                    PtnSts = Session["PatientStatus"].ToString();
                }
                DataSet theDS = PatientManager.GetPatientHistory(Convert.ToInt32(PId));
                //this.lblpatientname.Text = theDS.Tables[0].Rows[0]["Name"].ToString();
                //this.lblpatientenrolment.Text = theDS.Tables[0].Rows[0]["PatientId"].ToString();
                //this.lblexisclinicid.Text = theDS.Tables[0].Rows[0]["PatientClinicID"].ToString();
                ViewState["theCFDT"] = theDS.Tables[3].Copy();
                Session["FormStatus"] = "Pending";
                FormIQCare(theDS);
            }
        }

        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
            return;
        }
        finally
        {
            PatientManager = null;
        }


    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        string theUrl;
        theUrl = string.Format("{0}", "frmPatient_Home.aspx");
        Response.Redirect(theUrl);
    }

    private void SetPageLabels()
    {
        DataTable theDT = ((DataSet)ViewState["FacilityDS"]).Tables[1];
        if (theDT.Rows.Count > 0)
        {
            // lblFileRef.InnerHtml = theDT.Rows[0]["Label"].ToString();
        }
    }
    protected void TreeViewExisForm_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (TreeViewExisForm.SelectedNode.Value == "")
            return;

        string[] theName = TreeViewExisForm.SelectedNode.Text.Split('(');
        string[] theValue = TreeViewExisForm.SelectedNode.Value.Split('%');
        string url = "";
        string PgName;
        Session["PatientId"] = Convert.ToInt32(theValue[0]);
        Session["PatientVisitId"] = Convert.ToInt32(theValue[1]);
        Session["ServiceLocationId"] = Convert.ToInt32(theValue[2]);
        Session["PatientStatus"] = Convert.ToInt32(theValue[3]);
        Session["CEModule"] = theValue[4].ToString();

        Session["Redirect"] = "1";
        AuthenticationManager Authentication = new AuthenticationManager();

        DataTable filteresdDT = new DataTable();
        if (!object.Equals(Session["FilteredUserRight"], null))
            filteresdDT = (DataTable)Session["FilteredUserRight"];


        switch (theName[0].Trim())
        {
            case "Patient Registration":
                //url = string.Format("{0}", "~/frmPatientRegistration.aspx");
                url = string.Format("{0}", "~/frmPatientCustomRegistration.aspx");
                Response.Redirect(url);
                break;

            case "HIV-Enrollment":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                {
                    if (Session["SystemId"].ToString() == "1")
                    { PgName = "frmClinical_Enrolment.aspx"; }
                    else { PgName = "frmClinical_PatientRegistrationCTC.aspx"; }
                    url = string.Format("{0}", "" + PgName + "");
                    Response.Redirect(url);
                }
                break;

            case "Initial Evaluation":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                {
                    url = string.Format("{0}", "./frmClinical_InitialEvaluation.aspx");
                    Response.Redirect(url);
                }
                break;


            case "Prior ART/HIV Care":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 202)
                {
                    url = string.Format("{0}", "./frm_PriorArt_HivCare.aspx");
                    Response.Redirect(url);
                }
                break;

            case "ART Care":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 202)
                {
                    url = string.Format("{0}", "./frmClinical_ARTCare.aspx");
                    Response.Redirect(url);
                }
                break;

            //********************//
            //john - start
            case "ART Therapy":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 203)
                {
                    url = string.Format("{0}", "./frmClinical_ARVTherapy.aspx");
                    Response.Redirect(url);
                }
                break;

            case "Express":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 204)
                {
                    url = string.Format("{0}", "./frmClinical_KNH_ExpressForm.aspx");
                    Response.Redirect(url);
                }
                break;
            //john - end

            case "ART History":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 203)
                {
                    url = string.Format("{0}", "./frmClinical_ARTHistory.aspx");
                    Response.Redirect(url);
                }
                break;
            case "PEP":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 6)
                {
                    url = string.Format("{0}", "./frmClinical_KNH_PEP.aspx");
                    Response.Redirect(url);
                }
                break;
            case "Paediatric Initial Evaluation Form":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 204)
                {
                    url = string.Format("{0}", "./frmClinical_KNH_Paediatric_IE.aspx");
                    Response.Redirect(url);
                }
                break;
            case "Adult Initial Evaluation Form":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 204)
                {
                    url = string.Format("{0}", "./frmClinical_KNH_AdultIE.aspx");
                    Response.Redirect(url);
                }
                break;
            case "Adult Follow up Form":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 204)
                {
                    url = string.Format("{0}", "./frmClinical_RevisedAdultfollowup.aspx");
                    Response.Redirect(url);
                }
                break;
            case "Paediatric Follow up Form":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 204)
                {
                    url = string.Format("{0}", "./frmClinical_KNH_PediatricFollowup.aspx");
                    Response.Redirect(url);
                }
                break;
            case "Pharmacy":

                if (Convert.ToString(Session["CareEndFlag"]) == "1" && Convert.ToString(Session["CareendedStatus"]) == "1")
                {
                    if (Session["SystemId"].ToString() == "1")
                    {
                        url = string.Format("{0}", "~/./PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx");
                        Response.Redirect(url);
                    }
                    else
                    {
                        url = string.Format("{0}", "~/./Pharmacy/frmPharmacy_CTC.aspx");
                        Response.Redirect(url);
                    }
                }
                else if (!Authentication.HasFeatureRight(ApplicationAccess.AdultPharmacy, filteresdDT))
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "You are Not Authorized to Access this Form.";
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                }
                else
                {
                    if (Session["SystemId"].ToString() == "1")
                    {
                        url = string.Format("{0}", "~/./PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx");
                        Response.Redirect(url);
                    }
                    else
                    {
                        url = string.Format("{0}", "~/./Pharmacy/frmPharmacy_CTC.aspx");
                        Response.Redirect(url);
                    }
                }
                break;
            case "Paediatric Pharmacy":
                //if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                //{
                url = string.Format("{0}", "~/./PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx");
                Response.Redirect(url);
                //}
                break;
            //case "Enrollment-PMTCT":
            //    if (Convert.ToInt32(Session["TechnicalAreaId"]) == 1)
            //    {
            //        url = string.Format("{0}", "frmClinical_EnrolmentPMTCT.aspx");
            //        Response.Redirect(url);
            //    }
            //    break;
            case "ART Follow-Up":
                //if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                //{
                url = string.Format("{0}", "./frmClinical_ARTFollowup.aspx");
                //theFrmRoot.NavigateUrl = url;
                Response.Redirect(url);
                //}
                break;
            case "HIV Care/ART Encounter":
                url = string.Format("{0}", "./frmClinical_HIVCareARTCardEncounter.aspx");
                Response.Redirect(url);
                break;
            case "Initial and Follow up Visits":
                url = string.Format("{0}", "./frmClinical_InitialFollowupVisit.aspx");
                Response.Redirect(url);
                break;
            case "Nigeria ART Care Visitation":
                url = string.Format("{0}", "./frmClinical_Nigeria_ARTCareVisitation.aspx");
                Response.Redirect(url);
                break;
            case "Order Labs":
                //url = string.Format("{0}", "~/./Order Labs/frmLabOrder.aspx");
                Session["LabOrderID"] = Session["PatientVisitId"];

                if (!Authentication.HasFeatureRight(ApplicationAccess.Laboratory, filteresdDT))
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "You are Not Authorized to Access this Form.";
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                }
                else
                {

                    url = string.Format("{0}", "~/./Laboratory/frm_Laboratory.aspx");
                    Response.Redirect(url);
                }
                break;

            case "Home Visit":
                url = string.Format("{0}", "~/./Scheduler/frmScheduler_HomeVisit.aspx");
                Response.Redirect(url);
                break;
            case "Non-ART Follow-Up":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                {
                    url = string.Format("{0}", "./frmClinical_NonARTFollowUp.aspx");
                    Response.Redirect(url);
                }
                break;
            case "Care Tracking":
                url = string.Format("{0}", "~/./Scheduler/frmScheduler_ContactCareTracking.aspx");
                Response.Redirect(url);
                // theFrmRoot.NavigateUrl = url;
                break;
            case "Nigeria Adult Initial Evaluation":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 209)
                {
                    url = string.Format("{0}", "./frmClinical_Nigeria_AdultIE.aspx");
                    Response.Redirect(url);
                }
                break;
            case "Nigeria Paediatric Initial Evaluation":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 209)
                {
                    url = string.Format("{0}", "./frmClinical_Nigeria_PeadIE.aspx");
                    Response.Redirect(url);
                }
                break;

            case "Nigeria ART Care Summary":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 209)
                {
                    url = string.Format("{0}", "./frmClinical_Nigeria_ARTCareSummary.aspx");
                    Response.Redirect(url);
                }
                break;

            case "Nigeria Initial Visit":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 209)
                {
                    url = string.Format("{0}", "./frmClinical_Nigeria_InitialVisit.aspx");
                    Response.Redirect(url);
                }
                break;

            case "HEI":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 1)
                {
                    //url = string.Format("{0}", "./frmClinical_KNH_HEI.aspx");
                    url = string.Format("{0}", "./frmClinical_KNH_HEI_New.aspx");
                    Response.Redirect(url);
                }
                break;
            case "DCC Adult Initial Evaluation Form":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 210)
                {
                    url = string.Format("{0}", "./frmClinical_KNH_AdultIE.aspx");
                    Response.Redirect(url);
                }
                break;
            case "DCC Revised Adult Follow up Form":
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 210)
                {
                    url = string.Format("{0}", "./frmClinical_RevisedAdultfollowup.aspx");
                    Response.Redirect(url);
                }
                break;
            case "ANC":// Change MEI TO ANC By Rahmat because it was not redirected [15Feb2018]
                if (Convert.ToInt32(Session["TechnicalAreaId"]) == 1)
                {
                    //url = string.Format("{0}", "./frmClinical_KNH_MEI.aspx");
                    url = string.Format("{0}", "./frmClinical_ANC.aspx");
                    Response.Redirect(url);
                }
                break;
            case "HIVCE - Treatment Preparation":
                url = string.Format("{0}", "~/HIVCE/TreatmentPreparation.aspx");
                Response.Redirect(url);
                break;
            case "HVICE - ART Readiness Assessment":
                url = string.Format("{0}", "~/HIVCE/ARTReadinessAssessment.aspx");
                Response.Redirect(url);
                break;
            case "HVICE - Transition":
                url = string.Format("{0}", "~/HIVCE/Transition.aspx");
                Response.Redirect(url);
                break;
            case "HIVCE - Alcohol Depression Screening":
                url = string.Format("{0}", "~/HIVCE/AlcoholDepressionScreening.aspx");
                Response.Redirect(url);
                break;

            case "Clinical Encounter":
                url = string.Format("{0}", "~/HIVCE/ClinicalEncounter.aspx");
                Response.Redirect(url);
                break;

            case "Enhance Adherence Counselling":
                url = string.Format("{0}", "~/Adherence/EnhanceAdherence.aspx");
                Response.Redirect(url);
                break;
            
            case "Green Card Form":
                //if (Convert.ToInt32(Session["TechnicalAreaId"]) == 203)
                //{
                url = string.Format("{0}", "~/HIVCE/frm_GreenCard.aspx");
                Response.Redirect(url);
                //}
                break;
            //default: break;
            case "Refill Encounter":
                url = string.Format("{0}", "~/HIVCE/RefillEncounterForm.aspx");
                Response.Redirect(url);
                break;
            case "Adherence Barriers":
                url = string.Format("{0}", "~/Adherence/AdherenceBarriers.aspx");
                Response.Redirect(url);
                break;

        }

        foreach (DataRow DRCustomFrm in ((DataTable)ViewState["theCFDT"]).Rows)
        {
            if (DRCustomFrm["FeatureName"].ToString() == theName[0].Trim())
            {
                DataView theDV = new DataView((DataTable)ViewState["theCFDT"]);
                theDV.RowFilter = "FeatureName='" + theName[0].Trim() + "'";
                DataTable dtview = theDV.ToTable();
                Session["FeatureID"] = Convert.ToString(dtview.Rows[0]["FeatureID"]);
                if (Authentication.HasFunctionRight(Convert.ToInt32(dtview.Rows[0]["FeatureID"]), FunctionAccess.View, (DataTable)Session["UserRight"]) == true)
                {
                    if (DRCustomFrm["FeatureName"].ToString().Length > 9)
                    {
                        if (DRCustomFrm["FeatureName"].ToString().Substring(0, 9) == "Pharmacy_")
                        {
                            url = string.Format("{0}", "~/./PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx");
                        }
                        else
                        {
                            url = string.Format("{0}", "./frmClinical_CustomForm.aspx");
                        }
                    }
                    else
                        url = string.Format("{0}", "./frmClinical_CustomForm.aspx");
                    Response.Redirect(url);
                }
                else
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "You are Not Authorized to Access this Form.";
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                }

            }
        }

        TreeViewExisForm.SelectedNode.NavigateUrl = url;

    }
}