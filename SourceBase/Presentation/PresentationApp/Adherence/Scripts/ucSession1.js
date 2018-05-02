var arrMMAS4Score = [];
var arrMMAS8Score = [];

function Init_UC_Session1_Controls(response) {
    debugger;
    window.scrollTo(0, 0);

    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableAdherenceBarriersDiv(this);
    });

    $("#chkCaseManager").bootstrapSwitch('state', false);
    $("#dtAssignedDate").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    $("#ddlCaseManager").select2();
    $("#chkDirectlyObservedTherapy").bootstrapSwitch('state', false);
    $("#dtFilledDate").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    $("#chkForgotMed").bootstrapSwitch('state', false);
    $("#chkCarelessMed").bootstrapSwitch('state', false);
    $("#chkWorseTakingMed").bootstrapSwitch('state', false);
    $("#chkFeelBetterMed").bootstrapSwitch('state', false);
    $("#chkYesterdayMed").bootstrapSwitch('state', false);
    $("#chkSymptomUnderControl").bootstrapSwitch('state', false);
    $("#chkStickingTreatmentPlan").bootstrapSwitch('state', false);

    $("#chkForgotMed").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score("fm", GetSwitchValue("chkForgotMed"));
    });
    $("#chkCarelessMed").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score("cm", GetSwitchValue("chkCarelessMed"));
    });
    $("#chkWorseTakingMed").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score("wtm", GetSwitchValue("chkWorseTakingMed"));
    });
    $("#chkFeelBetterMed").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS4Score("fbm", GetSwitchValue("chkFeelBetterMed"));
    });

    $("#chkYesterdayMed").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS8Score("ym", GetSwitchValue("chkYesterdayMed"));
    });
    $("#chkSymptomUnderControl").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS8Score("suc", GetSwitchValue("chkSymptomUnderControl"));
    });
    $("#chkStickingTreatmentPlan").on('switchChange.bootstrapSwitch', function (event, state) {
        CalculateMMAS8Score("stp", GetSwitchValue("chkStickingTreatmentPlan"));
    });
    $("#chkDiscussViralLoadResult").bootstrapSwitch('state', false);
    $("#chkDiscussVLResult").bootstrapSwitch('state', false);
    $("#chkSocialEconomicBarrier").bootstrapSwitch('state', false);


    $("#chkChallengeGettingClinic").bootstrapSwitch('state', false);
    $("#chkHIVStatusBotherDetails").bootstrapSwitch('state', false);
    $("#chkPeopleTreatmentDetails").bootstrapSwitch('state', false);
    $("#chkStigmaInterfering").bootstrapSwitch('state', false);
    $("#chkReligiousBeliefs").bootstrapSwitch('state', false);
    $("#chkReferredOtherServices").bootstrapSwitch('state', false);
    $("#chkAttendAppointment").bootstrapSwitch('state', false);

    $("#chkBenefitHomeVisit").bootstrapSwitch('state', false);
    $("#dtFollowupDate").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    $("#chkCaseManager").on('switchChange.bootstrapSwitch', function (event, state) {
        DisableAdherenceBarriersDiv(this);
    });
    $("#chkChallengeGettingClinic").on('switchChange.bootstrapSwitch', function (event, state) {
        DisableAdherenceBarriersDiv(this);
    });
    $("#chkHIVStatusBotherDetails").on('switchChange.bootstrapSwitch', function (event, state) {
        DisableAdherenceBarriersDiv(this);
    });
    $("#chkAreaTreatDifferently").on('switchChange.bootstrapSwitch', function (event, state) {
        DisableAdherenceBarriersDiv(this);
    });
    $("#chkAreaStigma").on('switchChange.bootstrapSwitch', function (event, state) {
        DisableAdherenceBarriersDiv(this);
    });
    $("#chkReligiousBeliefs").on('switchChange.bootstrapSwitch', function (event, state) {
        DisableAdherenceBarriersDiv(this);
    });
    $("#ddlCaseManager").select2();


    //    $("#divOtherQesAdherence").css("visibility", "hidden");
    //    $("#divOtherQesAdherence").css("display", "none");
    //    $("#chkSTIScreening").on('switchChange.bootstrapSwitch', function (event, state) {
    //        EnableDisableSTIOption(GetSwitchValue("chkSTIScreening"));
    //    });
    //    $("#dtNextAppointmentDate").datepicker({
    //        autoclose: true
    //    });
    //    $("#chkDifferenciatedCare").bootstrapSwitch('state', false);

    //    $("#divEDC1").css("visibility", "hidden");
    //    $("#divEDC1").css("display", "none");

    //    $("#divEDC2").css("visibility", "hidden");
    //    $("#divEDC2").css("display", "none");

    //    $("#divEDC3").css("visibility", "hidden");
    //    $("#divEDC3").css("display", "none");

    //    $("#ddlPtnClassification").select2().on('change', function (e) {
    //        var $this = $(this);
    //        EnableDisableDifferenciatedCareDiv($(this).val());
    //    });
    //    $("#ddlDRAPurpose").select2();
    //    $("#ddlARTRefillModel").select2();
    //    $("#dtDrugRefillAppointmentDate").datepicker({
    //        autoclose: true
    //    });
    //    $('#dtlNextAppointment').dataTable().fnDestroy();

    BindSession1Data(response);
}


