var arrPresentingComplaints = [];
var activePage;
var arrNxtAppointment = [];
var zscoreDetails;

$(document).ready(function () {
    $.hivce.loader('show');

    GetRefillEnounterFormData();

});

function GetRefillEnounterFormData() {
    activePage = getPageName() + '.aspx';
    var postData;
    $.ajax({
        type: "POST",
        url: activePage + "?data=getdata",
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
                InitControls(response);
            }
        },
        error: function (msg) {
        }
    });
}


function InitControls(response) {
    window.scrollTo(0, 0);
    arrPresentingComplaints = [];
    $("#dtVisitDate").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");
    $("#dtLMP").datepicker({
        autoclose: true
    }); //.datepicker("setDate", "0")

    $("#chkComplaints").bootstrapSwitch('state', false);
    $("#chkOnFamilyPlanning").bootstrapSwitch('state', false);
    $("#ddlPregnant").select2();
    $("#ddlFPM").select2();
    $("#ddlNoFPReason").select2();
    $("#chkBreastFeeding").bootstrapSwitch('state', false);
    $("#chkForgotMed").bootstrapSwitch('state', false);

    $('#dtlPresentingComplaints').dataTable().fnDestroy();
    ReInitialisePCDatatable("PC");

    $("#dtNextAppointmentDate").datepicker({
        autoclose: true
    });
    $("#ddlPurpose").select2();
    $("#ddlPWP").select2();
    $("#ddlDRAPurpose").select2();
    $("#ddlARTRefillModel").select2();
    $("#dtDrugRefillAppointmentDate").datepicker({
        autoclose: true
    });
    $('#dtlNextAppointment').dataTable().fnDestroy();


    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });

    $('#chkOnFamilyPlanning').on('switchChange.bootstrapSwitch', function (event, state) {
        EnableDisableContraceptiveHistory(GetSwitchValue("chkOnFamilyPlanning"));
    });

    $('#chkForgotMed').on('switchChange.bootstrapSwitch', function (event, state) {
        ShowHideMissedDoseDiv(GetSwitchValue("chkForgotMed"));
    });


    $("#ddlPresentingComplaints").select2();
    //Date picker
    $("#dtOnsetDate").datepicker({
        autoclose: true
    });

    ReInitialiseAppointmentsDatatable();
    //BindMultiSelectDropDown("ddlPWP", response.PWPL);

    BindControls(response);
}

function ShowHideMissedDoseDiv(val) {
    if (val == 0) {
        $("#divForgotMed1").css("visibility", "hidden");
        $("#divForgotMed1").css("display", "none");
        $("#divForgotMed2").css("visibility", "hidden");
        $("#divForgotMed2").css("display", "none");
        $("#divForgotMed3").css("visibility", "hidden");
        $("#divForgotMed3").css("display", "none");
        $("#divForgotMed4").css("visibility", "hidden");
        $("#divForgotMed4").css("display", "none");


    } else {
        $("#divForgotMed1").css("visibility", "visible");
        $("#divForgotMed1").css("display", "block");
        $("#divForgotMed2").css("visibility", "visible");
        $("#divForgotMed2").css("display", "block");
        $("#divForgotMed3").css("visibility", "visible");
        $("#divForgotMed3").css("display", "block");
        $("#divForgotMed4").css("visibility", "visible");
        $("#divForgotMed4").css("display", "block");
    }
}

