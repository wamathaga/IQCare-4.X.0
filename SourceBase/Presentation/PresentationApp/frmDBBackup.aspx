<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmDBBackup" CodeBehind="frmDBBackup.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">System Back-Up/Restore</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Backup Directory :</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtbakuppath" runat="server" Width="406px" Text="c:\IQCareDBBackup" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <input id="chkDeidentified" type="checkbox" runat="server" />
             <label class="control-label">Make Backup of the Database with Identifiers Removed</label>
             </div>
                             
    </div>
    <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnBackup" runat="server" Text="Backup" OnClick="btnBackup_Click"
                                CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-cloud-download" style="margin-left: -2.5%; margin-right: 2%;
                                vertical-align: sub; color: #fff; margin-top: 1%;">
                            </label>
 </div>
 </div>
 <div class="row text-center">
 <hr />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="row">
  <asp:Label ID="Label2" runat="server" Text="Restore File :"></asp:Label>
                            <asp:TextBox ID="txtRestore" runat="server" Width="406px"></asp:TextBox>
                            <asp:Button ID="btnBrowse" runat="server" Text="Browse" OnClick="btnBrowse_Click"
                                CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-tags" style="margin-left: -2.5%; margin-right: 2%;
                                vertical-align: sub; color: #fff; margin-top: 1%;">
                            </label>
 </div>
 </div>
 </div>
 <div class="text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnRestore" runat="server" Text="Restore" OnClick="btnRestore_Click"
                                CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-download-alt" style="margin-left: -2.5%; margin-right: 2%;
                                vertical-align: sub; color: #fff; margin-top: 1%;">
                            </label>

                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-primary"
                                Height="30px" Width="9%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -2.5%; margin-right: 2%;
                                vertical-align: sub; color: #fff; margin-top: 1%;">
                            </label>

 </div>
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>

    
    <script language="javascript" type="text/javascript">
        function GetControl() {
            document.forms[0].submit();
        }
    </script>
</asp:Content>
