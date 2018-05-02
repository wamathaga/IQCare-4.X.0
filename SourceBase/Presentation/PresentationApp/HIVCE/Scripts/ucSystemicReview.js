var arrWHoStage = [];
var arrCWHoStage = [];


function InitSystemicReviewControls(response) {
    window.scrollTo(0, 0);
    $("#ddlGeneralExamination").select2();
    $("#ddlSkin").select2();
    $("#ddlENT").select2();
    $("#ddlChest").select2();
    $("#ddlCVS").select2();
    $("#ddlAbdomen").select2();
    $("#ddlCNS").select2();
    $("#ddlGUSystem").select2();
    $("#ddlCurrentWhoStage").select2();
    $("#ddlGenitoUrinary").select2();
    

    $("#chkSkin").bootstrapSwitch('state', false);
    $("#chkENT").bootstrapSwitch('state', false);
    $("#chkChest").bootstrapSwitch('state', false);
    $("#chkCVS").bootstrapSwitch('state', false);
    $("#chkAbdomen").bootstrapSwitch('state', false);
    $("#chkCNS").bootstrapSwitch('state', false);
    $("#chkGUSystem").bootstrapSwitch('state', false);
    $("#chkGenitoUrinary").bootstrapSwitch('state', false);

    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });
    var disDiv = "divSkin";
    $("#" + disDiv).css("visibility", "hidden");
    $("#" + disDiv).css("display", "none");

    disDiv = "divENT";
    $("#" + disDiv).css("visibility", "hidden");
    $("#" + disDiv).css("display", "none");

    disDiv = "divChest";
    $("#" + disDiv).css("visibility", "hidden");
    $("#" + disDiv).css("display", "none");

    disDiv = "divCVS";
    $("#" + disDiv).css("visibility", "hidden");
    $("#" + disDiv).css("display", "none");

    disDiv = "divAbdomen";
    $("#" + disDiv).css("visibility", "hidden");
    $("#" + disDiv).css("display", "none");

    disDiv = "divCNS";
    $("#" + disDiv).css("visibility", "hidden");
    $("#" + disDiv).css("display", "none");

    disDiv = "divGEU";
    $("#" + disDiv).css("visibility", "hidden");
    $("#" + disDiv).css("display", "none");

    disDiv = "divGUSystem";
    $("#" + disDiv).css("visibility", "hidden");
    $("#" + disDiv).css("display", "none");

    $('#divRadioNutritionGroup input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });


    var visitType = $("#ddlVisitType").select2("val");
    var visitTypeData = $("#ddlVisitType").select2('data')[0];


    if (visitType != null) {
        if (visitTypeData.text.toUpperCase().indexOf("SUPPORTER") == -1) {
            $("#lblCurrentWHOStaging").removeClass().addClass("control-label requiredFieldColor");
        }
        else {
            $("#lblCurrentWHOStaging").removeClass().addClass("control-label");
        }
    }

    $('#divNutritionCounseling input[type="radio"]').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });
    $('input:radio[name=rdoNutritionCounseling]').on('ifChecked', function (event) {
        $(this).closest("input").attr('checked', true);
    });
    $('input:radio[name=rdoNutritionCounseling]').on('ifUnchecked', function (event) {
        $(this).closest("input").attr('checked', false);
    });

    //GetSystemicReviewData();
    BindWhoStage(response);

}

//function GetSystemicReviewData() {
//    activePage = getPageName() + '.aspx';
//    //debugger;
//    var postData;
//    $.ajax({
//        type: "POST",
//        //url: "ClinicalEncounter.aspx?data=getWhoStage",
//        url: activePage + "?data=getsystemicreview",
//        //data: JSON.stringify(nodeId),
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        async: true,
//        cache: false,
//        beforeSend: function () {
//        },
//        success: function (response) {
//            console.log(response);
//            var responseSuccess = response.Success;
//            if (responseSuccess != null) {
//                if (responseSuccess == "false") {
//                    customAlert(errorMessage);
//                }
//            }
//            else {
//                BindWhoStage(response);
//            }

//        },
//        error: function (msg) {
//        }
//    });
//}


