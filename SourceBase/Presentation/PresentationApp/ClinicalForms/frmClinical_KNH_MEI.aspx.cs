using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Interface.Clinical;
using Interface.Security;
using Application.Presentation;
using Application.Common;
using Application.Interface;
using System.Text;
using Interface.Administration;
using System.Web.UI.HtmlControls;

namespace PresentationApp.ClinicalForms
{
    public partial class frmClinical_KNH_MEI : BasePage
    {
        DataTable theDTPrev3Freq = new DataTable("dtPrevpreg");
        DataTable DTCheckedIds;
        string chktrueother = "";
        IQCareUtils theUtils = new IQCareUtils();
        int chktrueothervalue = 0;

        protected void Page_Init(object sender, EventArgs e)
        {
            BindList();

        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            Session["isMEIVisible"] = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "MEI Form";
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "MEI Form";
            if (Session["isMEIVisible"] != null || Convert.ToBoolean(Session["isMEIVisible"]) == false)
            {
                Session["isMEIVisible"] = true;
            }

            if (!IsPostBack)
            {
                ViewState["GridPrevPregData"] = theDTPrev3Freq;
                addAttributes();
                GetLabResult();
            }
            HideControls();
            Authenticate();
        }

        protected void rdofamilyinformationFilledYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdofamilyinformationFilledYes.Checked)
            {
                IFamilyInfo PatientManager = (IFamilyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFamilyInfo, BusinessProcess.Clinical");
                if (Session["PatientId"] != null && Session["PatientId"].ToString() != "0")
                {
                    DataSet theDS = PatientManager.GetSearchFamilyInfo(Convert.ToInt32(Session["PatientId"]));
                    if (theDS.Tables[0].Rows.Count <= 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "familyInfo", "alert('Family Information not exists!');", true);
                    }
                }
            }
        }
        protected void rdofamilyinformationFilledNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdofamilyinformationFilledNo.Checked)
            {
                rdofamilyinformationFilledYes.Checked = false;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                if (!IsPostBack)
                {
                    GetFormData();
                }
            }
            else
            {
                if (!IsPostBack)
                {                    
                    GetFormData();
                }
            }
            if (ddlFieldVisitType.SelectedItem.Text == "Initial only")
            {
                showhideInitialOnly();
            }
            else if (ddlFieldVisitType.SelectedItem.Text == "Follow Up")
            {
                showhideFollowUp();
            }

            //ScriptManager.RegisterStartupScript(this, GetType(), "divTBSave", "hide('divTBSave');", true);

            //GetDataforAutopopulate();

            if (rdoHIVTestingTodayYes.Checked == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "trHIVTesting", "show('trHIVTesting1');show('trHIVTesting2');", true);
            }
            if (rdoPatientaccPartnerYes.Checked == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spnpap1", "show('pap1');show('pap2');", true);
            }
            if (ddlHMHealth.SelectedItem.Text == "Other (specify)")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spndivHMentalHealth", "show('divHMentalHealth');", true);
            }
            if (ddlCMHealth.SelectedItem.Text == "Other")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spndivCMentalHealth", "show('divCMentalHealth');", true);
            }
            if (ddlCMHealth.SelectedItem.Text == "Stop CTX")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spndivctx", "show('divctx');", true);
            }
            if (rdoHistoryBloodTransfusionYes.Checked == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spndivBloodTransfusion", "show('divBloodTransfusion');", true);
            }
            if (rdoHistoryBloodTransfusionYes.Checked == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spndivBloodTransfusion", "show('divBloodTransfusion');", true);
            }
            if (rdoMotheratriskyes.Checked == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spndivriskfactor", "show('divriskfactor');", true);
            }
            if (rdoMissedanydosesYes.Checked == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spntrMisseddoses", "show('trMisseddoses');", true);
            }
            if (UCWHOStage.radbtnMernarcheyes.Checked == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spndivmenarchedatehshowhide", "show('divmenarchedatehshowhide');", true);
            }
            if (rdoExperienceanyGBVYes.Checked == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spntbtdGBVExperiencedshowhide", "show('tdGBVExperienced');", true);
            }
            if (rdoHIVSubstanceAbusedYes.Checked == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spntbtdSubstanceAbusedshowhide", "show('tdSubstanceAbused');", true);
            }

            if (this.ddlSpecifyCurrentRegmn.SelectedItem.Text == "Other")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "spntbtdtdothercurrentregimenshowhide", "show('tdothercurrentregimen');", true);
            }
            if (rdodisclosedHIVStatusYes.Checked == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "tdPnlDiscloseHIVStatus", "show('tdPnlHIVStatus');", true);
            }
            if (rdoAdmittedtowardPMTCTYes.Checked == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "tdSpecifyWardToAdmitted", "show('tdSpecifyWardAdmitted');", true);
            }
        }

        private void HideControls()
        {

            //ScriptManager.RegisterStartupScript(this, GetType(), "trNursecomments", "hide('trNursecomments');", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "trPatientReferredto", "hide('trPatientReferredto');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "tblavailableTBResults", "hide('tblAvailableTBResultsBody');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "tblTBAssessment", "hide('tblTBAssessment');", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "DivavailableTBResults", "hide('ctl00_IQCareContentPlaceHolder_tabControl_TabPnlProfile_UCTBScreen_AvailableTBResultsHeader');", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "tblIPTBody", "show('tblIPTBody');", true);
            UCTBScreen.CollapsiblePanelExtender1.Collapsed = false;
            //ScriptManager.RegisterStartupScript(this, GetType(), "IPTHeader", "show('ctl00_IQCareContentPlaceHolder_tabControl_TabPnlProfile_UCTBScreen_IPTHeader');", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "tblDiscontinueIPTBody", "show('tblDiscontinueIPTBody');", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "DiscontinueIPTHeader", "show('ctl00_IQCareContentPlaceHolder_tabControl_TabPnlProfile_UCTBScreen_DiscontinueIPTHeader');", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "divTBSave", "hide('divTBSave');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "labresults", "SelectAllCheckboxes();", true);
        }

        private void BindList()
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            DataView theDVDecode = new DataView();
            DataTable theDTCode = new DataTable();
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            if (theDS.Tables["Mst_Decode"] != null)
            {

                theDVDecode = new DataView(theDS.Tables["Mst_Decode"]);
                theDVDecode.RowFilter = "CodeName='ReferredToFP' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    theDTCode = theDTCode.Select("Name <> 'Fertility Desire'").CopyToDataTable();
                    BindManager.BindCombo(ddlHIVTestDoneToday, theDTCode, "Name", "Id");
                }

            }

            if (theDS.Tables["Mst_PMTCTDecode"] != null)
            {
                //Field VisitType
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='FieldVisitType' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    theDTCode = theDTCode.Select("Name <> 'ANC PMTCT'").CopyToDataTable();
                    BindManager.BindCombo(ddlFieldVisitType, theDTCode, "Name", "Id");

                }
                //Parity
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='Parity' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "Name";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlparity, theDTCode, "Name", "Id");
                }
                //Gravidae
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='Gravidae' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlGravidae, theDTCode, "Name", "Id");
                }
                //VisitNumber
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='VisitNumber' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlVisitNumber, theDTCode, "Name", "Id");

                }

                //PrevHIVStatus
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='MotherHIVTestResult' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlPrevHIVStatus, theDTCode, "Name", "Id");
                }
                //PrevPointHIVTesting
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='HIVTestingPoint2' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlPrevPointHIVTesting, theDTCode, "Name", "Id");
                }
                //Pretestcounsellingtesting
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='PretestCounselling' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlPreTestCounselling, theDTCode, "Name", "Id");
                }
                //Posttestcounsellingandtesting
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='PostTestCounselling' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlPosttestcounselling, theDTCode, "Name", "Id");
                }
                //FinalHIVResult
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='FinalHIVResult' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    theDTCode = theDTCode.Select("Name not like 'Disc%'").CopyToDataTable();
                    BindManager.BindCombo(ddlFinalHIVResult, theDTCode, "Name", "Id");
                }
                //FinalHIVResultPartner
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='FinalHIVResultPartner' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    theDTCode = theDTCode.Select("Name not like 'Disc%'").CopyToDataTable();
                    BindManager.BindCombo(ddlPartnerFinalHIVresult, theDTCode, "Name", "Id");
                }
                //PartnerPretestcounsellingtesting
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='PartnerPretestCounselling' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlpartnerPreTestCounselling, theDTCode, "Name", "Id");
                }

                //PartnerPostTestCounselling
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='PartnerPostTestCounselling' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlPartnerPostTestCounselling, theDTCode, "Name", "Id");
                }

                //HIVTestDonetopartner
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='PartnerTestedHIV' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlHIVTestdonetopartner, theDTCode, "Name", "Id");
                }

                //PartnerDNAPCRResult
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='PCRpartner' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlPartnerDNA, theDTCode, "Name", "Id");
                }

                //FamilybeentestedHIV
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='FamilyTestingR' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlFamilybeentestedHIV, theDTCode, "Name", "Id");
                }

                //HistoricMentalHealth
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='HistoricMentalHealth' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlHMHealth, theDTCode, "Name", "Id");
                }

                //CurrentMentalHealth
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='CurrentMentalHealth' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlCMHealth, theDTCode, "Name", "Id");
                }

                //ExperiencedGBV
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='ExperiencedGBV' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    //BindManager.BindCheckedList(chklstGBVExperienced, theDTCode, "Name", "Id");
                    BindManager.CreateCheckedList(pnlGBVExperienced, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");


                }

                //SubstanceAbused
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='ExperiencedSubstanceAbuse' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    //BindManager.BindCheckedList(chklstSubstanceAbused, theDTCode, "Name", "Id");
                    BindManager.CreateCheckedList(pnlSubstanceAbused, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");
                }

                //PrefferedDeliveryMode
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='PrefferedDeliveryMode' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlPreferedmodeofdelivery, theDTCode, "Name", "Id");
                }

                //Referral
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='ReferralANC' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.CreateCheckedList(pnlReferral, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");
                    //BindManager.BindCheckedList(chklstReferral, theDTCode, "Name", "Id");
                }

                //Maturity 
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='Maturity' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlMaturityweeks, theDTCode, "Name", "Id");
                }

                //ModeOfDelivery
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='ModeOfDelivery' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlModeofDelivery, theDTCode, "Name", "Id");
                }

                //GenderofBaby
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='GenderOfBaby' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlGenderofBaby, theDTCode, "Name", "Id");
                }

                //FateofBaby
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='FateOfBaby' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlFateofBaby, theDTCode, "Name", "Id");
                }

                //MartenalBloodGroup
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='MartenalBloodGroup' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlMaternalBloodGroup, theDTCode, "Name", "Id");
                }

                //PartnersBloodGroup
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='PartnersBloodGroup' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlPartnersBloodGroup, theDTCode, "Name", "Id");
                }

                //ChronicIllnessHistory
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='ChronicIllnessHistory' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    //BindManager.BindCheckedList(chklstHistoryChronicIllness, theDTCode, "Name", "Id");
                    BindManager.CreateCheckedList(pnlHistoryChronicIllness, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");

                }

                //Ward Admitted
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='WardAdmitted' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlDiagnosisandPlanWardAdmitted, theDTCode, "Name", "Id");
                }
                //SpecifyCurrentRegmn
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='CurrentRegimenPMTCT' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlSpecifyCurrentRegmn, theDTCode, "Name", "Id");
                }
                //AdherenceCode/Reasonformisseddose
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='AdherenceCodes' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    //BindManager.BindCheckedList(chklstReasonmissdeddose, theDTCode, "Name", "Id");
                    BindManager.CreateCheckedList(pnlReasonmissdeddose, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");
                }
                //AdherenceBarriers
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='AdherenceBarriers' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    //BindManager.BindCheckedList(chklstBarriertoadherence, theDTCode, "Name", "Id");
                    BindManager.CreateCheckedList(pnlBarriertoadherence, theDTCode, "", "onclick");
                }

                //DisclosedHIVStatusTo
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='DisclosedHIVStatusTo' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    //BindManager.BindCheckedList(chklstHIVStatus, theDTCode, "Name", "Id");
                    BindManager.CreateCheckedList(pnlHIVStatus, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");
                }

                //ARTPreparation
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='ARTPreparation' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    //BindManager.BindCheckedList(chklstARTPreparation, theDTCode, "Name", "Id");
                    BindManager.CreateCheckedList(pnlARTPreparation, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");
                }

                //PMTCTARVRegimen
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='PMTCTregimen' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlARVRegimen, theDTCode, "Name", "Id");
                }

                //CTX
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='CTX' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlCTX, theDTCode, "Name", "Id");
                }

                //PMTCTWardadmitted
                theDVDecode = new DataView(theDS.Tables["Mst_PMTCTDecode"]);
                theDVDecode.RowFilter = "CodeName='WardAdmitted' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlWardAdmitted, theDTCode, "Name", "Id");
                }
            }

            theDS.ReadXml(MapPath("..\\XMLFiles\\DrugMasters.con"));
            if (theDS.Tables["Mst_DrugSchedule"] != null)
            {
                //Field VisitType
                theDVDecode = new DataView(theDS.Tables["Mst_DrugSchedule"]);
                theDVDecode.RowFilter = "Name Like 'TT%' and (DeleteFlag = 0 or DeleteFlag IS NULL)";
                theDVDecode.Sort = "ID";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlTTVaccine, theDTCode, "Name", "Id");

                }
            }


        }

        private void SaveCancel(String TabName)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            IQCareMsgBox.NotifyAction("MEI " + TabName + " tab saved successfully!", "MEI Form", false, this, "");
        }

        private void PMTCTSaveCancel(String TabName)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            IQCareMsgBox.NotifyAction("MEI " + TabName + " tab saved successfully!", "MEI Form", false, this, "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';");
        }





        private void GetFormData()
        {
            IKNHMEI KNHMEIManager;
            KNHMEIManager = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
            int ptnVisitID = 0;
            if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                ptnVisitID = Convert.ToInt32(Session["PatientVisitId"]);
            else
            {
                DataSet theDSVisitId = KNHMEIManager.GetKNHANC_Data(ptnVisitID);
                if(theDSVisitId!=null && theDSVisitId.Tables[0].Rows.Count>0)
                    ptnVisitID = Convert.ToInt32(theDSVisitId.Tables[0].Rows[0]["Visit_ID"]);
                else
                    ptnVisitID = 0;
            }            
            //DataSet theDS = KNHMEIManager.GetKNHMEI_Data(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));
            DataSet theDS = KNHMEIManager.GetKNHMEI_Data(Convert.ToInt32(Session["PatientId"]), ptnVisitID);
            //Table 0
            if (theDS.Tables[0].Rows.Count > 0)
            {
                if (theDS.Tables[0].Rows[0]["VisitDate"] != System.DBNull.Value)
                {
                    this.txtVisitDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["VisitDate"]).ToUpper();
                }
                if (theDS.Tables[0].Rows[0]["TypeofVisit"] != System.DBNull.Value)
                {
                    ddlFieldVisitType.SelectedValue = theDS.Tables[0].Rows[0]["TypeofVisit"].ToString();
                }

            }

            //Table 1
            if (theDS.Tables[1].Rows.Count > 0)
            {
                if (theDS.Tables[1].Rows[0]["VisitNumber"] != System.DBNull.Value)
                {
                    this.ddlVisitNumber.SelectedValue = theDS.Tables[1].Rows[0]["VisitNumber"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["HIVTestingPoint2"] != System.DBNull.Value)
                {
                    this.ddlPrevPointHIVTesting.SelectedValue = theDS.Tables[1].Rows[0]["HIVTestingPoint2"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["HIVTestingToday"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["HIVTestingToday"].ToString() == "1")
                    {
                        rdoHIVTestingTodayYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["HIVTestingToday"].ToString() == "0")
                    {
                        rdoHIVTestingTodayNo.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["HIVTestingTodayDone"] != System.DBNull.Value)
                {
                    this.ddlHIVTestDoneToday.SelectedValue = theDS.Tables[1].Rows[0]["HIVTestingTodayDone"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["PretestCounselling"] != System.DBNull.Value)
                {
                    this.ddlPreTestCounselling.SelectedValue = theDS.Tables[1].Rows[0]["PretestCounselling"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["FinalHIVResult"] != System.DBNull.Value)
                {
                    this.ddlFinalHIVResult.SelectedValue = theDS.Tables[1].Rows[0]["FinalHIVResult"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["PostTestCounselling"] != System.DBNull.Value)
                {
                    this.ddlPosttestcounselling.SelectedValue = theDS.Tables[1].Rows[0]["PostTestCounselling"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["PartnerInvolvement"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["PartnerInvolvement"].ToString() == "1")
                    {
                        rdoPatientaccPartnerYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["PartnerInvolvement"].ToString() == "0")
                    {
                        rdoPatientaccPartnerNo.Checked = true;
                    }
                }

                if (theDS.Tables[1].Rows[0]["PartnerPreTestCounselling"] != System.DBNull.Value)
                {
                    this.ddlpartnerPreTestCounselling.SelectedValue = theDS.Tables[1].Rows[0]["PartnerPreTestCounselling"].ToString();
                }

                if (theDS.Tables[1].Rows[0]["FinalHIVResultPartner"] != System.DBNull.Value)
                {
                    this.ddlPartnerFinalHIVresult.SelectedValue = theDS.Tables[1].Rows[0]["FinalHIVResultPartner"].ToString();
                }

                if (theDS.Tables[1].Rows[0]["PartnerPostTestCounselling"] != System.DBNull.Value)
                {
                    this.ddlPartnerPostTestCounselling.SelectedValue = theDS.Tables[1].Rows[0]["PartnerPostTestCounselling"].ToString();
                }

                if (theDS.Tables[1].Rows[0]["DiscordantCouple"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["DiscordantCouple"].ToString() == "1")
                    {
                        rdodiscordantcoupleYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["DiscordantCouple"].ToString() == "0")
                    {
                        rdodiscordantcoupleNo.Checked = true;
                    }
                }

                if (theDS.Tables[1].Rows[0]["PartnerTestedHIV"] != System.DBNull.Value)
                {
                    this.ddlHIVTestdonetopartner.SelectedValue = theDS.Tables[1].Rows[0]["PartnerTestedHIV"].ToString();
                }

                if (theDS.Tables[1].Rows[0]["PCRpartner"] != System.DBNull.Value)
                {
                    this.ddlPartnerDNA.SelectedValue = theDS.Tables[1].Rows[0]["PCRpartner"].ToString();
                }

                if (theDS.Tables[1].Rows[0]["FamilyInformationFilled"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["FamilyInformationFilled"].ToString() == "1")
                    {
                        rdofamilyinformationFilledYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["FamilyInformationFilled"].ToString() == "0")
                    {
                        rdofamilyinformationFilledNo.Checked = true;
                    }
                }

                if (theDS.Tables[1].Rows[0]["FamilyTestingR"] != System.DBNull.Value)
                {
                    this.ddlFamilybeentestedHIV.SelectedValue = theDS.Tables[1].Rows[0]["FamilyTestingR"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["HistoricMentalHealth"] != System.DBNull.Value)
                {
                    this.ddlHMHealth.SelectedValue = theDS.Tables[1].Rows[0]["HistoricMentalHealth"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["OtherHistoricMentalHealth"] != System.DBNull.Value)
                {
                    this.txtHMentalHealth.Text = theDS.Tables[1].Rows[0]["OtherHistoricMentalHealth"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["CurrentMentalHealth"] != System.DBNull.Value)
                {
                    this.ddlCMHealth.SelectedValue = theDS.Tables[1].Rows[0]["CurrentMentalHealth"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["OtherCurrentMentalHealth"] != System.DBNull.Value)
                {
                    this.txtCMentalHealth.Text = theDS.Tables[1].Rows[0]["OtherCurrentMentalHealth"].ToString();
                }

                if (theDS.Tables[1].Rows[0]["GBV"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["GBV"].ToString() == "1")
                    {
                        rdoExperienceanyGBVYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["GBV"].ToString() == "0")
                    {
                        rdoExperienceanyGBVNo.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["SubstanceAbuse"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["SubstanceAbuse"].ToString() == "1")
                    {
                        rdoHIVSubstanceAbusedYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["SubstanceAbuse"].ToString() == "0")
                    {
                        rdoHIVSubstanceAbusedNo.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["PrefferedDeliveryMode"] != System.DBNull.Value)
                {
                    this.ddlPreferedmodeofdelivery.SelectedValue = theDS.Tables[1].Rows[0]["PrefferedDeliveryMode"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["PrefferedDeliverySite"] != System.DBNull.Value)
                {
                    this.txtPreferedSiteDelivery.Text = theDS.Tables[1].Rows[0]["PrefferedDeliverySite"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["AdditionalNotesANC"] != System.DBNull.Value)
                {
                    this.txtPreferedSiteDeliveryAdditionalnotes.Text = theDS.Tables[1].Rows[0]["AdditionalNotesANC"].ToString();
                }

                if (theDS.Tables[1].Rows[0]["YearBaby"] != System.DBNull.Value)
                {
                    this.txtYrofDelivery.Text = theDS.Tables[1].Rows[0]["YearBaby"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["PlaceOfDelivery"] != System.DBNull.Value)
                {
                    this.txtPlaceofDelivery.Text = theDS.Tables[1].Rows[0]["PlaceOfDelivery"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["ProfileMaturity"] != System.DBNull.Value)
                {
                    this.ddlMaturityweeks.SelectedItem.Text = theDS.Tables[1].Rows[0]["ProfileMaturity"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["DurationOfLabour"] != System.DBNull.Value)
                {
                    this.txtLabourduratioin.Text = theDS.Tables[1].Rows[0]["DurationOfLabour"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["ModeOfDelivery"] != System.DBNull.Value)
                {
                    this.ddlModeofDelivery.SelectedValue = theDS.Tables[1].Rows[0]["ModeOfDelivery"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["GenderOfBaby"] != System.DBNull.Value)
                {
                    this.ddlGenderofBaby.SelectedValue = theDS.Tables[1].Rows[0]["GenderOfBaby"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["FateOfBaby"] != System.DBNull.Value)
                {
                    this.ddlFateofBaby.SelectedValue = theDS.Tables[1].Rows[0]["FateOfBaby"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["TetanusVaccine"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["TetanusVaccine"].ToString() == "1")
                    {
                        rdotetanustoxoidyes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["TetanusVaccine"].ToString() == "0")
                    {
                        rdotetanustoxoidno.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["TetanusVaccineDose"] != System.DBNull.Value)
                {
                    ddlTTVaccine.SelectedValue = theDS.Tables[1].Rows[0]["TetanusVaccineDose"].ToString();
                    ddlTTVaccine.Style.Add("display", "block");
                    txtNoTTReason.Style.Add("display", "none");
                }
                if ((theDS.Tables[1].Rows[0]["TetanusVaccineReason"] != System.DBNull.Value) && (theDS.Tables[1].Rows[0]["TetanusVaccineReason"].ToString() != string.Empty))
                {
                    txtNoTTReason.Value = theDS.Tables[1].Rows[0]["TetanusVaccineReason"].ToString();
                    ddlTTVaccine.Style.Add("display", "none");
                    txtNoTTReason.Style.Add("display", "block");
                }

                if (theDS.Tables[1].Rows[0]["MartenalBloodGroup"] != System.DBNull.Value)
                {
                    this.ddlMaternalBloodGroup.SelectedValue = theDS.Tables[1].Rows[0]["MartenalBloodGroup"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["PartnersBloodGroup"] != System.DBNull.Value)
                {
                    this.ddlPartnersBloodGroup.SelectedValue = theDS.Tables[1].Rows[0]["PartnersBloodGroup"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["BloodTransfusion"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["BloodTransfusion"].ToString() == "1")
                    {
                        rdoHistoryBloodTransfusionYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["BloodTransfusion"].ToString() == "0")
                    {
                        rdoHistoryBloodTransfusionNo.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["BloodTransfusionDate"] != System.DBNull.Value)
                {
                    txtBloodTransfusiondt.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[1].Rows[0]["BloodTransfusionDate"]);
                }
                if (theDS.Tables[1].Rows[0]["TwinsHistory"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["TwinsHistory"].ToString() == "1")
                    {
                        rdoHistoryOfTwinsYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["TwinsHistory"].ToString() == "0")
                    {
                        rdoHistoryOfTwinsNo.Checked = true;
                    }
                }

                if (theDS.Tables[1].Rows[0]["RhesusFactor"] != System.DBNull.Value)
                {
                    ddlRhesusFactor.SelectedValue = theDS.Tables[1].Rows[0]["RhesusFactor"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["PresentingComplaintsAdditionalNotes"] != System.DBNull.Value)
                {
                    this.txtPresentingcomplaints.Text = theDS.Tables[1].Rows[0]["PresentingComplaintsAdditionalNotes"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["GeneralAppearance"] != System.DBNull.Value)
                {
                    this.txtGeneralAppearance.Text = theDS.Tables[1].Rows[0]["GeneralAppearance"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["CVS"] != System.DBNull.Value)
                {
                    this.txtCVS.Text = theDS.Tables[1].Rows[0]["CVS"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["RS"] != System.DBNull.Value)
                {
                    this.txtRS.Text = theDS.Tables[1].Rows[0]["RS"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["Breasts"] != System.DBNull.Value)
                {
                    this.txtBreasts.Text = theDS.Tables[1].Rows[0]["Breasts"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["Abdomen"] != System.DBNull.Value)
                {
                    this.txtAbdomen.Text = theDS.Tables[1].Rows[0]["Abdomen"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["VE"] != System.DBNull.Value)
                {
                    this.txtVaginalExamination.Text = theDS.Tables[1].Rows[0]["VE"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["Discharge"] != System.DBNull.Value)
                {
                    this.txtdischarge.Text = theDS.Tables[1].Rows[0]["Discharge"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["Pallor"] != System.DBNull.Value)
                {
                    this.txtPallor.Text = theDS.Tables[1].Rows[0]["Pallor"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["CRMaturity"] != System.DBNull.Value)
                {
                    this.txtMaturity.Text = theDS.Tables[1].Rows[0]["CRMaturity"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["FundalHeight"] != System.DBNull.Value)
                {
                    this.txtFundalHeight.Text = theDS.Tables[1].Rows[0]["FundalHeight"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["Presentation"] != System.DBNull.Value)
                {
                    this.txtPresentation.Text = theDS.Tables[1].Rows[0]["Presentation"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["FoetalHeartRate"] != System.DBNull.Value)
                {
                    this.txtFoetalHeartRate.Text = theDS.Tables[1].Rows[0]["FoetalHeartRate"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["Oedema"] != System.DBNull.Value)
                {
                    this.txtOedema.Text = theDS.Tables[1].Rows[0]["Oedema"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["MotherAtRisk"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["MotherAtRisk"].ToString() == "1")
                    {
                        rdoMotheratriskyes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["MotherAtRisk"].ToString() == "0")
                    {
                        rdoMotheratriskno.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["MotherAtRiskOther"] != System.DBNull.Value)
                {
                    txtmthrriskfactor.Text = theDS.Tables[1].Rows[0]["MotherAtRiskOther"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["ANCPlan"] != System.DBNull.Value)
                {
                    this.txtPlan.Text = theDS.Tables[1].Rows[0]["ANCPlan"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["CRAppointmentDate"] != System.DBNull.Value)
                {
                    this.txtAppointmentDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[1].Rows[0]["CRAppointmentDate"]);
                }
                if (theDS.Tables[1].Rows[0]["CRWardAdmission"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["CRWardAdmission"].ToString() == "1")
                    {
                        rdoAdmittedtowardyes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["CRWardAdmission"].ToString() == "0")
                    {
                        rdoAdmittedtowardno.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["CRWardAdmitted"] != System.DBNull.Value)
                {
                    this.ddlDiagnosisandPlanWardAdmitted.SelectedValue = theDS.Tables[1].Rows[0]["CRWardAdmitted"].ToString();
                }


                if (theDS.Tables[1].Rows[0]["ProgressionInWHOstage"] != System.DBNull.Value)
                {
                    this.UCWHOStage.rdoProgressionInWHOstage.SelectedValue = theDS.Tables[1].Rows[0]["ProgressionInWHOstage"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["Mernarche"] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(theDS.Tables[1].Rows[0]["Mernarche"]) > 1)
                    {
                        this.txtAgeMernarche.Value = theDS.Tables[1].Rows[0]["Mernarche"].ToString();
                        //    UCWHOStage.radbtnMernarcheyes.Checked = true;
                        //}
                        //if (theDS.Tables[1].Rows[0]["Mernarche"].ToString() == "0")
                        //{
                        //    UCWHOStage.radbtnMernarcheno.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["MernarcheDate"] != System.DBNull.Value)
                {
                    this.UCWHOStage.txtmenarchedate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[1].Rows[0]["MernarcheDate"]);
                }
                if (theDS.Tables[1].Rows[0]["tannerstaging"] != System.DBNull.Value)
                {
                    this.UCWHOStage.ddltannerstaging.SelectedValue = theDS.Tables[1].Rows[0]["tannerstaging"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["ARVExposurePMTCT"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["ARVExposurePMTCT"].ToString() == "1")
                    {
                        rdoMothercurrentlyonARVYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["ARVExposurePMTCT"].ToString() == "0")
                    {
                        rdoMothercurrentlyonARVNo.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["CurrentRegimenPMTCT"] != System.DBNull.Value)
                {
                    this.ddlSpecifyCurrentRegmn.SelectedValue = theDS.Tables[1].Rows[0]["CurrentRegimenPMTCT"].ToString();
                    if (this.ddlSpecifyCurrentRegmn.SelectedItem.Text == "Other")
                    {
                        txtotherregimen.Text = theDS.Tables[1].Rows[0]["CurrentregimenPMTCTOther"].ToString();
                    }
                }
                if (theDS.Tables[1].Rows[0]["Multivitamins"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["Multivitamins"].ToString() == "1")
                    {
                        rdoMotherCurrentlyonmultivitaminsyes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["Multivitamins"].ToString() == "0")
                    {
                        rdoMotherCurrentlyonmultivitaminsNo.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["AdherenceExplained"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["AdherenceExplained"].ToString() == "1")
                    {
                        rdoMotherAdherenceAssessmentdoneYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["AdherenceExplained"].ToString() == "0")
                    {
                        rdoMotherAdherenceAssessmentdoneNo.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["MissesAnyDoses2"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["MissesAnyDoses2"].ToString() == "1")
                    {
                        rdoMissedanydosesYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["MissesAnyDoses2"].ToString() == "0")
                    {
                        rdoMissedanydosesNo.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["DosesMissed"] != System.DBNull.Value)
                {
                    this.txtNoofdosesmissed.Text = theDS.Tables[1].Rows[0]["DosesMissed"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["HomeVisits"] != System.DBNull.Value)
                {
                    this.txtNofHomevisits.Text = theDS.Tables[1].Rows[0]["HomeVisits"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["PrioritiseHomeVisits"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["PrioritiseHomeVisits"].ToString() == "1")
                    {
                        rdoPrioritiseHomeVisitYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["PrioritiseHomeVisits"].ToString() == "0")
                    {
                        rdoPrioritiseHomeVisitNo.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["DOTTimes"] != System.DBNull.Value)
                {
                    this.txtDOT.Text = theDS.Tables[1].Rows[0]["DOTTimes"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["AdultDisclosedHIVStatus"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["AdultDisclosedHIVStatus"].ToString() == "1")
                    {
                        rdodisclosedHIVStatusYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["AdultDisclosedHIVStatus"].ToString() == "0")
                    {
                        rdodisclosedHIVStatusNo.Checked = true;
                    }
                }

                if (theDS.Tables[1].Rows[0]["CondomsIssued"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["CondomsIssued"].ToString() == "1")
                    {
                        rdoCondomsIssuedYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["CondomsIssued"].ToString() == "0")
                    {
                        rdoCondomsIssuedNo.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["CounsellingAdditionalNotes"] != System.DBNull.Value)
                {
                    this.txtAdditionalPWPNotes.Text = theDS.Tables[1].Rows[0]["CounsellingAdditionalNotes"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["PwPMessagesGiven"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["PwPMessagesGiven"].ToString() == "1")
                    {
                        rdoPwpMessageGivenYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["PwPMessagesGiven"].ToString() == "0")
                    {
                        rdoPwpMessageGivenNo.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["PMTCTregimen"] != System.DBNull.Value)
                {
                    this.ddlARVRegimen.SelectedValue = theDS.Tables[1].Rows[0]["PMTCTregimen"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["InfantNVPANC"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["InfantNVPANC"].ToString() == "1")
                    {
                        rdoInfantNVPissuedYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["InfantNVPANC"].ToString() == "0")
                    {
                        rdoInfantNVPissuedNo.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["CTX"] != System.DBNull.Value)
                {
                    this.ddlCTX.SelectedValue = theDS.Tables[1].Rows[0]["CTX"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["CTXOther"] != System.DBNull.Value)
                {
                    this.txtctxstopreason.Text = theDS.Tables[1].Rows[0]["CTXOther"].ToString();
                }

                if (theDS.Tables[1].Rows[0]["OtherManagement"] != System.DBNull.Value)
                {
                    this.txtotherMgmt.Text = theDS.Tables[1].Rows[0]["OtherManagement"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["PMTCTAppointmentDate"] != System.DBNull.Value)
                {
                    this.txtPMTCTAppDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[1].Rows[0]["PMTCTAppointmentDate"]).ToString();

                }
                if (theDS.Tables[1].Rows[0]["PMTCTWardAdmission"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["PMTCTWardAdmission"].ToString() == "1")
                    {
                        rdoAdmittedtowardPMTCTYes.Checked = true;
                    }
                    if (theDS.Tables[1].Rows[0]["PMTCTWardAdmission"].ToString() == "0")
                    {
                        rdoAdmittedtowardPMTCTNo.Checked = true;
                    }
                }
                if (theDS.Tables[1].Rows[0]["PMTCTWardAdmitted"] != System.DBNull.Value)
                {
                    this.ddlWardAdmitted.SelectedValue = theDS.Tables[1].Rows[0]["PMTCTWardAdmitted"].ToString();

                }

            }
            //Table 2
            if (theDS.Tables[2].Rows.Count > 0)
            {
                if (theDS.Tables[2].Rows[0]["BPDiastolic"] != System.DBNull.Value)
                {
                    idVitalSign.txtBPDiastolic.Text = theDS.Tables[2].Rows[0]["BPDiastolic"].ToString();
                }

                if (theDS.Tables[2].Rows[0]["BPSystolic"] != System.DBNull.Value)
                {
                    idVitalSign.txtBPSystolic.Text = theDS.Tables[2].Rows[0]["BPSystolic"].ToString();
                }

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
            }
            //Table 3
            if (theDS.Tables[3].Rows.Count > 0)
            {
                if (theDS.Tables[3].Rows[0]["LMP"] != System.DBNull.Value)
                {
                    txtLMPDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[3].Rows[0]["LMP"]).ToUpper();
                }
                if (theDS.Tables[3].Rows[0]["EDD"] != System.DBNull.Value)
                {
                    txtEDD.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[3].Rows[0]["EDD"]).ToUpper();
                }

            }
            //Table 4
            if (theDS.Tables[4].Rows.Count > 0)
            {
                if (theDS.Tables[4].Rows[0]["Parity"] != System.DBNull.Value)
                {
                    ddlparity.SelectedValue = theDS.Tables[4].Rows[0]["Parity"].ToString();
                }
                if (theDS.Tables[4].Rows[0]["Gravidae"] != System.DBNull.Value)
                {
                    ddlGravidae.SelectedValue = theDS.Tables[4].Rows[0]["Gravidae"].ToString();
                }
                if (theDS.Tables[4].Rows[0]["GestAge"] != System.DBNull.Value)
                {
                    txtGestation.Value = theDS.Tables[4].Rows[0]["GestAge"].ToString();
                }
            }
            //Table 5
            if (theDS.Tables[5].Rows.Count > 0)
            {
                if (theDS.Tables[5].Rows[0]["MotherHIVTestResult"] != System.DBNull.Value)
                {
                    ddlPrevHIVStatus.SelectedValue = theDS.Tables[5].Rows[0]["MotherHIVTestResult"].ToString();
                }

                if (theDS.Tables[5].Rows[0]["MotherHIVTestDate"] != System.DBNull.Value)
                {
                    txtLastHIVTest.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[5].Rows[0]["MotherHIVTestDate"]).ToString();
                }
            }


            //Table 6
            if (theDS.Tables[6].Rows.Count > 0)
            {
                if (theDS.Tables[6].Rows[0]["TBFindings"] != System.DBNull.Value)
                {
                    UCTBScreen.ddlTBFindings.SelectedValue = theDS.Tables[6].Rows[0]["TBFindings"].ToString();
                }
                if (theDS.Tables[6].Rows[0]["ContactsScreenedForTB"] != System.DBNull.Value)
                {
                    if (theDS.Tables[6].Rows[0]["ContactsScreenedForTB"].ToString() == "1")
                    {
                        UCTBScreen.rdoContactsScreenedForTBYes.Checked = true;
                    }
                    if (theDS.Tables[6].Rows[0]["ContactsScreenedForTB"].ToString() == "0")
                    {
                        UCTBScreen.rdoContactsScreenedForTBNo.Checked = true;
                    }
                }
                if (theDS.Tables[6].Rows[0]["IfNoSpecifyWhy"] != System.DBNull.Value)
                {
                    UCTBScreen.txtSpecifyWhyContactNotScreenedForTB.Text = theDS.Tables[6].Rows[0]["IfNoSpecifyWhy"].ToString();
                }

                if (theDS.Tables[6].Rows[0]["FacilityPatientReferredTo"] != System.DBNull.Value)
                {
                    UCTBScreen.ddlPatientReferredForTreatment.SelectedValue = theDS.Tables[6].Rows[0]["FacilityPatientReferredTo"].ToString();
                }
            }
            //Table 7
            if (theDS.Tables[7].Rows.Count > 0)
            {
                if (theDS.Tables[7].Rows[0]["WHOStage"] != System.DBNull.Value)
                {
                    UCWHOStage.ddlwhostage1.SelectedValue = theDS.Tables[7].Rows[0]["WHOStage"].ToString();
                }
                if (theDS.Tables[7].Rows[0]["WABStage"] != System.DBNull.Value)
                {
                    UCWHOStage.ddlWABStage.SelectedValue = theDS.Tables[7].Rows[0]["WABStage"].ToString();
                }
            }
            //Table 8
            if (theDS.Tables[8].Rows.Count > 0)
            {
                if (theDS.Tables[8].Rows[0]["Cotrimoxazole"] != System.DBNull.Value)
                {
                    //ddlmthroncotrimoxazole.SelectedValue = theDS.Tables[8].Rows[0]["Cotrimoxazole"].ToString();
                    if (theDS.Tables[8].Rows[0]["Cotrimoxazole"].ToString() == "1")
                        ddlmthroncotrimoxazoleyes.Checked = true;
                    else if (theDS.Tables[8].Rows[0]["Cotrimoxazole"].ToString() == "0")
                        ddlmthroncotrimoxazoleNo.Checked = true;
                }
            }
            //Table 9
            if (theDS.Tables[9].Rows.Count > 0)
            {
                FillCheckBoxListData(theDS.Tables[9]);
                FillCheckBoxListData(theDS.Tables[9], pnlGBVExperienced, "ExperiencedGBV", "Other_Notes");
                FillCheckBoxListData(theDS.Tables[9], pnlSubstanceAbused, "ExperiencedSubstanceAbuse", "Other_Notes");
                FillCheckBoxListData(theDS.Tables[9], pnlReferral, "ReferralANC", "Other_Notes");
                for (int i = 0; i < theDS.Tables[9].Rows.Count; i++)
                {
                    ListItem currentCheckBox = UCTBScreen.cblTBAssessmentICF.Items.FindByValue(theDS.Tables[9].Rows[i]["ValueID"].ToString());
                    if (currentCheckBox != null)
                    {
                        currentCheckBox.Selected = true;
                    }
                }
                FillCheckBoxListData(theDS.Tables[9], pnlHistoryChronicIllness, "ChronicIllnessHistory", "Other_Notes");
                foreach (DataRow theDR in theDS.Tables[9].Rows)
                {
                    if (Convert.ToString(theDR["FieldName"]) == "WHOStageIConditions")
                    {
                        for (int i = 0; i < this.UCWHOStage.gvWHO1.Rows.Count; i++)
                        {
                            Label lblWHOId = (Label)UCWHOStage.gvWHO1.Rows[i].FindControl("lblwho1");
                            CheckBox chkWHOId = (CheckBox)UCWHOStage.gvWHO1.Rows[i].FindControl("Chkwho1");
                            HtmlInputText txtWHODate1 = (HtmlInputText)UCWHOStage.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date");
                            HtmlInputText txtWHODate2 = (HtmlInputText)UCWHOStage.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date1");
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
                        for (int i = 0; i < this.UCWHOStage.gvWHO2.Rows.Count; i++)
                        {
                            Label lblWHOId = (Label)UCWHOStage.gvWHO2.Rows[i].FindControl("lblwho2");
                            CheckBox chkWHOId = (CheckBox)UCWHOStage.gvWHO2.Rows[i].FindControl("Chkwho2");
                            HtmlInputText txtWHODate1 = (HtmlInputText)UCWHOStage.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date");
                            HtmlInputText txtWHODate2 = (HtmlInputText)UCWHOStage.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date1");
                            if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                            {
                                chkWHOId.Checked = true;
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", theDR["DateField1"]);
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", theDR["DateField2"]);
                            }
                        }
                    }

                    else if (Convert.ToString(theDR["FieldName"]) == "WHOStageIIIConditions")
                    {
                        for (int i = 0; i < this.UCWHOStage.gvWHO3.Rows.Count; i++)
                        {
                            Label lblWHOId = (Label)UCWHOStage.gvWHO3.Rows[i].FindControl("lblwho3");
                            CheckBox chkWHOId = (CheckBox)UCWHOStage.gvWHO3.Rows[i].FindControl("Chkwho3");
                            HtmlInputText txtWHODate1 = (HtmlInputText)UCWHOStage.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date");
                            HtmlInputText txtWHODate2 = (HtmlInputText)UCWHOStage.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date1");
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
                        for (int i = 0; i < this.UCWHOStage.gvWHO4.Rows.Count; i++)
                        {
                            Label lblWHOId = (Label)UCWHOStage.gvWHO4.Rows[i].FindControl("lblwho4");
                            CheckBox chkWHOId = (CheckBox)UCWHOStage.gvWHO4.Rows[i].FindControl("Chkwho4");
                            HtmlInputText txtWHODate1 = (HtmlInputText)UCWHOStage.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date");
                            HtmlInputText txtWHODate2 = (HtmlInputText)UCWHOStage.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date1");
                            if (Convert.ToInt32(theDR["ValueId"]) == Convert.ToInt32(lblWHOId.Text))
                            {
                                chkWHOId.Checked = true;
                                txtWHODate1.Value = String.Format("{0:dd-MMM-yyyy}", theDR["DateField1"]);
                                txtWHODate2.Value = String.Format("{0:dd-MMM-yyyy}", theDR["DateField2"]);
                            }
                        }
                    }
                }
                FillCheckBoxListData(theDS.Tables[9], pnlReasonmissdeddose, "AdherenceCodes", "Other_Notes");
                FillCheckBoxListData(theDS.Tables[9], pnlBarriertoadherence, "AdherenceBarriers", "Other_Notes");
                FillCheckBoxListData(theDS.Tables[9], pnlHIVStatus, "DisclosedHIVStatusTo", "Other_Notes");
                FillCheckBoxListData(theDS.Tables[9], pnlARTPreparation, "ARTPreparation", "Other_Notes");
            }
            if (theDS.Tables[10].Rows.Count > 0)
            {
                foreach (DataRow theDR in theDS.Tables[10].Rows)
                {
                    if (theDR["TabName"].ToString() == "KNHPMTCTMEITriage")
                    {
                        btnHTCSave.Enabled = true;
                        btnHTCClose.Enabled = true;
                        btnHTCPrint.Enabled = true;
                    }
                    else if (theDR["TabName"].ToString() == "KNHPMTCTMEIHTC")
                    {
                        btnProfileSave.Enabled = true;
                        btnProfileClose.Enabled = true;
                        btnProfilePrint.Enabled = true;
                    }
                    else if (theDR["TabName"].ToString() == "KNHPMTCTMEIProfile")
                    {
                        btnSaveClinicalReview.Enabled = true;
                        btnCloseClinicalReview.Enabled = true;
                        btnPrintClinicalReview.Enabled = true;
                        btnLab.Enabled = true;
                        btnPharmacylink.Enabled = true;
                    }
                    else if (theDR["TabName"].ToString() == "KNHPMTCTMEIClinicalReview")
                    {
                        btnSavePMTCT.Enabled = true;
                        btnClosePMTCT.Enabled = true;
                        btnPrintPMTCT.Enabled = true;
                        btnPharmacy.Enabled = true;
                    }
                }
            }
            if (theDS.Tables[11].Rows.Count > 0)
            {
                GrdPrevthreeFreq.Columns.Clear();
                BindGridPrevPreg();
                theDS.Tables[11].TableName = "dtPrevpreg";
                GrdPrevthreeFreq.DataSource = theDS.Tables[11];
                GrdPrevthreeFreq.DataBind();
                DataTable theDTGRVPrevPreg = (DataTable)GrdPrevthreeFreq.DataSource;
                ViewState["GridPrevPregData"] = theDTGRVPrevPreg;
            }
        }

        private void GetDataforAutopopulate()
        {
            IKNHMEI KNHMEIManager;
            KNHMEIManager = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
            DataSet theDS = KNHMEIManager.GetKNHMEIData_Autopopulate(Convert.ToInt32(Session["PatientId"]));

            if (theDS.Tables[0].Rows.Count > 0)
            {
                if (theDS.Tables[0].Rows[0]["BPDiastolic"] != System.DBNull.Value)
                {
                    idVitalSign.txtBPDiastolic.Text = theDS.Tables[0].Rows[0]["BPDiastolic"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["BPSystolic"] != System.DBNull.Value)
                {
                    idVitalSign.txtBPSystolic.Text = theDS.Tables[0].Rows[0]["BPSystolic"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["TEMP"] != System.DBNull.Value)
                {
                    idVitalSign.txtTemp.Text = theDS.Tables[0].Rows[0]["TEMP"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["RR"] != System.DBNull.Value)
                {
                    idVitalSign.txtRR.Text = theDS.Tables[0].Rows[0]["RR"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["HR"] != System.DBNull.Value)
                {
                    idVitalSign.txtHR.Text = theDS.Tables[0].Rows[0]["HR"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["Height"] != System.DBNull.Value)
                {
                    idVitalSign.txtHeight.Text = theDS.Tables[0].Rows[0]["Height"].ToString();
                }

                if (theDS.Tables[0].Rows[0]["Weight"] != System.DBNull.Value)
                {
                    idVitalSign.txtWeight.Text = theDS.Tables[0].Rows[0]["Weight"].ToString();
                }
            }

            //Table 1
            if (theDS.Tables[1].Rows.Count > 0)
            {
                if (theDS.Tables[1].Rows[0]["Parity"] != System.DBNull.Value)
                {
                    ddlparity.SelectedValue = theDS.Tables[1].Rows[0]["Parity"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["Gravidae"] != System.DBNull.Value)
                {
                    ddlGravidae.SelectedValue = theDS.Tables[1].Rows[0]["Gravidae"].ToString();
                }
                if (theDS.Tables[1].Rows[0]["GestAge"] != System.DBNull.Value)
                {
                    txtGestation.Value = theDS.Tables[1].Rows[0]["GestAge"].ToString();
                }
            }

            if (theDS.Tables[2].Rows.Count > 0)
            {
                if (theDS.Tables[2].Rows[0]["LMP"] != System.DBNull.Value)
                {
                    txtLMPDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[2].Rows[0]["LMP"]).ToUpper();
                }
                if (theDS.Tables[2].Rows[0]["EDD"] != System.DBNull.Value)
                {
                    txtEDD.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[2].Rows[0]["EDD"]).ToUpper();
                }

            }

            if (theDS.Tables[3].Rows.Count > 0)
            {
                if (theDS.Tables[3].Rows[0]["VisitNumber"] != System.DBNull.Value)
                {
                    this.ddlVisitNumber.SelectedValue = theDS.Tables[3].Rows[0]["VisitNumber"].ToString();
                }
                if (theDS.Tables[3].Rows[0]["PrefferedDeliveryMode"] != System.DBNull.Value)
                {
                    this.ddlPreferedmodeofdelivery.SelectedValue = theDS.Tables[3].Rows[0]["PrefferedDeliveryMode"].ToString();
                }
                if (theDS.Tables[3].Rows[0]["PrefferedDeliverySite"] != System.DBNull.Value)
                {
                    this.txtPreferedSiteDelivery.Text = theDS.Tables[3].Rows[0]["PrefferedDeliverySite"].ToString();
                }
            }
        }

        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Patientid";
            theCol0.DataField = "ptn_pk";
            theCol0.ItemStyle.CssClass = "textstyle";
            grdLatestResults.Columns.Add(theCol0);

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "VisitId";
            theCol1.DataField = "VisitId";
            theCol1.ItemStyle.CssClass = "textstyle";
            grdLatestResults.Columns.Add(theCol1);



            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "ParameterId";
            theCol2.DataField = "ParameterId";
            theCol2.ReadOnly = true;
            grdLatestResults.Columns.Add(theCol2);


            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "TestName";
            theCol3.DataField = "SubTestName";
            grdLatestResults.Columns.Add(theCol3);

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Date";
            theCol4.DataField = "TestDate";
            theCol4.DataFormatString = "{0:dd-MMM-yyyy}";
            theCol4.ReadOnly = true;
            grdLatestResults.Columns.Add(theCol4);

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Result";
            theCol5.DataField = "Result";
            theCol5.ReadOnly = true;
            grdLatestResults.Columns.Add(theCol5);

            CheckBoxField theCol6 = new CheckBoxField();
            theCol6.HeaderText = "Order Today";
            theCol6.DataField = "Order";
            theCol6.ReadOnly = false;
            grdLatestResults.Columns.Add(theCol6);

            //grdLatestResults.Columns[0].Visible = false;
            //grdLatestResults.Columns[1].Visible = false;
            //grdLatestResults.Columns[2].Visible = false;
        }

        private void GetLabResult()
        {
            grdLatestResults.Columns.Clear();
            BindGrid();
            IKNHMEI KNHMEIManager;
            KNHMEIManager = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
            DataSet theDS = KNHMEIManager.GetKNHMEI_LabResult(Convert.ToInt32(Session["PatientId"]));
            grdLatestResults.DataSource = theDS.Tables[0];
            grdLatestResults.DataBind();
            grdLatestResults.Columns[0].Visible = false;
            grdLatestResults.Columns[1].Visible = false;
            grdLatestResults.Columns[2].Visible = false;
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
            if (ddlFieldVisitType.SelectedValue == "0")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Visit Type";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                ddlFieldVisitType.Focus();
                validationCheck = false;
            }
            if (txtLMPDate.Value.Trim() == "" && ddlFieldVisitType.SelectedItem.Text == "Initial only")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -LMP Date";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }
            if (txtEDD.Value.Trim() == "" && ddlFieldVisitType.SelectedItem.Text == "Initial only")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -EDD Date";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }

            if (ddlparity.SelectedValue == "0" && ddlFieldVisitType.SelectedItem.Text == "Initial only")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Parity";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }
            if (ddlGravidae.SelectedValue == "0" && ddlFieldVisitType.SelectedItem.Text == "Initial only")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Gravidae";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }

            if (txtGestation.Value.Trim() == "" && ddlFieldVisitType.SelectedItem.Text == "Initial only")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Gestation";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }
            if (ddlVisitNumber.SelectedValue == "0" && ddlFieldVisitType.SelectedItem.Text == "Initial only")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Visit Number";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }
            //Change by Rahmat for-TETANUS TOXOID VACCINE is mandatory is Yes.
            if (rdotetanustoxoidyes.Checked == true)
            {
                if (ddlTTVaccine.SelectedValue == "0" && ddlFieldVisitType.SelectedItem.Text == "Initial only")
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Tetanus Toxoid Vaccine";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
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

        private bool HTCTabFieldValidation()
        {
            IIQCareSystem IQCareSystemInterface = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSystemInterface.SystemDate();
            IQCareUtils iQCareUtils = new IQCareUtils();
            string validateMessage = "Following values are required:</br>";
            bool validationCheck = true;
            AuthenticationManager auth = new AuthenticationManager();
            if (ddlFinalHIVResult.SelectedValue == "0")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Final HIV Result";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }


            if (rdoPatientaccPartnerYes.Checked == false && rdoPatientaccPartnerNo.Checked == false)
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Is Patient accompanied by partner?";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }

            if (rdoHIVTestingTodayYes.Checked == false && rdoHIVTestingTodayNo.Checked == false)
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Is Client due for HIV Testing?";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
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

        private bool ClinicalReviewTabFieldValidation()
        {
            IIQCareSystem IQCareSystemInterface = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSystemInterface.SystemDate();
            IQCareUtils iQCareUtils = new IQCareUtils();
            string validateMessage = "Following values are required:</br>";
            bool validationCheck = true;
            AuthenticationManager auth = new AuthenticationManager();
            if (ddlFieldVisitType.SelectedValue == "Initial only" && ddlMaternalBloodGroup.SelectedValue == "0")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Maternal Blood Group";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }


            if (ddlFieldVisitType.SelectedValue == "Initial only" && ddlRhesusFactor.SelectedValue == "0")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Rhesus Factor";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
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

        private Boolean ProfileTabfieldValidation()
        {

            IIQCareSystem IQCareSystemInterface = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSystemInterface.SystemDate();
            IQCareUtils iQCareUtils = new IQCareUtils();
            string validateMessage = "Following values are required:</br>";
            bool validationCheck = true;
            AuthenticationManager auth = new AuthenticationManager();
            if (ddlHMHealth.SelectedValue == "0" && ddlFieldVisitType.SelectedItem.Text == "Initial only")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Historic Mental Health";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }
            if (rdoExperienceanyGBVYes.Checked == false && rdoExperienceanyGBVNo.Checked == false && ddlFieldVisitType.SelectedItem.Text == "Initial only")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Experienced any GBV";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }

            if (rdoHIVSubstanceAbusedYes.Checked == false && rdoHIVSubstanceAbusedNo.Checked == false && ddlFieldVisitType.SelectedItem.Text == "Follow Up")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Substance abuse";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }
            if (txtPreferedSiteDelivery.Text == "" && ddlFieldVisitType.SelectedItem.Text == "Initial only")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Preffered Site of Delivery";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }

            if (ddlPreferedmodeofdelivery.SelectedValue == "0" && ddlFieldVisitType.SelectedItem.Text == "Initial only")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Preffered mode of delivery";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
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

        private Boolean PMTCTTabfieldValidation()
        {
            int i = 0;
            string validateMessage = "Following values are required:</br>";
            bool validationCheck = true;
            AuthenticationManager auth = new AuthenticationManager();
            if (rdoMotherAdherenceAssessmentdoneYes.Checked == false && rdoMotherAdherenceAssessmentdoneNo.Checked == false)
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Adherence Assessment done";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }

            if (rdodisclosedHIVStatusNo.Checked == false && rdodisclosedHIVStatusYes.Checked == false)
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Have you disclosed your HIV Status?";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }

            if (ddlARVRegimen.SelectedValue == "0")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -ARV Regimen";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }

            if (ddlCTX.SelectedValue == "0")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -CTX";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                validationCheck = false;
            }
            //TB Assessment
            foreach (ListItem LtItem in UCTBScreen.cblTBAssessmentICF.Items)
            {
                if (LtItem.Selected == true)
                {
                    i = 1;
                }
            }
            if (i == 0)
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -TB assessment (ICF):";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
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

        private DataTable getCheckBoxListItemValues(CheckBoxList cbl)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
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
        private void FillCheckBoxListData(DataTable theDT, Panel thePnl, string FieldName, string theFieldName)
        {
            try
            {
                int i = 0;
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
                                if (y.ID.Contains("pnl"))
                                {
                                    FillCheckBoxListData(theDT, (System.Web.UI.WebControls.Panel)y, FieldName, theFieldName);
                                }
                            }
                        }

                        else
                        {
                            if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                            {
                                String[] ID = ((CheckBox)y).ID.Split('-');
                                if (ID[0] + "-" + ID[1] == thePnl.ID + "-" + DR["ValueID"].ToString() && FieldName == DR["FieldName"].ToString())
                                    ((CheckBox)y).Checked = true;


                                else if ("other-" + ID[2] + "-" + ID[0] + "-" + ID[1] == thePnl.ID + "-" + DR["ValueID"].ToString() && FieldName == DR["FieldName"].ToString())
                                    ((CheckBox)y).Checked = true;
                                i++;
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

            }
        }





        private void showhideANCPMTCT()
        {

        }

        private void showhideInitialOnly()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "stblClientInformation", "show('ClientInformation');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stMT", "show('MT');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPHG", "show('PHG');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPHS", "show('PHS');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stBGP", "show('BGP');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPTPH", "show('PTPH');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stTBS", "show('TBS');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPOH", "show('POH');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stMHPCOF", "show('MHPCOF');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stWS", "show('WS');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stFHI", "show('FHI');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stdivPMTCTinterventions", "show('divPMTCTinterventions');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stspttvaccine", "show('spttvaccine');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "sttdHMH", "show('tdHMH');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "sttdExGBV", "show('tdExGBV');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblVitalSigns", "show('VitalSigns');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stFHI", "show('FHI');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblMHT", "show('MHT');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stHTRT", "show('HTRT');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPHG", "show('PHG');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPHS", "show('PHS');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stTBS", "show('TBS');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPEF", "show('PEF');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stLG", "show('LG');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stDP", "show('DP');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPMTCTDP", "show('PMTCTDP');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stAdhrnc", "show('Adhrnc');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPWP", "show('PWP');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stWS", "show('WS');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stFHI", "show('FHI');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stdivPMTCTinterventions", "show('divPMTCTinterventions');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stTreatP", "show('TreatP');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "sttdCMH", "show('tdCMH');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "sttdSubabuse", "show('tdSubabuse');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stspTbassessment", "show('spTbassessment');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stCR", "show('CR');", true);
        }

        private void showhideFollowUp()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "stblMHT", "show('MHT');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stHTRT", "show('HTRT');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPHG", "show('PHG');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPHS", "show('PHS');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stTBS", "show('TBS');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPEF", "show('PEF');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stLG", "show('LG');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stDP", "show('DP');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPMTCTDP", "show('PMTCTDP');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stAdhrnc", "show('Adhrnc');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stPWP", "show('PWP');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stWS", "show('WS');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stFHI", "show('FHI');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stdivPMTCTinterventions", "show('divPMTCTinterventions');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stTreatP", "show('TreatP');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "sttdCMH", "show('tdCMH');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "sttdSubabuse", "show('tdSubabuse');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stspTbassessment", "show('spTbassessment');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stCR", "show('CR');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stblVitalSigns", "show('VitalSigns');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "stBGP", "show('BGP');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "sttdExGBV", "show('tdExGBV');", true);
        }

        private void addAttributes()
        {

            UCTBScreen.lblTBassessment.CssClass = "required";
            //txtGestation.Attributes.Add("onkeyup", "chkDecimal('" + txtGestation.ClientID + "');");
            rdoHIVTestingTodayYes.Attributes.Add("onclick", "show('trHIVTesting1'), show('trHIVTesting2');");
            rdoHIVTestingTodayNo.Attributes.Add("onclick", "hide('trHIVTesting1'), hide('trHIVTesting2');");

            rdodisclosedHIVStatusYes.Attributes.Add("onclick", "show('tdPnlHIVStatus')");
            rdodisclosedHIVStatusNo.Attributes.Add("onclick", "hide('tdPnlHIVStatus')");

            rdoAdmittedtowardPMTCTYes.Attributes.Add("onclick", "show('tdSpecifyWardAdmitted')");
            rdoAdmittedtowardPMTCTNo.Attributes.Add("onclick", "hide('tdSpecifyWardAdmitted')");

            rdoPatientaccPartnerYes.Attributes.Add("onclick", "show('pap1'), show('pap2');");
            rdoPatientaccPartnerNo.Attributes.Add("onclick", "hide('pap1'), hide('pap2');");
            rdoExperienceanyGBVYes.Attributes.Add("onclick", "show('tdGBVExperienced');");
            rdoExperienceanyGBVNo.Attributes.Add("onclick", "hide('tdGBVExperienced');");
            rdoHIVSubstanceAbusedYes.Attributes.Add("onclick", "show('tdSubstanceAbused');");
            rdoHIVSubstanceAbusedNo.Attributes.Add("onclick", "hide('tdSubstanceAbused');");
            rdoHistoryBloodTransfusionYes.Attributes.Add("onclick", "show('divBloodTransfusion');");
            rdoHistoryBloodTransfusionNo.Attributes.Add("onclick", "hide('divBloodTransfusion');");
            rdoMotheratriskyes.Attributes.Add("onclick", "show('divriskfactor');");
            rdoMotheratriskno.Attributes.Add("onclick", "hide('divriskfactor');");
            rdoMissedanydosesYes.Attributes.Add("onclick", "show('trMisseddoses');");
            rdoMissedanydosesNo.Attributes.Add("onclick", "hide('trMisseddoses');");
            txtYrofDelivery.Attributes.Add("onkeyup", "chkDecimal('" + txtYrofDelivery.ClientID + "');");
            ddlMaturityweeks.Attributes.Add("onkeyup", "chkDecimal('" + ddlMaturityweeks.ClientID + "');");
            txtLabourduratioin.Attributes.Add("onkeyup", "chkDecimal('" + txtLabourduratioin.ClientID + "');");
            txtMaturity.Attributes.Add("onkeyup", "chkDecimal('" + txtMaturity.ClientID + "');");
            txtNoofdosesmissed.Attributes.Add("onkeyup", "chkDecimal('" + txtNoofdosesmissed.ClientID + "');");
            txtNofHomevisits.Attributes.Add("onkeyup", "chkDecimal('" + txtNofHomevisits.ClientID + "');");
            txtDOT.Attributes.Add("onkeyup", "chkDecimal('" + txtDOT.ClientID + "');");
            txtAgeMernarche.Attributes.Add("onkeyup", "chkDecimal('" + txtAgeMernarche.ClientID + "');");
            // CheckUncheklogic(pnlBarriertoadherence);
        }

        private void BindGridPrevPreg()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Patientid";
            theCol0.DataField = "ptn_pk";
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.Visible = false;
            GrdPrevthreeFreq.Columns.Add(theCol0);

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "VisitId";
            theCol1.DataField = "Visit_pk";
            theCol1.Visible = false;
            theCol1.ItemStyle.CssClass = "textstyle";
            GrdPrevthreeFreq.Columns.Add(theCol1);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Year of Delivery";
            theCol2.DataField = "YearofBaby";
            GrdPrevthreeFreq.Columns.Add(theCol2);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Place Of Delivery";
            theCol3.DataField = "PlaceOfDelivery";
            GrdPrevthreeFreq.Columns.Add(theCol3);

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Maturity";
            theCol4.DataField = "Maturity";
            GrdPrevthreeFreq.Columns.Add(theCol4);

            BoundField theCol10 = new BoundField();
            theCol10.HeaderText = "MaturityId";
            theCol10.DataField = "MaturityId";
            theCol10.Visible = false;
            GrdPrevthreeFreq.Columns.Add(theCol10);

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Labour hours";
            theCol5.DataField = "LabourHour";
            GrdPrevthreeFreq.Columns.Add(theCol5);

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "Mode of Delivery";
            theCol6.DataField = "ModeofDelivery";
            GrdPrevthreeFreq.Columns.Add(theCol6);

            BoundField theCol11 = new BoundField();
            theCol11.HeaderText = "ModeofDeliveryId";
            theCol11.DataField = "ModeofDeliveryId";
            theCol11.Visible = false;
            GrdPrevthreeFreq.Columns.Add(theCol11);

            BoundField theCol7 = new BoundField();
            theCol7.HeaderText = "Gender";
            theCol7.DataField = "Gender";
            GrdPrevthreeFreq.Columns.Add(theCol7);

            BoundField theCol12 = new BoundField();
            theCol12.HeaderText = "GenderId";
            theCol12.DataField = "GenderId";
            theCol12.Visible = false;
            GrdPrevthreeFreq.Columns.Add(theCol12);

            BoundField theCol8 = new BoundField();
            theCol8.HeaderText = "Fate";
            theCol8.DataField = "Fate";
            GrdPrevthreeFreq.Columns.Add(theCol8);

            BoundField theCol13 = new BoundField();
            theCol13.HeaderText = "FateId";
            theCol13.DataField = "FateId";
            theCol13.Visible = false;
            GrdPrevthreeFreq.Columns.Add(theCol13);

            CommandField theCol9 = new CommandField();
            theCol9.ButtonType = ButtonType.Link;
            theCol9.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
            theCol9.ShowDeleteButton = true;
            GrdPrevthreeFreq.Columns.Add(theCol9);


        }

        private void RefreshPrevPreg()
        {
            txtYrofDelivery.Text = "";
            txtPlaceofDelivery.Text = "";
            ddlMaturityweeks.SelectedIndex = 0;
            txtLabourduratioin.Text = string.Empty;
            ddlModeofDelivery.SelectedIndex = 0;
            ddlGenderofBaby.SelectedIndex = 0;
            ddlFateofBaby.SelectedIndex = 0;
        }

        public void Authenticate()
        {
            if (Request.QueryString["name"] == "Delete")
            {
                btnTriageSave.Text = "Delete";
                btnTriageSave.Width = Unit.Percentage(9);
                lblSave.Visible = false;
                lblDelete.Visible = true;
                //lblDelete.Attributes.Add("class", "glyphicon glyphicon-floppy-remove");
                //lblDelete.Attributes.Add("style", "margin-left: -3%; margin-right: 2%;vertical-align: sub; color: #fff;");
            }
            AuthenticationManager Authentication = new AuthenticationManager();
            if (Authentication.HasFunctionRight(ApplicationAccess.HEIForm, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnTriageClose.Enabled = false;
                btnTriagePrint.Enabled = false;

            }
            if (Authentication.HasFunctionRight(ApplicationAccess.HEIForm, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnTriageSave.Enabled = false;
                btnTriageClose.Enabled = false;
            }
            if (Authentication.HasFunctionRight(ApplicationAccess.HEIForm, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
            {
                btnTriageSave.Enabled = false;
                btnHTCSave.Enabled = false;
                btnProfileSave.Enabled = false;
                btnSaveClinicalReview.Enabled = false;
                btnSavePMTCT.Enabled = false;
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
                    btnTriageSave.Text = "Delete";
                    btnTriageSave.Enabled = false;
                    btnHTCSave.Enabled = false;
                    btnProfileSave.Enabled = false;
                    btnSaveClinicalReview.Enabled = false;
                    btnSavePMTCT.Enabled = false;
                }
            }

            if (Session["CEndedStatus"] != null)
            {
                if (((DataTable)Session["CEndedStatus"]).Rows.Count > 0)
                {
                    if (((DataTable)Session["CEndedStatus"]).Rows[0]["CareEnded"].ToString() == "1")
                    {
                        btnTriageSave.Enabled = false;
                        btnHTCSave.Enabled = false;
                        btnProfileSave.Enabled = false;
                        btnSaveClinicalReview.Enabled = false;
                        btnSavePMTCT.Enabled = false;
                    }
                }
            }
            if (Convert.ToString(Session["CareEndFlag"]) == "1" && Convert.ToString(Session["CareendedStatus"]) == "1")
            {
                btnTriageSave.Enabled = false;
                btnHTCSave.Enabled = false;
                btnProfileSave.Enabled = false;
                btnSaveClinicalReview.Enabled = false;
                btnSavePMTCT.Enabled = false;
            }
        }



        protected void btnTriageSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Delete")
            {
                int delete = theUtils.DeleteForm("MEI", Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppUserId"]));
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
            DataSet theDSTriage = new DataSet();
            int LocationID = Convert.ToInt32(Session["AppLocationId"]);
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            int visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            Hashtable theHTTriage = new Hashtable();
            theHTTriage.Add("PatientId", PatientID);
            theHTTriage.Add("LocationId", LocationID);
            theHTTriage.Add("visitPk", visitPK);
            theHTTriage.Add("VisitDate", txtVisitDate.Value);
            theHTTriage.Add("FieldVisitType", ddlFieldVisitType.SelectedValue);
            theHTTriage.Add("LMP", txtLMPDate.Value);
            theHTTriage.Add("EDD", txtEDD.Value);
            theHTTriage.Add("Parity", ddlparity.SelectedValue);
            theHTTriage.Add("Gravidae", ddlGravidae.SelectedValue);
            theHTTriage.Add("Gestation", txtGestation.Value == "" ? string.Empty : txtGestation.Value);
            theHTTriage.Add("VisitNumber", ddlVisitNumber.SelectedValue);
            theHTTriage.Add("Temp", idVitalSign.txtTemp.Text == "" ? "999" : idVitalSign.txtTemp.Text);
            theHTTriage.Add("RR", idVitalSign.txtRR.Text == "" ? "999" : idVitalSign.txtRR.Text);
            theHTTriage.Add("HR", idVitalSign.txtHR.Text == "" ? "999" : idVitalSign.txtHR.Text);
            theHTTriage.Add("BPSys", idVitalSign.txtBPSystolic.Text == "" ? "999" : idVitalSign.txtBPSystolic.Text);
            theHTTriage.Add("BPDys", idVitalSign.txtBPDiastolic.Text == "" ? "999" : idVitalSign.txtBPDiastolic.Text);
            theHTTriage.Add("Height", idVitalSign.txtHeight.Text == "" ? "999" : idVitalSign.txtHeight.Text);
            theHTTriage.Add("Weight", idVitalSign.txtWeight.Text == "" ? "999" : idVitalSign.txtWeight.Text);
            theHTTriage.Add("UserId", Session["AppUserId"].ToString());
            theHTTriage.Add("tetanustoxoid", rdotetanustoxoidyes.Checked == true ? 1 : rdotetanustoxoidno.Checked == true ? 0 : 2);
            theHTTriage.Add("tetanustoxoidVaccine", ddlTTVaccine.SelectedValue);
            theHTTriage.Add("tetanustoxoidVaccineNo", txtNoTTReason.Value);
            theHTTriage.Add("Mernarche", txtAgeMernarche.Value);
            GetMultiSelectListItems(ref theDSTriage);

            IKNHMEI KNHMEIManager;
            KNHMEIManager = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
            DataSet theDS = KNHMEIManager.SaveUpdateKNHMEI_TriageTab(theHTTriage, theDSTriage, "Triage");
            Session["PatientVisitId"] = theDS.Tables[0].Rows[0]["VisitId"];
            if (Convert.ToInt32(theDS.Tables[1].Rows[0]["TabId"]) > 0)
            {
                btnHTCSave.Enabled = true;
                btnHTCClose.Enabled = true;
                btnHTCPrint.Enabled = true;
            }
            SaveCancel("Triage");
            tabControl.ActiveTabIndex = 1;
        }

        protected void btnHTCSave_Click(object sender, EventArgs e)
        {
            if (HTCTabFieldValidation() == false)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    btnHTCSave.Enabled = true;
                    btnHTCClose.Enabled = true;
                }
                return;
            }
            int LocationID = Convert.ToInt32(Session["AppLocationId"]);
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            int visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            DataSet theDSHTC = new DataSet();
            Hashtable theHTHTC = new Hashtable();
            theHTHTC.Add("PatientId", PatientID);
            theHTHTC.Add("LocationId", LocationID);
            theHTHTC.Add("visitPk", visitPK);
            theHTHTC.Add("FieldVisitType", ddlFieldVisitType.SelectedValue);
            theHTHTC.Add("PrevHIVStatus", ddlPrevHIVStatus.SelectedValue);
            theHTHTC.Add("PrevPHIVTesting", ddlPrevPointHIVTesting.SelectedValue);
            theHTHTC.Add("LastHIVTest", txtLastHIVTest.Value);
            theHTHTC.Add("PreTestCounseling", ddlPreTestCounselling.SelectedValue);
            theHTHTC.Add("PostTestCounseling", ddlPosttestcounselling.SelectedValue);
            theHTHTC.Add("HIVTestingToday", rdoHIVTestingTodayYes.Checked == true ? 1 : rdoHIVTestingTodayNo.Checked == true ? 0 : 2);
            theHTHTC.Add("HIVTestingTodayDone", ddlHIVTestDoneToday.SelectedValue);
            theHTHTC.Add("FinalHIVResult", ddlFinalHIVResult.SelectedValue);
            theHTHTC.Add("Patientaccompaniedpartner", rdoPatientaccPartnerYes.Checked == true ? 1 : rdoPatientaccPartnerNo.Checked == true ? 0 : 2);
            theHTHTC.Add("partnerpretestcounselling", ddlpartnerPreTestCounselling.SelectedValue);
            theHTHTC.Add("partnerFinalHIVResult", ddlPartnerFinalHIVresult.SelectedValue);
            theHTHTC.Add("partnerPostTestcounselling", ddlPartnerPostTestCounselling.SelectedValue);
            theHTHTC.Add("CoupleDiscordant", rdodiscordantcoupleYes.Checked == true ? 1 : rdodiscordantcoupleNo.Checked == true ? 0 : 2);
            theHTHTC.Add("HIVTestdonetopartner", ddlHIVTestdonetopartner.SelectedValue);
            theHTHTC.Add("PartnersDNAPCRresult", ddlPartnerDNA.SelectedValue);
            theHTHTC.Add("familyinformationFilled", rdofamilyinformationFilledYes.Checked == true ? 1 : rdofamilyinformationFilledNo.Checked == true ? 0 : 2);
            theHTHTC.Add("membersofthefamilybeentested", ddlFamilybeentestedHIV.SelectedValue);
            theHTHTC.Add("UserId", Session["AppUserId"].ToString());

            GetMultiSelectListItems(ref theDSHTC);

            IKNHMEI KNHMEIManager;
            KNHMEIManager = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
            DataSet theDS = KNHMEIManager.SaveUpdateKNHMEI_TriageTab(theHTHTC, theDSHTC, "HTC");
            Session["PatientVisitId"] = theDS.Tables[0].Rows[0]["VisitId"];
            if (Convert.ToInt32(theDS.Tables[1].Rows[0]["TabId"]) > 0)
            {
                btnProfileSave.Enabled = true;
                btnProfileClose.Enabled = true;
                btnProfilePrint.Enabled = true;
            }
            SaveCancel("HTC");
            tabControl.ActiveTabIndex = 2;
        }

        protected void btnProfileSave_Click1(object sender, EventArgs e)
        {

            if (ProfileTabfieldValidation() == false)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    btnProfileClose.Enabled = true;
                    btnProfileSave.Enabled = true;
                }
                return;
            }

            int LocationID = Convert.ToInt32(Session["AppLocationId"]);
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            int visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            DataSet theDSProfile = new DataSet();
            Hashtable theHTProfile = new Hashtable();
            theHTProfile.Add("PatientId", PatientID);
            theHTProfile.Add("LocationId", LocationID);
            theHTProfile.Add("visitPk", visitPK);
            theHTProfile.Add("FieldVisitType", ddlFieldVisitType.SelectedValue);
            theHTProfile.Add("HMHealth", ddlHMHealth.SelectedValue);
            if (ddlHMHealth.SelectedItem.Text == "Other (specify)")
            {
                theHTProfile.Add("OtherHMHealth", txtHMentalHealth.Text);
            }
            else
            {
                theHTProfile.Add("OtherHMHealth", "");
            }
            theHTProfile.Add("CMHealth", ddlCMHealth.SelectedValue);
            if (ddlCMHealth.SelectedItem.Text == "Other")
            {
                theHTProfile.Add("OtherCMHealth", txtCMentalHealth.Text);
            }
            else
            {
                theHTProfile.Add("OtherCMHealth", "");
            }
            theHTProfile.Add("ExperienceanyGBV", rdoExperienceanyGBVYes.Checked == true ? 1 : rdoExperienceanyGBVNo.Checked == true ? 0 : 2);
            theHTProfile.Add("HIVSubstanceAbused", rdoHIVSubstanceAbusedYes.Checked == true ? 1 : rdoHIVSubstanceAbusedNo.Checked == true ? 0 : 2);
            theHTProfile.Add("Preferedmodeofdelivery", ddlPreferedmodeofdelivery.SelectedValue);
            theHTProfile.Add("PreferedSiteDelivery", txtPreferedSiteDelivery.Text);
            theHTProfile.Add("PreferedSiteDeliveryAdditionalnotes", txtPreferedSiteDeliveryAdditionalnotes.Text);
            //theHTProfile.Add("YrofDelivery", txtYrofDelivery.Text);
            //theHTProfile.Add("PlaceofDelivery", txtPlaceofDelivery.Text);
            //theHTProfile.Add("Maturityweeks", ddlMaturityweeks.SelectedItem.Text);
            //theHTProfile.Add("Labourduratioin", txtLabourduratioin.Text == "" ? "0909" : txtLabourduratioin.Text);
            //theHTProfile.Add("ModeofDelivery", ddlModeofDelivery.SelectedValue);
            //theHTProfile.Add("GenderofBaby", ddlGenderofBaby.SelectedValue);
            //theHTProfile.Add("FateofBaby", ddlFateofBaby.SelectedValue);


            theHTProfile.Add("UserId", Session["AppUserId"].ToString());

            GetMultiSelectListItems(ref theDSProfile);

            IKNHMEI KNHMEIManager;
            KNHMEIManager = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
            DataSet theDS = KNHMEIManager.SaveUpdateKNHMEI_TriageTab(theHTProfile, theDSProfile, "Profile");
            Session["PatientVisitId"] = theDS.Tables[0].Rows[0]["VisitId"];
            if (Convert.ToInt32(theDS.Tables[1].Rows[0]["TabId"]) > 0)
            {
                btnSaveClinicalReview.Enabled = true;
                btnCloseClinicalReview.Enabled = true;
                btnPrintClinicalReview.Enabled = true;
                btnLab.Enabled = true;
                btnPharmacylink.Enabled = true;
            }
            SaveCancel("Profile");
            tabControl.ActiveTabIndex = 3;
        }

        protected void btnSaveClinicalReview_Click1(object sender, EventArgs e)
        {
            if (!ClinicalReviewTabFieldValidation())
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    btnSaveClinicalReview.Enabled = true;
                    btnCloseClinicalReview.Enabled = true;
                }
                return;
            }
            ClinicalReviewData();
            btnPharmacy.Enabled = true;
            SaveCancel("Clinical Review");
            tabControl.ActiveTabIndex = 4;
        }
        private void ClinicalReviewData()
        {
            int LocationID = Convert.ToInt32(Session["AppLocationId"]);
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            int visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            DataSet theDSClinicalReview = new DataSet();
            Hashtable theHTClinicalReview = new Hashtable();
            theHTClinicalReview.Add("PatientId", PatientID);
            theHTClinicalReview.Add("LocationId", LocationID);
            theHTClinicalReview.Add("visitPk", visitPK);
            theHTClinicalReview.Add("FieldVisitType", ddlFieldVisitType.SelectedValue);
            theHTClinicalReview.Add("MaternalBloodGroup", ddlMaternalBloodGroup.SelectedValue);
            theHTClinicalReview.Add("PartnersBloodGroup", ddlPartnersBloodGroup.SelectedValue);
            theHTClinicalReview.Add("HistoryBloodTransfusion", rdoHistoryBloodTransfusionYes.Checked == true ? 1 : rdoHistoryBloodTransfusionNo.Checked == true ? 0 : 2);
            if (rdoHistoryBloodTransfusionYes.Checked == true)
            {
                theHTClinicalReview.Add("BloodTransfusiondt", txtBloodTransfusiondt.Value);
            }
            else
            {
                theHTClinicalReview.Add("BloodTransfusiondt", "01-Jan-1900");
            }
            theHTClinicalReview.Add("HistoryOfTwins", rdoHistoryOfTwinsYes.Checked == true ? 1 : rdoHistoryOfTwinsNo.Checked == true ? 0 : 2);
            theHTClinicalReview.Add("Presentingcomplaints", txtPresentingcomplaints.Text);
            theHTClinicalReview.Add("GeneralAppearance", txtGeneralAppearance.Text);
            theHTClinicalReview.Add("CVS", txtCVS.Text);
            theHTClinicalReview.Add("RS", txtRS.Text);
            theHTClinicalReview.Add("Breasts", txtBreasts.Text);
            theHTClinicalReview.Add("Abdomen", txtAbdomen.Text);
            theHTClinicalReview.Add("VaginalExamination", txtVaginalExamination.Text);
            theHTClinicalReview.Add("discharge", txtdischarge.Text);
            theHTClinicalReview.Add("Pallor", txtPallor.Text);
            theHTClinicalReview.Add("Maturity", txtMaturity.Text == "" ? "0909" : txtMaturity.Text);
            theHTClinicalReview.Add("FundalHeight", txtFundalHeight.Text);
            theHTClinicalReview.Add("Presentation", txtPresentation.Text);
            theHTClinicalReview.Add("FoetalHeartRate", txtFoetalHeartRate.Text);
            theHTClinicalReview.Add("Oedema", txtOedema.Text);
            theHTClinicalReview.Add("RhesusFactor", ddlRhesusFactor.SelectedItem.Text);
            theHTClinicalReview.Add("Motheratrisk", rdoMotheratriskyes.Checked == true ? 1 : rdoMotheratriskno.Checked == true ? 0 : 2);
            if (rdoMotheratriskyes.Checked == true)
            {
                theHTClinicalReview.Add("OtherMotheratrisk", txtmthrriskfactor.Text);
            }
            else
            {
                theHTClinicalReview.Add("OtherMotheratrisk", "");
            }
            theHTClinicalReview.Add("Plan", txtPlan.Text);
            theHTClinicalReview.Add("AppointmentDate", txtAppointmentDate.Value == "" ? "01-Jan-1900" : txtAppointmentDate.Value);
            theHTClinicalReview.Add("Admittedtoward", rdoAdmittedtowardyes.Checked == true ? 1 : rdoAdmittedtowardyes.Checked == true ? 0 : 2);
            theHTClinicalReview.Add("DiagnosisandPlanWardAdmitted", ddlDiagnosisandPlanWardAdmitted.SelectedValue);
            theHTClinicalReview.Add("ProgressionInWHOstage", UCWHOStage.rdoProgressionInWHOstage.SelectedValue);
            //theHTClinicalReview.Add("Currentwhostage", UCWHOStage.ddlwhostage1.SelectedValue);
            //theHTClinicalReview.Add("WABStage", UCWHOStage.ddlWABStage.SelectedValue);
            //theHTClinicalReview.Add("Mernarche", UCWHOStage.radbtnMernarcheyes.Checked == true ? 1 : UCWHOStage.radbtnMernarcheno.Checked == true ? 0 : 2);
            //theHTClinicalReview.Add("MernarcheDate", UCWHOStage.txtmenarchedate.Value == "" ? "01-Jan-1900" : UCWHOStage.txtmenarchedate.Value);
            //theHTClinicalReview.Add("tannerstaging", UCWHOStage.ddltannerstaging.SelectedValue);
            theHTClinicalReview.Add("UserId", Session["AppUserId"].ToString());



            //WHO Stage I
            //DataTable DTWHOStageI = new DataTable();
            //DTWHOStageI = GetCheckBoxListcheckedIDs(pnlReferral, "ReferralID", "Referral_Other", 0);

            GetMultiSelectListItems(ref theDSClinicalReview);
            //GetWHOGridData(ref theDSClinicalReview);


            IKNHMEI KNHMEIManager;
            KNHMEIManager = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
            DataSet theDS = KNHMEIManager.SaveUpdateKNHMEI_TriageTab(theHTClinicalReview, theDSClinicalReview, "ClinicalReview");
            Session["PatientVisitId"] = theDS.Tables[0].Rows[0]["VisitId"];
            if (Convert.ToInt32(theDS.Tables[1].Rows[0]["TabId"]) > 0)
            {
                btnSavePMTCT.Enabled = true;
                btnClosePMTCT.Enabled = true;
                btnPrintPMTCT.Enabled = true;
            }
        }

        protected void btnSavePMTCT_Click(object sender, EventArgs e)
        {
            if (PMTCTTabfieldValidation() == false)
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    btnSavePMTCT.Enabled = true;
                    btnClosePMTCT.Enabled = true;
                }
                return;
            }

            int LocationID = Convert.ToInt32(Session["AppLocationId"]);
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            int visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            DataSet theDSPMTCT = new DataSet();
            Hashtable theHTPMTCT = new Hashtable();
            theHTPMTCT.Add("PatientId", PatientID);
            theHTPMTCT.Add("LocationId", LocationID);
            theHTPMTCT.Add("visitPk", visitPK);
            theHTPMTCT.Add("FieldVisitType", ddlFieldVisitType.SelectedValue);
            theHTPMTCT.Add("MothercurrentlyonARV", rdoMothercurrentlyonARVYes.Checked == true ? "1" : rdoMothercurrentlyonARVNo.Checked == true ? "0" : "2");
            theHTPMTCT.Add("SpecifyCurrentRegmn", ddlSpecifyCurrentRegmn.SelectedValue);
            if (ddlSpecifyCurrentRegmn.SelectedItem.Text == "Other")
            {
                theHTPMTCT.Add("SpecifyCurrentRegmnother", txtotherregimen.Text);
            }
            else
            {
                theHTPMTCT.Add("SpecifyCurrentRegmnother", "");
            }
            theHTPMTCT.Add("mthroncotrimoxazole", ddlmthroncotrimoxazoleyes.Checked == true ? "1" : ddlmthroncotrimoxazoleNo.Checked == true ? "0" : "2");
            theHTPMTCT.Add("MotherCurrentlyonmultivitamins", rdoMotherCurrentlyonmultivitaminsyes.Checked == true ? "1" : rdoMotherCurrentlyonmultivitaminsNo.Checked == true ? "0" : "2");
            theHTPMTCT.Add("MotherAdherenceAssessmentdone", rdoMotherAdherenceAssessmentdoneYes.Checked == true ? "1" : rdoMotherAdherenceAssessmentdoneNo.Checked == true ? "0" : "2");
            theHTPMTCT.Add("Missedanydoses", rdoMissedanydosesYes.Checked == true ? "1" : rdoMissedanydosesNo.Checked == true ? "0" : "2");
            theHTPMTCT.Add("Noofdosesmissed", txtNoofdosesmissed.Text == "" ? "0909" : txtNoofdosesmissed.Text);
            theHTPMTCT.Add("NofHomevisits", txtNofHomevisits.Text);
            theHTPMTCT.Add("PrioritiseHomeVisit", rdoPrioritiseHomeVisitYes.Checked == true ? "1" : rdoPrioritiseHomeVisitNo.Checked == true ? "0" : "2");
            theHTPMTCT.Add("DOT", txtDOT.Text == "" ? "0909" : txtDOT.Text);
            theHTPMTCT.Add("disclosedHIVStatus", rdodisclosedHIVStatusYes.Checked == true ? "1" : rdodisclosedHIVStatusNo.Checked == true ? "0" : "2");
            theHTPMTCT.Add("CondomsIssuedYes", rdoCondomsIssuedYes.Checked == true ? "1" : rdoCondomsIssuedNo.Checked == true ? "0" : "2");
            theHTPMTCT.Add("AdditionalPWPNotes", txtAdditionalPWPNotes.Text);
            theHTPMTCT.Add("PwpMessageGiven", rdoPwpMessageGivenYes.Checked == true ? "1" : rdoPwpMessageGivenNo.Checked == true ? "0" : "2");
            theHTPMTCT.Add("ARVRegimen", ddlARVRegimen.SelectedValue);
            theHTPMTCT.Add("InfantNVPissued", rdoInfantNVPissuedYes.Checked == true ? "1" : rdoInfantNVPissuedNo.Checked == true ? "0" : "2");
            theHTPMTCT.Add("CTX", ddlCTX.SelectedValue);
            if (ddlCTX.SelectedItem.Text == "")
            {
                theHTPMTCT.Add("CTXOther", txtctxstopreason.Text);
            }
            else
            {
                theHTPMTCT.Add("CTXOther", "");
            }
            theHTPMTCT.Add("otherMgmt", txtotherMgmt.Text);
            theHTPMTCT.Add("PMTCTAppDate", txtPMTCTAppDate.Value);
            theHTPMTCT.Add("AdmittedtowardPMTCT", rdoAdmittedtowardPMTCTYes.Checked == true ? "1" : rdoAdmittedtowardPMTCTNo.Checked == true ? "0" : "2");
            theHTPMTCT.Add("WardAdmitted", ddlWardAdmitted.SelectedValue);

            // TB Screening
            theHTPMTCT.Add("TBFindings", UCTBScreen.ddlTBFindings.SelectedValue);
            theHTPMTCT.Add("ContactsScreenedForTB", UCTBScreen.rdoContactsScreenedForTBYes.Checked == true ? 1 : UCTBScreen.rdoContactsScreenedForTBNo.Checked == true ? 0 : 2);
            theHTPMTCT.Add("txtSpecifyWhyContactNotScreenedForTB", UCTBScreen.txtSpecifyWhyContactNotScreenedForTB.Text);
            theHTPMTCT.Add("PatientReferredForTreatment", UCTBScreen.ddlPatientReferredForTreatment.SelectedValue);

            theHTPMTCT.Add("IPT", UCTBScreen.rdoLstIPT.SelectedValue);
            theHTPMTCT.Add("ReasonDeclinedIPT", UCTBScreen.ddlReasonDeclinedIPT.SelectedValue);
            theHTPMTCT.Add("OtherReasonDeclinedIPT", UCTBScreen.txtOtherReasonDeclinedIPT.Text);
            theHTPMTCT.Add("INHStartDate", UCTBScreen.txtINHStartDate.Value);
            theHTPMTCT.Add("INHEndDate", UCTBScreen.txtINHEndDate.Value);
            theHTPMTCT.Add("PyridoxineStartDate", UCTBScreen.txtPyridoxineStartDate.Value);
            theHTPMTCT.Add("PyridoxineEndDate", UCTBScreen.txtPyridoxineEndDate.Value);
            theHTPMTCT.Add("AdherenceAddressed", UCTBScreen.AdherenceAddressed);
            theHTPMTCT.Add("missedAnyDoses", UCTBScreen.missedAnyDoses);
            theHTPMTCT.Add("ReferredForAdherence", UCTBScreen.ReferredForAdherence);
            theHTPMTCT.Add("SpecifyOtherTBSideEffects", UCTBScreen.txtSpecifyOtherTBSideEffects.Text);
            //theHTPMTCT.Add("tetanustoxoid", rdotetanustoxoidyes.Checked == true ? 1 : rdotetanustoxoidno.Checked == true ? 0 : 2);
            //theHTPMTCT.Add("tetanustoxoidVaccine", ddlTTVaccine.SelectedValue);
            //theHTPMTCT.Add("tetanustoxoidVaccineNo", txtNoTTReason.Value);

            // Staging
            theHTPMTCT.Add("Currentwhostage", UCWHOStage.ddlwhostage1.SelectedValue);
            theHTPMTCT.Add("WABStage", UCWHOStage.ddlWABStage.SelectedValue);
            //theHTPMTCT.Add("Mernarche", UCWHOStage.radbtnMernarcheyes.Checked == true ? 1 : UCWHOStage.radbtnMernarcheno.Checked == true ? 0 : 2);
            theHTPMTCT.Add("MernarcheDate", UCWHOStage.txtmenarchedate.Value == "" ? "01-Jan-1900" : UCWHOStage.txtmenarchedate.Value);
            theHTPMTCT.Add("tannerstaging", UCWHOStage.ddltannerstaging.SelectedValue);

            theHTPMTCT.Add("UserId", Session["AppUserId"].ToString());

            GetMultiSelectListItems(ref theDSPMTCT);

            IKNHMEI KNHMEIManager;
            KNHMEIManager = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
            DataSet theDS = KNHMEIManager.SaveUpdateKNHMEI_TriageTab(theHTPMTCT, theDSPMTCT, "PMTCT");
            Session["PatientVisitId"] = theDS.Tables[0].Rows[0]["VisitId"];
            PMTCTSaveCancel("PMTCT");
        }

        private void GetMultiSelectListItems(ref DataSet theDSMEI)
        {
            #region Triage Tab
            //Vital Sign Referred To
            DataTable DTVSReferredTo = new DataTable();
            DTVSReferredTo = GetCheckBoxListcheckedIDs(idVitalSign.cblReferredTo);
            DTVSReferredTo.TableName = "dtVS_Rt";
            theDSMEI.Tables.Add(DTVSReferredTo);
            #endregion

            #region Profile Tab
            //GBV
            DataTable DTGBVExperienced = new DataTable();
            DTGBVExperienced = GetCheckBoxListcheckedIDs(pnlGBVExperienced, "GBVExperienced", "GBVExperienced_Other", 0);
            DTGBVExperienced.TableName = "GBVExperience";
            theDSMEI.Tables.Add(DTGBVExperienced);

            //Substance
            DataTable DTSubstanceAbused = new DataTable();
            DTSubstanceAbused = GetCheckBoxListcheckedIDs(pnlSubstanceAbused, "SubstanceID", "Substance_Other", 0);
            DTSubstanceAbused.TableName = "Substance";
            theDSMEI.Tables.Add(DTSubstanceAbused);

            //Referral
            DataTable DTReferral = new DataTable();
            DTReferral = GetCheckBoxListcheckedIDs(pnlReferral, "ReferralID", "Referral_Other", 0);
            DTReferral.TableName = "Referral";
            theDSMEI.Tables.Add(DTReferral);

            //Prev Three Pregnanies
            theDSMEI.Tables.Add((DataTable)ViewState["GridPrevPregData"]);
            #endregion

            #region Clinical Review

            //HistoricalIllness
            DataTable DTHistoryChronicIllness = new DataTable();
            DTHistoryChronicIllness = GetCheckBoxListcheckedIDs(pnlHistoryChronicIllness, "HistoryChronicIllnessID", "HistoryChronicIllness_Other", 0);
            DTHistoryChronicIllness.TableName = "HistoricalIllness";
            theDSMEI.Tables.Add(DTHistoryChronicIllness);

            #endregion

            #region PMTCT

            //Reasonmissdeddose
            DataTable DTReasonmissdeddose = new DataTable();
            DTReasonmissdeddose = GetCheckBoxListcheckedIDs(pnlReasonmissdeddose, "ReasonmissdeddoseID", "Reasonmissdeddose_Other", 0);
            DTReasonmissdeddose.TableName = "Reasonmissdeddose";
            theDSMEI.Tables.Add(DTReasonmissdeddose);

            //Barrier Adherance
            DataTable DTBarriertoadherence = new DataTable();
            DTBarriertoadherence = GetCheckBoxListcheckedIDs(pnlBarriertoadherence, "BarriertoadherenceID", "Barriertoadherence_Other", 0);
            DTBarriertoadherence.TableName = "AdherenceBarriers";
            theDSMEI.Tables.Add(DTBarriertoadherence);

            //HIVStatus
            DataTable DTHIVStatus = new DataTable();
            DTHIVStatus = GetCheckBoxListcheckedIDs(pnlHIVStatus, "HIVStatusID", "HIVStatus_Other", 0);
            DTHIVStatus.TableName = "DisclosedHIVStatusTo";
            theDSMEI.Tables.Add(DTHIVStatus);

            //WHO MultiSelect List
            GetWHOGridData(ref theDSMEI);

            //ARTPreparation
            DataTable DTARTPreparation = new DataTable();
            DTARTPreparation = GetCheckBoxListcheckedIDs(pnlARTPreparation, "ARTPreparationID", "ARTPreparation_Other", 0);
            DTARTPreparation.TableName = "ARTPreparation";
            theDSMEI.Tables.Add(DTARTPreparation);

            //TBAssessment
            DataTable DTTBAssessment = new DataTable();
            DTTBAssessment = getCheckBoxListItemValues(UCTBScreen.cblTBAssessmentICF);
            DTTBAssessment.TableName = "TBAssessment";
            theDSMEI.Tables.Add(DTTBAssessment);

            DataTable DTTBReview = new DataTable();
            DTTBReview = getCheckBoxListItemValues(UCTBScreen.cblReviewChecklist);
            DTTBReview.TableName = "TBReview";
            theDSMEI.Tables.Add(DTTBReview);

            DataTable DTTBSignsOfHepatitis = new DataTable();
            DTTBSignsOfHepatitis = getCheckBoxListItemValues(UCTBScreen.cblSignsOfHepatitis);
            DTTBSignsOfHepatitis.TableName = "TBSignsOfHepatitis";
            theDSMEI.Tables.Add(DTTBSignsOfHepatitis);

            DataTable DTTBStopReason = new DataTable();
            DTTBStopReason = getCheckBoxListItemValues(UCTBScreen.cblStopTBReason);
            DTTBStopReason.TableName = "TBStopReason";
            theDSMEI.Tables.Add(DTTBStopReason);

            #endregion

        }

        private void GetWHOGridData(ref DataSet theDSMEIWHO)
        {
            DataTable DTWHOStage = new DataTable();
            DTWHOStage.Columns.Add("ValueID", typeof(int));
            DTWHOStage.Columns.Add("FieldName", typeof(string));
            DTWHOStage.Columns.Add("Date1", typeof(string));
            DTWHOStage.Columns.Add("Date2", typeof(string));
            for (int i = 0; i < this.UCWHOStage.gvWHO1.Rows.Count; i++)
            {
                Label lblWHOId = (Label)UCWHOStage.gvWHO1.Rows[i].FindControl("lblwho1");
                CheckBox chkWHOId = (CheckBox)UCWHOStage.gvWHO1.Rows[i].FindControl("Chkwho1");
                HtmlInputText txtWHODate1 = (HtmlInputText)UCWHOStage.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date");
                HtmlInputText txtWHODate2 = (HtmlInputText)UCWHOStage.gvWHO1.Rows[i].FindControl("txtCurrentWho1Date1");
                if (chkWHOId.Checked == true)
                {
                    DataRow dr = DTWHOStage.NewRow();
                    dr[0] = Convert.ToInt32(lblWHOId.Text);
                    dr[1] = "WHOStageIConditions";
                    dr[2] = Convert.ToString(txtWHODate1.Value == "" ? "01-Jan-1900" : txtWHODate1.Value);
                    dr[3] = Convert.ToString(txtWHODate2.Value == "" ? "01-Jan-1900" : txtWHODate2.Value);
                    DTWHOStage.Rows.Add(dr);
                }
            }
            for (int i = 0; i < this.UCWHOStage.gvWHO2.Rows.Count; i++)
            {
                Label lblWHOId = (Label)UCWHOStage.gvWHO2.Rows[i].FindControl("lblwho2");
                CheckBox chkWHOId = (CheckBox)UCWHOStage.gvWHO2.Rows[i].FindControl("Chkwho2");
                HtmlInputText txtWHODate1 = (HtmlInputText)UCWHOStage.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date");
                HtmlInputText txtWHODate2 = (HtmlInputText)UCWHOStage.gvWHO2.Rows[i].FindControl("txtCurrentWho2Date1");
                if (chkWHOId.Checked == true)
                {
                    DataRow dr = DTWHOStage.NewRow();
                    dr[0] = Convert.ToInt32(lblWHOId.Text);
                    dr[1] = "WHOStageIIConditions";
                    dr[2] = Convert.ToString(txtWHODate1.Value == "" ? "01-Jan-1900" : txtWHODate1.Value);
                    dr[3] = Convert.ToString(txtWHODate2.Value == "" ? "01-Jan-1900" : txtWHODate2.Value);
                    DTWHOStage.Rows.Add(dr);
                }
            }
            for (int i = 0; i < this.UCWHOStage.gvWHO3.Rows.Count; i++)
            {
                Label lblWHOId = (Label)UCWHOStage.gvWHO3.Rows[i].FindControl("lblwho3");
                CheckBox chkWHOId = (CheckBox)UCWHOStage.gvWHO3.Rows[i].FindControl("Chkwho3");
                HtmlInputText txtWHODate1 = (HtmlInputText)UCWHOStage.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date");
                HtmlInputText txtWHODate2 = (HtmlInputText)UCWHOStage.gvWHO3.Rows[i].FindControl("txtCurrentWho3Date1");
                if (chkWHOId.Checked == true)
                {
                    DataRow dr = DTWHOStage.NewRow();
                    dr[0] = Convert.ToInt32(lblWHOId.Text);
                    dr[1] = "WHOStageIIIConditions";
                    dr[2] = Convert.ToString(txtWHODate1.Value == "" ? "01-Jan-1900" : txtWHODate1.Value);
                    dr[3] = Convert.ToString(txtWHODate2.Value == "" ? "01-Jan-1900" : txtWHODate2.Value);
                    DTWHOStage.Rows.Add(dr);
                }
            }
            for (int i = 0; i < this.UCWHOStage.gvWHO4.Rows.Count; i++)
            {
                Label lblWHOId = (Label)UCWHOStage.gvWHO4.Rows[i].FindControl("lblwho4");
                CheckBox chkWHOId = (CheckBox)UCWHOStage.gvWHO4.Rows[i].FindControl("Chkwho4");
                HtmlInputText txtWHODate1 = (HtmlInputText)UCWHOStage.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date");
                HtmlInputText txtWHODate2 = (HtmlInputText)UCWHOStage.gvWHO4.Rows[i].FindControl("txtCurrentWho4Date1");
                if (chkWHOId.Checked == true)
                {
                    DataRow dr = DTWHOStage.NewRow();
                    dr[0] = Convert.ToInt32(lblWHOId.Text);
                    dr[1] = "WHOStageIVConditions";
                    dr[2] = Convert.ToString(txtWHODate1.Value == "" ? "01-Jan-1900" : txtWHODate1.Value);
                    dr[3] = Convert.ToString(txtWHODate2.Value == "" ? "01-Jan-1900" : txtWHODate2.Value);
                    DTWHOStage.Rows.Add(dr);
                }
            }
            DTWHOStage.TableName = "WHOStage";
            theDSMEIWHO.Tables.Add(DTWHOStage);
        }

        protected void btnClosePMTCT_Click(object sender, EventArgs e)
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

        protected void btnPrintPMTCT_Click(object sender, EventArgs e)
        {

        }

        protected void btnHTCClose_Click(object sender, EventArgs e)
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

        protected void btnHTCPrint_Click(object sender, EventArgs e)
        {

        }

        protected void btnProfileClose_Click1(object sender, EventArgs e)
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

        protected void btnProfilePrint_Click(object sender, EventArgs e)
        {

        }

        protected void btnCloseClinicalReview_Click1(object sender, EventArgs e)
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

        protected void btnPrintClinicalReview_Click(object sender, EventArgs e)
        {

        }

        protected void btnTriageClose_Click(object sender, EventArgs e)
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

        protected void btnLab_Click(object sender, EventArgs e)
        {
            ClinicalReviewData();
            IQCareUtils.Redirect("../Laboratory/frm_Laboratory.aspx?", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");


            //DataTable theDTLabOrdertoday = new DataTable();
            //theDTLabOrdertoday.Columns.Add("Ptn_pk", typeof(int));
            //theDTLabOrdertoday.Columns.Add("LabVisitId", typeof(int));
            //theDTLabOrdertoday.Columns.Add("ParameterID", typeof(int));
            //theDTLabOrdertoday.Columns.Add("PrevResult", typeof(string));
            //theDTLabOrdertoday.Columns.Add("PrevResultDate", typeof(string));
            //foreach (GridViewRow rw in grdLatestResults.Rows)
            //{
            //    CheckBox cb = ((CheckBox)rw.Cells[6].Controls[0]);
            //    if (cb.Checked == true)
            //    {
            //        DataRow dr = theDTLabOrdertoday.NewRow();
            //        dr[0] = Convert.ToInt32(rw.Cells[0].Text);
            //        dr[1] = Convert.ToInt32(rw.Cells[1].Text);
            //        dr[2] = Convert.ToInt32(rw.Cells[2].Text);
            //        dr[3] = Convert.ToString(rw.Cells[4].Text);
            //        dr[4] = Convert.ToString(rw.Cells[5].Text == "&nbsp;" ? "01-Jan-1900" : rw.Cells[5].Text);
            //        theDTLabOrdertoday.Rows.Add(dr);
            //    }

            //}
            //IKNHMEI KNHMEIManager;
            //KNHMEIManager = (IKNHMEI)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHMEI, BusinessProcess.Clinical");
            //int thelab = KNHMEIManager.SaveKNHMEILabResult(theDTLabOrdertoday, Convert.ToInt32(Session["AppUserId"].ToString()), Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));



        }

        protected void btnPharmacylink_Click(object sender, EventArgs e)
        {
            ClinicalReviewData();
            IQCareUtils.Redirect("../PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx?opento=ArtForm&LastRegimenDispensed=True", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
        }

        protected void btnPharmacy_Click(object sender, EventArgs e)
        {
            ClinicalReviewData();
            IQCareUtils.Redirect("../PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx?opento=ArtForm&LastRegimenDispensed=True", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
        }
        protected void ddlFieldVisitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFieldVisitType.SelectedItem.Text == "Initial only")
            {
                //showhideInitialOnly();
                EnableDisableObstreticsHistoryField(true, false);
            }
            else if (ddlFieldVisitType.SelectedItem.Text == "Follow Up")
            {
                showhideFollowUp();
                EnableDisableObstreticsHistoryField(false, true);
                //showhideInitialOnly();
            }
            else if (ddlFieldVisitType.SelectedItem.Text == "ANC PMTCT")
            {
                showhideANCPMTCT();
                EnableDisableObstreticsHistoryField(false, true);
                //showhideInitialOnly();
            }
            else
            {
                EnableDisableObstreticsHistoryField(false, true);
            }
            showhideInitialOnly();
        }
        #region "Enable Disable Field"
        //Created by Rahmat as on 09-Feb-2018 for (PMTCT - MEI>CLINICAL REVIEW>OBSTRETIC HISTORY Info should auto populate from initial form)
        void EnableDisableObstreticsHistoryField(bool flg1, bool flg2)
        {
            ddlMaternalBloodGroup.Enabled = flg1;
            ddlRhesusFactor.Enabled = flg1;
            ddlPartnersBloodGroup.Enabled = flg1;
            pnlHistoryChronicIllness.Enabled = flg1;
            rdoHistoryOfTwinsYes.Disabled = flg2;
            rdoHistoryBloodTransfusionYes.Disabled = flg2;
            txtBloodTransfusiondt.Disabled = flg2;            
        }

        #endregion
        protected void btnPrevthreeFreq_Click(object sender, EventArgs e)
        {
            int VisitId = Convert.ToInt32(Session["PatientVisitId"]) > 0 ? Convert.ToInt32(Session["PatientVisitId"]) : 0;
            if (txtYrofDelivery.Text == "")
            {
                IQCareMsgBox.Show("NoRecordSelected", this);
                return;
            }

            if (txtPlaceofDelivery.Text == "")
            {
                IQCareMsgBox.Show("NoRecordSelected", this);
                return;
            }
            if (ddlMaturityweeks.SelectedValue == "-1")
            {
                IQCareMsgBox.Show("NoRecordSelected", this);
                return;
            }
            DataTable theDTPrev3Freq = new DataTable("dtPrevpreg");
            if (((DataTable)ViewState["GridPrevPregData"]).Rows.Count == 0)
            {
                theDTPrev3Freq.Columns.Add("ptn_pk", typeof(Int32));
                theDTPrev3Freq.Columns.Add("Visit_pk", typeof(Int32));
                theDTPrev3Freq.Columns.Add("YearofBaby", typeof(Int32));
                theDTPrev3Freq.Columns.Add("PlaceOfDelivery", typeof(string));
                theDTPrev3Freq.Columns.Add("Maturity", typeof(string));
                theDTPrev3Freq.Columns.Add("MaturityId", typeof(Int32));
                theDTPrev3Freq.Columns.Add("LabourHour", typeof(string));
                theDTPrev3Freq.Columns.Add("ModeofDelivery", typeof(string));
                theDTPrev3Freq.Columns.Add("ModeofDeliveryId", typeof(Int32));
                theDTPrev3Freq.Columns.Add("Gender", typeof(string));
                theDTPrev3Freq.Columns.Add("GenderId", typeof(Int32));
                theDTPrev3Freq.Columns.Add("Fate", typeof(string));
                theDTPrev3Freq.Columns.Add("FateId", typeof(string));


                DataRow theDR = theDTPrev3Freq.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["YearofBaby"] = txtYrofDelivery.Text;
                theDR["PlaceOfDelivery"] = txtPlaceofDelivery.Text;
                theDR["Maturity"] = ddlMaturityweeks.SelectedItem.Text;
                theDR["MaturityId"] = ddlMaturityweeks.SelectedValue;
                theDR["LabourHour"] = txtLabourduratioin.Text;
                theDR["ModeofDelivery"] = ddlModeofDelivery.SelectedItem.Text;
                theDR["ModeofDeliveryId"] = ddlModeofDelivery.SelectedValue;
                theDR["Gender"] = ddlGenderofBaby.SelectedItem.Text;
                theDR["GenderId"] = ddlGenderofBaby.SelectedValue;
                theDR["Fate"] = ddlFateofBaby.SelectedItem.Text;
                theDR["FateId"] = ddlFateofBaby.SelectedValue;
                theDTPrev3Freq.Rows.Add(theDR);
                GrdPrevthreeFreq.Columns.Clear();
                BindGridPrevPreg();
                RefreshPrevPreg();
                GrdPrevthreeFreq.DataSource = theDTPrev3Freq;
                GrdPrevthreeFreq.DataBind();
                ViewState["GridPrevPregData"] = theDTPrev3Freq;
            }
            else
            {
                theDTPrev3Freq = (DataTable)ViewState["GridPrevPregData"];
                DataRow theDR = theDTPrev3Freq.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["Visit_pk"] = VisitId;
                theDR["YearofBaby"] = txtYrofDelivery.Text;
                theDR["PlaceOfDelivery"] = txtPlaceofDelivery.Text;
                theDR["Maturity"] = ddlMaturityweeks.SelectedItem.Text;
                theDR["MaturityId"] = ddlMaturityweeks.SelectedValue;
                theDR["LabourHour"] = txtLabourduratioin.Text;
                theDR["ModeofDelivery"] = ddlModeofDelivery.SelectedItem.Text;
                theDR["ModeofDeliveryId"] = ddlModeofDelivery.SelectedValue;
                theDR["Gender"] = ddlGenderofBaby.SelectedItem.Text;
                theDR["GenderId"] = ddlGenderofBaby.SelectedValue;
                theDR["Fate"] = ddlFateofBaby.SelectedItem.Text;
                theDR["FateId"] = ddlFateofBaby.SelectedValue;
                theDTPrev3Freq.Rows.Add(theDR);
                GrdPrevthreeFreq.Columns.Clear();
                BindGridPrevPreg();
                RefreshPrevPreg();
                GrdPrevthreeFreq.DataSource = theDTPrev3Freq;
                GrdPrevthreeFreq.DataBind();
                ViewState["GridPrevPregData"] = theDTPrev3Freq;
            }
        }

        protected void GrdPrevthreeFreq_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void GrdPrevthreeFreq_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GrdPrevthreeFreq_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)ViewState["GridPrevPregData"]);
            int r = Convert.ToInt32(e.RowIndex.ToString());
            int Id = -1;
            try
            {
                if (theDT.Rows.Count > 0)
                {

                    if (theDT.Rows[r].HasErrors == false)
                    {
                        if ((theDT.Rows[r]["YearofBaby"] != null) && (theDT.Rows[r]["YearofBaby"] != DBNull.Value))
                        {
                            if (theDT.Rows[r]["YearofBaby"].ToString() != "")
                            {
                                Id = Convert.ToInt32(theDT.Rows[r]["YearofBaby"]);
                                theDT.Rows[r].Delete();
                                theDT.AcceptChanges();
                                ViewState["GridPrevPregData"] = theDT;
                                GrdPrevthreeFreq.Columns.Clear();
                                BindGridPrevPreg();
                                RefreshPrevPreg();
                                GrdPrevthreeFreq.DataSource = (DataTable)ViewState["GridPrevPregData"];
                                GrdPrevthreeFreq.DataBind();
                                IQCareMsgBox.Show("DeleteSuccess", this);
                            }
                        }
                    }
                }
                else
                {
                    GrdPrevthreeFreq.Visible = false;
                    RefreshPrevPreg();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        protected void btnHideClick_Click(object sender, EventArgs e)
        {
            showhideInitialOnly();
        }



    }
}