<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master"  AutoEventWireup="true" Inherits="Reports_frmClinical_PatientSummary"
    CodeBehind="frmClinical_PatientSummary.aspx.cs" Title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <%-- <script src="../crystalreportviewers13/js/crviewer/crv.js" type="test/javascript"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div class="content-wrapper">
        <div class="box-body">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title">
                                Patient Profile Summary</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                            <%--Main Content--%>
                            <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true" HasCrystalLogo="False"
                                GroupTreeStyle-ShowLines="false" EnableToolTips="true" HasDrillUpButton="False"
                                HasSearchButton="False" HasToggleGroupTreeButton="False" HasZoomFactorList="False"
                                EnableDatabaseLogonPrompt="true" HasExportButton="False" HasPrintButton="true"
                                Height="50px" Width="350px" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
