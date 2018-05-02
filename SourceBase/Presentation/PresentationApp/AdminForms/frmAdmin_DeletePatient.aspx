<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="frmAdmin_DeletePatient" Title="Untitled Page" CodeBehind="frmAdmin_DeletePatient.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Delete Patient</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row" align="center">
         <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">
             Patient Name: <asp:Label ID="lblpatientname" runat="server"></asp:Label>
             </label>
             </div>
                          
    </div>
     <div class="row" align="center" id="trARTNo" runat="server">        
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <label class="bold">
                                <asp:Label ID="lblenroll" runat="server"></asp:Label>
                                <asp:Label ID="lblptnenrollment" runat="server"></asp:Label>
                            </label>
                            <label class="bold">
                                <asp:Label ID="lblClinicNo" runat="server"></asp:Label>
                                <asp:Label ID="lblexistingid" runat="server"></asp:Label>
                            </label>
 </div>
 </div>
 <div class="row" align="center" id="trPMTCTNo" runat="server">        
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <label class="bold">
                                <asp:Label ID="lblanc" runat="server">ANC Number:</asp:Label>
                                <asp:Label ID="lblancno" runat="server"></asp:Label>
                            </label>
                            <label class="bold">
                                <asp:Label ID="lblpmtct" runat="server">PMTCT Number:</asp:Label>
                                <asp:Label ID="lblpmtctno" runat="server"></asp:Label>
                            </label>
                            <label class="bold">
                                <asp:Label ID="lbladmission" runat="server">Admission Number:</asp:Label>
                                <asp:Label ID="lbladmissionno" runat="server"></asp:Label>
                            </label>
                            <label class="bold">
                                <asp:Label ID="lbloutpatient" runat="server">Outpatient Number:</asp:Label>
                                <asp:Label ID="lbloutpatientno" runat="server"></asp:Label>
                            </label>
 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <asp:Button ID="btndelete" runat="server" Text="Delete" OnClick="btndelete_Click"
                                Enabled="false" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-remove" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" CssClass="btn btn-primary"
                                Height="30px" Width="8%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-backward" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;">
                            </label>
                            <asp:Button ID="theBtn" Text="OK" CssClass="btn btn-primary" runat="server" OnClick="theBtn_Click" Height="30px" Width="8%" Style="text-align: left;"/>
                            <label class="glyphicon glyphicon-ok" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
             </div>               
    </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>

    <%--<form id="deleteform" method="post" enableviewstate="true" runat="server"  title="DeleteForm" >--%>
    
</asp:Content>
