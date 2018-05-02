var arrMMAS4Score3 = [];
var arrMMAS8Score3 = [];

function Init_UC_Session3_Controls(response) {
    debugger;
    window.scrollTo(0, 0);

    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableAdherenceBarriersDiv(this);
    });

    $("#chkCaseManager3").bootstrapSwitch('state', false);
    $("#dtFilledDate3").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    $("#chkForgotMed3").bootstrapSwitch('state', false);
    $("#chkCarelessMed3").bootstrapSwitch('state', false);
    $("#chkWorseTakingMed3").bootstrapSwitch('state', false);
    $("#chkFeelBetterMed3").bootstrapSwitch('state', false);
    $("#chkYesterdayMed3").bootstrapSwitch('state', false);
    $("#chkSymptomUnderControl3").bootstrapSwitch('state', false);
    $("#chkStickingTreatmentPlan3").bootstrapSwitch('state', false);

    $("#chkForgotMed3").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score3("fm", GetSwitchValue("chkForgotMed3"));
    });
    $("#chkCarelessMed3").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score3("cm", GetSwitchValue("chkCarelessMed3"));
    });
    $("#chkWorseTakingMed3").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score3("wtm", GetSwitchValue("chkWorseTakingMed3"));
    });
    $("#chkFeelBetterMed3").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score3("fbm", GetSwitchValue("chkFeelBetterMed3"));
    });

    $("#chkYesterdayMed3").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS8Score3("ym", GetSwitchValue("chkYesterdayMed3"));
    });
    $("#chkSymptomUnderControl3").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS8Score3("suc", GetSwitchValue("chkSymptomUnderControl3"));
    });
    $("#chkStickingTreatmentPlan3").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS8Score3("stp", GetSwitchValue("chkStickingTreatmentPlan3"));
    });

    $("#chkDiscussViralLoadResult3").bootstrapSwitch('state', false);
    $("#chkSocialEconomicBarrier3").bootstrapSwitch('state', false);
    $("#chkReferredOtherServices3").bootstrapSwitch('state', false);
    $("#chkAttendAppointment3").bootstrapSwitch('state', false);

    $("#chkBenefitHomeVisit3").bootstrapSwitch('state', false);
    $("#dtFollowupDate3").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    BindSession3Data(response);
}


