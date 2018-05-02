<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAddtionalHx.ascx.cs"
    Inherits="HIVCE.Presentation.Controls.ucAddtionalHx" %>
<!-- / sub box level G1 -->
<div id="divG1" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Neonatal History</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <!-- / sub box level 1 -->
        <div class="box box-default box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">
                    Milestones</h3>
                <!-- /.box-tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <%--    <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Milestone Assessed</label>
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <input type="checkbox" id="chkMA3Mon" class="flat-red" />
                        3 Mons
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <input type="checkbox" id="chkMA6mon" class="flat-red" />
                        6 Mons
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <input type="checkbox" id="chkMA12Mon" class="flat-red" />
                        12 Mons
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <input type="checkbox" id="chkMA18Mon" class="flat-red" />
                        18 Mons
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <input type="checkbox" id="chkMA2Yrs" class="flat-red" />
                        2 Years
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <input type="checkbox" id="chkMA3Yrs" class="flat-red" />
                        3 Years
                    </div>
                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        <input type="checkbox" id="chkMA4Yrs" class="flat-red" />
                        4 Years
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <a href="#">Milestones Guide</a>
                    </div>
                </div>--%>
                <!-- /.row -->
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Milestone Assessed:</label>
                                    <select class="form-control select2" data-placeholder="Select" id="ddlMilestone"
                                        style="width: 100%;">
                                    </select>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Date Assessed:</label>
                                    <input type="text" class="form-control" id="dtAssessed" placeholder="Date Assessed"
                                        data-date-format="dd-M-yyyy">
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Achieved:</label>
                                    <input type="checkbox" id="chkAchieved" class="flat-red" />
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Status:</label>
                                    <select class="form-control select2" data-placeholder="Select" id="ddlStatus" style="width: 100%;">
                                    </select>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Comment:</label>
                                    <input type="text" class="form-control" id="txtComment" placeholder="Comment">
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        &nbsp;
                                    </label>
                                    <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                                        id="btnAddMilestones" onclick="AddMilestones();">
                                        Add</button>
                                </div>
                            </div>
                            <!-- /.row -->
                            <div style="min-height: 10px; max-height: 150px; overflow-y: auto; overflow-x: hidden;">
                                <table id="dtlMilestones" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th style="width: 50px;">
                                                Milestone Assessed
                                            </th>
                                            <th style="width: 50px;">
                                                Date Assessed
                                            </th>
                                            <th style="width: 30px;">
                                                Achieved
                                            </th>
                                            <th style="width: 50px;">
                                                Status
                                            </th>
                                            <th style="width: 50px;">
                                                Comment
                                            </th>
                                            <th style="width: 10px;">
                                            </th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            <!-- /.box -->
                        </div>
                    </div>
                    <!-- /.row -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.box-body -->
        </div>
        <!-- /.box sub box level 1 -->
        <!-- / sub box level 2 -->
        <div class="box box-default box-solid">
            <div class="box-header with-border">
                <h3 class="box-title">
                    Immunization History</h3>
                <!-- /.box-tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Immunization Period:</label>
                                    <select class="form-control select2" data-placeholder="Select" id="ddlImmunizationPeriod"
                                        style="width: 100%;">
                                    </select>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Immunization Given:</label>
                                    <select class="form-control select2" data-placeholder="Select" id="ddlImmunizationGiven"
                                        style="width: 100%;">
                                    </select>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Date Immunized:</label>
                                    <input type="text" class="form-control" id="dtDateImmunized" placeholder="Date Assessed"
                                        data-date-format="dd-M-yyyy">
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        &nbsp;
                                    </label>
                                    <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                                        id="Button1" onclick="AddImmunization();">
                                        Add</button>
                                </div>
                            </div>
                            <!-- /.row -->
                            <div style="min-height: 10px; max-height: 150px; overflow-y: auto; overflow-x: hidden;">
                                <table id="dtlImmunizationHistory" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th style="width: 200px;">
                                                Immunization Period
                                            </th>
                                            <th style="width: 200px;">
                                                Immunization Given
                                            </th>
                                            <th style="width: 75px;">
                                                Date Immunized
                                            </th>
                                            <th style="width: 10px;">
                                            </th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Neonatal History Notes:</label>
                                    <textarea class="form-control" rows="6" placeholder="Neonatal History Notes" id="txtNeonatalNotes"></textarea>
                                </div>
                                <!-- /.box -->
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
        <!-- /.box-body -->
    </div>
    <!-- /.box sub box level G1 -->
