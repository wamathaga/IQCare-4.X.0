<%@ Page Language="C#" AutoEventWireup="True" Inherits="AdminForms_frmAdmin_LaboratorySelectList"
    CodeBehind="frmAdmin_LaboratorySelectList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="lblHeader" runat="server">Lab select list</title>
    <link rel="stylesheet" type="text/css" href="../style/styles.css" />
    <link href="../Style/bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../Style/bootstrap-3.3.6-dist/css/bootstrap-theme.css" rel="stylesheet"
        type="text/css" />
    <script src="../Style/bootstrap-3.3.6-dist/html5Shiv/html5shiv.js" type="text/javascript"></script>
    <link href="../Style/CustomStyle.css" rel="stylesheet" type="text/css" />
</head>
<body class="container-fluid">
    <script language="javascript" type="text/javascript">
        /*
        Author : Amitava Sinha
        Creation Date : 26-Mar-2007
        Purpose:atleast one item will selected 
        */

        function txtAdd(sel) {
            var txtselect = document.getElementById(sel);
            var intCount = txtselect.value.length;
            if (intCount > 0) {
                return true;
            }

            NotifyMessage("Fill the Enter Value!");
            return false;

        }
        function listBox_hasItem(sel) {
            var listBox = document.getElementById(sel);
            var intCount = listBox.options.length;

            for (i = 0; i < intCount; i++) {
                return true;

            }
            NotifyMessage("Add atleast one item !");
            return false;

        }
        function listBox_selected(sel) {
            var listBox = document.getElementById(sel);
            var intCount = listBox.options.length;

            for (i = 0; i < intCount; i++) {
                if (listBox.options(i).selected) {
                    return true;
                }

            }
            NotifyMessage("Select atleast one item !");
            return false;

        }

        function closeMe() {
            var win = window.open("", "_self");
            win.close();
        }
    </script>
    <form id="Selection" class="border" runat="server" style="height: 280px; width: 350px;">

    <table class="table-condensed">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Enter Value"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtselect" runat="server" CssClass="form-control"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary"
                    Height="30px" Width="50px" Style="text-align: left" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table cellpadding="18" width="100%" border="0" class="table-condensed">
                    <tbody>
                        <tr>
                            <td class="border formbg">
                                <asp:ListBox ID="lstselectList" runat="server" Height="180px" Width="100%" CssClass="ListBoxCssClass"></asp:ListBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                    CssClass="btn btn-primary" Height="30px" Width="70px" Style="text-align: left" />
                <asp:Button ID="btnRemove" runat="server" Text="Delete" OnClick="btnRemove_Click"
                    CssClass="btn btn-primary" Height="30px" Width="70px" Style="text-align: left" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
