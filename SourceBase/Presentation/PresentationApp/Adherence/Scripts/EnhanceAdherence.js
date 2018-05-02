var arrMilestones = [];
var arrImmunization = [];
var arrLTM = [];
var arrTannerStaging = [];
var arrPriorART = [];
var drugAllergen = [];
var envAllergen = [];
var foodAllergen = [];
var arrDrugAllergies = [];
var arrHIVFamilyMember = [];
var arrPresentingComplaints = [];
var arrChronicDisorders = [];
var arrPV = [];
var chkStillActive;
var activePage;
var currAST;

$(document).ready(function () {

    $.hivce.loader('show');
    GetSession1Data();

    $("#hidTabName").val("session1");
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href") // activated tab
        if (target.toLowerCase().indexOf("#wtab") == -1) {

            $.hivce.loader('show');

            if (target == "#tab_1") {
                $("#hidTabName").val("session1");
                GetSession1Data();
            }
            else if (target == "#tab_2") {
                GetSession2Data();
                $("#hidTabName").val("session2");
            }
            else if (target == "#tab_3") {
                GetSession3Data();
                $("#hidTabName").val("session3");
            }
            else if (target == "#tab_4") {
                GetSession4Data();
                $("#hidTabName").val("session4");
            }
            else if (target == "#tab_5") {
                GetSessionVLData();
                $("#hidTabName").val("sessionVL");
            }
        }
    });

    $("textarea").maxlength({
        //alwaysShow: true,
        threshold: 50,
        warningClass: "labelTextBox label label-success",
        limitReachedClass: "labelTextBox label label-danger",
        separator: ' out of ',
        preText: 'You write ',
        postText: ' characters.',
        validate: true,
        placement: 'top'
    });

});

function GetPreviousPage(target) {
    $.hivce.loader('show');
    $(".nav-tabs a[href=\"#" + target + "\"]").tab("show");
    if (target == "tab_1") {
        GetSession1Data();
        $("#hidTabName").val("session1");
    }
    else if (target == "tab_2") {
        GetSession2Data();
        $("#hidTabName").val("session2");
    }
    else if (target == "tab_3") {
        GetSession3Data();
        $("#hidTabName").val("session3");
    }
    else if (target == "tab_4") {
        GetSession4Data();
        $("#hidTabName").val("session4");
    }
    else if (target == "tab_5") {
        GetSessionVLData();
        $("#hidTabName").val("sessionVL");
    }
}

function GetNextPage(target) {
    debugger;
    $(".nav-tabs a[href=\"#" + target + "\"]").tab("show");
    if (target == "tab_1") {
        GetSession1Data();
        $("#hidTabName").val("session1");
    }
    else if (target == "tab_2") {
        GetSession2Data();
        $("#hidTabName").val("session2");
    }
    else if (target == "tab_3") {
        GetSession3Data();
        $("#hidTabName").val("session3");
    }
    else if (target == "tab_4") {
        GetSession4Data();
        $("#hidTabName").val("session4");
    }
    else if (target == "tab_5") {
        GetSessionVLData();
        $("#hidTabName").val("sessionVL");
    }
    $.hivce.loader('hide');
}

function enableAlreadySavedTabs(response) {
    debugger;
    $.each(response, function (index, value) {
        if (value.DN == 'Session2') {
            $('#S2').removeClass('disabled');
        }
        else if ((value.DN == 'Session3') || (value.DN == 'Session4') || (value.DN == 'SessionVL')) {
            $('#S3').removeClass('disabled');
            $('#S4').removeClass('disabled');
            $('#S5').removeClass('disabled');
        }
    });
}

