<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_Signature.ascx.cs" Inherits="PresentationApp.GreenCard.UserControls.UC_Signature" %>

<table width="100%">
    <tr>
        <td align="right" width="50%">
            <asp:Label ID="Label1" runat="server" CssClass="form-control label-right"  Text="Signature:"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblSignature" CssClass="form-control label-left" runat="server" ></asp:Label>
        </td>
    </tr>
</table>

