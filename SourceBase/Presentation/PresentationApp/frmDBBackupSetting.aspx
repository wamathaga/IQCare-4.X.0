<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmDBBackupSetting" CodeBehind="frmDBBackupSetting.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Backup/Restore Setup</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Auto-Backup Time: :</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              
             <asp:DropDownList ID="ddBackupTime" runat="server" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">             
             <label class="control-label">Backup Drive:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddBackupDrive" runat="server" CssClass="form-control">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>C:</asp:ListItem>
                            <asp:ListItem>D:</asp:ListItem>
                            <asp:ListItem>E:</asp:ListItem>
                            <asp:ListItem>F:</asp:ListItem>
                        </asp:DropDownList>
             </div>
                             
    </div>
    <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="cmdSave" runat="server" Text="Save" OnClick="cmdSave_Click" CssClass="btn btn-primary"
                            Height="30px" Width="8%" Style="text-align: left;" />
                        <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 5%;
                            vertical-align: sub; color: #fff; margin-top: 1%;">
                        </label>
                        <input type="button" onclick="location.href = 'frmFacilityHome.aspx';" value="Close" class="btn btn-primary"
                            style="height: 30px; width: 8%; text-align: left;" />
                        <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 5%;
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
</asp:Content>
