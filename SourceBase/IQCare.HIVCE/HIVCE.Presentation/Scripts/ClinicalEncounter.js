var arrMilestones = [];
var arrImmunization = [];
var arrLTM = [];
var arrTannerStaging = [];
var arrPriorART = [];
var drugAllergen = [];
var envAllergen = [];
var foodAllergen = [];


$(document).ready(function () {
    $.hivce.loader('show');
    GetTriageData();

    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href") // activated tab
        //alert(target);
        $.hivce.loader('show');
        if (target == "#tab_1") {
            GetTriageData();
        }
        else if (target == "#tab_2") {
            GetPresentingComplaintsData();
        }
        else if (target == "#tab_3") {
            GetAddtionalHxData();
        }
        else if (target == "#tab_4") {
        }
        else if (target == "#tab_5") {
        }
        else if (target == "#tab_6") {
        }
    });


});


function GetTriageData() {

    //debugger;
    var postData;
    $.ajax({
        type: "POST",
        url: "ClinicalEncounter.aspx?data=getdata",
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
                InitTriageControls(response);
            }
        },
        error: function (msg) {
        }
    });
}


function GetPresentingComplaintsData() {
    //debugger;
    var postData;
    $.ajax({
        type: "POST",
        url: "ClinicalEncounter.aspx?data=getpcdata",
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
        url: "ClinicalEncounter.aspx?data=getAddtionalHx",
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
                InitAdditionalHxControls(response);
            }

        },
        error: function (msg) {
        }
    });
}