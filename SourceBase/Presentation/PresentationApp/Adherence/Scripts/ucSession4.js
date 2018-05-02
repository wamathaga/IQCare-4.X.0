var arrMMAS4Score4 = [];
var arrMMAS8Score4 = [];

function Init_UC_Session4_Controls(response) {
    debugger;
    window.scrollTo(0, 0);

    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableAdherenceBarriersDiv(this);
    });

    $("#chkCaseManager4").bootstrapSwitch('state', false);
    $("#dtFilledDate4").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    $("#chkForgotMed4").bootstrapSwitch('state', false);
    $("#chkCarelessMed4").bootstrapSwitch('state', false);
    $("#chkWorseTakingMed4").bootstrapSwitch('state', false);
    $("#chkFeelBetterMed4").bootstrapSwitch('state', false);
    $("#chkYesterdayMed4").bootstrapSwitch('state', false);
    $("#chkSymptomUnderControl4").bootstrapSwitch('state', false);
    $("#chkStickingTreatmentPlan4").bootstrapSwitch('state', false);

    $("#chkForgotMed4").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score4("fm", GetSwitchValue("chkForgotMed4"));
    });
    $("#chkCarelessMed4").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score4("cm", GetSwitchValue("chkCarelessMed4"));
    });
    $("#chkWorseTakingMed4").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score4("wtm", GetSwitchValue("chkWorseTakingMed4"));
    });
    $("#chkFeelBetterMed4").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score4("fbm", GetSwitchValue("chkFeelBetterMed4"));
    });

    $("#chkYesterdayMed4").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS8Score4("ym", GetSwitchValue("chkYesterdayMed4"));
    });
    $("#chkSymptomUnderControl4").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS8Score4("suc", GetSwitchValue("chkSymptomUnderControl4"));
    });
    $("#chkStickingTreatmentPlan4").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS8Score4("stp", GetSwitchValue("chkStickingTreatmentPlan4"));
    });

    $("#chkDiscussViralLoadResult4").bootstrapSwitch('state', false);
    $("#chkSocialEconomicBarrier4").bootstrapSwitch('state', false);
    $("#chkReferredOtherServices4").bootstrapSwitch('state', false);
    $("#chkAttendAppointment4").bootstrapSwitch('state', false);

    $("#chkBenefitHomeVisit4").bootstrapSwitch('state', false);
    $("#dtFollowupDate4").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    BindSession4Data(response);
}


