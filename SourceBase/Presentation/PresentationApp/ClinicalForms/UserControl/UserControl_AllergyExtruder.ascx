<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_AllergyExtruder.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.UserControl_AllergyExtruder" %>



<div class="center formbg">
<table cellspacing="6" cellpadding="0" width="100%" border="0">
    <tr>
        <td class="form">
        <div class="GridView whitebg" style="cursor: pointer;">
                    <div class="grid">
                        <div class="rounded">
                            <div class="top-outer">
                                <div class="top-inner">
                                    <div class="top">
                                        <h2>
                                            Allergy Details</h2>
                                    </div>
                                </div>
                            </div>
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid" style="height: auto; overflow: auto">
                                        <div id="div-gridview" class="GridView whitebg">
                                            <asp:GridView ID="grdDrugAllergy" runat="server" 
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


