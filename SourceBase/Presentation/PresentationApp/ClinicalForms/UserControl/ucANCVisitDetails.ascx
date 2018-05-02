<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucANCVisitDetails.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.ucANCVisitDetails" %>
<div class="row">
    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
        <label for="inputEmail3" class="control-label requiredFieldColor">
            First Visit Date</label>
    </div>
    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
        <div class="input-group date">
            <div class="input-group-addon">
                <i class="fa fa-calendar"></i>
            </div>
            <input type="text" class="form-control" id="dtFirstVisitDate" data-date-format="dd-M-yyyy"
                style="padding: 13px 12px;">
        </div>
    </div>
    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
        (MM-DD-YYYY)
    </div>
</div>
<div id="divVisitDetails" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Visit Details</h3>
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
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Date:</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                    </div>
                    <input type="text" class="form-control" id="dtVDDate" data-date-format="dd-M-yyyy"
                        style="padding: 13px 12px;">
                </div>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Pregnancy state:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group" id="divRadioPregnancy">
                <input type="radio" id="rbANC" name="rbPS" class="flat-red" value="ANC" />
                <label for="minimal-radio-1">
                    ANC</label>
                <input type="radio" id="rbPNC" name="rbPS" class="flat-red" value="Postnatal/Postpartum" />
                <label for="minimal-radio-1">
                    Postnatal/Postpartum</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Visit Number:</label>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <input type="text" id="txtVisitNumber" class="form-control" />
            </div>
        </div>
        <!-- /.row -->
    </div>
    <!-- /.box sub box level G1 -->
</div>
<div id="div1" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Anthropometric Measurements</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i id="I1" class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    *Height (cm):</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input type="number" class="form-control" id="txtHeight" placeholder="Height/Length(cm)" />
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Weight(Kg):</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input type="number" class="form-control" id="txtWeight" placeholder="Weight">
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    MUAC(cm):</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtMUAC" placeholder="MUAC">
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    BP (mm/Hg):
                </label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group" style="display: inline-flex">
                <input type="number" class="form-control" id="txtMM" placeholder="MM" style="width: 50%">
                &nbsp;/&nbsp;
                <input type="number" class="form-control" id="txtHG" placeholder="HG" style="width: 50%">
            </div>
        </div>
        <div class="row">
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                    id="Button1" onclick="AddVisitAnthropometric();">
                    Add</button>
            </div>
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <table id="dtlVisitAnthropometric" class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th style="width: 15%;">
                                Date
                            </th>
                            <th style="width: 15%;">
                                State
                            </th>
                            <th style="width: 15%;">
                                Visit Number
                            </th>
                            <th style="width: 15%;">
                                Height
                            </th>
                            <th style="width: 15%;">
                                Weight
                            </th>
                            <th style="width: 15%;">
                                MUAC
                            </th>
                            <th style="width: 10%;">
                                BP (mm/hg)
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
    <!-- /.box sub box level G1 -->
</div>
<div id="div2" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Present Pregnancy</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i id="I2" class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Maturity (wks)</label>
                <input type="number" class="form-control" id="txtMaturity" placeholder="Maturity (wks)" />
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Lie</label>
                <select class="form-control select2" data-placeholder="Select" id="ddlLie" style="width: 100%;">
                    <option value="0">Select</option>
                    <option value="Longitudinal">Longitudinal</option>
                    <option value="Oblique">Oblique</option>
                    <option value="Transverse">Transverse</option>
                </select>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Fundal Height (cm)</label>
                <input type="number" class="form-control" id="txtFundalHeight" placeholder="Fundal Height" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Foetal Movement</label>
                <select class="form-control select2" data-placeholder="Select" id="ddlFoetalMovement"
                    style="width: 100%;">
                    <option value="0">Select</option>
                    <option value="Normal">Normal</option>
                    <option value="Reduced">Reduced</option>
                    <option value="Increased">Increased</option>
                </select>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Presentation</label>
                <select class="form-control select2" data-placeholder="Select" id="ddlPresentation"
                    style="width: 100%;">
                    <option value="0">Select</option>
                    <option value="Cephalic">Cephalic</option>
                    <option value="Breech">Breech</option>
                </select>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Foetal Heart Rate</label>
                <input type="number" class="form-control" id="txtFoetalHeartRate" placeholder="Foetal Heart Rate" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                    id="Button2" onclick="AddPresentPregnancy();">
                    Add</button>
            </div>
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <table id="dtlPresentPregnancy" class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th style="width: 15%;">
                                Maturity
                            </th>
                            <th style="width: 15%;">
                                Lie
                            </th>
                            <th style="width: 15%;">
                                Fundal Height
                            </th>
                            <th style="width: 15%;">
                                Foetal Movement
                            </th>
                            <th style="width: 15%;">
                                Presentation
                            </th>
                            <th style="width: 15%;">
                                Foetal Heart Rate
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
    <!-- /.box sub box level G1 -->
