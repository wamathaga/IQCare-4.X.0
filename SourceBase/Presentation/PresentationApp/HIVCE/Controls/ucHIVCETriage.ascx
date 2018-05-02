<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucHIVCETriage.ascx.cs"
    Inherits="HIVCE.Presentation.Controls.ucHIVCETriage" %>
<script type="text/javascript">

</script>
<div class="row">
    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
        <label for="inputEmail3" class="control-label requiredFieldColor">
            Visit Date</label>
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
        <label for="inputEmail3" class="control-label">
            Scheduled?:
        </label>
    </div>
    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
        <div class="form-group">
            <label>
                <input type="checkbox" class="minimal" id="chkScheduled" checked>
            </label>
        </div>
    </div>
    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
        <label for="inputEmail3" class="control-label requiredFieldColor">
            Visit Type:
        </label>
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
            Duration since ART start</label>
    </div>
    <div class="col-md-10 col-sm-12 col-xs-12 form-group">
        <input type="text" id="txtARTStart" disabled="disabled" value="" class="form-control"
            style="width: 5%; display: inline;">
        Months
    </div>
</div>
<div id="divOtherThanTS">
    <!-- Visit Details -->
    <div class="box box-primary" id="divVisitDetails">
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
            <div class="row" id="divReferredFrom">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Referred From (Place of first diagnosis)</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <select class="form-control select2" data-placeholder="Select" id="ddlReferredFrom"
                        style="width: 100%;">
                    </select>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <input type="text" class="form-control" id="txtReferredFrom" placeholder="Referred From">
                </div>
            </div>
            <div class="row" id="rowCareGiver">
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
    <div class="box box-primary" id="divHIVCare">
        <div class="box-header with-border">
            <h3 class="box-title">
                HIV Care</h3>
            <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                    <i id="divHIVCareI" class="fa fa-minus"></i>
                </button>
            </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body" id="divBodyHivCare">
            <div class="row">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label requiredFieldColor">
                        Date Confirmed HIV Positive:</label>
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
                    <label for="inputEmail3" class="control-label requiredFieldColor">
                        Date Enrolled in HIV Care:</label>
                </div>
                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
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
                        Transfer In</label>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <input id="chkTransferIn" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No">
                </div>
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="divTransferIn">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label requiredFieldColor">
                                        Facility Transfer From</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <select class="form-control select2" data-placeholder="Select" id="ddlFacility" style="width: 100%;">
                                    </select>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        MFL Code</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <input type="text" class="form-control" id="txtMFLCode" placeholder="MFL Code" disabled="disabled">
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        District</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <input type="text" class="form-control" id="txtDistrict" placeholder="District" disabled="disabled">
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label requiredFieldColor">
                                        ART Start Date</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control" id="dtDateStatedArt" style="padding: 13px 12px;"
                                            data-date-format="dd-M-yyyy">
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label requiredFieldColor">
                                        First Line Regimen:</label>
                                    <select class="form-control select2" data-placeholder="Select" id="ddlARTStartRegimen"
                                        style="width: 100%;">
                                    </select>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label requiredFieldColor">
                                        Current Regimen:</label>
                                    <select class="form-control select2" data-placeholder="Select" id="ddlCurrentRegimen"
                                        style="width: 100%;">
                                    </select>
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="background: gray;">
                                    <h5 class="box-title">
                                        Baseline Assessment at Treatment Initiation</h5>
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    WHO Stage
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <select class="form-control select2" data-placeholder="Select" id="ddlTARTWHOStage"
                                        style="width: 100%;">
                                    </select>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        CD4 Count</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <input type="text" class="form-control" id="txtTCD4Count" placeholder="CD4 Count">
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        MUAC(cm):</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <input type="text" class="form-control" id="txtTMUAC" placeholder="MUAC">
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Height/Length(cm):</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <input type="number" class="form-control" id="txtTHeight" placeholder="Height/Length(cm)"
                                        onblur="CalcualteBMI('hidPAYM','txtTWeight','txtTHeight','txtTBMI','lblTBMIClassification');" />
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Weight(Kg):</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <input type="number" class="form-control" id="txtTWeight" placeholder="Weight" onblur="CalcualteBMI('hidPAYM','txtTWeight','txtTHeight','txtTBMI','lblTBMIClassification');">
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        BMI:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <input type="text" class="form-control" id="txtTBMI" disabled="disabled">
                                    <label for="txtBMI" id="lblTBMIClassification">
                                    </label>
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    Baseline Viral Load
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <input type="text" class="form-control" id="txtBLViralLoad" placeholder="Viral Load">
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    Baseline Viral Load Date
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control" id="dtBLViralLoadDate" style="padding: 13px 12px;"
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
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label requiredFieldColor">
                        Height/Length(cm):</label>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <input type="number" class="form-control" id="txtHeight" placeholder="Height/Length(cm)"
                        onblur="CalcualteBMI('hidPAYM','txtWeight','txtHeight','txtBMI','lblBMIClassification');" />
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label requiredFieldColor">
                        Weight(Kg):</label>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <input type="number" class="form-control" id="txtWeight" placeholder="Weight" onblur="CalcualteBMI('hidPAYM','txtWeight','txtHeight','txtBMI','lblBMIClassification');">
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group" id="divBMILable">
                    <label for="inputEmail3" class="control-label">
                        BMI:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group" id="divBMIText">
                    <input type="text" class="form-control" id="txtBMI" disabled="disabled">
                    <label for="txtBMI" id="lblBMIClassification">
                    </label>
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
                <div class="col-md-1 col-sm-12 col-xs-12 form-group" id="divBMIZ">
                    <label for="inputEmail3" class="control-label">
                        BMI Zscore</label>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group" id="divBMIZV">
                    <input type="text" class="form-control" id="txtBMIZScore" disabled="disabled">
                    <label for="txtBMIZScore" id="lblBMIzClassification">
                    </label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group" id="divWA">
                    <label for="inputEmail3" class="control-label">
                        Weight for age zscore:</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group" id="divWAV">
                    <input type="text" class="form-control" id="txtWAgeZScore" disabled="disabled">
                    <label for="txtWAgeZScore" id="lblWAClassification">
                    </label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group" id="divHA">
                    <label for="inputEmail3" class="control-label">
                        Height for age zscore:
                    </label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group" id="divHAV">
                    <input type="text" class="form-control" id="txtHAgeZScore" disabled="disabled">
                    <label for="txtHAgeZScore" id="lblWHClassification">
                    </label>
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
                    <label id="lblBPMMHG" for="inputEmail3" class="control-label">
                        BP(mm/Hg):</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group" style="display: inline-flex">
                    <input type="number" class="form-control" id="txtMM" placeholder="MM" style="width: 50%">
                    &nbsp;/&nbsp;
                    <input type="number" class="form-control" id="txtHG" placeholder="HG" style="width: 50%">
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Temperature(°C):
                    </label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <input type="number" class="form-control" id="txtTemperature" placeholder="Temperature"
                        onblur="isBetween('txtTemperature', 'Temperature', '30', '50'); HighLightAbnormalValues('temp','txtTemperature');">
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Respiratory Rate (bpm):</label>
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <input type="number" class="form-control" id="txtRR">
                </div>
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Heart Rate (bpm):</label>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <input type="number" class="form-control" id="txtHRate">
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
                    <textarea class="form-control" rows="5" placeholder="Nurses Comments" id="txtNursesComment" maxlength="1000"></textarea>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.box-body -->
    </div>
    <div class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">
            </h3>
        </div>
        <div class="box-body">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                    <button type="button" class="btn btn-primary" onclick="OpenCEPopup('<%= ResolveUrl("~/ClinicalForms/frmPatientWaitingList.aspx") %>');">
                        Add to Waiting List<span class="glyphicon glyphicon-pencil" style="padding-left: 5px;"></span></button>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        Signature:
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <select class="form-control select2" data-placeholder="Select" id="ddlSignature"
                            style="width: 100%;">
                        </select>
                    </div>
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
                <button type="button" class="btn btn-primary" onclick="SaveTriageData(true);" runat="server"
                    id="btnSaveFindTriage" clientidmode="Static">
                    Save & Find<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
                <button type="button" class="btn btn-primary" onclick="SaveTriageData(false);" runat="server"
                    id="btnSaveTriage" clientidmode="Static">
                    Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
                <button type="button" class="btn btn-primary" runat="server" onclick="DeleteForm();"
                    id="btnDeleteForm" clientidmode="Static">
                    Delete<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
                <%--<button type="button" class="btn btn-primary">
                Reset<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>--%>
            </div>
        </div>
    </div>
</div>
<div id="divTSDetails">
    <!-- / sub box level G1 -->
    <div class="box box-default box-solid">
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
                                Next Appointment Date</label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control" id="dtTSNextAppointmentDate" data-date-format="dd-M-yyyy"
                                    style="padding: 13px 12px;" />
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Purpose</label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <select class="form-control select2" data-placeholder="Select" id="ddlTSPurpose">
                            </select>
                        </div>
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Signature
                            </label>
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                            <select class="form-control select2" data-placeholder="Select" id="ddlTSMgtSignature">
                            </select>
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
                <button type="button" class="btn btn-primary" onclick="SaveTSTriageData();" runat="server"
                    id="btnTSSaveManagement" clientidmode="Static">
                    Save & Find<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
            </div>
        </div>
    </div>
</div>
