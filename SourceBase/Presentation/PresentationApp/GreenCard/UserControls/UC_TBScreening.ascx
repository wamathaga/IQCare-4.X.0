<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_TBScreening.ascx.cs"
    Inherits="PresentationApp.GreenCard.UserControls.UC_TBScreening" %>

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


<table cellspacing="6" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="form" align="left" style="width: 50%">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblTBassessment" runat="server" Font-Bold="True" Text="TB assessment (ICF):"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding:10px 20px 5px 0">
                            <div  class="table-responsive  pre-scrollable box" style="max-height:200px;padding:10px 10px">
                                <asp:CheckBoxList ID="cblTBAssessmentICF" runat="server">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="form" align="center" width="50%">
            <table width="100%">
                        <tr>
                            <td >
                                <asp:Label ID="lblTBFindings" runat="server" CssClass="label-right" 
                                    Text="TB findings:"></asp:Label>
                                </td>
                                <td style="width:50%">
                <asp:DropDownList ID="ddlTBFindings" runat="server" Width="50%" CssClass="form-control select2" OnSelectedIndexChanged="ddlTBFindings_SelectedIndexChanged">
                </asp:DropDownList>
                </td>
                </tr>
                </table>
                
            </td>
        </tr>
        <tr>
        <td colspan="2">
        <div id="divContactsScreenedforTB" style="display: block">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <label>
                                    Contacts screened for TB?</label>
                                <asp:RadioButton ID="rdoContactsScreenedForTBYes" runat="server" Text="Yes" GroupName="rdoContactsScreenedForTB"
                                     />
                                <asp:RadioButton ID="rdoContactsScreenedForTBNo" runat="server" Text="No" GroupName="rdoContactsScreenedForTB"
                                     />
                            </td>
                        
                        
                        <td id="IfNoContactsScreenedSpecifyWhy" style="display: none" align="center" valign="top">
                            <table width="100%">
                                <tr>
                                    <td align="right" valign="middle" width="40%">
                                        <asp:Label ID="Label1" runat="server" Text="If no, specify why:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td align="left" width="60%">
                                        <asp:TextBox ID="txtSpecifyWhyContactNotScreenedForTB" runat="server" Width="100%"
                                            Columns="3" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        </tr>
                        <tr>                            
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Facility patient referred for treatment:"
                                Font-Bold="True"></asp:Label>
                        </td>
                                   
                        <td>
                            <asp:DropDownList ID="ddlPatientReferredForTreatment" CssClass="form-control select2" runat="server">
                            </asp:DropDownList>
                                        
                            </td>
                        </tr>
                    </table>
                </div>
        </td>
        </tr>
    </table>

