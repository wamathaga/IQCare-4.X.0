<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" Inherits="ClinicalForms_frmClinical_ARTHistory"
    EnableEventValidation="false" Codebehind="frmClinical_ARTHistory.aspx.cs" %>

<asp:Content ID="ConARTHistory" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function GetControl() {
            document.forms[0].submit();
        }

        function WindowPrint() {

            window.print();
        }

        function EnableDis(a) {
            var rdoVal = a;
            if (rdoVal.value == "Y") {
                document.getElementById('<%=ddlpurpose.ClientID%>').disabled = false;
                document.getElementById('<%=txtRegimen.ClientID%>').disabled = false;
                document.getElementById('<%=txtLastUsed.ClientID%>').disabled = false;
                document.getElementById('<%=btnRegimen.ClientID%>').disabled = false;
                document.getElementById('<%=btnAddPriorART.ClientID%>').disabled = false;
                document.getElementById('Img1').disabled = false;

                document.getElementById('<%=txtEnrolledinHIVCare.ClientID%>').disabled = false;
            }
            else {
                document.getElementById('<%=ddlpurpose.ClientID%>').disabled = true;
                document.getElementById('<%=txtRegimen.ClientID%>').disabled = true;
                document.getElementById('<%=txtLastUsed.ClientID%>').disabled = true;
                document.getElementById('<%=btnRegimen.ClientID%>').disabled = true;
                document.getElementById('<%=btnAddPriorART.ClientID%>').disabled = true;
                document.getElementById('Img1').disabled = true;
                //For disable calendar image
                document.getElementById("Img2").style.display = 'none'; //.setAttribute('disabled', 'disabled');
                document.getElementById('<%=txtEnrolledinHIVCare.ClientID%>').disabled = true;
            }
        }
            
        </script>
        <script type="text/javascript">
            function RegisterJQuery() {
                $('#txtTransferInDate').datepicker({ autoclose: true });
                $('#txtDateARTStarted').datepicker({ autoclose: true });
                $('#txtLastUsed').datepicker({ autoclose: true });
                $('#txtHIVConfirmHIVPosDate').datepicker({ autoclose: true });
                $('#txtEnrolledinHIVCare').datepicker({ autoclose: true });
            }
            //Calling RegisterJQuery when document is ready (Page loaded first time)
            $(document).ready(RegisterJQuery);
            //Calling RegisterJQuery when the page is doing postback (asp.net)
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
        