function BindSession4Data(response) {
    debugger;
    var data;
    var age = $("#hidDOB").val();

    $("#divMrgRM4").empty();
    data = $.grep(response.CD, function (e) { return e.CN == "RememberingMedications"; });
    data = SortArray(data);

    var vdata = [];
    var strIPT = '';
    $.each(data, function (index, value) {
        strIPT += "<label style=\"margin-right: 20px;\">";
        strIPT += "     <input type=\"radio\" id=\"rb" + value.DId + "\" name=\"rbRM4\" class=\"flat-red\" value=\"" + value.DId + "\" />&nbsp;";
        strIPT += value.DN;
        strIPT += "</label>";
    });
    $("#divMrgRM4").append(strIPT);
    $('#divMrgRM4 input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });
    $('input:radio[name=rbRM4]').on('ifChecked', function (event) {
        $(this).closest("input").attr('checked', true);
        CalculateRBMMAS8Score4(this);
    });
    $('input:radio[name=rbRM4]').on('ifUnchecked', function (event) {
        $(this).closest("input").attr('checked', false);
        CalculateRBMMAS8Score4(this);
    });

    CheckDatenAssign(response.FFD, "dtFilledDate4", false);
    CheckDatenAssign(response.FUD, "dtFollowupDate4", false);
    BindSession4Controls(response);
}


function BindSession4Controls(response) {
    debugger;

    if (jQuery.isEmptyObject(response.ACM) == false) {
        if (response.ACM > 0) {
            $("#chkCaseManager4").bootstrapSwitch('state', true);
        }
    }

    $("#txtAdherence4").val(response.APER);

    CheckDatenAssign(response.FFD, "dtFilledDate4", false);

    if (jQuery.isEmptyObject(response.ISFM) == false) {
        if (response.ISFM > 0) {
            $("#chkForgotMed4").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISCM) == false) {
        if (response.ISCM > 0) {
            $("#chkCarelessMed4").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISWTM) == false) {
        if (response.ISWTM > 0) {
            $("#chkWorseTakingMed4").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISFBM) == false) {
        if (response.ISFBM > 0) {
            $("#chkFeelBetterMed4").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISYM) == false) {
        if (response.ISYM > 0) {
            $("#chkYesterdayMed4").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISSUC) == false) {
        if (response.ISSUC > 0) {
            $("#chkSymptomUnderControl4").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISSTP) == false) {
        if (response.ISSTP > 0) {
            $("#chkStickingTreatmentPlan4").bootstrapSwitch('state', true);
        }
    }

    $("input:radio[name=rbRM4][value=" + response.RM + "]").attr('checked', 'checked');
    $("input:radio[name=rbRM4]").iCheck('update');

    $("#txtMMAS4Score4").val(response.MMAS4S);
    $("#txtMMAS4Rating4").val(response.MMAS4R);
    $("#txtMMAS8Score4").val(response.MMAS8S);
    $("#txtMMAS8Rating4").val(response.MMAS8R);
    $("#txtMMAS8Suggestion4").val(response.MMAS8SG);

    if (jQuery.isEmptyObject(response.PUVL) == false) {
        if (response.PUVL > 0) {
            $("#chkDiscussViralLoadResult4").bootstrapSwitch('state', true);
        }
    }
    $("#txtAreaRoutineReminder4").val(response.PDR);

    $("#chkReferredOtherServices4").bootstrapSwitch('state', ConvertValueBool(response.ROS));
    $("#chkAttendAppointment4").bootstrapSwitch('state', ConvertValueBool(response.AA));
    $("#txtAreaExperience4").val(response.EAR);

    if (jQuery.isEmptyObject(response.BHV) == false) {
        if (response.BHV > 0) {
            $("#chkBenefitHomeVisit4").bootstrapSwitch('state', true);
        }
    }
    $("#txtAreaAdherence4").val(response.APLAN);
    CheckDatenAssign(response.FUD, "dtFollowupDate4", false);

    $.hivce.loader('hide');
}

function SaveSession4() {
    debugger;
    $.hivce.loader('show');
    var session4Data = PrepareSession4Data();
    PostSession4Data(session4Data);
}

function PrepareSession4Data() {
    debugger;
    rowSession4Data = [];

    var chkCaseManager = GetSwitchValue("chkCaseManager4");
    var txtAdherence = $("#txtAdherence4").val();
    var dtFilledDate = GetDateData("dtFilledDate4", true);

    var chkForgotMed = GetSwitchValue("chkForgotMed4");
    var chkCarelessMed = GetSwitchValue("chkCarelessMed4");
    var chkWorseTakingMed = GetSwitchValue("chkWorseTakingMed4");
    var chkFeelBetterMed = GetSwitchValue("chkFeelBetterMed4");
    var txtMMAS4Score = $("#txtMMAS4Score4").val();
    var txtMMAS4Rating = $("#txtMMAS4Rating4").val();
    var chkYesterdayMed = GetSwitchValue("chkYesterdayMed4");
    var chkSymptomUnderControl = GetSwitchValue("chkSymptomUnderControl4");
    var chkStickingTreatmentPlan = GetSwitchValue("chkStickingTreatmentPlan4");
    var rbRM4 = $("input:radio[name=rbRM4]:checked").val();
    var urbRM = (rbRM4 === undefined) ? 0 : rbRM4;
    var txtMMAS8Score = $("#txtMMAS8Score4").val();
    var txtMMAS8Rating = $("#txtMMAS8Rating4").val();
    var txtMMAS8Suggestion = $("#txtMMAS8Suggestion4").val();

    var chkDiscussViralLoadResult = GetSwitchValue("chkDiscussViralLoadResult4");
    var chkSocialEconomicBarrier = GetSwitchValue("chkSocialEconomicBarrier4");
    var txtAreaRoutineReminder = $("#txtAreaRoutineReminder4").val();
    var chkReferredOtherServices = GetSwitchValue("chkReferredOtherServices4");
    var chkAttendAppointment = GetSwitchValue("chkAttendAppointment4");
    var txtAreaExperience = $("#txtAreaExperience4").val();
    var chkBenefitHomeVisit = GetSwitchValue("chkBenefitHomeVisit4");
    var txtAreaAdherence = $("#txtAreaAdherence4").val();
    var dtFollowupDate = GetDateData("dtFollowupDate4", true);

    rowSession4Data.push({
        Ptn_Pk: 0,
        LId: 0,
        VId: 0,

        ACM: chkCaseManager,
        CS: 4,
        FFD: dtFilledDate,
        APER: txtAdherence,

        ISFM: chkForgotMed,
        ISCM: chkCarelessMed,
        ISWTM: chkWorseTakingMed,
        ISFBM: chkFeelBetterMed,
        MMAS4S: txtMMAS4Score,
        MMAS4R: txtMMAS4Rating,
        ISYM: chkYesterdayMed,
        ISSUC: chkSymptomUnderControl,
        ISSTP: chkStickingTreatmentPlan,
        RM: urbRM,
        MMAS8S: txtMMAS8Score,
        MMAS8R: txtMMAS8Rating,
        MMAS8SG: txtMMAS8Suggestion,

        PUVL: chkDiscussViralLoadResult,
        SS: chkSocialEconomicBarrier,
        PDR: txtAreaRoutineReminder,

        ROS: chkReferredOtherServices,
        AA: chkAttendAppointment,
        EAR: txtAreaExperience,
        BHV: chkBenefitHomeVisit,
        APLAN: txtAreaAdherence,
        FUD: dtFollowupDate
    });

    return rowSession4Data[0];
}

function PostSession4Data(rowData) {
    debugger;
    var action = "saveSession4";
    var rowData1 = rowData;
    var errorMsg = CheckMXBlankValues();

    if (isNaN(errorMsg)) {
        customAlert(errorMsg);
        return;
    }
    else {
        // $.hivce.loader('show');
        var responseObject = null;
        $.ajax({
            type: "POST",
            url: "EnhanceAdherence.aspx?data=" + action,
            data: JSON.stringify(rowData1),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            beforeSend: function () {
            },
            success: function (response) {
                debugger;
                console.log(response);
                var responseSuccess = response.Success;
                var msg = response.EM;
                if (responseSuccess == "true") {
                    //GetSession4Data();
                    customAlert("Session 4 tab " + dataSuccessMessage.toLowerCase());
                    GetNextPage("tab_5");
                }
                else {
                    if (responseSuccess == "false") {
                        customAlert(msg);
                    }
                    else {
                        customAlert(responseSuccess);
                    }
                }
                $.hivce.loader('hide');
            },
            error: function (xhr, textStatus, errorThrown) {
                //ShowErrorMessage(msg);
                console.log(xhr);
                console.log(textStatus);
                console.log(errorThrown);
            }
            //            error: function (msg) {
            //                ShowErrorMessage(msg);
            //            }
        });
    }
}


function CalculateMMAS4Score4(ref, val) {
    debugger;
    var chkData = $.grep(arrMMAS4Score4, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        arrMMAS4Score4.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrMMAS4Score4, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }
    CalculateMMSImpl4();

}

function CalculateMMAS8Score4(ref, val) {
    debugger;
    var chkData = $.grep(arrMMAS8Score4, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        if (ref == "ym") {
            if (val == 1) {
                val = 0;
            }
            else {
                val = 1;
            }
        }
        arrMMAS8Score4.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrMMAS8Score4, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }

    CalculateMMSImpl4();

}

function CalculateRBMMAS8Score4(ctrlName) {
    debugger;
    var val = 0;
    var ref = "rm";

    if (ctrlName.parentNode.parentNode.innerText.trim().toLowerCase() == "never / rarely") {
        val = 0;
    }
    else if (ctrlName.parentNode.parentNode.innerText.trim().toLowerCase() == "once in a while") {
        val = .25;
    }
    else if (ctrlName.parentNode.parentNode.innerText.trim().toLowerCase() == "sometimes") {
        val = .50;
    }
    else if (ctrlName.parentNode.parentNode.innerText.trim().toLowerCase() == "usually") {
        val = .75;
    }
    else if (ctrlName.parentNode.parentNode.innerText.trim().toLowerCase() == "all the time") {
        val = 1;
    }

    var chkData = $.grep(arrMMAS8Score4, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        arrMMAS8Score4.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrMMAS8Score4, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }

    CalculateMMSImpl4();
}

function CalculateMMSImpl4() {
    debugger;
    var totMMAS8Score = 0.0;

    $.each(arrMMAS8Score4, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            totMMAS8Score += parseFloat(arrD.Val);
        }
    });

    var totMMAS4Score = 0;

    $.each(arrMMAS4Score4, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            totMMAS4Score += parseInt(arrD.Val);
        }
    });

    $("#txtMMAS4Score4").val(totMMAS4Score);

    if (parseInt(totMMAS4Score) == 0) {
        $("#txtMMAS4Rating4").val("Good");
        $("#divOtherQesAdherence4").css("visibility", "hidden");
        $("#divOtherQesAdherence4").css("display", "none");
    }
    else if (parseInt(totMMAS4Score) >= 1 && parseInt(totMMAS4Score) <= 2) {
        $("#txtMMAS4Rating4").val("Inadequate");
        $("#divOtherQesAdherence4").css("visibility", "visible");
        $("#divOtherQesAdherence4").css("display", "inline");
    }
    else if (parseInt(totMMAS4Score) >= 3 && parseInt(totMMAS4Score) <= 4) {
        $("#txtMMAS4Rating4").val("Poor");
        $("#divOtherQesAdherence4").css("visibility", "visible");
        $("#divOtherQesAdherence4").css("display", "inline");
    }

    if (totMMAS4Score > 0) {
        totMMAS8Score = parseFloat(totMMAS8Score) + parseFloat(totMMAS4Score);

        $("#txtMMAS8Score4").val(totMMAS8Score);

        if (parseInt(totMMAS8Score) == 0) {
            $("#txtMMAS8Rating4").val("Good");
            $("#txtMMAS8Suggestion4").val("");
        }
        else if (parseInt(totMMAS8Score) >= 1 && parseInt(totMMAS8Score) <= 2) {
            $("#txtMMAS8Rating4").val("Inadequate");
            $("#txtMMAS8Suggestion4").val("");
        }
        else if (parseInt(totMMAS8Score) >= 3 && parseInt(totMMAS8Score) <= 4) {
            $("#txtMMAS8Rating4").val("Poor");
            $("#txtMMAS8Suggestion4").val("Refer to Counselor");
        }
    }
    else {
        $("#txtMMAS8Score4").val("0");
        $("#txtMMAS8Rating4").val("Good");
        $("#txtMMAS8Suggestion4").val("");
    }
}

