<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="frmadmin_adduser" Title="Untitled Page" CodeBehind="frmadmin_adduser.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            $('#chkIsEmployee').change(function () {
                if ($('#chkIsEmployee').is(":checked")) {
                    Add_Remove_Class(true);
                } else {
                    Add_Remove_Class(false);
                }
            });

        });

        function Add_Remove_Class(state) {
            if (!state) {
                $('#lblPassword').removeClass('right required').addClass('right');
                $('#lblConfirmPassword').removeClass('right required').addClass('right');
                $('#lblUserGroup').removeClass('right required').addClass('right');
            }
            else {
                $('#lblPassword').removeClass('right').addClass('right required');
                $('#lblConfirmPassword').removeClass('right').addClass('right required');
                $('#lblUserGroup').removeClass('right').addClass('right required');
            }
        }

        function validstrngpwd(fname, lname, uname) {
            //var txtnewpwdid = badword.value;
            if (!validateStrongPassword('ctl00_IQCareContentPlaceHolder_txtPassword', { length: [6, Infinity], lower: 1, upper: 1, numeric: 1, alpha: 1, special: 1, badWords: ['password', fname, lname, uname], badSequenceLength: 4 })) {
                //alert(badword.toString());
                return true;
            }
            else {
                return false;
            }
        }
        function validnewuserstrngpwd() {
            //var txtnewpwdid = badword.value;
            if (!validateStrongPassword('ctl00_IQCareContentPlaceHolder_txtPassword', { length: [6, Infinity], lower: 1, upper: 1, numeric: 1, alpha: 1, special: 1, badWords: ['password'], badSequenceLength: 4 })) {
                //alert(badword.toString());
                return true;
            }
            else {
                return false;
            }
        }

        function phonenumber(LabelId) {
            var lbl = document.getElementById(LabelId);
            var phoneno = /^\d{10}$/;

            if (document.getElementById('<%= txtPhone.ClientID%>').value == "") {
                lbl.innerHTML = '';
                return true;
            }
            else if (document.getElementById('<%= txtPhone.ClientID%>').value.match(phoneno)) {
                lbl.innerHTML = '';
                return true;
            }

            else {

                lbl.innerHTML = 'Not a valid Phone Number';
                document.getElementById('<%= txtPhone.ClientID%>').focus();
                return false;
            }
        }

        function ValidateEmail(inputText, LabelId) {
            var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            var lbl = document.getElementById(LabelId);
            if (document.getElementById('<%= txtEmail.ClientID%>').value == "") {
                lbl.innerHTML = '';
                return true;
            }
            else if (inputText.value.match(mailformat)) {
                lbl.innerHTML = '';
                return true;
            }
            else {
                lbl.innerHTML = 'You have entered an invalid email address!';
                //alert("You have entered an invalid email address!");
                document.getElementById('<%= txtEmail.ClientID%>').focus();
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
        <h3 class="box-title"><asp:Label ID="lblh2" runat="server" Text="Add User"></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="unique_id" style="display: none;">
        The Password must meet the following Requirements:</br>&nbsp; Minimum of 6 character
        length. Atleast one upper case letter.</br>&nbsp; Atleast one numeric character.</br>&nbsp;
        Atleast one non alpha character.</br>&nbsp; No more than 3 consecutive characters
        e.g '1234' or 'abcd'.</br>&nbsp; You may not use the word 'password',firstname,lastname,username.
    </div>
             </div>                                   
    </div>
    <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <input type="checkbox" id="chkIsEmployee" runat="server" clientidmode="Static" />
                                <label class="right required" for="IsEmployee">
                                    Is an Employee</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label for="LastName" style="color: red">*New users will be required to change their password upon login.</label>
             </div>
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required" for="LastName">Last Name:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtlastname" runat="server" CssClass="form-control" Width="250"></asp:TextBox>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required" for="FirstName">First Name:</label>             
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtfirstname" runat="server" CssClass="form-control" Width="250"></asp:TextBox>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required" for="UserName">User Name:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtusername" runat="server" CssClass="form-control" Width="250"></asp:TextBox>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required" for="UserName">Designation:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="dddesignation" runat="server" CssClass="form-control" Width="250"></asp:DropDownList>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            <label class="control-label required" for="passverification" id="lblPassword" clientidmode="Static" runat="server">Password:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Width="250"></asp:TextBox>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label required" for="password" id="lblConfirmPassword" runat="server" clientidmode="Static">Confirm Password:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtConfirmpassword" runat="server" TextMode="Password" CssClass="form-control" Width="250"></asp:TextBox>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" for="email">Email:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Width="250"></asp:TextBox>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" for="phone">Phone Number:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Width="250"></asp:TextBox>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label required" for="usergroup" style="vertical-align: top;" runat="server" id="lblUserGroup" clientidmode="Static">User Group:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <label class="control-label" for="usergroup" style="vertical-align: top;">Stores:</label>
             </div>
                                 
    </div>
    <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
              <div id="grdchkboldugrp" style="width: 90%; padding-left: 35px">
                                    <div nowrap='nowrap' class="divborder">
                                        <asp:CheckBoxList ID="lstUsergroup" runat="server" Width="90%" CausesValidation="True"
                                            CssClass="margin10">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <div id="grdchkboldstore" style="width: 90%;">
                                    <div nowrap='nowrap' class="divborder">
                                        <asp:CheckBoxList ID="chklistStores" runat="server" Width="90%" CausesValidation="True"
                                            CssClass="margin10">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
             </div>
                                 
    </div>
    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnDelete" runat="server" Text="Remove" OnClick="btnDelete_Click" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align:left;margin-right:40px;"/>
             <span class="glyphicon glyphicon-remove-sign" style="margin-left: -4.7%; vertical-align: middle; color: #fff;margin-right:1%"></span>
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align:left;margin-right:10px;"/>
                                <span class="glyphicon glyphicon-floppy-disk" style="margin-left: -2.2%; vertical-align: middle; color: #fff; margin-top: .25%;margin-right:1%"></span>
                                <asp:Button ID="btnCancel" runat="server" Text="Reset" OnClick="btnCancel_Click"  CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align:left;margin-right:10px;"/>
                                <span class="glyphicon glyphicon-refresh"
                                style="margin-left: -2.2%; vertical-align: middle; color: #fff; margin-top: .25%;margin-right:1%"></span>
                                <asp:Button ID="btnExit" runat="server" Text="Back" OnClick="btnExit_Click"  CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align:left;margin-right:10px;"/>
                                <span class="glyphicon glyphicon-step-backward"
                                style="margin-left: -2.2%; vertical-align: middle; color: #fff; margin-top: .25%;margin-right:1%"></span>
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
