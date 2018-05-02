function InitProfileControls(response) {
    arrChronicDisorders = [];
    arrPreviousPregnancy = [];
    var todayDate = new Date();

    $("#dtVisitDate").datepicker({
        autoclose: true
    }).datepicker("setDate", "0");

    $("#dtLMP").datepicker({
        autoclose: true
    })//.datepicker("setDate", "0")
        .on('changeDate', function () {
            debugger;
            if (jQuery.isEmptyObject($('#dtLMP')) == false) {
                var lmpdt = $('#dtLMP').val();
                var EDDdate = new Date(Date.parse(lmpdt, "MM/dd/yyyy"));   //Get the Date object with actual date
                EDDdate.setDate(EDDdate.getDate() + 280); //Set date object adding 3 days.
                $("#dtEDD").datepicker("setDate", EDDdate);
                $("#hidDTEDD").val(EDDdate);
                fnCalculateGestation(lmpdt);

            }
        });
    $("#dtEDD").datepicker({
        autoclose: true
    });
    $("#dtCCOnsetDate").datepicker({
        autoclose: true,
        endDate: todayDate
    });
    $("#dtDateOfAllergen").datepicker({
        autoclose: true
    });


    $("#ddlChronicDisorder").select2();
    $("#ddlAllergenType").select2();
    $("#ddlSeverity").select2();
    $("#ddlAllergen").select2();
    $("#ddlFamilyHistoryOf").select2();
    $("#ddlMaturityweeks").select2();
    $("#ddlModeofDelivery").select2();
    $("#ddlGenderofBaby").select2();
    $("#ddlFateofBaby").select2();


    $("#chkChronicDisorders").bootstrapSwitch('state', false);
    $("#chkBloodTransfusionHistory").bootstrapSwitch('state', false);
    $("#chkDrugAllergies").bootstrapSwitch('state', false);

    $('#chkStillActive').on('ifChecked', function (event) {
        $(this).closest("input").attr('checked', true);
    });
    $('#chkStillActive').on('ifUnchecked', function (event) {
        $(this).closest("input").attr('checked', false);
    });

    $("#chkStillActive").iCheck({
        checkboxClass: 'icheckbox_flat-blue',
        radioClass: 'iradio_flat-blue'
    });

    $('input[type="checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
        DisableDiv(this);
    });

    $('#dtlChronicDisorders').dataTable().fnDestroy();
    ReInitialiseDatatable("CD");
    $('#dtlDrugAllergies').dataTable().fnDestroy();
    ReInitialiseDatatable("DA");
    $('#dtlPreviousPregnancy').dataTable().fnDestroy();
    ReInitialiseDatatable("PP");

    BindProfileControls(response);

}


function BindProfileControls(response) {
    debugger;
    var data = [];
    drugAllergen = [];
    envAllergen = [];
    foodAllergen = [];

    var collections = response.Collection;
    var vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "FieldVisitType"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlVisitType").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlVisitType").select2("val", "0");
    $("#ddlVisitType").trigger('change.select2');

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "Maturity"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlMaturityweeks").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlMaturityweeks").select2("val", "0");
    $("#ddlMaturityweeks").trigger('change.select2');

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "ModeOfDelivery"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlModeofDelivery").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlModeofDelivery").select2("val", "0");
    $("#ddlModeofDelivery").trigger('change.select2');

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "GenderOfBaby"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlGenderofBaby").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlGenderofBaby").select2("val", "0");
    $("#ddlGenderofBaby").trigger('change.select2');

    vdata = [];
    data = $.grep(collections, function (e) { return e.CNM == "FateOfBaby"; });
    $.each(data, function (index, value) {
        vdata.push({ id: value.Id, text: value.NM });
    });

    $("#ddlFateofBaby").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: vdata
    });
    $("#ddlFateofBaby").select2("val", "0");
    $("#ddlFateofBaby").trigger('change.select2');


    data = [];
    $.each(response.CC, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlChronicDisorder").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlChronicDisorder").select2("val", "0");
    $("#ddlChronicDisorder").trigger('change.select2');

    data = [];
    $.each(response.FHOF, function (index, value) {
        data.push({ id: value.DId, text: value.DN });
    });

    $("#ddlFamilyHistoryOf").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlFamilyHistoryOf").select2("val", "0");
    $("#ddlFamilyHistoryOf").trigger('change.select2');


    data = [];
    $.each(response.Severity, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlSeverity").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlSeverity").select2("val", "0");
    $("#ddlSeverity").trigger('change.select2');

    data = [];
    $.each(response.AT, function (index, value) {
        data.push({ id: value.Id, text: value.NM });
    });

    $("#ddlAllergenType").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: data
    });
    $("#ddlAllergenType").select2("val", "0");
    $("#ddlAllergenType").trigger('change.select2');

    $("#ddlAllergenType").select2().on('change', function () {
        BindAllergiesAllergen($(this).val());
    }).trigger('change');

    $.each(response.Allergen, function (index, value) {
        drugAllergen.push({ id: value.Id, text: value.NM });
    });
    $.each(response.ENVA, function (index, value) {
        envAllergen.push({ id: value.Id, text: value.NM });
    });
    $.each(response.FOODA, function (index, value) {
        foodAllergen.push({ id: value.Id, text: value.NM });
    });

    var selectList = $("#ddlAllergen");
    selectList.empty();
    $("#divtxtAller").css("visibility", "hidden");
    $("#divtxtAller").css("display", "none");
    $("#divddlAller").css("visibility", "visible");
    $("#divddlAller").css("display", "block");

    $("#ddlAllergen").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve',
        data: drugAllergen
    });
    $("#ddlAllergen").select2("val", "0");
    $("#ddlAllergen").trigger('change.select2');

    BindDBValues(response);
}

