var arrNxtAppointment = [];
var arrLabResult = [];

function InitPrEPInitiationControls(response) {
    debugger;
    window.scrollTo(0, 0);
    //Date picker
    var currentDate = new Date();
    $("#dtNextAppointmentDate").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");
    $("#chkDrugCollected").bootstrapSwitch('state', false);
    $("#ddlHIVTestResults").select2();
    $("#chkReferredToCCC").bootstrapSwitch('state', false);
    $("#chkHepatitisBVaccine").bootstrapSwitch('state', false);
    $("#chkPreviousPrEPuse").bootstrapSwitch('state', false);
    $("#chkWillingToStartPrEP").bootstrapSwitch('state', false);
    $("#chkSymptomsOfHIV").bootstrapSwitch('state', false);
    $("#chkMedicallyToStartPrep").bootstrapSwitch('state', false);
    $("#chkContraindications").bootstrapSwitch('state', false);
    $("#chkSigns_Symptoms").bootstrapSwitch('state', false);
    $("#ddlSelfAssessment").select2();
    $("#ddlWhyPoorFair").select2();
    $("#ddlPrepStatus").select2();
    $("#ddlReasonFordiscontinuation").select2();
    $("#ddlPreRegimen").select2();
    $("#chkCondomsIssed").bootstrapSwitch('state', false);
    $("#chkAdherenceCounsellingDone").bootstrapSwitch('state', false);
    $("#chkPrescribedPrEPtoday").bootstrapSwitch('state', false);

    $('#dtlLabResults').dataTable().fnDestroy();
    ReInitialiseDatatable("LabR", response);

    $('#dtlNextAppointment').dataTable().fnDestroy();
    ReInitialiseAppointmentsDatatable();

    BindInitiationControls(response);


}

function BindInitiationControls(response) {
    debugger;
    $("#ddlHIVTestResults").val(response.INIT.HIVR).trigger("change");
    $("#chkDrugCollected").bootstrapSwitch('state', ConvertValueBool(response.INIT.ISC));
    $("#chkReferredToCCC").bootstrapSwitch('state', ConvertValueBool(response.INIT.RCCC));
    $("#chkHepatitisBVaccine").bootstrapSwitch('state', ConvertValueBool(response.INIT.HBV));
    $("#chkPreviousPrEPuse").bootstrapSwitch('state', ConvertValueBool(response.INIT.PP));
    $("#chkWillingToStartPrEP").bootstrapSwitch('state', ConvertValueBool(response.INIT.WTSP));
    $("#ddlPrepStartNo").val(response.INIT.IWTSPN).trigger("change");
    $("#ddlSelfAssessment").val(response.INIT.PA).trigger("change");
    $("#txtPillCount").val(response.INIT.PILC);
    $("#ddlWhyPoorFair").val(response.INIT.WPFS).trigger("change");
    $("#ddlPrepStatus").val(response.INIT.PEPS).trigger("change");
    $("#ddlReasonFordiscontinuation").val(response.INIT.PRDR).trigger("change");
    $("#ddlPreRegimen").val(response.INIT.PR).trigger("change");
    $("#chkCondomsIssed").bootstrapSwitch('state', ConvertValueBool(response.INIT.CI));
    $("#chkAdherenceCounsellingDone").bootstrapSwitch('state', ConvertValueBool(response.INIT.ACD));
    $("#chkPrescribedPrEPtoday").bootstrapSwitch('state', ConvertValueBool(response.INIT.PPT));
    $("#txtRemindersWorkplan").val(response.INIT.WP);

    BindPrepInitiationCombo(response);


    //    var vLBID = response.INIT.LBID;
    //    var vLBID2 = response.INIT.HIVR;
}

