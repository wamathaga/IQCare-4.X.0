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
        table.rows.add(arrUI).draw().nodes();
    }
}

function DisableDiv(ctrlName) {
    var disDiv;
    if (ctrlName.id == "chkComplaints") {
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

    var ctrlVal = $("#" + ctrlName.id).bootstrapSwitch('state');
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



