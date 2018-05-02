using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Text;
//IQCare Libs
using Application.Presentation;
using Application.Common;
using Interface.Clinical;
//Third party Libs
using Telerik.Web.UI;
using System.IO;
using System.Drawing;
using PresentationApp.ClinicalForms.UserControl;


namespace PresentationApp.ClinicalForms
{

    public partial class frmClinical_KNH_AdultIE : LogPage
    {
        static Boolean isError = false;
        static DateTime startTime;
        // Delegate declaration
        public delegate void OnButtonClick();
        // Event declaration
        public event OnButtonClick btnHandler;
        IKNHAdultIE theExpressManager;
        int AIETriageTabID, AIECATabID, AIETBTabID, AIEExamTabID, AIEManageTabID, AIEPwPTabID;

        IQCareUtils theUtils = new IQCareUtils();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx", true);
            }
            //addAttributes();
            getTabIds();

            if (!IsPostBack == true)
            {
                GetPatientVisitIdAdultIE();
                Form_Init();
                BindAttributes();
                ShowHideBusinessRules();
                checkIfPreviuosTabSaved();
                BindCheckUncheklogic(cblDiagnosis);

                //GetDataValue();
                //Session["FormName"] = "Adult Initial Evaluation Form";
            }
            //AjaxControlToolkit.TabContainer a = new AjaxControlToolkit.TabContainer();
            //a = tabControl;
            //a.ActiveTabChanged += new EventHandler(a_ActiveTabChanged);
            if (Convert.ToString(Session["TechnicalAreaId"]) == "210")
            { GblIQCare.FormId = 275; }
            else { GblIQCare.FormId = 177; }


            FemaleControls();
            UcTBScreening.btnHandler += new UserControlKNH_TBScreening.OnButtonClick(tabControl_Handler);

