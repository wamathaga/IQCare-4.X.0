var arrDataIPT = [];
var flagTBContactsScreened = 0;

function InitScreeningControls(response) {
    //debugger;
    window.scrollTo(0, 0);
    $("#ddlTBAssessment").select2();
    $("#ddlTbFindings").select2({ allowClear: false });
    $("#ddlTBRegimen").select2();
    $("#ddlRxDuration").select2();
    $("#ddlSputumSmear").select2();
    $("#ddlGeneExpert").select2();
    $("#ddlSputumDST").select2();
    $("#ddlCXRResults").select2();
    $("#ddlIPTAdherence").select2();
    $("#ddlIPTContraindication").select2();
    $("#ddlIPTDiscontinued").select2();


    $("#chkTBContactsScreened").bootstrapSwitch('state', false);

    $("#chkEligibleForIPT").bootstrapSwitch('state', false);

    $("#chkENT").bootstrapSwitch('state', false);

    $("#chkYellowColoredUrine").bootstrapSwitch('state', false);
    $("#chkNumbnessBurning").bootstrapSwitch('state', false);
    $("#chkYellownessEyes").bootstrapSwitch('state', false);
    $("#chkTenderness").bootstrapSwitch('state', false);


    $("#dtTBStartDate").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");

    $("#dtSputumSmear").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");
    $("#dtGeneExpert").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");
    $("#dtSputumDST").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");
    $("#dtChestXray").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");
    $("#dtTissueBiopsy").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");

    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });

    $('#divIPT input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });
    $("#dtIPTStartDate").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    $("#dtIPTEndDate").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    $('#divAvailableTBResults input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });

    /**********/
    $("#div1IPT").css("visibility", "hidden");
    $("#div1IPT").css("display", "none");

    $("#divAvailableTBResults").css("visibility", "hidden");
    $("#divAvailableTBResults").css("display", "none");

    $("#divTBContactScreened").css("visibility", "hidden");
    $("#divTBContactScreened").css("display", "none");
    //$("#divTBContactScreenedDetails").css("visibility", "hidden");
    //$("#divTBContactScreenedDetails").css("display", "none");


    $("#divIPTStartDate").css("visibility", "hidden");
    $("#divIPTStartDate").css("display", "none");
    $("#divIPTEndDate").css("visibility", "hidden");
    $("#divIPTEndDate").css("display", "none");
    $("#divIPTAdherence").css("visibility", "hidden");
    $("#divIPTAdherence").css("display", "none");
    $("#divIPTContraindication").css("visibility", "hidden");
    $("#divIPTContraindication").css("display", "none");
    $("#divIPTDiscontinued").css("visibility", "hidden");
    $("#divIPTDiscontinued").css("display", "none");

    $("#divCXRResults").css("visibility", "hidden");
    $("#divCXRResults").css("display", "none");
    $('input:radio[name=rdoChestXray]').on('ifChecked', function (event) {
        DisplayCXRResultsDiv(this);
    });
    $('input:radio[name=rdoChestXray]').on('ifUnchecked', function (event) {
        DisplayCXRResultsDiv(this);
    });

    $('#chkEligibleForIPT').on('switchChange.bootstrapSwitch', function (event, state) {
        EnableDisableIPTDIV(GetSwitchValue("chkEligibleForIPT"));
    });

    var visitType = $("#ddlVisitType").select2("val");
    var visitTypeData = $("#ddlVisitType").select2('data')[0];


    if (visitType != null) {
        if (visitTypeData.text.toUpperCase().indexOf("SUPPORTER") == -1) {
            $("#lblTBAssessment").removeClass().addClass("control-label requiredFieldColor");
        }
        else {
            $("#lblTBAssessment").removeClass().addClass("control-label");
        }
    }

    $("#ddlIPTContraindication").select2().on('change', function () {
        EnableDisableOtherSpecifyTextBox("ddlIPTContraindication", "txtIPTContraindication", $(this).val());
    }).trigger('change');

    $("#ddlIPTDiscontinued").select2().on('change', function () {
        EnableDisableOtherSpecifyTextBox("ddlIPTDiscontinued", "txtIPTDiscontinued", $(this).val());
    }).trigger('change');

    //GetScreeningData();
    BindScreeningData(response);
    InitAlcoholDepressionScreeningControls();

}

function EnableDisableIPTDIV(ctrlValue) {
    if (ctrlValue == 0) {
        $("#divIPTOptions").css("visibility", "hidden");
        $("#divIPTOptions").css("display", "none");
        /****/
        ClearIPTOptionsValues();
        $("#divIPTStartDate").css("visibility", "hidden");
        $("#divIPTStartDate").css("display", "none");
        $("#divIPTEndDate").css("visibility", "hidden");
        $("#divIPTEndDate").css("display", "none");
        $("#divIPTAdherence").css("visibility", "hidden");
        $("#divIPTAdherence").css("display", "none");
        $("#divIPTContraindication").css("visibility", "hidden");
        $("#divIPTContraindication").css("display", "none");
        $("#divIPTDiscontinued").css("visibility", "hidden");
        $("#divIPTDiscontinued").css("display", "none");
        $("#divCXRResults").css("visibility", "hidden");
        $("#divCXRResults").css("display", "none");
    }
    else {
        $("#divIPTOptions").css("visibility", "visible");
        $("#divIPTOptions").css("display", "block");
    }
}

function ClearIPTOptionsValues() {
    $('input:radio[name=rbIPT]').prop('checked', false);
    $("#dtIPTStartDate").datepicker('setDate', "0");
    $("#dtIPTEndDate").datepicker('setDate', "0");
    $("#ddlIPTAdherence").select2("val", "");
    $("#ddlIPTContraindication").select2("val", "");
    $("#ddlIPTDiscontinued").select2("val", "");
}


function DisplayCXRResultsDiv(ctrlName) {
    $("#ddlCXRResults").select2("val", "0");
    $("#ddlCXRResults").trigger('change.select2');

    if (ctrlName.value.toLowerCase() == "yes") {
        $("#divCXRResults").css("visibility", "visible");
        $("#divCXRResults").css("display", "block");
    }
    else {
        $("#divCXRResults").css("visibility", "hidden");
        $("#divCXRResults").css("display", "none");
    }
}

function EnableDisableOtherSpecifyTextBox(ctrlName, ctrTextBox, val) {
    var ddlDrugList = $("#" + ctrlName).select2("val");
    if (ddlDrugList != null) {
        var data = $("#" + ctrlName).select2('data')[0];
        if (data.text.toLowerCase().indexOf("other") >= 0) {
            $("#" + ctrTextBox).prop("disabled", false);
        }
        else {
            $("#" + ctrTextBox).prop("disabled", true);
        }
    }
    else {
        $("#" + ctrTextBox).prop("disabled", true);
    }
}


function TBAssessmentValidation() {
    //debugger;

    var noneSelected = false;
    var values = [];
    var noneVal;

    $("#ddlTbFindings").find("option").each(function (i, opt) {
        if (jQuery.isEmptyObject(opt) == false) {
            opt.disabled = false;
            //opt.selected = false;
        }
    });

    $("#ddlTBAssessment").find("option:selected").each(function (i, selected) {
        if ($(selected).text().toLowerCase().indexOf("none") >= 0) {
            noneSelected = true;
            noneVal = $(selected).val();
        }
    });


    if (noneSelected) {
        // loop trough all the selects for each selected option
        $("#ddlTBAssessment").find("option").each(function (i, opt) {
            if (jQuery.isEmptyObject(opt) == false) {

                if (noneVal != $(opt).val()) {
                    opt.selected = false;
                    opt.disabled = true;
                }
            }
        });

        var tbFindingVal;
        $("#ddlTbFindings").find("option").each(function (i, opt) {
            if (jQuery.isEmptyObject(opt) == false) {
                if ($(opt).text().toLowerCase().indexOf("negative tb screen") >= 0) {
                    tbFindingVal = $(opt).val();
                }
            }
        });

        $("#ddlTbFindings").select2().val(tbFindingVal).trigger("change");

    }
    else {
        $("#ddlTBAssessment").find("option").each(function (i, opt) {
            if (jQuery.isEmptyObject(opt) == false) {
                opt.disabled = false;
            }
        });

        var ddlTBAssessment = $("#ddlTBAssessment").select2("val");
        if (ddlTBAssessment !== null) {
            if (ddlTBAssessment.length > 0) {
                var tbFindingVal;
                $("#ddlTbFindings").find("option").each(function (i, opt) {
                    if (jQuery.isEmptyObject(opt) == false) {
                        if ($(opt).text().toLowerCase().indexOf("negative tb screen") >= 0) {
                            opt.disabled = true;
                            opt.selected = false;
                            tbFindingVal = $(opt).val();
                        }
                        else if ($(opt).text().toLowerCase().indexOf("not done") >= 0) {
                            opt.disabled = true;
                            opt.selected = false;
                            tbFindingVal = $(opt).val();
                        }
                    }
                });

                if (tbFindingVal.length > 0) {
                    $("#ddlTbFindings").select2().val(0).trigger("change");
                }
            }
        }
    }

}

function DisplayTBContactScreenedDiv(id, txt) {
    //$("#chkTBContactsScreened").bootstrapSwitch('state', false);
    //$("#ddlTBRegimen").select2();
    //$("#ddlRxDuration").select2();

    //$("#ddlTBRegimen").select2("val", "0");
    //$("#ddlTBRegimen").trigger('change.select2');

    //$("#ddlRxDuration").select2("val", "0");
    //$("#ddlRxDuration").trigger('change.select2');

    //$("#dtTBStartDate").datepicker('setDate', "");

    if ($("#ddlTbFindings").select2('data').length > 0) {
        var theID = $("#ddlTbFindings").select2('data')[0].id;
        var theSelection = $("#ddlTbFindings").select2('data')[0].text;

        if (theSelection.toLowerCase() == "negative tb screen") {

            $("#div1IPT").css("visibility", "visible");
            $("#div1IPT").css("display", "block");

            $("#divAvailableTBResults").css("visibility", "hidden");
            $("#divAvailableTBResults").css("display", "none");

            /*******/
            $("#chkTBContactsScreened").bootstrapSwitch('state', false);
            $("#divTBContactScreened").css("visibility", "hidden");
            $("#divTBContactScreened").css("display", "none");
            //$("#divTBContactScreenedDetails").css("visibility", "hidden");
            //$("#divTBContactScreenedDetails").css("display", "none");
        }
        else if (theSelection.toLowerCase() == "presumptive tb") {
            $("#div1IPT").css("visibility", "hidden");
            $("#div1IPT").css("display", "none");

            $("#divAvailableTBResults").css("visibility", "visible");
            $("#divAvailableTBResults").css("display", "block");

            /*******/
            $("#chkTBContactsScreened").bootstrapSwitch('state', false);
            $("#divTBContactScreened").css("visibility", "hidden");
            $("#divTBContactScreened").css("display", "none");
            //$("#divTBContactScreenedDetails").css("visibility", "hidden");
            //$("#divTBContactScreenedDetails").css("display", "none");
        }
        else if (theSelection.toLowerCase() == "client on tb treatment") {
            $("#div1IPT").css("visibility", "visible");
            $("#div1IPT").css("display", "block");

            if (flagTBContactsScreened == 1) {
                $("#chkTBContactsScreened").bootstrapSwitch('state', true);
            } else {
                $("#chkTBContactsScreened").bootstrapSwitch('state', false);
            }

            $("#divAvailableTBResults").css("visibility", "visible");
            $("#divAvailableTBResults").css("display", "block");

            $("#divTBContactScreened").css("visibility", "visible");
            $("#divTBContactScreened").css("display", "block");

        }
        else if (theSelection.toLowerCase() == "client on inh") {
            $("#div1IPT").css("visibility", "visible");
            $("#div1IPT").css("display", "block");

            $("#divAvailableTBResults").css("visibility", "visible");
            $("#divAvailableTBResults").css("display", "block");

            /*******/
            $("#chkTBContactsScreened").bootstrapSwitch('state', false);
            $("#divTBContactScreened").css("visibility", "hidden");
            $("#divTBContactScreened").css("display", "none");
            //$("#divTBContactScreenedDetails").css("visibility", "hidden");
            //$("#divTBContactScreenedDetails").css("display", "none");
        }
        else {
            $("#div1IPT").css("visibility", "hidden");
            $("#div1IPT").css("display", "none");

            $("#divAvailableTBResults").css("visibility", "hidden");
            $("#divAvailableTBResults").css("display", "none");

            /*******/
            $("#chkTBContactsScreened").bootstrapSwitch('state', false);
            $("#divTBContactScreened").css("visibility", "hidden");
            $("#divTBContactScreened").css("display", "none");
            //$("#divTBContactScreenedDetails").css("visibility", "hidden");
            //$("#divTBContactScreenedDetails").css("display", "none");
        }
    }
}

function DisplayIPTDiv(ctrlName) {

    //$("#ddlIPTAdherence").select2("val", "0");
    //$("#ddlIPTAdherence").trigger('change.select2');

    //$("#ddlIPTContraindication").select2("val", "0");
    //$("#ddlIPTContraindication").trigger('change.select2');

    //$("#ddlIPTDiscontinued").select2("val", "0");
    //$("#ddlIPTDiscontinued").trigger('change.select2');

    //$("#dtIPTStartDate").datepicker('setDate', "");

    //$("#dtIPTEndDate").datepicker('setDate', "");
    //debugger;
    if (ctrlName.parentNode.parentNode.innerText.trim().toLowerCase() == "start ipt") {

        $("#divIPTStartDate").css("visibility", "visible");
        $("#divIPTStartDate").css("display", "block");

        $("#divIPTEndDate").css("visibility", "hidden");
        $("#divIPTEndDate").css("display", "none");
        $("#divIPTAdherence").css("visibility", "hidden");
        $("#divIPTAdherence").css("display", "none");
        $("#divIPTContraindication").css("visibility", "hidden");
        $("#divIPTContraindication").css("display", "none");
        $("#divIPTDiscontinued").css("visibility", "hidden");
        $("#divIPTDiscontinued").css("display", "none");

    }
    else if (ctrlName.parentNode.parentNode.innerText.trim().toLowerCase() == "declined ipt") {

        $("#divIPTContraindication").css("visibility", "visible");
        $("#divIPTContraindication").css("display", "block");

        $("#divIPTStartDate").css("visibility", "hidden");
        $("#divIPTStartDate").css("display", "none");
        $("#divIPTEndDate").css("visibility", "hidden");
        $("#divIPTEndDate").css("display", "none");
        $("#divIPTAdherence").css("visibility", "hidden");
        $("#divIPTAdherence").css("display", "none");
        $("#divIPTDiscontinued").css("visibility", "hidden");
        $("#divIPTDiscontinued").css("display", "none");
    }
    else if (ctrlName.parentNode.parentNode.innerText.trim().toLowerCase() == "continue ipt" || ctrlName.parentNode.parentNode.innerText.trim().toLowerCase() == "defaulted") {

        $("#divIPTAdherence").css("visibility", "visible");
        $("#divIPTAdherence").css("display", "block");

        $("#divIPTStartDate").css("visibility", "hidden");
        $("#divIPTStartDate").css("display", "none");
        $("#divIPTEndDate").css("visibility", "hidden");
        $("#divIPTEndDate").css("display", "none");
        $("#divIPTContraindication").css("visibility", "hidden");
        $("#divIPTContraindication").css("display", "none");
        $("#divIPTDiscontinued").css("visibility", "hidden");
        $("#divIPTDiscontinued").css("display", "none");
    }
    else if (ctrlName.parentNode.parentNode.innerText.trim().toLowerCase() == "completed ipt") {

        $("#divIPTEndDate").css("visibility", "visible");
        $("#divIPTEndDate").css("display", "block");

        $("#divIPTStartDate").css("visibility", "hidden");
        $("#divIPTStartDate").css("display", "none");
        $("#divIPTAdherence").css("visibility", "hidden");
        $("#divIPTAdherence").css("display", "none");
        $("#divIPTContraindication").css("visibility", "hidden");
        $("#divIPTContraindication").css("display", "none");
        $("#divIPTDiscontinued").css("visibility", "hidden");
        $("#divIPTDiscontinued").css("display", "none");
    }

    else if (ctrlName.parentNode.parentNode.innerText.trim().toLowerCase() == "discontinued") {
        //Discontinued
        $("#divIPTDiscontinued").css("visibility", "visible");
        $("#divIPTDiscontinued").css("display", "block");
        $("#divIPTEndDate").css("visibility", "visible");
        $("#divIPTEndDate").css("display", "block");

        $("#divIPTStartDate").css("visibility", "hidden");
        $("#divIPTStartDate").css("display", "none");
        $("#divIPTAdherence").css("visibility", "hidden");
        $("#divIPTAdherence").css("display", "none");
        $("#divIPTContraindication").css("visibility", "hidden");
        $("#divIPTContraindication").css("display", "none");

    }
}

//function GetScreeningData() {
//    //debugger;
//    var postData;
//    $.ajax({
//        type: "POST",
//        url: "ClinicalEncounter.aspx?data=getscreeningdata",
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
//                BindScreeningData(response);
//            }

//        },
//        error: function (msg) {
//        }
//    });
//}

function BindScreeningData(responseDB) {
    var response = responseDB.Collection;
    var data;
    var age = $("#hidPAYM").val();
    if (age <= 15) {
        data = $.grep(response, function (e) { return e.CN == "TBICFPaeds"; });
    }
    else {
        data = $.grep(response, function (e) { return e.CN == "TBAssessmentICF"; });
    }
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlTBAssessment").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });

    data = $.grep(response, function (e) { return e.CN == "TBStatus"; });

    vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlTbFindings").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: false,
        data: vdata
    });

    data = $.grep(response, function (e) { return e.CN == "TBRegimen"; });
    vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlTBRegimen").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlTBRegimen").select2("val", "0");
    $("#ddlTBRegimen").trigger('change.select2');

    data = [];
    $.each([6, 12], function (index, value) {
        data.push({ id: value, text: value });
    });

    $("#ddlRxDuration").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlRxDuration").select2("val", "0");
    $("#ddlRxDuration").trigger('change.select2');

    $("#divIPT").empty();
    data = $.grep(response, function (e) { return e.CN == "IPT"; });
    vdata = [];
    var strIPT = '';
    var disabled = false;
    if (jQuery.isEmptyObject(responseDB.IPTSD) == true) {
        disabled = true;
    }
    $.each(data, function (index, value) {
        if (value.DN.toLowerCase() !== "defaulted") {
            strIPT += "<label style=\"margin-right: 20px;\">";
            strIPT += "     <input type=\"radio\" id=\"rb" + value.DId + "\" name=\"rbIPT\" class=\"flat-red\"";
            if (value.DN !== "Start IPT" && value.DN !== "Declined IPT") {
                if (disabled) {
                    strIPT += " disabled= \"disabled\"";
                }
            }
            strIPT += "     value=\"" + value.DId + "\" />&nbsp;";
            strIPT += value.DN;
            strIPT += "</label>";
        }
    });
    $("#divIPT").append(strIPT);
    $('#divIPT input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });
    $('input:radio[name=rbIPT]').on('ifChecked', function (event) {
        $(this).closest("input").attr('checked', true);
        DisplayIPTDiv(this);
    });
    $('input:radio[name=rbIPT]').on('ifUnchecked', function (event) {
        $(this).closest("input").attr('checked', false);
        DisplayIPTDiv(this);
    });

    data = $.grep(response, function (e) { return e.CN == "SputumSmear"; });
    vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlSputumSmear").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlSputumSmear").select2("val", "0");
    $("#ddlSputumSmear").trigger('change.select2');

    data = $.grep(response, function (e) { return e.CN == "GeneExpert"; });
    vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlGeneExpert").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlGeneExpert").select2("val", "0");
    $("#ddlGeneExpert").trigger('change.select2');

    data = $.grep(response, function (e) { return e.CN == "SputumDST"; });
    vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlSputumDST").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlSputumDST").select2("val", "0");
    $("#ddlSputumDST").trigger('change.select2');

    data = $.grep(response, function (e) { return e.CN == "CXR"; });
    vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlCXRResults").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlCXRResults").select2("val", "0");
    $("#ddlCXRResults").trigger('change.select2');

    data = $.grep(response, function (e) { return e.CN == "IPTAdherence"; });
    vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlIPTAdherence").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlIPTAdherence").select2("val", "0");
    $("#ddlIPTAdherence").trigger('change.select2');

    data = $.grep(response, function (e) { return e.CN == "IPTContraindication"; });
    vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlIPTContraindication").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlIPTContraindication").select2("val", "0");
    $("#ddlIPTContraindication").trigger('change.select2');

    data = $.grep(response, function (e) { return e.CN == "IPTDiscontinued"; });
    vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });

    $("#ddlIPTDiscontinued").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlIPTDiscontinued").select2("val", "0");
    $("#ddlIPTDiscontinued").trigger('change.select2');


    $("#ddlTBAssessment").select2().on('change', function (e) {
        //debugger;
        var $this = $(this);
        if (jQuery.isEmptyObject($this) == false) {
            console.log($this.val());
            TBAssessmentValidation();
        }
    });

    $("#ddlTbFindings").select2().on('change', function (e) {
        var $this = $(this);
        if (jQuery.isEmptyObject($this) == false) {
            if ($this.val() != null) {
                console.log($this.val());
                DisplayTBContactScreenedDiv();
            }
        }

    });


    /* DB Object Binding*/
    var vHRB = $.grep(responseDB.TBSDisplay, function (e) { return e.FN == "TBAssessmentICF"; });
    var selectedValues = new Array();
    $.each(vHRB, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            if (arrD.ValId !== 0) {
                selectedValues.push([arrD.ValId]);
            }
        }
    });

    if (selectedValues.length > 0) {
        $("#ddlTBAssessment").select2().val(selectedValues).trigger("change");
    }
    else {
        $("#ddlTBAssessment").select2().val("0").trigger("change");
    }

    if (responseDB.TBF > 0) {
        $("#ddlTbFindings").select2().val(responseDB.TBF).trigger("change");
    }
    else {
        $("#ddlTbFindings").select2().val("0").trigger("change");
    }

    debugger;

    $("#txtTBTreatment").val(responseDB.TBT);
    //$("#chkTBContactsScreened").bootstrapSwitch('state', false);
    //$("#divTBContactScreened").css("visibility", "hidden");
    //$("#divTBContactScreened").css("display", "none");
    //$("#divTBContactScreenedDetails").css("visibility", "hidden");
    //$("#divTBContactScreenedDetails").css("display", "none");

    $("#ddlTBRegimen").select2().val(responseDB.TBR).trigger("change");
    $("#ddlRxDuration").select2().val(responseDB.RxD).trigger("change");
    $("#ddlSputumSmear").select2().val(responseDB.SS).trigger("change");
    $("#ddlGeneExpert").select2().val(responseDB.GE).trigger("change");
    $("#ddlSputumDST").select2().val(responseDB.SDST).trigger("change");

    $("#ddlCXRResults").select2().val(responseDB.CXRR).trigger("change");
    $("#ddlIPTAdherence").select2().val(responseDB.IPTA).trigger("change");
    $("#ddlIPTContraindication").select2().val(responseDB.IPTC).trigger("change");
    $("#ddlIPTDiscontinued").select2().val(responseDB.IPTD).trigger("change");

    flagTBContactsScreened = responseDB.IsTBCS;
    if (jQuery.isEmptyObject(responseDB.IsTBCS) == false) {
        var theID = $("#ddlTbFindings").select2('data')[0].id;
        var theSelection = $("#ddlTbFindings").select2('data')[0].text;

        if (responseDB.IsTBCS == "1") {
            if (theSelection.toLowerCase().indexOf("tb") != -1) {
                $("#chkTBContactsScreened").bootstrapSwitch('state', true);
            } else {
                $("#chkTBContactsScreened").bootstrapSwitch('state', false);
            }
        }
    }

    CheckDatenAssign(responseDB.TBSD, "dtTBStartDate", false);
    CheckDatenAssign(responseDB.SSD, "dtSputumSmear", false);
    CheckDatenAssign(responseDB.GED, "dtGeneExpert", false);
    CheckDatenAssign(responseDB.SDSTD, "dtSputumDST", false);
    CheckDatenAssign(responseDB.CXD, "dtChestXray", false);
    CheckDatenAssign(responseDB.TBD, "dtTissueBiopsy", false);
    CheckDatenAssign(responseDB.IPTSD, "dtIPTStartDate", false);
    CheckDatenAssign(responseDB.IPTED, "dtIPTEndDate", false);

    if (jQuery.isEmptyObject(responseDB.EFIPT) == false) {
        if (responseDB.EFIPT == 0) {
            $("#chkEligibleForIPT").bootstrapSwitch('state', false);
        }
        else {
            $("#chkEligibleForIPT").bootstrapSwitch('state', true);
        }
    }
    else {
        $("#chkEligibleForIPT").bootstrapSwitch('state', false);
    }

    if (responseDB.IPTWPYCU == 0) {
        $("#chkYellowColoredUrine").bootstrapSwitch('state', false);
    }
    else {
        $("#chkYellowColoredUrine").bootstrapSwitch('state', true);
    }

    if (responseDB.IPTWPNB == 0) {
        $("#chkNumbnessBurning").bootstrapSwitch('state', false);
    }
    else {
        $("#chkNumbnessBurning").bootstrapSwitch('state', true);
    }

    if (responseDB.IPTWPYE == 0) {
        $("#chkYellownessEyes").bootstrapSwitch('state', false);
    }
    else {
        $("#chkYellownessEyes").bootstrapSwitch('state', true);
    }

    if (responseDB.IPTWPT == 0) {
        $("#chkTenderness").bootstrapSwitch('state', false);
    }
    else {
        $("#chkTenderness").bootstrapSwitch('state', true);
    }

    $("#txtIPTContraindication").val(responseDB.ORDec);
    $("#txtIPTDiscontinued").val(responseDB.ORDis);

    EnableDisableIPTDIV(GetSwitchValue("chkEligibleForIPT"));

    //    $("input:radio[name=rbIPT][value=" + responseDB.IPT + "]").attr('checked', 'checked');
    //    $("input:radio[name=rbIPT]").iCheck('update');

    //debugger;
    var dataIPT = $.grep(response, function (e) { return e.CN == "IPT"; });
    arrDataIPT = $.grep(response, function (e) { return e.CN == "IPT"; });
    var startIPTID = 0, continueIPT = 0;
    $.each(dataIPT, function (index, value) {
        if (value.DN == "Start IPT") {
            startIPTID = value.DId;
        }
        else if (value.DN == "Continue IPT") {
            continueIPT = value.DId;
        }
    });

    if (responseDB.VId < $("#hidVId").val() && responseDB.IPT == startIPTID) {

        $("input:radio[name=rbIPT][value=" + continueIPT + "]").attr('checked', 'checked');
        $("input:radio[name=rbIPT]").iCheck('update');
        ReInitialiseIPTDiv(1159, dataIPT);

    }
    else {
        $("input:radio[name=rbIPT][value=" + responseDB.IPT + "]").attr('checked', 'checked');
        $("input:radio[name=rbIPT]").iCheck('update');
        ReInitialiseIPTDiv(responseDB.IPT, dataIPT);
    }


    //debugger;

    $("input:radio[name=rdoChestXray][value=" + responseDB.rbCX + "]").attr('checked', 'checked');
    $("input:radio[name=rdoChestXray]").iCheck('update');

    $("input:radio[name=rdoTissueBiopsy][value=" + responseDB.rbTB + "]").attr('checked', 'checked');
    $("input:radio[name=rdoTissueBiopsy]").iCheck('update');

    $.hivce.loader('hide');
}

