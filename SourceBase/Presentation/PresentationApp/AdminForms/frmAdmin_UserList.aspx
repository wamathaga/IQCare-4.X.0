<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="frmAdmin_UserList" Title="Untitled Page" Codebehind="frmAdmin_UserList.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<link type="text/css" href="../Style/_assets/css/grid.css" rel="stylesheet" />
    <link type="text/css" href="../Style/_assets/css/round.css" rel="stylesheet" />--%>
    <%--<form id ="UserList" method="post" runat="server" title="User List"> --%>
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
        <h3 class="box-title">User Administration</h3>
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
                                        <div id="div1" class="top-inner">
                                            <div class="top" id="divtop">
                                                <h2>
                                                    User List</h2>
                                            </div>
                                         </div>
                                      </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 300px; overflow: auto">
                                                <div id="div-gridview" class="GridView whitebg">
                                                    <asp:GridView ID="GrdUserList" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        PageSize="1" AllowSorting="true" OnRowDataBound="GrdUserList_RowDataBound" OnSelectedIndexChanging="GrdUserList_SelectedIndexChanging"
                                                        HorizontalAlign="Left" CellPadding="0" CellSpacing="0" BorderWidth="0" GridLines="None"
                                                        OnSorting="GrdUserList_Sorting" CssClass="table table-bordered table-hover" BackColor="White">
                                                        <%--<HeaderStyle HorizontalAlign="Left" />
                                                        <RowStyle CssClass="row" />--%>
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
 <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align:left;margin-right:50px;"/>
 <span class="glyphicon glyphicon-plus" style="margin-left: -6%; vertical-align: middle; color: #fff;"></span>
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align:left;margin-left:10px;"/>
                            <span class="glyphicon glyphicon-remove" style="margin-left: -2%; vertical-align: middle; color: #fff;"></span>
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
