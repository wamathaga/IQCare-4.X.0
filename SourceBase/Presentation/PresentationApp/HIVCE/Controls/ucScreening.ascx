<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucScreening.ascx.cs"
    Inherits="HIVCE.Presentation.Controls.ucScreening" %>
<%@ Register Src="ucAlcoholDepressionScreening.ascx" TagName="ucAlcoholDepressionScreening"
    TagPrefix="uc1" %>
<!-- / sub box level G1 -->
<div id="divG1" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            TB Assessment</h3>
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
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="row">
                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                        <label id="lblTBAssessment" for="inputEmail3" class="control-label">
                            TB assessment (ICF):</label>
                        <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                            id="ddlTBAssessment" style="width: 100%;">
                        </select>
                    </div>
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label requiredFieldColor">
                            TB findings:</label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlTbFindings"
                            style="width: 100%;">
                        </select>
                    </div>
                </div>
                <div id="divTBContactScreened" class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="div2">
                        <div class="row">
                            <div class="col-md-4 col-sm-12 col-xs-12 form-group" id="divContactsScreened">
                                <label for="inputEmail3" class="control-label">
                                    Contact Screened for TB</label>
                                <input id="chkTBContactsScreened" name="switch-size" type="checkbox" checked data-size="small"
                                    data-on-text="Yes" data-off-text="No">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    TB Regimen</label>
                                <select class="form-control select2" data-placeholder="Select" id="ddlTBRegimen"
                                    style="width: 100%;">
                                </select>
                            </div>
                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Start Date</label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control" id="dtTBStartDate" style="padding: 13px 12px;"
                                        data-date-format="dd-M-yyyy">
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Rx Duration</label>
                                <select class="form-control select2" data-placeholder="Select" id="ddlRxDuration"
                                    style="width: 100%;">
                                </select>
                            </div>
                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    TB Treatment #</label>
                                <input type="number" class="form-control" id="txtTBTreatment" placeholder="TB Registration No" />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.panel -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box sub box level 2 -->
</div>
<div id="div1IPT" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            IPT</h3>
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
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                Eligible for IPT this Visit
            </div>
            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                <input id="chkEligibleForIPT" name="switch-size" type="checkbox" data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
        </div>
        <div class="row" id="div1">
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            </div>
            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                <div class="callout callout-info" style="background-color: #929594 !important; border-color: #929594 !important;
                    padding: 5px; margin: 0 0 10px 0 !important;">
                    <p style="font-size: 14px;">
                        IPT Client Work Up</p>
                </div>
                <div class="row">
                    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label" style="font-size: 14px !important;">
                            ASK FOR</label>
                        <div class="row">
                            <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Yellow colored urine</label>
                            </div>
                            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                <input id="chkYellowColoredUrine" name="switch-size" type="checkbox" checked data-size="small"
                                    data-on-text="Yes" data-off-text="No">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Numbness/Burning sensation in the hands/feet</label>
                            </div>
                            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                <input id="chkNumbnessBurning" name="switch-size" type="checkbox" checked data-size="small"
                                    data-on-text="Yes" data-off-text="No">
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label" style="font-size: 14px !important;">
                            EXAMINE FOR</label>
                        <div class="row">
                            <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Yellowness of eyes</label>
                            </div>
                            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                <input id="chkYellownessEyes" name="switch-size" type="checkbox" checked data-size="small"
                                    data-on-text="Yes" data-off-text="No">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Tenderness in the right upper quadrant of the abdomen</label>
                            </div>
                            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                <input id="chkTenderness" name="switch-size" type="checkbox" checked data-size="small"
                                    data-on-text="Yes" data-off-text="No">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            </div>
        </div>
        <div class="row" id="divIPTOptions">
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            </div>
            <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                <div id="divIPT">
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                <div id="divIPTStartDate">
                    <label for="inputEmail3" class="control-label">
                        IPT Start Date</label>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control" id="dtIPTStartDate" style="padding: 13px 12px;"
                            data-date-format="dd-M-yyyy">
                    </div>
                </div>
                <div id="divIPTEndDate">
                    <label for="inputEmail3" class="control-label">
                        IPT End Date</label>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control" id="dtIPTEndDate" style="padding: 13px 12px;"
                            data-date-format="dd-M-yyyy">
                    </div>
                </div>
                <div id="divIPTAdherence">
                    <label for="inputEmail3" class="control-label">
                        IPT Adherence</label>
                    <select class="form-control select2" data-placeholder="Select" id="ddlIPTAdherence"
                        style="width: 100%;">
                    </select>
                </div>
                <div id="divIPTContraindication">
                    <label for="inputEmail3" class="control-label">
                        IPT Contraindication</label>
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                            <select class="form-control select2" data-placeholder="Select" id="ddlIPTContraindication"
                                style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                            <input type="text" class="form-control" id="txtIPTContraindication" placeholder="Other Reason"
                                maxlength="150" />
                        </div>
                    </div>
                </div>
                <div id="divIPTDiscontinued">
                    <label for="inputEmail3" class="control-label">
                        IPT Discontinued</label>
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                            <select class="form-control select2" data-placeholder="Select" id="ddlIPTDiscontinued"
                                style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                            <input type="text" class="form-control" id="txtIPTDiscontinued" placeholder="Other Reason"
                                maxlength="150" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box sub box level 2 -->
