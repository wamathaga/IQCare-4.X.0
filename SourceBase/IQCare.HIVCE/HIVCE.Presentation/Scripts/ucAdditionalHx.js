
function InitAdditionalHxControls(response) {
    //Date picker
    $("#dtAssessed").datepicker({
        autoclose: true
    });
    $("#dtDateImmunized").datepicker({
        autoclose: true
    });
    $("#dtLastScreenedOn").datepicker({
        autoclose: true
    });
    $("#dtNextScreenedOn").datepicker({
        autoclose: true
    });
    $("#dtDHStartdate").datepicker({
        autoclose: true
    });
    $("#dtARTLDU").datepicker({
        autoclose: true
    });
    $("#dt1stLineDate").datepicker({
        autoclose: true
    });
    $("#dtDateOfAllergen").datepicker({
        autoclose: true
    });
    $("#dtLastHIVTestDate").datepicker({
        autoclose: true
    });
    $("#dtTSDate").datepicker({
        autoclose: true
    });
    $("#dtDHStartDate").datepicker({
        autoclose: true
    });



    $("#ddlMilestone").select2();
    $("#ddlStatus").select2();
    $("#ddlImmunizationPeriod").select2();
    $("#ddlImmunizationGiven").select2();
    $("#ddlMOrF").select2();
    $("#ddlPublicHair").select2();
    $("#ddlDrugList").select2();
    $("#ddlDHReason").select2();
    $("#ddlARTPurpose").select2();
    $("#ddlRegimen").select2();
    $("#ddlARTStartRegimen").select2();
    $("#ddlARTWHOStage").select2();
    $("#ddlAllergenType").select2();
    $("#ddlSeverity").select2();
    $("#ddlReactionType").select2();
    $("#ddlAllergen").select2();
    $("#ddlSex").select2();
    $("#ddlHIVStatus").select2();
    $("#ddlHIVCareStatus").select2();
    $("#ddlPartnerStatus").select2();
    $("#ddlPartnerGender").select2();
    $("#ddlSexualOrientation").select2();
    $("#ddlHighRiskBehaviour").select2();
    $("#ddlRelationshipType").select2();


    $("#chkAchieved").iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });

    $('#dtlMilestones').dataTable().fnDestroy();
    $('#dtlMilestones').DataTable({
        "aaData": arrMilestones,
        "bSort": false,
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "autoWidth": true,
        "aoColumns": [
        { bSortable: false },
         { bSortable: false },
         { bSortable: false },
         { bSortable: false },
         { bSortable: false },
         {
             bSortable: false,
             mRender: function (o) { return '<i class="ui-tooltip fa fa-trash-o" style="font-size: 22px;" data-original-title="Delete"></i>'; }
         }
    ]
    });

    $('#dtlImmunizationHistory').dataTable().fnDestroy();
    $('#dtlImmunizationHistory').DataTable({
        "aaData": arrImmunization,
        "bSort": false,
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "autoWidth": true,
        "aoColumns": [
         { bSortable: false },
         { bSortable: false },
         { bSortable: false },
         {
             bSortable: false,
             mRender: function (o) { return '<i class="ui-tooltip fa fa-trash-o" style="font-size: 22px;" data-original-title="Delete"></i>'; }
         }
    ]
    });

    $('#dtlTannerStaging').dataTable().fnDestroy();
    $('#dtlTannerStaging').DataTable({
        "aaData": arrTannerStaging,
        "bSort": false,
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "autoWidth": false,
        "aoColumns": [
         { bSortable: false },
         { bSortable: false },
         { bSortable: false },
         {
             bSortable: false,
             mRender: function (o) { return '<i class="ui-tooltip fa fa-trash-o" style="font-size: 22px;" data-original-title="Delete"></i>'; }
         }
    ]
    });

    var table = $("#dtlTannerStaging").DataTable();
    table.rows.add(arrTannerStaging).draw().nodes();

    $('#dtlDrugList').dataTable().fnDestroy();
    $('#dtlDrugList').DataTable({
        "aaData": arrTannerStaging,
        "bSort": false,
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "autoWidth": false,
        "aoColumns": [
         { bSortable: false },
         { bSortable: false },
         { bSortable: false },
         {
             bSortable: false,
             mRender: function (o) { return '<i class="ui-tooltip fa fa-trash-o" style="font-size: 22px;" data-original-title="Delete"></i>'; }
         }
    ]
    });


    $('#dtlPriorART').dataTable().fnDestroy();
    $('#dtlPriorART').DataTable({
        "aaData": arrTannerStaging,
        "bSort": false,
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "autoWidth": false,
        "aoColumns": [
         { bSortable: false },
         { bSortable: false },
         { bSortable: false },
         {
             bSortable: false,
             mRender: function (o) { return '<i class="ui-tooltip fa fa-trash-o" style="font-size: 22px;" data-original-title="Delete"></i>'; }
         }
    ]
    });
    $("#chkLongTermMedication").bootstrapSwitch('state', false);
    $("#chkAlternativeMedicine").bootstrapSwitch('state', false);
    $("#chkPriorART").bootstrapSwitch('state', false);
    $("#chkDrugAllergies").bootstrapSwitch('state', false);
    $("#chkHIVFamilyMember").bootstrapSwitch('state', false);

    $("#chkSexuallyActive6Months").bootstrapSwitch('state', false);

    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });


    BindAddHxCombo(response);
}


