<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="AdminForms_frmAdmin_LPFTPatientTransfer" Title="Untitled Page" CodeBehind="frmAdmin_LPTFPatientTransfer.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function fnvalidate() {
            if (document.getElementById('<%=ddAnswer.ClientID %>').value == '') {
                alert('Please select AIDSRelief Site status');
                return false;
            }
        }
    </script>
    <%-- <form id="addLPTFPatientTransfer" method="post" runat="server">--%>

    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title"><asp:Label ID="lblH2" runat="server"></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">LPTF Patient Transfer :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtLPTFName" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">AIDSRelief Site :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddAnswer" runat="server" Width="99%" CssClass="form-control">
                                    <asp:ListItem Value="">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:DropDownList>
             </div>
              <asp:Label ID="lblactive" runat="server">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Status :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddStatus" runat="server" Width="99%" CssClass="form-control">
                                        <asp:ListItem Value="9">Select</asp:ListItem>
                                        <asp:ListItem Value="0">Active</asp:ListItem>
                                        <asp:ListItem Value="1">Inactive</asp:ListItem>
                                    </asp:DropDownList>
             </div>  
             </asp:Label>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">     
 <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return fnvalidate()"
                                    OnClick="btnsave_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-erase" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btncancel" runat="server" Text="Close" OnClick="btncancel_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>        
             </div>             
 </div>

        
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>
</asp:Content>