function BindControls(response) {
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
    debugger;
    $.each(response.PC, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlPresentingComplaints").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlPresentingComplaints").select2("val", "0");
    $("#ddlPresentingComplaints").trigger('change.select2');

    data = [];
    $.each(response.PS, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlPregnant").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlPregnant").select2("val", "0");
    $("#ddlPregnant").trigger('change.select2');

    data = [];
    $.each(response.FPMS, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlFPM").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlFPM").select2("val", "0");
    $("#ddlFPM").trigger('change.select2');

    data = [];
    $.each(response.RNFP, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlNoFPReason").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlNoFPReason").select2("val", "0");
    $("#ddlNoFPReason").trigger('change.select2');

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


    data = $.grep(response.PUR, function (e) { return e.CN == "PUR"; });
    vdata = [], vdata1 = [];
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

    $("#ddlRegimenCode").prop("disabled", true);
    $("#ddlRegimenCode").select2().val(response.RCV).trigger("change");

    debugger;
    CheckDatenAssign(response.OV.VD, "dtVisitDate", false);
    $("#hidVId").val(response.OV.VId);
    //debugger;
    $("#ddlVisitType").val(response.OV.TOV).trigger("change");

    $("#txtMM").val(response.PV.BPD);
    $("#txtHG").val(response.PV.BPS);
    $("#txtTemperature").val(response.PV.TEMP);
    $("#txtRR").val(response.PV.RR);
    $("#txtHRate").val(response.PV.HR);
    $("#txtHeadCircumference").val(response.PV.HC);
    $("#txtHeight").val(response.PV.Height);
    var hidVId = $("#hidVId").val();
    if (hidVId > 0) {
        $("#txtWeight").val(response.PV.Weight);
    }
    $("#txtMUAC").val(response.PV.MUAC);
    $("#txtSP").val(response.PV.SP);
    //CalcualteBMIWithoutZScore('hidPAYM', 'txtWeight', 'txtHeight', 'txtBMI', 'lblBMIClassification');

    $("#txtComplaintsNote").val(response.OPC);
    $.each(response.PCT, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            var data = $.grep(response.PC, function (e) { return e.DId == arrD.ValId; });
            if (jQuery.isEmptyObject(data) == false) {
                // parse JSON formatted date to javascript date object
                var dtOSD = new Date(parseInt(arrD.OSD.substr(6)));

                // format display date (e.g. 04/10/2012)
                var displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");

                arrPresentingComplaints.push({ PCID: arrD.ValId, PC: data[0].DN, OSD: arrD.OSD, displayDate: displayDate });
            }
        }
    });
    BindDataTable(arrPresentingComplaints, "divPresentingComplaints", "chkComplaints", "dtlPresentingComplaints");

    if (response.FPS == 1) {
        $("#chkOnFamilyPlanning").bootstrapSwitch('state', true);
        $("#ddlFPM").prop('disabled', false);
        $("#ddlNoFPReason").prop('disabled', true);
    }
    else {
        $("#ddlFPM").prop('disabled', true);
        $("#ddlNoFPReason").prop('disabled', false);
    }

    $("#ddlPregnant").select2().val(response.Pregnant).trigger("change");
    $("#ddlFPM").select2().val(response.FPM).trigger("change");
    $("#ddlNoFPReason").select2().val(response.NFP).trigger("change");

    var valFPM = $("#ddlFPM").select2("val");
    var dataFPM = $("#ddlFPM").select2('data')[0];


    if (valFPM != null) {
        if (dataFPM.text.toUpperCase().indexOf("TUBAL LIGATION") >= 0) {
            $("#chkOnFamilyPlanning").bootstrapSwitch('disabled', true);
            $("#ddlFPM").prop('disabled', true);
        }
        else if (dataFPM.text.toUpperCase().indexOf("VASECTOMY") >= 0) {
            $("#chkOnFamilyPlanning").bootstrapSwitch('disabled', true);
            $("#ddlFPM").prop('disabled', true);
        }
    }
    if (response.BS == 1) {
        $("#chkBreastFeeding").bootstrapSwitch('state', true);
    }
    CheckDatenAssign(response.LMP, "dtLMP", false);

    var age = $("#hidDOB").val();
    var gender = $("#hidGender").val();

    if (gender.toLowerCase() == "female") {
        if (age < 14) {
            $("#divOBSGYN").css("visibility", "hidden");
            $("#divOBSGYN").css("display", "none");
            $("#divContraceptiveHistory").css("visibility", "hidden");
            $("#divContraceptiveHistory").css("display", "none");
        }
    }
    else if (gender.toLowerCase() == "male") {
        $("#divOBSGYN").css("visibility", "hidden");
        $("#divOBSGYN").css("display", "none");
        $("#divContraceptiveHistory").css("visibility", "hidden");
        $("#divContraceptiveHistory").css("display", "none");
    }


    if (response.IsForgotMed == 1) {
        $("#divForgotMed1").css("visibility", "visible");
        $("#divForgotMed1").css("display", "block");
        $("#divForgotMed2").css("visibility", "visible");
        $("#divForgotMed2").css("display", "block");
        $("#divForgotMed3").css("visibility", "visible");
        $("#divForgotMed3").css("display", "block");
        $("#divForgotMed4").css("visibility", "visible");
        $("#divForgotMed4").css("display", "block");
    } else {
        $("#divForgotMed1").css("visibility", "hidden");
        $("#divForgotMed1").css("display", "none");
        $("#divForgotMed2").css("visibility", "hidden");
        $("#divForgotMed2").css("display", "none");
        $("#divForgotMed3").css("visibility", "hidden");
        $("#divForgotMed3").css("display", "none");
        $("#divForgotMed4").css("visibility", "hidden");
        $("#divForgotMed4").css("display", "none");
    }

    if (response.ISFM == 1) {
        $("#chkForgotMed").bootstrapSwitch('state', true);
    }

    var chkComplaints = GetSwitchValue("chkComplaints");
    var chkBreastFeeding = GetSwitchValue("chkBreastFeeding");
    var chkForgotMed = GetSwitchValue("chkForgotMed");

    var valPregnant = $("#ddlPregnant").select2("val");
    var dataPregnant = $("#ddlPregnant").select2('data')[0];

    var chkPregnant = 0;

    if (valPregnant != null) {
        if (dataPregnant.text.toUpperCase().indexOf("YES") >= 0) {
            chkPregnant = 1;
        }
    }

    if (
        chkComplaints == 1 && chkBreastFeeding == 1 && chkForgotMed == 1 && chkPregnant == 1
    ) {
        $("#txtActionBox").val("Refer to clinician for further evaluation");
    }



    $("#txtMissedDoses").val(response.NMD);
    $("#txtDrugs").val(response.Drugs);
    $("#txtAreaReminders").val(response.AREM);

    $("#txtServiceArea").val($("#hidsrvNm").val());
    $("#txtDRASA").val($("#hidsrvNm").val());

    BindMultiSelectDropDown("ddlPWP", response.PWPL);

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

    GetZScoreDetails();
    CalcualteBMI('hidPAYM', 'txtWeight', 'txtHeight', 'txtBMI', 'lblBMIClassification');
    HighLightAbnormalValues('temp', 'txtTemperature');
    VisibilityZScore();

    //$.hivce.loader('hide');

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