function BindWhoStage(response) {
    //debugger;
    var collections = response.Collection;

    var data = $.grep(collections, function (e) { return e.CN == "GeneralConditions"; });
    var vdata = [];
    $.each(data, function (index, value) {
        if (value.DN.toLowerCase() !== "other") {
            vdata.push({ id: value.DId, text: value.DN });
        }
    });

    $("#ddlGeneralExamination").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: false,
        data: vdata
    });
    $("#ddlGeneralExamination").select2("val", "0");
    $("#ddlGeneralExamination").trigger('change.select2');

    data = $.grep(collections, function (e) { return e.CN == "SkinConditions"; });
    vdata = [];
    $.each(data, function (index, value) {
        if (value.DN.toLowerCase() !== "other") {
            vdata.push({ id: value.DId, text: value.DN });
        }
    });

    $("#ddlSkin").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: false,
        data: vdata
    });
    $("#ddlSkin").select2("val", "0");
    $("#ddlSkin").trigger('change.select2');

    data = $.grep(collections, function (e) { return e.CN == "ENTConditions"; });
    vdata = [];

    $.each(data, function (index, value) {
        if (value.DN.toLowerCase() !== "other") {
            if (value.DN.toLowerCase() == "normal") {
                vdata.push({ id: value.DId, text: value.DN });
          }
        }
    });

    $("#ddlENT").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: false,
        data: vdata
    });

    $.each(data, function (index, value) {
        if (value.DN.toLowerCase() !== "other" && value.DN.toLowerCase() !== "normal") {
            vdata.push({ id: value.DId, text: value.DN });
        }
    });

    $("#ddlENT").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: false,
        data: vdata
    });
    $("#ddlENT").select2("val", "0");
    $("#ddlENT").trigger('change.select2');

    data = $.grep(collections, function (e) { return e.CN == "ChestLungsConditions"; });
    vdata = [];
    $.each(data, function (index, value) {
        if (value.DN.toLowerCase() !== "other") {
            vdata.push({ id: value.DId, text: value.DN });
        }
    });

    $("#ddlChest").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: false,
        data: vdata
    });
    $("#ddlChest").select2("val", "0");
    $("#ddlChest").trigger('change.select2');

    data = $.grep(collections, function (e) { return e.CN == "CardiovascularConditions"; });
    vdata = [];
    $.each(data, function (index, value) {
        if (value.DN.toLowerCase() !== "other") {
            vdata.push({ id: value.DId, text: value.DN });
        }
    });

    $("#ddlCVS").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: false,
        data: vdata
    });
    $("#ddlCVS").select2("val", "0");
    $("#ddlCVS").trigger('change.select2');

    data = $.grep(collections, function (e) { return e.CN == "AbdomenConditions"; });
    vdata = [];
    $.each(data, function (index, value) {
        if (value.DN.toLowerCase() !== "other") {
            vdata.push({ id: value.DId, text: value.DN });
        }
    });

    $("#ddlAbdomen").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: false,
        data: vdata
    });
    $("#ddlAbdomen").select2("val", "0");
    $("#ddlAbdomen").trigger('change.select2');

    data = $.grep(collections, function (e) { return e.CN == "CNSConditions"; });
    vdata = [];
    $.each(data, function (index, value) {
        if (value.DN.toLowerCase() !== "other") {
            vdata.push({ id: value.DId, text: value.DN });
        }
    });

    $("#ddlCNS").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: false,
        data: vdata
    });
    $("#ddlCNS").select2("val", "0");
    $("#ddlCNS").trigger('change.select2');

    data = $.grep(collections, function (e) { return e.CN == "GenitalUrinaryConditions"; });
    vdata = [];
    $.each(data, function (index, value) {
        if (value.DN.toLowerCase() !== "other") {
            vdata.push({ id: value.DId, text: value.DN });
        }
    });

    $("#ddlGenitoUrinary").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: false,
        data: vdata
    });
    $("#ddlGenitoUrinary").select2("val", "0");
    $("#ddlGenitoUrinary").trigger('change.select2');


    data = $.grep(collections, function (e) { return e.CN == "WHOStageIConditions"; });
    if (jQuery.isEmptyObject(data) == false) {

        $("#dtlStageI").dataTable().fnDestroy();

        $("#dtlStageI").DataTable({
            "aaData": data,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
         { bSortable: false,
             mRender: function (data, type, full) {
                 var str = "<input type=\"hidden\" runat=\"server\" id=\"hidRowId\" value=\"" + full["DId"] + "\"  />";
                 str = str + "<input type=\"checkbox\" id=\"chkAchieved" + full["DId"] + "\" class=\"flat-red\" title=\"1\" />";
                 return str;
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {

                 return full["DN"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 var str = "<input type=\"text\" class=\"form-control\" id=\"dtCurrent" + full["DId"] + "\" placeholder=\"Current\" data-date-format=\"dd-M-yyyy\">";
                 return str;
             }
         },
         {
             bSortable: false,
             mRender: function (data, type, full) {
                 var str = "<input type=\"text\" class=\"form-control\" id=\"dtHistory" + full["DId"] + "\" placeholder=\"History\" data-date-format=\"dd-M-yyyy\">";
                 return str;
             }
         }
    ]
        });

    }

    data = $.grep(collections, function (e) { return e.CN == "WHOStageIIConditions"; });
    if (jQuery.isEmptyObject(data) == false) {

        $("#dtlStageII").dataTable().fnDestroy();

        $("#dtlStageII").DataTable({
            "aaData": data,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
         { bSortable: false,
             mRender: function (data, type, full) {
                 var str = "<input type=\"hidden\" runat=\"server\" id=\"hidRowId\" value=\"" + full["DId"] + "\"  />";
                 str = str + "<input type=\"checkbox\" id=\"chkAchieved" + full["DId"] + "\" class=\"flat-red\" title=\"2\"/>";
                 return str;
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {

                 return full["DN"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 var str = "<input type=\"text\" class=\"form-control\" id=\"dtCurrent" + full["DId"] + "\" placeholder=\"Current\" data-date-format=\"dd-M-yyyy\">";
                 return str;
             }
         },
         {
             bSortable: false,
             mRender: function (data, type, full) {
                 var str = "<input type=\"text\" class=\"form-control\" id=\"dtHistory" + full["DId"] + "\" placeholder=\"History\" data-date-format=\"dd-M-yyyy\">";
                 return str;
             }
         }
    ]
        });

    }

    data = $.grep(collections, function (e) { return e.CN == "WHOStageIIICoditions"; });
    if (jQuery.isEmptyObject(data) == false) {

        $("#dtlStageIII").dataTable().fnDestroy();

        $("#dtlStageIII").DataTable({
            "aaData": data,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
         { bSortable: false,
             mRender: function (data, type, full) {
                 var str = "<input type=\"hidden\" runat=\"server\" id=\"hidRowId\" value=\"" + full["DId"] + "\"  />";
                 str = str + "<input type=\"checkbox\" id=\"chkAchieved" + full["DId"] + "\" class=\"flat-red\" title=\"3\"/>";
                 return str;
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {

                 return full["DN"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 var str = "<input type=\"text\" class=\"form-control\" id=\"dtCurrent" + full["DId"] + "\" placeholder=\"Current\" data-date-format=\"dd-M-yyyy\">";
                 return str;
             }
         },
         {
             bSortable: false,
             mRender: function (data, type, full) {
                 var str = "<input type=\"text\" class=\"form-control\" id=\"dtHistory" + full["DId"] + "\" placeholder=\"History\" data-date-format=\"dd-M-yyyy\">";
                 return str;
             }
         }
    ]
        });

    }

    data = $.grep(collections, function (e) { return e.CN == "WHOStageIVConditions"; });
    if (jQuery.isEmptyObject(data) == false) {

        $("#dtlStageIV").dataTable().fnDestroy();

        $("#dtlStageIV").DataTable({
            "aaData": data,
            "bSort": false,
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "autoWidth": false,
            "aoColumns": [
         { bSortable: false,
             mRender: function (data, type, full) {
                 var str = "<input type=\"hidden\" runat=\"server\" id=\"hidRowId\" value=\"" + full["DId"] + "\"  />";
                 str = str + "<input type=\"checkbox\" id=\"chkAchieved" + full["DId"] + "\" class=\"flat-red\" title=\"4\"/>";
                 return str;
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {

                 return full["DN"];
             }
         },
         { bSortable: false,
             mRender: function (data, type, full) {
                 var str = "<input type=\"text\" class=\"form-control\" id=\"dtCurrent" + full["DId"] + "\" placeholder=\"Current\" data-date-format=\"dd-M-yyyy\">";
                 return str;
             }
         },
         {
             bSortable: false,
             mRender: function (data, type, full) {
                 var str = "<input type=\"text\" class=\"form-control\" id=\"dtHistory" + full["DId"] + "\" placeholder=\"History\" data-date-format=\"dd-M-yyyy\">";
                 return str;
             }
         }
    ]
        });

    }

    data = $.grep(collections, function (e) { return e.CN == "WHO Stage"; });
    var vdata = [];
    $.each(data, function (index, value) {
        vdata.push({ id: value.DId, text: value.DN });
    });
    arrCWHoStage = vdata;

    $("#ddlCurrentWhoStage").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        data: vdata
    });
    $("#ddlCurrentWhoStage").select2("val", "0");
    $("#ddlCurrentWhoStage").trigger('change.select2');

    $('input[type="checkbox"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });
    $('input').on('ifChecked', function (event) {
        $(this).closest("input").attr('checked', true);
        WhoStageCheckBoxClick(this, false);
    });
    $('input').on('ifUnchecked', function (event) {
        $(this).closest("input").attr('checked', false);
        WhoStageCheckBoxClick(this, true);
    });

    $('input[data-date-format="dd-M-yyyy"]').datepicker({
        autoclose: true
    })//.datepicker("setDate", "0");

    $("input:radio[name=NutritionGroup][value=" + response.NA + "]").attr('checked', 'checked');
    $("input:radio[name=NutritionGroup]").iCheck('update');
    $("#txtAreaNutritionComment").val(response.NAC);
    $("#txtReviewSystemComments").val(response.RSC);
    $("#ddlCurrentWhoStage").select2().val(response.WS).trigger("change");

    debugger;

    $("input:radio[name=rdoNutritionCounseling][value=" + response.NC + "]").attr('checked', 'checked');
    $("input:radio[name=rdoNutritionCounseling]").iCheck('update');

    BindMultiSelectDropDown("ddlGeneralExamination", response.GE);

    BindMultiSelectDropDown("ddlSkin", response.Skin);
    if (jQuery.isEmptyObject(response.Skin) == false) {
        if (response.Skin.length > 0) {
            $("#chkSkin").bootstrapSwitch('state', true);
        }
    }

    BindMultiSelectDropDown("ddlENT", response.ENT);
    if (jQuery.isEmptyObject(response.ENT) == false) {
        if (response.ENT.length > 0) {
            $("#chkENT").bootstrapSwitch('state', true);
        }
    }
    BindMultiSelectDropDown("ddlChest", response.Chest);
    if (jQuery.isEmptyObject(response.Chest) == false) {
        if (response.Chest.length > 0) {
            $("#chkChest").bootstrapSwitch('state', true);
        }
    }
    BindMultiSelectDropDown("ddlCVS", response.CVS);
    if (jQuery.isEmptyObject(response.CVS) == false) {
        if (response.CVS.length > 0) {
            $("#chkCVS").bootstrapSwitch('state', true);
        }
    }
    BindMultiSelectDropDown("ddlAbdomen", response.Abdomen);
    if (jQuery.isEmptyObject(response.Abdomen) == false) {
        if (response.Abdomen.length > 0) {
            $("#chkAbdomen").bootstrapSwitch('state', true);
        }
    }
    BindMultiSelectDropDown("ddlCNS", response.CNS);
    if (jQuery.isEmptyObject(response.CNS) == false) {
        if (response.CNS.length > 0) {
            $("#chkCNS").bootstrapSwitch('state', true);
        }
    }

    BindMultiSelectDropDown("ddlGenitoUrinary", response.GEU);
    if (jQuery.isEmptyObject(response.GEU) == false) {
        if (response.GEU.length > 0) {
            $("#chkGenitoUrinary").bootstrapSwitch('state', true);
        }
    }

    AssignDataTableData("dtlStageI", response.WSS, "WHOStageIConditions");
    AssignDataTableData("dtlStageII", response.WSS, "WHOStageIIConditions");
    AssignDataTableData("dtlStageIII", response.WSS, "WHOStageIIICoditions");
    AssignDataTableData("dtlStageIV", response.WSS, "WHOStageIVConditions");

    $("#txtSRMM").val(arrPV.BPD);
    $("#txtSRHG").val(arrPV.BPS);
    $("#txtSRTemperature").val(arrPV.TEMP);
    $("#txtRR").val(arrPV.RR);
    $("#txtHRate").val(arrPV.HR);
    $("#txtSRHeadCircumference").val(arrPV.HC);
    $("#txtSRHeight").val(arrPV.Height);
    $("#txtSRWeight").val(arrPV.Weight);
    $("#txtSRMUAC").val(arrPV.MUAC);
    $("#txtSRWAgeZScore").val(arrPV.WFA);
    $("#txtSRHAgeZScore").val(arrPV.WFH);
    $("#txtSRBMIZScore").val(arrPV.BMIz);


    GetZScoreDetailsSR();

    CalcualteBMI('hidPAYM', 'txtSRWeight', 'txtSRHeight', 'txtSRBMI', 'lblSRBMIClassification');

    var bmiText = $("#lblSRBMIClassification").text();

    if (bmiText != null) {
        if (bmiText.toUpperCase().indexOf("OBESE") >= 0) {
            $("input:radio[name=NutritionGroup][value=941]").attr('checked', 'checked');
            $("input:radio[name=NutritionGroup]").iCheck('update');
        }
        else if (bmiText.toUpperCase().indexOf("OVERWEIGHT") >= 0) {
            $("input:radio[name=NutritionGroup][value=941]").attr('checked', 'checked');
            $("input:radio[name=NutritionGroup]").iCheck('update');
        }
        else if (bmiText.toUpperCase().indexOf("NORMAL") >= 0) {
            $("input:radio[name=NutritionGroup][value=939]").attr('checked', 'checked');
            $("input:radio[name=NutritionGroup]").iCheck('update');
        }

    }


    VisibilityZScoreSR();

    //$.hivce.loader('hide');
}

function WhoStageCheckBoxClick(ctrlName, isDelete) {
    debugger;
    var chk = $.grep(arrWHoStage, function (e) { return e == ctrlName.title; });

    if (jQuery.isEmptyObject(chk) == true) {
        arrWHoStage.push(ctrlName.title);
    }


    if (isDelete) {
        var cName = "WHOStage";
        if (ctrlName.title == "1") {
            cName = "dtlStageI";
        }
        else if (ctrlName.title == "2") {
            cName = "dtlStageII";
        }
        else if (ctrlName.title == "3") {
            cName = "dtlStageIII";
        }
        else if (ctrlName.title == "4") {
            cName = "dtlStageIV";
        }

        var isExists = false;
        var rows = $("#" + cName).dataTable().fnGetNodes();
        for (var i = 0; i < rows.length; i++) {
            // Get HTML of 3rd column (for example)
            var col0 = $(rows[i]).find("td:eq(0)").html();
            var hidV = $(col0).filter("#hidRowId").val();
            var chkVal = $("#chkAchieved" + hidV).iCheck('update')[0].checked;
            var col2 = $(rows[i]).find("td:eq(2)").html();
            var col3 = $(rows[i]).find("td:eq(3)").html();

            if (chkVal) {
                isExists = true;
            }
        }

        if (!isExists) {
            arrWHoStage = jQuery.grep(arrWHoStage, function (value) {
                return value != ctrlName.title;
            });
        }

    }

    if (arrWHoStage.length == 0) {
        var ddlValue = $.grep(arrCWHoStage, function (e) { return e.text == "N/A"; });
        if (jQuery.isEmptyObject(ddlValue) == false) {
            $("#ddlCurrentWhoStage").select2().val(ddlValue[0].id).trigger("change");
        }
    }
    else {
        var maxValue = Math.max.apply(Math, arrWHoStage);
        var ddlValue = $.grep(arrCWHoStage, function (e) { return e.text == maxValue; });
        if (jQuery.isEmptyObject(ddlValue) == false) {
            $("#ddlCurrentWhoStage").select2().val(ddlValue[0].id).trigger("change");
        }
    }

}


function GetZScoreDetailsSR() {
    activePage = getPageName() + '.aspx';

    //debugger;
    var postData;
    $.ajax({
        type: "POST",
        url: activePage + "?data=getzscore",
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
                zscoreDetails = response;
                CalcualteBMIzScoreSR();
            }
        },
        error: function (msg) {
        }
    });
}
function CalcualteBMIzScoreSR() {
    CalculateWeightZScoreSR();
    CalculateHeightZScoreSR();
    CalculateBMIZScoreSR();
    $.hivce.loader('hide');
}

