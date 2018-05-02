<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_TBScreening.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_TBScreening" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ Register Src="UserControlKNH_Signature.ascx" TagName="UserControlKNH_Signature"
    TagPrefix="uc1" %>
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtSputumSmearDate').datepicker({ autoclose: true });
        $('#txtGeneExpertDate').datepicker({ autoclose: true });
        $('#txtSputumDSTDate').datepicker({ autoclose: true });
        $('#txtChestXrayDate').datepicker({ autoclose: true });
        $('#txtTissueBiopsyDate').datepicker({ autoclose: true });
        $('#txtTBRegimenStartDate').datepicker({ autoclose: true });
        $('#txtTBRegimenEndDate').datepicker({ autoclose: true });
        $('#txtINHStartDate').datepicker({ autoclose: true });
        $('#txtINHEndDate').datepicker({ autoclose: true });
        $('#txtPyridoxineStartDate').datepicker({ autoclose: true });
        $('#txtPyridoxineEndDate').datepicker({ autoclose: true });
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
<script type="text/javascript">
    function show_hide(controlID, status) {
        var s = document.getElementById(controlID);
        if (status == "notvisible") {
            s.style.display = "none";
        }
        else {
            s.style.display = "block";
        }
    }

    function SelectOther(selectId, show_hide_control, otherControlID) {
        if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].text == "Other") {
            show_hide(show_hide_control, 'visible');
        }
        else {
            document.getElementById(otherControlID).value = "";
            show_hide(show_hide_control, 'notvisible');
        }
    }


    function SelectOtherSpecify(selectId, show_hide_control, otherControlID) {
        if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].text == "Other (Specify)") {
            show_hide(show_hide_control, 'visible');
        }
        else {
            document.getElementById(otherControlID).value = "";
            show_hide(show_hide_control, 'notvisible');
        }
    }

    function SelectCotrimoxazole(selectId, show_hide_control, otherControlID) {
        if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].text == "Cotrimoxazole") {
            show_hide(show_hide_control, 'visible');
        }
        else {
            document.getElementById(otherControlID).selectedIndex = 0;
            show_hide(show_hide_control, 'notvisible');
        }
    }


    function DisableNoSignsSymptoms(tbAssessment, tbFindings) {
        var tbAss = false;
        var TBAssessmentCtl = document.getElementById(tbAssessment);
        var cell;

        if (TBAssessmentCtl.rows.length > 0) {
            for (i = 0; i < TBAssessmentCtl.rows.length; i++) {
                cell = TBAssessmentCtl.rows[i].cells[0];
                for (j = 0; j < cell.childNodes.length; j++) {
                    if (cell.childNodes[j].type == "checkbox") {
                        if (cell.childNodes[j].checked == true) {
                            tbAss = true;
                        }
                    }
                }
            }
        }

        //
        var sel = document.getElementById(tbFindings);
        sel.options[1].disabled = false;

        if (tbAss == true) {
            sel.options[1].disabled = "disabled";
        }
    }

    //    function ClearSelectList(controlID) {
    //    try{
    //        document.getElementById(controlID).selectedIndex = 0;
    //        }
    //    catch (err) {
    //    }
    //    }

    //    function ClearTextBox(controlID) {
    //    try{
    //        document.getElementById(controlID).value = "";
    //        }
    //    catch (err) {
    //    }
    //    }

    //    function ClearRadioButtons(RadioYes, RadioNo) {
    //    try{
    //        document.getElementById(RadioYes).checked = false;
    //        document.getElementById(RadioNo).checked = false;
    //        }
    //        catch(err)
    //        {
    //        }
    //    }

    //    function ClearMultiSelect(controlID) {
    //    try{
    //        var elementRef = document.getElementById(controlID);
    //        var checkBoxArray = elementRef.getElementsByTagName('input');
    //        for (var i = 0; i < checkBoxArray.length; i++) {
    //            checkBoxArray[i].checked = false;
    //        }
    //        }
    //    catch (err) {
    //    }
    //    }

    //    function clearRadioButtonList(controlID) {
    //    try{
    //        var elementRef = document.getElementById(controlID);
    //        var inputElementArray = elementRef.getElementsByTagName('input');

    //        for (var i = 0; i < inputElementArray.length; i++) {
    //            var inputElement = inputElementArray[i];

    //            inputElement.checked = false;
    //        }
    //        return false;
    //        }
    //    catch (err) {
    //    }
    //    }

    function SelectOtherReviewChkList(controlID, show_hide_control, otherControlID) {

        var elementRef = document.getElementById(controlID);
        var checkBoxArray = elementRef.getElementsByTagName('input');
        var checkedValues = '';
        var checkedValues1 = '';

        for (var i = 0; i < checkBoxArray.length; i++) {
            var checkBoxRef = checkBoxArray[i];

            if (checkBoxRef.checked == true) {
                var labelArray = checkBoxRef.parentNode.getElementsByTagName('label');

                if (labelArray.length > 0) {
                    checkedValues = labelArray[0].innerHTML;
                    //alert(checkedValues);
                    if (checkedValues == "Other Side effects (specify)") {
                        show_hide(show_hide_control, 'visible');
                    }
                    else {
                        //document.getElementById(otherControlID).value = "";
                        //show_hide(show_hide_control, 'notvisible');
                    }
                }
            }
            else {
                var labelArray1 = checkBoxRef.parentNode.getElementsByTagName('label');

                if (labelArray1.length > 0) {
                    checkedValues1 = labelArray1[0].innerHTML;
                    //alert(checkedValues1);
                    if (checkedValues1 == "Other Side effects (specify)") {
                        //document.getElementById(otherControlID).value = '';
                        //document.getElementsByName(otherControlID)[0].value = '';
                        ClearTextBox(otherControlID);
                        show_hide(show_hide_control, 'notvisible');
                    }
                }

            }
        }
    }

    function SignsOfHepatitisReviewChkList(controlID, show_hide_control, otherControlID) {
        var elementRef = document.getElementById(controlID);
        var checkBoxArray = elementRef.getElementsByTagName('input');
        var checkedValues = '';
        var checkedValues1 = '';

        for (var i = 0; i < checkBoxArray.length; i++) {
            var checkBoxRef = checkBoxArray[i];

            if (checkBoxRef.checked == true) {
                var labelArray = checkBoxRef.parentNode.getElementsByTagName('label');

                if (labelArray.length > 0) {
                    checkedValues = labelArray[0].innerHTML;
                    if (checkedValues == "Signs of Hepatitis") {
                        show_hide(show_hide_control, 'visible');
                    }
                    else {
                        //document.getElementById(otherControlID).value = "";
                        //show_hide(show_hide_control, 'notvisible');
                    }
                }
            }
            else {
                var labelArray1 = checkBoxRef.parentNode.getElementsByTagName('label');

                if (labelArray1.length > 0) {
                    checkedValues1 = labelArray1[0].innerHTML;
                    //alert(checkedValues1);
                    if (checkedValues1 == "Signs of Hepatitis") {
                        //document.getElementById(otherControlID).value = '';
                        //document.getElementsByName(otherControlID)[0].value = '';
                        ClearMultiSelect(otherControlID);
                        show_hide(show_hide_control, 'notvisible');
                    }
                }

            }
        }
    }

    function SelectTBFindings(selectId, RadioYes, RadioNo) {
        var valueSelected = document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].text;
        if (valueSelected == 'TB Confirmed' || valueSelected == 'On Treatment') {
            document.getElementById(RadioYes).checked = true;
            document.getElementById(RadioNo).checked = false;
            show_hide('TBAvailableResults', 'visible');
            document.getElementById(RadioYes).disabled = true;
            document.getElementById(RadioNo).disabled = true;
        }
        else if (valueSelected == 'No signs or symptoms') {
            document.getElementById(RadioYes).checked = false;
            document.getElementById(RadioNo).checked = true;
            show_hide('TBAvailableResults', 'notvisible');
            document.getElementById(RadioYes).disabled = true;
            document.getElementById(RadioNo).disabled = true;
        }
        else {
            document.getElementById(RadioYes).disabled = false;
            document.getElementById(RadioNo).disabled = false;
        }


    }


    function EnableDisableStopReason(ipt, stopReason) {
        //var valueSelected = document.getElementById(ipt)[document.getElementById(ipt).selectedIndex].text;
        var list = document.getElementById(ipt);
        var inputs = list.getElementsByTagName("input");
        var selected;
        var checkedValue;
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].checked) {
                selected = inputs[i];
                var labelArray = selected.parentNode.getElementsByTagName('label');
                checkedValue = labelArray[0].innerHTML;

                break;
            }
        }

        //alert(checkedValue);
        if (checkedValue == 'Start IPT' || checkedValue == 'Continue IPT' || checkedValue == 'Completed IPT') {
            show_hide('divDiscontinueIPTBody', 'notvisible');
            ClearMultiSelect(stopReason);
            //document.getElementById(stopReason).disabled = true;
        }
        else {
            show_hide('divDiscontinueIPTBody', 'visible');

            //document.getElementById(stopReason).disabled = false;
            //deselectRadioListItem(stopReason)
            //alert("nop");
        }


    }

    function deselectRadioListItem(clientID) {

        var list = document.getElementById(clientID);
        var inputs = list.getElementsByTagName("input");

        for (var i = 0; i < inputs.length; i++) {
            var objCurrentRdo = document.getElementById(clientID + "_" + i.toString());
            objCurrentRdo.disabled = false;

        }

    }

    function SelectTBFindings(selectId, selSmear, dtSmear, selGene, dtGene, selDST, dtDST, rdoXrayYes, rdoXrayNo, dtxray, selCXR, txtCXR, rdoBiopsyYes, rdoBiopsyNo,
    dtBiopsy, selTBClassification, selPtnClassification, selTBPlan, txtTBPlan, selTBReg, txtTBReg, txtTBStart, txtTBEnd, selTBTreat, rdoScreenedTBYes, rdoScreenedTBNo,
    txtSpecifyWhy, selFacilityReferred, rdoIPT, dtINHStart, dtINHEnd, dtPyStart, dtPyEnd, rdoAdheAddYes, rdoAdheAddNo, rdoMisseddoseYes, rdoMisseddoseNo,
    rdoRefAdheYes, rdoRefAdheNo, cblRevChklst, cblhepatitis, txtOthersideeff, ddlReasonDeclinedIPT, txtOtherReasonDeclinedIPT) {
        var valueSelected = document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].text;
        if (valueSelected == 'TB Confirmed') {
            show_hide('AvailableTBResultsBodyDiv', 'visible');
            show_hide('divContactsScreenedforTB', 'visible');
            show_hide('divIPTBody', 'notvisible');

            clearRadioButtonList(rdoIPT); ClearTextBox(dtINHStart); ClearTextBox(dtINHEnd); ClearTextBox(dtPyStart); ClearTextBox(dtPyEnd);
            ClearRadioButtons(rdoAdheAddYes, rdoAdheAddNo); ClearRadioButtons(rdoMisseddoseYes, rdoMisseddoseNo); ClearRadioButtons(rdoRefAdheYes, rdoRefAdheNo);
            ClearMultiSelect(cblRevChklst); ClearMultiSelect(cblhepatitis); ClearTextBox(txtOthersideeff); ClearSelectList(ddlReasonDeclinedIPT); ClearTextBox(txtOtherReasonDeclinedIPT);
        }
        else if (valueSelected == 'On Treatment') {
            show_hide('AvailableTBResultsBodyDiv', 'visible');

            show_hide('divContactsScreenedforTB', 'notvisible');
            show_hide('divIPTBody', 'notvisible');
            ClearRadioButtons(rdoScreenedTBYes, rdoScreenedTBNo); ClearTextBox(txtSpecifyWhy); ClearSelectList(selFacilityReferred);

            clearRadioButtonList(rdoIPT); ClearTextBox(dtINHStart); ClearTextBox(dtINHEnd); ClearTextBox(dtPyStart); ClearTextBox(dtPyEnd);
            ClearRadioButtons(rdoAdheAddYes, rdoAdheAddNo); ClearRadioButtons(rdoMisseddoseYes, rdoMisseddoseNo); ClearRadioButtons(rdoRefAdheYes, rdoRefAdheNo);
            ClearMultiSelect(cblRevChklst); ClearMultiSelect(cblhepatitis); ClearTextBox(txtOthersideeff); ClearSelectList(ddlReasonDeclinedIPT); ClearTextBox(txtOtherReasonDeclinedIPT);
        }
        else if (valueSelected == 'No signs or symptoms') {
            show_hide('AvailableTBResultsBodyDiv', 'notvisible');

            show_hide('divContactsScreenedforTB', 'notvisible');
            show_hide('divIPTBody', 'visible');
            ClearRadioButtons(rdoScreenedTBYes, rdoScreenedTBNo); ClearTextBox(txtSpecifyWhy); ClearSelectList(selFacilityReferred);

            ClearSelectList(selSmear); ClearTextBox(dtSmear); ClearSelectList(selGene); ClearTextBox(dtGene); ClearSelectList(selDST); ClearTextBox(dtDST);
            ClearRadioButtons(rdoXrayYes, rdoXrayNo);
            ClearTextBox(dtxray); ClearSelectList(selCXR); ClearTextBox(txtCXR);
            ClearRadioButtons(rdoBiopsyYes, rdoBiopsyNo);
            ClearTextBox(dtBiopsy); ClearSelectList(selTBClassification); ClearSelectList(selPtnClassification); ClearSelectList(selTBPlan);
            ClearTextBox(txtTBPlan); ClearSelectList(selTBReg); ClearTextBox(txtTBReg); ClearTextBox(txtTBStart); ClearTextBox(txtTBEnd); ClearSelectList(selTBTreat);
        }
        else {
            show_hide('AvailableTBResultsBodyDiv', 'visible');
            show_hide('divContactsScreenedforTB', 'notvisible');
            show_hide('divIPTBody', 'notvisible');
            ClearRadioButtons(rdoScreenedTBYes, rdoScreenedTBNo); ClearTextBox(txtSpecifyWhy); ClearSelectList(selFacilityReferred);

            clearRadioButtonList(rdoIPT);
            ClearTextBox(dtINHStart); ClearTextBox(dtINHEnd); ClearTextBox(dtPyStart); ClearTextBox(dtPyEnd);
            ClearRadioButtons(rdoAdheAddYes, rdoAdheAddNo); ClearRadioButtons(rdoMisseddoseYes, rdoMisseddoseNo); ClearRadioButtons(rdoRefAdheYes, rdoRefAdheNo);
            ClearMultiSelect(cblRevChklst); ClearMultiSelect(cblhepatitis); ClearTextBox(txtOthersideeff); ClearSelectList(ddlReasonDeclinedIPT); ClearTextBox(txtOtherReasonDeclinedIPT);
        }


    }



    function SelectIPT(selectId, controlID1, controlID2, controlID3, controlID4, reasonDeclinedIPT, OtherReasonDeclinedIPT) {
        var list = document.getElementById(selectId);
        var inputs = list.getElementsByTagName("input");
        var valueSelected;
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].checked) {
                var selector = 'label[for=' + inputs[i].id + ']';
                var label = document.querySelector(selector);
                valueSelected = label.innerHTML;

                //valueSelected = inputs[i];
                break;
            }
        }

        if (valueSelected == 'Start IPT' || valueSelected == 'Continue IPT') {
            show_hide('INHStartEndDates', 'visible');
            show_hide('PyridoxineStartEnd', 'visible');
            show_hide('declinedIPTID', 'notvisible');
            ClearSelectList(reasonDeclinedIPT); ClearTextBox(OtherReasonDeclinedIPT);
            //show_hide('ifYesStopReason', 'notvisible');

        }
        //        else if (valueSelected == 'Stop IPT') {
        //            
        //            show_hide('ifYesStopReason', 'visible');
        //            show_hide('INHStartEndDates', 'visible');
        //            show_hide('PyridoxineStartEnd', 'visible');
        //            
        //        }
        else if (valueSelected == 'Completed IPT') {
            //show_hide('ifYesStopReason', 'notvisible');
            show_hide('INHStartEndDates', 'visible');
            show_hide('PyridoxineStartEnd', 'visible');
            show_hide('declinedIPTID', 'notvisible');
            ClearSelectList(reasonDeclinedIPT); ClearTextBox(OtherReasonDeclinedIPT);
        }
        else if (valueSelected == 'Declined IPT') {
            show_hide('declinedIPTID', 'visible');
            show_hide('INHStartEndDates', 'notvisible');
            show_hide('PyridoxineStartEnd', 'notvisible');
            ClearTextBox(controlID1); ClearTextBox(controlID2); ClearTextBox(controlID3); ClearTextBox(controlID4);
        }
        else {
            //show_hide('ifYesStopReason', 'notvisible');
            ClearTextBox(controlID1); ClearTextBox(controlID2); ClearTextBox(controlID3); ClearTextBox(controlID4);
            ClearSelectList(reasonDeclinedIPT); ClearTextBox(OtherReasonDeclinedIPT);
            show_hide('INHStartEndDates', 'notvisible');
            show_hide('PyridoxineStartEnd', 'notvisible');
            show_hide('declinedIPTID', 'notvisible');
        }


    }

    function WindowPrint() {
        window.print();
    }

