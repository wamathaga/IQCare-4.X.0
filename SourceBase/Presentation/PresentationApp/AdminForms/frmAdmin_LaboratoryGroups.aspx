<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmAdmin_LaboratoryGroups.aspx.cs" Inherits="PresentationApp.AdminForms.frmAdmin_LaboratoryGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Laboratory Test Groups</h3>
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
                                                <div id="grd_custom" class="GridView whitebg">
                                                    <asp:GridView ID="grdLab" runat="server" OnRowDataBound="grdLab_RowDataBound" AutoGenerateColumns="False"
                                                        Width="100%" AllowSorting="True" BorderWidth="0" GridLines="None" CssClass="table table-bordered table-hover"
                                                        CellPadding="0" CellSpacing="0" Height="99px" OnSelectedIndexChanging="grdLab_SelectedIndexChanging"
                                                        OnSorting="grdLab_Sorting">                                                       
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
              <asp:Button ID="btnAddLabGroup" Text="Add" runat="server" 
                                CssClass="btn btn-primary" Height="30px" Width="8%" 
                                Style="text-align: left;" onclick="btnAddLabGroup_Click" />
                            <label class="glyphicon glyphicon-open" style="margin-left: -3%; margin-right: 2%; vertical-align: sub;
                                color: #fff;">
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
</asp:Content>
