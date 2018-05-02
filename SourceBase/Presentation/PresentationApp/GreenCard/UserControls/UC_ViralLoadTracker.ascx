<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_ViralLoadTracker.ascx.cs" Inherits="PresentationApp.GreenCard.UserControls.UC_ViralLoadTracker" %>
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
<div class="box" style="width:100%">   
    <div class="table-responsive well well-sm pre-scrollable" style="max-height:150px">
                     <asp:GridView ID="grdviralload" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                        Width="100%" BorderColor="white" BorderWidth="0" GridLines="None" CssClass="table table-striped table-bordered tablepop"
                         CellPadding="0" CellSpacing="0">                        
                        <Columns>
                            <asp:BoundField HeaderText="Sample Date" DataField="ReportedbyDate" ItemStyle-Width="25%" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField HeaderText="Results" DataField="TestResults" />
                            <asp:BoundField HeaderText="Reason" DataField="Reason" />
                            <asp:BoundField HeaderText="ID" DataField="TestId" Visible="false"/>
                        </Columns>
                    </asp:GridView>
                    </div>
</div>