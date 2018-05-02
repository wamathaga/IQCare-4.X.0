var arrPresentingComplaints = [];
var arrPresentingComplaintsUI = [];
var arrChronicDisorders = [];
var arrChronicDisordersUI = [];
var chkStillActive;


function InitPresentingComplaintsControls(response) {
    //Date picker
    $("#dtOnsetDate").datepicker({
        autoclose: true
    });
    $("#dtCCOnsetDate").datepicker({
        autoclose: true
    });
    $("#dtLMP").datepicker({
        autoclose: true
    });
    $("#dtEDD").datepicker({
        autoclose: true
    });
    $("#dtOtherDates").datepicker({
        autoclose: true
    });
    $("#dtDateScreened").datepicker({
        autoclose: true
    });

    $("#ddlPresentingComplaints").select2();
    $("#ddlChronicDisorder").select2();
    $("#ddlReasonAmenorrhoea").select2();
    $("#ddlPregnant").select2();
    $("#ddlFPM").select2();
    $("#ddlNoFPReason").select2();

    BindPCCombo(response);

    $('#dtlPresentingComplaints').dataTable().fnDestroy();
    arrPresentingComplaints = [];
    $('#dtlPresentingComplaints').DataTable({
        "aaData": arrPresentingComplaintsUI,
        "bSort": false,
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "autoWidth": true,
        "aoColumns": [
         { bSortable: false },
         { bSortable: false },
         {
             bSortable: false,
             mRender: function (o) { return '<i class="ui-tooltip fa fa-trash-o" style="font-size: 22px;" data-original-title="Delete"></i>'; }
         }
    ]
    });

    $('#dtlChronicDisorders').dataTable().fnDestroy();
    arrChronicDisorders = [];
    $('#dtlChronicDisorders').DataTable({
        "aaData": arrChronicDisordersUI,
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

    /*$('#dtlChronicDisorders').DataTable({
    "aaData": arrChronicDisordersUI,
    "bSort": false,
    "bPaginate": false,
    "bFilter": false,
    "bInfo": false,
    "autoWidth": true,
    columns: [
    { data: null, render: function (data, type, row) {
    // Combine the first and last names into a single table field
    return data.first_name + ' ' + data.last_name;
    } 
    },
    { data: "position" },
    { data: "office" },
    { data: "extn" },
    { data: "start_date" },
    { data: "salary", render: $.fn.dataTable.render.number(',', '.', 0, '$') },
    {
    data: null,
    className: "center",
    defaultContent: '<a href="" class="editor_edit">Edit</a> / <a href="" class="editor_remove">Delete</a>'
    }
    ]
    });*/



    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });

    $('#chkStillActive').on('ifChecked', function (event) {
        chkStillActive = "true";
    });
    $('#chkStillActive').on('ifUnchecked', function (event) {
        chkStillActive = "false";
    });

    $("#chkStillActive").iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });

}

function BindPCCombo(response) {
    var data = [];

    $.each(response.PC, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlPresentingComplaints").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlPresentingComplaints").select2("val", "0");
    $("#ddlPresentingComplaints").trigger('change.select2');

    data = [];
    $.each(response.CC, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlChronicDisorder").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlChronicDisorder").select2("val", "0");
    $("#ddlChronicDisorder").trigger('change.select2');

    data = [];
    $.each(response.RFA, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlReasonAmenorrhoea").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlReasonAmenorrhoea").select2("val", "0");
    $("#ddlReasonAmenorrhoea").trigger('change.select2');

    data = [];
    $.each(response.PS, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlPregnant").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlPregnant").select2("val", "0");
    $("#ddlPregnant").trigger('change.select2');

    data = [];
    $.each(response.FPM, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlFPM").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlFPM").select2("val", "0");
    $("#ddlFPM").trigger('change.select2');

    data = [];
    $.each(response.RNFP, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlNoFPReason").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlNoFPReason").select2("val", "0");
    $("#ddlNoFPReason").trigger('change.select2');

    $("#chkComplaints").bootstrapSwitch('state', false);
    $("#chkChronicDisorders").bootstrapSwitch('state', false);
    $("#chkBreastFeeding").bootstrapSwitch('state', false);
    $("#chkOnFamilyPlanning").bootstrapSwitch('state', false);
    $("#chkCCS").bootstrapSwitch('state', false);
    $("#chkHPVVaccine").bootstrapSwitch('state', false);

    BindPCData(response);



}

