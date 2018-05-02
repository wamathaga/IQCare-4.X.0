<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    Inherits="ClinicalForms_frmClinical_InitialFollowupVisit" CodeBehind="frmClinical_InitialFollowupVisit.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register TagPrefix="UcVitalSign" TagName="Uc1" Src="~/ClinicalForms/UserControl/UserControlKNH_VitalSigns.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtVisitDate').datepicker({ autoclose: true });
        $('#txtEDD').datepicker({ autoclose: true });
        $('#txtdatemiscarriage').datepicker({ autoclose: true });
        $('#txtdateinducedabortion').datepicker({ autoclose: true });
        $('#txttbstartdate').datepicker({ autoclose: true });
        $('#txtTBtreatmentNumber').datepicker({ autoclose: true });
        $('#txtARTEndeddate').datepicker({ autoclose: true });
        $('#txtdatenextappointment').datepicker({ autoclose: true });  
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('#divClinicalStatus').addClass("border center formbg");
        });

        function WindowPrint() {
            window.print();
        }
        function fnPageOpen(pageopen) {
            if (pageopen == "Pharmacy") {
                //window.open('../Pharmacy/frmPharmacyform.aspx?opento=ArtForm');
                window.open('../PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx?opento=ArtForm');
            }
            else if (pageopen == "Labratory") {
                window.open('../Laboratory/frm_Laboratory.aspx?opento=ArtForm');
            }
            else if (pageopen == "LabTest") {
                window.open('../Laboratory/frm_Laboratory.aspx?opento=ArtForm');
            }
        }

        function WindowHistory() {
            history.go(-1);
            return false;
        }
        function fnchange() {
            var e = document.getElementById("<%=ddlpregnancy.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;


            if (strtext == "Select") {
                hide('spanEDD');
                hide('miscarriage');
                hide('Abortion');
                document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>").disabled = false;
                document.getElementById("<%=txtdatemiscarriage.ClientID %>").value = "";
                document.getElementById("<%=txtdateinducedabortion.ClientID %>").value = "";
                document.getElementById("<%=txtEDD.ClientID %>").value = "";
                document.getElementById("<%=txtANCNumber.ClientID %>").value = "";
                document.getElementById("<%=chkrefpmtct.ClientID %>").checked = false;
            }
            else if (strtext == "Yes") {
                show('spanEDD');
                document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>").selectedIndex = "0";
                document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>").disabled = true;
                hide('divFamilyPlanningMethod');
                hide('notfamilyplanning');
                hide('miscarriage');
                hide('Abortion');
                document.getElementById("<%=txtdatemiscarriage.ClientID %>").value = "";
                document.getElementById("<%=txtdateinducedabortion.ClientID %>").value = "";
            }
            else if (strtext == "No") {
                hide('spanEDD');
                hide('miscarriage');
                hide('Abortion');
                document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>").disabled = false;
                document.getElementById("<%=txtEDD.ClientID %>").value = "";
                document.getElementById("<%=txtANCNumber.ClientID %>").value = "";
                document.getElementById("<%=chkrefpmtct.ClientID %>").checked = false;
            }
            else if (strtext == "No - Induced Abortion (ab)") {
                hide('spanEDD')
                hide('miscarriage')
                show('Abortion');
                document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>").disabled = false;
                document.getElementById("<%=txtEDD.ClientID %>").value = "";
                document.getElementById("<%=txtANCNumber.ClientID %>").value = "";
                document.getElementById("<%=chkrefpmtct.ClientID %>").checked = false;
            }
            else if (strtext == "No - Miscarriage (mc)") {
                show('miscarriage');
                hide('spanEDD');
                hide('Abortion');
                document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>").disabled = false;
                document.getElementById("<%=txtEDD.ClientID %>").value = "";
                document.getElementById("<%=txtANCNumber.ClientID %>").value = "";
                document.getElementById("<%=chkrefpmtct.ClientID %>").checked = false;
            }
        }
        function fnfamilyplanning() {
            var e = document.getElementById("<%=ddlFamilyPanningStatus.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;

            if (strtext == "Select") {
                hide('divFamilyPlanningMethod');
                hide('notfamilyplanning');

            }
            else if (strtext == "Currently on Family Planning" || strtext == "Wants Family Planning") {
                show('divFamilyPlanningMethod');
                hide('notfamilyplanning');
            }
            else if (strtext == "Not on Family Planning") {
                show('notfamilyplanning');
                hide('divFamilyPlanningMethod');
            }
        }
        function fnTBStatus() {
            var e = document.getElementById("<%=ddlTBStatus.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Select") {
                hide('tbstartdate');
                hide('tbtreatment');
                document.getElementById("<%=txttbstartdate.ClientID %>").value = "";
                document.getElementById("<%=txtTBtreatmentNumber.ClientID %>").value = "";

            }
            else if (strtext == "TB Rx") {
                show('tbstartdate');
                show('tbtreatment');
            }
            else {
                hide('tbstartdate');
                hide('tbtreatment');
                document.getElementById("<%=txttbstartdate.ClientID %>").value = "";
                document.getElementById("<%=txtTBtreatmentNumber.ClientID %>").value = "";
            }
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
            if (strtext == "Other reason (specify)") {
                show('otherarvTherapyChangeCode');

            }
            else {
                hide('otherarvTherapyChangeCode');

            }
        }
        function fnStopReason() {
            var e = document.getElementById("<%=ddlArvTherapyStopCode.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Other (specify)") {
                show('otherarvTherapyStopCode');

            }
            else {
                hide('otherarvTherapyStopCode');

            }
        }
        function fnarvdrugother() {
            var e = document.getElementById("<%=ddlwhypoorfair.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "Other (specify)") {
                show('divReasonARVDrugsother');

            }
            else {
                hide('divReasonARVDrugsother');

            }
        }
        //shows div
        function showdiv(divId) {
            if (document.layers) { document.layers[divId].visibility = 'show'; }

            else if (document.getElementById(divId)) {

                if ($('#' + divId).css('display') == 'none') {
                    document.getElementById(divId).style.display = 'block';
                }
            }
        }
        //hides div
        function hidediv(divId) {
            if (document.layers) document.layers[divId].visibility = 'hide';
            else if (document.getElementById) {
                if ($('#' + divId).css('display') == 'block') {
                    document.getElementById(divId).style.display = 'none';
                }
            }
        }
        //shows div
        function showtablediv(divId) {
            if (document.layers) { document.layers[divId].visibility = 'show'; }

            else if (document.getElementById(divId)) {
                if ($('#' + divId).css('display') == 'none') {
                    document.getElementById(divId).style.display = 'table-row';
                }
            }
        }
        //hides div
        function hidetablediv(divId) {
            if (document.layers) document.layers[divId].visibility = 'hide';
            else if (document.getElementById) {
                if ($('#' + divId).css('display') == 'table-row') {
                    $('#' + divId).hide();
                    //document.getElementById(divId).style.display = 'none';
                }
            }
        }
        function fnTreamentProgram() {
            var e = document.getElementById("<%=ddlvisittype.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;
            if (strtext == "TS - Treatment Supporter") {
                hidediv('divClinicalStatus');
                hidetablediv('divtreatmentPharm');
                hidediv('divtreatmentRef');
                hidediv('divtreatmentPwp');
            }
            else {
                showdiv('divClinicalStatus');
                showtablediv('divtreatmentPharm');
                showdiv('divtreatmentRef');
                showdiv('divtreatmentPwp');

            }
        }
        function fnARVDrug() {
            var e = document.getElementById("<%=ddlarvdrugadhere.ClientID%>");
            var strtext = e.options[e.selectedIndex].text;

            if (strtext == "G=Good") {
                document.getElementById("<%=ddlwhypoorfair.ClientID %>").disabled = true;
            }
            else {
                document.getElementById("<%=ddlwhypoorfair.ClientID %>").disabled = false;
            }
        }


        function CalcualteBMI(txtBMI, txtWeight, txtHeight) {
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
        </script>
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Initial and Follow up Visits</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required" id="lblVisitDate" runat="server">Visit Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
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
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-center">
                                             <label  class="control-label">If Scheduled:</label>&nbsp;&nbsp;&nbsp;&nbsp; <input id="chkifschedule" type="checkbox" runat="server" />
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Visit Type:</label>
             </div> 
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlvisittype" runat="server" onchange="fnTreamentProgram();" CssClass="form-control" Width="270"></asp:DropDownList>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">If drug pick only, Treatment Supporter Name:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtTreatmentSupporterName" Columns="23" MaxLength="23" runat="server" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Treatment Supporter Contact:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <asp:TextBox ID="txtTreatmentSupporterContact" Columns="23" MaxLength="23" runat="server" CssClass="form-control" Width="270"></asp:TextBox>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Duration Since:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">ART Start</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtARTStart" CssClass="form-control" MaxLength="3" Columns="2" ReadOnly="true" runat="server" Width="100"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <span class="smalllabel">Months</span>
             </div>     
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Starting Current Regimen</label>
             </div> 
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtStartingCurrentRegimen" CssClass="form-control" MaxLength="3" Columns="2" ReadOnly="true" runat="server" Width="100"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <span class="smalllabel">Months</span>
             </div>                  
    </div>
    <div class="row" id="divClinicalStatus" style="margin-left:5px;">
    
    <div class="row box-header">
         <div class="col-md-11 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Clinical Status</h5>
         </div>             
            </div>
    <div class="row">
     <div class="col-md-11 col-sm-12 col-xs-12 form-group">
    <UcVitalSign:Uc1 ID="idVitalSign" runat="server" />
    </div>
    </div>
    
    </div>
    <div class="row" id="tdPregnant" runat="server">
    <div class="row">
 <div class="col-xs-4">
 <%--left--%>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group text-center">
             <label id="lblPregnant" runat="server" class="control-label">Pregnancy:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlpregnancy" onchange="fnchange()" runat="server" CssClass="form-control" Width="250"></asp:DropDownList>
             </div>
                                  
    </div>
 </div>
 <div class="col-xs-8">
<%--right--%>
 <span id="spanEDD" style="display: none">
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
 <label id="lblEDD" class="control-label" for="EDD">EDD:</label>
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
 <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
 <label id="lblanc" for="anc" class="control-label">ANC No:</label>
 </div>
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtANCNumber" runat="server" MaxLength="4" Columns="13" CssClass="form-control" Width="100"></asp:TextBox>
 </div>
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-right">
 <label id="lblrefpmtct" class="margin20">Referred to PMTCT (ac):</label>
 </div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
 <asp:CheckBox ID="chkrefpmtct" runat="server" />
 </div>
 </div>
 </span>
 <span id="miscarriage" style="display: none">
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group text-right">
 <label id="Label2" class="control-label">Date of Miscarriage:</label>
 </div>
  <div class="col-md-4 col-sm-12 col-xs-12 form-group text-right">
  <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtdatemiscarriage" clientidmode="Static"
                                                        maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
 </div>
 </div>
 </span>
 <span id="Abortion" style="display: none">
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group text-right">
 <label id="Label3" class="control-label">Date of Induced Abortion:</label>
 </div>
 <div class="col-md-4 col-sm-12 col-xs-12 form-group text-right">
 <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtdateinducedabortion" clientidmode="Static"
                                                       maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
 </div>
 </div>
 </span>
 </div>

             
                                   
    </div>

    </div>
   <div class="row" id="tdFamilyPlanning" runat="server">
 <div class="row">
 <div class="col-xs-4">
 <%--left--%>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group text-center">
 <label class="control-label">Family Planning:</label>
 </div>
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
  <asp:DropDownList ID="ddlFamilyPanningStatus" onchange="fnfamilyplanning();" runat="server" CssClass="form-control" Width="250"></asp:DropDownList>
 </div>
 </div> 
 </div>
 <div class="col-xs-4">
 <%--right--%>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="divborder checkbox" id="divFamilyPlanningMethod" style="display: none">
 <asp:Panel ID="PnlFamilyPlanningMethod" runat="server" Style="margin-left: 5.5%;"></asp:Panel>
  </div>
 </div>
 </div>
 <span id="notfamilyplanning" style="display: none">
 <div class="row">
 <div class="col-md-8 col-sm-12 col-xs-12 form-group">
 <label class="control-label">Reason Not on Family Planning:</label>
 </div>
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
 <asp:DropDownList ID="ddlnotfamilyplanning" runat="server" CssClass="form-control" Width="250"></asp:DropDownList>
 </div>
 </div>
 </span>
 </div>
 </div>


   </div>
   <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label id="lblTBStatus" runat="server" class="control-label">TB Status:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group" style="margin-left:22px;">
             <asp:DropDownList ID="ddlTBStatus" onchange="fnTBStatus();" runat="server" CssClass="form-control"></asp:DropDownList>
             </div>
             <span id="tbstartdate" style="display: none">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">TB Treatment Start Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txttbstartdate" clientidmode="Static"
                                                      maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
                                            </div> 
             </span>
             <span id="tbtreatment" style="display: none">
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">TB Treatment #:</label>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtTBtreatmentNumber" clientidmode="Static"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>  </div>    
             </span>                  
    </div>
    <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Potential Side Effects:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label">New OIs, Other Problems:</label>
             </div>
                                
    </div>
    <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <div id="divPotentialSideEffect" class="divborder margin10" style="margin-top: 10;
                                            margin-bottom: 10">
                                            <asp:Panel ID="PnlPotentialSideEffect" runat="server">
                                            </asp:Panel>
                                        </div>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div class="divborder margin20" style="margin-top: 10; margin-bottom: 10">
                                            <asp:Panel ID="PnlNewOIsProblemsOther" runat="server">
                                            </asp:Panel>
                                        </div>
             </div>
                                
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
 <label class="margin20" id="lblNutritionalProblems" style="visibility: hidden" runat="server">Nutritional Problems:</label>
 </div>
 <div class="col-md-10 col-sm-12 col-xs-12 form-group">
 <asp:DropDownList ID="ddlNutritionalProblems" Style="visibility: hidden" runat="server" CssClass="form-control"></asp:DropDownList>
 </div>
 </div>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="lblWHOStage" class="control-label" runat="server">WHO Stage:</label>
             </div>
             <div class="col-md-10 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlWHOStage" runat="server" CssClass="form-control" Width="100"></asp:DropDownList>
             </div>
                                   
    </div>
    <div class="row box-header">
         <div class="col-md-6 col-sm-12 col-xs-12 form-group">
         <h5 id="H1" class="box-title">Pharmacy</h5>
         </div>  
         <div class="col-md-6 col-sm-12 col-xs-12 form-group">
         <label class="control-label">Use pharmacy form for INH dispensing and other medications dispensed.</label>
         </div>              
            </div>
            <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">CTX: Adherence:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlCotrimoxazoleAdhere" runat="server" CssClass="form-control" Width="120"></asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <label class="margin10" id="lblARVDrugsAdhere" runat="server">ARV:</label><label class="control-label">Adherence:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlarvdrugadhere" onchange="fnARVDrug();" runat="server" CssClass="form-control" Width="120"> </asp:DropDownList>
             </div>  
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-center">
             <label  class="control-label">Why Poor/Fair:</label>
             </div>  
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlwhypoorfair" onchange="fnarvdrugother();" runat="server" CssClass="form-control" Width="275"></asp:DropDownList>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Substitutions/Interruptions:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlsubsituationInterruption" onchange="fnSubsituations();" runat="server" CssClass="form-control"></asp:DropDownList>
             </div>
             
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             
             </div> 
             <div id="divReasonARVDrugsother" style="display: none">
              <div class="col-md-2 col-sm-12 col-xs-12 form-group text-center">
             <label  class="control-label">Other Reason:</label>
             </div> 
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtReasonARVDrugsPoorFairOther" runat="server" MaxLength="10" Columns="10" CssClass="form-control" Width="275"></asp:TextBox>
             </div>   
             </div>                   
    </div>
    <div class="row" id="arvTherapyChange" style="display: none">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Change Regimen Reason:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlArvTherapyChangeCode" onchange="fnRegimenChange();" runat="server" CssClass="form-control"></asp:DropDownList>
             </div>
             <div id="otherarvTherapyChangeCode" style="display: none">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required" for="arvTherapyChangeCodeOtherName">Specify:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <input id="txtarvTherapyChangeCodeOtherName" maxlength="20" size="10" name="arvTherapyChangeCodeOtherName" runat="server" class="form-control"/>
             </div>  
             </div>                    
    </div>
     <div class="row" id="arvTherapyStop" style="display: none">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Date ART Ended</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtARTEndeddate" clientidmode="Static"
                                                       maxlength="11" size="10" name="txtARTEndeddate" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
                                             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label required">Stop Regimen Reason:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlArvTherapyStopCode" onchange="fnStopReason();" runat="server" CssClass="form-control" Width="250">
                                            </asp:DropDownList>
             </div>                      
    </div>
    <div class="row" id="otherarvTherapyStopCode" style="display: none">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
 </div>
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
 </div>
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
  <label class="control-label required" for="arvTherapyStopCodeOtherName">Specify:</label>
 </div>
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
 <input id="txtarvTherapyStopCodeOtherName" maxlength="20" size="10" name="arvTherapyStopCodeOtherName" runat="server" class="form-control" />
 </div>
 </div>
<div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="divAdultPharmacy" runat="server">
                                <asp:Button ID="btnpharmacy" Text="Prescribe Drugs" runat="server" OnClick="btnpharmacy_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="16%" Style="text-align: left;" /><label
                                        class="glyphicon glyphicon-list-alt" style="margin-left: -3%; vertical-align: sub;
                                        color: #fff; margin-right: 2%;"></label>
                            </div>
             </div>
                                  
    </div>
    <div class="row box-header">
         <div class="col-md-6 col-sm-12 col-xs-12 form-group">
         <h5 id="H5" class="box-title">Laboratory Investigations</h5>
         </div>  
         <div class="col-md-6 col-sm-12 col-xs-12 form-group">
         <label class="control-label">Use lab order test button to order labs and document results.</label>
         </div>              
            </div>
            <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group ">
             <div id="divLaboratory" runat="server">
                                <asp:Button ID="btnLabratory" Text="Laboratory" runat="server" OnClick="btnLabratory_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="12%" Style="text-align: left;" /><label
                                        class="glyphicon glyphicon-list" style="margin-left: -3%; vertical-align: sub;
                                        color: #fff; margin-right: 2%;"></label>
                            </div>
                            <div id="divLabOrderTest" runat="server">
                                <asp:Button ID="btnOrderLabTest" Text="Order Lab Tests" runat="server" OnClick="btnOrderLabTest_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="16%" Style="text-align: left;" /><label
                                        class="glyphicon glyphicon-qrcode" style="margin-left: -3%; vertical-align: sub;
                                        color: #fff; margin-right: 2%;"></label>
                            </div>
             </div>
             
                               
    </div>
    <div id="divtreatmentRef">
    <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Referrals and Consultations</h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Referred To:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div>
                                    <asp:Panel ID="PnlReferredTo" runat="server">
                                    </asp:Panel>
                                </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label"># of Days Hospitalized:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtNumOfDaysHospitalized" runat="server" MaxLength="4" Columns="4" CssClass="form-control" Width="100"></asp:TextBox>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Nutritional Support:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlNutritionalSupport" runat="server" CssClass="form-control" Width="300"></asp:DropDownList>
             </div>
             <div id="tdInfantFeedingPractice" style="display: none" runat="server">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Infant Feeding Practice:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlInfantFeedingPractice" runat="server" CssClass="form-control" Width="300"></asp:DropDownList>
             </div> 
             </div>                     
    </div>
            </div>
            <div id="divtreatmentPwp">
    <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title" id="H4">Positive Prevention</h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label">At Risk Population:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label">At Risk Population Services:</label>
             </div>                                 
    </div>
     <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div>
                                    <asp:Panel ID="pnlriskpopulation" runat="server">
                                    </asp:Panel>
                                </div>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div>
                                    <asp:Panel ID="pnlriskpopulationservice" runat="server">
                                    </asp:Panel>
                                </div>
             </div>                                 
    </div>
    <div class="row">
    <div class="col-xs-6">
    <%--left--%>
    <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label class="control-label">Prevention with positives (PwP):</label>
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <div>
 <asp:Panel ID="pnlprewithpositive" runat="server"></asp:Panel>
 </div>
 </div>
 </div>

    </div>
    <div class="col-xs-6">
    <%--right--%>
    <div id="divNextAppDate">
    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%" Wrap="true"></asp:Panel>
 </div>
 </div>
    <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblNextAppointment" class="control-label" runat="server">Date of Next Appointment:</label>
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtdatenextappointment" clientidmode="Static"
                                                        maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
 </div>
 </div>
 <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label class="control-label">Attending Clinician:</label> 
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:DropDownList ID="ddlattendingclinician" runat="server" CssClass="form-control" Width="200"></asp:DropDownList>
 </div>
 </div>

 </div>
    </div>
    </div>
     

    </div>
    <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                Height="30px" Width="8.5%" Style="text-align: left;" /><label class="glyphicon glyphicon-floppy-disk"
                                    style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                            <asp:Button ID="btnDataQualityCheck" Text="Data Quality check" runat="server" OnClick="btnDataQualityCheck_Click"
                                CssClass="btn btn-primary" Height="30px" Width="16%" Style="text-align: left;margin-right:7px;" /><label
                                    class="glyphicon glyphicon-floppy-saved" style="margin-left: -3.5%; vertical-align: sub;
                                    color: #fff; margin-right: 2%;"></label>
                            <asp:Button ID="btnClose" Text="Close" runat="server" OnClick="btnClose_Click" CssClass="btn btn-primary"
                                Height="30px" Width="8.5%" Style="text-align: left;" /><label class="glyphicon glyphicon-remove"
                                    style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()"
                                CssClass="btn btn-primary" Height="30px" Width="8.5%" Style="text-align: left;" /><label
                                    class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                    color: #fff; margin-right: 2%;"></label>
             </div>
                               
    </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>

    <div class="container-fluid">
    
        <div id="divtreatmentPwp3333" class="border center formbg">
            <br />
            
            
        </div>
        
        
    </div>
</asp:Content>
