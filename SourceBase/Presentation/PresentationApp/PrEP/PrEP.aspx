<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    CodeBehind="PrEP.aspx.cs" Inherits="PresentationApp.PrEP.PrEP" %>

<%@ Register Src="~/PrEP/Controls/ucPrEPTriage.ascx" TagName="ucPrEPTriage" TagPrefix="uc1" %>
<%@ Register Src="~/PrEP/Controls/ucPrEPAssessment.ascx" TagName="ucPrEPAssessment"
    TagPrefix="uc2" %>
<%@ Register Src="~/PrEP/Controls/ucPrEPInitiation.ascx" TagName="ucPrEPInitiation"
    TagPrefix="uc3" %>
<asp:Content ID="Data" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script src="<%=ResolveUrl("Scripts/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/Constants.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/PrEP.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/checktree.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ucPrEPTriage.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ucPrEPAssessment.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ucPrEPInitiation.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-maxlength/1.7.0/bootstrap-maxlength.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () { $("[name='switch-size']").bootstrapSwitch(); });
    </script>
    <div class="content-wrapper">
        <div class="content">
            <div class="box-body">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary box-solid">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    PrEP Form</h3>
                            </div>
                            <div class="box-body">
                                <div class="col-md-12 col-sm-12 col-xs-12" style="padding-left: 7px; padding-right: 7px;">
                                    <div class="nav-tabs-custom">
                                        <ul class="nav nav-tabs">
                                            <li class="active"><a href="#tab_1" data-toggle="tab">PrEP Triage</a></li>
                                            <li><a href="#tab_2" data-toggle="tab" class="disabled" id="a_PA">PrEP Assessment</a></li>
                                            <li><a href="#tab_3" data-toggle="tab" class="disabled" id="a_PI">PrEP Initiation</a></li>
                                            <%--<li><a href="#tab_2" data-toggle="tab" id="a_PA">PrEP Assessment</a></li>
                                            <li><a href="#tab_3" data-toggle="tab" id="a_PI">PrEP Initiation</a></li>--%>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab_1">
                                                <uc1:ucPrEPTriage ID="ucPrEPTriage1" runat="server" />
                                            </div>
                                            <div class="tab-pane" id="tab_2">
                                                <uc2:ucPrEPAssessment ID="ucPrEPAssessment1" runat="server" />
                                            </div>
                                            <div class="tab-pane" id="tab_3">
                                                <uc3:ucPrEPInitiation ID="ucPrEPInitiation1" runat="server" />
                                            </div>
                                        </div>
                                        <!-- /.tab-content -->
                                    </div>
                                    <!-- nav-tabs-custom -->
                                </div>
                            </div>
                            <!-- /.box-body -->
                        </div>
                        <!-- /.box -->
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
        </div>
        <!-- /.content -->
        <input type="hidden" runat="server" id="hidPAYM" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidDOB" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidPID" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidGender" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidVId" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidL" clientidmode="Static" value="0" />
        <input type="hidden" runat="server" id="hidM" clientidmode="Static" value="0" />
        <input type="hidden" runat="server" id="hidS" clientidmode="Static" value="0" />
        <input type="hidden" runat="server" id="hidTabName" clientidmode="Static" value="triage" />
        <input type="hidden" runat="server" id="hidARTStartDate" clientidmode="Static" value="" />
        <input type="hidden" runat="server" id="hidEIVID" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidEIVD" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidMOD" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidsrvNm" clientidmode="Static" />
    </div>
</asp:Content>
