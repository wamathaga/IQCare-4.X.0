function fnSetBMI(txtw, txth, txtbmi) {

    var weight = document.getElementById(txtw).value.replace(",", ".");
    var height = document.getElementById(txth).value.replace(",", ".");
    var txtbmi = document.getElementById(txtbmi);
    if (weight > 0 && height > 0) {
        var finalBmi = weight / (height / 100 * height / 100)
        txtbmi.value = finalBmi
    }
}

function fnGetToggleText(btnClientID, depedentCtlClientID) {
    var button = $find(btnClientID);
    var combo = $find(depedentCtlClientID);
    var text = button.get_selectedToggleState().get_text();


    if (text == 'Yes') {
        combo.disable();
    }
    else {
        combo.enable();
    }
}

function fnGetComboOnchange(ComboClientID, depedentCtlClientID) {
    
    var combo = $find(ComboClientID);
    var txtBox = $find(depedentCtlClientID);
    var valuex = combo.get_text();
    
   // alert(valuex);
    if (valuex == 'Other') {
        txtBox.enable();
    }
    else {
        txtBox.disable();
    }
}

function OnClientClose(oWnd, args) {
           var arg = args.get_argument();
           if (arg) {

               if (arg.flagmode == 'VITALSIGN') 
               {
                   var RadTemperature = arg.RadTemperature;
                   var RadRespirationRate = arg.RadRespirationRate;
                   var RadHeartRate = arg.RadHeartRate;
                   var RadSystollicBloodPressure = arg.RadSystollicBloodPressure;
                   var RadDiastolicBloodPressure = arg.RadDiastolicBloodPressure;
                   var RadHeight = arg.RadHeight;
                   var RadWeight = arg.RadWeight;
                     

                   //alert(RadTemperature +'|'+RadRespirationRate+'|'+RadHeartRate+'|'+RadSystollicBloodPressure+'|'+RadDiastolicBloodPressure+'|'+RadHeight+'|'+RadWeight);
                   //               document.getElementById('IDfrmKNHExpress_txtRadVitalSign').value = 'Temperature:' + RadTemperature + ',RespirationRate:' + RadRespirationRate + 
                   //               ',HeartRate:'+RadHeartRate+',SystollicBloodPressure'+RadSystollicBloodPressure+',DiastolicBloodPressure'+RadDiastolicBloodPressure+
                   //               ',Height:' + RadHeight + ',Weight:' + RadWeight;
                   var tablecontaint = '<Table width="100%" cellspacing=1 cellpadding=1><tr><td>Temperature (Celsius):&nbsp;</td><td>' + RadTemperature + '</td><td>&nbsp;&nbsp;' +
               'RR (Bpm):&nbsp;</td><td>' + RadRespirationRate + '</td></tr><tr>&nbsp;&nbsp;<td>Heart Rate (Bpm):&nbsp;</td><td>' + RadHeartRate + '</td>&nbsp;&nbsp;<td>&nbsp;Systollic Blood Pressure mmHg:&nbsp;</td><td>' + RadSystollicBloodPressure + '</td></tr>' +
               '<tr><td>Diastolic Blood Pressure:&nbsp;</td><td>' + RadDiastolicBloodPressure + '</td><td>&nbsp;&nbsp;' +
               'Height (cms):&nbsp;</td><td>' + RadHeight + '</td></tr><tr>&nbsp;&nbsp;<td>Weight (kgs):&nbsp;</td><td>' + RadWeight + '</td></tr></table>';
                   //</table>'
                  

                   document.getElementById(arg.Form_Name + '_lblVitalSign').innerHTML = tablecontaint;
                   document.getElementById(arg.Form_Name + '_HiddRadTemperature').value = RadTemperature;
                   document.getElementById(arg.Form_Name + '_HiddRadRespirationRate').value = RadRespirationRate;
                   document.getElementById(arg.Form_Name + '_HiddRadHeartRate').value = RadHeartRate;
                   document.getElementById(arg.Form_Name + '_HiddRadSystollicBloodPressure').value = RadSystollicBloodPressure;
                   document.getElementById(arg.Form_Name + '_HiddRadDiastolicBloodPressure').value = RadDiastolicBloodPressure;
                   document.getElementById(arg.Form_Name + '_HiddRadHeight').value = RadHeight;
                   document.getElementById(arg.Form_Name + '_HiddRadWeight').value = RadWeight;

               }
               if (arg.flagmode == 'PC') 
               {

                   var selectedRowArray = arg.PcValue.split('#');
                   var tablePCHtml = '<Table width="90%" cellspacing=1 cellpadding=1 ><tr><td><b>Selected Item</b></td><td><b>Selected Text<b></td></tr>';
                   document.getElementById(arg.Form_Name + '_hiddpcValue').value = arg.PcValue;
                   
                   for (i = 0; i < selectedRowArray.length; i++) 
                   {
                       var selectedRowValue = selectedRowArray[i].split(',');
                       tablePCHtml = tablePCHtml + '<tr><td>' + selectedRowValue[1] + '</td><td>' + selectedRowValue[2] + '</td></tr>';

                   }
                   tablePCHtml = tablePCHtml + '</table>';
                   document.getElementById(arg.Form_Name + '_lblHtmlPcSelected').innerHTML = tablePCHtml;
                   
                   
                 
               }

               

           }

       }

