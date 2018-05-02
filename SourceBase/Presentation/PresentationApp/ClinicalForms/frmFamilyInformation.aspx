<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage/IQCare.master"
    AutoEventWireup="true" Inherits="ClinicalForms_frmFamilyInformation" Title="untitled page"
    CodeBehind="frmFamilyInformation.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtLastHIVTestDate').datepicker({ autoclose: true });
        }
        //Calling RegisterJQuery when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);
        //Calling RegisterJQuery when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
    </script>
    <script language="javascript" type="text/javascript">
        //        $(window).unload(function () {
        //            alert("Handler for .unload() was called.");
        //            window.parent.FamilyPopUpClosed();
        //        });
        function OnCEClose() {
            debugger;
            window.opener.FamilyPopUpClosed();
            window.close();
        }


        function WindowPrint() {
            window.print();
        }
        function ShowRelationDt() {
            document.getElementById('divRelationDate').style.display = 'block';

        }
        function Redirect(id) {
            window.location.href = "../ClinicalForms/frmPatient_Home.aspx";
        }
        function fnDeleteMsg() {
            alert('Record Deleted Successfully');
            return false
        }
        function ShowHide(id) {
            var RegthisclinicYes = 'tdregclinic';
            var RegthisclinicNo = 'trhideHIV'
            if (id.value == 1) {
                document.getElementById(RegthisclinicYes).style.display = 'inline';
                document.getElementById(RegthisclinicNo).style.display = 'none';
            }
            else {
                document.getElementById(RegthisclinicNo).style.display = 'inline';
                document.getElementById(RegthisclinicYes).style.display = 'none';
            }
        }
        function ChkAgeMonth() {
            var v1 = document.getElementById('<%=txtAgeMonth.ClientID%>').value;
            if (v1 < 1 || v1 > 11)
                document.getElementById('<%=txtAgeMonth.ClientID%>').value = "";
        }

        function ShowHideRelationDt() {
            var v1 = document.getElementById('<%=ddlrelationtype.ClientID%>').value;
            if (v1 == 3 || v1 == 11)
                document.getElementById('divRelationDate').style.display = 'inline';
            else
                document.getElementById('divRelationDate').style.display = 'none';
        }
        //        function fnHideShowControl() {
        //            var val = document.getElementById('%=regthisclinic.ClientID %').value;

        //            if (val == 1) {
        //                document.getElementById(RegthisclinicYes).style.display = 'inline';
        //                document.getElementById(RegthisclinicNo).style.display = 'none';
        //            }
        //            else {
        //                document.getElementById(RegthisclinicNo).style.display = 'inline';
        //                document.getElementById(RegthisclinicYes).style.display = 'none';
        //            }
        //        }
        function fnValidate() {
            if (document.getElementById('<%=txtfname.ClientID%>').value == "") {
                alert("First Name cannot be blank");
                document.getElementById('<%=txtfname.ClientID%>').focus();
                return false;
            }
            else if (document.getElementById('<%=txtlname.ClientID%>').value == "") {
                alert("Last Name cannot be blank");
                document.getElementById('<%=txtlname.ClientID%>').focus();
                return false;
            }
            else if (document.getElementById('<%=ddlgender.ClientID%>').value == "0") {
                alert("sex is not selected");
                document.getElementById('<%=ddlgender.ClientID%>').focus();
                return false;
            }
            else if (document.getElementById('<%=txtAgeYear.ClientID%>').value == "") {
                alert("Age(Year) cannot be blank");
                document.getElementById('<%=txtAgeYear.ClientID%>').focus();
                return false;
            }
            else if (document.getElementById('<%=ddlhivstatus.ClientID%>').value == "0") {
                alert("Hiv Status not selected");
                document.getElementById('<%=ddlhivstatus.ClientID%>').focus();
                return false;
            }
            else if (document.getElementById('<%=ddlhivcstatus.ClientID%>').value == "0") {
                alert("Hiv Care Status not selected");
                document.getElementById('<%=ddlhivcstatus.ClientID%>').focus();
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
                            <h3 class="box-title">
                                Family Information</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                            <%--Main Content--%>
                            <div class="row" align="center">
                                <div id="divART" runat="server">
                                    <table width="100%" class="table-condensed">
                                        <tr>
                                            <td class="form" align="center">
                                                <label class="patientInfo">
                                                    Patient Name :
                                                    <asp:Label ID="lblname" runat="server" Text=""></asp:Label></label>
                                                <label class="bold">
                                                    IQ Number:
                                                    <asp:Label ID="lblIQnumber" runat="server"></asp:Label></label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="pmtct" runat="server">
                                    <table width="100%" class="table-condensed">
                                        <tr>
                                            <td>
                                                <div id="pmtctname" runat="server">
                                                    <table width="100%" class="table-condensed">
                                                        <tr>
                                                            <td class="form" align="center">
                                                                <label class="bold">
                                                                    Patient Name :
                                                                    <asp:Label ID="lblpatientnamepmtct" runat="server" Text=""></asp:Label></label>
                                                                <label class="bold">
                                                                    IQ Number:
                                                                    <asp:Label ID="lblIQnumberpmtct" runat="server"></asp:Label></label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:Panel ID="thePnlIdent" runat="server">
                                </asp:Panel>
                                <br />
                            </div>
                            <%--<div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Registered at this clinic:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="regthisclinic" name="regthisclinic" AutoPostBack="false" runat="server"
                                        Width="30%" class="form-control">
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <asp:Button ID="btnfind" Text="Find Patient" runat="server" Width="60%" OnClick="btnFind"
                                        CssClass="btn btn-primary" Height="30px" Style="text-align: left;" />
                                    <label class="glyphicon glyphicon-search" style="vertical-align: sub; margin-left: 0%;
                                        color: #fff;">
                                    </label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Relative First Name:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="txtfname" MaxLength="50" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Relative Last Name:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="txtlname" MaxLength="50" runat="server" CssClass="form-control"
                                        Width="98%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Sex:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddlgender" runat="server" class="form-control" Width="35%">
                                        <asp:ListItem Selected="True" Value="">-Select-</asp:ListItem>
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="16">Male</asp:ListItem>
                                        <asp:ListItem Value="17">Female</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Age:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <div class="row" style="margin-left: -4px;">
                                        <table class="table-condensed" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtAgeYear" MaxLength="2" runat="server" Width="30%" class="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <span class="smallerlabel" style="margin-left: -230px">yrs</span>
                                                </td>
                                                <td>
                                                    <div style="margin-left: -190px">
                                                        <asp:TextBox ID="txtAgeMonth" MaxLength="2" runat="server" Width="30%" class="form-control"></asp:TextBox></div>
                                                </td>
                                                <td>
                                                    <span class="smallerlabel" style="margin-left: -150px">mths</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        Relationship Type:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddlrelationtype" onchange="ShowHideRelationDt()" runat="server"
                                        class="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div id="divRelationDate" style="display: none;">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Birth/Marriage Date:</label>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                        <input id="txtRelationDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                            onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                            maxlength="11" size="8" name="RelationDate" runat="server" />
                                        <img id="appDateimg1" onclick="w_displayDatePicker('<%=txtRelationDate.ClientID%>');"
                                            height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                            border="0" name="appDateimg" style="vertical-align: sub;" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div id="hidstatus" runat="server">
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            HIV Status:</label>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                        <asp:DropDownList ID="ddlhivstatus" runat="server" class="form-control" Width="40%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                        <label for="inputEmail3" class="control-label">
                                            Last HIV Test Date:</label>
                                    </div>
                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                        <span id="spnHIVTestDate">
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtLastHIVTestDate" clientidmode="Static"
                                                        onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                        onfocus="javascript:vDateType='3'" maxlength="11" size="8" runat="server" data-date-format="dd-M-yyyy"
                                                        name="LastHIVTestDate" style="width: 120px;" />
                                                </div>
                                            </div>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                        HIV Care Status:</label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddlhivcstatus" runat="server" class="form-control" Width="55%">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                    <label for="inputEmail3" class="control-label">
                                    </label>
                                </div>
                                <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    &nbsp;
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                            <asp:Button ID="btnadd" runat="server" OnClientClick="return fnValidate();" ClientIDMode="Static"
                                                Text="Add Member" OnClick="btnAdd" CssClass="btn btn-primary" Height="30px" Style="text-align: left;
                                                margin-right: 20px;" />
                                            <span class="glyphicon glyphicon-plus" style="margin-left: -3%; vertical-align: middle;
                                                color: #fff; margin-right: 1%"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <asp:Button ID="btnfind" Text="Find Patient" runat="server" OnClick="btnFind" CssClass="btn btn-primary" />
                                    <label class="glyphicon glyphicon-search" style="vertical-align: sub; color: #fff;">
                                    </label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    &nbsp;
                                </div>
                            </div>
                            <div class="row" align="center">
                                <div class="col-sm-12 col-xs-12 form-group">
                                    <div class="GridView whitebg" style="cursor: pointer; width: 100%; padding-right: 5px;">
                                        <div class="grid">
                                            <div class="rounded">
                                                <div class="top-outer">
                                                    <div class="top-inner">
                                                        <div class="top">
                                                            <h2>
                                                                Family Members</h2>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="mid-outer">
                                                    <div class="mid-inner">
                                                        <div class="mid" style="height: 300px; overflow: auto">
                                                            <div id="div-gridview" class="GridView whitebg">
                                                                <asp:GridView ID="grdFamily" runat="server" OnRowDataBound="grdFamily_RowDataBound"
                                                                    AutoGenerateColumns="False" Width="100%" AllowSorting="True" BorderWidth="0"
                                                                    GridLines="None" CssClass="table table-bordered table-hover" CellPadding="0"
                                                                    CellSpacing="0" OnSelectedIndexChanging="grdFamily_SelectedIndexChanging" OnSorting="grdFamily_Sorting"
                                                                    OnRowDeleting="grdFamily_RowDeleting">
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="bottom-outer">
                                                    <div class="bottom-inner">
                                                        <div class="bottom">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" align="center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-floppy-disk" style="vertical-align: middle; margin-left: -3%;
                                    color: #fff; margin-top: .75%; margin-right: 2%;">
                                </label>
                                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-primary"
                                    Width="8%" Height="30px" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-backward" style="margin-left: -3%; vertical-align: sub;
                                    color: #fff; margin-right: 2%;">
                                </label>
                                <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()"
                                    CssClass="btn btn-primary" Width="8%" Height="30px" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                    color: #fff;">
                                </label>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<%--</body>
</html>--%>
