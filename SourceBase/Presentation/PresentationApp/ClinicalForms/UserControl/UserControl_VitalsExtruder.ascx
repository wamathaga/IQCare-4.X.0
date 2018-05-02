<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_VitalsExtruder.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControl_VitalsExtruder" %>
<%@ Register Src="UserControl_AllergyExtruder.ascx" TagName="UserControl_AllergyExtruder"
    TagPrefix="uc1" %>
<%@ Register Src="UserControlKNH_LabResults.ascx" TagName="UserControlKNH_LabResults"
    TagPrefix="uc2" %>
<%@ Register Src="UserControl_ARVHistoryExtruder.ascx" TagName="UserControl_ARVHistoryExtruder"
    TagPrefix="uc3" %>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
.vScroll
{
    max-height: 500px;
    overflow-y: scroll;
}
</style>
<div style="height: 900px; background-color: #FFFFFF;">
    <table cellpadding="2">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Sex:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSex" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="DOB:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDOB" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Age:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAge" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Phone:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPhone" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="District:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDistrict" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="BMI:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBMI" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="IPTSDLable" runat="server" Font-Bold="True" Text="IPT Start Date: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="IPTSD" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="IPTEDLable" runat="server" Font-Bold="True" Text="IPT End Date: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="IPTED" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ARTDateLabel" runat="server" Font-Bold="True" Text="ART Start Date: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblArtStartDt" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPatientClassification" runat="server" Font-Bold="True" Text="Patient Classification : "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPatientClassificationVal" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDifferenciatedCare" runat="server" Font-Bold="True" Text="Differenciated Care : "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDifferenciatedCareVal" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
    </table>
    <hr style="height: 3px; border: none; color: #333; background-color: #800000;" />
    <div class="vScroll">
    <uc1:UserControl_AllergyExtruder ID="UserControl_AllergyExtruder1" runat="server" />
    </div>
    <hr style="height: 3px; border: none; color: #333; background-color: #800000;" />
     <div class="vScroll">
    <uc3:UserControl_ARVHistoryExtruder ID="UserControl_ARVHistoryExtruder1" runat="server" />
    </div>
</div>