</script>
<%--<div class="border center formbg">
    <br />--%>
<style type="text/css">
    .style2
    {
        height: 27px;
    }
</style>
<table id="tblTBAssessment" class="center formbg" cellspacing="6" cellpadding="0"
    width="100%" border="0">
    <tr>
        <td>
            <asp:Panel ID="TBAssessmentHeader" runat="server" Style="padding: 6px" CssClass="border">
                <h5 class="forms" align="left">
                    &nbsp;<asp:ImageButton ID="imgTBAssessment" runat="server" ImageUrl="~/images/arrow-up.gif" />
                    &nbsp;<asp:Literal ID="literTBAssessment" Text="TB Assessment" runat="server"></asp:Literal>
                    <%--<asp:Label ID="lblTBAssessment" runat="server" Text="TB Assessment"></asp:Label>--%>
                </h5>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:Panel ID="TBAssessmentBody" runat="server" Style="overflow: hidden;">
    <div class="row">
        <%--left--%>
        <div class="col-xs-6">
            <div class="row">
                <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        <asp:Label ID="lblTBassessment" runat="server" Font-Bold="True" Text="TB assessment (ICF):"></asp:Label></label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                    <div id="Div1" class="customdivbordermultiselect" runat="server">
                        <asp:CheckBoxList ID="cblTBAssessmentICF" runat="server" CssClass="radioButtonList">
                        </asp:CheckBoxList>
                    </div>
                </div>
            </div>
        </div>
        <%--right--%>
        <div class="col-xs-6">
            <div class="row">
                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                    <asp:Label ID="lblTBFindings" runat="server" CssClass="control-label required"
                        Text="TB findings:"></asp:Label>
                </div>
                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                    <asp:DropDownList ID="ddlTBFindings" runat="server" Width="250" CssClass="form-control"
                        OnSelectedIndexChanged="ddlTBFindings_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div id="divContactsScreenedforTB" style="display: block">
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Contacts screened for TB?</label>
                        </div>
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                            <asp:RadioButton ID="rdoContactsScreenedForTBYes" runat="server" Text="Yes" GroupName="rdoContactsScreenedForTB"
                                OnCheckedChanged="rdoContactsScreenedForTBYes_CheckedChanged" />
                            <asp:RadioButton ID="rdoContactsScreenedForTBNo" runat="server" Text="No" GroupName="rdoContactsScreenedForTB"
                                OnCheckedChanged="rdoContactsScreenedForTBNo_CheckedChanged" />
                        </div>
                    </div>
                    <div id="IfNoContactsScreenedSpecifyWhy" style="display: none" valign="top">
                        <div class="row">
                            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    <asp:Label ID="Label1" runat="server" Text="If no, specify why:" Font-Bold="True"></asp:Label></label>
                            </div>
                            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                <asp:TextBox ID="txtSpecifyWhyContactNotScreenedForTB" runat="server" Width="95%"
                                    Columns="3" Rows="4" TextMode="MultiLine" CssClass="form-control" Style="resize: none;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                <asp:Label ID="Label2" runat="server" Text="Facility patient referred for treatment:"
                                    Font-Bold="True"></asp:Label></label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                            <asp:DropDownList ID="ddlPatientReferredForTreatment" runat="server" CssClass="form-control"
                                Width="450">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" id="divPaediatricIntensifiedCase" runat="server">
        <div class="col-md-10 col-sm-12 col-xs-12 form-group">
            <div id="divG1" class="box box-default box-solid">
                <div class="box-header with-border" style="background-color: #3c8dbc;">
                    <h5 class="box-title" style="color:White;">
                        Screening Questions</h5>
                    <!-- /.box-tools -->
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                            1. Cough of any duration
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                            <asp:RadioButtonList ID="rbPIsCough" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                            2. Fever
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                            <asp:RadioButtonList ID="rbPIsFever" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                            3. Failure to thrive or poor weight gain
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                            <asp:RadioButtonList ID="rbIsFailureThriveOrPoorWeightGain" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                            4. Lethargy, less playful than usual
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                            <asp:RadioButtonList ID="rbIsLethargy" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                            5. Contact with a TB case
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                            <asp:RadioButtonList ID="rbIsContact" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                            <ul>
                                <li>If “Yes” to any of the above questions, suspect TB, examine the child and use the
                                    pediatric TB diagnostic algorithm to evaluate for active disease. Rule out underlying
                                    conditions, refer if necessary</li><li>If “No” to all questions, initiate workup for
                                        IPT and repeat screening on subsequent visits</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- /.box sub box level G1 -->
            </div>
        </div>
    </div>
    <div class="row" id="divAdolescenAdultIntensifiedCase" runat="server">
        <div class="col-md-10 col-sm-12 col-xs-12 form-group">
            <div id="div2" class="box box-default box-solid">
                <div class="box-header with-border" style="background-color: #3c8dbc;">
                    <h5 class="box-title" style="color:White;">
                        Screening Questions</h5>
                    <!-- /.box-tools -->
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                            1. Cough of any duration
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                            <asp:RadioButtonList ID="rbAIsCough" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                            2. Fever
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                            <asp:RadioButtonList ID="rbAIsFever" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                            3. Noticeable weight loss
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                            <asp:RadioButtonList ID="rbIsNoticeableWeightLoss" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                            4. Night sweats
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                            <asp:RadioButtonList ID="rbIsNightSweats" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                            <ul>
                                <li>If “Yes” to any question; take a detailed history, examine the patient and do sputum
                                    examination if coughing (sputum smear or GeneXpert). Exclude underlying illnesses</li><li>
                                        If “No” to all questions, initiate workup for IPT and repeat screening on subsequent
                                        visits</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- /.box sub box level G1 -->
            </div>
        </div>
    </div>