function BindAddHxCombo(response) {
    var data = [];

    $.each(response.MS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlMilestone").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlMilestone").select2("val", "0");
    $("#ddlMilestone").trigger('change.select2');

    data = [];
    $.each(response.IS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlStatus").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlStatus").select2("val", "0");
    $("#ddlStatus").trigger('change.select2');

    data = [];
    $.each(response.IP, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlImmunizationPeriod").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlImmunizationPeriod").select2("val", "0");
    $("#ddlImmunizationPeriod").trigger('change.select2');

    data = [];
    $.each(response.IG, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlImmunizationGiven").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlImmunizationGiven").select2("val", "0");
    $("#ddlImmunizationGiven").trigger('change.select2');

    data = [];
    $.each(response.LTML, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlDrugList").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlDrugList").select2("val", "0");
    $("#ddlDrugList").trigger('change.select2');

    data = [];
    $.each(response.PARTP, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlARTPurpose").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlARTPurpose").select2("val", "0");
    $("#ddlARTPurpose").trigger('change.select2');

    data = [];
    $.each(response.WHOS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlARTWHOStage").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlARTWHOStage").select2("val", "0");
    $("#ddlARTWHOStage").trigger('change.select2');

    data = [];
    $.each(response.Severity, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlSeverity").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlSeverity").select2("val", "0");
    $("#ddlSeverity").trigger('change.select2');

    data = [];
    $.each(response.AT, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlAllergenType").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlAllergenType").select2("val", "0");
    //$("#ddlAllergenType").trigger('change.select2');

    data = [];
    $.each(response.HIVS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlHIVStatus").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlHIVStatus").select2("val", "0");
    $("#ddlHIVStatus").trigger('change.select2');

    data = [];
    $.each(response.Gender, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlSex").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlSex").select2("val", "0");
    $("#ddlSex").trigger('change.select2');

    data = [];
    $.each(response.SO, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlSexualOrientation").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlSexualOrientation").select2("val", "0");
    $("#ddlSexualOrientation").trigger('change.select2');



    data = [];
    $.each(response.PHIVS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlPartnerStatus").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlPartnerStatus").select2("val", "0");
    $("#ddlPartnerStatus").trigger('change.select2');


    data = [];
    $.each(response.HS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlHighRiskBehaviour").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlHighRiskBehaviour").select2("val", "0");
    $("#ddlHighRiskBehaviour").trigger('change.select2');


    data = [];
    $.each(response.Allergen, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlRegimen").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlRegimen").select2("val", "0");
    $("#ddlRegimen").trigger('change.select2');

    $("#ddlARTStartRegimen").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlARTStartRegimen").select2("val", "0");
    $("#ddlARTStartRegimen").trigger('change.select2');

    data = [];
    $.each(response.HIVCS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlHIVCareStatus").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlHIVCareStatus").select2("val", "0");
    $("#ddlHIVCareStatus").trigger('change.select2');


    data = [];
    $.each(response.RT, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlRelationshipType").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlRelationshipType").select2("val", "0");
    $("#ddlRelationshipType").trigger('change.select2');


    data = [];
    $.each(response.Gender, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlPartnerGender").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlPartnerGender").select2("val", "0");
    $("#ddlPartnerGender").trigger('change.select2');


    data = [];
    $.each([1, 2, 3, 4], function (index, value) {
        data.push({ id: value, text: value });
    });

    $("#ddlMOrF").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlMOrF").select2("val", "0");
    $("#ddlMOrF").trigger('change.select2');

    $("#ddlPublicHair").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlPublicHair").select2("val", "0");
    $("#ddlPublicHair").trigger('change.select2');

    data = [];
    $.each(response.LTMR, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlDHReason").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlDHReason").select2("val", "0");
    $("#ddlDHReason").trigger('change.select2');

    $.each(response.Allergen, function (index, value) {
        drugAllergen.push({ id: value.Id, text: value.NM });
    });
    $.each(response.ENVA, function (index, value) {
        envAllergen.push({ id: value.Id, text: value.NM });
    });
    $.each(response.FOODA, function (index, value) {
        foodAllergen.push({ id: value.Id, text: value.NM });
    });

    $("#ddlAllergenType").select2().on('change', function () {
        BindAllergiesAllergen($(this).val());
    }).trigger('change');


    $.hivce.loader('hide');
}
function BindAllergiesAllergen(val) {
    var data = [];
    data.push({ id: "", text: "" });
    var selectList = $("#ddlAllergen");
    selectList.empty();
    $("#txtAllergenOther").val("");


    $("#ddlAllergen").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve'
    });
    $("#ddlAllergen").select2("val", "0");
    $("#ddlAllergen").trigger('change.select2');

    if (val == 207 || val == null) {
        $("#divtxtAller").css("visibility", "hidden");
        $("#divtxtAller").css("display", "none");
        $("#divddlAller").css("visibility", "visible");
        $("#divddlAller").css("display", "block");

        $("#ddlAllergen").select2({
            placeholder: {
                id: '0', // the value of the option
                text: 'Select an option'
            },
            allowClear: true,
            width: 'resolve',
            data: drugAllergen
        });
        $("#ddlAllergen").select2("val", "0");
        $("#ddlAllergen").trigger('change.select2');
    }
    else if (val == 208) {
        $("#divtxtAller").css("visibility", "hidden");
        $("#divtxtAller").css("display", "none");
        $("#divddlAller").css("visibility", "visible");
        $("#divddlAller").css("display", "block");

        $("#ddlAllergen").select2({
            placeholder: {
                id: '0', // the value of the option
                text: 'Select an option'
            },
            allowClear: true,
            width: 'resolve',
            data: envAllergen
        });
        $("#ddlAllergen").select2("val", "0");
        $("#ddlAllergen").trigger('change.select2');
    }
    else if (val == 209) {
        $("#divtxtAller").css("visibility", "hidden");
        $("#divtxtAller").css("display", "none");
        $("#divddlAller").css("visibility", "visible");
        $("#divddlAller").css("display", "block");

        $("#ddlAllergen").select2({
            placeholder: {
                id: '0', // the value of the option
                text: 'Select an option'
            },
            allowClear: true,
            width: 'resolve',
            data: foodAllergen
        });
        $("#ddlAllergen").select2("val", "0");
        $("#ddlAllergen").trigger('change.select2');
    }
    else if (val == 211) {
        $("#divtxtAller").css("visibility", "visible");
        $("#divtxtAller").css("display", "block");
        $("#divddlAller").css("visibility", "hidden");
        $("#divddlAller").css("display", "none");

    }
    else {
        $("#divtxtAller").css("visibility", "hidden");
        $("#divtxtAller").css("display", "none");
        $("#divddlAller").css("visibility", "visible");
        $("#divddlAller").css("display", "block");

        $("#ddlAllergen").select2({
            placeholder: {
                id: '0', // the value of the option
                text: 'Select an option'
            },
            allowClear: true,
            width: 'resolve',
            data: drugAllergen
        });
        $("#ddlAllergen").select2("val", "0");
        $("#ddlAllergen").trigger('change.select2');
    }
}

