<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmBillingFindAddBill.aspx.cs" Inherits="IQCare.Web.Billing.frmBillingFindAddBill" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Patient Billing</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblpurpose" runat="server">Search for:</label>
             </div>
             <div class="col-md-11 col-sm-12 col-xs-12 form-group">
             <asp:RadioButtonList ID="rbtlst_findBill" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rbtlst_findBill_SelectedIndexChanged" CssClass="">
                                        <asp:ListItem id="rbt_openbills" Text="Open Bills" Selected="True"></asp:ListItem>
                                        <asp:ListItem id="rbt_patients" Text="Patient"></asp:ListItem>
                                    </asp:RadioButtonList>
             </div>
             
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Panel ID="divsearch" runat="server">   
<div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Search Criteria:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlSearchCriteria" runat="server" Width="98%" CssClass="form-control" Style="z-index: 2">
                                        </asp:DropDownList>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Value:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap">
             <asp:TextBox ID="txtSearchValue" MaxLength="50" runat="server" Enabled="False" Width="98%" CssClass="form-control"></asp:TextBox>
             <asp:Button ID="btnAdd" MaxLength="20" runat="server" Text="Add" OnClick="btnAdd_Click"> </asp:Button>
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Search Filter</label>
             </div>
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtSearchQuery" runat="server" Enabled="False" 
                                            TextMode="MultiLine" Columns="1" Rows="4" Width="98%" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="btnSearch" MaxLength="20" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"></asp:Button>
<label class="glyphicon glyphicon-search" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
<asp:Button ID="btnClear" MaxLength="20" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"></asp:Button>
<label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
 </div>
 </div>
  </asp:Panel>
             </div>             
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="grid" id="divBills" style="width: 99%;">
                            <div class="rounded">
                                <div class="mid-outer1">
                                    <div class="mid-inner">
                                        <div class="mid" style="height: 200px; overflow: auto">
                                            <div id="div-gridview" class="GridView whitebg">
                                                <asp:GridView ID="grdPatienBill" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                    Width="100%" BorderColor="white" PageIndex="1" BorderWidth="1" GridLines="None"
                                                    CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="grdPatienBill_SelectedIndexChanged"
                                                    AutoGenerateSelectButton="True" DataKeyNames="PatientID" 
                                                    onrowdatabound="grdPatienBill_RowDataBound">
                                                  
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Patient ID" DataField="ID" />
                                                        <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                                                        <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                                                        <asp:BoundField HeaderText="DOB" DataField="DOB" DataFormatString="{0:dd-MMM-yyyy}"/>
                                                        <asp:BoundField HeaderText="Bill Date" DataField="BillDate" DataFormatString="{0:dd-MMM-yyyy}"/>
                                                        <asp:BoundField HeaderText="Bill Amount" DataField="BillAmount" DataFormatString="{0:N}"/>
                                                        <asp:BoundField HeaderText="Outstanding Amount" DataField="OutStandingAmount" DataFormatString="{0:N}"/>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="bottom-outer1">
                                    <div class="bottom-inner">
                                        <div class="bottom">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
 </div>
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
    
</asp:Content>
