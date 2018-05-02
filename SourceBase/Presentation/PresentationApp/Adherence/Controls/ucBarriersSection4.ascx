<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBarriersSection4.ascx.cs"
    Inherits="PresentationApp.Adherence.Controls.ucBarriersSection4" %>
<!-- Daily Routine -->
<div class="box box-default box-solid" id="divHIVCare">
    <div class="box-header with-border">
        <h3 class="box-title">
            E. GBV, Alcohol and Depression Health screening</h3>
        <div class="box-tools pull-right">
        </div>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="x_content">
                    <div class="row">
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            Total =
                            <input id="txtTotal" type="text" disabled="disabled" class="form-control" value="0"
                                style="width: 30%; display: inline;">
                            / 30
                        </div>
                        <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                            Depression Severity:
                            <input type="text" id="txtDepressionSeverity" disabled="disabled" value="None" class="form-control"
                                style="width: 55%; display: inline;">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                            Recommended Management:
                            <input type="text" id="txtRecommendedManagement" disabled="disabled" value="None"
                                class="form-control" style="width: 55%; display: inline;">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5 col-sm-12 col-xs-12 form-group" style="vertical-align: center;">
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group" style="vertical-align: center;">
                            <button type="button" class="btn btn-primary" onclick="OpenCEPopup('<%= ResolveUrl("~/HIVCE/AlcoholDepressionScreening.aspx?add=0") %>');">
                                Click to open<span class="glyphicon glyphicon-pencil" style="padding-left: 5px;"></span></button>
                        </div>
                        <div class="col-md-5 col-sm-12 col-xs-12 form-group" style="vertical-align: center;">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /.box-body -->
</div>
<div class="box box-default box-solid" id="div1">
    <div class="box-header with-border">
        <h3 class="box-title">
            F. Referrals and networks</h3>
        <div class="box-tools pull-right">
        </div>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <table class="table" style="padding-left: 10px; width: 80%;">
                    <tr>
                        <td style="border-top: 0px; width: 90%">
                            Has the patient been referred to other services? (nutrition, psychosocial support
                            services, other medical clinics, substance use treatment, etc)
                        </td>
                        <td style="border-top: 0px; width: 10%">
                            <input id="chkReferredOtherServices" name="switch-size" type="checkbox" checked data-size="small"
                                data-on-text="Yes" data-off-text="No" />
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top: 0px; width: 90%">
                            Did he/she attend the appointments?
                        </td>
                        <td style="border-top: 0px; width: 10%">
                            <input id="chkAppointmentsAttend" name="switch-size" type="checkbox" checked data-size="small"
                                data-on-text="Yes" data-off-text="No" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="box box-primary" id="div8">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            What was the experience? Do the referrals need to be re-organized?</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i id="I7" class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <textarea class="form-control" rows="6" placeholder="Notes" id="txtExperienceAppointReorganized"
                                    maxlength="1000" style="resize: none;"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.box-body -->
    </div>
</div>
<!-- Button Footer -->
<div class="row no-print" align="center" style="margin-bottom: 10px;">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <button type="button" class="btn btn-primary" onclick="GetPreviousPage('tab_3');"
                runat="server" id="btnS4Previous" clientidmode="Static">
                Previous<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
            <button type="button" class="btn btn-primary" onclick="SavePatientAdherenceBarriers();"
                runat="server" id="btnSaveForm" clientidmode="Static">
                Save<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
