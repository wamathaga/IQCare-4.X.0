<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    CodeBehind="RefillEncounterForm.aspx.cs" Inherits="HIVCE.Presentation.RefillEncounterForm" %>

<asp:Content ID="Data" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script src="<%=ResolveUrl("Scripts/RefillEncounterForm.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/Constants.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
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
                                    <h3 class="box-title">Refill Encounter Form</h3>
                                    <!-- /.box-tools -->
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="col-md-12 col-sm-12 col-xs-12" style="padding-left:7px;padding-right:7px;">
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
                                        
                                        <div id="divOtherThanTS">
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
                                                            <input type="number" class="form-control" id="txtWeight" placeholder="Weight" 
                                                                   onblur="CalcualteBMI('hidPAYM','txtWeight','txtHeight','txtBMI','lblBMIClassification');"/>
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
                                                </div>
                                                <!-- /.box-body -->
                                            </div>
                                        </div>
                                    
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
                                                        Any current/worsening symptoms
                                                    </label>
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
                                                                        <th style="width: 65%;">
                                                                            Presenting Complaints
                                                                        </th>
                                                                        <th style="width: 30%;">
                                                                            Onset Date
                                                                        </th>
                                                                        <th style="width: 5%;">
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
                                    id="txtComplaintsNote"></textarea>
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
                                    <!-- /.panel -->
                                        <div class="panel panel-default" id="divOBSGYN">
                                            <div class="panel-body">
                                                <!-- /.box -->
                                                <div class="box">
                                                    <div class="box-header">
                                                        <h3 class="box-title">
                                                            OBS / GYN History</h3>
                                                    </div>
                                                    <!-- /.box-header -->
                                                </div>
                                                <div class="box-body">
                                                    <div class="row">
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                            <label for="inputEmail3" class="control-label requiredFieldColor">
                                                                Pregnant:</label>
                                                        </div>
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                            <select class="form-control select2" data-placeholder="Select" id="ddlPregnant" style="width: 100%;">
                                                            </select>
                                                            <input id="Checkbox1" name="switch-size" type="checkbox" checked data-size="small"
                                                                   data-on-text="Yes" data-off-text="No">
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
                                                                Breast Feeding</label>
                                                        </div>
                                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                            <input id="chkBreastFeeding" name="switch-size" type="checkbox" checked data-size="small"
                                                                   data-on-text="Yes" data-off-text="No">
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- /.box-body -->
                                            </div>
                                            <!-- /.box -->
                                        </div>
                                    <!-- /.panel -->
                                        <div class="box box-primary" id="divContraceptiveHistory">
                                            <div class="box-header">
                                                <h4 class="box-title">
                                                    Contraceptive History</h4>
                                                <div class="box-tools pull-right">
                                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                                        <i id="divContraceptiveHistoryI" class="fa fa-minus"></i>
                                                    </button>
                                                </div>
                                            </div>
                                            <!-- /.box-header -->
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
                                                <!-- /.row -->
                                            </div>
                                            <!-- /.box-body -->
                                        </div>
                                    <!-- /.panel -->
                                    <div class="box box-primary" id="div1">
                                        <div class="box-header">
                                            <h4 class="box-title">
                                                Adherence</h4>
                                            <div class="box-tools pull-right">
                                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                                    <i id="I1" class="fa fa-minus"></i>
                                                </button>
                                            </div>
                                        </div>
                                        <!-- /.box-header -->
                                        <div class="box-body">
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <label for="inputEmail3" class="control-label">
                                                        Did you forget to take any of your medicine since the last visit?</label>
                                                </div>
                                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                                    <input id="chkForgotMed" name="switch-size" type="checkbox" checked data-size="small"
                                                           data-on-text="Yes" data-off-text="No">
                                                </div>
                                                <div class="col-md-1 col-sm-12 col-xs-12 form-group"  id="divForgotMed1">
                                                    <label for="inputEmail3" class="control-label">
                                                        No. of Missed Doses:</label>
                                                </div>
                                                <div class="col-md-1 col-sm-12 col-xs-12 form-group"id="divForgotMed2">
                                                    <input type="number" class="form-control" id="txtMissedDoses" placeholder="Missed Doses">
                                                </div>
                                                <div class="col-md-1 col-sm-12 col-xs-12 form-group"id="divForgotMed3">
                                                    <label for="inputEmail3" class="control-label">
                                                        Drug(s):</label>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group"id="divForgotMed4">
                                                    <input type="text" class="form-control" id="txtDrugs" placeholder="Drugs">
                                                </div>
                                            </div>
                                            <!-- /.row -->
                                            <div class="row">
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                    <label for="inputEmail3" class="control-label requiredFieldColor">
                                                        Prevention with positives (PwP):
                                                    </label>
                                                </div>
                                                <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                                                    <select class="form-control select2" data-placeholder="Select" multiple="multiple"
                                                            id="ddlPWP" style="width: 100%;">
                                                    </select>
                                                </div>
                                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                                </div>
                                            </div>
                                            <!-- /.row -->
                                            <div class="box-header with-border" id="divMgrRAWH">
                                                <h3 class="box-title">
                                                    Reminders and workplan
                                                </h3>
                                                <!-- /.box-tools -->
                                            </div>
                                            <!-- /.box-header -->
                                            <div class="box-body" id="divMgrRAWB">
                                                <!-- / sub box level 2 -->
                                                <div class="box box-default box-solid">
                                                    <!-- /.box-header -->
                                                    <div class="box-body">
                                                        <!-- /.box-body -->
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                <textarea class="form-control" rows="5" placeholder="Reminders and Workplan" id="txtAreaReminders"></textarea>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- /.box sub box level 2 -->
                                                </div>
                                                <!-- /.box-body -->
                                            </div>
                                       
                                            <div class="row">
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                    <label for="inputEmail3" class="control-label">
                                                        Current Regimen:
                                                    </label>
                                                </div>
                                                <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                                                    <label for="inputEmail3" class="control-label">
                                                       Regimen Code:</label>
                                                    <select class="form-control select2" data-placeholder="Select" multiple="multiple"
                                                            id="ddlRegimenCode" style="width: 100%;">
                                                    </select>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                </div>
                                             </div>
                                             <!-- /.row -->
                                            <div class="row">
                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                    <label for="inputEmail3" class="control-label">
                                                        Action Box:
                                                    </label>
                                                </div>
                                                <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                                                    <textarea class="form-control" style="background-color:green !important; color: white !important;" rows="2" placeholder="Action Box" id="txtActionBox">
