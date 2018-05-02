<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_Nutrition.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.UserControl_Nutrition" %>

<style type="text/css">
    .style1
    {
        width: 100%;
    }
    .style2
    {
        width: 211px;
    }
</style>
<div style="height: 900px; background-color: White">
    <table width="100%">
    <tr>
    <td class="form">
    <div class="GridView whitebg" style="cursor: pointer;">
                    <div class="grid">
                        <div class="rounded">
                            <div class="top-outer">
                                <div class="top-inner">
                                    <div class="top center">
                                        <h2>
                                            Nutrition Details</h2>
                                    </div>
                                </div>
                            </div>
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="height: auto; overflow: auto">
                                        <div id="div-gridview" class="GridView whitebg">
                                            <asp:GridView ID="grdNutrition" runat="server" 
                                                AutoGenerateColumns="False" Width="100%"  BorderWidth="0"
                                                GridLines="None" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>                                                
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="bottom-outer">
                                <div class="bottom-inner">
                                    <div class="bottom">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
    </td>
    </tr>
    </table>
    
    </div>