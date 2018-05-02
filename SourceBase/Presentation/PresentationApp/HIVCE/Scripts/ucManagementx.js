var arrMMAS4Score = [];
var arrMMAS8Score = [];
var arrICDData = [];
var arrICDBaseData = [];
var arrICD10 = [];
var arrNxtAppointment = [];
var arrPatientClassification = [];

function InitManagementControls(response) {
    window.scrollTo(0, 0);

    arrICD10 = [];
    arrICDData = [];
    arrICDBaseData = [];
    $("#dtARTDateEnded").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    $("#ddlSortTermSideEffect").select2();
    $("#ddlLongTermSideEffect").select2();

    $("#ddlCTXAdhere").select2();
    $("#ddlCTXWhypoorfair").select2();
    $("#ddlARVAdhere").select2();
    $("#ddlARVWhypoorfair").select2();
    $("#ddlsubsituationInterruption").select2();

    $("#ddlChangeRegimenReason").select2();
    $("#ddlStopRegimenReason").select2();

    $("#chkThreapyChange").bootstrapSwitch('state', false);
    $("#chkARTSideEffect").bootstrapSwitch('state', false);
    $("#chkKeyPopulation").bootstrapSwitch('state', false);
    $("#chkSTIScreening").bootstrapSwitch('state', false);
    $("#chkPartnerNotified").bootstrapSwitch('state', false);


    $("#divOtherQesAdherence").css("visibility", "hidden");
    $("#divOtherQesAdherence").css("display", "none");

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

    $("#chkKeyPopulation").on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });

    $("#chkThreapyChange").on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });

    $("#chkSTIScreening").on('switchChange.bootstrapSwitch', function (event, state) {
        EnableDisableSTIOption(GetSwitchValue("chkSTIScreening"));
    });


    $("#dtNextAppointmentDate").datepicker({
        autoclose: true
    });

    $("#ddlPurpose").select2();
    $("#ddlMgtSignature").select2();
    $("#ddlPtnClassification").select2();
    $("#chkDifferenciatedCare").bootstrapSwitch('state', false);

    $("#divEDC1").css("visibility", "hidden");
    $("#divEDC1").css("display", "none");

    $("#divEDC2").css("visibility", "hidden");
    $("#divEDC2").css("display", "none");

    $("#divEDC3").css("visibility", "hidden");
    $("#divEDC3").css("display", "none");

    $("#ddlPtnClassification").select2().on('change', function (e) {
        var $this = $(this);
        EnableDisableDifferenciatedCareDiv($(this).val());
    });


    $("#ddlDRAPurpose").select2();
    $("#ddlARTRefillModel").select2();
    $("#dtDrugRefillAppointmentDate").datepicker({
        autoclose: true
    });
    $('#dtlNextAppointment').dataTable().fnDestroy();

    ReInitialiseMtXDatatable();
    ReInitialiseAppointmentsDatatable();

    //GetManagementData();
    BindManagementData(response);
}

function EnableDisableSTIOption(ctrlValue) {
    var stiVal;
    var stiSelected = false;
    var arrPWP = [];
    debugger;

    $("#ddlPWP").find("option").each(function (i, opt) {
        if (jQuery.isEmptyObject(opt) == false) {
            if (opt.selected) {
                if ($(opt).text().toLowerCase().indexOf("screening") >= 0) {
                    stiSelected = true;
                    if (ctrlValue == 1) {
                        arrPWP.push({ id: opt.value });
                    }
                } else {
                    arrPWP.push({ id: opt.value });

                }
            }
        }
    });


    if (ctrlValue == 0) {
        $("#ddlPWP").find("option").each(function (i, opt) {
            if (jQuery.isEmptyObject(opt) == false) {
                if ($(opt).text().toLowerCase().indexOf("screening") >= 0) {
                    opt.disabled = true;
                    if (stiSelected) {
                        opt.selected = false;
                    }
                    stiVal = $(opt).val();
                }
            }
        });

    } else {

        $("#ddlPWP").find("option").each(function (i, opt) {
            if (jQuery.isEmptyObject(opt) == false) {
                if ($(opt).text().toLowerCase().indexOf("screening") >= 0) {
                    opt.disabled = false;
                    stiVal = $(opt).val();
                }
            }
        });

    }

    if (arrPWP.length > 0) {
        //$("#ddlPWP").select2().val(arrPWP).trigger("change");
        BindMultiSelectDropDown("ddlPWP", arrPWP);
    }
    else {
        $("#ddlPWP").select2().val("0").trigger("change");
    }

}

function EnableDisableDifferenciatedCareDiv() {
    if ($("#ddlPtnClassification").select2('data').length > 0) {
        var theSelection = $("#ddlPtnClassification").select2('data')[0];

        if (theSelection.text.toUpperCase().indexOf("STABLE") >= 0) {

            $("#divEDC1").css("visibility", "visible");
            $("#divEDC1").css("display", "block");

            $("#divEDC2").css("visibility", "visible");
            $("#divEDC2").css("display", "block");

            $("#divEDC3").css("visibility", "visible");
            $("#divEDC3").css("display", "block");

        } else {

            $("#divChangeRegimen").css("visibility", "hidden");
            $("#divChangeRegimen").css("display", "none");

            $("#divARTStop").css("visibility", "hidden");
            $("#divARTStop").css("display", "none");

            $("#divEligibleART").css("visibility", "hidden");
            $("#divEligibleART").css("display", "none");

            $("#divRegimenCodes").css("visibility", "hidden");
            $("#divRegimenCodes").css("display", "none");
        }
    }
}

