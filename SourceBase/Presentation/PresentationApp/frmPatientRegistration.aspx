<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmPatientRegistration" Title="Untitled Page" CodeBehind="frmPatientRegistration.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#TxtDOB').datepicker({ autoclose: true });
            $('#txtRegDate').datepicker({ autoclose: true });
        }
        //Calling RegisterJQuery when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);
        //Calling RegisterJQuery when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
    </script>
    <script language="javascript" type="text/javascript">
        function CalcualteAge(txtAge, txtmonth, txtDT1, txtDT2) {
            var YR1 = document.getElementById(txtDT1).value.toString().substr(7, 4);
            var YR2 = document.getElementById(txtDT2).value.toString().substr(7, 4);

            var mm1 = document.getElementById(txtDT1).value.toString().substr(3, 3);
            var mm2 = document.getElementById(txtDT2).value.toString().substr(3, 3);

            var dd1 = document.getElementById(txtDT1).value.toString().substr(0, 2);
            var dd2 = document.getElementById(txtDT2).value.toString().substr(0, 2);

            var nmm1;
            switch (mm1.toLowerCase()) {
                case "jan": nmm1 = "01";
                    break;
                case "feb": nmm1 = "02";
                    break;
                case "mar": nmm1 = "03";
                    break;
                case "apr": nmm1 = "04";
                    break;
                case "may": nmm1 = "05";
                    break;
                case "jun": nmm1 = "06";
                    break;
                case "jul": nmm1 = "07";
                    break;
                case "aug": nmm1 = "08";
                    break;
                case "sep": nmm1 = "09";
                    break;
                case "oct": nmm1 = "10";
                    break;
                case "nov": nmm1 = "11";
                    break;
                case "dec": nmm1 = "12";
                    break;
            }
            var nmm2;
            switch (mm2.toLowerCase()) {
                case "jan": nmm2 = "01";
                    break;
                case "feb": nmm2 = "02";
                    break;
                case "mar": nmm2 = "03";
                    break;
                case "apr": nmm2 = "04";
                    break;
                case "may": nmm2 = "05";
                    break;
                case "jun": nmm2 = "06";
                    break;
                case "jul": nmm2 = "07";
                    break;
                case "aug": nmm2 = "08";
                    break;
                case "sep": nmm2 = "09";
                    break;
                case "oct": nmm2 = "10";
                    break;
                case "nov": nmm2 = "11";
                    break;
                case "dec": nmm2 = "12";
                    break;
            }
            var dt1 = nmm1 + "/" + dd1 + "/" + YR1;
            var dt2 = nmm2 + "/" + dd2 + "/" + YR2;

            var val1 = dateDiff("d", dt1, dt2, "", "") / 365;
            var val2 = Math.round((dateDiff("d", dt1, dt2, "", "") / 365));
            if (val2 > val1) {
                document.getElementById(txtAge).value = Math.round((dateDiff("d", dt1, dt2, "", "") / 365)) - 1;
                var yr = Math.round(dateDiff("d", dt1, dt2, "", "") / 365) - 1;
                document.getElementById(txtmonth).value = Math.round((dateDiff("d", dt1, dt2, "", "") - (365 * yr)) / 30);
            }
            else {
                document.getElementById(txtAge).value = Math.round((dateDiff("d", dt1, dt2, "", "") / 365));
                var yr = Math.round(dateDiff("d", dt1, dt2, "", "") / 365);
                document.getElementById(txtmonth).value = Math.round((dateDiff("d", dt1, dt2, "", "") - (365 * yr)) / 30);
            }
        }

        function Button1_onclick() {

        }

        function Button2_onclick() {

        }

        function Button1_onclick() {

        }

        function Button2_onclick() {

        }

        function fnshow() {


            var fname = document.getElementById('<%=txtfirstName.ClientID %>').value;
            var mname = "";
            var lname = document.getElementById('<%=txtlastName.ClientID %>').value;
            var address = "";
            var phone = "";
            if (fname != '') {
                var result = frmPatientRegistration.GetDuplicateRecord(fname, mname, lname, address, phone).value;
                if (result != "") {
                    document.getElementById('search_popup').style.display = 'inline';
                    document.getElementById('showresult').innerHTML = result;
                }
                else {
                    document.getElementById('search_popup').style.display = 'none';
                }
            }

        }
        function jsAreaClose(id) {
            document.getElementById(id).style.display = 'none';
        }

        var pickedUp = new Array("", false);
        function getReadyToMove(element, evt) {
            pickedUp[0] = element;
            pickedUp[1] = true;
        }

        function checkLoadedObjects(evt) {
            if (pickedUp[1] == true) {
                var currentSelection = document.getElementById(pickedUp[0]);

                currentSelection.style.position = "absolute";
                currentSelection.style.top = (evt.clientY + 1) + "px";
                currentSelection.style.left = (evt.clientX + 1) + "px";
            }
        }

        function dropLoadedObject(evt) {
            if (pickedUp[1] == true) {
                var currentSelection = document.getElementById(pickedUp[0]);
                currentSelection.style.position = "absolute";
                currentSelection.style.top = (evt.clientY + 1) + "px";
                currentSelection.style.left = (evt.clientX + 1) + "px";

                pickedUp = new Array("", false);
            }

        }
        function WindowPrint() {
            window.print();
        }
        function CheckFileExistence() {

            var filePath = document.getElementById('<%= this.ImgfileUploader.ClientID %>').value;
            var valid = true; //var barvalid = true;

            if (filePath.length > 0) {

                var filext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
                var validExtensions = new Array();
                validExtensions[0] = 'jpg';
                validExtensions[1] = 'jpeg';
                for (var i = 0; i < validExtensions.length; i++) {
                    if (filext == validExtensions[i])
                    { valid = true; }

                }
            }


            if (valid == false) {
                alert('The file extension is not allowed!');
                return false
            }

        }

    </script>
    <div style="padding-top: 1px;">
        <asp:UpdatePanel ID="UpdateMasterLink" runat="server">
            <ContentTemplate>
                <div class="content-wrapper">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="box box-primary box-solid">
                                    <div class="box-header">
                                        <h3 class="box-title">
                                            IQCare Registration</h3>
                                    </div>
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-xs-8 col-md-7">
                                                <div class="row">
                                                    <div class="box-header with-border">
                                                        <h3 class="box-title" style="margin-left: 10px;">
                                                            Personal Information
                                                        </h3>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <label class="control-label">
                                                            Patient First Name:</label>
                                                    </div>
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <asp:TextBox ID="txtfirstName" MaxLength="50" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            Middle Name:</label>
                                                    </div>
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <asp:TextBox ID="txtmiddleName" MaxLength="50" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            Last Name:</label>
                                                    </div>
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <asp:TextBox ID="txtlastName" onchange="fnshow();" onblur="fnshow();" MaxLength="50"
                                                            runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            Sex:</label>
                                                    </div>
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <asp:DropDownList ID="ddgender" runat="server" class="form-control" Width="100">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            Marital Status:</label>
                                                    </div>
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <asp:DropDownList ID="ddmaritalStatus" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            Date of Birth:</label>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <div class="form-group">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <input type="text" class="form-control pull-left" id="TxtDOB" clientidmode="Static"
                                                                    maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
                                                        <input id="rbtndobPrecExact" onmouseup="up(this);" onfocus="up(this);" onclick="down(this)"
                                                            type="radio" value="1" name="dobPrecision" runat="server">
                                                        <span class="smalllabel">Exact </span>
                                                    </div>
                                                    <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
                                                        <input id="rbtndobPrecEstimated" onmouseup="up(this);" onfocus="up(this);" onclick="down(this)"
                                                            type="radio" value="0" name="dobPrecision" runat="server">
                                                        <span class="smalllabel">Estimated</span>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            Registration Date:</label>
                                                    </div>
                                                    <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                                        <div class="form-group">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <input type="text" class="form-control pull-left" id="txtRegDate" clientidmode="Static"
                                                                    maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            National ID:</label>
                                                    </div>
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtNationalID" MaxLength="8" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-4 col-md-5">
                                                <div class="row">
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label" style="margin-top: 65px;">
                                                            Image Upload:</label>
                                                    </div>
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <img runat="server" id="imgpatient" src="~/PatientImages/nouser.jpg" width="135"
                                                            height="150" style="border: #666699 1px solid;" />
                                                        <asp:FileUpload ID="ImgfileUploader" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            IQCare Reference:</label>
                                                    </div>
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <asp:TextBox ID="txtIQCareRef" MaxLength="11" runat="server" Width="200" TabIndex="100"
                                                            ReadOnly="true" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <label for="inputEmail3" class="control-label">
                                                            Age:</label>
                                                    </div>
                                                    <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap">
                                                                <asp:TextBox ID="txtageCurrentYears" MaxLength="2" ReadOnly="false" runat="server"
                                                                    Width="50" class="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1" style="margin-top: 7px;">
                                                                <span class="smallerlabel">yrs</span></div>
                                                            <div class="col-md-3 col-sm-12 col-xs-12 form-group text-nowrap">
                                                                <asp:TextBox ID="txtageCurrentMonths" MaxLength="2" ReadOnly="false" runat="server"
                                                                    Width="50" class="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1" style="margin-top: 7px;">
                                                                <span class="smallerlabel">mths</span></div>
                                                            <div class="col-md-4 col-sm-12 col-xs-12 form-group text-nowrap">
                                                                <asp:Button ID="btncalculate_DOB" runat="server" Text="Calculate DOB" OnClick="btncalculate_DOB_Click"
                                                                    CssClass="btn btn-info" Height="30px" Width="45%" /></div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                                        <img runat="server" id="imgbarcode" style="border: #666699 1px solid;" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box box-primary" style="background-color: #fff;">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">
                                                Contact Information</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="row">
                                                <asp:UpdatePanel runat="server" ID="upProvince" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="col-xs-8 col-md-7">
                                                            <div class="row">
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <label for="inputEmail3" class="control-label">
                                                                        Chief/Local Council:</label>
                                                                </div>
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <asp:TextBox ID="txtlocalCouncil" runat="server" class="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <label for="inputEmail3" class="control-label">
                                                                        District/County/Ward/LGA:</label>
                                                                </div>
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <asp:DropDownList ID="dddistrictName" runat="server" class="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <label for="inputEmail3" class="control-label">
                                                                        Residence/Address/PO Box:</label>
                                                                </div>
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <asp:TextBox ID="txtaddress" runat="server" class="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <label for="inputEmail3" class="control-label">
                                                                        Phone Number:</label>
                                                                </div>
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <asp:TextBox ID="txtphone" runat="server" class="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-4 col-md-5">
                                                            <div class="row">
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <label for="inputEmail3" class="control-label">
                                                                        Village/Town/City Name:</label>
                                                                </div>
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <asp:DropDownList ID="ddvillageName" runat="server" class="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <label for="inputEmail3" class="control-label">
                                                                        Province/State:</label>
                                                                </div>
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <asp:DropDownList ID="ddProvince" runat="server" class="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <label for="inputEmail3" class="control-label">
                                                                        Country:</label>
                                                                </div>
                                                                <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                                    <asp:DropDownList ID="ddCountry" runat="server" class="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="dddistrictName" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box box-primary" style="background-color: #fff;">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">
                                                Emergency Contact Information</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="row">
                                                <div class="col-xs-8 col-md-7">
                                                    <div class="row">
                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                            <label for="inputEmail3" class="control-label">
                                                                Emergency Contact Name:</label>
                                                        </div>
                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox ID="txtemergContactName" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                            <label for="inputEmail3" class="control-label">
                                                                Emergency Contact Phone Number:</label>
                                                        </div>
                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox ID="txtemergContactPhone" runat="server" class="form-control"> </asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                            <label for="inputEmail3" class="control-label">
                                                                Application Date Format</label>
                                                        </div>
                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox ID="txtSysDate" runat="server" class="form-control" Width="120" ReadOnly="true"> </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-4 col-md-5">
                                                    <div class="row">
                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                            <label for="inputEmail3" class="control-label">
                                                                Emergency Contact Relationship:</label>
                                                        </div>
                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                            <asp:DropDownList ID="ddEmergContactRelation" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                            <label for="inputEmail3" class="control-label">
                                                                Emergency Contact Address:</label>
                                                        </div>
                                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                                            <asp:TextBox ID="txtemergContactAddress" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row no-print" align="center">
                                        <div class="form-group">
                                            <div class="col-md-12" align="center">
                                                <asp:Button ID="btncontinue" runat="server" Text="Continue" OnClientClick="return CheckFileExistence()"
                                                    OnClick="btncontinue_Click" CssClass="btn btn-primary" Width="100" Style="text-align: left;" />
                                                <span class="fa fa-forward" style="margin-left: -2%; vertical-align: sub; color: #fff;
                                                    margin-right: 1%;"></span>&nbsp;
                                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" Visible="False"
                                                    OnClientClick="return CheckFileExistence()" Width="100" Style="text-align: left;" />
                                                <asp:Button ID="btncomplete" runat="server" Text="Data quality check" OnClick="btncomplete_Click"
                                                    Visible="False" Width="100" OnClientClick="return CheckFileExistence()" Style="text-align: left;" />&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click"
                                                    CssClass="btn btn-primary" Width="100" Style="text-align: left;" />
                                                <span class="fa fa-close" style="margin-left: -3%; vertical-align: sub; color: #fff;
                                                    margin-right: 2.3%;"></span>&nbsp;&nbsp;
                                                <asp:Button ID="btnPrint" Text="Print" CssClass="btn btn-primary" runat="server"
                                                    Width="100" OnClientClick="WindowPrint()" Style="text-align: left;" />
                                                <span class="fa fa-print" style="margin-left: -3%; vertical-align: sub; color: #fff;
                                                    margin-right: 1%;"></span>
                                            </div>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="border center formbg">
                    <div class="popupWindow" id='search_popup' onclick="javascript:getReadyToMove('search_popup', event);"
                        name='styled_popup' style="display: none;">
                        <table cellspacing="0" cellpadding="0" style="width: 100%" border="0">
                            <tr bgcolor="#666699">
                                <td align="right">
                                    <span style="cursor: hand" onclick="jsAreaClose('search_popup')">
                                        <img alt="Hide Popup" src="../Images/close.gif" border="0"></span>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="height: 150px">
                                    <div id="showresult">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="alert">
                                    Please check for duplicates
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="border center formbg">
                    <table cellspacing="6" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <td class="pad5 center" colspan="2" style="height: 53px">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsave"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btncomplete"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btncontinue"></asp:PostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <script language="javascript" type="text/javascript">
        if (typeof (Sys) !== 'undefined')
            Sys.Application.notifyScriptLoaded();
        var pageManager = Sys.WebForms.PageRequestManager.getInstance();
        var uiId = '';
        pageManager.add_beginRequest(myBeginRequestCallback);
        function myBeginRequestCallback(sender, args) {
            ;
            var postbackElem = args.get_postBackElement();
            uiId = postbackElem.id;
            postbackElem.disabled = true;

        }
        pageManager.add_endRequest(myEndRequestCallback);
        function myEndRequestCallback(sender, args) {
            var status = '<%=Request.QueryString["name"]%>';
            if (status != 'Edit') {
                fnshow();
            }
        }
    </script>
</asp:Content>