            if (Request.QueryString["name"] == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showDeletebutton", "ShowHide('tblDeleteButton','show');", true);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "hideSavebutton", "ShowHide('tblSaveButton','hide');", true);
            }
        }

        private void addAttributes()
        {
            txtVisitDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
        }
        void tabControl_Handler()
        {
            checkIfPreviuosTabSaved();
            tabControl.ActiveTabIndex = 3;
        }

        public void FemaleControls()
        {
            if (Session["PatientSex"].ToString() == "Male")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "trLMP", "ShowHide('trLMP','hide');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "trPregnancyTest", "ShowHide('trPregnancyTest','hide');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "trIfYesPregnant", "ShowHide('trIfYesPregnant','hide');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "trIntendPreg", "ShowHide('trIntendPreg','hide');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "trCervicalCancer", "ShowHide('trCervicalCancer','hide');", true);
            }
        }
        private void visibleDiv(String divId)
        {
            String script = "";
            script = "<script language = 'javascript' defer ='defer' id = '" + divId + "'>\n";
            script += "ShowHide('" + divId + "','show');\n";
            script += "</script>\n";
            RegisterStartupScript("'" + divId + "'", script);
        }
        public void ShowHide()
        {
            string script = string.Empty;
            if (rblDiagnosisYesNo.SelectedValue == "1")
            {
                visibleDiv("DIVHIVDiagnosis");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "DIVHIVDiagnosis", "ShowHide('DIVHIVDiagnosis','show')", true);
            }
            if (rblChildAccompaniedBy.SelectedValue == "1")
            {
                visibleDiv("divTreatmentSupporterRelationship");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divTreatmentSupporterRelationship", "ShowHide('divTreatmentSupporterRelationship','show')", true);
            }
            if (ddlDisclosureStatus.SelectedItem.Text == "Not ready")
            {
                visibleDiv("divSpecifyreason");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divSpecifyreason", "ShowHide('divSpecifyreason','show')", true);

            }
            if (ddreasondisclosed.SelectedItem.Text == "Other")
            {
                visibleDiv("divSpecifyotherdisclosurestatus");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divSpecifyotherdisclosurestatus", "ShowHide('divSpecifyotherdisclosurestatus','show')", true);
            }
            if (ddlPatientReferred.SelectedItem.Text == "Other")
            {
                visibleDiv("divPatRefother");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divPatRefother", "ShowHide('divPatRefother','show')", true);
            }
            if (ddlSchoolingStatus.SelectedItem.Text == "Enrolled")
            {
                visibleDiv("divHighestLevelattained");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divHighestLevelattained", "ShowHide('divHighestLevelattained','show')", true);
            }
            if (rblHIVSupportgroup.SelectedValue == "1")
            {
                visibleDiv("DIVsupportgroup");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "DIVsupportgroup", "ShowHide('DIVsupportgroup','show')", true);
            }
            for (int i = 0; i < this.UcPc.gvPresentingComplaints.Rows.Count; i++)
            {
                CheckBox chkPComplaints = (CheckBox)UcPc.gvPresentingComplaints.Rows[i].FindControl("ChkPresenting");
                if (chkPComplaints.Text == "Other" && chkPComplaints.Checked == true)
                {
                    visibleDiv("DivOther");
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "DivOther", "ShowHide('DivOther','show')", true);
                }
            }
            if (rblFP.SelectedValue == "1")
            {
                visibleDiv("hideFP");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideFP", "ShowHide('hideFP','show')", true);
            }

            for (int i = 0; i < chkLTMedications.Items.Count; i++)
            {
                if (chkLTMedications.Items[i].Text == "Other" && chkLTMedications.Items[i].Selected == true)
                {
                    visibleDiv("hideOtherLTM");
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideOtherLTM", "ShowHide('hideOtherLTM','show')", true);
                }
            }

            if (rblARVdosesmissed.SelectedValue == "1")
            {
                visibleDiv("DIVStopINHDate");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "DIVStopINHDate", "ShowHide('DIVStopINHDate','show')", true);
            }
            if (UcWhostaging.radbtnMernarcheyes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'DIVMenarcheDateU'>\n";
                script += "ShowHide('divmenarchedatehshowhide','show');\n";
                script += "</script>\n";
                RegisterStartupScript("DIVMenarcheDateU", script);
            }

            for (int i = 0; i < cblShortTermEffects.Items.Count; i++)
            {
                if (cblShortTermEffects.Items[i].Text == "Other Specify" && cblShortTermEffects.Items[i].Selected == true)
                {
                    visibleDiv("divshorttermeffecttxt");
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divshorttermeffecttxt", "ShowHide('divshorttermeffecttxt','show')", true);
                }
            }
            for (int i = 0; i < cblLongTermEffects.Items.Count; i++)
            {
                if (cblLongTermEffects.Items[i].Text == "Other specify" && cblLongTermEffects.Items[i].Selected == true)
                {
                    visibleDiv("divlongtermeffecttxt");
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divlongtermeffecttxt", "ShowHide('divlongtermeffecttxt','show')", true);
                }
            }
            ShowChkboxlistDiv(UcPE.cblGeneralConditions, "Other", "divgeneralothercondition");
            ShowChkboxlistDiv(UcPE.cblCardiovascularConditions, "Other", "divOtherCardiovascularConditions");
            ShowChkboxlistDiv(UcPE.cblOralCavityConditions, "Other", "divOtherOralCavityConditions");
            ShowChkboxlistDiv(UcPE.cblGenitalUrinaryConditions, "Other", "divOtherGenitourinaryConditions");
            ShowChkboxlistDiv(UcPE.cblCNSConditions, "Other", "divOtherCNSConditions");
            ShowChkboxlistDiv(UcPE.cblChestLungsConditions, "Other", "divOtherChestLungsConditions");
            ShowChkboxlistDiv(UcPE.cblSkinConditions, "Other", "divOtherSkinConditions");
            ShowChkboxlistDiv(UcPE.cblAbdomenConditions, "Other", "divOtherAbdomenConditions");

        }
        public void ShowChkboxlistDiv(CheckBoxList Chklst, string itemname, string divname)
        {
            // linq query to find selected items from checkboxlist  
            var items = from ListItem li in Chklst.Items
                        where li.Selected == true && li.Text == itemname
                        select li;
            if (items.ToList().Count > 0)
            {
                visibleDiv(divname);
            }
        }
        public void ShowHideUpdate()
        {
            string script = string.Empty;
            if (rblDiagnosisYesNo.SelectedValue == "1")
            {
                visibleDiv("DIVHIVDiagnosis");

            }
            if (rblChildAccompaniedBy.SelectedValue == "1")
            {
                visibleDiv("divTreatmentSupporterRelationship");

            }
            if (ddlDisclosureStatus.SelectedItem.Text == "Not ready")
            {
                visibleDiv("divSpecifyreason");

                if (ddreasondisclosed.SelectedItem.Text == "Other")
                {
                    visibleDiv("divSpecifyotherdisclosurestatus");

                }
            }
            if (ddlSchoolingStatus.SelectedItem.Text == "Enrolled")
            {
                visibleDiv("divHighestLevelattained");

            }
            if (rblHIVSupportgroup.SelectedValue == "1")
            {
                visibleDiv("DIVsupportgroup");

            }
            for (int i = 0; i < this.UcPc.gvPresentingComplaints.Rows.Count; i++)
            {
                CheckBox chkPComplaints = (CheckBox)UcPc.gvPresentingComplaints.Rows[i].FindControl("ChkPresenting");
                if (chkPComplaints.Text == "Other" && chkPComplaints.Checked == true)
                {
                    visibleDiv("DivOther");

                }
            }
            if (rblFP.SelectedValue == "1")
            {
                visibleDiv("hideFP");

            }

            for (int i = 0; i < chkLTMedications.Items.Count; i++)
            {
                if (chkLTMedications.Items[i].Text == "Other" && chkLTMedications.Items[i].Selected == true)
                {
                    visibleDiv("hideOtherLTM");

                }
            }

            if (rblARVdosesmissed.SelectedValue == "1")
            {
                visibleDiv("DIVStopINHDate");

            }

            if (UcWhostaging.radbtnMernarcheyes.Checked)
            {
                visibleDiv("divmenarchedatehshowhide");

            }

            for (int i = 0; i < cblShortTermEffects.Items.Count; i++)
            {
                if (cblShortTermEffects.Items[i].Text == "Other Specify" && cblShortTermEffects.Items[i].Selected == true)
                {
                    visibleDiv("divshorttermeffecttxt");

                }
            }
            for (int i = 0; i < cblLongTermEffects.Items.Count; i++)
            {
                if (cblLongTermEffects.Items[i].Text == "Other specify" && cblLongTermEffects.Items[i].Selected == true)
                {
                    visibleDiv("divlongtermeffecttxt");

                }
            }
            if (UCPharmacy.ddlTreatmentplan.SelectedItem.Text == "Start new treatment (naive patient)")
            {
                visibleDiv("divEligiblethrough");
            }
            if (UCPharmacy.ddlTreatmentplan.SelectedItem.Text == "Change regimen")
            {
                visibleDiv("divARTchangecode");
            }
            if (UCPharmacy.ddlTreatmentplan.SelectedItem.Text == "Switch to second line")
            {
                visibleDiv("divReasonforswitchto2ndlineregimen");
            }
            if (UCPharmacy.ddlTreatmentplan.SelectedItem.Text == "Stop treatment")
            {
                visibleDiv("divARTstopcode");
            }

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    GetDataValue();
                }
                else
                    txtVisitDate.Value = Application["AppCurrentDate"].ToString();
            }

            //john
            IPatientKNHPEP KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            DataTable dtSignature = KNHPEP.GetSignature(177, Convert.ToInt32(Session["PatientVisitId"]));
            foreach (DataRow dr in dtSignature.Rows)
            {
                if (dr["TabName"].ToString() == "AdultIETriage")
                    this.UserControlKNH_SignatureTriage.lblSignature.Text = dr["UserName"].ToString();
                if (dr["TabName"].ToString() == "AdultIEClinicalHistory")
                    this.UserControlKNH_SignatureClinical.lblSignature.Text = dr["UserName"].ToString();
                if (dr["TabName"].ToString() == "AdultIEExamination")
                    this.UserControlKNH_SignatureExam.lblSignature.Text = dr["UserName"].ToString();
                if (dr["TabName"].ToString() == "AdultIEManagement")
                    this.UserControlKNH_SignatureMgt.lblSignature.Text = dr["UserName"].ToString();
            }
            //
        }
        private void BindListData(DataTable theDT)
        {
            foreach (DataRow theDR in theDT.Rows)
            {
                if (Convert.ToString(theDR["FieldName"]) == "PresentingComplaints")
                {
                    for (int i = 0; i < this.UcPc.gvPresentingComplaints.Rows.Count; i++)
                    {
                        Label lblPComplaintsId = (Label)UcPc.gvPresentingComplaints.Rows[i].FindControl("lblPresenting");
                        CheckBox chkPComplaints = (CheckBox)UcPc.gvPresentingComplaints.Rows[i].FindControl("ChkPresenting");
                        TextBox txtPComplaints = (TextBox)UcPc.gvPresentingComplaints.Rows[i].FindControl("txtPresenting");
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblPComplaintsId.Text))
                        {
                            chkPComplaints.Checked = true;
                            txtPComplaints.Text = theDR["NumericField"].ToString();
                            if (theDR["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("DivOther");
                            }

                            if (theDR["Name"].ToString().ToLower() == "none")
                            {
                                String script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'togglePresComp'>\n";
                                script += "togglePC('" + chkPComplaints.ClientID + "');\n";
                                script += "</script>\n";
                                RegisterStartupScript("'togglePresComp'", script);
                            }
                        }

                    }
                }
                else if (Convert.ToString(theDR["FieldName"]) == "LongTermMedications")
                {
                    for (int i = 0; i < chkLTMedications.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(chkLTMedications.Items[i].Value))
                        {
                            chkLTMedications.Items[i].Selected = true;
                        }

                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "GeneralConditions")
                {
                    for (int i = 0; i < this.UcPE.cblGeneralConditions.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(this.UcPE.cblGeneralConditions.Items[i].Value))
                        {
                            this.UcPE.cblGeneralConditions.Items[i].Selected = true;
                            if (theDR["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("divgeneralothercondition");

                            }
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "CardiovascularConditions")
                {
                    for (int i = 0; i < this.UcPE.cblCardiovascularConditions.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(this.UcPE.cblCardiovascularConditions.Items[i].Value))
                        {
                            this.UcPE.cblCardiovascularConditions.Items[i].Selected = true;
                            if (theDR["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("divOtherCardiovascularConditions");

                            }
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "OralCavityConditions")
                {
                    for (int i = 0; i < this.UcPE.cblOralCavityConditions.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(this.UcPE.cblOralCavityConditions.Items[i].Value))
                        {
                            this.UcPE.cblOralCavityConditions.Items[i].Selected = true;
                            if (theDR["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("divOtherOralCavityConditions");

                            }
                        }
                    }

                }
                else if (Convert.ToString(theDR["FieldName"]) == "GenitalUrinaryConditions")
                {
                    for (int i = 0; i < this.UcPE.cblGenitalUrinaryConditions.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(this.UcPE.cblGenitalUrinaryConditions.Items[i].Value))
                        {
                            this.UcPE.cblGenitalUrinaryConditions.Items[i].Selected = true;
                            if (theDR["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("divOtherGenitourinaryConditions");

                            }
                        }
                    }

                }
                else if (Convert.ToString(theDR["FieldName"]) == "CNSConditions")
                {
                    for (int i = 0; i < this.UcPE.cblCNSConditions.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(this.UcPE.cblCNSConditions.Items[i].Value))
                        {
                            this.UcPE.cblCNSConditions.Items[i].Selected = true;
                            if (theDR["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("divOtherCNSConditions");

                            }
                        }
                    }
                }


                else if (Convert.ToString(theDR["FieldName"]) == "ChestLungsConditions")
                {
                    for (int i = 0; i < this.UcPE.cblChestLungsConditions.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(this.UcPE.cblChestLungsConditions.Items[i].Value))
                        {
                            this.UcPE.cblChestLungsConditions.Items[i].Selected = true;
                            if (theDR["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("divOtherChestLungsConditions");

                            }
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "SkinConditions")
                {
                    for (int i = 0; i < this.UcPE.cblSkinConditions.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(this.UcPE.cblSkinConditions.Items[i].Value))
                        {
                            this.UcPE.cblSkinConditions.Items[i].Selected = true;
                            if (theDR["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("divOtherSkinConditions");

                            }
                        }
                    }
                }
                else if (Convert.ToString(theDR["FieldName"]) == "AbdomenConditions")
                {
                    for (int i = 0; i < this.UcPE.cblAbdomenConditions.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(this.UcPE.cblAbdomenConditions.Items[i].Value))
                        {
                            this.UcPE.cblAbdomenConditions.Items[i].Selected = true;
                            if (theDR["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("divOtherAbdomenConditions");

                            }
                        }
                    }
                }
                else if (Convert.ToString(theDR["FieldName"]) == "Diagnosis")
                {
                    for (int i = 0; i < cblDiagnosis.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(cblDiagnosis.Items[i].Value))
                        {
                            cblDiagnosis.Items[i].Selected = true;
                            if (theDR["Name"].ToString().ToLower() == "hiv-related illness")
                            {
                                visibleDiv("DIVHIVrelatedOI");

                            }
                            else if (theDR["Name"].ToString().ToLower() == "non-hiv related illness")
                            {
                                visibleDiv("DIVNonHIVrelatedOI");

                            }
                        }
                    }
                }
                else if (Convert.ToString(theDR["FieldName"]) == "WHOStageIConditions")
                {
                    for (int i = 0; i < this.UcWhostaging.gvWHO1.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UcWhostaging.gvWHO1.Rows[i].FindControl("lblwho1");
                        CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO1.Rows[i].FindControl("Chkwho1");
                        HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date");
                        HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date1");
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", theDR["DateField1"]);
                            txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", theDR["DateField2"]);
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "WHOStageIIConditions")
                {
                    for (int i = 0; i < this.UcWhostaging.gvWHO2.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UcWhostaging.gvWHO2.Rows[i].FindControl("lblwho2");
                        CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO2.Rows[i].FindControl("Chkwho2");
                        HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date");
                        HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date1");
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", theDR["DateField1"]);
                            txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", theDR["DateField2"]);
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "WHOStageIIICoditions")
                {
                    for (int i = 0; i < this.UcWhostaging.gvWHO3.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UcWhostaging.gvWHO3.Rows[i].FindControl("lblwho3");
                        CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO3.Rows[i].FindControl("Chkwho3");
                        HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date");
                        HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date1");
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", theDR["DateField1"]);
                            txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", theDR["DateField2"]);
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "WHOStageIVConditions")
                {
                    for (int i = 0; i < this.UcWhostaging.gvWHO4.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UcWhostaging.gvWHO4.Rows[i].FindControl("lblwho4");
                        CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO4.Rows[i].FindControl("Chkwho4");
                        HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date");
                        HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date1");
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", theDR["DateField1"]);
                            txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", theDR["DateField2"]);
                        }
                    }
                }


                else if (Convert.ToString(theDR["FieldName"]) == "RefferedToFUpF")
                {
                    for (int i = 0; i < this.idVitalSign.cblReferredTo.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(this.idVitalSign.cblReferredTo.Items[i].Value))
                        {
                            if (theDR["Name"].ToString().ToLower() == "other specialist clinic")
                            {
                                visibleDiv("TriagedivReferToSpecialistClinic");
                            }
                            if (theDR["Name"].ToString().ToLower() == "other (specify)")
                            {
                                visibleDiv("TriagedivReferToOther");
                                idVitalSign.txtSpecifyOtherRefferedTo.Text = theDR["Other_notes"].ToString();
                            }
                            this.idVitalSign.cblReferredTo.Items[i].Selected = true;

                        }

                    }
                }
                else if (Convert.ToString(theDR["FieldName"]) == "ShortTermEffects")
                {
                    for (int i = 0; i < cblShortTermEffects.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(cblShortTermEffects.Items[i].Value))
                        {
                            cblShortTermEffects.Items[i].Selected = true;
                        }
                    }
                }
                else if (Convert.ToString(theDR["FieldName"]) == "LongTermEffects")
                {
                    for (int i = 0; i < cblLongTermEffects.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(cblLongTermEffects.Items[i].Value))
                        {
                            cblLongTermEffects.Items[i].Selected = true;
                        }
                    }
                }


                else if (Convert.ToString(theDR["FieldName"]) == "ARTchangecode")
                {
                    for (int i = 0; i < UCPharmacy.chklistARTchangecode.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(UCPharmacy.chklistARTchangecode.Items[i].Value))
                        {
                            UCPharmacy.chklistARTchangecode.Items[i].Selected = true;
                        }
                    }
                }
                else if (Convert.ToString(theDR["FieldName"]) == "ARTEligibility")
                {
                    for (int i = 0; i < UCPharmacy.chklistEligiblethrough.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(UCPharmacy.chklistEligiblethrough.Items[i].Value))
                        {
                            UCPharmacy.chklistEligiblethrough.Items[i].Selected = true;
                        }
                    }
                }
                else if (Convert.ToString(theDR["FieldName"]) == "ARTstopcode")
                {
                    for (int i = 0; i < UCPharmacy.chklistARTstopcode.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(UCPharmacy.chklistARTstopcode.Items[i].Value))
                        {
                            UCPharmacy.chklistARTstopcode.Items[i].Selected = true;
                        }
                    }
                }
                else if (Convert.ToString(theDR["FieldName"]) == "SpecificMedicalCondition")
                {
                    for (int i = 0; i < cblPreExistingMedConditions.Items.Count; i++)
                    {
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(cblPreExistingMedConditions.Items[i].Value))
                        {
                            cblPreExistingMedConditions.Items[i].Selected = true;
                            if (theDR["Name"].ToString().ToLower() == "other medical conditions")
                            {
                                visibleDiv("divothermedcondition");
                                txtOthermedicalconditions.Text = theDR["Other_notes"].ToString();
                            }
                        }
                    }
                }

            }

        }
        private void GetPatientVisitIdAdultIE()
        {
            IKNHAdultIE theExpressManager;
            theExpressManager = (IKNHAdultIE)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAdultIE, BusinessProcess.Clinical");
            DataTable dtVisitId = new DataTable();
            if (Convert.ToString(Session["TechnicalAreaId"]) == "210")
            {
                dtVisitId = theExpressManager.GetPatientVisitIdAdultIE(Convert.ToInt32(Session["PatientId"].ToString()), 38);
            }
            else { dtVisitId = theExpressManager.GetPatientVisitIdAdultIE(Convert.ToInt32(Session["PatientId"].ToString()), 25); }
            if (dtVisitId.Rows.Count > 0)
            {
                Session["PatientVisitId"] = dtVisitId.Rows[0]["Visit_Id"];
            }
            else
                Session["PatientVisitId"] = 0;

        }
        private void GetDataValue()
        {

            //IPatientKNHPEP KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            //DataTable dtSignature = KNHPEP.GetSignature(177, Convert.ToInt32(Session["PatientVisitId"]));
            //foreach (DataRow dr in dtSignature.Rows)
            //{
            //    if (dr["TabName"].ToString() == "AdultIETriage")
            //        this.UserControlKNH_SignatureTriage.lblSignature.Text = dr["UserName"].ToString();
            //    if (dr["TabName"].ToString() == "AdultIEClinicalHistory")
            //        this.UserControlKNH_SignatureClinical.lblSignature.Text = dr["UserName"].ToString();
            //    if (dr["TabName"].ToString() == "AdultIEExamination")
            //        this.UserControlKNH_SignatureExam.lblSignature.Text = dr["UserName"].ToString();
            //    if (dr["TabName"].ToString() == "AdultIEManagement")
            //        this.UserControlKNH_SignatureMgt.lblSignature.Text = dr["UserName"].ToString();
            //}


            List<BIQAdultIE> list = new List<BIQAdultIE>();
            BIQAdultIE objAdultIE = new BIQAdultIE();
            objAdultIE.PtnPk = Convert.ToInt32(Session["PatientId"]);
            objAdultIE.VisitPk = Convert.ToInt32(Session["PatientVisitId"]);
            objAdultIE.LocationId = Convert.ToInt32(Session["AppLocationId"]);
            list.Add(objAdultIE);
            theExpressManager = (IKNHAdultIE)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAdultIE, BusinessProcess.Clinical");
            DataSet theDSData = theExpressManager.GetKnhAdultIEFormData(objAdultIE);
            if (theDSData.Tables[0].Rows.Count > 0)
            {
                txtVisitDate.Value = String.Format("{0:dd-MMM-yyyy}", theDSData.Tables[0].Rows[0]["VisitDate"]);
                if (theDSData.Tables[1].Rows.Count > 0)
                {
                    if (theDSData.Tables[1].Rows[0]["DiagnosisConfirmed"] != DBNull.Value)
                        if (theDSData.Tables[1].Rows[0]["DiagnosisConfirmed"].ToString() == "True")
                            rblDiagnosisYesNo.SelectedValue = "1";
                        else if (theDSData.Tables[1].Rows[0]["DiagnosisConfirmed"].ToString() == "False")
                            rblDiagnosisYesNo.SelectedValue = "0";
                    if (theDSData.Tables[1].Rows[0]["ConfirmHIVPosDate"] != DBNull.Value)
                        txtdtConfirmHIVPosDate.Value = String.Format("{0:dd-MMM-yyyy}", theDSData.Tables[1].Rows[0]["ConfirmHIVPosDate"]);

                    if (theDSData.Tables[1].Rows[0]["ChildAccompaniedByCaregiver"] != DBNull.Value)
                    {
                        if (theDSData.Tables[1].Rows[0]["ChildAccompaniedByCaregiver"].ToString() == "True")
                            this.rblChildAccompaniedBy.SelectedValue = "1";
                        else
                            this.rblChildAccompaniedBy.SelectedValue = "0";
                    }
                    if (theDSData.Tables[1].Rows[0]["TreatmentSupporterRelationship"] != DBNull.Value)
                    {
                        this.ddlTreatmentSupporterRelationship.SelectedValue = theDSData.Tables[1].Rows[0]["TreatmentSupporterRelationship"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["DisclosureStatus"] != DBNull.Value)
                    {
                        this.ddlDisclosureStatus.SelectedValue = theDSData.Tables[1].Rows[0]["DisclosureStatus"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["ReasonNotDisclosed"] != DBNull.Value)
                    {
                        this.ddreasondisclosed.SelectedValue = theDSData.Tables[1].Rows[0]["ReasonNotDisclosed"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["OtherDisclosureStatus"] != DBNull.Value)
                    {
                        txtotherdisclosurestatus.Text = theDSData.Tables[1].Rows[0]["OtherDisclosureStatus"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["SchoolingStatus"] != DBNull.Value)
                    {
                        this.ddlSchoolingStatus.SelectedValue = theDSData.Tables[1].Rows[0]["SchoolingStatus"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["HighestLevelAttained"] != DBNull.Value)
                        ddHighestlevelattained.SelectedValue = theDSData.Tables[1].Rows[0]["HighestLevelAttained"].ToString();
                    if (theDSData.Tables[1].Rows[0]["HIVSupportgroup"] != DBNull.Value)
                    {
                        if (theDSData.Tables[1].Rows[0]["HIVSupportgroup"].ToString() == "True")
                        {
                            this.rblHIVSupportgroup.SelectedValue = "1";
                        }
                        else
                        {
                            this.rblHIVSupportgroup.SelectedValue = "0";
                        }
                    }
                    if (theDSData.Tables[1].Rows[0]["HIVSupportGroupMembership"] != DBNull.Value)
                    {
                        txtHIVSupportGroup.Text = theDSData.Tables[1].Rows[0]["HIVSupportGroupMembership"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["HealthEducation"] != DBNull.Value)
                    {
                        if (theDSData.Tables[1].Rows[0]["HealthEducation"].ToString() == "True")
                            rdoHealthEducation.SelectedValue = "1";
                        else if (theDSData.Tables[1].Rows[0]["HealthEducation"].ToString() == "False")
                            rdoHealthEducation.SelectedValue = "0";
                    }
                    if (theDSData.Tables[1].Rows[0]["PatientReferredFrom"] != DBNull.Value)
                    {
                        ddlPatientReferred.SelectedValue = theDSData.Tables[1].Rows[0]["PatientReferredFrom"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["PatientReferredFromOthers"] != DBNull.Value)
                        txtPatRefother.Text = theDSData.Tables[1].Rows[0]["PatientReferredFromOthers"].ToString();
                    if (theDSData.Tables[1].Rows[0]["NursesComments"] != DBNull.Value)
                        this.idVitalSign.txtnursescomments.Text = theDSData.Tables[1].Rows[0]["NursesComments"].ToString();

                    if (theDSData.Tables[1].Rows[0]["ReferToSpecialistClinic"] != DBNull.Value)
                        this.idVitalSign.txtReferToSpecialistClinic.Text = theDSData.Tables[1].Rows[0]["ReferToSpecialistClinic"].ToString();
                    if (theDSData.Tables[1].Rows[0]["NursesComments"] != DBNull.Value)
                        this.idVitalSign.txtnursescomments.Text = theDSData.Tables[1].Rows[0]["NursesComments"].ToString();

                    //Presenting complaints
                    //radbtnPresentingYesNO.SelectedValue = theDSData.Tables[1].Rows[0]["PresentingComplaints"].ToString();
                    if (theDSData.Tables[1].Rows[0]["otherpresentingcomplaints"] != DBNull.Value)
                        this.UcPc.txtOtherPresentingComplaints.Text = theDSData.Tables[1].Rows[0]["otherpresentingcomplaints"].ToString();
                    if (theDSData.Tables[1].Rows[0]["additionalpresentingcomplaints"] != DBNull.Value)
                        this.UcPc.txtAdditionalComplaints.Text = theDSData.Tables[1].Rows[0]["additionalpresentingcomplaints"].ToString();
                    if (theDSData.Tables[1].Rows[0]["OtherMedicalConditionNotes"] != DBNull.Value)
                        txtOthermedicalconditions.Text = theDSData.Tables[1].Rows[0]["OtherMedicalConditionNotes"].ToString();
                    if (theDSData.Tables[1].Rows[0]["MedHistoryFP"].ToString() == "1")
                        rblFP.SelectedValue = "1";
                    if (theDSData.Tables[1].Rows[0]["MedHistoryLastFP"] != DBNull.Value)
                        txtLastFP.Text = theDSData.Tables[1].Rows[0]["MedHistoryLastFP"].ToString();
                    if (theDSData.Tables[1].Rows[0]["OtherLongTermMedications"] != DBNull.Value)
                        txtOthermedicalconditions.Text = theDSData.Tables[1].Rows[0]["OtherLongTermMedications"].ToString();
                    if (theDSData.Tables[1].Rows[0]["OtherGeneralConditions"] != DBNull.Value)
                        UcPE.txtOtherGeneralConditions.Text = theDSData.Tables[1].Rows[0]["OtherGeneralConditions"].ToString();
                    if (theDSData.Tables[1].Rows[0]["OtherAbdomenConditions"] != DBNull.Value)
                        UcPE.txtOtherAbdomenConditions.Text = theDSData.Tables[1].Rows[0]["OtherAbdomenConditions"].ToString();
                    if (theDSData.Tables[1].Rows[0]["OtherCardiovascularConditions"] != DBNull.Value)
                        UcPE.txtOtherCardiovascularConditions.Text = theDSData.Tables[1].Rows[0]["OtherCardiovascularConditions"].ToString();
                    if (theDSData.Tables[1].Rows[0]["OtherOralCavityConditions"] != DBNull.Value)
                        UcPE.txtOtherOralCavityConditions.Text = theDSData.Tables[1].Rows[0]["OtherOralCavityConditions"].ToString();
                    if (theDSData.Tables[1].Rows[0]["OtherGenitourinaryConditions"] != DBNull.Value)
                        UcPE.txtOtherGenitourinaryConditions.Text = theDSData.Tables[1].Rows[0]["OtherGenitourinaryConditions"].ToString();
                    if (theDSData.Tables[1].Rows[0]["OtherCNSConditions"] != DBNull.Value)
                        UcPE.txtOtherCNSConditions.Text = theDSData.Tables[1].Rows[0]["OtherCNSConditions"].ToString();
                    if (theDSData.Tables[1].Rows[0]["OtherChestLungsConditions"] != DBNull.Value)
                        UcPE.txtOtherChestLungsConditions.Text = theDSData.Tables[1].Rows[0]["OtherChestLungsConditions"].ToString();
                    if (theDSData.Tables[1].Rows[0]["OtherSkinConditions"] != DBNull.Value)
                        UcPE.txtOtherSkinConditions.Text = theDSData.Tables[1].Rows[0]["OtherSkinConditions"].ToString();
                    if (theDSData.Tables[1].Rows[0]["OtherMedicalConditionNotes"] != DBNull.Value)
                        UcPE.txtOtherMedicalConditionNotes.Text = theDSData.Tables[1].Rows[0]["OtherMedicalConditionNotes"].ToString();

                    if (theDSData.Tables[1].Rows[0]["InitialCD4"] != DBNull.Value)
                    {
                        if (Convert.ToDecimal(theDSData.Tables[1].Rows[0]["InitialCD4"]) != 0)
                            txtInitialCD4.Text = theDSData.Tables[1].Rows[0]["InitialCD4"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["InitialCD4Percent"] != DBNull.Value)
                    {
                        if (Convert.ToDecimal(theDSData.Tables[1].Rows[0]["InitialCD4Percent"]) != 0)
                            txtInitialCD4Percent.Text = theDSData.Tables[1].Rows[0]["InitialCD4Percent"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["InitialCD4Date"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(theDSData.Tables[1].Rows[0]["InitialCD4Date"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            dtInitialCD4Date.Value = String.Format("{0:dd-MMM-yyyy}", theDSData.Tables[1].Rows[0]["InitialCD4Date"]);
                    }
                    if (theDSData.Tables[1].Rows[0]["HighestCD4Ever"] != DBNull.Value)
                    {
                        if (Convert.ToDecimal(theDSData.Tables[1].Rows[0]["HighestCD4Ever"]) != 0)
                            txtHighestCD4Ever.Text = theDSData.Tables[1].Rows[0]["HighestCD4Ever"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["HighestCD4Percent"] != DBNull.Value)
                    {
                        if (Convert.ToDecimal(theDSData.Tables[1].Rows[0]["HighestCD4Percent"]) != 0)
                            txtHighestCD4Percent.Text = theDSData.Tables[1].Rows[0]["HighestCD4Percent"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["HighestCD4EverDate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(theDSData.Tables[1].Rows[0]["HighestCD4EverDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            dtHighestCD4Date.Value = String.Format("{0:dd-MMM-yyyy}", theDSData.Tables[1].Rows[0]["HighestCD4EverDate"]);
                    }
                    if (theDSData.Tables[1].Rows[0]["CD4atARTInitiation"] != DBNull.Value)
                    {
                        if (Convert.ToDecimal(theDSData.Tables[1].Rows[0]["CD4atARTInitiation"]) != 0)
                            txtCD4atARTinitiation.Text = theDSData.Tables[1].Rows[0]["CD4atARTInitiation"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["CD4AtARTInitiationPercent"] != DBNull.Value)
                    {
                        if (Convert.ToDecimal(theDSData.Tables[1].Rows[0]["CD4AtARTInitiationPercent"]) != 0)
                            txtCD4PercentAtARTInitiation.Text = theDSData.Tables[1].Rows[0]["CD4AtARTInitiationPercent"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["CD4atARTInitiationDate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(theDSData.Tables[1].Rows[0]["CD4atARTInitiationDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            dtCD4atARTinitiationDate.Value = String.Format("{0:dd-MMM-yyyy}", theDSData.Tables[1].Rows[0]["CD4atARTInitiationDate"]);
                    }
                    if (theDSData.Tables[1].Rows[0]["MostRecentCD4"] != DBNull.Value)
                    {
                        if (Convert.ToDecimal(theDSData.Tables[1].Rows[0]["MostRecentCD4"]) != 0)
                            txtMostRecentCD4.Text = theDSData.Tables[1].Rows[0]["MostRecentCD4"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["MostRecentCD4Percent"] != DBNull.Value)
                    {
                        if (Convert.ToDecimal(theDSData.Tables[1].Rows[0]["MostRecentCD4Percent"]) != 0)
                            txtMostRecentCD4Percent.Text = theDSData.Tables[1].Rows[0]["MostRecentCD4Percent"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["MostRecentCD4Date"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(theDSData.Tables[1].Rows[0]["MostRecentCD4Date"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            dtMostRecentCD4Date.Value = String.Format("{0:dd-MMM-yyyy}", theDSData.Tables[1].Rows[0]["MostRecentCD4Date"]);
                    }
                    if (theDSData.Tables[1].Rows[0]["PreviousViralLoad"] != DBNull.Value)
                    {
                        if (Convert.ToDecimal(theDSData.Tables[1].Rows[0]["PreviousViralLoad"]) != 0)
                            txtPreviousViralLoad.Text = theDSData.Tables[1].Rows[0]["PreviousViralLoad"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["PreviousViralLoadDate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(theDSData.Tables[1].Rows[0]["PreviousViralLoadDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            dtPreviousViralLoadDate.Value = String.Format("{0:dd-MMM-yyyy}", theDSData.Tables[1].Rows[0]["PreviousViralLoadDate"]);
                    }
                    txtOtherHIVRelatedHistory.Text = theDSData.Tables[1].Rows[0]["OtherHIVRelatedHistory"].ToString();
                    //Examination - ARV Exposure
                    //DDLARVExposure.SelectedValue = theDSData.Tables[1].Rows[0]["AnyARVExposure"].ToString();
                    if (theDSData.Tables[1].Rows[0]["PMTC1Regimen"] != DBNull.Value)
                        txtPMTCTRegimen.Text = theDSData.Tables[1].Rows[0]["PMTC1Regimen"].ToString();
                    if (theDSData.Tables[1].Rows[0]["PEP1Regimen"] != DBNull.Value)
                        txtPEP.Text = theDSData.Tables[1].Rows[0]["PEP1Regimen"].ToString();
                    if (theDSData.Tables[1].Rows[0]["HAART1Regimen"] != DBNull.Value)
                        txtHAARTRegimen.Text = theDSData.Tables[1].Rows[0]["HAART1Regimen"].ToString();
                    if (theDSData.Tables[1].Rows[0]["PMTC1StartDate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(theDSData.Tables[1].Rows[0]["PMTC1StartDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            dtPMTCTStartDate.Value = String.Format("{0:dd-MMM-yyyy}", theDSData.Tables[1].Rows[0]["PMTC1StartDate"]);
                    }
                    if (theDSData.Tables[1].Rows[0]["PEP1StartDate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(theDSData.Tables[1].Rows[0]["PEP1StartDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            dtPEP1StartDate.Value = String.Format("{0:dd-MMM-yyyy}", theDSData.Tables[1].Rows[0]["PEP1StartDate"]);
                    }
                    if (theDSData.Tables[1].Rows[0]["HAART1StartDate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(theDSData.Tables[1].Rows[0]["HAART1StartDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            dtHAART1StartDate.Value = String.Format("{0:dd-MMM-yyyy}", theDSData.Tables[1].Rows[0]["HAART1StartDate"]);
                    }
                    if (theDSData.Tables[1].Rows[0]["ARVExposerdosesmissed"] != DBNull.Value)
                    {
                        rblARVdosesmissed.SelectedValue = theDSData.Tables[1].Rows[0]["ARVExposerdosesmissed"].ToString();
                    }
                    if (theDSData.Tables[1].Rows[0]["ARVExposerdelaydoses"] != DBNull.Value)
                    {
                        rblARVDelayeddoses.SelectedValue = theDSData.Tables[1].Rows[0]["ARVExposerdelaydoses"].ToString();
                    }

                    if (theDSData.Tables[1].Rows[0]["ProgressionInWHOstage"] != DBNull.Value)
                    {
                        if (theDSData.Tables[1].Rows[0]["ProgressionInWHOstage"].ToString() == "True")
                            UcWhostaging.rdoProgressionInWHOstage.SelectedValue = "1";
                    }
                    if (theDSData.Tables[1].Rows[0]["SpecifyWHOprogression"] != DBNull.Value)
                        UcWhostaging.txtSpecifyWHOprogression.Text = theDSData.Tables[1].Rows[0]["SpecifyWHOprogression"].ToString();

                    if (theDSData.Tables[1].Rows[0]["WeightforAge"] != DBNull.Value)
                    {
                        this.idVitalSign.lblWA.Text = theDSData.Tables[1].Rows[0]["WeightforAge"].ToString();
                    }

                    if (theDSData.Tables[1].Rows[0]["Menarche"] != System.DBNull.Value)
                    {
                        if (theDSData.Tables[1].Rows[0]["Menarche"].ToString() == "True")
                        {
                            this.UcWhostaging.radbtnMernarcheyes.Checked = true;
                            this.UcWhostaging.radbtnMernarcheno.Checked = false;
                            visibleDiv("divmenarchedatehshowhide");

                        }
                        else if (theDSData.Tables[1].Rows[0]["Menarche"].ToString() == "False")
                        {
                            this.UcWhostaging.radbtnMernarcheyes.Checked = false;
                            this.UcWhostaging.radbtnMernarcheno.Checked = true;
                        }
                    }
                    if (theDSData.Tables[1].Rows[0]["OtherTreatment"] != DBNull.Value)
                        txtOtherTreatment.Text = theDSData.Tables[1].Rows[0]["OtherTreatment"].ToString();

                    //Menarche Date :
                    if (theDSData.Tables[1].Rows[0]["MenarcheDate"] != DBNull.Value)
                        UcWhostaging.txtmenarchedate.Value = String.Format("{0:dd-MMM-yyyy}", theDSData.Tables[1].Rows[0]["MenarcheDate"]);
                    //Tanner staging
                    if (theDSData.Tables[1].Rows[0]["TannerStaging"] != DBNull.Value)
                        UcWhostaging.ddltannerstaging.SelectedValue = theDSData.Tables[1].Rows[0]["TannerStaging"].ToString();
                    if (theDSData.Tables[1].Rows[0]["NonHIVRelatedOI"] != DBNull.Value)
                        txtNonHIVRelatedOI.Text = theDSData.Tables[1].Rows[0]["NonHIVRelatedOI"].ToString();
                    if (theDSData.Tables[1].Rows[0]["HIVRelatedOI"] != DBNull.Value)
                        txtHIVRelatedOI.Text = theDSData.Tables[1].Rows[0]["HIVRelatedOI"].ToString();
                    if (theDSData.Tables[1].Rows[0]["Impression"] != DBNull.Value)
                        txtImpression.Text = theDSData.Tables[1].Rows[0]["Impression"].ToString();

                    //Management - Lab Evaluation
                    if (theDSData.Tables[1].Rows[0]["OtherShortTermEffects"] != DBNull.Value)
                        txtOtherShortTermEffects.Text = theDSData.Tables[1].Rows[0]["OtherShortTermEffects"].ToString();
                    if (theDSData.Tables[1].Rows[0]["OtherLongtermEffects"] != DBNull.Value)
                        txtOtherLongtermEffects.Text = theDSData.Tables[1].Rows[0]["OtherLongtermEffects"].ToString();
                    if (theDSData.Tables[1].Rows[0]["WorkUpPlan"] != DBNull.Value)
                        txtWorkUpPlan.Text = theDSData.Tables[1].Rows[0]["WorkUpPlan"].ToString();
                    if (theDSData.Tables[1].Rows[0]["SpecifyLabEvaluation"] != DBNull.Value)
                        UcLabEval.txtlabdiagnosticinput.Text = theDSData.Tables[1].Rows[0]["SpecifyLabEvaluation"].ToString();
                    if (theDSData.Tables[1].Rows[0]["OIProphylaxis"] != DBNull.Value)
                    {
                        rcbOIProphylaxis.SelectedValue = theDSData.Tables[1].Rows[0]["OIProphylaxis"].ToString();
                        if (rcbOIProphylaxis.SelectedItem.Text == "Cotrimoxazole")
                        {
                            visibleDiv("DIVCotrimoxazole");
                            rcbReasonCTXPrescribed.SelectedValue = theDSData.Tables[1].Rows[0]["ReasonCTXPrescribed"].ToString();
                        }
                        else if (rcbOIProphylaxis.SelectedItem.Text == "Fluconazole")
                        {
                            visibleDiv("divFluconazoleshowhide");
                            //Fluconazole prescribed for :
                            if (theDSData.Tables[1].Rows[0]["Fluconazole"] != DBNull.Value)
                                ddlfluconazole.SelectedValue = theDSData.Tables[1].Rows[0]["Fluconazole"].ToString();
                        }
                        else if (rcbOIProphylaxis.SelectedItem.Text == "CTX and Fluconazol")
                        {
                            visibleDiv("divFluconazoleshowhide");
                            visibleDiv("DIVCotrimoxazole");
                            //Cotrimoxazole prescribed for
                            if (theDSData.Tables[1].Rows[0]["ReasonCTXPrescribed"] != DBNull.Value)
                                rcbReasonCTXPrescribed.SelectedValue = theDSData.Tables[1].Rows[0]["ReasonCTXPrescribed"].ToString();
                        }
                        else if (rcbOIProphylaxis.SelectedItem.Text == "Other")
                        {
                            visibleDiv("divOtherOIPropholyxis");
                            //Other (Specify)
                            if (theDSData.Tables[1].Rows[0]["OtherOIProphylaxis"] != DBNull.Value)
                                txtOtherOIPropholyxis.Text = theDSData.Tables[1].Rows[0]["OtherOIProphylaxis"].ToString();
                        }
                    }

                }



                // Modal Vital Sign	
                if (theDSData.Tables[2].Rows.Count > 0)
                {
                    if (theDSData.Tables[2].Rows[0]["Temp"] != DBNull.Value)
                    {
                        this.idVitalSign.txtTemp.Text = theDSData.Tables[2].Rows[0]["Temp"].ToString();
                    }
                    if (theDSData.Tables[2].Rows[0]["RR"] != DBNull.Value)
                    {
                        this.idVitalSign.txtRR.Text = theDSData.Tables[2].Rows[0]["RR"].ToString();
                    }
                    if (theDSData.Tables[2].Rows[0]["HR"] != DBNull.Value)
                    {
                        this.idVitalSign.txtHR.Text = theDSData.Tables[2].Rows[0]["HR"].ToString();
                    }
                    if (theDSData.Tables[2].Rows[0]["BPSystolic"] != DBNull.Value)
                    {
                        this.idVitalSign.txtBPSystolic.Text = theDSData.Tables[2].Rows[0]["BPSystolic"].ToString();
                    }
                    if (theDSData.Tables[2].Rows[0]["BPDiastolic"] != DBNull.Value)
                    {
                        this.idVitalSign.txtBPDiastolic.Text = theDSData.Tables[2].Rows[0]["BPDiastolic"].ToString();
                    }
                    if (theDSData.Tables[2].Rows[0]["Height"] != DBNull.Value)
                    {
                        this.idVitalSign.txtHeight.Text = theDSData.Tables[2].Rows[0]["Height"].ToString();
                    }
                    if (theDSData.Tables[2].Rows[0]["Weight"] != DBNull.Value)
                    {
                        this.idVitalSign.txtWeight.Text = theDSData.Tables[2].Rows[0]["Weight"].ToString();
                    }
                }

                if (theDSData.Tables[4].Rows.Count > 0)
                {
                    if (theDSData.Tables[4].Rows[0]["WHOStage"] != DBNull.Value)
                        UcWhostaging.ddlwhostage1.SelectedValue = theDSData.Tables[4].Rows[0]["WHOStage"].ToString();
                    if (theDSData.Tables[4].Rows[0]["WABStage"] != DBNull.Value)
                        UcWhostaging.ddlWABStage.SelectedValue = theDSData.Tables[4].Rows[0]["WABStage"].ToString();
                }
                //Mernarche

                if (theDSData.Tables[5].Rows.Count > 0)
                {
                    if (theDSData.Tables[5].Rows[0]["TherapyPlan"] != DBNull.Value)
                        UCPharmacy.ddlTreatmentplan.SelectedValue = theDSData.Tables[5].Rows[0]["TherapyPlan"].ToString();
                    if (theDSData.Tables[5].Rows[0]["specifyOtherEligibility"] != DBNull.Value)
                        UCPharmacy.txtSpecifyOtherEligibility.Text = theDSData.Tables[5].Rows[0]["specifyOtherEligibility"].ToString();
                    if (theDSData.Tables[5].Rows[0]["NoOfDrugsSubstituted"] != DBNull.Value)
                        UCPharmacy.txtNoofdrugssubstituted.Text = theDSData.Tables[5].Rows[0]["NoOfDrugsSubstituted"].ToString();
                    if (theDSData.Tables[5].Rows[0]["specifyOtherARTChangeReason"] != DBNull.Value)
                        UCPharmacy.txtSpecifyotherARTchangereason.Text = theDSData.Tables[5].Rows[0]["specifyOtherARTChangeReason"].ToString();
                    if (theDSData.Tables[5].Rows[0]["specifyOtherStopCode"] != DBNull.Value)
                        UCPharmacy.txtSpecifyOtherStopCode.Text = theDSData.Tables[5].Rows[0]["specifyOtherStopCode"].ToString();
                    if (theDSData.Tables[5].Rows[0]["reasonForSwitchTo2ndLineRegimen"] != DBNull.Value)
                        UCPharmacy.ddlReasonforswitchto2ndlineregimen.SelectedValue = theDSData.Tables[5].Rows[0]["reasonForSwitchTo2ndLineRegimen"].ToString();
                }
                //if (theDSData.Tables[1].Rows[0]["ARTTreatmentPlanPeads"] != DBNull.Value)
                //    UCPharmacy.ddlTreatmentplan.SelectedValue = theDSData.Tables[1].Rows[0]["ARTTreatmentPlanPeads"].ToString();
                //if (theDSData.Tables[1].Rows[0]["OtherARTEligibilityCriteria"] != DBNull.Value)
                //    UCPharmacy.txtSpecifyOtherEligibility.Text = theDSData.Tables[1].Rows[0]["OtherARTEligibilityCriteria"].ToString();
                //if (theDSData.Tables[1].Rows[0]["NumberDrugsSubstituted"] != DBNull.Value)
                //    UCPharmacy.txtNoofdrugssubstituted.Text = theDSData.Tables[1].Rows[0]["NumberDrugsSubstituted"].ToString();
                //if (theDSData.Tables[1].Rows[0]["SpecifyotherARTchangereason"] != DBNull.Value)
                //    UCPharmacy.txtSpecifyotherARTchangereason.Text = theDSData.Tables[1].Rows[0]["SpecifyotherARTchangereason"].ToString();
                //if (theDSData.Tables[1].Rows[0]["OtherARTStopCode"] != DBNull.Value)
                //    UCPharmacy.txtSpecifyOtherStopCode.Text = theDSData.Tables[1].Rows[0]["OtherARTStopCode"].ToString();
                //if (theDSData.Tables[1].Rows[0]["secondLineRegimenSwitch"] != DBNull.Value)
                //    UCPharmacy.ddlReasonforswitchto2ndlineregimen.SelectedValue = theDSData.Tables[1].Rows[0]["secondLineRegimenSwitch"].ToString();

            }
            BindListData(theDSData.Tables[3]);
            ShowHideUpdate();
            ShowHide();
        }
        private void BindAttributes()
        {
            if (Convert.ToString(Session["TechnicalAreaId"]) == "210")
            {
                //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "DCC Adult Initial Evaluation";
                lblHeading.Text = "DCC Adult Initial Evaluation";
            }
            else
            {
                //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Adult Initial Evaluation"; 
                lblHeading.Text = "Adult Initial Evaluation";
            }


            ddlDisclosureStatus.Attributes.Add("onchange", "getSelectedValue('divSpecifyreason', '" + ddlDisclosureStatus.ClientID + "' ,'Not ready'); getSelectedValue('divSpecifyotherdisclosurestatus', '" + ddlDisclosureStatus.ClientID + "','Other specify');");
            ddreasondisclosed.Attributes.Add("onchange", "getSelectedValue('divSpecifyotherdisclosurestatus', '" + ddreasondisclosed.ClientID + "' ,'Other')");
            ddlSchoolingStatus.Attributes.Add("onchange", "getSelectedValue('divHighestLevelattained', '" + ddlSchoolingStatus.ClientID + "' ,'Enrolled')");
            ddlPatientReferred.Attributes.Add("onchange", "getSelectedValue('divPatRefother', '" + ddlPatientReferred.ClientID + "' ,'Other Specify')");
            //ddlTBPlan.Attributes.Add("onchange", "getSelectedValue('DivSOtherTBPlan', '" + ddlTBPlan.ClientID + "' ,'Other (Specify)')");
            //ddlTBRegimen.Attributes.Add("onchange", "getSelectedValue('DivOtherTBRegimen', '" + ddlTBRegimen.ClientID + "', 'Other')");
            //chkreviewchecklist.Attributes.Add("onchange", "CheckBoxToggleShowHide('DIVTBSideeffects', '" + chkreviewchecklist.ClientID + "', 'Other Side effects (specify)')");
            //ddlHIVRelatedHistory.Attributes.Add("onchange", "getSelectedValue('DIVInitialCD4', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes'); getSelectedValue('DIVInitialCD4Perc', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes'); getSelectedValue('DIVInitialCD4Date', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes');getSelectedValue('DIVHighestCD4', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes');getSelectedValue('DIVHighestCD4Per', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes');getSelectedValue('DIVHighestCD4EverDate', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes');getSelectedValue('DIVCD4atARTInitial', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes');getSelectedValue('DIVCD4ARTInitialPerc', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes');getSelectedValue('DIVCD4ARTInitialDate', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes');getSelectedValue('DIVMostRecentCD4', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes');getSelectedValue('DIVMostRecentCD4Perc', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes');getSelectedValue('DIVMostRecentCD4Date', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes');getSelectedValue('DIVPrevviralload', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes');getSelectedValue('DIVPrevviralloaddate', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes');getSelectedValue('DIVOtherHIVHistory', '" + ddlHIVRelatedHistory.ClientID + "', 'Yes')");
            cblDiagnosis.Attributes.Add("onclick", "CheckBoxToggleShowHideKNHAdultIE('DIVHIVrelatedOI', '" + cblDiagnosis.ClientID + "', 'HIV-Related illness');CheckBoxToggleShowHideKNHAdultIE('DIVNonHIVrelatedOI', '" + cblDiagnosis.ClientID + "', 'Non-HIV related illness');");
            //cblDrugAllergiesToxicities.Attributes.Add("onclick", "CheckBoxToggleShowHideKNHAdultIE('DIVDrugAllergy','" + cblDrugAllergiesToxicities.ClientID + "', 'ARV');CheckBoxToggleShowHideKNHAdultIE('DIVOtherdrugallergy', '" + cblDrugAllergiesToxicities.ClientID + "', 'Other');CheckBoxToggleShowHideKNHAdultIE('DIVantibioticallergy', '" + cblDrugAllergiesToxicities.ClientID + "', 'Antibiotic');");
            //cblCounselling.Attributes.Add("onclick", "CheckBoxToggleShowHideKNHAdultIE('DIVCounsellingOther', '" + cblCounselling.ClientID + "', 'Other');");
            cblShortTermEffects.Attributes.Add("onclick", "CheckBoxToggleShowHideKNHAdultIE('divshorttermeffecttxt', '" + cblShortTermEffects.ClientID + "', 'Other Specify');");
            cblLongTermEffects.Attributes.Add("onclick", "CheckBoxToggleShowHideKNHAdultIE('divlongtermeffecttxt', '" + cblLongTermEffects.ClientID + "', 'Other specify');");
            chkLTMedications.Attributes.Add("onclick", "CheckBoxToggleShowHideKNHAdultIE('hideOtherLTM', '" + chkLTMedications.ClientID + "', 'Other');");
            cblPreExistingMedConditions.Attributes.Add("onclick", "CheckBoxToggleShowHideKNHAdultIE('divothermedcondition', '" + cblPreExistingMedConditions.ClientID + "', 'Other medical conditions');");
            //ddlARTTreatmentPlan.Attributes.Add("onchange", "getSelectedValue('DIVswitchregimen', '" + ddlARTTreatmentPlan.ClientID + "' ,'Switch to 2nd line')");
            rcbOIProphylaxis.Attributes.Add("onchange", "getSelectedValue('DIVCotrimoxazole', '" + rcbOIProphylaxis.ClientID + "' ,'Cotrimoxazole');getSelectedValue('divFluconazoleshowhide', '" + rcbOIProphylaxis.ClientID + "' ,'Fluconazole');getSelectedValue('divOtherOIPropholyxis', '" + rcbOIProphylaxis.ClientID + "' ,'Other')");
            txtInitialCD4.Attributes.Add("onkeyup", "chkDecimal('" + txtInitialCD4.ClientID + "')");
            txtInitialCD4Percent.Attributes.Add("onkeyup", "chkDecimal('" + txtInitialCD4Percent.ClientID + "')");
            txtHighestCD4Ever.Attributes.Add("onkeyup", "chkDecimal('" + txtHighestCD4Ever.ClientID + "')");
            txtHighestCD4Percent.Attributes.Add("onkeyup", "chkDecimal('" + txtHighestCD4Percent.ClientID + "')");
            txtCD4atARTinitiation.Attributes.Add("onkeyup", "chkDecimal('" + txtCD4atARTinitiation.ClientID + "')");
            txtCD4PercentAtARTInitiation.Attributes.Add("onkeyup", "chkDecimal('" + txtCD4PercentAtARTInitiation.ClientID + "')");
            txtMostRecentCD4.Attributes.Add("onkeyup", "chkDecimal('" + txtMostRecentCD4.ClientID + "')");
            txtMostRecentCD4Percent.Attributes.Add("onkeyup", "chkDecimal('" + txtMostRecentCD4Percent.ClientID + "')");
            txtPreviousViralLoad.Attributes.Add("onkeyup", "chkDecimal('" + txtPreviousViralLoad.ClientID + "')");

        }
        private void Form_Init()
        {

            IQCareUtils iQCareUtils = new IQCareUtils();
            BindFunctions BindManager = new BindFunctions();
            //BindManager.BindCombo(ddlTreatmentSupporterRelationship, GetDataTable("MST_CODE", "TreatmentSupporterRelationship"), "Name", "Id");
            BindDropdown(ddlTreatmentSupporterRelationship, "TreatmentSupporterRelationship");
            //BindManager.BindCombo(ddlDisclosureStatus, GetDataTable("MST_CODE", "DisclosureStatus"), "Name", "Id");
            BindDropdown(ddlDisclosureStatus, "DisclosureStatus");
            //BindManager.BindCombo(ddreasondisclosed, GetDataTable("MST_CODE", "Reason Not Disclosed"), "Name", "Id");
            BindDropdown(ddreasondisclosed, "Reason Not Disclosed");
            //BindManager.BindCombo(ddlSchoolingStatus, GetDataTable("MST_CODE", "SchoolingStatus"), "Name", "Id");
            BindDropdown(ddlSchoolingStatus, "SchoolingStatus");
            //BindManager.BindCombo(ddlPatientReferred, GetDataTable("MST_CODE", "PatientReferred"), "Name", "Id");
            BindDropdown(ddlPatientReferred, "PatientReferred");
            //BindManager.BindCombo(ddlLMPNotaccessedReason, GetDataTable("MST_CODE", "LMPNotaccessedReason"), "Name", "Id");
            //BindManager.BindCombo(ddlTBFindings, GetDataTable("MST_CODE", "TBFindings"), "Name", "Id");
            //BindManager.BindCombo(ddlSputumSmear, GetDataTable("MST_CODE", "SputumSmear"), "Name", "Id");
            //BindManager.BindCombo(ddlCXR, GetDataTable("MST_CODE", "CXR"), "Name", "Id");
            //BindManager.BindCombo(ddlTBType, GetDataTable("MST_CODE", "TBTypePeads"), "Name", "Id");
            //BindManager.BindCombo(ddlTBPatientType, GetDataTable("MST_CODE", "PeadsTBPatientType"), "Name", "Id");
            //BindManager.BindCombo(ddlTBPlan, GetDataTable("MST_CODE", "TBPlan"), "Name", "Id");
            //BindManager.BindCombo(ddlTBRegimen, GetDataTable("MST_CODE", "TBRegimen"), "Name", "Id");
            //BindManager.BindCombo(ddlTBTreatmentOutcomes, GetDataTable("MST_CODE", "TBTreatmentOutcomesPeads"), "Name", "Id");
            //BindManager.BindCombo(ddlSchoolPerfomance, GetDataTable("MST_CODE", "SchoolPerfomance"), "Name", "Id");
            //BindManager.BindCombo(ddlHIVRelatedHistory, GetDataTable("MST_CODE", "HIVRelatedHistory"), "Name", "Id");
            //BindManager.BindCombo(ddlInitiationWHOstage, GetDataTable("MST_CODE", "InitiationWHOstage"), "Name", "Id");
            //BindManager.BindCombo(ddlWhoStage, GetDataTable("MST_CODE", "InitiationWHOstage"), "Name", "Id");
            //BindManager.BindCombo(ddlWABStage, GetDataTable("MST_CODE", "WABStage"), "Name", "Id");
            //BindManager.BindCombo(rcbTannerStaging, GetDataTable("MST_CODE", "TannerStaging"), "Name", "Id");
            //BindManager.BindCombo(rcbSignature, GetDataTable("MST_CODE", "Employee_Master"), "Name", "Id");
            //BindManager.BindCombo(ddlARTTreatmentPlan, GetDataTable("MST_CODE", "ARTTreatmentPlanPeads"), "Name", "Id");
            //BindManager.BindCombo(rcbNumberDrugsSubstituted, GetDataTable("MST_CODE", "NumberDrugsSubstituted"), "Name", "Id");
            //BindManager.BindCombo(rcbRegimenPrescribed, GetDataTable("MST_CODE", "RegimenPrescribed"), "Name", "Id");
            //BindManager.BindCombo(rcbOIProphylaxis, GetDataTable("MST_CODE", "OIProphylaxis"), "Name", "Id");
            BindDropdown(rcbOIProphylaxis, "OIProphylaxis");
            //BindManager.BindCombo(rcbReasonCTXPrescribed, GetDataTable("MST_CODE", "ReasonCTXpresribed"), "Name", "Id");
            BindDropdown(rcbReasonCTXPrescribed, "ReasonCTXpresribed");
            //BindManager.BindCombo(ddlfluconazole, GetDataTable("MST_CODE", "Fluconazole"), "Name", "Id");
            BindDropdown(ddlfluconazole, "FluconazolePrescribedFor");
            //BindManager.BindCombo(rcbPartnerHIVStatus, GetDataTable("MST_CODE", "PartnerHIVStatus"), "Name", "Id");
            //BindManager.BindCombo(rcbFPMethod, GetDataTable("MST_CODE", "FPMethod"), "Name", "Id");
            //BindManager.BindCombo(rcbCervicalCancerScreeningResults, GetDataTable("MST_CODE", "CervicalCancerScreeningResults"), "Name", "Id");
            //BindManager.BindCombo(rcbRefferedToFupF, GetDataTable("MST_CODE", "RefferedToFupF"), "Name", "Id");
            //BindManager.BindCombo(ddHighestlevelattained, GetDataTable("MST_CODE", "HighestLevelAttained"), "Name", "Id");
            BindDropdown(ddHighestlevelattained, "HighestLevelAttained");
            //BindManager.BindCombo(DDLARVExposure, GetDataTable("Mst_Code", "ARVExposure"), "Name", "Id");
            //BindManager.BindCombo(DDLReasonLMP, GetDataTable("Mst_Code", "LMP Not Accessed"), "Name", "Id");
            BindMultiCheckBox();
        }

        public void BindChkboxlstControl(CheckBoxList chklst, string fieldname)
        {
            DataTable thedeCodeDT = new DataTable();
            IQCareUtils iQCareUtils = new IQCareUtils();
            BindFunctions BindManager = new BindFunctions();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));


            DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theCodeDV);
            DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);

            if (theCodeDT.Rows.Count > 0)
            {

                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + theCodeDT.Rows[0]["CodeId"];
                thedeCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            }

            if (thedeCodeDT.Rows.Count > 0)
            {
                BindManager.BindCheckedList(chklst, thedeCodeDT, "Name", "ID");
            }


        }
        private void BindDropdown(DropDownList DropDownID, string fieldname)
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataView theCodeDV = new DataView(theDS.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
            //ddSignature.DataSource = null;
            //ddSignature.Items.Clear();


            if (theDS.Tables["Mst_Decode"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Decode"]);
                if (theCodeDT.Rows.Count > 0)
                {
                    theDV.RowFilter = "DeleteFlag=0 and CodeId=" + theCodeDT.Rows[0]["CodeId"] + "and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
                    if (theDV.Table != null)
                    {
                        DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(DropDownID, theDT, "Name", "Id");
                    }
                }

            }
        }
        //protected DataTable GetDataTable(string flag, string fieldName)
        //{
        //    BIQAdultIE objAdultIEFields = new BIQAdultIE();
        //    objAdultIEFields.Flag = flag;
        //    objAdultIEFields.PtnPk = 0;//Convert.ToInt32(Session["PatientID"].ToString());
        //    objAdultIEFields.LocationId = 0;// Int32.Parse(Session["AppLocationId"].ToString());
        //    objAdultIEFields.FieldName = fieldName;
        //    theExpressManager = (IKNHAdultIE)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAdultIE, BusinessProcess.Clinical");
        //    DataTable dt = theExpressManager.GetKnhAdultIEData(objAdultIEFields);
        //    return dt;

        //}
        protected void BindMultiCheckBox()
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            BindFunctions bindManager = new BindFunctions();
            //bindManager.BindCheckedList(cblTBAssessmentICF, GetDataTable("Mst_Code", "TBAssessmentICF"), "Name", "ID");
            //bindManager.BindCheckedList(cblDiagnosis, GetDataTable("Mst_Code", "Diagnosis"), "Name", "ID");
            BindChkboxlstControl(cblDiagnosis, "Diagnosis");
            //bindManager.BindCheckedList(cblDrugAllergiesToxicities, GetDataTable("Mst_Code", "DrugAllergiesToxicitiesPaeds"), "Name", "ID");
            //bindManager.BindCheckedList(cblShortTermEffects, GetDataTable("Mst_Code", "ShortTermEffects"), "NAME", "ID");
            BindChkboxlstControl(cblShortTermEffects, "ShortTermEffects");
            //bindManager.BindCheckedList(cblLongTermEffects, GetDataTable("Mst_Code", "LongTermEffects"), "NAME", "ID");
            BindChkboxlstControl(cblLongTermEffects, "LongTermEffects");
            //bindManager.BindCheckedList(cblSpecifyLabEvaluation, GetDataTable("Mst_Code", "SpecifyLabEvaluation"), "NAME", "ID");
            //bindManager.BindCheckedList(cblCounselling, GetDataTable("Mst_Code", "Counselling"), "NAME", "ID");
            //bindManager.BindCheckedList(cblSwitchReason, GetDataTable("Mst_Code", "SwitchReason"), "NAME", "ID");
            //bindManager.BindCheckedList(cblStopTreatmentCodes, GetDataTable("Mst_Code", "StopTreatmentCodes"), "NAME", "ID");
            //bindManager.BindCheckedList(cblARTEligibilityCriteria, GetDataTable("Mst_Code", "ARTEligibilityCriteria"), "NAME", "ID");
            //bindManager.BindCheckedList(cblHighRisk, GetDataTable("Mst_Code", "HighRisk"), "NAME", "ID");
            //bindManager.BindCheckedList(chkLstStopTBReason, GetDataTable("Mst_Code", "TBStopReason"), "Name", "Id");
            //bindManager.BindCheckedList(chkreviewchecklist, GetDataTable("Mst_Code", "TBSideEffects"), "Name", "Id");
            //bindManager.BindCheckedList(chkLTMedications, GetDataTable("Mst_Code", "LongTermMedications"), "Name", "Id");
            BindChkboxlstControl(chkLTMedications, "LongTermMedications");
            //bindManager.BindCheckedList(cblPreExistingMedConditions, GetDataTable("Mst_Code", "SpecificMedicalCondition"), "Name", "ID");
            BindChkboxlstControl(cblPreExistingMedConditions, "SpecificMedicalCondition");

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            //string s = this.idVitalSign.txtRadTemperatureModal.Text;
        }
        int ConvertInt(string intvalue)
        {
            int retval = 99;
            if (intvalue != "")
            {
                retval = Convert.ToInt32(intvalue);
            }
            else
            {
                retval = 99;
            }
            return retval;
        }
        DateTime DateGiven(string dateVal)
        {
            DateTime dt = Convert.ToDateTime("01/01/1900");
            if (!string.IsNullOrEmpty(dateVal))
            {
                dt = DateTime.Parse(dateVal);
            }
            return dt;
        }
        int rcbSelectedValue(DropDownList ddl)
        {
            int retval = 0;
            if (ddl.SelectedValue != "")
            {
                retval = Convert.ToInt32(ddl.SelectedValue.ToString());
            }
            return retval;
        }
        decimal GettxtValue(string strtxtVal)
        {
            decimal retval = 0;
            if (strtxtVal != "")
            {
                retval = Convert.ToDecimal(strtxtVal);
            }
            return retval;

        }
        protected void SaveForm()
        {
            if (fieldValidation(btnSaveTriage.ID) == false)
            {
                ShowHide();
                return;
            }
            List<BIQAdultIE> list = new List<BIQAdultIE>();
            List<AdultIEMultiselect> listmulti = new List<AdultIEMultiselect>();
            BIQAdultIE objAdultIE = new BIQAdultIE();
            AdultIEMultiselect objAdultIEMultiSelect = new AdultIEMultiselect();
            StringBuilder Insertcbl = new StringBuilder();
            string error = "";
            try
            {
                objAdultIE.ID = 0;
                objAdultIE.PtnPk = Convert.ToInt32(Session["PatientId"]);
                string VisitIdforMultiSelect = "IDENT_CURRENT('ord_visit')";
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    VisitIdforMultiSelect = Session["PatientVisitId"].ToString();
                    objAdultIE.VisitPk = Convert.ToInt32(Session["PatientVisitId"]);
                    objAdultIE.LocationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                else
                {

                    objAdultIE.VisitPk = Convert.ToInt32(Session["PatientVisitId"]);
                    objAdultIE.LocationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                //objAdultIE.LocationId = Int32.Parse(Session["AppLocationId"].ToString());
                objAdultIE.UserId = Int32.Parse(Session["AppUserId"].ToString());
                objAdultIE.VisitDate = string.Format("{0:dd-MMM-yyyy}", txtVisitDate.Value);
                //HIV Care and Support Evaluation
                objAdultIE.DiagnosisConfirmed = this.rblDiagnosisYesNo.SelectedValue == "1" ? 1 : 0;
                objAdultIE.ConfirmHIVPosDate = string.Format("{0:dd-MMM-yyyy}", txtdtConfirmHIVPosDate.Value);
                objAdultIE.ChildAccompaniedByCaregiver = this.rblChildAccompaniedBy.SelectedValue == "1" ? 1 : 0;
                objAdultIE.TreatmentSupporterRelationship = ConvertInt(this.ddlTreatmentSupporterRelationship.SelectedValue);
                objAdultIE.DisclosureStatus = ConvertInt(this.ddlDisclosureStatus.SelectedValue);
                //objAdultIE.reasonnotdisclosed = "";
                if (ddlDisclosureStatus.SelectedItem.Text == "Not ready")
                { objAdultIE.reasonnotdisclosed = ConvertInt(ddreasondisclosed.SelectedItem.Value); }
                objAdultIE.otherdisclosurestatus = "";
                if (ddreasondisclosed.SelectedItem.Text == "Other")
                { objAdultIE.otherdisclosurestatus = txtotherdisclosurestatus.Text; }
                objAdultIE.SchoolingStatus = ConvertInt(this.ddlSchoolingStatus.SelectedValue);
                objAdultIE.Highestlevelattained = 0;
                if (this.ddlSchoolingStatus.Text == "Enrolled")
                { objAdultIE.Highestlevelattained = ConvertInt(ddHighestlevelattained.SelectedValue); }
                objAdultIE.HIVSupportgroup = this.rblHIVSupportgroup.SelectedValue == "1" ? 1 : 0;
                objAdultIE.supportgroupmembership = "";
                if (rblHIVSupportgroup.SelectedValue == "1")
                { objAdultIE.supportgroupmembership = txtHIVSupportGroup.Text; }
                //objAdultIE.PatientReferredFrom = ConvertInt(ddlPatientReferred.SelectedValue);
                //objAdultIE.NursesComments = txtNursesComment.Text;

                // Modal Vital Sign
                objAdultIE.Temperature = GettxtValue(this.idVitalSign.txtTemp.Text.ToString());
                objAdultIE.RespirationRate = GettxtValue(this.idVitalSign.txtRR.Text.ToString());
                objAdultIE.HeartRate = GettxtValue(this.idVitalSign.txtHR.Text.ToString());
                objAdultIE.SystolicBloodPressure = GettxtValue(this.idVitalSign.txtBPSystolic.Text.ToString());
                objAdultIE.DiastolicBloodPressure = GettxtValue(this.idVitalSign.txtBPDiastolic.Text.ToString());
                objAdultIE.Height = GettxtValue(idVitalSign.txtHeight.Text.ToString());
                objAdultIE.Weight = GettxtValue(idVitalSign.txtWeight.Text.ToString());
                objAdultIE.txtheadcircumference = GettxtValue(idVitalSign.txtheadcircumference.Text.ToString());
                objAdultIE.ddlweightforage = idVitalSign.lblWA.Text == "" ? 0 : ConvertInt(idVitalSign.lblWA.Text);
                objAdultIE.txtweightforheight = GettxtValue(idVitalSign.lblWH.Text.ToString());

                // Section Clinical History
                objAdultIE.PresentingComplaints = 0;
                //StringBuilder Insertcbl = new StringBuilder();
                //if (radbtnPresentingYesNO.SelectedValue == "1")
                //{
                //objAdultIE.PresentingComplaints = ConvertInt(radbtnPresentingYesNO.SelectedValue);
                for (int i = 0; i < UcPc.gvPresentingComplaints.Rows.Count; i++)
                {
                    Label lblPComplaintsId = (Label)UcPc.gvPresentingComplaints.Rows[i].FindControl("lblPresenting");
                    CheckBox chkPComplaints = (CheckBox)UcPc.gvPresentingComplaints.Rows[i].FindControl("ChkPresenting");
                    TextBox txtPComplaints = (TextBox)UcPc.gvPresentingComplaints.Rows[i].FindControl("txtPresenting");
                    if (chkPComplaints.Checked == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "PresentingComplaints";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(lblPComplaintsId.Text);
                        objAdultIEMultiSelect.Notes = "";
                        if (!String.IsNullOrEmpty(txtPComplaints.Text))
                        {
                            objAdultIEMultiSelect.NumericField = Convert.ToInt32(txtPComplaints.Text);
                        }


                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                        // listmulti.Add(objAdultIEMultiSelect);
                        // objAdultIE.AdultIEMultiSelect = listmulti;
                    }
                }
                //}
                objAdultIE.otherspecifiedcomplaints = this.UcPc.txtOtherPresentingComplaints.Text;
                //objAdultIE.Additionalcomplaints = txtAdditionalComplaints.Text;
                objAdultIE.RespiratoryDiseaseName = "";
                objAdultIE.RespiratoryDiseaseDate = DateGiven("");
                objAdultIE.RespiratoryDiseaseTreatment = "";
                objAdultIE.CardiovascularDiseaseName = "";
                objAdultIE.CardiovascularDiseaseDate = DateGiven("");
                objAdultIE.CardiovascularDiseaseTreatment = "";
                objAdultIE.GastroIntestinalDiseaseName = "";
                objAdultIE.GastroIntestinalDiseaseDate = DateGiven("");
                objAdultIE.GastroIntestinalDiseaseTreatment = "";
                objAdultIE.NervousDiseaseName = "";
                objAdultIE.NervousDiseaseDate = DateGiven("");
                objAdultIE.NervousDiseaseTreatment = "";
                objAdultIE.DermatologyDiseaseName = "";
                objAdultIE.DermatologyDiseaseDate = DateGiven("");
                objAdultIE.DermatologyDiseaseTreatment = "";
                objAdultIE.MusculoskeletalDiseaseName = "";
                objAdultIE.MusculoskeletalDiseaseDate = DateGiven("");
                objAdultIE.MusculoskeletalDiseaseTreatment = "";
                objAdultIE.PsychiatricDiseaseName = "";
                objAdultIE.PsychiatricDiseaseDate = DateGiven("");
                objAdultIE.PsychiatricDiseaseTreatment = "";
                objAdultIE.HematologicalDiseaseName = "";
                objAdultIE.HematologicalDiseaseDate = DateGiven("");
                objAdultIE.HematologicalDiseaseTreatment = "";
                objAdultIE.GenitalUrinaryDiseaseName = "";
                objAdultIE.GenitalUrinaryDiseaseDate = DateGiven("");
                objAdultIE.GenitalUrinaryDiseaseTreatment = "";
                objAdultIE.OphthamologyDiseaseName = "";
                objAdultIE.OphthamologyDiseaseDate = DateGiven("");
                objAdultIE.OphthamologyDiseaseTreatment = "";
                objAdultIE.ENTDiseaseName = "";
                objAdultIE.ENTDiseaseDate = DateGiven("");
                objAdultIE.ENTDiseaseTreatment = "";
                //Other Medical History
                //objAdultIE.OtherMedicalHistory = ConvertInt(radbtnMedicalYesNO.SelectedValue);
                objAdultIE.LMPassessmentValid = 0;
                objAdultIE.OtherDiseaseName = "";
                objAdultIE.OtherDiseaseDate = DateGiven("");
                objAdultIE.OtherDiseaseTreatment = "";
                objAdultIE.SchoolPerfomance = 0;
                objAdultIE.MedHistoryFP = ConvertInt(rblFP.SelectedValue);
                objAdultIE.MedHistoryLastFP = "";
                if (rblFP.SelectedValue == "1")
                {
                    objAdultIE.MedHistoryLastFP = Convert.ToString(txtLastFP.Text);

                }
                //TB Findings
                //TB Screening-TB Assessment
                //objAdultIE.TBAssessementICF = ConvertInt(radbtnTBAssessmentYesNo.SelectedValue);
                //objAdultIE.TBresultsAvailable = ConvertInt(rblTBresultsAvailable.SelectedValue);
                //objAdultIE.SputumSmear = 0;
                //objAdultIE.SputumSmearDate = DateGiven("");
                //objAdultIE.ChestXRay = 0;
                //objAdultIE.ChestXRayDate = DateGiven("");
                //objAdultIE.TissueBiopsy = 0;
                //objAdultIE.TissueBiopsyDate = DateGiven(dtTissueBiopsyDate.Value.ToString());
                //objAdultIE.CXR = 0;
                //objAdultIE.OtherCXR = txtOtherCXR.Text;
                //objAdultIE.TBType = rcbSelectedValue(ddlTBType);
                //objAdultIE.TBPatientType = rcbSelectedValue(ddlTBPatientType);
                //objAdultIE.TBPlan = rcbSelectedValue(ddlTBPlan);
                //objAdultIE.TBRegimen = rcbSelectedValue(ddlTBRegimen);
                //objAdultIE.TBRegimenStartDate = DateGiven(dtTBRegimenStartDate.Value.ToString());
                //objAdultIE.TBRegimenEndDate = DateGiven(dtTBRegimenEndDate.Value.ToString());
                //objAdultIE.TBTreatmentOutcomes = rcbSelectedValue(ddlTBTreatmentOutcomes);
                //if (radbtnTBAssessmentYesNo.SelectedValue == "1")
                //{
                //    for (int i = 0; i < cblTBAssessmentICF.Items.Count; i++)
                //    {
                //        if (cblTBAssessmentICF.Items[i].Selected == true)
                //        {
                //            objAdultIEMultiSelect.FieldId = 0;
                //            objAdultIEMultiSelect.FieldName = "TBAssessmentICF";
                //            objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblTBAssessmentICF.Items[i].Value);
                //            listmulti.Add(objAdultIEMultiSelect);
                //            objAdultIE.AdultIEMultiSelect = listmulti;
                //        }
                //    }
                //    objAdultIE.TBFindings = rcbSelectedValue(ddlTBFindings);
                //    if (rblTBresultsAvailable.SelectedValue == "1")
                //    {
                //        objAdultIE.SputumSmear = rcbSelectedValue(ddlSputumSmear);
                //        objAdultIE.SputumSmearDate = DateGiven(txtSputumSmearDate.Value.ToString());
                //        objAdultIE.ChestXRay = ConvertInt(rblbtnChestray.SelectedValue.ToString());
                //        objAdultIE.ChestXRayDate = DateGiven(dtChestrayDate.Value.ToString());
                //        objAdultIE.TissueBiopsy = ConvertInt(rblbtnTissueBiopsy.SelectedValue.ToString());
                //        objAdultIE.TissueBiopsyDate = DateGiven(dtTissueBiopsyDate.Value.ToString());
                //        objAdultIE.CXR = rcbSelectedValue(ddlCXR);
                //        objAdultIE.OtherCXR = txtOtherCXR.Text;
                //    }
                //}
                //objAdultIE.TBType = rcbSelectedValue(ddlTBType);
                //objAdultIE.TBPatientType = rcbSelectedValue(ddlTBPatientType);
                //if (ddlTBPlan.Text == "Other (Specify)")
                //{
                //    objAdultIE.TBPlanOther = txTBPlanOther.Text;
                //}
                //if (ddlTBRegimen.Text == "Other")
                //{
                //    objAdultIE.TBRegimenother = txTBRegimenOther.Text;
                //}
                //TB Screening-IPT(Patients with No signs and symptoms)
                //objAdultIE.NoTBSign = ConvertInt(rbbtnNoTB.SelectedValue.ToString());
                //objAdultIE.pyridoxine = this.chkpyridoxine.Checked == true ? 1 : 0;
                //objAdultIE.INHStartDate = DateGiven("");
                //objAdultIE.INHEndDate = DateGiven("");
                //objAdultIE.PyridoxineStartDate = DateGiven("");
                //objAdultIE.PyridoxineEndDate = DateGiven("");
                //objAdultIE.adherenceassessed = ConvertInt(rblAdherenceassessed.SelectedValue.ToString());
                //objAdultIE.dosesmissed = ConvertInt(rblmisseddoses.SelectedValue.ToString());
                //objAdultIE.adherencereferred = ConvertInt(rblrefadherence.SelectedValue.ToString());
                //if (rblIPTPatients.SelectedValue == "1")
                //{
                //    if (rbbtnNoTB.SelectedValue == "1")
                //    {
                //        //Stop reason
                //        for (int i = 0; i < chkLstStopTBReason.Items.Count; i++)
                //        {
                //            if (chkLstStopTBReason.Items[i].Selected == true)
                //            {
                //                objAdultIEMultiSelect.FieldId = 0;
                //                objAdultIEMultiSelect.FieldName = "TBStopReason";
                //                objAdultIEMultiSelect.ValueID = Convert.ToInt32(chkLstStopTBReason.Items[i].Value);
                //                listmulti.Add(objAdultIEMultiSelect);
                //                objAdultIE.AdultIEMultiSelect = listmulti;
                //            }
                //        }

                //        objAdultIE.pyridoxine = this.chkpyridoxine.Checked == true ? 1 : 0;
                //        objAdultIE.INHStartDate = DateGiven(dtINHStartDate.Value.ToString());
                //        objAdultIE.INHEndDate = DateGiven(dtINHEndDate.Value.ToString());
                //        objAdultIE.PyridoxineStartDate = DateGiven(dtPyridoxineStartDate.Value.ToString());
                //        objAdultIE.PyridoxineEndDate = DateGiven(dtPyridoxineEndDate.Value.ToString());
                //        objAdultIE.adherenceassessed = ConvertInt(rblAdherenceassessed.SelectedValue.ToString());
                //        if (rblmisseddoses.SelectedValue == "1")
                //        {
                //            objAdultIE.adherencereferred = ConvertInt(rblrefadherence.SelectedValue.ToString());
                //        }
                //    }
                //Review Checklist
                //    for (int i = 0; i < chkreviewchecklist.Items.Count; i++)
                //    {
                //        if (chkreviewchecklist.Items[i].Selected == true)
                //        {
                //            objAdultIEMultiSelect.FieldId = 0;
                //            objAdultIEMultiSelect.FieldName = "TBSideEffects";
                //            objAdultIEMultiSelect.ValueID = Convert.ToInt32(chkreviewchecklist.Items[i].Value);
                //            listmulti.Add(objAdultIEMultiSelect);
                //            objAdultIE.AdultIEMultiSelect = listmulti;
                //        }
                //    }
                //}
                //TB Screening-Discontinue IPT
                //objAdultIE.DiscontinueIPT = ConvertInt(rblDiscontinueIPT.SelectedValue);
                //objAdultIE.SuspectTB = ConvertInt(rblbtnSuspectTB.SelectedValue.ToString());
                //objAdultIE.StopINHDate = DateGiven("");
                //objAdultIE.ContactsScreenedForTB = ConvertInt(rblbtnContactsScreenedForTB.SelectedValue.ToString());
                //objAdultIE.TBNotScreenedSpecify = "";
                //if (rblDiscontinueIPT.SelectedValue == "0")
                //{
                //    if (rblbtnSuspectTB.SelectedValue == "0")
                //    {
                //        objAdultIE.StopINHDate = DateGiven(dtStopINHDate.Value.ToString());
                //    }
                //    if (rblbtnContactsScreenedForTB.SelectedValue == "0")
                //    {
                //        objAdultIE.TBNotScreenedSpecify = txtTBNotScreenedSpecify.Text;
                //    }
                //}
                //Current Long Term Medications
                //objAdultIE.CurrentLongtermmedications = ConvertInt(rblCLongTermmedications.SelectedValue.ToString());
                //objAdultIE.LongTermMedications = ConvertInt(rblbtnLongTermMedications.SelectedValue.ToString());
                //objAdultIE.Cotrimoxazole = DateGiven(dtSulfaTMPDate.Value.ToString());
                //objAdultIE.HormonalContraceptivesDate = DateGiven(dtHormonalContraceptivesDate.Value.ToString());
                //objAdultIE.AntihypertensivesDate = DateGiven(dtAntihypertensivesDate.Value.ToString());
                //objAdultIE.HypoglycemicsDate = DateGiven(dtHypoglycemicsDate.Value.ToString());
                //objAdultIE.AnticonvulsantsDate = DateGiven(dtAntincovulsantsDate.Value.ToString());
                //objAdultIE.OtherLongTermMedications = txOtherLongTermMedications.Text;
                //objAdultIE.OtherCurrentLongTermMedications = DateGiven(dtOtherCurrentLongTermMedications.Value.ToString());
                for (int i = 0; i < chkLTMedications.Items.Count; i++)
                {
                    if (chkLTMedications.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "LongTermMedications";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(chkLTMedications.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ", " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                    }
                }
                objAdultIE.OtherLongTermMedications = txOtherLongTermMedications.Text;

                //Physical examination
                //objAdultIE.Physicalexamination = ConvertInt("");
                objAdultIE.Additionalmedicalconditionnotes = this.UcPE.txtOtherAbdomenConditions.Text;
                for (int i = 0; i < this.UcPE.cblGeneralConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblGeneralConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "GeneralConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblGeneralConditions.Items[i].Value);
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                    }
                }
                for (int i = 0; i < this.UcPE.cblCardiovascularConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblCardiovascularConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "CardiovascularConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblCardiovascularConditions.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");

                    }
                }
                for (int i = 0; i < this.UcPE.cblOralCavityConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblOralCavityConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "OralCavityConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblOralCavityConditions.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");

                    }
                }
                for (int i = 0; i < this.UcPE.cblGenitalUrinaryConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblGenitalUrinaryConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "GenitalUrinaryConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblGenitalUrinaryConditions.Items[i].Value);
                        objAdultIEMultiSelect.Notes = this.UcPE.txtOtherGenitourinaryConditions.Text;
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");

                    }
                }
                for (int i = 0; i < this.UcPE.cblCNSConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblCNSConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "CNSConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblCNSConditions.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");

                    }
                }
                for (int i = 0; i < this.UcPE.cblChestLungsConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblChestLungsConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "ChestLungsConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblChestLungsConditions.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");

                    }
                }
                for (int i = 0; i < this.UcPE.cblSkinConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblSkinConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "SkinConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblSkinConditions.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");

                    }
                }
                for (int i = 0; i < this.UcPE.cblAbdomenConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblAbdomenConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "AbdomenConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblAbdomenConditions.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");

                    }
                }
                //Examination-HIVRelatedTests
                //objAdultIE.HIVRelatedTest = ConvertInt(rblHIVRelatedTests.SelectedValue);
                //objAdultIE.HIVRelatedHistory = ConvertInt(rblHIVHistory.SelectedValue);
                objAdultIE.InitialCD4 = GettxtValue("");
                objAdultIE.InitialCD4Percent = GettxtValue("");
                objAdultIE.InitialCD4Date = DateGiven("");
                objAdultIE.HighestCD4Ever = GettxtValue("");
                objAdultIE.HighestCD4Percent = GettxtValue("");
                objAdultIE.HighestCD4EverDate = DateGiven("");
                objAdultIE.CD4atARTInitiation = GettxtValue("");
                objAdultIE.CD4AtARTInitiationPercent = GettxtValue("");
                objAdultIE.CD4atARTInitiationDate = DateGiven("");
                objAdultIE.MostRecentCD4 = GettxtValue("");
                objAdultIE.MostRecentCD4Percent = GettxtValue("");
                objAdultIE.MostRecentCD4Date = DateGiven("");
                objAdultIE.PreviousViralLoad = GettxtValue("");
                objAdultIE.PreviousViralLoadDate = DateGiven("");
                objAdultIE.OtherHIVRelatedHistory = "";
                //if (rblHIVHistory.SelectedValue == "1")
                //{
                objAdultIE.InitialCD4 = GettxtValue(txtInitialCD4.Text);
                objAdultIE.InitialCD4Percent = GettxtValue(txtInitialCD4Percent.Text);
                objAdultIE.InitialCD4Date = DateGiven(dtInitialCD4Date.Value.ToString());
                objAdultIE.HighestCD4Ever = GettxtValue(txtHighestCD4Ever.Text);
                objAdultIE.HighestCD4Percent = GettxtValue(txtHighestCD4Percent.Text);
                objAdultIE.HighestCD4EverDate = DateGiven(dtHighestCD4Date.Value.ToString());
                objAdultIE.CD4atARTInitiation = GettxtValue(txtCD4atARTinitiation.Text);
                objAdultIE.CD4AtARTInitiationPercent = GettxtValue(txtCD4PercentAtARTInitiation.Text);
                objAdultIE.CD4atARTInitiationDate = DateGiven(dtCD4atARTinitiationDate.Value.ToString());
                objAdultIE.MostRecentCD4 = GettxtValue(txtMostRecentCD4.Text);
                objAdultIE.MostRecentCD4Percent = GettxtValue(txtMostRecentCD4Percent.Text);
                objAdultIE.MostRecentCD4Date = DateGiven(dtMostRecentCD4Date.Value.ToString());
                objAdultIE.PreviousViralLoad = GettxtValue(txtPreviousViralLoad.Text);
                objAdultIE.PreviousViralLoadDate = DateGiven(dtPreviousViralLoadDate.Value.ToString());
                objAdultIE.OtherHIVRelatedHistory = txtOtherHIVRelatedHistory.Text;
                //}
                //Examination - ARV Exposure
                //objAdultIE.ARVExposureYesNo = ConvertInt(rblARVExposure.SelectedValue.ToString());
                //objAdultIE.ARVExposure = ConvertInt(DDLARVExposure.SelectedValue.ToString());
                objAdultIE.PMTC1StartDate = DateGiven("");
                objAdultIE.PEP1StartDate = DateGiven("");
                objAdultIE.HAART1StartDate = DateGiven("");
                objAdultIE.ARVExposerdosesmissed = ConvertInt(rblARVdosesmissed.SelectedValue);
                objAdultIE.ARVExposerdelaydoses = ConvertInt(rblARVDelayeddoses.SelectedValue);
                objAdultIE.PMTC1Regimen = txtPMTCTRegimen.Text;
                objAdultIE.PMTC1StartDate = DateGiven(dtPMTCTStartDate.Value);
                objAdultIE.PEP1Regimen = txtPEP.Text;
                objAdultIE.PEP1StartDate = DateGiven(dtPEP1StartDate.Value);
                objAdultIE.HAART1Regimen = txtHAARTRegimen.Text;
                objAdultIE.HAART1StartDate = DateGiven(dtHAART1StartDate.Value);


                //if (DDLARVExposure.SelectedItem.Text == "PMTCT Regimen")
                //{
                //    objAdultIE.PMTC1StartDate = DateGiven(dtPMTCTStartDate.Value);
                //}
                //else if (DDLARVExposure.SelectedItem.Text == "PEP Regimen")
                //{
                //    objAdultIE.PEP1StartDate = DateGiven(dtPEP1StartDate.Value);
                //}

                //else if (DDLARVExposure.SelectedItem.Text == "HAART Regimen")
                //{
                //    objAdultIE.HAART1StartDate = DateGiven(dtHAART1StartDate.Value.ToString());
                //}
                objAdultIE.Impression = txtImpression.Text;

                objAdultIE.HIVRelatedOI = "";
                objAdultIE.NonHIVRelatedOI = "";
                for (int i = 0; i < cblDiagnosis.Items.Count; i++)
                {
                    if (cblDiagnosis.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "Diagnosis";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblDiagnosis.Items[i].Value);
                        if (cblDiagnosis.Items[i].Text == "HIV-Related illness")
                        {
                            objAdultIE.HIVRelatedOI = txtHIVRelatedOI.Text;
                            //listmulti.Add(objAdultIEMultiSelect);
                            //objAdultIE.AdultIEMultiSelect = listmulti;

                            Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                            Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                            Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                            Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                        }
                        if (cblDiagnosis.Items[i].Text == "Non-HIV related illness")
                        {
                            objAdultIE.NonHIVRelatedOI = txtNonHIVRelatedOI.Text;
                            //listmulti.Add(objAdultIEMultiSelect);
                            //objAdultIE.AdultIEMultiSelect = listmulti;
                            Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                            Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                            Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                            Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                        }

                    }
                }
                //WHO Staging
                //objAdultIE.WHOStage = ConvertInt(RBLWHOStaging.SelectedValue.ToString());
                //objAdultIE.WHOStageIConditions = ConvertInt(this.UcWhostaging.rblWHOStageI.SelectedValue.ToString());
                //objAdultIE.WHOStageIIConditions = ConvertInt(this.UcWhostaging.rblWHOStageII.SelectedValue.ToString());
                //objAdultIE.WHOStageIIIConditions = ConvertInt(this.UcWhostaging.rblWHOStageIII.SelectedValue.ToString());
                //objAdultIE.WHOStageIVConditions = ConvertInt(this.UcWhostaging.rblWHOStageIV.SelectedValue.ToString());
                //WHO-I
                //if (this.UcWhostaging.rblWHOStageI.SelectedValue == "1")
                //{
                for (int i = 0; i < this.UcWhostaging.gvWHO1.Rows.Count; i++)
                {
                    Label lblWHOId = (Label)UcWhostaging.gvWHO1.Rows[i].FindControl("lblwho1");
                    CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO1.Rows[i].FindControl("Chkwho1");
                    HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date");
                    HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date1");
                    if (chkWHOId.Checked == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "WHOStageIConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(lblWHOId.Text);
                        if (!String.IsNullOrEmpty(txtWHODate1.Value))
                        {
                            objAdultIEMultiSelect.DateField1 = Convert.ToString(txtWHODate1.Value);
                        }
                        if (!String.IsNullOrEmpty(txtWHODate2.Value))
                        {
                            objAdultIEMultiSelect.DateField2 = Convert.ToString(txtWHODate2.Value);
                        }
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                    }
                }
                //}
                //WHO-II
                //if (this.UcWhostaging.rblWHOStageII.SelectedValue == "1")
                //{
                for (int i = 0; i < this.UcWhostaging.gvWHO2.Rows.Count; i++)
                {
                    Label lblWHOId = (Label)UcWhostaging.gvWHO2.Rows[i].FindControl("lblwho2");
                    CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO2.Rows[i].FindControl("Chkwho2");
                    HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date");
                    HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date1");
                    if (chkWHOId.Checked == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "WHOStageIIConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(lblWHOId.Text);
                        if (!String.IsNullOrEmpty(txtWHODate1.Value))
                        {
                            objAdultIEMultiSelect.DateField1 = Convert.ToString(txtWHODate1.Value);
                        }
                        if (!String.IsNullOrEmpty(txtWHODate2.Value))
                        {
                            objAdultIEMultiSelect.DateField2 = Convert.ToString(txtWHODate2.Value);
                        }
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                    }
                }

                // }
                //WHO-III
                //if (this.UcWhostaging.rblWHOStageIII.SelectedValue == "1")
                //{
                for (int i = 0; i < this.UcWhostaging.gvWHO3.Rows.Count; i++)
                {
                    Label lblWHOId = (Label)UcWhostaging.gvWHO3.Rows[i].FindControl("lblwho3");
                    CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO3.Rows[i].FindControl("Chkwho3");
                    HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date");
                    HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date1");
                    if (chkWHOId.Checked == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "WHOStageIIICoditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(lblWHOId.Text);
                        if (!String.IsNullOrEmpty(txtWHODate1.Value))
                        {
                            objAdultIEMultiSelect.DateField1 = Convert.ToString(txtWHODate1.Value);
                        }
                        if (!String.IsNullOrEmpty(txtWHODate2.Value))
                        {
                            objAdultIEMultiSelect.DateField2 = Convert.ToString(txtWHODate2.Value);
                        }
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                    }
                }
                //}
                //WHO-IV
                //if (this.UcWhostaging.rblWHOStageIV.SelectedValue == "1")
                //{
                for (int i = 0; i < this.UcWhostaging.gvWHO4.Rows.Count; i++)
                {
                    Label lblWHOId = (Label)UcWhostaging.gvWHO4.Rows[i].FindControl("lblwho4");
                    CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO4.Rows[i].FindControl("Chkwho4");
                    HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date");
                    HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date1");
                    if (chkWHOId.Checked == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "WHOStageIVConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(lblWHOId.Text);
                        if (!String.IsNullOrEmpty(txtWHODate1.Value))
                        {
                            objAdultIEMultiSelect.DateField1 = Convert.ToString(txtWHODate1.Value);
                        }
                        if (!String.IsNullOrEmpty(txtWHODate2.Value))
                        {
                            objAdultIEMultiSelect.DateField2 = Convert.ToString(txtWHODate2.Value);
                        }
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                    }
                }
                //}

                //objAdultIE.InitialEvaluation = ConvertInt(rblInitialEvaluation.SelectedValue.ToString());
                //objAdultIE.InitiationWHOstage = rcbSelectedValue(ddlInitiationWHOstage);
                //objAdultIE.WHOStage = rcbSelectedValue(ddlWhoStage);
                //objAdultIE.WABStage = rcbSelectedValue(ddlWABStage);
                //objAdultIE.TannerStaging = rcbSelectedValue(rcbTannerStaging);
                //objAdultIE.Mernarche = ConvertInt(radbtnMernarche.SelectedValue.ToString());
                //objAdultIE.Mernarchedate = DateGiven("");
                //if (radbtnMernarche.SelectedValue == "1")
                //{
                //    objAdultIE.Mernarchedate = DateGiven(txtMenarcheDate.Value);
                //}

                //Management-Drug Allergy and toxicities
                //objAdultIE.DrugAllergyToxicity = 0;
                objAdultIE.SpecifyAntibioticAllery = "";
                objAdultIE.ARVDrugAllergy = "";
                objAdultIE.OtherDrugAllergy = "";
                //for (int i = 0; i < cblDrugAllergiesToxicities.Items.Count; i++)
                //{
                //    if (cblDrugAllergiesToxicities.Items[i].Selected == true)
                //    {
                //        objAdultIEMultiSelect.FieldId = 0;
                //        objAdultIEMultiSelect.FieldName = "DrugAllergiesToxicitiesPaeds";
                //        objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblDrugAllergiesToxicities.Items[i].Value);
                //        //listmulti.Add(objAdultIEMultiSelect);
                //        //objAdultIE.AdultIEMultiSelect = listmulti;

                //        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                //        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  "+VisitIdforMultiSelect+",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                //        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                //        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                //    }
                //}
                objAdultIE.SpecifyAntibioticAllery = "";
                objAdultIE.ARVDrugAllergy = "";
                //objAdultIE.OtherDrugAllergy = txtSpecifyOtherAllery.Text;
                //Management - ARV Side effects
                //objAdultIE.ARVSideEffect = ConvertInt(rblSideEffects.SelectedValue.ToString());
                //objAdultIE.AnyARVSideEffects = ConvertInt(radbtnARVSideEffects.SelectedValue.ToString());
                objAdultIE.WorkUpPlan = "";
                //if (radbtnARVSideEffects.SelectedValue == "1")
                //{
                for (int i = 0; i < cblShortTermEffects.Items.Count; i++)
                {
                    if (cblShortTermEffects.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "ShortTermEffects";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblShortTermEffects.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                    }
                }
                //Short term effects multiselect
                //ShortTerm effects textbox for other
                for (int i = 0; i < cblLongTermEffects.Items.Count; i++)
                {
                    if (cblLongTermEffects.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "LongTermEffects";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblLongTermEffects.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                    }
                }

                //Long term effects multiselect
                //Longterm effects for other
                objAdultIE.WorkUpPlan = txtWorkUpPlan.Text;
                //}
                //Management - Lab Evaluation
                //objAdultIE.LabEvaluation = ConvertInt(radbtnLabEvaluationAdult.SelectedValue);
                //if (radbtnLabEvaluationAdult.SelectedValue == "1")
                //{
                //    //lab evaluation - multiselect
                //    for (int i = 0; i < cblSpecifyLabEvaluation.Items.Count; i++)
                //    {
                //        if (cblSpecifyLabEvaluation.Items[i].Selected == true)
                //        {
                //            objAdultIEMultiSelect.FieldId = 0;
                //            objAdultIEMultiSelect.FieldName = "SpecifyLabEvaluation";
                //            objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblSpecifyLabEvaluation.Items[i].Value);
                //            //listmulti.Add(objAdultIEMultiSelect);
                //            //objAdultIE.AdultIEMultiSelect = listmulti;

                //            Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                //            Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  "+VisitIdforMultiSelect+",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                //            Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                //            Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                //        }
                //    }
                //counselling - counselling
                //for (int i = 0; i < cblCounselling.Items.Count; i++)
                //{
                //    if (cblCounselling.Items[i].Selected == true)
                //    {
                //        objAdultIEMultiSelect.FieldId = 0;
                //        objAdultIEMultiSelect.FieldName = "Counselling";
                //        objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblCounselling.Items[i].Value);
                //        //listmulti.Add(objAdultIEMultiSelect);
                //        //objAdultIE.AdultIEMultiSelect = listmulti;

                //        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                //        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  "+VisitIdforMultiSelect+",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                //        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                //        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                //    }
                //}
                //other counselling
                //}
                //Management - Plan
                //objAdultIE.Plan = ConvertInt(rblPlan.SelectedValue.ToString());
                //objAdultIE.WardAdmission = ConvertInt(radbtnWardAdmission.SelectedValue.ToString());
                //objAdultIE.ReferToSpecialistClinic = txtReferToSpecialistClinic.Text;
                //objAdultIE.TransferOut = txtTransferOut.Text;

                //Management - Treatment
                //objAdultIE.ARTTreatment = ConvertInt(rblTreatment.SelectedValue);
                //objAdultIE.ARTTreatmentPlan = ConvertInt(ddlARTTreatmentPlan.SelectedValue);
                //objAdultIE.StartART = ConvertInt(radbtnStartART.SelectedValue);
                //objAdultIE.SubstituteRegimen = ConvertInt(radbtnSubstituteRegimen.SelectedValue); 
                //objAdultIE.NumberDrugsSubstituted = ConvertInt(rcbNumberDrugsSubstituted.SelectedValue);
                //objAdultIE.StopTreatment = ConvertInt(radbtnStopTreatment.SelectedValue);
                objAdultIE.RegimenPrescribed = 0;
                objAdultIE.OtherRegimenPrescribed = "";
                //if (ddlARTTreatmentPlan.Text == "Substitute Regimen")
                //{
                //    //specify reason for switching regimen
                //}
                //if (radbtnStartART.SelectedValue == "1")
                //{
                //    for (int i = 0; i < cblARTEligibilityCriteria.Items.Count; i++)
                //    {
                //        if (cblARTEligibilityCriteria.Items[i].Selected == true)
                //        {
                //            objAdultIEMultiSelect.FieldId = 0;
                //            objAdultIEMultiSelect.FieldName = "ARTEligibilityCriteria";
                //            objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblARTEligibilityCriteria.Items[i].Value);
                //            //listmulti.Add(objAdultIEMultiSelect);
                //            //objAdultIE.AdultIEMultiSelect = listmulti;

                //            Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                //            Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  "+VisitIdforMultiSelect+",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                //            Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                //            Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                //        }
                //    }
                //    //Eligible through multiselect
                //    //other eligible criteria
                //    //public int ARTEligibilityCriteria { get; set; }
                //}
                //if (radbtnSubstituteRegimen.SelectedValue == "1")
                //{
                //    objAdultIE.NumberDrugsSubstituted = ConvertInt(rcbNumberDrugsSubstituted.SelectedValue);
                //}
                //if (radbtnStopTreatment.SelectedValue == "1")
                //{
                //    //ART stop reason - multiselect
                //    for (int i = 0; i < cblStopTreatmentCodes.Items.Count; i++)
                //    {
                //        if (cblStopTreatmentCodes.Items[i].Selected == true)
                //        {
                //            objAdultIEMultiSelect.FieldId = 0;
                //            objAdultIEMultiSelect.FieldName = "StopTreatmentCodes";
                //            objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblStopTreatmentCodes.Items[i].Value);
                //            //listmulti.Add(objAdultIEMultiSelect);
                //            //objAdultIE.AdultIEMultiSelect = listmulti;

                //            Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                //            Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  "+VisitIdforMultiSelect+",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                //            Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                //            Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                //        }
                //    }
                //    objAdultIE.RegimenPrescribed = ConvertInt(rcbRegimenPrescribed.SelectedValue);
                //}
                //objAdultIE.OtherRegimenPrescribed = txtOtherRegimenPrescribed.Text;
                ////Management-OI Treatment
                ////objAdultIE.OITreatment =ConvertInt(rblOITreatment.SelectedValue);
                //objAdultIE.OIProphylaxis = ConvertInt(rcbOIProphylaxis.SelectedValue);
                //objAdultIE.ReasonCoTrimoxPrescribed = ConvertInt(rcbReasonCTXPrescribed.SelectedValue);
                //objAdultIE.OtherTreatment = "";
                //if (rcbOIProphylaxis.SelectedItem.Text == "Cotrimoxazole")
                //{
                //    objAdultIE.ReasonCoTrimoxPrescribed = ConvertInt(rcbReasonCTXPrescribed.SelectedValue);
                //}
                //objAdultIE.OtherTreatment = txtOtherTreatment.Text;

                //Prev with +Ve-Sexuallity Assessment
                //objAdultIE.SexualAssessment = ConvertInt(rblSexAssessment.SelectedValue);
                //objAdultIE.SexualActiveness = ConvertInt(radbtnSexualActiveness.SelectedValue);
                //objAdultIE.SexualOrientation = 0;
                //objAdultIE.KnowSexualPartnerHIVStatus = ConvertInt(radbtnKnowSexualPartnerHIVStatus.SelectedValue);
                //objAdultIE.PartnerHIVStatus = 0;
                //if (radbtnSexualActiveness.SelectedValue == "1")
                //{
                //    for (int i = 0; i < cblHighRisk.Items.Count; i++)
                //    {
                //        if (cblHighRisk.Items[i].Selected == true)
                //        {
                //            objAdultIEMultiSelect.FieldId = 0;
                //            objAdultIEMultiSelect.FieldName = "HighRisk";
                //            objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblHighRisk.Items[i].Value);
                //            //listmulti.Add(objAdultIEMultiSelect);
                //            //objAdultIE.AdultIEMultiSelect = listmulti;

                //            Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                //            Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  "+VisitIdforMultiSelect+",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                //            Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                //            Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                //        }
                //    }
                //    objAdultIE.SexualOrientation = ConvertInt(rcbSexualOrientation.SelectedValue);
                //}
                //objAdultIE.KnowSexualPartnerHIVStatus = ConvertInt(radbtnKnowSexualPartnerHIVStatus.SelectedValue);
                //objAdultIE.PartnerHIVStatus = ConvertInt(rcbPartnerHIVStatus.SelectedValue);
                ////Prev with +Ve - PWP Interventions
                //objAdultIE.GivenPWPMessages = ConvertInt(radbtnGivenPWPMessages.SelectedValue);
                //objAdultIE.SaferSexImportanceExplained = ConvertInt(radbtnSaferSexImportanceExplained.SelectedValue.ToString());

                //objAdultIE.UnsafeSexImportanceExplained = ConvertInt(radbtnLMP.SelectedValue.ToString());
                //objAdultIE.LMPDate = DateGiven("");
                //objAdultIE.LMPNotaccessedReason = 0;

                //objAdultIE.PDTDone = ConvertInt(radbtnPDTDone.SelectedValue.ToString());
                //objAdultIE.ClientPregnant = ConvertInt("0");

                //objAdultIE.PMTCTOffered = ConvertInt(radbtnPMTCTOffered.SelectedValue.ToString());
                //objAdultIE.IntentionOfPregnancy = ConvertInt(radbtnIntentionOfPregnancy.SelectedValue.ToString());
                //objAdultIE.DiscussedFertilityOptions = ConvertInt("0");
                //objAdultIE.DiscussedDualContraception = ConvertInt("0");
                //objAdultIE.CondomsIssued = ConvertInt(radbtnCondomsIssued.SelectedValue.ToString());
                //objAdultIE.STIScreened = ConvertInt(radbtnSTIScreened.SelectedValue.ToString());

                //objAdultIE.PWPinterventions = ConvertInt(rcbPartnerHIVStatus.SelectedValue);
                //objAdultIE.ReasonCondomNotIssued = "";
                //objAdultIE.VaginalDischarge =  0;
                //objAdultIE.UrethralDischarge = 0;
                //objAdultIE.GenitalUlceration = 0;
                //objAdultIE.STITreatmentPlan = "";
                //objAdultIE.FPMethod = 0;
                //objAdultIE.CervicalCancerScreened = ConvertInt(radbtnCervicalCancerScreened.SelectedValue.ToString());
                //objAdultIE.CervicalCancerScreeningResults = 0;
                //objAdultIE.OfferedHPVVaccine = 0;
                //objAdultIE.HPVDoseDate = DateGiven("");
                //objAdultIE.RefferedToFupF = 0;
                //objAdultIE.SpecifyOtherRefferedTo = "";

                //if (radbtnPDTDone.SelectedValue == "1")
                //{
                //    objAdultIE.ClientPregnant = ConvertInt(radbtnPregnant.SelectedValue);
                //}
                //if (radbtnLMP.SelectedValue == "1")
                //{
                //    objAdultIE.LMPDate = DateGiven(txtLMPDate.Value);
                //}
                //else if (radbtnLMP.SelectedValue == "0")
                //{
                //    objAdultIE.LMPNotaccessedReason = ConvertInt(DDLReasonLMP.SelectedValue);
                //}
                //if (radbtnIntentionOfPregnancy.SelectedValue == "1")
                //{
                //    objAdultIE.DiscussedFertilityOptions = ConvertInt(radbtnDiscussedFertilityOptions.SelectedValue.ToString());
                //}
                //if (radbtnIntentionOfPregnancy.SelectedValue == "0")
                //{
                //    objAdultIE.DiscussedDualContraception = ConvertInt(radbtnDiscussedDualContraception.SelectedValue.ToString());
                //}
                //if (radbtnCondomsIssued.SelectedValue == "0")
                //{
                //    objAdultIE.ReasonCondomNotIssued = txtCondomNotIssued.Text;
                //}

                //if (radbtnSTIScreened.SelectedValue == "1")
                //{
                //    objAdultIE.VaginalDischarge = this.chkUrethralDischarge.Checked == true ? 1 : 0;
                //    objAdultIE.UrethralDischarge = this.chkGenitalUlceration.Checked == true ? 1 : 0;
                //    objAdultIE.GenitalUlceration = this.chkVaginalDischarge.Checked == true ? 1 : 0;
                //    objAdultIE.STITreatmentPlan = txtSTITreatmentPlan.Text;
                //}
                //objAdultIE.OnFP = ConvertInt(radbtnOnFP.SelectedValue);
                //if (radbtnOnFP.SelectedValue == "1")
                //{
                //    objAdultIE.FPMethod = ConvertInt(rcbFPMethod.SelectedValue.ToString());
                //}

                //if (radbtnCervicalCancerScreened.SelectedValue == "1")
                //{
                //    objAdultIE.CervicalCancerScreeningResults = rcbSelectedValue(rcbCervicalCancerScreeningResults);
                //}
                //if (radbtnCervicalCancerScreened.SelectedValue == "0")
                //{
                //    objAdultIE.CervicalCancerScreened = ConvertInt(radbtnCervicalCancerScreened.SelectedValue.ToString());
                //}
                //objAdultIE.HPVOffered = ConvertInt(radbtnHPVOffered.SelectedValue.ToString());
                //if (radbtnHPVOffered.SelectedValue == "1")
                //{
                //    objAdultIE.OfferedHPVVaccine = rcbSelectedValue(rcbOfferedHPVVaccine);
                //    objAdultIE.HPVDoseDate = DateGiven(dtHPVDoseDate.Value.ToString());
                //}
                //objAdultIE.RefferedToFupF = rcbSelectedValue(rcbRefferedToFupF);
                //objAdultIE.SpecifyOtherRefferedTo = txtSpecifyOtherRefferedTo.Text;
                list.Add(objAdultIE);
                IKNHAdultIE theExpressManager;
                theExpressManager = (IKNHAdultIE)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAdultIE, BusinessProcess.Clinical");
                DataTable DTresult = theExpressManager.SaveAdultIE(list, Insertcbl);
                if (DTresult.Rows.Count > 0)
                {
                    //SaveCancel();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "saveSuc", "alert('Tab Data saved successfully');", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "goBack", "BackBtnClick();", true);
                }
            }
            catch (Exception ex)
            {
                isError = true;
                error = ex.Message.ToString();
            }
            if (isError)
            {

                ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "saveFail", "alert('" + error + "')", true);
                ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clsLoadingPanelDueToError", "parent.CloseLoading();", true);
            }
        }
        private void SaveCancel(string tabname)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            IQCareMsgBox.NotifyAction(tabname + " Tab saved successfully.", "Adult Initial Evaluation Form", true, this, "");
            //MsgBuilder totalMsgBuilder = new MsgBuilder();
            //totalMsgBuilder.DataElements["MessageText"] = tabname + " Tab saved successfully.";
            //IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
        }

        private Boolean fieldValidation(string TabName)
        {
            IQCareUtils theUtil = new IQCareUtils();
            MsgBuilder totalMsgBuilder = new MsgBuilder();
            ShowHide();
            if (txtVisitDate.Value.Trim() == "")
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValVisitDate", "alert('Enter Visit Date')", true);
                totalMsgBuilder.DataElements["MessageText"] = "Enter Visit Date";
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                return false;
            }
            int count = 0;
            if (TabName == "btnSaveTriage" || TabName == "btnSubmitTriage")
            {
                if (rblChildAccompaniedBy.SelectedItem == null)
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValPRef", "alert('Select Patient referred from')", true);
                    totalMsgBuilder.DataElements["MessageText"] = "Select Patient accompanied by caregiver";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    lblcaregiver.ForeColor = Color.Red;
                    lblheadTriage.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    lblcaregiver.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadTriage.ForeColor = Color.FromArgb(0, 0, 142);
                }
                if (this.idVitalSign.txtHeight.Text == "")
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValHeight", "alert('Enter Height')", true);
                    totalMsgBuilder.DataElements["MessageText"] = "Enter Height";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    idVitalSign.lblHeight.ForeColor = Color.Red;
                    lblVitalSigns.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    idVitalSign.lblHeight.ForeColor = Color.FromArgb(0, 0, 142);
                    lblVitalSigns.ForeColor = Color.FromArgb(0, 0, 142);
                }
                if (this.idVitalSign.txtWeight.Text == "")
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValWeight", "alert('Enter Weight')", true);
                    totalMsgBuilder.DataElements["MessageText"] = "Enter Weight";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    idVitalSign.lblWeight.ForeColor = Color.Red;
                    lblVitalSigns.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    idVitalSign.lblWeight.ForeColor = Color.FromArgb(0, 0, 142);
                    lblVitalSigns.ForeColor = Color.FromArgb(0, 0, 142);
                }
                if (this.idVitalSign.txtBPSystolic.Text == "")
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Enter Systolic Blood pressure";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    idVitalSign.lblBP.ForeColor = Color.Red;
                    lblVitalSigns.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    idVitalSign.lblBP.ForeColor = Color.FromArgb(0, 0, 142);
                    lblVitalSigns.ForeColor = Color.FromArgb(0, 0, 142);
                }
                if (this.idVitalSign.txtBPDiastolic.Text == "")
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Enter Diastolic Blood pressure";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    idVitalSign.lblBP.ForeColor = Color.Red;
                    lblVitalSigns.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    idVitalSign.lblBP.ForeColor = Color.FromArgb(0, 0, 142);
                    lblVitalSigns.ForeColor = Color.FromArgb(0, 0, 142);
                }

                if (ddlPatientReferred.SelectedValue == "0")
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValPRef", "alert('Select Patient referred from')", true);
                    totalMsgBuilder.DataElements["MessageText"] = "Select Patient referred from";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    lblPatRef.ForeColor = Color.Red;
                    lblheadTriage.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    lblPatRef.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadTriage.ForeColor = Color.FromArgb(0, 0, 142);
                }
            }
            else if (TabName == "Clinical History" || tabControl.ActiveTabIndex == 1)
            {
                count = 0;
                GridView gvPresentingComplaints = (GridView)UcPc.FindControl("gvPresentingComplaints");
                foreach (GridViewRow row in gvPresentingComplaints.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("ChkPresenting");
                    if (chk.Checked == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCC", "alert('Select Presenting Complaints - Clinical History')", true);
                    totalMsgBuilder.DataElements["MessageText"] = "Select Presenting Complaints - Clinical History";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPc.gvPresentingComplaints.HeaderStyle.ForeColor = Color.Red;
                    lblPresComp.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    UcPc.gvPresentingComplaints.HeaderStyle.ForeColor = Color.FromArgb(0, 0, 142);
                    lblPresComp.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < cblPreExistingMedConditions.Items.Count; i++)
                {
                    if (cblPreExistingMedConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Long Term Medication";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    lblchlongtermmedication.ForeColor = Color.Red;
                    lblheadMedicalCond.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValLTM", "alert('Select Long Term Medication')", true);
                    return false;
                }
                else
                {
                    lblchlongtermmedication.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadMedicalCond.ForeColor = Color.FromArgb(0, 0, 142);
                }
            }
            else if (TabName == "btnSaveExam" || TabName == "btnSubmitExam")
            {
                //if (ddlWhoStage.SelectedValue == "0")
                //{
                //    //totalMsgBuilder.DataElements["MessageText"] = "Select Current WHO Stage";
                //    //IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValWHO", "alert('Select Current WHO Stage')", true);
                //    return false;
                //}

                for (int i = 0; i < chkLTMedications.Items.Count; i++)
                {
                    if (chkLTMedications.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Long Term Medication";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    lblchkLTMedications.ForeColor = Color.Red;
                    lblCurrentLongTermMedications.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValLTM", "alert('Select Long Term Medication')", true);
                    return false;
                }
                else
                {
                    lblchkLTMedications.ForeColor = Color.FromArgb(0, 0, 142);
                    lblCurrentLongTermMedications.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPE.cblGeneralConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblGeneralConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select General Condition";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPE.lblGeneral.ForeColor = Color.Red;
                    lblPhysicalExamination.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValGC", "alert('Select General Condition')", true);
                    return false;
                }
                else
                {
                    UcPE.lblGeneral.ForeColor = Color.FromArgb(0, 0, 142);
                    lblPhysicalExamination.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPE.cblCardiovascularConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblCardiovascularConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Cardiovascular Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPE.lblCardiovarscular.ForeColor = Color.Red;
                    lblPhysicalExamination.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCC", "alert('Select Cardiovascular Conditions')", true);
                    return false;
                }
                else
                {
                    UcPE.lblCardiovarscular.ForeColor = Color.FromArgb(0, 0, 142);
                    lblPhysicalExamination.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPE.cblOralCavityConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblOralCavityConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Oral Cavity Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPE.lblOralCavity.ForeColor = Color.Red;
                    lblPhysicalExamination.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValOC", "alert('Select Oral Cavity Conditions')", true);
                    return false;
                }
                else
                {
                    UcPE.lblOralCavity.ForeColor = Color.FromArgb(0, 0, 142);
                    lblPhysicalExamination.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPE.cblGenitalUrinaryConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblGenitalUrinaryConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select GenitalUrinary Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPE.lblGenitourinary.ForeColor = Color.Red;
                    lblPhysicalExamination.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValGU", "alert('Select GenitalUrinary Conditions')", true);
                    return false;
                }
                else
                {
                    UcPE.lblGenitourinary.ForeColor = Color.FromArgb(0, 0, 142);
                    lblPhysicalExamination.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPE.cblCNSConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblCNSConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select CNS Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPE.lblSkin.ForeColor = Color.Red;
                    lblPhysicalExamination.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCNS", "alert('Select CNS Conditions')", true);
                    return false;
                }
                else
                {
                    UcPE.lblSkin.ForeColor = Color.FromArgb(0, 0, 142);
                    lblPhysicalExamination.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPE.cblChestLungsConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblChestLungsConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select ChestLung Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPE.lblChest.ForeColor = Color.Red;
                    lblPhysicalExamination.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValChest", "alert('Select ChestLung Conditions')", true);
                    return false;
                }
                else
                {
                    UcPE.lblChest.ForeColor = Color.FromArgb(0, 0, 142);
                    lblPhysicalExamination.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPE.cblSkinConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblSkinConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Skin Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPE.lblSkin.ForeColor = Color.Red;
                    lblPhysicalExamination.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValSkin", "alert('Select Skin Conditions')", true);
                    return false;
                }
                else
                {
                    UcPE.lblSkin.ForeColor = Color.FromArgb(0, 0, 142);
                    lblPhysicalExamination.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPE.cblAbdomenConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblAbdomenConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Abdomen Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPE.lblAbdomen.ForeColor = Color.Red;
                    lblPhysicalExamination.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValAbdomen", "alert('Select Abdomen Conditions')", true);
                    return false;
                }
                else
                {
                    UcPE.lblAbdomen.ForeColor = Color.FromArgb(0, 0, 142);
                    lblPhysicalExamination.ForeColor = Color.FromArgb(0, 0, 142);
                }
                if (UcWhostaging.ddlwhostage1.SelectedIndex == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select WHO Stage";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcWhostaging.lblWHOStage.ForeColor = Color.Red;
                    lblheadWHOStage.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    UcWhostaging.lblWHOStage.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadWHOStage.ForeColor = Color.FromArgb(0, 0, 142);
                }
                if (UcWhostaging.ddlWABStage.SelectedIndex == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select WAB Stage";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcWhostaging.lblWABStage.ForeColor = Color.Red;
                    lblheadWHOStage.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    UcWhostaging.lblWABStage.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadWHOStage.ForeColor = Color.FromArgb(0, 0, 142);
                }
            }
            else if (TabName == "btnSaveManagement" || TabName == "btnSubmitManagement")
            {
                if (this.UCPharmacy.ddlTreatmentplan.SelectedIndex == 0)
                {

                    totalMsgBuilder.DataElements["MessageText"] = "Select Treatment Plan";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UCPharmacy.lblTreatmentplan.ForeColor = Color.Red;
                    lblheadregimenpresc.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    UCPharmacy.lblTreatmentplan.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadregimenpresc.ForeColor = Color.FromArgb(0, 0, 142);
                }

            }
            return true;

        }
        public void ShowHideBusinessRules()
        {

            string script = string.Empty;
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'progressioninwhoshowhide'>\n";
            script += "ShowHide('divprogressioninwhoshowhide','hide');\n";
            script += "</script>\n";
            RegisterStartupScript("progressioninwhoshowhide", script);

            if (Convert.ToDecimal(Session["PatientAge"]) >= 15 && Convert.ToDecimal(Session["PatientAge"]) < 19)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'trSpecifyreasony'>\n";
                script += "ShowHide('trSpecifyreason','hide');\n";
                script += "ShowHide('trstatus','hide');\n";
                script += "</script>\n";
                RegisterStartupScript("trSpecifyreasony", script);
            }

            if (Session["PatientSex"].ToString() == "Female")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divlmpassessedy'>\n";
                script += "ShowHide('divlmpassessed','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divlmpassessedy", script);
            }
            if (Session["PatientSex"].ToString() == "Female")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divshowhideeddy'>\n";
                script += "ShowHide('divshowhideedd','show');ShowHide('divscreenedcc','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divshowhideeddy", script);
            }




        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveForm();

        }
        protected void SaveTraigeTab()
        {
            List<BIQAdultIE> list = new List<BIQAdultIE>();
            List<AdultIEMultiselect> listmulti = new List<AdultIEMultiselect>();
            BIQAdultIE objAdultIE = new BIQAdultIE();
            AdultIEMultiselect objAdultIEMultiSelect = new AdultIEMultiselect();
            StringBuilder Insertcbl = new StringBuilder();
            string error = "";
            try
            {
                objAdultIE.ID = 0;
                objAdultIE.AdultIEFeatureID = GblIQCare.FormId;
                objAdultIE.PtnPk = Convert.ToInt32(Session["PatientId"]);
                string VisitIdforMultiSelect = "IDENT_CURRENT('ord_visit')";
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    VisitIdforMultiSelect = Session["PatientVisitId"].ToString();
                    objAdultIE.VisitPk = Convert.ToInt32(Session["PatientVisitId"]);
                    objAdultIE.LocationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                else
                {
                    objAdultIE.VisitPk = Convert.ToInt32(Session["PatientVisitId"]);
                    objAdultIE.LocationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                //objAdultIE.LocationId = Int32.Parse(Session["AppLocationId"].ToString());
                objAdultIE.UserId = Int32.Parse(Session["AppUserId"].ToString());
                objAdultIE.VisitDate = string.Format("{0:dd-MMM-yyyy}", txtVisitDate.Value);
                //HIV Care and Support Evaluation
                objAdultIE.DiagnosisConfirmed = this.rblDiagnosisYesNo.SelectedValue == "1" ? 1 : 0;
                objAdultIE.ConfirmHIVPosDate = string.Format("{0:dd-MMM-yyyy}", txtdtConfirmHIVPosDate.Value);
                objAdultIE.ChildAccompaniedByCaregiver = this.rblChildAccompaniedBy.SelectedValue == "1" ? 1 : 0;
                objAdultIE.TreatmentSupporterRelationship = ConvertInt(this.ddlTreatmentSupporterRelationship.SelectedValue);
                objAdultIE.DisclosureStatus = ConvertInt(this.ddlDisclosureStatus.SelectedValue);
                //objAdultIE.reasonnotdisclosed = "";
                if (ddlDisclosureStatus.SelectedItem.Text == "Not ready")
                { objAdultIE.reasonnotdisclosed = ConvertInt(ddreasondisclosed.SelectedValue); }
                objAdultIE.HealthEducation = this.rdoHealthEducation.SelectedValue == "1" ? 1 : 0;
                //objAdultIE.HealthEducation = ConvertInt(rdoHealthEducation.SelectedValue);

                objAdultIE.otherdisclosurestatus = "";
                if (ddreasondisclosed.SelectedItem.Text == "Other")
                { objAdultIE.otherdisclosurestatus = txtotherdisclosurestatus.Text; }
                objAdultIE.SchoolingStatus = ConvertInt(this.ddlSchoolingStatus.SelectedValue);
                objAdultIE.Highestlevelattained = 0;
                if (this.ddlSchoolingStatus.SelectedItem.Text == "Enrolled")
                { objAdultIE.Highestlevelattained = ConvertInt(ddHighestlevelattained.SelectedValue); }

                //objAdultIE.HIVSupportgroup = ConvertInt(rblHIVSupportgroup.SelectedValue) ;
                objAdultIE.HIVSupportgroup = this.rblHIVSupportgroup.SelectedValue == "1" ? 1 : 0;
                objAdultIE.supportgroupmembership = "";
                if (rblHIVSupportgroup.SelectedValue == "1")
                { objAdultIE.supportgroupmembership = txtHIVSupportGroup.Text; }
                objAdultIE.PatientReferredFrom = ConvertInt(ddlPatientReferred.SelectedValue);
                objAdultIE.OtherPatientReferredFrom = txtPatRefother.Text;
                objAdultIE.NursesComments = idVitalSign.txtnursescomments.Text;

                // Modal Vital Sign
                objAdultIE.Temperature = GettxtValue(this.idVitalSign.txtTemp.Text.ToString());
                objAdultIE.RespirationRate = GettxtValue(this.idVitalSign.txtRR.Text.ToString());
                objAdultIE.HeartRate = GettxtValue(this.idVitalSign.txtHR.Text.ToString());
                objAdultIE.SystolicBloodPressure = GettxtValue(this.idVitalSign.txtBPSystolic.Text.ToString());
                objAdultIE.DiastolicBloodPressure = GettxtValue(this.idVitalSign.txtBPDiastolic.Text.ToString());
                objAdultIE.Height = GettxtValue(idVitalSign.txtHeight.Text.ToString());
                objAdultIE.Weight = GettxtValue(idVitalSign.txtWeight.Text.ToString());
                objAdultIE.txtheadcircumference = GettxtValue(idVitalSign.txtheadcircumference.Text.ToString());
                //objAdultIE.ddlweightforage = idVitalSign.lblWA.Text == "" ? 0 : ConvertInt(idVitalSign.lblWA.Text);
                //objAdultIE.txtweightforheight = GettxtValue(idVitalSign.lblWH.Text.ToString());
                objAdultIE.ReferToSpecialistClinic = idVitalSign.txtReferToSpecialistClinic.Text;
                list.Add(objAdultIE);
                Insertcbl = new StringBuilder();
                for (int i = 0; i < idVitalSign.cblReferredTo.Items.Count; i++)
                {
                    if (idVitalSign.cblReferredTo.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "RefferedToFUpF";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(idVitalSign.cblReferredTo.Items[i].Value);
                        if (Convert.ToString(idVitalSign.cblReferredTo.Items[i].Text) == "Other Specialist Clinic")
                        {
                            objAdultIEMultiSelect.Notes = idVitalSign.txtReferToSpecialistClinic.Text;
                        }
                        else if (Convert.ToString(idVitalSign.cblReferredTo.Items[i].Text) == "Other (Specify)")
                        {
                            objAdultIEMultiSelect.Notes = idVitalSign.txtSpecifyOtherRefferedTo.Text;
                        }
                        //objAdultIEMultiSelect.Notes = idVitalSign.txtSpecifyOtherRefferedTo.Text;
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ", " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                    }
                }
                IKNHAdultIE theExpressManager;
                theExpressManager = (IKNHAdultIE)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAdultIE, BusinessProcess.Clinical");
                DataTable DTresult = theExpressManager.SaveAdultIE_TriageTab(list, Insertcbl);
                if (DTresult.Rows.Count > 0)
                {
                    Session["PatientVisitId"] = DTresult.Rows[0]["Visit_ID"].ToString();
                    SaveCancel("Triage");
                    //GetDataValue();
                    checkIfPreviuosTabSaved();
                    tabControl.ActiveTabIndex = 1;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "saveSuc", "alert('Triage Tab Data saved successfully')", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "goBack", "BackBtnClick();", true);
                }
            }
            catch (Exception ex)
            {
                isError = true;
                error = ex.Message.ToString();
            }


        }
        protected void SaveClinicalHistoryTab()
        {
            List<BIQAdultIE> list = new List<BIQAdultIE>();
            List<AdultIEMultiselect> listmulti = new List<AdultIEMultiselect>();
            BIQAdultIE objAdultIE = new BIQAdultIE();
            AdultIEMultiselect objAdultIEMultiSelect = new AdultIEMultiselect();
            StringBuilder Insertcbl = new StringBuilder();
            string error = "";
            try
            {
                objAdultIE.ID = 0;
                objAdultIE.AdultIEFeatureID = GblIQCare.FormId;
                objAdultIE.PtnPk = Convert.ToInt32(Session["PatientId"]);
                string VisitIdforMultiSelect = "IDENT_CURRENT('ord_visit')";
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    VisitIdforMultiSelect = Session["PatientVisitId"].ToString();
                    objAdultIE.VisitPk = Convert.ToInt32(Session["PatientVisitId"]);
                    objAdultIE.LocationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                else
                {

                    objAdultIE.VisitPk = Convert.ToInt32(Session["PatientVisitId"]);
                    objAdultIE.LocationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                //objAdultIE.LocationId = Int32.Parse(Session["AppLocationId"].ToString());
                objAdultIE.UserId = Int32.Parse(Session["AppUserId"].ToString());
                objAdultIE.VisitDate = string.Format("{0:dd-MMM-yyyy}", txtVisitDate.Value);
                objAdultIE.PresentingComplaints = 0;
                //StringBuilder Insertcbl = new StringBuilder();
                //if (radbtnPresentingYesNO.SelectedValue == "1")
                //{
                //objAdultIE.PresentingComplaints = ConvertInt(radbtnPresentingYesNO.SelectedValue);
                for (int i = 0; i < UcPc.gvPresentingComplaints.Rows.Count; i++)
                {
                    Label lblPComplaintsId = (Label)UcPc.gvPresentingComplaints.Rows[i].FindControl("lblPresenting");
                    CheckBox chkPComplaints = (CheckBox)UcPc.gvPresentingComplaints.Rows[i].FindControl("ChkPresenting");
                    TextBox txtPComplaints = (TextBox)UcPc.gvPresentingComplaints.Rows[i].FindControl("txtPresenting");
                    if (chkPComplaints.Checked == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "PresentingComplaints";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(lblPComplaintsId.Text);
                        objAdultIEMultiSelect.Notes = "";
                        if (!String.IsNullOrEmpty(txtPComplaints.Text))
                        {
                            objAdultIEMultiSelect.NumericField = Convert.ToInt32(txtPComplaints.Text);
                        }


                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                        // listmulti.Add(objAdultIEMultiSelect);
                        // objAdultIE.AdultIEMultiSelect = listmulti;
                    }
                }
                for (int i = 0; i < cblPreExistingMedConditions.Items.Count; i++)
                {
                    if (cblPreExistingMedConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "SpecificMedicalCondition";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblPreExistingMedConditions.Items[i].Value);
                        if (Convert.ToString(cblPreExistingMedConditions.Items[i].Text) == "Other medical conditions")
                        {
                            objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblPreExistingMedConditions.Items[i].Value);
                            objAdultIEMultiSelect.Notes = txtOthermedicalconditions.Text;
                        }
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ", " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                    }
                }
                //}
                objAdultIE.otherspecifiedcomplaints = this.UcPc.txtOtherPresentingComplaints.Text;
                objAdultIE.Additionalcomplaints = this.UcPc.txtAdditionalComplaints.Text;
                objAdultIE.RespiratoryDiseaseName = "";
                objAdultIE.RespiratoryDiseaseDate = DateGiven("");
                objAdultIE.RespiratoryDiseaseTreatment = "";
                objAdultIE.CardiovascularDiseaseName = "";
                objAdultIE.CardiovascularDiseaseDate = DateGiven("");
                objAdultIE.CardiovascularDiseaseTreatment = "";
                objAdultIE.GastroIntestinalDiseaseName = "";
                objAdultIE.GastroIntestinalDiseaseDate = DateGiven("");
                objAdultIE.GastroIntestinalDiseaseTreatment = "";
                objAdultIE.NervousDiseaseName = "";
                objAdultIE.NervousDiseaseDate = DateGiven("");
                objAdultIE.NervousDiseaseTreatment = "";
                objAdultIE.DermatologyDiseaseName = "";
                objAdultIE.DermatologyDiseaseDate = DateGiven("");
                objAdultIE.DermatologyDiseaseTreatment = "";
                objAdultIE.MusculoskeletalDiseaseName = "";
                objAdultIE.MusculoskeletalDiseaseDate = DateGiven("");
                objAdultIE.MusculoskeletalDiseaseTreatment = "";
                objAdultIE.PsychiatricDiseaseName = "";
                objAdultIE.PsychiatricDiseaseDate = DateGiven("");
                objAdultIE.PsychiatricDiseaseTreatment = "";
                objAdultIE.HematologicalDiseaseName = "";
                objAdultIE.HematologicalDiseaseDate = DateGiven("");
                objAdultIE.HematologicalDiseaseTreatment = "";
                objAdultIE.GenitalUrinaryDiseaseName = "";
                objAdultIE.GenitalUrinaryDiseaseDate = DateGiven("");
                objAdultIE.GenitalUrinaryDiseaseTreatment = "";
                objAdultIE.OphthamologyDiseaseName = "";
                objAdultIE.OphthamologyDiseaseDate = DateGiven("");
                objAdultIE.OphthamologyDiseaseTreatment = "";
                objAdultIE.ENTDiseaseName = "";
                objAdultIE.ENTDiseaseDate = DateGiven("");
                objAdultIE.ENTDiseaseTreatment = "";
                //Other Medical History
                //objAdultIE.OtherMedicalHistory = ConvertInt(radbtnMedicalYesNO.SelectedValue);
                objAdultIE.LMPassessmentValid = 0;
                objAdultIE.OtherDiseaseName = "";
                objAdultIE.OtherDiseaseDate = DateGiven("");
                objAdultIE.OtherDiseaseTreatment = "";
                objAdultIE.SchoolPerfomance = 0;
                objAdultIE.MedHistoryFP = ConvertInt(rblFP.SelectedValue);
                objAdultIE.MedHistoryLastFP = "";
                if (rblFP.SelectedValue == "1")
                {
                    objAdultIE.MedHistoryLastFP = Convert.ToString(txtLastFP.Text);

                }
                list.Add(objAdultIE);
                IKNHAdultIE theExpressManager;
                theExpressManager = (IKNHAdultIE)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAdultIE, BusinessProcess.Clinical");
                DataTable DTresult = theExpressManager.SaveAdultIE_CATab(list, Insertcbl);
                if (DTresult.Rows.Count > 0)
                {
                    SaveCancel("Clinical History");
                    //GetDataValue();
                    checkIfPreviuosTabSaved();
                    tabControl.ActiveTabIndex = 2;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "saveSuc", "alert('Clinical History Tab Data saved successfully')", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "goBack", "BackBtnClick();", true);
                }
            }
            catch (Exception ex)
            {
                isError = true;
                error = ex.Message.ToString();
            }

        }
        protected void SaveExaminationTab()
        {
            List<BIQAdultIE> list = new List<BIQAdultIE>();
            List<AdultIEMultiselect> listmulti = new List<AdultIEMultiselect>();
            BIQAdultIE objAdultIE = new BIQAdultIE();
            AdultIEMultiselect objAdultIEMultiSelect = new AdultIEMultiselect();
            StringBuilder Insertcbl = new StringBuilder();
            string error = "";
            try
            {
                objAdultIE.ID = 0;
                objAdultIE.AdultIEFeatureID = GblIQCare.FormId;
                objAdultIE.PtnPk = Convert.ToInt32(Session["PatientId"]);
                string VisitIdforMultiSelect = "IDENT_CURRENT('ord_visit')";
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    VisitIdforMultiSelect = Session["PatientVisitId"].ToString();
                    objAdultIE.VisitPk = Convert.ToInt32(Session["PatientVisitId"]);
                    objAdultIE.LocationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                else
                {

                    objAdultIE.VisitPk = Convert.ToInt32(Session["PatientVisitId"]);
                    objAdultIE.LocationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                //objAdultIE.LocationId = Int32.Parse(Session["AppLocationId"].ToString());
                objAdultIE.UserId = Int32.Parse(Session["AppUserId"].ToString());
                objAdultIE.VisitDate = string.Format("{0:dd-MMM-yyyy}", txtVisitDate.Value);
                for (int i = 0; i < chkLTMedications.Items.Count; i++)
                {
                    if (chkLTMedications.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "LongTermMedications";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(chkLTMedications.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ", " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                        Insertcbl.Append(" END ");
                    }
                }
                objAdultIE.OtherLongTermMedications = txOtherLongTermMedications.Text;

                //Physical examination
                //objAdultIE.Physicalexamination = ConvertInt("");
                objAdultIE.Additionalmedicalconditionnotes = this.UcPE.txtOtherMedicalConditionNotes.Text;
                objAdultIE.OtherGeneralConditions = this.UcPE.txtOtherGeneralConditions.Text;
                for (int i = 0; i < this.UcPE.cblGeneralConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblGeneralConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "GeneralConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblGeneralConditions.Items[i].Value);

                        Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                        Insertcbl.Append(" END ");
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                    }
                }
                objAdultIE.OtherCardiovascularConditions = this.UcPE.txtOtherCardiovascularConditions.Text;
                for (int i = 0; i < this.UcPE.cblCardiovascularConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblCardiovascularConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "CardiovascularConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblCardiovascularConditions.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                        Insertcbl.Append(" END ");
                    }
                }
                objAdultIE.OtherOralCavityConditions = this.UcPE.txtOtherOralCavityConditions.Text;
                for (int i = 0; i < this.UcPE.cblOralCavityConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblOralCavityConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "OralCavityConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblOralCavityConditions.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                        Insertcbl.Append(" END ");
                    }
                }
                objAdultIE.OtherGenitourinaryConditions = this.UcPE.txtOtherGenitourinaryConditions.Text;
                for (int i = 0; i < this.UcPE.cblGenitalUrinaryConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblGenitalUrinaryConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "GenitalUrinaryConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblGenitalUrinaryConditions.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                        Insertcbl.Append(" END ");
                    }
                }
                objAdultIE.OtherCNSConditions = this.UcPE.txtOtherCNSConditions.Text;
                for (int i = 0; i < this.UcPE.cblCNSConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblCNSConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "CNSConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblCNSConditions.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                        Insertcbl.Append(" END ");
                    }
                }
                objAdultIE.OtherChestLungsConditions = this.UcPE.txtOtherChestLungsConditions.Text;
                for (int i = 0; i < this.UcPE.cblChestLungsConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblChestLungsConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "ChestLungsConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblChestLungsConditions.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                        Insertcbl.Append(" END ");
                    }
                }
                objAdultIE.OtherSkinConditions = this.UcPE.txtOtherSkinConditions.Text;
                for (int i = 0; i < this.UcPE.cblSkinConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblSkinConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "SkinConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblSkinConditions.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                        Insertcbl.Append(" END ");
                    }
                }
                objAdultIE.OtherAbdomenConditions = this.UcPE.txtOtherAbdomenConditions.Text;
                for (int i = 0; i < this.UcPE.cblAbdomenConditions.Items.Count; i++)
                {
                    if (this.UcPE.cblAbdomenConditions.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "AbdomenConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(this.UcPE.cblAbdomenConditions.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIEMultiSelect.Notes + "')");
                        Insertcbl.Append(" END ");
                    }
                }



                objAdultIE.InitialCD4 = GettxtValue(txtInitialCD4.Text);
                objAdultIE.InitialCD4Percent = GettxtValue(txtInitialCD4Percent.Text);
                objAdultIE.InitialCD4Date = DateGiven(dtInitialCD4Date.Value.ToString());
                objAdultIE.HighestCD4Ever = GettxtValue(txtHighestCD4Ever.Text);
                objAdultIE.HighestCD4Percent = GettxtValue(txtHighestCD4Percent.Text);
                objAdultIE.HighestCD4EverDate = DateGiven(dtHighestCD4Date.Value.ToString());
                objAdultIE.CD4atARTInitiation = GettxtValue(txtCD4atARTinitiation.Text);
                objAdultIE.CD4AtARTInitiationPercent = GettxtValue(txtCD4PercentAtARTInitiation.Text);
                objAdultIE.CD4atARTInitiationDate = DateGiven(dtCD4atARTinitiationDate.Value.ToString());
                objAdultIE.MostRecentCD4 = GettxtValue(txtMostRecentCD4.Text);
                objAdultIE.MostRecentCD4Percent = GettxtValue(txtMostRecentCD4Percent.Text);
                objAdultIE.MostRecentCD4Date = DateGiven(dtMostRecentCD4Date.Value.ToString());
                objAdultIE.PreviousViralLoad = GettxtValue(txtPreviousViralLoad.Text);
                objAdultIE.PreviousViralLoadDate = DateGiven(dtPreviousViralLoadDate.Value.ToString());
                objAdultIE.OtherHIVRelatedHistory = txtOtherHIVRelatedHistory.Text;
                // }
                //Examination - ARV Exposure
                //objAdultIE.ARVExposureYesNo = ConvertInt(rblARVExposure.SelectedValue.ToString());
                //objAdultIE.ARVExposure = ConvertInt(DDLARVExposure.SelectedValue.ToString());
                objAdultIE.PMTC1StartDate = DateGiven("");
                objAdultIE.PEP1StartDate = DateGiven("");
                objAdultIE.HAART1StartDate = DateGiven("");
                objAdultIE.ARVExposerdosesmissed = ConvertInt(rblARVdosesmissed.SelectedValue);
                objAdultIE.ARVExposerdelaydoses = ConvertInt(rblARVDelayeddoses.SelectedValue);
                objAdultIE.PMTC1Regimen = txtPMTCTRegimen.Text;
                objAdultIE.PMTC1StartDate = DateGiven(dtPMTCTStartDate.Value);
                objAdultIE.PEP1Regimen = txtPEP.Text;
                objAdultIE.PEP1StartDate = DateGiven(dtPEP1StartDate.Value);
                objAdultIE.HAART1Regimen = txtHAARTRegimen.Text;
                objAdultIE.HAART1StartDate = DateGiven(dtHAART1StartDate.Value);

                objAdultIE.Impression = txtImpression.Text;

                objAdultIE.HIVRelatedOI = "";
                objAdultIE.NonHIVRelatedOI = "";
                for (int i = 0; i < cblDiagnosis.Items.Count; i++)
                {
                    if (cblDiagnosis.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "Diagnosis";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblDiagnosis.Items[i].Value);
                        if (cblDiagnosis.Items[i].Text == "HIV-Related illness")
                        {
                            objAdultIE.HIVRelatedOI = txtHIVRelatedOI.Text;

                            Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                            Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                            Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                            Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                            Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");
                            Insertcbl.Append(" END ");
                        }
                        if (cblDiagnosis.Items[i].Text == "Non-HIV related illness")
                        {
                            objAdultIE.NonHIVRelatedOI = txtNonHIVRelatedOI.Text;
                            //listmulti.Add(objAdultIEMultiSelect);
                            //objAdultIE.AdultIEMultiSelect = listmulti;
                            Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                            Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                            Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                            Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                            Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");
                            Insertcbl.Append(" END ");
                        }

                    }
                }

                for (int i = 0; i < this.UcWhostaging.gvWHO1.Rows.Count; i++)
                {
                    Label lblWHOId = (Label)UcWhostaging.gvWHO1.Rows[i].FindControl("lblwho1");
                    CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO1.Rows[i].FindControl("Chkwho1");
                    HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date");
                    HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date1");
                    if (chkWHOId.Checked == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "WHOStageIConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(lblWHOId.Text);
                        if (!String.IsNullOrEmpty(txtWHODate1.Value))
                        {
                            objAdultIEMultiSelect.DateField1 = Convert.ToString(txtWHODate1.Value);
                        }
                        if (!String.IsNullOrEmpty(txtWHODate2.Value))
                        {
                            objAdultIEMultiSelect.DateField2 = Convert.ToString(txtWHODate2.Value);
                        }
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");
                        Insertcbl.Append(" END ");
                    }
                }
                //}
                //WHO-II
                //if (this.UcWhostaging.rblWHOStageII.SelectedValue == "1")
                //{
                for (int i = 0; i < this.UcWhostaging.gvWHO2.Rows.Count; i++)
                {
                    Label lblWHOId = (Label)UcWhostaging.gvWHO2.Rows[i].FindControl("lblwho2");
                    CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO2.Rows[i].FindControl("Chkwho2");
                    HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date");
                    HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date1");
                    if (chkWHOId.Checked == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "WHOStageIIConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(lblWHOId.Text);
                        if (!String.IsNullOrEmpty(txtWHODate1.Value))
                        {
                            objAdultIEMultiSelect.DateField1 = Convert.ToString(txtWHODate1.Value);
                        }
                        if (!String.IsNullOrEmpty(txtWHODate2.Value))
                        {
                            objAdultIEMultiSelect.DateField2 = Convert.ToString(txtWHODate2.Value);
                        }
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");
                        Insertcbl.Append(" END ");
                    }
                }

                //}
                //WHO-III
                //if (this.UcWhostaging.rblWHOStageIII.SelectedValue == "1")
                //{
                for (int i = 0; i < this.UcWhostaging.gvWHO3.Rows.Count; i++)
                {
                    Label lblWHOId = (Label)UcWhostaging.gvWHO3.Rows[i].FindControl("lblwho3");
                    CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO3.Rows[i].FindControl("Chkwho3");
                    HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date");
                    HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date1");
                    if (chkWHOId.Checked == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "WHOStageIIICoditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(lblWHOId.Text);
                        if (!String.IsNullOrEmpty(txtWHODate1.Value))
                        {
                            objAdultIEMultiSelect.DateField1 = Convert.ToString(txtWHODate1.Value);
                        }
                        if (!String.IsNullOrEmpty(txtWHODate2.Value))
                        {
                            objAdultIEMultiSelect.DateField2 = Convert.ToString(txtWHODate2.Value);
                        }
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");
                        Insertcbl.Append(" END ");
                    }
                }
                //}
                //WHO-IV
                //if (this.UcWhostaging.rblWHOStageIV.SelectedValue == "1")
                //{
                for (int i = 0; i < this.UcWhostaging.gvWHO4.Rows.Count; i++)
                {
                    Label lblWHOId = (Label)UcWhostaging.gvWHO4.Rows[i].FindControl("lblwho4");
                    CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO4.Rows[i].FindControl("Chkwho4");
                    HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date");
                    HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date1");
                    if (chkWHOId.Checked == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "WHOStageIVConditions";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(lblWHOId.Text);
                        if (!String.IsNullOrEmpty(txtWHODate1.Value))
                        {
                            objAdultIEMultiSelect.DateField1 = Convert.ToString(txtWHODate1.Value);
                        }
                        if (!String.IsNullOrEmpty(txtWHODate2.Value))
                        {
                            objAdultIEMultiSelect.DateField2 = Convert.ToString(txtWHODate2.Value);
                        }
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;
                        Insertcbl.Append("if not exists(select * from dtl_Multiselect_line where ptn_pk=" + objAdultIE.PtnPk + " and visit_pk=" + VisitIdforMultiSelect + " and valueid=" + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + " and FieldName='" + objAdultIEMultiSelect.FieldName + "') BEGIN ");
                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");
                        Insertcbl.Append(" END ");
                    }
                }
                //}

                objAdultIE.ProgressionInWHOstage = ConvertInt(UcWhostaging.rdoProgressionInWHOstage.SelectedValue);
                objAdultIE.SpecifyWHOprogression = UcWhostaging.txtSpecifyWHOprogression.Text;
                objAdultIE.WHOStage = rcbSelectedValue(UcWhostaging.ddlwhostage1);
                objAdultIE.WABStage = rcbSelectedValue(UcWhostaging.ddlWABStage);
                objAdultIE.TannerStaging = rcbSelectedValue(UcWhostaging.ddltannerstaging);
                int Menarche = UcWhostaging.radbtnMernarcheyes.Checked ? 1 : UcWhostaging.radbtnMernarcheno.Checked ? 0 : 9;
                objAdultIE.Mernarche = ConvertInt(Menarche.ToString());
                objAdultIE.Mernarchedate = DateGiven("");
                if (Menarche == 1)
                {
                    objAdultIE.Mernarchedate = DateGiven(UcWhostaging.txtmenarchedate.Value);
                }
                list.Add(objAdultIE);
                IKNHAdultIE theExpressManager;
                theExpressManager = (IKNHAdultIE)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAdultIE, BusinessProcess.Clinical");
                DataTable DTresult = theExpressManager.SaveAdultIE_ExamTab(list, Insertcbl);
                if (DTresult.Rows.Count > 0)
                {
                    SaveCancel("Examination");
                    //GetDataValue();
                    checkIfPreviuosTabSaved();
                    tabControl.ActiveTabIndex = 4;

                }
            }
            catch (Exception ex)
            {
                isError = true;
                error = ex.Message.ToString();
            }


        }

        protected void SaveManagementTab()
        {
            List<BIQAdultIE> list = new List<BIQAdultIE>();
            List<AdultIEMultiselect> listmulti = new List<AdultIEMultiselect>();
            BIQAdultIE objAdultIE = new BIQAdultIE();
            AdultIEMultiselect objAdultIEMultiSelect = new AdultIEMultiselect();
            StringBuilder Insertcbl = new StringBuilder();
            string error = "";
            try
            {
                objAdultIE.ID = 0;
                objAdultIE.AdultIEFeatureID = GblIQCare.FormId;
                objAdultIE.PtnPk = Convert.ToInt32(Session["PatientId"]);
                string VisitIdforMultiSelect = "IDENT_CURRENT('ord_visit')";
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    VisitIdforMultiSelect = Session["PatientVisitId"].ToString();
                    objAdultIE.VisitPk = Convert.ToInt32(Session["PatientVisitId"]);
                    objAdultIE.LocationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                else
                {

                    objAdultIE.VisitPk = Convert.ToInt32(Session["PatientVisitId"]);
                    objAdultIE.LocationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                //objAdultIE.LocationId = Int32.Parse(Session["AppLocationId"].ToString());
                objAdultIE.UserId = Int32.Parse(Session["AppUserId"].ToString());
                objAdultIE.VisitDate = string.Format("{0:dd-MMM-yyyy}", txtVisitDate.Value);
                //Management-Drug Allergy and toxicities

                objAdultIE.SpecifyAntibioticAllery = "";
                objAdultIE.ARVDrugAllergy = "";
                objAdultIE.OtherDrugAllergy = "";

                objAdultIE.SpecifyAntibioticAllery = "";
                objAdultIE.ARVDrugAllergy = "";

                objAdultIE.WorkUpPlan = "";

                objAdultIE.OtherShortTermEffects = txtOtherShortTermEffects.Text;
                for (int i = 0; i < cblShortTermEffects.Items.Count; i++)
                {
                    if (cblShortTermEffects.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "ShortTermEffects";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblShortTermEffects.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.HIVRelatedOI + "')");

                    }
                }
                //Short term effects multiselect
                //ShortTerm effects textbox for other
                objAdultIE.OtherLongtermEffects = txtOtherLongtermEffects.Text;
                for (int i = 0; i < cblLongTermEffects.Items.Count; i++)
                {
                    if (cblLongTermEffects.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "LongTermEffects";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(cblLongTermEffects.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.OtherLongtermEffects + "')");

                    }
                }

                //Long term effects multiselect
                //Longterm effects for other
                objAdultIE.WorkUpPlan = txtWorkUpPlan.Text;
                //}
                //Management - Lab Evaluation
                objAdultIE.OtherLabReview = UcLabEval.txtlabdiagnosticinput.Text;

                //Management - Treatment                
                objAdultIE.ARTTreatmentPlan = ConvertInt(UCPharmacy.ddlTreatmentplan.SelectedValue);
                objAdultIE.RegimenPrescribed = 0;
                objAdultIE.OtherRegimenPrescribed = "";
                objAdultIE.OtherARTEligibilityCriteria = UCPharmacy.txtSpecifyOtherEligibility.Text;
                for (int i = 0; i < UCPharmacy.chklistEligiblethrough.Items.Count; i++)
                {
                    if (UCPharmacy.chklistEligiblethrough.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "ARTEligibilityCriteria";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(UCPharmacy.chklistEligiblethrough.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.OtherARTEligibilityCriteria + "')");

                    }
                }
                objAdultIE.NumberDrugsSubstituted = ConvertInt(UCPharmacy.txtNoofdrugssubstituted.Text);
                objAdultIE.OtherARTChangeCode = UCPharmacy.txtSpecifyotherARTchangereason.Text;

                for (int i = 0; i < UCPharmacy.chklistARTchangecode.Items.Count; i++)
                {
                    if (UCPharmacy.chklistARTchangecode.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "ARTchangecode";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(UCPharmacy.chklistARTchangecode.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.OtherARTChangeCode + "')");

                    }
                }
                objAdultIE.OtherARTStopCode = UCPharmacy.txtSpecifyOtherStopCode.Text;
                for (int i = 0; i < UCPharmacy.chklistARTstopcode.Items.Count; i++)
                {
                    if (UCPharmacy.chklistARTstopcode.Items[i].Selected == true)
                    {
                        objAdultIEMultiSelect.FieldId = 0;
                        objAdultIEMultiSelect.FieldName = "ARTstopcode";
                        objAdultIEMultiSelect.ValueID = Convert.ToInt32(UCPharmacy.chklistARTstopcode.Items[i].Value);
                        //listmulti.Add(objAdultIEMultiSelect);
                        //objAdultIE.AdultIEMultiSelect = listmulti;

                        Insertcbl.Append("insert into [dtl_Multiselect_line] (Ptn_pk,ValueID,Visit_Pk,FieldName,FieldID,UserId,CreateDate,DateField1,DateField2,NumericField,Other_Notes)");
                        Insertcbl.Append(" values (" + objAdultIE.PtnPk + ", " + ConverTotValue.NullToInt(objAdultIEMultiSelect.ValueID).ToString() + ",  " + VisitIdforMultiSelect + ",'" + objAdultIEMultiSelect.FieldName + "'," + objAdultIEMultiSelect.FieldId + ",");
                        Insertcbl.Append(" " + ConverTotValue.NullToInt(objAdultIEMultiSelect.UserId).ToString() + ",  getdate(),'" + objAdultIEMultiSelect.DateField1 + "','" + objAdultIEMultiSelect.DateField2 + "',");
                        Insertcbl.Append(" '" + ConverTotValue.NullToInt(objAdultIEMultiSelect.NumericField).ToString() + "',  '" + objAdultIE.OtherARTStopCode + "')");

                    }
                }
                objAdultIE.SubstituteRegimen = ConvertInt(UCPharmacy.ddlReasonforswitchto2ndlineregimen.SelectedValue);

                ////Management-OI Treatment
                ////objAdultIE.OITreatment =ConvertInt(rblOITreatment.SelectedValue);
                objAdultIE.OIProphylaxis = ConvertInt(rcbOIProphylaxis.SelectedValue);
                objAdultIE.ReasonCoTrimoxPrescribed = ConvertInt(rcbReasonCTXPrescribed.SelectedValue);
                objAdultIE.ReasonFluconazolePrescribed = ConvertInt(ddlfluconazole.SelectedValue);
                objAdultIE.OtherTreatment = "";
                objAdultIE.OtherTreatment = txtOtherTreatment.Text;
                list.Add(objAdultIE);
                IKNHAdultIE theExpressManager;
                theExpressManager = (IKNHAdultIE)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAdultIE, BusinessProcess.Clinical");
                DataTable DTresult = theExpressManager.SaveAdultIE_MgtTab(list, Insertcbl);
                if (DTresult.Rows.Count > 0)
                {
                    SaveCancel("Management");
                    //GetDataValue();
                    checkIfPreviuosTabSaved();
                    tabControl.ActiveTabIndex = 5;

                }
            }
            catch (Exception ex)
            {
                isError = true;
                error = ex.Message.ToString();
            }
            //if (isError)
            //{

            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "saveFail", "alert('" + error + "');", true);
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clsLoadingPanelDueToError", "parent.CloseLoading();", true);
            //}

        }
        protected void SavePositiveTab()
        {
            List<BIQAdultIE> list = new List<BIQAdultIE>();
            List<AdultIEMultiselect> listmulti = new List<AdultIEMultiselect>();
            BIQAdultIE objAdultIE = new BIQAdultIE();
            AdultIEMultiselect objAdultIEMultiSelect = new AdultIEMultiselect();
            StringBuilder Insertcbl = new StringBuilder();
            string error = "";
            try
            {
                objAdultIE.ID = 0;
                objAdultIE.AdultIEFeatureID = GblIQCare.FormId;
                objAdultIE.PtnPk = Convert.ToInt32(Session["PatientId"]);
                string VisitIdforMultiSelect = "IDENT_CURRENT('ord_visit')";
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    VisitIdforMultiSelect = Session["PatientVisitId"].ToString();
                    objAdultIE.VisitPk = Convert.ToInt32(Session["PatientVisitId"]);
                    objAdultIE.LocationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                else
                {

                    objAdultIE.VisitPk = Convert.ToInt32(Session["PatientVisitId"]);
                    objAdultIE.LocationId = Convert.ToInt32(Session["AppLocationId"]);
                }
                //objAdultIE.LocationId = Int32.Parse(Session["AppLocationId"].ToString());
                objAdultIE.UserId = Int32.Parse(Session["AppUserId"].ToString());
                objAdultIE.VisitDate = string.Format("{0:dd-MMM-yyyy}", txtVisitDate.Value);

                list.Add(objAdultIE);
                list.Add(objAdultIE);
                IKNHAdultIE theExpressManager;
                theExpressManager = (IKNHAdultIE)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAdultIE, BusinessProcess.Clinical");
                DataTable DTresult = theExpressManager.SaveAdultIE(list, Insertcbl);
                if (DTresult.Rows.Count > 0)
                {
                    //SaveCancel();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "saveSucc", "alert('Tab Data saved successfully');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "goBack", "BackBtnClick();", true);
                }
            }
            catch (Exception ex)
            {
                isError = true;
                error = ex.Message.ToString();
            }

        }
        protected void btncomplete_Click(object sender, EventArgs e)
        {
            SaveForm();
        }
        protected void btnSaveTriage_Click(object sender, EventArgs e)
        {
            if (fieldValidation(btnSaveTriage.ID) == false)
            {
                ShowHide();
                return;
            }
            SaveTraigeTab();

        }

        protected void btnSaveCHistory_Click(object sender, EventArgs e)
        {
            if (fieldValidation(btnSaveCHistory.ID) == false)
            {
                ShowHide();
                return;
            }
            SaveClinicalHistoryTab();

        }

        protected void btnSaveExam_Click(object sender, EventArgs e)
        {
            if (fieldValidation(btnSaveExam.ID) == false)
            {
                ShowHide();
                return;
            }
            SaveExaminationTab();

        }

        protected void btnSaveManagement_Click(object sender, EventArgs e)
        {
            if (fieldValidation(btnSaveManagement.ID) == false)
            {
                ShowHide();
                return;
            }
            SaveManagementTab();
        }


        public void checkIfPreviuosTabSaved()
        {
            IKNHStaticForms KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            //securityPertab();

            DataSet dsTriage = new DataSet();
            dsTriage = KNHStatic.CheckIfPreviuosTabSaved("AdultIETriage", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(dsTriage, btnSaveCHistory, btnPrintCHistory);

            DataSet dsClinincal = new DataSet();
            dsClinincal = KNHStatic.CheckIfPreviuosTabSaved("AdultIEClinicalHistory", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(dsClinincal, UcTBScreening.btnTBSave, UcTBScreening.btnTBPrint);

            DataSet dsTB = new DataSet();
            dsTB = KNHStatic.CheckIfPreviuosTabSaved("AdultIETBScreening", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(dsTB, btnSaveExam, btnPrintExam);

            DataSet dsExam = new DataSet();
            dsExam = KNHStatic.CheckIfPreviuosTabSaved("AdultIEExamination", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(dsExam, btnSaveManagement, btnPrintManagement);

            DataSet dsMgt = new DataSet();
            dsMgt = KNHStatic.CheckIfPreviuosTabSaved("AdultIEManagement", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(dsMgt, UcPwp.btnSave, UcPwp.btnPrintPositive);

            //
            DataSet dsPwP = new DataSet();
            dsPwP = KNHStatic.CheckIfPreviuosTabSaved("AdultIEPwP", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(dsPwP, btnSave, btnPrint);

            dsTriage.Dispose();
            dsClinincal.Dispose();
            dsTB.Dispose();
            dsExam.Dispose();
            dsMgt.Dispose();

        }
        private void buttonEnabledAndDisabled(DataSet ds, Button btnSave, Button btnPrint)
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                btnSave.Enabled = false;
                btnPrint.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
                btnPrint.Enabled = true;
                securityPertab();

            }
        }

        private void getTabIds()
        {
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(theDSXML.Tables["Mst_FormBuilderTab"]);
            if (Convert.ToString(Session["TechnicalAreaId"]) == "210")
            {
                theDV.RowFilter = "FeatureId=275";
            }
            else { theDV.RowFilter = "FeatureId=177"; }
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable dtTabId = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);

            //featureID = ApplicationAccess.KNHPaediatricInitialEvaulation;
            if (dtTabId.Rows.Count > 0)
            {
                foreach (DataRow drTabsId in dtTabId.Rows)
                {
                    switch (drTabsId["TabName"].ToString())
                    {
                        case "AdultIETriage":
                            AIETriageTabID = Convert.ToInt32(drTabsId["TabID"]);
                            break;
                        case "AdultIEClinicalHistory":
                            AIECATabID = Convert.ToInt32(drTabsId["TabID"]);
                            break;
                        case "AdultIETBScreening":
                            AIETBTabID = Convert.ToInt32(drTabsId["TabID"]);
                            break;
                        case "AdultIEExamination":
                            AIEExamTabID = Convert.ToInt32(drTabsId["TabID"]);
                            break;
                        case "AdultIEManagement":
                            AIEManageTabID = Convert.ToInt32(drTabsId["TabID"]);
                            break;
                        case "AdultIEPwP":
                            AIEPwPTabID = Convert.ToInt32(drTabsId["TabID"]);
                            break;
                    }
                }
            }
        }

        public void securityPertab()
        {
            AuthenticationManager Authentication = new AuthenticationManager();
            if (Convert.ToString(Session["TechnicalAreaId"]) == "210")
            {
                //triage
                Authentication.TabUserRights(btnSaveTriage, btnPrintTriage, 275, AIETriageTabID);
                //CA
                Authentication.TabUserRights(btnSaveCHistory, btnPrintCHistory, 275, AIECATabID);
                //TB
                Authentication.TabUserRights(this.UcTBScreening.btnTBSave, this.UcTBScreening.btnTBPrint, 275, AIETBTabID);
                //exam
                Authentication.TabUserRights(btnSaveExam, btnPrintExam, 275, AIEExamTabID);
                //manage
                Authentication.TabUserRights(this.btnSaveManagement, this.btnPrintManagement, 275, AIEManageTabID);
                //PwP
                Authentication.TabUserRights(this.UcPwp.btnSave, this.UcPwp.btnSubmitPositive, 275, AIEPwPTabID);
            }
            else
            {
                //triage
                Authentication.TabUserRights(btnSaveTriage, btnPrintTriage, 177, AIETriageTabID);
                //CA
                Authentication.TabUserRights(btnSaveCHistory, btnPrintCHistory, 177, AIECATabID);
                //TB
                Authentication.TabUserRights(this.UcTBScreening.btnTBSave, this.UcTBScreening.btnTBPrint, 177, AIETBTabID);
                //exam
                Authentication.TabUserRights(btnSaveExam, btnPrintExam, 177, AIEExamTabID);
                //manage
                Authentication.TabUserRights(this.btnSaveManagement, this.btnPrintManagement, 177, AIEManageTabID);
                //PwP
                Authentication.TabUserRights(this.UcPwp.btnSave, this.UcPwp.btnSubmitPositive, 177, AIEPwPTabID);
            }
        }

        public static void Redirect(string url, string target, string windowFeatures)
        {
            HttpContext context = HttpContext.Current;

            if ((String.IsNullOrEmpty(target) ||
                target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&
                String.IsNullOrEmpty(windowFeatures))
            {

                context.Response.Redirect(url);
            }
            else
            {
                Page page = (Page)context.Handler;
                if (page == null)
                {
                    throw new InvalidOperationException(
                        "Cannot redirect to new window outside Page context.");
                }
                url = page.ResolveClientUrl(url);

                string script;
                if (!String.IsNullOrEmpty(windowFeatures))
                {
                    script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
                }
                else
                {
                    script = @"window.open(""{0}"", ""{1}"");";
                }

                script = String.Format(script, url, target, windowFeatures);
                ScriptManager.RegisterStartupScript(page,
                    typeof(Page),
                    "Redirect",
                    script,
                    true);
            }
        }

        protected void tabControl_ActiveTabChanged(object sender, EventArgs e)
        {
            //checkIfPreviuosTabSaved();
            ShowHide();
        }
        protected void btncloseTriage_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }
        protected void btncloseCHistory_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }
        protected void btncloseExam_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }
        protected void btncloseMgt_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Delete")
            {
                int delete = theUtils.DeleteForm("Adult Initial Evaluation", Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppUserId"]));

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
        public void BindCheckUncheklogic(CheckBoxList chklst)
        {
            for (int i = 0; i < chklst.Items.Count; i++)
            {
                ListItem li = chklst.Items[i];
                if (li.Text.ToUpper() == "NO ILLNESS FOUND")
                {
                    li.Attributes.Add("onclick", "fnUncheckall('" + chklst.ClientID + "')");
                }
                else
                {
                    li.Attributes.Add("onclick", "fnUncheckNormal('" + chklst.ClientID + "')");
                }

            }
        }
    }
}