function CalculateMMAS4Score(ref, val) {
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

//function GetManagementData() {
//    var postData;
//    activePage = getPageName() + '.aspx';
//    $.ajax({
//        type: "POST",
//        //url: "ClinicalEncounter.aspx?data=getmgtdata",
//        url: activePage + "?data=getmgtdata",
//        //data: JSON.stringify(nodeId),
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        async: true,
//        cache: false,
//        beforeSend: function () {
//        },
//        success: function (response) {
//            console.log(response);
//            var responseSuccess = response.Success;
//            if (responseSuccess != null) {
//                if (responseSuccess == "false") {
//                    customAlert(errorMessage);
//                }
//            }
//            else {
//                BindManagementData(response);
//            }

//        },
//        error: function (msg) {
//        }
//    });
//}

function BindManagementData(response) {
    arrICDData = [];
    var data;
    var age = $("#hidPAYM").val();
    data = $.grep(response.DDV, function (e) { return e.CN == "ShortTermEffects"; });
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlSortTermSideEffect").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlSortTermSideEffect").select2("val", "0");
    $("#ddlSortTermSideEffect").trigger('change.select2');

    data = $.grep(response.DDV, function (e) { return e.CN == "LongTermEffects"; });
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlLongTermSideEffect").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlLongTermSideEffect").select2("val", "0");
    $("#ddlLongTermSideEffect").trigger('change.select2');


    data = $.grep(response.DDV, function (e) { return e.CN == "ARV Eligibility"; });
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlEligibleART").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlEligibleART").select2("val", "0");
    $("#ddlEligibleART").trigger('change.select2');


    $("#divMrgRM").empty();
    data = $.grep(response.DDV, function (e) { return e.CN == "RememberingMedications"; });

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

    data = $.grep(response.DDV, function (e) { return e.CN == "Adherence"; });
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlCTXAdhere").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlCTXAdhere").select2("val", "0");
    $("#ddlCTXAdhere").trigger('change.select2');

    $("#ddlCTXAdhere").select2().on('change', function () {
        DisableEnableWhyPoor($(this).val(), 'ddlCTXWhypoorfair');
    }).trigger('change');

    $("#ddlARVAdhere").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlARVAdhere").select2("val", "0");
    $("#ddlARVAdhere").trigger('change.select2');

    $("#ddlARVAdhere").select2().on('change', function () {
        DisableEnableWhyPoor($(this).val(), 'ddlARVWhypoorfair');
    }).trigger('change');

    data = $.grep(response.DDV, function (e) { return e.CN == "FairPoor"; });
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlCTXWhypoorfair").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlCTXWhypoorfair").select2("val", "0");
    $("#ddlCTXWhypoorfair").trigger('change.select2');

    $("#ddlARVWhypoorfair").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlARVWhypoorfair").select2("val", "0");
    $("#ddlARVWhypoorfair").trigger('change.select2');

    data = $.grep(response.DDV, function (e) { return e.CN == "Substitution"; });
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlsubsituationInterruption").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlsubsituationInterruption").select2("val", "0");
    $("#ddlsubsituationInterruption").trigger('change.select2');

    data = $.grep(response.DDV, function (e) { return e.CN == "Stopped reason"; });
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlStopRegimenReason").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlStopRegimenReason").select2("val", "0");
    $("#ddlStopRegimenReason").trigger('change.select2');

    data = $.grep(response.DDV, function (e) { return e.CN == "Change reason"; });
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlChangeRegimenReason").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlChangeRegimenReason").select2("val", "0");
    $("#ddlChangeRegimenReason").trigger('change.select2');

    $("#divChangeRegimen").css("visibility", "hidden");
    $("#divChangeRegimen").css("display", "none");

    $("#divARTStop").css("visibility", "hidden");
    $("#divARTStop").css("display", "none");

    $("#divEligibleART").css("visibility", "hidden");
    $("#divEligibleART").css("display", "none");

    $("#ddlsubsituationInterruption").select2().on('change', function () {
        BindSubsituationInterruption($(this).val());
    }).trigger('change');

    $("#divKeyPopulation").empty();
    data = $.grep(response.DDV, function (e) { return e.CN == "KeyPopulation"; });

    data = SortArray(data);

    var vdata = [];
    var strIPT = '';
    $.each(data, function (index, value) {
        strIPT += "<label style=\"margin-right: 20px;\">";
        strIPT += "     <input type=\"radio\" id=\"rb" + value.DId + "\" name=\"rbgKP\" class=\"flat-red\" value=\"" + value.DId + "\" />&nbsp;";
        strIPT += value.DN;
        strIPT += "</label>";
    });
    $("#divKeyPopulation").append(strIPT);
    $('#divKeyPopulation input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });

    data = $.grep(response.DDV, function (e) { return e.CN == "PWP"; });
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlPWP").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlPWP").select2("val", "0");
    $("#ddlPWP").trigger('change.select2');

    //ICD 10..........
    arrICD10 = [];
    arrICDData = [];
    arrICDBaseData = [];
    arrICDBaseData = response.ICD;
    //data = $.grep(response.ICD, function (e) { return e.CN == "ICD"; });
    $.each(response.ICD, function (index, value) {
        arrICDData.push({ id: value.ICDId, text: value.ICDC + " - " + value.ICDN });
    });

    //    $("#ddlICD").select2({
    //        placeholder: {
    //            id: '0', // the value of the option
    //            text: 'Select an option'
    //        },
    //        //data: arrICDData,
    //        //init selected from elements value
    //        initSelection: function (element, callback) {
    //            var initialData = [];
    //            debugger;
    //            $(element.val()).each(function () {
    //                initialData.push({ id: this, text: this });
    //            });
    //            callback(initialData);
    //        },
    //        allowClear: true,
    //        minimumInputLength: 3,
    //        query: function (query) {
    //            var sdata = { results: [] };
    //            if (jQuery.isEmptyObject(query.term) == false) {
    //                $.each(arrICDData, function () {
    //                    if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
    //                        sdata.results.push({ id: this.id, text: this.text });
    //                    }
    //                });
    //            }
    //            query.callback(sdata);

    //        }
    //    });

    var urlPath = getPageName() + '.aspx?data=geticd10';

    $("#ddlICD").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        minimumInputLength: 3,
        allowClear: true,
        ajax: {
            url: urlPath,
            dataType: 'json',
            quietMillis: 250,
            data: function (term, page) { // page is the one-based page number tracked by Select2
                return {
                    q: term, //search term
                    page: page, // page number
                    type: 'tags'
                };
            },
            //            results: function (dataicd, page) {
            //                var more = (page * 30) < data.total_count; // whether or not there are more results available
            //                debugger;
            //                console.log(data.items);
            //                //                results: []
            //                //                if (jQuery.isEmptyObject(query.term) == false) {
            //                //                    $.each(arrICDData, function () {
            //                //                        if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
            //                //                            sdata.results.push({ id: this.id, text: this.text });
            //                //                        }
            //                //                    });
            //                //                }
            //                // notice we return the value of more so Select2 knows if more results can be loaded
            //                return { results: data.items, more: more };
            //            },
            //matcher: matchStart,
            //            matcher: function (term, text) {
            //                var terms = term.split(" ");
            //                for (var i = 0; i < terms.length; i++) {
            //                    var tester = new RegExp("\\b" + terms[i], 'i');
            //                    if (tester.test(text) == false) {
            //                        return (text === 'Other')
            //                    }
            //                }
            //                return true;
            //            },
            processResults: function (dataicd, params) {

                // parse the results into the format expected by Select2
                // since we are using custom formatting functions we do not need to
                // alter the remote JSON data, except to indicate that infinite
                // scrolling can be used
                dataObj = new Array();
                //                results: $.map(dataicd, function (obj) {
                //                    dataObj.push({
                //                        id: obj.ICDId,
                //                        text: obj.ICDC + ' - ' + obj.ICDN
                //                        //text: obj.ICDN
                //                    });
                //                });

                results: []
                if (jQuery.isEmptyObject(params.term) == false) {
                    $.each(dataicd, function (index, value) {
                        var txtVal = value.ICDC + ' - ' + value.ICDN;
                        if (params.term.length == 0 || txtVal.toUpperCase().indexOf(params.term.toUpperCase()) >= 0) {
                            dataObj.push({ id: value.ICDId, text: txtVal });
                        }
                    });
                }


                params.page = params.page || 1;
                return {
                    results: dataObj,
                    pagination: {
                        more: (params.page * 30) < data.total_count
                    }
                };
            },
            cache: true

        },
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work

        minimumInputLength: 3
    });



    $("#ddlICD").select2("val", "0");
    $("#ddlICD").trigger('change.select2');

    CheckDatenAssign(response.AD, "dtNextAppointmentDate", false);

    data = $.grep(response.PUR, function (e) { return e.CN == "PUR"; });
    var vdata = [], vdata1 = [];
    debugger;

    $.each(data, function (index, value) {
        if (value.DN.toUpperCase().indexOf("REFILL") >= 0) {
            vdata1.push({ id: value.DId, text: value.DN });
        }
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlPurpose").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlPurpose").select2("val", "0");
    $("#ddlPurpose").trigger('change.select2');

    $("#ddlDRAPurpose").select2({
        //        placeholder: {
        //            id: '0', // the value of the option
        //            text: 'Select an option'
        //        },
        allowClear: false,
        data: vdata1
    });
    $("#ddlDRAPurpose").trigger('change.select2');



    data = $.grep(response.RC, function (e) { return e.CN == "RegimenCodes"; });
    vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlRegimenCode").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlRegimenCode").select2("val", "0");
    $("#ddlRegimenCode").trigger('change.select2');

    vdata = [];
    $.each(response.SIG, function (index, value) {
        vdata.push({ id: value.UserID, text: value.UserName + ' - ' + value.Designation });
    });

    $("#ddlMgtSignature").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlMgtSignature").select2("val", "0");
    $("#ddlMgtSignature").trigger('change.select2');

    debugger;

    data = $.grep(response.PC, function (e) { return e.CN == "Classification"; });
    //    vdata = [];
    //    $.each(data, function (index, value) {
    //        vdata.push({ id: value.DId, text: value.DN });
    //    });
    arrPatientClassification = data;

    //    $("#ddlPtnClassification").select2({
    //        placeholder: {
    //            id: '0', // the value of the option
    //            text: 'Select an option'
    //        },
    //        allowClear: true,
    //        data: vdata
    //    });
    //    $("#ddlPtnClassification").select2("val", "0");
    //    $("#ddlPtnClassification").trigger('change.select2');


    data = $.grep(response.ARTRM, function (e) { return e.CN == "ARTRM"; });
    vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlARTRefillModel").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlARTRefillModel").select2("val", "0");
    $("#ddlARTRefillModel").trigger('change.select2');




    var age = $("#hidDOB").val();

    if (age < 14) {
        $("#div3PositivePreventionH").css("visibility", "hidden");
        $("#div3PositivePreventionH").css("display", "none");

        $("#div3PositivePreventionB").css("visibility", "hidden");
        $("#div3PositivePreventionB").css("display", "none");
    }
    else {
        $("#div3PositivePreventionH").css("visibility", "visible");
        $("#div3PositivePreventionH").css("display", "block");

        $("#div3PositivePreventionB").css("visibility", "visible");
        $("#div3PositivePreventionB").css("display", "block");
    }

    BindManagementxControls(response);

}

