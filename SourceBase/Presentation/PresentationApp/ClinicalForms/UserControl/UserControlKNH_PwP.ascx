<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_PwP.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_PwP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Src="UserControlKNH_NextAppointment.ascx" TagName="UserControlKNH_NextAppointment"
    TagPrefix="uc11" %>
<%@ Register Src="UserControlKNH_Signature.ascx" TagName="UserControlKNH_Signature"
    TagPrefix="uc1" %>
<script type="text/javascript" language="javascript">
    //        $(function () {

    //            $("#tabs").tabs();

    //        });
    function ShowHide(theDiv, YN, theFocus) {

        $(document).ready(function () {

            if (YN == "show") {
                //                    $("#" + theDiv).slideDown();
                $("#" + theDiv).show();

            }
            if (YN == "hide") {
                //                    $("#" + theDiv).slideUp();
                $("#" + theDiv).hide();


            }

        });

    }

    function ShowMore(sender, eventArgs) {
        var substr = eventArgs._commandArgument.toString().split('|')
        ShowHide(substr[0], substr[1]);
    }

    function rblSelectedValue(rbl, divID) {
        var selectedvalue = $("#" + rbl.id + " input:radio:checked").val();
        if (selectedvalue == "1") {
            YN = "show";
        }
        else {
            YN = "hide";
        }
        ShowHide(divID, YN);
    }

    function rblSelectedValueNo(rbl, divID) {
        var selectedvalue = $("#" + rbl.id + " input:radio:checked").val();
        if (selectedvalue == "0") {
            YN = "show";
        }
        else {
            YN = "hide";
        }
        ShowHide(divID, YN);
    }
    //Checkbox list
    function getCheckBoxValue(DivId, chktext, str) {
        var YN = "";
        var id = "#" + CheckBoxID;
        if ($(id).is(':checked')) {
            YN = "show";
        }
        else {
            YN = "hide";
        }
        ShowHide(CheckBoxID, YN);
    }
    //DropDown list
    function getSelectedValue(DivId, DDText, str) {
        var e = document.getElementById(DDText);
        var text = e.options[e.selectedIndex].innerText;
        var YN = "";
        if (text == str) {
            YN = "show";
        }
        else {
            YN = "hide";
        }
        ShowHide(DivId, YN);
    }
    function CheckBoxToggleShowHideKNHAdultIE(divID, val, txt) {
        var checkList = document.getElementById(val);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();
        for (var i = 0; i < checkBoxList.length; i++) {

            if (checkBoxList[i].checked) {
                if (arrayOfCheckBoxLabels[i].innerText.trim() == txt) {
                    ShowHide(divID, "show");
                    break;
                }
            }
            else {
                if (arrayOfCheckBoxLabels[i].innerText == txt) {
                    ShowHide(divID, "hide");
                }

            }
        }

    }
    function WindowPrint() {
        window.print();
    }

    function CheckBoxHideUnhideOtherSpecialistClinic(strcblcontrolId, txtControlId) {

        var checkList = document.getElementById(strcblcontrolId);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();

        for (var i = 0; i < checkBoxList.length; i++) {

            if (checkBoxList[i].checked) {
                if (arrayOfCheckBoxLabels[i].innerHTML == 'Other Specialist Clinic') {
                    //alert(arrayOfCheckBoxLabels[i].innerHTML + ' show ' + strdivId);
                    ShowHide('divReferToSpecialistClinic', "show");
                    return;
                }
                else {
                    //alert(arrayOfCheckBoxLabels[i].innerHTML + ' hide ' + strdivId);
                    document.getElementById(txtControlId).value = '';
                    ShowHide('divReferToSpecialistClinic', "hide");
                }
            }
            else {
                //alert(arrayOfCheckBoxLabels[i].innerHTML + ' hide ' + strdivId);
                document.getElementById(txtControlId).value = '';
                ShowHide('divReferToSpecialistClinic', "hide");
            }
        }
    }

    function CheckBoxHideUnhideOtherReferral(strcblcontrolId, txtControlId) {

        var checkList = document.getElementById(strcblcontrolId);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();

        for (var i = 0; i < checkBoxList.length; i++) {

            if (checkBoxList[i].checked) {
                if (arrayOfCheckBoxLabels[i].innerHTML == 'Other (Specify)') {
                    //alert(arrayOfCheckBoxLabels[i].innerHTML + ' show ' + strdivId);
                    ShowHide('divReferToOther', "show");
                    return;
                }
                else {
                    //alert(arrayOfCheckBoxLabels[i].innerHTML + ' hide ' + strdivId);
                    document.getElementById(txtControlId).value = '';
                    ShowHide('divReferToOther', "hide");
                }
            }
            else {
                //alert(arrayOfCheckBoxLabels[i].innerHTML + ' hide ' + strdivId);
                document.getElementById(txtControlId).value = '';
                ShowHide('divReferToOther', "hide");
            }
        }
    }

    function CheckBoxHideUnhideOtherCounselling(strcblcontrolId, txtControlId) {

        var checkList = document.getElementById(strcblcontrolId);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();

        for (var i = 0; i < checkBoxList.length; i++) {

            if (checkBoxList[i].checked) {
                if (arrayOfCheckBoxLabels[i].innerHTML == 'Other') {
                    //alert(arrayOfCheckBoxLabels[i].innerHTML + ' show ' + strdivId);
                    ShowHide('divOtherCounselling', "show");
                    return;
                }
                else {
                    document.getElementById(txtControlId).value = '';
                    ShowHide('divOtherCounselling', "hide");
                }
            }
            else {
                // alert(arrayOfCheckBoxLabels[i].innerHTML + ' hide ' + strdivId);
                document.getElementById(txtControlId).value = '';
                ShowHide('divOtherCounselling', "hide");
            }
        }
    }

    function CheckBoxHideUnhideDivCounselling(strcblcontrolId, ControlId) {

        var checkList = document.getElementById(strcblcontrolId);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();

        for (var i = 0; i < checkBoxList.length; i++) {

            if (checkBoxList[i].checked) {
                if (arrayOfCheckBoxLabels[i].innerHTML == 'Psychologist') {
                    //alert(arrayOfCheckBoxLabels[i].innerHTML + ' show ' + strdivId);
                    ShowHide('DivCounselling', "show");
                    return;
                }
                else {
                    ClearMultiSelect(ControlId);
                    ShowHide('DivCounselling', "hide");
                }
            }
            else {
                // alert(arrayOfCheckBoxLabels[i].innerHTML + ' hide ' + strdivId);
                ClearMultiSelect(ControlId);
                ShowHide('DivCounselling', "hide");
            }
        }
    }

    //        function ClearSelectList(controlID) {
    //            document.getElementById(controlID).selectedIndex = 0;
    //        }

    //        function ClearTextBox(controlID) {
    //            document.getElementById(controlID).value = "";
    //        }

    //        function ClearRadioButtons(RadioYes, RadioNo) {
    //            document.getElementById(RadioYes).checked = false;
    //            document.getElementById(RadioNo).checked = false;
    //        }

    //        function clearRadioButtonList(controlID) {

    //            var elementRef = document.getElementById(controlID);
    //            var inputElementArray = elementRef.getElementsByTagName('input');

    //            for (var i = 0; i < inputElementArray.length; i++) {
    //                var inputElement = inputElementArray[i];

    //                inputElement.checked = false;
    //            }
    //            return false;
    //        }
    ////        function ClearRadioButtonList(controlID) {
    ////            document.getElementById(controlID).selectedIndex = 0;
    ////        }

    //        function ClearMultiSelect(controlID) {
    //            var elementRef = document.getElementById(controlID);
    //            var checkBoxArray = elementRef.getElementsByTagName('input');
    //            for (var i = 0; i < checkBoxArray.length; i++) {
    //                checkBoxArray[i].checked = false;
    //            }
    //        }
    function ClearCheckBox(controlID) {
        document.getElementById(controlID).checked = false;
    }

    function clearvaluesSexuallyActive(mainControl, controlID1, controlID2, controlID3, controlID4) {
        var selectedvalue = $("#" + mainControl.id + " input:radio:checked").val();
        if (selectedvalue == "0") {
            ClearSelectList(controlID1);
            ClearMultiSelect(controlID2);
            clearRadioButtonList(controlID3);
            ClearSelectList(controlID4);
        }
    }

    function clearLMP(mainControl, controlID1, controlID2) {
        var selectedvalue = $("#" + mainControl.id + " input:radio:checked").val();
        if (selectedvalue == "1") {
            ClearSelectList(controlID1);

        }
        else if (selectedvalue == "0") {
            ClearTextBox(controlID2);
        }
    }

    function clearPTD(mainControl, controlID1, controlID2, controlID3) {
        var selectedvalue = $("#" + mainControl.id + " input:radio:checked").val();
        if (selectedvalue == "0") {
            clearRadioButtonList(controlID1);
            clearRadioButtonList(controlID2);
            ClearTextBox(controlID3);
            ShowHide('trIfYesPregnant', 'hide');
        }

    }

    function clearClientPreg(mainControl, controlID1, controlID2) {
        var selectedvalue = $("#" + mainControl.id + " input:radio:checked").val();
        if (selectedvalue == "0") {
            clearRadioButtonList(controlID1);
            ClearTextBox(controlID2);
        }

    }

    function clearPregInt(mainControl, controlID1, controlID2) {
        var selectedvalue = $("#" + mainControl.id + " input:radio:checked").val();
        if (selectedvalue == "0") {
            clearRadioButtonList(controlID1);
        }
        else if (selectedvalue == "1") {
            clearRadioButtonList(controlID2);
        }

    }

    function clearCondomsIssued(mainControl, controlID1) {
        var selectedvalue = $("#" + mainControl.id + " input:radio:checked").val();
        if (selectedvalue == "1") {
            ClearTextBox(controlID1);
        }
    }

    function clearSTIScreened(mainControl, controlID1, controlID2, controlID3, controlID4, controlID5) {
        var selectedvalue = $("#" + mainControl.id + " input:radio:checked").val();
        if (selectedvalue == "0") {
            ClearCheckBox(controlID1);
            ClearCheckBox(controlID2);
            ClearCheckBox(controlID3);
            ClearTextBox(controlID4);
            ClearTextBox(controlID5);
        }
    }

    function clearOnFP(mainControl, controlID1, controlID2) {
        var selectedvalue = $("#" + mainControl.id + " input:radio:checked").val();
        if (selectedvalue == "0") {
            ClearSelectList(controlID1);
        }
        else if (selectedvalue == "1") {
            ClearSelectList(controlID2);
        }

    }

    function clearCervicalCancer(mainControl, controlID1, controlID2) {
        var selectedvalue = $("#" + mainControl.id + " input:radio:checked").val();
        if (selectedvalue == "0") {
            ClearSelectList(controlID1);
        }
        else if (selectedvalue == "1") {
            clearRadioButtonList(controlID2);
        }

    }

    function clearHPV(mainControl, controlID1, controlID2) {
        var selectedvalue = $("#" + mainControl.id + " input:radio:checked").val();
        if (selectedvalue == "0") {
            ClearSelectList(controlID1);
            ClearTextBox(controlID2);
        }

    }

    function clearPatientReferred(mainControl, controlID1, controlID2, controlID3) {
        var selectedvalue = $("#" + mainControl.id + " input:radio:checked").val();
        if (selectedvalue == "0") {
            ClearSelectList(controlID1);
            ClearTextBox(controlID2);
        }

    }

    function fnCheckUncheckPwP(strcblcontrolId) {
        var checkList = document.getElementById(strcblcontrolId);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();

        if (checkBoxList[0].checked == true && arrayOfCheckBoxLabels[0].innerHTML == 'None') {
            for (var i = 1; i < checkBoxList.length; i++) {
                checkBoxList[i].checked = false;
                checkBoxList[i].disabled = true;
            }
        }
        else {
            checkBoxList[0].checked = false;
            for (var i = 1; i < checkBoxList.length; i++) {
                checkBoxList[i].disabled = false;
            }
        }
    }
