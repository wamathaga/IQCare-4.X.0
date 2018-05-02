<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucTriage.ascx.cs" Inherits="HIVCE.Presentation.Controls.ucTriage" %>
<script type="text/javascript">
    $(document).ready(function () {

        //Date picker
        $("#datepicker111").datepicker({
            autoclose: true
        });
        $("#txtHivConfirm").datepicker({
            autoclose: true
        });
        $("#txtHivEnroll").datepicker({
            autoclose: true
        });
        $("#txtTransferDate").datepicker({
            autoclose: true
        });



        $("#ddlVisitType").select2();
        $("#ddlCareGiven").select2();

    });
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
            <input type="text" class="form-control" id="datepicker111" style="padding: 13px 12px;">
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
                <input type="checkbox" class="minimal" checked>
            </label>
        </div>
    </div>
    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
        *Visit Type:
    </div>
    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
        <select class="form-control select2" data-placeholder="Select" id="ddlVisitType"
            style="width: 100%;">
            <option>Initial Visit -Self</option>
            <option>Follow Up Visit -Self</option>
            <option>Express Visit -Self</option>
            <option>TS -Treatment Supporter</option>
        </select>
    </div>
</div>
<div class="row">
    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
        <label for="inputEmail3" class="control-label">
            *Duration since ART start</label>
    </div>
    <div class="col-md-10 col-sm-12 col-xs-12 form-group">
        <input type="text" disabled="disabled" value="" class="form-control" style="width: 5%;
            display: inline;">
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
                <input id="Checkbox8" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Childs relationship to care giver</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <select class="form-control select2" data-placeholder="Select" id="ddlCareGiven"
                    style="width: 100%;">
                    <option>Aunty/Uncle</option>
                    <option>Brother/Sister</option>
                    <option>Cousin</option>
                    <option>Grandparent</option>
                    <option>Parent</option>
                    <option>Not Family</option>
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
                <input id="Checkbox1" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Name of Support Group</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtSupportGroup" placeholder="Name of Support Group">
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
                    <input type="text" class="form-control" id="txtHivConfirm" style="padding: 13px 12px;">
                </div>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Where:</label>
            </div>
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="Text2" placeholder="Where..">
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    *Date Enrolled in HIV Care:</label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                    </div>
                    <input type="text" class="form-control" id="txtHivEnroll" style="padding: 13px 12px;">
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
                <input id="Checkbox2" name="switch-size" type="checkbox" checked data-size="small"
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
                                    <input type="text" class="form-control" id="txtTransferDate" style="padding: 13px 12px;">
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    From District/County</label>
                            </div>
                            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                <input type="text" class="form-control" id="txtLocation" placeholder="From District/County">
                            </div>
                        </div>
                        <!-- /.row -->
                        <div class="row">
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    From Facility</label>
                            </div>
                            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                <input type="text" class="form-control" id="txtFacility" placeholder="From Facility">
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Date Started ART</label>
                            </div>
                            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                <input type="text" class="form-control" id="txtDateStatedArt" placeholder="Date Started ART">
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
                <input type="text" class="form-control" id="txtHeight" placeholder="Height/Length(cm)">
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Weight(Kg):</label>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtWeight" placeholder="Weight">
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
                <input type="text" class="form-control" id="Text1" disabled="disabled">
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Height for age zscore:
                </label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="Text3" disabled="disabled">
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    BMI Zscore</label>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="Text4" disabled="disabled">
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
                <input type="text" class="form-control" id="Text6">
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
                <textarea class="form-control" rows="5" placeholder="Nurses Comments"></textarea>
            </div>
        </div>
        <!-- /.row -->
    </div>
    <!-- /.box-body -->
</div>
<!-- Button Footer -->
<div class="row no-print">
    <div class="form-group">
        <div class="col-md-6">
            <button class="btn btn-primary" style="margin-right: 5px;">
                <i class="fa fa-save" style="padding-right:5px;"></i>Save Triage</button>
        </div>
    </div>
</div>
