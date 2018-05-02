<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPresentingComplaints.ascx.cs"
    Inherits="HIVCE.Presentation.Controls.ucPresentingComplaints" %>
<%--<script>
    $(document).ready(function () {

        $('#dtlPresentingComplaints').DataTable({
            "paging": false,
            "lengthChange": false,
            "searching": false,
            "ordering": false,
            "info": false,
            "autoWidth": true
        });

    });
</script>--%>
<!-- Presenting Complaints -->
<div class="box box-primary">
    <div class="box-header with-border">
        <h3 class="box-title">
            Presenting Complaints</h3>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    *Any Complaints</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkComplaints" name="switch-size" type="checkbox" data-size="small" data-on-text="Yes"
                    data-off-text="No">
            </div>
        </div>
        <!-- /.row -->
        <div id="divPresentingComplaints" class="row" style="visibility: hidden; display: none;">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Presenting Complaints:</label>
                                <select class="form-control select2" data-placeholder="Select" id="ddlPresentingComplaints"
                                    style="width: 100%;">
                                </select>
                            </div>
                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Onset Date:</label>
                                <input type="text" class="form-control" id="dtOnsetDate" placeholder="Onset Date"
                                    data-date-format="dd-M-yyyy">
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    &nbsp;
                                </label>
                                <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                                    id="btnAddComplaints" onclick="AddPresentingComplaints();">
                                    Add</button>
                            </div>
                        </div>
                        <!-- /.row -->
                        <div style="min-height: 10px; max-height: 150px; overflow-y: scroll; overflow-x: hidden;">
                            <table id="dtlPresentingComplaints" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th style="width: 100px;">
                                            Presenting Complaints
                                        </th>
                                        <th style="width: 80px;">
                                            Onset Date
                                        </th>
                                        <th style="width: 10px;">
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <!-- /.box -->
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                Presenting Complaints Notes:
                                <textarea class="form-control" rows="3" placeholder="Presenting Complaints Notes"
                                    id="tstComplaintsNote"></textarea>
                            </div>
                        </div>
                        <!-- /.row -->
                    </div>
                </div>
                <!-- /.panel -->
            </div>
            <!-- /.row -->
        </div>
    </div>
    <!-- /.box-body -->
</div>
<!-- Past Medical History -->
<div class="box box-primary">
    <div class="box-header with-border">
        <h3 class="box-title">
            Past Medical History</h3>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Any Chronic Disorders?</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkChronicDisorders" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
        </div>
        <!-- /.row -->
        <div id="divChronicDisorders" class="row" style="visibility: hidden; display: none;">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Chronic Condition:</label>
                                <select class="form-control select2" data-placeholder="Select" id="ddlChronicDisorder"
                                    style="width: 100%;">
                                </select>
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Onset Date:</label>
                                <input type="text" class="form-control" id="dtCCOnsetDate" placeholder="Onset Date"
                                    data-date-format="dd-M-yyyy">
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Still Active:</label><br />
                                <input type="checkbox" id="chkStillActive" class="flat-red" />
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    &nbsp;
                                </label>
                                <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                                    id="Button1" onclick="AddChronicDisorder();">
                                    Add</button>
                            </div>
                        </div>
                        <!-- /.row -->
                        <div>
                            <table id="dtlChronicDisorders" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>
                                            Chronic Conditions
                                        </th>
                                        <th>
                                            Onset Date
                                        </th>
                                        <th>
                                            Active?
                                        </th>
                                        <th style="width: 10px;">
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <!-- /.box -->
                        <div class="box">
                            <div class="box-header">
                                <h3 class="box-title">
                                    Previous Admissions</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                Reasons for Admission:
                                <textarea class="form-control" rows="3" placeholder="Presenting Complaints Notes"></textarea>
                            </div>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.row -->
    </div>
</div>
<!-- /.box-body -->
<!-- OBS/GYN History -->
<div class="box box-primary" id="divOBSGYN">
    <div class="box-header with-border">
        <h3 class="box-title">
            OBS / GYN History</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div class="panel panel-default">
                <div class="panel-body">
                    <!-- /.box -->
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">
                                Menstrual History</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Age at Menarche:</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <input type="text" class="form-control" id="txtAgeAtMenarche" placeholder="Age at Menarche">
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        LMP:</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control" id="dtLMP" data-date-format="dd-M-yyyy" style="padding: 13px 12px;">
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Reason For Amenorrhoea:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <select class="form-control select2" data-placeholder="Select" id="ddlReasonAmenorrhoea"
                                        style="width: 100%;">
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->
            </div>
        </div>
        <!-- /.panel -->
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div class="panel panel-default">
                <div class="panel-body">
                    <!-- /.box -->
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">
                                Obstetric History</h3>
                        </div>
                        <!-- /.box-header -->
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    *Pregnant:</label>
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <select class="form-control select2" data-placeholder="Select" id="ddlPregnant" style="width: 100%;">
                                </select>
                            </div>
                            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    EDD:</label>
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control" id="dtEDD" data-date-format="dd-M-yyyy" style="padding: 13px 12px;">
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Delivery /Abortion/Miscarriage Date:</label>
                            </div>
                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control" id="dtOtherDates" data-date-format="dd-M-yyyy"
                                        style="padding: 13px 12px;">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                <div class="row">
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Parity:</label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <input type="text" class="form-control" id="txtParity" placeholder="Parity">
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Gravidae:</label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <input type="text" class="form-control" id="txtGravidae" placeholder="Gravidae">
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Breast Feeding</label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <input id="chkBreastFeeding" name="switch-size" type="checkbox" checked data-size="small"
                                            data-on-text="Yes" data-off-text="No">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->
            </div>
        </div>
        <!-- /.panel -->
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div class="panel panel-default">
                <div class="panel-body">
                    <!-- /.box -->
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">
                                Contraceptive History</h3>
                        </div>
                        <!-- /.box-header -->
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    On Family Planning?</label>
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <input id="chkOnFamilyPlanning" name="switch-size" type="checkbox" checked data-size="small"
                                    data-on-text="Yes" data-off-text="No">
                            </div>
                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Family Planning Method:</label>
                                <select class="form-control select2" data-placeholder="Select" id="ddlFPM" style="width: 100%;">
                                </select>
                            </div>
                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Reason not on Family Planning:</label>
                                <select class="form-control select2" data-placeholder="Select" id="ddlNoFPReason"
                                    style="width: 100%;">
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Cervical Cancer screening in the past 12 months?</label>
                                <input id="chkCCS" name="switch-size" type="checkbox" checked data-size="small" data-on-text="Yes"
                                    data-off-text="No">
                            </div>
                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Date Screened</label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control" id="dtDateScreened" data-date-format="dd-M-yyyy"
                                        style="padding: 13px 12px;">
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    HPV Vaccine ever administered?</label>
                                <input id="chkHPVVaccine" name="switch-size" type="checkbox" checked data-size="small"
                                    data-on-text="Yes" data-off-text="No">
                            </div>
                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->
            </div>
        </div>
        <!-- /.panel -->
    </div>
    <!-- /.row -->
</div>
<!-- /.box-body -->
<!-- Button Footer -->
<div class="row no-print" align="center">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <button type="button" class="btn btn-primary">
                Previous<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
            <button type="button" class="btn btn-primary" onclick="SavePresentingComplaints();">
                Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
