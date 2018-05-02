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
using System.IO;
using System.Drawing;
using PresentationApp.ClinicalForms.UserControl;


namespace PresentationApp.ClinicalForms
{
    
    public partial class frmClinical_KNH_PediatricFollowup : LogPage
    {
        IKNHPeadraticFollowup KNHPEDFWUP;
        IKNHStaticForms KNHStatic;
        IPatientKNHPEP KNHPEP;
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
            
            if (!IsPostBack)
            {
                startTime = String.Format("{0:dd-MMM-yyyy hh:mm:ss}", DateTime.Now);
                //startTime = String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now);
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    usePEFUForm();

                }
                tabControl.OnClientActiveTabChanged = "ValidateSave";
                hdnCurrentTabId.Value = tabControl.ActiveTab.ID;
                hdnPrevTabId.Value = tabControl.ActiveTab.ID;
                hdnCurrenTabName.Value = tabControl.ActiveTab.HeaderText;
                hdnPrevTabName.Value = tabControl.ActiveTab.HeaderText;
                ViewState["ActiveTabIndex"] = tabControl.ActiveTabIndex;
                hdnPrevTabIndex.Value = Convert.ToString(tabControl.ActiveTabIndex);
                hdnCurrenTabIndex.Value = Convert.ToString(tabControl.ActiveTabIndex);
                BindDropdown();
                BindChkboxlst();
                validate();
                //BindExistingData();
                //checkIfPreviuosTabSaved();
            }
            GblIQCare.FormId = 176;
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Paediatric Follow up";
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Paediatric Follow up";
            UcTBScreen.btnHandler += new UserControlKNH_TBScreening.OnButtonClick(tabControl_Handler);

