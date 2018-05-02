/*****Validation*****/
function isNullOrWhiteSpace(str) {
    return str === null || str.match(/^ *$/) !== null;
}
/*********/

function ltrim(s) {
    var l = 0;
    while (l < s.length && s[l] == ' ')
    { l++; }
    return s.substring(l, s.length);
}


$.hivce = {
    loader: function (req) {
        if (req == "show") {
            $('#overeffect, #blockpage').remove();
            $(document.body).append('<div id="blockpage">Working...</div><div id="overeffect"></div>');
            $('#overeffect').show().css('height', $(document).height() + 'px');
            $('#blockpage').show()
        }
        else {
            $('#overeffect, #blockpage').remove();
        }
    }
}

if (typeof (String.prototype.trim) != 'function') {
    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, '');
    }
}

var JSON = JSON || {};
// implement JSON.stringify serialization
JSON.stringify = JSON.stringify || function (obj) {
    var t = typeof (obj);
    if (t != "object" || obj === null) {
        // simple data type
        if (t == "string")
            obj = '"' + obj + '"';
        return String(obj);
    } else {
        // recurse array or object
        var n, v, json = [], arr = (obj && obj.constructor == Array);
        for (n in obj) {
            v = obj[n];
            t = typeof (v);
            if (t == "string")
                v = '"' + v + '"';
            else if (t == "object" && v !== null)
                v = JSON.stringify(v);
            json.push((arr ? "" : '"' + n + '":') + String(v));
        }
        return (arr ? "[" : "{") + String(json) + (arr ? "]" : "}");
    }
};



function ShowErrorMessage(msg) {
    customAlert('Error has been occured !!. Please try after some time. [Error Code:' + msg.status + ']');

    if (msg.status == 0) {
        //window.location.href = 'Timeout.aspx?message=Error has been occured !!. Please try after some time. [Error Code:' + msg.status + ']';
    } else if (msg.status == 400) {
        //window.location.href = 'Timeout.aspx?message=Error has been occured !!. Please try after some time. [Error Code:' + msg.status + ']';
    } else if (msg.status == 404) {
        //window.location.href = 'Timeout.aspx?message=Error has been occured !!. Please try after some time. [Error Code:' + msg.status + ']';
    } else if (msg.status == 500) {
        //window.location.href = 'Timeout.aspx?message=Error has been occured !!. Please try after some time. [Error Code:' + msg.status + ']';
    } else if (msg.status == 200) {
        //window.location.href = 'Timeout.aspx?message=Error has been occured !!. Please try after some time. [Error Code:' + msg.status + ']';
    } else {
        customAlert('Error has been occured !!. Please try after some time. [Error Code:' + msg.status + ']');
        //$.modal.close();
    }
}

function GetTodaysDate() {

}


function validateEmail(sEmail) {
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (filter.test(sEmail)) {
        return true;
    }
    else {
        return false;
    }
}

function userApprove(str, options, callback) {
    $.colorbox({ width: "500px", title: "Please Confirm", html: '<div id="customAlert"><div class="msg">' + str + '</div><div class="btn"><a href="#" id="YesButton">OK</a> <a href="#" id="NoButton">Cancel</a></div></div>' });
    $("#YesButton").one("mouseup", function () { $.colorbox.close(); callback(true); });
    $("#NoButton").one("mouseup", function () { $.colorbox.close(); callback(false); });
}

function customConfirm(msg) {
    //userApprove(msg, "YN", function (result) { callback(result); });
    //    bootbox.confirm(msg, function (result) {
    //        return result;
    //    });
    bootbox.confirm(msg, function (e) {
        if (e) {
            return true;
        }
        else {
            return false;
        }
    });
}

function customAlert(msg) {
    $.hivce.loader('hide');
    bootbox.alert(msg, function () {
        console.log("Alert Callback");
    });

    //    $.colorbox(
    //    {
    //        width: "500px",
    //        title: "EVA Alert",
    //        html: '<div id="customAlert"><div class="msg">' + msg + '</div><div class="btn"><a href="#" onclick="$.colorbox.close()">OK</a></div></div>'
    //    });
}


var dateFormat = function (d) {
    var month = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var date = d.getDay() + " " + month[d.getMonth()] + ", " + d.getFullYear();
    var time = d.toLocaleTimeString().toLowerCase();
    return (date + " at " + time);
} (new Date());


function CloseModal(ctrl, modalName, hidFieldName) {
    var objId = $("#" + hidFieldName).val();

    if (objId == 0) {
        $('#' + modalName).modal('hide');
    }
    else {
        $("#" + ctrl).attr("disabled", "disabled");
        bootbox.confirm(closeModal, function (e) {
            if (e) {
                $('#' + modalName).modal('hide');
            }
            $("#" + ctrl).removeAttr("disabled");
        });
    }
}

function CloseModalWithoutFldChk(ctrl, modalName) {
    $("#" + ctrl).attr("disabled", "disabled");
    bootbox.confirm(closeModal, function (e) {
        if (e) {
            $('#' + modalName).modal('hide');
        }
        $("#" + ctrl).removeAttr("disabled");
    });
}


