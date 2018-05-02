<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_ChronicCondition.ascx.cs" Inherits="PresentationApp.GreenCard.UserControls.UC_ChronicCondition" %>
<style type="text/css">
    .label-align
    {
        text-align: right !important;
    }
    .control-align
    {
        text-align: left !important;
    }
    .tablepop th
    {        
        height: 16px;
        font-size: 12px;
    }
    .tablepop td
    {        
        height: 14px;
        font-size: 11px;
    }
</style>
<div class="box" style="width: 100%">
    <div class="table-responsive well well-sm pre-scrollable" style="max-height: 150px">
        <asp:GridView ID="grdchronic" runat="server" AutoGenerateColumns="False" AllowSorting="true"
            Width="100%" BorderColor="white" PageIndex="1" BorderWidth="0" GridLines="None"
            CssClass="table table-striped table-bordered table-hover" CellPadding="0" CellSpacing="0">
            <%--<Columns>
                            <asp:BoundField HeaderText="Sample Date" DataField="ViralDate" />
                            <asp:BoundField HeaderText="Condition" DataField="ChronicCondition" />
                            <asp:BoundField HeaderText="ID" DataField="Id" Visible="false"/>
                        </Columns>--%>
        </asp:GridView>
    </div>
</div>