function BindDBValues(response) {

    $.each(response.CDT, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            var data = $.grep(response.CC, function (e) { return e.DId == arrD.ValId; });
            if (jQuery.isEmptyObject(data) == false) {
                var displayDate = "";
                var osd = arrD.OSD;
                if (jQuery.isEmptyObject(arrD.OSD) == false) {
                    // parse JSON formatted date to javascript date object
                    var dtOSD = new Date(parseInt(arrD.OSD.substr(6)));
                    // format display date (e.g. 04/10/2012)
                    displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");

                    /*var dt = new Date(parseInt(arrD.OSD.substr(6))).toString();
                    console.log(dt); // Mon Jul 16 2012 15:21:09 GMT+0530 (India Standard Time)
                    dt = dt.substr(0, dt.indexOf('GMT'));
                    console.log(dt); // Mon Jul 16 2012 15:21:09 
                    osd = convertToJSONDate(dt, true);*/
                }
                var IsA = 0;
                var IsAVal = "false";
                if (arrD.NF == "1") {
                    IsA = 1;
                    IsAVal = "true";
                }
                arrChronicDisorders.push({ PCID: arrD.ValId, PC: data[0].DN, OSD: arrD.OSD, IsActive: IsA, IsAVal: IsAVal, displayDate: displayDate, VId: arrD.VId });
            }
        }
    });
    BindDataTable(arrChronicDisorders, "divChronicDisorders", "chkChronicDisorders", "dtlChronicDisorders");

    CheckDatenAssign(response.OV.VD, "dtVisitDate", false);
    $("#hidVId").val(response.OV.VId);
    //debugger;
    $("#ddlVisitType").val(response.OV.TOV).trigger("change");

    $("#txtAgeAtMenarche").val(response.OBSGYN.Menarche);
    $("#txtParity").val(response.OBSGYN.Parity);
    $("#txtGravidae").val(response.OBSGYN.Gravidae);
    CheckDatenAssign(response.OBSGYN.LMP, "dtLMP", false);
    $("#hidDTEDD").val(response.OBSGYN.EDD);
    CheckDatenAssign(response.OBSGYN.EDD, "dtEDD", false);
    $("#txtPreviousAdmissions").val(response.SH);

    if (response.HBT == 1) {
        $("#chkBloodTransfusionHistory").bootstrapSwitch('state', true);
    }


    BindMultiSelectDropDown("ddlFamilyHistoryOf", response.SFHOF);


    arrDrugAllergies = [];
    $.each(response.AllergiesD, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            // parse JSON formatted date to javascript date object
            var dtOSD = new Date(parseInt(arrD.DAller.substr(6)));

            // format display date (e.g. 04/10/2012)
            var displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");
            var allerTID, allergenVal, allerDesc;

            if (arrD.ATD.trim() == "Other") {
                allergenVal = arrD.OAller;
            }
            else if (arrD.ATD.trim() == "Environmental") {
                var vdataENVA = $.grep(response.ENVA, function (e) { return e.Id == arrD.PID; });
                allerTID = arrD.Allergen;
                allergenVal = arrD.AllerDesc;
                allerDesc = arrD.AllerDesc;
            }
            else if (arrD.ATD.trim() == "Food") {
                var vdataFOODA = $.grep(response.FOODA, function (e) { return e.Id == arrD.PID; });
                allerTID = arrD.Allergen;
                allergenVal = arrD.AllerDesc;
                allerDesc = arrD.AllerDesc;
            }
            else {
                allergenVal = arrD.Allergen;
                allerDesc = arrD.AllerDesc;
                allerTID = "";
            }

            arrDrugAllergies.push({ ATID: arrD.ATID, ATD: arrD.ATD, Allergen: allergenVal, AllerDesc: allerDesc,
                AllerTID: allerTID,
                OAller: arrD.OAller,
                STID: arrD.STID,
                SDesc: arrD.SDesc,
                DAller: arrD.DAller,
                displayDAller: displayDate,
                TR: arrD.TR,
                Comment: arrD.Comment
                //,VId: arrD.VId
            });

        }
    });
    if (arrDrugAllergies.length > 0) {
        $("#chkDrugAllergies").bootstrapSwitch('state', true);
    }
    DrawDataTable("dtlDrugAllergies", arrDrugAllergies);



    $.each(response.Pregnancies, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            // parse JSON formatted date to javascript date object
            //var dtOSD = new Date(parseInt(arrD.DT.substr(6)));

            // format display date (e.g. 04/10/2012)
            //var displayDate = $.format.date(dtOSD, "dd-MMM-yyyy");

            arrPreviousPregnancy.push({ Id: arrD.Id,
                YOB: arrD.YOB,
                POD: arrD.POD,
                MID: arrD.MID,
                MName: arrD.MName,
                LH: arrD.LH,
                MODID: arrD.MODID,
                MOD: arrD.MOD,
                GID: arrD.GID,
                Gender: arrD.Gender,
                FID: arrD.FID,
                Fate: arrD.Fate,
                VId: arrD.VId,
                BW: arrD.BW
            });

            /*
            {"Id":20,"Ptn_Pk":801,"VId":0,
            "YOB":1111,"POD":"1","MID":401,
            "MName":"Term","LH":1.00,
            "MODID":263,"MOD":"Normal SVD",
            "GID":265,"Gender":"Male",
            "FID":267,"Fate":"Live Birth",
            "CD":"\/Date(1519817266000+0530)\/",
            "UD":"\/Date(1519817266000+0530)\/",
            "UId":1}
            */


        }
    });
    DrawDataTable("dtlPreviousPregnancy", arrPreviousPregnancy);


    $.hivce.loader('hide');
}

