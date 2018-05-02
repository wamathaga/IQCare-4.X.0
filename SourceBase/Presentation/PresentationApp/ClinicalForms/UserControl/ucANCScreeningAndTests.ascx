<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucANCScreeningAndTests.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.ucANCScreeningAndTests" %>
<div id="divProfile" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Antenatal Profile</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i id="divTannersI" class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <table id="dtlLabResults" class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th style="width: 40%;">
                                TestName
                            </th>
                            <th style="width: 20%;">
                                Date
                            </th>
                            <th style="width: 30%;">
                                Result
                            </th>
                            <th style="width: 10%;">
                                Order Today
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div class="row">
                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 150px;"
                        id="btnLabResultPopup" onclick="OpenLabResultPopup();">
                        Order Lab</button>
                </div>
                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                </div>
            </div>
        </div>
    </div>
    <!-- /.box sub box level G1 -->
</div>
<div id="div1" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            HIV Testing</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i id="I1" class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="box box-default box-solid" id="divMilestones">
            <div class="box-header with-border">
                <h3 class="box-title">
                    Maternal HIV testing</h3>
                <!-- /.box-tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Client due for HIV Testing?</label>
                                <input id="chkHIVTestingToday" name="switch-size" type="checkbox" checked data-size="small"
                                    data-on-text="Yes" data-off-text="No">
                            </div>
                        </div>
                        <div class="row">
                            <div id="divHIVTesting" class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Previous HIV Status:</label>
                                        <select class="form-control select2" data-placeholder="Select" id="ddlPrevHIVStatus"
                                            style="width: 100%;">
                                        </select>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Previous Point of HIV Testing:</label>
                                        <select class="form-control select2" data-placeholder="Select" id="ddlPrevPointHIVTesting"
                                            style="width: 100%;">
                                        </select>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" id="Label1" class="control-label">
                                            Date of last HIV Test:</label>
                                        <input type="text" class="form-control" id="dtLastHIVTest" placeholder="Start Date"
                                            data-date-format="dd-M-yyyy">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Pre test counselling and testing:</label>
                                        <select class="form-control select2" data-placeholder="Select" id="ddlPreTestCounselling"
                                            style="width: 100%;">
                                        </select>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Post test counselling and testing:</label>
                                        <select class="form-control select2" data-placeholder="Select" id="ddlPosttestcounselling"
                                            style="width: 100%;">
                                        </select>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            HIV Test Done Today:</label>
                                        <select class="form-control select2" data-placeholder="Select" id="ddlHIVTestDoneToday"
                                            style="width: 100%;">
                                        </select>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Final HIV result:</label>
                                        <select class="form-control select2" data-placeholder="Select" id="ddlFinalHIVResult"
                                            style="width: 100%;">
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Client accompanied by partner?</label>
                                <input id="chkPatientaccPartner" name="switch-size" type="checkbox" checked data-size="small"
                                    data-on-text="Yes" data-off-text="No">
                            </div>
                        </div>
                        <div class="row">
                            <div id="divPartnerDetails" class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Pre Test Counselling:</label>
                                        <select class="form-control select2" data-placeholder="Select" id="ddlpartnerPreTestCounselling"
                                            style="width: 100%;">
                                        </select>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            HIV Test done to partner:</label>
                                        <select class="form-control select2" data-placeholder="Select" id="ddlHIVTestdonetopartner"
                                            style="width: 100%;">
                                        </select>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" id="Label2" class="control-label">
                                            Final HIV result for partner:</label>
                                        <select class="form-control select2" data-placeholder="Select" id="ddlPartnerFinalHIVresult"
                                            style="width: 100%;">
                                        </select>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Post Test Counselling:</label>
                                        <select class="form-control select2" data-placeholder="Select" id="ddlPartnerPostTestCounselling"
                                            style="width: 100%;">
                                        </select>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Is the couple discordant?</label>
                                        <input id="chkDiscordantcouple" name="switch-size" type="checkbox" checked data-size="small"
                                            data-on-text="Yes" data-off-text="No">
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Partners DNA PCR result?</label>
                                        <select class="form-control select2" data-placeholder="Select" id="ddlPartnerDNA"
                                            style="width: 100%;">
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
        </div>
    </div>
    <!-- /.box sub box level G1 -->
</div>
<!-- Button Footer -->
<div class="row no-print" align="center">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <button type="button" class="btn btn-primary" onclick="GetPreviousPage('tab_1');">
                Previous<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
            <button type="button" class="btn btn-primary" onclick="SaveScreeningAndTestsData();"
                runat="server" id="btnSaveManagement" clientidmode="Static">
                Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
