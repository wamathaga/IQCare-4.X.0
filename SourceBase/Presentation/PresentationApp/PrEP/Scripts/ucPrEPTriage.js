var zscoreDetails;
var arrFacilityData = [];
var arrFacilityBaseData = [];
var arrCountryData = [];

function InitTriageControls(response) {
    debugger;
    window.scrollTo(0, 0);
    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });

    $("#dtVisitDate").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");
    $("#dtTransferInDate").datepicker({
        autoclose: true
    });
    $("#dtDateStatedArt").datepicker({
        autoclose: true
    });

    $("#ddlVisitType").select2();
    $("#ddlSchoolClass").select2();
    $("#ddlReferredFrom").select2();
    $("#ddlReferredFrom").select2().on('change', function (e) {
        var $this = $(this);
        EnableDisableReferredFrom();
    });
    $("#ddlFacility").select2();

    $("#txtReferredFrom").prop('disabled', true);

    $("#chkIsInSchool").bootstrapSwitch('state', false);
    $("#chkIsInSchool").on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });
    $("#chkKeyPopulation").bootstrapSwitch('state', false);
    $("#chkKeyPopulation").on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });

    $("#chkTransferIn").bootstrapSwitch('state', false);
    var disDiv = "divTransferIn";
    $("#" + disDiv).css("visibility", "hidden");
    $("#" + disDiv).css("display", "none");

    $("#txtNursesComment").on('blur', function () {
        debugger;
        $("span.bootstrap-maxlength").empty();
        $("span.bootstrap-maxlength").removeClass("bootstrap-maxlength labelTextBox label");
        $("span.bootstrap-maxlength").removeAttr("style").attr("style", "display:none");
    });

    BindTriageCombo(response);
    BindControls(response);
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
    arrCountryData = data;

    debugger;
    data = [];
    arrFacilityData = [];
    arrFacilityBaseData = response.FL;
    $.each(response.FL, function (index, value) {
        arrFacilityData.push({ id: value.Id, text: value.CN + ' - ' + value.Name });
    });


    var urlPath = getPageName() + '.aspx?data=getfacility';

    $("#ddlFacility").select2({
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
                debugger;
                if (jQuery.isEmptyObject(params.term) == false) {
                    $.each(dataicd, function (index, value) {
                        var txtVal = value.CN + ' - ' + value.Name;
                        if (params.term.length == 0 || txtVal.toUpperCase().indexOf(params.term.toUpperCase()) >= 0) {
                            dataObj.push({ id: value.Id, text: txtVal });
                        }
                        if (params.term.length == 0 || value.MFL.toUpperCase().indexOf(params.term.toUpperCase()) >= 0) {
                            dataObj.push({ id: value.Id, text: txtVal });
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
    }).on('change', function (e) {
        var $this = $(this);
        if (jQuery.isEmptyObject($this) == false) {
            console.log($this.val());
            GetFacilityDetails($this.val());
        }
    });

    $("#ddlFacility").select2("val", "0");
    $("#ddlFacility").trigger('change.select2');


    data = [];
    $.each(response.UDL, function (index, value) {
        data.push({ id: value.UserID, text: value.UserName + ' - ' + value.Designation });
    });

    $("#ddlSignature").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlSignature").select2("val", "0");
    $("#ddlSignature").trigger('change.select2');

    data = [];
    $.each(response.Regimen, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

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

    $("#ddlCurrentRegimen").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlCurrentRegimen").select2("val", "0");
    $("#ddlCurrentRegimen").trigger('change.select2');

    data = [];
    $.each(response.WHOS, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlTARTWHOStage").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlTARTWHOStage").select2("val", "0");
    $("#ddlTARTWHOStage").trigger('change.select2');

    data = [];
    $.each(response.PRef, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlReferredFrom").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlReferredFrom").select2("val", "0");
    $("#ddlReferredFrom").trigger('change.select2');


    data = [];
    $.each(response.UDL, function (index, value) {
        data.push({ id: value.UserID, text: value.UserName + ' - ' + value.Designation });
    });

    $("#ddlTSMgtSignature").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlTSMgtSignature").select2("val", "0");
    $("#ddlTSMgtSignature").trigger('change.select2');

    data = $.grep(response.PUR, function (e) { return e.CN == "PUR"; });
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlTSPurpose").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlTSPurpose").select2("val", "0");
    $("#ddlTSPurpose").trigger('change.select2');
}

function BindControls(response) {

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

    CheckDatenAssign(response.OV.VD, "dtVisitDate", false);
    $("#hidVId").val(response.OV.VId);
    debugger;
    $("#ddlVisitType").val(response.OV.TOV).trigger("change");
    $("#ddlSignature").val(response.OV.SG).trigger("change");

    if (jQuery.isEmptyObject(response.HIV.HIVED) == false) {

        $("#dtHivEnroll").prop('disabled', true);
        CheckDatenAssign(response.HIV.HIVED, "dtHivEnroll", false);
    }

    CheckDatenAssign(response.HIV.HIVD, "dtHivConfirm", false);
    CheckDatenAssign(response.HIV.ARTTD, "dtTransferDate", false);
    $("#txtHIVPositiveWhere").val(response.HIV.HIVCW);

    //debugger;
    var baseData = $.grep(arrFacilityData, function (e) { return e.id == response.HIV.ARTTF; });
    if (jQuery.isEmptyObject(baseData) == false) {
        //$("#ddlFacility").select2().val(baseData[0].Id).trigger("change");
        $("#ddlFacility").empty().append("<option value=\"" + baseData[0].id + "\" selected=\"selected\" >" + baseData[0].text + "</option>");
        //.val(baseData[0].Id).trigger("change");
        var facBaseData = $.grep(arrFacilityBaseData, function (e) { return e.Id == response.HIV.ARTTF; });
        if (jQuery.isEmptyObject(facBaseData) == false) {
            $("#txtMFLCode").val(facBaseData[0].MFL);
            $("#txtDistrict").val(facBaseData[0].CN);
        }
    }

    $("#ddlFacility").val(response.HIV.ARTTF).trigger("change");
    $("#ddlDistrict").val(response.HIV.DIST).trigger("change");
    CheckDatenAssign(response.HIV.ARTSD, "dtDateStatedArt", false);
    debugger;
    $("#chkTransferIn").bootstrapSwitch('state', ConvertValueBool(response.HIV.TRAN));
    CheckDatenAssign(response.HIV.HIVCPD, "dtHivConfirm", false);
    $("#txtMFLCode").val(response.HIV.MFL);
    $("#txtDistrict").val(response.HIV.DIST);

    //    $("#ddlARTStartRegimen").val(response.HIV.ARTS.FLR).trigger("change");
    //    $("#ddlCurrentRegimen").val(response.HIV.ARTS.CR).trigger("change");
    //    $("#ddlTARTWHOStage").val(response.HIV.ARTS.WS).trigger("change");
    //    $("#txtTCD4Count").val(response.HIV.ARTS.CD4);
    //    $("#txtTMUAC").val(response.HIV.ARTS.MUAC);
    //    $("#txtTHeight").val(response.HIV.ARTS.HED);
    //    $("#txtTWeight").val(response.HIV.ARTS.WE);
    //    CalcualteBMI('hidPAYM', 'txtTWeight', 'txtTHeight', 'txtTBMI', 'lblTBMIClassification');
    //    $("#txtBLViralLoad").val(response.HIV.ARTS.BLVL);
    //    CheckDatenAssign(response.HIV.ARTS.BLVLD, "dtBLViralLoadDate", false);

    arrPV = response.PV;

    $("#txtMM").val(response.PV.BPD);
    $("#txtHG").val(response.PV.BPS);
    $("#txtTemperature").val(response.PV.TEMP);
    $("#txtRR").val(response.PV.RR);
    $("#txtHRate").val(response.PV.HR);
    $("#txtHeadCircumference").val(response.PV.HC);
    $("#txtHeight").val(response.PV.Height);
    $("#txtWeight").val(response.PV.Weight);
    var hidVId = $("#hidVId").val();
    if (hidVId > 0) {
        $("#txtWeight").val(response.PV.Weight);
    }
    $("#txtMUAC").val(response.PV.MUAC);
    $("#txtWAgeZScore").val(response.PV.WFA);
    $("#txtHAgeZScore").val(response.PV.WFH);
    $("#txtBMIZScore").val(response.PV.BMIz);
    $("#txtNursesComment").val(response.PV.NC);
    $("#txtSP").val(response.PV.SP);

    $("#hidL").val(response.L);
    $("#hidM").val(response.M);
    $("#hidS").val(response.S);

    $("#hidEIVID").val(response.EIVID);

    $("#hidEIVD").val(response.EIVD);

    var age = $("#hidDOB").val();
    if (age <= 12) {
        $("#rowCareGiver").css("visibility", "visible");
        $("#rowCareGiver").css("display", "block");
    }
    else {
        $("#rowCareGiver").css("visibility", "hidden");
        $("#rowCareGiver").css("display", "none");
    }

    var visitType = $("#ddlVisitType").select2("val");
    var visitTypeData = $("#ddlVisitType").select2('data')[0];


    if (visitType != null) {
        if (visitTypeData.text.toUpperCase().indexOf("INITIAL") == -1) {
            // $("#dtHivConfirm").prop('disabled', true);
            // $("#dtHivEnroll").prop('disabled', true);
        }

    }

    $("#txtReferredFrom").val(response.RFO);
    $("#ddlReferredFrom").val(response.RF).trigger("change");

    CheckDatenAssign(response.AD, "dtTSNextAppointmentDate", false);
    $("#ddlTSPurpose").select2().val(response.AR).trigger("change");
    $("#ddlTSMgtSignature").select2().val(response.MGTS).trigger("change");


    GetZScoreDetails();

    CalcualteBMI('hidPAYM', 'txtWeight', 'txtHeight', 'txtBMI', 'lblBMIClassification');
    HighLightAbnormalValues('temp', 'txtTemperature');
    VisibilityZScore();



}


function EnableDisableReferredFrom() {
    if ($("#ddlReferredFrom").select2('data').length > 0) {
        var ddlReferredFrom = $("#ddlReferredFrom").select2('data')[0];

        if (ddlReferredFrom.text.toUpperCase().indexOf("OTHER") > -1) {
            $("#txtReferredFrom").prop('disabled', false);
        }
        else {
            $("#txtReferredFrom").prop('disabled', true);
        }

    }
}

function EnableDisableHIVECareDiv(val) {
    if ($("#ddlVisitType").select2('data').length > 0) {
        var theSelection = $("#ddlVisitType").select2('data')[0];

        if (theSelection.text.toUpperCase().indexOf("INITIAL") == -1) {

            if (theSelection.text.toUpperCase().indexOf("TREATMENT") == -1) {

                $("#divReferredFrom").css("display", "none");
                $("#divReferredFrom").css("visibility", "hidden");

                enableAlreadySavedTabs(currAST);
            }
            else {

                $('#a_PA').addClass('disabled');
                $('#a_PI').addClass('disabled');
            }

        }
        else {

            $("#divReferredFrom").css("display", "block");
            $("#divReferredFrom").css("visibility", "visible");
            enableAlreadySavedTabs(currAST);
        }

    }
}




$.fn.select2.amd.require(['select2/compat/matcher'], function (oldMatcher) {
    $("#ddlFacility").select2({
        matcher: oldMatcher(matchStart)
    });
});

function matchStart(arrFacilityData, text) {
    if (text.toUpperCase().indexOf(term.toUpperCase()) == 0) {
        return true;
    }
    return false;
}

function GetFacilityDetails(val) {
    //debugger;
    var facData = $.grep(arrFacilityBaseData, function (e) { return e.Id == val; });
    if (jQuery.isEmptyObject(facData) == false) {
        $("#txtMFLCode").val(facData[0].MFL);
        $("#txtDistrict").val(facData[0].CN);
    }
}





function GetZScoreDetails() {
    activePage = getPageName() + '.aspx';

    //debugger;
    var postData;
    $.ajax({
        type: "POST",
        url: activePage + "?data=getzscore",
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
                zscoreDetails = response;
                CalcualteBMIzScore();
            }
        },
        error: function (msg) {
        }
    });
}

function CalcualteBMIzScore() {
    CalculateWeightZScore();
    CalculateHeightZScore();
    CalculateBMIZScore();
    $.hivce.loader('hide');
}

function VisibilityZScore() {
    var hidPAYM = $("#hidPAYM").val();

    if (hidPAYM >= 0 && hidPAYM <= 19) {

        if (hidPAYM >= 0 && hidPAYM <= 5) {

            $("#divWA").css("visibility", "visible");
            $("#divWA").css("display", "block");
            $("#divWAV").css("visibility", "visible");
            $("#divWAV").css("display", "block");
            $("#divHA").css("visibility", "visible");
            $("#divHA").css("display", "block");
            $("#divHAV").css("visibility", "visible");
            $("#divHAV").css("display", "block");
        } else {
            $("#divWA").css("visibility", "hidden");
            $("#divWA").css("display", "none");
            $("#divWAV").css("visibility", "hidden");
            $("#divWAV").css("display", "none");
            $("#divHA").css("visibility", "hidden");
            $("#divHA").css("display", "none");
            $("#divHAV").css("visibility", "hidden");
            $("#divHAV").css("display", "none");
        }
        $("#divBMIZ").css("visibility", "visible");
        $("#divBMIZ").css("display", "block");
        $("#divBMIZV").css("visibility", "visible");
        $("#divBMIZV").css("display", "block");


        $("#divBMILable").css("visibility", "hidden");
        $("#divBMILable").css("display", "none");
        $("#divBMIText").css("visibility", "hidden");
        $("#divBMIText").css("display", "none");
    }
    else {
        $("#divWA").css("visibility", "hidden");
        $("#divWA").css("display", "none");
        $("#divWAV").css("visibility", "hidden");
        $("#divWAV").css("display", "none");
        $("#divHA").css("visibility", "hidden");
        $("#divHA").css("display", "none");
        $("#divHAV").css("visibility", "hidden");
        $("#divHAV").css("display", "none");
        $("#divBMIZ").css("visibility", "hidden");
        $("#divBMIZ").css("display", "none");
        $("#divBMIZV").css("visibility", "hidden");
        $("#divBMIZV").css("display", "none");

        $("#divBMILable").css("visibility", "visible");
        $("#divBMILable").css("display", "block");
        $("#divBMIText").css("visibility", "visible");
        $("#divBMIText").css("display", "block");
    }

}

//function CalcualteBMI() {
//    var height = $("#txtHeight").val();
//    var weight = $("#txtWeight").val();
//    var txtBMI = $("#txtBMI").val();
//    //var weight = document.getElementById(txtWeight).value;
//    //var height = document.getElementById(txtHeight).value;
//    if (weight == "" || height == "") {
//        weight = 0;
//        height = 0;
//        $("#txtBMI").val("")
//        //document.getElementById(txtBMI).value = "";
//    }
//    else {
//        var BMI = weight / ((height / 100) * (height / 100));
//        BMI = BMI.toFixed(2);
//        //document.getElementById(txtBMI).value = BMI;
//        $("#txtBMI").val(BMI)

//    }
//}

function CheckTriBlankValues() {
    var ddlCtrl = $("#ddlVisitType").select2("val");
    var data = $("#ddlVisitType").select2('data')[0];
    errorMsg = '';
    var errorField = '';

    if ($("#dtVisitDate").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Visit date ';
    }

    if (ddlCtrl == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Visit type ';
    }

    if (data != null) {
        if (data.text.toLowerCase().indexOf("initial") >= 0) {
            //            if ($("#dtHivConfirm").val().length == 0) {
            //                if (errorField.length > 1) {
            //                    errorField += ', ';
            //                }
            //                errorField += 'HIV confirm date ';
            //            }

            //            if ($("#dtHivEnroll").val().length == 0) {
            //                if (errorField.length > 1) {
            //                    errorField += ', ';
            //                }
            //                errorField += 'HIV enrolled date ';
            //            }
        }
    }

    var transferIn = GetSwitchValue("chkTransferIn");
    if (transferIn > 0) {

        var ddlTransferFrom = $("#ddlFacility").select2("val");

        if (ddlTransferFrom == null) {
            if (errorField.length > 1) {
                errorField += ', ';
            }
            errorField += 'Facility transfer from ';
        }

        if ($("#dtDateStatedArt").val().length == 0) {
            if (errorField.length > 1) {
                errorField += ', ';
            }
            errorField += 'ART start date ';
        }

        var ddlARTStartRegimen = $("#ddlARTStartRegimen").select2("val");
        if (ddlARTStartRegimen == null) {
            if (errorField.length > 1) {
                errorField += ', ';
            }
            errorField += 'First line regimen ';
        }

        //        var ddlCurrentRegimen = $("#ddlCurrentRegimen").select2("val");
        //        if (ddlCurrentRegimen == null) {
        //            if (errorField.length > 1) {
        //                errorField += ', ';
        //            }
        //            errorField += 'Current regimen ';
        //        }
    }


    if ($("#txtHeight").val().length == 0 || $("#txtHeight").val() == "0") {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Height ';
    }

    if ($("#txtWeight").val().length == 0 || $("#txtWeight").val() == "0") {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Weight ';
    }
    var age = $("#hidDOB").val();
    if (age > 14) {
        if ($("#txtMM").val().length == 0 || $("#txtMM").val() == "0") {
            if (errorField.length > 1) {
                errorField += ', ';
            }
            errorField += 'BPDiastolic ';
        }

        if ($("#txtHG").val().length == 0 || $("#txtHG").val() == "0") {
            if (errorField.length > 1) {
                errorField += ', ';
            }
            errorField += 'BPSystolic ';
        }
    }

    if (isNaN(errorField)) {
        errorMsg = errorField + 'cannot be blank.';
    }

    return errorMsg;
}

function CheckTSTriBlankValues() {
    var ddlCtrl = $("#ddlVisitType").select2("val");
    var data = $("#ddlVisitType").select2('data')[0];
    errorMsg = '';
    var errorField = '';

    if ($("#dtVisitDate").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Visit date ';
    }

    if (ddlCtrl == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Visit type ';
    }

    if ($("#dtTSNextAppointmentDate").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Next Appointment date ';
    }

    if (isNaN(errorField)) {
        errorMsg = errorField + 'cannot be blank.';
    }
    return errorMsg;
}

function SaveTriageData(IsFind) {

    activePage = getPageName() + '.aspx';
    var action = "savetriage";
    var errorMsg = CheckTriBlankValues();
    if (isNaN(errorMsg)) {
        customAlert(errorMsg);
        return;
    }
    else {
        var hidEIVID = $("#hidEIVID").val();
        var hidEIVD = $("#hidEIVD").val();

        if ($("#hidVId").val() == 0) {
            var visitType = $("#ddlVisitType").select2("val");
            var visitTypeData = $("#ddlVisitType").select2('data')[0];

            if ($("#hidEIVD").val().length > 0) {
                if (visitTypeData.text.toUpperCase().indexOf("INITIAL") >= 0) {
                    // parse JSON formatted date to javascript date object
                    var dtOSD = new Date(parseInt(hidEIVD.substr(6)));

                    // format display date (e.g. 04/10/2012)
                    var displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                    bootbox.confirm("Initial visit already exists on day " + displayDate + ", do you want to create another initial visit?", function (e) {
                        if (e) {
                            //debugger;
                            PostTriageData(IsFind);
                            isPreviousTabSave = true;
                        }
                    });
                }
                else {
                    PostTriageData(IsFind);
                    isPreviousTabSave = true;
                }
            }
            else {
                if (visitType != null) {
                    if (visitTypeData.text.toUpperCase().indexOf("INITIAL") > -1) {
                        PostTriageData(IsFind);
                        isPreviousTabSave = true;
                    }
                    else {
                        customAlert(initialTypeMessage);
                    }
                }
            }
        }
        else {
            PostTriageData(IsFind);
            isPreviousTabSave = true;
        }
    }
}

function SaveTSTriageData() {

    activePage = getPageName() + '.aspx';
    var action = "savetstriage";
    var errorMsg = CheckTSTriBlankValues();
    if (isNaN(errorMsg)) {
        customAlert(errorMsg);
        return;
    }
    else {
        var hidEIVID = $("#hidEIVID").val();
        var hidEIVD = $("#hidEIVD").val();

        if ($("#hidVId").val() == 0) {
            var visitType = $("#ddlVisitType").select2("val");
            var visitTypeData = $("#ddlVisitType").select2('data')[0];

            if ($("#hidEIVD").val().length > 0) {
                if (visitTypeData.text.toUpperCase().indexOf("INITIAL") >= 0) {
                    // parse JSON formatted date to javascript date object
                    var dtOSD = new Date(parseInt(hidEIVD.substr(6)));

                    // format display date (e.g. 04/10/2012)
                    var displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
                    bootbox.confirm("Initial visit already exists on day " + displayDate + ", do you want to create another initial visit?", function (e) {
                        if (e) {
                            //debugger;
                            PostTSTriageData();
                            isPreviousTabSave = true;
                        }
                    });
                }
                else {
                    PostTSTriageData();
                    isPreviousTabSave = true;
                }
            }
            else {
                if (visitType != null) {
                    if (visitTypeData.text.toUpperCase().indexOf("INITIAL") > -1) {
                        PostTSTriageData();
                        isPreviousTabSave = true;
                    }
                    else {
                        customAlert(initialTypeMessage);
                    }
                }
            }
        }
        else {
            PostTSTriageData();
            isPreviousTabSave = true;
        }
    }
}

function PostTriageData(IsFind) {

    //activePage = getPageName() + '.aspx';
    var action = "savetriage";
    var rowData1 = PrepareSaveTriageData();
    $.hivce.loader('show');
    var responseObject = null;
    $.ajax({
        type: "POST",
        url: "PrEP.aspx?data=" + action,
        //url: activePage + "?data=" + action,
        data: JSON.stringify(rowData1),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        beforeSend: function () {
        },
        success: function (response) {
            console.log(response);
            var res = response.Success;
            var msg = response.EM;
            if (res == "true") {

                GetTriageData();
                customAlert("Triage tab " + dataSuccessMessage.toLowerCase());
                if (IsFind) {
                    var srvNm = $("#hidsrvNm").val();
                    var mod = $("#hidMOD").val();
                    var url = "../frmFindAddCustom.aspx?srvNm=" + srvNm + "&mod=" + mod;
                    window.location.assign(url);
                }
                else {
                    GetNextPage("tab_2");
                }

            }
            else {

                if (res == "false") {
                    if (msg != null) {
                        if (msg.toLowerCase().indexOf("visit already exists") >= 0) {
                            //string contains hello
                            customAlert(msg);
                        }
                        else {
                            customAlert(errorMessage);
                        }
                    }
                    else {
                        customAlert(errorMessage);
                    }
                }
                else {
                    customAlert(res);
                }
            }

            //$.eva.loader('hide');
        },
        error: function (msg) {
            //ShowErrorMessage(msg);
        }
    });
}


function PrepareSaveTriageData() {
    debugger;
    var localdtformat = true, formName = '';

    //alert(GetDateData("dtVisitDate", localdtformat));

    if (~activePage.indexOf('Green')) { formName = 'Green Card Form'; }
    else if (~activePage.indexOf('Clinical')) { formName = 'Clinical Encounter'; }
    else if (~activePage.indexOf('PrEP')) { formName = 'PrEP'; }


    var ddlSig = $("#ddlSignature").select2("val");
    if (ddlSig == null) { ddlSig = 0; }

    var ddlReferredFrom = $("#ddlReferredFrom").select2("val");

    if (ddlReferredFrom == null) {
        ddlReferredFrom = 0;
    }

    rowData = [];
    rowData.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        HIV: GetHIVData(localdtformat),
        IE: GetIEData(),
        OV: GetOVData(localdtformat),
        PV: GetPVData(),
        TS: ddlSig,
        FN: formName,
        RF: ddlReferredFrom,
        RFO: $("#txtReferredFrom").val()
    });

    console.log(rowData);
    return rowData[0];
}


