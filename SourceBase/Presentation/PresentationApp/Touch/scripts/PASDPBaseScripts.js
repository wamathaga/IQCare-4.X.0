function PatientHome_Load() {
    //load up the status pane
    $("#extruderLeft").buildMbExtruder({
        position: "left",
        width: 245,
        extruderOpacity: .8,
        hidePanelsOnClose: false,
        accordionPanels: false,
        onExtOpen: function () { },
        onExtContentLoad: function () { $("#extruderLeft").openPanel(); },
        onExtClose: function () { }
    });

    //put it into closed mode at page start
    $("#extruderLeft").closeMbExtruder();

    //fade in on load
    $("body").css("display", "none");
    $("body").fadeIn(500);


    /*-- Accordion for the find/add window --*/
    //InIEvent();

    resizeScrollbars();
}

/* This maintains the request for the accordian in the RadWindow */
// Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
/* END */

/* to fade out to Facility PAge and choose the mode */
function GoToFacility(Mode, IsFormLoaded) {

    if (IsFormLoaded == 'Loaded') {

        var theAnswer = window.confirm("Are you sure you want leave without saving this form?");
        if (theAnswer) {
            $('body').fadeOut(500, function () {
                document.location.href = "frmTouchFacilityHome.aspx?Mode=" + Mode;
            });
        }
        else {
            args.set_cancel(true);
            return false;
        }

    } else {

        $('body').fadeOut(500, function () {
            document.location.href = "frmTouchFacilityHome.aspx?Mode=" + Mode;
        });

    }
}



function BackToMain() {
    $(function () {
        CloseLoading();
        $('#middlePane').scrollTo($('#phMenu'), 800);
        $("#extruderLeft").closeMbExtruder();
        $("#divBack").css("display", "none");
        $("#divMore").css("display", "block");
        
    });
    //$("#extruderLeft").openMbExtruder(true);
    return false;
}

function OpenFindAdd(theWindow) {
    var oWnd = $find(theWindow);
    oWnd.show();
}

function InIEvent() {
    //if ($("#dialog-addPatient").is(":visible")) {
    // $("#jAccordion").accordion({
    //     heightStyle: "content",
    //     active: 1
    //    });
    //} else {
    //    $("#jAccordion").accordion({
    //        heightStyle: "content"
    //    });
    //}

    $("#jAccordion").accordion({
        heightStyle: "content",
        active: false
    });
    $("#jAccordion").accordion("option", "active", 0);
    $("#rwFindAdd_C_divAddHdr").click();
    $("#ui-accordion-jAccordion-header-0").click();
}
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);

function ShowMore(sender, eventArgs) {
    var substr = eventArgs._commandArgument.toString().split('|')
    ShowHide(substr[0], substr[1]);
}

function ShowMoreMenu(theDiv) {
    //if ($("#" + theDiv).is(":visible"))
    $("#" + theDiv).show('size', { origin: ["top", "left"] }, 400);
    //else
    //  $("#" + theDiv).hide().slideDown("slow");
}
function HideMoreMenu(theDiv) {
    $("#" + theDiv).hide('size', { origin: ["top", "left"] }, 400);
}


function ShowGraphs(theDiv) {
    $("#" + theDiv).toggle('size', { origin: ["bottom", "left"] }, 400);
    event.stopPropagation();
}

function ShowMoreMulti(sender, eventArgs) {
    var substr = eventArgs.get_item().get_value().toString().split('|');

    var suffixPOS = sender.get_id().lastIndexOf("_");
    if (suffixPOS < 1) {
        var wrapperDiv = "wrapper_" + sender.get_id();
    } else {
        var wrapperDiv = "wrapper" + sender.get_id().substring(suffixPOS);
    }
    //var wrapperDiv = sender.get_id().replace("wrapper_", "");
    var childrenToShow = new Array(); var counter = 0;
    $('#' + wrapperDiv).find('div').each(function () {

        var innerDivId = $(this).attr('id');
        if (innerDivId != null) {
            if ((innerDivId.lastIndexOf("_") < 0) && (innerDivId.lastIndexOf("rcb") < 0)) {
                if (substr.length > 1) {
                    if (innerDivId == substr[1]) {
                        ShowHide(innerDivId, "show");
                        //$('#' + innerDivId).find('div').each(function () {
                        //    //ShowHide($(this).attr('id'), "show");
                        //    //childrenToShow[counter] = $(this).attr('id'); counter++;
                        //});
                    }
                    else {
                        ShowHide(innerDivId, "hide");
                    }
                } else {
                    ShowHide(innerDivId, "hide");
                }
            }
        }
    });
    //for (var i = 0; i < childrenToShow.length; i++) {
    //    ShowHide(childrenToShow[i], "show");
    //}
    if (substr[2] != null)
        $("#" + substr[2]).focus();
}
function ShowMoreSingle(sender, eventArgs) {
    var substr = eventArgs.get_item().get_value().toString().split('|');
    ShowMoreSingleFunc(substr);
}
function ShowMoreSingleFunc(substr) {
    if (substr.length > 1)
        if (substr[2] != null)
            if (substr[2] == "hide")
                ShowHide(substr[1], "hide");
            else
                ShowHide(substr[1], "show", substr[3]);
        else
            ShowHide(substr[1], "hide");
    else
        ShowHide(substr[1], "hide");


}

