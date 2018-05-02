
var totPHQ9 = 0;
var arrPHQ9 = [];
var arrCRAFFTScore = [];
var arrCAGEAIDScore = [];
$(document).ready(function () {

    $.hivce.loader('show');
    $("#hidId").val(0);

    $("textarea").maxlength({
        alwaysShow: false,
        threshold: 10,
        warningClass: "labelTextBox label label-success",
        limitReachedClass: "labelTextBox label label-danger",
        separator: ' out of ',
        preText: 'You write ',
        postText: ' characters.',
        validate: true,
        placement: 'top'
    });

    InitAlcoholDepressionScreeningControls();

});

function InitAlcoholDepressionScreeningControls() {
    $("#chkFeelingDown").bootstrapSwitch('state', false);
    $("#chkLittleInterest").bootstrapSwitch('state', false);

    $("#borderedTable").css("visibility", "hidden");
    $("#borderedTable").css("display", "none");

    $("#chkFeelingDown").on('switchChange.bootstrapSwitch', function (event, state) {
        displayPHQ();
    });
    $("#chkLittleInterest").on('switchChange.bootstrapSwitch', function (event, state) {
        displayPHQ();
    });

    $("#chkSGBV1").bootstrapSwitch('state', false);
    $("#chkSGBV2").bootstrapSwitch('state', false);
    $("#chkSGBV3").bootstrapSwitch('state', false);
    $("#chkSGBV4").bootstrapSwitch('state', false);
    $("#chkSGBV5").bootstrapSwitch('state', false);

    $("#chkDisclosedHIVStatus").bootstrapSwitch('state', false);

    $("#ddlDisclosureStatus").prop('disabled', true);
    $("#ddlDisclosedTo").prop('disabled', true);

    $("#chkDrinkAnyAlcohol").bootstrapSwitch('state', false);
    $("#chkSmokeMarijuana").bootstrapSwitch('state', false);
    $("#chkUsageGetHigh").bootstrapSwitch('state', false);
    $("#chkRiddenCarWhenHigh").bootstrapSwitch('state', false);
    $("#chkRelaxOption").bootstrapSwitch('state', false);
    $("#chkAloneOption").bootstrapSwitch('state', false);
    $("#chkForgotOption").bootstrapSwitch('state', false);
    $("#chkCutDown").bootstrapSwitch('state', false);
    $("#chkGotIntoTrouble").bootstrapSwitch('state', false);

    $("#chk3MonOpt1").bootstrapSwitch('state', false);
    $("#chk3MonOpt2").bootstrapSwitch('state', false);
    $("#chk3MonOpt3").bootstrapSwitch('state', false);
    $("#chk3MonOpt4").bootstrapSwitch('state', false);

    $("#chkTriedStopSmoking").bootstrapSwitch('state', false);



    $("#chkDrinkAnyAlcohol").on('switchChange.bootstrapSwitch', function (event, state) {
        /*var daa = GetSwitchValue("chkDrinkAnyAlcohol");
        var sm = GetSwitchValue("chkSmokeMarijuana");
        var ugh = GetSwitchValue("chkUsageGetHigh");
        if (daa == 1 || sm == 1 || ugh == 1) {
        $("#divCRAFFTOptions").css("visibility", "visible");
        $("#divCRAFFTOptions").css("display", "block");
        }
        else {
        $("#divCRAFFTOptions").css("visibility", "hidden");
        $("#divCRAFFTOptions").css("display", "none");
        }
        $("#chkRelaxOption").bootstrapSwitch('state', false);
        $("#chkAloneOption").bootstrapSwitch('state', false);
        $("#chkForgotOption").bootstrapSwitch('state', false);
        $("#chkCutDown").bootstrapSwitch('state', false);
        $("#chkGotIntoTrouble").bootstrapSwitch('state', false);
        */
        BindCRAFFTDiv();
    });


    $("#chkSmokeMarijuana").on('switchChange.bootstrapSwitch', function (event, state) {
        BindCRAFFTDiv();
    });


    $("#chkUsageGetHigh").on('switchChange.bootstrapSwitch', function (event, state) {
        BindCRAFFTDiv();
    });


    $("#chkRiddenCarWhenHigh").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateCRAFFTScore("rcwh", GetSwitchValue("chkRiddenCarWhenHigh"));
    });

    $("#chkRelaxOption").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateCRAFFTScore("ro", GetSwitchValue("chkRelaxOption"));
    });

    $("#chkAloneOption").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateCRAFFTScore("ao", GetSwitchValue("chkAloneOption"));
    });

    $("#chkForgotOption").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateCRAFFTScore("fo", GetSwitchValue("chkForgotOption"));
    });

    $("#chkCutDown").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateCRAFFTScore("cd", GetSwitchValue("chkCutDown"));
    });

    $("#chkGotIntoTrouble").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateCRAFFTScore("git", GetSwitchValue("chkGotIntoTrouble"));
    });

    $("#chk3MonOpt1").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateCAGEAIDScore("Opt1", GetSwitchValue("chk3MonOpt1"));
    });
    $("#chk3MonOpt2").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateCAGEAIDScore("Opt2", GetSwitchValue("chk3MonOpt2"));
    });
    $("#chk3MonOpt3").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateCAGEAIDScore("Opt3", GetSwitchValue("chk3MonOpt3"));
    });
    $("#chk3MonOpt4").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateCAGEAIDScore("Opt4", GetSwitchValue("chk3MonOpt4"));
    });

    $("#ddlDisclosureStatus").select2();


    $("#ddlDisclosedTo").select2();

    $('input:radio[name=rbSmoke]').on('ifChecked', function (event) {
        DisplayTriedSmokingDiv(this);
    });
    $('input:radio[name=rbSmoke]').on('ifUnchecked', function (event) {
        DisplayTriedSmokingDiv(this);
    });

    //    $("input:radio[name=rbSmoke]").click(function () {
    //        DisplayTriedSmokingDiv();
    //    });


    var dob = $("#hidDOB").val();
    if (dob >= 15 && dob <= 49) {
        $("#divSGBV").css("visibility", "visible");
        $("#divSGBV").css("display", "inline");
    }
    else {
        $("#divSGBV").css("visibility", "hidden");
        $("#divSGBV").css("display", "none");
    }

    if (dob < 14) {
        $("#divDisclosureStatus").css("visibility", "visible");
        $("#divDisclosureStatus").css("display", "inline");
    }
    else {
        $("#divDisclosureStatus").css("visibility", "hidden");
        $("#divDisclosureStatus").css("display", "none");
    }

    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });

    $('#divDCA input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });

    $('#divUDrugs input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });

    $('#divSmoke input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });





    BindCRAFFTDiv();
    GetDisclosureData();
}

function displayPHQ() {

    $("#borderedTable").css("visibility", "hidden");
    $("#borderedTable").css("display", "none");

    var chkFeelingDown = GetSwitchValue("chkFeelingDown");
    var chkLittleInterest = GetSwitchValue("chkLittleInterest");

    if (chkFeelingDown == 1 && chkLittleInterest == 1) {
        $("#borderedTable").css("visibility", "visible");
        $("#borderedTable").css("display", "block");
    }

}


function BindCRAFFTDiv() {
    var daa = GetSwitchValue("chkDrinkAnyAlcohol");
    var sm = GetSwitchValue("chkSmokeMarijuana");
    var ugh = GetSwitchValue("chkUsageGetHigh");
    if (daa == 1 || sm == 1 || ugh == 1) {
        $("#divCRAFFTOptions").css("visibility", "visible");
        $("#divCRAFFTOptions").css("display", "block");
    }
    else {
        $("#divCRAFFTOptions").css("visibility", "hidden");
        $("#divCRAFFTOptions").css("display", "none");
    }
    $("#chkRelaxOption").bootstrapSwitch('state', false);
    $("#chkAloneOption").bootstrapSwitch('state', false);
    $("#chkForgotOption").bootstrapSwitch('state', false);
    $("#chkCutDown").bootstrapSwitch('state', false);
    $("#chkGotIntoTrouble").bootstrapSwitch('state', false);
}
function CalculateCRAFFTScore(ref, val) {
    var chkData = $.grep(arrCRAFFTScore, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        arrCRAFFTScore.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrCRAFFTScore, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }
    var totCRAFFTScore = 0;

    $.each(arrCRAFFTScore, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            totCRAFFTScore += parseInt(arrD.Val);
        }
    });

    //totPHQ9 = parseInt(totPHQ9) + parseInt(val);
    $("#txtCRAFFTScore").val(totCRAFFTScore);

    if (parseInt(totCRAFFTScore) <= 2) {
        $("#txtCRAFFTRisk").val("Low Risk");
    }
    else if (parseInt(totCRAFFTScore) > 2) {
        $("#txtCRAFFTRisk").val("High Risk");
    }

}

