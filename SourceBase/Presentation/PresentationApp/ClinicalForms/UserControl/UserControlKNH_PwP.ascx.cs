using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Application.Common;
using Application.Presentation;
using Interface.Clinical;
using System.Collections;
using System.Drawing;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlKNH_PwP : System.Web.UI.UserControl
    {
        IKNHStaticForms PwPManager;   
        DataSet theDSXML;
        DataView theDVCodeID, theDV;
        DataTable theDT;
        String startTime;
        IQCareUtils theUtils = new IQCareUtils();
        BindFunctions BindManager = new BindFunctions();
        //string UrethraDischarge, VaginalDischarge, genitalUlceration, TCA;

        protected void Page_Load(object sender, EventArgs e)
        {
            //UrethraDischarge = VaginalDischarge = genitalUlceration = TCA = "";
            //startTime = String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now);
            startTime = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", DateTime.Now);

            PwPManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            if (!IsPostBack)
            {                
                fillSelect_MultiSelect();
                FemaleControls();
                addAttributes();
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    //Load Details
                    LoadExistingData();
                }
                else if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    LoadAutoPopulatingData();
                }
            }

            showHideControls();

            loadSignature();
        }

        public void loadSignature()
        {
            if (this.Page.Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") != null)
            {
                switch ((this.Page.Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text)
                {
                    case "Express":
                        this.UserControlKNH_SignaturePwP.lblSignature.Text = PwPManager.GetSignature("PwP", Convert.ToInt32(Session["PatientVisitId"]));
                        break;
                    case "Paediatric Initial Evaluation":
                        this.UserControlKNH_SignaturePwP.lblSignature.Text = PwPManager.GetSignature("PaediatricIEPwP", Convert.ToInt32(Session["PatientVisitId"]));
                        break;
                    case "Paediatric Follow Up":
                        this.UserControlKNH_SignaturePwP.lblSignature.Text = PwPManager.GetSignature("PaedFUPwP", Convert.ToInt32(Session["PatientVisitId"]));
                        break;
                    case "Adult Initial Evaluation":
                        this.UserControlKNH_SignaturePwP.lblSignature.Text = PwPManager.GetSignature("AdultIEPwP", Convert.ToInt32(Session["PatientVisitId"]));
                        break;
                    case "Adult Follow Up":
                        this.UserControlKNH_SignaturePwP.lblSignature.Text = PwPManager.GetSignature("AdultFUPwP", Convert.ToInt32(Session["PatientVisitId"]));
                        break;
                }
            }


            //if ((this.Page.Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text == "Express")
            //    this.UserControlKNH_SignaturePwP.lblSignature.Text = PwPManager.GetSignature("PwP", Convert.ToInt32(Session["PatientVisitId"]));
            //else if ((this.Page.Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text == "Paediatric Initial Evaluation")
            //    this.UserControlKNH_SignaturePwP.lblSignature.Text = PwPManager.GetSignature("PaediatricIEPwP", Convert.ToInt32(Session["PatientVisitId"]));
            //else if ((this.Page.Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text == "Paediatric Follow Up")
            //    this.UserControlKNH_SignaturePwP.lblSignature.Text = PwPManager.GetSignature("PaedFUPwP", Convert.ToInt32(Session["PatientVisitId"]));
            //else if ((this.Page.Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text == "Adult Initial Evaluation")
            //    this.UserControlKNH_SignaturePwP.lblSignature.Text = PwPManager.GetSignature("AdultIEPwP", Convert.ToInt32(Session["PatientVisitId"]));
            //else if ((this.Page.Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text == "Adult Follow Up")
            //    this.UserControlKNH_SignaturePwP.lblSignature.Text = PwPManager.GetSignature("AdultFUPwP", Convert.ToInt32(Session["PatientVisitId"]));
        }

        public void addAttributes()
        {
            radbtnSexualActiveness.Attributes.Add("OnClick", "rblSelectedValue(this,'DIVSexualOrientation');rblSelectedValue(this,'DIVSexualHighrisk'); rblSelectedValue(this,'DIVDiscloseSexualPartner'); rblSelectedValue(this,'DIVDiscloseSexualPartner'); rblSelectedValue(this,'DIVStatusSexualPartner');clearvaluesSexuallyActive(this,'" + rcbSexualOrientation.ClientID + "', '" + cblHighRisk.ClientID + "', '" + radbtnDisclosedstatusToSexualPartner.ClientID + "', '" + rcbPartnerHIVStatus.ClientID + "');");
            radbtnLMP.Attributes.Add("OnClick", "rblSelectedValue(this,'DIVLMPDate'); rblSelectedValueNo(this,'DIVMenopausal');clearLMP(this, '" + DDLReasonLMP.ClientID + "', '" + txtLMPDate.ClientID + "');");
            radbtnPDTDone.Attributes.Add("OnClick", "rblSelectedValue(this,'DIVClientPregnant');clearPTD(this, '" + rblClientPregnant.ClientID + "', '" + radbtnPMTCTOffered.ClientID + "', '" + txtEDD.ClientID + "');");
            rblClientPregnant.Attributes.Add("OnClick", "rblSelectedValue(this,'trIfYesPregnant');clearClientPreg(this, '" + radbtnPMTCTOffered.ClientID + "', '" + txtEDD.ClientID + "');");
            radbtnIntentionOfPregnancy.Attributes.Add("OnClick", "rblSelectedValue(this,'DIVdisfertility'); rblSelectedValueNo(this,'DIVDiscusdualcontraception');clearPregInt(this, '" + radbtnDiscussedFertilityOptions.ClientID + "', '" + radbtnDiscussedDualContraception.ClientID + "')");
            radbtnCondomsIssued.Attributes.Add("OnClick", "rblSelectedValueNo(this,'DIVnotICon');clearCondomsIssued(this, '" + txtCondomNotIssued.ClientID + "');");
            radbtnSTIScreened.Attributes.Add("OnClick", "rblSelectedValue(this,'DIVUrethral'); rblSelectedValue(this,'DIVGenUlceration');rblSelectedValue(this,'STITreatmentOther');clearSTIScreened(this, '" + chkUrethralDischarge.ClientID + "', '" + chkVaginalDischarge.ClientID + "', '" + chkGenitalUlceration.ClientID + "', '" + txtSTITreatmentPlan.ClientID + "', '" + txtOtherSTITreatment.ClientID + "')");
            radbtnOnFP.Attributes.Add("OnClick", "rblSelectedValue(this,'DIVFPMethod');rblSelectedValueNo(this,'DIVReferredFP');clearOnFP(this, '" + rcbFPMethod.ClientID + "', '" + ddlReferredFP.ClientID + "')");
            radbtnCervicalCancerScreened.Attributes.Add("OnClick", "rblSelectedValue(this,'DIVCacervix');rblSelectedValueNo(this,'DIVRefCervCancer');clearCervicalCancer(this, '" + rcbCervicalCancerScreeningResults.ClientID + "', '" + radbtnReferredForCervicalCancerScreening.ClientID + "');");
            radbtnHPVOffered.Attributes.Add("OnClick", "rblSelectedValue(this,'DIVHPVVaccine');rblSelectedValue(this,'DIVHPVVaccineDate');clearHPV(this, '" + rcbOfferedHPVVaccine.ClientID + "', '" + dtHPVDoseDate.ClientID + "');");

            cblReferredTo.Attributes.Add("OnClick", "CheckBoxHideUnhideOtherSpecialistClinic('" + cblReferredTo.ClientID + "','" + txtReferToSpecialistClinic.ClientID + "');CheckBoxHideUnhideOtherReferral('" + cblReferredTo.ClientID + "','" + txtSpecifyOtherRefferedTo.ClientID + "');CheckBoxHideUnhideDivCounselling('" + cblReferredTo.ClientID + "','" + cblCounselling.ClientID + "');fnCheckUncheckPwP('"+cblReferredTo.ClientID+"');");
            cblCounselling.Attributes.Add("OnClick", "CheckBoxHideUnhideOtherCounselling('" + cblCounselling.ClientID + "','" + txtOtherCounselling.ClientID + "');");

            txtLMPDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
            txtEDD.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3);");
        }

        public void FemaleControls()
        {
            if (Session["PatientSex"].ToString() == "Male")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "trLMP", "ShowHide('trLMP','hide');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "trLMP", "ShowHide('trLMP','hide');", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "trPregnancyTest", "ShowHide('trPregnancyTest','hide');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "trPregnancyTest", "ShowHide('trPregnancyTest','hide');", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "trIfYesPregnant", "ShowHide('trIfYesPregnant','hide');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "trIfYesPregnant", "ShowHide('trIfYesPregnant','hide');", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "trIntendPreg", "ShowHide('trIntendPreg','hide');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "trIntendPreg", "ShowHide('trIntendPreg','hide');", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "trCervicalCancer", "ShowHide('trCervicalCancer','hide');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "trCervicalCancer", "ShowHide('trCervicalCancer','hide');", true);
            }
        }

        public void fillSelect_MultiSelect()
        {
            try
            {
                theDSXML = new DataSet();
                theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\AllMasters.con"));

                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='SexualOrientation'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(rcbSexualOrientation, theDT, "Name", "ID");

                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='PartnerHIVStatus'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(rcbPartnerHIVStatus, theDT, "Name", "ID");

                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='HighRisk'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCheckedList(cblHighRisk, theDT, "Name", "ID");

                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='counselling'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCheckedList(cblCounselling, theDT, "Name", "ID");

                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='RefferedToFUpF'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCheckedList(cblReferredTo, theDT, "Name", "ID");

                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='TransitionPreparation'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCheckedList(cblTransitionPreparation, theDT, "Name", "ID");

                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='FPMethod'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(rcbFPMethod, theDT, "Name", "ID");

                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='ReferredToFP'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddlReferredFP, theDT, "Name", "ID");

                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='LMP Not Accessed'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(DDLReasonLMP, theDT, "Name", "ID");

                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='CervicalCancerScreeningResults'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(rcbCervicalCancerScreeningResults, theDT, "Name", "ID");

                theDVCodeID = new DataView(theDSXML.Tables["Mst_Code"]);
                theDVCodeID.RowFilter = "Name='OfferedHPVaccine'";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=" + ((DataTable)theDVCodeID.ToTable()).Rows[0]["CodeID"].ToString();
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(rcbOfferedHPVVaccine, theDT, "Name", "ID");
            }
            catch (Exception ex)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "fillMultiSelectError", "alert('Please refresh system cache.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fillMultiSelectError", "alert('Please refresh system cache.');", true);
            }
          

        }

        private void showHideControls()
        {
            //string IPTSelectedText = string.Empty;
            //foreach (ListItem item in rdoLstIPT.Items)
            //{
            //    if (item.Selected)
            //    {
            //        IPTSelectedText = item.Text;
            //    }
            //}

            if (radbtnSexualActiveness.SelectedValue == "1")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "sexaulActiveness", "ShowHide('DIVSexualOrientation','show');ShowHide('DIVSexualHighrisk','show'); ShowHide('DIVDiscloseSexualPartner','show'); ShowHide('DIVDiscloseSexualPartner','show'); ShowHide('DIVStatusSexualPartner','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "sexaulActiveness", "ShowHide('DIVSexualOrientation','show');ShowHide('DIVSexualHighrisk','show'); ShowHide('DIVDiscloseSexualPartner','show'); ShowHide('DIVDiscloseSexualPartner','show'); ShowHide('DIVStatusSexualPartner','show');", true);
            }

            if (radbtnLMP.SelectedValue == "1")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnLMPYes", "ShowHide('DIVLMPDate','show'); ShowHide('DIVMenopausal','hide');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnLMPYes", "ShowHide('DIVLMPDate','show'); ShowHide('DIVMenopausal','hide');", true);
            }
            else if (radbtnLMP.SelectedValue == "0")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnLMPNo", "ShowHide('DIVLMPDate','hide'); ShowHide('DIVMenopausal','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnLMPNo", "ShowHide('DIVLMPDate','hide'); ShowHide('DIVMenopausal','show');", true);
            }

            if (radbtnPDTDone.SelectedValue == "1")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnPDT", "ShowHide('DIVClientPregnant','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnPDT", "ShowHide('DIVClientPregnant','show');", true);
            }

            if (rblClientPregnant.SelectedValue == "1")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnClientPreg", "ShowHide('trIfYesPregnant','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnClientPreg", "ShowHide('trIfYesPregnant','show');", true);
            }

            if (radbtnIntentionOfPregnancy.SelectedValue == "1")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnPregInt", "ShowHide('DIVdisfertility','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnPregInt", "ShowHide('DIVdisfertility','show');", true);
            }
            else if (radbtnIntentionOfPregnancy.SelectedValue == "0")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnPregInt", "ShowHide('DIVDiscusdualcontraception','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnPregInt", "ShowHide('DIVDiscusdualcontraception','show');", true);
            }

            if (radbtnCondomsIssued.SelectedValue == "0")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnCondIss", "ShowHide('DIVnotICon','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnCondIss", "ShowHide('DIVnotICon','show');", true);
            }

            if (radbtnSTIScreened.SelectedValue == "1")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnSTIScr", "ShowHide('DIVUrethral','show'); ShowHide('DIVGenUlceration','show');ShowHide('STITreatmentOther','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnSTIScr", "ShowHide('DIVUrethral','show'); ShowHide('DIVGenUlceration','show');ShowHide('STITreatmentOther','show');", true);
            }

            if (radbtnOnFP.SelectedValue == "1")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnONFP", "ShowHide('DIVFPMethod','show'); ShowHide('DIVReferredFP','hide');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnONFP", "ShowHide('DIVFPMethod','show'); ShowHide('DIVReferredFP','hide');", true);
            }
            else if (radbtnOnFP.SelectedValue == "0")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnONFP", "ShowHide('DIVFPMethod','hide'); ShowHide('DIVReferredFP','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnONFP", "ShowHide('DIVFPMethod','hide'); ShowHide('DIVReferredFP','show');", true);
            }

            if (radbtnCervicalCancerScreened.SelectedValue == "1")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btncervical", "ShowHide('DIVCacervix','show'); ShowHide('DIVRefCervCancer','hide');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btncervical", "ShowHide('DIVCacervix','show'); ShowHide('DIVRefCervCancer','hide');", true);
            }
            else if (radbtnCervicalCancerScreened.SelectedValue == "0")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btncervical", "ShowHide('DIVCacervix','hide'); ShowHide('DIVRefCervCancer','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btncervical", "ShowHide('DIVCacervix','hide'); ShowHide('DIVRefCervCancer','show');", true);
            }

            if (radbtnHPVOffered.SelectedValue == "1")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "btnHPV", "ShowHide('DIVHPVVaccine','show'); ShowHide('DIVHPVVaccineDate','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btnHPV", "ShowHide('DIVHPVVaccine','show'); ShowHide('DIVHPVVaccineDate','show');", true);
            }

            if (this.UserControlKNH_NextAppointment1.rdoTCAYes.Checked)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "NxtApp", "ShowHide('trNextAppointment','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "NxtApp", "ShowHide('trNextAppointment','show');", true);
            }
            else if (this.UserControlKNH_NextAppointment1.rdoTCANo.Checked)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "careEnd", "ShowHide('trCareEnd','show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "careEnd", "ShowHide('trCareEnd','show');", true);
            }

            ShowHideOther(cblReferredTo, "DivCounselling", "Psychologist");
            ShowHideOther(cblCounselling, "divOtherCounselling","Other");
            ////TB Findings
            ////Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction27", "SelectTBFindings('" + ddlTBFindings.ClientID + "','" + rdoAvailableTBResultsYes.ClientID + "','" + rdoAvailableTBResultsNo.ClientID + "');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction27", "SelectTBFindings('" + ddlTBFindings.ClientID + "');", true);

            //if (rdoMissedAnyTBDosesYes.Checked == true)
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction28", "show_hide('MissedDosesYesReferredforadherence','visible');", true);

            //if (rdoMissedAnyTBDosesNo.Checked == true)
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction29", "show_hide('MissedDosesYesReferredforadherence','notvisible');", true);

            //if (rdoContactsScreenedForTBYes.Checked == true)
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction30", "show_hide('IfNoContactsScreenedSpecifyWhy','notvisible');", true);

            //if (rdoContactsScreenedForTBNo.Checked == true)
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction31", "show_hide('IfNoContactsScreenedSpecifyWhy','visible');", true);

            ////if (rdoTBConfirmedSuspectedYes.Checked == true)
            ////    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction32", "show_hide('INHStopDate','visible');", true);

            ////if (rdoTBConfirmedSuspectedNo.Checked == true)
            ////    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction33", "show_hide('INHStopDate','notvisible');", true);


            //foreach (ListItem item in cblReviewChecklist.Items)
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction34", "SelectOtherReviewChkList('" + cblReviewChecklist.ClientID + "','ReviewChkListSpecifyOtherTBSideEffects','" + txtSpecifyOtherTBSideEffects.ClientID + "');SignsOfHepatitisReviewChkList('" + cblReviewChecklist.ClientID + "','divSignsOfHepatitis');", true);
            //}
        }

        public void ShowHideOther(CheckBoxList chklst, string divID, string text)
        {
            for (int i = 0; i < chklst.Items.Count; i++)
            {
                ListItem li = chklst.Items[i];
                if (li.Text == text)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "show" + divID + "div", "ShowHide('" + divID + "', 'show');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hide" + divID + "div", "ShowHide('" + divID + "', 'hide');", true);
                }

            }
        }

        public void LoadAutoPopulatingData()
        {
            DataSet dsAutopopulate = new DataSet();
            dsAutopopulate = PwPManager.GetPwPAutoPopulatingData(Convert.ToInt32(Session["PatientId"]));

            if (dsAutopopulate.Tables[0].Rows.Count > 0)
            {
                txtLMPDate.Value = dsAutopopulate.Tables[0].Rows[0].IsNull("LMPDate") ? "" : ((DateTime)dsAutopopulate.Tables[0].Rows[0]["LMPDate"]).ToString("dd-MMM-yyyy");
                txtEDD.Value = dsAutopopulate.Tables[0].Rows[0].IsNull("EDD") ? "" : ((DateTime)dsAutopopulate.Tables[0].Rows[0]["EDD"]).ToString("dd-MMM-yyyy");
            }
        }

        public void LoadExistingData()
        {
            DataSet theDSExistingForm = new DataSet();
            theDSExistingForm = PwPManager.GetPwPFormData(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));
            if (theDSExistingForm.Tables[0].Rows.Count > 0)
            {

                radbtnSexualActiveness.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["SexuallyActiveLast6Months"].ToString();
                rcbSexualOrientation.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["SexualOrientation"].ToString();
                radbtnDisclosedstatusToSexualPartner.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["DisclosedStatusToSexualPartner"].ToString();
                rcbPartnerHIVStatus.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["HIVstatusOfsexualPartner"].ToString();
                radbtnGivenPWPMessages.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["PwPMessagesGiven"].ToString();
                radbtnSaferSexImportanceExplained.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["ImpOfSafeSexExplained"].ToString();
                radbtnLMP.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["LMPAssessed"].ToString();

                DDLReasonLMP.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["ReasonLMPNotAssessed"].ToString();
                radbtnPDTDone.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["PregnancyTestDone"].ToString();
                rblClientPregnant.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["clientPregnant"].ToString();
                radbtnPMTCTOffered.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["referredToPMTCT"].ToString();

                radbtnIntentionOfPregnancy.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["IntendToBePregnantBeforeNextVisit"].ToString();
                radbtnDiscussedFertilityOptions.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["DiscussedFertilityOptions"].ToString();
                radbtnDiscussedDualContraception.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["discussedDualContraception"].ToString();
                radbtnCondomsIssued.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["condomsIssued"].ToString();
                txtCondomNotIssued.Text = theDSExistingForm.Tables[0].Rows[0]["ReasonCondomNoIssued"].ToString();
                radbtnSTIScreened.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["ScreenedForSTI"].ToString();

                txtSTITreatmentPlan.Text = theDSExistingForm.Tables[0].Rows[0]["STITreatment"].ToString();
                txtOtherSTITreatment.Text = theDSExistingForm.Tables[0].Rows[0]["OtherSTITreatment"].ToString();
                radbtnOnFP.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["OnFPMethod"].ToString();
                rcbFPMethod.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["SpecifyFPMethod"].ToString();
                ddlReferredFP.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["referredForFP"].ToString();
                radbtnCervicalCancerScreened.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["screenedForCervicalCancer"].ToString();
                rcbCervicalCancerScreeningResults.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["CacervixScreeningResults"].ToString();
                radbtnReferredForCervicalCancerScreening.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["referredForCaScreening"].ToString();
                radbtnHPVOffered.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["HPVOffered"].ToString();
                rcbOfferedHPVVaccine.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["HPVVaccineOffered"].ToString();

                rblWardAdmission.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["WardAdmission"].ToString();
                txtReferToSpecialistClinic.Text = theDSExistingForm.Tables[0].Rows[0]["specifySpecialistClinic"].ToString();
                txtSpecifyOtherRefferedTo.Text = theDSExistingForm.Tables[0].Rows[0]["specifyOtherReferredTo"].ToString();
                txtOtherCounselling.Text = theDSExistingForm.Tables[0].Rows[0]["OtherCounselling"].ToString();


                if (theDSExistingForm.Tables[0].Rows[0]["TCA"].ToString() == "1")
                {
                    this.UserControlKNH_NextAppointment1.rdoTCAYes.Checked = true;
                    //TCA = "1";
                }
                else if (theDSExistingForm.Tables[0].Rows[0]["TCA"].ToString() == "0")
                {
                    this.UserControlKNH_NextAppointment1.rdoTCANo.Checked = true;
                    //TCA = "0";
                }
                else
                {
                    this.UserControlKNH_NextAppointment1.rdoTCAYes.Checked = false;
                    this.UserControlKNH_NextAppointment1.rdoTCANo.Checked = false;
                }

                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["LMPDate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    txtLMPDate.Value = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["LMPDate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtLMPDate.Value = "";
                //}
                txtLMPDate.Value = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["LMPDate"]);
                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["EDD"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    txtEDD.Value = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["EDD"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    txtEDD.Value = "";
                //}
                txtEDD.Value = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["EDD"]);
                //if (((DateTime)theDSExistingForm.Tables[0].Rows[0]["HPVVaccinedate"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                //{
                //    dtHPVDoseDate.Value = ((DateTime)theDSExistingForm.Tables[0].Rows[0]["HPVVaccinedate"]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    dtHPVDoseDate.Value = "";
                //}
                dtHPVDoseDate.Value = String.Format("{0:dd-MMM-yyyy}", theDSExistingForm.Tables[0].Rows[0]["HPVVaccinedate"]);
                //chkUrethralDischarge.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["UrethralDischarge"].ToString();
                //chkVaginalDischarge.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["VaginalDischarge"].ToString();
                //chkGenitalUlceration.SelectedValue = theDSExistingForm.Tables[0].Rows[0]["GenitalUlceration"].ToString();

                if (theDSExistingForm.Tables[0].Rows[0]["UrethralDischarge"].ToString() == "1")
                {
                    chkUrethralDischarge.Checked = true;
                    //UrethraDischarge = "1";
                }
                else
                {
                    chkUrethralDischarge.Checked = false;
                    //UrethraDischarge = "0";
                }

                if (theDSExistingForm.Tables[0].Rows[0]["VaginalDischarge"].ToString() == "1")
                {
                    chkVaginalDischarge.Checked = true;
                    //VaginalDischarge = "1";
                }
                else
                {
                    chkVaginalDischarge.Checked = false;
                    //VaginalDischarge = "0";
                }

                if (theDSExistingForm.Tables[0].Rows[0]["GenitalUlceration"].ToString() == "1")
                {
                    chkGenitalUlceration.Checked = true;
                    //genitalUlceration = "1";
                }
                else
                {
                    chkGenitalUlceration.Checked = false;
                    //genitalUlceration = "0";
                }

                string script = string.Empty;
                if (theDSExistingForm.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < theDSExistingForm.Tables[1].Rows.Count; i++)
                    {
                        ListItem currentCheckBox = cblHighRisk.Items.FindByValue(theDSExistingForm.Tables[1].Rows[i]["ValueID"].ToString());
                        if (currentCheckBox != null)
                        {
                            currentCheckBox.Selected = true;
                            
                        }
                    }
                }

                if (theDSExistingForm.Tables[2].Rows.Count > 0)
                {
                    for (int i = 0; i < theDSExistingForm.Tables[2].Rows.Count; i++)
                    {
                        ListItem currentCheckBox = cblTransitionPreparation.Items.FindByValue(theDSExistingForm.Tables[2].Rows[i]["ValueID"].ToString());
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
                        ListItem currentCheckBox = cblReferredTo.Items.FindByValue(theDSExistingForm.Tables[3].Rows[i]["ValueID"].ToString());
                        if (currentCheckBox != null)
                        {
                            currentCheckBox.Selected = true;
                            if (currentCheckBox.Text.ToLower() == "other specialist clinic")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ReferToSpecialistClinic", "ShowHide('divReferToSpecialistClinic','show');", true);

                            }
                            else if (currentCheckBox.Text.ToLower() == "other (specify)")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ReferToOther", "ShowHide('divReferToOther','show');", true);

                            }
                        }
                    }
                }

                if (theDSExistingForm.Tables[4].Rows.Count > 0)
                {
                    for (int i = 0; i < theDSExistingForm.Tables[4].Rows.Count; i++)
                    {
                        ListItem currentCheckBox = cblCounselling.Items.FindByValue(theDSExistingForm.Tables[4].Rows[i]["ValueID"].ToString());
                        if (currentCheckBox != null)
                        {
                            currentCheckBox.Selected = true;
                        }
                    }
                }

                
            }
        }

        protected Hashtable PwPHT()
        {
            //if (this.UserControlKNH_NextAppointment1.rdoTCAYes.Checked)
            //{
            //    TCA = "1";
            //}
            //else if (this.UserControlKNH_NextAppointment1.rdoTCANo.Checked)
            //{
            //    TCA = "0";
            //}
            //else
            //{
            //    TCA = null;
            //}

            //validateDates();

            Hashtable theHT = new Hashtable();
            try
            {
                Label lbl_formName = (Label)this.Parent.Parent.Parent.Parent.FindControl("lblHeading");
                string formName = lbl_formName == null ? "" : lbl_formName.Text;

                theHT.Add("patientID", Convert.ToInt32(Session["PatientId"]));
                theHT.Add("visitID", Convert.ToInt32(Session["PatientVisitId"]));
                theHT.Add("locationID", Convert.ToInt32(Session["AppLocationID"]));
                theHT.Add("userID", Convert.ToInt32(Session["AppUserId"]));
                theHT.Add("SexualActiveness", radbtnSexualActiveness.SelectedValue);
                theHT.Add("SexualOrientation", rcbSexualOrientation.SelectedValue);
                theHT.Add("KnowSexualPartnerHIVStatus", radbtnDisclosedstatusToSexualPartner.SelectedValue);
                theHT.Add("PartnerHIVStatus", rcbPartnerHIVStatus.SelectedValue);
                theHT.Add("GivenPWPMessages", radbtnGivenPWPMessages.SelectedValue);
                theHT.Add("SaferSexImportanceExplained", radbtnSaferSexImportanceExplained.SelectedValue);
                theHT.Add("LMP", radbtnLMP.SelectedValue);
                theHT.Add("LMPDate", txtLMPDate.Value);
                theHT.Add("ReasonLMP", DDLReasonLMP.SelectedValue);
                theHT.Add("PDTDone", radbtnPDTDone.SelectedValue);
                theHT.Add("ClientPregnant", rblClientPregnant.SelectedValue);
                theHT.Add("PMTCTOffered", radbtnPMTCTOffered.SelectedValue);
                theHT.Add("EDD", txtEDD.Value);
                theHT.Add("IntentionOfPregnancy", radbtnIntentionOfPregnancy.SelectedValue);
                theHT.Add("DiscussedFertilityOptions", radbtnDiscussedFertilityOptions.SelectedValue);
                theHT.Add("DiscussedDualContraception", radbtnDiscussedDualContraception.SelectedValue);
                theHT.Add("CondomsIssued", radbtnCondomsIssued.SelectedValue);
                theHT.Add("CondomNotIssued", txtCondomNotIssued.Text);
                theHT.Add("STIScreened", radbtnSTIScreened.SelectedValue);
                theHT.Add("UrethralDischarge", chkUrethralDischarge.Checked ? 1 : 0);
                theHT.Add("VaginalDischarge", chkVaginalDischarge.Checked ? 1 : 0);
                theHT.Add("GenitalUlceration", chkGenitalUlceration.Checked ? 1 : 0);
                theHT.Add("STITreatmentPlan", txtSTITreatmentPlan.Text);
                theHT.Add("OtherSTITreatmentPlan", txtOtherSTITreatment.Text);
                theHT.Add("OnFP", radbtnOnFP.SelectedValue);
                theHT.Add("FPMethod", rcbFPMethod.SelectedValue);
                theHT.Add("ReferredFP", ddlReferredFP.SelectedValue);
                theHT.Add("CervicalCancerScreened", radbtnCervicalCancerScreened.SelectedValue);
                theHT.Add("CervicalCancerScreeningResults", rcbCervicalCancerScreeningResults.SelectedValue);
                theHT.Add("ReferredForCervicalCancerScreening", radbtnReferredForCervicalCancerScreening.SelectedValue);
                theHT.Add("HPVOffered", radbtnHPVOffered.SelectedValue);
                theHT.Add("OfferedHPVVaccine", rcbOfferedHPVVaccine.SelectedValue);
                theHT.Add("HPVDoseDate", dtHPVDoseDate.Value);
                theHT.Add("WardAdmission", rblWardAdmission.SelectedValue);
                theHT.Add("ReferToSpecialistClinic", txtReferToSpecialistClinic.Text);
                theHT.Add("SpecifyOtherRefferedTo", txtSpecifyOtherRefferedTo.Text);
                theHT.Add("OtherCounselling", txtOtherCounselling.Text);
                theHT.Add("TCA", this.UserControlKNH_NextAppointment1.rdoTCAYes.Checked ? "1" : this.UserControlKNH_NextAppointment1.rdoTCANo.Checked ? "0" : "");
                //theHT.Add("TCA", TCA);
                theHT.Add("startTime", startTime);
                //theHT.Add("FormName", (this.Page.Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text);
                theHT.Add("FormName", formName);
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }
            return theHT;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkRequiredFieldsPwP() == true)
                {

                    if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                    {
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction99", "alert('Triage Information has not been entered. Please fill in Triage information first.');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction99", "alert('Triage Information has not been entered. Please fill in Triage information first.');", true);
                    }
                    else
                    {
                        //Hashtable HT = PwPHT();
                        PwPManager.SaveUpdatePwP(PwPHT(), getCheckBoxListItemValues(cblHighRisk), getCheckBoxListItemValues(cblTransitionPreparation), getCheckBoxListItemValues(cblReferredTo), getCheckBoxListItemValues(cblCounselling));
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "PwPSaveUpdateRecord", "alert('Data on Prevention with Positives tab saved successfully.');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PwPSaveUpdateRecord", "alert('Data on Prevention with Positives tab saved successfully.');", true);
                    }
                }
                
           
            }
            catch (Exception ex)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "PwPSaveUpdateError", "alert('Error encountered. Please contact the system administrator');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PwPSaveUpdateError", "alert('Error encountered. Please contact the system administrator');", true);
            }
            
        }

        private bool checkRequiredFieldsPwP()
        {
            bool value = true;
            if (radbtnSexualActiveness.SelectedIndex == -1)
            {
                value = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "sexuallyActivePast6Months", "alert('Have you been sexually active in the past 6 months not filled in.');", true);
                lblSexualityAssessment.ForeColor = Color.Red;
                lblSexuallyActiveLast6Months.ForeColor = Color.Red;
                return value;
            }
            else if (radbtnSexualActiveness.SelectedValue == "1")
            {
                lblSexuallyActiveLast6Months.ForeColor = Color.Black;
                if (radbtnDisclosedstatusToSexualPartner.SelectedIndex == -1)
                {
                    value = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "disclosedstatusTopartner", "alert('Have you disclosed your status to sexual partner not filled in.');", true);
                    lblSexualityAssessment.ForeColor = Color.Red;
                    lblDisclosedToSexualPartner.ForeColor = Color.Red;
                    return value;
                }
                else
                {
                    lblSexualityAssessment.ForeColor = Color.Black;
                    lblDisclosedToSexualPartner.ForeColor = Color.Black;
                }

                if (rcbPartnerHIVStatus.SelectedValue == "0")
                {
                    value = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HIVStatusofpartner", "alert('HIV Status of sexual partner not filled in.');", true);
                    lblSexualityAssessment.ForeColor = Color.Red;
                    lblStatusOfSexualPartner.ForeColor = Color.Red;
                    return value;
                }
                else
                {
                    lblSexualityAssessment.ForeColor = Color.Black;
                    lblStatusOfSexualPartner.ForeColor = Color.Black;
                }
            }
            else
            {
                lblSexualityAssessment.ForeColor = Color.Black;
                lblSexuallyActiveLast6Months.ForeColor = Color.Black;
            }


            if (this.UserControlKNH_NextAppointment1.rdoTCAYes.Checked == false && this.UserControlKNH_NextAppointment1.rdoTCANo.Checked == false)
            {
                value = false;
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "TCANotSelected", "alert('TCA not filled in.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "TCANotSelected", "alert('TCA not filled in.');", true);

                lblReferralAndAppointments.ForeColor = Color.Red;
                this.UserControlKNH_NextAppointment1.lblTCA.ForeColor = Color.Red;   
            }
            else
            {
                lblReferralAndAppointments.ForeColor = Color.Black;
                this.UserControlKNH_NextAppointment1.lblTCA.ForeColor = Color.Black; 
            }

            

            return value;
        }

        private DataTable getCheckBoxListItemValues(CheckBoxList cbl)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value", typeof(int));
            if (cbl.Items.Count > 0)
            {
                for (int i = 0; i < cbl.Items.Count; i++)
                {
                    if (cbl.Items[i].Selected)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = cbl.Items[i].Value;
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        protected void radbtnSexualActiveness_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radbtnSexualActiveness.SelectedValue == "0")
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "clearSexualActiveness", "ClearSelectList('" + rcbSexualOrientation.ClientID + "');ClearMultiSelect('" + cblHighRisk.ClientID + "');clearRadioButtonList('" + radbtnDisclosedstatusToSexualPartner.ClientID + "');ClearSelectList('" + rcbPartnerHIVStatus.ClientID + "');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "clearSexualActiveness", "ClearSelectList('" + rcbSexualOrientation.ClientID + "');ClearMultiSelect('" + cblHighRisk.ClientID + "');clearRadioButtonList('" + radbtnDisclosedstatusToSexualPartner.ClientID + "');ClearSelectList('" + rcbPartnerHIVStatus.ClientID + "');", true);
            }
        }

        protected void radbtnDisclosedstatusToSexualPartner_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void radbtnGivenPWPMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void radbtnSaferSexImportanceExplained_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void radbtnLMP_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void radbtnPDTDone_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void rblClientPregnant_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void radbtnPMTCTOffered_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void radbtnIntentionOfPregnancy_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void radbtnDiscussedFertilityOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void radbtnDiscussedDualContraception_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void radbtnCondomsIssued_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void radbtnSTIScreened_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void chkUrethralDischarge_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkUrethralDischarge.Checked)
            //    UrethraDischarge = "1";
            //else
            //    UrethraDischarge = "0";
        }

        protected void chkVaginalDischarge_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkVaginalDischarge.Checked)
            //    VaginalDischarge = "1";
            //else
            //    VaginalDischarge = "0";
        }

        protected void chkGenitalUlceration_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkGenitalUlceration.Checked)
            //    genitalUlceration = "1";
            //else
            //    genitalUlceration = "0";
        }

        protected void radbtnOnFP_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void radbtnCervicalCancerScreened_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void radbtnReferredForCervicalCancerScreening_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void radbtnHPVOffered_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void rblWardAdmission_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        //private void validateDates()
        //{

        //    DateTime temp;

        //    if (DateTime.TryParse(txtEDD.Value, out temp))
        //    {
        //        txtEDD.Value = temp.ToString("dd-MMM-yyyy");
        //    }
        //    else
        //    {
        //        txtEDD.Value = "1900-01-01";
        //    }

        //    if (DateTime.TryParse(txtLMPDate.Value, out temp))
        //    {
        //        txtLMPDate.Value = temp.ToString("dd-MMM-yyyy");
        //    }
        //    else
        //    {
        //        txtLMPDate.Value = "1900-01-01";
        //    }

        //    if (DateTime.TryParse(dtHPVDoseDate.Value, out temp))
        //    {
        //        dtHPVDoseDate.Value = temp.ToString("dd-MMM-yyyy");
        //    }
        //    else
        //    {
        //        dtHPVDoseDate.Value = "1900-01-01";
        //    }
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }
    }
}