/*** Get Date Data****/

function convertToJSONDate(strDate, localformat) {
    var dt = new Date(strDate);
    var newDate = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));
    /*if (localformat) {
    return newDate.toLocaleDateString();
    }
    else {*/
    return '/Date(' + newDate.getTime() + ')/';
    //}
}

function GetDateData(ctrl, localformat) {
    var valCtrl = $("#" + ctrl).val();
    if (valCtrl.length > 0) {
        return convertToJSONDate($("#" + ctrl).datepicker('getDate'), localformat);
    }
    else {
        /*var dt = new Date(1900, 1, 1);
        var newDate = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));
        return '/Date(' + newDate.getTime() + ')/';*/
        return $("#" + ctrl).datepicker('getDate');
    }
}

function GetSwitchValue(ctrlName) {
    var ctrlVal = $("#" + ctrlName).bootstrapSwitch('state');
    if (ctrlVal)
        ctrlVal = 1;
    else
        ctrlVal = 0;
    return ctrlVal;
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


function DrawDataTable(ctrlName, arrUI) {

    if (arrUI.length > 0) {
        var table = $("#" + ctrlName).DataTable();
        table.rows().remove().draw();
        table.rows.add(arrUI).draw().nodes();
    }
}

function DisableDiv(ctrlName) {
    var disDiv;
    var IsControlDisable = false;
    var ctrl,ctrl1;
    if (ctrlName.id == "chkTransferIn") {
        disDiv = "divTransferIn";
    }
    else if (ctrlName.id == "chkComplaints") {
        disDiv = "divPresentingComplaints";
    }
    else if (ctrlName.id == "chkChronicDisorders") {
        disDiv = "divChronicDisorders";
    }
    else if (ctrlName.id == "chkLongTermMedication") {
        disDiv = "divLongTermMedication";
    }
    else if (ctrlName.id == "chkPriorART") {
        disDiv = "divPriorART";
    }
    else if (ctrlName.id == "chkDrugAllergies") {
        disDiv = "divDrugAllergies";
    }
    else if (ctrlName.id == "chkHIVFamilyMember") {
        disDiv = "divHIVFamilyMember";
    }
    else if (ctrlName.id == "chkSexuallyActive6Months") {
        disDiv = "divSexuallyActive6Months";
    }
    /*Systemic Review*/
    else if (ctrlName.id == "chkSkin") {
        disDiv = "divSkin";
    }
    else if (ctrlName.id == "chkENT") {
        disDiv = "divENT";
    }
    else if (ctrlName.id == "chkChest") {
        disDiv = "divChest";
    }
    else if (ctrlName.id == "chkCVS") {
        disDiv = "divCVS";
    }
    else if (ctrlName.id == "chkAbdomen") {
        disDiv = "divAbdomen";
    }
    else if (ctrlName.id == "chkCNS") {
        disDiv = "divCNS";
    }
    else if (ctrlName.id == "chkGUSystem") {
        disDiv = "divGUSystem";
    }
    /*else if (ctrlName.id == "chkTBContactsScreened") {
        disDiv = "divTBContactScreenedDetails";
    }*/
    else if (ctrlName.id == "chkAlternativeMedicine") {
        ctrl = "txtAlternativeMedicine";
        IsControlDisable = true;
    }
    else if (ctrlName.id == "chkARTSideEffect") {
        disDiv = "divARTSideEffect";
    }
    else if (ctrlName.id == "chkANCProfileDone") {
        disDiv = "divANCProfile";
    }
    else if (ctrlName.id == "chkKeyPopulation") {
        disDiv = "divKeyPopulation";
    }
    else if (ctrlName.id == "chkThreapyChange") {
        disDiv = "divThreapyChangeComments";
    }
    else if (ctrlName.id == "chkSupportGroup") {
        ctrl = "txtSupportGroupName";
        IsControlDisable = true;
    }
    else if (ctrlName.id == "chkCareGiver") {
        ctrl = "ddlCareGiven";
        IsControlDisable = true;
    }

    else if (ctrlName.id == "chkDisclosedHIVStatus") {
        ctrl = "ddlDisclosureStatus";
        ctrl1 = "ddlDisclosedTo";
        IsControlDisable = true;
    }
    else if (ctrlName.id == "chkGenitoUrinary") {
        disDiv = "divGEU";
    }

    var ctrlVal = $("#" + ctrlName.id).bootstrapSwitch('state');
    if (IsControlDisable) {
        if (ctrlVal) {
            $("#" + ctrl).prop('disabled', false);
            if (ctrl1 != null) {
                $("#" + ctrl1).prop('disabled', false);
            }
        }
        else {
            $("#" + ctrl).prop('disabled', true);
            if (ctrl1 != null) {
                $("#" + ctrl1).prop('disabled', true);
            }
        }
    }
    else {
        $("#" + disDiv).removeAttr("style");
        if (ctrlVal) {
            $("#" + disDiv).css("visibility", "visible");
            $("#" + disDiv).css("display", "block");
        }
        else {
            $("#" + disDiv).css("visibility", "hidden");
            $("#" + disDiv).css("display", "none");
        }
    }
}

function CalcualteBMIWithoutZScore(hidAge, txtWeight, txtHeight, txtBMI, lblBMIClassification) {
    var age = $("#" + hidAge).val();
    var weight = $("#" + txtWeight).val();
    var height = $("#" + txtHeight).val();
    if (weight == "" || height == "") {
        weight = 0;
        height = 0;
        $("#" + txtBMI).val("");
    }
    else {
        var BMI = weight / ((height / 100) * (height / 100));
        BMI = BMI.toFixed(2);
        if (isNaN(BMI)) {
            BMI = 0;
        }
        $("#" + txtBMI).val(BMI);

        if (age > 15) {
            if (BMI < 18.5) {
                //underweight
                document.getElementById(lblBMIClassification).innerHTML = 'UnderWeight';
                document.getElementById(lblBMIClassification).style.color = "red";
                document.getElementById(txtBMI).style.color = "red";
                document.getElementById(lblBMIClassification).style.fontWeight = 'bold';
            }

            if (BMI >= 18.5 && BMI < 25) {
                document.getElementById(lblBMIClassification).innerHTML = 'Normal';
                document.getElementById(lblBMIClassification).style.color = "black";
                document.getElementById(txtBMI).style.color = "black";
            }

            if (BMI > 25 && BMI < 30) {
                //OverWeight
                document.getElementById(lblBMIClassification).innerHTML = 'OverWeight';
                document.getElementById(lblBMIClassification).style.color = "red";
                document.getElementById(txtBMI).style.color = "red";
                document.getElementById(lblBMIClassification).style.fontWeight = 'bold';
            }

            if (BMI >= 30) {
                //Obese
                document.getElementById(lblBMIClassification).innerHTML = 'Obese';
                document.getElementById(lblBMIClassification).style.color = "red";
                document.getElementById(txtBMI).style.color = "red";
                document.getElementById(lblBMIClassification).style.fontWeight = 'bold';
            }
        }
    }
}

function CalcualteBMI(hidAge, txtWeight, txtHeight, txtBMI, lblBMIClassification) {
    var age = $("#" + hidAge).val();
    var weight = $("#" + txtWeight).val();
    var height = $("#" + txtHeight).val();
    if (weight == "" || height == "") {
        weight = 0;
        height = 0;
        $("#" + txtBMI).val("");
    }
    else {
        var BMI = weight / ((height / 100) * (height / 100));
        BMI = BMI.toFixed(2);
        if (isNaN(BMI)) {
            BMI = 0;
        }
        $("#" + txtBMI).val(BMI);

        if (age > 15) {
            if (BMI < 18.5) {
                //underweight
                document.getElementById(lblBMIClassification).innerHTML = 'UnderWeight';
                document.getElementById(lblBMIClassification).style.color = "red";
                document.getElementById(txtBMI).style.color = "red";
                document.getElementById(lblBMIClassification).style.fontWeight = 'bold';
            }

            if (BMI >= 18.5 && BMI < 25) {
                document.getElementById(lblBMIClassification).innerHTML = 'Normal';
                document.getElementById(lblBMIClassification).style.color = "black";
                document.getElementById(txtBMI).style.color = "black";
            }

            if (BMI > 25 && BMI < 30) {
                //OverWeight
                document.getElementById(lblBMIClassification).innerHTML = 'OverWeight';
                document.getElementById(lblBMIClassification).style.color = "red";
                document.getElementById(txtBMI).style.color = "red";
                document.getElementById(lblBMIClassification).style.fontWeight = 'bold';
            }

            if (BMI >= 30) {
                //Obese
                document.getElementById(lblBMIClassification).innerHTML = 'Obese';
                document.getElementById(lblBMIClassification).style.color = "red";
                document.getElementById(txtBMI).style.color = "red";
                document.getElementById(lblBMIClassification).style.fontWeight = 'bold';
            }
        }
    }

    CalculateWeightZScore();
    CalculateHeightZScore();
    CalculateBMIZScore();
}
function CalculateWeightZScore() {
    if (jQuery.isEmptyObject(zscoreDetails) == false) {
        var txtWeight = $("#txtWeight").val();
        var L = zscoreDetails.WFA.L;
        var M = zscoreDetails.WFA.M;
        var S = zscoreDetails.WFA.S;
        var WeightAgeZ = 0;
        var weight = 0;
        if (txtWeight !== "")
            weight = parseFloat(txtWeight);
        else
            weight = 0;

        //Weight for age calculation
        if (L != 0)
            WeightAgeZ = ((Math.pow((weight / M), L)) - 1) / (S * L);
        else
            WeightAgeZ = (Math.log(weight / M)) / S;


        if (WeightAgeZ >= 4) {
            $("#txtWAgeZScore").val("4");
            document.getElementById("txtWAgeZScore").style.color = "red";
            document.getElementById("lblWAClassification").innerHTML = " Overweight";
            document.getElementById("lblWAClassification").style.color = "red";
        }
        else if (WeightAgeZ >= 3 && WeightAgeZ < 4) {
            $("#txtWAgeZScore").val("3");
            document.getElementById("txtWAgeZScore").style.color = "red";
            document.getElementById("lblWAClassification").innerHTML = " Overweight";
            document.getElementById("lblWAClassification").style.color = "red";
        }
        else if (WeightAgeZ >= 2 && WeightAgeZ < 3) {
            $("#txtWAgeZScore").val("2");
            document.getElementById("txtWAgeZScore").style.color = "red";
            document.getElementById("lblWAClassification").innerHTML = " Overweight";
            document.getElementById("lblWAClassification").style.color = "red"; ;
        }
        else if (WeightAgeZ >= 1 && WeightAgeZ < 2) {
            $("#txtWAgeZScore").val("1");
            document.getElementById("txtWAgeZScore").style.color = "red";
            document.getElementById("lblWAClassification").innerHTML = " Overweight";
            document.getElementById("lblWAClassification").style.color = "red"; ;
        }
        else if (WeightAgeZ > -1 && WeightAgeZ < 1) {
            $("#txtWAgeZScore").val("0");
            document.getElementById("txtWAgeZScore").style.color = "green";
            document.getElementById("lblWAClassification").innerHTML = " Normal";
            document.getElementById("lblWAClassification").style.color = "green";
        }
        else if (WeightAgeZ <= -1 && WeightAgeZ > -2) {
            $("#txtWAgeZScore").val("-1");
            document.getElementById("txtWAgeZScore").style.color = "orange";
            document.getElementById("lblWAClassification").innerHTML = " Mild";
            document.getElementById("lblWAClassification").style.color = "orange";
        }
        else if (WeightAgeZ <= -2 && WeightAgeZ > -3) {
            $("#txtWAgeZScore").val("-2");
            document.getElementById("txtWAgeZScore").style.color = "red";
            document.getElementById("lblWAClassification").innerHTML = " Moderate";
            document.getElementById("lblWAClassification").style.color = "red"; ;
        }
        else if (WeightAgeZ <= -3 && WeightAgeZ > -4) {
            $("#txtWAgeZScore").val("-3");
            document.getElementById("txtWAgeZScore").style.color = "red";
            document.getElementById("lblWAClassification").innerHTML = " Severe";
            document.getElementById("lblWAClassification").style.color = "red"; ;
        }
        else if (WeightAgeZ <= -4) {
            $("#txtWAgeZScore").val("-4");
            document.getElementById("txtWAgeZScore").style.color = "red";
            document.getElementById("lblWAClassification").innerHTML = "Severe";
            document.getElementById("lblWAClassification").style.color = "red"; ;
        }
    }
}

function CalculateHeightZScore() {
    if (jQuery.isEmptyObject(zscoreDetails) == false) {
        var txtHeight = $("#txtHeight").val();
        var L = zscoreDetails.WFH.L;
        var M = zscoreDetails.WFH.M;
        var S = zscoreDetails.WFH.S;
        var WeightHeightZ = 0;
        var height = 0;
        if (txtHeight !== "")
            height = parseFloat(txtHeight);
        else
            height = 0;

        //Weight for age calculation
        if (L != 0)
            WeightHeightZ = ((Math.pow((height / M), L)) - 1) / (S * L);
        else
            WeightHeightZ = (Math.log(height / M)) / S;


        if (WeightHeightZ >= 4) {
            $("#txtHAgeZScore").val("4");
            document.getElementById("txtHAgeZScore").style.color = "red";
            document.getElementById("lblWHClassification").innerHTML = " Overweight";
            document.getElementById("lblWHClassification").style.color = "red";
        }
        else if (WeightHeightZ >= 3 && WeightHeightZ < 4) {
            $("#txtHAgeZScore").val("3");
            document.getElementById("txtHAgeZScore").style.color = "red";
            document.getElementById("lblWHClassification").innerHTML = " Overweight";
            document.getElementById("lblWHClassification").style.color = "red";
        }
        else if (WeightHeightZ >= 2 && WeightHeightZ < 3) {
            $("#txtHAgeZScore").val("2");
            document.getElementById("txtHAgeZScore").style.color = "red";
            document.getElementById("lblWHClassification").innerHTML = " Overweight";
            document.getElementById("lblWHClassification").style.color = "red"; ;
        }
        else if (WeightHeightZ >= 1 && WeightHeightZ < 2) {
            $("#txtHAgeZScore").val("1");
            document.getElementById("txtHAgeZScore").style.color = "red";
            document.getElementById("lblWHClassification").innerHTML = " Overweight";
            document.getElementById("lblWHClassification").style.color = "red"; ;
        }
        else if (WeightHeightZ > -1 && WeightHeightZ < 1) {
            $("#txtHAgeZScore").val("0");
            document.getElementById("txtHAgeZScore").style.color = "green";
            document.getElementById("lblWHClassification").innerHTML = " Normal";
            document.getElementById("lblWHClassification").style.color = "green";
        }
        else if (WeightHeightZ <= -1 && WeightHeightZ > -2) {
            $("#txtHAgeZScore").val("-1");
            document.getElementById("txtHAgeZScore").style.color = "orange";
            document.getElementById("lblWHClassification").innerHTML = " Mild";
            document.getElementById("lblWHClassification").style.color = "orange";
        }
        else if (WeightHeightZ <= -2 && WeightHeightZ > -3) {
            $("#txtHAgeZScore").val("-2");
            document.getElementById("txtHAgeZScore").style.color = "red";
            document.getElementById("lblWHClassification").innerHTML = " Moderate";
            document.getElementById("lblWHClassification").style.color = "red"; ;
        }
        else if (WeightHeightZ <= -3 && WeightHeightZ > -4) {
            $("#txtHAgeZScore").val("-3");
            document.getElementById("txtHAgeZScore").style.color = "red";
            document.getElementById("lblWHClassification").innerHTML = " Severe";
            document.getElementById("lblWHClassification").style.color = "red"; ;
        }
        else if (WeightHeightZ <= -4) {
            $("#txtHAgeZScore").val("-4");
            document.getElementById("txtHAgeZScore").style.color = "red";
            document.getElementById("lblWHClassification").innerHTML = "Severe";
            document.getElementById("lblWHClassification").style.color = "red"; ;
        }
    }
}

function CalculateBMIZScore() {
    if (jQuery.isEmptyObject(zscoreDetails) == false) {
        var txtWeight = $("#txtWeight").val();
        var txtHeight = $("#txtHeight").val();
        var L = zscoreDetails.BMIz.L;
        var M = zscoreDetails.BMIz.M;
        var S = zscoreDetails.BMIz.S;
        var bmi = 0;
        var BMIz = 0;

        if (txtHeight != "" && txtWeight != "")
            bmi = parseFloat(txtWeight) / ((parseFloat(txtHeight) / 100) * (parseFloat(txtHeight) / 100));
        else
            bmi = 0;

        if (L != 0)
            BMIz = ((Math.pow((bmi / M), L)) - 1) / (S * L);
        else
            BMIz = (Math.log(bmi / M)) / S;


        if (BMIz >= 4) {
            $("#txtBMIZScore").val("4");
            document.getElementById("txtBMIZScore").style.color = "red";
            document.getElementById("lblBMIzClassification").innerHTML = " Overweight";
            document.getElementById("lblBMIzClassification").style.color = "red";
        }
        else if (BMIz >= 3 && BMIz < 4) {
            $("#txtBMIZScore").val("3");
            document.getElementById("txtBMIZScore").style.color = "red";
            document.getElementById("lblBMIzClassification").innerHTML = " Overweight";
            document.getElementById("lblBMIzClassification").style.color = "red";
        }
        else if (BMIz >= 2 && BMIz < 3) {
            $("#txtBMIZScore").val("2");
            document.getElementById("txtBMIZScore").style.color = "red";
            document.getElementById("lblBMIzClassification").innerHTML = " Overweight";
            document.getElementById("lblBMIzClassification").style.color = "red"; ;
        }
        else if (BMIz >= 1 && BMIz < 2) {
            $("#txtBMIZScore").val("1");
            document.getElementById("txtBMIZScore").style.color = "red";
            document.getElementById("lblBMIzClassification").innerHTML = " Overweight";
            document.getElementById("lblBMIzClassification").style.color = "red"; ;
        }
        else if (BMIz > -1 && BMIz < 1) {
            $("#txtBMIZScore").val("0");
            document.getElementById("txtBMIZScore").style.color = "green";
            document.getElementById("lblBMIzClassification").innerHTML = " Normal";
            document.getElementById("lblBMIzClassification").style.color = "green";
        }
        else if (BMIz <= -1 && BMIz > -2) {
            $("#txtBMIZScore").val("-1");
            document.getElementById("txtBMIZScore").style.color = "orange";
            document.getElementById("lblBMIzClassification").innerHTML = " Mild";
            document.getElementById("lblBMIzClassification").style.color = "orange";
        }
        else if (BMIz <= -2 && BMIz > -3) {
            $("#txtBMIZScore").val("-2");
            document.getElementById("txtBMIZScore").style.color = "red";
            document.getElementById("lblBMIzClassification").innerHTML = " Moderate";
            document.getElementById("lblBMIzClassification").style.color = "red"; ;
        }
        else if (BMIz <= -3 && BMIz > -4) {
            $("#txtBMIZScore").val("-3");
            document.getElementById("txtBMIZScore").style.color = "red";
            document.getElementById("lblBMIzClassification").innerHTML = " Severe";
            document.getElementById("lblBMIzClassification").style.color = "red"; ;
        }
        else if (BMIz <= -4) {
            $("#txtBMIZScore").val("-4");
            document.getElementById("txtBMIZScore").style.color = "red";
            document.getElementById("lblBMIzClassification").innerHTML = "Severe";
            document.getElementById("lblBMIzClassification").style.color = "red"; ;
        }
    }
}


function ConvertValueBool(val) {
    var blnVal = false;
    if (val == 1) {
        blnVal = true;
    }
    return blnVal;
}

function SortArray(data) {
    var sortData = data.sort(function (a, b) {
        return parseFloat(a.DId) - parseFloat(b.DId);
    });

    return sortData;
}

function getPageName() {
    var url = window.location.pathname;
    var index = url.lastIndexOf("/") + 1;
    var filenameWithExtension = url.substr(index);
    var filename = filenameWithExtension.split(".")[0]; // <-- added this line
    return filename;                                    // <-- added this line
}

function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}