<div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">ART History</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Transfer In</h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label" for="transferInDate">Transfer In Date:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtTransferInDate" clientidmode="Static"
                                                       maxlength="11" size="11" name="txtTransferInDate" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>            
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="lblenroldate" class="control-label" for="District">From District:</label>             
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="dddistrict" runat="server" CssClass="form-control" Width="250"></asp:DropDownList>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="transferInDate" class="control-label">Facility:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddfacility" runat="server" CssClass="form-control" Width="350"></asp:DropDownList>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="Label2" class="control-label" for="District">Date Started ART:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtDateARTStarted" clientidmode="Static"
                                                       maxlength="11" size="11" name="txtTransferInDate" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>                      
    </div>
    <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Prior ART</h5>
         </div>             
            </div>
            <div class="row">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label id="lblpurpose" runat="server" class="control-label">Prior ART:</label>
             </div>
             <div class="col-md-11 col-sm-12 col-xs-12 form-group">
             <input id="rbtnknownYes" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); EnableDis(this)"
                                            type="radio" value="Y" name="PriorART" />
                                        <label for="y">
                                            Yes</label>
                                        <input id="rbtnknownNo" runat="server" onmouseup="up(this);" onfocus="up(this);" onclick="down(this); EnableDis(this)"
                                            type="radio" value="N" name="PriorART" />
                                        <label for="n">
                                            No</label>
             </div>
                                  
    </div>
    <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="Label1" runat="server">Purpose:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlpurpose" runat="server" Width="140px" Style="z-index: 2" CssClass="form-control"></asp:DropDownList>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Regimen:</label>             
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="row">
             <div class="col-md-8 text-right"><asp:TextBox ID="txtRegimen" MaxLength="50" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox></div>
             <div class="col-md-4 text-left">
             <asp:Button ID="btnRegimen" MaxLength="20" runat="server" Text="..." OnClick="btnRegimen_Click" CssClass="btn btn-primary" ></asp:Button>
              
             </div>
             </div>
             
             
             </div> 
              <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Date Last Used:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtLastUsed" clientidmode="Static"
                                                        maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             
             </div> 
                                  
    </div>
    <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group" id="divbtnPriorART">
     <asp:Button ID="btnAddPriorART" Text="Add Prior ART" runat="server" OnClick="btnAddPriorART_Click" CssClass="btn btn-primary" Height="30px" Width="12%" Style="color:White;text-align:left;" />
     <label class="glyphicon glyphicon-plus" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff; margin-top: 1%;"></label>
             </div>                                 
    </div>
    <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="grid" id="divDrugAllergyMedicalAlr" style="width: 100%;">
                                <div class="rounded">
                                  <div class="top-outer">
                                            <div class="top-inner">
                                                <div class="top">
                                                    
                                                </div>
                                            </div>
                                        </div>
                                  <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 200px; overflow: auto">
                                                    <div id="div-gridview" class="GridView whitebg">
                                                        <asp:GridView Height="50px" ID="GrdPriorART" runat="server" AutoGenerateColumns="False"
                                                        Width="100%" AllowSorting="true" BorderWidth="1" GridLines="None" CssClass="table table-bordered table-hover"
                                                        CellPadding="0" CellSpacing="0" HeaderStyle-HorizontalAlign="Left" RowStyle-CssClass="row"
                                                        OnRowDataBound="GrdPriorART_RowDataBound" OnSelectedIndexChanging="GrdPriorART_SelectedIndexChanging"
                                                        OnRowDeleting="GrdPriorART_RowDeleting">
                                                        
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
 <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">HIV Care</h5>
         </div>             
            </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
 <label class="control-label" id="lblHIVConfirmHIVPosDate">Date Confirmed HIV Positive:</label>
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtHIVConfirmHIVPosDate" clientidmode="Static"
                                                        maxlength="11" size="11" name="HIVConfirmHIVPosDate" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
                                             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label id="lblwhere" class="control-label" for="District">Where:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
<asp:TextBox ID="txtWhere" MaxLength="50" runat="server" CssClass="form-control" Width="350"></asp:TextBox>
             </div>                      
    </div>
    <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
 <label id="lblEnrolHIVCare" class="control-label">Date Enrolled in HIV Care:</label>             
             </div>
             <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtEnrolledinHIVCare" clientidmode="Static"
                                                       maxlength="11" size="11" name="EnrolledinHIVCare" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblWHOStage" for="WHOStage">WHO Stage:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlWHOStage" runat="server" Width="150px" Style="z-index: 2" CssClass="form-control"></asp:DropDownList>
             </div>                      
    </div>
    <div class="row box-header">
         <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <h5 class="box-title">Drug Allergies</h5>
         </div>             
            </div>
            <div class="row text-center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
         <textarea id="txtAreaAllergy" rows="4" cols="1" runat="server" style="width: 99%;resize:none;" class="form-control"/>
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
 <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" CssClass="btn btn-primary" Height="30px" Width="8%" Style="color:White;text-align:left;" />
 <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff; margin-top: 1%;"></label>
                            <asp:Button ID="btncomplete" runat="server" Text="Data Quality Check" OnClick="btncomplete_Click" CssClass="btn btn-primary" Height="30px" Width="15%" Style="color:White;text-align:left;" />
                            <label class="glyphicon glyphicon-ok" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff; margin-top: 1%;"></label>
                            <asp:Button ID="btnback" runat="server" Text="Close" OnClick="btnback_Click" CssClass="btn btn-primary" Height="30px" Width="7%" Style="color:White;text-align:left;"/>
                            <label class="glyphicon glyphicon-remove" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff; margin-top: 1%;"></label>
                            <asp:Button ID="btnPrint" Text="Print" OnClientClick="WindowPrint()" runat="server" CssClass="btn btn-primary" Height="30px" Width="7%" Style="color:White;text-align:left;" OnClick="btnPrint_Click" />
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
   
    
</asp:Content>
