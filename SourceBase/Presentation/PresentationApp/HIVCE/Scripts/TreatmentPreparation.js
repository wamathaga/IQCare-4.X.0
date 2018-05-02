$(document).ready(function () {
    $.hivce.loader('show');
    $("#hidId").val(0);
    //Date picker
    $("#dtHIVIT").datepicker({
        autoclose: true
    });
    $("#dtHIVR").datepicker({
        autoclose: true
    });
    $("#dtVLIT").datepicker({
        autoclose: true
    });
    $("#dtVLR").datepicker({
        autoclose: true
    });
    $("#dtCDIT").datepicker({
        autoclose: true
    });
    $("#dtCDR").datepicker({
        autoclose: true
    });
    $("#dtATIT").datepicker({
        autoclose: true
    });
    $("#dtATR").datepicker({
        autoclose: true
    });
    $("#dtTFIT").datepicker({
        autoclose: true
    });
    $("#dtTFR").datepicker({
        autoclose: true
    });
    $("#dtARTIT").datepicker({
        autoclose: true
    });
    $("#dtARTR").datepicker({
        autoclose: true
    });
    $("#dtAIT").datepicker({
        autoclose: true
    });
    $("#dtAIR").datepicker({
        autoclose: true
    });
    $("#dtOMIT").datepicker({
        autoclose: true
    });
    $("#dtOMR").datepicker({
        autoclose: true
    });
    $("#dtNIT").datepicker({
        autoclose: true
    });
    $("#dtNR").datepicker({
        autoclose: true
    });
    $("#dtFUIT").datepicker({
        autoclose: true
    });
    $("#dtFUR").datepicker({
        autoclose: true
    });
    $("#dtMPIT").datepicker({
        autoclose: true
    });
    $("#dtMPR").datepicker({
        autoclose: true
    });

    $("textarea").maxlength({
        alwaysShow: false,
        threshold: 10,
        warningClass: "labelTextBox label label-success",
        limitReachedClass: "labelTextBox label label-danger",
        separator: ' out of ',
        preText: 'You write ',
        postText: ' characters.',
        validate: true,
        placement: 'top'
    });
    
    $("[name='switch-size-Art']").bootstrapSwitch();
    GetData();

});

function GetData() {

    //debugger;
    var postData;
    $.ajax({
        type: "POST",
        url: "TreatmentPreparation.aspx?data=gettp",
        //data: JSON.stringify(nodeId),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
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
                BindData(response);
            }
        },
        error: function (msg) {
        }
    });
}

function BindData(response) {
    //console.log(JSON.stringify(response.HIVIT));
    $("#hidId").val(response.Id);
    CheckDatenAssign(response.HIVIT, "dtHIVIT", true);

    CheckDatenAssign(response.HIVR, "dtHIVR", false);
    CheckDatenAssign(response.VLIT, "dtVLIT", true);
    CheckDatenAssign(response.VLR, "dtVLR", false);
    CheckDatenAssign(response.CD4IT, "dtCDIT", true);
    CheckDatenAssign(response.CD4R, "dtCDR", false);
    CheckDatenAssign(response.ANIT, "dtATIT", true);
    CheckDatenAssign(response.ANR, "dtATR", false);
    CheckDatenAssign(response.TFIT, "dtTFIT", true);
    CheckDatenAssign(response.TFR, "dtTFR", false);
    CheckDatenAssign(response.ARTSEIT, "dtARTIT", true);
    CheckDatenAssign(response.ARTSER, "dtARTR", false);
    CheckDatenAssign(response.ADIT, "dtAIT", true);
    CheckDatenAssign(response.ADR, "dtAIR", false);
    CheckDatenAssign(response.OMIT, "dtOMIT", true);
    CheckDatenAssign(response.OMR, "dtOMR", false);
    CheckDatenAssign(response.NIT, "dtNIT", true);
    CheckDatenAssign(response.NR, "dtNR", false);
    CheckDatenAssign(response.FUIT, "dtFUIT", true);
    CheckDatenAssign(response.FUR, "dtFUR", false);
    CheckDatenAssign(response.MPIT, "dtMPIT", true);
    CheckDatenAssign(response.MPR, "dtMPR", false);
    var blnART = false;
    if (response.ARA == 1) {
        blnART = true;
    }

    $("[name='switch-size-Art']").bootstrapSwitch('state', blnART);

    $("#txtNotes").val(response.Notes);

    $.hivce.loader('hide');

}