function BindPrepInitiationCombo(response) {
    debugger;
    var data = [];
    $.each(response.PUR, function (index, value) {
        data.push({ id: value.CId, text: value.CN });
    });
    $("#ddlHIVTestResults").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlHIVTestResults").select2("val", "0");
    $("#ddlHIVTestResults").trigger('change.select2');
    //
    data = [];
    $.each(response.SPN, function (index, value) {
        data.push({ id: value.CId, text: value.CN });
    });
    $("#ddlPrepStartNo").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlPrepStartNo").select2("val", "0");
    $("#ddlPrepStartNo").trigger('change.select2');
    data = [];
    //
    $.each(response.PADH, function (index, value) {
        data.push({ id: value.CId, text: value.CN });
    });
    $("#ddlSelfAssessment").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlSelfAssessment").select2("val", "0");
    $("#ddlSelfAssessment").trigger('change.select2');
    data = [];
    //
    $.each(response.WPF, function (index, value) {
        data.push({ id: value.CId, text: value.CN });
    });
    $("#ddlWhyPoorFair").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlWhyPoorFair").select2("val", "0");
    $("#ddlWhyPoorFair").trigger('change.select2');
    data = [];
    //
    $.each(response.PS, function (index, value) {
        data.push({ id: value.CId, text: value.CN });
    });
    $("#ddlPrepStatus").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlPrepStatus").select2("val", "0");
    $("#ddlPrepStatus").trigger('change.select2');
    data = [];
    //
    $.each(response.RGMN, function (index, value) {
        data.push({ id: value.CId, text: value.CN });
    });
    $("#ddlPreRegimen").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlPreRegimen").select2("val", "0");
    $("#ddlPreRegimen").trigger('change.select2');
    data = [];
    //
    $.each(response.REGRESON, function (index, value) {
        data.push({ id: value.CId, text: value.CN });
    });
    $("#ddlReasonFordiscontinuation").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlReasonFordiscontinuation").select2("val", "0");
    $("#ddlReasonFordiscontinuation").trigger('change.select2');
    data = [];
    //
    $.each(response.APTR, function (index, value) {
        data.push({ id: value.CId, text: value.CN });
    });
    $("#ddlPurpose").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlPurpose").select2("val", "0");
    $("#ddlPurpose").trigger('change.select2');
    data = [];
    
    arrNxtAppointment = [];
    if (jQuery.isEmptyObject(response.APT) == false) {
        $.each(response.APT, function (index, value) {
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
        DrawDataTable("dtlNextAppointment", arrNxtAppointment);

    }
    $.hivce.loader('hide');

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



//SAVE DATA START
function SaveUpdatePrEPInitiationData(tabName) {
    //alert('Start Saving........')
    $.hivce.loader('show');
    var data = PreparePrEPInitiationData();
    PostPrEPInitiationDataData(data, false, tabName);
}

function PreparePrEPInitiationData() {
    var varSampleCollected = 0;
    if ($('#chkDrugCollected').is(':checked'))
        varSampleCollected = 1;
    var varRCCC = 0;
    if ($('#chkReferredToCCC').is(':checked'))
        varRCCC = 1;
    var varHBV = 0;
    if ($('#chkHepatitisBVaccine').is(':checked'))
        varHBV = 1;
    var varPrePrep = 0;
    if ($('#chkPreviousPrEPuse').is(':checked'))
        varPrePrep = 1;
    var varWTSP = 0;
    if ($('#chkPreviousPrEPuse').is(':checked'))
        varWTSP = 1;
    var varIWTSPY = 0;
    if ($('#chkWillingToStartPrEP').is(':checked'))
        varIWTSPY = 1;
    var varCI = 0;
    if ($('#chkCondomsIssed').is(':checked'))
        varCI = 1;
    var varACD = 0;
    if ($('#chkAdherenceCounsellingDone').is(':checked'))
        varACD = 1;
    var varPPT = 0;
    if ($('#chkPrescribedPrEPtoday').is(':checked'))
        varPPT = 1;
    var varPRDR = 0;
    if ($("#ddlReasonFordiscontinuation").select2("val") != null)
        varPRDR = $("#ddlReasonFordiscontinuation").select2("val");
    var varHIVR = 0;
    if ($("#ddlHIVTestResults").select2("val") != null)
        varHIVR = $("#ddlHIVTestResults").select2("val");
    var varIWTSPN = 0;
    if ($("#ddlPrepStartNo").select2("val") != null)
        varIWTSPN = $("#ddlPrepStartNo").select2("val");
    var varPA = 0;
    if ($("#ddlSelfAssessment").select2("val") != null)
        varPA = $("#ddlSelfAssessment").select2("val");
    var varWPFS = 0;
    if ($("#ddlWhyPoorFair").select2("val") != null)
        varWPFS = $("#ddlWhyPoorFair").select2("val");
    var varPEPS = 0;
    if ($("#ddlPrepStatus").select2("val") != null)
        varPEPS = $("#ddlPrepStatus").select2("val");
    var varPR = 0;
    if ($("#ddlPreRegimen").select2("val") != null)
        varPR = $("#ddlPreRegimen").select2("val");

    var table = document.getElementById("dtlLabResults");
    var tableArr = [];
    var varOT = false;
    var chkList = document.getElementById("dtlLabResults").getElementsByTagName("input")
    for (var i = 1; i < table.rows.length; i++) {
        if (chkList[i - 1].checked == true) {
            varOT = true;
        }
        tableArr.push({
            LTID: table.rows[i].cells[0].innerHTML,
            OT: varOT
        });
    }
    //

    //debugger;
    rowData = [];
    rowData.push({
        HIVR: varHIVR,
        ISC: varSampleCollected,
        RCCC: varRCCC,
        HBV: varHBV,
        PP: varPrePrep,
        WTSP: varWTSP,
        IWTSPN: varIWTSPN,
        IWTSPY: varIWTSPY,
        PA: varPA,
        PILC: $("#txtPillCount").val(),
        WPFS: varWPFS,
        PEPS: varPEPS,
        PR: varPR,
        PRDR: varPRDR,
        CI: varCI,
        ACD: varACD,
        PPT: varPPT,
        WP: $("#txtRemindersWorkplan").val()
        , LRINI: tableArr
        , NAPPT: arrNxtAppointment
    });
    return rowData[0];
}
function PostPrEPInitiationDataData(rowData, isPartial, tabName) {
    debugger;
    var action = "saveinitiation";
    var rowData1 = rowData;
    var errorMsg = ''; //CheckMXBlankValues();
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
                debugger;
                var responseSuccess = response.Success;
                var msg = response.EM;
                if (responseSuccess == "true") {
                    customAlert("PrEP Initiation  " + dataSuccessMessage.toLowerCase());
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
        });
    }
}
//
function ReInitialiseDatatable(from, response) {
    //debugger;
    //
    if (from == 'LabR') {
        //alert('x');
        $('#dtlLabResults').DataTable({
            "aaData": response.LR,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": true,
            "aoColumns": [
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["LTID"];
                    }
                },
                { bSortable: false,
                    mRender: function (data, type, full) {
                        return full["STN"];
                    }
                },
                 { bSortable: false,
                     mRender: function (data, type, full) {
                         return full["RRD"];
                     }
                 },
                 { bSortable: false,
                     mRender: function (data, type, full) {
                         return full["TR"];
                     }
                 },
                 { bSortable: false,
                     mRender: function (data, type, full) {
                         //return full["TRD"];
                         return "<input id=\"cbOrderToday\" type=\"checkbox\" " + full["OTS"] + " />";
                     }
                 }
            ]
        });
    }
    //alert(response.LabR.length);
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
    //    else {

    //        dtDate = GetDateData("dtDrugRefillAppointmentDate", true);
    //        ddlP = $("#ddlDRAPurpose").select2("val");
    //        data = $("#ddlDRAPurpose").select2('data')[0];
    //        desc = $("#txtDRADescription").val();
    //        ctrlDT = "dtDrugRefillAppointmentDate";
    //    }



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
    //    else {
    //        if ($("#dtDrugRefillAppointmentDate").val().length == 0) {
    //            customAlert("Please enter appointment date");
    //            return;
    //        }
    //    }
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

    $('#txtNADDescription').val('');
    $('#dtNextAppointmentDate').val('');
    $("#ddlPurpose").select2("val", "0");
    $("#ddlPurpose").trigger('change.select2');
    //$('#ddlPurpose').get(0).selectedIndex = 0; 

}


