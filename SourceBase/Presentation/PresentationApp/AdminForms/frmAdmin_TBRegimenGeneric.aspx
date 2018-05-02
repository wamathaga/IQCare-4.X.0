<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="AdminForms_frmAdmin_TBRegimenGeneric" CodeBehind="frmAdmin_TBRegimenGeneric.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%-- <form id="addeditmasterdrug" method="post" runat="server">--%>
    <script language="javascript" type="text/javascript">
        function GetControl() {
            document.forms[0].submit();
        }
        function fnValidate() {
            if (document.getElementById('<%=txtRegimenName.ClientID %>').value == "") {
                NotifyMessage("Enter TB Regimen Name");
                document.getElementById('<%=txtRegimenName.ClientID %>').focus();
                return false;
            }
            else if (document.getElementById('<%=ddTrMonths.ClientID %>').value == "0") {
                NotifyMessage("Please select Month");
                document.getElementById('<%=ddTrMonths.ClientID %>').focus();
                return false;
            }
            else if (document.getElementById('<%=ddStatus.ClientID %>').value == "") {
                NotifyMessage("Please select status");
                document.getElementById('<%=ddStatus.ClientID %>').focus();
                return false;
            }
            else if (document.getElementById('<%=txtSeqNo.ClientID %>').value == "") {
                NotifyMessage("Please Enter Priority");
                document.getElementById('<%=txtSeqNo.ClientID %>').focus();
                return false;
            }

            var list = document.getElementById('<%=lstGeneric.ClientID %>');
            var listcount = list.getElementsByTagName('option');
            var blnfound = false;

            if (listcount.length > 0) {
                blnfound = true;

            }

            if (!blnfound) {
                var msg = "Please enter generic name";
                NotifyMessage(msg);
                return false;
            }

            return true;
        }
 
        </script>
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
 <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">TB Regimen Name:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtRegimenName" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div class="row">
             <div class="col-md-2"><label class="control-label">Generic:</label></div>
             <div class="col-md-2"><asp:Button ID="btnAddGeneric" runat="server" Text="..." OnClick="btnAddGeneric_Click" CssClass="btn btn-primary" /></div>
              <div class="col-md-8"><asp:ListBox ID="lstGeneric" runat="server" Height="100px" Width="250px" CssClass="ListBoxCssClass"></asp:ListBox></div>
             </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Treatment Time (Months):</label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddTrMonths" runat="server" Width="99%" CssClass="form-control">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                </asp:DropDownList>
             </div> 
 </div>
 <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Status :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddStatus" runat="server" CssClass="form-control" Width="99%">
                                    <asp:ListItem Value="">Select</asp:ListItem>
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">InActive</asp:ListItem>
                                </asp:DropDownList>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblPriority" runat="server" Text="Priority :"></asp:Label></label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtSeqNo" runat="server" CssClass="form-control" Width="99%"></asp:TextBox>
             </div> 
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">  
  <div id="divStatus" runat="server" style="display: inline">
                                </div>           
             </div>             
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">  
 <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return fnValidate();"
                                    OnClick="btnSave_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btn" runat="server" Text="OK" OnClick="btn_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;"/>
                                <label class="glyphicon glyphicon-ok" style="margin-left: -3%; margin-right: 2%;
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

    <div class="container-fluid">
        <div class="center" style="padding: 5px;">
            <div class="border center formbg">
                <br />
                <table class="table-condensed" width="100%">
                    <tbody>
                        <tr>
                            <td colspan="3">
                                <table width="100%">
                                    <tr>
                                        <td class="border center pad5 whitebg" width="50%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 50%" align="right">
                                                        <label>
                                                            </label>
                                                    </td>
                                                    <td style="width: 50%">
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="border center pad5 whitebg" width="50%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 50%" align="right">
                                                        <label>
                                                            </label>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    
                                                                </td>
                                                                <td>
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="border center pad5 whitebg" width="40%">
                                <label>
                                    </label>
                               
                            </td>
                            <td class="border center pad5 whitebg" width="30%">
                                <label class="margin">
                                    </label>
                                
                            </td>
                            <td class="border center pad5 whitebg" width="30%">
                                <table>
                                    <tr>
                                        <td style="width: 50%" align="right">
                                            
                                        </td>
                                        <td style="width: 50%">
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="border center pad5 whitebg" width="100%" colspan="3">
                               
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td align="center">
                                
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
