<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_PhysicalExamination.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_PhysicalExaminationascx" %>
 <script type="text/javascript" language="javascript" >
     function ShowHidePE(theDiv, YN, theFocus) {

         $(document).ready(function () {

             if (YN == "show") {
                 $("#" + theDiv).slideDown();

             }
             if (YN == "hide") {
                 $("#" + theDiv).slideUp();

             }

         });

     }
     function CheckBoxToggle(strcblcontrolId, strdivId, strfieldname) {
         var checkList = document.getElementById(strcblcontrolId);
         var checkBoxList = checkList.getElementsByTagName("input");
         var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
         var checkBoxSelectedItems = new Array();

         for (var i = 0; i < checkBoxList.length; i++) {

             if (checkBoxList[i].checked) {
                 if (arrayOfCheckBoxLabels[i].innerText == strfieldname) {
                     ShowHidePE(strdivId, "show");
                 }
                 else {
                     ShowHidePE(strdivId, "hide");
                 }
             }
             else {
                 ShowHidePE(strdivId, "hide");
             }
         }



     }
     function fnUncheckall(strcblcontrolId) {
         
         var checkList = document.getElementById(strcblcontrolId);
         var checkBoxList = checkList.getElementsByTagName("input");
         var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
         var checkBoxSelectedItems = new Array();
         
         
         for (var i = 0; i < checkBoxList.length; i++) {
             if ($("label[for='" + checkBoxList[i].id + "']").text() != 'Normal') {
                 //alert($("label[for='" + checkBoxList[i].id + "']").text());
                 checkBoxList[i].checked = false;                 
             }

         }
     }
     function fnUncheckNormal(strcblcontrolId) {
         var checkList = document.getElementById(strcblcontrolId);
         var checkBoxList = checkList.getElementsByTagName("input");
         var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
         var checkBoxSelectedItems = new Array();
         for (var i = 0; i < checkBoxList.length; i++) {
             if ($("label[for='" + checkBoxList[i].id + "']").text() == 'Normal') {
                 checkBoxList[i].checked = false;
             }

         }
         
     }
     function CheckBoxToggleShowHidePE(val, divID, txt, txtControlId) {
         var checkList = document.getElementById(val);
         var checkBoxList = checkList.getElementsByTagName("input");
         var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
         var checkBoxSelectedItems = new Array();
         var arrayOfCheckBoxLabelsnew;
         for (var i = 0; i < checkBoxList.length; i++) {
             if (checkBoxList[i].checked) {
                 if ($("label[for='" + checkBoxList[i].id + "']").text() == txt) {
                     ShowHidePE(divID, "show");
                 }
//                 if (arrayOfCheckBoxLabels[i].innerHTML == txt) {
//                     ShowHidePE(divID, "show");
//                 }

             }
             else {
                 if ($("label[for='" + checkBoxList[i].id + "']").text() == txt) {
                     document.getElementById(txtControlId).value = '';
                     ShowHidePE(divID, "hide");
                 }

//                 if (arrayOfCheckBoxLabels[i].innerHTML == txt) {
//                     document.getElementById(txtControlId).value = '';
//                     ShowHidePE(divID, "hide");
//                 }
             }
         }



     }
</script>
<div class="row">
 <div class="col-xs-6">
 <%--left--%>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <label class="control-label required">
             <asp:Label ID="lblGeneral" runat="server" Text="General :" ></asp:Label></label>
 </div>
 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div id="divGeneralConditions" enableviewstate="true" runat="server" >
                                    <asp:CheckBoxList ID="cblGeneralConditions" CssClass="checkbox-btn" runat="server">
                                    </asp:CheckBoxList>
                                </div>
 </div>
 </div>
  <div  class="row" id="divgeneralothercondition" style="display: none;">
  <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblOther general conditions-8888326" class="control-label">Other general conditions :</label>
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtOtherGeneralConditions" ClientIDMode="Static" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>
 </div>
 </div>
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Label ID="lblOralCavity" runat="server" CssClass="control-label required" Text="Oral Cavity :" ></asp:Label>
 </div>
 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div id="divOralCavityConditions" enableviewstate="true" runat="server">
  <asp:CheckBoxList ID="cblOralCavityConditions" Width="100%" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
  </asp:CheckBoxList>
                                </div>
 </div>
 </div>
 <div id="divOtherOralCavityConditions" style="display: none;">
  <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblOther oral cavity conditions-8888332" class="control-label">Other oral cavity conditions:</label>
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtOtherOralCavityConditions" ClientIDMode="Static" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>
 </div>
 </div>

 </div>
      <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <label class="control-label required"><asp:Label ID="lblCNS" runat="server" Text="CNS :" ></asp:Label></label>
             
 </div>
 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<div id="divCNSConditions" enableviewstate="true" runat="server">
                                    <asp:CheckBoxList ID="cblCNSConditions"  Width="100%"
                                        RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                    </asp:CheckBoxList>
                                </div>
 </div>
 </div>
  <div class="row" id="divOtherCNSConditions" style="display: none;">
  
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblOther CNS conditions-8888336" class="control-label">Other CNS conditions :</label>
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtOtherCNSConditions" ClientIDMode="Static" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>

 </div>
 
 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <label class="control-label required"><asp:Label ID="lblSkin" runat="server" Text="Skin :" ></asp:Label> </label>
             
 </div>
 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <div id="divSkinConditions" enableviewstate="true" runat="server">
  <asp:CheckBoxList ID="cblSkinConditions"  Width="100%" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
  </asp:CheckBoxList>
                                </div>
 </div>
 </div>
  <div class="row" id="divOtherSkinConditions" style="display: none;">

 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblOther skin conditions-8888340" class="control-label">Other skin conditions :</label>
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtOtherSkinConditions" ClientIDMode="Static" runat="server" Width="250" CssClass="form-control"></asp:TextBox>

 </div>

 </div>
 </div> 
 <div class="col-xs-6">
 <%--right--%>
 <div class="row"> 
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label required"><asp:Label ID="lblCardiovarscular" runat="server" Text="Cardiovarscular :" ></asp:Label></label>
             </div>                                
    </div>
