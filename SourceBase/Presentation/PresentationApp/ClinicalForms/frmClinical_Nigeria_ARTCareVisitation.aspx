<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmClinical_Nigeria_ARTCareVisitation.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_Nigeria_ARTCareVisitation" %>

<%@ Register TagPrefix="UcNextAppointment" TagName="UcNxtAppt" Src="~/ClinicalForms/UserControl/UserControlKNH_NextAppointment.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtVisitDate').datepicker({ autoclose: true });
        $('#txtEDD').datepicker({ autoclose: true });
        $('#txtARTEndeddate').datepicker({ autoclose: true });  
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>

    <script language="javascript" type="text/javascript">

        function WindowPrint() {
            window.print();
        }

        function fnPageOpen(pageopen) {
            if (pageopen == "Pharmacy") {
                window.open('../PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx?opento=ArtForm');
            }
            else if (pageopen == "Labratory") {
                window.open('../Laboratory/frm_Laboratory.aspx?opento=ArtForm');
            }
            else if (pageopen == "LabTest") {
                window.open('../Laboratory/frm_Laboratory.aspx?opento=ArtForm');
            }
        }

        function fnfamilyplanning() {
            var e = document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Select") {
                hide('divFamilyPlanningMethod');
            }
            else if (strtext == "ONFP=on Family Planning") {
                show('divFamilyPlanningMethod');
            }
            else { hide('divFamilyPlanningMethod'); }
        }

        function fnARVDrug() {
            var e = document.getElementById("<%=ddlarvdrugadhere.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Select") {
                hide('divARVAdherence');
            }
            else if (strtext == "Fair" || strtext == "Poor") {
                show('divARVAdherence');
            }
            else { hide('divARVAdherence'); }
        }

        function fnCotrimoxale() {
            var e = document.getElementById("<%=ddlCotrimoxazoleAdhere.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Select") {
                hide('divCotrimoxazole');
            }
            else if (strtext == "Fair" || strtext == "Poor") {
                show('divCotrimoxazole');
            }
            else { hide('divCotrimoxazole'); }
        }

        function fnINH() {
            var e = document.getElementById("<%=DDLINH.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Select") {
                hide('divINH');
            }
            else if (strtext == "Fair" || strtext == "Poor") {
                show('divINH');
            }
            else { hide('divINH'); }
        }

        function fnSubsituations() {
            var e = document.getElementById("<%=ddlsubsituationInterruption.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Change regimen") {
                show('arvTherapyChange');
                hide('arvTherapyStop');

            }
            else if (strtext == "Stop treatment") {
                show('arvTherapyStop');
                hide('arvTherapyChange');
            }
            else {
                hide('arvTherapyChange');
                hide('arvTherapyStop');
            }
        }

        function fnRegimenChange() {
            var e = document.getElementById("<%=ddlArvTherapyChangeCode.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "10=Other reasons(specify)") {
                show('otherarvTherapyChangeCode');

            }
            else {
                hide('otherarvTherapyChangeCode');
            }
        }

        function fnStopReason() {
            var e = document.getElementById("<%=ddlArvTherapyStopCode.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "10=Other reasons(specify)") {
                show('otherarvTherapyStopCode');

            }
            else {
                hide('otherarvTherapyStopCode');
            }
        }

        function CalculateBMI(txtBMI, txtWeight, txtHeight) {
            var weight = document.getElementById(txtWeight).value;
            var height = document.getElementById(txtHeight).value;
            if (weight == "" || height == "") {
                weight = 0;
                height = 0;
            }

            weight = parseFloat(weight);
            height = parseFloat(height);

            var BMI = weight / ((height / 100) * (height / 100));
            BMI = BMI.toFixed(2);
            document.getElementById(txtBMI).value = BMI;
        }

        function fnTBStatus() {
            var e = document.getElementById("<%=ddlTBStatus.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Select") {
                hide('tbCardNo');
                document.getElementById("<%=txtTBCardNo.ClientID %>").value = "";
            }
            else if (strtext == "4=TB Rx") {
                show('tbCardNo');
            }
            else {
                hide('tbCardNo');
                document.getElementById("<%=txtTBCardNo.ClientID %>").value = "";
            }
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

        function GetCheckboxId(Id) {
            if (document.getElementById("<%=PnlNotedSideEffects.ClientID %>") != null) {
                var searchEles = document.getElementById("<%=PnlNotedSideEffects.ClientID %>").children;
                for (var i = 0; i < searchEles.length; i++) {
                    if (searchEles[i].children.length > 0) {
                        for (var ii = 0; ii < searchEles[i].children.length; ii++) {
                            if (searchEles[i].children[ii].tagName == 'LABEL' && searchEles[i].children[ii].textContent == "12=None" && searchEles[i].children[ii].htmlFor == Id) {
                                checkNone(searchEles, Id);
                            }
                            else if (searchEles[i].children[ii].tagName == 'LABEL' && searchEles[i].children[ii].htmlFor == Id) {
                                checkNotNone(searchEles, Id);
                            }
                        }
                    }
                }
            }
        }

        function ShowPnlforOther(param, shwpnl) {
            if (document.getElementById(param) != null) {
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
            }
            if (document.getElementById(chkboxId) != null) {
                if (document.getElementById(chkboxId).checked == true) {
                    show(shwpnl);
                }
            }
        }
            
    </script>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">ART Card Visitation</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            <label class="control-label required" id="lblVisitDate" runat="server">Visit Date:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtVisitDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap text-right">
             <label class="control-label">If Scheduled:</label><input id="chkifschedule" type="checkbox" runat="server" class="checkbox-btn"/>
             </div>
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <div class="row">
              <div class="col-md-6">
             <label  class="control-label">Duration Since: ART Start</label>
             </div> 
             <div class="col-md-2">
            <asp:TextBox ID="txtARTStart" CssClass="form-control" Width="60" MaxLength="3" Columns="2" runat="server"></asp:TextBox>            
             </div> 
              <div class="col-md-2">
             <span class="smalllabel">Months</span>
             </div>
              </div>
              </div>
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
               <div class="row">
              <div class="col-md-6">
              <label  class="control-label">Starting Current Regimen</label>
              </div>
              <div class="col-md-2">
              <asp:TextBox ID="txtStartingCurrentRegimen" CssClass="form-control" Width="60" MaxLength="3" Columns="2" runat="server"></asp:TextBox>
              </div>
              <div class="col-md-2">
              <span class="smalllabel">Months</span>
              </div>
              </div>
              </div>
              </div>
              <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Clinical Status</h5>
         </div>             
            </div>
            <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <div class="row">
              <div class="col-md-4">
              <label id="lblWeight" runat="server" class="control-label">Weight:</label>
            </div>
            <div class="col-md-5">
            <asp:TextBox ID="txtPhysWeight" runat="server" MaxLength="5" Columns="4" Width="80" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">
             <span class="smallerlabel">kg</span>
            </div>
            </div>
             </div>

             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <div class="row">
              <div class="col-md-4">
              <label id="lblHeight" runat="server" class="control-label">Height:</label>
            </div>
             <div class="col-md-5">
             <asp:TextBox ID="txtPhysHeight" runat="server" MaxLength="3" Columns="4" Width="80" CssClass="form-control"></asp:TextBox>
            </div>
             <div class="col-md-2">
             <span class="smallerlabel">cm</span>
            </div>
            </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <div class="row">
              <div class="col-md-4">
              <label id="lblBmi" runat="server" class="control-label">BMI:</label>
            </div>
            <div class="col-md-5">
            <asp:TextBox ID="txtBMI" runat="server" MaxLength="6" Columns="4" ReadOnly="true" Width="80" CssClass="form-control"></asp:TextBox>
            </div>
            </div>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <div class="row">
            <div class="col-md-3 text-right">
            <label class="control-label">BP:</label>
            </div>
            <div class="col-md-4 text-nowrap">
            <div class="row">
            <div class="col-md-8">
            <asp:TextBox ID="txtBPSystolic" runat="server" MaxLength="3" Columns="4" Enabled="false" Width="80" CssClass="form-control"></asp:TextBox>
            </div>
             <div class="col-md-4">
             <label class="smallerlabel">/</label>
            </div>
            </div>
            
             
            </div>
            
            <div class="col-md-3">
            <asp:TextBox ID="txtBPDiastolic" runat="server" MaxLength="3" Columns="4" Width="80" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">
            <span class="smallerlabel">(mm/Hg)</span>
            </div>
            </div>
             </div> 
 </div>
 <div class="row" id="trPregnant" visible="false" runat="server">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Pregnant:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input type="radio" id="PregnantYes" name="GrpPregnant" runat="server" class="checkbox-btn" /><label>Yes</label>
                                        <input type="radio" id="PregnantNo" name="GrpPregnant" value="No" runat="server" class="checkbox-btn" /><label>No</label>
                                        <input type="radio" id="PregnantUnknown" name="GrpPregnant" value="Unknown" runat="server" class="checkbox-btn" /><label>Unknown</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">EDD:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtEDD" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             
             </div>
              <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">onPMTCT:</label>
             </div>
              <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            <asp:CheckBox ID="ChkonPMTCT" runat="server" CssClass="checkbox-btn"/>
             </div>
             
 </div>
 <div class="row" id="tdFamilyPlanning" runat="server" visible="false">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblFP" runat="server">Family Planning:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlFamilyPanningStatus" onchange="fnfamilyplanning();" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <div class="divborder checkbox" id="divFamilyPlanningMethod" style="display: none">
                                            <asp:Panel ID="PnlFamilyPlanningMethod" runat="server" CssClass="checkbox-btn">
                                            </asp:Panel>
                                        </div>
             </div>
             
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblFS" runat="server">Functional Status:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlFunctionalStatus" onchange="fnfamilyplanning();" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblWHOStage" runat="server">WHO Clinical Stage:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="DDLWHOStage" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label" id="lblTBStatus" runat="server">TB Status:</label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlTBStatus" onchange="fnTBStatus();" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div> 
 </div>
 <div class="row" id="tbCardNo" style="display: none">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
           
             </div>
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
           
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
           
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
           
             </div>          
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblTBCardNo" runat="server">TB Card No:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtTBCardNo" runat="server" maxlength="11" size="11" class="form-control" style="width:99%"/>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-6">
 <%--left--%>
 <div class="row">
 <div class="col-md-12">
             <label class="control-label">Other OIs/Other Problems:</label>
             </div>
 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="divPotentialSideEffect" class="divborder margin10" style="margin-top: 10;
                                            margin-bottom: 10">
                                            <asp:Panel ID="PnlOIsOtherProblems" runat="server">
                                            </asp:Panel>
                                        </div>
             </div>
 </div>

 </div>
 <div class="col-md-6">
 <%--right--%>
 <div class="row">
 <div class="col-md-12">
             <label class="control-label">Noted Side Effects:</label>
             </div>
 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <div class="divborder margin20" style="margin-top: 10; margin-bottom: 10">
                                            <asp:Panel ID="PnlNotedSideEffects" runat="server">
                                            </asp:Panel>
                                        </div>
             </div>
 </div>
 </div>
 </div>
 <div class="row box-header"> 
 <div class="col-md-6">
 <%--left--%>
 <h5 class="box-title" id="H1">Pharmacy</h5>
 </div>
 <div class="col-md-6">
 <%--right--%>
              <label class="control-label">Use pharmacy form for INH dispensing and other medications dispensed.</label>
 
 </div>
  </div>
  <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblARVDrugs" runat="server">ARV Drugs Adherence: </label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlarvdrugadhere" onchange="fnARVDrug();" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group" id="divARVAdherence" style="display: none">
             <label class="control-label">Why Poor/Fair</label>
             <br />
             <div class="divborder checkbox">
                                                <asp:Panel ID="PnlARVwhypoorfair" runat="server">
                                                </asp:Panel>
                                            </div>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Cotrimoxazole Adherence:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlCotrimoxazoleAdhere" onchange="fnCotrimoxale();" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group" id="divCotrimoxazole" style="display: none">
             <label class="control-label">Why Poor/Fair</label>
             <br />
             <div class="divborder checkbox">
                                                <asp:Panel ID="PnlCotrimoxazolewhypoorfair" runat="server">
                                                </asp:Panel>
                                            </div>
             </div>             
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">INH Adherence</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="DDLINH" onchange="fnINH();" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group" id="divINH" style="display: none">
             <label class="control-label">Why Poor/Fair</label>
             <br />
             <div class="divborder checkbox">
                                                <asp:Panel ID="PnlINHWhyPoorFair" runat="server">
                                                </asp:Panel>
                                            </div>
             </div>             
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Substitutions/Interruptions:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlsubsituationInterruption" onchange="fnSubsituations();" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div>
             <div id="arvTherapyChange" style="display: none">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Change Regimen Reason:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlArvTherapyChangeCode" onchange="fnRegimenChange();" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div> 
             <div id="otherarvTherapyChangeCode" style="display: none">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Specify:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtarvTherapyChangeCodeOtherName" maxlength="20" size="10" name="arvTherapyChangeCodeOtherName" runat="server" style="width:99%" class="form-control" />
             </div>
             </div>
             </div>
             <div id="arvTherapyStop" style="display: none">
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label required">Date ART Ended:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtARTEndeddate" clientidmode="Static"
                                                       maxlength="11" size="10" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label required">Stop Regimen Reason:</label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlArvTherapyStopCode" onchange="fnStopReason();" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div> 
             <br />
             <div id="otherarvTherapyStopCode" style="display: none">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label required">Specify:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtarvTherapyStopCodeOtherName" maxlength="20" size="10" name="arvTherapyStopCodeOtherName" runat="server" style="width:99%" class="form-control" />
             </div>
             </div>
             </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="divAdultPharmacy" runat="server">
                                <asp:Button ID="btnpharmacy" Text="Prescribe Drugs" runat="server" OnClick="btnpharmacy_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="12%" style="text-align:left;"></asp:Button>
  <label class="fa fa-medkit" style="margin-left: -2%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
                            </div>
             </div>
             
 </div>
 <div class="row box-header"> 
 <div class="col-md-6">
 <%--left--%>
 <h5 class="box-title" id="H4">Laboratory Investigations</h5>
 </div>
 <div class="col-md-6">
 <%--right--%>
              <label class="control-label">Use lab order test button to order labs and document results.</label>
 
 </div>
  </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div id="divLaboratory" runat="server">
                                <asp:Button ID="btnLabratory" Text="Laboratory" runat="server" OnClick="btnLabratory_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="10%" style="text-align:left;" />
                                    <label class="fa fa-flask" style="margin-left: -2%; margin-right:2%; vertical-align: sub;
                                color: #fff;">
                            </label>
                            </div>
 </div>
 </div>
 <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Referrals and Consultations</h5>
         </div>             
            </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Referred To:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <asp:Panel ID="PnlReferredTo" runat="server">
                                    </asp:Panel>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label"># of Days Hospitalized:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtNumOfDaysHospitalized" runat="server" MaxLength="4" Columns="4" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
             
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <UcNextAppointment:UcNxtAppt ID="UserControlKNH_NextAppointment" runat="server" />
             </div>
            
            
 </div>
 <div class="row">
             <div class="col-md-6 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Signature:</label>             
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="DDSignature" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div>
            
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
 <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -2%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
 <asp:Button ID="btnDataQualityCheck" Text="Data Quality check" runat="server" OnClick="btnDataQualityCheck_Click" CssClass="btn btn-primary" Height="30px" Width="15%" style="text-align:left;" />
 <label class="glyphicon glyphicon-ok" style="margin-left: -2%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
 <asp:Button ID="btnClose" Text="Close" runat="server" OnClick="btnClose_Click" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;"/>
  <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right:2%; vertical-align: sub; color: #fff;"></label>
<asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" CssClass="btn btn-primary" Height="30px" Width="8%" style="text-align:left;" />
<label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub; color: #fff;"></label>
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
