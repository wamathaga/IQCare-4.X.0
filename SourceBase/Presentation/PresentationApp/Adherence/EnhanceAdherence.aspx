<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    CodeBehind="EnhanceAdherence.aspx.cs" Inherits="PresentationApp.Adherence.EnhanceAdherence" %>

<%@ Register Src="~/Adherence/Controls/ucCounsellingSession1.ascx" TagName="ucSession1"
    TagPrefix="uc1" %>
<%@ Register Src="~/Adherence/Controls/ucCounsellingSession2.ascx" TagName="ucSession2"
    TagPrefix="uc2" %>
<%@ Register Src="~/Adherence/Controls/ucCounsellingSession3.ascx" TagName="ucSession3"
    TagPrefix="uc3" %>
<%@ Register Src="~/Adherence/Controls/ucCounsellingSession4.ascx" TagName="ucSession4"
    TagPrefix="uc4" %>
<%@ Register Src="~/Adherence/Controls/ucCounsellingSessionVL.ascx" TagName="ucSessionVL"
    TagPrefix="uc5" %>
<asp:Content ID="Data" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script src="<%=ResolveUrl("~/Incl/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Incl/Constants.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/AdherenceBarriers.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/EnhanceAdherence.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ucSession1.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ucSession2.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ucSession3.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ucSession4.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ucSessionVL.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-maxlength/1.7.0/bootstrap-maxlength.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-maxlength/1.7.0/bootstrap-maxlength.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () { $("[name='switch-size']").bootstrapSwitch(); });
    </script>
    <div class="content-wrapper">
        <!-- Main content -->
        <div class="content">
            <div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box box-primary box-solid">
                                <div class="box-header with-border">
                                    <h3 class="box-title">
                                        Enhance Adherence Counselling</h3>
                                </div>
                                <div class="box-body">
                                    <div class="col-md-12 col-sm-12 col-xs-12" style="padding-left: 7px; padding-right: 7px;">
                                        <div class="nav-tabs-custom">
                                            <ul class="nav nav-tabs">
                                                <li class="active"><a href="#tab_1" data-toggle="tab">Session 1</a></li>
                                                <li><a href="#tab_2" data-toggle="tab" class="disabled" id="S2">Session 2</a></li>
                                                <li><a href="#tab_3" data-toggle="tab" class="disabled" id="S3">Session 3</a></li>
                                                <li><a href="#tab_4" data-toggle="tab" class="disabled" id="S4">Session 4</a></li>
                                                <li><a href="#tab_5" data-toggle="tab" class="disabled" id="S5">Viral Load</a></li>
                                            </ul>
                                            <div class="tab-content">
                                                <div class="tab-pane active" id="tab_1">
                                                    <uc1:ucSession1 ID="ucSession1" runat="server" />
                                                </div>
                                                <div class="tab-pane" id="tab_2">
                                                    <uc2:ucSession2 ID="ucSession2" runat="server" />
                                                </div>
                                                <div class="tab-pane" id="tab_3">
                                                    <uc3:ucSession3 ID="ucSession3" runat="server" />
                                                </div>
                                                <div class="tab-pane" id="tab_4">
                                                    <uc4:ucSession4 ID="ucSession4" runat="server" />
                                                </div>
                                                <div class="tab-pane" id="tab_5">
                                                    <uc5:ucSessionVL ID="ucSessionVL" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row no-print" align="center">
                                    <div class="form-group">
                                        <div class="col-md-12" align="center">
                                            <button type="button" class="btn btn-primary" runat="server" onclick="DeleteForm();"
                                                id="btnDeleteForm" clientidmode="Static" style="margin-bottom: 1%;">
                                                Delete<span class="glyphicon glyphicon glyphicon-remove" style="padding-left: 5px;"></span></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <input type="hidden" runat="server" id="hidPAYM" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidDOB" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidPID" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidGender" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidVId" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidL" clientidmode="Static" value="0" />
        <input type="hidden" runat="server" id="hidM" clientidmode="Static" value="0" />
        <input type="hidden" runat="server" id="hidS" clientidmode="Static" value="0" />
        <input type="hidden" runat="server" id="hidTabName" clientidmode="Static" value="Session1" />
        <input type="hidden" runat="server" id="hidARTStartDate" clientidmode="Static" value="" />
        <input type="hidden" runat="server" id="hidEIVID" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidEIVD" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidMOD" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidsrvNm" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidCallFrom" clientidmode="Static" />
    </div>
</asp:Content>
