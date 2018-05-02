function InitVisitDetailControls(response) {
    arrVisitAnthropometric = [];
    arrPresentPregnancy = [];

    $("#ddVDDate").datepicker({
        autoclose: true
    });


    $('#divVisitDetails input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });

    $("#dtFirstVisitDate").datepicker({
        autoclose: true
    });
    $("#dtVDDate").datepicker({
        autoclose: true
    });

    //    debugger;
    //    $("input:radio[name=rbPS]").click(function () {
    //        var selected_choice = $(this).val();
    //        if (selected_choice == 'ANC') {
    //            $("#txtVisitNumber").attr("disabled", false);
    //        }
    //        else {
    //            $("#txtVisitNumber").attr("disabled", true);
    //        }
    //    })

    $("#ddlLie").select2();
    $("#ddlFoetalMovement").select2();
    $("#ddlPresentation").select2();
    $("#chkTT").bootstrapSwitch('state', false);

    $("#ddlIPTp").select2();
    $("#dtIPTp").datepicker({
        autoclose: true
    });
    $("#chkInsectisideTreated").bootstrapSwitch('state', false);
    $("#dtInsectisideTreated").datepicker({
        autoclose: true
    });
    $("#chkDewormed").bootstrapSwitch('state', false);
    $("#dtDewormed").datepicker({
        autoclose: true
    });
    $("#chkIronFolate").bootstrapSwitch('state', false);
    $("#dtIronFolate").datepicker({
        autoclose: true
    });
    $("#chkInfantFeedingCounselling").bootstrapSwitch('state', false);
    $("#chkExBreastfeedingCounselling").bootstrapSwitch('state', false);
    $("#chkHIVIFIF").bootstrapSwitch('state', false);
    $("#ddlMDIF").select2();
    $("#chkACERD").bootstrapSwitch('state', false);

    $("#dtPOD").datepicker({
        autoclose: true
    });
    $("#ddlMOD").select2();
    $("#ddlBloodloss").select2();
    $("#chkResusitationdone").bootstrapSwitch('state', false);
    $("#dtNextAppointmentDate").datepicker({
        autoclose: true
    });
    $("#ddlPurpose").select2();

    $("#dtlNextAppointment").datepicker({
        autoclose: true
    });

    $('#dtlVisitAnthropometric').dataTable().fnDestroy();
    ReInitialiseDatatable("VA");

    $('#dtlPresentPregnancy').dataTable().fnDestroy();
    ReInitialiseDatatable("PREG");
    debugger;
    ReInitialiseAppointmentsDatatable();

    BindVDControls(response);


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
        //            { bSortable: false,
        //                mRender: function (data, type, full) {
        //                    return full["Id"];
        //                }
        //            },
            {bSortable: false,
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
            }
        //            ,{
        //                bSortable: false,
        //                mRender: function (data, type, full) {
        //                    return "";
        //                    //return "<a class=\"btn btn-app btn-app-small\" onclick=\"DeleteICD10Row('" + full["ICDId"] + "');\"><i class=\"glyphicon glyphicon-trash\"></i></a>";
        //                }
        //            }
        ]
    });
}

function BindVDControls(response) {
    var data;
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

    CheckDatenAssign(response.VD.FVD, "dtFirstVisitDate", false);

    //$("input:radio[name=rbANC][value=" + response.VD.CAIDA + "]").attr('checked', 'checked');
    //$("input:radio[name=rbANC]").iCheck('update');


    $("#ddlIPTp").val(response.VD.IPTP).trigger("change");

    if (response.VD.TTV == 1) {
        $("#chkTT").bootstrapSwitch('state', true);
    }
    $("#txtTTNotes").val(response.VD.TTVR);

    $("#ddlIPTp").val(response.VD.IPTP).trigger("change");
    CheckDatenAssign(response.VD.IPTPDG, "dtIPTp", false);

    if (response.VD.ITN == 1) {
        $("#chkInsectisideTreated").bootstrapSwitch('state', true);
    }
    CheckDatenAssign(response.VD.ITNDG, "dtInsectisideTreated", false);

    if (response.VD.Dewormed == 1) {
        $("#chkDewormed").bootstrapSwitch('state', true);
    }
    CheckDatenAssign(response.VD.DewormedDG, "dtDewormed", false);

    if (response.VD.IAF == 1) {
        $("#chkIronFolate").bootstrapSwitch('state', true);
    }
    CheckDatenAssign(response.VD.IAFDG, "dtIronFolate", false);

    if (response.VD.IFC == 1) {
        $("#chkInfantFeedingCounselling").bootstrapSwitch('state', true);
    }
    if (response.VD.EBF == 1) {
        $("#chkExBreastfeedingCounselling").bootstrapSwitch('state', true);
    }
    if (response.VD.HIVIFO == 1) {
        $("#chkHIVIFIF").bootstrapSwitch('state', true);
    }

    $("#ddlMDIF").val(response.VD.MDIF).trigger("change");

    if (response.VD.ER == 1) {
        $("#chkACERD").bootstrapSwitch('state', true);
    }
    $("#txtPOD").val(response.VD.POD);
    CheckDatenAssign(response.VD.PODD, "dtPOD", false);
    $("#ddlMOD").val(response.VD.MOD).trigger("change");
    $("#ddlBloodloss").val(response.VD.BL).trigger("change");

    $("#txtApgarScore1min").val(response.VD.AS1M);
    $("#txtApgarScore5min").val(response.VD.AS5M);
    $("#txtApgarScore10min").val(response.VD.AS10M);

    if (response.VD.RD == 1) {
        $("#chkResusitationdone").bootstrapSwitch('state', true);
    }

    $.each(response.VA, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            var displayDate = "";
            var osd = arrD.TD;
            if (jQuery.isEmptyObject(arrD.VDD) == false) {
                // parse JSON formatted date to javascript date object
                var dtOSD = new Date(parseInt(arrD.VDD.substr(6)));
                // format display date (e.g. 04/10/2012)
                displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
            }
            arrVisitAnthropometric.push({
                VD: arrD.VDD
                , PS: arrD.PS
                , VN: arrD.VN
                , Height: arrD.Height
                , Weight: arrD.Weight
                , MUAC: arrD.MUAC
                , BPD: arrD.BPD
                , BPS: arrD.BPS
                , displayDate: displayDate
            });

        }
    });

    DrawDataTable("dtlVisitAnthropometric", arrVisitAnthropometric);

    debugger;
    $.each(response.PREG, function (index, arrP) {
        if (jQuery.isEmptyObject(arrP) == false) {
            arrPresentPregnancy.push({
                M: arrP.M
                , Lie: arrP.Lie
                , FH: arrP.FH
                , FM: arrP.FM
                , P: arrP.P
                , FHR: arrP.FHR
            });
        }
    });

    DrawDataTable("dtlPresentPregnancy", arrPresentPregnancy);

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

    $.hivce.loader('hide');

}