function BindPCData(response) {
    $.each(response.PCT, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            var data = $.grep(response.PC, function (e) { return e.DId == arrD.ValId; });
            if (jQuery.isEmptyObject(data) == false) {
                // parse JSON formatted date to javascript date object
                var date = new Date(parseInt(arrD.OSD.substr(6)));

                // format display date (e.g. 04/10/2012)
                var displayDate = $.format.date(date, "dd-M-yyyy");

                arrPresentingComplaints.push({ PCID: arrD.PCID, PC: data[0].DN, OSD: displayDate });

                //arrPresentingComplaintsUI.push([data[0].DN, displayDate]);
            }
        }
    });
    BindDataTable(arrPresentingComplaints, "divPresentingComplaints", "chkComplaints", "dtlPresentingComplaints");

    $.each(response.CDT, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            var data = $.grep(response.CC, function (e) { return e.DId == arrD.ValId; });
            if (jQuery.isEmptyObject(data) == false) {
                // parse JSON formatted date to javascript date object
                var date = new Date(parseInt(arrD.OSD.substr(6)));
                // format display date (e.g. 04/10/2012)
                var displayDate = $.format.date(date, "dd-M-yyyy");

                var IsA = 0;
                var IsAVal = "false";
                if (arrD.NF == "1") {
                    IsA = 1;
                    IsAVal = "true";
                }
                arrChronicDisorders.push({ PCID: arrD.PCID, PC: data[0].DN, OSD: displayDate, IsActive: IsA });

                arrChronicDisordersUI.push([data[0].DN, displayDate, IsAVal]);
            }
        }
    });
    BindDataTable(arrChronicDisordersUI, "divChronicDisorders", "chkChronicDisorders", "dtlChronicDisorders");


    $("#ddlReasonAmenorrhoea").select2().val(response.OBSGYN.Amenorrhoea).trigger("change");
    $("#ddlPregnant").select2().val(response.OBSGYN.Pregnant).trigger("change");
    $("#ddlFPM").select2().val(response.OBSGYN.FPM).trigger("change");
    $("#ddlNoFPReason").select2().val(response.OBSGYN.NFP).trigger("change");

    $("#txtAgeAtMenarche").val(response.OBSGYN.Menarche);
    $("#txtParity").val(response.OBSGYN.Parity);
    $("#txtGravidae").val(response.OBSGYN.Gravidae);
    if (response.OBSGYN.HPVV == 1) {
        $("#chkHPVVaccine").bootstrapSwitch('state', true);
    }
    if (response.OBSGYN.BS == 1) {
        $("#chkBreastFeeding").bootstrapSwitch('state', true);
    }
    if (response.OBSGYN.CCS == 1) {
        $("#chkCCS").bootstrapSwitch('state', true);
    }
    if (response.OBSGYN.FPS == 1) {
        $("#chkOnFamilyPlanning").bootstrapSwitch('state', true);
    }

    CheckDatenAssign(response.OBSGYN.EDD, "dtEDD", false);

    CheckDatenAssign(response.OBSGYN.LMP, "dtLMP", false);

    CheckDatenAssign(response.OBSGYN.CCSD, "dtDateScreened", false);

    var age = $("#hidDOB").val();
    var gender = $("#hidGender").val();

    if (gender == "Female") {
        if (age < 12) {
            $("#divOBSGYN").css("visibility", "hidden");
            $("#divOBSGYN").css("display", "none");
        }
    }

    $.hivce.loader('hide');
}

function BindDataTable(dtRows, disDiv, switchName, dtTable) {

    if (dtRows.length > 0) {
        $("#" + switchName).bootstrapSwitch('state', true);
        var ctrlVal = $("#" + switchName).bootstrapSwitch('state');
        $("#" + disDiv).removeAttr("style");
        if (ctrlVal) {
            $("#" + disDiv).css("visibility", "visible");
            $("#" + disDiv).css("display", "block");
        }
        else {
            $("#" + disDiv).css("visibility", "hidden");
            $("#" + disDiv).css("display", "none");
        }
        DrawDataTable(dtTable, dtRows);
    }
}

function AddPresentingComplaints() {
    var ddlPC = $("#ddlPresentingComplaints").select2("val");
    var data = $("#ddlPresentingComplaints").select2('data')[0];
    var dtOnsetDate = GetDateData("dtOnsetDate", true);

    if (ddlPC == null) {
        customAlert("Please select presenting complaint from the list");
        return;
    }

    if ($("#dtOnsetDate").val().length == 0) {
        customAlert("Please enter on set date");
        return;
    }

    var chkData = $.grep(arrPresentingComplaints, function (e) { return e.PCID == data.id; });
    arrPresentingComplaintsUI = [];
    if (jQuery.isEmptyObject(chkData) == true) {
        arrPresentingComplaints.push({ PCID: data.id, PC: data.text, OSD: dtOnsetDate });
        arrPresentingComplaintsUI.push([data.text, $("#dtOnsetDate").val()]);
    }
    else {
        customAlert("Already exits in the table.");
    }
    DrawDataTable("dtlPresentingComplaints", arrPresentingComplaintsUI);
}

