<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSystemicReview.ascx.cs"
    Inherits="HIVCE.Presentation.Controls.ucSystemicReview" %>
<!-- / sub box level G1 -->
<div id="divGeneral" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Review of Systems and Physical Exam</h3>
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
            <%--<div class="box-header with-border">
                <h2 class="box-title">
                    General Examination</h2>
                <!-- /.box-tools -->
            </div>--%>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label requiredFieldColor">
                                        General Examination:</label>
                                </div>
                                <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                    <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                                        id="ddlGeneralExamination" style="width: 90%">
                                    </select>
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Skin:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <input id="chkSkin" name="switch-size" type="checkbox" checked data-size="small"
                                        data-on-text="Yes" data-off-text="No">
                                </div>
                                <div class="col-md-9 col-sm-12 col-xs-12 form-group" id="divSkin">
                                    <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                                        id="ddlSkin" style="width: 90%">
                                    </select>
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        ENT:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <input id="chkENT" name="switch-size" type="checkbox" checked data-size="small" data-on-text="Yes"
                                        data-off-text="No">
                                </div>
                                <div class="col-md-9 col-sm-12 col-xs-12 form-group" id="divENT">
                                    <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                                        id="ddlENT" style="width: 90%">
                                    </select>
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Chest:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <input id="chkChest" name="switch-size" type="checkbox" checked data-size="small"
                                        data-on-text="Yes" data-off-text="No">
                                </div>
                                <div class="col-md-9 col-sm-12 col-xs-12 form-group" id="divChest">
                                    <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                                        id="ddlChest" style="width: 90%">
                                    </select>
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        CVS:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <input id="chkCVS" name="switch-size" type="checkbox" checked data-size="small" data-on-text="Yes"
                                        data-off-text="No">
                                </div>
                                <div class="col-md-9 col-sm-12 col-xs-12 form-group" id="divCVS">
                                    <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                                        id="ddlCVS" style="width: 90%">
                                    </select>
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Abdomen:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <input id="chkAbdomen" name="switch-size" type="checkbox" checked data-size="small"
                                        data-on-text="Yes" data-off-text="No">
                                </div>
                                <div class="col-md-9 col-sm-12 col-xs-12 form-group" id="divAbdomen">
                                    <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                                        id="ddlAbdomen" style="width: 90%">
                                    </select>
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        CNS:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <input id="chkCNS" name="switch-size" type="checkbox" checked data-size="small" data-on-text="Yes"
                                        data-off-text="No">
                                </div>
                                <div class="col-md-9 col-sm-12 col-xs-12 form-group" id="divCNS">
                                    <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                                        id="ddlCNS" style="width: 90%">
                                    </select>
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Genito- Urinary:</label>
                                </div>
                                <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                    <input id="chkGenitoUrinary" name="switch-size" type="checkbox" checked data-size="small" data-on-text="Yes"
                                           data-off-text="No">
                                </div>
                                <div class="col-md-9 col-sm-12 col-xs-12 form-group" id="divGEU">
                                    <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                                            id="ddlGenitoUrinary" style="width: 90%">
                                    </select>
                                </div>
                            </div>
                            <!-- /.row -->
                            <!-- /.row -->
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Review of System Comments :</label>
                                    <textarea class="form-control" rows="4" placeholder="Review of System Comments and Other Systemic Finding" id="txtReviewSystemComments" maxlength="1000"></textarea>
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
<div id="divNutrition" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            Nutritional Assessment</h3>
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
            <%--<div class="box-header with-border">
                <h2 class="box-title">
                    General Examination</h2>
                <!-- /.box-tools -->
            </div>--%>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="div1">
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
                                            Height/Length(cm):</label>
                                    </div>
                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                        <input type="number" class="form-control" id="txtSRHeight" placeholder="Height/Length(cm)"
                                            disabled="disabled" />
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Weight(Kg):</label>
                                    </div>
                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                        <input type="number" class="form-control" id="txtSRWeight" placeholder="Weight" disabled="disabled">
                                    </div>
                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group" id="divSRBMILable">
                                        <label for="inputEmail3" class="control-label">
                                            BMI:</label>
                                    </div>
                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group" id="divSRBMIText">
                                        <input type="text" class="form-control" id="txtSRBMI" disabled="disabled">
                                        <label for="txtBMI" id="lblSRBMIClassification">
                                        </label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Head circumference(cm):</label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <input type="text" class="form-control" id="txtSRHeadCircumference" placeholder="Head circumference"
                                            disabled="disabled">
                                    </div>
                                </div>
                                <!-- /.row -->
                                <div class="row">
                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            MUAC(cm):</label>
                                    </div>
                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                        <input type="text" class="form-control" id="txtSRMUAC" placeholder="MUAC" disabled="disabled">
                                    </div>
                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group" id="divBMIZ1">
                                        <label for="inputEmail3" class="control-label">
                                            BMI Zscore</label>
                                    </div>
                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group" id="divBMIZV1">
                                        <input type="text" class="form-control" id="txtSRBMIZScore" disabled="disabled">
                                        <label for="txtBMIZScore" id="lblSRBMIzClassification">
                                        </label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group" id="divWA1">
                                        <label for="inputEmail3" class="control-label">
                                            Weight for age zscore:</label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group" id="divWAV1">
                                        <input type="text" class="form-control" id="txtSRWAgeZScore" disabled="disabled">
                                        <label for="txtWAgeZScore" id="lblSRWAClassification">
                                        </label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group" id="divHA1">
                                        <label for="inputEmail3" class="control-label">
                                            Height for age zscore:
                                        </label>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group" id="divHAV1">
                                        <input type="text" class="form-control" id="txtSRHAgeZScore" disabled="disabled">
                                        <label for="txtHAgeZScore" id="lblSRWHClassification">
                                        </label>
                                    </div>
                                    
                                </div>
                                <!-- /.row -->
                            </div>
                            <!-- /.box-body -->
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10 col-sm-12 col-xs-12 form-group" id="divRadioNutritionGroup">
                        <label class="control-label radio-inline">
                            <input type="radio" name="NutritionGroup" value="939" />
                            Normal</label>
                        <label class="control-label radio-inline">
                            <input type="radio" name="NutritionGroup" value="936" />
                            Severe Acute Malnutrition</label>
                        <label class="control-label radio-inline">
                            <input type="radio" name="NutritionGroup" value="937" />
                            Moderate Acute Malnutrition</label>
                        <label class="control-label radio-inline">
                            <input type="radio" name="NutritionGroup" value="941" />
                            OverWeight/Obese</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Nutritional Assessment Comments :</label>
                        <textarea class="form-control" rows="4" placeholder="Nutritional Assessment Comments"
                            id="txtAreaNutritionComment" maxlength="1000"></textarea>
                    </div>
                    <!-- /.box -->
                </div>
                <div id="divNutritionCounseling" class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <label for="inputEmail3" class="control-label">
                            Nutrition Counseling / Referral Provided?</label>
                        <label style="margin-right: 20px;">
                            <input type="radio" id="rdoNutritionCounseling1" name="rdoNutritionCounseling" class="flat-red" value="1" />
                            Yes
                        </label>
                        <label style="margin-right: 20px;">
                            <input type="radio" id="rdoNutritionCounseling2" name="rdoNutritionCounseling" class="flat-red" value="0" />
                            No
                        </label>
                    </div>
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.box sub box level 2 -->
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box sub box level G1 -->
</div>
<div id="divWHO" class="box box-default box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">
            WHO Staging</h3>
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
            <%--<div class="box-header with-border">
                <h2 class="box-title">
                    General Examination</h2>
                <!-- /.box-tools -->
            </div>--%>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#Wtab_S1" data-toggle="tab">Stage 1</a></li>
                                <li><a href="#Wtab_S2" data-toggle="tab">Stage 2</a></li>
                                <li><a href="#Wtab_S3" data-toggle="tab">Stage 3</a></li>
                                <li><a href="#Wtab_S4" data-toggle="tab">Stage 4</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane active" id="Wtab_S1">
                                    <div style="min-height: 10px; max-height: 250px; overflow-y: auto; overflow-x: hidden;">
                                        <table id="dtlStageI" class="table table-bordered table-striped">
                                            <thead>
                                                <tr>
                                                    <th style="width: 20px;">
                                                    </th>
                                                    <th style="width: 300px;">
                                                        WHO Stage I Conditions
                                                    </th>
                                                    <th style="width: 30px;">
                                                        Current
                                                    </th>
                                                    <th style="width: 50px;">
                                                        Historic
                                                    </th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                                <!-- /.tab-pane -->
                                <div class="tab-pane" id="Wtab_S2">
                                    <div style="min-height: 10px; max-height: 250px; overflow-y: auto; overflow-x: hidden;">
                                        <table id="dtlStageII" class="table table-bordered table-striped">
                                            <thead>
                                                <tr>
                                                    <th style="width: 20px;">
                                                    </th>
                                                    <th style="width: 300px;">
                                                        WHO Stage II Conditions
                                                    </th>
                                                    <th style="width: 30px;">
                                                        Current
                                                    </th>
                                                    <th style="width: 50px;">
                                                        Historic
                                                    </th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                                <!-- /.tab-pane -->
                                <div class="tab-pane" id="Wtab_S3">
                                    <div style="min-height: 10px; max-height: 250px; overflow-y: auto; overflow-x: hidden;">
                                        <table id="dtlStageIII" class="table table-bordered table-striped">
                                            <thead>
                                                <tr>
                                                    <th style="width: 20px;">
                                                    </th>
                                                    <th style="width: 300px;">
                                                        WHO Stage III Conditions
                                                    </th>
                                                    <th style="width: 30px;">
                                                        Current
                                                    </th>
                                                    <th style="width: 50px;">
                                                        Historic
                                                    </th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                                <!-- /.tab-pane -->
                                <div class="tab-pane" id="Wtab_S4">
                                    <div style="min-height: 10px; max-height: 250px; overflow-y: auto; overflow-x: hidden;">
                                        <table id="dtlStageIV" class="table table-bordered table-striped">
                                            <thead>
                                                <tr>
                                                    <th style="width: 20px;">
                                                    </th>
                                                    <th style="width: 300px;">
                                                        WHO Stage IV Conditions
                                                    </th>
                                                    <th style="width: 30px;">
                                                        Current
                                                    </th>
                                                    <th style="width: 50px;">
                                                        Historic
                                                    </th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                                <!-- /.tab-pane -->
                            </div>
                            <!-- /.tab-content -->
                        </div>
                        <!-- nav-tabs-custom -->
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                        <label id="lblCurrentWHOStaging" for="inputEmail3" class="control-label">
                            Current WHO Staging:&nbsp;&nbsp;</label>
                        <select class="form-control select2" data-placeholder="Select" id="ddlCurrentWhoStage"
                            style="width: 40%;" disabled>
                        </select>
                    </div>
                    <!-- /.box -->
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.box sub box level 2 -->
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box sub box level G1 -->
</div>
<!-- Button Footer -->
<div class="row no-print" align="center">
    <div class="form-group">
        <div class="col-md-12" align="center">
            <button type="button" class="btn btn-primary" onclick="GetPreviousPage('tab_4');">
                Previous<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
            <button type="button" class="btn btn-primary" onclick="SaveSystemicReview();" runat="server"
                id="btnSaveSystemicReview" clientidmode="Static">
                Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
        </div>
    </div>
</div>