function ReInitialiseIPTDiv(id, dataIPT) {

    var vdataENVA = $.grep(dataIPT, function (e) { return e.DId == id; });
    if (jQuery.isEmptyObject(vdataENVA) == false) {
        var name = vdataENVA[0].DN;
        if (name.toLowerCase() == "start ipt") {

            $("#divIPTStartDate").css("visibility", "visible");
            $("#divIPTStartDate").css("display", "block");

            $("#divIPTEndDate").css("visibility", "hidden");
            $("#divIPTEndDate").css("display", "none");
            $("#divIPTAdherence").css("visibility", "hidden");
            $("#divIPTAdherence").css("display", "none");
            $("#divIPTContraindication").css("visibility", "hidden");
            $("#divIPTContraindication").css("display", "none");
            $("#divIPTDiscontinued").css("visibility", "hidden");
            $("#divIPTDiscontinued").css("display", "none");


        }
        else if (name.toLowerCase() == "continue ipt" || name.toLowerCase() == "defaulted") {

            $("#divIPTAdherence").css("visibility", "visible");
            $("#divIPTAdherence").css("display", "block");


            $("#divIPTStartDate").css("visibility", "hidden");
            $("#divIPTStartDate").css("display", "none");
            $("#divIPTEndDate").css("visibility", "hidden");
            $("#divIPTEndDate").css("display", "none");
            $("#divIPTContraindication").css("visibility", "hidden");
            $("#divIPTContraindication").css("display", "none");
            $("#divIPTDiscontinued").css("visibility", "hidden");
            $("#divIPTDiscontinued").css("display", "none");
        }
        else if (name.toLowerCase() == "completed ipt") {

            $("#divIPTEndDate").css("visibility", "visible");
            $("#divIPTEndDate").css("display", "block");

            $("#divIPTAdherence").css("visibility", "hidden");
            $("#divIPTAdherence").css("display", "none");
            $("#divIPTStartDate").css("visibility", "hidden");
            $("#divIPTStartDate").css("display", "none");
            $("#divIPTContraindication").css("visibility", "hidden");
            $("#divIPTContraindication").css("display", "none");
            $("#divIPTDiscontinued").css("visibility", "hidden");
            $("#divIPTDiscontinued").css("display", "none");
        }
        else if (name.toLowerCase() == "declined ipt") {
            $("#divIPTContraindication").css("visibility", "visible");
            $("#divIPTContraindication").css("display", "block");

            $("#divIPTStartDate").css("visibility", "hidden");
            $("#divIPTStartDate").css("display", "none");
            $("#divIPTEndDate").css("visibility", "hidden");
            $("#divIPTEndDate").css("display", "none");
            $("#divIPTAdherence").css("visibility", "hidden");
            $("#divIPTAdherence").css("display", "none");
            $("#divIPTDiscontinued").css("visibility", "hidden");
            $("#divIPTDiscontinued").css("display", "none");
        }
        else if (name.toLowerCase() == "discontinued") {
            //Discontinued
            $("#divIPTDiscontinued").css("visibility", "visible");
            $("#divIPTDiscontinued").css("display", "block");
            $("#divIPTEndDate").css("visibility", "visible");
            $("#divIPTEndDate").css("display", "block");

            $("#divIPTStartDate").css("visibility", "hidden");
            $("#divIPTStartDate").css("display", "none");
            $("#divIPTAdherence").css("visibility", "hidden");
            $("#divIPTAdherence").css("display", "none");
            $("#divIPTContraindication").css("visibility", "hidden");
            $("#divIPTContraindication").css("display", "none");

        }
    }
}


