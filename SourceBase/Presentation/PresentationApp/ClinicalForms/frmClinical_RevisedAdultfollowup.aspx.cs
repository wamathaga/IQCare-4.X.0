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
using System.Linq;
using System.Drawing;
using PresentationApp.ClinicalForms.UserControl;

namespace PresentationApp.ClinicalForms
{
    
    public partial class frmClinical_RevisedAdultfollowup : LogPage
    {
        IKNHRevisedAdult KNHREVFWUP;
        IKNHStaticForms KNHStatic;
        DataTable dtmuiltselect;
        static string startTime;
        // Delegate declaration
        public delegate void OnButtonClick();
        // Event declaration
        public event OnButtonClick btnHandler;
        IQCareUtils theUtils = new IQCareUtils();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx",true);
            }
            KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            
            if (IsPostBack != true)
            {
                BindDropdown();
                BindChkboxlst();
                LoadAutoPopulatingData();
                startTime = String.Format("{0:dd-MMM-yyyy hh:mm:ss}", DateTime.Now);
                //startTime = String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now);
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    usePEFUForm();
                    //LoadAutoPopulatingData();

                }
                
                //hdnCurrentTabId.Value = tabControl.ActiveTab.ID;
                //hdnPrevTabId.Value = tabControl.ActiveTab.ID;
                //hdnCurrenTabName.Value = tabControl.ActiveTab.HeaderText;
                //hdnPrevTabName.Value = tabControl.ActiveTab.HeaderText;
                //ViewState["ActiveTabIndex"] = tabControl.ActiveTabIndex;
                //hdnPrevTabIndex.Value = Convert.ToString(tabControl.ActiveTabIndex);
                //hdnCurrenTabIndex.Value = Convert.ToString(tabControl.ActiveTabIndex);
                                
                ShowHideBusinessRules();
                
