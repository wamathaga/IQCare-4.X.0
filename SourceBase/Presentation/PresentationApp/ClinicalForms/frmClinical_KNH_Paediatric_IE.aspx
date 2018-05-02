<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    CodeBehind="frmClinical_KNH_Paediatric_IE.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_KNH_Paediatric_IE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="UcVitalSign" TagName="Uc1" Src="~/ClinicalForms/UserControl/UserControlKNH_VitalSigns.ascx" %>
<%@ Register TagPrefix="UcMedicalHistory" TagName="Uc2" Src="~/ClinicalForms/UserControl/UserControlKNH_MedicalHistory.ascx" %>
<%@ Register TagPrefix="UcPresentingComplaints" TagName="Uc3" Src="~/ClinicalForms/UserControl/UserControlKNHPresentingComplaints.ascx" %>
<%@ Register TagPrefix="UcPhysicalExamination" TagName="Uc4" Src="~/ClinicalForms/UserControl/UserControlKNH_PhysicalExamination.ascx" %>
<%@ Register TagPrefix="UcWhoStaging" TagName="Uc5" Src="~/ClinicalForms/UserControl/UserControlKNH_WHOStaging.ascx" %>
<%--Nidhi--%>
<%--<%@ Register TagPrefix="uc3" TagName="UserControlKNH_Extruder" Src="UserControl/UserControlKNH_Extruder.ascx" %>--%>
<%@ Register TagPrefix="UcTBScreening" TagName="Uc8" Src="~/ClinicalForms/UserControl/UserControlKNH_TBScreening.ascx" %>
<%@ Register TagPrefix="UcDrugAllergy" TagName="Uc6" Src="~/ClinicalForms/UserControl/UserControlKNH_DrugAllergies.ascx" %>
<%@ Register TagPrefix="UcLabEvalution" TagName="Uc7" Src="~/ClinicalForms/UserControl/UserControlKNH_LabEvaluation.ascx" %>
<%@ Register TagPrefix="UcPharmacy" TagName="Uc9" Src="~/ClinicalForms/UserControl/UserControlKNH_Pharmacy.ascx" %>
<%@ Register TagPrefix="UcPwP" TagName="Uc10" Src="~/ClinicalForms/UserControl/UserControlKNH_PwP.ascx" %>
<%@ Register Src="UserControl/UserControlKNH_Signature.ascx" TagName="UserControlKNHSignature"
    TagPrefix="UCSignature" %>
<%@ Register Src="UserControl/UserControlKNH_BackToTop.ascx" TagName="UserControlKNH_BackToTop"
    TagPrefix="uc12" %>
<%--Nidhi--%>
<%--<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>--%>

