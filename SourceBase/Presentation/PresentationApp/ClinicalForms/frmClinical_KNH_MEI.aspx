<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmClinical_KNH_MEI.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_KNH_MEI" %>

<%@ Register TagPrefix="UcVitalSign" TagName="Uc1" Src="~/ClinicalForms/UserControl/UserControlKNH_VitalSigns.ascx" %>
<%@ Register TagPrefix="UcWhoStaging" TagName="Uc2" Src="~/ClinicalForms/UserControl/UserControlKNH_WHOStaging.ascx" %>
<%@ Register TagPrefix="UcTBScreening" TagName="Uc1" Src="~/ClinicalForms/UserControl/UserControlKNH_TBScreening.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtVisitDate').datepicker({ autoclose: true });
            $('#txtLMPDate').datepicker({ autoclose: true });
            $('#txtEDD').datepicker({ autoclose: true });
            $('#txtLastHIVTest').datepicker({ autoclose: true });
            $('#txtBloodTransfusiondt').datepicker({ autoclose: true });
            $('#txtAppointmentDate').datepicker({ autoclose: true });
            $('#txtPMTCTAppDate').datepicker({ autoclose: true });

        }
        //Calling RegisterJQuery when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);
        //Calling RegisterJQuery when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
    </script>
    <script type="text/javascript" language="javascript">

        function fnUncheckYes() {
            document.getElementById("<%=rdofamilyinformationFilledYes.ClientID%>").checked = false;
        }

        function fnUncheckNo() {
            document.getElementById("<%=rdofamilyinformationFilledNo.ClientID%>").checked = false;
        }
        function fnCalculateGestation(date1) {
            var one_day = 1000 * 60 * 60 * 24;    //Get 1 day in milliseconds
            // Convert both dates to milliseconds
            var date1_ms = Date.parse(date1);
            var date2_ms = new Date().getTime();
            // Calculate the difference in milliseconds  
            var difference_ms = date2_ms - date1_ms;
            // Convert back to days and return 
            var gestationInDays = Math.round(difference_ms / one_day);
            var gestationInWeeks = Math.round(gestationInDays / 7);
            var remainder = gestationInDays % 7;
            var textValue = gestationInWeeks + ' weeks and ' + remainder + ' days';
            document.getElementById("<%=txtGestation.ClientID%>").value = textValue;
        }

        function WindowPrintAll() {
            window.print();
        }
        function SelectAllCheckboxes() {
            $('#<%=grdLatestResults.ClientID%>').find("input:checkbox").removeAttr("disabled");
        }
        function fnsetCollapseState() {
            var e = document.getElementById("<%=ddlFieldVisitType.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Select") {
                alert('Please select visit type');
            }
        }
        function fnsetCollapseAll() {
            document.getElementById("<%=btnHideClick.ClientID%>").click();
        }
        function fnotherHMHealth() {
            var e = document.getElementById("<%=ddlHMHealth.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Other (specify)") {
                show('divHMentalHealth');
            }
            else {
                hide('divHMentalHealth');
            }
        }
        function fnotherCMHealth() {
            var e = document.getElementById("<%=ddlCMHealth.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Other") {
                show('divCMentalHealth');
            }
            else {
                hide('divCMentalHealth');
            }
        }

        function fnotherCTXStopreason() {
            var e = document.getElementById("<%=ddlCTX.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Stop CTX") {
                show('divctx');
            }
            else {
                hide('divctx');
            }
        }

        function fnotherCurrentRegimen() {
            var e = document.getElementById("<%=ddlSpecifyCurrentRegmn.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Other") {
                show('tdothercurrentregimen');
            }
            else {
                hide('tdothercurrentregimen');
            }

        }

        function checkNone(searchEles, Id_None) {
            for (var i = 0; i < searchEles.length; i++) {
                if (searchEles[i].children.length > 0) {
                    for (var ii = 0; ii < searchEles[i].children.length; ii++) {
                        if (searchEles[i].children[ii].tagName == 'LABEL' && searchEles[i].children[ii].htmlFor != Id_None) {
                            document.getElementById(searchEles[i].children[ii].htmlFor).checked = false;
                        }
                        else if (searchEles[i].children[ii].textContent == "Other" && searchEles[i].children[ii].tagName == 'SPAN') {
                            for (var iii = 0; iii < searchEles[i].children[ii].children.length; iii++) {
                                for (var iv = 0; iv < searchEles[i].children[ii].children.length; iv++) {
                                    if (searchEles[i].children[ii].children[iii].children[iv].tagName == 'LABEL' && searchEles[i].children[ii].children[iii].children[iv].htmlFor != Id_None) {
                                        document.getElementById(searchEles[i].children[ii].children[iii].children[iv].htmlFor).checked = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        function checkNotNone(searchEles, Id_None) {
            for (var i = 0; i < searchEles.length; i++) {
                if (searchEles[i].children.length > 0) {
                    for (var ii = 0; ii < searchEles[i].children.length; ii++) {
                        if (searchEles[i].children[ii].tagName == 'LABEL' && searchEles[i].children[ii].textContent == "None") {
                            document.getElementById(searchEles[i].children[ii].htmlFor).checked = false;
                        }
                    }
                }
            }
        }


        function GetCheckboxId(Id) {
            var searchEles = document.getElementById("<%=pnlBarriertoadherence.ClientID %>").children;
            for (var i = 0; i < searchEles.length; i++) {
                if (searchEles[i].children.length > 0) {
                    for (var ii = 0; ii < searchEles[i].children.length; ii++) {
                        if (searchEles[i].children[ii].textContent == "Other" && searchEles[i].children[ii].tagName == 'SPAN') {
                            for (var iii = 0; iii < searchEles[i].children[ii].children.length; iii++) {
                                for (var iv = 0; iv < searchEles[i].children[ii].children.length; iv++) {
                                    if (searchEles[i].children[ii].children[iii].children[iv].tagName == 'LABEL' && searchEles[i].children[ii].children[iii].children[iv].htmlFor == Id) {
                                        checkNotNone(searchEles, Id);
                                    }
                                }
                            }
                        }
                        else if (searchEles[i].children[ii].tagName == 'LABEL' && searchEles[i].children[ii].textContent == "None" && searchEles[i].children[ii].htmlFor == Id) {
                            checkNone(searchEles, Id);
                        }
                        else if (searchEles[i].children[ii].tagName == 'LABEL' && searchEles[i].children[ii].htmlFor == Id) {
                            checkNotNone(searchEles, Id);
                        }
                    }
                }
            }
        }

        function showHideControls() {
            var radioYes = document.getElementById('<%=rdotetanustoxoidyes.ClientID%>');
            if (radioYes.checked == true) {
                var ddl = document.getElementById('<%=ddlTTVaccine.ClientID%>');
                ddl.style.display = 'block';
            }
            else {
                var ddl = document.getElementById('<%=ddlTTVaccine.ClientID%>');
                ddl.style.display = 'none';
            }

            var radioNo = document.getElementById('<%= rdotetanustoxoidno.ClientID%>');
            if (radioNo.checked == true) {
                var txt = document.getElementById('<%=txtNoTTReason.ClientID%>');
                txt.style.display = 'block';
            }
            else {
                var txt = document.getElementById('<%=txtNoTTReason.ClientID%>');
                txt.style.display = 'none';
            }
        }
    </script>
    <div class="content-wrapper">
        <div>
            <div class="box-body">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary box-solid">
                            <div class="box-header">
                                <h3 class="box-title">
                                    ANC Form</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                                <div class="row">
                                    <br />
                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
                                        <label for="inputEmail3" class="control-label required">
                                            Visit date:</label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <div class="form-group">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <input type="text" class="form-control pull-left" id="txtVisitDate" clientidmode="Static"
                                                    onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                                                    onfocus="javascript:vDateType='3'" maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy"
                                                    style="width: 120px;" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
                                        <label for="inputEmail3" class="control-label required">
                                            Visit Type:</label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <asp:DropDownList ID="ddlFieldVisitType" runat="server" AutoPostBack="true" CssClass="form-control"
                                            Width="250" OnSelectedIndexChanged="ddlFieldVisitType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="" style="margin-left: 1.5%; margin-bottom: 5px;">
                                        <act:TabContainer ID="tabControl" runat="server" ActiveTabIndex="0" Width="98%" CssClass="ajax__myTab">
                                            <%--Triage--%>
                                            <act:TabPanel ID="TabPnlTriage" runat="server" HeaderText="Triage">
                                                <ContentTemplate>
                                                    <div class="border center formbg">
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1ClientInformation" runat="server" onclick="fnsetCollapseState();"
                                                                        CssClass="border center formbg" Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label31" runat="server" Text="Client Information"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2ClientInformation" runat="server" Style="overflow: hidden;">
                                                                        <div id="ClientInformation" style="display: none;">
                                                                            <div class="row">
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        LMP:</label>
                                                                                </div>
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                    <div class="form-group">
                                                                                        <div class="input-group date">
                                                                                            <div class="input-group-addon">
                                                                                                <i class="fa fa-calendar"></i>
                                                                                            </div>
                                                                                            <input type="text" class="form-control pull-left" id="txtLMPDate" clientidmode="Static"
                                                                                                maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;"
                                                                                                onblur="DateFormat(this,this.value,event,false,3); fnCalculateGestation(this.value)"
                                                                                                onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,3);" />
                                                                                        &nbsp;</input></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        EDD:</label>
                                                                                </div>
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                    <div class="form-group">
                                                                                        <div class="input-group date">
                                                                                            <div class="input-group-addon">
                                                                                                <i class="fa fa-calendar"></i>
                                                                                            </div>
                                                                                            <input type="text" class="form-control pull-left" id="txtEDD" clientidmode="Static"
                                                                                                maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;"
                                                                                                onblur="DateFormat(this,this.value,event,false,3)" onfocus="javascript:vDateType='3'"
                                                                                                onkeyup="DateFormat(this,this.value,event,false,3);" />
                                                                                        &nbsp;</input></div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Age at Mernarche:</label>
                                                                                </div>
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                    <input type="text" id="txtAgeMernarche" runat="server" style="width: 108px;" maxlength="2"
                                                                                        class="form-control" />
                                                                                &nbsp;</input></div>
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Parity:</label>
                                                                                </div>
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:DropDownList ID="ddlparity" runat="server" CssClass="form-control" 
                                                                                        Width="150px">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Gravidae:</label>
                                                                                </div>
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:DropDownList ID="ddlGravidae" runat="server" CssClass="form-control" 
                                                                                        Width="150px">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Gestation:</label>
                                                                                </div>
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                    <input type="text" id="txtGestation" runat="server" style="width: 40%;" readonly="readonly"
                                                                                        class="form-control" />
                                                                                &nbsp;</input></div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        <label id="lblVisitNumber" runat="server" class="required margin35">
                                                                                            Visit Number:</label>
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:DropDownList ID="ddlVisitNumber" runat="server" CssClass="form-control" 
                                                                                        Width="150px">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div id="spttvaccine" style="display: none">
                                                                                    <div class="row">
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label" style="margin-left: 20px;">
                                                                                                Did Client receive tetanus toxoid vaccine?</label>
                                                                                        </div>
                                                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                            <input id="rdotetanustoxoidyes" type="radio" value="Yes" runat="server" name="tetanustoxoid"
                                                                                                onclick="showHideControls();" />
                                                                                            &nbsp;</input><label for="rdotetanustoxoidYes">Yes</label>
                                                                                            <input id="rdotetanustoxoidno" type="radio" value="No" runat="server" name="tetanustoxoid"
                                                                                                onclick="showHideControls();" />
                                                                                            &nbsp;</input><label for="rdotetanustoxoidNo">No</label>
                                                                                        </div>
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <asp:DropDownList ID="ddlTTVaccine" runat="server" Width="200px" 
                                                                                                CssClass="form-control">
                                                                                            </asp:DropDownList>
                                                                                            <textarea id="txtNoTTReason" runat="server" cols="40" rows="1" style="display: none;
                                                                                                width: 450px; height: 50px;" placeholder="Give reason why TT vaccine wasn't issued"
                                                                                                class="form-control"></textarea>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1VitalSigns" runat="server" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                                                        Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImgPC" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="lblVitalSigns" runat="server" Text="Vital Signs"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2VitalSigns" runat="server" Style="overflow: hidden;">
                                                                        <div id="VitalSigns" style="display: none">
                                                                            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                                                                <tr>
                                                                                    <td class="border pad5 whitebg" width="100%">
                                                                                        <UcVitalSign:Uc1 ID="idVitalSign" runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <act:CollapsiblePanelExtender ID="CPEClientInformation" runat="server" SuppressPostBack="True"
                                                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2ClientInformation"
                                                            CollapseControlID="pnl1ClientInformation" ExpandControlID="pnl1ClientInformation"
                                                            CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                                                            BehaviorID="_content_CPEClientInformation"></act:CollapsiblePanelExtender>
                                                        <act:CollapsiblePanelExtender ID="CPEVitalSigns" runat="server" SuppressPostBack="True"
                                                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2VitalSigns" CollapseControlID="pnl1VitalSigns"
                                                            ExpandControlID="pnl1VitalSigns" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                                            ImageControlID="ImgPC" BehaviorID="_content_CPEVitalSigns"></act:CollapsiblePanelExtender>
                                                    </div>
                                                    <br />
                                                    <div class="border center formbg">
                                                        <table id="Table1" cellspacing="6" cellpadding="0" width="100%" border="0" runat="server">
                                                            <tr id="Tr2" runat="server" align="center">
                                                                <td id="Td2" runat="server" class="form">
                                                                    <asp:Button ID="btnTriageSave" runat="server" Text="Save" OnClick="btnTriageSave_Click"
                                                                        CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                    <asp:Label ID="lblSave" CssClass="glyphicon glyphicon-floppy-disk" Style="margin-left: -3%;
                                                                        margin-right: 2%; vertical-align: sub; color: #fff;" runat="server"></asp:Label>
                                                                    <asp:Label ID="lblDelete" CssClass="glyphicon glyphicon-floppy-remove" Style="margin-left: -3%;
                                                                        margin-right: 2%; vertical-align: sub; color: #fff;" runat="server" Visible="False"></asp:Label>
                                                                    <asp:Button ID="btnTriageClose" runat="server" Text="Close" OnClick="btnTriageClose_Click"
                                                                        CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                    <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                                                        vertical-align: sub; color: #fff;">
                                                                    </label>
                                                                    <asp:Button ID="btnTriagePrint" runat="server" Text="Print" OnClientClick="WindowPrint()"
                                                                        CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                    <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                                                        color: #fff;">
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </act:TabPanel>
                                            <%--HTC--%>
                                            <act:TabPanel ID="TabPnlHTC" runat="server" HeaderText="HTC">
                                                <ContentTemplate>
                                                    <div class="border center formbg">
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1HTC" runat="server" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                                                        Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="imgMHT" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="lblMHT" runat="server" Text="Maternal HIV Testing"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2HTC" runat="server" Style="overflow: hidden;">
                                                                        <div id="MHT" style="display: none">
                                                                            <div class="row">
                                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label required">
                                                                                        Is client due for HIV Testing?:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <input id="rdoHIVTestingTodayYes" type="radio" value="Yes" runat="server" name="HIVTestingToday" />
                                                                                    <label for="rdoHIVTestingTodayYes">
                                                                                        Yes</label>
                                                                                    <input id="rdoHIVTestingTodayNo" type="radio" value="No" runat="server" name="HIVTestingToday" />
                                                                                    <label for="rdoHIVTestingTodayNo">
                                                                                        No</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                </div>
                                                                            </div>
                                                                            <div id="trHIVTesting1" style="display: none;" class="whitebg">
                                                                                <div class="row">
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            Previous HIV Status:</label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:DropDownList ID="ddlPrevHIVStatus" runat="server" CssClass="form-control" Width="120">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            Previous Point of HIV Testing:
                                                                                        </label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:DropDownList ID="ddlPrevPointHIVTesting" runat="server" CssClass="form-control"
                                                                                            Width="120">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            Date of last HIV Test:</label>
                                                                                    </div>
                                                                                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                                                                        <div class="form-group">
                                                                                            <div class="input-group date">
                                                                                                <div class="input-group-addon">
                                                                                                    <i class="fa fa-calendar"></i>
                                                                                                </div>
                                                                                                <input type="text" class="form-control pull-left" id="txtLastHIVTest" clientidmode="Static"
                                                                                                    maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;"
                                                                                                    onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                                                                                                    onfocus="javascript:vDateType='3'" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div id="trHIVTesting2" style="display: none" class="whitebg">
                                                                                <div class="row">
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            Pre test counselling and testing:</label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:DropDownList ID="ddlPreTestCounselling" runat="server" CssClass="form-control"
                                                                                            Width="120">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            Post test counselling and testing:</label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:DropDownList ID="ddlPosttestcounselling" runat="server" CssClass="form-control"
                                                                                            Width="120">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            HIV Test Done Today:</label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:DropDownList ID="ddlHIVTestDoneToday" runat="server" CssClass="form-control"
                                                                                            Width="120">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                        </label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Final HIV result:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:DropDownList ID="ddlFinalHIVResult" runat="server" CssClass="form-control" Width="120">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1PartnerHIVStatus" runat="server" onclick="fnsetCollapseState();"
                                                                        CssClass="border center formbg" Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton3" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label7" runat="server" Text="Partners HIV Status"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2PartnerHIVStatus" runat="server" Style="overflow: hidden;">
                                                                        <div id="PHS" style="display: none">
                                                                            <div class="row">
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Is Patient accompanied by partner?</label>
                                                                                </div>
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    <input id="rdoPatientaccPartnerYes" type="radio" value="Yes" runat="server" name="Patientaccompaniedbypartner" />
                                                                                    <label for="rdodiscordantcoupleYes">
                                                                                        Yes</label>
                                                                                    <input id="rdoPatientaccPartnerNo" type="radio" value="No" runat="server" name="Patientaccompaniedbypartner" />
                                                                                    <label for="rdodiscordantcoupleNo">
                                                                                        No</label>
                                                                                </div>
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                </div>
                                                                            </div>
                                                                            <div id="pap1" style="display: none">
                                                                                <div class="row">
                                                                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            Pre Test Counselling:</label>
                                                                                    </div>
                                                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:DropDownList ID="ddlpartnerPreTestCounselling" runat="server" CssClass="form-control"
                                                                                            Style="width: 150px;">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            HIV Test done to partner:</label>
                                                                                    </div>
                                                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:DropDownList ID="ddlHIVTestdonetopartner" runat="server" CssClass="form-control"
                                                                                            Style="width: 150px;">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            Final HIV result for partner:</label>
                                                                                    </div>
                                                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:DropDownList ID="ddlPartnerFinalHIVresult" runat="server" CssClass="form-control"
                                                                                            Style="width: 150px;">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                        </label>
                                                                                    </div>
                                                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div id="pap2" style="display: none">
                                                                                <div class="row">
                                                                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            Post Test Counselling:</label>
                                                                                    </div>
                                                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:DropDownList ID="ddlPartnerPostTestCounselling" runat="server" CssClass="form-control"
                                                                                            Style="width: 150px;">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            Is the couple discordant?</label>
                                                                                    </div>
                                                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                        <input id="rdodiscordantcoupleYes" type="radio" value="Yes" runat="server" name="discordantcouple" />
                                                                                        <label for="rdodiscordantcoupleYes">
                                                                                            Yes</label>
                                                                                        <input id="rdodiscordantcoupleNo" type="radio" value="No" runat="server" name="discordantcouple" />
                                                                                        <label for="rdodiscordantcoupleNo">
                                                                                            No</label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            Partners DNA PCR result?:</label>
                                                                                    </div>
                                                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:DropDownList ID="ddlPartnerDNA" runat="server" CssClass="form-control" Style="width: 150px;">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                        </label>
                                                                                    </div>
                                                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1FamilyHIVinformation" runat="server" onclick="fnsetCollapseState();"
                                                                        CssClass="border center formbg" Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton5" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label17" runat="server" Text="Family HIV information"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2FamilyHIVinformation" runat="server" Style="overflow: hidden;">
                                                                        <div id="FHI" style="display: none">
                                                                            <div class="row">
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Has the family information form been filled?</label>
                                                                                </div>
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:RadioButton ID="rdofamilyinformationFilledYes" type="radio" value="Yes" runat="server"
                                                                                        name="familyinformationFilled" OnCheckedChanged="rdofamilyinformationFilledYes_CheckedChanged"
                                                                                        AutoPostBack="true" onclick="fnUncheckNo();" />
                                                                                    <label for="rdofamilyinformationFilledYes">
                                                                                        Yes</label>
                                                                                    <asp:RadioButton ID="rdofamilyinformationFilledNo" type="radio" value="No" runat="server"
                                                                                        name="familyinformationFilled" onclick="fnUncheckYes(); window.open('frmFamilyInformation.aspx');"
                                                                                        AutoPostBack="true" OnCheckedChanged="rdofamilyinformationFilledNo_CheckedChanged" />
                                                                                    <label for="rdofamilyinformationFilledNo">
                                                                                        No</label>
                                                                                </div>
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Have other members of the family been tested for HIV?
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:DropDownList ID="ddlFamilybeentestedHIV" runat="server" CssClass="form-control"
                                                                                        Style="width: 150px;">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <act:CollapsiblePanelExtender ID="CPEHTC" runat="server" SuppressPostBack="True"
                                                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2HTC" CollapseControlID="pnl1HTC"
                                                            ExpandControlID="pnl1HTC" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                                            ImageControlID="ImgPC" BehaviorID="_content_CPEHTC"></act:CollapsiblePanelExtender>
                                                        <%--<act:CollapsiblePanelExtender ID="CPEMaternalTesting" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2MaternalTesting" CollapseControlID="pnl1MaternalTesting"
                                ExpandControlID="pnl1MaternalTesting" CollapsedImage="~/images/arrow-up.gif"
                                Collapsed="True" ImageControlID="ImgPC" BehaviorID="_content_CPEMaternalTesting">
                            </act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPEHIVTestResultsToday" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2HIVTestResultsToday"
                                CollapseControlID="pnl1HIVTestResultsToday" ExpandControlID="pnl1HIVTestResultsToday"
                                CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                                BehaviorID="_content_CPEHIVTestResultsToday"></act:CollapsiblePanelExtender>--%>
                                                        <act:CollapsiblePanelExtender ID="CPEPartnerHIVStatus" runat="server" SuppressPostBack="True"
                                                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2PartnerHIVStatus"
                                                            CollapseControlID="pnl1PartnerHIVStatus" ExpandControlID="pnl1PartnerHIVStatus"
                                                            CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                                                            BehaviorID="_content_CPEPartnerHIVStatus"></act:CollapsiblePanelExtender>
                                                        <act:CollapsiblePanelExtender ID="CPEFamilyHIVinformation" runat="server" SuppressPostBack="True"
                                                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2FamilyHIVinformation"
                                                            CollapseControlID="pnl1FamilyHIVinformation" ExpandControlID="pnl1FamilyHIVinformation"
                                                            CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                                                            BehaviorID="_content_CPEFamilyHIVinformation"></act:CollapsiblePanelExtender>
                                                    </div>
                                                    <br />
                                                    <div class="border center formbg">
                                                        <table cellspacing="6" cellpadding="0" width="100%" border="0" id="tblHTC" runat="server">
                                                            <tr id="TrHTC" runat="server" align="center">
                                                                <td id="TdHTC" runat="server" class="form">
                                                                    <asp:Button ID="btnHTCSave" runat="server" Text="Save" OnClick="btnHTCSave_Click"
                                                                        Enabled="false" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                    <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                                                        vertical-align: sub; color: #fff;">
                                                                    </label>
                                                                    <asp:Button ID="btnHTCClose" runat="server" Text="Close" OnClick="btnHTCClose_Click"
                                                                        Enabled="false" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                    <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                                                        vertical-align: sub; color: #fff;">
                                                                    </label>
                                                                    <asp:Button ID="btnHTCPrint" runat="server" Text="Print" OnClientClick="WindowPrint()"
                                                                        Enabled="false" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                    <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                                                        color: #fff;">
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </act:TabPanel>
                                            <%--profile--%>
                                            <act:TabPanel ID="TabPnlProfile" runat="server" HeaderText="Profile">
                                                <ContentTemplate>
                                                    <div class="border center formbg">
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1PsychosocialHistoryGBV" runat="server" onclick="fnsetCollapseState();"
                                                                        CssClass="border center formbg" Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton6" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label20" runat="server" Text="Psychosocial History & GBV"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2PsychosocialHistoryGBV" runat="server" Style="overflow: hidden;">
                                                                        <div id="PHG" style="display: none">
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Historic Mental Health:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:DropDownList ID="ddlHMHealth" onchange="fnotherHMHealth();" runat="server" CssClass="form-control"
                                                                                        Width="180">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Current Mental Health:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:DropDownList ID="ddlCMHealth" onchange="fnotherCMHealth();" runat="server" CssClass="form-control"
                                                                                        Width="180">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div id="divHMentalHealth" style="display: none">
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                        </label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:TextBox ID="txtHMentalHealth" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div id="divCMentalHealth" style="display: none">
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                        </label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:TextBox ID="txtCMentalHealth" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div id="tdExGBV" style="display: none">
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            Experienced any GBV?</label>
                                                                                    </div>
                                                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                        <input id="rdoExperienceanyGBVYes" type="radio" value="Yes" runat="server" name="ExperienceanyGBV" />
                                                                                        <label for="rdoExperienceanyGBVYes">
                                                                                            Yes</label>
                                                                                        <input id="rdoExperienceanyGBVNo" type="radio" value="No" runat="server" name="ExperienceanyGBV" />
                                                                                        <label for="rdoExperienceanyGBVNo">
                                                                                            No</label>
                                                                                    </div>
                                                                                </div>
                                                                                <div id="tdGBVExperienced" style="width: 50%; display: none;">
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            GBV Experienced:</label>
                                                                                    </div>
                                                                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                        <div id="divGBVExperienced" class="customdivbordermultiselect">
                                                                                            <asp:Panel ID="pnlGBVExperienced" runat="server">
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div id="tdSubabuse" style="display: none">
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            Substance abuse?</label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <input id="rdoHIVSubstanceAbusedYes" type="radio" value="Yes" runat="server" name="HIVSubstanceAbused" />
                                                                                        <label for="rdoHIVSubstanceAbusedYes">
                                                                                            Yes</label>
                                                                                        <input id="rdoHIVSubstanceAbusedNo" type="radio" value="No" runat="server" name="HIVSubstanceAbused" />
                                                                                        <label for="rdoHIVSubstanceAbusedNo">
                                                                                            No</label>
                                                                                    </div>
                                                                                </div>
                                                                                <div id="tdSubstanceAbused" style="width: 50%; display: none;">
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            Substance Abused:</label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <div id="divSubstanceAbused" class="customdivbordermultiselect">
                                                                                            <asp:Panel ID="pnlSubstanceAbused" runat="server">
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1BirthandGeneralPlan" runat="server" onclick="fnsetCollapseState();"
                                                                        CssClass="border center formbg" Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton7" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label27" runat="server" Text="Birth and General Plan"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2BirthandGeneralPlan" runat="server" Style="overflow: hidden;">
                                                                        <div id="BGP" style="display: none">
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label required">
                                                                                        Preffered mode of delivery:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:DropDownList ID="ddlPreferedmodeofdelivery" runat="server" CssClass="form-control"
                                                                                        Width="180">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label required">
                                                                                        Preffered Site of Delivery:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtPreferedSiteDelivery" runat="server" CssClass="form-control"
                                                                                        Width="180"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Comments - Additional notes?</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtPreferedSiteDeliveryAdditionalnotes" runat="server" TextMode="MultiLine"
                                                                                        CssClass="form-control" Style="height: 100px; vertical-align: middle; resize: none;"
                                                                                        Width="100%"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Referral:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <div id="div20" class="customdivbordermultiselect" style="margin-left: -25px;">
                                                                                        <asp:Panel ID="pnlReferral" runat="server">
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1PreviousthreePregnanciesHistory" runat="server" onclick="fnsetCollapseState();"
                                                                        CssClass="border center formbg" Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton8" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label33" runat="server" Text="Previous three Pregnancies History"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2PreviousthreePregnanciesHistory" runat="server" Style="overflow: hidden;">
                                                                        <div id="PTPH" style="display: none">
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Year of Delivery:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtYrofDelivery" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Place of Delivery:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtPlaceofDelivery" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Maturity :</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:DropDownList ID="ddlMaturityweeks" runat="server" CssClass="form-control" Width="130">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Labour duration (hrs)</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtLabourduratioin" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Mode of Delivery:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:DropDownList ID="ddlModeofDelivery" runat="server" CssClass="form-control" Width="180">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Gender</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:DropDownList ID="ddlGenderofBaby" runat="server" CssClass="form-control" Width="140">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Fate:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:DropDownList ID="ddlFateofBaby" runat="server" CssClass="form-control" Width="140">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                </div>
                                                                            </div>
                                                                            <div class="row" align="center">
                                                                                <div id="divPTF" class="whitebg" align="center">
                                                                                    <asp:Button ID="btnPrevthreeFreq" Text="Add" runat="server" OnClick="btnPrevthreeFreq_Click"
                                                                                        CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                                    <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                                                                        color: #fff;">
                                                                                    </label>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row" align="center">
                                                                                <br />
                                                                                <div class="grid" id="div2" style="width: 100%;">
                                                                                    <div class="rounded">
                                                                                        <div class="top-outer">
                                                                                            <div class="top-inner">
                                                                                                <div class="top">
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="mid-outer">
                                                                                            <div class="mid-inner">
                                                                                                <div class="mid" style="height: 100px; overflow: auto">
                                                                                                    <div id="div3" class="GridView whitebg">
                                                                                                        <asp:GridView Height="25px" ID="GrdPrevthreeFreq" runat="server" AutoGenerateColumns="False"
                                                                                                            Width="100%" AllowSorting="True" BorderWidth="1px" GridLines="None" CssClass="table table-bordered table-hover"
                                                                                                            CellPadding="0" OnRowDataBound="GrdPrevthreeFreq_RowDataBound" OnRowDeleting="GrdPrevthreeFreq_RowDeleting"
                                                                                                            OnSelectedIndexChanging="GrdPrevthreeFreq_SelectedIndexChanging">
                                                                                                        </asp:GridView>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                        </table>
                                                        <act:CollapsiblePanelExtender ID="CPEPsychosocialHistoryGBV" runat="server" SuppressPostBack="True"
                                                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2PsychosocialHistoryGBV"
                                                            CollapseControlID="pnl1PsychosocialHistoryGBV" ExpandControlID="pnl1PsychosocialHistoryGBV"
                                                            CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                                                            BehaviorID="_content_CPEPsychosocialHistoryGBV"></act:CollapsiblePanelExtender>
                                                        <act:CollapsiblePanelExtender ID="CPEBirthandGeneralPlan" runat="server" SuppressPostBack="True"
                                                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2BirthandGeneralPlan"
                                                            CollapseControlID="pnl1BirthandGeneralPlan" ExpandControlID="pnl1BirthandGeneralPlan"
                                                            CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                                                            BehaviorID="_content_CPEBirthandGeneralPlan"></act:CollapsiblePanelExtender>
                                                        <act:CollapsiblePanelExtender ID="CPEPreviousthreePregnanciesHistory" runat="server"
                                                            SuppressPostBack="True" ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2PreviousthreePregnanciesHistory"
                                                            CollapseControlID="pnl1PreviousthreePregnanciesHistory" ExpandControlID="pnl1PreviousthreePregnanciesHistory"
                                                            CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                                                            BehaviorID="_content_CPEPreviousthreePregnanciesHistory"></act:CollapsiblePanelExtender>
                                                    </div>
                                                    <br />
                                                    <div class="border center formbg">
                                                        <table cellspacing="6" cellpadding="0" width="100%" border="0" id="Table2" runat="server">
                                                            <tr id="Tr1" runat="server" align="center">
                                                                <td id="Td1" runat="server" class="form">
                                                                    <asp:Button ID="btnProfileSave" runat="server" Text="Save" OnClick="btnProfileSave_Click1"
                                                                        Enabled="False" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                    <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                                                        vertical-align: sub; color: #fff;">
                                                                    </label>
                                                                    <asp:Button ID="btnProfileClose" runat="server" Text="Close" OnClick="btnProfileClose_Click1"
                                                                        Enabled="False" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                    <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                                                        vertical-align: sub; color: #fff;">
                                                                    </label>
                                                                    <asp:Button ID="btnProfilePrint" runat="server" Text="Print" OnClientClick="WindowPrint()"
                                                                        Enabled="False" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                    <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                                                        color: #fff;">
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </act:TabPanel>
                                            <%--Clinical Review--%>
                                            <act:TabPanel ID="TabPnlClinicalReview" runat="server" HeaderText="Clinical Review">
                                                <ContentTemplate>
                                                    <asp:Button runat="server" ID="btnHideClick" Text="" Style="display: none;" OnClick="btnHideClick_Click" />
                                                    <div class="border center formbg">
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1PreviousObstreticsHistory" runat="server" onclick="fnsetCollapseState();"
                                                                        CssClass="border center formbg" Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton9" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label41" runat="server" Text="Obstretics History"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2PreviousObstreticsHistory" runat="server" Style="overflow: hidden;">
                                                                        <div id="POH" style="display: none">
                                                                            <div class="row">
                                                                                <div class="col-xs-11 col-md-7">
                                                                                    <div class="row">
                                                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label required">
                                                                                                Maternal Blood Group:</label>
                                                                                        </div>
                                                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                                            <asp:DropDownList ID="ddlMaternalBloodGroup" runat="server" CssClass="form-control"
                                                                                                Width="120">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label required">
                                                                                                Rhesus Factor :</label>
                                                                                        </div>
                                                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                                            <asp:DropDownList ID="ddlRhesusFactor" runat="server" CssClass="form-control" Width="120">
                                                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                                                <asp:ListItem Text="Positive" Value="Positive"></asp:ListItem>
                                                                                                <asp:ListItem Text="Negative" Value="Negative"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label">
                                                                                                Partners Blood Group:</label>
                                                                                        </div>
                                                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                                            <asp:DropDownList ID="ddlPartnersBloodGroup" runat="server" CssClass="form-control"
                                                                                                Width="120">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label">
                                                                                                History Of Twins :</label>
                                                                                        </div>
                                                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                                            <input id="rdoHistoryOfTwinsYes" type="radio" value="Yes" runat="server" name="HistoryOfTwins" />
                                                                                            <label for="rdoHistoryBloodTransfusionYes">
                                                                                                Yes</label>
                                                                                            <input id="rdoHistoryOfTwinsNo" type="radio" value="No" runat="server" name="HistoryOfTwins" />
                                                                                            <label for="rdoHistoryBloodTransfusionNo">
                                                                                                No</label>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label">
                                                                                                History of Blood Transfusion:</label>
                                                                                        </div>
                                                                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                                            <input id="rdoHistoryBloodTransfusionYes" type="radio" value="Yes" runat="server"
                                                                                                name="HistoryBloodTransfusion" />
                                                                                            <label for="rdoHistoryBloodTransfusionYes">
                                                                                                Yes</label>
                                                                                            <input id="rdoHistoryBloodTransfusionNo" type="radio" value="No" runat="server" name="HistoryBloodTransfusion" />
                                                                                            <label for="rdoHistoryBloodTransfusionNo">
                                                                                                No</label>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div id="divBloodTransfusion" style="display: none">
                                                                                            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                                                <label for="inputEmail3" class="control-label">
                                                                                                    Date</label>
                                                                                            </div>
                                                                                            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                                                                <div class="form-group">
                                                                                                    <div class="input-group date">
                                                                                                        <div class="input-group-addon">
                                                                                                            <i class="fa fa-calendar"></i>
                                                                                                        </div>
                                                                                                        <input type="text" class="form-control pull-left" id="txtBloodTransfusiondt" clientidmode="Static"
                                                                                                            maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;"
                                                                                                            onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                                                                                                            onfocus="javascript:vDateType='3'" />
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-xs-1 col-md-5">
                                                                                    <div class="row">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            History of Chronic Illness:</label>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div id="div23" class="customdivbordermultiselect">
                                                                                            <asp:Panel ID="pnlHistoryChronicIllness" runat="server">
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1PresentingComplaints" runat="server" onclick="fnsetCollapseState();"
                                                                        CssClass="border center formbg" Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton10" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label48" runat="server" Text="Presenting Complaints"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2PresentingComplaints" runat="server" Style="overflow: hidden;">
                                                                        <div id="CR" style="display: none">
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Presenting complaints:</label>
                                                                                </div>
                                                                                <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtPresentingcomplaints" TextMode="MultiLine" runat="server" Width="100%"
                                                                                        placeholder="Please Enter Detail about Presenting Complaints" CssClass="form-control"
                                                                                        Style="resize: none;"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1PhysicalExaminationFindings" runat="server" onclick="fnsetCollapseState();"
                                                                        CssClass="border center formbg" Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton11" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label53" runat="server" Text="Physical Examination Findings"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2PhysicalExaminationFindings" runat="server" Style="overflow: hidden;">
                                                                        <div id="PEF" style="display: none">
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        General Appearance:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtGeneralAppearance" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        CVS:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtCVS" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        RS:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtRS" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Breasts:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtBreasts" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Abdomen:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtAbdomen" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Vaginal Examination:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtVaginalExamination" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Discharge:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtdischarge" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Pallor:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtPallor" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Maturity:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtMaturity" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Fundal Height:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtFundalHeight" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Presentation:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtPresentation" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Foetal Heart Rate:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtFoetalHeartRate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Oedema :</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtOedema" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                </div>
                                                                            </div>
                                                                            <div class="row" align="center">
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1LabGrid" runat="server" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                                                        Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton12" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label63" runat="server" Text="Lab Grid"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2LabGrid" runat="server" Style="overflow: hidden;">
                                                                        <div id="LG" style="display: none; height: 100px">
                                                                            <div class="row" align="center">
                                                                                <div class="GridView whitebg" style="cursor: pointer;">
                                                                                    <div class="grid">
                                                                                        <div class="rounded">
                                                                                            <div class="top-outer">
                                                                                                <div class="top-inner">
                                                                                                    <div class="top">
                                                                                                        <h2 align="center">
                                                                                                            Latest Lab Results</h2>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="mid-outer">
                                                                                                <div class="mid-inner">
                                                                                                    <div class="mid" style="height: auto; overflow: auto">
                                                                                                        <div id="div29" class="GridView whitebg">
                                                                                                            <asp:GridView ID="grdLatestResults" runat="server" Width="100%" BorderWidth="0px"
                                                                                                                AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered table-hover"
                                                                                                                CellPadding="0">
                                                                                                            </asp:GridView>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="bottom-outer">
                                                                                                <div class="bottom-inner">
                                                                                                    <div class="bottom">
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row" align="center">
                                                                                <asp:Button ID="btnLab" runat="server" Font-Bold="True" Enabled="False" Text="Order Labs"
                                                                                    OnClick="btnLab_Click" CssClass="btn btn-primary" Height="30px" Width="13%" Style="text-align: left;" />
                                                                                <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                                                                    color: #fff;">
                                                                                </label>
                                                                                <br />
                                                                                <br />
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1DiagnosisandPlan" runat="server" onclick="fnsetCollapseState();"
                                                                        CssClass="border center formbg" Style="padding: 6px; display: none;">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton13" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label65" runat="server" Text="Diagnosis and Plan"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2DiagnosisandPlan" runat="server" Style="display: none;">
                                                                        <div id="DP" style="display: none">
                                                                            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                                                                <tr>
                                                                                    <td class="border pad5 whitebg" width="100%">
                                                                                        <table width="100%" border="0">
                                                                                            <tr>
                                                                                                <td style="width: 50%;">
                                                                                                    <label id="Label66" runat="server" class="margin5">
                                                                                                        Mother at risk:
                                                                                                        <input id="rdoMotheratriskyes" type="radio" value="Yes" runat="server" name="Motheratrisk" />
                                                                                                        <label for="rdoMotheratriskYes">
                                                                                                            Yes</label>
                                                                                                        <input id="rdoMotheratriskno" type="radio" value="No" runat="server" name="Motheratrisk" />
                                                                                                        <label for="rdoMotheratriskNo">
                                                                                                            No</label>
                                                                                                    </label>
                                                                                                    <div id="divriskfactor" style="display: none">
                                                                                                        <label id="Label64" runat="server" class="margin5">
                                                                                                            Specify risk factor:
                                                                                                            <asp:TextBox ID="txtmthrriskfactor" runat="server"></asp:TextBox>
                                                                                                        </label>
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td style="width: 35%;">
                                                                                                    <label id="Label67" runat="server" class="margin5">
                                                                                                        Plan:
                                                                                                        <asp:TextBox ID="txtPlan" runat="server" TextMode="MultiLine" Style="vertical-align: middle"
                                                                                                            Width="100%"></asp:TextBox>
                                                                                                    </label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Panel ID="pnl2Pharmacy" runat="server">
                                                                                            <div id="Phrmcy" style="display: inline">
                                                                                                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                                                                                    <tr>
                                                                                                        <td class="border pad5 whitebg" width="100%">
                                                                                                            <asp:Button ID="btnPharmacylink" runat="server" Text="Pharmacy" Enabled="False" OnClick="btnPharmacylink_Click"
                                                                                                                CssClass="btn btn-primary" Height="30px" Width="11%" Style="text-align: left;" />
                                                                                                            <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                                                                                                color: #fff;">
                                                                                                            </label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </asp:Panel>
                                                        </table>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                <label for="inputEmail3" class="control-label">
                                                                    Appointment Date:</label>
                                                            </div>
                                                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                <div class="form-group">
                                                                    <div class="input-group date">
                                                                        <div class="input-group-addon">
                                                                            <i class="fa fa-calendar"></i>
                                                                        </div>
                                                                        <input type="text" class="form-control pull-left" id="txtAppointmentDate" clientidmode="Static"
                                                                            maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;"
                                                                            onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                                                                            onfocus="javascript:vDateType='3'" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                <label for="inputEmail3" class="control-label">
                                                                    Admitted to ward?</label>
                                                            </div>
                                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                <input id="rdoAdmittedtowardyes" type="radio" value="Yes" runat="server" name="Admittedtoward" />
                                                                &nbsp;</input><label for="rdoAdmittedtowardYes">Yes</label>
                                                                <input id="rdoAdmittedtowardno" type="radio" value="No" runat="server" name="Admittedtoward" />
                                                                &nbsp;</input><label for="rdoAdmittedtowardNo">No</label>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                <label for="inputEmail3" class="control-label">
                                                                    Specify Ward Admitted:</label>
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                <asp:DropDownList ID="ddlDiagnosisandPlanWardAdmitted" runat="server" CssClass="form-control"
                                                                    Width="180">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                <label for="inputEmail3" class="control-label">
                                                                </label>
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <act:CollapsiblePanelExtender ID="CPEPreviousObstreticsHistory" runat="server" SuppressPostBack="True"
                                                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2PreviousObstreticsHistory"
                                                        CollapseControlID="pnl1PreviousObstreticsHistory" ExpandControlID="pnl1PreviousObstreticsHistory"
                                                        CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                                                        BehaviorID="_content_CPEPreviousObstreticsHistory"></act:CollapsiblePanelExtender>
                                                    <act:CollapsiblePanelExtender ID="CPEPresentingComplaints" runat="server" SuppressPostBack="True"
                                                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2PresentingComplaints"
                                                        CollapseControlID="pnl1PresentingComplaints" ExpandControlID="pnl1PresentingComplaints"
                                                        CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                                                        BehaviorID="_content_CPEPresentingComplaints"></act:CollapsiblePanelExtender>
                                                    <act:CollapsiblePanelExtender ID="CPEPhysicalExaminationFindings" runat="server"
                                                        SuppressPostBack="True" ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2PhysicalExaminationFindings"
                                                        CollapseControlID="pnl1PhysicalExaminationFindings" ExpandControlID="pnl1PhysicalExaminationFindings"
                                                        CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                                                        BehaviorID="_content_CPEPhysicalExaminationFindings"></act:CollapsiblePanelExtender>
                                                    <act:CollapsiblePanelExtender ID="CPELabGrid" runat="server" SuppressPostBack="True"
                                                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2LabGrid" CollapseControlID="pnl1LabGrid"
                                                        ExpandControlID="pnl1LabGrid" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                                        ImageControlID="ImgPC" BehaviorID="_content_CPELabGrid"></act:CollapsiblePanelExtender>
                                                    <act:CollapsiblePanelExtender ID="CPEDiagnosisandPlan" runat="server" SuppressPostBack="True"
                                                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2DiagnosisandPlan"
                                                        CollapseControlID="pnl1DiagnosisandPlan" ExpandControlID="pnl1DiagnosisandPlan"
                                                        CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                                                        BehaviorID="_content_CPEDiagnosisandPlan"></act:CollapsiblePanelExtender>
                                                    <br />
                                                    <div class="border center formbg">
                                                        <table cellspacing="6" cellpadding="0" width="100%" border="0" id="tblClinicalReview"
                                                            runat="server">
                                                            <tr id="TrClinicalReview" runat="server" align="center">
                                                                <td id="tdclinicalreview" runat="server" class="form">
                                                                    <asp:Button ID="btnSaveClinicalReview" runat="server" Text="Save" OnClick="btnSaveClinicalReview_Click1"
                                                                        Enabled="False" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                    <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                                                        vertical-align: sub; color: #fff;">
                                                                    </label>
                                                                    <asp:Button ID="btnCloseClinicalReview" runat="server" Text="Close" OnClick="btnCloseClinicalReview_Click1"
                                                                        Enabled="False" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                    <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                                                        vertical-align: sub; color: #fff;">
                                                                    </label>
                                                                    <asp:Button ID="btnPrintClinicalReview" runat="server" Text="Print" OnClientClick="WindowPrint()"
                                                                        Enabled="False" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                    <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                                                        color: #fff;">
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </act:TabPanel>
                                            <%-- PMTCT--%>
                                            <act:TabPanel ID="TabPnlPMTCT" runat="server" HeaderText="PMTCT">
                                                <ContentTemplate>
                                                    <div class="border center formbg">
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1ManagementHIVPositiveClientOtherFacility" runat="server" onclick="fnsetCollapseState();"
                                                                        CssClass="border center formbg" Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton16" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label73" runat="server" Text="PMTCT Interventions"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2ManagementHIVPositiveClientOtherFacility" runat="server" Style="overflow: hidden;">
                                                                        <div id="MHPCOF" style="display: none">
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Mother currently on ARVs:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <input id="rdoMothercurrentlyonARVYes" type="radio" value="Yes" runat="server" name="MothercurrentlyonARV" />
                                                                                    <label for="rdoMothercurrentlyonARVYes">
                                                                                        Yes</label>
                                                                                    <input id="rdoMothercurrentlyonARVNo" type="radio" value="No" runat="server" name="MothercurrentlyonARV" />
                                                                                    <label for="rdoMothercurrentlyonARVNo">
                                                                                        No</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        <label id="Label75" runat="server">
                                                                                            Specify current regimen:</label></label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:DropDownList ID="ddlSpecifyCurrentRegmn" runat="server" onchange="fnotherCurrentRegimen();"
                                                                                        CssClass="form-control" Width="200">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                </div>
                                                                                <div id="tdothercurrentregimen" style="display: none">
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            <label id="Label72" runat="server">
                                                                                                Regimen:</label></label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:TextBox ID="txtotherregimen" runat="server" Width="200" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        <label id="Label76" runat="server">
                                                                                            Mother currently on cotrimoxazole:</label></label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <input id="ddlmthroncotrimoxazoleyes" type="radio" value="Yes" runat="server" name="cotrimoxa" />
                                                                                    <label for="ddlmthroncotrimoxazoleyes">
                                                                                        Yes</label>
                                                                                    <input id="ddlmthroncotrimoxazoleNo" type="radio" value="No" runat="server" name="cotrimoxa" />
                                                                                    <label for="ddlmthroncotrimoxazoleNo">
                                                                                        No</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        <label id="Label77" runat="server">
                                                                                            Mother currently on multivitamins:</label></label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <input id="rdoMotherCurrentlyonmultivitaminsyes" type="radio" value="Yes" runat="server"
                                                                                        name="MotherCurrentlyonmultivitamins" />
                                                                                    <label for="rdoMotherCurrentlymultivitaminsYes">
                                                                                        Yes</label>
                                                                                    <input id="rdoMotherCurrentlyonmultivitaminsNo" type="radio" value="No" runat="server"
                                                                                        name="MotherCurrentlyonmultivitamins" />
                                                                                    <label for="rdoMotherCurrentlymultivitaminsNo">
                                                                                        No</label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1Adherence" runat="server" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                                                        Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton17" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label79" runat="server" Text="Adherence"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2Adherence" runat="server" Style="overflow: hidden;">
                                                                        <div id="Adhrnc" style="display: none">
                                                                            <div class="row">
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Adherence Assessment done:</label>
                                                                                </div>
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    <input id="rdoMotherAdherenceAssessmentdoneYes" type="radio" value="Yes" runat="server"
                                                                                        name="MotherAdherenceAssessmentdone" />
                                                                                    <label for="rdoMotherAdherenceAssessmentdoneYes">
                                                                                        Yes</label>
                                                                                    <input id="rdoMotherAdherenceAssessmentdoneNo" type="radio" value="No" runat="server"
                                                                                        name="MotherAdherenceAssessmentdone" />
                                                                                    <label for="rdoMotherAdherenceAssessmentdoneNo">
                                                                                        No</label>
                                                                                </div>
                                                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Have you missed any doses:</label>
                                                                                </div>
                                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                                    <input id="rdoMissedanydosesYes" type="radio" value="Yes" runat="server" name="Missedanydoses" />
                                                                                    <label for="rdoMissedanydosesYes">
                                                                                        Yes</label>
                                                                                    <input id="rdoMissedanydosesNo" type="radio" value="No" runat="server" name="Missedanydoses" />
                                                                                    <label for="rdoMissedanydosesNo">
                                                                                        No</label>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <%--left--%>
                                                                                <div class="col-xs-6">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label">
                                                                                                Barriers to adherence:</label>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                                            <div id="div25" class="customdivbordermultiselect">
                                                                                                <asp:Panel ID="pnlBarriertoadherence" runat="server">
                                                                                                </asp:Panel>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <%--right--%>
                                                                                <div class="col-xs-6">
                                                                                    <div class="row" id="trMisseddoses" style="display: none">
                                                                                        <div class="row">
                                                                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                                                                <label for="inputEmail3" class="control-label">
                                                                                                    <label id="Label94" runat="server" class="margin20">
                                                                                                        Number of doses missed last month:</label></label>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                                                                <asp:TextBox ID="txtNoofdosesmissed" runat="server" CssClass="form-control" Width="200"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                                                                <label for="inputEmail3" class="control-label">
                                                                                                    Reason for missed dose:</label>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                                                                <div id="div24" class="customdivbordermultiselect">
                                                                                                    <asp:Panel ID="pnlReasonmissdeddose" runat="server">
                                                                                                    </asp:Panel>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-12">
                                                                                <div class="row">
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            <label id="Label97" runat="server">
                                                                                                Number of Home visits:</label></label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:TextBox ID="txtNofHomevisits" runat="server" CssClass="form-control" Width="200"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            <label id="Label98" runat="server">
                                                                                                Prioritise Home Visits:</label></label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <input id="rdoPrioritiseHomeVisitYes" type="radio" value="Yes" runat="server" name="PrioritiseHomeVisit" />
                                                                                        <label for="rdoPrioritiseHomeVisitYes">
                                                                                            Yes</label>
                                                                                        <input id="rdoPrioritiseHomeVisitNo" type="radio" value="No" runat="server" name="PrioritiseHomeVisit" />
                                                                                        <label for="rdoPrioritiseHomeVisitNo">
                                                                                            No</label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                            <label id="Label99" runat="server">
                                                                                                DOT:</label></label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <asp:TextBox ID="txtDOT" runat="server" CssClass="form-control" Width="200"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                        <label for="inputEmail3" class="control-label">
                                                                                        </label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1Pwp" runat="server" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                                                        Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton18" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label81" runat="server" Text="PwP"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2Pwp" runat="server" Style="overflow: hidden;">
                                                                        <div id="PWP" style="display: none">
                                                                            <div class="row">
                                                                                <%--Left--%>
                                                                                <div class="col-xs-6">
                                                                                    <div class="row">
                                                                                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label required">
                                                                                                Have you disclosed your HIV status:</label>
                                                                                        </div>
                                                                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                                            <input id="rdodisclosedHIVStatusYes" type="radio" value="Yes" runat="server" name="disclosedyourHIVstatus" />
                                                                                            <label for="rdodisclosedyourHIVstatusYes">
                                                                                                Yes</label>
                                                                                            <input id="rdodisclosedHIVStatusNo" type="radio" value="No" runat="server" name="disclosedyourHIVstatus" />
                                                                                            <label for="rdodisclosedyourHIVstatusNo">
                                                                                                No</label>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <%--Right--%>
                                                                                <div class="col-xs-6">
                                                                                    <div id="tdPnlHIVStatus" style="display: none;">
                                                                                        <div class="row">
                                                                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                                                                <label for="inputEmail3" class="control-label">
                                                                                                    If yes disclosed HIV Status to:
                                                                                                </label>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                                                                <div id="div26" class="customdivbordermultiselect">
                                                                                                    <asp:Panel ID="pnlHIVStatus" runat="server">
                                                                                                    </asp:Panel>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Additonal Notes:</label>
                                                                                </div>
                                                                                <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                                                                                    <asp:TextBox ID="txtAdditionalPWPNotes" runat="server" Style="resize: none;" CssClass="form-control"
                                                                                        TextMode="MultiLine" Height="60px" Width="90%"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        Condoms Issued:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <input id="rdoCondomsIssuedYes" type="radio" value="Yes" runat="server" name="CondomsIssued" />
                                                                                    <label for="rdoCondomsIssuedYes">
                                                                                        Yes</label>
                                                                                    <input id="rdoCondomsIssuedNo" type="radio" value="No" runat="server" name="CondomsIssued" />
                                                                                    <label for="rdoCondomsIssuedNo">
                                                                                        No</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <label for="inputEmail3" class="control-label">
                                                                                        PwP Messages given:</label>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                                    <input id="rdoPwpMessageGivenYes" type="radio" value="Yes" runat="server" name="PwpMessageGiven" />
                                                                                    <label for="rdoPwpMessageGivenYes">
                                                                                        Yes</label>
                                                                                    <input id="rdoPwpMessageGivenNo" type="radio" value="No" runat="server" name="PwpMessageGiven" />
                                                                                    <label for="rdoPwpMessageGivenNo">
                                                                                        No</label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1WHOStaging" runat="server" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                                                        Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton14" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label71" runat="server" Text="WHO Staging"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2WHOStaging" runat="server" Style="overflow: hidden;">
                                                                        <div id="WS" style="display: none">
                                                                            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                                                                <tr>
                                                                                    <td class="border pad5 whitebg" width="100%">
                                                                                        <UcWhoStaging:Uc2 ID="UCWHOStage" runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Pnl1TBScreening" runat="server" onclick="fnsetCollapseState();" CssClass="border center formbg"
                                                                        Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton19" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label88" runat="server" Text="TB Screening"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Pnl2TBScreening" runat="server" Style="overflow: hidden;">
                                                                        <div id="TBS" style="display: none">
                                                                            <span id="spTbassessment" style="display: none">
                                                                                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                                                                    <tr>
                                                                                        <td class="border pad5 whitebg">
                                                                                            <UcTBScreening:Uc1 ID="UCTBScreen" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </span>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl1TreatmentPlan" runat="server" onclick="fnsetCollapseState();"
                                                                        CssClass="border center formbg" Style="padding: 6px">
                                                                        <h5 class="forms" align="left">
                                                                            <asp:ImageButton ID="ImageButton20" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                            <asp:Label ID="Label103" runat="server" Text="Appointment and Admission"></asp:Label></h5>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnl2TreatmentPlan" runat="server" Style="overflow: hidden;">
                                                                        <div id="TreatP" style="display: none">
                                                                            <div class="row">
                                                                                <div class="col-xs-4">
                                                                                    <div class="row">
                                                                                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label">
                                                                                                <label id="lblARTPreparation" runat="server">
                                                                                                    ART Preparation (TPS):</label></label>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                                                                            <div id="div27" class="customdivbordermultiselect">
                                                                                                <asp:Panel ID="pnlARTPreparation" runat="server">
                                                                                                </asp:Panel>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-xs-8">
                                                                                    <div class="row">
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label required">
                                                                                                ARV Regimen:</label>
                                                                                        </div>
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <asp:DropDownList ID="ddlARVRegimen" runat="server" CssClass="form-control" Width="250">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label">
                                                                                                <label id="Label105" runat="server">
                                                                                                    Infant NVP issued:</label></label>
                                                                                        </div>
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <input id="rdoInfantNVPissuedYes" type="radio" value="Yes" runat="server" name="InfantNVPissued" />
                                                                                            <label for="rdoInfantNVPissuedYes">
                                                                                                Yes</label>
                                                                                            <input id="rdoInfantNVPissuedNo" type="radio" value="No" runat="server" name="InfantNVPissued" />
                                                                                            <label for="rdoInfantNVPissuedNo">
                                                                                                No</label>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label">
                                                                                                <label id="Label107" runat="server" class="required margin5">
                                                                                                    CTX:</label></label>
                                                                                        </div>
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <asp:DropDownList ID="ddlCTX" runat="server" onchange="fnotherCTXStopreason();" CssClass="form-control"
                                                                                                Width="250">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" id="divctx" style="display: none">
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label">
                                                                                                Stop reason:</label>
                                                                                        </div>
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <asp:TextBox ID="txtctxstopreason" runat="server" TextMode="SingleLine" CssClass="form-control"
                                                                                                Width="250"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <label for="inputEmail3" class="control-label">
                                                                                                Other Management:</label>
                                                                                        </div>
                                                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                                            <asp:TextBox ID="txtotherMgmt" runat="server" CssClass="form-control" TextMode="MultiLine"
                                                                                                Style="vertical-align: middle; resize: none;"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                <label for="inputEmail3" class="control-label">
                                                                    Appointment Date:</label>
                                                            </div>
                                                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                <div class="form-group">
                                                                    <div class="input-group date">
                                                                        <div class="input-group-addon">
                                                                            <i class="fa fa-calendar"></i>
                                                                        </div>
                                                                        <input type="text" class="form-control pull-left" id="txtPMTCTAppDate" clientidmode="Static"
                                                                            maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;"
                                                                            onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                                                                            onfocus="javascript:vDateType='3'" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                <label for="inputEmail3" class="control-label">
                                                                    Admitted to ward?</label>
                                                            </div>
                                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                                <input id="rdoAdmittedtowardPMTCTYes" type="radio" value="Yes" runat="server" name="AdmittedtowardPMTCT" />
                                                                <label for="rdoAdmittedtowardPMTCTYes">
                                                                    Yes</label>
                                                                <input id="rdoAdmittedtowardPMTCTNo" type="radio" value="No" runat="server" name="AdmittedtowardPMTCT" />
                                                                <label for="rdoAdmittedtowardPMTCTNo">
                                                                    No</label>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tdSpecifyWardAdmitted">
                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                            </div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                                <label for="inputEmail3" class="control-label">
                                                                    Specify Ward Admitted?</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlWardAdmitted" runat="server" CssClass="form-control" Width="180">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        </div>
                                                    </div>
                                                    <div class="row" align="center">
                                                        <div id="Div1" style="display: inline; margin-right: 310px;">
                                                            <asp:Button ID="btnPharmacy" runat="server" Text="Pharmacy" Enabled="False" OnClick="btnPharmacy_Click"
                                                                CssClass="btn btn-primary" Height="30px" Width="11%" />
                                                            <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                                                color: #fff;">
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div class="row" align="center">
                                                        |
                                                    </div>
                                                    <div class="row" align="center">
                                                        <div class="border center formbg">
                                                            <table cellspacing="6" cellpadding="0" width="100%" border="0" id="tblPMTCT" runat="server">
                                                                <tr id="TrPMTCT" runat="server" align="center">
                                                                    <td id="tdPMTCT" runat="server" class="form">
                                                                        <asp:Button ID="btnSavePMTCT" runat="server" Text="Save" OnClick="btnSavePMTCT_Click"
                                                                            Enabled="False" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                        <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                                                            vertical-align: sub; color: #fff;">
                                                                        </label>
                                                                        <asp:Button ID="btnClosePMTCT" runat="server" Text="Close" OnClick="btnClosePMTCT_Click"
                                                                            Enabled="False" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                        <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                                                            vertical-align: sub; color: #fff;">
                                                                        </label>
                                                                        <asp:Button ID="btnPrintPMTCT" runat="server" Text="Print" OnClientClick="WindowPrint()"
                                                                            Enabled="False" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                                        <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                                                            color: #fff;">
                                                                        </label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    </div>
                                                    <act:CollapsiblePanelExtender ID="CPEManagementHIVPositiveClientOtherFacility" runat="server"
                                                        SuppressPostBack="True" ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2ManagementHIVPositiveClientOtherFacility"
                                                        CollapseControlID="pnl1ManagementHIVPositiveClientOtherFacility" ExpandControlID="pnl1ManagementHIVPositiveClientOtherFacility"
                                                        CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                                                        BehaviorID="_content_CPEManagementHIVPositiveClientOtherFacility"></act:CollapsiblePanelExtender>
                                                    <act:CollapsiblePanelExtender ID="CPEAdherence" runat="server" SuppressPostBack="True"
                                                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2Adherence" CollapseControlID="pnl1Adherence"
                                                        ExpandControlID="pnl1Adherence" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                                        ImageControlID="ImgPC" BehaviorID="_content_CPEAdherence"></act:CollapsiblePanelExtender>
                                                    <act:CollapsiblePanelExtender ID="CPEPwp" runat="server" SuppressPostBack="True"
                                                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2Pwp" CollapseControlID="pnl1Pwp"
                                                        ExpandControlID="pnl1Pwp" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                                        ImageControlID="ImgPC" BehaviorID="_content_CPEPwp"></act:CollapsiblePanelExtender>
                                                    <%-- <act:CollapsiblePanelExtender ID="CPEPMTCTInteventions" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2PMTCTInteventions"
                            CollapseControlID="pnl1PMTCTInteventions" ExpandControlID="pnl1PMTCTInteventions"
                            CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                            BehaviorID="_content_CPEPMTCTInteventions"></act:CollapsiblePanelExtender>--%>
                                                    <act:CollapsiblePanelExtender ID="CPETreatmentPlan" runat="server" SuppressPostBack="True"
                                                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2TreatmentPlan" CollapseControlID="pnl1TreatmentPlan"
                                                        ExpandControlID="pnl1TreatmentPlan" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                                        ImageControlID="ImgPC" BehaviorID="_content_CPETreatmentPlan"></act:CollapsiblePanelExtender>
                                                    <%--<act:CollapsiblePanelExtender ID="CPEPMTCTDiagnosisPlan" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2PMTCTDiagnosisPlan"
                            CollapseControlID="pnl1PMTCTDiagnosisPlan" ExpandControlID="pnl1PMTCTDiagnosisPlan"
                            CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgPC"
                            BehaviorID="_content_CPEPMTCTDiagnosisPlan"></act:CollapsiblePanelExtender>--%>
                                                    <act:CollapsiblePanelExtender ID="CPEWHOStaging" runat="server" SuppressPostBack="True"
                                                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnl2WHOStaging" CollapseControlID="pnl1WHOStaging"
                                                        ExpandControlID="pnl1WHOStaging" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                                        ImageControlID="ImgPC" BehaviorID="_content_CPEWHOStaging"></act:CollapsiblePanelExtender>
                                                    <act:CollapsiblePanelExtender ID="CPETBScreening" runat="server" SuppressPostBack="True"
                                                        ExpandedImage="~/images/arrow-dn.gif" TargetControlID="Pnl2TBScreening" CollapseControlID="Pnl1TBScreening"
                                                        ExpandControlID="Pnl1TBScreening" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                                        ImageControlID="ImgPC" BehaviorID="_content_CPETBScreening"></act:CollapsiblePanelExtender>
                                                    <br />
                                                </ContentTemplate>
                                            </act:TabPanel>
                                        </act:TabContainer>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
