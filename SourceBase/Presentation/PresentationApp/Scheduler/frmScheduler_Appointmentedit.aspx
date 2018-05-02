<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmScheduler_Appointmentedit" Title="Untitled Page" CodeBehind="frmScheduler_Appointmentedit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            Change Appointment for Mary Adams, 123456789</h1>
        <div style="position: absolute; left: 634px; top: 16px; width: 299px; text-align: right;">
            <a href="Appointment.aspx" class="button">SCHEDULER MAIN</a>
        </div>
        <div class="border center">
            <br />
            <table cellspacing="0" cellpadding="0" width="70%">
                <tbody>
                    <tr>
                        <td class="pad5" valign="middle" width="65%">
                            <label class="right30">
                                Appointment Date:
                            </label>
                            <asp:TextBox ID="txtDate" runat="server">15/06/2006</asp:TextBox><img onclick="w_displayDatePicker('<%= txtDate.ClientID %>');"
                                height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22"
                                border="0" style="vertical-align: bottom; margin-bottom: 2px;"><span class="smallerlabel">(DD/MM/YYYY)</span>
                        </td>
                        <td class="pad5" valign="middle">
                            <label class="right25">
                                Purpose:
                            </label>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem>Follow Up</asp:ListItem>
                                <asp:ListItem>Lab Tests</asp:ListItem>
                                <asp:ListItem>Treatment Preparation</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5" valign="middle">
                            <label class="right30">
                                Status:
                            </label>
                            <asp:DropDownList ID="DropDownList2" runat="server">
                                <asp:ListItem>Pending</asp:ListItem>
                                <asp:ListItem>Met</asp:ListItem>
                                <asp:ListItem>Missed</asp:ListItem>
                                <asp:ListItem>Care Ended</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="pad5" valign="middle">
                            <label class="right27">
                                Provider:
                            </label>
                            <asp:DropDownList ID="DropDownList3" runat="server">
                                <asp:ListItem>Dr. Burrows</asp:ListItem>
                                <asp:ListItem>Dr. Johnson</asp:ListItem>
                                <asp:ListItem>Lab</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <br />
            <input type="submit" name="submit" value="Save" id="btnSave" runat="server" onserverclick="btnSave_ServerClick" />
            <input type="submit" name="delete" value="Delete" id="delect" runat="server" onclick="confirm('Are you sure you want to delete?');" />
            <input type="reset" name="cancel" value=" Cancel " id="btnReset" runat="server" />
            <br />
            <br />
        </div>
    </div>
</asp:Content>