function AddVisitAnthropometric() {
    debugger;
    var rbANC = $("input:radio[name=rbPS]:checked").val();
    var anc = (rbANC === undefined) ? '' : rbANC;

    arrVisitAnthropometric.push({
        VDD: GetDateData("dtVDDate", true)
        , State: anc
        , PS: anc
        , VN: $("#txtVisitNumber").val()
        , Height: $("#txtHeight").val()
        , Weight: $("#txtWeight").val()
        , MUAC: $("#txtMUAC").val()
        , BPD: $("#txtMM").val()
        , BPS: $("#txtHG").val()
        , displayDate: $("#dtVDDate").val()
    });

    DrawDataTable("dtlVisitAnthropometric", arrVisitAnthropometric);

}


function AddPresentPregnancy() {
    debugger;
    var ddlLie = $("#ddlLie").select2('data')[0];
    var ddlFoetalMovement = $("#ddlFoetalMovement").select2('data')[0];
    var ddlPresentation = $("#ddlPresentation").select2('data')[0];

    arrPresentPregnancy.push({
        M: $("#txtMaturity").val()
        , Lie: ddlLie.text
        , FH: $("#txtFundalHeight").val()
        , FM: ddlFoetalMovement.text
        , P: ddlPresentation.text
        , FHR: $("#txtFoetalHeartRate").val()
    });

    DrawDataTable("dtlPresentPregnancy", arrPresentPregnancy);

}

function AddAppointment(ref) {
    debugger;
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
    }

    var currDt = new Date();
    if ($("#dtNextAppointmentDate").val() < currDt.format('dd-MMM-yyyy')) {
        customAlert("Appointment date should be greater then Today's date");
        return;
    }


    //dtDate
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


function SaveVisitDetails() {
    debugger;
    activePage = getPageName() + '.aspx';
    var action = "savevisitdetailsdata";

    var rowprofileData = PrepareVisitDetails();
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
                    customAlert("Visit Details tab " + dataSuccessMessage.toLowerCase());
                    window.location.assign("../ClinicalForms/frmPatient_History.aspx?&sts=0");
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

function PrepareVisitDetails() {
    var localdtformat = true;
    var ddlIPTp = $("#ddlIPTp").select2("val");

    if (ddlIPTp == null) {
        ddlIPTp = 0;
    }

    var ddlMDIF = $("#ddlMDIF").select2("val");

    if (ddlMDIF == null) {
        ddlMDIF = 0;
    }

    var ddlMOD = $("#ddlMOD").select2("val");

    if (ddlMOD == null) {
        ddlMOD = 0;
    }
    var ddlBloodloss = $("#ddlBloodloss").select2("val");

    if (ddlBloodloss == null) {
        ddlBloodloss = 0;
    }

    rowData = [];
    rowData.push({
        Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        VD: {
            AS10M: $("#txtApgarScore10min").val(),
            AS1M: $("#txtApgarScore1min").val(),
            AS5M: $("#txtApgarScore5min").val(),
            BL: ddlBloodloss,
            Dewormed: GetSwitchValue("chkDewormed"),
            DewormedDG: GetDateData("dtDewormed", localdtformat),
            EBF: GetSwitchValue("chkExBreastfeedingCounselling"),
            ER: GetSwitchValue("chkACERD"),
            FVD: GetDateData("dtFirstVisitDate", true),
            HIVIFO: GetSwitchValue("chkHIVIFIF"),
            MDIF: ddlMDIF,
            IAF: GetSwitchValue("chkIronFolate"),
            IAFDG: GetDateData("dtIronFolate", true),
            IFC: GetSwitchValue("chkInfantFeedingCounselling"),
            IPTP: ddlIPTp,
            IPTPDG: GetDateData("dtIPTp", localdtformat),
            ITN: GetSwitchValue("chkInsectisideTreated"),
            ITNDG: GetDateData("dtInsectisideTreated", localdtformat),
            MOD: ddlMOD,
            POD: $("#txtPOD").val(),
            PODD: GetDateData("dtPOD", localdtformat),
            RD: GetSwitchValue("chkResusitationdone"),
            TTV: GetSwitchValue("chkTT"),
            TTVR: $("#txtTTNotes").val()
        },
        VA: arrVisitAnthropometric,
        PREG: arrPresentPregnancy,
        Appointments: arrNxtAppointment

    });
    console.log(rowData);
    return rowData[0];
}