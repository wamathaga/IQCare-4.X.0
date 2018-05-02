<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="AdminForms_frmAdmin_PMTCT_CustomItems" Title="Untitled Page" CodeBehind="frmAdmin_PMTCT_CustomItems.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Customize Lists</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div class="GridView whitebg" style="cursor: pointer; height: 280px; overflow: auto">
                                <asp:TreeView ID="tvcustomlist" runat="server">
                                </asp:TreeView>
                                <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/XMLFiles/customizelist.xml">
                                </asp:XmlDataSource>
                            </div>
             </div>
                             
    </div>
    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click"
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
     </div>


    
</asp:Content>
