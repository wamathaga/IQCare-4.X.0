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
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Collections;
using Interface.Service;


namespace PresentationApp.ClinicalForms
{
	public partial class frmClinical_KNH_ExpressForm : LogPage
	{
        IKNHStaticForms ExpressFormManager;
        DataSet theDSXML;
        IQCareUtils theUtils = new IQCareUtils();
        BindFunctions BindManager = new BindFunctions();
        DataView theDV, theDVCodeID;
        DataTable theDT;
        //static int ptnAccByCareGiver, medicalCondition, areYouOnFollowUp,missedAnyDoses,delayedTakingMedication,labEvaluation,issuedWithCondoms, pregIntBeforeNxtVist;
        //static int fertilityOptions, dualContraception, otherFPMethod, screenedForCancer, referredForCervicalScreening;
        static DateTime startTime;
        int expressTriageTabID, expressTriageFeatureID, expressCATabID, expressCAFeatureID, expressTBTabID, expressTBFeatureID;

        DataSet dsExpressTriage = new DataSet();
        DataSet dsExpressCA = new DataSet();

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    LoadTriageTabDetails();
                    LoadCATabDetails();
                    //ViewState["PatientVisitId"] = Convert.ToInt32(Session["PatientVisitId"]);
                }
                else if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    txtVisitDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    //ViewState["PatientVisitId"] = Convert.ToInt32(Session["PatientVisitId"]);
                    LoadAutoPopulatingData();
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "lastTwoWeeks", "window.onload = function() { admittedLastTwoWeeks(); };", true);
                //AddAttributes();
            }


            showHideControls();
            checkIfPreviuosTabSaved();
            this.UserControlKNH_SignatureTriage.lblSignature.Text = ExpressFormManager.GetSignature("ExpressTriage", Convert.ToInt32(Session["PatientVisitId"]));
            this.UserControlKNH_SignatureCA.lblSignature.Text = ExpressFormManager.GetSignature("ExpressClinicalAssessment", Convert.ToInt32(Session["PatientVisitId"]));
        }

        protected void Page_Load(object sender, EventArgs e)
		{
            ExpressFormManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");

            getTabIds();
           
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Express";
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Express";

            if (Request.QueryString["name"] == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showDeletebutton", "show_hide('tblDeleteButton','visible');", true);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "hideSavebutton", "show_hide('tblSaveButton','notvisible');", true);
            }
            
            if (!IsPostBack)
            {
                startTime = DateTime.Now;
                Bind_Select_Multiselect_Lists();
                AddAttributes();
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    useExpressForm();
                }
            }
		}

        private void getTabIds()
        {
            theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(theDSXML.Tables["Mst_FormBuilderTab"]);
            DataTable dtTabId = theDV.ToTable();
            //featureID = ApplicationAccess.KNHPaediatricInitialEvaulation;
            if (dtTabId.Rows.Count > 0)
            {
                foreach (DataRow drTabsId in dtTabId.Rows)
                {
                    switch (drTabsId["TabName"].ToString())
                    {
                        case "ExpressTriage":
                        expressTriageTabID = Convert.ToInt32(drTabsId["TabID"]);
                        expressTriageFeatureID = Convert.ToInt32(drTabsId["FeatureID"]);
                        break;

                        case "ExpressClinicalAssessment":
                        expressCATabID = Convert.ToInt32(drTabsId["TabID"]);
                        expressCAFeatureID = Convert.ToInt32(drTabsId["FeatureID"]);
                        break;

                        case "TBScreening":
                        expressTBTabID = Convert.ToInt32(drTabsId["TabID"]);
                        expressTBFeatureID = Convert.ToInt32(drTabsId["FeatureID"]);
                        break;
                    }
                }
            }
        }


        public void checkIfPreviuosTabSaved()
        {
            securityPertab();
            DataSet ExpressTriage = ExpressFormManager.CheckIfPreviuosTabSaved("ExpressTriage", Convert.ToInt32(Session["PatientVisitId"]));
            if (ExpressTriage.Tables[0].Rows.Count == 0)
            {
                
                btnCASave.Enabled = false;
                btnCADQC.Enabled = false;
                btnCAPrint.Enabled = false;
            }
            else
            {
                securityPertab();
            }

            DataSet ExpressCA = ExpressFormManager.CheckIfPreviuosTabSaved("ExpressClinicalAssessment", Convert.ToInt32(Session["PatientVisitId"]));
            if (ExpressCA.Tables[0].Rows.Count == 0)
            {
                this.UserControlKNH_TBScreening1.btnTBSave.Enabled = false;
                this.UserControlKNH_TBScreening1.btnTBDQC.Enabled = false;
                this.UserControlKNH_TBScreening1.btnTBPrint.Enabled = false;
                
            }
            else
            {
                securityPertab();
            }

            ExpressTriage.Dispose();
            ExpressCA.Dispose();
        }

        public void securityPertab()
        {
            AuthenticationManager Authentication = new AuthenticationManager();
            //triage
            Authentication.TabUserRights(btnTriageSave, btnTraigePrint, expressTriageFeatureID, expressTriageTabID);

            //CA
            Authentication.TabUserRights(btnCASave, btnCAPrint, expressCAFeatureID, expressCATabID);

            //TB
            Authentication.TabUserRights(this.UserControlKNH_TBScreening1.btnTBSave, this.UserControlKNH_TBScreening1.btnTBPrint, expressTBFeatureID, expressTBTabID);
        }


        public void useExpressForm()
        {
            DataSet ExpressFormRules = new DataSet();
            ExpressFormRules = ExpressFormManager.useExpressFormRules(Convert.ToInt32(Session["PatientId"]));
            //First 2 visits of a patient cannot be carried out with an Express Form
            if (ExpressFormRules.Tables[1].Rows.Count < 2)
            {
                string script = "alert('Express form cannot be used for the first 2 visits. Please use Follow Up form. Redirecting...');";
                script += "window.location.replace('frmClinical_RevisedAdultfollowup.aspx');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "2FirstVisits", script, true);
                return;

            }

            //Do not use Express Form 3 consecutive times.
            int threeConsecutive = 0;
            if (ExpressFormRules.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ExpressFormRules.Tables[0].Rows.Count; i++)
                {
                    if (ExpressFormRules.Tables[0].Rows[i]["visittype"].ToString() == "31")
                    {
                        threeConsecutive += 1;
                    }
                }
            }
            if (threeConsecutive == 2)
            {
                string script = "alert('Express form already used 2 consecutive times. Please use Follow Up form. Redirecting...');";
                script += "window.location.replace('frmClinical_RevisedAdultfollowup.aspx');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ExpressFormUsed2ConsecutiveTimes", script, true);
                return;
            }

            //TBRegimen Start Date less than 30 days
            if (ExpressFormRules.Tables[2].Rows.Count > 0)
            {
                string script = "alert('TB Regimen start date is less than 30 days. Please use Followup form. Redirecting...');";
                script += "window.location.replace('frmClinical_RevisedAdultfollowup.aspx');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "TBRegimenStartdateLessthan30", script, true);
                return;
            }

            //if TB Findings is suspected at last visit, do not use express form
            if (ExpressFormRules.Tables[3].Rows.Count > 0)
            {
                string script = "alert('TB suspected in previous visit. Please use Followup form. Redirecting...');";
                script += "window.location.replace('frmClinical_RevisedAdultfollowup.aspx');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "TBSuspectedPreviuosVisit", script, true);
                return;
            }

            
        }

        private void Bind_Select_Multiselect_Lists()
        {
            ICommonData oCommonData = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData,BusinessProcess.Service");
            DataTable dtAllCode = oCommonData.getAllMSTCode();

            //theDSXML = new DataSet(); 
            //theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));

            theDVCodeID = new DataView(dtAllCode);
            theDVCodeID.RowFilter = "Name='TreatmentSupporterRelationship'";

            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlCareGiverRelationship, theDT, "Name", "ID");
            theDVCodeID.Dispose();

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='SpecificMedicalCondition'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCheckedList(cblPreExistingMedConditions, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='OIProphylaxis'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlOIProphylaxis, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='ReasonCTXpresribed'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlCotrimoxazolePrescribed, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='Fluconazole'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlFluconazolePrescribedFor, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='GivenPWPMessages'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlPwPMessageGiven, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='FPmethod'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlSpecifyOtherFPMethod, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='CervicalCancerScreeningResults'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddlCaCervixScreeningResults, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='ShortTermEffects'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCheckedList(cblshorttermeffects, theDT, "Name", "ID");

            theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='LongTermEffects'";
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCheckedList(cbllongtermeffects, theDT, "Name", "ID");

        }

        private void AddAttributes()
        {
            rdoPtnAccByCareGiverYes.Attributes.Add("OnClick", "show_hide('CaregiverRelationship','visible');");
            rdoPtnAccByCareGiverNo.Attributes.Add("OnClick", "show_hide('CaregiverRelationship','notvisible');ClearSelectList('" + ddlCareGiverRelationship.ClientID + "');");
            rdoOnFollowUpYes.Attributes.Add("OnClick", "show_hide('lastFollowUpDate','visible');");
            rdoOnFollowUpNo.Attributes.Add("OnClick", "show_hide('lastFollowUpDate','notvisible');ClearTextBox('" + txtLastFollowUp.ClientID + "')");
            rdoMissedAnyDosesYes.Attributes.Add("OnClick", "show_hide('specifyMissedDoses','visible');");
            rdoMissedAnyDosesNo.Attributes.Add("OnClick", "show_hide('specifyMissedDoses','notvisible');ClearTextBox('" + txtSpecifyWhyDosesMissed.ClientID + "')");
            rdoLabEvaluationYes.Attributes.Add("OnClick", "show_hide('LabEvaluationItems','visible');show_hide('LabReviewOtherTests','visible');");
            rdoLabEvaluationNo.Attributes.Add("OnClick", "show_hide('LabEvaluationItems','notvisible');show_hide('LabReviewOtherTests','notvisible');");
            ddlOIProphylaxis.Attributes.Add("OnChange", "SelectOther('" + ddlOIProphylaxis.ClientID + "','OtherOIProphylaxis','" + txtSpecifyOtherOIProphylaxis.ClientID + "');SelectCotrimoxazoleFluconazole('" + ddlOIProphylaxis.ClientID + "','Cotrimoxazoleprescribedfor','Fluconazoleprescribedfor','" + ddlCotrimoxazolePrescribed.ClientID + "','"+ddlFluconazolePrescribedFor.ClientID+"');");
            rdoIssuedWithCondomsYes.Attributes.Add("OnClick", "show_hide('ReasonsCondomNotIssued','notvisible');");
            rdoIssuedWithCondomsNo.Attributes.Add("OnClick", "show_hide('ReasonsCondomNotIssued','visible');");
            rdoPregBeforeNextVisitYes.Attributes.Add("OnClick", "show_hide('discussedFertilityOptions','visible');show_hide('discussedDualContraception','notvisible');ClearRadioButtons('"+rdoDiscussedDualContraceptionYes.ClientID+"','"+rdoDiscussedDualContraceptionNo.ClientID+"');");
            rdoPregBeforeNextVisitNo.Attributes.Add("OnClick", "show_hide('discussedFertilityOptions','notvisible');show_hide('discussedDualContraception','visible');ClearRadioButtons('" + rdoDiscussedFertilityOptionsYes.ClientID + "','" + rdoDiscussedFertilityOptionsNo.ClientID + "');");
            rdoOtherFPMethodYes.Attributes.Add("OnClick", "show_hide('specifyOtherFPMethod','visible');");
            rdoOtherFPMethodNo.Attributes.Add("OnClick", "show_hide('specifyOtherFPMethod','notvisible');ClearSelectList('" + ddlSpecifyOtherFPMethod.ClientID + "');");
            rdoscreenedForCervicalCancerYes.Attributes.Add("OnClick", "show_hide('cacervixscreenresults','visible');show_hide('referredForCervicalScreening','notvisible');ClearRadioButtons('"+rdoReferredForCervicalScreeningYes.ClientID+"','"+rdoReferredForCervicalScreeningNo.ClientID+"');");
            rdoscreenedForCervicalCancerNo.Attributes.Add("OnClick", "show_hide('cacervixscreenresults','notvisible');show_hide('referredForCervicalScreening','visible');ClearSelectList('" + ddlCaCervixScreeningResults.ClientID + "');");

            txtVisitDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");

            cblshorttermeffects.Attributes.Add("OnClick", "CheckBoxHideUnhideExpress('" + cblshorttermeffects.ClientID + "','OtherShortTermSideEffect','Other Specify','" + txtOtherShortTermSideEffect.ClientID + "');");
            cbllongtermeffects.Attributes.Add("OnClick", "CheckBoxHideUnhideExpress('" + cbllongtermeffects.ClientID + "','OtherLongTermSideEffect','Other specify','" + txtOtherLongTermSideEffect.ClientID + "');");
            cblPreExistingMedConditions.Attributes.Add("OnClick", "CheckBoxHideUnhideExpress('" + cblPreExistingMedConditions.ClientID + "','divSpecifyOthermedicalCondition','Other medical conditions','" + txtSpecifyOthermedicalCondition.ClientID + "');");
        }

        protected void btnTriageSave_Click(object sender, EventArgs e)
        {
            if (Session["PatientVisitId"].ToString() == "0")
            {
                if(checkduplicateVisit() == true)
                {
                    return;
                }
            }

            if (txtVisitDate.Value != "")
            {
                
                if (checkRequiredFieldsTriage() == true)
                {
                    try
                    {
                        Hashtable HT = TriageHT();
                        DataSet saveTriage = ExpressFormManager.SaveUpdateExpressFormTriageTab(HT, getCheckBoxListItemValues(cblPreExistingMedConditions), getCheckBoxListItemValues(this.UserControlKNH_VitalSigns1.cblReferredTo));
                        if (Session["PatientVisitId"].ToString() == "0")
                        {
                            if (saveTriage.Tables[0].Rows.Count > 0)
                            {
                                Session["PatientVisitId"] = saveTriage.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        SaveCancel("Triage");
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "TraigeSaveUpdate", "alert('Data on Triage tab saved successfully.');", true);
                        startTime = DateTime.Now;
                        checkIfPreviuosTabSaved();
                        tabControl.ActiveTabIndex = 1;
                    }
                    catch (Exception ex)
                    {
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "TraigeSaveUpdateError", "alert('Error Occurred :'" + ex.Message + "'.');", true);
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["MessageText"] = "Error Occurred :" + ex.Message + ".";
                        IQCareMsgBox.Show("#C1", theBuilder, this);
                        
                    }
                }
                
            }
            else
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Visit Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "PtnAccByCareG45", "alert('Visit Date is Required.');", true);
            }
            
        }
        private void SaveCancel(string tabname)
        {
            
            IQCareMsgBox.NotifyAction(tabname + " Tab saved successfully.", Header.InnerText, true, this, "");
            
        }
        public bool checkduplicateVisit()
        {
            bool duplicate = false;
            if (txtVisitDate.Value != "")
            {
                DataSet dsDuplicateVisit = ExpressFormManager.checkDuplicateVisit(txtVisitDate.Value, 31, Convert.ToInt32(Session["PatientId"]));
                if (dsDuplicateVisit.Tables[0].Rows.Count > 0)
                {
                    duplicate = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "duplicateForm", "alert('Form already exists. " + Convert.ToDateTime(txtVisitDate.Value).ToString("dd-MMM-yyyy") + "');", true);
                }
            }
            else
            {
                duplicate = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "blankVisitdate", "alert('Visit date cannot be blank.');", true);
            }

            return duplicate;
        }

        private bool checkRequiredFieldsTriage()
        {
            bool Required = true;

            if (rdoPtnAccByCareGiverYes.Checked == false && rdoPtnAccByCareGiverNo.Checked == false)
            {
                Required = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PtnAccByCareG", "alert('Patient accompanied by caregiver has not been filled in.');", true);
                lblPtnAccByCareGiver.ForeColor = Color.Red;
                lblClientInfo.ForeColor = Color.Red;
                return Required;
            }
            else
            {
                lblPtnAccByCareGiver.ForeColor = Color.Black;
                lblClientInfo.ForeColor = Color.Black;
            }

            if (this.UserControlKNH_VitalSigns1.txtBPDiastolic.Text == "" || this.UserControlKNH_VitalSigns1.txtBPSystolic.Text == "")
            {
                Required = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "diastolicMissing", "alert('BP Diastolic/Systolic has not been filled in.');", true);

                this.UserControlKNH_VitalSigns1.lblBP.ForeColor = Color.Red;
                lblVitalSigns.ForeColor = Color.Red;
                return Required;
            }
            else
            {
                this.UserControlKNH_VitalSigns1.lblBP.ForeColor = Color.Black;
            }

            if (this.UserControlKNH_VitalSigns1.txtWeight.Text == "")
            {
                Required = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "weightMissing", "alert('Weight has not been filled in.');", true);

                this.UserControlKNH_VitalSigns1.lblWeight.ForeColor = Color.Red;
                lblVitalSigns.ForeColor = Color.Red;
                return Required;
            }
            else
            {
                this.UserControlKNH_VitalSigns1.lblWeight.ForeColor = Color.Black;
            }

            if (this.UserControlKNH_VitalSigns1.txtHeight.Text == "")
            {
                Required = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "heightMissing", "alert('Height has not been filled in.');", true);

                this.UserControlKNH_VitalSigns1.lblHeight.ForeColor = Color.Red;
                lblVitalSigns.ForeColor = Color.Red;
                return Required;
            }
            else
            {
                this.UserControlKNH_VitalSigns1.lblHeight.ForeColor = Color.Black;
            }

            return Required;
        }

        private bool checkRequiredFieldsCA()
        {
            bool Required = true;

            if (this.UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedValue == "0")
            {
                Required = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "missingTreatmentPlam", "alert('Treatment plan has not been selected.');", true);
                lblRegimenPrescribed.ForeColor = Color.Red;
                this.UserControlKNH_Pharmacy1.lblTreatmentplan.ForeColor = Color.Red;
            }
            else
            {
                lblRegimenPrescribed.ForeColor = Color.Black;
                this.UserControlKNH_Pharmacy1.lblTreatmentplan.ForeColor = Color.Black;
            }

            return Required;
        }

        private DataTable getCheckBoxListItemValues(CheckBoxList cbl)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value", typeof(int));
            for (int i = 0; i < cbl.Items.Count; i++)
            {
                if (cbl.Items[i].Selected)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = cbl.Items[i].Value;
                    dt.Rows.Add(dr);
                }
            }
            
            return dt;
        }
        
        protected void rdoPtnAccByCareGiverYes_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoPtnAccByCareGiverYes.Checked)
            //{
            //    ptnAccByCareGiver = 1;

            //}
        }

        protected void rdoPtnAccByCareGiverNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoPtnAccByCareGiverNo.Checked)
            //{
            //    ptnAccByCareGiver = 0;
            //}
        }

        protected void rdoOnFollowUpYes_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoOnFollowUpYes.Checked)
            //{
            //    areYouOnFollowUp = 1;
            //}
        }

        protected void rdoOnFollowUpNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoOnFollowUpNo.Checked)
            //{
            //    areYouOnFollowUp = 0;
            //}
        }

        protected void btnTriageClose_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }

        private void showHideControls()
        {
            //Traige
            if (rdoPtnAccByCareGiverYes.Checked)
            {
                //ptnAccByCareGiver = 1;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction1", "show_hide('CaregiverRelationship','visible');", true);
            }

            if (rdoPtnAccByCareGiverNo.Checked)
            {
                //ptnAccByCareGiver = 0;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction2", "show_hide('CaregiverRelationship','notvisible');", true);
            }


            if (rdoOnFollowUpYes.Checked)
            {
                //areYouOnFollowUp = 1;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction5", "show_hide('lastFollowUpDate','visible');", true);
            }

            if (rdoOnFollowUpNo.Checked)
            {
                //areYouOnFollowUp = 0;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction6", "show_hide('lastFollowUpDate','notvisible');", true);
            }


            //clinical Assessment
            
            if (rdoMissedAnyDosesYes.Checked)
            {
                //missedAnyDoses = 1;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction7", "show_hide('specifyMissedDoses','visible');", true);
            }
            if (rdoMissedAnyDosesNo.Checked)
            {
                //missedAnyDoses = 0;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction8", "show_hide('specifyMissedDoses','notvisible');", true);
            }
           
            if (rdoIssuedWithCondomsYes.Checked)
            {
                //issuedWithCondoms = 1;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction13", "show_hide('ReasonsCondomNotIssued','notvisible');", true);
            }
            if (rdoIssuedWithCondomsNo.Checked)
            {
                //issuedWithCondoms = 0;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction14", "show_hide('ReasonsCondomNotIssued','visible');", true);
            }
            if (rdoPregBeforeNextVisitYes.Checked)
            {
                //pregIntBeforeNxtVist = 1;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction15", "show_hide('discussedFertilityOptions','visible');show_hide('discussedDualContraception','notvisible');", true);
            }
            if (rdoPregBeforeNextVisitNo.Checked)
            {
                //pregIntBeforeNxtVist = 0;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction16", "show_hide('discussedFertilityOptions','notvisible');show_hide('discussedDualContraception','visible');", true);
            }
            if (rdoOtherFPMethodYes.Checked)
            {
                //otherFPMethod = 1;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction17", "show_hide('specifyOtherFPMethod','visible');", true);
            }
            if (rdoOtherFPMethodNo.Checked)
            {
                //otherFPMethod = 0;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction18", "show_hide('specifyOtherFPMethod','notvisible');", true);
            }
            if (rdoscreenedForCervicalCancerYes.Checked)
            {
                //screenedForCancer = 1;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction19", "show_hide('cacervixscreenresults','visible');show_hide('referredForCervicalScreening','notvisible');", true);
            }

            if (rdoscreenedForCervicalCancerNo.Checked)
            {
                //screenedForCancer = 0;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction20", "show_hide('cacervixscreenresults','notvisible');show_hide('referredForCervicalScreening','visible');", true);
            }

            if (ddlOIProphylaxis.SelectedItem.Text == "Cotrimoxazole")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction22", "show_hide('Cotrimoxazoleprescribedfor','visible');", true);
            }
            else if (ddlOIProphylaxis.SelectedItem.Text == "Fluconazole")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showFluconazole", "show_hide('Fluconazoleprescribedfor','visible');", true);
            }
            else if (ddlOIProphylaxis.SelectedItem.Text == "CTX and Fluconazol")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showFluconazoleNCTX", "show_hide('Cotrimoxazoleprescribedfor','visible');show_hide('Fluconazoleprescribedfor','visible');", true);
            }


            if (ddlOIProphylaxis.SelectedItem.Text == "Other")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction23", "show_hide('OtherOIProphylaxis','visible');", true);
            }

            if (this.UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedItem.Text == "Start new treatment (naive patient)")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "treatmentplan1", "show_hide('divEligiblethrough','visible');", true);
            }
            else if (this.UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedItem.Text == "Change regimen")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "treatmentplan2", "show_hide('divARTchangecode','visible');", true);
            }
            else if (this.UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedItem.Text == "Stop treatment")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "treatmentplan3", "show_hide('divARTstopcode','visible');", true);
            }
            else if (this.UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedItem.Text == "Switch to second line")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "treatmentplan4", "show_hide('divReasonforswitchto2ndlineregimen','visible');", true);
            }
            else
            {
            }

            foreach (ListItem item in cblPreExistingMedConditions.Items)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showOtherPreexisting", "CheckBoxHideUnhideExpress('" + cblPreExistingMedConditions.ClientID + "','divSpecifyOthermedicalCondition','Other medical conditions','" + txtSpecifyOthermedicalCondition.ClientID + "');", true);
            }

        }

        private void LoadAutoPopulatingData()
        {
            string script = string.Empty;
            DataSet dsAutopopulate = new DataSet();
            dsAutopopulate = ExpressFormManager.GetExpressFormAutoPopulatingData(Convert.ToInt32(Session["PatientId"]));

            if (dsAutopopulate.Tables[0].Rows.Count > 0)
            {
                //getUserControlValues();
                this.UserControlKNH_VitalSigns1.txtHeight.Text = dsAutopopulate.Tables[0].Rows[0]["Height"].ToString();
            }

            if (dsAutopopulate.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < dsAutopopulate.Tables[1].Rows.Count; i++)
                {
                    ListItem currentCheckBox = cblPreExistingMedConditions.Items.FindByValue(dsAutopopulate.Tables[1].Rows[i]["ValueID"].ToString());
                    if (currentCheckBox != null)
                    {
                        currentCheckBox.Selected = true;
                    }
                }
            }

            if (dsAutopopulate.Tables[2].Rows.Count > 0)
            {
                ddlOIProphylaxis.SelectedValue = dsAutopopulate.Tables[2].Rows[0]["OIProphylaxis"].ToString();
                ddlCotrimoxazolePrescribed.SelectedValue = dsAutopopulate.Tables[2].Rows[0]["CotrimoxazolePrescribedFor"].ToString();
                txtSpecifyOtherOIProphylaxis.Text = dsAutopopulate.Tables[2].Rows[0]["OtherOIProphylaxis"].ToString();
            }
        }

        private void LoadTriageTabDetails()
        {
            string script = string.Empty;
            DataSet ViewExistingForm = new DataSet();
            ViewExistingForm = ExpressFormManager.GetExpressFormData(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));
            if (ViewExistingForm.Tables[0].Rows.Count > 0)
            {
                txtVisitDate.Value = ((DateTime)ViewExistingForm.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
            }
            if (ViewExistingForm.Tables[1].Rows.Count > 0)
            {
                if (ViewExistingForm.Tables[1].Rows[0]["PtnAccompaniedByCareGiver"].ToString() == "True")
                {
                    rdoPtnAccByCareGiverYes.Checked = true;
                }
                else if (ViewExistingForm.Tables[1].Rows[0]["PtnAccompaniedByCareGiver"].ToString() == "False")
                {
                    rdoPtnAccByCareGiverNo.Checked = true;
                }
                else
                {
                    rdoPtnAccByCareGiverYes.Checked = false;
                    rdoPtnAccByCareGiverNo.Checked = false;
                }

                ddlCareGiverRelationship.SelectedValue = ViewExistingForm.Tables[1].Rows[0]["CareGiverRelationship"].ToString();

                txtSpecifyOthermedicalCondition.Text = ViewExistingForm.Tables[1].Rows[0]["OtherMedicalCondition"].ToString();

                if (ViewExistingForm.Tables[1].Rows[0]["AreYouOnFollowUp"].ToString() == "True")
                {
                    rdoOnFollowUpYes.Checked = true;
                }
                else if (ViewExistingForm.Tables[1].Rows[0]["AreYouOnFollowUp"].ToString() == "False")
                {
                    rdoOnFollowUpNo.Checked = true;
                }
                else
                {
                    rdoOnFollowUpYes.Checked = false;
                    rdoOnFollowUpNo.Checked = false;
                }

                txtLastFollowUp.Text = ViewExistingForm.Tables[1].Rows[0]["LastFollowUpDetails"].ToString();

                this.UserControlKNH_VitalSigns1.txtnursescomments.Text = ViewExistingForm.Tables[1].Rows[0]["NurseComments"].ToString();
                this.UserControlKNH_VitalSigns1.txtReferToSpecialistClinic.Text = ViewExistingForm.Tables[1].Rows[0]["SpecialistReferral"].ToString();
                this.UserControlKNH_VitalSigns1.txtSpecifyOtherRefferedTo.Text = ViewExistingForm.Tables[1].Rows[0]["OtherReferral"].ToString();
            }

            if (ViewExistingForm.Tables[2].Rows.Count > 0)
            {
                this.UserControlKNH_VitalSigns1.txtTemp.Text = ViewExistingForm.Tables[2].Rows[0]["Temp"].ToString();
                this.UserControlKNH_VitalSigns1.txtRR.Text = ViewExistingForm.Tables[2].Rows[0]["RR"].ToString();
                this.UserControlKNH_VitalSigns1.txtHR.Text = ViewExistingForm.Tables[2].Rows[0]["HR"].ToString();
                this.UserControlKNH_VitalSigns1.txtBPDiastolic.Text = ViewExistingForm.Tables[2].Rows[0]["BPDiastolic"].ToString();
                this.UserControlKNH_VitalSigns1.txtBPSystolic.Text = ViewExistingForm.Tables[2].Rows[0]["BPSystolic"].ToString();
                this.UserControlKNH_VitalSigns1.txtHeight.Text = ViewExistingForm.Tables[2].Rows[0]["Height"].ToString();
                this.UserControlKNH_VitalSigns1.txtWeight.Text = ViewExistingForm.Tables[2].Rows[0]["Weight"].ToString();
            }

            theDV = new DataView(ViewExistingForm.Tables[3]);
            theDV.RowFilter = "FieldName = 'SpecificMedicalCondition'";
            theUtils.CheckBoxListBindExistingInfo(theDV.ToTable(), cblPreExistingMedConditions);

            theDV = new DataView(ViewExistingForm.Tables[3]);
            theDV.RowFilter = "FieldName = 'RefferedToFUpF'";
            theUtils.CheckBoxListBindExistingInfo(theDV.ToTable(), this.UserControlKNH_VitalSigns1.cblReferredTo);
            for (int i = 0; i < this.UserControlKNH_VitalSigns1.cblReferredTo.Items.Count; i++)
            {
                if (this.UserControlKNH_VitalSigns1.cblReferredTo.Items[i].Text == "Other Specialist Clinic")
                {
                    if (this.UserControlKNH_VitalSigns1.cblReferredTo.Items[i].Selected == true)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "divotherspecialistClinic", "ShowHide('TriagedivReferToSpecialistClinic','show');", true);
                    }
                }

                if (this.UserControlKNH_VitalSigns1.cblReferredTo.Items[i].Text == "Other (Specify)")
                {
                    if (this.UserControlKNH_VitalSigns1.cblReferredTo.Items[i].Selected == true)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "divotherclinicreferral", "ShowHide('TriagedivReferToOther','show');", true);
                    }
                }
            }

        }

        private void LoadCATabDetails()
        {
            string script = string.Empty;

            DataSet ViewExistingForm = new DataSet();
            ViewExistingForm = ExpressFormManager.GetExpressFormData(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));

            
            if (ViewExistingForm.Tables[1].Rows.Count > 0)
            {
                if (ViewExistingForm.Tables[1].Rows[0]["MissedAnyDoses"].ToString() == "True")
                {
                    rdoMissedAnyDosesYes.Checked = true;
                }
                else if (ViewExistingForm.Tables[1].Rows[0]["MissedAnyDoses"].ToString() == "False")
                {
                    rdoMissedAnyDosesNo.Checked = true;
                }
                else
                {
                    rdoMissedAnyDosesYes.Checked = false;
                    rdoMissedAnyDosesNo.Checked = false;
                }

                txtSpecifyWhyDosesMissed.Text = ViewExistingForm.Tables[1].Rows[0]["SpecifyWhyDosesMissed"].ToString();

                if (ViewExistingForm.Tables[1].Rows[0]["DelayedTakingMedication"].ToString() == "True")
                {
                    rdoDelayedTakingMedicationYes.Checked = true;
                }
                else if (ViewExistingForm.Tables[1].Rows[0]["DelayedTakingMedication"].ToString() == "False")
                {
                    rdoDelayedTakingMedicationNo.Checked = true;
                }
                else
                {
                    rdoDelayedTakingMedicationYes.Checked = false;
                    rdoDelayedTakingMedicationNo.Checked = false;
                }

                if (ViewExistingForm.Tables[1].Rows[0]["LabEvaluation"].ToString() == "True")
                {
                    rdoLabEvaluationYes.Checked = true;
                }
                else if (ViewExistingForm.Tables[1].Rows[0]["LabEvaluation"].ToString() == "False")
                {
                    rdoLabEvaluationNo.Checked = true;
                }
                else
                {
                    rdoLabEvaluationYes.Checked = false;
                    rdoLabEvaluationNo.Checked = false;
                }

                this.UserControlKNH_LabEvaluation1.txtlabdiagnosticinput.Text = ViewExistingForm.Tables[1].Rows[0]["LabReviewOtherTests"].ToString();
                ddlOIProphylaxis.SelectedValue = ViewExistingForm.Tables[1].Rows[0]["OIProphylaxis"].ToString();
                ddlCotrimoxazolePrescribed.SelectedValue = ViewExistingForm.Tables[1].Rows[0]["CotrimoxazolePrescribedFor"].ToString();
                ddlFluconazolePrescribedFor.SelectedValue = ViewExistingForm.Tables[1].Rows[0]["FluconazolePrescribedFor"].ToString();
                txtSpecifyOtherOIProphylaxis.Text = ViewExistingForm.Tables[1].Rows[0]["OtherOIProphylaxis"].ToString();
                txtPlan.Text = ViewExistingForm.Tables[1].Rows[0]["Plan"].ToString();
                ddlPwPMessageGiven.SelectedValue = ViewExistingForm.Tables[1].Rows[0]["PwPMessageGiven"].ToString();

                if (ViewExistingForm.Tables[1].Rows[0]["PatientIssuedWithCondoms"].ToString() == "True")
                {
                    rdoIssuedWithCondomsYes.Checked = true;
                }
                else if (ViewExistingForm.Tables[1].Rows[0]["PatientIssuedWithCondoms"].ToString() == "False")
                {
                    rdoIssuedWithCondomsNo.Checked = true;
                }
                else
                {
                    rdoIssuedWithCondomsYes.Checked = false;
                    rdoIssuedWithCondomsNo.Checked = false;
                }

                txtReasonCondomsNotIssued.Text = ViewExistingForm.Tables[1].Rows[0]["ReasonCondomNotIssued"].ToString();

                if (ViewExistingForm.Tables[1].Rows[0]["PregIntBeforeNxtVisit"].ToString() == "True")
                {
                    rdoPregBeforeNextVisitYes.Checked = true;
                }
                else if (ViewExistingForm.Tables[1].Rows[0]["PregIntBeforeNxtVisit"].ToString() == "False")
                {
                    rdoPregBeforeNextVisitNo.Checked = true;
                }
                else
                {
                    rdoPregBeforeNextVisitYes.Checked = false;
                    rdoPregBeforeNextVisitNo.Checked = false;
                }

                if (ViewExistingForm.Tables[1].Rows[0]["DiscussedFertilityOptions"].ToString() == "True")
                {
                    rdoDiscussedFertilityOptionsYes.Checked = true;
                }
                else if (ViewExistingForm.Tables[1].Rows[0]["DiscussedFertilityOptions"].ToString() == "False")
                {
                    rdoDiscussedFertilityOptionsNo.Checked = true;
                }
                else
                {
                    rdoDiscussedFertilityOptionsYes.Checked = false;
                    rdoDiscussedFertilityOptionsNo.Checked = false;
                }

                if (ViewExistingForm.Tables[1].Rows[0]["DiscussedDualContraception"].ToString() == "True")
                {
                    rdoDiscussedDualContraceptionYes.Checked = true;
                }
                else if (ViewExistingForm.Tables[1].Rows[0]["DiscussedDualContraception"].ToString() == "False")
                {
                    rdoDiscussedDualContraceptionNo.Checked = true;
                }
                else
                {
                    rdoDiscussedDualContraceptionYes.Checked = false;
                    rdoDiscussedDualContraceptionNo.Checked = false;
                }

                if (ViewExistingForm.Tables[1].Rows[0]["OtherFPMethod"].ToString() == "True")
                {
                    rdoOtherFPMethodYes.Checked = true;
                }
                else if (ViewExistingForm.Tables[1].Rows[0]["OtherFPMethod"].ToString() == "False")
                {
                    rdoOtherFPMethodNo.Checked = true;
                }
                else
                {
                    rdoOtherFPMethodYes.Checked = false;
                    rdoOtherFPMethodNo.Checked = false;
                }

                if (ViewExistingForm.Tables[1].Rows[0]["ScreenedForCancer"].ToString() == "True")
                {
                    rdoscreenedForCervicalCancerYes.Checked = true;
                }
                else if (ViewExistingForm.Tables[1].Rows[0]["ScreenedForCancer"].ToString() == "False")
                {
                    rdoscreenedForCervicalCancerNo.Checked = true;
                }
                else
                {
                    rdoscreenedForCervicalCancerYes.Checked = false;
                    rdoscreenedForCervicalCancerNo.Checked = false;
                }

                if (ViewExistingForm.Tables[1].Rows[0]["ReferredForCervicalScreening"].ToString() == "True")
                {
                    rdoReferredForCervicalScreeningYes.Checked = true;
                }
                else if (ViewExistingForm.Tables[1].Rows[0]["ReferredForCervicalScreening"].ToString() == "False")
                {
                    rdoReferredForCervicalScreeningNo.Checked = true;
                }
                else
                {
                    rdoReferredForCervicalScreeningYes.Checked = false;
                    rdoReferredForCervicalScreeningNo.Checked = false;
                }

                ddlCaCervixScreeningResults.SelectedValue = ViewExistingForm.Tables[1].Rows[0]["CaCervixScreeningResults"].ToString();
                ddlSpecifyOtherFPMethod.SelectedValue = ViewExistingForm.Tables[1].Rows[0]["SpecifyOtherFPMethod"].ToString();

                txtOtherShortTermSideEffect.Text = ViewExistingForm.Tables[1].Rows[0]["otherShortTermEffects"].ToString();
                txtOtherLongTermSideEffect.Text = ViewExistingForm.Tables[1].Rows[0]["otherLongTermEffects"].ToString();
                
            }
            theDV = new DataView(ViewExistingForm.Tables[3]);
            theDV.RowFilter = "FieldName = 'ShortTermEffects'";
            theUtils.CheckBoxListBindExistingInfoWithAssociatedField(theDV.ToTable(), cblshorttermeffects, "Other Specify", "divotherShortTermSideEffect", "ShowHide('OtherShortTermSideEffect','show');",this);

            theDV = new DataView(ViewExistingForm.Tables[3]);
            theDV.RowFilter = "FieldName = 'LongTermEffects'";
            theUtils.CheckBoxListBindExistingInfoWithAssociatedField(theDV.ToTable(), cbllongtermeffects, "Other specify", "divotherLongTermSideEffect", "ShowHide('OtherLongTermSideEffect','show');", this);

            theDV = new DataView(ViewExistingForm.Tables[3]);
            theDV.RowFilter = "FieldName = 'ARTEligibility'";
            theUtils.CheckBoxListBindExistingInfo(theDV.ToTable(), this.UserControlKNH_Pharmacy1.chklistEligiblethrough);

            theDV = new DataView(ViewExistingForm.Tables[3]);
            theDV.RowFilter = "FieldName = 'ARTchangecode'";
            theUtils.CheckBoxListBindExistingInfo(theDV.ToTable(), this.UserControlKNH_Pharmacy1.chklistARTchangecode);

            theDV = new DataView(ViewExistingForm.Tables[3]);
            theDV.RowFilter = "FieldName = 'ARTstopcode'";
            theUtils.CheckBoxListBindExistingInfo(theDV.ToTable(), this.UserControlKNH_Pharmacy1.chklistARTstopcode);

            if (ViewExistingForm.Tables[4].Rows.Count>0)
                this.UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedValue = ViewExistingForm.Tables[4].Rows[0]["TherapyPlan"].ToString();
        }

        protected Hashtable TriageHT()
        {

            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Convert.ToInt32(Session["PatientId"]));
                theHT.Add("visitID", Convert.ToInt32(Session["PatientVisitId"]));
                theHT.Add("locationID", Convert.ToInt32(Session["AppLocationID"]));
                theHT.Add("userID",Convert.ToInt32(Session["AppUserId"]));
                theHT.Add("ptnAccByCareGiver",rdoPtnAccByCareGiverYes.Checked ? "1" : rdoPtnAccByCareGiverNo.Checked ? "0" : "");
                theHT.Add("careGiverRelationship",Convert.ToInt32(ddlCareGiverRelationship.SelectedValue));
                theHT.Add("temp", this.UserControlKNH_VitalSigns1.txtTemp.Text == "" ? 0 : Convert.ToDecimal(this.UserControlKNH_VitalSigns1.txtTemp.Text));
                theHT.Add("rr", this.UserControlKNH_VitalSigns1.txtRR.Text == "" ? 0 : Convert.ToDecimal(this.UserControlKNH_VitalSigns1.txtRR.Text));
                theHT.Add("hr", this.UserControlKNH_VitalSigns1.txtHR.Text == "" ? 0 : Convert.ToDecimal(this.UserControlKNH_VitalSigns1.txtHR.Text));
                theHT.Add("BPSystolic", this.UserControlKNH_VitalSigns1.txtBPSystolic.Text == "" ? 0 : Convert.ToDecimal(this.UserControlKNH_VitalSigns1.txtBPSystolic.Text));
                theHT.Add("BPDiastolic", this.UserControlKNH_VitalSigns1.txtBPDiastolic.Text == "" ? 0 : Convert.ToDecimal(this.UserControlKNH_VitalSigns1.txtBPDiastolic.Text));
                theHT.Add("height", this.UserControlKNH_VitalSigns1.txtHeight.Text == "" ? 0 : Convert.ToDecimal(this.UserControlKNH_VitalSigns1.txtHeight.Text));
                theHT.Add("weight", this.UserControlKNH_VitalSigns1.txtWeight.Text == "" ? 0 : Convert.ToDecimal(this.UserControlKNH_VitalSigns1.txtWeight.Text));
                theHT.Add("OtherMedicalCondition",txtSpecifyOthermedicalCondition.Text);
                theHT.Add("areYouOnFollowUp",rdoOnFollowUpYes.Checked ? "1" : rdoOnFollowUpNo.Checked ? "0" : "");
                theHT.Add("lastFollowUp",txtLastFollowUp.Text);
                theHT.Add("visitDate",Convert.ToDateTime(txtVisitDate.Value));
                theHT.Add("startTime",startTime);
                theHT.Add("NurseComments", this.UserControlKNH_VitalSigns1.txtnursescomments.Text);
                theHT.Add("SpecilistReferral", this.UserControlKNH_VitalSigns1.txtReferToSpecialistClinic.Text);
                theHT.Add("OtherReferral", this.UserControlKNH_VitalSigns1.txtSpecifyOtherRefferedTo.Text);

            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;

        }
        
        protected Hashtable CAHT()
        {

            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Convert.ToInt32(Session["PatientId"]));
                theHT.Add("visitID", Convert.ToInt32(Session["PatientVisitId"]));
                theHT.Add("locationID", Convert.ToInt32(Session["AppLocationID"]));
                theHT.Add("userID",Convert.ToInt32(Session["AppUserId"]));
                theHT.Add("missedAnyDoses", rdoMissedAnyDosesYes.Checked ? "1": rdoMissedAnyDosesNo.Checked ? "0" : "");
                theHT.Add("specifyWhyDosesMissed",txtSpecifyWhyDosesMissed.Text);
                theHT.Add("delayedTakingMedication", rdoDelayedTakingMedicationYes.Checked ? "1" : rdoDelayedTakingMedicationNo.Checked ? "0" : "");
                theHT.Add("labEvaluation",rdoLabEvaluationYes.Checked ? "1" : rdoLabEvaluationNo.Checked ? "0" : "");
                theHT.Add("txtLabReviewOtherTests", this.UserControlKNH_LabEvaluation1.txtlabdiagnosticinput.Text);
                theHT.Add("OIProphylaxis",Convert.ToInt32(ddlOIProphylaxis.SelectedValue));
                theHT.Add("cotrimoxazolePrescribed",Convert.ToInt32(ddlCotrimoxazolePrescribed.SelectedValue));
                theHT.Add("FluconazolePrescribed", Convert.ToInt32(ddlFluconazolePrescribedFor.SelectedValue));
                theHT.Add("specifyOtherOIProphylaxis",txtSpecifyOtherOIProphylaxis.Text);
                theHT.Add("plan",txtPlan.Text);
                theHT.Add("PwPMessageGiven",Convert.ToInt32(ddlPwPMessageGiven.SelectedValue));
                theHT.Add("issuedWithCondoms", rdoIssuedWithCondomsYes.Checked ? "1" : rdoIssuedWithCondomsNo.Checked ? "0" : "");
                theHT.Add("reasonCondomsNotIssued",txtReasonCondomsNotIssued.Text);
                theHT.Add("pregIntBeforeNxtVist", rdoPregBeforeNextVisitYes.Checked ? "1" : rdoPregBeforeNextVisitNo.Checked ? "0" : "");
                theHT.Add("fertilityOptions", rdoDiscussedFertilityOptionsYes.Checked ? "1" : rdoDiscussedFertilityOptionsNo.Checked ? "0" : "");
                theHT.Add("dualContraception", rdoDiscussedDualContraceptionYes.Checked ? "1" : rdoDiscussedDualContraceptionNo.Checked ? "0" : "");
                theHT.Add("otherFPMethod", rdoOtherFPMethodYes.Checked ? "1" : rdoOtherFPMethodNo.Checked ? "0" : "");
                theHT.Add("specifyOtherFPMethod",Convert.ToInt32(ddlSpecifyOtherFPMethod.SelectedValue));
                theHT.Add("screenedForCancer",rdoscreenedForCervicalCancerYes.Checked ? "1" : rdoscreenedForCervicalCancerNo.Checked ? "0" : "");
                theHT.Add("caCervixScreeningResults",Convert.ToInt32(ddlCaCervixScreeningResults.SelectedValue));
                theHT.Add("referredForCervicalScreening",rdoReferredForCervicalScreeningYes.Checked ? "1" : rdoReferredForCervicalScreeningNo.Checked ? "0" : "");
                theHT.Add("startTime",startTime);

                theHT.Add("treatmentPlan", this.UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedValue);
                theHT.Add("Noofdrugssubstituted", this.UserControlKNH_Pharmacy1.txtNoofdrugssubstituted.Text);
                theHT.Add("reasonforswitchto2ndlineregimen", this.UserControlKNH_Pharmacy1.ddlReasonforswitchto2ndlineregimen.SelectedValue);
                theHT.Add("specifyOtherEligibility", this.UserControlKNH_Pharmacy1.txtSpecifyOtherEligibility.Text);
                theHT.Add("specifyotherARTchangereason", this.UserControlKNH_Pharmacy1.txtSpecifyotherARTchangereason.Text);
                theHT.Add("specifyOtherStopCode", this.UserControlKNH_Pharmacy1.txtSpecifyOtherStopCode.Text);

                theHT.Add("OtherShortTermSideEffect", txtOtherShortTermSideEffect.Text);
                theHT.Add("OtherLongTermSideEffect", txtOtherLongTermSideEffect.Text);
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;

        }

        protected void btnCASave_Click(object sender, EventArgs e)
        {
            //save clinical assessmenttab
            if (checkRequiredFieldsCA() == true)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "Triage Information has not been entered. Please fill in Triage information first.";
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CANoTriageInfo", "alert('Triage Information has not been entered. Please fill in Triage information first.');", true);
                }
                else
                {
                    try
                    {
                        Hashtable HT = CAHT();
                        ExpressFormManager.SaveUpdateExpressFormClinicalAssessmentTab(HT, getCheckBoxListItemValues(cblshorttermeffects), getCheckBoxListItemValues(cbllongtermeffects), getCheckBoxListItemValues(this.UserControlKNH_Pharmacy1.chklistEligiblethrough), getCheckBoxListItemValues(this.UserControlKNH_Pharmacy1.chklistARTchangecode), getCheckBoxListItemValues(this.UserControlKNH_Pharmacy1.chklistARTstopcode));
                        SaveCancel("Clinical Assessment");
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CASaveUpdate", "alert('Data on Clinical Assessment tab saved successfully.');", true);
                        startTime = DateTime.Now;
                        checkIfPreviuosTabSaved();
                        tabControl.ActiveTabIndex = 2;
                    }
                    catch (Exception ex)
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["MessageText"] = "Error Occurred :" + ex.Message + ".";
                        IQCareMsgBox.Show("#C1", theBuilder, this);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CASaveUpdateError", "alert('Error Occurred :'" + ex.Message + "'.');", true);
                    }
                }
            }
            
        }

        protected void rdoMissedAnyDosesYes_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoMissedAnyDosesYes.Checked)
            //{
            //    missedAnyDoses = 1;
            //}
        }

        protected void rdoMissedAnyDosesNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoMissedAnyDosesNo.Checked)
            //{
            //    missedAnyDoses = 0;
            //}
        }

        protected void rdoDelayedTakingMedicationYes_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoDelayedTakingMedicationYes.Checked)
            //    delayedTakingMedication = 1;
        }

        protected void rdoDelayedTakingMedicationNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoDelayedTakingMedicationNo.Checked)
            //    delayedTakingMedication = 0;
        }

        protected void rdoLabEvaluationYes_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoLabEvaluationYes.Checked)
            //    labEvaluation = 1;
        }

        protected void rdoLabEvaluationNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoLabEvaluationNo.Checked)
            //    labEvaluation = 0;
        }

        protected void rdoIssuedWithCondomsYes_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoIssuedWithCondomsYes.Checked)
            //    issuedWithCondoms = 1;
        }

        protected void rdoIssuedWithCondomsNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoIssuedWithCondomsNo.Checked)
            //    issuedWithCondoms = 0;
        }

        protected void rdoPregBeforeNextVisitYes_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoPregBeforeNextVisitYes.Checked)
            //    pregIntBeforeNxtVist = 1;
        }

        protected void rdoPregBeforeNextVisitNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoPregBeforeNextVisitNo.Checked)
            //    pregIntBeforeNxtVist = 0;
        }

        protected void rdoOtherFPMethodYes_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoOtherFPMethodYes.Checked)
            //    otherFPMethod = 1;
        }

        protected void rdoOtherFPMethodNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoOtherFPMethodNo.Checked)
            //    otherFPMethod = 0;
        }

        protected void rdoscreenedForCervicalCancerYes_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoscreenedForCervicalCancerYes.Checked)
            //    screenedForCancer = 1;
        }

        protected void rdoscreenedForCervicalCancerNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoscreenedForCervicalCancerNo.Checked)
            //    screenedForCancer = 0;
        }

        protected void rdoDiscussedFertilityOptionsYes_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoDiscussedFertilityOptionsYes.Checked)
            //    fertilityOptions = 1;
        }

        protected void rdoDiscussedFertilityOptionsNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoDiscussedFertilityOptionsNo.Checked)
            //    fertilityOptions = 0;
        }

        protected void rdoReferredForCervicalScreeningYes_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoReferredForCervicalScreeningYes.Checked)
            //    referredForCervicalScreening = 1;
        }

        protected void rdoReferredForCervicalScreeningNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoReferredForCervicalScreeningNo.Checked)
            //    referredForCervicalScreening = 0;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IQCareUtils.Redirect("../Laboratory/LabOrderForm.aspx", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
        }

        protected void btnCAClose_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            IQCareUtils.Redirect("../Pharmacy/frmPharmacyform.aspx?opento=ArtForm&LastRegimenDispensed=True", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Delete")
            {
                int delete = theUtils.DeleteForm("Express",Convert.ToInt32(Session["PatientVisitId"]),Convert.ToInt32(Session["PatientId"]),Convert.ToInt32(Session["AppUserId"]));

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
        }
	}
}