function fnCalculateGestation(date1) {
    var one_day = 1000 * 60 * 60 * 24;    //Get 1 day in milliseconds
    // Convert both dates to milliseconds
    var date1_ms = Date.parse(date1);
    var date2_ms = new Date().getTime();
    // Calculate the difference in milliseconds  
    var difference_ms = date2_ms - date1_ms;
    // Convert back to days and return 
    var gestationInDays = Math.round(difference_ms / one_day);
    var gestationInWeeks = Math.round(gestationInDays / 7);
    var remainder = gestationInDays % 7;
    var textValue = gestationInWeeks + ' weeks and ' + remainder + ' days';
    //document.getElementById("<%=txtGestation.ClientID%>").value = textValue;
    $("#txtGestation").val(textValue);
}


function BindAllergiesAllergen(val) {
    debugger;
    var data = [];
    data.push({ id: "", text: "" });
    var selectList = $("#ddlAllergen");
    selectList.empty();
    $("#txtAllergenOther").val("");


    $("#ddlAllergen").select2({
        placeholder: {
            id: '0', // the value of the option
            text: 'Select an option'
        },
        allowClear: true,
        width: 'resolve'
    });
    $("#ddlAllergen").select2("val", "0");
    $("#ddlAllergen").trigger('change.select2');

    if (val == 207 || val == null) {
        $("#divtxtAller").css("visibility", "hidden");
        $("#divtxtAller").css("display", "none");
        $("#divddlAller").css("visibility", "visible");
        $("#divddlAller").css("display", "block");

        $("#ddlAllergen").select2({
            placeholder: {
                id: '0', // the value of the option
                text: 'Select an option'
            },
            allowClear: true,
            width: 'resolve',
            data: drugAllergen
        });
        $("#ddlAllergen").select2("val", "0");
        $("#ddlAllergen").trigger('change.select2');
    }
    else if (val == 208) {
        $("#divtxtAller").css("visibility", "hidden");
        $("#divtxtAller").css("display", "none");
        $("#divddlAller").css("visibility", "visible");
        $("#divddlAller").css("display", "block");

        $("#ddlAllergen").select2({
            placeholder: {
                id: '0', // the value of the option
                text: 'Select an option'
            },
            allowClear: true,
            width: 'resolve',
            data: envAllergen
        });
        $("#ddlAllergen").select2("val", "0");
        $("#ddlAllergen").trigger('change.select2');
    }
    else if (val == 209) {
        $("#divtxtAller").css("visibility", "hidden");
        $("#divtxtAller").css("display", "none");
        $("#divddlAller").css("visibility", "visible");
        $("#divddlAller").css("display", "block");

        $("#ddlAllergen").select2({
            placeholder: {
                id: '0', // the value of the option
                text: 'Select an option'
            },
            allowClear: true,
            width: 'resolve',
            data: foodAllergen
        });
        $("#ddlAllergen").select2("val", "0");
        $("#ddlAllergen").trigger('change.select2');
    }
    else if (val == 211) {
        $("#divtxtAller").css("visibility", "visible");
        $("#divtxtAller").css("display", "block");
        $("#divddlAller").css("visibility", "hidden");
        $("#divddlAller").css("display", "none");

    }
    else {
        $("#divtxtAller").css("visibility", "hidden");
        $("#divtxtAller").css("display", "none");
        $("#divddlAller").css("visibility", "visible");
        $("#divddlAller").css("display", "block");

        $("#ddlAllergen").select2({
            placeholder: {
                id: '0', // the value of the option
                text: 'Select an option'
            },
            allowClear: true,
            width: 'resolve',
            data: drugAllergen
        });
        $("#ddlAllergen").select2("val", "0");
        $("#ddlAllergen").trigger('change.select2');
    }
}