// Using Class name instead of ID for divs that require the same criteria as name
function ShowMoreMultiBeta(sender, eventArgs) {
    //parent.ShowLoading()

    var substr = eventArgs.get_item().get_value().toString().split('|');

    var suffixPOS = sender.get_id().lastIndexOf("_");
    if (suffixPOS < 1) {
        var wrapperDiv = "wrapper_" + sender.get_id();
    } else {
        var wrapperDiv = "wrapper" + sender.get_id().substring(suffixPOS);
    }
    //var wrapperDiv = "wrapper" + sender.get_id().substring(suffixPOS);
    //var wrapperDiv = sender.get_id().replace("wrapper_", "");

    $('.' + wrapperDiv).find('div').each(function () {

        if ($(this).attr('class') !== undefined) {
            var innerDivId = $(this).attr('class');
            if (innerDivId != null) {
                if (innerDivId.lastIndexOf("_") < 0) {
                    if (substr.length > 1) {
                        if (innerDivId == substr[1])
                            ShowHideBeta(innerDivId, "show");
                        else
                            ShowHideBeta(innerDivId, "hide");
                    } else {
                        ShowHideBeta(innerDivId, "hide");
                    }
                }
            }
        }
    });
    //parent.CloseLoading()
}
function ShowHideBeta(theDiv, YN) {
    if (YN == "show") {
        $("." + theDiv).slideDown("slow", function () {
            complete: resizeScrollbars();
        });
    } else if (YN == "hide") {
        $("." + theDiv).slideUp("slow", function () {
            complete: resizeScrollbars();
        });
    }
}

// End of beta

function ShowHide(theDiv, YN, theFocus) {
    if (YN == "show") {
        $("#" + theDiv).slideDown("slow", function () {
            complete: focusAndLayout(theFocus)
        });
    } else if (YN == "hide") {
        $("#" + theDiv).slideUp("slow", function () {
            complete: focusAndLayout(theFocus)
        });
    }
}

function ShowHideClass(theDivClass, YN, theFocus) {
    if (YN == "show") {
        $("." + theDivClass).slideDown("slow", function () {
            complete: focusAndLayout(theFocus)
        });
    } else if (YN == "hide") {
        $("." + theDivClass).slideUp("slow", function () {
            complete: focusAndLayout(theFocus)
        });
    }
}

function focusAndLayout(theFocus) {
    resizeScrollbars();
    if (theFocus != null)
        $("#" + theFocus.toString()).focus();

}
function ShowHideadditional(s, e) {
    var theVals = e._commandArgument.toString().split('|');
    $elem = null;
    $elem = $("#" + theVals[1]).clone(); ;
    var num = Math.floor((Math.random() * 1000) + 1);
    $elem.attr("id", "newId" + num.toString());
    var theId = $elem.attr("id");
    $elem.appendTo("#" + theVals[0])
    $elem.slideDown("slow", function () {
        complete: resizeScrollbars();
    });
}
function OpenModal(button, args) {
    var oWnd = $find("allmodalsControl_" + args._commandArgument.toString());
    oWnd.show();
    return false;
}

function OpenModalFromClient(TheWindow) {
    var oWnd = $find("allmodalsControl_" + TheWindow);
    oWnd.show();
    return false;
}

function CloseModalFromClient(TheWindow) {
    var oWnd = $find("allmodalsControl_" + TheWindow);
    if (oWnd != null)
        oWnd.close();
    return true;
}

function OpenModalASPX(button, args) {
    var oWnd = $find(args._commandArgument.toString());
    if (oWnd != null)
        oWnd.show();
    return false;
}