</div>
<div id="divAvailableTBResults" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Available TB Results</h3>
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
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <table id="dtlPresentingComplaints" class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th style="width: 40%;">
                                Test
                            </th>
                            <th style="width: 30%;">
                                Result
                            </th>
                            <th style="width: 30%;">
                                Date
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                Sputum smear
                            </td>
                            <td>
                                <select class="form-control select2" data-placeholder="Select" id="ddlSputumSmear"
                                    style="width: 100%;">
                                </select>
                            </td>
                            <td>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control" id="dtSputumSmear" style="padding: 13px 12px;"
                                        data-date-format="dd-M-yyyy">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                GeneExpert:
                            </td>
                            <td>
                                <select class="form-control select2" data-placeholder="Select" id="ddlGeneExpert"
                                    style="width: 100%;">
                                </select>
                            </td>
                            <td>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control" id="dtGeneExpert" style="padding: 13px 12px;"
                                        data-date-format="dd-M-yyyy">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sputum for DST:
                            </td>
                            <td>
                                <select class="form-control select2" data-placeholder="Select" id="ddlSputumDST"
                                    style="width: 100%;">
                                </select>
                            </td>
                            <td>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control" id="dtSputumDST" style="padding: 13px 12px;"
                                        data-date-format="dd-M-yyyy">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Chest X-Ray:
                            </td>
                            <td>
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                        <label style="margin-right: 20px;">
                                            <input type="radio" id="Radio7" name="rdoChestXray" class="flat-red" value="1" />
                                            Yes
                                        </label>
                                        <label style="margin-right: 20px;">
                                            <input type="radio" id="Radio8" name="rdoChestXray" class="flat-red" value="0" />
                                            No
                                        </label>
                                    </div>
                                </div>
                                <div id="divCXRResults" class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            CXR results</label>
                                        <select class="form-control select2" data-placeholder="Select" id="ddlCXRResults"
                                            style="width: 100%;">
                                        </select>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control" id="dtChestXray" style="padding: 13px 12px;"
                                        data-date-format="dd-M-yyyy">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tissue Biopsy:
                            </td>
                            <td>
                                <label style="margin-right: 20px;">
                                    <input type="radio" id="Radio9" name="rdoTissueBiopsy" class="flat-red" value="1" />
                                    Yes
                                </label>
                                <label style="margin-right: 20px;">
                                    <input type="radio" id="Radio10" name="rdoTissueBiopsy" class="flat-red" value="0" />
                                    No
                                </label>
                            </td>
                            <td>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control" id="dtTissueBiopsy" style="padding: 13px 12px;"
                                        data-date-format="dd-M-yyyy">
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box sub box level 2 -->
</div>
<uc1:ucAlcoholDepressionScreening ID="ucAlcoholDepressionScreening" runat="server" />
<!-- Button Footer -->
<div class="row no-print" align="center">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <button type="button" class="btn btn-primary" onclick="GetPreviousPage('tab_3');">
                Previous<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
            <button type="button" class="btn btn-primary" onclick="SaveScreeningData();" runat="server"
                id="btnSaveScreening" clientidmode="Static">
                Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
