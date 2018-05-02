<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPharmacy_StockTransactions.aspx.cs"
    Inherits="PresentationApp.PharmacyDispense.frmPharmacy_StockTransactions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Style/_assets/css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../Style/_assets/css/round.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<asp:ScriptManager ID="mst" runat="server" EnablePageMethods="true" EnablePartialRendering="true">
    </asp:ScriptManager>--%>
        <style type="text/css">
            #mainMaster
            {
                width: 100% !important;
            }
            #containerMaster
            {
                width: 1200px !important;
            }
            #ulAlerts
            {
                width: 1180px !important;
            }
            #divPatientInfo123
            {
                width: 1180px !important;
            }
            #Img2
            {
                height: 22px;
            }
        </style>
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr valign="top">
                            <td class="data-control">
                                <table style="width: 100%">
                                    <tr>
                                        <td width="33%">
                                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Transaction Type:"></asp:Label>
                                            &nbsp;<asp:DropDownList ID="DropDownList7" runat="server" Width="150px">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Receive</asp:ListItem>
                                                <asp:ListItem>Opening Stock</asp:ListItem>
                                                <asp:ListItem>Adjustment (+)</asp:ListItem>
                                                <asp:ListItem>Adjustment (-)</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="33%">
                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Transaction Date:"></asp:Label>
                                            <asp:TextBox ID="TextBox21" runat="server" Width="125px"></asp:TextBox>
                                            <img id="Img2" onclick="w_displayDatePicker('<%=TextBox21.ClientID%>');" alt="Date Helper"
                                                hspace="5" src="../images/cal_icon.gif" width="22" border="0" name="appDateimg1"
                                                style="vertical-align: bottom; margin-bottom: 2px;" /><span class="smallerlabel"
                                                    id="Span2">(DD-MMM-YYYY)</span>
                                        </td>
                                        <td width="34%">
                                            <asp:Label ID="Label21" runat="server" Font-Bold="True" Text="Order No:"></asp:Label>
                                            &nbsp;<asp:TextBox ID="TextBox22" runat="server" Width="125px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td width="33%">
                                            <asp:Label ID="Label22" runat="server" Font-Bold="True" Text="Destination Store:"></asp:Label>
                                            &nbsp;<asp:DropDownList ID="DropDownList9" runat="server" Width="150px">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Adult Pharmacy</asp:ListItem>
                                                <asp:ListItem>Paediatric Phamacy</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Source Store:"></asp:Label>
                                            &nbsp;<asp:DropDownList ID="DropDownList8" runat="server">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Adult Phamacy</asp:ListItem>
                                                <asp:ListItem>Paediatric Pharmacy</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <hr />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <div class="GridView whitebg" style="cursor: pointer;">
                                    <div class="grid">
                                        <div class="rounded">
                                            <div class="top-outer">
                                                <div class="top-inner">
                                                    <div class="top">
                                                        <h2 class="center">
                                                            Items</h2>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="mid-outer">
                                                <div class="mid-inner">
                                                    <div class="mid" style="height: 300px; overflow: auto">
                                                        <div id="div-gridview" class="GridView whitebg">
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                                Width="100%" BorderWidth="0px" CellPadding="0" CssClass="table table-bordered table-hover">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Drug Name" />
                                                                    <asp:BoundField HeaderText="Unit" />
                                                                    <asp:BoundField HeaderText="Batch No" />
                                                                    <asp:BoundField HeaderText="Expiry Date" />
                                                                    <asp:BoundField HeaderText="Available Quantity" />
                                                                    <asp:BoundField HeaderText="Quantity" />
                                                                    <asp:BoundField HeaderText="Comments" />
                                                                </Columns>
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
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Button ID="Button3" runat="server" Font-Bold="True" Font-Size="Medium" Text="Submit" />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
