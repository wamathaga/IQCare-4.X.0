<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_Pharmacy.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_Pharmacy" %>


<script type="text/javascript">

    function ShowHide(theDiv, YN) {

        $(document).ready(function () {

            if (YN == "show") {
                $("#" + theDiv).show();

            }
            if (YN == "hide") {
                $("#" + theDiv).hide();

            }

        });

    }
//    function fnSubsituations() {
//        var e = document.getElementById("=ddlSubstitutionInterruption.ClientID%>");
//        var strtext = e.options[e.selectedIndex].text;
//        if (strtext == "Change regimen") {
//            show('arvTherapyChange');
//            hide('arvTherapyStop');

//        }
//        else if (strtext == "Stop treatment") {
//            show('arvTherapyStop');
//            hide('arvTherapyChange');
//        }
//        else {
//            hide('arvTherapyChange');
//            hide('arvTherapyStop');
//        }
//    }
//    function fnRegimenChange() {
//        var e = document.getElementById("=ddlArvTherapyChangeCode.ClientID%>");
//        var strtext = e.options[e.selectedIndex].text;
//        if (strtext == "Other reason (specify)") {
//            show('otherarvTherapyChangeCode');

//        }
//        else {
//            hide('otherarvTherapyChangeCode');

//        }
//    }
//    function fnStopReason() {
//        var e = document.getElementById("=ddlArvTherapyStopCode.ClientID%>");
//        var strtext = e.options[e.selectedIndex].text;
//        if (strtext == "Other (specify)") {
//            show('otherarvTherapyStopCode');

//        }
//        else {
//            hide('otherarvTherapyStopCode');

//        }
//    }


    //followup
    function getSelectedtableValue(DivId, DDText, str, tableId) {
        var e = document.getElementById(DDText);
        var text = e.options[e.selectedIndex].innerHTML;
        var YN = "";
//        if (str == "Start new treatment (naive patient)" || str == "Change regimen" || str == "Switch to second line" || str == "Stop treatment") {
//            ShowHide(tableId, "show");
//        }
//        else {
//            ShowHide(tableId, "hide");
//        }

        if (text == str) {
            YN = "show";
        }
        else {
            YN = "hide";
        }
//        if (YN == "hide") {
//            hideChklistEligiblethrough();
//        }
        //ShowHide(tableId, "show");
        hideChklistEligiblethrough('<%=chklistEligiblethrough.ClientID %>');
        hideChklistEligiblethrough('<%=chklistARTchangecode.ClientID %>');
        hideChklistEligiblethrough('<%=chklistARTstopcode.ClientID %>');
        ShowHide(DivId, YN);
    }
    //Nidhi
    //Unselect all the selected list onselection changed of dropdownlist
    function hideChklistEligiblethrough(controlId) {
        var elementRef = document.getElementById(controlId);
        var inputElementArray = elementRef.getElementsByTagName('INPUT');
        for (var i = 0; i < inputElementArray.length; i++) {
            if (inputElementArray[i].type == 'checkbox')
                inputElementArray[i].checked = false; ;
        }
    }
    function CheckBoxHideUnhide(strcblcontrolId, strdivId, strfieldname) {
        //alert(strcblcontrolId);
        var checkList = document.getElementById(strcblcontrolId);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();

        for (var i = 0; i < checkBoxList.length; i++) {

            if (checkBoxList[i].checked) {
                if (arrayOfCheckBoxLabels[i].innerHTML == strfieldname) {
                    ShowHide(strdivId, "show");
                }
                else {
                    ShowHide(strdivId, "hide");
                }
            }
            else {
                ShowHide(strdivId, "hide");
            }
        }

    }

    
</script>
<style type="text/css">
    .style2
    {
        width: 15%;
    }
