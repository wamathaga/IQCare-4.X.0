<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlKNH_VitalSign.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControlKNH_VitalSign" %>
<script type="text/javascript" language="javascript">
    function fnSetBMI(txtw, txth, txtbmi) {

        var weight = document.getElementById(txtw).value.replace(",", ".");
        var height = document.getElementById(txth).value.replace(",", ".");
        var txtbmi = document.getElementById(txtbmi);
        if (weight > 0 && height > 0) {
            var finalBmi = weight / (height / 100 * height / 100)
            txtbmi.value = finalBmi
        }
    }

</script>
<style type="text/css">
    td
    {
        word-break: break-all;
    }
    .tbl-right
    {
        width: 100%;
        border: none;
    }
    .data-control
    {
        width: 50%;
    }
    .data-lable
    {
        width: 50%;
    }
    .tbl-left
    {
        width: 100%;
        border: none;
    }
    .col-left
    {
        width: 350px;
    }
    .col-right
    {
        width: 350px;
    }
</style>
<table class="border center formbg" cellspacing="6" cellpadding="0" width="100%"
    border="0">
    <tr>
        <td width="50%" class="border pad5 whitebg">
            <table class="tbl-right">
                <tr>
                    <td align="right" class="data-lable">
                        <label class="control-label">
                            Temperature (Celsius):</label>
                    </td>
                    <td align="left" class="data-control">
                        <asp:TextBox ID="txtTemperatureModal" runat="server">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" class="border pad5 whitebg">
            <table class="tbl-left">
                <tr>
                    <td align="right" class="data-lable">
                        <label class="control-label">
                            RR (Bpm):</label>
                    </td>
                    <td align="left" class="data-control">
                        <asp:TextBox ID="txtRespirationRate" runat="server">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="50%" class="border pad5 whitebg">
            <table class="tbl-right">
                <tr>
                    <td align="right" class="data-lable">
                        <label class="control-label">
                            Heart Rate (Bpm):</label>
                    </td>
                    <td align="left" class="data-control">
                        <asp:TextBox ID="txtHeartRate" runat="server">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" class="border pad5 whitebg">
            <table class="tbl-left">
                <tr>
                    <td align="right" class="data-lable">
                        <label class="control-label required">
                            BP:</label>
                    </td>
                    <td align="left" class="data-control">
                        <asp:TextBox ID="txtSystollicBloodPressure" Width="50px" runat="server">
                        </asp:TextBox>/
                        <asp:TextBox ID="txtDiastolicBloodPressure" Width="50px" runat="server">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="50%" class="border pad5 whitebg">
            <table class="tbl-left">
                <tr>
                    <td align="right" class="data-lable">
                        <label class="control-label required">
                            Height (cms):</label>
                    </td>
                    <td align="left" class="data-control">
                        <asp:TextBox ID="txtHeight" runat="server">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" class="border pad5 whitebg">
            <table class="tbl-right">
                <tr>
                    <td align="right" class="data-lable">
                        <label class="control-label required">
                            Weight (kgs):</label>
                    </td>
                    <td align="left" class="data-control">
                        <asp:TextBox ID="txtWeight" runat="server">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="50%" class="border pad5 whitebg">
            <table class="tbl-left">
                <tr>
                    <td align="right" class="data-lable">
                        <label class="control-label">
                            BMI:</label>
                    </td>
                    <td align="left" class="data-control">
                        <asp:TextBox ID="txtBMI" runat="server" Enabled="false">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" class="border pad5 whitebg">
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="divshowvitalsign" style="display: none;">
                <table width="100%">
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Head Circumference :</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtheadcircumference" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Weight for age :</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:DropDownList ID="ddlweightforage" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Weight for height :</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtweightforheight" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" class="border pad5 whitebg">
                            <table class="tbl-right">
                                <tr>
                                    <td align="right" class="data-lable">
                                        <label class="control-label">
                                            Nurses Comments :</label>
                                    </td>
                                    <td align="left" class="data-control">
                                        <asp:TextBox ID="txtnursescomments" TextMode="MultiLine" Width="200px" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
