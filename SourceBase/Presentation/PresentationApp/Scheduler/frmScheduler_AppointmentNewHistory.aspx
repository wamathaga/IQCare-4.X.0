<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmScheduler_AppointmentNewHistory" Title="Untitled Page" CodeBehind="frmScheduler_AppointmentNewHistory.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function CheckDate(vDateName) {

            var mYear = vDateName.value.substr(7, 4)
            if (mYear != '') {
                if (mYear < 1930) {
                    alert("Selected year should be between 1930 and Current year. Reenter..");
                    vDateName.value = "";
                    vDateName.focus();

                }
            }
        }

        function getFormattedDate(input) {
            var pattern = /(.*?)\/(.*?)\/(.*?)$/;
            var result = input.replace(pattern, function (match, p1, p2, p3) {
                var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                return (p2 < 10 ? "0" + p2 : p2) + "-" + months[(p1 - 1)] + "-" + p3;
            });
            //alert(result);
            return result;
        }

        function noOfDays(days, newDate) {
            var daysValue = document.getElementById(days).value;
            var someDate = new Date();
            var numberOfDaysToAdd = 0;
            if (daysValue == "") {
                numberOfDaysToAdd = 0;
            }
            else {
                numberOfDaysToAdd = parseInt(daysValue);
            }

            someDate.setDate(someDate.getDate() + numberOfDaysToAdd);
            //alert(someDate);
            var dd = someDate.getDate();
            var mm = someDate.getMonth() + 1;
            var y = someDate.getFullYear();

            //weekends
            var dayOfWeek = someDate.getDay();
            var isWeekend = (dayOfWeek == 6) || (dayOfWeek == 0);    // 6 = Saturday, 0 = Sunday
            if (isWeekend) {
                var ans = confirm('Selected day is a weekend. Move date to following Monday?');
                if (ans == true) {
                    if (dayOfWeek == 6) {
                        dd += 2;
                    }

                    if (dayOfWeek == 0) {
                        dd += 1;
                    }
                }


            }
            var noofdaysinmonth = daysInMonth(mm, y);
            if (dd > noofdaysinmonth) {
                dd = noofdaysinmonth - dd;
                mm = mm + 1;
            }
            var someFormattedDate = new Date(mm + '/' + Math.abs(dd) + '/' + y);

            var curr_date = someFormattedDate.getDate();
            var curr_month = someFormattedDate.getMonth() + 1;
            var curr_year = someFormattedDate.getFullYear();
            var someFormattedDate1 = curr_month + "/" + curr_date + "/" + curr_year;

            document.getElementById(newDate).value = getFormattedDate(someFormattedDate1.toString());
        }
        function daysInMonth(month, year) {
            return new Date(year, month, 0).getDate();
        }
    </script>
     <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title" id="header" runat="server">
        <asp:Label ID="lblFormName" runat="server" Text="New Appointment"></asp:Label>
        </h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content--%>
         
            <div class="row">
            <br />
            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <label for="inputEmail3" class="control-label required">
                  Appointment Date:</label>
            </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
             <div style="float: left"><asp:TextBox ID="txtAppDate" MaxLength="11" runat="server" Width="108px" class="form-control"></asp:TextBox></div>
             <div style="float: left">
              <img src="../images/cal_icon.gif" height="22" alt="Date Helper" onclick="w_displayDatePicker('<%= txtAppDate.ClientID %>');"
                            style="vertical-align: sub;" />
                        <span class="smallerlabel">(DD-MMM-YYYY)</span>
                       
             </div>
             <div style="float: left"> <asp:TextBox ID="txtNoOfDays" runat="server" Width="34px" class="form-control"></asp:TextBox>
                        </div>
                        <div style="float: left"><asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Days"></asp:Label></div>
             </div>
            </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label required">Purpose:</label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group"> 
            <asp:DropDownList ID="ddAppPurpose" runat="server" AutoPostBack="false" class="form-control">
                        </asp:DropDownList>
            </div>
            
            </div>
             <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group"> <label for="inputEmail3" class="control-label required">
                            Provider</label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddAppProvider" runat="server" AutoPostBack="false" class="form-control">
                            </asp:DropDownList>
            </div>
             </div>
             <div class="row text-center">
             <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" Enabled="True"
                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                        <label class="glyphicon glyphicon-floppy-disk" style="vertical-align: middle; margin-left: -3%;
                            color: #fff; margin-top: .75%; margin-right: 2%;">
                        </label>
                        <asp:Button ID="btndelete" runat="server" Text="Delete" OnClick="btndelete_Click"
                            Visible="false" CssClass="btn btn-primary" Height="30px" Width="8.5%" Style="text-align: left;" />
                        <label id="gly_btndelete" runat="server" class="glyphicon glyphicon-remove-circle" visible="false"
                            style="margin-left: -3%; vertical-align: middle; color: #fff; margin-right:2%;margin-top:.7%;">
                        </label>
                        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" Enabled="True"
                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                        <span class="glyphicon glyphicon-backward" style="margin-left: -3%; vertical-align: middle;
                            color: #fff;"></span>
                        <asp:Button ID="theBtn" Text="OK" Height="30px" Width="8%" Style="text-align: left;margin-left:25px;" CssClass="btn btn-primary" runat="server" OnClick="theBtn_Click" />
                        <label class="glyphicon glyphicon-ok" style="margin-left: -3%; vertical-align: sub;
                                color: #fff;">
                            </label>
                            <br />
                            <br />
             </div>
              <div class="row text-center">
              <div class="grid">
                            <div class="rounded">
                                <div class="top-outer">
                                    <div class="top-inner">
                                        <div class="top">
                                            <h2>
                                                Appointments</h2>
                                        </div>
                                    </div>
                                </div>
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid" style="cursor: pointer; overflow: auto; border: 1px solid #666699;">
                                            <div id="div-gridview" class="GridView whitebg" style="height: 300px">
                                                <asp:GridView ID="grdSearchResult" AllowSorting="True" runat="server" BorderWidth="0"
                                                    GridLines="None" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0" AutoGenerateColumns="False"
                                                    Width="100%" PageSize="1" BorderColor="white" OnSorting="grdSearchResult_Sorting"
                                                    OnRowDataBound="grdSearchResult_RowDataBound" OnSelectedIndexChanging="grdSearchResult_SelectedIndexChanging">
                                                   
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
      </div>
      </div>
     </div>
     </div>
    
    <script language="javascript" type="text/javascript">
    </script>
</asp:Content>
