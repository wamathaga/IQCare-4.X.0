<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAlcoholDepressionScreening.ascx.cs"
    Inherits="PresentationApp.HIVCE.Controls.ucAlcoholDepressionScreening" %>
<!-- / sub box level 1 -->
<div id="divG1" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Depression Screening:</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <table class="table" style="width: 60%;">
            <tr>
                <td colspan="2" style="border-top: 0px;">
                    During the past two weeks have you often been bothered by :
                </td>
            </tr>
            <tr>
                <td style="border-top: 0px;">
                    - Feeling down, depressed, or hopeless?
                </td>
                <td style="border-top: 0px;">
                    <p>
                        <input id="chkFeelingDown" name="chkFeelingDown" type="checkbox" data-size="small"
                            data-on-text="Yes" data-off-text="No">
                    </p>
                </td>
            </tr>
            <tr>
                <td style="border-top: 0px;">
                    - Little interest or pleasure in doing things?
                </td>
                <td style="border-top: 0px;">
                    <p>
                        <input id="chkLittleInterest" name="switch-size" type="checkbox" data-size="small"
                            data-on-text="Yes" data-off-text="No">
                    </p>
                </td>
            </tr>
        </table>
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="borderedTable" class="body collapse in">
                <table class="table table-bordered responsive-table">
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <h4 class="box-title">
                                Patient Health Questionnaire (PHQ-9)</h4>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; background-color: rgba(128, 128, 128, 0.42);">
                            Over the last two weeks, how often have you been bothered by any of the following
                            problems?
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; text-align: right;">
                            Little interest or pleasure in doing things?
                        </td>
                        <td>
                            <div id="divPHQLI" class="btn-group-vertical" data-toggle="buttons">
                                <label class="btn btn-default" id="0" onclick="CalculatePHQ9('PHQLI','0');">
                                    <input type="radio" name="options" id="LP0" value="0">
                                    Not at All
                                </label>
                                <label class="btn btn-default" id="1" onclick="CalculatePHQ9('PHQLI','1');">
                                    <input type="radio" name="options" id="LP1" value="1">
                                    Several Days
                                </label>
                                <label class="btn btn-default" id="2" onclick="CalculatePHQ9('PHQLI','2');">
                                    <input type="radio" name="options" id="LP2" value="2">
                                    More than half the days
                                </label>
                                <label class="btn btn-default" id="3" onclick="CalculatePHQ9('PHQLI','3');">
                                    <input type="radio" name="options" id="LP3" value="3">Nearly every day
                                </label>
                            </div>
                        </td>
                        <%--</tr>
                                                                                <tr>--%>
                        <td style="vertical-align: middle; text-align: right;">
                            Feeling down, depressed, or hopeless?
                        </td>
                        <td>
                            <div id="divPHQFD" class="btn-group-vertical" data-toggle="buttons">
                                <label class="btn btn-default" id="0" onclick="CalculatePHQ9('PHQFD','0');">
                                    <input type="radio" name="options" id="Radio8">
                                    Not at All
                                </label>
                                <label class="btn btn-default" id="1" onclick="CalculatePHQ9('PHQFD','1');">
                                    <input type="radio" name="options" id="Radio9">
                                    Several Days
                                </label>
                                <label class="btn btn-default" id="2" onclick="CalculatePHQ9('PHQFD','2');">
                                    <input type="radio" name="options" id="Radio10">
                                    More than half the days
                                </label>
                                <label class="btn btn-default" id="3" onclick="CalculatePHQ9('PHQFD','3');">
                                    <input type="radio" name="options" id="Radio11">Nearly every day
                                </label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; text-align: right;">
                            Trouble falling or staying asleep, or sleeping too much?
                        </td>
                        <td>
                            <div id="divPHQTS" class="btn-group-vertical" data-toggle="buttons">
                                <label class="btn btn-default" id="1" onclick="CalculatePHQ9('PHQTS','0');">
                                    <input type="radio" name="options" id="Radio12">
                                    Not at All
                                </label>
                                <label class="btn btn-default" id="1" onclick="CalculatePHQ9('PHQTS','1');">
                                    <input type="radio" name="options" id="Radio13">
                                    Several Days
                                </label>
                                <label class="btn btn-default" id="2" onclick="CalculatePHQ9('PHQTS','2');">
                                    <input type="radio" name="options" id="Radio14">
                                    More than half the days
                                </label>
                                <label class="btn btn-default" id="3" onclick="CalculatePHQ9('PHQTS','3');">
                                    <input type="radio" name="options" id="Radio15">Nearly every day
                                </label>
                            </div>
                        </td>
                        <%-- </tr>
                                                                                <tr>--%>
                        <td style="vertical-align: middle; text-align: right;">
                            Feeling tired or having little energy?
                        </td>
                        <td>
                            <div id="divPHQTLE" class="btn-group-vertical" data-toggle="buttons">
                                <label class="btn btn-default" id="0" onclick="CalculatePHQ9('PHQTLE','0');">
                                    <input type="radio" name="options" id="Radio16">
                                    Not at All
                                </label>
                                <label class="btn btn-default" id="1" onclick="CalculatePHQ9('PHQTLE','1');">
                                    <input type="radio" name="options" id="Radio17">
                                    Several Days
                                </label>
                                <label class="btn btn-default" id="2" onclick="CalculatePHQ9('PHQTLE','2');">
                                    <input type="radio" name="options" id="Radio18">
                                    More than half the days
                                </label>
                                <label class="btn btn-default" id="3" onclick="CalculatePHQ9('PHQTLE','3');">
                                    <input type="radio" name="options" id="Radio19">Nearly every day
                                </label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; text-align: right;">
                            Poor appetite or overeating?
                        </td>
                        <td>
                            <div id="divPHQA" class="btn-group-vertical" data-toggle="buttons">
                                <label class="btn btn-default" id="0" onclick="CalculatePHQ9('PHQA','0');">
                                    <input type="radio" name="options" id="Radio20">
                                    Not at All
                                </label>
                                <label class="btn btn-default" id="1" onclick="CalculatePHQ9('PHQA','1');">
                                    <input type="radio" name="options" id="Radio21">
                                    Several Days
                                </label>
                                <label class="btn btn-default" id="2" onclick="CalculatePHQ9('PHQA','2');">
                                    <input type="radio" name="options" id="Radio22">
                                    More than half the days
                                </label>
                                <label class="btn btn-default" id="3" onclick="CalculatePHQ9('PHQA','3');">
                                    <input type="radio" name="options" id="Radio23">Nearly every day
                                </label>
                            </div>
                        </td>
                        <%--</tr>
                                                                                <tr>--%>
                        <td style="vertical-align: middle; text-align: right;">
                            Feeling bad about yourself - or that you are a failure or have let yourself or your
                            family down?
                        </td>
                        <td>
                            <div id="divPHQYD" class="btn-group-vertical" data-toggle="buttons">
                                <label class="btn btn-default" id="0" onclick="CalculatePHQ9('PHQYD','0');">
                                    <input type="radio" name="options" id="Radio24">
                                    Not at All
                                </label>
                                <label class="btn btn-default" id="1" onclick="CalculatePHQ9('PHQYD','1');">
                                    <input type="radio" name="options" id="Radio25">
                                    Several Days
                                </label>
                                <label class="btn btn-default" id="2" onclick="CalculatePHQ9('PHQYD','2');">
                                    <input type="radio" name="options" id="Radio26">
                                    More than half the days
                                </label>
                                <label class="btn btn-default" id="3" onclick="CalculatePHQ9('PHQYD','3');">
                                    <input type="radio" name="options" id="Radio27">Nearly every day
                                </label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; text-align: right;">
                            Trouble concentrating on things, such as reading the newspaper or watching television?
                        </td>
                        <td>
                            <div id="divPHQTC" class="btn-group-vertical" data-toggle="buttons">
                                <label class="btn btn-default" id="0" onclick="CalculatePHQ9('PHQTC','0');">
                                    <input type="radio" name="options" id="Radio28">
                                    Not at All
                                </label>
                                <label class="btn btn-default" id="1" onclick="CalculatePHQ9('PHQTC','1');">
                                    <input type="radio" name="options" id="Radio29">
                                    Several Days
                                </label>
                                <label class="btn btn-default" id="2" onclick="CalculatePHQ9('PHQTC','2');">
                                    <input type="radio" name="options" id="Radio30">
                                    More than half the days
                                </label>
                                <label class="btn btn-default" id="3" onclick="CalculatePHQ9('PHQTC','3');">
                                    <input type="radio" name="options" id="Radio31">Nearly every day
                                </label>
                            </div>
                        </td>
                        <%-- </tr>
                                                                                <tr>--%>
                        <td style="vertical-align: middle; text-align: right;">
                            Moving or speaking so slowly that other people could have noticed?
                        </td>
                        <td>
                            <div id="divPHQMS" class="btn-group-vertical" data-toggle="buttons">
                                <label class="btn btn-default" id="0" onclick="CalculatePHQ9('PHQMS','0');">
                                    <input type="radio" name="options" id="Radio32">
                                    Not at All
                                </label>
                                <label class="btn btn-default" id="1" onclick="CalculatePHQ9('PHQMS','1');">
                                    <input type="radio" name="options" id="Radio33">
                                    Several Days
                                </label>
                                <label class="btn btn-default" id="2" onclick="CalculatePHQ9('PHQMS','2');">
                                    <input type="radio" name="options" id="Radio34">
                                    More than half the days
                                </label>
                                <label class="btn btn-default" id="3" onclick="CalculatePHQ9('PHQMS','3');">
                                    <input type="radio" name="options" id="Radio35">Nearly every day
                                </label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; text-align: right;">
                            Or the opposite - being so fidgety or restless that you have been moving around
                            a lot more than usual?
                        </td>
                        <td>
                            <div id="divPHQFR" class="btn-group-vertical" data-toggle="buttons">
                                <label class="btn btn-default" id="0" onclick="CalculatePHQ9('PHQFR','0');">
                                    <input type="radio" name="options" id="Radio36">
                                    Not at All
                                </label>
                                <label class="btn btn-default" id="1" onclick="CalculatePHQ9('PHQFR','1');">
                                    <input type="radio" name="options" id="Radio37">
                                    Several Days
                                </label>
                                <label class="btn btn-default" id="2" onclick="CalculatePHQ9('PHQFR','2');">
                                    <input type="radio" name="options" id="Radio38">
                                    More than half the days
                                </label>
                                <label class="btn btn-default" id="3" onclick="CalculatePHQ9('PHQFR','3');">
                                    <input type="radio" name="options" id="Radio39">Nearly every day
                                </label>
                            </div>
                        </td>
                        <%--</tr>
                                                                                <tr>--%>
                        <td style="vertical-align: middle; text-align: right;">
                            Thoughts that you would be better off dead, or of hurting yourself in some way?
                        </td>
                        <td>
                            <div id="divPHQHY" class="btn-group-vertical" data-toggle="buttons">
                                <label class="btn btn-default" id="0" onclick="CalculatePHQ9('PHQHY','0');">
                                    <input type="radio" name="options" id="Radio40">
                                    Not at All
                                </label>
                                <label class="btn btn-default" id="1" onclick="CalculatePHQ9('PHQHY','1');">
                                    <input type="radio" name="options" id="Radio41">
                                    Several Days
                                </label>
                                <label class="btn btn-default" id="2" onclick="CalculatePHQ9('PHQHY','2');">
                                    <input type="radio" name="options" id="Radio42">
                                    More than half the days
                                </label>
                                <label class="btn btn-default" id="3" onclick="CalculatePHQ9('PHQHY','3');">
                                    <input type="radio" name="options" id="Radio43">Nearly every day
                                </label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="x_content">
                                <div class="row">
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        Total =
                                        <input id="PHQ9Total" type="text" disabled="disabled" class="form-control" value="0"
                                            style="width: 30%; display: inline;">
                                        / 30
                                    </div>
                                    <div class="col-md-10 col-sm-12 col-xs-12 form-group">
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
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; background-color: rgba(128, 128, 128, 0.42);">
                            Depression Severity: 0-4 none, 5-9 mild, 10-14 moderate, 15-19 moderately severe,
                            20-27 severe.
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <!-- /.box-body -->
</div>
<!-- /.box sub box level 1 -->
<!-- / sub box level 1 -->
<div id="divSGBV" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Sexual and Gender Base Violence:</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <table class="table" style="padding-left: 10px; width: 80%;">
            <tr>
                <td colspan="2" style="border-top: 0px;">
                    Many people do not realize that violence can lead to all kinds of health problems.
                    Because violence is so common in many women’s lives, and because there is help available
                    for women being abused, we now ask all female patients about their experiences with
                    violence.
                </td>
            </tr>
            <tr>
                <td style="border-top: 0px; width: 90%">
                    1. Within the past year, have you been hit, slapped, kicked, or physically hurt
                    by someone in any way?
                </td>
                <td style="border-top: 0px; width: 10%">
                    <input id="chkSGBV1" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No">
                </td>
            </tr>
            <tr>
                <td style="border-top: 0px; width: 90%">
                    2. Are you in a relationship with a person who physically hurts you?
                </td>
                <td style="border-top: 0px; width: 10%">
                    <input id="chkSGBV2" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No">
                </td>
            </tr>
            <tr>
                <td style="border-top: 0px; width: 90%">
                    3. Are you in a relationship with a person who threatens, frightens, or insults
                    you, or treats you badly?
                </td>
                <td style="border-top: 0px; width: 10%">
                    <input id="chkSGBV3" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No">
                </td>
            </tr>
            <tr>
                <td style="border-top: 0px; width: 90%">
                    4. Are you in a relationship with a person who forces you to participate in sexual
                    activities that make you feel uncomfortable?
                </td>
                <td style="border-top: 0px; width: 10%">
                    <input id="chkSGBV4" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No">
                </td>
            </tr>
            <tr>
                <td style="border-top: 0px; width: 90%">
                    5. Have you ever experienced any of the above with someone you do not have a relationship
                    with?
                </td>
                <td style="border-top: 0px; width: 10%">
                    <input id="chkSGBV5" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No">
                </td>
            </tr>
        </table>
    </div>
    <!-- /.box-body -->
