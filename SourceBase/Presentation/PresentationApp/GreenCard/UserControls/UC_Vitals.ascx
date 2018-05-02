<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_Vitals.ascx.cs" Inherits="PresentationApp.GreenCard.UserControls.UC_Vitals" %>
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
    <!-- /.box-header -->
        
        <div class="table-responsive">
            <asp:GridView ID="grdAllergy" runat="server" CssClass="table table-striped table-bordered tablepop"
            AutoGenerateColumns="False" BorderWidth="0" 
            CellPadding="0" CellSpacing="0" >                       
        </asp:GridView>
        
    </div>
    <!-- /.box-body -->
</div>