</asp:Panel>
<act:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" SuppressPostBack="true"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="TBAssessmentBody" CollapseControlID="TBAssessmentHeader"
    ExpandControlID="TBAssessmentHeader" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
    ImageControlID="imgTBAssessment"></act:CollapsiblePanelExtender>
<table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0"
    id='tblAvailableTBResultsBody'>
    <tr>
        <td>
            <asp:Panel ID="AvailableTBResultsHeader" runat="server" Style="padding: 6px" CssClass="border">
                <h5 class="forms" align="left">
                    <asp:ImageButton ID="imgAvailableTBResults" runat="server" ImageUrl="~/images/arrow-up.gif" />
                    <asp:Label ID="lblAvailableTBResults" runat="server" Text="Available TB Results"></asp:Label>
                    <asp:RadioButton ID="rdoAvailableTBResultsYes" runat="server" Text="Yes" GroupName="rdoAvailableTBResults"
                        OnCheckedChanged="rdoAvailableTBResultsYes_CheckedChanged" Visible="False" />
                    <asp:RadioButton ID="rdoAvailableTBResultsNo" runat="server" Text="No" GroupName="rdoAvailableTBResults"
                        OnCheckedChanged="rdoAvailableTBResultsNo_CheckedChanged" Visible="False" /></h5>
            </asp:Panel>
        </td>
    </tr>
