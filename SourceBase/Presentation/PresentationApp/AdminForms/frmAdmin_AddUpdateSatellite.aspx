<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="AdminForms_frmAdmin_AddUpdateSatellite" CodeBehind="frmAdmin_AddUpdateSatellite.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id ="AddUpdateSatellite" method="post" runat="server" title="Add/Update Satellite">--%>
    <div class="container-fluid">
        <h5 class="margin">
            <asp:Label ID="lblTitle" runat="server" Text="Add/Edit LaboratoryTest"></asp:Label></h5>
        <div class="center" style="padding: 5px;">
            <div class="border formbg center">
                <table class="table-condensed" width="100%">
                    <tbody>
                        <tr>
                            <td class="pad5 whitebg border">
                                <label class="right40" for="SatelliteID">
                                    Satellite ID:</label>
                                <asp:TextBox ID="txtSatelliteID" runat="server"></asp:TextBox>
                            </td>
                            <td class="pad5 whitebg border">
                                <label class="right40" for="SatelliteName">
                                    Satellite Name:</label>
                                <asp:TextBox ID="txtSatelliteName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="td1" class="pad5 whitebg border" runat="server">
                                <label class="right40" for="Status">
                                    Status:</label>
                                <asp:DropDownList ID="ddStatus" runat="server">
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td id="td2" class="pad5 whitebg border" runat="server">
                                <label class="right40" for="Priority">
                                    Priority:</label>
                                <asp:TextBox ID="txtPriority" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 center" colspan="2">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-erase" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
