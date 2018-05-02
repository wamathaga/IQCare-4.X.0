<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="IQCare.Web.Billing.TestPage" %>
<%@ Register src="TransactionReversal.ascx" tagname="TransactionReversal" tagprefix="BillingTR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
   
    <BillingTR:TransactionReversal ID="ReversalForm" runat="server"  IsApprovalMode="TRUE" PatientID="0"/> 
    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px">
    </asp:DetailsView>
</asp:Content>
