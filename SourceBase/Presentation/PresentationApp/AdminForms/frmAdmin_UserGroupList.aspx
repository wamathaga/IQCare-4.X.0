<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="frmAdmin_UserGroupList" Title="Untitled Page" CodeBehind="frmAdmin_UserGroupList.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<style type="text/css">
    .table-hover tbody tr:hover td, .table-hover tbody tr:hover th {
   cursor:pointer;
}
    </style>
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">List of User Groups</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row" align="center">
         <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div class="grid" style="padding-right:5px;">
                                    <div class="rounded">
                                        <div class="top-outer">
                                            <div class="top-inner">
                                                <div class="top">
                                                    <h2 align="center">
                                                        User Group List</h2>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 300px; overflow: auto">
                                                    <div id="div-gridview" class="GridView whitebg">
                                                        <asp:GridView ID="grdUsergroup" runat="server" AutoGenerateColumns="False" Width="99%"
                                                            AllowSorting="true" PageIndex="1" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0"
                                                            BorderWidth="0" GridLines="None" OnRowDataBound="grdUsergroup_RowDataBound" OnSelectedIndexChanging="grdUsergroup_SelectedIndexChanging"
                                                            BackColor="White" OnSorting="grdUsergroup_Sorting">
                                                           
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="bottom-outer">
                                            <div class="bottom-inner">
                                                <div class="bottom">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
             </div>                                   
    </div>
    <div class="row" align="center">
    
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnAddgroup" Text="Add" runat="server" OnClick="btnAddgroup_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="7%" Style="text-align: left;
                                    margin-left: 10%;" /><span class="glyphicon glyphicon-plus" style="margin-left: -2%;
                                        vertical-align: middle; color: #fff; "></span>
                                <asp:Button ID="btnCancelgroup" Text="Close" runat="server" OnClick="btnCancelgroup_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;
                                    margin-left: 2%;" /><span class="glyphicon glyphicon-remove" style="margin-left: -2%;
                                        vertical-align: middle; color: #fff; margin-top:.25%;"></span>

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
