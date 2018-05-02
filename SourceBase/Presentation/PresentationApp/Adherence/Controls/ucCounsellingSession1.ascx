<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCounsellingSession1.ascx.cs"
    Inherits="PresentationApp.Adherence.Controls.ucCounsellingSession1" %>
<div id="divPage" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Enhance Adherence Counselling Sessions</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
    </div>
    <div class="box-body">
        <div class="box box-default box-solid">
            <div class="box-body">
                <div class="row">
                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Assigned case manager</label>
                            <input id="chkCaseManager" name="switch-size" type="checkbox" checked data-size="small"
                                data-on-text="Yes" data-off-text="No">
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group" id="divCaseManager">
                            <select class="form-control select2" data-placeholder="Select" id="ddlCaseManager"
                                style="width: 100%; float: left;">
                            </select></div>
                    </div>
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Date Assigned</label></div>
                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                            <input type="text" class="form-control" id="dtAssignedDate" placeholder="Date Assigned"
                                data-date-format="dd-M-yyyy" style="width: 60%;">
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Will Directly observed therapy be implemented?
                        </label>
                        <input id="chkDirectlyObservedTherapy" name="switch-size" type="checkbox" checked
                            data-size="small" data-on-text="Yes" data-off-text="No">
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-default box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">
                    Session 1</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                </div>
            </div>
            <div class="box-body">
                <div class="box box-default box-solid">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Adherence % (from pill count)
                                    </label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <input type="number" min="1" max="20" id="txtAdherence" value="None" class="form-control"
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
                                    <input type="text" class="form-control" id="dtFilledDate" placeholder="Date Filled In"
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
                        <div class="row">
                            <div class="box box-default box-solid">
                                <div class="box-header">
                                    <h3 class="box-title">
                                        Viral Load
                                    </h3>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body" id="divViralLoad">
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Discuss Viral Load Results</label>
                                            <input id="chkDiscussViralLoadResult" name="switch-size" type="checkbox" checked
                                                data-size="small" data-on-text="Yes" data-off-text="No">
                                        </div>
                                    </div>
                                    <div class="row" id="divVL1" style="margin: 0.5%;">
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <b>Understanding Viral Load (High/Suppressed): </b>How does the patient feel concerning
                                                the result?
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtAreaViralloadUnderstanding" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                What does the patient think caused the high viral load?
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtAreaHighVL" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <b>Access for Possible Barriers to Adherence</b> <b>Cognitive Barriers: </b>(HIV
                                                and ART knowledge) Assess patient's knowledge about HIV and ART; Correct any misconceptions?
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtAreaCorrectMisconception" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <b>Behavioural Barriers: </b>Let the patient explain how they take their drugs,
                                                and at what time and how they store them?
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtDrugIntake" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                How does treatment fit the patient daily routines? What reminder tools are used?
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtAreaRoutineReminder" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                What does the patient do in case of visits and travel?
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtAreaVisitTravel" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                What does the patient do incase of Side Effects?
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtAreaSideEffect" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                What are the most difficult situations for the patient to take drugs?
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtDifficultSituationDrug" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <b>Emotional Barriers: </b>How does the patient feel about taking drugs evryday?
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtAreaTakingDrugDaily" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                Motivation. What are the patients ambitions in life? What are the 3 most important
                                                things they still want to achieve?
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtAreaMotivation" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="x_content" style="margin: 1%;">
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
                                                            Alcohol Depression Screening<span class="glyphicon glyphicon-pencil" style="padding-left: 5px;"></span></button>
                                                    </div>
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group" style="vertical-align: center;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="divVL2" style="margin: 0.5%;">
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <label for="inputEmail3" class="control-label">
                                                    <b>Social-Economical Barriers: </b>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                Does the patient have any people in their life who they can talk to about HIV Status
                                                and ART?
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <input id="chkSocialEconomicBarrier" name="switch-size" type="checkbox" checked data-size="small"
                                                    data-on-text="Yes" data-off-text="No" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                Discuss how the patient can enlist the support of their family, friends and/or co-workers,
                                                a treatment buddy, community or supports group?
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtAreaDiscussWithFamily" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                Review the patient's and family's sources of income and how well they cover their
                                                needs?
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtAreaFamilyIncome" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                Does the patient have any challenges getting the clinic on regular basis?
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <input id="chkChallengeGettingClinic" name="switch-size" type="checkbox" checked
                                                    data-size="small" data-on-text="Yes" data-off-text="No" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="divChallengeGettingClinic">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtAreaChallengeGettingClinic" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                Is the patient worried about people finding out about their HIV status accidentally?
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <input id="chkHIVStatusBotherDetails" name="switch-size" type="checkbox" checked
                                                    data-size="small" data-on-text="Yes" data-off-text="No" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="divHIVStatusAccidentally">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtHIVStatusBotherDetails" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                Does the patient feel like people treat them differently when they know their HIV
                                                Status?
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <input id="chkPeopleTreatmentDetails" name="switch-size" type="checkbox" checked
                                                    data-size="small" data-on-text="Yes" data-off-text="No" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="divAreaTreatDifferently">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtPeopleTreatmentDetails" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                Does Stigma is making it difficult for them to take their medications on thime,
                                                or for them to attend clinic appointments?
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <input id="chkStigmaInterfering" name="switch-size" type="checkbox" checked data-size="small"
                                                    data-on-text="Yes" data-off-text="No" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="divAreaStigma">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtStigmaInterfering" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                Find out if the patient has tried faith healing, or if they have ever stopped taking
                                                their medicin because of their religious beliefs?
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <input id="chkReligiousBeliefs" name="switch-size" type="checkbox" checked data-size="small"
                                                    data-on-text="Yes" data-off-text="No" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="divFaithhealing">
                                                <textarea class="form-control" cols="1" style="resize: none; width: 90%;" rows="5"
                                                    id="txtReligiousBeliefs" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                <b>Referrals and Networks:</b><br />
                                                Has the patient been referred to other services? (nutrition, psychosocial support
                                                services, substance use treatment, etc)
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <input id="chkReferredOtherServices" name="switch-size" type="checkbox" checked data-size="small"
                                                    data-on-text="Yes" data-off-text="No" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                Did he/she attend the appointments?
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <input id="chkAttendAppointment" name="switch-size" type="checkbox" checked data-size="small"
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
                                                    id="txtAreaExperience" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                Will the patient benefit from a home visit?
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                <input id="chkBenefitHomeVisit" name="switch-size" type="checkbox" checked data-size="small"
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
                                                    id="txtAreaAdherence" maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                <label for="inputEmail3" class="control-label">
                                                    Follow-up Date
                                                </label>
                                            </div>
                                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                <input type="text" class="form-control" id="dtFollowupDate" placeholder="Follow-up Date"
                                                    data-date-format="dd-M-yyyy" style="width: 50%;">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Button Footer -->
<div class="row no-print" align="center">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <button type="button" class="btn btn-primary" onclick="SaveSession1();" runat="server"
                id="btnSaveSession1" clientidmode="Static">
                Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
