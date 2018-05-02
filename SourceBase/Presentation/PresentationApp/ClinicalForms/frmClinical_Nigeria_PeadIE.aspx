<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmClinical_Nigeria_PeadIE.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_Nigeria_PeadIE" %>

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
    </script>
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtVisitDate').datepicker({ autoclose: true });
        }
        //Calling RegisterJQuery when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);
        //Calling RegisterJQuery when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>

      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Nigeria Paediatric Initial Evaluation</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
            <label class="control-label required">*Visit date:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtVisitDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                       onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                            onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-12">
  <act:TabContainer ID="tabControl" runat="server" ActiveTabIndex="0" Width="100%" CssClass="ajax__myTab">    
                    <act:TabPanel ID="TabPanel3" runat="server" HeaderText="Clinical History">
                    <ContentTemplate>
                    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlNigeriaMedical" runat="server">
          <h5 class="forms" align="left">
        <asp:ImageButton ID="imgNigeriaMedical" ImageUrl="~/images/arrow-up.gif" runat="server" />
          <asp:Label ID="lblNigeriaMedical" runat="server" Text="Medical History"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="pnlNigeriaMedicalDetails" runat="server" style="overflow:hidden;">
        <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<UcNigeriaMedicalHistory:Uc4 ID="idNigeriaMedical" runat="server" />
             </div>
             
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Developmental Assessment :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddldevlopAssessment" runat="server" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Immunisation: Complete for age :</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rdoImmunAgryesno" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Mode of infant feeding :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
<asp:DropDownList ID="ddlfeedingMode" runat="server" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<UcCurrentMed:Uc8 ID="UcCurrentMed" runat="server" />
             </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Service entry into program :</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlreferredfrom" runat="server" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
           
             </div> 
 </div>
 </asp:Panel>
 <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlpriorart" runat="server">
          <h5 class="forms" align="left">
       <asp:ImageButton ID="imgpriorart" ImageUrl="~/images/arrow-up.gif" runat="server" />
       <asp:Label ID="lblpriorart" runat="server" Text="Prior ART"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
 <asp:Panel ID="pnlpriorartDetails" runat="server" style="overflow:hidden;">
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Patient has received previous care for HIV/AIDS :</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="rdopreviousHIV" runat="server" RepeatDirection="Horizontal"
                                                                        OnClick="rblSelectedValue(this,'divshowpriorart')">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">

             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">

             </div> 
 </div>
 <div class="row" id="divshowpriorart"  style="display: none;">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<UcPriorArt:Uc7 ID="UcPriorArt" runat="server" />            
 </div>
 </div>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Previous ARV exposure other than PMTCT :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
 <asp:DropDownList ID="ddlPrevARVExpoPMTCT" runat="server" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             
             </div> 
 </div>
<div class="row" id="divshowPrevARVExpoPMTCT" style="display: none;">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group text-righ">
             <label class="control-label">Specify months :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtPreExpSpecifyMonths" runat="server" Width="50%" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-righ">
             <label class="control-label">Specify drugs :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtPreExpSpecifyDrugs" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div> 
 </div>     
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <UcDrugAllergies:Uc6 id="UCDrugAllergies" runat="server" />
             </div>
 </div>           
</asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlAdherence" runat="server">
          <h5 class="forms" align="left">
<asp:ImageButton ID="imgAdherence" ImageUrl="~/images/arrow-up.gif" runat="server" />
<asp:Label ID="lbladherence" runat="server" Text="Adherence"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="PnlAdherenceDetails" runat="server" style="overflow:hidden;">
<div class="row">
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

                        
                        <act:CollapsiblePanelExtender ID="CPENigeriaMedical" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlNigeriaMedicalDetails"
                            CollapseControlID="pnlNigeriaMedical" ExpandControlID="pnlNigeriaMedical" CollapsedImage="~/images/arrow-up.gif"
                            Collapsed="True" ImageControlID="imgNigeriaMedical" Enabled="True">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEpriorart" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlpriorartDetails" CollapseControlID="pnlpriorart"
                            ExpandControlID="pnlpriorart" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgpriorart" Enabled="True">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEAdherence" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlAdherenceDetails" CollapseControlID="PnlAdherence"
                            ExpandControlID="PnlAdherence" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgAdherence" Enabled="True">
                        </act:CollapsiblePanelExtender>
                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureCH" runat="server" />
             </div>
 </div>
                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="btnClinicalHistorySave" runat="server" OnClick="btnClinicalHistorySave_Click" Text="Save" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />                                        
