<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_Allergies.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_Allergies" %>
<script language="javascript" type="text/javascript">
    function CheckBoxToggleShowHide1(val, divID, txt) {

        var checkList = document.getElementById(val);
        var checkBoxList = checkList.getElementsByTagName("input");
        var arrayOfCheckBoxLabels = checkList.getElementsByTagName("label");
        var checkBoxSelectedItems = new Array();

        for (var i = 0; i < checkBoxList.length; i++) {

            if (checkBoxList[i].checked) {
                if (arrayOfCheckBoxLabels[i].innerText == txt) {
                    ShowHide(divID, "show");
                }

            }
            else {
                if (arrayOfCheckBoxLabels[i].innerText == txt) {
                    ShowHide(divID, "hide");
                }
            }
        }



    }

</script>
<table width="100%" class="formbg" cellspacing="6" cellpadding="0" border="0">
    <tr>
        <td class="border center pad5 whitebg" style="width: 50%">
            <table width="100%">
                <tbody>
                    <tr>
                        <td style="width: 40%" align="right">
                            <label class="control-label">
                                Drug allergy toxicities:</label>
                        </td>
                        <td style="width: 60%" align="left">
                            <div id="divDrugAllergiesToxicitiesPaeds" class="divborder margin10" style="margin-top: 10;
                                margin-bottom: 10">
                                <asp:CheckBoxList ID="cblDrugAllergiesToxicitiesPaeds" RepeatLayout="Flow" runat="server">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
        <td class="border center pad5 whitebg" style="width: 50%">
            <div id="divspecifyarvallergyshowhide" style="display: none;">
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 40%" align="right">
                                <label class="control-label">
                                    Specify ARV allergy :</label>
                            </td>
                            <td style="width: 60%" align="left">
                                <asp:TextBox ID="txtarvallergy" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td class="border center pad5 whitebg" style="width: 50%">
            <div id="divspecifyantibioticshowhide" style="display: none;">
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 40%" align="right">
                                <label class="control-label">
                                    Specify antibiotic allergy :</label>
                            </td>
                            <td style="width: 60%" align="left">
                                <asp:TextBox ID="txtantibioticallergy" runat="server" Width="100%">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </td>
        <td class="border center pad5 whitebg" style="width: 50%">
            <div id="divspecifyotherdrugshowhide" style="display: none;">
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 40%" align="right">
                                <label class="control-label">
                                    Specify other drug allergy:</label>
                            </td>
                            <td style="width: 60%" align="left">
                                <asp:TextBox ID="txtotherdrugallergy" runat="server" Width="100%">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </td>
    </tr>
</table>