function BindSession1Data(response) {
    debugger;
    var data;
    var age = $("#hidDOB").val();

    data = [];
    $.each(response.UDL, function (index, value) {
        data.push({ id: value.U_Id, text: value.UN + ' - ' + value.Desg });
    });

    $("#ddlCaseManager").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlCaseManager").select2("val", "0");
    $("#ddlCaseManager").trigger('change.select2');

    //   data = $.grep(response.DDV, function (e) { return e.CN == "LongTermEffects"; });
    //    var vdata = [];
    //    $.each(data, function (index, value) {
    //        vdata.push({ id: value.DId, text: value.DN });
    //    });
    //    $("#ddlLongTermSideEffect").select2({
    //        placeholder: {
    //            id: '0', // the value of the option
    //            text: 'Select an option'
    //        },
    //        allowClear: true,
    //        data: vdata
    //    });
    //    $("#ddlLongTermSideEffect").select2("val", "0");
    //    $("#ddlLongTermSideEffect").trigger('change.select2');

    $("#divMrgRM").empty();
    data = $.grep(response.CD, function (e) { return e.CN == "RememberingMedications"; });
    data = SortArray(data);

    var vdata = [];
    var strIPT = '';
    $.each(data, function (index, value) {
        strIPT += "<label style=\"margin-right: 20px;\">";
        strIPT += "     <input type=\"radio\" id=\"rb" + value.DId + "\" name=\"rbRM\" class=\"flat-red\" value=\"" + value.DId + "\" />&nbsp;";
        strIPT += value.DN;
        strIPT += "</label>";
    });
    $("#divMrgRM").append(strIPT);
    $('#divMrgRM input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });
    $('input:radio[name=rbRM]').on('ifChecked', function (event) {
        $(this).closest("input").attr('checked', true);
        CalculateRBMMAS8Score(this);
    });
    $('input:radio[name=rbRM]').on('ifUnchecked', function (event) {
        $(this).closest("input").attr('checked', false);
        CalculateRBMMAS8Score(this);
    });

    CheckDatenAssign(response.AD, "dtAssignedDate", false);
    CheckDatenAssign(response.FFD, "dtFilledDate", false);
    CheckDatenAssign(response.FUD, "dtFollowupDate", false);
    BindSession1Controls(response);
}


function BindSession1Controls(response) {
    debugger;
    if (jQuery.isEmptyObject(response.ACM) == false) {
        if (response.ACM > 0) {
            $("#chkCaseManager").bootstrapSwitch('state', true);
            $("#ddlCaseManager").select2().val(response.ACM).trigger("change");
        }
    }

    CheckDatenAssign(response.AD, "dtAssignedDate", false);

    if (jQuery.isEmptyObject(response.TI) == false) {
        if (response.TI > 0) {
            $("#chkDirectlyObservedTherapy").bootstrapSwitch('state', true);
        }
    }

    CheckDatenAssign(response.FFD, "dtFilledDate", false);

    $("#txtAdherence").val(response.APER);

    if (jQuery.isEmptyObject(response.ISFM) == false) {
        if (response.ISFM > 0) {
            $("#chkForgotMed").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISCM) == false) {
        if (response.ISCM > 0) {
            $("#chkCarelessMed").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISWTM) == false) {
        if (response.ISWTM > 0) {
            $("#chkWorseTakingMed").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISFBM) == false) {
        if (response.ISFBM > 0) {
            $("#chkFeelBetterMed").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISYM) == false) {
        if (response.ISYM > 0) {
            $("#chkYesterdayMed").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISSUC) == false) {
        if (response.ISSUC > 0) {
            $("#chkSymptomUnderControl").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.ISSTP) == false) {
        if (response.ISSTP > 0) {
            $("#chkStickingTreatmentPlan").bootstrapSwitch('state', true);
        }
    }

    $("input:radio[name=rbRM][value=" + response.RM + "]").attr('checked', 'checked');
    $("input:radio[name=rbRM]").iCheck('update');

    $("#txtMMAS4Score").val(response.MMAS4S);
    $("#txtMMAS4Rating").val(response.MMAS4R);
    $("#txtMMAS8Score").val(response.MMAS8S);
    $("#txtMMAS8Rating").val(response.MMAS8R);
    $("#txtMMAS8Suggestion").val(response.MMAS8SG);

    if (jQuery.isEmptyObject(response.PUVL) == false) {
        if (response.PUVL > 0) {
            $("#chkDiscussViralLoadResult").bootstrapSwitch('state', true);
        }
    }
    $("#txtAreaViralloadUnderstanding").val(response.PFAR);
    $("#txtAreaHighVL").val(response.PTAHVL);
    $("#txtAreaCorrectMisconception").val(response.HIVSAD);
    $("#txtDrugIntake").val(response.EHTD);
    $("#txtAreaRoutineReminder").val(response.PDR);
    $("#txtAreaVisitTravel").val(response.TVD);
    $("#txtAreaSideEffect").val(response.SE);
    $("#txtDifficultSituationDrug").val(response.DSTD);
    $("#txtAreaTakingDrugDaily").val(response.TDED);
    $("#txtAreaMotivation").val(response.PM);
    $("#txtTotal").val(response.Total);
    $("#txtDepressionSeverity").val(response.DS);
    $("#txtRecommendedManagement").val(response.REC);

    if (jQuery.isEmptyObject(response.SS) == false) {
        if (response.SS > 0) {
            $("#chkSocialEconomicBarrier").bootstrapSwitch('state', true);
        }
    }
    $("#txtAreaDiscussWithFamily").val(response.SSD);
    $("#txtAreaFamilyIncome").val(response.SOI);

    if (jQuery.isEmptyObject(response.CGC) == false) {
        if (response.CGC.length > 0) {
            $("#chkChallengeGettingClinic").bootstrapSwitch('state', true);

        }
    }
    $("#txtAreaChallengeGettingClinic").val(response.CGC);

    if (jQuery.isEmptyObject(response.HIVBD) == false) {
        if (response.HIVBD.length > 0) {
            $("#chkHIVStatusBotherDetails").bootstrapSwitch('state', true);
        }
    }
    $("#txtHIVStatusBotherDetails").val(response.HIVBD);

    if (jQuery.isEmptyObject(response.PTD) == false) {
        if (response.PTD.length > 0) {
            $("#chkPeopleTreatmentDetails").bootstrapSwitch('state', true);
        }
    }
    $("#txtPeopleTreatmentDetails").val(response.PTD);

    if (jQuery.isEmptyObject(response.SI) == false) {
        if (response.SI.length > 0) {
            $("#chkStigmaInterfering").bootstrapSwitch('state', true);
        }
    }
    $("#txtStigmaInterfering").val(response.SI);

    if (jQuery.isEmptyObject(response.RB) == false) {
        if (response.RB.length > 0) {
            $("#chkReligiousBeliefs").bootstrapSwitch('state', true);
        }
    }
    $("#txtReligiousBeliefs").val(response.RB);

    $("#chkReferredOtherServices").bootstrapSwitch('state', ConvertValueBool(response.ROS));
    $("#chkAttendAppointment").bootstrapSwitch('state', ConvertValueBool(response.AA));
    $("#txtAreaExperience").val(response.EAR);

    if (jQuery.isEmptyObject(response.BHV) == false) {
        if (response.BHV > 0) {
            $("#chkBenefitHomeVisit").bootstrapSwitch('state', true);
        }
    }
    $("#txtAreaAdherence").val(response.APLAN);
    CheckDatenAssign(response.FUD, "dtFollowupDate", false);

    $.hivce.loader('hide');
}

function SaveSession1() {
    debugger;
    $.hivce.loader('show');
    var session1Data = PrepareSession1Data();
    PostSession1Data(session1Data);
}

function PrepareSession1Data() {
    debugger;
    rowSession1Data = [];

    var chkCaseManager = GetSwitchValue("chkCaseManager");
    if (chkCaseManager == null) {
        var ddlCaseManager = 0;
    }
    else {
        var ddlCaseManager = $("#ddlCaseManager").select2("val");
        if (ddlCaseManager == null) {
            ddlCaseManager = 0;
        }
    }
    var dtAssignedDate = GetDateData("dtAssignedDate", true);
    var chkDirectlyObservedTherapy = GetSwitchValue("chkDirectlyObservedTherapy");
    var dtFilledDate = GetDateData("dtFilledDate", true);
    var txtAdherence = $("#txtAdherence").val();

    var chkForgotMed = GetSwitchValue("chkForgotMed");
    var chkCarelessMed = GetSwitchValue("chkCarelessMed");
    var chkWorseTakingMed = GetSwitchValue("chkWorseTakingMed");
    var chkFeelBetterMed = GetSwitchValue("chkFeelBetterMed");
    var txtMMAS4Score = $("#txtMMAS4Score").val();
    var txtMMAS4Rating = $("#txtMMAS4Rating").val();
    var chkYesterdayMed = GetSwitchValue("chkYesterdayMed");
    var chkSymptomUnderControl = GetSwitchValue("chkSymptomUnderControl");
    var chkStickingTreatmentPlan = GetSwitchValue("chkStickingTreatmentPlan");
    var rbRM = $("input:radio[name=rbRM]:checked").val();
    var urbRM = (rbRM === undefined) ? 0 : rbRM;
    var txtMMAS8Score = $("#txtMMAS8Score").val();
    var txtMMAS8Rating = $("#txtMMAS8Rating").val();
    var txtMMAS8Suggestion = $("#txtMMAS8Suggestion").val();

    var chkDiscussViralLoadResult = GetSwitchValue("chkDiscussViralLoadResult");
    var txtAreaViralloadUnderstanding = $("#txtAreaViralloadUnderstanding").val();
    var txtAreaHighVL = $("#txtAreaHighVL").val();
    var txtAreaCorrectMisconception = $("#txtAreaCorrectMisconception").val();
    var txtDrugIntake = $("#txtDrugIntake").val();
    var txtAreaRoutineReminder = $("#txtAreaRoutineReminder").val();
    var txtAreaVisitTravel = $("#txtAreaVisitTravel").val();
    var txtAreaSideEffect = $("#txtAreaSideEffect").val();
    var txtDifficultSituationDrug = $("#txtDifficultSituationDrug").val();
    var txtAreaTakingDrugDaily = $("#txtAreaTakingDrugDaily").val();
    var txtAreaMotivation = $("#txtAreaMotivation").val();
    var chkSocialEconomicBarrier = GetSwitchValue("chkSocialEconomicBarrier");

    var txtAreaDiscussWithFamily = $("#txtAreaDiscussWithFamily").val();

    var txtAreaFamilyIncome = $("#txtAreaFamilyIncome").val();

    var chkChallengeGettingClinic = GetSwitchValue("chkChallengeGettingClinic");
    var txtAreaChallengeGettingClinic = $("#txtAreaChallengeGettingClinic").val();

    var chkHIVStatusBotherDetails = GetSwitchValue("chkHIVStatusBotherDetails");
    var txtHIVStatusBotherDetails = $("#txtHIVStatusBotherDetails").val();

    var chkPeopleTreatmentDetails = GetSwitchValue("chkPeopleTreatmentDetails");
    var txtPeopleTreatmentDetails = $("#txtPeopleTreatmentDetails").val();

    var chkStigmaInterfering = GetSwitchValue("chkStigmaInterfering");
    var txtStigmaInterfering = $("#txtStigmaInterfering").val();

    var chkReligiousBeliefs = GetSwitchValue("chkReligiousBeliefs");
    var txtReligiousBeliefs = $("#txtReligiousBeliefs").val();

    var chkReferredOtherServices = GetSwitchValue("chkReferredOtherServices");
    var chkAttendAppointment = GetSwitchValue("chkAttendAppointment");

    var txtAreaExperience = $("#txtAreaExperience").val();
    var chkBenefitHomeVisit = GetSwitchValue("chkBenefitHomeVisit");
    var txtAreaAdherence = $("#txtAreaAdherence").val();
    var dtFollowupDate = GetDateData("dtFollowupDate", true);

    rowSession1Data.push({
        Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        ACM: ddlCaseManager,
        AD: dtAssignedDate,
        TI: chkDirectlyObservedTherapy,
        CS: 1,
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
        PFAR: txtAreaViralloadUnderstanding,
        PTAHVL: txtAreaHighVL,
        HIVSAD: txtAreaCorrectMisconception,
        EHTD: txtDrugIntake,
        PDR: txtAreaRoutineReminder,
        TVD: txtAreaVisitTravel,
        SE: txtAreaSideEffect,
        DSTD: txtDifficultSituationDrug,
        TDED: txtAreaTakingDrugDaily,
        PM: txtAreaMotivation,
        SS: chkSocialEconomicBarrier,
        SSD: txtAreaDiscussWithFamily,
        SOI: txtAreaFamilyIncome,
        CGC: txtAreaChallengeGettingClinic,
        HIVBD: txtHIVStatusBotherDetails,
        PTD: txtPeopleTreatmentDetails,
        SI: txtStigmaInterfering,
        RB: txtReligiousBeliefs,
        ROS: chkReferredOtherServices,
        AA: chkAttendAppointment,
        EAR: txtAreaExperience,
        BHV: chkBenefitHomeVisit,
        APLAN: txtAreaAdherence,
        FUD: dtFollowupDate
    });

    return rowSession1Data[0];
}

function PostSession1Data(rowData) {
    debugger;
    var action = "saveSession1";
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
                    //GetSession1Data();
                    customAlert("Session 1 tab " + dataSuccessMessage.toLowerCase());
                    GetNextPage("tab_2");
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


function CalculateMMAS4Score(ref, val) {
    debugger;
    var chkData = $.grep(arrMMAS4Score, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        arrMMAS4Score.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrMMAS4Score, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }
    CalculateMMSImpl();

    /*var totMMAS4Score = 0;

    $.each(arrMMAS4Score, function (index, arrD) {
    if (jQuery.isEmptyObject(arrD) == false) {
    totMMAS4Score += parseInt(arrD.Val);
    }
    });*/

    //totPHQ9 = parseInt(totPHQ9) + parseInt(val);
    //    $("#txtMMAS4Score").val(totMMAS4Score);

    //    if (parseInt(totMMAS4Score) == 0) {
    //        $("#txtMMAS4Rating").val("Good");
    //        $("#divOtherQesAdherence").css("visibility", "hidden");
    //        $("#divOtherQesAdherence").css("display", "none");
    //    }
    //    else if (parseInt(totMMAS4Score) >= 1 && parseInt(totMMAS4Score) <= 2) {
    //        $("#txtMMAS4Rating").val("Inadequate");
    //        $("#divOtherQesAdherence").css("visibility", "visible");
    //        $("#divOtherQesAdherence").css("display", "inline");
    //    }
    //    else if (parseInt(totMMAS4Score) >= 3 && parseInt(totMMAS4Score) <= 4) {
    //        $("#txtMMAS4Rating").val("Poor");
    //        $("#divOtherQesAdherence").css("visibility", "visible");
    //        $("#divOtherQesAdherence").css("display", "inline");
    //    }

}

function CalculateMMAS8Score(ref, val) {
    var chkData = $.grep(arrMMAS8Score, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        if (ref == "ym") {
            if (val == 1) {
                val = 0;
            }
            else {
                val = 1;
            }
        }
        arrMMAS8Score.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrMMAS8Score, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }

    CalculateMMSImpl();

}

function CalculateRBMMAS8Score(ctrlName) {
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

    var chkData = $.grep(arrMMAS8Score, function (e) { return e.Ref.toLowerCase() == ref.toLowerCase(); });
    if (jQuery.isEmptyObject(chkData) == true) {
        arrMMAS8Score.push({ Ref: ref, Val: val });
    }
    else {
        $.each(arrMMAS8Score, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.Ref.toLowerCase() == ref.toLowerCase()) {
                    arrD.Val = val;
                }
            }
        });
    }

    CalculateMMSImpl();
}