function GetHIVData(localdtformat) {
    debugger;
    var ddlTransferFrom = $("#ddlFacility").select2("val");

    if (ddlTransferFrom == null) {
        ddlTransferFrom = 0;
    }

    var ddlDis = $("#ddlDistrict").select2("val");

    if (ddlDis == null) {
        ddlDis = 0;
    }

    var txtDistrict = $("#txtDistrict").val();
    if (txtDistrict.length == 0) {
        txtDistrict = 0;
    }
    else {
        var facData = $.grep(arrFacilityBaseData, function (e) { return e.CN == txtDistrict; });
        if (jQuery.isEmptyObject(facData) == false) {
            txtDistrict = facData[0].CId;
        }


    }

    //    var txtMFLCode = $("#txtMFLCode").val();
    //    if (txtMFLCode.length == 0) {
    //        txtMFLCode = 0;
    //    }

    rowData1 = [];
    rowData1.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        //HIVED: '', GetDateData("dtHivEnroll", localdtformat),
        //HIVD: '', GetDateData("dtHivConfirm", localdtformat),
        //HIVCW: $("#txtHIVPositiveWhere").val(),
        //ARTTD: GetDateData("dtTransferDate", localdtformat),
        ARTTD: GetDateData("dtDateStatedArt", localdtformat),
        ARTTF: ddlTransferFrom,
        DIST: txtDistrict, //ddlDis,
        UID: 0,
        ARTSD: GetDateData("dtDateStatedArt", localdtformat),
        TRAN: GetSwitchValue("chkTransferIn"),
        //HIVCPD: '', GetDateData("dtHivConfirm", localdtformat),
        ARTS: GetARTSData(),
        //MFL: ''txtMFLCode
    });
    return rowData1[0];
}