            if (Request.QueryString["name"] == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showDeletebutton", "ShowHide('tblDeleteButton','show');", true);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "hideSavebutton", "ShowHide('tblSaveButton','hide');", true);
            }
        }
        void tabControl_Handler()
        {
            checkIfPreviuosTabSaved();
            tabControl.ActiveTabIndex = 3;
        }
        //Checking all the required values 
        private Boolean fieldValidation(string TabName)
        {
            IQCareUtils theUtil = new IQCareUtils();
            MsgBuilder totalMsgBuilder = new MsgBuilder();
            if ((Convert.ToInt32(Session["PatientVisitId"]) == 0) || (Session["PatientVisitId"] == null))  
            {
                KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
                DataSet dsValidate = KNHStatic.GetExistKNHStaticFormbydate(Convert.ToInt32(Session["PatientId"]), txtvisitDate.Value.ToString(), Convert.ToInt32(Session["AppLocationId"]), 24);

                if (dsValidate.Tables[0].Rows.Count > 0)
                {
                    totalMsgBuilder.DataElements["FormName"] = "Paediatric Follow up Form";
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
                if (rdopatientcaregiver.SelectedItem==null)
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
                //for (int i = 0; i < this.UcPhysExam.cblGeneralConditions.Items.Count; i++)
                //{
                //    if (this.UCPresComp.gvPresentingComplaints.Items[i].Selected == true)
                //    {
                //        count++;
                //    }
                //}
                //if (count == 0)
                //{
                //    totalMsgBuilder.DataElements["MessageText"] = "Select General Condition";
                //    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValGC", "alert('Select General Condition')", true);
                //    return false;
                //}
            }

            else if (TabName == "Examination" || tabControl.ActiveTabIndex == 3)
            {
                for (int i = 0; i < chkLongTermMedication.Items.Count; i++)
                {
                    if (chkLongTermMedication.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Long Term Medication";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    lblchlongtermmed.ForeColor = Color.Red;
                    lblMedicalConditions.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValLTM", "alert('Select Long Term Medication')", true);
                    return false;
                }
                else
                {
                    lblchlongtermmed.ForeColor = Color.FromArgb(0, 0, 142);
                    lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPhysExam.cblGeneralConditions.Items.Count; i++)
                {
                    if (this.UcPhysExam.cblGeneralConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select General Condition";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysExam.lblGeneral.ForeColor = Color.Red;
                    lblMedicalConditions.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValGC", "alert('Select General Condition')", true);
                    return false;
                }
                else
                {
                    UcPhysExam.lblGeneral.ForeColor = Color.FromArgb(0, 0, 142);
                    lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPhysExam.cblCardiovascularConditions.Items.Count; i++)
                {
                    if (this.UcPhysExam.cblCardiovascularConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Cardiovascular Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysExam.lblCardiovarscular.ForeColor = Color.Red;
                    lblMedicalConditions.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCC", "alert('Select Cardiovascular Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysExam.lblCardiovarscular.ForeColor = Color.FromArgb(0, 0, 142);
                    lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPhysExam.cblOralCavityConditions.Items.Count; i++)
                {
                    if (this.UcPhysExam.cblOralCavityConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Oral Cavity Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysExam.lblOralCavity.ForeColor = Color.Red;
                    lblMedicalConditions.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValOC", "alert('Select Oral Cavity Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysExam.lblOralCavity.ForeColor = Color.FromArgb(0, 0, 142);
                    lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
                }
                count = 0;
                for (int i = 0; i < this.UcPhysExam.cblGenitalUrinaryConditions.Items.Count; i++)
                {
                    if (this.UcPhysExam.cblGenitalUrinaryConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select GenitalUrinary Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysExam.lblGenitourinary.ForeColor = Color.Red;
                    lblMedicalConditions.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValGU", "alert('Select GenitalUrinary Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysExam.lblGenitourinary.ForeColor = Color.FromArgb(0, 0, 142);
                    lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
                }

                count = 0;
                for (int i = 0; i < this.UcPhysExam.cblCNSConditions.Items.Count; i++)
                {
                    if (this.UcPhysExam.cblCNSConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select CNS Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysExam.lblCNS.ForeColor = Color.Red;
                    lblMedicalConditions.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValCNS", "alert('Select CNS Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysExam.lblCNS.ForeColor = Color.FromArgb(0, 0, 142);
                    lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
                }

                count = 0;
                for (int i = 0; i < this.UcPhysExam.cblChestLungsConditions.Items.Count; i++)
                {
                    if (this.UcPhysExam.cblChestLungsConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select ChestLung Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysExam.lblChest.ForeColor = Color.Red;
                    lblMedicalConditions.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValChest", "alert('Select ChestLung Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysExam.lblChest.ForeColor = Color.FromArgb(0, 0, 142);
                    lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
                }

                count = 0;
                for (int i = 0; i < this.UcPhysExam.cblSkinConditions.Items.Count; i++)
                {
                    if (this.UcPhysExam.cblSkinConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Skin Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysExam.lblSkin.ForeColor = Color.Red;
                    lblMedicalConditions.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValSkin", "alert('Select Skin Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysExam.lblSkin.ForeColor = Color.FromArgb(0, 0, 142);
                    lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
                }

                count = 0;
                for (int i = 0; i < this.UcPhysExam.cblAbdomenConditions.Items.Count; i++)
                {
                    if (this.UcPhysExam.cblAbdomenConditions.Items[i].Selected == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Abdomen Conditions";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UcPhysExam.lblAbdomen.ForeColor = Color.Red;
                    lblMedicalConditions.ForeColor = Color.Red;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ValAbdomen", "alert('Select Abdomen Conditions')", true);
                    return false;
                }
                else
                {
                    UcPhysExam.lblAbdomen.ForeColor = Color.FromArgb(0, 0, 142);
                    lblMedicalConditions.ForeColor = Color.FromArgb(0, 0, 142);
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
                
                if (this.UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedIndex==0)
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
                
            }            
            
            return true;

        }
        public void FillCheckboxlist(CheckBoxList chk, DataTable thedt, string name)
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable dt = new DataTable();
            string script = string.Empty;
            if (thedt.Rows.Count > 0)
            {
                DataView theDV = new DataView(thedt);
                theDV.RowFilter = "FieldName='" + name + "'";
                dt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);

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
                                    script += "ShowHide('divOtherChronicCondition','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divothercondition", script);
                                }
                            }
                            if (name == "TBSideEffects")
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
                            if (name == "DrugAllergiesToxicitiesPaeds")
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
                            if (name == "LongTermEffects")
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
                            if (name == "ShortTermEffects")
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
                            
                            if (name == "Counselling")
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
                            if (name == "RefferedToFUpF")
                            {
                                if (chk.Items[j].Text.ToLower() == "other specialist clinic")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'TriagedivReferToSpecialistClinicyesno'>\n";
                                    script += "ShowHide('TriagedivReferToSpecialistClinic','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("TriagedivReferToSpecialistClinicyesno", script);
                                }
                                if (chk.Items[j].Text.ToLower() == "other (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'TriagedivReferToOtheryesno'>\n";
                                    script += "ShowHide('TriagedivReferToOther','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("TriagedivReferToOtheryesno", script);
                                }
                            }
                        }
                    }
                }

            }
        }
        public void Save(int dqchk)
        {
            int tabindex = 0;
            string savetabname = tabControl.ActiveTab.HeaderText.ToString();
            if (fieldValidation(savetabname) == false)
            {
                ErrorLoad();
                return;
            }
            Hashtable theHT = new Hashtable();
            DataTable dtmuiltselect = CreateTempTable();
            string tabname = string.Empty;
            DataSet DsReturns = new DataSet();
            KNHPEDFWUP = (IKNHPeadraticFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHPeadraticFollowup, BusinessProcess.Clinical");
            if (savetabname == "Triage" || tabControl.ActiveTabIndex == 0)
            {
                tabname = "Triage";
                dtmuiltselect = GetCheckBoxListValues(this.idVitalSign.cblReferredTo, dtmuiltselect, "RefferedToFUpF");
                theHT = HT(dqchk, tabname);
                DsReturns = KNHPEDFWUP.SaveUpdateKNHPeadraticFollowupData_TriageTab(theHT, dtmuiltselect, dqchk, 0, Convert.ToInt32(Session["AppUserId"]));
                
                tabindex = 1;

            }
            else if (savetabname == "Clinical History" || tabControl.ActiveTabIndex == 1)
            {
                tabname = "Clinical History";
                dtmuiltselect = GetCheckBoxListValues(cblChronicCondition, dtmuiltselect, "ChronicCondition");
                dtmuiltselect = PresentingComplaints(dtmuiltselect, "PresentingComplaints");
                theHT = HT(dqchk, tabname);
                DsReturns = KNHPEDFWUP.SaveUpdateKNHPeadraticFollowupData_CATab(theHT, dtmuiltselect, dqchk, 0, Convert.ToInt32(Session["AppUserId"]));
                
                tabindex = 2;
            }
            
            else if (savetabname == "Examination" || tabControl.ActiveTabIndex == 3)
            {
                tabname = "Examination";
                dtmuiltselect = GetCheckBoxListValues(chkLongTermMedication, dtmuiltselect, "PaedCurrentLongTermMedications"); 
                dtmuiltselect = GetCheckBoxListValues(UcPhysExam.cblGeneralConditions, dtmuiltselect, "GeneralConditions");
                //Cardiovascular conditions
                dtmuiltselect = GetCheckBoxListValues(UcPhysExam.cblCardiovascularConditions, dtmuiltselect, "CardiovascularConditions");
                //CNS 
                dtmuiltselect = GetCheckBoxListValues(UcPhysExam.cblCNSConditions, dtmuiltselect, "CNSConditions");
                //Oral cavity
                dtmuiltselect = GetCheckBoxListValues(UcPhysExam.cblOralCavityConditions, dtmuiltselect, "OralCavityConditions");
                //Chest Lungs
                dtmuiltselect = GetCheckBoxListValues(UcPhysExam.cblChestLungsConditions, dtmuiltselect, "ChestLungsConditions");
                //Genitourinary 
                dtmuiltselect = GetCheckBoxListValues(UcPhysExam.cblGenitalUrinaryConditions, dtmuiltselect, "GenitalUrinaryConditions");
                //Skin 
                dtmuiltselect = GetCheckBoxListValues(UcPhysExam.cblSkinConditions, dtmuiltselect, "SkinConditions");
                //Abdomen conditions
                dtmuiltselect = GetCheckBoxListValues(UcPhysExam.cblAbdomenConditions, dtmuiltselect, "AbdomenConditions");
                //WHO Stage I
                //GridView gdviewwho1 = (GridView)UcWhostaging.FindControl("gvWHO1");
                dtmuiltselect = InsertMultiSelectList(UCWHO.gvWHO1, dtmuiltselect, "CurrentWHOStageIConditions");
                //// WHO Stage II
                dtmuiltselect = InsertMultiSelectList(UCWHO.gvWHO2, dtmuiltselect, "CurrentWHOStageIIConditions");
                //// WHO Stage III
                dtmuiltselect = InsertMultiSelectList(UCWHO.gvWHO3, dtmuiltselect, "CurrentWHOStageIIIConditions");
                //// WHO Stage IV
                dtmuiltselect = InsertMultiSelectList(UCWHO.gvWHO4, dtmuiltselect, "CurrentWHOStageIVConditions");
                ///diagnosis
                dtmuiltselect = GetCheckBoxListValues(cblDiagnosis2, dtmuiltselect, "Diagnosis");
                theHT = HT(dqchk, tabname);
                DsReturns = KNHPEDFWUP.SaveUpdateKNHPeadraticFollowupData_ExamTab(theHT, dtmuiltselect, dqchk, 0, Convert.ToInt32(Session["AppUserId"]));
                tabindex = 4;

            }
            else if (savetabname == "Management" || tabControl.ActiveTabIndex == 4)
            {
                tabname = "Management";
                dtmuiltselect = GetCheckBoxListValues(UserControlKNH_Pharmacy1.chklistARTchangecode, dtmuiltselect, "ARTchangecode");
                dtmuiltselect = GetCheckBoxListValues(UserControlKNH_Pharmacy1.chklistEligiblethrough, dtmuiltselect, "ARTEligibility");
                dtmuiltselect = GetCheckBoxListValues(UserControlKNH_Pharmacy1.chklistARTstopcode, dtmuiltselect, "ARTstopcode");
                dtmuiltselect = GetCheckBoxListValues(cblShorttermeffects, dtmuiltselect, "ShortTermEffects");
                dtmuiltselect = GetCheckBoxListValues(chklistlongtermeffect, dtmuiltselect, "LongTermEffects");
                //dtmuiltselect = GetCheckBoxListValues(cblDiagnosis2, dtmuiltselect, "Diagnosis");
                //dtmuiltselect = GetCheckBoxListValues(cblSpecifyLabEvaluation, dtmuiltselect, "SpecifyLabEvaluation");
                //dtmuiltselect = GetCheckBoxListValues(cblCounselling, dtmuiltselect, "Counselling");
                theHT = HT(dqchk, tabname);
                DsReturns = KNHPEDFWUP.SaveUpdateKNHPeadraticFollowupData_MgtTab(theHT, dtmuiltselect, dqchk, 0, Convert.ToInt32(Session["AppUserId"]));
                tabindex = 5;
            }
                        
            Session["Redirect"] = "0";
            if (Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]) > 0)
            {
                Session["PatientVisitId"] = Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]);
                SaveCancel(savetabname);
                //BindExistingData();
                checkIfPreviuosTabSaved();
                tabControl.ActiveTabIndex = tabindex;
            }
        }
        private DataTable PresentingComplaints(DataTable dtprescompl, string name)
        {

            GridView gdview = (GridView)UCPresComp.FindControl("gvPresentingComplaints");
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
                    if (txt.Text != "")
                    {
                        dr["Other_Notes"] = txt.Text;
                    }
                    dtprescompl.Rows.Add(dr);
                }
                
                
            }
            return dtprescompl;
            
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            Save(0);
        }
        public void usePEFUForm()
        {
            DataTable FURuleFormRules = new DataTable();
            IQCareUtils theUtils = new IQCareUtils();
            KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            FURuleFormRules = KNHStatic.GetPatientFeatures(Convert.ToInt32(Session["PatientId"]));
            DataView theCodeDV = new DataView(FURuleFormRules);
            theCodeDV.RowFilter = "VisitType=22";
            DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
            if (theDT.Rows.Count == 0)
            {
                string script = "alert('Paediatric Follow up Form cannot be saved before Paediatric Initial Evaluation Form. Please save Initial Evaluation form. Redirecting...');";
                script += "window.location.replace('frmClinical_KNH_Paediatric_IE.aspx');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "2FirstVisits", script, true);
                return;

            }

           


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
        private void SaveCancel(string tabname)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            IQCareMsgBox.NotifyAction(tabname + " Tab saved successfully.", "KNH Paediatric FollowUp", true, this, "");
            //MsgBuilder totalMsgBuilder = new MsgBuilder();
            //totalMsgBuilder.DataElements["MessageText"] = tabname + " Tab saved successfully.";
            //IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
            
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
        protected void btncomplete_Click(object sender, EventArgs e)
        {
            //System.IO.StreamReader myFile =
            //    new System.IO.StreamReader("C:\\Users\\naveen\\Documents\\Read.txt");
            //string myString = "";
            //string[] splitmyString;
            //TextWriter tw = new StreamWriter("C:\\Users\\naveen\\Documents\\Write.txt");
            ////myString = myFile.ReadToEnd();
            ////tw.WriteLine("insert into dtl_peadi (" + myString.Replace("@", "") + ") ");
            ////tw.WriteLine("VALUES (" + myString.Replace("[", "").Replace("]", "") + ") ");
            //do
            //{

            //    myString = myFile.ReadLine() + "\r\n";
            //    splitmyString = myString.Split(',');
            //    if (splitmyString.Length > 1)
            //    {
            //        for (int i = 0; splitmyString.Length > i; i++)
            //        {
            //            //string str = splitmyString[0];
            //            //string[] splitsp = splitmyString[2].Split('.');
            //            //string[] spldatatype = splitsp[1].Split(',');
            //            //string[] splitstr = str.Split(' ');
            //            tw.WriteLine(splitmyString[i] + "= @" + splitmyString[i].Replace("[", "").Replace("]", "") + ", ");
            //            //if (spldatatype[0] == "Int")
            //            //    tw.Write(splitmyString[1] + " int=null, ");
            //            //else if (spldatatype[0] == "DateTime")
            //            //    tw.Write(splitmyString[1] + " datetime=null,");
            //            //else
            //            //    tw.Write(splitmyString[1] + " varchar(1000)=null,");
            //            //tw.Write("["+ splitmyString[1] + "], ");
            //            //tw.WriteLine("ClsUtility.AddParameters('@"+ splitmyString[1]+ "', SqlDbType.Int, hashTable['"+ splitmyString[1] +"'].ToString());");
            //            //tw.WriteLine( splitstr[0].Replace("@","") + "= " + splitstr[0] + ", ");
            //        }
            //    }

            //} while (myFile.Peek() != -1);
            
            //// write a line of text to the file
            
            //myFile.Close();
            

            //// close the stream
            //tw.Close();
            //// Display the file contents.
            ////Console.WriteLine(myString);
            //// Suspend the screen.
            ////Console.ReadLine();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        private void BindDropdown()
        {
            UCWHO.gvWHO1.Columns[2].Visible = false;
            UCWHO.gvWHO2.Columns[2].Visible = false;
            UCWHO.gvWHO3.Columns[2].Visible = false;
            UCWHO.gvWHO4.Columns[2].Visible = false;            
            BindDropdown(ddlSchoolingStatus, "SchoolingStatus");
            ddlSchoolingStatus.Attributes.Add("OnChange", "getSelectedValue('divHighestLevelAttained','" + ddlSchoolingStatus.ClientID + "','Enrolled')");
            BindDropdown(ddlHighestLevelAttained, "HighestLevelAttained");
            BindDropdown(ddlDisclosureStatus, "DisclosureStatus");
            ddlDisclosureStatus.Attributes.Add("OnChange", "getSelectedValue('divReasonNotDisclosed','" + ddlDisclosureStatus.ClientID + "','Not ready');getSelectedValue('divOtherDisclosureReason','" + ddlDisclosureStatus.ClientID + "','Other specify')");
            
            BindDropdown(ddlImmunisationStatus, "ImmunisationStatus");
            BindDropdown(ddlHAARTImpression, "HAARTImpression");
            ddlHAARTImpression.Attributes.Add("OnChange", "getSelectedValue('divSpecifyHAART','" + ddlHAARTImpression.ClientID + "','HAART experienced');getSelectedValue('divSpecifyotherimpression','" + ddlHAARTImpression.ClientID + "','Other specify')");
            BindDropdown(ddlHAARTexperienced, "HAARTexperienced");            
            BindDropdown(ddlSchoolPerfomance, "SchoolPerfomance");
            BindDropdown(ddlcaregiverrelationship, "TreatmentSupporterRelationship");
            BindDropdown(ddlCotrimoxazoleprescribed, "ReasonCTXpresribed");
            BindDropdown(ddlfluconazole, "FluconazolePrescribedFor");
            BindDropdown(ddlOIProphylaxis, "OIProphylaxis");
            ddlOIProphylaxis.Attributes.Add("OnChange", "getSelectedValue('divCotrimoxazoleprescribedfor','" + ddlOIProphylaxis.ClientID + "','Cotrimoxazole');getSelectedValue('divFluconazoleshowhide','" + ddlOIProphylaxis.ClientID + "','Fluconazole');getSelectedValue('divOtherOIPropholyxis','" + ddlOIProphylaxis.ClientID + "','Other')");
                    

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
            string script = string.Empty;
            
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
                    visibleDiv("divUpdated_phone");
                }
            }
            if (rdofatheraliveno.Checked)
            {
                visibleDiv("divDateOfDeathFather");
            }
            if (rdomotheraliveno.Checked)
            {
                visibleDiv("divDateOfDeathMother");
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

            for (int i = 0; i < cblChronicCondition.Items.Count; i++)
            {
                if (cblChronicCondition.Items[i].Text.ToLower() == "other specify")
                {
                    if (cblChronicCondition.Items[i].Selected)
                        visibleDiv("divOtherChronicCondition");
                }
            }

            
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

            for (int i = 0; i < chkLongTermMedication.Items.Count; i++)
            {
                if (chkLongTermMedication.Items[i].Text.ToLower() == "other")
                {
                    if (chkLongTermMedication.Items[i].Selected)
                        visibleDiv("divLongTermMedication");
                }
            }

            if (rdoMilestoneAppropriateNo.Checked)
                visibleDiv("divResonMilestoneInappropriate");

            if (UCWHO.rdoProgressionInWHOstage.SelectedValue == "1")
                visibleDiv("divspecifyprog");

            if (UCWHO.radbtnMernarcheyes.Checked)
                visibleDiv("divmenarchedatehshowhide");

            for (int i = 0; i < cblDiagnosis2.Items.Count; i++)
            {
                if (cblDiagnosis2.Items[i].Text.ToLower() == "hiv-related illness")
                {
                    if (cblDiagnosis2.Items[i].Selected)
                        visibleDiv("divSpecifyHIVrelatedOI");
                }
                else if (cblDiagnosis2.Items[i].Text.ToLower() == "non-hiv related illness")
                {
                    if (cblDiagnosis2.Items[i].Selected)
                        visibleDiv("divNonHIVRelatedOI");
                }
            }

                if (rdoTBHistory.SelectedValue == "1")
                {
                    visibleDiv("divTBrxCompleteDate");
                    visibleDiv("divTBRetreatmentDate");
                }
            
            if (rdoMilestoneAppropriateNo.Checked)
            {
                visibleDiv("divResonMilestoneInappropriate");
                
            }
            if (rdoHaveyoumissedanydoses.SelectedItem != null)
            {
                if (rdoHaveyoumissedanydoses.SelectedValue == "1")
                {
                    visibleDiv("divSpecifywhydosesmissed");
                }
            }
            if (rdoARVsideeffects.SelectedItem != null)
            {
                if (rdoARVsideeffects.SelectedValue == "1")
                {
                    visibleDiv("divshorttermeffects");
                    visibleDiv("divLongtermeffects");
                }
            }


            if (ddlSchoolingStatus.SelectedItem.Text=="Enrolled")
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

            if (ddlHAARTImpression.SelectedItem.Text == "HAART experienced")
            {
                visibleDiv("divSpecifyHAART");
            }
            if (ddlHAARTImpression.SelectedItem.Text == "Other specify")
            {
                visibleDiv("divSpecifyotherimpression");
            }
            if (UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedItem.Text == "Start new treatment (naive patient)")
            {
                visibleDiv("divEligiblethrough");
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
                {
                    txtvisitDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    BindAutopopulatingData();
                }
            }
            checkIfPreviuosTabSaved();
            showHideOnLoad();
            if (idVitalSign.txtHeight.Text != "" && idVitalSign.txtWeight.Text != "")
                idVitalSign.calculateZScores();

            //john
            KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            DataTable dtSignature = KNHPEP.GetSignature(176, Convert.ToInt32(Session["PatientVisitId"]));
            foreach (DataRow dr in dtSignature.Rows)
            {
                if (dr["TabName"].ToString() == "PaedFUTriage")
                    this.UserControlKNH_SignatureTriage.lblSignature.Text = dr["UserName"].ToString();
                if (dr["TabName"].ToString() == "PaedFUClinicalHistory")
                    this.UserControlKNH_SignatureClinical.lblSignature.Text = dr["UserName"].ToString();
                if (dr["TabName"].ToString() == "PaedFUExamination")
                    this.UserControlKNH_SignatureExam.lblSignature.Text = dr["UserName"].ToString();
                if (dr["TabName"].ToString() == "PaedFUManagement")
                    this.UserControlKNH_SignatureMgt.lblSignature.Text = dr["UserName"].ToString();
            }
            ErrorLoad();
            //
       
            checkIfPreviuosTabSaved();
            
            
        }

        private void showHideOnLoad()
        {
            foreach (ListItem item in cblShorttermeffects.Items)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSTEffects", "CheckBoxToggleShowHidePE('" + cblShorttermeffects.ClientID + "','divSpecifyothershorttermeffects','Other Specify','" + txtSpecifyothershorttermeffects.ClientID + "');", true);
            }

            foreach (ListItem item in chklistlongtermeffect.Items)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showLTEffects", "CheckBoxToggleShowHidePE('" + chklistlongtermeffect.ClientID + "','divSpecifyOtherlongtermeffects','Other specify','" + txtlistlongtermeffect.ClientID + "');", true);
            }
        }
        public void BindChkboxlst()
        {
            BindChkboxlstControl(cblShorttermeffects, "ShortTermEffects");
            cblShorttermeffects.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblShorttermeffects.ClientID + "','divSpecifyothershorttermeffects','Other Specify','" + txtSpecifyothershorttermeffects.ClientID + "')");
            BindChkboxlstControl(chklistlongtermeffect, "LongTermEffects");
            chklistlongtermeffect.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + chklistlongtermeffect.ClientID + "','divSpecifyOtherlongtermeffects','Other specify','" + txtlistlongtermeffect.ClientID + "')");
            
            BindChkboxlstControl(cblChronicCondition, "ChronicCondition");
            cblChronicCondition.Attributes.Add("OnClick", "CheckBoxToggleShowHide('" + cblChronicCondition.ClientID + "','divOtherChronicCondition','Other specify')");
            //BindChkboxlstControl(cblCounselling, "Counselling");
            //cblCounselling.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblCounselling.ClientID + "','divOtherCounselling','Other')");
            BindChkboxlstControl(cblDiagnosis2, "Diagnosis");
            cblDiagnosis2.Attributes.Add("OnClick", "CheckBoxToggleShowHide('" + cblDiagnosis2.ClientID + "','divSpecifyHIVrelatedOI','HIV-Related illness');CheckBoxToggleShowHide('" + cblDiagnosis2.ClientID + "','divNonHIVRelatedOI','Non-HIV related illness')");
            BindChkboxlstControl(chkLongTermMedication, "PaedCurrentLongTermMedications");
            chkLongTermMedication.Attributes.Add("OnClick", "CheckBoxToggleShowHide('" + chkLongTermMedication.ClientID + "','divLongTermMedication','Other')");
            
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
        }
        protected Hashtable HT(int qltyFlag,string tabname)
        {
            dtmuiltselect = CreateTempTable();
            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Session["PatientVisitId"]);
                theHT.Add("locationID", Session["AppLocationId"]);
                // Visit date:
                theHT.Add("visitDate", txtvisitDate.Value);
                //Start Time 
                if(startTime != string.Empty)
                    theHT.Add("starttime", startTime);
                else
                    theHT.Add("starttime", String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now));
                //LMP
                //theHT.Add("LMP", txtdtLMP.Value);
                //Patient accompanied by caregiver
                if (tabname.ToString() == "Triage")
                {
                    theHT.Add("ChildAccompaniedByCaregiver", rdopatientcaregiver.SelectedValue);
                    theHT.Add("TreatmentSupporterRelationship", Convert.ToInt32(ddlcaregiverrelationship.SelectedValue));
                    theHT.Add("AddressChanged", rdoaddresschanged.SelectedValue);
                    theHT.Add("AddressChange", txtAddresschange.Text);
                    theHT.Add("PhoneNoChange", txtUpdated_phone.Text);
                    theHT.Add("PrimaryCareGiver", txtPrimaryCareGiver.Text);
                    theHT.Add("DisclosureStatus", Convert.ToInt32(ddlDisclosureStatus.SelectedValue));
                    theHT.Add("ReasonNotDisclosed", txtReasonNotDisclosed.Text);
                    theHT.Add("OtherDisclosureReason", txtOtherDisclosureReason.Text);
                    theHT.Add("HighestLevelAttained", Convert.ToInt32(ddlHighestLevelAttained.SelectedValue));
                    theHT.Add("SchoolingStatus", Convert.ToInt32(ddlSchoolingStatus.SelectedValue));
                    theHT.Add("HIVSupportGroupMembership", txtHIVSupportGroupMembership.Text);
                    theHT.Add("HealthEducation", rdoHealthEducation.SelectedValue);
                    int fatheralive = rdofatheraliveyes.Checked ? 1 : rdofatheraliveno.Checked ? 0 : 9;
                    theHT.Add("FatherAlive2", fatheralive);
                    theHT.Add("DateOfDeathFather", txtDateOfDeathFather.Value);
                    int motheralive = rdomotheraliveyes.Checked ? 1 : rdomotheraliveno.Checked ? 0 : 9;
                    theHT.Add("MotherAlive2", motheralive);
                    theHT.Add("HIVSupportGroup", rdoHIVSupportGroup.SelectedValue);
                    theHT.Add("DateOfDeathMother", txtDateOfDeathMother.Value);
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
                    
                    theHT.Add("WeightForAge", this.idVitalSign.lblWA.Text);

                    
                    theHT.Add("WeightForHeight", this.idVitalSign.lblWH.Text);
                    theHT.Add("BMIz",this.idVitalSign.lblBMIz.Text);
                   
                    theHT.Add("NursesComments", this.idVitalSign.txtnursescomments.Text);
                    #endregion
                }
                if (tabname.ToString() == "Clinical History")
                {
                    //////////Medical History (Disease, diagnosis& treatment)//////////////
                    theHT.Add("MedicalHistory", rdoMedicalHistory.SelectedValue);
                    theHT.Add("OtherMedicalHistorySpecify", txtOtherMedicalHistorySpecify.Text);
                    theHT.Add("OtherChronicCondition", txtOtherChronicCondition.Text);

                    //////////Additional Presenting Complaints//////////////
                    //TextBox txtAdditionPresentingComplaints = (TextBox)UCPresComp.FindControl("txtAdditionPresentingComplaints");
                    theHT.Add("PresentingComplaintsAdditionalNotes", UCPresComp.txtAdditionalComplaints.Text);
                    theHT.Add("SchoolPerfomance", Convert.ToInt32(ddlSchoolPerfomance.SelectedItem.Value));
                    //////////Immunisation Status//////////////
                    theHT.Add("ImmunisationStatus", Convert.ToInt32(ddlImmunisationStatus.SelectedItem.Value));

                    //////////TB History//////////////
                    theHT.Add("TBHistory", rdoTBHistory.SelectedValue);
                    theHT.Add("TBrxCompleteDate", txtTBrxCompleteDate.Value);
                    theHT.Add("TBRetreatmentDate", txtTBRetreatmentDate.Value);

                    #region "Presenting Complaints"
                    //GridView gdview = (GridView)UCPresComp.FindControl("gvPresentingComplaints");
                    //DataTable dtprescompl = CreateTempTable();
                    //foreach (GridViewRow row in gdview.Rows)
                    //{
                    //    DataRow dr = dtprescompl.NewRow();
                    //    CheckBox chk = (CheckBox)row.FindControl("ChkPresenting");
                    //    TextBox txt = (TextBox)row.FindControl("txtPresenting");
                    //    Label lbl = (Label)row.FindControl("lblPresenting");
                    //    if (chk.Checked)
                    //    {
                    //        dr["ID"] = Convert.ToInt32(lbl.Text);
                    //        if (txt.Text != "")
                    //        {
                    //            dr["OtherNotes"] = txt.Text;
                    //        }

                    //    }                        
                    //    if (dr != null)
                    //    {
                    //        dr["FieldName"] = "PresentingComplaints";
                    //        dtprescompl.Rows.Add(dr);
                    //    }

                    //}
                    //dtmuiltselect.Merge(dtprescompl);
                    #endregion
                }

                
                if (tabname.ToString() == "Examination")
                {
                    theHT.Add("OtherCurrentLongTermMedications", txOtherLongTermMedications.Text);
                    //////////Medical Conditions//////////////
                    int MilestoneAppropriate = rdoMilestoneAppropriateYes.Checked ? 1 : rdoMilestoneAppropriateNo.Checked ? 0 : 9;
                    theHT.Add("MilestoneAppropriate", MilestoneAppropriate);
                    theHT.Add("ResonMilestoneInappropriate", txtResonMilestoneInappropriate.Text);
                    //////MultiSelect////
                    theHT.Add("OtherGeneralConditions", UcPhysExam.txtOtherGeneralConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherAbdomenConditions", UcPhysExam.txtOtherAbdomenConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherCardiovascularConditions", UcPhysExam.txtOtherCardiovascularConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherOralCavityConditions", UcPhysExam.txtOtherOralCavityConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherGenitourinaryConditions", UcPhysExam.txtOtherGenitourinaryConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherCNSConditions", UcPhysExam.txtOtherCNSConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherChestLungsConditions", UcPhysExam.txtOtherChestLungsConditions.Text);
                    //////MultiSelect////
                    theHT.Add("OtherSkinConditions", UcPhysExam.txtOtherSkinConditions.Text);
                    theHT.Add("OtherMedicalConditionNotes", UcPhysExam.txtOtherMedicalConditionNotes.Text);

                    #region "WHO Staging"                    
                    theHT.Add("WABStage", Convert.ToInt32(UCWHO.ddlWABStage.SelectedValue));
                    //////////Progression in WHO//////////////
                    theHT.Add("ProgressionInWHOstage", UCWHO.rdoProgressionInWHOstage.SelectedValue);
                    theHT.Add("SpecifyWHOprogression", UCWHO.txtSpecifyWHOprogression.Text);
                    theHT.Add("CurrentWHOStage", UCWHO.ddlwhostage1.SelectedValue);
                    int Menarche = UCWHO.radbtnMernarcheyes.Checked ? 1 : UCWHO.radbtnMernarcheno.Checked ? 0 : 9;
                    theHT.Add("Menarche", Convert.ToInt32(Menarche));
                    theHT.Add("MenarcheDate", UCWHO.txtmenarchedate.Value);
                    theHT.Add("TannerStaging", Convert.ToInt32(UCWHO.ddltannerstaging.SelectedValue));

                    #endregion
                    theHT.Add("Impression", Convert.ToInt32(ddlHAARTImpression.SelectedValue));
                    theHT.Add("OtherImpression", txtOtherHAARTImpression.Text);
                    theHT.Add("reviewprevresult", rdoReviewedPreviousResults.SelectedValue);
                    theHT.Add("additonalinformation", txtResultsReviewComments.Text);
                    //////MultiSelect////cblDiagnosis2
                    theHT.Add("HIVRelatedOI", txtHIVRelatedOI.Text);
                    theHT.Add("NonHIVRelatedOI", txtNonHIVRelatedOI.Text);
                    
                }
                if (tabname.ToString() == "Management")
                {
                    //////////Adherence Assessment//////////////
                    theHT.Add("MissedDosesFUP", rdoHaveyoumissedanydoses.SelectedValue);
                    theHT.Add("MissedDosesFUPspecify", txtSpecifywhydosesmissed.Text);
                    theHT.Add("DelaysInTakingMedication", rdohavedelayed.SelectedValue);

                    theHT.Add("OIProphylaxis", Convert.ToInt32(ddlOIProphylaxis.SelectedValue));
                    theHT.Add("ReasonCTXpresribed", Convert.ToInt32(ddlCotrimoxazoleprescribed.SelectedValue));
                    theHT.Add("OtherOIProphylaxis", txtOtherOIPropholyxis.Text);
                    theHT.Add("OtherTreatment", txtOthertreatment.Text);                    
                    ///////////////Drug Allergy and Toxicities///////////////////////
                    theHT.Add("ARTTreatmentPlan", Convert.ToInt32(UserControlKNH_Pharmacy1.ddlTreatmentplan.SelectedValue));
                    theHT.Add("OtherEligiblethorugh", UserControlKNH_Pharmacy1.txtSpecifyOtherEligibility.Text);
                    theHT.Add("OtherARTStopCode", UserControlKNH_Pharmacy1.txtSpecifyOtherStopCode.Text);
                    theHT.Add("NumberDrugsSubstituted", UserControlKNH_Pharmacy1.txtNoofdrugssubstituted.Text);
                    theHT.Add("SpecifyotherARTchangereason", UserControlKNH_Pharmacy1.txtSpecifyotherARTchangereason.Text);
                    theHT.Add("2ndLineRegimenSwitch", Convert.ToInt32(UserControlKNH_Pharmacy1.ddlReasonforswitchto2ndlineregimen.SelectedValue));                                      
                    //////MultiSelect////Short term effects
                    theHT.Add("Specifyothershorttermeffects", txtSpecifyothershorttermeffects.Text);
                    //////MultiSelect////Long term effects
                    theHT.Add("OtherLongtermEffects", txtlistlongtermeffect.Text);
                    //////////Diagnosis//////////////
                    theHT.Add("SpecifyLabEvaluation", UcLabEval.txtlabdiagnosticinput.Text);
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
                        dr["DateField1"] = txtdt1.Value; // == "" ? null : txtdt1.Value;
                        //dr["DateField2"] = txtdt2.Value;
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
                        dr["DateField1"] = txtdt1.Value;
                        //dr["DateField2"] = txtdt2.Value;
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
                        dr["DateField1"] = txtdt1.Value;
                        //dr["DateField2"] = txtdt2.Value;
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
                        dr["DateField1"] = txtdt1.Value;
                        //dr["DateField2"] = txtdt2.Value;
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;



        }
        public void ShowHideBusinessRules()
        {

            string script = string.Empty;

            //script = "";
            //script = "<script language = 'javascript' defer ='defer' id = 'divshowvitalsigny'>\n";
            //script += "ShowHide('divshowvitalsign','hide');\n";
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
        //private DataTable CreateTempTable()
        //{
        //    DataTable dtprescompl = new DataTable();
        //    DataColumn theID = new DataColumn("ID");
        //    theID.DataType = System.Type.GetType("System.Int32");
        //    dtprescompl.Columns.Add(theID);
        //    DataColumn theDateValue1 = new DataColumn("DateField1");
        //    theDateValue1.DataType = System.Type.GetType("System.DateTime");
        //    dtprescompl.Columns.Add(theDateValue1);
        //    DataColumn theValue1 = new DataColumn("OtherNotes");
        //    theValue1.DataType = System.Type.GetType("System.String");
        //    dtprescompl.Columns.Add(theValue1);
        //    DataColumn theFieldName = new DataColumn("FieldName");
        //    theFieldName.DataType = System.Type.GetType("System.String");
        //    dtprescompl.Columns.Add(theFieldName);
        //    return dtprescompl;
        //}
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
        private void validate()
        {
            txtvisitDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtvisitDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtUpdated_phone.Attributes.Add("OnKeyup", "chkPostiveInteger('" + txtUpdated_phone.ClientID + "')");
            txtDateOfDeathFather.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtDateOfDeathFather.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtDateOfDeathMother.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtDateOfDeathMother.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtTBrxCompleteDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtTBrxCompleteDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtTBRetreatmentDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtTBRetreatmentDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");            

            //txtCurrentARTRegimenDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')";
            //txtCurrentARTRegimenDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')";
            //txtCurrentARTRegimenDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,true,'3')";
            this.idVitalSign.txtHeight.Attributes.Add("onkeyup", "chkDecimal('" + this.idVitalSign.txtHeight.ClientID + "')");
            this.idVitalSign.txtWeight.Attributes.Add("onkeyup", "chkDecimal('" + this.idVitalSign.txtWeight.ClientID + "')");
            this.idVitalSign.txtBPSystolic.Attributes.Add("onkeyup", "chkDecimal('" + this.idVitalSign.txtBPSystolic.ClientID + "')");
            this.idVitalSign.txtBPDiastolic.Attributes.Add("onkeyup", "chkDecimal('" + this.idVitalSign.txtBPDiastolic.ClientID + "')");
            this.idVitalSign.txtRR.Attributes.Add("onkeyup", "chkDecimal('" + this.idVitalSign.txtRR.ClientID + "')");
            this.idVitalSign.txtHR.Attributes.Add("onkeyup", "chkDecimal('" + this.idVitalSign.txtHR.ClientID + "')");

            //txtTBRegimenStartDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            //txtTBRegimenStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,true,'3')");

            //txtTBRegimenEndDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            //txtTBRegimenEndDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,true,'3')");

            //txtINHStartDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            //txtINHStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,true,'3')");
            //txtINHEndDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            //txtINHEndDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,true,'3')");
            //txtPyridoxineStartDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            //txtPyridoxineStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,true,'3')");
            //txtPyridoxineEndDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            //txtPyridoxineEndDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,true,'3')");

            //txtStopINHDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            //txtStopINHDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,true,'3')");

            //txtMenarcheDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            //xtMenarcheDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,true,'3')");
 
 

        }
        public void checkIfPreviuosTabSaved()
        {
            KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            
            
                DataSet dsTriage = new DataSet();
                dsTriage = KNHStatic.CheckIfPreviuosTabSaved("PaedFUTriage", Convert.ToInt32(Session["PatientVisitId"]));
                buttonEnabledAndDisabled(dsTriage, btnClinicalHistorySave, btnClinicalHistoryPrint);
           
                DataSet dsClinincal = new DataSet();
                dsClinincal = KNHStatic.CheckIfPreviuosTabSaved("PaedFUClinicalHistory", Convert.ToInt32(Session["PatientVisitId"]));
                buttonEnabledAndDisabled(dsClinincal, UcTBScreen.btnTBSave, UcTBScreen.btnTBPrint);

                DataSet dsTB = new DataSet();
                dsTB = KNHStatic.CheckIfPreviuosTabSaved("PaedFUTBScreening", Convert.ToInt32(Session["PatientVisitId"]));
                buttonEnabledAndDisabled(dsTB, btnExaminationSave, btnManagementPrint);


                DataSet dsExam = new DataSet();
                dsExam = KNHStatic.CheckIfPreviuosTabSaved("PaedFUExamination", Convert.ToInt32(Session["PatientVisitId"]));
                buttonEnabledAndDisabled(dsExam, btnManagementSave, btnExaminationPrint);

                DataSet dsMgt = new DataSet();
                dsMgt = KNHStatic.CheckIfPreviuosTabSaved("PaedFUManagement", Convert.ToInt32(Session["PatientVisitId"]));
                buttonEnabledAndDisabled(dsMgt, UcPwp.btnSave, UcPwp.btnPrintPositive);
            
            //ds = new DataSet();
            //ds = KNHStatic.CheckIfPreviuosTabSaved("PaedFUPwP", Convert.ToInt32(Session["PatientVisitId"]));
            //buttonEnabledAndDisabled(ds, UcPwp.btnSave,UcPwp.btnSubmitPositive, UcPwp.btnPrintPositive);
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
        public void securityPertab()
        {
            IPatientKNHPEP KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            DataTable thePEPDS = KNHPEP.GetTabID(176);
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable thedt = new DataTable();
            DataView theDV = new DataView(thePEPDS);
            theDV.RowFilter = "TabName='PaedFUTriage'";
            thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            AuthenticationManager Authentication = new AuthenticationManager();
            //triage
            if (thedt.Rows.Count > 0)
            {
                Authentication.TabUserRights(btnTriagesave, btnTriagePrint, 176, Convert.ToInt32(thedt.Rows[0]["TabId"]));
            }

            //CA
            thedt = new DataTable();
            theDV = new DataView(thePEPDS);
            theDV.RowFilter = "TabName='PaedFUClinicalHistory'";
            thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            Authentication.TabUserRights(btnClinicalHistorySave, btnClinicalHistoryPrint, 176, Convert.ToInt32(thedt.Rows[0]["TabId"]));

            //TB
            thedt = new DataTable();
            theDV = new DataView(thePEPDS);
            theDV.RowFilter = "TabName='PaedFUTBScreening'";
            thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            Authentication.TabUserRights(this.UcTBScreen.btnTBSave, this.UcTBScreen.btnTBPrint, 176, Convert.ToInt32(thedt.Rows[0]["TabId"]));

            //Exam
            thedt = new DataTable();
            theDV = new DataView(thePEPDS);
            theDV.RowFilter = "TabName='PaedFUExamination'";
            thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            Authentication.TabUserRights(btnExaminationSave, btnExaminationPrint, 176, Convert.ToInt32(thedt.Rows[0]["TabId"]));
            //Mgt
            thedt = new DataTable();
            theDV = new DataView(thePEPDS);
            theDV.RowFilter = "TabName='PaedFUManagement'";
            thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            Authentication.TabUserRights(this.btnManagementSave, this.btnManagementPrint, 176, Convert.ToInt32(thedt.Rows[0]["TabId"]));
            //PwP
            thedt = new DataTable();
            theDV = new DataView(thePEPDS);
            theDV.RowFilter = "TabName='PaedFUPwP'";
            thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            Authentication.TabUserRights(this.UcPwp.btnSave, this.UcPwp.btnSubmitPositive, 176, Convert.ToInt32(thedt.Rows[0]["TabId"]));
        }

        public void BindAutopopulatingData()
        {
            string script = "";
            IKNHPeadraticFollowup paedFuAP = (IKNHPeadraticFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHPeadraticFollowup, BusinessProcess.Clinical");
            DataSet DSAutopolating = paedFuAP.GetKNHPeadtricFollowupAutoPopulating(Convert.ToInt32(Session["PatientId"].ToString()));
            if (DSAutopolating.Tables[0].Rows.Count > 0)
            {
                if (DSAutopolating.Tables[0].Rows[0]["MotherAlive"] != DBNull.Value)
                {
                    if (DSAutopolating.Tables[0].Rows[0]["MotherAlive"].ToString() == "1")
                        rdomotheraliveyes.Checked = true;
                    else if (DSAutopolating.Tables[0].Rows[0]["MotherAlive"].ToString() == "0")
                    {
                        rdomotheraliveno.Checked = true;

                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'divMotherAliveshowhideyesAP'>\n";
                        script += "ShowHide('divDateOfDeathMother','show');\n";
                        script += "</script>\n";
                        RegisterStartupScript("divMotherAliveshowhideyesAP", script);
                    }
                }

                if (DSAutopolating.Tables[0].Rows[0]["FatherAlive"] != DBNull.Value)
                {
                    if (DSAutopolating.Tables[0].Rows[0]["FatherAlive"].ToString() == "1")
                        rdofatheraliveyes.Checked = true;
                    else if (DSAutopolating.Tables[0].Rows[0]["FatherAlive"].ToString() == "0")
                    {
                        rdofatheraliveno.Checked = true;

                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'divFatherAliveshowhideyesAP'>\n";
                        script += "ShowHide('divDateOfDeathFather','show');\n";
                        script += "</script>\n";
                        RegisterStartupScript("divFatherAliveshowhideyesAP", script);
                    }
                }
                
                if (DSAutopolating.Tables[0].Rows[0]["DateOfDeathMother"] != DBNull.Value)
                    txtDateOfDeathMother.Value = Convert.ToDateTime(DSAutopolating.Tables[0].Rows[0]["DateOfDeathMother"]).ToString("dd-MMM-yyyy");
                if (DSAutopolating.Tables[0].Rows[0]["DateOfDeathFather"] != DBNull.Value)
                    txtDateOfDeathFather.Value = Convert.ToDateTime(DSAutopolating.Tables[0].Rows[0]["DateOfDeathFather"]).ToString("dd-MMM-yyyy");
            }
        }


        public void BindExistingData()
        {
            string script = string.Empty;
            //KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            //DataTable dtSignature = KNHPEP.GetSignature(176, Convert.ToInt32(Session["PatientVisitId"]));
            //foreach (DataRow dr in dtSignature.Rows)
            //{
            //    if (dr["TabName"].ToString() == "PaedFUTriage")
            //        this.UserControlKNH_SignatureTriage.lblSignature.Text = dr["UserName"].ToString();
            //    if (dr["TabName"].ToString() == "PaedFUClinicalHistory")
            //        this.UserControlKNH_SignatureClinical.lblSignature.Text = dr["UserName"].ToString();
            //    if (dr["TabName"].ToString() == "PaedFUExamination")
            //        this.UserControlKNH_SignatureExam.lblSignature.Text = dr["UserName"].ToString();
            //    if (dr["TabName"].ToString() == "PaedFUManagement")
            //        this.UserControlKNH_SignatureMgt.lblSignature.Text = dr["UserName"].ToString();
            //}
            if (Convert.ToInt32(Session["PatientVisitId"].ToString()) > 0)
            {
                KNHPEDFWUP = (IKNHPeadraticFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHPeadraticFollowup, BusinessProcess.Clinical");
                DataSet dsGet = KNHPEDFWUP.GetKNHPeadtricFollowupDetails(Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["PatientVisitId"].ToString()));
                if (dsGet.Tables[0].Rows.Count > 0)
                {
                    //------------------------------section client information

                    if (dsGet.Tables[0].Rows[0]["VisitDate"] != DBNull.Value)
                    {
                        txtvisitDate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["VisitDate"]);
                    }
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
                        txtAddresschange.Text = dsGet.Tables[0].Rows[0]["AddressChange"].ToString();
                    if (dsGet.Tables[0].Rows[0]["PhoneNoChange"] != DBNull.Value)
                        txtUpdated_phone.Text = dsGet.Tables[0].Rows[0]["PhoneNoChange"].ToString();
                    if (dsGet.Tables[0].Rows[0]["PrimaryCareGiver"] != DBNull.Value)
                        txtPrimaryCareGiver.Text = dsGet.Tables[0].Rows[0]["PrimaryCareGiver"].ToString();
                    if (dsGet.Tables[0].Rows[0]["DisclosureStatus"] != DBNull.Value)
                        ddlDisclosureStatus.SelectedValue = dsGet.Tables[0].Rows[0]["DisclosureStatus"].ToString();
                    if (dsGet.Tables[0].Rows[0]["OtherDisclosureReason"] != DBNull.Value)
                        txtOtherDisclosureReason.Text = dsGet.Tables[0].Rows[0]["OtherDisclosureReason"].ToString();
                    if (dsGet.Tables[0].Rows[0]["ReasonNotDisclosed"] != DBNull.Value)
                        txtReasonNotDisclosed.Text = dsGet.Tables[0].Rows[0]["ReasonNotDisclosed"].ToString();
                    if (dsGet.Tables[0].Rows[0]["MotherAlive2"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["MotherAlive2"].ToString() == "0")
                        {
                            //this.ddlMotherAlive2.SelectedValue = "2";
                            rdomotheraliveno.Checked = true;
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divMotherAliveshowhideyes'>\n";
                            script += "ShowHide('divDateOfDeathMother','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divMotherAliveshowhideyes", script);

                        }
                        else if (dsGet.Tables[0].Rows[0]["MotherAlive2"].ToString() == "1")
                        {
                            rdomotheraliveyes.Checked = true;
                            //this.ddlMotherAlive2.SelectedValue = "1";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["DateOfDeathMother"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(dsGet.Tables[0].Rows[0]["DateOfDeathMother"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            txtDateOfDeathMother.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["DateOfDeathMother"]);
                    }
                    if (dsGet.Tables[0].Rows[0]["FatherAlive2"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["FatherAlive2"].ToString() == "0")
                        {
                            rdofatheraliveno.Checked = true;
                            //this.ddlFatherAlive2.SelectedValue = "2";
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divFatherAliveshowhideyes'>\n";
                            script += "ShowHide('divDateOfDeathFather','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divFatherAliveshowhideyes", script);

                        }
                        else if (dsGet.Tables[0].Rows[0]["FatherAlive2"].ToString() == "1")
                        {
                            rdofatheraliveyes.Checked = true;
                            //this.ddlFatherAlive2.SelectedValue = "1";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["DateOfDeathFather"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(dsGet.Tables[0].Rows[0]["DateOfDeathFather"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            txtDateOfDeathFather.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["DateOfDeathFather"]);
                    }
                    if (dsGet.Tables[0].Rows[0]["SchoolingStatus"] != DBNull.Value)
                        ddlSchoolingStatus.SelectedValue = dsGet.Tables[0].Rows[0]["SchoolingStatus"].ToString();
                    if (dsGet.Tables[0].Rows[0]["HighestLevelAttained"] != DBNull.Value)
                        ddlHighestLevelAttained.SelectedValue = dsGet.Tables[0].Rows[0]["HighestLevelAttained"].ToString();
                    if (dsGet.Tables[0].Rows[0]["HIVSupportGroup"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["HIVSupportGroup"].ToString() == "True")
                        {
                            this.rdoHIVSupportGroup.SelectedValue = "1";
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divHIVSupportGroupshowhideyes'>\n";
                            script += "ShowHide('divHIVSupportGroup','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divHIVSupportGroupshowhideyes", script);

                        }
                        else if (dsGet.Tables[0].Rows[0]["HIVSupportGroup"].ToString() == "False")
                        {
                            this.rdoHIVSupportGroup.SelectedValue = "0";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["HIVSupportGroupMembership"] != DBNull.Value)
                        txtHIVSupportGroupMembership.Text = dsGet.Tables[0].Rows[0]["HIVSupportGroupMembership"].ToString();
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
                        if (dsGet.Tables[3].Rows[0]["headcircumference"] != DBNull.Value)
                            this.idVitalSign.txtheadcircumference.Text = dsGet.Tables[3].Rows[0]["headcircumference"].ToString();
                        if (dsGet.Tables[3].Rows[0]["WeightforAge"] != DBNull.Value)
                            this.idVitalSign.lblWA.Text = dsGet.Tables[3].Rows[0]["WeightforAge"].ToString();
                        if (dsGet.Tables[3].Rows[0]["Weightforheight"] != DBNull.Value)
                            this.idVitalSign.lblWH.Text = dsGet.Tables[3].Rows[0]["Weightforheight"].ToString();
                        if (dsGet.Tables[3].Rows[0]["BMIz"] != DBNull.Value)
                            this.idVitalSign.lblBMIz.Text = dsGet.Tables[3].Rows[0]["BMIz"].ToString();
                        if (dsGet.Tables[0].Rows[0]["NursesComments"] != DBNull.Value)
                            this.idVitalSign.txtnursescomments.Text = dsGet.Tables[0].Rows[0]["NursesComments"].ToString();
                    }
                    FillCheckboxlist(idVitalSign.cblReferredTo, dsGet.Tables[1], "RefferedToFUpF");

                    ////////////////////Clinical History///////////////////////////////////////////

                    if (dsGet.Tables[0].Rows[0]["MedicalHistory"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["MedicalHistory"].ToString() == "True")
                        {
                            this.rdoMedicalHistory.SelectedValue = "1";

                        }
                        else if (dsGet.Tables[0].Rows[0]["MedicalHistory"].ToString() == "False")
                        {
                            this.rdoMedicalHistory.SelectedValue = "0";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["OtherMedicalHistorySpecify"] != DBNull.Value)
                        txtOtherMedicalHistorySpecify.Text = dsGet.Tables[0].Rows[0]["OtherMedicalHistorySpecify"].ToString();

                    if (dsGet.Tables[0].Rows[0]["OtherChronicCondition"] != DBNull.Value)
                        txtOtherChronicCondition.Text = dsGet.Tables[0].Rows[0]["OtherChronicCondition"].ToString();

                    if (dsGet.Tables[0].Rows[0]["PresentingComplaintsAdditionalNotes"] != DBNull.Value)
                        UCPresComp.txtAdditionalComplaints.Text = dsGet.Tables[0].Rows[0]["PresentingComplaintsAdditionalNotes"].ToString();
                    if (dsGet.Tables[0].Rows[0]["SchoolPerfomance"] != DBNull.Value)
                        ddlSchoolPerfomance.SelectedValue = dsGet.Tables[0].Rows[0]["SchoolPerfomance"].ToString();
                    if (dsGet.Tables[0].Rows[0]["ImmunisationStatus"] != DBNull.Value)
                        ddlImmunisationStatus.SelectedValue = dsGet.Tables[0].Rows[0]["ImmunisationStatus"].ToString();
                    if (dsGet.Tables[0].Rows[0]["TBHistory"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["TBHistory"].ToString() == "True")
                        {
                            this.rdoTBHistory.SelectedValue = "1";
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'divTBHistoryshowhideyes'>\n";
                            script += "ShowHide('divTBrxCompleteDate','show');ShowHide('divTBRetreatmentDate','show');\n";
                            script += "</script>\n";
                            RegisterStartupScript("divTBHistoryshowhideyes", script);

                        }
                        else if (dsGet.Tables[0].Rows[0]["TBHistory"].ToString() == "False")
                        {
                            this.rdoTBHistory.SelectedValue = "0";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["TBrxCompleteDate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(dsGet.Tables[0].Rows[0]["TBrxCompleteDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            txtTBrxCompleteDate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["TBrxCompleteDate"]);
                    }
                    if (dsGet.Tables[0].Rows[0]["TBRetreatmentDate"] != DBNull.Value)
                    {
                        if (Convert.ToDateTime(dsGet.Tables[0].Rows[0]["TBrxCompleteDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            txtTBRetreatmentDate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["TBRetreatmentDate"]);
                    }


                    if (dsGet.Tables[0].Rows[0]["OtherCurrentLongTermMedications"] != DBNull.Value)
                        txOtherLongTermMedications.Text = dsGet.Tables[0].Rows[0]["OtherCurrentLongTermMedications"].ToString();

                    if (dsGet.Tables[0].Rows[0]["MilestoneAppropriate"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["MilestoneAppropriate"].ToString() == "True")
                        {
                            this.rdoMilestoneAppropriateYes.Checked = true;
                            
                        }
                        else if (dsGet.Tables[0].Rows[0]["MilestoneAppropriate"].ToString() == "False")
                        {
                            this.rdoMilestoneAppropriateNo.Checked = true;
                            visibleDiv("divResonMilestoneInappropriate");

                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["ResonMilestoneInappropriate"] != DBNull.Value)
                        txtResonMilestoneInappropriate.Text = dsGet.Tables[0].Rows[0]["ResonMilestoneInappropriate"].ToString();
                    FillCheckboxlist(chkLongTermMedication, dsGet.Tables[1], "PaedCurrentLongTermMedications");
                    CheckBoxList chkPEGeneral = (CheckBoxList)this.UcPhysExam.FindControl("cblGeneralConditions");
                    FillCheckboxlist(chkPEGeneral, dsGet.Tables[1], "GeneralConditions");
                    //Cardiovascular conditions
                    CheckBoxList chkPECardiovascular = (CheckBoxList)this.UcPhysExam.FindControl("cblCardiovascularConditions");
                    FillCheckboxlist(chkPECardiovascular, dsGet.Tables[1], "CardiovascularConditions");
                    //CNS 
                    CheckBoxList chkPECNS = (CheckBoxList)this.UcPhysExam.FindControl("cblCNSConditions");
                    FillCheckboxlist(chkPECNS, dsGet.Tables[1], "CNSConditions");
                    //Oral cavity
                    CheckBoxList chkPEOralCavity = (CheckBoxList)this.UcPhysExam.FindControl("cblOralCavityConditions");
                    FillCheckboxlist(chkPEOralCavity, dsGet.Tables[1], "OralCavityConditions");
                    //Chest Lungs
                    CheckBoxList chkPEChestLungs = (CheckBoxList)this.UcPhysExam.FindControl("cblChestLungsConditions");
                    FillCheckboxlist(chkPEChestLungs, dsGet.Tables[1], "ChestLungsConditions");
                    //Genitourinary 
                    CheckBoxList chkPEGenitourinary = (CheckBoxList)this.UcPhysExam.FindControl("cblGenitalUrinaryConditions");
                    FillCheckboxlist(chkPEGenitourinary, dsGet.Tables[1], "GenitalUrinaryConditions");
                    //Skin 
                    CheckBoxList chkPESkin = (CheckBoxList)this.UcPhysExam.FindControl("cblSkinConditions");
                    FillCheckboxlist(chkPESkin, dsGet.Tables[1], "SkinConditions");
                    //Abdomen conditions
                    CheckBoxList chkPEabdomen = (CheckBoxList)this.UcPhysExam.FindControl("cblAbdomenConditions");
                    FillCheckboxlist(chkPEabdomen, dsGet.Tables[1], "AbdomenConditions");

                    UcPhysExam.txtOtherGeneralConditions.Text = dsGet.Tables[0].Rows[0]["OtherGeneralConditions"].ToString();
                    UcPhysExam.txtOtherAbdomenConditions.Text = dsGet.Tables[0].Rows[0]["OtherAbdomenConditions"].ToString();
                    UcPhysExam.txtOtherCardiovascularConditions.Text = dsGet.Tables[0].Rows[0]["OtherCardiovascularConditions"].ToString();
                    UcPhysExam.txtOtherOralCavityConditions.Text = dsGet.Tables[0].Rows[0]["OtherOralCavityConditions"].ToString();
                    UcPhysExam.txtOtherGenitourinaryConditions.Text = dsGet.Tables[0].Rows[0]["OtherGenitourinaryConditions"].ToString();
                    UcPhysExam.txtOtherCNSConditions.Text = dsGet.Tables[0].Rows[0]["OtherCNSConditions"].ToString();
                    UcPhysExam.txtOtherChestLungsConditions.Text = dsGet.Tables[0].Rows[0]["OtherChestLungsConditions"].ToString();
                    UcPhysExam.txtOtherSkinConditions.Text = dsGet.Tables[0].Rows[0]["OtherSkinConditions"].ToString();
                    

                    if (dsGet.Tables[0].Rows[0]["OtherMedicalConditionNotes"] != DBNull.Value)
                        UcPhysExam.txtOtherMedicalConditionNotes.Text = dsGet.Tables[0].Rows[0]["OtherMedicalConditionNotes"].ToString();
                    ////////////WHO Stage///////////////////////////////////////////
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

                    ////WHO Stage I
                    IQCareUtils iQCareUtils = new IQCareUtils();
                    BindWHOListData(dsGet.Tables[1]);
                    //DataTable dt = new DataTable();
                    //DataView theDV = new DataView(dsGet.Tables[1]);
                    //theDV.RowFilter = "FieldName='CurrentWHOStageIConditions'";
                    //dt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                    //if (dt.Rows.Count > 0)
                    //{

                    //    ViewState["WHOStage1"] = dt;
                    //    UCWHO.gvWHO1.RowDataBound += new GridViewRowEventHandler(gvWHO1_RowDataBound);
                    //}
                    //////WHO Stage 2
                    //dt = new DataTable();
                    //theDV = new DataView(dsGet.Tables[1]);
                    //theDV.RowFilter = "FieldName='CurrentWHOStageIIConditions'";
                    //dt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                    //if (dt.Rows.Count > 0)
                    //{

                    //    ViewState["WHOStage2"] = dt;
                    //    UCWHO.gvWHO2.RowDataBound += new GridViewRowEventHandler(gvWHO2_RowDataBound);
                    //}
                    //////WHO Stage 3
                    //dt = new DataTable();
                    //theDV = new DataView(dsGet.Tables[1]);
                    //theDV.RowFilter = "FieldName='CurrentWHOStageIIIConditions'";
                    //dt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                    //if (dt.Rows.Count > 0)
                    //{

                    //    ViewState["WHOStage3"] = dt;
                    //    UCWHO.gvWHO3.RowDataBound += new GridViewRowEventHandler(gvWHO3_RowDataBound);
                    //}
                    //////WHO Stage 4
                    //dt = new DataTable();
                    //theDV = new DataView(dsGet.Tables[1]);
                    //theDV.RowFilter = "FieldName='CurrentWHOStageIVConditions'";
                    //dt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                    //if (dt.Rows.Count > 0)
                    //{

                    //    ViewState["WHOStage4"] = dt;
                    //    UCWHO.gvWHO4.RowDataBound += new GridViewRowEventHandler(gvWHO4_RowDataBound);
                    //}
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

                    //////////////////////////////Diagnosis///////////////////////////////////////////////////////
                    if (dsGet.Tables[0].Rows[0]["Impression"] != DBNull.Value)
                    {
                        ddlHAARTImpression.SelectedValue = dsGet.Tables[0].Rows[0]["Impression"].ToString();
                        if (ddlHAARTImpression.SelectedItem.Text == "Other specify")
                        {
                            visibleDiv("divSpecifyotherimpression");
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["HAARTexperienced"] != DBNull.Value)
                        ddlHAARTexperienced.SelectedValue = dsGet.Tables[0].Rows[0]["HAARTexperienced"].ToString();
                    if (dsGet.Tables[0].Rows[0]["OtherImpression"] != DBNull.Value)
                        txtOtherHAARTImpression.Text = dsGet.Tables[0].Rows[0]["OtherImpression"].ToString();
                    if (dsGet.Tables[0].Rows[0]["reviewprevresult"] != System.DBNull.Value)
                    {
                        //if (dsGet.Tables[0].Rows[0]["reviewprevresult"].ToString() == "True")
                        //{
                            this.rdoReviewedPreviousResults.SelectedValue = dsGet.Tables[0].Rows[0]["reviewprevresult"].ToString();

                        //}

                    }
                    if (dsGet.Tables[0].Rows[0]["additonalinformation"] != DBNull.Value)
                        txtResultsReviewComments.Text = dsGet.Tables[0].Rows[0]["additonalinformation"].ToString();
                    if (dsGet.Tables[0].Rows[0]["HIVRelatedOI"] != DBNull.Value)
                        txtHIVRelatedOI.Text = dsGet.Tables[0].Rows[0]["HIVRelatedOI"].ToString();
                    if (dsGet.Tables[0].Rows[0]["NonHIVRelatedOI"] != DBNull.Value)
                        txtNonHIVRelatedOI.Text = dsGet.Tables[0].Rows[0]["NonHIVRelatedOI"].ToString();
                    /////////////////////////////////////////////////////////////////////////////////////////////

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

                    }
                    if (dsGet.Tables[0].Rows[0]["MissedDosesFUPspecify"] != DBNull.Value)
                        txtSpecifywhydosesmissed.Text = dsGet.Tables[0].Rows[0]["MissedDosesFUPspecify"].ToString();
                    //if (dsGet.Tables[0].Rows[0]["DelayedMedication"] != DBNull.Value)
                    if (dsGet.Tables[0].Rows[0]["DelaysInTakingMedication"] != DBNull.Value)
                    {
                        this.rdohavedelayed.SelectedValue = dsGet.Tables[0].Rows[0]["DelaysInTakingMedication"].ToString();

                        //if (dsGet.Tables[0].Rows[0]["DelaysInTakingMedication"].ToString() == "1")
                        //{
                        //    this.rdohavedelayed.SelectedValue = "1";

                        //}
                        //else if (dsGet.Tables[0].Rows[0]["DelaysInTakingMedication"].ToString() == "0")
                        //{
                        //    this.rdohavedelayed.SelectedValue = "0";
                        //}
                    }



                    if (dsGet.Tables[0].Rows[0]["ARVSideEffects"] != System.DBNull.Value)
                    {
                        if (dsGet.Tables[0].Rows[0]["ARVSideEffects"].ToString() == "True")
                        {
                            this.rdoARVsideeffects.SelectedValue = "1";
                            //script = "";
                            //script = "<script language = 'javascript' defer ='defer' id = 'divARVSideEffectsshowhideyes'>\n";
                            //script += "ShowHide('divshorttermeffects','show');ShowHide('divLongtermeffects','show');\n";
                            //script += "</script>\n";
                            //RegisterStartupScript("divARVSideEffectsshowhideyes", script);

                        }
                        else if (dsGet.Tables[0].Rows[0]["ARVSideEffects"].ToString() == "False")
                        {
                            this.rdoARVsideeffects.SelectedValue = "0";
                        }
                    }
                    if (dsGet.Tables[0].Rows[0]["OtherShortTermEffects"] != DBNull.Value)
                        txtSpecifyothershorttermeffects.Text = dsGet.Tables[0].Rows[0]["OtherShortTermEffects"].ToString();
                    if (dsGet.Tables[0].Rows[0]["OtherLongtermEffects"] != DBNull.Value)
                        txtlistlongtermeffect.Text = dsGet.Tables[0].Rows[0]["OtherLongtermEffects"].ToString();


                    if (dsGet.Tables[0].Rows[0]["SpecifyLabEvaluation"] != System.DBNull.Value)
                    {
                        UcLabEval.txtlabdiagnosticinput.Text = dsGet.Tables[0].Rows[0]["SpecifyLabEvaluation"].ToString();
                    }
                    if (dsGet.Tables[0].Rows[0]["OIProphylaxis"] != DBNull.Value)
                    {
                        ddlOIProphylaxis.SelectedValue = dsGet.Tables[0].Rows[0]["OIProphylaxis"].ToString();
                        if (ddlOIProphylaxis.SelectedItem.Text == "Cotrimoxazole")
                        {
                            visibleDiv("divCotrimoxazoleprescribedfor");
                        }
                        if (ddlOIProphylaxis.SelectedItem.Text == "Fluconazole")
                        {
                            visibleDiv("divFluconazoleshowhide");
                            //Fluconazole prescribed for :
                            if (dsGet.Tables[0].Rows[0]["Fluconazole"] != DBNull.Value)
                                ddlfluconazole.SelectedValue = dsGet.Tables[0].Rows[0]["Fluconazole"].ToString();
                        }
                        if (ddlOIProphylaxis.SelectedItem.Text == "CTX and Fluconazol")
                        {
                            visibleDiv("divFluconazoleshowhide");
                            visibleDiv("divCotrimoxazoleprescribedfor");
                            //Cotrimoxazole prescribed for
                            if (dsGet.Tables[0].Rows[0]["ReasonCTXpresribed"] != DBNull.Value)
                                ddlCotrimoxazoleprescribed.SelectedValue = dsGet.Tables[0].Rows[0]["ReasonCTXpresribed"].ToString();
                        }
                        if (ddlOIProphylaxis.SelectedItem.Text == "Other")
                        {
                            visibleDiv("divOtherOIPropholyxis");
                            //Other (Specify)
                            if (dsGet.Tables[0].Rows[0]["OtherOIProphylaxis"] != DBNull.Value)
                                txtOtherOIPropholyxis.Text = dsGet.Tables[0].Rows[0]["OtherOIProphylaxis"].ToString();
                        }
                    }

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

                    bindPresentingComplaint(dsGet.Tables[1]);
                    FillCheckboxlist(cblShorttermeffects, dsGet.Tables[1], "ShortTermEffects");
                    FillCheckboxlist(UserControlKNH_Pharmacy1.chklistEligiblethrough, dsGet.Tables[1], "ARTEligibility");
                    FillCheckboxlist(UserControlKNH_Pharmacy1.chklistARTstopcode, dsGet.Tables[1], "ARTstopcode");
                    FillCheckboxlist(UserControlKNH_Pharmacy1.chklistARTchangecode, dsGet.Tables[1], "ARTchangecode");

                    FillCheckboxlist(chklistlongtermeffect, dsGet.Tables[1], "LongTermEffects");
                    FillCheckboxlist(cblDiagnosis2, dsGet.Tables[1], "Diagnosis");

                    FillCheckboxlist(chkLongTermMedication, dsGet.Tables[1], "PaedCurrentLongTermMedications");
                    FillCheckboxlist(cblChronicCondition, dsGet.Tables[1], "ChronicCondition");


                }
            }
            else
            {
                hdnVisitId.Value = "0";
                txtvisitDate.Value = Application["AppCurrentDate"].ToString();
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
                            if (theDR["DateField1"].ToString() != "")
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
        private void bindPresentingComplaint(DataTable thedt)
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable dt = new DataTable();
            string script = string.Empty;
            if (thedt.Rows.Count > 0)
            {
                DataView theDV = new DataView(thedt);
                theDV.RowFilter = "FieldName='PresentingComplaints'";
                dt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                {
                    for (int i = 0; i < this.UCPresComp.gvPresentingComplaints.Rows.Count; i++)
                    {
                        Label lblPComplaintsId = (Label)UCPresComp.gvPresentingComplaints.Rows[i].FindControl("lblPresenting");
                        CheckBox chkPComplaints = (CheckBox)UCPresComp.gvPresentingComplaints.Rows[i].FindControl("ChkPresenting");
                        TextBox txtPComplaints = (TextBox)UCPresComp.gvPresentingComplaints.Rows[i].FindControl("txtPresenting");
                        if (Convert.ToInt32(dt.Rows[j]["ValueId"]) == Convert.ToInt32(lblPComplaintsId.Text))
                        {
                            if (dt.Rows[j]["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("DivOther");
                            }
                            chkPComplaints.Checked = true;
                            txtPComplaints.Text = dt.Rows[j]["Other_notes"].ToString();
                            if (dt.Rows[j]["Name"].ToString().ToLower() == "none")
                            {
                                String pcscript = "";
                                pcscript = "<script language = 'javascript' defer ='defer' id = 'togglePresComp'>\n";
                                pcscript += "togglePC('" + chkPComplaints.ClientID + "')\n";
                                pcscript += "</script>\n";
                                RegisterStartupScript("'togglePresComp'", pcscript);
                            }
                        }
                                               
                    }
                }
            }
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
        
        protected void tabControl_ActiveTabChanged(object sender, EventArgs e)
        {
            checkIfPreviuosTabSaved();
            ErrorLoad();
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
                int delete = theUtils.DeleteForm("Paediatric Follow up", Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppUserId"]));

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