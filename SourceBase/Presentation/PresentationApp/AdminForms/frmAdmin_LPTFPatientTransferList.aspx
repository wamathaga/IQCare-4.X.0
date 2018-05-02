<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="AdminForms_frmAdmin_LPFTPatientTransferList" Title="Untitled Page"
    CodeBehind="frmAdmin_LPTFPatientTransferList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<link type="text/css" href="../Style/_assets/css/grid.css" rel="stylesheet" />
<link type="text/css" href="../Style/_assets/css/round.css" rel="stylesheet" />
<form id ="EmpMasterList" method="post" runat="server"> --%>
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">LPTF Patient Transfer</h3>
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
                                            <div class="mid" style="height: 300px; overflow: auto">
                                                <div id="grd_custom" class="gridview whitebg" style="cursor: pointer; height: 280px;
                                                    overflow: auto">
                                                    <asp:GridView ID="grdMasterLPTFPatientTransfer" runat="server" AllowSorting="True"
                                                        AutoGenerateColumns="False" Width="100%" BackColor="White" PageIndex="1" BorderWidth="0"
                                                        GridLines="None" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0" OnRowDataBound="grdMasterLPTFPatientTransfer_RowDataBound"
                                                        OnSelectedIndexChanging="grdMasterLPTFPatientTransfer_SelectedIndexChanging"
                                                        OnSorting="grdMasterLPTFPatientTransfer_Sorting">                                                       
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
  <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary"
                                Height="30px" Width="8%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-plus" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
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
