<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmReportDebitNote" Title="Untitled Page" CodeBehind="frmReportDebitNote.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#TxtFromDate').datepicker({ autoclose: true });
        $('#TxtToDate').datepicker({ autoclose: true });
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
    <head>
        <%--  <link type="text/css" href="../Style/_assets/css/grid.css" rel="stylesheet" />--%>
        <%-- <link rel="stylesheet" type="text/css" href="../Style/StyleSheet.css" />--%>
        <%--   <link type="text/css" href="../Style/_assets/css/round.css" rel="stylesheet" />--%>
        <script type="text/javascript" language="javascript">
            function fnOpenWin(url) {
                //var result=frmFindAddPatient.SetPatientId_Session(url).value;
                //alert(url);
                window.open(url, 'ab', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,scrollbars=yes');

            }
        </script>
    </head>
    <%--    <link type="text/css" href="Style/_assets/css/grid.css" rel="stylesheet" />--%>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Debit Note</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content--%>
         <div class="row" style="margin-left:5px;">
         <br />
            <asp:Label ID="LabelRange" runat="server" Text="No Open Transaction"></asp:Label>
            </div>
            <div class="row">
            <br />
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
               <label for="inputEmail3" class="control-label">
                  From:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="TxtFromDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
                                            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
               <label for="inputEmail3" class="control-label">
                  To:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="TxtToDate" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
                                             </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="ButtonShow" runat="server" Text="Show transactions for date range"
                                                                    OnClick="ButtonShow_Click" CssClass="btn btn-primary" Height="30px" Width="75%"
                                                                    Style="text-align: left;" />
                                                                <label class="glyphicon glyphicon-list" style="margin-left: -9%;
                                                                    margin-right: 2%; vertical-align: sub; color: #fff; margin-top:1%;">
                                                                </label>
            </div>
            </div>
            <div class="row text-center">
            <div class="grid" style="margin-left:1%;">
                                                                    <div class="mid" style="cursor: pointer; height: 280px; width:98%; overflow: auto; border: 1px solid #666699;">
                                                                        <div id="div-gridview" class="gridView whitebg">
                                                                            <asp:GridView ID="GridViewTran" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                                                                                GridLines="None" AllowSorting="True">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="TransactionDateFmt" HeaderText="Transaction Date" ReadOnly="True" />
                                                                                    <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" />
                                                                                    <asp:BoundField DataField="Cost" HeaderText="Cost" ReadOnly="True" />
                                                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" />
                                                                                </Columns>
                                                                                
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
            </div>
             <div class="row alert">
             **Warning: By submitting this report you will be creating a bill for the transactions
                                shown above.
            </div>
             <div class="row text-center">
             <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                                    CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-log-in" style="margin-left: -2.5%;
                                    margin-right: 2%; vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                    CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-remove" style="margin-left: -2.5%;
                                    margin-right: 2%; vertical-align: sub; color: #fff; ">
                                </label>
            </div>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>
</asp:Content>
