<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="ClinicalForms_frmClinical_ARVTherapy" CodeBehind="frmClinical_ARVTherapy.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtdateEligible').datepicker({ autoclose: true });
        $('#txtanotherRegimendate').datepicker({ autoclose: true });
        $('#txtthisRegimendate').datepicker({ autoclose: true });  
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
    <script language="javascript" type="text/javascript">

        function WindowPrint() {

            window.print();

        }

        function GetControl() {
            document.forms[0].submit();
        }
        function ShowValue() {
            document.getElementById('Img1').disabled = true;
            document.getElementById('Img2').disabled = true;
            document.getElementById('Img6').disabled = true;
        }
        function CalEnbleDisble(a, b, c) {
            if (a == 0) {
                document.getElementById('Img1').disabled = true;
            }
            if (b == 0) {
                document.getElementById('Img2').disabled = true;
            }
            if (c == 0) {
                document.getElementById('Img6').disabled = true;
            }
        }
        function setMonthYear() {
            var artTransferDate = document.getElementById("<%=txtanotherRegimendate.ClientID%>").value;
            if (artTransferDate != "") {
                var arrMonthDate = artTransferDate.split('-');
                if (arrMonthDate[1] != "")
                    document.getElementById("<%=txtcohortmnth.ClientID%>").value = arrMonthDate[1];
                else
                    document.getElementById("<%=txtcohortmnth.ClientID%>").value = "";

                if (arrMonthDate[2] != "")
                    document.getElementById("<%=txtcohortyear.ClientID%>").value = arrMonthDate[2];
                else
                    document.getElementById("<%=txtcohortyear.ClientID%>").value = "";
            }
        }

        //    function EligibilityEnableDisable(ddleligibleThru) {
        //        var ControlName = document.getElementById("<%=ddleligibleThru.ClientID%>");

        //        if (ControlName.value == 330)  //it depends on which value Selection do u want to hide or show your textbox 
        //        {
        //            document.getElementById("<%=txtCD4.ClientID%>").value = '';
        //            document.getElementById("<%=txtCD4.ClientID%>").disabled = true;
        //            document.getElementById("<%=txtCD4percent.ClientID%>").value = '';
        //            document.getElementById("<%=txtCD4percent.ClientID%>").disabled = true;
        //            document.getElementById("<%=lstClinicalStage.ClientID%>").value = '0';
        //            document.getElementById("<%=lstClinicalStage.ClientID%>").disabled = false;
        //            // document.getElementById("=TextBox1.ClientID%>").style.display = 'none';


        //        }
        //        else if (ControlName.value == 331) {
        //            document.getElementById("<%=txtCD4.ClientID%>").value = '';
        //            document.getElementById("<%=txtCD4.ClientID%>").disabled = false;
        //            document.getElementById("<%=txtCD4percent.ClientID%>").value = '';
        //            document.getElementById("<%=txtCD4percent.ClientID%>").disabled = false;
        //            document.getElementById("<%=lstClinicalStage.ClientID%>").value = '0';
        //            document.getElementById("<%=lstClinicalStage.ClientID%>").disabled = true;
        //            // document.getElementById("=TextBox1.ClientID%>").style.display = 'none';
        //            //document.getElementById("=TextBox1.ClientID%>").style.display = '';
        //        }
        //        else {
        //            document.getElementById("<%=txtCD4.ClientID%>").value = '';
        //            document.getElementById("<%=txtCD4.ClientID%>").disabled = true;
        //            document.getElementById("<%=txtCD4percent.ClientID%>").value = '';
        //            document.getElementById("<%=txtCD4percent.ClientID%>").disabled = true;
        //            document.getElementById("<%=lstClinicalStage.ClientID%>").value = '0';
        //            document.getElementById("<%=lstClinicalStage.ClientID%>").disabled = true;
        //            // document.getElementById("=TextBox1.ClientID%>").style.display = 'none';
        //            //document.getElementById("=TextBox1.ClientID%>").style.display = '';
        //        }
        //    }

        function CalcualteBMI(txtBMI, txtWeight, txtHeight) {
            var weight = document.getElementById(txtWeight).value;
            var height = document.getElementById(txtHeight).value;
            if (weight == "" || height == "") {
                weight = 0;
                height = 0;
            }

            weight = parseFloat(weight);
            height = parseFloat(height);
            if (weight == 0 || height == 0) {
                document.getElementById(txtBMI).value = "";
            }
            else {
                var BMI = weight / ((height / 100) * (height / 100));
                BMI = BMI.toFixed(2);
                document.getElementById(txtBMI).value = BMI;
            }
        }

        function compareDates(dob, otherdate) {
            if (document.getElementById(otherdate).value == "") {
                return true;
            }
            var dobdd = dob.toString().substr(0, 2);
            var dobmm = dob.toString().substr(3, 3);
            var dobyr = dob.toString().substr(7, 4);
            var dmm;
            switch (dobmm.toLowerCase()) {
                case "jan": dmm = "0";
                    break;
                case "feb": dmm = "1";
                    break;
                case "mar": dmm = "2";
                    break;
                case "apr": dmm = "3";
                    break;
                case "may": dmm = "4";
                    break;
                case "jun": dmm = "5";
                    break;
                case "jul": dmm = "6";
                    break;
                case "aug": dmm = "7";
                    break;
                case "sep": dmm = "8";
                    break;
                case "oct": dmm = "9";
                    break;
                case "nov": dmm = "10";
                    break;
                case "dec": dmm = "11";
                    break;
            }
            var myDOB = new Date();
            myDOB.setFullYear(dobyr, dmm, dobdd);

            var otherdd = document.getElementById(otherdate).value.toString().substr(0, 2);
            var othermm = document.getElementById(otherdate).value.toString().substr(3, 3);
            var otheryr = document.getElementById(otherdate).value.toString().substr(7, 4);
            var omm;
            switch (othermm.toLowerCase()) {
                case "jan": omm = "0";
                    break;
                case "feb": omm = "1";
                    break;
                case "mar": omm = "2";
                    break;
                case "apr": omm = "3";
                    break;
                case "may": omm = "4";
                    break;
                case "jun": omm = "5";
                    break;
                case "jul": omm = "6";
                    break;
                case "aug": omm = "7";
                    break;
                case "sep": omm = "8";
                    break;
                case "oct": omm = "9";
                    break;
                case "nov": omm = "10";
                    break;
                case "dec": omm = "11";
                    break;
            }
            var myOther = new Date();
            myOther.setFullYear(otheryr, omm, otherdd);

            if (myDOB <= myOther) {
                return true;
            }
            else {
                alert("Date cannot be Less than Date of Birth!!");
                document.getElementById(otherdate).value = "";
                document.getElementById(otherdate).focus();
                //document.getElementById(otherdate).select();
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
        <h3 class="box-title">ART Therapy</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">ART Eligibility</h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date Medically Eligible:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtdateEligible" clientidmode="Static"
                                                        maxlength="11" size="10" name="txtarttransdate" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Eligible Through:</label>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddleligibleThru" runat="Server" OnSelectedIndexChanged="ddleligibleThru_SelectedIndexChanged" CssClass="form-control" Width="300" AutoPostBack="True">
                            </asp:DropDownList>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">WHO Stage:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="lstClinicalStage" runat="server" Style="width: 150px" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-7 col-sm-12 col-xs-12 form-group">
             <div class="row">
            <div class="col-md-2">
             <label class="control-label">CD4:</label>
             </div>
             <div class="col-md-4">
             <asp:TextBox ID="txtCD4" runat="server" Width="150px" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="col-md-2">
             <label class="control-label">CD4 %:</label>
             </div>
             <div class="col-md-4">
             <asp:TextBox ID="txtCD4percent" runat="server" Width="150px" CssClass="form-control"></asp:TextBox>
             </div>                      
    </div>
             </div>
                                
    </div>
    <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Cohort</h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Cohort Month:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <input id="txtcohortmnth" size="10" name="CohortMonth" runat="server" readonly="readonly" class="form-control" style="width:120px;"/>
             </div>
            
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Cohort Year:</label>
             </div> 
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
              <input id="txtcohortyear" size="10" name="CohortYear" runat="server" readonly="readonly" class="form-control" style="width:120px;"/>
             </div>
              <div class="col-md-3 col-sm-12 col-xs-12 form-group"></div>
                                
    </div>
    <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">ART Start at Another Facility</h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblrARTdate">Start ART 1st Line Regimen Date:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtanotherRegimendate" clientidmode="Static"
                                                      maxlength="11" size="10" name="txtanotherRegimendate" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Regimen:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div class="row">
 <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <input id="txtanotherregimen" size="16" name="anotherregimen" readonly="readonly" runat="server" class="form-control" />
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnanotherRegimen" runat="server" Text="..." OnClick="btnTransRegimen_Click" CssClass="btn btn-primary"/>
             </div>
                                   
    </div>

             
                            
             </div>                      
    </div>
     <div class="row align-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
 <label>Weight :</label>
 </div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
 <input id="txtanotherwght" size="6" maxlength="3" name="anotherwght" runat="server" class="form-control" />
 </div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
 Kgs
 </div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
 <label>Height :</label>
 </div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
 <input id="txtanotherheight" size="6" maxlength="3" name="anotherheight" runat="server" class="form-control" />
                           
 </div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
  cm
 </div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
  <label class="smallerlabel">BMI :</label>
 </div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
  <input id="txtanotherbmi" size="6" name="anotherbmi" runat="server" readonly="readonly" class="form-control" />
 </div>
 <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
 <label class="smallerlabel">WHO Stage :</label>
 </div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
 <asp:DropDownList ID="lstanotherClinicalStage" runat="server" Style="width: 120px" CssClass="form-control"></asp:DropDownList>
 </div>

 </div>
 </div>
 </div>
 <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">ART Start at This Facility</h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblthisregi">Start ART 1st Line Regimen Date:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtthisRegimendate" clientidmode="Static"
                                                        maxlength="11" size="10" name="txtthisRegimendate" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Regimen:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div class="row">
 <div class="col-md-8 col-sm-12 col-xs-12 form-group">
 <input id="txtthisregimen" size="16" name="thisregimen" readonly="readonly" runat="server" class="form-control"/>
 </div>
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnthisRegimen" runat="server" Text="..." Enabled="False" CssClass="btn btn-primary" />
 </div>
 </div>
             </div>                      
    </div>
    <div class="row align-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="row">

 <div class="col-md-1 col-sm-12 col-xs-12 form-group"><label>Weight :</label></div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group"><input id="txtthiswght" size="6" name="thisweight" runat="server" readonly="readonly" class="form-control" /></div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">Kgs</div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group"><label>Height :</label></div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group"><input id="txtthisheight" size="6" name="thisheight" runat="server" readonly="readonly" class="form-control" /></div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">cm</div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group"><label>BMI :</label></div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group"><input id="txtthisbmi" size="6" name="thisbmi" runat="server" readonly="readonly" class="form-control"/></div>
 <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right"><label>WHO Stage :</label></div>
 <div class="col-md-1 col-sm-12 col-xs-12 form-group"><asp:DropDownList ID="lstthisClinicalStage" runat="server" Style="width: 120px" Enabled="false" CssClass="form-control">
                            </asp:DropDownList></div>

 </div>
 </div>
 </div>
 <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                    <center style="color:#fff;">
                                                        Substitutions and Switches
                                                    </center>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 200px; overflow: auto">
                                                <div id="div-gridview" class="gridviewbackup whitebg">
                                                    <asp:GridView ID="grdSubsARVs" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                        Width="100%" BorderColor="white" PageIndex="1" BorderWidth="0" GridLines="None"
                                                        CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">                                                        
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Date" DataField="ChangeDate" />
                                                            <asp:BoundField HeaderText="Regimen" DataField="regimentype" />
                                                            <asp:BoundField HeaderText="Line" DataField="RegimenLine" />
                                                            <asp:BoundField HeaderText="Why" DataField="ChangeReason" />
                                                        </Columns>
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
    <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                    <center style="color:#fff;">
                                                        ART Treatment Interruptions</center>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 200px; overflow: auto">
                                                <div id="div-gridview2" class="gridviewbackup whitebg">
                                                    <asp:GridView ID="grdInteruptions" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                        Width="100%" BorderColor="white" PageIndex="1" BorderWidth="0" GridLines="None"
                                                        Height="20px" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">                                                       
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Stop/Lost" DataField="StopLost" />
                                                            <asp:BoundField HeaderText="Date" DataField="ARTEndDate" />
                                                            <asp:BoundField HeaderText="Why" DataField="Reason" />
                                                        </Columns>
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
    <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                Wrap="true">
                            </asp:Panel>
             </div>             
    </div>
     <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
            <asp:Button ID="btn_save" Text="Save" runat="server" OnClick="btn_save_Click" CssClass="btn btn-primary" Height="30px" Width="8%" Style="color:White;text-align:left;" />
            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff; margin-top: 1%;"></label>
                            <asp:Button ID="DQ_Check" Text="Data Quality check" runat="server" Visible="false" OnClick="DQ_Check_Click"  CssClass="btn btn-primary" Height="30px" Width="8%" Style="color:White;text-align:left;"/>
            
                            <asp:Button ID="btn_close" Text="Close" runat="server" OnClick="btn_close_Click"  CssClass="btn btn-primary" Height="30px" Width="8%" Style="color:White;text-align:left;"/>
                            <label class="glyphicon glyphicon-remove" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff; margin-top: 1%;"></label>
                            <asp:Button ID="btn_print" Text="Print" runat="server" OnClientClick="WindowPrint()"  CssClass="btn btn-primary" Height="30px" Width="8%" Style="color:White;text-align:left;" OnClick="btn_print_Click" />
                            <label class="glyphicon glyphicon-print" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff; margin-top: 1%;"></label>
             </div>             
    </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>
    <div style="padding-left: 8px; padding-right: 8px; width:100%;" class="container">
        <div class="border center formbg">
           
            
            <br />
        </div>
        <br />
        <div class="border center formbg">
            
        </div>
        <br />
        
        <br />
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0" class="table-condensed">
                <tbody>
                    <tr>
                        <td class="pad5 formbg border">
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="form">
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="form pad5 center" colspan="2">
                            <br />
                            
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
