<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmBilling_Reciept.aspx.cs" Inherits="IQCare.Web.Billing.frmBilling_Reciept" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="frmBilling" runat="server">
    <div>
        <CR:CrystalReportViewer runat="server" AutoDataBind="True" 
            ID="billingRptViewer" HasCrystalLogo="False" HasDrillUpButton="False" 
            ToolPanelView="None" HasDrilldownTabs="False" PrintMode="ActiveX" 
            onunload="billingRptViewer_Unload"/>
    </div>
    </form>
</body>
</html>
