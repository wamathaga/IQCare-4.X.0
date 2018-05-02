<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="AdminForms_frmAdmin_EmployeeMaster" Title="Untitled Page" CodeBehind="frmAdmin_EmployeeMaster.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<style type="text/css">
.modalBackground 
{
  background-color:#F5F5F5;
  filter: alpha(opacity=80);
  opacity: 0.80; 
  z-index: 2000;
}
</style>
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title"><asp:Label ID="lblH2" runat="server"></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">First Name :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtFirstName" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Last Name :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtLastName" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Designation :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddDesignation" runat="server" Width="99%" CssClass="form-control"> </asp:DropDownList>
             </div>
             <asp:Label ID="lblactive" runat="server">
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Status :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddStatus" runat="server" Width="99%" CssClass="form-control">
                                        <asp:ListItem Value="0">Active</asp:ListItem>
                                        <asp:ListItem Value="1">Inactive</asp:ListItem>
                                    </asp:DropDownList>
             </div> 
             </asp:Label>
 </div>
   <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="9%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-erase" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btncancel" runat="server" Text="Close" OnClick="btnCancel_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
 </div>
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>


</asp:Content>
