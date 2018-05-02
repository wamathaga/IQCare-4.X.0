<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" Inherits="frmFacilityHomenew" Title="Untitled Page"
    EnableViewState="true" Codebehind="frmFacilityHomenew.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div>
        <asp:Panel ID="pnl_FacTexhAreas" runat="server" Width="100%">
            <table width="100%">
                <tr>
                    <td>
                        <div class="GridView">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" GridLines="Both">
                                <Columns>
                                    <asp:BoundField HeaderText="sanjay1" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