function AddChronicDisorder() {
    var ddlPC = $("#ddlChronicDisorder").select2("val");
    var data = $("#ddlChronicDisorder").select2('data')[0];
    var dtOnsetDate = GetDateData("dtCCOnsetDate", true);


    if (ddlPC == null) {
        customAlert("Please select chronic disorder from the list");
        return;
    }

    if ($("#dtCCOnsetDate").val().length == 0) {
        customAlert("Please enter on set date");
        return;
    }
    debugger;
    var chkData = false;
    //    $.grep(arrChronicDisorders, function (e) {
    //        return e.PCID == data.id && e.OSD == dtOnsetDate;
    //    });

    $.each(arrChronicDisorders, function (index, arrD) {
        if (jQuery.isEmptyObject(arrD) == false) {
            if (jQuery.isEmptyObject(arrD.OSD) == false) {
                var dt = new Date(parseInt(arrD.OSD.substr(6))).toString();
                //console.log(dt); // Mon Jul 16 2012 15:21:09 GMT+0530 (India Standard Time)
                dt = dt.substr(0, dt.indexOf('GMT'));
                //console.log(dt); // Mon Jul 16 2012 15:21:09 
                osd = convertToJSONDate(dt, true);

                var dtOSD = new Date(parseInt(osd.substr(6)));
                dtOSD.setHours(0, 0, 0, 0);
                var dt = new Date(parseInt(dtOnsetDate.substr(6)));
                dt.setHours(0, 0, 0, 0);
                if (arrD.PCID == data.id) {
                    if (dtOSD.getTime() === dt.getTime()) {
                        chkData = true;
                    }
                }
            }
        }
    });

    if (chkData == false) {
        //chkStillActive
        var IsA = 0;
        var IsAVal = "false";
        if ($('#chkStillActive').is(":checked")) {
            IsA = 1;
            IsAVal = "true";
        }
        arrChronicDisorders.push({ PCID: data.id, PC: data.text, OSD: dtOnsetDate, IsActive: IsA, IsAVal: IsAVal, displayDate: $("#dtCCOnsetDate").val(), VId: 0 });
        //arrChronicDisordersUI.push([data.text, $("#dtCCOnsetDate").val(), IsAVal, data.id]);
    }
    else {
        customAlert("Already exits in the table.");
    }

    DrawDataTable("dtlChronicDisorders", arrChronicDisorders);
}

