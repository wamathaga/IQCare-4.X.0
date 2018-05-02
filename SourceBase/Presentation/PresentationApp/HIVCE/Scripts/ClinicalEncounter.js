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
    GetTriageData();
    $("#hidTabName").val("triage");

    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href") // activated tab
        if (target.toLowerCase().indexOf("#wtab") == -1) {

            $.hivce.loader('show');
            if (target == "#tab_1") {
                $("#hidTabName").val("triage");
                GetTriageData();
            }
            else if (target == "#tab_2") {
                GetPresentingComplaintsData();
                $("#hidTabName").val("presentingcomplaints");
            }
            else if (target == "#tab_3") {
                GetAddtionalHxData();
                $("#hidTabName").val("addtionalhx");
            }
            else if (target == "#tab_4") {
                GetScreeningData();
                $("#hidTabName").val("screening");
            }
            else if (target == "#tab_5") {
                GetSystematicReviewData();
                $("#hidTabName").val("systemicreview");
            }
            else if (target == "#tab_6") {
                GetManagementData();
                $("#hidTabName").val("management");
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
        GetTriageData();
        $("#hidTabName").val("triage");
    }
    else if (target == "tab_2") {
        GetPresentingComplaintsData();
        $("#hidTabName").val("presentingcomplaints");
    }
    else if (target == "tab_3") {
        GetAddtionalHxData();
        $("#hidTabName").val("addtionalhx");
    }
    else if (target == "tab_4") {
        GetScreeningData();
        $("#hidTabName").val("screening");
    }
    else if (target == "tab_5") {
        GetSystematicReviewData();
        $("#hidTabName").val("systemicreview");
    }
    else if (target == "tab_6") {
        GetManagementData();
        $("#hidTabName").val("management");
    }
}

function GetNextPage(target) {
    $(".nav-tabs a[href=\"#" + target + "\"]").tab("show");
    if (target == "tab_1") {
        GetTriageData();
        $("#hidTabName").val("triage");
    }
    else if (target == "tab_2") {
        GetPresentingComplaintsData();
        $("#hidTabName").val("presentingcomplaints");
    }
    else if (target == "tab_3") {
        GetAddtionalHxData();
        $("#hidTabName").val("addtionalhx");
    }
    else if (target == "tab_4") {
        GetScreeningData();
        $("#hidTabName").val("screening");
    }
    else if (target == "tab_5") {
        GetSystematicReviewData();
        $("#hidTabName").val("systemicreview");
    }
    else if (target == "tab_6") {
        GetManagementData();
        $("#hidTabName").val("management");
    }
    $.hivce.loader('hide');
}

//function getPageName() {
//    var url = window.location.pathname;
//    var index = url.lastIndexOf("/") + 1;
//    var filenameWithExtension = url.substr(index);
//    var filename = filenameWithExtension.split(".")[0]; // <-- added this line
//    return filename;                                    // <-- added this line
//}

function stripTrailingSlash(str) {
    if (str.substr(-1) == '/') {
        return str.substr(0, str.length - 1);
    }
    return str;
}

function enableAlreadySavedTabs(response) {
    //        HIVCETriage
    //        PresentingComplaints
    //        AddtionalHx
    //        Screening
    //        SystemicReview
    //        Management
    debugger;
    $.each(response, function (index, value) {
        if (value.DN == 'PresentingComplaints') {
            $('#a_PC').removeClass('disabled');
        }
        else if (value.DN == 'AddtionalHx') {
            $('#a_AH').removeClass('disabled');
        }
        else if (value.DN == 'Screening') {
            $('#a_S').removeClass('disabled');
        }
        else if (value.DN == 'SystemicReview') {
            $('#a_SR').removeClass('disabled');
        }
        else if (value.DN == 'Management') {
            $('#a_M').removeClass('disabled');
        }
    });
}
function GetTriageData() {
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
                if (response.AST != null) {
                    currAST = response.AST;
                    enableAlreadySavedTabs(response.AST);
                }
                InitTriageControls(response);
            }
        },
        error: function (msg) {
        }
    });
}


function GetPresentingComplaintsData() {
    debugger;
    var postData;
    $.ajax({
        type: "POST",
        //url: "ClinicalEncounter.aspx?data=getpcdata",
        url: activePage + "?data=getpcdata",
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
                $('#a_PC').removeClass('disabled');
                InitPresentingComplaintsControls(response);

            }

        },
        error: function (msg) {
        }
    });
}

function GetAddtionalHxData() {
    //debugger;
    var postData;
    $.ajax({
        type: "POST",
        //url: "ClinicalEncounter.aspx?data=getAddtionalHx",
        url: activePage + "?data=getAddtionalHx",
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
                $('#a_AH').removeClass('disabled');
                InitAdditionalHxControls(response);
            }

        },
        error: function (msg) {
        }
    });
}

function GetScreeningData() {
    debugger;
    var postData;
    $.ajax({
        type: "POST",
        //url: "ClinicalEncounter.aspx?data=getAddtionalHx",
        url: activePage + "?data=getscreeningdata",
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
                $('#a_S').removeClass('disabled');
                InitScreeningControls(response)
            }

        },
        error: function (msg) {
        }
    });
}

function GetSystematicReviewData() {
    //debugger;
    var postData;
    $.ajax({
        type: "POST",
        //url: "ClinicalEncounter.aspx?data=getAddtionalHx",
        url: activePage + "?data=getsystemicreview",
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
                $('#a_SR').removeClass('disabled');
                InitSystemicReviewControls(response);
            }

        },
        error: function (msg) {
        }
    });
}

function GetManagementData() {
    //debugger;
    var postData;
    $.ajax({
        type: "POST",
        //url: "ClinicalEncounter.aspx?data=getAddtionalHx",
        url: activePage + "?data=getmgtdata",
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
                $('#a_M').removeClass('disabled');
                InitManagementControls(response);
            }

        },
        error: function (msg) {
        }
    });
}





function DeleteForm() {
    //alert(GetParameterValues('name'));
    $.hivce.loader('show');
    var formName = '';
    activePage = getPageName() + '.aspx';

    if (~activePage.indexOf('Clinical')) { formName = 'Clinical Encounter'; }


    var postData;
    $.ajax({
        type: "POST",
        //url: "ClinicalEncounter.aspx?data=getAddtionalHx",
        url: activePage + "?data=deleteform",
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