function SaveScreeningData() {
    $.hivce.loader('show');
    var screeningData = PrepareScreeningData();

    PostScreeningData(screeningData);

}

function PrepareScreeningData() {
    var localdtformat = true;
    //alert(GetDateData("dtVisitDate", localdtformat));
    rowScreeningData = [];

    /*var ddlTBAssessment = $("#ddlTBAssessment").select2("val");
    if (ddlTBAssessment == null) {
    ddlTBAssessment = 0;
    }*/

    var ddlTbFindings = $("#ddlTbFindings").select2("val");
    if (ddlTbFindings == null) {
        ddlTbFindings = 0;
    }

    var ddlTBRegimen = $("#ddlTBRegimen").select2("val");
    if (ddlTBRegimen == null) {
        ddlTBRegimen = 0;
    }

    var ddlRxDuration = $("#ddlRxDuration").select2("val");
    if (ddlRxDuration == null) {
        ddlRxDuration = 0;
    }

    var ddlSputumSmear = $("#ddlSputumSmear").select2("val");
    if (ddlSputumSmear == null) {
        ddlSputumSmear = 0;
    }

    var ddlGeneExpert = $("#ddlGeneExpert").select2("val");
    if (ddlGeneExpert == null) {
        ddlGeneExpert = 0;
    }

    var ddlSputumDST = $("#ddlSputumDST").select2("val");
    if (ddlSputumDST == null) {
        ddlSputumDST = 0;
    }

    var ddlCXRResults = $("#ddlCXRResults").select2("val");
    if (ddlCXRResults == null) {
        ddlCXRResults = 0;
    }

    var ddlIPTAdherence = $("#ddlIPTAdherence").select2("val");
    if (ddlIPTAdherence == null) {
        ddlIPTAdherence = 0;
    }

    var ddlIPTContraindication = $("#ddlIPTContraindication").select2("val");
    if (ddlIPTContraindication == null) {
        ddlIPTContraindication = 0;
    }

    var ddlIPTDiscontinued = $("#ddlIPTDiscontinued").select2("val");
    if (ddlIPTDiscontinued == null) {
        ddlIPTDiscontinued = 0;
    }

    var chkTBContactsScreened = GetSwitchValue("chkTBContactsScreened");

    var dtTBStartDate = GetDateData("dtTBStartDate", true);
    var txtTBTreatment = $("#txtTBTreatment").val();
    var dtSputumSmear = GetDateData("dtSputumSmear", true);
    var dtGeneExpert = GetDateData("dtGeneExpert", true);
    var dtSputumDST = GetDateData("dtSputumDST", true);
    var dtChestXray = GetDateData("dtChestXray", true);
    var dtTissueBiopsy = GetDateData("dtTissueBiopsy", true);
    var dtIPTStartDate = GetDateData("dtIPTStartDate", true);
    var dtIPTEndDate = GetDateData("dtIPTEndDate", true);

    var rdoChestXray = $("input:radio[name=rdoChestXray]:checked").val();
    var rdoTissueBiopsy = $("input:radio[name=rdoTissueBiopsy]:checked").val();

    var uChestXray = (rdoChestXray === undefined) ? 0 : rdoChestXray;
    var uTissueBiopsy = (rdoTissueBiopsy === undefined) ? 0 : rdoTissueBiopsy;

    var rbIPT = $("input:radio[name=rbIPT]:checked").val();
    var ipt = (rbDCA === undefined) ? 0 : rbIPT;

    var chkEligibleForIPT = GetSwitchValue("chkEligibleForIPT");

    var chkYellowColoredUrine = GetSwitchValue("chkYellowColoredUrine");
    var chkNumbnessBurning = GetSwitchValue("chkNumbnessBurning");
    var chkYellownessEyes = GetSwitchValue("chkYellownessEyes");
    var chkTenderness = GetSwitchValue("chkTenderness");


    var rowAlcoholDepressionScreening = PrepareAlcoholDepressionScreeningData();

    if (txtTBTreatment.length == 0) {
        txtTBTreatment = 0;
    }

    rowScreeningData.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        TBS: GetTBAssessment(),
        TBF: ddlTbFindings,
        TBR: ddlTBRegimen,
        RxD: ddlRxDuration,
        TBT: txtTBTreatment,
        SS: ddlSputumSmear,
        GE: ddlGeneExpert,
        SDST: ddlSputumDST,
        CXRR: ddlCXRResults,
        IPT: ipt,
        IPTA: ddlIPTAdherence,
        IPTC: ddlIPTContraindication,
        IPTD: ddlIPTDiscontinued,
        IsTBCS: chkTBContactsScreened,
        TBSD: dtTBStartDate,
        SSD: dtSputumSmear,
        GED: dtGeneExpert,
        SDSTD: dtSputumDST,
        CXD: dtChestXray,
        TBD: dtTissueBiopsy,
        IPTSD: dtIPTStartDate,
        IPTED: dtIPTEndDate,
        rbCX: uChestXray,
        rbTB: uTissueBiopsy,
        ADS: rowAlcoholDepressionScreening,
        EFIPT: chkEligibleForIPT,
        IPTWPYCU: chkYellowColoredUrine,
        IPTWPNB: chkNumbnessBurning,
        IPTWPYE: chkYellownessEyes,
        IPTWPT: chkTenderness,
        ORDec: $("#txtIPTContraindication").val(),
        ORDis: $("#txtIPTDiscontinued").val()
    });

    return rowScreeningData;
}