function CheckDatenAssign(dtVal, ctrl, IsDisableCheck) {
    var parsedDate;
    var jsDate;
    if (IsDisableCheck) {
        if (dtVal != null) {
            parsedDate = new Date(parseInt(dtVal.substr(6)));
            jsDate = new Date(parsedDate); //Date object
            //console.log(jsDate);
            $("#" + ctrl).datepicker('setDate', jsDate);
            $("#" + ctrl).prop('disabled', true);
        }
        else {
            $("#" + ctrl).prop('disabled', false);
        }
    }
    else {
        if (dtVal != null) {
            parsedDate = new Date(parseInt(dtVal.substr(6)));
            jsDate = new Date(parsedDate); //Date object
            //console.log(jsDate);
            $("#" + ctrl).datepicker('setDate', jsDate);
        }
    }
}

function convertToJSONDate(strDate) {
    var dt = new Date(strDate);
    var newDate = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));
    return '/Date(' + newDate.getTime() + ')/';
}

function GetDateData(ctrl) {
    var valCtrl = $("#" + ctrl).val();
    if (valCtrl.length > 0) {
        return convertToJSONDate($("#" + ctrl).datepicker('getDate'));
    }
    else {
        /*var dt = new Date(1900, 1, 1);
        var newDate = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));
        return '/Date(' + newDate.getTime() + ')/';*/
        return $("#" + ctrl).datepicker('getDate');
    }
}


function SaveData() {
    //customAlert("AddUser");
    rowData = [];
    $.hivce.loader('show');
    //var x = convertToJSONDate($("#dtHIVIT").val());
    var id = $("#hidId").val();
    var ARTreadinessassessment = $("[name='switch-size-Art']").bootstrapSwitch('state');
    if (ARTreadinessassessment)
        ARTreadinessassessment = 1;
    else
        ARTreadinessassessment = 0;

    rowData.push({ Id: id,
        Ptn_Pk: 0,
        HIVIT: GetDateData("dtHIVIT"),
        HIVR: GetDateData("dtHIVR"),
        VLIT: GetDateData("dtVLIT"),
        VLR: GetDateData("dtVLR"),
        CD4IT: GetDateData("dtCDIT"),
        CD4R: GetDateData("dtCDR"),
        ANIT: GetDateData("dtATIT"),
        ANR: GetDateData("dtATR"),
        TFIT: GetDateData("dtTFIT"),
        TFR: GetDateData("dtTFR"),
        ARTSEIT: GetDateData("dtARTIT"),
        ARTSER: GetDateData("dtARTR"),
        ADIT: GetDateData("dtAIT"),
        ADR: GetDateData("dtAIR"),
        OMIT: GetDateData("dtOMIT"),
        OMR: GetDateData("dtOMR"),
        NIT: GetDateData("dtNIT"),
        NR: GetDateData("dtNR"),
        FUIT: GetDateData("dtFUIT"),
        FUR: GetDateData("dtFUR"),
        ARA: ARTreadinessassessment,
        MPIT: GetDateData("dtMPIT"),
        MPR: GetDateData("dtMPR"),
        Notes: $("#txtNotes").val()
    });


    PostData(rowData);
}

function PostData(rowData) {

    var action = "save";
    var rowData1 = rowData[0];

    var responseObject = null;
    $.ajax({
        type: "POST",
        url: "TreatmentPreparation.aspx?data=" + action,
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
                customAlert("Treatment preparation " + dataSuccessMessage.toLowerCase());
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

function ResetData() {
    $("#dtHIVIT").datepicker('setDate', "");
    $("#dtHIVR").datepicker('setDate', "");
    $("#dtVLIT").datepicker('setDate', "");
    $("#dtVLR").datepicker('setDate', "");
    $("#dtCDIT").datepicker('setDate', "");
    $("#dtCDR").datepicker('setDate', "");
    $("#dtATIT").datepicker('setDate', "");
    $("#dtATR").datepicker('setDate', "");
    $("#dtTFIT").datepicker('setDate', "");
    $("#dtTFR").datepicker('setDate', "");
    $("#dtARTIT").datepicker('setDate', "");
    $("#dtARTR").datepicker('setDate', "");
    $("#dtAIT").datepicker('setDate', "");
    $("#dtAIR").datepicker('setDate', "");
    $("#dtOMIT").datepicker('setDate', "");
    $("#dtOMR").datepicker('setDate', "");
    $("#dtNIT").datepicker('setDate', "");
    $("#dtNR").datepicker('setDate', "");
    $("#dtFUIT").datepicker('setDate', "");
    $("#dtFUR").datepicker('setDate', "");
    $("#dtMPIT").datepicker('setDate', "");
    $("#dtMPR").datepicker('setDate', "");
    $("[name='switch-size-Art']").bootstrapSwitch('state', false);
    $("#txtNotes").val("");
    GetData();
}