function DisableEnableWhyPoor(val, whyCtrlName) {
    if (val != null) {
        if (whyCtrlName == 'ddlCTXWhypoorfair') {
            var data = $("#ddlCTXAdhere").select2('data')[0];
            if (data.text.toLowerCase().indexOf("good") >= 0) {
                $("#ddlCTXWhypoorfair").prop("disabled", true);
            }
            else {
                $("#ddlCTXWhypoorfair").prop("disabled", false);
            }
        }
        if (whyCtrlName == 'ddlARVWhypoorfair') {
            var data = $("#ddlARVAdhere").select2('data')[0];
            if (data.text.toLowerCase().indexOf("good") >= 0) {
                $("#ddlARVWhypoorfair").prop("disabled", true);
            }
            else {
                $("#ddlARVWhypoorfair").prop("disabled", false);
            }
        }
    }
}

$.fn.select2.amd.require(['select2/compat/matcher'], function (oldMatcher) {
    $("#ddlICD").select2({
        matcher: oldMatcher(matchStart)
    });
});

function matchStart(arrICDData, text) {
    if (text.toUpperCase().indexOf(term.toUpperCase()) == 0) {
        return true;
    }
    return false;
}

function BindManagementxControls(response) {
    if (jQuery.isEmptyObject(response.ARVSE) == false) {
        if (response.ARVSE > 0) {
            $("#chkARTSideEffect").bootstrapSwitch('state', true);
        }
    }

    BindMultiSelectDropDown("ddlSortTermSideEffect", response.STES);
    BindMultiSelectDropDown("ddlLongTermSideEffect", response.LTES);

    if (jQuery.isEmptyObject(response.MPA.ISFM) == false) {
        if (response.MPA.ISFM > 0) {
            $("#chkForgotMed").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.MPA.ISCM) == false) {
        if (response.MPA.ISCM > 0) {
            $("#chkCarelessMed").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.MPA.ISWTM) == false) {
        if (response.MPA.ISWTM > 0) {
            $("#chkWorseTakingMed").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.MPA.ISFBM) == false) {
        if (response.MPA.ISFBM > 0) {
            $("#chkFeelBetterMed").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.MPA.ISYM) == false) {
        if (response.MPA.ISYM > 0) {
            $("#chkYesterdayMed").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.MPA.ISSUC) == false) {
        if (response.MPA.ISSUC > 0) {
            $("#chkSymptomUnderControl").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.MPA.ISSTP) == false) {
        if (response.MPA.ISSTP > 0) {
            $("#chkStickingTreatmentPlan").bootstrapSwitch('state', true);
        }
    }

    $("input:radio[name=rbRM][value=" + response.MPA.RM + "]").attr('checked', 'checked');
    $("input:radio[name=rbRM]").iCheck('update');

    $("#txtMMAS4Score").val(response.MPA.MMAS4S);
    $("#txtMMAS4Rating").val(response.MPA.MMAS4R);
    $("#txtMMAS8Score").val(response.MPA.MMAS8S);
    $("#txtMMAS8Rating").val(response.MPA.MMAS8R);
    $("#txtMMAS8Suggestion").val(response.MPA.MMAS8SG);
    debugger;
    if (response.TP != 99) {
        if (response.RVID < $("#hidVId").val()) {
            $("#ddlsubsituationInterruption").select2().val(95).trigger("change");
            BindSubsituationInterruption(95);
        }
        else {
            $("#ddlsubsituationInterruption").select2().val(response.TP).trigger("change");
            BindSubsituationInterruption(response.TP);
        }
    } else {
        $("#ddlsubsituationInterruption").select2().val(response.TP).trigger("change");
        BindSubsituationInterruption(response.TP);
    }

    $("#ddlCTXAdhere").select2().val(response.CTXA).trigger("change");
    $("#ddlCTXWhypoorfair").select2().val(response.CTXWPF).trigger("change");
    $("#ddlARVAdhere").select2().val(response.ARVA).trigger("change");
    $("#ddlARVWhypoorfair").select2().val(response.ARVWPF).trigger("change");

    $("#ddlEligibleART").select2().val(response.ET).trigger("change");

    $("#ddlChangeRegimenReason").select2().val(response.TRC).trigger("change");
    $("#ddlStopRegimenReason").select2().val(response.TRC).trigger("change");

    $("#hidARTStartDate").val(response.ARTS);


    CheckDatenAssign(response.ARVED, "dtARTDateEnded", false);

    if (jQuery.isEmptyObject(response.ARTCRMDT) == false) {
        if (response.ARTCRMDT > 0) {
            $("#chkThreapyChange").bootstrapSwitch('state', true);
            $("#txtAreaChangeDiscussMDT").val(response.ARTCRMDTC);
        }
    }

    if (jQuery.isEmptyObject(response.ARTCRMDT) == false) {
        if (response.ARTCRMDT > 0) {
            $("#chkThreapyChange").bootstrapSwitch('state', true);
        }
    }

    if (jQuery.isEmptyObject(response.SSTI) == false) {
        if (response.SSTI > 0) {
            $("#chkSTIScreening").bootstrapSwitch('state', true);

        }
    }

    if (jQuery.isEmptyObject(response.PN) == false) {
        if (response.PN > 0) {
            $("#chkPartnerNotified").bootstrapSwitch('state', true);
        }
    }

    debugger;
    var idGKP;
    $.each(response.KPL, function (index, value) {
        idGKP = value.id;
    });

    $("input:radio[name=rbgKP][value=" + idGKP + "]").attr('checked', 'checked');
    $("input:radio[name=rbgKP]").iCheck('update');

    if (jQuery.isEmptyObject(idGKP) == false && idGKP > 0) {
        $("#chkKeyPopulation").bootstrapSwitch('state', true);
    }
    debugger;
    BindMultiSelectDropDown("ddlPWP", response.PWPL);


    EnableDisableSTIOption(GetSwitchValue("chkSTIScreening"));

    if (jQuery.isEmptyObject(response.ICDGV) == false) {
        $.each(response.ICDGV, function (index, value) {
            if (jQuery.isEmptyObject(value) == false) {

                arrICD10.push({
                    CID: value.CID,
                    CHN: value.CHN,
                    BID: value.BID,
                    BC: value.BC,
                    BN: value.BN,
                    SBID: value.SBID,
                    SBC: value.SBC,
                    SBN: value.SBN,
                    ICDId: value.ICDId,
                    ICDC: value.ICDC,
                    ICDN: value.ICDN
                });
            }
        });

        DrawDataTable("dtlICD10", arrICD10);

    }
    arrNxtAppointment = [];
    if (jQuery.isEmptyObject(response.Appointments) == false) {
        $.each(response.Appointments, function (index, value) {
            if (jQuery.isEmptyObject(value) == false) {

                // parse JSON formatted date to javascript date object
                var dtOSD = new Date(parseInt(value.ANAD.substr(6)));
                // format display date (e.g. 04/10/2012)
                var displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                arrNxtAppointment.push({
                    Id: value.Id,
                    Ptn_Pk: value.Ptn_Pk,
                    LId: value.LId,
                    VId: value.VId,
                    displayDate: displayDate,
                    ANAD: value.ANAD,
                    APurpose: value.APurpose,
                    PurposeN: value.PurposeN,
                    AT: value.AT,
                    SA: value.SA,
                    SAN: value.SAN,
                    Desc: value.Desc,
                    Status: value.Status

                });
            }
        });
        debugger;

        DrawDataTable("dtlNextAppointment", arrNxtAppointment);

    }



    $("#txtICDComments").val(response.Comment);
    $("#txtAreaReminders").val(response.AREM);
    $("#txtAreaReferrals").val(response.PRD);


    CheckDatenAssign(response.AD, "dtNextAppointmentDate", false);

    $("#ddlPurpose").select2().val(response.AR).trigger("change");
    $("#ddlMgtSignature").select2().val(response.MPA.MGTSIG).trigger("change");

    $("#ddlRegimenCode").select2().val(response.RCV).trigger("change");

    var vdata = [];
    debugger;
    $("#ddlPtnClassification").select2();
    var end = new Date();
    var diff = 1;


    if (jQuery.isEmptyObject(response.ARTS) == false) {
        var dtART = new Date(parseInt(response.ARTS.substr(6)));

        // format display date (e.g. 04/10/2012)
        var displayARTDate = $.format.date(dtART, "dd-mm-yyyy");
        var startDay = new Date(displayARTDate);
        diff = new Date(end - startDay);
    }

    // get days
    var days = diff / 1000 / 60 / 60 / 24;
    var IsEnrollDifferenciatedCare = false;

    $.each(arrPatientClassification, function (index, value) {
        if (days < 365) {
            IsEnrollDifferenciatedCare = false;
            if (value.DN == 'Well') {
                vdata.push({ id: value.DId, text: value.DN });
            } else if (value.DN == 'Advance HIV Disease') {
                vdata.push({ id: value.DId, text: value.DN });
            }
        } else {
            IsEnrollDifferenciatedCare = true;
            if (value.DN == 'Stable') {
                vdata.push({ id: value.DId, text: value.DN });
            } else if (value.DN == 'Unstable') {
                vdata.push({ id: value.DId, text: value.DN });
            }

        }
    });

    $("#ddlPtnClassification").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlPtnClassification").select2("val", "0");
    $("#ddlPtnClassification").trigger('change.select2');

    $("#ddlPtnClassification").select2().val(response.Classification).trigger("change");

    $("#txtServiceArea").val($("#hidsrvNm").val());
    $("#txtDRASA").val($("#hidsrvNm").val());

    if (IsEnrollDifferenciatedCare) {
        $("#divEDC1").css("visibility", "visible");
        $("#divEDC1").css("display", "block");

        $("#divEDC2").css("visibility", "visible");
        $("#divEDC2").css("display", "block");

        $("#divEDC3").css("visibility", "visible");
        $("#divEDC3").css("display", "block");

    } else {
        $("#divEDC1").css("visibility", "hidden");
        $("#divEDC1").css("display", "none");

        $("#divEDC2").css("visibility", "hidden");
        $("#divEDC2").css("display", "none");

        $("#divEDC3").css("visibility", "hidden");
        $("#divEDC3").css("display", "none");
    }


    if (jQuery.isEmptyObject(response.ISEDC) == false) {
        if (response.ISEDC > 0) {
            $("#chkDifferenciatedCare").bootstrapSwitch('state', true);

        }
    }
    $("#ddlARTRefillModel").select2().val(response.ARTRefillModel).trigger("change");

    $.hivce.loader('hide');
}

