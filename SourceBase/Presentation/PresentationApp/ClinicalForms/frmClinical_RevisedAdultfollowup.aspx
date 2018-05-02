<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmClinical_RevisedAdultfollowup.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_RevisedAdultfollowup" %>

<%@ Register TagPrefix="UcVitalSign" TagName="Uc1" Src="~/ClinicalForms/UserControl/UserControlKNH_VitalSigns.ascx" %>
<%@ Register TagPrefix="UcPastMedicalHistory" TagName="Uc2" Src="~/ClinicalForms/UserControl/UserControlKNH_PastMedicalHistory.ascx" %>
<%@ Register TagPrefix="UcWHOStaging" TagName="Uc3" Src="~/ClinicalForms/UserControl/UserControlKNH_WHOStaging.ascx" %>
<%@ Register TagPrefix="UcPhysExam" TagName="Uc4" Src="~/ClinicalForms/UserControl/UserControlKNH_PhysicalExamination.ascx" %>
<%@ Register TagPrefix="UcAllergies" TagName="Uc5" Src="~/ClinicalForms/UserControl/UserControlKNH_DrugAllergies.ascx" %>
<%@ Register TagPrefix="UcPresComp" TagName="Uc6" Src="~/ClinicalForms/UserControl/UserControlKNHPresentingComplaints.ascx" %>
<%@ Register TagPrefix="UcTBScreen" TagName="Uc7" Src="~/ClinicalForms/UserControl/UserControlKNH_TBScreening.ascx" %>
<%@ Register TagPrefix="UcPharmacy" TagName="Uc8" Src="~/ClinicalForms/UserControl/UserControlKNH_Pharmacy.ascx" %>
<%@ Register TagPrefix="UcPWP" TagName="Uc9" Src="~/ClinicalForms/UserControl/UserControlKNH_PwP.ascx" %>

<%@ Register Src="~/ClinicalForms/UserControl/UserControlKNH_LabEvaluation.ascx"
    TagName="Uclabeval" TagPrefix="uc11" %>
<%@ Register Src="~/ClinicalForms/UserControl/UserControlKNH_Signature.ascx" TagName="UserControlKNH_Signature"
    TagPrefix="uc12" %>