function BindDataTable(dtRows, disDiv, switchName, dtTable) {

    if (dtRows.length > 0) {
        $("#" + switchName).bootstrapSwitch('state', true);
        var ctrlVal = $("#" + switchName).bootstrapSwitch('state');
        $("#" + disDiv).removeAttr("style");
        if (ctrlVal) {
            $("#" + disDiv).css("visibility", "visible");
            $("#" + disDiv).css("display", "block");
        }
        else {
            $("#" + disDiv).css("visibility", "hidden");
            $("#" + disDiv).css("display", "none");
        }
        DrawDataTable(dtTable, dtRows);
    }
}


function EnableDisableContraceptiveHistory(chkValue) {
    $("#ddlFPM").select2("val", "0");
    $("#ddlNoFPReason").select2("val", "0");

    if (chkValue == 0) {
        $("#ddlFPM").prop('disabled', true);
        $("#ddlNoFPReason").prop('disabled', false);
    }
    else {
        $("#ddlFPM").prop('disabled', false);
        $("#ddlNoFPReason").prop('disabled', true);
    }
}

function ReInitialisePCDatatable(from) {
    if (from == 'PC') {
        $('#dtlPresentingComplaints').DataTable({
            "aaData": arrPresentingComplaints,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["PC"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["displayDate"];
                    }
                },
                {
                    bSortable: false,
                    mRender: function (data, type, full) {
                        if (full !== undefined) {
                            return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteRow('" + full["PCID"] + "','null','PC');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                        }
                        else {
                            return "<a class=\"btn btn-app btn-app-small\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                        }
                    }
                }
            ]
        });
    }


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


function DeleteRow(id, vid, from) {
    if (from == 'PC') {
        $('#dtlPresentingComplaints').dataTable().fnDestroy();
        ReInitialisePCDatatable(from);
        $.each(arrPresentingComplaints, function (i, el) {
            if (this.PCID == id) {
                arrPresentingComplaints.splice(i, 1);
            }
        });

        var table = $("#dtlPresentingComplaints").DataTable();
        table.rows().remove().draw();

        BindDataTable(arrPresentingComplaints, "divPresentingComplaints", "chkComplaints", "dtlPresentingComplaints");
    }

}

function BindDataTable(dtRows, disDiv, switchName, dtTable) {

    if (dtRows.length > 0) {
        $("#" + switchName).bootstrapSwitch('state', true);
        var ctrlVal = $("#" + switchName).bootstrapSwitch('state');
        $("#" + disDiv).removeAttr("style");
        if (ctrlVal) {
            $("#" + disDiv).css("visibility", "visible");
            $("#" + disDiv).css("display", "block");
        }
        else {
            $("#" + disDiv).css("visibility", "hidden");
            $("#" + disDiv).css("display", "none");
        }
        DrawDataTable(dtTable, dtRows);
    }
}