function AddDrugAllergies() {
    var ddlAllergenType = $("#ddlAllergenType").select2("val");
    var data = $("#ddlAllergenType").select2('data')[0];
    var ddlAllergen = $("#ddlAllergen").select2("val");
    var data1 = $("#ddlAllergen").select2('data')[0];
    var ddlSeverity = $("#ddlSeverity").select2("val");
    var data2 = $("#ddlSeverity").select2('data')[0];
    var dtDateOfAllergen = GetDateData("dtDateOfAllergen", true);
    var txtAllergenOther = $("#txtAllergenOther").val();
    var txtTypeofReaction = $("#txtTypeofReaction").val();
    var txtAllergenComment = $("#txtAllergenComment").val();

    var errorField = '';
    var allergenVal = "";
    var allerTID = 0;

    if (ddlAllergenType == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Allergen Type ';
    }
    if (data != undefined) {
        if (data.text == "Other") {
            if (txtAllergenOther.length == 0) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'Allergen ';
            }
        }
        else {
            if (ddlAllergen == null) {
                if (errorField.length > 1) {
                    errorField += ', ';
                }
                errorField += 'Allergen ';
            }
        }
    }
    else {
        if (ddlAllergen == null) {
            if (errorField.length > 1) {
                errorField += ', ';
            }
            errorField += 'Allergen ';
        }
    }
    if (data1 != undefined) {
        allerTID = data1.id;
    }
    if (ddlSeverity == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Severity ';
    }

    if ($("#dtDateOfAllergen").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Date Of Allergen ';
    }

    if ($("#txtTypeofReaction").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Reaction Type ';
    }

    if ($("#txtAllergenComment").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Comment ';
    }

    if (isNaN(errorField)) {
        customAlert(errorField + 'cannot be blank.');
        return;
    }


    if (data.text == "Other") {
        allergenVal = txtAllergenOther;
    }
    else {
        allergenVal = data1.text;
    }

    var chkData = $.grep(arrDrugAllergies, function (e) { return e.ATID == data.id && e.AllerDesc == allergenVal; });

    if (jQuery.isEmptyObject(chkData) == true) {
        arrDrugAllergies.push({ ATID: data.id, ATD: data.text, Allergen: allergenVal, AllerDesc: allergenVal,
            AllerTID: allerTID,
            OAller: txtAllergenOther,
            STID: data2.id,
            SDesc: data2.text,
            DAller: dtDateOfAllergen,
            displayDAller: $("#dtDateOfAllergen").val(),
            TR: txtTypeofReaction,
            Comment: txtAllergenComment
            //,VId: 0
        });
    }
    else {
        customAlert("Already exits in the table.");
    }
    DrawDataTable("dtlDrugAllergies", arrDrugAllergies);

}

function AddPreviousPregnancy() {
    var ddlMaturityweeks = $("#ddlMaturityweeks").select2("val");
    var data = $("#ddlMaturityweeks").select2('data')[0];

    var ddlModeofDelivery = $("#ddlModeofDelivery").select2("val");
    var data1 = $("#ddlModeofDelivery").select2('data')[0];
    var ddlGenderofBaby = $("#ddlGenderofBaby").select2("val");
    var data2 = $("#ddlGenderofBaby").select2('data')[0];
    var ddlFateofBaby = $("#ddlFateofBaby").select2("val");
    var data3 = $("#ddlFateofBaby").select2('data')[0];


    var txtBirthWeight = $("#txtBirthWeight").val();


    var txtYrofDelivery = $("#txtYrofDelivery").val();
    var txtPlaceofDelivery = $("#txtPlaceofDelivery").val();
    var txtLabourduratioin = $("#txtLabourduratioin").val();

    var errorField = '';


    if (ddlMaturityweeks == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Maturity weeks ';
    }

    if ($("#txtYrofDelivery").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Year of Delivery ';
    }

    if ($("#txtPlaceofDelivery").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Place of Delivery ';
    }

    if (isNaN(errorField)) {
        customAlert(errorField + 'cannot be blank.');
        return;
    }

    //var chkData = $.grep(arrDrugAllergies, function (e) { return e.ATID == data.id && e.AllerDesc == allergenVal; });

    //if (jQuery.isEmptyObject(chkData) == true) {

    arrPreviousPregnancy.push({ Id: 0,
        YOB: txtYrofDelivery,
        POD: txtPlaceofDelivery,
        MID: data.id,
        MName: data.text,
        LH: txtLabourduratioin,
        MODID: data1.id,
        MOD: data1.text,
        GID: data2.id,
        Gender: data2.text,
        FID: data3.id,
        Fate: data3.text,
        BW: txtBirthWeight
    });

    //}
    //else {
    //    customAlert("Already exits in the table.");
    //}
    DrawDataTable("dtlPreviousPregnancy", arrPreviousPregnancy);

}

