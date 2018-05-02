<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="Reports_frmReportCustom" Title="Untitled Page" CodeBehind="frmReportCustom.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<script language="javascript" type="text/javascript">

    function hide(divId) {
        if (document.layers) document.layers[divId].visibility = 'hide';
        else if (document.all) document.all[divId].style.display = 'none';
        else if (document.getElementById) document.getElementById(divId).style.display = 'none';
    }

    //shows div
    function show(divId) {
        if (document.layers) document.layers[divId].visibility = 'show';
        else if (document.all) document.all[divId].style.display = 'inline';
        else if (document.getElementById) document.getElementById(divId).style.display = 'inline';
    }

    function CheckReport(ddlReportName) {
        if (document.getElementById(ddlReportName).value == "") {
            alert('No Report Selected.');
            return false;
        }
        return true;
    }
    function fnRedirect(id) {
        //alert(id);
        window.location.href = "frmReportCustomNew.aspx?ReportId=" + id + "&ReportImpMode=RIEdit";
    }
    function redirectNew() {
        window.location.href = "frmReportCustomNew.aspx";
    }
        
        </script>
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Custom Reports</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
          <asp:UpdatePanel ID="up_report" runat="server">
                <ContentTemplate>
                <div class="row">
                <br />
 <div class="col-md-9">
 <%--left--%>
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Saved Reports:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddCategory" runat="server" Width="98%" OnSelectedIndexChanged="ddCategory_SelectedIndexChanged"
                                                        AutoPostBack="true" CssClass="form-control">
                                                    </asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddTitle" runat="server" Width="98%" CssClass="form-control"> </asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">Import Reports:</label>
             </div>
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <asp:FileUpload ID="inptReport" runat="server" Height="30px" Width="98%" CssClass="btn btn-default" />
             
              </div> 
 </div>

 </div>
 <div class="col-md-3">
 <%--right--%>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnNew" runat="server" Text="Create New Report" OnClick="btnNew_Click"
                                            CssClass="btn btn-primary" Height="30px" Width="54%" Style="text-align: left;" />
                                        <label class="glyphicon glyphicon-plus" style="vertical-align: middle; margin-left: -8%;
                                            color: #fff; margin-top: 2.5%;">
                                        </label>            
 </div>            
 </div>
  </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnRun" OnClick="btnRun_Click" runat="server" Text="Run" CssClass="btn btn-primary"
                                                        Height="30px" Width="8%" Style="text-align: left;" />
                                                    <label class="glyphicon glyphicon-check" style="vertical-align: middle; margin-left: -2.5%; margin-right: 2%;
                                                        color: #fff;">
                                                    </label>  
 <asp:Button ID="btnEdit" OnClick="btnEdit_Click" runat="server" Text="Edit" CssClass="btn btn-primary"
                                                        Height="30px" Width="8%" Style="text-align: left;" />
                                                    <label class="glyphicon glyphicon-log-out" style="vertical-align: middle; margin-left: -2.5%; margin-right: 2%;
                                                        color: #fff;">
                                                    </label>  
<asp:Button ID="btnExportReport" OnClick="btnExportReport_Click" runat="server" Text="Export Report"
                                                        CssClass="btn btn-primary" Height="30px" Width="12%" Style="text-align: left;" />
                                                    <label class="glyphicon glyphicon-export" style="vertical-align: middle; margin-left: -2.5%; margin-right: 2%;
                                                        color: #fff;">
                                                    </label>
                                                    <asp:HiddenField ID="hidMessage" runat="server"></asp:HiddenField>
                                                    <asp:HiddenField ID="hidorgmsg" runat="server"></asp:HiddenField> 
<asp:Button ID="btnEditImport" OnClick="btnEditImport_Click" runat="server" Text="Edit"
                                                        CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                                    <label class="glyphicon glyphicon-log-out" style="vertical-align: middle; margin-left: -2.5%; margin-right: 2%;
                                                        color: #fff;">
                                                    </label>      

 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <asp:Button ID="btnClose" OnClick="btnClose_Click" runat="server" Text="Close" CssClass="btn btn-primary"
                                        Height="30px" Width="8%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-remove-circle" style="margin-left: -2.5%; margin-right: 2%;
                                        vertical-align: sub; color: #fff; margin-top:1%;">
                                    </label>
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                        CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-refresh" style="margin-left: -2.5%; margin-right: 2%;
                                        vertical-align: sub; color: #fff; margin-top:1%;">
                                    </label>
             </div>
 </div>
                    
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnEditImport" />
                    <asp:PostBackTrigger ControlID="btnExportReport" />
                    <asp:PostBackTrigger ControlID="btnEdit" />
                    <asp:PostBackTrigger ControlID="btnRefresh" />
                    <asp:PostBackTrigger ControlID="btnNew" />
                    <asp:AsyncPostBackTrigger ControlID="btnRun" />
                </Triggers>
            </asp:UpdatePanel>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>

</asp:Content>
