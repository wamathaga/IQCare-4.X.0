<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucANCProfile.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.ucANCProfile" %>
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
<div id="divProfile" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            PROFILE</h3>
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
                    <input type="hidden" runat="server" id="hidDTEDD" clientidmode="Static" />
                </div>
            </div>
            <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Gestation:</label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                <input type="text" id="txtGestation" readonly="readonly" class="form-control" />
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="row">
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Age at Menarche:</label>
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <input type="number" class="form-control" id="txtAgeAtMenarche" placeholder="Age at Menarche">
                    </div>
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
                </div>
            </div>
        </div>
    </div>
    <!-- /.box sub box level G1 -->
</div>
<div class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Past Medical History</h3>
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
        <div class="row">
            <div id="divChronicDisorders" class="col-md-12 col-sm-12 col-xs-12 form-group" style="visibility: hidden;
                display: none;">
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
                                        <th style="width: 65%;">
                                            Chronic Conditions
                                        </th>
                                        <th style="width: 20%;">
                                            Onset Date
                                        </th>
                                        <th style="width: 5%;">
                                            Active?
                                        </th>
                                        <th style="width: 5%;">
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <!-- /.box -->
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="box">
                            <div class="box-header">
                                <h3 class="box-title">
                                    Surgical History</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                        Describe previous surgical procedures:
                                        <textarea class="form-control" id="txtPreviousAdmissions" rows="3" placeholder="Previous Admissions Notes"
                                            maxlength="1000"></textarea>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        History of Blood Transfusion
                                    </div>
                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                        <input id="chkBloodTransfusionHistory" name="switch-size" type="checkbox" checked
                                            data-size="small" data-on-text="Yes" data-off-text="No">
                                    </div>
                                    <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.row -->
    </div>
    <!-- /.box sub box level G1 -->
</div>
<div class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Allergies</h3>
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
            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Patient has any drug allergies?</label>
                <input id="chkDrugAllergies" name="switch-size" type="checkbox" checked data-size="small"
                    data-on-text="Yes" data-off-text="No">
            </div>
        </div>
        <div id="divDrugAllergies" style="visibility: hidden; display: none;">
            <div class="row">
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Allergen Type
                    </label>
                    <select class="form-control select2" data-placeholder="Select" id="ddlAllergenType"
                        style="width: 100%;">
                    </select>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Allergen
                    </label>
                    <div id="divddlAller">
                        <select class="form-control select2" data-placeholder="Select" id="ddlAllergen" style="width: 100%;">
                        </select></div>
                    <div id="divtxtAller">
                        <input type="text" class="form-control" id="txtAllergenOther" placeholder="Allergen" />
                    </div>
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Severity :
                    </label>
                    <select class="form-control select2" data-placeholder="Select" id="ddlSeverity" style="width: 100%;">
                    </select>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Date Of Allergen
                    </label>
                    <input type="text" class="form-control" id="dtDateOfAllergen" placeholder="Date Of Allergen"
                        data-date-format="dd-M-yyyy">
                </div>
                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Type of Reaction
                    </label>
                    <input type="text" class="form-control" id="txtTypeofReaction" placeholder=" Type of Reaction" />
                </div>
                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Comment
                    </label>
                    <input type="text" class="form-control" id="txtAllergenComment" placeholder="Comment" />
                </div>
                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        &nbsp;
                    </label>
                    <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                        id="Button5" onclick="AddDrugAllergies();">
                        Add</button>
                </div>
            </div>
            <div style="min-height: 10px; max-height: 150px; overflow-y: auto; overflow-x: hidden;">
                <table id="dtlDrugAllergies" class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th style="width: 15%;">
                                Allergen Type
                            </th>
                            <th style="width: 30%;">
                                Allergen
                            </th>
                            <th style="width: 10%">
                                Severity
                            </th>
                            <th style="width: 10%">
                                Date
                            </th>
                            <th style="width: 15%">
                                Reaction Type
                            </th>
                            <th style="width: 15%">
                                Comment
                            </th>
                            <th style="width: 5%;">
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
    <!-- /.box sub box level G1 -->
</div>
<div class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Family History of ..</h3>
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
            <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                    id="ddlFamilyHistoryOf" style="width: 100%;">
                </select>
            </div>
        </div>
    </div>
    <!-- /.box sub box level G1 -->
</div>
<div class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Previous Pregnancy</h3>
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
                    Year of Delivery:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <input type="number" class="form-control" id="txtYrofDelivery" style="padding: 13px 12px;">
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Place of Delivery:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <input type="text" class="form-control" id="txtPlaceofDelivery" style="padding: 13px 12px;">
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Maturity :</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <select class="form-control select2" data-placeholder="Select" id="ddlMaturityweeks"
                    style="width: 100%;">
                </select>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Labour duration (hrs)</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <input type="number" class="form-control" id="txtLabourduratioin" style="padding: 13px 12px;">
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Mode of Delivery:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <select class="form-control select2" data-placeholder="Select" id="ddlModeofDelivery"
                    style="width: 100%;">
                </select>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Gender</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <select class="form-control select2" data-placeholder="Select" id="ddlGenderofBaby"
                    style="width: 100%;">
                </select>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Fate:</label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <select class="form-control select2" data-placeholder="Select" id="ddlFateofBaby"
                    style="width: 100%;">
                </select>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Birth Weight
                </label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                <input type="number" class="form-control" id="txtBirthWeight" style="padding: 13px 12px;">
            </div>
        </div>
        <div class="row">
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                    id="Button2" onclick="AddPreviousPregnancy();">
                    Add</button>
            </div>
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
            </div>
        </div>
        <div>
            <table id="dtlPreviousPregnancy" class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>
                            Year of Delivery
                        </th>
                        <th>
                            Place Of Delivery
                        </th>
                        <th>
                            Maturity
                        </th>
                        <th>
                            Labour hours
                        </th>
                        <th>
                            Mode of Delivery
                        </th>
                        <th>
                            Gender
                        </th>
                        <th>
                            Fate
                        </th>
                        <th>
                            Birth Weight
                        </th>
                        <th style="width: 5%;">
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
        <!-- /.box -->
    </div>
    <!-- /.box sub box level G1 -->
</div>
<!-- Button Footer -->
<div class="row no-print" align="center">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <button type="button" class="btn btn-primary" onclick="SaveProfileData();" runat="server"
                id="btnSaveProfile" clientidmode="Static">
                Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
            <button type="button" class="btn btn-primary" runat="server" onclick="DeleteForm();" 
                id="btnDeleteForm" clientidmode="Static">
                Delete<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
