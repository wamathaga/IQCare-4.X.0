<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPharmacy_PrintLabels.aspx.cs" Inherits="PresentationApp.PharmacyDispense.frmPharmacy_PrintLabels" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Style/_assets/css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../Style/_assets/css/round.css" rel="stylesheet" type="text/css" />

    <title></title>
    <style type="text/css">
        .style3
        {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                <asp:Panel ID="pnlContents" runat="server" Height="283px" Width="345px" 
            BorderColor="#999999" BorderStyle="Solid">
            <table id="Table1">
            <tr>
            <td class="style3">
                </td>
                <td class="style3">
                    </td>
                <td class="style3">
                    DrugQuantity</td>
            </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="DrugName" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="TextBox1" runat="server" Width="328px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Name:</td>
                    <td>
                        &nbsp;</td>
                    <td rowspan="4">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style3">
                        StoreName</td>
                    <td class="style3">
                        </td>
                </tr>
                <tr>
                    <td>
                        FacilityName</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        Keep medicine in a cool dry place out of the reach of children</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        No of copies:</td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="95px">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Button id="btnPrint" runat="server" Text="Print" OnClientClick = "return PrintPanel();"/>
        <br /><br />
        <asp:Repeater ID="rptLabels" runat="server">
        <ItemTemplate>
        <asp:Panel ID="Panel1" runat="server" Height="283px" Width="345px" 
            BorderColor="#999999" BorderStyle="Solid">
            <table id="Table1">
            <tr>
            <td class="style3">
                </td>
                <td class="style3">
                    </td>
                <td class="style3">
                    DrugQuantity</td>
            </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="DrugName" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="TextBox1" runat="server" Width="328px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Name:</td>
                    <td>
                        &nbsp;</td>
                    <td rowspan="4">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style3">
                        StoreName</td>
                    <td class="style3">
                        </td>
                </tr>
                <tr>
                    <td>
                        FacilityName</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        Keep medicine in a cool dry place out of the reach of children</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        No of copies:</td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="95px">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
        </ItemTemplate>
        </asp:Repeater>

    </div>
    </form>
</body>
</html>

    <script type = "text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
                printWindow.close();
            }, 500);
            return false;
        }
    </script>