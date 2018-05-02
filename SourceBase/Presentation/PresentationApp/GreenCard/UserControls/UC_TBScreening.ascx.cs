using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using System.Data;
using System.Text;
using Interface.Clinical;
using System.Collections;
using System.Drawing;

namespace PresentationApp.GreenCard.UserControls
{
    public partial class UC_TBScreening : System.Web.UI.UserControl
    {
       
        // Delegate declaration
        public delegate void OnButtonClick();
        // Event declaration
        public event OnButtonClick btnHandler;
        /***************End********************************/
        IKNHStaticForms TBScreeningManager;
        DataSet theDSXML;
        IQCareUtils theUtils = new IQCareUtils();
        BindFunctions BindManager = new BindFunctions();
        DataView theDV, theDVCodeID;
        DataTable theDT;
        String startTime;
        public string availableTBResults, chestXRay, tissueBiopsy, IPT, AdherenceAddressed, missedAnyDoses, ReferredForAdherence, ContactsScreenedForTB;
        public string sputumSmearDate, GeneExpertDate, SputumDSTDate, chestXRayDate, TissueBiopsyDate, TBRegimenStartDate, TBRegimenEndDate, INHStartDate, INHEndDate,
                PyridoxineStartDate, PyridoxineEndDate;
        string TBFindingsScript;

        protected void Page_Load(object sender, EventArgs e)
        {
            //startTime = DateTime.Now;
            startTime = String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now);
            TBScreeningManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");

            TBFindingsScript = "SelectTBFindings('" + ddlTBFindings.ClientID + "','" + ddlSputumSmear.ClientID + "','" + txtSputumSmearDate.ClientID + "','" + ddlGeneExpert.ClientID + "','" +
                 txtGeneExpertDate.ClientID + "','" + ddlSputumDST.ClientID + "','" + txtSputumDSTDate.ClientID + "','" +
                 rdoChestXrayYes.ClientID + "','" + rdoChestXrayNo.ClientID + "','" + txtChestXrayDate.ClientID + "','" +
                ddlCXRResults.ClientID + "','" + txtOtherCXRResults.ClientID + "','" +
                rdoTissueBiopsyYes.ClientID + "','" + rdoTissueBiopsyNo.ClientID + "','" + txtTissueBiopsyDate.ClientID + "','" + ddlTBClassification.ClientID + "','" + ddlPatientClassification.ClientID
                + "','" + ddlTBPLan.ClientID + "','" + txtOtherTBPlan.ClientID + "','" + ddlTBRegimen.ClientID + "','" + txtOtherTBRegimen.ClientID + "','" + txtTBRegimenStartDate.ClientID
                + "','" + txtTBRegimenEndDate.ClientID + "','" + ddlTBTreatment.ClientID + "','" + rdoContactsScreenedForTBYes.ClientID + "','" + rdoContactsScreenedForTBNo.ClientID
                + "','" + txtSpecifyWhyContactNotScreenedForTB.ClientID + "','" + ddlPatientReferredForTreatment.ClientID + "','"
                //
                + rdoLstIPT.ClientID + "','" + txtINHStartDate.ClientID + "','" + txtINHEndDate.ClientID + "','" + txtPyridoxineStartDate.ClientID + "','" + txtPyridoxineEndDate.ClientID
                + "','" + rdoAdherenceBeenAddressedYes.ClientID + "','" + rdoAdherenceBeenAddressedNo.ClientID + "','" + rdoMissedAnyTBDosesYes.ClientID + "','" + rdoMissedAnyTBDosesNo.ClientID
                + "','" + rdoReferredForAdherenceYes.ClientID + "','" + rdoReferredForAdherenceNo.ClientID + "','" + cblReviewChecklist.ClientID + "','" + cblSignsOfHepatitis.ClientID
                + "','" + txtSpecifyOtherTBSideEffects.ClientID + "','" + ddlReasonDeclinedIPT.ClientID + "','" + txtOtherReasonDeclinedIPT.ClientID + "');";
            AddAttributes();

