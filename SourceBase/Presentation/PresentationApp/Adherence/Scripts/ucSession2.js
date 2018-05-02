var arrMMAS4Score2 = [];
var arrMMAS8Score2 = [];

function Init_UC_Session2_Controls(response) {
    debugger;
    window.scrollTo(0, 0);

    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableAdherenceBarriersDiv(this);
    });

    $("#chkCaseManager2").bootstrapSwitch('state', false);
    $("#dtFilledDate2").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    $("#chkForgotMed2").bootstrapSwitch('state', false);
    $("#chkCarelessMed2").bootstrapSwitch('state', false);
    $("#chkWorseTakingMed2").bootstrapSwitch('state', false);
    $("#chkFeelBetterMed2").bootstrapSwitch('state', false);
    $("#chkYesterdayMed2").bootstrapSwitch('state', false);
    $("#chkSymptomUnderControl2").bootstrapSwitch('state', false);
    $("#chkStickingTreatmentPlan2").bootstrapSwitch('state', false);

    $("#chkForgotMed2").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score2("fm", GetSwitchValue("chkForgotMed2"));
    });
    $("#chkCarelessMed2").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score2("cm", GetSwitchValue("chkCarelessMed2"));
    });
    $("#chkWorseTakingMed2").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score2("wtm", GetSwitchValue("chkWorseTakingMed2"));
    });
    $("#chkFeelBetterMed2").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score2("fbm", GetSwitchValue("chkFeelBetterMed2"));
    });

    $("#chkYesterdayMed2").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS8Score2("ym", GetSwitchValue("chkYesterdayMed2"));
    });
    $("#chkSymptomUnderControl2").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS8Score2("suc", GetSwitchValue("chkSymptomUnderControl2"));
    });
    $("#chkStickingTreatmentPlan2").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS8Score2("stp", GetSwitchValue("chkStickingTreatmentPlan2"));
    });

    $("#chkDiscussViralLoadResult2").bootstrapSwitch('state', false);
    $("#chkSocialEconomicBarrier2").bootstrapSwitch('state', false);
    $("#chkReferredOtherServices2").bootstrapSwitch('state', false);
    $("#chkAttendAppointment2").bootstrapSwitch('state', false);

    $("#chkBenefitHomeVisit2").bootstrapSwitch('state', false);
    $("#dtFollowupDate2").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    BindSession2Data(response);
}


