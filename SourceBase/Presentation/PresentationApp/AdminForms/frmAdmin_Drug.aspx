<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="frmAdmin_Drug" Title="Untitled Page" Codebehind="frmAdmin_Drug.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id="addeditmasterdrug" method="post" runat="server">--%>
    <script language="javascript" type="text/javascript">
        function GetControl() {

            document.forms[0].submit();
        }
        function EnabDisabButton(QueryString) {
            var btnadddose = document.getElementById("<%=btnAddDose.ClientID%>");
            var btnaddfrequency = document.getElementById("<%=btnAddFrequency.ClientID%>");
            if (document.getElementById("<%=txtDrugName.ClientID%>").value != "") {
                if (btnadddose != null && btnaddfrequency != null) {
                    btnadddose.disabled = true;
                    btnaddfrequency.disabled = true;
                }
                //alert("not blank");
                //        document.getElementById("<%=btnAddDose.ClientID%>").disabled=true;
                //        document.getElementById("<%=btnAddFrequency.ClientID%>").disabled=true;
                //var coun2
                //for (count2 = 0 ; count2 < document.getElementById("<%=lstFrequency.ClientID%>").options.length ; count2++)
                //{
                //    document.getElementById("<%=lstFrequency.ClientID%>").remove (count2);
                // }
            }
            else {
                var count;

                count = document.getElementById("<%=lstGeneric.ClientID%>").options.length;
                if (count < 2) {
                    if (QueryString == "Edit") {
                        //alert(QueryString)
                        if (btnadddose != null && btnaddfrequency != null) {
                            btnadddose.disabled = true;
                            btnaddfrequency.disabled = true;
                        }
                        //                document.getElementById("<%=btnAddDose.ClientID%>").disabled=true;
                        //                document.getElementById("<%=btnAddFrequency.ClientID%>").disabled=true;
                    }
                    //            document.getElementById("<%=btnAddDose.ClientID%>").disabled=false;
                    //            document.getElementById("<%=btnAddFrequency.ClientID%>").disabled=false;
                }
                else {
                    if (btnadddose != null && btnaddfrequency != null) {
                        btnadddose.disabled = true;
                        btnaddfrequency.disabled = true;
                    }
                    //            document.getElementById("<%=btnAddDose.ClientID%>").disabled=true;
                    //            document.getElementById("<%=btnAddFrequency.ClientID%>").disabled=true;
                }
            }
        }
        </script>
  
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title"><asp:Label ID="lblH2" runat="server"></asp:Label> Drugs</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row" align="center">
         <br />           
        </div>

        <div class="row">
 <div class="col-md-4">
 <%--left1--%>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Drug type :</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddDrugTypeID" runat="server" OnSelectedIndexChanged="ddDrugType_SelectedIndexChanged" AutoPostBack="True" Width="99%" CssClass="form-control">
                                        </asp:DropDownList>
             </div>             
 </div>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Trade name :</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtDrugName" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>             
 </div>
 <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Drug abbreviation :</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtDrugAbbre" ReadOnly="true" runat="server" Width="99%" CssClass="form-control"></asp:TextBox>
             </div>             
 </div>

 </div>
 <div class="col-md-4">
 <%--left2--%>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Generic name :</label>
             </div>                     
 </div>
 <div class="row" align="center"> 
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:ListBox ID="lstGeneric" runat="server" Height="100px" Width="300px" CssClass="ListBoxCssClass"></asp:ListBox>
             </div>             
 </div>
 <div class="row" align="center"> 
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnAddGeneric" runat="server" Text="Add Generic Drug" OnClick="btnAddGeneric_Click" CssClass="btn btn-primary" Height="30px" Width="35%" Style="text-align: left;" />
             <label class="glyphicon glyphicon-plus" style="margin-left: -5%; margin-right: 2%; vertical-align: sub; color: #fff;"></label>
             </div>             
 </div>
 </div>
 <div class="col-md-4" id="arvShow" runat="server">
 <%--left3--%>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Available Strengths</label>
             </div>                     
 </div>
 <div class="row" align="center"> 
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:ListBox ID="lstStrength" runat="server" Height="100px" Width="300px" CssClass="ListBoxCssClass"></asp:ListBox>
             </div>             
 </div>
 <div class="row" align="center"> 
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnAddDose" runat="server" Text="Add Strengths" OnClick="btnAddDose_Click" CssClass="btn btn-primary" Height="30px" Width="30%" Style="text-align: left;"/>
             <label class="glyphicon glyphicon-plus" style="margin-left: -4%; margin-right: 2%; vertical-align: sub; color: #fff;"></label>
             </div>             
 </div>
 </div>
 </div>
 <div class="row">
 <div class="col-md-4" id="arvShow1" runat="server">
 <%--left1--%>
  <div class="row" align="center">
  <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Available Frequency</label>
             </div>
             
  </div>
  <div class="row" align="center">
  <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <asp:ListBox ID="lstFrequency" runat="server" Height="100px" Width="300px" CssClass="ListBoxCssClass"></asp:ListBox>
  </div>
  </div>
  <div class="row" align="center">
  <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <asp:Button ID="btnAddFrequency" runat="server" Text="Add Frequency" Enabled="false" OnClick="btnAddFrequency_Click" CssClass="btn btn-primary" Height="30px" Width="35%" Style="text-align: left;"/>
  <label class="glyphicon glyphicon-plus" style="margin-left: -5%; margin-right: 2%; vertical-align: sub; color: #fff;"></label>
  </div>
  </div>
 </div>
 <div class="col-md-4" id="vaccshow" runat="server" style="display:none">
 <%--left2--%>
 <div class="row" align="center">
  <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Available Schedule Times</label>
             </div>
             
  </div>
  <div class="row" align="center">
  <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:ListBox ID="lstshedule" runat="server" Height="100px" Width="300px" CssClass="ListBoxCssClass"></asp:ListBox>
             </div>
             
  </div>
  <div class="row" align="center">
  <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="btnAddshedule" runat="server" Text="Add Times" OnClick="btnAddshedule_Click" CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;"/>
<label class="glyphicon glyphicon-plus" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff;"></label>
             </div>
             
  </div>
 </div>
 <div class="col-md-4">
 <%--left3--%>
 </div>
 </div>
 <div class="row" id="divStatus" runat="server" style="display: inline">
 <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Status :</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddStatus" runat="server" Width="99%" CssClass="form-control">
                                        <asp:ListItem Value="0">Active</asp:ListItem>
                                        <asp:ListItem Value="1">InActive</asp:ListItem>
                                    </asp:DropDownList>
             </div>
            
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;"/>
<label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff;"></label>
<asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click"  CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;"/>
<label class="glyphicon glyphicon-remove" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff;"></label>
<asp:Button ID="btn" runat="server" Text="OK" OnClick="btn_Click"  CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;"/>
<label class="glyphicon glyphicon-ok" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff;"></label>
             </div>
              </div>
               <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div id="nonARVShow1" runat="server">
                            </div>
 </div>
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
 
    
</asp:Content>
