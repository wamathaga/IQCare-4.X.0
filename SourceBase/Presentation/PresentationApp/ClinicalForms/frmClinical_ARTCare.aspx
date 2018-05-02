<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="ClinicalForms_frmClinical_ARTCare" CodeBehind="frmClinical_ARTCare.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtarttransdate').datepicker({ autoclose: true });
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
    </script>

    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">ART Care</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <h4>Cohort</h4>
             </div>
                             
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Cohort Month:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtcohortmnth" size="10" name="CohortMonth" runat="server" readonly="readonly" class="form-control" style="width:180px" />
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label  class="control-label">Cohort Year:</label>
             </div>   
               <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <input id="txtcohortyear" size="10" name="CohortYear" runat="server" readonly="readonly" class="form-control" style="width:180px"/>
             </div>
               <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             
             </div>                   
    </div>
    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <h4>Transfer in on ART</h4>
             </div>
                             
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">ART Transfer in Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtarttransdate" clientidmode="Static"
                                                        maxlength="11" size="10" name="txtarttransdate" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">ART Transfer in From:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlarttransferinfrom" runat="Server" CssClass="form-control" Width="300"> </asp:DropDownList>
             </div> 
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"><label  class="control-label">ARVs:</label></div>
<div style="float: left;margin-right:5px;">
<input id="txttransferARVs" size="10" name="transferARVs" readonly="readonly" runat="server" class="form-control"/></div>
<div style="float: left"><asp:Button ID="btntransferARVs" runat="server" Text="..." OnClick="btnRegimen_Click" CssClass="btn btn-primary" /></div>
</div>
             
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <h4>ART Start at Another Facility</h4>
             </div>
                             
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Start ART 1st Line Regimen Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtanotherRegimendate" clientidmode="Static"
                                                       maxlength="11" size="10" name="txtanotherRegimendate" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"><label  class="control-label">Regimen:</label></div>
<div style="float: left">
<input id="txtanotherregimen" size="16" name="anotherregimen" readonly="readonly" runat="server" class="form-control"/>
                                </div>
<div style="float: left">
<asp:Button ID="btnanotherRegimen" runat="server" Text="..." OnClick="btnTransRegimen_Click" Width="40" CssClass="btn btn-primary" />
</div>
</div>

             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"><label  class="control-label">Weight :</label></div>
<div style="float: left"><input id="txtanotherwght" size="6" maxlength="6" name="anotherwght" runat="server" class="form-control" style="width:120px;" /></div>
<div style="float: left"> 
<label class="control-label">Kgs</label></div>
</div>
             
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Clinical Stage:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="lstanotherClinicalStage" runat="server" CssClass="form-control" Style="width: 100px"></asp:DropDownList>
             </div>            
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"> <label  class="control-label">CD4:</label></div>
<div style="float: left;"><input id="txtanotherCD4" size="6" name="CD4" runat="server" class="form-control"/></div>

</div>
            
             </div>     
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <div style="width: 100%; margin: 0px auto;">
              <div style="float: left;margin-right:5px;"> <label  class="control-label">CD4 %:</label></div>
<div style="float: left"><input id="txtanotherCD4percent" size="6" name="CD4percent" runat="server" class="form-control"/></div>
              </div>
             </div>                 
    </div>
    <div class="row">
    <span id="divanothpreg" runat="server">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Pregnant:</label>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlpregnantanother" runat="Server" Width="400" CssClass="form-control"> </asp:DropDownList>
             </div>
</span>                
    </div>
    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <h4>ART Start at This Facility</h4>
             </div>
                             
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Start ART 1st Line Regimen Date:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtthisRegimendate" clientidmode="Static"
                                                       maxlength="11" size="10" name="txtthisRegimendate" readonly="readonly" runat="server" data-date-format="dd-M-yyyy" style="width:110px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"> <label  class="control-label">Regimen:</label></div>
