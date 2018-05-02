function InitScreeningAndTestsControls(response) {
    arrLabResults = [];


    $("#chkHIVTestingToday").bootstrapSwitch('state', false);
    $("#ddlPrevHIVStatus").select2();
    $("#ddlPrevPointHIVTesting").select2();

    $("#dtLastHIVTest").datepicker({
        autoclose: true
    });

    $("#ddlPreTestCounselling").select2();
    $("#ddlPosttestcounselling").select2();
    $("#ddlHIVTestDoneToday").select2();
    $("#ddlFinalHIVResult").select2();


    $("#chkPatientaccPartner").bootstrapSwitch('state', false);
    $("#ddlpartnerPreTestCounselling").select2();
    $("#ddlHIVTestdonetopartner").select2();
    $("#ddlPartnerFinalHIVresult").select2();
    $("#ddlPartnerPostTestCounselling").select2();
    $("#chkDiscordantcouple").bootstrapSwitch('state', false);
    $("#ddlPartnerDNA").select2();

    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });

    $('#dtlLabResults').dataTable().fnDestroy();
    ReInitialiseDatatable("LR");
    BindSTControls(response);
}

function BindSTControls(response) {
    debugger;

    var collections = response.Collection;
    var vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "MotherHIVTestResult"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlPrevHIVStatus").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlPrevHIVStatus").select2("val", "0");
    $("#ddlPrevHIVStatus").trigger('change.select2');


    CheckDatenAssign(response.HIVTesting.HIVTRD, "dtLastHIVTest", false);

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "HIVTestingPoint2"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlPrevPointHIVTesting").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlPrevPointHIVTesting").select2("val", "0");
    $("#ddlPrevPointHIVTesting").trigger('change.select2');

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "PretestCounselling"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlPreTestCounselling").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlPreTestCounselling").select2("val", "0");
    $("#ddlPreTestCounselling").trigger('change.select2');

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "PostTestCounselling"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlPosttestcounselling").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlPosttestcounselling").select2("val", "0");
    $("#ddlPosttestcounselling").trigger('change.select2');

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "ReferredToFP"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlHIVTestDoneToday").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlHIVTestDoneToday").select2("val", "0");
    $("#ddlHIVTestDoneToday").trigger('change.select2');

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "FinalHIVResult"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlFinalHIVResult").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlFinalHIVResult").select2("val", "0");
    $("#ddlFinalHIVResult").trigger('change.select2');

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "PartnerPretestCounselling"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlpartnerPreTestCounselling").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlpartnerPreTestCounselling").select2("val", "0");
    $("#ddlpartnerPreTestCounselling").trigger('change.select2');

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "PartnerTestedHIV"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlHIVTestdonetopartner").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlHIVTestdonetopartner").select2("val", "0");
    $("#ddlHIVTestdonetopartner").trigger('change.select2');

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "FinalHIVResultPartner"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlPartnerFinalHIVresult").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlPartnerFinalHIVresult").select2("val", "0");
    $("#ddlPartnerFinalHIVresult").trigger('change.select2');

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "PartnerPostTestCounselling"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlPartnerPostTestCounselling").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlPartnerPostTestCounselling").select2("val", "0");
    $("#ddlPartnerPostTestCounselling").trigger('change.select2');

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "PCRpartner"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlPartnerDNA").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlPartnerDNA").select2("val", "0");
    $("#ddlPartnerDNA").trigger('change.select2');

    if (response.HIVTesting.HIVTT == 1) {
        $("#chkHIVTestingToday").bootstrapSwitch('state', true);
    }
    if (response.HIVTesting.PI == 1) {
        $("#chkPatientaccPartner").bootstrapSwitch('state', true);
    }
    if (response.HIVTesting.DC == 1) {
        $("#chkDiscordantcouple").bootstrapSwitch('state', true);
    }

    $("#ddlPrevHIVStatus").val(response.HIVTesting.HIVTR).trigger("change");
    $("#ddlPrevPointHIVTesting").val(response.HIVTesting.HIVTP2).trigger("change");
    $("#ddlPreTestCounselling").val(response.HIVTesting.PRC).trigger("change");
    $("#ddlPosttestcounselling").val(response.HIVTesting.POTC).trigger("change");
    $("#ddlHIVTestDoneToday").val(response.HIVTesting.HIVTTD).trigger("change");
    $("#ddlFinalHIVResult").val(response.HIVTesting.HIVFR).trigger("change");
    $("#ddlpartnerPreTestCounselling").val(response.HIVTesting.PPRTC).trigger("change");
    $("#ddlHIVTestdonetopartner").val(response.HIVTesting.PHIVT).trigger("change");
    $("#ddlPartnerFinalHIVresult").val(response.HIVTesting.PHIVFR).trigger("change");
    $("#ddlPartnerPostTestCounselling").val(response.HIVTesting.PPOTC).trigger("change");
    $("#ddlPartnerDNA").val(response.HIVTesting.PPCR).trigger("change");


    BindLabResults(response);
}

