<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_PhysicalExamination.ascx.cs"
    Inherits="PresentationApp.GreenCard.UserControls.UC_PhysicalExaminationascx" %>
 <script type="text/javascript" language="javascript" >
     $(document).ready(function () {
         
         
     });
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

<table width="100%" border="0" cellspacing="6" cellpadding="0">
    <tbody>
        <tr>
            <td class="border center pad5 whitebg" style="width: 50%;">
                <table width="100%">
                    <tbody>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblGeneral" runat="server" Font-Bold="true" CssClass="required" Text="General :" ></asp:Label>
                              <%--  <label id="lblGeneral-8888325" align="center" style="width: 100%" class="required">
                                    General :</label>--%>
                            </td>
                            </tr>
                            <tr>
                            <td align="left" style="padding:10px 20px 5px 0">
                                <div id="divGeneralConditions" enableviewstate="true" class="table-responsive  pre-scrollable box form-group" style="max-height:200px;padding:10px 10px">
                                    <asp:CheckBoxList ID="cblGeneralConditions" CssClass="minimal"  runat="server">
                                    </asp:CheckBoxList>
                                </div>
                                
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                        <div id="divgeneralothercondition" style="display: none;">
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td align="right" style="width: 38%;">
                                    <label id="lblOther general conditions-8888326" align="center">
                                        Other general conditions :</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOtherGeneralConditions" ClientIDMode="Static" runat="server" Width="180px"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                        </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td class="border center pad5 whitebg" style="width: 50%;">
                <table width="100%">
                    <tbody>
                        <tr>
                            <td align="left">
                               <asp:Label ID="lblCardiovarscular" runat="server" Font-Bold="true" CssClass="required" Text="Cardiovarscular :" ></asp:Label>
                               <%-- <label id="lblCardiovarscular-8888329" align="center" style="width: 100%" class="required">
                                    Cardiovarscular :</label>--%>
                            </td>
                            </tr>
                            <tr>
                            <td align="left" style="padding:10px 20px 5px 0">
                                <div id="divCardiovascularConditions" enableviewstate="true" class="table-responsive  pre-scrollable box" style="max-height:200px;padding:10px 10px">
                                    <asp:CheckBoxList ID="cblCardiovascularConditions" CssClass="minimal"
                                        Width="100%" RepeatLayout="Flow" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                        <div id="divOtherCardiovascularConditions" style="display: none;">
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td align="right" style="width: 50%;">
                                    <label id="lblOther cardiovascular condition-8888330" align="center">
                                        Other cardiovascular condition :</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOtherCardiovascularConditions" ClientIDMode="Static" runat="server" Width="180px"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                        </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td class="border center pad5 whitebg" style="width: 50%;">
              <table width="100%">
                    <tbody>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblOralCavity" runat="server" Font-Bold="true" CssClass="required" Text="Oral Cavity :" ></asp:Label>
                               <%-- <label id="lblOral Cavity-8888331" align="center" style="width: 100%" class="required">
                                    Oral Cavity :</label>--%>
                            </td>
                            </tr>
                            <tr>
                            <td align="left" style="padding:10px 20px 5px 0">
                                <div id="divOralCavityConditions" enableviewstate="true" class="table-responsive  pre-scrollable box" style="max-height:200px;padding:10px 10px">
                                    <asp:CheckBoxList ID="cblOralCavityConditions" 
                                        Width="100%" RepeatLayout="Flow" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                        <div id="divOtherOralCavityConditions" style="display: none;">
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td align="right" style="width: 50%;">
                                    <label id="lblOther oral cavity conditions-8888332" align="center">
                                        Other oral cavity conditions:</label>
                                </td>
                                <td align="left" >
                                    <asp:TextBox ID="txtOtherOralCavityConditions" ClientIDMode="Static" runat="server" Width="180px"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                        </td>
                        </tr>
                    </tbody>
                </table>  
            </td>
            <td class="border center pad5 whitebg" style="width: 50%;">
              <table width="100%">
                    <tbody>
                        <tr>
                            <td align="left">
                                 <asp:Label ID="lblGenitourinary" runat="server" Font-Bold="true" CssClass="required" Text="Genitourinary :" ></asp:Label>
                                <%--<label id="lblGenitourinary-8888333" align="center" class="required">
                                    Genitourinary :</label>--%>
                            </td>
                            </tr>
                            <tr>
                            <td align="left" style="padding:10px 20px 5px 0">
                                <div id="divGenitalUrinaryConditions" enableviewstate="true" class="table-responsive  pre-scrollable box" style="max-height:200px;padding:10px 10px">
                                    <asp:CheckBoxList ID="cblGenitalUrinaryConditions" 
                                        Width="100%" RepeatLayout="Flow" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                        <div id="divOtherGenitourinaryConditions" style="display: none;">
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td align="right" style="width: 50%;">
                                    <label id="lblOther genital urinary conditions-8888334" align="center">
                                        Other genital urinary conditions :</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOtherGenitourinaryConditions" ClientIDMode="Static" runat="server" Width="180px"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                        </td>
                        </tr>
                    </tbody>
                </table>  
            </td>
        </tr>
        <tr>
            <td class="border center pad5 whitebg" style="width: 50%;">
               <table width="100%">
                    <tbody>
                        <tr>
                            <td align="left">
                               <asp:Label ID="lblCNS" runat="server" Font-Bold="true" CssClass="required" Text="CNS :" ></asp:Label>
                               <%-- <label id="lblCNS-8888335" align="center" class="required">
                                    CNS :</label>--%>
                            </td>
                            </tr>
                            <tr>
                            <td align="left" style="padding:10px 20px 5px 0">
                                <div id="divCNSConditions" enableviewstate="true" class="table-responsive  pre-scrollable box" style="max-height:200px;padding:10px 10px">
                                    <asp:CheckBoxList ID="cblCNSConditions"  Width="100%"
                                        RepeatLayout="Flow" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                        <div id="divOtherCNSConditions" style="display: none;">
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td align="right" style="width: 35%;">
                                    <label id="lblOther CNS conditions-8888336" align="center">
                                        Other CNS conditions :</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOtherCNSConditions" ClientIDMode="Static" runat="server" Width="180px"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                        </td>
                        </tr>
                    </tbody>
                </table> 
            </td>
            <td class="border center pad5 whitebg" style="width: 50%;">
                <table width="100%">
                    <tbody>
                        <tr>
                            <td align="left">
                               <asp:Label ID="lblChest" runat="server" Font-Bold="true" CssClass="required" Text="Chest/Lungs :" ></asp:Label> 
                               <%-- <label id="lblChest/Lungs-8888337" align="center" class="required">
                                    Chest/Lungs :</label>--%>
                            </td>
                            </tr>
                            <tr>
                            <td align="left" style="padding:10px 20px 5px 0">
                                <div id="divChestLungsConditions" enableviewstate="true" class="table-responsive  pre-scrollable box" style="max-height:200px;padding:10px 10px">
                                    <asp:CheckBoxList ID="cblChestLungsConditions" 
                                        Width="100%" RepeatLayout="Flow" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                        <div id="divOtherChestLungsConditions" style="display: none;">
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td align="right" style="width: 45%;">
                                    <label id="lblOther chest lungs conditions-8888338" align="center">
                                        Other chest lungs conditions :</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOtherChestLungsConditions" ClientIDMode="Static" runat="server" Width="180px"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                        </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td class="border center pad5 whitebg" style="width: 50%;">
               <table width="100%">
                    <tbody>
                        <tr>
                            <td align="left" >
                                <asp:Label ID="lblSkin" runat="server" Font-Bold="true" CssClass="required" Text="Skin :" ></asp:Label> 
                                <%--<label id="lblSkin-8888339" align="center" class="required">
                                    Skin :</label>--%>
                            </td>
                            </tr>
                            <tr>
                            <td align="left" style="padding:10px 20px 5px 0">
                                <div id="divSkinConditions" enableviewstate="true" class="table-responsive  pre-scrollable box" style="max-height:200px;padding:10px 10px">
                                    <asp:CheckBoxList ID="cblSkinConditions"  Width="100%"
                                        RepeatLayout="Flow" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2" align="left">
                <div id="divOtherSkinConditions" style="display: none;">
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td align="right" style="width: 35%;">
                                    <label id="lblOther skin conditions-8888340" >
                                        Other skin conditions :</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOtherSkinConditions" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                        </td>
                        </tr>
                    </tbody>
                </table> 
            </td>
            <td class="border center pad5 whitebg" style="width: 50%;">
                <table width="100%">
                    <tbody>
                        <tr>
                            <td align="left">
                             <asp:Label ID="lblAbdomen" runat="server" Font-Bold="true" CssClass="required" Text="Abdomen :" ></asp:Label> 
                               <%-- <label id="lblAbdomen-8888327" align="center" style="width: 100%" class="required">
                                    Abdomen :</label>--%>
                            </td>
                            </tr>
                            <tr>
                            <td align="left" style="padding:10px 20px 5px 0">
                                <div id="divAbdomenConditions" enableviewstate="true" class="table-responsive  pre-scrollable box" style="max-height:200px;padding:10px 10px">
                                    <asp:CheckBoxList ID="cblAbdomenConditions" onclick="CheckBoxToggleShortTerm();"
                                        Width="100%" RepeatLayout="Flow" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                        <div id="divOtherAbdomenConditions" style="display: none;">
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td align="right" style="width: 50%;">
                                    <label id="lblOther abdomen conditions-8888328" align="center">
                                        Other abdomen conditions:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOtherAbdomenConditions" ClientIDMode="Static" runat="server" Width="180px"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                        </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td class="border center pad5 whitebg" colspan="2">
              <table width="100%">
                    <tbody>
                        <tr>
                            <td align="left" style="width: 100%;">
                                <label id="lblAdditional medical condition notes-8888564" align="center">
                                    Additional 
                                physical examination notes :</label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;">
                                <asp:TextBox ID="txtOtherMedicalConditionNotes" Columns="20" Rows="3" Width="100%" TextMode="MultiLine"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>  
            </td>
            
        </tr>
        
    </tbody>
</table>