function CalculateCAGEAIDScore(ref, val) {
    var chkData = $.grep(arrCAGEAIDScore, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        arrCAGEAIDScore.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrCAGEAIDScore, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }
    var totCAGEAIDScore = 0;

    $.each(arrCAGEAIDScore, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            totCAGEAIDScore += parseInt(arrD.Val);
        }
    });

    //totPHQ9 = parseInt(totPHQ9) + parseInt(val);
    $("#txtCAGEAIDScore").val(totCAGEAIDScore);

    if (parseInt(totCAGEAIDScore) <= 2) {
        $("#txtCAGEAID").val("Low Risk");
    }
    else if (parseInt(totCAGEAIDScore) > 2) {
        $("#txtCAGEAID").val("High Risk");
    }

}

function DisplayTriedSmokingDiv(ctrlName) {
    if (ctrlName.value <= 2) {
        $("#divTriedStopSmoking").css("visibility", "hidden");
        $("#divTriedStopSmoking").css("display", "none");

    }
    else {
        $("#divTriedStopSmoking").css("visibility", "visible");
        $("#divTriedStopSmoking").css("display", "block");
    }
}


function GetDisclosureData() {
    var postData;
    $.ajax({
        type: "POST",
        url: "AlcoholDepressionScreening.aspx?data=getdata",
        //data: JSON.stringify(nodeId),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        beforeSend: function () {
        },
        success: function (response) {
            console.log(response);
            var responseSuccess = response.Success;
            if (responseSuccess != null) {
                if (responseSuccess == "false") {
                    customAlert(errorMessage);
                }
            }
            else {
                BindDisclosure(response);
            }

        },
        error: function (msg) {
        }
    });
}