<label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
 <asp:Button ID="btnClinicalHistoryPrint" runat="server" OnClientClick="WindowPrintAll()" Text="Print" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
 <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
             </div>
 </div>
                       
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabPanelExamination" runat="server" HeaderText="Examination">
                    <ContentTemplate>
                    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlvitalsign" runat="server">
          <h5 class="forms" align="left">
        <asp:ImageButton ID="imgvitalsign" ImageUrl="~/images/arrow-up.gif" runat="server" />
         <asp:Label ID="lblVitalSigns" runat="server" Text="Vital Signs"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
          <asp:Panel ID="pnlVitalSignDetail" runat="server" style="overflow:hidden;">
          <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<UcVitalSign:Uc1 ID="idVitalSign" runat="server" />
             </div>
            
 </div>
                                            
                                        </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlPE" runat="server">
          <h5 class="forms" align="left">
       <asp:ImageButton ID="imgPE" ImageUrl="~/images/arrow-up.gif" runat="server" />
<asp:Label ID="lblPhysicalExamination" runat="server" Text="Physical Examination"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
 <asp:Panel ID="pnlPEDetail" runat="server" style="overflow:hidden;">
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <UcPhysicalExamination:Uc2 ID="UcPE" runat="server" />
             </div>
            
 </div>
                                
                            </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="PnlAssessment" runat="server">
          <h5 class="forms" align="left">
        <asp:ImageButton ID="ImgAssessment" ImageUrl="~/images/arrow-up.gif" runat="server" />
        <asp:Label ID="lblPnlAssessment" runat="server" Text="Assessment"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="PnlAssessmentDetails" runat="server" style="overflow:hidden;">
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:RadioButtonList ID="rblassessment" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label required"><asp:Label ID="lblWHOStage" runat="server" Text="WHO Stage:"></asp:Label></label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlwhostage1" runat="server" Width="35%" CssClass="form-control"></asp:DropDownList>
             </div>
            
 </div>
                                
                            </asp:Panel>
                        <act:CollapsiblePanelExtender ID="CPEVitalSign" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlVitalSignDetail" CollapseControlID="pnlVitalSign"
                            ExpandControlID="pnlVitalSign" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgVitalSign" Enabled="True">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEPE" runat="server" SuppressPostBack="True" ExpandedImage="~/images/arrow-dn.gif"
                            TargetControlID="pnlPEDetail" CollapseControlID="pnlPE" ExpandControlID="pnlPE"
                            CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="imgPE"
                            Enabled="True">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEPnlAssessment" runat="server" SuppressPostBack="True" ExpandedImage="~/images/arrow-dn.gif"
                            TargetControlID="PnlAssessmentDetails" CollapseControlID="PnlAssessment" ExpandControlID="PnlAssessment"
                            CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgAssessment"
                            Enabled="True">
                        </act:CollapsiblePanelExtender>
                       <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureExamination" runat="server" />
             </div>
            
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnExaminationSave" runat="server" OnClick="btnExaminationSave_Click" Text="Save" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"  />                                            
 <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
 <asp:Button ID="btnExaminationPrint" runat="server" OnClientClick="WindowPrintAll();" Text="Print" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"  />
 <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
             </div>
            
 </div>
                        
                    </ContentTemplate>
                </act:TabPanel>
  <act:TabPanel ID="Tabmanagement" runat="server" HeaderText="Management">
                    <ContentTemplate>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlTreatment" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="imgtreatment" ImageUrl="~/images/arrow-up.gif" runat="server" />
         <asp:Label ID="lblheadregimenpresc" runat="server" Text="Treatment Plan"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="pnlTreatmentdetail" runat="server" Style="overflow:hidden;">
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <UcTreatment:Uc10 ID="UCTreatment" runat="server" />
             </div>
            
 </div>
 </asp:Panel>
 <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlAppointmentsHeader" runat="server">
          <h5 class="forms" align="left">
       <asp:ImageButton ID="imgAppointments" ImageUrl="~/images/arrow-up.gif" runat="server" />
        <asp:Label ID="lblAppointment" runat="server" Text="Next Appointment"></asp:Label>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="pnlAppointmentsBody" runat="server" Style="overflow:hidden;">
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <uc12:UcAppoint ID="UserControlKNH_NextAppointment1" runat="server" />
             </div>
            
 </div>
 </asp:Panel>
                       
                            <act:CollapsiblePanelExtender ID="CPETreatment" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlTreatmentdetail" CollapseControlID="pnlTreatment"
                                ExpandControlID="pnlTreatment" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                ImageControlID="imgtreatment" Enabled="True">
                            </act:CollapsiblePanelExtender>
                            <act:CollapsiblePanelExtender ID="CPELAppointment" runat="server" SuppressPostBack="True"
                                ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlAppointmentsBody" CollapseControlID="pnlAppointmentsHeader"
                                ExpandControlID="pnlAppointmentsHeader" CollapsedImage="~/images/arrow-up.gif"
                                Collapsed="True" ImageControlID="imgAppointments" Enabled="True">
                            </act:CollapsiblePanelExtender>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureMgt" runat="server" />
             </div>
            
 </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnSaveMgt" runat="server" Text="Save" OnClick="btnSaveTriage_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
 <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
<asp:Button ID="btnPrintMgt" Text="Print" OnClientClick="WindowPrintAll();" runat="server" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
<label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
             </div>
            
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
    
</asp:Content>