<div class="row">
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="divCardiovascularConditions" enableviewstate="true" runat="server">
                                    <asp:CheckBoxList ID="cblCardiovascularConditions" Width="100%" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                    </asp:CheckBoxList>
                                </div>
             </div>                                
    </div>
    <div class="row" id="divOtherCardiovascularConditions" style="display: none;">
    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
    <label id="lblOther cardiovascular condition-8888330" class="control-label">Other cardiovascular condition :</label>
    </div>
    <div class="col-md-6 col-sm-12 col-xs-12 form-group">
    <asp:TextBox ID="txtOtherCardiovascularConditions" ClientIDMode="Static" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>
    </div>
    </div>
<div class="row"> 
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label required"><asp:Label ID="lblGenitourinary" runat="server" Font-Bold="true" Text="Genitourinary :" ></asp:Label></label>
             </div>
             </div>
<div class="row">
<div class="col-md-12 col-sm-12 col-xs-12 form-group">
<div id="divGenitalUrinaryConditions" enableviewstate="true"
                                    runat="server">
                                    <asp:CheckBoxList ID="cblGenitalUrinaryConditions" 
                                        Width="100%" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                    </asp:CheckBoxList>
                                </div>
</div>
</div>
<div class="row" id="divOtherGenitourinaryConditions" style="display: none;">
<div class="col-md-6 col-sm-12 col-xs-12 form-group">
<label id="lblOther genital urinary conditions-8888334" class="control-label">Other genital urinary conditions :</label>
</div>
<div class="col-md-6 col-sm-12 col-xs-12 form-group">
<asp:TextBox ID="txtOtherGenitourinaryConditions" ClientIDMode="Static" runat="server" Width="250" CssClass="form-control"></asp:TextBox>
</div>
</div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <label class="control-label required"><asp:Label ID="lblChest" runat="server" Text="Chest/Lungs :" ></asp:Label> </label>             
 </div>
 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div id="divChestLungsConditions" enableviewstate="true" runat="server" >
<asp:CheckBoxList ID="cblChestLungsConditions"  Width="100%" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
</asp:CheckBoxList>                    
                                </div>
 </div>
 </div>
  <div class="row" id="divOtherChestLungsConditions" style="display: none;">

 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblOther chest lungs conditions-8888338" class="control-label">Other chest lungs conditions :</label>
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtOtherChestLungsConditions" ClientIDMode="Static" runat="server" Width="250" CssClass="form-control"></asp:TextBox>

 </div>

 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <label class="control-label required"><asp:Label ID="lblAbdomen" runat="server" Text="Abdomen :" ></asp:Label> </label>             
 </div>
 </div>
  <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div id="divAbdomenConditions" enableviewstate="true" runat="server">
                                    <asp:CheckBoxList ID="cblAbdomenConditions" onclick="CheckBoxToggleShortTerm();"
                                        Width="100%" RepeatLayout="Flow" runat="server" CssClass="checkbox-btn">
                                    </asp:CheckBoxList>
                                </div>
 </div>
 </div>
  <div class="row" id="divOtherAbdomenConditions" style="display: none;">

 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <label id="lblOther abdomen conditions-8888328" align="center" class="control-label">Other abdomen conditions:</label>
 </div>
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
 <asp:TextBox ID="txtOtherAbdomenConditions" ClientIDMode="Static" runat="server" Width="250" CssClass="form-control"></asp:TextBox>

 </div>

 </div>
 </div>                             
    </div>
<div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
 <label id="lblAdditional medical condition notes-8888564" class="control-label">Additional physical examination notes :</label>            
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtOtherMedicalConditionNotes" Columns="20" Rows="6" Width="100%" TextMode="MultiLine"
                                    runat="server" CssClass="form-control" style="resize:none;"></asp:TextBox>
             </div>
                                
    </div>



    

