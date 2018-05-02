<%@ Page AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master" Inherits="frmReport_PatientARVPickup"
    Language="C#" CodeBehind="frmReport_PatientARVPickup.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--   <form runat="server" id="frmPatientARVPickup">--%>
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtStartDate').datepicker({ autoclose: true });
            $('#txtEndDate').datepicker({ autoclose: true });
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
                            <h3 class="box-title">
                                Patient ARV Pick-up</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                            <%--Main Content--%>
                            <br />
                            <div class="row">
                                <input id="chkAll" enableviewstate="true" type="checkbox" value="None" name="All"
                                    runat="server" />
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Date Ordered From:</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="form-group">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input type="text" class="form-control pull-left" id="txtStartDate" clientidmode="Static"
                                                maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        To:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <div class="form-group">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input type="text" class="form-control pull-left" id="txtEndDate" clientidmode="Static"
                                                maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-5 col-sm-12 col-xs-12 form-group" align="center">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmitClick"
                                        CssClass="btn btn-primary" Height="30px" Width="20%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-log-in" style="margin-left: -4%; margin-right: 2%;
                                        vertical-align: sub; color: #fff;">
                                    </label>
                                    <asp:Button ID="btnCancel" runat="server" Text="Back" OnClick="btnCancel_Click" CssClass="btn btn-primary"
                                        Height="30px" Width="20%" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-backward" style="margin-left: -4%; margin-right: 2%;
                                        vertical-align: sub; color: #fff;">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
