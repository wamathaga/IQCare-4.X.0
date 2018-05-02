<%@ Page Language="C#" AutoEventWireup="true" Inherits="frmConTest" Codebehind="frmConTest.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtCheck" runat="server" CausesValidation = "true" OnTextChanged="TextBox1_TextChanged" Width="239px"></asp:TextBox><br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Encryption" />
        <asp:DataGrid ID ="testgrid" runat ="server">
            <Columns>
                <asp:BoundColumn HeaderText="Col1"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Col2"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Custom reports" />
    </form>
</body>
</html>
