var activePage;
var currAST;

$(document).ready(function () {
    $.hivce.loader('show');
    debugger;
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
                GetAssessmentData();
                $("#hidTabName").val("assessment");
            }
            else if (target == "#tab_3") {
                GetInitiationData();
                $("#hidTabName").val("initiation");
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
        GetAssessmentData();
        $("#hidTabName").val("assessment");
    }
    else if (target == "tab_3") {
        GetInitiationData();
        $("#hidTabName").val("initiation");
    }
}

function GetNextPage(target) {
    $(".nav-tabs a[href=\"#" + target + "\"]").tab("show");
    if (target == "tab_1") {
        GetTriageData();
        $("#hidTabName").val("triage");
    }
    else if (target == "tab_2") {
        GetAssessmentData();
        $("#hidTabName").val("assessment");
    }
    else if (target == "tab_3") {
        GetInitiationData();
        $("#hidTabName").val("initiation");
    }

    $.hivce.loader('hide');
}



function stripTrailingSlash(str) {
    if (str.substr(-1) == '/') {
        return str.substr(0, str.length - 1);
    }
    return str;
}

function enableAlreadySavedTabs(response) {
    debugger;
    $.each(response, function (index, value) {
        if (value.DN == 'assessment') {
            $('#a_PA').removeClass('disabled');
        }
        else if (value.DN == 'initiation') {
            $('#a_PI').removeClass('disabled');
        }
    });
}
function GetTriageData() {
    //activePage = getPageName() + '.aspx';
    var postData;
    $.ajax({
        type: "POST",
        url: "PrEP.aspx?data=gettriagedata",
        //url: activePage + "?data=getdata",
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
    //Write By Rahmat for TEST(We wilc comment below Line after Tes)
    $.hivce.loader('hide');
}


function GetAssessmentData() {
    debugger;
    var postData;
    $.ajax({
        type: "POST",
        url: "PrEP.aspx?data=getassessmentdata",
        //url: activePage + "?data=getpcdata",
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
                $('#a_PA').removeClass('disabled');
                InitAssessmentControls(response);

            }

        },
        error: function (msg) {
        }
    });
}

function GetInitiationData() {
    //debugger;
    var postData;
    $.ajax({
        type: "POST",
        url: "PrEP.aspx?data=getinitiationdata",
        //url: activePage + "?data=getAddtionalHx",
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
                $('#a_PI').removeClass('disabled');
                InitPrEPInitiationControls(response);
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
    //activePage = getPageName() + '.aspx';
    //if (~activePage.indexOf('PrEP')) { formName = 'PrEP'; }


    var postData;
    $.ajax({
        type: "POST",
        url: "PrEP.aspx?data=getAddtionalHx",
        //url: activePage + "?data=deleteform",
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

