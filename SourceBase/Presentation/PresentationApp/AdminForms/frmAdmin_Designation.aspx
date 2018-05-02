<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="AdminDesignation" Title="Untitled Page" CodeBehind="frmAdmin_Designation.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">

      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Designation</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Employee Designation:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
             <asp:TextBox ID="txtDesignationName" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Form Signature:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap">
             <asp:RadioButton ID="rdouserName" GroupName="usr" Text="Display username only" runat="server" />
                            <asp:RadioButton ID="rdouserlist" GroupName="usr" Text="Display username list" runat="server" />
             </div> 
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Priority:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <asp:TextBox ID="txtSeq" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
             <div class="row">
             <div class="col-md-4"> <label class="control-label">Status:</label></div>
             <div class="col-md-8"><asp:DropDownList ID="ddStatus" runat="server" Width="99%" CssClass="form-control">
                                <asp:ListItem Text="Active" Value="0"></asp:ListItem>
                                <asp:ListItem Text="InActive" Value="1"></asp:ListItem>
                            </asp:DropDownList></div>
             </div>
            
             </div>
             
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                Height="30px" Width="8%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-remove" style="margin-left: -3%; margin-right: 2%;
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
