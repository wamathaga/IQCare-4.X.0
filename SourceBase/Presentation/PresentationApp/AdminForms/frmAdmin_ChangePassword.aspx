<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="AdminForms_frmAdmin_ChangePassword"
    Title="Untitled Page" Codebehind="frmAdmin_ChangePassword.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id="adduser" method="post" runat="server">--%>
    <style>
.alert {
    padding: 10px;
    background-color: #f44336;
    color: white;
    margin-right:4px;
}
</style>
    <script language = "javascript" type="text/javascript">
        function validstrngpwd(txtnewpwd, badword) {
            //var txtnewpwdid = badword.value;
            if (!validateStrongPassword('ctl00_IQCareContentPlaceHolder_txtNewPassword', { length: [6, Infinity], lower: 1, upper: 1, numeric: 1, alpha: 1, special: 1, badWords: ['password', badword], badSequenceLength: 4 })) {
                //alert(badword.toString());
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title"><asp:Label ID="lblh2" runat="server" Text="Change Password"></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
         </div>
          <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group text-nowrap">
 <div id="unique_id" style="display:none;">
 <div class="alert">
    <p>The Password must meet the following Requirements:</p>    
     <p>Minimum of 6 character length,
     Atleast one upper case letter,
     Atleast one numeric character,
     Atleast one non alpha character
     No more than 3 consecutive characters e.g '1234' or 'abcd'.</p>
     <p>You may not use the word 'password',firstname,lastname,username.</p>
    </div>
    </div>
 </div>
 </div>
         <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label" for="UserName">User Name:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtUserName" runat="server" MaxLength="30" CssClass="form-control" Width="98%"></asp:TextBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label" for="passverification">Existing Password:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtExisPassword" runat="server" TextMode="Password" MaxLength="30" CssClass="form-control" Width="98%"></asp:TextBox>
             </div> 
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label" for="passverification">New Password:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"  MaxLength="30" CssClass="form-control" Width="98%"></asp:TextBox>
             </div> 
              <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label" for="password">Confirm Password:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtConfirmpassword" runat="server" TextMode="Password" MaxLength="30" CssClass="form-control" Width="98%"></asp:TextBox>
             </div> 
 </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group text-nowrap">
  <asp:Button ID="btnSave" runat="server" Text="Update" OnClick="btnSave_Click" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
  <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
<asp:Button ID="btnCancel" runat="server" Text="Reset"  CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align:left;"/>
<label class="glyphicon glyphicon-refresh" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
<input id="btnExit" type="button" value="Close" runat="server" class="btn btn-primary" onserverclick="btnExit_ServerClick" style="text-align:left;height:30px; width:9%;"/>
<label class="glyphicon glyphicon-remove" style="margin-left: -3%; vertical-align: sub; color: #fff; margin-right: 2%;"></label>
 </div>
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>

    
    
      
        <div class="center" style="padding: 5px;">
            <div class="border center formbg">
                <br>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="border pad5 whitebg" width="50%">
                                <label class="right" for="UserName">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </label>
                                &nbsp;
                            </td>
                            <td class="border pad5 whitebg" width="50%">
                                <label class="right" for="existingpwd">
                                    </label>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="border pad5 whitebg" width="50%">
                                <label class="right" for="passverification">
                                    </label>
                                
                            </td>
                            <td class="border pad5 whitebg" width="50%">
                                <label class="right" for="password">
                                    </label>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 center" colspan="2">
                               
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
   
</asp:Content>