//
// Helper functions
//    
// extract pre-selected IDs from associated element (HTML 'value' attributes or .val() function)
// element: object
function extract_preselected_ids(element) {
    var preselected_ids = [];
    var delimiter = ',';
    if (element.val()) {
        if (element.val().indexOf(delimiter) != -1)
            $.each(element.val().split(delimiter), function () {
                preselected_ids.push({ id: this[0] });
            });
        else
            preselected_ids.push({ id: element.val() });
    }
    return preselected_ids;
};

// find all objects with the pre-selected IDs
// preselected_ids: array of IDs
function find_preselections(preselected_ids) {
    var pre_selections = []
    for (index in arrICDData)
        for (id_index in preselected_ids) {
            var objects = find_object_with_attr(selections[index], { key: 'id', val: preselected_ids[id_index].id })
            if (objects.length > 0)
                pre_selections = pre_selections.concat(objects);
        }
    return pre_selections;
};

// check if the given object has the specified attribute
// object: object
// attr: 
function find_object_with_attr(object, attr) {
    var objects = [];
    for (var index in object) {
        if (!object.hasOwnProperty(index)) // make sure object has a property. Otherwise, skip to next object.
            continue;
        if (object[index] && typeof object[index] == 'object') { // recursive call into children objects.
            objects = objects.concat(find_object_with_attr(object[index], attr));
        }
        else if (index == attr['key'] && object[attr['key']] == attr['val'])
            objects.push(object);
    }
    return objects;
}