function AddMilestones() {
    var ddlPC = $("#ddlMilestone").select2("val");
    var data = $("#ddlMilestone").select2('data')[0];
    var dtAssessed = GetDateData("dtAssessed", true);
    var chkAchieved = $("#chkAchieved").bootstrapSwitch('state');
    var data1 = $("#ddlStatus").select2('data')[0];

    var txtComment = $("#txtComment").val();


    if (ddlPC == null) {
        customAlert("Please select presenting complaint from the list");
        return;
    }

    if ($("#dtAssessed").val().length == 0) {
        customAlert("Please enter date assessed");
        return;
    }

    var chkData = $.grep(arrMilestones, function (e) { return e.MA == data.id; });
    arrMilestonesUI = [];
    if (jQuery.isEmptyObject(chkData) == true) {
        arrMilestones.push({ MA: data.id, MAT: data.text, DA: dtAssessed, IsA: chkAchieved, ST: data1.id, STA: data1.text, CM: txtComment });
        arrMilestonesUI.push([data.text, $("#dtAssessed").val(), chkAchieved, data1.text, txtComment]);
    }
    else {
        customAlert("Already exits in the table.");
    }
    DrawDataTable("dtlMilestones", arrMilestonesUI);
}

function AddImmunization() {
    var ddlIP = $("#ddlImmunizationPeriod").select2("val");
    var data = $("#ddlImmunizationPeriod").select2('data')[0];
    var ddlIG = $("#ddlImmunizationGiven").select2("val");
    var data1 = $("#ddlImmunizationGiven").select2('data')[0];

    var dtDateImmunized = GetDateData("dtDateImmunized", true);
    var errorField = '';

    if (ddlIP == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Immunization period ';
    }

    if (ddlIG == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Immunization given ';
    }

    if ($("#dtDateImmunized").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Immunization date ';
    }

    if (isNaN(errorField)) {
        customAlert(errorField + 'cannot be blank.');
        return;
    }

    var chkData = $.grep(arrImmunization, function (e) { return e.IP == data.id; });
    arrImmunizationUI = [];
    if (jQuery.isEmptyObject(chkData) == true) {
        arrImmunization.push({ IP: data.id, IPT: data.text, IG: data1.id, IGT: data1.text, ID: dtDateImmunized });
        arrImmunizationUI.push([data.text, data1.text, $("#dtDateImmunized").val()]);
    }
    else {
        customAlert("Already exits in the table.");
    }
    DrawDataTable("dtlImmunizationHistory", arrImmunizationUI);

}

