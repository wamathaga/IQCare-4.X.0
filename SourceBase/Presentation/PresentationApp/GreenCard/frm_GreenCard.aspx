<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_GreenCard.aspx.cs"
    Inherits="PresentationApp.GreenCard.frm_GreenCard" %>

<%@ Register Src="~/GreenCard/UserControls/UserControl_VitalSigns.ascx" TagName="ucVitals"
    TagPrefix="uc2" %>
<%@ Register Src="~/GreenCard/UserControls/UC_Vitals.ascx" TagName="ucVitalSigns"
    TagPrefix="uc4" %>
<%@ Register Src="~/GreenCard/UserControls/UC_ViralLoadTracker.ascx" TagName="ucViralTrack"
    TagPrefix="uc5" %>
<%@ Register Src="~/GreenCard/UserControls/UC_Family.ascx" TagName="ucFamily" TagPrefix="uc6" %>
<%@ Register Src="~/GreenCard/UserControls/UC_ChronicCondition.ascx" TagName="ucChronic"
    TagPrefix="uc7" %>
<%@ Register Src="~/GreenCard/UserControls/UC_DrugChanges.ascx" TagName="ucDrug"
    TagPrefix="uc8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="pageHead">
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
        });
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
            <asp:Panel ID="divError" runat="server" Style="padding: 5px;float:left" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                            HorizontalAlign="Left">
                            <asp:Label CssClass="label-right" ID="lblError" runat="server" Style="font-weight: bold; color: #800000;
                                padding-left: 5px"></asp:Label>
                        </asp:Panel>
            </div>
            </div>
            <div class="row">
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label5" runat="server">
                                        Visit Date:</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                     </div>
                        <asp:TextBox ID="txtVisitDate" runat="server" class="form-control" Width="120px" data-date-format="dd-M-yyyy"></asp:TextBox>
                        </div>
                        </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group" >
                        <asp:Label CssClass="label-right" style="padding-left: 10px;" id="Label8" runat="server">
                                        Visit Type:</asp:Label>
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group" style="float:right">
                        <asp:DropDownList ID="ddlvisittype" class="form-control" Width="130px" runat="server" >
                            </asp:DropDownList>
                        </div>
        </div>
             <div class="col-md-12 col-sm-12 col-xs-12">

<!-- Custom Tabs -->
          <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
              <li class="active"><a href="#tab_1" data-toggle="tab">Patient Initial Visit</a></li>
              <li><a href="#tab_2" data-toggle="tab">Patient Follow up Visit</a></li>              
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
                                <asp:Label runat="server" style="padding-left: 10px" CssClass="label-right">
                                    Cohort Month:</asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <input id="txtcohortmnth" size="10" name="CohortMonth" runat="server" readonly="readonly" />
                            </div>
                         <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <asp:Label runat="server" style="padding-left: 10px" CssClass="label-right">
                                    Cohort Year:</asp:Label>
                            </div>
                         <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <input id="txtcohortyear" size="10" name="CohortYear" runat="server" readonly="readonly" />
                             </div>
                         <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                <asp:Label ststyle="padding-left: 10px" CssClass="label-right" runat="server">
                                    Regimen:</asp:Label>
                        </div>
                         <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                         <div class="input-group">
                                     <asp:TextBox ID="txtFirstARTRegimen" MaxLength="50" runat="server" Enabled="False" Width="90%" class="form-control"></asp:TextBox>
                                     <span class="input-group-btn pull-left">
                                     <asp:Button ID="btnfirstregimen" MaxLength="20" Width="60px" runat="server" Text="..." 
                                            class="btn btn-primary btn-flat" onclick="btnRegimen_Click"  >
                                        </asp:Button>                                            
                                    </span>                                        
                                        </div>
                                
                            </div>
                        </div>
                        <div  class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label style="padding-left: 10px" CssClass="label-right" id="Label2" runat="server">
                                    Baseline Viral load:</asp:Label>
                         </div>
                         
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="txtBaseViral" Width="100px" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <asp:Label style="padding-left: 10px" CssClass="label-right" id="Label3" runat="server">
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
                                   <asp:Label style="padding-left: 10px" CssClass="label-right" id="Label6" runat="server">
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
                                        <asp:Label style="padding-left: 10px" CssClass="label-right" id="Label1" runat="server">
                                            Purpose:</asp:Label>                                        
                          </div>                         
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddlpurpose" runat="server" Width="110px" Style="z-index: 2" class="form-control">
                                        </asp:DropDownList>
                          </div>
                          <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                        <asp:Label style="padding-left: 10px" CssClass="label-right" runat="server">
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
                                        <asp:Label style="padding-left: 10px" CssClass="label-right" runat="server">
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
                  <table class="table" style="padding-left:10px;width:100%;">
                      <tr>
                      <td style="width:100%;">
                      <div class="row">
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <asp:Label CssClass="label-right" style="padding-left: 10px" id="Label63" runat="server">
                                        Scheduled:</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="rdoshdulyes" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="Y" name="ScheduledVisit" />
                                <label for="y">
                                    Yes</label>
                                <input id="rdoshdulno" runat="server" class="minimal" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                    type="radio" value="N" name="ScheduledVisit" />
                                <label for="n">
                                    No</label>
                        </div>
                        <div class="col-md-5 col-sm-12 col-xs-12 form-group" >
                        <asp:Label CssClass="label-right" style="padding-left: 10px;" id="Label64" runat="server">
                                        Visit by:</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group" style="float:right">
                       
                        <asp:ListBox ID="ddlfwupvisitby" class="form-control select2"  multiple="multiple" Width="60%" runat="server">
                        <asp:ListItem Text="Alabama" Value="Alabama"></asp:ListItem>
                        <asp:ListItem Text="California" Value="California"></asp:ListItem>
                        <asp:ListItem Text="Delaware" Value="Delaware"></asp:ListItem>
                        <asp:ListItem Text="Tennessee" Value="Tennessee"></asp:ListItem>
                        </asp:ListBox>
                         
                        <%--<asp:DropDownList ID="ddlfwupvisitby" class="form-control select2"  multiple="multiple" Width="60%" runat="server" >
                        <asp:ListItem Text="Alabama" Value="Alabama"></asp:ListItem>
                        <asp:ListItem Text="California" Value="California"></asp:ListItem>
                        <asp:ListItem Text="Delaware" Value="Delaware"></asp:ListItem>
                        <asp:ListItem Text="Tennessee" Value="Tennessee"></asp:ListItem>
                            </asp:DropDownList>--%>
                        </div>
                        </div>
                      </td>
                      </tr>
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
                        <uc2:ucVitals ID="UCFwupVitalSigns" runat="server" />
                        </div>
                        </div>
                        </div>
                     </td>
                     </tr>
                    <tr>
                        <td id="tdPregnant" runat="server" style="width:100%;">
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
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <input id="rdopregnantYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD'); "
                                    type="radio" value="Y" name="pregnant" runat="server" />
                                <label>
                                    Yes</label>
                                <input id="rdopregnantNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('spanEDD');"
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
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <input id="rdoDeliveredYes" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); show('spanEDD'); "
                                    type="radio" value="Y" name="pregnant" runat="server" />
                                <label>
                                    Yes</label>
                                <input id="rdoDeliveredNo" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); hide('spanEDD');"
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
                          
                        </td>
                    </tr>

                  </table>
                
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
    </form>
</body>
</html>
