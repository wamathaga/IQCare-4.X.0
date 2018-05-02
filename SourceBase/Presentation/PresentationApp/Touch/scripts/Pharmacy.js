function Validate(txt, txt1) {
    var txtbx = document.getElementById(txt);
    var txtbx1 = document.getElementById(txt1);
    var dis = Math.round(txtbx.value.replace(",", "."));
    var pre = Math.round(txtbx1.value.replace(",", "."));
    
    if (dis > pre) {
        alert("Dispense quantity cannot be greater than prescribed quantity");
        txtbx.value = '';
    }


}
function fnSetBMITouch(txtw, txth, txtbmi) {

    var weight = document.getElementById(txtw).value.replace(",", ".");
    var height = document.getElementById(txth).value.replace(",", ".");
    var txtbmi= document.getElementById(txtbmi);
    if (weight > 0 && height > 0) {
        var finalBmi = Math.sqrt((height * weight) / 3600);
        finalBmi = Math.round(finalBmi * 100) / 100
        txtbmi.value = finalBmi
    }
}

function CalculateTotalDailyDose(txtdose, txtdur, ddfr,txtpre) {
    var dose = document.getElementById(txtdose).value.replace(",", ".");
    var duration = document.getElementById(txtdur).value.replace(",", ".");
    var selText = document.getElementById(ddfr).value.replace(",", ".");
    var multiplier = 0;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.open("GET", "../../XMLFiles/Frequency.xml", false);
    xmlhttp.send();
    var xmlDoc = xmlhttp.responseXML;
    var x = xmlDoc.getElementsByTagName("NAME");
    for (i = 0; i < x.length; i++) {
        if (x[i].childNodes[0].nodeValue == selText)
        {
            multiplier = xmlDoc.getElementsByTagName("multiplier")[i].childNodes[0].nodeValue;
        }
    }
    if (selText != "0" && dose != "" && duration != "") {

        var TotalDose = dose * duration * multiplier;
        
        document.getElementById(txtpre).value = TotalDose;
    }
    else {
        document.getElementById(txtpre).value = "";
    }
}