function AddTannerStaging() {
    var ddlMOrF = $("#ddlMOrF").select2("val");
    var data = $("#ddlMOrF").select2('data')[0];
    var ddlPublicHair = $("#ddlPublicHair").select2("val");
    var data1 = $("#ddlPublicHair").select2('data')[0];

    var dtTSDate = GetDateData("dtTSDate", true);
    var errorField = '';

    if (ddlMOrF == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'F Breasts ';
    }

    if (ddlPublicHair == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Public hair ';
    }

    if ($("#dtTSDate").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Tanners staging date ';
    }

    if (isNaN(errorField)) {
        customAlert(errorField + 'cannot be blank.');
        return;
    }

    var chkData = $.grep(arrTannerStaging, function (e) { return e.TSD == dtTSDate; });
    arrUI = [];
    if (jQuery.isEmptyObject(chkData) == true) {
        arrTannerStaging.push({ TSD: dtTSDate, MOrF: data.id, MOrFT: data.text, PH: data1.id, PHT: data1.text });
        arrUI.push([$("#dtTSDate").val(), data.text, data1.text]);
    }
    else {
        customAlert("Already exits in the table.");
    }
    DrawDataTable("dtlTannerStaging", arrUI);

}

function AddLTM() {
    var ddlDrugList = $("#ddlDrugList").select2("val");
    var data = $("#ddlDrugList").select2('data')[0];
    var ddlDHReason = $("#ddlDHReason").select2("val");
    var data1 = $("#ddlDHReason").select2('data')[0];

    var dtDHStartDate = GetDateData("dtDHStartDate", true);
    var errorField = '';

    if (ddlDrugList == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Drug ';
    }

    if (ddlDHReason == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Reason ';
    }

    if ($("#dtDHStartDate").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Start date ';
    }

    if (isNaN(errorField)) {
        customAlert(errorField + 'cannot be blank.');
        return;
    }

    var chkData = $.grep(arrLTM, function (e) { return e.TSD == dtTSDate; });
    arrUI = [];
    if (jQuery.isEmptyObject(chkData) == true) {
        arrLTM.push({ DL: data.id, DLT: data.text, TSD: dtTSDate, RES: data1.id, REST: data1.text });
        arrUI.push([data.text, $("#dtDHStartDate").val(), data1.text]);
    }
    else {
        customAlert("Already exits in the table.");
    }
    DrawDataTable("dtlDrugList", arrUI);

}

function AddPriorART() {
    var ddlARTPurpose = $("#ddlARTPurpose").select2("val");
    var data = $("#ddlARTPurpose").select2('data')[0];
    var ddlRegimen = $("#ddlRegimen").select2("val");
    var data1 = $("#ddlRegimen").select2('data')[0];

    var dtARTLDU = GetDateData("dtARTLDU", true);
    var errorField = '';

    if (ddlARTPurpose == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Prior ART purpose ';
    }

    if (ddlRegimen == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Regimen ';
    }

    if ($("#dtARTLDU").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Last date used';
    }

    if (isNaN(errorField)) {
        customAlert(errorField + 'cannot be blank.');
        return;
    }

    var chkData = $.grep(arrPriorART, function (e) { return e.PP == data.id; });
    arrUI = [];
    if (jQuery.isEmptyObject(chkData) == true) {
        arrPriorART.push({ PP: data.id, PPT: data.text, RES: data1.id, REST: data1.text, LDU: dtTSDate });
        arrUI.push([data.text, data1.text, $("#dtARTLDU").val()]);
    }
    else {
        customAlert("Already exits in the table.");
    }
    DrawDataTable("dtlPriorART", arrUI);

}