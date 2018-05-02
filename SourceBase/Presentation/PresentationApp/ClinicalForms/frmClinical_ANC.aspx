<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    CodeBehind="frmClinical_ANC.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmClinical_ANC" %>

<%@ Register Src="UserControl/ucANCProfile.ascx" TagName="ucANCProfile" TagPrefix="uc1" %>
<%@ Register Src="UserControl/ucANCScreeningAndTests.ascx" TagName="ucANCScreeningAndTests"
    TagPrefix="uc2" %>
<%@ Register Src="UserControl/ucANCVisitDetails.ascx" TagName="ucANCVisitDetails"
    TagPrefix="uc3" %>
<asp:Content ID="Data" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script src="<%=ResolveUrl("~/Incl/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Incl/Constants.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
            type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Incl/ANC.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Incl/ANCProfile.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Incl/ANCScreeningAndTests.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Incl/ANCVisitDetails.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
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
                                    <h3 class="box-title">ANC Form</h3>
                                    <!-- /.box-tools -->
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="col-md-12 col-sm-12 col-xs-12" style="padding-left:7px;padding-right:7px;">

                                        <!-- Custom Tabs -->
                                        <div class="nav-tabs-custom">
                                            <ul class="nav nav-tabs">
                                                <li class="active"><a href="#tab_1" data-toggle="tab">Profile</a></li>
                                                <li><a href="#tab_2" data-toggle="tab" id="a_SAT">Screening and Tests</a></li>
                                                <li><a href="#tab_3" data-toggle="tab" id="a_VD">Visit Details</a></li>
                                            </ul>
                                            <div class="tab-content">
                                                <div class="tab-pane active" id="tab_1">
                                                    <uc1:ucANCProfile ID="ucANCProfile1" runat="server" />
                                                </div>
                                                <!-- /.tab-pane -->
                                                <div class="tab-pane" id="tab_2">
                                                    <uc2:ucANCScreeningAndTests ID="ucANCScreeningAndTests1" runat="server" />
                                                </div>
                                                <!-- /.tab-pane -->
                                                <div class="tab-pane" id="tab_3">
                                                    <uc3:ucANCVisitDetails ID="ucANCVisitDetails1" runat="server" />
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
        <input type="hidden" runat="server" id="hidTabName" clientidmode="Static" value="profile"
            runat="server" />
        <input type="hidden" runat="server" id="hidMOD" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidsrvNm" clientidmode="Static" />
    </div>
</asp:Content>