<%@ Register src="UserControl/UserControlKNH_BackToTop.ascx" tagname="UserControlKNH_BackToTop" tagprefix="uc14" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    
    <script language="javascript" type="text/javascript">        buildWeeklyCalendar(0);</script>
    <script type="text/javascript" language="javascript">

        //    
        //        $(function () {

        //            $("#tabs").tabs();

        //        });
        function ShowHide(theDiv, YN) {

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
        function CheckBoxHideUnhide(val, divID, txt) {

            var checkList = document.getElementById(val);
            var checkBoxList = checkList.getElementsByTagName("input");
            var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
            var checkBoxSelectedItems = new Array();

            for (var i = 0; i < checkBoxList.length; i++) {

                if (checkBoxList[i].checked) {
                    if (arrayOfCheckBoxLabels[i].innerHTML == txt) {
                        ShowHide(divID, "show");
                    }

                }
                else {

                    if (arrayOfCheckBoxLabels[i].innerHTML == txt) {
                        ShowHide(divID, "hide");
                    }
                }
            }



        }

        //        function CheckBoxHideUnhide(strcblcontrolId, strdivId, strfieldname) {
        //            //alert(strcblcontrolId);
        //            var checkList = document.getElementById(strcblcontrolId);
        //            var checkBoxList = checkList.getElementsByTagName("input");
        //            var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        //            var checkBoxSelectedItems = new Array();

        //            for (var i = 0; i < checkBoxList.length; i++) {

        //                if (checkBoxList[i].checked) {
        //                    if (arrayOfCheckBoxLabels[i].innerHTML == strfieldname) {
        //                        //alert(strfieldname);
        //                        ShowHide(strdivId, "show");
        //                    }
        //                    else {
        //                        ShowHide(strdivId, "hide");
        //                    }
        //                }
        //                else {
        //                    ShowHide(strdivId, "hide");
        //                }
        //            }



        //        }

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
        function getMultiDivSelectedValue(DivId, DivId2, DDText, str) {
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
            ShowHide(DivId2, YN);
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

        function CheckBoxToggleShowHide(val, divID, txt) {

            var checkList = document.getElementById(val);
            var checkBoxList = checkList.getElementsByTagName("input");
            var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
            var checkBoxSelectedItems = new Array();

            for (var i = 0; i < checkBoxList.length; i++) {

                if (checkBoxList[i].checked) {
                    if (arrayOfCheckBoxLabels[i].innerHTML == txt) {
                        ShowHide(divID, "show");
                    }

                }
                else {

                    if (arrayOfCheckBoxLabels[i].innerHTML == txt) {
                        ShowHide(divID, "hide");
                    }
                }
            }
        }


        function ToggleSurgicalItems(checkBoxList, Value) {
            var chkBoxList = document.getElementById(checkBoxList);
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            var arrayOfCheckBoxLabels = chkBoxList.getElementsByTagName("label");
            if (Value == 'None') {
                for (var i = 0; i < chkBoxCount.length; i++) {
                    if (arrayOfCheckBoxLabels[i].innerText != 'None') {
                        chkBoxCount[i].checked = false;
                    }
                }
            }
            if (Value != 'None') {
                for (var i = 0; i < chkBoxCount.length; i++) {
                    if (arrayOfCheckBoxLabels[i].innerText == 'None') {
                        chkBoxCount[i].checked = false;
                    }
                }
            }
        }
    </script>
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtvisitDate').datepicker({ autoclose: true });
            $('#txtPreviousAdmissionStart').datepicker({ autoclose: true });
            $('#txtPreviousAdmissionEnd').datepicker({ autoclose: true });
        }
        //Calling RegisterJQuery when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);
        //Calling RegisterJQuery when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>

    
    <%--<asp:ScriptManager ID="mst" runat="server">
    </asp:ScriptManager>--%>
    <%--<asp:UpdatePanel ID="UpdateMasterLink" runat="server">
        <ContentTemplate>--%>
        <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title" id="Header"><asp:Label ID="lblHeading" runat="server" Text="Adult Follow Up"></asp:Label> </h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <label id="lblvdate" class="control-label required">*Visit Date:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtvisitDate" clientidmode="Static"
                                                       maxlength="11" size="8" name="visitDate" runat="server" onkeyup="DateFormat(this,this.value,event,false,'3');" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>             
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <act:TabContainer ID="tabControl" runat="server" CssClass="ajax__myTab" ActiveTabIndex="0" OnActiveTabChanged="tabControl_ActiveTabChanged"
                AutoPostBack="False" Width="100%">
                <act:TabPanel ID="tbpnlgeneral" runat="server" HeaderText="Triage">
                    <HeaderTemplate>
                        Triage
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="border center formbg">
                            <br />
                            <div class="center formbg pad5">
                                <table class="border leftallign formbg" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlHIVCare" runat="server">
                                                <table>
                                                    <tr align="left">
                                                        <td>
                                                            <asp:ImageButton ID="imgHIVCare" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                        </td>
                                                        <td>
                                                            <h5 class="forms" align="left">
                                                                <asp:Label ID="lblClientInfo" runat="server" Text="Patient Information"></asp:Label></h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="pnlHivCareDetail" runat="server" Style="overflow:hidden;">
                            <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblPtnAccByCareGiver" runat="server" CssClass="required" Text="*Patient accompanied by caregiver:"></asp:Label></label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rdopatientcaregiver" runat="server" RepeatDirection="Horizontal"
                                                                    OnClick="rblSelectedValue(this,'divcarrelationYN')" CssClass="checkbox-btn">
                                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
             </div>
              <div id="divcarrelationYN" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="lblCaregiver relationship-8888358">Caregiver relationship:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList Width="70%" runat="server" ID="ddlcaregiverrelationship" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <label class="control-label" id='lblHealth education given?-88881455'>Health education given?:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="rdoHealthEducation" runat="server" RepeatDirection="Horizontal" CssClass="checkbox-btn">
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
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id='lblDisclosure status-8888813'>If adolescent, disclosure status:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList runat="server" ID="ddlDisclosureStatus" CssClass="form-control" 
                     Width="300px"></asp:DropDownList>
             </div>
              <div id="divReasonNotDisclosed" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id='lblSpecify reason not disclosed-88881312'>Specify reason not disclosed:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" ID="txtReasonNotDisclosed" Width="250px" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
             <div id="divOtherDisclosureReason" style="display: none;">
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <label class="control-label" id='lblSpecify other disclosure status-88881313'>Specify other disclosure status:</label></div>
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <asp:TextBox runat="server" ID="txtOtherDisclosureReason" Width="250px" CssClass="form-control"></asp:TextBox>
              </div>
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id='lblIf adolescent, schooling status-8888805'>If adolescent, schooling status:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList runat="server" ID="ddlSchoolingStatus" Width="250px" 
                     CssClass="form-control"></asp:DropDownList>
             </div>
             <div id="divHighestLevelAttained" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id='lblHighest level attained-88881456'>Highest level attained:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlHighestLevelAttained" Width="250px" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <label class="control-label" id='lblIs client a member of a support group?-8888504'>Is client a member of a support group?:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="rdoHIVSupportGroup" runat="server" RepeatDirection="Horizontal" OnClick="rblSelectedValue(this,'divHIVmembership');" CssClass="checkbox-btn">
                                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                            </asp:RadioButtonList>
             </div>
              <div id="divHIVmembership" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id='lblHIV support group membership-8888505'>HIV support group membership:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" MaxLength="50" ID="txtHIVSupportGroupMembership" Width="250px" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
             </div>
             <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
 <label class="control-label">
             <asp:Label ID="lbladdresschanged" runat="server" CssClass="required" Text="*Has your address or phone changed:"></asp:Label></label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rdoaddresschanged" runat="server" RepeatDirection="Horizontal"
                                                                    OnClick="rblSelectedValue(this,'hideaddchangeUpdateYN');rblSelectedValue(this,'divUpdatePhone')" CssClass="checkbox-btn">
                                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
             </div>
             <div id="hideaddchangeUpdateYN" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblAddress change update-8888519">Address change update:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" ID="txtAddress_change" Width="250px" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
             
 </div>

 
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            
             </div>
             <div id="divUpdatePhone" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblUpdated phone No-8888593">Updated phone No:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <asp:TextBox runat="server" ID="txtUpdated_phone" Width="250px" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
 </div>
                                
                            </asp:Panel>
                            <div class="center formbg pad5">
                                <table class="border leftallign formbg" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlVitalSigns" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                        </td>
                                                        <td>
                                                            <h5 align="left" class="forms">
                                                                <asp:Label ID="lblVitalSigns" runat="server" Text="Vital Signs"></asp:Label></h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="PnlVitalSignsDetails" runat="server" Style="overflow:hidden;">
                            <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <UcVitalSign:Uc1 ID="idVitalSign" runat="server" />
             </div>           
 </div>
                               
                            </asp:Panel>
                        </div>
                        <act:CollapsiblePanelExtender ID="CPEHIVCare" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlHivCareDetail" CollapseControlID="pnlHIVCare"
                            ExpandControlID="pnlHIVCare" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgHIVCare" BehaviorID="_content_CPEHIVCare">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEVitalSign" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlVitalSignsDetails"
                            CollapseControlID="PnlVitalSigns" ExpandControlID="PnlVitalSigns" CollapsedImage="~/images/arrow-up.gif"
                            Collapsed="True" ImageControlID="imgVitalSigns" 
                            BehaviorID="_content_CPEVitalSign">
                        </act:CollapsiblePanelExtender>
                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureTriage" runat="server" />
             </div>           
 </div>
 <div class="row" align="center" id="tblSaveButton" >
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="btnTriagesave" runat="server" OnClick="btnTriagesave_Click" Text="Save" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
<asp:Button ID="btncloseTriage" Text="Close" runat="server" OnClick="btncloseTriage_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
<asp:Button ID="btnTriagePrint" runat="server" OnClientClick="WindowPrint()" Text="Print" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
             </div>           
 </div>
 <div class="row" align="center" id="tblDeleteButton" style="display: none">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
 </div>
 </div>
                       
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabPanel1" runat="server" HeaderText="Clinical History">
                    <HeaderTemplate>
                        Clinical History
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="border center formbg">
                            <br />
                            <div class="center formbg pad5">
                                <table class="border leftallign formbg" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlPresentingComplaint" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgPresentingComplaint" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                        </td>
                                                        <td>
                                                            <h5 align="left" class="forms">
                                                                <asp:Label ID="lblPresComp" runat="server" Text="Presenting Complaints"></asp:Label></h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="PnlPresentingComplaintdetails" runat="server" Style="overflow:hidden;">
                            <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           <UcPresComp:Uc6 ID="UCPresComp" runat="server" />
             </div>
 </div>
                                <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <label class="control-label" id='lblIf adolescent and schooling, current school perfomance-88881464'>If adolescent and schooling, current school perfomance:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList runat="server" ID="ddlSchoolPerfomance" Width="250px" CssClass="form-control"></asp:DropDownList>
             </div>
            
 </div>
                                
                            </asp:Panel>
                            <div class="center formbg pad5">
                                <table class="border leftallign formbg" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlPastmedicalHistory" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgPastmedicalHistory" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                        </td>
                                                        <td>
                                                            <h5 align="left" class="forms">
                                                                <asp:Label ID="lblheadPastmedicalHistory" runat="server" Text="Past medical History"></asp:Label></h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="PnlPastmedicalHistorydetails" runat="server" Style="overflow:hidden;">
                            <div id="divcblSpecificMedicalCondition">
                            <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblPreExistingMedicalCondition" runat="server" Text="*Pre existing medical condition :" CssClass="required"></asp:Label></label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div id="divSpecificMedicalCondition" enableviewstate="true" class="customdivbordermultiselect" runat="server">
                                                                            <asp:CheckBoxList ID="cblSpecificMedicalCondition" Width="100%" runat="server" CssClass="checkbox-btn">
                                                                            </asp:CheckBoxList>
                                                                        </div>
             </div>
             <div id="divothermedconditon" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <label class="control-label" id='Label3'>Specify other medical condition :</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtothermedconditon" Width="99%" runat="server" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
 </div>
                            </div>
                            <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           <label class="control-label" id='lblSurgical Conditions-8888596'>Surgical Conditions :</label>
             </div>
             </div>
             <div class="row">
             <div class="col-md-4">
             <%--left--%>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <div id="divSurgicalConditions" enableviewstate="true" style="overflow: auto; text-align: left;" runat="server">
                                                            <asp:CheckBoxList ID="cblSurgicalConditions"
                                                                Width="100%" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                                            </asp:CheckBoxList>
                                                        </div>
             </div>
             </div>
             <div class="col-md-8">
             <%--right--%>
              <div id="divPreviousSurgicalCondition" style="display: none;">
               <div class="row">
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
           <label class="control-label" id='lblSpecify previous surgical condition-8888598'>Specify previous surgical condition :</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtPreviousSurgicalCondition" Width="250px" runat="server" CssClass="form-control"></asp:TextBox>
             </div>
             </div>
             </div>
             <div id="divCurrentSurgicalConditionYN" style="display: none;">
             <div class="row">
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <label class="control-label" id='lblSpecify current surgical condition-8888597'>Specify current surgical condition :</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtCurrentSurgicalCondition" Width="250px" runat="server" CssClass="form-control"></asp:TextBox>
             </div>
             </div>
             </div>
             </div>
             </div>


             
 

 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <label class="control-label" id='lblCurrent long term medications-8888900'>Current long term medications :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="rdoPreExistingMedicalConditionsFUP" runat="server" RepeatDirection="Horizontal"
                                                            OnClick="rblSelectedValue(this,'trAntihypertensives');rblSelectedValue(this,'trHypoglycemics');rblSelectedValue(this,'trothers');" CssClass="checkbox-btn">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
           
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            
             </div> 
 </div>
 
 <div class="row" id="trAntihypertensives" style="display: none;">
 <div class="col-md-6">
 <%--left--%>
 <div class="row">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
            <label class="control-label" id='lblAntihypertensives-8888426'>Antihypertensives :</label>
             </div>
             </div>
 <div class="row">
             <div class="col-md-11 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtAntihypertensives" Columns="20" Rows="3" Width="99%" TextMode="MultiLine" runat="server" CssClass="form-control" Style="resize:none"></asp:TextBox>
             </div>
             </div>
 </div>
  
 <div class="col-md-6">
 <%--right--%>
  <div class="row">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
 <label class="control-label" id='lblAnticonvulsants-8888429'>Anticonvulsants :</label>
 </div>
 </div>
 <div class="row">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtAnticonvulsants" Columns="20" Rows="3" Width="99%" TextMode="MultiLine" runat="server" CssClass="form-control" Style="resize:none"></asp:TextBox>
 </div>
 </div>
 </div>
 </div>
 <div class="row" id="trHypoglycemics" style="display: none;">
 <div class="col-md-6">
 <%--left--%>
 <div class="row">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
  <label class="control-label" id='lblHypoglycemics-8888427'>Hypoglycemics :</label>
 </div>
 </div>
  <div class="row">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtHypoglycemics" Columns="20" Rows="3" Width="99%" TextMode="MultiLine" runat="server" CssClass="form-control" Style="resize:none"></asp:TextBox>
 </div>
 </div>

 
 </div>
 <div class="col-md-6">
 <%--right--%>
 <div class="row">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
 <label class="control-label" id='lblRadiotherapy/Chemotherapy -8888901'>Radiotherapy/Chemotherapy :</label>
 </div>
 </div>
 <div class="row">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
  <asp:TextBox ID="txtRadiotherapyChemotherapy" Columns="20" Rows="3" Width="99%" TextMode="MultiLine" runat="server" CssClass="form-control" Style="resize:none"></asp:TextBox>
 </div>
 </div>
 </div>
 </div>
 
 <div class="row" id="trothers" style="display: none;">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <label class="control-label" id='Label4'>Others :</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtothers" Columns="20" Rows="3" Width="100%" TextMode="MultiLine" runat="server" CssClass="form-control" Style="resize:none"></asp:TextBox>
             </div>
             
 </div>
 
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id='lblPreviously admitted since last clinic-8888599'>Previously admitted since last clinic :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="rdoPreviousAdmission" runat="server" RepeatDirection="Horizontal"
                                                            OnClick="rblSelectedValue(this,'divPreviousAdmissionDiagnosis');rblSelectedValue(this,'trPreviousAdmission');" CssClass="checkbox-btn">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
             </div>
             <div id="divPreviousAdmissionDiagnosis" style="display: none;">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <label class="control-label" id='lblDiagnosis-8888600'>Diagnosis :</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtPreviousAdmissionDiagnosis" Width="220px" runat="server" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
 </div>
 <div class="row" id="trPreviousAdmission" style="display: none;">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id='lblAdmission start-8888601'>Admission start :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtPreviousAdmissionStart" clientidmode="Static"
                                                       maxlength="11" size="8" name="visitDate" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
           
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <label class="control-label" id='lblAdmission end-8888602'>Admission end :</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtPreviousAdmissionEnd" clientidmode="Static"
                                                       maxlength="11" size="8" name="visitDate" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>             
             </div> 

 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
            <label class="control-label required"><asp:Label ID="lblHIVAssociatedCond" runat="server" Text="*HIV associated conditions:"></asp:Label></label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlHIVAssociatedConditionsPeads" AutoPostBack="false" Width="120" CssClass="form-control"></asp:DropDownList>
             </div>
             
 </div>

                               
                                
                            </asp:Panel>
                        </div>
                        <act:CollapsiblePanelExtender ID="CPPnlPresentingComplaint" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlPresentingComplaintdetails"
                            CollapseControlID="PnlPresentingComplaint" ExpandControlID="PnlPresentingComplaint"
                            CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="ImgPresentingComplaint">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPPnlPastmedicalHistory" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlPastmedicalHistorydetails"
                            CollapseControlID="PnlPastmedicalHistory" ExpandControlID="PnlPastmedicalHistory"
                            CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="ImgPastmedicalHistory">
                        </act:CollapsiblePanelExtender>
                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureClinical" runat="server" />
             </div>
            
 </div>
    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="btnClinicalHistorySave" runat="server" OnClick="btnClinicalHistorySave_Click" Text="Save" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
<label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
<asp:Button ID="btncloseCHistory" Text="Close" runat="server" OnClick="btncloseCHistory_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
<label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
<asp:Button ID="btnClinicalHistoryPrint" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
<label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
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
                        <div class="border center formbg">
                            <br />
                            <div class="center formbg pad5">
                                <table class="border leftallign formbg" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlPhysicalExam" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgPhysicalExam" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                        </td>
                                                        <td>
                                                            <h5 align="left" class="forms">
                                                                <asp:Label ID="lblheadPhysicalExam" runat="server" Text="Physical Exam"></asp:Label></h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="PnlPhysicalExamdetails" runat="server">
                                <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <UcPhysExam:Uc4 ID="UcPhysicalExam" runat="server" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <div class="center formbg pad5">
                                <table class="border leftallign formbg" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlARVsideeffects" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgARVsideeffects" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                        </td>
                                                        <td>
                                                            <h5 align="left" class="forms">
                                                                ARV side effects</h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="PnlARVsideeffectsdetails" runat="server" Style="overflow:hidden;">
                            <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label required" id="lblARV side effects-8888346" align="center">ARV side effects:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <input id="rdoARVsideeffectsyes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(1,'trshorttermeffects');"
                                                                    type="radio" name="ARVsideeffects" runat="server" class="checkbox-btn" />
                                                                <label>
                                                                    Yes</label>
                                                                <input id="rdoARVsideeffectsno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(0,'trshorttermeffects');"
                                                                    type="radio" name="ARVsideeffects" runat="server"  class="checkbox-btn"/>
                                                                <label>
                                                                    No</label>
             </div>
            
 </div>
 <div class="row" id="trshorttermeffects" style="display: none;">
 <div class="col-md-6">
 <%--left--%>
 <div class="row">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
             <label id="lblShort term effects-8888862" class="control-label">Short term effects:</label>
             </div>             
 </div>
 <div class="row">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
  <div id="divcblShorttermeffects" enableviewstate="true" class="customdivbordermultiselect" runat="server">
  <asp:CheckBoxList ID="cblShorttermeffects" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn"></asp:CheckBoxList>
  </div>
             </div>             
 </div>
 <div class="row" id="divSpecifyothershorttermeffects" style="display: none;">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
 <label id="lblSpecify other short term effects-8888863" class="control-label">Specify other short term effects:</label>
 </div>
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtSpecifyothershorttermeffects" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
 </div>
 </div>
 </div>
 <div class="col-md-6">
 <%--right--%>
 <div class="row">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
             <label id="lblLong term effects-8888864" class="control-label">Long term effects:</label>
             </div>             
 </div>
 <div class="row">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
  <div id="divchklistlongtermeffect" enableviewstate="true" class="customdivbordermultiselect"
                                                                    runat="server" style="width: 100%">
                                                                    <asp:CheckBoxList ID="chklistlongtermeffect" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                                                    </asp:CheckBoxList>
                                                                </div>
             </div>             
 </div>
 <div class="row" id="divSpecifyOtherlongtermeffects" style="display: none;">
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
 <label id="lblSpecify Other long term effects-8888865" class="control-label">Specify Other long term effects:</label>
 </div>
 <div class="col-md-11 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtlistlongtermeffect" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
 </div>

 </div>
 </div>
 </div>
                                
                            </asp:Panel>
                            <div class="center formbg pad5">
                                <table class="border leftallign formbg" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlDiagnosis" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDiagnosis" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                        </td>
                                                        <td>
                                                            <h5 align="left" class="forms">
                                                                Diagnosis</h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="PnlDiagnosisdetails" runat="server" Style="overflow:hidden;">
                            <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
            <label class="control-label" id='lblHave you reviewed previuos results-88881378'>Have you reviewed previous results:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rdoReviewedPreviousResults" runat="server" RepeatDirection="Horizontal" CssClass="checkbox-btn">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id='lblAdditional information-88881379'>Additional information:</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtResultsReviewComments" Columns="20" Rows="3" Width="99%" TextMode="MultiLine" runat="server" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <label  class="control-label" id='lblDiagnosis-88881366'>Diagnosis and current illness at this visit:</label>
             </div>
            
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            
             </div> 
 </div>
 <div class="row">
 <div class="col-md-4">
 <%--left--%>
 <div class="row">
 <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <div id="divcblDiagnosis2" enableviewstate="true" style="overflow: auto; text-align: left;" runat="server">
   <asp:CheckBoxList ID="cblDiagnosis2" Width="100%" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn"></asp:CheckBoxList>
                                                        </div>
             </div>
           
 </div>
 
 </div>
 <div class="col-md-8">
 <%--right--%>
 <div class="row" id="divHIVrelated" style="display: none;">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <label class="control-label" id='lblSpecify HIV related OI-8888641'>Specify HIV related OI:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" MaxLength="50" ID="txtHIVRelatedOI" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
             
 </div>
 <div class="row" id="divNonHIVrelated" style="display: none;">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
           <label class="control-label" id='lblSpecify Non HIV related OI-8888642'>Specify Non HIV related OI:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <asp:TextBox runat="server" MaxLength="50" ID="txtNonHIVRelatedOI" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
 </div>
 </div>
 </div>
                                
                            </asp:Panel>
                            <div class="center formbg pad5">
                                <table class="border leftallign formbg" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlWHOStage" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgWHOStage" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                        </td>
                                                        <td>
                                                            <h5 align="left" class="forms">
                                                                <asp:Label ID="lblheadWHOStage" runat="server" Text="WHO Stage"></asp:Label></h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="PnlWHOStagedetails" runat="server" Style="overflow:hidden;">
                            <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <UcWHOStaging:Uc3 ID="UCWHO" runat="server" />
             </div>
             </div>
                               
                            </asp:Panel>
                        </div>
                        <act:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlPhysicalExamDetails"
                            CollapseControlID="PnlPhysicalExam" ExpandControlID="PnlPhysicalExam" CollapsedImage="~/images/arrow-up.gif"
                            Collapsed="true" ImageControlID="ImgPhysicalExam">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPPnlARVsideeffects" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlARVsideeffectsdetails"
                            CollapseControlID="PnlARVsideeffects" ExpandControlID="PnlARVsideeffects" CollapsedImage="~/images/arrow-up.gif"
                            Collapsed="true" ImageControlID="ImgARVsideeffects">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPPnlDiagnosis" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlDiagnosisdetails" CollapseControlID="PnlDiagnosis"
                            ExpandControlID="PnlDiagnosis" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                            ImageControlID="ImgDiagnosis">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPPnlWHOStage" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlWHOStagedetails" CollapseControlID="PnlWHOStage"
                            ExpandControlID="PnlWHOStage" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                            ImageControlID="ImgWHOStage">
                        </act:CollapsiblePanelExtender>
                   <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureExam" runat="server" />
 </div>
 </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="btnExaminationSave" runat="server" Text="Save" OnClick="btnExaminationSave_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
<asp:Button ID="btncloseExam" Text="Close" runat="server" OnClick="btncloseExam_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
<asp:Button ID="btnExaminationPrint" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
 </div>
       </div>                 
       
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabPanel4" runat="server" HeaderText="Management">
                    <HeaderTemplate>
                        Management
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="border center formbg">
                            <br />
                            <div class="center formbg pad5">
                                <table class="border leftallign formbg" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlAdherence" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgAdherence" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                        </td>
                                                        <td>
                                                            <h5 align="left" class="forms">
                                                                Adherence</h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="PnlAdherencedetails" runat="server" Style="overflow:hidden;">
                            <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="lblHave you missed any doses?-8888904" class="control-label">Have you missed any doses?:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rdoHaveyoumissedanydoses" runat="server" RepeatDirection="Horizontal"
                                                                    OnClick="rblSelectedValue(this,'divSpecifywhydosesmissed')" CssClass="checkbox-btn">
                                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
             </div>
             <div id="divSpecifywhydosesmissed" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <label id="lblSpecify why doses missed?-88881305" class="control-label">Specify why doses missed?:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtSpecifywhydosesmissed" Columns="20" Rows="3" TextMode="MultiLine" runat="server" Width="99%" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label id="lblHave you delayed in taking medication?-8888905" class="control-label">Have you delayed in taking medication?:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rdohavedelayed" runat="server" RepeatDirection="Horizontal" CssClass="checkbox-btn">
                                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <label id="Label2" class="control-label">If missed or delayed dosage refered to counsellor?:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="rdomissdosrefcons" runat="server" RepeatDirection="Horizontal" CssClass="checkbox-btn">
                                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
             </div> 
 </div>
                                
                            </asp:Panel>
                            <div class="center formbg pad5">
                                <table class="border leftallign formbg" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlDrugAllergiesToxicities" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDrugAllergiesToxicities" ImageUrl="~/images/arrow-up.gif"
                                                                runat="server" />
                                                        </td>
                                                        <td>
                                                            <h5 align="left" class="forms">
                                                                Drug Allergies Toxicities</h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="PnlDrugAllergiesToxicitiesdetails" runat="server">
                                <table width="100%" border="0" cellspacing="6" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <UcAllergies:Uc5 ID="UCAllergies" runat="server" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <div class="center formbg pad5">
                                <table class="border leftallign formbg" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlWorkupplan" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton12" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                        </td>
                                                        <td>
                                                            <h5 align="left" class="forms">
                                                                Work up plan</h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="PnlWorkupplandetails" runat="server" Style="overflow:hidden;">
                            <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="lblWork up plan-8888866" class="control-label">Work up plan:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtworkupplan" Columns="20" Rows="3" Width="99%" TextMode="MultiLine" runat="server" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>
             
 </div>

                                
                            </asp:Panel>
                            <div class="center formbg pad5">
                                <table class="border center formbg" width="100%">
                                    <tr>
                                        <td align="left">
                                            <asp:Panel ID="PnlPlan" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgPlan" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                        </td>
                                                        <td>
                                                            <h5 align="left" class="forms">
                                                                Lab Evaluation</h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="PnlPlanDetails" runat="server" Style="overflow:hidden;">
                                <table cellspacing='6' cellpadding='0' width='100%' border='0'>
                                    <tr>
                                        <td colspan="2">
                                            <uc11:Uclabeval ID="UcLabEval" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div class="center formbg pad5">
                                <table class="border leftallign formbg" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlRegimenPrescribed" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgRegimenPrescribed" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                        </td>
                                                        <td>
                                                            <h5 align="left" class="forms">
                                                                <asp:Label ID="lblheadregimenpresc" runat="server" Text="Regimen Prescribed"></asp:Label></h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="PnlRegimenPrescribeddetails" runat="server" Style="overflow:hidden;">
                            <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <UcPharmacy:Uc8 ID="UserControlKNH_Pharmacy1" runat="server" />
             </div>
            
 </div>
                                <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="lblOI Prophylaxis-8888796" class="control-label">OI Prophylaxis:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList Width="98%" runat="server" ID="ddlOIProphylaxis" CssClass="form-control"></asp:DropDownList>
             </div>
             <div id="divCotrimoxazoleprescribedfor" style="display: none;">
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
           <label id="lblCotrimoxazole prescribed for-88881192" class="control-label">Cotrimoxazole prescribed for:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList Width="98%" runat="server" ID="ddlCotrimoxazoleprescribed" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
              <div id="divFluconazoleshowhide" style="display: none;">
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <label id="Label1" class="control-label">Fluconazole prescribed for:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList runat="server" ID="ddlfluconazole" Width="98%"  CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
              <div id="divOtherOIPropholyxis" style="display: none;">
               <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <label id="lblOther (Specify)-8888578" class="control-label">Other (Specify):</label>
             </div>
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtOtherOIPropholyxis" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
              </div>
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="lblOther treatment-8888643" class="control-label">Other treatment:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtOthertreatment" Columns="20" Rows="6" Width="98%" TextMode="MultiLine" runat="server" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>
             
 </div>

                                
                            </asp:Panel>
                        </div>
                        <act:CollapsiblePanelExtender ID="CPPnlAdherence" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlAdherenceDetails" CollapseControlID="PnlAdherence"
                            ExpandControlID="PnlAdherence" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                            ImageControlID="ImgAdherence">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPPnlDrugAllergiesToxicities" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlDrugAllergiesToxicitiesdetails"
                            CollapseControlID="PnlDrugAllergiesToxicities" ExpandControlID="PnlDrugAllergiesToxicities"
                            CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="ImgDrugAllergiesToxicities">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPPnlWorkupplan" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlWorkupplandetails"
                            CollapseControlID="PnlWorkupplan" ExpandControlID="PnlWorkupplan" CollapsedImage="~/images/arrow-up.gif"
                            Collapsed="true" ImageControlID="ImgDiagnosis">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPPnlRegimenPrescribed" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlRegimenPrescribeddetails"
                            CollapseControlID="PnlRegimenPrescribed" ExpandControlID="PnlRegimenPrescribed"
                            CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="ImgRegimenPrescribed">
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
<asp:Button ID="btnManagementSave" runat="server" Text="Save" OnClick="btnManagementSave_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
<asp:Button ID="btncloseMgt" Text="Close" runat="server" OnClick="btncloseMgt_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
<asp:Button ID="btnManagementPrint" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
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
<div id="TrSignatureAll" runat="server" visible="false">
<div class="col-md-6 col-sm-12 col-xs-12 form-group pull-right"></div>
<label for="VisitDate">Signature:</label>
</div>
<div class="col-md-6 col-sm-12 col-xs-12 form-group"></div>
<asp:DropDownList ID="ddSignature" runat="server" CssClass="form-control" Width="300"> </asp:DropDownList>
                            <asp:HiddenField ID="hidupdate" runat="server" />
                            <asp:HiddenField ID="hidregimen" runat="server" />
</div>
<div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
       <uc14:UserControlKNH_BackToTop ID="UserControlKNH_BackToTop1" runat="server" />     
 </div>
 </div>

	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
  </div>

    
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
   
    
    
</asp:Content>
