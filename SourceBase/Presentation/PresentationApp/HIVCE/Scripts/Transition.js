$(document).ready(function () {
    $("#hidId").val(0);

    //Switch 
    $("#G1Q1").bootstrapSwitch();
    $("#G1Q2").bootstrapSwitch();
    $("#G1Q3").bootstrapSwitch();
    $("#G1Q4").bootstrapSwitch();
    $("#G2Q1").bootstrapSwitch();
    $("#G2Q2").bootstrapSwitch();
    $("#G2Q3").bootstrapSwitch();
    $("#G3Q1").bootstrapSwitch();
    $("#G3Q2").bootstrapSwitch();
    $("#G4Q1").bootstrapSwitch();
    $("#G4Q2").bootstrapSwitch();
    $("#G4Q3").bootstrapSwitch();
    $("#G4Q4").bootstrapSwitch();

    //Date picker
    $("#dtG2Q1").datepicker({
        autoclose: true
    });
    $("#dtG2Q2").datepicker({
        autoclose: true
    });
    $("#dtG2Q3").datepicker({
        autoclose: true
    });
    $("#dtG3Q1").datepicker({
        autoclose: true
    });
    $("#dtG3Q2").datepicker({
        autoclose: true
    });
    $("#dtG4Q1").datepicker({
        autoclose: true
    });
    $("#dtG4Q2").datepicker({
        autoclose: true
    });
    $("#dtG4Q3").datepicker({
        autoclose: true
    });
    $("#dtG4Q4").datepicker({
        autoclose: true
    });

    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDate(this);
    });

    GetData();
});

function DisableDate(ctrlName) {
    var ctrlVal = $("#" + ctrlName.id).bootstrapSwitch('state');
    //alert(ctrlName.id.toLowerCase().indexOf("g1"));
    if (ctrlName.id.toLowerCase().indexOf("g1") == -1) {
        var blnVal = true;
        if (ctrlVal)
            blnVal = false;
        $("#dt" + ctrlName.id).prop('disabled', blnVal);
    }
}

