<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="ClinicalForms_frmClinicalICD10Selector" MaintainScrollPositionOnPostback="true" Codebehind="frmClinicalICD10Selector.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="lblHeader" runat="server">ICD10 Disease Selector</title>
    <link rel="Stylesheet" type="text/css" href="../Style/StyleSheetBrowser.css" />
    <script type="text/css" src="../Incl/jquery-1.7.1.js"></script>
    <style type="text/css">
        div.border
        {
            border-right: #666699 1px solid;
            border-top: #666699 1px solid;
            border-left: #666699 1px solid;
            border-bottom: #666699 1px solid;
            overflow: auto;
        }
        .formbg
        {
            background-color: #e1e1e1;
        }
        .pad5
        {
            padding-right: 5px;
            padding-left: 5px;
            padding-bottom: 5px;
            padding-top: 5px;
        }
        .whitebg
        {
            background-color: #ffffff;
        }
        div.treeview
        {
            border-right: #666699 1px solid;
            border-top: #666699 1px solid;
            display: inline;
            margin: 15px 6px 25px 6px;
            overflow: auto;
            border-left: #666699 1px solid;
            width: 100%;
            border-bottom: #666699 1px solid;
            height: 300px;
            text-align: left;
        }
        td.form
        {
            border-right: #666699 1px solid;
            border-top: #666699 1px solid;
            border-left: #666699 1px solid;
            border-bottom: #666699 1px solid;
            padding-right: 5px;
            padding-left: 5px;
            padding-bottom: 5px;
            padding-top: 5px;
            background-color: #ffffff;
        }
        .highlight
        {
            background-color: yellow;
        }
        .style1
        {
            width: 20%;
            height: 193px;
        }
        .style2
        {
            width: 80%;
            height: 193px;
        }
    </style>
</head>
<body>
    <form id="ICD10" method="post" runat="server">
    <script language="javascript" type="text/javascript">
        function PostPage() {
            window.close();
        }
        function closeMe() {
            var win = window.open("", "_self");
            win.close();
        }
        var sPath = window.location.pathname;
        var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
        var browserName = navigator.appName;
        if (browserName != "Microsoft Internet Explorer") {
            if (sPage == "frmConfig_Customfields.aspx") {
                document.write('<link rel="stylesheet" type="text/css" href=="../style/StyleSheetBrowser.css" />');
                document.write("<style>#container {z-index: 1; margin: 0px auto; width: 950px; height:900px ; position: relative; background-color: #ffffff; text-align: left</style>")
            }
            else {
                document.write('<link rel="stylesheet" type="text/css" href="../style/StyleSheetBrowser.css" />');
            }

        }
        else {
            document.write('<link rel="stylesheet" type="text/css" href="../style/styles.css" />');
        }

        function FnSearch() {

            var tree = document.getElementById('TVICD10');
            var links = tree.getElementsByTagName('a');
            var keysrch = document.getElementById('textsearch').value.toLowerCase();
            if (keysrch != "") {
                var keysrchlen = keysrch.length 
                for (var i = 0, j = links.length; i < j; i++) {
                    var twoletter = links[i].innerHTML.substr(0).toLowerCase();
                    var twoletter1 = links[i].innerHTML.substr(0, keysrchlen).toLowerCase()
                    if (keysrch == twoletter1) 
                    {
                        links[i].innerHTML = (links[i].innerHTML.fontcolor("red"))
                    }
                    var n = twoletter.search(keysrch);
                    if (n > 0) 
                    {
                        links[i].innerHTML = (links[i].innerHTML.fontcolor("red"))
                    }
                }
            }
        }

//        function ScrollToSelectedNode() {
//            
//            var selectedNodeID = $('#<%=TVICD10.ClientID %>_SelectedNode').val();
//            alert(selectedNodeID);
//            if (selectedNodeID != '') {
//                //* calculate selected top an left position (http://docs.jquery.com/CSS/position)
//                //* in order to get correct relative position remember to set div position to absolute
//                var scrollTop = $('#' + selectedNodeID).position().top;
//                var scrollLeft = $('#' + selectedNodeID).position().left;

//                //* here 'divTreeViewScrollTo' is the id of the div where we put our tree view
//                $('#divTreeViewScrollTo').scrollTop(scrollTop);
//                $('#divTreeViewScrollTo').scrollLeft(scrollLeft);
//            }
//        }
    </script>
   <asp:ScriptManager ID="SMscd10list" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="scd10list" runat="server">
        <ContentTemplate>
            <div  align="center">
                <table cellpadding="6" cellspacing="0" border="0">
                    <tbody>
                        <tr>
                            <td class="whitebg form" align="left" nowrap="nowrap">
                                <div style="overflow: scroll; overflow-x; width: 400px; height: 400px;">
                                    <asp:TreeView ID="TVICD10" Font-Size="Small"  Font-Bold="true" Font-Names="arial, helvetica, verdana, sans-serif"
                                        runat="server" NodeWrap="true" PopulateNodesFromClient="true" EnableClientScript="true"
                                        OnSelectedNodeChanged="TVICD10_SelectedNodeChanged">
                                    </asp:TreeView>
                                    <asp:TextBox ID="txtvalue" runat="server" Visible="false"></asp:TextBox>
                                    <asp:HiddenField ID="hdnvalue" runat="server" />
                                </div>
                            </td>
                            <td>
                                <asp:Button ID="btnAdd" runat="server" Width="80px" Text="Add >>" OnClick="btnAdd_Click" />
                                <br />
                                <asp:Button ID="btnRemove" runat="server" Width="80px" Text="<< Remove" OnClick="btnRemove_Click" />
                            </td>
                            <td class="whitebg form" valign="top">
                                <div style="overflow: scroll; overflow-x: hidden; width: 400px; height: 400px;">
                                    <asp:ListBox ID="lstSelectedICD10" runat="server" Width="400px" Height="400px"></asp:ListBox>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div align="left" style="display: none;">
                    <asp:Label ID="Label1" runat="server" Text="Search Criteria"></asp:Label>
                    <asp:TextBox ID="txtSearch" AutoPostBack="true" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <div align="center" style="padding-left: 10px;">
                <table width="100%">
                    <tr>
                   
                        <td align="center" style="width:20%">
                            <strong>Search</strong>
                            <input type="text" id="textsearch" onblur="FnSearch();" />
                             <strong>Use Tab to search</strong>
                        </td>
                        <td align="center"  style="width:60%">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" OnClientClick="PostPage()" />
                        </td>
                        <td align="center"  style="width:20%">
                        
                            <asp:LinkButton ID="lnkfullpath" Text="Full ICD10 Tree" runat="server" 
                                onclick="lnkfullpath_Click"></asp:LinkButton>
                        
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnRemove" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
