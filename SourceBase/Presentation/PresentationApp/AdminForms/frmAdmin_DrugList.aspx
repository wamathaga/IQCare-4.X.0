<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="DrugMaster_List" Title="Untitled Page" Codebehind="frmAdmin_DrugList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<link type="text/css" href="../Style/_assets/css/grid.css" rel="stylesheet" />
<link type="text/css" href="../Style/_assets/css/round.css" rel="stylesheet" />
<form id ="masterdruglist" method="post" runat="server"> --%>
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Drugs</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row" align="center">
         <br />             
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">   
           <div class="grid" style="padding-right:5px;">
                                    <div class="rounded">
                                        <div class="top-outer">
                                            <div class="top-inner">
                                                <div class="top">
                                                    <h2>
                                                    </h2>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid">
                                                    <div id="grd_custom" class="GridView whitebg" style="cursor: pointer; width: 100%;
                                                        height: 300px; overflow: scroll;">
                                                        <asp:GridView ID="grdMasterDrugs" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0"
                                                            runat="server" OnRowDataBound="grdMasterDrugs_RowDataBound" AutoGenerateColumns="False"
                                                            PageSize="1" AllowSorting="True" BorderWidth="0" GridLines="None" Height="99px"
                                                            OnSelectedIndexChanging="grdMasterDrugs_SelectedIndexChanging" OnSorting="grdMasterDrugs_Sorting">
                                                           
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
 <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;"/>
 <label class="glyphicon glyphicon-plus" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff;"></label>
<asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;"/>  
<label class="glyphicon glyphicon-remove" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff;"></label>     
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
