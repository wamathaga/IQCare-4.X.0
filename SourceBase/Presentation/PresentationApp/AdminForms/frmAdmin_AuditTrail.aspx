<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="AdminForms_frmAdmin_AuditTrail" EnableEventValidation="false" CodeBehind="frmAdmin_AuditTrail.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtFromDate').datepicker({ autoclose: true });
        $('#txtToDate').datepicker({ autocloae: true });
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>

<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title"><asp:Label ID="lblh2" runat="server" Text="Audit Trail"></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="lblForms" for="Forms" class="control-label">Forms</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddAuditTrail" runat="server" AutoPostBack="false" CssClass="form-control" Width="300"></asp:DropDownList>
             </div>
                                 
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="lblFromDate" class="control-label required" for="FromDate">From Date:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtFromDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
             
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <label id="lblToDate" class="control-label required" for="ToDate">To Date:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtToDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
                                            
             </div>                      
    </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" CssClass="btn btn-primary"
                            Height="30px" Width="8%" Style="text-align: left;" />
                        <label class="glyphicon glyphicon-eye-open" style="margin-left: -3%; margin-right: 2%;
                            vertical-align: sub; color: #fff; margin-top: 1%;">
                        </label>
                        <asp:Button ID="btnExporttoExcel" runat="server" Text="Export to Excel" Enabled="false"
                            OnClick="btnExporttoExcel_Click" CssClass="btn btn-primary" Height="30px" Width="15%"
                            Style="text-align: left;" />
                        <label class="glyphicon glyphicon-export" style="margin-left: -3%; margin-right: 2%;
                            vertical-align: sub; color: #fff; margin-top: 1%;">
                        </label>
 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="grid">
                            <div class="rounded">
                                <div class="top-outer">
                                    <div class="top-inner">
                                        <div class="top">
                                            <h2>
                                                Searched Result</h2>
                                        </div>
                                    </div>
                                </div>
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid" style="cursor: pointer; height: 280px; overflow: auto; border: 1px solid #666699;">
                                            <div id="div-gridview" class="whitebg">
                                                <asp:GridView ID="GrdAuditTrail" runat="server" Width="100%" OnRowDataBound="GrdAuditTrail_RowDataBound"
                                                    PageSize="1" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" CellPadding="0"
                                                    CellSpacing="0" BorderWidth="0" GridLines="None" AllowSorting="True">
                                                    
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
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>
</asp:Content>