function CloseModalASPX(TheWindow) {
    var oWnd = $find(TheWindow);
    if (oWnd != null)
        oWnd.close();
    return true;
}

function setTabs() {
    var st = $("input[name*='hidtab']").val(); //  $find("input[id*='hidtab']").val();
    if (st == null)
        st = 0;
    $("#tabs").tabs(
        {
            activate: function (event, ui) {
                var selectedTab = $('#tabs').tabs('option', 'active');
                //$("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
                $("input[name*='hidtab']").val(selectedTab)

                resizeScrollbars();
            },
            selected: st

            //,
            //active: $('#hidtab').val()
        });
    $(function () {
        resizeScrollbars();
        $('.jspContainer').css("width", "855px");
        $('.tabwidth').css("width", "855px");

        //$('#tabs').tabs({ selected: st });
    });
    $("#tabs").tabs("option", "active", st);
}

function resizeScrollbars() {
    $('.scroll-pane').jScrollPane({ showArrows: true, verticalArrowPositions: 'split' });
    $('.jspContainer').css("width", "855px");
    $('.tabwidth').css("width", "855px");

}
function GoToPanePos(YPos) {
    $('.jspContainer').scrollTo(YPos, 0)
    resizeScrollbars()
}
function GoToElem(theElem) {
    var element = $('.scroll-pane').jScrollPane({ showArrows: true, verticalArrowPositions: 'split' });
    $('.jspContainer').css("width", "855px");
    $('.tabwidth').css("width", "855px");
    var api = element.data('jsp');
    api.scrollToElement('#' + theElem, true, true);
}
/*///////////////////////////////*/
/*    History Form Specific      */
/*//////////////////////////////*/

function drugcombo(sender, eventArgs) {
    var item = eventArgs.get_item();
    ShowHide("ShowIfChildReceivedYes");
}

/*/////*/
/* End */
/*/////*/

// Onblur methods for validation */

function OnBlur(sender, args) {
    if (sender.get_value() == "") {
        sender.get_styles().EnabledStyle[0] += "background-color: #FCB514;";
        sender.updateCssClass();
    } else {
        sender.get_styles().EnabledStyle[0] += "background-color: #FFFFCC;";
        sender.updateCssClass();
    }
}


function RadDatePicker_SetMaxDateToCurrentDate(sender, args) {
    var date = new Date();
    var arr = new Array(date.getFullYear(), date.getMonth() + 1, date.getDate());
    sender.set_rangeMaxDate(arr);
}

function OnBlurDateP(sender, args) {
    if (sender.get_selectedDate() == null) {
        sender.get_dateInput().get_styles().EnabledStyle[0] += "background-color: #FCB514;";
        sender.get_dateInput().updateCssClass();
    } else {
        sender.get_dateInput().get_styles().EnabledStyle[0] += "background-color: #FFFFCC;";
        sender.get_dateInput().updateCssClass();
    }
}

//override the click function when needed
var stop_prop = function (obj) {
    obj.click(function (e) {
        e.stopPropagation();
    });
}

/* End */

/* handle the toggle button null state */
var ischecked;
function up(radiobutton) {
    var button = $find(radiobutton.childNodes[0].id.replace("_input", ""));
    ischecked = button.get_checked();
} 
function down(radiobutton, args) 
{
    if (ischecked) {
        radiobutton.set_checked(false);
       // button.set_selectedToggleStateIndex(-1);
        //button.clearCheckedRadios("Scheduled");
        ischecked = false;
    } 
    else 
    {
        ischecked = true;
    }
}

function ChangeToggle(thebut) {
    var button = $find(thebut);
    button.set_checked(false);
    button.set_selectedToggleStateIndex(-1);
    button.clearCheckedRadios("Scheduled");
}
/* End */







//	    $(function () {
//	        if (self.location.href == top.location.href) {
//	            $("body").css({ font: "normal 13px/16px 'trebuchet MS', verdana, sans-serif" });
//	        }
//	        $("#extruderLeft").buildMbExtruder({
//	            position: "left",
//	            width: 300,
//	            extruderOpacity: .8,

//	            hidePanelsOnClose: false,
//	            accordionPanels: false,
//	            onExtOpen: function () { },
//	            onExtContentLoad: function () { $("#extruderLeft").openPanel(); },
//	            onExtClose: function () { }
//	        });
//	        $.fn.changeLabel = function (text) {
//	            $(this).find(".flapLabel").html(text);
//	            $(this).find(".flapLabel").mbFlipText();
//	        };

//	    });