                GblIQCare.FormId = 175;
                IIQCareSystem AdminManager;
                AdminManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                //startTime = AdminManager.SystemDate();
                //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
                //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Adult Follow Up";
                //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Adult Follow Up";

            }
            UcTBScreen.btnHandler += new UserControlKNH_TBScreening.OnButtonClick(tabControl_Handler);

            addAttributes();

            if (Request.QueryString["name"] == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showDeletebutton", "ShowHide('tblDeleteButton','show');", true);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "hideSavebutton", "ShowHide('tblSaveButton','hide');", true);
            }
        }

        private void addAttributes()
        {
            txtvisitDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
            foreach (ListItem li in cblSurgicalConditions.Items)
            {
                li.Attributes.Add("onclick", "ToggleSurgicalItems('" + cblSurgicalConditions.ClientID + "','" + li.Text + "');");
            }
        }
        void tabControl_Handler()
        {
            checkIfPreviuosTabSaved();
            tabControl.ActiveTabIndex = 3;
        }

        private void checkMedicalCondition()
        {
            if (cblSpecificMedicalCondition.SelectedValue != "0" && cblSpecificMedicalCondition.SelectedValue != "1")
            {

            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {                    
                    BindExistingData();
                    //ErrorLoad();
                }
                else
                    txtvisitDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            //john
            ErrorLoad();
            int feature = 0;
            if (Convert.ToString(Session["TechnicalAreaId"]) == "210")
                feature = 276;
            else
                feature = 175;
            IPatientKNHPEP KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            DataTable dtSignature = KNHPEP.GetSignature(feature, Convert.ToInt32(Session["PatientVisitId"]));
            foreach (DataRow dr in dtSignature.Rows)
            {
                if (dr["TabName"].ToString() == "AdultFUTriage")
                    this.UserControlKNH_SignatureTriage.lblSignature.Text = dr["UserName"].ToString();
                if (dr["TabName"].ToString() == "AdultFUClinicalHistory")
                    this.UserControlKNH_SignatureClinical.lblSignature.Text = dr["UserName"].ToString();
                if (dr["TabName"].ToString() == "AdultFUExamination")
                    this.UserControlKNH_SignatureExam.lblSignature.Text = dr["UserName"].ToString();
                if (dr["TabName"].ToString() == "AdultFUManagement")
                    this.UserControlKNH_SignatureMgt.lblSignature.Text = dr["UserName"].ToString();
            }
            //
            checkIfPreviuosTabSaved();
        }

        private void LoadAutoPopulatingData()
        {
            string script = string.Empty;
            DataSet dsAutopopulate = new DataSet();
            dsAutopopulate = KNHStatic.GetAdultFollowUpFormAutoPopulatingData(Convert.ToInt32(Session["PatientId"]));

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                if (dsAutopopulate.Tables[0].Rows.Count > 0)
                {
                    this.idVitalSign.txtHeight.Text = dsAutopopulate.Tables[0].Rows[0]["Height"].ToString();
                }
            }

            //autopopulate existing medical conditions
            //int val=9;
            //if (dsAutopopulate.Tables[3].Rows.Count > 0)
            //{
            //    for (int i = 0; i < dsAutopopulate.Tables[3].Rows.Count; i++)
            //    {
            //        if (dsAutopopulate.Tables[3].Rows[i]["MedicalCondition"].ToString() == "True")
            //            val = 1;
            //        else if (dsAutopopulate.Tables[3].Rows[i]["MedicalCondition"].ToString() == "False")
            //            val = 0;
            //    }
            //}
            //if (dsAutopopulate.Tables[3].Rows[0]["MedicalCondition"] != System.DBNull.Value)
            //{
                //if (val == 1)
                //{
                //    this.rdoMedicalCondition.SelectedValue = "1";
                //    script = "";
                //    script = "<script language = 'javascript' defer ='defer' id = 'divMedicalConditionshowhideyes'>\n";
                //    script += "ShowHide('divcblSpecificMedicalCondition','show');\n";
                //    script += "</script>\n";
                //    RegisterStartupScript("divMedicalConditionshowhideyes", script);

                //}
                //else if (val == 0)
                //{
                //    this.rdoMedicalCondition.SelectedValue = "0";
                //}
            //}
            if(dsAutopopulate.Tables[1].Rows.Count > 0)
                FillCheckboxlist(cblSpecificMedicalCondition, dsAutopopulate.Tables[1], "SpecificMedicalCondition");
          
        }

        private void BindDropdown()
        {
            UCWHO.gvWHO1.Columns[2].Visible = false;
            UCWHO.gvWHO2.Columns[2].Visible = false;
            UCWHO.gvWHO3.Columns[2].Visible = false;
            UCWHO.gvWHO4.Columns[2].Visible = false;
            txtUpdated_phone.Attributes.Add("OnKeyup", "chkPostiveInteger('" + txtUpdated_phone.ClientID + "')");
            //BindDropdown(ddlCaCervixscreening, "CervicalCancerScreeningResults");
            BindDropdown(ddlcaregiverrelationship, "TreatmentSupporterRelationship");

            BindDropdown(ddlDisclosureStatus, "DisclosureStatus");
            ddlDisclosureStatus.Attributes.Add("OnChange", "getSelectedValue('divReasonNotDisclosed','" + ddlDisclosureStatus.ClientID + "','Not ready');getSelectedValue('divOtherDisclosureReason','" + ddlDisclosureStatus.ClientID + "','Other specify')");
            BindDropdown(ddlSchoolingStatus, "SchoolingStatus");
            ddlSchoolingStatus.Attributes.Add("OnChange", "getSelectedValue('divHighestLevelAttained','" + ddlSchoolingStatus.ClientID + "','Enrolled')");
            BindDropdown(ddlHighestLevelAttained, "HighestLevelAttained");

            //BindDropdown(ddlcxrresults, "CXR");
            //BindDropdown(ddlDiagnosisandcurrentillness, "Diagnosisandcurrentillness");
            //BindDropdown(ddlDrugallergytoxicities, 3039);
            //BindDropdown(ddlHIVassociatedconditions, 5029);
            BindDropdown(ddlfluconazole, "FluconazolePrescribedFor");
            BindDropdown(ddlOIProphylaxis, "OIProphylaxis");
            ddlOIProphylaxis.Attributes.Add("OnChange", "getSelectedValue('divCotrimoxazoleprescribedfor','" + ddlOIProphylaxis.ClientID + "','Cotrimoxazole');getSelectedValue('divFluconazoleshowhide','" + ddlOIProphylaxis.ClientID + "','Fluconazole');getSelectedValue('divOtherOIPropholyxis','" + ddlOIProphylaxis.ClientID + "','Other');");
            
            BindDropdown(ddlHIVAssociatedConditionsPeads, "HIVAssociatedConditionsPeads");
            //BindDropdown(ddlReasonforswitchto2ndlineregimen, "2ndLineRegimenSwitch");
            //BindDropdown(ddlReasonLMPnotaccessed, 3046);
            //BindDropdown(ddlRegimenPrescribed, "RegimenPrescribed");
            //ddlRegimenPrescribed.Attributes.Add("OnChange", "getSelectedValue('divOtherregimen','" + ddlRegimenPrescribed.ClientID + "','Other')");
            //BindDropdown(ddlFPmethod, "FPmethod");
            //BindDropdown(ddlSputumsmear, "Sputumsmear");
            //BindDropdown(ddltbfindings, "tbfindings");
            //BindDropdown(ddltbplan, "TBPlan");
            //ddltbplan.Attributes.Add("OnChange", "getSelectedValue('divSpecifyOtherTBplan','" + ddltbplan.ClientID + "','Other (Specify)')");
            //BindDropdown(ddltbregimen, "tbregimen");
            //ddltbregimen.Attributes.Add("OnChange", "getSelectedValue('divOtherTBregimen','" + ddltbregimen.ClientID + "','Other')");
            //BindDropdown(ddlteatmentoutcome, "TBTreatmentOutcomesPeads");
            //BindDropdown(ddltbtype, "TBTypePeads");
            //BindDropdown(ddlTreatmentplan, "ARTTreatmentPlan");
            //ddlTreatmentplan.Attributes.Add("OnChange", "getSelectedtableValue('divEligiblethrough','" + ddlTreatmentplan.ClientID + "','Start ART','DIVTreatmentplan');getSelectedtableValue('divARTchangecode','" + ddlTreatmentplan.ClientID + "','Substitute regimen','DIVTreatmentplan');getSelectedtableValue('divReasonforswitchto2ndlineregimen','" + ddlTreatmentplan.ClientID + "','Switch to second lin','DIVTreatmentplan');getSelectedtableValue('divARTstopcode','" + ddlTreatmentplan.ClientID + "','Stop treatment','DIVTreatmentplan')");
            BindDropdown(ddlCotrimoxazoleprescribed, "ReasonCTXpresribed");
            //BindDropdown(ddlSexualOrientation, "SexualOrientation");
            //BindDropdown(ddlPartnerHIVStatus, "PartnerHIVStatus");
            //BindDropdown(ddlGivenPWPMessages, "GivenPWPMessages");
            //BindDropdown(ddlOfferedHPVaccine, "OfferedHPVaccine");
            BindDropdown(ddlSchoolPerfomance, "SchoolPerfomance");
            //BindDropdown(ddlCervicalCancerScreeningResults, "CervicalCancerScreeningResults");
            //BindDropdown(ddlDiagnosisandcurrentillness, "Diagnosis");
            //ddlDiagnosisandcurrentillness.Attributes.Add("OnChange", "getSelectedValue('divIndicateHIVrelatedillness','" + ddlDiagnosisandcurrentillness.ClientID + "','HIV-Related illness');getSelectedValue('divIndicateNonHIVrelatedillness','" + ddlDiagnosisandcurrentillness.ClientID + "','Non-HIV related illness')");
            //DropDownList ddlDrugAllergyToxicity = (DropDownList)UCAllergies.FindControl("ddlDrugAllergyToxicity");
            //BindDropdown(ddlDrugAllergyToxicity, "DrugAllergyToxicity");
            //BindDropdown(ddlLMPNotaccessedReason, "LMPNotaccessedReason");
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
            ddSignature.DataSource = null;
            ddSignature.Items.Clear();


            if (theDS.Tables["Mst_Decode"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Decode"]);
                if (theCodeDT.Rows.Count > 0)
                {
                    theDV.RowFilter = "DeleteFlag=0 and CodeId=" + theCodeDT.Rows[0]["CodeId"];
                    if (theDV.Table != null)
                    {
                        DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(DropDownID, theDT, "Name", "Id");
                    }
                }

            }

            bool isIncomplete = this.Controls.OfType<TextBox>().Any(tb => string.IsNullOrEmpty(tb.Text));
            if (isIncomplete)
            {
                // do your work
            }
            var emptyTextboxes = from tb in this.Controls.OfType<DropDownList>()
                                 where tb.SelectedIndex < 0
                                 select tb;
            //bool isIncomplete = this.Controls.OfType<DropDownList>().Any(tb => tb.SelectedIndex<0);
            if (emptyTextboxes.Any())
            {
                // do your work
            }
        }
        private void CreateMultiSelectwithDate(int reqflg, System.Web.UI.WebControls.Panel DIVCustomItem, string fieldlabel, string fieldname)
        {
            DIVCustomItem.CssClass = "border center formbg";
            DIVCustomItem.Controls.Add(new LiteralControl("</br>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<table cellspacing='6' cellpadding='0' width='100%' border='0'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<div class='customdivborder leftallign' runat='server' nowrap='nowrap'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<table width=100%>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%'>"));

            if (reqflg == 1)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl-" + fieldlabel + "'>" + fieldlabel + " </label>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl-" + fieldlabel + "'>" + fieldlabel + " </label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));

            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td colspan='4' class='border'>"));

            //WithPanel
            System.Web.UI.WebControls.Panel PnlMulti = new System.Web.UI.WebControls.Panel();
            PnlMulti.ID = "Pnl_-" + fieldname;
            PnlMulti.ToolTip = fieldlabel;
            PnlMulti.Controls.Add(new LiteralControl("<div class='customdivborder1 leftallign' runat='server' nowrap='nowrap'>"));
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);

            DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + theCodeDT.Rows[0]["CodeId"];



            BindFunctions BindManager = new BindFunctions();
            DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            if (theDT != null)
            {
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    // Dates Control creation for multi Select list
                    //Date 1 Control
                    TextBox theDate1 = new TextBox();
                    theDate1.ID = "TXTDT1-" + theDT.Rows[i][0] + "-" + fieldname;
                    Control ctl = (TextBox)theDate1;
                    theDate1.Width = 83;
                    theDate1.MaxLength = 11;
                    theDate1.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");


                    //string thDTVar = "ctl00_IQCareContentPlaceHolder_" + DIVCustomItem.ID + "_" + theDate1.ClientID;
                    string thDTVar = theDate1.ClientID;
                    theDate1.Attributes.Add("onblur", "DateFormat(this, this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + thDTVar + "', '" + thDTVar + "')");

                    System.Web.UI.WebControls.Image theDateImage1 = new System.Web.UI.WebControls.Image();
                    theDateImage1.ID = "img" + theDate1.ID;
                    theDateImage1.Height = 22;
                    theDateImage1.Width = 22;
                    //theDateImage1.Visible = theEnable;
                    theDateImage1.ToolTip = "Date Helper";
                    theDateImage1.ImageUrl = "~/images/cal_icon.gif";
                    theDateImage1.Attributes.Add("onClick", "w_displayDatePicker('" + ((TextBox)ctl).ClientID + "');");
                    theDate1.Visible = false;
                    theDateImage1.Visible = false;

                    CheckBox chkbox = new CheckBox();
                    chkbox.ID = Convert.ToString("CHKMULTI-" + theDT.Rows[i][0] + "-" + fieldname);
                    chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);

                    PnlMulti.Controls.Add(chkbox);
                    PnlMulti.Controls.Add(theDate1);
                    PnlMulti.Controls.Add(new LiteralControl("&nbsp;"));
                    PnlMulti.Controls.Add(theDateImage1);
                    PnlMulti.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
                    chkbox.Width = 210;
                    PnlMulti.Controls.Add(new LiteralControl("<br/>"));
                    theDate1.Visible = true;
                    theDateImage1.Visible = true;
                }
            }
            PnlMulti.Controls.Add(new LiteralControl("</div>"));

            DIVCustomItem.Controls.Add(PnlMulti);
            //ApplyBusinessRules(PnlMulti, ControlID, theEnable);
            //PnlMulti.Enabled = theEnable;
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</div>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }
        public void BindChkboxlst()
        {
            //BindChkboxlstControl(chklistARTstopcode, "ARTstopcode");
            //BindChkboxlstControl(chklistcounselling, "counselling");
            //chklistcounselling.Attributes.Add("OnClick", "CheckBoxHideUnhide('" + chklistcounselling.ClientID + "','divOthercounselling','Other')");
            //BindChkboxlstControl(cblTBScreening, "TBAssessmentICF");
            BindChkboxlstControl(cblShorttermeffects, "ShortTermEffects");
            cblShorttermeffects.Attributes.Add("OnClick", "CheckBoxHideUnhide('" + cblShorttermeffects.ClientID + "','divSpecifyothershorttermeffects','Other Specify')");
            BindChkboxlstControl(chklistlongtermeffect, "LongTermEffects");
            chklistlongtermeffect.Attributes.Add("OnClick", "CheckBoxHideUnhide('" + chklistlongtermeffect.ClientID + "','divSpecifyOtherlongtermeffects','Other specify')");
            //BindChkboxlstControl(chklistlabevaluation, "SpecifyLabEvaluation");
            //BindChkboxlstControl(chklistEligiblethrough, "ARTEligibility");
            //BindChkboxlstControl(chklistARTchangecode, "ARTchangecode");
            //chklistARTchangecode.Attributes.Add("OnClick", "CheckBoxHideUnhide('" + chklistARTchangecode.ClientID + "','divSpecifyotherARTchangereason','Other')");
            BindChkboxlstControl(cblDiagnosis2, "Diagnosis");
            cblDiagnosis2.Attributes.Add("OnClick", "CheckBoxToggleShowHide('" + cblDiagnosis2.ClientID + "','divHIVrelated','HIV-Related illness');CheckBoxHideUnhide('" + cblDiagnosis2.ClientID + "','divNonHIVrelated','Non-HIV related illness')");

            BindChkboxlstControl(cblSurgicalConditions, "SurgicalConditions");
            BindChkboxlstControl(cblSpecificMedicalCondition, "SpecificMedicalCondition");
            cblSurgicalConditions.Attributes.Add("OnClick", "CheckBoxToggleShowHide('" + cblSurgicalConditions.ClientID + "','divCurrentSurgicalConditionYN','Current');CheckBoxToggleShowHide('" + cblSurgicalConditions.ClientID + "','divPreviousSurgicalCondition','Previous');");
            cblSpecificMedicalCondition.Attributes.Add("OnClick", "CheckBoxToggleShowHide('" + cblSpecificMedicalCondition.ClientID + "','divothermedconditon','Other medical condition')");
            //BindChkboxlstControl(cblHighRisk, "HighRisk");
            //BindChkboxlstControl(cblRefferedToFUpF, "RefferedToFUpF");
            //cblRefferedToFUpF.Attributes.Add("OnClick", "CheckBoxHideUnhide('" + cblRefferedToFUpF.ClientID + "','divSpecifyOtherReferredTo','Other ( Specify )')");

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
        public void ShowHideBusinessRules()
        {

            string script = string.Empty;

            //script = "";
            //script = "<script language = 'javascript' defer ='defer' id = 'divshowvitalsigny'>\n";
            //script += "ShowHide('divshowvitalsign','hide');ShowHide('divprogressioninwhoshowhide','show'); ;ShowHide('trPatFUstatus','show')\n";
            //script += "</script>\n";
            //RegisterStartupScript("divshowvitalsigny", script);

            if (Convert.ToDecimal(Session["PatientAge"]) >= 15 && Convert.ToDecimal(Session["PatientAge"]) < 19)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divdisclosurestatusy'>\n";
                script += "ShowHide('divdisclosurestatus','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divdisclosurestatusy", script);
            }
            if (Convert.ToDecimal(Session["PatientAge"]) >= 15 && Convert.ToDecimal(Session["PatientAge"]) < 19)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divschoolingstatusy'>\n";
                script += "ShowHide('divschoolingstatus','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divschoolingstatusy", script);
            }
            if (Convert.ToDecimal(Session["PatientAge"]) >= 15 && Convert.ToDecimal(Session["PatientAge"]) < 19)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'divHighestLevelAttainedy'>\n";
                script += "ShowHide('divHighestLevelAttained','show');\n";
                script += "</script>\n";
                RegisterStartupScript("divHighestLevelAttainedy", script);
            }

            //if (Convert.ToDecimal(Session["PatientAge"]) >= 10)
            //{
            //    script = "";
            //    script = "<script language = 'javascript' defer ='defer' id = 'divtannerstagingy'>\n";
            //    script += "ShowHide('divtannerstaging','show');\n";
            //    script += "</script>\n";
            //    RegisterStartupScript("divtannerstagingy", script);
            //}
            //if (Convert.ToDecimal(Session["PatientAge"]) >= 8 && Session["PatientSex"].ToString() == "Female")
            //{
            //    script = "";
            //    script = "<script language = 'javascript' defer ='defer' id = 'divmernarcheshowy'>\n";
            //    script += "ShowHide('divmernarcheshow','show');\n";
            //    script += "</script>\n";
            //    RegisterStartupScript("divmernarcheshowy", script);
            //}
            //if (Session["PatientSex"].ToString() == "Female")
            //{
            //    script = "";
            //    script = "<script language = 'javascript' defer ='defer' id = 'divlmpassessedy'>\n";
            //    script += "ShowHide('divlmpassessed','show');\n";
            //    script += "</script>\n";
            //    RegisterStartupScript("divlmpassessedy", script);
            //}
            //if (Session["PatientSex"].ToString() == "Female")
            //{
            //    script = "";
            //    script = "<script language = 'javascript' defer ='defer' id = 'divshowhideeddy'>\n";
            //    script += "ShowHide('divshowhideedd','show');ShowHide('divscreenedcc','show');\n";
            //    script += "</script>\n";
            //    RegisterStartupScript("divshowhideeddy", script);
            //}


        }
        public void FillCheckboxlist(CheckBoxList chk, DataTable thedt, string name)
        {
            IQCareUtils theUtils = new IQCareUtils();
            DataView theDV = new DataView(thedt);
            theDV.RowFilter = "FieldName='" + name + "'";
            DataTable dt = (DataTable)theUtils.CreateTableFromDataView(theDV);
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
                            if (name == "ChronicCondition")
                            {
                                if (chk.Items[j].Text == "Other specify")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divothercondition'>\n";
                                    script += "ShowHide('divothercondition','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divothercondition", script);
                                }
                            }
                            else if (name == "TBSideEffects")
                            {
                                if (chk.Items[j].Text == "Other Side effects (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divothertbshowhideyes'>\n";
                                    script += "ShowHide('divothertbshowhide','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divothertbshowhideyes", script);
                                }
                            }
                            else if (name == "DrugAllergiesToxicitiesPaeds")
                            {
                                if (chk.Items[j].Text == "ARV")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divspecifyarvallergyshowhideA'>\n";
                                    script += "ShowHide('divspecifyarvallergyshowhide','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divspecifyarvallergyshowhideA", script);
                                }
                                if (chk.Items[j].Text == "Antibiotic")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divspecifyantibioticshowhideA'>\n";
                                    script += "ShowHide('divspecifyantibioticshowhide','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divspecifyantibioticshowhideA", script);
                                }
                                if (chk.Items[j].Text == "Other")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divspecifyotherdrugshowhideA'>\n";
                                    script += "ShowHide('divspecifyotherdrugshowhide','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divspecifyotherdrugshowhideA", script);
                                }
                            }
                            else if (name == "LongTermEffects")
                            {
                                if (chk.Items[j].Text == "Other specify")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divspecityotherlogntermeffectsy'>\n";
                                    script += "ShowHide('divspecityotherlogntermeffects','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divspecityotherlogntermeffectsy", script);
                                }
                            }
                            else if (name == "ShortTermEffects")
                            {
                                if (chk.Items[j].Text == "Other Specify")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divothershorttermeffectsshowhidey'>\n";
                                    script += "ShowHide('divothershorttermeffectsshowhide','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divothershorttermeffectsshowhidey", script);
                                }
                            }
                            else if (name == "Counselling")
                            {
                                if (chk.Items[j].Text == "Other")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divothercounsellingshowhidey'>\n";
                                    script += "ShowHide('divothercounsellingshowhide','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divothercounsellingshowhidey", script);
                                }
                            }
                            else if (name == "SpecificMedicalCondition")
                            {
                                if (chk.Items[j].Text == "Other medical conditions")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divothermedconditonshowhidey'>\n";
                                    script += "ShowHide('divothermedconditon','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divothermedconditonshowhidey", script);
                                }
                            }
                            else if (name == "SurgicalConditions")
                            {
                                if (chk.Items[j].Text == "Previous")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divPreviousSurgicalConditionshowhidey'>\n";
                                    script += "ShowHide('divPreviousSurgicalCondition','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divPreviousSurgicalConditionshowhidey", script);
                                }
                                if (chk.Items[j].Text == "Current")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divCurrentSurgicalConditionYNshowhidey'>\n";
                                    script += "ShowHide('divCurrentSurgicalConditionYN','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divCurrentSurgicalConditionYNshowhidey", script);
                                }
                            }
                            else if (name == "GeneralConditions")
                            {
                                if (chk.Items[j].Text == "Other")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divGeneralConditionsshowhidey'>\n";
                                    script += "ShowHide('divgeneralothercondition','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divGeneralConditionsshowhidey", script);
                                }
                            }
                            else if (name == "CardiovascularConditions")
                            {
                                if (chk.Items[j].Text == "Other")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherCardiovascularConditionsshowhidey'>\n";
                                    script += "ShowHide('divOtherCardiovascularConditions','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherCardiovascularConditionsshowhidey", script);
                                }
                            }
                            else if (name == "OralCavityConditions")
                            {
                                if (chk.Items[j].Text == "Other")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOralCavityConditionsshowhidey'>\n";
                                    script += "ShowHide('divOtherOralCavityConditions','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOralCavityConditionsshowhidey", script);
                                }
                            }
                            else if (name == "GenitalUrinaryConditions")
                            {
                                if (chk.Items[j].Text == "Other")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherGenitourinaryConditionsshowhidey'>\n";
                                    script += "ShowHide('divOtherGenitourinaryConditions','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherGenitourinaryConditionsshowhidey", script);
                                }
                            }
                            else if (name == "CNSConditions")
                            {
                                if (chk.Items[j].Text == "Other")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherCNSConditionsshowhidey'>\n";
                                    script += "ShowHide('divOtherCNSConditions','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherCNSConditionsshowhidey", script);
                                }
                            }
                            else if (name == "ChestLungsConditions")
                            {
                                if (chk.Items[j].Text == "Other")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherChestLungsConditionsshowhidey'>\n";
                                    script += "ShowHide('divOtherChestLungsConditions','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherChestLungsConditionsshowhidey", script);
                                }
                            }
                            else if (name == "SkinConditions")
                            {
                                if (chk.Items[j].Text == "Other")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherSkinConditionsshowhidey'>\n";
                                    script += "ShowHide('divOtherSkinConditions','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherSkinConditionsshowhidey", script);
                                }
                            }
                            else if (name == "AbdomenConditions")
                            {
                                if (chk.Items[j].Text == "Other")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherAbdomenConditionsshowhidey'>\n";
                                    script += "ShowHide('divOtherAbdomenConditions','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherAbdomenConditionsshowhidey", script);
                                }
                            }
                            else if (name == "RefferedToFUpF")
                            {
                                if (chk.Items[j].Text.ToLower() == "other specialist clinic")
                                {
                                    visibleDiv("TriagedivReferToSpecialistClinic");
                                }
                                if (chk.Items[j].Text.ToLower() == "other (specify)")
                                {
                                    visibleDiv("TriagedivReferToOther");
                                }

                            }
                        }
                    }
                }

            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            Save(0);
            //Hashtable htsave = HT(0);

            //if (rdoAdherenceYesNo.SelectedValue != "")
            //{

            //}
            //else
            //{

            //}
        }
        public void Save(int dqchk)
        {
            int tabindex = 0;
            dtmuiltselect = CreateTempTable();
            Hashtable theHT = new Hashtable();
            string savetabname = tabControl.ActiveTab.HeaderText.ToString();
            DataSet DsReturns = new DataSet();
            IKNHRevisedAdult KNHADULTFWUP = (IKNHRevisedAdult)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHRevisedAdult, BusinessProcess.Clinical");
            if (fieldValidation(savetabname) == false)
            {
                ErrorLoad();
                return;
            }
            //Hashtable theHT = HT(dqchk);
            string tabname = string.Empty;
            if (savetabname == "Triage" || tabControl.ActiveTabIndex == 0)
            {
                tabname = "Triage";
                CheckBoxList cblReferredTo = (CheckBoxList)this.idVitalSign.FindControl("cblReferredTo");                
                DataTable dtRefferedToFUpF = GetCheckBoxListValues(cblReferredTo, "RefferedToFUpF");
                dtmuiltselect.Merge(dtRefferedToFUpF);
                theHT = HT(dqchk, tabname);
                DsReturns = KNHADULTFWUP.SaveUpdateKNHRevisedFollowupData_TriageTab(theHT, dtmuiltselect, dqchk, 0, Convert.ToInt32(Session["AppUserId"]), Convert.ToString(Session["TechnicalAreaId"]));
                
                tabindex = 1;
            }
            else if (savetabname == "Clinical History" || tabControl.ActiveTabIndex == 1)
            {
                tabname = "Clinical History";
                GridView gdview = (GridView)UCPresComp.FindControl("gvPresentingComplaints");
                DataTable dtprescompl = CreateTempTable();
                foreach (GridViewRow row in gdview.Rows)
                {
                    DataRow dr = dtprescompl.NewRow();
                    CheckBox chk = (CheckBox)row.FindControl("ChkPresenting");
                    TextBox txt = (TextBox)row.FindControl("txtPresenting");
                    Label lbl = (Label)row.FindControl("lblPresenting");
                    if (chk.Checked)
                    {
                        dr["ID"] = Convert.ToInt32(lbl.Text);
                        if (txt.Text != "")
                        {
                            dr["OtherNotes"] = txt.Text;
                        }

                        dr["FieldName"] = "PresentingComplaints";
                        dtprescompl.Rows.Add(dr);


                    }


                }
                dtmuiltselect.Merge(dtprescompl);
                //CheckBoxList cblSpecificMedicalCondition = (CheckBoxList)this.UC2.FindControl("cblSpecificMedicalCondition");  
                DataTable dtSpecificMedicalCondition = GetCheckBoxListValues(cblSpecificMedicalCondition, "SpecificMedicalCondition");
                //CheckBoxList cblSurgicalConditions = (CheckBoxList)this.UC2.FindControl("cblSurgicalConditions");  
                DataTable dtSurgicalConditions = GetCheckBoxListValues(cblSurgicalConditions, "SurgicalConditions");
                dtmuiltselect.Merge(dtSpecificMedicalCondition);
                dtmuiltselect.Merge(dtSurgicalConditions);
                theHT = HT(dqchk, tabname);
                DsReturns = KNHADULTFWUP.SaveUpdateKNHRevisedFollowupData_CATab(theHT, dtmuiltselect, dqchk, 0, Convert.ToInt32(Session["AppUserId"]), Convert.ToString(Session["TechnicalAreaId"]));
                
                tabindex = 2;
            }
            
            else if (savetabname == "Examination" || tabControl.ActiveTabIndex == 3)
            {
                tabname = "Examination";
                CheckBoxList cblGeneralConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblGeneralConditions");
                DataTable dtGeneralConditions = GetCheckBoxListValues(cblGeneralConditions, "GeneralConditions");
                dtmuiltselect.Merge(dtGeneralConditions);
                CheckBoxList cblCardiovascularConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblCardiovascularConditions");
                DataTable dtCardiovascularConditions = GetCheckBoxListValues(cblCardiovascularConditions, "CardiovascularConditions");
                dtmuiltselect.Merge(dtCardiovascularConditions);
                CheckBoxList cblOralCavityConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblOralCavityConditions");
                DataTable dtOralCavityConditions = GetCheckBoxListValues(cblOralCavityConditions, "OralCavityConditions");
                dtmuiltselect.Merge(dtOralCavityConditions);
                CheckBoxList cblGenitalUrinaryConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblGenitalUrinaryConditions");
                DataTable dtGenitalUrinaryConditions = GetCheckBoxListValues(cblGenitalUrinaryConditions, "GenitalUrinaryConditions");
                dtmuiltselect.Merge(dtGenitalUrinaryConditions);
                CheckBoxList cblCNSConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblCNSConditions");
                DataTable dtCNSConditions = GetCheckBoxListValues(this.UcPhysicalExam.cblCNSConditions, "CNSConditions");
                dtmuiltselect.Merge(dtCNSConditions);
                CheckBoxList cblChestLungsConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblChestLungsConditions");
                DataTable dtChestLungsConditions = GetCheckBoxListValues(cblChestLungsConditions, "ChestLungsConditions");
                dtmuiltselect.Merge(dtChestLungsConditions);
                CheckBoxList cblSkinConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblSkinConditions");
                DataTable dtSkinConditions = GetCheckBoxListValues(cblSkinConditions, "SkinConditions");
                dtmuiltselect.Merge(dtSkinConditions);
                CheckBoxList cblAbdomenConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblAbdomenConditions");
                DataTable dtAbdomenConditions = GetCheckBoxListValues(cblAbdomenConditions, "AbdomenConditions");
                dtmuiltselect.Merge(dtAbdomenConditions);
                //////MultiSelect////Short term effects
                DataTable dtShorttermeffects = GetCheckBoxListValues(cblShorttermeffects, "ShortTermEffects");
                dtmuiltselect.Merge(dtShorttermeffects);
                DataTable dtlistlongtermeffect = GetCheckBoxListValues(chklistlongtermeffect, "LongTermEffects");
                dtmuiltselect.Merge(dtlistlongtermeffect);
                //////MultiSelect////cblDiagnosis2
                DataTable dtDiagnosis2 = GetCheckBoxListValues(cblDiagnosis2, "Diagnosis");
                dtmuiltselect.Merge(dtDiagnosis2);
                //WHO Stage I
                GridView gvWHO1 = (GridView)UCWHO.FindControl("gvWHO1");
                dtmuiltselect = InsertMultiSelectList(gvWHO1, dtmuiltselect, "CurrentWHOStageIConditions");

                //// WHO Stage II
                GridView gvWHO2 = (GridView)UCWHO.FindControl("gvWHO2");
                dtmuiltselect = InsertMultiSelectList(gvWHO2, dtmuiltselect, "CurrentWHOStageIIConditions");

                //// WHO Stage III
                GridView gvWHO3 = (GridView)UCWHO.FindControl("gvWHO3");
                dtmuiltselect = InsertMultiSelectList(gvWHO3, dtmuiltselect, "CurrentWHOStageIIIConditions");

                //// WHO Stage IV
                GridView gvWHO4 = (GridView)UCWHO.FindControl("gvWHO4");
                dtmuiltselect = InsertMultiSelectList(gvWHO4, dtmuiltselect, "CurrentWHOStageIVConditions");
                theHT = HT(dqchk, tabname);
                DsReturns = KNHADULTFWUP.SaveUpdateKNHRevisedFollowupData_ExamTab(theHT, dtmuiltselect, dqchk, 0, Convert.ToInt32(Session["AppUserId"]), Convert.ToString(Session["TechnicalAreaId"]));
                
                tabindex = 4;

            }
            else if (savetabname == "Management" || tabControl.ActiveTabIndex == 4)
            {
                tabname = "Management";
                DataTable dtARTchangecode = GetCheckBoxListValues(UserControlKNH_Pharmacy1.chklistARTchangecode, "ARTchangecode");
                dtmuiltselect.Merge(dtARTchangecode);
                DataTable dtARTeligibilityCriteria = GetCheckBoxListValues(UserControlKNH_Pharmacy1.chklistEligiblethrough, "ARTEligibility");
                dtmuiltselect.Merge(dtARTeligibilityCriteria);
                DataTable dtARTstopcode = GetCheckBoxListValues(UserControlKNH_Pharmacy1.chklistARTstopcode, "ARTstopcode");
                dtmuiltselect.Merge(dtARTstopcode);
                theHT = HT(dqchk, tabname);
                DsReturns = KNHADULTFWUP.SaveUpdateKNHRevisedFollowupData_MgtTab(theHT, dtmuiltselect, dqchk, 0, Convert.ToInt32(Session["AppUserId"]), Convert.ToString(Session["TechnicalAreaId"]));
                
                tabindex = 5;
            }
           
            if (Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]) > 0)
            {
                Session["PatientVisitId"] = Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]);
                SaveCancel(savetabname);
                //BindExistingData();
                checkIfPreviuosTabSaved();
                tabControl.ActiveTabIndex = tabindex;
            }
        }
        private Boolean fieldValidation(string TabName)
        {
            string visitdate = string.Empty;
            IQCareUtils theUtil = new IQCareUtils();
            MsgBuilder totalMsgBuilder = new MsgBuilder();
            if ((Convert.ToInt32(Session["PatientVisitId"]) == 0)||(Session["PatientVisitId"]==null))            
            {
                visitdate = theUtil.MakeDate(txtvisitDate.Value);
                //KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
                DataSet dsValidate = KNHStatic.GetExistKNHStaticFormbydate(Convert.ToInt32(Session["PatientId"]), txtvisitDate.Value.ToString(), Convert.ToInt32(Session["AppLocationId"]), 23);

                if (dsValidate.Tables[0].Rows.Count > 0)
                {
                    totalMsgBuilder.DataElements["FormName"] = "Adult Follow up Form";
                    IQCareMsgBox.Show("KNHFormRecordExist", totalMsgBuilder, this);
                    return false; ;

                }
            }
            if (txtvisitDate.Value.Trim() == "")
            {

                totalMsgBuilder.DataElements["MessageText"] = "Enter Encounter Date";
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                return false;

            }

            int count = 0;
            if (TabName == "Triage" || TabName == "btnSubmitTriage")
            {
                if (rdopatientcaregiver.SelectedItem == null)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Patient accompanied by caregiver";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    lblPtnAccByCareGiver.ForeColor = Color.Red;
                    lblClientInfo.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    lblPtnAccByCareGiver.ForeColor = Color.FromArgb(0, 0, 142);
                    lblClientInfo.ForeColor = Color.FromArgb(0, 0, 142);
                }
                if (rdoaddresschanged.SelectedItem == null)
                {

                    totalMsgBuilder.DataElements["MessageText"] = "Has your address or phone changed";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    lbladdresschanged.ForeColor = Color.Red;
                    lblClientInfo.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    lbladdresschanged.ForeColor = Color.FromArgb(0, 0, 142);
                    lblClientInfo.ForeColor = Color.FromArgb(0, 0, 142);
                }
                if (this.idVitalSign.txtHeight.Text == "")
                {

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

                    totalMsgBuilder.DataElements["MessageText"] = "Enter BP Systolic";
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

                    totalMsgBuilder.DataElements["MessageText"] = "Enter BP Diastolic";
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

            }
            else if (TabName == "Clinical History" || tabControl.ActiveTabIndex == 1)
            {
                count = 0;
                GridView gvPresentingComplaints = (GridView)UCPresComp.FindControl("gvPresentingComplaints");
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
                    UCPresComp.gvPresentingComplaints.HeaderStyle.ForeColor = Color.Red;
                    lblPresComp.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    UCPresComp.gvPresentingComplaints.HeaderStyle.ForeColor = Color.FromArgb(0, 0, 142);
                    lblPresComp.ForeColor = Color.FromArgb(0, 0, 142);
                }

                //Pre existing medical condition 
                count = 0;
                for (int i = 0; i < cblSpecificMedicalCondition.Items.Count; i++)
                {
                    if (cblSpecificMedicalCondition.Items[i].Selected)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Pre existing medical condition";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    lblPreExistingMedicalCondition.ForeColor = Color.Red;
                    lblheadPastmedicalHistory.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    lblPreExistingMedicalCondition.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadPastmedicalHistory.ForeColor = Color.FromArgb(0, 0, 142);
                }
                //
                //if (rdoMedicalCondition.SelectedItem == null)
                //{
                //    totalMsgBuilder.DataElements["MessageText"] = "Select Medical Condition";
                //    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                //    lblMedicalcondition.ForeColor = Color.Red;
                //    lblheadPastmedicalHistory.ForeColor = Color.Red;
                //    return false;
                //}
                //else
                //{
                //    lblMedicalcondition.ForeColor = Color.FromArgb(0, 0, 142);
                //    lblheadPastmedicalHistory.ForeColor = Color.FromArgb(0, 0, 142);
                //}
                if (ddlHIVAssociatedConditionsPeads.SelectedIndex == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "HIV associated conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    lblHIVAssociatedCond.ForeColor = Color.Red;
                    lblheadPastmedicalHistory.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    lblHIVAssociatedCond.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadPastmedicalHistory.ForeColor = Color.FromArgb(0, 0, 142);
                }
              
            }

            else if (TabName == "Examination" || tabControl.ActiveTabIndex == 3)
            {
                //for (int i = 0; i < chkLongTermMedication.Items.Count; i++)
                //{
                //    if (chkLongTermMedication.Items[i].Selected == true)
                //    {
                //        count++;
                //    }
                //}
                //if (count == 0)
                //{
                //    totalMsgBuilder.DataElements["MessageText"] = "Select Long Term Medication";
                //    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                //    lblchlongtermmed.ForeColor = Color.Red;
                //    lblMedicalConditions.ForeColor = Color.Red;
                //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValLTM", "alert('Select Long Term Medication')", true);
                //    return false;
                //}
                //else
                //{
                //    lblchlongtermmed.ForeColor = Color.FromArgb(0, 0, 142);
                //    lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
                //}
                count = 0;
                for (int i = 0; i < this.UcPhysicalExam.cblGeneralConditions.Items.Count; i++)
                {
                    if (this.UcPhysicalExam.cblGeneralConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select General Condition";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysicalExam.lblGeneral.ForeColor = Color.Red;
                    lblheadPhysicalExam.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValGC", "alert('Select General Condition')", true);
                    return false;
                }
                else
                {
                    UcPhysicalExam.lblGeneral.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadPhysicalExam.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPhysicalExam.cblCardiovascularConditions.Items.Count; i++)
                {
                    if (this.UcPhysicalExam.cblCardiovascularConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Cardiovascular Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysicalExam.lblCardiovarscular.ForeColor = Color.Red;
                    lblheadPhysicalExam.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCC", "alert('Select Cardiovascular Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysicalExam.lblCardiovarscular.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadPhysicalExam.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPhysicalExam.cblOralCavityConditions.Items.Count; i++)
                {
                    if (this.UcPhysicalExam.cblOralCavityConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Oral Cavity Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysicalExam.lblOralCavity.ForeColor = Color.Red;
                    lblheadPhysicalExam.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValOC", "alert('Select Oral Cavity Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysicalExam.lblOralCavity.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadPhysicalExam.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPhysicalExam.cblGenitalUrinaryConditions.Items.Count; i++)
                {
                    if (this.UcPhysicalExam.cblGenitalUrinaryConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select GenitalUrinary Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysicalExam.lblGenitourinary.ForeColor = Color.Red;
                    lblheadPhysicalExam.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValGU", "alert('Select GenitalUrinary Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysicalExam.lblGenitourinary.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadPhysicalExam.ForeColor = Color.FromArgb(0, 0, 142);
                }

                count = 0;
                for (int i = 0; i < this.UcPhysicalExam.cblCNSConditions.Items.Count; i++)
                {
                    if (this.UcPhysicalExam.cblCNSConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select CNS Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysicalExam.lblSkin.ForeColor = Color.Red;
                    lblheadPhysicalExam.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCNS", "alert('Select CNS Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysicalExam.lblSkin.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadPhysicalExam.ForeColor = Color.FromArgb(0, 0, 142);
                }

                count = 0;
                for (int i = 0; i < this.UcPhysicalExam.cblChestLungsConditions.Items.Count; i++)
                {
                    if (this.UcPhysicalExam.cblChestLungsConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select ChestLung Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysicalExam.lblChest.ForeColor = Color.Red;
                    lblheadPhysicalExam.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValChest", "alert('Select ChestLung Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysicalExam.lblChest.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadPhysicalExam.ForeColor = Color.FromArgb(0, 0, 142);
                }

                count = 0;
                for (int i = 0; i < this.UcPhysicalExam.cblSkinConditions.Items.Count; i++)
                {
                    if (this.UcPhysicalExam.cblSkinConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Skin Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysicalExam.lblSkin.ForeColor = Color.Red;
                    lblheadPhysicalExam.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValSkin", "alert('Select Skin Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysicalExam.lblSkin.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadPhysicalExam.ForeColor = Color.FromArgb(0, 0, 142);
                }

                count = 0;
                for (int i = 0; i < this.UcPhysicalExam.cblAbdomenConditions.Items.Count; i++)
                {
                    if (this.UcPhysicalExam.cblAbdomenConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Abdomen Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysicalExam.lblAbdomen.ForeColor = Color.Red;
                    lblheadPhysicalExam.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValAbdomen", "alert('Select Abdomen Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysicalExam.lblAbdomen.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadPhysicalExam.ForeColor = Color.FromArgb(0, 0, 142);
                }
                if (UCWHO.ddlwhostage1.SelectedIndex == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select WHO Stage";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UCWHO.lblWHOStage.ForeColor = Color.Red;
                    lblheadWHOStage.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    UCWHO.lblWHOStage.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadWHOStage.ForeColor = Color.FromArgb(0, 0, 142);
                }
                if (UCWHO.ddlWABStage.SelectedIndex == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select WAB Stage";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UCWHO.lblWABStage.ForeColor = Color.Red;
                    lblheadWHOStage.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    UCWHO.lblWABStage.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadWHOStage.ForeColor = Color.FromArgb(0, 0, 142);
                }

            }
            else if (TabName == "Management" || tabControl.ActiveTabIndex == 4)
            {

                if (this.UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedIndex == 0)
                {

                    totalMsgBuilder.DataElements["MessageText"] = "Select Treatment Plan";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UserControlKNH_Pharmacy1.lblTreatmentplan.ForeColor = Color.Red;
                    lblheadregimenpresc.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    UserControlKNH_Pharmacy1.lblTreatmentplan.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadregimenpresc.ForeColor = Color.FromArgb(0, 0, 142);
                }
                //for (int i = 0; i < cblDrugAllergiesToxicities.Items.Count; i++)
                //{
                //    if (cblDrugAllergiesToxicities.Items[i].Selected == true)
                //    {
                //        count++;
                //    }
                //}
                //if (count == 0)
                //{
                //    totalMsgBuilder.DataElements["MessageText"] = "Select Drug Allergy Toxicities";
                //    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValDAllergy", "alert('Select Drug Allergy Toxicities')", true);
                //    return false;
                //}
            }

            return true;

        }
        
        protected Hashtable HT(int qltyFlag, string tabname)
        {

            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Session["PatientVisitId"]);
                theHT.Add("locationID", Session["AppLocationId"]);
                // Visit date:
                theHT.Add("visitDate", txtvisitDate.Value);
                theHT.Add("qltyFlag", qltyFlag.ToString());
                if (startTime != string.Empty)
                    theHT.Add("starttime", startTime);
                else                  
                    theHT.Add("starttime", String.Format("{0:dd-MMM-yyyy hh:mm:ss}", DateTime.Now));
                //Patient accompanied by caregiver
                if (tabname.ToString() == "Triage")
                {
                    theHT.Add("ChildAccompaniedByCaregiver", rdopatientcaregiver.SelectedValue);
                    theHT.Add("TreatmentSupporterRelationship", Convert.ToInt32(ddlcaregiverrelationship.SelectedValue));
                    theHT.Add("DisclosureStatus", Convert.ToInt32(ddlDisclosureStatus.SelectedValue));
                    theHT.Add("HealthEducation", rdoHealthEducation.SelectedValue);
                    theHT.Add("ReasonNotDisclosed", txtReasonNotDisclosed.Text);
                    theHT.Add("OtherDisclosureReason", txtOtherDisclosureReason.Text);
                    theHT.Add("SchoolingStatus", Convert.ToInt32(ddlSchoolingStatus.SelectedValue));
                    theHT.Add("HighestLevelAttained", Convert.ToInt32(ddlHighestLevelAttained.SelectedValue));
                    theHT.Add("HIVSupportGroup", rdoHIVSupportGroup.SelectedValue);
                    theHT.Add("HIVSupportGroupMembership", txtHIVSupportGroupMembership.Text);
                    theHT.Add("AddressChanged", rdoaddresschanged.SelectedValue);
                    theHT.Add("AddressChange", txtAddress_change.Text);
                    theHT.Add("PhoneNoChange", txtUpdated_phone.Text);
                    #region "Vital Signs"
                    if (this.idVitalSign.txtTemp.Text != "")
                    {
                        theHT.Add("Temp", this.idVitalSign.txtTemp.Text.ToString());
                    }
                    else
                    {
                        theHT.Add("Temp", "0");
                    }
                    if (this.idVitalSign.txtRR.Text != "")
                    {
                        theHT.Add("RR", this.idVitalSign.txtRR.Text);
                    }
                    else
                    {
                        theHT.Add("RR", "0");
                    }
                    if (this.idVitalSign.txtHR.Text != "")
                    {
                        theHT.Add("HR", this.idVitalSign.txtHR.Text);
                    }
                    else
                    {
                        theHT.Add("HR", "0");
                    }
                    if (this.idVitalSign.txtHeight.Text != "")
                    {
                        theHT.Add("height", this.idVitalSign.txtHeight.Text);
                    }
                    else
                    {
                        theHT.Add("height", "0");
                    }
                    if (this.idVitalSign.txtWeight.Text != "")
                    {
                        theHT.Add("weight", this.idVitalSign.txtWeight.Text);
                    }
                    else
                    {
                        theHT.Add("weight", "0");
                    }
                    if (this.idVitalSign.txtBPDiastolic.Text != "")
                    {
                        theHT.Add("BPDiastolic", this.idVitalSign.txtBPDiastolic.Text);
                    }
                    else
                    {
                        theHT.Add("BPDiastolic", "0");
                    }
                    if (this.idVitalSign.txtBPSystolic.Text != "")
                    {
                        theHT.Add("BPSystolic", this.idVitalSign.txtBPSystolic.Text);
                    }
                    else
                    {
                        theHT.Add("BPSystolic", "0");
                    }
                    if (this.idVitalSign.txtheadcircumference.Text != "")
                    {
                        theHT.Add("HeadCircumference", this.idVitalSign.txtheadcircumference.Text);
                    }
                    else
                    {
                        theHT.Add("HeadCircumference", "0");
                    }

                    
                    theHT.Add("NursesComments", this.idVitalSign.txtnursescomments.Text);
                    theHT.Add("ReferSpecClinic", this.idVitalSign.txtReferToSpecialistClinic.Text);
                    theHT.Add("ReferOther", this.idVitalSign.txtSpecifyOtherRefferedTo.Text);
                    #endregion
                }
                
                if (tabname.ToString() == "Clinical History")
                {
                    #region "Presenting Complaints"                    
                    theHT.Add("OtherPresentingComplaints", UCPresComp.txtOtherPresentingComplaints.Text);
                    theHT.Add("OtherAdditionPresentingComplaints", UCPresComp.txtAdditionalComplaints.Text);
                    #endregion
                    theHT.Add("SchoolPerfomance", Convert.ToInt32(ddlSchoolPerfomance.SelectedValue));
                    #region "Past Medical History"

                    //theHT.Add("MedicalCondition", Convert.ToInt32(this.rdoMedicalCondition.SelectedValue));
                    theHT.Add("OtherMedicalCondition", this.txtothermedconditon.Text);
                    theHT.Add("CurrentSurgicalCondition", this.txtCurrentSurgicalCondition.Text);
                    theHT.Add("PreviousSurgicalCondition", this.txtPreviousSurgicalCondition.Text);
                    theHT.Add("PreExistingMedicalConditionsFUP", this.rdoPreExistingMedicalConditionsFUP.SelectedValue);
                    theHT.Add("Antihypertensives", this.txtAntihypertensives.Text);
                    theHT.Add("Anticonvulsants", this.txtAnticonvulsants.Text);
                    theHT.Add("Hypoglycemics", this.txtHypoglycemics.Text);
                    theHT.Add("RadiotherapyChemotherapy", this.txtRadiotherapyChemotherapy.Text);
                    theHT.Add("OtherCurrentLongtermMedication", this.txtothers.Text);
                    theHT.Add("PreviousAdmission", this.rdoPreviousAdmission.SelectedValue);
                    theHT.Add("PreviousAdmissionDiagnosis", this.txtPreviousAdmissionDiagnosis.Text);
                    //HtmlInputText txtPreviousAdmissionStart = (HtmlInputText)FindControl("txtPreviousAdmissionStart");
                    theHT.Add("PreviousAdmissionStart", txtPreviousAdmissionStart.Value);
                    theHT.Add("PreviousAdmissionEnd", this.txtPreviousAdmissionEnd.Value);
                    //DropDownList ddlHIVAssociatedConditionsPeads = (DropDownList)FindControl("ddlHIVAssociatedConditionsPeads");
                    theHT.Add("HIVAssociatedConditionsPeads", Convert.ToInt32(ddlHIVAssociatedConditionsPeads.SelectedValue));
                    //theHT.Add("starttime", startTime);
                    #endregion

                }
                if (tabname == "Examination" || tabControl.ActiveTabIndex == 3)
                {
                    tabname = "Examination";
                    theHT.Add("OtherGeneralConditions", UcPhysicalExam.txtOtherGeneralConditions.Text);

                    theHT.Add("OtherAbdomenConditions", UcPhysicalExam.txtOtherAbdomenConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherCardiovascularConditions", UcPhysicalExam.txtOtherCardiovascularConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherOralCavityConditions", UcPhysicalExam.txtOtherOralCavityConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherGenitourinaryConditions", UcPhysicalExam.txtOtherGenitourinaryConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherCNSConditions", UcPhysicalExam.txtOtherCNSConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherChestLungsConditions", UcPhysicalExam.txtOtherChestLungsConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherSkinConditions", UcPhysicalExam.txtOtherSkinConditions.Text);
                    theHT.Add("OtherMedicalConditionNotes", UcPhysicalExam.txtOtherMedicalConditionNotes.Text);
                    //////////ARV Side Effects//////////////
                    int ARVsideeffects = rdoARVsideeffectsyes.Checked ? 1 : rdoARVsideeffectsno.Checked ? 0 : 9;
                    theHT.Add("ARVSideEffects", ARVsideeffects);
                    theHT.Add("Specifyothershorttermeffects", txtSpecifyothershorttermeffects.Text);
                    //////MultiSelect////Long term effects
                    theHT.Add("OtherLongtermEffects", txtlistlongtermeffect.Text);
                    //////////Diagnosis//////////////
                    theHT.Add("ReviewedPreviousResults", rdoReviewedPreviousResults.SelectedValue);
                    theHT.Add("ResultsReviewComments", txtResultsReviewComments.Text);

                    theHT.Add("HIVRelatedOI", txtHIVRelatedOI.Text);
                    theHT.Add("NonHIVRelatedOI", txtNonHIVRelatedOI.Text);
                    #region "WHO Staging"

                    theHT.Add("WABStage", Convert.ToInt32(UCWHO.ddlWABStage.SelectedValue));
                    //////////Progression in WHO//////////////
                    theHT.Add("ProgressionInWHOstage", UCWHO.rdoProgressionInWHOstage.SelectedValue);
                    theHT.Add("SpecifyWHOprogression", UCWHO.txtSpecifyWHOprogression.Text);
                    theHT.Add("CurrentWHOStage", Convert.ToInt32(UCWHO.ddlwhostage1.SelectedValue));
                    int Menarche = UCWHO.radbtnMernarcheyes.Checked ? 1 : UCWHO.radbtnMernarcheno.Checked ? 0 : 9;
                    theHT.Add("Menarche", Convert.ToInt32(Menarche));
                    theHT.Add("MenarcheDate", UCWHO.txtmenarchedate.Value);
                    theHT.Add("TannerStaging", Convert.ToInt32(UCWHO.ddltannerstaging.SelectedValue));
                    //theHT.Add("PatientFUStatus", Convert.ToInt32(UCWHO.ddlPatFUstatus.SelectedValue));
                    #endregion
                }
                if (tabname == "Management" || tabControl.ActiveTabIndex == 4)
                {
                    tabname = "Management";
                    //////////Adherence Assessment//////////////
                    theHT.Add("MissedDosesFUP", rdoHaveyoumissedanydoses.SelectedValue);
                    theHT.Add("MissedDosesFUPspecify", txtSpecifywhydosesmissed.Text);
                    theHT.Add("DelaysInTakingMedication", rdohavedelayed.SelectedValue);
                    theHT.Add("DelaysMedReferConsul", rdoHaveyoumissedanydoses.SelectedValue);
                    //////////Plan//////////////
                    theHT.Add("WorkUpPlan", txtworkupplan.Text);
                    theHT.Add("SpecifyLabEvaluation", UcLabEval.txtlabdiagnosticinput.Text);
                    //////////Treatment//////////////
                    theHT.Add("ARTTreatmentPlan", Convert.ToInt32(UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedValue));
                    theHT.Add("OtherEligiblethorugh", UserControlKNH_Pharmacy1.txtSpecifyOtherEligibility.Text);
                    theHT.Add("OtherARTStopCode", UserControlKNH_Pharmacy1.txtSpecifyOtherStopCode.Text);
                    theHT.Add("NumberDrugsSubstituted", UserControlKNH_Pharmacy1.txtNoofdrugssubstituted.Text);
                    theHT.Add("SpecifyotherARTchangereason", UserControlKNH_Pharmacy1.txtSpecifyotherARTchangereason.Text);
                    theHT.Add("2ndLineRegimenSwitch", Convert.ToInt32(UserControlKNH_Pharmacy1.ddlReasonforswitchto2ndlineregimen.SelectedValue));

                    //////////Regimen Prescribed//////////////                    
                    theHT.Add("OIProphylaxis", Convert.ToInt32(ddlOIProphylaxis.SelectedValue));
                    theHT.Add("ReasonCTXpresribed", Convert.ToInt32(ddlCotrimoxazoleprescribed.SelectedValue));
                    theHT.Add("OtherOIProphylaxis", txtOtherOIPropholyxis.Text);
                    theHT.Add("OtherTreatment", txtOthertreatment.Text);
                    theHT.Add("Fluconazole", Convert.ToInt32(ddlfluconazole.SelectedValue));
                }
                

            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;

        }
        private DataTable CreateTempTable()
        {
            DataTable dtprescompl = new DataTable();
            DataColumn theID = new DataColumn("ID");
            theID.DataType = System.Type.GetType("System.Int32");
            dtprescompl.Columns.Add(theID);
            DataColumn theDateValue1 = new DataColumn("DateField1");
            theDateValue1.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theDateValue1);
            DataColumn theValue1 = new DataColumn("OtherNotes");
            theValue1.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theValue1);
            DataColumn theFieldName = new DataColumn("FieldName");
            theFieldName.DataType = System.Type.GetType("System.String");
            dtprescompl.Columns.Add(theFieldName);
            return dtprescompl;
        }
        private DataTable GetCheckBoxListValues(CheckBoxList chklist, string fieldname)
        {
            DataTable dt = CreateTempTable();

            DataRow dr;


            for (int i = 0; i < chklist.Items.Count; i++)
            {
                if (chklist.Items[i].Selected)
                {
                    dr = dt.NewRow();
                    dr["ID"] = Convert.ToInt32(chklist.Items[i].Value);
                    dr["FieldName"] = fieldname.ToString();
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

                if (fieldname == "CurrentWHOStageIConditions")
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
                if (fieldname == "CurrentWHOStageIIConditions")
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
                if (fieldname == "CurrentWHOStageIIIConditions")
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
                if (fieldname == "CurrentWHOStageIVConditions")
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
        private void SaveCancel(string tabname)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            IQCareMsgBox.NotifyAction(tabname + " Tab saved successfully.", "Adult Followup", true, this, "");
            //MsgBuilder totalMsgBuilder = new MsgBuilder();
            //totalMsgBuilder.DataElements["MessageText"] = tabname + " Tab saved successfully.";
            //IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
            //string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            //script += "var ans;\n";
            //script += "ans=window.confirm(" + tabname + " Tab saved successfully. Do you want to close?');\n";
            //script += "if (ans==true)\n";
            //script += "{\n";
            //script += "window.location.href='frmPatient_History.aspx?PatientId=" + PatientID + "';\n";
            //script += "}\n";
            //script += "</script>\n";
            //RegisterStartupScript("confirm", script);
        }

        protected void btncomplete_Click(object sender, EventArgs e)
        {
            //if (fieldValidation() == false)
            //{
            //    return;
            //}
            //Hashtable theHT = HT(0);

            //KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            //DataSet DsReturns = KNHPEP.SaveUpdateKNHPEPData(theHT, PreExistingMedicalConditions, LabEvaluationsSpecify, ShortTermEffects, LongTermEffects);
            //Session["Redirect"] = "0";
            //if (Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]) > 0)
            //{
            //    SaveCancel();
            //}
        }
        //private Boolean fieldValidation()
        //{
        //    IQCareUtils theUtil = new IQCareUtils();
        //    DataTable PreExistingMedicalConditions;
        //    PreExistingMedicalConditions = GetCheckBoxListValues(cblMedicalConditions);
        //    if (txtvisitDate.Value.Trim() == "")
        //    {
        //        MsgBuilder totalMsgBuilder = new MsgBuilder();
        //        totalMsgBuilder.DataElements["MessageText"] = "Enter visit date";
        //        IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
        //        return false;

        //    }
        //    int childaccompaniedbycaregiver = rdopatientcaregiverYes.Checked ? 1 : rdopatientcaregiverNo.Checked ? 0 : 9;
        //    if (childaccompaniedbycaregiver == 9)
        //    {
        //        IQCareMsgBox.Show("patientcaregiver", this);
        //        return false;
        //    }
        //    if (this.idVitalSign.txtHeight.Text == "")
        //    {
        //        MsgBuilder totalMsgBuilder = new MsgBuilder();
        //        totalMsgBuilder.DataElements["MessageText"] = "Enter Height";
        //        IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
        //        return false;
        //    }
        //    if (this.idVitalSign.txtWeight.Text == "")
        //    {
        //        MsgBuilder totalMsgBuilder = new MsgBuilder();
        //        totalMsgBuilder.DataElements["MessageText"] = "Enter Weight";
        //        IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
        //        return false;
        //    }
        //    if (PreExistingMedicalConditions.Rows.Count <= 0)
        //    {
        //        IQCareMsgBox.Show("MedicalConditions", this);
        //        return false;

        //    }
        //    int drugallergiestoxicities = rdodrugallergiesyes.Checked ? 1 : rdodrugallergiesno.Checked ? 0 : 9;
        //    if (drugallergiestoxicities == 9)
        //    {
        //        IQCareMsgBox.Show("DrugAllergiesToxicities", this);
        //        return false;
        //    }
        //    int arvsideeffectes = rdoarvsideeffectsyes.Checked ? 1 : rdoarvsideeffectsno.Checked ? 0 : 9;
        //    if (arvsideeffectes == 9)
        //    {
        //        IQCareMsgBox.Show("ARVSideEffects", this);
        //        return false;
        //    }
        //    int labevaluation = rdolabevaluationyes.Checked ? 1 : rdolabevaluationno.Checked ? 0 : 0;
        //    if (labevaluation == 9)
        //    {
        //        IQCareMsgBox.Show("LabEvaluation", this);
        //        return false;
        //    }
        //    return true;
        //}

        public void BindExistingData()
        {
            string script = string.Empty;
            //IPatientKNHPEP KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            //DataTable dtSignature = KNHPEP.GetSignature(175, Convert.ToInt32(Session["PatientVisitId"]));
            //foreach (DataRow dr in dtSignature.Rows)
            //{
            //    if (dr["TabName"].ToString() == "AdultFUTriage")
            //        this.UserControlKNH_SignatureTriage.lblSignature.Text = dr["UserName"].ToString();
            //    if (dr["TabName"].ToString() == "AdultFUClinicalHistory")
            //        this.UserControlKNH_SignatureClinical.lblSignature.Text = dr["UserName"].ToString();
            //    if (dr["TabName"].ToString() == "AdultFUExamination")
            //        this.UserControlKNH_SignatureExam.lblSignature.Text = dr["UserName"].ToString();
            //    if (dr["TabName"].ToString() == "AdultFUManagement")
            //        this.UserControlKNH_SignatureMgt.lblSignature.Text = dr["UserName"].ToString();
            //}
            if (Convert.ToInt32(Session["PatientVisitId"].ToString()) > 0)
            {
                KNHREVFWUP = (IKNHRevisedAdult)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHRevisedAdult, BusinessProcess.Clinical");
                DataSet dsGet = KNHREVFWUP.GetKNHRevisedAdultDetails(Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["PatientVisitId"].ToString()));
                if (dsGet.Tables[0].Rows.Count > 0)
                {
                    //------------------------------section client information

                    if (dsGet.Tables[0].Rows[0]["VisitDate"] != DBNull.Value)
                        txtvisitDate.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dsGet.Tables[0].Rows[0]["VisitDate"]));
                    if (dsGet.Tables[0].Rows[0]["ChildAccompaniedByCaregiver"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["ChildAccompaniedByCaregiver"].ToString() == "True")
                        {
                            this.rdopatientcaregiver.SelectedValue = "1";
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divChildAccompaniedByCaregivershowhideyes'>\n";
                            script += "ShowHide('divcarrelationYN','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divChildAccompaniedByCaregivershowhideyes", script);

                        }
                        else if (dsGet.Tables[0].Rows[0]["ChildAccompaniedByCaregiver"].ToString() == "False")
                        {
                            this.rdopatientcaregiver.SelectedValue = "0";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["TreatmentSupporterRelationship"] != DBNull.Value)
                        ddlcaregiverrelationship.SelectedValue = dsGet.Tables[0].Rows[0]["TreatmentSupporterRelationship"].ToString();
                    if (dsGet.Tables[0].Rows[0]["HealthEducation"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["HealthEducation"].ToString() == "True")
                        {
                            this.rdoHealthEducation.SelectedValue = "1";

                        }
                        else if (dsGet.Tables[0].Rows[0]["HealthEducation"].ToString() == "False")
                        {
                            this.rdoHealthEducation.SelectedValue = "0";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["DisclosureStatus"] != DBNull.Value)
                        ddlDisclosureStatus.SelectedValue = dsGet.Tables[0].Rows[0]["DisclosureStatus"].ToString();
                    if (dsGet.Tables[0].Rows[0]["ReasonNotDisclosed"] != DBNull.Value)
                        txtReasonNotDisclosed.Text = dsGet.Tables[0].Rows[0]["ReasonNotDisclosed"].ToString();
                    if (dsGet.Tables[0].Rows[0]["OtherDisclosureReason"] != DBNull.Value)
                        txtOtherDisclosureReason.Text = dsGet.Tables[0].Rows[0]["OtherDisclosureReason"].ToString();
                    if (dsGet.Tables[0].Rows[0]["SchoolingStatus"] != DBNull.Value)
                        ddlSchoolingStatus.SelectedValue = dsGet.Tables[0].Rows[0]["SchoolingStatus"].ToString();
                    if (dsGet.Tables[0].Rows[0]["HighestLevelAttained"] != DBNull.Value)
                        ddlHighestLevelAttained.SelectedValue = dsGet.Tables[0].Rows[0]["HighestLevelAttained"].ToString();
                    if (dsGet.Tables[0].Rows[0]["HIVSupportGroup"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["HIVSupportGroup"].ToString() == "True")
                        {
                            this.rdoHIVSupportGroup.SelectedValue = "1";

                        }
                        else if (dsGet.Tables[0].Rows[0]["HIVSupportGroup"].ToString() == "False")
                        {
                            this.rdoHIVSupportGroup.SelectedValue = "0";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["HIVSupportGroupMembership"] != DBNull.Value)
                        txtHIVSupportGroupMembership.Text = dsGet.Tables[0].Rows[0]["HIVSupportGroupMembership"].ToString();
                    if (dsGet.Tables[0].Rows[0]["AddressChanged"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["AddressChanged"].ToString() == "True")
                        {
                            this.rdoaddresschanged.SelectedValue = "1";
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divAddressChangedshowhideyes'>\n";
                            script += "ShowHide('hideaddchangeUpdateYN','show');\n";
                            script += "ShowHide('divUpdated_phone','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divAddressChangedshowhideyes", script);

                        }
                        else if (dsGet.Tables[0].Rows[0]["AddressChanged"].ToString() == "False")
                        {
                            this.rdoaddresschanged.SelectedValue = "0";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["AddressChange"] != DBNull.Value)
                        txtAddress_change.Text = dsGet.Tables[0].Rows[0]["AddressChange"].ToString();
                    if (dsGet.Tables[0].Rows[0]["PhoneNoChange"] != DBNull.Value)
                        txtUpdated_phone.Text = dsGet.Tables[0].Rows[0]["PhoneNoChange"].ToString();

                    if (dsGet.Tables[0].Rows[0]["NursesComments"] != DBNull.Value)
                        this.idVitalSign.txtnursescomments.Text = dsGet.Tables[0].Rows[0]["NursesComments"].ToString();
                    if (dsGet.Tables[0].Rows[0]["ReferSpecClinic"] != DBNull.Value)
                        this.idVitalSign.txtReferToSpecialistClinic.Text = dsGet.Tables[0].Rows[0]["ReferSpecClinic"].ToString();
                    if (dsGet.Tables[0].Rows[0]["ReferOther"] != DBNull.Value)
                        this.idVitalSign.txtSpecifyOtherRefferedTo.Text = dsGet.Tables[0].Rows[0]["ReferOther"].ToString();
                    FillCheckboxlist(idVitalSign.cblReferredTo, dsGet.Tables[1], "RefferedToFUpF");
                    //---------------section vital sign-------------------
                    if (dsGet.Tables[3].Rows.Count > 0)
                    {
                        //rdoVitalYesNo.Items[0].Selected = true;

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

                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'hideVitalYNy'>\n";
                        script += "ShowHide('hideVitalYN','show');\n";
                        script += "</script>\n";
                        RegisterStartupScript("hideVitalYNy", script);

                        //if (dsGet.Tables[3].Rows[0]["Height"] != DBNull.Value && dsGet.Tables[3].Rows[0]["Weight"] != DBNull.Value)
                        //{
                        //    decimal bmi = Convert.ToDecimal(this.idVitalSign.txtWeight.Text) / (Convert.ToDecimal(this.idVitalSign.txtHeight.Text) / 100 * Convert.ToDecimal(this.idVitalSign.txtHeight.Text) / 100);
                        //    this.idVitalSign.txtBMI.Text = Convert.ToString(Math.Round(bmi, 2));

                        //}
                    }
                    if (dsGet.Tables[0].Rows[0]["CurrentlyOnHAART"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["CurrentlyOnHAART"].ToString() == "True")
                        {
                            //this.rdocurrentlyhaart.SelectedValue = "1";
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divCurrentlyOnHAARTshowhideyes'>\n";
                            script += "ShowHide('divCurrentregimenlineYN','show');ShowHide('divCurrentARTRegimen','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divCurrentlyOnHAARTshowhideyes", script);

                        }
                        else if (dsGet.Tables[0].Rows[0]["CurrentlyOnHAART"].ToString() == "False")
                        {
                            //this.rdocurrentlyhaart.SelectedValue = "0";
                        }
                    }
                    
                    IQCareUtils theUtils = new IQCareUtils();
                    DataView theDV = new DataView(dsGet.Tables[1]);
                    theDV.RowFilter = "FieldName='PresentingComplaints'";
                    DataTable dtPres = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    if (dtPres.Rows.Count > 0)
                    {
                        bindPresentingComplaint(dtPres);                        
                    }
                    if (dsGet.Tables[0].Rows[0]["OtherPresentingComplaints"] != DBNull.Value)
                        UCPresComp.txtOtherPresentingComplaints.Text = dsGet.Tables[0].Rows[0]["OtherPresentingComplaints"].ToString();
                    if (dsGet.Tables[0].Rows[0]["PresentingComplaintsAdditionalNotes"] != DBNull.Value)
                        UCPresComp.txtAdditionalComplaints.Text = dsGet.Tables[0].Rows[0]["PresentingComplaintsAdditionalNotes"].ToString();
                    if (dsGet.Tables[0].Rows[0]["SchoolPerfomance"] != DBNull.Value)
                        ddlSchoolPerfomance.SelectedValue = dsGet.Tables[0].Rows[0]["SchoolPerfomance"].ToString();
                    //if (dsGet.Tables[0].Rows[0]["MedicalCondition"] != System.DBNull.Value)
                    //{
                    //    if (dsGet.Tables[0].Rows[0]["MedicalCondition"].ToString() == "True")
                    //    {
                    //        this.rdoMedicalCondition.SelectedValue = "1";
                    //        script = "";
                    //        script = "<script language = 'javascript' defer ='defer' id = 'divMedicalConditionshowhideyes'>\n";
                    //        script += "ShowHide('divcblSpecificMedicalCondition','show');\n";
                    //        script += "</script>\n";
                    //        RegisterStartupScript("divMedicalConditionshowhideyes", script);

                    //    }
                    //    else if (dsGet.Tables[0].Rows[0]["MedicalCondition"].ToString() == "False")
                    //    {
                    //        this.rdoMedicalCondition.SelectedValue = "0";
                    //    }
                    //}
                    //CheckBoxList cblSpecificMedicalCondition = (CheckBoxList)UC2.FindControl("cblSpecificMedicalCondition");
                    FillCheckboxlist(cblSpecificMedicalCondition, dsGet.Tables[1], "SpecificMedicalCondition");
                    if (dsGet.Tables[0].Rows[0]["OtherMedicalConditionNotes"] != DBNull.Value)
                        txtothermedconditon.Text = dsGet.Tables[0].Rows[0]["OtherMedicalConditionNotes"].ToString();
                    //CheckBoxList cblSurgicalConditions = (CheckBoxList)UC2.FindControl("cblSurgicalConditions");
                    FillCheckboxlist(cblSurgicalConditions, dsGet.Tables[1], "SurgicalConditions");
                    if (dsGet.Tables[0].Rows[0]["CurrentSurgicalCondition"] != DBNull.Value)
                        txtCurrentSurgicalCondition.Text = dsGet.Tables[0].Rows[0]["CurrentSurgicalCondition"].ToString();
                    if (dsGet.Tables[0].Rows[0]["PreviousSurgicalCondition"] != DBNull.Value)
                        txtPreviousSurgicalCondition.Text = dsGet.Tables[0].Rows[0]["PreviousSurgicalCondition"].ToString();
                    if (dsGet.Tables[0].Rows[0]["PreExistingMedicalConditionsFUP"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["PreExistingMedicalConditionsFUP"].ToString() == "True")
                        {
                            this.rdoPreExistingMedicalConditionsFUP.SelectedValue = "1";
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divPreExistingMedicalConditionsFUPshowhideyes'>\n";
                            script += "ShowHide('divAntihypertensives','show');ShowHide('divAnticonvulsants','show');ShowHide('divHypoglycemics','show');ShowHide('divRadiotherapyChemotherapy','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divPreExistingMedicalConditionsFUPshowhideyes", script);

                        }
                        else if (dsGet.Tables[0].Rows[0]["PreExistingMedicalConditionsFUP"].ToString() == "False")
                        {
                            this.rdoPreExistingMedicalConditionsFUP.SelectedValue = "0";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["Anticonvulsants"] != DBNull.Value)
                        this.txtAnticonvulsants.Text = dsGet.Tables[0].Rows[0]["Anticonvulsants"].ToString();
                    if (dsGet.Tables[0].Rows[0]["Antihypertensives"] != DBNull.Value)
                        this.txtAntihypertensives.Text = dsGet.Tables[0].Rows[0]["Antihypertensives"].ToString();
                    if (dsGet.Tables[0].Rows[0]["Hypoglycemics"] != DBNull.Value)
                        this.txtHypoglycemics.Text = dsGet.Tables[0].Rows[0]["Hypoglycemics"].ToString();
                    if (dsGet.Tables[0].Rows[0]["RadiotherapyChemotherapy2"] != DBNull.Value)
                        this.txtRadiotherapyChemotherapy.Text = dsGet.Tables[0].Rows[0]["RadiotherapyChemotherapy2"].ToString();
                    if (dsGet.Tables[0].Rows[0]["Othercurrentlongmedication"] != DBNull.Value)
                        this.txtothers.Text = dsGet.Tables[0].Rows[0]["Othercurrentlongmedication"].ToString();
                    if (dsGet.Tables[0].Rows[0]["PreviousAdmission"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["PreviousAdmission"].ToString() == "True")
                        {
                            this.rdoPreviousAdmission.SelectedValue = "1";
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divPreviousAdmissionshowhideyes'>\n";
                            script += "ShowHide('divPreviousAdmissionDiagnosis','show');ShowHide('divPreviousAdmissionStart','show');ShowHide('divPreviousAdmissionEnd','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divPreviousAdmissionshowhideyes", script);

                        }
                        else if (dsGet.Tables[0].Rows[0]["PreviousAdmission"].ToString() == "False")
                        {
                            this.rdoPreviousAdmission.SelectedValue = "0";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["PreviousAdmissionDiagnosis"] != DBNull.Value)
                    {
                        
                            txtPreviousAdmissionDiagnosis.Text = dsGet.Tables[0].Rows[0]["PreviousAdmissionDiagnosis"].ToString();
                    }
                    if (dsGet.Tables[0].Rows[0]["PreviousAdmissionStart"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(dsGet.Tables[0].Rows[0]["PreviousAdmissionStart"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            txtPreviousAdmissionStart.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dsGet.Tables[0].Rows[0]["PreviousAdmissionStart"].ToString()));
                    }
                    if (dsGet.Tables[0].Rows[0]["PreviousAdmissionEnd"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(dsGet.Tables[0].Rows[0]["PreviousAdmissionEnd"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            txtPreviousAdmissionEnd.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dsGet.Tables[0].Rows[0]["PreviousAdmissionEnd"].ToString()));
                    }
                    //DropDownList ddlHIVAssociatedConditionsPeads = (DropDownList)UC2.FindControl("ddlHIVAssociatedConditionsPeads");
                    if (dsGet.Tables[0].Rows[0]["HIVAssociatedConditionsPeads"] != DBNull.Value)
                        ddlHIVAssociatedConditionsPeads.SelectedValue = dsGet.Tables[0].Rows[0]["HIVAssociatedConditionsPeads"].ToString();

                    CheckBoxList chkPEGeneral = (CheckBoxList)this.UcPhysicalExam.FindControl("cblGeneralConditions");
                    FillCheckboxlist(chkPEGeneral, dsGet.Tables[1], "GeneralConditions");
                    if (dsGet.Tables[0].Rows[0]["OtherGeneralConditions"] != DBNull.Value)
                        UcPhysicalExam.txtOtherGeneralConditions.Text = dsGet.Tables[0].Rows[0]["OtherGeneralConditions"].ToString();

                    CheckBoxList cblCardiovascularConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblCardiovascularConditions");
                    FillCheckboxlist(cblCardiovascularConditions, dsGet.Tables[1], "CardiovascularConditions");
                    if (dsGet.Tables[0].Rows[0]["OtherCardiovascularConditions"] != DBNull.Value)
                        UcPhysicalExam.txtOtherCardiovascularConditions.Text = dsGet.Tables[0].Rows[0]["OtherCardiovascularConditions"].ToString();
                    CheckBoxList cblCNSConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblCNSConditions");
                    FillCheckboxlist(UcPhysicalExam.cblCNSConditions, dsGet.Tables[1], "CNSConditions");
                    if (dsGet.Tables[0].Rows[0]["OtherCNSConditions"] != DBNull.Value)
                        UcPhysicalExam.txtOtherCNSConditions.Text = dsGet.Tables[0].Rows[0]["OtherCNSConditions"].ToString();
                    CheckBoxList cblChestLungsConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblChestLungsConditions");
                    FillCheckboxlist(cblChestLungsConditions, dsGet.Tables[1], "ChestLungsConditions");
                    if (dsGet.Tables[0].Rows[0]["OtherChestLungsConditions"] != DBNull.Value)
                        UcPhysicalExam.txtOtherChestLungsConditions.Text = dsGet.Tables[0].Rows[0]["OtherChestLungsConditions"].ToString();
                    //if (dsGet.Tables[0].Rows[0]["ChestLungsConditions"] != DBNull.Value)
                    //    ddlChestLungsConditions.SelectedValue = dsGet.Tables[0].Rows[0]["ChestLungsConditions"].ToString();
                    CheckBoxList cblOralCavityConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblOralCavityConditions");
                    FillCheckboxlist(cblOralCavityConditions, dsGet.Tables[1], "OralCavityConditions");
                    if (dsGet.Tables[0].Rows[0]["OtherOralCavityConditions"] != DBNull.Value)
                        UcPhysicalExam.txtOtherOralCavityConditions.Text = dsGet.Tables[0].Rows[0]["OtherOralCavityConditions"].ToString();
                    //if (dsGet.Tables[0].Rows[0]["GenitalUrinaryConditions"] != DBNull.Value)
                    //    ddlGenitalUrinaryConditions.SelectedValue = dsGet.Tables[0].Rows[0]["GenitalUrinaryConditions"].ToString();
                    CheckBoxList cblGenitalUrinaryConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblGenitalUrinaryConditions");
                    FillCheckboxlist(cblGenitalUrinaryConditions, dsGet.Tables[1], "GenitalUrinaryConditions");
                    if (dsGet.Tables[0].Rows[0]["OtherGenitourinaryConditions"] != DBNull.Value)
                        UcPhysicalExam.txtOtherGenitourinaryConditions.Text = dsGet.Tables[0].Rows[0]["OtherGenitourinaryConditions"].ToString();
                    //if (dsGet.Tables[0].Rows[0]["SkinConditions"] != DBNull.Value)
                    //    ddlSkinConditions.SelectedValue = dsGet.Tables[0].Rows[0]["SkinConditions"].ToString();
                    CheckBoxList cblSkinConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblSkinConditions");
                    FillCheckboxlist(cblSkinConditions, dsGet.Tables[1], "SkinConditions");
                    if (dsGet.Tables[0].Rows[0]["OtherSkinConditions"] != DBNull.Value)
                        UcPhysicalExam.txtOtherSkinConditions.Text = dsGet.Tables[0].Rows[0]["OtherSkinConditions"].ToString();
                    //if (dsGet.Tables[0].Rows[0]["AbdomenConditions"] != DBNull.Value)
                    //    ddlAbdomenConditions.SelectedValue = dsGet.Tables[0].Rows[0]["AbdomenConditions"].ToString();
                    CheckBoxList cblAbdomenConditions = (CheckBoxList)this.UcPhysicalExam.FindControl("cblAbdomenConditions");
                    FillCheckboxlist(cblAbdomenConditions, dsGet.Tables[1], "AbdomenConditions");
                    if (dsGet.Tables[0].Rows[0]["OtherAbdomenConditions"] != DBNull.Value)
                        UcPhysicalExam.txtOtherAbdomenConditions.Text = dsGet.Tables[0].Rows[0]["OtherAbdomenConditions"].ToString();
                    if (dsGet.Tables[0].Rows[0]["OtherMedicalConditionNotes"] != DBNull.Value)
                        UcPhysicalExam.txtOtherMedicalConditionNotes.Text = dsGet.Tables[0].Rows[0]["OtherMedicalConditionNotes"].ToString();
                    if (dsGet.Tables[0].Rows[0]["ARVSideEffects"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["ARVSideEffects"].ToString() == "True")
                        {
                            rdoARVsideeffectsyes.Checked = true;
                            //john
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divARVSideEffectsshowhideyes'>\n";
                            script += "ShowHide('divshorttermeffects','show');ShowHide('trshorttermeffects','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divARVSideEffectsshowhideyes", script);
                        }
                        else if (dsGet.Tables[0].Rows[0]["ARVSideEffects"].ToString() == "False")
                        {
                            rdoARVsideeffectsno.Checked = true;
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divARVSideEffectsshowhideyes'>\n";
                            script += "ShowHide('divshorttermeffects','hide');ShowHide('trshorttermeffects','hide');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divARVSideEffectsshowhideyes", script);
                        }

                    }
                    FillCheckboxlist(cblShorttermeffects, dsGet.Tables[1], "ShortTermEffects");
                    FillCheckboxlist(UserControlKNH_Pharmacy1.chklistEligiblethrough, dsGet.Tables[1], "ARTEligibility");
                    FillCheckboxlist(UserControlKNH_Pharmacy1.chklistARTstopcode, dsGet.Tables[1], "ARTstopcode");
                    FillCheckboxlist(UserControlKNH_Pharmacy1.chklistARTchangecode, dsGet.Tables[1], "ARTchangecode");
                    FillCheckboxlist(chklistlongtermeffect, dsGet.Tables[1], "LongTermEffects");
                    
                    if (dsGet.Tables[0].Rows[0]["OtherLongtermEffects"] != DBNull.Value)
                        txtlistlongtermeffect.Text = dsGet.Tables[0].Rows[0]["OtherLongtermEffects"].ToString();
                    if (dsGet.Tables[0].Rows[0]["OtherShortTermEffects"] != DBNull.Value)
                        txtSpecifyothershorttermeffects.Text = dsGet.Tables[0].Rows[0]["OtherShortTermEffects"].ToString();
                    if (dsGet.Tables[0].Rows[0]["ReviewedPreviousResults"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["ReviewedPreviousResults"].ToString() == "True")
                        {
                            this.rdoReviewedPreviousResults.SelectedValue = "1";

                        }
                        else if (dsGet.Tables[0].Rows[0]["ReviewedPreviousResults"].ToString() == "False")
                        {
                            this.rdoReviewedPreviousResults.SelectedValue = "0";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["ResultsReviewComments"] != DBNull.Value)
                        txtResultsReviewComments.Text = dsGet.Tables[0].Rows[0]["ResultsReviewComments"].ToString();
                    FillCheckboxlist(cblDiagnosis2, dsGet.Tables[1], "Diagnosis");
                    if (dsGet.Tables[0].Rows[0]["HIVRelatedOI"] != DBNull.Value)
                        txtHIVRelatedOI.Text = dsGet.Tables[0].Rows[0]["HIVRelatedOI"].ToString();
                    if (dsGet.Tables[0].Rows[0]["NonHIVRelatedOI"] != DBNull.Value)
                        txtNonHIVRelatedOI.Text = dsGet.Tables[0].Rows[0]["NonHIVRelatedOI"].ToString();
                    BindWHOListData(dsGet.Tables[1]);
                    
                    if (dsGet.Tables[0].Rows[0]["ProgressionInWHOstage"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["ProgressionInWHOstage"].ToString() == "True")
                        {
                            this.UCWHO.rdoProgressionInWHOstage.SelectedValue = "1";
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divdivspecifyproghideyes'>\n";
                            script += "ShowHide('divspecifyprog','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divdivspecifyproghideyes", script);

                        }

                    }
                    if (dsGet.Tables[0].Rows[0]["SpecifyWHOprogression"] != DBNull.Value)
                        UCWHO.txtSpecifyWHOprogression.Text = dsGet.Tables[0].Rows[0]["SpecifyWHOprogression"].ToString();
                    if (dsGet.Tables[2].Rows.Count > 0)
                    {
                        if (dsGet.Tables[2].Rows[0]["WHOStage"] != DBNull.Value)
                            UCWHO.ddlwhostage1.SelectedValue = dsGet.Tables[2].Rows[0]["WHOStage"].ToString();
                        if (dsGet.Tables[2].Rows[0]["WABStage"] != DBNull.Value)
                            UCWHO.ddlWABStage.SelectedValue = dsGet.Tables[2].Rows[0]["WABStage"].ToString();
                    }
                    //Mernarche
                    if (dsGet.Tables[0].Rows[0]["Menarche"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["Menarche"].ToString() == "True")
                        {
                            this.UCWHO.radbtnMernarcheyes.Checked = true;
                            this.UCWHO.radbtnMernarcheno.Checked = false;
                            visibleDiv("divmenarchedatehshowhide");

                        }
                        else if (dsGet.Tables[0].Rows[0]["Menarche"].ToString() == "False")
                        {
                            this.UCWHO.radbtnMernarcheyes.Checked = false;
                            this.UCWHO.radbtnMernarcheno.Checked = true;
                        }
                    }

                    //Menarche Date :
                    if (dsGet.Tables[0].Rows[0]["MenarcheDate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(dsGet.Tables[0].Rows[0]["MenarcheDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            UCWHO.txtmenarchedate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["MenarcheDate"]);
                    }
                    //Tanner staging
                    if (dsGet.Tables[0].Rows[0]["TannerStaging"] != DBNull.Value)
                        UCWHO.ddltannerstaging.SelectedValue = dsGet.Tables[0].Rows[0]["TannerStaging"].ToString();
                    if (dsGet.Tables[0].Rows[0]["MissedDosesFUP"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["MissedDosesFUP"].ToString() == "True")
                        {
                            this.rdoHaveyoumissedanydoses.SelectedValue = "1";
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divMissedDosesFUPshowhideyes'>\n";
                            script += "ShowHide('divSpecifywhydosesmissed','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divMissedDosesFUPshowhideyes", script);

                        }
                        else if (dsGet.Tables[0].Rows[0]["MissedDosesFUP"].ToString() == "False")
                        {
                            this.rdoHaveyoumissedanydoses.SelectedValue = "0";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["MissedDosesFUPspecify"] != DBNull.Value)
                        txtSpecifywhydosesmissed.Text = dsGet.Tables[0].Rows[0]["MissedDosesFUPspecify"].ToString();
                    if (dsGet.Tables[0].Rows[0]["DelaysInTakingMedication"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["DelaysInTakingMedication"].ToString() == "True")
                        {
                            this.rdohavedelayed.SelectedValue = "1";

                        }
                        else if (dsGet.Tables[0].Rows[0]["DelaysInTakingMedication"].ToString() == "False")
                        {
                            this.rdohavedelayed.SelectedValue = "0";
                        }
                    }
                   
                    if (dsGet.Tables[0].Rows[0]["ReviewLabDiagtest"] != DBNull.Value)
                        UcLabEval.txtlabdiagnosticinput.Text = dsGet.Tables[0].Rows[0]["ReviewLabDiagtest"].ToString();

                    if (dsGet.Tables[0].Rows[0]["WorkUpPlan"] != DBNull.Value)
                        txtworkupplan.Text = dsGet.Tables[0].Rows[0]["WorkUpPlan"].ToString();
                    if (dsGet.Tables[0].Rows[0]["OtherTreatment"] != DBNull.Value)
                        txtOthertreatment.Text = dsGet.Tables[0].Rows[0]["OtherTreatment"].ToString();
                    if (dsGet.Tables[6].Rows.Count > 0)
                    {
                        if (dsGet.Tables[6].Rows[0]["TherapyPlan"] != DBNull.Value)
                            UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedValue = dsGet.Tables[6].Rows[0]["TherapyPlan"].ToString();
                        if (dsGet.Tables[6].Rows[0]["specifyOtherEligibility"] != DBNull.Value)
                            UserControlKNH_Pharmacy1.txtSpecifyOtherEligibility.Text = dsGet.Tables[6].Rows[0]["specifyOtherEligibility"].ToString();
                        if (dsGet.Tables[6].Rows[0]["NoOfDrugsSubstituted"] != DBNull.Value)
                            UserControlKNH_Pharmacy1.txtNoofdrugssubstituted.Text = dsGet.Tables[6].Rows[0]["NoOfDrugsSubstituted"].ToString();
                        if (dsGet.Tables[6].Rows[0]["specifyOtherARTChangeReason"] != DBNull.Value)
                            UserControlKNH_Pharmacy1.txtSpecifyotherARTchangereason.Text = dsGet.Tables[6].Rows[0]["specifyOtherARTChangeReason"].ToString();
                        if (dsGet.Tables[6].Rows[0]["specifyOtherStopCode"] != DBNull.Value)
                            UserControlKNH_Pharmacy1.txtSpecifyOtherStopCode.Text = dsGet.Tables[6].Rows[0]["specifyOtherStopCode"].ToString();
                        if (dsGet.Tables[6].Rows[0]["reasonForSwitchTo2ndLineRegimen"] != DBNull.Value)
                            UserControlKNH_Pharmacy1.ddlReasonforswitchto2ndlineregimen.SelectedValue = dsGet.Tables[6].Rows[0]["reasonForSwitchTo2ndLineRegimen"].ToString();
                    }
                    if (dsGet.Tables[0].Rows[0]["OIProphylaxis"] != DBNull.Value)
                        ddlOIProphylaxis.SelectedValue = dsGet.Tables[0].Rows[0]["OIProphylaxis"].ToString();
                    if (dsGet.Tables[0].Rows[0]["Fluconazole"] != DBNull.Value)
                        ddlfluconazole.SelectedValue = dsGet.Tables[0].Rows[0]["Fluconazole"].ToString();
                    if (dsGet.Tables[0].Rows[0]["ReasonCTXpresribed"] != DBNull.Value)
                        ddlCotrimoxazoleprescribed.SelectedValue = dsGet.Tables[0].Rows[0]["ReasonCTXpresribed"].ToString();
                    if (dsGet.Tables[0].Rows[0]["OtherOIProphylaxis"] != DBNull.Value)
                        txtOtherOIPropholyxis.Text = dsGet.Tables[0].Rows[0]["OtherOIProphylaxis"].ToString();
                    if (dsGet.Tables[0].Rows[0]["OtherTreatment"] != DBNull.Value)
                        txtOthertreatment.Text = dsGet.Tables[0].Rows[0]["OtherTreatment"].ToString();
                    

                    


                }
            }
        }
        private void BindWHOListData(DataTable theDT)
        {
            foreach (DataRow theDR in theDT.Rows)
            {

                if (Convert.ToString(theDR["FieldName"]) == "CurrentWHOStageIConditions")
                {
                    for (int i = 0; i < this.UCWHO.gvWHO1.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UCWHO.gvWHO1.Rows[i].FindControl("lblwho1");
                        CheckBox chkWHOId = (CheckBox)UCWHO.gvWHO1.Rows[i].FindControl("Chkwho1");
                        HtmlInputText txtWHODate1 = (HtmlInputText)UCWHO.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date");
                        HtmlInputText txtWHODate2 = (HtmlInputText)UCWHO.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date1");
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            if(theDR["DateField1"].ToString() !="")
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField1"].ToString()));
                            if (theDR["DateField2"].ToString() != "")
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField2"].ToString()));
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "CurrentWHOStageIIConditions")
                {
                    for (int i = 0; i < this.UCWHO.gvWHO2.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UCWHO.gvWHO2.Rows[i].FindControl("lblwho2");
                        CheckBox chkWHOId = (CheckBox)UCWHO.gvWHO2.Rows[i].FindControl("Chkwho2");
                        HtmlInputText txtWHODate1 = (HtmlInputText)UCWHO.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date");
                        HtmlInputText txtWHODate2 = (HtmlInputText)UCWHO.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date1");
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            if (theDR["DateField1"].ToString() != "")
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField1"].ToString()));
                            if (theDR["DateField2"].ToString() != "")
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField2"].ToString()));
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "CurrentWHOStageIIIConditions")
                {
                    for (int i = 0; i < this.UCWHO.gvWHO3.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UCWHO.gvWHO3.Rows[i].FindControl("lblwho3");
                        CheckBox chkWHOId = (CheckBox)UCWHO.gvWHO3.Rows[i].FindControl("Chkwho3");
                        HtmlInputText txtWHODate1 = (HtmlInputText)UCWHO.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date");
                        HtmlInputText txtWHODate2 = (HtmlInputText)UCWHO.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date1");
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            if (theDR["DateField1"].ToString() != "")
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField1"].ToString()));
                            if (theDR["DateField2"].ToString() != "")
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField2"].ToString()));
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "CurrentWHOStageIVConditions")
                {
                    for (int i = 0; i < this.UCWHO.gvWHO4.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UCWHO.gvWHO4.Rows[i].FindControl("lblwho4");
                        CheckBox chkWHOId = (CheckBox)UCWHO.gvWHO4.Rows[i].FindControl("Chkwho4");
                        HtmlInputText txtWHODate1 = (HtmlInputText)UCWHO.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date");
                        HtmlInputText txtWHODate2 = (HtmlInputText)UCWHO.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date1");
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            if (theDR["DateField1"].ToString() != "")
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField1"].ToString()));
                            if (theDR["DateField2"].ToString() != "")
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDR["DateField2"].ToString()));
                        }
                    }
                }




            }

        }
        
        private void bindPresentingComplaint(DataTable dt)
        {
            for (int j = 0; j <= dt.Rows.Count - 1; j++)
            {
                for (int i = 0; i < this.UCPresComp.gvPresentingComplaints.Rows.Count; i++)
                {
                    Label lblPComplaintsId = (Label)UCPresComp.gvPresentingComplaints.Rows[i].FindControl("lblPresenting");
                    CheckBox chkPComplaints = (CheckBox)UCPresComp.gvPresentingComplaints.Rows[i].FindControl("ChkPresenting");
                    TextBox txtPComplaints = (TextBox)UCPresComp.gvPresentingComplaints.Rows[i].FindControl("txtPresenting");
                    //chkPComplaints.Attributes.Add("onclick", "togglePC(" + chkPComplaints.ClientID + ")");
                    if (Convert.ToInt32(dt.Rows[j]["ValueId"]) == Convert.ToInt32(lblPComplaintsId.Text))
                    {
                        if (dt.Rows[j]["Name"].ToString().ToLower() == "other")
                        {
                            visibleDiv("DivOther");
                        }
                        chkPComplaints.Checked = true;
                        txtPComplaints.Text = dt.Rows[j]["NumericField"].ToString();
                        if (dt.Rows[j]["Name"].ToString().ToLower() == "none")
                        {
                            String script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'togglePresComp'>\n";
                            script += "togglePC('" + chkPComplaints.ClientID + "')\n";
                            script += "</script>\n";
                            RegisterStartupScript("'togglePresComp'", script);
                        }
                    }
                }
            }
        }
        protected void gvPresentingComplaints_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["presentingcomplains"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblPresenting");
                CheckBox chkPresenting = (CheckBox)e.Row.FindControl("ChkPresenting");
                TextBox txtPresenting = (TextBox)e.Row.FindControl("txtPresenting");
                foreach (DataRow r in dt.Rows)
                {
                    if (r[1].ToString() == lbl.Text)
                    {
                        chkPresenting.Checked = true;
                        txtPresenting.Text = r[2].ToString();
                    }
                }

            }


        }
        private DataTable ReturnMultiSeletTable(DataTable theDT, string fieldname)
        {
            IQCareUtils theUtils = new IQCareUtils();
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "FieldName='" + fieldname + "'";
            DataTable dt = (DataTable)theUtils.CreateTableFromDataView(theDV);
            return dt;
        }
        protected void gvWHO1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["WHOStage1"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("Chkwho1");
                System.Web.UI.HtmlControls.HtmlInputText txtdt1 = (HtmlInputText)e.Row.FindControl("txtCurrentWho1Date");
                System.Web.UI.HtmlControls.HtmlInputText txtdt2 = (HtmlInputText)e.Row.FindControl("txtCurrentWho1Date1");
                Label lbl = (Label)e.Row.FindControl("lblwho1");
                foreach (DataRow r in dt.Rows)
                {
                    if (r["ValueID"].ToString() == lbl.Text)
                    {
                        chk.Checked = true;
                        if (r["DateField1"].ToString() != "")
                        {
                            txtdt1.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(r["DateField1"].ToString()));
                        }
                        if (r["DateField2"].ToString() != "")
                        {
                            txtdt2.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(r["DateField2"].ToString()));
                        }
                    }
                }

            }


        }
        protected void gvWHO2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["WHOStage2"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("Chkwho2");
                System.Web.UI.HtmlControls.HtmlInputText txtdt1 = (HtmlInputText)e.Row.FindControl("txtCurrentWho2Date");
                System.Web.UI.HtmlControls.HtmlInputText txtdt2 = (HtmlInputText)e.Row.FindControl("txtCurrentWho2Date1");
                Label lbl = (Label)e.Row.FindControl("lblwho2");
                foreach (DataRow r in dt.Rows)
                {
                    if (r[1].ToString() == lbl.Text)
                    {
                        chk.Checked = true;
                        if (r[2].ToString() != "")
                        {
                            txtdt1.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(r[2].ToString()));
                        }
                        if (r[3].ToString() != "")
                        {
                            txtdt2.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(r[3].ToString()));
                        }
                    }
                }

            }


        }
        protected void gvWHO3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["WHOStage3"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("Chkwho3");
                System.Web.UI.HtmlControls.HtmlInputText txtdt1 = (HtmlInputText)e.Row.FindControl("txtCurrentWho3Date");
                System.Web.UI.HtmlControls.HtmlInputText txtdt2 = (HtmlInputText)e.Row.FindControl("txtCurrentWho3Date1");
                Label lbl = (Label)e.Row.FindControl("lblwho3");
                foreach (DataRow r in dt.Rows)
                {
                    if (r[1].ToString() == lbl.Text)
                    {
                        chk.Checked = true;
                        if (r[2].ToString() != "")
                        {
                            txtdt1.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(r[2].ToString()));
                        }
                        if (r[3].ToString() != "")
                        {
                            txtdt2.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(r[3].ToString()));
                        }
                    }
                }

            }


        }
        protected void gvWHO4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["WHOStage4"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("Chkwho4");
                System.Web.UI.HtmlControls.HtmlInputText txtdt1 = (HtmlInputText)e.Row.FindControl("txtCurrentWho4Date");
                System.Web.UI.HtmlControls.HtmlInputText txtdt2 = (HtmlInputText)e.Row.FindControl("txtCurrentWho4Date1");
                Label lbl = (Label)e.Row.FindControl("lblwho4");
                foreach (DataRow r in dt.Rows)
                {
                    if (r[1].ToString() == lbl.Text)
                    {
                        chk.Checked = true;
                        if (r[2].ToString() != "")
                        {
                            txtdt1.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(r[2].ToString()));
                        }
                        if (r[3].ToString() != "")
                        {
                            txtdt2.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(r[3].ToString()));
                        }
                    }
                }

            }


        }
        protected void btnTriagesave_Click(object sender, EventArgs e)
        {
            Save(0);
        }
        protected void btnTriageDQsave_Click(object sender, EventArgs e)
        {
            Save(1);
        }
        protected void btnClinicalHistorySave_Click(object sender, EventArgs e)
        {
            Save(0);
        }
        protected void btnClinicalHistoryDQSave_Click(object sender, EventArgs e)
        {
            Save(1);
        }
        protected void btnExaminationSave_Click(object sender, EventArgs e)
        {
            Save(0);
        }
        protected void btnExaminationDQSave_Click(object sender, EventArgs e)
        {
            Save(1);
        }
        protected void btnManagementSave_Click(object sender, EventArgs e)
        {
            Save(0);
        }
        protected void btnManagementDQSave_Click(object sender, EventArgs e)
        {
            Save(1);
        }
        public void usePEFUForm()
        {
            DataTable FURuleFormRules = new DataTable();
            IQCareUtils theUtils = new IQCareUtils();
            //KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            FURuleFormRules = KNHStatic.GetPatientFeatures(Convert.ToInt32(Session["PatientId"]));
            DataView theCodeDV = new DataView(FURuleFormRules);
            //theCodeDV.RowFilter = "VisitType=25";
            DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
            if (theDT.Rows.Count == 0)
            {
                //string script = "alert('Adult Follow up Form cannot be saved before Adult Initial Evaluation Form. Please save Initial Evaluation form. Redirecting...');";
                //script += "window.location.replace('frmClinical_KNH_AdultIE.aspx');";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "2FirstVisits", script, true);
                IQCareMsgBox.NotifyAction("Adult Follow up Form cannot be saved before Adult Initial Evaluation Form. Please save Initial Evaluation form. Redirecting...", "Adult Initial Evaluation", true, this, "window.location.replace('frmClinical_KNH_AdultIE.aspx');");
                return;

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
        public void ErrorLoad()
        {
            //string script = string.Empty;
            
            if (rdopatientcaregiver.SelectedItem != null)
            {
                if (rdopatientcaregiver.SelectedItem.Value == "1")
                {
                    visibleDiv("divcarrelationYN");
                }
            }
            if (rdoaddresschanged.SelectedItem != null)
            {
                if (rdoaddresschanged.SelectedItem.Value == "1")
                {
                    visibleDiv("hideaddchangeUpdateYN");
                    visibleDiv("divUpdatePhone");
                }
            }
            if (rdoHIVSupportGroup.SelectedItem != null)
            {
                if (rdoHIVSupportGroup.SelectedItem.Value == "1")
                {
                    visibleDiv("divHIVmembership");
                    
                }
            }

            for (int i = 0; i < idVitalSign.cblReferredTo.Items.Count; i++)
            {
                if (idVitalSign.cblReferredTo.Items[i].Text.ToLower() == "other specialist clinic")
                {
                    if (idVitalSign.cblReferredTo.Items[i].Selected)
                        visibleDiv("TriagedivReferToSpecialistClinic");
                }
                else if (idVitalSign.cblReferredTo.Items[i].Text.ToLower() == "other (specify)")
                {
                    if (idVitalSign.cblReferredTo.Items[i].Selected)
                        visibleDiv("TriagedivReferToOther");
                }
            }


            for (int i = 0; i < cblSpecificMedicalCondition.Items.Count; i++)
            {
                if (cblSpecificMedicalCondition.Items[i].Text.ToLower() == "other medical condition")
                {
                    if (cblSpecificMedicalCondition.Items[i].Selected)
                        visibleDiv("divothermedconditon");
                }
            }

            for (int i = 0; i < cblSurgicalConditions.Items.Count; i++)
            {
                if (cblSurgicalConditions.Items[i].Text.ToLower() == "current")
                {
                    if (cblSurgicalConditions.Items[i].Selected)
                        visibleDiv("divCurrentSurgicalConditionYN");
                }
                else if (cblSurgicalConditions.Items[i].Text.ToLower() == "previous")
                {
                    if (cblSurgicalConditions.Items[i].Selected)
                        visibleDiv("divPreviousSurgicalCondition");
                }
            }

                if (rdoHaveyoumissedanydoses.SelectedItem != null)
                {
                    if (rdoHaveyoumissedanydoses.SelectedValue == "1")
                    {
                        visibleDiv("divSpecifywhydosesmissed");
                    }
                }
            
                if (rdoARVsideeffectsyes.Checked == true)
                {
                    //visibleDiv("divshorttermeffects");
                    visibleDiv("trshorttermeffects");

                    for (int i = 0; i < cblShorttermeffects.Items.Count; i++)
                    {
                        
                        if (cblShorttermeffects.Items[i].Text.ToLower() == "other specify")
                        {
                            if (cblShorttermeffects.Items[i].Selected)
                                visibleDiv("divSpecifyothershorttermeffects");
                        }
                        
                    }

                    for (int i = 0; i < chklistlongtermeffect.Items.Count; i++)
                    {
                        if (chklistlongtermeffect.Items[i].Text.ToLower() == "other specify")
                        {
                            if (chklistlongtermeffect.Items[i].Selected)
                                visibleDiv("divSpecifyOtherlongtermeffects");
                        }
                    }
                }

                for (int i = 0; i < cblDiagnosis2.Items.Count; i++)
                {
                    if (cblDiagnosis2.Items[i].Text.ToLower() == "hiv-related illness")
                    {
                        if(cblDiagnosis2.Items[i].Selected)
                            visibleDiv("divHIVrelated");
                    }
                    else if (cblDiagnosis2.Items[i].Text.ToLower() == "non-hiv related illness")
                    {
                        if (cblDiagnosis2.Items[i].Selected)
                            visibleDiv("divNonHIVrelated");
                    }
                }

                if (UCWHO.rdoProgressionInWHOstage.SelectedValue == "1")
                    visibleDiv("divspecifyprog");

                if (UCWHO.radbtnMernarcheyes.Checked)
                    visibleDiv("divmenarchedatehshowhide");

            if (ddlSchoolingStatus.SelectedItem.Text == "Enrolled")
            {
                visibleDiv("divHighestLevelAttained");
            }
            if (ddlDisclosureStatus.SelectedItem.Text == "Not ready")
            {
                visibleDiv("divReasonNotDisclosed");
            }
            if (ddlDisclosureStatus.SelectedItem.Text == "Other specify")
            {
                visibleDiv("divOtherDisclosureReason");
            }

            //john
            for (int i = 0; i < this.UCPresComp.gvPresentingComplaints.Rows.Count; i++)
            {
                //Label lblPComplaintsId = (Label)UCPresComp.gvPresentingComplaints.Rows[i].FindControl("lblPresenting");
                CheckBox chkPComplaints = (CheckBox)UCPresComp.gvPresentingComplaints.Rows[i].FindControl("ChkPresenting");
                //TextBox txtPComplaints = (TextBox)UCPresComp.gvPresentingComplaints.Rows[i].FindControl("txtPresenting");
                
                if (chkPComplaints.Checked)
                {
                    if (chkPComplaints.Text.ToLower() == "other")
                    {
                        visibleDiv("DivOther");
                    }
                }
            }
            //if (rdoMedicalCondition.SelectedItem  != null)
            //{
            //    if (rdoMedicalCondition.SelectedValue == "1")
            //    {
            //        visibleDiv("divcblSpecificMedicalCondition");
            //    }
            //}
            if (rdoPreExistingMedicalConditionsFUP.SelectedItem != null)
            {
                if (rdoPreExistingMedicalConditionsFUP.SelectedValue == "1")
                {
                    visibleDiv("trAntihypertensives");
                    visibleDiv("trHypoglycemics");
                    visibleDiv("trothers");
                }
            }
            if (rdoPreviousAdmission.SelectedItem != null)
            {
                if (rdoPreviousAdmission.SelectedValue == "1")
                {
                    visibleDiv("divPreviousAdmissionDiagnosis");
                    visibleDiv("trPreviousAdmission");                    
                }
            }
            //if (ddlHAARTImpression.SelectedItem.Text == "HAART experienced")
            //{
            //    visibleDiv("divSpecifyHAART");
            //}
            //if (ddlHAARTImpression.SelectedItem.Text == "Other specify")
            //{
            //    visibleDiv("divSpecifyotherimpression");
            //}
            if (UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedItem.Text == "Start new treatment (naive patient)")
            {
                visibleDiv("divEligiblethrough");
                for (int i = 0; i < UserControlKNH_Pharmacy1.chklistEligiblethrough.Items.Count; i++)
                {
                    if (UserControlKNH_Pharmacy1.chklistEligiblethrough.Items[i].Text.ToLower() == "other")
                    {
                        if (UserControlKNH_Pharmacy1.chklistEligiblethrough.Items[i].Selected)
                            visibleDiv("divOtherEligibility");
                    }
                }
            }
            if (UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedItem.Text == "Change regimen")
            {
                visibleDiv("divARTchangecode");
            }
            if (UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedItem.Text == "Switch to second line")
            {
                visibleDiv("divReasonforswitchto2ndlineregimen");
            }
            if (UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedItem.Text == "Stop treatment")
            {
                visibleDiv("divARTstopcode");
            }
            if (ddlOIProphylaxis.SelectedItem.Text == "Cotrimoxazole")
            {
                visibleDiv("divCotrimoxazoleprescribedfor");
            }
            if (ddlOIProphylaxis.SelectedItem.Text == "Fluconazole")
            {
                visibleDiv("divFluconazoleshowhide");
            }
            if (ddlOIProphylaxis.SelectedItem.Text == "Other")
            {
                visibleDiv("divOtherOIPropholyxis");
            }
        }

        protected void tabControl_ActiveTabChanged(object sender, EventArgs e)
        {
            checkIfPreviuosTabSaved();
            ErrorLoad();

        }
        public void checkIfPreviuosTabSaved()
        {
            //KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            //DataSet ds;
            //if (tabControl.ActiveTabIndex == 1)
            //{
                DataSet dsTriage = new DataSet();
                dsTriage = KNHStatic.CheckIfPreviuosTabSaved("AdultFUTriage", Convert.ToInt32(Session["PatientVisitId"]));
                buttonEnabledAndDisabled(dsTriage, btnClinicalHistorySave, btnClinicalHistoryPrint);
            //}
            //if (tabControl.ActiveTabIndex == 2)
            //{
                DataSet dsClinical = new DataSet();
                dsClinical = KNHStatic.CheckIfPreviuosTabSaved("AdultFUClinicalHistory", Convert.ToInt32(Session["PatientVisitId"]));
                buttonEnabledAndDisabled(dsClinical, UcTBScreen.btnTBSave, UcTBScreen.btnTBPrint);
            //}
            //if (tabControl.ActiveTabIndex == 3)
            //{
                DataSet dsTB = new DataSet();
                dsTB = KNHStatic.CheckIfPreviuosTabSaved("AdultFUTBScreening", Convert.ToInt32(Session["PatientVisitId"]));
                buttonEnabledAndDisabled(dsTB, btnExaminationSave, btnExaminationPrint);
            //}

            //if (tabControl.ActiveTabIndex == 4)
            //{
                DataSet dsExam = new DataSet();
                dsExam = KNHStatic.CheckIfPreviuosTabSaved("AdultFUExamination", Convert.ToInt32(Session["PatientVisitId"]));
                buttonEnabledAndDisabled(dsExam, btnManagementSave, btnManagementPrint);
            //}

            //if (tabControl.ActiveTabIndex == 5)
            //{
                DataSet dsManage = new DataSet();
                dsManage = KNHStatic.CheckIfPreviuosTabSaved("AdultFUManagement", Convert.ToInt32(Session["PatientVisitId"]));
                buttonEnabledAndDisabled(dsManage, UcPwp.btnSave, UcPwp.btnPrintPositive);
            //}
            //ds = new DataSet();
            //ds = KNHStatic.CheckIfPreviuosTabSaved("PaedFUPwP", Convert.ToInt32(Session["PatientVisitId"]));
            //buttonEnabledAndDisabled(ds, UcPwp.btnSave,UcPwp.btnSubmitPositive, UcPwp.btnPrintPositive);
                dsTriage.Dispose();
                dsClinical.Dispose();
                dsTB.Dispose();
                dsExam.Dispose();
                dsExam.Dispose();
                dsManage.Dispose();

        }
        private void buttonEnabledAndDisabled(DataSet ds, Button btnSave, Button btnPrint)
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                btnSave.Enabled = false;
                //btnQuality.Enabled = false;
                btnPrint.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
                //btnQuality.Enabled = true;
                btnPrint.Enabled = true;
                securityPertab();

            }
        }
        public void securityPertab()
        {
            int feature = 0;
            if (Convert.ToString(Session["TechnicalAreaId"]) == "210")
                feature = 276;
            else
                feature = 175;
            IPatientKNHPEP KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            DataTable thePEPDS = KNHPEP.GetTabID(feature);
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable thedt = new DataTable();
            DataView theDV = new DataView(thePEPDS);
            theDV.RowFilter = "TabName='AdultFUTriage'";
            thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            AuthenticationManager Authentication = new AuthenticationManager();
            if (Convert.ToString(Session["TechnicalAreaId"]) == "210")
            {
                //triage
                if (thedt.Rows.Count > 0)
                {
                    Authentication.TabUserRights(btnTriagesave, btnTriagePrint, 276, Convert.ToInt32(thedt.Rows[0]["TabId"]));
                }

                //CA
                thedt = new DataTable();
                theDV = new DataView(thePEPDS);
                theDV.RowFilter = "TabName='AdultFUClinicalHistory'";
                thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                Button btnCH = new Button();
                Authentication.TabUserRights(btnClinicalHistorySave, btnClinicalHistoryPrint, 276, Convert.ToInt32(thedt.Rows[0]["TabId"]));

                //TB
                thedt = new DataTable();
                theDV = new DataView(thePEPDS);
                theDV.RowFilter = "TabName='AdultFUTBScreening'";
                thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                Authentication.TabUserRights(this.UcTBScreen.btnTBSave, this.UcTBScreen.btnTBPrint, 276, Convert.ToInt32(thedt.Rows[0]["TabId"]));

                //Exam
                thedt = new DataTable();
                theDV = new DataView(thePEPDS);
                theDV.RowFilter = "TabName='AdultFUExamination'";
                thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                Authentication.TabUserRights(btnExaminationSave, btnExaminationPrint, 276, Convert.ToInt32(thedt.Rows[0]["TabId"]));
                //Mgt
                thedt = new DataTable();
                theDV = new DataView(thePEPDS);
                theDV.RowFilter = "TabName='AdultFUManagement'";
                thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                Authentication.TabUserRights(this.btnManagementSave, this.btnManagementPrint, 276, Convert.ToInt32(thedt.Rows[0]["TabId"]));
                //PwP
                thedt = new DataTable();
                theDV = new DataView(thePEPDS);
                theDV.RowFilter = "TabName='AdultFUPwP'";
                thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                Authentication.TabUserRights(this.UcPwp.btnSave, this.UcPwp.btnPrintPositive, 276, Convert.ToInt32(thedt.Rows[0]["TabId"]));
            }
            else
            {
                //triage
                if (thedt.Rows.Count > 0)
                {
                    Authentication.TabUserRights(btnTriagesave, btnTriagePrint, 175, Convert.ToInt32(thedt.Rows[0]["TabId"]));
                }

                //CA
                thedt = new DataTable();
                theDV = new DataView(thePEPDS);
                theDV.RowFilter = "TabName='AdultFUClinicalHistory'";
                thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                Button btnCH = new Button();
                Authentication.TabUserRights(btnClinicalHistorySave, btnClinicalHistoryPrint, 175, Convert.ToInt32(thedt.Rows[0]["TabId"]));

                //TB
                thedt = new DataTable();
                theDV = new DataView(thePEPDS);
                theDV.RowFilter = "TabName='AdultFUTBScreening'";
                thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                Authentication.TabUserRights(this.UcTBScreen.btnTBSave, this.UcTBScreen.btnTBPrint, 175, Convert.ToInt32(thedt.Rows[0]["TabId"]));

                //Exam
                thedt = new DataTable();
                theDV = new DataView(thePEPDS);
                theDV.RowFilter = "TabName='AdultFUExamination'";
                thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                Authentication.TabUserRights(btnExaminationSave, btnExaminationPrint, 175, Convert.ToInt32(thedt.Rows[0]["TabId"]));
                //Mgt
                thedt = new DataTable();
                theDV = new DataView(thePEPDS);
                theDV.RowFilter = "TabName='AdultFUManagement'";
                thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                Authentication.TabUserRights(this.btnManagementSave, this.btnManagementPrint, 175, Convert.ToInt32(thedt.Rows[0]["TabId"]));
                //PwP
                thedt = new DataTable();
                theDV = new DataView(thePEPDS);
                theDV.RowFilter = "TabName='AdultFUPwP'";
                thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                Authentication.TabUserRights(this.UcPwp.btnSave, this.UcPwp.btnPrintPositive, 175, Convert.ToInt32(thedt.Rows[0]["TabId"]));
            }
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
                int delete = theUtils.DeleteForm("Adult Follow up", Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppUserId"]));

                if (delete == 0)
                {
                    IQCareMsgBox.Show("RemoveFormError", this);
                    return;
                }
                else
                {
                    IQCareMsgBox.NotifyAction("Form deleted successfully.", Header.InnerText, false, this, "window.location.href='frmPatient_Home.aspx';");
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "deleteSuccessful", "alert('Form deleted successfully.');", true);
                    //string theUrl;
                    //theUrl = string.Format("frmPatient_Home.aspx");
                    //Response.Redirect(theUrl);

                }
            }
        }
    }
}

