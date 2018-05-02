<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    CodeBehind="frmClinical_KNH_AdultIE.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_KNH_AdultIE" %>
   
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="UcVitalSign" TagName="Uc1" Src="~/ClinicalForms/UserControl/UserControlKNH_VitalSigns.ascx" %>
<%@ Register TagPrefix="UcTBScreening" TagName="Uc1" Src="~/ClinicalForms/UserControl/UserControlKNH_TBScreening.ascx" %>
<%@ Register TagPrefix="UcMedicalHistory" TagName="Uc2" Src="~/ClinicalForms/UserControl/UserControlKNH_MedicalHistory.ascx" %>
<%@ Register TagPrefix="UcPresentingComplaints" TagName="Uc3" Src="~/ClinicalForms/UserControl/UserControlKNHPresentingComplaints.ascx" %>
<%@ Register TagPrefix="UcPhysicalExamination" TagName="Uc4" Src="~/ClinicalForms/UserControl/UserControlKNH_PhysicalExamination.ascx" %>
<%@ Register TagPrefix="UcWhoStaging" TagName="Uc5" Src="~/ClinicalForms/UserControl/UserControlKNH_WHOStaging.ascx" %>
<%@ Register TagPrefix="UcDrugAllergies" TagName="Uc6" Src="~/ClinicalForms/UserControl/UserControlKNH_DrugAllergies.ascx" %>
<%@ Register TagPrefix="UcPharmacy" TagName="Uc7" Src="~/ClinicalForms/UserControl/UserControlKNH_Pharmacy.ascx" %>
<%@ Register TagPrefix="UcPWP" TagName="Uc9" Src="~/ClinicalForms/UserControl/UserControlKNH_PwP.ascx" %>
<%--<%@ Register Src="UserControl/UserControlKNH_Extruder.ascx" TagName="UserControlKNH_Extruder"
    TagPrefix="uc10" %>--%>
<%@ Register Src="~/ClinicalForms/UserControl/UserControlKNH_LabEvaluation.ascx"
    TagName="Uclabeval" TagPrefix="uc11" %>
<%@ Register Src="~/ClinicalForms/UserControl/UserControlKNH_Signature.ascx" TagName="UserControlKNH_Signature"
    TagPrefix="uc12" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ Register Src="UserControl/UserControlKNH_BackToTop.ascx" TagName="UserControlKNH_BackToTop"
    TagPrefix="uc1" %>
