using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
using System.Text;
using Interface.Administration;

namespace PresentationApp.ClinicalForms
{
    public partial class frmClinical_KNH_HEI : BasePage
    {
        int PatientID, LocationID, visitPK = 0;
        Hashtable htKNHHEIParameters;
        DataTable DTCheckedIds;
        string chktrueother = "";
        int chktrueothervalue = 0;
        IQCareUtils theUtils = new IQCareUtils();
        DataTable theDTGrid = new DataTable();
        DataView theDTView = new DataView();

        protected void Page_Init(object sender, EventArgs e)
        {
            BindLists();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "HEI Form";
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "HEI Form";
            if (Session["isHEIVisible"] != null || Convert.ToBoolean(Session["isHEIVisible"]) == false)
            {
                Session["isHEIVisible"] = true;
            }
            idVitalSign.txtBMI.Style.Add("display", "none");
            idVitalSign.lblBMI.Style.Add("display", "none"); ;
            idVitalSign.lblBMIClassification.Style.Add("display", "none");


            if (!IsPostBack)
            {
                if (theDTGrid != null && theDTGrid.Rows.Count > 0)
                {
                    theDTView = new DataView(theDTGrid);

                    theDTView.RowFilter = "Section = Neonatal History";
                    if (theDTView.Count > 0)
                    {
                        ViewState["GridNeonatalData"] = (DataTable)theUtils.CreateTableFromDataView(theDTView);
                    }

                    theDTView.RowFilter = "Section = Maternal History";
                    if (theDTView.Count > 0)
                    {
                        ViewState["GridMaternallData"] = (DataTable)theUtils.CreateTableFromDataView(theDTView);
                    }

                    theDTView.RowFilter = "Section = Immunization";
                    if (theDTView.Count > 0)
                    {
                        ViewState["GridImmunizationData"] = (DataTable)theUtils.CreateTableFromDataView(theDTView);
                    }

                    theDTView.RowFilter = "Section = Milestone";
                    if (theDTView.Count > 0)
                    {
                        ViewState["GridMilestoneData"] = (DataTable)theUtils.CreateTableFromDataView(theDTView);
                    }

                    theDTView.RowFilter = "Section = TBAssessment";
                    if (theDTView.Count > 0)
                    {
                        ViewState["GridTBAssessmentData"] = (DataTable)theUtils.CreateTableFromDataView(theDTView);
                    }
                }
                else
                {
                    ViewState["GridTBAssessmentData"] = theDTGrid;
                    ViewState["GridMilestoneData"] = theDTGrid;
                    ViewState["GridImmunizationData"] = theDTGrid;
                    ViewState["GridMaternalData"] = theDTGrid;
                    ViewState["GridNeonatalData"] = theDTGrid;
                }
            }


            Authenticate();
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Session["isHEIVisible"] = false;
        }
        protected void tabControl_ActiveTabChanged(object sender, EventArgs e)
        {
            AjaxControlToolkit.TabPanel activeTab = tabControl.ActiveTab;
            if (activeTab == TabClinicalHistory)
            {
                IQCareMsgBox.NotifyActionTab("Please Update Immunization and Vitamin A, if requires in subsequent visit.", "HEI Form", false, this, tabControl, "0");
                //Below two line uncomment by Rahmat on 09-Jan-2017 for enable save button
                btnHIVHistorySave.Enabled = true;
                btncloseHIVHistory.Enabled = true;
            }

        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                if (!IsPostBack)
                {
                    KNHPMTCTHEIData();
                }
            }
            if (ddlPlaceofDelivery.SelectedItem != null)
            {
                if (ddlPlaceofDelivery.SelectedItem.Text == "Other facility")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "spnotherfacility", "show('spnotherfacility');", true);
                }
                if (ddlPlaceofDelivery.SelectedItem.Text == "Other Specify")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "spanotherdelivery", "show('spanotherdelivery');", true);
                }
            }

            if (ddlPlan.SelectedItem != null && ddlPlan.SelectedItem.Text != "Select")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spnRegimen", "show('spnRegimen');", true);
            }

            if (ddlARVProphylaxis.SelectedItem != null && ddlARVProphylaxis.SelectedItem.Text == "Other Specify")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spnotherARVProphy", "show('spnotherARVProphy');", true);
            }

            if (ddlIfeedingoption.SelectedItem != null && ddlIfeedingoption.SelectedItem.Text == "Other")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spnotherfeedingoption", "show('spnotherfeedingoption');", true);
            }
            if (ddlmothersANCFU.SelectedItem != null && ddlmothersANCFU.SelectedItem.Text == "Other Facility")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spnANCFollowup", "show('spnANCFollowup');", true);
            }
            if (ddlReferred.SelectedItem != null && ddlReferred.SelectedItem.Text == "Other")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spnReferredto", "show('spnReferredto');", true);
            }
            if (ddlVisitType.SelectedItem != null)
            {
                if (ddlVisitType.SelectedItem.Text == "Express")
                {
                    showhideExpress();
                }
                else if (ddlVisitType.SelectedItem.Text == "Full Visit")
                {
                    showhideFullVisit();
                }
            }



        }

        private Hashtable htableKNHHEIParameters()
        {
            htKNHHEIParameters = new Hashtable();
            htKNHHEIParameters.Add("KNHHEIVisitDate", txtVisitDate.Value);
            htKNHHEIParameters.Add("KNHHEIVisitType", ddlVisitType.SelectedValue);

            //Vital Sign
            htKNHHEIParameters.Add("KNHHEITemp", idVitalSign.txtTemp.Text != "" ? idVitalSign.txtTemp.Text : "999");
            htKNHHEIParameters.Add("KNHHEIRR", idVitalSign.txtRR.Text != "" ? idVitalSign.txtRR.Text : "999");
            htKNHHEIParameters.Add("KNHHEIHR", idVitalSign.txtHR.Text != "" ? idVitalSign.txtHR.Text : "999");
            htKNHHEIParameters.Add("KNHHEIBPSystolic", idVitalSign.txtBPSystolic.Text != "" ? idVitalSign.txtBPSystolic.Text : "999");
            htKNHHEIParameters.Add("KNHHEIBPDiastolic", idVitalSign.txtBPDiastolic.Text != "" ? idVitalSign.txtBPDiastolic.Text : "999");
            htKNHHEIParameters.Add("KNHHEIHeight", idVitalSign.txtHeight.Text != "" ? idVitalSign.txtHeight.Text : "999");
            htKNHHEIParameters.Add("KNHHEIWeight", idVitalSign.txtWeight.Text != "" ? idVitalSign.txtWeight.Text : "999");
            htKNHHEIParameters.Add("KNHHEIHeadCircum", idVitalSign.txtheadcircumference.Text != "" ? idVitalSign.txtheadcircumference.Text : "999");
            htKNHHEIParameters.Add("KNHHEIWA", idVitalSign.lblWA.Text != "" ? idVitalSign.lblWA.Text : "999");
            htKNHHEIParameters.Add("KNHHEIWH", idVitalSign.lblWH.Text != "" ? idVitalSign.lblWH.Text : "999");
            htKNHHEIParameters.Add("KNHHEIBMIz", idVitalSign.lblBMIz.Text != "" ? idVitalSign.lblBMIz.Text : "999");
            htKNHHEIParameters.Add("KNHHEINurseComments", idVitalSign.txtnursescomments.Text != "" ? idVitalSign.txtnursescomments.Text : string.Empty);
            htKNHHEIParameters.Add("KNHHEIReferToSpecialClinic", idVitalSign.txtReferToSpecialistClinic.Text != "" ? idVitalSign.txtReferToSpecialistClinic.Text : string.Empty);
            htKNHHEIParameters.Add("KNHHEIReferToOther", idVitalSign.txtSpecifyOtherRefferedTo.Text != "" ? idVitalSign.txtSpecifyOtherRefferedTo.Text : string.Empty);

            //Neonatal History
            htKNHHEIParameters.Add("KNHHEISrRefral", txtSourceofReferral.Text);
            htKNHHEIParameters.Add("KNHHEIPlDelivery", ddlPlaceofDelivery.SelectedValue);
            htKNHHEIParameters.Add("KNHHEIPlDeliveryotherfacility", txtOtherFacility.Text);
            htKNHHEIParameters.Add("KNHHEIPlDeliveryother", txtOtherDelivery.Text);
            htKNHHEIParameters.Add("KNHHEIMdDelivery", ddlModeofDelivery.SelectedValue);
            htKNHHEIParameters.Add("KNHHEIBWeight", txtBirthWeight.Text != "" ? txtBirthWeight.Text : "999");
            htKNHHEIParameters.Add("KNHHEIARVProp", ddlARVProphylaxis.SelectedValue);
            htKNHHEIParameters.Add("KNHHEIARVPropOther", txtOtherARVProphylaxis.Text);
            htKNHHEIParameters.Add("KNHHEIIFeedoption", ddlIfeedingoption.SelectedValue);
            htKNHHEIParameters.Add("KNHHEIIFeedoptionother", txtOtherFeedingoption.Text);
            //Neonatal Grid
            //htKNHHEIParameters.Add("KNHHEITypeofTest", ddlTypeofTest.SelectedValue);
            //htKNHHEIParameters.Add("KNHHEIResult", ddlTestResults.SelectedValue);
            //htKNHHEIParameters.Add("KNHHEIResultGiven", txttestresultsgiven.Value);
            //htKNHHEIParameters.Add("KNHHEINNComments", txtcomments.Text);


            //Maternal History
            htKNHHEIParameters.Add("KNHHEIStateofMother", ddlStateofMother.SelectedValue);
            htKNHHEIParameters.Add("KNHHEIMRegisthisclinic", rdoMotherRegisYes.Checked == true ? 1 : rdoMotherRegisNo.Checked == true ? 0 : 2);
            htKNHHEIParameters.Add("KNHHEIPlMFollowup", ddlmothersANCFU.SelectedValue);
            htKNHHEIParameters.Add("KNHHEIPlMFollowupother", txtmotherANCfollowup.Text);
            htKNHHEIParameters.Add("KNHHEIMRecievedDrug", rdMotherRDrugYes.Checked == true ? 1 : rdMotherRDrugNo.Checked == true ? 0 : 2);
            htKNHHEIParameters.Add("KNHHEIOnARTEnrol", rdoARTEnrolYes.Checked == true ? 1 : rdoARTEnrolNo.Checked == true ? 0 : 2);
            //Maternal Grid
            //htKNHHEIParameters.Add("KNHHEIMTestDone", ddlTestDone.SelectedValue);
            //htKNHHEIParameters.Add("KNHHEIMTestResult", txtresultmother.Text);
            //htKNHHEIParameters.Add("KNHHEIMTestResultGiven", txtresultmothergiven.Value);
            //htKNHHEIParameters.Add("KNHHEIMRemarks", txtRemarks.Text);

            //Immunization Grid
            //htKNHHEIParameters.Add("KNHHEIDateImmunised", txtDateImmunised.Value);
            //htKNHHEIParameters.Add("KNHHEIPeriodImmunised", ddlImmunisationPeriod.SelectedValue);
            //htKNHHEIParameters.Add("KNHHEIGivenImmunised", ddImmunisationgiven.SelectedValue);

            //Presenting Complaints
            htKNHHEIParameters.Add("KNHHEIAdditionalComplaint", UcHEIPcomplaints.txtAdditionalComplaints.Text);

            //Examination, Milestone and Diagnosis
            htKNHHEIParameters.Add("KNHHEIExamination", txtExamination.Text);
            //Milestone Grid
            //htKNHHEIParameters.Add("KNHHEIMilestonesDuration", ddlDuration.SelectedValue);
            //htKNHHEIParameters.Add("KNHHEIMilestones", ddlStatus.SelectedValue);
            //htKNHHEIParameters.Add("KNHHEIMilestonesComment", txtComment.Text);
            //Diagnosis
            //-------
            //-------

            //Management Plan
            htKNHHEIParameters.Add("KNHHEIVitamgiven", rdoHasVitaminGivenYes.Checked == true ? 1 : rdoHasVitaminGivenNo.Checked == true ? 0 : 2);
            htKNHHEIParameters.Add("KNHHEIWorkPlan", txtAreaWorkPlan.Value);
            // Plan Grid
            //htKNHHEIParameters.Add("KNHHEIPlan", ddlPlan.SelectedValue);
            //htKNHHEIParameters.Add("KNHHEIPlanRegimen", ddlRegimen.SelectedValue);
            //htKNHHEIParameters.Add("KNHHEIPlanRegimenTreatmentDt", txtTreatmentDate.Value);

            //Referral, Admission and Appointment
            htKNHHEIParameters.Add("KNHHEIReferredto", ddlReferred.SelectedValue);
            htKNHHEIParameters.Add("KNHHEIReferredtoother", txtOtherReferredto.Text);
            htKNHHEIParameters.Add("KNHHEIAdmittoward", rdoadmittowardyes.Checked == true ? 1 : rdoadmittowardno.Checked == true ? 0 : 2);
            htKNHHEIParameters.Add("KNHHEITCA", UserControlKNH_NextAppointment.rdoTCAYes.Checked == true ? 1 : UserControlKNH_NextAppointment.rdoTCANo.Checked == true ? 0 : 2);
            return htKNHHEIParameters;
        }

        protected void KNHPMTCTHEIData()
        {
            IKNHHEI KNHManager;
            KNHManager = (IKNHHEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHHEI, BusinessProcess.Clinical");
            try
            {
                PatientID = Convert.ToInt32(Session["PatientId"]);
                DataSet theDS = KNHManager.GetKNHPMTCTHEI(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));
                if (theDS.Tables[0].Rows.Count > 0 && theDS.Tables[0].Rows[0]["Visit_Id"] != System.DBNull.Value)
                {
                    // Session["PatientVisitId"] = theDS.Tables[0].Rows[0]["Visit_Id"].ToString();
                    visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                    btnHIVHistorySave.Enabled = true;
                    btncloseHIVHistory.Enabled = true;
                }
                else
                    Session["PatientVisitId"] = 0;

                if (theDS.Tables[0].Rows.Count > 0)
                {
                    if (theDS.Tables[0].Rows[0]["VisitDate"] != System.DBNull.Value)
                    {
                        this.txtVisitDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["VisitDate"]).ToUpper();
                    }
                    if (theDS.Tables[0].Rows[0]["TypeofVisit"] != System.DBNull.Value)
                    {
                        ddlVisitType.SelectedValue = theDS.Tables[0].Rows[0]["TypeofVisit"].ToString();
                        if (ddlVisitType.SelectedItem.Text == "Express")
                        {
                            showhideExpress();
                        }
                        else if (ddlVisitType.SelectedItem.Text == "Full Visit")
                        {
                            showhideFullVisit();
                        }
                    }
                }
                // Vital Sign
                if (theDS.Tables[2].Rows.Count > 0)
                {
                    if (theDS.Tables[2].Rows[0]["TEMP"] != System.DBNull.Value)
                    {
                        idVitalSign.txtTemp.Text = theDS.Tables[2].Rows[0]["TEMP"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["RR"] != System.DBNull.Value)
                    {
                        idVitalSign.txtRR.Text = theDS.Tables[2].Rows[0]["RR"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["HR"] != System.DBNull.Value)
                    {
                        idVitalSign.txtHR.Text = theDS.Tables[2].Rows[0]["HR"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["Height"] != System.DBNull.Value)
                    {
                        idVitalSign.txtHeight.Text = theDS.Tables[2].Rows[0]["Height"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["Weight"] != System.DBNull.Value)
                    {
                        idVitalSign.txtWeight.Text = theDS.Tables[2].Rows[0]["Weight"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["BPSystolic"] != System.DBNull.Value)
                    {
                        idVitalSign.txtBPSystolic.Text = theDS.Tables[2].Rows[0]["BPSystolic"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["BPDiastolic"] != System.DBNull.Value)
                    {
                        idVitalSign.txtBPDiastolic.Text = theDS.Tables[2].Rows[0]["BPDiastolic"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["Headcircumference"] != System.DBNull.Value)
                    {
                        idVitalSign.txtheadcircumference.Text = theDS.Tables[2].Rows[0]["Headcircumference"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["WeightForAge"] != System.DBNull.Value)
                    {
                        idVitalSign.lblWA.Text = theDS.Tables[2].Rows[0]["WeightForAge"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["WeightForHeight"] != System.DBNull.Value)
                    {
                        idVitalSign.lblWH.Text = theDS.Tables[2].Rows[0]["WeightForHeight"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["BMIz"] != System.DBNull.Value)
                    {
                        idVitalSign.lblBMIz.Text = theDS.Tables[2].Rows[0]["BMIz"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["NurseComments"] != System.DBNull.Value)
                    {
                        idVitalSign.txtnursescomments.Text = theDS.Tables[2].Rows[0]["NurseComments"].ToString();
                    }
                }

                if (theDS.Tables[1].Rows.Count > 0)
                {
                    if (theDS.Tables[1].Rows[0]["ChildReferredFrom"] != System.DBNull.Value)
                    {
                        txtSourceofReferral.Text = theDS.Tables[1].Rows[0]["ChildReferredFrom"].ToString();
                    }
                    if (theDS.Tables[1].Rows[0]["DeliveryPlaceHEI"] != System.DBNull.Value)
                    {
                        ddlPlaceofDelivery.SelectedValue = theDS.Tables[1].Rows[0]["DeliveryPlaceHEI"].ToString();
                    }
                    if (theDS.Tables[1].Rows[0]["Deliveryotherfacility"] != System.DBNull.Value)
                    {
                        txtOtherFacility.Text = theDS.Tables[1].Rows[0]["Deliveryotherfacility"].ToString();
                    }
                    if (theDS.Tables[1].Rows[0]["Deliveryother"] != System.DBNull.Value)
                    {
                        txtOtherDelivery.Text = theDS.Tables[1].Rows[0]["Deliveryother"].ToString();
                    }
                    if (theDS.Tables[1].Rows[0]["ModeofDeliveryHEI"] != System.DBNull.Value)
                    {
                        ddlModeofDelivery.SelectedValue = theDS.Tables[1].Rows[0]["ModeofDeliveryHEI"].ToString();
                    }
                    if (theDS.Tables[1].Rows[0]["ChildPEPARVs"] != System.DBNull.Value)
                    {
                        ddlARVProphylaxis.SelectedValue = theDS.Tables[1].Rows[0]["ChildPEPARVs"].ToString();
                    }

                    if (theDS.Tables[1].Rows[0]["ARVPropOther"] != System.DBNull.Value)
                    {
                        txtOtherARVProphylaxis.Text = theDS.Tables[1].Rows[0]["ARVPropOther"].ToString();
                    }
                    //htparameter data....
                    if (theDS.Tables[1].Rows[0]["MotherRegisteredClinic"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[1].Rows[0]["MotherRegisteredClinic"].ToString() == "1")
                        {
                            rdoMotherRegisYes.Checked = true;
                            btnFind.Visible = true;
                            lblBtnFind.Visible = true;
                        }

                        else if (theDS.Tables[1].Rows[0]["MotherRegisteredClinic"].ToString() == "0")
                        {
                            rdoMotherRegisNo.Checked = true;
                            btnFind.Visible = false;
                            lblBtnFind.Visible = false;
                        };
                    }

                    if (theDS.Tables[1].Rows[0]["ANCFollowup"] != System.DBNull.Value)
                    {
                        ddlmothersANCFU.SelectedValue = theDS.Tables[1].Rows[0]["ANCFollowup"].ToString();
                    }

                    if (theDS.Tables[1].Rows[0]["PlMFollowupother"] != System.DBNull.Value)
                    {
                        txtmotherANCfollowup.Text = theDS.Tables[1].Rows[0]["PlMFollowupother"].ToString();
                    }

                    if (theDS.Tables[1].Rows[0]["MotherReferredtoARV"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[1].Rows[0]["MotherReferredtoARV"].ToString() == "1")
                        {
                            rdMotherRDrugYes.Checked = true;
                        }
                        else if (theDS.Tables[1].Rows[0]["MotherReferredtoARV"].ToString() == "0")
                        {
                            rdMotherRDrugNo.Checked = true;
                        }
                    }

                    if (theDS.Tables[1].Rows[0]["StateOfMother"] != System.DBNull.Value)
                    {
                        ddlStateofMother.SelectedValue = theDS.Tables[1].Rows[0]["StateOfMother"].ToString();
                    }

                    if (theDS.Tables[1].Rows[0]["OnART"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[1].Rows[0]["OnART"].ToString() == "1")
                        { rdoARTEnrolYes.Checked = true; }

                        else if (theDS.Tables[1].Rows[0]["OnART"].ToString() == "0")
                        { rdoARTEnrolNo.Checked = true; };
                    }
                    //if (theDS.Tables[1].Rows[0]["ImmunisationDate"] != System.DBNull.Value)
                    //{
                    //    this.txtDateImmunised.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[1].Rows[0]["ImmunisationDate"]);
                    //}
                    //if (theDS.Tables[1].Rows[0]["ImmunisationPeriod"] != System.DBNull.Value)
                    //{
                    //    ddlImmunisationPeriod.SelectedValue = theDS.Tables[1].Rows[0]["ImmunisationPeriod"].ToString();
                    //}
                    //if (theDS.Tables[1].Rows[0]["ImmunisationGiven"] != System.DBNull.Value)
                    //{
                    //    ddImmunisationgiven.SelectedValue = theDS.Tables[1].Rows[0]["ImmunisationGiven"].ToString();
                    //}
                    //if (theDS.Tables[1].Rows[0]["Plan"] != System.DBNull.Value)
                    //{
                    //    ddlPlan.SelectedValue = theDS.Tables[1].Rows[0]["Plan"].ToString();
                    //}
                    //if (theDS.Tables[1].Rows[0]["PlanRegimen"] != System.DBNull.Value)
                    //{
                    //    ddlRegimen.SelectedValue = theDS.Tables[1].Rows[0]["PlanRegimen"].ToString();
                    //}
                    if (theDS.Tables[1].Rows[0]["Examinations"] != System.DBNull.Value)
                    {
                        txtExamination.Text = theDS.Tables[1].Rows[0]["Examinations"].ToString();
                    }
                    //if (theDS.Tables[1].Rows[0]["MilestonesPeads"] != System.DBNull.Value)
                    //{
                    //    ddlStatus.SelectedValue = theDS.Tables[1].Rows[0]["MilestonesPeads"].ToString();
                    //}
                    if (theDS.Tables[1].Rows[0]["VitaminA"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[1].Rows[0]["VitaminA"].ToString() == "1")
                        { rdoHasVitaminGivenYes.Checked = true; }
                        else if (theDS.Tables[1].Rows[0]["VitaminA"].ToString() == "0")
                        { rdoHasVitaminGivenNo.Checked = true; };
                    }
                    if (theDS.Tables[1].Rows[0]["WorkPlan"] != System.DBNull.Value)
                    {
                        txtAreaWorkPlan.Value = theDS.Tables[1].Rows[0]["WorkPlan"].ToString();
                    }
                    if (theDS.Tables[1].Rows[0]["ReferralPeads"] != System.DBNull.Value)
                    {
                        ddlReferred.SelectedValue = theDS.Tables[1].Rows[0]["ReferralPeads"].ToString();
                    }
                    if (theDS.Tables[1].Rows[0]["Referredtoother"] != System.DBNull.Value)
                    {
                        txtOtherReferredto.Text = theDS.Tables[1].Rows[0]["Referredtoother"].ToString();
                    }
                    if (theDS.Tables[1].Rows[0]["WardAdmissionPead"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[1].Rows[0]["WardAdmissionPead"].ToString() == "1")
                        { rdoadmittowardyes.Checked = true; }

                        else if (theDS.Tables[1].Rows[0]["WardAdmissionPead"].ToString() == "0")
                        { rdoadmittowardno.Checked = true; };
                    }
                    if (theDS.Tables[1].Rows[0]["TCA"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[1].Rows[0]["TCA"].ToString() == "1")
                        { UserControlKNH_NextAppointment.rdoTCAYes.Checked = true; }
                        else if (theDS.Tables[1].Rows[0]["TCA"].ToString() == "0")
                        { UserControlKNH_NextAppointment.rdoTCANo.Checked = true; };
                    }
                    if (theDS.Tables[1].Rows[0]["AdditionalComplaint"] != System.DBNull.Value)
                    {
                        UcHEIPcomplaints.txtAdditionalComplaints.Text = theDS.Tables[1].Rows[0]["AdditionalComplaint"].ToString();
                    }

                }


                if (theDS.Tables[3].Rows.Count > 0)
                {
                    if (theDS.Tables[3].Rows[0]["BirthWeight"] != System.DBNull.Value)
                    {
                        txtBirthWeight.Text = theDS.Tables[3].Rows[0]["BirthWeight"].ToString();
                    }
                    if (theDS.Tables[3].Rows[0]["FeedingOption"] != System.DBNull.Value)
                    {
                        ddlIfeedingoption.SelectedValue = theDS.Tables[3].Rows[0]["FeedingOption"].ToString();
                    }
                    if (theDS.Tables[3].Rows[0]["FeedingoptionOther"] != System.DBNull.Value)
                    {
                        txtOtherFeedingoption.Text = theDS.Tables[3].Rows[0]["FeedingoptionOther"].ToString();
                    }

                }

                if (theDS.Tables[4].Rows.Count > 0)
                {
                    FillCheckBoxListData(theDS.Tables[4], PnlDiagnosis, "DiagnosisPeads", "Other_Notes");
                    FillCheckBoxListData(theDS.Tables[4], pnl2PComplaints, "PresentingComplaints", "Other_Notes");
                    FillCheckBoxListData(theDS.Tables[4]);
                }

                if (theDS.Tables[5].Rows.Count > 0)
                {
                    DataView dv = new DataView(theDS.Tables[5]);

                    // Neonatal 
                    dv.RowFilter = "Section = 'Neonatal History'";
                    DataTable dt = (DataTable)theUtils.CreateTableFromDataView(dv);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        GrdNNHistory.Columns.Clear();
                        BindGrid(GrdNNHistory, "Neonatal");
                        dt.TableName = "dtNNatal";
                        GrdNNHistory.DataSource = dt;
                        GrdNNHistory.DataBind();
                        ViewState["GridNeonatalData"] = dt;
                    }

                    // Maternal 
                    dv.RowFilter = "Section = 'Maternal History'";
                    dt = (DataTable)theUtils.CreateTableFromDataView(dv);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        GrdMMHistory.Columns.Clear();
                        BindGrid(GrdMMHistory, "Maternal");
                        dt.TableName = "dtMother";
                        GrdMMHistory.DataSource = dt;
                        GrdMMHistory.DataBind();
                        ViewState["GridMaternalData"] = dt;
                    }

                    // Immunization 
                    dv.RowFilter = "Section = 'Immunization'";
                    dt = (DataTable)theUtils.CreateTableFromDataView(dv);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        GrdImmunization.Columns.Clear();
                        BindGrid(GrdImmunization, "Immunization");
                        dt.TableName = "dtImmunization";
                        GrdImmunization.DataSource = dt;
                        GrdImmunization.DataBind();
                        ViewState["GridImmunizationData"] = dt;
                    }

                    // Milestone 
                    dv.RowFilter = "Section = 'Milestone'";
                    dt = (DataTable)theUtils.CreateTableFromDataView(dv);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gvMilestones.Columns.Clear();
                        BindGrid(gvMilestones, "Milestone");
                        dt.TableName = "dtMilestone";
                        gvMilestones.DataSource = dt;
                        gvMilestones.DataBind();
                        ViewState["GridMilestoneData"] = dt;
                    }

                    // TBAssessment 
                    dv.RowFilter = "Section = 'TBAssessment'";
                    dt = (DataTable)theUtils.CreateTableFromDataView(dv);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gvTB.Columns.Clear();
                        BindGrid(gvTB, "TBAssessment");
                        dt.TableName = "dtTBA";
                        gvTB.DataSource = dt;
                        gvTB.DataBind();
                        ViewState["GridTBAssessmentData"] = dt;
                    }
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
                KNHManager = null;
            }
        }

        private void BindLists()
        {
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx", true);
            }

            IPatientHome PatientManager;
            PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataSet thePatientDS = PatientManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
            PatientManager = null;
            if (thePatientDS != null && thePatientDS.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(thePatientDS.Tables[0].Rows[0]["AgeInMonths"]) > 24)
                {
                    IQCareMsgBox.NotifyAction("Patient Age should not be older than 24 months!", "HEI Form", true, this, "window.location.replace('frmPatient_Home.aspx');");
                    return;
                }
            }

            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            DataView theDVDecode = new DataView();
            DataTable theDTCode = new DataTable();
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            if (theDS.Tables["mst_pmtctdecode"] != null)
            {
                //Infant Feeding Option
                theDVDecode = new DataView(theDS.Tables["mst_pmtctdecode"]);

                //if ((Convert.ToInt32(thePatientDS.Tables[0].Rows[0]["AgeInMonths"]) > 6) && (Convert.ToInt32(thePatientDS.Tables[0].Rows[0]["AgeInMonths"]) <= 24))
                //{
                //    theDVDecode.RowFilter = "CodeName='FeedingOption' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1) and NAME IN ('Exclusive substitute feeding (ESF)', 'Other','less than 6/12 EBF','less than 6/12 ERF','less than 6/12 Mixed feeding','greater than 6/12 breast feeding','greater than 6/12 Not breastfeeding')";
                //}
                //else
                //{
                //    theDVDecode.RowFilter = "CodeName='FeedingOption' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1) and NAME IN ('Exclusive breast feeding - (EBF)','Replacement feeding - (EBMS)','Mixed feeding (MF)','less than 6/12 EBF','less than 6/12 ERF','less than 6/12 Mixed feeding','greater than 6/12 breast feeding','greater than 6/12 Not breastfeeding')";
                //}
                //Change by Rahmat[01Mar2018] after discussed by Devang and Wamathaga.
                if ((Convert.ToInt32(thePatientDS.Tables[0].Rows[0]["AgeInMonths"]) < 6))
                {
                    theDVDecode.RowFilter = "CodeName='FeedingOption' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1) and NAME IN ('Exclusive Breast Feeding','Replacement Feeding','Mixed Feeding','Other')";
                }
                else
                {
                    theDVDecode.RowFilter = "CodeName='FeedingOption' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1) and NAME IN ('Breastfeeding','Not Breastfeeding','Other')";
                }

                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlIfeedingoption, theDTCode, "Name", "Id");
                }

                //TB Assesment Outcome
                theDVDecode = new DataView(theDS.Tables["mst_pmtctdecode"]);
                theDVDecode.RowFilter = "CodeName='TBAssessmentoutcome' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo desc";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    //BindManager.BindCombo(ddlTBAssesment, theDTCode, "Name", "Id");
                    BindManager.BindCheckedList(cblTBAssesment, theDTCode, "Name", "ID");
                    cblTBAssesment.Attributes.Add("OnClick", "fnUncheckallVitals('" + cblTBAssesment.ClientID + "');");

                }

                //Immunization Period
                theDVDecode = new DataView(theDS.Tables["mst_pmtctdecode"]);
                theDVDecode.RowFilter = "CodeName='Immunisationperiod' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlImmunisationPeriod, theDTCode, "Name", "Id");

                }

                //Duration Period
                theDVDecode = new DataView(theDS.Tables["mst_pmtctdecode"]);
                theDVDecode.RowFilter = "CodeName='Immunisationperiod' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1) and Name NOT IN ('Birth','6 weeks','10 weeks','14 weeks','3 Months','15 Months')";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlDuration, theDTCode, "Name", "Id");

                }

                //Immunization given
                theDVDecode = new DataView(theDS.Tables["mst_pmtctdecode"]);
                theDVDecode.RowFilter = "CodeName='Immunisationgiven' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddImmunisationgiven, theDTCode, "Name", "Id");

                }

                //Plan
                theDVDecode = new DataView(theDS.Tables["mst_pmtctdecode"]);
                theDVDecode.RowFilter = "CodeName='Plan' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlPlan, theDTCode, "Name", "Id");
                }
                theDVDecode = new DataView(theDS.Tables["mst_pmtctdecode"]);
                theDVDecode.RowFilter = "CodeName='Regimen' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlRegimen, theDTCode, "Name", "Id");
                }
            }


            if (theDS.Tables["Mst_ModDecode"] != null)
            {
                //Visit Type
                theDVDecode = new DataView(theDS.Tables["Mst_ModDecode"]);
                theDVDecode.RowFilter = "CodeName='HEIType' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlVisitType, theDTCode, "Name", "Id");

                }
                //Place of Delivery
                theDVDecode = new DataView(theDS.Tables["Mst_ModDecode"]);
                theDVDecode.RowFilter = "CodeName='DeliveryPlaceHEI' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlPlaceofDelivery, theDTCode, "Name", "Id");

                }
                //Mode of Delivery
                theDVDecode = new DataView(theDS.Tables["Mst_ModDecode"]);
                theDVDecode.RowFilter = "CodeName='ModeofDeliveryHEI' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlModeofDelivery, theDTCode, "Name", "Id");

                }

                //ARV Prophylaxis
                theDVDecode = new DataView(theDS.Tables["Mst_ModDecode"]);
                theDVDecode.RowFilter = "CodeName='ChildPEPARVs' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlARVProphylaxis, theDTCode, "Name", "Id");

                }

                //Type of Test
                theDVDecode = new DataView(theDS.Tables["Mst_ModDecode"]);
                theDVDecode.RowFilter = "CodeName='TypeOfTest' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlTypeofTest, theDTCode, "Name", "Id");

                }

                //Place of mothers ANC follow up
                theDVDecode = new DataView(theDS.Tables["Mst_ModDecode"]);
                theDVDecode.RowFilter = "CodeName='ANCFollowUp' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlmothersANCFU, theDTCode, "Name", "Id");

                }

                //State of Mother
                theDVDecode = new DataView(theDS.Tables["Mst_ModDecode"]);
                theDVDecode.RowFilter = "CodeName='StateOfMother' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlStateofMother, theDTCode, "Name", "Id");

                }

                //Test of Mother
                theDVDecode = new DataView(theDS.Tables["Mst_ModDecode"]);
                theDVDecode.RowFilter = "CodeName='TypeOfTestMother' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlTestDone, theDTCode, "Name", "Id");

                }

                //Milestones assessment
                theDVDecode = new DataView(theDS.Tables["Mst_ModDecode"]);
                theDVDecode.RowFilter = "CodeName='MilestonesPeads' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlStatus, theDTCode, "Name", "Id");

                }

                //Diagnosis
                theDVDecode = new DataView(theDS.Tables["Mst_ModDecode"]);
                theDVDecode.RowFilter = "CodeName='DiagnosisPeads' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.CreateCheckedList(PnlDiagnosis, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");

                }

                //Referred to
                theDVDecode = new DataView(theDS.Tables["Mst_ModDecode"]);
                theDVDecode.RowFilter = "CodeName='ReferralPeads' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlReferred, theDTCode, "Name", "Id");

                }

                //Result
                theDVDecode = new DataView(theDS.Tables["Mst_Decode"]);
                theDVDecode.RowFilter = "DeleteFlag=0 and CodeID=10";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlTestResults, theDTCode, "Name", "Id");

                }

            }



        }

        private void SaveCancel()
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            //IQCareMsgBox.NotifyAction("HEI Form saved successfully. Do you want to close?", "HEI Form", false, this, "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';");
            IQCareMsgBox.NotifyActionTab("HEI Triage and Neonatal History tab saved successfully.<br> Move to next Tab?", "HEI Form", false, this, tabControl, "0");
        }

        private void SaveCancelClinicalReview()
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            IQCareMsgBox.NotifyAction("HEI Form saved successfully. Do you want to close?", "HEI Form", false, this, "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';");
        }

        private DataTable GetCheckBoxListcheckedIDs(Panel thePnl, string FieldName, string thetxtFieldName, int Flag, string optionalstr = "default string")
        {
            if (Flag == 0)
            {
                DTCheckedIds = new DataTable();
                if (DTCheckedIds.Columns.Contains(FieldName) == false && DTCheckedIds.Columns.Contains(FieldName) == false)
                {
                    DataColumn dataColumn = new DataColumn(FieldName);
                    dataColumn.DataType = System.Type.GetType("System.Int32");
                    DTCheckedIds.Columns.Add(dataColumn);
                    if (thetxtFieldName != "")
                    {
                        DataColumn dataColumn_Other = new DataColumn(thetxtFieldName);
                        dataColumn_Other.DataType = System.Type.GetType("System.String");
                        DTCheckedIds.Columns.Add(dataColumn_Other);
                    }
                }

            }
            DataRow theDR;
            if (thePnl.ID == "pnl2PComplaints")
            {
                for (int i = 0; i < UcHEIPcomplaints.gvPresentingComplaints.Rows.Count; i++)
                {
                    Label lblPComplaintsId = (Label)UcHEIPcomplaints.gvPresentingComplaints.Rows[i].FindControl("lblPresenting");
                    CheckBox chkPComplaints = (CheckBox)UcHEIPcomplaints.gvPresentingComplaints.Rows[i].FindControl("ChkPresenting");
                    TextBox txtPComplaints = (TextBox)UcHEIPcomplaints.gvPresentingComplaints.Rows[i].FindControl("txtPresenting");
                    if (chkPComplaints.Checked == true)
                    {

                        theDR = DTCheckedIds.NewRow();
                        theDR[FieldName] = Convert.ToString(lblPComplaintsId.Text);
                        theDR[thetxtFieldName] = Convert.ToString(txtPComplaints.Text);
                        DTCheckedIds.Rows.Add(theDR);

                    }
                }
            }
            {
                foreach (Control y in thePnl.Controls)
                {
                    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                        GetCheckBoxListcheckedIDs((System.Web.UI.WebControls.Panel)y, FieldName, thetxtFieldName, 1);
                    else
                    {
                        if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                        {
                            if (((CheckBox)y).Checked == true)
                            {
                                string[] theControlId = ((CheckBox)y).ID.ToString().Split('-');
                                if (((CheckBox)y).Text.Contains("Other") == true)
                                {
                                    chktrueother = theControlId[1].ToString();
                                    chktrueothervalue = Convert.ToInt32(theControlId[1].ToString());
                                }
                                else
                                {
                                    theDR = DTCheckedIds.NewRow();
                                    theDR[FieldName] = theControlId[1].ToString();
                                    DTCheckedIds.Rows.Add(theDR);
                                }
                            }
                        }
                        if (y.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                        {
                            if (thetxtFieldName != "")
                            {
                                if (((System.Web.UI.WebControls.TextBox)y).ID.Contains("OtherTXT") == true)
                                {
                                    theDR = DTCheckedIds.NewRow();
                                    string[] theControlId = ((TextBox)y).ID.ToString().Split('-');
                                    theDR[FieldName] = chktrueothervalue.ToString();
                                    if (((TextBox)y).Text != "")
                                    {
                                        theDR[thetxtFieldName] = ((TextBox)y).Text;
                                        DTCheckedIds.Rows.Add(theDR);
                                    }

                                }
                                string script = "";
                                script = "<script language = 'javascript' defer ='defer' id = " + ((TextBox)y).ID + ">\n";
                                script += "show('txt" + chktrueothervalue.ToString() + "');\n";
                                script += "</script>\n";
                                RegisterStartupScript("" + ((TextBox)y).ID + "", script);
                            }
                            chktrueother = "";
                            chktrueothervalue = 0;
                        }
                    }
                }
            }
            return DTCheckedIds;
        }

        public DataTable GetCheckBoxListcheckedIDs(System.Web.UI.WebControls.CheckBoxList cbl)
        {
            DataTable dtCbl = new DataTable(); ;
            DataColumn ID = new DataColumn("ID", System.Type.GetType("System.Int32"));
            dtCbl.Columns.Add(ID);
            DataRow dr;

            for (int i = 0; i < cbl.Items.Count; i++)
            {
                if (cbl.Items[i].Selected)
                {
                    dr = dtCbl.NewRow();
                    dr["ID"] = Convert.ToInt32(cbl.Items[i].Value);
                    dtCbl.Rows.Add(dr);
                }
            }
            return dtCbl;
        }

        private void FillCheckBoxListData(DataTable theDT)
        {
            if (theDT != null && theDT.Rows.Count > 0)
            {
                DataRow[] foundRow = theDT.Select("FieldName = 'VitalSign'");
                if (foundRow.Length > 0)
                {
                    foreach (System.Web.UI.WebControls.ListItem item in idVitalSign.cblReferredTo.Items)
                    {
                        foreach (DataRow dr in foundRow)
                        {
                            if (item.Value == dr[1].ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }

                foundRow = theDT.Select("FieldName = 'TBAssessment'");
                if (foundRow.Length > 0)
                {
                    foreach (System.Web.UI.WebControls.ListItem item in cblTBAssesment.Items)
                    {
                        foreach (DataRow dr in foundRow)
                        {
                            if (item.Value == dr[1].ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
            }
        }

        private void FillCheckBoxListData(DataTable theDT, Panel thePnl, string FieldName, string theFieldName)
        {
            try
            {

                if (thePnl.ID == "pnl2PComplaints")
                {
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        for (int i = 0; i < this.UcHEIPcomplaints.gvPresentingComplaints.Rows.Count; i++)
                        {
                            Label lblPComplaintsId = (Label)UcHEIPcomplaints.gvPresentingComplaints.Rows[i].FindControl("lblPresenting");
                            CheckBox chkPComplaints = (CheckBox)UcHEIPcomplaints.gvPresentingComplaints.Rows[i].FindControl("ChkPresenting");
                            TextBox txtPComplaints = (TextBox)UcHEIPcomplaints.gvPresentingComplaints.Rows[i].FindControl("txtPresenting");
                            if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblPComplaintsId.Text))
                            {
                                chkPComplaints.Checked = true;
                                txtPComplaints.Text = theDR["NumericField"].ToString();
                                if (chkPComplaints.Text.ToLower() == "other")
                                {
                                    //visibleDiv("DivOther");
                                }
                            }

                        }
                    }
                }
                {
                    foreach (DataRow DR in theDT.Rows)
                    {
                        foreach (Control y in thePnl.Controls)
                        {
                            if (y.GetType() == typeof(System.Web.UI.LiteralControl))
                            {
                                string thePn = y.ID;
                            }

                            else if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                            {
                                if (y.ID != null)
                                {
                                    if (y.ID.Contains("Pnl"))
                                    {
                                        FillCheckBoxListData(theDT, (System.Web.UI.WebControls.Panel)y, FieldName, theFieldName);
                                    }
                                }
                            }

                            else
                            {

                                if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                {

                                    if (((CheckBox)y).ID.Contains(thePnl.ID + "-" + DR["ValueID"].ToString()) && FieldName == DR["FieldName"].ToString())
                                        ((CheckBox)y).Checked = true;

                                    else if ("other" + ((CheckBox)y).ID == thePnl.ID + "-" + DR["ValueID"].ToString() && FieldName == DR["FieldName"].ToString())
                                        ((CheckBox)y).Checked = true;
                                }
                                if (y.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                {
                                    if (theFieldName != "")
                                    {
                                        string[] theControlId;
                                        if (((System.Web.UI.WebControls.TextBox)y).ID.Contains("OtherTXT") == true && FieldName == DR["FieldName"].ToString())
                                        {
                                            theControlId = ((TextBox)y).ID.ToString().Split('-');
                                            ((TextBox)y).Text = DR[theFieldName].ToString();
                                        }
                                        string script = "";
                                        script = "<script language = 'javascript' defer ='defer' id = " + ((TextBox)y).ID + ">\n";
                                        script += "show('" + (((TextBox)y).ID.ToString().Split('-')[1]).ToString() + "');\n";
                                        script += "</script>\n";
                                        RegisterStartupScript("" + ((TextBox)y).ID + "", script);
                                    }
                                }
                            }

                        }

                    }
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

            }
        }

        private void SaveUpdateKNHPMTCTHEI(int DataQuality)
        {

            DataSet theDSforChklist = new DataSet();

            //Diagnosis
            DataTable DTDiagnosis = new DataTable();
            DTDiagnosis = GetCheckBoxListcheckedIDs(PnlDiagnosis, "DiagnosisID", "Diagnosis_Other", 0);
            DTDiagnosis.TableName = "dtD";
            theDSforChklist.Tables.Add(DTDiagnosis);

            //Presenting Complaints
            DataTable DTPresentComplaints = new DataTable();
            DTPresentComplaints = GetCheckBoxListcheckedIDs(pnl2PComplaints, "PComplaintId", "Complaint_Other", 0);
            DTPresentComplaints.TableName = "dtPC";
            theDSforChklist.Tables.Add(DTPresentComplaints);

            //Vital Sign Referred To
            DataTable DTVSReferredTo = new DataTable();
            DTVSReferredTo = GetCheckBoxListcheckedIDs(idVitalSign.cblReferredTo);
            DTVSReferredTo.TableName = "dtVS_Rt";
            theDSforChklist.Tables.Add(DTVSReferredTo);

            //TB Assessment
            DataTable DTTBAssessment = new DataTable();
            DTTBAssessment = GetCheckBoxListcheckedIDs(cblTBAssesment);
            DTTBAssessment.TableName = "dtTBA";
            theDSforChklist.Tables.Add(DTTBAssessment);

            //Neonatal
            DataTable DTNeonatal = new DataTable();
            DTNeonatal = (DataTable)ViewState["GridNeonatalData"];
            DTNeonatal.TableName = "dtNeonatal";
            theDSforChklist.Tables.Add(DTNeonatal);

            //Maternal
            DataTable DTMaternal = new DataTable();
            DTMaternal = (DataTable)ViewState["GridMaternalData"];
            DTMaternal.TableName = "dtMaternal";
            theDSforChklist.Tables.Add(DTMaternal);

            //Immunization
            DataTable DTImmunization = new DataTable();
            DTImmunization = (DataTable)ViewState["GridImmunizationData"];
            DTImmunization.TableName = "dtImmunization";
            theDSforChklist.Tables.Add(DTImmunization);

            //Milestone
            DataTable DTMilestone = new DataTable();
            DTMilestone = (DataTable)ViewState["GridMilestoneData"];
            DTMilestone.TableName = "dtMilestone";
            theDSforChklist.Tables.Add(DTMilestone);

            //TBAssessment1
            DataTable DTTBAssessment1 = new DataTable();
            DTTBAssessment1 = (DataTable)ViewState["GridTBAssessmentData"];
            DTTBAssessment1.TableName = "dtTBAssessment";
            theDSforChklist.Tables.Add(DTTBAssessment1);


            IKNHHEI KNHHEIManager;
            KNHHEIManager = (IKNHHEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHHEI, BusinessProcess.Clinical");
            LocationID = Convert.ToInt32(Session["AppLocationId"]);
            PatientID = Convert.ToInt32(Session["PatientId"]);
            visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            Hashtable htparam = htableKNHHEIParameters();

            visitPK = KNHHEIManager.Save_Update_KNHHEI(PatientID, visitPK, LocationID, htparam, theDSforChklist, Convert.ToInt32(Session["AppUserId"]), DataQuality);
            Session["PatientVisitId"] = visitPK;
        }

        private void BindGrid(System.Web.UI.WebControls.GridView gridView, string gridName)
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "VisitId";
            theCol0.DataField = "VisitId";
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.Visible = false;
            gridView.Columns.Add(theCol0);

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Patientid";
            theCol1.DataField = "ptn_pk";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.Visible = false;
            gridView.Columns.Add(theCol1);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "TypeofTestId";
            theCol2.DataField = "TypeofTestId";
            theCol2.Visible = false;
            gridView.Columns.Add(theCol2);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "ResultId";
            theCol3.DataField = "ResultId";
            theCol3.Visible = false;
            gridView.Columns.Add(theCol3);

            BoundField theCol4 = new BoundField();
            if (gridName == "Immunization")
            {
                theCol4.HeaderText = "Immunization Period";
            }
            else if (gridName == "Milestone")
            {
                theCol4.HeaderText = "Duration";
            }
            else if (gridName == "TBAssessment")
            {
                theCol4.HeaderText = "Treatment";
            }
            else
            {
                theCol4.HeaderText = "Test Type";
            }
            theCol4.DataField = "TypeofTest";
            gridView.Columns.Add(theCol4);

            BoundField theCol5 = new BoundField();
            if (gridName == "Immunization")
            {
                theCol5.HeaderText = "Immunization Given";
            }
            else if (gridName == "Milestone")
            {
                theCol5.HeaderText = "Status";
            }
            else if (gridName == "TBAssessment")
            {
                theCol5.HeaderText = "Plan";
            }
            else
            {
                theCol5.HeaderText = "Result";
            }
            theCol5.DataField = "Result";
            theCol5.ReadOnly = true;
            gridView.Columns.Add(theCol5);

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "Date";
            theCol6.DataField = "Date";
            theCol6.DataFormatString = "{0:dd-MMM-yyyy}";
            theCol6.ReadOnly = true;
            if (gridName == "Milestone")
            {
                theCol6.Visible = false;
            }
            else
            {
                theCol6.Visible = true;
            }
            gridView.Columns.Add(theCol6);

            BoundField theCol7 = new BoundField();
            theCol7.HeaderText = "Comments";
            theCol7.DataField = "Comments";
            theCol7.ReadOnly = true;
            if (gridName == "TBAssessment" || gridName == "Immunization")
            {
                theCol7.Visible = false;
            }
            else
            {
                theCol7.Visible = true;
            }
            gridView.Columns.Add(theCol7);

            CommandField theCol8 = new CommandField();
            theCol8.ButtonType = ButtonType.Link;
            theCol8.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
            theCol8.ShowDeleteButton = true;
            gridView.Columns.Add(theCol8);

            BoundField theCol9 = new BoundField();
            theCol9.HeaderText = "Section";
            theCol9.DataField = "Section";
            theCol9.Visible = false;
            gridView.Columns.Add(theCol9);


        }

        private void RefreshGrid(string grdName)
        {
            if (grdName == "Neonatal")
            {
                ddlTypeofTest.SelectedIndex = -1;
                ddlTestResults.SelectedIndex = -1;
                txttestresultsgiven.Value = "";
                txtcomments.Text = "";
            }
            else if (grdName == "Maternal")
            {
                ddlTestDone.SelectedIndex = -1;
                txtresultmother.Text = "";
                txtresultmothergiven.Value = "";
                txtRemarks.Text = "";
            }

            else if (grdName == "Immunization")
            {
                ddlImmunisationPeriod.SelectedIndex = -1;
                ddImmunisationgiven.SelectedIndex = -1;
                txtDateImmunised.Value = "";
            }
            else if (grdName == "Milestone")
            {
                ddlDuration.SelectedIndex = -1;
                ddlStatus.SelectedIndex = -1;
                txtComment.Text = "";
            }

            else if (grdName == "TBAssessment")
            {
                ddlPlan.SelectedIndex = -1;
                ddlRegimen.SelectedIndex = -1;
                txtTreatmentDate.Value = "";
            }
        }

        private Boolean fieldValidation()
        {
            IIQCareSystem IQCareSystemInterface = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSystemInterface.SystemDate();
            IQCareUtils iQCareUtils = new IQCareUtils();
            string validateMessage = "Following values are required:</br>";
            bool validationCheck = true;
            AuthenticationManager auth = new AuthenticationManager();
            bool dateconstraint = auth.CheckDateConstriant(Convert.ToInt32(Session["AppLocationId"]));
            #region Check Visit Date
            if (Session["RegDate"] != null && txtVisitDate.Value != "")
            {
                if (!dateconstraint)
                {
                    if (Convert.ToDateTime(txtVisitDate.Value) < Convert.ToDateTime(Session["RegDate"]))
                    {
                        txtVisitDate.Focus();
                        MsgBuilder totalMsgBuilder = new MsgBuilder();
                        totalMsgBuilder.DataElements["MessageText"] = "Visit Date should not be less then registration date";
                        IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                        return false;
                    }
                }
            }
            if (txtVisitDate.Value.Trim() == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Visit Date";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                txtVisitDate.Focus();
                validationCheck = false;
            }

            if (ddlVisitType.SelectedValue == "0")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Visit Type";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                ddlVisitType.Focus();
                validationCheck = false;
            }

            if (idVitalSign.txtHeight.Text == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Height";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                txtVisitDate.Focus();
                validationCheck = false;
            }

            if (idVitalSign.txtWeight.Text == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Weight";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                txtVisitDate.Focus();
                validationCheck = false;
            }

            if ((idVitalSign.txtBPDiastolic.Text == "") || (idVitalSign.txtBPSystolic.Text == ""))
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -BP";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                txtVisitDate.Focus();
                validationCheck = false;
            }


            if (ddlVisitType.SelectedItem.Text == "Full Visit")
            {

                if (txtBirthWeight.Text.Trim() == "")
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Birth Weight";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                    txtBirthWeight.Focus();
                    validationCheck = false;
                }
                if (ddlIfeedingoption.SelectedValue == "0")
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Infant feeding";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                    ddlIfeedingoption.Focus();
                    validationCheck = false;
                }
                if (rdoMotherRegisNo.Checked == false && rdoMotherRegisYes.Checked == false)
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Mother Registered at this Clinic";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                    rdoMotherRegisYes.Focus();
                    validationCheck = false;
                }
                if (rdoARTEnrolNo.Checked == false && rdoARTEnrolYes.Checked == false)
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -On ART at enrollment of infant";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                    rdoARTEnrolYes.Focus();
                    validationCheck = false;
                }
                if (rdMotherRDrugNo.Checked == false && rdMotherRDrugYes.Checked == false)
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Mother received drugs for PMTCT";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                    rdMotherRDrugYes.Focus();
                    validationCheck = false;
                }
                if (ddlARVProphylaxis.SelectedValue == "0")
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -ARV Prophylaxis";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                    ddlARVProphylaxis.Focus();
                    validationCheck = false;
                }
                if (ddlModeofDelivery.SelectedValue == "0")
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Mode of delivery";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                    ddlModeofDelivery.Focus();
                    validationCheck = false;
                }
                if (ddlStateofMother.SelectedValue == "0")
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -State of mother";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                    ddlStateofMother.Focus();
                    validationCheck = false;
                }
            }

            #endregion
            if (!validationCheck)
            {
                MsgBuilder totalMsgBuilder = new MsgBuilder();
                totalMsgBuilder.DataElements["MessageText"] = validateMessage;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
            }
            return validationCheck;
        }

        private bool fieldValidation_ClinicalReview()
        {
            string validateMessage = "Following values are required:</br>";
            bool validationCheck = true;

            //if (ddlDuration.SelectedValue == "0")
            //{
            //    MsgBuilder msgBuilder = new MsgBuilder();
            //    msgBuilder.DataElements["Control"] = " -Duration";
            //    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
            //    ddlDuration.Focus();
            //    validationCheck = false;
            //}
            //if (ddlStatus.SelectedValue == "0")
            //{
            //    MsgBuilder msgBuilder = new MsgBuilder();
            //    msgBuilder.DataElements["Control"] = " -Status";
            //    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
            //    ddlStatus.Focus();
            //    validationCheck = false;
            //}
            if (gvMilestones.Rows.Count <= 0)
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Duration OR Status";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                ddlDuration.Focus();
                validationCheck = false;
            }
            bool isAnyTBAssessmentSelected = cblTBAssesment.SelectedIndex != -1;
            if (ddlVisitType.SelectedItem.Text == "Full Visit" && (!isAnyTBAssessmentSelected))
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -TB Assessment";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                txtBirthWeight.Focus();
                validationCheck = false;
            }

            if (!validationCheck)
            {
                MsgBuilder totalMsgBuilder = new MsgBuilder();
                totalMsgBuilder.DataElements["MessageText"] = validateMessage;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
            }
            return validationCheck;
        }

        private void showhideExpress()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "stblVitalSigns", "show('VitalSigns');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblNNHistory", "hide('NNHistory');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblMHistory", "hide('MHistory');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblIHistory", "hide('IHistory');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblPComplaints", "hide('PComplaints');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblExamination", "show('Examination');", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "stblMileStones", "show('MileStones');", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "stblDiagnosis", "hide('Diagnosis');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblManagementPlan", "show('ManagementPlan');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblAdminAppointment", "show('AdminAppointment');", true);
        }

        private void showhideFullVisit()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "stblVitalSigns", "show('VitalSigns');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblNNHistory", "show('NNHistory');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblMHistory", "show('MHistory');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblIHistory", "show('IHistory');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblPComplaints", "show('PComplaints');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblExamination", "show('Examination');", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "stblMileStones", "show('MileStones');", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "stblDiagnosis", "show('Diagnosis');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblManagementPlan", "show('ManagementPlan');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblAdminAppointment", "show('AdminAppointment');", true);

        }

        public void Authenticate()
        {
            if (Request.QueryString["name"] == "Delete")
            {
                btnClinicalHistorySave.Text = "Delete";
                btnClinicalHistorySave.Width = Unit.Percentage(9);
                lblbtnHIVHistorySave.Visible = false;
                lblbtnDelete.Visible = true;
            }
            AuthenticationManager Authentication = new AuthenticationManager();
            if (Authentication.HasFunctionRight(ApplicationAccess.HEIForm, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnClinicalHistoryPrint.Enabled = false;
                btnHIVHistoryPrint.Enabled = false;

            }
            if (Authentication.HasFunctionRight(ApplicationAccess.HEIForm, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnClinicalHistorySave.Enabled = false;
                btncloseClinicalHist.Enabled = false;
                btnHIVHistorySave.Enabled = false;
                btncloseHIVHistory.Enabled = false;
            }
            else if (Request.QueryString["name"] == "Delete")
            {
                if (Authentication.HasFunctionRight(ApplicationAccess.HEIForm, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {

                    int PatientID = Convert.ToInt32(Session["PatientId"]);
                    string theUrl = "";
                    theUrl = string.Format("{0}", "frmClinical_DeleteForm.aspx");
                    Response.Redirect(theUrl);
                }
                else if (Authentication.HasFunctionRight(ApplicationAccess.HEIForm, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                {
                    btnClinicalHistorySave.Text = "Delete";
                    btnClinicalHistorySave.Enabled = false;
                    btncloseClinicalHist.Enabled = false;
                    btnHIVHistorySave.Enabled = false;
                    btncloseHIVHistory.Enabled = false;
                }
            }

            if (Session["CEndedStatus"] != null)
            {
                if (((DataTable)Session["CEndedStatus"]).Rows.Count > 0)
                {
                    if (((DataTable)Session["CEndedStatus"]).Rows[0]["CareEnded"].ToString() == "1")
                    {
                        btnClinicalHistorySave.Enabled = false;
                        btncloseClinicalHist.Enabled = false;
                        btnHIVHistorySave.Enabled = false;
                        btncloseHIVHistory.Enabled = false;
                    }
                }
            }
            if (Convert.ToString(Session["CareEndFlag"]) == "1" && Convert.ToString(Session["CareendedStatus"]) == "1")
            {
                btnClinicalHistorySave.Enabled = false;
                btncloseClinicalHist.Enabled = false;
                btnHIVHistorySave.Enabled = false;
                btncloseHIVHistory.Enabled = false;
            }
        }

        private void ShowVitalSignItems()
        {
            idVitalSign.lblTemp.Text = "Temp 0c:";
            idVitalSign.txtTemp.MaxLength = 5;
            idVitalSign.txtRR.MaxLength = 5;
            idVitalSign.txtHR.MaxLength = 5;
            idVitalSign.txtHeight.MaxLength = 5;
            idVitalSign.txtWeight.MaxLength = 5;
            idVitalSign.txtBPDiastolic.MaxLength = 3;
            idVitalSign.txtBPSystolic.MaxLength = 3;

            ScriptManager.RegisterStartupScript(this, GetType(), "trNursecomments", "hide('trNursecomments');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "trPatientReferredto", "hide('trPatientReferredto');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "tdBPDP", "hide('tdBPDP');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "tdBMI", "hide('tdBMI');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "tdHC", "hide('tdHC');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "tdBMIZscore", "hide('tdBMIZscore');", true);
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            string theUrl = string.Format("../frmFindAddPatient.aspx?FormName=FamilyInfo");
            Session["PtnRedirect"] = Convert.ToInt32(Session["PatientId"]);
            if (Session["SaveFlag"] != null)
            {
                if (Session["SaveFlag"].ToString() == "Edit")
                {
                    Session["SaveFlag"] = "Add";

                }
            }
            Response.Redirect(theUrl);
        }

        protected void btnClinicalHistorySave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Delete")
            {
                int delete = theUtils.DeleteForm("HEI", Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppUserId"]));
                if (delete == 0)
                {
                    IQCareMsgBox.Show("RemoveFormError", this);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "deleteSuccessful", "alert('Form deleted successfully.');", true);
                    string theUrl;
                    theUrl = string.Format("frmPatient_Home.aspx");
                    Response.Redirect(theUrl);
                }
            }
            if (fieldValidation() == false)
            { return; }
            SaveUpdateKNHPMTCTHEI(0);
            SaveCancel();
        }

        protected void btnHIVHistorySave_Click(object sender, EventArgs e)
        {
            if (fieldValidation_ClinicalReview() == false)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    btncloseHIVHistory.Enabled = true;
                    btnHIVHistorySave.Enabled = true;
                }
                return;
            }

            SaveUpdateKNHPMTCTHEI(0);
            SaveCancelClinicalReview();
        }

        protected void btncloseHIVHistory_Click(object sender, EventArgs e)
        {

        }

        protected void ddlVisitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVisitType.SelectedItem.Text == "Express")
            {
                showhideExpress();
            }
            else
            {
                showhideFullVisit();
            }

        }

        private void CreateGridColoumn()
        {
            theDTGrid = new DataTable();
            theDTGrid.Columns.Add("ptn_pk", typeof(Int32));
            theDTGrid.Columns.Add("Visit_pk", typeof(Int32));
            theDTGrid.Columns.Add("Section", typeof(string));
            theDTGrid.Columns.Add("TypeofTestId", typeof(Int32));
            theDTGrid.Columns.Add("TypeofTest", typeof(string));
            theDTGrid.Columns.Add("ResultId", typeof(Int32));
            theDTGrid.Columns.Add("Result", typeof(string));
            theDTGrid.Columns.Add("Comments", typeof(string));
            theDTGrid.Columns.Add("Date", typeof(string));
        }

        protected void btnAddImmunization_Click(object sender, EventArgs e)
        {
            int VisitId = Convert.ToInt32(Session["PatientVisitId"]) > 0 ? Convert.ToInt32(Session["PatientVisitId"]) : 0;
            if (ddlImmunisationPeriod.SelectedItem.Text == "Select" || ddImmunisationgiven.SelectedItem.Text == "Select" || txtDateImmunised.Value == "")
            {
                IQCareMsgBox.Show("NoRecordSelected", this);
                return;
            }

            if (((DataTable)ViewState["GridImmunizationData"]).Rows.Count == 0)
            {
                CreateGridColoumn();

                DataRow theDR = theDTGrid.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["Section"] = "Immunization";
                theDR["TypeofTestId"] = ddlImmunisationPeriod.SelectedValue;
                theDR["TypeofTest"] = ddlImmunisationPeriod.SelectedItem.Text;
                theDR["ResultId"] = ddImmunisationgiven.SelectedValue;
                theDR["Result"] = ddImmunisationgiven.SelectedItem.Text;
                theDR["Date"] = "" + txtDateImmunised.Value + "";
                theDR["Comments"] = string.Empty;
                theDTGrid.Rows.Add(theDR);
                GrdImmunization.Columns.Clear();
                BindGrid(GrdImmunization, "Immunization");
                RefreshGrid("Immunization");
                GrdImmunization.DataSource = theDTGrid;
                GrdImmunization.DataBind();
                ViewState["GridImmunizationData"] = theDTGrid;
            }
            else
            {
                theDTGrid = (DataTable)ViewState["GridImmunizationData"];
                DataRow theDR = theDTGrid.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["Section"] = "Immunization";
                theDR["TypeofTestId"] = ddlImmunisationPeriod.SelectedValue;
                theDR["TypeofTest"] = ddlImmunisationPeriod.SelectedItem.Text;
                theDR["ResultId"] = ddImmunisationgiven.SelectedValue;
                theDR["Result"] = ddImmunisationgiven.SelectedItem.Text;
                theDR["Date"] = "" + txtDateImmunised.Value + "";
                theDR["Comments"] = string.Empty;
                theDTGrid.Rows.Add(theDR);
                GrdImmunization.Columns.Clear();
                BindGrid(GrdImmunization, "Immunization");
                RefreshGrid("Immunization");
                GrdImmunization.DataSource = theDTGrid;
                GrdImmunization.DataBind();
                ViewState["GridImmunizationData"] = theDTGrid;
            }
        }

        protected void btnAddNNatal_Click(object sender, EventArgs e)
        {
            int VisitId = Convert.ToInt32(Session["PatientVisitId"]) > 0 ? Convert.ToInt32(Session["PatientVisitId"]) : 0;
            if (ddlTypeofTest.SelectedItem.Text == "Select" || ddlTestResults.SelectedItem.Text == "Select" || txttestresultsgiven.Value == "" || txtcomments.Text == "")
            {
                IQCareMsgBox.Show("NoRecordSelected", this);
                return;
            }

            if (((DataTable)ViewState["GridNeonatalData"]).Rows.Count == 0)
            {
                CreateGridColoumn();

                DataRow theDR = theDTGrid.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["Section"] = "Neonatal History";
                theDR["TypeofTestId"] = ddlTypeofTest.SelectedValue;
                theDR["TypeofTest"] = ddlTypeofTest.SelectedItem.Text;
                theDR["ResultId"] = ddlTestResults.SelectedValue;
                theDR["Result"] = ddlTestResults.SelectedItem.Text;
                theDR["Date"] = "" + txttestresultsgiven.Value + "";
                theDR["Comments"] = txtcomments.Text;
                theDTGrid.Rows.Add(theDR);
                GrdNNHistory.Columns.Clear();
                BindGrid(GrdNNHistory, "Neonatal");
                RefreshGrid("Neonatal");
                GrdNNHistory.DataSource = theDTGrid;
                GrdNNHistory.DataBind();
                ViewState["GridNeonatalData"] = theDTGrid;
            }
            else
            {
                theDTGrid = (DataTable)ViewState["GridNeonatalData"];
                DataRow theDR = theDTGrid.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["Section"] = "Neonatal History";
                theDR["TypeofTestId"] = ddlTypeofTest.SelectedValue;
                theDR["TypeofTest"] = ddlTypeofTest.SelectedItem.Text;
                theDR["ResultId"] = ddlTestResults.SelectedValue;
                theDR["Result"] = ddlTestResults.SelectedItem.Text;
                theDR["Date"] = "" + txttestresultsgiven.Value + "";
                theDR["Comments"] = txtcomments.Text;
                theDTGrid.Rows.Add(theDR);
                GrdNNHistory.Columns.Clear();
                BindGrid(GrdNNHistory, "Neonatal");
                RefreshGrid("Neonatal");
                GrdNNHistory.DataSource = theDTGrid;
                GrdNNHistory.DataBind();
                ViewState["GridNeonatalData"] = theDTGrid;
            }
        }

        protected void btnMMother_Click(object sender, EventArgs e)
        {
            int VisitId = Convert.ToInt32(Session["PatientVisitId"]) > 0 ? Convert.ToInt32(Session["PatientVisitId"]) : 0;
            if (ddlTestDone.SelectedItem.Text == "Select" || txtresultmother.Text == "" || txtresultmothergiven.Value == "" || txtRemarks.Text == "")
            {
                IQCareMsgBox.Show("NoRecordSelected", this);
                return;
            }

            if (((DataTable)ViewState["GridMaternalData"]).Rows.Count == 0)
            {
                CreateGridColoumn();

                DataRow theDR = theDTGrid.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["Section"] = "Maternal History";
                theDR["TypeofTestId"] = ddlTestDone.SelectedValue;
                theDR["TypeofTest"] = ddlTestDone.SelectedItem.Text;
                theDR["ResultId"] = -1;
                theDR["Result"] = txtresultmother.Text;
                theDR["Date"] = "" + txtresultmothergiven.Value + "";
                theDR["Comments"] = txtRemarks.Text;
                theDTGrid.Rows.Add(theDR);
                GrdMMHistory.Columns.Clear();
                BindGrid(GrdMMHistory, "Maternal");
                RefreshGrid("Maternal");
                GrdMMHistory.DataSource = theDTGrid;
                GrdMMHistory.DataBind();
                ViewState["GridMaternalData"] = theDTGrid;
            }
            else
            {
                theDTGrid = (DataTable)ViewState["GridMaternalData"];
                DataRow theDR = theDTGrid.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["Section"] = "Maternal History";
                theDR["TypeofTestId"] = ddlTestDone.SelectedValue;
                theDR["TypeofTest"] = ddlTestDone.SelectedItem.Text;
                theDR["ResultId"] = -1;
                theDR["Result"] = txtresultmother.Text;
                theDR["Date"] = "" + txtresultmothergiven.Value + "";
                theDR["Comments"] = txtRemarks.Text;
                theDTGrid.Rows.Add(theDR);
                GrdMMHistory.Columns.Clear();
                BindGrid(GrdMMHistory, "Maternal");
                RefreshGrid("Maternal");
                GrdMMHistory.DataSource = theDTGrid;
                GrdMMHistory.DataBind();
                ViewState["GridMaternalData"] = theDTGrid;
            }
        }

        protected void btnAddMilestone_Click(object sender, EventArgs e)
        {
            int VisitId = Convert.ToInt32(Session["PatientVisitId"]) > 0 ? Convert.ToInt32(Session["PatientVisitId"]) : 0;
            if (ddlDuration.SelectedItem.Text == "Select" || ddlStatus.SelectedItem.Text == "Select")
            {
                IQCareMsgBox.Show("NoRecordSelected", this);
                return;
            }

            if (((DataTable)ViewState["GridMilestoneData"]).Rows.Count == 0)
            {
                CreateGridColoumn();

                DataRow theDR = theDTGrid.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["Section"] = "Milestone";
                theDR["TypeofTestId"] = ddlDuration.SelectedValue;
                theDR["TypeofTest"] = ddlDuration.SelectedItem.Text;
                theDR["ResultId"] = ddlStatus.SelectedValue;
                theDR["Result"] = ddlStatus.SelectedItem.Text;
                theDR["Date"] = string.Empty;
                theDR["Comments"] = txtComment.Text;
                theDTGrid.Rows.Add(theDR);
                gvMilestones.Columns.Clear();
                BindGrid(gvMilestones, "Milestone");
                RefreshGrid("Milestone");
                gvMilestones.DataSource = theDTGrid;
                gvMilestones.DataBind();
                ViewState["GridMilestoneData"] = theDTGrid;
            }
            else
            {
                theDTGrid = (DataTable)ViewState["GridMilestoneData"];
                DataRow theDR = theDTGrid.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["Section"] = "Milestone";
                theDR["TypeofTestId"] = ddlDuration.SelectedValue;
                theDR["TypeofTest"] = ddlDuration.SelectedItem.Text;
                theDR["ResultId"] = ddlStatus.SelectedValue;
                theDR["Result"] = ddlStatus.SelectedItem.Text;
                theDR["Date"] = string.Empty;
                theDR["Comments"] = txtComment.Text;
                theDTGrid.Rows.Add(theDR);
                gvMilestones.Columns.Clear();
                BindGrid(gvMilestones, "Milestone");
                RefreshGrid("Milestone");
                gvMilestones.DataSource = theDTGrid;
                gvMilestones.DataBind();
                ViewState["GridMilestoneData"] = theDTGrid;
            }
        }

        protected void btnAddTB_Click(object sender, EventArgs e)
        {
            int VisitId = Convert.ToInt32(Session["PatientVisitId"]) > 0 ? Convert.ToInt32(Session["PatientVisitId"]) : 0;
            if (ddlPlan.SelectedItem.Text == "Select" || ddlRegimen.SelectedItem.Text == "Select" || txtTreatmentDate.Value == "")
            {
                IQCareMsgBox.Show("NoRecordSelected", this);
                return;
            }

            if (((DataTable)ViewState["GridTBAssessmentData"]).Rows.Count == 0)
            {
                CreateGridColoumn();

                DataRow theDR = theDTGrid.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["Section"] = "TBAssessment";
                theDR["TypeofTestId"] = ddlPlan.SelectedValue;
                theDR["TypeofTest"] = ddlPlan.SelectedItem.Text;
                theDR["ResultId"] = ddlRegimen.SelectedValue;
                theDR["Result"] = ddlRegimen.SelectedItem.Text;
                theDR["Date"] = "" + txtTreatmentDate.Value + "";
                theDR["Comments"] = string.Empty;
                theDTGrid.Rows.Add(theDR);
                gvTB.Columns.Clear();
                BindGrid(gvTB, "TBAssessment");
                RefreshGrid("TBAssessment");
                gvTB.DataSource = theDTGrid;
                gvTB.DataBind();
                ViewState["GridTBAssessmentData"] = theDTGrid;
            }
            else
            {
                theDTGrid = (DataTable)ViewState["GridTBAssessmentData"];

                DataRow theDR = theDTGrid.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["Section"] = "TBAssessment";
                theDR["TypeofTestId"] = ddlPlan.SelectedValue;
                theDR["TypeofTest"] = ddlPlan.SelectedItem.Text;
                theDR["ResultId"] = ddlRegimen.SelectedValue;
                theDR["Result"] = ddlRegimen.SelectedItem.Text;// txtTestResults.Text;
                theDR["Date"] = "" + txtTreatmentDate.Value + "";
                theDR["Comments"] = string.Empty;
                theDTGrid.Rows.Add(theDR);
                gvTB.Columns.Clear();
                BindGrid(gvTB, "TBAssessment");
                RefreshGrid("TBAssessment");
                gvTB.DataSource = theDTGrid;
                gvTB.DataBind();
                ViewState["GridTBAssessmentData"] = theDTGrid;
            }
        }

        protected void GrdNNHistory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)ViewState["GridNeonatalData"]);
            int r = Convert.ToInt32(e.RowIndex.ToString());
            int Id = -1;
            try
            {
                if (theDT.Rows.Count > 0)
                {

                    if (theDT.Rows[r].HasErrors == false)
                    {
                        if ((theDT.Rows[r]["TypeofTestId"] != null) && (theDT.Rows[r]["TypeofTestId"] != DBNull.Value))
                        {
                            if (theDT.Rows[r]["TypeofTestId"].ToString() != "")
                            {
                                Id = Convert.ToInt32(theDT.Rows[r]["TypeofTestId"]);
                                theDT.Rows[r].Delete();
                                theDT.AcceptChanges();
                                ViewState["GridNNData"] = theDT;
                                GrdNNHistory.Columns.Clear();
                                BindGrid(GrdNNHistory, "Neonatal");
                                RefreshGrid("Neonatal");
                                GrdNNHistory.DataSource = (DataTable)ViewState["GridNeonatalData"];
                                GrdNNHistory.DataBind();
                                IQCareMsgBox.Show("DeleteSuccess", this);
                            }
                        }
                    }
                    //if (((DataTable)ViewState["GridNNData"]).Rows.Count == 0)
                    //    btAddNNatal.Enabled = false;
                    //else
                    //    btAddNNatal.Enabled = true;
                }
                else
                {
                    GrdNNHistory.Visible = false;
                    RefreshGrid("Neonatal");
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        protected void GrdMMHistory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)ViewState["GridMaternalData"]);
            int r = Convert.ToInt32(e.RowIndex.ToString());
            int Id = -1;
            try
            {
                if (theDT.Rows.Count > 0)
                {

                    if (theDT.Rows[r].HasErrors == false)
                    {
                        if ((theDT.Rows[r]["TypeofTestId"] != null) && (theDT.Rows[r]["TypeofTestId"] != DBNull.Value))
                        {
                            if (theDT.Rows[r]["TypeofTestId"].ToString() != "")
                            {
                                Id = Convert.ToInt32(theDT.Rows[r]["TypeofTestId"]);
                                theDT.Rows[r].Delete();
                                theDT.AcceptChanges();
                                ViewState["GridMaternalData"] = theDT;
                                GrdMMHistory.Columns.Clear();
                                BindGrid(GrdMMHistory, "Maternal");
                                RefreshGrid("Maternal");
                                GrdMMHistory.DataSource = (DataTable)ViewState["GridMaternalData"];
                                GrdMMHistory.DataBind();
                                IQCareMsgBox.Show("DeleteSuccess", this);
                            }
                        }
                    }
                    //if (((DataTable)ViewState["GridMData"]).Rows.Count == 0)
                    //    btnMMother.Enabled = false;
                    //else
                    //    btnMMother.Enabled = true;
                }
                else
                {
                    GrdMMHistory.Visible = false;
                    RefreshGrid("Maternal");
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        protected void gvMilestones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)ViewState["GridMilestoneData"]);
            int r = Convert.ToInt32(e.RowIndex.ToString());
            int Id = -1;
            try
            {
                if (theDT.Rows.Count > 0)
                {

                    if (theDT.Rows[r].HasErrors == false)
                    {
                        if ((theDT.Rows[r]["TypeofTestId"] != null) && (theDT.Rows[r]["TypeofTestId"] != DBNull.Value))
                        {
                            if (theDT.Rows[r]["TypeofTestId"].ToString() != "")
                            {
                                Id = Convert.ToInt32(theDT.Rows[r]["TypeofTestId"]);
                                theDT.Rows[r].Delete();
                                theDT.AcceptChanges();
                                ViewState["GridMilestoneData"] = theDT;
                                gvMilestones.Columns.Clear();
                                BindGrid(gvMilestones, "Milestone");
                                RefreshGrid("Milestone");
                                gvMilestones.DataSource = (DataTable)ViewState["GridMilestoneData"];
                                gvMilestones.DataBind();
                                IQCareMsgBox.Show("DeleteSuccess", this);
                            }
                        }
                    }
                }
                else
                {
                    gvMilestones.Visible = false;
                    RefreshGrid("Milestone");
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        protected void GrdImmunization_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)ViewState["GridImmunizationData"]);
            int r = Convert.ToInt32(e.RowIndex.ToString());
            int Id = -1;
            try
            {
                if (theDT.Rows.Count > 0)
                {

                    if (theDT.Rows[r].HasErrors == false)
                    {
                        if ((theDT.Rows[r]["TypeofTestId"] != null) && (theDT.Rows[r]["TypeofTestId"] != DBNull.Value))
                        {
                            if (theDT.Rows[r]["TypeofTestId"].ToString() != "")
                            {
                                Id = Convert.ToInt32(theDT.Rows[r]["TypeofTestId"]);
                                theDT.Rows[r].Delete();
                                theDT.AcceptChanges();
                                ViewState["GridImmunizationData"] = theDT;
                                GrdImmunization.Columns.Clear();
                                BindGrid(GrdImmunization, "Immunization");
                                RefreshGrid("Immunization");
                                GrdImmunization.DataSource = (DataTable)ViewState["GridImmunizationData"];
                                GrdImmunization.DataBind();
                                IQCareMsgBox.Show("DeleteSuccess", this);
                            }
                        }
                    }
                }
                else
                {
                    GrdImmunization.Visible = false;
                    RefreshGrid("Immunization");
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        protected void gvTB_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)ViewState["GridTBAssessmentData"]);
            int r = Convert.ToInt32(e.RowIndex.ToString());
            int Id = -1;
            try
            {
                if (theDT.Rows.Count > 0)
                {

                    if (theDT.Rows[r].HasErrors == false)
                    {
                        if ((theDT.Rows[r]["TypeofTestId"] != null) && (theDT.Rows[r]["TypeofTestId"] != DBNull.Value))
                        {
                            if (theDT.Rows[r]["TypeofTestId"].ToString() != "")
                            {
                                Id = Convert.ToInt32(theDT.Rows[r]["TypeofTestId"]);
                                theDT.Rows[r].Delete();
                                theDT.AcceptChanges();
                                ViewState["GridTBAssessmentData"] = theDT;
                                gvTB.Columns.Clear();
                                BindGrid(gvTB, "TBAssessment");
                                RefreshGrid("TBAssessment");
                                gvTB.DataSource = (DataTable)ViewState["GridTBAssessmentData"];
                                gvTB.DataBind();
                                IQCareMsgBox.Show("DeleteSuccess", this);
                            }
                        }
                    }
                }
                else
                {
                    gvTB.Visible = false;
                    RefreshGrid("TBAssessment");
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
    }
}