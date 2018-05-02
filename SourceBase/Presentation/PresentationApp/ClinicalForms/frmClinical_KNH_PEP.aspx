<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    CodeBehind="frmClinical_KNH_PEP.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_KNH_PEP" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="UcVitalSign" TagName="Uc1" Src="~/ClinicalForms/UserControl/UserControlKNH_VitalSigns.ascx" %>
<%@ Register Src="UserControl/UserControlKNH_DrugAllergies.ascx" TagName="UserControlKNH_DrugAllergies"
    TagPrefix="uc1" %>
<%@ Register Src="UserControl/UserControlKNH_LabEvaluation.ascx" TagName="UserControlKNH_LabEvaluation"
    TagPrefix="uc2" %>
<%--<%@ Register Src="UserControl/UserControlKNH_Extruder.ascx" TagName="UserControlKNH_Extruder"
    TagPrefix="uc3" %>--%>
<%@ Register Src="UserControl/UserControlKNH_PwP.ascx" TagName="UserControlKNH_PwP"
    TagPrefix="uc4" %>
<%@ Register Src="UserControl/UserControlKNH_Signature.ascx" TagName="UserControlKNH_Signature"
    TagPrefix="uc5" %>
<%@ Register Src="UserControl/UserControlKNH_BackToTop.ascx" TagName="UserControlKNH_BackToTop"
    TagPrefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">        buildWeeklyCalendar(0);</script>
    <%--<link href="../../Touch/styles/mbExtruder.css" media="all" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" language="javascript">

        //            $(function () {

        //                $("#tabs").tabs();

        //            });

        function ShowHide(theDiv, YN, theFocus) {

            $(document).ready(function () {

                if (YN == "show") {
                    $("#" + theDiv).show();

                }
                if (YN == "hide") {
                    $("#" + theDiv).hide();

                }

            });

        }
        function ShowMore(sender, eventArgs) {
            var substr = eventArgs._commandArgument.toString().split('|')
            ShowHide(substr[0], substr[1]);
        }

        function rblSelectedValue(val, divID) {
            var selectedvalue = val;
            var YN = "";
            if (selectedvalue == "1") {
                YN = "show";
            }
            else {
                YN = "hide";
            }
            ShowHide(divID, YN);

        }
        function fnReset(ctrlid, ctrltype) {
            if (ctrltype == "dd") {
                document.getElementById(ctrlid).selectedIndex = 0;
            }
            else if (ctrltype == "txt") {
                document.getElementById(ctrlid).value = "";
            }


        }

        function CheckBoxToggleMedicalConditions() {

            var checkList = document.getElementById('ctl00_IQCareContentPlaceHolder_tabControlKNHPEP_tbpnlTriage_cblMedicalConditions');
            var checkBoxList = checkList.getElementsByTagName("input");
            var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
            var checkBoxSelectedItems = new Array();

            for (var i = 0; i < checkBoxList.length; i++) {

                if (checkBoxList[i].checked) {
                    if (arrayOfCheckBoxLabels[i].innerText == 'Other') {
                        ShowHide("divothercondition", "show");
                    }
                    else {
                        ShowHide("divothercondition", "hide");
                    }
                }
                else {
                    ShowHide("divothercondition", "hide");
                }
            }



        }

        function CheckBoxToggleShortTerm() {

            var checkList = document.getElementById('ctl00_IQCareContentPlaceHolder_tabControlKNHPEP_tbpnlClinicalAssessment_cblshorttermeffects');
            var checkBoxList = checkList.getElementsByTagName("input");
            var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
            var checkBoxSelectedItems = new Array();

            for (var i = 0; i < checkBoxList.length; i++) {

                if (checkBoxList[i].checked) {
                    if (arrayOfCheckBoxLabels[i].innerText == 'Other Specify') {
                        ShowHide("divothershorttermeffects", "show");
                    }
                    else {
                        ShowHide("divothershorttermeffects", "hide");
                    }
                }
                else {
                    ShowHide("divothershorttermeffects", "hide");
                }
            }



        }
        function CheckBoxTogglelongTerm() {

            var checkList = document.getElementById('ctl00_IQCareContentPlaceHolder_tabControlKNHPEP_tbpnlClinicalAssessment_cbllongtermeffects');
            var checkBoxList = checkList.getElementsByTagName("input");
            var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
            var checkBoxSelectedItems = new Array();

            for (var i = 0; i < checkBoxList.length; i++) {

                if (checkBoxList[i].checked) {
                    if (arrayOfCheckBoxLabels[i].innerText == 'Other specify') {
                        ShowHide("divspecityotherlogntermeffects", "show");
                    }
                    else {
                        ShowHide("divspecityotherlogntermeffects", "hide");
                    }
                }
                else {
                    ShowHide("divspecityotherlogntermeffects", "hide");
                }
            }



        }


        function dropdownchange(ddl, theDiv) {

            if (ddl.selectedIndex == "2" || ddl.selectedIndex == "4" || ddl.selectedIndex == "3") {
                ShowHide(theDiv, "show");
            }
            else {
                ShowHide(theDiv, "hide");
            }
        }

        function dropdownchangetext(ddl, theDiv, txt, ctrl) {

            var e = document.getElementById(ddl);
            //var strtxt = e.options[e.selectedIndex].innerText;
            var strtxt = e.options[e.selectedIndex].innerHTML;
            if (strtxt == txt) {
                ShowHide(theDiv, "show");
            }
            else {
                ShowHide(theDiv, "hide");
                document.getElementById(ctrl).value = "";
            }
        }

        function ClearTabData(TabId) {
            var tabhid1 = document.getElementById("<%=hidtab1.ClientID%>").value;
            var TabArray1 = tabhid1.split("^");
            var TabDataNew = "";
            for (var i = 0; i < TabArray1.length; i++) {
                var ctrltype = TabArray1[i].split('_');

                var newctrltype = ctrltype[4].substring(0, 3);
                if (newctrltype.toUpperCase() == "TXT") {
                    if (ctrltype[3] == TabId) {
                        document.getElementById(TabArray1[i]).value = "";
                    }
                }
                else if (newctrltype.toUpperCase() == "RDO") {
                    if (ctrltype[3] == TabId) {
                        if (document.getElementById(TabArray1[i]).checked == true) {
                            document.getElementById(TabArray1[i]).checked = false;
                        }
                    }
                }
                else if (newctrltype.toUpperCase() == "DDL") {
                    if (ctrltype[3] == TabId) {
                        if (document.getElementById(TabArray1[i]).selectedIndex != 0) {
                            document.getElementById(TabArray1[i]).selectedIndex = 0;
                        }
                    }
                }
                else if (newctrltype.toUpperCase() == "CBL") {

                    if (ctrltype[3] == TabId) {
                        var checkList = document.getElementById(TabArray1[i]);
                        var checkBoxList = checkList.getElementsByTagName("input");
                        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
                        var checkBoxSelectedItems = new Array();

                        for (var k = 0; k < checkBoxList.length; k++) {

                            checkBoxList[k].checked = false;
                        }
                    }
                }

            }

        }


        function StringASCII(TabId) {



            var tabhid1 = document.getElementById("<%=hidtab1.ClientID%>").value;
            var TabArray1 = tabhid1.split("^");
            var TabDataNew = "";
            for (var i = 0; i < TabArray1.length; i++) {
                var ctrltype = TabArray1[i].split('_');

                var newctrltype = ctrltype[4].substring(0, 3);
                if (newctrltype.toUpperCase() == "TXT") {
                    if (ctrltype[3] == TabId) {
                        TabDataNew = TabDataNew + document.getElementById(TabArray1[i]).value;
                    }
                }
                else if (newctrltype.toUpperCase() == "RDO") {
                    if (ctrltype[3] == TabId) {
                        if (document.getElementById(TabArray1[i]).checked == true) {
                            TabDataNew = TabDataNew + document.getElementById(TabArray1[i]).value;
                        }
                    }
                }
                else if (newctrltype.toUpperCase() == "DDL") {
                    if (ctrltype[3] == TabId) {
                        if (document.getElementById(TabArray1[i]).selectedIndex != 0) {
                            TabDataNew = TabDataNew + document.getElementById(TabArray1[i]).selectedIndex;
                        }
                    }
                }
                else if (newctrltype.toUpperCase() == "CBL") {

                    if (ctrltype[3] == TabId) {


                        var checkList = document.getElementById(TabArray1[i]);
                        var checkBoxList = checkList.getElementsByTagName("input");
                        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
                        var checkBoxSelectedItems = new Array();

                        for (var k = 0; k < checkBoxList.length; k++) {

                            if (checkBoxList[k].checked) {


                                TabDataNew = TabDataNew + "chkTrue";


                            }
                        }
                    }
                }

            }


            document.getElementById("<%= hdnStringASCIIValue.ClientID%>").value = TabDataNew;


        }
        function ValidateSave(sender, args) {

            var PrevTabName = document.getElementById("<%=hdnPrevTabName.ClientID%>").value;
            var TabId = document.getElementById("<%=hdnPrevTabId.ClientID%>").value;
            var PrevTabIndex = document.getElementById("<%=hdnPrevTabIndex.ClientID%>").value;
            var tabhid1 = document.getElementById("<%=hidtab1.ClientID%>").value;
            var TabArray1 = tabhid1.split("^");
            var TabDataNew = "";
            for (var i = 0; i < TabArray1.length; i++) {


                var ctrltype = TabArray1[i].split('_');

                var newctrltype = ctrltype[4].substring(0, 3);
                if (newctrltype.toUpperCase() == "TXT") {
                    if (ctrltype[3] == TabId) {
                        TabDataNew = TabDataNew + document.getElementById(TabArray1[i]).value;
                    }
                }
                else if (newctrltype.toUpperCase() == "RDO") {
                    if (ctrltype[3] == TabId) {
                        if (document.getElementById(TabArray1[i]).checked == true) {
                            TabDataNew = TabDataNew + document.getElementById(TabArray1[i]).value;
                        }
                    }
                }
                else if (newctrltype.toUpperCase() == "DDL") {
                    if (ctrltype[3] == TabId) {
                        if (document.getElementById(TabArray1[i]).selectedIndex != 0) {
                            TabDataNew = TabDataNew + document.getElementById(TabArray1[i]).selectedIndex;
                        }
                    }
                }
                else if (newctrltype.toUpperCase() == "CBL") {

                    if (ctrltype[3] == TabId) {
                        var checkList = document.getElementById(TabArray1[i]);
                        var checkBoxList = checkList.getElementsByTagName("input");
                        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
                        var checkBoxSelectedItems = new Array();

                        for (var k = 0; k < checkBoxList.length; k++) {

                            if (checkBoxList[k].checked) {
                                //TabDataNew = TabDataNew + arrayOfCheckBoxLabels[k].innerText;
                                TabDataNew = TabDataNew + "chkTrue";
                            }
                        }
                    }
                }

            }


            var PrevTabData = document.getElementById("<%= hdnStringASCIIValue.ClientID%>").value



            if (TabDataNew != PrevTabData) {

                var userSelectedYesElement = confirm("" + PrevTabName.trim() + " Tab has unsaved data. Do you want to save?");
                //get the hidden field reference:
                var CurrenttabId = sender.get_activeTab().get_id().split('_');
                var CurrentTabIndex = sender._activeTabIndex;
                var CurrentTabName = sender.get_activeTab()._header.innerHTML;
                CurrenttabId = CurrenttabId[3];
                document.getElementById("<%=hdnCurrentTabId.ClientID%>").value = CurrenttabId;
                document.getElementById("<%=hdnSaveTabData.ClientID%>").value = userSelectedYesElement;
                document.getElementById("<%=hdnCurrenTabName.ClientID%>").value = CurrentTabName;
                //document.getElementById("<%=hdnCurrenTabIndex.ClientID%>").value = CurrentTabIndex;
                if (userSelectedYesElement) {
                    document.getElementById("<%=hdnCurrenTabIndex.ClientID%>").value = CurrentTabIndex;
                    document.getElementById("<%=hidtab2.ClientID %>").value = PrevTabName.trim();
                    var clickButton = document.getElementById('<%=btnsave.ClientID %>');
                    clickButton.click();
                }
                else {
                    document.getElementById("<%=hdnPrevTabIndex.ClientID%>").value = CurrentTabIndex;
                    document.getElementById("<%=hdnPrevTabId.ClientID%>").value = CurrenttabId;
                    document.getElementById("<%=hdnPrevTabName.ClientID%>").value = CurrentTabName;
                    if (document.getElementById("<%=hdnVisitId.ClientID%>").value == "0") {
                        ClearTabData(TabId);
                    }
                    else {

                        StringASCII(CurrenttabId);
                    }
                }

                return userSelectedYesElement;
            }
            else {
                var CurrenttabId = sender.get_activeTab().get_id().split('_');
                var CurrentTabName = sender.get_activeTab()._header.innerHTML;
                var CurrentTabIndex = sender._activeTabIndex;
                CurrenttabId = CurrenttabId[3];
                document.getElementById("<%=hdnSaveTabData.ClientID%>").value = false;
                document.getElementById("<%=hdnPrevTabId.ClientID%>").value = CurrenttabId;
                document.getElementById("<%=hdnPrevTabName.ClientID%>").value = CurrentTabName;
                document.getElementById("<%=hdnPrevTabIndex.ClientID%>").value = CurrentTabIndex;
                StringASCII(CurrenttabId);
            }
        }


        function disableButton(buttonID) {
            document.getElementById(buttonID).disabled = true;
        }

        function enableButton(buttonID) {
            document.getElementById(buttonID).disabled = false;
        }

        function fnUncheckallMedCond(strcblcontrolId) {
            var checkList = document.getElementById(strcblcontrolId);
            var checkBoxList = checkList.getElementsByTagName("input");
            var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
            var checkBoxSelectedItems = new Array();

            for (var i = 1; i < checkBoxList.length; i++) {
                checkBoxList[i].checked = false;
            }
        }
        function fnUncheckNoneMedCond(strcblcontrolId) {
            var checkList = document.getElementById(strcblcontrolId);
            var checkBoxList = checkList.getElementsByTagName("input");
            var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
            var checkBoxSelectedItems = new Array();
            checkBoxList[0].checked = false;

        }

    </script>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
    <%--<asp:UpdatePanel ID="KNH" runat="server">
            <ContentTemplate>--%>
    <div class="container-fluid">
        <div id="divhidden" runat="server">
            <div id="divknhpep" runat="server">
                <table width="100%" class="table-condensed">
                    <tbody>
                        <tr>
                            <td colspan="2" class="border pad5 whitebg" align="center">
                                <label class="required">
                                    Visit date:
                                </label>
                                <input id="txtVisitDate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                    onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="VisitDate"
                                    runat="server" />
                                <img id="Img3" onclick="w_displayDatePicker('<%=txtVisitDate.ClientID%>');" height="22 "
                                    alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0"
                                    name="appDateimg" style="vertical-align: bottom; margin-bottom: 2px;" />
                                <span class="smallerlabel" id="Span3">(DD-MMM-YYYY)</span>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <act:TabContainer ID="tabControlKNHPEP" runat="server" ActiveTabIndex="0" Width="100%" CssClass="ajax__myTab">
                    <act:TabPanel ID="tbpnlTriage" runat="server" HeaderText="Triage">
                        <HeaderTemplate>
                            Triage</HeaderTemplate>
                        <ContentTemplate>
                            <div id="tab1" class="border center formbg">
                                <div class="center formbg">
                                    <table class="center formbg table-condensed" width="100%">
                                        <tr>
                                            <td colspan="2" class="border center formbg">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlheaderstarttime" runat="server">
                                                                <table width="100%">
                                                                    <tr align="left">
                                                                        <td>
                                                                            <h4 class="h4 forms" align="left">
                                                                                <asp:ImageButton ID="imgstarttime" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                                <asp:Label ID="lblClientInfo" runat="server" Text="Client Information"></asp:Label>
                                                                            </h4>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="center formbg">
                                    <asp:Panel ID="pnlstarttime" runat="server">
                                        <table id="Table4" class="border center formbg table-condensed" width="100%">
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td width="50%" align="left">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 65%" align="right">
                                                                                <label class="required">
                                                                                    <asp:Label ID="lblPtnCareGiver" runat="server" Text="Patient accompanied by caregiver:"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td style="width: 35%" align="left">
                                                                                <input id="rdopatientcaregiverYes" runat="server" name="patientcaregiver" onclick="down(this);rblSelectedValue(1,'divcaregiver');"
                                                                                    onfocus="up(this);" onmouseup="up(this);" type="radio" />
                                                                                </input></input></input><label>Yes</label>
                                                                                <input id="rdopatientcaregiverNo" runat="server" name="patientcaregiver" onclick="down(this);rblSelectedValue(0,'divcaregiver');fnReset('ctl00_IQCareContentPlaceHolder_tabControlKNHPEP_tbpnlTriage_ddlcaregiverrelationship','dd')"
                                                                                    onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                                </input> </input> </input> </input> </input> </input> </input>
                                                                                <label>
                                                                                    No</label>
                                                                                </input> </input>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <div id="divcaregiver" style="display: none;">
                                                                    <table width="100%" class="table-condensed">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 40%" align="right">
                                                                                    <label>
                                                                                        Caregiver relationship:
                                                                                    </label>
                                                                                </td>
                                                                                <td style="width: 60%" align="left">
                                                                                    <asp:DropDownList runat="server" ID="ddlcaregiverrelationship">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" width="50%">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 65%" align="right">
                                                                                <label>
                                                                                    Have you been referred from another facility:</label>
                                                                            </td>
                                                                            <td style="width: 35%" align="left">
                                                                                <input id="rdorefferedfacilityYes" runat="server" name="refferedfromanotherfacility"
                                                                                    onclick="down(this);rblSelectedValue(1,'divspecity');" onfocus="up(this);" onmouseup="up(this);"
                                                                                    type="radio" />
                                                                                </input></input></input></input></input><label>Yes</label>
                                                                                <input id="rdorefferedfacilityNO" runat="server" name="refferedfromanotherfacility"
                                                                                    onclick="down(this);rblSelectedValue(0,'divspecity');fnReset('ctl00_IQCareContentPlaceHolder_tabControlKNHPEP_tbpnlTriage_txtspecity','txt');"
                                                                                    onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                                </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                                </input> </input> </input> </input> </input> </input> </input>
                                                                                <label>
                                                                                    No</label>
                                                                                </input>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <div id="divspecity" style="display: none;">
                                                                    <table width="100%" class="table-condensed">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 40%" align="right">
                                                                                    <label>
                                                                                        If yes, specify :</label>
                                                                                </td>
                                                                                <td style="width: 60%" align="left">
                                                                                    <asp:TextBox runat="server" ID="txtspecity"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="trLMP">
                                                <td class="border center pad5 whitebg" colspan="2" style="width: 100%">
                                                    <table width="100%" class="table-condensed">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 50%" align="right">
                                                                    <label>
                                                                        LMP:</label>
                                                                </td>
                                                                <td style="width: 50%" align="left">
                                                                    <input id="txtdtLMP" runat="server" maxlength="11" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                        onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                                        size="11" type="text" />
                                                                    </input></input> </input></input></input></input>
                                                                    <img id="Img1" alt="Date Helper" border="0" height="22 " hspace="5" name="appDateimg"
                                                                        onclick="w_displayDatePicker('<%=txtdtLMP.ClientID%>');" src="../images/cal_icon.gif"
                                                                        width="22" /><span id="Span1" class="smallerlabel">(DD-MMM-YYYY)</span>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                                <div class="center formbg">
                                    <table class=" table-condensed center formbg" width="100%">
                                        <tr>
                                            <td class="border center formbg">
                                                <table width="100%" class="table-condensed">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlvitalsign" runat="server">
                                                                <table width="100%">
                                                                    <tr align="left">
                                                                        <td>
                                                                            <h4 class="h4 forms" align="left">
                                                                                <asp:ImageButton ID="imgvitalsign" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                                <asp:Label ID="lblVitalSigns" runat="server" Text="Vital Signs"></asp:Label>
                                                                            </h4>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100%">
                                                <asp:Panel ID="pnlVitalSignDetail" runat="server">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td>
                                                                <UcVitalSign:Uc1 ID="idVitalSign" runat="server"></UcVitalSign:Uc1>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="center formbg">
                                    <table class="center formbg" width="100%">
                                        <tr>
                                            <td class="border center formbg">
                                                <table width="100%" class="table-condensed">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlpreexistingknow" runat="server">
                                                                <table width="100%">
                                                                    <tr align="left">
                                                                        <td>
                                                                            <h4 class="h4 forms" align="left">
                                                                                <asp:ImageButton ID="imgpreexisitnknow" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                                <asp:Label ID="lblPreExstConds" runat="server" Text="Pre - Existing Known Conditions"></asp:Label>
                                                                            </h4>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="center formbg">
                                    <asp:Panel ID="pnlpreexistingknown" runat="server">
                                        <table id="Table1" class=" table-condensed border center formbg" width="100%">
                                            <tr>
                                                <td class="border pad6 whitebg" width="50%" align="center" colspan="2">
                                                    <label id="Label5">
                                                        <asp:Label ID="lblAccFirstDose" runat="server" CssClass="required" Text="From exposure, time taken to access 1st dose:"></asp:Label>
                                                    </label>
                                                    <asp:DropDownList runat="server" ID="ddlexposure">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 50%">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td align="left" width="100%">
                                                                <table width="100%" class=" table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 100%" align="left">
                                                                                <label class="required">
                                                                                    <asp:Label ID="lblMedicalConditions" runat="server" Text="Medical conditions:"></asp:Label>
                                                                                </label>
                                                                                <div id="div1" runat="server">
                                                                                    <div class="customdivbordermultiselect" id="divPresenting" nowrap="noWrap">
                                                                                        <asp:CheckBoxList ID="cblMedicalConditions" onclick="CheckBoxToggleMedicalConditions();"
                                                                                            RepeatLayout="Flow" runat="server">
                                                                                        </asp:CheckBoxList>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <div id="divothercondition" style="display: none;">
                                                                    <table width="100%" class=" table-condensed">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 40%" align="left">
                                                                                    <label>
                                                                                        Other medical conditions:</label>
                                                                                </td>
                                                                                <td style="width: 60%" align="left">
                                                                                    <asp:TextBox runat="server" ID="txtothermedicalconditions" Width="100%"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="border center pad5 whitebg" style="width: 50%" valign="top">
                                                    <table width="100%" class=" table-condensed">
                                                        <tr>
                                                            <td valign="top" align="left">
                                                                <label id="Label6">
                                                                    Pre existing conditions additional notes:</label>
                                                            </td>
                                                        </tr>
                                                        <tr valign="top">
                                                            <td>
                                                                <asp:TextBox ID="txtpreconditionsnotes" runat="server" TextMode="MultiLine" Width="100%"
                                                                    Rows="11"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>
                            <act:CollapsiblePanelExtender ID="CPEStarttime" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlstarttime" CollapseControlID="pnlheaderstarttime"
                                ExpandControlID="pnlheaderstarttime" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="imgstarttime" Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPEVitalSign" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlVitalSignDetail" CollapseControlID="pnlVitalSign"
                                ExpandControlID="pnlVitalSign" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="imgVitalSign" Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPEPreexisting" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlpreexistingknown" CollapseControlID="pnlpreexistingknow"
                                ExpandControlID="pnlpreexistingknow" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="imgpreexisitnknow" Enabled="True"></act:CollapsiblePanelExtender>
                            <br />
                            <div class="border center formbg">
                                <table width="100%" class=" table-condensed">
                                    <tr>
                                        <td class="form" align="center">
                                            <uc5:UserControlKNH_Signature ID="UserControlKNH_SignatureTriage" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="tblSaveButton">
                                        <td class="form" align="center">
                                            <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Save" CssClass="btn btn-primary"
                                                Height="30px" Width="8%" Style="text-align: left;" />
                                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                                vertical-align: sub; color: #fff; margin-top: 1%;">
                                            </label>
                                            <asp:Button ID="btncomplete" runat="server" OnClick="btncomplete_Click" Text="Data Quality Check"
                                                Visible="False" CssClass="btn btn-primary" Height="30px" Width="14%" Style="text-align: left;" />
                                            <label class="glyphicon glyphicon-floppy-saved" style="margin-left: -3%; margin-right: 2%;
                                                vertical-align: sub; color: #fff; margin-top: 1%; visibility: hidden;">
                                            </label>
                                            <asp:Button ID="btnback" runat="server" OnClick="btnback_Click" Text="Close" CssClass="btn btn-primary"
                                                Height="30px" Width="8%" Style="text-align: left;" />
                                            <label class="glyphicon glyphicon-remove" style="margin-left: -3%; margin-right: 2%;
                                                vertical-align: sub; color: #fff; margin-top: 1%;">
                                            </label>
                                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" OnClientClick="WindowPrint()"
                                                Text="Print" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                            <label class="glyphicon glyphicon-print" style="margin-left: -3%; margin-right: 2%;
                                                vertical-align: sub; color: #fff; margin-top: 1%;">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr id="tblDeleteButton" style="display: none" align="center">
                                        <td align="center" class="form" width="100%">
                                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete"
                                                CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                                vertical-align: sub; color: #fff; margin-top: 1%;">
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </act:TabPanel>
                    <act:TabPanel ID="tbpnlClinicalAssessment" runat="server" HeaderText="Clinical Assessment">
                        <HeaderTemplate>
                            Clinical Assessment</HeaderTemplate>
                        <ContentTemplate>
                            <div id="tab2" class="border center formbg">
                                <div class="center formbg">
                                    <table class="center formbg table-condensed" width="100%">
                                        <tr>
                                            <td class="border center formbg">
                                                <table width="100%" class="table-condensed">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlheaderpastmedicalrecord" runat="server">
                                                                <table width="100%">
                                                                    <tr align="left">
                                                                        <td>
                                                                            <h4 class="h4 forms" align="left">
                                                                                <asp:ImageButton ID="imgpastmedicalrecord" ImageUrl="~/images/arrow-up.gif" runat="server" />Past
                                                                                Medical Record
                                                                            </h4>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="center formbg">
                                    <asp:Panel ID="pnlpastmedicalrecord" runat="server">
                                        <table id="Table2" class="border center formbg" width="100%" class="table-condensed">
                                            <tr>
                                                <td class="border pad6 whitebg" align="left" valign="top" colspan="2" width="100%">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td valign="top" align="left">
                                                                <label id="Label7">
                                                                    Past medical record:</label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtpostmedicalrecord" runat="server" TextMode="MultiLine" Width="100%"
                                                                    Rows="6"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                                <div class="center formbg">
                                    <table class="center formbg" width="100%">
                                        <tr>
                                            <td class="border center formbg">
                                                <table width="100%" class="table-condensed">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlheaderreasonforpep" runat="server">
                                                                <table width="100%">
                                                                    <tr align="left">
                                                                        <td>
                                                                            <h4 class="h4 forms" align="left">
                                                                                <asp:ImageButton ID="imgreasonforpep" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                                <asp:Label ID="lblReasonPEP" runat="server" Text="Reason for PEP"></asp:Label>
                                                                            </h4>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="center formbg">
                                    <asp:Panel ID="pnlreasonforpep" runat="server">
                                        <table id="Table3" class="table-condensed border center formbg" cellspacing="6" cellpadding="0"
                                            width="100%" border="0">
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td align="left" width="50%">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 40%" align="right">
                                                                                <label>
                                                                                    <asp:Label ID="lblOccupational" runat="server" Text="Occupational:"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td style="width: 60%" align="left">
                                                                                <asp:DropDownList ID="ddloccupational" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <div id="divotheroccupational" style="display: none;">
                                                                    <table width="100%" class="table-condensed">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 60%" align="right">
                                                                                    <label>
                                                                                        Specify other occupational PEP:</label>
                                                                                </td>
                                                                                <td style="width: 40%" align="left">
                                                                                    <asp:TextBox runat="server" ID="txtotherPEP"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td align="left" width="50%">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 40%" align="right">
                                                                                <label>
                                                                                    <asp:Label ID="lblBodyFluid" runat="server" Text="Body fluid involved:"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td style="width: 60%" align="left">
                                                                                <asp:DropDownList runat="server" ID="ddlbodyfluid">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td width="50%" align="left">
                                                                <div id="divbodyfluid" style="display: none;">
                                                                    <table width="100%" class="table-condensed">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 60%" align="right">
                                                                                    <label>
                                                                                        Specify other body fluid involved:
                                                                                    </label>
                                                                                </td>
                                                                                <td style="width: 40%" align="left">
                                                                                    <asp:TextBox ID="txtfluidother" runat="server"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td align="left" width="50%">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 40%" align="right">
                                                                                <label>
                                                                                    <asp:Label ID="lblNonOccupational" runat="server" Text="Non - occupational:"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td style="width: 60%" align="left">
                                                                                <asp:DropDownList runat="server" ID="ddlnonoccupational">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td width="50%" align="left">
                                                                <div id="divnonoccupational" style="display: none;">
                                                                    <table width="100%" class="table-condensed">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 60%" align="right">
                                                                                    <label>
                                                                                        Specify other non-occupational indication:
                                                                                    </label>
                                                                                </td>
                                                                                <td style="width: 40%" align="left">
                                                                                    <asp:TextBox runat="server" ID="txtnonoccupationalother"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td align="left" width="50%">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 40%" align="right">
                                                                                <label>
                                                                                    <asp:Label ID="lblSexualAssualt" runat="server" Text="Sexual assault:"></asp:Label>
                                                                                </label>
                                                                            </td>
                                                                            <td style="width: 60%" align="left">
                                                                                <asp:DropDownList runat="server" ID="ddlsexualassault">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <div id="divsexualassault" style="display: none;">
                                                                    <table width="100%" class="table-condensed">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 60%" align="right">
                                                                                    <label>
                                                                                        Specify other sexual assault:
                                                                                    </label>
                                                                                </td>
                                                                                <td style="width: 40%" align="left">
                                                                                    <asp:TextBox runat="server" ID="txtothersexual"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 50%">
                                                    <table width="100%" class="table-condensed">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 40%" align="right">
                                                                    <label>
                                                                        Action taken after exposure:</label>
                                                                </td>
                                                                <td style="width: 60%" align="left">
                                                                    <asp:DropDownList runat="server" ID="ddltactionafterexposure">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="border center pad5 whitebg" style="width: 50%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 50%" colspan="2">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td align="left">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <label>
                                                                                    PEP regimen:</label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList runat="server" ID="ddlpepregimen">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td align="left">
                                                                <div id="divotherpep" style="display: none;">
                                                                    <table width="100%" class="table-condensed">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <label>
                                                                                        Other PEP regimen:</label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtotherpepregimen" runat="server"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                            <td align="left">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <label>
                                                                                    Current PEP regimen start date:</label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <input id="datecurrentpepregimen" runat="server" maxlength="11" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                    onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                                                    size="11" type="text" />
                                                                                </input></input>
                                                                                <img id="Img2" alt="Date Helper" border="0" height="22 " hspace="5" name="appDateimg"
                                                                                    onclick="w_displayDatePicker('<%=datecurrentpepregimen.ClientID%>');" src="../images/cal_icon.gif"
                                                                                    width="22" /><span id="Span2" class="smallerlabel">(DD-MMM-YYYY)</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <label>
                                                                                    Current PEP regimen end date:</label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <input id="txtPEPRegimenEndDate" runat="server" maxlength="11" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                    onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                                                    size="11" type="text" />
                                                                                </input></input>
                                                                                <img id="Img4" alt="Date Helper" border="0" height="22 " hspace="5" name="appDateimg"
                                                                                    onclick="w_displayDatePicker('<%=txtPEPRegimenEndDate.ClientID%>');" src="../images/cal_icon.gif"
                                                                                    width="22" /><span id="Span4" class="smallerlabel">(DD-MMM-YYYY)</span>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td align="left" width="50%">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 40%" align="right">
                                                                                <label>
                                                                                    Days PEP dispensed so far:</label>
                                                                            </td>
                                                                            <td style="width: 60%" align="left">
                                                                                <asp:TextBox ID="txtdayspepdispensedsofar" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td width="50%" align="left">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 60%" align="right">
                                                                                <label>
                                                                                    Days PEP dispensed during this visit:</label>
                                                                            </td>
                                                                            <td style="width: 40%" align="left">
                                                                                <asp:TextBox ID="txtdayspepdispensedthisvisit" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                                <div class="center formbg">
                                    <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="border center formbg">
                                                <table width="100%" class="table-condensed">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlheaderdrugallergies" runat="server">
                                                                <table width="100%">
                                                                    <tr align="left">
                                                                        <td>
                                                                            <h4 class="forms" align="left">
                                                                                <asp:ImageButton ID="imgdrugallergies" ImageUrl="~/images/arrow-up.gif" runat="server" />Drug
                                                                                Allergies
                                                                            </h4>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="center formbg">
                                    <asp:Panel ID="pnldrugallergies" runat="server">
                                        <uc1:UserControlKNH_DrugAllergies ID="UserControlKNH_DrugAllergies1" runat="server">
                                        </uc1:UserControlKNH_DrugAllergies>
                                    </asp:Panel>
                                </div>
                                <div class="center formbg">
                                    <table class="center formbg" width="100%">
                                        <tr>
                                            <td class="border center formbg" width="100%">
                                                <table width="100%" class="table-condensed">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlheaderarvsideeffects" runat="server">
                                                                <table width="100%">
                                                                    <tr align="left">
                                                                        <td>
                                                                            <h4 class="h4 forms" align="left">
                                                                                <asp:ImageButton ID="imgARVSideEffects" ImageUrl="~/images/arrow-up.gif" runat="server" />ARV
                                                                                Side Effects
                                                                            </h4>
                                                                        </td>
                                                                        <td>
                                                                            <input id="rdoarvsideeffectsyes" runat="server" name="arvsideeffects" onclick="down(this);rblSelectedValue(1,'divshortermeffects');rblSelectedValue(1,'divlongtermeffectsshow');"
                                                                                onfocus="up(this);" onmouseup="up(this);" type="radio" visible="False"></input>
                                                                            </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                            </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                            </input>
                                                                            <input id="rdoarvsideeffectsno" runat="server" name="arvsideeffects" onclick="down(this);rblSelectedValue(0,'divshortermeffects');rblSelectedValue(0,'divlongtermeffectsshow');"
                                                                                onfocus="up(this);" onmouseup="up(this);" type="radio" visible="False"></input>
                                                                            </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                            </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                            </input> </input> </input>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="center formbg" id="divshortermeffects" style="display: block">
                                    <asp:Panel ID="pnlBodyarvsideeffects" runat="server">
                                        <table id="Table6" class="border center formbg" width="100%" class="table-condensed">
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 50%" valign="top">
                                                    <table width="100%" class="table-condensed">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 100%" align="left">
                                                                    <label>
                                                                        Short term effects:</label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%" align="left">
                                                                    <div id="divshortterm" runat="server">
                                                                        <div class="customdivbordermultiselect" id="div3" nowrap="noWrap">
                                                                            <asp:CheckBoxList ID="cblshorttermeffects" onclick="CheckBoxToggleShortTerm();" RepeatLayout="Flow"
                                                                                runat="server">
                                                                            </asp:CheckBoxList>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div id="divothershorttermeffects" style="display: none;">
                                                                        <table width="100%" class="table-condensed">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 25%" align="left">
                                                                                        <label>
                                                                                            Specify other:
                                                                                        </label>
                                                                                    </td>
                                                                                    <td style="width: 75%" align="left">
                                                                                        <asp:TextBox runat="server" ID="txtspecityothershortterm" Width="100%"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="border center pad5 whitebg" style="width: 50%" valign="top">
                                                    <table width="100%" class="table-condensed">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 20%" align="left">
                                                                    <label>
                                                                        Long term effects:</label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 80%" align="left">
                                                                    <div id="divlongtermeffects" runat="server">
                                                                        <div class="customdivbordermultiselect" id="div2" nowrap="noWrap">
                                                                            <asp:CheckBoxList ID="cbllongtermeffects" onclick="CheckBoxTogglelongTerm();" RepeatLayout="Flow"
                                                                                runat="server">
                                                                            </asp:CheckBoxList>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div id="divspecityotherlogntermeffects" style="display: none;">
                                                                        <table width="100%" class="table-condensed">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 25%" align="left">
                                                                                        <label>
                                                                                            Specify other:
                                                                                        </label>
                                                                                    </td>
                                                                                    <td style="width: 75%" align="left">
                                                                                        <asp:TextBox runat="server" ID="txtspecifyotherlongterm" Width="100%"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                                <div class="center formbg">
                                    <table class="center formbg" width="100%">
                                        <tr>
                                            <td class="border center formbg">
                                                <table width="100%" class="table-condensed">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlheaderadherence" runat="server">
                                                                <table width="100%">
                                                                    <tr align="left">
                                                                        <td>
                                                                            <h4 class="h4 forms" align="left">
                                                                                <asp:ImageButton ID="imgadherence" ImageUrl="~/images/arrow-up.gif" runat="server" />Adherence
                                                                            </h4>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="center formbg">
                                    <asp:Panel ID="pnlAdherence" runat="server">
                                        <table id="Table7" class="border center formbg" cellspacing="6" cellpadding="0" width="100%"
                                            border="0">
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="50%" align="left">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 50%" align="right">
                                                                                <label>
                                                                                    Have you missed any doses?:</label>
                                                                            </td>
                                                                            <td style="width: 50%" align="left">
                                                                                <input id="rdomisseddosesyes" runat="server" name="misseddoses" onclick="down(this);rblSelectedValue(1,'divdosesmissedlastweek');"
                                                                                    onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                                </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                                </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                                </input>
                                                                                <label>
                                                                                    Yes</label><input id="rdomisseddosesno" runat="server" name="misseddoses" onclick="down(this);rblSelectedValue(0,'divdosesmissedlastweek');fnReset('ctl00_IQCareContentPlaceHolder_tabControlKNHPEP_tbpnlClinicalAssessment_txtdosesmissed','txt');"
                                                                                        onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                                </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                                </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                                </input> </input> </input>
                                                                                <label>
                                                                                    No</label>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <div id="divdosesmissedlastweek" style="display: none;">
                                                                    <table width="100%" class="table-condensed">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 60%" align="right">
                                                                                    <label>
                                                                                        Doses missed last week (times per week) :
                                                                                    </label>
                                                                                </td>
                                                                                <td style="width: 40%" align="left">
                                                                                    <asp:TextBox runat="server" ID="txtdosesmissed"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="50%" align="left">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 50%" align="right">
                                                                                <label>
                                                                                    Have you vomited any doses?:</label>
                                                                            </td>
                                                                            <td style="width: 50%" align="left">
                                                                                <input id="vomitteddosesyes" runat="server" name="vomitteddoses" onclick="down(this);rblSelectedValue(1,'divdosesvomited');"
                                                                                    onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                                </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                                </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                                </input>
                                                                                <label>
                                                                                    Yes</label><input id="vomitteddosesno" runat="server" name="vomitteddoses" onclick="down(this);rblSelectedValue(0,'divdosesvomited');fnReset('ctl00_IQCareContentPlaceHolder_tabControlKNHPEP_tbpnlClinicalAssessment_txtdosesvomited','txt');"
                                                                                        onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                                </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                                </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                                </input> </input> </input>
                                                                                <label>
                                                                                    No</label>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <div id="divdosesvomited" style="display: none;">
                                                                    <table width="100%" class="table-condensed">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 60%" align="right">
                                                                                    <label>
                                                                                        Doses vomited last week (times per week) :
                                                                                    </label>
                                                                                </td>
                                                                                <td style="width: 40%" align="left">
                                                                                    <asp:TextBox runat="server" ID="txtdosesvomited"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td width="50%" align="left">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 50%" align="right">
                                                                                <label>
                                                                                    Have you delayed in any dose?:</label>
                                                                            </td>
                                                                            <td style="width: 50%" align="left">
                                                                                <input id="rdodelayedinanydoseyes" runat="server" name="delayeddose" onclick="down(this);rblSelectedValue(1,'divnodoseddelayed');"
                                                                                    onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                                </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                                </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                                </input>
                                                                                <label>
                                                                                    Yes</label><input id="rdodelayedinanydoseno" runat="server" name="delayeddose" onclick="down(this);rblSelectedValue(0,'divnodoseddelayed');fnReset('ctl00_IQCareContentPlaceHolder_tabControlKNHPEP_tbpnlClinicalAssessment_txtdosesdelayed','txt');"
                                                                                        onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                                </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                                </input> </input> </input> </input> </input> </input> </input> </input> </input>
                                                                                </input> </input> </input>
                                                                                <label>
                                                                                    No</label>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <div id="divnodoseddelayed" style="display: none;">
                                                                    <table width="100%" class="table-condensed">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 60%" align="right">
                                                                                    <label>
                                                                                        No of doses delayed last week :
                                                                                    </label>
                                                                                </td>
                                                                                <td style="width: 40%" align="left">
                                                                                    <asp:TextBox runat="server" ID="txtdosesdelayed"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                                <div class="center formbg">
                                    <table class="center formbg" width="100%">
                                        <tr>
                                            <td class="border center formbg">
                                                <table width="100%" class="table-condensed">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlLabEvaluationHeader" runat="server">
                                                                <table width="100%">
                                                                    <tr align="left">
                                                                        <td>
                                                                            <h4 class="h4 forms" align="left">
                                                                                <asp:ImageButton ID="imgLabEvaluation" ImageUrl="~/images/arrow-up.gif" runat="server" />Lab
                                                                                Evaluation
                                                                            </h4>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="center formbg">
                                    <asp:Panel ID="pnlLabEvaluationBody" runat="server">
                                        <uc2:UserControlKNH_LabEvaluation ID="UserControlKNH_LabEvaluation1" runat="server">
                                        </uc2:UserControlKNH_LabEvaluation>
                                    </asp:Panel>
                                </div>
                                <div class="center formbg">
                                    <table class="center formbg" width="100%">
                                        <tr>
                                            <td class="border center formbg">
                                                <table width="100%" class="table-condensed">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlclientsourcestatus" runat="server">
                                                                <table width="100%">
                                                                    <tr align="left">
                                                                        <td>
                                                                            <h4 class="h4 forms" align="left">
                                                                                <asp:ImageButton ID="imgclientstatus" ImageUrl="~/images/arrow-up.gif" runat="server" />Client
                                                                                and Source Patient Status
                                                                            </h4>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="center formbg">
                                    <asp:Panel ID="pnlclientstatus" runat="server">
                                        <table id="Table8" class="border center formbg" width="100%">
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%" class="table-condensed">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 40%" align="right">
                                                                    <label>
                                                                        HIV elisa result:
                                                                    </label>
                                                                </td>
                                                                <td style="width: 60%" align="left">
                                                                    <asp:DropDownList ID="ddlhivelisaresult" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td width="50%" align="left">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 60%" align="right">
                                                                                <label>
                                                                                    HIV status for client:</label>
                                                                            </td>
                                                                            <td style="width: 40%" align="left">
                                                                                <asp:DropDownList ID="ddlhivstatusforclient" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 60%" align="right">
                                                                                <label>
                                                                                    Source HIV status:
                                                                                </label>
                                                                            </td>
                                                                            <td style="width: 40%" align="left">
                                                                                <asp:DropDownList ID="ddlsourcehivstatus" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td width="50%" align="left">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 60%" align="right">
                                                                                <label>
                                                                                    Hepatitis B status for client:
                                                                                </label>
                                                                            </td>
                                                                            <td style="width: 40%" align="left">
                                                                                <asp:DropDownList ID="ddlhapatitisbstatus" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 60%" align="right">
                                                                                <label>
                                                                                    Source hepatitis B status:</label>
                                                                            </td>
                                                                            <td style="width: 40%" align="left">
                                                                                <asp:DropDownList ID="ddlsourcehepatitisbstatus" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 60%" align="right">
                                                                                <label>
                                                                                    Hepatitis C status for client:</label>
                                                                            </td>
                                                                            <td style="width: 40%" align="left">
                                                                                <asp:DropDownList ID="ddlhepatitiscstatus" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 60%" align="right">
                                                                                <label>
                                                                                    Source hepatitis C status:
                                                                                </label>
                                                                            </td>
                                                                            <td style="width: 40%" align="left">
                                                                                <asp:DropDownList ID="ddlsourcehepatitiscstatus" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 50%">
                                                    <table width="100%" class="table-condensed">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60%" align="right">
                                                                    <label>
                                                                        Has client completed HBV vaccination?:</label>
                                                                </td>
                                                                <td style="width: 40%" align="left">
                                                                    <input id="rdohbvvaccinationyes" runat="server" name="hbv" onclick="down(this);"
                                                                        onfocus="up(this);" onmouseup="up(this);" type="radio" />
                                                                    </input><label>Yes</label><input id="rdohbvvaccinationno" runat="server" name="hbv"
                                                                        onclick="down(this);" onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                    </input> </input> </input></input></input></input></input></input></input></input><label>No</label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="border center pad5 whitebg" style="width: 50%">
                                                    <table width="100%" class="table-condensed">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60%" align="right">
                                                                    <label>
                                                                        Discussed disclosure plan:
                                                                    </label>
                                                                </td>
                                                                <td style="width: 40%" align="left">
                                                                    <input id="rdodiscusseddisclosureyes" runat="server" name="disclosureplan" onclick="down(this);"
                                                                        onfocus="up(this);" onmouseup="up(this);" type="radio" />
                                                                    </input><label>Yes</label><input id="rdodiscusseddisclosureno" runat="server" name="disclosureplan"
                                                                        onclick="down(this);" onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                    </input> </input> </input></input></input></input></input></input></input></input><label>No</label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 50%">
                                                    <table width="100%" class="table-condensed">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60%" align="right">
                                                                    <label>
                                                                        Discussed safe sex practices:
                                                                    </label>
                                                                </td>
                                                                <td style="width: 40%" align="left">
                                                                    <input id="rdosafesexyes" runat="server" name="safesex" onclick="down(this);" onfocus="up(this);"
                                                                        onmouseup="up(this);" type="radio" />
                                                                    </input><label>Yes</label><input id="rdosafesexno" runat="server" name="safesex"
                                                                        onclick="down(this);" onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                    </input> </input> </input></input></input></input></input></input></input></input><label>No</label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="border center pad5 whitebg" style="width: 50%">
                                                    <table width="100%" class="table-condensed">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60%" align="right">
                                                                    <label>
                                                                        Adherence/supportive counselling:</label>
                                                                </td>
                                                                <td style="width: 40%" align="left">
                                                                    <input id="rdoadherencecounsellingyes" runat="server" name="adherence" onclick="down(this);"
                                                                        onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                    </input> </input> </input></input></input></input></input></input></input></input></input></input></input></input></input></input></input></input><label>Yes</label><input
                                                                        id="rdoadherencecounsellingno" runat="server" name="adherence" onclick="down(this);"
                                                                        onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                    </input> </input> </input></input></input></input></input></input></input></input></input></input></input></input></input></input></input></input></input></input><label>No</label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td width="50%" align="left">
                                                                <table width="100%" class="table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 60%" align="right">
                                                                                <label>
                                                                                    Condoms dispensed?:</label>
                                                                            </td>
                                                                            <td style="width: 40%" align="left">
                                                                                <input id="rdocondomsdispensedyes" runat="server" name="condoms" onclick="down(this);rblSelectedValue(0,'divreasonfornotissue');fnReset('ctl00_IQCareContentPlaceHolder_tabControlKNHPEP_tbpnlClinicalAssessment_txtreasonfornotissuecondoms','txt');"
                                                                                    onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                                </input> </input> </input></input></input></input></input></input></input></input></input></input></input></input></input></input></input></input><label>Yes</label><input
                                                                                    id="rdocondomsdispensedno" runat="server" name="condoms" onclick="down(this);rblSelectedValue(1,'divreasonfornotissue');"
                                                                                    onfocus="up(this);" onmouseup="up(this);" type="radio"></input>
                                                                                </input> </input> </input></input></input></input></input></input></input></input></input></input></input></input></input></input></input></input></input></input><label>No</label>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <div id="divreasonfornotissue" style="display: none;">
                                                                    <table width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 60%" align="right">
                                                                                    <label>
                                                                                        Reason for not issuing condoms :
                                                                                    </label>
                                                                                </td>
                                                                                <td style="width: 40%" align="left">
                                                                                    <asp:TextBox runat="server" ID="txtreasonfornotissuecondoms"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>
                            <act:CollapsiblePanelExtender ID="CPElpastmedicalrecord" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlpastmedicalrecord"
                                CollapseControlID="pnlheaderpastmedicalrecord" ExpandControlID="pnlheaderpastmedicalrecord"
                                CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="imgpastmedicalrecord"
                                Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPErreasonforpep" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlreasonforpep" CollapseControlID="pnlheaderreasonforpep"
                                ExpandControlID="pnlheaderreasonforpep" CollapsedImage="~/images/arrow-up.gif"
                                Collapsed="True" ImageControlID="imgreasonforpep" Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="cpedrugallergies" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnldrugallergies" CollapseControlID="pnlheaderdrugallergies"
                                ExpandControlID="pnlheaderdrugallergies" CollapsedImage="~/images/arrow-up.gif"
                                Collapsed="True" ImageControlID="imgdrugallergies" Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPEadherence" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlAdherence" CollapseControlID="pnlheaderadherence"
                                ExpandControlID="pnlheaderadherence" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="imgadherence" Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPEclientsourcestatus" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlclientstatus" CollapseControlID="pnlclientsourcestatus"
                                ExpandControlID="pnlclientsourcestatus" CollapsedImage="~/images/arrow-up.gif"
                                Collapsed="True" ImageControlID="imgclientstatus" Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPEARVSideEffects" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlBodyarvsideeffects"
                                CollapseControlID="pnlheaderarvsideeffects" ExpandControlID="pnlheaderarvsideeffects"
                                CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="imgARVSideEffects"
                                Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPELabEvaluation" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlLabEvaluationBody"
                                CollapseControlID="pnlLabEvaluationHeader" ExpandControlID="pnlLabEvaluationHeader"
                                CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="imgLabEvaluation"
                                Enabled="True"></act:CollapsiblePanelExtender>
                            <br />
                            <div class="border center formbg">
                                <table width="100%" class="table-condensed">
                                    <tr>
                                        <td class="form" align="center">
                                            <uc5:UserControlKNH_Signature ID="UserControlKNH_SignatureCA" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form" align="center">
                                            <asp:Button ID="btnCAsave" runat="server" Text="Save" OnClick="btnCAsave_Click" CssClass="btn btn-primary"
                                                Height="30px" Width="8%" Style="text-align: left;" />
                                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                                vertical-align: sub; color: #fff; margin-top: 1%;">
                                            </label>
                                            <asp:Button ID="btnCAcomplete" runat="server" Text="Data Quality Check" OnClick="btncomplete_Click"
                                                Visible="False" CssClass="btn btn-primary" Height="30px" Width="18%" Style="text-align: left;" />
                                            <label class="glyphicon glyphicon-floppy-saved" style="margin-left: -3%; margin-right: 2%;
                                                vertical-align: sub; color: #fff; margin-top: 1%; visibility:hidden;">
                                            </label>
                                            <asp:Button ID="btnCAback" runat="server" Text="Close" OnClick="btnCAback_Click"
                                                CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                            <label class="glyphicon glyphicon-remove" style="margin-left: -3%; margin-right: 2%;
                                                vertical-align: sub; color: #fff; margin-top: 1%;">
                                            </label>
                                            <asp:Button ID="btnCAPrint" runat="server" OnClientClick="WindowPrint()" Text="Print"
                                                CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                            <label class="glyphicon glyphicon-print" style="margin-left: -3%; margin-right: 2%;
                                                vertical-align: sub; color: #fff; margin-top: 1%;">
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </act:TabPanel>
                </act:TabContainer>
                <div class="border center formbg" style="display: none">
                    <table width="100%" class="table-condensed">
                        <tbody>
                            <tr>
                                <td class="border pad6 whitebg" width="100%" colspan="2" align="center">
                                    <label id="Label54">
                                        Signature:</label>
                                        <asp:HiddenField ID="hdfldDOB" runat="server" />
                                        <asp:HiddenField ID="theHitCntrl" runat="server" />
                                        <asp:HiddenField ID="hdnPrevTabId" runat="server" />
                                        <asp:HiddenField ID="hdnCurrentTabId" runat="server" />
                                        <asp:HiddenField ID="hdnPrevTabIndex" runat="server" />
                                        <asp:HiddenField ID="hdnCurrenTabIndex" runat="server" />
                                        <asp:HiddenField ID="hdnSaveTabData" runat="server" />
                                        <asp:HiddenField ID="hdnStringASCIIValue" runat="server" />
                                        <asp:HiddenField ID="hdnVisitId" runat="server" />
                                        <asp:HiddenField ID="hdnPrevTabName" runat="server" />
                                        <asp:HiddenField ID="hdnCurrenTabName" runat="server" />
                                        <asp:HiddenField ID="hidtab1" runat="server" />
                                        <asp:HiddenField ID="hidtab2" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <%--</ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsave"></asp:PostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>--%>
        <%--<uc3:UserControlKNH_Extruder ID="UserControlKNH_Extruder1" runat="server" />--%>
        <uc6:UserControlKNH_BackToTop ID="UserControlKNH_BackToTop1" runat="server" />
    </div>
</asp:Content>
