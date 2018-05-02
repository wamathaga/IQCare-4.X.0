using System;
using System.Data;
using System.Configuration;
using System.Collections;
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
using Interface.Administration;
using System.Text;

namespace PresentationApp.ClinicalForms
{

    public partial class frmClinical_Nigeria_ARTCareSummary : BasePage
    {
        int PatientID, LocationID, visitPK=0;
        private Hashtable ARTCareSummaryParameters()
        {
            Hashtable htARTCareSummaryParameters = new Hashtable();
            htARTCareSummaryParameters.Add("visitdate", System.DateTime.Now.ToString("dd-MMM-yyyy"));
            htARTCareSummaryParameters.Add("CohortMonth", txtcohortmnth.Value);
            htARTCareSummaryParameters.Add("CohortYear", txtcohortyear.Value);
            htARTCareSummaryParameters.Add("OtherfacilityRegimen", txtotherregimen.Value);
            htARTCareSummaryParameters.Add("OtherfacilityRegimenStartDate", txtotherRegimendate.Value);
            htARTCareSummaryParameters.Add("OtherfacilityWHOStage", ddlotherFacilityClinicalStage.SelectedValue);
            htARTCareSummaryParameters.Add("OtherfacilityCD4", txtotherCD4.Value != "" ? txtotherCD4.Value : "0.00");
            htARTCareSummaryParameters.Add("OtherfacilityCD4Percent", txtotherCD4Percent.Value != "" ? txtotherCD4Percent.Value : "0.00");
            htARTCareSummaryParameters.Add("OtherfacilityrWeight", txtotherwght.Value !=  "" ? txtotherwght.Value : "0.00");
            htARTCareSummaryParameters.Add("OtherfacilityHeight", txtotherheight.Value != "" ? txtotherheight.Value : "0.00");
            htARTCareSummaryParameters.Add("OtherfacilityClinicalStage", ddlotherFunction.SelectedValue);
            return htARTCareSummaryParameters;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "ART Care Summary Form";
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "ART Care Summary Form";
            if (!IsPostBack)
            {
                BindList();
                GetARTCareSummary();
                BusinessRule();
                Authenticate();
            }
            if ((DataTable)Application["AddRegimen"] != null)
            {
                ViewState["ARVMasterData"] = (DataTable)Application["MasterData"];
                Application.Remove("MasterData");
                DataTable theDT = (DataTable)Application["AddRegimen"];
                ViewState["TransSelectedData"] = theDT;
                string theStr = FillRegimen(theDT);
                txtotherregimen.Value = theStr;
                Application.Remove("AddRegimen");
            }
        }
        private void BusinessRule()
        {
            if (Convert.ToDecimal(Session["PatientAge"]) > 14)
            {
                
                lblotherHeight.Visible = false;
                lblotherCD4Percent.Visible = false;
                txtotherheight.Visible = false;
                txtotherCD4Percent.Visible = false;
                lblthisheight.Visible = false;
                lblthisCD4Percent.Visible = false;
                txtthisheight.Visible = false;
                txtthisCDPercent.Visible = false;
            }
            if (Convert.ToDecimal(Session["PatientAge"]) < 14)
            {
                lblotherCD4.Visible = false;
                txtotherCD4.Visible = false;
                lblthisCD4.Visible = false;
                txtthisCD4.Visible = false;
            }
        }
        private void BindList()
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            if (theDS.Tables["Mst_Decode"] != null)
            {
                DataView theDVWHOStage = new DataView(theDS.Tables["Mst_Decode"]);
                theDVWHOStage.RowFilter = "CodeId=22 and (DeleteFlag = 0 or DeleteFlag IS NULL) and SystemId in(0,1)";
                theDVWHOStage.Sort = "SRNo";
                if (theDVWHOStage.Table != null)
                {
                    DataTable theDTWHOStage = (DataTable)theUtils.CreateTableFromDataView(theDVWHOStage);
                    BindManager.BindCombo(ddlotherFacilityClinicalStage, theDTWHOStage, "Name", "Id");
                    BindManager.BindCombo(ddlthisfacilityClinicalStage, theDTWHOStage, "Name", "Id");
                }
                DataView theDVFunctionalStage = new DataView(theDS.Tables["Mst_Decode"]);
                theDVFunctionalStage.RowFilter = "CodeId=21 and (DeleteFlag=0 or DeleteFlag IS NULL) and SystemId in (0,1)";
                theDVFunctionalStage.Sort = "SRNo";
                if (theDVFunctionalStage.Table != null)
                {
                    DataTable theDTFunctionalStage = (DataTable)theUtils.CreateTableFromDataView(theDVFunctionalStage);
                    BindManager.BindCombo(ddlotherFunction, theDTFunctionalStage, "Name", "Id");
                    BindManager.BindCombo(ddlthisFunction, theDTFunctionalStage, "Name", "Id");

                }
            }
        }
        private string FillRegimen(DataTable theDT)
        {
            string theRegimen = "";
            DataView theDV = new DataView();
            if (theDT.Rows.Count != 0)
            {
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    DataTable theFilDT = (DataTable)ViewState["MasterARVData"];
                    if (theFilDT.Rows.Count > 0)
                    {
                        theDV = new DataView(theFilDT);
                        theDV.RowFilter = "DrugId = " + theDT.Rows[i]["DrugId"]; // DrugTypeID = 37";
                        if (theDV.Count > 0)
                        {
                            for (int j = 0; j < theDV.Count; j++)
                            {
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[j]["Abbr"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[j]["Abbr"].ToString();
                                }
                            }
                        }
                    }
                    theRegimen = theRegimen.Trim();
                }
            }
            return theRegimen;
        }
        private void GetARTCareSummary()
        {
            txtotherRegimendate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtotherCD4.Attributes.Add("onKeyup", "chkNumeric('" + txtotherCD4.ClientID + "')");
            txtotherwght.Attributes.Add("onKeyup", "chkNumeric('" + txtotherwght.ClientID + "')");
            txtotherheight.Attributes.Add("onKeyup", "chkNumeric('" + txtotherheight.ClientID + "')");
            txtotherCD4.Attributes.Add("onKeyup", "chkNumeric('" + txtotherCD4.ClientID + "')");
            txtthisRegimendate.Attributes.Add("onKeyup", "chkNumeric('" + txtthisRegimendate.ClientID + "')");
            txtthiswght.Attributes.Add("onKeyup", "chkNumeric('" + txtthiswght.ClientID + "')");
            txtthisheight.Attributes.Add("onKeyup", "chkNumeric('" + txtthisheight.ClientID + "')");
            txtthisCD4.Attributes.Add("onKeyup", "chkNumeric('" + txtthisCD4.ClientID + "')");
            INigeriaARTCareSummary INARTManager;
            try
            {
                INARTManager = (INigeriaARTCareSummary)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNigeriaARTCareSummary, BusinessProcess.Clinical");
                DataSet theDS = INARTManager.GetPatientARTCareSummary(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppLocationId"]));
                Session["ARVMasterData"] = theDS.Tables[1];
                ViewState["MasterData"] = theDS.Tables[1];
                ViewState["MasterARVData"] = theDS.Tables[1];

                if (theDS.Tables[0].Rows.Count > 0)
                {
                    if (theDS.Tables[0].Rows[0]["ARTStartDate"] != System.DBNull.Value)
                    {
                        this.txtthisRegimendate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["ARTStartDate"]);
                        this.txtcohortmnth.Value = String.Format("{0:MMM}", theDS.Tables[0].Rows[0]["ARTStartDate"]).ToUpper();
                        this.txtcohortyear.Value = String.Format("{0:yyyy}", theDS.Tables[0].Rows[0]["ARTStartDate"]).ToUpper();
                    }
                    if (theDS.Tables[0].Rows[0]["VisitId"] != System.DBNull.Value)
                    {
                        Session["PatientVisitId"] = theDS.Tables[0].Rows[0]["VisitId"].ToString();
                    }
                }
                //ART at another facility
                if (theDS.Tables[6].Rows.Count > 0)
                {
                    if (theDS.Tables[6].Rows[0]["Firstlinereg"] != System.DBNull.Value)
                    {
                        txtotherregimen.Value = theDS.Tables[6].Rows[0]["Firstlinereg"].ToString();
                    }
                    if (theDS.Tables[6].Rows[0]["FirstLineRegStDate"] != System.DBNull.Value)
                    {
                        txtotherRegimendate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[6].Rows[0]["FirstLineRegStDate"]);
                    }
                    if (theDS.Tables[6].Rows[0]["whostage"] != System.DBNull.Value)
                    {
                        ddlotherFacilityClinicalStage.SelectedValue = theDS.Tables[6].Rows[0]["whostage"].ToString();
                    }
                    if (theDS.Tables[6].Rows[0]["weight"] != System.DBNull.Value)
                    {
                        txtotherwght.Value = theDS.Tables[6].Rows[0]["weight"].ToString();
                    }
                    if (theDS.Tables[6].Rows[0]["Height"] != System.DBNull.Value)
                    {
                        txtotherheight.Value = theDS.Tables[6].Rows[0]["Height"].ToString();
                    }
                    if (theDS.Tables[6].Rows[0]["FunctionalStatus"] != System.DBNull.Value)
                    {
                        ddlotherFunction.SelectedValue = theDS.Tables[6].Rows[0]["FunctionalStatus"].ToString();
                    }
                   
                    if (theDS.Tables[6].Rows[0]["cd4"] != System.DBNull.Value)
                    {
                        txtotherCD4.Value = theDS.Tables[6].Rows[0]["cd4"].ToString();
                    }
                    if (theDS.Tables[6].Rows[0]["cd4percent"] != System.DBNull.Value)
                    {
                        txtotherCD4Percent.Value = theDS.Tables[6].Rows[0]["cd4percent"].ToString();
                    }
                }
                //Height
                if (theDS.Tables[2].Rows.Count > 0)
                {
                    if (theDS.Tables[2].Rows[0]["Height"] != System.DBNull.Value)
                    {
                       txtthisheight.Value = theDS.Tables[2].Rows[0]["Height"].ToString();
                    }
                }
                //Weight
                if (theDS.Tables[3].Rows.Count > 0)
                {
                    if (theDS.Tables[3].Rows[0]["Weight"] != System.DBNull.Value)
                    {
                        txtthiswght.Value = theDS.Tables[3].Rows[0]["Weight"].ToString();
                    }
                }

                //CD4
                if (theDS.Tables[4].Rows.Count > 0)
                {
                    if (theDS.Tables[4].Rows[0]["CD4"] != System.DBNull.Value)
                    {
                        txtthisCD4.Value = theDS.Tables[4].Rows[0]["CD4"].ToString();
                    }
                }
                //CD4
                if (theDS.Tables[5].Rows.Count > 0)
                {
                    if (theDS.Tables[5].Rows[0]["CD4Percent"] != System.DBNull.Value)
                    {
                        txtthisCD4.Value = theDS.Tables[5].Rows[0]["CD4Percent"].ToString();
                    }
                }


                //Regimen at this facility
                if (theDS.Tables[7].Rows.Count > 0)
                {
                    if (theDS.Tables[7].Rows[0]["FirstLineRegimen"] != System.DBNull.Value)
                    {
                        txtthisregimen.Value = theDS.Tables[7].Rows[0]["FirstLineRegimen"].ToString();
                    }
                }
                //WHO Stage and WAB Stage
                if (theDS.Tables[8].Rows.Count > 0)
                {
                    if (theDS.Tables[8].Rows[0]["WHOStage"] != System.DBNull.Value)
                    {
                        ddlthisfacilityClinicalStage.SelectedValue = theDS.Tables[8].Rows[0]["WHOStage"].ToString();
                    }
                    //if (theDS.Tables[8].Rows[0]["WABStage"] != System.DBNull.Value)
                    //{
                    //    ddlthisFunction.SelectedValue = theDS.Tables[8].Rows[0]["WABStage"].ToString();
                    //}
                }
                if (theDS.Tables[12].Rows.Count > 0)
                {
                    if (theDS.Tables[12].Rows[0]["FunctionalStatus"] != System.DBNull.Value)
                    {
                        ddlthisFunction.SelectedValue = theDS.Tables[12].Rows[0]["FunctionalStatus"].ToString();
                    }
                }

                //Grid Substitution of ARVs
                grdSubsARVs.DataSource = theDS.Tables[9];
                grdSubsARVs.DataBind();

                //Grid ART Treatment Interruption
                grdInteruptions.DataSource = theDS.Tables[10];
                grdInteruptions.DataBind();

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
                INARTManager = null;
            }
        }
        private void SaveCancel()
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            IQCareMsgBox.NotifyAction("ART Care Summary Form saved successfully. Do you want to close?", "ART Care Summary", false, this, "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';");
        }
        private Boolean Validate_Data()
        {
            if (txtotherwght.Value != "")
            {
                decimal weight = Convert.ToDecimal(txtotherwght.Value);
                if (weight < 0 || weight > 225)
                {
                    IQCareMsgBox.Show("chkWeight", this);
                    txtotherwght.Focus();
                    return false;
                }
            }
            if (txtotherheight.Value != "")
            {
                decimal height = Convert.ToDecimal(txtotherheight.Value);

                if (height < 0 || height > 250)
                {
                    IQCareMsgBox.Show("chkHeight", this);
                    txtotherheight.Focus();
                    return false;
                }

            }
            if (txtotherRegimendate.Value != "" && txtthisRegimendate.Value != "")
            {
                DateTime dtanother = Convert.ToDateTime(txtotherRegimendate.Value.Trim()).Date;
                DateTime dtthis = Convert.ToDateTime(txtthisRegimendate.Value.Trim()).Date;
                if (dtanother >= dtthis)
                {
                    IQCareMsgBox.Show("CmpARVTherapyDate", this);
                    txtthisRegimendate.Focus();
                    return false;
                }

            }
            return true;
        }
        private string DataQuality_Msg()
        {
            string strmsg = "Following values are required to complete the data quality check:</br>";
            if (txtotherregimen.Value == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Regimen";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                txtotherregimen.Focus();
                lblotherregimen.Style.Add("color", "red");
            }
            if (txtotherRegimendate.Value == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Regimen Date";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                txtotherRegimendate.Focus();
                lblrARTdate.Style.Add("color", "red");
            }

            if (txtotherwght.Value == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Weight";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                txtotherwght.Focus();
                lblotherweight.Style.Add("color", "red");
            }
            return strmsg;
        }
        private void Authenticate()
        {
            if (Request.QueryString["name"] == "Delete")
            {
                btn_save.Text = "Delete";
                DQ_Check.Visible = false;
            }
            /***************** Check For User Rights ****************/
            AuthenticationManager MgrAuthenticate = new AuthenticationManager();
            if (MgrAuthenticate.HasFunctionRight(ApplicationAccess.NigeriaARTCareSummary, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btn_print.Enabled = false;
            }
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                if (MgrAuthenticate.HasFunctionRight(ApplicationAccess.NigeriaARTCareSummary, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btn_save.Enabled = false;
                    DQ_Check.Enabled = false;
                }
            }
            else
            {
                if (MgrAuthenticate.HasFunctionRight(ApplicationAccess.NigeriaARTCareSummary, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    string theUrl = "";
                    theUrl = string.Format("{0}?sts={1}", "frmPatient_History.aspx", Session["HIVPatientStatus"].ToString());
                    Response.Redirect(theUrl);
                }
                else if (MgrAuthenticate.HasFunctionRight(ApplicationAccess.NigeriaARTCareSummary, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btn_save.Enabled = false;
                    DQ_Check.Enabled = false;
                }
                if (Convert.ToString(Session["HIVPatientStatus"]) == "1")
                {
                    btn_save.Enabled = false;
                    DQ_Check.Enabled = false;
                }
                //Privilages for Care End
                if (Convert.ToString(Session["CareEndFlag"]) == "1" && Convert.ToString(Session["CareendedStatus"]) == "1")
                {
                    btn_save.Enabled = true;
                    DQ_Check.Enabled = true;
                }
            }
        }
        private void DeleteForm()
        {

            INigeriaARTCareSummary ARTCareSummary;
            int theResultRow, OrderNo;
            string FormName;
            OrderNo = Convert.ToInt32(Session["PatientVisitId"].ToString());
            FormName = "ART Care Summary";
            ARTCareSummary = (INigeriaARTCareSummary)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNigeriaARTCareSummary, BusinessProcess.Clinical");
            theResultRow = (int)ARTCareSummary.DeleteARTCareSummaryForm(FormName, OrderNo, Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["AppUserId"].ToString()));
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
        protected void btnTransRegimen_Click(object sender, EventArgs e)
        {
            string theScript;
            Application.Add("MasterData", ViewState["MasterData"]);
            Application.Add("SelectedDrug", (DataTable)ViewState["TransSelectedData"]);
            ViewState.Remove("ARVMasterData");
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=btnRegimen' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.RegisterStartupScript("DrgPopup", theScript);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["name"] == "Delete")
                {
                    DeleteForm();
                }
                if (Validate_Data() == false)
                {
                    return;
                }
                int visitPK = 0;
                LocationID = Convert.ToInt32(Session["AppLocationId"]);
                PatientID = Convert.ToInt32(Session["PatientId"]);
                if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
                {
                    visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                }
                INigeriaARTCareSummary INARTManager = (INigeriaARTCareSummary)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNigeriaARTCareSummary, BusinessProcess.Clinical");
                Hashtable htparam = ARTCareSummaryParameters();
                INARTManager.SaveUpdatePatientARTCareSummary(PatientID, visitPK, LocationID, htparam, Convert.ToInt32(Session["AppUserId"]), 0);
                SaveCancel();
            }
            finally { }
        }
        protected void DQ_Check_Click(object sender, EventArgs e)
        {
            if (Validate_Data() == false)
            {
                return;
            }
            string msg = DataQuality_Msg();
            if (msg.Length > 70)
            {
                MsgBuilder theBuilder1 = new MsgBuilder();
                theBuilder1.DataElements["MessageText"] = msg;
                IQCareMsgBox.Show("#C1", theBuilder1, this);
                return;
            }

            LocationID = Convert.ToInt32(Session["AppLocationId"]);
            PatientID = Convert.ToInt32(Session["PatientId"]);
            if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
            {
                visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            }
            INigeriaARTCareSummary INARTManager = (INigeriaARTCareSummary)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNigeriaARTCareSummary, BusinessProcess.Clinical");
            Hashtable htparam = ARTCareSummaryParameters();
            INARTManager.SaveUpdatePatientARTCareSummary(PatientID, visitPK, LocationID, htparam, Convert.ToInt32(Session["AppUserId"]), 1);
            SaveCancel();
        }

        protected void btn_close_Click(object sender, EventArgs e)
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
    }
}