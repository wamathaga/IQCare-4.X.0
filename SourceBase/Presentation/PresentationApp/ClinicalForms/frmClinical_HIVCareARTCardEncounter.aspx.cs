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
using System.Collections.Generic;

public partial class frmClinical_HIVCareARTCardEncounter : LogPage
{
    DataTable DTCheckedIds;
    AuthenticationManager Authentication;
    IHivCareARTEncounter HivCareARTEncounterInterface;
    DataSet tempDataSet;
    DataTable tempDataTable;
    DataView tempDataView;
    Hashtable hashTable;
    DataSet dataSetForSaving;
    DataTable theDTNewOisProblem, theDTpotentialSideEffect, theDTReferTo, theDTFamilyPlanning;
    int patientID;
    int locationID;
    string gender;
    int ageInYear;
    int ageInMonth;
    int visitID;
    int oedema = 0;
    int pregnant = -1;
    int PMTCT;
    string DOBofpatient;
    TextBox txtNewOIsProblemsOther;
    HtmlTableCell tcNewOIsProblemsOther;
    bool isUpdate;
    Hashtable GetValuefromHT;
    bool isAdult = false;
    int ArtStartinMonth = 0;
    int ArtRegimeninMonth = 0;
    StringBuilder sbValues;
    int PatID;
    string strmultiselect;
    String TableName;
    int icount;
    ArrayList arl;
    StringBuilder sbParameter;
    int PId;
    private void AddUIAttributes()
    {
        //txtVisitDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'); addDays(), SendCodeName()");
        //  txtVisitDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
        //txtVisitDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
        //txtVisitDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtVisitDate.ClientID + "', '" + txtVisitDate.ClientID + "');");

        // txtFollowUpDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
        txtFollowUpDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
        txtFollowUpDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");

        txtPhysHeight.Attributes.Add("onkeyup", "chkInteger('" + txtPhysHeight.ClientID + "')");
        txtPhysHeight.Attributes.Add("onBlur", "isBetween('" + txtPhysHeight.ClientID + "', '" + "physHeight" + "', '" + 0 + "', '" + 250 + "')");

        txtPhysWeight.Attributes.Add("onkeyup", "chkDecimal('" + txtPhysWeight.ClientID + "')");
        txtPhysWeight.Attributes.Add("onBlur", "isBetween('" + txtPhysWeight.ClientID + "', '" + "physWeight" + "', '" + 0 + "', '" + 225 + "')");

        txtEDD.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
        txtEDD.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

        txtGestation.Attributes.Add("onkeyup", "chkInteger('" + txtGestation.ClientID + "')");

        txtDeliveryDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');HIVcareArtPostPartumBRule(this.value,'" + cbPostPartum.ClientID + "')");
        txtDeliveryDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3');HIVcareArtPostPartumBRule(this.value,'" + cbPostPartum.ClientID + "')");
        txtDeliveryDate.Attributes.Add("onfocus", "DateFormat(this,this.value,event,false,'3');HIVcareArtPostPartumBRule(this.value,'" + cbPostPartum.ClientID + "')");

        txtMUAC.Attributes.Add("OnKeyup", "chkDecimal('" + txtMUAC.ClientID + "')");

        //txtTBRxStart.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'4')");
        //txtTBRxStart.Attributes.Add("OnFoucus", "javascript:vDateType='4'");

        //txtTBRxStop.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'4')");
        //txtTBRxStop.Attributes.Add("OnFoucus", "javascript:vDateType='4'");

        // txtcurrentARTDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtvisitDate.ClientID + "', '" + txtcurrentARTDate.ClientID + "', '" + "Current ART" + "', '" + txtcurrentARTDate.ClientID + "')");
        // txtTBRxStart.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtVisitDate.ClientID + "', '" + txtTBRxStart.ClientID + "', '" + "TB Rx Start" + "', '" + txtTBRxStart.ClientID + "'); isCheckValidDate_MMM_YR_TBRx('" + DOBofpatient + "', '" + txtTBRxStart.ClientID + "', '" + "TB Rx Start" + "', '" + txtTBRxStart.ClientID + "')");
        // txtTBRxStop.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtVisitDate.ClientID + "', '" + txtTBRxStop.ClientID + "', '" + "TB Rx Stop" + "', '" + txtTBRxStop.ClientID + "')");

        txtNumOfDaysHospitalized.Attributes.Add("onkeyup", "chkInteger('" + txtNumOfDaysHospitalized.ClientID + "')");

        ddlArvTherapyChangeCode.Attributes.Add("onchange", "TherapyBlueCard(this.options[this.selectedIndex].text, 1);");
        ddlArvTherapyStopCode.Attributes.Add("onchange", "TherapyBlueCard(this.options[this.selectedIndex].text, 2);");

        ddlTBStatus.Attributes.Add("onchange", "TBstatusBRule(this.options[this.selectedIndex].text);");
        ddlARVDrugsAdhere.Attributes.Add("onchange", "ARVDrugsPoorFairBRule(this.options[this.selectedIndex].text,'" + ddlReasonARVDrugsPoorFair.ClientID + "');HIVcareArtPoorFairOtherBRule('" + ddlReasonARVDrugsPoorFair.ClientID + "','Other','divReasonARVDrugsother');");
        ddlReasonARVDrugsPoorFair.Attributes.Add("onchange", "HIVcareArtOtherBRule(this.options[this.selectedIndex].text,'Other','divReasonARVDrugsother');");
        // ddlReferredTo.Attributes.Add("onchange", "HIVcareArtOtherBRule(this.options[this.selectedIndex].text,'Other (specify)','divotherReferredTo');");
        ddlFamilyPanningStatus.Attributes.Add("onchange", "HIVcareArtOtherBRule(this.options[this.selectedIndex].text,'ONFP=on Family Planning','divFamilyPlanningMethod');");
        lstclinPlanFU.Attributes.Add("onchange", "specifyChangeStop('" + lstclinPlanFU.ClientID + "');");
        // rdoPMTCT.Attributes.Add("onclick", "HIVcareArtPMTCTBRule('" + rdoPMTCT.ClientID + "','spanpmctcancno');");
    }
    private void restoreFontColor()
    {
        lblVisitDate.Style.Remove("color");
        lblFollowUpDate.Style.Remove("color");
        lblHeight.Style.Remove("color");
        lblWeight.Style.Remove("color");
        lblPregnant.Style.Remove("color");
        lblTBStatus.Style.Remove("color");
        lblWABStage.Style.Remove("color");
        lblWHOStage.Style.Remove("color");
        lblARVDrugsAdhere.Style.Remove("color");
    }
    private Boolean fieldValidation()
    {
        IIQCareSystem IQCareSystemInterface = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)IQCareSystemInterface.SystemDate();
        IQCareUtils iQCareUtils = new IQCareUtils();

        string validateMessage = "Following values are required to complete the data quality check:</br>";
        bool validationCheck = true;
        DateTime temp;

