<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="Scheduler_frmScheduler_ContactCareTracking" Title="Untitled Page" EnableViewState="true"
    CodeBehind="frmScheduler_ContactCareTracking.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<style type="text/css">
.udform-control   
{
  width: 100%;
  height: 34px;
  padding: 6px 12px;
  font-size: 14px;
  line-height: 1.42857143;
  color: #555;
  background-color: #fff;
  background-image: none;
  border: 1px solid #ccc;
  border-radius: 4px;
  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
          box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
  -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
       -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
          transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
}
.udform-control:focus {
  border-color: #66afe9;
  outline: 0;
  -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6);
          box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6);
}
.udPnlMulti {
position: relative;
  display: block;
  margin-top: 10px;
  margin-bottom: 10px;
  width:100%;
}
</style>
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtMissedAppDate').datepicker({ autoclose: true });
        $('#txtDateLastContact').datepicker({ autoclose: true });
        $('#txtDeathDate').datepicker({ autoclose: true });
        $('#txtCareEndDate').datepicker({ autoclose: true });
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
 <script language="javascript" type="text/javascript">

     function WindowPrint() {
         window.print();
     }
     function SetValue(theObject, theValue) {
         document.getElementById('ctl00_IQCareContentPlaceHolder_' + theObject).value = theValue;
         document.forms[0].submit();
     }

     function fnvalidate(checkQty) {
         validateDeathDate();
         var hidval = document.getElementById('ctl00_IQCareContentPlaceHolder_hidID').value;
         var hidchkbox = document.getElementById('ctl00_IQCareContentPlaceHolder_hidcheckbox').value;
         var hidsinglechk = document.getElementById('ctl00_IQCareContentPlaceHolder_hidchkbox').value;
         var hidradio = document.getElementById('ctl00_IQCareContentPlaceHolder_hidradio').value;

         var ctrlid = hidval.split('%');
         var chkboxid = hidchkbox.split('%');
         var chksingle = hidsinglechk.split('%');
         var rdo = hidradio.split('%');
         var findradio = "";
         var rdoid = "";


         for (var i = 0; i < ctrlid.length; i++) {
             var cid = "ctl00_IQCareContentPlaceHolder_" + ctrlid[i];

             if (document.getElementById(cid) != null) {
                 //                 if(document.getElementById(cid).disabled = false)
                 //                   {

                 if (document.getElementById(cid).value == '') {
                     var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                     var txt = document.getElementById(id).innerText;
                     document.getElementById(id).style.color = 'blue';
                     alert("Please enter the value for :" + txt);
                     document.getElementById(cid).focus();
                     return false;

                 }
                 if (document.getElementById(cid).value == '0') {
                     var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                     var txt = document.getElementById(id).innerText;
                     document.getElementById(id).style.color = 'blue';
                     alert("Please select for :" + txt);
                     document.getElementById(cid).focus();
                     return false;
                 }

                 // }   
             }


         }
         for (var i = 0; i < chksingle.length; i++) {
             var cid = "ctl00_IQCareContentPlaceHolder_" + chksingle[i];
             if (document.getElementById(cid) != null) {
                 if (document.getElementById(cid).disabled = false) {
                     if (document.getElementById(cid).checked == false) {
                         var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                         var txt = document.getElementById(id).innerText;
                         document.getElementById(id).style.color = 'blue';
                         alert("please select for :" + txt);

                         return false;
                     }
                 }
             }
         }

         for (var i = 0; i < chkboxid.length; i++) {
             var chkid = "ctl00_IQCareContentPlaceHolder_" + chkboxid[i];
             if (document.getElementById(chkid) != null) {
                 //                  if(document.getElementById(chkid).disabled = false)
                 //                     {
                 var chkBoxList = document.getElementById(chkid);
                 var find = "";
                 var chkBoxCount = chkBoxList.getElementsByTagName("input");
                 for (var j = 0; j < chkBoxCount.length; j++) {
                     if (chkBoxCount[j].checked) {
                         find = "True";
                     }
                 }
                 if (find == "") {

                     var id = "ctl00_IQCareContentPlaceHolder_LBL" + chkid.substring(chkid.indexOf("-"), chkid.length);
                     var txt = document.getElementById(id).innerText;
                     document.getElementById(id).style.color = 'blue';
                     alert("Please select for :" + txt);
                     document.getElementById(chkid).focus();
                     return false;

                 }

                 //} 

             }
         }

         for (var i = 0; i < rdo.length; i++) {
             var cid = "ctl00_IQCareContentPlaceHolder_" + rdo[i];
             rdoid = cid;
             if (document.getElementById(cid) != null) {
                 if (document.getElementById(cid).checked) {

                     findradio = "find";
                 }


             }

         }
         if (document.getElementById(rdoid) != null) {
             if (document.getElementById(rdoid).disabled = false) {

                 if (findradio == '') {
                     var id = "ctl00_IQCareContentPlaceHolder_LBL" + rdoid.substring(rdoid.indexOf("-"), rdoid.length);
                     var txt = document.getElementById(id).innerText;
                     document.getElementById(id).style.color = 'blue';
                     alert("Please check radio for :" + txt);

                     return false;
                 }
             }
         }


     }


     function fnvalidateSaveQty(checkQty) {
         var hidval = document.getElementById('ctl00_IQCareContentPlaceHolder_hidID').value;
         var hidchkbox = document.getElementById('ctl00_IQCareContentPlaceHolder_hidcheckbox').value;
         var hidsinglechk = document.getElementById('ctl00_IQCareContentPlaceHolder_hidchkbox').value;
         var hidradio = document.getElementById('ctl00_IQCareContentPlaceHolder_hidradio').value;

         var ctrlid = hidval.split('%');
         var chkboxid = hidchkbox.split('%');
         var chksingle = hidsinglechk.split('%');
         var rdo = hidradio.split('%');
         var findradio = "";
         var rdoid = "";

         var HiddenMsgBuilder = ""

         HiddenMsgBuilder = "Following values are required to complete the data quality check:\n\n";


         for (var i = 0; i < ctrlid.length; i++) {
             var cid = "ctl00_IQCareContentPlaceHolder_" + ctrlid[i];

             if (document.getElementById(cid) != null) {
                 //                   if(document.getElementById(cid).disabled = false)
                 //                     {

                 if (document.getElementById(cid).value == '') {
                     var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                     var txt = document.getElementById(id).innerText;

                     document.getElementById(id).style.color = 'red';
                     HiddenMsgBuilder = HiddenMsgBuilder + txt;
                     HiddenMsgBuilder = HiddenMsgBuilder + '\n';

                 }
                 if (document.getElementById(cid).value == '0') {
                     var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                     var txt = document.getElementById(id).innerText;
                     document.getElementById(id).style.color = 'red';
                     HiddenMsgBuilder = HiddenMsgBuilder + txt;
                     HiddenMsgBuilder = HiddenMsgBuilder + '\n';
                 }

                 // }

             }


         }
         for (var i = 0; i < chksingle.length; i++) {
             var cid = "ctl00_IQCareContentPlaceHolder_" + chksingle[i];
             if (document.getElementById(cid) != null) {
                 if (document.getElementById(cid).disabled = false) {

                     if (document.getElementById(cid).checked == false) {
                         var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                         var txt = document.getElementById(id).innerText;
                         document.getElementById(id).style.color = 'red';
                         HiddenMsgBuilder = HiddenMsgBuilder + txt;
                         HiddenMsgBuilder = HiddenMsgBuilder + '\n';
                     }
                 }

             }
         }

         for (var i = 0; i < chkboxid.length; i++) {
             var chkid = "ctl00_IQCareContentPlaceHolder_" + chkboxid[i];
             if (document.getElementById(chkid) != null) {
                 //                      if(document.getElementById(chkid).disabled = false)
                 //                       { 

                 var chkBoxList = document.getElementById(chkid);
                 var find = "";
                 var chkBoxCount = chkBoxList.getElementsByTagName("input");
                 for (var j = 0; j < chkBoxCount.length; j++) {
                     if (chkBoxCount[j].checked) {
                         find = "True";
                     }
                 }
                 if (find == "") {
                     var id = "ctl00_IQCareContentPlaceHolder_LBL" + chkid.substring(chkid.indexOf("-"), chkid.length);
                     var txt = document.getElementById(id).innerText;
                     document.getElementById(id).style.color = 'red';
                     HiddenMsgBuilder = HiddenMsgBuilder + txt;
                     HiddenMsgBuilder = HiddenMsgBuilder + '\n';
                 }
                 // }      
             }

         }

         for (var i = 0; i < rdo.length; i++) {
             var cid = "ctl00_IQCareContentPlaceHolder_" + rdo[i];
             rdoid = cid;
             if (document.getElementById(cid) != null) {
                 if (document.getElementById(cid).checked) {
                     findradio = "find";
                 }
             }
         }
         if (document.getElementById(rdoid) != null) {
             //                 if(document.getElementById(rdoid).disabled = false)
             //                   {

             if (findradio == '') {
                 var id = "ctl00_IQCareContentPlaceHolder_LBL" + rdoid.substring(rdoid.indexOf("-"), rdoid.length);
                 var txt = document.getElementById(id).innerText;
                 document.getElementById(id).style.color = 'red';
                 HiddenMsgBuilder = HiddenMsgBuilder + txt;
                 HiddenMsgBuilder = HiddenMsgBuilder + '\n';
             }
             // }

         }

         if (checkQty == '0') {

             return fnvalidateQty(HiddenMsgBuilder);
         }

     }



     function fnvalidateQty() {
         var hidvalQty = document.getElementById('ctl00_IQCareContentPlaceHolder_hidIDQty').value;
         var hidchkboxQty = document.getElementById('ctl00_IQCareContentPlaceHolder_hidcheckboxQty').value;
         var hidsinglechkQty = document.getElementById('ctl00_IQCareContentPlaceHolder_hidchkboxQty').value;
         var hidradioQty = document.getElementById('ctl00_IQCareContentPlaceHolder_hidradioQty').value;

         var ctrlidQty = hidvalQty.split('%');
         var chkboxidQty = hidchkboxQty.split('%');
         var chksingleQty = hidsinglechkQty.split('%');
         var rdoQty = hidradioQty.split('%');
         var findradio = "";
         var rdoid = "";
         var HiddenMsgBuilder1 = ""

         HiddenMsgBuilder1 = "Following values are required to complete the data quality check:\n\n";

         for (var i = 0; i < ctrlidQty.length; i++) {
             var cid = "ctl00_IQCareContentPlaceHolder_" + ctrlidQty[i];

             if (document.getElementById(cid) != null) {
                 //               if(document.getElementById(cid).disabled = false)
                 //                   {
                 if (document.getElementById(cid).value == '') {
                     var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                     var txt = document.getElementById(id).innerText;
                     document.getElementById(id).style.color = 'red';
                     HiddenMsgBuilder1 = HiddenMsgBuilder1 + txt;
                     HiddenMsgBuilder1 = HiddenMsgBuilder1 + '\n';
                 }
                 if (document.getElementById(cid).value == '0') {
                     var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                     var txt = document.getElementById(id).innerText;
                     document.getElementById(id).style.color = 'red';
                     HiddenMsgBuilder1 = HiddenMsgBuilder1 + txt;
                     HiddenMsgBuilder1 = HiddenMsgBuilder1 + '\n';
                 }
                 // }   

             }


         }
         for (var i = 0; i < chksingleQty.length; i++) {
             var cid = "ctl00_IQCareContentPlaceHolder_" + chksingleQty[i];
             if (document.getElementById(cid) != null) {
                 if (document.getElementById(cid).disabled = false) {
                     if (document.getElementById(cid).checked == false) {
                         var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                         var txt = document.getElementById(id).innerText;
                         document.getElementById(id).style.color = 'red';
                         HiddenMsgBuilder1 = HiddenMsgBuilder1 + txt;
                         HiddenMsgBuilder1 = HiddenMsgBuilder1 + '\n';
                     }
                 }

             }
         }

         for (var i = 0; i < chkboxidQty.length; i++) {
             var chkid = "ctl00_IQCareContentPlaceHolder_" + chkboxidQty[i];
             if (document.getElementById(chkid) != null) {
                 //                   if(document.getElementById(chkid).disabled = false)
                 //                   {
                 var chkBoxList = document.getElementById(chkid);
                 var find = "";
                 var chkBoxCount = chkBoxList.getElementsByTagName("input");
                 for (var j = 0; j < chkBoxCount.length; j++) {
                     if (chkBoxCount[j].checked) {
                         find = "True";
                     }
                 }
                 if (find == "") {

                     var id = "ctl00_IQCareContentPlaceHolder_LBL" + chkid.substring(chkid.indexOf("-"), chkid.length);

                     var txt = document.getElementById(id).innerText;
                     document.getElementById(id).style.color = 'red';
                     HiddenMsgBuilder1 = HiddenMsgBuilder1 + txt;
                     HiddenMsgBuilder1 = HiddenMsgBuilder1 + '\n';
                 }
                 //}
             }

         }
         for (var i = 0; i < rdoQty.length; i++) {
             var cid = "ctl00_IQCareContentPlaceHolder_" + rdoQty[i];
             rdoid = cid;
             if (document.getElementById(cid) != null) {
                 if (document.getElementById(cid).checked) {

                     findradio = "find";
                 }

             }

         }
         if (document.getElementById(rdoid) != null) {
             //            if(document.getElementById(rdoid).disabled = false)
             //               {
             if (findradio == '') {
                 var id = "ctl00_IQCareContentPlaceHolder_LBL" + rdoid.substring(rdoid.indexOf("-"), rdoid.length);
                 var txt = document.getElementById(id).innerText;
                 document.getElementById(id).style.color = 'red';
                 HiddenMsgBuilder1 = HiddenMsgBuilder1 + txt;
                 HiddenMsgBuilder1 = HiddenMsgBuilder1 + '\n';
             }
             // }    
         }

         if (HiddenMsgBuilder1 != 'Following values are required to complete the data quality check:\n\n') {
             alert(HiddenMsgBuilder1);
             return false;
         }
     }
     function DeathDate_Change() {
         document.getElementById('ctl00_IQCareContentPlaceHolder_txtCareEndDate').value = document.getElementById('ctl00_IQCareContentPlaceHolder_txtDeathDate').value;
     }

     function validateDeathDate() {


         var today = new Date();
         var dd = today.getDate();
         var mm = today.getMonth() + 1; //January is 0!

         var yyyy = today.getFullYear();
         if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = dd + '-' + mmm + '-' + yyyy;
         document.write(today);

         var enteredDate = document.getElementById('ctl00_IQCareContentPlaceHolder_txtDeathDate').value;

         var dd = enteredDate.getDate();
         var mm = enteredDate.getMonth() + 1; //January is 0!

         var yyyy = enteredDate.getFullYear();
         if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = dd + '-' + mmm + '-' + yyyy;
         document.write(enteredDate);

         var todayDate = new Date(today);
         var startDate = new Date(SDate);

         if (SDate != '' && EDate != '' && startDate > endDate) {
             alert("Care end date cannot be a future's date.");
             return false;
         }
         return false;
     }

        </script>
    <%--<form id="CareTraking" method="post" runat="server">--%>
    <%-- <form id="CareTraking" method="post" runat="server">--%>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Care Termination</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
          <!-- Display Date of Miss Appoinment and Date of Actual Contact -->
         <div class="row">
         <br />
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" for="AppDate">Date of Missed Scheduled Appointment:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">            
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtMissedAppDate" clientidmode="Static"
                                                       maxlength="11" size="10" name="AppDate" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date of Last Actual Contact:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtDateLastContact" clientidmode="Static"
                                                       maxlength="11" size="10" name="txtDateLastContact" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             
             </div> 
 </div>
 <!-- Display ART Patient Careended and Non_ART Care Ended -->
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Patient Exit Reason:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="cmbPatientExitReason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbPatientExitReason_SelectedIndexChanged" Width="90%" CssClass="form-control">
                                </asp:DropDownList>
             </div>
             <div class="col-md-6">
            <div class="row" id="Tr_Deathreason" runat="server">
 <div id="td1" visible="true" style="display: none">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Death Reason:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
           <asp:DropDownList ID="cmbDeathReason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbDeathReason_SelectedIndexChanged" Width="90%" CssClass="form-control">
                            </asp:DropDownList>
             </div>
 </div>
 <div id="td2" visible="true" style="display: block">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Death Date:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtDeathDate" clientidmode="Static"
                                                       maxlength="11" size="10" name="AppDate1" onblur="DeathDate_Change();" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
           
             </div>
 </div>


 </div>
             </div>
             
 </div>
 
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Panel ID="PnlConFields" Visible="false" runat="server" Height="100%" Width="100%"
                        Wrap="true">
                    </asp:Panel>                    
             </div>              
 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Panel ID="DIVCustomItem" Visible="false" runat="server" Height="100%" Width="100%"
                        Wrap="true">
                    </asp:Panel>
 </div>
 </div>
 <div class="row">
 <div id="tdsignature" visible="true">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">*Signature:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddinterviewer" runat="server" Visible="true" Width="90%" CssClass="form-control"></asp:DropDownList>
             </div>
             </div>
             <div id="tdCareEndDate" visible="true" >
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date Care Ended:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtCareEndDate" clientidmode="Static"
                                                       maxlength="11" size="10" visible="true" name="appDate" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
                                              </div> 
             </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <input type="submit" value="Save" id="btnsave" name="submit" runat="server" onclick="return fnvalidate('1');"
                                onserverclick="btnsave_ServerClick1" class="btn btn-primary" style="text-align: left;
                                width: 8%; height: 30px" />
                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <input type="submit" value="Data Quality Check" id="btnComplete" runat="server" name="DataQualityCheck"
                                onserverclick="btnComplete_ServerClick1" onclick="return fnvalidateQty();" class="btn btn-primary"
                                style="text-align: left; width: 17%; height: 30px" />
                            <label class="glyphicon glyphicon-floppy-saved" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click1"
                                CssClass="btn btn-primary" Width="8%" Height="30px" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <asp:Button ID="btnOk" Text="OK" CssClass="textstylehidden" runat="server" Visible="false"/>
                            <asp:Button ID="theBtnDQ" Text="DQ" CssClass="textstylehidden" runat="server" Visible="false" />
                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()"
                                CssClass="btn btn-primary" Width="8%" Height="30px" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                color: #fff;">
                            </label>
                            <asp:HiddenField ID="hidID" runat="server" />
                            <asp:HiddenField ID="hidcheckbox" runat="server" />
                            <asp:HiddenField ID="hidradio" runat="server" />
                            <asp:HiddenField ID="hidchkbox" runat="server" />
                            <asp:HiddenField ID="hidIDQty" runat="server" />
                            <asp:HiddenField ID="hidcheckboxQty" runat="server" />
                            <asp:HiddenField ID="hidradioQty" runat="server" />
                            <asp:HiddenField ID="hidchkboxQty" runat="server" />
                            <asp:HiddenField ID="theHitCntrl" runat="server" />
                            <asp:HiddenField ID="HiddenMsgBuilderfield" runat="server" />
             </div>              
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>
    <div class="container-fluid">
       
        <div class="border center formbg">
            <table width="100%" class="table-condensed">
                <tbody>
                    
                    
                    
                    
                   
                    <!-- Display Button -->
                    <tr>
                        <td class="Pad5 center" colspan="2">
                            <br />
                            
                            <br />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