function GetControlMultiSelectValues(ctrlName) {
    var ddlCtrl = $("#" + ctrlName).select2("val");
    var arrCtrl = [];
    if (ddlCtrl !== null) {
        if (ddlCtrl.length > 0) {
            if (jQuery.isEmptyObject(ddlCtrl) == false) {
                $.each(ddlCtrl, function (index, arrD) {
                    if (jQuery.isEmptyObject(arrD) == false) {
                        arrCtrl.push({ id: arrD });
                    }
                });
            }
        }
        else {
            arrCtrl.push({ id: 0 });
        }
    }
    else {
        arrCtrl.push({ id: 0 });
    }

    return arrCtrl;
}

function BindMultiSelectDropDown(ctrlName, arrList) {
    var selectedValues = new Array();
    $.each(arrList, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            if (arrD.id !== 0) {
                selectedValues.push([arrD.id]);
            }
        }
    });

    $("#" + ctrlName).select2().val(selectedValues).trigger("change");

}

function HighLightAbnormalValues(type, ctrlName) {
    var hidPAYM = $("#hidPAYM").val();

    if (hidPAYM >= 15) {
        HighLightAbnormalValuesAdults(type, ctrlName);
    }
    else {
        HighLightAbnormalValuesPeads(type, ctrlName);
    }
}

