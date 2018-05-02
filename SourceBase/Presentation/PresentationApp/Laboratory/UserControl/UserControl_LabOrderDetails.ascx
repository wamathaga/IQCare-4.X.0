<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_LabOrderDetails.ascx.cs" Inherits="PresentationApp.Laboratory.UserControl.UserControl_LabOrderDetails" %>
<table cellspacing="6" cellpadding="0" width="100%" border="0">
                                        <tr class="form">
                                            <td align="center" valign="middle" width="50%">
                                                <label>
                                                    Lab Order No:</label><asp:TextBox ID="txtlabnumber"  ReadOnly="true" runat="server" Width="120px"></asp:TextBox>
                                            </td>
                                            <td align="center" valign="middle">
                                                <label for="LabtobeDone" class="right35">
                                                    Lab Order Date:</label>
                                                <telerik:RadDatePicker ID="txtSpecLabOrderdt" runat="server" Skin="Office2007" Enabled="false"
                                                    Width="120px">
                                                    <DateInput ID="DateInputSpecLabOrderdt" DateFormat="dd-MMM-yyyy" runat="server">
                                                    </DateInput>
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                    </table>