<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmClinical_KNH_PediatricFollowup.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_KNH_PediatricFollowup" %>

<%@ Register TagPrefix="UcVitalSign" TagName="Uc1" Src="~/ClinicalForms/UserControl/UserControlKNH_VitalSigns.ascx" %>
<%--<%@ Register TagPrefix="UcPastMedicalHistory" TagName="Uc2" Src="~/ClinicalForms/UserControl/UserControlKNH_PastMedicalHistory.ascx" %>--%>
<%@ Register TagPrefix="UcWHOStaging" TagName="Uc3" Src="~/ClinicalForms/UserControl/UserControlKNH_WHOStaging.ascx" %>
<%@ Register TagPrefix="UcPhysExam" TagName="Uc4" Src="~/ClinicalForms/UserControl/UserControlKNH_PhysicalExamination.ascx" %>
<%@ Register TagPrefix="UcAllergies" TagName="Uc5" Src="~/ClinicalForms/UserControl/UserControlKNH_DrugAllergies.ascx" %>
<%@ Register TagPrefix="UcPresComp" TagName="Uc6" Src="~/ClinicalForms/UserControl/UserControlKNHPresentingComplaints.ascx" %>
<%@ Register TagPrefix="UcTBScreen" TagName="Uc7" Src="~/ClinicalForms/UserControl/UserControlKNH_TBScreening.ascx" %>
<%@ Register TagPrefix="UcPharmacy" TagName="Uc8" Src="~/ClinicalForms/UserControl/UserControlKNH_Pharmacy.ascx" %>
<%@ Register TagPrefix="UcPWP" TagName="Uc9" Src="~/ClinicalForms/UserControl/UserControlKNH_PwP.ascx" %>
<%--<%@ Register src="UserControl/UserControlKNH_Extruder.ascx" tagname="UserControlKNH_Extruder" tagprefix="uc10" %>--%>
<%@ Register Src="~/ClinicalForms/UserControl/UserControlKNH_LabEvaluation.ascx"
    TagName="Uclabeval" TagPrefix="uc11" %>
<%@ Register Src="~/ClinicalForms/UserControl/UserControlKNH_Signature.ascx" TagName="UserControlKNH_Signature"
    TagPrefix="uc12" %>