function HighLightAbnormalValuesAdults(type, ctrlName) {
    //temperature
    if (type == "temp") {
        var tempValue = $("#" + ctrlName).val();
        if (tempValue < 36.1 || tempValue > 37.8) {
            document.getElementById(ctrlName).style.color = "red";
            document.getElementById(ctrlName).style.fontWeight = "bold";
        }
        else {
            document.getElementById(ctrlName).style.color = "black";
            document.getElementById(ctrlName).style.fontWeight = "normal";
        }
    }
}

function HighLightAbnormalValuesPeads(type, ctrlName) {
    //alert(age);
    //var age = '<%= Session["patientageinyearmonth"] %>';
    //temperature
    if (type == "temp") {
        var tempValue = $("#" + ctrlName).val();
        if (tempValue < 36.1 || tempValue > 37.8) {
            document.getElementById(ctrlName).style.color = "red";
            document.getElementById(ctrlName).style.fontWeight = "bold";
        }
        else {
            document.getElementById(ctrlName).style.color = "black";
            document.getElementById(ctrlName).style.fontWeight = "normal";
        }
    }
}


function CalculateWeightZScoreSR() {
    if (jQuery.isEmptyObject(zscoreDetails) == false) {
        var txtWeight = $("#txtSRWeight").val();
        var L = zscoreDetails.WFA.L;
        var M = zscoreDetails.WFA.M;
        var S = zscoreDetails.WFA.S;
        var WeightAgeZ = 0;
        var weight = 0;
        if (txtWeight !== "")
            weight = parseFloat(txtWeight);
        else
            weight = 0;

        //Weight for age calculation
        if (L != 0)
            WeightAgeZ = ((Math.pow((weight / M), L)) - 1) / (S * L);
        else
            WeightAgeZ = (Math.log(weight / M)) / S;


        if (WeightAgeZ >= 4) {
            $("#txtSRWAgeZScore").val("4");
            document.getElementById("txtSRWAgeZScore").style.color = "red";
            document.getElementById("lblSRWAClassification").innerHTML = " Overweight";
            document.getElementById("lblSRWAClassification").style.color = "red";
        }
        else if (WeightAgeZ >= 3 && WeightAgeZ < 4) {
            $("#txtSRWAgeZScore").val("3");
            document.getElementById("txtSRWAgeZScore").style.color = "red";
            document.getElementById("lblSRWAClassification").innerHTML = " Overweight";
            document.getElementById("lblSRWAClassification").style.color = "red";
        }
        else if (WeightAgeZ >= 2 && WeightAgeZ < 3) {
            $("#txtSRWAgeZScore").val("2");
            document.getElementById("txtSRWAgeZScore").style.color = "red";
            document.getElementById("lblSRWAClassification").innerHTML = " Overweight";
            document.getElementById("lblSRWAClassification").style.color = "red"; ;
        }
        else if (WeightAgeZ >= 1 && WeightAgeZ < 2) {
            $("#txtSRWAgeZScore").val("1");
            document.getElementById("txtSRWAgeZScore").style.color = "red";
            document.getElementById("lblSRWAClassification").innerHTML = " Overweight";
            document.getElementById("lblSRWAClassification").style.color = "red"; ;
        }
        else if (WeightAgeZ > -1 && WeightAgeZ < 1) {
            $("#txtSRWAgeZScore").val("0");
            document.getElementById("txtSRWAgeZScore").style.color = "green";
            document.getElementById("lblSRWAClassification").innerHTML = " Normal";
            document.getElementById("lblSRWAClassification").style.color = "green";
        }
        else if (WeightAgeZ <= -1 && WeightAgeZ > -2) {
            $("#txtSRWAgeZScore").val("-1");
            document.getElementById("txtSRWAgeZScore").style.color = "orange";
            document.getElementById("lblSRWAClassification").innerHTML = " Mild";
            document.getElementById("lblSRWAClassification").style.color = "orange";
        }
        else if (WeightAgeZ <= -2 && WeightAgeZ > -3) {
            $("#txtSRWAgeZScore").val("-2");
            document.getElementById("txtSRWAgeZScore").style.color = "red";
            document.getElementById("lblSRWAClassification").innerHTML = " Moderate";
            document.getElementById("lblSRWAClassification").style.color = "red"; ;
        }
        else if (WeightAgeZ <= -3 && WeightAgeZ > -4) {
            $("#txtSRWAgeZScore").val("-3");
            document.getElementById("txtSRWAgeZScore").style.color = "red";
            document.getElementById("lblSRWAClassification").innerHTML = " Severe";
            document.getElementById("lblSRWAClassification").style.color = "red"; ;
        }
        else if (WeightAgeZ <= -4) {
            $("#txtSRWAgeZScore").val("-4");
            document.getElementById("txtSRWAgeZScore").style.color = "red";
            document.getElementById("lblSRWAClassification").innerHTML = "Severe";
            document.getElementById("lblSRWAClassification").style.color = "red"; ;
        }
    }
}