<asp:Content ID="content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtVisitDate').datepicker({ autoclose: true });
        $('#txtdtConfirmHIVPosDate').datepicker({ autoclose: true });
        $('#dtInitialCD4Date').datepicker({ autoclose: true });
        $('#dtHighestCD4Date').datepicker({ autoclose: true });
        $('#dtCD4atARTinitiationDate').datepicker({ autoclose: true });
        $('#dtMostRecentCD4Date').datepicker({ autoclose: true });
        $('#dtPreviousViralLoadDate').datepicker({ autoclose: true });
        $('#dtPMTCTStartDate').datepicker({ autoclose: true });
        $('#dtPEP1StartDate').datepicker({ autoclose: true });
        $('#dtHAART1StartDate').datepicker({ autoclose: true });   
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>


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
    
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">
            <asp:Label ID="lblHeading" runat="server" Text="Label"></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
        <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label required">Visit date:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtVisitDate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
                                            </div>
                                  
             </div>
             <div class="row">
        <div class="col-md-12">
        <act:TabContainer ID="tabControl" runat="server" ActiveTabIndex="0" Width="100%" CssClass="ajax__myTab">
                <act:TabPanel ID="TabPanel3" runat="server" HeaderText="Triage">
                    <ContentTemplate>
                    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Panel ID="pnlHIVCare" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgHIVCare" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblheadTriage" runat="server" Text="HIV Care and Support Evaluation"></asp:Label>
                                            </h5>
                                        </asp:Panel>
             </div>
                                 
    </div>
                            <asp:Panel ID="pnlHivCareDetail" runat="server" Style="overflow:hidden;">
                            <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label for="rblDiagnosisYesNo" class="control-label">Diagnosis confirmed:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rblDiagnosisYesNo" runat="server" onclick="rblSelectedValue(this,'DIVHIVDiagnosis');"
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
             </div>
             <div id="DIVHIVDiagnosis" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date of HIV diagnosis:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtdtConfirmHIVPosDate" clientidmode="Static"
                                                       size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div> </div>   
             </div>                   
    </div>
                                <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:Label ID="lblcaregiver" runat="server" CssClass="control-label required" Font-Bold="True" Text="Patient accompanied by caregiver:"></asp:Label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rblChildAccompaniedBy" runat="server" RepeatDirection="Horizontal"
                                                            OnClick="rblSelectedValue(this,'divTreatmentSupporterRelationship')">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
             </div>
             <div id="divTreatmentSupporterRelationship" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Caregiver relationship:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlTreatmentSupporterRelationship" runat="server" Width="180px" CssClass="form-control"> </asp:DropDownList>
             </div> 
             </div>  
                               
    </div>
    <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <label id='lblHealth education given?-88881455' class="control-label">Health education given?:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rdoHealthEducation" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
             </div>
                                 
    </div>
                               
  <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">If adolescent, disclosure status:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlDisclosureStatus" runat="server" Width="180px" CssClass="form-control"></asp:DropDownList>
             </div>
             <div id="divSpecifyreason" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify reason not disclosed:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddreasondisclosed" runat="server" Width="170px" CssClass="form-control"></asp:DropDownList>
             </div>  
             </div>   
                              
    </div> 
    <div class="row">
    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
    </div>
    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
    </div>
    <div id="divSpecifyotherdisclosurestatus" style="display: none;">
    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
    <label class="control-label">Specify other disclosure status:</label>
    </div>
    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
    <asp:TextBox ID="txtotherdisclosurestatus" runat="server" CssClass="form-control" Width="250"></asp:TextBox>
    </div>
    </div>
    </div>
   <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">If adolescent, schooling status:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlSchoolingStatus" runat="server" Width="150px" CssClass="form-control"></asp:DropDownList>
             </div>
             <div id="divHighestLevelattained" style="display: none">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Highest level attained:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddHighestlevelattained" runat="server" Width="180px" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>                     
    </div>
    <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Is client a member of a support group?</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rblHIVSupportgroup" runat="server" RepeatDirection="Horizontal"
                                                            onclick="rblSelectedValue(this,'DIVsupportgroup');" CssClass="checkbox-btn">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
             </div>
             <div id="DIVsupportgroup" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">HIV support group membership:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtHIVSupportGroup" runat="server" CssClass="form-control" Width="250"></asp:TextBox>
             </div>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblPatRef" runat="server" CssClass="required" Font-Bold="True" Text="Patient referred from:"></asp:Label></label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlPatientReferred" runat="server" Width="150px" CssClass="form-control"></asp:DropDownList>
             </div>
             <div id="divPatRefother" style="display: none">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" for="txtPatRefother">Other specify:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtPatRefother" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>
             </div>      
             </div>                
    </div>
    
                            </asp:Panel>
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
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                        <act:CollapsiblePanelExtender ID="CPEHIVCare" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlHivCareDetail" CollapseControlID="pnlHIVCare"
                            ExpandControlID="pnlHIVCare" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgHIVCare" Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEVitalSign" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlVitalSignDetail" CollapseControlID="pnlVitalSign"
                            ExpandControlID="pnlVitalSign" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="imgVitalSign" Enabled="True"></act:CollapsiblePanelExtender>                  
                         <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureTriage" runat="server" />
 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="tblSaveButton">
 <asp:Button ID="btnSaveTriage" runat="server" Text="Save" OnClick="btnSaveTriage_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
                                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                            <asp:Button ID="btnCloseTriage" Text="Close" runat="server" OnClick="btncloseTriage_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
                                            <label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
                                            <asp:Button ID="btnPrintTriage" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
                                            <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="tblDeleteButton" style="display: none" >
 <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" CssClass="btn btn-primary"  Height="30px" Width="9%" Style="text-align:left;"/>
 </div>
 </div>
                        
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabClinicalHistory" runat="server" HeaderText="Clinical History">
                    <HeaderTemplate>
                        Clinical History
                    </HeaderTemplate>
                    <ContentTemplate>
                    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Panel ID="pnlPComp" runat="server" CssClass="border center formbg" Style="padding: 6px;">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="ImgPC" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblPresComp" runat="server" Text="Presenting Complaints"></asp:Label></h5>
                                        </asp:Panel>
             </div>                                   
    </div>
    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <asp:Panel ID="pnlTPComp" runat="server" Style="overflow:hidden;">
                                            <UcPresentingComplaints:Uc3 ID="UcPc" runat="server" />
                                        </asp:Panel>
 </div>
       </div>                 
