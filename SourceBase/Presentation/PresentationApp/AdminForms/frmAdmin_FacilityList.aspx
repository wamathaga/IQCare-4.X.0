<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="FacilityMaster_List" Title="Untitled Page" CodeBehind="frmAdmin_FacilityList.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Facility/Satellite</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div id="tblSystem" runat="server">
         <div class="row">
         <br />
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">System Type:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="cmbSystem" runat="server" Width="40%" CssClass="form-control"></asp:DropDownList>
             </div>
                               
    </div>
     <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnSystemSave" runat="server" Text="Set System" OnClick="btnSystemSave_Click" Width="10%" Height="30px" CssClass="btn btn-primary" Style="text-align: left;" />
 <span class="glyphicon glyphicon-floppy-disk" style="vertical-align: sub; margin-left: -3%;
                                        margin-right: 2%; color: #fff"></span>
 </div>
 </div>
 </div>

 <div class="row" align="center">
 <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div class="GridView whitebg" style="cursor: pointer;">
                                <div class="grid">
                                    <div class="rounded">
                                        <div class="top-outer">
                                            <div class="top-inner">
                                                <div class="top">
                                                    <h2>
                                                        Facility List</h2>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 300px; overflow: auto;">
                                                    <div id="div-gridview" class="GridView whitebg">
                                                        <asp:GridView ID="grdMasterFacility" runat="server" OnRowDataBound="grdMasterFacility_RowDataBound"
                                                            AutoGenerateColumns="False" Width="100%" PageSize="1" AllowSorting="True" CssClass="table table-bordered table-hover"
                                                            OnSelectedIndexChanging="grdMasterFacility_SelectedIndexChanging" CellPadding="2"
                                                            OnSorting="grdMasterFacility_Sorting">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
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
    </div>
    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary"
                                Height="30px" Width="8%" Style="text-align: left;" /><span class="glyphicon glyphicon-plus"
                                    style="vertical-align: middle; color: #fff; margin-top: .25%; margin-left:-1%;"></span>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click"
                                CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left; margin-left:2%;" /><span
                                    class="glyphicon glyphicon-remove" style="margin-left:-2%; vertical-align: middle;
                                    color: #fff; margin-top:.25%;"></span>
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