function CalculateHeightZScoreSR() {
    if (jQuery.isEmptyObject(zscoreDetails) == false) {
        var txtHeight = $("#txtSRHeight").val();
        var L = zscoreDetails.WFH.L;
        var M = zscoreDetails.WFH.M;
        var S = zscoreDetails.WFH.S;
        var WeightHeightZ = 0;
        var height = 0;
        if (txtHeight !== "")
            height = parseFloat(txtHeight);
        else
            height = 0;

        //Weight for age calculation
        if (L != 0)
            WeightHeightZ = ((Math.pow((height / M), L)) - 1) / (S * L);
        else
            WeightHeightZ = (Math.log(height / M)) / S;


        if (WeightHeightZ >= 4) {
            $("#txtSRHAgeZScore").val("4");
            document.getElementById("txtSRHAgeZScore").style.color = "red";
            document.getElementById("lblSRWHClassification").innerHTML = " Overweight";
            document.getElementById("lblSRWHClassification").style.color = "red";
        }
        else if (WeightHeightZ >= 3 && WeightHeightZ < 4) {
            $("#txtSRHAgeZScore").val("3");
            document.getElementById("txtSRHAgeZScore").style.color = "red";
            document.getElementById("lblSRWHClassification").innerHTML = " Overweight";
            document.getElementById("lblSRWHClassification").style.color = "red";
        }
        else if (WeightHeightZ >= 2 && WeightHeightZ < 3) {
            $("#txtSRHAgeZScore").val("2");
            document.getElementById("txtSRHAgeZScore").style.color = "red";
            document.getElementById("lblSRWHClassification").innerHTML = " Overweight";
            document.getElementById("lblSRWHClassification").style.color = "red"; ;
        }
        else if (WeightHeightZ >= 1 && WeightHeightZ < 2) {
            $("#txtSRHAgeZScore").val("1");
            document.getElementById("txtSRHAgeZScore").style.color = "red";
            document.getElementById("lblSRWHClassification").innerHTML = " Overweight";
            document.getElementById("lblSRWHClassification").style.color = "red"; ;
        }
        else if (WeightHeightZ > -1 && WeightHeightZ < 1) {
            $("#txtSRHAgeZScore").val("0");
            document.getElementById("txtSRHAgeZScore").style.color = "green";
            document.getElementById("lblSRWHClassification").innerHTML = " Normal";
            document.getElementById("lblSRWHClassification").style.color = "green";
        }
        else if (WeightHeightZ <= -1 && WeightHeightZ > -2) {
            $("#txtSRHAgeZScore").val("-1");
            document.getElementById("txtSRHAgeZScore").style.color = "orange";
            document.getElementById("lblSRWHClassification").innerHTML = " Mild";
            document.getElementById("lblSRWHClassification").style.color = "orange";
        }
        else if (WeightHeightZ <= -2 && WeightHeightZ > -3) {
            $("#txtSRHAgeZScore").val("-2");
            document.getElementById("txtSRHAgeZScore").style.color = "red";
            document.getElementById("lblSRWHClassification").innerHTML = " Moderate";
            document.getElementById("lblSRWHClassification").style.color = "red"; ;
        }
        else if (WeightHeightZ <= -3 && WeightHeightZ > -4) {
            $("#txtSRHAgeZScore").val("-3");
            document.getElementById("txtSRHAgeZScore").style.color = "red";
            document.getElementById("lblSRWHClassification").innerHTML = " Severe";
            document.getElementById("lblSRWHClassification").style.color = "red"; ;
        }
        else if (WeightHeightZ <= -4) {
            $("#txtSRHAgeZScore").val("-4");
            document.getElementById("txtSRHAgeZScore").style.color = "red";
            document.getElementById("lblSRWHClassification").innerHTML = "Severe";
            document.getElementById("lblSRWHClassification").style.color = "red"; ;
        }
    }
}