<div id="AvailableTBResultsBodyDiv">    
    <table id="TBAvailableResults" cellspacing="6" cellpadding="0" width="100%" border="0">
            <tr id="sputumSmear">
                <td class="form" align="center" style="width: 100%" colspan="2">
                    <table width="100%">
                        <tr>
                            <td width="20%" align="left">
                                &nbsp;<asp:Label ID="lblSputumSmear" runat="server" Font-Bold="True" Text="Sputum smear:"></asp:Label>
                            </td>
                            <td width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="40%">
                                            <label>
                                                Result:</label>&nbsp;
                                        </td>
                                        <td align="left" width="60%">
                                            <asp:DropDownList ID="ddlSputumSmear" Width="60%" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <label>
                                                &nbsp;Date:
                                            </label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                 </div>
                                                <asp:TextBox ID="txtSputumSmearDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                                            </div>    
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="Tr1">
                <td class="form" align="center" style="width: 100%" colspan="2">
                    <table width="100%">
                        <tr>
                            <td width="20%" align="left">
                                &nbsp;<asp:Label ID="lblGeneExpert" runat="server" Font-Bold="True" Text="GeneExpert:"></asp:Label>
                            </td>
                            <td width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="40%">
                                            <label>
                                                Result:</label>&nbsp;
                                        </td>
                                        <td align="left" width="60%">
                                            <asp:DropDownList ID="ddlGeneExpert" Width="60%" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <label>
                                                &nbsp;Date:
                                            </label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                             </div>
                                                <asp:TextBox ID="txtGeneExpertDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                                            </div>    
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="Tr2">
                <td class="form" align="center" style="width: 100%" colspan="2">
                    <table width="100%">
                        <tr>
                            <td width="20%" align="left">
                                &nbsp;<asp:Label ID="lblSuptumDST" runat="server" Font-Bold="True" Text="Sputum for DST:"></asp:Label>
                            </td>
                            <td width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="40%">
                                            <label>
                                                Result:</label>&nbsp;
                                        </td>
                                        <td align="left" width="60%">
                                            <asp:DropDownList ID="ddlSputumDST" Width="60%" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <label>
                                                Date:
                                            </label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                             </div>
                                                <asp:TextBox ID="txtSputumDSTDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy" ></asp:TextBox>
                                            </div>    
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="chestXRay">
                <td class="form" align="center" style="width: 100%" colspan="2">
                    <table width="100%">
                        <tr>
                            <td width="20%" align="left">
                                &nbsp;<asp:Label ID="lblChestXRay" runat="server" Font-Bold="True" Text="Chest X-Ray:"></asp:Label>
                            </td>
                            <td width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="40%">
                                            <label>
                                                Result:</label>
                                        </td>
                                        <td align="left" width="60%">
                                            <asp:RadioButton ID="rdoChestXrayYes" runat="server" GroupName="rdoChestXray" Text="Yes"
                                                />
                                            <asp:RadioButton ID="rdoChestXrayNo" runat="server" GroupName="rdoChestXray" Text="No"
                                                 />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <label>
                                                Date:
                                            </label>
                                        </td>
                                        <td align="left" width="80%">
                                           <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                             </div>
                                                <asp:TextBox ID="txtChestXrayDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                                             </div>   
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="CXRResults" style="display: none">
                <td align="center" class="form" colspan="2" width="50%">
                    <table width="100%" style="table-layout: fixed">
                        <tr>
                            <td width="50%" align="left">
                                <label>
                                    CXR results :</label>
                                <asp:DropDownList ID="ddlCXRResults" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td id="OtherCRXSpecify" style="display: none" width="100%">
                                <label>
                                    Other CXR (Specify):</label>
                                <asp:TextBox ID="txtOtherCXRResults" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="tissueBiopsy">
                <td class="form" align="center" style="width: 100%" colspan="2">
                    <table width="100%">
                        <tr>
                            <td width="20%" align="left">
                                &nbsp;<asp:Label ID="lblTissueBiopsy" runat="server" Font-Bold="True" Text="Tissue Biopsy:"></asp:Label>
                            </td>
                            <td width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="40%">
                                            <label>
                                                Result:</label>&nbsp;
                                        </td>
                                        <td align="left" width="60%">
                                            <asp:RadioButton ID="rdoTissueBiopsyYes" runat="server" GroupName="rdoTissueBiopsy"
                                                Text="Yes"  />
                                            <asp:RadioButton ID="rdoTissueBiopsyNo" runat="server" GroupName="rdoTissueBiopsy"
                                                Text="No"  />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <label>
                                                Date:
                                            </label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                             </div>
                                                <asp:TextBox ID="txtTissueBiopsyDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                                             </div>   
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="form" style="width: 50%">
                    <table width="100%">
                        <tr>
                            <td align="right" width="40%">
                                <label>
                                    TB classification:</label>&nbsp;
                            </td>
                            <td align="left" width="60%">
                                <asp:DropDownList ID="ddlTBClassification" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="center" class="form" width="50%">
                    <table width="100%">
                        <tr>
                            <td align="right" width="40%">
                                <label>
                                    Patient classification:</label>&nbsp;
                            </td>
                            <td align="left" width="60%">
                                <asp:DropDownList ID="ddlPatientClassification" Width="60%" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="form" align="center" width="50%" colspan="2">
                    <table width="100%">
                        <tr>
                            <td width="50%" align="left">
                                <label>
                                    TB plan :</label><asp:DropDownList ID="ddlTBPLan" runat="server">
                                    </asp:DropDownList>
                            </td>
                            <td id="OtherTBPlanSpecify" style="display: none" width="100%">
                                <label>
                                    Specify other TB plan:</label><asp:TextBox ID="txtOtherTBPlan" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="form" width="50%" colspan="2">
                    <table width="100%">
                        <tr>
                            <td width="50%" align="left">
                                <label>
                                    TB regimen :</label><asp:DropDownList ID="ddlTBRegimen" Width="60%" runat="server">
                                    </asp:DropDownList>
                            </td>
                            <td id="OtherTBRegimenSpecify" style="display: none" width="100%">
                                <label>
                                    Other TB regimen:</label><asp:TextBox ID="txtOtherTBRegimen" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="form" style="width: 50%" colspan="2">
                    <table width="100%">
                        <tr>
                            <td width="15%" align="left">
                                <label>
                                    TB Regimen :&nbsp;
                                </label>
                            </td>
                            <td width="45%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <label>
                                                Start date:&nbsp;
                                            </label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                             </div>
                                                <asp:TextBox ID="txtTBRegimenStartDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                                             </div>   
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <label>
                                                End date:
                                            </label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                 </div>
                                                <asp:TextBox ID="txtTBRegimenEndDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                                            </div>    
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="form" colspan="2">
                    <table width="100%">
                        <tr>
                            <td align="left" width="50%">
                                <label>
                                    TB treatment outcome:</label>
                                <asp:DropDownList ID="ddlTBTreatment" Width="60%" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td id="specifyOtheroutcome" align="left" style="display: none" width="50%">
                                <label>
                                    Specify other:</label>
                                <asp:TextBox ID="txtOtherTreatmentOutcome" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>    