function VisibilityZScoreSR() {
    var hidPAYM = $("#hidPAYM").val();

    if (hidPAYM >= 0 && hidPAYM <= 19) {

        if (hidPAYM >= 0 && hidPAYM <= 5) {
            $("#divWA1").css("visibility", "visible");
            $("#divWA1").css("display", "block");
            $("#divWAV1").css("visibility", "visible");
            $("#divWAV1").css("display", "block");
            $("#divHA1").css("visibility", "visible");
            $("#divHA1").css("display", "block");
            $("#divHAV1").css("visibility", "visible");
            $("#divHAV1").css("display", "block");
        } else {
            $("#divWA1").css("visibility", "hidden");
            $("#divWA1").css("display", "none");
            $("#divWAV1").css("visibility", "hidden");
            $("#divWAV1").css("display", "none");
            $("#divHA1").css("visibility", "hidden");
            $("#divHA1").css("display", "none");
            $("#divHAV1").css("visibility", "hidden");
            $("#divHAV1").css("display", "none");
        }

        $("#divBMIZ1").css("visibility", "visible");
        $("#divBMIZ1").css("display", "block");
        $("#divBMIZV1").css("visibility", "visible");
        $("#divBMIZV1").css("display", "block");

        $("#divSRBMILable").css("visibility", "hidden");
        $("#divSRBMILable").css("display", "none");
        $("#divSRBMIText").css("visibility", "hidden");
        $("#divSRBMIText").css("display", "none");
    }
    else {
        $("#divWA1").css("visibility", "hidden");
        $("#divWA1").css("display", "none");
        $("#divWAV1").css("visibility", "hidden");
        $("#divWAV1").css("display", "none");
        $("#divHA1").css("visibility", "hidden");
        $("#divHA1").css("display", "none");
        $("#divHAV1").css("visibility", "hidden");
        $("#divHAV1").css("display", "none");
        $("#divBMIZ1").css("visibility", "hidden");
        $("#divBMIZ1").css("display", "none");
        $("#divBMIZV1").css("visibility", "hidden");
        $("#divBMIZV1").css("display", "none");

        $("#divSRBMILable").css("visibility", "visible");
        $("#divSRBMILable").css("display", "block");
        $("#divSRBMIText").css("visibility", "visible");
        $("#divSRBMIText").css("display", "block");
    }

}



