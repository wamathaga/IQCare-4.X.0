<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    CodeBehind="frmAdmin_PredefineDrugList.aspx.cs" Inherits="PresentationApp.AdminForms.frmAdmin_PredefineDrugList" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%-- <form id="addeditDisease" method="post" runat="server">--%>
    <script type="text/javascript">
        function MoveItemUpDown(goUp) {

            var list = document.getElementById('<%= lstSelectedDrug.ClientID %>');
            //Get the current selected items index
            var selectedIndex = list.selectedIndex;

            //tell the user to select one if he hasn't
            if (selectedIndex == -1) {
                NotifyMessage("Select an item for re-ordering.");
            }
            else {
                //check if we need to move up or down
                var newIndex = selectedIndex + (goUp ? -1 : 1);
                if (newIndex < 0) {
                    //If we have reached top cycle it to end
                    newIndex = list.length - 1;
                }
                else if (newIndex >= list.length) {
                    //If we have reached end cycle it to top
                    newIndex = 0;
                }

                //Lets take the old items value and text
                var oldVal = list[selectedIndex].value;
                var oldText = list[selectedIndex].text;

                //Swap the value and text of old and new items.  
                list[selectedIndex].value = list[newIndex].value;
                list[selectedIndex].text = list[newIndex].text;
                list[newIndex].value = oldVal;
                list[newIndex].text = oldText;
                list.selectedIndex = newIndex;

                //Done, the element is moved now
            }
        }                                           

    </script>
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title"><asp:Label ID="lblHeader" runat="server" Text="Predefined Drug List"></asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
 <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <asp:ListBox ID="lstDrugList" runat="server" Height="180px" Width="300px" CssClass="ListBoxCssClass"></asp:ListBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group" style="margin-left:-130px;">
              <div class="row">
              <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"
                                                CssClass="btn btn-primary" Height="30px" Width="55%" Style="text-align: left;" />
                                            <label class="glyphicon glyphicon-plus" style="margin-left: -17%; margin-right: 4%;
                                                vertical-align: sub; color: #fff;">
                                            </label>
                                            </div>
                                            <br />
              <div class="row">
              <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_Click"
                                                CssClass="btn btn-primary" Height="30px" Width="82%" Style="text-align: left;" />
                                            <label class="glyphicon glyphicon-remove" style="margin-left: -18%; margin-right: 4%;
                                                vertical-align: sub; color: #fff;">
                                            </label>
              </div>
             </div>
             <div class="col-md-5 col-sm-12 col-xs-12 form-group">
             <asp:ListBox ID="lstSelectedDrug" runat="server" Height="180px" Width="300px" CssClass="ListBoxCssClass"></asp:ListBox>
             </div>
             <div class="col-md-1 col-sm-12 col-xs-12 form-group">
             <div class="row">
             <asp:ImageButton ID="btn_Up" Width="35px" Height="35px" runat="server" ImageUrl="~/Images/upknh.png"
                                                        OnClick="btn_Up_Click" />
             </div>
             <div class="row">
             <asp:ImageButton ID="btn_down" Width="35px" Height="35px" runat="server" ImageUrl="~/Images/downknh.png"
                                                        OnClick="btn_down_Click" />
             </div>
             </div> 
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                            Height="30px" Width="8%" Style="text-align: left;" />
                                        <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                            vertical-align: sub; color: #fff;">
                                        </label>
                                        <asp:Button ID="btnCancel" runat="server" Text="Reset" OnClick="btnCancel_Click"
                                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                        <label class="glyphicon glyphicon-erase" style="margin-left: -3%; margin-right: 2%;
                                            vertical-align: sub; color: #fff;">
                                        </label>
                                        <asp:Button ID="btnExit" runat="server" Text="Close" OnClick="btnExit_Click" CssClass="btn btn-primary"
                                            Height="30px" Width="8%" Style="text-align: left;" />
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
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnCancel"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnExit"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btn_Up"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btn_down"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnAdd"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnRemove"></asp:PostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