</script>
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtLMPDate').datepicker({ autoclose: true });
        $('#txtEDD').datepicker({ autoclose: true });
        $('#dtHPVDoseDate').datepicker({ autoclose: true });
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
<div class="border center formbg">
    <table id="TabPrevWithPos" class="center formbg" cellspacing="6" cellpadding="0"
        width="100%" border="0">
        <tr>
            <td colspan="2" align="left">
                <asp:Panel ID="pnlSexAssessment" CssClass="border center formbg" runat="server" Style="padding: 6px">
                    <h5 class="forms" align="left">
                        <asp:ImageButton ID="imgSexAss" ImageUrl="~/images/arrow-up.gif" runat="server" />
                        <asp:Label ID="lblSexualityAssessment" runat="server" Text="Sexuality Assessment"></asp:Label>
                    </h5>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlSexAssessmentDetail" runat="server" Style="overflow:hidden;">
    <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">
             <asp:Label ID="lblSexuallyActiveLast6Months" runat="server" Text="Have you been sexually active in the past 6 months?:"></asp:Label></label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radbtnSexualActiveness" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="radbtnSexualActiveness_SelectedIndexChanged" CssClass="checkbox-btn">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
             </div>
             
 </div>
 <div class="row">
 <div id="DIVSexualOrientation" style="display: none; vertical-align:top;">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Sexual orientation:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="rcbSexualOrientation" runat="server" Width="250" CssClass="form-control"></asp:DropDownList>
             </div>
             </div>
             <div id="DIVSexualHighrisk" style="display: none; vertical-align:top;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Sexuality high risk factor:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div id="divHighRisk" class="customdivbordermultiselect">
                                        <asp:CheckBoxList ID="cblHighRisk" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                        </asp:CheckBoxList>
                                    </div>
             </div> 
             </div>
 </div>
 <div class="row">
 <div id="DIVDiscloseSexualPartner" style="display: none;vertical-align:top;">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">
             <asp:Label ID="lblDisclosedToSexualPartner" runat="server" Text="Have you disclosed your status to sexual partner:"></asp:Label>
             </label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radbtnDisclosedstatusToSexualPartner" runat="server" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="radbtnDisclosedstatusToSexualPartner_SelectedIndexChanged" CssClass="checkbox-btn">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
             </div>
