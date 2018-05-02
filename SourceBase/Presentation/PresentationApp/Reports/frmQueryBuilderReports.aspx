<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="Reports_frmQueryBuilderReports" Title="Untitled Page" CodeBehind="frmQueryBuilderReports.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">QueryBuilder Reports</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">ReportsCategory:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlCategory" Width="95%" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                                AutoPostBack="True" CssClass="form-control">
                                            </asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblReportName" runat="server" Text="Reports Name:" Visible="false"></asp:Label></label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:Panel ID="dyanamicRadiobutton" CssClass="left whitebg" align="left" runat="server">
                                                <asp:RadioButtonList RepeatLayout="Flow" Width="80%" ID="rdButtonList" runat="server" CssClass="checkbox-btn">
                                                </asp:RadioButtonList>
                                            </asp:Panel>
             </div> 
 </div>
 <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                            CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                        <label class="glyphicon glyphicon-download" style="margin-left: -2.5%; margin-right: 2%;
                            vertical-align: sub; color: #fff; margin-top: 1%;">
                        </label>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                            CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                        <label class="glyphicon glyphicon-remove-circle" style="margin-left: -2.5%; margin-right: 2%;
                            vertical-align: sub; color: #fff; margin-top: 1%;">
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