function AssignDataTableData(dataTableName, arrObject, arrFilterCriteria) {
    var dtData = $.grep(arrObject, function (e) { return e.FN == arrFilterCriteria; });
    var rows = $("#" + dataTableName).dataTable().fnGetNodes();
    for (var i = 0; i < rows.length; i++) {

        // Get HTML of 3rd column (for example)
        var col0 = $(rows[i]).find("td:eq(0)").html();
        var hidV = $(col0).filter("#hidRowId").val();
        var chkVal = $("#chkAchieved" + hidV).iCheck('update')[0].checked;
        var col2 = $(rows[i]).find("td:eq(2)").html();
        var col3 = $(rows[i]).find("td:eq(3)").html();

        var filterDate = $.grep(dtData, function (e) { return e.ValId == hidV; });
        if (jQuery.isEmptyObject(filterDate) == false) {
            $("#chkAchieved" + hidV).iCheck('check');
            CheckDatenAssign(filterDate[0].DF1, "dtCurrent" + hidV, false);
            CheckDatenAssign(filterDate[0].DF2, "dtHistory" + hidV, false);

        }
    }
}

function SaveSystemicReview() {

    $.hivce.loader('show');
    var systemicReviewDataData = PrepareSystemicReviewData();

    PostSystemicReviewData(systemicReviewDataData);
}