//=========================================================
function HIVTestResultsChange(HIVTestResult, urDiv) {
    var codeID = document.getElementById(HIVTestResult).value;
    if (codeID == 555) {
        document.getElementById(urDiv).style.display = "block";
    }
    else {
        document.getElementById(urDiv).style.display = "none";
    }
}
function WillingToStartPrEPChange(ctrlID, urDivNo, urDivYes) {
    var valYesNo = document.getElementById(ctrlID).checked;
    if (valYesNo == true) {
        document.getElementById(urDivYes).style.display = "block";
        document.getElementById(urDivNo).style.display = "none";
    }
    else {
        document.getElementById(urDivYes).style.display = "none";
        document.getElementById(urDivNo).style.display = "block";
    }
}
//3
function WillingToStartPrEPChangeNo(ctrlID1, ctrlID2, ctrlID3, urDivY, urDivNot) {
    var valYesNo1 = document.getElementById(ctrlID1).checked;
    var valYesNo2 = document.getElementById(ctrlID2).checked;
    var valYesNo3 = document.getElementById(ctrlID3).checked;
    if (valYesNo1 == false && valYesNo2 == false && valYesNo2 == false) {
        document.getElementById(urDivY).style.display = "block";
        document.getElementById(urDivNot).style.display = "none";
    }
    else {
        document.getElementById(urDivY).style.display = "none";
        document.getElementById(urDivNot).style.display = "block";
    }
}
//4
function WhyPoor(ctrlID, urDivID) {
    var strName = $("#ddlSelfAssessment option:selected").text();
    if (strName != "") {
        if (strName == 'Fair' || strName == 'Poor') {
            document.getElementById(urDivID).style.display = "block";
        }
        else {
            document.getElementById(urDivID).style.display = "none";
        }
    }
}
//5
function PrEPStatus(ctrlID, urDivIDReason, urDivIDRegimen) {
    var strName = $("#ddlPrepStatus option:selected").text();
    if (strName != "") {
        if (strName == 'Restart treatment' || strName == 'Start new treatment (naïve patient)') {
            document.getElementById(urDivIDReason).style.display = "none";
            document.getElementById(urDivIDRegimen).style.display = "block";
        }
        else if (strName == 'Stop treatment') {
            document.getElementById(urDivIDReason).style.display = "block";
            document.getElementById(urDivIDRegimen).style.display = "none";
        }
        else {
            document.getElementById(urDivIDReason).style.display = "none";
            document.getElementById(urDivIDRegimen).style.display = "none";
        }
    }
}