function BindLabResults(response) {

    $.each(response.LRS, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            var displayDate = "";
            var osd = arrD.TD;
            if (jQuery.isEmptyObject(arrD.TD) == false) {
                // parse JSON formatted date to javascript date object
                var dtOSD = new Date(parseInt(arrD.TD.substr(6)));
                // format display date (e.g. 04/10/2012)
                displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
            }
            arrLabResults.push({
                Order: arrD.Order
                , PId: arrD.PId
                , Ptn_Pk: arrD.Ptn_Pk
                , Result: arrD.Result
                , STN: arrD.STN
                , TD: arrD.TD
                , TR: arrD.TR
                , TRD: arrD.TRD
                , VId: arrD.VId
                , displayTestDate: displayDate
            });

        }
    });

    DrawDataTable("dtlLabResults", arrLabResults);
    $.hivce.loader('hide');
}

function LabPopUpClosed() {
    debugger;
    //alert("FamilyPopUpClosed");
    $.hivce.loader('show');
    var postData;
    activePage = getPageName() + '.aspx';
    var postData;
    $.ajax({
        type: "POST",
        url: activePage + "?data=getscreeningtests",
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
                BindLabResults(response);
            }
        },
        error: function (msg) {
        }
    });


}

function SaveScreeningAndTestsData() {

    activePage = getPageName() + '.aspx';
    var action = "savescreeningtestdata";

    var rowprofileData = PrepareScreeningAndTestsData();
    var errorMsg = '';

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
            data: JSON.stringify(rowprofileData),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            beforeSend: function () {
            },
            success: function (response) {
                //console.log(response);
                var responseSuccess = response.Success;
                if (responseSuccess == "true") {
                    //GetPresentingComplaintsData();
                    customAlert("Screening And Tests tab " + dataSuccessMessage.toLowerCase());
                    GetNextPage("tab_3");
                }
                else {
                    if (responseSuccess == "false") {
                        customAlert(errorMessage);
                    }
                    else {
                        customAlert(responseSuccess);
                        GetNextPage("tab_3");
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

function PrepareScreeningAndTestsData() {

    rowData = [];
    rowData.push({
        Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        HIVTesting: PrepareHIVData()
    });
    console.log(rowData);
    return rowData[0];
}

function PrepareHIVData() {
    rowData1 = [];
    rowData1.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        HIVTT: GetSwitchValue("chkHIVTestingToday"),
        PI: GetSwitchValue("chkPatientaccPartner"),
        DC: GetSwitchValue("chkDiscordantcouple"),
        HIVTR: GetDDLValue("ddlPrevHIVStatus"),

        HIVTRD: GetDateData("dtLastHIVTest", true),

        HIVTP2: GetDDLValue("ddlPrevPointHIVTesting"),
        PRC: GetDDLValue("ddlPreTestCounselling"),
        POTC: GetDDLValue("ddlPosttestcounselling"),
        HIVTTD: GetDDLValue("ddlHIVTestDoneToday"),
        HIVFR: GetDDLValue("ddlFinalHIVResult"),
        PPRTC: GetDDLValue("ddlpartnerPreTestCounselling"),
        PHIVT: GetDDLValue("ddlHIVTestdonetopartner"),
        PHIVFR: GetDDLValue("ddlPartnerFinalHIVresult"),
        PPOTC: GetDDLValue("ddlPartnerPostTestCounselling"),
        PPCR: GetDDLValue("ddlPartnerDNA")
    });

    console.log(rowData1);
    return rowData1[0];
}

function GetDDLValue(ctrl) {
    debugger;
    var ddlCtrl = $("#" + ctrl).select2("val");
    if (ddlCtrl == null) {
        ddlCtrl = 0;
    }
    return ddlCtrl;
}