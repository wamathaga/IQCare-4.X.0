<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPrEPTriage.ascx.cs"
    Inherits="PresentationApp.PrEP.Controls.ucPrEPTriage" %>
<script type="text/javascript">

</script>
<div>
    <!-- Visit Details -->
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
                    style="padding: 13px 12px;" />
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
                    <input type="checkbox" class="minimal" id="chkScheduled" />
                </label>
            </div>
        </div>
        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
            <label for="inputEmail3" class="control-label requiredFieldColor">
                Visit Type:
            </label>
        </div>
        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <select class="form-control select2" data-placeholder="Select" id="ddlVisitType"
                style="width: 100%;">
            </select>
        </div>
    </div>
    <!-- Client Details -->
    <div class="box box-primary" id="divClientDetails">
        <div class="box-header with-border">
            <h3 class="box-title">
                Client Details</h3>
        </div>
        <div class="box-body">
            <div class="row">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Is the child in school</label>
                </div>
                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                    <input id="chkIsInSchool" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No">
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group" id="divSchool">
                    <select class="form-control select2" data-placeholder="Select" id="ddlSchoolClass"
                        style="width: 100%;">
                        <option value="0">Select</option>
                        <option value="PrePrimary">PrePrimary</option>
                        <option value="Primary">Primary</option>
                        <option value="Secondary">Secondary</option>
                    </select>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Key Population?</label>
                    <input id="chkKeyPopulation" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No">
                </div>
                <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                    <div id="divKeyPopulation">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Entry Point and Transfer Status -->
    <div class="box box-primary" id="divTransfer">
        <div class="box-header with-border">
            <h3 class="box-title">
                Entry Point and Transfer Status</h3>
            <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                    <i class="fa fa-minus"></i>
                </button>
            </div>
        </div>
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
                                    <label for="inputEmail3" class="control-label requiredFieldColor">
                                        Transfer In Date</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control" id="dtTransferInDate" style="padding: 13px 12px;"
                                            data-date-format="dd-M-yyyy">
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        District</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <input type="text" class="form-control" id="txtDistrict" placeholder="District" disabled="disabled">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label requiredFieldColor">
                                        PrEP Start Date</label>
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
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label requiredFieldColor">
                                        PrEP Regimen:</label>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <select class="form-control select2" data-placeholder="Select" id="ddlARTStartRegimen"
                                        style="width: 100%;">
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Anthropometric Measurements -->
    <div class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">
                Anthropometric Measurements</h3>
        </div>
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
    </div>
    <!-- Vital Signs -->
    <div class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">
                Vital Signs</h3>
        </div>
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
                    <textarea class="form-control" rows="5" placeholder="Nurses Comments" id="txtNursesComment"
                        maxlength="1000"></textarea>
                </div>
            </div>
            <!-- /.row -->
        </div>
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
        </div>
    </div>
    <!-- Button Footer -->
    <div class="row no-print" align="center">
        <div class="form-group">
            <div class="col-md-12" align="center">
                <button type="button" class="btn btn-primary" onclick="SaveTriageData(false);" runat="server"
                    id="btnSaveTriage" clientidmode="Static">
                    Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
                <button type="button" class="btn btn-primary" runat="server" onclick="DeleteForm();"
                    id="btnDeleteForm" clientidmode="Static">
                    Delete<span class="glyphicon glyphicon-remove" style="padding-left: 5px;"></span></button>
            </div>
        </div>
    </div>
</div>
