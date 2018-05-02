<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="AdminForms_frmAdmin_OI_Symptoms" CodeBehind="frmAdmin_OI_Symptoms.aspx.cs" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id="addeditDisease" method="post" runat="server">--%>    
        <asp:UpdatePanel ID="UpdateOIAIDSIllness" runat="server">
            <ContentTemplate>               
               <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title"><asp:Label ID="lblHeader" runat="server" Text=""></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>        
<div class="row">
 <div class="col-md-6">
 <%--left--%>
 <div class="row">
 <div class="col-md-10 col-sm-12 col-xs-12 form-group">
          <div class="treeview">
                                        <asp:TreeView ID="TVICD10" runat="server" NodeWrap="True" PopulateNodesFromClient="true"
                                            EnableClientScript="true" OnSelectedNodeChanged="TVICD10_SelectedNodeChanged">
                                        </asp:TreeView>
                                    </div>
             </div>    
             <div class="col-md-2 col-sm-12 col-xs-12 form-group" style="display:none;">
             <div class="treeview">
                                        <asp:CheckBoxList ID="cblModuleName" RepeatLayout="Flow" runat="server">
                                        </asp:CheckBoxList>
                                    </div>
             </div>
 </div>
 </div>
 <div class="col-md-6">
 <%--right--%>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label"><asp:Label ID="lblName" runat="server" Text=""></asp:Label></label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtName" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>            
 </div>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label"><asp:Label ID="lblPriority" runat="server" Text="Priority :"></asp:Label></label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtSeqNo" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>            
 </div>
 <div class="row" id="tdStatus" runat="server">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label"><asp:Label ID="lblStatus" runat="server" Text="Status :"></asp:Label></label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddStatus" runat="server" Width="99%" CssClass="form-control">
                                        <asp:ListItem Value="0">Active</asp:ListItem>
                                        <asp:ListItem Value="1">Inactive</asp:ListItem>
                                    </asp:DropDownList>
             </div>            
 </div>
 <div class="row" id="td1" runat="server">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label"><asp:Label ID="lblType" runat="server" Text="Form List Type :"></asp:Label></label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddType" runat="server" Width="99%" CssClass="form-control">
                                        <asp:ListItem Value="1">Disease</asp:ListItem>
                                        <asp:ListItem Value="0">Symptom</asp:ListItem>
                                    </asp:DropDownList>
             </div>            
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
                                    <asp:Button ID="btnCancel" runat="server" Text="Reset" Visible="false" CssClass="btn btn-primary"
                                        Height="30px" Width="8%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-erase" style="display:none; margin-left: -3%; margin-right: 2%;
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
     </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnCancel"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExit"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="TVICD10"></asp:PostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    
</asp:Content>