</table>
<div id="AvailableTBResultsBodyDiv">
    <asp:Panel ID="AvailableTBResultsBody" runat="server" Style="overflow: hidden;">
        <div id="TBAvailableResults">
            <div class="row" id="sputumSmear">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        <asp:Label ID="lblSputumSmear" runat="server" Font-Bold="True" Text="Sputum smear:"></asp:Label></label>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Result:</label>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <asp:DropDownList ID="ddlSputumSmear" runat="server" CssClass="form-control" Width="250">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Date:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <div class="form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" class="form-control pull-left" id="txtSputumSmearDate" clientidmode="Static"
                                maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                        </div>
                    </div>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        <asp:Label ID="lblGeneExpert" runat="server" Text="GeneExpert:"></asp:Label></label>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Result:</label>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <asp:DropDownList ID="ddlGeneExpert" runat="server" CssClass="form-control" Width="250">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Date:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <div class="form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" class="form-control pull-left" id="txtGeneExpertDate" clientidmode="Static"
                                maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                        </div>
                    </div>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                </div>
            </div>
            <div class="row" id="Tr2">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        <asp:Label ID="lblSuptumDST" runat="server" Font-Bold="True" Text="Sputum for DST:"></asp:Label></label>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Result:</label>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <asp:DropDownList ID="ddlSputumDST" runat="server" CssClass="form-control" Width="250">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Date:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <div class="form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" class="form-control pull-left" id="txtSputumDSTDate" clientidmode="Static"
                                maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                        </div>
                    </div>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                </div>
            </div>
            <div class="row" id="chestXRay">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        <asp:Label ID="lblChestXRay" runat="server" Font-Bold="True" Text="Chest X-Ray:"></asp:Label></label>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Result:</label>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <asp:RadioButton ID="rdoChestXrayYes" runat="server" GroupName="rdoChestXray" Text="Yes"
                        OnCheckedChanged="rdoChestXrayYes_CheckedChanged" />
                    <asp:RadioButton ID="rdoChestXrayNo" runat="server" GroupName="rdoChestXray" Text="No"
                        OnCheckedChanged="rdoChestXrayNo_CheckedChanged" CssClass="checkbox-btn" />
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Date:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <div class="form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" class="form-control pull-left" id="txtChestXrayDate" clientidmode="Static"
                                maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                        </div>
                    </div>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                </div>
            </div>
            <div class="row" id="CXRResults" style="display: none">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        CXR results :</label>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <asp:DropDownList ID="ddlCXRResults" runat="server" CssClass="form-control" Width="250">
                    </asp:DropDownList>
                </div>
                <div id="OtherCRXSpecify" style="display: none">
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Other CXR (Specify):</label>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <asp:TextBox ID="txtOtherCXRResults" runat="server" CssClass="form-control" Width="250"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row" id="tissueBiopsy">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        <asp:Label ID="lblTissueBiopsy" runat="server" Text="Tissue Biopsy:"></asp:Label></label>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Result:</label>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <asp:RadioButton ID="rdoTissueBiopsyYes" runat="server" GroupName="rdoTissueBiopsy"
                        Text="Yes" OnCheckedChanged="rdoTissueBiopsyYes_CheckedChanged" />
                    <asp:RadioButton ID="rdoTissueBiopsyNo" runat="server" GroupName="rdoTissueBiopsy"
                        Text="No" OnCheckedChanged="rdoTissueBiopsyNo_CheckedChanged" CssClass="checkbox-btn" />
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Date:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <div class="form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" class="form-control pull-left" id="txtTissueBiopsyDate" clientidmode="Static"
                                maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                        </div>
                    </div>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        TB classification:</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:DropDownList ID="ddlTBClassification" runat="server" CssClass="form-control"
                        Width="250">
                    </asp:DropDownList>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Patient classification:</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:DropDownList ID="ddlPatientClassification" runat="server" CssClass="form-control"
                        Width="250">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        TB plan :</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:DropDownList ID="ddlTBPLan" runat="server" CssClass="form-control" Width="250">
                    </asp:DropDownList>
                </div>
                <div id="OtherTBPlanSpecify" style="display: none">
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Specify other TB plan:</label>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <asp:TextBox ID="txtOtherTBPlan" runat="server" CssClass="form-control" Width="250"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        TB regimen :</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:DropDownList ID="ddlTBRegimen" runat="server" CssClass="form-control" Width="250">
                    </asp:DropDownList>
                </div>
                <div id="OtherTBRegimenSpecify" style="display: none">
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Other TB regimen:</label>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <asp:TextBox ID="txtOtherTBRegimen" runat="server" CssClass="form-control" Width="250"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        TB Regimen Start date:</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <div class="form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" class="form-control pull-left" id="txtTBRegimenStartDate" clientidmode="Static"
                                maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        End date:</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <div class="form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" class="form-control pull-left" id="txtTBRegimenEndDate" clientidmode="Static"
                                maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        TB treatment outcome:</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:DropDownList ID="ddlTBTreatment" runat="server" CssClass="form-control" Width="250">
                    </asp:DropDownList>
                </div>
                <div id="specifyOtheroutcome" align="left" style="display: none">
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label class="control-label">
                            Specify other:</label>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <asp:TextBox ID="txtOtherTreatmentOutcome" runat="server" CssClass="form-control"
                            Width="250"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</div>