</div>
<div id="DIVStatusSexualPartner" style="display: none;vertical-align:top;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">
             <asp:Label ID="lblStatusOfSexualPartner" runat="server" Text="HIV Status of sexual partner:"></asp:Label>
             </label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="rcbPartnerHIVStatus" runat="server" Width="170px" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
 </div>
        
    </asp:Panel>
    <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
        <tr>
            <td colspan="2" align="left">
                <asp:Panel ID="pnlPWP" CssClass="border center formbg" runat="server" Style="padding: 6px">
                    <h5 class="forms" align="left">
                        <asp:ImageButton ID="imgPWP" ImageUrl="~/images/arrow-up.gif" runat="server" />
                        PWP Interventions</h5>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlPWPDetail" runat="server" Style="overflow:hidden;">
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">PWP messages given:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radbtnGivenPWPMessages" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="radbtnGivenPWPMessages_SelectedIndexChanged" CssClass="checkbox-btn">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Importance of safe sex explained:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radbtnSaferSexImportanceExplained" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="radbtnSaferSexImportanceExplained_SelectedIndexChanged" CssClass="checkbox-btn">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
             </div> 
 </div>
 <div class="row" id="trLMP">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">LMP assessed:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="radbtnLMP" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="radbtnLMP_SelectedIndexChanged" CssClass="checkbox-btn">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
             </div>
             <div id="DIVLMPDate" style="display: none">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">LMP Date:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtLMPDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div>
             </div> 
              <div id="DIVMenopausal" style="display: none">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Reason LMP not assessed:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="DDLReasonLMP" runat="server" Width="160px" CssClass="form-control"></asp:DropDownList>
             </div>
             </div> 
 </div>
 <div class="row" id="trPregnancyTest">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Pregnancy test done:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radbtnPDTDone" runat="server" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="radbtnPDTDone_SelectedIndexChanged" CssClass="checkbox-btn">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
             </div>
             <div id="DIVClientPregnant" style="display: none">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">If yes, client pregnant?:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rblClientPregnant" runat="server" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rblClientPregnant_SelectedIndexChanged" CssClass="checkbox-btn">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
             </div> 
             </div>
 </div>
 <div class="row" id="trIfYesPregnant" style="display: none">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">If yes for pregnant, has patient been offered or referred to PMTCT?</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radbtnPMTCTOffered" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="radbtnPMTCTOffered_SelectedIndexChanged" CssClass="checkbox-btn">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">EDD:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtEDD" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             
             </div> 
 </div>
 <div class="row" id="trIntendPreg">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Patient or partner intend to become pregnant before next visit:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radbtnIntentionOfPregnancy" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="radbtnIntentionOfPregnancy_SelectedIndexChanged" CssClass="checkbox-btn">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
             </div>
              <div id="DIVdisfertility" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">If yes, discussed fertility options:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radbtnDiscussedFertilityOptions" runat="server" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="radbtnDiscussedFertilityOptions_SelectedIndexChanged" CssClass="checkbox-btn">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
             </div>
             </div> 
              <div id="DIVDiscusdualcontraception" style="display: none;">
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">If No discussed dual contraception:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radbtnDiscussedDualContraception" runat="server" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="radbtnDiscussedDualContraception_SelectedIndexChanged" CssClass="checkbox-btn">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
             </div>
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Condoms issued:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radbtnCondomsIssued" runat="server" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="radbtnCondomsIssued_SelectedIndexChanged" CssClass="checkbox-btn">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
             </div>
             <div id="DIVnotICon" style="display: none">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Reasons condoms not issued:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtCondomNotIssued" runat="server" Skin="MetroTouch" CssClass="form-control" Width="98%"></asp:TextBox>
             </div> 
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Screened for STI:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="radbtnSTIScreened" runat="server" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="radbtnSTIScreened_SelectedIndexChanged" CssClass="checkbox-btn">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
             </div>
            
 </div>

 <div class="row" id="DIVUrethral" style="display: none">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Urethral Discharge:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:CheckBox ID="chkUrethralDischarge" runat="server" OnCheckedChanged="chkUrethralDischarge_CheckedChanged" CssClass="checkbox-btn" />
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Vaginal Discharge:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:CheckBox ID="chkVaginalDischarge" runat="server" OnCheckedChanged="chkVaginalDischarge_CheckedChanged" CssClass="checkbox-btn" />
             </div> 
 </div>
 <div class="row" id="DIVGenUlceration" style="display: none">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Genital Ulceration:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:CheckBox ID="chkGenitalUlceration" runat="server" OnCheckedChanged="chkGenitalUlceration_CheckedChanged" CssClass="checkbox-btn"/>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">STI treatment plan:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtSTITreatmentPlan" runat="server" Skin="MetroTouch" Rows="3" TextMode="MultiLine" Width="98%" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div> 
 </div>
 <div class="row" id="STITreatmentOther" style="display: none">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="Label1" runat="server" Text="Other STI Treatment"></asp:Label></label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtOtherSTITreatment" runat="server" Rows="3" TextMode="MultiLine" Width="98%" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            
             </div> 
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Are you on any family planning method?:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radbtnOnFP" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="radbtnOnFP_SelectedIndexChanged" CssClass="checkbox-btn">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
             </div>
             <div id="DIVFPMethod" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify other FP method :</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="rcbFPMethod" runat="server" Width="210px" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
             <div id="DIVReferredFP" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">If no, referred for family planning?:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlReferredFP" runat="server" Width="160px" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>

 </div>
 <div class="row" id="trCervicalCancer">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Have you been screened for cervical cancer:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radbtnCervicalCancerScreened" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="radbtnCervicalCancerScreened_SelectedIndexChanged" CssClass="checkbox-btn">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
             </div>
             <div id="DIVCacervix" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">CaCervix screening results:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="rcbCervicalCancerScreeningResults" runat="server" Width="160px" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
             <div id="DIVRefCervCancer" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">If No, referred for cervical cancer screening:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="radbtnReferredForCervicalCancerScreening" runat="server"
                                                RepeatDirection="Horizontal" OnSelectedIndexChanged="radbtnReferredForCervicalCancerScreening_SelectedIndexChanged" CssClass="checkbox-btn">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
             </div>
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">HPV offered:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="radbtnHPVOffered" runat="server" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="radbtnHPVOffered_SelectedIndexChanged" CssClass="checkbox-btn">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
             </div>
             <div id="DIVHPVVaccine" style="display: none">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">HPV Vaccine Offered:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="rcbOfferedHPVVaccine" runat="server" Width="160px" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
             <div id="DIVHPVVaccineDate" style="display: none">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date of HPV vaccine:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtHPVDoseDate" clientidmode="Static"
                                                      onfocus="javascript:vDateType='3'" maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>            
             </div>
             </div>

 </div>
 <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Transition preparation (for 10 - 19 years old):</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
            <div class="customdivbordermultiselectAutoHeight" nowrap="noWrap">
                                    <asp:CheckBoxList ID="cblTransitionPreparation" runat="server" CssClass="checkbox-btn">
                                    </asp:CheckBoxList>
                                </div>
             </div>
             
 </div>
        
    </asp:Panel>
    <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
        <tr>
            <td colspan="2" align="left">
                <asp:Panel ID="pnlAppointmentsHeader" CssClass="border center formbg" runat="server"
                    Style="padding: 6px">
                    <h5 class="forms" align="left">
                        <asp:ImageButton ID="imgAppointments" ImageUrl="~/images/arrow-up.gif" runat="server" />
                        <asp:Label ID="lblReferralAndAppointments" runat="server" Text="Referral and Appointments"></asp:Label>
                    </h5>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlAppointmentsBody" runat="server" Style="overflow:hidden;">
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Ward Admission:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="rblWardAdmission" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rblWardAdmission_SelectedIndexChanged" CssClass="checkbox-btn">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
           
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
           
             </div> 
 </div>
 <div class="row">
  <div class="col-md-6">
  <div class="row">
 <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Patient referred to:</label>
             </div>            
 </div>
 <div class="row">
 <div class="col-md-10 col-sm-12 col-xs-12 form-group">
            <div class="customdivbordermultiselect" nowrap="noWrap">
                                                <asp:CheckBoxList ID="cblReferredTo" runat="server" CssClass="checkbox-btn">
                                                </asp:CheckBoxList>
                                            </div>
             </div>            
 </div>
 <div class="row" id="divReferToSpecialistClinic" style="display: none">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Refer to specialist clinic:</label>
             </div>   
              <div class="col-md-5 col-sm-12 col-xs-12 form-group">
              <asp:TextBox ID="txtReferToSpecialistClinic" runat="server" Width="60%" CssClass="form-control"></asp:TextBox>
              </div>         
 </div>

 <div id="divReferToOther" style="display: none">
 <div class="row">
 <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify other referral point:</label>
             </div> 