function GetTBAssessment() {
    var ddlTBAssessment = $("#ddlTBAssessment").select2("val");
    var arrTBA = [];
    if (ddlTBAssessment !== null) {
        if (ddlTBAssessment.length > 0) {
            if (jQuery.isEmptyObject(ddlTBAssessment) == false) {
                $.each(ddlTBAssessment, function (index, arrD) {
                    if (jQuery.isEmptyObject(arrD) == false) {
                        arrTBA.push({ id: arrD });
                    }
                });
            }
        }
        else {
            arrTBA.push({ id: 0 });
        }
    }
    else {
        arrTBA.push({ id: 0 });
    }

    return arrTBA;
}

function CheckScreeningBlankValues() {

    errorMsg = '';
    var errorField = '';
    var data;
    //debugger;
    var rbIPT = $("input:radio[name=rbIPT]:checked").val();
    var uIPT = (rbIPT === undefined) ? 0 : rbIPT;
    if (uIPT > 0) {
        var vdataENVA = $.grep(arrDataIPT, function (e) { return e.DId == uIPT; });
        var name = vdataENVA[0].DN;
        if (name.toLowerCase() == "start ipt") {
            if ($("#dtIPTStartDate").val().length == 0) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'IPT Start Date ';
            }
        }
        else if (name.toLowerCase() == "completed ipt") {

            if ($("#dtIPTEndDate").val().length == 0) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'IPT End Date ';
            }

            /* var ddlIPTAdherence = $("#ddlIPTAdherence").select2("val");

            if (ddlIPTAdherence == null) {
            if (errorField.length > 1) {
            errorField += ', ';
            }
            errorField += 'IPT Adherence ';
            }*/

        }
        else if (name.toLowerCase() == "declined ipt") {

            var ddlIPTContraindication = $("#ddlIPTContraindication").select2("val");

            if (ddlIPTContraindication == null) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'IPT Contraindication ';
            }
            else {
                data = $("#ddlIPTContraindication").select2('data')[0];
                if (data.text.toLowerCase().indexOf("other") >= 0) {
                    if ($("#txtIPTContraindication").val().length == 0) {
                        if (errorField.length > 1) {
                            errorField += ', ';
                        }
                        errorField += 'IPT Contraindication Other Reason ';
                    }
                }
            }
        }
        else if (name.toLowerCase() == "discontinued") {
            //Discontinued

            var ddlIPTDiscontinued = $("#ddlIPTDiscontinued").select2("val");

            if (ddlIPTDiscontinued == null) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'IPT Discontinued ';
            }
            else {
                data = $("#ddlIPTDiscontinued").select2('data')[0];
                if (data.text.toLowerCase().indexOf("other") >= 0) {
                    if ($("#txtIPTDiscontinued").val().length == 0) {
                        if (errorField.length > 1) {
                            errorField += ', ';
                        }
                        errorField += 'IPT Discontinued Other Reason ';
                    }
                }
            }
        }
    }

    var ddlTbFindings = $("#ddlTbFindings").select2("val");

    if (ddlTbFindings == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Tb Findings ';
    }

    var age = $("#hidDOB").val();
    if (age < 14) {
        var ddlDisclosureStatus = $("#ddlDisclosureStatus").select2("val");

        if (ddlDisclosureStatus == null) {
            if (errorField.length > 1) {
                errorField += ', ';
            }
            errorField += 'Disclosure Status ';
        }
    }

    var visitType = $("#ddlVisitType").select2("val");
    var visitTypeData = $("#ddlVisitType").select2('data')[0];


    if (visitType != null) {
        if (visitTypeData.text.toUpperCase().indexOf("SUPPORTER") == -1) {
            var ddlTBAssessment = $("#ddlTBAssessment").select2("val");

            if (ddlTBAssessment == null) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'TB Assessment ';
            }
        }
    }

    if (age >= 20) {

        var rbDCA = $("input:radio[name=rbDCA]:checked").val();
        var uDCA = (rbDCA === undefined) ? 0 : rbDCA;

        var rbUDrugs = $("input:radio[name=rbUDrugs]:checked").val();
        var uUDrugs = (rbUDrugs === undefined) ? 0 : rbUDrugs;

        var rbSmoke = $("input:radio[name=rbSmoke]:checked").val();
        var uSmoke = (rbSmoke === undefined) ? 0 : rbSmoke;

        if (visitType != null) {
            if (visitTypeData.text.toUpperCase().indexOf("INITIAL") >= 0) {

                if (uDCA == 0) {
                    if (errorField.length > 1) {
                        errorField += ', ';
                    }
                    errorField += 'Question - How often do you have a drink containing alcohol? ';
                }

                if (uUDrugs == 0) {
                    if (errorField.length > 1) {
                        errorField += ', ';
                    }
                    errorField += 'Question - How often do you use drugs? ';
                }

                if (uSmoke == 0) {
                    if (errorField.length > 1) {
                        errorField += ', ';
                    }
                    errorField += 'Question - How often do you smoke? ';
                }

            }
        }
    }

    if (isNaN(errorField)) {
        errorMsg = errorField + 'cannot be blank.';
    }

    return errorMsg;

}

function PostScreeningData(rowData) {

    var action = "savescreening";
    var rowData1 = rowData[0];
    var errorMsg = CheckScreeningBlankValues();

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
                //debugger;
                //console.log(response);
                var responseSuccess = response.Success;
                if (responseSuccess == "true") {
                    GetScreeningData();
                    customAlert("Screening tab " + dataSuccessMessage.toLowerCase());
                    GetNextPage("tab_5");
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