function BindSubsituationInterruption(val) {

    $("#ddlRegimenCode").prop("disabled", false);
    if (val == 95) {

        $("#divRegimenCodes").css("visibility", "visible");
        $("#divRegimenCodes").css("display", "block");
        $("#ddlRegimenCode").prop("disabled", true);

        $("#divEligibleART").css("visibility", "hidden");
        $("#divEligibleART").css("display", "none");

        $("#divChangeRegimen").css("visibility", "hidden");
        $("#divChangeRegimen").css("display", "none");

        $("#divARTStop").css("visibility", "hidden");
        $("#divARTStop").css("display", "none");
    }
    else if (val == 96) {

        $("#divRegimenCodes").css("visibility", "visible");
        $("#divRegimenCodes").css("display", "block");

        $("#divEligibleART").css("visibility", "hidden");
        $("#divEligibleART").css("display", "none");

        $("#divChangeRegimen").css("visibility", "hidden");
        $("#divChangeRegimen").css("display", "none");

        $("#divARTStop").css("visibility", "hidden");
        $("#divARTStop").css("display", "none");
    }
    else if (val == 97) {


        $("#divEligibleART").css("visibility", "visible");
        $("#divEligibleART").css("display", "block");

        $("#divRegimenCodes").css("visibility", "visible");
        $("#divRegimenCodes").css("display", "block");

        $("#divChangeRegimen").css("visibility", "hidden");
        $("#divChangeRegimen").css("display", "none");

        $("#divARTStop").css("visibility", "hidden");
        $("#divARTStop").css("display", "none");
    }
    else if (val == 98) {
        $("#divChangeRegimen").css("visibility", "visible");
        $("#divChangeRegimen").css("display", "block");

        $("#divRegimenCodes").css("visibility", "visible");
        $("#divRegimenCodes").css("display", "block");

        $("#divARTStop").css("visibility", "hidden");
        $("#divARTStop").css("display", "none");

        $("#divEligibleART").css("visibility", "hidden");
        $("#divEligibleART").css("display", "none");
    }
    else if (val == 99) {
        $("#divChangeRegimen").css("visibility", "hidden");
        $("#divChangeRegimen").css("display", "none");

        $("#divARTStop").css("visibility", "visible");
        $("#divARTStop").css("display", "block");

        $("#divEligibleART").css("visibility", "hidden");
        $("#divEligibleART").css("display", "none");

        $("#divRegimenCodes").css("visibility", "hidden");
        $("#divRegimenCodes").css("display", "none");
    }
    else {
        $("#divChangeRegimen").css("visibility", "hidden");
        $("#divChangeRegimen").css("display", "none");

        $("#divARTStop").css("visibility", "hidden");
        $("#divARTStop").css("display", "none");

        $("#divEligibleART").css("visibility", "hidden");
        $("#divEligibleART").css("display", "none");

        $("#divRegimenCodes").css("visibility", "hidden");
        $("#divRegimenCodes").css("display", "none");


    }
}
function ReInitialiseMtXDatatable(from) {
    $('#dtlICD10').DataTable({
        "aaData": arrICD10,
        "bSort": false,
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "autoWidth": true,
        "destroy": true,
        "sSearch": false,
        "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["ICDId"];
                    }
                },
                 { bSortable: false,
                     mRender: function (data, type, full) {
                         return full["ICDN"];
                     }
                 },
                 { bSortable: false,
                     mRender: function (data, type, full) {
                         return full["CHN"];
                     }
                 },
                 { bSortable: false,
                     mRender: function (data, type, full) {
                         return full["BC"] + " - " + full["BN"];
                     }
                 },
                 { bSortable: false,
                     mRender: function (data, type, full) {
                         return full["SBC"] + " - " + full["SBN"];
                     }
                 },
                 {
                     bSortable: false,
                     mRender: function (data, type, full) {
                         return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteICD10Row('" + full["ICDId"] + "');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                     }
                 }
            ]
    });
}