function BindSession3Data(response) {
    debugger;
    var data;
    var age = $("#hidDOB").val();

    $("#divMrgRM3").empty();
    data = $.grep(response.CD, function (e) { return e.CN == "RememberingMedications"; });
    data = SortArray(data);

    var vdata = [];
    var strIPT = '';
    $.each(data, function (index, value) {
        strIPT += "<label style=\"margin-right: 20px;\">";
        strIPT += "     <input type=\"radio\" id=\"rb" + value.DId + "\" name=\"rbRM3\" class=\"flat-red\" value=\"" + value.DId + "\" />&nbsp;";
        strIPT += value.DN;
        strIPT += "</label>";
    });
    $("#divMrgRM3").append(strIPT);
    $('#divMrgRM3 input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });
    $('input:radio[name=rbRM3]').on('ifChecked', function (event) {
        $(this).closest("input").attr('checked', true);
        CalculateRBMMAS8Score3(this);
    });
    $('input:radio[name=rbRM3]').on('ifUnchecked', function (event) {
        $(this).closest("input").attr('checked', false);
        CalculateRBMMAS8Score3(this);
    });

    CheckDatenAssign(response.FFD, "dtFilledDate3", false);
    CheckDatenAssign(response.FUD, "dtFollowupDate3", false);
    BindSession3Controls(response);
}


function BindSession3Controls(response) {
    debugger;

    if (jQuery.isEmptyObject(response.ACM) == false) {
        if (response.ACM > 0) {
            $("#chkCaseManager3").bootstrapSwitch('state', true);
        }
    }

    $("#txtAdherence3").val(response.APER);

    CheckDatenAssign(response.FFD, "dtFilledDate3", false);

    if (jQuery.isEmptyObject(response.ISFM) == false) {
        if (response.ISFM > 0) {
            $("#chkForgotMed3").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISCM) == false) {
        if (response.ISCM > 0) {
            $("#chkCarelessMed3").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISWTM) == false) {
        if (response.ISWTM > 0) {
            $("#chkWorseTakingMed3").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISFBM) == false) {
        if (response.ISFBM > 0) {
            $("#chkFeelBetterMed3").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISYM) == false) {
        if (response.ISYM > 0) {
            $("#chkYesterdayMed3").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISSUC) == false) {
        if (response.ISSUC > 0) {
            $("#chkSymptomUnderControl3").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISSTP) == false) {
        if (response.ISSTP > 0) {
            $("#chkStickingTreatmentPlan3").bootstrapSwitch('state', true);
        }
    }

    $("input:radio[name=rbRM3][value=" + response.RM + "]").attr('checked', 'checked');
    $("input:radio[name=rbRM3]").iCheck('update');

    $("#txtMMAS4Score3").val(response.MMAS4S);
    $("#txtMMAS4Rating3").val(response.MMAS4R);
    $("#txtMMAS8Score3").val(response.MMAS8S);
    $("#txtMMAS8Rating3").val(response.MMAS8R);
    $("#txtMMAS8Suggestion3").val(response.MMAS8SG);

    if (jQuery.isEmptyObject(response.PUVL) == false) {
        if (response.PUVL > 0) {
            $("#chkDiscussViralLoadResult3").bootstrapSwitch('state', true);
        }
    }
    $("#txtAreaRoutineReminder3").val(response.PDR);

    $("#chkReferredOtherServices3").bootstrapSwitch('state', ConvertValueBool(response.ROS));
    $("#chkAttendAppointment3").bootstrapSwitch('state', ConvertValueBool(response.AA));
    $("#txtAreaExperience3").val(response.EAR);

    if (jQuery.isEmptyObject(response.BHV) == false) {
        if (response.BHV > 0) {
            $("#chkBenefitHomeVisit3").bootstrapSwitch('state', true);
        }
    }
    $("#txtAreaAdherence3").val(response.APLAN);
    CheckDatenAssign(response.FUD, "dtFollowupDate3", false);

    $.hivce.loader('hide');
}

function SaveSession3() {
    debugger;
    $.hivce.loader('show');
    var session3Data = PrepareSession3Data();
    PostSession3Data(session3Data);
}

function PrepareSession3Data() {
    debugger;
    rowSession3Data = [];

    var chkCaseManager = GetSwitchValue("chkCaseManager3");
    var txtAdherence = $("#txtAdherence3").val();
    var dtFilledDate = GetDateData("dtFilledDate3", true);

    var chkForgotMed = GetSwitchValue("chkForgotMed3");
    var chkCarelessMed = GetSwitchValue("chkCarelessMed3");
    var chkWorseTakingMed = GetSwitchValue("chkWorseTakingMed3");
    var chkFeelBetterMed = GetSwitchValue("chkFeelBetterMed3");
    var txtMMAS4Score = $("#txtMMAS4Score3").val();
    var txtMMAS4Rating = $("#txtMMAS4Rating3").val();
    var chkYesterdayMed = GetSwitchValue("chkYesterdayMed3");
    var chkSymptomUnderControl = GetSwitchValue("chkSymptomUnderControl3");
    var chkStickingTreatmentPlan = GetSwitchValue("chkStickingTreatmentPlan3");
    var rbRM3 = $("input:radio[name=rbRM3]:checked").val();
    var urbRM = (rbRM3 === undefined) ? 0 : rbRM3;
    var txtMMAS8Score = $("#txtMMAS8Score3").val();
    var txtMMAS8Rating = $("#txtMMAS8Rating3").val();
    var txtMMAS8Suggestion = $("#txtMMAS8Suggestion3").val();

    var chkDiscussViralLoadResult = GetSwitchValue("chkDiscussViralLoadResult3");
    var chkSocialEconomicBarrier = GetSwitchValue("chkSocialEconomicBarrier3");
    var txtAreaRoutineReminder = $("#txtAreaRoutineReminder3").val();
    var chkReferredOtherServices = GetSwitchValue("chkReferredOtherServices3");
    var chkAttendAppointment = GetSwitchValue("chkAttendAppointment3");
    var txtAreaExperience = $("#txtAreaExperience3").val();
    var chkBenefitHomeVisit = GetSwitchValue("chkBenefitHomeVisit3");
    var txtAreaAdherence = $("#txtAreaAdherence3").val();
    var dtFollowupDate = GetDateData("dtFollowupDate3", true);

    rowSession3Data.push({
        Ptn_Pk: 0,
        LId: 0,
        VId: 0,

        ACM: chkCaseManager,
        CS: 3,
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

    return rowSession3Data[0];
}

function PostSession3Data(rowData) {
    debugger;
    var action = "saveSession3";
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
                    //GetSession3Data();
                    customAlert("Session 3 tab " + dataSuccessMessage.toLowerCase());
                    GetNextPage("tab_4");
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


function CalculateMMAS4Score3(ref, val) {
    var chkData = $.grep(arrMMAS4Score3, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        arrMMAS4Score3.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrMMAS4Score3, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }
    CalculateMMSImpl3();

}

function CalculateMMAS8Score3(ref, val) {
    var chkData = $.grep(arrMMAS8Score3, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        if (ref == "ym") {
            if (val == 1) {
                val = 0;
            }
            else {
                val = 1;
            }
        }
        arrMMAS8Score3.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrMMAS8Score3, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }

    CalculateMMSImpl3();

}

function CalculateRBMMAS8Score3(ctrlName) {
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

    var chkData = $.grep(arrMMAS8Score3, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        arrMMAS8Score3.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrMMAS8Score3, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }

    CalculateMMSImpl3();
}

function CalculateMMSImpl3() {
    var totMMAS8Score = 0.0;

    $.each(arrMMAS8Score3, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            totMMAS8Score += parseFloat(arrD.Val);
        }
    });

    var totMMAS4Score = 0;

    $.each(arrMMAS4Score3, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            totMMAS4Score += parseInt(arrD.Val);
        }
    });

    $("#txtMMAS4Score3").val(totMMAS4Score);

    if (parseInt(totMMAS4Score) == 0) {
        $("#txtMMAS4Rating3").val("Good");
        $("#divOtherQesAdherence3").css("visibility", "hidden");
        $("#divOtherQesAdherence3").css("display", "none");
    }
    else if (parseInt(totMMAS4Score) >= 1 && parseInt(totMMAS4Score) <= 2) {
        $("#txtMMAS4Rating3").val("Inadequate");
        $("#divOtherQesAdherence3").css("visibility", "visible");
        $("#divOtherQesAdherence3").css("display", "inline");
    }
    else if (parseInt(totMMAS4Score) >= 3 && parseInt(totMMAS4Score) <= 4) {
        $("#txtMMAS4Rating3").val("Poor");
        $("#divOtherQesAdherence3").css("visibility", "visible");
        $("#divOtherQesAdherence3").css("display", "inline");
    }

    if (totMMAS4Score > 0) {
        totMMAS8Score = parseFloat(totMMAS8Score) + parseFloat(totMMAS4Score);

        $("#txtMMAS8Score3").val(totMMAS8Score);

        if (parseInt(totMMAS8Score) == 0) {
            $("#txtMMAS8Rating3").val("Good");
            $("#txtMMAS8Suggestion3").val("");
        }
        else if (parseInt(totMMAS8Score) >= 1 && parseInt(totMMAS8Score) <= 2) {
            $("#txtMMAS8Rating3").val("Inadequate");
            $("#txtMMAS8Suggestion3").val("");
        }
        else if (parseInt(totMMAS8Score) >= 3 && parseInt(totMMAS8Score) <= 4) {
            $("#txtMMAS8Rating3").val("Poor");
            $("#txtMMAS8Suggestion3").val("Refer to Counselor");
        }
    }
    else {
        $("#txtMMAS8Score3").val("0");
        $("#txtMMAS8Rating3").val("Good");
        $("#txtMMAS8Suggestion3").val("");
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

    if ($("#dtFilledDate3").val().length == 0) {
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