</style>
<div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
      <%-- <div class="box-header">
        <h3 class="box-title">Heading	</h3>
       </div>--%>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Last regimen dispensed:</label>
             <asp:Label ID="lblLastRegimenDispensed" runat="server" Font-Bold="True"></asp:Label>
             </div>
            
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblTreatmentplan" runat="server" Text="Treatment plan:"></asp:Label></label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlTreatmentplan" runat="server" onselectedindexchanged="ddlTreatmentplan_SelectedIndexChanged" CssClass="form-control" Width="99%"></asp:DropDownList>
             </div>
           
 </div>
  <div class="row" id="divEligiblethrough" style="display: none;">
  <div id="DIVTreatmentplan">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap" >
 <label class="control-label">Eligible through :</label>
 </div>
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
 <div id="divchklistEligiblethrough" enableviewstate="true" runat="server">
                                <asp:CheckBoxList ID="chklistEligiblethrough" RepeatLayout="Flow" runat="server" >
                                </asp:CheckBoxList>
                            </div>
 </div>
 <div id="divOtherEligibility" style="display: none;">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group" >
 <label id="Label2" class="control-label">Specify other:</label></div>
 <div class="col-md-3 col-sm-12 col-xs-12 form-group" >
 <asp:TextBox ID="txtSpecifyOtherEligibility" runat="server" Width="70%" CssClass="form-control"></asp:TextBox>
 </div>
 </div>
 </div>
 </div>
 <div class="row" id="divReasonforswitchto2ndlineregimen" style="display: none;">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label id="lblReason for switch to 2nd line regimen-8888483" class="control-label">Reason for switch to 2nd line regimen:</label>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlReasonforswitchto2ndlineregimen" CssClass="form-control" Width="99%"></asp:DropDownList>
             </div>
            
 </div>
 <div id="divARTchangecode" style="display: none;">
<div class="row" id="divNoofdrugssubstituted">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap">
            <label id="lblNo of drugs substituted-8888482" class="control-label">No of drugs substituted :</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtNoofdrugssubstituted" runat="server" CssClass="form-control" Width="99%"></asp:TextBox>
             </div>
           
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap">
            <label id="lblART change code-8888552" class="control-label">ART change code :</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div id="divchklistARTchangecode" enableviewstate="true" 
                                runat="server">
                                <asp:CheckBoxList ID="chklistARTchangecode" RepeatLayout="Flow" runat="server" CssClass="">
                                </asp:CheckBoxList>
                            </div>
             </div>
             <div id="divSpecifyotherARTchangereason" style="display: none;">
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap">
             <label id="lblSpecify other ART change reason-8888873" class="control-label">Specify other ART change reason:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtSpecifyotherARTchangereason" runat="server" Width="70%" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
 </div>
 <div class="row" id="divARTstopcode" style="display: none;">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <label id="lblART stop code-8888553" class="control-label">ART stop code :</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div id="divchklistARTstopcode" enableviewstate="true" class="customdivbordermultiselect"
                                runat="server">
                                <asp:CheckBoxList ID="chklistARTstopcode" RepeatLayout="Flow" runat="server">
                                </asp:CheckBoxList>
                            </div>
             </div>
              <div id="divARTstopcodeother" style="display: none;">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="Label1" class="control-label">Specify other:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtSpecifyOtherStopCode" runat="server" Width="70%" CssClass="form-control"></asp:TextBox>
             </div> 
             </div>
 </div>
 
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnPrescribeDrugs" runat="server" Text="Prescribe Regimen" 
            onclick="btnPrescribeDrugs_Click" CssClass="btn btn-primary" Height="30px" Width="15%" style="text-align:left"/>
            <label id="Label3" runat = "server" class="fa fa-medkit" style="margin-left: -2.5%; vertical-align: sub; color: #fff;"></label>
             </div>
             
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>

<div class="center formbg">
<table cellspacing="6" cellpadding="0" width="100%" border="0">
<tr>
    <td class="form" align="left">
        <label>
        </label>
    </td>
</tr>

 



<tr align="left">
    <td class="form" align="left">
        
    </td>
</tr>
</table>
</div>