function ReInitialiseAppointmentsDatatable() {
    $('#dtlNextAppointment').DataTable({
        "aaData": arrNxtAppointment,
        "bSort": false,
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "autoWidth": true,
        "aoColumns": [
            { bSortable: false,
                mRender: function (data, type, full) {
                    return full["Id"];
                }
            },
            { bSortable: false,
                mRender: function (data, type, full) {
                    return full["displayDate"];
                }
            },
            { bSortable: false,
                mRender: function (data, type, full) {
                    return full["SAN"];
                }
            },
            { bSortable: false,
                mRender: function (data, type, full) {
                    return full["PurposeN"];
                }
            },
            { bSortable: false,
                mRender: function (data, type, full) {
                    return full["Desc"];
                }
            },
            { bSortable: false,
                mRender: function (data, type, full) {
                    return full["Status"];
                }
            },
            {
                bSortable: false,
                mRender: function (data, type, full) {
                    return "";
                    //return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteICD10Row('" + full["ICDId"] + "');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                }
            }
        ]
    });
}

function AddICD10() {

    var ddlICD = $("#ddlICD").select2("val");
    var data = $("#ddlICD").select2('data')[0];
    //var txtICDComments = $("#txtICDComments").val();

    if (ddlICD == null) {
        customAlert("Please select ICD Diagnosis from the list");
        return;
    }

    var chkData = $.grep(arrICD10, function (e) { return e.ICDID == data.id; });
    if (jQuery.isEmptyObject(chkData) == true) {
        var baseData = $.grep(arrICDBaseData, function (e) { return e.ICDId == data.id; });
        if (jQuery.isEmptyObject(baseData) == false) {

            arrICD10.push({
                CID: baseData[0].CID,
                CHN: baseData[0].CHN,
                BID: baseData[0].BID,
                BC: baseData[0].BC,
                BN: baseData[0].BN,
                SBID: baseData[0].SBID,
                SBC: baseData[0].SBC,
                SBN: baseData[0].SBN,
                ICDId: baseData[0].ICDId,
                ICDC: baseData[0].ICDC,
                ICDN: baseData[0].ICDN
            });
        }
    }
    else {
        customAlert("Already exits in the table.");
    }

    DrawDataTable("dtlICD10", arrICD10);

}


function DeleteICD10Row(icdId) {
    $.each(arrICD10, function (i, el) {
        if (this.ICDId == icdId) {
            arrICD10.splice(i, 1);
        }
    });
    var table = $("#dtlICD10").DataTable();
    table.rows().remove().draw();

    DrawDataTable("dtlICD10", arrICD10);
}

function SaveManagementx() {
    $.hivce.loader('show');

    var managementxData = PrepareManagementxData();

    PostManagementxData(managementxData);
}


function PrepareManagementxData() {

    rowManagementxData = [];
    //var rdoNutritionGroup = $("input:radio[name=NutritionGroup]:checked").val();
    //var uNutritionGroup = (rdoNutritionGroup === undefined) ? 0 : rdoNutritionGroup;

    var chkARTSideEffect = GetSwitchValue("chkARTSideEffect");

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

    var ddlCTXAdhere = $("#ddlCTXAdhere").select2("val");
    if (ddlCTXAdhere == null) {
        ddlCTXAdhere = 0;
    }

    var ddlCTXWhypoorfair = $("#ddlCTXWhypoorfair").select2("val");
    if (ddlCTXWhypoorfair == null) {
        ddlCTXWhypoorfair = 0;
    }

    var ddlARVAdhere = $("#ddlARVAdhere").select2("val");
    if (ddlARVAdhere == null) {
        ddlARVAdhere = 0;
    }

    var ddlARVWhypoorfair = $("#ddlARVWhypoorfair").select2("val");
    if (ddlARVWhypoorfair == null) {
        ddlARVWhypoorfair = 0;
    }

    var ddlsubsituationInterruption = $("#ddlsubsituationInterruption").select2("val");
    if (ddlsubsituationInterruption == null) {
        ddlsubsituationInterruption = 0;
    }

    var ddlRegimenCode = $("#ddlRegimenCode").select2("val");
    if (ddlRegimenCode == null) {
        ddlRegimenCode = 0;
    }

    var ddlEligibleART = $("#ddlEligibleART").select2("val");
    if (ddlEligibleART == null) {
        ddlEligibleART = 0;
    }

    var ddlChangeRegimenReason = $("#ddlChangeRegimenReason").select2("val");
    if (ddlChangeRegimenReason == null) {
        ddlChangeRegimenReason = 0;
    }

    var ddlStopRegimenReason = $("#ddlStopRegimenReason").select2("val");
    if (ddlStopRegimenReason == null) {
        ddlStopRegimenReason = 0;
    }

    var dtARTDateEnded = GetDateData("dtARTDateEnded", true);
    debugger;
    var chkThreapyChange = GetSwitchValue("chkThreapyChange");

    var txtAreaChangeDiscussMDT = $("#txtAreaChangeDiscussMDT").val();

    var chkKeyPopulation = GetSwitchValue("chkKeyPopulation");

    var rbgKP = $("input:radio[name=rbgKP]:checked").val();
    var urbgKP = (rbgKP === undefined) ? 0 : rbgKP;

    var chkSTIScreening = GetSwitchValue("chkSTIScreening");
    var chkPartnerNotified = GetSwitchValue("chkPartnerNotified");

    var ddlPWP = $("#ddlPWP").select2("val");
    if (ddlPWP == null) {
        ddlPWP = 0;
    }

    var ddlICD = $("#ddlICD").select2("val");
    if (ddlICD == null) {
        ddlICD = 0;
    }


    var txtAreaReminders = $("#txtAreaReminders").val();
    var txtAreaReferrals = $("#txtAreaReferrals").val();

    var dtNextAppointmentDate = GetDateData("dtNextAppointmentDate", true);
    var ddlPurpose = $("#ddlPurpose").select2("val");
    if (ddlPurpose == null) {
        ddlPurpose = 0;
    }

    var ddlMgtSignature = $("#ddlMgtSignature").select2("val");
    if (ddlMgtSignature == null) {
        ddlMgtSignature = 0;
    }

    var ddlPtnClassification = $("#ddlPtnClassification").select2("val");
    if (ddlPtnClassification == null) {
        ddlPtnClassification = 0;
    }

    var ddlARTRefillModel = $("#ddlARTRefillModel").select2("val");
    if (ddlARTRefillModel == null) {
        ddlARTRefillModel = 0;
    }

    var txtICDComments = $("#txtICDComments").val();


    var arrMADE = [];
    arrMADE.push({
        Ptn_Pk: 0,
        LId: 0,
        VId: 0,
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
        MMAS8SG: txtMMAS8Suggestion
    });

    rowManagementxData.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        IsASE: chkARTSideEffect,
        STSE: GetControlMultiSelectValues("ddlSortTermSideEffect"),
        LTSE: GetControlMultiSelectValues("ddlLongTermSideEffect"),
        MADE: arrMADE[0],
        CTXA: ddlCTXAdhere,
        CTXW: ddlCTXWhypoorfair,
        ARVA: ddlARVAdhere,
        ARVW: ddlARVWhypoorfair,
        SI: ddlsubsituationInterruption,
        RCV: ddlRegimenCode,
        EART: ddlEligibleART,
        CRR: ddlChangeRegimenReason,
        SRR: ddlStopRegimenReason,
        ARTDE: GetDateData("dtARTDateEnded", true),
        TC: chkThreapyChange,
        TCC: txtAreaChangeDiscussMDT,
        KP: chkKeyPopulation,
        KPO: urbgKP,
        STIS: chkSTIScreening,
        PN: chkPartnerNotified,
        PWP: GetControlMultiSelectValues("ddlPWP"),
        AREM: txtAreaReminders,
        AREF: txtAreaReferrals,
        //NAD: GetDateData("dtNextAppointmentDate", true),
        //Purpose: ddlPurpose,
        // MGTS: ddlMgtSignature,
        ICDV: arrICD10,
        ICDC: txtICDComments,
        Classification: ddlPtnClassification,
        ISEDC: GetSwitchValue("chkSTIScreening"),
        ARTRefillModel: ddlARTRefillModel,
        Appointments: arrNxtAppointment
    });

    return rowManagementxData[0];
}



