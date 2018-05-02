<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="frmClinicalSummary.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinicalSummary" %>
<%@ Register Src="UserControl/ucCaseSummary.ascx" TagName="ucCaseSummary" TagPrefix="uc1" %>
<%@ Register Src="UserControl/ucEvaluation.ascx" TagName="ucEvaluation" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
 <script src="<%=ResolveUrl("~/Incl/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Incl/Constants.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Incl/ClinicalSummary.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
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
                <div class="box-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box box-primary box-solid">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Clinical Summary Form</h3>
                                    <!-- /.box-tools -->
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="col-md-12 col-sm-12 col-xs-12" style="padding-left:7px;padding-right:7px;">

                                        <!-- Custom Tabs -->
                                        <div class="nav-tabs-custom">
                                            <ul class="nav nav-tabs">
                                                <li class="active"><a href="#tab_1" data-toggle="tab">Case Summary</a></li>
                                                <li><a href="#tab_2" data-toggle="tab" class="" id="a_CSF">Evaluation</a></li>                                                
                                            </ul>
                                            <div class="tab-content">
                                                <div class="tab-pane active" id="tab_1">
                                                    <uc1:ucCaseSummary ID="ucCaseSummary1" runat="server" />
                                                </div>
                                                <!-- /.tab-pane -->
                                                <div class="tab-pane" id="tab_2">
                                                    <uc2:ucEvaluation ID="ucEvaluation1" runat="server" />
                                                </div>                                               
                                                <!-- /.tab-pane -->
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
            <!-- /.box -->
        </section>
        <!-- /.content -->
        <input type="hidden" runat="server" id="hidPAYM" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidDOB" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidPID" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidGender" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidVId" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidTabName" clientidmode="Static" value="Case Summary"/>
        <input type="hidden" runat="server" id="hidMOD" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidsrvNm" clientidmode="Static" />
    </div>
</asp:Content>