</div>
<div id="div1" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Tanners Staging</h3>
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
                <div>
                    <div class="row">
                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                            </label>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Last screened on:</label>
                        </div>
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                            <input type="text" class="form-control" id="dtLastScreenedOn" placeholder="Last screened on"
                                data-date-format="dd-M-yyyy">
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Next screened on:</label>
                        </div>
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                            <input type="text" class="form-control" id="dtNextScreenedOn" placeholder="Next screened on"
                                data-date-format="dd-M-yyyy">
                        </div>
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Date:</label>
                            <input type="text" class="form-control" id="dtTSDate" placeholder="Date" data-date-format="dd-M-yyyy">
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" id="lblMOrF" class="control-label">
                                F Breasts:</label>
                            <select class="form-control select2" data-placeholder="Select" id="ddlMOrF" style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Public Hair:</label>
                            <select class="form-control select2" data-placeholder="Select" id="ddlPublicHair"
                                style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                &nbsp;
                            </label>
                            <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                                id="Button2" onclick="AddTannerStaging();">
                                Add</button>
                        </div>
                    </div>
                    <div style="min-height: 10px; max-height: 150px; overflow-y: auto; overflow-x: hidden;">
                        <table id="dtlTannerStaging" class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th style="width: 150px;">
                                        Date
                                    </th>
                                    <th style="width: 150px;">
                                        F Breasts
                                    </th>
                                    <th style="width: 150px;">
                                        Public Hair
                                    </th>
                                    <th style="width: 10px;">
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <!-- /.row -->
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.row -->
    </div>
    <!-- /.box sub box level G1 -->
</div>
<!-- /.box sub box level G1 -->
<div id="div2" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Drug History</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="background-color: #eeeeee;">
                        <h4 class="box-title">
                            Current long Term Medication</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Patient on any long term medication?</label>
                        <input id="chkLongTermMedication" name="switch-size" type="checkbox" checked data-size="small"
                            data-on-text="Yes" data-off-text="No">
                    </div>
                </div>
                <div id="divLongTermMedication" style="visibility: hidden; display: none;">
                    <div class="row">
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Drug:</label>
                            <select class="form-control select2" data-placeholder="Select" id="ddlDrugList" style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" id="Label1" class="control-label">
                                Start Date:</label>
                            <input type="text" class="form-control" id="dtDHStartDate" placeholder="Start Date"
                                data-date-format="dd-M-yyyy">
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Reason:</label>
                            <select class="form-control select2" data-placeholder="Select" id="ddlDHReason" style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                &nbsp;
                            </label>
                            <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                                id="Button3" onclick="AddLTM();">
                                Add</button>
                        </div>
                    </div>
                    <div style="min-height: 10px; max-height: 150px; overflow-y: auto; overflow-x: hidden;">
                        <table id="dtlDrugList" class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th style="width: 40%;">
                                        Drug
                                    </th>
                                    <th style="width: 15%;">
                                        Start Date
                                    </th>
                                    <th style="width: 40%;">
                                        Reason
                                    </th>
                                    <th style="width: 5%;">
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Has patient used any herbal or alternative medicine?</label>
                        <input id="chkAlternativeMedicine" name="switch-size" type="checkbox" checked data-size="small"
                            data-on-text="Yes" data-off-text="No">
                        </br> if yes explain </br>
                        <textarea class="form-control" rows="2" placeholder="Alternative Medicines" id="txtAlternativeMedicine"></textarea>
                    </div>
                </div>
                <!-- /.row -->
            </div>
        </div>
        <!-- /.row -->
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="background-color: #eeeeee;">
                    <h4 class="box-title">
                        Prior ART</h4>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                    <label for="inputEmail3" class="control-label">
                        Prior ART?</label>
                    <input id="chkPriorART" name="switch-size" type="checkbox" checked data-size="small"
                        data-on-text="Yes" data-off-text="No">
                </div>
            </div>
            <div id="divPriorART" style="visibility: hidden; display: none;">
                <div class="row">
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Purpose</label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlARTPurpose"
                            style="width: 100%;">
                        </select>
                    </div>
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Regimen:</label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlRegimen" style="width: 100%;">
                        </select>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" id="Label2" class="control-label">
                            Date Last Used:</label>
                        <input type="text" class="form-control" id="dtARTLDU" placeholder="Start Date" data-date-format="dd-M-yyyy">
                    </div>
                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            &nbsp;
                        </label>
                        <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                            id="Button4" onclick="AddPriorART();">
                            Add</button>
                    </div>
                </div>
                <div style="min-height: 10px; max-height: 150px; overflow-y: auto; overflow-x: hidden;">
                    <table id="dtlPriorART" class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <th style="width: 30%;">
                                    Purpose
                                </th>
                                <th style="width: 50%;">
                                    Regimen
                                </th>
                                <th style="width: 10%">
                                    Date Last Used
                                </th>
                                <th style="width: 10%;">
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.row -->
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="background-color: #eeeeee;">
                        <h4 class="box-title">
                            ART Start at Another Facility</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            *Start ART 1st Line Regimen Date:</label>
                        <input type="text" class="form-control" id="dt1stLineDate" placeholder="Start ART 1st Line Regimen Date:"
                            data-date-format="dd-M-yyyy">
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Regimen:</label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlARTStartRegimen"
                            style="width: 100%;">
                        </select>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Weight : Kgs
                        </label>
                        <input type="number" class="form-control" id="txtWeight" placeholder="Comment" />
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Height : cm
                        </label>
                        <input type="number" class="form-control" id="txtHeight" placeholder="Comment" />
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            BMI :
                        </label>
                        <input type="text" class="form-control" id="txtBMI" placeholder="Comment" />
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            WHO Stage :
                        </label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlARTWHOStage"
                            style="width: 100%;">
                        </select>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.row -->
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="background-color: #eeeeee;">
                        <h4 class="box-title">
                            Allergies</h4>
                    </div>
                </div>
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
                                id="Button5">
                                Add</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.row -->
    </div>
    <!-- /.row -->
