<%@ Page Language="c#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmPatient_Home" Title="untitled page" MaintainScrollPositionOnPostback="true"
    CodeBehind="frmpatient_home.aspx.cs" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%--<%@ MasterType VirtualPath="~/ClinicalForms/ClinicalHomeHeaderFooter.master" %>--%>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%--<%@ Register src="wucDebitNote.ascx" tagname="wucDebitNote" tagprefix="uc1" %>--%>
<asp:Content ID="content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        function fnSetSession(url) {
            //var result=frmFindAddPatient.SetPatientId_Session(url).value;
            //alert(url);
            window.open(url, 'ab', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=800,height=700,scrollbars=yes');

        }
        function fnSetalert(url) {
            //alert('');
        }
    </script>
    <style type="text/css">
        .lb-sm
        {
            font-size: 12px;
        }
        .lb-md
        {
            font-size: 16px;
        }
        
        .lb-lg
        {
            font-size: 20px;
        }
        .aspxlb-sm
        {
            font-size: 11px;
        }
        .voffset
        {
            margin-top: 2px;
        }
        .voffset1
        {
            margin-top: 5px;
        }
        .voffset2
        {
            margin-top: 10px;
        }
        .voffset3
        {
            margin-top: 15px;
        }
        .voffset4
        {
            margin-top: 30px;
        }
        .voffset5
        {
            margin-top: 40px;
        }
        .voffset6
        {
            margin-top: 60px;
        }
        .voffset7
        {
            margin-top: 80px;
        }
        .voffset8
        {
            margin-top: 100px;
        }
        .voffset9
        {
            margin-top: 150px;
        }
    </style>
    <asp:UpdatePanel ID="UpdateMasterLink" runat="server">
        <ContentTemplate>
            <div class="box-body">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary box-solid">
                            <div class="box-header">
                                <div class="row">
                                    <div class="col-md-6">
                                        <h3 class="box-title" id="tHeading">
                                            Patient Home</h3>
                                    </div>
                                    <div class="col-md-6 text-right">
                                        <h3 class="box-title" id="lblServiceArea" runat="server">
                                            Service Area</h3>
                                    </div>
                                </div>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                                <%--Main Content Start--%>
                                <div class="row" align="center">
                                    <br />
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                        <asp:Button ID="btnReactivate" runat="server" Text="Reactivate Patient" CssClass="btn btn-primary"
                                            Style="height: 30px; width: 17%; text-align: left;" Visible="false" OnClick="btnReactivate_Click" />
                                        <span class="glyphicon glyphicon-check" style="vertical-align: sub; margin-left: -3%;
                                            margin-right: 2%; color: #fff"></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap">
                                        <label class="control-label">
                                            Patient Name:</label>
                                        <asp:Label ID="lblpatientname" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
                                        <label class="control-label">
                                            DOB:</label>
                                        <asp:Label ID="lbldob" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
                                        Age:
                                        <asp:Label ID="lblage" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
                                        <label class="control-label">
                                            Sex:</label>
                                        <asp:Label ID="lblgender" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
                                        <label class="control-label">
                                            Patient Status:</label>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                        <asp:Label ID="lblptnstatus" runat="server" Text="" Font-Bold="true"></asp:Label><br />
                                        <asp:Label ID="lbloutofstock" runat="server" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <asp:LinkButton ID="btnTechChange" runat="server" OnClick="btnTechChange_Click">Find Different Patient
                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-md-8 col-sm-12 col-xs-12 form-group" align="center">
                                        <div id="divAddChildren" style="background-color: white; display: none;">
                                            <asp:Button ID="btnAddChildren" runat="server" Text="Add/Show Children" OnClick="btnAddChildren_Click"
                                                CssClass="btn btn-primary" Height="30px" Width="70%" Style="text-align: left;" />
                                            <span class="glyphicon glyphicon-blackboard" style="vertical-align: sub; margin-left: -11%;
                                                margin-right: 2%; color: #fff"></span>
                                            <asp:Button ID="btnShowMother" runat="server" Text="Show Mother" Visible="false"
                                                OnClick="btnShowMother_Click" CssClass="btn btn-primary" Height="30px" Width="50%"
                                                Style="text-align: left;" />
                                            <span id="spanShowMother" runat="server" class="glyphicon glyphicon-blackboard" style="vertical-align: sub;
                                                margin-left: -8%; margin-right: 2%; color: #fff; visibility: hidden;"></span>
                                            <asp:HiddenField ID="hpIQNumber" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <asp:Label CssClass="required" ID="lblTechVisited" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                        <telerik:radajaxpanel id="pnlnotification" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                        <div id="TabDiv" runat="server" style="padding-right: 5px;">
                                            <!-- Custom Tabs start -->
                                            <div class="nav-tabs-custom">
                                                <ul class="nav nav-tabs">
                                                    <li class="active"><a href="#tabGeneral" data-toggle="tab">General</a></li>
                                                    <li><a href="#tabDynamicHeader" data-toggle="tab">
                                                        <asp:Label ID="lblDynamicHeader" runat="server" Text="Label"></asp:Label></a></li>
                                                    <li><a href="#tabPatientCosts" data-toggle="tab">Patient Costs</a></li>
                                                    <li><a href="#tabPatientAdherence" data-toggle="tab">Patient Adherence</a></li>
                                                </ul>
                                                <div class="tab-content">
                                                    <div class="tab-pane active" id="tabGeneral">
                                                        <div class="row box-header">
                                                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                                <h5 class="box-title">
                                                                    Clinical Information</h5>
                                                            </div>
                                                            <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                                                <asp:LinkButton ID="lnkPharmacyNotes" runat="server" OnClick="lnkPharmacyNotes_Click">View Prescription Notes
                                                                                <span class="glyphicon glyphicon-align-justify" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row" align="center">
                                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                <asp:Label ID="lblPregnant" runat="server" Visible="False"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-7">
                                                                <%--left--%>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            Last Recorded WAB:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblWABStage" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            WHO Stage:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblWHOStage" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            Weight:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblweight" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            Lowest CD4:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblLowestCD4" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            Most Recent CD4:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblRecentCD4" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            Next CD4 Due:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblCD4Due" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            HCT:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblHB" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            HB:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblHCT" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            AST:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblAST" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            CR:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblCr" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            Syphilis:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblSyphilis" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            Pregnancy Test:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblPregnancyTest" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            <asp:Label ID="lblnexthivstatuscheck" runat="server" Visible="False" Text="Next HIV Status Check"></asp:Label></label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblnexthivscheck" runat="server" Width="95%" CssClass="control-label aspxlb-sm"
                                                                            Visible="False"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row box-header">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                        <h5 class="box-title">
                                                                            Contact Information</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            Patient Phone:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblpatientphone" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            <asp:Label ID="lblShowAddress" runat="server"></asp:Label></label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lbladdress" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            <asp:Label ID="lblEmrContact" runat="server"></asp:Label></label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblemergencycontact" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            <asp:Label ID="lblEmrPhone" runat="server"></asp:Label></label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblemgphone" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row box-header">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                        <h5 class="box-title">
                                                                            Family Information</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-5">
                                                                        <label class="control-label lb-sm">
                                                                            No. of related members enrolled:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblfamilyEnrolled" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-5">
                                                                        <label class="control-label lb-sm">
                                                                            No. of related members on ART:</label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Label ID="lblfamilyArt" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label lb-sm">
                                                                            <asp:HyperLink ID="hlFamilyInfo" runat="server" CssClass="utility" Visible="False">Family Information</asp:HyperLink></label>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Image ID="imgfamily" runat="server" Visible="False" />
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1" align="center">
                                                                    <div class="col-md-12">
                                                                        <asp:Label ID="lblhivpositivemother" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <%--right--%>
                                                                <div class="row box-header">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                        <h5 class="box-title">
                                                                            Weight and BMI over time</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1" align="center">
                                                                    <div class="col-md-12">
                                                                        <chart:WebChartViewer ID="WebChartViewerWeight" runat="server" Height="350px" Width="98%" />
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1" align="center">
                                                                    <div class="col-md-12">
                                                                        <asp:Button ID="btnWeightChart" runat="server" Text="View Larger Graph" OnClick="btnWeightChart_Click"
                                                                            Style="height: 30px;" CssClass="btn btn-primary" />
                                                                    </div>
                                                                </div>
                                                                <div class="row box-header">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                        <h5 class="box-title">
                                                                            CD4 Count and Viral Load over time</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1" align="center">
                                                                    <div class="col-md-12">
                                                                        <chart:WebChartViewer ID="WebChartViewerCD4VL" runat="server" Height="450px" Width="300px" />
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1" align="center">
                                                                    <div class="col-md-12">
                                                                        <asp:Button ID="btnCD4Graph" runat="server" Text="View Larger Graph" OnClick="btnCD4Graph_Click"
                                                                            Style="height: 30px;" CssClass="btn btn-primary" />
                                                                    </div>
                                                                </div>
                                                                <div class="row voffset1" align="center">
                                                                    <div class="col-md-12">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="tab-pane" id="tabDynamicHeader">
                                                        <asp:Panel ID="thePnl" Width="100%" runat="server">
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="tab-pane" id="tabPatientCosts">
                                                        <div class="row box-header">
                                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                <h5 class="box-title">
                                                                    <asp:Label ID="Label1" runat="server" Text="HIV Care Program Costs"></asp:Label></h5>
                                                            </div>
                                                        </div>
                                                        <div class="row voffset1">
                                                            <div class="col-md-12">
                                                                <b>Patient Cost Per Debit Note</b>
                                                            </div>
                                                        </div>
                                                        <div class="row voffset1">
                                                            <div class="col-md-12">
                                                                <div class="grid">
                                                                    <div id="div-gridview" class="gridviewdebitnote whitebg">
                                                                        <asp:GridView ID="GridViewSummary" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                                                                            GridLines="None" OnRowCommand="GridViewSummary_RowCommand">
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="50%">
                                                                </td>
                                                                <td width="50%" style="text-align: center">
                                                                    <html>
                                                                    <head id="Head1">
                                                                        <%-- <title>Facility.Reports</title>
                                                                            <style type="text/css">
                                                                            html, body {
                                                                                height: 100%;
                                                                                overflow: auto;
                                                                            }
                                                                            body {
                                                                                padding: 0;
                                                                                margin: 0;
                                                                            }
                                                                            #silverlightControlHost {
                                                                                height: 100%;
                                                                                text-align:center;
                                                                            }
                                                                            </style>--%>
                                                                        <script type="text/javascript" src="../Incl/Silverlight.js"></script>
                                                                        <script type="text/javascript">
                                                                            function onSilverlightError(sender, args) {
                                                                                var appSource = "";
                                                                                if (sender != null && sender != 0) {
                                                                                    appSource = sender.getHost().Source;
                                                                                }

                                                                                var errorType = args.ErrorType;
                                                                                var iErrorCode = args.ErrorCode;

                                                                                if (errorType == "ImageError" || errorType == "MediaError") {
                                                                                    return;
                                                                                }

                                                                                var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

                                                                                errMsg += "Code: " + iErrorCode + "    \n";
                                                                                errMsg += "Category: " + errorType + "       \n";
                                                                                errMsg += "Message: " + args.ErrorMessage + "     \n";

                                                                                if (errorType == "ParserError") {
                                                                                    errMsg += "File: " + args.xamlFile + "     \n";
                                                                                    errMsg += "Line: " + args.lineNumber + "     \n";
                                                                                    errMsg += "Position: " + args.charPosition + "     \n";
                                                                                }
                                                                                else if (errorType == "RuntimeError") {
                                                                                    if (args.lineNumber != 0) {
                                                                                        errMsg += "Line: " + args.lineNumber + "     \n";
                                                                                        errMsg += "Position: " + args.charPosition + "     \n";
                                                                                    }
                                                                                    errMsg += "MethodName: " + args.methodName + "     \n";
                                                                                }

                                                                                throw new Error(errMsg);
                                                                            }
                                                                        </script>
                                                                    </head>
                                                                    <body>
                                                                        <table class="table-condensed" style="height: 100%; width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <div id="silverlightControlHost">
                                                                                        <%--                                                                                        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2"
                                                                                            width="100%" height="100%">
                                                                                            <param name="source" value="../ClientBin/Facility.Reports.xap" />
                                                                                            <param name="onError" value="onSilverlightError" />
                                                                                            <param name="minRuntimeVersion" value="3.0.40818.0" />
                                                                                            <param name="autoUpgrade" value="true" />
                                                                                            <param name="initparams" value="<%=string.Format("TranDateFrom={0}, TranDateTo={1}, ReportID={2}, PatientID={3}", "", "", 2, Session["PatientId"]) %>" />
                                                                                            <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none">
                                                                                                <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                                                                                                    style="border-style: none" />
                                                                                        </object>--%>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </body>
                                                                    </html>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width: 100%" width="50%">
                                                                    <div class="grid">
                                                                        <div id="div1" class="gridviewDebitNote whitebg">
                                                                            <asp:GridView ID="GridViewTran" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                                                                                GridLines="None" AllowSorting="True">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" HeaderStyle-Width="15%"
                                                                                        DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="True" />
                                                                                    <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="25%"
                                                                                        ReadOnly="True" />
                                                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" HeaderStyle-Width="15%" />
                                                                                    <asp:BoundField DataField="Adminsitration" HeaderText="Administration" ReadOnly="True"
                                                                                        HeaderStyle-Width="15%" />
                                                                                    <asp:BoundField DataField="Cost" HeaderText="Actual Price" ReadOnly="True" HeaderStyle-Width="15%" />
                                                                                    <asp:BoundField DataField="ChargedPrice" HeaderText="Charged Price" ReadOnly="True"
                                                                                        HeaderStyle-Width="15%" />
                                                                                </Columns>
                                                                                <EmptyDataTemplate>
                                                                                    <span style="text-align: center">Select a debit note above to see details here.
                                                                                    </span>
                                                                                </EmptyDataTemplate>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="tab-pane" id="tabPatientAdherence">
                                                        <div class="row box-header">
                                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                <h5 class="box-title">
                                                                    <asp:Label ID="Label2" runat="server" Text="Patient Adherence"></asp:Label></h5>
                                                            </div>
                                                        </div>
                                                        <div class="row voffset1">
                                                            <div class="col-md-12">
                                                                <b>ARV Adherence</b>
                                                            </div>
                                                        </div>
                                                        <div class="row voffset1">
                                                            <div class="col-md-12">
                                                                <div class="grid">
                                                                    <div id="div2" class="gridviewDebitNote whitebg">
                                                                        <asp:GridView ID="grdarvadherence" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                            CssClass="table table-bordered table-hover" GridLines="None" AllowSorting="True"
                                                                            OnDataBound="grdarvadherence_OnRowDataBound" EmptyDataText="No Data Available"
                                                                            OnRowDataBound="grdarvadherence_RowDataBound">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="OrderedbyDate" HeaderText="Ordered Date" HeaderStyle-Width="25%"
                                                                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="True"
                                                                                    ItemStyle-Width="25%" />
                                                                                <asp:BoundField DataField="DrugName" HeaderText="DrugName" HeaderStyle-Width="38%"
                                                                                    ItemStyle-HorizontalAlign="Center" ReadOnly="True" ItemStyle-Width="40%" />
                                                                                <asp:BoundField DataField="OrderedQuantity" HeaderText="Ordered Qty" ReadOnly="True"
                                                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" ItemStyle-Width="12%" />
                                                                                <asp:BoundField DataField="Pillcount" HeaderText="Pillcount" ReadOnly="True" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderStyle-Width="12%" ItemStyle-Width="12%" />
                                                                                <asp:TemplateField HeaderText="Adherence (%)" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderStyle-Width="25%" ItemStyle-Width="25%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbladherance" runat="server" Width="100%"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row voffset1">
                                                            <div class="col-md-12">
                                                                <b>Cotrimoxazole Adherence</b>
                                                            </div>
                                                        </div>
                                                        <div class="row voffset1">
                                                            <div class="col-md-12">
                                                                <div class="grid">
                                                                    <div id="div3" class="gridviewDebitNote whitebg">
                                                                        <asp:GridView ID="grdCotriadherence" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                                                                            Width="100%" GridLines="None" AllowSorting="True" OnDataBound="grdCotriadherence_OnRowDataBound"
                                                                            EmptyDataText="No Data Available">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="OrderedbyDate" HeaderText="Ordered Date" HeaderStyle-Width="25%"
                                                                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="True" />
                                                                                <asp:BoundField DataField="DrugName" HeaderText="DrugName" HeaderStyle-Width="38%"
                                                                                    ItemStyle-HorizontalAlign="Center" ReadOnly="True" />
                                                                                <asp:BoundField DataField="OrderedQuantity" HeaderText="Ordered Qty" ReadOnly="True"
                                                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" />
                                                                                <asp:BoundField DataField="Pillcount" HeaderText="Pill count" ReadOnly="True" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderStyle-Width="12%" />
                                                                                <asp:BoundField HeaderText="Adherence (%)" ReadOnly="True" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderStyle-Width="25%" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Custom Tabs End -->
                                        </div>
                                    </div>
                                    <div>
                                        <div class="border" style="width: 100%">
                                            <table width="100%" border="0">
                                                <tbody align="left">
                                                    <%--<div class="tabContents">--%>
                                                    <tr valign="top">
                                                        <td style="width: 60%; height: auto">
                                                            <asp:MultiView ID="TabView" ActiveViewIndex="0" runat="server">
                                                                <asp:View ID="ViewGeneral" runat="server">
                                                                </asp:View>
                                                                <asp:View ID="ViewPMTCT" runat="server">
                                                                    <table width="100%" id="tblpmtct" runat="server">
                                                                        <tr>
                                                                            <td>
                                                                                <div class="row box-header">
                                                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                                        <h5 class="box-title">
                                                                                            PMTCT Identification Information</h5>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            <asp:Label ID="lblanc" runat="server">ANC Number:</asp:Label></label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblancno" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            <asp:Label ID="lblpmtct" runat="server">PMTCT Number:</asp:Label></label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblpmtctno" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            <asp:Label ID="lbladmission" runat="server">Admission Number:</asp:Label></label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lbladmissionno" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            <asp:Label ID="lbloutpatient" runat="server">Outpatient Number:</asp:Label></label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lbloutpatientno" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row box-header">
                                                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                                        <h5 class="box-title">
                                                                                            PMTCT Information</h5>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            LMP</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblLMP" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            EDD</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblEDD" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            Gestational Age (Weeks)</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblGestAge" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            Current ARV Prophylaxis Regimen</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblARVProRegimen" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            Current ARV Prophylaxis Start Date</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblARVProStartDate" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            TB Status</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblTBStatus" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            Partner HIV Status</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblPartnerHIVStatus" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            Last Visit</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblLastVisit" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row box-header">
                                                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                                        <h5 class="box-title">
                                                                                            Exposed Infant Information</h5>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            Date of Birth (Current Pregnancy)</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblDeliveryDate" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            Infant Prophylaxis Regimen</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblInfantProphylaxisRegimen" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            Feeding Option</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblFeedingOption" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-6">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-12">
                                                                                        <div id="divResult" runat="server">
                                                                                            <asp:Literal ID="Literal1" runat="server" Text=""></asp:Literal>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-12">
                                                                                        <asp:Panel ID="DynControlPMTCT" runat="server">
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:View>
                                                                <asp:View ID="ViewART" runat="server">
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server" id="tbhiv">
                                                                        <tr>
                                                                            <td>
                                                                                <div class="row box-header">
                                                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                                        <h5 class="box-title">
                                                                                            HIVCare Static Form Identification Information</h5>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            <asp:Label ID="lblenroll" runat="server">Enroll#:</asp:Label></label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblptnenrollment" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            <asp:Label ID="lblClinicNo" runat="server"></asp:Label></label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblexistingid" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row box-header">
                                                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                                        <h5 class="box-title">
                                                                                            ART Information</h5>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            ART/Palliative Care:</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblprogram" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            Current ARV Regimen:</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblarvregimen" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            Current ARV Start Date:</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblarvstartdate" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            ARV Start Date At This Facility:</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblaidsrstartdate" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            Historical ART Start Date:</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblhistoricalsdate" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            Last Visit:</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lbllstvisit" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            Next Appointment:</label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="lblnextapp" runat="server" Width="95%" CssClass="control-label aspxlb-sm"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <label class="control-label lb-sm">
                                                                                            <asp:HyperLink ID="hlFollowupeducation" Visible="false" CssClass="utility" runat="server">Follow-up Education</asp:HyperLink>
                                                                                        </label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Image ID="imgFollowupeducation" Visible="false" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row voffset1">
                                                                                    <div class="col-md-12">
                                                                                        <asp:Panel ID="DynControlsARV" runat="server">
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:View>
                                                                <asp:View ID="ViewDynamic" runat="server">
                                                                    <%--<asp:Panel id="thePnl" width="100%" runat="server"></asp:Panel>--%>
                                                                </asp:View>
                                                            </asp:MultiView>
                                                        </td>
                                                    </tr>
                                                    <%--</div>--%>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <%--Main Content End--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddChildren"></asp:PostBackTrigger>
            <%--<asp:PostBackTrigger ControlID="GridViewSummary"></asp:PostBackTrigger>--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