function BindDisclosure(response) {
    data = $.grep(response, function (e) { return e.CN == "DisclosureStatus"; });
    var vdata = [];
    $.each(data, function (index, value) {
        if (value.DN == 'Not done' || value.DN == 'Complete' || value.DN == 'Partial') {
            vdata.push({ id: value.DId, text: value.DN });
        }
    });

    $("#ddlDisclosureStatus").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlDisclosureStatus").select2("val", "0");
    $("#ddlDisclosureStatus").trigger('change.select2');
    $("#ddlDisclosureStatus").prop("disabled", false);

    data = $.grep(response, function (e) { return e.CN == "DisclosedHIVStatusTo"; });
    vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlDisclosedTo").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlDisclosedTo").select2("val", "0");
    $("#ddlDisclosedTo").trigger('change.select2');

    var age = $("#hidDOB").val();
    if (age >= 10 && age <= 19) {
        $("#divCRAFFTAlcoholDrugAbuse").css("visibility", "visible");
        $("#divCRAFFTAlcoholDrugAbuse").css("display", "block");
    }
    else {
        $("#divCRAFFTAlcoholDrugAbuse").css("visibility", "hidden");
        $("#divCRAFFTAlcoholDrugAbuse").css("display", "none");
    }
    if (age >= 20) {

        $("#divCAGEAIDScreening").css("visibility", "visible");
        $("#divCAGEAIDScreening").css("display", "block");
    }
    else {
        $("#divCAGEAIDScreening").css("visibility", "hidden");
        $("#divCAGEAIDScreening").css("display", "none");
    }


    GetADSData();

}