            if (!IsPostBack)
            {
                //this.CollapsiblePanelExtender1.Collapsed = true;
                //this.CollapsiblePanelExtender2.Collapsed = true;
                //this.CollapsiblePanelExtender3.Collapsed = true;
                //this.CollapsiblePanelExtender4.Collapsed = true;
                Bind_Select_Multiselect_Lists();
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    LoadAutoPopulatingData();
                }
                else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    //Load Details
                    LoadExistingFormdata();
                }
            }
            showHideControls();            
        }

        

        private void LoadExistingFormdata()
        {
            DataSet theDSExistingForm = new DataSet();
            theDSExistingForm = TBScreeningManager.GetTBScreeningFormData(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));
            if (theDSExistingForm.Tables[0].Rows.Count > 0)
            {
                ddlTBFindings.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["TBFindings"].ToString();
                if (theDSExistingForm.Tables[0].Rows[0]["TBAvailableResults"].ToString() == "1")
                {
                    //rdoAvailableTBResultsYes.Checked = true;
                }
                else if (theDSExistingForm.Tables[0].Rows[0]["TBAvailableResults"].ToString() == "0")
                {
                    //rdoAvailableTBResultsNo.Checked = true;
                }
                else
                {
                    //rdoAvailableTBResultsYes.Checked = false;
                    //rdoAvailableTBResultsNo.Checked = false;
                }
                ddlSputumSmear.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["SputumSmear"].ToString();
                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["SputumSmearDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //     txtSputumSmearDate.Text = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["SputumSmearDate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtSputumSmearDate.Text = "";
                //}
                txtSputumSmearDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["SputumSmearDate"]);
                ddlGeneExpert.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["GeneExpert"].ToString();
                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["GeneExpertDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    txtGeneExpertDate.Text = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["GeneExpertDate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtGeneExpertDate.Text = "";
                //}
                txtGeneExpertDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["GeneExpertDate"]);
                ddlSputumDST.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["SputumDST"].ToString();
                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["SputumDSTDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    txtSputumDSTDate.Text = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["SputumDSTDate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtSputumDSTDate.Text = "";
                //}
                txtSputumDSTDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["SputumDSTDate"]);
                if (theDSExistingForm.Tables[0].Rows[0]["ChestXRay"].ToString() == "1")
                {
                    rdoChestXrayYes.Checked = true;
                    chestXRay = "1";
                }
                else if (theDSExistingForm.Tables[0].Rows[0]["ChestXRay"].ToString() == "0")
                {
                    rdoChestXrayNo.Checked = true;
                    chestXRay = "0";
                }
                else
                {
                    rdoChestXrayYes.Checked = false;
                    rdoChestXrayNo.Checked = false;
                    chestXRay = null;
                }

                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["ChestXRayDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    txtChestXrayDate.Text = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["ChestXRayDate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtChestXrayDate.Text = "";
                //}
                txtChestXrayDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["ChestXRayDate"]);
                if (theDSExistingForm.Tables[0].Rows[0]["TissueBiopsy"].ToString() == "1")
                {
                    rdoTissueBiopsyYes.Checked = true;
                    tissueBiopsy = "1";
                }
                else if (theDSExistingForm.Tables[0].Rows[0]["TissueBiopsy"].ToString() == "0")
                {
                    rdoTissueBiopsyNo.Checked = true;
                    tissueBiopsy = "0";
                }
                else
                {
                    rdoTissueBiopsyYes.Checked = false;
                    rdoTissueBiopsyNo.Checked = false;
                    tissueBiopsy = null;
                }

                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["TissueBiopsyDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    txtTissueBiopsyDate.Text = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["TissueBiopsyDate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtTissueBiopsyDate.Text = "";
                //}
                txtTissueBiopsyDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["TissueBiopsyDate"]);
                ddlCXRResults.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["CXRResults"].ToString();
                txtOtherCXRResults.Text = theDSExistingForm.Tables[0].Rows[0]["OtherCXR"].ToString();
                ddlTBClassification.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["TBClassification"].ToString();
                ddlPatientClassification.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["PatientClassification"].ToString();
                ddlTBPLan.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["TBPlan"].ToString();
                txtOtherTBPlan.Text = theDSExistingForm.Tables[0].Rows[0]["OtherTBPlan"].ToString();
                ddlTBRegimen.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["TBRegimen"].ToString();
                txtOtherTBRegimen.Text = theDSExistingForm.Tables[0].Rows[0]["OtherTBRegimen"].ToString();
                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["TBRegimenStartDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    txtTBRegimenStartDate.Text = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["TBRegimenStartDate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtTBRegimenStartDate.Text = "";
                //}
                txtTBRegimenStartDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["TBRegimenStartDate"]);
                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["TBRegimenEndDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    txtTBRegimenEndDate.Text = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["TBRegimenEndDate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtTBRegimenEndDate.Text = "";
                //}
                txtTBRegimenEndDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["TBRegimenEndDate"]);
                ddlTBTreatment.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["TBTreatmentOutcome"].ToString();
                txtOtherTreatmentOutcome.Text = theDSExistingForm.Tables[0].Rows[0]["OtherTBTreatmentOutcome"].ToString();
                if (theDSExistingForm.Tables[0].Rows[0]["IPT"].ToString() != "")
                {
                    rdoLstIPT.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["IPT"].ToString();
                    if (rdoLstIPT.SelectedItem != null)
                    {
                        if (rdoLstIPT.SelectedItem.Text == "Start IPT" || rdoLstIPT.SelectedItem.Text == "Continue IPT" || rdoLstIPT.SelectedItem.Text == "Completed IPT")
                        {
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "showINHPyridoxine", "show_hide('INHStartEndDates','visible');show_hide('PyridoxineStartEnd','visible');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showINHPyridoxine", "show_hide('INHStartEndDates','visible');show_hide('PyridoxineStartEnd','visible');EnableDisableStopReason('" + rdoLstIPT.ClientID + "','" + cblStopTBReason.ClientID + "');", true);
                            //cblStopTBReason.Enabled = false;
                        }
                        else if (rdoLstIPT.SelectedItem.Text == "Declined IPT")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showDeclinedIPT", "show_hide('declinedIPTID','visible');", true);
                        }
                    }
                }

                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["INHStartDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    txtINHStartDate.Text = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["INHStartDate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtINHStartDate.Text = "";
                //}
                ddlReasonDeclinedIPT.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["ReasonDeclinedIPT"].ToString();
                txtOtherReasonDeclinedIPT.Text = theDSExistingForm.Tables[0].Rows[0]["OtherReasonDeclinedIPT"].ToString();

                txtINHStartDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["INHStartDate"]);
                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["INHEndDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    txtINHEndDate.Text = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["INHEndDate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtINHEndDate.Text = "";
                //}
                txtINHEndDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["INHEndDate"]);

                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["PyridoxineStartDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    txtPyridoxineStartDate.Text = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["PyridoxineStartDate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtPyridoxineStartDate.Text = "";
                //}
                txtPyridoxineStartDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["PyridoxineStartDate"]);

                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["PyridoxineEndDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    txtPyridoxineEndDate.Text = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["PyridoxineEndDate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtPyridoxineEndDate.Text = "";
                //}
                txtPyridoxineEndDate.Text = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["PyridoxineEndDate"]);
                if (theDSExistingForm.Tables[0].Rows[0]["AdherenceAddressed"].ToString() == "1")
                {
                    rdoAdherenceBeenAddressedYes.Checked = true;
                    AdherenceAddressed = "1";
                }
                else if (theDSExistingForm.Tables[0].Rows[0]["AdherenceAddressed"].ToString() == "0")
                {
                    rdoAdherenceBeenAddressedNo.Checked = true;
                    AdherenceAddressed = "0";
                }
                else
                {
                    rdoAdherenceBeenAddressedYes.Checked = false;
                    rdoAdherenceBeenAddressedNo.Checked = false;
                    AdherenceAddressed = null;
                }

                if (theDSExistingForm.Tables[0].Rows[0]["AnyMissedDoses"].ToString() == "1")
                {
                    rdoMissedAnyTBDosesYes.Checked = true;
                    missedAnyDoses = "1";
                }
                else if (theDSExistingForm.Tables[0].Rows[0]["AnyMissedDoses"].ToString() == "0")
                {
                    rdoMissedAnyTBDosesNo.Checked = true;
                    missedAnyDoses = "0";
                }
                else
                {
                    rdoMissedAnyTBDosesYes.Checked = false;
                    rdoMissedAnyTBDosesNo.Checked = false;
                    missedAnyDoses = null;
                }

                if (theDSExistingForm.Tables[0].Rows[0]["ReferredForAdherence"].ToString() == "1")
                {
                    rdoReferredForAdherenceYes.Checked = true;
                    ReferredForAdherence = "1";
                }
                else if (theDSExistingForm.Tables[0].Rows[0]["ReferredForAdherence"].ToString() == "0")
                {
                    rdoReferredForAdherenceNo.Checked = true;
                    ReferredForAdherence = "0";
                }
                else
                {
                    rdoReferredForAdherenceYes.Checked = false;
                    rdoReferredForAdherenceNo.Checked = false;
                    ReferredForAdherence = null;
                }

                txtSpecifyOtherTBSideEffects.Text = theDSExistingForm.Tables[0].Rows[0]["OtherTBSideEffects"].ToString();

                ddlPatientReferredForTreatment.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["FacilityPatientReferredTo"].ToString();

                //if (theDSExistingForm.Tables[0].Rows[0]["TBConfirmedSuspected"].ToString() == "True")
                //{
                //    rdoTBConfirmedSuspectedYes.Checked = true;
                //}
                //else if (theDSExistingForm.Tables[0].Rows[0]["TBConfirmedSuspected"].ToString() == "False")
                //{
                //    rdoTBConfirmedSuspectedNo.Checked = true;
                //}
                //else
                //{
                //    rdoTBConfirmedSuspectedYes.Checked = false;
                //    rdoTBConfirmedSuspectedNo.Checked = false;
                //}

                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["PyridoxineEndDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    txtINHStopDate.Text = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["PyridoxineEndDate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtINHStopDate.Text = "";
                //}

                if (theDSExistingForm.Tables[0].Rows[0]["ContactsScreenedForTB"].ToString() == "1")
                {
                    rdoContactsScreenedForTBYes.Checked = true;
                    ContactsScreenedForTB = "1";
                }
                else if (theDSExistingForm.Tables[0].Rows[0]["ContactsScreenedForTB"].ToString() == "0")
                {
                    rdoContactsScreenedForTBNo.Checked = true;
                    ContactsScreenedForTB = "0";
                }
                else
                {
                    rdoContactsScreenedForTBYes.Checked = false;
                    rdoContactsScreenedForTBNo.Checked = false;
                    ContactsScreenedForTB = null;
                }

                txtSpecifyWhyContactNotScreenedForTB.Text = theDSExistingForm.Tables[0].Rows[0]["IfNoSpecifyWhy"].ToString();


            }

            if (Convert.ToDouble(Session["patientageinyearmonth"].ToString()) >= 15)
            {
                if (theDSExistingForm.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < theDSExistingForm.Tables[1].Rows.Count; i++)
                    {
                        ListItem currentCheckBox = cblTBAssessmentICF.Items.FindByValue(theDSExistingForm.Tables[1].Rows[i]["ValueID"].ToString());
                        if (currentCheckBox != null)
                        {
                            currentCheckBox.Selected = true;
                        }
                    }
                }
            }
            else
            {
                if (theDSExistingForm.Tables[4].Rows.Count > 0)
                {
                    for (int i = 0; i < theDSExistingForm.Tables[4].Rows.Count; i++)
                    {
                        ListItem currentCheckBox = cblTBAssessmentICF.Items.FindByValue(theDSExistingForm.Tables[4].Rows[i]["ValueID"].ToString());
                        if (currentCheckBox != null)
                        {
                            currentCheckBox.Selected = true;
                        }
                    }
                }
            }

            if (theDSExistingForm.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < theDSExistingForm.Tables[2].Rows.Count; i++)
                {
                    ListItem currentCheckBox = cblStopTBReason.Items.FindByValue(theDSExistingForm.Tables[2].Rows[i]["ValueID"].ToString());
                    if (currentCheckBox != null)
                    {
                        currentCheckBox.Selected = true;
                    }
                }
            }

            if (theDSExistingForm.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < theDSExistingForm.Tables[3].Rows.Count; i++)
                {
                    ListItem currentCheckBox = cblReviewChecklist.Items.FindByValue(theDSExistingForm.Tables[3].Rows[i]["ValueID"].ToString());
                    if (currentCheckBox != null)
                    {
                        currentCheckBox.Selected = true;
                    }
                }
            }

            if (theDSExistingForm.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < theDSExistingForm.Tables[5].Rows.Count; i++)
                {
                    ListItem currentCheckBox = cblSignsOfHepatitis.Items.FindByValue(theDSExistingForm.Tables[5].Rows[i]["ValueID"].ToString());
                    if (currentCheckBox != null)
                    {
                        currentCheckBox.Selected = true;
                    }
                }
            }


        }

        private void LoadAutoPopulatingData()
        {
            DataSet theDSAutoPopulatingData = new DataSet();
            theDSAutoPopulatingData = TBScreeningManager.GetTBScreeningAutoPopulatingData(Convert.ToInt32(Session["PatientId"]));
            if (theDSAutoPopulatingData.Tables[0].Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(theDSAutoPopulatingData.Tables[0].Rows[0]["TBRegimenStartDate"].ToString()))
                    txtTBRegimenStartDate.Text = ((DateTime)theDSAutoPopulatingData.Tables[0].Rows[0]["TBRegimenStartDate"]).ToString("dd-MMM-yyyy");

                if (!String.IsNullOrEmpty(theDSAutoPopulatingData.Tables[0].Rows[0]["TBRegimenEndDate"].ToString()))
                    txtTBRegimenEndDate.Text = ((DateTime)theDSAutoPopulatingData.Tables[0].Rows[0]["TBRegimenEndDate"]).ToString("dd-MMM-yyyy");

                if (!String.IsNullOrEmpty(theDSAutoPopulatingData.Tables[0].Rows[0]["INHStartDate"].ToString()))
                    txtINHStartDate.Text = ((DateTime)theDSAutoPopulatingData.Tables[0].Rows[0]["INHStartDate"]).ToString("dd-MMM-yyyy");

                if (!String.IsNullOrEmpty(theDSAutoPopulatingData.Tables[0].Rows[0]["INHEndDate"].ToString()))
                    txtINHEndDate.Text = ((DateTime)theDSAutoPopulatingData.Tables[0].Rows[0]["INHEndDate"]).ToString("dd-MMM-yyyy");

                if (!String.IsNullOrEmpty(theDSAutoPopulatingData.Tables[0].Rows[0]["PyridoxineStartDate"].ToString()))
                    txtPyridoxineStartDate.Text = ((DateTime)theDSAutoPopulatingData.Tables[0].Rows[0]["PyridoxineStartDate"]).ToString("dd-MMM-yyyy");

                if (!String.IsNullOrEmpty(theDSAutoPopulatingData.Tables[0].Rows[0]["PyridoxineEndDate"].ToString()))
                    txtPyridoxineEndDate.Text = ((DateTime)theDSAutoPopulatingData.Tables[0].Rows[0]["PyridoxineEndDate"]).ToString("dd-MMM-yyyy");

            }
        }

        private void Bind_Select_Multiselect_Lists()
        {
            theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\AllMasters.con"));
            double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());
            if (age >= 15)
            {
                //adults
                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='TBAssessmentICF'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                //theDV.RowFilter = "CodeID=145";
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCheckedList(cblTBAssessmentICF, theDT, "Name", "ID");
            }
            else
            {
                //Paeds
                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='TBICFPaeds'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                //theDV.RowFilter = "CodeID=295";
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCheckedList(cblTBAssessmentICF, theDT, "Name", "ID");
            }

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='TBFindings'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=296";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlTBFindings, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='SputumSmear'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=120";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlSputumSmear, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='CXR'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=125";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlCXRResults, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='TBTypePeads'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=297";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlTBClassification, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='PeadsTBPatientType'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=298";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlPatientClassification, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='TBPlan'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=299";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlTBPLan, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='TBRegimen'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=147";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlTBRegimen, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='TBStopReason'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=602";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCheckedList(cblStopTBReason, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='TBSideEffects'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=603";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCheckedList(cblReviewChecklist, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='TBTreatmentOutcomesPeads'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=113";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlTBTreatment, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='GeneExpert'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=1035";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlGeneExpert, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='SputumDST'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=1036";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlSputumDST, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='IPT'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=1037";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.RadioButtonList(rdoLstIPT, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='SignsOfHepatitis'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCheckedList(cblSignsOfHepatitis, theDT, "Name", "ID");

            //theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            //theDVCodeID.RowFilter = "Name='SputumDST'";
            theDV = new DataView(theDSXML.Tables["Mst_Facility"]);
            theDV.Sort = "FacilityName ASC";
            //theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlPatientReferredForTreatment, theDT, "FacilityName", "FacilityID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='ReasonDeclinedIPT'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            //theDV.RowFilter = "CodeID=1036";
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlReasonDeclinedIPT, theDT, "Name", "ID");


        }

        private void AddAttributes()
        {
            //rdoAvailableTBResultsYes.Attributes.Add("OnClick", "show_hide('TBAvailableResults','visible');");
            string NoTBAvailableResultsScript = "show_hide('TBAvailableResults','notvisible');ClearTextBox('" + txtSputumSmearDate.ClientID + "');ClearTextBox('" + txtSputumDSTDate.ClientID + "');ClearTextBox('" + txtGeneExpertDate.ClientID + "');ClearTextBox('" + txtChestXrayDate.ClientID + "');ClearTextBox('" + txtTissueBiopsyDate.ClientID + "');ClearTextBox('" + txtOtherCXRResults.ClientID + "');ClearTextBox('" + txtOtherTBPlan.ClientID + "');ClearTextBox('" + txtOtherTBRegimen.ClientID + "');ClearTextBox('" + txtTBRegimenStartDate.ClientID + "');ClearTextBox('" + txtTBRegimenEndDate.ClientID + "');";
            NoTBAvailableResultsScript += "ClearSelectList('" + ddlSputumSmear.ClientID + "');ClearSelectList('" + ddlGeneExpert.ClientID + "');ClearSelectList('" + ddlSputumDST.ClientID + "');ClearSelectList('" + ddlCXRResults.ClientID + "');ClearSelectList('" + ddlTBClassification.ClientID + "');ClearSelectList('" + ddlPatientClassification.ClientID + "');ClearSelectList('" + ddlTBPLan.ClientID + "');ClearSelectList('" + ddlTBRegimen.ClientID + "');ClearSelectList('" + ddlTBTreatment.ClientID + "');";
            NoTBAvailableResultsScript += "ClearRadioButtons('" + rdoChestXrayYes.ClientID + "','" + rdoChestXrayNo.ClientID + "');ClearRadioButtons('" + rdoTissueBiopsyYes.ClientID + "','" + rdoTissueBiopsyNo.ClientID + "');";
            //rdoAvailableTBResultsNo.Attributes.Add("OnClick", NoTBAvailableResultsScript);
            ddlCXRResults.Attributes.Add("OnChange", "SelectOther('" + ddlCXRResults.ClientID + "','OtherCRXSpecify','" + txtOtherCXRResults.ClientID + "');");
            ddlTBPLan.Attributes.Add("OnChange", "SelectOtherSpecify('" + ddlTBPLan.ClientID + "','OtherTBPlanSpecify','" + txtOtherTBPlan.ClientID + "');");
            ddlTBRegimen.Attributes.Add("OnChange", "SelectOther('" + ddlTBRegimen.ClientID + "','OtherTBRegimenSpecify','" + txtOtherTBRegimen.ClientID + "');");
            ddlTBTreatment.Attributes.Add("OnChange", "SelectOther('" + ddlTBTreatment.ClientID + "','specifyOtheroutcome','" + txtOtherTreatmentOutcome.ClientID + "');");
            ddlReasonDeclinedIPT.Attributes.Add("OnChange", "SelectOther('" + ddlReasonDeclinedIPT.ClientID + "','otherReasonDeclinedIPTID','" + txtOtherReasonDeclinedIPT.ClientID + "');");
            //ddlTBFindings.Attributes.Add("OnChange", "SelectTBFindings('" + ddlTBFindings.ClientID + "','" + rdoAvailableTBResultsYes.ClientID + "','" + rdoAvailableTBResultsNo.ClientID + "');");
            cblTBAssessmentICF.Attributes.Add("OnClick", "DisableNoSignsSymptoms('" + cblTBAssessmentICF.ClientID + "','" + ddlTBFindings.ClientID + "');");

            ddlTBFindings.Attributes.Add("OnChange", TBFindingsScript);

            rdoLstIPT.Attributes.Add("OnClick", "SelectIPT('" + rdoLstIPT.ClientID + "','" + txtINHStartDate.ClientID + "','" + txtINHEndDate.ClientID + "','" + txtPyridoxineStartDate.ClientID + "','" + txtPyridoxineEndDate.ClientID + "','" + ddlReasonDeclinedIPT.ClientID + "','" + txtOtherReasonDeclinedIPT.ClientID + "');EnableDisableStopReason('" + rdoLstIPT.ClientID + "','" + cblStopTBReason.ClientID + "');");
            //rdoStartIPT.Attributes.Add("OnClick", "show_hide('INHStartEndDates','visible');show_hide('PyridoxineStartEnd','visible');");
            //rdoContinueIPT.Attributes.Add("OnClick", "show_hide('INHStartEndDates','visible');show_hide('PyridoxineStartEnd','visible');");
            //rdoStopIPT.Attributes.Add("OnClick", "show_hide('ifYesStopReason','visible');show_hide('INHStartEndDates','visible');show_hide('PyridoxineStartEnd','visible');");
            //rdoCompletedIPT.Attributes.Add("OnClick", "show_hide('ifYesStopReason','notvisible');show_hide('INHStartEndDates','visible');show_hide('PyridoxineStartEnd','visible');");
            //rdoDeclinedIPT.Attributes.Add("OnClick", "show_hide('ifYesStopReason','notvisible');show_hide('INHStartEndDates','notvisible');show_hide('PyridoxineStartEnd','notvisible');");

            rdoMissedAnyTBDosesYes.Attributes.Add("OnClick", "show_hide('MissedDosesYesReferredforadherence','visible');");
            rdoMissedAnyTBDosesNo.Attributes.Add("OnClick", "show_hide('MissedDosesYesReferredforadherence','notvisible');ClearRadioButtons('" + rdoReferredForAdherenceYes.ClientID + "','" + rdoReferredForAdherenceNo.ClientID + "')");
            //rdoTBConfirmedSuspectedYes.Attributes.Add("OnClick", "show_hide('INHStopDate','visible');");
            //rdoTBConfirmedSuspectedNo.Attributes.Add("OnClick", "show_hide('INHStopDate','notvisible');");
            rdoContactsScreenedForTBYes.Attributes.Add("OnClick", "show_hide('IfNoContactsScreenedSpecifyWhy','notvisible');ClearTextBox('" + txtSpecifyWhyContactNotScreenedForTB.ClientID + "')");
            rdoContactsScreenedForTBNo.Attributes.Add("OnClick", "show_hide('IfNoContactsScreenedSpecifyWhy','visible');");
            rdoChestXrayYes.Attributes.Add("OnClick", "show_hide('CXRResults','visible');");
            rdoChestXrayNo.Attributes.Add("OnClick", "show_hide('CXRResults','notvisible');ClearSelectList('" + ddlCXRResults.ClientID + "'); ClearTextBox('" + txtOtherCXRResults.ClientID + "')");

            txtSputumSmearDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
            txtChestXrayDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
            txtTissueBiopsyDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
            txtTBRegimenStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
            txtTBRegimenEndDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
            txtINHStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
            txtINHEndDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
            txtPyridoxineStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
            txtPyridoxineEndDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
            txtGeneExpertDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
            txtSputumDSTDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
            //txtINHStopDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");


            cblReviewChecklist.Attributes.Add("OnChange", "SelectOtherReviewChkList('" + cblReviewChecklist.ClientID + "','ReviewChkListSpecifyOtherTBSideEffects','" + txtSpecifyOtherTBSideEffects.ClientID + "');SignsOfHepatitisReviewChkList('" + cblReviewChecklist.ClientID + "','divSignsOfHepatitis','" + cblSignsOfHepatitis.ClientID + "');");

            //foreach (ListItem item in cblReviewChecklist.Items)
            //{
            //    item.Attributes.Add("OnChange", "SelectOtherReviewChkList('" + cblReviewChecklist.ClientID + "','ReviewChkListSpecifyOtherTBSideEffects','" + txtSpecifyOtherTBSideEffects.ClientID + "');SignsOfHepatitisReviewChkList('" + cblReviewChecklist.ClientID + "','divSignsOfHepatitis','" + cblSignsOfHepatitis.ClientID + "');");
            //    //item.Attributes.Add("onclick", "document.forms[0].isRecordModified.value=document.activeElement.checked");
            //}
            cblTBAssessmentICF.Attributes.Add("OnClick", "fnUncheckallVitals('" + cblTBAssessmentICF.ClientID + "');");
        }    



        private void showHideControls()
        {
            //if (rdoAvailableTBResultsYes.Checked==true)
            //{
            //    //medicalCondition = 1;
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction24", "show_hide('TBAvailableResults','visible');", true);
            //}

            //if (rdoAvailableTBResultsNo.Checked==true)
            //{
            //    //medicalCondition = 0;
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction25", "show_hide('TBAvailableResults','notvisible');", true);
            //}

            //IPT
            string IPTSelectedText = string.Empty;
            foreach (ListItem item in rdoLstIPT.Items)
            {
                if (item.Selected)
                {
                    IPTSelectedText = item.Text;
                }
            }

            if (IPTSelectedText == "Stop IPT")
            //if (rdoLstIPT.SelectedItem.Text == "Stop IPT")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction26", "SelectIPT('"+rdoLstIPT.ClientID+"');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction26", "SelectIPT('" + rdoLstIPT.ClientID + "');", true);
            }

            if (rdoLstIPT.SelectedItem != null)
            {
                if (rdoLstIPT.SelectedItem.Text == "Start IPT" || rdoLstIPT.SelectedItem.Text == "Continue IPT" || rdoLstIPT.SelectedItem.Text == "Completed IPT")
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "showINHPyridoxine", "show_hide('INHStartEndDates','visible');show_hide('PyridoxineStartEnd','visible');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showINHPyridoxine", "show_hide('INHStartEndDates','visible');show_hide('PyridoxineStartEnd','visible');", true);
                    //cblStopTBReason.Enabled = false;
                }
                else if (rdoLstIPT.SelectedItem.Text == "Declined IPT")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showDeclinedIPT", "show_hide('declinedIPTID','visible');", true);
                }
            }





            //TB Findings
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction27", "SelectTBFindings('" + ddlTBFindings.ClientID + "','" + rdoAvailableTBResultsYes.ClientID + "','" + rdoAvailableTBResultsNo.ClientID + "');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction27", "SelectTBFindings('" + ddlTBFindings.ClientID + "');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction27", "SelectTBFindings('" + ddlTBFindings.ClientID + "');", true);

            if (rdoMissedAnyTBDosesYes.Checked == true)
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction28", "show_hide('MissedDosesYesReferredforadherence','visible');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction28", "show_hide('MissedDosesYesReferredforadherence','visible');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction28", "show_hide('MissedDosesYesReferredforadherence','visible');", true);

            if (rdoMissedAnyTBDosesNo.Checked == true)
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction29", "show_hide('MissedDosesYesReferredforadherence','notvisible');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction29", "show_hide('MissedDosesYesReferredforadherence','notvisible');", true);

            if (rdoContactsScreenedForTBYes.Checked == true)
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction30", "show_hide('IfNoContactsScreenedSpecifyWhy','notvisible');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction30", "show_hide('IfNoContactsScreenedSpecifyWhy','notvisible');", true);

            if (rdoContactsScreenedForTBNo.Checked == true)
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction31", "show_hide('IfNoContactsScreenedSpecifyWhy','visible');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction31", "show_hide('IfNoContactsScreenedSpecifyWhy','visible');", true);

            if (rdoChestXrayYes.Checked == true)
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "showcxr", "show_hide('CXRResults','visible');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showcxr", "show_hide('CXRResults','visible');", true);

            //if (rdoTBConfirmedSuspectedYes.Checked == true)
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction32", "show_hide('INHStopDate','visible');", true);

            //if (rdoTBConfirmedSuspectedNo.Checked == true)
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction33", "show_hide('INHStopDate','notvisible');", true);
            if (ddlTBTreatment.SelectedItem.Text == "Other")
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "showOtherOutcome", "show_hide('specifyOtheroutcome','visible');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showOtherOutcome", "show_hide('specifyOtheroutcome','visible');", true);
            if (ddlCXRResults.SelectedItem.Text == "Other")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showCXRResults", "show_hide('OtherCRXSpecify','visible');", true);
            if (ddlTBRegimen.SelectedItem.Text == "Other")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showOtherTBRegimen", "show_hide('OtherTBRegimenSpecify','visible');", true);
            if (ddlTBPLan.SelectedItem.Text == "Other (Specify)")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showOtherTBPlan", "show_hide('OtherTBPlanSpecify','visible');", true);

            if (ddlReasonDeclinedIPT.SelectedItem.Text == "Other")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showOtherReasonIPTDeclined", "show_hide('otherReasonDeclinedIPTID','visible');", true);

            //ddlCXRResults.Attributes.Add("OnChange", "SelectOther('" + ddlCXRResults.ClientID + "','OtherCRXSpecify','" + txtOtherCXRResults.ClientID + "');");
            //ddlTBPLan.Attributes.Add("OnChange", "SelectOtherSpecify('" + ddlTBPLan.ClientID + "','OtherTBPlanSpecify','" + txtOtherTBPlan.ClientID + "');");
            //ddlTBRegimen.Attributes.Add("OnChange", "SelectOther('" + ddlTBRegimen.ClientID + "','OtherTBRegimenSpecify','" + txtOtherTBRegimen.ClientID + "');");

            foreach (ListItem item in cblReviewChecklist.Items)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction34", "SelectOtherReviewChkList('" + cblReviewChecklist.ClientID + "','ReviewChkListSpecifyOtherTBSideEffects','" + txtSpecifyOtherTBSideEffects.ClientID + "');SignsOfHepatitisReviewChkList('" + cblReviewChecklist.ClientID + "','divSignsOfHepatitis');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction34", "SelectOtherReviewChkList('" + cblReviewChecklist.ClientID + "','ReviewChkListSpecifyOtherTBSideEffects','" + txtSpecifyOtherTBSideEffects.ClientID + "');SignsOfHepatitisReviewChkList('" + cblReviewChecklist.ClientID + "','divSignsOfHepatitis');", true);
            }
        }

        
        

        

        //private void validateDates()
        //{

        //    DateTime temp;

        //    if (DateTime.TryParse(txtSputumSmearDate.Text, out temp))
        //    {
        //        sputumSmearDate = temp.ToString();
        //    }
        //    //else
        //    //{
        //    //    sputumSmearDate = "1900-01-01";
        //    //}

        //    if (DateTime.TryParse(txtGeneExpertDate.Text, out temp))
        //    {
        //        GeneExpertDate = temp.ToString();
        //    }
        //    //else
        //    //{
        //    //    GeneExpertDate = "1900-01-01";
        //    //}

        //    if (DateTime.TryParse(txtSputumDSTDate.Text, out temp))
        //    {
        //        SputumDSTDate = temp.ToString();
        //    }
        //    //else
        //    //{
        //    //    SputumDSTDate = "1900-01-01";
        //    //}

        //    if (DateTime.TryParse(txtChestXrayDate.Text, out temp))
        //    {
        //        chestXRayDate = temp.ToString();
        //    }
        //    //else
        //    //{
        //    //    chestXRayDate = "1900-01-01";
        //    //}

        //    if (DateTime.TryParse(txtTissueBiopsyDate.Text, out temp))
        //    {
        //        TissueBiopsyDate = temp.ToString();
        //    }
        //    //else
        //    //{
        //    //    TissueBiopsyDate = "1900-01-01";
        //    //}

        //    if (DateTime.TryParse(txtTBRegimenStartDate.Text, out temp))
        //    {
        //        TBRegimenStartDate = temp.ToString();
        //    }
        //    //else
        //    //{
        //    //    TBRegimenStartDate = "1900-01-01";
        //    //}

        //    if (DateTime.TryParse(txtTBRegimenEndDate.Text, out temp))
        //    {
        //        TBRegimenEndDate = temp.ToString();
        //    }
        //    //else
        //    //{
        //    //    TBRegimenEndDate = "1900-01-01";
        //    //}

        //    if (DateTime.TryParse(txtINHStartDate.Text, out temp))
        //    {
        //        INHStartDate = temp.ToString();
        //    }
        //    //else
        //    //{
        //    //    INHStartDate = "1900-01-01";
        //    //}

        //    if (DateTime.TryParse(txtINHEndDate.Text, out temp))
        //    {
        //        INHEndDate = temp.ToString();
        //    }
        //    //else
        //    //{
        //    //    INHEndDate = "1900-01-01";
        //    //}

        //    if (DateTime.TryParse(txtPyridoxineStartDate.Text, out temp))
        //    {
        //        PyridoxineStartDate = temp.ToString();
        //    }
        //    //else
        //    //{
        //    //    PyridoxineStartDate = "1900-01-01";
        //    //}

        //    if (DateTime.TryParse(txtPyridoxineEndDate.Text, out temp))
        //    {
        //        PyridoxineEndDate = temp.ToString();
        //    }
        //    //else
        //    //{
        //    //    PyridoxineEndDate = "1900-01-01";
        //    //}

        //    //if (DateTime.TryParse(txtINHStopDate.Text, out temp))
        //    //{
        //    //    INHStopDate = temp.ToString();
        //    //}
        //    //else
        //    //{
        //    //    INHStopDate = "1900-01-01";
        //    //}
        //}

        //protected void rdoAvailableTBResultsNo_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdoAvailableTBResultsNo.Checked == true)
        //    {
        //        availableTBResults = "0";
        //    }
        //}

        protected void rdoChestXrayYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoChestXrayYes.Checked == true)
                chestXRay = "1";
        }

        
        protected void ddlTBFindings_SelectedIndexChanged(object sender, EventArgs e)
        {
            //System.Windows.MessageBox.Show("test");
            if (ddlTBFindings.SelectedItem.Text == "No Signs or symptoms")
            {
                string NoTBAvailableResultsScript = "show_hide('TBAvailableResults','notvisible');ClearTextBox('" + txtSputumSmearDate.ClientID + "');ClearTextBox('" + txtSputumDSTDate.ClientID + "');ClearTextBox('" + txtGeneExpertDate.ClientID + "');ClearTextBox('" + txtChestXrayDate.ClientID + "');ClearTextBox('" + txtTissueBiopsyDate.ClientID + "');ClearTextBox('" + txtOtherCXRResults.ClientID + "');ClearTextBox('" + txtOtherTBPlan.ClientID + "');ClearTextBox('" + txtOtherTBRegimen.ClientID + "');ClearTextBox('" + txtTBRegimenStartDate.ClientID + "');ClearTextBox('" + txtTBRegimenEndDate.ClientID + "');";
                NoTBAvailableResultsScript += "ClearSelectList('" + ddlSputumSmear.ClientID + "');ClearSelectList('" + ddlGeneExpert.ClientID + "');ClearSelectList('" + ddlSputumDST.ClientID + "');ClearSelectList('" + ddlCXRResults.ClientID + "');ClearSelectList('" + ddlTBClassification.ClientID + "');ClearSelectList('" + ddlPatientClassification.ClientID + "');ClearSelectList('" + ddlTBPLan.ClientID + "');ClearSelectList('" + ddlTBRegimen.ClientID + "');ClearSelectList('" + ddlTBTreatment.ClientID + "');";
                NoTBAvailableResultsScript += "ClearRadioButtons('" + rdoChestXrayYes.ClientID + "','" + rdoChestXrayNo.ClientID + "');ClearRadioButtons('" + rdoTissueBiopsyYes.ClientID + "','" + rdoTissueBiopsyNo.ClientID + "');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction98", NoTBAvailableResultsScript, true);
            }
        }      

        

        protected void rdoLstIPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoLstIPT.SelectedItem.Text == "Start IPT" || rdoLstIPT.SelectedItem.Text == "Continue IPT" || rdoLstIPT.SelectedItem.Text == "Completed IPT")
            {
                //cblStopTBReason.Enabled = false;
            }
            else
            {
                //cblStopTBReason.Enabled = true;
            }
        }


    }
}
