<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.Master" CodeBehind="ClinicalEncounter.aspx.cs"
    Inherits="HIVCE.Presentation.ClinicalEncounter" %>

<%@ Register Src="Controls/ucHIVCETriage.ascx" TagName="ucHIVCETriage" TagPrefix="uc1" %>
<%@ Register Src="Controls/ucPresentingComplaints.ascx" TagName="ucPresentingComplaints"
    TagPrefix="uc1" %>
<%@ Register Src="Controls/ucAddtionalHx.ascx" TagName="ucAddtionalHx" TagPrefix="uc1" %>
<%@ Register Src="Controls/ucScreening.ascx" TagName="ucScreening" TagPrefix="uc1" %>
<%@ Register Src="Controls/ucManagementx.ascx" TagName="ucManagementx" TagPrefix="uc1" %>
<%@ Register Src="Controls/ucSystemicReview.ascx" TagName="ucSystemicReview" TagPrefix="uc1" %>
<asp:Content ID="Data" ContentPlaceHolderID="SiteContent" runat="Server">
    <script src="<%=ResolveUrl("Scripts/ClinicalEncounter.js") %>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ucTriage.js") %>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ucPresentingComplaints.js") %>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/ucAdditionalHx.js") %>" type="text/javascript"></script>
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
              <h3 class="box-title">HIV Clinical Encounter Form</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
              </div>
              <!-- /.box-tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body">
             <div class="col-md-12 col-sm-12 col-xs-12">

<!-- Custom Tabs -->
          <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
              <li class="active"><a href="#tab_1" data-toggle="tab">Triage</a></li>
              <li><a href="#tab_2" data-toggle="tab">Presenting Complaints</a></li>
              <li><a href="#tab_3" data-toggle="tab">Addtional Hx</a></li>
              <%--<li><a href="#tab_4" data-toggle="tab">Screening</a></li>
              <li><a href="#tab_5" data-toggle="tab">Systemic Review</a></li>
              <li><a href="#tab_6" data-toggle="tab">Managementx</a></li>--%>
            </ul>
            <div class="tab-content">
              <div class="tab-pane active" id="tab_1">
                <uc1:ucHIVCETriage ID="ucHIVCETriage1" runat="server" />
              </div>
              <!-- /.tab-pane -->
              <div class="tab-pane" id="tab_2">
              <uc1:ucPresentingComplaints ID="ucPresentingComplaints1" runat="server" />
              </div>
              <!-- /.tab-pane -->
              <div class="tab-pane" id="tab_3">
               <uc1:ucAddtionalHx ID="ucAddtionalHx" runat="server" />
              </div>
              <%--<!-- /.tab-pane -->
               <div class="tab-pane" id="tab_4">
               <uc1:ucScreening ID="ucScreening" runat="server" />
              </div>
              <!-- /.tab-pane -->
               <div class="tab-pane" id="tab_5">
               <uc1:ucSystemicReview ID="ucSystemicReview" runat="server" />
              </div>
              <div class="tab-pane" id="tab_6">
               <uc1:ucManagementx ID="ucManagementx" runat="server" />
              </div>--%>
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
        <input type="hidden" runat="server" id="hidDOB" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidPID" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidGender" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidVId" clientidmode="Static" />
    </div>
</asp:Content>