function PrepareSystemicReviewData() {


    rowSystemicReviewData = [];
    var rdoNutritionGroup = $("input:radio[name=NutritionGroup]:checked").val();
    var uNutritionGroup = (rdoNutritionGroup === undefined) ? 0 : rdoNutritionGroup;

    var rdoNutritionCounseling = $("input:radio[name=rdoNutritionCounseling]:checked").val();
    var uNutritionCounseling = (rdoNutritionCounseling === undefined) ? 0 : rdoNutritionCounseling;

    var arrDatatable = [];
    GetDataTableData("dtlStageI", arrDatatable, "WHOStageIConditions");
    GetDataTableData("dtlStageII", arrDatatable, "WHOStageIIConditions");
    GetDataTableData("dtlStageIII", arrDatatable, "WHOStageIIICoditions");
    GetDataTableData("dtlStageIV", arrDatatable, "WHOStageIVConditions");

    var ddlCurrentWhoStage = $("#ddlCurrentWhoStage").select2("val");
    if (ddlCurrentWhoStage == null) {
        ddlCurrentWhoStage = 0;
    }


    rowSystemicReviewData.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        GE: GetControlMultiSelectValues("ddlGeneralExamination"),
        Skin: GetControlMultiSelectValues("ddlSkin"),
        ENT: GetControlMultiSelectValues("ddlENT"),
        Chest: GetControlMultiSelectValues("ddlChest"),
        CVS: GetControlMultiSelectValues("ddlCVS"),
        Abdomen: GetControlMultiSelectValues("ddlAbdomen"),
        CNS: GetControlMultiSelectValues("ddlCNS"),
        RSC: $("#txtReviewSystemComments").val(),
        NA: uNutritionGroup,
        NAC: $("#txtAreaNutritionComment").val(),
        WSS: arrDatatable,
        WS: ddlCurrentWhoStage,
        NC: uNutritionCounseling,
        GEU: GetControlMultiSelectValues("ddlGenitoUrinary")
    });

    return rowSystemicReviewData[0];
}

