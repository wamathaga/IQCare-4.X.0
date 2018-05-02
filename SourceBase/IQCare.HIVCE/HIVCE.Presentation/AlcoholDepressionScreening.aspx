<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.Master" CodeBehind="AlcoholDepressionScreening.aspx.cs"
    Inherits="HIVCE.Presentation.AlcoholDepressionScreening" %>

<asp:Content ID="Data" ContentPlaceHolderID="SiteContent" runat="Server">
    <script src="<%=ResolveUrl("Scripts/AlcoholDepressionScreening.js") %>" type="text/javascript"></script>
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
                            <h3 class="box-title">Alcohol Depression Screening</h3>
                            <!-- /.box-tools -->
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                        <!-- / sub box level 1 -->
                        <div id="divG1" class="box box-default box-solid">
                            <div class="box-header with-border">
                                    <h3 class="box-title">Depression Screening:</h3>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                    <!-- /.box-tools -->
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <table class="table" style="width:60%;">    
                                   <tr>
                                        <td colspan="2" style="border-top: 0px;">
                                            During the past two weeks have you often been bothered by 
                                        </td>
                                        </tr>
                                        <tr>
                                        <td style="border-top: 0px;">
                                           Feeling down, depressed, or hopeless? 
                                        </td>
                                        <td style="border-top: 0px;">
                                                             <p>
                                        <input id="chkFeelingDown" name="chkFeelingDown" type="checkbox" checked data-size="small"
                                         data-on-text="Yes" 
                                         data-off-text="No" >
                                    </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-top: 0px;">
                                          Little interest or pleasure in doing things?
                                        </td>
                                        <td  style="border-top: 0px;">
                                                             <p>
                                        <input id="chkLittleInterest" name="switch-size" type="checkbox" checked data-size="small"
                                         data-on-text="Yes" 
                                         data-off-text="No" >
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
                                                    <div class="btn-group-vertical" data-toggle="buttons">
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio4">
                                                            Not at All
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio5">
                                                            Several Days
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio6">
                                                            More than half the days
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio7">Nearly every day
                                                        </label>
                                                    </div>
                                                </td>
                                                <%--</tr>
                                                                                <tr>--%>
                                                <td style="vertical-align: middle; text-align: right;">
                                                    Feeling down, depressed, or hopeless?
                                                </td>
                                                <td>
                                                    <div class="btn-group-vertical" data-toggle="buttons">
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio8">
                                                            Not at All
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio9">
                                                            Several Days
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio10">
                                                            More than half the days
                                                        </label>
                                                        <label class="btn btn-default">
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
                                                    <div class="btn-group-vertical" data-toggle="buttons">
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio12">
                                                            Not at All
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio13">
                                                            Several Days
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio14">
                                                            More than half the days
                                                        </label>
                                                        <label class="btn btn-default">
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
                                                    <div class="btn-group-vertical" data-toggle="buttons">
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio16">
                                                            Not at All
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio17">
                                                            Several Days
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio18">
                                                            More than half the days
                                                        </label>
                                                        <label class="btn btn-default">
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
                                                    <div class="btn-group-vertical" data-toggle="buttons">
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio20">
                                                            Not at All
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio21">
                                                            Several Days
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio22">
                                                            More than half the days
                                                        </label>
                                                        <label class="btn btn-default">
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
                                                    <div class="btn-group-vertical" data-toggle="buttons">
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio24">
                                                            Not at All
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio25">
                                                            Several Days
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio26">
                                                            More than half the days
                                                        </label>
                                                        <label class="btn btn-default">
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
                                                    <div class="btn-group-vertical" data-toggle="buttons">
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio28">
                                                            Not at All
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio29">
                                                            Several Days
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio30">
                                                            More than half the days
                                                        </label>
                                                        <label class="btn btn-default">
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
                                                    <div class="btn-group-vertical" data-toggle="buttons">
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio32">
                                                            Not at All
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio33">
                                                            Several Days
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio34">
                                                            More than half the days
                                                        </label>
                                                        <label class="btn btn-default">
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
                                                    <div class="btn-group-vertical" data-toggle="buttons">
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio36">
                                                            Not at All
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio37">
                                                            Several Days
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio38">
                                                            More than half the days
                                                        </label>
                                                        <label class="btn btn-default">
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
                                                    <div class="btn-group-vertical" data-toggle="buttons">
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio40">
                                                            Not at All
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio41">
                                                            Several Days
                                                        </label>
                                                        <label class="btn btn-default">
                                                            <input type="radio" name="options" id="Radio42">
                                                            More than half the days
                                                        </label>
                                                        <label class="btn btn-default">
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
                                                                <input type="text" disabled="disabled" class="form-control" value="15" style="width: 22%;
                                                                    display: inline;">
                                                                / 27
                                                            </div>
                                                            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                                                Depression Severity:
                                                                <input type="text" disabled="disabled" value="Moderately severe depression" class="form-control"
                                                                    style="width: 55%; display: inline;">
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
                        <div id="divG2" class="box box-default box-solid">
                            <div class="box-header with-border">
                                    <h3 class="box-title">Sexual and Gender Base Violence:</h3>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
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
                                            <input id="Checkbox2" name="switch-size" type="checkbox" checked data-size="small"
                                                data-on-text="Yes" data-off-text="No">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-top: 0px; width: 90%">
                                            2. Are you in a relationship with a person who physically hurts you?
                                        </td>
                                        <td style="border-top: 0px; width: 10%">
                                            <input id="Checkbox3" name="switch-size" type="checkbox" checked data-size="small"
                                                data-on-text="Yes" data-off-text="No">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-top: 0px; width: 90%">
                                            3. Are you in a relationship with a person who threatens, frightens, or insults
                                            you, or treats you badly?
                                        </td>
                                        <td style="border-top: 0px; width: 10%">
                                            <input id="Checkbox4" name="switch-size" type="checkbox" checked data-size="small"
                                                data-on-text="Yes" data-off-text="No">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-top: 0px; width: 90%">
                                            4. Are you in a relationship with a person who forces you to participate in sexual
                                            activities that make you feel uncomfortable?
                                        </td>
                                        <td style="border-top: 0px; width: 10%">
                                            <input id="Checkbox5" name="switch-size" type="checkbox" checked data-size="small"
                                                data-on-text="Yes" data-off-text="No">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-top: 0px; width: 90%">
                                            5. Have you ever experienced any of the above with someone you do not have a relationship
                                            with?
                                        </td>
                                        <td style="border-top: 0px; width: 10%">
                                            <input id="Checkbox6" name="switch-size" type="checkbox" checked data-size="small"
                                                data-on-text="Yes" data-off-text="No">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- /.box-body -->;
                        </div>
                        <!-- /.box sub box level 1 -->
                        <!-- / sub box level 1 -->
                        <div id="divG3" class="box box-default box-solid">
                            <div class="box-header with-border">
                                    <h3 class="box-title">Sexual and Gender Base Violence:</h3>
                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                    <!-- /.box-tools -->
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                            </div>
                            <!-- /.box-body -->;
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