function AddChronicDisorder() {
    var ddlPC = $("#ddlChronicDisorder").select2("val");
    var data = $("#ddlChronicDisorder").select2('data')[0];
    var dtOnsetDate = GetDateData("dtCCOnsetDate", true);


    if (ddlPC == null) {
        customAlert("Please select chronic disorder from the list");
        return;
    }

    if ($("#dtCCOnsetDate").val().length == 0) {
        customAlert("Please enter on set date");
        return;
    }

    var chkData = $.grep(arrChronicDisorders, function (e) { return e.PCID == data.id; });
    arrChronicDisordersUI = [];
    if (jQuery.isEmptyObject(chkData) == true) {
        var IsA = 0;
        var IsAVal = "false";
        if (chkStillActive == true) {
            IsA = 1;
            IsAVal = "true";
        }
        arrChronicDisorders.push({ PCID: data.id, PC: data.text, OSD: dtOnsetDate, IsActive: IsA });
        arrChronicDisordersUI.push([data.text, $("#dtCCOnsetDate").val(), IsAVal]);
    }
    else {
        customAlert("Already exits in the table.");
    }

    DrawDataTable("dtlChronicDisorders", arrChronicDisordersUI);
}

function SavePresentingComplaints() {
    var action = "savepc";
    var rowData1 = PrepareSaveData();
    var errorMsg = CheckBlankValues();
    if (isNaN(errorMsg)) {
        customAlert(errorMsg);
        return;
    }
    else {
        $.hivce.loader('show');
        var responseObject = null;
        $.ajax({
            type: "POST",
            url: "ClinicalEncounter.aspx?data=" + action,
            data: JSON.stringify(rowData1),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            beforeSend: function () {
            },
            success: function (response) {
                debugger;
                //console.log(response);
                var response = response.Success;
                if (response == "true") {
                    customAlert("Presenting complaints form " + dataSuccessMessage.toLowerCase());
                }
                else {
                    if (response == "false") {
                        customAlert(errorMessage);
                    }
                    else {
                        customAlert(response);
                        GetPresentingComplaintsData();
                    }
                }

                //$.eva.loader('hide');
            },
            error: function (msg) {
                //ShowErrorMessage(msg);
            }
        });
    }

}

function CheckBlankValues() {
    var ddlCtrl = $("#ddlVisitType").select2("val");
    errorMsg = '';
    var errorField = '';

    //    if (ddlCtrl == null) {
    //        errorField += 'Visit type ';
    //    }

    //    if (isNaN(errorField)) {
    //        errorMsg = errorField + 'cannot be blank.';
    //    }
    return errorMsg;
}

function PrepareSaveData() {
    var localdtformat = true;
    //alert(GetDateData("dtVisitDate", localdtformat));
    rowData = [];
    rowData.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        OBSGYN: GetOBSGYNData(),
        PCT: GetPCTData(),
        CDT: GetCDTData()
    });

    console.log(rowData);
    return rowData[0];
}

function GetOBSGYNData() {


    var ddlReasonAmenorrhoea = $("#ddlReasonAmenorrhoea").select2("val");

    if (ddlReasonAmenorrhoea == null) {
        ddlReasonAmenorrhoea = 0;
    }

    var ddlPregnant = $("#ddlPregnant").select2("val");

    if (ddlPregnant == null) {
        ddlPregnant = 0;
    }

    var ddlNoFPReason = $("#ddlNoFPReason").select2("val");

    if (ddlNoFPReason == null) {
        ddlNoFPReason = 0;
    }

    var ddlFPM = $("#ddlFPM").select2("val");

    if (ddlFPM == null) {
        ddlFPM = 0;
    }

    rowData1 = [];
    rowData1.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        Menarche: $("#txtAgeAtMenarche").val(),
        HPVV: GetSwitchValue("chkHPVVaccine"),
        Amenorrhoea: ddlReasonAmenorrhoea,
        BS: GetSwitchValue("chkBreastFeeding"),
        CCS: GetSwitchValue("chkCCS"),
        DOD: null,
        DOIA: null,
        DOM: null,
        EDD: GetDateData("dtEDD", true),
        FPM: ddlFPM,
        FPS: GetSwitchValue("chkOnFamilyPlanning"),
        Gravidae: $("#txtGravidae").val(),
        LMP: GetDateData("dtLMP", true),
        NFP: ddlNoFPReason,
        Parity: $("#txtParity").val(),
        Pregnant: ddlPregnant,
        CCSD: GetDateData("dtDateScreened", true)
    });
    return rowData1[0];
}

function GetPCTData() {
    rowData1 = [];
    if (jQuery.isEmptyObject(arrPresentingComplaints) == false) {
        $.each(arrPresentingComplaints, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                rowData1.push({ Ptn_Pk: 0, VId: 0, ValId: arrD.PCID, FN: "PresentingComplaints", OSD: arrD.OSD });
            }
        });
    }
    return rowData1;
}

function GetCDTData() {
    rowData1 = [];
    if (jQuery.isEmptyObject(arrChronicDisorders) == false) {
        $.each(arrChronicDisorders, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                rowData1.push({ Ptn_Pk: 0, VId: 0, ValId: arrD.PCID, FN: "ChronicCondition", OSD: arrD.OSD, NF: arrD.IsActive });
            }
        });
    }
    return rowData1;
}