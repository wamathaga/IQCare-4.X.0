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
using PresentationApp.ClinicalForms.UserControl;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace PresentationApp.ClinicalForms
{

    public partial class frmClinical_KNH_Paediatric_IE : LogPage
    {
        IPatientKNHPEP KNHPEP;
        DataSet dsBind;
        static String startTime;
        IKNHStaticForms KNHStatic;
        public int triageTabID, featureID, clinicalHistoryTabId, tbScreeningTabId, examinationTabId, managementTabId, pwpTabId = 0;
        IQCareUtils theUtils = new IQCareUtils();
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindExistingData();
            }
            getAllTabId();
            checkIfPreviuosTabSaved();
            if (idVitalSign.txtHeight.Text != "" && idVitalSign.txtWeight.Text != "")
                idVitalSign.calculateZScores();

            //sessionSize();
        }
        void tabControl_Handler()
        {
            tabControl.ActiveTabIndex = 3;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx",true);
            }

            UcTBScreening.btnHandler += new UserControlKNH_TBScreening.OnButtonClick(tabControl_Handler);
            KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            if (!IsPostBack)
            {
                startTime = String.Format("{0:dd-MMM-yyyy hh:mm:ss}", DateTime.Now);
                //hdnCurrentTabId.Value = tabControl.ActiveTab.ID;
                //hdnPrevTabId.Value = tabControl.ActiveTab.ID;
                //hdnCurrenTabName.Value = tabControl.ActiveTab.HeaderText;
                //hdnPrevTabName.Value = tabControl.ActiveTab.HeaderText;
                //ViewState["ActiveTabIndex"] = tabControl.ActiveTabIndex;
                //hdnPrevTabIndex.Value = Convert.ToString(tabControl.ActiveTabIndex);
                //hdnCurrenTabIndex.Value = Convert.ToString(tabControl.ActiveTabIndex);
                this.UcWhostaging.hiddateshow.Value = "SHOW";
                BindControl();
                //BindHidfortab();
                //Validate();
                ShowHideBusinessRules();
                getVisitId();
                addAttributes();
            }
            GblIQCare.FormId = 174;
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "KNH Paediatric IE";
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Paediatric Initial Evaluation";

            if (Request.QueryString["name"] == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showDeletebutton", "ShowHide('tblDeleteButton','show');", true);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "hideSavebutton", "ShowHide('tblSaveButton','hide');", true);
            }
            
        }
        private void addAttributes()
        {
            txtcd4.Attributes.Add("onkeyup", "chkDecimal('" + txtcd4.ClientID + "');");
            txtcd4per.Attributes.Add("onkeyup", "chkDecimal('" + txtcd4per.ClientID + "');");
            txthighCD4ever.Attributes.Add("onkeyup", "chkDecimal('" + txthighCD4ever.ClientID + "');");
            txthighestcd4everper.Attributes.Add("onkeyup", "chkDecimal('" + txthighestcd4everper.ClientID + "');");
            txtmostrecent_cd4.Attributes.Add("onkeyup", "chkDecimal('" + txtmostrecent_cd4.ClientID + "');");
            txtmostrecentcd4per.Attributes.Add("onkeyup", "chkDecimal('" + txtmostrecentcd4per.ClientID + "');");
            txtcd4artinitiation.Attributes.Add("onkeyup", "chkDecimal('" + txtcd4artinitiation.ClientID + "');");
            txtcd4artinitper.Attributes.Add("onkeyup", "chkDecimal('" + txtcd4artinitper.ClientID + "');");
            txtpreviousviral_load.Attributes.Add("onkeyup", "chkDecimal('" + txtpreviousviral_load.ClientID + "');");
        }
        public void sessionSize()
        {
            long totalSessionBytes = 0;
            BinaryFormatter b = new BinaryFormatter();
            MemoryStream m;
            foreach (var obj in Session)
            {
                m = new MemoryStream();
                b.Serialize(m, obj);
                totalSessionBytes += m.Length;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "sessionSize", "alert('Size of session :"  + totalSessionBytes.ToString() + "');", true);
        }
        private void getAllTabId()
        {
            KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            //DataTable dtTabId = KNHPEP.GetTabID(Convert.ToInt32(ApplicationAccess.KNHPaediatricInitialEvaulation));
            /********** getting the data from XML file *******************************/
            DataSet XMLDS = new DataSet();
            XMLDS.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(XMLDS.Tables["Mst_FormBuilderTab"]);
            DataTable dtTabId = theDV.ToTable();
            featureID = ApplicationAccess.KNHPaediatricInitialEvaulation;
            if (dtTabId.Rows.Count > 0)
            {
                foreach (DataRow drTabsId in dtTabId.Rows)
                {
                    if (drTabsId["TabName"].ToString() == "PaediatricIETriage")
                        triageTabID = Convert.ToInt32(drTabsId["TabId"]);
                    if (drTabsId["TabName"].ToString() == "PaediatricIEClinicalHistory")
                        clinicalHistoryTabId = Convert.ToInt32(drTabsId["TabId"]);
                    if (drTabsId["TabName"].ToString() == "PaediatricIETBScreening")
                        tbScreeningTabId = Convert.ToInt32(drTabsId["TabId"]);
                    if (drTabsId["TabName"].ToString() == "PaediatricIEExamination")
                        examinationTabId = Convert.ToInt32(drTabsId["TabId"]);
                    if (drTabsId["TabName"].ToString() == "PaediatricIEManagement")
                        managementTabId = Convert.ToInt32(drTabsId["TabId"]);
                    if (drTabsId["TabName"].ToString() == "PaediatricIEPwP")
                        pwpTabId = Convert.ToInt32(drTabsId["TabId"]);
                }
            }
        }
        public void ShowHideBusinessRules()
        {
            if (Convert.ToDecimal(Session["PatientAge"]) >= 3 && Convert.ToDecimal(Session["PatientAge"]) < 24)
                visibleDiv("divschoolingstatusy");

            if (Convert.ToDecimal(Session["PatientAge"]) >= 5)
                visibleDiv("divschoolperformancey");

            if (Convert.ToDecimal(Session["PatientAge"]) >= 10)
                visibleDiv("divtannerstagingy");

            if (Convert.ToDecimal(Session["PatientAge"]) >= 8 && Session["PatientSex"].ToString() == "Female")
                visibleDiv("divmernarcheshowy");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            // Save(0);
        }
        private void visibleDiv(String divId)
        {
            String script = "";
            script = "<script language = 'javascript' defer ='defer' id = '" + divId + "'>\n";
            script += "ShowHide('" + divId + "','show');\n";
            script += "</script>\n";
            RegisterStartupScript("'" + divId + "'", script);
        }
        private void getVisitId()
        {
            //Created by-Nidhi (Start)
            //Desc- getting the visitid based on patientId coz PED IE is single visit form
            KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            DataSet dsVisitId = KNHPEP.getVisitIdByPatient(Convert.ToInt32(Session["PatientId"].ToString()));
            if (dsVisitId.Tables[0].Rows.Count > 0)
            {
                Session["PatientVisitId"] = dsVisitId.Tables[0].Rows[0]["Visit_Id"];
            }
            //End
        }
        //get the added values from db and bind with controls
        public void BindExistingData()
        {
            getVisitId();
            string script = string.Empty;
            if (Convert.ToInt32(Session["PatientVisitId"].ToString()) > 0)
            {
                //hdnVisitId.Value = Session["PatientVisitId"].ToString();
                KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
                DataSet dsGet = KNHPEP.GetPaediatric_IE(Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["PatientVisitId"].ToString()));
                if (dsGet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drRow in dsGet.Tables[32].Rows)
                    {
                        for (int i = 0; i < cblDiagnosis.Items.Count; i++)
                        {
                            if (Convert.ToInt32(drRow["ValueId"]) == Convert.ToInt32(cblDiagnosis.Items[i].Value))
                            {
                                cblDiagnosis.Items[i].Selected = true;
                                if (drRow["Name"].ToString().Trim() == "HIV-Related illness")
                                {
                                    visibleDiv("divHIV");
                                    txtHIVRelatedOI.Text = dsGet.Tables[0].Rows[0]["HIVRelatedOI"].ToString();
                                }
                                if (drRow["Name"].ToString().Trim() == "Non-HIV related illness")
                                {
                                    visibleDiv("divNonHIV");
                                    txtNonHIVRelatedOI.Text = dsGet.Tables[0].Rows[0]["NonHIVRelatedOI"].ToString();
                                }
                            }
                        }
                    }
                    //for vital control referred to list
                    CheckBoxList cblReferredTo = (CheckBoxList)this.idVitalSign.FindControl("cblReferredTo");
                    TextBox txtReferToSpecialistClinic = (TextBox)this.idVitalSign.FindControl("txtReferToSpecialistClinic");
                    TextBox txtSpecifyOtherRefferedTo = (TextBox)this.idVitalSign.FindControl("txtSpecifyOtherRefferedTo");
                    foreach (DataRow drRow in dsGet.Tables[33].Rows)
                    {
                        for (int j = 0; j <= dsGet.Tables[33].Rows.Count - 1; j++)
                        {
                            for (int i = 0; i < cblReferredTo.Items.Count; i++)
                            {
                                if (Convert.ToInt32(drRow["ValueId"]) == Convert.ToInt32(cblReferredTo.Items[i].Value))
                                {
                                    cblReferredTo.Items[i].Selected = true;
                                    if (drRow["Name"].ToString().Trim() == "Other Specialist Clinic")
                                    {
                                        visibleDiv("TriagedivReferToSpecialistClinic");
                                        txtReferToSpecialistClinic.Text = dsGet.Tables[0].Rows[0]["PatientReferredOtherSpecialistClinic"].ToString();
                                    }
                                    if (drRow["Name"].ToString().Trim() == "Other (Specify)")
                                    {
                                        visibleDiv("TriagedivReferToOther");
                                        txtSpecifyOtherRefferedTo.Text = dsGet.Tables[0].Rows[0]["PatientReferredOtherSpecify"].ToString();
                                    }
                                }
                            }
                        }
                    }
                    /****************************************/
                    if (dsGet.Tables[0].Rows[0]["HAARTImpression"] != DBNull.Value)
                    {
                        ddlHAARTImpression.SelectedValue = dsGet.Tables[0].Rows[0]["HAARTImpression"].ToString();
                        if (ddlHAARTImpression.SelectedItem.Text == "Other specify")
                        {
                            visibleDiv("divSpecifyotherimpression");
                            txtOtherHAARTImpression.Text = dsGet.Tables[0].Rows[0]["OtherHAARTImpression"].ToString();
                        }
                    }
                    /*******Management***************/

                    if (dsGet.Tables[35].Rows.Count > 0)
                    {
                        if (dsGet.Tables[35].Rows[0]["TherapyPlan"] != DBNull.Value)
                        {
                            UcPharmacy.ddlTreatmentplan.SelectedValue = dsGet.Tables[35].Rows[0]["TherapyPlan"].ToString();
                            if (UcPharmacy.ddlTreatmentplan.SelectedItem.Text.ToLower() == "switch to second line")
                            {
                                UcPharmacy.ddlReasonforswitchto2ndlineregimen.SelectedValue = dsGet.Tables[35].Rows[0]["reasonForSwitchTo2ndLineRegimen"].ToString();
                                visibleDiv("divReasonforswitchto2ndlineregimen");
                            }
                            /*********** ART stop code :*******************/
                            if (UcPharmacy.ddlTreatmentplan.SelectedItem.Text.ToLower() == "stop treatment")
                            {
                                if (dsGet.Tables[38].Rows.Count > 0)
                                {
                                    foreach (DataRow row in dsGet.Tables[38].Rows)
                                    {
                                        for (int i = 0; i < this.UcPharmacy.chklistARTstopcode.Items.Count; i++)
                                        {
                                            if (Convert.ToInt32(this.UcPharmacy.chklistARTstopcode.Items[i].Value) == Convert.ToInt32(row["ValueId"]))
                                            {
                                                UcPharmacy.chklistARTstopcode.Items[i].Selected = true;
                                                if (UcPharmacy.chklistARTstopcode.Items[i].Text.ToLower() == "other patient decisi")
                                                {
                                                    visibleDiv("divARTstopcodeother");
                                                    UcPharmacy.txtSpecifyOtherStopCode.Text = dsGet.Tables[35].Rows[0]["specifyOtherStopCode"].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                                visibleDiv("divARTstopcode");
                            }
                        }
                        /*******Eligible through :********/
                        if (UcPharmacy.ddlTreatmentplan.SelectedItem.Text.ToLower() == "start new treatment (naive patient)")
                        {
                            if (dsGet.Tables[36].Rows.Count > 0)
                            {
                                foreach (DataRow row in dsGet.Tables[36].Rows)
                                {
                                    for (int i = 0; i < this.UcPharmacy.chklistEligiblethrough.Items.Count; i++)
                                    {
                                        if (Convert.ToInt32(this.UcPharmacy.chklistEligiblethrough.Items[i].Value) == Convert.ToInt32(row["ValueId"]))
                                        {
                                            UcPharmacy.chklistEligiblethrough.Items[i].Selected = true;
                                            if (UcPharmacy.chklistEligiblethrough.Items[i].Text.ToLower() == "other")
                                            {
                                                visibleDiv("divOtherEligibility");
                                                UcPharmacy.txtSpecifyOtherEligibility.Text = dsGet.Tables[35].Rows[0]["specifyOtherEligibility"].ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            visibleDiv("divEligiblethrough");
                        }

                        /*******ART change code :********/
                        if (UcPharmacy.ddlTreatmentplan.SelectedItem.Text.ToLower() == "change regimen")
                        {
                            UcPharmacy.txtNoofdrugssubstituted.Text = dsGet.Tables[35].Rows[0]["NoOfDrugsSubstituted"].ToString();
                            if (dsGet.Tables[37].Rows.Count > 0)
                            {
                                foreach (DataRow row in dsGet.Tables[37].Rows)
                                {
                                    for (int i = 0; i < this.UcPharmacy.chklistARTchangecode.Items.Count; i++)
                                    {
                                        if (Convert.ToInt32(this.UcPharmacy.chklistARTchangecode.Items[i].Value) == Convert.ToInt32(row["ValueId"]))
                                        {
                                            UcPharmacy.chklistARTchangecode.Items[i].Selected = true;
                                            if (UcPharmacy.chklistARTchangecode.Items[i].Text.ToLower() == "other")
                                            {
                                                visibleDiv("divSpecifyotherARTchangereason");
                                                UcPharmacy.txtSpecifyotherARTchangereason.Text = dsGet.Tables[35].Rows[0]["specifyOtherARTChangeReason"].ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            visibleDiv("divARTchangecode");
                        }
                        if (dsGet.Tables[35].Rows[0]["reasonForSwitchTo2ndLineRegimen"] != DBNull.Value)
                            UcPharmacy.ddlReasonforswitchto2ndlineregimen.SelectedValue = dsGet.Tables[35].Rows[0]["reasonForSwitchTo2ndLineRegimen"].ToString();
                    }

                    //------------------------------section client information
                    if (dsGet.Tables[0].Rows[0]["visitdate"] != DBNull.Value)
                        txtVisitDate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["visitdate"]);

                    txtchildaccompaniedby.Text = dsGet.Tables[0].Rows[0]["ChildAccompaniedBy"].ToString();

                    if (dsGet.Tables[0].Rows[0]["ChildDiagnosisConfirmed"] != DBNull.Value)
                        ddlchilddiagnosis.SelectedValue = dsGet.Tables[0].Rows[0]["ChildDiagnosisConfirmed"].ToString();

                    txtchildcaregiver.Text = dsGet.Tables[0].Rows[0]["PrimaryCareGiver"].ToString();


                    if (!String.IsNullOrEmpty(dsGet.Tables[0].Rows[0]["DisclosureStatus"].ToString()))
                    {
                        ddldisclosurestatus.SelectedValue = dsGet.Tables[0].Rows[0]["DisclosureStatus"].ToString();
                        if (ddldisclosurestatus.SelectedItem.Text == "Not ready")
                        {
                            visibleDiv("divreasonnotdisclosed");
                        }
                        if (ddldisclosurestatus.SelectedItem.Text == "Other specify")
                        {
                            visibleDiv("divotherdisclosurestatus");
                        }
                    }

                    txtspecifyreason.Text = dsGet.Tables[0].Rows[0]["ReasonNotDisclosed"].ToString();
                    txtspecifyotherdisclosurestatus.Text = dsGet.Tables[0].Rows[0]["OtherDisclosureReason"].ToString();
                    //For Mother
                    if (!String.IsNullOrEmpty(dsGet.Tables[0].Rows[0]["MotherAlive"].ToString()))
                    {
                        rbListMotherAlive.SelectedValue = (Convert.ToBoolean(dsGet.Tables[0].Rows[0]["MotherAlive"])) ? "1" : "0";
                        txtdateofmotherdeath.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["DateOfDeathMother"]);
                        if (rbListMotherAlive.SelectedValue == "0")
                            visibleDiv("divdateofmotherdeath");
                    }
                    //For Father 
                    if (!String.IsNullOrEmpty(dsGet.Tables[0].Rows[0]["FatherAlive"].ToString()))
                    {
                        rbListFatherAlive.SelectedValue = (Convert.ToBoolean(dsGet.Tables[0].Rows[0]["FatherAlive"])) ? "1" : "0";
                        txtdateoffatherdeath.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["DateOfDeathFather"]);
                        if (rbListFatherAlive.SelectedValue == "0")
                            visibleDiv("divdatefatherdeath");
                    }
                    if (!String.IsNullOrEmpty(dsGet.Tables[0].Rows[0]["ChildReferred"].ToString()))
                    {
                        rbListRefFacility.SelectedValue = (Convert.ToBoolean(dsGet.Tables[0].Rows[0]["ChildReferred"])) ? "1" : "0";
                        txtspecifyfacility.Text = dsGet.Tables[0].Rows[0]["ChildReferredFrom"].ToString();
                        if (rbListRefFacility.SelectedValue == "1")
                            visibleDiv("divspecifyfacility");
                    }
                    if (!String.IsNullOrEmpty(dsGet.Tables[0].Rows[0]["CurrentlyOnHAART"].ToString()))
                    {
                        rbListChildART.SelectedValue = (Convert.ToBoolean(dsGet.Tables[0].Rows[0]["CurrentlyOnHAART"])) ? "1" : "0";
                        if (rbListChildART.SelectedValue == "1")
                        {
                            visibleDiv("divcurrentregimenline");
                            visibleDiv("divARTRegimen");
                            visibleDiv("divARTDate");
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["CurrentARTRegimenLine"] != DBNull.Value)
                        ddlcurrentregimenline.SelectedValue = dsGet.Tables[0].Rows[0]["CurrentARTRegimenLine"].ToString();

                    if (dsGet.Tables[0].Rows[0]["CurrentARTRegimen"] != DBNull.Value)
                        ddlcurrentartregimen.SelectedValue = dsGet.Tables[0].Rows[0]["CurrentARTRegimen"].ToString();

                    if (dsGet.Tables[0].Rows[0]["CurrentARTRegimenDate"] != DBNull.Value)
                        txtcurrentartregimendate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["CurrentARTRegimenDate"]);

                    if (!String.IsNullOrEmpty(dsGet.Tables[0].Rows[0]["CurrentlyOnCTX"].ToString()))
                        rbListChildCTX.SelectedValue = (Convert.ToBoolean(dsGet.Tables[0].Rows[0]["CurrentlyOnCTX"])) ? "1" : "0";

                    if (dsGet.Tables[0].Rows[0]["SchoolingStatus"] != DBNull.Value)
                    {
                        ddlschoolingstatus.SelectedValue = dsGet.Tables[0].Rows[0]["SchoolingStatus"].ToString();
                        if (ddlschoolingstatus.SelectedIndex == 1)
                        {
                            visibleDiv("divhighestlevel");
                        }
                    }

                    if (dsGet.Tables[0].Rows[0]["HighestLevelAttained"] != DBNull.Value)
                        ddlhighestlevelattained.SelectedValue = dsGet.Tables[0].Rows[0]["HighestLevelAttained"].ToString();

                    //16 Ju
                    if (!String.IsNullOrEmpty(dsGet.Tables[0].Rows[0]["HealthEducation"].ToString()))
                    {
                        rbListHealthEducation.SelectedValue = (Convert.ToBoolean(dsGet.Tables[0].Rows[0]["HealthEducation"])) ? "1" : "0";
                    }
                    //16 Ju
                    if (!String.IsNullOrEmpty(dsGet.Tables[0].Rows[0]["HIVSupportGroup"].ToString()))
                    {
                        rbListSupportGroup.SelectedValue = (Convert.ToBoolean(dsGet.Tables[0].Rows[0]["HIVSupportGroup"])) ? "1" : "0";
                        if (rbListSupportGroup.SelectedValue == "1")
                            visibleDiv("divsupportgroup");
                    }
                    if (dsGet.Tables[0].Rows[0]["HIVSupportGroupMembership"] != DBNull.Value)
                        txthivsupportgroupmembership.Text = dsGet.Tables[0].Rows[0]["HIVSupportGroupMembership"].ToString();
                    //------vital sign

                    

                    
                    

                    if (dsGet.Tables[0].Rows[0]["NursesComments"] != DBNull.Value)
                        this.idVitalSign.txtnursescomments.Text = dsGet.Tables[0].Rows[0]["NursesComments"].ToString();

                    //If schooling,current school perfomance :
                    if (dsGet.Tables[0].Rows[0]["SchoolPerfomance"] != DBNull.Value)
                        ddlschoolperfomance.SelectedValue = dsGet.Tables[0].Rows[0]["SchoolPerfomance"].ToString();

                    //-------------Medical history (Disease, diagnosis and treatment)
                    // Medical history
                    if (dsGet.Tables[0].Rows[0]["MedicalHistory"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["MedicalHistory"].ToString() == "True")
                        {
                            this.rdomedicalhistoryyes.Checked = true;

                        }
                        else if (dsGet.Tables[0].Rows[0]["MedicalHistory"].ToString() == "False")
                        {
                            this.rdomedicalhistoryno.Checked = true;
                        }
                    }
                    //Specify Medical history :
                    if (dsGet.Tables[0].Rows[0]["OtherMedicalHistorySpecify"] != DBNull.Value)
                        txtspecifymedicalhistory.Text = dsGet.Tables[0].Rows[0]["OtherMedicalHistorySpecify"].ToString();

                    //Specify other chronic condition
                    if (dsGet.Tables[0].Rows[0]["OtherChronicCondition"] != DBNull.Value)
                        txtotherchroniccondition.Text = dsGet.Tables[0].Rows[0]["OtherChronicCondition"].ToString();

                    //Previously admitted :
                    if (!String.IsNullOrEmpty(dsGet.Tables[0].Rows[0]["PreviousAdmission"].ToString()))
                    {
                        rbListPreviouslyAdmitt.SelectedValue = (Convert.ToBoolean(dsGet.Tables[0].Rows[0]["PreviousAdmission"])) ? "1" : "0";
                        if (rbListPreviouslyAdmitt.SelectedValue == "1")
                        {
                            visibleDiv("divdiagnosis");
                            visibleDiv("divadmissionstart");
                            visibleDiv("divadmissionend");
                            // Diagnosis :
                            if (dsGet.Tables[0].Rows[0]["PreviousAdmissionDiagnosis"] != DBNull.Value)
                                txtdiagnosis.Text = dsGet.Tables[0].Rows[0]["PreviousAdmissionDiagnosis"].ToString();

                            //Admission start 
                            if (dsGet.Tables[0].Rows[0]["PreviousAdmissionStart"] != DBNull.Value)
                                txtadmissionstart.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["PreviousAdmissionStart"]);

                            //Admission end
                            if (dsGet.Tables[0].Rows[0]["PreviousAdmissionEnd"] != DBNull.Value)
                                txtadmissionend.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["PreviousAdmissionEnd"]);
                        }
                    }


                    //----------------TB History--------------------------
                    //TB History :
                    if (!String.IsNullOrEmpty(dsGet.Tables[0].Rows[0]["TBHistory"].ToString()))
                    {
                        rbListTBHistory.SelectedValue = (Convert.ToBoolean(dsGet.Tables[0].Rows[0]["TBHistory"])) ? "1" : "0";
                        if (rbListTBHistory.SelectedValue == "1")
                        {
                            visibleDiv("divcompletetxdate");
                            visibleDiv("divretreatmentdate");
                        }
                    }
                    //Complete TX Date
                    if (dsGet.Tables[0].Rows[0]["TBrxCompleteDate"] != DBNull.Value)
                        txtcompletetxdate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["TBrxCompleteDate"]);

                    //Retreatment Date
                    if (dsGet.Tables[0].Rows[0]["TBRetreatmentDate"] != DBNull.Value)
                        txtretreatmentdate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["TBRetreatmentDate"]);

                    //----------------------Immunisation Status----------------
                    //Immunisation Status
                    if (dsGet.Tables[0].Rows[0]["ImmunisationStatus"] != DBNull.Value)
                        ddlimmunisationstatus.SelectedValue = dsGet.Tables[0].Rows[0]["ImmunisationStatus"].ToString();

                    //PMTCT
                    if (dsGet.Tables[0].Rows[0]["PMTCT1StartDate"] != DBNull.Value)
                        txtpmtctdate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["PMTCT1StartDate"]);

                    // PMTCT Regimen :
                    if (dsGet.Tables[0].Rows[0]["PMTCT1Regimen"] != DBNull.Value)
                        txtpmtctregimen.Text = dsGet.Tables[0].Rows[0]["PMTCT1Regimen"].ToString();

                    //HAART Start date
                    if (dsGet.Tables[0].Rows[0]["HAART1StartDate"] != DBNull.Value)
                        txthaartstartdate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["HAART1StartDate"]);

                    // HAART Regimen :
                    if (dsGet.Tables[0].Rows[0]["HAART1Regimen"] != DBNull.Value)
                        txthaartregimen.Text = dsGet.Tables[0].Rows[0]["HAART1Regimen"].ToString();

                    //PEP Start date
                    if (dsGet.Tables[0].Rows[0]["PEP1StartDate"] != DBNull.Value)
                        txtpepstartdate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["PEP1StartDate"]);

                    //PEP Regimen
                    if (dsGet.Tables[0].Rows[0]["PEP1Regimen"] != DBNull.Value)
                        txtpepregimen.Text = dsGet.Tables[0].Rows[0]["PEP1Regimen"].ToString();


                    //Initial CD4 :
                    if (dsGet.Tables[0].Rows[0]["InitialCD4"] != DBNull.Value)
                        txtcd4.Text = dsGet.Tables[0].Rows[0]["InitialCD4"].ToString();

                    // Initial CD4% 
                    if (dsGet.Tables[0].Rows[0]["InitialCD4Percent"] != DBNull.Value)
                        txtcd4per.Text = dsGet.Tables[0].Rows[0]["InitialCD4Percent"].ToString();

                    //Initial CD4 date :
                    if (dsGet.Tables[0].Rows[0]["InitialCD4Date"] != DBNull.Value)
                        txtcd4date.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["InitialCD4Date"]);

                    //Highest CD4 ever
                    if (dsGet.Tables[0].Rows[0]["HighestCD4Ever"] != DBNull.Value)
                        txthighCD4ever.Text = dsGet.Tables[0].Rows[0]["HighestCD4Ever"].ToString();

                    //Highest CD4 ever % :
                    if (dsGet.Tables[0].Rows[0]["HighestCD4Percent"] != DBNull.Value)
                        txthighestcd4everper.Text = dsGet.Tables[0].Rows[0]["HighestCD4Percent"].ToString();

                    //Highest CD4 ever date
                    if (dsGet.Tables[0].Rows[0]["HighestCD4EverDate"] != DBNull.Value)
                        txthigcd4everdate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["HighestCD4EverDate"]);

                    //CD4 at ART initiation :
                    if (dsGet.Tables[0].Rows[0]["CD4atARTInitiation"] != DBNull.Value)
                        txtcd4artinitiation.Text = dsGet.Tables[0].Rows[0]["CD4atARTInitiation"].ToString();

                    //CD4 at ART initiation %
                    if (dsGet.Tables[0].Rows[0]["CD4AtARTInitiationPercent"] != DBNull.Value)
                        txtcd4artinitper.Text = dsGet.Tables[0].Rows[0]["CD4AtARTInitiationPercent"].ToString();

                    //CD4 at ART initiation date :
                    if (dsGet.Tables[0].Rows[0]["CD4atARTInitiationDate"] != DBNull.Value)
                        txtcd4artinitdate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["CD4atARTInitiationDate"]);

                    //Most recent CD4
                    if (dsGet.Tables[0].Rows[0]["MostRecentCD4"] != DBNull.Value)
                        txtmostrecent_cd4.Text = dsGet.Tables[0].Rows[0]["MostRecentCD4"].ToString();

                    //Most Recent CD4% :
                    if (dsGet.Tables[0].Rows[0]["MostRecentCD4Percent"] != DBNull.Value)
                        txtmostrecentcd4per.Text = dsGet.Tables[0].Rows[0]["MostRecentCD4Percent"].ToString();

                    //Most recent CD4 date :
                    if (dsGet.Tables[0].Rows[0]["MostRecentCD4Date"] != DBNull.Value)
                        txtmostrecentcd4date.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["MostRecentCD4Date"]);

                    // Previous viral load :
                    if (dsGet.Tables[0].Rows[0]["PreviousViralLoad"] != DBNull.Value)
                        txtpreviousviral_load.Text = dsGet.Tables[0].Rows[0]["PreviousViralLoad"].ToString();

                    //Previous viral load date 
                    if (dsGet.Tables[0].Rows[0]["PreviousViralLoadDate"] != DBNull.Value)
                        txtpreviousviralloaddate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["PreviousViralLoadDate"]);

                    //Other HIV related history :
                    if (dsGet.Tables[0].Rows[0]["OtherHIVRelatedHistory"] != DBNull.Value)
                        txtotherhivrelated_history.Text = dsGet.Tables[0].Rows[0]["OtherHIVRelatedHistory"].ToString();


                    //pyridoxine 5mg\kg and INH 10mg/kg :
                    if (dsGet.Tables[0].Rows[0]["ReminderIPT"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["ReminderIPT"].ToString() == "True")
                        {
                            this.chkpyridoxine.Checked = true;
                        }
                    }
                    //-----------------Physical Examination ?------------------------
                    if (dsGet.Tables[0].Rows[0]["OtherGeneralConditions"] != DBNull.Value)
                    {
                        //visibleDiv("divgeneralothercondition");
                        this.UcPE.txtOtherGeneralConditions.Text = dsGet.Tables[0].Rows[0]["OtherGeneralConditions"].ToString();
                    }
                    if (dsGet.Tables[0].Rows[0]["OtherAbdomenConditions"] != DBNull.Value)
                    {
                        //visibleDiv("divOtherAbdomenConditions");
                        this.UcPE.txtOtherAbdomenConditions.Text = dsGet.Tables[0].Rows[0]["OtherAbdomenConditions"].ToString();
                    }
                    if (dsGet.Tables[0].Rows[0]["OtherCardiovascularConditions"] != DBNull.Value)
                    {
                        //visibleDiv("divOtherCardiovascularConditions");
                        this.UcPE.txtOtherCardiovascularConditions.Text = dsGet.Tables[0].Rows[0]["OtherCardiovascularConditions"].ToString();
                    }

                    if (dsGet.Tables[0].Rows[0]["OtherOralCavityConditions"] != DBNull.Value)
                    {
                        //visibleDiv("divOtherOralCavityConditions");
                        this.UcPE.txtOtherOralCavityConditions.Text = dsGet.Tables[0].Rows[0]["OtherOralCavityConditions"].ToString();
                    }

                    if (dsGet.Tables[0].Rows[0]["OtherGenitourinaryConditions"] != DBNull.Value)
                    {
                       // visibleDiv("divOtherGenitourinaryConditions");
                        this.UcPE.txtOtherGenitourinaryConditions.Text = dsGet.Tables[0].Rows[0]["OtherGenitourinaryConditions"].ToString();
                    }

                    if (dsGet.Tables[0].Rows[0]["OtherCNSConditions"] != DBNull.Value)
                    {
                        //visibleDiv("divOtherCNSConditions");
                        this.UcPE.txtOtherCNSConditions.Text = dsGet.Tables[0].Rows[0]["OtherCNSConditions"].ToString();
                    }

                    if (dsGet.Tables[0].Rows[0]["OtherChestLungsConditions"] != DBNull.Value)
                    {
                        //visibleDiv("divOtherChestLungsConditions");
                        this.UcPE.txtOtherChestLungsConditions.Text = dsGet.Tables[0].Rows[0]["OtherChestLungsConditions"].ToString();
                    }

                    if (dsGet.Tables[0].Rows[0]["OtherSkinConditions"] != DBNull.Value)
                    {
                        //visibleDiv("divOtherSkinConditions");
                        this.UcPE.txtOtherSkinConditions.Text = dsGet.Tables[0].Rows[0]["OtherSkinConditions"].ToString();
                    }
                    if (dsGet.Tables[0].Rows[0]["OtherMedicalConditionNotes"] != DBNull.Value)
                        this.UcPE.txtOtherMedicalConditionNotes.Text = dsGet.Tables[0].Rows[0]["OtherMedicalConditionNotes"].ToString();

                    //----------------------Developmental milestones
                    // Milestone appropriate? :
                    if (!String.IsNullOrEmpty(dsGet.Tables[0].Rows[0]["MilestoneAppropriate"].ToString()))
                    {
                        rbListMilestone.SelectedValue = (Convert.ToBoolean(dsGet.Tables[0].Rows[0]["MilestoneAppropriate"])) ? "1" : "0";
                        if (rbListMilestone.SelectedValue == "0")
                            visibleDiv("divmilestoneshowhide");
                        //If No specify why inappropriate
                        if (dsGet.Tables[0].Rows[0]["ResonMilestoneInappropriate"] != DBNull.Value)
                            txtspecifywhyinappropriate.Text = dsGet.Tables[0].Rows[0]["ResonMilestoneInappropriate"].ToString();

                    }
                    //if (dsGet.Tables[0].Rows[0]["MilestoneAppropriate"] != System.DBNull.Value)
                    //{
                    //    if (dsGet.Tables[0].Rows[0]["MilestoneAppropriate"].ToString() == "True")
                    //    {
                    //        this.rdomilestoneappropriateyes.Checked = true;
                    //        visibleDiv("divmilestoneshowhidey");
                    //    }
                    //    else if (dsGet.Tables[0].Rows[0]["MilestoneAppropriate"].ToString() == "False")
                    //    {
                    //        this.rdomilestoneappropriateno.Checked = true;
                    //        visibleDiv("divmilestoneshowhiden");
                    //    }
                    //}



                    //------------------- Tests and labs-------------------
                    // Lab evaluation 
                    if (dsGet.Tables[0].Rows[0]["LabEvaluationPeads"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["LabEvaluationPeads"].ToString() == "True")
                        {
                            this.rdolabevaluationyes.Checked = true;
                            visibleDiv("divlabevaluatinshowhidey");
                        }
                        else if (dsGet.Tables[0].Rows[0]["LabEvaluationPeads"].ToString() == "False")
                        {
                            this.rdolabevaluationno.Checked = true;
                        }
                    }

                    TextBox txtlabdiagnosticinput = (TextBox)UCLabEval.FindControl("txtlabdiagnosticinput");
                    if (dsGet.Tables[0].Rows[0]["LabEvaluationDiagnosticInput"] != DBNull.Value)
                        txtlabdiagnosticinput.Text = dsGet.Tables[0].Rows[0]["LabEvaluationDiagnosticInput"].ToString();

                    //-------------  Drug Allergy and Toxicities ?---------------
                    //Specify ARV allergy 
                    if (dsGet.Tables[0].Rows[0]["SpecifyARVallergy"] != DBNull.Value)
                        txtarvallergy.Text = dsGet.Tables[0].Rows[0]["SpecifyARVallergy"].ToString();

                    //Specify antibiotic allergy 
                    if (dsGet.Tables[0].Rows[0]["SpecifyAntibioticAllery"] != DBNull.Value)
                        txtantibioticallergy.Text = dsGet.Tables[0].Rows[0]["SpecifyAntibioticAllery"].ToString();

                    //Specify other drug allergy
                    if (dsGet.Tables[0].Rows[0]["OtherDrugAllergy"] != DBNull.Value)
                        txtotherdrugallergy.Text = dsGet.Tables[0].Rows[0]["OtherDrugAllergy"].ToString();

                    //Specify other short term effects\
                    if (dsGet.Tables[0].Rows[0]["OtherShortTermEffects"] != DBNull.Value)
                    {
                        //visibleDiv("divshorttermeffecttxt");
                        txtspecityothershortterm.Text = dsGet.Tables[0].Rows[0]["OtherShortTermEffects"].ToString();
                    }
                    // Specify Other long term effects 
                    if (dsGet.Tables[0].Rows[0]["OtherLongtermEffects"] != DBNull.Value)
                    {
                        //visibleDiv("divlongtermeffecttxt");
                        txtspecifyotherlongterm.Text = dsGet.Tables[0].Rows[0]["OtherLongtermEffects"].ToString();
                    }
                    //Work up plan :
                    if (dsGet.Tables[0].Rows[0]["WorkUpPlan"] != DBNull.Value)
                        txtworkupplan.Text = dsGet.Tables[0].Rows[0]["WorkUpPlan"].ToString();

                    //OI Prophylaxis
                    if (dsGet.Tables[0].Rows[0]["OIProphylaxis"] != DBNull.Value)
                    {
                        ddloiprophylaxis.SelectedValue = dsGet.Tables[0].Rows[0]["OIProphylaxis"].ToString(); //dsGet.Tables[0].Rows[0]["RegimenPrescribed"].ToString();
                        if (ddloiprophylaxis.SelectedItem.Text == "Cotrimoxazole")
                        {
                            visibleDiv("divoiprophylaxisshowhidey");
                        }
                        if (ddloiprophylaxis.SelectedItem.Text == "Fluconazole")
                        {
                            visibleDiv("divFluconazoleshowhide");
                            //Fluconazole prescribed for :
                            if (dsGet.Tables[0].Rows[0]["ReasonFluconazolepresribed"] != DBNull.Value)
                                ddlfluconazole.SelectedValue = dsGet.Tables[0].Rows[0]["ReasonFluconazolepresribed"].ToString();
                        }
                        if (ddloiprophylaxis.SelectedItem.Text == "CTX and Fluconazol")
                        {
                            visibleDiv("divFluconazoleshowhide");
                            //Cotrimoxazole prescribed for
                            if (dsGet.Tables[0].Rows[0]["ReasonCTXpresribed"] != DBNull.Value)
                                ddlcotrimoxazole.SelectedValue = dsGet.Tables[0].Rows[0]["ReasonCTXpresribed"].ToString();
                        }
                        if (ddloiprophylaxis.SelectedItem.Text == "Other")
                        {
                            visibleDiv("divoiprophylasixothershowhide");
                            //Other (Specify)
                            if (dsGet.Tables[0].Rows[0]["OtherOIProphylaxis"] != DBNull.Value)
                                txtothercotrimoxazole.Text = dsGet.Tables[0].Rows[0]["OtherOIProphylaxis"].ToString();
                        }
                    }
                    this.UcPc.txtAdditionalComplaints.Text = dsGet.Tables[0].Rows[0]["PresentingComplaintsAdditionalNotes"].ToString();

                    //Other treatment :
                    if (dsGet.Tables[0].Rows[0]["OtherTreatment"] != DBNull.Value)
                        txtothertreatementcatrimozazole.Text = dsGet.Tables[0].Rows[0]["OtherTreatment"].ToString();
                }

                if (dsGet.Tables[1].Rows.Count > 0)
                {
                    if (dsGet.Tables[1].Rows[0]["ConfirmHIVPosDate"] != DBNull.Value)
                        txtdateofhivdiagnosis.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[1].Rows[0]["ConfirmHIVPosDate"]);

                }
                //---------------section vital sign-------------------
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
                    if (dsGet.Tables[3].Rows[0]["WeightForAge"] != DBNull.Value)
                        this.idVitalSign.lblWA.Text = dsGet.Tables[3].Rows[0]["WeightForAge"].ToString();
                    if (dsGet.Tables[3].Rows[0]["WeightForHeight"] != DBNull.Value)
                        this.idVitalSign.lblWH.Text = dsGet.Tables[3].Rows[0]["WeightForHeight"].ToString();

                    if (dsGet.Tables[3].Rows[0]["BMIz"] != DBNull.Value)
                        this.idVitalSign.lblBMIz.Text = dsGet.Tables[3].Rows[0]["BMIz"].ToString();
                    

                    visibleDiv("hideVitalYNy");

   
                }
                FillCheckboxlist(cblchroniccondition, dsGet.Tables[5], "ChronicCondition");
                //FillCheckboxlist(chkLongTermMedication, dsGet.Tables[34], "PaedCurrentLongTermMedications");
                foreach (DataRow drRow in dsGet.Tables[34].Rows)
                {
                    //for (int j = 0; j <= dsGet.Tables[33].Rows.Count - 1; j++)
                    //{
                    for (int i = 0; i < chkLongTermMedication.Items.Count; i++)
                    {
                        if (Convert.ToInt32(drRow["ValueId"]) == Convert.ToInt32(chkLongTermMedication.Items[i].Value))
                        {
                            chkLongTermMedication.Items[i].Selected = true;
                            if (drRow["Name"].ToString().Trim() == "Other")
                            {
                                visibleDiv("divLongTermMedication");
                                txOtherLongTermMedications.Text = dsGet.Tables[0].Rows[0]["OtherCurrentLongTermMedications"].ToString();
                            }
                        }
                    }
                    // }
                }
                //UcWhostaging.ddlWABStage.SelectedItem.Value = dsGet.Tables[0].Rows[0]["WABStage"].ToString();
                //UcWhostaging.ddltannerstaging.SelectedItem.Value = dsGet.Tables[0].Rows[0]["TannerStaging"].ToString();

                FillCheckboxlist(cbllabevaluation, dsGet.Tables[9], "SpecifyLabEvaluation");
                FillCheckboxlist(cblDrugAllergiesToxicitiesPaeds, dsGet.Tables[10], "DrugAllergiesToxicitiesPaeds");
                FillCheckboxlist(cblshorttermeffects, dsGet.Tables[11], "ShortTermEffects");
                FillCheckboxlist(cbllongtermeffects, dsGet.Tables[12], "LongTermEffects");

                if (dsGet.Tables[19].Rows.Count > 0)
                {
                    ViewState["presentingcomplains"] = dsGet.Tables[19];
                    visibleDiv("hidePresentingYNy");
                }
                //binding the data in Presenting user control 
                bindPresentingComplaint(dsGet);

                //TextBox txtAdditionalComplaints = (TextBox)UcPc.FindControl("txtAdditionPresentingComplaints");
                if (dsGet.Tables[0].Rows.Count > 0)
                    UcPc.txtOtherPresentingComplaints.Text = dsGet.Tables[0].Rows[0]["otherpresentingcomplaints"].ToString();

                CheckBoxList chkPEGeneral = (CheckBoxList)this.UcPE.FindControl("cblGeneralConditions");
                FillCheckboxlist(chkPEGeneral, dsGet.Tables[20], "GeneralConditions");
                //Cardiovascular conditions
                CheckBoxList chkPECardiovascular = (CheckBoxList)this.UcPE.FindControl("cblCardiovascularConditions");
                FillCheckboxlist(chkPECardiovascular, dsGet.Tables[21], "CardiovascularConditions");
                //CNS 
                CheckBoxList chkPECNS = (CheckBoxList)this.UcPE.FindControl("cblCNSConditions");
                FillCheckboxlist(chkPECNS, dsGet.Tables[22], "CNSConditions");
                //Oral cavity
                CheckBoxList chkPEOralCavity = (CheckBoxList)this.UcPE.FindControl("cblOralCavityConditions");
                FillCheckboxlist(chkPEOralCavity, dsGet.Tables[23], "OralCavityConditions");
                //Chest Lungs
                CheckBoxList chkPEChestLungs = (CheckBoxList)this.UcPE.FindControl("cblChestLungsConditions");
                FillCheckboxlist(chkPEChestLungs, dsGet.Tables[24], "ChestLungsConditions");
                //Genitourinary 
                CheckBoxList chkPEGenitourinary = (CheckBoxList)this.UcPE.FindControl("cblGenitalUrinaryConditions");
                FillCheckboxlist(chkPEGenitourinary, dsGet.Tables[25], "GenitalUrinaryConditions");
                //Skin 
                CheckBoxList chkPESkin = (CheckBoxList)this.UcPE.FindControl("cblSkinConditions");
                FillCheckboxlist(chkPESkin, dsGet.Tables[26], "SkinConditions");
                //Abdomen conditions
                CheckBoxList chkPEabdomen = (CheckBoxList)this.UcPE.FindControl("cblAbdomenConditions");
                FillCheckboxlist(chkPEabdomen, dsGet.Tables[27], "AbdomenConditions");

                ////WHO Stage I CurrentWHOStageIConditions
                //Bind the values in user control WHO Stage 1
                if (dsGet.Tables[28].Rows.Count > 0)
                {
                    foreach (DataRow row in dsGet.Tables[28].Rows)
                    {
                        for (int i = 0; i < this.UcWhostaging.gvWHO1.Rows.Count; i++)
                        {
                            Label lblWHOId = (Label)UcWhostaging.gvWHO1.Rows[i].FindControl("lblwho1");
                            CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO1.Rows[i].FindControl("Chkwho1");
                            HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date");
                            HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date1");
                            if (Convert.ToInt32(row["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                            {
                                chkWHOId.Checked = true;
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", row["DateField1"]);
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", row["DateField2"]);
                            }
                        }
                    }
                }
                ////WHO Stage 2
                //Bind the values in user control WHO Stage 2
                if (dsGet.Tables[29].Rows.Count > 0)
                {
                    foreach (DataRow row in dsGet.Tables[29].Rows)
                    {
                        for (int i = 0; i < this.UcWhostaging.gvWHO2.Rows.Count; i++)
                        {
                            Label lblWHOId = (Label)UcWhostaging.gvWHO2.Rows[i].FindControl("lblwho2");
                            CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO2.Rows[i].FindControl("Chkwho2");
                            HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date");
                            HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date1");
                            if (Convert.ToInt32(row["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                            {
                                chkWHOId.Checked = true;
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", row["DateField1"]);
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", row["DateField2"]);
                            }
                        }
                    }
                }
                ////WHO Stage 3
                //Bind the values in user control WHO Stage 3
                if (dsGet.Tables[30].Rows.Count > 0)
                {
                    foreach (DataRow row in dsGet.Tables[30].Rows)
                    {
                        for (int i = 0; i < this.UcWhostaging.gvWHO3.Rows.Count; i++)
                        {
                            Label lblWHOId = (Label)UcWhostaging.gvWHO3.Rows[i].FindControl("lblwho3");
                            CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO3.Rows[i].FindControl("Chkwho3");
                            HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date");
                            HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date1");
                            if (Convert.ToInt32(row["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                            {
                                chkWHOId.Checked = true;
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", row["DateField1"]);
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", row["DateField2"]);
                            }
                        }
                    }
                }
                ////WHO Stage 4
                //Bind the values in user control WHO Stage 4
                if (dsGet.Tables[31].Rows.Count > 0)
                {
                    foreach (DataRow row in dsGet.Tables[31].Rows)
                    {
                        for (int i = 0; i < this.UcWhostaging.gvWHO4.Rows.Count; i++)
                        {
                            Label lblWHOId = (Label)UcWhostaging.gvWHO4.Rows[i].FindControl("lblwho4");
                            CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO4.Rows[i].FindControl("Chkwho4");
                            HtmlInputText txtWHODate1 = (HtmlInputText)UcWhostaging.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date");
                            HtmlInputText txtWHODate2 = (HtmlInputText)UcWhostaging.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date1");
                            if (Convert.ToInt32(row["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                            {
                                chkWHOId.Checked = true;
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", row["DateField1"]);
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", row["DateField2"]);
                            }
                        }
                    }
                }
                //------------Staging at initial evaluation ?
                if (dsGet.Tables[0].Rows.Count > 0)
                {
                    if (dsGet.Tables[0].Rows[0]["InitiationWHOstage"] != DBNull.Value)
                        UcWhostaging.ddlInitiationWHOstage.SelectedValue = dsGet.Tables[0].Rows[0]["InitiationWHOstage"].ToString();


                    //HIV associated conditions
                    if (dsGet.Tables[0].Rows[0]["HIVAssociatedConditionsPeads"] != DBNull.Value)
                        UcWhostaging.ddlhivassociated.SelectedValue = dsGet.Tables[0].Rows[0]["HIVAssociatedConditionsPeads"].ToString();

                    //Tanner staging
                    if (dsGet.Tables[0].Rows[0]["TannerStaging"] != DBNull.Value)
                        UcWhostaging.ddltannerstaging.SelectedValue = dsGet.Tables[0].Rows[0]["TannerStaging"].ToString();

                    //Mernarche 16 ju
                    if (dsGet.Tables[0].Rows[0]["Menarche"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["Menarche"].ToString() == "True")
                        {
                            UcWhostaging.radbtnMernarcheyes.Checked = true;
                            UcWhostaging.radbtnMernarcheno.Checked = false;
                            visibleDiv("divmenarchedatehshowhide");
                            ////Menarche Date :
                            if (!String.IsNullOrEmpty(dsGet.Tables[0].Rows[0]["MenarcheDate"].ToString()))
                                UcWhostaging.txtmenarchedate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["MenarcheDate"]);

                        }
                        else if (dsGet.Tables[0].Rows[0]["Menarche"].ToString() == "False")
                        {
                            UcWhostaging.radbtnMernarcheyes.Checked = false;
                            UcWhostaging.radbtnMernarcheno.Checked = true;
                        }
                    }
                }

                //HIV associated conditions
                if (dsGet.Tables[2].Rows.Count > 0)
                {
                    if (dsGet.Tables[2].Rows[0]["WhoStage"] != DBNull.Value)
                        UcWhostaging.ddlwhostage1.SelectedValue = dsGet.Tables[2].Rows[0]["WhoStage"].ToString();
                    //WHO Stage at initiation (Transfer in)
                    if (dsGet.Tables[2].Rows[0]["WABStage"] != DBNull.Value)
                        UcWhostaging.ddlWABStage.SelectedValue = dsGet.Tables[2].Rows[0]["WABStage"].ToString();

                }
                

               

                
            }
            else
            {
                //hdnVisitId.Value = "0";
                txtVisitDate.Value = Application["AppCurrentDate"].ToString();
            }
        }
        private void bindPresentingComplaint(DataSet ds)
        {
            for (int j = 0; j <= ds.Tables[19].Rows.Count - 1; j++)
            {
                for (int i = 0; i < this.UcPc.gvPresentingComplaints.Rows.Count; i++)
                {
                    Label lblPComplaintsId = (Label)UcPc.gvPresentingComplaints.Rows[i].FindControl("lblPresenting");
                    CheckBox chkPComplaints = (CheckBox)UcPc.gvPresentingComplaints.Rows[i].FindControl("ChkPresenting");
                    TextBox txtPComplaints = (TextBox)UcPc.gvPresentingComplaints.Rows[i].FindControl("txtPresenting");
                    if (Convert.ToInt32(ds.Tables[19].Rows[j]["ValueId"]) == Convert.ToInt32(lblPComplaintsId.Text))
                    {
                        if (ds.Tables[19].Rows[j]["Name"].ToString().ToLower() == "other")
                        {
                            visibleDiv("DivOther");
                        }
                        chkPComplaints.Checked = true;
                        txtPComplaints.Text = ds.Tables[19].Rows[j]["NumericField"].ToString();
                    }
                }
            }
        }
        public void FillCheckboxlist(CheckBoxList chk, DataTable dt, string name)
        {
            string script = string.Empty;
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < chk.Items.Count; j++)
                    {
                        if (chk.Items[j].Value == dt.Rows[i]["ValueID"].ToString())
                        {
                            chk.Items[j].Selected = true;
                            
                            if (name == "GeneralConditions")
                            {
                                if (chk.Items[j].Text.Trim().ToLower() == "other")
                                {
                                    visibleDiv("divgeneralothercondition");
                                }
                            }
                            if (name == "CardiovascularConditions")
                            {
                                if (chk.Items[j].Text.Trim().ToLower() == "other")
                                {
                                    visibleDiv("divOtherCardiovascularConditions");
                                }
                            }
                            if (name == "CNSConditions")
                            {
                                if (chk.Items[j].Text.Trim().ToLower() == "other")
                                {
                                    visibleDiv("divOtherCNSConditions");
                                }
                            }
                            if (name == "OralCavityConditions")
                            {
                                if (chk.Items[j].Text.Trim().ToLower() == "other")
                                {
                                    visibleDiv("divOtherOralCavityConditions");
                                }
                            }
                            if (name == "ChestLungsConditions")
                            {
                                if (chk.Items[j].Text.Trim().ToLower() == "other")
                                {
                                    visibleDiv("divOtherChestLungsConditions");
                                }
                            }
                            if (name == "GenitalUrinaryConditions")
                            {
                                if (chk.Items[j].Text.Trim().ToLower() == "other")
                                {
                                    visibleDiv("divOtherGenitourinaryConditions");
                                }
                            }
                            if (name == "AbdomenConditions")
                            {
                                if (chk.Items[j].Text.Trim().ToLower() == "other")
                                {
                                    visibleDiv("divOtherAbdomenConditions");
                                }
                            }
                            if (name == "SkinConditions")
                            {
                                if (chk.Items[j].Text.Trim().ToLower() == "other")
                                {
                                    visibleDiv("divOtherSkinConditions");
                                }
                            }
                            if (name == "ChronicCondition")
                            {
                                if (chk.Items[j].Text == "Other specify")
                                {
                                    visibleDiv("hideOtherLTM");
                                }
                            }
                            if (name == "TBSideEffects")
                            {
                                if (chk.Items[j].Text == "Other Side effects (specify)")
                                {
                                    visibleDiv("divothertbshowhideyes");
                                }
                            }
                            if (name == "DrugAllergiesToxicitiesPaeds")
                            {
                                if (chk.Items[j].Text == "ARV")
                                {
                                    visibleDiv("divspecifyarvallergyshowhideA");
                                }
                                if (chk.Items[j].Text == "Antibiotic")
                                {
                                    visibleDiv("divspecifyantibioticshowhideA");
                                }
                                if (chk.Items[j].Text == "Other")
                                {
                                    visibleDiv("divspecifyotherdrugshowhideA");
                                }
                            }
                            if (name == "LongTermEffects")
                            {
                                if (chk.Items[j].Text == "Other specify")
                                {
                                    visibleDiv("divlongtermeffecttxt");
                                }
                            }
                            if (name == "ShortTermEffects")
                            {
                                if (chk.Items[j].Text == "Other Specify")
                                {
                                    visibleDiv("divshorttermeffecttxt");
                                }
                            }
                            if (name == "Counselling")
                            {
                                if (chk.Items[j].Text == "Other")
                                {
                                    visibleDiv("divothercounsellingshowhidey");
                                }
                            }
                        }
                    }
                }

            }
        }
        private void triage_Save(int qltyFlag)
        {
            Hashtable theHT = new Hashtable();
            DataTable dtSave = CreateTempTable();
            try
            {
                theHT.Add("visitDate", txtVisitDate.Value);
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Session["PatientVisitId"]);
                theHT.Add("locationID", Session["AppLocationId"]);
                //Child accompanied by
                theHT.Add("ChildAccompaniedBy", txtchildaccompaniedby.Text);
                //Child Primary Caregiver
                theHT.Add("PrimaryCareGiver", txtchildcaregiver.Text);
                //Child diagnosis confirmed
                theHT.Add("ChildDiagnosisConfirmed", ddlchilddiagnosis.SelectedItem.Value);
                //Date of HIV Diagnosis
                theHT.Add("ConfirmHIVPosDate", txtdateofhivdiagnosis.Value);

                //Disclosure status
                theHT.Add("DisclosureStatus", ddldisclosurestatus.SelectedItem.Value);
                //Specify reason not discloesd
                theHT.Add("ReasonNotDisclosed", txtspecifyreason.Text);
                //Specify other disclosure status
                theHT.Add("OtherDisclosureReason", txtspecifyotherdisclosurestatus.Text);

                //Father alive
                theHT.Add("FatherAlive", rbListFatherAlive.SelectedValue);
                //Date of fathers death
                theHT.Add("DateOfDeathFather", txtdateoffatherdeath.Value);
                //Mother alive
                theHT.Add("MotherAlive", rbListMotherAlive.SelectedValue);
                //Date of mothers death
                theHT.Add("DateOfDeathMother", txtdateofmotherdeath.Value);
                //Have you been referred from another facility :
                //int referredanotherfacility = rdoreferedfromfacilityyes.Checked ? 1 : rdoreferedfromfacilityno.Checked ? 0 : 9;
                theHT.Add("ChildReferred", rbListRefFacility.SelectedValue);
                //Specify facility referred from :
                theHT.Add("ChildReferredFrom", txtspecifyfacility.Text);

                //Is the child on ART
                theHT.Add("CurrentlyOnHAART", rbListChildART.SelectedValue);
                //Current regimen line 
                theHT.Add("CurrentARTRegimenLine", ddlcurrentregimenline.SelectedItem.Value);
                //Current ART regimen
                theHT.Add("CurrentARTRegimen", ddlcurrentartregimen.SelectedItem.Value);
                //Current ART Regimen Began Date 
                theHT.Add("CurrentARTRegimenDate", txtcurrentartregimendate.Value);

                //Is the child on CTX :                
                theHT.Add("CurrentlyOnCTX", rbListChildCTX.SelectedValue);
                //Health education given?
                theHT.Add("HealthEducation", rbListHealthEducation.SelectedValue);
                //Schooling Status
                theHT.Add("SchoolingStatus", ddlschoolingstatus.SelectedItem.Value);
                //Highest level attained
                theHT.Add("HighestLevelAttained", ddlhighestlevelattained.SelectedItem.Value);
                //Is client a member of a support group
                theHT.Add("HIVSupportGroup", rbListSupportGroup.SelectedValue);
                //HIV support group membership
                theHT.Add("HIVSupportGroupMembership", txthivsupportgroupmembership.Text);

                //--------------section vital sign
                if (this.idVitalSign.txtTemp.Text != "")
                {
                    theHT.Add("Temperature", this.idVitalSign.txtTemp.Text.ToString());
                }
                else
                {
                    theHT.Add("Temperature", "0");
                }
                if (this.idVitalSign.txtRR.Text != "")
                {
                    theHT.Add("RespirationRate", this.idVitalSign.txtRR.Text);
                }
                else
                {
                    theHT.Add("RespirationRate", "0");
                }
                if (this.idVitalSign.txtHR.Text != "")
                {
                    theHT.Add("HeartRate", this.idVitalSign.txtHR.Text);
                }
                else
                {
                    theHT.Add("HeartRate", "0");
                }
                if (this.idVitalSign.txtHeight.Text != "")
                {
                    theHT.Add("Height", this.idVitalSign.txtHeight.Text);
                }
                else
                {
                    theHT.Add("Height", "0");
                }
                if (this.idVitalSign.txtWeight.Text != "")
                {
                    theHT.Add("Weight", this.idVitalSign.txtWeight.Text);
                }
                else
                {
                    theHT.Add("Weight", "0");
                }
                if (this.idVitalSign.txtBPDiastolic.Text != "")
                {
                    theHT.Add("DiastolicBloodPressure", this.idVitalSign.txtBPDiastolic.Text);
                }
                else
                {
                    theHT.Add("DiastolicBloodPressure", "0");
                }
                if (this.idVitalSign.txtBPSystolic.Text != "")
                {
                    theHT.Add("SystolicBloodPressure", this.idVitalSign.txtBPSystolic.Text);
                }
                else
                {
                    theHT.Add("SystolicBloodPressure", "0");
                }

                theHT.Add("BMI", 0);
                theHT.Add("HeadCircumference", this.idVitalSign.txtheadcircumference.Text);
                theHT.Add("WeightForAge", this.idVitalSign.lblWA.Text);
                theHT.Add("WeightForHeight", this.idVitalSign.lblWH.Text);
                theHT.Add("BMIz",this.idVitalSign.lblBMIz.Text);
                theHT.Add("NursesComments", this.idVitalSign.txtnursescomments.Text);
                theHT.Add("qltyFlag", qltyFlag);

                //Patient referred to:
                CheckBoxList cblReferredTo = (CheckBoxList)this.idVitalSign.FindControl("cblReferredTo");
                dtSave = GetCheckBoxListValues(cblReferredTo, dtSave, "RefferedToFUpF");
                theHT.Add("PatientReferredOtherSpecialistClinic", this.idVitalSign.txtReferToSpecialistClinic.Text);
                theHT.Add("PatientReferredOtherSpecify", this.idVitalSign.txtSpecifyOtherRefferedTo.Text);

                theHT.Add("startTime", startTime);
                theHT.Add("userID", Convert.ToInt32(Session["AppUserId"]));
                KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
                DataSet DsReturns = KNHPEP.SaveUpdatePaediatricIE_TriageTab(theHT, dtSave);
                if (DsReturns.Tables[0].Rows.Count > 0)
                {
                    Session["PatientVisitId"] = DsReturns.Tables[0].Rows[0]["Visit_Id"].ToString();
                }
                //string saveupdate = string.Empty;
                if (Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]) > 0)
                {
                    //if (Convert.ToInt32(Session["PatientVisitId"].ToString()) > 0)
                    //{
                    //    saveupdate = "Update";
                    //}
                    //else
                    //{
                    //    saveupdate = "Save";
                    //}
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "saveSuc", "alert('Data on Triage tab saved successfully.');", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "saveTriage", "alert('Data on Triage tab saved successfully.');", true);
                    SaveCancel("Triage");
                    //BindExistingData();
                    tabControl.ActiveTabIndex = 1;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
        }
        private void SaveCancel(string tabname)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            IQCareMsgBox.NotifyAction(tabname + " Tab saved successfully.", "KNH Paediatric Initial Evaluation", true, this, "");
           
        }
        private void clinicalHistory_Save(int qltyFlag)
        {
            Hashtable theHT = new Hashtable();
            DataTable dtSave = CreateTempTable();
            try
            {
                ////------Presenting Complaints------------------
                dtSave = PresentingComplaints(dtSave, "PresentingComplaints");
                //Presenting complaints additional notes
                theHT.Add("PresentingComplaintsAdditionalNotes", UcPc.txtAdditionalComplaints.Text);
                //TextBox txtAdditionalComplaints = (TextBox)UcPc.FindControl("txtAdditionPresentingComplaints");
                theHT.Add("OtherPresentingComplaints", UcPc.txtOtherPresentingComplaints.Text);
                //If schooling,current school perfomance :
                theHT.Add("SchoolPerfomance", ddlschoolperfomance.SelectedItem.Value);

                //-------------Medical history (Disease, diagnosis and treatment)
                // Medical history
                int medicalhistory = rdomedicalhistoryyes.Checked ? 1 : rdomedicalhistoryno.Checked ? 0 : 9;
                theHT.Add("MedicalHistory", medicalhistory);
                //Specify Medical history :
                theHT.Add("OtherMedicalHistorySpecify", txtspecifymedicalhistory.Text);
                //Chronic condition
                dtSave = GetCheckBoxListValues(cblchroniccondition, dtSave, "ChronicCondition");
                //Specify other chronic condition
                theHT.Add("OtherChronicCondition", txtotherchroniccondition.Text);

                //Previously admitted :
                //int previouslyadmitted = rdopreviouslyadmittedyes.Checked ? 1 : rdopreviouslyadmittedno.Checked ? 0 : 9;
                theHT.Add("PreviousAdmission", rbListPreviouslyAdmitt.SelectedValue);
                // Diagnosis :
                theHT.Add("PreviousAdmissionDiagnosis", txtdiagnosis.Text);
                //Admission start 
                theHT.Add("PreviousAdmissionStart", txtadmissionstart.Value);
                //Admission end
                theHT.Add("PreviousAdmissionEnd", txtadmissionend.Value);

                //----------------TB History--------------------------
                //TB History :
                //int tbhistory = rdotvhistoryyes.Checked ? 1 : rdotvhistoryno.Checked ? 0 : 9;
                theHT.Add("TBHistory", rbListTBHistory.SelectedValue);
                //Complete TX Date
                theHT.Add("TBrxCompleteDate", txtcompletetxdate.Value);
                //Retreatment Date
                theHT.Add("TBRetreatmentDate", txtretreatmentdate.Value);
                //----------------------Immunisation Status----------------
                //Immunisation Status
                theHT.Add("ImmunisationStatus", ddlimmunisationstatus.SelectedItem.Value);
                //---------------------ARV Exposure----------------------
                //PMTCT
                theHT.Add("PMTCT1StartDate", txtpmtctdate.Value);
                // PMTCT Regimen :
                theHT.Add("PMTCT1Regimen", txtpmtctregimen.Text);
                //HAART Start date
                theHT.Add("HAART1StartDate", txthaartstartdate.Value);
                // HAART Regimen :
                theHT.Add("HAART1Regimen", txthaartregimen.Text);
                //PEP Start date
                theHT.Add("PEP1StartDate", txtpepstartdate.Value);
                //PEP Regimen
                theHT.Add("PEP1Regimen", txtpepregimen.Text);
                //--------------------HIV Related History----------------
                //Initial CD4 :
                theHT.Add("InitialCD4", txtcd4.Text);
                // Initial CD4% 
                theHT.Add("InitialCD4Percent", txtcd4per.Text);
                //Initial CD4 date :
                theHT.Add("InitialCD4Date", txtcd4date.Value);
                //Highest CD4 ever
                theHT.Add("HighestCD4Ever", txthighCD4ever.Text);
                //Highest CD4 ever % :
                theHT.Add("HighestCD4Percent", txthighestcd4everper.Text);
                //Highest CD4 ever date
                theHT.Add("HighestCD4EverDate", txthigcd4everdate.Value);
                //CD4 at ART initiation :
                theHT.Add("CD4atARTInitiation", txtcd4artinitiation.Text);
                //CD4 at ART initiation %
                theHT.Add("CD4AtARTInitiationPercent", txtcd4artinitper.Text);
                //CD4 at ART initiation date :
                theHT.Add("CD4atARTInitiationDate", txtcd4artinitdate.Value);
                //Most recent CD4
                theHT.Add("MostRecentCD4", txtmostrecent_cd4.Text);
                //Most Recent CD4% :
                theHT.Add("MostRecentCD4Percent", txtmostrecentcd4per.Text);
                //Most recent CD4 date :
                theHT.Add("MostRecentCD4Date", txtmostrecentcd4date.Value);
                // Previous viral load :
                theHT.Add("PreviousViralLoad", txtpreviousviral_load.Text);
                //Previous viral load date 
                theHT.Add("PreviousViralLoadDate", txtpreviousviralloaddate.Value);
                //Other HIV related history :
                theHT.Add("OtherHIVRelatedHistory", txtotherhivrelated_history.Text);

                theHT.Add("visitDate", txtVisitDate.Value);
                theHT.Add("startTime", startTime);
                theHT.Add("userID", Convert.ToInt32(Session["AppUserId"]));
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Session["PatientVisitId"]);
                KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
                DataSet DsReturns = KNHPEP.SaveUpdatePaediatricIE_ClinicalHistoryTab(theHT, dtSave);
                SaveCancel("Clinical History");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "saveClinical", "alert('Data on Clinical History tab saved successfully.');", true);
                //BindExistingData();
                tabControl.ActiveTabIndex = 2;
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
        }
        private void examination_Save(int qltyFlag)
        {
            Hashtable theHT = new Hashtable();
            DataTable dtSave = CreateTempTable();
            try
            {
                //Logn Term
                dtSave = GetCheckBoxListValues(chkLongTermMedication, dtSave, "PaedCurrentLongTermMedications");
                //Diagnosis          
                dtSave = GetCheckBoxListValues(cblDiagnosis, dtSave, "Diagnosis");
                //LabEvaluation
                dtSave = GetCheckBoxListValues(cbllabevaluation, dtSave, "SpecifyLabEvaluation");
                CheckBoxList chkPEGeneral = (CheckBoxList)this.UcPE.FindControl("cblGeneralConditions");
                dtSave = GetCheckBoxListValues(chkPEGeneral, dtSave, "GeneralConditions");
                //Cardiovascular conditions
                CheckBoxList chkPECardiovascular = (CheckBoxList)this.UcPE.FindControl("cblCardiovascularConditions");
                dtSave = GetCheckBoxListValues(chkPECardiovascular, dtSave, "CardiovascularConditions");
                //CNS 
                CheckBoxList chkPECNS = (CheckBoxList)this.UcPE.FindControl("cblCNSConditions");
                dtSave = GetCheckBoxListValues(chkPECNS, dtSave, "CNSConditions");
                //Oral cavity
                CheckBoxList chkPEOralCavity = (CheckBoxList)this.UcPE.FindControl("cblOralCavityConditions");
                dtSave = GetCheckBoxListValues(chkPEOralCavity, dtSave, "OralCavityConditions");
                //Chest Lungs
                CheckBoxList chkPEChestLungs = (CheckBoxList)this.UcPE.FindControl("cblChestLungsConditions");
                dtSave = GetCheckBoxListValues(chkPEChestLungs, dtSave, "ChestLungsConditions");
                //Genitourinary 
                CheckBoxList chkPEGenitourinary = (CheckBoxList)this.UcPE.FindControl("cblGenitalUrinaryConditions");
                dtSave = GetCheckBoxListValues(chkPEGenitourinary, dtSave, "GenitalUrinaryConditions");
                //Skin 
                CheckBoxList chkPESkin = (CheckBoxList)this.UcPE.FindControl("cblSkinConditions");
                dtSave = GetCheckBoxListValues(chkPESkin, dtSave, "SkinConditions");
                //Abdomen conditions
                CheckBoxList chkPEabdomen = (CheckBoxList)this.UcPE.FindControl("cblAbdomenConditions");
                dtSave = GetCheckBoxListValues(chkPEabdomen, dtSave, "AbdomenConditions");
                //WHO Stage I
                GridView gdviewwho1 = (GridView)UcWhostaging.FindControl("gvWHO1");
                dtSave = InsertMultiSelectList(gdviewwho1, dtSave, "WHOStageIConditions");
                // WHO Stage II
                GridView gdviewwho2 = (GridView)UcWhostaging.FindControl("gvWHO2");
                dtSave = InsertMultiSelectList(gdviewwho2, dtSave, "WHOStageIIConditions");
                // WHO Stage III
                GridView gdviewwho3 = (GridView)UcWhostaging.FindControl("gvWHO3");
                dtSave = InsertMultiSelectList(gdviewwho3, dtSave, "WHOStageIIIConditions");
                // WHO Stage IV
                GridView gdviewwho4 = (GridView)UcWhostaging.FindControl("gvWHO4");
                dtSave = InsertMultiSelectList(gdviewwho4, dtSave, "WHOStageIVConditions");

                //-----------------Physical Examination ?------------------------
                theHT.Add("OtherCurrentLongTermMedications", txOtherLongTermMedications.Text);
                theHT.Add("OtherMedicalConditionNotes", this.UcPE.txtOtherMedicalConditionNotes.Text);
                theHT.Add("OtherGeneralConditions", this.UcPE.txtOtherGeneralConditions.Text);
                theHT.Add("OtherAbdomenConditions", this.UcPE.txtOtherAbdomenConditions.Text);
                theHT.Add("OtherCardiovascularConditions", this.UcPE.txtOtherCardiovascularConditions.Text);
                theHT.Add("OtherOralCavityConditions", this.UcPE.txtOtherOralCavityConditions.Text);
                theHT.Add("OtherGenitourinaryConditions", this.UcPE.txtOtherGenitourinaryConditions.Text);
                theHT.Add("OtherCNSConditions", this.UcPE.txtOtherCNSConditions.Text);
                theHT.Add("OtherChestLungsConditions", this.UcPE.txtOtherChestLungsConditions.Text);
                theHT.Add("OtherSkinConditions", this.UcPE.txtOtherSkinConditions.Text);
                theHT.Add("HAARTImpression", ddlHAARTImpression.SelectedValue);
                theHT.Add("Diagnosis", 0);
                theHT.Add("HIVRelatedOI", txtHIVRelatedOI.Text.Trim());
                theHT.Add("NonHIVRelatedOI", txtNonHIVRelatedOI.Text.Trim());
                theHT.Add("HAARTexperienced", "");
                theHT.Add("OtherHAARTImpression", txtOtherHAARTImpression.Text.Trim());

                TextBox txtlabdiagnosticinput = (TextBox)UCLabEval.FindControl("txtlabdiagnosticinput");
                theHT.Add("LabEvaluationDiagnosticInput", txtlabdiagnosticinput.Text);
                //----------------------Developmental milestones
                // Milestone appropriate? :
                //int milestone = rdomilestoneappropriateyes.Checked ? 1 : rdomilestoneappropriateno.Checked ? 0 : 9;
                theHT.Add("MilestoneAppropriate", rbListMilestone.SelectedValue);
                //If No specify why inappropriate
                theHT.Add("ResonMilestoneInappropriate", txtspecifywhyinappropriate.Text);

                //------------------- Tests and labs-------------------
                // Lab evaluation 
                int labevaluation = rdolabevaluationyes.Checked ? 1 : rdolabevaluationno.Checked ? 0 : 9;
                theHT.Add("LabEvaluationPeads", labevaluation);

                //------------Staging at initial evaluation ?
                //------------Staging at initial evaluation ?
                //WHO Stage at initiation (Transfer in)
                theHT.Add("InitiationWHOstage", UcWhostaging.ddlInitiationWHOstage.SelectedItem.Value);
                //HIV associated conditions
                theHT.Add("HIVAssociatedConditionsPeads", UcWhostaging.ddlhivassociated.SelectedItem.Value);
                //Current WHO Stage

                theHT.Add("PeadiatricNutritionAssessment", UcWhostaging.ddlwhostage1.SelectedItem.Value);
                //WAB Stage
                theHT.Add("WABStage", UcWhostaging.ddlWABStage.SelectedItem.Value);
                //Tanner staging
                theHT.Add("TannerStaging", UcWhostaging.ddltannerstaging.SelectedItem.Value);
                //Mernarche
                String mernache = UcWhostaging.radbtnMernarcheyes.Checked ? "1" : UcWhostaging.radbtnMernarcheno.Checked ? "0" : "NULL";
                theHT.Add("Menarche", mernache);
                //Menarche Date :
                theHT.Add("MenarcheDate", UcWhostaging.txtmenarchedate.Value);

                theHT.Add("visitDate", txtVisitDate.Value);
                theHT.Add("startTime", startTime);
                theHT.Add("userID", Convert.ToInt32(Session["AppUserId"]));
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Session["PatientVisitId"]);

                KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
                DataSet DsReturns = KNHPEP.SaveUpdatePaediatricIE_ExaminationTab(theHT, dtSave);
                SaveCancel("Examination");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "saveExam", "alert('Data on Examination tab saved successfully.');", true);
                //BindExistingData();
                tabControl.ActiveTabIndex = 4;
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
        }
        private void management_Save(int qltyFlag)
        {
            Hashtable theHT = new Hashtable();
            DataTable dtSave = CreateTempTable();
            try
            {
                dtSave = GetCheckBoxListValues(cblshorttermeffects, dtSave, "ShortTermEffects");
                dtSave = GetCheckBoxListValues(cbllongtermeffects, dtSave, "LongTermEffects");
                /*********Treatment***************************************/
                dtSave = GetCheckBoxListValues(this.UcPharmacy.chklistEligiblethrough, dtSave, "ARTEligibility");
                dtSave = GetCheckBoxListValues(this.UcPharmacy.chklistARTchangecode, dtSave, "ARTchangecode");
                dtSave = GetCheckBoxListValues(this.UcPharmacy.chklistARTstopcode, dtSave, "ARTstopcode");
                //-------------  Drug Allergy and Toxicities ?---------------
                theHT.Add("OtherShortTermEffects", txtspecityothershortterm.Text);
                // Specify Other long term effects 
                theHT.Add("OtherLongtermEffects", txtspecifyotherlongterm.Text);
                theHT.Add("reasonForSwitchTo2ndLineRegimen", this.UcPharmacy.ddlReasonforswitchto2ndlineregimen.SelectedValue);
                theHT.Add("NumberDrugsSubstituted", this.UcPharmacy.txtNoofdrugssubstituted.Text);
                //Work up plan :
                theHT.Add("WorkUpPlan", txtworkupplan.Text);
                //OI Treatment
                theHT.Add("OIProphylaxis", ddloiprophylaxis.SelectedItem.Value);
                //Cotrimoxazole prescribed for
                theHT.Add("ReasonCTXpresribed", ddlcotrimoxazole.SelectedItem.Value);
                //Fluconazole prescribed for
                theHT.Add("ReasonFluconazolepresribed", ddlfluconazole.SelectedItem.Value);
                //Other (Specify)
                theHT.Add("OtherOIProphylaxis", txtothercotrimoxazole.Text);
                //Other treatment :
                theHT.Add("OtherTreatment", txtothertreatementcatrimozazole.Text);
                theHT.Add("startTime", startTime);
                theHT.Add("userID", Convert.ToInt32(Session["AppUserId"]));
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Session["PatientVisitId"]);
                theHT.Add("locationID", Session["AppLocationId"]);
                theHT.Add("treatmentPlan", UcPharmacy.ddlTreatmentplan.SelectedValue);
                theHT.Add("Noofdrugssubstituted", UcPharmacy.txtNoofdrugssubstituted.Text);
                theHT.Add("reasonforswitchto2ndlineregimen", UcPharmacy.ddlReasonforswitchto2ndlineregimen.SelectedValue);
                theHT.Add("specifyOtherEligibility", UcPharmacy.txtSpecifyOtherEligibility.Text);
                theHT.Add("specifyotherARTchangereason", UcPharmacy.txtSpecifyotherARTchangereason.Text);
                theHT.Add("specifyOtherStopCode", UcPharmacy.txtSpecifyOtherStopCode.Text);
                theHT.Add("visitDate", txtVisitDate.Value);
                KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
                DataSet DsReturns = KNHPEP.SaveUpdatePaediatricIE_ManagementTab(theHT, dtSave);
                SaveCancel("Management");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "saveMgt", "alert('Data on Management tab saved successfully.');", true);
                //BindExistingData();
                tabControl.ActiveTabIndex = 5;
                //commanMSG(DsReturns, 5);
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
        }
        public void BindControl()
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            BindFunctions BindManager = new BindFunctions();
            KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            dsBind = KNHPEP.GetDetailsPaediatric_IE();
            BindManager.BindCombo(ddlHAARTImpression, dsBind.Tables[55], "Name", "Id"); //"HAARTImpression");
            BindManager.BindCheckedList(chkLongTermMedication, dsBind.Tables[56], "Name", "Id");
            BindManager.BindCombo(ddlchilddiagnosis, dsBind.Tables[0], "Name", "Id");
            BindManager.BindCheckedList(cblDiagnosis, dsBind.Tables[54], "NAME", "ID");
            BindManager.BindCombo(ddldisclosurestatus, dsBind.Tables[1], "Name", "Id");
            //BindManager.BindCombo(ddldisclosurestatus, dsBind.Tables[1], "Name", "Id");
            BindManager.BindCombo(ddlcurrentregimenline, dsBind.Tables[3], "Name", "Id");
            BindManager.BindCombo(ddlcurrentartregimen, dsBind.Tables[4], "Name", "Id");
            BindManager.BindCombo(ddlschoolingstatus, dsBind.Tables[5], "Name", "Id");
            BindManager.BindCombo(ddlhighestlevelattained, dsBind.Tables[6], "Name", "Id");
            //BindManager.BindCombo(this.idVitalSign.ddlweightforage, dsBind.Tables[7], "Name", "Id");
            BindManager.BindCombo(ddlschoolperfomance, dsBind.Tables[9], "Name", "Id");
            BindManager.BindCheckedList(cblchroniccondition, dsBind.Tables[10], "Name", "ID");
            BindManager.BindCombo(ddlimmunisationstatus, dsBind.Tables[11], "Name", "Id");
            BindManager.BindCheckedList(cbllabevaluation, dsBind.Tables[25], "Name", "ID");
            //BindManager.BindCombo(ddlInitiationWHOstage, dsBind.Tables[26], "Name", "Id");
            BindManager.BindCheckedList(cblDrugAllergiesToxicitiesPaeds, dsBind.Tables[31], "Name", "ID");
            BindManager.BindCombo(ddloiprophylaxis, dsBind.Tables[34], "Name", "Id");
            BindManager.BindCheckedList(cblshorttermeffects, dsBind.Tables[35], "Name", "ID");
            BindManager.BindCheckedList(cbllongtermeffects, dsBind.Tables[36], "Name", "ID");
            BindManager.BindCombo(ddlcotrimoxazole, dsBind.Tables[41], "Name", "Id");
            BindManager.BindCombo(ddlsingature, dsBind.Tables[52], "name", "EmployeeID");
            BindManager.BindCombo(ddlfluconazole, dsBind.Tables[53], "Name", "Id");
        }
        private DataTable PresentingComplaints(DataTable dtprescompl, string name)
        {

            GridView gdview = (GridView)UcPc.FindControl("gvPresentingComplaints");
            foreach (GridViewRow row in gdview.Rows)
            {
                DataRow dr = dtprescompl.NewRow();
                CheckBox chk = (CheckBox)row.FindControl("ChkPresenting");
                TextBox txt = (TextBox)row.FindControl("txtPresenting");
                Label lbl = (Label)row.FindControl("lblPresenting");
                if (chk.Checked)
                {
                    dr["ID"] = Convert.ToInt32(lbl.Text);
                    dr["FieldName"] = name;
                }
                if (txt.Text != "")
                {
                    dr["NumericField"] = txt.Text;
                }
                dtprescompl.Rows.Add(dr);
            }
            return dtprescompl;
        }
        private DataTable GetCheckBoxListValues(CheckBoxList chklist, DataTable dt, string name)
        {
            DataRow dr;
            for (int i = 0; i < chklist.Items.Count; i++)
            {
                if (chklist.Items[i].Selected)
                {
                    dr = dt.NewRow();
                    dr["ID"] = Convert.ToInt32(chklist.Items[i].Value);
                    dr["FieldName"] = name;
                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }
        private DataTable InsertMultiSelectList(GridView gdview, DataTable dt, string fieldname)
        {
            foreach (GridViewRow row in gdview.Rows)
            {
                DataRow dr = dt.NewRow();

                if (fieldname == "WHOStageIConditions")
                {
                    CheckBox chk = (CheckBox)row.FindControl("Chkwho1");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt1 = (HtmlInputText)row.FindControl("txtCurrentWho1Date");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt2 = (HtmlInputText)row.FindControl("txtCurrentWho1Date1");
                    Label lbl = (Label)row.FindControl("lblwho1");

                    if (chk.Checked)
                    {
                        dr["ID"] = Convert.ToInt32(lbl.Text);
                        dr["FieldName"] = fieldname;
                        if (txtdt1.Value != "")
                        {
                            dr["DateField1"] = txtdt1.Value;
                        }
                        if (txtdt2.Value != "")
                        {
                            dr["DateField2"] = txtdt2.Value;
                        }
                        dt.Rows.Add(dr);
                    }
                }
                if (fieldname == "WHOStageIIConditions")
                {
                    CheckBox chk = (CheckBox)row.FindControl("Chkwho2");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt1 = (HtmlInputText)row.FindControl("txtCurrentWho2Date");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt2 = (HtmlInputText)row.FindControl("txtCurrentWho2Date1");
                    Label lbl = (Label)row.FindControl("lblwho2");

                    if (chk.Checked)
                    {
                        dr["ID"] = Convert.ToInt32(lbl.Text);
                        dr["FieldName"] = fieldname;
                        if (txtdt1.Value != "")
                        {
                            dr["DateField1"] = txtdt1.Value;
                        }
                        if (txtdt2.Value != "")
                        {
                            dr["DateField2"] = txtdt2.Value;
                        }
                        dt.Rows.Add(dr);
                    }
                }
                if (fieldname == "WHOStageIIIConditions")
                {
                    CheckBox chk = (CheckBox)row.FindControl("Chkwho3");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt1 = (HtmlInputText)row.FindControl("txtCurrentWho3Date");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt2 = (HtmlInputText)row.FindControl("txtCurrentWho3Date1");
                    Label lbl = (Label)row.FindControl("lblwho3");

                    if (chk.Checked)
                    {
                        dr["ID"] = Convert.ToInt32(lbl.Text);
                        dr["FieldName"] = fieldname;
                        if (txtdt1.Value != "")
                        {
                            dr["DateField1"] = txtdt1.Value;
                        }
                        if (txtdt2.Value != "")
                        {
                            dr["DateField2"] = txtdt2.Value;
                        }
                        dt.Rows.Add(dr);
                    }
                }
                if (fieldname == "WHOStageIVConditions")
                {
                    CheckBox chk = (CheckBox)row.FindControl("Chkwho4");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt1 = (HtmlInputText)row.FindControl("txtCurrentWho4Date");
                    System.Web.UI.HtmlControls.HtmlInputText txtdt2 = (HtmlInputText)row.FindControl("txtCurrentWho4Date1");
                    Label lbl = (Label)row.FindControl("lblwho4");

                    if (chk.Checked)
                    {
                        dr["ID"] = Convert.ToInt32(lbl.Text);
                        dr["FieldName"] = fieldname;
                        if (txtdt1.Value != "")
                        {
                            dr["DateField1"] = txtdt1.Value;
                        }
                        if (txtdt2.Value != "")
                        {
                            dr["DateField2"] = txtdt2.Value;
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        private DataTable CreateTempTable()
        {
            DataTable dtprescompl = new DataTable();

            DataColumn theID = new DataColumn("ID");
            theID.DataType = System.Type.GetType("System.Int32");
            dtprescompl.Columns.Add(theID);

            DataColumn theFieldName = new DataColumn("FieldName");
            theFieldName.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theFieldName);

            DataColumn theDateValue1 = new DataColumn("DateField1");
            theDateValue1.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theDateValue1);

            DataColumn theDateValue2 = new DataColumn("DateField2");
            theDateValue2.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theDateValue2);

            DataColumn theValue1 = new DataColumn("NumericField");
            theValue1.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theValue1);

            DataColumn theOther = new DataColumn("Other_Notes");
            theOther.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theOther);
            return dtprescompl;
        }
        public void checkIfPreviuosTabSaved()
        {
            /**********check previous values are inserted ************************/
            securityPertab("PaediatricIETriage");
            DataSet ds = KNHStatic.CheckIfPreviuosTabSaved("PaediatricIETriage", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(ds, btnSaveCHistory, btnPrintCHistory, "PaediatricIEClinicalHistory");
        
            ds = KNHStatic.CheckIfPreviuosTabSaved("PaediatricIEClinicalHistory", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(ds, UcTBScreening.btnTBSave, UcTBScreening.btnTBPrint, "PaediatricIETBScreening");

            ds = KNHStatic.CheckIfPreviuosTabSaved("PaediatricIETBScreening", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(ds, btnSaveExam, btnPrintExam, "PaediatricIEExamination");
            
            ds = KNHStatic.CheckIfPreviuosTabSaved("PaediatricIEExamination", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(ds, btnSaveManagement, btnPrintManagement, "PaediatricIEManagement");

            Button btnSave = (Button)UcPWP.FindControl("btnSave");
            Button btnSubmitPositive = (Button)UcPWP.FindControl("btnSubmitPositive");
            Button btnPrintPositive = (Button)UcPWP.FindControl("btnPrintPositive");
            ds = KNHStatic.CheckIfPreviuosTabSaved("PaediatricIEManagement", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(ds, btnSave, btnPrintPositive, "PaediatricIEPwP");
            
            /*******************************************************/
            //For Signature
            DataTable dtSignature = KNHPEP.GetSignature(Convert.ToInt32(ApplicationAccess.KNHPaediatricInitialEvaulation), Convert.ToInt32(Session["PatientVisitId"]));
            if (dtSignature.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSignature.Rows)
                {
                    if (dr["TabName"].ToString() == "PaediatricIETriage")
                        this.UserControlKNHSignature_Triage.lblSignature.Text = dr["UserName"].ToString();
                    if (dr["TabName"].ToString() == "PaediatricIEClinicalHistory")
                        this.UserControlKNHSignature_ClinicalHistory.lblSignature.Text = dr["UserName"].ToString();
                    if (dr["TabName"].ToString() == "PaediatricIEExamination")
                        this.UserControlKNHSignature_Examination.lblSignature.Text = dr["UserName"].ToString();
                    if (dr["TabName"].ToString() == "PaediatricIEManagement")
                        this.UserControlKNHSignature_Management.lblSignature.Text = dr["UserName"].ToString();
                }
            }
        }
        private void securityPertab(String tabName)
        {
            AuthenticationManager Authentication = new AuthenticationManager();

            //triage
            if (tabName == "PaediatricIETriage")
                Authentication.TabUserRights(btnSaveTriage, btnPrintTriage, featureID, triageTabID);
                

            //Clinical History
            else if (tabName == "PaediatricIEClinicalHistory")
                Authentication.TabUserRights(btnSaveCHistory, btnPrintCHistory, featureID, clinicalHistoryTabId);

            //TB Screening
            else if (tabName == "PaediatricIETBScreening")
                Authentication.TabUserRights(UcTBScreening.btnTBSave, UcTBScreening.btnTBPrint, featureID, tbScreeningTabId);

            //Examination
            else if (tabName == "PaediatricIEExamination")
                Authentication.TabUserRights(btnSaveExam, btnPrintExam, featureID, examinationTabId);

            //Management
            else if (tabName == "PaediatricIEManagement")
                Authentication.TabUserRights(btnSaveManagement, btnPrintManagement, featureID, managementTabId);
            //Pwp
            Button btnSave = (Button)UcPWP.FindControl("btnSave");
            Button btnSubmitPositive = (Button)UcPWP.FindControl("btnSubmitPositive");
            Button btnPrintPositive = (Button)UcPWP.FindControl("btnPrintPositive");
            if (tabName == "PaediatricIEPwP")
            {
                Authentication.TabUserRights(btnSave, btnPrintPositive, featureID, pwpTabId);
            }
        }
        private void buttonEnabledAndDisabled(DataSet ds, Button btnSave, Button btnPrint,String tabName)
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
                securityPertab(tabName);
            }
        }
        private void setTabIdAndColor(int tabId,Label lblControlName)
        {
            tabControl.ActiveTabIndex = tabId;
            lblControlName.ForeColor = Color.Red;
        }
        private Boolean fieldValidation_Triage()
        {
            if (this.idVitalSign.txtHeight.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValHeight", "alert('Enter Height - Triage Tab');", true);
                setTabIdAndColor(0, this.idVitalSign.lblHeight);
                return false;
            }
            else
            {
                this.idVitalSign.lblHeight.ForeColor = Color.Black;
            }

            if (this.idVitalSign.txtWeight.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValWeight", "alert('Enter Weight - Triage Tab');", true);
                setTabIdAndColor(0, this.idVitalSign.lblWeight);
                return false;
            }
            else
            {
                this.idVitalSign.lblWeight.ForeColor = Color.Black;
            }

            if (this.idVitalSign.txtBPSystolic.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValSBP", "alert('Enter Systolic Blood pressure - Triage Tab');", true);
                setTabIdAndColor(0, this.idVitalSign.lblBP);
                return false;
            }
            else
            {
                this.idVitalSign.lblBP.ForeColor = Color.Black;
            }
            if (this.idVitalSign.txtBPDiastolic.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValDBP", "alert('Enter Diastolic Blood pressure - Triage Tab');", true);
                setTabIdAndColor(0, this.idVitalSign.lblBP);
                return false;
            }
            else
            {
                this.idVitalSign.lblBP.ForeColor = Color.Black;
            }
            return true;
        }
        private Boolean fieldValidation_Clinical()
        {
            int count = 0;
            for (int i = 0; i < this.cblchroniccondition.Items.Count; i++)
            {
                if (this.cblchroniccondition.Items[i].Selected == true)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCC", "alert('Select Chronic conditions - Clinical History');", true);
                setTabIdAndColor(1, lblHeadingChronic);
                literalMedicalHistory.Text = @"<font size=3 color=Red>Medical History (Disease, Diagnosis and Treatment)</font>";
                return false;
            }
            else
            {                
                lblHeadingChronic.ForeColor = Color.Black;
                literalMedicalHistory.Text = @"<font size=3 Color.FromArgb(0,0,142)>Medical History (Disease, Diagnosis and Treatment)</font>";
            }
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCC", "alert('Select Presenting Complaints - Clinical History');", true);
                literPresenting.Text = @"<font size=3 color=Red>Presenting Complaints</font>";
                return false;
            }
            else
            {
                literPresenting.Text = @"<font size=3 Color.FromArgb(0,0,142)>Presenting Complaints</font>";
            }
            return true;
        }
        private Boolean fieldValidation_Examination()
        {
            int count = 0;
            for (int i = 0; i < chkLongTermMedication.Items.Count; i++)
            {
                if (chkLongTermMedication.Items[i].Selected == true)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValGC", "alert('Select Long Term Medications - Examination Tab');", true);
                literalCurrentLong.Text = @"<font size=3 color=Red>Current Long Term Medications</font>";
                lblLongTermText.ForeColor = Color.Red;
                return false;
            }
            else
            {
                literalCurrentLong.Text = @"<font size=3 Color.FromArgb(0,0,142)>Current Long Term Medications</font>";
                lblLongTermText.ForeColor = Color.FromArgb(0, 0, 153);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValGC", "alert('Select General Condition - Examination Tab');", true);
                literalPhysicalExam.Text = @"<font size=3 color=Red>Physical Examination</font>";
                this.UcPE.lblGeneral.ForeColor = Color.Red;
                return false;
            }
            else
            {
                literalPhysicalExam.Text = @"<font size=3 Color.FromArgb(0,0,142)>Physical Examination</font>";
                this.UcPE.lblGeneral.ForeColor = Color.FromArgb(0, 0, 153);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCC", "alert('Select Cardiovascular Conditions - Examination Tab');", true);
                literalPhysicalExam.Text = @"<font size=3 color=Red>Physical Examination</font>";
                this.UcPE.lblCardiovarscular.ForeColor = Color.Red;
                return false;
            }
            else
            {
                literalPhysicalExam.Text = @"<font size=3 Color.FromArgb(0,0,142)>Physical Examination</font>";
                this.UcPE.lblCardiovarscular.ForeColor = Color.FromArgb(0, 0, 153);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValOC", "alert('Select Oral Cavity Conditions - Examination Tab');", true);
                literalPhysicalExam.Text = @"<font size=3 color=Red>Physical Examination</font>";
                this.UcPE.lblOralCavity.ForeColor = Color.Red;
                return false;
            }
            else
            {
                literalPhysicalExam.Text = @"<font size=3 Color.FromArgb(0,0,142)>Physical Examination</font>";
                this.UcPE.lblOralCavity.ForeColor = Color.FromArgb(0, 0, 153);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValGU", "alert('Select GenitalUrinary Conditions - Examination Tab');", true);
                literalPhysicalExam.Text = @"<font size=3 color=Red>Physical Examination</font>";
                this.UcPE.lblGenitourinary.ForeColor = Color.Red;
                return false;
            }
            else
            {
                literalPhysicalExam.Text = @"<font size=3 Color.FromArgb(0,0,142)>Physical Examination</font>";
                this.UcPE.lblGenitourinary.ForeColor = Color.FromArgb(0, 0, 153);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCNS", "alert('Select CNS Conditions - Examination Tab');", true);
                literalPhysicalExam.Text = @"<font size=3 color=Red>Physical Examination</font>";
                this.UcPE.lblCNS.ForeColor = Color.Red;
                return false;
            }
            else
            {
                literalPhysicalExam.Text = @"<font size=3 Color.FromArgb(0,0,142)>Physical Examination</font>";
                this.UcPE.lblCNS.ForeColor = Color.FromArgb(0, 0, 153);
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

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValChest", "alert('Select ChestLung Conditions - Examination Tab');", true);
                literalPhysicalExam.Text = @"<font size=3 color=Red>Physical Examination</font>";
                this.UcPE.lblChest.ForeColor = Color.Red;
                return false;
            }
            else
            {
                literalPhysicalExam.Text = @"<font size=3 Color.FromArgb(0,0,142)>Physical Examination</font>";
                this.UcPE.lblChest.ForeColor = Color.FromArgb(0, 0, 153);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValSkin", "alert('Select Skin Conditions - Examination Tab');", true);
                literalPhysicalExam.Text = @"<font size=3 color=Red>Physical Examination</font>";
                this.UcPE.lblSkin.ForeColor = Color.Red;
                return false;
            }
            else
            {
                literalPhysicalExam.Text = @"<font size=3 Color.FromArgb(0,0,142)>Physical Examination</font>";
                this.UcPE.lblSkin.ForeColor = Color.FromArgb(0, 0, 153);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValAbdomen", "alert('Select Abdomen Conditions - Examination Tab');", true);
                literalPhysicalExam.Text = @"<font size=3 color=Red>Physical Examination</font>";
                this.UcPE.lblAbdomen.ForeColor = Color.Red;
                return false;
            }
            else
            {
                literalPhysicalExam.Text = @"<font size=3 Color.FromArgb(0,0,142)>Physical Examination</font>";
                this.UcPE.lblAbdomen.ForeColor = Color.FromArgb(0, 0, 153);
            }

            DropDownList ddlwhostage1 = (DropDownList)UcWhostaging.FindControl("ddlwhostage1");
            if (ddlwhostage1.SelectedItem.Text.ToLower() == "select")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValHeight", "alert('Select Current WHO Stage- Examination Tab');", true);
                literalWHOStaging.Text = @"<font size=3 color=Red>WHO Staging</font>";
                UcWhostaging.literalStaging.Text = @"<font size=3 color=Red>Staging</font>";
                setTabIdAndColor(3, this.UcWhostaging.lblWHOStage);
                return false;

            }
            else
            {
                literalWHOStaging.Text = @"<font size=3 Color.FromArgb(0,0,142)>WHO Staging</font>";
                UcWhostaging.literalStaging.Text = @"<font size=3 Color.FromArgb(0,0,142)>Staging</font>";
                UcWhostaging.lblWHOStage.ForeColor = Color.Black;
            }

            DropDownList ddlWABStage1 = (DropDownList)UcWhostaging.FindControl("ddlWABStage");
            if (ddlWABStage1.SelectedItem.Text.ToLower() == "select")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValHeight", "alert('Select WAB Stage - Examination Tab');", true);
                literalWHOStaging.Text = @"<font size=3 color=Red>WHO Staging</font>";
                setTabIdAndColor(3, this.UcWhostaging.lblWABStage);
                return false;

            }
            else
            {
                literalWHOStaging.Text = @"<font size=3 Color.FromArgb(0,0,142)>WHO Staging</font>";
                UcWhostaging.literalStaging.Text = @"<font size=3 Color.FromArgb(0,0,142)>WAB Stage</font>";
                UcWhostaging.lblWABStage.ForeColor = Color.Black;
            }

            return true;
        }
        private Boolean fieldValidation_Management()
        {
            DropDownList ddlTreatmentplan = (DropDownList)UcPharmacy.FindControl("ddlTreatmentplan");
            if (ddlTreatmentplan.SelectedItem.Text.ToLower() == "select")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValHeight", "alert('Select Treatment Plan - Management Tab');", true);
                literalTreatment.Text = @"<font size=3 color=Red>Treatment</font>";
                this.UcPharmacy.lblTreatmentplan.ForeColor = Color.Red;
                return false;
            }
            else
            {
                literalTreatment.Text = @"<font size=3 Color.FromArgb(0,0,142)>Treatment</font>";
                this.UcPharmacy.lblTreatmentplan.ForeColor = Color.FromArgb(0, 0, 153);
            }
            return true;
        }
        public void ErrorLoad()
        {
            string script = string.Empty;
            
            if (rbListFatherAlive.SelectedValue == "0")
            {
                visibleDiv("divdatefatherdeath");
            }
            if (rbListMotherAlive.SelectedValue == "0")
            {
                visibleDiv("divdateofmotherdeath");
            }
            if (rbListRefFacility.SelectedValue == "1")
            {
                visibleDiv("divspecifyfacility");
            }
            

            if (ddlschoolingstatus.SelectedIndex == 1)
            {
                visibleDiv("divhighestlevel");
            }
            if (ddldisclosurestatus.SelectedItem.Text == "Not ready")
            {
                visibleDiv("divreasonnotdisclosed");
            }
            if (ddldisclosurestatus.SelectedItem.Text == "Other specify")
            {
                visibleDiv("divotherdisclosurestatus");
            }
            if (rbListRefFacility.SelectedValue == "1")
                visibleDiv("divspecifyfacility");
            //if (rdoreferedfromfacilityyes.Checked)
            //{
            //    visibleDiv("divspecifyfacility");
            //}
            //16 Ju
            //int i = rdochildonartyes.Checked ? 1 : rdochildonartno.Checked ? 0 : 9;
            //if (1 == 1)
            //{
            //    visibleDiv("divcurrentregimenline");
            //    visibleDiv("divARTRegimen");
            //    visibleDiv("divARTDate");
            //}
            //if (this.rdochildonartyes.Checked)
            //{
            //    visibleDiv("divcurrentregimenline");
            //}
            
            //if (this.rdoclientsupportgroupyes.Checked)
            //{
            //    visibleDiv("divsupportgroup");
            //}

            if (this.rbListPreviouslyAdmitt.SelectedValue == "1")
            {
                visibleDiv("divdiagnosis");
                visibleDiv("divadmissionstart");
                visibleDiv("divadmissionend");
            }
            if (this.rbListTBHistory.SelectedValue == "1")
            {
                visibleDiv("divcompletetxdate");
                visibleDiv("divretreatmentdate");
            }
            //if (this.rdotvhistoryyes.Checked)
            //{
            //    visibleDiv("divcompletetxdate");
            //    visibleDiv("divretreatmentdate");
            //}
            if (this.rbListMilestone.SelectedValue == "0")
            {
                visibleDiv("divmilestoneshowhide");
            }
            if (this.rdolabevaluationyes.Checked)
            {
                visibleDiv("divlabevaluatinshowhidey");
            }
            //16 Ju
            //if (this.radbtnMernarcheyes.Checked)
            //{
            //    visibleDiv("divmenarchedatehshowhidey");
            //}
            if (ddloiprophylaxis.SelectedItem.Text == "Cotrimoxazole")
            {
                visibleDiv("divoiprophylaxisshowhidey");
            }
            if (ddloiprophylaxis.SelectedItem.Text == "Other")
            {
                visibleDiv("divoiprophylasixothershowhidey");
            }
        }
        //Save - Triage Tab 
        protected void btnSaveTriage_Click(object sender, EventArgs e)
        {
            //First check the required values if valid the call the save function
            if (fieldValidation_Triage() == false)
            {
                literalVitalSign.Text = @"<font size=3 color=red>Vital Signs</font>";
                ErrorLoad();
                return;
            }
            literalVitalSign.Text = @"<font size=3 Color.FromArgb(0,0,142)>Vital Signs</font>";
            triage_Save(0);
        }
        //Clinical History 
        //Save click - History Tab
        protected void btnSaveCHistory_Click(Object o, EventArgs e)
        {
            if (fieldValidation_Clinical() == false)
            {
                ErrorLoad();
                return;
            }
            clinicalHistory_Save(0);
        }
        //Save- Examination Tab
        protected void btnSaveExam_Click(Object o, EventArgs e)
        {
            if (fieldValidation_Examination() == false)
            {
                ErrorLoad();
                return;
            }
            examination_Save(0);
        }
        //Save - Management Tab
        protected void btnSaveManagement_Click(Object o, EventArgs e)
        {
            if (fieldValidation_Management() == false)
            {
                ErrorLoad();
                return;
            }
            management_Save(1);
        }


        private void UserRights(Button save, Button print, int FeatureID, int TabID)
        {
            AuthenticationManager Authentication = new AuthenticationManager();
            bool bCanView = !Authentication.HasFunctionRight(FeatureID, TabID, FunctionAccess.View, (DataTable)Session["UserRight"]);

            //if user have view permission
            save.Enabled = bCanView;
            //first time  - new user form creation
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                bool bCanAdd = Authentication.HasFunctionRight(FeatureID, TabID, FunctionAccess.Add, (DataTable)Session["UserRight"]);
                save.Enabled = bCanAdd;
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                bool bCanUpdate = Authentication.HasFunctionRight(FeatureID, TabID, FunctionAccess.Update, (DataTable)Session["UserRight"]);
                if (Convert.ToInt32(Session["AppUserID"]) == 1)
                {
                    bCanUpdate = true;
                }
                save.Enabled = bCanUpdate;
            }

            print.Enabled = Authentication.HasFunctionRight(FeatureID, TabID, FunctionAccess.Print, (DataTable)Session["UserRight"]);
        }


        protected void btnCloseTriage_Click(Object o, EventArgs e)
        {
            if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                Response.Redirect("frmPatient_History.aspx?&sts=0");
            }
            else
            {
                Response.Redirect("../frmFindAddPatient.aspx");
            }
        }
        protected void btnCloseManagement_Click(Object o, EventArgs e)
        {
            if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                Response.Redirect("frmPatient_History.aspx?&sts=0");
            }
            else
            {
                Response.Redirect("../frmFindAddPatient.aspx");
            }
        }
        protected void btnCloseExam_Click(Object o, EventArgs e)
        {
            if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                Response.Redirect("frmPatient_History.aspx?&sts=0");
            }
            else
            {
                Response.Redirect("../frmFindAddPatient.aspx");
            }
        }
        protected void btnCloseHistory_Click(Object o, EventArgs e)
        {
            if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                Response.Redirect("frmPatient_History.aspx?&sts=0");
            }
            else
            {
                Response.Redirect("../frmFindAddPatient.aspx");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Delete")
            {
                int delete = theUtils.DeleteForm("Paediatric Initial Evaluation", Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppUserId"]));

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