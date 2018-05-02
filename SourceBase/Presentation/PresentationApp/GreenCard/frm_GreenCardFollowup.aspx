<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_GreenCardFollowup.aspx.cs" Inherits="PresentationApp.GreenCard.frm_GreenCardFollowup" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/GreenCard/UserControls/UserControl_VitalSigns.ascx" TagName="ucVitals" TagPrefix="uc2" %>
<%@ Register Src="~/GreenCard/UserControls/UC_Vitals.ascx" TagName="ucVitalSigns" TagPrefix="uc4" %>
<%@ Register Src="~/GreenCard/UserControls/UC_ViralLoadTracker.ascx" TagName="ucViralTrack" TagPrefix="uc5" %>
<%@ Register Src="~/GreenCard/UserControls/UC_Family.ascx" TagName="ucFamily" TagPrefix="uc6" %>
<%@ Register Src="~/GreenCard/UserControls/UC_ChronicCondition.ascx" TagName="ucChronic" TagPrefix="uc7" %>
<%@ Register Src="~/GreenCard/UserControls/UC_DrugChanges.ascx" TagName="ucDrug" TagPrefix="uc8" %>
<%@ Register Src="~/GreenCard/UserControls/UC_PhysicalExamination.ascx" TagName="ucPhyExam" TagPrefix="uc9" %>
<%@ Register Src="~/GreenCard/UserControls/UC_TBScreening.ascx" TagName="ucTBScre" TagPrefix="uc10" %> 
<%@ Register Src="~/GreenCard/UserControls/UC_WHOStaging.ascx" TagName="ucWHO" TagPrefix="uc11" %>
<%@ Register Src="~/GreenCard/UserControls/UC_Signature.ascx" TagName="UC_Signature" TagPrefix="uc12" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="cc1" Namespace="AjaxControlToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server" id="pageHead">
<title title="Green Card Form"></title>
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="../bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="../bootstrap/css/bootstrap-switch.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
    <!-- daterange picker -->
    <link rel="stylesheet" href="../bootstrap/plugins/daterangepicker/daterangepicker.css">
    <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="../bootstrap/plugins/datepicker/datepicker3.css">
    <!-- iCheck for checkboxes and radio inputs -->
    <link rel="stylesheet" href="../bootstrap/plugins/iCheck/all.css">
   
    <!-- Select2 -->
    <link rel="stylesheet" href="../bootstrap/plugins/select2/select2.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="../bootstrap/dist/css/AdminLTE.min.css">
    <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="../bootstrap/dist/css/skins/_all-skins.min.css">
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <!-- page -->
    <script src="images/page.js"></script>
    <!-- jQuery 2.2.3 -->
    <script src="../bootstrap/plugins/jQuery/jquery-2.2.3.min.js"></script>
    <!-- Bootstrap 3.3.6 -->
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <!-- DataTables -->
    <script src="../bootstrap/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="../bootstrap/plugins/datatables/dataTables.bootstrap.min.js"></script>
    <!-- Bootstrap Switch -->
    <script src="../bootstrap/js/bootstrap-switch.js"></script>
    <script src="../bootstrap/js/highlight.js"></script>
    <%--<script src="bootstrap/js/main.js"></script>--%>
    <!-- Select2 -->
    <script src="../bootstrap/plugins/select2/select2.full.min.js"></script>
    <!-- InputMask -->
    <script src="../bootstrap/plugins/input-mask/jquery.inputmask.js"></script>
    <script src="../bootstrap/plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
    <script src="../bootstrap/plugins/input-mask/jquery.inputmask.extensions.js"></script>
    <!-- date-range-picker -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.2/moment.min.js"></script>
    <script src="../bootstrap/plugins/daterangepicker/daterangepicker.js"></script>
    <!-- bootstrap datepicker -->
    <script src="../bootstrap/plugins/datepicker/bootstrap-datepicker.js"></script>
    <!-- bootstrap color picker -->
    <script src="../bootstrap/plugins/colorpicker/bootstrap-colorpicker.min.js"></script>
    <!-- iCheck 1.0.1 -->
    <script src="../bootstrap/plugins/iCheck/icheck.js"></script>
    <!-- AdminLTE App -->
    <script src="../bootstrap/dist/js/app.min.js"></script>
    <!-- AdminLTE for demo purposes -->
    <script src="../bootstrap/dist/js/demo.js"></script>
    <!-- Masked Textbox -->
    <script src="images/jquery.maskedinput.js"></script>
    <script language="javascript" type="text/javascript" src="<%# ResolveUrl("~/Incl/jsCommon.js") %>"></script>
    <style type="text/css">
        .popover
        {
            max-width: 100%; /* Max Width of the popover (depending on the container!) */
        }
        .panel-group .panel
        {
            border-radius: 0;
            box-shadow: none;
            border-color: #EEEEEE;
        }
        
        .panel-default > .panel-heading
        {
            padding: 0;
            border-radius: 0;
            color: #212121;
            background-color: #FAFAFA;
            border-color: #EEEEEE;
        }
        
        .panel-title
        {
            font-size: 14px;
        }
        
        .panel-title > a
        {
            display: block;
            padding: 15px;
            text-decoration: none;
        }
        
        .more-less
        {
            float: right;
            color: #212121;
        }
        
        .panel-default > .panel-heading + .panel-collapse > .panel-body
        {
            border-top-color: #EEEEEE;
        }
        .label-normal
        {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: normal;
        }
        .label-right
        {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: bold;
            float: right;
        }
        .label-left
        {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: bold;
            float: left;
        }
        .nav-tabs-custom > .nav-tabs > li.active
        {
            border-top-color: #fff;
        }
        .nav-tabs-custom > .nav-tabs > li.active > a, .nav-tabs-custom > .nav-tabs > li.active:hover > a
        {
            background-color: #3c8dbc;
            color: #fff;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            //Date picker
            $("#txtbaseviraldate").datepicker({
                autoclose: true
            });
            //Date picker
            $("#txtdeathdate").datepicker({
                autoclose: true
            });
            //Date picker
            $("#txtLastUsed").datepicker({
                autoclose: true
            });
            //Date picker
            $("#txtVisitDate").datepicker({
                autoclose: true
            });

            //Date picker
            $("#txtTransferInDate").datepicker({
                autoclose: true
            });
            //Date picker
            $("#txtDateARTStarted").datepicker({
                autoclose: true
            });
            //Date picker
            $("#txttransferdate").datepicker({
                autoclose: true
            });
            //Date picker
            $("#txtHIVConfirmHIVPosDate").datepicker({
                autoclose: true
            });
            //Date picker
            $("#txtEnrolledinHIVCare").datepicker({
                autoclose: true
            });
            //Date picker
            $("#txtsexDisDate").datepicker({
                autoclose: true
            });
            //Date picker
            $("#partDisDate").datepicker({
                autoclose: true
            });
            //Date picker
            $("#txtFullDisDate").datepicker({
                autoclose: true
            });

            //Date picker
            $("#txtINHStartDate").datepicker({
                autoclose: true
            });
            //Date picker
            $("#txtINHCompDate").datepicker({
                autoclose: true
            });
            //Date picker
            $("#txtDateImmunised").datepicker({
                autoclose: true
            });
            
            //Date picker
            $("#txtARTEndeddate").datepicker({
                autoclose: true
            });

            $("#allergybtn").tooltip({

            });
            $("#drugbtn").tooltip({

            });
            $("#familybtn").tooltip({

            });
            $("#viralbtn").tooltip({

            });
            $("#txtHaemophilus").mask("9");
            $("#txtPertussis").mask("9");
            $("#txtTetanus").mask("9");
            $("#txtHepatitis").mask("9");
            $("#txtDiphtheria").mask("9");
            $("#txtFlu").mask("9");
            $("#txtHBV").mask("9");
            $("#txtBCG").mask("9");
            $("#txtPCV").mask("9");
            $("#txtPenta").mask("9");
            $("#txtIPV").mask("9");
            $("#txtMeasles").mask("9");
            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
            $('input[type="checkbox"],input[type="radio"]').each(function () {
                $(this).addClass("minimal");
            });
            $('select').each(function () {
                $(this).addClass("form-control select2");
            });

            //iCheck for checkbox and radio inputs
            $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue'
            });

            $("[data-toggle='popover']").each(function (index, element) {
                var contentElementId = $(element).data().target;
                var contentHtml = $(contentElementId).html();
                $(element).popover({
                    content: contentHtml
                });
            });
            function toggleIcon(e) {
                $(e.target)
            .prev('.panel-heading')
            .find(".more-less")
            .toggleClass('glyphicon-plus glyphicon-minus');
            }
            $('.panel-group').on('hidden.bs.collapse', toggleIcon);
            $('.panel-group').on('shown.bs.collapse', toggleIcon);

            $("#ddlfwupvisitby").select2();
            $(".checkbox").on("change", function () {

                if ($(this).is(":checked")) {
                    alert('a');
                    $(".selectAll").prop('checked', false);
                }
            });

            var GV = document.getElementById("<%= gvPresentingComplaints.ClientID %>");
            var inputList = GV.getElementsByTagName("input");
            var arrayOfCheckBoxLabels = GV.getElementsByTagName("label");
            if (arrayOfCheckBoxLabels[0].innerText == "None") {
                if (GV.rows.length > 0) {
                    var lbl = GV.rows[1].getElementsByTagName('label');
                    var txt = GV.rows[1].getElementsByTagName('text');
                    if (lbl[0].innerText == "None") {
                        lbl[0].style.marginLeft = "-2%";
                    }
                }
            }
        });
        function togglePC(strcblcontrolId) {
            var GV = document.getElementById("<%= gvPresentingComplaints.ClientID %>");
            var inputList = GV.getElementsByTagName("input");
            var arrayOfCheckBoxLabels = GV.getElementsByTagName("label");
            if ((inputList[0].checked == true) && (arrayOfCheckBoxLabels[0].innerText == "None")) {
                if (GV.rows.length > 0) {
                    for (var i = 1; i < GV.rows.length; i++) {
                        var inputs = GV.rows[i].getElementsByTagName('input');
                        var lbl = GV.rows[i].getElementsByTagName('label');
                        var txt = GV.rows[i].getElementsByTagName('text');
                        //alert(inputList[0].Id);
                        if (lbl[0].innerText != "None") {
                            inputs[0].checked = false;
                            var txtbx = GV.rows[i].cells[1].children[0];
                            txtbx.disabled = true;
                            inputs[0].disabled = true;

                        }
                        else if (lbl[0].innerText == "None") {
                            var txtbx = GV.rows[i].cells[1].children[0];
                            //txtbx.disabled = false;

                        }
                    }
                }
            }
            else if ((inputList[0].checked == true) && (arrayOfCheckBoxLabels[0].innerText != "None")) {

                if (GV.rows.length > 0) {
                    for (var i = 1; i < GV.rows.length; i++) {
                        var inputs = GV.rows[i].getElementsByTagName('input');
                        var lbl = GV.rows[i].getElementsByTagName('label');
                        if (lbl[0].innerText == "None") {
                            inputs[0].checked = false;
                            var txtbx = GV.rows[i].cells[1].children[0];
                            txtbx.disabled = true;
                        }
                        else {
                            var txtbx = GV.rows[i].cells[1].children[0];
                            txtbx.disabled = false;
                        }
                    }
                }
            }
            else if ((inputList[0].checked == false) && (arrayOfCheckBoxLabels[0].innerText == "None")) {
                if (GV.rows.length > 0) {
                    for (var i = 1; i < GV.rows.length; i++) {
                        var inputs = GV.rows[i].getElementsByTagName('input');
                        var lbl = GV.rows[i].getElementsByTagName('label');

                        if (lbl[0].innerText == "None") {
                            //inputs[0].checked = false;
                            var txtbx = GV.rows[i].cells[1].children[0];
                            txtbx.disabled = true;
                            txtbx.value = '';
                        }
                        else {
                            var txtbx = GV.rows[i].cells[1].children[0];
                            txtbx.disabled = false;
                            //txtbx.value = '';
                            inputs[0].disabled = false;
                        }
                    }
                }
            }
        }
        function EnableDis(a) {
            var rdoVal = a;
            if (rdoVal.value == "Y") {
                document.getElementById('<%=ddlpurpose.ClientID%>').disabled = false;
                document.getElementById('<%=txtRegimen.ClientID%>').disabled = false;
                document.getElementById('<%=txtLastUsed.ClientID%>').disabled = false;
                document.getElementById('<%=btnRegimen.ClientID%>').disabled = false;
                document.getElementById('<%=btnAddPriorART.ClientID%>').disabled = false;
                document.getElementById('Img1').disabled = false;
            }
            else {
                document.getElementById('<%=ddlpurpose.ClientID%>').disabled = true;
                document.getElementById('<%=txtRegimen.ClientID%>').disabled = true;
                document.getElementById('<%=txtLastUsed.ClientID%>').disabled = true;
                document.getElementById('<%=btnRegimen.ClientID%>').disabled = true;
                document.getElementById('<%=btnAddPriorART.ClientID%>').disabled = true;
                document.getElementById('Img1').disabled = true;
            }
        }
        function setMonthYear() {
            var artTransferDate = document.getElementById("<%=txtDateARTStarted.ClientID%>").value;
            if (artTransferDate != "") {
                var arrMonthDate = artTransferDate.split('-');
                if (arrMonthDate[1] != "")
                    document.getElementById("<%=txtcohortmnth.ClientID%>").value = arrMonthDate[1];
                else
                    document.getElementById("<%=txtcohortmnth.ClientID%>").value = "";

                if (arrMonthDate[2] != "")
                    document.getElementById("<%=txtcohortyear.ClientID%>").value = arrMonthDate[2];
                else
                    document.getElementById("<%=txtcohortyear.ClientID%>").value = "";
            }
        }
        function GetControl() {
            document.forms[0].submit();
        }
        function CalcualteBMI(txtWeight, txtHeight, txtBMI) {
            var weight = document.getElementById(txtWeight).value;
            var height = document.getElementById(txtHeight).value;
            if (weight == "" || height == "") {
                weight = 0;
                height = 0;
                document.getElementById(txtBMI).value = "";
            }
            else {
                var BMI = weight / ((height / 100) * (height / 100));
                BMI = BMI.toFixed(2);
                document.getElementById(txtBMI).value = BMI;

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
        //shows div
        function show(divId) {
            if (document.getElementById(divId)) {
                $('#' + divId).removeClass('hidden-md hidden-lg');
                $('#' + divId).addClass('visible-md visible-lg');
            }
        }
        //hides div
        function hide(divId) {
            if (document.getElementById(divId)) {
                alert(divId);
                $('#' + divId).removeClass('visible-md visible-lg');
                $('#' + divId).addClass('hidden-md hidden-lg');
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
                        show(divID);
                    }

                }
                else {
                    if (arrayOfCheckBoxLabels[i].innerText == txt) {
                        hide(divID);
                    }
                }
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
        function DisableTabbyId(tabId) {
            if (tabId == 'int') {
                $('#int a[href="#tab_1"]').siblings().removeClass('active');
                $('#follow a[href="#tab_2"]').tab('show');
                $('#follow a[href="#tab_2"]').addClass('active');
            }

            else if (tabId == 'follow') {
                $('#follow a[href="#tab_2"]').siblings().removeClass('active');
                $('#int a[href="#tab_1"]').tab('show');
                $('#int a[href="#tab_1"]').addClass('active');
            }
        }
        function ShowConfirmYesNo(tabid) {
            AsyncConfirmYesNo(
                    "Yes & No Confirmation Box",
                    tabid + " saved successfully. Do you want close it?",
                    MyYesFunction(tabid),
                    MyNoFunction(tabid)
                );
        }
        function MyYesFunction(tabid) {
            //alert("Time to get off your workstation!");
            //$("#lblTestResult").html("You are hungry");
            //alert(tabid);
            $('.nav-tabs a[href="#'+tabid+'"]').tab('show');
        }
        function MyNoFunction(tabid) {
            //alert(tabid);
//            alert("Well... just contine working.");
            //            $("#lblTestResult").html("You are not hungry");
            $('.nav-tabs a[href="#' + tabid + '"]').tab('show');
        }
        function AsyncConfirmYesNo(title, msg, yesFn, noFn) {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('show');
            $("#lblTitleConfirmYesNo").html(title);
            $("#lblMsgConfirmYesNo").html(msg);
            $("#btnYesConfirmYesNo").off('click').click(function () {
                yesFn();
                //$confirm.modal("hide");
            });
            $("#btnNoConfirmYesNo").off('click').click(function () {
                noFn();
                //$confirm.modal("hide");
            });
        }
    </script>
</head>
<body class="hold-transition skin-blue layout-top-nav">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="masterScriptManager" runat="server" EnablePageMethods="true"
        EnablePartialRendering="true" ScriptMode="Auto">
    </asp:ScriptManager>
    <div class="wrapper">
        <div class="content-wrapper">
            <!-- Main content -->
            <section class="content">
      <!-- Default box -->
      <div>
        
        <div class="box-body">
                <div class="row">
        <div class="col-xs-12">
          <div class="box box-primary box-solid">
            <div class="box-header with-border">
              <h3 class="box-title">Green Card Encounter Form</h3>
              
              <!-- /.box-tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body">
            <div class="row">
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            <asp:Panel ID="divGCError" runat="server" Style="padding: 5px;float:left" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                            HorizontalAlign="Left">
                            <asp:Label CssClass="label-right" ID="lblGCError" runat="server" Style="font-weight: bold; color: #800000;
                                padding-left: 5px"></asp:Label>
                        </asp:Panel>
            </div>
            </div>
            <div class="row">
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label5" runat="server">
                                        Visit Date:</asp:Label>
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                     </div>
                        <asp:TextBox ID="txtVisitDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                        </div>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label63" runat="server">
                                        Scheduled:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input id="rdoshdulyes" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="Y" name="ScheduledVisit" />
                                <label for="y">
                                    Yes</label>
                                <input id="rdoshdulno" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="N" name="ScheduledVisit" />
                                <label for="n">
                                    No</label>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>  
                         <div class="col-md-1 col-sm-12 col-xs-12 form-group" >
                        <asp:Label CssClass="label-right" style="padding-left: 10px;" id="Label8" runat="server">
                                        Visit Type:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group" style="float:left">
                        <asp:DropDownList ID="ddlvisittype" class="form-control" Width="130px" 
                                runat="server" onselectedindexchanged="ddlvisittype_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
        </div>
             <div class="col-md-12 col-sm-12 col-xs-12">

<!-- Custom Tabs -->
          <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
              <li class="active" id="#int"><a href="#tab_1" data-toggle="tab">Patient Initial Visit</a></li>
              <li id="#follow"><a href="#tab_2" data-toggle="tab">Patient Follow up Visit</a></li>              
            </ul>
            <div class="tab-content">
              <div class="tab-pane active" id="tab_1">
                 <table class="table" style="padding-left:10px;width:100%;">
                  <tr>
                  <td style="width:100%;">
                  <div class="box box-default box-solid">
                        <div class="box-header with-border">
                          <h3 class="box-title">Patient Profile</h3>
                          <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                            </button>
                          </div>                      
                        </div> 
                        <div class="box-body">
                        <div class="form-group" style="padding-top:20px">
                                                <div  class="row">
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label13" runat="server">
                                                                Patient Name:</asp:Label>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label style="padding-left: 10px" id="lblPatName" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label14" runat="server">
                                                                Unique patient number:</asp:Label>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label style="padding-left: 10px" id="lbluniquename" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label17" runat="server">
                                                               Sex:</asp:Label>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label style="padding-left: 10px" id="lblsex" runat="server"></asp:Label>
                                                </div>
                                                </div>
                                                
                                                <div  class="row">
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label15" runat="server">
                                                                Date of Birth:</asp:Label>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label  style="padding-left: 10px" id="lblDOB" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" runat="server" style="padding-left: 10px" id="Label16" >
                                                                Age:</asp:Label>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label style="padding-left: 10px" id="lblAge" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label18" runat="server">
                                                               Marital Status:</asp:Label>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label style="padding-left: 10px" id="lblmaritalstatus" runat="server"></asp:Label>
                                                </div> 
                                                </div>
                                                
                                                </div>
                        <div class="box box-default collapsed-box box-solid">
                            <div class="box-header with-border" style="background-color:#eeeeee;">
				                        <h3 class="box-title">
						                        Contact Details
					                        </h3>
                                             <div class="box-tools pull-right">
                                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i>
                                                </button>
                                                </div>
				                        
			                        </div>			                        
				                        <div class="box-body">
					                          <div class="form-group" style="padding-top:20px">
                                                <div  class="row">
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label4" runat="server">
                                                                Postal Address:</asp:Label>
                                                </div>
                                                <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                <asp:TextBox ID="txtpostaladdress" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            
                                                </div>
                                                <div  class="row">
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label10" runat="server">
                                                                Country:</asp:Label>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                <asp:TextBox ID="txtpatcountry" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label11" runat="server" class="control-label">
                                                                Sub Country:</asp:Label>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                <asp:TextBox ID="txtpatSubCountry" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                </div>
                                                <div  class="row">
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label12" runat="server">
                                                               Sub location:</asp:Label>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                <asp:TextBox ID="txtPatSubLocation" runat="server" class="form-control"></asp:TextBox>
                                                </div>                                            
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label7" runat="server">
                                                               Landmark:</asp:Label>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                <asp:TextBox ID="txtPatLandmark" runat="server" class="form-control"></asp:TextBox>
                                                </div> 
                                                </div>
                                                <div  class="row">
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label19" runat="server">
                                                               Nearest HC:</asp:Label>
                                                </div>
                                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                <asp:TextBox ID="txtNearHC" runat="server" class="form-control"></asp:TextBox>
                                                </div>                                            
                                            
                                                </div>
                                                </div>
				                        </div>
			                       
		                        </div>

		                        <div class="box box-default collapsed-box box-solid">
                                    <div class="box-header with-border" style="background-color:#eeeeee;">
				                        <h3 class="box-title">
						                        Treament Supporter/Parent/Guardian
					                        
				                        </h3>
                                        <div class="box-tools pull-right">
                                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i>
                                                </button>
                                                </div>
			                        </div>
			                       <div class="box-body">				                        
					                        <%--<div class="form-group" style="padding-top:20px">--%>
                                                <div  class="row">
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label20" runat="server">
                                                                Name:</asp:Label>
                                                </div>
                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                <asp:TextBox ID="txtGuardName" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label23" runat="server">
                                                                Relaiotnship:</asp:Label>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:DropDownList ID="ddlGuardRelationship" class="form-control" Width="120px" runat="server" >
                                                </asp:DropDownList>                                            
                                                </div>
                                                </div>
                                                <div  class="row">
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label21" runat="server">
                                                                Postal Address:</asp:Label>
                                                </div>
                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                <asp:TextBox ID="txtGuardAddress" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label22" runat="server" class="control-label">
                                                                Tel No:</asp:Label>
                                                </div>
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <asp:TextBox ID="txtGuardTel" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                </div>
				                        </div>
			                        <%--</div>--%>
		                        
                        </div>
                        </div>
                        </div>
                        </td>
                        </tr>
                <tr><!--Patient Source-->
              <td style="width:100%;">
              <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">Patient Source (Place of first diagnosis) and Transfer Status</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                    <div class="box-body">
                   <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label style="padding-left: 10px" id="Label9" CssClass="label-right"  runat="server">
                                        Transfer in From:</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:DropDownList ID="ddltransferfrom" class="form-control" Width="200px" runat="server" >
                            </asp:DropDownList>
                            </div>
                         <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <asp:Label style="padding-left: 10px" id="Label24" CssClass="label-right" runat="server">
                                        Transfer in Date:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                     </div>
                        <asp:TextBox ID="txtTransferInDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                        </div>
                        </div>                   
                    </div>
                    <div class="form-group" style="padding-top:20px">
                        <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label style="padding-left: 10px" id="Label29" CssClass="label-right" runat="server">
                                        Country From:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:DropDownList ID="ddlCountryFrom" runat="server" class="form-control" Width="150px">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label style="padding-left: 10px" id="Label25" CssClass="label-right" runat="server">
                                        District From:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:DropDownList ID="dddistrict" runat="server" class="form-control" Width="100px">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <asp:Label style="padding-left: 10px" id="Label26" CssClass="label-right" runat="server">
                                        Facility:</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <asp:DropDownList ID="ddfacility" runat="server" class="form-control" Width="200px">
                            </asp:DropDownList>
                        </div>
                        </div>
                        <div  class="row">
                        
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label style="padding-left: 10px" id="Label27" CssClass="label-right" runat="server">
                                        ART Start Date:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                     </div>
                        <asp:TextBox ID="txtDateARTStarted" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy" onmouseout="setMonthYear();"></asp:TextBox>
                        </div>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label style="padding-left: 10px" id="Label28" CssClass="label-right" runat="server">
                                        MFL Code:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:TextBox ID="txtMFLCode" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        </div>
                       
                        </div>
                    </div>
                    </div>
                    </td>
                    </tr>
                    <tr> <!--Baseline Assessment-->
              <td style="width:100%;">
             <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">Baseline Assesment & Treatment Initiation</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div> 
                    <div class="box-body" style="padding:20px 5px">
                    <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="Label52" style="padding-left: 10px" CssClass="label-right" runat="server" for="WHOStage">
                                WHO Stage:</asp:Label>
                            
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:DropDownList ID="ddlbaseWHO" runat="server" class="form-control" Width="110px" Style="z-index: 2">
                            </asp:DropDownList>
                            </div>
                    
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="Label54" style="padding-left: 10px" CssClass="label-right" runat="server">
                                CD4 Count:</asp:Label>
                            
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:TextBox ID="txtbaseCD4" runat="server" class="form-control" Width="50px" ></asp:TextBox>
                            </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="Label55" runat="server" style="padding-left: 10px" CssClass="label-right">
                                Hep B Co-infection:</asp:Label>
                            
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input id="rdoHepByes" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="Y" name="HepBInf" />
                                <label for="y">
                                    Yes</label>
                                <input id="rdoHepBNo" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="N" name="HepBInf" />
                                <label for="n">
                                    No</label>
                            </div>

                    </div>
                    <div  class="row">
                    
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="Label56" runat="server" style="padding-left: 10px" CssClass="label-right">
                               Pregnancy:</asp:Label>
                            
                       </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input id="rdobasePrgencyYes" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="Y" name="basePrgency" />
                                <label for="y">
                                    Yes</label>
                                <input id="rdobasePrgencyNo" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="N" name="basePrgency" />
                                <label for="n">
                                    No</label>
                            </div>
                    
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="Label57" runat="server" style="padding-left: 10px" CssClass="label-right">
                                Breast Feeding:</asp:Label>
                            
                        </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input id="rdobasebrestFeedYes" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="Y" name="brestFeed" />
                                <label for="y">
                                    Yes</label>
                                <input id="rdobasebrestFeedNo" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="N" name="brestFeed" />
                                <label for="n">
                                    No</label>
                           </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="Label58" runat="server" style="padding-left: 10px" CssClass="label-right">
                               TB/HIV:</asp:Label>
                            
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input id="rdobaseTBYes" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="Y" name="baseTB" />
                                <label for="y">
                                    Yes</label>
                                <input id="rdobaseTBNo" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="N" name="baseTB" />
                                <label for="n">
                                    No</label>
                            </div>
                            
                    </div>
                    <div  class="row">
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="Label53" runat="server" style="padding-left: 10px" CssClass="label-right">
                                Weight (Kgs):</asp:Label>
                            
                        </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:TextBox ID="txtbaseweight" runat="server" class="form-control" Width="50px" ></asp:TextBox>
                            </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="Label60" runat="server" style="padding-left: 10px" CssClass="label-right">
                                Height (cms):</asp:Label>                            
                        </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:TextBox ID="txtbaseheight" runat="server" class="form-control" Width="50px" ></asp:TextBox>
                           </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="Label61" runat="server" style="padding-left: 10px" CssClass="label-right">
                                BMI:</asp:Label>
                            
                       </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:TextBox ID="txtbaseBMI" runat="server" class="form-control" Width="50px" ReadOnly="true" ></asp:TextBox>
                           </div>
                    </div>
                    <div  class="row">
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="Label62" runat="server" style="padding-left: 10px" CssClass="label-right">
                                MUAC:</asp:Label>
                            
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:TextBox ID="txtbaseMUAC" runat="server" class="form-control" Width="50px" ></asp:TextBox>
                            </div>                    
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="Label59" runat="server" style="padding-left: 10px" CssClass="label-right">
                               Discordant:</asp:Label>
                            
                        </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input id="rdoDiscordantYes" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="Y" name="Discordant" />
                                <label for="y">
                                    Yes</label>
                                <input id="rdoDiscordantNo" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="N" name="Discordant" />
                                <label for="n">
                                    No</label>
                      </div>                    
                    </div>            
                   </div>
                   <div class="pad">
                    <div class="box-header with-border" style="background-color:#eeeeee;">
                      <h3 class="box-title">Date started on 1st line</h3>                                         
                    </div>
                    <div class="box-body" style="padding:20px 5px">
                    <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label style="padding-left: 10px;font-size:15px;color:#8d8f94" CssClass="label-right" id="Label51" runat="server">
                                    ART Cohort</asp:Label>
                         </div>
                    </div>
                    <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <asp:Label ID="Label1" runat="server" style="padding-left: 10px" CssClass="label-right">
                                    Cohort Month:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <input id="txtcohortmnth" size="10" name="CohortMonth" runat="server" readonly="readonly" />
                            </div>
                         <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <asp:Label ID="Label2" runat="server" style="padding-left: 10px" CssClass="label-right">
                                    Cohort Year:</asp:Label>
                            </div>
                         <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <input id="txtcohortyear" size="10" name="CohortYear" runat="server" readonly="readonly" />
                             </div>
                         <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                <asp:Label ID="Label3" ststyle="padding-left: 10px" CssClass="label-right" runat="server">
                                    Regimen:</asp:Label>
                        </div>
                         <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                         <div class="input-group">
                                     <asp:TextBox ID="txtFirstARTRegimen" MaxLength="50" runat="server" Enabled="False" Width="90%" class="form-control"></asp:TextBox>
                                     <span class="input-group-btn pull-left">
                                     <asp:Button ID="btnfirstregimen" MaxLength="20" Width="60px" runat="server" Text="..." 
                                            class="btn btn-primary btn-flat" onclick="btnRegimenFA_Click"  >
                                        </asp:Button>                                            
                                    </span>                                        
                                        </div>
                                
                            </div>
                        </div>
                        <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label style="padding-left: 10px" CssClass="label-right" id="lblbaseviral" runat="server">
                                    Baseline Viral load:</asp:Label>
                         </div>
                         
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="txtBaseViral" Width="100px" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label style="padding-left: 10px" CssClass="label-right" id="lblbasevirdate" runat="server">
                                    Date:</asp:Label>
                                    </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                 </div>
                                <asp:TextBox ID="txtbaseviraldate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                    
                                </div>
                        </div>
                       </div>
                        <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">                       
                                   <asp:Label style="padding-left: 10px" CssClass="label-right" id="lblexitdate" runat="server">
                                   <b>Exit:</b> Transfer Out (Date):</asp:Label>
                        </div>
                         
                       <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                     </div>
                    <asp:TextBox ID="txttransferdate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                    </div>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                       <asp:Label style="padding-left: 10px" CssClass="label-right" id="lbldeathDate" runat="server">
                                   Death Date:</asp:Label>
                                    </div>
                                   <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                    </div>
                    <asp:TextBox ID="txtdeathdate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                    </div>
                        </div>
                       </div>
                        </div>
                      </div>  
                       
               </div>
              </td>
              </tr>
              <tr>
              <td style="width:100%;">
              <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">Diagnosis & ART history</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                   <div class="box-body" style="padding:20px 0px">
                   <div  class="row">
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                <asp:Label style="padding-left: 10px" CssClass="label-right" id="lblpurpose" runat="server">
                                    Prior ART:</asp:Label>
                         </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <input id="rbtnknownYes" class="minimal" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);EnableDis(this);"
                                    type="radio" value="Y" name="PriorART" />
                                <label for="y">
                                    Yes</label>
                                <input id="rbtnknownNo" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); EnableDis(this);"
                                    type="radio" value="N" name="PriorART" />
                                <label for="n">
                                    No</label>
                        </div>
                            </div>
                                                     
                        <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <asp:Label style="padding-left: 10px" CssClass="label-right" id="Label6" runat="server">
                                            Purpose:</asp:Label>                                        
                          </div>                         
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddlpurpose" runat="server" Width="110px" Style="z-index: 2" class="form-control">
                                        </asp:DropDownList>
                          </div>
                          <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                        <asp:Label ID="lblregimen" style="padding-left: 10px" CssClass="label-right" runat="server">
                                            Regimen:</asp:Label>                                       
                           </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">                                    
                                    <div class="input-group">
                                     <asp:TextBox ID="txtRegimen" MaxLength="50" runat="server" Enabled="False" Width="90%" class="form-control"></asp:TextBox>
                                     <span class="input-group-btn pull-left">
                                     <asp:Button ID="btnRegimen" MaxLength="20" Width="60px" runat="server" Text="..." 
                                            class="btn btn-primary btn-flat" onclick="btnRegimen_Click"  >
                                        </asp:Button>                                            
                                    </span>                                        
                                        </div>
                                </div>
                                  <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <asp:Label ID="lbldatelastused" style="padding-left: 10px" CssClass="label-right" runat="server">
                                            Date Last Used:</asp:Label>                                       
                                    </div>
                                   <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>                                  

                                     <asp:TextBox ID="txtLastUsed" runat="server" Width="120px" MaxLength="11" class="form-control" data-date-format="dd-M-yyyy" ></asp:TextBox>                                       
                                        </div>
                                        </div>
                                </div>
                         <div  class="row">
                                     <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                        <div id="divbtnPriorART"  align="center">
                                            <asp:Button ID="btnAddPriorART" Text="Add Prior ART" runat="server" 
                                                CssClass="btn btn-primary" onclick="btnAddPriorART_Click"/></div>
                                    </div>
                             </div>
                      <div  class="row">
                     <div class="col-md-12 col-sm-12 col-xs-12 form-group"> 
                     <div class="table-responsive" style="padding:10 10 5 10">
                     <asp:GridView ID="GrdPriorART" runat="server" CssClass="table table-striped table-bordered table-hover"
                        AutoGenerateColumns="False" BorderWidth="0"  CellPadding="0" CellSpacing="0" 
                             onrowdatabound="GrdPriorART_RowDataBound" 
                             onselectedindexchanging="GrdPriorART_SelectedIndexChanging" >                       
                    </asp:GridView>
                    </div>
                    </div>
                    </div>
                   </div>
                   <div class="box-body" style="padding:20px 5px">
                    <div  class="row">
                     <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <asp:Label style="padding-left: 10px" CssClass="label-right"  id="lblHIVConfirmHIVPosDate" runat="server">
                                Date Confirmed HIV Positive:</asp:Label>                           
                      </div>
                       <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                     <asp:TextBox ID="txtHIVConfirmHIVPosDate" runat="server" Width="120px" class="form-control" data-date-format="dd-M-yyyy" ></asp:TextBox>
                                        
                                        </div>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="lblwhere" style="padding-left: 10px" CssClass="label-right" runat="server">
                                Where:</asp:Label>
                            
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:TextBox ID="txtWhere" MaxLength="50" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div  class="row">
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="lblEnrolHIVCare" style="padding-left: 10px" CssClass="label-right" runat="server">
                                Date Enrolled in HIV Care:</asp:Label>
                            
                       </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                     <asp:TextBox ID="txtEnrolledinHIVCare" runat="server" Width="120px" class="form-control" data-date-format="dd-M-yyyy" ></asp:TextBox>
                                        
                                        </div>
                                  </div>
                       <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <asp:Label id="lblWHOStage" style="padding-left: 10px" CssClass="label-right" runat="server">
                                WHO Stage:</asp:Label>
                            
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:DropDownList ID="ddlWHOStage" runat="server" class="form-control" Width="110px" Style="z-index: 2">
                            </asp:DropDownList>
                          </div>
                    </div>    
            </div>
            </div>
              </td>
              </tr>
              <tr>
              <td style="width:100%;">
              <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">One Time Event Tracker</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                    <div class="box-body" style="padding:20px 5px">
                    
                    <%--<div class="form-group" style="padding-top:20px">--%>
                        <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label30" runat="server">
                                        Sexual Partner Disclosure:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input id="rdodiclosureyes" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="Y" name="PriorART" />
                                <label for="y">
                                    Yes</label>
                                <input id="rdodiclosureno" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="N" name="PriorART" />
                                <label for="n">
                                    No</label>
                        </div>
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label31" runat="server">
                                        Date:</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                        <asp:TextBox ID="txtsexDisDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                        </div>
                        </div>
                        </div>
                        <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label style="padding-left: 10px" CssClass="label-right" id="Label32" runat="server">
                                        Disclosure to Adolescents:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label36" runat="server">
                                        Partial Disclosure Date:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                        <asp:TextBox ID="partDisDate" runat="server" Width="120px" class="form-control" data-date-format="dd-M-yyyy"></asp:TextBox>
                        </div>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label33" runat="server">
                                        Full Disclosure Date:</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                        <asp:TextBox ID="txtFullDisDate" runat="server" Width="120px" class="form-control" data-date-format="dd-M-yyyy"></asp:TextBox>
                        </div>
                        </div>
                        </div>
                        <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label34" runat="server">
                                        INH Prophylaxis:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label35" runat="server">
                                        Start Date:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                        <asp:TextBox ID="txtINHStartDate" runat="server" Width="120px" class="form-control" data-date-format="dd-M-yyyy"></asp:TextBox>
                        </div>
                        </div>
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label38" runat="server">
                                        Completion?:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                         <input id="Inhcompyes" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="Y" name="INHCompletion"/>
                                <label for="y">
                                    Yes</label>
                                <input id="InhcompNo" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="N" name="INHCompletion" />
                                <label for="n">
                                    No</label>
                        </div>
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label37" runat="server">
                                        Date:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                        <asp:TextBox ID="txtINHCompDate" runat="server" Width="120px" class="form-control" data-date-format="dd-M-yyyy"></asp:TextBox>
                        </div>
                        </div>
                        </div>
                        
                        <%--</div>--%>
                       <div class="pad">
                    <div class="box-header with-border" style="background-color:#eeeeee;">
                      <h3 class="box-title">Vaccination</h3>
                                           
                    </div>
                    <div class="box-body" style="padding:20px 5px">
                    <div  class="row">
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label39" runat="server">
                                    Measles:</asp:Label>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtMeasles" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label40" runat="server">
                                    IPV/OPV:</asp:Label>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtIPV" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label45" runat="server">
                                    Penta:</asp:Label>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtPenta" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label46" runat="server">
                                    PCV:</asp:Label>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtPCV" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label44" runat="server">
                                    Diphtheria:</asp:Label>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtDiphtheria" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    
                    </div>
                    <div  class="row">
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label41" runat="server">
                                    BCG:</asp:Label>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtBCG" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label42" runat="server">
                                    HBV:</asp:Label>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtHBV" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label43" runat="server">
                                    Flu Vaccine:</asp:Label>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtFlu" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    
                    </div>
                    <div  class="row">
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:Label CssClass="label-right"  id="Label47" runat="server">
                                    HepatitisB:</asp:Label>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtHepatitis" runat="server" class="form-control"></asp:TextBox>
                    </div>                    
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label48" runat="server">
                                    Tetanus:</asp:Label>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtTetanus" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label49" runat="server">
                                    Pertussis:</asp:Label>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtPertussis" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label50" runat="server">
                                    Haemophilus influenzae type b (Hib):</asp:Label>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <asp:TextBox ID="txtHaemophilus" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    </div>
                    
                    </div>
                    </div>
                    </div>
                    </div>
                    </td>
                    </tr>
                    <tr>
              <td style="width:100%;padding:10px 0 10px 0" >
                <table class="box" width="100%" >
                <tr  class="row" style="padding-bottom:20px">
                <td class="form-group" style="padding-right:20px;padding-top:10px" align="right">
                <asp:Button ID="btnsave" Text="Save" Width="14%" runat="server"  Style="text-align: left;"
                        class="btn btn-primary" onclick="btnsave_Click" />
                 <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -4%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
                </td>
                <td class="form-group" align="left" style="padding-left:20px;padding-top:10px">
                <asp:Button ID="btnClose" Text="Close" Width="14%" runat="server" 
                        class="btn btn-primary" onclick="btnClose_Click" />
                 <label class="glyphicon glyphicon-remove-circle" style="margin-left: -4%; margin-right: 2%;
                                            vertical-align: sub; color: #fff;">
                                        </label>
                </td>
                </tr>
                </table>
                </td>
                </tr>
                    </table>
              </div>
              <!-- /.tab-pane -->
              <div class="tab-pane" id="tab_2">
              <div class="col-md-12 col-sm-12 col-xs-12">
              <div class="nav-tabs-custom">
                <ul class="nav nav-tabs">
                  <li class="active"><a href="#Triage" data-toggle="tab">Triage</a></li>
                  <li><a href="#PresCompl" data-toggle="tab">Presenting Complaints</a></li>
                  <li><a href="#AddHx" data-toggle="tab">Additional Hx</a></li>
                  <li><a href="#SystRew" data-toggle="tab">Systematic Review</a></li>
                  <li><a href="#Mgmt" data-toggle="tab">Management</a></li>             
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="Triage">
                        <table class="table" style="padding-left:10px;width:100%;">
                      
                      <tr>
                  <td style="width:100%;">
                  <div class="box box-default box-solid">
                        <div class="box-header with-border">
                          <h3 class="box-title">Patient Vitals Signs</h3>
                          <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                            </button>
                          </div>                      
                        </div> 
                        <div class="box-body">
                        <div class="form-group" style="padding-top:20px">
                        <uc2:ucVitals ID="idVitalSign" runat="server" />
                        </div>
                        </div>
                        </div>
                     </td>
                     </tr>            
                    <tr align="center">
                        <td >
                            <uc12:UC_Signature ID="UC_SignatureTriage" runat="server" />
                        </td>
                    </tr>
                    <tr>
              <td style="width:100%;padding:10px 0 10px 0;" align="center" >
              <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>                            
                <%--<table class="box" width="100%" >
                <tr  class="row" style="padding-bottom:20px">
                <td class="form-group" style="padding-right:20px;padding-top:10px" align="right">--%>
                <asp:Button ID="btnSaveTriage" runat="server" Text="Save" OnClick="btnSaveTriage_Click" Width="8%"  Style="text-align: left;"
                        class="btn btn-primary" />
                 <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -4%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>&nbsp;&nbsp;&nbsp;&nbsp;
                <%--</td>
                <td class="form-group" align="left" style="padding-left:20px;padding-top:10px">--%>
                <asp:Button ID="btnCloseTriage" Text="Close" runat="server" OnClick="btncloseTriage_Click" Width="8%" Style="text-align: left;" class="btn btn-primary" />
                 <label class="glyphicon glyphicon-remove-circle" style="margin-left: -4%; margin-right: 2%;
                                            vertical-align: sub; color: #fff;">
                                        </label>&nbsp;&nbsp;&nbsp;&nbsp;
                <%--</td>
                <td class="form-group" align="left" style="padding-left:20px;padding-top:10px">--%>
                            <asp:Button ID="Button1" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Width="10%" />
                            <%--<%--</td>
                </tr>
                </table>--%>
                </ContentTemplate>
                </asp:UpdatePanel>
                </td>
                </tr>
                    
                                    <tr id="tblDeleteButton" style="display: none" align="center">
                                        <td align="center" width="100%">
                                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" CssClass="btn btn-primary" Height="40px"/>
                                        </td>
                                    </tr>                                
                            </table>
                        </div>
                    <div class="tab-pane" id="PresCompl">
                    <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">Presenting Complaints</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                    <div class="box-body">
                   <div  class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <div class="table-responsive  pre-scrollable" style="max-height:200px">
                        <asp:GridView ID="gvPresentingComplaints" runat="server" AutoGenerateColumns="False"
                            Width="100%" GridLines="None" CssClass="table-responsive table-striped box-body" OnRowDataBound="gvPresentingComplaints_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Complaint" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresenting" runat="server"  Text='<%# Eval("ID")%>' Visible="false"></asp:Label>                                        
                                        <asp:CheckBox ID="ChkPresenting" runat="server" class="minimal" Text='<%# Eval("NAME")%>' />                                        
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Duration(Days)" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPresenting" runat="server" Width="150px"> 
                                        </asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>
                        </div>
                    </div>
                    <div  class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                       <label>
                                Specify other Presenting Complaints:</label>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <asp:TextBox ID="txtOtherPresentingComplaints" runat="server" Width="99%" Rows="3"
                                TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    </div>
                    </div>
                    <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">OBS/GYN History</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                    <div class="box-body">
                        <div class="row">
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label65" runat="server">
                                        LMP:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                         </div>
                            <asp:TextBox ID="txtLMPdate" runat="server" class="form-control" Width="100%" data-date-format="dd-M-yyyy"></asp:TextBox>
                         </div>
                         </div>
                         <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="lblpregnanttmp" runat="server">
                                                Pregnant:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input id="rdopregnantYes" onmouseup="up(this);" class="minimal" onfocus="up(this);" onclick="down(this); show('spanEDD'); "
                                    type="radio" value="Y" name="pregnant" runat="server" />
                                <label>
                                    Yes</label>
                                <input id="rdopregnantNo" onmouseup="up(this);" class="minimal" onfocus="up(this);" onclick="down(this); hide('spanEDD');"
                                    type="radio" value="N" name="pregnant" runat="server" />
                                <label>
                                    No</label>
                        </div>
                        </div>
                        <div class="row">
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="lblEDD" runat="server">
                                        EDD:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                         </div>
                            <asp:TextBox ID="txtEDDDate" runat="server" class="form-control" Width="100%" data-date-format="dd-M-yyyy"></asp:TextBox>
                         </div>
                         </div>
                         <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="lbldelivered" runat="server">
                                                Delivered since last visit:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input id="rdoDeliveredYes" onmouseup="up(this);" onfocus="up(this);" class="minimal" onclick="down(this); show('spanEDD'); "
                                    type="radio" value="Y" name="pregnant" runat="server" />
                                <label>
                                    Yes</label>
                                <input id="rdoDeliveredNo" onmouseup="up(this);" class="minimal" onfocus="up(this);" onclick="down(this); hide('spanEDD');"
                                    type="radio" value="N" name="pregnant" runat="server" />
                                <label>
                                    No</label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="lblDelDate" runat="server">
                                        Delivered Date:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                         </div>
                            <asp:TextBox ID="txtDeliDate" runat="server" class="form-control" Width="100%" data-date-format="dd-M-yyyy"></asp:TextBox>
                         </div>
                         </div>
                         </div>
                    
                    </div>
                    
                    <div class="pad">
                    <div class="box-header with-border" style="background-color:#eeeeee;">
                      <h3 class="box-title">Contraceptive History</h3>                                         
                    </div>
                    <div class="box-body" style="padding:20px 5px">
                    <div  class="row">
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label66" runat="server">
                                        Family Planning:</asp:Label>
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <asp:DropDownList ID="ddlFamilyPanningStatus" class="form-control select2" onchange="fnfamilyplanning();" runat="server">
                                        </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row hidden-md hidden-lg" id="divFamilyPlanningMethod">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-left" style="padding-left: 10px" id="Label68" runat="server">
                                        Family Planning Method:</asp:Label>
                        </div>
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                    <asp:Panel ID="PnlFamilyPlanningMethod" runat="server" CssClass="box" Width="100%" >
                                            </asp:Panel>
                    </div>
                    </div>
                    <div  class="row hidden-md hidden-lg" id="notfamilyplanning">
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label67" runat="server">
                                        Reason Not on Family Planning:</asp:Label>
                        </div>
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <asp:DropDownList ID="ddlnotfamilyplanning" class="form-control select2" runat="server">
                                            </asp:DropDownList>
                    </div>
                    </div>
                    <div  class="row">
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label69" runat="server">
                                                Cervical Cancer screening in the past 12 months?:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input id="rdocervcancyes" onmouseup="up(this);" onfocus="up(this);" class="minimal" onclick="down(this); show('screeneddate'); "
                                    type="radio" value="Y" name="Cervical" runat="server" />
                                <label>
                                    Yes</label>
                                <input id="rdocervcancNo" onmouseup="up(this);" class="minimal" onfocus="up(this);" onclick="down(this); hide('screeneddate');"
                                    type="radio" value="N" name="Cervical" runat="server" />
                                <label>
                                    No</label>
                        </div>
                        <div id="screeneddate" class="hidden-md hidden-lg">
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group" >
                                <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label70" runat="server">
                                                Date Screened:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                         </div>
                            <asp:TextBox ID="txtCervCancDate" runat="server" class="form-control" Width="100%" data-date-format="dd-M-yyyy"></asp:TextBox>
                         </div>
                         </div>
                         </div>
                    </div>
                    </div>
                    </div>
                    </div>
                        <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">                    
                            <uc12:UC_Signature ID="UC_SignaturePresComplaint" runat="server" />
                        </div>
                        </div>
                        <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">                    
                        <table width="100%">
                        <tr>
                      <td style="width:100%;padding:10px 0 10px 0;" align="center" >
                      <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>                            
                        
                        <asp:Button ID="btnPresCompl" runat="server" Text="Save" OnClick="btnSavePresCompl_Click" Width="8%"  Style="text-align: left;"
                                class="btn btn-primary" />
                         <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -4%; margin-right: 2%;
                                        vertical-align: sub; color: #fff;"></label>&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <asp:Button ID="btnClosePresCompl" Text="Close" runat="server" OnClick="btnClosePresCompl_Click" Width="8%" Style="text-align: left;" class="btn btn-primary" />
                         <label class="glyphicon glyphicon-remove-circle" style="margin-left: -4%; margin-right: 2%;
                                                    vertical-align: sub; color: #fff;">
                                                </label>&nbsp;&nbsp;&nbsp;&nbsp;
                       
                                    <asp:Button ID="btnPrintPresCompl" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Width="10%" />
                                  
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                        </tr>
                        </table>
                        </div>
                        
                      </div>
                    
                                                    
                    
                    </div>
                    
                    <div class="tab-pane" id="AddHx">
                    <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">Immunization History</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                    <div class="box-body">
                     <asp:UpdatePanel ID="grdUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>                            
                        <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <asp:Label style="padding-left: 10px" CssClass="label-right" id="lblImmunisationPeriod" runat="server">
                                            Immunization Period:</asp:Label>                                        
                          </div>                         
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddlImmunisationPeriod" runat="server" Width="110px" Style="z-index: 2" class="form-control select2">
                                        </asp:DropDownList>
                          </div>
                          <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <asp:Label ID="lblImmunisationGiven" style="padding-left: 10px" CssClass="label-right" runat="server">
                                            Immunizations Given:</asp:Label>                                       
                           </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">                                    
                                    <asp:DropDownList ID="ddImmunisationgiven" runat="server" Width="110px" Style="z-index: 2" class="form-control select2">
                                                                        </asp:DropDownList>
                                </div>
                                  <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <asp:Label ID="lblDateImmunised" style="padding-left: 10px" CssClass="label-right" runat="server">
                                            Date Immunised:</asp:Label>                                       
                                    </div>
                                   <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div> 
                                     <asp:TextBox ID="txtDateImmunised" runat="server" Width="120px" MaxLength="11" class="form-control" data-date-format="dd-M-yyyy" ></asp:TextBox>                                       
                                        </div>
                                        </div>
                                </div>
                         <div  class="row">
                                     <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                        <div id="div1"  align="center">
                                            <asp:Button ID="btnAddImmunization" Text="Add Immunization" runat="server" 
                                                CssClass="btn btn-primary" onclick="btnAddImmunization_Click" Height="30px" Width="14%" Style="text-align: left;"/>
                                         <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                                                    color: #fff;">
                                                                </label>       
                                         </div>
                                    </div>
                             </div>
                      <div  class="row">
                     <div class="col-md-12 col-sm-12 col-xs-12 form-group"> 
                     <div class="table-responsive" style="padding:10 10 5 10">
                     
                     <asp:GridView ID="GrdImmunization" runat="server" CssClass="table table-striped table-bordered table-hover"
                        AutoGenerateColumns="False" BorderWidth="0"  CellPadding="0" CellSpacing="0" 
                             OnRowDeleting="GrdImmunization_RowDeleting" >                       
                    </asp:GridView>
                    
                    </div>
                    </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <div  class="row">
                        <div class="col-md-3 col-sm-12 col-xs-12" style="padding-left:40px">
                         <label>
                                        Immunization  History Notes :</label>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-left:40px">
                                    <asp:TextBox ID="txtImmuniNotes" runat="server" TextMode="MultiLine" Rows="3" Width="90%">
                                    </asp:TextBox>
                        </div>
                        </div>
                    </div>
                    </div>
              <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">Prior ART</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                   <div class="box-body" style="padding:20px 0px">
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                   <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <asp:Label style="padding-left: 10px" CssClass="label-right" id="Label74" runat="server">
                                    Prior ART:</asp:Label>
                         </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <input id="rdoPriorARTFWPYes" class="minimal" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);EnableDis(this);"
                                    type="radio" value="Y" name="PriorARTFWP" />
                                <label for="y">
                                    Yes</label>
                                <input id="rdoPriorARTFWPNo" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); EnableDis(this);"
                                    type="radio" value="N" name="PriorARTFWP" />
                                <label for="n">
                                    No</label>
                        </div>
                            </div>
                                                     
                        <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <asp:Label style="padding-left: 10px" CssClass="label-right" id="Label75" runat="server">
                                            Purpose:</asp:Label>                                        
                          </div>                         
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddlPApurposeFWP" runat="server" Width="110px" Style="z-index: 2" class="form-control">
                                        </asp:DropDownList>
                          </div>
                          <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                        <asp:Label ID="Label76" style="padding-left: 10px" CssClass="label-right" runat="server">
                                            Regimen:</asp:Label>                                       
                           </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">                                    
                                    <div class="input-group">
                                     <asp:TextBox ID="txtRegimenFWP" MaxLength="50" runat="server" Enabled="False" Width="90%" class="form-control"></asp:TextBox>
                                     <span class="input-group-btn pull-left">
                                     <asp:Button ID="btnRegimenFWP" MaxLength="20" Width="60px" runat="server" Text="..." 
                                            class="btn btn-primary btn-flat" onclick="btnFWPRegimen_Click"  >
                                        </asp:Button>                                            
                                    </span>                                        
                                        </div>
                                </div>
                                  <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <asp:Label ID="Label77" style="padding-left: 10px" CssClass="label-right" runat="server">
                                            Date Last Used:</asp:Label>                                       
                                    </div>
                                   <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>                                  

                                     <asp:TextBox ID="txtdateLastusedFWP" runat="server" Width="120px" MaxLength="11" class="form-control" data-date-format="dd-M-yyyy" ></asp:TextBox>                                       
                                        </div>
                                        </div>
                                </div>
                         <div  class="row">
                                     <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                        <div id="div4"  align="center">
                                            <asp:Button ID="btnAddPriorARTFWP" Text="Add Prior ART" runat="server" 
                                                CssClass="btn btn-primary" onclick="btnAddPriorARTFWP_Click"/></div>
                                    </div>
                             </div>
                        
                      <div  class="row">
                     <div class="col-md-12 col-sm-12 col-xs-12 form-group"> 
                     <div class="table-responsive" style="padding:10 10 5 10">
                     <asp:GridView ID="GrdPriorARTFWP" runat="server" CssClass="table table-striped table-bordered table-hover"
                        AutoGenerateColumns="False" BorderWidth="0"  CellPadding="0" CellSpacing="0" 
                        OnSelectedIndexChanging="GrdPriorARTFWP_SelectedIndexChanging" OnRowDataBound="GrdPriorARTFWP_RowDataBound">                       
                    </asp:GridView>
                    </div>
                    </div>
                    </div>
                    </ContentTemplate>
                        </asp:UpdatePanel>
                   </div>
                   
            </div>
            <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">                    
                            <uc12:UC_Signature ID="UC_Signature1" runat="server" />
                        </div>
                        </div>
                        <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">                    
                        <table width="100%">
                        <tr>
                      <td style="width:100%;padding:10px 0 10px 0;" align="center" >
                      <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>                            
                        
                        <asp:Button ID="btnSaveAddHx" runat="server" Text="Save" OnClick="btnSaveAddHx_Click" Width="8%"  Style="text-align: left;"
                                class="btn btn-primary" />
                         <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -4%; margin-right: 2%;
                                        vertical-align: sub; color: #fff;"></label>&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <asp:Button ID="btncloseAddHx" Text="Close" runat="server" OnClick="btncloseAddHx_Click" Width="8%" Style="text-align: left;" class="btn btn-primary" />
                         <label class="glyphicon glyphicon-remove-circle" style="margin-left: -4%; margin-right: 2%;
                                                    vertical-align: sub; color: #fff;">
                                                </label>&nbsp;&nbsp;&nbsp;&nbsp;
                       
                                    <asp:Button ID="btnprintAddHx" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Width="10%" />
                                  
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                        </tr>
                        </table>
                        </div>
                        
                      </div>
                    </div>
                    <div class="tab-pane" id="SystRew">
                    <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">Physical Examination</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                    <div class="box-body">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                    <uc9:ucPhyExam ID="UCPhyExam" runat="server" />
                    </div>
                    </div>
                    </div>
                    <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">TB Screning</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                    <div class="box-body">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                    <uc10:ucTBScre ID="UCTBScreen" runat="server" />
                    </div>
                    </div>

                    </div>
                    <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">Nutritional Assessment</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                   <div class="box-body" style="padding:20px 0px">
                   <div  class="row">
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group" style="padding-left:40px">
                        <input id="rdoNorm" class="minimal" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);EnableDis(this);"
                                    type="radio" value="Normal" name="Nutritional" />
                                <label for="y">
                                    Normal</label>                                                                        
                          </div> 
                         <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="rdoSMaln" class="minimal" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);EnableDis(this);"
                                    type="radio" value="SMalnutrition" name="Nutritional" />
                                <label for="y">
                                    Severe Acute Malnutrition</label>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="rdoMMaln" class="minimal" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);EnableDis(this);"
                                    type="radio" value="MMalnutrition" name="Nutritional" />
                                <label for="y">
                                    Moderate Acute Malnutrition</label>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="rdoObese" class="minimal" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);EnableDis(this);"
                                    type="radio" value="Obese" name="Nutritional" />
                                <label for="y">
                                    Overweigh/Obese</label>
                        </div>                       
                         </div>
                         <div  class="row">
                        <div class="col-md-3 col-sm-12 col-xs-12" style="padding-left:40px">
                         <label>
                                        Nutritional Assessment Comments :</label>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-left:40px">
                                    <asp:TextBox ID="txtNutrAssComments" runat="server" TextMode="MultiLine" Rows="3" Width="90%">
                                    </asp:TextBox>
                        </div>
                        </div>
                   </div>
                   
            </div>
                    <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">WHO Stage</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                    <div class="box-body">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                    <uc11:ucWHO ID="UCWHO" runat="server" />
                    </div>
                    </div>

                    </div>
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">                    
                            <uc12:UC_Signature ID="UC_Signature2" runat="server" />
                        </div>
                        </div>
                        <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">                    
                        <table width="100%">
                        <tr>
                      <td style="width:100%;padding:10px 0 10px 0;" align="center" >
                      <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>                            
                        
                        <asp:Button ID="btnSaveSystRew" runat="server" Text="Save" OnClick="btnSaveSystRew_Click" Width="8%"  Style="text-align: left;"
                                class="btn btn-primary" />
                         <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -4%; margin-right: 2%;
                                        vertical-align: sub; color: #fff;"></label>&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <asp:Button ID="btncloseSystRew" Text="Close" runat="server" OnClick="btncloseSystRew_Click" Width="8%" Style="text-align: left;" class="btn btn-primary" />
                         <label class="glyphicon glyphicon-remove-circle" style="margin-left: -4%; margin-right: 2%;
                                                    vertical-align: sub; color: #fff;">
                                                </label>&nbsp;&nbsp;&nbsp;&nbsp;
                       
                                    <asp:Button ID="btnprintSystRew" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Width="10%" />
                                  
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                        </tr>
                        </table>
                        </div>
                        
                      </div>
                    </div>
                    <div class="tab-pane" id="Mgmt">
                     <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">ARV Side Effects</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                    <div class="box-body">
                    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <label>
                                    Short term effects:</label>
                                <div id="divShortTermEffects" class="table-responsive  pre-scrollable box" style="max-height:200px;margin-bottom: 20px">
                                    <asp:CheckBoxList ID="cblshorttermeffects" onclick="CheckBoxToggleShowHide1('<%=cblshorttermeffects.ClientID%>','divshorttermeffecttxt','Other Specify','ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_txtspecityothershortterm');"
                                        RepeatLayout="Flow" runat="server" Width="200px">
                                    </asp:CheckBoxList>
                                </div>
                                <div id="divshorttermeffecttxt" style="display: none">
                                    <label>
                                        Specify other short term effects:</label>
                                    <asp:TextBox ID="txtspecityothershortterm" runat="server" Skin="MetroTouch" Width="100%">
                                    </asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <label>
                                    Long term effects:</label>
                                <div id="divLongTermEffects" class="table-responsive  pre-scrollable box" style="max-height:200px;margin-bottom: 20px">
                                    <asp:CheckBoxList ID="cbllongtermeffects" onclick="CheckBoxToggleShowHide1('<%=cbllongtermeffects.ClientID%>','divlongtermeffecttxt','Other specify','ctl00_IQCareContentPlaceHolder_tabControl_TabManagement_txtspecifyotherlongterm');"
                                        RepeatLayout="Flow" runat="server" Width="200px">
                                    </asp:CheckBoxList>
                                </div>
                                <div id="divlongtermeffecttxt" style="display: none">
                                    <label>
                                        Specify other long term effects:</label>
                                    <asp:TextBox ID="txtspecifyotherlongterm" runat="server" Skin="MetroTouch" Width="100%">
                                    </asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                    </div>
                    </div>
                    </div>
                    <div class="tab-pane" id="Div2">
                     <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">Adherence</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                    <div class="box-body">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                    <table class="table-condensed" width="100%" border="0">
                            <tbody>
                                <tr id="divtreatmentPharm">
                                    <td class="form">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 20%;" align="left">
                                                    <label class="margin10">
                                                        CTX:
                                                    </label>
                                                    <label class="margin10">
                                                        Adherence:
                                                    </label>
                                                    
                                                </td>
                                                <td style="width: 30%;" align="left" colspan="4">
                                                <asp:DropDownList ID="ddlCotrimoxazoleAdhere" Width="150px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td style="width: 20%; display: inline" align="left">
                                                    <label class="margin10" id="lblARVDrugsAdhere" runat="server">
                                                        ARV:
                                                    </label>
                                                    <label class="margin10">
                                                        Adherence:
                                                    </label>
                                                </td>
                                                <td style="width: 30%;" align="left">
                                                    <asp:DropDownList ID="ddlarvdrugadhere" onchange="fnARVDrug();" Width="150px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 10%;" align="left">
                                                    <label class="margin10">
                                                        Why Poor/Fair:
                                                    </label>
                                                </td>
                                                <td style="width: 20%;" align="left">
                                                    <asp:DropDownList ID="ddlwhypoorfair" onchange="fnarvdrugother();" Width="150px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 30%;" align="left">
                                                    <div id="divReasonARVDrugsother" style="display: none">
                                                        <label class="right15">
                                                            Other Reason:</label>
                                                        <asp:TextBox ID="txtReasonARVDrugsPoorFairOther" runat="server" MaxLength="10" Columns="10"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td style="width: 10%; display: inline" align="left">
                                                    <label class="required margin10">
                                                        Substitutions/Interruptions:
                                                    </label>
                                                </td>
                                                <td style="width: 30%;" align="left">
                                                    <asp:DropDownList ID="ddlsubsituationInterruption" Width="150px" onchange="fnSubsituations();"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 30%;" align="left" colspan="3">
                                                <table width="100%" id="arvTherapyChange" style="display: none">
                                                <tr>
                                                <td>                                                   
                                                        <label class="required margin10">
                                                            Change Regimen Reason:</label>
                                                </td>
                                                <td>
                                                        <asp:DropDownList ID="ddlArvTherapyChangeCode" onchange="fnRegimenChange();" Width="150px" runat="server">
                                                        </asp:DropDownList>
                                                </td>
                                                <td>
                                                        <div id="otherarvTherapyChangeCode" style="display: none">
                                                            <label class="required margin10" for="arvTherapyChangeCodeOtherName">
                                                                Specify:</label>
                                                                <span>
                                                            <input id="txtarvTherapyChangeCodeOtherName" maxlength="20" size="10" name="arvTherapyChangeCodeOtherName"
                                                                runat="server" /></span></div>
                                                </td>
                                                </tr>
                                                </table>
                                                 </td>
                                                 </tr>
                                                 <tr >
                                                 <td width="100%" colspan="5" style="padding-top:10px">
                                                    <table width="100%" id="arvTherapyStop" style="display: none">
                                                    <tr>
                                                    <td>
                                                        <label id="lblrARTdate" class="required margin10">
                                                            Date ART Ended</label>
                                                    </td>
                                                    <td>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                             </div>
                                                        <input id="txtARTEndeddate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy" name="txtARTEndeddate" />
                                                        </div>
                                                      </td>
                                                      <td>
                                                        <label class="required margin10">
                                                            Stop Regimen Reason:</label
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlArvTherapyStopCode" onchange="fnStopReason();" Width="150px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <div id="otherarvTherapyStopCode" style="display: none">
                                                            <label class="required margin10" for="arvTherapyStopCodeOtherName">
                                                                Specify:</label>
                                                            <span>
                                                            <input id="txtarvTherapyStopCodeOtherName" maxlength="20" size="10" name="arvTherapyStopCodeOtherName"
                                                                runat="server" />                                                                
                                                                </span>
                                                                </div>
                                                    </td>                                                    
                                                 </tr>
                                                </table>
                                                
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>                                
                            </tbody>
                        </table>
                    </div>
                    </div>
                </div>
                <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">Positive Health, Dignity, and Prevention</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                   <div class="box-body" style="padding:20px 0px">
                   <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <asp:Label style="padding-left: 10px" CssClass="label-left" id="Label64" runat="server">
                                    Key Population?:</asp:Label>
                         </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group label-left">
                                <input id="rdokeyPopulYes" class="minimal" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);EnableDis(this);"
                                    type="radio" value="Y" name="keyPopul" />
                                <label for="y">
                                    Yes</label>
                                <input id="rdokeyPopulNo" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); EnableDis(this);"
                                    type="radio" value="N" name="keyPopul" />
                                <label for="n">
                                    No</label>
                        </div>
                            </div>
                                                     
                        <div  class="row">
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group" style="padding-left:40px">
                        <input id="rdoMSM" class="minimal" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);EnableDis(this);"
                                    type="radio" value="MSM" name="keyPopulOpt" />
                                <label for="y">
                                    Men Who have Sex with Men (MSM)</label>                                                                        
                          </div> 
                         <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="rdoFSW" class="minimal" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);EnableDis(this);"
                                    type="radio" value="FSW" name="keyPopulOpt" />
                                <label for="y">
                                    Female Sex Worker (FSW)</label>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="rdoPWID" class="minimal" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);EnableDis(this);"
                                    type="radio" value="PWID" name="keyPopulOpt" />
                                <label for="y">
                                    People Who Inject Drugs (PWID)</label>
                        </div>                        
                         </div>
                         <div  class="row">
                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                 <asp:Label style="padding-left: 10px" CssClass="label-right" id="Label71" runat="server">
                                    STI screening done during this visit?:</asp:Label>       
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                              <input id="rdoSTIScreenYes" class="minimal" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);EnableDis(this);"
                                    type="radio" value="Y" name="STIScreen" />
                                <label for="y">
                                    Yes</label>
                                <input id="rdoSTIScreenNo" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); EnableDis(this);"
                                    type="radio" value="N" name="STIScreen" />
                                <label for="n">
                                    No</label>          
                            </div>
                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                  <asp:Label style="padding-left: 10px" CssClass="label-right" id="Label72" runat="server">
                                    Partner Notified?:</asp:Label>      
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <input id="rdoPartnerNotYes" class="minimal" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);EnableDis(this);"
                                    type="radio" value="Y" name="PartnerNot" />
                                <label for="y">
                                    Yes</label>
                                <input id="rdoPartnerNotNo" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); EnableDis(this);"
                                    type="radio" value="N" name="PartnerNot" />
                                <label for="n">
                                    No</label>        
                            </div>
                          </div>
                      <div  class="row">
                      <div class="col-md-12 col-sm-12 col-xs-12">
                      <asp:Label style="padding-left: 10px" CssClass="label-left" id="Label73" runat="server">
                                    *Prevention with positives (PwP):</asp:Label>
                      </div>
                      </div>
                      <div  class="row">
                     <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-left:40px">                     
                        <div id="div3" class="table-responsive  pre-scrollable box" style="max-height:250px;margin-bottom: 20px;padding:10px 10px 5px 10px;width:400px">
                            <asp:CheckBoxList ID="chklistPWP" RepeatLayout="Flow" runat="server" Width="200px">
                            <asp:ListItem Text="STI  =  STI screening" Value="STI"></asp:ListItem>
                             <asp:ListItem Text="PT  =   Partner Testing" Value="PT"></asp:ListItem>
                             <asp:ListItem Text="CD =    Condom distribution" Value="CD"></asp:ListItem>
                             <asp:ListItem Text="AdC = Adherence counselling" Value="Adc"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>                    
                    </div>
                    </div>
                   </div>
                   
            </div>
            <div class="box box-default box-solid">
                    <div class="box-header with-border">
                      <h3 class="box-title">Next Appointment</h3>
                      <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                      </div>                      
                    </div>
                   <div class="box-body" style="padding:20px 0px">
                   <div  class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <asp:UpdatePanel ID="updtpnlappointment" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        <center>
                              <asp:Button ID="btnNextAppointment" runat="server" Text="Next Appointment"  CssClass="btn btn-primary" Height="30px" Width="130px" Style="text-align: left;"
                                onclick="btnAppointment_Click" />
                        </center>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                         </div>
                        
                            </div>
                   </div>
                   
            </div>
                </div>
                <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">                    
                            <uc12:UC_Signature ID="UC_Signature3" runat="server" />
                        </div>
                        </div>
                        <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">                    
                        <table width="100%">
                        <tr>
                      <td style="width:100%;padding:10px 0 10px 0;" align="center" >
                      <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>                            
                        
                        <asp:Button ID="btnSaveMgmt" runat="server" Text="Save" OnClick="btnSaveMgmt_Click" Width="8%"  Style="text-align: left;"
                                class="btn btn-primary" />
                         <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -4%; margin-right: 2%;
                                        vertical-align: sub; color: #fff;"></label>&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <asp:Button ID="btncloseMgmt" Text="Close" runat="server" OnClick="btncloseMgmt_Click" Width="8%" Style="text-align: left;" class="btn btn-primary" />
                         <label class="glyphicon glyphicon-remove-circle" style="margin-left: -4%; margin-right: 2%;
                                                    vertical-align: sub; color: #fff;">
                                                </label>&nbsp;&nbsp;&nbsp;&nbsp;
                       
                                    <asp:Button ID="btnPrintMgmt" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Width="10%" />
                                  
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                        </tr>
                        </table>
                        </div>
                        
                      </div>
              </div>
                
              </div>              
              <!-- /.tab-pane -->
            </div>
            <!-- /.tab-content -->
          </div>
          <!-- nav-tabs-custom -->
                          </div>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
      </div>
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box -->
    </section>
            <!-- /.content -->
        </div>        
    </div>
    <div id="divVital" style="display: none; width: 100%">
        <uc4:ucVitalSigns ID="ucVitalsigns" runat="server" />
    </div>
    <div id="divViralLoad" style="display: none; width: 100%">
        <uc5:ucViralTrack ID="ucViralTrack" runat="server" />
    </div>
    <div id="divDrugChanges" style="display: none; width: 100%">
        <uc8:ucDrug ID="ucDrug" runat="server" />
    </div>
    <div id="divfamily" style="display: none; width: 100%">
        <uc6:ucFamily ID="ucFamily" runat="server" />
    </div>
    <div class="a2a_kit a2a_kit_size_32 a2a_floating_style a2a_default_style" style="bottom: 0px;
        left: 50%; margin-left: 450px;">
        <a class="btn primary" data-toggle="popover" id="allergybtn" title="Allergies" data-target="#divVital"
            data-container="body" data-placement="top" data-html="true">
            <img src="images/allergies.jpg" class="img-responsive img-circle" width="40px" height="40px" /></a>
        <a class="btn primary" id="drugbtn" data-toggle="popover" title="Drug Changes" data-target="#divDrugChanges"
            data-container="body" data-placement="top" data-html="true">
            <img src="images/Drugs.jpg" class="img-responsive img-circle" width="40px" height="40px" /></a>
        <a class="btn primary" id="familybtn" data-toggle="popover" title="HIV status of family members"
            data-target="#divfamily" data-container="body" data-placement="top" data-html="true">
            <img src="images/family.jpg" class="img-responsive img-circle" width="40px" height="40px" /></a>
        <a class="btn primary" id="viralbtn" data-toggle="popover" title="Viral Load Tracker"
            data-target="#divViralLoad" data-container="body" data-placement="top" data-html="true">
            <img src="images/lab.jpg" class="cimg-responsive img-circle" width="40px" height="40px" /></a>
    </div>
    <div id="modalConfirmYesNo" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 id="lblTitleConfirmYesNo" class="modal-title">Confirmation</h4>
                </div>
                <div class="modal-body">
                    <p id="lblMsgConfirmYesNo"></p>
                </div>
                <div class="modal-footer">
                    <button id="btnYesConfirmYesNo" type="button" class="btn btn-primary">Yes</button>
                    <button id="btnNoConfirmYesNo" type="button" class="btn btn-default">No</button>
                </div>
            </div>
        </div>
    </div>
    </form>
    
</body>
</html>