</div>
<!-- /.box sub box level 1 -->
<!-- / sub box level 1 -->
<div id="divG3Disclosure" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Disclosure:</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Disclosed HIV Status?</label>
                <input id="chkDisclosedHIVStatus" name="switch-size" type="checkbox" data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
            <div id="divDisclosureStatus" class="col-md-5 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label requiredFieldColor">
                    Disclosure status</label>
                <select class="form-control select2" data-placeholder="Select" id="ddlDisclosureStatus"
                    style="width: 100%;">
                </select>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Disclosed To :</label>
                <select class="form-control select2" data-placeholder="Select" id="ddlDisclosedTo"
                    style="width: 100%;">
                </select>
            </div>
        </div>
    </div>
    <!-- /.box-body -->
</div>
<!-- /.box sub box level 1 -->
<!-- / sub box level 1 -->
<div id="divCRAFFTAlcoholDrugAbuse" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            CRAFFT Alcohol and drug abuse screening in adolescents:</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    During the past 12 months, did you</label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    drink any alcohol (more than a few sips)?</label>
            </div>
            <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                <input id="chkDrinkAnyAlcohol" name="switch-size" type="checkbox" data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    did you smoke any marijuana?</label>
            </div>
            <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                <input id="chkSmokeMarijuana" name="switch-size" type="checkbox" data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    did you use anything else to get high?
                </label>
                <br />
                “anything else” includes illegal drugs, over the counter and prescription drugs,
                and things that you sniff or “huff”
            </div>
            <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                <input id="chkUsageGetHigh" name="switch-size" type="checkbox" data-size="small"
                    data-on-text="Yes" data-off-text="No"><br />
            </div>
        </div>
        <div class="row">
            <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    C &nbsp; 1.Have you ever ridden in a CAR driven by someone (including yourself)
                    who was “high” or had been using alcohol or drugs?
                </label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <input id="chkRiddenCarWhenHigh" name="switch-size" type="checkbox" data-size="small"
                    data-on-text="Yes" data-off-text="No"><br />
            </div>
        </div>
        <div class="row" id="divCRAFFTOptions">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="row">
                    <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            R &nbsp; 2. Do you ever use alcohol or drugs to RELAX, feel better about yourself,
                            or fit in?
                        </label>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="chkRelaxOption" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes"
                            data-off-text="No"><br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            A &nbsp; 3. Do you ever use alcohol or drugs while you are by yourself, or ALONE?
                        </label>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="chkAloneOption" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes"
                            data-off-text="No"><br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            F &nbsp; 4. Do you ever FORGET things you did while using alcohol or drugs?
                        </label>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="chkForgotOption" name="switch-size" type="checkbox" data-size="small"
                            data-on-text="Yes" data-off-text="No"><br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            F &nbsp; 5. Do your FAMILY or FRIENDS ever tell you that you should cut down on
                            your drinking or drug use?
                        </label>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="chkCutDown" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes"
                            data-off-text="No"><br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            T &nbsp; 6. Have you ever gotten into TROUBLE while you were using alcohol or drugs?
                        </label>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="chkGotIntoTrouble" name="switch-size" type="checkbox" data-size="small"
                            data-on-text="Yes" data-off-text="No"><br />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                CRAFFT Score =
                <input id="txtCRAFFTScore" type="text" disabled="disabled" class="form-control" value="0"
                    style="width: 30%; display: inline;">
                / 6
            </div>
            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                Risk:
                <input type="text" id="txtCRAFFTRisk" disabled="disabled" value="None" class="form-control"
                    style="width: 55%; display: inline;">
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box sub box level 1 -->
</div>
<!-- / sub box level 1 -->
<div id="divCAGEAIDScreening" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            CAGE-AID Screening for Alcohol and Drug Use Disorders for Adults:</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    How often do you have a drink containing alcohol?</label>
                <div id="divDCA">
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="rbDCA" name="rbDCA" class="flat-red" value="1" />
                            <label for="minimal-radio-1">
                                Never</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Checkbox1" name="rbDCA" class="flat-red" value="2" />
                            <label for="minimal-radio-1">
                                Monthly or less</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Checkbox2" name="rbDCA" class="flat-red" value="3" />
                            <label for="minimal-radio-1">
                                2-4 times a month</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Checkbox3" name="rbDCA" class="flat-red" value="4" />
                            <label for="minimal-radio-1">
                                2-3 times a week</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Checkbox4" name="rbDCA" class="flat-red" value="5" />
                            <label for="minimal-radio-1">
                                4 or more times a week</label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    How often do you use drugs?</label>
                <div id="divUDrugs">
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Radio1" name="rbUDrugs" class="flat-red" value="1" />
                            <label for="minimal-radio-1">
                                Never</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Radio2" name="rbUDrugs" class="flat-red" value="2" />
                            <label for="minimal-radio-1">
                                Monthly or less</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Radio3" name="rbUDrugs" class="flat-red" value="3" />
                            <label for="minimal-radio-1">
                                2-4 times a month</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Radio4" name="rbUDrugs" class="flat-red" value="4" />
                            <label for="minimal-radio-1">
                                2-3 times a week</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Radio5" name="rbUDrugs" class="flat-red" value="5" />
                            <label for="minimal-radio-1">
                                4 or more times a week</label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    How often do you smoke?</label>
                <div id="divSmoke">
                    <div class="row">
                        <div class="col-md-10 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Radio6" name="rbSmoke" class="flat-red" value="1" />
                            <label for="minimal-radio-1">
                                Never smoked</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Radio7" name="rbSmoke" class="flat-red" value="2" />
                            <label for="minimal-radio-1">
                                Former smoker
                            </label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Radio44" name="rbSmoke" class="flat-red" value="3" />
                            <label for="minimal-radio-1">
                                Current some day smoker
                            </label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Radio45" name="rbSmoke" class="flat-red" value="4" />
                            <label for="minimal-radio-1">
                                Light tobacco smoker (<10 per day)</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Radio46" name="rbSmoke" class="flat-red" value="5" />
                            <label for="minimal-radio-1">
                                Heavy tobacco smoker (Over 10 per day)</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10 col-sm-12 col-xs-12 form-group" style="margin-bottom: 5px;">
                            <input type="radio" id="Radio47" name="rbSmoke" class="flat-red" value="6" />
                            <label for="minimal-radio-1">
                                Smoker, current status unknown
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="row">
                    <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            In the last three months
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            1. Have you felt you should cut down on your drinking or drug use?
                        </label>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="chk3MonOpt1" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes"
                            data-off-text="No"><br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            2. Have people ever annoyed you by criticizing your drinking or drug use?
                        </label>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="chk3MonOpt2" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes"
                            data-off-text="No"><br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            3. Have you ever felt bad or guilty about your drinking or drug use?
                        </label>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="chk3MonOpt3" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes"
                            data-off-text="No"><br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            4. Have you ever had a drink or used drugs first thing in the morning to steady
                            your nerves or to get rid of a hangover?
                        </label>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <input id="chk3MonOpt4" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes"
                            data-off-text="No"><br />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                CAGE-AID Score =
                <input id="txtCAGEAIDScore" type="text" disabled="disabled" class="form-control"
                    value="0" style="width: 30%; display: inline;">
                / 4
            </div>
            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                Risk:
                <input type="text" id="txtCAGEAID" disabled="disabled" value="None" class="form-control"
                    style="width: 55%; display: inline;">
            </div>
        </div>
        <div class="row" id="divTriedStopSmoking">
            <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    During the past 12 months, have you tried to stop smoking?
                </label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <input id="chkTriedStopSmoking" name="switch-size" type="checkbox" data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
        </div>
    </div>
    <!-- /.box-body -->
</div>
<div id="div3" class="box box-default box-solid">
    <div class="box-body">
        <!-- /.box sub box level 1 -->
        Notes:
        <textarea class="form-control" rows="3" placeholder="Notes" id="txtNotes" maxlength="1000"></textarea>
    </div>
</div>