<div style="float: left"><input id="txtthisregimen" size="16" name="thisregimen" readonly="readonly" runat="server" class="form-control" /></div>
</div>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin:5px;"><label  class="control-label">Weight:</label></div>
<div style="float: left"><input id="txtthiswght" size="6" name="CohortYear" runat="server" readonly="readonly" class="form-control" /></div>
<div style="float: left"><label class="smallerlabel">Kgs</label></div>

</div>
             
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Clinical Stage:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="lstthisClinicalStage" runat="server" Style="width: 100px" Enabled="false" CssClass="form-control"> </asp:DropDownList>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"><label  class="control-label">CD4:</label></div>
<div style="float: left"><input id="txtthisCD4" size="6" name="CD4this" runat="server" readonly="readonly" class="form-control" /></div>
</div>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div style="width: 100%; margin: 0px auto;">
<div style="float: left;margin-right:5px;"><label  class="control-label">CD4 %:</label></div>
<div style="float: left"><input id="txtthisCD4percent" size="6" name="CD4percentthis" runat="server" readonly="readonly" class="form-control" /></div>
</div>
             
             </div>                      
    </div>
     <div class="row">
     <span id="spnthispreg" runat="server">
      <div class="col-md-2 col-sm-12 col-xs-12 form-group">
      <label class="control-label">Pregnant:</label>
      </div>
      <div class="col-md-6 col-sm-12 col-xs-12 form-group">
      <asp:DropDownList ID="ddlpregthis" runat="Server" Enabled="false" CssClass="form-control"> </asp:DropDownList></div>
                               
                            </span>
     </div>
      <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           <div class="grid">
                                <div class="rounded" style="padding-right:5px;">
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
                                            <div class="mid">
                                                <div id="div-gridview" class="gridviewbackup whitebg">
                                                    <asp:GridView ID="grdSubsARVs" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                        Width="100%" BorderColor="white" PageIndex="1" BorderWidth="0" GridLines="None"
                                                        CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>                                                      
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
    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           <div class="grid">
                                <div class="rounded" style="padding-right:5px;">
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
                                            <div class="mid">
                                                <div id="div-gridview2" class="gridviewbackup whitebg">
                                                    <asp:GridView ID="grdInteruptions" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                        Width="100%" BorderColor="white" PageIndex="1" BorderWidth="0" GridLines="None"
                                                        CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0">
                                                        <%--<HeaderStyle   CssClass ="tableheaderstyle" ForeColor="Blue"  Font-Underline="true"  HorizontalAlign="Left" />--%>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>                                                        
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
    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
           <asp:Button ID="btn_save" Text="Save" runat="server" OnClick="btn_save_Click" CssClass="btn btn-primary" Width="8%" Height="30px" />
           <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
                            <asp:Button ID="DQ_Check" Text="Data quality check" runat="server" OnClick="DQ_Check_Click" CssClass="btn btn-primary"  Width="15%" Height="30px"/>
                            <label class="glyphicon glyphicon-ok" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>

                            <asp:Button ID="btn_close" Text="Close" runat="server" OnClick="btn_close_Click"
                                CssClass="btn btn-primary" Width="8%" Height="30px" />
                                 <label class="glyphicon glyphicon-remove" style="margin-left: -2%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
                            <asp:Button ID="btn_print" Text="Print" runat="server" OnClientClick="WindowPrint()"
                                CssClass="btn btn-primary" Width="8%" Height="30px" OnClick="btn_print_Click" />
                                <label class="glyphicon glyphicon-print" style="margin-left: -2%; margin-right: 2%;
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

    <div style="padding-left: 8px; padding-right: 8px; width: 100%;" class="container">
        
        <br />
        
       
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0" class="table-condensed">
                <tbody>
                    <tr>
                        <td class="pad5 formbg border">
                            
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <br />
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0" class="table-condensed">
                <tbody>
                    <tr>
                        <td class="pad5 formbg border">
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="form" colspan="2">
                            <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                Wrap="true">
                            </asp:Panel>
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
