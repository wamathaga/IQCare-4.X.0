<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="AdminForms_frmAdmin_ControlListSelector" Codebehind="frmAdmin_ControlListSelector.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title id="lblHeader" runat="server">Custom List</title>
   <%-- <link rel="stylesheet" type="text/css" href="../style/styles.css" />   --%>
    <link href="../Bootstrap/dist/css/AdminLTE.css" rel="stylesheet" type="text/css" /> 
</head>
<script language="javascript" type="text/javascript">
    /*
    Author : Amitava Sinha
    Creation Date :30-Jan-2007
    Purpose: Validate List Box  
    */
    function Validate(objListBox) {
        var i = objListBox.length;
        if (i == 0) {
            NotifyMessage("Please enter atleast one Item !")
            return;
        }
    }
    function CheckDuplicate(sel) {
        var selobj = document.getElementById(sel);
        var fld = document.getElementById('<%=txtList.ClientID%>');
        var len = selobj.length;
        for (var i = 0; i < len; i++) {
            if (selobj.options[i].text == fld.value) {
                NotifyMessage("Duplicate Items !")
            }
        }

    }


</script>
<body>
    <form id="ListSelection" class="border" runat="server" style="height: 400px; width: 700px;">
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Select List</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row" align="left">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="Label1" runat="server" Text="Field Label" Width="84px"></asp:Label>
             <asp:Label ID="lblField" runat="server" Text="" Width="50%"></asp:Label>
             </label>
             </div>
                         
    </div>
<div class="row" align="left">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Label ID="Label2" runat="server" Text="Enter Value" Width="77px"></asp:Label>
        <asp:TextBox ID="txtList" runat="server" Width="50%" AutoPostBack="false" CssClass="form-control"></asp:TextBox>&nbsp;
        <asp:Button ID="btnAdd" runat="server"  Height="30px" Width="20%" Text="Add" Style="color:White;" OnClick="btnAdd_Click" CssClass="btn btn-primary" />
        <label class="glyphicon glyphicon-plus" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff; margin-top: 1%;">
                                </label>
 </div>
 </div>
 <div class="row" align="left">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="margin-left:80px;">
 <asp:ListBox ID="lstControlList" runat="server" Height="180px" Width="210px"></asp:ListBox>
 </div>
 </div>
 <div class="row" align="left">
 <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="margin-left:80px;">
 <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Height="30px" Width="20%" CssClass="btn btn-primary" Style="color:White;"/>
<asp:Button ID="btnBack" runat="server" Text="Close" OnClick="btnBack_Click" Height="30px" Width="20%" CssClass="btn btn-primary" Style="color:White;"/>
 </div>
 <br />
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>
    <script language="javascript" type="text/javascript">    
    </script>
    </form>
</body>
</html>