function GetADSData() {
    var postData;
    $.ajax({
        type: "POST",
        url: "AlcoholDepressionScreening.aspx?data=getadsdata",
        //data: JSON.stringify(nodeId),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        beforeSend: function () {
        },
        success: function (response) {
            console.log(response);
            if (response != null) {
                var responseSuccess = response.Success;
                if (responseSuccess != null) {
                    if (responseSuccess == "false") {
                        customAlert(errorMessage);
                    }
                }
                else {
                    BindADSData(response);
                }
            }
            else {
                $.hivce.loader('hide');
            }
        },
        error: function (msg) {
        }
    });
}

function BindADSData(response) {
    debugger;
    $("#hidId").val(response.Id);

    $("#divPHQLI").children("#" + response.PHQLI).removeClass("btn btn-default").addClass("btn btn-default active");
    CalculatePHQ9("PHQLI", response.PHQLI);
    $("#divPHQFD").children("#" + response.PHQFD).removeClass("btn btn-default").addClass("btn btn-default active");
    CalculatePHQ9("PHQFD", response.PHQFD);
    $("#divPHQTS").children("#" + response.PHQTS).removeClass("btn btn-default").addClass("btn btn-default active");
    CalculatePHQ9("PHQTS", response.PHQTS);
    $("#divPHQTLE").children("#" + response.PHQTLE).removeClass("btn btn-default").addClass("btn btn-default active");
    CalculatePHQ9("PHQTLE", response.PHQTLE);
    $("#divPHQA").children("#" + response.PHQA).removeClass("btn btn-default").addClass("btn btn-default active");
    CalculatePHQ9("PHQA", response.PHQA);
    $("#divPHQYD").children("#" + response.PHQYD).removeClass("btn btn-default").addClass("btn btn-default active");
    CalculatePHQ9("PHQYD", response.PHQYD);
    $("#divPHQTC").children("#" + response.PHQTC).removeClass("btn btn-default").addClass("btn btn-default active");
    CalculatePHQ9("PHQTC", response.PHQTC);
    $("#divPHQMS").children("#" + response.PHQMS).removeClass("btn btn-default").addClass("btn btn-default active");
    CalculatePHQ9("PHQMS", response.PHQMS);
    $("#divPHQFR").children("#" + response.PHQFR).removeClass("btn btn-default").addClass("btn btn-default active");
    CalculatePHQ9("PHQFR", response.PHQFR);
    $("#divPHQHY").children("#" + response.PHQHY).removeClass("btn btn-default").addClass("btn btn-default active");
    CalculatePHQ9("PHQHY", response.PHQHY);

    $("#chkFeelingDown").bootstrapSwitch('state', ConvertValueBool(response.ISFD));
    $("#chkLittleInterest").bootstrapSwitch('state', ConvertValueBool(response.ISLI));

    $("#chkSGBV1").bootstrapSwitch('state', ConvertValueBool(response.SGBQ1));
    $("#chkSGBV2").bootstrapSwitch('state', ConvertValueBool(response.SGBQ2));
    $("#chkSGBV3").bootstrapSwitch('state', ConvertValueBool(response.SGBQ3));
    $("#chkSGBV4").bootstrapSwitch('state', ConvertValueBool(response.SGBQ4));
    $("#chkSGBV5").bootstrapSwitch('state', ConvertValueBool(response.SGBQ5));
    $("#chkDisclosedHIVStatus").bootstrapSwitch('state', ConvertValueBool(response.DHIVS));
    $("#chkDrinkAnyAlcohol").bootstrapSwitch('state', ConvertValueBool(response.CAL));
    $("#chkSmokeMarijuana").bootstrapSwitch('state', ConvertValueBool(response.CS));
    $("#chkUsageGetHigh").bootstrapSwitch('state', ConvertValueBool(response.CAH));
    $("#chkRiddenCarWhenHigh").bootstrapSwitch('state', ConvertValueBool(response.CAC));
    $("#chkRelaxOption").bootstrapSwitch('state', ConvertValueBool(response.CAR));
    $("#chkAloneOption").bootstrapSwitch('state', ConvertValueBool(response.CAA));
    $("#chkForgotOption").bootstrapSwitch('state', ConvertValueBool(response.CAF));
    $("#chkCutDown").bootstrapSwitch('state', ConvertValueBool(response.CAFF));
    $("#chkGotIntoTrouble").bootstrapSwitch('state', ConvertValueBool(response.CAT));
    $("#chk3MonOpt1").bootstrapSwitch('state', ConvertValueBool(response.CAIDQ1));
    $("#chk3MonOpt2").bootstrapSwitch('state', ConvertValueBool(response.CAIDQ2));
    $("#chk3MonOpt3").bootstrapSwitch('state', ConvertValueBool(response.CAIDQ3));
    $("#chk3MonOpt4").bootstrapSwitch('state', ConvertValueBool(response.CAIDQ4));
    $("#chkTriedStopSmoking").bootstrapSwitch('state', ConvertValueBool(response.CAIDSS));

    $("#ddlDisclosureStatus").select2().val(response.DS).trigger("change");


    $("#ddlDisclosedTo").select2().val(response.DTO).trigger("change");
    $("#PHQ9Total").val(response.PHQT);

    $("#txtDepressionSeverity").val(response.PHQDS);
    $("#txtRecommendedManagement").val(response.PHQR);
    $("#txtCRAFFTScore").val(response.CSC); $("#txtCRAFFTRisk").val(response.CR);
    $("#txtCAGEAIDScore").val(response.CAIDSC); $("#txtCAGEAID").val(response.CAIDR);

    $("input:radio[name=rbDCA][value=" + response.CAIDA + "]").attr('checked', 'checked');
    $("input:radio[name=rbDCA]").iCheck('update');
    $("input:radio[name=rbUDrugs][value=" + response.CAIDD + "]").attr('checked', 'checked');
    $("input:radio[name=rbUDrugs]").iCheck('update');
    $("input:radio[name=rbSmoke][value=" + response.CAIDS + "]").attr('checked', 'checked');
    $("input:radio[name=rbSmoke]").iCheck('update');

    $("#txtNotes").val(response.Notes);

    displayPHQ();
    enableDisableDisclouserStatus();

    $.hivce.loader('hide');
}

