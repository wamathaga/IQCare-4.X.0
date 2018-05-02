<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_NigeriaPriorART.ascx.cs"
    Inherits="PresentationApp.ClinicalForms.UserControl.UserControl_NigeriaPriorART" %>    
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtdurationfrom').datepicker({ autoclose: true });
            $('#txtdurationto').datepicker({ autoclose: true });
        });
</script>
<script language="javascript" type="text/javascript">
    function GetControl() {
        document.forms[0].submit();
    }

    function EnableDis(a) {
        var rdoVal = a;
        if (rdoVal.value == "Y") {
            document.getElementById('<%=ddlfacilityname.ClientID%>').disabled = false;
            document.getElementById('<%=ddlentrytype.ClientID%>').disabled = false;
            document.getElementById('<%=txtdurationfrom.ClientID%>').disabled = false;
            document.getElementById('<%=txtdurationto.ClientID%>').disabled = false;
            document.getElementById('<%=btnAddPriorART.ClientID%>').disabled = false;
            document.getElementById('Img1').disabled = false;
            document.getElementById('Img2').disabled = false;
        }
        else {
            document.getElementById('<%=ddlfacilityname.ClientID%>').disabled = true;
            document.getElementById('<%=ddlentrytype.ClientID%>').disabled = true;
            document.getElementById('<%=txtdurationfrom.ClientID%>').disabled = true;
            document.getElementById('<%=txtdurationto.ClientID%>').disabled = true;
            document.getElementById('<%=btnAddPriorART.ClientID%>').disabled = true;
            document.getElementById('Img1').disabled = true;
            document.getElementById('Img2').disabled = false;
        }
    }
    function show_Priorhide(controlID, status) {
        var s = document.getElementById(controlID);
        if (status == "notvisible") {
            s.style.display = "none";
        }
        else {
            //s.style.display = "block";
        }
    }
</script>
<asp:UpdatePanel runat="server" ID="divComponent" UpdateMode="Conditional">
    <ContentTemplate>
    <div class="row" id="divshowpreviousexposure">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label" id="lblpurpose" runat="server">Previous ARV Exposure:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
            <input id="rbtnknownYes" runat="server" onmouseup="up(this);" onfocus="up(this);"
                                        onclick="down(this); EnableDis(this)" type="radio" value="Y" name="PriorART" class="checkbox-btn"/>
                                    <label for="y">
                                        Yes</label>
                                    <input id="rbtnknownNo" runat="server" onmouseup="up(this);" onfocus="up(this);"
                                        onclick="down(this); EnableDis(this)" type="radio" value="N" name="PriorART" class="checkbox-btn"/>
                                    <label for="n">
                                        No</label>
             </div>     
              <div class="col-md-3 col-sm-12 col-xs-12 form-group">
              <label class="control-label" id="lblfacilityname" runat="server">Name of the facility:</label>
              </div>   
              <div class="col-md-4 col-sm-12 col-xs-12 form-group">
              <asp:DropDownList ID="ddlfacilityname" runat="server" Width="96%" CssClass="form-control"></asp:DropDownList>
              </div>   
 </div>
 
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">Duration of care:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
             <label class="control-label">From: </label>             
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtdurationfrom" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" 
                                                       onblur="DateFormat(this,this.value,event,false,'3')"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div>
             
           
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right" >
              <label  class="control-label">To: </label>  
             
             </div>
              <div class="col-md-1 col-sm-12 col-xs-12 form-group">             
             <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtdurationto" clientidmode="Static"
                                                       maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;" onblur="DateFormat(this,this.value,event,false,'3')"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"/>
                                                </div>
                                            </div>
             </div> 
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">             
             
             </div> 
 </div> 
 <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Entry type:</label>
             </div>
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlentrytype" runat="server" Width="98%" Style="z-index: 2" CssClass="form-control">
                                    </asp:DropDownList>
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">             
             
             </div>
             
 </div>
 <div class="row" id="divbtnPriorART" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnAddPriorART" Text="Add Prior ART" runat="server" OnClick="btnAddPriorART_Click" CssClass="btn btn-primary" Height="30px" Width="14%" Style="text-align:left;"/>
 <label class="glyphicon glyphicon-plus" style="margin-left: -3%; vertical-align: sub; color: #fff;margin-right: 1%;""></label>
 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div id="divDrugAllergyMedicalAlr" class="grid" style="width: 98%;">
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
                                                    <asp:GridView ID="GrdPriorART" runat="server" AllowSorting="true" AutoGenerateColumns="False"
                                                        BorderWidth="1" CellPadding="0" CellSpacing="0" CssClass="table table-bordered table-hover" GridLines="None"
                                                        HeaderStyle-HorizontalAlign="Left" Height="50px" OnRowDataBound="GrdPriorART_RowDataBound"
                                                        OnRowDeleting="GrdPriorART_RowDeleting" OnSelectedIndexChanging="GrdPriorART_SelectedIndexChanging"
                                                        RowStyle-CssClass="row" Width="100%">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        
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
        
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAddPriorART" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
