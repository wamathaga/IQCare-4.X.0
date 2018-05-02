$(document).ready(function () {
    $("#hidId").val(0);

    $("#PQ1").bootstrapSwitch();
    $("#PQ2").bootstrapSwitch();
    $("#PQ3").bootstrapSwitch();
    $("#PQ4").bootstrapSwitch();
    $("#PQ5").bootstrapSwitch();
    $("#PQ6").bootstrapSwitch();
    $("#PQ7").bootstrapSwitch();
    $("#PQ8").bootstrapSwitch();
    $("#PQ9").bootstrapSwitch();
    $("#PQ10").bootstrapSwitch();
    $("#SQ1").bootstrapSwitch();
    $("#SQ2").bootstrapSwitch();
    $("#SQ3").bootstrapSwitch();
    $("#SQ4").bootstrapSwitch();
    $("#SQ5").bootstrapSwitch();

    $('#chkAllAdolescentService').on('ifChecked', function (event) {
        chkAllAdolescentService(true);
    });
    $('#chkAllAdolescentService').on('ifUnchecked', function (event) {
        chkAllAdolescentService(false);
    });

    $('#chkAllCriteria').on('ifChecked', function (event) {
        chkAllSystemsCriteria(true);
    });
    $('#chkAllCriteria').on('ifUnchecked', function (event) {
        chkAllSystemsCriteria(false);
    });


    //Flat red color scheme for iCheck
    $("#chkAllAdolescentService").iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });
    $("#chkAllCriteria").iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });


    GetData();
});

function GetData() {

    //debugger;
    var postData;
    $.ajax({
        type: "POST",
        url: "ARTReadinessAssessment.aspx?data=gettp",
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

function BindData(response) {
    //console.log(JSON.stringify(response.HIVIT));
    $("#hidId").val(response.Id);


    $("#PQ1").bootstrapSwitch('state', ConvertValueBool(response.PQ1));
    $("#PQ2").bootstrapSwitch('state', ConvertValueBool(response.PQ2));
    $("#PQ3").bootstrapSwitch('state', ConvertValueBool(response.PQ3));
    $("#PQ4").bootstrapSwitch('state', ConvertValueBool(response.PQ4));
    $("#PQ5").bootstrapSwitch('state', ConvertValueBool(response.PQ5));
    $("#PQ6").bootstrapSwitch('state', ConvertValueBool(response.PQ6));
    $("#PQ7").bootstrapSwitch('state', ConvertValueBool(response.PQ7));
    $("#PQ8").bootstrapSwitch('state', ConvertValueBool(response.PQ8));
    $("#PQ9").bootstrapSwitch('state', ConvertValueBool(response.PQ9));
    $("#PQ10").bootstrapSwitch('state', ConvertValueBool(response.PQ10));
    $("#SQ1").bootstrapSwitch('state', ConvertValueBool(response.SQ1));
    $("#SQ2").bootstrapSwitch('state', ConvertValueBool(response.SQ2));
    $("#SQ3").bootstrapSwitch('state', ConvertValueBool(response.SQ3));
    $("#SQ4").bootstrapSwitch('state', ConvertValueBool(response.SQ4));
    $("#SQ5").bootstrapSwitch('state', ConvertValueBool(response.SQ5));
    console.log();
}

function GetSwitchValue(ctrlName) {
    var ctrlVal = $("#" + ctrlName).bootstrapSwitch('state');
    if (ctrlVal)
        ctrlVal = 1;
    else
        ctrlVal = 0;
    return ctrlVal;
}

function chkAllSystemsCriteria(blnValue) {
    $("#SQ1").bootstrapSwitch('state', blnValue);
    $("#SQ2").bootstrapSwitch('state', blnValue);
    $("#SQ3").bootstrapSwitch('state', blnValue);
    $("#SQ4").bootstrapSwitch('state', blnValue);
    $("#SQ5").bootstrapSwitch('state', blnValue);
}

function chkAllAdolescentService(blnValue) {
    $("#PQ1").bootstrapSwitch('state', blnValue);
    $("#PQ2").bootstrapSwitch('state', blnValue);
    $("#PQ3").bootstrapSwitch('state', blnValue);
    $("#PQ4").bootstrapSwitch('state', blnValue);
    $("#PQ5").bootstrapSwitch('state', blnValue);
    $("#PQ6").bootstrapSwitch('state', blnValue);
    $("#PQ7").bootstrapSwitch('state', blnValue);
    $("#PQ8").bootstrapSwitch('state', blnValue);
    $("#PQ9").bootstrapSwitch('state', blnValue);
    $("#PQ10").bootstrapSwitch('state', blnValue);

}


function SaveData() {
    //customAlert("AddUser");
    rowData = [];
    //var x = convertToJSONDate($("#dtHIVIT").val());
    var id = $("#hidId").val();
    rowData.push({ Id: id,
        Ptn_Pk: 0,
        VId: 0,
        PQ1: GetSwitchValue("PQ1"),
        PQ2: GetSwitchValue("PQ2"),
        PQ3: GetSwitchValue("PQ3"),
        PQ4: GetSwitchValue("PQ4"),
        PQ5: GetSwitchValue("PQ5"),
        PQ6: GetSwitchValue("PQ6"),
        PQ7: GetSwitchValue("PQ7"),
        PQ8: GetSwitchValue("PQ8"),
        PQ9: GetSwitchValue("PQ9"),
        PQ10: GetSwitchValue("PQ10"),
        SQ1: GetSwitchValue("SQ1"),
        SQ2: GetSwitchValue("SQ2"),
        SQ3: GetSwitchValue("SQ3"),
        SQ4: GetSwitchValue("SQ4"),
        SQ5: GetSwitchValue("SQ5")
    });


    PostData(rowData);
}

function PostData(rowData) {

    var action = "save";
    var rowData1 = rowData[0];

    var responseObject = null;
    $.ajax({
        type: "POST",
        url: "ARTReadinessAssessment.aspx?data=" + action,
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
            var msg = response.EM;
            if (responseSuccess == "true") {
                customAlert("ART readiness assessment " + dataSuccessMessage.toLowerCase());
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
    $("#PQ1").bootstrapSwitch('state', false);
    $("#PQ2").bootstrapSwitch('state', false);
    $("#PQ3").bootstrapSwitch('state', false);
    $("#PQ4").bootstrapSwitch('state', false);
    $("#PQ5").bootstrapSwitch('state', false);
    $("#PQ6").bootstrapSwitch('state', false);
    $("#PQ7").bootstrapSwitch('state', false);
    $("#PQ8").bootstrapSwitch('state', false);
    $("#PQ9").bootstrapSwitch('state', false);
    $("#PQ10").bootstrapSwitch('state', false);
    $("#SQ1").bootstrapSwitch('state', false);
    $("#SQ2").bootstrapSwitch('state', false);
    $("#SQ3").bootstrapSwitch('state', false);
    $("#SQ4").bootstrapSwitch('state', false);
    $("#SQ5").bootstrapSwitch('state', false);

    $("#chkAllAdolescentService").iCheck('uncheck');
    $("#chkAllCriteria").iCheck('uncheck');

    GetData();
}