function enableDisableDisclouserStatus() {
    $("#ddlDisclosureStatus").prop("disabled", false);
    var data = $("#ddlDisclosureStatus").select2('data')[0];
    if (data != undefined) {
        if (data.text == "Complete") {
            //$('#ddlDisclosureStatus').attr("disabled", true);
            $("#ddlDisclosureStatus").prop("disabled", true);
        }

    }
}

function ConvertValueBool(val) {
    var blnVal = false;
    if (val == 1) {
        blnVal = true;
    }
    return blnVal;
}

function CalculatePHQ9(ref, val) {
    var chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        arrPHQ9.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrPHQ9, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }
    totPHQ9 = 0;

    $.each(arrPHQ9, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            totPHQ9 += parseInt(arrD.Val);
        }
    });

    //totPHQ9 = parseInt(totPHQ9) + parseInt(val);
    $("#PHQ9Total").val(totPHQ9);
    //alert(ref + val);

    if (parseInt(totPHQ9) == 0 && parseInt(totPHQ9) <= 4) {
        $("#txtDepressionSeverity").val("None");
    }
    else if (parseInt(totPHQ9) >= 5 && parseInt(totPHQ9) <= 9) {
        $("#txtDepressionSeverity").val("Mild");
    }
    else if (parseInt(totPHQ9) >= 10 && parseInt(totPHQ9) <= 14) {
        $("#txtDepressionSeverity").val("Moderate");
    }
    else if (parseInt(totPHQ9) >= 15 && parseInt(totPHQ9) <= 19) {
        $("#txtDepressionSeverity").val("Moderately Severe");
    }
    else if (parseInt(totPHQ9) >= 10) {
        $("#txtDepressionSeverity").val("Severe");
    }

    if (parseInt(totPHQ9) == 0 && parseInt(totPHQ9) <= 4) {
        $("#txtRecommendedManagement").val("Repeat screening in future");
    }
    else if (parseInt(totPHQ9) >= 5 && parseInt(totPHQ9) <= 9) {
        $("#txtRecommendedManagement").val("Provide counselling support");
    }
    else if (parseInt(totPHQ9) >= 10) {
        $("#txtRecommendedManagement").val("Refer to psychiatrist, or mental health team");
    }

}