<act:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" SuppressPostBack="true"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="AvailableTBResultsBody"
    CollapseControlID="AvailableTBResultsHeader" ExpandControlID="AvailableTBResultsHeader"
    CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="imgAvailableTBResults">
</act:CollapsiblePanelExtender>
<table id='tblIPTBody' class="center formbg" cellspacing="6" cellpadding="0" width="100%"
    border="0">
    <tr>
        <td>
            <asp:Panel ID="IPTHeader" runat="server" Style="padding: 6px" CssClass="border">
                <h5 class="forms" align="left">
                    <asp:ImageButton ID="imgIPT" runat="server" ImageUrl="~/images/arrow-up.gif" />
                    &nbsp;IPT (Patients with No Signs and Symptoms)</h5>
            </asp:Panel>
        </td>
    </tr>
</table>
<div id="divIPTBody">
    <asp:Panel ID="IPTBody" runat="server">
        <div class="row" align="center">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <asp:RadioButtonList ID="rdoLstIPT" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoLstIPT_SelectedIndexChanged"
                    CssClass="checkbox-btn">
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="row">
            <div id="declinedIPTID" style="display: none">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Reason for declining IPT :</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:DropDownList ID="ddlReasonDeclinedIPT" runat="server" CssClass="form-control"
                        Width="250">
                    </asp:DropDownList>
                </div>
            </div>
            <div id="otherReasonDeclinedIPTID" style="display: none; width:100%">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Specify Other :</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtOtherReasonDeclinedIPT" runat="server" CssClass="form-control"
                        Width="250"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row" id="INHStartEndDates" style="display: none">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label class="control-label">
                    Start date:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <div class="form-group">
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-left" id="txtINHStartDate" clientidmode="Static"
                            maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label class="control-label">
                    End date:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <div class="form-group">
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-left" id="txtINHEndDate" clientidmode="Static"
                            maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row" id="PyridoxineStartEnd" style="display: none">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label class="control-label">
                    Pyridoxine Start date:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <div class="form-group">
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-left" id="txtPyridoxineStartDate" clientidmode="Static"
                            maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label class="control-label">
                    End date:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <div class="form-group">
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-left" id="txtPyridoxineEndDate" clientidmode="Static"
                            maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Has adherence been addressed?</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:RadioButton ID="rdoAdherenceBeenAddressedYes" runat="server" GroupName="rdoAdherenceBeenAddressed"
                        Text="Yes" OnCheckedChanged="rdoAdherenceBeenAddressedYes_CheckedChanged" />
                    <asp:RadioButton ID="rdoAdherenceBeenAddressedNo" runat="server" GroupName="rdoAdherenceBeenAddressed"
                        Text="No" OnCheckedChanged="rdoAdherenceBeenAddressedNo_CheckedChanged" />
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Any missed doses?</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:RadioButton ID="rdoMissedAnyTBDosesYes" runat="server" GroupName="rdoMissedAnyTBDoses"
                        Text="Yes" OnCheckedChanged="rdoMissedAnyTBDosesYes_CheckedChanged" />
                    <asp:RadioButton ID="rdoMissedAnyTBDosesNo" runat="server" GroupName="rdoMissedAnyTBDoses"
                        Text="No" OnCheckedChanged="rdoMissedAnyTBDosesNo_CheckedChanged" />
                </div>
            </div>
            <div class="row" id="MissedDosesYesReferredforadherence" style="display: none">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        If yes, referred for adherence?</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:RadioButton ID="rdoReferredForAdherenceYes" runat="server" GroupName="rdoReferredForAdherence"
                        Text="Yes" OnCheckedChanged="rdoReferredForAdherenceYes_CheckedChanged" />
                    <asp:RadioButton ID="rdoReferredForAdherenceNo" runat="server" GroupName="rdoReferredForAdherence"
                        Text="No" OnCheckedChanged="rdoReferredForAdherenceNo_CheckedChanged" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label class="control-label">
                        Review Checklist:</label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:CheckBoxList ID="cblReviewChecklist" runat="server" RepeatDirection="Horizontal"
                         CssClass="radioButtonList">
                    </asp:CheckBoxList>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group" id="divSignsOfHepatitis" style="display: none">
                    <asp:CheckBoxList ID="cblSignsOfHepatitis" runat="server"  CssClass="radioButtonList">
                    </asp:CheckBoxList>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group" id="ReviewChkListSpecifyOtherTBSideEffects"
                    style="display: none">
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">
                                Specify other TB side effects:</label></div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSpecifyOtherTBSideEffects" runat="server" CssClass="form-control"
                                Width="250"></asp:TextBox></div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</div>