function GetARTSData() {
    debugger;
    rowARTSData = [];

    var ddlARTStartRegimen = $("#ddlARTStartRegimen").select2("val");
    if (ddlARTStartRegimen == null) {
        ddlARTStartRegimen = 0;
    }

    var ddlCurrentRegimen = $("#ddlCurrentRegimen").select2("val");
    if (ddlCurrentRegimen == null) {
        ddlCurrentRegimen = 0;
    }

    var ddlTARTWHOStage = $("#ddlTARTWHOStage").select2("val");
    if (ddlTARTWHOStage == null) {
        ddlTARTWHOStage = 0;
    }

    var txtTWeight = $("#txtWeight").val();
    var txtTHeight = $("#txtHeight").val();
    var txtTBMI = $("#txtTBMI").val();

    var txtBLViralLoad = $("#txtBLViralLoad").val();
    var txtTCD4Count = $("#txtCD4Count").val();

    var txtTWeight = $("#txtWeight").val();
    if (txtTWeight.length == 0) {
        txtTWeight = 0;
    }

    var txtTHeight = $("#txtHeight").val();
    if (txtTHeight.length == 0) {
        txtTHeight = 0;
    }

    var txtTBMI = $("#txtBMI").val();
    if (txtTBMI.length == 0) {
        txtTBMI = 0;
    }

    var txtBLViralLoad = $("#txtBLViralLoad").val();
    //    if (txtBLViralLoad.length == 0) {
    //        txtBLViralLoad = 0;
    //    }

    var txtTCD4Count = $("#txtTCD4Count").val();
    //    if (txtTCD4Count.length == 0) {
    //        txtTCD4Count = 0;
    //    }

    var txtTMUAC = $("#txtMUAC").val();
    if (txtTMUAC.length == 0) {
        txtTMUAC = 0;
    }


    rowARTSData.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        FLR: ddlARTStartRegimen,
        CR: ddlCurrentRegimen,
        WE: txtTWeight,
        HED: txtTHeight,
        WS: ddlTARTWHOStage,
        CD4: txtTCD4Count,
        BLVL: txtBLViralLoad,
        //BLVLD: '', GetDateData("dtBLViralLoadDate", true),
        MUAC: txtTMUAC
    });

    return rowARTSData[0];
}

