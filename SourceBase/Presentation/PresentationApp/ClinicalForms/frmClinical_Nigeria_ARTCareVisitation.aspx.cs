using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Interface.Clinical;
using Interface.Security;
using Application.Presentation;
using Application.Common;
using Application.Interface;
using System.Text;
using Interface.Administration;

namespace PresentationApp.ClinicalForms
{
    public partial class frmClinical_Nigeria_ARTCareVisitation : BasePage
    {
        int PatientID, LocationID, visitPK = 0;
        string chktrueother = "";
        int chktrueothervalue = 0;
        Hashtable htNigeriaArtCareParameters;
        DataTable DTCheckedIds;
        protected void Page_Init(object sender, EventArgs e)
        {
            BindLists();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "ART Care Visitation Form";
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "ART Care Visitation Form";
            AddAttributes();
            BMIAttributes();
            if (!IsPostBack)
            {
                UserControlKNH_NextAppointment.lblTCA.Text = "To return again?";
                UserControlKNH_NextAppointment.rdoTCAYes.Checked = true;
                ARTCareVisitation();
            }
           Authenticate();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (ddlFamilyPanningStatus.SelectedItem.Text == "ONFP=on Family Planning")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "divFamilyPlanningMethod", "show('divFamilyPlanningMethod');", true);
            }
            if (ddlTBStatus.SelectedItem.Text == "4=TB Rx")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "tbCardNo", "show('tbCardNo');", true);
            }
            if (ddlarvdrugadhere.SelectedItem.Text == "Fair" || ddlarvdrugadhere.SelectedItem.Text == "Poor")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "divARVAdherence", "show('divARVAdherence');", true);
            }
            if (ddlCotrimoxazoleAdhere.SelectedItem.Text == "Fair" || ddlCotrimoxazoleAdhere.SelectedItem.Text == "Poor")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "divCotrimoxazole", "show('divCotrimoxazole');", true);
            }
            if (DDLINH.SelectedItem.Text == "Fair" || DDLINH.SelectedItem.Text == "Poor")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "divINH", "show('divINH');", true);
            }
            if (ddlsubsituationInterruption.SelectedValue.ToString() == "99" && ddlArvTherapyStopCode.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "arvTherapyStop", "show('arvTherapyStop');", true);
            }
            if (ddlsubsituationInterruption.SelectedValue.ToString() == "99" && ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other") && txtarvTherapyStopCodeOtherName.Value == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "arvTherapyStop", "show('arvTherapyStop');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "otherarvTherapyStopCode", "show('otherarvTherapyStopCode');", true);
            }
            if (ddlsubsituationInterruption.SelectedValue.ToString() == "98" && ddlArvTherapyChangeCode.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onTherapyChange", "show('arvTherapyChange');", true);
            }
            if (ddlsubsituationInterruption.SelectedValue.ToString() == "98" && ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
            {
                    ScriptManager.RegisterStartupScript(this, GetType(), "onTherapyChange_1", "show('arvTherapyChange');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "otherarvTherapyChangeCode", "show('otherarvTherapyChangeCode');", true);
            }
            if (UserControlKNH_NextAppointment.rdoTCANo.Checked == true)
            {
            ScriptManager.RegisterStartupScript(this, GetType(), "trCareEnd", "show('trCareEnd');", true);
            }
            else if (UserControlKNH_NextAppointment.rdoTCAYes.Checked == true)
            {
            ScriptManager.RegisterStartupScript(this, GetType(), "trNextAppointment", "show('trNextAppointment');", true);
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "otherPnlFamilyPlanningMethod", "ShowPnlforOther('ctl00_IQCareContentPlaceHolder_otherPnlFamilyPlanningMethod','PnlFamilyPlanningMethod');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "otherPnlReferredTo", "ShowPnlforOther('ctl00_IQCareContentPlaceHolder_otherPnlReferredTo','PnlReferredTo');", true);

        }
        public void Authenticate()
        {
            if (Request.QueryString["name"] == "Delete")
            { btnSave.Text = "Delete";
            btnDataQualityCheck.Visible = false;
            }
            AuthenticationManager Authentication = new AuthenticationManager();
            if (Authentication.HasFunctionRight(ApplicationAccess.NigeriaARTCareVisitation, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;

            }
            if (Authentication.HasFunctionRight(ApplicationAccess.NigeriaARTCareVisitation, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnSave.Enabled = false;
                btnDataQualityCheck.Enabled = false;
            }
            else if (Request.QueryString["name"] == "Delete")
            {
                if (Authentication.HasFunctionRight(ApplicationAccess.NigeriaARTCareVisitation, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {

                    int PatientID = Convert.ToInt32(Session["PatientId"]);
                    string theUrl = "";
                    theUrl = string.Format("{0}", "frmClinical_DeleteForm.aspx");
                    Response.Redirect(theUrl);
                }
                else if (Authentication.HasFunctionRight(ApplicationAccess.NigeriaARTCareVisitation, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                {
                    btnSave.Text = "Delete";
                    btnSave.Enabled = false;
                    btnDataQualityCheck.Visible = false;
                }
            }

            if (Session["CEndedStatus"] != null)
            {
                if (((DataTable)Session["CEndedStatus"]).Rows.Count > 0)
                {
                    if (((DataTable)Session["CEndedStatus"]).Rows[0]["CareEnded"].ToString() == "1")
                    {
                        btnSave.Enabled = false;
                        btnDataQualityCheck.Enabled = false;
                    }
                }
            }
            if (Convert.ToString(Session["CareEndFlag"]) == "1" && Convert.ToString(Session["CareendedStatus"]) == "1")
            {
                btnSave.Enabled = true;
                btnDataQualityCheck.Enabled = true;
            }
        }
        private void BMIAttributes()
        {
            txtPhysWeight.Attributes.Add("OnBlur", "isBetween('" + txtPhysWeight.ClientID + "', '" + "physWeight" + "', '" + 0 + "', '" + 225 + "'); CalculateBMI('" + txtBMI.ClientID + "','" + txtPhysWeight.ClientID + "','" + txtPhysHeight.ClientID + "');");
            txtPhysHeight.Attributes.Add("OnBlur", "isBetween('" + txtPhysHeight.ClientID + "', '" + "physHeight" + "', '" + 0 + "', '" + 250 + "'); CalculateBMI('" + txtBMI.ClientID + "','" + txtPhysWeight.ClientID + "','" + txtPhysHeight.ClientID + "');");
        }
        private void BindLists()
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            DataView theDVDecode = new DataView();
            DataTable theDTCode = new DataTable();
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            if (theDS.Tables["Mst_ModDecode"] != null)
            {
                //Family Planning Status
                theDVDecode = new DataView(theDS.Tables["Mst_ModDecode"]);
                theDVDecode.RowFilter = "CodeId=2 and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlFamilyPanningStatus, theDTCode, "Name", "Id");
                }
            }
            if (theDS.Tables["Mst_Decode"] != null)
            {
                //Family Planning Methods
                theDVDecode = new DataView(theDS.Tables["Mst_Decode"]);
                theDVDecode.RowFilter = "CodeName='FamilyPlanningMethods-Nigeria' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.CreateCheckedList(PnlFamilyPlanningMethod, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");
                }

                //Functional Status
                theDVDecode = new DataView(theDS.Tables["Mst_Decode"]);
                theDVDecode.RowFilter = "CodeName='WAB Stage' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlFunctionalStatus, theDTCode, "Name", "Id");
                }

                //WHO Stage
                theDVDecode = new DataView(theDS.Tables["Mst_Decode"]);
                theDVDecode.RowFilter = "CodeName='WHO Stage' and moduleId=0 and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(DDLWHOStage, theDTCode, "Name", "Id");
                }

                //TB Status
                theDVDecode = new DataView(theDS.Tables["Mst_Decode"]);
                theDVDecode.RowFilter = "CodeName='TBStatus-Nigeria' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlTBStatus, theDTCode, "Name", "Id");
                }
                //ARV Drug Adherance
                theDVDecode = new DataView(theDS.Tables["Mst_Decode"]);
                theDVDecode.RowFilter = "CodeName='ARVDrugsAdherence-Nigeria' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlarvdrugadhere, theDTCode, "Name", "Id");
                }
                //Cotrimoxable Adherance
                theDVDecode = new DataView(theDS.Tables["Mst_Decode"]);
                theDVDecode.RowFilter = "CodeName='ARVDrugsAdherence-Nigeria' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlCotrimoxazoleAdhere, theDTCode, "Name", "Id");
                }
                //INH Adherance
                theDVDecode = new DataView(theDS.Tables["Mst_Decode"]);
                theDVDecode.RowFilter = "CodeName='ARVDrugsAdherence-Nigeria' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(DDLINH, theDTCode, "Name", "Id");
                }
                //Drugs and Substitution
                theDVDecode = new DataView(theDS.Tables["Mst_Decode"]);
                theDVDecode.RowFilter = "CodeName='ARV Therapy Plan' and id < 100 and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlsubsituationInterruption, theDTCode, "Name", "Id");
                }
                //Referrals and Consultantions
                theDVDecode = new DataView(theDS.Tables["Mst_Decode"]);
                theDVDecode.RowFilter = "CodeName='ReferredTo-Nigeria' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.CreateCheckedList(PnlReferredTo, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");
                }
            }
            if (theDS.Tables["mst_HivDisease"] != null)
            {
                //Other OIs/Other Problem
                theDVDecode = new DataView(theDS.Tables["mst_HivDisease"]);
                theDVDecode.RowFilter = "SectionName='OtherOIs/OtherProblems-Nigeria' and (DeleteFlag = 0 or DeleteFlag IS NULL)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.CreateCheckedList(PnlOIsOtherProblems, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");
                }
            }
            if (theDS.Tables["mst_symptom"] != null)
            {
                //Noted Side Effects
                theDVDecode = new DataView(theDS.Tables["mst_symptom"]);
                theDVDecode.RowFilter = "SymptomCategoryName='NotedSideEffects-Nigeria' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.CreateCheckedList(PnlNotedSideEffects, theDTCode, "", "onclick");
                }
            }


            if (theDS.Tables["mst_Reason"] != null)
            {
                //Noted Side Effects
                theDVDecode = new DataView(theDS.Tables["mst_Reason"]);
                theDVDecode.RowFilter = "CategoryName='ARVWhy-Nigeria' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.CreateCheckedList(PnlARVwhypoorfair, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");
                }
                theDVDecode = new DataView(theDS.Tables["mst_Reason"]);
                theDVDecode.RowFilter = "CategoryName='CotrimWhy-Nigeria' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.CreateCheckedList(PnlCotrimoxazolewhypoorfair, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");
                }

                theDVDecode = new DataView(theDS.Tables["mst_Reason"]);
                theDVDecode.RowFilter = "CategoryName='INHWhy-Nigeria' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.CreateCheckedList(PnlINHWhyPoorFair, theDTCode, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");
                }

                theDVDecode = new DataView(theDS.Tables["mst_Reason"]);
                theDVDecode.RowFilter = "CategoryName='Change/StopRegimen-Nigeria' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlArvTherapyChangeCode, theDTCode, "Name", "Id");
                }

                theDVDecode = new DataView(theDS.Tables["mst_Reason"]);
                theDVDecode.RowFilter = "CategoryName='Change/StopRegimen-Nigeria' and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVDecode.Sort = "SRNo";
                if (theDVDecode.Table != null)
                {
                    theDTCode = (DataTable)theUtils.CreateTableFromDataView(theDVDecode);
                    BindManager.BindCombo(ddlArvTherapyStopCode, theDTCode, "Name", "Id");
                }
            }
            //BindUserDropdown(DDSignature, String.Empty);
            BindUserDropdown(DDSignature, Session["AppUserId"].ToString());
        }
        protected void ARTCareVisitation()
        {
            INigeriaARTCareVisitation ACVManager;
            ACVManager = (INigeriaARTCareVisitation)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNigeriaARTCareVisitation, BusinessProcess.Clinical");
            try
            {
                PatientID = Convert.ToInt32(Session["PatientId"]);
                if (Session["PatientSex"].ToString() == "Female" && Convert.ToDecimal(Session["PatientAge"]) > 12)
                {
                    trPregnant.Visible = true;
                }

                if (Convert.ToDecimal(Session["PatientAge"]) >= 12) { tdFamilyPlanning.Visible = true; }
                if (Convert.ToDecimal(Session["PatientAge"]) > 14) { txtBPSystolic.Enabled = true; }

                    
                DataSet theDS = ACVManager.GetNigeriaPatientARTCareVisitation(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]));
                if (theDS.Tables[0].Rows.Count > 0 && theDS.Tables[0].Rows[0]["Visit_Id"] != System.DBNull.Value)
                {
                    Session["PatientVisitId"] = theDS.Tables[0].Rows[0]["Visit_Id"].ToString();
                    visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                }
                else
                    Session["PatientVisitId"] = 0;

                if (theDS.Tables[0].Rows.Count > 0)
                {
                    if (theDS.Tables[0].Rows[0]["VisitDate"] != System.DBNull.Value)
                    {
                        this.txtVisitDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["VisitDate"]).ToUpper();
                    }
                    if (theDS.Tables[0].Rows[0]["DataQuality"] != System.DBNull.Value && Convert.ToInt32(theDS.Tables[0].Rows[0]["DataQuality"]) == 1)
                    {
                        btnDataQualityCheck.CssClass = "greenbutton";
                    }
                    if (theDS.Tables[0].Rows[0]["Signature"] != System.DBNull.Value)
                    {
                        DDSignature.SelectedValue = theDS.Tables[0].Rows[0]["Signature"].ToString();
                    }
                    
                }
                if (theDS.Tables[1].Rows.Count > 0) 
                {
                    if (theDS.Tables[1].Rows[0]["ScheduledAppt"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[1].Rows[0]["ScheduledAppt"].ToString() == "1")
                        {
                            chkifschedule.Checked = true;
                        }
                    }
                    if (theDS.Tables[1].Rows[0]["DurationART"] != System.DBNull.Value)
                    {
                        txtARTStart.Text = theDS.Tables[1].Rows[0]["DurationART"].ToString();
                    }

                    if (theDS.Tables[1].Rows[0]["DurationCurrentRegimen"] != System.DBNull.Value)
                    {
                        txtStartingCurrentRegimen.Text = theDS.Tables[1].Rows[0]["DurationCurrentRegimen"].ToString();
                    }

                    if (theDS.Tables[1].Rows[0]["HospitalizedNumberOfDays"] != System.DBNull.Value)
                    {
                        txtNumOfDaysHospitalized.Text = theDS.Tables[1].Rows[0]["HospitalizedNumberOfDays"].ToString();
                    }

                    if (theDS.Tables[1].Rows[0]["TCA"].ToString() == "1")
                    {
                        this.UserControlKNH_NextAppointment.rdoTCAYes.Checked = true;
                        //TCA = "1";
                    }
                    else if (theDS.Tables[1].Rows[0]["TCA"].ToString() == "0")
                    {
                        this.UserControlKNH_NextAppointment.rdoTCANo.Checked = true;
                        //TCA = "0";
                    }
                    else
                    {
                        this.UserControlKNH_NextAppointment.rdoTCAYes.Checked = false;
                        this.UserControlKNH_NextAppointment.rdoTCANo.Checked = false;
                    }
                }
                if (theDS.Tables[2].Rows.Count > 0)
                {
                    double theWeight = 0;
                    double theHeight = 0;
                    if (theDS.Tables[2].Rows[0]["Weight"] != System.DBNull.Value)
                    {
                        txtPhysWeight.Text = theDS.Tables[2].Rows[0]["Weight"].ToString();
                        theWeight = Convert.ToDouble(txtPhysWeight.Text);
                    }
                    if (theDS.Tables[2].Rows[0]["Height"] != System.DBNull.Value)
                    {
                        txtPhysHeight.Text = theDS.Tables[2].Rows[0]["Height"].ToString();
                        theHeight = Convert.ToDouble(txtPhysHeight.Text);
                    }
                    if (theDS.Tables[2].Rows[0]["BPSystolic"] != System.DBNull.Value)
                    {
                        txtBPSystolic.Text = theDS.Tables[2].Rows[0]["BPSystolic"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["BPDiastolic"] != System.DBNull.Value)
                    {
                       txtBPDiastolic.Text = theDS.Tables[2].Rows[0]["BPDiastolic"].ToString();
                    }
                    if (theDS.Tables[2].Rows[0]["FunctionalStatus"] != System.DBNull.Value)
                    {
                        ddlFunctionalStatus.SelectedValue = theDS.Tables[2].Rows[0]["FunctionalStatus"].ToString();
                    }
                    double theBMI = theWeight / (theHeight / 100 * theHeight / 100);
                    txtBMI.Text = Convert.ToString(Math.Round(theBMI, 2));
                }
                if (theDS.Tables[3].Rows.Count > 0)
                {
                    if (theDS.Tables[3].Rows[0]["Pregnant"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[3].Rows[0]["Pregnant"].ToString() == "0")
                        {
                            PregnantNo.Checked = true;
                        }
                        else if (theDS.Tables[3].Rows[0]["Pregnant"].ToString() == "1")
                        {
                            PregnantYes.Checked = true;
                        }
                        else if (theDS.Tables[3].Rows[0]["Pregnant"].ToString() == "2")
                        {
                            PregnantUnknown.Checked = true;
                        }
                    }
                    if (theDS.Tables[3].Rows[0]["EDD"] != System.DBNull.Value)
                    {
                        txtEDD.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[3].Rows[0]["EDD"]).ToUpper();
                    }
                    if (theDS.Tables[3].Rows[0]["PMTCTLink"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[3].Rows[0]["PMTCTLink"].ToString() == "1")
                        {
                            ChkonPMTCT.Checked = true;
                        }
                    }
                }

                if (theDS.Tables[4].Rows.Count > 0)
                {
                    if (theDS.Tables[4].Rows[0]["FamilyPlanningStatus"] != System.DBNull.Value)
                    {
                        ddlFamilyPanningStatus.SelectedValue = theDS.Tables[4].Rows[0]["FamilyPlanningStatus"].ToString();
                        if (ddlFamilyPanningStatus.SelectedItem.Text == "ONFP=on Family Planning")
                        {
                            string script = "";
                            script = "<script language = 'javascript' id = 'FP'>\n";
                            script += "show('divFamilyPlanningMethod');\n";
                            script += "</script>\n";
                            RegisterStartupScript("FP", script);
                        }
                    }
                }
                if (theDS.Tables[5].Rows.Count > 0)
                {
                    if (theDS.Tables[5].Rows[0]["WHOStage"] != System.DBNull.Value)
                    {
                        DDLWHOStage.SelectedValue = theDS.Tables[5].Rows[0]["WHOStage"].ToString(); 
                    }
                }
                if (theDS.Tables[6].Rows.Count > 0)
                {
                    if (theDS.Tables[6].Rows[0]["TBStatus"] != System.DBNull.Value)
                    {
                        ddlTBStatus.SelectedValue = theDS.Tables[6].Rows[0]["TBStatus"].ToString();
                    }
                    if (theDS.Tables[6].Rows[0]["TBCardNo"] != System.DBNull.Value)
                    {
                        txtTBCardNo.Value = theDS.Tables[6].Rows[0]["TBCardNo"].ToString();
                        string script = "";
                        script = "<script language = 'javascript' id = 'TCN'>\n";
                        script += "show('tbCardNo');\n";
                        script += "</script>\n";
                        RegisterStartupScript("TCN", script);
                    }
                }
                if (theDS.Tables[7].Rows.Count > 0)
                {
                    if (theDS.Tables[7].Rows[0]["ARVAdherenceLevel"] != System.DBNull.Value)
                    {
                        ddlarvdrugadhere.SelectedValue = theDS.Tables[7].Rows[0]["ARVAdherenceLevel"].ToString();
                        if (ddlarvdrugadhere.SelectedItem.Text == "Fair" || ddlarvdrugadhere.SelectedItem.Text == "Poor")
                        {
                            string script = "";
                            script = "<script language = 'javascript' id = 'AL'>\n";
                            script += "show('divARVAdherence');\n";
                            script += "</script>\n";
                            RegisterStartupScript("AL", script);
                        }
                    }
                    if (theDS.Tables[7].Rows[0]["CotrimoxazoleAdhere"] != System.DBNull.Value)
                    {
                        ddlCotrimoxazoleAdhere.SelectedValue = theDS.Tables[7].Rows[0]["CotrimoxazoleAdhere"].ToString();
                        if (ddlCotrimoxazoleAdhere.SelectedItem.Text == "Fair" || ddlCotrimoxazoleAdhere.SelectedItem.Text == "Poor")
                        {
                            string script = "";
                            script = "<script language = 'javascript' id = 'CL'>\n";
                            script += "show('divCotrimoxazole');\n";
                            script += "</script>\n";
                            RegisterStartupScript("CL", script);
                        }

                    }
                    if (theDS.Tables[7].Rows[0]["INHAdherence"] != System.DBNull.Value)
                    {
                        DDLINH.SelectedValue = theDS.Tables[7].Rows[0]["INHAdherence"].ToString();
                        if (DDLINH.SelectedItem.Text == "Fair" || DDLINH.SelectedItem.Text == "Poor")
                        {
                            string script = "";
                            script = "<script language = 'javascript' id = 'IL'>\n";
                            script += "show('divINH');\n";
                            script += "</script>\n";
                            RegisterStartupScript("IL", script);
                        }
                    }
                }

                if (theDS.Tables[8].Rows.Count > 0)
                {
                    if (theDS.Tables[8].Rows[0]["TherapyPlan"] != System.DBNull.Value)
                    {
                        ddlsubsituationInterruption.SelectedValue = theDS.Tables[8].Rows[0]["TherapyPlan"].ToString();
                        if (theDS.Tables[8].Rows[0]["TherapyReasonCode"] != System.DBNull.Value && theDS.Tables[8].Rows[0]["TherapyPlan"].ToString() == "98")
                        {
                            string script = "";
                             ddlArvTherapyChangeCode.SelectedValue = theDS.Tables[8].Rows[0]["TherapyReasonCode"].ToString();
                             if (theDS.Tables[8].Rows[0]["TherapyOther"] != System.DBNull.Value)
                             {
                                txtarvTherapyChangeCodeOtherName.Value = theDS.Tables[8].Rows[0]["TherapyOther"].ToString();
                                script = "";
                                script = "<script language = 'javascript' id = 'CCother'>\n";
                                script += "show('otherarvTherapyChangeCode');\n";
                                script += "</script>\n";
                                RegisterStartupScript("CCother", script);
                             }
                                 script = "";
                                 script = "<script language = 'javascript' id = 'CC'>\n";
                                 script += "show('arvTherapyChange');\n";
                                 script += "</script>\n";
                                 RegisterStartupScript("CC", script);
                           
                        }
                        else if (theDS.Tables[8].Rows[0]["TherapyReasonCode"] != System.DBNull.Value && theDS.Tables[8].Rows[0]["TherapyPlan"].ToString() == "99")
                        {
                            string script = "";
                            ddlArvTherapyStopCode.SelectedValue = theDS.Tables[8].Rows[0]["TherapyReasonCode"].ToString();
                            if (theDS.Tables[8].Rows[0]["TherapyOther"] != System.DBNull.Value)
                             {
                                txtarvTherapyStopCodeOtherName.Value = theDS.Tables[8].Rows[0]["TherapyOther"].ToString();
                                script = "";
                                script = "<script language = 'javascript' id = 'SCother'>\n";
                                script += "show('otherarvTherapyStopCode');\n";
                                script += "</script>\n";
                                RegisterStartupScript("SCother", script);
                             }
                            if (theDS.Tables[8].Rows[0]["ARVEndDate"] != System.DBNull.Value)
                            {
                                txtARTEndeddate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[8].Rows[0]["ARVEndDate"]);
                            }
                            script = "";
                            script = "<script language = 'javascript' id = 'SC'>\n";
                            script += "show('arvTherapyStop');\n";
                            script += "</script>\n";
                            RegisterStartupScript("SC", script);
                        }
                     }
                }
                if (theDS.Tables[9].Rows.Count > 0)
                {
                    FillCheckBoxListData(theDS.Tables[9], PnlFamilyPlanningMethod, "FamilyPlanningMethods-Nigeria", "Other_Notes");
                    FillCheckBoxListData(theDS.Tables[9], PnlOIsOtherProblems, "OtherOIs/OtherProblems-Nigeria", "Other_Notes");
                    FillCheckBoxListData(theDS.Tables[9], PnlNotedSideEffects, "NotedSideEffects-Nigeria", "Other_Notes");
                    FillCheckBoxListData(theDS.Tables[9], PnlARVwhypoorfair, "ARVWhy-Nigeria", "Other_Notes");
                    FillCheckBoxListData(theDS.Tables[9], PnlCotrimoxazolewhypoorfair, "CotrimWhy-Nigeria", "Other_Notes");
                    FillCheckBoxListData(theDS.Tables[9], PnlINHWhyPoorFair, "INHWhy-Nigeria", "Other_Notes");
                    FillCheckBoxListData(theDS.Tables[9], PnlReferredTo, "ReferredTo-Nigeria", "Other_Notes");
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
                ACVManager = null;
            }
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
            DateTime temp;
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
            else
            {
                if (!DateTime.TryParseExact(txtVisitDate.Value, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Visit Date";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "</br>";
                    txtVisitDate.Focus();
                    validationCheck = false;
                }
                else if (theCurrentDate.Date < Convert.ToDateTime(iQCareUtils.MakeDate(txtVisitDate.Value)))
                {
                    if (!dateconstraint)
                    {
                        validateMessage += "-" + IQCareMsgBox.GetMessage("CompareDate5", this) + "</br>";
                        txtVisitDate.Focus();
                        validationCheck = false;
                    }
                }
            }
            #endregion
            #region Check EDD
            if (Session["PatientSex"].ToString() == "Female" && Convert.ToDecimal(Session["PatientAge"]) > 12)
            {
                if (txtEDD.Value.Trim() != "")
                    if (!DateTime.TryParseExact(txtEDD.Value, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                    {
                        MsgBuilder msgBuilder = new MsgBuilder();
                        msgBuilder.DataElements["Control"] = " -EDD";
                        validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "</br>";
                        txtEDD.Focus();
                        validationCheck = false;
                    }
                    else if (theCurrentDate.Date >= Convert.ToDateTime(iQCareUtils.MakeDate(txtEDD.Value)))
                    {
                        if (!dateconstraint)
                        {
                            validateMessage += "-" + IQCareMsgBox.GetMessage("EDDDate", this) + "</br>";
                            txtEDD.Focus();
                            validationCheck = false;
                        }
                    }
            }
            #endregion
            #region Subsituations/Interruption
            if (ddlsubsituationInterruption.SelectedValue.ToString() == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Subsitutions/Interruption";
                validateMessage += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this) + "</br>";
                ddlsubsituationInterruption.Focus();
                validationCheck = false;

            }

            if (ddlsubsituationInterruption.SelectedValue.ToString() == "99" && ddlArvTherapyStopCode.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Stop Regimen Reason";
                validateMessage += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this) + "</br>";
                ddlArvTherapyStopCode.Focus();
                validationCheck = false;

            }

            if (ddlsubsituationInterruption.SelectedValue.ToString() == "99" && txtARTEndeddate.Value == "")
            {
                if (!dateconstraint)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Date ART Ended";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "</br>";
                    txtARTEndeddate.Focus();
                    validationCheck = false;
                }
            }
            if (ddlsubsituationInterruption.SelectedValue.ToString() == "99" && (theCurrentDate.Date < Convert.ToDateTime(iQCareUtils.MakeDate(txtARTEndeddate.Value))))
            {
                if (!dateconstraint)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    validateMessage += "-" + IQCareMsgBox.GetMessage("ARTEndDATE", this) + "</br>";
                    txtARTEndeddate.Focus();
                    validationCheck = false;
                }
            }
            if (ddlsubsituationInterruption.SelectedValue.ToString() == "99" && ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other") && txtarvTherapyStopCodeOtherName.Value == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Stop Regimen Reason Other(Specify)";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "</br>";
                ddlArvTherapyStopCode.Focus();
                validationCheck = false;

            }
            if (ddlsubsituationInterruption.SelectedValue.ToString() == "98" && ddlArvTherapyChangeCode.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Change Regimen Reason";
                validateMessage += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this) + "</br>";
                ddlArvTherapyChangeCode.Focus();
                validationCheck = false;
            }
            if (ddlsubsituationInterruption.SelectedValue.ToString() == "98" && ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
            {
                if (txtarvTherapyChangeCodeOtherName.Value == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Change Regimen Reason (Other)Specify";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "</br>";
                    ddlArvTherapyChangeCode.Focus();
                    validationCheck = false;
                }
            }

            if (UserControlKNH_NextAppointment.rdoTCANo.Checked == false &&  UserControlKNH_NextAppointment.rdoTCAYes.Checked == false)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-To return again?";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "</br>";
                ddlArvTherapyChangeCode.Focus();
                validationCheck = false;
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
        private Boolean DataQualityCheck()
        {
            IIQCareSystem IQCareSystemInterface = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSystemInterface.SystemDate();
            IQCareUtils iQCareUtils = new IQCareUtils();
            DateTime temp;
            string validateMessage = "Following values are required to complete the data quality check:</br>";
            bool qualityCheck = true;
            #region Check Visit Date
            if (Session["RegDate"] != null && txtVisitDate.Value != "")
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

            if (txtVisitDate.Value.Trim() == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Visit Date";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                txtVisitDate.Focus();
                qualityCheck = false;
                lblVisitDate.Style.Add("color", "red");
            }
            else
            {
                if (!DateTime.TryParseExact(txtVisitDate.Value, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Visit Date";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "</br>";
                    txtVisitDate.Focus();
                    qualityCheck = false;
                    lblVisitDate.Style.Add("color", "red");
                }
                else if (theCurrentDate < Convert.ToDateTime(iQCareUtils.MakeDate(txtVisitDate.Value)))
                {
                    validateMessage += "-" + IQCareMsgBox.GetMessage("CompareDate5", this) + "</br>";
                    txtVisitDate.Focus();
                    qualityCheck = false;
                    lblVisitDate.Style.Add("color", "red");
                }
            }
            #endregion

            #region Weight
            if (txtPhysWeight.Text.Trim() == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Weight";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                txtPhysWeight.Focus();
                qualityCheck = false;
                lblWeight.Style.Add("color", "red");
            }
            #endregion

            #region Height
            if (txtPhysHeight.Text.Trim() == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Height";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                txtPhysHeight.Focus();
                qualityCheck = false;
                lblHeight.Style.Add("color", "red");
            }
            #endregion

            #region "Family Planning Status"
            if (ddlFamilyPanningStatus.SelectedItem.Text == "Select")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Family Planning Status";
                validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "</br>";
                ddlFamilyPanningStatus.Focus();
                qualityCheck = false;
                lblFP.Style.Add("color", "red");
            }
            #endregion
            #region Family Planning Status
            if (ddlFunctionalStatus.SelectedItem.Text == "Select")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Functional Status";
                validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "</br>";
                ddlFunctionalStatus.Focus();
                qualityCheck = false;
                lblFS.Style.Add("color", "red");
            }
            #endregion

            #region WHOStage
            if (DDLWHOStage.SelectedItem.Text == "Select")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -WHO Stage";
                validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "</br>";
                DDLWHOStage.Focus();
                qualityCheck = false;
                lblWHOStage.Style.Add("color", "red");
            }
            #endregion
            #region TBStatus
            if (ddlTBStatus.SelectedItem.Text == "Select")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -TB Status";
                validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "</br>";
                ddlTBStatus.Focus();
                qualityCheck = false;
                lblTBStatus.Style.Add("color", "red");
            }
            #endregion

            #region ARVDrugsAdhere
            if (ddlarvdrugadhere.SelectedItem.Text == "Select")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -ARV Drugs Adherence";
                validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "</br>";
                ddlarvdrugadhere.Focus();
                qualityCheck = false;
                lblARVDrugs.Style.Add("color", "red");
            }
            #endregion
            if (!qualityCheck)
            {
                MsgBuilder totalMsgBuilder = new MsgBuilder();
                totalMsgBuilder.DataElements["MessageText"] = validateMessage;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
            }
            return qualityCheck;
        }
        private void AddAttributes()
        {

            txtVisitDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            txtVisitDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtPhysHeight.Attributes.Add("onkeyup", "chkInteger('" + txtPhysHeight.ClientID + "')");
            txtPhysWeight.Attributes.Add("onkeyup", "chkDecimal('" + txtPhysWeight.ClientID + "')");

            txtARTStart.Attributes.Add("Onkeyup", "chkInteger('" + txtARTStart.ClientID + "')");
            txtStartingCurrentRegimen.Attributes.Add("Onkeyup", "chkInteger('" + txtStartingCurrentRegimen.ClientID + "')");

            txtBPSystolic.Attributes.Add("Onkeyup", "chkInteger('" + txtBPSystolic.ClientID + "')");
            txtBPDiastolic.Attributes.Add("Onkeyup", "chkInteger('" + txtBPDiastolic.ClientID + "')");

            txtEDD.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
            txtEDD.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtNumOfDaysHospitalized.Attributes.Add("Onkeyup", "chkInteger('" + txtNumOfDaysHospitalized.ClientID + "')");
        }
        private void BindUserDropdown(DropDownList DropDownID, String userId)
        {
            Dictionary<int, string> userList = new Dictionary<int, string>();
            CustomFieldClinical.BindUserDropDown(DropDownID, out userList);
            if (!string.IsNullOrEmpty(userId))
            {
                if (userList.ContainsKey(Convert.ToInt32(userId)))
                {
                    DropDownID.SelectedValue = userId;
                }
            }
        }
        private Hashtable NigeriaArtCareVisitationParameters()
        {
            htNigeriaArtCareParameters = new Hashtable();
            htNigeriaArtCareParameters.Add("ACVVisitDate", txtVisitDate.Value);
            htNigeriaArtCareParameters.Add("ACVchkschedule", chkifschedule.Checked==true ? 1 : 0);
            htNigeriaArtCareParameters.Add("ACVARTStart", txtARTStart.Text);
            htNigeriaArtCareParameters.Add("ACVCurrentRegimen", txtStartingCurrentRegimen.Text);
            htNigeriaArtCareParameters.Add("ACVPhysWeight", txtPhysWeight.Text !=  "" ? txtPhysWeight.Text : "999");
            htNigeriaArtCareParameters.Add("ACTPhysHeight", txtPhysHeight.Text !=  "" ? txtPhysHeight.Text : "999");
            htNigeriaArtCareParameters.Add("ACVBPSystolic", txtBPSystolic.Text);
            htNigeriaArtCareParameters.Add("ACVBPDiastolic", txtBPDiastolic.Text);
            htNigeriaArtCareParameters.Add("ACVPregnant", PregnantYes.Checked == true ? 1 : PregnantNo.Checked == true ? 0 : 2);
            htNigeriaArtCareParameters.Add("ACVEDDDate", txtEDD.Value);
            htNigeriaArtCareParameters.Add("ACVChkonPMTCT", ChkonPMTCT.Checked==true ? 1 : 0);
            htNigeriaArtCareParameters.Add("ACVFamilyPlanningStatus", ddlFamilyPanningStatus.SelectedValue);
            htNigeriaArtCareParameters.Add("ACVFunctionalStatus", ddlFunctionalStatus.SelectedValue);
            htNigeriaArtCareParameters.Add("ACVWHOStage", DDLWHOStage.SelectedValue);
            htNigeriaArtCareParameters.Add("ACVTBStatus", ddlTBStatus.SelectedValue);
            htNigeriaArtCareParameters.Add("ACVTBCardNo", txtTBCardNo.Value);
            htNigeriaArtCareParameters.Add("ACVARVDrugAdherence", ddlarvdrugadhere.SelectedValue);
            htNigeriaArtCareParameters.Add("ACVCotrimoxazoleAdherence", ddlCotrimoxazoleAdhere.SelectedValue);
            htNigeriaArtCareParameters.Add("ACVINH", DDLINH.SelectedValue);
            htNigeriaArtCareParameters.Add("ACVddlsubsitution", ddlsubsituationInterruption.SelectedValue);
            if (ddlsubsituationInterruption.SelectedValue == "98")
            { htNigeriaArtCareParameters.Add("ACVTherapyChangeStopCode", ddlArvTherapyChangeCode.SelectedValue);
              htNigeriaArtCareParameters.Add("ACVTherapyChangeStopCodeOtheName", txtarvTherapyChangeCodeOtherName.Value);
              htNigeriaArtCareParameters.Add("ACVARTEnddate", "");
            }
            else if (ddlsubsituationInterruption.SelectedValue == "99")
            {
                htNigeriaArtCareParameters.Add("ACVTherapyChangeStopCode", ddlArvTherapyStopCode.SelectedValue);
                htNigeriaArtCareParameters.Add("ACVTherapyChangeStopCodeOtheName", txtarvTherapyStopCodeOtherName.Value);
                htNigeriaArtCareParameters.Add("ACVARTEnddate", txtARTEndeddate.Value);
            }
            else
            {
                htNigeriaArtCareParameters.Add("ACVTherapyChangeStopCode", "");
                htNigeriaArtCareParameters.Add("ACVTherapyChangeStopCodeOtheName", "");
                htNigeriaArtCareParameters.Add("ACVARTEnddate", "");
            }
            htNigeriaArtCareParameters.Add("ACVNumofDaysHospitalised", txtNumOfDaysHospitalized.Text);
            htNigeriaArtCareParameters.Add("TCA", this.UserControlKNH_NextAppointment.rdoTCAYes.Checked ? "1" : this.UserControlKNH_NextAppointment.rdoTCANo.Checked ? "0" : "");
            htNigeriaArtCareParameters.Add("Signature", DDSignature.SelectedValue);
            return htNigeriaArtCareParameters;
        }
        private DataTable GetCheckBoxListcheckedIDs(Panel thePnl, string FieldName, string thetxtFieldName, int Flag)
        {
            if (Flag == 0)
            {
                DTCheckedIds = new DataTable();
                if (DTCheckedIds.Columns.Contains(FieldName) == false && DTCheckedIds.Columns.Contains(FieldName) == false)
                {
                    DataColumn dataColumnPotentialSideEffect = new DataColumn(FieldName);
                    dataColumnPotentialSideEffect.DataType = System.Type.GetType("System.Int32");
                    DTCheckedIds.Columns.Add(dataColumnPotentialSideEffect);
                    if (thetxtFieldName != "")
                    {
                        DataColumn thepotentialSideEffect_Other = new DataColumn(thetxtFieldName);
                        thepotentialSideEffect_Other.DataType = System.Type.GetType("System.String");
                        DTCheckedIds.Columns.Add(thepotentialSideEffect_Other);
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
                            //if (theControlId[1].ToString().Contains("Other") == true)
                            if ( ((CheckBox)y).Text.Contains("Other") == true)
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
        private void FillCheckBoxListData(DataTable theDT, Panel thePnl, string FieldName, string theFieldName)
        {
            try
            {
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
                                if (y.ID.Contains("Pnl"))
                                {
                                    FillCheckBoxListData(theDT, (System.Web.UI.WebControls.Panel)y, FieldName, theFieldName);
                                }
                            }
                        }

                        else
                        {
                            
                            if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                            {
                                
                                if (((CheckBox)y).ID.Contains(thePnl.ID + "-" + DR["ValueID"].ToString()) && FieldName == DR["FieldName"].ToString())
                                    ((CheckBox)y).Checked = true;

                                else if ("other"+((CheckBox)y).ID == thePnl.ID +"-"+ DR["ValueID"].ToString() && FieldName == DR["FieldName"].ToString())
                                    ((CheckBox)y).Checked = true;
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
        private void SaveCancel()
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            IQCareMsgBox.NotifyAction("ART Care Visitation Form saved successfully. Do you want to close?", "ART Care Visitation Form", false, this, "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';");
        }
        private void SaveARTCareVisitation(int DataQuality)
        {
           
            DataSet theDSforChklist = new DataSet();
            //Family Planning
            DataTable DTFamilyPlanning = new DataTable();
            DTFamilyPlanning = GetCheckBoxListcheckedIDs(PnlFamilyPlanningMethod, "FamilyPlanningId", "FamilyPlanning_Other", 0);
            theDSforChklist.Tables.Add(DTFamilyPlanning);

            //OIs/Other Problem
            DataTable DTOIsOtherProblems = new DataTable();
            DTOIsOtherProblems = GetCheckBoxListcheckedIDs(PnlOIsOtherProblems, "OIOtherProblemID", "OIProblem_Other", 0);
            theDSforChklist.Tables.Add(DTOIsOtherProblems);

            //Noted Side Effects
            DataTable DTNotedSideEffects = new DataTable();
            DTNotedSideEffects = GetCheckBoxListcheckedIDs(PnlNotedSideEffects, "NotedSideEffectId", "NotedSide_Other", 0);
            theDSforChklist.Tables.Add(DTNotedSideEffects);

            //ARVWhyPoorFair
            DataTable DTARVWhyPoorFair = new DataTable();
            DTARVWhyPoorFair = GetCheckBoxListcheckedIDs(PnlARVwhypoorfair, "ARVwhypoorfairId", "ARVwhypoorfair_Other", 0);
            theDSforChklist.Tables.Add(DTARVWhyPoorFair);

            //Cotrimoxazole
            DataTable DTCotrimoxazoleWhyPoorFair = new DataTable();
            DTCotrimoxazoleWhyPoorFair = GetCheckBoxListcheckedIDs(PnlCotrimoxazolewhypoorfair, "CotrimoxazolewhypoorfairId", "Cotrimoxazolewhypoorfair_Other", 0);
            theDSforChklist.Tables.Add(DTCotrimoxazoleWhyPoorFair);

            //INH
            DataTable DTINHWhyPoorFair = new DataTable();
            DTINHWhyPoorFair = GetCheckBoxListcheckedIDs(PnlINHWhyPoorFair, "INHwhypoorfairId", "INHwhypoorfair_Other", 0);
            theDSforChklist.Tables.Add(DTINHWhyPoorFair);

            //Referred to
            DataTable DTReferredto = new DataTable();
            DTReferredto = GetCheckBoxListcheckedIDs(PnlReferredTo, "ReferredtoId", "Referredto_Other", 0);
            theDSforChklist.Tables.Add(DTReferredto);


            INigeriaARTCareVisitation ACVManager;
            ACVManager = (INigeriaARTCareVisitation)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNigeriaARTCareVisitation, BusinessProcess.Clinical");
            LocationID = Convert.ToInt32(Session["AppLocationId"]);
            PatientID = Convert.ToInt32(Session["PatientId"]);
            visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            Hashtable htparam = NigeriaArtCareVisitationParameters();
            visitPK = ACVManager.Save_Update_ARTCareVisitation(PatientID, visitPK, LocationID, htparam, theDSforChklist, Convert.ToInt32(Session["AppUserId"]), DataQuality);
            Session["PatientVisitId"] = visitPK;


        }
        private void DeleteForm()
        {

            INigeriaARTCareVisitation ARTCareVisitation;
            int theResultRow, OrderNo;
            string FormName;
            OrderNo = Convert.ToInt32(Session["PatientVisitId"].ToString());
            FormName = "ART Care Visitation";
            ARTCareVisitation = (INigeriaARTCareVisitation)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNigeriaARTCareVisitation, BusinessProcess.Clinical");
            theResultRow = (int)ARTCareVisitation.DeleteARTCareVisitationForm(FormName, OrderNo, Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["AppUserId"].ToString()));
            if (theResultRow == 0)
            {
                IQCareMsgBox.Show("RemoveFormError", this);
                return;
            }
            else
            {
                string theUrl;
                theUrl = string.Format("{0}", "frmPatient_Home.aspx?Func=Delete");
                Response.Redirect(theUrl);
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["name"] == "Delete")
                {
                    DeleteForm();
                }
                if (fieldValidation() == false)
                { return; }
                SaveARTCareVisitation(0);
                SaveCancel();
            }
            finally { }
        }
        protected void btnDataQualityCheck_Click(object sender, EventArgs e)
        {
            if (DataQualityCheck() == false)
            { return; }
            SaveARTCareVisitation(1);
            SaveCancel();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
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
        protected void btnLabratory_Click(object sender, EventArgs e)
        {
            try
            {
                if (fieldValidation() == false)
                { return; }
                SaveARTCareVisitation(0);
                string script = "<script language = 'javascript' defer ='defer' id = 'Labratory'>\n";
                script += "fnPageOpen('Labratory');\n";
                script += "</script>\n";
                RegisterStartupScript("Labratory", script);
            }
            finally { }
        }
        protected void btnpharmacy_Click(object sender, EventArgs e)
        {
            try
            {
                if (fieldValidation() == false)
                { return; }
                SaveARTCareVisitation(0);
                string script = "<script language = 'javascript' defer ='defer' id = 'Pharmacy'>\n";
                script += "fnPageOpen('Pharmacy');\n";
                script += "</script>\n";
                RegisterStartupScript("pharmacy", script);
            }
            finally { }
        }
    
    }
}