function DeletePPRow(yob, pod, mid, vid, from) {
    debugger;
    if (from == 'PP') {
        //$('#dtlPreviousPregnancy').dataTable().fnDestroy();

        //ReInitialiseDatatable(from);
        $.each(arrPreviousPregnancy, function (i, el) {
            if (this.YOB == yob && this.POD == pod && this.MID == mid) {
                //debugger;
                //Console.log(el);
                arrPreviousPregnancy.splice(i, 1);
            }
        });

        var table = $("#dtlPreviousPregnancy").DataTable();
        table.rows().remove().draw();

        DrawDataTable("dtlPreviousPregnancy", arrPreviousPregnancy);

    }

}

function DeleteRow(id, vid, from) {
    if (from == 'CD') {
        $('#dtlChronicDisorders').dataTable().fnDestroy();

        ReInitialiseDatatable(from);
        $.each(arrChronicDisorders, function (i, el) {
            if (this.PCID == id && this.VId == vid) {
                arrChronicDisorders.splice(i, 1);
            }
        });

        var table = $("#dtlChronicDisorders").DataTable();
        table.rows().remove().draw();

        BindDataTable(arrChronicDisorders, "divChronicDisorders", "chkChronicDisorders", "dtlChronicDisorders");

    }
    else if (from == 'DA') {
        //$('#dtlDrugAllergies').dataTable().fnDestroy();

        //ReInitialiseDatatable(from);

        $.each(arrDrugAllergies, function (i, el) {
            if (this.ATID == id) {
                arrDrugAllergies.splice(i, 1);
            }
        });
        var table = $("#dtlDrugAllergies").DataTable();
        table.rows().remove().draw();

        DrawDataTable("dtlDrugAllergies", arrDrugAllergies);
    }
}

function BindDataTable(dtRows, disDiv, switchName, dtTable) {

    if (dtRows.length > 0) {
        $("#" + switchName).bootstrapSwitch('state', true);
        var ctrlVal = $("#" + switchName).bootstrapSwitch('state');
        $("#" + disDiv).removeAttr("style");
        if (ctrlVal) {
            $("#" + disDiv).css("visibility", "visible");
            $("#" + disDiv).css("display", "block");
        }
        else {
            $("#" + disDiv).css("visibility", "hidden");
            $("#" + disDiv).css("display", "none");
        }
        DrawDataTable(dtTable, dtRows);
    }
}

function SaveProfileData() {

    activePage = getPageName() + '.aspx';
    var action = "saveprofile";

    var rowprofileData = PrepareProfileData();
    var errorMsg = CheckProfileBlankValues();

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
            data: JSON.stringify(rowprofileData),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            beforeSend: function () {
            },
            success: function (response) {
                //console.log(response);
                var responseSuccess = response.Success;
                if (responseSuccess == "true") {
                    //GetPresentingComplaintsData();
                    customAlert("Profile tab " + dataSuccessMessage.toLowerCase());
                    GetNextPage("tab_2");
                }
                else {
                    if (responseSuccess == "false") {
                        customAlert(errorMessage);
                    }
                    else {
                        customAlert(responseSuccess);
                        GetNextPage("tab_2");
                    }
                }

                //$.eva.loader('hide');
            },
            error: function (msg) {
                //ShowErrorMessage(msg);
            }
        });
    }

}

function CheckProfileBlankValues() {
    var ddlCtrl = $("#ddlVisitType").select2("val");
    var data = $("#ddlVisitType").select2('data')[0];
    errorMsg = '';
    var errorField = '';

    if ($("#dtVisitDate").val().length == 0) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Visit date ';
    }

    if (ddlCtrl == null) {
        if (errorField.length > 1) {
            errorField += ', ';
        }
        errorField += 'Visit type ';
    }

    if (isNaN(errorField)) {
        errorMsg = errorField + 'cannot be blank.';
    }
    return errorMsg;
}