function GetData() {

    //debugger;
    var postData;
    $.ajax({
        type: "POST",
        url: "Transition.aspx?data=gettp",
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

function ConvertValueBool(val) {
    var blnVal = false;
    if (val == 1) {
        blnVal = true;
    }
    return blnVal;
}

function CheckDatenAssign(dtVal, ctrl, IsDisableCheck) {
    var parsedDate;
    var jsDate;
    if (dtVal != null) {
        parsedDate = new Date(parseInt(dtVal.substr(6)));
        jsDate = new Date(parsedDate); //Date object
        //console.log(jsDate);
        $("#" + ctrl).datepicker('setDate', jsDate);
    }
}

function BindData(response) {
    //console.log(JSON.stringify(response.HIVIT));
    $("#hidId").val(response.Id);

    $("#G1Q1").bootstrapSwitch('state', ConvertValueBool(response.G1Q1));
    $("#G1Q2").bootstrapSwitch('state', ConvertValueBool(response.G1Q2));
    $("#G1Q3").bootstrapSwitch('state', ConvertValueBool(response.G1Q3));
    $("#G1Q4").bootstrapSwitch('state', ConvertValueBool(response.G1Q4));
    $("#G2Q1").bootstrapSwitch('state', ConvertValueBool(response.G2Q1));
    $("#G2Q2").bootstrapSwitch('state', ConvertValueBool(response.G2Q2));
    $("#G2Q3").bootstrapSwitch('state', ConvertValueBool(response.G2Q3));
    $("#G3Q1").bootstrapSwitch('state', ConvertValueBool(response.G3Q1));
    $("#G3Q2").bootstrapSwitch('state', ConvertValueBool(response.G3Q2));
    $("#G4Q1").bootstrapSwitch('state', ConvertValueBool(response.G4Q1));
    $("#G4Q2").bootstrapSwitch('state', ConvertValueBool(response.G4Q2));
    $("#G4Q3").bootstrapSwitch('state', ConvertValueBool(response.G4Q3));
    $("#G4Q4").bootstrapSwitch('state', ConvertValueBool(response.G4Q4));

    CheckDatenAssign(response.G2Q1D, "dtG2Q1", false);
    CheckDatenAssign(response.G2Q2D, "dtG2Q2", false);
    CheckDatenAssign(response.G2Q3D, "dtG2Q3", false);
    CheckDatenAssign(response.G3Q1D, "dtG3Q1", false);
    CheckDatenAssign(response.G3Q2D, "dtG3Q2", false);
    CheckDatenAssign(response.G4Q1D, "dtG4Q1", false);
    CheckDatenAssign(response.G4Q2D, "dtG4Q2", false);
    CheckDatenAssign(response.G4Q3D, "dtG4Q3", false);
    CheckDatenAssign(response.G4Q4D, "dtG4Q4", false);

    console.log();
    $("#divG1").css("visibility", "hidden");
    $("#divG2").css("visibility", "hidden");
    $("#divG3").css("visibility", "hidden");
    $("#divG4").css("visibility", "hidden");
    var dob = $("#hidDOB").val();
    if (dob <= 12) {
        $("#divG1").css("visibility", "visible");
        $("#divG2").css("visibility", "hidden");
        $("#divG3").css("visibility", "hidden");
        $("#divG4").css("visibility", "hidden");
        $("#divG2").css("display", "none");
        $("#divG3").css("display", "none");
        $("#divG4").css("display", "none");
    }
    else if (dob <= 16) {
        $("#divG1").css("visibility", "visible");
        $("#divG2").css("visibility", "visible");
        $("#divG3").css("visibility", "hidden");
        $("#divG4").css("visibility", "hidden");
        $("#divG3").css("display", "none");
        $("#divG4").css("display", "none");
    }
    else if (dob <= 19) {
        $("#divG1").css("visibility", "visible");
        $("#divG2").css("visibility", "visible");
        $("#divG3").css("visibility", "visible");
        $("#divG4").css("visibility", "hidden");
        $("#divG4").css("display", "none");
    }
    else if (dob > 19) {
        $("#divG1").css("visibility", "visible");
        $("#divG2").css("visibility", "visible");
        $("#divG3").css("visibility", "visible");
        $("#divG4").css("visibility", "visible");
    }
    else {
        $("#divG1").css("visibility", "visible");
        $("#divG2").css("visibility", "hidden");
        $("#divG3").css("visibility", "hidden");
        $("#divG4").css("visibility", "hidden");
        $("#divG2").css("display", "none");
        $("#divG3").css("display", "none");
        $("#divG4").css("display", "none");
    }

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

function convertToJSONDate(strDate) {
    var dt = new Date(strDate);
    var newDate = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));
    return '/Date(' + newDate.getTime() + ')/';
}

function GetSwitchValue(ctrlName) {
    var ctrlVal = $("#" + ctrlName).bootstrapSwitch('state');
    if (ctrlVal)
        ctrlVal = 1;
    else
        ctrlVal = 0;
    return ctrlVal;
}

function SaveData() {
    //customAlert("AddUser");
    rowData = [];
    //var x = convertToJSONDate($("#dtHIVIT").val());
    var id = $("#hidId").val();
    rowData.push({ Id: id,
        Ptn_Pk: 0,
        VId: 0,
        G1Q1: GetSwitchValue("G1Q1"),
        G1Q2: GetSwitchValue("G1Q2"),
        G1Q3: GetSwitchValue("G1Q3"),
        G1Q4: GetSwitchValue("G1Q4"),
        G2Q1: GetSwitchValue("G2Q1"),
        G2Q1D: GetDateData("dtG2Q1"),
        G2Q2: GetSwitchValue("G2Q2"),
        G2Q2D: GetDateData("dtG2Q2"),
        G2Q3: GetSwitchValue("G2Q3"),
        G2Q3D: GetDateData("dtG2Q3"),
        G3Q1: GetSwitchValue("G3Q1"),
        G3Q1D: GetDateData("dtG3Q1"),
        G3Q2: GetSwitchValue("G3Q2"),
        G3Q2D: GetDateData("dtG3Q2"),
        G4Q1: GetSwitchValue("G4Q1"),
        G4Q1D: GetDateData("dtG4Q1"),
        G4Q2: GetSwitchValue("G4Q2"),
        G4Q2D: GetDateData("dtG4Q2"),
        G4Q3: GetSwitchValue("G4Q3"),
        G4Q3D: GetDateData("dtG4Q3"),
        G4Q4: GetSwitchValue("G4Q4"),
        G4Q4D: GetDateData("dtG4Q4")
    });


    PostData(rowData);
}

function PostData(rowData) {

    var action = "save";
    var rowData1 = rowData[0];

    var responseObject = null;
    $.ajax({
        type: "POST",
        url: "Transition.aspx?data=" + action,
        data: JSON.stringify(rowData1),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        beforeSend: function () {
        },
        success: function (response) {
            debugger;
            //console.log(response);
            var responseSuccess = response.Success;
            var msg = response.EM;
            if (responseSuccess == "true") {
                customAlert("Transition " + dataSuccessMessage.toLowerCase());
            }
            else {
                if (responseSuccess == "false") {
                    if (msg.toLowerCase().indexOf("visit already exists") >= 0) {
                        //string contains hello
                        customAlert(msg);
                    }
                    else {
                        customAlert(errorMessage);
                    }
                }
                else {
                    customAlert(res);
                }
                //                if (responseSuccess == "false") {
                //                    customAlert(errorMessage);
                //                }
                //                else {
                //                    customAlert(responseSuccess);
                //                }
            }

            //$.eva.loader('hide');
        },
        error: function (msg) {
            //ShowErrorMessage(msg);
        }
    });
}


function ResetData() {

    $("#G1Q1").bootstrapSwitch('state', false);
    $("#G1Q2").bootstrapSwitch('state', false);
    $("#G1Q3").bootstrapSwitch('state', false);
    $("#G1Q4").bootstrapSwitch('state', false);
    $("#G2Q1").bootstrapSwitch('state', false);
    $("#G2Q2").bootstrapSwitch('state', false);
    $("#G2Q3").bootstrapSwitch('state', false);
    $("#G3Q1").bootstrapSwitch('state', false);
    $("#G3Q2").bootstrapSwitch('state', false);
    $("#G4Q1").bootstrapSwitch('state', false);
    $("#G4Q2").bootstrapSwitch('state', false);
    $("#G4Q3").bootstrapSwitch('state', false);
    $("#G4Q4").bootstrapSwitch('state', false);

    $("#dtG2Q1").datepicker('setDate', "");
    $("#dtG2Q2").datepicker('setDate', "");
    $("#dtG2Q3").datepicker('setDate', "");
    $("#dtG3Q1").datepicker('setDate', "");
    $("#dtG3Q2").datepicker('setDate', "");
    $("#dtG4Q1").datepicker('setDate', "");
    $("#dtG4Q2").datepicker('setDate', "");
    $("#dtG4Q3").datepicker('setDate', "");
    $("#dtG4Q4").datepicker('setDate', "");

    GetData();
}