</div>
<div class="row">
      <div class="col-md-10 col-sm-12 col-xs-12 form-group">
      <asp:TextBox ID="txtSpecifyOtherRefferedTo" runat="server" Skin="MetroTouch" Width="55%" CssClass="form-control"></asp:TextBox>
      </div>      
        </div>   
 </div>
  <%--left--%>
  </div>
  <div class="col-md-6">
  <%--riht--%>
  <div class="row" id="DivCounselling" style="display: none">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Counselling:</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <div class="customdivbordermultiselect" nowrap="noWrap">
                                                <asp:CheckBoxList ID="cblCounselling" runat="server" CssClass="checkbox-btn">
                                                </asp:CheckBoxList>
                                            </div>
             </div>
            
 </div>
 <div id="divOtherCounselling" style="display: none">
 <div class="row">
 <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other:</label>
             </div>
             </div>
             <div class="row">
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtOtherCounselling" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
             </div>
           
 </div>
  </div>
 </div>
        
        <uc11:UserControlKNH_NextAppointment ID="UserControlKNH_NextAppointment1" runat="server" />
    </asp:Panel>
</div>
<act:CollapsiblePanelExtender ID="CPESexAssess" runat="server" SuppressPostBack="True"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlSexAssessmentDetail"
    CollapseControlID="pnlSexAssessment" ExpandControlID="pnlSexAssessment" CollapsedImage="~/images/arrow-up.gif"
    Collapsed="True" ImageControlID="imgSexAss" Enabled="True">
