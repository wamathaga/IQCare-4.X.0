<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" EnableEventValidation="false"
    AutoEventWireup="True" Inherits="frmAdmin_LabTestBoundary" Title="Untitled Page"
    CodeBehind="frmAdmin_LabTestBoundary.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<link type="text/css" href="../Style/_assets/css/grid.css" rel="stylesheet" />
<link type="text/css" href="../Style/_assets/css/round.css" rel="stylesheet" />--%>
    <script language="javascript" type="text/javascript">
        //function setVisibility(divUnit1,divBoundary1,divDefault1,divButtons1,visibility) 
        function setVisibility(visibility) {

            document.getElementById('<%= divUnit.ClientID %>').style.display = visibility;
            document.getElementById('<%= divBoundary.ClientID %>').style.display = visibility;
            document.getElementById('<%= divDefault.ClientID %>').style.display = visibility;
            document.getElementById('<%= divButtons.ClientID %>').style.display = visibility;

            document.getElementById('<%=txtLower.ClientID %>').value = "";
            document.getElementById('<%=txtUpper.ClientID %>').value = "";

            document.getElementById('<%= txtOperation.ClientID %>').value = "ADD";

            document.getElementById('<%=ddUnit.ClientID %>').disabled = false;
            document.getElementById('<%= ddUnit.ClientID %>').selectedIndex = 0;

            //alert(document.getElementById('<%= txtOperation.ClientID %>').value);
        }

        /*
        function Show1(ddlUnit)
        {
        alert("aaaa");
        document.getElementById(ddlUnit).style.visibility = "visible";
        }
    
        function Reset(txtLower)
        {
        alert("reset");
        alert(document.getElementById(txtLower));
        alert(document.getElementById(txtLower).value);
        document.getElementById(txtLower).value = '';
        }
        */
          
    </script>
    <%--  <form id="appointment" method="post" runat="server" enableviewstate="true" title="patientAppointment">--%>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title nomargin"><asp:Label ID="lblSubTest" runat="Server" Text=""></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
        <div runat="server" id="divUnit">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Units:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddUnit" runat="server" Width="99%" CssClass="form-control">
                                            </asp:DropDownList>
             </div>
             </div>
             <div id="tdDate" runat="server" visible="true">
             <div runat="server" id="divBoundary">            
             <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <div class="row">
             <div class="col-md-2"><label class="control-label">Boundary</label></div>
             <div class="col-md-1 text-right"><label class="control-label">Lower</label></div>
             <div class="col-md-3"><asp:TextBox ID="txtLower" MaxLength="11" runat="server" Width="99%" CssClass="form-control"></asp:TextBox></div>
             <div class="col-md-1 text-right"><label class="control-label">Upper</label></div>
             <div class="col-md-3"><asp:TextBox ID="txtUpper" MaxLength="11" runat="server" Width="99%" CssClass="form-control"></asp:TextBox></div>
             </div>
             </div>
             </div>
             </div>
             <div id="tddetect" runat="server" visible="true">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group text-right">
             <label  class="control-label">Undetectable Option:</label>
             </div> 
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddldetect" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div>            
             </div>
             <div runat="server" id="divDefault">
             <div class="col-md-1 col-sm-12 col-xs-12 form-group text-right">
             <label class="control-label">Default:</label>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddDefault" runat="server" Width="99%" CssClass="form-control"></asp:DropDownList>
             </div>
             </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">        
      <div class="grid" style="padding-right:5px;">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 300px; overflow: auto">
                                                <div id="div-gridview" class="GridView whitebg">
                                                    <asp:GridView ID="grdSearchResult" AllowSorting="True" runat="server" BorderWidth="0"
                                                        GridLines="None" CssClass="table table-bordered table-hover" CellPadding="0" CellSpacing="0" Width="100%"
                                                        PageSize="1" AutoGenerateColumns="false" OnSorting="grdSearchResult_Sorting"
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
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group" runat="server" id="divButtons"> 
 <asp:HiddenField ID="txtOperation" runat="server" />
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <input type="button" name="btnReset" value='Reset' onclick="setVisibility('inline');"
                                    class="btn btn-primary"  style="height:30px; width:10%;"/>
                                <label class="glyphicon glyphicon-erase" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                                <asp:Button ID="btnClose1" runat="server" Text="Close" OnClick="btnClose1_Click"
                                    CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff;">
                                </label>
                           
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
