<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmClinical_Nigeria_AdultIE.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_Nigeria_AdultIE" %>

<%@ Register TagPrefix="UcVitalSign" TagName="Uc1" Src="~/ClinicalForms/UserControl/UserControlNigeria_VitalSigns.ascx" %>
<%@ Register TagPrefix="UcPhysicalExamination" TagName="Uc2" Src="~/ClinicalForms/UserControl/UserControlNigeria_PhysicalExamination.ascx" %>
<%@ Register TagPrefix="UcPresentingComplaints" TagName="Uc3" Src="~/ClinicalForms/UserControl/UserControlKNHPresentingComplaints.ascx" %>
<%@ Register TagPrefix="UcNigeriaMedicalHistory" TagName="Uc4" Src="~/ClinicalForms/UserControl/UserControl_NigeriaMedicalHistory.ascx" %>
<%@ Register TagPrefix="UcWhoStaging" TagName="Uc5" Src="~/ClinicalForms/UserControl/UserControlNigeria_WHOStaging.ascx" %>
<%@ Register TagPrefix="UcDrugAllergies" TagName="Uc6" Src="~/ClinicalForms/UserControl/UserControlKNH_DrugAllergies.ascx" %>
<%@ Register TagPrefix="UcPriorArt" TagName="Uc7" Src="~/ClinicalForms/UserControl/UserControl_NigeriaPriorART.ascx" %>
<%@ Register TagPrefix="UcCurrentMed" TagName="Uc8" Src="~/ClinicalForms/UserControl/UserControl_NigeriaCurrentMedication.ascx" %>
<%@ Register TagPrefix="UcAdherence" TagName="Uc9" Src="~/ClinicalForms/UserControl/UserControl_NigeriaAdherence.ascx" %>
<%@ Register TagPrefix="UcTreatment" TagName="Uc10" Src="~/ClinicalForms/UserControl/UserControl_NigeriaTreatmentPlan.ascx" %>
<%@ Register Src="~/ClinicalForms/UserControl/UserControlKNH_LabEvaluation.ascx"
    TagName="Uclabeval" TagPrefix="uc11" %>
<%@ Register Src="~/ClinicalForms/UserControl/UserControlKNH_NextAppointment.ascx"
    TagName="UcAppoint" TagPrefix="uc12" %>