function AddPresentingComplaints() {
    var ddlPC = $("#ddlPresentingComplaints").select2("val");
    var data = $("#ddlPresentingComplaints").select2('data')[0];
    var dtOnsetDate = GetDateData("dtOnsetDate", true);

    if (ddlPC == null) {
        customAlert("Please select presenting complaint from the list");
        return;
    }

    if ($("#dtOnsetDate").val().length == 0) {
        customAlert("Please enter on set date");
        return;
    }

    var chkData = $.grep(arrPresentingComplaints, function (e) { return e.PCID == data.id; });

    if (jQuery.isEmptyObject(chkData) == true) {
        arrPresentingComplaints.push({ PCID: data.id, PC: data.text, OSD: dtOnsetDate, displayDate: $("#dtOnsetDate").val() });
        //arrPresentingComplaintsUI.push([data.text, $("#dtOnsetDate").val(), data.id]);
    }
    else {
        customAlert("Already exits in the table.");
    }
    DrawDataTable("dtlPresentingComplaints", arrPresentingComplaints);
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


function SaveRefillEncounterForm() {

    activePage = getPageName() + '.aspx';
    var action = "saverefillencounter";

    var rowData1 = PrepareSaveData();
    var errorMsg = CheckPCBlankValues();

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
                //console.log(response);
                var responseSuccess = response.Success;
                var msg = response.EM;
                if (responseSuccess == "true") {
                    //GetRefillEnounterFormData();
                    customAlert("Refill enounter form " + dataSuccessMessage.toLowerCase());
                    window.location.assign("../ClinicalForms/frmPatient_History.aspx?&sts=0");
                }
                else {
                    /*if (responseSuccess == "false") {
                    customAlert(errorMessage);
                    }
                    else {
                    customAlert(responseSuccess);
                    }*/
                    if (responseSuccess == "false") {
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
                        customAlert(responseSuccess);
                    }
                }

                $.eva.loader('hide');
            },
            error: function (msg) {
                //ShowErrorMessage(msg);
            }
        });
    }

}

function CheckPCBlankValues() {
    var gender = $("#hidGender").val();
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
    var chkComplaints = GetSwitchValue("chkComplaints");

    if (chkComplaints == 1) {
        if (jQuery.isEmptyObject(arrPresentingComplaints) == true) {
            errorField += 'Presenting Complaints ';
        }
    }

    var ddlPWP = $("#ddlPWP").select2("val");
    var age = $("#hidDOB").val();
    if (age > 14) {

        if (ddlPWP == null) {
            if (errorField.length > 1) {
                errorField += ', ';
            }
            errorField += 'Prevention with positives ';
        }
    }

    if (isNaN(errorField)) {
        errorMsg = errorField + 'cannot be blank.';
    }
    return errorMsg;
}
function PrepareSaveData() {
    var localdtformat = true, formName = '';

    var ddlPregnant = $("#ddlPregnant").select2("val");

    if (ddlPregnant == null) {
        ddlPregnant = 0;
    }
    var ddlNoFPReason = $("#ddlNoFPReason").select2("val");

    if (ddlNoFPReason == null) {
        ddlNoFPReason = 0;
    }

    var ddlFPM = $("#ddlFPM").select2("val");

    if (ddlFPM == null) {
        ddlFPM = 0;
    }
    var txtAreaReminders = $("#txtAreaReminders").val();

    var txtMissedDoses = $("#txtMissedDoses").val();
    if (txtMissedDoses.length == 0) {
        txtMissedDoses = 0;
    }

    rowData = [];
    rowData.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        OV: GetOVData(localdtformat),
        PV: GetPVData(),
        PCT: GetPCTData(),
        Pregnant: ddlPregnant,
        LMP: GetDateData("dtLMP", true),
        BS: GetSwitchValue("chkBreastFeeding"),
        FPS: GetSwitchValue("chkOnFamilyPlanning"),
        FPM: ddlFPM,
        NFP: ddlNoFPReason,
        PWP: GetControlMultiSelectValues("ddlPWP"),
        ISFM: GetSwitchValue("chkForgotMed"),
        NMD: txtMissedDoses,
        Drugs: $("#txtDrugs").val(),
        AREM: txtAreaReminders,
        Appointments: arrNxtAppointment,
        OPC: $("#txtComplaintsNote").val()
    });

    console.log(rowData);
    return rowData[0];
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
        SP: txtSP
    });
    return rowData3[0];
}

function GetPCTData() {
    rowData1 = [];
    if (jQuery.isEmptyObject(arrPresentingComplaints) == false) {
        $.each(arrPresentingComplaints, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                rowData1.push({ Ptn_Pk: 0, VId: 0, ValId: arrD.PCID, FN: "PresentingComplaints", OSD: arrD.OSD });
            }
        });
    }
    return rowData1;
}