function CalculateMMSImpl() {
    var totMMAS8Score = 0.0;

    $.each(arrMMAS8Score, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            totMMAS8Score += parseFloat(arrD.Val);
        }
    });

    var totMMAS4Score = 0;

    $.each(arrMMAS4Score, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            totMMAS4Score += parseInt(arrD.Val);
        }
    });

    $("#txtMMAS4Score").val(totMMAS4Score);

    if (parseInt(totMMAS4Score) == 0) {
        $("#txtMMAS4Rating").val("Good");
        $("#divOtherQesAdherence").css("visibility", "hidden");
        $("#divOtherQesAdherence").css("display", "none");
    }
    else if (parseInt(totMMAS4Score) >= 1 && parseInt(totMMAS4Score) <= 2) {
        $("#txtMMAS4Rating").val("Inadequate");
        $("#divOtherQesAdherence").css("visibility", "visible");
        $("#divOtherQesAdherence").css("display", "inline");
    }
    else if (parseInt(totMMAS4Score) >= 3 && parseInt(totMMAS4Score) <= 4) {
        $("#txtMMAS4Rating").val("Poor");
        $("#divOtherQesAdherence").css("visibility", "visible");
        $("#divOtherQesAdherence").css("display", "inline");
    }

    if (totMMAS4Score > 0) {
        totMMAS8Score = parseFloat(totMMAS8Score) + parseFloat(totMMAS4Score);

        $("#txtMMAS8Score").val(totMMAS8Score);

        if (parseInt(totMMAS8Score) == 0) {
            $("#txtMMAS8Rating").val("Good");
            $("#txtMMAS8Suggestion").val("");
        }
        else if (parseInt(totMMAS8Score) >= 1 && parseInt(totMMAS8Score) <= 2) {
            $("#txtMMAS8Rating").val("Inadequate");
            $("#txtMMAS8Suggestion").val("");
        }
        else if (parseInt(totMMAS8Score) >= 3 && parseInt(totMMAS8Score) <= 4) {
            $("#txtMMAS8Rating").val("Poor");
            $("#txtMMAS8Suggestion").val("Refer to Counselor");
        }
    }
    else {
        $("#txtMMAS8Score").val("0");
        $("#txtMMAS8Rating").val("Good");
        $("#txtMMAS8Suggestion").val("");
    }
}

function CheckMXBlankValues() {
    errorMsg = '';
    var errorField = '';
   
    if ($("#dtFilledDate").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Filled date ';
    }

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

    //    //    if ($("#dtNextAppointmentDate").val().length == 0) {
    //    //        if (errorField.length > 1) {
    //    //            errorField += ', ';
    //    //        }
    //    //        errorField += 'Next Appointment date ';
    //    //    }


    if (isNaN(errorField)) {
        errorMsg = errorField + 'cannot be blank.';
    }
    return errorMsg;
}

//function EnableDisableOtherSpecifyTextBox(ctrlName, ctrTextBox, val) {
//    var chkbox = GetSwitchValue(ctrlName);
//    if (chkbox != null) {
//        var data = $("#" + ctrlName).select2('data')[0];
//        if (data.text.toLowerCase().indexOf("other") >= 0) {
//            $("#" + ctrTextBox).prop("disabled", false);
//        }
//        else {
//            $("#" + ctrTextBox).prop("disabled", true);
//        }
//    }
//    else {
//        $("#" + ctrTextBox).prop("disabled", true);
//    }
//}