function GetSession1Data() {
    debugger;
    var postData;
    var callFrom = $("#hidCallFrom").val();
    $.ajax({
        type: "POST",
        url: "EnhanceAdherence.aspx?data=getSession1Data",
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
            var msg = response.EM;
            if (responseSuccess != null) {
                if (responseSuccess == "false") {
                    customAlert(errorMessage);
                }
            }
            else {
                if (response.MV != null && response.MV == 0 && response.VId > 0 && callFrom == 'new') {
                    // display message to open existing form.......
                    customAlert("Please complete the previous unfilled form, first before opening a new one of the same.");
                    window.location.assign("../ClinicalForms/frmPatient_History.aspx?&sts=0");
                }
                else {
                    if (response.AST != null) {
                        currAST = response.AST;
                        enableAlreadySavedTabs(response.AST);
                    }
                    Init_UC_Session1_Controls(response);
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            //ShowErrorMessage(msg);
            console.log(xhr);
            console.log(textStatus);
            console.log(errorThrown);
        }
        //        error: function (msg) {
        //        }
    });
}


function GetSession2Data() {
    debugger;
    var postData;
    $.ajax({
        type: "POST",
        url: "EnhanceAdherence.aspx?data=getSession2Data",
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
            var msg = response.EM;
            if (responseSuccess != null) {
                if (responseSuccess == "false") {
                    customAlert(errorMessage);
                }
            }
            else {
                $('#S2').removeClass('disabled');
                Init_UC_Session2_Controls(response);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            //ShowErrorMessage(msg);
            console.log(xhr);
            console.log(textStatus);
            console.log(errorThrown);
        }
        //        error: function (msg) {
        //        }
    });
}

function GetSession3Data() {
    //debugger;
    var postData;
    $.ajax({
        type: "POST",
        url: "EnhanceAdherence.aspx?data=getSession3Data",

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
            var msg = response.EM;
            if (responseSuccess != null) {
                if (responseSuccess == "false") {
                    customAlert(errorMessage);
                }
            }
            else {
                $('#S3').removeClass('disabled');
                $('#S4').removeClass('disabled');
                $('#S5').removeClass('disabled');
                Init_UC_Session3_Controls(response);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            //ShowErrorMessage(msg);
            console.log(xhr);
            console.log(textStatus);
            console.log(errorThrown);
        }
    });
}

function GetSession4Data() {
    debugger;
    var postData;
    $.ajax({
        type: "POST",
        url: "EnhanceAdherence.aspx?data=getSession4Data",

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
            var msg = response.EM;
            if (responseSuccess != null) {
                if (responseSuccess == "false") {
                    customAlert(errorMessage);
                }
            }
            else {
                Init_UC_Session4_Controls(response)
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            //ShowErrorMessage(msg);
            console.log(xhr);
            console.log(textStatus);
            console.log(errorThrown);
        }
    });
}

function GetSessionVLData() {
    debugger;
    var postData;
    $.ajax({
        type: "POST",
        url: "EnhanceAdherence.aspx?data=getSessionVLData",
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
            var msg = response.EM;
            if (responseSuccess != null) {
                if (responseSuccess == "false") {
                    customAlert(errorMessage);
                }
            }
            else {
                Init_UC_SessionVL_Controls(response)
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            //ShowErrorMessage(msg);
            console.log(xhr);
            console.log(textStatus);
            console.log(errorThrown);
        }
    });
}


function DeleteForm() {
    $.hivce.loader('show');
    var postData;
    $.ajax({
        type: "POST",
        url: "EnhanceAdherence.aspx?data=deleteform",
        //data: '{formName: "' + formName + '" }',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        beforeSend: function () {
        },
        success: function (response) {
            console.log(response);
            var responseSuccess = response.Success;
            var msg = response.EM;
            if (responseSuccess != null) {
                if (responseSuccess == "false") {
                    customAlert(errorMessage);
                }
            }
            else {
                //string theUrl;
                //theUrl = string.Format("{0}", "frmPatient_Home.aspx?Func=Delete");
                //Response.Redirect(theUrl);
                window.open("../ClinicalForms/frmPatient_Home.aspx?Func=Delete", "_self");
            }
        },
        error: function (msg) {
        }
    });
}