function PrepareAlcoholDepressionScreeningData() {
    rowData1 = [];
    var id = $("#hidId").val();
    var PHQLI = 0, PHQFD = 0, PHQTS = 0, PHQTLE = 0, PHQA = 0, PHQYD = 0, PHQTC = 0, PHQMS = 0, PHQFR = 0, PHQHY = 0, PHQT = 0, PHQDS = 0, PHQR = 0;

    var chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == "phqli"; });
    if (jQuery.isEmptyObject(chkData) == false) {
        PHQLI = chkData[0].Val;
    }

    chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == "phqfd"; });
    if (jQuery.isEmptyObject(chkData) == false) {
        PHQFD = chkData[0].Val;
    }

    chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == "phqts"; });
    if (jQuery.isEmptyObject(chkData) == false) {
        PHQTS = chkData[0].Val;
    }
    chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == "phqtle"; });
    if (jQuery.isEmptyObject(chkData) == false) {
        PHQTLE = chkData[0].Val;
    }
    chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == "phqa"; });
    if (jQuery.isEmptyObject(chkData) == false) {
        PHQA = chkData[0].Val;
    }
    chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == "phqyd"; });
    if (jQuery.isEmptyObject(chkData) == false) {
        PHQYD = chkData[0].Val;
    }
    chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == "phqtc"; });
    if (jQuery.isEmptyObject(chkData) == false) {
        PHQTC = chkData[0].Val;
    }
    chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == "phqms"; });
    if (jQuery.isEmptyObject(chkData) == false) {
        PHQMS = chkData[0].Val;
    }
    chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == "phqfr"; });
    if (jQuery.isEmptyObject(chkData) == false) {
        PHQFR = chkData[0].Val;
    }
    chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == "phqhy"; });
    if (jQuery.isEmptyObject(chkData) == false) {
        PHQHY = chkData[0].Val;
    }

    chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == "phqt"; });
    if (jQuery.isEmptyObject(chkData) == false) {
        PHQT = chkData[0].Val;
    }

    chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == "phqds"; });
    if (jQuery.isEmptyObject(chkData) == false) {
        PHQDS = chkData[0].Val;
    }

    chkData = $.grep(arrPHQ9, function (e) { return e.Ref.toLowerCase() == "phqr"; });
    if (jQuery.isEmptyObject(chkData) == false) {
        PHQR = chkData[0].Val;
    }

    var ddlDisclosureStatus = $("#ddlDisclosureStatus").select2("val");

    if (ddlDisclosureStatus == null) {
        ddlDisclosureStatus = 0;
    }

    var ddlDisclosedTo = $("#ddlDisclosedTo").select2("val");

    if (ddlDisclosedTo == null) {
        ddlDisclosedTo = 0;
    }


    var rbDCA = $("input:radio[name=rbDCA]:checked").val();
    var rbUDrugs = $("input:radio[name=rbUDrugs]:checked").val();
    var rbSmoke = $("input:radio[name=rbSmoke]:checked").val();

    var dca = (rbDCA === undefined) ? 0 : rbDCA;
    var udrugs = (rbUDrugs === undefined) ? 0 : rbUDrugs;
    var smoke = (rbSmoke === undefined) ? 0 : rbSmoke;

    rowData1.push({ Id: id,
        Ptn_Pk: 0,
        VId: 0,
        ISFD: GetSwitchValue("chkFeelingDown"),
        ISLI: GetSwitchValue("chkLittleInterest"),
        PHQLI: PHQLI, PHQFD: PHQFD, PHQTS: PHQTS, PHQTLE: PHQTLE, PHQA: PHQA, PHQYD: PHQYD,
        PHQTC: PHQTC, PHQMS: PHQMS, PHQFR: PHQFR, PHQHY: PHQHY,
        PHQT: $("#PHQ9Total").val(), PHQDS: $("#txtDepressionSeverity").val(), PHQR: $("#txtRecommendedManagement").val(),
        SGBQ1: GetSwitchValue("chkSGBV1"),
        SGBQ2: GetSwitchValue("chkSGBV2"),
        SGBQ3: GetSwitchValue("chkSGBV3"),
        SGBQ4: GetSwitchValue("chkSGBV4"),
        SGBQ5: GetSwitchValue("chkSGBV5"),
        DHIVS: GetSwitchValue("chkDisclosedHIVStatus"),
        DS: ddlDisclosureStatus,
        DTO: ddlDisclosedTo,
        CAL: GetSwitchValue("chkDrinkAnyAlcohol"),
        CS: GetSwitchValue("chkSmokeMarijuana"),
        CAH: GetSwitchValue("chkUsageGetHigh"),
        CAC: GetSwitchValue("chkRiddenCarWhenHigh"),
        CAR: GetSwitchValue("chkRelaxOption"),
        CAA: GetSwitchValue("chkAloneOption"),
        CAF: GetSwitchValue("chkForgotOption"),
        CAFF: GetSwitchValue("chkCutDown"),
        CAT: GetSwitchValue("chkGotIntoTrouble"),
        CSC: $("#txtCRAFFTScore").val(), CR: $("#txtCRAFFTRisk").val(),
        CAIDA: dca, CAIDD: udrugs, CAIDS: smoke,
        CAIDQ1: GetSwitchValue("chk3MonOpt1"),
        CAIDQ2: GetSwitchValue("chk3MonOpt2"),
        CAIDQ3: GetSwitchValue("chk3MonOpt3"),
        CAIDQ4: GetSwitchValue("chk3MonOpt4"),
        CAIDSC: $("#txtCAGEAIDScore").val(), CAIDR: $("#txtCAGEAID").val(),
        CAIDSS: GetSwitchValue("chkTriedStopSmoking"),
        Notes: $("#txtNotes").val()

    });

    return rowData1;
}

