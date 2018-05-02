<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="AdminForms_frmAdmin_IQCCustomPage" Codebehind="frmAdmin_IQCCustomPage.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%-- <form id="form1" runat="server">--%>
    
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title"><asp:Label ID="lblHeader" runat="server"></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">             
             </div>             
 </div>
 <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label"><asp:Label ID="lblcode" runat="server" Text="Code:"></asp:Label></label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <asp:TextBox ID="txtcode" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
            
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label  class="control-label"><asp:Label ID="lblName" runat="server" Text="Name :"></asp:Label></label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
            <asp:TextBox ID="txtName" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
             
           <div id="tdStatus" runat="server">
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label"><asp:Label ID="lblStatus" runat="server" Text="Status :"></asp:Label></label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
            <asp:DropDownList ID="ddStatus" runat="server" Width="99%" CssClass="form-control">
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Inactive</asp:ListItem>
                                </asp:DropDownList>
             </div> 
             </div>
            
             <div id="tdPriority" runat="server">
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label"><asp:Label ID="lblPriority" runat="server" Text="Priority :"></asp:Label></label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
              <asp:TextBox ID="txtSeqNo" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
            
             </div> 
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
          <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  CssClass="btn btn-primary"
                                    Height="30px" Width="9%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnCancel_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-erase" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnExit" runat="server" Text="Close" OnClick="btnExit_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="8%" Style="text-align: left;" />
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
