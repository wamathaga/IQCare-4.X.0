
function InitPresentingComplaintsControls(response) {
    window.scrollTo(0, 0);
    arrPresentingComplaints = [];
    arrChronicDisorders = [];
    var todayDate = new Date();

    //Date picker
    $("#dtOnsetDate").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");
    $("#dtCCOnsetDate").datepicker({
        autoclose: true,
        endDate: todayDate
    })//.datepicker("setDate", "0");
    $("#dtLMP").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0")
    .on('changeDate', function () {
        debugger;
        if (jQuery.isEmptyObject($('#dtLMP')) == false) {
            var lmpdt = $('#dtLMP').val();
            var EDDdate = new Date(Date.parse(lmpdt, "MM/dd/yyyy"));   //Get the Date object with actual date
            EDDdate.setDate(EDDdate.getDate() + 280); //Set date object adding 3 days.
            //$("#dtEDD").datepicker("setDate", EDDdate);
            $("#hidDTEDD").val(EDDdate);
        }
    });
    $("#dtEDD").datepicker({
        autoclose: true
    }); //.datepicker("setDate", "0");
    $("#dtOtherDates").datepicker({
        autoclose: true
    }); //.datepicker("setDate", "0");
    $("#dtDateScreened").datepicker({
        autoclose: true
    }); //.datepicker("setDate", "0");
    $("#dtVDRL").datepicker({
        autoclose: true
    }); //.datepicker("setDate", "0");
    $("#dtRhesus").datepicker({
        autoclose: true
    }); //.datepicker("setDate", "0");
    $("#dtHb").datepicker({
        autoclose: true
    }); //.datepicker("setDate", "0");


    $("#ddlPresentingComplaints").select2();
    $("#ddlChronicDisorder").select2();
    $("#ddlReasonAmenorrhoea").select2();
    $("#ddlPregnant").select2();
    $("#ddlFPM").select2();
    $("#ddlNoFPReason").select2();
    $("#ddlVDRLResult").select2();
    $("#ddlRhesusResult").select2();

    $('#dtlPresentingComplaints').dataTable().fnDestroy();
    ReInitialisePCDatatable("PC");

    $('#dtlChronicDisorders').dataTable().fnDestroy();
    ReInitialisePCDatatable("CD");

    BindPCCombo(response);


    /*$('#dtlChronicDisorders').DataTable({
    "aaData": arrChronicDisordersUI,
    "bSort": false,
    "bPaginate": false,
    "bFilter": false,
    "bInfo": false,
    "autoWidth": true,
    columns: [
    { data: null, render: function (data, type, row) {
    // Combine the first and last names into a single table field
    return data.first_name + ' ' + data.last_name;
    } 
    },
    { data: "position" },
    { data: "office" },
    { data: "extn" },
    { data: "start_date" },
    { data: "salary", render: $.fn.dataTable.render.number(',', '.', 0, '$') },
    {
    data: null,
    className: "center",
    defaultContent: '<a href="" class="editor_edit">Edit</a> / <a href="" class="editor_remove">Delete</a>'
    }
    ]
    });*/



    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });

    $('#chkStillActive').on('ifChecked', function (event) {
        $(this).closest("input").attr('checked', true);
    });
    $('#chkStillActive').on('ifUnchecked', function (event) {
        $(this).closest("input").attr('checked', false);
    });

    $("#chkStillActive").iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });

    $('#chkOnFamilyPlanning').on('switchChange.bootstrapSwitch', function (event, state) {
        EnableDisableContraceptiveHistory(GetSwitchValue("chkOnFamilyPlanning"));
    });

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
    else if (from == 'CD') {
        $('#dtlChronicDisorders').DataTable({
            "aaData": arrChronicDisorders,
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
         { bSortable: false,
             mRender: function (data, type, full) {
                 return full["IsAVal"];
             }
         },
         {
             bSortable: false,
             mRender: function (data, type, full) {
                 if (full !== undefined) {
                     if (full["VId"] == $("#hidVId").val() || full["VId"] == "0") {
                         return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteRow('" + full["PCID"] + "','" + full["VId"] + "','CD');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
                     }
                     else {
                         return "";
                     }
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
    else if (from == 'CD') {
        $('#dtlChronicDisorders').dataTable().fnDestroy();

        ReInitialisePCDatatable(from);
        $.each(arrChronicDisorders, function (i, el) {
            if (this.PCID == id && this.VId == vid) {
                arrChronicDisorders.splice(i, 1);
            }
        });

        var table = $("#dtlChronicDisorders").DataTable();
        table.rows().remove().draw();

        BindDataTable(arrChronicDisorders, "divChronicDisorders", "chkChronicDisorders", "dtlChronicDisorders");

    }
}

function BindPCCombo(response) {
    var data = [];

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
    $.each(response.CC, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlChronicDisorder").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlChronicDisorder").select2("val", "0");
    $("#ddlChronicDisorder").trigger('change.select2');

    data = [];
    $.each(response.RFA, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlReasonAmenorrhoea").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlReasonAmenorrhoea").select2("val", "0");
    $("#ddlReasonAmenorrhoea").trigger('change.select2');

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
    $.each(response.FPM, function (index, value) {
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


    data = [];
    $.each(response.VDRL_RH, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });
    $("#ddlVDRLResult").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlVDRLResult").select2("val", "0");
    $("#ddlVDRLResult").trigger('change.select2');

    $("#ddlRhesusResult").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });

    $("#ddlRhesusResult").select2("val", "0");
    $("#ddlRhesusResult").trigger('change.select2');

    $("#chkComplaints").bootstrapSwitch('state', false);
    $("#chkChronicDisorders").bootstrapSwitch('state', false);
    $("#chkBreastFeeding").bootstrapSwitch('state', false);
    $("#chkOnFamilyPlanning").bootstrapSwitch('state', false);
    $("#chkCCS").bootstrapSwitch('state', false);
    $("#chkHPVVaccine").bootstrapSwitch('state', false);
    $("#chkANCProfileDone").bootstrapSwitch('state', false);

    $("#dtEDD").prop('disabled', true);
    $("#dtOtherDates").prop('disabled', true);
    //$("#txtParity").prop('disabled', true);
    //$("#txtGravidae").prop('disabled', true);

    //// ANC Profile Section hide or visible.......
    $("#ddlPregnant").select2().on('change', function () {
        // var preg =  $("#ddlPregnant").select2().val();
        debugger;
        var preg = $(this).val();
        var pregData = $(this).select2('data')[0];
        if (preg != null) {
            if (pregData.text.toUpperCase() == "YES") {
                $("#divContraceptiveHistory").removeClass().addClass("box box-primary collapsed-box");
                $("#divContraceptiveHistoryI").removeClass("fa fa-minus").addClass("fa fa-plus");
            }
            else {
                $("#divContraceptiveHistory").removeClass().addClass("box box-primary");
                $("#divContraceptiveHistoryI").removeClass("fa fa-minus").addClass("fa fa-minus");
            }
            $("#dtEDD").datepicker('setDate', "");
            if (pregData.text.toUpperCase() == "NO") {
                $("#dtEDD").prop('disabled', true);
                $("#dtOtherDates").prop('disabled', true);
                //$("#txtParity").prop('disabled', true);
                //$("#txtGravidae").prop('disabled', true);
            }
            else {
                if (pregData.text.toUpperCase().indexOf("INDUCED ABORTION") >= 0) {
                    $("#dtEDD").prop('disabled', true);
                    $("#dtOtherDates").prop('disabled', false);
                }
                else if (pregData.text.toUpperCase().indexOf("MISCARRIAGE") >= 0) {
                    $("#dtEDD").prop('disabled', true);
                    $("#dtOtherDates").prop('disabled', false);
                }
                else {
                    $("#dtEDD").prop('disabled', false);
                    var eddDate = new Date(Date.parse($("#hidDTEDD").val(), "MM/dd/yyyy"));
                    var fmtEDD = $.format.date(eddDate, "MM/dd/yyyy");
                    $("#dtEDD").datepicker("setDate", fmtEDD);
                    $("#dtOtherDates").prop('disabled', true);
                    //$("#txtParity").prop('disabled', false);
                    //$("#txtGravidae").prop('disabled', false);

                }
            }
        }

        var data = $(this).select2('data')[0];
        var edd = $("#dtEDD").val();
        var mis = $("#dtOtherDates").val();
        if (preg != null) {
            if (data.text.toLowerCase() == "yes") {
                $("#chkANCProfileDone").bootstrapSwitch('state', true);
                $("#divANCProfileHead").css("visibility", "visible");
                $("#divANCProfileHead").css("display", "block");
            }
            else {
                $("#chkANCProfileDone").bootstrapSwitch('state', false);
                $("#divANCProfileHead").css("visibility", "hidden");
                $("#divANCProfileHead").css("display", "none");
            }
        }
    }).trigger('change');

    BindPCData(response);
}

function BindPCData(response) {
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

    $.each(response.CDT, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            var data = $.grep(response.CC, function (e) { return e.DId == arrD.ValId; });
            if (jQuery.isEmptyObject(data) == false) {
                var displayDate = "";
                var osd = arrD.OSD;
                if (jQuery.isEmptyObject(arrD.OSD) == false) {
                    // parse JSON formatted date to javascript date object
                    var dtOSD = new Date(parseInt(arrD.OSD.substr(6)));
                    // format display date (e.g. 04/10/2012)
                    displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");

                    /*var dt = new Date(parseInt(arrD.OSD.substr(6))).toString();
                    console.log(dt); // Mon Jul 16 2012 15:21:09 GMT+0530 (India Standard Time)
                    dt = dt.substr(0, dt.indexOf('GMT'));
                    console.log(dt); // Mon Jul 16 2012 15:21:09 
                    osd = convertToJSONDate(dt, true);*/
                }
                var IsA = 0;
                var IsAVal = "false";
                if (arrD.NF == "1") {
                    IsA = 1;
                    IsAVal = "true";
                }
                arrChronicDisorders.push({ PCID: arrD.ValId, PC: data[0].DN, OSD: arrD.OSD, IsActive: IsA, IsAVal: IsAVal, displayDate: displayDate, VId: arrD.VId });
            }
        }
    });
    BindDataTable(arrChronicDisorders, "divChronicDisorders", "chkChronicDisorders", "dtlChronicDisorders");

    debugger;

    if (response.OBSGYN.FPS == 1) {
        $("#chkOnFamilyPlanning").bootstrapSwitch('state', true);
        $("#ddlFPM").prop('disabled', false);
        $("#ddlNoFPReason").prop('disabled', true);
    }
    else {
        $("#ddlFPM").prop('disabled', true);
        $("#ddlNoFPReason").prop('disabled', false);
    }

    //CheckDatenAssign(response.OBSGYN.EDD, "dtEDD", false);
    $("#hidDTEDD").val(response.OBSGYN.EDD);

    $("#ddlReasonAmenorrhoea").select2().val(response.OBSGYN.Amenorrhoea).trigger("change");
    $("#ddlPregnant").select2().val(response.OBSGYN.Pregnant).trigger("change");
    $("#ddlFPM").select2().val(response.OBSGYN.FPM).trigger("change");
    $("#ddlNoFPReason").select2().val(response.OBSGYN.NFP).trigger("change");
    $("#ddlRhesusResult").select2().val(response.OBSGYN.RhesusTR).trigger("change");
    $("#ddlVDRLResult").select2().val(response.OBSGYN.VDRLTR).trigger("change");


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


    $("#txtAgeAtMenarche").val(response.OBSGYN.Menarche);
    $("#txtParity").val(response.OBSGYN.Parity);
    $("#txtGravidae").val(response.OBSGYN.Gravidae);
    $("#txtHbResult").val(response.OBSGYN.HbTR);

    $("#txtComplaintsNote").val(response.OBSGYN.OPC);
    $("#txtPreviousAdmissions").val(response.OBSGYN.PAD);

    if (response.OBSGYN.HPVV == 1) {
        $("#chkHPVVaccine").bootstrapSwitch('state', true);
    }
    if (response.OBSGYN.BS == 1) {
        $("#chkBreastFeeding").bootstrapSwitch('state', true);
    }
    if (response.OBSGYN.CCS == 1) {
        $("#chkCCS").bootstrapSwitch('state', true);
    }

    CheckDatenAssign(response.OBSGYN.LMP, "dtLMP", false);

    CheckDatenAssign(response.OBSGYN.CCSD, "dtDateScreened", false);
    CheckDatenAssign(response.OBSGYN.DOD, "dtOtherDates", false);

    CheckDatenAssign(response.OBSGYN.VDRLTD, "dtVDRL", false);

    CheckDatenAssign(response.OBSGYN.HbTD, "dtHb", false);

    CheckDatenAssign(response.OBSGYN.RhesusTD, "dtRhesus", false);

    var age = $("#hidDOB").val();
    var gender = $("#hidGender").val();

    if (gender.toLowerCase() == "female") {
        if (age < 12) {
            $("#divOBSGYN").css("visibility", "hidden");
            $("#divOBSGYN").css("display", "none");
        }
        if (age < 18) {
            $("#divCervicalCancer").css("visibility", "hidden");
            $("#divCervicalCancer").css("display", "none");
        }


    }
    else if (gender.toLowerCase() == "male") {
        $("#divOBSGYN").css("visibility", "hidden");
        $("#divOBSGYN").css("display", "none");
    }


    $.hivce.loader('hide');
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

function AddChronicDisorder() {
    var ddlPC = $("#ddlChronicDisorder").select2("val");
    var data = $("#ddlChronicDisorder").select2('data')[0];
    var dtOnsetDate = GetDateData("dtCCOnsetDate", true);


    if (ddlPC == null) {
        customAlert("Please select chronic disorder from the list");
        return;
    }

    if ($("#dtCCOnsetDate").val().length == 0) {
        customAlert("Please enter on set date");
        return;
    }
    debugger;
    var chkData = false;
    //    $.grep(arrChronicDisorders, function (e) {
    //        return e.PCID == data.id && e.OSD == dtOnsetDate;
    //    });

    $.each(arrChronicDisorders, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            if (jQuery.isEmptyObject(arrD.OSD) == false) {
                var dt = new Date(parseInt(arrD.OSD.substr(6))).toString();
                //console.log(dt); // Mon Jul 16 2012 15:21:09 GMT+0530 (India Standard Time)
                dt = dt.substr(0, dt.indexOf('GMT'));
                //console.log(dt); // Mon Jul 16 2012 15:21:09 
                osd = convertToJSONDate(dt, true);

                var dtOSD = new Date(parseInt(osd.substr(6)));
                dtOSD.setHours(0, 0, 0, 0);
                var dt = new Date(parseInt(dtOnsetDate.substr(6)));
                dt.setHours(0, 0, 0, 0);
                if (arrD.PCID == data.id) {
                    if (dtOSD.getTime() === dt.getTime()) {
                        chkData = true;
                    }
                }
            }
        }
    });

    if (chkData == false) {
        //chkStillActive
        var IsA = 0;
        var IsAVal = "false";
        if ($('#chkStillActive').is(":checked")) {
            IsA = 1;
            IsAVal = "true";
        }
        arrChronicDisorders.push({ PCID: data.id, PC: data.text, OSD: dtOnsetDate, IsActive: IsA, IsAVal: IsAVal, displayDate: $("#dtCCOnsetDate").val(), VId: 0 });
        //arrChronicDisordersUI.push([data.text, $("#dtCCOnsetDate").val(), IsAVal, data.id]);
    }
    else {
        customAlert("Already exits in the table.");
    }

    DrawDataTable("dtlChronicDisorders", arrChronicDisorders);
}


function SavePresentingComplaints() {

    activePage = getPageName() + '.aspx';
    var action = "savepc";

    var rowData1 = PreparePCSaveData();
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
                if (responseSuccess == "true") {
                    GetPresentingComplaintsData();
                    customAlert("Presenting complaints tab " + dataSuccessMessage.toLowerCase());
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

function CheckPCBlankValues() {
    errorMsg = '';
    var errorField = '';
    var age = $("#hidDOB").val();
    var gender = $("#hidGender").val();

    var chkComplaints = GetSwitchValue("chkComplaints");

    if (chkComplaints == 1) {
        if (jQuery.isEmptyObject(arrPresentingComplaints) == true) {
            errorField += 'Presenting Complaints ';
        }
    }
    if ($("#hidGender").val().toLowerCase() == "female") {
        var age = $("#hidDOB").val();
        debugger;
        if (age > 12) {
            var ddlPregnant = $("#ddlPregnant").select2("val");
            if (ddlPregnant == null) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'Pregnant ';
            }
            else {

                var pregData = $("#ddlPregnant").select2('data')[0];
                if (pregData.text.toUpperCase() == "YES") {

                    var chkANCProfileDone = GetSwitchValue("chkANCProfileDone");
                    if (chkANCProfileDone == 1) {

                        var ddlVDRLResult = $("#ddlVDRLResult").select2("val");
                        if (ddlVDRLResult != null) {
                            if ($("#dtVDRL").val().length == 0) {
                                if (errorField.length > 1) {
                                    errorField += ', ';
                                }
                                errorField += 'VDRL date ';
                            }
                        }

                        var txtHbResult = $("#txtHbResult").val();
                        if (parseInt(txtHbResult) > 0) {
                            if ($("#dtHb").val().length == 0) {
                                if (errorField.length > 1) {
                                    errorField += ', ';
                                }
                                errorField += 'HB date ';
                            }
                        }

                        var ddlRhesusResult = $("#ddlRhesusResult").select2("val");
                        if (ddlRhesusResult != null) {
                            if ($("#dtRhesus").val().length == 0) {
                                if (errorField.length > 1) {
                                    errorField += ', ';
                                }
                                errorField += 'Rhesus date ';
                            }
                        }
                    }
                }
            }

        }
    }
    if (gender.toLowerCase() == "female") {
        if (age > 12) {
            var chkOnFamilyPlanning = GetSwitchValue("chkOnFamilyPlanning");
            if (chkOnFamilyPlanning == 1) {
                var ddlFPM = $("#ddlFPM").select2("val");
                if (ddlFPM == null) {
                    if (errorField.length > 1) {
                        errorField += ', ';
                    }
                    errorField += 'Family Planning Method ';
                }
            } else {
                var ddlNoFPReason = $("#ddlNoFPReason").select2("val");
                var ddlPregnant = $("#ddlPregnant").select2("val");
                if (ddlPregnant != null) {
                    var pregData = $("#ddlPregnant").select2('data')[0];
                    if (pregData.text.toUpperCase() != "YES") {
                        if (ddlNoFPReason == null) {
                            if (errorField.length > 1) {
                                errorField += ', ';
                            }
                            errorField += 'Reason not on Family Planning ';
                        }
                    }
                }
            }
        }
    }


    if (isNaN(errorField)) {
        errorMsg = errorField + 'cannot be blank.';
    }
    return errorMsg;
}

function PreparePCSaveData() {
    var localdtformat = true;
    //alert(GetDateData("dtVisitDate", localdtformat));
    rowData = [];
    rowData.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        OBSGYN: GetOBSGYNData(),
        PCT: GetPCTData(),
        CDT: GetCDTData()
    });

    console.log(rowData);
    return rowData[0];
}