function SaveData() {

    $.hivce.loader('show');
    var rowData2 = PrepareAlcoholDepressionScreeningData();
    PostData(rowData2);
}

function CheckAlcoholDepressionBlankValues() {

    errorMsg = '';
    var errorField = '';
    //debugger;


    var age = $("#hidDOB").val();
    if (age < 14) {
        if ($("#chkDisclosedHIVStatus").bootstrapSwitch('state')) {
            var ddlDisclosureStatus = $("#ddlDisclosureStatus").select2("val");

            if (ddlDisclosureStatus == null) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'Disclosure Status ';
            }
        }
    }

    if (isNaN(errorField)) {
        errorMsg = errorField + 'cannot be blank.';
    }

    return errorMsg;

}


function PostData(rowData) {

    var action = "save";
    var rowData1 = rowData[0];

    var errorMsg = CheckAlcoholDepressionBlankValues();

    if (isNaN(errorMsg)) {
        customAlert(errorMsg);
        return;
    }
    else {
        var responseObject = null;
        $.ajax({
            type: "POST",
            url: "AlcoholDepressionScreening.aspx?data=" + action,
            data: JSON.stringify(rowData1),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            beforeSend: function () {
            },
            success: function (response) {
                //debugger;
                //console.log(response);
                var responseSuccess = response.Success;
                if (responseSuccess == "true") {
                    customAlert("Alcohol Depression Screening " + dataSuccessMessage.toLowerCase());
                }
                else {
                    if (responseSuccess == "false") {
                        customAlert(errorMessage);
                    }
                    else {
                        customAlert(responseSuccess);
                    }
                }

                $.hivce.loader('hide');
            },
            error: function (msg) {
                //ShowErrorMessage(msg);
            }
        });
    }
}

function ResetForm() {
    $.hivce.loader('show');
    debugger;
    GetADSData();

}