<%@ Register Src="~/ClinicalForms/UserControl/UserControlKNH_Signature.ascx" TagName="UserControlKNH_Signature"
    TagPrefix="uc12" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtVisitDate').datepicker({ autoclose: true });
        $('#txtLMPdate').datepicker({ autoclose: true });
        $('#txtEDDDate').datepicker({ autoclose: true });
        $('#dtlatestcd4date').datepicker({ autoclose: true });
        $('#txtlbllowestCD4Date').datepicker({ autoclose: true });
        $('#txtlatestViralLoadDate').datepicker({ autoclose: true });  
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
<script type="text/javascript" language="javascript">
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
    function WindowPrintAll() {
        window.print();
    }
    function fnUncheckall(strcblcontrolId) {
        var checkList = document.getElementById(strcblcontrolId);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();

        for (var i = 1; i < checkBoxList.length; i++) {
            checkBoxList[i].checked = false;
        }
    }
    function fnUncheckNormal(strcblcontrolId) {
        var checkList = document.getElementById(strcblcontrolId);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();
        checkBoxList[0].checked = false;

    }
    function checkNone(searchEles, Id_None) {
        for (var i = 0; i < searchEles.length; i++) {
            if (searchEles[i].children.length > 0) {
                for (var ii = 0; ii < searchEles[i].children.length; ii++) {
                    if (searchEles[i].children[ii].tagName == 'LABEL' && searchEles[i].children[ii].htmlFor != Id_None) {
                        document.getElementById(searchEles[i].children[ii].htmlFor).checked = false;
                    }
                }
            }
        }
    }
    function checkNotNone(searchEles, Id_None) {
        for (var i = 0; i < searchEles.length; i++) {
            if (searchEles[i].children.length > 0) {
                for (var ii = 0; ii < searchEles[i].children.length; ii++) {
                    if (searchEles[i].children[ii].tagName == 'LABEL' && searchEles[i].children[ii].textContent == "12=None") {
                        document.getElementById(searchEles[i].children[ii].htmlFor).checked = false;
                    }
                }
            }
        }
    }



    function ShowPnlforOther(param, shwpnl) {
        var searchpnl = document.getElementById(param).children;
        var chkboxId = "";
        for (var i = 0; i < searchpnl.length; i++) {
            var insidei = searchpnl[i].children;
            for (var j = 0; j < insidei.length - 1; j++) {
                var insidej = insidei[j].children;
                for (var k = 0; k < insidej.length; k++) {
                    if (insidej[k].type == "checkbox")
                        chkboxId = insidej[k].id;
                }
            }
        }

        if (document.getElementById(chkboxId).checked == true)
            show(shwpnl);

    }
    </script>
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Adult Initial Evaluation</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-6 text-right">
             <label class="control-label required">*Visit date:</label>
             </div>
             <div class="col-md-6">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtVisitDate" clientidmode="Static"
                                                       onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                            onfocus="javascript:vDateType='3'" maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>
             
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <act:TabContainer ID="tabControl" runat="server" ActiveTabIndex="0" Width="100%" CssClass="ajax__myTab">
                <act:TabPanel ID="TabPanel3" runat="server" HeaderText="Clinical History">
                    <ContentTemplate>
                        <div class="border center formbg">
                            <table class="center formbg" width="100%" border="0" class="table-condensed">
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlPComp" runat="server" CssClass="border center formbg" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="ImgPC" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblPresComp" runat="server" Text="Presenting Complaints"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" width="100%">
                                        <asp:Panel ID="pnlTPComp" runat="server" Style="overflow:hidden;">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <UcPresentingComplaints:Uc3 ID="UcPc" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg" width="100%" border="0" class="table-condensed">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlNigeriaMedical" runat="server" CssClass="border center formbg"
                                            Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgNigeriaMedical" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblNigeriaMedical" runat="server" Text="Medical History"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnlNigeriaMedicalDetails" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <UcNigeriaMedicalHistory:Uc4 ID="idNigeriaMedical" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table id="TabManagement" class="center formbg" width="100%" border="0" >
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Panel ID="pnlDAToxities" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgDAToxities" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                Drug Allergies</h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnlDAToxitiesDetail" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <UcDrugAllergies:Uc6 ID="UCDrugAllergies" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg" width="100%" border="0" class="table-condensed">
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlriskfactors" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="Imgriskfactors" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblriskfactors" runat="server" Text="Risk Factors"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlriskfactorsDetail" runat="server" Style="overflow:hidden;">
                                        <div class="row">
 <div class="col-md-5">
 <%--left--%>
 <div class="row">
 <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblriskfactorslist" runat="server" Font-Bold="True" Text="Risk Factors"></asp:Label></label>
             </div>
           
 </div>
 <div class="row">
 <div class="col-md-10 col-sm-12 col-xs-12 form-group">
            <div class="customdivbordermultiselect">
                                                            <asp:CheckBoxList ID="cblRiskFactors" runat="server" CssClass="checkbox-btn">
                                                            </asp:CheckBoxList>
                                                        </div>
             </div>
           
 </div>
 </div>
 <div class="col-md-7">
 <%--right--%>
  <div  class="row" id="divotherriskfactors" style="display: none">
  <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other medical conditions :</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtOtherriskfactors" runat="server" Width="95%" CssClass="form-control"></asp:TextBox>
             </div>
  </div>

 </div>
 </div>
                                            
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg" width="100%" border="0" class="table-condensed">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlpregnant" runat="server" CssClass="border center formbg" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgpregnant" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblpregnant" runat="server" Text="Pregnancy"></asp:Label>
                                            </h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr id="trNigPregnancy">
                                    <td>
                                        <asp:Panel ID="pnlpregnantDetail" runat="server" Style="overflow:hidden;">
                                        <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" for="LMP">LMP:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtLMPdate" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>

            
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblpregnantlabal">Currently pregnant:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rdopregnantyesno" runat="server" RepeatDirection="Horizontal"
                                                                        OnClick="rblSelectedValue(this,'hideFP')" CssClass="checkbox-btn">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
             </div> 
 </div>
 <div class="row" id="hideFP" style="display: none">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" for="EDD">EDD:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtEDDDate" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblgestage" for="gestage">Gestational age (wks):</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtgestage" runat="server" Columns="2" MaxLength="3" CssClass="form-control" Width="98%"></asp:TextBox>
             </div> 
 </div>
                                            
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <act:CollapsiblePanelExtender ID="CPEHIVCare" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlTPComp" CollapseControlID="pnlPComp"
                            ExpandControlID="pnlPComp" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="ImgPC" Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPENigeriaMedical" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlNigeriaMedicalDetails"
                            CollapseControlID="pnlNigeriaMedical" ExpandControlID="pnlNigeriaMedical" CollapsedImage="~/images/arrow-up.gif"
                            Collapsed="True" ImageControlID="imgNigeriaMedical" Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEDAT" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlDAToxitiesDetail" CollapseControlID="pnlDAToxities"
                            ExpandControlID="pnlDAToxities" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgDAToxities" Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEriskfactors" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlriskfactorsDetail"
                            CollapseControlID="pnlriskfactors" ExpandControlID="pnlriskfactors" CollapsedImage="~/images/arrow-up.gif"
                            Collapsed="True" ImageControlID="imgriskfactors" Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEpregnant" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlpregnantDetail" CollapseControlID="pnlpregnant"
                            ExpandControlID="pnlpregnant" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgpregnant" Enabled="True"></act:CollapsiblePanelExtender>
                 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureCH" runat="server" />
             </div>
             
 </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnClinicalHistorySave" runat="server" OnClick="btnClinicalHistorySave_Click"
                                            Text="Save" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
                                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                        <asp:Button ID="btnClinicalHistoryPrint" runat="server" OnClientClick="WindowPrintAll();"
                                            Text="Print" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
                                            <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
 </div>
 </div>

                        <div class="border center formbg">
                            <table cellspacing="6" cellpadding="0" width="100%" border="0" id="Table4" runat="server">
                                <tr id="Tr1" runat="server" align="center">
                                    <td id="Td1" runat="server" class="form">
                                        
                                    </td>
                                </tr>
                                <tr id="Tr2" runat="server" align="center">
                                    <td id="Td2" runat="server" class="form">
                                        
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabHIVHistory" runat="server" HeaderText="HIV History">
                    <ContentTemplate>
                        <div class="border center formbg">
                            <table class="center formbg" width="100%" border="0" class="table-condensed">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlhivrelhistory" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imghivrelhistory" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblhivrelhistory" runat="server" Text="HIV Related History"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnlhivrelhistoryDetails" runat="server" Style="overflow:hidden;">
                                        <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">
             <asp:Label ID="lbllatestCD4" runat="server"  Text="Latest CD4:"></asp:Label>
             <asp:Label ID="lbllatestCD4number" runat="server"></asp:Label></label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label"><asp:Label ID="lbllatestCD4count" runat="server" Text="Count:"></asp:Label></label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtlatestcd4number" runat="server" Columns="2" MaxLength="3" CssClass="form-control" Width="80"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label"><asp:Label ID="lbllatestcd4date" runat="server" Text="Date:"></asp:Label></label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtlatestcd4date" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                       onblur="DateFormat(this,this.value,event,false,'3')"
                                                                        onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                                        size="11"/>
                                                </div>
                                            </div>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             
             </div> 
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">
             <asp:Label ID="lbllowestCD4" runat="server" Text="Lowest CD4:"></asp:Label>
             <asp:Label ID="lbllowestCD4Value" runat="server"></asp:Label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">
             <asp:Label ID="lbllowestCD4Count" runat="server" Text="Count:"></asp:Label>                                                                    
             </label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtlowestCD4Count" runat="server" Columns="2" MaxLength="3" Width="80" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">
             <asp:Label ID="lbllowestCD4Date" runat="server" Text="Date:"></asp:Label>
             </label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtlbllowestCD4Date" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                       onblur="DateFormat(this,this.value,event,false,'3')"
                                                                        onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                                        size="11"/>
                                                </div>
                                            </div>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
              <asp:CheckBox runat="server" Text="Lab records seen" ID="chklowestcd4labrecord" CssClass="checkbox-btn" />
             </div> 
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">
             <asp:Label ID="lbllatestViralLoad" runat="server" Text="Latest ViralLoad:"></asp:Label>
             <asp:Label ID="lbllatestViralLoadValue" runat="server"></asp:Label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">
             <asp:Label ID="lbllatestViralLoadCount" runat="server" Text="Count:"></asp:Label>
                                                                    
             </label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtlatestViralLoadCount" runat="server" Columns="2" MaxLength="3" Width="80" CssClass="form-control"></asp:TextBox>             
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">
             <asp:Label ID="lbllatestViralLoadDate" runat="server" Text="Date:"></asp:Label>
             </label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtlatestViralLoadDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                       onblur="DateFormat(this,this.value,event,false,'3')"
                                                                        onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                                        size="11"/>
                                                </div>
                                            </div>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
             <asp:CheckBox runat="server" Text="Lab records seen" ID="chkviralloadlabrecord" CssClass="checkbox-btn"/>
             </div> 
 </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg" width="100%" border="0" class="table-condensed">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlpriorart" runat="server" CssClass="border center formbg" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgpriorart" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="Label2" runat="server" Text="ARV Exposure"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnlpriorartDetails" runat="server" Style="overflow:hidden;">
                                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <UcPriorArt:Uc7 ID="UcPriorArt" runat="server" />
             </div>
            
 </div>
   <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <UcCurrentMed:Uc8 ID="UcCurrentMed" runat="server" />
 </div>
 </div>
                                  
                                  <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Service entry into program :</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlreferredfrom" runat="server" Width="300" CssClass="form-control"></asp:DropDownList>
             </div>
             
 </div>          
                                            
                                            
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg" width="100%" border="0" class="table-condensed">
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="PnlAdherence" runat="server" CssClass="border center formbg" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgAdherence" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="Label3" runat="server" Text="Adherence"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" width="100%">
                                        <asp:Panel ID="PnlAdherenceDetails" runat="server" Style="overflow:hidden;">
                                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <UcAdherence:Uc9 ID="UcAdherence" runat="server" />
             </div>
            
 </div>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg table-condensed" width="100%" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:Panel ID="pnlARVSideeffects" CssClass="border center formbg" runat="server"
                                            Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="ImgARVSideEffect" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                ARV Side Effects</h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnlARVSideEffectDetail" runat="server" Style="overflow:hidden;">
                                        
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
            <div id="divShortTermEffects" class="customdivbordermultiselect" style="margin-bottom: 20px">
                                                                        <asp:CheckBoxList ID="cblShortTermEffects" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                                                        </asp:CheckBoxList>
                                                                    </div>
             </div>
           
 </div>
 <div id="divshorttermeffecttxt" style="display: none">
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-nowrap">
 <label class="control-label">Specify other short term effects:</label>
 </div>
  </div>
 <div class="row">
              <div class="col-md-12 col-sm-12 col-xs-12 form-group">
              <asp:TextBox ID="txtOtherShortTermEffects" runat="server" Skin="MetroTouch" Width="99%" CssClass="form-control"></asp:TextBox>
              </div>
           
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
            <div id="divLongTermEffects" class="customdivbordermultiselect" style="margin-bottom: 20px">
                                                                        <asp:CheckBoxList ID="cblLongTermEffects" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                                                        </asp:CheckBoxList>
                                                                    </div>
             </div>
           
 </div>
 <div id="divlongtermeffecttxt" style="display: none">
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Specify other long term effects:</label>
             </div>
             </div>
              <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtOtherLongtermEffects" runat="server" Skin="MetroTouch" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
         </div>  
 </div>
 </div>
 </div>
                                            
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <act:CollapsiblePanelExtender ID="CPEhivrelhistory" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlhivrelhistoryDetails"
                            CollapseControlID="pnlhivrelhistory" ExpandControlID="pnlhivrelhistory" CollapsedImage="~/images/arrow-up.gif"
                            Collapsed="True" ImageControlID="imghivrelhistory" Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEpriorart" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlpriorartDetails" CollapseControlID="pnlpriorart"
                            ExpandControlID="pnlpriorart" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgpriorart" Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEAdherence" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlAdherenceDetails" CollapseControlID="PnlAdherence"
                            ExpandControlID="PnlAdherence" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgAdherence" Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEARVSE" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlARVSideEffectDetail"
                            CollapseControlID="pnlARVSideeffects" ExpandControlID="pnlARVSideeffects" CollapsedImage="~/images/arrow-up.gif"
                            Collapsed="True" ImageControlID="ImgARVSideEffect" Enabled="True"></act:CollapsiblePanelExtender>
                        <br />
                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">           
            <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureHH" runat="server" />            
             </div>
          
 </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">  
 <asp:Button ID="btnHIVHistorySave" runat="server" Text="Save" OnClick="btnHIVHistorySave_Click"
                                            CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
                                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                        <asp:Button ID="btnHIVHistoryPrint" runat="server" OnClientClick="WindowPrintAll();"
                                            CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" Text="Print" />    
                                            <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>  
 </div>
 </div>
                        
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabPanelExamination" runat="server" HeaderText="Examination">
                    <ContentTemplate>
                        <div class="border center formbg">
                            <table class="center formbg table-condensed" width="100%" border="0">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlvitalsign" runat="server" CssClass="border center formbg" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgvitalsign" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblVitalSigns" runat="server" Text="Vital Signs"></asp:Label>
                                            </h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnlVitalSignDetail" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <UcVitalSign:Uc1 ID="idVitalSign" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg table-condensed" width="100%" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:Panel ID="pnlPE" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgPE" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblPhysicalExamination" runat="server" Text="Physical Examination"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnlPEDetail" runat="server" Style="overflow:hidden;">
                                        <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <UcPhysicalExamination:Uc2 ID="UcPE" runat="server" />
             </div>
             
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
 <label id="Label4" class="control-label">Assessment :</label>           
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rblassessment" runat="server" RepeatDirection="Horizontal" CssClass="checkbox-btn">
                                                                        </asp:RadioButtonList>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <label id="lblOther general conditions-8888326" class="control-label">Assessment Notes :</label>             
             </div>
           
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
          <asp:TextBox ID="txtAssessmentNotes" ClientIDMode="Static" runat="server" Width="99%"
                                                                            Rows="3" TextMode="MultiLine" CssClass="form-control" Style="resize:none:"></asp:TextBox>
             </div>
            
 </div>
                                            
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg" width="100%" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:Panel ID="pnlWHOStage" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgWHOStage" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblheadWHOStage" runat="server" Text="WHO Stage"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnlWHOStageDetail" runat="server" Style="overflow:hidden;">
                                        <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <UcWhoStaging:Uc5 ID="UcWhostaging" runat="server" />
             </div>
            
 </div>
                                            
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <act:CollapsiblePanelExtender ID="CPEVitalSign" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlVitalSignDetail" CollapseControlID="pnlVitalSign"
                            ExpandControlID="pnlVitalSign" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgVitalSign" Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEPE" runat="server" SuppressPostBack="True" ExpandedImage="~/images/arrow-dn.gif"
                            TargetControlID="pnlPEDetail" CollapseControlID="pnlPE" ExpandControlID="pnlPE"
                            CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgLTM"
                            Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPWHOStage" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlWHOStageDetail" CollapseControlID="pnlWHOStage"
                            ExpandControlID="pnlWHOStage" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="ImgWHOStage" Enabled="True"></act:CollapsiblePanelExtender>
                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureExamination" runat="server" />
             </div>
              
 </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnExaminationSave" runat="server" OnClick="btnExaminationSave_Click" Text="Save" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
 <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
 <asp:Button ID="btnExaminationPrint" runat="server" OnClientClick="WindowPrintAll();" Text="Print" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
 <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff;"></label>
                                                
 </div>
 </div>
                        <div class="border center formbg">
                            <table cellspacing="6" cellpadding="0" width="100%" border="0" id="Table2" runat="server">
                                <tbody>
                                 <%--   <tr id="Tr5" runat="server" align="center">
                                        <td id="Td5" runat="server" class="form">
                                            
                                        </td>
                                    </tr>--%>
                                    <tr id="Tr6" runat="server" align="center">
                                        <td id="Td6" runat="server" class="form">
                                            
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="Tabmanagement" runat="server" HeaderText="Management">
                    <ContentTemplate>
                        <div class="border center formbg">
                            <table class="center formbg table-condensed" width="100%" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:Panel ID="pnlTreatment" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgtreatment" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblheadregimenpresc" runat="server" Text="Treatment Plan"></asp:Label>
                                            </h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnlTreatmentdetail" runat="server" Style="overflow:hidden;">
                                        <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <UcTreatment:Uc10 ID="UCTreatment" runat="server" />
             </div>
             
 </div>
                                            
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="center formbg table-condensed" width="100%" border="0">
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Panel ID="pnlAppointmentsHeader" CssClass="border center formbg" runat="server"
                                            Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgAppointments" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblAppointment" runat="server" Text="Next Appointment"></asp:Label>
                                            </h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:Panel ID="pnlAppointmentsBody" runat="server" Style="overflow:hidden;">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <uc12:UcAppoint ID="UserControlKNH_NextAppointment1" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <act:CollapsiblePanelExtender ID="CPETreatment" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlTreatmentdetail" CollapseControlID="pnlTreatment"
                                ExpandControlID="pnlTreatment" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="imgtreatment" Enabled="True"></act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlAppointmentsBody" CollapseControlID="pnlAppointmentsHeader"
                                ExpandControlID="pnlAppointmentsHeader" CollapsedImage="~/images/arrow-up.gif"
                                Collapsed="True" ImageControlID="imgAppointments" Enabled="True"></act:CollapsiblePanelExtender>
                        </div>
                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureMgt" runat="server" />
             </div>
            
 </div>
 <div class="row" align="center" id="tblSaveButton">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="btnSaveMgt" runat="server" Text="Save" OnClick="btnSaveTriage_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
<label class="glyphicon glyphicon-floppy-disk" style="margin-left: -4%; margin-right:3%; vertical-align: sub; color: #fff;"></label>
<asp:Button ID="btnPrintMgt" Text="Print" OnClientClick="WindowPrintAll();" runat="server" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
 <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right:2%"></label>
 </div>
 </div>
                        <div class="border center formbg">
                            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr align="center">
                                        <td class="form">
                                            
                                        </td>
                                    </tr>
                                    <tr id="tblSaveButton11" align="center">
                                        <td class="form">
                                          
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </ContentTemplate>
                </act:TabPanel>
            </act:TabContainer>
 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           <asp:Button ID="btnClose" Text="Close" runat="server" OnClick="btnClose_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
           <label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
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
