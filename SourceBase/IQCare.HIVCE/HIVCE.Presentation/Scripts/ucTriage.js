
function InitTriageControls(response) {
    //Date picker
    $("#dtVisitDate").datepicker({
        autoclose: true
    });
    $("#dtHivConfirm").datepicker({
        autoclose: true
    });
    $("#dtHivEnroll").datepicker({
        autoclose: true
    });
    $("#dtTransferDate").datepicker({
        autoclose: true
    });

    $("#ddlVisitType").select2();
    $("#ddlCareGiven").select2();
    $("#ddlDistrict").select2();
    $("#ddlFacility").select2();
    BindTriageCombo(response);
}

function BindTriageCombo(response) {
    var data = [];

    $.each(response.VT, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlVisitType").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlVisitType").select2("val", "0");
    $("#ddlVisitType").trigger('change.select2');

    data = [];
    $.each(response.CRS, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlCareGiven").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlCareGiven").select2("val", "0");
    $("#ddlCareGiven").trigger('change.select2');


    data = [];
    $.each(response.DL, function (index, value) {
        data.push({ id: value.Id, text: value.Name });
    });

    $("#ddlDistrict").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlDistrict").select2("val", "0");
    $("#ddlDistrict").trigger('change.select2');

    data = [];
    $.each(response.FL, function (index, value) {
        data.push({ id: value.Id, text: value.Name });
    });

    $("#ddlFacility").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlFacility").select2("val", "0");
    $("#ddlFacility").trigger('change.select2');

    $.hivce.loader('hide');
}

function CalcualteBMI() {
    var height = $("#txtHeight").val();
    var weight = $("#txtWeight").val();
    var txtBMI = $("#txtBMI").val();
    //var weight = document.getElementById(txtWeight).value;
    //var height = document.getElementById(txtHeight).value;
    if (weight == "" || height == "") {
        weight = 0;
        height = 0;
        $("#txtBMI").val("")
        //document.getElementById(txtBMI).value = "";
    }
    else {
        var BMI = weight / ((height / 100) * (height / 100));
        BMI = BMI.toFixed(2);
        //document.getElementById(txtBMI).value = BMI;
        $("#txtBMI").val(BMI)

    }
}

function CheckBlankValues() {
    var ddlCtrl = $("#ddlVisitType").select2("val");
    errorMsg = '';
    var errorField = '';

    if (ddlCtrl == null) {
        errorField += 'Visit type ';
    }

    if (isNaN(errorField)) {
        errorMsg = errorField + 'cannot be blank.';
    }
    return errorMsg;
}

function SaveTriageData() {

    var action = "savetriage";
    var rowData1 = PrepareSaveData();
    var errorMsg = CheckBlankValues();
    if (isNaN(errorMsg)) {
        customAlert(errorMsg);
        return;
    }
    else {
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
                    customAlert("Triage form " + dataSuccessMessage.toLowerCase());
                }
                else {
                    if (response == "false") {
                        customAlert(errorMessage);
                    }
                    else {
                        customAlert(response);
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


function PrepareSaveData() {
    var localdtformat = true;
    //alert(GetDateData("dtVisitDate", localdtformat));
    rowData = [];
    rowData.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        HIV: GetHIVData(localdtformat),
        IE: GetIEData(),
        OV: GetOVData(localdtformat),
        PV: GetPVData()
    });

    console.log(rowData);
    return rowData[0];
}

function GetHIVData(localdtformat) {

    var ddlFac = $("#ddlFacility").select2("val");

    if (ddlFac == null) {
        ddlFac = 0;
    }

    var ddlDis = $("#ddlDistrict").select2("val");

    if (ddlDis == null) {
        ddlDis = 0;
    }

    rowData1 = [];
    rowData1.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        HIVED: GetDateData("dtHivEnroll", localdtformat),
        HIVD: GetDateData("dtHivConfirm", localdtformat),
        HIVCW: $("#txtHIVPositiveWhere").val(),
        ARTTD: GetDateData("dtTransferDate", localdtformat),
        ARTTF: ddlFac,
        DIST: ddlDis,
        UID: 0,
        ARTSD: GetDateData("dtDateStatedArt", localdtformat),
        TRAN: GetSwitchValue("chkTransferIn"),
        HIVCPD: GetDateData("dtHivConfirm", localdtformat)
    });
    return rowData1[0];
}

function GetIEData() {
    rowData2 = [];

    var ddlDis = $("#ddlCareGiven").select2("val");

    if (ddlDis == null) {
        ddlDis = 0;
    }

    rowData2.push({
        Id: 0,
        Ptn_Pk: 0,
        VID: 0,
        LId: 0,
        HIVSG: GetSwitchValue("chkSupportGroup"),
        HIVSGM: $("#txtSupportGroupName").val(),
        ACG: GetSwitchValue("chkCareGiver"),
        CGR: ddlDis,
        Menarche: ""
    });
    return rowData2[0];
}

function GetOVData(localdtformat) {
    rowData3 = [];
    var ddlCtrl = $("#ddlVisitType").select2("val");

    if (ddlCtrl == null) {
        ddlCtrl = 0;
    }
    rowData3.push({
        VId: 0, Ptn_Pk: 0, LId: 0,
        VD: GetDateData("dtVisitDate", localdtformat),
        VTId: ddlCtrl
    });
    return rowData3[0];
}

function GetPVData() {
    rowData3 = [];
    rowData3.push({
        VId: 0,
        BPD: $("#txtMM").val(),
        BPS: $("#txtHG").val(),
        TEMP: $("#txtTemperature").val(),
        RR: $("#txtRR").val(),
        HR: $("#txtHRate").val(),
        HC: $("#txtHeadCircumference").val(),
        Height: $("#txtHeight").val(),
        Weight: $("#txtWeight").val(),
        MUAC: $("#txtMUAC").val(),
        WFA: $("#txtWAgeZScore").val(),
        WFH: $("#txtHAgeZScore").val(),
        BMIz: $("#txtBMIZScore").val(),
        NC: $("#txtNursesComment").val()
    });
    return rowData3[0];
}



