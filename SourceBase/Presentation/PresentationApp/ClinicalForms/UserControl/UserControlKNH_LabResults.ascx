<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_LabResults.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_LabResults" %>
<style type="text/css">
.vScroll
{
    max-height: 500px;
    overflow-y: scroll;
}
</style>


<div style="height: 900px; background-color: #FFFFFF;">
<table width="100%">
<tr>
<td>
<div class="GridView whitebg" style="cursor: pointer;">
            <div class="grid">
                <div class="rounded">
                    <div class="top-outer">
                        <div class="top-inner">
                            <div class="top">
                                <h2 align="center">
                                    Latest Lab Results</h2>
                            </div>
                        </div>
                    </div>
                    <div class="mid-outer">
                        <div class="mid-inner">
                            <div class="mid" style="height: auto; overflow: auto">
                                <div id="div2" class="GridView whitebg vScroll">
                                    <asp:GridView ID="grdLatestResults" runat="server" Width="100%"  BorderWidth="0"
                                        GridLines="None" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>                                       
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="bottom-outer">
                        <div class="bottom-inner">
                            <div class="bottom">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</td>
</tr>
</table>

<hr style="height:3px;border:none;color:#333;background-color:#800000;" />

<table cellpadding="2" width="100%">
    <tr align="left">
        <td>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                Text="Highest CD4:"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblHighestCD4" runat="server" Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblHighestCD4Date" runat="server" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Lowest CD4:"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblLowestCD4" runat="server" Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblLowestCD4Date" runat="server" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr align="left">
        <td colspan="3">
        <div class="GridView whitebg" style="cursor: pointer;">
            <div class="grid">
                <div class="rounded">
                    <div class="top-outer">
                        <div class="top-inner">
                            <div class="top">
                                <h2 align="center">
                                    Last 3 CD4s</h2>
                            </div>
                        </div>
                    </div>
                    <div class="mid-outer">
                        <div class="mid-inner">
                            <div class="mid" style="height: auto; overflow: auto">
                                <div id="div-gridview" class="GridView whitebg vScroll">
                                    <asp:GridView ID="grdCD4" runat="server" Width="100%"  BorderWidth="0"
                                        GridLines="None" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                      
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="bottom-outer">
                        <div class="bottom-inner">
                            <div class="bottom">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            </td>
    </tr>
    
</table>

<hr style="height:3px;border:none;color:#333;background-color:#800000;" />

<table width="100%" cellpadding="2">
<tr align="left">
<td>
            <asp:Label ID="Label4" runat="server" Font-Bold="True" 
                Text="Highest Viral Load:"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblHighestViralLoad" runat="server" Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblHighestVLDate" runat="server" Font-Bold="True"></asp:Label>
        </td>
</tr>
<tr align="left">
<td>
            <asp:Label ID="Label6" runat="server" Font-Bold="True" 
                Text="Lowest viral Load:"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblLowestViralLoad" runat="server" Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblLowestVLDate" runat="server" Font-Bold="True"></asp:Label>
        </td>
</tr>
<tr align="left">
<td colspan="3">
            <div class="GridView whitebg" style="cursor: pointer;">
            <div class="grid">
                <div class="rounded">
                    <div class="top-outer">
                        <div class="top-inner">
                            <div class="top">
                                <h2 align="center">
                                    Last 3 Viral Loads</h2>
                            </div>
                        </div>
                    </div>
                    <div class="mid-outer">
                        <div class="mid-inner">
                            <div class="mid" style="height: auto; overflow: auto">
                                <div id="div1" class="GridView whitebg vScroll">
                                    <asp:GridView ID="grdViralLoad" runat="server" Width="100%"  BorderWidth="0"
                                        GridLines="None" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>                                       
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="bottom-outer">
                        <div class="bottom-inner">
                            <div class="bottom">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </td>
</tr>
</table>



</div>

