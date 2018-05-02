<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucManagementx.ascx.cs"
    Inherits="HIVCE.Presentation.Controls.ucManagementx" %>
<!-- / sub box level G1 -->
<div id="divG1" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            ART Therapy</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <!-- / sub box level 2 -->
        <div class="box box-default box-solid">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            ART Side Effect</label>
                        <input id="chkARTSideEffect" name="switch-size" type="checkbox" checked data-size="small"
                            data-on-text="Yes" data-off-text="No">
                    </div>
                </div>
                <div class="row" id="divARTSideEffect">
                    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Short Term Side Effects
                        </label>
                        <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                            id="ddlSortTermSideEffect" style="width: 100%;">
                        </select>
                    </div>
                    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Long Term Side Effects
                        </label>
                        <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                            id="ddlLongTermSideEffect" style="width: 100%;">
                        </select>
                    </div>
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.box sub box level 2 -->
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box sub box level G1 -->
</div>
<!-- / sub box level G1 -->
<div id="div1" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Adherence</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <!-- / sub box level 2 -->
        <div class="box box-default box-solid">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <table class="table" style="padding-left: 10px; margin-bottom: 0px;">
                            <tr>
                                <td colspan="2" style="border-top: 0px; padding: 2px;">
                                    <div class="box box-default box-solid" style="margin-bottom: 5px;">
                                        <div class="box-header">
                                            <h5 class="box-title">
                                                Morisky Medication Adherence Scale (MMAS-4)</h5>
                                            <!-- /.box-tools -->
                                        </div>
                                        <!-- /.box-header -->
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: 0px; width: 90%">
                                    1. Do you forget to take any of your medicine since the last visit?
                                </td>
                                <td style="border-top: 0px; width: 10%">
                                    <input id="chkForgotMed" name="switch-size" type="checkbox" checked data-size="small"
                                        data-on-text="Yes" data-off-text="No">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: 0px; width: 90%">
                                    2. Are you careless at times about taking your medicine?
                                </td>
                                <td style="border-top: 0px; width: 10%">
                                    <input id="chkCarelessMed" name="switch-size" type="checkbox" checked data-size="small"
                                        data-on-text="Yes" data-off-text="No">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: 0px; width: 90%">
                                    3. Sometimes if you feel worse when you take the medicine, do you stop taking it?
                                </td>
                                <td style="border-top: 0px; width: 10%">
                                    <input id="chkWorseTakingMed" name="switch-size" type="checkbox" checked data-size="small"
                                        data-on-text="Yes" data-off-text="No">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: 0px; width: 90%">
                                    4. When you feel better do you sometimes stop taking your medicine?
                                </td>
                                <td style="border-top: 0px; width: 10%">
                                    <input id="chkFeelBetterMed" name="switch-size" type="checkbox" checked data-size="small"
                                        data-on-text="Yes" data-off-text="No">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-top: 0px;">
                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            (MMAS-4) Score =
                                            <input id="txtMMAS4Score" type="text" disabled="disabled" class="form-control" value="0"
                                                style="width: 30%; display: inline;">
                                            / 4
                                        </div>
                                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                            Adherence Rating:
                                            <input type="text" id="txtMMAS4Rating" disabled="disabled" value="Good" class="form-control"
                                                style="width: 55%; display: inline;">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div id="divOtherQesAdherence">
                            <table class="table" style="padding-left: 10px;">
                                <tr>
                                    <td style="border-top: 0px; width: 90%">
                                        5. Did you take your medicine yesterday?
                                    </td>
                                    <td style="border-top: 0px; width: 10%">
                                        <input id="chkYesterdayMed" name="switch-size" type="checkbox" checked data-size="small"
                                            data-on-text="Yes" data-off-text="No">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-top: 0px; width: 90%">
                                        6. When you feel like your symptoms are under control, do you sometimes stop taking
                                        your medicine?
                                    </td>
                                    <td style="border-top: 0px; width: 10%">
                                        <input id="chkSymptomUnderControl" name="switch-size" type="checkbox" checked data-size="small"
                                            data-on-text="Yes" data-off-text="No">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-top: 0px; width: 90%">
                                        7. Taking medication every day is a real inconvenience for some people. Do you ever
                                        feel under pressure about sticking to your treatment plan?
                                    </td>
                                    <td style="border-top: 0px; width: 10%">
                                        <input id="chkStickingTreatmentPlan" name="switch-size" type="checkbox" checked data-size="small"
                                            data-on-text="Yes" data-off-text="No">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-top: 0px; width: 90%">
                                        8. How often do you have difficulty remembering to take all your medications?
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-top: 0px; width: 10%">
                                        <div class="row">
                                            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            </div>
                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                <div id="divMrgRM">
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-top: 0px;">
                                        <div class="row">
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                (MMAS-8) Score =
                                                <input id="txtMMAS8Score" type="text" disabled="disabled" class="form-control" value="0"
                                                    style="width: 30%; display: inline;">
                                                / 8
                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                Adherence Rating:
                                                <input type="text" id="txtMMAS8Rating" disabled="disabled" value="None" class="form-control"
                                                    style="width: 55%; display: inline;">
                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                <input type="text" id="txtMMAS8Suggestion" disabled="disabled" value="None" class="form-control"
                                                    style="width: 55%; display: inline;">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="row" id="div2">
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label requiredFieldColor">
                            CTX/Dapson: Adherence:
                        </label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlCTXAdhere"
                            style="width: 100%;">
                        </select>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Why Poor/Fair:
                        </label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlCTXWhypoorfair"
                            style="width: 100%;">
                        </select>
                    </div>
                    <%--</div>
                <div class="row" id="div3">--%>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label requiredFieldColor">
                            ARV: Adherence:
                        </label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlARVAdhere"
                            style="width: 100%;">
                        </select>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Why Poor/Fair:
                        </label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlARVWhypoorfair"
                            style="width: 100%;">
                        </select>
                    </div>
                </div>
                <div class="row" id="div4">
                    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label requiredFieldColor">
                            Substitutions/Interruptions:
                        </label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlsubsituationInterruption"
                            style="width: 100%;">
                        </select>
                    </div>
                    <div class="col-md-6 col-sm-12 col-xs-12 form-group" id="divRegimenCodes">
                        <label for="inputEmail3" class="control-label requiredFieldColor">
                            Regimen Code:
                        </label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlRegimenCode"
                            style="width: 100%;">
                        </select>
                    </div>
                </div>
                <div class="row" id="divEligibleART">
                    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Eligible for ART Through
                        </label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlEligibleART"
                            style="width: 100%;">
                        </select>
                    </div>
                </div>
                <div class="row" id="divChangeRegimen">
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Change Regimen Reason:
                        </label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlChangeRegimenReason"
                            style="width: 100%;">
                        </select>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Therapy Change discussed by MDT?</label>
                        <input id="chkThreapyChange" name="switch-size" type="checkbox" checked data-size="small"
                            data-on-text="Yes" data-off-text="No">
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group" id="divThreapyChangeComments">
                        <textarea class="form-control" rows="2" placeholder="Comments" id="txtAreaChangeDiscussMDT"
                             maxlength="1000"></textarea>
                    </div>
                </div>
                <div class="row" id="divARTStop">
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Date ART Ended
                        </label>
                        <input type="text" class="form-control" id="dtARTDateEnded" placeholder="Date ART Ended"
                            data-date-format="dd-M-yyyy">
                    </div>
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Stop Regimen Reason:
                        </label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlStopRegimenReason"
                            style="width: 100%;">
                        </select>
                    </div>
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.box sub box level 2 -->
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box sub box level G1 -->
</div>
<!-- / sub box level G1 -->
<div class="box box-default box-solid">
    <div class="box-header with-border" id="div3PositivePreventionH">
        <h3 class="box-title">
            Positive Prevention</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body" id="div3PositivePreventionB">
        <!-- / sub box level 2 -->
        <div class="box box-default box-solid">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Key Population?</label>
                        <input id="chkKeyPopulation" name="switch-size" type="checkbox" checked data-size="small"
                            data-on-text="Yes" data-off-text="No">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                        <div id="divKeyPopulation">
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            STI screening done during this visit?</label>
                        <input id="chkSTIScreening" name="switch-size" type="checkbox" checked data-size="small"
                            data-on-text="Yes" data-off-text="No">
                    </div>
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Partner Notified?</label>
                        <input id="chkPartnerNotified" name="switch-size" type="checkbox" checked data-size="small"
                            data-on-text="Yes" data-off-text="No">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label requiredFieldColor">
                            Prevention with positives (PwP):
                        </label>
                    </div>
                    <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                        <select class="form-control select2" data-placeholder="Select" multiple="multiple"
                            id="ddlPWP" style="width: 100%;">
                        </select>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    </div>
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.box sub box level 2 -->
        </div>
        <!-- /.box-body -->
    </div>
    <div class="box-header with-border" id="divMgrDiagnosisH">
        <h3 class="box-title">
            Diagnosis
        </h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body" id="divMgrDiagnosisB">
        <!-- / sub box level 2 -->
        <div class="box box-default box-solid">
            <!-- /.box-header -->
            <div class="box-body">
                <!-- /.box-body -->
                <div class="row">
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Diagnosis (ICD 10):
                        </label>
                    </div>
                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                        <select class="form-control select2" data-placeholder="Select" id="ddlICD" style="width: 100%;">
                        </select>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                            id="btnICD10" onclick="AddICD10();">
                            Add</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <table id="dtlICD10" class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th style="width: 50px;">
                                        ICD Id
                                    </th>
                                    <th style="width: 150px;">
                                        ICD Name
                                    </th>
                                    <th style="width: 150px;">
                                        Chapter Name
                                    </th>
                                    <th style="width: 200px;">
                                        Block Code - Name
                                    </th>
                                    <th style="width: 200px;">
                                        Sub Block Code - Name
                                    </th>
                                    <th style="width: 10px;">
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <textarea class="form-control" rows="5" placeholder="ICD Comments" id="txtICDComments" maxlength="1000"></textarea>
                    </div>
                </div>
            </div>
            <!-- /.box sub box level 2 -->
        </div>
        <!-- /.box-body -->
    </div>
    <div class="box-header with-border" id="divMgrRAWH">
        <h3 class="box-title">
            Reminders and workplan
        </h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body" id="divMgrRAWB">
        <!-- / sub box level 2 -->
        <div class="box box-default box-solid">
            <!-- /.box-header -->
            <div class="box-body">
                <!-- /.box-body -->
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <textarea class="form-control" rows="5" placeholder="Reminders and Workplan" id="txtAreaReminders" maxlength="1000"></textarea>
                    </div>
                </div>
            </div>
            <!-- /.box sub box level 2 -->
        </div>
        <!-- /.box-body -->
    </div>
    <div class="box-header with-border" id="divMgrReferralsH">
        <h3 class="box-title">
            Referrals
        </h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body" id="divMgrReferralsB">
        <!-- / sub box level 2 -->
        <div class="box box-default box-solid">
            <!-- /.box-header -->
            <div class="box-body">
                <!-- /.box-body -->
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <textarea class="form-control" rows="5" placeholder="Referrals" id="txtAreaReferrals" maxlength="1000"></textarea>
                    </div>
                </div>
            </div>
            <!-- /.box sub box level 2 -->
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box sub box level G1 -->
</div>
<!-- / sub box level G1 -->
<div id="div5" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Next Appointment</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <!-- / sub box level 2 -->
        <div class="box box-default box-solid">
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label requiredFieldColor">
                            * Patient Classification</label>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <select class="form-control select2" data-placeholder="Select" id="ddlPtnClassification">
                        </select>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        &nbsp;</div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group" id="divEDC1">
                        <label for="inputEmail3" class="control-label">
                            Enrolled into Differenciated Care?</label>
                        <input id="chkDifferenciatedCare" name="switch-size" type="checkbox" checked data-size="small"
                            data-on-text="Yes" data-off-text="No">
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group" id="divEDC2">
                        <label for="inputEmail3" class="control-label">
                            ARt Refill Model</label>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group" id="divEDC3">
                        <select class="form-control select2" data-placeholder="Select" id="ddlARTRefillModel">
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        &nbsp;
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        &nbsp;
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Service Area</label>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Purpose</label>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Description</label>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        &nbsp;
                    </div>
                    <%--<div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Signature
                        </label>
                    </div>
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <select class="form-control select2" data-placeholder="Select" id="ddlMgtSignature">
                        </select>
                    </div>--%>
                </div>
                <div class="row">
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label requiredFieldColor">
                            * Next Appointment Date</label>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" class="form-control" id="dtNextAppointmentDate" data-date-format="dd-M-yyyy"
                                style="padding: 13px 12px;" />
                        </div>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input type="text" class="form-control" id="txtServiceArea" placeholder="Service Area"
                            disabled="disabled" />
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <select class="form-control select2" data-placeholder="Select" id="ddlPurpose">
                        </select>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input type="text" class="form-control" id="txtNADDescription" placeholder="Description" />
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                            id="btnAddNextAppointment" onclick="AddAppointment('0');">
                            Add</button>
                    </div>
                    <%--<div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Signature
                        </label>
                    </div>
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <select class="form-control select2" data-placeholder="Select" id="ddlMgtSignature">
                        </select>
                    </div>--%>
                </div>
                <div class="row">
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label requiredFieldColor">
                            * Drug Refill Appointment</label>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <div class="input-group date">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" class="form-control" id="dtDrugRefillAppointmentDate" data-date-format="dd-M-yyyy"
                                style="padding: 13px 12px;" />
                        </div>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input type="text" class="form-control" id="txtDRASA" placeholder="Service Area"
                            disabled="disabled" />
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <select class="form-control select2" data-placeholder="Select" id="ddlDRAPurpose" disabled="disabled">
                        </select>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input type="text" class="form-control" id="txtDRADescription" placeholder="Description" />
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                            id="btnAddDrugRefillAppointment" onclick="AddAppointment('1');">
                            Add</button>
                    </div>
                    <%--<div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Signature
                        </label>
                    </div>
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <select class="form-control select2" data-placeholder="Select" id="ddlMgtSignature">
                        </select>
                    </div>--%>
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <table id="dtlNextAppointment" class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th style="width: 50px;">
                                        #
                                    </th>
                                    <th style="width: 150px;">
                                        Appintment Date
                                    </th>
                                    <th style="width: 100px;">
                                        Service Area
                                    </th>
                                    <th style="width: 100px;">
                                        Reason
                                    </th>
                                    <th style="width: 250px;">
                                        Description
                                    </th>
                                    <th style="width: 100px;">
                                        Status
                                    </th>
                                    <th style="width: 10px;">
                                        Edit
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.box sub box level 2 -->
        </div>
        <!-- /.box-body -->
    </div>
    <div class="box-body">
        <!-- / sub box level 2 -->
        <div class="box box-default box-solid">
            <!-- /.box-header -->
            <div class="box-body">
                <!-- /.box-body -->
                <div class="row">
                    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                        <button type="button" class="btn btn-primary" onclick="OpenCEPopup('<%= ResolveUrl("~/PharmacyDispense/frmPharmacyDispense_PatientOrder.aspx") %>');">
                            Prescription<span class="glyphicon glyphicon-pencil" style="padding-left: 5px;"></span></button>
                    </div>
                    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                        <button type="button" class="btn btn-primary" onclick="OpenCEPopup('<%= ResolveUrl("~/Laboratory/frm_Laboratory.aspx") %>');">
                            Laboratory<span class="glyphicon glyphicon-tags" style="padding-left: 5px;"></span></button>
                    </div>
                </div>
            </div>
            <!-- /.box sub box level 2 -->
        </div>
        <!-- /.box-body -->
    </div>
</div>
<!-- Button Footer -->
<div class="row no-print" align="center">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <button type="button" class="btn btn-primary" onclick="GetPreviousPage('tab_4');">
                Previous<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
            <button type="button" class="btn btn-primary" onclick="SaveManagementx();" runat="server"
                id="btnSaveManagement" clientidmode="Static">
                Save<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