<div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Panel ID="pnlFP" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="ImgFP" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblheadMedicalCond" runat="server" Text="Pre-Existing (Known Conditions)-Medical Conditions"></asp:Label></h5>
                                        </asp:Panel>
 </div>
 </div>
                            
                            <asp:Panel ID="pnlFPDetail" runat="server" Style="overflow:hidden;">
                            <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblchlongtermmedication" runat="server" CssClass="required"
                                                Text="Long term medications:"></asp:Label></label>
             </div>                                 
    </div>
    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div class="customdivbordermultiselect">
                                                <asp:CheckBoxList ID="cblPreExistingMedConditions" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
             </div>                                 
    </div>
    <div class="row">
    <div id="divothermedcondition" style="display: none">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other medical conditions :</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtOthermedicalconditions" runat="server" Width="90%" CssClass="form-control"></asp:TextBox>
             </div>
             </div>
                                  
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Are you on followup?</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rblFP" runat="server" RepeatDirection="Horizontal" OnClick="rblSelectedValue(this,'hideFP')" CssClass="checkbox-btn">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
             </div>
             <div id="hideFP" style="display: none;">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Last follow up/treatment :</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtLastFP" runat="server" Rows="4" TextMode="MultiLine" Width="100%" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>     
             </div>                 
    </div>
                                
                            </asp:Panel>
                        
                        <act:CollapsiblePanelExtender ID="CPPC" runat="server" SuppressPostBack="true" ExpandedImage="~/images/arrow-dn.gif"
                            TargetControlID="pnlTPComp" CollapseControlID="pnlPComp" ExpandControlID="pnlPComp"
                            CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="ImgPC">
                        </act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEFP" runat="server" SuppressPostBack="true" ExpandedImage="~/images/arrow-dn.gif"
                            TargetControlID="pnlFPDetail" CollapseControlID="pnlFP" ExpandControlID="pnlFP"
                            CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="ImgFP">
                        </act:CollapsiblePanelExtender>
                          <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureClinical" runat="server" />
 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnSaveCHistory" runat="server" Text="Save" OnClick="btnSaveCHistory_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
 <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
 <asp:Button ID="btncloseCHistory" Text="Close" runat="server" OnClick="btncloseCHistory_Click" CssClass="btn btn-primary"  Height="30px" Width="9%" Style="text-align:left;" />
 <label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
 <asp:Button ID="btnPrintCHistory" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary"  Height="30px" Width="9%" Style="text-align:left;" />
 <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
 </div>
 </div>

                        
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabpnlTBSCreening" runat="server" HeaderText="TB Screening">
                    <HeaderTemplate>
                        TB Screening
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="border center formbg">
                            <table class="center formbg" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td colspan="2" width="100%">
                                        <asp:Panel ID="Panel3" runat="server" Style="overflow:hidden;">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <UcTBScreening:Uc1 ID="UcTBScreening" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabpnlExamination" runat="server" HeaderText="Examination">
                    <HeaderTemplate>
                        Examination
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="border center formbg">
                            <table id="TabExamination" class="center formbg" cellspacing="6" cellpadding="0"
                                width="100%" border="0">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlLTM" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="ImgLTM" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblCurrentLongTermMedications" runat="server" Text="Current Long Term Medications"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlLTMedicine" runat="server" Style="overflow:hidden;">
                            <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblchkLTMedications" runat="server" CssClass="required" Font-Bold="True"
                                                            Text="Long term medications:"></asp:Label></label>
             </div>
             
                                
    </div>
    <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group customdivbordermultiselect">
 
   <asp:CheckBoxList ID="chkLTMedications" RepeatLayout="Flow" runat="server">
                                                            </asp:CheckBoxList>
                                                        
 </div>
 <div style="display: none;" id="hideOtherLTM">  
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
 <label class="control-label" for="txOtherLongTermMedications">Other current long term medications:</label>
 </div>
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txOtherLongTermMedications" runat="server" Width="88%" CssClass="form-control" TextMode="MultiLine" Style="resize:none;"></asp:TextBox>
 </div>
 </div>
 </div>
                               
                            </asp:Panel>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:Panel ID="pnlPE" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgPE" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblPhysicalExamination" runat="server" Text="Physical Examination"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlPEDetail" runat="server" Style="overflow:hidden;">
                                <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td colspan="2" width="100%">
                                            <UcPhysicalExamination:Uc4 ID="UcPE" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:Panel ID="PnlHReHis" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="ImgRH" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                HIV Related History</h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlTHReHis" runat="server" Style="overflow:hidden;">
                            <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Initial CD4:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Count:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtInitialCD4" runat="server" Width="80%" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Percent:</label>
             </div>    
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="row">
              <div class="col-md-10"><asp:TextBox ID="txtInitialCD4Percent" runat="server" Width="50%" CssClass="form-control"></asp:TextBox></div>
              <div class="col-md-1"><label style="margin-left:-95px;margin-top:5px;">%</label></div>
              </div>

             
              
             </div>
             
              <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Date:</label>
             </div>
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtInitialCD4Date" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                      size="11" onblur="DateFormat(this,this.value,event,false,'3')"/>
                                                </div>
                                            </div>

           
             </div>                  
    </div>
                                <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Highest CD4 ever:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Count:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtHighestCD4Ever" Width="80%" CssClass="form-control" runat="server"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Percent:</label>
             </div>    
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="row">
              <div class="col-md-10">
            <asp:TextBox ID="txtHighestCD4Percent" Width="50%" CssClass="form-control" runat="server"></asp:TextBox>
              </div>
              <div class="col-md-1"><label style="margin-left:-95px;margin-top:5px;">%</label></div>
              </div>            
              
             </div>
             
              <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Date:</label>
             </div>
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtHighestCD4Date" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                      size="11" onblur="DateFormat(this,this.value,event,false,'3')"/>
                                                </div>
                                            </div>

           
             </div>                  
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Most Recent CD4:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Count:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtMostRecentCD4" Width="80%" CssClass="form-control" runat="server"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Percent:</label>
             </div>    
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="row">
              <div class="col-md-10">
            <asp:TextBox ID="txtMostRecentCD4Percent" Width="50%" runat="server" CssClass="form-control"></asp:TextBox>
              </div>
              <div class="col-md-1"><label style="margin-left:-95px;margin-top:5px;">%</label></div>
              </div>            
              
             </div>
             
              <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Date:</label>
             </div>
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtCD4atARTinitiationDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                      size="11" onblur="DateFormat(this,this.value,event,false,'3')"/>
                                                </div>
                                            </div>

           
             </div>                  
    </div>