function CheckMXBlankValues() {
    errorMsg = '';
    var errorField = '';
    var ddlCTXAdhere = $("#ddlCTXAdhere").select2("val");
    var ddlARVAdhere = $("#ddlARVAdhere").select2("val");
    var ddlPWP = $("#ddlPWP").select2("val");

    var visitType = $("#ddlVisitType").select2("val");
    var visitTypeData = $("#ddlVisitType").select2('data')[0];


    if (visitType != null) {
        if (visitTypeData.text.toUpperCase().indexOf("SUPPORTER") == -1) {

            if (ddlCTXAdhere == null) {
                errorField += 'CTX/Dapson: Adherence ';
            }

            var hidARTStartDate = $("#hidARTStartDate").val();
            if (ddlARVAdhere == null && hidARTStartDate.length > 0) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'ARV: Adherence ';
            }
        }
    }

    var ddlsubsituationInterruption = $("#ddlsubsituationInterruption").select2("val");
    if (ddlsubsituationInterruption == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Substitutions/Interruptions ';
    }

    if (ddlsubsituationInterruption == 96 || ddlsubsituationInterruption == 97 || ddlsubsituationInterruption == 98) {
        var ddlRegimenCode = $("#ddlRegimenCode").select2("val");
        if (ddlRegimenCode == null) {
            if (errorField.length > 1) {
                errorField += ', ';
            }
            errorField += 'Regimen Code ';
        }
    }

    var age = $("#hidDOB").val();
    if (age > 14) {

        if (ddlPWP == null) {
            if (errorField.length > 1) {
                errorField += ', ';
            }
            errorField += 'Prevention with positives ';
        }
    }

    var chkKeyPopulation = GetSwitchValue("chkKeyPopulation");

    var rbgKP = $("input:radio[name=rbgKP]:checked").val();
    var urbgKP = (rbgKP === undefined) ? 0 : rbgKP;
    if (chkKeyPopulation > 0) {
        if (urbgKP == 0) {
            if (errorField.length > 1) {
                errorField += ', ';
            }
            errorField += 'Key Population ';
        }
    }

    //    if ($("#dtNextAppointmentDate").val().length == 0) {
    //        if (errorField.length > 1) {
    //            errorField += ', ';
    //        }
    //        errorField += 'Next Appointment date ';
    //    }


    if (isNaN(errorField)) {
        errorMsg = errorField + 'cannot be blank.';
    }
    return errorMsg;
}