Refill Prescription and Schedule/Confirm next clinical Appointment 
</textarea>
                                                </div>
                                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                                </div>
                                            </div>
                                            <!-- /.row -->
                                        </div>
                                        <!-- /.box-body -->
                                    </div>
                                    <!-- /.panel -->
                                    
                                    <div id="div5" class="box box-default box-solid">
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
                                                        &nbsp;
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        &nbsp;
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            Service Area</label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            Purpose</label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            Description</label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        &nbsp;
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label requiredFieldColor">
                                                            * Next Appointment Date</label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <input type="text" class="form-control" id="dtNextAppointmentDate" data-date-format="dd-M-yyyy"
                                                                   style="padding: 13px 12px;" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <input type="text" class="form-control" id="txtServiceArea" placeholder="Service Area"
                                                               disabled="disabled" />
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <select class="form-control select2" data-placeholder="Select" id="ddlPurpose">
                                                        </select>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <input type="text" class="form-control" id="txtNADDescription" placeholder="Description" />
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                                                                id="btnAddNextAppointment" onclick="AddAppointment('0');">
                                                            Add</button>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label requiredFieldColor">
                                                            * Drug Refill Appointment</label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <input type="text" class="form-control" id="dtDrugRefillAppointmentDate" data-date-format="dd-M-yyyy"
                                                                   style="padding: 13px 12px;" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <input type="text" class="form-control" id="txtDRASA" placeholder="Service Area"
                                                               disabled="disabled" />
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <select class="form-control select2" data-placeholder="Select" id="ddlDRAPurpose">
                                                        </select>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <input type="text" class="form-control" id="txtDRADescription" placeholder="Description" />
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                                                                id="btnAddDrugRefillAppointment" onclick="AddAppointment('1');">
                                                            Add</button>
                                                    </div>
                                                 
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                        <table id="dtlNextAppointment" class="table table-bordered table-striped">
                                                            <thead>
                                                            <tr>
                                                                <th style="width: 50px;">
                                                                    #
                                                                </th>
                                                                <th style="width: 150px;">
                                                                    Appintment Date
                                                                </th>
                                                                <th style="width: 100px;">
                                                                    Service Area
                                                                </th>
                                                                <th style="width: 100px;">
                                                                    Reason
                                                                </th>
                                                                <th style="width: 250px;">
                                                                    Description
                                                                </th>
                                                                <th style="width: 100px;">
                                                                    Status
                                                                </th>
                                                                <th style="width: 10px;">
                                                                    Edit
                                                                </th>
                                                            </tr>
                                                            </thead>
                                                        </table>
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
                                                <button type="button" class="btn btn-primary" onclick="SaveRefillEncounterForm();" runat="server"
                                                        id="btnRefillEncounterForm" clientidmode="Static">
                                                    Save<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.box -->
                                    </div>
                                </div>
                                <!-- /.box-body -->
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
        <input type="hidden" runat="server" id="hidPAYM" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidDOB" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidPID" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidGender" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidVId" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidL" clientidmode="Static" value="0" />
        <input type="hidden" runat="server" id="hidM" clientidmode="Static" value="0" />
        <input type="hidden" runat="server" id="hidS" clientidmode="Static" value="0" />
        <input type="hidden" runat="server" id="hidTabName" clientidmode="Static" value="triage"
            runat="server" />
        <input type="hidden" runat="server" id="hidARTStartDate" clientidmode="Static" value=""
            runat="server" />
        <input type="hidden" runat="server" id="hidEIVID" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidEIVD" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidMOD" clientidmode="Static" />
        <input type="hidden" runat="server" id="hidsrvNm" clientidmode="Static" />
    </div>
</asp:Content>
