using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.VisualBasic;
using Interface.Clinical;
using Interface.Security;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Service;

public partial class frmPatientCustomRegistration : LogPage
{
    DataSet theDSXML = new DataSet();
    string ObjFactoryParameter = "BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical";
    string ObjFactoryParameterBCustom = "BusinessProcess.Clinical.BCustomForm, BusinessProcess.Clinical";
    int FeatureID = 126, PatientID = 0, VisitID = 0, LocationID = 0;
    Boolean theConditional;
    Hashtable htParameters;

    Boolean rdoTrueFalseStatus = true;
    protected void Page_PreRender(object sender, EventArgs e)
    {

        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        if (this.IsPostBack == true)
        {
            if (HdnCntrl.Value != "")
            {
                string[] theCntrl = HdnCntrl.Value.Split('%');
                CheckControl(PnlDynamicElements, theCntrl);
                HdnCntrl.Value = "";
            }
            foreach (Control x in PnlDynamicElements.Controls)
            {
                if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                {
                    DropDownList theDList = (DropDownList)x;
                    if (theDList.AutoPostBack == true)
                    {
                        EventArgs s = new EventArgs();
                        ddlSelectList_SelectedIndexChanged(x, s);
                    }
                }
                else if (x.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    CheckBox chklst = (CheckBox)x;
                    if (chklst.AutoPostBack == true)
                    {
                        EventArgs s = new EventArgs();
                        ddlSelectList_SelectedIndexChanged(x, s);
                    }
                }
            }
        }
        /////HTML Controls PostBack//////
        ConFieldEnableDisable(PnlDynamicElements);


        if (Convert.ToInt32(!string.IsNullOrEmpty(txtageCurrentYears.Text.ToString())) > 15)
        {

            foreach (Control control in PnlDynamicElements.Controls)
            {
                if (control is System.Web.UI.LiteralControl)
                {
                    System.Web.UI.LiteralControl divControl = (System.Web.UI.LiteralControl)control;
                    //if (divControl.Text == "<div id='Adult Patient Demographic Information'>")
                    //{
                    //    divControl.Text = "<div id='Adult Patient Demographic Information' style='display:none;'>";
                    //}
                }
            }
        }
        else
        {
            foreach (Control control in PnlDynamicElements.Controls)
            {
                if (control is System.Web.UI.LiteralControl)
                {
                    System.Web.UI.LiteralControl divControl = (System.Web.UI.LiteralControl)control;
                    //if (divControl.Text == "<div id='Paediatric Patient Demographic Information'>")
                    //{
                    //    divControl.Text = "<div id='Paediatric Patient Demographic Information' style='display:none;'>";
                    //}
                }
            }
        }



    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }

        (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
        Ajax.Utility.RegisterTypeForAjax(typeof(frmPatientCustomRegistration));
        ddProvince.SelectedIndexChanged += new EventHandler(SelectedProvinceItemTypeChanged);
        Attributes();
        theDSXML.ReadXml(MapPath(".\\XMLFiles\\AllMasters.con"));
        if (!IsPostBack)
        {
            Binddropdown();
        }
        PatientID = Convert.ToInt32(Session["PatientId"]);
        LocationID = Convert.ToInt32(Session["AppLocationId"]);
        VisitID = Convert.ToInt32(ViewState["VisitPk"]);
        LoadPredefinedLabel_Field(FeatureID);

        if (PatientID > 0)
        {
            if (!IsPostBack)
            {
                LoadPatientStaticData(PatientID);
                VisitID = Convert.ToInt32(ViewState["VisitPk"]);
                BindValue(PatientID, VisitID, LocationID, PnlDynamicElements);
            }

        }


    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string GetDuplicateRecord(string strfname, string strmname, string strlname, string address, string Phone)
    {
        IPatientRegistration PatientManager;
        StringBuilder objBilder = new StringBuilder();
        PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataSet dsPatient = new DataSet();
        dsPatient = PatientManager.GetDuplicatePatientSearchResults(strlname, strmname, strfname, address, Phone);

        if (dsPatient.Tables[0].Rows.Count > 0)
        {
            objBilder.Append("<table border='0'  width='100%'>");
            objBilder.Append("<tr style='background-color:#e1e1e1'>");
            //objBilder.Append("<td class='smallerlabel'>PatientID</td>");
            objBilder.Append("<td class='smallerlabel'>IQ Number</td>");
            objBilder.Append("<td class='smallerlabel'>F name</td>");
            objBilder.Append("<td class='smallerlabel'>L name</td>");
            objBilder.Append("<td class='smallerlabel'>Registration Date</td>");
            objBilder.Append("<td class='smallerlabel'>Dob</td>");
            objBilder.Append("<td class='smallerlabel'>Sex</td>");
            objBilder.Append("<td class='smallerlabel'>Phone</td>");
            objBilder.Append("<td class='smallerlabel'>Facility</td>");
            objBilder.Append("</tr>");
            for (int i = 0; i < dsPatient.Tables[0].Rows.Count; i++)
            {
                objBilder.Append("<tr>");
                //objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["PatientRegistrationID"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["IQNumber"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["firstname"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["lastname"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["RegistrationDate"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["dobPatient"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["sex"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["Phone"].ToString() + "</td>");
                objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["FacilityName"].ToString() + "</td>");
                objBilder.Append("</tr>");
            }
            objBilder.Append("</table>");
        }
        return objBilder.ToString();
    }
    private void ConFieldEnableDisable(Control theControl)
    {
        foreach (Control x in theControl.Controls)
        {
            if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
            {
                ConFieldEnableDisable(x);
            }
            else
            {
                if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                {
                    if (((HtmlInputRadioButton)x).Checked == true)
                    {
                        DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                        string[] theId = ((HtmlInputRadioButton)x).ID.Split('-');
                        theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                        if (theDVConditionalField.Count > 0)
                        {
                            EventArgs s = new EventArgs();
                            this.HtmlRadioButtonSelect(x);
                        }
                    }
                }
                if (x.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    if (((CheckBox)x).Checked == true)
                    {
                        DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                        string[] theId = ((CheckBox)x).ID.Split('-');
                        if (theId.Length == 5)
                        {
                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                            if (theDVConditionalField.Count > 0)
                            {
                                EventArgs s = new EventArgs();
                                this.HtmlCheckBoxSelect(x);

                            }
                        }
                    }
                }
                if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                {
                    if (Convert.ToInt32(((DropDownList)x).SelectedIndex) > 0)
                    {
                        DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                        string[] theId = ((DropDownList)x).ID.Split('-');
                        theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                        if (theDVConditionalField.Count > 0)
                        {
                            EventArgs s = new EventArgs();
                            this.ddlSelectList_SelectedIndexChanged(x, s);
                        }
                    }
                }
            }
        }
    }
    private void CheckControl(Control theCntrl, string[] theId)
    {
        string theCntrlType = theId[0];
        foreach (Control x in theCntrl.Controls)
        {
            if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                CheckControl(x, theId);
            else if (x.ID == theId[1] && x.GetType().ToString() == theCntrlType && theCntrlType == "System.Web.UI.WebControls.CheckBox")
            {
                HtmlCheckBoxSelect(x);
                return;
            }
            else if (x.ID == theId[1] && x.GetType().ToString() == theCntrlType && theCntrlType == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
            {
                HtmlRadioButtonSelect(x);
                return;
            }
        }
    }
    private void Attributes()
    {

        IIQCareSystem SystemManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = SystemManager.SystemDate();
        SystemManager = null;
        txtSysDate.Text = theCurrentDate.ToString(Session["AppDateFormat"].ToString());
        txtlastName.Attributes.Add("onkeyup", "chkString('" + txtlastName.ClientID + "')");
        txtfirstName.Attributes.Add("onkeyup", "chkString('" + txtfirstName.ClientID + "')");
        TxtDOB.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3');");
        TxtDOB.Attributes.Add("onblur", "ValidateAge(); DateFormat(this,this.value,event,true,'3'); CalcualteAge('" + txtageCurrentYears.ClientID + "','" + txtageCurrentMonths.ClientID + "','" + TxtDOB.ClientID + "','" + txtSysDate.ClientID + "'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + TxtDOB.ClientID + "', '" + TxtDOB.ClientID + "');");
        //Add by Rahmat as on 07-Feb-2018 for calculate year and month and fill text box from first time on words..
        TxtDOB.Attributes.Add("onChange", "CalcualteAge('" + txtageCurrentYears.ClientID + "','" + txtageCurrentMonths.ClientID + "','" + TxtDOB.ClientID + "','" + txtSysDate.ClientID + "');");

        txtRegDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
        txtRegDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtRegDate.ClientID + "', '" + txtRegDate.ClientID + "');");

        txtageCurrentYears.Attributes.Add("onkeyup", "chkNumeric('" + txtageCurrentYears.ClientID + "')");
        txtageCurrentMonths.Attributes.Add("onkeyup", "chkNumeric('" + txtageCurrentMonths.ClientID + "')");
        txtphone.Attributes.Add("onkeyup", "chkNumeric('" + txtphone.ClientID + "')");
        txtemergContactPhone.Attributes.Add("onkeyup", "chkNumeric('" + txtemergContactPhone.ClientID + "')");
        txtNationalID.Attributes.Add("onkeyup", "chkNumeric('" + txtNationalID.ClientID + "')");
        //txtNationalID.Attributes.Add("onblur", "chkLength();");
        txtageCurrentMonths.Attributes.Add("onkeyup", "chkNumeric('" + txtageCurrentMonths.ClientID + "')");
        txtageCurrentYears.Attributes.Add("onkeyup", "chkNumeric('" + txtageCurrentYears.ClientID + "')");

    }

    private void HashTableParameter()
    {
        try
        {
            htParameters = new Hashtable();
            htParameters.Clear();
            htParameters.Add("FirstName", txtfirstName.Text.Trim());
            htParameters.Add("MiddleName", txtmiddleName.Text.Trim());
            htParameters.Add("LastName", txtlastName.Text.Trim());
            htParameters.Add("Gender", ddgender.SelectedValue);
            htParameters.Add("DOB", TxtDOB.Text);
            htParameters.Add("RegistrationDate", txtRegDate.Value);
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
    private void LoadFieldTypeControl(string ControlID, string Column, string FieldId, string CodeID, string Label, string Table, string BindSource, Boolean theEnable)
    {
        try
        {
            DataTable theBusinessRuleDT = (DataTable)ViewState["BusRule"];
            DataView theBusinessRuleDV = new DataView(theBusinessRuleDT);
            DataView theAutoDV = new DataView();
            theBusinessRuleDV.RowFilter = "BusRuleId=17 and FieldId = " + FieldId.ToString();
            if (ControlID == "1") ///SingleLine Text Box
            {
                PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%' class='table-condensed'>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "' >" + Label + " :</label>"));
                }
                else
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                TextBox theSingleText = new TextBox();
                theSingleText.ID = "TXT-" + Column + "-" + Table + "-" + FieldId;

                if (hdnIds.Value == "")
                {
                    hdnIds.Value = theSingleText.ID;
                }
                else
                {
                    hdnIds.Value = hdnIds.Value + "," + theSingleText.ID;
                }

                theSingleText.Width = 180;
                theSingleText.MaxLength = 50;
                theSingleText.Enabled = theEnable;
                PnlDynamicElements.Controls.Add(theSingleText);
                ApplyBusinessRules(theSingleText, ControlID, theEnable);
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));

            }
            else if (ControlID == "2") ///DecimalTextBox
            {

                PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%' class='table-condensed'>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                TextBox theSingleDecimalText = new TextBox();
                theSingleDecimalText.ID = "TXT-" + Column + "-" + Table + "-" + FieldId;
                if (hdnIds.Value == "")
                {
                    hdnIds.Value = theSingleDecimalText.ID;
                }
                else
                {
                    hdnIds.Value = hdnIds.Value + "," + theSingleDecimalText.ID;
                }
                theSingleDecimalText.Width = 180;
                theSingleDecimalText.MaxLength = 50;
                theSingleDecimalText.Enabled = theEnable;
                PnlDynamicElements.Controls.Add(theSingleDecimalText);
                ApplyBusinessRules(theSingleDecimalText, ControlID, theEnable);
                theSingleDecimalText.Attributes.Add("onkeyup", "chkNumeric('" + theSingleDecimalText.ClientID + "')");
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));

            }
            else if (ControlID == "3")   /// Numeric (Integer)
            {
                PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%' class='table-condensed'>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                TextBox theNumberText = new TextBox();
                theNumberText.ID = "TXTNUM-" + Column + "-" + Table + "-" + FieldId;
                theNumberText.Width = 100;
                theNumberText.MaxLength = 10;
                theNumberText.Enabled = theEnable;
                PnlDynamicElements.Controls.Add(theNumberText);
                theNumberText.Attributes.Add("onkeyup", "chkInteger('" + theNumberText.ClientID + "')");
                ApplyBusinessRules(theNumberText, ControlID, theEnable);
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
            }

