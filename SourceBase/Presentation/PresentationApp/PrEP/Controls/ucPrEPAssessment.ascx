<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPrEPAssessment.ascx.cs"
    Inherits="PresentationApp.PrEP.Controls.ucPrEPAssessment" %>
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
            Behaviour Risk Assessment</h3>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                <label for="inputEmail3" class="control-label">
                    Mark all that Apply</label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                <ul class="checktree">
                    <li>
                        <input id="1" type="checkbox" /><label for="1">Sex partner(s) is HIV+</label>
                        <ul>
                            <li>
                                <input id="101" type="checkbox" /><label for="101">Not on ART</label></li>
                            <li>
                                <input id="102" type="checkbox" /><label for="102">On ART <6 months</label></li>
                            <li>
                                <input id="103" type="checkbox" /><label for="103">Suspected poor adherence to ART</label></li>
                            <li>
                                <input id="104" type="checkbox" /><label for="104">Detectable HIV viral load</label></li>
                            <li>
                                <input id="105" type="checkbox" /><label for="105">Couple is trying to conceive</label></li>
                        </ul>
                    </li>
                    <li>
                        <input id="2" type="checkbox" /><label for="2">Sex partner(s) high risk & HIV status
                            is unknown</label></li>
                    <li>
                        <input id="3" type="checkbox" /><label for="3">Has sex with >1 partner</label></li>
                    <li>
                        <input id="4" type="checkbox" /><label for="4">Ongoing IPV/GBV</label></li>
                    <li>
                        <input id="5" type="checkbox" /><label for="5">Transactional sex</label></li>
                    <li>
                        <input id="6" type="checkbox" /><label for="6">>Recent STI (past 6 months)</label></li>
                    <li>
                        <input id="7" type="checkbox" /><label for="7">Recurrent use of post-exposure prophylaxis
                            (PEP)</label></li>
                    <li>
                        <input id="8" type="checkbox" /><label for="8">Recurrent sex under influence of alcohol/recreational
                            drugs</label></li>
                    <li>
                        <input id="9" type="checkbox" /><label for="9">Inconsistent or no condom use</label></li>
                    <li>
                        <input id="10" type="checkbox" /><label for="10">Injection drug use with shared needles
                            and/or syringes</label></li>
                </ul>
            </div>
        </div>
        <!-- /.row -->
    </div>
    <!-- /.box-body -->
</div>
<!-- Medical Assessment -->
<div class="box box-primary">
    <div class="box-header with-border">
        <h3 class="box-title">
            Medical Assessment</h3>
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
                        <div class="row">
                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                <label for="inputEmail3" class="control-label">
                                    Sign and Symptoms of STI:</label>
                                <input id="chkSign" name="switch-size" type="checkbox" checked data-size="small"
                                    data-on-text="Yes" data-off-text="No">
                            </div>
                            <div class="col-md-4 col-sm-12 col-xs-12 form-group" id="divCaseManager">
                                <select class="form-control select2" multiple="multiple" data-placeholder="Select"
                                    id="ddlTBAssessment" style="width: 90%">
                                    <option value="Genital Ulcer Disease(GUD)">Genital Ulcer Disease(GUD)</option>
                                    <option value="Vaginitis and/or Vaginal Discharge(VG)">Vaginitis and/or Vaginal Discharge(VG)</option>
                                    <option value="Cervicitis and/or Cervical Discharge(CD)">Cervicitis and/or Cervical
                                        Discharge(CD)</option>
                                    <option value="Pelvic Inflammatory Disease(PID)">Pelvic Inflammatory Disease(PID)</option>
                                    <option value="Urethral Discharge(UD)">Urethral Discharge(UD)</option>
                                    <option value="Anal Discharge(AD)">Anal Discharge(AD)</option>
                                    <option value="Others(O)">Others(O)</option>
                                    <option value="None">None</option>
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="box">
                                            <!-- /.box-header -->
                                            <div class="box-body">
                                                Notes:
                                                <textarea class="form-control" id="txtPreviousAdmissions" rows="3" placeholder="Notes"
                                                    maxlength="1000"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8 col-sm-12 col-xs-12 form-group">
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
                                    <label for="inputEmail3" class="control-label requiredFieldColor">
                                        Pregnant:</label>
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
                                        <input type="hidden" runat="server" id="hidDTEDD" clientidmode="Static" />
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
                            <div class="row" id="divANCProfileHead" style="visibility: hidden; display: none;">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <label class="control-label">
                                                        ANC Profile Done</label>
                                                </div>
                                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                                    <input id="chkANCProfileDone" name="switch-size" type="checkbox" checked data-size="small"
                                                        data-on-text="Yes" data-off-text="No" />
                                                </div>
                                            </div>
                                            <div id="divANCProfile">
                                                <div class="row">
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="dtVDRL" class="control-label">
                                                            VDRL:</label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="txtVDRLResult" class="control-label">
                                                            Result:</label>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <select class="form-control select2" data-placeholder="Select" id="ddlVDRLResult"
                                                            style="width: 100%;">
                                                        </select>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="txtVDRLResult" class="control-label">
                                                            Date:</label>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <input type="text" class="form-control" id="dtVDRL" data-date-format="dd-M-yyyy"
                                                                style="padding: 13px 12px;" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="dtHb" class="control-label">
                                                            Hb:</label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="HbResult" class="control-label">
                                                            Result:</label>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <input type="number" min="0" max="999" class="form-control" id="txtHbResult" placeholder="Hb Result">
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="HbResult" class="control-label">
                                                            Date:</label>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <input type="text" class="form-control" id="dtHb" data-date-format="dd-M-yyyy" style="padding: 13px 12px;">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="dtRhesus" class="control-label">
                                                            Rhesus:</label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="RhesusResult" class="control-label">
                                                            Result:</label>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <%--<input type="text" class="form-control" id="txtRhesusResult" placeholder="Rhesus Result">--%>
                                                        <select class="form-control select2" data-placeholder="Select" id="ddlRhesusResult"
                                                            style="width: 100%;">
                                                        </select>
                                                    </div>
                                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                                        <label for="RhesusResult" class="control-label">
                                                            Date:</label>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <input type="text" class="form-control" id="dtRhesus" data-date-format="dd-M-yyyy"
                                                                style="padding: 13px 12px;">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /.box-body -->
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
                        <div class="row" id="divCervicalCancer">
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
                        <!-- /.row -->
                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->
            </div>
        </div>
        <!-- /.row -->
    </div>
    <!-- Allergy  -->
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
    </div>
    <!-- /.box-body -->
    <!-- Button Footer -->
    <div class="row no-print" align="center">
        <div class="form-group">
            <div class="col-md-12" align="center">
                <button type="button" class="btn btn-primary" onclick="GetPreviousPage('tab_1');">
                    Previous<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
                <button type="button" class="btn btn-primary" onclick="SavePresentingComplaints();"
                    runat="server" id="btnSavePC" clientidmode="Static">
                    Next<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
            </div>
        </div>
    </div>
