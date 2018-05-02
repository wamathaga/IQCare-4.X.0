using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Interface.Security;
using Application.Presentation;
using Application.Common;
using Interface.Clinical;
using System.Text;
using Interface.Administration;
using System.Drawing;

namespace PresentationApp.ClinicalForms
{
    
    public partial class frmClinical_KNH_PEP : LogPage
    {
        DataSet dsBind;
        IPatientKNHPEP KNHPEP;
        IKNHStaticForms KNHStatic;
        DataTable DTCheckedIds;
        DataSet theDSXML;
        static DateTime startTime;
        IQCareUtils theUtils = new IQCareUtils();
        TextBox txtlabdiagnosticinput;
        int PEPTriageTabID, PEPTriageFeatureID, PEPCATabID, PEPCAFeatureID;
        BindFunctions BindManager = new BindFunctions();

        protected void Page_PreRender(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindExistingData();

                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    LoadAutoPopulatingData();
                }
            }

            ShowAssociatedFieldsOnLoad();
            checkIfPreviuosTabSaved();

            this.UserControlKNH_SignatureTriage.lblSignature.Text = KNHStatic.GetSignature("PEPTraige", Convert.ToInt32(Session["PatientVisitId"]));
            this.UserControlKNH_SignatureCA.lblSignature.Text = KNHStatic.GetSignature("PEPClinicalAssessment", Convert.ToInt32(Session["PatientVisitId"]));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx",true);
            }

            KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            getUserControlValues();
            getTabFeatureIDs();
            if (!IsPostBack)
            {
                startTime = DateTime.Now;
                tabControlKNHPEP.OnClientActiveTabChanged = "ValidateSave";
                hdnCurrentTabId.Value = tabControlKNHPEP.ActiveTab.ID;
                hdnPrevTabId.Value = tabControlKNHPEP.ActiveTab.ID;
                hdnCurrenTabName.Value = tabControlKNHPEP.ActiveTab.HeaderText;
                hdnPrevTabName.Value = tabControlKNHPEP.ActiveTab.HeaderText;
                ViewState["ActiveTabIndex"] = tabControlKNHPEP.ActiveTabIndex;
                hdnPrevTabIndex.Value = Convert.ToString(tabControlKNHPEP.ActiveTabIndex);
                hdnCurrenTabIndex.Value = Convert.ToString(tabControlKNHPEP.ActiveTabIndex);
                BindControl();
                //BindExistingData();
                AddControlInDiv();

                string script = string.Empty;
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'stringascii'>\n";
                script += "StringASCII('tbpnlTriage');\n";
                script += "</script>\n";
                RegisterStartupScript("stringascii", script);


                
                FemaleControls();


                
            }
            addAttributes();

            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "PEP";
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "PEP";


            

            //if (Convert.ToInt32(Session["PatientVisitId"].ToString()) == 0)
            //{
            //    tbpnlClinicalAssessment.Enabled = false;
            //}
            if (Request.QueryString["name"] == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showDeletebutton", "ShowHide('tblDeleteButton','show');", true);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "hideSavebutton", "ShowHide('tblSaveButton','hide');", true);
            }
            
        }

        public void checkIfPreviuosTabSaved()
        {
            securityPertab();
            DataSet pepTriage = KNHStatic.CheckIfPreviuosTabSaved("PEPTraige", Convert.ToInt32(Session["PatientVisitId"]));
            if (pepTriage.Tables[0].Rows.Count == 0)
            {
                btnCAsave.Enabled = false;
                btnCAcomplete.Enabled = false;
                btnCAPrint.Enabled = false;
            }
            else
            {
                securityPertab();
                //btnCAsave.Enabled = true;
                //btnCAcomplete.Enabled = true;
                //btnCAPrint.Enabled = true;
            }
        }

        public void securityPertab()
        {
            AuthenticationManager Authentication = new AuthenticationManager();
            //triage
            Authentication.TabUserRights(btnsave, btnPrint, PEPTriageFeatureID, PEPTriageTabID);

            //CA
            Authentication.TabUserRights(btnCAsave, btnCAPrint, PEPCAFeatureID, PEPCATabID);

        }

        public void CheckUncheklogic(CheckBoxList chklst)
        {
            for (int i = 0; i < chklst.Items.Count; i++)
            {
                ListItem li = chklst.Items[i];
                if (li.Text == "None")
                {
                    li.Attributes.Add("onclick", "fnUncheckallMedCond('" + chklst.ClientID + "')");
                }
                else
                {
                    li.Attributes.Add("onclick", "fnUncheckNoneMedCond('" + chklst.ClientID + "')");
                }

            }
        }

        public void getTabFeatureIDs()
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
                        case "PEPTraige":
                            PEPTriageTabID = Convert.ToInt32(drTabsId["TabID"]);
                            PEPTriageFeatureID = Convert.ToInt32(drTabsId["FeatureID"]);
                            break;

                        case "PEPClinicalAssessment":
                            PEPCATabID = Convert.ToInt32(drTabsId["TabID"]);
                            PEPCAFeatureID = Convert.ToInt32(drTabsId["FeatureID"]);
                            break;
                    }
                }
            }
        }

        public void addAttributes()
        {
            txtVisitDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtVisitDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtdtLMP.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtdtLMP.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtdayspepdispensedsofar.Attributes.Add("onkeyup", "chkNumeric('" + txtdayspepdispensedsofar.ClientID + "')");
            txtdayspepdispensedthisvisit.Attributes.Add("onkeyup", "chkNumeric('" + txtdayspepdispensedthisvisit.ClientID + "')");
            txtdosesmissed.Attributes.Add("onkeyup", "chkNumeric('" + txtdosesmissed.ClientID + "')");
            txtdosesvomited.Attributes.Add("onkeyup", "chkNumeric('" + txtdosesvomited.ClientID + "')");

            txtdosesdelayed.Attributes.Add("onkeyup", "chkNumeric('" + txtdosesdelayed.ClientID + "')");
            CheckUncheklogic(cblMedicalConditions);

            ddloccupational.Attributes.Add("OnClick", "dropdownchangetext('" + ddloccupational.ClientID + "','divotheroccupational','Other Specify','" + txtotherPEP.ClientID + "');");
            ddlbodyfluid.Attributes.Add("OnClick", "dropdownchangetext('" + ddlbodyfluid.ClientID + "','divbodyfluid','Other Specify','" + txtfluidother.ClientID + "');");
            ddlnonoccupational.Attributes.Add("OnClick", "dropdownchangetext('" + ddlnonoccupational.ClientID + "','divnonoccupational','Other Specify','" + txtnonoccupationalother.ClientID + "');");
            ddlsexualassault.Attributes.Add("OnClick", "dropdownchangetext('" + ddlsexualassault.ClientID + "','divsexualassault','Other (specify)','" + txtothersexual.ClientID + "');");
            ddlpepregimen.Attributes.Add("OnClick", "dropdownchangetext('" + ddlpepregimen.ClientID + "','divotherpep','Other','" + txtotherpepregimen.ClientID + "');");
            /*
             <asp:DropDownList ID="ddloccupational" onchange=""
                                                                                    runat="server">
                                                                                </asp:DropDownList>
             
             <asp:DropDownList runat="server" ID="ddlbodyfluid" onchange=>
                                                                                </asp:DropDownList>
             * 
             * <asp:DropDownList runat="server" ID="ddlnonoccupational" onchange=>
                                                                                </asp:DropDownList>
             * 
             * <asp:DropDownList runat="server" ID="ddlsexualassault" onchange=">
                                                                                </asp:DropDownList>
             * 
             * <asp:DropDownList runat="server" ID="ddlpepregimen" onchange=>
                                                                                </asp:DropDownList>
             
             
             */
        }

        private void ShowAssociatedFieldsOnLoad()
        {
            string script;
            if (this.rdopatientcaregiverYes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'caregiverYes'>\n";
                script += "ShowHide('divcaregiver','show');\n";
                script += "</script>\n";
                RegisterStartupScript("caregiverYes", script);
            }

            if (this.rdorefferedfacilityYes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divspecityYes'>\n";
                script += "ShowHide('divspecity','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divspecityYes", script);
            }
          
            if(ddloccupational.SelectedItem.Text == "Other Specify")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divotheroccupationalYes'>\n";
                script += "ShowHide('divotheroccupational','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divotheroccupationalYes", script);
            }
                 
            if (ddlbodyfluid.SelectedItem.Text == "Other Specify")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divbodyfluidYes'>\n";
                script += "ShowHide('divbodyfluid','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divbodyfluidYes", script);
            }
        
            if (ddlnonoccupational.SelectedItem.Text == "Other Specify")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divnonoccupationalYes'>\n";
                script += "ShowHide('divnonoccupational','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divnonoccupationalYes", script);
            }

            if (ddlsexualassault.SelectedItem.Text == "Other (specify)")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divsexualassaultYes'>\n";
                script += "ShowHide('divsexualassault','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divsexualassaultYes", script);
            }

            if (ddlpepregimen.SelectedItem.Text == "Other")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divotherpepYes'>\n";
                script += "ShowHide('divotherpep','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divotherpepYes", script);
            }

            if(this.rdoarvsideeffectsyes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divshortermeffectsYes'>\n";
                script += "ShowHide('divshortermeffects','show');ShowHide('divlongtermeffectsshow','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divshortermeffectsYes", script);
            }
                        
            if(this.rdomisseddosesyes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divdosesmissedlastweekYes'>\n";
                script += "ShowHide('divdosesmissedlastweek','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divdosesmissedlastweekYes", script);
            }
                        
            if(this.vomitteddosesyes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divdosesvomitedYes'>\n";
                script += "ShowHide('divdosesvomited','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divdosesvomitedYes", script);
            }

            if(this.rdodelayedinanydoseyes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divnodoseddelayedYes'>\n";
                script += "ShowHide('divnodoseddelayed','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divnodoseddelayedYes", script);
            }
                        
            if(this.rdocondomsdispensedyes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divreasonfornotissueYes'>\n";
                script += "ShowHide('divreasonfornotissue','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divreasonfornotissueYes", script);
            }
                        
            if(this.rdocondomsdispensedno.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divcondomNotIssued'>\n";
                script += "ShowHide('divreasonfornotissue','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divcondomNotIssued", script);
            }


            for (int i = 0; i < this.idVitalSign.cblReferredTo.Items.Count; i++)
            {
                if (this.idVitalSign.cblReferredTo.Items[i].Text == "Other Specialist Clinic")
                {
                    if (this.idVitalSign.cblReferredTo.Items[i].Selected == true)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "divotherspecialistClinic", "ShowHide('TriagedivReferToSpecialistClinic','show');", true);
                    }
                }

                if (this.idVitalSign.cblReferredTo.Items[i].Text == "Other (Specify)")
                {
                    if (this.idVitalSign.cblReferredTo.Items[i].Selected == true)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "divotherclinicreferral", "ShowHide('TriagedivReferToOther','show');", true);
                    }
                }
            }

            for (int i = 0; i < cblMedicalConditions.Items.Count; i++)
            {
                if (cblMedicalConditions.Items[i].Text == "Other")
                {
                    if (cblMedicalConditions.Items[i].Selected == true)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "divcblmedicalcondOther", "ShowHide('divothercondition','show');", true);
                    }
                }
            }

            for (int i = 0; i < cblshorttermeffects.Items.Count; i++)
            {
                if (cblshorttermeffects.Items[i].Text == "Other Specify")
                {
                    if (cblshorttermeffects.Items[i].Selected == true)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "divcblshortTermOther", "ShowHide('divothershorttermeffects','show');", true);
                    }
                }
            }

            for (int i = 0; i < cbllongtermeffects.Items.Count; i++)
            {
                if (cbllongtermeffects.Items[i].Text == "Other specify")
                {
                    if (cbllongtermeffects.Items[i].Selected == true)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "divcbllongTermOther", "ShowHide('divspecityotherlogntermeffects','show');", true);
                    }
                }
            }

            //rdopatientcaregiverYes.Attributes.Add("OnClick", "ShowHide('divcaregiver','show');");
            //rdopatientcaregiverNo.Attributes.Add("OnClick", "ShowHide('divcaregiver','hide');");

            //rdorefferedfacilityYes.Attributes.Add("OnClick", "ShowHide('divspecity','show');");
            //rdorefferedfacilityNO.Attributes.Add("OnClick", "ShowHide('divspecity','hide');");
        }

        private void getUserControlValues()
        {
            txtlabdiagnosticinput = (TextBox)this.UserControlKNH_LabEvaluation1.FindControl("txtlabdiagnosticinput");

        }

        public void FemaleControls()
        {
            if (Session["PatientSex"].ToString() == "Male")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "trLMP", "ShowHide('trLMP','hide');", true);
                
            }
        }
    
        public void AddControlInDiv()
        {
             //hidtab1.Value = txtstarttime.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + txtdtLMP.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + rdopatientcaregiverYes.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + rdopatientcaregiverNo.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + ddlcaregiverrelationship.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + cblMedicalConditions.ClientID;
             //Have you been reffered from another facility
             hidtab1.Value = hidtab1.Value + "^" + rdorefferedfacilityYes.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + rdorefferedfacilityNO.ClientID;

             //If yes, specify
             hidtab1.Value = hidtab1.Value + "^" + txtspecity.ClientID;
             //Other medical conditions
             hidtab1.Value = hidtab1.Value + "^" + txtothermedicalconditions.ClientID;
             //Pre existing conditions additional notes
             hidtab1.Value = hidtab1.Value + "^" + txtpreconditionsnotes.ClientID;
             // From exposure, time taken to access 1st dose
             hidtab1.Value = hidtab1.Value + "^" + ddlexposure.ClientID;
             // Past Medical Record
             hidtab1.Value = hidtab1.Value + "^" + txtpostmedicalrecord.ClientID;
             //Occupational
             hidtab1.Value = hidtab1.Value + "^" + ddloccupational.ClientID;
             //Specify Other Occupational PEP
             hidtab1.Value = hidtab1.Value + "^" + txtotherPEP.ClientID;
             // Body Fluid Involved
             hidtab1.Value = hidtab1.Value + "^" + ddlbodyfluid.ClientID;
             //Specify other Body Fluid involved
             hidtab1.Value = hidtab1.Value + "^" + txtfluidother.ClientID;
             // Non - Occupational 
             hidtab1.Value = hidtab1.Value + "^" + ddlnonoccupational.ClientID;
             // Specify Other Non-Occupational Indication
             hidtab1.Value = hidtab1.Value + "^" + txtnonoccupationalother.ClientID;
             // Sexual assault
             hidtab1.Value = hidtab1.Value + "^" + ddlsexualassault.ClientID;
             // Specify other sexual assault
             hidtab1.Value = hidtab1.Value + "^" + txtothersexual.ClientID;
             // Action taken after exposure
             hidtab1.Value = hidtab1.Value + "^" + ddltactionafterexposure.ClientID;
             //PEP Regimen
             hidtab1.Value = hidtab1.Value + "^" + ddlpepregimen.ClientID;
             // Other PEP Regimen
             hidtab1.Value = hidtab1.Value + "^" + txtotherpepregimen.ClientID;
             // Current PEP regimen start date 
             hidtab1.Value = hidtab1.Value + "^" + datecurrentpepregimen.ClientID;
             //Days PEP Dispensed so far
             hidtab1.Value = hidtab1.Value + "^" + txtdayspepdispensedsofar.ClientID;
             //Days PEP Dispensed during this visit
             hidtab1.Value = hidtab1.Value + "^" + txtdayspepdispensedthisvisit.ClientID;
             // Drug Allergies / Toxicities 
             //hidtab1.Value = hidtab1.Value + "^" + rdodrugallergiesyes.ClientID;
             //hidtab1.Value = hidtab1.Value + "^" + rdodrugallergiesno.ClientID;

             //If yes check
             //hidtab1.Value = hidtab1.Value + "^" + ddlifyes.ClientID;
             //specify drug allergy
             //hidtab1.Value = hidtab1.Value + "^" + txtspecifydrugallergy.ClientID;
             //Other (Specify) 
             //hidtab1.Value = hidtab1.Value + "^" + txtotherspecity.ClientID;
             //ARV Side Effects :
             hidtab1.Value = hidtab1.Value + "^" + rdoarvsideeffectsyes.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + rdoarvsideeffectsno.ClientID;

             //Specify Other long term effects
             hidtab1.Value = hidtab1.Value + "^" + txtspecifyotherlongterm.ClientID;
             //Specify other short term effects
             hidtab1.Value = hidtab1.Value + "^" + txtspecityothershortterm.ClientID;
             //Have you missed any doses
             hidtab1.Value = hidtab1.Value + "^" + rdomisseddosesyes.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + rdomisseddosesno.ClientID;

             //Have you vomitted any doses
             hidtab1.Value = hidtab1.Value + "^" + vomitteddosesyes.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + vomitteddosesno.ClientID;

             //Have you delayed in any dose? :
             hidtab1.Value = hidtab1.Value + "^" + rdodelayedinanydoseyes.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + rdodelayedinanydoseno.ClientID;

             //Doses missed last week (times per week) 
             hidtab1.Value = hidtab1.Value + "^" + txtdosesmissed.ClientID;
             //Doses vomited last week (times per week) 
             hidtab1.Value = hidtab1.Value + "^" + txtdosesvomited.ClientID;
             //No of doses delayed last week :
             hidtab1.Value = hidtab1.Value + "^" + txtdosesdelayed.ClientID;
             //Lab Evaluation
             //hidtab1.Value = hidtab1.Value + "^" + rdolabevaluationyes.ClientID;
             //hidtab1.Value = hidtab1.Value + "^" + rdolabevaluationno.ClientID;

             //Lab Diagnostic Input 
             hidtab1.Value = hidtab1.Value + "^" + txtlabdiagnosticinput.ClientID;
             // HIV Elisa result 
             hidtab1.Value = hidtab1.Value + "^" + ddlhivelisaresult.ClientID;
             // HIV status for client 
             hidtab1.Value = hidtab1.Value + "^" + ddlhivstatusforclient.ClientID;
             //Hepatitis B Status for Client
             hidtab1.Value = hidtab1.Value + "^" + ddlhapatitisbstatus.ClientID;
             //Hepatitis C Status for client 
             hidtab1.Value = hidtab1.Value + "^" + ddlhepatitiscstatus.ClientID;
             //Source HIV Status
             hidtab1.Value = hidtab1.Value + "^" + ddlsourcehivstatus.ClientID;
             //Source Hepatitis B Status
             hidtab1.Value = hidtab1.Value + "^" + ddlsourcehepatitisbstatus.ClientID;
             //Source Hepatitis C Status
             hidtab1.Value = hidtab1.Value + "^" + ddlsourcehepatitiscstatus.ClientID;
             // Has client completed HBV vaccination?
             hidtab1.Value = hidtab1.Value + "^" + rdohbvvaccinationyes.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + rdohbvvaccinationno.ClientID;

             //Discussed disclosure plan 
             hidtab1.Value = hidtab1.Value + "^" + rdodiscusseddisclosureyes.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + rdodiscusseddisclosureno.ClientID;

             //Discussed safe sex practices 
             hidtab1.Value = hidtab1.Value + "^" + rdosafesexyes.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + rdosafesexno.ClientID;

             //Adherence/supportive counselling
             hidtab1.Value = hidtab1.Value + "^" + rdoadherencecounsellingyes.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + rdoadherencecounsellingno.ClientID;

             //Condoms dispensed
             hidtab1.Value = hidtab1.Value + "^" + rdocondomsdispensedyes.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + rdocondomsdispensedno.ClientID;

             //Reason for not issuing condoms 
             hidtab1.Value = hidtab1.Value + "^" + txtreasonfornotissuecondoms.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + this.idVitalSign.txtTemp.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + this.idVitalSign.txtRR.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + this.idVitalSign.txtHR.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + this.idVitalSign.txtHeight.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + this.idVitalSign.txtWeight.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + this.idVitalSign.txtBPSystolic.ClientID;
            
             //hidtab1.Value = hidtab1.Value + "^" + cbllabevaluation.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + cblshorttermeffects.ClientID;
             hidtab1.Value = hidtab1.Value + "^" + cbllongtermeffects.ClientID;


             
            
            
        }

        public void LoadAutoPopulatingData()
        {
            DataSet dsAutopopulate = new DataSet();
            dsAutopopulate = KNHStatic.GetPEPFormAutoPopulatingData(Convert.ToInt32(Session["PatientId"]));

            if (dsAutopopulate.Tables[0].Rows.Count > 0)
                this.idVitalSign.txtHeight.Text = dsAutopopulate.Tables[0].Rows[0]["Height"].ToString();

            if(dsAutopopulate.Tables[1].Rows.Count>0)
                txtdtLMP.Value = String.Format("{0:dd-MMM-yyyy}", dsAutopopulate.Tables[1].Rows[0]["LMP"]);
        }
       
        public void BindExistingData()
        {
            string script = string.Empty;
            if (Convert.ToInt32(Session["PatientVisitId"].ToString()) > 0)
            {
                hdnVisitId.Value = Session["PatientVisitId"].ToString();
                KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
                DataSet dsGet = KNHPEP.GetKNHPEPDetails(Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["PatientVisitId"].ToString()));
                if (dsGet.Tables[0].Rows.Count > 0)
                {
                    txtVisitDate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["visitdate"]);
                    //txtstarttime.Text = dsGet.Tables[0].Rows[0]["starttime"].ToString();
                    if (dsGet.Tables[1].Rows[0]["LMP"] != DBNull.Value)
                        txtdtLMP.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[1].Rows[0]["LMP"]);

                    // Patient accompanied by caregiver 

                    if (dsGet.Tables[0].Rows[0]["ChildAccompaniedByCaregiver"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["ChildAccompaniedByCaregiver"].ToString() == "True")
                        {
                            this.rdopatientcaregiverYes.Checked = true;

                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'caregiverYes'>\n";
                            script += "ShowHide('divcaregiver','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("caregiverYes", script);


                        }
                        else if (dsGet.Tables[0].Rows[0]["ChildAccompaniedByCaregiver"].ToString() == "False")
                        {
                            this.rdopatientcaregiverNo.Checked = true;
                        }
                    }
                    //Caregiver relationship
                    if (dsGet.Tables[0].Rows[0]["TreatmentSupporterRelationship"] != DBNull.Value)
                        ddlcaregiverrelationship.SelectedValue = dsGet.Tables[0].Rows[0]["TreatmentSupporterRelationship"].ToString();
                    //Have you been reffered from another facility
                    if (dsGet.Tables[0].Rows[0]["PatientRefferedOrNot"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["PatientRefferedOrNot"].ToString() == "True")
                        {
                            this.rdorefferedfacilityYes.Checked = true;

                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divspecityYes'>\n";
                            script += "ShowHide('divspecity','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divspecityYes", script);
                        }
                        else if (dsGet.Tables[0].Rows[0]["PatientRefferedOrNot"].ToString() == "False")
                        {
                            this.rdorefferedfacilityNO.Checked = true;
                        }
                    }
                    //If yes, specify
                    if (dsGet.Tables[0].Rows[0]["YesSpecify"] != DBNull.Value)
                        txtspecity.Text = dsGet.Tables[0].Rows[0]["YesSpecify"].ToString();
                    // Other medical conditions :
                    if (dsGet.Tables[0].Rows[0]["OtherPreExistingMedicalConditions"] != DBNull.Value)
                        txtothermedicalconditions.Text = dsGet.Tables[0].Rows[0]["OtherPreExistingMedicalConditions"].ToString();
                    // Pre existing conditions additional notes 
                    if (dsGet.Tables[0].Rows[0]["PresentingComplaintsAdditionalNotes"] != DBNull.Value)
                        txtpreconditionsnotes.Text = dsGet.Tables[0].Rows[0]["PresentingComplaintsAdditionalNotes"].ToString();
                    //From exposure, time taken to access 1st dose :
                    if (dsGet.Tables[0].Rows[0]["TimeToAccessDose"] != DBNull.Value)
                        ddlexposure.SelectedValue = dsGet.Tables[0].Rows[0]["TimeToAccessDose"].ToString();

                    if (dsGet.Tables[0].Rows[0]["NurseComments"] != DBNull.Value)
                        this.idVitalSign.txtnursescomments.Text = dsGet.Tables[0].Rows[0]["NurseComments"].ToString();

                    if (dsGet.Tables[0].Rows[0]["SpecialistClinicReferral"] != DBNull.Value)
                        this.idVitalSign.txtReferToSpecialistClinic.Text = dsGet.Tables[0].Rows[0]["SpecialistClinicReferral"].ToString();

                    if (dsGet.Tables[0].Rows[0]["OtherReferral"] != DBNull.Value)
                        this.idVitalSign.txtSpecifyOtherRefferedTo.Text = dsGet.Tables[0].Rows[0]["OtherReferral"].ToString();

                    //Past Medical Record
                    if (dsGet.Tables[0].Rows[0]["MedicalHistoryAdditionalNotes"] != DBNull.Value)
                        txtpostmedicalrecord.Text = dsGet.Tables[0].Rows[0]["MedicalHistoryAdditionalNotes"].ToString();
                    //Occupational
                    if (dsGet.Tables[0].Rows[0]["OccupationalPEP"] != DBNull.Value)
                    {
                        ddloccupational.SelectedValue = dsGet.Tables[0].Rows[0]["OccupationalPEP"].ToString();
                        //if (ddloccupational.SelectedIndex.ToString() == "2")
                        if(ddloccupational.SelectedItem.Text == "Other Specify")
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divotheroccupationalYes'>\n";
                            script += "ShowHide('divotheroccupational','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divotheroccupationalYes", script);
                        }
                    }
                    //Specify Other Occupational PEP :
                    if (dsGet.Tables[0].Rows[0]["OtherOccupationalPEP"] != DBNull.Value)
                        txtotherPEP.Text = dsGet.Tables[0].Rows[0]["OtherOccupationalPEP"].ToString();
                    //Body Fluid Involved :
                    if (dsGet.Tables[0].Rows[0]["BodyFluidInvolved"] != DBNull.Value)
                    {
                        ddlbodyfluid.SelectedValue = dsGet.Tables[0].Rows[0]["BodyFluidInvolved"].ToString();
                        if (ddlbodyfluid.SelectedItem.Text == "Other Specify")
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divbodyfluidYes'>\n";
                            script += "ShowHide('divbodyfluid','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divbodyfluidYes", script);
                        }
                    }
                    // Specify other Body Fluid involved:
                    if (dsGet.Tables[0].Rows[0]["OtherBodyFluidInvolved"] != DBNull.Value)
                        txtfluidother.Text = dsGet.Tables[0].Rows[0]["OtherBodyFluidInvolved"].ToString();
                    // Non - Occupational
                    if (dsGet.Tables[0].Rows[0]["NonOccupational"] != DBNull.Value)
                    {
                        ddlnonoccupational.SelectedValue = dsGet.Tables[0].Rows[0]["NonOccupational"].ToString();
                        if (ddlnonoccupational.SelectedItem.Text == "Other Specify")
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divnonoccupationalYes'>\n";
                            script += "ShowHide('divnonoccupational','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divnonoccupationalYes", script);
                        }
                    }
                    //Specify Other Non-Occupational Indication 
                    if (dsGet.Tables[0].Rows[0]["OtherNonOccupationalPEP"] != DBNull.Value)
                        txtnonoccupationalother.Text = dsGet.Tables[0].Rows[0]["OtherNonOccupationalPEP"].ToString();
                    //Sexual assault
                    if (dsGet.Tables[0].Rows[0]["SexualAssault"] != DBNull.Value)
                    {
                        ddlsexualassault.SelectedValue = dsGet.Tables[0].Rows[0]["SexualAssault"].ToString();
                        if (ddlsexualassault.SelectedItem.Text == "Other (specify)")
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divsexualassaultYes'>\n";
                            script += "ShowHide('divsexualassault','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divsexualassaultYes", script);
                        }
                    }
                    //Specify other sexual assault
                    if (dsGet.Tables[0].Rows[0]["OtherSexualAssault"] != DBNull.Value)
                        txtothersexual.Text = dsGet.Tables[0].Rows[0]["OtherSexualAssault"].ToString();
                    //Action taken after exposure
                    if (dsGet.Tables[0].Rows[0]["ActionAfterPEP"] != DBNull.Value)
                        ddltactionafterexposure.SelectedValue = dsGet.Tables[0].Rows[0]["ActionAfterPEP"].ToString();
                    // PEP Regimen 
                    if (dsGet.Tables[0].Rows[0]["PEPRegimen"] != DBNull.Value)
                    {
                        ddlpepregimen.SelectedValue = dsGet.Tables[0].Rows[0]["PEPRegimen"].ToString();
                        //if (ddlsexualassault.SelectedIndex.ToString() == "2")
                        if (ddlpepregimen.SelectedItem.Text == "Other")
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divotherpepYes'>\n";
                            script += "ShowHide('divotherpep','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divotherpepYes", script);
                        }
                    }
                    //Other PEP Regimen :
                    if (dsGet.Tables[0].Rows[0]["OtherPEPRegimen"] != DBNull.Value)
                        txtotherpepregimen.Text = dsGet.Tables[0].Rows[0]["OtherPEPRegimen"].ToString();
                    //Days PEP Dispensed so far :
                    if (dsGet.Tables[0].Rows[0]["DaysPEPDispensed"] != DBNull.Value)
                        txtdayspepdispensedsofar.Text = dsGet.Tables[0].Rows[0]["DaysPEPDispensed"].ToString();
                    //Days PEP Dispensed during this visit 
                    if (dsGet.Tables[0].Rows[0]["PEPDispensedInVisit"] != DBNull.Value)
                        txtdayspepdispensedthisvisit.Text = dsGet.Tables[0].Rows[0]["PEPDispensedInVisit"].ToString();
                    // ARV Side Effects :
                    if (dsGet.Tables[0].Rows[0]["ARVSideEffects"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["ARVSideEffects"].ToString() == "True")
                        {
                            this.rdoarvsideeffectsyes.Checked = true;
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divshortermeffectsYes'>\n";
                            script += "ShowHide('divshortermeffects','show');ShowHide('divlongtermeffectsshow','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divshortermeffectsYes", script);
                        }
                        else if (dsGet.Tables[0].Rows[0]["ARVSideEffects"].ToString() == "False")
                        {
                            this.rdoarvsideeffectsno.Checked = true;
                        }
                    }
                    //Specify other short term effects
                    if (dsGet.Tables[0].Rows[0]["OtherShortTermEffects"] != DBNull.Value)
                        txtspecityothershortterm.Text = dsGet.Tables[0].Rows[0]["OtherShortTermEffects"].ToString();
                    //pecify Other long term effects
                    if (dsGet.Tables[0].Rows[0]["OtherLongtermEffects"] != DBNull.Value)
                        txtspecifyotherlongterm.Text = dsGet.Tables[0].Rows[0]["OtherLongtermEffects"].ToString();
                    //Have you missed any doses?
                    if (dsGet.Tables[0].Rows[0]["MissedDoses"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["MissedDoses"].ToString() == "True")
                        {
                            this.rdomisseddosesyes.Checked = true;
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divdosesmissedlastweekYes'>\n";
                            script += "ShowHide('divdosesmissedlastweek','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divdosesmissedlastweekYes", script);
                        }
                        else if (dsGet.Tables[0].Rows[0]["MissedDoses"].ToString() == "False")
                        {
                            this.rdomisseddosesno.Checked = true;
                        }
                    }
                    //Doses missed last week (times per week) 
                    if (dsGet.Tables[0].Rows[0]["DosesMissedPEP"] != DBNull.Value)
                        txtdosesmissed.Text = dsGet.Tables[0].Rows[0]["DosesMissedPEP"].ToString();
                    // Have you vomitted any doses? 
                    if (dsGet.Tables[0].Rows[0]["VomitedDoses"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["VomitedDoses"].ToString() == "True")
                        {
                            this.vomitteddosesyes.Checked = true;
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divdosesvomitedYes'>\n";
                            script += "ShowHide('divdosesvomited','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divdosesvomitedYes", script);
                        }
                        else if (dsGet.Tables[0].Rows[0]["VomitedDoses"].ToString() == "False")
                        {
                            this.vomitteddosesno.Checked = true;
                        }
                    }
                    // Doses vomited last week (times per week) :
                    if (dsGet.Tables[0].Rows[0]["DosesVomited"] != DBNull.Value)
                        txtdosesvomited.Text = dsGet.Tables[0].Rows[0]["DosesVomited"].ToString();
                    //Have you delayed in any dose? :
                    if (dsGet.Tables[0].Rows[0]["DelayedDoses"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["DelayedDoses"].ToString() == "True")
                        {
                            this.rdodelayedinanydoseyes.Checked = true;
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divnodoseddelayedYes'>\n";
                            script += "ShowHide('divnodoseddelayed','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divnodoseddelayedYes", script);
                        }
                        else if (dsGet.Tables[0].Rows[0]["DelayedDoses"].ToString() == "False")
                        {
                            this.rdodelayedinanydoseno.Checked = true;
                        }
                    }
                    //No of doses delayed last week
                    if (dsGet.Tables[0].Rows[0]["DosesDelayed"] != DBNull.Value)
                        txtdosesdelayed.Text = dsGet.Tables[0].Rows[0]["DosesDelayed"].ToString();
                    // Lab Diagnostic Input 
                    if (dsGet.Tables[0].Rows[0]["LabEvaluationDiagnosticInput"] != DBNull.Value)
                        txtlabdiagnosticinput.Text = dsGet.Tables[0].Rows[0]["LabEvaluationDiagnosticInput"].ToString();
                    // HIV Elisa result 
                    if (dsGet.Tables[0].Rows[0]["Elisa"] != DBNull.Value)
                        ddlhivelisaresult.SelectedValue = dsGet.Tables[0].Rows[0]["Elisa"].ToString();
                    // HIV status for client 
                    if (dsGet.Tables[0].Rows[0]["HIVStatusClient"] != DBNull.Value)
                        ddlhivstatusforclient.SelectedValue = dsGet.Tables[0].Rows[0]["HIVStatusClient"].ToString();
                    // Hepatitis B Status for Client
                    if (dsGet.Tables[0].Rows[0]["HepatitisBStatusForClient"] != DBNull.Value)
                        ddlhapatitisbstatus.SelectedValue = dsGet.Tables[0].Rows[0]["HepatitisBStatusForClient"].ToString();
                    //Hepatitis C Status for client 
                    if (dsGet.Tables[0].Rows[0]["HepatitisCStatusForClient"] != DBNull.Value)
                        ddlhepatitiscstatus.SelectedValue = dsGet.Tables[0].Rows[0]["HepatitisCStatusForClient"].ToString();
                    //Source HIV Status :
                    if (dsGet.Tables[0].Rows[0]["HIVStatusSource"] != DBNull.Value)
                        ddlsourcehivstatus.SelectedValue = dsGet.Tables[0].Rows[0]["HIVStatusSource"].ToString();
                    //Source Hepatitis B Status
                    if (dsGet.Tables[0].Rows[0]["HepatitisBStatusSource"] != DBNull.Value)
                        ddlsourcehepatitisbstatus.SelectedValue = dsGet.Tables[0].Rows[0]["HepatitisBStatusSource"].ToString();
                    //Source Hepatitis C Status
                    if (dsGet.Tables[0].Rows[0]["HepatitisCStatusSource"] != DBNull.Value)
                        ddlsourcehepatitiscstatus.SelectedValue = dsGet.Tables[0].Rows[0]["HepatitisCStatusSource"].ToString();
                    //Has client completed HBV vaccination
                    if (dsGet.Tables[0].Rows[0]["HBVVaccine"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["HBVVaccine"].ToString() == "True")
                        {
                            this.rdohbvvaccinationyes.Checked = true;
                        }
                        else if (dsGet.Tables[0].Rows[0]["HBVVaccine"].ToString() == "False")
                        {
                            this.rdohbvvaccinationno.Checked = true;
                        }
                    }
                    // Discussed disclosure plan :
                    if (dsGet.Tables[0].Rows[0]["DisclosurePlanDiscussed"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["DisclosurePlanDiscussed"].ToString() == "True")
                        {
                            this.rdodiscusseddisclosureyes.Checked = true;
                        }
                        else if (dsGet.Tables[0].Rows[0]["DisclosurePlanDiscussed"].ToString() == "False")
                        {
                            this.rdodiscusseddisclosureno.Checked = true;
                        }
                    }
                    // Discussed safe sex practices 
                    if (dsGet.Tables[0].Rows[0]["SaferSexImportanceExplained"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["SaferSexImportanceExplained"].ToString() == "True")
                        {
                            this.rdosafesexyes.Checked = true;
                        }
                        else if (dsGet.Tables[0].Rows[0]["SaferSexImportanceExplained"].ToString() == "False")
                        {
                            this.rdosafesexno.Checked = true;
                        }
                    }
                    //Adherence/supportive counselling
                    if (dsGet.Tables[0].Rows[0]["AdherenceExplained"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["AdherenceExplained"].ToString() == "True")
                        {
                            this.rdoadherencecounsellingyes.Checked = true;
                        }
                        else if (dsGet.Tables[0].Rows[0]["AdherenceExplained"].ToString() == "False")
                        {
                            this.rdoadherencecounsellingno.Checked = true;
                        }
                    }
                    //Condoms dispensed?
                    if (dsGet.Tables[0].Rows[0]["CondomsIssued"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["CondomsIssued"].ToString() == "True")
                        {
                            this.rdocondomsdispensedyes.Checked = true;
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divreasonfornotissueYes'>\n";
                            script += "ShowHide('divreasonfornotissue','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divreasonfornotissueYes", script);
                        }
                        else if (dsGet.Tables[0].Rows[0]["CondomsIssued"].ToString() == "False")
                        {
                            this.rdocondomsdispensedno.Checked = true;
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divcondomNotIssued'>\n";
                            script += "ShowHide('divreasonfornotissue','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divcondomNotIssued", script);
                        }
                    }
                    //Reason for not issuing condoms 
                    if (dsGet.Tables[0].Rows[0]["ReasonfornotIssuingCondoms"] != DBNull.Value)
                        txtreasonfornotissuecondoms.Text = dsGet.Tables[0].Rows[0]["ReasonfornotIssuingCondoms"].ToString();
                }

                if (dsGet.Tables[3].Rows.Count > 0)
                {
                    if (dsGet.Tables[3].Rows[0]["Temp"] != DBNull.Value)
                        this.idVitalSign.txtTemp.Text = dsGet.Tables[3].Rows[0]["Temp"].ToString();
                    if (dsGet.Tables[3].Rows[0]["RR"] != DBNull.Value)
                        this.idVitalSign.txtRR.Text = dsGet.Tables[3].Rows[0]["RR"].ToString();
                    if (dsGet.Tables[3].Rows[0]["HR"] != DBNull.Value)
                        this.idVitalSign.txtHR.Text = dsGet.Tables[3].Rows[0]["HR"].ToString();
                    if (dsGet.Tables[3].Rows[0]["BPDiastolic"] != DBNull.Value)
                        this.idVitalSign.txtBPDiastolic.Text = dsGet.Tables[3].Rows[0]["BPDiastolic"].ToString();
                    if (dsGet.Tables[3].Rows[0]["BPSystolic"] != DBNull.Value)
                        this.idVitalSign.txtBPSystolic.Text = dsGet.Tables[3].Rows[0]["BPSystolic"].ToString();
                    if (dsGet.Tables[3].Rows[0]["Height"] != DBNull.Value)
                        this.idVitalSign.txtHeight.Text = dsGet.Tables[3].Rows[0]["Height"].ToString();
                    if (dsGet.Tables[3].Rows[0]["Weight"] != DBNull.Value)
                        this.idVitalSign.txtWeight.Text = dsGet.Tables[3].Rows[0]["Weight"].ToString();
                    if (dsGet.Tables[3].Rows[0]["HeadCircumference"] != DBNull.Value)
                        this.idVitalSign.txtheadcircumference.Text = dsGet.Tables[3].Rows[0]["HeadCircumference"].ToString();

                    this.idVitalSign.lblWA.Text = dsGet.Tables[3].Rows[0]["WeightForAge"].ToString();
                    this.idVitalSign.lblWH.Text = dsGet.Tables[3].Rows[0]["WeightForHeight"].ToString();

                    script = "";
                    script = "<script language = 'javascript' defer ='defer' id = 'hideVitalYNy'>\n";
                    script += "ShowHide('hideVitalYN','show');\n";
                    script += "</script>\n";
                    RegisterStartupScript("hideVitalYNy", script);
                }

                //Current PEP regimen start date :
                if (dsGet.Tables[2].Rows.Count > 0)
                {
                    if (dsGet.Tables[2].Rows[0]["PEPStartDate"] != DBNull.Value && ((DateTime)dsGet.Tables[2].Rows[0]["PEPStartDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                        datecurrentpepregimen.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[2].Rows[0]["PEPStartDate"]);

                    if (dsGet.Tables[2].Rows[0]["PEPEndDate"] != DBNull.Value && ((DateTime)dsGet.Tables[2].Rows[0]["PEPEndDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                        txtPEPRegimenEndDate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[2].Rows[0]["PEPEndDate"]);
                }


                DataView theDV = new DataView(dsGet.Tables[4]);
                theDV.RowFilter = "FieldName = 'PreExistingMedicalConditions'";
                theUtils.CheckBoxListBindExistingInfoWithAssociatedField(theDV.ToTable(), cblMedicalConditions, "Other", "divotherMedocalCondition", "ShowHide('divothercondition','show');", this);

                theDV = new DataView(dsGet.Tables[4]);
                theDV.RowFilter = "FieldName = 'ShortTermEffects'";
                theUtils.CheckBoxListBindExistingInfoWithAssociatedField(theDV.ToTable(), cblshorttermeffects, "Other Specify", "divothershorttermeffectsYes", "ShowHide('divothershorttermeffects','show');", this);

                theDV = new DataView(dsGet.Tables[4]);
                theDV.RowFilter = "FieldName = 'LongTermEffects'";
                theUtils.CheckBoxListBindExistingInfoWithAssociatedField(theDV.ToTable(), cbllongtermeffects, "Other specify", "divspecityotherlogntermeffectsYes", "ShowHide('divspecityotherlogntermeffects','show');", this);

                theDV = new DataView(dsGet.Tables[4]);
                theDV.RowFilter = "FieldName = 'RefferedToFUpF'";
                theUtils.CheckBoxListBindExistingInfo(theDV.ToTable(), this.idVitalSign.cblReferredTo);
                for (int i = 0; i < this.idVitalSign.cblReferredTo.Items.Count; i++)
                {
                    if (this.idVitalSign.cblReferredTo.Items[i].Text == "Other Specialist Clinic")
                    {
                        if (this.idVitalSign.cblReferredTo.Items[i].Selected == true)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "divotherspecialistClinic", "ShowHide('TriagedivReferToSpecialistClinic','show');", true);
                        }
                    }

                    if (this.idVitalSign.cblReferredTo.Items[i].Text == "Other (Specify)")
                    {
                        if (this.idVitalSign.cblReferredTo.Items[i].Selected == true)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "divotherclinicreferral", "ShowHide('TriagedivReferToOther','show');", true);
                        }
                    }
                }

            }
            else
            {
                hdnVisitId.Value = "0";
                txtVisitDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            }

            
        }

        


        public void ErrorLoad()
        {
            string script = string.Empty;
            
            if (rdopatientcaregiverYes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'caregiverYes'>\n";
                script += "ShowHide('divcaregiver','show');\n";
                script += "</script>\n";
                RegisterStartupScript("caregiverYes", script);
            }
            if (rdorefferedfacilityYes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divspecityYes'>\n";
                script += "ShowHide('divspecity','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divspecityYes", script);
            }
            if (ddloccupational.SelectedIndex.ToString() == "2")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divotheroccupationalYes'>\n";
                script += "ShowHide('divotheroccupational','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divotheroccupationalYes", script);
            }

            if (ddlbodyfluid.SelectedIndex.ToString() == "4")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divbodyfluidYes'>\n";
                script += "ShowHide('divbodyfluid','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divbodyfluidYes", script);
            }
            if (ddlnonoccupational.SelectedIndex.ToString() == "4")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divnonoccupationalYes'>\n";
                script += "ShowHide('divnonoccupational','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divnonoccupationalYes", script);
            }
            if (ddlsexualassault.SelectedIndex.ToString() == "2")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divsexualassaultYes'>\n";
                script += "ShowHide('divsexualassault','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divsexualassaultYes", script);
            }
            if (ddlpepregimen.SelectedIndex.ToString() == "2")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divotherpepYes'>\n";
                script += "ShowHide('divotherpep','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divotherpepYes", script);
            }

            //if (rdodrugallergiesyes.Checked)
            //{
            //    script = "";
            //    script = "<script language = 'javascript' defer ='defer' id = 'divspecifydrugYes'>\n";
            //    script += "ShowHide('divspecifydrug','show');\n";
            //    script += "</script>\n";
            //    RegisterStartupScript("divspecifydrugYes", script);
            //}

            //if (ddlifyes.SelectedIndex.ToString() == "3")
            //{
            //    script = "";
            //    script = "<script language = 'javascript' defer ='defer' id = 'divotherspecityYes'>\n";
            //    script += "ShowHide('divotherspecity','show');\n";
            //    script += "</script>\n";
            //    RegisterStartupScript("divotherspecityYes", script);
            //}

            if (rdoarvsideeffectsyes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divshortermeffectsYes'>\n";
                script += "ShowHide('divshortermeffects','show');ShowHide('divlongtermeffectsshow','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divshortermeffectsYes", script);
            }


            if (rdomisseddosesyes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divdosesmissedlastweekYes'>\n";
                script += "ShowHide('divdosesmissedlastweek','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divdosesmissedlastweekYes", script);
            }

            if (vomitteddosesyes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divdosesvomitedYes'>\n";
                script += "ShowHide('divdosesvomited','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divdosesvomitedYes", script);

            }
            if (rdodelayedinanydoseyes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divnodoseddelayedYes'>\n";
                script += "ShowHide('divnodoseddelayed','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divnodoseddelayedYes", script);
            }

            //if (rdolabevaluationyes.Checked)
            //{
            //    script = "";
            //    script = "<script language = 'javascript' defer ='defer' id = 'divlabdiagosticYes'>\n";
            //    script += "ShowHide('divlabdiagostic','show');\n";
            //    script += "</script>\n";
            //    RegisterStartupScript("divlabdiagosticYes", script);
            //}

            if (rdocondomsdispensedyes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divreasonfornotissueYes'>\n";
                script += "ShowHide('divreasonfornotissue','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divreasonfornotissueYes", script);
            }
            for (int j = 0; j < cblMedicalConditions.Items.Count; j++)
            {
               
                    if (cblMedicalConditions.Items[j].Selected)
                    {

                        if (cblMedicalConditions.Items[j].Text == "Other")
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divotherconditionYes'>\n";
                            script += "ShowHide('divothercondition','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divotherconditionYes", script);

                        }
                    }

                
            }
            for (int j = 0; j < cblshorttermeffects.Items.Count; j++)
            {
                
                    if (cblshorttermeffects.Items[j].Selected)
                    {
                        if (cblshorttermeffects.Items[j].Text == "Other Specify")
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divothershorttermeffectsYes'>\n";
                            script += "ShowHide('divothershorttermeffects','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divothershorttermeffectsYes", script);

                        }
                    }

                
            }
            for (int j = 0; j < cbllongtermeffects.Items.Count; j++)
            {
                if (cbllongtermeffects.Items[j].Selected)
                {
                    
                    if (cbllongtermeffects.Items[j].Text== "Other specify")
                    {
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'divspecityotherlogntermeffectsYes'>\n";
                        script += "ShowHide('divspecityotherlogntermeffects','show');\n";
                        script += "</script>\n";
                        RegisterStartupScript("divspecityotherlogntermeffectsYes", script);

                    }
                }
            }
        }
        public void BindControl()
        {
            
            //KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            //dsBind = KNHPEP.GetDetails();


            theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));

            BindComboXML(theDSXML, "TreatmentSupporterRelationship", ddlcaregiverrelationship);
            //BindManager.BindCombo(ddlcaregiverrelationship, dsBind.Tables[0], "Name", "Id");
            BindCheckListXML(theDSXML, "PreExistingMedicalConditions", cblMedicalConditions);
            //BindManager.BindCheckedList(cblMedicalConditions, dsBind.Tables[1], "Name", "ID");
            BindComboXML(theDSXML, "TimeToAccessDose", ddlexposure);
            //BindManager.BindCombo(ddlexposure, dsBind.Tables[2], "Name", "Id");
            BindComboXML(theDSXML, "OccupationalPEP", ddloccupational);
            //BindManager.BindCombo(ddloccupational, dsBind.Tables[3], "Name", "Id");
            BindComboXML(theDSXML, "BodyFluidInvolved", ddlbodyfluid);
            //BindManager.BindCombo(ddlbodyfluid, dsBind.Tables[4], "Name", "Id");
            BindComboXML(theDSXML, "NonOccupational", ddlnonoccupational);
            //BindManager.BindCombo(ddlnonoccupational, dsBind.Tables[5], "Name", "Id");
            BindComboXML(theDSXML, "SexualAssault", ddlsexualassault);
            //BindManager.BindCombo(ddlsexualassault, dsBind.Tables[6], "Name", "Id");
            BindComboXML(theDSXML, "ActionAfterPEP", ddltactionafterexposure);
            //BindManager.BindCombo(ddltactionafterexposure,dsBind.Tables[7], "Name", "Id");
            BindComboXML(theDSXML, "PEPRegimen", ddlpepregimen);
            //BindManager.BindCombo(ddlpepregimen, dsBind.Tables[8], "Name", "Id");
            //BindManager.BindCombo(ddlifyes, dsBind.Tables[9], "Name", "Id");

            //BindManager.BindCheckedList(cbllabevaluation,dsBind.Tables[10], "Name", "ID");

            BindComboXML(theDSXML, "Elisa", ddlhivelisaresult);
            //BindManager.BindCombo(ddlhivelisaresult, dsBind.Tables[11], "Name", "Id");
            BindComboXML(theDSXML, "HIVStatusClient", ddlhivstatusforclient);
            //BindManager.BindCombo(ddlhivstatusforclient, dsBind.Tables[12], "Name", "Id");
            BindComboXML(theDSXML, "HepatitisBStatusForClient", ddlhapatitisbstatus);
            //BindManager.BindCombo(ddlhapatitisbstatus, dsBind.Tables[13], "Name", "Id");
            BindComboXML(theDSXML, "HepatitisCStatusForClient", ddlhepatitiscstatus);
            //BindManager.BindCombo(ddlhepatitiscstatus, dsBind.Tables[14], "Name", "Id");
            BindComboXML(theDSXML, "HIVStatusSource", ddlsourcehivstatus);
            //BindManager.BindCombo(ddlsourcehivstatus, dsBind.Tables[15], "Name", "Id");
            BindComboXML(theDSXML, "HepatitisBStatusSource", ddlsourcehepatitisbstatus);
            //BindManager.BindCombo(ddlsourcehepatitisbstatus, dsBind.Tables[16], "Name", "Id");
            BindComboXML(theDSXML, "HepatitisCStatusSource", ddlsourcehepatitiscstatus);
            //BindManager.BindCombo(ddlsourcehepatitiscstatus, dsBind.Tables[17], "Name", "Id");

            BindCheckListXML(theDSXML, "ShortTermEffects", cblshorttermeffects);
            //BindManager.BindCheckedList(cblshorttermeffects, dsBind.Tables[18], "Name", "ID");
            BindCheckListXML(theDSXML, "LongTermEffects", cbllongtermeffects);
            //BindManager.BindCheckedList(cbllongtermeffects, dsBind.Tables[19], "Name", "ID");

            
            //BindManager.BindCombo(ddlsingature, dsBind.Tables[20], "FirstName", "Employeeid");
            
        }

        public void BindComboXML(DataSet theDSXML, string FieldName, DropDownList ddl)
        {
            DataView theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='" + FieldName + "'";
            DataView theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddl, theDT, "Name", "ID");
        }

        public void BindCheckListXML(DataSet theDSXML, string FieldName, CheckBoxList cbl)
        {
            DataView theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
            theDVCodeID.RowFilter = "Name='" + FieldName + "'";
            DataView theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
            DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCheckedList(cbl, theDT, "Name", "ID");
        }

        private Boolean fieldValidationTriage()
        {

            IQCareUtils theUtil = new IQCareUtils();
            DataTable PreExistingMedicalConditions;
            MsgBuilder totalMsgBuilder = new MsgBuilder();
            PreExistingMedicalConditions = GetCheckBoxListValues(cblMedicalConditions);
            if (txtVisitDate.Value.Trim() == "")
            {
               
                totalMsgBuilder.DataElements["MessageText"] = "Enter visit date";
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                return false;
                
            }

            tabControlKNHPEP.ActiveTabIndex = 0;
            int childaccompaniedbycaregiver = rdopatientcaregiverYes.Checked ? 1 : rdopatientcaregiverNo.Checked ? 0 : 9;
            if (childaccompaniedbycaregiver == 9)
            {
                IQCareMsgBox.Show("patientcaregiver", this);
                tabControlKNHPEP.ActiveTabIndex = 0;
                lblClientInfo.ForeColor = Color.Red;
                lblPtnCareGiver.ForeColor = Color.Red;
                return false;
            }
            else
            {
                lblClientInfo.ForeColor = Color.Black;
                lblPtnCareGiver.ForeColor = Color.Black;
            }
            if (this.idVitalSign.txtHeight.Text == "")
            {

                totalMsgBuilder.DataElements["MessageText"] = "Enter Height";
                tabControlKNHPEP.ActiveTabIndex = 0;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                this.idVitalSign.lblHeight.ForeColor = Color.Red;
                lblVitalSigns.ForeColor = Color.Red;
                return false;
            }
            else
            {
                this.idVitalSign.lblHeight.ForeColor = Color.Black;
                lblVitalSigns.ForeColor = Color.Black;
            }
            if (this.idVitalSign.txtWeight.Text == "")
            {

                totalMsgBuilder.DataElements["MessageText"] = "Enter Weight";
                tabControlKNHPEP.ActiveTabIndex = 0;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                this.idVitalSign.lblWeight.ForeColor = Color.Red;
                lblVitalSigns.ForeColor = Color.Red;
                return false;
            }
            else
            {
                this.idVitalSign.lblWeight.ForeColor = Color.Black;
                lblVitalSigns.ForeColor = Color.Black;
            }

            if (this.idVitalSign.txtBPSystolic.Text == "")
            {
                totalMsgBuilder.DataElements["MessageText"] = "Enter Systolic Blood pressure";
                tabControlKNHPEP.ActiveTabIndex = 0;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                this.idVitalSign.lblBP.ForeColor = Color.Red;
                lblVitalSigns.ForeColor = Color.Red;
                return false;
            }
            else
            {
                this.idVitalSign.lblBP.ForeColor = Color.Black;
                lblVitalSigns.ForeColor = Color.Black;
            }

            if (this.idVitalSign.txtBPDiastolic.Text == "")
            {
                totalMsgBuilder.DataElements["MessageText"] = "Enter Diastolic Blood pressure";
                tabControlKNHPEP.ActiveTabIndex = 0;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                this.idVitalSign.lblBP.ForeColor = Color.Red;
                lblVitalSigns.ForeColor = Color.Red;
                return false;
            }
            else
            {
                this.idVitalSign.lblBP.ForeColor = Color.Black;
                lblVitalSigns.ForeColor = Color.Black;
            }

            if (PreExistingMedicalConditions.Rows.Count <= 0)
            {
                IQCareMsgBox.Show("MedicalConditions", this);
                tabControlKNHPEP.ActiveTabIndex = 0;
                lblMedicalConditions.ForeColor = Color.Red;
                lblPreExstConds.ForeColor = Color.Red;
                return false;

            }
            else
            {
                lblMedicalConditions.ForeColor = Color.Black;
                lblPreExstConds.ForeColor = Color.Black;
            }

            if (ddlexposure.SelectedValue == "0")
            {
                totalMsgBuilder.DataElements["MessageText"] = "Enter Time taken to access 1st dose";
                tabControlKNHPEP.ActiveTabIndex = 0;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                lblAccFirstDose.ForeColor = Color.Red;
                lblPreExstConds.ForeColor = Color.Red;
                return false;
            }
            else
            {
                lblAccFirstDose.ForeColor = Color.Black;
                lblPreExstConds.ForeColor = Color.Black;
            }
            
            return true;
        }


        private Boolean fieldValidationCA()
        {
            
            MsgBuilder totalMsgBuilder = new MsgBuilder();
            
            tabControlKNHPEP.ActiveTabIndex = 1;
            if (ddloccupational.SelectedValue == "0" && ddlnonoccupational.SelectedValue == "0" && ddlsexualassault.SelectedValue == "0")
            {
                totalMsgBuilder.DataElements["MessageText"] = "Occupational, Non-occupational or Sexual assult not filled in.";
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                lblReasonPEP.ForeColor = Color.Red;
                lblOccupational.ForeColor = Color.Red;
                lblNonOccupational.ForeColor = Color.Red;
                lblSexualAssualt.ForeColor = Color.Red;
                return false;
            }
            else
            {
                lblReasonPEP.ForeColor = Color.Black;
                lblOccupational.ForeColor = Color.Black;
                lblNonOccupational.ForeColor = Color.Black;
                lblSexualAssualt.ForeColor = Color.Black;
            }
                
            
            return true;
        }

        private DataTable GetCheckBoxListValues(CheckBoxList chklist)
        {
            DataTable dt = new DataTable();
            DataColumn theID = new DataColumn("ID");
            theID.DataType = System.Type.GetType("System.Int32");
            dt.Columns.Add(theID);

            DataRow dr;


            for (int i = 0; i < chklist.Items.Count; i++)
            {
                if (chklist.Items[i].Selected)
                {
                    dr = dt.NewRow();
                    dr["ID"] = Convert.ToInt32(chklist.Items[i].Value);
                    dt.Rows.Add(dr);
                }

            }
            return dt;
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
        protected Hashtable HTTriage()
        {

            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID",Session["PatientId"]);
                theHT.Add("visitID",Session["PatientVisitId"]);
                theHT.Add("locationID",Session["AppLocationId"]);
                theHT.Add("userID", Convert.ToInt32(Session["AppUserId"]));
                // Visit date:
                theHT.Add("visitDate",txtVisitDate.Value);
                //-----------------Tab---Triage----------------------
                //Start Time 
                theHT.Add("starttime",startTime.ToString());
                //LMP
                theHT.Add("LMP",txtdtLMP.Value);
                //Patient accompanied by caregiver
                string childaccompaniedbycaregiver = rdopatientcaregiverYes.Checked ? "1" : rdopatientcaregiverNo.Checked ? "0" : "";
                theHT.Add("ChildAccompaniedByCaregiver",childaccompaniedbycaregiver);
                //Caregiver relationship
                theHT.Add("TreatmentSupporterRelationship",ddlcaregiverrelationship.SelectedItem.Value);
                //Have you been reffered from another facility
                string patientrefferedornot=rdorefferedfacilityYes.Checked ? "1" : rdorefferedfacilityNO.Checked ? "0" : "";
                theHT.Add("PatientRefferedOrNot",patientrefferedornot);
                //If yes, specify
                theHT.Add("YesSpecify",txtspecity.Text);
                //Other medical conditions
                theHT.Add("OtherPreExistingMedicalConditions",txtothermedicalconditions.Text);
                //Pre existing conditions additional notes
                theHT.Add("PresentingComplaintsAdditionalNotes",txtpreconditionsnotes.Text);
                // From exposure, time taken to access 1st dose
                theHT.Add("TimeToAccessDose",ddlexposure.SelectedItem.Value);

                theHT.Add("NurseComments", this.idVitalSign.txtnursescomments.Text);
                theHT.Add("SpecialistClinicReferral", this.idVitalSign.txtReferToSpecialistClinic.Text);
                theHT.Add("OtherReferral", this.idVitalSign.txtSpecifyOtherRefferedTo.Text);

                theHT.Add("Temp", this.idVitalSign.txtTemp.Text != "" ? this.idVitalSign.txtTemp.Text : "0");
                theHT.Add("RR", this.idVitalSign.txtRR.Text != "" ? this.idVitalSign.txtRR.Text : "0");
                theHT.Add("HR", this.idVitalSign.txtHR.Text != "" ? this.idVitalSign.txtHR.Text : "0");
                theHT.Add("height", this.idVitalSign.txtHeight.Text != "" ? this.idVitalSign.txtHeight.Text : "0");
                theHT.Add("weight", this.idVitalSign.txtWeight.Text != "" ? this.idVitalSign.txtWeight.Text : "0");
                theHT.Add("BPDiastolic", this.idVitalSign.txtBPDiastolic.Text != "" ? this.idVitalSign.txtBPDiastolic.Text : "0");
                theHT.Add("BPSystolic", this.idVitalSign.txtBPSystolic.Text != "" ? this.idVitalSign.txtBPSystolic.Text : "0");
                theHT.Add("HeadCircumference", this.idVitalSign.txtheadcircumference.Text != "" ? this.idVitalSign.txtheadcircumference.Text : "0");
                theHT.Add("WeightForHeight", this.idVitalSign.lblWH.Text);
                theHT.Add("WeightForAge", this.idVitalSign.lblWA.Text);
               
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;

        }

        protected Hashtable HTCA()
        {

            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Session["PatientVisitId"]);
                theHT.Add("locationID", Session["AppLocationId"]);
                theHT.Add("userID", Convert.ToInt32(Session["AppUserId"]));
                // Visit date:
                //theHT.Add("visitDate", txtVisitDate.Value);
                //-----------------Tab---Triage----------------------
                //Start Time 
                theHT.Add("starttime", startTime.ToString());

                //----------------Tab--Clinical Assessment------------------------

                // Past Medical Record
                theHT.Add("MedicalHistoryAdditionalNotes", txtpostmedicalrecord.Text);
                //Occupational
                theHT.Add("OccupationalPEP", ddloccupational.SelectedItem.Value);
                //Specify Other Occupational PEP
                theHT.Add("OtherOccupationalPEP", txtotherPEP.Text);
                // Body Fluid Involved
                theHT.Add("BodyFluidInvolved", ddlbodyfluid.SelectedItem.Value);
                //Specify other Body Fluid involved
                theHT.Add("OtherBodyFluidInvolved", txtfluidother.Text);
                // Non - Occupational 
                theHT.Add("NonOccupational", ddlnonoccupational.SelectedItem.Value);
                // Specify Other Non-Occupational Indication
                theHT.Add("OtherNonOccupationalPEP", txtnonoccupationalother.Text);
                // Sexual assault
                theHT.Add("SexualAssault", ddlsexualassault.SelectedItem.Value);
                // Specify other sexual assault
                theHT.Add("OtherSexualAssault", txtothersexual.Text);
                // Action taken after exposure
                theHT.Add("ActionAfterPEP", ddltactionafterexposure.SelectedItem.Value);
                //PEP Regimen
                theHT.Add("PEPRegimen", ddlpepregimen.SelectedItem.Value);
                // Other PEP Regimen
                theHT.Add("OtherPEPRegimen", txtotherpepregimen.Text);
                // Current PEP regimen start date 
                theHT.Add("CurrentPEPregimenstartdate", datecurrentpepregimen.Value);
                // Current PEP regimen end date 
                theHT.Add("CurrentPEPregimenEnddate", txtPEPRegimenEndDate.Value);
                //Days PEP Dispensed so far
                theHT.Add("DaysPEPDispensed", txtdayspepdispensedsofar.Text);
                //Days PEP Dispensed during this visit
                theHT.Add("PEPDispensedInVisit", txtdayspepdispensedthisvisit.Text);
                //ARV Side Effects :
                string arvsideeffectes = rdoarvsideeffectsyes.Checked ? "1" : rdoarvsideeffectsno.Checked ? "0" : "";
                theHT.Add("ARVSideEffects", arvsideeffectes);
                //Specify Other long term effects
                theHT.Add("OtherLongtermEffects", txtspecifyotherlongterm.Text);
                //Specify other short term effects
                theHT.Add("OtherShortTermEffects", txtspecityothershortterm.Text);
                //Have you missed any doses
                string misseddoses = rdomisseddosesyes.Checked ? "1" : rdomisseddosesno.Checked ? "0" : "";
                theHT.Add("MissedDoses", misseddoses);
                //Have you vomitted any doses
                string vomittedanydoses = vomitteddosesyes.Checked ? "1" : vomitteddosesno.Checked ? "0" : "";
                theHT.Add("VomitedDoses", vomittedanydoses);
                //Have you delayed in any dose? :
                string delayedinanydose = rdodelayedinanydoseyes.Checked ? "1" : rdodelayedinanydoseno.Checked ? "0" : "";
                theHT.Add("DelayedDoses", delayedinanydose);
                //Doses missed last week (times per week) 
                theHT.Add("DosesMissedPEP", txtdosesmissed.Text);
                //Doses vomited last week (times per week) 
                theHT.Add("DosesVomited", txtdosesvomited.Text);
                //No of doses delayed last week :
                theHT.Add("DosesDelayed", txtdosesdelayed.Text);
                //Lab Diagnostic Input 
                theHT.Add("LabEvaluationDiagnosticInput", txtlabdiagnosticinput.Text);
                // HIV Elisa result 
                theHT.Add("Elisa", ddlhivelisaresult.SelectedItem.Value);
                // HIV status for client 
                theHT.Add("HIVStatusClient", ddlhivstatusforclient.SelectedItem.Value);
                //Hepatitis B Status for Client
                theHT.Add("HepatitisBStatusForClient", ddlhapatitisbstatus.SelectedItem.Value);
                //Hepatitis C Status for client 
                theHT.Add("HepatitisCStatusForClient", ddlhepatitiscstatus.SelectedItem.Value);
                //Source HIV Status
                theHT.Add("HIVStatusSource", ddlsourcehivstatus.SelectedItem.Value);
                //Source Hepatitis B Status
                theHT.Add("HepatitisBStatusSource", ddlsourcehepatitisbstatus.SelectedItem.Value);
                //Source Hepatitis C Status
                theHT.Add("HepatitisCStatusSource", ddlsourcehepatitiscstatus.SelectedItem.Value);
                // Has client completed HBV vaccination?
                string clientcompletedhbvvaccination = rdohbvvaccinationyes.Checked ? "1" : rdohbvvaccinationno.Checked ? "0" : "";
                theHT.Add("HBVVaccine", clientcompletedhbvvaccination);
                //Discussed disclosure plan 
                string discusseddisclosureplan = rdodiscusseddisclosureyes.Checked ? "1" : rdodiscusseddisclosureno.Checked ? "0" : "";
                theHT.Add("DisclosurePlanDiscussed", discusseddisclosureplan);
                //Discussed safe sex practices 
                string discussedsafesex = rdosafesexyes.Checked ? "1" : rdosafesexno.Checked ? "0" : "";
                theHT.Add("SaferSexImportanceExplained", discussedsafesex);
                //Adherence/supportive counselling
                string adherencecounselling = rdoadherencecounsellingyes.Checked ? "1" : rdoadherencecounsellingno.Checked ? "0" : "";
                theHT.Add("AdherenceExplained", adherencecounselling);
                //Condoms dispensed
                string condomsdispensed = rdocondomsdispensedyes.Checked ? "1" : rdocondomsdispensedno.Checked ? "0" : "";
                theHT.Add("CondomsIssued", condomsdispensed);
                //Reason for not issuing condoms 
                theHT.Add("ReasonfornotIssuingCondoms", txtreasonfornotissuecondoms.Text);

            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;

        }

        public bool checkduplicateVisit()
        {
            bool duplicate = false;
            if (txtVisitDate.Value != "")
            {
                DataSet dsDuplicateVisit = KNHStatic.checkDuplicateVisit(txtVisitDate.Value, 21, Convert.ToInt32(Session["PatientId"]));
                if (dsDuplicateVisit.Tables[0].Rows.Count > 0)
                {
                    duplicate = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "duplicateForm", "alert('Form already exists.');", true);
                }
            }
            else
            {
                duplicate = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "blankVisitdate", "alert('Visit date cannot be blank.');", true);
            }

            return duplicate;
        }
        
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Session["PatientVisitId"].ToString() == "0")
            {
                if (checkduplicateVisit() == true)
                {
                    return;
                }
            }

            //string savetabname = hidtab2.Value;
            if (fieldValidationTriage() == false)
            {
                ErrorLoad();
                return;
            }            

            string PrevTabId = hdnPrevTabId.Value;
            hdnPrevTabId.Value = hdnCurrentTabId.Value;
            string SaveTabData = hdnSaveTabData.Value;
           
            Hashtable theHT = HTTriage();         
           


            KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            DataSet DsReturns = KNHPEP.SaveUpdateKNHPEPTriage(theHT, getCheckBoxListItemValues(cblMedicalConditions), getCheckBoxListItemValues(this.idVitalSign.cblReferredTo));
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "PEPTriageSaveUpdate", "alert('Data on Triage tab saved successfully.');", true);
            SaveCancel("Triage");
            startTime = DateTime.Now;
            if (Session["PatientVisitId"].ToString() == "0")
            {
                if (DsReturns.Tables[0].Rows.Count > 0)
                {
                    Session["PatientVisitId"] = DsReturns.Tables[0].Rows[0][0].ToString();
                }
            }
            Session["Redirect"] = "0";
            checkIfPreviuosTabSaved();
            tabControlKNHPEP.ActiveTabIndex = 1;
            
        }



        protected void btnCAsave_Click(object sender, EventArgs e)
        {
     
            if (fieldValidationCA() == false)
            {
                ErrorLoad();
                return;
            }
 
            //string PrevTabId = hdnPrevTabId.Value;
            //hdnPrevTabId.Value = hdnCurrentTabId.Value;
            //string SaveTabData = hdnSaveTabData.Value;

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "triageInfoPEP", "alert('Triage Information has not been entered. Please fill in Triage information first.');", true);
            }
            else
            {

                Hashtable theHT = HTCA();

                KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
                DataSet DsReturns = KNHPEP.SaveUpdateKNHPEPCA(theHT, getCheckBoxListItemValues(cblshorttermeffects), getCheckBoxListItemValues(cbllongtermeffects));
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "PEPCASaveUpdate", "alert('Data on Clinical Assessment tab saved successfully.');", true);
                SaveCancel("Clinical Assessment");
                Session["Redirect"] = "0";
            }
        }

        private void SaveCancel(string tabname)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            IQCareMsgBox.NotifyAction(tabname + " Tab saved successfully.", Header.InnerText, true, this, "");           

        }

    
        protected void btncomplete_Click(object sender, EventArgs e)
        {
            //string savetabname = hidtab2.Value;
            //if (fieldValidation() == false)
            //{
            //    ErrorLoad();
            //    return;
            //}
            //Hashtable theHT = HT(1);
            //DataTable PreExistingMedicalConditions;
            ////DataTable LabEvaluationsSpecify;
            //DataTable ShortTermEffects;
            //DataTable LongTermEffects;

            //string tabname = string.Empty;
            //if (savetabname == "Triage" || tabControlKNHPEP.ActiveTabIndex == 0)
            //{
            //    tabname = "Triage";
            //}
            //else if (savetabname == "Clinical Assessment" || tabControlKNHPEP.ActiveTabIndex == 1)
            //{
            //    tabname = "Clinical Assessment";
            //}


            //PreExistingMedicalConditions = GetCheckBoxListValues(cblMedicalConditions);
            ////LabEvaluationsSpecify = GetCheckBoxListValues(cblMedicalConditions);
            //ShortTermEffects = GetCheckBoxListValues(cblshorttermeffects);
            //LongTermEffects = GetCheckBoxListValues(cbllongtermeffects);

            //KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            //DataSet DsReturns = KNHPEP.SaveUpdateKNHPEPData(theHT, PreExistingMedicalConditions, ShortTermEffects, LongTermEffects, tabname);
            //Session["Redirect"] = "0";
            //string saveupdate = string.Empty;
            //if (Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]) > 0)
            //{
            //    if (Convert.ToInt32(Session["PatientVisitId"].ToString()) > 0)
            //    {
            //        saveupdate = "Update";
            //    }
            //    else
            //    {
            //        saveupdate = "Save";
            //    }
            //    Session["PatientVisitId"] = Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]);
            //    hdnVisitId.Value = DsReturns.Tables[0].Rows[0]["Visit_Id"].ToString();
            //    if (savetabname == "Triage" || tabControlKNHPEP.ActiveTabIndex == 0)
            //    {
            //        tabControlKNHPEP.ActiveTabIndex = 0;
            //    }
            //    if (savetabname == "Clinical Assessment" || tabControlKNHPEP.ActiveTabIndex == 1)
            //    {
            //        tabControlKNHPEP.ActiveTabIndex = 1;
            //    }
            //    SaveCancel(tabname, saveupdate);
            //}
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            string theUrl = "";
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                theUrl = string.Format("{0}", "frmPatient_Home.aspx");
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                theUrl = string.Format("{0}", "frmPatient_History.aspx");

            }

            Response.Redirect(theUrl);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IQCareUtils.Redirect("../Laboratory/LabOrderForm.aspx", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
        }

        

        protected void btnCAback_Click(object sender, EventArgs e)
        {
            string theUrl = "";
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                theUrl = string.Format("{0}", "frmPatient_Home.aspx");
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                theUrl = string.Format("{0}", "frmPatient_History.aspx");

            }

            Response.Redirect(theUrl);
        }

        protected void btnCAcomplete_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Delete")
            {
                int delete = theUtils.DeleteForm("PEP", Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppUserId"]));

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

        

        //private void SaveCancel()
        //{

        //    string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        //    script += "var ans;\n";

        //    script += "ans=window.confirm('Form saved successfully. Do you want to close?');\n";
        //    script += "if (ans==true)\n";
        //    script += "{\n";
        //    if (ViewState["Redirect"] == "0")
        //    {
        //        script += "window.location.href='frmPatient_Home.aspx';\n";
        //    }
        //    else
        //    {
        //        script += "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';\n";
        //    }
        //    script += "}\n";
        //    script += "else \n";
        //    script += "{\n";
        //    script += "window.location.href='frmClinical_KNH_PEP.aspx';\n";
        //    script += "}\n";
        //    script += "</script>\n";
        //    RegisterStartupScript("confirm", script);
        //}
        
    }
}