</div>


<div id="divIPTBody">    
     <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tr>
                <td align="center" class="form">
                    <asp:RadioButtonList ID="rdoLstIPT" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoLstIPT_SelectedIndexChanged">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td id="declinedIPTID" style="display: none" class="form">
                    <table width="100%">
                        <tr>
                            <td width="50%">
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <label>
                                                Reason for declining IPT :</label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlReasonDeclinedIPT" Width="60%" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50%">
                                <table id="otherReasonDeclinedIPTID" width="100%" style="display: none">
                                    <tr>
                                        <td align="right">
                                            <label>
                                                Specify Other :</label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtOtherReasonDeclinedIPT" runat="server" Width="80%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="INHStartEndDates" style="display: none" class="form">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="15%" align="left">
                                <label>
                                    INH :</label>
                            </td>
                            <td align="center" width="45%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="25%">
                                            <label>
                                                Start date:
                                            </label>
                                        </td>
                                        <td align="left" width="75%">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                 </div>
                                                <asp:TextBox ID="txtINHStartDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                                             </div>   
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="center" width="40%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <label>
                                                End date:
                                            </label>
                                        </td>
                                        <td align="left" width="80%">
                                           <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                             </div>
                                                <asp:TextBox ID="txtINHEndDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                                             </div>   
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="PyridoxineStartEnd" style="display: none" class="form">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="15%" align="left">
                                <label>
                                    Pyridoxine :</label>
                            </td>
                            <td align="center" style="width: 45%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="25%">
                                            <label>
                                                Start date:
                                            </label>
                                        </td>
                                        <td align="left" width="75%">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                 </div>
                                                <asp:TextBox ID="txtPyridoxineStartDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                                             </div>   
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="center">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="20%">
                                            <label>
                                                End date:
                                            </label>
                                        </td>
                                        <td align="left" width="80%">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                 </div>
                                                <asp:TextBox ID="txtPyridoxineEndDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                                            </div>   
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="form" align="center" style="width: 50%;">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="60%">
                                            <label>
                                                Has adherence been addressed?</label>
                                        </td>
                                        <td align="left" width="40%">
                                            <asp:RadioButton ID="rdoAdherenceBeenAddressedYes" runat="server" GroupName="rdoAdherenceBeenAddressed"
                                                Text="Yes"  />
                                            <asp:RadioButton ID="rdoAdherenceBeenAddressedNo" runat="server" GroupName="rdoAdherenceBeenAddressed"
                                                Text="No"  />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6px">
                            </td>
                            <td class="form" align="center">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td align="right" width="50%">
                                                        <label>
                                                            Any missed doses?</label>
                                                    </td>
                                                    <td align="left" width="60%">
                                                        <asp:RadioButton ID="rdoMissedAnyTBDosesYes" runat="server" GroupName="rdoMissedAnyTBDoses"
                                                            Text="Yes"  />
                                                        <asp:RadioButton ID="rdoMissedAnyTBDosesNo" runat="server" GroupName="rdoMissedAnyTBDoses"
                                                            Text="No" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="MissedDosesYesReferredforadherence" style="display: none">
                                            <table width="100%">
                                                <tr>
                                                    <td align="right" width="50%">
                                                        <label>
                                                            If yes, referred for adherence?</label>&nbsp;
                                                    </td>
                                                    <td align="left" width="50%">
                                                        <asp:RadioButton ID="rdoReferredForAdherenceYes" runat="server" GroupName="rdoReferredForAdherence"
                                                            Text="Yes" />
                                                        <asp:RadioButton ID="rdoReferredForAdherenceNo" runat="server" GroupName="rdoReferredForAdherence"
                                                            Text="No" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="form" align="center" width="50%" colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td align="left" style="height: 45px">
                                            <label>
                                                Review Checklist:</label><asp:CheckBoxList ID="cblReviewChecklist" runat="server"
                                                    RepeatDirection="Horizontal">
                                                </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <div id="divSignsOfHepatitis" style="display: none" class="customdivbordermultiselectAutoHeight">
                                                <asp:CheckBoxList ID="cblSignsOfHepatitis" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                            <br />
                                            <div id="ReviewChkListSpecifyOtherTBSideEffects" style="display: none">
                                                <label>
                                                    Specify other TB side effects:</label>
                                                <asp:TextBox ID="txtSpecifyOtherTBSideEffects" runat="server" Width="70%"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>    
</div>

<div id="divDiscontinueIPTBody">    
  <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tr>
                <td align="left" class="form" style="height: 59px">
                    <label>
                        Stop reason:</label>
                    <asp:CheckBoxList ID="cblStopTBReason" runat="server" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>    
</div>


