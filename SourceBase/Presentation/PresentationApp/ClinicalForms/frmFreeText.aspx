<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true" CodeBehind="frmFreeText.aspx.cs" Inherits="PresentationApp.ClinicalForms.frmFreeText" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">

<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtDate').datepicker({ autoclose: true });
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>
<script language="javascript" type="text/javascript">
    function divexpandcollapse(divname) {
        var div = document.getElementById(divname);
        var img = document.getElementById('img' + divname);
        if (div.style.display == "none") {           
            div.style.display = "inline";
            img.src = "../Images/minus.png";
        } else {
           
            div.style.display = "none";
            img.src = "../Images/plus.png";
        }

    }
    //Lenth Validation For Descriptive Heading
    function ChkLenForDescriptiveHeading() {
        var maxLen = 500;       
        var x = document.getElementById("<%=txtDescriptiveHeading.ClientID%>");
        var str = x.value;
        if (str.length > maxLen) {
            alert('Length should not exceed ' + maxLen + ' characters.');
            str = str.substring(0, str.length - 1);
            document.getElementById("<%=txtDescriptiveHeading.ClientID%>").value = str;
        }
    }
    //Lenth Validation For Description.
    function ChkLenForDescription() {
        var maxLen = 2000;
        var x = document.getElementById("<%=txtDescription.ClientID%>");
        var str = x.value;
        if (str.length > maxLen) {
            alert('Length should not exceed ' + maxLen + ' characters.');
            str = str.substring(0, str.length - 1);
            document.getElementById("<%=txtDescription.ClientID%>").value = str;
        }
    }
    //
    function CountCharacters() {
        var y = document.getElementById("<%=txtDescription.ClientID%>");
        var str13 = y.value.length;      
        document.getElementById('<%= lblCharCounter.ClientID %>').innerHTML = "<b>No. of Characters:-  </b>" + str13;
    }
    </script>
 <div class="content-wrapper">
  <div class="box-body">
   <div class="row">
    <div class="col-xs-12">
    <div class="box box-primary box-solid">
    <div class="box-header">
              <h3 class="box-title">Free Text Form</h3>
            </div>
            <!-- /.box-header -->
             <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;margin-top:10px;">
             <%--Inside Border--%>
             <asp:UpdatePanel ID="grdUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>                            
                        <div  class="row">
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                        <asp:Label style="padding-left: 10px" CssClass="label-right" id="lblDate" runat="server">
                                            Date:</asp:Label>                                        
                          </div>  
                          <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div> 
                                     <input type="text" class="form-control pull-left" id="txtDate" clientidmode="Static"
                                                       onblur="DateFormat(this,this.value,event,false,3)" onkeyup="DateFormat(this,this.value,event,false,3);"
                            onfocus="javascript:vDateType='3'" maxlength="11" size="11" runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>                                       
                                        </div>
                                        </div>  
                                         <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                         </div>                     
                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <asp:Label ID="Label1" style="padding-left: 10px" CssClass="label-right" runat="server">
                                            Descriptive Heading:</asp:Label> 
                          </div>
                          <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                       <asp:TextBox ID="txtDescriptiveHeading" runat="server" Width="380px" CssClass="form-control" onkeyup="ChkLenForDescriptiveHeading()" placeholder="Maximum limit: 500 characters"></asp:TextBox>                                        
                           </div>
                           
                                </div>
                                <div  class="row">
                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                     <asp:TextBox ID="txtDescription" runat="server" 
                                     TextMode="MultiLine" style="resize:none;width:99%" CssClass="form-control" placeholder="Comment:-Maximum limit: 2000 characters" onkeyup="ChkLenForDescription();CountCharacters();"></asp:TextBox>                                      
                                     <br />
                                    <asp:Label ID="lblCharCounter" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                          </div>
                                </div>
                         <div  class="row">
                                     <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                        <div align="center">
                                            <asp:Button ID="btnAddFreeText" Text="Add" runat="server" 
                                                CssClass="btn btn-primary" Height="30px" Width="10%" 
                                                Style="text-align: left;" onclick="btnAddFreeText_Click" />
                                         <label class="glyphicon glyphicon-open" style="margin-left: -3%; vertical-align: sub;
                                                                    color: #fff;">
                                                                </label>       
                                         </div>
                                    </div>
                             </div>
                      <div  class="row">
                     <div class="col-md-12 col-sm-12 col-xs-12 form-group"> 
                     <div class="table-responsive" style="padding:10 10 5 10">
                     
                     <asp:GridView ID="GrdFreeText" runat="server" CssClass="table table-striped table-bordered table-hover"
                        AutoGenerateColumns="false" BorderWidth="0"  CellPadding="0" CellSpacing="0" 
                             OnRowDeleting="GrdFreeText_RowDeleting" 
                             onrowdatabound="GrdFreeText_RowDataBound" 
                             DataKeyNames="Freetext_id,Description" onsorting="GrdFreeText_Sorting" AllowSorting="true">  
                             <Columns>
                            
                             <asp:BoundField DataField="ptn_pk" HeaderText="Patientid" ItemStyle-CssClass="textstyle" Visible="false" />
                              <asp:BoundField DataField="VisitId" HeaderText="VisitId" ItemStyle-CssClass="textstyle" Visible="false" />                              
                              <asp:BoundField DataField="Form_filling_date" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" Visible="true" />
                              <asp:BoundField DataField="DescriptiveHeading" HeaderText="Descriptive Heading" ItemStyle-CssClass="textstyle" Visible="true" SortExpression="DescriptiveHeading" />
                              <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-CssClass="textstyle" Visible="false" />
                              <asp:BoundField DataField="Freetext_id" HeaderText="Freetext_id" Visible="false" />
                              <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Freetext_id") %>'
                                        CommandName="Delete"><img src='../Images/del.gif' alt='Delete this' border='0' /></asp:LinkButton>
                                </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField>
            <ItemTemplate>
               <%-- <img alt="" style="cursor: pointer" src="../Images/plus.png" />--%>
                <a href="JavaScript:divexpandcollapse('div<%# Eval("Freetext_id") %>');">
                            <img id="imgdiv<%# Eval("Freetext_id") %>" width="25px" border="0" src="../Images/plus.png" alt="" /></a>     
               <tr>