function BindSession2Data(response) {
    debugger;
    var data;
    var age = $("#hidDOB").val();

    $("#divMrgRM2").empty();
    data = $.grep(response.CD, function (e) { return e.CN == "RememberingMedications"; });
    data = SortArray(data);

    var vdata = [];
    var strIPT = '';
    $.each(data, function (index, value) {
        strIPT += "<label style=\"margin-right: 20px;\">";
        strIPT += "     <input type=\"radio\" id=\"rb" + value.DId + "\" name=\"rbRM2\" class=\"flat-red\" value=\"" + value.DId + "\" />&nbsp;";
        strIPT += value.DN;
        strIPT += "</label>";
    });
    $("#divMrgRM2").append(strIPT);
    $('#divMrgRM2 input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });
    $('input:radio[name=rbRM2]').on('ifChecked', function (event) {
        $(this).closest("input").attr('checked', true);
        CalculateRBMMAS8Score2(this);
    });
    $('input:radio[name=rbRM2]').on('ifUnchecked', function (event) {
        $(this).closest("input").attr('checked', false);
        CalculateRBMMAS8Score2(this);
    });

    CheckDatenAssign(response.FFD, "dtFilledDate2", false);
    CheckDatenAssign(response.FUD, "dtFollowupDate2", false);
    BindSession2Controls(response);
}


function BindSession2Controls(response) {
    debugger;

    if (jQuery.isEmptyObject(response.ACM) == false) {
        if (response.ACM > 0) {
            $("#chkCaseManager2").bootstrapSwitch('state', true);
        }
    }

    $("#txtAdherence2").val(response.APER);

    CheckDatenAssign(response.FFD, "dtFilledDate2", false);

    if (jQuery.isEmptyObject(response.ISFM) == false) {
        if (response.ISFM > 0) {
            $("#chkForgotMed2").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISCM) == false) {
        if (response.ISCM > 0) {
            $("#chkCarelessMed2").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISWTM) == false) {
        if (response.ISWTM > 0) {
            $("#chkWorseTakingMed2").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISFBM) == false) {
        if (response.ISFBM > 0) {
            $("#chkFeelBetterMed2").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISYM) == false) {
        if (response.ISYM > 0) {
            $("#chkYesterdayMed2").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISSUC) == false) {
        if (response.ISSUC > 0) {
            $("#chkSymptomUnderControl2").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISSTP) == false) {
        if (response.ISSTP > 0) {
            $("#chkStickingTreatmentPlan2").bootstrapSwitch('state', true);
        }
    }

    $("input:radio[name=rbRM2][value=" + response.RM + "]").attr('checked', 'checked');
    $("input:radio[name=rbRM2]").iCheck('update');

    $("#txtMMAS4Score2").val(response.MMAS4S);
    $("#txtMMAS4Rating2").val(response.MMAS4R);
    $("#txtMMAS8Score2").val(response.MMAS8S);
    $("#txtMMAS8Rating2").val(response.MMAS8R);
    $("#txtMMAS8Suggestion2").val(response.MMAS8SG);

    if (jQuery.isEmptyObject(response.PUVL) == false) {
        if (response.PUVL > 0) {
            $("#chkDiscussViralLoadResult2").bootstrapSwitch('state', true);
        }
    }
    $("#txtAreaRoutineReminder2").val(response.PDR);

    $("#chkReferredOtherServices2").bootstrapSwitch('state', ConvertValueBool(response.ROS));
    $("#chkAttendAppointment2").bootstrapSwitch('state', ConvertValueBool(response.AA));
    $("#txtAreaExperience2").val(response.EAR);

    if (jQuery.isEmptyObject(response.BHV) == false) {
        if (response.BHV > 0) {
            $("#chkBenefitHomeVisit2").bootstrapSwitch('state', true);
        }
    }
    $("#txtAreaAdherence2").val(response.APLAN);
    CheckDatenAssign(response.FUD, "dtFollowupDate2", false);

    $.hivce.loader('hide');
}

function SaveSession2() {
    debugger;
    $.hivce.loader('show');
    var session2Data = PrepareSession2Data();
    PostSession2Data(session2Data);
}

function PrepareSession2Data() {
    debugger;
    rowSession2Data = [];

    var chkCaseManager = GetSwitchValue("chkCaseManager2");
    var txtAdherence = $("#txtAdherence2").val();
    var dtFilledDate = GetDateData("dtFilledDate2", true);

    var chkForgotMed = GetSwitchValue("chkForgotMed2");
    var chkCarelessMed = GetSwitchValue("chkCarelessMed2");
    var chkWorseTakingMed = GetSwitchValue("chkWorseTakingMed2");
    var chkFeelBetterMed = GetSwitchValue("chkFeelBetterMed2");
    var txtMMAS4Score = $("#txtMMAS4Score2").val();
    var txtMMAS4Rating = $("#txtMMAS4Rating2").val();
    var chkYesterdayMed = GetSwitchValue("chkYesterdayMed2");
    var chkSymptomUnderControl = GetSwitchValue("chkSymptomUnderControl2");
    var chkStickingTreatmentPlan = GetSwitchValue("chkStickingTreatmentPlan2");
    var rbRM2 = $("input:radio[name=rbRM2]:checked").val();
    var urbRM = (rbRM2 === undefined) ? 0 : rbRM2;
    var txtMMAS8Score = $("#txtMMAS8Score2").val();
    var txtMMAS8Rating = $("#txtMMAS8Rating2").val();
    var txtMMAS8Suggestion = $("#txtMMAS8Suggestion2").val();

    var chkDiscussViralLoadResult = GetSwitchValue("chkDiscussViralLoadResult2");
    var chkSocialEconomicBarrier = GetSwitchValue("chkSocialEconomicBarrier2");
    var txtAreaRoutineReminder = $("#txtAreaRoutineReminder2").val();
    var chkReferredOtherServices = GetSwitchValue("chkReferredOtherServices2");
    var chkAttendAppointment = GetSwitchValue("chkAttendAppointment2");
    var txtAreaExperience = $("#txtAreaExperience2").val();
    var chkBenefitHomeVisit = GetSwitchValue("chkBenefitHomeVisit2");
    var txtAreaAdherence = $("#txtAreaAdherence2").val();
    var dtFollowupDate = GetDateData("dtFollowupDate2", true);

    rowSession2Data.push({
        Ptn_Pk: 0,
        LId: 0,
        VId: 0,

        ACM: chkCaseManager,
        CS: 2,
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

    return rowSession2Data[0];
}

function PostSession2Data(rowData) {
    debugger;
    var action = "saveSession2";
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
                    //GetSession2Data();
                    customAlert("Session 2 tab " + dataSuccessMessage.toLowerCase());
                    GetNextPage("tab_3");
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


function CalculateMMAS4Score2(ref, val) {
    var chkData = $.grep(arrMMAS4Score2, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        arrMMAS4Score2.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrMMAS4Score2, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }
    CalculateMMSImpl2();

}

function CalculateMMAS8Score2(ref, val) {
    var chkData = $.grep(arrMMAS8Score2, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        if (ref == "ym") {
            if (val == 1) {
                val = 0;
            }
            else {
                val = 1;
            }
        }
        arrMMAS8Score2.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrMMAS8Score2, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }

    CalculateMMSImpl2();

}

function CalculateRBMMAS8Score2(ctrlName) {
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

    var chkData = $.grep(arrMMAS8Score2, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        arrMMAS8Score2.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrMMAS8Score2, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }

    CalculateMMSImpl2();
}

function CalculateMMSImpl2() {
    var totMMAS8Score = 0.0;

    $.each(arrMMAS8Score2, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            totMMAS8Score += parseFloat(arrD.Val);
        }
    });

    var totMMAS4Score = 0;

    $.each(arrMMAS4Score2, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            totMMAS4Score += parseInt(arrD.Val);
        }
    });

    $("#txtMMAS4Score2").val(totMMAS4Score);

    if (parseInt(totMMAS4Score) == 0) {
        $("#txtMMAS4Rating2").val("Good");
        $("#divOtherQesAdherence2").css("visibility", "hidden");
        $("#divOtherQesAdherence2").css("display", "none");
    }
    else if (parseInt(totMMAS4Score) >= 1 && parseInt(totMMAS4Score) <= 2) {
        $("#txtMMAS4Rating2").val("Inadequate");
        $("#divOtherQesAdherence2").css("visibility", "visible");
        $("#divOtherQesAdherence2").css("display", "inline");
    }
    else if (parseInt(totMMAS4Score) >= 3 && parseInt(totMMAS4Score) <= 4) {
        $("#txtMMAS4Rating2").val("Poor");
        $("#divOtherQesAdherence2").css("visibility", "visible");
        $("#divOtherQesAdherence2").css("display", "inline");
    }

    if (totMMAS4Score > 0) {
        totMMAS8Score = parseFloat(totMMAS8Score) + parseFloat(totMMAS4Score);

        $("#txtMMAS8Score2").val(totMMAS8Score);

        if (parseInt(totMMAS8Score) == 0) {
            $("#txtMMAS8Rating2").val("Good");
            $("#txtMMAS8Suggestion2").val("");
        }
        else if (parseInt(totMMAS8Score) >= 1 && parseInt(totMMAS8Score) <= 2) {
            $("#txtMMAS8Rating2").val("Inadequate");
            $("#txtMMAS8Suggestion2").val("");
        }
        else if (parseInt(totMMAS8Score) >= 3 && parseInt(totMMAS8Score) <= 4) {
            $("#txtMMAS8Rating2").val("Poor");
            $("#txtMMAS8Suggestion2").val("Refer to Counselor");
        }
    }
    else {
        $("#txtMMAS8Score2").val("0");
        $("#txtMMAS8Rating2").val("Good");
        $("#txtMMAS8Suggestion2").val("");
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

    if ($("#dtFilledDate2").val().length == 0) {
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