<%@ Register Src="UserControl/UserControlKNH_BackToTop.ascx" TagName="UserControlKNH_BackToTop"
    TagPrefix="uc13" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <br />
    <div style="padding-left: 8px; padding-right: 8px; padding-top: 2px; width:100%" class="container">
        <script language="javascript" type="text/javascript">            buildWeeklyCalendar(0);</script>
        <script type="text/javascript" language="javascript">

            //            $(function () {

            //                $("#tabs").tabs();

            //            });
            function ShowHide(theDiv, YN) {

                $(document).ready(function () {

                    if (YN == "show") {
                        $("#" + theDiv).slideDown();

                    }
                    if (YN == "hide") {
                        $("#" + theDiv).slideUp();

                    }

                });

            }
            function ShowMore(sender, eventArgs) {
                var substr = eventArgs._commandArgument.toString().split('|')
                ShowHide(substr[0], substr[1]);
            }

            function rblSelectedValue(rbl, divID) {
                var selectedvalue = $("#" + rbl.id + " input:radio:checked").val();
                var YN = "";
                if (selectedvalue == "1") {
                    YN = "show";
                }
                else {
                    YN = "hide";
                }
                ShowHide(divID, YN);

            }
            function rblSelectedValue1(val, divID) {
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
            function rblNoSelectedValue(rbl, divID) {
                var selectedvalue = $("#" + rbl.id + " input:radio:checked").val();
                var YN = "";
                if (selectedvalue == "1") {
                    YN = "hide";
                }
                else {
                    YN = "show";
                }
                ShowHide(divID, YN);

            }

            function CheckBoxHideUnhide(strcblcontrolId, strdivId, strfieldname) {
                //alert(strcblcontrolId);
                var checkList = document.getElementById(strcblcontrolId);
                var checkBoxList = checkList.getElementsByTagName("input");
                var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
                var checkBoxSelectedItems = new Array();

                for (var i = 0; i < checkBoxList.length; i++) {

                    if (checkBoxList[i].checked) {
                        if (arrayOfCheckBoxLabels[i].innerHTML == strfieldname) {
                            ShowHide(strdivId, "show");
                        }
                        else {
                            ShowHide(strdivId, "hide");
                        }
                    }
                    else {
                        ShowHide(strdivId, "hide");
                    }
                }



            }
            function ShowHidePE(theDiv, YN, theFocus) {

                $(document).ready(function () {

                    if (YN == "show") {
                        $("#" + theDiv).slideDown();

                    }
                    if (YN == "hide") {
                        $("#" + theDiv).slideUp();

                    }

                });

            }

            function CheckBoxToggleShowHidePE(val, divID, txt) {
                var checkList = document.getElementById(val);
                var checkBoxList = checkList.getElementsByTagName("input");
                var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
                var checkBoxSelectedItems = new Array();
                var arrayOfCheckBoxLabelsnew;
                alert('aa');
                for (var i = 0; i < checkBoxList.length; i++) {
                    if (checkBoxList[i].checked) {
                        if (arrayOfCheckBoxLabels[i].innerHTML == txt) {
                            ShowHidePE(divID, "show");
                        }

                    }
                    else {

                        if (arrayOfCheckBoxLabels[i].innerHTML == txt) {
                            ShowHidePE(divID, "hide");
                        }
                    }
                }



            }

            function getSelectedValue(DivId, DDText, str) {
                var e = document.getElementById(DDText);
                var text = e.options[e.selectedIndex].innerHTML;
                var YN = "";
                if (text == str) {
                    YN = "show";
                }
                else {
                    YN = "hide";
                }
                ShowHide(DivId, YN);
            }
            function getSelectedtableValue(DivId, DDText, str, tableId) {
                var e = document.getElementById(DDText);
                var text = e.options[e.selectedIndex].innerHTML;
                var YN = "";
                if (text == str) {
                    YN = "show";
                }
                else {
                    YN = "hide";
                }
                ShowHide(tableId, "show");
                ShowHide(DivId, YN);
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

            function CheckBoxToggleShowHide(val, divID, txt) {

                var checkList = document.getElementById(val);
                var checkBoxList = checkList.getElementsByTagName("input");
                var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
                var checkBoxSelectedItems = new Array();

                for (var i = 0; i < checkBoxList.length; i++) {

                    if (checkBoxList[i].checked) {
                        if (arrayOfCheckBoxLabels[i].innerText == txt) {
                            ShowHide(divID, "show");
                        }

                    }
                    else {

                        if (arrayOfCheckBoxLabels[i].innerText == txt) {
                            ShowHide(divID, "hide");
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
                //                for (var i = 0; i < TabArray1.length; i++) {


                //                    var ctrltype = TabArray1[i].split('_');

                //                    var newctrltype = ctrltype[4].substring(0, 3);
                //                    if (newctrltype.toUpperCase() == "TXT") {
                //                        if (ctrltype[3] == TabId) {
                //                            TabDataNew = TabDataNew + document.getElementById(TabArray1[i]).value;
                //                        }
                //                    }
                //                    else if (newctrltype.toUpperCase() == "RDO") {
                //                        if (ctrltype[3] == TabId) {
                //                            if (document.getElementById(TabArray1[i]).checked == true) {
                //                                TabDataNew = TabDataNew + document.getElementById(TabArray1[i]).value;
                //                            }
                //                        }
                //                    }
                //                    else if (newctrltype.toUpperCase() == "DDL") {
                //                        if (ctrltype[3] == TabId) {
                //                            if (document.getElementById(TabArray1[i]).selectedIndex != 0) {
                //                                TabDataNew = TabDataNew + document.getElementById(TabArray1[i]).selectedIndex;
                //                            }
                //                        }
                //                    }
                //                    else if (newctrltype.toUpperCase() == "CBL") {

                //                        if (ctrltype[3] == TabId) {
                //                            var checkList = document.getElementById(TabArray1[i]);
                //                            var checkBoxList = checkList.getElementsByTagName("input");
                //                            var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
                //                            var checkBoxSelectedItems = new Array();

                //                            for (var k = 0; k < checkBoxList.length; k++) {

                //                                if (checkBoxList[k].checked) {
                //                                    TabDataNew = TabDataNew + "chkTrue";
                //                                }
                //                            }
                //                        }
                //                    }

                //                }


                //                var PrevTabData = document.getElementById("<%= hdnStringASCIIValue.ClientID%>").value

                //                if (TabDataNew != PrevTabData) {

                //                    var userSelectedYesElement = confirm("" + PrevTabName + " Tab has unsaved data. Do you want to save?");
                //                    //get the hidden field reference:
                //                    var CurrenttabId = sender.get_activeTab().get_id().split('_');
                //                    var CurrentTabIndex = sender._activeTabIndex;
                //                    var CurrentTabName = sender.get_activeTab()._header.innerHTML;
                //                    CurrenttabId = CurrenttabId[3];
                //                    document.getElementById("<%=hdnCurrentTabId.ClientID%>").value = CurrenttabId;
                //                    document.getElementById("<%=hdnSaveTabData.ClientID%>").value = userSelectedYesElement;
                //                    document.getElementById("<%=hdnCurrenTabName.ClientID%>").value = CurrentTabName;
                //                    //document.getElementById("<%=hdnCurrenTabIndex.ClientID%>").value = CurrentTabIndex;
                //                    if (userSelectedYesElement) {
                //                        document.getElementById("<%=hdnCurrenTabIndex.ClientID%>").value = CurrentTabIndex;
                //                        document.getElementById("<%=hidtab2.ClientID %>").value = PrevTabName;
                //                        
                //                    }
                //                    else {
                //                        document.getElementById("<%=hdnPrevTabIndex.ClientID%>").value = CurrentTabIndex;
                //                        document.getElementById("<%=hdnPrevTabId.ClientID%>").value = CurrenttabId;
                //                        document.getElementById("<%=hdnPrevTabName.ClientID%>").value = CurrentTabName;
                //                        if (document.getElementById("<%=hdnVisitId.ClientID%>").value == "0") {
                //                            ClearTabData(TabId);
                //                        }
                //                        else {

                //                            StringASCII(CurrenttabId);
                //                        }
                //                    }

                //                    return userSelectedYesElement;
                //                }
                //                else {
                //                    var CurrenttabId = sender.get_activeTab().get_id().split('_');
                //                    var CurrentTabName = sender.get_activeTab()._header.innerHTML;
                //                    var CurrentTabIndex = sender._activeTabIndex;
                //                    CurrenttabId = CurrenttabId[3];
                //                    document.getElementById("<%=hdnSaveTabData.ClientID%>").value = false;
                //                    document.getElementById("<%=hdnPrevTabId.ClientID%>").value = CurrenttabId;
                //                    document.getElementById("<%=hdnPrevTabName.ClientID%>").value = CurrentTabName;
                //                    document.getElementById("<%=hdnPrevTabIndex.ClientID%>").value = CurrentTabIndex;
                //                    StringASCII(CurrenttabId);
                //                }
            }    

        </script>
        <script type="text/javascript">
            function RegisterJQuery() {
                $('#txtvisitDate').datepicker({ autoclose: true });
                $('#txtDateOfDeathFather').datepicker({ autoclose: true });
                $('#txtDateOfDeathMother').datepicker({ autoclose: true });
                $('#txtTBrxCompleteDate').datepicker({ autoclose: true });
                $('#txtTBRetreatmentDate').datepicker({ autoclose: true });
                $('#dtAntifungalsDate').datepicker({ autoclose: true });
                $('#dtAntihypertensivesDate').datepicker({ autoclose: true });
                $('#dtSulfaTMPDate').datepicker({ autoclose: true });
                
            }
            //Calling RegisterJQuery when document is ready (Page loaded first time)
            $(document).ready(RegisterJQuery);
            //Calling RegisterJQuery when the page is doing postback (asp.net)
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>

<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title" id="Header"><asp:Label ID="lblHeading" runat="server" Text="Paediatric Follow up"></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">       
        <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label required">Visit date:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtvisitDate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
                                            </div>
                                  
             </div>
             <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
        <act:TabContainer ID="tabControl" runat="server" ActiveTabIndex="0" CssClass="ajax__myTab">
                    <act:TabPanel ID="tbpnlgeneral" runat="server" HeaderText="Triage">
                        <ContentTemplate>
                        <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlHIVCare" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="imgHIVCare" ImageUrl="~/images/arrow-up.gif" runat="server" />
          <asp:Label ID="lblClientInfo" runat="server" Text="Patient Information"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="pnlHivCareDetail" runat="server" style="overflow:hidden;">
        <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required"><asp:Label ID="lblPtnAccByCareGiver" runat="server" Text="Patient accompanied by caregiver:"></asp:Label></label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:RadioButtonList ID="rdopatientcaregiver" runat="server" RepeatDirection="Horizontal"
                                                                        OnClick="rblSelectedValue(this,'divcarrelationYN')">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList></label>
             </div>
              <div id="divcarrelationYN" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Caregiver relationship:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList  Width="98%" CssClass="form-control" runat="server" ID="ddlcaregiverrelationship"></asp:DropDownList>
             </div> 
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required"><asp:Label ID="lbladdresschanged" runat="server" Text="Has your address or phone changed:"></asp:Label></label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:RadioButtonList ID="rdoaddresschanged" runat="server" RepeatDirection="Horizontal"
                                                                                        OnClick="rblSelectedValue(this,'hideaddchangeUpdateYN');rblSelectedValue(this,'divUpdated_phone')">
                                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                                    </asp:RadioButtonList>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Child Primary Caregiver:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" ID="txtPrimaryCareGiver" Width="98%" CssClass="form-control"></asp:TextBox>
             </div> 
 </div>
 <div class="row">
 <div id="hideaddchangeUpdateYN" style="display: none;">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Address change update:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
           <asp:TextBox runat="server" ID="txtAddresschange" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
             </div>
              <div id="divUpdated_phone" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Updated phone No:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" ID="txtUpdated_phone" Width="98%" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Disclosure status:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList runat="server" ID="ddlDisclosureStatus" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div id="divReasonNotDisclosed" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify reason not disclosed:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" ID="txtReasonNotDisclosed" Width="98%" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
             <div id="divOtherDisclosureReason" style="display: none;">
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify other disclosure status:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" ID="txtOtherDisclosureReason" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Father alive?:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="rdofatheraliveyes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(0,'divDateOfDeathFather')"
                                                                    type="radio" name="fatheralive" runat="server" />
                                                                &nbsp; &nbsp;
                                                                <label>
                                                                    Yes</label>
                                                                <input id="rdofatheraliveno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(1,'divDateOfDeathFather');"
                                                                    type="radio" name="fatheralive" runat="server" />
                                                                &nbsp; &nbsp;
                                                                <label>
                                                                    No</label>
             </div>
             <div id="divDateOfDeathFather" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date of father's death:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtDateOfDeathFather" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div> 
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Mother alive?:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <input id="rdomotheraliveyes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(0,'divDateOfDeathMother');"
                                                                    type="radio" name="motheraliveyes" runat="server" />
                                                                &nbsp; &nbsp;
                                                                <label>
                                                                    Yes</label>
                                                                <input id="rdomotheraliveno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(1,'divDateOfDeathMother');"
                                                                    type="radio" name="motheraliveyes" runat="server" />
                                                                &nbsp; &nbsp;
                                                                <label>
                                                                    No</label>
             </div>
             <div id="divDateOfDeathMother" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date of mother's death:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtDateOfDeathMother" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div> 
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Schooling status:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlSchoolingStatus" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div id="divHighestLevelAttained" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Highest level attained:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlHighestLevelAttained" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Is client a member of a support group?:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="rdoHIVSupportGroup" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">HIV support group membership:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:TextBox runat="server" ID="txtHIVSupportGroupMembership" Width="98%" CssClass="form-control"></asp:TextBox>
             </div> 
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Health education given?:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:RadioButtonList ID="rdoHealthEducation" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
             </div> 
 </div>
                                    
                                </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlVitalSigns" runat="server">
          <h5 class="forms" align="left">
          <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/arrow-up.gif" runat="server" />
            <asp:Label ID="lblVitalSigns" runat="server" Text="Vital Signs"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
          <asp:Panel ID="PnlVitalSignsDetails" runat="server" style="overflow:hidden;">
          <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <UcVitalSign:Uc1 ID="idVitalSign" runat="server" />
             </div>
             
 </div>
 </asp:Panel>
                          
                            <act:CollapsiblePanelExtender ID="CPEHIVCare" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlHivCareDetail" CollapseControlID="pnlHIVCare"
                                ExpandControlID="pnlHIVCare" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="imgHIVCare" Enabled="True">
                            </act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPEVitalSign" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlVitalSignsDetails"
                                CollapseControlID="PnlVitalSigns" ExpandControlID="PnlVitalSigns" CollapsedImage="~/images/arrow-up.gif"
                                Collapsed="True" ImageControlID="imgVitalSigns" Enabled="True">
                            </act:CollapsiblePanelExtender>
                            <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureTriage" runat="server" />
             </div>
            
 </div>
                            <div class="row" align="center" id="tblSaveButton">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <asp:Button ID="btnTriagesave" runat="server" OnClick="btnTriagesave_Click" Text="Save" CssClass="btn btn-primary"
                                Height="30px" Width="9%" style="text-align:left;"  />
 <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right:2%; vertical-align: sub;
                                color: #fff;">
                            </label>
<asp:Button ID="btncloseTriage" Text="Close" runat="server" OnClick="btncloseTriage_Click" CssClass="btn btn-primary"
                                Height="30px" Width="9%" style="text-align:left;"  />
<label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right:2%; vertical-align: sub;
                                color: #fff;">
                            </label>
<asp:Button ID="btnTriagePrint" runat="server" OnClientClick="WindowPrint()" Text="Print" CssClass="btn btn-primary"
                                Height="30px" Width="9%" style="text-align:left;" />
                                <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff;">
                            </label>
             </div>
            
 </div>
 <div class="row" align="center" id="tblDeleteButton" style="display: none" >
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" CssClass="btn btn-primary"
                                Height="30px" Width="9%" style="text-align:left;" />
 <label class="glyphicon glyphicon-trash" style="margin-left: -3%; margin-right:2%; vertical-align: sub;
                                color: #fff;">
                            </label>
 </div>
 </div>
                            
                        </ContentTemplate>
                    </act:TabPanel>
                    <act:TabPanel ID="TabPanel1" runat="server" HeaderText="Clinical History">
                        <ContentTemplate>
                        <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlMedicalHistory" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="ImgMedicalHistory" ImageUrl="~/images/arrow-up.gif" runat="server" />
         Medical History (Disease, Diagnosis and Treatment)
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="PnlMedicalHistorydetails" runat="server" style="overflow:hidden;">
        <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Medical history:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="rdoMedicalHistory" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                            </asp:RadioButtonList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Chronic condition:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
            <div id="div2" class="customdivbordermultiselect" style="width: 100%" runat="server">
                                                                <asp:CheckBoxList ID="cblChronicCondition" Width="100%" runat="server">
                                                                </asp:CheckBoxList>
                                                            </div>
             </div> 
 </div>
 <div class="row" id="divOtherChronicCondition" style="display: none;">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             
             </div>
            
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Specify other chronic condition:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtOtherChronicCondition" Columns="2" Rows="3" TextMode="MultiLine" Width="98%" CssClass="form-control" style="resize:none;" runat="server"></asp:TextBox>
             </div> 
 </div>

 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify medical history:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtOtherMedicalHistorySpecify" Columns="20" Rows="3" TextMode="MultiLine" runat="server" Width="98%" CssClass="form-control" style="resize:none;"></asp:TextBox>
             </div>
             
 </div>

                                </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlPresentingComplaint" runat="server">
          <h5 class="forms" align="left">
        <asp:ImageButton ID="ImgPresentingComplaint" ImageUrl="~/images/arrow-up.gif" runat="server" />
          <asp:Label ID="lblPresComp" runat="server" Text="Presenting Complaints"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="PnlPresentingComplaintdetails" runat="server" style="overflow:hidden;">
<div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <UcPresComp:Uc6 ID="UCPresComp" runat="server" />
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">If schooling, current school perfomance:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlSchoolPerfomance"  Width="98%" CssClass="form-control"> </asp:DropDownList>
             </div>
           
 </div>
 </asp:Panel>
 <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlImmunisationStatus" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="ImgImmunisationStatus" ImageUrl="~/images/arrow-up.gif" runat="server" />
          Immunisation Status
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="PnlImmunisationStatusDetails" runat="server" style="overflow:hidden;">
        <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Immunisation Status:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlImmunisationStatus" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             </div>             
 </div>
 </asp:Panel>

 <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlTBHistory" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="ImgTBHistory" ImageUrl="~/images/arrow-up.gif" runat="server" />
          TB History
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="PnlTBHistoryDetails" runat="server" style="overflow:hidden;">
<div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">TB History:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rdoTBHistory" runat="server" RepeatDirection="Horizontal"
                                                                OnClick="rblSelectedValue(this,'divTBrxCompleteDate');rblSelectedValue(this,'divTBRetreatmentDate')">
                                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                            </asp:RadioButtonList>
             </div>
              <div id="divTBrxCompleteDate" style="display: none;">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Complete TX Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
               <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtTBrxCompleteDate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div> 
             </div>
             <div id="divTBRetreatmentDate" style="display: none;">
              <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Retreatment Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
               <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtTBRetreatmentDate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div> 
             </div>
 </div>

                                    
                                </asp:Panel>
                            <act:CollapsiblePanelExtender ID="CPPnlMedicalHistory" runat="server" SuppressPostBack="true"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlMedicalHistoryDetails"
                                CollapseControlID="PnlMedicalHistory" ExpandControlID="PnlMedicalHistory" CollapsedImage="~/images/arrow-up.gif"
                                Collapsed="true" ImageControlID="ImgMedicalHistory">
                            </act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPPnlPresentingComplaint" runat="server" SuppressPostBack="true"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlPresentingComplaintdetails"
                                CollapseControlID="PnlPresentingComplaint" ExpandControlID="PnlPresentingComplaint"
                                CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="ImgPresentingComplaint">
                            </act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPPnlImmunisationStatus" runat="server" SuppressPostBack="true"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlImmunisationStatusDetails"
                                CollapseControlID="PnlImmunisationStatus" ExpandControlID="PnlImmunisationStatus"
                                CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="ImgImmunisationStatus">
                            </act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPPnlTBHistory" runat="server" SuppressPostBack="true"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlTBHistoryDetails" CollapseControlID="PnlTBHistory"
                                ExpandControlID="PnlTBHistory" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                                ImageControlID="ImgTBHistory">
                            </act:CollapsiblePanelExtender>
                            <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureClinical" runat="server" />
             </div>
           
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="btnClinicalHistorySave" runat="server" OnClick="btnClinicalHistorySave_Click" Text="Save" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;"/>
  <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right:2%; vertical-align: sub;
                                color: #fff;">
                            </label>                                               
<asp:Button ID="btncloseCHistory" Text="Close" runat="server" OnClick="btncloseCHistory_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;"/>
 <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right:2%; vertical-align: sub;
                                color: #fff;">
                            </label>
<asp:Button ID="btnClinicalHistoryPrint" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
  <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff;">
                            </label>
             </div>
           
 </div>
                        </ContentTemplate>
                    </act:TabPanel>
                    <act:TabPanel ID="TabPanel2" runat="server" HeaderText="TB Screening">
                        <ContentTemplate>
                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <UcTBScreen:Uc7 ID="UcTBScreen" runat="server" />
             </div>
            
 </div>

                            
                        </ContentTemplate>
                    </act:TabPanel>
                    <act:TabPanel ID="TabPanel3" runat="server" HeaderText="Examination">
                        <ContentTemplate>
                        <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlCurrentLongTermMedications" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="ImgCurrentLongTermMedications" ImageUrl="~/images/arrow-up.gif" runat="server" />
          <asp:Label ID="lblheadcurrentlongterm" runat="server" Text="Current Long Term Medications"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="PnlCurrentLongTermMedicationsDetails" runat="server" style="overflow:hidden;">
        <div class="row">
 <div class="col-md-4">
 <%--left--%>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label required"><asp:Label ID="lblchlongtermmed" runat="server" Text="Long term medications:"></asp:Label></label>
             </div>           
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<div class="customdivbordermultiselect">
                                                                <asp:CheckBoxList ID="chkLongTermMedication" RepeatLayout="Flow" Width="190px" runat="server">
                                                                </asp:CheckBoxList>
                                                            </div>
             </div>           
 </div>
 </div>
 <div class="col-md-8">
 <%--right--%>
 <div class="row" style="display: none;" id="divLongTermMedication">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other current long term medications:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txOtherLongTermMedications" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
             
              
 </div>

 </div>
 </div>
 <div class="row" style="display: none;">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Long term medications:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="rblbtnLongTermMedicationsyes" onmouseup="up(this);" onfocus="up(this);"
                                                                onclick="down(this);" type="radio" name="rblbtnLongTermMedicationsyes" runat="server" />
                                                            <label>
                                                                Yes</label>
                                                            <input id="rblbtnLongTermMedicationsno" onmouseup="up(this);" onfocus="up(this);"
                                                                onclick="down(this);" type="radio" name="rblbtnLongTermMedicationsyes" runat="server" />
                                                            <label>
                                                                No</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other current long term medications:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <div class="data-control"></div>
             </div> 
 </div>
 <div class="row" style="display: none;">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Antifungals:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtAntifungalsDate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                       onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Anticonvulsants:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtAntihypertensivesDate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                        onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')" 
                                                        onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
 </div>
 <div class="row" style="display: none;">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Sulfa TMP:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
               <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtSulfaTMPDate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;"
                                                        onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                                onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other long term medications:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtOtherCurrentLongTermMedications" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                       onblur="DateFormat(this,this.value,event,false,'3')"
                                                                onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
 </div>
 </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlMedicalConditions" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="ImgMedicalConditions" ImageUrl="~/images/arrow-up.gif" runat="server" />
         <asp:Label ID="lblMedicalConditions" runat="server" Text="Medical Conditions"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="PnlMedicalConditionsDetails" runat="server" style="overflow:hidden">
<div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Milestone appropriate for age?:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
           <input id="rdoMilestoneAppropriateYes" onmouseup="up(this);" onfocus="up(this);"
                                                                    onclick="down(this);rblSelectedValue1(0,'divResonMilestoneInappropriate')" type="radio"
                                                                    name="MilestoneAppropriate" runat="server" />
                                                                <label>
                                                                    Yes</label>
                                                                <input id="rdoMilestoneAppropriateNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(1,'divResonMilestoneInappropriate');"
                                                                    type="radio" name="MilestoneAppropriate" runat="server" />
                                                                <label>
                                                                    No</label>
             </div>
              <div id="divResonMilestoneInappropriate" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">If No specify why inappropriate:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtResonMilestoneInappropriate" Columns="20" Rows="2" Width="98%" CssClass="form-control" TextMode="MultiLine" runat="server" style="resize:none;"></asp:TextBox>
             </div> 
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <UcPhysExam:Uc4 ID="UcPhysExam" runat="server" />
</div>
 </div>
 </asp:Panel>
 <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlWHOStaging" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="ImgWHOStaging" ImageUrl="~/images/arrow-up.gif" runat="server" />
          <asp:Label ID="lblheadWHOStage" runat="server" Text="WHO Stage"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="PnlWHOStagingDetails" runat="server" style="overflow:hidden;">
<div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<UcWHOStaging:Uc3 ID="UCWHO" runat="server" />
             </div>
             
 </div>

                                    
                                </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlDiagnosis" runat="server">
          <h5 class="forms" align="left">
          <asp:ImageButton ID="ImgDiagnosis" ImageUrl="~/images/arrow-up.gif" runat="server" />
          Diagnosis
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="PnlDiagnosisDetails" runat="server" style="overflow:hidden;">
<div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Impression:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlHAARTImpression" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div id="divSpecifyHAART" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap text-right">
             <label class="control-label">Specify HAART patient impression:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlHAARTexperienced" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
             <div id="divSpecifyotherimpression" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Specify other impression:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtOtherHAARTImpression" Columns="20" Rows="6" Width="98%" CssClass="form-control" style="resize:none;" TextMode="MultiLine" runat="server"></asp:TextBox>
             </div>
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Have you reviewed previous results:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="rdoReviewedPreviousResults" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                            </asp:RadioButtonList>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Additional information:</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtResultsReviewComments" Columns="20" Rows="6" Width="98%" CssClass="form-control" TextMode="MultiLine" runat="server" style="resize:none;"></asp:TextBox>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-4">
 <%--left--%>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Diagnosis:</label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<div id="divcblDiagnosis2" enableviewstate="true" class="customdivbordermultiselect"
                                                                            runat="server">
                                                                            <asp:CheckBoxList ID="cblDiagnosis2" Width="100%" RepeatLayout="Flow" runat="server">
                                                                            </asp:CheckBoxList>
                                                                        </div>
             </div>
 </div>
 </div>
 <div class="col-md-8">
 <%--right--%>
 <div class="row" id="divNonHIVRelatedOI" style="display: none;">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify Non HIV related OI:</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" MaxLength="50" ID="txtNonHIVRelatedOI" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
            
 </div>
 <div class="row" id="divSpecifyHIVrelatedOI" style="display: none;">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify HIV related OI:</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
<asp:TextBox runat="server" MaxLength="50" ID="txtHIVRelatedOI" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
            
 </div>
 
 </div>
 </div>

 
 </asp:Panel>
                         
                            <act:CollapsiblePanelExtender ID="CPPnlCurrentLongTermMedications" runat="server"
                                SuppressPostBack="true" ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlCurrentLongTermMedicationsDetails"
                                CollapseControlID="PnlCurrentLongTermMedications" ExpandControlID="PnlCurrentLongTermMedications"
                                CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="ImgCurrentLongTermMedications">
                            </act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPPnlMedicalConditions" runat="server" SuppressPostBack="true"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlMedicalConditionsDetails"
                                CollapseControlID="PnlMedicalConditions" ExpandControlID="PnlMedicalConditions"
                                CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="ImgMedicalConditions">
                            </act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPPnlWHOStaging" runat="server" SuppressPostBack="true"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlWHOStagingDetails"
                                CollapseControlID="PnlWHOStaging" ExpandControlID="PnlWHOStaging" CollapsedImage="~/images/arrow-up.gif"
                                Collapsed="true" ImageControlID="ImgWHOStaging">
                            </act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPPnlDiagnosis" runat="server" SuppressPostBack="true"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlDiagnosisDetails" CollapseControlID="PnlDiagnosis"
                                ExpandControlID="PnlDiagnosis" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                                ImageControlID="ImgDiagnosis">
                            </act:CollapsiblePanelExtender>
                            <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
   <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureExam" runat="server" />
             </div>
           
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="btnExaminationSave" runat="server" Text="Save" OnClick="btnExaminationSave_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
 <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right:2%; vertical-align: sub;
                                color: #fff;">
                            </label>
<asp:Button ID="btncloseExam" Text="Close" runat="server" OnClick="btncloseExam_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;"/>
  <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right:2%; vertical-align: sub;
                                color: #fff;">
                            </label>
<asp:Button ID="btnExaminationPrint" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;"/>
<label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff;">
                            </label>
             </div>
           
 </div>
                          
                        </ContentTemplate>
                    </act:TabPanel>
                    <act:TabPanel ID="TabPanel4" runat="server" HeaderText="Management">
                        <ContentTemplate>
                        <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlAdherenceAssessment" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="ImgAdherenceAssessment" ImageUrl="~/images/arrow-up.gif" runat="server" />
          Adherence Assessment
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="PnlAdherenceAssessmentDetails" runat="server" style="overflow:hidden;">
        <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Have you missed any doses?:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rdoHaveyoumissedanydoses" runat="server" RepeatDirection="Horizontal"
                                                                        OnClick="rblSelectedValue(this,'divSpecifywhydosesmissed')">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Have you delayed in taking medication?:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
              <asp:RadioButtonList ID="rdohavedelayed" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
             </div> 
 </div>
 <div class="row" id="divSpecifywhydosesmissed" style="display: none;">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify why doses missed?:</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtSpecifywhydosesmissed" Columns="20" Rows="3" TextMode="MultiLine" runat="server" Width="98%" CssClass="form-control" style="resize:none;"></asp:TextBox>
             </div>
 </div>
                                   
                                </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlDrugAllergiesToxicities" runat="server">
          <h5 class="forms" align="left">
       <asp:ImageButton ID="ImgDrugAllergiesToxicities" ImageUrl="~/images/arrow-up.gif" runat="server" />
         Drug Allergies Toxicities
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="PnlDrugAllergiesToxicitiesDetails" runat="server" style="overflow:hidden;">
<div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<UcAllergies:Uc5 ID="UCAllergies" runat="server" />
             </div>
 </div>
                                    
                                </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlARVsideeffects" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="ImgARVsideeffects" ImageUrl="~/images/arrow-up.gif" runat="server" />
          ARV Side Effects
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="PnlARVsideeffectsDetails" runat="server" style="overflow:hidden;">
<div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">ARV side effects:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:RadioButtonList ID="rdoARVsideeffects" runat="server" RepeatDirection="Horizontal"
                                                                        OnClick="rblSelectedValue(this,'divshorttermeffects');rblSelectedValue(this,'divLongtermeffects')">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             
             </div>
           
 </div>
<div class="row">
 <div class="col-md-6">
 <%--left--%>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Short term effects:</label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<div id="divcblShorttermeffects" enableviewstate="true" class="customdivbordermultiselect"
                                                                        runat="server">
                                                                        <asp:CheckBoxList ID="cblShorttermeffects" RepeatLayout="Flow" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
             </div>
 </div>
 <div class="row" id="divSpecifyothershorttermeffects" style="display: none;">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify other short term effects:</label>
             </div>
 <div class="col-md-8 col-sm-12 col-xs-12 form-group">
<asp:TextBox ID="txtSpecifyothershorttermeffects" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
 </div>
 </div>
 <div class="col-md-6">
 <%--right--%>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Long term effects:</label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div id="divchklistlongtermeffect" enableviewstate="true" class="customdivbordermultiselect"
                                                                        runat="server">
                                                                        <asp:CheckBoxList ID="chklistlongtermeffect" RepeatLayout="Flow" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
             </div>
 </div>
 <div class="row" id="divSpecifyOtherlongtermeffects" style="display: none;">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify Other long term effects:</label>
             </div>
 <div class="col-md-8 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtlistlongtermeffect" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
 </div>
 </div>
 </div>
 </asp:Panel>
 <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlPlan" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="ImgPlan" ImageUrl="~/images/arrow-up.gif" runat="server" />
        Lab Evaluation
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="PnlPlanDetails" runat="server" style="overflow:hidden;">
<div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <uc11:Uclabeval ID="UcLabEval" runat="server" />
             </div>
 </div>
 </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlCurrentARTRegimen" runat="server">
          <h5 class="forms" align="left">
          <asp:ImageButton ID="ImgCurrentARTRegimen" ImageUrl="~/images/arrow-up.gif" runat="server" />
         <asp:Label ID="lblheadregimenpresc" runat="server" Text="Regimen Prescribed"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="PnlCurrentARTRegimenDetails" runat="server" style="overflow:hidden;">
<div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <UcPharmacy:Uc8 ID="UserControlKNH_Pharmacy1" runat="server" />
             </div>
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">OI Prophylaxis:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlOIProphylaxis" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div id="divCotrimoxazoleprescribedfor" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Cotrimoxazole prescribed for:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList  Width="98%" CssClass="form-control" runat="server" ID="ddlCotrimoxazoleprescribed"></asp:DropDownList>
             </div> 
             </div>
             <div id="divFluconazoleshowhide" style="display: none;">
              <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Fluconazole prescribed for:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlfluconazole" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
              <div id="divOtherOIPropholyxis" style="display: none;">
              <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Other (Specify):</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtOtherOIPropholyxis" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
              </div>
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other treatment:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
<asp:TextBox ID="txtOthertreatment" Columns="20" Rows="6"  Width="98%" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
             </div>
             
 </div>

                                    
                                    
                                </asp:Panel>
                            <act:CollapsiblePanelExtender ID="CPPnlCurrentARTRegimen" runat="server" SuppressPostBack="true"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlCurrentARTRegimenDetails"
                                CollapseControlID="PnlCurrentARTRegimen" ExpandControlID="PnlCurrentARTRegimen"
                                CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="ImgCurrentARTRegimen">
                            </act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPPnlAdherenceAssessment" runat="server" SuppressPostBack="true"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlAdherenceAssessmentDetails"
                                CollapseControlID="PnlAdherenceAssessment" ExpandControlID="PnlAdherenceAssessment"
                                CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="ImgAdherenceAssessment">
                            </act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPPnlDrugAllergiesToxicities" runat="server" SuppressPostBack="true"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlDrugAllergiesToxicitiesDetails"
                                CollapseControlID="PnlDrugAllergiesToxicities" ExpandControlID="PnlDrugAllergiesToxicities"
                                CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="ImgDrugAllergiesToxicities">
                            </act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPPnlARVsideeffects" runat="server" SuppressPostBack="true"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlARVsideeffectsDetails"
                                CollapseControlID="PnlARVsideeffects" ExpandControlID="PnlARVsideeffects" CollapsedImage="~/images/arrow-up.gif"
                                Collapsed="true" ImageControlID="ImgARVsideeffects">
                            </act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPPnlPlan" runat="server" SuppressPostBack="true"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlPlanDetails" CollapseControlID="PnlPlan"
                                ExpandControlID="PnlPlan" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                                ImageControlID="ImgPlan">
                            </act:CollapsiblePanelExtender>
                            <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureMgt" runat="server" />
             </div>
            
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnManagementSave" runat="server" Text="Save" OnClick="btnManagementSave_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
   <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
<asp:Button ID="btncloseMgt" Text="Close" runat="server" OnClick="btncloseMgt_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;"/>
 <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
<asp:Button ID="btnManagementPrint" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;"/>
 <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff;"></label>
             </div>
            
 </div>
                           
                        </ContentTemplate>
                    </act:TabPanel>
                    <act:TabPanel ID="TabPanel5" runat="server" HeaderText="Prevention with Positives">
                        <ContentTemplate>
                            <div class="border center formbg">
                                <table class="border center formbg" width="100%">
                                    <tr>
                                        <td>
                                            <UcPWP:Uc9 ID="UcPwp" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </act:TabPanel>
                </act:TabContainer>
        </div>
        </div>
        <div class="row" style="display: none;">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label required">Signature:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddSignature" Width="98%" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hidupdate" runat="server" />
                                        <asp:HiddenField ID="hidregimen" runat="server" />
             </div>
        </div>
        </div>
         <div class="row" align="center">       
        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
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
        </div>
        </div>
         <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
        <uc13:UserControlKNH_BackToTop ID="UserControlKNH_BackToTop1" runat="server" />
        </div>
        </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>

</asp:Content>