function CheckMXBlankValues() {
    errorMsg = '';
    var errorField = '';
    //    var ddlCTXAdhere = $("#ddlCTXAdhere").select2("val");
    //    var ddlARVAdhere = $("#ddlARVAdhere").select2("val");
    //    var ddlPWP = $("#ddlPWP").select2("val");

    //    var visitType = $("#ddlVisitType").select2("val");
    //    var visitTypeData = $("#ddlVisitType").select2('data')[0];


    //    if (visitType != null) {
    //        if (visitTypeData.text.toUpperCase().indexOf("SUPPORTER") == -1) {

    //            if (ddlCTXAdhere == null) {
    //                errorField += 'CTX/Dapson: Adherence ';
    //            }

    //            var hidARTStartDate = $("#hidARTStartDate").val();
    //            if (ddlARVAdhere == null && hidARTStartDate.length > 0) {
    //                if (errorField.length > 1) {
    //                    errorField += ', ';
    //                }
    //                errorField += 'ARV: Adherence ';
    //            }
    //        }
    //    }

    //    var ddlsubsituationInterruption = $("#ddlsubsituationInterruption").select2("val");
    //    if (ddlsubsituationInterruption == null) {
    //        if (errorField.length > 1) {
    //            errorField += ', ';
    //        }
    //        errorField += 'Substitutions/Interruptions ';
    //    }

    //    if (ddlsubsituationInterruption == 96 || ddlsubsituationInterruption == 97 || ddlsubsituationInterruption == 98) {
    //        var ddlRegimenCode = $("#ddlRegimenCode").select2("val");
    //        if (ddlRegimenCode == null) {
    //            if (errorField.length > 1) {
    //                errorField += ', ';
    //            }
    //            errorField += 'Regimen Code ';
    //        }
    //    }

    //    var age = $("#hidDOB").val();
    //    if (age > 14) {

    //        if (ddlPWP == null) {
    //            if (errorField.length > 1) {
    //                errorField += ', ';
    //            }
    //            errorField += 'Prevention with positives ';
    //        }
    //    }

    //    var chkKeyPopulation = GetSwitchValue("chkKeyPopulation");

    //    var rbgKP = $("input:radio[name=rbgKP]:checked").val();
    //    var urbgKP = (rbgKP === undefined) ? 0 : rbgKP;
    //    if (chkKeyPopulation > 0) {
    //        if (urbgKP == 0) {
    //            if (errorField.length > 1) {
    //                errorField += ', ';
    //            }
    //            errorField += 'Key Population ';
    //        }
    //    }

    if ($("#dtFilledDate4").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Filled date ';
    }


    if (isNaN(errorField)) {
        errorMsg = errorField + 'cannot be blank.';
    }
    return errorMsg;
}