function GetDataTableData(ctrlName, arrObject, DBName) {
    var rows = $("#" + ctrlName).dataTable().fnGetNodes();
    for (var i = 0; i < rows.length; i++) {

        //hidRowId 0
        //chkAchieved" + full["DId"] 0
        //dtCurrent" + full["DId"] 2
        //dtHistory" + full["DId"] 3

        // Get HTML of 3rd column (for example)
        var col0 = $(rows[i]).find("td:eq(0)").html();
        var hidV = $(col0).filter("#hidRowId").val();
        var chkVal = $("#chkAchieved" + hidV).iCheck('update')[0].checked;
        var col2 = $(rows[i]).find("td:eq(2)").html();
        var col3 = $(rows[i]).find("td:eq(3)").html();

        if (chkVal) {
            /*
            var col2 = $(rows[i]).find("td:eq(2)").html();
            var col3 = $(rows[i]).find("td:eq(3)").html();
            var CD = $(col0).filter("#dtCurrent").val();
            var HD = $(col0).filter("#dtHistory").val();
            */
            arrObject.push({ ValId: hidV
            , FN: DBName
            , DF1: GetDateData("dtCurrent" + hidV, true)
            , DF2: GetDateData("dtHistory" + hidV, true)
            });
        }
    }
}





function CheckSRBlankValues() {
    errorMsg = '';
    var errorField = '';

    var visitType = $("#ddlVisitType").select2("val");
    var visitTypeData = $("#ddlVisitType").select2('data')[0];


    if (visitType != null) {
        if (visitTypeData.text.toUpperCase().indexOf("SUPPORTER") == -1) {
            var ddlCurrentWhoStage = $("#ddlCurrentWhoStage").select2("val");

            if (ddlCurrentWhoStage == null) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'Current Who Stage ';
            }
        }
    }

    var ddlGeneralExamination = $("#ddlGeneralExamination").select2("val");

    if (ddlGeneralExamination == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'General Examination ';
    }

    var rdoNutritionGroup = $("input:radio[name=NutritionGroup]:checked").val();
    var uNutritionGroup = (rdoNutritionGroup === undefined) ? 0 : rdoNutritionGroup;

    if (uNutritionGroup == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Nutritional Assessment ';

    }
    if (isNaN(errorField)) {
        errorMsg = errorField + 'cannot be blank.';
    }

    return errorMsg;
}

function PostSystemicReviewData(rowData) {

    var action = "savesystemicreview";
    activePage = getPageName() + '.aspx';
    var rowData1 = rowData;
    var errorMsg = CheckSRBlankValues();
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
                //console.log(response);
                var responseSuccess = response.Success;
                if (responseSuccess == "true") {
                    GetSystematicReviewData();
                    customAlert("Systemic Review tab " + dataSuccessMessage.toLowerCase());
                    GetNextPage("tab_6");
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
}