<td colspan="99%">
               <div id="div<%# Eval("Freetext_id") %>" style="overflow:auto; display:none; position: relative; left: 15px;">
                   <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label>                  
                    </div>
                   </td>
                   </tr>
            
            </ItemTemplate>
        </asp:TemplateField>
                             </Columns>                     
                    </asp:GridView>
                    
                    </div>
                    </div>
                    </div>
                    <div  class="row" align="center">
                     <div class="col-md-12 col-sm-12 col-xs-12 form-group"> 
                     <asp:Button ID="btnsaveFreeText" Text="Save" Width="14%" runat="server"  Style="text-align: left;"
                        class="btn btn-primary" onclick="btnsaveFreeText_Click" Visible="false" />
                 <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -4%; margin-right: 2%;
                                vertical-align: sub; color: #fff;"></label>
                     </div>
                     </div>
                    </ContentTemplate>
                    <Triggers>
     <asp:AsyncPostBackTrigger ControlID="btnAddFreeText" EventName="Click"/> 
      <asp:AsyncPostBackTrigger ControlID="btnsaveFreeText" EventName="Click"/> 
       <asp:AsyncPostBackTrigger ControlID="GrdFreeText" EventName="Sorting"/> 
    </Triggers>
                    </asp:UpdatePanel>
                    <%--UpdateProgress--%>
                    <asp:UpdateProgress ID="updProgress"  AssociatedUpdatePanelID="grdUpdatePanel" runat="server">
                <ProgressTemplate>           
                 <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;">
                    <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                        background-color: #FFFFC0; width: 150px; height: 24px;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="right" valign="middle" style="width: 30px; height: 22px;">
                                <img src="../Images/loading.gif" height="16px" width="16px" alt="" />
                            </td>
                            <td align="left" valign="middle" style="font-weight: bold; color: #808080; width: 100px;
                                height: 22px; padding-left: 5px">
                                Processing....
                            </td>
                        </tr>
                    </table>
                </div>
                </ProgressTemplate>
        </asp:UpdateProgress>

             </div>
    </div>
    </div>
   </div>
  </div>
 </div>
</asp:Content>