function PrepareProfileData() {
    var localdtformat = true;
    var txtGestation = $("#txtGestation").val();
    if (txtGestation.length == 0) {
        txtGestation = 0;
    }
    rowData = [];
    rowData.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        OV: GetOVData(localdtformat),
        OBSGYN: GetOBSGYNData(),
        CDT: GetCDTData(),
        SH: $("#txtPreviousAdmissions").val(),
        HBT: GetSwitchValue("chkBloodTransfusionHistory"),
        SFHOF: GetControlMultiSelectValues("ddlFamilyHistoryOf"),
        Pregnancies: GetPreviousPregnancy(),
        AllerHF: GetSwitchValue("chkDrugAllergies"),
        AllerH: GetAllergies(),
        GestAge: txtGestation
    });

    console.log(rowData);
    return rowData[0];
}


function GetOVData(localdtformat) {
    rowData3 = [];
    var ddlCtrl = $("#ddlVisitType").select2("val");

    if (ddlCtrl == null) {
        ddlCtrl = 0;
    }
    rowData3.push({
        VId: 0, Ptn_Pk: 0, LId: 0,
        VD: GetDateData("dtVisitDate", localdtformat),
        VTId: ddlCtrl
    });
    return rowData3[0];
}

function GetOBSGYNData() {
    var txtParity = $("#txtParity").val();
    if (txtParity.length == 0) {
        txtParity = 0;
    }

    var txtGravidae = $("#txtGravidae").val();
    if (txtGravidae.length == 0) {
        txtGravidae = 0;
    }

    rowData1 = [];
    rowData1.push({ Ptn_Pk: 0,
        LId: 0,
        VId: 0,
        Menarche: $("#txtAgeAtMenarche").val(),
        EDD: GetDateData("dtEDD", true),
        Gravidae: txtGravidae,
        LMP: GetDateData("dtLMP", true),
        Parity: txtParity
    });
    return rowData1[0];
}

function GetCDTData() {
    rowData1 = [];
    if (jQuery.isEmptyObject(arrChronicDisorders) == false) {
        $.each(arrChronicDisorders, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {
                if (arrD.VId == 0 || arrD.VId == $("#hidVId").val()) {
                    rowData1.push({ Ptn_Pk: 0, VId: arrD.VId, ValId: arrD.PCID, FN: "ChronicCondition", OSD: arrD.OSD, NF: arrD.IsActive });
                }
            }
        });
    }
    return rowData1;
}

function GetAllergies() {
    var rowDataAllergy = [];
    if (jQuery.isEmptyObject(arrDrugAllergies) == false) {
        $.each(arrDrugAllergies, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {

                rowDataAllergy.push({ ATID: arrD.ATID, ATD: arrD.ATD, Allergen: arrD.Allergen,
                    AllerDesc: arrD.AllerDesc, AllerTID: arrD.AllerTID, OAller: arrD.OAller,
                    STID: arrD.STID,
                    SDesc: arrD.SDesc,
                    DAller: arrD.DAller,
                    displayDAller: arrD.displayDAller,
                    TR: arrD.TR,
                    Comment: arrD.Comment
                    //,VId: arrD.VId
                });
            }
        });

    }
    return rowDataAllergy;
}

function GetPreviousPregnancy() {
    var rowDataPP = [];
    if (jQuery.isEmptyObject(arrPreviousPregnancy) == false) {
        $.each(arrPreviousPregnancy, function (index, arrD) {
            if (jQuery.isEmptyObject(arrD) == false) {

                rowDataPP.push({ Id: arrD.Id,
                    YOB: arrD.YOB,
                    POD: arrD.POD,
                    MID: arrD.MID,
                    MName: arrD.MName,
                    LH: arrD.LH,
                    MODID: arrD.MODID,
                    MOD: arrD.MOD,
                    GID: arrD.GID,
                    Gender: arrD.Gender,
                    FID: arrD.FID,
                    Fate: arrD.Fate,
                    BW: arrD.BW
                });
            }
        });

    }
    return rowDataPP;
}


function DeleteForm() {
    //alert(GetParameterValues('name'));
    $.hivce.loader('show');
    var formName = '';
    activePage = getPageName() + '.aspx';

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