<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.Master" CodeBehind="ARTReadinessAssessment.aspx.cs"
    Inherits="HIVCE.Presentation.ARTReadinessAssessment" %>

<asp:Content ID="Data" ContentPlaceHolderID="SiteContent" runat="Server">
    <script src="<%=ResolveUrl("Scripts/ARTReadinessAssessment.js") %>" type="text/javascript"></script>
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
              <h3 class="box-title">ART Readiness Assessment</h3>
              <!-- /.box-tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body">
            <!-- / sub box level 1 -->
              <div class="box box-default box-solid">
                <div class="box-header with-border">
                  <h3 class="box-title">A. Psychosocial/Knowledge Criteria (applies to patients and caregivers)</h3>

                  <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                    </button>
                  </div>
                  <!-- /.box-tools -->
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                  <table class="table" style="border-top: 0px;">
                    <tr>
                        <td width="90%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            Select All&nbsp; <input type="checkbox" id="chkAllAdolescentService" class="flat-red" />
                        </td>
                        </tr>
                    <tr>
                        <td style="border-top: 0px;">
                            1. Understands the nature of HIV infection and benefits of ART?
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="PQ1" name="PQ1" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>
                    <tr>
                        <td style="border-top: 0px;">
                            2. Has screened negative for alcohol or other drug use disorder, or is stable on treatment
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="PQ2" name="PQ2" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>
                    <tr>
                        <td style="border-top: 0px;">
                            3. Has screened negative for depression or other psychiatric illness, or is stable on treatment
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="PQ3" name="PQ3" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>
                    <tr>
                        <td style="border-top: 0px;">
                           4. Is willing to disclose/has disclosed HIV status, ideally to a family member or close friend?
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="PQ4" name="PQ4" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>
                    <tr>
                        <td style="border-top: 0px;">
                            5. Has received demonstration of how to take/administer ART and other prescribed medication?
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="PQ5" name="PQ5" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>
                    <tr>
                        <td style="border-top: 0px;">
                            6. Has received information on predictable side effects of ART and understands what steps to take in case of these side effects?
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="PQ6" name="PQ6" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>
                    <tr>
                        <td style="border-top: 0px;">
                            7. For patients dependent on a caregiver: caregiver is committed to long-term support of the patient, daily administration of ART, and meets the criteria above?
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="PQ7" name="PQ7" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>
                    <tr>
                        <td style="border-top: 0px;">
                           8. Other likely barriers to adherence have been identified and there is a plan in place to address them
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="PQ8" name="PQ8" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>
                    <tr>
                        <td style="border-top: 0px;">
                            9. Patient/caregiver has provided accurate locator information and contact details?
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="PQ9" name="PQ9" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>
                    <tr>
                        <td style="border-top: 0px;">
                            10. Patient/caregiver feels ready to start ART today?
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="PQ10" name="PQ10" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>
                  </table>
                  

                </div>
                <!-- /.box-body -->
              </div>
              <div class="box box-default box-solid">
                <div class="box-header with-border">
                  <h3 class="box-title">B. Support Systems Criteria (applies to patients and caregivers)</h3>

                  <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                    </button>
                  </div>
                  <!-- /.box-tools -->
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                <table class="table" style="border-top: 0px;">
              <tr>
                        <td width="90%">
                          &nbsp;
                        </td>
                        <td width="10%">
                            Select All &nbsp; <input type="checkbox" id="chkAllCriteria" class="flat-red">
                        </td>
                        </tr>
                    <tr>
                        <td style="border-top: 0px;">
                            1. Has identified convenient time/s of day for taking ART, and/or associated dose/s with daily event/s?
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="SQ1" name="SQ1" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>

                        <tr>
                        <td style="border-top: 0px;">
                            2. Treatment supporter has been identified and engaged in HIV education, or will attend next counselling session?
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="SQ2" name="SQ2" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>

                        <tr>
                        <td style="border-top: 0px;">
                            3. Is aware of support group meeting time/s?
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="SQ3" name="SQ3" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>

                        <tr>
                        <td style="border-top: 0px;">
                           4. If facility has SMS reminder system: Has enrolled into SMS reminder system?
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="SQ4" name="SQ4" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>

                        <tr>
                        <td style="border-top: 0px;">
                            5. Other support systems are in place or planned (e.g. setting phone alarm, pill box)?
                        </td>
                                                <td style="border-top: 0px;">
                            <input id="SQ5" name="SQ5" type="checkbox" data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                        </tr>

                       
                        </table>
                </div>
                <!-- /.box-body -->
              </div>
              <!-- /.box sub box level 1 -->
            </div>
            <!-- /.box-body -->
             <div class="box-footer" align="center">
                     <input type="hidden" runat="server" id="hidId" clientidmode="Static" />
                  <button type="button" class="btn btn-primary" onclick="SaveData();">
                Save<span class="glyphicon glyphicon-floppy-disk" style="padding-left:5px;"></span></button>
                <button type="button" class="btn btn-primary" onclick="ResetData();">Reset<span class="glyphicon glyphicon-remove-circle" style="padding-left:5px;"></span></button>
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
    </div>
</asp:Content>