        #region Check Visit Date
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
            else if (theCurrentDate < Convert.ToDateTime(iQCareUtils.MakeDate(txtVisitDate.Value)))
            {
                validateMessage += "-" + IQCareMsgBox.GetMessage("CompareDate5", this) + "</br>";
                txtVisitDate.Focus();
                validationCheck = false;
            }
        }
        #endregion

        #region Check Follow Up Date
        //Check FollowUp Date
        if (txtFollowUpDate.Value.Trim() != "")
            if (!DateTime.TryParseExact(txtFollowUpDate.Value, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Follow Up Date";
                validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "</br>";
                txtFollowUpDate.Focus();
                validationCheck = false;
            }
            else if (validationCheck && temp < Convert.ToDateTime(iQCareUtils.MakeDate(txtVisitDate.Value)))
            {
                validateMessage += "-" + IQCareMsgBox.GetMessage("App_Visit", this) + "</br>";
                txtFollowUpDate.Focus();
                validationCheck = false;
            }
        #endregion
        oedema = rdoOedemaPlus.Checked == true ? 1 : (rdoOedemaMinus.Checked == true ? 2 : 0);

        //Pregnant
        pregnant = rdoPregnantYes.Checked == true ? 1 : (rdoPregnantNo.Checked == true ? 0 : -1);
        if (pregnant != -1)
        {
            if (pregnant == 1)
                txtDeliveryDate.Text = "";
            else
            {
                // txtEDD.Text = "";
                txtGestation.Text = "";
                // cbPMTCT.Checked = false;
                txtPMTCTANCNumber.Text = "";
            }
        }

        #region Check EDD
        ////if (txtEDD.Text.Trim() != "")
        ////    if (!DateTime.TryParseExact(txtEDD.Text, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
        ////    {
        ////        MsgBuilder msgBuilder = new MsgBuilder();
        ////        msgBuilder.DataElements["Control"] = " -EDD";
        ////        validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "</br>";
        ////        txtEDD.Focus();
        ////        validationCheck = false;
        ////    }            
        #endregion
        #region Check Delivery Date

        if (txtDeliveryDate.Text.Trim() != "")
            if (!DateTime.TryParseExact(txtDeliveryDate.Text, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -DeliveryDate";
                validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "</br>";
                txtDeliveryDate.Focus();
                validationCheck = false;
            }
        #endregion
        PMTCT = this.rdoPMTCT.Checked == true ? 1 : 0;

        #region Check TR Rx
        //////if (ddlTBStatus.SelectedItem.Text != "TB Rx")
        //////{
        //////    txtTBRxStart.Text = "";
        //////    txtTBRxStop.Text = "";
        //////    txtTBRegNumber.Text = "";
        //////}
        //////else
        //////{
        //////    if (txtTBRxStart.Text.Trim() != "")
        //////        if (!DateTime.TryParseExact(txtTBRxStart.Text, "MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
        //////        {
        //////            MsgBuilder msgBuilder = new MsgBuilder();
        //////            msgBuilder.DataElements["Control"] = " -TBRxStart";
        //////            validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "</br>";
        //////            txtTBRxStart.Focus();
        //////            validationCheck = false;
        //////        }
        //////    if (txtTBRxStop.Text.Trim() != "")
        //////        if (!DateTime.TryParseExact(txtTBRxStop.Text, "MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
        //////        {
        //////            MsgBuilder msgBuilder = new MsgBuilder();
        //////            msgBuilder.DataElements["Control"] = " -TBRxStop";
        //////            validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "</br>";
        //////            txtTBRxStop.Focus();
        //////            validationCheck = false;
        //////        }
        //////}
        #endregion

        //Check Other Potential Effect
        //////if (cblPotentialSideEffect.Items.FindByText("Other") != null && !cblPotentialSideEffect.Items.FindByText("Other").Selected)
        //////    txtPotentialSideEffectOther.Text = "";

        //New Ois Other Reason.
        ////if(!cblNewOIsProblems.Items.FindByText("Other").Selected)
        ////    txtNewOIsProblemsOther.Text = "";

        //Check Other Reason of Why ARV Drugs are Poor of Fair
        if (ddlARVDrugsAdhere.SelectedIndex == 0 || ddlARVDrugsAdhere.SelectedItem.Text == "G=Good")
            ddlReasonARVDrugsPoorFair.SelectedIndex = 0;

        if (ddlReasonARVDrugsPoorFair.SelectedItem.Text.Contains("Other") == false)
            txtReasonARVDrugsPoorFairOther.Text = "";

        //Check Referred To Other
        //if (ddlReferredTo.SelectedItem.Text != "Other (specify)")
        //    txtReferredToOther.Text = "";


        //  Subsitutions/Interruption
        #region
        if (lstclinPlanFU.SelectedValue.ToString() == "0")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "-Subsitutions/Interruption";
            //  IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            validateMessage += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this) + "</br>";
            lstclinPlanFU.Focus();
            validationCheck = false;

        }

        if (lstclinPlanFU.SelectedValue.ToString() == "99" && ddlArvTherapyStopCode.SelectedIndex == 0)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "-Stop Regimen Reason";
            // IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            validateMessage += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this) + "</br>";
            ddlArvTherapyStopCode.Focus();
            validationCheck = false;

        }
        if (lstclinPlanFU.SelectedValue.ToString() == "99" && txtARTEndeddate.Value == "")
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "-Date ART Ended";
            // IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "</br>";
            txtARTEndeddate.Focus();
            validationCheck = false;
        }
        if (lstclinPlanFU.SelectedValue.ToString() == "99" && ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other") && txtarvTherapyStopCodeOtherName.Value == "")
        {

            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "- Stop Regimen Reason Other(Specify)";
            // IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "</br>";
            ddlArvTherapyStopCode.Focus();
            validationCheck = false;

        }



        if (lstclinPlanFU.SelectedValue.ToString() == "98" && ddlArvTherapyChangeCode.SelectedIndex == 0)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "-Stop Regimen Reason";
            // IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            validateMessage += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this) + "</br>";
            ddlArvTherapyChangeCode.Focus();
            validationCheck = false;
        }
        if (lstclinPlanFU.SelectedValue.ToString() == "98" && ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
        {
            if (txtarvTherapyChangeCodeOtherName.Value == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Stop Regimen Reason (Other)Specify";
                // IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "</br>";
                ddlArvTherapyChangeCode.Focus();
                validationCheck = false;
            }
        }

        IQCareUtils theUtils = new IQCareUtils();
        HivCareARTEncounterInterface = (IHivCareARTEncounter)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BHivCareARTEncounter, BusinessProcess.Clinical");
        DataSet dsValidate = HivCareARTEncounterInterface.GetExistHIVArtCareEncounterbydate(patientID, txtVisitDate.Value, locationID);

        if (dsValidate.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToInt32(ViewState["VisitID"]) != Convert.ToInt32(dsValidate.Tables[0].Rows[0][0]))
            {
                IQCareMsgBox.Show("ARTHIVCareenCounterExists", this);
                validationCheck = false;
                return validationCheck; ;
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
    private Boolean dataQualityCheck()
    {
        IIQCareSystem IQCareSystemInterface = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DateTime theCurrentDate = (DateTime)IQCareSystemInterface.SystemDate();
        IQCareUtils iQCareUtils = new IQCareUtils();
        DateTime temp;
        string validateMessage = "Following values are required to complete the data quality check:</br>";
        bool qualityCheck = true;
        AuthenticationManager auth = new AuthenticationManager();
        bool dateconstraint = auth.CheckDateConstriant(Convert.ToInt32(Session["AppLocationId"]));
        restoreFontColor();
        #region Check Visit Date
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
        #region Check Follow Up Date
        if (txtFollowUpDate.Value.Trim() != "")
        {
            if (!DateTime.TryParseExact(txtFollowUpDate.Value, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Follow Up Date";
                validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "</br>";
                txtFollowUpDate.Focus();
                qualityCheck = false;
                lblFollowUpDate.Style.Add("color", "red");
            }
            else if (qualityCheck && temp < Convert.ToDateTime(iQCareUtils.MakeDate(txtVisitDate.Value)))
            {
                validateMessage += "-" + IQCareMsgBox.GetMessage("App_Visit", this) + "</br>";
                txtFollowUpDate.Focus();
                qualityCheck = false;
                lblFollowUpDate.Style.Add("color", "red");
            }
        }
        else
        {
            if (!dateconstraint)
            {
                if (txtFollowUpDate.Value.Trim() == "")
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -FollowUp Date";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
                    txtFollowUpDate.Focus();
                    qualityCheck = false;
                    lblFollowUpDate.Style.Add("color", "red");
                }
            }
        }
        #endregion
        //Weight
        if (txtPhysWeight.Text.Trim() == "")
        {
            MsgBuilder msgBuilder = new MsgBuilder();
            msgBuilder.DataElements["Control"] = " -Weight";
            validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
            txtPhysWeight.Focus();
            qualityCheck = false;
            lblWeight.Style.Add("color", "red");
        }
        //Height
        if (txtPhysHeight.Text.Trim() == "")
        {
            MsgBuilder msgBuilder = new MsgBuilder();
            msgBuilder.DataElements["Control"] = " -Height";
            validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "</br>";
            txtPhysHeight.Focus();
            qualityCheck = false;
            lblHeight.Style.Add("color", "red");
        }
        //Pregnant
        if (!rdoPregnantYes.Checked && !rdoPregnantNo.Checked && gender != "Male")
        {
            MsgBuilder msgBuilder = new MsgBuilder();
            msgBuilder.DataElements["Control"] = " -Pregnant";
            validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "</br>";
            rdoPregnantYes.Focus();
            qualityCheck = false;
            lblPregnant.Style.Add("color", "red");
        }
        //TBStatus
        ////if (ddlTBStatus.SelectedItem.Text == "Select")
        ////{
        ////    MsgBuilder msgBuilder = new MsgBuilder();
        ////    msgBuilder.DataElements["Control"] = " -TB Status";
        ////    validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "</br>";
        ////    ddlTBStatus.Focus();
        ////    qualityCheck = false;
        ////    lblTBStatus.Style.Add("color", "red");
        ////}
        //WABStage
        if (ddlWABStage.SelectedItem.Text == "Select")
        {
            MsgBuilder msgBuilder = new MsgBuilder();
            msgBuilder.DataElements["Control"] = " -WAB Stage";
            validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "</br>";
            ddlWABStage.Focus();
            qualityCheck = false;
            lblWABStage.Style.Add("color", "red");
        }
        //WHOStage
        if (ddlWHOStage.SelectedItem.Text == "Select")
        {
            MsgBuilder msgBuilder = new MsgBuilder();
            msgBuilder.DataElements["Control"] = " -WHO Stage";
            validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "</br>";
            ddlWHOStage.Focus();
            qualityCheck = false;
            lblWHOStage.Style.Add("color", "red");
        }
        //ARVDrugsAdhere
        if (ddlARVDrugsAdhere.SelectedItem.Text == "Select")
        {
            MsgBuilder msgBuilder = new MsgBuilder();
            msgBuilder.DataElements["Control"] = " -ARV Drugs Adhere";
            validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "</br>";
            ddlARVDrugsAdhere.Focus();
            qualityCheck = false;
            lblARVDrugsAdhere.Style.Add("color", "red");
        }

        if (!qualityCheck)
        {
            MsgBuilder totalMsgBuilder = new MsgBuilder();
            totalMsgBuilder.DataElements["MessageText"] = validateMessage;
            IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
        }
        return qualityCheck;
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        MsgBuilder theBuilder = new MsgBuilder();
        if (Request.QueryString["name"] == "Delete")
        {
            btnDataQualityCheck.Visible = false;
            btnSave.Text = "Delete";
            theBuilder.DataElements["FormName"] = "HIV Care/ART Encounter";
            IQCareMsgBox.ShowConfirm("DeleteForm", theBuilder, btnSave);
        }


        //GetValuefromHT = (Hashtable)Session["htPtnRegParameter"];
        Authentication = new AuthenticationManager();
        if (Authentication.HasFunctionRight(ApplicationAccess.HIVCareARTEncounter, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
        {
            btnPrint.Enabled = false;

        }

        // Utility utils = new Utility();

        if (Authentication.HasFunctionRight(ApplicationAccess.HIVCareARTEncounter, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
        {
            btnSave.Enabled = false;
            btnDataQualityCheck.Enabled = false;
        }
        else if (Request.QueryString["name"] == "Delete")
        {
            if (Authentication.HasFunctionRight(ApplicationAccess.HIVCareARTEncounter, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                string theUrl = "";
                theUrl = string.Format("{0}", "frmClinical_DeleteForm.aspx");
                Response.Redirect(theUrl);
            }
            else if (Authentication.HasFunctionRight(ApplicationAccess.HIVCareARTEncounter, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
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
        //Privilages for Care End
        //if (Convert.ToString(Session["CareEndFlag"]) == "1" && Convert.ToString(Session["CareendedStatus"]) == "1")
        //{
        //    btnSave.Enabled = true;
        //    btnDataQualityCheck.Enabled = true;
        //}
        IQCareUtils iQCareUtils = new IQCareUtils();
        BindFunctions bindFunctions = new BindFunctions();

        //Get Sessions
        patientID = Convert.ToInt32(Session["PatientId"].ToString());
        locationID = Convert.ToInt32(Session["AppLocationId"].ToString());
        gender = Session["PatientSex"].ToString();
        ageInYear = (int)Convert.ToDouble(Session["PatientAge"].ToString());
        ageInMonth = Convert.ToInt32(Session["PatientAge"].ToString().Substring(Session["PatientAge"].ToString().IndexOf(".") + 1));


        //Header Texts
        //(Master.FindControl("lblformname") as Label).Text = "HIV Care/ART Encounter Form";
        //(Master.FindControl("lblRoot") as Label).Text = "Clinical Forms >>";
        //(Master.FindControl("lblMark") as Label).Visible = false;
        //(Master.FindControl("lblheader") as Label).Text = "HIV Care/ART Encounter";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "HIV Care/ART Encounter";
        //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "HIV Care/ART Encounter Form";

        //UI Decision
        if (ageInYear <= 14)
        {
            //isAdult = false;

            divAdultPharmacy.Visible = true;
        }
        else
        {

            divAdultPharmacy.Visible = true;
        }
        if (Session["Paperless"].ToString() == "1")
        {
            //isAdult = false;
            divLabOrderTest.Visible = true;
            divLaboratory.Visible = false;
        }
        else
        {
            divLabOrderTest.Visible = false;
            divLaboratory.Visible = true;
        }

        if (ageInYear < 5)
        {
            tdOedema.Style.Remove("display");
            tdMUACAge.Style.Remove("display");
            txtAge.Text = ageInMonth.ToString();
            lblNutritionalProblems.Style.Remove("visibility");
            ddlNutritionalProblems.Style.Remove("visibility");
        }
        if (gender == "Male")
            tdPregnant.Style.Add("display", "none");

        if (gender == "Female" && ageInYear < 12)
        {
            tdPregnant.Style.Add("display", "none");
        }
        if (ageInYear < 2)
        {
            tdInfantFeedingPractice.Style.Remove("display");
        }
        if (gender == "Female" && ageInYear > 12)
        {
            tdInfantFeedingPractice.Style.Remove("display");
        }

        if (ageInYear < 12)
        {
            tdFamilyPlanning.Style.Add("display", "none");
        }

        AddUIAttributes();
        //  Show_Hide();
        //Auto Populate Fields
        HivCareARTEncounterInterface = (IHivCareARTEncounter)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BHivCareARTEncounter, BusinessProcess.Clinical");
        tempDataSet = HivCareARTEncounterInterface.GetHIVCareARTPatientFormData(patientID, locationID);
        tempDataTable = tempDataSet.Tables[0];




        if (tempDataTable.Rows.Count != 0)
        {
            if (tempDataTable.Rows[0]["ART"] != DBNull.Value)
            {
                //txtARTStart.Text = (((DateTime.Now.Year - Convert.ToDateTime(tempDataTable.Rows[0]["ART"]).Year) * 12) +
                //    (DateTime.Now.Month - Convert.ToDateTime(tempDataTable.Rows[0]["ART"]).Month)).ToString();

                ArtStartinMonth = Convert.ToDateTime(tempDataTable.Rows[0]["ART"]).Year * 12 + Convert.ToDateTime(tempDataTable.Rows[0]["ART"]).Month;
            }
            else
            {
                //txtARTStart.Text= "0";
                ArtStartinMonth = 0;
            }
            if (tempDataTable.Rows[0]["REGIMEN"] != DBNull.Value)
            {
                ArtRegimeninMonth = Convert.ToDateTime(tempDataTable.Rows[0]["REGIMEN"]).Year * 12 + Convert.ToDateTime(tempDataTable.Rows[0]["REGIMEN"]).Month;
                //txtStartingCurrentRegimen.Text = (((DateTime.Now.Year - Convert.ToDateTime(tempDataTable.Rows[0]["REGIMEN"]).Year) * 12) +
                //    (DateTime.Now.Month - Convert.ToDateTime(tempDataTable.Rows[0]["REGIMEN"]).Month)).ToString();
            }
            else
            {
                ArtRegimeninMonth = 0;
            }
        }
        //this,'" + ArtRegimeninMonth +"','"+txtARTStart.ClientID+"');");

        txtVisitDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "');HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "');isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtVisitDate.ClientID + "', '" + txtVisitDate.ClientID + "');");
        txtVisitDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3');HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "');HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "')");
        // txtVisitDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3');HIVCareEncounterARTStartBrule(this,'" + ArtRegimeninMonth + "','" + txtARTStart.ClientID + "');");

        if ((Convert.ToInt32(ViewState["VisitID"]) > 1) && (!IsPostBack))
        {
            //    HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "');HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "')");

            string ArtinfoScript = "<script language = 'javascript' defer ='defer' id = 'BRuleArtInfo'>\n";
            ArtinfoScript += "HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "'); \n";
            ArtinfoScript += "HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "'); \n";
            ArtinfoScript += "</script>\n";
            RegisterStartupScript("BRuleArtInfo", ArtinfoScript);
        }



        //Family Planning Status
        tempDataView = new DataView(tempDataSet.Tables[1]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            bindFunctions.BindCombo(ddlFamilyPanningStatus, tempDataTable, "Name", "ID");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }

        //Family Planning Methods
        tempDataView = new DataView(tempDataSet.Tables[2]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            //  bindFunctions.BindCheckedList(cblFamilyPlanningMethod, tempDataTable, "Name", "ID");
            //cblFamilyPlanningMethod.DataBind = tempDataTable;
            //tempDataView.Dispose();
            //tempDataTable.Clear();

            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            theDTFamilyPlanning = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            BindFunctions theBindFunction = new BindFunctions();
            theBindFunction.CreateBlueCheckedList(PnlFamilyPlanningMethod, theDTFamilyPlanning, "", "");
            //int countFamilyPlanning = 0;
            //for (int i = 0; i < tempDataTable.Rows.Count; i++)
            //{
            //    HtmlTableRow tr = new HtmlTableRow();
            //    HtmlTableCell tc = new HtmlTableCell();
            //    HtmlInputCheckBox chkFamilyPlanning = new HtmlInputCheckBox();
            //    chkFamilyPlanning.ID = Convert.ToString("FamilyPlanning" + i);
            //    chkFamilyPlanning.Value = tempDataTable.Rows[i][1].ToString();
            //    tc.Controls.Add(chkFamilyPlanning);
            //    tc.Controls.Add(new LiteralControl(chkFamilyPlanning.Value));
            //    tr.Cells.Add(tc);
            //    //if (chkPotentialSideEffect.Value == "Other")
            //    //{
            //    //    HtmlTableCell tc1 = new HtmlTableCell();
            //    //    //tc1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' >"));
            //    //    tc1.Controls.Add(new LiteralControl("<SPAN id='otherPotentialSideEffect' style='DISPLAY:none'>Other (specify): "));
            //    //    HtmlInputText HTextRMissed = new HtmlInputText();
            //    //    HTextRMissed.ID = "txtotherPotential";
            //    //    HTextRMissed.Size = 5;
            //    //    tc1.Controls.Add(HTextRMissed);
            //    //    tc1.Controls.Add(new LiteralControl(HTextRMissed.Value));
            //    //    tc1.Controls.Add(new LiteralControl("</SPAN>"));
            //    //    tr.Cells.Add(tc1);
            //    //    chkPotentialSideEffect.Attributes.Add("onclick", "toggle('otherPotentialSideEffect');");
            //    //}
            //    countFamilyPlanning++;
            //    tbFamilyPlanningMethod.Rows.Add(tr);

            //}
            tempDataView.Dispose();
            tempDataTable.Clear();
        }




        //TB Status
        tempDataView = new DataView(tempDataSet.Tables[3]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            bindFunctions.BindCombo(ddlTBStatus, tempDataTable, "Name", "ID");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }

        //Potential Side Effects
        tempDataView = new DataView(tempDataSet.Tables[4]);

        if (tempDataView.Table != null)
        {
            ////tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            ////bindFunctions.BindCheckedList(cblPotentialSideEffect, tempDataTable, "Name", "ID");
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            theDTpotentialSideEffect = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            BindFunctions theBindFunction = new BindFunctions();
            theBindFunction.CreateBlueCheckedList(PnlPotentialSideEffect, theDTpotentialSideEffect, "", "");
            int RMissed = 0;
            //for (int i = 0; i < tempDataTable.Rows.Count; i++)
            //{
            //    HtmlTableRow tr = new HtmlTableRow();
            //    HtmlTableCell tc = new HtmlTableCell();
            //    HtmlInputCheckBox chkPotentialSideEffect = new HtmlInputCheckBox();
            //    chkPotentialSideEffect.ID = Convert.ToString("PotentialSide" + i);
            //    chkPotentialSideEffect.Value = tempDataTable.Rows[i][1].ToString();
            //    tc.Controls.Add(chkPotentialSideEffect);
            //    tc.Controls.Add(new LiteralControl(chkPotentialSideEffect.Value));
            //    tr.Cells.Add(tc);
            //    if (chkPotentialSideEffect.Value == "Other")
            //    {
            //        HtmlTableCell tc1 = new HtmlTableCell();
            //        //tc1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' >"));
            //        tc1.Controls.Add(new LiteralControl("<SPAN id='otherPotentialSideEffect' style='DISPLAY:none'>Other (specify): "));
            //        HtmlInputText HTextRMissed = new HtmlInputText();
            //        HTextRMissed.ID = "txtotherPotential";
            //        HTextRMissed.Size = 5;
            //        tc1.Controls.Add(HTextRMissed);
            //        tc1.Controls.Add(new LiteralControl(HTextRMissed.Value));
            //        tc1.Controls.Add(new LiteralControl("</SPAN>"));
            //        tr.Cells.Add(tc1);
            //        chkPotentialSideEffect.Attributes.Add("onclick", "toggle('otherPotentialSideEffect');");
            //    }
            //    RMissed++;
            //    tblPotentialSideEffect.Rows.Add(tr);

            //}
            tempDataView.Dispose();
            tempDataTable.Clear();
        }

        //New Ois
        tempDataView = new DataView(tempDataSet.Tables[5]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            theDTNewOisProblem = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            //  bindFunctions.BindCheckedList(cblNewOIsProblems, tempDataTable, "Name", "ID");

            //int counterOIsProblems = 0;
            //for (int i = 0; i < tempDataTable.Rows.Count; i++)
            //{
            //    HtmlTableRow tr = new HtmlTableRow();
            //    HtmlTableCell tc = new HtmlTableCell();
            //    HtmlInputCheckBox chkNewOIsProblems = new HtmlInputCheckBox();
            //    chkNewOIsProblems.ID = Convert.ToString("NewOIsProblems" + i);
            //    chkNewOIsProblems.Value = tempDataTable.Rows[i][1].ToString();
            //    tc.Controls.Add(chkNewOIsProblems);
            //    tc.Controls.Add(new LiteralControl(chkNewOIsProblems.Value));
            //    tr.Cells.Add(tc);
            //    if (chkNewOIsProblems.Value == "Other")
            //    {
            //        HtmlTableCell tc1 = new HtmlTableCell();
            //        //tc1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' >"));
            //        tc1.Controls.Add(new LiteralControl("<SPAN id='otherchkNewOIsProblems' style='DISPLAY:none'>Specify: "));
            //        HtmlInputText HTextOIsProblems = new HtmlInputText();
            //        HTextOIsProblems.ID = "txtOIsProblems";
            //        HTextOIsProblems.Size = 5;
            //        tc1.Controls.Add(HTextOIsProblems);
            //        tc1.Controls.Add(new LiteralControl(HTextOIsProblems.Value));
            //        tc1.Controls.Add(new LiteralControl("</SPAN>"));
            //        tr.Cells.Add(tc1);
            //        chkNewOIsProblems.Attributes.Add("onclick", "toggle('otherchkNewOIsProblems');");
            //    }
            //    counterOIsProblems++;
            //    tblNewOIsProblemsOther.Rows.Add(tr);

            //}
            BindFunctions theBindFunction = new BindFunctions();
            theBindFunction.CreateBlueCheckedList(PnlNewOIsProblemsOther, theDTNewOisProblem, "", "");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }


        //Nutritional Problems
        tempDataView = new DataView(tempDataSet.Tables[6]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            bindFunctions.BindCombo(ddlNutritionalProblems, tempDataTable, "Name", "ID");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }

        //WAB Stage
        tempDataView = new DataView(tempDataSet.Tables[7]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            bindFunctions.BindCombo(ddlWABStage, tempDataTable, "Name", "ID");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }

        //WHO Stage
        tempDataView = new DataView(tempDataSet.Tables[8]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            bindFunctions.BindCombo(ddlWHOStage, tempDataTable, "Name", "ID");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }

        //CPT Adhere
        tempDataView = new DataView(tempDataSet.Tables[9]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            bindFunctions.BindCombo(ddlCPTAdhere, tempDataTable, "Name", "ID");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }

        //ARV Drugs Adhere
        tempDataView = new DataView(tempDataSet.Tables[10]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            bindFunctions.BindCombo(ddlARVDrugsAdhere, tempDataTable, "Name", "ID");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }

        //Reason why ARV Drugs Poor of Fair
        tempDataView = new DataView(tempDataSet.Tables[11]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            bindFunctions.BindCombo(ddlReasonARVDrugsPoorFair, tempDataTable, "Name", "ID");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }

        //Referred To
        tempDataView = new DataView(tempDataSet.Tables[12]);
        //if (tempDataView.Table != null)
        //{
        //    tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
        //    bindFunctions.BindCombo(ddlReferredTo, tempDataTable, "Name", "ID");
        //    tempDataView.Dispose();
        //    tempDataTable.Clear();
        //}
        if (tempDataView.Table != null)
        {
            ////tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            ////bindFunctions.BindCheckedList(cblPotentialSideEffect, tempDataTable, "Name", "ID");
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            theDTReferTo = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            //int reftoCount = 0;
            //for (int i = 0; i < tempDataTable.Rows.Count; i++)
            //{
            //    HtmlTableRow tr = new HtmlTableRow();
            //    HtmlTableCell tc = new HtmlTableCell();
            //    HtmlInputCheckBox chkRefferedTO = new HtmlInputCheckBox();
            //    chkRefferedTO.ID = Convert.ToString("ReferTo" + i);
            //    chkRefferedTO.Value = tempDataTable.Rows[i][1].ToString();
            //    tc.Controls.Add(chkRefferedTO);
            //    tc.Controls.Add(new LiteralControl(chkRefferedTO.Value));
            //    tr.Cells.Add(tc);
            //    if (chkRefferedTO.Value == "Other (specify)")
            //    {
            //        HtmlTableCell tc1 = new HtmlTableCell();
            //        //tc1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' >"));
            //        tc1.Controls.Add(new LiteralControl("<SPAN id='spanotherReferTo' style='DISPLAY:none'>Other (specify): "));
            //        HtmlInputText HTextReferTo = new HtmlInputText();
            //        HTextReferTo.ID = "txtotherReferTo";
            //        HTextReferTo.Size = 5;
            //        tc1.Controls.Add(HTextReferTo);
            //        tc1.Controls.Add(new LiteralControl(HTextReferTo.Value));
            //        tc1.Controls.Add(new LiteralControl("</SPAN>"));
            //        tr.Cells.Add(tc1);
            //        chkRefferedTO.Attributes.Add("onclick", "toggle('spanotherReferTo');");
            //    }
            //    reftoCount++;
            //    tblReferredTo.Rows.Add(tr);

            //}
            theDTReferTo = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            BindFunctions theBindFunction = new BindFunctions();
            theBindFunction.CreateBlueCheckedList(PnlReferredTo, theDTReferTo, "", "");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }


        //Nutritional Support
        tempDataView = new DataView(tempDataSet.Tables[13]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            bindFunctions.BindCombo(ddlNutritionalSupport, tempDataTable, "Name", "ID");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }



        //Infant Feeding Option
        tempDataView = new DataView(tempDataSet.Tables[14]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            bindFunctions.BindCombo(ddlInfantFeedingPractice, tempDataTable, "Name", "ID");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }

        //Attending Clinician
        tempDataView = new DataView(tempDataSet.Tables[15]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            DataColumn dataColumnFullName = new DataColumn("Name");
            dataColumnFullName.DataType = System.Type.GetType("System.String");
            tempDataTable.Columns.Add(dataColumnFullName);

            for (int i = 0; i < tempDataTable.Rows.Count; i++)
                tempDataTable.Rows[i]["Name"] = tempDataTable.Rows[i]["FirstName"].ToString() + " " + tempDataTable.Rows[i]["LastName"].ToString();

            //bindFunctions.BindCombo(ddlAttendingClinician, tempDataTable, "Name", "EmployeeID");
            BindUserDropdown(ddlAttendingClinician, Session["AppUserId"].ToString());

            tempDataView.Dispose();
            tempDataTable.Clear();
        }

        //Height
        tempDataView = new DataView(tempDataSet.Tables[16]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            if (tempDataTable.Rows.Count > 0 && ageInYear >= 15)
                txtPhysHeight.Text = tempDataTable.Rows[0]["height"].ToString();
        }


        // tempDataView = new DataView(tempDataSet.Tables[17]);
        if (tempDataSet.Tables[17].Rows.Count > 0)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(tempDataSet.Tables[17].Rows[0]["EmergContactName"])))
                txtTreatmentSupporterName.Text = tempDataSet.Tables[17].Rows[0]["EmergContactName"].ToString();

            if (!String.IsNullOrEmpty(Convert.ToString(tempDataSet.Tables[17].Rows[0]["EmergContactPhone"])))
                txtTreatmentSupporterContact.Text = tempDataSet.Tables[17].Rows[0]["EmergContactPhone"].ToString();
        }
        //Subsitution /Interruption
        tempDataView = new DataView(tempDataSet.Tables[18]);
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            bindFunctions.BindCombo(lstclinPlanFU, tempDataTable, "Name", "ID");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }

        //PMTCT/ANC No.
        tempDataView = new DataView(tempDataSet.Tables[19]);
        if (tempDataView.Table != null)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(tempDataSet.Tables[19].Rows[0]["ANCNumber"])))
            {
                txtPMTCTANCNumber.Text = tempDataSet.Tables[19].Rows[0]["ANCNumber"].ToString();
                tempDataView.Dispose();
                tempDataTable.Clear();
                txtPMTCTANCNumber.ReadOnly = true;
            }
            else
            {
                txtPMTCTANCNumber.ReadOnly = false;
            }
            if (!String.IsNullOrEmpty(Convert.ToString(tempDataSet.Tables[19].Rows[0]["DOB"])))
            {
                DOBofpatient = String.Format("{0:dd-MMM-yyyy}", tempDataSet.Tables[19].Rows[0]["DOB"]);
                // DOBofpatient = Convert.ToString(Convert.ToString(tempDataSet.Tables[19].Rows[0]["DOB"]));
                txtTBRxStart.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtVisitDate.ClientID + "', '" + txtTBRxStart.ClientID + "', '" + "TB Rx Start" + "', '" + txtTBRxStart.ClientID + "'); isCheckValidDate_MMM_YR_TBRxStart('" + DOBofpatient + "', '" + txtTBRxStart.ClientID + "', '" + "TB Rx Start" + "', '" + txtTBRxStart.ClientID + "')");
                txtTBRxStop.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtVisitDate.ClientID + "', '" + txtTBRxStop.ClientID + "', '" + "TB Rx Stop" + "', '" + txtTBRxStop.ClientID + "');  isCheckValidDate_MMM_YR_TBRxSTOP('" + txtTBRxStart.ClientID + "', '" + txtTBRxStop.ClientID + "', '" + "TB Rx Stop" + "', '" + txtTBRxStop.ClientID + "')");
            }

        }

        //Therapy Change Codes
        tempDataView = new DataView(tempDataSet.Tables[20]);
        tempDataView.RowFilter = "CodeID='6' and ModuleId=" + Convert.ToInt32(Session["TechnicalAreaId"]);
        tempDataView.Sort = "ID ASC";
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            bindFunctions.BindCombo(ddlArvTherapyChangeCode, tempDataTable, "Name", "ID");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }
        //Therapy Stop Codes
        tempDataView = new DataView(tempDataSet.Tables[20]);
        tempDataView.RowFilter = "CodeID='5' and ModuleId=" + Convert.ToInt32(Session["TechnicalAreaId"]);
        tempDataView.Sort = "ID ASC";
        if (tempDataView.Table != null)
        {
            tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
            bindFunctions.BindCombo(ddlArvTherapyStopCode, tempDataTable, "Name", "ID");
            tempDataView.Dispose();
            tempDataTable.Clear();
        }

        //DataSet DStheXML = new DataSet();
        //DStheXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
        //DataView theDV = new DataView();
        //DataTable theDT = new DataTable();
        //theDV = new DataView(DStheXML.Tables["mst_Reason"]);
        //theDV.RowFilter = "DeleteFlag='0' and CategoryID='2'";
        //theDV.Sort = "Name ASC";
        //if (theDV.Table != null)
        //{
        //    theDT = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
        //    bindFunctions.BindCombo(ddlArvTherapyChangeCode, theDT, "Name", "ID");
        //    theDV.Dispose();
        //    theDT.Clear();
        //}
        ////Therapy Stop Codes
        //theDV = new DataView(DStheXML.Tables["mst_Reason"]);
        //theDV.RowFilter = "DeleteFlag='0' and CategoryID='2'";
        //theDV.Sort = "Name ASC";
        //if (theDV.Table != null)
        //{
        //    theDT = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
        //    bindFunctions.BindCombo(ddlArvTherapyStopCode, theDT, "Name", "ID");
        //    theDV.Dispose();
        //    theDT.Clear();
        //}
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
                //SecurityPerTabSignature = userId;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["PatientVisitId"] != null)

        Maintain_ViewState(Convert.ToString(ddlArvTherapyChangeCode.SelectedItem), Convert.ToString(ddlArvTherapyStopCode.SelectedItem), Convert.ToInt32(lstclinPlanFU.SelectedValue), Convert.ToString(ddlARVDrugsAdhere.SelectedItem), Convert.ToString(ddlReasonARVDrugsPoorFair.SelectedItem), Convert.ToString(ddlFamilyPanningStatus.SelectedItem), Convert.ToString(ddlTBStatus.SelectedItem));
        Show_Hide();
        PutCustomControl();
        if (Convert.ToInt32(ViewState["VisitID"]) > 0)
        {
            isUpdate = true;
        }
        else
        {
            if (Convert.ToInt32(Session["PatientVisitIdhiv"]) > 0)
            {
                Session["ArtEncounterPatientVisitId"] = Session["PatientVisitIdhiv"];
                visitID = Convert.ToInt32(Session["PatientVisitIdhiv"]);

                isUpdate = true;
            }
            else
            {
                visitID = 0;
                Session["ArtEncounterPatientVisitId"] = 0;
            }
            ViewState["VisitID"] = visitID;
        }



        if (!IsPostBack && Convert.ToInt32(ViewState["VisitID"]) > 0)
        {
            tempDataSet = HivCareARTEncounterInterface.GetHIVCareARTPatientVisitInfo(patientID, locationID, Convert.ToInt32(ViewState["VisitID"]));

            //VisitDate & Attending Clinican & dataQuality
            tempDataTable = tempDataSet.Tables[0];
            if (tempDataTable.Rows.Count != 0)
            {
                if (tempDataTable.Rows[0]["visitDate"] != DBNull.Value)
                {
                    txtVisitDate.Value = Convert.ToDateTime(tempDataTable.Rows[0]["visitDate"].ToString()).ToString(Session["AppDateFormat"].ToString());
                    string ArtinfoScript = "<script language = 'javascript' defer ='defer' id = 'BRuleArtInfo'>\n";
                    ArtinfoScript += "HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "'); \n";
                    ArtinfoScript += "HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "'); \n";
                    ArtinfoScript += "</script>\n";
                    RegisterStartupScript("BRuleArtInfo", ArtinfoScript);

                }
                //if (tempDataTable.Rows[0]["attendingClinician"] != DBNull.Value)                    
                //    ddlAttendingClinician.SelectedValue = tempDataTable.Rows[0]["attendingClinician"].ToString();
                if (tempDataTable.Rows[0]["createDate"] != DBNull.Value)
                    ViewState["createDate"] = tempDataTable.Rows[0]["createDate"].ToString();
                if (tempDataTable.Rows[0]["dataQuality"] != System.DBNull.Value)
                {
                    //ViewState["dataQuality"] = tempDataTable.Rows[0]["dataQuality"].ToString();
                    if (Convert.ToInt32(tempDataTable.Rows[0]["dataQuality"]) == 1)
                        btnDataQualityCheck.CssClass = "greenbutton";
                }
            }

            //TreatmentSupporter Name & Contact
            tempDataTable = tempDataSet.Tables[1];
            if (tempDataTable.Rows.Count != 0)
            {
                if (tempDataTable.Rows[0]["treatmentSupporterName"] != DBNull.Value)
                    txtTreatmentSupporterName.Text = tempDataTable.Rows[0]["treatmentSupporterName"].ToString();
                if (tempDataTable.Rows[0]["treatmentSupporterContact"] != DBNull.Value)
                    txtTreatmentSupporterContact.Text = tempDataTable.Rows[0]["treatmentSupporterContact"].ToString();

                //if (tempDataTable.Rows[0]["nutritionalSupport"] != DBNull.Value)
                //    ddlNutritionalSupport.SelectedValue = tempDataTable.Rows[0]["nutritionalSupport"].ToString();

                if (tempDataTable.Rows[0]["TBRegNumber"] != DBNull.Value)
                    txtTBRegNumber.Value = tempDataTable.Rows[0]["TBRegNumber"].ToString();
                if (tempDataTable.Rows[0]["nutritionalProblem"] != DBNull.Value)
                    ddlNutritionalProblems.SelectedValue = tempDataTable.Rows[0]["nutritionalProblem"].ToString();

                if (tempDataTable.Rows[0]["attendingClinician"] != DBNull.Value)
                    ddlAttendingClinician.SelectedValue = tempDataTable.Rows[0]["attendingClinician"].ToString();


                if (tempDataTable.Rows[0]["Scheduled"] != DBNull.Value)
                    chkifschedule.Checked = tempDataTable.Rows[0]["Scheduled"].ToString() == "1" ? true : false;


            }

            //Follow Up Date
            tempDataTable = tempDataSet.Tables[2];
            if (tempDataTable.Rows.Count != 0)
                if (tempDataTable.Rows[0]["followUpDate"] != DBNull.Value)
                    txtFollowUpDate.Value = Convert.ToDateTime(tempDataTable.Rows[0]["followUpDate"].ToString()).ToString(Session["AppDateFormat"].ToString());

            //Height Weight Oedema
            tempDataTable = tempDataSet.Tables[3];
            if (tempDataTable.Rows.Count != 0)
            {
                if (tempDataTable.Rows[0]["height"] != DBNull.Value)
                    txtPhysHeight.Text = tempDataTable.Rows[0]["height"].ToString();
                if (tempDataTable.Rows[0]["weight"] != DBNull.Value)
                    txtPhysWeight.Text = tempDataTable.Rows[0]["weight"].ToString();
                if (tempDataTable.Rows[0]["oedema"] != DBNull.Value)
                {
                    if (Convert.ToInt32(tempDataTable.Rows[0]["oedema"].ToString()) == 1)
                        rdoOedemaPlus.Checked = true;
                    else if (Convert.ToInt32(tempDataTable.Rows[0]["oedema"].ToString()) == 2)
                        rdoOedemaMinus.Checked = true;
                }
            }
            string script;
            //	Pregnancy & EDD & DateOfDelivery & PMTCT & MUAC	
            tempDataTable = tempDataSet.Tables[4];
            if (tempDataTable.Rows.Count != 0)
            {
                if (tempDataTable.Rows[0]["pregnant"] != DBNull.Value)
                {
                    if (Convert.ToInt32(tempDataTable.Rows[0]["pregnant"].ToString()) == 1)
                    {
                        rdoPregnantYes.Checked = true;
                        if (tempDataTable.Rows[0]["EDD"] != System.DBNull.Value)
                        {
                            this.txtEDD.Value = String.Format("{0:dd-MMM-yyyy}", tempDataTable.Rows[0]["EDD"]);
                        }
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
                        script += "show('rdopregnantyesno');\n";
                        script += "hide('spanEDDNopregnant');\n";
                        script += "show('spanEDD');\n";
                        script += "</script>\n";
                        RegisterStartupScript("PregnantYes", script);
                        ViewState["Pregstatus"] = "1";
                    }
                    else if (Convert.ToInt32(tempDataTable.Rows[0]["Pregnant"].ToString()) == 0)
                    {
                        this.rdoPregnantNo.Checked = true;
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'PregnantNo'>\n";
                        script += "show('rdopregnantyesno');\n";
                        script += "show('spanEDDNopregnant');\n";
                        script += "</script>\n";
                        RegisterStartupScript("PregnantNo", script);
                        ViewState["Pregstatus"] = "2";

                        //if (tempDataTable.Rows[0]["PostPartum"] != System.DBNull.Value)
                        //{
                        //    if (Convert.ToInt32(tempDataTable.Rows[0]["PostPartum"].ToString()) == 0)
                        //    {
                        //        cbPostPartum.Checked = false;
                        //    }
                        //    else if (Convert.ToInt32(tempDataTable.Rows[0]["PostPartum"].ToString()) == 1)
                        //    {
                        //        cbPostPartum.Checked = true;
                        //    }
                        //}
                        if (tempDataTable.Rows[0]["deliveryDate"] != System.DBNull.Value)
                        {
                            this.txtDeliveryDate.Text = String.Format("{0:dd-MMM-yyyy}", tempDataTable.Rows[0]["deliveryDate"]);
                            cbPostPartum.Checked = true;
                        }
                        else
                        {
                            cbPostPartum.Checked = false;
                        }
                    }

                }


                if (tempDataTable.Rows[0]["PMTCT"] != DBNull.Value)
                {
                    if (Convert.ToInt32(tempDataTable.Rows[0]["PMTCT"].ToString()) == 1)
                    {
                        this.rdoPMTCT.Checked = true;
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'PregnantPMTCT_0'>\n";
                        script += "show('spanpmctcancno');\n";

                        script += "</script>\n";
                        RegisterStartupScript("PregnantPMTCT_0", script);
                    }
                    else
                    {
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'PregnantPMTCT_0'>\n";
                        script += "hide('spanpmctcancno');\n"; ;
                        script += "</script>\n";
                        RegisterStartupScript("PregnantPMTCT_0", script);
                    }
                }


                //if (tempDataTable.Rows[0]["EDD"] != DBNull.Value)
                //  txtEDD = Convert.ToDateTime(tempDataTable.Rows[0]["EDD"].ToString()).ToString(Session["AppDateFormat"].ToString());
                //if (tempDataTable.Rows[0]["deliveryDate"] != DBNull.Value)
                //    txtDeliveryDate.Text = Convert.ToDateTime(tempDataTable.Rows[0]["deliveryDate"].ToString()).ToString(Session["AppDateFormat"].ToString());


                if (tempDataTable.Rows[0]["MUAC"] != DBNull.Value)
                    txtMUAC.Text = tempDataTable.Rows[0]["MUAC"].ToString();
            }

            //Gestation
            tempDataTable = tempDataSet.Tables[5];
            if (tempDataTable.Rows.Count != 0)
                if (tempDataTable.Rows[0]["gestation"] != DBNull.Value)
                    txtGestation.Text = tempDataTable.Rows[0]["gestation"].ToString();

            //PMTCTANCNumber
            tempDataTable = tempDataSet.Tables[6];
            if (tempDataTable.Rows.Count != 0)
                if (tempDataTable.Columns.Contains("PMTCTANCNumber"))
                    if (tempDataTable.Rows[0]["PMTCTANCNumber"] != DBNull.Value)
                        txtPMTCTANCNumber.Text = tempDataTable.Rows[0]["PMTCTANCNumber"].ToString();

            // txtMUAC.Text = tempDataTable.Rows[0]["PMTCTAMCNumber"].ToString();

            //Family Planning & NumOfDaysHospitalized & NutritionalSupport
            tempDataTable = tempDataSet.Tables[7];
            if (tempDataTable.Rows.Count != 0)
            {
                if (tempDataTable.Rows[0]["familyPlanningStatus"] != DBNull.Value)
                    ddlFamilyPanningStatus.SelectedValue = tempDataTable.Rows[0]["familyPlanningStatus"].ToString();
                if (tempDataTable.Rows[0]["numOfDaysHospitalized"] != DBNull.Value)
                    txtNumOfDaysHospitalized.Text = tempDataTable.Rows[0]["numOfDaysHospitalized"].ToString();

                if (tempDataTable.Rows[0]["nutritionalSupport"] != DBNull.Value)
                    ddlNutritionalSupport.SelectedValue = tempDataTable.Rows[0]["nutritionalSupport"].ToString();


                if (ddlFamilyPanningStatus.SelectedItem.Text.ToUpper() == "ONFP=ON FAMILY PLANNING")
                {
                    script = "";
                    script = "<script language = 'javascript' defer ='defer' id = 'FamilyPlanning_0'>\n";
                    script += "show('divFamilyPlanningMethod');\n";
                    script += "</script>\n";
                    RegisterStartupScript("FamilyPlanning_0", script);
                    tempDataTable = tempDataSet.Tables[17];
                    //for (int i = 0; i < tempDataTable.Rows.Count; i++)
                    //{
                    //    //if (tempDataTable.Rows[i]["familyPlanningMethodID"] != DBNull.Value)
                    //    //    cblFamilyPlanningMethod.Items.FindByValue(tempDataTable.Rows[i]["familyPlanningMethodID"].ToString()).Selected = true;
                    //}

                    if (tempDataTable.Rows.Count > 0)
                    {
                        FillCheckBoxListData(tempDataTable, PnlFamilyPlanningMethod, "familyPlanningMethodID", "");
                        //    for (int i = 0; i < tempDataTable.Rows.Count; i++)
                        //    {
                        //        foreach (HtmlTableRow r in tbFamilyPlanningMethod.Rows)
                        //        {
                        //            foreach (HtmlTableCell c in r.Cells)
                        //            {
                        //                foreach (Control ct in c.Controls)
                        //                {
                        //                    if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        //                    {
                        //                        foreach (DataRow drFamilyPlanning in theDTFamilyPlanning.Rows)
                        //                        {
                        //                            if (((HtmlInputCheckBox)ct).Value == drFamilyPlanning[1].ToString())
                        //                            {
                        //                                if (drFamilyPlanning[0].ToString() == tempDataTable.Rows[i]["familyPlanningMethodID"].ToString())
                        //                                {
                        //                                    //if (drNewOisProblem[1].ToString() == "Other")
                        //                                    //{
                        //                                    //    OtherOIsProblem = tempDataTable.Rows[i]["newOIsProblemOther"].ToString();
                        //                                    //    OIsProblem = drNewOisProblem[1].ToString();
                        //                                    //    ((HtmlInputCheckBox)ct).Checked = true;
                        //                                    //}
                        //                                    //else
                        //                                    //{
                        //                                        ((HtmlInputCheckBox)ct).Checked = true;

                        //                                   // }
                        //                                }
                        //                            }
                        //                        }

                        //                    }
                        //                    //else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        //                    //{
                        //                    //    if (OIsProblem == "Other")
                        //                    //    {
                        //                    //        ((HtmlInputText)ct).Value = OtherOIsProblem;
                        //                    //        script = "";
                        //                    //        script = "<script language = 'javascript' defer ='defer' id = 'OIsProblem_0'>\n";
                        //                    //        script += "show('otherchkNewOIsProblems');\n";
                        //                    //        script += "</script>\n";
                        //                    //        RegisterStartupScript("OIsProblem_0", script);
                        //                    //        ViewState["OIsProblemOther"] = 1;
                        //                    //    }
                        //                    //}

                        //                }
                        //            }
                        //        }
                        //    }
                        //}






                    }
                    else
                    {
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'FamilyPlanning_1'>\n";
                        script += "hide('divFamilyPlanningMethod');\n";
                        script += "</script>\n";
                        RegisterStartupScript("FamilyPlanning_1", script);
                    }
                }
            }
            //TB Status		
            tempDataTable = tempDataSet.Tables[8];
            if (tempDataTable.Rows.Count != 0)
            {
                if (tempDataTable.Rows[0]["TBStatus"] != DBNull.Value)
                {
                    ddlTBStatus.SelectedValue = tempDataTable.Rows[0]["TBStatus"].ToString();

                    if (tempDataTable.Rows[0]["TBStatus"] != DBNull.Value)
                    {
                        ddlTBStatus.SelectedValue = tempDataTable.Rows[0]["TBStatus"].ToString();
                        if (ddlTBStatus.SelectedItem.Text.Trim() == "TB Rx")
                        {
                            if (tempDataTable.Rows[0]["TBRxStart"] != DBNull.Value)
                                this.txtTBRxStart.Value = Convert.ToDateTime(tempDataTable.Rows[0]["TBRxStart"].ToString()).ToString("MMM-yyyy");

                            if (tempDataTable.Rows[0]["TBRxStop"] != DBNull.Value)
                                txtTBRxStop.Value = Convert.ToDateTime(tempDataTable.Rows[0]["TBRxStop"].ToString()).ToString("MMM-yyyy");

                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TBStatusRX_0'>\n";
                            script += "show('divTBStatusTBRX');\n";
                            script += "</script>\n";
                            RegisterStartupScript("TBStatusRX_0", script);
                        }
                        else
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TBStatusRX_0'>\n";
                            script += "hide('divTBStatusTBRX');\n";
                            script += "</script>\n";
                            RegisterStartupScript("TBStatusRX_0", script);
                        }

                    }
                }

            }

            //  Subsitutions/Interruption
            tempDataTable = tempDataSet.Tables[16];
            if (tempDataTable.Rows.Count != 0)
            {
                if (tempDataTable.Rows[0]["TherapyPlan"] != DBNull.Value)
                    lstclinPlanFU.SelectedValue = tempDataTable.Rows[0]["TherapyPlan"].ToString();
                if (tempDataTable.Rows[0]["PrescribedARVStartDate"] != DBNull.Value)
                {
                    // this.txt.Value = Convert.ToDateTime(tempDataTable.Rows[0]["PrescribedARVStartDate"].ToString()).ToString("MMM-yyyy");
                }
                if (tempDataTable.Rows[0]["TherapyReasonCode"] != System.DBNull.Value)
                {

                    if (this.lstclinPlanFU.SelectedValue == "98")
                    {
                        this.ddlArvTherapyChangeCode.SelectedValue = tempDataTable.Rows[0]["TherapyReasonCode"].ToString();
                        this.txtarvTherapyChangeCodeOtherName.Value = tempDataTable.Rows[0]["TherapyOther"].ToString();
                        if (this.ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TherapyCode10'>\n";
                            script += "show('arvTherapyChange');\n";
                            script += "show('otherarvTherapyChangeCode');\n";
                            script += "</script>\n";
                            RegisterStartupScript("TherapyCode10", script);
                        }
                        else
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TherapyCode11'>\n";
                            script += "show('arvTherapyChange');\n";
                            script += "</script>\n";
                            RegisterStartupScript("TherapyCode11", script);
                        }
                    }
                    if (this.lstclinPlanFU.SelectedValue == "99")
                    {
                        this.ddlArvTherapyStopCode.SelectedValue = tempDataTable.Rows[0]["THerapyReasonCOde"].ToString();
                        this.txtarvTherapyStopCodeOtherName.Value = tempDataTable.Rows[0]["TherapyOther"].ToString();
                        DateTime theTmpDtTherapy = Convert.ToDateTime(tempDataTable.Rows[0]["PrescribedARVStartDate"]);
                        this.txtARTEndeddate.Value = theTmpDtTherapy.ToString(Session["AppDateFormat"].ToString());

                        if (this.ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other"))
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TherapyCode20'>\n";
                            script += "show('arvTherapyStop');\n";
                            script += "show('otherarvTherapyStopCode');\n";
                            script += "</script>\n";
                            RegisterStartupScript("TherapyCode20", script);
                        }
                        else
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TherapyCode21'>\n";
                            script += "show('arvTherapyStop');\n";
                            script += "</script>\n";
                            RegisterStartupScript("TherapyCode21", script);
                        }
                    }
                }
            }

            //  ddlTBStatus_SelectedIndexChanged(null, null);	
            //TB Reg Number & New OIs Problem & Nutritional Problem
            tempDataTable = tempDataSet.Tables[9];
            if (tempDataTable.Rows.Count != 0)
            {
                //if (tempDataTable.Rows[0]["TBRegNumber"] != DBNull.Value)
                //    txtTBRegNumber.Value = tempDataTable.Rows[0]["TBRegNumber"].ToString();
                //if (tempDataTable.Rows[0]["nutritionalProblem"] != DBNull.Value)
                //    ddlNutritionalProblems.SelectedValue = tempDataTable.Rows[0]["nutritionalProblem"].ToString();
                ////for (int i = 0; i < tempDataTable.Rows.Count; i++)
                ////{

                ////    if (tempDataTable.Rows[i]["newOIsProblemID"] != DBNull.Value)
                ////         cblNewOIsProblems.Items.FindByValue(tempDataTable.Rows[i]["newOIsProblemID"].ToString()).Selected = true;

                ////        if (tempDataTable.Rows[i]["newOIsProblemOther"] != DBNull.Value)
                ////            txtNewOIsProblemsOther.Text = tempDataTable.Rows[i]["newOIsProblemOther"].ToString();                    
                ////}
            }
            if (tempDataTable.Rows.Count > 0)
            {
                FillCheckBoxListData(tempDataTable, PnlNewOIsProblemsOther, "newOIsProblemID", "newOIsProblemOther");
                //string OtherOIsProblem = "";
                //string OIsProblem = "";
                //for (int i = 0; i < tempDataTable.Rows.Count; i++)
                //{
                //    foreach (HtmlTableRow r in tblNewOIsProblemsOther.Rows)
                //    {
                //        foreach (HtmlTableCell c in r.Cells)
                //        {
                //            foreach (Control ct in c.Controls)
                //            {
                //                if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                //                {
                //                    foreach (DataRow drNewOisProblem in theDTNewOisProblem.Rows)
                //                    {
                //                        if (((HtmlInputCheckBox)ct).Value == drNewOisProblem[1].ToString())
                //                        {
                //                            if (drNewOisProblem[0].ToString() == tempDataTable.Rows[i]["newOIsProblemID"].ToString())
                //                            {
                //                                if (drNewOisProblem[1].ToString() == "Other")
                //                                {
                //                                    OtherOIsProblem = tempDataTable.Rows[i]["newOIsProblemOther"].ToString();
                //                                    OIsProblem = drNewOisProblem[1].ToString();
                //                                    ((HtmlInputCheckBox)ct).Checked = true;
                //                                }
                //                                else
                //                                {
                //                                    ((HtmlInputCheckBox)ct).Checked = true;

                //                                }
                //                            }
                //                        }
                //                    }

                //                }
                //                else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                //                {
                //                    if (OIsProblem == "Other")
                //                    {
                //                        ((HtmlInputText)ct).Value = OtherOIsProblem;
                //                        script = "";
                //                        script = "<script language = 'javascript' defer ='defer' id = 'OIsProblem_0'>\n";
                //                        script += "show('otherchkNewOIsProblems');\n";
                //                        script += "</script>\n";
                //                        RegisterStartupScript("OIsProblem_0", script);
                //                        ViewState["OIsProblemOther"] = 1;
                //                    }
                //                }

                //            }
                //        }
                //    }
                //}
            }

            ////   cblNewOIsProblems_SelectedIndexChanged(null, null);
            //10 Potential Side Effects
            tempDataTable = tempDataSet.Tables[10];
            ////if(tempDataTable.Rows.Count != 0)            
            ////    for(int i = 0; i < tempDataTable.Rows.Count; i++)
            ////    {                    
            ////        ////if(tempDataTable.Rows[i]["potentialSideEffectID"] != DBNull.Value)
            ////        ////    cblPotentialSideEffect.Items.FindByValue(tempDataTable.Rows[i]["potentialSideEffectID"].ToString()).Selected = true;
            ////        ////if(tempDataTable.Rows[i]["potentialSideEffectOther"] != DBNull.Value)
            ////        ////    txtPotentialSideEffectOther.Text = tempDataTable.Rows[i]["potentialSideEffectOther"].ToString();                    
            ////    }
            if (tempDataTable.Rows.Count > 0)
            {
                FillCheckBoxListData(tempDataTable, PnlPotentialSideEffect, "potentialSideEffectID", "potentialSideEffectOther");
                //string OtherpotentialSideEffect = "";
                //string potentialSideEffect = "";
                //for (int i = 0; i < tempDataTable.Rows.Count; i++)
                //{
                //    foreach (HtmlTableRow r in tblPotentialSideEffect.Rows)
                //    {
                //        foreach (HtmlTableCell c in r.Cells)
                //        {
                //            foreach (Control ct in c.Controls)
                //            {
                //                if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                //                {
                //                    foreach (DataRow drpotentialSideEffect in theDTpotentialSideEffect.Rows)
                //                    {
                //                        if (((HtmlInputCheckBox)ct).Value == drpotentialSideEffect[1].ToString())
                //                        {
                //                            if (drpotentialSideEffect[0].ToString() == tempDataTable.Rows[i]["potentialSideEffectID"].ToString())
                //                            {
                //                                if (drpotentialSideEffect[1].ToString() == "Other")
                //                                {
                //                                    OtherpotentialSideEffect = tempDataTable.Rows[i]["potentialSideEffectOther"].ToString();
                //                                    potentialSideEffect = drpotentialSideEffect[1].ToString();
                //                                    ((HtmlInputCheckBox)ct).Checked = true;
                //                                }
                //                                else
                //                                {
                //                                    ((HtmlInputCheckBox)ct).Checked = true;

                //                                }
                //                            }
                //                        }
                //                    }

                //                }
                //                else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                //                {
                //                    if (potentialSideEffect == "Other")
                //                    {
                //                        ((HtmlInputText)ct).Value = OtherpotentialSideEffect;
                //                        script = "";
                //                        script = "<script language = 'javascript' defer ='defer' id = 'potentialSideEffect_0'>\n";
                //                        script += "show('otherPotentialSideEffect');\n";
                //                        script += "</script>\n";
                //                        RegisterStartupScript("potentialSideEffect_0", script);
                //                        ViewState["potentialSideEffecOther"] = 1;
                //                    }
                //                }

                //            }
                //        }
                //    }
                //}
            }


            //  cblPotentialSideEffect_SelectedIndexChanged(null, null);
            //11 WAB Stage & WHO Stage
            tempDataTable = tempDataSet.Tables[11];
            if (tempDataTable.Rows.Count != 0)
            {
                if (tempDataTable.Rows[0]["WABStage"] != DBNull.Value)
                    ddlWABStage.SelectedValue = tempDataTable.Rows[0]["WABStage"].ToString();
                if (tempDataTable.Rows[0]["WHOStage"] != DBNull.Value)
                    ddlWHOStage.SelectedValue = tempDataTable.Rows[0]["WHOStage"].ToString();
            }

            //12 CPTAdhere
            tempDataTable = tempDataSet.Tables[12];
            if (tempDataTable.Rows.Count != 0)
                if (tempDataTable.Rows[0]["CPTAdhere"] != DBNull.Value)
                    ddlCPTAdhere.SelectedValue = tempDataTable.Rows[0]["CPTAdhere"].ToString();

            //13 ARV Drugs Adhere + Reason
            tempDataTable = tempDataSet.Tables[13];
            if (tempDataTable.Rows.Count != 0)
            {
                if (tempDataTable.Rows[0]["ARVDrugsAdhere"] != DBNull.Value)
                {
                    ddlARVDrugsAdhere.SelectedValue = tempDataTable.Rows[0]["ARVDrugsAdhere"].ToString();
                    // ddlARVDrugsAdhere.Attributes.Add("onchange", "ARVDrugsPoorFairBRule(this.options[this.selectedIndex].text,'" + ddlReasonARVDrugsPoorFair.ClientID + "');HIVcareArtPoorFairOtherBRule('" + ddlReasonARVDrugsPoorFair.ClientID + "','Other','divReasonARVDrugsother');");
                    if (ddlARVDrugsAdhere.SelectedItem.Text == "F=Fair" || ddlARVDrugsAdhere.SelectedItem.Text == "P=Poor")
                    {
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsAdhere_0'>\n";
                        script += "HIVcareArtEnableARVDrugsPoor('" + ddlReasonARVDrugsPoorFair.ClientID + "','0');\n";
                        script += "</script>\n";
                        RegisterStartupScript("ARVDrugsAdhere_0", script);
                    }
                    else
                    {
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsAdhere_1'>\n";
                        // script += "DisableDIVallControl('divARVDrugsPoorFair');\n";
                        script += "HIVcareArtDisableARVDrugsPoor('" + ddlReasonARVDrugsPoorFair.ClientID + "''0');\n";
                        script += "</script>\n";
                        RegisterStartupScript("ARVDrugsAdhere_1", script);
                    }
                }
                if (tempDataTable.Rows[0]["reasonARVDrugsPoorFair"] != DBNull.Value)
                {
                    ddlReasonARVDrugsPoorFair.SelectedValue = tempDataTable.Rows[0]["reasonARVDrugsPoorFair"].ToString();
                    if (ddlReasonARVDrugsPoorFair.SelectedItem.Text.Contains("Other"))
                    {
                        //tdReasonARVDrugsPoorFairOther
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsPoorFairOther_0'>\n";
                        script += "show('divReasonARVDrugsother');\n";
                        script += "</script>\n";
                        RegisterStartupScript("ARVDrugsPoorFairOther_0", script);
                    }
                    else
                    {
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsPoorFairOther_1'>\n";
                        script += "hide('divReasonARVDrugsother');\n";
                        script += "</script>\n";
                        RegisterStartupScript("ARVDrugsPoorFairOther_1", script);
                    }
                }
                if (tempDataTable.Rows[0]["reasonARVDrugsPoorFairOther"] != DBNull.Value)
                    txtReasonARVDrugsPoorFairOther.Text = tempDataTable.Rows[0]["reasonARVDrugsPoorFairOther"].ToString();
            }
            //ddlARVDrugsAdhere_SelectedIndexChanged(null, null);
            //ddlReasonARVDrugsPoorFair_SelectedIndexChanged(null, null);
            ////14 ReferredTo + Other  
            tempDataTable = tempDataSet.Tables[14];
            ////if(tempDataTable.Rows.Count != 0)
            ////{            
            ////if(tempDataTable.Rows[0]["referredTo"] != DBNull.Value)
            ////    ddlReferredTo.SelectedValue = tempDataTable.Rows[0]["referredTo"].ToString();
            ////if(tempDataTable.Rows[0]["referredToOther"] != DBNull.Value)
            ////    txtReferredToOther.Text = tempDataTable.Rows[0]["referredToOther"].ToString();



            //// }
            if (tempDataTable.Rows.Count > 0)
            {
                FillCheckBoxListData(tempDataTable, PnlReferredTo, "referredTo", "referredToOther");
                //string OtherReferTo = "";
                //string ReferTo = "";
                //for (int i = 0; i < tempDataTable.Rows.Count; i++)
                //{
                //    foreach (HtmlTableRow r in tblReferredTo.Rows)
                //    {
                //        foreach (HtmlTableCell c in r.Cells)
                //        {
                //            foreach (Control ct in c.Controls)
                //            {
                //                if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                //                {
                //                    foreach (DataRow drReferTo in theDTReferTo.Rows)
                //                    {
                //                        if (((HtmlInputCheckBox)ct).Value == drReferTo[1].ToString())
                //                        {
                //                            if (drReferTo[0].ToString() == tempDataTable.Rows[i]["referredTo"].ToString())
                //                            {
                //                                if (drReferTo[1].ToString() == "Other (specify)")
                //                                {
                //                                    OtherReferTo = tempDataTable.Rows[i]["referredToOther"].ToString();
                //                                    ReferTo = drReferTo[1].ToString();
                //                                    ((HtmlInputCheckBox)ct).Checked = true;
                //                                }
                //                                else
                //                                {
                //                                    ((HtmlInputCheckBox)ct).Checked = true;

                //                                }
                //                            }
                //                        }
                //                    }

                //                }
                //                else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                //                {
                //                    if (ReferTo == "Other (specify)")
                //                    {
                //                        ((HtmlInputText)ct).Value = OtherReferTo;
                //                        script = "";
                //                        script = "<script language = 'javascript' defer ='defer' id = 'ReferredTo_0'>\n";
                //                        script += "show('spanotherReferTo');\n";
                //                        script += "</script>\n";
                //                        RegisterStartupScript("ReferredTo_0", script);
                //                        ViewState["ReferredToOther"] = 1;
                //                    }
                //                }

                //            }
                //        }
                //    }
                //}
            }

            //  ddlReferredTo_SelectedIndexChanged(null, null);
            //15 Infant Feeding Option
            tempDataTable = tempDataSet.Tables[15];
            if (tempDataTable.Rows.Count != 0)
                if (tempDataTable.Rows[0]["infantFeedingOption"] != DBNull.Value)
                    ddlInfantFeedingPractice.SelectedValue = tempDataTable.Rows[0]["infantFeedingOption"].ToString();

            PId = Convert.ToInt32(Session["PatientId"]);

            FillOldData(PId);

        }
    }
    #region "Added New Functions-07-June-2012"



    private void FillCheckBoxListData(DataTable theDT, Panel thePnl, string FieldName, string theFieldName)
    {

        foreach (DataRow DR in theDT.Rows)
        {
            foreach (Control y in thePnl.Controls)
            {

                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    if (y.ID.StartsWith("Pnl"))
                    {
                        FillCheckBoxListData(theDT, (System.Web.UI.WebControls.Panel)y, FieldName, theFieldName);
                    }
                }

                else
                {
                    if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                    {
                        string[] theControlId = ((CheckBox)y).ID.ToString().Split('-');
                        if (((CheckBox)y).ID == "Chk-" + DR[FieldName].ToString() + "-" + theControlId[2].ToString())
                            ((CheckBox)y).Checked = true;
                    }
                    if (y.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {
                        if (theFieldName != "")
                        {
                            string[] theControlId;
                            if (((System.Web.UI.WebControls.TextBox)y).ID.Contains("OtherTXT") == true)
                            {
                                theControlId = ((TextBox)y).ID.ToString().Split('-');
                                ((TextBox)y).Text = DR[theFieldName].ToString();

                            }
                            string script = "";
                            script = "<script language = 'javascript' defer ='defer' id = " + ((TextBox)y).ID + ">\n";
                            script += "show('txt" + (((TextBox)y).ID.ToString().Split('-')[1]).ToString() + "');\n";
                            script += "</script>\n";
                            RegisterStartupScript("" + ((TextBox)y).ID + "", script);
                        }



                    }
                }

            }

        }
    }

    private void ShowhideTextbox(Panel thePnl)
    {
        foreach (Control y in thePnl.Controls)
        {

            if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
            {
                if (y.ID.StartsWith("Pnl"))
                {
                    ShowhideTextbox((System.Web.UI.WebControls.Panel)y);
                }
            }

            else
            {
                if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                {
                    string[] theControlId = ((CheckBox)y).ID.ToString().Split('-');
                    if (((CheckBox)y).Text.Contains("Other") && ((CheckBox)y).Checked == true)
                    {
                        //((CheckBox)y).Checked = true;
                        string script = "";
                        script = "<script language = 'javascript' defer ='defer' id = " + ((CheckBox)y).ID + ">\n";
                        script += "show('txt" + (((CheckBox)y).ID.ToString().Split('-')[1]).ToString() + "');\n";
                        script += "</script>\n";
                        RegisterStartupScript("" + ((CheckBox)y).ID + "", script);
                    }
                }

            }

        }


    }

    private DataTable GetCheckBoxListcheckedIDs(Panel thePnl, string FieldName, string thetxtFieldName, int Flag)
    {

        string chktrueother = "";
        int chktrueothervalue = 0;
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
                        //theDR[FieldName] = theControlId[1].ToString();
                        if (theControlId[2].ToString().Contains("Other") == true)
                        {
                            chktrueother = theControlId[2].ToString();
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
                            if (chktrueothervalue > 0)
                            {
                                theDR[FieldName] = chktrueothervalue.ToString();
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

                }
                //DTCheckedIds.Rows.Add(theDR);
            }
        }
        return DTCheckedIds;

    }
    #endregion
    private void save(bool isDataQuailtyChecked)
    {
        if (fieldValidation() == false)
        { return; }

        hashTable = new Hashtable();
        dataSetForSaving = new DataSet();
        HivCareARTEncounterInterface = (IHivCareARTEncounter)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BHivCareARTEncounter, BusinessProcess.Clinical");
        //Appointment Scheduling
        hashTable.Add("patientID", patientID.ToString());
        if (!isDataQuailtyChecked)
            hashTable.Add("dataQuality", "0");
        else
            hashTable.Add("dataQuality", "1");
        hashTable.Add("locationID", locationID.ToString());
        hashTable.Add("visitDate", txtVisitDate.Value);
        hashTable.Add("UserID", Convert.ToInt32(Session["AppUserId"].ToString()));

        if (chkifschedule.Checked == true)
        {
            hashTable.Add("Scheduled", 1);
        }
        else
        {
            hashTable.Add("Scheduled", 0);
        }

        hashTable.Add("treatmentSupporterName", txtTreatmentSupporterName.Text);
        hashTable.Add("treatmentSupporterContact", txtTreatmentSupporterContact.Text);
        hashTable.Add("followUpDate", txtFollowUpDate.Value);

        //Clinical Status
        hashTable.Add("height", txtPhysHeight.Text);
        hashTable.Add("weight", txtPhysWeight.Text);
        hashTable.Add("oedema", oedema.ToString());

        hashTable.Add("pregnant", pregnant.ToString());
        hashTable.Add("EDD", txtEDD.Value);
        hashTable.Add("gestation", txtGestation.Text);
        hashTable.Add("PMTCT", PMTCT.ToString());
        hashTable.Add("PMTCTANCNumber", txtPMTCTANCNumber.Text);
        hashTable.Add("deliveryDate", txtDeliveryDate.Text);

        //if (pregnant == 1)
        //{
        //    hashTable.Add("PostPartum", "");
        //}
        //else
        //{
        //    hashTable.Add("PostPartum", (cbPostPartum.Checked==true)?1:0);
        //}



        //Family Planning
        hashTable.Add("familyPlanningStatus", ddlFamilyPanningStatus.SelectedValue);
        //Table 0-Family Planning Methods
        DataTable dataTableFamilyPlanningMethods = new DataTable();
        DataColumn dataColumnFamilyPlanningMethods = new DataColumn("familyPlanningMethodID");
        dataColumnFamilyPlanningMethods.DataType = System.Type.GetType("System.Int32");
        dataTableFamilyPlanningMethods.Columns.Add(dataColumnFamilyPlanningMethods);
        DataRow dataRowFamilyPlanningMethods;

        if (ddlFamilyPanningStatus.SelectedItem.Text.ToUpper() == "ONFP=ON FAMILY PLANNING")
        {
            dataTableFamilyPlanningMethods = GetCheckBoxListcheckedIDs(PnlFamilyPlanningMethod, "familyPlanningMethodID", "", 0);
            //for (int i = 0; i < cblFamilyPlanningMethod.Items.Count; i++)
            //    if (cblFamilyPlanningMethod.Items[i].Selected)
            //    {
            //        dataRowFamilyPlanningMethods = dataTableFamilyPlanningMethods.NewRow();
            //        dataRowFamilyPlanningMethods["familyPlanningMethodID"] = Convert.ToInt32(cblFamilyPlanningMethod.Items[i].Value);
            //        dataTableFamilyPlanningMethods.Rows.Add(dataRowFamilyPlanningMethods);
            //    }
            //foreach (HtmlTableRow tr in tbFamilyPlanningMethod.Rows)
            //{
            //    dataRowFamilyPlanningMethods = dataTableFamilyPlanningMethods.NewRow();
            //    foreach (HtmlTableCell tc in tr.Cells)
            //    {
            //        foreach (Control ct in tc.Controls)
            //        {
            //            if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
            //            {
            //                if (((HtmlInputCheckBox)ct).Checked == true)
            //                {
            //                    foreach (DataRow dr in theDTFamilyPlanning.Rows)
            //                    {
            //                        if (((HtmlInputCheckBox)ct).Value == dr[1].ToString())
            //                        {
            //                            //if (dr[1].ToString() == "Other")
            //                            //{
            //                            //    chktrueothervalue = Convert.ToInt32(dr[0]);
            //                            //    chktrueother = dr[1].ToString();
            //                            //}
            //                            //else
            //                            //{
            //                            dataRowFamilyPlanningMethods["familyPlanningMethodID"] = dr[0].ToString();
            //                            //dataRowFamilyPlanningMethods["potentialSideEffect_Other"] = null;
            //                            dataTableFamilyPlanningMethods.Rows.Add(dataRowFamilyPlanningMethods);
            //                          //  }
            //                        }
            //                    }
            //                }
            //            }

            //            //if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
            //            //{
            //            //    if (chktrueother == "Other")
            //            //    {
            //            //        if (ct.ID.ToString() == "txtotherPotential")
            //            //        {
            //            //            drPotentialSideEffect["potentialSideEffectID"] = chktrueothervalue;
            //            //            drPotentialSideEffect["potentialSideEffect_Other"] = ((HtmlInputText)ct).Value;
            //            //            dataTablePotentialSideEffect.Rows.Add(drPotentialSideEffect);
            //            //        }
            //            //    }
            //            //}
            //        }
            //    }
            //}

        }
        dataSetForSaving.Tables.Add(dataTableFamilyPlanningMethods);

        //MUAC
        hashTable.Add("MUAC", txtMUAC.Text);

        //TB Rx
        hashTable.Add("TBStatus", ddlTBStatus.SelectedValue);
        hashTable.Add("TBRxStart", txtTBRxStart.Value);
        hashTable.Add("TBRxStop", txtTBRxStop.Value);
        hashTable.Add("TBRegNumber", txtTBRegNumber.Value);

        //Subsitutions/Interruption
        hashTable.Add("TherapyPlan", lstclinPlanFU.SelectedValue);


        string OtherReason = "";
        // int ReasonID;

        string ARVTherapyCode = "0";
        string artendeddate = "";
        if (lstclinPlanFU.SelectedValue == "98")
        {
            ARVTherapyCode = ddlArvTherapyChangeCode.SelectedValue;
            if (ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
            {
                OtherReason = txtarvTherapyChangeCodeOtherName.Value;
            }
        }
        else if (lstclinPlanFU.SelectedValue == "99")
        {

            ARVTherapyCode = ddlArvTherapyStopCode.SelectedValue;
            artendeddate = txtARTEndeddate.Value;
            if (ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other"))
            {
                OtherReason = txtarvTherapyStopCodeOtherName.Value;
            }

        }
        if (lstclinPlanFU.SelectedValue.ToString() == "96")
        {
            Session["ARTEndedStatus"] = "";
        }
        else if (lstclinPlanFU.SelectedValue.ToString() == "99")
        {
            Session["ARTEndedStatus"] = "ART Stopped";
        }
        hashTable.Add("PrescribedARVStartDate", artendeddate);
        hashTable.Add("TherapyReasonCode", ARVTherapyCode);
        hashTable.Add("TherapyOther", OtherReason);

        //Potential Side Effects
        ////if (cblPotentialSideEffect.Items.FindByText("Other").Selected)        
        ////    hashTable.Add("potentialSideEffectOtherID", cblPotentialSideEffect.Items.FindByText("Other").Value.ToString());                    
        ////else
        ////    hashTable.Add("potentialSideEffectOtherID", "99999");
        ////hashTable.Add("potentialSideEffectOther", txtPotentialSideEffectOther.Text);
        //DataTable dataTablePotentialSideEffect = new DataTable();
        //DataColumn dataColumnPotentialSideEffect = new DataColumn("potentialSideEffectID");
        //dataColumnPotentialSideEffect.DataType = System.Type.GetType("System.Int32");
        //dataTablePotentialSideEffect.Columns.Add(dataColumnPotentialSideEffect);
        //DataRow dataRowPotentialSideEffect;

        ////for (int i = 0; i < cblPotentialSideEffect.Items.Count; i++)
        ////    if (cblPotentialSideEffect.Items[i].Selected && cblPotentialSideEffect.Items[i].Text != "Other")
        ////    {
        ////        dataRowPotentialSideEffect = dataTablePotentialSideEffect.NewRow();
        ////        dataRowPotentialSideEffect["potentialSideEffectID"] = Convert.ToInt32(cblPotentialSideEffect.Items[i].Value);
        ////        dataTablePotentialSideEffect.Rows.Add(dataRowPotentialSideEffect);
        ////    }
        //DataTable dataTablePotentialSideEffect = new DataTable();
        //DataColumn dataColumnPotentialSideEffect = new DataColumn("potentialSideEffectID");
        //dataColumnPotentialSideEffect.DataType = System.Type.GetType("System.Int32");
        //dataTablePotentialSideEffect.Columns.Add(dataColumnPotentialSideEffect);
        //DataRow drPotentialSideEffect;

        //DataColumn thepotentialSideEffect_Other = new DataColumn("potentialSideEffect_Other");
        //thepotentialSideEffect_Other.DataType = System.Type.GetType("System.String");
        //dataTablePotentialSideEffect.Columns.Add(thepotentialSideEffect_Other);

        //string chktrueother = "";
        //int chktrueothervalue = 0;
        //foreach (HtmlTableRow tr in tblPotentialSideEffect.Rows)
        //{
        //    drPotentialSideEffect = dataTablePotentialSideEffect.NewRow();
        //    foreach (HtmlTableCell tc in tr.Cells)
        //    {
        //        foreach (Control ct in tc.Controls)
        //        {
        //            if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
        //            {
        //                if (((HtmlInputCheckBox)ct).Checked == true)
        //                {
        //                    foreach (DataRow dr in theDTpotentialSideEffect.Rows)
        //                    {
        //                        if (((HtmlInputCheckBox)ct).Value == dr[1].ToString())
        //                        {
        //                            if (dr[1].ToString() == "Other")
        //                            {
        //                                chktrueothervalue = Convert.ToInt32(dr[0]);
        //                                chktrueother = dr[1].ToString();
        //                            }
        //                            else
        //                            {
        //                                drPotentialSideEffect["potentialSideEffectID"] = dr[0].ToString();
        //                                drPotentialSideEffect["potentialSideEffect_Other"] = null;
        //                                dataTablePotentialSideEffect.Rows.Add(drPotentialSideEffect);
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
        //            {
        //                if (chktrueother == "Other")
        //                {
        //                    if (ct.ID.ToString() == "txtotherPotential")
        //                    {
        //                        drPotentialSideEffect["potentialSideEffectID"] = chktrueothervalue;
        //                        drPotentialSideEffect["potentialSideEffect_Other"] = ((HtmlInputText)ct).Value;
        //                        dataTablePotentialSideEffect.Rows.Add(drPotentialSideEffect);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        DataTable dataTablePotentialSideEffect = new DataTable();
        dataTablePotentialSideEffect = GetCheckBoxListcheckedIDs(PnlPotentialSideEffect, "potentialSideEffectID", "potentialSideEffect_Other", 0);
        dataSetForSaving.Tables.Add(dataTablePotentialSideEffect);


        //New Ois Problems        
        ////////if (cblNewOIsProblems.Items.FindByText("Other").Selected)        
        ////////    hashTable.Add("newOIsProblemOtherID", cblNewOIsProblems.Items.FindByText("Other").Value.ToString());                    
        ////////else
        ////////    hashTable.Add("newOIsProblemOtherID", "99999");        
        //hashTable.Add("newOIsProblemOther", txtNewOIsProblemsOther.Text);
        //DataTable dataTableNewOIsProblems = new DataTable();
        //DataColumn dataColumnNewOIsProblems = new DataColumn("newOIsProblemID");
        //dataColumnNewOIsProblems.DataType = System.Type.GetType("System.Int32");
        //dataTableNewOIsProblems.Columns.Add(dataColumnNewOIsProblems);
        //DataRow drNewOIsProblems;

        //DataColumn NewOIsProblems_Other = new DataColumn("newOIsProblemID_Other");
        //NewOIsProblems_Other.DataType = System.Type.GetType("System.String");
        //dataTableNewOIsProblems.Columns.Add(NewOIsProblems_Other);

        // chktrueother = "";
        // chktrueothervalue = 0;
        //foreach (HtmlTableRow tr in tblNewOIsProblemsOther.Rows)
        //{
        //    drNewOIsProblems = dataTableNewOIsProblems.NewRow();
        //    foreach (HtmlTableCell tc in tr.Cells)
        //    {
        //        foreach (Control ct in tc.Controls)
        //        {
        //            if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
        //            {
        //                if (((HtmlInputCheckBox)ct).Checked == true)
        //                {
        //                    foreach (DataRow dr in theDTNewOisProblem.Rows)
        //                    {
        //                        if (((HtmlInputCheckBox)ct).Value == dr[1].ToString())
        //                        {
        //                            if (dr[1].ToString() == "Other")
        //                            {
        //                                chktrueothervalue = Convert.ToInt32(dr[0]);
        //                                chktrueother = dr[1].ToString();
        //                            }
        //                            else
        //                            {
        //                                drNewOIsProblems["newOIsProblemID"] = dr[0].ToString();
        //                                drNewOIsProblems["newOIsProblemID_Other"] = null;
        //                                dataTableNewOIsProblems.Rows.Add(drNewOIsProblems);
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
        //            {
        //                if (chktrueother == "Other")
        //                {
        //                    if (ct.ID.ToString() == "txtOIsProblems")
        //                    {
        //                        drNewOIsProblems["newOIsProblemID"] = chktrueothervalue;
        //                        drNewOIsProblems["newOIsProblemID_Other"] = ((HtmlInputText)ct).Value;
        //                        dataTableNewOIsProblems.Rows.Add(drNewOIsProblems);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}


        //////for (int i = 0; i < cblNewOIsProblems.Items.Count; i++)
        //////    if (cblNewOIsProblems.Items[i].Selected && cblNewOIsProblems.Items[i].Text != "Other")
        //////    {
        //////        dataRowNewOIsProblems = dataTableNewOIsProblems.NewRow();
        //////        dataRowNewOIsProblems["newOIsProblemID"] = Convert.ToInt32(cblNewOIsProblems.Items[i].Value);
        //////        dataTableNewOIsProblems.Rows.Add(dataRowNewOIsProblems);
        //////    }
        //dataSetForSaving.Tables.Add(dataTableNewOIsProblems);

        DataTable dataTableNewOIsProblems = new DataTable();
        dataTableNewOIsProblems = GetCheckBoxListcheckedIDs(PnlNewOIsProblemsOther, "newOIsProblemID", "newOIsProblemID_Other", 0);
        dataSetForSaving.Tables.Add(dataTableNewOIsProblems);

        //Nutritional Problems
        hashTable.Add("nutritionalProblem", ddlNutritionalProblems.SelectedValue);
        //WAB Stage
        hashTable.Add("WABStage", ddlWABStage.SelectedValue);
        //WHO Stage
        hashTable.Add("WHOStage", ddlWHOStage.SelectedValue);
        //CPT Adhere
        hashTable.Add("CPTAdhere", ddlCPTAdhere.SelectedValue);
        //ARV Drugs Adhere
        hashTable.Add("ARVDrugsAdhere", ddlARVDrugsAdhere.SelectedValue);
        //Reason why ARV Drugs are poor or fair
        hashTable.Add("reasonARVDrugsPoorFair", ddlReasonARVDrugsPoorFair.SelectedValue);
        //Other Reason why ARV Drugs are poor or fair
        hashTable.Add("reasonARVDrugsPoorFairOther", txtReasonARVDrugsPoorFairOther.Text);
        //Referred To
        ////hashTable.Add("referredTo", ddlReferredTo.SelectedValue);
        //////Referred To Other
        ////hashTable.Add("referredToOther", txtReferredToOther.Text);

        //DataTable dataTableReferredTo = new DataTable();
        //DataColumn dataColumnReferredTo = new DataColumn("referredToID");
        //dataColumnReferredTo.DataType = System.Type.GetType("System.Int32");
        //dataTableReferredTo.Columns.Add(dataColumnReferredTo);
        //DataRow drReferredTo;

        //DataColumn ReferredTo_Other = new DataColumn("referredToOtherID_Other");
        //ReferredTo_Other.DataType = System.Type.GetType("System.String");
        //dataTableReferredTo.Columns.Add(ReferredTo_Other);

        //chktrueother = "";
        //chktrueothervalue = 0;
        //foreach (HtmlTableRow tr in tblReferredTo.Rows)
        //{
        //    drReferredTo = dataTableReferredTo.NewRow();
        //    foreach (HtmlTableCell tc in tr.Cells)
        //    {
        //        foreach (Control ct in tc.Controls)
        //        {
        //            if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
        //            {
        //                if (((HtmlInputCheckBox)ct).Checked == true)
        //                {
        //                    foreach (DataRow dr in theDTReferTo.Rows)
        //                    {
        //                        if (((HtmlInputCheckBox)ct).Value == dr[1].ToString())
        //                        {
        //                            if (dr[1].ToString() == "Other (specify)")
        //                            {
        //                                chktrueothervalue = Convert.ToInt32(dr[0]);
        //                                chktrueother = dr[1].ToString();
        //                            }
        //                            else
        //                            {
        //                                drReferredTo["referredToID"] = dr[0].ToString();
        //                                drReferredTo["referredToOtherID_Other"] = null;
        //                                dataTableReferredTo.Rows.Add(drReferredTo);
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
        //            {
        //                if (chktrueother == "Other (specify)")
        //                {
        //                    if (ct.ID.ToString() == "txtotherReferTo")
        //                    {
        //                        drReferredTo["referredToID"] = chktrueothervalue;
        //                        drReferredTo["referredToOtherID_Other"] = ((HtmlInputText)ct).Value;
        //                        dataTableReferredTo.Rows.Add(drReferredTo);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //dataSetForSaving.Tables.Add(dataTableReferredTo);
        DataTable dataTableReferredTo = new DataTable();
        dataTableReferredTo = GetCheckBoxListcheckedIDs(PnlReferredTo, "referredToID", "referredToOtherID_Other", 0);
        dataSetForSaving.Tables.Add(dataTableReferredTo);


        //If Hospitalized # of Days
        hashTable.Add("numOfDaysHospitalized", txtNumOfDaysHospitalized.Text);
        //Nutritional Support
        hashTable.Add("nutritionalSupport", ddlNutritionalSupport.SelectedValue);
        //Infant Feeding Option
        hashTable.Add("infantFeedingOption", ddlInfantFeedingPractice.SelectedValue);
        //Name of attending clinician
        hashTable.Add("attendingClinician", ddlAttendingClinician.SelectedValue);

        //CreateDate
        if (isUpdate)
        {
            hashTable.Add("createDate", Session["visitdatenewhiv"].ToString());
            hashTable.Add("visitID", Convert.ToInt32(Session["PatientVisitIdhiv"]));
        }


        DataTable theCustomDataDT = null;
        if ((Convert.ToInt32(Session["PatientVisitIdhiv"]) > 0))
        {
            //   visitPK = Convert.ToInt32(Session["PatientVisitId"]);

            CustomFieldClinical theCustomManager = new CustomFieldClinical();
            theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);

        }
        else
        {
            CustomFieldClinical theCustomManager = new CustomFieldClinical();
            theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);

        }




        DataSet dsreturn = HivCareARTEncounterInterface.SaveUpdateHIVCareARTPatientVisit(hashTable, dataSetForSaving, isUpdate, theCustomDataDT);
        Session["PatientVisitIdhiv"] = Convert.ToInt32(dsreturn.Tables[0].Rows[0]["visitID"].ToString());
        ViewState["VisitID"] = Session["PatientVisitIdhiv"];
        Session["visitdatenewhiv"] = Convert.ToDateTime(dsreturn.Tables[0].Rows[0]["VisitDate"].ToString());
        ViewState["VisitDate"] = Convert.ToDateTime(dsreturn.Tables[0].Rows[0]["VisitDate"].ToString());
        Session["ServiceLocationId"] = Convert.ToInt32(dsreturn.Tables[0].Rows[0]["LocationID"].ToString());

        hashTable.Clear();
        closeWindow();
    }
    private void save(bool isDataQuailtyChecked, string button)
    {
        if (fieldValidation() == false)
        { return; }

        hashTable = new Hashtable();
        dataSetForSaving = new DataSet();
        HivCareARTEncounterInterface = (IHivCareARTEncounter)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BHivCareARTEncounter, BusinessProcess.Clinical");
        //Appointment Scheduling
        hashTable.Add("patientID", patientID.ToString());
        if (!isDataQuailtyChecked)
            hashTable.Add("dataQuality", "0");
        else
            hashTable.Add("dataQuality", "1");
        hashTable.Add("locationID", locationID.ToString());
        hashTable.Add("visitDate", txtVisitDate.Value);
        hashTable.Add("UserID", Convert.ToInt32(Session["AppUserId"].ToString()));

        if (chkifschedule.Checked == true)
        {
            hashTable.Add("Scheduled", 1);
        }
        else
        {
            hashTable.Add("Scheduled", 0);
        }

        hashTable.Add("treatmentSupporterName", txtTreatmentSupporterName.Text);
        hashTable.Add("treatmentSupporterContact", txtTreatmentSupporterContact.Text);
        hashTable.Add("followUpDate", txtFollowUpDate.Value);

        //Clinical Status
        hashTable.Add("height", txtPhysHeight.Text);
        hashTable.Add("weight", txtPhysWeight.Text);
        hashTable.Add("oedema", oedema.ToString());

        hashTable.Add("pregnant", pregnant.ToString());
        hashTable.Add("EDD", txtEDD.Value);
        hashTable.Add("gestation", txtGestation.Text);
        hashTable.Add("PMTCT", PMTCT.ToString());
        hashTable.Add("PMTCTANCNumber", txtPMTCTANCNumber.Text);
        hashTable.Add("deliveryDate", txtDeliveryDate.Text);




        //Family Planning
        hashTable.Add("familyPlanningStatus", ddlFamilyPanningStatus.SelectedValue);
        //Table 0-Family Planning Methods
        DataTable dataTableFamilyPlanningMethods = new DataTable();
        DataColumn dataColumnFamilyPlanningMethods = new DataColumn("familyPlanningMethodID");
        dataColumnFamilyPlanningMethods.DataType = System.Type.GetType("System.Int32");
        dataTableFamilyPlanningMethods.Columns.Add(dataColumnFamilyPlanningMethods);
        DataRow dataRowFamilyPlanningMethods;

        if (ddlFamilyPanningStatus.SelectedItem.Text.ToUpper() == "ONFP=ON FAMILY PLANNING")
        {
            dataTableFamilyPlanningMethods = GetCheckBoxListcheckedIDs(PnlFamilyPlanningMethod, "familyPlanningMethodID", "", 0);

        }
        dataSetForSaving.Tables.Add(dataTableFamilyPlanningMethods);

        //MUAC
        hashTable.Add("MUAC", txtMUAC.Text);

        //TB Rx
        hashTable.Add("TBStatus", ddlTBStatus.SelectedValue);
        hashTable.Add("TBRxStart", txtTBRxStart.Value);
        hashTable.Add("TBRxStop", txtTBRxStop.Value);
        hashTable.Add("TBRegNumber", txtTBRegNumber.Value);

        //Subsitutions/Interruption
        hashTable.Add("TherapyPlan", lstclinPlanFU.SelectedValue);


        string OtherReason = "";
        // int ReasonID;

        string ARVTherapyCode = "0";
        string artendeddate = "";
        if (lstclinPlanFU.SelectedValue == "98")
        {
            ARVTherapyCode = ddlArvTherapyChangeCode.SelectedValue;
            if (ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
            {
                OtherReason = txtarvTherapyChangeCodeOtherName.Value;
            }
        }
        else if (lstclinPlanFU.SelectedValue == "99")
        {

            ARVTherapyCode = ddlArvTherapyStopCode.SelectedValue;
            artendeddate = txtARTEndeddate.Value;
            if (ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other"))
            {
                OtherReason = txtarvTherapyStopCodeOtherName.Value;
            }

        }
        if (lstclinPlanFU.SelectedValue.ToString() == "96")
        {
            Session["ARTEndedStatus"] = "";
        }
        else if (lstclinPlanFU.SelectedValue.ToString() == "99")
        {
            Session["ARTEndedStatus"] = "ART Stopped";
        }
        hashTable.Add("PrescribedARVStartDate", artendeddate);
        hashTable.Add("TherapyReasonCode", ARVTherapyCode);
        hashTable.Add("TherapyOther", OtherReason);


        DataTable dataTablePotentialSideEffect = new DataTable();
        dataTablePotentialSideEffect = GetCheckBoxListcheckedIDs(PnlPotentialSideEffect, "potentialSideEffectID", "potentialSideEffect_Other", 0);
        dataSetForSaving.Tables.Add(dataTablePotentialSideEffect);



        DataTable dataTableNewOIsProblems = new DataTable();
        dataTableNewOIsProblems = GetCheckBoxListcheckedIDs(PnlNewOIsProblemsOther, "newOIsProblemID", "newOIsProblemID_Other", 0);
        dataSetForSaving.Tables.Add(dataTableNewOIsProblems);

        //Nutritional Problems
        hashTable.Add("nutritionalProblem", ddlNutritionalProblems.SelectedValue);
        //WAB Stage
        hashTable.Add("WABStage", ddlWABStage.SelectedValue);
        //WHO Stage
        hashTable.Add("WHOStage", ddlWHOStage.SelectedValue);
        //CPT Adhere
        hashTable.Add("CPTAdhere", ddlCPTAdhere.SelectedValue);
        //ARV Drugs Adhere
        hashTable.Add("ARVDrugsAdhere", ddlARVDrugsAdhere.SelectedValue);
        //Reason why ARV Drugs are poor or fair
        hashTable.Add("reasonARVDrugsPoorFair", ddlReasonARVDrugsPoorFair.SelectedValue);
        //Other Reason why ARV Drugs are poor or fair
        hashTable.Add("reasonARVDrugsPoorFairOther", txtReasonARVDrugsPoorFairOther.Text);
        //Referred To
        DataTable dataTableReferredTo = new DataTable();
        dataTableReferredTo = GetCheckBoxListcheckedIDs(PnlReferredTo, "referredToID", "referredToOtherID_Other", 0);
        dataSetForSaving.Tables.Add(dataTableReferredTo);


        //If Hospitalized # of Days
        hashTable.Add("numOfDaysHospitalized", txtNumOfDaysHospitalized.Text);
        //Nutritional Support
        hashTable.Add("nutritionalSupport", ddlNutritionalSupport.SelectedValue);
        //Infant Feeding Option
        hashTable.Add("infantFeedingOption", ddlInfantFeedingPractice.SelectedValue);
        //Name of attending clinician
        hashTable.Add("attendingClinician", ddlAttendingClinician.SelectedValue);

        //CreateDate
        if (isUpdate)
        {
            hashTable.Add("createDate", Session["visitdatenewhiv"].ToString());
            hashTable.Add("visitID", Convert.ToInt32(Session["PatientVisitIdhiv"]));
        }


        DataTable theCustomDataDT = null;
        if ((Convert.ToInt32(Session["PatientVisitIdhiv"]) > 0))
        {
            //   visitPK = Convert.ToInt32(Session["PatientVisitId"]);

            CustomFieldClinical theCustomManager = new CustomFieldClinical();
            theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);

        }
        else
        {
            CustomFieldClinical theCustomManager = new CustomFieldClinical();
            theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);

        }




        DataSet dsreturn = HivCareARTEncounterInterface.SaveUpdateHIVCareARTPatientVisit(hashTable, dataSetForSaving, isUpdate, theCustomDataDT);
        Session["PatientVisitIdhiv"] = Convert.ToInt32(dsreturn.Tables[0].Rows[0]["visitID"].ToString());
        ViewState["VisitID"] = Session["PatientVisitIdhiv"];
        Session["visitdatenewhiv"] = Convert.ToDateTime(dsreturn.Tables[0].Rows[0]["VisitDate"].ToString());
        ViewState["VisitDate"] = Convert.ToDateTime(dsreturn.Tables[0].Rows[0]["VisitDate"].ToString());
        Session["ServiceLocationId"] = Convert.ToInt32(dsreturn.Tables[0].Rows[0]["LocationID"].ToString());

        hashTable.Clear();
        string script = string.Empty;

        if (button == "Pharmacy")
        {
            script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
            script += "fnPageOpen('Pharmacy');\n";
            script += "</script>\n";
            RegisterStartupScript("pharmacy", script);
        }
        else if (button == "Labratory")
        {
            script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
            script += "fnPageOpen('Labratory');\n";
            script += "</script>\n";
            RegisterStartupScript("pharmacy", script);
        }
        else if (button == "LabTest")
        {
            script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
            script += "fnPageOpen('LabTest');\n";
            script += "</script>\n";
            RegisterStartupScript("pharmacy", script);
        }
    }
    #region UI Control Event Handlers

    //protected void rdoPregnancy_CheckedChanged(Object sender, EventArgs e)
    //{
    //    if (rdoPregnantYes.Checked == true)
    //    {
    //        tdEDD.Style.Add("display", "inline-table");
    //        tdEDDDataField.Style.Add("display", "inline-table");
    //        tdGestation.Style.Add("display", "inline-table");
    //        tdGestationDataField.Style.Add("display", "inline-table");
    //        tdPMTCT.Style.Add("display", "inline-table");
    //        tdDeliveryDate.Style.Add("display", "none");
    //        tdDeliveryDateDataField.Style.Add("display", "none");            
    //        if (cbPMTCT.Checked)
    //            tdPMTCTANCNum.Style.Add("display", "inline-table");
    //        else
    //            tdPMTCTANCNum.Style.Add("display", "none");
    //    }
    //    else
    //    {
    //        tdEDD.Style.Add("display", "none");
    //        tdEDDDataField.Style.Add("display", "none");
    //        tdGestation.Style.Add("display", "none");
    //        tdGestationDataField.Style.Add("display", "none");
    //        tdPMTCT.Style.Add("display", "none");                           
    //        tdPMTCTANCNum.Style.Add("display", "none");
    //        tdDeliveryDate.Style.Add("display", "inline-table");
    //        tdDeliveryDateDataField.Style.Add("display", "inline-table"); 
    //    }
    //}
    //protected void cbPMTCT_CheckedChanged(Object sender, EventArgs e)
    //{
    //    if (cbPMTCT.Checked)
    //        tdPMTCTANCNum.Style.Add("display", "inline-table");
    //    else
    //        tdPMTCTANCNum.Style.Add("display", "none");
    //}
    //////protected void ddlTBStatus_SelectedIndexChanged(Object sender, EventArgs e)
    //////{
    //////    if (ddlTBStatus.SelectedItem.Text.Trim() == "TB Rx")
    //////    {
    //////        tdTBRxStart.Style.Add("display", "inline");
    //////        tdTBRxStop.Style.Add("display", "inline");
    //////        tdTBRegNumber.Style.Add("display", "inline");
    //////    }
    //////    else
    //////    {
    //////        tdTBRxStart.Style.Add("display", "none");
    //////        tdTBRxStop.Style.Add("display", "none");
    //////        tdTBRegNumber.Style.Add("display", "none");
    //////    }
    //////}
    ////protected void cblPotentialSideEffect_SelectedIndexChanged(Object sender, EventArgs e)
    ////{
    ////    //To be modified -> Other
    ////    if (cblPotentialSideEffect.Items.FindByText("Other") != null && cblPotentialSideEffect.Items.FindByText("Other").Selected)
    ////    {
    ////        lblPotentialSideEffectOther.Style.Add("display", "inline");
    ////        txtPotentialSideEffectOther.Visible = true;
    ////        txtPotentialSideEffectOther.Focus();
    ////    }
    ////    else
    ////    {
    ////        lblPotentialSideEffectOther.Style.Add("display", "none");
    ////        txtPotentialSideEffectOther.Visible = false;
    ////        cblPotentialSideEffect.Focus();
    ////    }

    ////}
    //private int PotentialSideEffect_Count()
    //{
    //    int count = 0;
    //    foreach (HtmlTableRow tr in tblPotentialSideEffect.Rows)
    //    {
    //        foreach (HtmlTableCell tc in tr.Cells)
    //        {
    //            foreach (Control ct in tc.Controls)
    //            {
    //                if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
    //                {
    //                    if (((HtmlInputCheckBox)ct).Checked == true)
    //                    {
    //                        count++;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return count;
    //}
    //private int NewOIsProblemsOther_Count()
    //{
    //    int count = 0;
    //    foreach (HtmlTableRow tr in tblNewOIsProblemsOther.Rows)
    //    {
    //        foreach (HtmlTableCell tc in tr.Cells)
    //        {
    //            foreach (Control ct in tc.Controls)
    //            {
    //                if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
    //                {
    //                    if (((HtmlInputCheckBox)ct).Checked == true)
    //                    {
    //                        count++;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return count;
    //}


    ////protected void cblNewOIsProblems_SelectedIndexChanged(Object sender, EventArgs e)
    ////{
    ////    if (cblNewOIsProblems.Items.FindByText("Other").Selected)
    ////    {
    ////        tcNewOIsProblemsOther.Style.Remove("visibility");
    ////        tcNewOIsProblemsOther.Focus();
    ////    }
    ////    else
    ////    {
    ////        tcNewOIsProblemsOther.Style.Add("visibility", "hidden");
    ////        cblNewOIsProblems.Focus();
    ////    }
    ////}
    //protected void ddlARVDrugsAdhere_SelectedIndexChanged(Object sender, EventArgs e)
    //{
    //    //To Be Modified
    //    if (ddlARVDrugsAdhere.SelectedItem.Text == "F=Fair" || ddlARVDrugsAdhere.SelectedItem.Text == "P=Poor")
    //   // if (ddlARVDrugsAdhere.SelectedIndex>0)
    //    {
    //        tdWhyPoorFair.Style.Add("display", "table-cell");
    //        if (ddlReasonARVDrugsPoorFair.SelectedItem.Text == "Other")
    //            tdReasonARVDrugsPoorFairOther.Style.Add("display", "table-cell");
    //        else
    //            tdReasonARVDrugsPoorFairOther.Style.Add("display", "none");
    //    }
    //    else
    //    {
    //        tdWhyPoorFair.Style.Add("display", "none");
    //        tdReasonARVDrugsPoorFairOther.Style.Add("display", "none");
    //        tdReasonARVDrugsPoorFairOther.Style.Add("display", "none");
    //    }
    //}
    //protected void ddlReasonARVDrugsPoorFair_SelectedIndexChanged(Object sender, EventArgs e)
    //{        
    //    if (ddlReasonARVDrugsPoorFair.SelectedItem.Text == "Other")
    //        tdReasonARVDrugsPoorFairOther.Style.Add("display", "table-cell");
    //    else
    //        tdReasonARVDrugsPoorFairOther.Style.Add("display", "none");
    //}
    ////protected void ddlReferredTo_SelectedIndexChanged(Object sender, EventArgs e)
    ////{
    ////    if (ddlReferredTo.SelectedItem.Text == "Other (specify)")
    ////    {
    ////        lblReferredToOther.Style.Remove("display");
    ////        txtReferredToOther.Style.Remove("display");

    ////    }
    ////    else
    ////    {
    ////        lblReferredToOther.Style.Add("display", "none");
    ////        txtReferredToOther.Style.Add("display", "none");
    ////    }
    ////}
    private void closeWindow()
    {
        Session["ArtEncounterPatientVisitId"] = 0;
        IQCareMsgBox.NotifyAction("HIV Care ARTCard Encounter saved successfully. Do you want to close?", "HIV Care ARTCard Encounter", false, this, "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';");
        //string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        //script += "var ans;\n";
        //script += "ans=window.confirm('Form saved successfully. Do you want to close?');\n";
        //script += "if (ans==true)\n";
        //script += "{\n";
        //script += "window.location.href='frmPatient_Home.aspx';\n";
        //script += "}\n";
        //script += "else \n";
        //script += "{\n";
        //script += "window.location.href='frmClinical_HIVCareARTCardEncounter.aspx';\n";
        //script += "}\n";
        //script += "</script>\n";
        //RegisterStartupScript("confirm", script);
    }
    protected void btnPrescribeDrugs_Click(Object sender, EventArgs e)
    {
    }

    protected void btnSave_Click(Object sender, EventArgs e)
    {
        if (Request.QueryString["name"] == "Delete")
        {
            DeleteForm();
        }

        save(false);
    }
    protected void btnDataQualityCheck_Click(Object sender, EventArgs e)
    {
        if (dataQualityCheck())
        {
            //ViewState["dataQuality"] = "1";
            btnDataQualityCheck.CssClass = "greenButton";
            save(true);
        }
        else
        {
            //ViewState["dataQuality"] = "0";
            btnDataQualityCheck.CssClass = "";
        }
    }
    private void DeleteForm()
    {
        Session["ArtEncounterPatientVisitId"] = 0;
        IHivCareARTEncounter CareARTEncounter;
        int theResultRow, OrderNo;
        string FormName;
        OrderNo = Convert.ToInt32(Session["PatientVisitIdhiv"].ToString());
        FormName = "HIV Care/ART Encounter";

        CareARTEncounter = (IHivCareARTEncounter)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BHivCareARTEncounter, BusinessProcess.Clinical");
        theResultRow = (int)CareARTEncounter.DeleteHIVCareEncounterForms(FormName, OrderNo, Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["AppUserId"].ToString()));
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
    protected void btnClose_Click(Object sender, EventArgs e)
    {
        //Session["PatientVisitId"]
        if (Request.QueryString["name"] == "Add" && Convert.ToInt32(ViewState["VisitID"]) > 0)
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
        Session["ArtEncounterPatientVisitId"] = 0;
    }
    //protected void btnPrint_Click(Object sender, EventArgs e)
    //{
    //}

    private void Maintain_ViewState(string ArvTherapyChangeCode, string ArvTherapyStopCode, int clinPlanFU, string ARVDrugsAdhereFairPoor, string ReasonARVDrugsPoorFair, string FamilyPanningStatus, string TBStatus)
    {
        if (clinPlanFU == 98) ViewState["ChangeRegimen"] = "1";

        if (clinPlanFU == 99) ViewState["StopRegimen"] = "1";

        if (ArvTherapyChangeCode == "Other") ViewState["changeROther"] = "1";

        if (ArvTherapyStopCode == "Other") ViewState["stopROther"] = "1";

        if ((ARVDrugsAdhereFairPoor == "F=Fair") || (ARVDrugsAdhereFairPoor == "P=Poor")) ViewState["ARVDrugsAdhereFairPoor"] = "1";

        if (ReasonARVDrugsPoorFair == "Other") ViewState["ReasonARVDrugsPoorFair"] = "1";

        if (FamilyPanningStatus.ToUpper() == "ONFP=ON FAMILY PLANNING") ViewState["FamilyPanningStatus"] = "1";

        if (TBStatus == "TB Rx") ViewState["TBStatusTBRx"] = "1";

    }
    private void Show_Hide()
    {
        string script;
        if (ViewState["ChangeRegimen"] == "1")
        {
            string scriptChangeRegimen = "<script language = 'javascript' defer ='defer' id = 'onChangeRegimen'>\n";
            scriptChangeRegimen += "show('arvTherapyChange'); \n";
            scriptChangeRegimen += "</script>\n";
            RegisterStartupScript("onChangeRegimen", scriptChangeRegimen);

            if (ViewState["changeROther"] == "1")
            {
                string scriptchangeROther = "<script language = 'javascript' defer ='defer' id = 'onchangeROther'>\n";
                scriptchangeROther += "show('otherarvTherapyChangeCode'); \n";
                scriptchangeROther += "</script>\n";
                RegisterStartupScript("onchangeROther", scriptchangeROther);
            }

        }
        if (ViewState["StopRegimen"] == "1")
        {
            string scriptStopRegimen = "<script language = 'javascript' defer ='defer' id = 'onstopRegimen'>\n";
            scriptStopRegimen += "show('arvTherapyStop'); \n";
            scriptStopRegimen += "</script>\n";
            RegisterStartupScript("onstopRegimen", scriptStopRegimen);

            if (ViewState["stopROther"] == "1")
            {
                string scriptstopROther = "<script language = 'javascript' defer ='defer' id = 'onstopROther'>\n";
                scriptstopROther += "show('otherarvTherapyStopCode'); \n";
                scriptstopROther += "</script>\n";
                RegisterStartupScript("onstopROther", scriptstopROther);
            }
        }
        if (ViewState["ARVDrugsAdhereFairPoor"] == "1")
        {
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsAdhere_00'>\n";
            script += "HIVcareArtEnableARVDrugsPoor('" + ddlReasonARVDrugsPoorFair.ClientID + "','0');\n";
            script += "</script>\n";
            RegisterStartupScript("ARVDrugsAdhere_00", script);
        }
        else
        {
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsAdhere_11'>\n";
            // script += "DisableDIVallControl('divARVDrugsPoorFair');\n";
            script += "HIVcareArtDisableARVDrugsPoor('" + ddlReasonARVDrugsPoorFair.ClientID + "''0');\n";
            script += "</script>\n";
            RegisterStartupScript("ARVDrugsAdhere_11", script);
        }

        if (ViewState["ReasonARVDrugsPoorFair"] == "1")
        {
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsPoorFairOther_00'>\n";
            script += "show('divReasonARVDrugsother');\n";
            script += "</script>\n";
            RegisterStartupScript("ARVDrugsPoorFairOther_00", script);
        }

        if (Convert.ToInt32(ViewState["Pregstatus"]) == 1)
        {
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes_11'>\n";
            script += "show('rdopregnantyesno');\n";
            script += "hide('spanEDDNopregnant');\n";
            script += "show('spanEDD');\n";
            script += "</script>\n";
            RegisterStartupScript("PregnantYes_11", script);
        }

        if (Convert.ToInt32(ViewState["Pregstatus"]) == 2)
        {
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'PregnantNo_00'>\n";
            script += "show('rdopregnantyesno');\n";
            script += "show('spanEDDNopregnant');\n";
            script += "</script>\n";
            RegisterStartupScript("PregnantNo_00", script);
        }
        if (rdoPregnantYes.Checked)
        {
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes_11'>\n";
            script += "show('rdopregnantyesno');\n";
            script += "hide('spanEDDNopregnant');\n";
            script += "show('spanEDD');\n";
            script += "</script>\n";
            RegisterStartupScript("PregnantYes_11", script);
        }

        if (rdoPregnantNo.Checked)
        {
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'PregnantNo_00'>\n";
            script += "show('rdopregnantyesno');\n";
            script += "show('spanEDDNopregnant');\n";
            script += "</script>\n";
            RegisterStartupScript("PregnantNo_00", script);
        }
        if (rdoPMTCT.Checked)
        {
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'PregnantPMTCT_00'>\n";
            script += "show('spanpmctcancno');\n";

            script += "</script>\n";
            RegisterStartupScript("PregnantPMTCT_00", script);
        }

        if (Convert.ToInt32(ViewState["FamilyPanningStatus"]) == 1)
        {
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'FamilyPlanning_00'>\n";
            script += "show('divFamilyPlanningMethod');\n";
            script += "</script>\n";
            RegisterStartupScript("FamilyPlanning_00", script);
        }
        if (Convert.ToInt32(ViewState["TBStatusTBRx"]) == 1)
        {
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'TBStatusRX_00'>\n";
            script += "show('divTBStatusTBRX');\n";
            script += "</script>\n";
            RegisterStartupScript("TBStatusRX_00", script);
        }
        if (Convert.ToInt32(ViewState["ReferredToOther"]) == 1)
        {
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'ReferredToOther_00'>\n";
            script += "show('spanotherReferTo');\n";
            script += "</script>\n";
            RegisterStartupScript("ReferredToOther_00", script);

        }
        if (Convert.ToInt32(ViewState["potentialSideEffecOther"]) == 1)
        {
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'potentialSideEffect_00'>\n";
            script += "show('otherPotentialSideEffect');\n";
            script += "</script>\n";
            RegisterStartupScript("potentialSideEffect_00", script);

        }
        if (Convert.ToInt32(ViewState["OIsProblemOther"]) == 1)
        {
            script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'OIsProblemOther_00'>\n";
            script += "show('otherchkNewOIsProblems');\n";
            script += "</script>\n";
            RegisterStartupScript("OIsProblemOther_00", script);
        }

    }

    #endregion


    private void PutCustomControl()
    {
        ICustomFields CustomFields;
        CustomFieldClinical theCustomField = new CustomFieldClinical();
        try
        {

            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
            DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.HIVCareARTEncounter));
            if (theDS.Tables[0].Rows.Count != 0)
            {
                theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "HCACounter");
                //ViewState["CustomFieldsDS"] = theDS;
                //pnlCustomList.Visible = true;
            }
            //theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "HCACounter");
            ViewState["CustomFieldsDS"] = theDS;
            pnlCustomList.Visible = true;
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
        finally
        {
            CustomFields = null;
        }

    }

    private void UpdateCustomFieldsValues()
    {
        GenerateCustomFieldsValues(pnlCustomList);
        string sqlstr = string.Empty;
        PatID = Convert.ToInt32(Session["PatientId"]);
        string sqlselect;
        string strdelete;
        Int32 visitID = 0;
        DateTime visitdate = System.DateTime.Now;
        ICustomFields CustomFields;
        //  if (txtVisitDate.Value.ToString() != "")
        visitdate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy"));
        if (ViewState["VisitID_add"] != null)
            visitID = Convert.ToInt32(ViewState["VisitID_add"]);

        if (sbValues.ToString().Trim() != "")
        {
            if (ViewState["CustomFieldsData"] != null)
            {
                sbValues = sbValues.Remove(0, 1);
                sqlstr = "UPDATE dtl_CustomField_" + TableName.ToString().Replace("-", "_") + " SET ";
                sqlstr += sbValues.ToString() + " where Ptn_pk= " + PatID.ToString() + " and Visit_pk=" + visitID.ToString();
            }
            else
            {
                sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
                sqlstr += " VALUES(" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";
                ViewState["CustomFieldsData"] = 1;
            }


            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
                if (icount == -1)
                {
                    return;
                }
            }
            catch
            {
            }
            finally
            {
                CustomFields = null;
            }
        }
        if (strmultiselect.ToString() != "")
        {
            string[] FieldValues = strmultiselect.Split(new char[] { '^' });
            if (arl.Count != 0)
            {
                int p = 0;
                foreach (object obj in arl)
                {
                    sqlselect = "";
                    strdelete = "";
                    if (obj.ToString() != "")
                    {
                        try
                        {
                            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                            strdelete = "DELETE from [" + obj.ToString() + "] where ptn_pk= " + PatID.ToString() + " and LocationID=" + Session["AppLocationID"] + " and visit_pk=" + visitID;
                            icount = CustomFields.SaveCustomFieldValues(strdelete.ToString());

                            if (FieldValues[p].ToString() != "")
                            {
                                string[] mValues = FieldValues[p].Split(new char[] { ',' });

                                foreach (string str in mValues)
                                {
                                    if (str.ToString() != "")
                                    {
                                        string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
                                        Int32 ispos = Convert.ToInt32(strtab.Length);
                                        Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;

                                        sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,visit_pk,visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
                                        sqlselect += " VALUES (" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";



                                        icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
                                        if (icount == -1)
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                        finally
                        {
                            CustomFields = null;
                        }
                    }
                    p += 1;
                }
            }
        }
    }

    private void InsertCustomFieldsValues()
    {
        GenerateCustomFieldsValues(pnlCustomList);
        string sqlstr = string.Empty;
        string sqlselect;
        Int32 visitID = 0;
        DateTime visitdate = System.DateTime.Now;
        PatID = Convert.ToInt32(Session["PatientId"]);
        ICustomFields CustomFields;
        if (ViewState["VisitID_add"] != null)
            visitID = Convert.ToInt32(ViewState["VisitID_add"]);
        //  if (txtVisitDate.Value.ToString() != "")
        visitdate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy"));

        if (sbValues.ToString().Trim() != "")
        {
            sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
            sqlstr += " VALUES(" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";

            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
                if (icount == -1)
                {
                    return;
                }
            }
            catch
            {
            }
            finally
            {
                CustomFields = null;
            }
        }
        if (strmultiselect.ToString() != "")
        {
            string[] FieldValues = strmultiselect.Split(new char[] { '^' });
            if (arl.Count != 0)
            {
                int p = 0;
                foreach (object obj in arl)
                {
                    sqlselect = "";
                    if (obj.ToString() != "")
                    {
                        if (FieldValues[p].ToString() != "")
                        {
                            string[] mValues = FieldValues[p].Split(new char[] { ',' });
                            foreach (string str in mValues)
                            {
                                if (str.ToString() != "")
                                {
                                    string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
                                    Int32 ispos = Convert.ToInt32(strtab.Length);
                                    Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;
                                    sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,Visit_pk,Visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
                                    sqlselect += " VALUES (" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";
                                    try
                                    {
                                        CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                                        icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
                                        if (icount == -1)
                                        {
                                            return;
                                        }

                                    }
                                    catch
                                    {
                                    }
                                    finally
                                    {
                                        CustomFields = null;
                                    }
                                }
                            }
                        }
                    }
                    p += 1;
                }
            }
        }
    }

    private void GenerateCustomFieldsValues(Control Cntrl)
    {
        string pnlName = Cntrl.ID;
        sbValues = new StringBuilder();
        strmultiselect = string.Empty;
        string strfName = string.Empty;
        Boolean radioflag = false;

        Int32 stpos = 0;
        Int32 enpos = 0;
        if (ViewState["CustomFieldsData"] != null)
        {
            foreach (Control x in Cntrl.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                    {
                        strfName = pnlName.ToUpper() + "TXT";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = x.ID.Substring(stpos, enpos).ToString();
                        if (((TextBox)x).Text != "")
                        {
                            sbValues.Append(",[" + strfName + "] = '" + ((TextBox)x).Text.ToString() + "'");
                        }
                        else
                        {
                            sbValues.Append(",[" + strfName + "] = ' " + "'");
                        }
                    }
                    else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                    {
                        strfName = pnlName.ToUpper() + "NUM";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = x.ID.Substring(stpos, enpos).ToString();
                        if (((TextBox)x).Text != "")
                        {
                            sbValues.Append(",[" + strfName + "]=" + ((TextBox)x).Text.ToString());
                        }
                        else
                        {
                            sbValues.Append("," + strfName + "=Null");
                        }

                    }
                    else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                    {
                        strfName = pnlName.ToUpper() + "DT";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = x.ID.Substring(stpos, enpos).ToString();
                        if (((TextBox)x).Text != "")
                        {
                            sbValues.Append(",[" + strfName + "]='" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
                        }
                        else
                        {
                            sbValues.Append(",[" + strfName + "]=" + "Null");
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                {
                    if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                    {
                        radioflag = false;
                        strfName = pnlName.ToUpper() + "RADIO1";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = x.ID.Substring(stpos, enpos).ToString();
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            sbValues.Append(",[" + strfName + "]=" + "1");
                            radioflag = true;
                        }
                    }
                    if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                    {
                        strfName = pnlName.ToUpper() + "RADIO2";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = x.ID.Substring(stpos, enpos).ToString();
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            sbValues.Append(",[" + strfName + "]=" + "0");
                            radioflag = true;
                        }
                        if (radioflag == false)
                        {
                            sbValues.Append(",[" + strfName + "]=" + "Null");
                        }
                    }

                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                    {
                        strfName = pnlName.ToUpper() + "SELECTLIST";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = x.ID.Substring(stpos, enpos).ToString();
                        if (((DropDownList)x).SelectedValue != "0")
                        {
                            sbValues.Append(",[" + strfName + "] = " + ((DropDownList)x).SelectedValue.ToString() + " ");
                        }
                        else
                        {
                            sbValues.Append(",[" + strfName + "] =  " + "0");
                        }

                    }
                }

            }
        }

        if (ViewState["CustomFieldsData"] == null)
        {
            foreach (Control x in Cntrl.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                    {
                        if (((TextBox)x).Text != "")
                        {
                            sbValues.Append(",'" + ((TextBox)x).Text.ToString() + "'");
                        }
                        else
                        {
                            sbValues.Append(",' " + "'");
                        }
                    }
                    else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                    {
                        if (((TextBox)x).Text != "")
                        {
                            sbValues.Append("," + ((TextBox)x).Text.ToString());
                        }
                        else
                        {
                            sbValues.Append(",0");
                        }

                    }
                    else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                    {
                        if (((TextBox)x).Text != "")
                        {
                            sbValues.Append(",'" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
                        }
                        else
                        {
                            sbValues.Append(",Null");
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                {
                    if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                    {
                        radioflag = false;
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            sbValues.Append(",1");
                            radioflag = true;
                        }
                    }
                    if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                    {
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            sbValues.Append(",0");
                            radioflag = true;
                        }
                        if (radioflag == false)
                        {
                            sbValues.Append(",Null");
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                    {
                        if (((DropDownList)x).SelectedValue != "0")
                        {
                            sbValues.Append("," + ((DropDownList)x).SelectedValue.ToString() + " ");
                        }
                        else
                        {
                            sbValues.Append(", " + "0");
                        }

                    }
                }
            }
        }
        if (ViewState["CustomFieldsMulti"] != null || ViewState["CustomFieldsMulti"] == null)
        {
            foreach (Control x in Cntrl.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
                {

                    if (x.ID.Substring(0, 28).ToString().ToUpper() == pnlName.ToUpper() + "MULTISELECTLIST")
                    {

                        foreach (ListItem li in ((CheckBoxList)x).Items)
                        {
                            if (Convert.ToInt32(li.Selected) == 1)
                            {
                                strmultiselect += " " + li.Value.ToString() + ",";
                            }
                        }
                        strmultiselect += "^";
                    }
                }
            }
        }
    }

    private void FillOldData(Int32 PatID)
    {
        DataSet dsvalues = null;
        ICustomFields CustomFields;

        try
        {
            DataSet theCustomFields = (DataSet)ViewState["CustomFieldsDS"];
            string theTblName = "";
            if (theCustomFields.Tables[0].Rows.Count > 0)
                theTblName = theCustomFields.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");
            string theColName = "";
            foreach (DataRow theDR in theCustomFields.Tables[0].Rows)
            {
                if (theDR["ControlId"].ToString() != "9")
                {
                    if (theColName == "")
                        theColName = theDR["Label"].ToString();
                    else
                        theColName = theColName + "," + theDR["Label"].ToString();
                }
            }

            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
            dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, visitID, 0, 0, Convert.ToInt32(ApplicationAccess.HIVCareARTEncounter));
            CustomFieldClinical theCustomManager = new CustomFieldClinical();
            theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "HCACounter");
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
        finally
        {
            CustomFields = null;
        }
    }

    protected void btnpharmacy_Click(object sender, EventArgs e)
    {
        try
        {
            save(false, "Pharmacy");
        }
        finally { }
    }
    protected void btnLabratory_Click(object sender, EventArgs e)
    {
        try
        {
            save(false, "Labratory");
        }
        finally { }
    }
    protected void btnOrderLabTest_Click(Object sender, EventArgs e)
    {
        try
        {
            save(false, "LabTest");
        }
        finally { }
    }
}