            else if (ControlID == "4") /// Dropdown
            {
                bool theCntrlPresent = false;
                if (theCntrlPresent != true)
                {

                    PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%' class='table-condensed'>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    DropDownList ddlSelectListAuto = new DropDownList();
                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    else
                    {
                        PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                    DropDownList ddlSelectList = new DropDownList();
                    ddlSelectList.ID = "SELECTLIST-" + Column + "-" + Table + "-" + FieldId;
                    if (CodeID == "")
                    {
                        CodeID = "0";
                    }
                    DataView theDV = new DataView(theDSXML.Tables[BindSource]);
                    if (VisitID == 0)
                    {
                        if (BindSource.ToUpper() == "MST_DISTRICT" || BindSource.ToUpper() == "MST_VILLAGE" || BindSource.ToUpper() == "MST_TOWN" || BindSource.ToUpper() == "MST_COUNTRY" || BindSource.ToUpper() == "MST_PROVINCE" || BindSource.ToUpper() == "MST_WARD")
                        {
                            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
                        }
                        else if (BindSource.ToUpper() == "MST_DECODE")
                        {
                            DataView theDV4 = new DataView(((DataSet)Session["AllData"]).Tables[1]);
                            theDV4.RowFilter = "FieldId=" + FieldId + "";
                            DataTable theDT4 = theDV4.ToTable();
                            if (theDT4.Rows.Count > 0)
                            {
                                theDV.RowFilter = "(ModuleId IN(0, " + theDT4.Rows[0]["ModuleId"] + ") or ModuleId Is null) and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + CodeID + "";
                            }
                        }
                        else if (BindSource.ToUpper() == "MST_PMTCTDECODE" || BindSource.ToUpper() == "MST_MODDECODE")
                        { theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + CodeID + ""; }
                        else
                        {
                            theDV.RowFilter = "DeleteFlag=0";
                        }
                    }
                    else
                    {
                        if (BindSource.ToUpper() == "MST_DISTRICT" || BindSource.ToUpper() == "MST_VILLAGE" || BindSource.ToUpper() == "MST_TOWN" || BindSource.ToUpper() == "MST_COUNTRY" || BindSource.ToUpper() == "MST_PROVINCE" || BindSource.ToUpper() == "MST_WARD")
                        {
                            theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
                        }
                        else if (BindSource.ToUpper() == "MST_DECODE")
                        {
                            DataView theDV4 = new DataView(((DataSet)Session["AllData"]).Tables[1]);
                            theDV4.RowFilter = "FieldId=" + FieldId + "";
                            DataTable theDT4 = theDV4.ToTable();
                            if (theDT4.Rows.Count > 0)
                            {
                                theDV.RowFilter = "(ModuleId IN(0, " + theDT4.Rows[0]["ModuleId"] + ") or ModuleId Is null) and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + CodeID + "";
                            }
                        }
                        else if (BindSource.ToUpper() == "MST_PMTCTDECODE" || BindSource.ToUpper() == "MST_MODDECODE")
                        {
                            theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + CodeID + "";
                        }
                    }

                    //if (theDV.Table != null)
                    //{
                    IQCareUtils theUtils = new IQCareUtils();
                    BindFunctions BindManager = new BindFunctions();
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    //DataView theDVCon = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                    //theDVCon.RowFilter = "FieldId=" + FieldId + "";
                    //DataTable theDTFilter = theDVCon.ToTable();
                    //DataView theDVSelect = new DataView(theDT);
                    //if (BindSource.ToUpper() == "MST_DECODE")
                    //{
                    //    theDVSelect.RowFilter = "(ModuleId IN(" + theDTFilter.Rows[0]["ModuleId"] + ") and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + "))";
                    //}
                    ////DataTable theDTCon = theDVSelect.ToTable();
                    if (theDT != null)
                    {
                        DataTable theDTCon = theDT.Copy();

                        if (theDTCon.Rows.Count > 0)
                        {
                            BindManager.BindCombo(ddlSelectListAuto, theDTCon, "Name", "ID");
                            BindManager.BindCombo(ddlSelectList, theDTCon, "Name", "ID");
                        }
                        if (theDTCon.Rows.Count == 0)
                        {
                            ListItem theItem = new ListItem();
                            theItem.Text = "Select";
                            theItem.Value = "0";
                            ddlSelectList.Items.Add(theItem);
                        }
                    }
                    else
                    {
                        ListItem theItem1 = new ListItem();
                        theItem1.Text = "Select";
                        theItem1.Value = "0";
                        ddlSelectList.Items.Add(theItem1);
                    }
                    ddlSelectList.Width = 180;
                    ddlSelectList.AutoPostBack = true;
                    ddlSelectList.Enabled = theEnable;
                    if (theConditional == true && theEnable == true)
                    {
                        ddlSelectList.AutoPostBack = true;
                        ddlSelectList.SelectedIndexChanged += new EventHandler(ddlSelectList_SelectedIndexChanged);
                    }

                    PnlDynamicElements.Controls.Add(ddlSelectList);
                    ApplyBusinessRules(ddlSelectList, ControlID, theEnable);

                    PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
                }

            }
            else if (ControlID == "5") ///Date
            {
                PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%' class='table-condensed'>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                TextBox theDateText = new TextBox();
                theDateText.ID = "TXTDT-" + Column + "-" + Table + "-" + FieldId;
                if (hdnIds.Value == "")
                {
                    hdnIds.Value = theDateText.ID;
                }
                else
                {
                    hdnIds.Value = hdnIds.Value + "," + theDateText.ID;
                }
                Control ctl = (TextBox)theDateText;
                theDateText.Width = 83;
                theDateText.MaxLength = 11;
                theDateText.Enabled = theEnable;
                PnlDynamicElements.Controls.Add(theDateText);
                theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                ApplyBusinessRules(theDateText, ControlID, theEnable);
                PnlDynamicElements.Controls.Add(new LiteralControl("&nbsp;"));

                Image theDateImage = new Image();
                theDateImage.ID = "img" + theDateText.ID;
                theDateImage.Height = 22;
                theDateImage.Width = 22;
                theDateImage.Visible = theEnable;
                theDateImage.ToolTip = "Date Helper";
                theDateImage.ImageUrl = "~/images/cal_icon.gif";

                theDateImage.Attributes.Add("onClick", "w_displayDatePicker('" + ((TextBox)ctl).ClientID + "');");

                PnlDynamicElements.Controls.Add(theDateImage);
                ApplyBusinessRules(theDateImage, ControlID, theEnable);
                PnlDynamicElements.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
            }
            else if (ControlID == "6")  /// Radio Button
            {

                PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%' class='table-condensed'>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                HtmlInputRadioButton theYesNoRadio1 = new HtmlInputRadioButton();
                theYesNoRadio1.ID = "RADIO1-" + Column + "-" + Table + "-" + FieldId;
                theYesNoRadio1.Value = "Yes";
                theYesNoRadio1.Name = "" + Column + "";
                if (theConditional == true && theEnable == true)
                    theYesNoRadio1.Attributes.Add("onclick", "down(this);SetValue('HdnCntrl','System.Web.UI.HtmlControls.HtmlInputRadioButton%" + theYesNoRadio1.ClientID + "');");
                else
                    theYesNoRadio1.Attributes.Add("onclick", "down(this);");
                theYesNoRadio1.Attributes.Add("onfocus", "up(this)");
                PnlDynamicElements.Controls.Add(theYesNoRadio1);
                theYesNoRadio1.Visible = theEnable;
                ApplyBusinessRules(theYesNoRadio1, ControlID, theEnable);

                if (rdoTrueFalseStatus == false)
                {
                    theEnable = false;
                }


                theYesNoRadio1.Visible = theEnable;
                PnlDynamicElements.Controls.Add(new LiteralControl("<label align='labelright' id='lblYes-" + FieldId + "'>Yes</label>"));

                HtmlInputRadioButton theYesNoRadio2 = new HtmlInputRadioButton();
                theYesNoRadio2.ID = "RADIO2-" + Column + "-" + Table + "-" + FieldId;
                theYesNoRadio2.Value = "No";
                theYesNoRadio2.Name = "" + Column + "";
                if (theConditional == true && theEnable == true)
                    theYesNoRadio2.Attributes.Add("onclick", "down(this);SetValue('HdnCntrl','System.Web.UI.HtmlControls.HtmlInputRadioButton%" + theYesNoRadio2.ClientID + "');");
                else
                    theYesNoRadio2.Attributes.Add("onclick", "down(this);");
                theYesNoRadio2.Attributes.Add("onchange", "up(this)");
                PnlDynamicElements.Controls.Add(theYesNoRadio2);
                ApplyBusinessRules(theYesNoRadio2, ControlID, theEnable);
                theYesNoRadio2.Visible = theEnable;
                PnlDynamicElements.Controls.Add(new LiteralControl("<label align='labelright' id='lblNo-" + FieldId + "'>No</label>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));

            }
            else if (ControlID == "7") //Checkbox
            {
                PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%' class='table-condensed'> "));
                PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));


                HtmlInputCheckBox theChk = new HtmlInputCheckBox();
                theChk.ID = "Chk-" + Column + "-" + Table + "-" + FieldId;
                theChk.Visible = theEnable;
                PnlDynamicElements.Controls.Add(theChk);
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));

            }
            else if (ControlID == "8")  /// MultiLine TextBox
            {
                PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%' class='table-condensed'>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                TextBox theMultiText = new TextBox();
                theMultiText.ID = "TXTMulti-" + Column + "-" + Table + "-" + FieldId;
                theMultiText.Width = 200;
                theMultiText.TextMode = TextBoxMode.MultiLine;
                theMultiText.MaxLength = 200;
                theMultiText.Enabled = theEnable;
                PnlDynamicElements.Controls.Add(theMultiText);
                ApplyBusinessRules(theMultiText, ControlID, theEnable);
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
            }
            else if (ControlID == "9") ///  MultiSelect List 
            {

                PnlDynamicElements.Controls.Add(new LiteralControl("<table class='table-condensed'>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                if (SetBusinessrule(FieldId, Column) == true)
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                else
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                }
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                //WithPanel
                Panel PnlMulti = new Panel();
                PnlMulti.ID = "Pnl_" + FieldId;
                PnlMulti.ToolTip = Label;
                PnlMulti.Enabled = theEnable;
                PnlMulti.Controls.Add(new LiteralControl("<DIV class = 'customdivbordermultiselect'  runat='server' nowrap='nowrap'>"));

                if (CodeID == "")
                {
                    CodeID = "0";
                }
                string theCodeFldName = "";
                DataTable theBindTbl = theDSXML.Tables[BindSource];
                if (theBindTbl.Columns.Contains("CategoryId") == true)
                    theCodeFldName = "CategoryId";
                else if (theBindTbl.Columns.Contains("SectionId") == true)
                    theCodeFldName = "SectionId";
                else
                    theCodeFldName = "CodeId";
                DataView theDV = new DataView(theDSXML.Tables[BindSource]);
                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and " + theCodeFldName + "=" + CodeID + "";
                IQCareUtils theUtils = new IQCareUtils();
                BindFunctions BindManager = new BindFunctions();
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                if (theDT != null)
                {
                    for (int i = 0; i < theDT.Rows.Count; i++)
                    {
                        CheckBox chkbox = new CheckBox();
                        chkbox.ID = Convert.ToString("CHKMULTI-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId);
                        chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);
                        if (chkbox.Text == "Other")
                        {
                            PnlMulti.Controls.Add(chkbox);
                            PnlMulti.Controls.Add(new LiteralControl("<DIV  class='pad10' id='" + Column + "' style='DISPLAY:none'>Specify: "));
                            HtmlInputText HTextother = new HtmlInputText();
                            HTextother.ID = "TXTMULTI-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId;
                            HTextother.Size = 10;
                            PnlMulti.Controls.Add(HTextother);
                            PnlMulti.Controls.Add(new LiteralControl(HTextother.Value));
                            PnlMulti.Controls.Add(new LiteralControl("</DIV>"));
                            if (theConditional == true && theEnable == true)
                                chkbox.Attributes.Add("onclick", "toggle('" + Column + "');SetValue('HdnCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");
                            else
                                chkbox.Attributes.Add("onclick", "toggle('" + Column + "');");

                        }
                        else
                        {
                            if (theConditional == true && theEnable == true)
                                chkbox.Attributes.Add("onclick", "SetValue('HdnCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");

                            PnlMulti.Controls.Add(chkbox);
                            chkbox.Width = 300;
                            PnlMulti.Controls.Add(new LiteralControl("<br>"));

                        }
                    }
                }
                PnlMulti.Controls.Add(new LiteralControl("</DIV>"));
                PnlDynamicElements.Controls.Add(PnlMulti);
                ApplyBusinessRules(PnlMulti, ControlID, theEnable);
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
            }
            else if (ControlID == "13")  /// Placeholder
            {
                PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%' class='table-condensed'>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:100%;Height:25px' align='right'>"));
                HtmlGenericControl div1 = new HtmlGenericControl("div");
                div1.ID = "DIVPLC-" + Column + "-" + FieldId;
                PlaceHolder thePlchlderText = new PlaceHolder();
                thePlchlderText.ID = "plchlder-" + Column + "-" + FieldId;
                thePlchlderText.Controls.Add(div1);
                PnlDynamicElements.Controls.Add(thePlchlderText);
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
            }

            else if (ControlID == "14")
            {
                bool theCntrlPresent = false;
                if (theCntrlPresent != true)
                {

                    PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%' class='table-condensed'>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    DropDownList ddlSelectListAuto = new DropDownList();
                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    else
                    {
                        PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                    DropDownList ddlSelectList = new DropDownList();
                    ddlSelectList.ID = "SELECTLIST-" + Column + "-" + Table + "-" + FieldId;
                    IQCareUtils theUtil = new IQCareUtils();
                    DataTable theDT = theUtil.CreateTimeTable(15);
                    DataRow theDR = theDT.NewRow();
                    theDR[0] = "0";
                    theDR[1] = "Select";
                    theDT.Rows.InsertAt(theDR, 0);
                    ddlSelectList.DataSource = theDT;
                    ddlSelectList.DataTextField = "Time";
                    ddlSelectList.DataValueField = "Id";
                    ddlSelectList.DataBind();
                    ddlSelectList.SelectedValue = "Select";
                    ddlSelectList.Width = 180;
                    //ddlSelectList.AutoPostBack = true;
                    ddlSelectList.Enabled = theEnable;
                    if (theConditional == true && theEnable == true)
                    {
                        //ddlSelectList.AutoPostBack = true;
                        //ddlSelectList.SelectedIndexChanged += new EventHandler(ddlSelectList_SelectedIndexChanged);
                    }
                    PnlDynamicElements.Controls.Add(ddlSelectList);
                    ApplyBusinessRules(ddlSelectList, ControlID, theEnable);
                    PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
                }
            }

        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    private void ApplyBusinessRules(object theControl, string ControlID, bool theConField)
    {
        try
        {
            DataTable theDT = (DataTable)ViewState["BusRule"];
            string Max = "", Min = "", Column = "";
            bool theEnable = theConField;
            string[] Field;
            if (ControlID == "9")
            {
                Field = ((Control)theControl).ID.Split('_');
            }
            else
            {
                Field = ((Control)theControl).ID.Split('-');
            }
            foreach (DataRow DR in theDT.Rows)
            {
                if (Field[0] == "Pnl")
                {

                    if (Field[1] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "14"
                        && Session["PatientSex"].ToString() != "Male")
                        theEnable = false;

                    if (Field[1] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "15"
                        && Session["PatientSex"].ToString() != "Female")
                        theEnable = false;

                    if (Field[1] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "16")
                    {
                        if ((DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                        {
                            if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                            {
                            }
                            else
                                theEnable = false;
                        }
                    }

                }
                else
                {
                    if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2].ToLower() == Convert.ToString(DR["TableName"]).ToLower() && Field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "2")
                    {
                        Max = Convert.ToString(DR["Value"]);
                        Column = Convert.ToString(DR["FieldLabel"]);
                    }
                    if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2].ToLower() == Convert.ToString(DR["TableName"]).ToLower() && Field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "3")
                    {
                        Min = Convert.ToString(DR["Value"]);

                    }
                    if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2].ToLower() == Convert.ToString(DR["TableName"]).ToLower() && Field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "16")
                    {
                        if ((DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                        {
                            if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                            {
                            }
                            else
                                theEnable = false;
                        }
                    }
                }
            }

            if (theControl.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
            {
                Field = ((Control)theControl).ID.Split('-');
                TextBox tbox = (TextBox)theControl;
                tbox.Enabled = theEnable;
                if (ControlID == "1")
                { }
                else if (ControlID == "2" && Field[0] == "TXT")
                {
                    tbox.Attributes.Add("onkeyup", "chkDecimal('" + tbox.ClientID + "')");
                }
                else if (ControlID == "3" && Field[0] == "TXTNUM")
                {
                    tbox.Attributes.Add("onkeyup", "chkNumeric('" + tbox.ClientID + "')");
                }
                else if (ControlID == "5" && Field[0] == "TXTDT")
                {
                    tbox.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                    tbox.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                }
                if (Max != "" && Min != "")
                {
                    tbox.Attributes.Add("onblur", "isBetween('" + tbox.ClientID + "', '" + Column + "', '" + Min + "', '" + Max + "')");
                }
                else if (Max != "")
                {
                    tbox.Attributes.Add("onblur", "checkMax('" + tbox.ClientID + "', '" + Column + "', '" + Max + "')");
                }
                else if (Min != "")
                {
                    tbox.Attributes.Add("onblur", "checkMin('" + tbox.ClientID + "', '" + Column + "', '" + Min + "')");
                }

            }
            else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
            {
                DropDownList ddList = (DropDownList)theControl;
                ddList.Enabled = theEnable;

            }
            else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
            {
                CheckBox Multichk = (CheckBox)theControl;
                Multichk.Enabled = theEnable;
            }
            else if (theControl.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
            {
                HtmlInputRadioButton Rdobtn = (HtmlInputRadioButton)theControl;
                Rdobtn.Visible = theEnable;
                rdoTrueFalseStatus = true;
                if (theEnable == false)
                {
                    rdoTrueFalseStatus = false;
                }
            }
            else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.Image")
            {
                Image img = (Image)theControl;
                img.Visible = theEnable;
            }
            else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.Panel")
            {
                Panel pnl = (Panel)theControl;
                pnl.Enabled = theEnable;
            }
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }
    void ddlSelectList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList theDList = ((DropDownList)sender);
        DataSet theDS = (DataSet)Session["AllData"];
        string[] theCntrl = theDList.ID.Split('-');
        DataView theDVConFieldEnable = new DataView(theDS.Tables[6]);
        theDVConFieldEnable.RowFilter = "ConditionalFieldSectionId=" + theDList.SelectedValue.ToString() + "";
        DataTable Dtcon = new DataTable();
        IQCareUtils theUtils = new IQCareUtils();
        Dtcon = theUtils.CreateTableFromDataView(theDVConFieldEnable);
        foreach (DataRow theDR in Dtcon.Rows)
        {
            foreach (Control x in PnlDynamicElements.Controls)
            {
                if (x.ID != null)
                {
                    string[] theIdent = x.ID.Split('-');
                    if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                        {
                            ((TextBox)x).Enabled = true;
                            if (theDR["controlid"].ToString() == "1")
                            {
                                ApplyBusinessRules(x, "1", true);
                            }
                            if (theDR["controlid"].ToString() == "2")
                            {
                                ApplyBusinessRules(x, "2", true);
                            }
                            if (theDR["controlid"].ToString() == "3")
                            {
                                ApplyBusinessRules(x, "3", true);
                            }
                        }
                        else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                        {
                            ((TextBox)x).Enabled = false;
                            ((TextBox)x).Text = "";
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                        {
                            if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                            {
                                ((DropDownList)x).Enabled = true;
                                ApplyBusinessRules(x, "4", true);
                            }
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            {
                                ((DropDownList)x).Enabled = false;
                                ((DropDownList)x).SelectedValue = "0";

                            }
                        }
                    }
                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl_" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                        {
                            ((Panel)x).Enabled = true;
                            ApplyBusinessRules(x, "9", true);
                        }
                        else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                        {
                            ((Panel)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                        {
                            ((Image)x).Visible = true;
                            ApplyBusinessRules(x, "5", true);
                        }
                        else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            ((Image)x).Visible = false;
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                        {
                            ((HtmlInputRadioButton)x).Visible = true;
                            //ApplyBusinessRules(x, "6", true);
                        }
                        else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            ((HtmlInputRadioButton)x).Visible = false;
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                            ((HtmlInputCheckBox)x).Visible = true;
                        else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            ((HtmlInputCheckBox)x).Visible = false;
                    }
                }
            }
        }
    }

    private StringBuilder InsertMultiSelectList(string PatientID, string FieldName, int FeatureID, string Multi_SelectTable, Int32 theControlId, Int32 theFieldId)
    {
        StringBuilder Insertcbl = new StringBuilder();
        foreach (Control y in PnlDynamicElements.Controls)
        {
            if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
            {
                if (((Panel)y).ID == "Pnl_" + theControlId.ToString() && ((Panel)y).Enabled == false)
                    return Insertcbl;
                foreach (Control x in y.Controls)
                {

                    if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                    {
                        string[] TableName = ((CheckBox)x).ID.Split('-');
                        if (TableName.Length == 5)
                        {
                            string Table = TableName[3];

                            if (Table == Multi_SelectTable)
                            {
                                if (Table == "dtl_CustomField")
                                {
                                    Table = "dtl_FB_" + TableName[2] + "";
                                    Table = Table.Replace(' ', '_');
                                }
                                if (Table != "dtl_CustomField" && Convert.ToInt32(TableName[4]) == theFieldId)
                                {

                                    if (((CheckBox)x).Checked == true && ((CheckBox)x).Text != "Other")
                                    {
                                        Insertcbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                        Insertcbl.Append("values (" + PatientID + ",  IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                        Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                    }

                                    else if (((CheckBox)x).Checked == true && ((CheckBox)x).Text == "Other")
                                    {
                                        ViewState["OtherNote"] = ((CheckBox)x).Text;
                                    }
                                }

                                else if (Convert.ToInt32(TableName[4]) == theFieldId)
                                {
                                    if (((CheckBox)x).Checked == true)
                                    {
                                        Insertcbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                        Insertcbl.Append("values (" + PatientID + ",  IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                        Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                    }

                                }
                            }
                        }
                    }

                    if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                    {
                        string[] TableName = ((HtmlInputText)x).ID.Split('-');
                        string Table = TableName[3];
                        string Other = "";
                        if (Table == Multi_SelectTable)
                        {
                            if (Table == "dtl_CustomField")
                            {
                                Table = "dtl_FB_" + TableName[2] + "";
                                Table = Table.Replace(' ', '_');
                            }
                            if (Table != "dtl_CustomField")
                            {
                                string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                                DataSet dsMultiSelectList = new DataSet();
                                dsMultiSelectList.ReadXml(filePath);
                                DataTable DT = dsMultiSelectList.Tables[0];
                                foreach (DataRow DR in DT.Rows)
                                {
                                    if (DR[0].ToString() == Table)
                                    {
                                        Other = DR[2].ToString();
                                    }
                                }

                                if (Convert.ToString(ViewState["OtherNote"]) != "" && ((HtmlInputText)x).Value != "")
                                {
                                    Insertcbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "],[" + Other + "], [UserID], [CreateDate])");
                                    Insertcbl.Append("values (" + PatientID + ", IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                    Insertcbl.Append("'" + ((HtmlInputText)x).Value + "', " + Session["AppUserId"].ToString() + ", Getdate())");
                                }
                            }
                        }
                    }
                }
            }

        }
        return Insertcbl;
    }

    private StringBuilder UpdateMultiSelectList(int PatientID, int FeatureID, int VisitID, int LocationID, string Multi_SelectTable, string ColumnName, int DeleteFlag, Int32 theControlId)
    {
        StringBuilder Updatecbl = new StringBuilder();

        if (DeleteFlag == 0)
        {
            if (Multi_SelectTable == "dtl_CustomField")
            {
                Multi_SelectTable = "dtl_FB_" + ColumnName + "";
                Multi_SelectTable = Multi_SelectTable.Replace(' ', '_');
            }
            if (Updatecbl.ToString().Contains(Multi_SelectTable.ToString()) == false)
                Updatecbl.Append("Delete from [" + Multi_SelectTable + "] where [ptn_pk]=" + PatientID + " and [Visit_Pk]=" + VisitID + " and [LocationID]=" + LocationID + "");
            return Updatecbl;
        }
        else
        {
            foreach (Control y in PnlDynamicElements.Controls)
            {
                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    foreach (Control x in y.Controls)
                    {
                        if (((Panel)y).ID == "Pnl_" + theControlId.ToString() && ((Panel)y).Enabled == false)
                            return Updatecbl;
                        if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                        {
                            string[] TableName = ((CheckBox)x).ID.Split('-');
                            if (TableName.Length == 5)
                            {
                                string Table = TableName[3];
                                if (Table == Multi_SelectTable)
                                {
                                    if (Table == "dtl_CustomField")
                                    {
                                        Table = "dtl_FB_" + TableName[2] + "";
                                        Table = Table.Replace(' ', '_');
                                    }
                                    if (Table != "dtl_CustomField")
                                    {


                                        if (((CheckBox)x).Checked == true && ((CheckBox)x).Text != "Other")
                                        {
                                            Updatecbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                            Updatecbl.Append("values (" + PatientID + ",  " + VisitID + ", " + LocationID + ", " + TableName[1] + ",");
                                            Updatecbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                        }

                                        else if (((CheckBox)x).Checked == true && ((CheckBox)x).Text == "Other")
                                        {
                                            ViewState["OtherNote"] = ((CheckBox)x).Text;
                                        }
                                    }

                                    else
                                    {
                                        if (((CheckBox)x).Checked == true)
                                        {
                                            Updatecbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                            Updatecbl.Append("values (" + PatientID + ",  " + VisitID + ", " + LocationID + "," + TableName[1] + ",");
                                            Updatecbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                        }

                                    }
                                }
                            }
                        }

                        if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        {
                            string[] TableName = ((HtmlInputText)x).ID.Split('-');
                            string Table = TableName[3];
                            string Other = "";
                            if (Table == Multi_SelectTable)
                            {
                                if (Table == "dtl_CustomField")
                                {
                                    Table = "dtl_FB_" + TableName[2] + "";
                                    Table = Table.Replace(' ', '_');
                                }
                                if (Table != "dtl_CustomField")
                                {
                                    string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                                    DataSet dsMultiSelectList = new DataSet();
                                    dsMultiSelectList.ReadXml(filePath);
                                    DataTable DT = dsMultiSelectList.Tables[0];
                                    foreach (DataRow DR in DT.Rows)
                                    {
                                        if (DR[0].ToString() == Table)
                                        {
                                            Other = DR[2].ToString();
                                        }
                                    }

                                    if (Convert.ToString(ViewState["OtherNote"]) != "" && ((HtmlInputText)x).Value != "")
                                    {
                                        Updatecbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "],[" + Other + "], [UserID], [CreateDate])");
                                        Updatecbl.Append("values (" + PatientID + ", " + VisitID + ", " + LocationID + "," + TableName[1] + ",");
                                        Updatecbl.Append("'" + ((HtmlInputText)x).Value + "', " + Session["AppUserId"].ToString() + ", Getdate())");
                                        ViewState["OtherNote"] = null;
                                    }
                                }
                            }
                        }

                    }

                }
            }
            return Updatecbl;
        }
    }

    private Boolean SetBusinessrule(string FieldID, string FieldLabel)
    {

        DataTable theDT = (DataTable)ViewState["BusRule"];
        foreach (DataRow DR in theDT.Rows)
        {
            if (Convert.ToString(DR["FieldID"]) == FieldID && Convert.ToString(DR["FieldName"]) == FieldLabel && Convert.ToString(DR["BusRuleId"]) == "1")
            {
                return true;
            }
        }
        return false;
    }
    private void SectionHeading(String H2)
    {
        PnlDynamicElements.Controls.Add(new LiteralControl("<br>"));
        PnlDynamicElements.Controls.Add(new LiteralControl("<h4 class='forms h4' align='left'>" + H2 + "</h4>"));
    }

    private void LoadPredefinedLabel_Field(int FeatureID)
    {
        IPatientRegistration IPatientCustomFormMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
        DataSet theDS = IPatientCustomFormMgr.GetFieldName_and_Label(FeatureID, PatientID);
        Session["AllData"] = theDS;
        if (theDS.Tables.Count > 0)
        {
            if (theDS.Tables[10].Rows.Count == 0)
            {
                return;
            }
        }
        Session["AllData"] = theDS;
        ViewState["LnkTable"] = theDS.Tables[1];
        ViewState["LnkConTable"] = theDS.Tables[6];
        ViewState["BusRule"] = theDS.Tables[2];
        Session["SessionBusRule"] = theDS.Tables[2];
        DataTable DT = theDS.Tables[1].DefaultView.ToTable(true, "SectionID", "SectionName").Copy();
        int Numtds = 2, td = 1;
        PnlDynamicElements.Controls.Clear();
        DataTable theConditionalFields = theDS.Tables[6].Copy();
        theConditionalFields.Columns.Add("Load", typeof(System.String));
        theConditionalFields.Columns["Load"].DefaultValue = "T";
        foreach (DataRow theMDR in theDS.Tables[6].Rows)
        {
            Int32 theFieldId = Convert.ToInt32(theMDR["FieldId"]);
            bool theRecFnd = false;
            foreach (DataRow theDR in theConditionalFields.Rows)
            {
                if (Convert.ToInt32(theDR["FieldId"]) == theFieldId && theRecFnd == true)
                    theDR["Load"] = "F";
                else if (Convert.ToInt32(theDR["FieldId"]) == theFieldId)
                {
                    theDR["Load"] = "T";
                    theRecFnd = true;
                }
            }
            theRecFnd = false;
        }
        theConditionalFields.AcceptChanges();
        foreach (DataRow dr in DT.Rows)
        {
            PnlDynamicElements.Controls.Add(new LiteralControl("<div id='" + dr["SectionName"].ToString().Trim() + "'>"));
            SectionHeading(dr["SectionName"].ToString());
            PnlDynamicElements.Controls.Add(new LiteralControl("<table cellspacing='6' cellpadding='0' width='100%' border='0'>"));
            foreach (DataRow DRLnkTable in theDS.Tables[1].Rows)
            {
                if (dr["SectionID"].ToString() == DRLnkTable["SectionID"].ToString())
                {
                    #region "CheckConditionalFields"
                    DataView theDVConditionalField = new DataView(theConditionalFields);
                    theDVConditionalField.RowFilter = "ConFieldId=" + DRLnkTable["FieldID"].ToString() + " and ConFieldPredefined=" + DRLnkTable["Predefined"].ToString() + " and Load = 'T'";
                    theDVConditionalField.Sort = "ConditionalFieldSectionId, Seq asc";
                    if (theDVConditionalField.Count > 0)
                        theConditional = true;
                    else
                        theConditional = false;
                    #endregion
                    if (td <= Numtds)
                    {
                        if (td == 1)
                            PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));

                        if (td == 1)
                        {
                            PnlDynamicElements.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                            LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString(), DRLnkTable["BindSource"].ToString(), true);
                            PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                            td++;
                        }
                        else
                        {
                            PnlDynamicElements.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                            LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString(), DRLnkTable["BindSource"].ToString(), true);
                            PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                            PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                            td = 1;
                        }
                    }
                    #region "Create Conditional Fields"
                    if (theConditional == true)
                    {
                        for (int i = 0; i < theDVConditionalField.Count; i++)
                        {
                            if (td <= Numtds)
                            {
                                if (td == 1)
                                    PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                                if (td == 1)
                                {
                                    PnlDynamicElements.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                    LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                    theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(),
                                    theDVConditionalField[i]["BindSource"].ToString(), false);
                                    PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                                    td++;
                                }
                                else
                                {
                                    PnlDynamicElements.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                    LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                    theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(),
                                    theDVConditionalField[i]["BindSource"].ToString(), false);
                                    PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                                    PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                                    td = 1;
                                }
                            }

                        }
                    }

                    #endregion
                }
            }
            if (td == 2)
            {
                PnlDynamicElements.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
            }
            td = 1;
            PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
            PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
            PnlDynamicElements.Controls.Add(new LiteralControl("</br>"));
        }

        ViewState["NoMulti"] = theDS.Tables[3];
    }

    private void LoadPatientStaticData(int Ptn_Pk)
    {
        String moduleID;
        if (Session["CEModule"] != null)
            moduleID = Session["CEModule"].ToString();
        IQCareUtils theUtil = new IQCareUtils();
        try
        {
            IPatientRegistration PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet theDS = PatientManager.GetPatientRegistration(Ptn_Pk, 12);
            ViewState["themstpatient"] = theDS.Tables[0];
            ViewState["VisitPk"] = theDS.Tables[4].Rows[0]["VisitId"].ToString();
            this.txtIQCareRef.Text = theDS.Tables[0].Rows[0]["IQNumber"].ToString();
            ViewState["IQNumber"] = txtIQCareRef.Text;
            this.ddgender.SelectedValue = theDS.Tables[0].Rows[0]["RegSex"].ToString();

            this.txtRegDate.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDS.Tables[0].Rows[0]["RegDate"]));
            this.txtageCurrentYears.Text = theDS.Tables[0].Rows[0]["Age"].ToString();
            this.txtageCurrentMonths.Text = theDS.Tables[0].Rows[0]["Age1"].ToString();
            this.txtlastName.Text = theDS.Tables[0].Rows[0]["LastName"].ToString();
            this.txtmiddleName.Text = theDS.Tables[0].Rows[0]["MiddleName"].ToString();
            this.txtfirstName.Text = theDS.Tables[0].Rows[0]["FirstName"].ToString();
            this.TxtDOB.Text = ((DateTime)theDS.Tables[0].Rows[0]["RegDOB"]).ToString(Session["AppDateFormat"].ToString());
            if (Convert.ToInt32(theDS.Tables[0].Rows[0]["DobPrecision"]) == 1)
            {
                this.rbtndobPrecEstimated.Checked = true;
            }
            else if (Convert.ToInt32(theDS.Tables[0].Rows[0]["DobPrecision"]) == 0)
            {
                this.rbtndobPrecExact.Checked = true;
            }
            this.ddmaritalStatus.SelectedValue = theDS.Tables[0].Rows[0]["MaritalStatus"].ToString();
            this.txtNationalID.Text = theDS.Tables[0].Rows[0]["ID/PassportNo"].ToString();
            this.ddvillageName.SelectedValue = theDS.Tables[0].Rows[0]["VillageName"].ToString();
            this.dddistrictName.SelectedValue = theDS.Tables[0].Rows[0]["DistrictName"].ToString();
            this.ddProvince.SelectedValue = theDS.Tables[0].Rows[0]["Province"].ToString();
            this.ddCountry.SelectedValue = theDS.Tables[0].Rows[0]["CountryID"].ToString();
            //this.ddCountry.Enabled = false;
            this.txtlocalCouncil.Text = theDS.Tables[0].Rows[0]["LocalCouncil"].ToString();
            this.txtaddress.Text = theDS.Tables[0].Rows[0]["Address"].ToString();
            this.txtphone.Text = theDS.Tables[0].Rows[0]["Phone"].ToString();
            this.txtemergContactName.Text = theDS.Tables[1].Rows[0]["EmergContactName"].ToString();
            this.txtemergContactPhone.Text = theDS.Tables[1].Rows[0]["EmergContactPhone"].ToString();
            this.txtemergContactAddress.Text = theDS.Tables[1].Rows[0]["EmergContactAddress"].ToString();
            this.ddEmergContactRelation.SelectedValue = theDS.Tables[1].Rows[0]["EmergContactRelation"].ToString();
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

    private void BindValue(int PatientID, int VisitID, int LocationID, Control theControl)
    {
        ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameterBCustom);
        DataTable theDT = SetControlIDs(PnlDynamicElements);
        DataTable TempDT = theDT.DefaultView.ToTable(true, "TableName").Copy();
        StringBuilder GetStaticData = new StringBuilder();
        GetStaticData.Append("Select VisitDate, Signature,DataQuality from ord_visit where Ptn_Pk=" + PatientID + " and Visit_Id=" + VisitID + " and LocationID=" + LocationID + "");
        DataSet theDS = new DataSet();
        DataSet TmpDS = MgrBindValue.Common_GetSaveUpdate(GetStaticData.ToString());
        DataTable theMstPatientDT = new DataTable();
        try
        {
            foreach (DataRow TempDR in TempDT.Rows)
            {
                string GetValue = "";
                if (Convert.ToString(TempDR["TableName"]).ToUpper() == "DTL_CUSTOMFIELD")
                {
                    theMstPatientDT = new DataTable();
                    string TableName = "DTL_FBCUSTOMFIELD_" + "Patient Registration".Replace(' ', '_');
                    GetValue = "Select * from [" + TableName + "] where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                }
                else
                {
                    if (Convert.ToString(TempDR["TableName"]).ToUpper() == "DTL_PATIENTCONTACTS")
                    {
                        theMstPatientDT = new DataTable();
                        GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and Visitid=" + VisitID + " and LocationId=" + LocationID + "";
                    }
                    else if (Convert.ToString(TempDR["TableName"]).ToUpper() == "MST_PATIENT")
                    {
                        theMstPatientDT = new DataTable();
                        theMstPatientDT = (DataTable)ViewState["themstpatient"];
                        GetValue = "";
                    }
                    else if ((Convert.ToString(TempDR["TableName"]).ToUpper() == "DTL_RURALRESIDENCE") || (Convert.ToString(TempDR["TableName"]).ToUpper() == "DTL_PATIENTHOUSEHOLDINFO"))
                    {
                        theMstPatientDT = new DataTable();
                        GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                    }
                    else
                    {

                        theMstPatientDT = new DataTable();
                        GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and VisitID=" + VisitID + " and LocationId=" + LocationID + "";
                    }
                }
                DataSet TempDS = new DataSet();
                if (GetValue != "")
                {
                    TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
                    theMstPatientDT = TempDS.Tables[0].Copy();
                }

                for (int i = 0; i <= theMstPatientDT.Columns.Count - 1; i++)
                {

                    foreach (Control x in theControl.Controls)
                    {
                        if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                        {

                            if ("TXTMulti-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                            {
                                if (theMstPatientDT.Rows.Count > 0)
                                {
                                    ((TextBox)x).Text = Convert.ToString(theMstPatientDT.Rows[0][i]);
                                }
                            }
                            if ("TXTSingle-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                            {
                                if (theMstPatientDT.Rows.Count > 0)
                                {
                                    ((TextBox)x).Text = Convert.ToString(theMstPatientDT.Rows[0][i]);
                                }
                            }

                            if ("TXT-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                            {
                                if (theMstPatientDT.Rows.Count > 0)
                                {
                                    ((TextBox)x).Text = Convert.ToString(theMstPatientDT.Rows[0][i]);
                                }
                            }
                            if ("TXTNUM-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                            {
                                if (theMstPatientDT.Rows.Count > 0)
                                {
                                    ((TextBox)x).Text = Convert.ToString(theMstPatientDT.Rows[0][i]);
                                }
                            }

                            if ("TXTDT-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                            {
                                if (theMstPatientDT.Rows.Count > 0)
                                {
                                    ((TextBox)x).Text = String.Format("{0:dd-MMM-yyyy}", theMstPatientDT.Rows[0][i]);
                                }
                            }

                        }

                        else if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                        {
                            if (Convert.ToString("SELECTLIST-" + theMstPatientDT.Columns[i] + "-" + TempDR["TableName"]).ToUpper() == ((DropDownList)x).ID.Substring(0, ((DropDownList)x).ID.LastIndexOf('-')).ToUpper())
                            {
                                if (theMstPatientDT.Rows.Count > 0)
                                {
                                    ((DropDownList)x).SelectedValue = Convert.ToString(theMstPatientDT.Rows[0][i]);

                                    //DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                    //string[] theId = ((DropDownList)x).ID.Split('-');
                                    //theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                    //if (theDVConditionalField.Count > 0)
                                    //{
                                    //EventArgs s = new EventArgs();
                                    //ddlSelectList_SelectedIndexChanged((DropDownList)x, s);
                                    //}

                                }
                            }

                        }
                        else if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                        {
                            if (theMstPatientDT.Columns[i].ToString() == ((HtmlInputRadioButton)x).Name)
                            {
                                for (int k = 0; k < theMstPatientDT.Rows.Count; k++)
                                {
                                    if (theMstPatientDT.Rows[k][theMstPatientDT.Columns[i]].ToString() == "True" || theMstPatientDT.Rows[k][theMstPatientDT.Columns[i]].ToString() == "1")
                                    {
                                        if ("RADIO1-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((HtmlInputRadioButton)x).ID.Substring(0, ((HtmlInputRadioButton)x).ID.LastIndexOf('-')))
                                        {
                                            ((HtmlInputRadioButton)x).Checked = true;
                                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                                            string[] theId = ((HtmlInputRadioButton)x).ID.Split('-');
                                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                            if (theDVConditionalField.Count > 0)
                                            {
                                                EventArgs s = new EventArgs();
                                                //this.HtmlRadioButtonSelect(x);
                                            }

                                        }

                                    }
                                    else if (theMstPatientDT.Rows[k][theMstPatientDT.Columns[i]].ToString() == "False" || theMstPatientDT.Rows[k][theMstPatientDT.Columns[i]].ToString() == "0")
                                    {
                                        if ("RADIO2-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((HtmlInputRadioButton)x).ID.Substring(0, ((HtmlInputRadioButton)x).ID.LastIndexOf('-')))
                                        {
                                            ((HtmlInputRadioButton)x).Checked = true;
                                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                                            string[] theId = ((HtmlInputRadioButton)x).ID.Split('-');
                                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                            if (theDVConditionalField.Count > 0)
                                            {
                                                EventArgs s = new EventArgs();
                                                //this.HtmlRadioButtonSelect(x);
                                            }

                                        }

                                    }
                                }
                            }
                        }

                        else if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if ("Chk-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((HtmlInputCheckBox)x).ID.Substring(0, ((HtmlInputCheckBox)x).ID.LastIndexOf('-')))
                            {
                                for (int k = 0; k < theMstPatientDT.Rows.Count; k++)
                                {

                                    if (theMstPatientDT.Rows[k][theMstPatientDT.Columns[i]].ToString() == "True")
                                    {
                                        ((HtmlInputCheckBox)x).Checked = true;

                                    }
                                    else { ((HtmlInputCheckBox)x).Checked = false; }

                                }
                            }
                        }

                    }
                }
            }
            foreach (Control y in PnlDynamicElements.Controls)
            {
                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    foreach (Control z in y.Controls)
                    {
                        if (z.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                        {
                            string[] Table = ((CheckBox)z).ID.Split('-');
                            if (Table.Length == 5)
                            {
                                string TableName = Table[3];
                                String GetValue = "";
                                string Id = Table[1];
                                if (TableName == "dtl_CustomField")
                                {
                                    TableName = "dtl_FB_" + Table[2] + "";
                                    TableName = TableName.Replace(' ', '_');
                                    GetValue = "Select * from " + TableName + " where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                                }
                                else
                                {
                                    GetValue = "Select * from " + TableName + " where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                                }

                                DataSet TmpDSMulti = MgrBindValue.Common_GetSaveUpdate(GetValue);
                                if (Table[3] == "dtl_CustomField")
                                {
                                    foreach (DataRow theDR in TmpDSMulti.Tables[0].Rows)
                                    {
                                        if (Id == theDR[Table[2]].ToString())
                                        {
                                            ((CheckBox)z).Checked = true;
                                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                                            string[] theId = ((CheckBox)z).ID.Split('-');
                                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                            if (theDVConditionalField.Count > 0)
                                            {
                                                EventArgs s = new EventArgs();
                                                this.HtmlCheckBoxSelect(z);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (DataRow theDR in TmpDSMulti.Tables[0].Rows)
                                    {
                                        if (Id == theDR[3].ToString())
                                        {
                                            if (((CheckBox)z).Text == "Other")
                                            {
                                                ((CheckBox)z).Checked = true;
                                                DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                                                string[] theId = ((CheckBox)z).ID.Split('-');
                                                theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                                if (theDVConditionalField.Count > 0)
                                                {
                                                    EventArgs s = new EventArgs();
                                                    this.HtmlCheckBoxSelect(z);
                                                }

                                                string script = "";
                                                script = "<script language = 'javascript' defer ='defer' id = 'Other'" + Id + ">\n";
                                                script += "show('" + Table[2] + "');\n";
                                                script += "</script>\n";
                                                RegisterStartupScript("Other" + Id + "", script);
                                                ViewState["Otherchk"] = ((CheckBox)z).Text;
                                                ViewState["Othertxt"] = theDR[4];
                                            }
                                            else
                                            {
                                                ((CheckBox)z).Checked = true;
                                                DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                                string[] theId = ((CheckBox)z).ID.Split('-');
                                                theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                                if (theDVConditionalField.Count > 0)
                                                {
                                                    EventArgs s = new EventArgs();
                                                    this.HtmlCheckBoxSelect(z);
                                                }
                                            }

                                        }
                                    }

                                }
                            }


                            if (z.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                            {
                                if (Convert.ToString(ViewState["Otherchk"]) == "Other")
                                {
                                    ((HtmlInputText)z).Value = Convert.ToString(ViewState["Othertxt"]);
                                }

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
        }

    }

    private StringBuilder UpdateCustomRegistrationData(int PatientID, int VisitID, int LocationID)
    {
        DataTable LnkDTUnique = new DataTable();
        StringBuilder sbUpdate = new StringBuilder();
        DataTable theDTNoMulti = new DataTable();
        DataTable theDTMulti = new DataTable();
        DataTable theDTConMulti = new DataTable();
        ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameterBCustom);
        DataTable theDT = SetControlIDs(PnlDynamicElements);

        if (ViewState["NoMulti"] != null)
        {
            theDTNoMulti = ((DataTable)ViewState["NoMulti"]);
            theDTMulti = ((DataTable)ViewState["LnkTable"]);
            theDTConMulti = ((DataTable)ViewState["LnkConTable"]);
            theDTNoMulti = theDTNoMulti.Select("IsNull(FieldName,'') <> ''").CopyToDataTable();
            LnkDTUnique = theDTNoMulti.DefaultView.ToTable(true, "PDFTableName", "FeatureName");

        }
        int DOBPrecision = 0;
        if (rbtndobPrecEstimated.Checked == true)
        {
            DOBPrecision = 1;
        }
        else if (rbtndobPrecExact.Checked == true)
        {
            DOBPrecision = 0;
        }
        else
        {
            DOBPrecision = 2;
        }
        /////////////////////////////////////////////////
        StringBuilder SbmstpatColumns = new StringBuilder();
        StringBuilder SbmstpatValues = new StringBuilder();
        SbmstpatColumns.Append("Update [MST_PATIENT]Set ");
        SbmstpatColumns.Append("FirstName=encryptbykey(key_guid('Key_CTC'),'" + txtfirstName.Text + "'), MiddleName=encryptbykey(key_guid('Key_CTC'),'" + txtmiddleName.Text + "'),");
        SbmstpatColumns.Append("LastName=encryptbykey(key_guid('Key_CTC'),'" + txtlastName.Text + "'), LocationID='" + Session["AppLocationId"] + "', RegistrationDate='" + txtRegDate.Value + "',[ID/PassportNo]='" + txtNationalID.Text + "',");
        SbmstpatColumns.Append("Sex='" + ddgender.SelectedValue + "',DOB='" + TxtDOB.Text + "',DobPrecision='" + DOBPrecision + "',MaritalStatus='" + ddmaritalStatus.SelectedValue + "',VillageName='" + ddvillageName.SelectedValue + "',DistrictName='" + dddistrictName.SelectedValue + "',Province='" + ddProvince.SelectedValue + "',");
        SbmstpatColumns.Append("LocalCouncil='" + txtlocalCouncil.Text + "',Address=encryptbykey(key_guid('Key_CTC'),'" + txtaddress.Text + "'),Phone=encryptbykey(key_guid('Key_CTC'),'" + txtphone.Text + "'),");
        SbmstpatColumns.Append("CountryId='" + Session["AppCountryId"] + "', PosId='" + Session["AppPosID"] + "', SatelliteId='" + Session["AppSatelliteId"] + "', ");


        StringBuilder SbContColumns = new StringBuilder();
        StringBuilder SbContValues = new StringBuilder();
        SbContColumns.Append(" Delete  from [DTL_PATIENTCONTACTS] where Ptn_Pk=" + PatientID + " and Visitid=" + VisitID + " and LocationID=" + LocationID + " ");
        SbContColumns.Append(" Insert into [DTL_PATIENTCONTACTS](Ptn_pk,Visitid,LocationId,UserID,CreateDate,EmergContactName,EmergContactRelation,EmergContactPhone,EmergContactAddress,");
        SbContValues.Append("Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),'" + txtemergContactName.Text + "'," + ddEmergContactRelation.SelectedValue + ",'" + txtemergContactPhone.Text + "','" + txtemergContactAddress.Text + "',");

        StringBuilder SbHouseHoldColumns = new StringBuilder();
        StringBuilder SbHouseHoldValues = new StringBuilder();
        SbHouseHoldColumns.Append(" Delete  from [DTL_PATIENTHOUSEHOLDINFO] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
        SbHouseHoldColumns.Append("Insert into [DTL_PATIENTHOUSEHOLDINFO](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbHouseHoldValues.Append("Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SbruralResidenceColumns = new StringBuilder();
        StringBuilder SbruralResidenceValues = new StringBuilder();
        SbruralResidenceColumns.Append(" Delete  from [DTL_RURALRESIDENCE] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
        SbruralResidenceColumns.Append("Insert into [DTL_RURALRESIDENCE](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbruralResidenceValues.Append("Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SburbanresidenceColumns = new StringBuilder();
        StringBuilder SburbanresidenceValues = new StringBuilder();
        SburbanresidenceColumns.Append(" Delete  from [DTL_URBANRESIDENCE] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
        SburbanresidenceColumns.Append("Insert into [DTL_URBANRESIDENCE](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SburbanresidenceValues.Append("Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SbpatienthivprevcareenrollmentColumns = new StringBuilder();
        StringBuilder SbpatienthivprevcareenrollmentValues = new StringBuilder();
        SbpatienthivprevcareenrollmentColumns.Append(" Delete  from [DTL_PATIENTHIVPREVCAREENROLLMENT] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
        SbpatienthivprevcareenrollmentColumns.Append("Insert into [DTL_PATIENTHIVPREVCAREENROLLMENT](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbpatienthivprevcareenrollmentValues.Append("Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SbpatientguarantorColumns = new StringBuilder();
        StringBuilder SbpatientguarantorValues = new StringBuilder();
        SbpatientguarantorColumns.Append(" Delete  from [DTL_PATIENTGUARANTOR] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
        SbpatientguarantorColumns.Append("Insert into [DTL_PATIENTGUARANTOR](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbpatientguarantorValues.Append("Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SbpatientDepositsColumns = new StringBuilder();
        StringBuilder SbpatientDepositsValues = new StringBuilder();
        SbpatientDepositsColumns.Append(" Delete  from [DTL_PATIENTDEPOSITS] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
        SbpatientDepositsColumns.Append("Insert into [DTL_PATIENTDEPOSITS](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbpatientDepositsValues.Append("Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SbpatientInterviewerColumns = new StringBuilder();
        StringBuilder SbpatientInterviewerValues = new StringBuilder();
        SbpatientInterviewerColumns.Append(" Delete  from [DTL_PATIENTINTERVIEWER] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
        SbpatientInterviewerColumns.Append("Insert into [DTL_PATIENTINTERVIEWER](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbpatientInterviewerValues.Append("Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SbCustColumns = new StringBuilder();
        StringBuilder SbCustValues = new StringBuilder();
        string TableName = "DTL_FBCUSTOMFIELD_" + "Patient Registration".Replace(' ', '_');
        SbCustColumns.Append("if exists(select name from sysobjects where type = 'u' and name ='" + TableName + "') begin ");
        SbCustColumns.Append(" Delete  from [" + TableName + "] where Ptn_Pk=" + PatientID + " and Visit_pk=" + VisitID + " and LocationID=" + LocationID + " ");
        SbCustColumns.Append(" Insert into [" + TableName + "](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbCustValues.Append("Values(" + PatientID + "," + VisitID + "," + LocationID + "," + Session["AppUserId"] + ", GetDate(),");
        //////////////////////////////////////////////////
        //For Controls Other than Multiselect
        foreach (DataRow theMainDR in LnkDTUnique.Rows)
        {
            StringBuilder SbColumns = new StringBuilder();
            StringBuilder SbValues = new StringBuilder();


            //if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "MST_PATIENT")
            //{
            //}
            //else 

            //else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTHOUSEHOLDINFO")
            //{ }

            //else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_RURALRESIDENCE")
            //{
            //}

            //else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_URBANRESIDENCE")
            //{

            //}

            //else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTHIVPREVCAREENROLLMENT")
            //{ }

            //else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTGUARANTOR")
            //{
            //}


            //else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTDEPOSITS")
            //{

            //}

            //else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTINTERVIEWER")
            //{

            //}

            //else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_CUSTOMFIELD")
            //{

            //}

            //else
            //{
            if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTCONTACTS")
            {

                SbColumns.Append(" Delete  from [" + theMainDR["PDFTableName"] + "] where Ptn_Pk=" + PatientID + " and VisitID=" + VisitID + " and LocationID=" + LocationID + " ");
                SbColumns.Append(" Insert into [" + theMainDR["PDFTableName"] + "](Ptn_pk,VisitId,LocationId,UserID,Updatedate,");
                SbValues.Append("Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");

            }
            else if (theMainDR["PDFTableName"].ToString() == "ord_patientpharmacyorder")
            {
                SbColumns.Append(" Delete  from [" + theMainDR["PDFTableName"] + "] where Ptn_Pk=" + PatientID + " and VisitID=" + VisitID + " and LocationID=" + LocationID + " ");
                SbColumns.Append(" Insert into [" + theMainDR["PDFTableName"] + "](Ptn_pk,VisitId,LocationId,UserID,Updatedate,");
                SbValues.Append("Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");
            }
            else
            {
                SbColumns.Append(" Delete  from [" + theMainDR["PDFTableName"] + "] where Ptn_Pk=" + PatientID + " and Visit_pk=" + VisitID + " and LocationID=" + LocationID + " ");
                SbColumns.Append(" Insert into [" + theMainDR["PDFTableName"] + "](Ptn_pk,Visit_pk,LocationId,UserID,Updatedate,");
                SbValues.Append("Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");
            }
            //}
            foreach (DataRow theSub1DR in theDT.Rows)
            {
                if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == Convert.ToString(theSub1DR["TableName"]).ToUpper())
                {
                    if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "MST_PATIENT")
                    {
                        if (Convert.ToString(theSub1DR["Value"]) != "")
                        {
                            SbmstpatColumns.Append("[" + theSub1DR["Column"] + "]=");
                            if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "ADDRESS")
                            {
                                //SbmstpatColumns.Append("encryptbykey(key_guid('Key_CTC'),'" + theSub1DR["Value"] + "'),");
                            }
                            else if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "PHONE")
                            {
                                //SbmstpatColumns.Append("encryptbykey(key_guid('Key_CTC'),'" + theSub1DR["Value"] + "'),");
                            }
                            else
                            {
                                SbmstpatColumns.Append("'" + theSub1DR["Value"] + "',");
                            }
                        }
                    }

                    else
                    {
                        if (Convert.ToString(theSub1DR["Value"]) != "")
                        {
                            if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTCONTACTS")
                            {
                                SbContColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbContValues.Append("'" + theSub1DR["Value"] + "',");
                            }
                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTHOUSEHOLDINFO")
                            {
                                SbHouseHoldColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbHouseHoldValues.Append("'" + theSub1DR["Value"] + "',");
                            }

                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_RURALRESIDENCE")
                            {
                                SbruralResidenceColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbruralResidenceValues.Append("'" + theSub1DR["Value"] + "',");
                            }

                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_URBANRESIDENCE")
                            {
                                SburbanresidenceColumns.Append("[" + theSub1DR["Column"] + "],");
                                SburbanresidenceValues.Append("'" + theSub1DR["Value"] + "',");
                            }

                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTHIVPREVCAREENROLLMENT")
                            {
                                SbpatienthivprevcareenrollmentColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbpatienthivprevcareenrollmentValues.Append("'" + theSub1DR["Value"] + "',");
                            }

                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTGUARANTOR")
                            {
                                SbpatientguarantorColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbpatientguarantorValues.Append("'" + theSub1DR["Value"] + "',");
                            }

                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTDEPOSITS")
                            {
                                SbpatientDepositsColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbpatientDepositsValues.Append("'" + theSub1DR["Value"] + "',");
                            }

                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTINTERVIEWER")
                            {
                                SbpatientInterviewerColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbpatientInterviewerValues.Append("'" + theSub1DR["Value"] + "',");

                            }
                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_CUSTOMFIELD")
                            {
                                SbCustColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbCustValues.Append("'" + theSub1DR["Value"] + "',");
                            }
                            else
                            {
                                SbColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbValues.Append("'" + theSub1DR["Value"] + "',");
                            }
                        }
                    }
                }
            }
            if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "MST_PATIENT")
            {
            }
            else
            {
                if (SbColumns.Length > 0)
                {
                    SbColumns.Remove(SbColumns.Length - 1, 1);
                    SbValues.Remove(SbValues.Length - 1, 1);
                    sbUpdate.Append(SbColumns.Append(")"));
                    sbUpdate.Append(SbValues.Append(")"));
                }
            }
        }

        SbmstpatColumns.Append("UserID='" + Session["AppUserId"] + "', UpdateDate=getdate() where ptn_pk='" + PatientID + "' and LocationID='" + Session["AppLocationId"] + "' ");
        sbUpdate.Append(SbmstpatColumns);
        SbmstpatColumns = new StringBuilder();
        SbmstpatColumns.Append("Update [ord_Visit] Set VisitDate='" + txtRegDate.Value + "', UserID='" + Session["AppUserId"] + "', UpdateDate=getdate() where Visit_Id='" + VisitID + "' and Visittype=12");
        sbUpdate.Append(SbmstpatColumns);
        if (SbContColumns.Length > 0)
        {
            SbContColumns.Remove(SbContColumns.Length - 1, 1);
            SbContValues.Remove(SbContValues.Length - 1, 1);
            sbUpdate.Append(SbContColumns.Append(")"));
            sbUpdate.Append(SbContValues.Append(")"));
        }

        if (SbHouseHoldColumns.Length > 0)
        {
            SbHouseHoldColumns.Remove(SbHouseHoldColumns.Length - 1, 1);
            SbHouseHoldValues.Remove(SbHouseHoldValues.Length - 1, 1);
            sbUpdate.Append(SbHouseHoldColumns.Append(")"));
            sbUpdate.Append(SbHouseHoldValues.Append(")"));
        }

        if (SbruralResidenceColumns.Length > 0)
        {
            SbruralResidenceColumns.Remove(SbruralResidenceColumns.Length - 1, 1);
            SbruralResidenceValues.Remove(SbruralResidenceValues.Length - 1, 1);
            sbUpdate.Append(SbruralResidenceColumns.Append(")"));
            sbUpdate.Append(SbruralResidenceValues.Append(")"));
        }

        if (SburbanresidenceColumns.Length > 0)
        {
            SburbanresidenceColumns.Remove(SburbanresidenceColumns.Length - 1, 1);
            SburbanresidenceValues.Remove(SburbanresidenceValues.Length - 1, 1);
            sbUpdate.Append(SburbanresidenceColumns.Append(")"));
            sbUpdate.Append(SburbanresidenceValues.Append(")"));
        }

        if (SbpatienthivprevcareenrollmentColumns.Length > 0)
        {
            SbpatienthivprevcareenrollmentColumns.Remove(SbpatienthivprevcareenrollmentColumns.Length - 1, 1);
            SbpatienthivprevcareenrollmentValues.Remove(SbpatienthivprevcareenrollmentValues.Length - 1, 1);
            sbUpdate.Append(SbpatienthivprevcareenrollmentColumns.Append(")"));
            sbUpdate.Append(SbpatienthivprevcareenrollmentValues.Append(")"));
        }

        if (SbpatientguarantorColumns.Length > 0)
        {
            SbpatientguarantorColumns.Remove(SbpatientguarantorColumns.Length - 1, 1);
            SbpatientguarantorValues.Remove(SbpatientguarantorValues.Length - 1, 1);
            sbUpdate.Append(SbpatientguarantorColumns.Append(")"));
            sbUpdate.Append(SbpatientguarantorValues.Append(")"));
        }

        if (SbpatientDepositsColumns.Length > 0)
        {
            SbpatientDepositsColumns.Remove(SbpatientDepositsColumns.Length - 1, 1);
            SbpatientDepositsValues.Remove(SbpatientDepositsValues.Length - 1, 1);
            sbUpdate.Append(SbpatientDepositsColumns.Append(")"));
            sbUpdate.Append(SbpatientDepositsValues.Append(")"));
        }

        if (SbpatientInterviewerColumns.Length > 0)
        {
            SbpatientInterviewerColumns.Remove(SbpatientInterviewerColumns.Length - 1, 1);
            SbpatientInterviewerValues.Remove(SbpatientInterviewerValues.Length - 1, 1);
            sbUpdate.Append(SbpatientInterviewerColumns.Append(")"));
            sbUpdate.Append(SbpatientInterviewerValues.Append(")"));
        }
        if (SbCustColumns.Length > 0)
        {
            SbCustColumns.Remove(SbCustColumns.Length - 1, 1);
            SbCustValues.Remove(SbCustValues.Length - 1, 1);
            sbUpdate.Append(SbCustColumns.Append(")"));
            sbUpdate.Append(SbCustValues.Append(") end "));
        }
        //For MultiSelect control
        if (theDTMulti != null)
        {
            foreach (DataRow DRMultiSelect in theDTMulti.Rows)
            {
                if (DRMultiSelect["ControlID"].ToString() == "9")
                {
                    StringBuilder DeleteMultiselect = UpdateMultiSelectList(PatientID, FeatureID, VisitID, LocationID, DRMultiSelect["PDFTableName"].ToString(),
                        DRMultiSelect["FieldName"].ToString(), 0, Convert.ToInt32(DRMultiSelect["ControlID"]));
                    sbUpdate.Append(DeleteMultiselect);
                    StringBuilder InsertMultiselect = UpdateMultiSelectList(PatientID, FeatureID, VisitID, LocationID, DRMultiSelect["PDFTableName"].ToString(),
                        DRMultiSelect["FieldName"].ToString(), 1, Convert.ToInt32(DRMultiSelect["ControlID"]));
                    sbUpdate.Append(InsertMultiselect);
                }
            }
        }


        //Generating Query for CondMultiSelect 
        if (theDTConMulti != null)
        {
            foreach (DataRow DRMultiSelect in theDTConMulti.Rows)
            {
                if (DRMultiSelect["ControlID"].ToString() == "9")
                {
                    StringBuilder DeleteMultiselect = UpdateMultiSelectList(PatientID, FeatureID, VisitID, LocationID, DRMultiSelect["PDFTableName"].ToString(),
                        DRMultiSelect["FieldName"].ToString(), 0, Convert.ToInt32(DRMultiSelect["ControlID"]));
                    sbUpdate.Append(DeleteMultiselect);
                    StringBuilder InsertMultiselect = UpdateMultiSelectList(PatientID, FeatureID, VisitID, LocationID, DRMultiSelect["PDFTableName"].ToString(),
                        DRMultiSelect["FieldName"].ToString(), 1, Convert.ToInt32(DRMultiSelect["ControlID"]));
                    sbUpdate.Append(InsertMultiselect);
                }
            }
        }
        //  

        sbUpdate.Append("Select 1[Saved]");
        return sbUpdate;
    }
    private StringBuilder SaveCustomRegistrationData()
    {
        ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameterBCustom);

        DataTable LnkDTUnique = new DataTable();
        StringBuilder sbUpdate = new StringBuilder();
        DataTable theDTNoMulti = new DataTable();
        DataTable theDTMulti = new DataTable();
        DataTable theDTConMulti = new DataTable();

        DataTable theDT = SetControlIDs(PnlDynamicElements);
        theDTNoMulti = ((DataTable)ViewState["NoMulti"]);
        theDTMulti = ((DataTable)ViewState["LnkTable"]);
        theDTConMulti = ((DataTable)ViewState["LnkConTable"]);

        StringBuilder SbInsert = new StringBuilder();
        if (ViewState["NoMulti"] != null)
        {
            LnkDTUnique = theDTNoMulti.DefaultView.ToTable(true, "PDFTableName", "FeatureName").Copy();
        }
        int DOBPrecision = 0;
        if (rbtndobPrecEstimated.Checked == true)
        {
            DOBPrecision = 1;
        }
        else if (rbtndobPrecExact.Checked == true)
        {
            DOBPrecision = 0;
        }
        else
        {
            DOBPrecision = 2;
        }
        ///////////////Added by Naveen///////////////////
        StringBuilder SbmstpatColumns = new StringBuilder();
        StringBuilder SbmstpatValues = new StringBuilder();
        SbmstpatColumns.Append("Insert into [MST_PATIENT](");
        SbmstpatColumns.Append("Status, FirstName, MiddleName, LastName, LocationID, RegistrationDate, Sex, DOB, [ID/PassportNo], DobPrecision, MaritalStatus, VillageName, DistrictName, Province, LocalCouncil, Address, Phone, CountryId,  PosId, SatelliteId, UserID, CreateDate,");
        SbmstpatValues.Append("Values(");
        SbmstpatValues.Append("'0', encryptbykey(key_guid('Key_CTC'),'" + txtfirstName.Text + "'), encryptbykey(key_guid('Key_CTC'),'" + txtmiddleName.Text + "'), encryptbykey(key_guid('Key_CTC'),'" + txtlastName.Text + "')");
        SbmstpatValues.Append(", '" + Session["AppLocationId"] + "', '" + txtRegDate.Value + "', '" + ddgender.SelectedValue + "', '" + TxtDOB.Text + "', '" + txtNationalID.Text + "','" + DOBPrecision + "', '" + ddmaritalStatus.SelectedValue + "', '" + ddvillageName.SelectedValue + "','" + dddistrictName.SelectedValue + "','" + ddProvince.SelectedValue + "',");
        SbmstpatValues.Append("'" + txtlocalCouncil.Text + "',encryptbykey(key_guid('Key_CTC'),'" + txtaddress.Text + "'),encryptbykey(key_guid('Key_CTC'),'" + txtphone.Text + "'),");
        SbmstpatValues.Append("'" + Session["AppCountryId"] + "', '" + Session["AppPosID"] + "', '" + Session["AppSatelliteId"] + "', '" + Session["AppUserId"] + "', getdate(),");



        StringBuilder SbContColumns = new StringBuilder();
        StringBuilder SbContValues = new StringBuilder();
        //SbContColumns.Append("Insert into [DTL_PATIENTCONTACTS](Ptn_pk,Visitid,LocationId,UserID,CreateDate,");
        SbContColumns.Append(" Insert into [DTL_PATIENTCONTACTS](Ptn_pk,Visitid,LocationId,UserID,CreateDate,EmergContactName,EmergContactRelation,EmergContactPhone,EmergContactAddress,");
        SbContValues.Append("Values(IDENT_CURRENT('mst_Patient'),IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),'" + txtemergContactName.Text + "'," + ddEmergContactRelation.SelectedValue + ",'" + txtemergContactPhone.Text + "','" + txtemergContactAddress.Text + "',");


        StringBuilder SbHouseHoldColumns = new StringBuilder();
        StringBuilder SbHouseHoldValues = new StringBuilder();
        SbHouseHoldColumns.Append("Insert into [DTL_PATIENTHOUSEHOLDINFO](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbHouseHoldValues.Append("Values(IDENT_CURRENT('mst_Patient'),IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SbruralResidenceColumns = new StringBuilder();
        StringBuilder SbruralResidenceValues = new StringBuilder();
        SbruralResidenceColumns.Append("Insert into [DTL_RURALRESIDENCE](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbruralResidenceValues.Append("Values(IDENT_CURRENT('mst_Patient'),IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SburbanresidenceColumns = new StringBuilder();
        StringBuilder SburbanresidenceValues = new StringBuilder();
        SburbanresidenceColumns.Append("Insert into [DTL_URBANRESIDENCE](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SburbanresidenceValues.Append("Values(IDENT_CURRENT('mst_Patient'),IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SbpatienthivprevcareenrollmentColumns = new StringBuilder();
        StringBuilder SbpatienthivprevcareenrollmentValues = new StringBuilder();
        SbpatienthivprevcareenrollmentColumns.Append("Insert into [DTL_PATIENTHIVPREVCAREENROLLMENT](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbpatienthivprevcareenrollmentValues.Append("Values(IDENT_CURRENT('mst_Patient'),IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SbpatientguarantorColumns = new StringBuilder();
        StringBuilder SbpatientguarantorValues = new StringBuilder();
        SbpatientguarantorColumns.Append("Insert into [DTL_PATIENTGUARANTOR](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbpatientguarantorValues.Append("Values(IDENT_CURRENT('mst_Patient'),IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SbpatientDepositsColumns = new StringBuilder();
        StringBuilder SbpatientDepositsValues = new StringBuilder();
        SbpatientDepositsColumns.Append("Insert into [DTL_PATIENTDEPOSITS](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbpatientDepositsValues.Append("Values(IDENT_CURRENT('mst_Patient'),IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SbpatientInterviewerColumns = new StringBuilder();
        StringBuilder SbpatientInterviewerValues = new StringBuilder();
        SbpatientInterviewerColumns.Append("Insert into [DTL_PATIENTINTERVIEWER](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbpatientInterviewerValues.Append("Values(IDENT_CURRENT('mst_Patient'),IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

        StringBuilder SbCustColumns = new StringBuilder();
        StringBuilder SbCustValues = new StringBuilder();
        string TableName = "DTL_FBCUSTOMFIELD_" + "Patient Registration".Replace(' ', '_');
        SbCustColumns.Append("if exists(select name from sysobjects where name = '" + TableName + "') begin ");
        SbCustColumns.Append("Insert into [" + TableName + "](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
        SbCustValues.Append("Values(IDENT_CURRENT('mst_Patient'),IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

        /////////////////////////////////////////////////////
        foreach (DataRow theMainDR in LnkDTUnique.Rows)
        {
            StringBuilder SbColumns = new StringBuilder();
            StringBuilder SbValues = new StringBuilder();

            foreach (DataRow theSub1DR in theDT.Rows)
            {
                if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == Convert.ToString(theSub1DR["TableName"]).ToUpper())
                {
                    if (Convert.ToString(theSub1DR["Value"]) != "")
                    {
                        if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "MST_PATIENT")
                        {
                            SbmstpatColumns.Append("[" + theSub1DR["Column"] + "],");
                            if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "ADDRESS")
                            { SbmstpatValues.Append("encryptbykey(key_guid('Key_CTC'),'" + theSub1DR["Value"] + "'),"); }
                            else if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "PHONE")
                            { SbmstpatValues.Append("encryptbykey(key_guid('Key_CTC'),'" + theSub1DR["Value"] + "'),"); }
                            else { SbmstpatValues.Append("'" + theSub1DR["Value"] + "',"); }
                        }
                        else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTCONTACTS")
                        {
                            SbContColumns.Append("[" + theSub1DR["Column"] + "],");
                            if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "ADDRESS")
                            { SbContValues.Append("encryptbykey(key_guid('Key_CTC'),'" + theSub1DR["Value"] + "'),"); }
                            else if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "PHONE")
                            { SbContValues.Append("encryptbykey(key_guid('Key_CTC'),'" + theSub1DR["Value"] + "'),"); }
                            else { SbContValues.Append("'" + theSub1DR["Value"] + "',"); }
                        }

                        else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTHOUSEHOLDINFO")
                        {
                            SbHouseHoldColumns.Append("[" + theSub1DR["Column"] + "],");
                            SbHouseHoldValues.Append("'" + theSub1DR["Value"] + "',");
                        }

                        else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_RURALRESIDENCE")
                        {
                            SbruralResidenceColumns.Append("[" + theSub1DR["Column"] + "],");
                            SbruralResidenceValues.Append("'" + theSub1DR["Value"] + "',");
                        }

                        else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_URBANRESIDENCE")
                        {
                            SburbanresidenceColumns.Append("[" + theSub1DR["Column"] + "],");
                            SburbanresidenceValues.Append("'" + theSub1DR["Value"] + "',");
                        }

                        else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTHIVPREVCAREENROLLMENT")
                        {
                            SbpatienthivprevcareenrollmentColumns.Append("[" + theSub1DR["Column"] + "],");
                            SbpatienthivprevcareenrollmentValues.Append("'" + theSub1DR["Value"] + "',");
                        }

                        else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTGUARANTOR")
                        {
                            SbpatientguarantorColumns.Append("[" + theSub1DR["Column"] + "],");
                            SbpatientguarantorValues.Append("'" + theSub1DR["Value"] + "',");
                        }

                        else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTDEPOSITS")
                        {
                            SbpatientDepositsColumns.Append("[" + theSub1DR["Column"] + "],");
                            SbpatientDepositsValues.Append("'" + theSub1DR["Value"] + "',");
                        }

                        else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTINTERVIEWER")
                        {
                            SbpatientInterviewerColumns.Append("[" + theSub1DR["Column"] + "],");
                            SbpatientInterviewerValues.Append("'" + theSub1DR["Value"] + "',");

                        }
                        else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_CUSTOMFIELD")
                        {
                            SbCustColumns.Append("[" + theSub1DR["Column"] + "],");
                            if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "ADDRESS")
                            { SbCustValues.Append("encryptbykey(key_guid('Key_CTC'),'" + theSub1DR["Value"] + "'),"); }
                            else if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "PHONE")
                            { SbCustValues.Append("encryptbykey(key_guid('Key_CTC'),'" + theSub1DR["Value"] + "'),"); }
                            else { SbCustValues.Append("'" + theSub1DR["Value"] + "',"); }
                        }
                    }
                }
            }
        }
        if (SbmstpatColumns.Length > 0)
        {
            SbmstpatColumns.Remove(SbmstpatColumns.Length - 1, 1);
            SbmstpatValues.Remove(SbmstpatValues.Length - 1, 1);
            SbInsert.Append(SbmstpatColumns.Append(")"));
            SbInsert.Append(SbmstpatValues.Append(")"));
            SbmstpatColumns = new StringBuilder();
            SbmstpatValues = new StringBuilder();
            SbmstpatColumns.Append("Insert into ord_Visit(Ptn_Pk,LocationID,VisitDate,VisitType,UserID,CreateDate");
            SbmstpatValues.Append("values (IDENT_CURRENT('mst_Patient'),'" + Session["AppLocationId"] + "', '" + txtRegDate.Value + "', 12, '" + Session["AppUserId"] + "', getdate()");
            SbInsert.Append(SbmstpatColumns.Append(")"));
            SbInsert.Append(SbmstpatValues.Append(")"));
        }

        if (SbContColumns.Length > 0)
        {
            SbContColumns.Remove(SbContColumns.Length - 1, 1);
            SbContValues.Remove(SbContValues.Length - 1, 1);
            SbInsert.Append(SbContColumns.Append(")"));
            SbInsert.Append(SbContValues.Append(")"));
        }

        if (SbHouseHoldColumns.Length > 0)
        {
            SbHouseHoldColumns.Remove(SbHouseHoldColumns.Length - 1, 1);
            SbHouseHoldValues.Remove(SbHouseHoldValues.Length - 1, 1);
            SbInsert.Append(SbHouseHoldColumns.Append(")"));
            SbInsert.Append(SbHouseHoldValues.Append(")"));
        }

        if (SbruralResidenceColumns.Length > 0)
        {
            SbruralResidenceColumns.Remove(SbruralResidenceColumns.Length - 1, 1);
            SbruralResidenceValues.Remove(SbruralResidenceValues.Length - 1, 1);
            SbInsert.Append(SbruralResidenceColumns.Append(")"));
            SbInsert.Append(SbruralResidenceValues.Append(")"));
        }

        if (SburbanresidenceColumns.Length > 0)
        {
            SburbanresidenceColumns.Remove(SburbanresidenceColumns.Length - 1, 1);
            SburbanresidenceValues.Remove(SburbanresidenceValues.Length - 1, 1);
            SbInsert.Append(SburbanresidenceColumns.Append(")"));
            SbInsert.Append(SburbanresidenceValues.Append(")"));
        }

        if (SbpatienthivprevcareenrollmentColumns.Length > 0)
        {
            SbpatienthivprevcareenrollmentColumns.Remove(SbpatienthivprevcareenrollmentColumns.Length - 1, 1);
            SbpatienthivprevcareenrollmentValues.Remove(SbpatienthivprevcareenrollmentValues.Length - 1, 1);
            SbInsert.Append(SbpatienthivprevcareenrollmentColumns.Append(")"));
            SbInsert.Append(SbpatienthivprevcareenrollmentValues.Append(")"));
        }

        if (SbpatientguarantorColumns.Length > 0)
        {
            SbpatientguarantorColumns.Remove(SbpatientguarantorColumns.Length - 1, 1);
            SbpatientguarantorValues.Remove(SbpatientguarantorValues.Length - 1, 1);
            SbInsert.Append(SbpatientguarantorColumns.Append(")"));
            SbInsert.Append(SbpatientguarantorValues.Append(")"));
        }

        if (SbpatientDepositsColumns.Length > 0)
        {
            SbpatientDepositsColumns.Remove(SbpatientDepositsColumns.Length - 1, 1);
            SbpatientDepositsValues.Remove(SbpatientDepositsValues.Length - 1, 1);
            SbInsert.Append(SbpatientDepositsColumns.Append(")"));
            SbInsert.Append(SbpatientDepositsValues.Append(")"));
        }

        if (SbpatientInterviewerColumns.Length > 0)
        {
            SbpatientInterviewerColumns.Remove(SbpatientInterviewerColumns.Length - 1, 1);
            SbpatientInterviewerValues.Remove(SbpatientInterviewerValues.Length - 1, 1);
            SbInsert.Append(SbpatientInterviewerColumns.Append(")"));
            SbInsert.Append(SbpatientInterviewerValues.Append(")"));
        }


        if (SbCustColumns.Length > 0)
        {
            SbCustColumns.Remove(SbCustColumns.Length - 1, 1);
            SbCustValues.Remove(SbCustValues.Length - 1, 1);
            SbInsert.Append(SbCustColumns.Append(")"));
            SbInsert.Append(SbCustValues.Append(") end "));
        }

        SbInsert.Append("update mst_patient set IQNumber = 'IQ-'+convert(varchar,Replicate('0',20-len(x.[ptnIdentifier]))) +x.[ptnIdentifier]  from ");
        SbInsert.Append("(select UPPER(substring(convert(varchar(50),decryptbykey(firstname)),1,1))+UPPER(substring(convert(varchar(50),decryptbykey(lastname)),1,1))+");
        SbInsert.Append("convert(varchar,dob,112)+convert(varchar,locationid)+Convert(varchar(10),ptn_pk) [ptnIdentifier] from mst_patient ");
        SbInsert.Append("where ptn_pk = ident_current('mst_patient'))x where ptn_pk= ident_current('mst_patient') ");
        SbInsert.Append("Select ident_current('mst_patient')[ptn_pk], a.IQNumber, b.Visit_ID from mst_patient a inner join Ord_visit b on a.ptn_pk=b.ptn_pk where a.Ptn_Pk=ident_current('mst_patient') and b.visittype=12");
        //Generating Query for MultiSelect 
        if (theDTMulti != null)
        {
            foreach (DataRow DRMultiSelect in theDTMulti.Rows)
            {
                if (DRMultiSelect["ControlID"].ToString() == "9")
                {
                    StringBuilder InsertMultiselect = InsertMultiSelectList("ident_current('mst_patient')", DRMultiSelect["FieldName"].ToString(), Convert.ToInt32(DRMultiSelect["FeatureID"].ToString()),
                        DRMultiSelect["PDFTableName"].ToString(), Convert.ToInt32(DRMultiSelect["ControlID"]), Convert.ToInt32(DRMultiSelect["FieldId"]));
                    if (SbInsert[0].ToString().Contains(DRMultiSelect["PDFTableName"].ToString()) == false)
                        SbInsert.Append(InsertMultiselect);
                }

            }
        }
        //  

        //Generating Query for CondMultiSelect 
        if (theDTConMulti != null)
        {
            foreach (DataRow DRMultiSelect in theDTConMulti.Rows)
            {
                if (DRMultiSelect["ControlID"].ToString() == "9")
                {
                    StringBuilder InsertMultiselect = InsertMultiSelectList("ident_current('mst_patient')", DRMultiSelect["FieldName"].ToString(), Convert.ToInt32(DRMultiSelect["FeatureID"].ToString()),
                        DRMultiSelect["PDFTableName"].ToString(), Convert.ToInt32(DRMultiSelect["ControlID"]), Convert.ToInt32(DRMultiSelect["FieldId"]));
                    if (SbInsert[0].ToString().Contains(DRMultiSelect["PDFTableName"].ToString()) == false)
                        SbInsert.Append(InsertMultiselect);
                }

            }
        }
        //  
        return SbInsert;

    }

    void SelectedProvinceItemTypeChanged(object sender, EventArgs e)
    {
        if (ddProvince.SelectedValue != string.Empty && int.Parse(ddProvince.SelectedValue) > 0)
        {
            BindFunctions BindManager = new BindFunctions();
            int provinceID = int.Parse(ddProvince.SelectedValue);
            ICommonData oCommonData = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData,BusinessProcess.Service");
            DataTable theDT = (DataTable)oCommonData.getAllDistrict();
            DataView theDV = theDT.DefaultView;
            theDV.RowFilter = "ProvinceID = " + provinceID + "";
            if (theDV != null && theDV.Table.Rows.Count > 0)
            {
                BindManager.BindCombo(dddistrictName, theDV.ToTable(), "Name", "ID");
                theDT.Clear();
            }
        }
    }



    private void Binddropdown()
    {
        try
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataView theDV = new DataView();
            DataTable theDT = new DataTable();

            if ((Session["PatientId"] == null) || (Convert.ToInt32(Session["PatientId"]) == 0))
            {
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=4";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddgender, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                Session["SystemId"] = ConfigurationManager.AppSettings["SystemId"].ToString();
                theDV.RowFilter = "CodeID=12 and SystemID=" + Session["SystemId"] + " and DeleteFlag=0";
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddmaritalStatus, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }
            else
            {
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=4";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddgender, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                Session["SystemId"] = ConfigurationManager.AppSettings["SystemId"].ToString();

                theDV.RowFilter = "CodeID=12 and SystemID=" + Session["SystemId"] + "";
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddmaritalStatus, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();

            }

            ICommonData oCommonData = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData,BusinessProcess.Service");
            theDT = (DataTable)oCommonData.getAllCountries();
            if (theDT != null && theDT.Rows.Count > 0)
            {
                BindManager.BindCombo(ddCountry, theDT, "CountryName", "CountryID");
                ddCountry.SelectedValue = Session["AppCountryId"].ToString();
                //ddCountry.Enabled = false;
                theDV.Dispose();
                theDT.Clear();
            }

            theDT = (DataTable)oCommonData.getAllProvince();
            theDV = theDT.DefaultView;
            theDV.RowFilter = "CountryID = " + Session["AppCountryId"].ToString() + "";
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            if (theDT != null && theDT.Rows.Count > 0)
            {
                BindManager.BindCombo(ddProvince, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }

            theDT = (DataTable)oCommonData.getAllDistrict();
            theDV = theDT.DefaultView;
            theDV.RowFilter = "SystemID = " + Session["SystemId"].ToString() + "";
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            if (theDT != null && theDT.Rows.Count > 0)
            {
                BindManager.BindCombo(dddistrictName, theDT, "Name", "ID");
                theDT.Clear();
            }

            theDT = (DataTable)oCommonData.getAllVillages();
            theDV = theDT.DefaultView;
            theDV.RowFilter = "SystemID = " + Session["SystemId"].ToString() + "";
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            if (theDT != null && theDT.Rows.Count > 0)
            {
                BindManager.BindCombo(ddvillageName, theDT, "Name", "ID");
                theDT.Clear();
            }

            theDV = new DataView(theDSXML.Tables["Mst_RelationshipType"]);
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(ddEmergContactRelation, theDT, "Name", "ID");
            theDV.Dispose();
            theDT.Clear();

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

    private DataTable SetControlIDs(Control theControl)
    {
        DataTable TempDT = new DataTable();

        DataColumn Column = new DataColumn("Column");
        Column.DataType = System.Type.GetType("System.String");
        TempDT.Columns.Add(Column);

        DataColumn Control = new DataColumn("FieldID");
        Control.DataType = System.Type.GetType("System.String");
        TempDT.Columns.Add(Control);

        DataColumn Value = new DataColumn("Value");
        Value.DataType = System.Type.GetType("System.String");
        TempDT.Columns.Add(Value);

        DataColumn TableName = new DataColumn("TableName");
        TableName.DataType = System.Type.GetType("System.String");
        TempDT.Columns.Add(TableName);

        DataRow DRTemp;
        DRTemp = TempDT.NewRow();
        ///////////////Sanjay////////////////////////
        ConFieldEnableDisable(PnlDynamicElements);
        ////////////////////////////////////////////
        foreach (Control x in theControl.Controls)
        {
            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
            {

                DRTemp = TempDT.NewRow();
                string[] str = ((TextBox)x).ID.Split('-');
                if (str[1] != "Phone" && str[1] != "LocalCouncil" && str[1] != "Address" && str[1] != "EmergContactPhone" && str[1] != "EmergContactName" && str[1] != "EmergContactAddress")
                {
                    if (str[1] != string.Empty)
                    {
                        DRTemp["Column"] = str[1];
                        if (((TextBox)x).Enabled == true)
                            DRTemp["Value"] = ((TextBox)x).Text;
                        else
                            DRTemp["Value"] = "";
                        DRTemp["TableName"] = str[2];
                        DRTemp["FieldID"] = str[3];
                        TempDT.Rows.Add(DRTemp);
                    }
                }

            }
            if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
            {

                DRTemp = TempDT.NewRow();
                string[] str = ((HtmlInputRadioButton)x).ID.Split('-');
                if (((HtmlInputRadioButton)x).ID == "RADIO1-" + str[1] + "-" + str[2] + "-" + str[3])
                {
                    if (((HtmlInputRadioButton)x).Checked == true)
                    {
                        DRTemp["Column"] = str[1];
                        if (((HtmlInputRadioButton)x).Visible == true)
                            DRTemp["Value"] = "1";
                        else
                            DRTemp["Value"] = "";
                    }
                }
                else if (((HtmlInputRadioButton)x).ID == "RADIO2-" + str[1] + "-" + str[2] + "-" + str[3])
                {
                    if (((HtmlInputRadioButton)x).Checked == true)
                    {
                        DRTemp["Column"] = str[1];
                        if (((HtmlInputRadioButton)x).Visible == true)
                            DRTemp["Value"] = "0";
                        else
                            DRTemp["Value"] = "";
                    }

                }

                DRTemp["TableName"] = str[2];
                DRTemp["FieldID"] = str[3];
                TempDT.Rows.Add(DRTemp);
            }
            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
            {
                DRTemp = TempDT.NewRow();
                string[] str = ((DropDownList)x).ID.Split('-');
                if (str[1] != "EmergContactRelation" && str[1] != "VillageName" && str[1] != "DistrictName" && str[1] != "Province" && str[1] != "CountryId")
                {
                    DRTemp["Column"] = str[1];

                    if (((DropDownList)x).Enabled == true)
                        DRTemp["Value"] = ((DropDownList)x).SelectedValue;
                    else
                        //DRTemp["Value"] = "0";
                        DRTemp["Value"] = "";
                    DRTemp["TableName"] = str[2];
                    DRTemp["FieldID"] = str[3];
                    TempDT.Rows.Add(DRTemp);
                }
            }

            if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
            {
                DRTemp = TempDT.NewRow();
                string[] str = ((HtmlInputCheckBox)x).ID.Split('-');
                DRTemp["Column"] = str[1];
                if (((HtmlInputCheckBox)x).Visible == true)
                {
                    if (((HtmlInputCheckBox)x).Checked == true)
                    {
                        DRTemp["Value"] = 1;
                    }
                    else
                    {
                        DRTemp["Value"] = 0;
                    }
                }
                else
                {
                    DRTemp["Value"] = "";
                }
                DRTemp["TableName"] = str[2];
                DRTemp["FieldID"] = str[3];
                TempDT.Rows.Add(DRTemp);
            }
        }
        return TempDT;
    }

    private Boolean FieldValidation()
    {
        IIQCareSystem IQCareSecurity;
        IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        IQCareUtils theUtils = new IQCareUtils();
        if (txtfirstName.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "First Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtfirstName.Focus();
            return false;
        }
        else if (txtlastName.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Last Name";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtlastName.Focus();
            return false;
        }
        else if (txtRegDate.Value.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Registration Date";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtlastName.Focus();
            return false;
        }
        DateTime theEnrolDate = Convert.ToDateTime(theUtils.MakeDate(txtRegDate.Value));
        if (theEnrolDate > theCurrentDate)
        {
            IQCareMsgBox.Show("EnrolDate", this);
            return false;
        }
        if (ddgender.SelectedValue.Trim() == "0")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Sex";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            ddgender.Focus();
            return false;
        }
        if (TxtDOB.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "DOB";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            TxtDOB.Focus();
            return false;
        }

        if (txtNationalID.Text.Trim() != string.Empty)
        {
            int len = txtNationalID.Text.Length;
            if (len != 8)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "National ID";
                IQCareMsgBox.Show("Length", theBuilder, this);
                txtNationalID.Focus();
                return false;
            }
        }
        DateTime theDOBDate = Convert.ToDateTime(theUtils.MakeDate(TxtDOB.Text));
        if (theDOBDate > theCurrentDate)
        {
            IQCareMsgBox.Show("DOBDate", this);
            TxtDOB.Focus();
            return false;
        }
        if (theDOBDate > theEnrolDate)
        {
            IQCareMsgBox.Show("DOB_EnrolDate", this);
            return false;
        }
        if (Convert.ToInt32(Session["PatientId"]) > 0 && ViewState["ARTStartDate"] != null)
        {
            DateTime theARTRegDate = Convert.ToDateTime(ViewState["ARTStartDate"].ToString());
            if (theEnrolDate > theARTRegDate)
            {
                IQCareMsgBox.Show("ARTRegDate", this);
                return false;
            }
        }

        //PMTCT Business rule
        //

        if (Request.Form[txtageCurrentYears.UniqueID].ToString().Length == 0 || Request.Form[txtageCurrentMonths.UniqueID].ToString().Length == 0)
        {
            CalculateAge_YearMonth(Convert.ToDateTime(TxtDOB.Text.Trim()));
        }
        if (Convert.ToInt32(Session["TechnicalAreaId"]) == 1)
        {
            //Change By Rahmat 06-Feb-2018
            // Male above than 2 yrs
            if ((Convert.ToInt32(txtageCurrentYears.Text.Trim() == "" ? "0" : txtageCurrentYears.Text.Trim()) > 2) && (ddgender.SelectedValue == "16"))
            {
                IQCareMsgBox.Show("PMTCTMaleRegister", this);
                return false;
            }
            // Child above than 2 yrs
            else if ((Convert.ToInt32(txtageCurrentYears.Text.Trim() == "" ? "0" : txtageCurrentYears.Text.Trim()) == 2) && (Convert.ToInt32(txtageCurrentMonths.Text.Trim() == "" ? "0" : txtageCurrentMonths.Text.Trim()) != 0))
            {
                IQCareMsgBox.Show("PMTCTMaleRegister", this);
                return false;
            }
            //// Male above than 2 yrs
            //if ((Convert.ToInt32(Request.Form[txtageCurrentYears.UniqueID]) > 2) && (ddgender.SelectedValue == "16"))
            //{
            //    IQCareMsgBox.Show("PMTCTMaleRegister", this);
            //    return false;
            //}
            //// Child above than 2 yrs
            //else if ((Convert.ToInt32(Request.Form[txtageCurrentYears.UniqueID]) == 2) && (Convert.ToInt32(Request.Form[txtageCurrentMonths.UniqueID]) != 0))
            //{
            //    IQCareMsgBox.Show("PMTCTMaleRegister", this);
            //    return false;
            //}
        }

        return true;
    }
    private void CalculateAge_YearMonth(DateTime Dob)
    {
        DateTime Now = DateTime.Now;
        int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
        DateTime PastYearDate = Dob.AddYears(Years);
        int Months = 0;
        for (int i = 1; i <= 12; i++)
        {
            if (PastYearDate.AddMonths(i) == Now)
            {
                Months = i;
                break;
            }
            else if (PastYearDate.AddMonths(i) >= Now)
            {
                Months = i - 1;
                break;
            }
        }
        txtageCurrentYears.Text = Years.ToString();
        txtageCurrentMonths.Text = Months.ToString();
    }
    private String ValidationMessage()
    {
        IIQCareSystem IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        string strmsg = "Following values are required to complete this:</br>";
        DataTable theDT = (DataTable)ViewState["BusRule"];
        String Radio1 = "", Radio2 = "", MultiSelectName = "", MultiSelectLabel = "";
        int TotCount = 0, FalseCount = 0;
        try
        {
            foreach (Control x in PnlDynamicElements.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    string[] Field = ((TextBox)x).ID.Split('-');
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        if ((((TextBox)x).ID.Contains("=") == true) && (((TextBox)x).Enabled == true))
                        {
                            string[] Field10 = ((TextBox)x).ID.Replace('=', '-').Split('-');
                            if (Field10[1] == Convert.ToString(theDR["FieldName"]) && Field10[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field10[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                            {
                                if (((TextBox)x).Text == "")
                                {
                                    strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                    strmsg = strmsg + "</br>";
                                }
                            }

                        }

                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                        {
                            if ((((TextBox)x).Text == "") && (((TextBox)x).Enabled == true))
                            {
                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                strmsg = strmsg + "</br>";
                            }
                        }
                        //Date Greater than Today's Date
                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "7")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);
                                if (GetDate <= theCurrentDate)
                                {
                                    strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                    strmsg = strmsg + "</br>";
                                }
                            }
                        }
                        //Date Less than Today's Date
                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "8")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);

                                if (GetDate >= theCurrentDate)
                                {
                                    strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                    strmsg = strmsg + "</br>";
                                }
                            }
                        }
                        //Date greater than Date of Birth
                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "9")
                        {
                            DateTime GetDOB = Convert.ToDateTime(TxtDOB.Text);
                            if (((TextBox)x).Text != "")
                            {
                                DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);
                                if (GetDate <= GetDOB)
                                {
                                    strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                    strmsg = strmsg + "</br>";
                                }
                            }
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                {
                    string[] Field = ((HtmlInputRadioButton)x).ID.Split('-');
                    if (Field[0] == "RADIO1" && ((HtmlInputRadioButton)x).Checked == false)
                    {
                        Radio1 = Field[3];
                    }
                    if (Field[0] == "RADIO2" && ((HtmlInputRadioButton)x).Checked == false)
                    {
                        Radio2 = Field[3];
                    }

                    foreach (DataRow theDR in theDT.Rows)
                    {

                        if (Radio1 == Field[3] && Radio2 == Field[3])
                        {
                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                            {
                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                strmsg = strmsg + "</br>";
                            }

                        }

                    }
                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    string[] Field = ((DropDownList)x).ID.Split('-');
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1" && Field[0].ToString() != "SELECTLISTAuto")
                        {
                            if ((((DropDownList)x).SelectedValue == "0") && (Field[0].ToString() != "SELECTLISTAuto") && ((DropDownList)x).Enabled == true)
                            {
                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                strmsg = strmsg + "</br>";
                            }
                        }
                    }
                }

                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                {
                    string[] Field = ((HtmlInputCheckBox)x).ID.Split('-');
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                        {
                            if (((HtmlInputCheckBox)x).Checked == false)
                            {
                                strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                strmsg = strmsg + "</br>";
                            }
                        }
                    }

                }

            }

            //MultiSelect Validation

            foreach (Control y in PnlDynamicElements.Controls)
            {
                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    foreach (Control z in y.Controls)
                    {

                        if (z.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                        {
                            TotCount++;
                            if (((CheckBox)z).Checked == false)
                            {
                                FalseCount++;

                            }
                        }
                    }
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        if (Convert.ToString(theDR["ControlId"]) == "9" && ((Panel)y).ID.Substring(4, (((Panel)y).ID.Length - 4)) == Convert.ToString(theDR["FieldID"]) && Convert.ToInt32(theDR["BusRuleId"]) < 13)
                        {
                            MultiSelectName = Convert.ToString(theDR["Name"]);
                            MultiSelectLabel = Convert.ToString(theDR["FieldLabel"]);
                            if (TotCount == FalseCount)
                            {
                                strmsg += MultiSelectLabel + " is " + MultiSelectName;
                                strmsg = strmsg + "</br>";
                            }
                        }
                    }

                    TotCount = 0; FalseCount = 0;
                    MultiSelectName = ""; MultiSelectLabel = "";
                }
            }

        }

        catch (Exception err)
        {

            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
        finally { }

        return strmsg;
    }
    protected void btncontinue_Click(object sender, EventArgs e)
    {
        try
        {
            if (FieldValidation() == false)
            {
                return;
            }
            string msg = ValidationMessage();
            if (msg.Length > 52)
            {
                MsgBuilder theBuilder1 = new MsgBuilder();
                theBuilder1.DataElements["MessageText"] = msg;
                IQCareMsgBox.Show("#C1", theBuilder1, this);
                return;
            }

            IPatientRegistration IPatientFormMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
            if (PatientID == 0)
            {
                HashTableParameter();
                Session["htPtnRegParameter"] = htParameters;
                StringBuilder Add = SaveCustomRegistrationData();
                Session["CustomRegistration"] = Add;
                SaveCancel();
            }
            else if (PatientID > 0)
            {
                StringBuilder Edit = UpdateCustomRegistrationData(PatientID, VisitID, LocationID);
                DataSet Update = IPatientFormMgr.Common_GetSaveUpdateforCustomRegistrion(Edit.ToString());
                if (Update.Tables[0].Rows.Count > 0)
                {
                    UpdateCancel();
                }
            }

        }
        catch (Exception)
        {

            throw;
        }

    }

    private void SaveCancel()
    {
        IQCareMsgBox.NotifyAction("This Registration will be redirected to Service. Do you want to close?", "Patient Registration Form", false, this, "window.location.href='./frmAddTechnicalArea.aspx?mod=" + Session["TechnicalAreaId"] + "';\n");
        //string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        //script += "var ans;\n";
        //script += "ans=window.confirm('This Registration will be redirected to Service. Do you want to close?');\n";
        //script += "if (ans==true)\n";
        //script += "{\n";
        //script += "window.location.href='./frmAddTechnicalArea.aspx?mod=" + Session["TechnicalAreaId"] + "';\n";
        //script += "}\n";
        //script += "</script>\n";
        //RegisterStartupScript("confirm", script);
    }

    private void UpdateCancel()
    {
        IQCareMsgBox.NotifyAction("'Registration Form Update Successfully. Do you want to close?", "Patient Registration Form", false, this, "window.location.href='./frmAddTechnicalArea.aspx?mod=" + Session["TechnicalAreaId"] + "';\n");
        //string script = "<script language = 'javascript' defer ='defer' id = 'confirm2'>\n";
        //script += "var ans;\n";
        //script += "ans=window.confirm('Registration Form Update Successfully. Do you want to close?');\n";
        //script += "if (ans==true)\n";
        //script += "{\n";
        //// script += "window.location.href='./ClinicalForms/frmPatient_Home.aspx';\n";
        //script += "window.location.href='./frmAddTechnicalArea.aspx?mod=" + Session["TechnicalAreaId"] + "';\n";
        //script += "}\n";
        //script += "</script>\n";
        //RegisterStartupScript("confirm2", script);
    }

    public void HtmlRadioButtonSelect(object sender)
    {
        HtmlInputRadioButton theButton = ((HtmlInputRadioButton)sender);
        string[] theControlId = theButton.ID.Split('-');
        DataSet theDS = (DataSet)Session["AllData"];
        int theValue = 0;
        if (theButton.Value == "Yes" && theButton.Checked == true)
            theValue = 1;
        else if (theButton.Value == "Yes" && theButton.Checked == false)
            theValue = 0;

        if (theButton.Value == "No" && theButton.Checked == true)
            theValue = 2;
        else if (theButton.Value == "No" && theButton.Checked == false)
            theValue = 0;

        foreach (DataRow theDR in theDS.Tables[6].Rows)
        {
            foreach (Control x in PnlDynamicElements.Controls)
            {
                if (x.ID != null)
                {
                    string[] theIdent = x.ID.Split('-');
                    if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                        {
                            ((TextBox)x).Enabled = true;
                            //ApplyBusinessRules(x, "1", true);
                            //ApplyBusinessRules(x, "2", true);
                            //ApplyBusinessRules(x, "3", true);
                        }
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                        {
                            ((TextBox)x).Enabled = false;
                            ((TextBox)x).Text = "";
                        }
                        if ((theIdent[0] == "TXTDTAuto") || (theIdent[0] == "TXTMultiAuto") || (theIdent[0] == "TXTAuto") || (theIdent[0] == "TXTNUMAuto"))
                        {
                            ((TextBox)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                        {
                            ((DropDownList)x).Enabled = true;
                            //ApplyBusinessRules(x, "4", true);
                        }
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                        {
                            ((DropDownList)x).Enabled = false;
                            ((DropDownList)x).SelectedValue = "0";
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl_" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                        {
                            ((Panel)x).Enabled = true;
                            //ApplyBusinessRules(x, "9", true);
                        }
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                        {
                            ((Panel)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                        {
                            ((Image)x).Visible = true;
                            //ApplyBusinessRules(x, "5", true);
                        }
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                            ((Image)x).Visible = false;
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                        {
                            ((HtmlInputRadioButton)x).Visible = true;
                            //ApplyBusinessRules(x, "6", true);
                        }
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                            ((HtmlInputRadioButton)x).Visible = false;
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                            ((HtmlInputCheckBox)x).Visible = true;
                        else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                            ((HtmlInputCheckBox)x).Visible = false;
                    }
                }
            }
        }


    }

    public void HtmlCheckBoxSelect(object theObj)
    {
        CheckBox theButton = ((CheckBox)theObj);
        string[] theControlId = theButton.ID.ToString().Split('-');
        DataSet theDS = (DataSet)Session["AllData"];
        int theValue = 0;
        if (theButton.Checked == true)
            theValue = 1;
        else
            theValue = 0;
        foreach (DataRow theDR in theDS.Tables[6].Rows)
        {
            foreach (Control x in PnlDynamicElements.Controls)
            {
                if (x.ID != null)
                {
                    string[] theIdent = x.ID.Split('-');
                    if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                        {
                            ((TextBox)x).Enabled = true;
                            //ApplyBusinessRules(x, "1", true);
                            //ApplyBusinessRules(x, "2", true);
                            //ApplyBusinessRules(x, "3", true);

                        }
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                        {
                            ((TextBox)x).Enabled = false;
                            ((TextBox)x).Text = "";
                        }
                        if ((theIdent[0] == "TXTDTAuto") || (theIdent[0] == "TXTMultiAuto") || (theIdent[0] == "TXTAuto") || (theIdent[0] == "TXTNUMAuto"))
                        {
                            ((TextBox)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                        {
                            ((DropDownList)x).Enabled = true;
                            //ApplyBusinessRules(x, "4", true);

                        }
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                        {
                            ((DropDownList)x).Enabled = false;
                            ((DropDownList)x).SelectedValue = "0";
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl_" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                        {
                            ((Panel)x).Enabled = true;
                            //ApplyBusinessRules(x, "9", true);

                        }
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                        {
                            ((Panel)x).Enabled = false;
                        }
                    }

                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                        {
                            ((Image)x).Visible = true;
                            //ApplyBusinessRules(x, "5", true);

                        }
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                            ((Image)x).Visible = false;
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                        {
                            ((HtmlInputRadioButton)x).Visible = true;
                            //ApplyBusinessRules(x, "6", true);

                        }
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                            ((HtmlInputRadioButton)x).Visible = false;
                    }

                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                    {
                        if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                            ((HtmlInputCheckBox)x).Visible = true;
                        else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                            ((HtmlInputCheckBox)x).Visible = false;
                    }
                }
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //VY changed 2014-10-07 to go to facility home when closed
        Response.Redirect("~/frmFacilityHome.aspx");
        /* if (txtIQCareRef.Text == "")
         {
             Response.Redirect("~/frmFindAddPatient.aspx");
         }
         else
             Response.Redirect("frmAddTechnicalArea.aspx");*/
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string EnableControlAge(string strhidden, string age)
    {
        string strreturn = string.Empty;
        //try
        //{
        string[] ArrCtlId = strhidden.Split(',');
        DataTable theDT = (DataTable)Session["SessionBusRule"];
        foreach (DataRow DR in theDT.Rows)
        {
            for (int i = 0; i < ArrCtlId.Length; i++)
            {
                string[] a = ArrCtlId[i].Split('-');
                if (a[3].ToString() == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "16")
                {
                    if ((DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                    {
                        if (Convert.ToDecimal(age) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(age) <= Convert.ToDecimal(DR["Value1"]))
                        {
                            strreturn = ArrCtlId[i].ToString();
                        }

                    }
                }
            }

        }
        //}
        //catch
        //{

        //}
        //finally
        //{
        //}
        return strreturn;
    }
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    protected void btncalculate_DOB_Click(object sender, EventArgs e)
    {
        if (txtageCurrentYears.Text.Trim() == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "Age (Years)";
            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
            txtfirstName.Focus();
            return;
        }
        if (txtageCurrentMonths.Text != "")
        {
            if ((Convert.ToInt32(txtageCurrentMonths.Text) < 0) || (Convert.ToInt32(txtageCurrentMonths.Text) > 11))
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Age (Month)";
                IQCareMsgBox.Show("AgeMonthRange", theMsg, this);
                return;
            }
        }

        int age = 0;
        int months = 0;
        DateTime currentdate;
        age = Convert.ToInt32(txtageCurrentYears.Text);
        if (txtageCurrentMonths.Text != "")
        {
            currentdate = Convert.ToDateTime(Convert.ToDateTime(txtSysDate.Text).Month + "-01-" + Convert.ToDateTime(txtSysDate.Text).Year);
        }
        else
            currentdate = Convert.ToDateTime("06-15-" + Convert.ToDateTime(txtSysDate.Text).Year);

        DateTime birthdate = currentdate.AddYears(age * -1);
        if (txtageCurrentMonths.Text != "")
        {
            months = Convert.ToInt32(txtageCurrentMonths.Text);
            birthdate = birthdate.AddMonths(months * -1);
        }

        TxtDOB.Text = ((DateTime)birthdate).ToString(Session["AppDateFormat"].ToString());
        if (TxtDOB.Text != "")
        {
            rbtndobPrecEstimated.Checked = true;
        }
    }
}

