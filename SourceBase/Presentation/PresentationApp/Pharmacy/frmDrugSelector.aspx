<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="frmDrugSelector" Codebehind="frmDrugSelector.aspx.cs" %>

<link rel="stylesheet" type="text/css" href="../style/styles.css" />
<head id="Head1" runat="server">
<link href="../Style/bootstrap-3.3.6-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/bootstrap-3.3.6-dist/css/bootstrap-theme.min.css" rel="stylesheet"
        type="text/css" />
    <%--<script src="Style/bootstrap-3.3.6-dist/respond/respond.min.js" type="text/javascript"></script>
    <script src="Style/bootstrap-3.3.6-dist/html5Shiv/html5shiv.min.js" type="text/javascript"></script>--%>
    <title id="lblHeader" runat="server">Untitled Page</title>
</head>
<body>
    <script language="javascript" type="text/javascript">
        /*
        Author : Amitava Sinha
        Creation Date : 26-Mar-2007
        Purpose:atleast one item will selected 
        */
        function listBox_selected(sel) {
            var listBox = document.getElementById(sel).value;
            if (listBox == "") {
                alert("Select atleast one drug !");
                return false;
            }
            var intCount = listBox.options.length;
            for (i = 0; i < intCount; i++) {
                if (listBox.options(i).selected) {
                    return true;
                }
            }
            //            alert("Select atleast one item !");
            //            return false;

        }
        function fnset(id, name) {

            window.opener.document.getElementById(id).value = name;
        }
        function closeMe() {
            var win = window.open("", "_self");
            win.close();
        }
        function fnClose(sel) {

            var listBox = document.getElementById(sel);
            var intCount = listBox.options.length;
            for (i = 0; i < intCount; i++) {
                listBox.remove(i);
            }
            window.close();
        }


    </script>
    <form id="DrugSelection" class="border" runat="server" style="height: 300px; width: 622px;"> 
    <div style="width: 600px; height: 300px;margin-left:10px;">
        <table cellpadding="18" width="100%" height="70%" border="0">
            <tbody>
                <tr>
                    <td class="border formbg">
                        <asp:ListBox ID="lstDrugList" runat="server" Height="180px" Width="210px"></asp:ListBox>
                    </td>
                    <td>
                        <div>
                            <asp:Button ID="btnAdd" runat="server" Width="80px" Height="30" Text="Add" CssClass="btn btn-primary" Style="text-align: left;" OnClick="btnAdd_Click" />
                             <label class=" glyphicon glyphicon-forward" style="margin-left: -3%; vertical-align: sub;
                                color: #fff;">
                            </label>
                        </div>
                        <br />
                        <div>
                            <asp:Button ID="btnRemove" runat="server" Width="80px" Height="30px" Text="Remove" 
                            OnClick="btnRemove_Click" CssClass="btn btn-primary" Style="text-align: left;" />
                            <label class=" glyphicon glyphicon-backward" style="margin-left: -3%; vertical-align: sub;
                                color: #fff;">
                            </label>
                        </div>
                    </td>
                    <td class="border formbg">
                        <asp:ListBox ID="lstSelectedDrug" runat="server" Height="180px" Width="210px"></asp:ListBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div align="left" style="width: 600px">
            <asp:Label ID="Label1" runat="server" Text="Search Criteria"></asp:Label>
            <asp:TextBox ID="txtSearch" AutoPostBack="true" runat="server" OnTextChanged="txtSearch_TextChanged" CssClass="form-control"></asp:TextBox>
        </div>
        <br />
        <div class="border1" align="center" style="width: 620px">
            <asp:Button ID="btnSubmit" runat="server" Width="80px" Height="30"  Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
             <label class="glyphicon glyphicon-floppy-disk" style="vertical-align: middle; margin-left: -3%;
                                    color: #fff; margin-top: .75%; margin-right: 2%;">
                                </label>
            <asp:Button ID="btnBack" runat="server" Width="80px" Height="30"  Text="Back" OnClick="btnBack_Click" CssClass="btn btn-primary"/>
            <label class="glyphicon glyphicon-backward" style="margin-left: -3%; vertical-align: sub;
                                    color: #fff; margin-right: 2%;">
                                </label>
            <br />
            <br />
            <br />
        </div>
    </div>
    <script language="javascript" type="text/javascript">
    
    </script>
    </form>
</body>