</div>
<div id="div3" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Preventive Services</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i id="I3" class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Did client receive tetanus toxoid vaccine?</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkTT" name="switch-size" type="checkbox" checked data-size="small" data-on-text="Yes"
                    data-off-text="No">
            </div>
            <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                <textarea class="form-control" id="txtTTNotes" rows="3" placeholder="Give reason why-TT vaccine wasn't issued"
                    maxlength="1000"></textarea>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                Malaria Prophylaxis
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Intermittent preventive treatment in pregnancy (IPTp)</label>
                <select class="form-control select2" data-placeholder="Select" id="ddlIPTp" style="width: 100%;">
                    <option value="0">Select</option>
                    <option value="1">IPTp 1</option>
                    <option value="2">IPTp 2</option>
                    <option value="3">IPTp 3</option>
                    <option value="4">IPTp 4</option>
                    <option value="5">IPTp 5</option>
                    <option value="6">IPTp 6</option>
                    <option value="7">IPTp 7</option>
                </select>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Date Given</label>
                <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                    </div>
                    <input type="text" class="form-control" id="dtIPTp" data-date-format="dd-M-yyyy"
                        style="padding: 13px 12px;">
                </div>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Insectiside Treated Net given?</label>
                <input id="chkInsectisideTreated" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Date Given</label>
                <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                    </div>
                    <input type="text" class="form-control" id="dtInsectisideTreated" data-date-format="dd-M-yyyy"
                        style="padding: 13px 12px;">
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Dewormed?</label>
                <input id="chkDewormed" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Date Given</label>
                <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                    </div>
                    <input type="text" class="form-control" id="dtDewormed" data-date-format="dd-M-yyyy"
                        style="padding: 13px 12px;">
                </div>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Iron and Folate given</label>
                <input id="chkIronFolate" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Date Given</label>
                <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                    </div>
                    <input type="text" class="form-control" id="dtIronFolate" data-date-format="dd-M-yyyy"
                        style="padding: 13px 12px;">
                </div>
            </div>
        </div>
    </div>
    <!-- /.box sub box level G1 -->
</div>
<div id="div4" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Infant Feeding</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i id="I4" class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Infant Feeding counselling done?</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkInfantFeedingCounselling" name="switch-size" type="checkbox" checked
                    data-size="small" data-on-text="Yes" data-off-text="No">
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Counselling on exclusive breastfeeding done?</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkExBreastfeedingCounselling" name="switch-size" type="checkbox" checked
                    data-size="small" data-on-text="Yes" data-off-text="No">
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Infant feeding options for HIV infected discussed?</label>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <input id="chkHIVIFIF" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Mothers decision on Infant feeding:</label>
                <select class="form-control select2" data-placeholder="Select" id="ddlMDIF" style="width: 100%;">
                    <option value="0">Select</option>
                    <option value="1">Exclusive Breastfeeding</option>
                    <option value="2">Replacement Feeding</option>
                    <option value="3">Not decided</option>
                </select>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Assessment for conditions for exclusive replacement done:</label>
                <input id="chkACERD" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
        </div>
    </div>
    <!-- /.box sub box level G1 -->
</div>
<div id="div5" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Delivery</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i id="I5" class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Place of Delivery</label>
                <input type="text" class="form-control" id="txtPOD" placeholder="Foetal Heart Rate"
                    maxlength="200" />
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Date</label>
                <div class="input-group date">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                    </div>
                    <input type="text" class="form-control" id="dtPOD" data-date-format="dd-M-yyyy" style="padding: 13px 12px;">
                </div>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Mode of Delivery</label>
                <select class="form-control select2" data-placeholder="Select" id="ddlMOD" style="width: 100%;">
                    <option value="0">Select</option>
                    <option value="263">Normal SVD</option>
                    <option value="264">Ceasarian section</option>
                </select>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Blood Loss</label>
                <select class="form-control select2" data-placeholder="Select" id="ddlBloodloss"
                    style="width: 100%;">
                    <option value="0">Select</option>
                    <option value="1">Light</option>
                    <option value="2">Medium</option>
                    <option value="3">Heavy</option>
                </select>
            </div>
            <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Apgar Score</label>
                <div class="row">
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            1 min</label>
                        <input type="text" class="form-control" id="txtApgarScore1min" placeholder="1 min"
                            maxlength="200" />
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            5 min</label>
                        <input type="text" class="form-control" id="txtApgarScore5min" placeholder="5 min"
                            maxlength="200" />
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            10 min</label>
                        <input type="text" class="form-control" id="txtApgarScore10min" placeholder="10 min"
                            maxlength="200" />
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Resusitation done</label>
                <input id="chkResusitationdone" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
        </div>
    </div>
    <!-- /.box sub box level G1 -->
</div>
<div id="div6" class="box box-default box-solid">
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
                    disabled="disabled" value="PMTCT" />
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
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <table id="dtlNextAppointment" class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <%--<th style="width: 50px;">
                                #
                            </th>--%>
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
                            <%--<th style="width: 5%;">
                            </th>--%>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
</div>
<div id="div7" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            PMTCT</h3>
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
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <button type="button" class="btn btn-primary" onclick="OpenCEPopup('<%= ResolveUrl("~/HIVCE/ClinicalEncounter.aspx") %>');">
                    Clinical Encounter<span class="glyphicon glyphicon-pencil" style="padding-left: 5px;"></span></button>
            </div>
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            </div>
        </div>
    </div>
</div>
<!-- Button Footer -->
<div class="row no-print" align="center">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <button type="button" class="btn btn-primary" onclick="GetPreviousPage('tab_2');">
                Previous<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
            <button type="button" class="btn btn-primary" onclick="SaveVisitDetails();" runat="server"
                id="btnSaveManagement" clientidmode="Static">
                Save<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