<act:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" SuppressPostBack="true"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="IPTBody" CollapseControlID="IPTHeader"
    ExpandControlID="IPTHeader" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
    ImageControlID="imgIPT"></act:CollapsiblePanelExtender>
<table id='tblDiscontinueIPTBody' class="center formbg" cellspacing="6" cellpadding="0"
    width="100%" border="0">
    <tr>
        <td>
            <asp:Panel ID="DiscontinueIPTHeader" runat="server" Style="padding: 6px" CssClass="border">
                <h5 class="forms" align="left">
                    <asp:ImageButton ID="imgDiscontinueIPT" runat="server" ImageUrl="~/images/arrow-up.gif" />
                    &nbsp;Discontinue IPT</h5>
            </asp:Panel>
        </td>
    </tr>
</table>
<div id="divDiscontinueIPTBody">
    <asp:Panel ID="DiscontinueIPTBody" runat="server" Style="overflow: hidden;">
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
                <label for="inputEmail3" class="control-label">
                    Stop reason:</label>
            </div>
            <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                <asp:CheckBoxList ID="cblStopTBReason" runat="server" RepeatDirection="Horizontal"
                     CssClass="radioButtonList">
                </asp:CheckBoxList>
            </div>
        </div>
    </asp:Panel>
</div>
<act:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server" SuppressPostBack="true"
    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="DiscontinueIPTBody" CollapseControlID="DiscontinueIPTHeader"
    ExpandControlID="DiscontinueIPTHeader" CollapsedImage="~/images/arrow-up.gif"
    Collapsed="true" ImageControlID="imgDiscontinueIPT"></act:CollapsiblePanelExtender>
<%--</div>--%>
<br />
<div class="row" align="center">
    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
        <uc1:UserControlKNH_Signature ID="UserControlKNH_SignatureTB" runat="server" />
    </div>
</div>
<div class="row" align="center">
    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
        <asp:Button ID="btnTBSave" runat="server" Text="Save" OnClick="btnTBSave_Click" CssClass="btn btn-primary"
            Height="30px" Width="9%" Style="text-align: left;" />
        <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub;
            color: #fff; margin-right: 2%;">
        </label>
        <asp:Button ID="btnTBDQC" runat="server" Text="Data Quality Check" Visible="False"
            Height="30px" Width="16%" Style="text-align: left;" />
        <asp:Button ID="btnTBClose" runat="server" Text="Close" OnClick="btnTBClose_Click"
            CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
        <label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub;
            color: #fff; margin-right: 2%;">
        </label>
        <asp:Button ID="btnTBPrint" runat="server" Text="Print" OnClientClick="WindowPrint();"
            CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
        <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
            color: #fff; margin-right: 2%;">
        </label>
    </div>
</div>
