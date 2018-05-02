<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IQCareIssue_Report.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="Style/Stylesheet.css" />
    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript">
        function fnvalidate() {
            if (document.getElementById("txtemail").value != "") {
                var x = document.getElementById("txtemail").value;
                var atpos = x.indexOf("@");
                var dotpos = x.lastIndexOf(".");
                if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                    alert("Not a valid e-mail address");
                    document.getElementById("txtemail").focus();
                    return false;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/iq_logo.gif" />
    </div>
    <div style="background-color: Gray; height: 2px">
        &nbsp;
    </div>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Report  a Defect" CssClass="normaltext"></asp:Label>
    </div>
    <div>
        <table class="style1">
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                    <asp:Label ID="lblname" runat="server" Text="Your Name:" CssClass="required bold"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:TextBox ID="txtname" runat="server" Width="267px" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px">
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                    <asp:Label ID="lblemail" runat="server" Text="Your Email Address:" CssClass="required bold"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:TextBox ID="txtemail" runat="server" Width="265px" MaxLength="150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px">
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                    <asp:Label ID="lblseverity" runat="server" CssClass="required bold" Text="Severity:"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:DropDownList ID="cmbseverity" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px">
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                    <asp:Label ID="lblComponent" runat="server" Text="Component:" CssClass="required bold"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:DropDownList ID="cmbcomponent" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px">
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Version and Patch Number:" CssClass="required bold"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:TextBox ID="txtversion" MaxLength="50" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px">
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                    <asp:Label ID="lblsummary" runat="server" CssClass="required bold" Text="Summary:"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:TextBox ID="txtsummary" runat="server" Width="489px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px">
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                    <asp:Label ID="lblreproduce" runat="server" CssClass="required bold" Text="Steps to Reproduce:"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:TextBox ID="txtreproduce" runat="server" AutoCompleteType="Disabled" Height="97px"
                        TextMode="MultiLine" Width="492px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px">
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                    <asp:Label ID="lblfileupload" runat="server" CssClass="normaltext" Text="File upload:"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="486px" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                </td>
                <td class="tfont">
                    Please protect patient privacy. If you are uploading a file, please make sure that
                    all patient identification details are removed.
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblmessage" CssClass="alert" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                </td>
                <td>
                    <table>
                        <tr>
                            <td style="width: 150px">
                            </td>
                            <td class="pad5 center">
                                <asp:Button ID="btnsave" runat="server" Text="Save" 
                                    OnClientClick="javascript:return fnvalidate();" onclick="btnsave_Click" />
                            </td>
                            <td class="pad5 center">
                                <asp:Button ID="btnreset" runat="server" Text="Reset" 
                                    onclick="btnreset_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div style="background-color: Gray; height: 2px">
        &nbsp;
    </div>
    <div>
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Image/FGI.bmp" />
    </div>
    </form>
</body>
</html>
