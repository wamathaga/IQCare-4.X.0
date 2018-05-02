<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucHIVCETriage.ascx.cs"
    Inherits="HIVCE.Presentation.Controls.ucHIVCETriage" %>
<script type="text/javascript">
   
</script>
<div class="row">
    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
        <label for="inputEmail3" class="control-label">
            *Visit Date</label>
    </div>
    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
        <div class="input-group date">
            <div class="input-group-addon">
                <i class="fa fa-calendar"></i>
            </div>
            <input type="text" class="form-control" id="dtVisitDate" data-date-format="dd-M-yyyy"
                style="padding: 13px 12px;">
        </div>
    </div>
    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
        (MM-DD-YYYY)
    </div>
    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
        *Scheduled?:
    </div>
    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
        <div class="form-group">
            <label>
                <input type="checkbox" class="minimal" id="chkScheduled" checked>
            </label>
        </div>
    </div>
    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
        *Visit Type:
    </div>
    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
        <select class="form-control select2" data-placeholder="Select" id="ddlVisitType"
            style="width: 100%;">
        </select>
    </div>
</div>
<div class="row">
    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
        <label for="inputEmail3" class="control-label">
            *Duration since ART start</label>
    </div>
    <div class="col-md-10 col-sm-12 col-xs-12 form-group">
        <input type="text" id="txtARTStart" disabled="disabled" value="" class="form-control"
            style="width: 5%; display: inline;">
        Months
    </div>
</div>
<!-- Visit Details -->
<div class="box box-primary">
    <div class="box-header with-border">
        <h3 class="box-title">
            Visit Details</h3>
        <%--<div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>--%>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Child Accompanied by Care Giver?</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkCareGiver" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Childs relationship to care giver</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <select class="form-control select2" data-placeholder="Select" id="ddlCareGiven"
                    style="width: 100%;">
                </select>
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Patient is part of a support group?</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkSupportGroup" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Name of Support Group</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtSupportGroupName" placeholder="Name of Support Group">
            </div>
            <!-- /.row -->
        </div>
    </div>
    <!-- /.box-body -->
</div>
<!-- HIV Care -->
<div class="box box-primary">
    <div class="box-header with-border">
        <h3 class="box-title">
            HIV Care</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    *Date Confirmed HIV Positive:</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                    </div>
                    <input type="text" class="form-control" id="dtHivConfirm" style="padding: 13px 12px;"
                        data-date-format="dd-M-yyyy">
                </div>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Where:</label>
            </div>
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtHIVPositiveWhere" placeholder="Where..">
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    *Date Enrolled in HIV Care:</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                    </div>
                    <input type="text" class="form-control" id="dtHivEnroll" style="padding: 13px 12px;"
                        data-date-format="dd-M-yyyy">
                </div>
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    *Transfer In</label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <input id="chkTransferIn" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            </div>
            <div class="col-md-11 col-sm-12 col-xs-12 form-group">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Transfer in date</label>
                            </div>
                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control" id="dtTransferDate" style="padding: 13px 12px;">
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    From District/County</label>
                            </div>
                            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                <select class="form-control select2" data-placeholder="From District" id="ddlDistrict"
                                    style="width: 100%;">
                                </select>
                            </div>
                        </div>
                        <!-- /.row -->
                        <div class="row">
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    From Facility</label>
                            </div>
                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                <select class="form-control select2" data-placeholder="From Facility" id="ddlFacility"
                                    style="width: 100%;">
                                </select>
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Date Started ART</label>
                            </div>
                            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control" id="dtDateStatedArt" style="padding: 13px 12px;"
                                        data-date-format="dd-M-yyyy">
                                </div>
                            </div>
                        </div>
                        <!-- /.row -->
                    </div>
                </div>
                <!-- /.panel -->
            </div>
        </div>
        <!-- /.row -->
    </div>
    <!-- /.box-body -->
</div>
<!-- Anthropometric Measurements -->
<div class="box box-primary">
    <div class="box-header with-border">
        <h3 class="box-title">
            Anthropometric Measurements</h3>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    *Height/Length(cm):</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtHeight" placeholder="Height/Length(cm)"
                    onblur="CalcualteBMI();">
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Weight(Kg):</label>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtWeight" placeholder="Weight" onblur="CalcualteBMI();">
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    BMI:</label>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtBMI" disabled="disabled">
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Head circumference(cm):</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtHeadCircumference" placeholder="Head circumference">
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    MUAC(cm):</label>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtMUAC" placeholder="MUAC">
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Weight for age zscore:</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtWAgeZScore" disabled="disabled">
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Height for age zscore:
                </label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtHAgeZScore" disabled="disabled">
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    BMI Zscore</label>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtBMIZScore" disabled="disabled">
            </div>
        </div>
        <!-- /.row -->
    </div>
    <!-- /.box-body -->
</div>
<!-- Vital Signs -->
<div class="box box-primary">
    <div class="box-header with-border">
        <h3 class="box-title">
            Vital Signs</h3>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    BP (mm/Hg):</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group" style="display: inline-flex">
                <input type="text" class="form-control" id="txtMM" placeholder="MM" style="width: 30%">
                &nbsp;/&nbsp;
                <input type="text" class="form-control" id="txtHG" placeholder="HG" style="width: 30%">
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Temperature(°C):
                </label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtTemperature" placeholder="Temperature">
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Respiratory Rate (bpm):</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtRR">
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Heart Rate (bpm):</label>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtHRate">
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    SPO2 (%):</label>
            </div>
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtSP" style="width: 30%">
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                Nurses Comments:
                <textarea class="form-control" rows="5" placeholder="Nurses Comments" id="txtNursesComment"></textarea>
            </div>
        </div>
        <!-- /.row -->
    </div>
    <!-- /.box-body -->
</div>
<!-- Button Footer -->
<div class="row no-print" align="center">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <button type="button" class="btn btn-primary" onclick="SaveTriageData();">
                Save<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
            <button type="button" class="btn btn-primary">
                Reset<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