</div>
<!-- /.box sub box level G1 -->
<div id="div3" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Family History</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Add family members tested for HIV?</label>
                        <input id="chkHIVFamilyMember" name="switch-size" type="checkbox" checked data-size="small"
                            data-on-text="Yes" data-off-text="No">
                    </div>
                </div>
                <div id="divHIVFamilyMember" style="visibility: hidden; display: none;">
                    <div class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Registered at this clinic:
                            </label>
                            <select class="form-control select2" data-placeholder="Select" id="ddlRegistered"
                                style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Relative First Name:
                            </label>
                            <input type="text" class="form-control" id="txtFirstName" placeholder="First Name" />
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Relative Last name :
                            </label>
                            <input type="text" class="form-control" id="txtLastName" placeholder="Last Name" />
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Sex
                            </label>
                            <select class="form-control select2" data-placeholder="Select" id="ddlSex" style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Age
                            </label>
                            <input type="number" class="form-control" id="txtAge" placeholder="Comment" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Relationship Type:
                            </label>
                            <select class="form-control select2" data-placeholder="Select" id="ddlRelationshipType"
                                style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                HIV Status:
                            </label>
                            <select class="form-control select2" data-placeholder="Select" id="ddlHIVStatus"
                                style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Last HIV Test Date:
                            </label>
                            <input type="text" class="form-control" id="dtLastHIVTestDate" placeholder="Last HIV Test Date"
                                data-date-format="dd-M-yyyy">
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                HIV Care Status:
                            </label>
                            <select class="form-control select2" data-placeholder="Select" id="ddlHIVCareStatus"
                                style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                &nbsp;
                            </label>
                            <button type="button" class="btn btn-block btn-primary btn-sm" style="width: 50px;"
                                id="btnAddFamilyMember">
                                Add</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /.row -->
</div>
<!-- /.box sub box level G1 -->
<div id="div4" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Sexual History</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Sexually Active in the past 6 months?</label>
                        <input id="chkSexuallyActive6Months" name="switch-size" type="checkbox" checked data-size="small"
                            data-on-text="Yes" data-off-text="No">
                    </div>
                </div>
                <div id="divSexuallyActive6Months" style="visibility: hidden; display: none;">
                    <div class="row">
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                # Partners in the past 6 Months
                            </label>
                            <input type="number" class="form-control" id="txtPartners" placeholder="Partners" />
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Partner's Status:
                            </label>
                            <select class="form-control select2" data-placeholder="Select" id="ddlPartnerStatus"
                                style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Partners Gender:
                            </label>
                            <select class="form-control select2" data-placeholder="Select" id="ddlPartnerGender"
                                style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                Sexual orientation:
                            </label>
                            <select class="form-control select2" data-placeholder="Select" id="ddlSexualOrientation"
                                style="width: 100%;">
                            </select>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                            <label for="inputEmail3" class="control-label">
                                High Risk Behaviour:
                            </label>
                            <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                                id="ddlHighRiskBehaviour" style="width: 100%;">
                            </select>
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- /.box-body -->
<!-- Button Footer -->
<div class="row no-print" align="center">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <button type="button" class="btn btn-primary">
                Previous<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
            <button type="button" class="btn btn-primary">
                Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
