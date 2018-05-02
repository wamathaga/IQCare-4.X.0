<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_Family.ascx.cs" Inherits="PresentationApp.GreenCard.UserControls.UC_Family" %>
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
    <div class="table-responsive">
        <asp:GridView ID="grdFamily" runat="server" CssClass="table table-striped table-bordered table-hover"
            AutoGenerateColumns="False" BorderWidth="0" CellPadding="0" CellSpacing="0">
            <Columns>
                            <asp:BoundField HeaderText="Id" DataField="Id" Visible="false" />
                            <asp:BoundField HeaderText="Patientid" DataField="Patientid" Visible="false"/>
                            <asp:BoundField HeaderText="Last Name" DataField="RLastName" />
                            <asp:BoundField HeaderText="First Name" DataField="RFirstName" />                            
                            <asp:BoundField HeaderText="Registration No" DataField="RegistrationNo" />
                            <asp:BoundField HeaderText="Registered?" DataField="Registered?" />
                            <asp:BoundField HeaderText="RelationshipTypeId" DataField="RelationshipTypeId" Visible="false" />
                            <asp:BoundField HeaderText="Relationship" DataField="RelationshipTypeDesc"/>
                            <asp:BoundField HeaderText="Relationship Date" DataField="RelationshipDate"  DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField HeaderText="SexId" DataField="SexId" Visible="false"/>
                            <asp:BoundField HeaderText="Sex" DataField="SexDesc" />
                            <asp:BoundField HeaderText="Age(yrs)" DataField="AgeYear"/>
                            <asp:BoundField HeaderText="Age(mths)" DataField="AgeMonth"/>
                            <asp:BoundField HeaderText="HivStatusId" DataField="HivStatusId" Visible="false"/>
                            <asp:BoundField HeaderText="HIVStatus" DataField="HivStatusDesc" />
                            <asp:BoundField HeaderText="Last HIV Test Date" DataField="LastHIVTestDate" DataFormatString="{0:dd-MMM-yyyy}"/>
                            <asp:BoundField HeaderText="HIVCareStatusId" DataField="HIVCareStatusId" Visible="false"/>
                            <asp:BoundField HeaderText="HIVCareStatus" DataField="HivCareStatusDesc" />

                        </Columns>
        </asp:GridView>
    </div>
</div>
