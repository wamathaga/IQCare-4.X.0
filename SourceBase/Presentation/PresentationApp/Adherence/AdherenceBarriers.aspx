<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    CodeBehind="AdherenceBarriers.aspx.cs" Inherits="PresentationApp.Adherence.AdherenceBarriers" %>

<%@ Register Src="Controls/ucBarriersSection1.ascx" TagName="ucBarriersSection1"
    TagPrefix="uc1" %>
<%@ Register Src="Controls/ucBarriersSection2.ascx" TagName="ucBarriersSection2"
    TagPrefix="uc2" %>
<%@ Register Src="Controls/ucBarriersSection3.ascx" TagName="ucBarriersSection3"
    TagPrefix="uc3" %>
<%@ Register Src="Controls/ucBarriersSection4.ascx" TagName="ucBarriersSection4"
    TagPrefix="uc4" %>
<asp:Content ID="Data" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script src="<%=ResolveUrl("~/Incl/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Incl/Constants.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/AdherenceBarriers.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-maxlength/1.7.0/bootstrap-maxlength.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () { $("[name='switch-size']").bootstrapSwitch(); });
    </script>
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div>
                <%--<div class="box-header with-border">
          <h3 class="box-title">Title</h3>
        </div>--%>
                <div class="box-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box box-primary box-solid">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Assessing Barriers to Adherence</h3>
                                    <!-- /.box-tools -->
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="col-md-12 col-sm-12 col-xs-12" style="padding-left:7px;padding-right:7px;">
                                        <!-- Custom Tabs -->
                                        <div class="nav-tabs-custom">
                                            <ul class="nav nav-tabs">
                                                <li class="active"><a href="#tab_1" data-toggle="tab" id="a_S1">Section 1</a></li>
                                                <li><a href="#tab_2" data-toggle="tab" id="a_S2">Section 2</a></li>
                                                <li><a href="#tab_3" data-toggle="tab" id="a_S3">Section 3</a></li>
                                                <li><a href="#tab_4" data-toggle="tab" id="a_S4">Section 4</a></li>
                                            </ul>
                                            <div class="tab-content">
                                                <div class="tab-pane active" id="tab_1">
                                                    <uc1:ucBarriersSection1 ID="ucBarriersSection11" runat="server" />
                                                </div>
                                                <!-- /.tab-pane -->
                                                <div class="tab-pane" id="tab_2">
                                                    <uc2:ucBarriersSection2 ID="ucBarriersSection21" runat="server" />
                                                   </div>
                                                <!-- /.tab-pane -->
                                                <div class="tab-pane" id="tab_3">
                                                    <uc3:ucBarriersSection3 ID="ucBarriersSection31" runat="server" />
                                                </div>
                                                <!-- /.tab-pane -->
                                                <div class="tab-pane" id="tab_4">
                                                    <uc4:ucBarriersSection4 ID="ucBarriersSection41" runat="server" />
                                                </div>
                                            </div>
                                            <!-- /.tab-content -->
                                        </div>
                                        <!-- nav-tabs-custom -->
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <!-- Button Footer -->
                                <div class="row no-print" align="center" style="margin-bottom: 15px;">
                                    <div class="form-group">
                                        <div class="col-md-12" align="center">
                                            
                                            <button type="button" class="btn btn-primary" runat="server" onclick="DeleteForm();"
                                                    id="btnDeleteForm" clientidmode="Static">
                                                Delete<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /.box -->
                        </div>
                    </div>
                   
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.box -->
            
        </section>
        <!-- /.content -->
        <input type="hidden" runat="server" id="hidPAYM" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidDOB" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidPID" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidGender" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidVId" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidsrvNm" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidMOD" clientidmode="Static" />
    </div>
</asp:Content>