</act:CollapsiblePanelExtender>
<act:CollapsiblePanelExtender ID="CPEPWP" runat="server" SuppressPostBack="True"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlPWPDetail" CollapseControlID="pnlPWP"
    ExpandControlID="pnlPWP" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
    ImageControlID="imgPWP" Enabled="True">
</act:CollapsiblePanelExtender>
<act:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" SuppressPostBack="True"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlAppointmentsBody" CollapseControlID="pnlAppointmentsHeader"
    ExpandControlID="pnlAppointmentsHeader" CollapsedImage="~/images/arrow-up.gif"
    Collapsed="True" ImageControlID="imgAppointments" Enabled="True">
</act:CollapsiblePanelExtender>
<br />
<div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <uc1:UserControlKNH_Signature ID="UserControlKNH_SignaturePwP" runat="server" />
             </div>            
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"  Height="30px" Width="9%" Style="text-align:left;"/>
             <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                    <asp:Button ID="btnSubmitPositive" runat="server" Text="Data Quality Check" Visible="False" CssClass="btn btn-primary" Height="30px" Width="15%" Style="text-align:left;"/>
                    
                    <asp:Button ID="btnPwPClose" runat="server" OnClick="Button1_Click" Text="Close" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
                    <label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
                    <asp:Button ID="btnPrintPositive" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
                    <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
             </div>            
 </div>

