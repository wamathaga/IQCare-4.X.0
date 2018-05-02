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
    public partial class frmClinical_Nigeria_AdultIE : LogPage
    {
        INigeriaARTCard NigAdultIE;
        IKNHStaticForms KNHStatic;
        DataTable dtmuiltselect;
        protected void Page_Load(object sender, EventArgs e)
        {
            FemaleControls();
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx", true);
            }
            if (!IsPostBack)
            {
                //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
                //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Adult Initial Evaluation";
                //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Adult Initial Evaluation";
                BindChkboxlstControl(cblRiskFactors, "RiskFactors");
                BindChkboxlstControl(cblShortTermEffects, "ShortTermEffects");
                cblShortTermEffects.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblShortTermEffects.ClientID + "','divshorttermeffecttxt','Other Specify','" + txtOtherShortTermEffects.ClientID + "')");
                BindChkboxlstControl(cblLongTermEffects, "LongTermEffects");
                cblLongTermEffects.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblLongTermEffects.ClientID + "','divlongtermeffecttxt','Other specify','" + txtOtherLongtermEffects.ClientID + "')");
                BindControl(rblassessment, "NigeriaAssessment");
                validate();
                UserControlKNH_NextAppointment1.rdoTCAYes.Checked = true;
                
            }

        }
        public void BindControl(Control cntrl, string fieldname)
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
            if (fieldname.ToString() != "")
            {
                if (theCodeDT.Rows.Count > 0)
                {

                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + theCodeDT.Rows[0]["CodeId"];
                    theDV.Sort = "SRNo ASC";
                    thedeCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                }
                if (cntrl is CheckBoxList)
                {
                    if (thedeCodeDT.Rows.Count > 0)
                    {
                        BindManager.BindCheckedList((CheckBoxList)cntrl, thedeCodeDT, "Name", "ID");
                    }
                }
                else if (cntrl is DropDownList)
                {
                    if (thedeCodeDT.Rows.Count > 0)
                    {
                        BindManager.BindCombo((DropDownList)cntrl, thedeCodeDT, "Name", "ID");
                    }
                }
                else if (cntrl is RadioButtonList)
                {
                    if (thedeCodeDT.Rows.Count > 0)
                    {
                        BindManager.RadioButtonList((RadioButtonList)cntrl, thedeCodeDT, "Name", "ID");
                    }
                }

            }
            else
            {
                theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
                if (theDV.Table.Rows.Count > 0)
                {
                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=17 and ModuleId=209";
                    theDV.Sort = "SRNo ASC";
                    thedeCodeDT = new DataTable();
                    thedeCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                }
                if (cntrl is CheckBoxList)
                {
                    if (thedeCodeDT.Rows.Count > 0)
                    {
                        BindManager.BindCheckedList((CheckBoxList)cntrl, thedeCodeDT, "Name", "ID");
                    }
                }
                else if (cntrl is DropDownList)
                {
                    if (thedeCodeDT.Rows.Count > 0)
                    {
                        BindManager.BindCombo((DropDownList)cntrl, thedeCodeDT, "Name", "ID");
                    }
                }
                else if (cntrl is RadioButtonList)
                {
                    if (thedeCodeDT.Rows.Count > 0)
                    {
                        BindManager.RadioButtonList((RadioButtonList)cntrl, thedeCodeDT, "Name", "ID");
                    }
                }
            }

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    BindExistingData();                    
                }
                else
                {
                    txtVisitDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");

                }
                
            }
            ErrorLoad();
            checkIfPreviuosTabSaved();
        }
        public void FemaleControls()
        {
            if (!object.Equals(Session["PatientSex"], null))//Add this line by Rahmat for check session object is created or not(16-Feb-17)
            {
                if (Session["PatientSex"].ToString() == "Male")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "trNigPregnancy", "ShowHide('trNigPregnancy','hide');", true);
                }
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

            if (UcAdherence.rdoTreatmentIntrupted.SelectedValue == "0")
            {
                visibleDiv("DIVInturptedReason");
            }
            if (UcAdherence.rblstopped.SelectedValue == "0")
            {
                visibleDiv("DIVStopedReason");
            }
            if (UserControlKNH_NextAppointment1.rdoTCANo.Checked == true)
            {
                visibleDiv("trCareEnd");                
            }
            else if (UserControlKNH_NextAppointment1.rdoTCAYes.Checked == true)
            {
                visibleDiv("trNextAppointment");                
            }
            for (int i = 0; i < this.UcPc.gvPresentingComplaints.Rows.Count; i++)
            {
                CheckBox chkPComplaints = (CheckBox)UcPc.gvPresentingComplaints.Rows[i].FindControl("ChkPresenting");
                if (chkPComplaints.Checked)
                {
                    if (chkPComplaints.Text.ToLower() == "other")
                    {
                        visibleDiv("DivOther");
                    }
                    if (chkPComplaints.Text.ToUpper() == "NONE")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "togglePCChecked", "togglePC('" + chkPComplaints.ClientID + "');", true);
                    }
                }
            }
            if (rdopregnantyesno.SelectedValue == "1")
            {
                visibleDiv("hideFP");
            }

            if (UCTreatment.ChkLabEvaluation.Checked)
            {
                visibleDiv("DivLabEval");
            }
            if (UCTreatment.chkregimen.Checked)
            {
                visibleDiv("DivPrescDrug");
            }
            for (int i = 0; i < this.UCTreatment.gvtreatment.Rows.Count; i++)
            {
                CheckBox Chktreatment = (CheckBox)UCTreatment.gvtreatment.Rows[i].FindControl("Chktreatment");
                if (Chktreatment.Checked)
                {
                    if (Chktreatment.Text.ToLower() == "other referrals")
                    {
                        visibleDiv("DivTreatmentOther");
                    }
                    
                }
            }

            if (UCTreatment.ddlTreatmentplan.SelectedItem.Text.ToLower() == "change treatment")
            {
                visibleDiv("divARTchangecode");
            }
            for (int i = 0; i < this.UcAdherence.gvdisclosed.Rows.Count; i++)
            {
                CheckBox Chkdisclosed = (CheckBox)UcAdherence.gvdisclosed.Rows[i].FindControl("Chkdisclosed");
                if (Chkdisclosed.Checked)
                {
                    if (Chkdisclosed.Text.ToLower() == "other")
                    {
                        visibleDiv("DivDiscloseOther");
                    }

                }
            }
               
        }

        private Hashtable HtParameters(string Tabname)
        {
            dtmuiltselect = CreateTempTable();
            Hashtable theHT = new Hashtable();
            try
            {
                theHT.Add("patientID", Session["PatientId"]);
                theHT.Add("visitID", Session["PatientVisitId"]);
                theHT.Add("locationID", Session["AppLocationId"]);
                // Visit date:
                theHT.Add("visitDate", txtVisitDate.Value);

                if (Tabname.ToString() == "Clinical History")
                {
                    //////////Additional Presenting Complaints//////////////
                    theHT.Add("OtherPresentingComplaints", UcPc.txtOtherPresentingComplaints.Text);
                    theHT.Add("PresentingComplaintsAdditionalNotes", UcPc.txtAdditionalComplaints.Text);
                    //////////Medical History (Disease, diagnosis& treatment)//////////////
                    theHT.Add("MedicalHistoryAdditionalComplaints", idNigeriaMedical.txtAdditionalComplaints.Text);
                    theHT.Add("MedicalHistoryLastHistory", this.idNigeriaMedical.txtlastmedical.Text);
                    theHT.Add("MedicalHistoryFamilyHistory", idNigeriaMedical.txtfamhistory.Text);
                    theHT.Add("MedicalHistoryHospitalization", idNigeriaMedical.txthospitalization.Text);

                    //////////Pregnancy//////////////
                    theHT.Add("Pregnant", rdopregnantyesno.SelectedValue);
                    theHT.Add("LMPDate", txtLMPdate.Value);
                    theHT.Add("EDDDate", txtEDDDate.Value);
                    theHT.Add("GestationalAge", txtgestage.Text);
                }
                else if (Tabname.ToString() == "HIV History")
                {
                    //////////Medical History (Disease, diagnosis& treatment)//////////////
                    int PrevARVExposure = UcPriorArt.rbtnknownYes.Checked ? 1 : UcPriorArt.rbtnknownNo.Checked ? 0 : 9;
                    theHT.Add("PrevARVExposure", PrevARVExposure);
                    theHT.Add("ARTTransferinFrom", UcPriorArt.ddlfacilityname.SelectedValue);
                    theHT.Add("durationcarefrom", UcPriorArt.txtdurationfrom.Value);
                    theHT.Add("durationcareto", UcPriorArt.txtdurationto.Value);
                    theHT.Add("EntryType", UcPriorArt.ddlentrytype.SelectedValue);
                    //////////HIV Related History//////////////
                    theHT.Add("LatestCD4", txtlatestcd4number.Text);
                    theHT.Add("LatestCD4Date", dtlatestcd4date.Value);
                    theHT.Add("LowestCD4", txtlatestcd4number.Text);
                    theHT.Add("LowestCD4Date", txtlbllowestCD4Date.Value);
                    theHT.Add("LatestViralLoad", txtlatestViralLoadCount.Text);
                    theHT.Add("LatestViralLoadDate", txtlatestViralLoadDate.Value);

                    theHT.Add("ComplaintOther", UcCurrentMed.txtOtherComplaints.Text);
                    theHT.Add("ServiceEntry", ddlreferredfrom.SelectedValue);

                    theHT.Add("ParticipatedAdhernce", UcAdherence.rbladherenceYesNo.SelectedValue);
                    theHT.Add("MissedArv3days", UcAdherence.rblmissedarvYesNo.SelectedValue);
                    theHT.Add("ReasomMissedARV", UcAdherence.ddlReasomMissed.SelectedValue);
                    theHT.Add("TreatmentIntrupted", UcAdherence.rdoTreatmentIntrupted.SelectedValue);
                    theHT.Add("IntrupptedDate", UcAdherence.txtdtIntrupptedDate.Value);
                    theHT.Add("intrpdays", UcAdherence.txtintrpdays.Text);
                    theHT.Add("ReasonInterrupted", UcAdherence.ddlreasonInterrupted.SelectedValue);
                    theHT.Add("Treatmentstopped", UcAdherence.rblstopped.SelectedValue);
                    theHT.Add("StopedReasonDate", UcAdherence.txtStopedReasonDate.Value);
                    theHT.Add("stoppeddays", UcAdherence.txtstoppeddays.Text);
                    theHT.Add("StopedReason", UcAdherence.ddlStopedReason.SelectedValue);

                    theHT.Add("Otherdisclosed", UcAdherence.txtOtherdisclosed.Text);
                    theHT.Add("hivdiscussed", UcAdherence.txthivdiscussed.Text);
                    theHT.Add("supportgroup", UcAdherence.rblsupportgroup.SelectedValue);

                    theHT.Add("OtherShortTermEffects", txtOtherShortTermEffects.Text);
                    theHT.Add("OtherLongtermEffects", txtOtherLongtermEffects.Text);
                }
                if (Tabname.ToString() == "Examination")
                {
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

                    theHT.Add("BMIz", this.idVitalSign.txtBMI.Text);


                    #endregion

                    theHT.Add("OtherNigeriaPEGeneral", UcPE.txtOtherNigeriaPEGeneral.Text);
                    theHT.Add("OtherNigeriaPESkin", UcPE.txtOtherNigeriaPESkin.Text);
                    theHT.Add("OtherNigeriaPEHeadEyeEnt", UcPE.txtOtherNigeriaPEHeadEyeEnt.Text);
                    theHT.Add("OtherNigeriaPECardiovascular", UcPE.txtOtherNigeriaPECardiovascular.Text);
                    theHT.Add("OtherNigeriaPEBreast", UcPE.txtOtherNigeriaPEBreast.Text);
                    theHT.Add("OtherNigeriaPEGenitalia", UcPE.txtOtherNigeriaPEGenitalia.Text);
                    theHT.Add("txtOtherNigeriaPERespiratory", UcPE.txtOtherNigeriaPERespiratory.Text);
                    theHT.Add("OtherNigeriaPEGastrointestinal", UcPE.txtOtherNigeriaPEGastrointestinal.Text);
                    theHT.Add("OtherNigeriaPENeurological", UcPE.txtOtherNigeriaPENeurological.Text);
                    theHT.Add("OtherNigeriaPEMentalstatus", UcPE.txtOtherNigeriaPEMentalstatus.Text);
                    theHT.Add("OtherAdditionaldetailedfindings", UcPE.txtOtherAdditionaldetailedfindings.Text);
                    theHT.Add("Assessment", rblassessment.SelectedValue);
                    theHT.Add("AssessmentDesc", txtAssessmentNotes.Text);
                    theHT.Add("WHOStage", UcWhostaging.ddlwhostage1.SelectedValue);
                }
                if (Tabname.ToString() == "Management")
                {
                    theHT.Add("LabEvaluation", UCTreatment.ChkLabEvaluation.Checked ? 1 : 0);
                    theHT.Add("LabReview", UCTreatment.UcLabEval.txtlabdiagnosticinput.Text);
                    theHT.Add("OtherReferrals", UCTreatment.txtOtherReferrals.Text);
                    theHT.Add("Regimen", UCTreatment.chkregimen.Checked ? 1 : 0);
                    theHT.Add("ARVTherapyPlan", UCTreatment.ddlTreatmentplan.SelectedValue);
                    theHT.Add("OtherARVChangePlan", UCTreatment.txtSpecifyotherARTchangereason.Text);
                }
                return theHT;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void GetCheckBoxListData(string savetabname)
        {
            dtmuiltselect = CreateTempTable();
            if (savetabname == "Clinical History" || tabControl.ActiveTabIndex == 0)
            {
                savetabname = "Clinical History";
                dtmuiltselect = PresentingComplaints(dtmuiltselect, "PresentingComplaints");
                dtmuiltselect = MedicalHistory(dtmuiltselect, "NigeriaSymptoms");
                dtmuiltselect = GetCheckBoxListValues(cblRiskFactors, dtmuiltselect, "RiskFactors");
            }
            else if (savetabname == "HIV History" || tabControl.ActiveTabIndex == 1)
            {
                savetabname = "HIV History";
                dtmuiltselect = CurrentMedication(dtmuiltselect, "NigeriaCurrentMedication");
                dtmuiltselect = DisclosedStatus(dtmuiltselect, "NigeriaHIVDisclosure");
                dtmuiltselect = GetCheckBoxListValues(cblShortTermEffects, dtmuiltselect, "ShortTermEffects");
                dtmuiltselect = GetCheckBoxListValues(cblLongTermEffects, dtmuiltselect, "LongTermEffects");
            }
            else if (savetabname == "Examination" || tabControl.ActiveTabIndex == 2)
            {
                savetabname = "Examination";
                dtmuiltselect = GetCheckBoxListValues(UcPE.cblNigeriaPEGeneral, dtmuiltselect, "NigeriaPEGeneral");
                dtmuiltselect = GetCheckBoxListValues(UcPE.cblNigeriaPESkin, dtmuiltselect, "NigeriaPESkin");
                dtmuiltselect = GetCheckBoxListValues(UcPE.cblNigeriaPEHeadEyeEnt, dtmuiltselect, "NigeriaPEHeadEyeEnt");
                dtmuiltselect = GetCheckBoxListValues(UcPE.cblNigeriaPECardiovascular, dtmuiltselect, "NigeriaPECardiovascular");
                dtmuiltselect = GetCheckBoxListValues(UcPE.cblNigeriaPEBreast, dtmuiltselect, "NigeriaPEBreast");
                dtmuiltselect = GetCheckBoxListValues(UcPE.cblNigeriaPEGenitalia, dtmuiltselect, "NigeriaPEGenitalia");
                dtmuiltselect = GetCheckBoxListValues(UcPE.cblNigeriaPERespiratory, dtmuiltselect, "NigeriaPERespiratory");
                dtmuiltselect = GetCheckBoxListValues(UcPE.cblNigeriaPEGastrointestinal, dtmuiltselect, "NigeriaPEGastrointestinal");
                dtmuiltselect = GetCheckBoxListValues(UcPE.cblNigeriaPENeurological, dtmuiltselect, "NigeriaPENeurological");
                dtmuiltselect = GetCheckBoxListValues(UcPE.cblNigeriaPEMentalstatus, dtmuiltselect, "NigeriaPEMentalstatus");
                //WHO Stage I            
                dtmuiltselect = InsertMultiSelectList(UcWhostaging.gvWHO1, dtmuiltselect, "NigeriaWHOStageIConditions");
                //// WHO Stage II
                dtmuiltselect = InsertMultiSelectList(UcWhostaging.gvWHO2, dtmuiltselect, "NigeriaWHOStageIIConditions");
                //// WHO Stage III
                dtmuiltselect = InsertMultiSelectList(UcWhostaging.gvWHO3, dtmuiltselect, "NigeriaWHOStageIIIConditions");
                //// WHO Stage IV
                dtmuiltselect = InsertMultiSelectList(UcWhostaging.gvWHO4, dtmuiltselect, "NigeriaWHOStageIVConditions");
            }
            else if (savetabname == "Management" || tabControl.ActiveTabIndex == 3)
            {
                savetabname = "Management";
                dtmuiltselect = TreatmentLab(dtmuiltselect, "NigeriaListLabEvaluation");
                dtmuiltselect = EnrollIn(dtmuiltselect, "NigeriaListEnrollin");
                dtmuiltselect = GetCheckBoxListValues(UCTreatment.chklistARTchangecode, dtmuiltselect, "NigeriaARVTreamentChangeReason");
            }

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
                    if (txt.Text != "")
                    {
                        dr["Other_Notes"] = txt.Text;
                    }
                    dtprescompl.Rows.Add(dr);
                }


            }
            return dtprescompl;

        }
        private DataTable MedicalHistory(DataTable dtprescompl, string name)
        {

            GridView gdview = (GridView)idNigeriaMedical.FindControl("gvMedicalHistory");
            foreach (GridViewRow row in gdview.Rows)
            {
                DataRow dr = dtprescompl.NewRow();
                CheckBox chk = (CheckBox)row.FindControl("ChkMedical");
                TextBox txt = (TextBox)row.FindControl("txtMedical");
                Label lbl = (Label)row.FindControl("lblMedical");
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
        private DataTable CurrentMedication(DataTable dtprescompl, string name)
        {
            GridView gdview = (GridView)UcCurrentMed.FindControl("gvcurrentmedication");
            foreach (GridViewRow row in gdview.Rows)
            {
                DataRow dr = dtprescompl.NewRow();
                CheckBox chk = (CheckBox)row.FindControl("Chkmedication");
                Label lbl = (Label)row.FindControl("lblmedication");
                if (chk.Checked)
                {
                    dr["ID"] = Convert.ToInt32(lbl.Text);
                    dr["FieldName"] = name;

                    dtprescompl.Rows.Add(dr);
                }

            }
            return dtprescompl;

        }
        private DataTable DisclosedStatus(DataTable dtprescompl, string name)
        {
            GridView gdview = (GridView)UcAdherence.FindControl("gvdisclosed");
            foreach (GridViewRow row in gdview.Rows)
            {
                DataRow dr = dtprescompl.NewRow();
                CheckBox chk = (CheckBox)row.FindControl("Chkdisclosed");
                Label lbl = (Label)row.FindControl("lbldisclosed");
                if (chk.Checked)
                {
                    dr["ID"] = Convert.ToInt32(lbl.Text);
                    dr["FieldName"] = name;

                    dtprescompl.Rows.Add(dr);
                }

            }
            return dtprescompl;

        }
        private DataTable InsertMultiSelectList(GridView gdview, DataTable dt, string fieldname)
        {
            foreach (GridViewRow row in gdview.Rows)
            {
                DataRow dr = dt.NewRow();

                if (fieldname == "NigeriaWHOStageIConditions")
                {
                    CheckBox chk = (CheckBox)row.FindControl("Chkwho1");

                    Label lbl = (Label)row.FindControl("lblwho1");

                    if (chk.Checked)
                    {
                        dr["ID"] = Convert.ToInt32(lbl.Text);
                        dr["FieldName"] = fieldname;
                        dt.Rows.Add(dr);
                    }
                }
                if (fieldname == "NigeriaWHOStageIIConditions")
                {
                    CheckBox chk = (CheckBox)row.FindControl("Chkwho2");
                    Label lbl = (Label)row.FindControl("lblwho2");

                    if (chk.Checked)
                    {
                        dr["ID"] = Convert.ToInt32(lbl.Text);
                        dr["FieldName"] = fieldname;
                        dt.Rows.Add(dr);
                    }
                }
                if (fieldname == "NigeriaWHOStageIIIConditions")
                {
                    CheckBox chk = (CheckBox)row.FindControl("Chkwho3");
                    Label lbl = (Label)row.FindControl("lblwho3");

                    if (chk.Checked)
                    {
                        dr["ID"] = Convert.ToInt32(lbl.Text);
                        dr["FieldName"] = fieldname;
                        dt.Rows.Add(dr);
                    }
                }
                if (fieldname == "NigeriaWHOStageIVConditions")
                {
                    CheckBox chk = (CheckBox)row.FindControl("Chkwho4");
                    Label lbl = (Label)row.FindControl("lblwho4");

                    if (chk.Checked)
                    {
                        dr["ID"] = Convert.ToInt32(lbl.Text);
                        dr["FieldName"] = fieldname;
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;



        }
        private DataTable TreatmentLab(DataTable dtprescompl, string name)
        {
            GridView gdview = (GridView)UCTreatment.FindControl("gvtreatment");
            foreach (GridViewRow row in gdview.Rows)
            {
                DataRow dr = dtprescompl.NewRow();
                CheckBox chk = (CheckBox)row.FindControl("Chktreatment");
                Label lbl = (Label)row.FindControl("lbltreatment");
                if (chk.Checked)
                {
                    dr["ID"] = Convert.ToInt32(lbl.Text);
                    dr["FieldName"] = name;

                    dtprescompl.Rows.Add(dr);
                }

            }
            return dtprescompl;

        }
        private DataTable EnrollIn(DataTable dtprescompl, string name)
        {
            GridView gdview = (GridView)UCTreatment.FindControl("gvenrollin");
            foreach (GridViewRow row in gdview.Rows)
            {
                DataRow dr = dtprescompl.NewRow();
                CheckBox chk = (CheckBox)row.FindControl("Chkenrollin");
                Label lbl = (Label)row.FindControl("lblenrollin");
                if (chk.Checked)
                {
                    dr["ID"] = Convert.ToInt32(lbl.Text);
                    dr["FieldName"] = name;

                    dtprescompl.Rows.Add(dr);
                }

            }
            return dtprescompl;

        }

        private DataTable PriorART()
        {
            DataTable tbl = CreateTempTable();
            if (Session["PriorGridData"] != null)
            {
                DataTable Gridtbl = (DataTable)Session["PriorGridData"] as DataTable;
                foreach (DataRow row in Gridtbl.Rows)
                {
                    DataRow dr;
                    dr = tbl.NewRow();
                    dr["DateField1"] = row["DurationFromDate"];
                    dr["DateField2"] = row["DurationToDate"];
                    dr["ID"] = row["FacilityId"];
                    dr["NumericField"] = row["EntrytypeId"];
                    tbl.Rows.Add(dr);
                }
            }
            return tbl;

        }
        private void bindDisclosure(DataTable thedt)
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable dt = new DataTable();
            string script = string.Empty;
            if (thedt.Rows.Count > 0)
            {
                DataView theDV = new DataView(thedt);
                theDV.RowFilter = "FieldName='NigeriaHIVDisclosure'";
                dt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                {
                    for (int i = 0; i < this.UcAdherence.gvdisclosed.Rows.Count; i++)
                    {
                        Label lbldisclosed = (Label)UcAdherence.gvdisclosed.Rows[i].FindControl("lbldisclosed");
                        CheckBox Chkdisclosed = (CheckBox)UcAdherence.gvdisclosed.Rows[i].FindControl("Chkdisclosed");

                        if (Convert.ToInt32(dt.Rows[j]["ValueId"]) == Convert.ToInt32(lbldisclosed.Text))
                        {
                            if (dt.Rows[j]["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("DivDiscloseOther", "show");
                            }
                            Chkdisclosed.Checked = true;

                            if (dt.Rows[j]["Name"].ToString().ToUpper() == "NO ONE")
                            {
                                String pcscript = "";
                                pcscript = "<script language = 'javascript' defer ='defer' id = 'toggleDisclosedPC'>\n";
                                pcscript += "toggleDisclosedPC('" + Chkdisclosed.ClientID + "')\n";
                                pcscript += "</script>\n";
                                RegisterStartupScript("'toggleDisclosedPC'", pcscript);
                            }
                        }

                    }
                }
            }
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
                theDV.Sort = "SRNo ASC";
                thedeCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            }

            if (thedeCodeDT.Rows.Count > 0)
            {
                BindManager.BindCheckedList(chklst, thedeCodeDT, "Name", "ID");
            }

            theDV = new DataView(theDSXML.Tables["MST_DECODE"]);

            if (theDV.Table.Rows.Count > 0)
            {

                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=17 and ModuleId=209";
                theDV.Sort = "SRNo ASC";
                thedeCodeDT = new DataTable();
                thedeCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            }

            if (thedeCodeDT.Rows.Count > 0)
            {
                BindManager.BindCombo(ddlreferredfrom, thedeCodeDT, "Name", "ID");
            }


        }
        public void Save(int dqchk)
        {
            int tabindex = 0;
            string savetabname = tabControl.ActiveTab.HeaderText.ToString();
            if (fieldValidation(savetabname) == false)
            {
                //ErrorLoad();
                return;
            }
            Hashtable theHT = HtParameters(savetabname);
            GetCheckBoxListData(savetabname);
            string tabname = string.Empty;
            DataSet DsReturns = new DataSet();
            NigAdultIE = (INigeriaARTCard)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNigeriaARTCard, BusinessProcess.Clinical");
            if (savetabname == "Clinical History" || tabControl.ActiveTabIndex == 0)
            {
                savetabname = "Clinical History";
                DsReturns = NigAdultIE.SaveUpdateNigeriaAdultIEClinicalHistoryData(theHT, dtmuiltselect, dqchk, Convert.ToInt32(Session["AppUserId"]));
                tabindex = 1;
                
            }
            else if (savetabname == "HIV History" || tabControl.ActiveTabIndex == 1)
            {
                savetabname = "HIV History";
                DataTable dtPrior = PriorART();
                DsReturns = NigAdultIE.SaveUpdateNigeriaAdultIEHIVHistoryData(theHT, dtmuiltselect, dqchk, Convert.ToInt32(Session["AppUserId"]), dtPrior);
                tabindex = 2;
               
            }
            else if (savetabname == "Examination" || tabControl.ActiveTabIndex == 2)
            {
                DsReturns = NigAdultIE.SaveUpdateNigeriaAdultIEExaminationData(theHT, dtmuiltselect, dqchk, Convert.ToInt32(Session["AppUserId"]));
                tabindex = 3;
               
            }
            else if (savetabname == "Management" || tabControl.ActiveTabIndex == 3)
            {
                DsReturns = NigAdultIE.SaveUpdateNigeriaAdultIEManagementData(theHT, dtmuiltselect, dqchk, Convert.ToInt32(Session["AppUserId"]));
                tabindex = 4;
            }
            
            Session["Redirect"] = "0";
            if (Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]) > 0)
            {
                Session["PatientVisitId"] = Convert.ToInt32(DsReturns.Tables[0].Rows[0]["Visit_Id"]);
                SaveCancel(savetabname);
                checkIfPreviuosTabSaved();
                if (tabindex < 4)
                {
                    tabControl.ActiveTabIndex = tabindex;
                }
            }
        }
        public void checkIfPreviuosTabSaved()
        {
            KNHStatic = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");


            DataSet dsClinical = new DataSet();
            dsClinical = KNHStatic.CheckIfPreviuosTabSaved("NigAdultIEClinicalHis", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(dsClinical, btnHIVHistorySave, btnHIVHistoryPrint);

            DataSet dsHIV = new DataSet();
            dsHIV = KNHStatic.CheckIfPreviuosTabSaved("NigAdultIEHIVHis", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(dsHIV, btnExaminationSave, btnExaminationPrint);

            DataSet dsExam = new DataSet();
            dsExam = KNHStatic.CheckIfPreviuosTabSaved("NigAdultIEExam", Convert.ToInt32(Session["PatientVisitId"]));
            buttonEnabledAndDisabled(dsExam, btnSaveMgt, btnPrintMgt);


            //For Signature
            IPatientKNHPEP KNHPEP;
            KNHPEP = (IPatientKNHPEP)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientKNHPEP, BusinessProcess.Clinical");
            DataTable dtSignature = KNHPEP.GetSignature(Convert.ToInt32(ApplicationAccess.NigeriaAdultIE), Convert.ToInt32(Session["PatientVisitId"]));
            if (dtSignature.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSignature.Rows)
                {
                    if (dr["TabName"].ToString() == "NigAdultIEClinicalHis")
                        this.UserControlKNH_SignatureCH.lblSignature.Text = dr["UserName"].ToString();
                    if (dr["TabName"].ToString() == "NigAdultIEHIVHis")
                        this.UserControlKNH_SignatureHH.lblSignature.Text = dr["UserName"].ToString();
                    if (dr["TabName"].ToString() == "NigAdultIEExam")
                        this.UserControlKNH_SignatureExamination.lblSignature.Text = dr["UserName"].ToString();
                    if (dr["TabName"].ToString() == "NigAdultIEMgt")
                        this.UserControlKNH_SignatureMgt.lblSignature.Text = dr["UserName"].ToString();
                }
            }
            dtSignature.Dispose();

            dsClinical.Dispose();
            dsHIV.Dispose();
            dsExam.Dispose();
            //dsMgt.Dispose();

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
            DataTable thePEPDS = KNHPEP.GetTabID(260);
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable thedt = new DataTable();
            DataView theDV = new DataView(thePEPDS);
            theDV.RowFilter = "TabName='NigAdultIEClinicalHis'";
            thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            AuthenticationManager Authentication = new AuthenticationManager();
            //Clinical History
            if (thedt.Rows.Count > 0)
            {
                Authentication.TabUserRights(btnClinicalHistorySave, btnClinicalHistoryPrint, 260, Convert.ToInt32(thedt.Rows[0]["TabId"]));
            }

            //HIV HIstory
            thedt = new DataTable();
            theDV = new DataView(thePEPDS);
            theDV.RowFilter = "TabName='NigAdultIEHIVHis'";
            thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            Authentication.TabUserRights(btnHIVHistorySave, btnHIVHistoryPrint, 260, Convert.ToInt32(thedt.Rows[0]["TabId"]));

            //Examination
            thedt = new DataTable();
            theDV = new DataView(thePEPDS);
            theDV.RowFilter = "TabName='NigAdultIEExam'";
            thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            Authentication.TabUserRights(btnExaminationSave, btnExaminationPrint, 260, Convert.ToInt32(thedt.Rows[0]["TabId"]));

            //Management
            thedt = new DataTable();
            theDV = new DataView(thePEPDS);
            theDV.RowFilter = "TabName='NigAdultIEMgt'";
            thedt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            Authentication.TabUserRights(btnSaveMgt, btnPrintMgt, 260, Convert.ToInt32(thedt.Rows[0]["TabId"]));
           
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
            txtVisitDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtVisitDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtlatestViralLoadDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtlatestViralLoadDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtlbllowestCD4Date.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtlbllowestCD4Date.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtLMPdate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtLMPdate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtEDDDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtEDDDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtlatestcd4number.Attributes.Add("onkeyup", "chkDecimal('" + txtlatestcd4number.ClientID + "')");
            txtlatestViralLoadCount.Attributes.Add("onkeyup", "chkDecimal('" + txtlatestViralLoadCount.ClientID + "')");
            txtlowestCD4Count.Attributes.Add("onkeyup", "chkDecimal('" + txtlowestCD4Count.ClientID + "')");
            txtgestage.Attributes.Add("onkeyup", "chkDecimal('" + txtgestage.ClientID + "')");
            this.idVitalSign.txtHeight.Attributes.Add("onkeyup", "chkDecimal('" + this.idVitalSign.txtHeight.ClientID + "')");
            this.idVitalSign.txtWeight.Attributes.Add("onkeyup", "chkDecimal('" + this.idVitalSign.txtWeight.ClientID + "')");
            this.idVitalSign.txtBPSystolic.Attributes.Add("onkeyup", "chkDecimal('" + this.idVitalSign.txtBPSystolic.ClientID + "')");
            this.idVitalSign.txtBPDiastolic.Attributes.Add("onkeyup", "chkDecimal('" + this.idVitalSign.txtBPDiastolic.ClientID + "')");
            this.idVitalSign.txtRR.Attributes.Add("onkeyup", "chkDecimal('" + this.idVitalSign.txtRR.ClientID + "')");
            if (Session["PatientSex"] != DBNull.Value)
            {
                if (Session["PatientSex"].ToString().ToUpper() == "MALE")
                {
                    visibleDiv(pnlpregnantDetail.ClientID, "hide");
                }
            }

        }
        //Checking all the required values 
        private Boolean fieldValidation(string TabName)
        {
            IQCareUtils theUtil = new IQCareUtils();
            MsgBuilder totalMsgBuilder = new MsgBuilder();

            if (txtVisitDate.Value.Trim() == "")
            {

                totalMsgBuilder.DataElements["MessageText"] = "Enter Visit Date";
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                return false;

            }
            
            if (TabName == "Examination" || TabName == "TabPanelExamination")
            {
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
                if (this.UcWhostaging.ddlwhostage1.SelectedIndex == 0)
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
            }
            else if (TabName == "Management" || TabName == "Tabmanagement")
            {
                if (this.UCTreatment.ddlTreatmentplan.SelectedIndex == 0)
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Treatment Plan";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    UCTreatment.lblTreatmentplan.ForeColor = Color.Red;
                    lblheadregimenpresc.ForeColor = Color.Red;
                    return false;
                }
                else
                {
                    UCTreatment.lblTreatmentplan.ForeColor = Color.FromArgb(0, 0, 142);
                    lblheadregimenpresc.ForeColor = Color.FromArgb(0, 0, 142);
                }
            }

            return true;

        }
        //private void SaveCancel()
        //{
        //    int PatientID = Convert.ToInt32(Session["PatientId"]);
        //    IQCareMsgBox.NotifyAction("Adult Initial Evaluation Form saved successfully. Do you want to close?", "Adult Initial Evaluation Form", false, this, "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';");
        //}
        private void SaveCancel(string tabname)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            IQCareMsgBox.NotifyAction(tabname + " Tab saved successfully.", "Adult Initial Evaluation", false, this, "");
        }

        protected void btnSaveTriage_Click(object sender, EventArgs e)
        {
            Save(0);
        }
        private void visibleDiv(String divId, string showhide)
        {
            String script = "";
            script = "<script language = 'javascript' defer ='defer' id = '" + divId + "'>\n";
            script += "ShowHide('" + divId + "','" + showhide + "');\n";
            script += "</script>\n";
            RegisterStartupScript("'" + divId + "'", script);
        }
        public void BindExistingData()
        {
                string script = string.Empty;

                if (Convert.ToInt32(Session["PatientVisitId"].ToString()) > 0)
                {
                    NigAdultIE = (INigeriaARTCard)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNigeriaARTCard, BusinessProcess.Clinical");
                    DataSet dsGet = NigAdultIE.GetNigeriaAdultIEDetails(Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["PatientVisitId"].ToString()));
                    if (dsGet.Tables[0].Rows.Count > 0)
                    {
                        //------------------------------section client information

                        if (dsGet.Tables[0].Rows[0]["VisitDate"] != DBNull.Value)
                        {
                            txtVisitDate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["VisitDate"]);
                        }
                        bindPresentingComplaint(dsGet.Tables[1]);
                        if (dsGet.Tables[0].Rows[0]["OtherPresentingComplaints"] != DBNull.Value)
                            UcPc.txtOtherPresentingComplaints.Text = dsGet.Tables[0].Rows[0]["OtherPresentingComplaints"].ToString();
                        if (dsGet.Tables[0].Rows[0]["PresentingComplaintsAdditionalNotes"] != DBNull.Value)
                            UcPc.txtAdditionalComplaints.Text = dsGet.Tables[0].Rows[0]["PresentingComplaintsAdditionalNotes"].ToString();
                        FillCheckboxlist(cblRiskFactors, dsGet.Tables[1], "RiskFactors");
                        bindMedicalHistory(dsGet.Tables[1]);
                        if (dsGet.Tables[0].Rows[0]["PastMedicalConditionNotes"] != DBNull.Value)
                            idNigeriaMedical.txtlastmedical.Text = dsGet.Tables[0].Rows[0]["PastMedicalConditionNotes"].ToString();
                        if (dsGet.Tables[0].Rows[0]["RelevantFamilyHistoryNotes"] != DBNull.Value)
                            idNigeriaMedical.txtfamhistory.Text = dsGet.Tables[0].Rows[0]["RelevantFamilyHistoryNotes"].ToString();
                        if (dsGet.Tables[0].Rows[0]["HospitalisationNotes"] != DBNull.Value)
                            idNigeriaMedical.txthospitalization.Text = dsGet.Tables[0].Rows[0]["HospitalisationNotes"].ToString();
                        FillCheckboxlist(cblRiskFactors, dsGet.Tables[1], "RiskFactors");
                        if (dsGet.Tables[4].Rows.Count > 0)
                        {
                            if (Convert.ToDateTime(dsGet.Tables[4].Rows[0]["LMP"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                                txtLMPdate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[4].Rows[0]["LMP"]);
                            if (dsGet.Tables[4].Rows[0]["Pregnant"] != System.DBNull.Value)
                            {
                                if (dsGet.Tables[4].Rows[0]["Pregnant"].ToString() == "1")
                                {
                                    this.rdopregnantyesno.SelectedValue = "1";
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divhideFPyes'>\n";
                                    script += "ShowHide('hideFP','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divhideFPyes", script);

                                }
                                else if (dsGet.Tables[4].Rows[0]["Pregnant"].ToString() == "0")
                                {
                                    this.rdopregnantyesno.SelectedValue = "0";
                                }
                            }
                            if (dsGet.Tables[4].Rows[0]["EDD"] != System.DBNull.Value)
                            {
                                if (Convert.ToDateTime(dsGet.Tables[4].Rows[0]["EDD"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                                    txtLMPdate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[4].Rows[0]["EDD"]);
                            }
                            txtgestage.Text = dsGet.Tables[4].Rows[0]["Gestationalage"].ToString();
                        }
                        txtlatestcd4number.Text = dsGet.Tables[0].Rows[0]["LatestCD4"].ToString();
                        txtlowestCD4Count.Text = dsGet.Tables[0].Rows[0]["LowestCD4"].ToString();
                        if (dsGet.Tables[0].Rows[0]["LowestCD4Date"] != System.DBNull.Value)
                        {
                            if (Convert.ToDateTime(dsGet.Tables[0].Rows[0]["LowestCD4Date"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                                txtlbllowestCD4Date.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["LowestCD4Date"]);
                        }
                        if (dsGet.Tables[0].Rows[0]["LatestCD4Date"] != System.DBNull.Value)
                        {
                            if (Convert.ToDateTime(dsGet.Tables[0].Rows[0]["LatestCD4Date"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                                dtlatestcd4date.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["LatestCD4Date"]);
                        }
                        txtlatestViralLoadCount.Text = dsGet.Tables[0].Rows[0]["LowestViralLoad"].ToString();
                        if (dsGet.Tables[0].Rows[0]["LowestViralLoadDate"] != System.DBNull.Value)
                        {
                            if (Convert.ToDateTime(dsGet.Tables[0].Rows[0]["LowestViralLoadDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                                txtlatestViralLoadDate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[0].Rows[0]["LowestViralLoadDate"]);
                        }
                        if (dsGet.Tables[0].Rows[0]["LowestCD4LastSeen"] != DBNull.Value)
                        {
                            if (dsGet.Tables[0].Rows[0]["LowestCD4LastSeen"].ToString() == "1")
                                chklowestcd4labrecord.Checked = true;
                        }
                        if (dsGet.Tables[0].Rows[0]["LowestViralLoadLastseen"] != DBNull.Value)
                        {
                            if (dsGet.Tables[0].Rows[0]["LowestViralLoadLastseen"].ToString() == "1")
                                chkviralloadlabrecord.Checked = true;
                        }
                        bindCurrentMedication(dsGet.Tables[1]);
                        UcCurrentMed.txtOtherComplaints.Text = dsGet.Tables[0].Rows[0]["currentmedicationother"].ToString();
                        bindDisclosure(dsGet.Tables[1]);
                        if (dsGet.Tables[0].Rows[0]["serviceentry"] != DBNull.Value)
                            ddlreferredfrom.SelectedValue = dsGet.Tables[0].Rows[0]["serviceentry"].ToString();
                        if (dsGet.Tables[7].Rows.Count > 0)
                        {
                            if (dsGet.Tables[7].Rows[0]["ARVAdhere"] != System.DBNull.Value)
                            {
                                if (dsGet.Tables[7].Rows[0]["ARVAdhere"].ToString() == "1")
                                {
                                    UcAdherence.rbladherenceYesNo.SelectedValue = "1";


                                }
                                else if (dsGet.Tables[7].Rows[0]["ARVAdhere"].ToString() == "0")
                                {
                                    UcAdherence.rbladherenceYesNo.SelectedValue = "0";
                                }
                            }

                            if (dsGet.Tables[7].Rows[0]["MissedARV3days"] != System.DBNull.Value)
                            {
                                if (dsGet.Tables[7].Rows[0]["MissedARV3days"].ToString() == "1")
                                {
                                    UcAdherence.rblmissedarvYesNo.SelectedValue = "1";
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'DIVmissedarvPyes'>\n";
                                    script += "ShowHide('DIVmissedarv','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("DIVmissedarvyes", script);

                                }
                                else if (dsGet.Tables[7].Rows[0]["MissedARV3days"].ToString() == "0")
                                {
                                    UcAdherence.rblmissedarvYesNo.SelectedValue = "0";
                                }
                            }
                            UcAdherence.ddlReasomMissed.SelectedValue = dsGet.Tables[7].Rows[0]["MissedReason"].ToString();
                            if (dsGet.Tables[7].Rows[0]["TreatmentInterrupted"] != System.DBNull.Value)
                            {
                                if (dsGet.Tables[7].Rows[0]["TreatmentInterrupted"].ToString() == "1")
                                {
                                    UcAdherence.rdoTreatmentIntrupted.SelectedValue = "1";
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'DIVInturptedReasonyes'>\n";
                                    script += "ShowHide('DIVInturptedReason','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("DIVInturptedReasonyes", script);

                                }
                                else if (dsGet.Tables[7].Rows[0]["TreatmentInterrupted"].ToString() == "0")
                                {
                                    UcAdherence.rdoTreatmentIntrupted.SelectedValue = "0";
                                }
                            }
                            if (Convert.ToDateTime(dsGet.Tables[7].Rows[0]["InterruptedDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                                UcAdherence.txtdtIntrupptedDate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[7].Rows[0]["InterruptedDate"]);
                            UcAdherence.txtintrpdays.Text = dsGet.Tables[7].Rows[0]["InterruptedNumDays"].ToString();
                            //UcAdherence.ddlreasonInterrupted.SelectedValue = dsGet.Tables[7].Rows[0]["InterruptedNumDays"].ToString();
                            if (dsGet.Tables[7].Rows[0]["TreatmentStopped"] != System.DBNull.Value)
                            {
                                if (dsGet.Tables[7].Rows[0]["TreatmentStopped"].ToString() == "1")
                                {
                                    UcAdherence.rblstopped.SelectedValue = "1";
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'DIVStopedReasonyes'>\n";
                                    script += "ShowHide('DIVStopedReason','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("DIVStopedReasonyes", script);

                                }
                                else if (dsGet.Tables[7].Rows[0]["TreatmentStopped"].ToString() == "0")
                                {
                                    UcAdherence.rblstopped.SelectedValue = "0";
                                }
                            }
                            if (Convert.ToDateTime(dsGet.Tables[7].Rows[0]["StoppedDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                                UcAdherence.txtStopedReasonDate.Value = String.Format("{0:dd-MMM-yyyy}", dsGet.Tables[7].Rows[0]["StoppedDate"]);
                            UcAdherence.txtstoppeddays.Text = dsGet.Tables[7].Rows[0]["StoppedNumDays"].ToString();
                            UcAdherence.ddlStopedReason.SelectedValue = dsGet.Tables[7].Rows[0]["StoppedReason"].ToString();

                            UcAdherence.txtOtherdisclosed.Text = dsGet.Tables[0].Rows[0]["Otherdisclosed"].ToString();
                            UcAdherence.txthivdiscussed.Text = dsGet.Tables[0].Rows[0]["HIVStatusDiscussedwith"].ToString();
                            if (dsGet.Tables[7].Rows[0]["supportgroup"] != System.DBNull.Value)
                            {
                                if (dsGet.Tables[7].Rows[0]["supportgroup"].ToString() == "1")
                                {
                                    UcAdherence.rblsupportgroup.SelectedValue = "1";

                                }
                                else if (dsGet.Tables[7].Rows[0]["supportgroup"].ToString() == "0")
                                {
                                    UcAdherence.rblsupportgroup.SelectedValue = "0";
                                }
                            }
                        }
                        
                        FillCheckboxlist(cblShortTermEffects, dsGet.Tables[1], "ShortTermEffects");
                        FillCheckboxlist(cblLongTermEffects, dsGet.Tables[1], "LongTermEffects");
                        txtOtherShortTermEffects.Text = dsGet.Tables[0].Rows[0]["OtherShortARVSideEffects"].ToString();
                        txtOtherLongtermEffects.Text = dsGet.Tables[0].Rows[0]["OtherLongARVSideEffects"].ToString();
                        if (dsGet.Tables[3].Rows.Count > 0)
                        {
                            if (dsGet.Tables[3].Rows[0]["Temp"] != DBNull.Value)
                                this.idVitalSign.txtTemp.Text = dsGet.Tables[3].Rows[0]["Temp"].ToString();
                            if (dsGet.Tables[3].Rows[0]["RR"] != DBNull.Value)
                                this.idVitalSign.txtRR.Text = dsGet.Tables[3].Rows[0]["RR"].ToString();                            
                            if (dsGet.Tables[3].Rows[0]["BPDiastolic"] != DBNull.Value)
                                this.idVitalSign.txtBPDiastolic.Text = dsGet.Tables[3].Rows[0]["BPDiastolic"].ToString();
                            if (dsGet.Tables[3].Rows[0]["BPSystolic"] != DBNull.Value)
                                this.idVitalSign.txtBPSystolic.Text = dsGet.Tables[3].Rows[0]["BPSystolic"].ToString();
                            if (dsGet.Tables[3].Rows[0]["Height"] != DBNull.Value)
                                this.idVitalSign.txtHeight.Text = dsGet.Tables[3].Rows[0]["Height"].ToString();
                            if (dsGet.Tables[3].Rows[0]["Weight"] != DBNull.Value)
                                this.idVitalSign.txtWeight.Text = dsGet.Tables[3].Rows[0]["Weight"].ToString();  
                            if (dsGet.Tables[3].Rows[0]["BMIz"] != DBNull.Value)
                                this.idVitalSign.txtBMI.Text = dsGet.Tables[3].Rows[0]["BMIz"].ToString();                            
                        }
                        
                        FillCheckboxlist(UcPE.cblNigeriaPEGeneral, dsGet.Tables[1], "NigeriaPEGeneral");
                        FillCheckboxlist(UcPE.cblNigeriaPESkin, dsGet.Tables[1], "NigeriaPESkin");
                        FillCheckboxlist(UcPE.cblNigeriaPEHeadEyeEnt, dsGet.Tables[1], "NigeriaPEHeadEyeEnt");
                        FillCheckboxlist(UcPE.cblNigeriaPECardiovascular, dsGet.Tables[1], "NigeriaPECardiovascular");                        
                        FillCheckboxlist(UcPE.cblNigeriaPEBreast, dsGet.Tables[1], "NigeriaPEBreast");
                        FillCheckboxlist(UcPE.cblNigeriaPEGenitalia, dsGet.Tables[1], "NigeriaPEGenitalia");
                        FillCheckboxlist(UcPE.cblNigeriaPERespiratory, dsGet.Tables[1], "NigeriaPERespiratory");
                        FillCheckboxlist(UcPE.cblNigeriaPEGastrointestinal, dsGet.Tables[1], "NigeriaPEGastrointestinal");
                        FillCheckboxlist(UcPE.cblNigeriaPENeurological, dsGet.Tables[1], "NigeriaPENeurological");
                        FillCheckboxlist(UcPE.cblNigeriaPEMentalstatus, dsGet.Tables[1], "NigeriaPEMentalstatus");

                        UcPE.txtOtherNigeriaPEGeneral.Text = dsGet.Tables[0].Rows[0]["OtherGeneralConditions"].ToString();
                        UcPE.txtOtherNigeriaPESkin.Text = dsGet.Tables[0].Rows[0]["OtherSkinConditions"].ToString();
                        UcPE.txtOtherNigeriaPEHeadEyeEnt.Text = dsGet.Tables[0].Rows[0]["HeadEyeENTOther"].ToString();
                        UcPE.txtOtherNigeriaPECardiovascular.Text = dsGet.Tables[0].Rows[0]["OtherCardiovascularConditions"].ToString();
                        UcPE.txtOtherNigeriaPEBreast.Text = dsGet.Tables[0].Rows[0]["BreastsOther"].ToString();
                        UcPE.txtOtherNigeriaPEGenitalia.Text = dsGet.Tables[0].Rows[0]["Genitaliaother"].ToString();
                        UcPE.txtOtherNigeriaPERespiratory.Text = dsGet.Tables[0].Rows[0]["Respiratoryother"].ToString();
                        UcPE.txtOtherNigeriaPEGastrointestinal.Text = dsGet.Tables[0].Rows[0]["Gastrointestinalother"].ToString();
                        UcPE.txtOtherNigeriaPENeurological.Text = dsGet.Tables[0].Rows[0]["Neurologicalother"].ToString();
                        UcPE.txtOtherNigeriaPEMentalstatus.Text = dsGet.Tables[0].Rows[0]["Mentalstatusother"].ToString();
                        UcPE.txtOtherAdditionaldetailedfindings.Text = dsGet.Tables[0].Rows[0]["PEAdditionaldetails"].ToString();
                        if (dsGet.Tables[10].Rows.Count > 0)
                        {
                            if (dsGet.Tables[10].Rows[0]["AssessmentID"] != System.DBNull.Value)
                            {
                                rblassessment.SelectedValue = dsGet.Tables[10].Rows[0]["AssessmentID"].ToString();
                            }
                            txtAssessmentNotes.Text = dsGet.Tables[10].Rows[0]["Description1"].ToString();
                        }

                        BindWHOListData(dsGet.Tables[1]);
                        if (dsGet.Tables[2].Rows.Count > 0)
                        {
                            if (dsGet.Tables[2].Rows[0]["WHOStage"] != DBNull.Value)
                                UcWhostaging.ddlwhostage1.SelectedValue = dsGet.Tables[2].Rows[0]["WHOStage"].ToString();
                            
                        }
                        if (dsGet.Tables[0].Rows[0]["LabEvaluation"] != System.DBNull.Value)
                        {
                            if (dsGet.Tables[0].Rows[0]["LabEvaluation"].ToString() == "1")
                            {
                                UCTreatment.ChkLabEvaluation.Checked = true;
                                script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'DivLabEvalyes'>\n";
                                script += "ShowHide('DivLabEval','show');\n";
                                script += "</script>\n";
                                RegisterStartupScript("DivLabEvalyes", script);

                            }
                            else if (dsGet.Tables[0].Rows[0]["LabEvaluation"].ToString() == "0")
                            {
                                UCTreatment.ChkLabEvaluation.Checked = false;
                            }
                        }
                        UCTreatment.UcLabEval.txtlabdiagnosticinput.Text = dsGet.Tables[0].Rows[0]["LabReview"].ToString(); 
                        if (dsGet.Tables[0].Rows[0]["Regimen"] != System.DBNull.Value)
                        {
                            if (dsGet.Tables[0].Rows[0]["Regimen"].ToString() == "1")
                            {
                                UCTreatment.chkregimen.Checked = true;
                                script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'DivPrescDrugyes'>\n";
                                script += "ShowHide('DivPrescDrug','show');\n";
                                script += "</script>\n";
                                RegisterStartupScript("DivPrescDrugyes", script);

                            }
                            else if (dsGet.Tables[0].Rows[0]["Regimen"].ToString() == "0")
                            {
                                UCTreatment.ChkLabEvaluation.Checked = false;
                            }
                        }
                        bindTreatment(dsGet.Tables[1]);
                        bindEnrollIn(dsGet.Tables[1]);
                        FillCheckboxlist(UCTreatment.chklistARTchangecode, dsGet.Tables[1], "NigeriaARVTreamentChangeReason");                        
                        if (dsGet.Tables[8].Rows.Count > 0)
                        {
                            if (dsGet.Tables[8].Rows[0]["TherapyReasonCode"] != DBNull.Value)
                                UCTreatment.ddlTreatmentplan.SelectedValue = dsGet.Tables[8].Rows[0]["TherapyReasonCode"].ToString();
                            UCTreatment.txtSpecifyotherARTchangereason.Text = dsGet.Tables[8].Rows[0]["specifyOtherARTChangeReason"].ToString();
                            if (UCTreatment.ddlTreatmentplan.SelectedItem.Text == "Change Treatment")
                            {
                                visibleDiv("divARTchangecode","show");
                            }

                        }
                    }
                }
            
        }
        private void BindWHOListData(DataTable theDT)
        {
            foreach (DataRow theDR in theDT.Rows)
            {

                if (Convert.ToString(theDR["FieldName"]) == "NigeriaWHOStageIConditions")
                {
                    for (int i = 0; i < this.UcWhostaging.gvWHO1.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UcWhostaging.gvWHO1.Rows[i].FindControl("lblwho1");
                        CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO1.Rows[i].FindControl("Chkwho1");                        
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "NigeriaWHOStageIIConditions")
                {
                    for (int i = 0; i < this.UcWhostaging.gvWHO2.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UcWhostaging.gvWHO2.Rows[i].FindControl("lblwho2");
                        CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO2.Rows[i].FindControl("Chkwho2");
                        
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "NigeriaWHOStageIIIConditions")
                {
                    for (int i = 0; i < this.UcWhostaging.gvWHO3.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UcWhostaging.gvWHO3.Rows[i].FindControl("lblwho3");
                        CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO3.Rows[i].FindControl("Chkwho3");                        
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;                            
                        }
                    }
                }

                else if (Convert.ToString(theDR["FieldName"]) == "NigeriaWHOStageIVConditions")
                {
                    for (int i = 0; i < this.UcWhostaging.gvWHO4.Rows.Count; i++)
                    {
                        Label lblWHOId = (Label)UcWhostaging.gvWHO4.Rows[i].FindControl("lblwho4");
                        CheckBox chkWHOId = (CheckBox)UcWhostaging.gvWHO4.Rows[i].FindControl("Chkwho4");
                        
                        if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                        {
                            chkWHOId.Checked = true;
                            
                        }
                    }
                }

            }

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
                            if (name == "ShortTermEffects")
                            {
                                if (chk.Items[j].Text.ToLower() == "other specify")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divotherShortTermEffects'>\n";
                                    script += "ShowHide('divshorttermeffecttxt','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divotherShortTermEffects", script);
                                }
                            }
                            if (name == "LongTermEffects")
                            {
                                if (chk.Items[j].Text.ToLower() == "other specify")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divotherLongTermEffects'>\n";
                                    script += "ShowHide('divlongtermeffecttxt','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divotherLongTermEffects", script);
                                }
                            }                            
                            if (name == "NigeriaPEGeneral")
                            {
                                if (chk.Items[j].Text.ToUpper() == "NSF")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "NigeriaPEGeneral", "$('#" + chk.ClientID + "').trigger('onchange');", true);
                                }
                                else if (chk.Items[j].Text == "Other (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divNigeriaPEGeneralOther'>\n";
                                    script += "ShowHide('divNigeriaPEGeneralOther','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divNigeriaPEGeneralOther", script);
                                }
                            }
                            if (name == "NigeriaPESkin")
                            {
                                if (chk.Items[j].Text.ToUpper() == "NSF")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "NigeriaPESkin", "$('#" + chk.ClientID + "').trigger('onchange');", true);
                                }
                                else if (chk.Items[j].Text == "Other (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherNigeriaPESkin'>\n";
                                    script += "ShowHide('divOtherNigeriaPESkin','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherNigeriaPESkin", script);
                                }
                            }

                            if (name == "NigeriaPEHeadEyeEnt")
                            {
                                if (chk.Items[j].Text.ToUpper() == "NSF")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "NigeriaPEHeadEyeEnt", "$('#" + chk.ClientID + "').trigger('onchange');", true);
                                }
                                else if (chk.Items[j].Text == "Other (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherNigeriaPEHeadEyeEnt'>\n";
                                    script += "ShowHide('divOtherNigeriaPEHeadEyeEnt','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherNigeriaPEHeadEyeEnt", script);
                                }
                            }
                            if (name == "NigeriaPECardiovascular")
                            {
                                if (chk.Items[j].Text.ToUpper() == "NSF")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "NigeriaPECardiovascular", "$('#" + chk.ClientID + "').trigger('onchange');", true);
                                }
                                else if (chk.Items[j].Text.ToLower() == "other (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherNigeriaPECardiovascular'>\n";
                                    script += "ShowHide('divOtherNigeriaPECardiovascular','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherNigeriaPECardiovascular", script);
                                }
                            }
                            if (name == "NigeriaPEBreast")
                            {
                                if (chk.Items[j].Text.ToUpper() == "NSF")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "NigeriaPEBreast", "$('#" + chk.ClientID + "').trigger('onchange');", true);
                                }
                                else if (chk.Items[j].Text == "Other (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherNigeriaPEBreast'>\n";
                                    script += "ShowHide('divOtherNigeriaPEBreast','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherNigeriaPEBreast", script);
                                }
                            }
                            if (name == "NigeriaPEGenitalia")
                            {
                                if (chk.Items[j].Text.ToUpper() == "NSF")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "NigeriaPEGenitalia", "$('#" + chk.ClientID + "').trigger('onchange');", true);
                                }
                                else if (chk.Items[j].Text == "Other (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherNigeriaPEGenitalia'>\n";
                                    script += "ShowHide('divOtherNigeriaPEGenitalia','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherNigeriaPEGenitalia", script);
                                }
                            }

                            if (name == "NigeriaPERespiratory")
                            {
                                if (chk.Items[j].Text.ToUpper() == "NSF")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "NigeriaPERespiratory", "$('#" + chk.ClientID + "').trigger('onchange');", true);
                                }
                                else if (chk.Items[j].Text == "Other (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherNigeriaPERespiratory'>\n";
                                    script += "ShowHide('divOtherNigeriaPERespiratory','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherNigeriaPERespiratory", script);
                                }
                            }
                            if (name == "NigeriaPEGastrointestinal")
                            {
                                if (chk.Items[j].Text.ToUpper() == "NSF")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "NigeriaPEGastrointestinal", "$('#" + chk.ClientID + "').trigger('onchange');", true);
                                }
                                else if (chk.Items[j].Text.ToLower() == "other (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherNigeriaPEGastrointestinal'>\n";
                                    script += "ShowHide('divOtherNigeriaPEGastrointestinal','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherNigeriaPEGastrointestinal", script);
                                }
                            }
                            if (name == "NigeriaPENeurological")
                            {
                                if (chk.Items[j].Text.ToUpper() == "NSF")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "NigeriaPENeurological", "$('#" + chk.ClientID + "').trigger('onchange');", true);
                                }
                                else if (chk.Items[j].Text == "Other (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherNigeriaPENeurological'>\n";
                                    script += "ShowHide('divOtherNigeriaPENeurological','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherNigeriaPENeurological", script);
                                }
                            }
                            if (name == "NigeriaPEMentalstatus")
                            {
                                if (chk.Items[j].Text.ToUpper() == "NSF")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "NigeriaPEMentalstatus", "$('#" + chk.ClientID + "').trigger('onchange');", true);
                                }
                                else if (chk.Items[j].Text.ToLower() == "other (specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divOtherNigeriaPEMentalstatus'>\n";
                                    script += "ShowHide('divOtherNigeriaPEMentalstatus','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divOtherNigeriaPEMentalstatus", script);
                                }
                            }
                            if (name == "NigeriaARVTreamentChangeReason")
                            {
                                if (chk.Items[j].Text.ToLower() == "other(specify)")
                                {
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'divSpecifyotherARTchangereason'>\n";
                                    script += "ShowHide('divSpecifyotherARTchangereason','show');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("divSpecifyotherARTchangereason", script);
                                }
                            }
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
                    for (int i = 0; i < this.UcPc.gvPresentingComplaints.Rows.Count; i++)
                    {
                        Label lblPComplaintsId = (Label)UcPc.gvPresentingComplaints.Rows[i].FindControl("lblPresenting");
                        CheckBox chkPComplaints = (CheckBox)UcPc.gvPresentingComplaints.Rows[i].FindControl("ChkPresenting");
                        TextBox txtPComplaints = (TextBox)UcPc.gvPresentingComplaints.Rows[i].FindControl("txtPresenting");
                        if (Convert.ToInt32(dt.Rows[j]["ValueId"]) == Convert.ToInt32(lblPComplaintsId.Text))
                        {
                            if (dt.Rows[j]["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("DivOther","show");
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
        private void bindMedicalHistory(DataTable thedt)
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable dt = new DataTable();
            string script = string.Empty;
            if (thedt.Rows.Count > 0)
            {
                DataView theDV = new DataView(thedt);
                theDV.RowFilter = "FieldName='NigeriaSymptoms'";
                dt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                {
                    for (int i = 0; i < this.idNigeriaMedical.gvMedicalHistory.Rows.Count; i++)
                    {
                        Label lblMedical = (Label)idNigeriaMedical.gvMedicalHistory.Rows[i].FindControl("lblMedical");
                        CheckBox ChkMedical = (CheckBox)idNigeriaMedical.gvMedicalHistory.Rows[i].FindControl("ChkMedical");
                        TextBox txtMedical = (TextBox)idNigeriaMedical.gvMedicalHistory.Rows[i].FindControl("txtMedical");
                        if (Convert.ToInt32(dt.Rows[j]["ValueId"]) == Convert.ToInt32(lblMedical.Text))
                        {
                            if (dt.Rows[j]["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("DivtoggleMedicalPC", "show");
                            }
                            ChkMedical.Checked = true;
                            txtMedical.Text = dt.Rows[j]["Other_notes"].ToString();
                            if (dt.Rows[j]["Name"].ToString().ToLower() == "none")
                            {
                                String pcscript = "";
                                pcscript = "<script language = 'javascript' defer ='defer' id = 'toggleMedicalPC'>\n";
                                pcscript += "toggleMedicalPC('" + ChkMedical.ClientID + "')\n";
                                pcscript += "</script>\n";
                                RegisterStartupScript("'toggleMedicalPC'", pcscript);
                            }
                        }

                    }
                }
            }
        }
        private void bindCurrentMedication(DataTable thedt)
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable dt = new DataTable();
            string script = string.Empty;
            if (thedt.Rows.Count > 0)
            {
                DataView theDV = new DataView(thedt);
                theDV.RowFilter = "FieldName='NigeriaCurrentMedication'";
                dt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                {
                    for (int i = 0; i < this.UcCurrentMed.gvcurrentmedication.Rows.Count; i++)
                    {
                        Label lblmedication = (Label)UcCurrentMed.gvcurrentmedication.Rows[i].FindControl("lblmedication");
                        CheckBox Chkmedication = (CheckBox)UcCurrentMed.gvcurrentmedication.Rows[i].FindControl("Chkmedication");
                        
                        if (Convert.ToInt32(dt.Rows[j]["ValueId"]) == Convert.ToInt32(lblmedication.Text))
                        {
                            if (dt.Rows[j]["Name"].ToString().ToLower() == "other")
                            {
                                visibleDiv("DivOtherComplaint", "show");
                            }
                            Chkmedication.Checked = true;
                            
                            if (dt.Rows[j]["Name"].ToString().ToLower() == "none")
                            {
                                String pcscript = "";
                                pcscript = "<script language = 'javascript' defer ='defer' id = 'toggleCurrentPC'>\n";
                                pcscript += "toggleCurrentPC('" + Chkmedication.ClientID + "')\n";
                                pcscript += "</script>\n";
                                RegisterStartupScript("'toggleCurrentPC'", pcscript);
                            }
                        }

                    }
                }
            }
        }
       
        private void bindTreatment(DataTable thedt)
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable dt = new DataTable();
            string script = string.Empty;
            if (thedt.Rows.Count > 0)
            {
                DataView theDV = new DataView(thedt);
                theDV.RowFilter = "FieldName='NigeriaListLabEvaluation'";
                dt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                {
                    for (int i = 0; i < this.UCTreatment.gvtreatment.Rows.Count; i++)
                    {
                        Label lbltreatment = (Label)UCTreatment.gvtreatment.Rows[i].FindControl("lbltreatment");
                        CheckBox Chktreatment = (CheckBox)UCTreatment.gvtreatment.Rows[i].FindControl("Chktreatment");

                        if (Convert.ToInt32(dt.Rows[j]["ValueId"]) == Convert.ToInt32(lbltreatment.Text))
                        {
                            Chktreatment.Checked = true;
                            if (dt.Rows[j]["Name"].ToString().ToUpper() == "OTHER REFERRALS")
                            {
                                visibleDiv("DivTreatmentOther", "show");
                            }                    


                        }

                    }
                }
            }
        }
        private void bindEnrollIn(DataTable thedt)
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            DataTable dt = new DataTable();
            string script = string.Empty;
            if (thedt.Rows.Count > 0)
            {
                DataView theDV = new DataView(thedt);
                theDV.RowFilter = "FieldName='NigeriaListEnrollin'";
                dt = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                {
                    for (int i = 0; i < this.UCTreatment.gvenrollin.Rows.Count; i++)
                    {
                        Label lblenrollin = (Label)UCTreatment.gvenrollin.Rows[i].FindControl("lblenrollin");
                        CheckBox Chkenrollin = (CheckBox)UCTreatment.gvenrollin.Rows[i].FindControl("Chkenrollin");

                        if (Convert.ToInt32(dt.Rows[j]["ValueId"]) == Convert.ToInt32(lblenrollin.Text))
                        {
                            Chkenrollin.Checked = true;


                        }

                    }
                }
            }
        }
        protected void btnClinicalHistorySave_Click(object sender, EventArgs e)
        {
            Save(0);
        }

        protected void btnHIVHistorySave_Click(object sender, EventArgs e)
        {
            Save(0);
        }

        protected void btnExaminationSave_Click(object sender, EventArgs e)
        {
            Save(0);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Add" && Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                string theUrl;
                theUrl = string.Format("frmPatient_Home.aspx");
                Response.Redirect(theUrl);
            }

            else
            {
                string theUrl;
                theUrl = string.Format("frmPatient_History.aspx");
                Response.Redirect(theUrl);
            }
        }
    }
}