<asp:Content ID="content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">    
  
        <script language="javascript" type="text/javascript">            buildWeeklyCalendar(0);</script>
        <script type="text/javascript" language="javascript">
            //            $(function () {
            //                $("#tabs").tabs();
            //            });
            function ShowHide(theDiv, YN) {
                if (YN == "show") {
                    $("#" + theDiv).slideDown();

                }
                if (YN == "hide") {
                    $("#" + theDiv).slideUp();

                }
            }
            function rblSelectedValue1(val, divID, txtControlId) {
                var selectedvalue = $("#" + val.id + " input:radio:checked").val();
                var YN = "";
                if (selectedvalue == "1") {
                    YN = "show";
                }
                else {

                    document.getElementById(txtControlId).value = '';
                    YN = "hide";
                }
                ShowHide(divID, YN);
            }
            function rblSelectedValueShowHide(val, divID, txtControlId) {
                var selectedvalue = $("#" + val.id + " input:radio:checked").val();
                var YN = "";
                if (selectedvalue == "1") {
                    YN = "hide";
                }
                else {

                    document.getElementById(txtControlId).value = '';
                    YN = "show";
                }
                ShowHide(divID, YN);
            }
            function ShowMore(sender, eventArgs) {
                var substr = eventArgs._commandArgument.toString().split('|')
                ShowHide(substr[0], substr[1]);
            }
            function rblSelectedValueFordpList(val, divID, txtControlId) {
                var YN = "";
                var selectedvalue = $("#" + val.id + " input:radio:checked").val();
                if (selectedvalue == "1") {
                    YN = "show";
                }
                else {

                    document.getElementById(txtControlId).value = '0';
                    YN = "hide";
                }
                ShowHide(divID, YN);
            }

            function CheckBoxToggleShowHide(val, divID, txt, txtControlId) {

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
                            document.getElementById(txtControlId).value = '';
                            ShowHide(divID, "hide");
                        }
                    }
                }
            }

            function CheckBoxToggleShowHide1(val, divID, txt) {

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

            function fnControlUnableDesable(val, txt, dd) {
                var checkList = document.getElementById(val);
                var checkBoxList = checkList.getElementsByTagName("input");
                var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
                var checkBoxSelectedItems = new Array();

                for (var i = 0; i < checkBoxList.length; i++) {

                    if (checkBoxList[i].checked) {
                        if (arrayOfCheckBoxLabels[i].innerText == txt) {


                            $("#" + dd + " option:contains('Fluconazole')").attr('disabled', true);
                            $("#" + dd + " option:contains('CTX and Fluconazol')").attr('disabled', true);
                        }

                    }
                    else {
                        if (arrayOfCheckBoxLabels[i].innerText == txt) {

                            $("#" + dd + " option:contains('Fluconazole')").attr('disabled', false);
                            $("#" + dd + " option:contains('CTX and Fluconazol')").attr('disabled', false);
                        }
                    }
                }
            }


            function dropdownchange(ddl, theDiv, txtControlId) {

                if (ddl.selectedIndex == "1") {
                    ShowHide(theDiv, "show");
                }
                else {
                    document.getElementById(txtControlId).value = '0';
                    ShowHide(theDiv, "hide");
                }
            }

            function dropdownchangetext(ddl, theDiv, txt, txtControlId) {
                var e = document.getElementById(ddl);
                var strtxt = e.options[e.selectedIndex].innerText;
                if (strtxt == txt) {
                    ShowHide(theDiv, "show");

                }
                else {
                    document.getElementById(txtControlId).value = '';
                    ShowHide(theDiv, "hide");

                }
            }

            function fnReset(ctrlid, ctrltype) {
                if (ctrltype == "dd") {
                    document.getElementById(ctrlid).selectedIndex = 0;
                }
                else if (ctrltype == "txt") {
                    document.getElementById(ctrlid).value = "";
                }


            }

            function dropdownFluconazole(ddl, theDiv, txt) {
                var e = document.getElementById(ddl);
                var strtxt = e.options[e.selectedIndex].innerText;
                if (strtxt == 'Fluconazole' || strtxt == 'CTX and Fluconazol') {
                    ShowHide(theDiv, "show");

                }
                else {
                    ShowHide(theDiv, "hide");

                }
            }

            function fnalertTreatment(ddl) {
                if (document.getElementById('<%=hidupdate.ClientID %>').value == '1') {
                    var e = document.getElementById(ddl);
                    var strtxt = e.options[e.selectedIndex].innerText;
                    if (strtxt != 'Select') {
                        alert('you are changed the treatment');
                    }
                }
            }

            function fnalertregimen(ddl) {
                if (document.getElementById('<%=hidregimen.ClientID %>').value == '1') {
                    var e = document.getElementById(ddl);
                    var strtxt = e.options[e.selectedIndex].innerText;
                    if (strtxt != 'Select') {
                        alert('you are changed the regimen');
                    }
                }
            }
        </script>
        <script type="text/javascript">
            function RegisterJQuery() {
                $('#txtVisitDate').datepicker({ autoclose: true });
                $('#txtdateofhivdiagnosis').datepicker({ autoclose: true });
                $('#txtdateoffatherdeath').datepicker({ autoclose: true });
                $('#txtdateofmotherdeath').datepicker({ autoclose: true });
                $('#txtcurrentartregimendate').datepicker({ autoclose: true });
                $('#txtadmissionstart').datepicker({ autoclose: true });
                $('#txtadmissionend').datepicker({ autoclose: true });
                $('#txtcompletetxdate').datepicker({ autoclose: true });
                $('#txtretreatmentdate').datepicker({ autoclose: true });
                $('#txtpmtctdate').datepicker({ autoclose: true });
                $('#txtpepstartdate').datepicker({ autoclose: true });
                $('#txthaartstartdate').datepicker({ autoclose: true });
                $('#txtcd4date').datepicker({ autoclose: true });
                $('#txthigcd4everdate').datepicker({ autoclose: true });
                $('#txtmostrecentcd4date').datepicker({ autoclose: true });
                $('#txtcd4artinitdate').datepicker({ autoclose: true });
                $('#txtpreviousviralloaddate').datepicker({ autoclose: true });
                $('#dtAntifungalsDate').datepicker({ autoclose: true });
                $('#dtAntihypertensivesDate').datepicker({ autoclose: true });
                $('#dtSulfaTMPDate').datepicker({ autoclose: true });
                $('#dtOtherCurrentLongTermMedications').datepicker({ autoclose: true });
                
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
        <h3 class="box-title">
            <asp:Label ID="lblHeading" runat="server" Text="Paediatric Initial Evaluation"></asp:Label>	</h3>
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
                                                    <input type="text" class="form-control pull-left" id="txtVisitDate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
                                            </div>
                                  
             </div>
             <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
        <asp:TabContainer ID="tabControl" runat="server" ActiveTabIndex="0" Width="100%" CssClass="ajax__myTab">
                        <asp:TabPanel ID="TabTriage" runat="server" HeaderText="Triage">
                            <HeaderTemplate>
                                Triage
                            </HeaderTemplate>
                            <ContentTemplate>
                            <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlHIVCare" runat="server">
          <h5 class="forms" align="left">
          <asp:ImageButton ID="imgHIVCare" ImageUrl="~/images/arrow-up.gif" runat="server" />
           Client Information
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="pnlHivCareDetail" runat="server" Style="overflow:hidden;">
        <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Child accompanied by:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtchildaccompaniedby" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Child primary caregiver:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtchildcaregiver" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Child diagnosis confirmed:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlchilddiagnosis" runat="server" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date of HIV Diagnosis:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtdateofhivdiagnosis" clientidmode="Static" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'" 
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Disclosure status:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddldisclosurestatus" onchange="dropdownchangetext('ctl00_IQCareContentPlaceHolder_tabControl_TabTriage_ddldisclosurestatus','divreasonnotdisclosed','Not ready','ctl00_IQCareContentPlaceHolder_tabControl_TabTriage_txtspecifyreason');dropdownchangetext('ctl00_IQCareContentPlaceHolder_tabControl_TabTriage_ddldisclosurestatus','divotherdisclosurestatus','Other specify','ctl00_IQCareContentPlaceHolder_tabControl_TabTriage_txtspecifyotherdisclosurestatus');"
                                                                            runat="server" Width="98%" CssClass="form-control">
                                                                        </asp:DropDownList>
             </div> 
             <div id="divreasonnotdisclosed" style="display: none;">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Specify reason not disclosed:</label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtspecifyreason" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
             <div id="divotherdisclosurestatus" style="display: none;">
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify other disclosure status:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtspecifyotherdisclosurestatus" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
             </div>
 </div>
 <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Father alive: </label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rbListFatherAlive" runat="server" RepeatDirection="Horizontal"
                                                                            OnClick="rblSelectedValueShowHide(this,'divdatefatherdeath','txtdateoffatherdeath')">
                                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                        </asp:RadioButtonList>
             </div>
             <div id="divdatefatherdeath" style="display: none;">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date of father's death:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtdateoffatherdeath" clientidmode="Static" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                                onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div> 
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Mother alive: </label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <asp:RadioButtonList ID="rbListMotherAlive" runat="server" RepeatDirection="Horizontal"
                                                                            OnClick="rblSelectedValueShowHide(this,'divdateofmotherdeath','txtdateofmotherdeath')">
                                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                        </asp:RadioButtonList>
             </div>
              <div id="divdateofmotherdeath" style="display: none;">
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date of mother's death:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtdateofmotherdeath" clientidmode="Static" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                                onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>
              </div>
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Have you been referred from another facility:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rbListRefFacility" runat="server" RepeatDirection="Horizontal"
                                                                            OnClick="rblSelectedValue1(this,'divspecifyfacility','ctl00_IQCareContentPlaceHolder_tabControl_TabTriage_txtspecifyfacility')">
                                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                        </asp:RadioButtonList>
             </div>
             <div id="divspecifyfacility" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify facility referred from:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
           <asp:TextBox ID="txtspecifyfacility" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Is the child on ART:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="rbListChildART" runat="server" RepeatDirection="Horizontal"
                                                                                        OnClick="rblSelectedValue1(this,'divARTDate','txtcurrentartregimendate'); rblSelectedValueFordpList(this,'divcurrentregimenline','ctl00_IQCareContentPlaceHolder_tabControl_TabTriage_ddlcurrentartregimen');rblSelectedValueFordpList(this,'divARTRegimen','ctl00_IQCareContentPlaceHolder_tabControl_TabTriage_ddlcurrentregimenline');">
                                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                                    </asp:RadioButtonList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Is the child on CTX:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:RadioButtonList ID="rbListChildCTX" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                        </asp:RadioButtonList>
             </div> 
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Health education given:</label>
             </div>
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rbListHealthEducation" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                        </asp:RadioButtonList>
             </div>
 </div>

 <div class="row">
 <div id="divcurrentregimenline" style="display: none;">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Current regimen line:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlcurrentregimenline" runat="server" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div>
             </div>
              <div id="divARTRegimen" style="display: none;">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">ART regimen:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlcurrentartregimen" runat="server" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
              <div id="divARTDate" style="display: none;">
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtcurrentartregimendate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                                                    onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
             </div>
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Schooling Status:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlschoolingstatus" onchange="dropdownchange(this,'divhighestlevel','ctl00_IQCareContentPlaceHolder_tabControl_TabTriage_ddlhighestlevelattained');"
                                                                                runat="server" Width="98%" CssClass="form-control">
                                                                            </asp:DropDownList>
             </div>
             <div id="divhighestlevel" style="display: none;">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Highest level attained:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlhighestlevelattained" runat="server" Width="98%" CssClass="form-control"> </asp:DropDownList>
             </div> 
             </div>
 </div>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Is client a member of a support group:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rbListSupportGroup" runat="server" RepeatDirection="Horizontal"
                                                                            OnClick="rblSelectedValue1(this,'divsupportgroup','ctl00_IQCareContentPlaceHolder_tabControl_TabTriage_txthivsupportgroupmembership')">
                                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                        </asp:RadioButtonList>
             </div>
             <div id="divsupportgroup" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">HIV support group membership:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txthivsupportgroupmembership" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
            </div>  
 </div>

                                            
                                        </asp:Panel>
<div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Panel ID="pnlvitalsign" runat="server">

 <h5 class="forms" align="left">
          <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/arrow-up.gif" runat="server" />
          <asp:Literal ID="literalVitalSign" Text="Vital Signs" runat="server"></asp:Literal>
          </h5>

                                                                    
                                                                </asp:Panel>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           <asp:Panel ID="pnlVitalSignDetail" runat="server" Style="overflow:hidden;">
                                                         <UcVitalSign:Uc1 ID="idVitalSign" runat="server" />
                                                    </asp:Panel>
             </div>
            
 </div>
 
                                <div class="border center formbg">
                                  
                                    <div class="center formbg">
                                        <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td colspan="2" class="border leftallign formbg">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <asp:CollapsiblePanelExtender ID="CPEHIVCare" runat="server" SuppressPostBack="True"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlHivCareDetail" CollapseControlID="pnlHIVCare"
                                    ExpandControlID="pnlHIVCare" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                    ImageControlID="imgHIVCare" Enabled="True"></asp:CollapsiblePanelExtender>
                                <asp:CollapsiblePanelExtender ID="CPEVitalSign" runat="server" SuppressPostBack="True"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlVitalSignDetail" CollapseControlID="pnlVitalSign"
                                    ExpandControlID="pnlVitalSign" CollapsedImage="~/images/arrow-up.gif" Collapsed="True"
                                    ImageControlID="imgVitalSign" Enabled="True"></asp:CollapsiblePanelExtender>
                              <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <UCSignature:UserControlKNHSignature ID="UserControlKNHSignature_Triage" runat="server" />
             </div>
            
 </div>
 <div class="row" align="center" id="tblSaveButton">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
              <asp:Button ID="btnSaveTriage" runat="server" Text="Save" OnClick="btnSaveTriage_Click"
                                                        CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
<label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                                    <asp:Button ID="btnCloseTriage" runat="server" Text="Close" OnClick="btnCloseTriage_Click"
                                                        CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
<label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
                                                    <asp:Button ID="btnPrintTriage" Text="Print" OnClientClick="WindowPrint()" runat="server"
                                                        CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
<label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
             </div>
            
 </div>
 <div class="row" id="tblDeleteButton" style="display: none" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
               <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete"
                                                        CssClass="btn btn-primary" Height="26px" />
             </div>
            
 </div>
                                
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabClinicalHistory" runat="server" HeaderText="Clinical History">
                            <HeaderTemplate>
                                Clinical History
                            </HeaderTemplate>
                            <ContentTemplate>
                             <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlpresenticomplains" runat="server">
          <h5 class="forms" align="left">
          <asp:ImageButton ID="imgpresenticomplains" ImageUrl="~/images/arrow-up.gif" runat="server" />
          <asp:Literal ID="literPresenting" Text="Presenting Complaints" runat="server"></asp:Literal>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
          <div class="row">
        <div class="col-md-12">
        <asp:Panel ID="pnltargetpresentingcomplain" runat="server" style="overflow:hidden;">
        <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
        <UcPresentingComplaints:Uc3 ID="UcPc" runat="server" />
        </div>
        </div>
        <div class="row">
        <div id="divschoolperformance">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">If schooling,current school perfomance:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlschoolperfomance" runat="server" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div>
             </div>
             <div style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Presenting complaints additional notes:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtAdditionPresentingComplaints" runat="server" TextMode="MultiLine" Width="98%" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
 </div>
                                                        
                                                    </asp:Panel>
        </div>
        </div>

        <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="headermedicalhistory" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="imgheadermedicalhistory" ImageUrl="~/images/arrow-up.gif" runat="server" />
         <asp:Literal ID="literalMedicalHistory" Text="Medical History (Disease, Diagnosis and Treatment)" runat="server"></asp:Literal>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="pnlmadicalhistory" runat="server" style="overflow:hidden;">
        <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify Medical history:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" ID="txtspecifymedicalhistory" TextMode="MultiLine" Width="98%" CssClass="form-control" style="resize:none;"></asp:TextBox>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required"><asp:Label ID="lblHeadingChronic" Text="Chronic condition:" runat="server"></asp:Label></label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <div class="customdivbordermultiselect">
                                                                        <asp:CheckBoxList ID="cblchroniccondition" RepeatLayout="Flow" onclick="CheckBoxToggleShowHide('ctl00_IQCareContentPlaceHolder_tabControl_TabClinicalHistory_cblchroniccondition','hideOtherLTM','Other specify','ctl00_IQCareContentPlaceHolder_tabControl_TabClinicalHistory_txtotherchroniccondition');"
                                                                            Width="190px" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
             </div>
             <div style="display: none;" id="hideOtherLTM">
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify other chronic condition:</label>
             </div>
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:TextBox runat="server" ID="txtotherchroniccondition" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
             </div>
              
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Previously admitted:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
              <asp:RadioButtonList ID="rbListPreviouslyAdmitt" runat="server" RepeatDirection="Horizontal"
                                                                        OnClick="rblSelectedValue1(this,'divdiagnosis','ctl00_IQCareContentPlaceHolder_tabControl_TabClinicalHistory_txtdiagnosis');rblSelectedValue1(this,'divadmissionstart','txtadmissionstart');rblSelectedValue1(this,'divadmissionend','txtadmissionend');">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
             </div>
            
 </div>
 <div class="row">
  <div id="divdiagnosis" style="display: none;">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Diagnosis:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:TextBox runat="server" ID="txtdiagnosis" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
             </div>
             <div id="divadmissionstart" style="display: none;">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Admission start:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtadmissionstart" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
             </div>
              <div id="divadmissionend" style="display: none;">
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Admission end:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtadmissionend" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
             </div>
 </div>
 <div class="row" style="display: none;">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Medical history:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
              <input id="rdomedicalhistoryyes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                                                        type="radio" name="medicalhistoryyes" runat="server" />
                                                                    <label>
                                                                        Yes</label>
                                                                    <input id="rdomedicalhistoryno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                                                        type="radio" name="medicalhistoryyes" runat="server" />
                                                                    <label>
                                                                        No</label>
             </div>
            
 </div>
                                            
                                        </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlheadertbhistory" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/arrow-up.gif" runat="server" />
          TB History
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="pnltbhistory" runat="server" style="overflow:hidden;">
        <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">TB History:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rbListTBHistory" runat="server" RepeatDirection="Horizontal"
                                                                            OnClick="rblSelectedValue1(this,'divcompletetxdate','txtcompletetxdate');rblSelectedValue1(this,'divretreatmentdate','txtretreatmentdate')">
                                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                        </asp:RadioButtonList>
             </div>
              <div id="divcompletetxdate" style="display: none;">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Complete TX Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtcompletetxdate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
             </div>
             <div id="divretreatmentdate" style="display: none;">
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Retreatment Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtretreatmentdate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
             </div>
 </div>
 <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
        <h5 class="forms" align="left">         
          Immunisation Status
          </h5>
          </div>
        </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label"> Immunisation Status:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlimmunisationstatus" runat="server" CssClass="form-control" Width="98%"></asp:DropDownList>
             </div>
           
 </div>
                                            
                                        </asp:Panel>
        
        <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlheaderarvhistory" runat="server">
          <h5 class="forms" align="left">
        <asp:ImageButton ID="imgpnlheaderarvhistory" ImageUrl="~/images/arrow-up.gif" runat="server" />
        ARV Exposure
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="pnlarvhistory" runat="server" style="overflow:hidden;">
         <div id="divarvexposure">
         <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">PMTCT Regimen:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" ID="txtpmtctregimen" CssClass="form-control" Width="98%"></asp:TextBox>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Start Date:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtpmtctdate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group  text-right">
             <label class="control-label">PEP Regimen:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:TextBox runat="server" ID="txtpepregimen" CssClass="form-control" Width="98%"></asp:TextBox>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Start Date:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtpepstartdate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">HAART Regimen:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" ID="txthaartregimen" CssClass="form-control" Width="98%"></asp:TextBox>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Start Date:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txthaartstartdate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
 </div>
         </div>

                                            <table id="Table3" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                
                                                <tr style="display: none;">
                                                    <td class="border pad6 whitebg" width="100%" colspan="2" align="center">
                                                        <table width="100%;">
                                                            <tr align="left" class="whitebg border pad6">
                                                                <td>
                                                                    <label id="Label37" style="padding-left: 1%;" for="District">
                                                                        HIV related history:</label>
                                                                    <asp:DropDownList ID="ddlhivhistory" onchange="dropdownchangetext('ctl00_IQCareContentPlaceHolder_tabControl_TabClinicalHistory_ddlhivhistory','divhivrelatedhistory','Yes');"
                                                                        runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%;">
                                                                    <div id="divhivrelatedhistory" style="display: none;">
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlHeaderHIV" runat="server">
          <h5 class="forms" align="left">
        <asp:ImageButton ID="imgHIVHistory" ImageUrl="~/images/arrow-up.gif" runat="server" />
          HIV Related History
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="Panel1" runat="server" style="overflow:hidden;">
        <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Initial CD4:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Count:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            <asp:TextBox runat="server" Width="98%" CssClass="form-control" ID="txtcd4"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">Percent:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
              <div class="row">
 <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" Width="98%" ID="txtcd4per" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group" style="margin-left:-30px;">
             %
             </div>
             </div>
            
             </div> 
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">Date:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtcd4date" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Highest CD4 ever:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Count:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            <asp:TextBox runat="server" Width="98%" CssClass="form-control" ID="txthighCD4ever"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">Percent:</label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
              <div class="row">
 <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" Width="98%" CssClass="form-control" ID="txthighestcd4everper"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group" style="margin-left:-30px;">
             %
             </div>
             </div>
            
             </div>
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">Date:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txthigcd4everdate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Most recent CD4:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Count:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" ID="txtmostrecent_cd4" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">Percent:</label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
              <div class="row">
 <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" Width="98%" CssClass="form-control" ID="txtmostrecentcd4per"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group" style="margin-left:-30px;">
             %
             </div>
             </div>
            
             </div>
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">Date:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtmostrecentcd4date" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">CD4 at ART initiation:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Count:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" Width="98%" CssClass="form-control" ID="txtcd4artinitiation"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">Percent:</label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
              <div class="row">
 <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" Width="98%" CssClass="form-control" ID="txtcd4artinitper"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group" style="margin-left:-30px;">
             %
             </div>
             </div>
            
             </div>
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">Date:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtcd4artinitdate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Previous viral load:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:TextBox runat="server" Width="98%" CssClass="form-control" ID="txtpreviousviral_load"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtpreviousviralloaddate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other HIV related history:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
              <asp:TextBox runat="server" ID="txtotherhivrelated_history"  Width="98%" CssClass="form-control" TextMode="MultiLine" style="resize:none;"></asp:TextBox>
             </div>
             
 </div>



                                            
                                        </asp:Panel>
                                <asp:CollapsiblePanelExtender ID="cpeprecomplain" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnltargetpresentingcomplain"
                                    CollapseControlID="pnlpresenticomplains" ExpandControlID="pnlpresenticomplains"
                                    CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="imgpresenticomplains">
                                </asp:CollapsiblePanelExtender>
                                <asp:CollapsiblePanelExtender ID="cpemedicalhistory" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlmadicalhistory" CollapseControlID="headermedicalhistory"
                                    ExpandControlID="headermedicalhistory" CollapsedImage="~/images/arrow-up.gif"
                                    Collapsed="true" ImageControlID="imgheadermedicalhistory"></asp:CollapsiblePanelExtender>
                                <asp:CollapsiblePanelExtender ID="cpetbhistory" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnltbhistory" CollapseControlID="pnlheadertbhistory"
                                    ExpandControlID="pnlheadertbhistory" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                                    ImageControlID="imgpnlheadertbhistory"></asp:CollapsiblePanelExtender>
                                <asp:CollapsiblePanelExtender ID="cpearvhistory" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlarvhistory" CollapseControlID="pnlheaderarvhistory"
                                    ExpandControlID="pnlheaderarvhistory" CollapsedImage="~/images/arrow-up.gif"
                                    Collapsed="true" ImageControlID="imgpnlheaderarvhistory"></asp:CollapsiblePanelExtender>
                                <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="Panel1" CollapseControlID="pnlHeaderHIV"
                                    ExpandControlID="pnlHeaderHIV" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                                    ImageControlID="imgHIVHistory"></asp:CollapsiblePanelExtender>
                               <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <UCSignature:UserControlKNHSignature ID="UserControlKNHSignature_ClinicalHistory"
                                                        runat="server" />
             </div>
           
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <asp:Button ID="btnSaveCHistory" runat="server" Text="Save" OnClick="btnSaveCHistory_Click"
                                                        CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
<label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                                    <asp:Button ID="btnCloseHistory" runat="server" Text="Close" OnClick="btnCloseHistory_Click"
                                                        CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
<label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 2%;"></label>
                                                    <asp:Button ID="btnPrintCHistory" Text="Print" OnClientClick="WindowPrint()" runat="server"
                                                       CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
<label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
             </div>
           
 </div>
                                <div class="border center formbg">
                                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="form" align="center">
                                                    
                                                </td>
                                            </tr>
                                            <tr align="center" valign="top">
                                                <td class="form">
                                                    
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabTBSCreening" runat="server" HeaderText="TB Screening">
                            <HeaderTemplate>
                                TB Screening
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div class="border center formbg">
                                    <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                        <tr valign="top">
                                            <td colspan="2" width="100%">
                                                <asp:Panel ID="Panel3" runat="server">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <UcTBScreening:Uc8 ID="UcTBScreening" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <%--  <div class="border center formbg">
                                            <div class="center formbg">
                                                <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td colspan="2" class="border center formbg">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="pnlheadertbscreening" runat="server">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="ImgTBScreening" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <h4 class="forms" align="left">
                                                                                            TB Screening ICF(2 signs & 2 symptoms - TB likely)
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
                                                <asp:Panel ID="pnltbscreening" runat="server">
                                                    <table id="Table4" class="center formbg" cellspacing="6" cellpadding="0" width="100%"
                                                        border="0">
                                                        <tr>
                                                            <td class="border pad6 whitebg" width="100%" colspan="2" align="center">
                                                                <div id="divtbassessmentshowhide">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 40%" align="right">
                                                                                                <label>
                                                                                                    TB Findings :</label>
                                                                                            </td>
                                                                                            <td style="width: 60%" align="left">
                                                                                                <asp:DropDownList ID="ddltbfinding" runat="server">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 40%" align="right">
                                                                                                <label>
                                                                                                    Sputum smear :</label>
                                                                                            </td>
                                                                                            <td style="width: 60%" align="left">
                                                                                                <asp:DropDownList ID="ddlsputum_smear" runat="server">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 40%" align="right">
                                                                                                <label>
                                                                                                    Tissue Biopsy :</label>
                                                                                            </td>
                                                                                            <td style="width: 60%" align="left">
                                                                                                <asp:DropDownList ID="ddltissuebiopsy" runat="server">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 40%" align="right">
                                                                                                <label>
                                                                                                    Chest X ray results :</label>
                                                                                            </td>
                                                                                            <td style="width: 60%" align="left">
                                                                                                <asp:DropDownList ID="ddlchestxrayresults" runat="server">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 40%" align="right">
                                                                                                <label>
                                                                                                    Other CXR (specify) :</label>
                                                                                            </td>
                                                                                            <td style="width: 60%" align="left">
                                                                                                <asp:TextBox runat="server" ID="txtothercxrspecify"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 40%" align="right">
                                                                                                <label>
                                                                                                    Tissue Biopsy results :</label>
                                                                                            </td>
                                                                                            <td style="width: 60%" align="left">
                                                                                                <asp:TextBox runat="server" ID="txttissuebiopsyresults"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="border pad6 whitebg" width="100%" colspan="2" align="left">
                                                                                <label id="Label54" style="padding-left: 2%;" for="District">
                                                                                    TB Assessment :</label>
                                                                                <div class="checkbox" id="divtbassessment" nowrap="noWrap">
                                                                                    <asp:CheckBoxList ID="cbltbassessment" RepeatLayout="Flow" runat="server">
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                            <div class="center formbg">
                                                <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td colspan="2" class="border center formbg">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="pnlheadertbevaluationtp" runat="server">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="imgtbevaluation" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <h4 class="forms" align="left">
                                                                                            TB Evaluation and Treatment Plan
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
                                                <asp:Panel ID="pnltbevaluation" runat="server">
                                                    <table id="Table5" class="center formbg" cellspacing="6" cellpadding="0" width="100%"
                                                        border="0">
                                                        <tr>
                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 40%" align="right">
                                                                                <label>
                                                                                    TB Type :</label>
                                                                            </td>
                                                                            <td style="width: 60%" align="left">
                                                                                <asp:DropDownList ID="ddltbtype" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 40%" align="right">
                                                                                <label>
                                                                                    Patient type :</label>
                                                                            </td>
                                                                            <td style="width: 60%" align="left">
                                                                                <asp:DropDownList ID="ddlpatienttype" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                                <table width="100%" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 20%" align="right">
                                                                                <label>
                                                                                    TB plan :</label>
                                                                            </td>
                                                                            <td style="width: 32%" align="left">
                                                                                <asp:DropDownList ID="ddltbplan" onchange="dropdownchangetext('ctl00_IQCareContentPlaceHolder_tabControl_TabTBSCreening_ddltbplan','divtbplanshowhine','Other (Specify)');"
                                                                                    runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <div id="divtbplanshowhine" style="display: none;">
                                                                                    <table width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 38%" align="right">
                                                                                                    <label>
                                                                                                        Specify Other TB plan :</label>
                                                                                                </td>
                                                                                                <td style="width: 60%" align="left">
                                                                                                    <asp:TextBox runat="server" ID="txtothertbplan"></asp:TextBox>
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
                                                        <tr>
                                                            <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 20%" align="right">
                                                                                <label>
                                                                                    TB regimen :</label>
                                                                            </td>
                                                                            <td style="width: 32%" align="left">
                                                                                <asp:DropDownList ID="ddltbregimen" onchange="dropdownchangetext('ctl00_IQCareContentPlaceHolder_tabControl_TabTBSCreening_ddltbregimen','divothertbregimen','Other');"
                                                                                    runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <div id="divothertbregimen" style="display: none;">
                                                                                    <table width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 39%" align="right">
                                                                                                    <label>
                                                                                                        Other TB regimen :</label>
                                                                                                </td>
                                                                                                <td align="left">
                                                                                                    <asp:TextBox runat="server" ID="txtothertbregimen"></asp:TextBox>
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
                                                        <tr>
                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 40%" align="right">
                                                                                <label>
                                                                                    TB regimen start date :</label>
                                                                            </td>
                                                                            <td style="width: 60%" align="left">
                                                                                <input id="txttbregimentstartdate" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                    onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                                                    maxlength="11" size="11" name="txtdtLMP" runat="server" />
                                                                                <img id="Img5" onclick="w_displayDatePicker('<%=txttbregimentstartdate.ClientID%>');"
                                                                                    height="22 " alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                                                                    border="0" name="appDateimg" style="vertical-align: text-bottom;" />
                                                                                <span class="smallerlabel" id="Span5">(DD-MMM-YYYY)</span>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 40%" align="right">
                                                                                <label>
                                                                                    TB regimen end date :</label>
                                                                            </td>
                                                                            <td style="width: 60%" align="left">
                                                                                <input id="txttbregimentenddate" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                    onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                                                    maxlength="11" size="11" name="txtdtLMP" runat="server" />
                                                                                <img id="Img6" onclick="w_displayDatePicker('<%=txttbregimentenddate.ClientID%>');"
                                                                                    height="22 " alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                                                                    border="0" name="appDateimg" style="vertical-align: text-bottom;" />
                                                                                <span class="smallerlabel" id="Span6">(DD-MMM-YYYY)</span>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="border pad6 whitebg" colspan="2" width="100%" align="center">
                                                                <label id="Label71" class="">
                                                                    TB treatment outcome :
                                                                </label>
                                                                <asp:DropDownList ID="ddltbtreatmentoutcome" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                            <div class="center formbg">
                                                <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td colspan="2" class="border center formbg">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="pnlheaderIPT" runat="server">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="ImgIPT" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <h4 class="forms" align="left">
                                                                                            IPT (Patients with no signs and symptoms)
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
                                                <asp:Panel ID="pnlIPTDetails" runat="server">
                                                    <table id="Table6" class="center formbg" cellspacing="6" cellpadding="0" width="100%"
                                                        border="0">
                                                        <tr align="left">
                                                            <td class="border pad6 whitebg" width="100%" colspan="2">
                                                                <label id="Label72" class="" style="padding-left: 11%;">
                                                                    INH Started :
                                                                </label>
                                                                <input id="rdoinhstartedyes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(1,'divinhstartedshowhide');"
                                                                    type="radio" name="inhstartedyes" runat="server" />
                                                                <label>
                                                                    Yes</label>
                                                                <input id="rdoinhstartedno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(0,'divinhstartedshowhide');"
                                                                    type="radio" name="inhstartedyes" runat="server" />
                                                                <label>
                                                                    No</label><br />
                                                                <div id="divinhstartedshowhide" style="display: none;">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td class="border center pad5 whitebg" colspan="2" style="width: 100%">
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 40%" align="left">
                                                                                                <label>
                                                                                                    If yes,stop reason :</label>
                                                                                                <div class="customdivbordermultiselect" id="divstopreason" nowrap="noWrap">
                                                                                                    <asp:CheckBoxList ID="cblstopreason" RepeatLayout="Flow" runat="server">
                                                                                                    </asp:CheckBoxList>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 40%" align="right">
                                                                                                <label>
                                                                                                    INH Start Date :</label>
                                                                                            </td>
                                                                                            <td style="width: 60%" align="left">
                                                                                                <input id="txtinhstartdate" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                                    onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                                                                    maxlength="11" size="11" name="txtdtLMP" runat="server" />
                                                                                                <img id="Img7" onclick="w_displayDatePicker('<%=txtinhstartdate.ClientID%>');" height="22 "
                                                                                                    alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0"
                                                                                                    name="appDateimg" style="vertical-align: text-bottom;" />
                                                                                                <span class="smallerlabel" id="Span7">(DD-MMM-YYYY)</span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 40%" align="right">
                                                                                                <label>
                                                                                                    INH End Date :</label>
                                                                                            </td>
                                                                                            <td style="width: 60%" align="left">
                                                                                                <input id="txtinhenddate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                                                                    onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="txtdtLMP" runat="server" />
                                                                                                <img id="Img8" onclick="w_displayDatePicker('<%=txtinhenddate.ClientID%>');" height="22 "
                                                                                                    alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0"
                                                                                                    name="appDateimg" style="vertical-align: text-bottom;" />
                                                                                                <span class="smallerlabel" id="Span8">(DD-MMM-YYYY)</span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 40%" align="right">
                                                                                                <label>
                                                                                                    Pyriodoxine Start Date :</label>
                                                                                            </td>
                                                                                            <td style="width: 60%" align="left">
                                                                                                <input id="txtpyriodoxinestartdate" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                                    onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                                                                    maxlength="11" size="11" name="txtdtLMP" runat="server" />
                                                                                                <img id="Img9" onclick="w_displayDatePicker('<%=txtpyriodoxinestartdate.ClientID%>');"
                                                                                                    height="22 " alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                                                                                    border="0" name="appDateimg" style="vertical-align: text-bottom;" />
                                                                                                <span class="smallerlabel" id="Span9">(DD-MMM-YYYY)</span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td class="border center pad5 whitebg" style="width: 50%">
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 40%" align="right">
                                                                                                <label>
                                                                                                    Pyriodoxine End Date :</label>
                                                                                            </td>
                                                                                            <td style="width: 60%" align="left">
                                                                                                <input id="txtpyriodoxineenddate" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                                    onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                                                                    maxlength="11" size="11" name="txtdtLMP" runat="server" />
                                                                                                <img id="Img10" onclick="w_displayDatePicker('<%=txtpyriodoxineenddate.ClientID%>');"
                                                                                                    height="22 " alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                                                                                    border="0" name="appDateimg" style="vertical-align: text-bottom;" />
                                                                                                <span class="smallerlabel" id="Span10">(DD-MMM-YYYY)</span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="border pad6 whitebg" width="100%" colspan="2" align="center">
                                                                <label id="Label79" class="margin35" for="District">
                                                                    Has adherence been addressed?</label>
                                                                <input id="rdohasadherenceassessedyes" onmouseup="up(this);" onfocus="up(this);"
                                                                    onclick="down(this);" type="radio" name="hasadherenceassessedyes" runat="server"
                                                                    checked />
                                                                <label>
                                                                    Yes</label>
                                                                <input id="rdohasadherenceassessedno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                                                    type="radio" name="hasadherenceassessedyes" runat="server" />
                                                                <label>
                                                                    No</label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="border pad6 whitebg" width="100%" align="center" colspan="2">
                                                                <table width="100%" border="0">
                                                                    <tr>
                                                                        <td style="width: 20%;" align="right">
                                                                            <label>
                                                                                Any missed doses :
                                                                            </label>
                                                                        </td>
                                                                        <td style="width: 25%;">
                                                                            <input id="rdoanymisseddosesyes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(1,'divreferredforadherence');"
                                                                                type="radio" name="anymisseddosesyes" runat="server" />
                                                                            <label>
                                                                                Yes</label>
                                                                            <input id="rdoanymisseddosesno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(0,'divreferredforadherence');"
                                                                                type="radio" name="anymisseddosesyes" runat="server" />
                                                                            <label>
                                                                                No</label>
                                                                        </td>
                                                                        <td>
                                                                            <div id="divreferredforadherence" style="display: none;">
                                                                                <label id="Label81" class="margin35" for="District">
                                                                                    If yes referred for adherence :
                                                                                </label>
                                                                                <input id="rdoyesreferredadherenceyes" onmouseup="up(this);" onfocus="up(this);"
                                                                                    onclick="down(this);" type="radio" name="yesreferredadherenceyes" runat="server" />
                                                                                <label>
                                                                                    Yes</label>
                                                                                <input id="rdoyesreferredadherenceno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                                                                    type="radio" name="yesreferredadherenceyes" runat="server" />
                                                                                <label>
                                                                                    No</label>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="border pad6 whitebg" width="50%" align="center">
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="border center pad5 whitebg" style="width: 100%" colspan="2">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <label>
                                                                                    Review checklist :</label>
                                                                                <div class="customdivbordermultiselect" id="divreviewchecklist" nowrap="noWrap">
                                                                                    <asp:CheckBoxList ID="cblreviewlist" RepeatLayout="Flow" onclick="CheckBoxToggleShowHide1('ctl00_IQCareContentPlaceHolder_tabControl_TabTBSCreening_cblreviewlist','divothertbshowhide','Other Side effects (specify)');"
                                                                                        Width="185px" runat="server">
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div id="divothertbshowhide" style="display: none;">
                                                                                    <table width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 55%" align="left">
                                                                                                    <label>
                                                                                                        Specify other TB side effects :</label><br />
                                                                                                    <asp:TextBox ID="othertbsideeffects" Width="100%" runat="server"></asp:TextBox>
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
                                                <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td colspan="2" class="border center formbg">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="pnlconfirmtbsuspect" runat="server">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="imgconfirmtb" ImageUrl="~/images/arrow-up.gif" runat="server" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <h4 class="forms" align="left">
                                                                                            Confirmed or TB Suspected
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
                                                <asp:Panel ID="pnlconfirmtbsuspected" runat="server">
                                                    <table id="Table7" class="center formbg" cellspacing="6" cellpadding="0" width="100%"
                                                        border="0">
                                                        <tr>
                                                            <td width="100%" class="border pad5 whitebg">
                                                                <table width="100%" border="0">
                                                                    <tr>
                                                                        <td width="35%;" align="right">
                                                                            <label>
                                                                                Confirmed or Suspected TB (Stop INH) :</label>
                                                                        </td>
                                                                        <td width="15%;">
                                                                            <input id="rdostopINHYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(1,'divstopinhdateshowhide');"
                                                                                type="radio" name="stopINHYes" runat="server" />
                                                                            <label>
                                                                                Yes</label>
                                                                            <input id="rdostopINHno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(0,'divstopinhdateshowhide');fnReset('ctl00_IQCareContentPlaceHolder_tabControl_TabTBSCreening_dtStopINHDate','txt');"
                                                                                type="radio" name="stopINHYes" runat="server" checked />
                                                                            <label>
                                                                                No</label>
                                                                        </td>
                                                                        <td>
                                                                            <div id="divstopinhdateshowhide" style="display: none;">
                                                                                <table class="tbl-left">
                                                                                    <tr>
                                                                                        <td align="right" style="width: 38%;" class="data-lable">
                                                                                            <label>
                                                                                                Stop INH Date:</label>
                                                                                        </td>
                                                                                        <td align="left" class="data-control">
                                                                                            <input id="dtStopINHDate" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                                                                onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="VisitDate"
                                                                                                runat="server" />
                                                                                            <img id="Img14" onclick="w_displayDatePicker('<%=dtStopINHDate.ClientID%>');" height="22 "
                                                                                                alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0"
                                                                                                name="appDateimg" style="vertical-align: text-bottom;" />
                                                                                            <span class="smallerlabel" id="Span14">(DD-MMM-YYYY)</span>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100%" class="border pad5 whitebg">
                                                                <table width="100%" border="0">
                                                                    <tr>
                                                                        <td width="35%;" align="right">
                                                                            <label>
                                                                                Household contacts screened for TB :</label>
                                                                        </td>
                                                                        <td width="15%;">
                                                                            <input id="rdohouseholdtbyes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(0,'divhouseholdshowhide');fnReset('ctl00_IQCareContentPlaceHolder_tabControl_TabTBSCreening_txtTBNotScreenedSpecify','txt');"
                                                                                type="radio" name="householdtbyes" runat="server" />
                                                                            <label>
                                                                                Yes</label>
                                                                            <input id="rdohouseholdtbno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(1,'divhouseholdshowhide');"
                                                                                type="radio" name="householdtbyes" runat="server" checked />
                                                                            <label>
                                                                                No</label>
                                                                        </td>
                                                                        <td>
                                                                            <div id="divhouseholdshowhide" style="display: none;">
                                                                                <table class="tbl-left">
                                                                                    <tr>
                                                                                        <td align="right" style="width: 38%;" class="data-lable">
                                                                                            <label>
                                                                                                If No specify why :</label>
                                                                                        </td>
                                                                                        <td align="left" class="data-control">
                                                                                            <asp:TextBox ID="txtTBNotScreenedSpecify" runat="server" Width="50%">
                                                                                            </asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
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
                                        </div>--%>
                                <%--  <asp:CollapsiblePanelExtender ID="cpetbscreening" runat="server" SuppressPostBack="true"
                                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnltbscreening" CollapseControlID="pnlheadertbscreening"
                                            ExpandControlID="pnlheadertbscreening" CollapsedImage="~/images/arrow-up.gif"
                                            Collapsed="true" ImageControlID="ImgTBScreening">
                                        </asp:CollapsiblePanelExtender>--%>
                                <%-- <asp:CollapsiblePanelExtender ID="CPETBEvaluationTP" runat="server" SuppressPostBack="true"
                                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnltbevaluation" CollapseControlID="pnlheadertbevaluationtp"
                                            ExpandControlID="pnlheadertbevaluationtp" CollapsedImage="~/images/arrow-up.gif"
                                            Collapsed="true" ImageControlID="imgtbevaluation">
                                        </asp:CollapsiblePanelExtender>--%>
                                <%--  <asp:CollapsiblePanelExtender ID="CPEIPT" runat="server" SuppressPostBack="true"
                                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlIPTDetails" CollapseControlID="pnlheaderIPT"
                                            ExpandControlID="pnlheaderIPT" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                                            ImageControlID="ImgIPT">
                                        </asp:CollapsiblePanelExtender>--%>
                                <%--  <asp:CollapsiblePanelExtender ID="CPEConfirmTB" runat="server" SuppressPostBack="true"
                                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlconfirmtbsuspected"
                                            CollapseControlID="pnlconfirmtbsuspect" ExpandControlID="pnlconfirmtbsuspect"
                                            CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="imgconfirmtb">
                                        </asp:CollapsiblePanelExtender>--%>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabExamination" runat="server" HeaderText="Examination">
                            <HeaderTemplate>
                                Examination
                            </HeaderTemplate>
                            <ContentTemplate>
                            <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlheaderlongtermmedication" runat="server">
          <h5 class="forms" align="left">
          <asp:ImageButton ID="imglongtermmedication" ImageUrl="~/images/arrow-up.gif" runat="server" />
           <asp:Literal ID="literalCurrentLong" Text="Current Long Term Medications" runat="server"></asp:Literal>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
         <asp:Panel ID="pnllongtermmedication" runat="server" style="overflow:hidden;">
        <div class="row">
 <div class="col-md-4">
 <%--left--%>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label"> <asp:Label ID="lblLongTermText" CssClass="required" runat="server">Long term medications:</asp:Label></label>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           <div class="customdivbordermultiselect">
                                                                        <asp:CheckBoxList ID="chkLongTermMedication" RepeatLayout="Flow" onclick="CheckBoxToggleShowHide('ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_chkLongTermMedication','divLongTermMedication','Other','ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_txOtherLongTermMedications');"
                                                                            Width="190px" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
             </div>
            
 </div>
 </div>
 <div class="col-md-8">
 <%--right--%>

 <div class="row" style="display: none;" id="divLongTermMedication">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other current long term medications:</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
           <asp:TextBox ID="txOtherLongTermMedications" runat="server" Width="98%" CssClass="form-control"> </asp:TextBox>
             </div>
            
 </div>


 </div>
 </div>
 <div class="row" style="display: none;">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Long term medications:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <input id="rblbtnLongTermMedicationsyes" onmouseup="up(this);" onfocus="up(this);"
                                                                        onclick="down(this);" type="radio" name="rblbtnLongTermMedicationsyes" runat="server"
                                                                        checked />
                                                                    <label>
                                                                        Yes</label>
                                                                    <input id="rblbtnLongTermMedicationsno" onmouseup="up(this);" onfocus="up(this);"
                                                                        onclick="down(this);" type="radio" name="rblbtnLongTermMedicationsyes" runat="server" />
                                                                    <label>
                                                                        No</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label  class="control-label"></label>
             </div> 
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other current long term medications:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label"></label>
             </div> 
 </div>
 <div class="row" style="display: none;">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Antifungals:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtAntifungalsDate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Anticonvulsants:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtAntihypertensivesDate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
 </div>
 <div class="row" style="display: none;">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Sulfa TMP:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtSulfaTMPDate" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other long term medications:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="dtOtherCurrentLongTermMedications" clientidmode="Static"
                                                       maxlength="11" size="11"  runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
 </div>
 </asp:Panel>
 <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlheaderphysicalexam" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="imgphysicalexam" ImageUrl="~/images/arrow-up.gif" runat="server" />
          <asp:Literal ID="literalPhysicalExam" runat="server" Text="Physical Examination"></asp:Literal>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="pnlphysicalexam" runat="server" style="overflow:hidden;">
<div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <UcPhysicalExamination:Uc4 ID="UcPE" runat="server" />
             </div>
          
 </div>
</asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlheaderdevmil" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="imgdevmilestones" ImageUrl="~/images/arrow-up.gif" runat="server" />
           Developmental Milestones
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="pnldevemilestones" runat="server" style="overflow:hidden;">
<div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Milestone appropriate:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rbListMilestone" onclick="rblSelectedValueShowHide(this,'divmilestoneshowhide','ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_txtspecifywhyinappropriate');"
                                                                        runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
             </div>
             <div id="divmilestoneshowhide" style="display: none; text-indent: inherit">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">If No specify why inappropriate:</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            <asp:TextBox runat="server" ID="txtspecifywhyinappropriate" TextMode="MultiLine"  Width="98%" CssClass="form-control" style="resize:none;"></asp:TextBox>
             </div> 
             </div>
 </div>
                                                
                                        </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlheadertestsandlabs" runat="server">
          <h5 class="forms" align="left">
          <asp:ImageButton ID="imgtestsandlab" ImageUrl="~/images/arrow-up.gif" runat="server" />
           Lab Evaluation
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="pnltestsandlabs" runat="server" style="overflow:hidden;">
<div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <UcLabEvalution:Uc7 ID="UCLabEval" runat="server" />
             </div>
           
 </div>
 <div class="row" style="display: none;">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Lab evaluation:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="rdolabevaluationyes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(1,'divlabevaluatinshowhide');"
                                                                        type="radio" name="labevaluationyes" runat="server" />
                                                                    <label>
                                                                        Yes</label>
                                                                    <input id="rdolabevaluationno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(0,'divlabevaluatinshowhide');"
                                                                        type="radio" name="labevaluationyes" runat="server" />
                                                                    <label>
                                                                        No</label>
             </div>
              <div id="divlabevaluatinshowhide" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label"> If yes specify lab evaluation:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="checkbox" id="divlabevaluation" nowrap="noWrap">
                                                                            <asp:CheckBoxList ID="cbllabevaluation" RepeatLayout="Flow" runat="server">
                                                                            </asp:CheckBoxList>
                                                                        </div>
             </div> 
             </div>
 </div>


                                            
                                        </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlheaderwhostaging" runat="server">
          <h5 class="forms" align="left">
          <asp:ImageButton ID="imgwhostaging" ImageUrl="~/images/arrow-up.gif" runat="server" />
          <asp:Literal ID="literalWHOStaging" runat="server" Text="WHO Staging"></asp:Literal>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="pnlwhostaging" runat="server" style="overflow:hidden;">
<div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <UcWhoStaging:Uc5 ID="UcWhostaging" runat="server" />
             </div>
 </div>
 </asp:Panel>
   <div class="center formbg" style="display: none;">
   <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlheaderstaging" runat="server">
          <h5 class="forms" align="left">
          <asp:ImageButton ID="imgstagingat" ImageUrl="~/images/arrow-up.gif" runat="server" />
           Staging at Initial Evaluation
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
                                        
                                    </div>
                                    <%--                                            <div class="center formbg" style="display:none;">
                                                <asp:Panel ID="pnlstagingatinitial" runat="server">
                                                    <table id="Table11" class=" center formbg" cellspacing="6" cellpadding="0" width="100%"
                                                        border="0">
                                                        <tr>
                                                            <td width="50%" class="border pad5 whitebg">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align="right" style="width: 55%;">
                                                                            <label>
                                                                                WHO Stage at initiation (Transfer in):</label>
                                                                        </td>
                                                                        <td align="left" class="data-control">
                                                                            <asp:DropDownList ID="ddlInitiationWHOstage" runat="server" AutoPostBack="false"
                                                                                Width="130px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td width="50%" class="border pad5 whitebg">
                                                                <table class="tbl-left">
                                                                    <tr>
                                                                        <td align="right" class="data-lable">
                                                                            <label class="required">
                                                                                *HIV associated conditions:</label>
                                                                        </td>
                                                                        <td align="left" class="data-control">
                                                                            <asp:DropDownList ID="ddlhivassociated" runat="server" Width="130px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="50%" class="border pad5 whitebg">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align="right" style="width: 55%;">
                                                                            <label class="required">
                                                                                *Current WHO Stage:</label>
                                                                        </td>
                                                                        <td align="left" class="data-control">
                                                                            <asp:DropDownList ID="ddlwhostage1" runat="server" Width="130px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td width="50%" class="border pad5 whitebg">
                                                                <table class="tbl-left">
                                                                    <tr>
                                                                        <td align="right" class="data-lable">
                                                                            <label class="required">
                                                                                *WAB Stage:</label>
                                                                        </td>
                                                                        <td align="left" class="data-control">
                                                                            <asp:DropDownList ID="ddlwabstage1" runat="server" Width="130px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div id="divmernarcheshow">
                                                                  <table class="border pad6 whitebg" width="100%">
                                                                        <tr>
                                                                            <td style="width: 27%;" align="right" class="pad5">
                                                                                <label>
                                                                                    Mernarche:
                                                                                </label>
                                                                            </td>
                                                                            <td style="width: 37%;" align="left">
                                                                                <input id="radbtnMernarcheyes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(1,'divmenarchedatehshowhide','ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_txtmenarchedate');"
                                                                                    type="radio" name="labevaluationyes" runat="server" />
                                                                                <label>
                                                                                    Yes</label>
                                                                                <input id="radbtnMernarcheno" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);rblSelectedValue1(0,'divmenarchedatehshowhide','ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_txtmenarchedate');"
                                                                                    type="radio" name="labevaluationyes" runat="server" checked />
                                                                                <label>
                                                                                    No</label>
                                                                            </td>
                                                                            <td>
                                                                                <div id="divmenarchedatehshowhide" style="display: none;">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <label id="Label88">
                                                                                                    Menarche Date:
                                                                                                </label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <input id="txtmenarchedate" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                                                    onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                                                                    maxlength="11" size="11" name="VisitDate" runat="server" />
                                                                                                <img id="Img11" onclick="w_displayDatePicker('<%=txtmenarchedate.ClientID%>');" height="22 "
                                                                                                    alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22" border="0"
                                                                                                    name="appDateimg" style="vertical-align: text-bottom;" />
                                                                                                <span class="smallerlabel" id="Span11">(DD-MMM-YYYY)</span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                 </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="50%" class="border pad5 whitebg" colspan="2">
                                                                <div id="divtannerstaging">
                                                                    <table class="tbl-left">
                                                                        <tr>
                                                                            <td align="right" class="data-lable">
                                                                                <label>
                                                                                    Tanner Staging:</label>
                                                                            </td>
                                                                            <td align="left" class="data-control">
                                                                                <asp:DropDownList ID="ddltannerstaging" runat="server" AutoPostBack="false" Width="130px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                    --%>

<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlDiagnosis" runat="server">
          <h5 class="forms" align="left">
          <asp:ImageButton ID="imgDiagnosis" ImageUrl="~/images/arrow-up.gif" runat="server" />
           Diagnosis
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="pnlDiagnosisDetail" runat="server" style="overflow:hidden;">
<div class="row">
 <div class="col-md-4">
 <%--left--%>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Diagnosis and current illness at this visit:</label>
             </div>
             
 </div>
 <div class="row" >
 <div class="col-md-12 col-sm-12 col-xs-12 form-group customdivbordermultiselect" id="div1">
              <asp:CheckBoxList ID="cblDiagnosis" RepeatLayout="Flow" runat="server" Width="100%"
                                                                        onclick="CheckBoxToggleShowHide('ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_cblDiagnosis','divNonHIV','Non-HIV related illness','ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_txtNonHIVRelatedOI');CheckBoxToggleShowHide('ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_cblDiagnosis','divHIV','HIV-Related illness','ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_txtHIVRelatedOI');">
                                                                    </asp:CheckBoxList>
             </div>
           
 </div>


 </div>
 <div class="col-md-8">
 <%--right--%>
 <div class="row" id="divHIV" style="display: none;">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify HIV related OI:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtHIVRelatedOI" runat="server" Skin="MetroTouch" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
           
 </div>
 <div class="row" style="display: none;" id="divNonHIV">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify Non HIV related OI:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtNonHIVRelatedOI" runat="server" Skin="MetroTouch"  Width="98%" CssClass="form-control"> </asp:TextBox>
             </div>
            
 </div>

 </div>
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblImpression-8888925">Impression:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList runat="server" ID="ddlHAARTImpression" Width="98%" CssClass="form-control" onchange="dropdownchangetext('ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_ddlHAARTImpression','divSpecifyotherimpression','Other specify','ctl00_IQCareContentPlaceHolder_tabControl_TabExamination_txtOtherHAARTImpression');">
                                                                </asp:DropDownList>
             </div>
              <div id="divSpecifyHAART" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblSpecify HAART patient impression-8888926">Specify HAART patient impression:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlHAARTexperienced" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
             <div id="divSpecifyotherimpression" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblSpecify other impression-88881307">Specify other impression:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtOtherHAARTImpression" Columns="20" Rows="6" style="resize:none;"  Width="98%" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
             </div>
             </div>
 </div>


                                        
                                        
                                    </asp:Panel>
                               
                                <asp:CollapsiblePanelExtender ID="CPElongtermmedication" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnllongtermmedication"
                                    CollapseControlID="pnlheaderlongtermmedication" ExpandControlID="pnlheaderlongtermmedication"
                                    CollapsedImage="~/images/arrow-up.gif" Collapsed="true" ImageControlID="imglongtermmedication">
                                </asp:CollapsiblePanelExtender>
                                <asp:CollapsiblePanelExtender ID="CPEPhysicalexam" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlphysicalexam" CollapseControlID="pnlheaderphysicalexam"
                                    ExpandControlID="pnlheaderphysicalexam" CollapsedImage="~/images/arrow-up.gif"
                                    Collapsed="true" ImageControlID="imgphysicalexam"></asp:CollapsiblePanelExtender>
                                <asp:CollapsiblePanelExtender ID="cpedevmil" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnldevemilestones" CollapseControlID="pnlheaderdevmil"
                                    ExpandControlID="pnlheaderdevmil" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                                    ImageControlID="imgdevmilestones"></asp:CollapsiblePanelExtender>
                                <asp:CollapsiblePanelExtender ID="CPETestandlabs" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnltestsandlabs" CollapseControlID="pnlheadertestsandlabs"
                                    ExpandControlID="pnlheadertestsandlabs" CollapsedImage="~/images/arrow-up.gif"
                                    Collapsed="true" ImageControlID="imgtestsandlab"></asp:CollapsiblePanelExtender>
                                <asp:CollapsiblePanelExtender ID="CPEWhostaging" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlwhostaging" CollapseControlID="pnlheaderwhostaging"
                                    ExpandControlID="pnlheaderwhostaging" CollapsedImage="~/images/arrow-up.gif"
                                    Collapsed="true" ImageControlID="imgwhostaging"></asp:CollapsiblePanelExtender>
                                <%--  <asp:CollapsiblePanelExtender ID="CPEStaging" runat="server" SuppressPostBack="true"
                                            ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlstagingatinitial" CollapseControlID="pnlheaderstaging"
                                            ExpandControlID="pnlheaderstaging" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                                            ImageControlID="imgstagingat">
                                        </asp:CollapsiblePanelExtender>--%>
                                <asp:CollapsiblePanelExtender ID="CPDiagnosis" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlDiagnosisDetail" CollapseControlID="pnlDiagnosis"
                                    ExpandControlID="pnlDiagnosis" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                                    ImageControlID="ImgDiagnosis"></asp:CollapsiblePanelExtender>
                               <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <UCSignature:UserControlKNHSignature ID="UserControlKNHSignature_Examination" runat="server" />
             </div>
            
 </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnSaveExam" runat="server" Text="Save" OnClick="btnSaveExam_Click"
                                                        CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
<label class="glyphicon glyphicon-floppy-disk" style="margin-left: -4%; margin-right:3%; vertical-align: sub; color: #fff;"></label>
                                                    <asp:Button ID="btnCloseExam" runat="server" Text="Close" OnClick="btnCloseExam_Click"
                                                        CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
<label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
                                                    <asp:Button ID="btnPrintExam" Text="Print" OnClientClick="WindowPrint()" runat="server"
                                                        CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
<label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right:2%"></label>
 </div>
 </div>
                                
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabManagement" runat="server" HeaderText="Management">
                            <HeaderTemplate>
                                Management
                            </HeaderTemplate>
                            <ContentTemplate>
                            <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlheaderdrugallergy" runat="server">
          <h5 class="forms" align="left">
          <asp:ImageButton ID="imgdrugallergy" ImageUrl="~/images/arrow-up.gif" runat="server" />
           Drug Allergy
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
<asp:Panel ID="pnldrugallergy" runat="server" style="overflow:hidden;">
<div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <UcDrugAllergy:Uc6 ID="UCDrugAllergy" runat="server" />
             </div>
            
 </div>
 <div class="row" style="display: none;">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Drug allergy toxicities:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div id="divDrugAllergiesToxicitiesPaeds" class="customdivbordermultiselect">
                                                                        <asp:CheckBoxList ID="cblDrugAllergiesToxicitiesPaeds" onclick="CheckBoxToggleShowHide1('ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_cblDrugAllergiesToxicitiesPaeds','divspecifyotherdrugshowhide','Other');fnControlUnableDesable('ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_cblDrugAllergiesToxicitiesPaeds','Sulphur','ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_ddloiprophylaxis');"
                                                                            RepeatLayout="Flow" runat="server" Width="250px">
                                                                        </asp:CheckBoxList>
                                                                    </div>
             </div>
              <div id="divspecifyotherdrugshowhide" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify allergy:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtotherdrugallergy" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
 </div>
 <div class="row" style="display: none;">
 <div id="divspecifyantibioticshowhide" style="display: none;">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify antibiotic allergy:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtantibioticallergy" runat="server" Width="98%" CssClass="form-control"> </asp:TextBox>
             </div>
             </div>
             <div id="divspecifyarvallergyshowhide" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Specify ARV allergy:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtarvallergy" runat="server" Width="98%" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
 </div>
                                            
                                        </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlARVSideeffects" runat="server">
          <h5 class="forms" align="left">
          <asp:ImageButton ID="ImgARVSideEffect" ImageUrl="~/images/arrow-up.gif" runat="server" />
          ARV Side Effects
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="pnlARVSideEffectDetail" runat="server" style="overflow:hidden;">
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
                                                                    <asp:CheckBoxList ID="cblshorttermeffects" onclick="CheckBoxToggleShowHide1('ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_cblshorttermeffects','divshorttermeffecttxt','Other Specify','ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_txtspecityothershortterm');"
                                                                        RepeatLayout="Flow" runat="server" Width="200px">
                                                                    </asp:CheckBoxList>
                                                                </div>
             </div>
 </div>
 <div id="divshorttermeffecttxt" style="display: none">
 <div class="row">
 <div class="col-md-12">
             <label class="control-label">Specify other short term effects:</label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <asp:TextBox ID="txtspecityothershortterm" runat="server" Skin="MetroTouch" Width="98%" CssClass="form-control"></asp:TextBox>
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
                                                                    <asp:CheckBoxList ID="cbllongtermeffects" onclick="CheckBoxToggleShowHide1('ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_cbllongtermeffects','divlongtermeffecttxt','Other specify','ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_txtspecifyotherlongterm');"
                                                                        RepeatLayout="Flow" runat="server" Width="200px">
                                                                    </asp:CheckBoxList>
                                                                </div>
             </div>
 </div>
 <div id="divlongtermeffecttxt" style="display: none">
 <div class="row">
 <div class="col-md-12">
             <label class="control-label">Specify other long term effects:</label>
             </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:TextBox ID="txtspecifyotherlongterm" runat="server" Skin="MetroTouch" Width="98%" CssClass="form-control"></asp:TextBox>
             </div>
 </div>
 </div>
 </div>
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Work up plan:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
<asp:TextBox ID="txtworkupplan" runat="server" Skin="MetroTouch" TextMode="MultiLine" Width="98%" CssClass="form-control" style="resize:none;"></asp:TextBox>
             </div>
            
 </div>

                                        
                                    </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlheadertreatment" runat="server">
          <h5 class="forms" align="left">
         <asp:ImageButton ID="imgtreatment" ImageUrl="~/images/arrow-up.gif" runat="server" />
           <asp:Literal ID="literalTreatment" runat="server" Text="Treatment"></asp:Literal>
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
         <asp:Panel ID="pnltreatment" runat="server" style="overflow:hidden;">
         <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<UcPharmacy:Uc9 ID="UcPharmacy" runat="server" />
             </div>
            
 </div>
                                            
                                        </asp:Panel>
<div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <asp:Panel ID="pnlOITreatment" runat="server">
          <h5 class="forms" align="left">
          <asp:ImageButton ID="imgOITreatment" ImageUrl="~/images/arrow-up.gif" runat="server" />
           OI Treatment
          </h5>
                                                                    
                                                                </asp:Panel>
        </div>
        </div>
        <asp:Panel ID="pnlOITreatmentDetail" runat="server" Width="100%" style="overflow:hidden;">
        <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">OI Prophylaxis:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList runat="server" ID="ddloiprophylaxis"  Width="98%" CssClass="form-control" onchange="dropdownchangetext('ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_ddloiprophylaxis','divoiprophylaxisshowhide','Cotrimoxazole','ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_ddlcotrimoxazole');dropdownchangetext('ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_ddloiprophylaxis','divoiprophylasixothershowhide','Other','ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_txtothercotrimoxazole');dropdownFluconazole('ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_ddloiprophylaxis','divFluconazoleshowhide','Fluconazole','ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_ddlfluconazole');dropdownFluconazole('ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_ddloiprophylaxis','divFluconazoleshowhide','CTX and Fluconazol','ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_ddlcotrimoxazole');">
             </asp:DropDownList>
             </div>
              <div id="divoiprophylaxisshowhide" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Cotrimoxazole prescribed for:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlcotrimoxazole" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
             <div id="divFluconazoleshowhide" style="display: none;">
              <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Fluconazole prescribed for:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlfluconazole" Width="98%" CssClass="form-control"></asp:DropDownList>
             </div> 
             </div>
             <div id="divoiprophylasixothershowhide" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Other (Specify):</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:TextBox runat="server" ID="txtothercotrimoxazole" Width="98%" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>

 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Other treatment:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
            <asp:TextBox runat="server" ID="txtothertreatementcatrimozazole"  Width="98%" CssClass="form-control" TextMode="MultiLine" style="resize:none;"></asp:TextBox>
             </div>
            
 </div>
                                            
                                        </asp:Panel>
                                <asp:CollapsiblePanelExtender ID="CPEOITreatment" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlOITreatmentDetail"
                                    CollapseControlID="pnlOITreatment" ExpandControlID="pnlOITreatment" CollapsedImage="~/images/arrow-up.gif"
                                    Collapsed="true" ImageControlID="imgOITreatment"></asp:CollapsiblePanelExtender>
                                <asp:CollapsiblePanelExtender ID="CPEARVSE" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnlARVSideEffectDetail"
                                    CollapseControlID="pnlARVSideeffects" ExpandControlID="pnlARVSideeffects" CollapsedImage="~/images/arrow-up.gif"
                                    Collapsed="true" ImageControlID="ImgARVSideEffect"></asp:CollapsiblePanelExtender>
                                <asp:CollapsiblePanelExtender ID="CPEdrugallergy" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnldrugallergy" CollapseControlID="pnlheaderdrugallergy"
                                    ExpandControlID="pnlheaderdrugallergy" CollapsedImage="~/images/arrow-up.gif"
                                    Collapsed="true" ImageControlID="imgdrugallergy"></asp:CollapsiblePanelExtender>
                                <asp:CollapsiblePanelExtender ID="CPETreatment" runat="server" SuppressPostBack="true"
                                    ExpandedImage="~/images/arrow-dn.gif" TargetControlID="pnltreatment" CollapseControlID="pnlheadertreatment"
                                    ExpandControlID="pnlheadertreatment" CollapsedImage="~/images/arrow-up.gif" Collapsed="true"
                                    ImageControlID="imgtreatment"></asp:CollapsiblePanelExtender>
                             
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           <UCSignature:UserControlKNHSignature ID="UserControlKNHSignature_Management" runat="server" />
             </div>
             
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
          <asp:Button ID="btnSaveManagement" runat="server" Text="Save" OnClick="btnSaveManagement_Click"
                                                       CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
                                                       <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                                                    <asp:Button ID="btnCloseManagement" runat="server" Text="Close" OnClick="btnCloseManagement_Click"
                                                        CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
                                                   <label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 2%;""></label>
                                                    <asp:Button ID="btnPrintManagement" Text="Print" OnClientClick="WindowPrint()" runat="server"
                                                        CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;" />
                                                        <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
             </div>
             
 </div>
                                <div class="border center formbg">
                                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="form" align="center">
                                                    
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td class="form">
                                                   
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabPrevwithpositive" runat="server" HeaderText="Prev With +ve">
                            <HeaderTemplate>
                                Prevention With Positives
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div class="border center formbg">
                                    <table class="center formbg" cellspacing="6" cellpadding="0" width="100%" border="0">
                                        <tr valign="top">
                                            <td colspan="2" width="100%">
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <UcPwP:Uc10 ID="UcPWP" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
        </div>
        </div>
         <div class="row" style="display: none;">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label required">Signature:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlsingature" Width="98%" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hidupdate" runat="server" />
                                        <asp:HiddenField ID="hidregimen" runat="server" />
             </div>
        </div>
        </div>
         <div class="row" align="center">       
        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
          <asp:Button ID="Button1" runat="server" Visible="false" Text="Save" OnClick="Button1_Click"
                                            CssClass="btn btn-primary" Height="26px" />
                                        <%-- <asp:Button ID="btncomplete" runat="server" Text="Data Quality Check" OnClick="btncomplete_Click" />
                                                <asp:Button ID="btnback" runat="server" Visible="false" Text="Close" />
                                                <asp:Button ID="btnPrint" Text="Print" OnClientClick="WindowPrint()" runat="server" />--%>
                                        <asp:CheckBox ID="chkpyridoxine" Visible="false" runat="server" />
        </div>
        </div>
           <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
        <uc12:UserControlKNH_BackToTop ID="UserControlKNH_BackToTop1" runat="server" />
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

