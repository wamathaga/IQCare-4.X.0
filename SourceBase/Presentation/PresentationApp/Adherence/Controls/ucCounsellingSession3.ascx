<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCounsellingSession3.ascx.cs"
    Inherits="PresentationApp.Adherence.Controls.ucCounsellingSession3" %>
<div id="divPageGroup1" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Enhance Adherence Counselling Sessions</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
    </div>
</div>
<div id="divPageGroup2" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Session 3</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
    </div>
    <div class="box-body">
        <div class="row">
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                Fill In Session 3?
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <input id="chkCaseManager3" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No" />
            </div>
        </div>
    </div>
    <div class="box box-default box-body">
        <div class="row">
            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Adherence % (from pill count)
                    </label>
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <input type="number" min="1" max="20" id="txtAdherence3" value="None" class="form-control"
                        style="width: 100%; display: inline;">
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label requiredFieldColor">
                        Date filled In
                    </label>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <input type="text" class="form-control" id="dtFilledDate3" placeholder="Date Filled In"
                        data-date-format="dd-M-yyyy" style="width: 100%;">
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <table class="table" style="padding-left: 10px; margin-bottom: 0px;">
                    <tr>
                        <td colspan="2" style="border-top: 0px; padding: 2px;">
                            <div class="box box-default box-solid" style="margin-bottom: 5px;">
                                <div class="box-header">
                                    <h5 class="box-title">
                                        Morisky Medication Adherence Scale (MMAS-4)</h5>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top: 0px; width: 90%">
                            1. Do you forget to take any of your medicine since the last visit?
                        </td>
                        <td style="border-top: 0px; width: 10%">
                            <input id="chkForgotMed3" name="switch-size" type="checkbox" checked data-size="small"
                                data-on-text="Yes" data-off-text="No">
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top: 0px; width: 90%">
                            2. Are you careless at times about taking your medicine?
                        </td>
                        <td style="border-top: 0px; width: 10%">
                            <input id="chkCarelessMed3" name="switch-size" type="checkbox" checked data-size="small"
                                data-on-text="Yes" data-off-text="No">
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top: 0px; width: 90%">
                            3. Sometimes if you feel worse when you take the medicine, do you stop taking it?
                        </td>
                        <td style="border-top: 0px; width: 10%">
                            <input id="chkWorseTakingMed3" name="switch-size" type="checkbox" checked data-size="small"
                                data-on-text="Yes" data-off-text="No">
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top: 0px; width: 90%">
                            4. When you feel better do you sometimes stop taking your medicine?
                        </td>
                        <td style="border-top: 0px; width: 10%">
                            <input id="chkFeelBetterMed3" name="switch-size" type="checkbox" checked data-size="small"
                                data-on-text="Yes" data-off-text="No">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-top: 0px;">
                            <div class="row">
                                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                    (MMAS-4) Score =
                                    <input id="txtMMAS4Score3" type="text" disabled="disabled" class="form-control" value="0"
                                        style="width: 30%; display: inline;">
                                    / 4
                                </div>
                                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                    Adherence Rating:
                                    <input type="text" id="txtMMAS4Rating3" disabled="disabled" value="Good" class="form-control"
                                        style="width: 55%; display: inline;">
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <div id="divOtherQesAdherence2">
                    <table class="table" style="padding-left: 10px;">
                        <tr>
                            <td style="border-top: 0px; width: 90%">
                                5. Did you take your medicine yesterday?
                            </td>
                            <td style="border-top: 0px; width: 10%">
                                <input id="chkYesterdayMed3" name="switch-size" type="checkbox" checked data-size="small"
                                    data-on-text="Yes" data-off-text="No">
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: 0px; width: 90%">
                                6. When you feel like your symptoms are under control, do you sometimes stop taking
                                your medicine?
                            </td>
                            <td style="border-top: 0px; width: 10%">
                                <input id="chkSymptomUnderControl3" name="switch-size" type="checkbox" checked data-size="small"
                                    data-on-text="Yes" data-off-text="No">
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: 0px; width: 90%">
                                7. Taking medication every day is a real inconvenience for some people. Do you ever
                                feel under pressure about sticking to your treatment plan?
                            </td>
                            <td style="border-top: 0px; width: 10%">
                                <input id="chkStickingTreatmentPlan3" name="switch-size" type="checkbox" checked data-size="small"
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
                                        <div id="divMrgRM3">
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
                                        <input id="txtMMAS8Score3" type="text" disabled="disabled" class="form-control" value="0"
                                            style="width: 30%; display: inline;">
                                        / 8
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        Adherence Rating:
                                        <input type="text" id="txtMMAS8Rating3" disabled="disabled" value="None" class="form-control"
                                            style="width: 55%; display: inline;">
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <input type="text" id="txtMMAS8Suggestion3" disabled="disabled" value="None" class="form-control"
                                            style="width: 55%; display: inline;">
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    <b>Review Adherence Plan from previous session</b> Does Patient think Adherence
                    has imporove since last visit?</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkDiscussViralLoadResult3" name="switch-size" type="checkbox" checked
                    data-size="small" data-on-text="Yes" data-off-text="No"></div>
        </div>
        <div class="row">
            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Have any doses been missed</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkSocialEconomicBarrier3" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No"></div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                Review barriers to adherence from the previous session and if strategies identified
                have been taken up Identify other possible gaps and issue emerging (missed pills,
                appointment etc)?
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <textarea class="form-control" rows="5" cols="1" id="txtAreaRoutineReminder3" maxlength="1000"></textarea>
            </div>
        </div>
        <div class="row">
            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                <b>Referrals and Networks:</b><br />
                Has the patient been referred to other services? (nutrition, psychosocial support
                services, substance use treatment, etc)
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkReferredOtherServices3" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                Did he/she attend the appointments?
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkAttendAppointment3" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                What was the experience? Do the referrals need to be re-organized?
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                    id="txtAreaExperience3" maxlength="1000"></textarea>
            </div>
        </div>
        <div class="row">
            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                Will the patient benefit from a home visit?
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkBenefitHomeVisit3" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                Adherence Plan
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                    id="txtAreaAdherence3" maxlength="1000"></textarea>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Follow-up Date
                </label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="dtFollowupDate3" placeholder="Follow-up Date"
                    data-date-format="dd-M-yyyy" style="width: 50%;">
            </div>
        </div>
    </div>
</div>
<!-- Button Footer -->
<div class="row no-print" align="center">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <button type="button" class="btn btn-primary" onclick="GetPreviousPage('tab_2');">
                Previous<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
            <button type="button" class="btn btn-primary" onclick="SaveSession3();"
                runat="server" id="btnSaveCounsellingSession3" clientidmode="Static">
                Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