<div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">CD4 at ART initiation:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Count:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtCD4atARTinitiation" Width="80%" CssClass="form-control" runat="server"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Percent:</label>
             </div>    
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="row">
              <div class="col-md-10">
            <asp:TextBox ID="txtCD4PercentAtARTInitiation" Width="50%" CssClass="form-control" runat="server"></asp:TextBox>
              </div>
              <div class="col-md-1"><label style="margin-left:-95px;margin-top:5px;">%</label></div>
              </div>            
              
             </div>
             
              <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Date:</label>
             </div>
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtMostRecentCD4Date" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                      size="11" onblur="DateFormat(this,this.value,event,false,'3')"/>
                                                </div>
                                            </div>

           
             </div>                  
    </div>  
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Previous viral load:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtPreviousViralLoad" Width="60%" runat="server" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtPreviousViralLoadDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                      size="11" onblur="DateFormat(this,this.value,event,false,'3')"/>
                                                </div>
                                            </div> </div>
             </div>   
      <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" for="txtOtherHIVRelatedHistory">Other HIV related history:</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtOtherHIVRelatedHistory" Width="100%" runat="server" CssClass="form-control" TextMode="MultiLine" Style="resize:none;"></asp:TextBox>
             </div>
                               
    </div>                          
                                
                                
                                
                            
                            </asp:Panel>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:Panel ID="pnlARVExposure" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgARVExposure" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                ARV Exposure</h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlARVExposureDetail" runat="server" Style="overflow:hidden;">
                            <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">PMTCT Regimen:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtPMTCTRegimen" runat="server" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label pull-right">Start Date:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtPMTCTStartDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">PEP Regimen:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtPEP" runat="server" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label  pull-right">Start Date:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtPEP1StartDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">HAART Regimen:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtHAARTRegimen" runat="server" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label  pull-right">Start Date:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtHAART1StartDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Any doses missed:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rblARVdosesmissed" CssClass="checkbox-btn" runat="server" RepeatDirection="Horizontal"
                                                            onclick="rblSelectedValue(this, 'DIVStopINHDate')">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
             </div>
             <div id="DIVStopINHDate" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label pull-right">Delayed doses:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rblARVDelayeddoses" CssClass="checkbox-btn" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                            </asp:RadioButtonList>
             </div>   
             </div>                   
    </div>
    </asp:Panel>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:Panel ID="pnlWHOStage" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgWHOStage" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblheadWHOStage" runat="server" Text="WHO Stage"></asp:Label></h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlWHOStageDetail" runat="server">
                                <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td colspan="2" width="100%">
                                            <UcWhoStaging:Uc5 ID="UcWhostaging" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:Panel ID="pnlDiagnosis" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgDiagnosis" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                Diagnosis</h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlDiagnosisDetail" runat="server" Style="overflow:hidden;">
                            <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Diagnosis and current illness at this visit:</label>
             </div>
                                   
    </div>

                            <div class="row">
                            <div class="col-xs-6">
                            <%--left--%>
                            <div class="row">
                         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                         <div id="divDiagnosis" class="customdivbordermultiselect">
                                                            <asp:CheckBoxList ID="cblDiagnosis" RepeatLayout="Flow" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                         </div>
                         </div>
                            </div>
                            <div class="col-xs-6">
                            <%--right--%>
                             <div class="row" id="DIVHIVrelatedOI" style="display: none;">
                         <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                      <label class="control-label">Specify HIV related OI:</label>
                         </div>
                          <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                          <asp:TextBox ID="txtHIVRelatedOI" runat="server" Skin="MetroTouch" Width="98%" CssClass="form-control"></asp:TextBox>
                         </div>
                         </div>
                          <div class="row" id="DIVNonHIVrelatedOI" style="display: none;">
                         <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                      <label class="control-label">Specify Non HIV related OI:</label>
                         </div>
                          <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                             <asp:TextBox ID="txtNonHIVRelatedOI" runat="server" Skin="MetroTouch" Width="98%" CssClass="form-control"></asp:TextBox>         
                         </div>
                         </div>
                            </div>
                            </div>
                            <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Impression:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtImpression" runat="server" Columns="20" Rows="2" Width="100%" TextMode="MultiLine" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>
                                 
    </div>
                                
                            </asp:Panel>
                        </div>
                        <act:CollapsiblePanelExtender ID="CPELTM" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlLTMedicine" CollapseControlID="pnlLTM"
                            ExpandControlID="pnlLTM" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="ImgLTM" Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEPE" runat="server" SuppressPostBack="True" ExpandedImage="~/images/arrow-dn.gif"
                            TargetControlID="pnlPEDetail" CollapseControlID="pnlPE" ExpandControlID="pnlPE"
                            CollapsedImage="~/images/arrow-up.gif" Collapsed="True" ImageControlID="ImgLTM"
                            Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEHIVRH1" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlTHReHis" CollapseControlID="pnlHReHis"
                            ExpandControlID="pnlHReHis" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="ImgRH" Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPARVExposure" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlARVExposureDetail"
                            CollapseControlID="pnlARVExposure" ExpandControlID="pnlARVExposure" CollapsedImage="~/images/arrow-up.gif"
                            Collapsed="True" ImageControlID="ImgARVExposure" Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPDiagnosis" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlDiagnosisDetail" CollapseControlID="pnlDiagnosis"
                            ExpandControlID="pnlDiagnosis" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="ImgDiagnosis" Enabled="True"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPWHOStage" runat="server" SuppressPostBack="True"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlWHOStageDetail" CollapseControlID="pnlWHOStage"
                            ExpandControlID="pnlWHOStage" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                            ImageControlID="ImgWHOStage" Enabled="True"></act:CollapsiblePanelExtender>
                      
                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureExam" runat="server" />
             </div>
                               
    </div>
    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnSaveExam" runat="server" Text="Save" OnClick="btnSaveExam_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left" />
 <label id="lblSave" runat = "server" class="glyphicon glyphicon-floppy-disk" style="margin-left: -2.5%; vertical-align: sub; color: #fff; margin-right:2%;"></label>
 <asp:Button ID="btncloseExam" Text="Close" runat="server" OnClick="btncloseExam_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left"/>
 <label class="glyphicon glyphicon-remove-circle" style="margin-left:-2.5%; vertical-align: sub; color: #fff; margin-right:2%;"></label>
 <asp:Button ID="btnPrintExam" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left"/>
 <label id="lblPrintAll" runat = "server" class="glyphicon glyphicon-print" style="margin-left: -2.5%; vertical-align: sub; color: #fff;"></label>
 </div>
 </div>
                        
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabpnlManagement" runat="server" HeaderText="Management">
                    <HeaderTemplate>
                        Management
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="border center formbg">
                            <table id="TabManagement" class="center formbg" cellspacing="6" cellpadding="0" width="100%"
                                border="0">
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Panel ID="pnlDAToxities" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgDAToxities" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                Drug Allergies</h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlDAToxitiesDetail" runat="server" Style="overflow:hidden;">
                            <div class="row" align="center">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                           <UcDrugAllergies:Uc6 ID="UCDrugAllergies" runat="server" />
                             </div>                             
                    </div>
                                
                            </asp:Panel>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Panel ID="pnlARVSideeffects" CssClass="border center formbg" runat="server"
                                            Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="ImgARVSideEffect" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                ARV Side Effects</h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlARVSideEffectDetail" runat="server" Style="overflow:hidden;">
                            <div class="row">
                            <div class="col-md-6">
                            <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                            <label class="control-label">Short term effects:</label>
                            </div>                             
                            </div>
                            <div class="row">
                            <div class="col-md-12">
                            <div id="divShortTermEffects" class="customdivbordermultiselect">
                                                            <asp:CheckBoxList ID="cblShortTermEffects" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                        
                            </div>                             
                            </div>
                             <div class="row" id="divshorttermeffecttxt" style="display: none">
                            <div class="col-md-6">                            
                                                            <label>
                                                                Specify other short term effects:</label>
                                                            

                            </div>
                             <div class="col-md-6">
                             <asp:TextBox ID="txtOtherShortTermEffects" runat="server" Skin="MetroTouch" Width="100%" CssClass="form-control"></asp:TextBox>
                             </div>
                            </div>
                            <%--Left--%>
                            </div>
                            <div class="col-md-6">
                            <%--Right--%>
                             <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                            <label class="control-label">Long term effects:</label>
                            </div>                             
                            </div>
                            <div class="row">
                            <div class="col-md-12">
                            <div id="divLongTermEffects" class="customdivbordermultiselect">
                                                            <asp:CheckBoxList ID="cblLongTermEffects" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                       
                            </div>                             
                            </div>
                            <div class="row" id="divlongtermeffecttxt" style="display: none">
                            <div class="col-md-6">
                            <label>Specify other long term effects:</label>
                            </div>
                             <div class="col-md-6">
                             <asp:TextBox ID="txtOtherLongtermEffects" runat="server" Skin="MetroTouch" Width="100%" CssClass="form-control"></asp:TextBox>
                            </div>
                            </div>
                            </div>
                            </div>
                            </asp:Panel>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Panel ID="PnlWupplan" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="Imgwupplan" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                Work up plan</h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="PnlWupplandetails" runat="server" Style="overflow:hidden;">
                            <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Work up plan:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtWorkUpPlan" runat="server" Skin="MetroTouch" Columns="20" Rows="2" Width="99%" TextMode="MultiLine" CssClass="form-control" Style="resize:none;">
                                                        </asp:TextBox>
             </div>
                           
    </div>
                                
                            </asp:Panel>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Panel ID="pnllabEvaluation" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imglabevaluation" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                Lab Evaluation</h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <%--                                     <asp:RadioButtonList ID="radbtnARVSideEffects" runat="server" RepeatDirection="Horizontal"
                                                                onclick="rblSelectedValue(this,'hideARVSideEffect');">
                                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                            </asp:RadioButtonList>--%>
                            <asp:Panel ID="pnllabEvaluationDetail" runat="server" Style="overflow:hidden;">
                            <div class="row" align="center">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                         <uc11:Uclabeval ID="UcLabEval" runat="server" />
                         </div>                           
                        </div>
                                <table width="100%" class="formbg" cellspacing="6" cellpadding="0" border="0">
                                    <tr>
                                        <td class="border pad5 whitebg">
                                            
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Panel ID="pnlTreatment" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgtreatment" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                <asp:Label ID="lblheadregimenpresc" runat="server" Text="Treatment"></asp:Label>
                                            </h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlTreatmentdetail" runat="server"  Style="overflow:hidden;">
                            <div class="row" align="center">
                             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                             <UcPharmacy:Uc7 ID="UCPharmacy" runat="server" />
                            </div>
                            </div>
                                
                            </asp:Panel>
                            <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Panel ID="pnlOITreatment" CssClass="border center formbg" runat="server" Style="padding: 6px">
                                            <h5 class="forms" align="left">
                                                <asp:ImageButton ID="imgOITreatment" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                OI Treatment
                                            </h5>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlOITreatmentDetail" runat="server" Style="overflow:hidden;">
                            <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">OI Prophylaxis:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="rcbOIProphylaxis" runat="server" AutoPostBack="false" Width="250px" CssClass="form-control"></asp:DropDownList>
             </div>
             <div id="DIVCotrimoxazole" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Cotrimoxazole prescribed for:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="rcbReasonCTXPrescribed" runat="server" AutoPostBack="false" CssClass="form-control" Width="250px"></asp:DropDownList>
             </div>
             </div>
             <div id="divFluconazoleshowhide" style="display: none;">
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Fluconazole prescribed for:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlfluconazole" CssClass="form-control" Width="250px"></asp:DropDownList>
             </div>
             </div>
             <div id="divOtherOIPropholyxis" style="display: none;">
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="lblOther (Specify)-8888578" align="center">Other (Specify):</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtOtherOIPropholyxis" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>
             </div>
             </div>
                                   
    </div>
                                <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other treatment:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtOtherTreatment" runat="server" Skin="MetroTouch" Width="99%" TextMode="MultiLine" CssClass="form-control" style="resize:none;"></asp:TextBox>
             </div>
                                  
    </div>
                                
                            </asp:Panel>
                        </div>
                        <act:CollapsiblePanelExtender ID="CPLWupplan" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="PnlWupplandetails" CollapseControlID="PnlWupplan"
                            ExpandControlID="PnlWupplan" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                            ImageControlID="ImgWupplan"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEDAT" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlDAToxitiesDetail" CollapseControlID="pnlDAToxities"
                            ExpandControlID="pnlDAToxities" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                            ImageControlID="imgDAToxities"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEARVSE" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlARVSideEffectDetail"
                            CollapseControlID="pnlARVSideeffects" ExpandControlID="pnlARVSideeffects" CollapsedImage="~/images/arrow-up.gif"
                            Collapsed="true" ImageControlID="ImgARVSideEffect"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPELabEval" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnllabEvaluationDetail"
                            CollapseControlID="pnllabEvaluation" ExpandControlID="pnllabEvaluation" CollapsedImage="~/images/arrow-up.gif"
                            Collapsed="true" ImageControlID="imglabevaluation"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPETreatment" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlTreatmentdetail" CollapseControlID="pnlTreatment"
                            ExpandControlID="pnlTreatment" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                            ImageControlID="imgtreatment"></act:CollapsiblePanelExtender>
                        <act:CollapsiblePanelExtender ID="CPEOITreatment" runat="server" SuppressPostBack="true"
                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlOITreatmentDetail"
                            CollapseControlID="pnlOITreatment" ExpandControlID="pnlOITreatment" CollapsedImage="~/images/arrow-up.gif"
                            Collapsed="true" ImageControlID="imgOITreatment"></act:CollapsiblePanelExtender>
                        
                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <uc12:UserControlKNH_Signature ID="UserControlKNH_SignatureMgt" runat="server" />
             </div>
                              
    </div>
    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnSaveManagement" runat="server" Text="Save" OnClick="btnSaveManagement_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left"/>
 <label id="Label1" runat = "server" class="glyphicon glyphicon-floppy-disk" style="margin-left: -2.5%; vertical-align: sub; color: #fff; margin-right:2%;"></label>
 <asp:Button ID="btncloseMgt" Text="Close" runat="server" OnClick="btncloseMgt_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left" />
 <label class="glyphicon glyphicon-remove-circle" style="margin-left:-2.5%; vertical-align: sub; color: #fff; margin-right:2%;"></label>
 <asp:Button ID="btnPrintManagement" Text="Print" OnClientClick="WindowPrint()" runat="server"  CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left"/>
 <label id="Label2" runat = "server" class="glyphicon glyphicon-print" style="margin-left: -2.5%; vertical-align: sub; color: #fff;"></label>
 </div>
 </div>
                        
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabpnlPrev" runat="server"  HeaderText="Prevention with Positives">
                    <HeaderTemplate>
                        Prevention with Positives
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="border center formbg">
                            <table class="center formbg" width="100%" border="0">
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
        <div class="row text-center" style="display: none">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group" >
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" Height="26px" />
        <asp:Button ID="btncomplete" runat="server" Text="Data Quality Check" CssClass="btn btn-primary" Height="26px" />                               
        <asp:Button ID="btnPrint" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Height="26px" />
        </div>
        </div>
        <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<uc1:UserControlKNH_BackToTop ID="UserControlKNH_BackToTop1" runat="server" />
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