function CalculateBMIZScoreSR() {
    if (jQuery.isEmptyObject(zscoreDetails) == false) {
        var txtWeight = $("#txtSRWeight").val();
        var txtHeight = $("#txtSRHeight").val();
        var L = zscoreDetails.BMIz.L;
        var M = zscoreDetails.BMIz.M;
        var S = zscoreDetails.BMIz.S;
        var bmi = 0;
        var BMIz = 0;

        if (txtHeight != "" && txtWeight != "")
            bmi = parseFloat(txtWeight) / ((parseFloat(txtHeight) / 100) * (parseFloat(txtHeight) / 100));
        else
            bmi = 0;

        if (L != 0)
            BMIz = ((Math.pow((bmi / M), L)) - 1) / (S * L);
        else
            BMIz = (Math.log(bmi / M)) / S;


        if (BMIz >= 4) {
            $("#txtSRBMIZScore").val("4");
            document.getElementById("txtSRBMIZScore").style.color = "red";
            document.getElementById("lblSRBMIzClassification").innerHTML = " Overweight";
            document.getElementById("lblSRBMIzClassification").style.color = "red";
        }
        else if (BMIz >= 3 && BMIz < 4) {
            $("#txtSRBMIZScore").val("3");
            document.getElementById("txtSRBMIZScore").style.color = "red";
            document.getElementById("lblSRBMIzClassification").innerHTML = " Overweight";
            document.getElementById("lblSRBMIzClassification").style.color = "red";
        }
        else if (BMIz >= 2 && BMIz < 3) {
            $("#txtSRBMIZScore").val("2");
            document.getElementById("txtSRBMIZScore").style.color = "red";
            document.getElementById("lblSRBMIzClassification").innerHTML = " Overweight";
            document.getElementById("lblSRBMIzClassification").style.color = "red"; ;
        }
        else if (BMIz >= 1 && BMIz < 2) {
            $("#txtSRBMIZScore").val("1");
            document.getElementById("txtSRBMIZScore").style.color = "red";
            document.getElementById("lblSRBMIzClassification").innerHTML = " Overweight";
            document.getElementById("lblSRBMIzClassification").style.color = "red"; ;
        }
        else if (BMIz > -1 && BMIz < 1) {
            $("#txtSRBMIZScore").val("0");
            document.getElementById("txtSRBMIZScore").style.color = "green";
            document.getElementById("lblSRBMIzClassification").innerHTML = " Normal";
            document.getElementById("lblSRBMIzClassification").style.color = "green";
        }
        else if (BMIz <= -1 && BMIz > -2) {
            $("#txtBMIZScore").val("-1");
            document.getElementById("txtHAgeZScore").style.color = "orange";
            document.getElementById("lblSRBMIzClassification").innerHTML = " Mild";
            document.getElementById("lblSRBMIzClassification").style.color = "orange";
        }
        else if (BMIz <= -2 && BMIz > -3) {
            $("#txtSRBMIZScore").val("-2");
            document.getElementById("txtSRBMIZScore").style.color = "red";
            document.getElementById("lblSRBMIzClassification").innerHTML = " Moderate";
            document.getElementById("lblSRBMIzClassification").style.color = "red"; ;
        }
        else if (BMIz <= -3 && BMIz > -4) {
            $("#txtSRBMIZScore").val("-3");
            document.getElementById("txtSRBMIZScore").style.color = "red";
            document.getElementById("lblSRBMIzClassification").innerHTML = " Severe";
            document.getElementById("lblSRBMIzClassification").style.color = "red"; ;
        }
        else if (BMIz <= -4) {
            $("#txtSRBMIZScore").val("-4");
            document.getElementById("txtSRBMIZScore").style.color = "red";
            document.getElementById("lblSRBMIzClassification").innerHTML = "Severe";
            document.getElementById("lblSRBMIzClassification").style.color = "red"; ;
        }
    }
}


function OpenCEPopup(url) {
    var NWin = window.open(url, '', 'height=800,width=800');
    if (window.focus) {
        NWin.focus();
    }
    return false;
}