function PostManagementxData(rowData) {

    var action = "savemanagementx";
    var rowData1 = rowData;
    var errorMsg = CheckMXBlankValues();
    activePage = getPageName() + '.aspx';

    if (isNaN(errorMsg)) {
        customAlert(errorMsg);
        return;
    }
    else {
        $.hivce.loader('show');
        var responseObject = null;
        $.ajax({
            type: "POST",
            //url: "ClinicalEncounter.aspx?data=" + action,
            url: activePage + "?data=" + action,
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
                    //GetManagementData();
                    customAlert("Management tab " + dataSuccessMessage.toLowerCase());
                    window.location.assign("../ClinicalForms/frmPatient_History.aspx?&sts=0");
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
            error: function (xhr, textStatus, errorThrown) {
                ShowErrorMessage(msg);
            }
            //error: function (msg) {
            //    ShowErrorMessage(msg);
            //}
        });
    }
}

function AddAppointment(ref) {

    var ddlP;
    var data;
    var dtDate;
    var desc;
    var ctrlDT;
    if (ref == '0') {
        dtDate = GetDateData("dtNextAppointmentDate", true);
        ddlP = $("#ddlPurpose").select2("val");
        data = $("#ddlPurpose").select2('data')[0];
        desc = $("#txtNADDescription").val();
        ctrlDT = "dtNextAppointmentDate";

    } else {

        dtDate = GetDateData("dtDrugRefillAppointmentDate", true);
        ddlP = $("#ddlDRAPurpose").select2("val");
        data = $("#ddlDRAPurpose").select2('data')[0];
        desc = $("#txtDRADescription").val();
        ctrlDT = "dtDrugRefillAppointmentDate";
    }



    if (ddlP == null) {
        customAlert("Please select purpose from the list");
        return;
    }
    if (ref == '0') {
        if ($("#dtNextAppointmentDate").val().length == 0) {
            customAlert("Please enter appointment date");
            return;
        }
    } else {
        if ($("#dtDrugRefillAppointmentDate").val().length == 0) {
            customAlert("Please enter appointment date");
            return;
        }
    }
    debugger;

    var chkData = false;

    $.each(arrNxtAppointment, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            if (jQuery.isEmptyObject(arrD.NAD) == false) {
                var dt = new Date(parseInt(arrD.NAD.substr(6))).toString();
                //console.log(dt); // Mon Jul 16 2012 15:21:09 GMT+0530 (India Standard Time)
                dt = dt.substr(0, dt.indexOf('GMT'));
                //console.log(dt); // Mon Jul 16 2012 15:21:09 
                osd = convertToJSONDate(dt, true);

                var dtOSD = new Date(parseInt(osd.substr(6)));
                dtOSD.setHours(0, 0, 0, 0);
                var dt = new Date(parseInt(dtDate.substr(6)));
                dt.setHours(0, 0, 0, 0);
                if (dtOSD.getTime() === dt.getTime()) {
                    chkData = true;
                }
            }
        }
    });

    if (chkData == false) {

        arrNxtAppointment.push({
            Id: 0,
            Ptn_Pk: 0,
            LId: 0,
            VId: 0,
            displayDate: $("#" + ctrlDT).val(),
            ANAD: dtDate,
            APurpose: data.id,
            PurposeN: data.text,
            AT: ref,
            SA: $("#hidMOD").val(),
            SAN: $("#hidsrvNm").val(),
            Desc: desc,
            Status: "--"

        });

        //arrChronicDisorders.push({ PCID: data.id, PC: data.text, OSD: dtOnsetDate, IsActive: IsA, IsAVal: IsAVal, displayDate: $("#dtCCOnsetDate").val(), VId: 0 });
        //arrChronicDisordersUI.push([data.text, $("#dtCCOnsetDate").val(), IsAVal, data.id]);
    }
    else {
        customAlert("Already exits in the table.");
    }

    DrawDataTable("dtlNextAppointment", arrNxtAppointment);
}