function GetOBSGYNData() {


    var ddlReasonAmenorrhoea = $("#ddlReasonAmenorrhoea").select2("val");

    if (ddlReasonAmenorrhoea == null) {
        ddlReasonAmenorrhoea = 0;
    }

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

    var ddlVDRLResult = $("#ddlVDRLResult").select2("val");

    if (ddlVDRLResult == null) {
        ddlVDRLResult = 0;
    }

    var ddlRhesusResult = $("#ddlRhesusResult").select2("val");

    if (ddlRhesusResult == null) {
        ddlRhesusResult = 0;
    }

    var txtHbResult = $("#txtHbResult").val();
    if (txtHbResult.length == 0) {
        txtHbResult = 0;
    }

    var txtParity = $("#txtParity").val();
    if (txtParity.length == 0) {
        txtParity = 0;
    }

    var txtGravidae = $("#txtGravidae").val();
    if (txtGravidae.length == 0) {
        txtGravidae = 0;
    }

    rowData1 = [];
    rowData1.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        Menarche: $("#txtAgeAtMenarche").val(),
        HPVV: GetSwitchValue("chkHPVVaccine"),
        Amenorrhoea: ddlReasonAmenorrhoea,
        BS: GetSwitchValue("chkBreastFeeding"),
        CCS: GetSwitchValue("chkCCS"),
        DOD: GetDateData("dtOtherDates", true),
        DOIA: null,
        DOM: null,
        EDD: GetDateData("dtEDD", true),
        FPM: ddlFPM,
        FPS: GetSwitchValue("chkOnFamilyPlanning"),
        Gravidae: txtGravidae,
        LMP: GetDateData("dtLMP", true),
        NFP: ddlNoFPReason,
        Parity: txtParity,
        Pregnant: ddlPregnant,
        CCSD: GetDateData("dtDateScreened", true),
        VDRLTD: GetDateData("dtVDRL", true),
        VDRLTR: ddlVDRLResult,
        HbTD: GetDateData("dtHb", true),
        HbTR: txtHbResult,
        RhesusTD: GetDateData("dtRhesus", true),
        RhesusTR: ddlRhesusResult,
        OPC: $("#txtComplaintsNote").val(),
        PAD: $("#txtPreviousAdmissions").val()
    });
    return rowData1[0];
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

function GetCDTData() {
    rowData1 = [];
    if (jQuery.isEmptyObject(arrChronicDisorders) == false) {
        $.each(arrChronicDisorders, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.VId == 0 || arrD.VId == $("#hidVId").val()) {
                    rowData1.push({ Ptn_Pk: 0, VId: arrD.VId, ValId: arrD.PCID, FN: "ChronicCondition", OSD: arrD.OSD, NF: arrD.IsActive });
                }
            }
        });
    }
    return rowData1;
}