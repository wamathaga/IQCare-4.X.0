<%@ Page Language="C#" AutoEventWireup="true" Inherits="frmDBBackupSetup" CodeBehind="frmDBBackupSetup.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Database backup setup</title>
    <link href="Style/styles.css" rel="stylesheet" type="text/css" />
<link href="Style/bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="Style/bootstrap-3.3.6-dist/css/bootstrap-theme.css" rel="stylesheet"
    type="text/css" />
<script src="Style/bootstrap-3.3.6-dist/js/bootstrap.js" type="text/javascript"></script>
<script src="Style/bootstrap-3.3.6-dist/html5Shiv/html5shiv.js" type="text/javascript"></script>
</head>
<body class="container-fluid">
    <form id="form1" runat="server" style="height: 75px; width: 430px;">
    <h5 class="h5">
        Backup/Restore Setup</h5>
    <div class="border formbg">
        <table class="table-condensed" width="100%">
            <tbody>
                <tr>
                    <td class="border pad5 whitebg" valign="top" width="60%" nowrap="nowrap">
                        <label class="right" for="lblBackupTime">
                            Auto-Backup Time:</label>
                        <asp:DropDownList ID="ddBackupTime" runat="server" Width="73px">
                        </asp:DropDownList>
                    </td>
                    <td class="border pad5 whitebg" valign="top" width="40%" nowrap="nowrap">
                        <label class="right" for="lblBackupDrive">
                            Backup Drive:</label>
                        <asp:DropDownList ID="ddBackupDrive" runat="server" Width="60px">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>C:</asp:ListItem>
                            <asp:ListItem>D:</asp:ListItem>
                            <asp:ListItem>E:</asp:ListItem>
                            <asp:ListItem>F:</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="whitebg">
                    <td colspan="2">
                        <asp:Button ID="cmdSave" runat="server" Text="Save" OnClick="cmdSave_Click" CssClass="btn btn-primary"
                            Height="30px" Width="17%" Style="text-align: left;" />
                        <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -6%; margin-right: 2%;
                            vertical-align: sub; color: #fff; margin-top: 1%; margin-right:5%;">
                        </label>
                        <input type="button" onclick="window.close();" value="Close" class="btn btn-primary"
                            style="height: 30px; width: 17%; text-align: left;" />
                        <label class="glyphicon glyphicon-remove-circle" style="margin-left: -6%; margin-right: 2%;
                            vertical-align: sub; color: #fff; margin-top: 1%;">
                        </label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