function GetIEData() {
    debugger;
    rowData2 = [];

//    var ddlDis = $("#ddlCareGiven").select2("val");
//    if (ddlDis == null) {
//        ddlDis = 0;
//    }

    rowData2.push({
        Id: 0,
        Ptn_Pk: 0,
        VID: 0,
        LId: 0,
        HIVSG: 0, //GetSwitchValue("chkSupportGroup"),
        HIVSGM: '', //$("#txtSupportGroupName").val(),
        ACG: 0, //GetSwitchValue("chkCareGiver"),
        CGR: 0, //ddlDis,
        Menarche: ""
    });
    return rowData2[0];
}

function GetOVData(localdtformat) {
    debugger;
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
    debugger;
    var txtHeight = $("#txtHeight").val();
    if (txtHeight.length == 0) {
        txtHeight = 0;
    }

    var txtWeight = $("#txtWeight").val();
    if (txtWeight.length == 0) {
        txtWeight = 0;
    }

    var txtSP = $("#txtSP").val();
    if (txtSP.length == 0) {
        txtSP = 0;
    }


    rowData3 = [];
    rowData3.push({
        VId: 0,
        BPD: $("#txtMM").val(),
        BPS: $("#txtHG").val(),
        TEMP: $("#txtTemperature").val(),
        RR: $("#txtRR").val(),
        HR: $("#txtHRate").val(),
        HC: $("#txtHeadCircumference").val(),
        Height: txtHeight,
        Weight: txtWeight,
        MUAC: $("#txtMUAC").val(),
        WFA: $("#txtWAgeZScore").val(),
        WFH: $("#txtHAgeZScore").val(),
        BMIz: $("#txtBMIZScore").val(),
        NC: $("#txtNursesComment").val(),
        SP: txtSP

    });
    return rowData3[0];

}


