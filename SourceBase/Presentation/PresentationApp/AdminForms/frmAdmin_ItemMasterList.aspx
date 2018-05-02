<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmAdmin_ItemMasterList.aspx.cs" Inherits="IQCare.Web.AdminForms.frmAdmin_ItemMasterList" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script type="text/javascript" language="javascript">
        function ace1_itemSelected(sender, e) {
            var hdCustID = $get('<%= hdCustID.ClientID %>');
            hdCustID.value = e.get_value();
            //alert(hdCustID.value);
        }              
    </script>
    <style type="text/css">
        /*AutoComplete flyout */
        .autocomplete_completionListElement
        {
            margin: 0px !important;
            background-color: inherit;
            color: windowtext;
            border: solid 1px #006699;
            border-width: 1px;
            border-style: solid;
            cursor: 'default';
            overflow: auto;
            height: 200px;
            width: 580px;
            font-family: Courier New, Arial, sans-serif;
            font-size: 11px;
            text-align: left;
            line-height: 15px;
            list-style-type: none;
        }
        /* AutoComplete highlighted item */
        .autocomplete_highlightedListItem
        {
            background-color: #ffff99;
            color: black;
            padding: 1px;
        }
        
        /* AutoComplete item */
        .autocomplete_listItem
        {
            background-color: window;
            color: windowtext;
            padding: 1px;
        }
        #divwidthfooter
        {
            width: 800px !important;
        }
        #divwidthfooter div
        {
            width: 800px !important;
        }
      .modalBackground 
{
  background-color:#F5F5F5;
  filter: alpha(opacity=80);
  opacity: 0.80; 
  z-index: 2000;
}
    </style>
    <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Billable Master List</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />    
          <asp:HiddenField ID="HActiveTabIndex" runat="server" Value="" />       
        </div>
        <asp:UpdatePanel runat="server" ID="updatePanelMasterItem" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                    HorizontalAlign="Left" Visible="true">
                                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                                        Text=""></asp:Label>
                                    <asp:HiddenField ID="HItemType" runat="server" Value="299" />
                                    <asp:HiddenField ID="HSubItemID" runat="server" />
                                    <asp:HiddenField ID="HItemID" runat="server" />
                                    <asp:HiddenField ID="HActionType" runat="server" Value="VIEW" />
                                </asp:Panel>
                                <div>
                                <div class="row">
 <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Main Item Type:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList ID="ddlMainItem" runat="server" Width="99%" CssClass="form-control" AutoPostBack="true" />
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group" style="white-space: nowrap; display: none">
             <label class="control-label">Item Subtype:</label>
             </div>
             <div class="col-md-2 col-sm-12 col-xs-12 form-group" style="display: none;">
             <asp:DropDownList ID="ddlSubType" runat="server" Width="99%" CssClass="form-control"/>
             </div> 
             <div class="col-md-4 col-sm-12 col-xs-12 form-group" style="display: none;" align="center">
             <asp:Button ID="btnGenerate" runat="server" Text="Filter Items" CssClass="btn btn-primary" Height="30px" Width="9%" Style="text-align: left;"/>
             <label class="glyphicon glyphicon-filter" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff;">
             <asp:Button ID="ChangeTabs" runat="server" Style="display: none;text-align: left;" CssClass="btn btn-primary" Height="30px" Width="9%" />
             </div>
 </div>

                                </div>
                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlMainItem" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <ajaxToolkit:TabContainer ID="itemMasterTab" runat="server" Width="100%" ActiveTabIndex="0"
                            AutoPostBack="false" OnClientActiveTabChanged="ActiveTabChanged" CssClass="ajax__myTab">
                            <ajaxToolkit:TabPanel ID="tabItemList" runat="server">
                                <HeaderTemplate>
                                    <b>Items</b>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:UpdatePanel runat="server" ID="divComponent" UpdateMode="Conditional">
                                        <ContentTemplate>
                                        <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div id="divConsumables" class="grid">
                                                                <div class="mid-outer">
                                                                    <div class="mid-inner">
                                                                        <div class="mid" style="cursor: pointer; height: 280px; max-height: 480px; overflow: auto;
                                                                            border: 1px solid #666699;">
                                                                            <div id="div-gridview" class="whitebg">
                                                                                <asp:GridView ID="gridItemList" CssClass="table table-bordered table-hover" CellPadding="0" runat="server"
                                                                                    AutoGenerateColumns="False" PageSize="1" BorderWidth="0px" GridLines="None" DataKeyNames="item_pk,itemtypeid,itemname"
                                                                                    EmptyDataText="No Data to display" Width="100%" ShowHeaderWhenEmpty="True">
                                                                                    <Columns>
                                                                                        <asp:CommandField ShowSelectButton="true" Visible="false" />
                                                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name" SortExpression="ItemName">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="itemtypename" HeaderText="Item Type" SortExpression="itemtypename">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                        </asp:BoundField>
                                                                                        <asp:TemplateField HeaderText="Modify" ShowHeader="False">
                                                                                            <ItemTemplate>
                                                                                                <div style='white-space: nowrap; text-align: center; display: <%# ShowHideDetailColumn(Eval("HasDetails")) %>'>
                                                                                                    <asp:Button ID="buttonItemDetails" runat="server" CausesValidation="false" CommandName="ManageDetails"
                                                                                                        Text="Details" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Blue" CssClass="btn btn-default" />
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                   
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
             </div>
            
 </div>
 <div class="row" align="center">
 <asp:Button ID="buttonRaiseItemPopup" runat="server" Style="display: none" />
                                                            <asp:Panel ID="divData" runat="server" Style="display: none; width: 580px; border: solid 1px #808080;
                                                                 z-index: 15000; overflow: hidden;">
                                                                 
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">
        <asp:Panel ID="divTitle" runat="server" Style="cursor: move;">
                                                                    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Label ID="lblActionTitle" runat="server" Text="Add / Edit Items" ForeColor="White"></asp:Label>
             </div>
            
 </div>
                                                                </asp:Panel>
        </h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />           
        </div>
        <div class="row">
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                 <label class="control-label"><i>All of the fields in this section are required.</i></label>
                                                                 </div>                                                                 
                                                                </div>
                                                                <div class="row">
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                <asp:Panel ID="panelError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                                                        HorizontalAlign="Left" Visible="true">
                                                                      <label class="control-label"> <asp:Label ID="errorLabel" runat="server" Style="font-weight: bold; color: #800000"
                                                                                    Text=""></asp:Label></label>
                                                                    </asp:Panel>
                                                                 </div>                                                                 
                                                                </div>
                                                                <div class="row">
                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
                                                                 <label class="control-label">Item Type:</label>
                                                                 </div>
                                                                 <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                                                 <label class="control-label"><asp:Label ID="labelItemMainType" runat="server" Width="99%" Text=""></asp:Label></label>
                                                                 </div>
                                                                </div>
                                                                 <div class="row">
                                                                <div class="col-md-2 col-sm-12 col-xs-12 form-group text-nowrap">
                                                                 <label class="control-label">Item Name:</label>
                                                                </div>
                                                                <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                                                <asp:TextBox ID="textItemName" runat="server" Width="99%" CssClass="form-control" AutoComplete="false"></asp:TextBox>
                                                                </div>
                                                                </div>
                                                                 <div class="row" style="display: <%= ShowHideSubTypes() %>;">
                                                                <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
                                                                 <label class="control-label">Item SubType:</label>
                                                                </div>
                                                                 <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                                                  <div id="divSubTypes" class="divborder checkbox" style="margin-top: 2px; margin-bottom: 2px;
                                                                                overflow: auto; margin-left: 0px; background-color: White;">
                                                                                <asp:RadioButtonList ID="checkListSubTypes" runat="server" AutoPostBack="false" Width="99%"
                                                                                    RepeatColumns="1" />
                                                                            </div>
                                                                 </div>
                                                                </div>
                                                                 <div class="row">
                                                                <div class="col-md-1 col-sm-12 col-xs-12 form-group text-nowrap">
                                                                 <label class="control-label">Status:</label>
                                                                </div>
                                                                <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                                                <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                                                                <asp:ListItem Value="0">InActive</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                </div>
                                                                </div>
                                                                <div class="row">
                                                                <hr style="height: 2px; color: #C0C0C0; margin: 1px; padding: 0px" />
                                                                </div>
                                                                  <div class="row" align="center">
                                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                                <asp:Button ID="buttonSubmitItem" runat="server" Text="Save" OnClick="SaveItem" CssClass="btn btn-primary"
                                                                                Height="30px" Width="15%" Style="text-align: left;" />
                                                                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                                                                vertical-align: sub; color: #fff;">
                                                                            </label>
                                                                            <asp:Button ID="buttonClose" runat="server" Text="Close" CssClass="btn btn-primary"
                                                                                Height="30px" Width="15%" Style="text-align: left;" />
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
     
                                                                
                                                                 
                                                                
                                                            </asp:Panel>
                                                            <ajaxToolkit:ModalPopupExtender ID="mpeItemPopup" runat="server" TargetControlID="buttonRaiseItemPopup"
                                                                PopupControlID="divData" BackgroundCssClass="modalBackground" DropShadow="True"
                                                                BehaviorID="addedititems" PopupDragHandleControlID="divTitle" Enabled="True"
                                                                DynamicServicePath="" CancelControlID="buttonClose" X="340" Y="1">
                                                            </ajaxToolkit:ModalPopupExtender>
            
 </div>
  <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <asp:Button ID="buttonAddItem" runat="server" Text="Add Item" OnClick="AddNewItem"
                                                                CssClass="btn btn-primary" Height="30px" Width="11%" Style="text-align: left;" />
                                                            <label class="glyphicon glyphicon-plus" style="margin-left: -3%; margin-right: 2%;
                                                                vertical-align: sub; color: #fff;">
                                                            </label>
                                                            
                                                            <asp:Button ID="Button3" runat="server" OnClick="btn_close_Click" Text="Close" CssClass="btn btn-primary"
                                                                Height="30px" Width="9%" />
                                                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                                                vertical-align: sub; color: #fff;">
 </div>
 </div>

                                            
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGenerate" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ChangeTabs" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlMainItem" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="gridItemList" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="buttonAddItem" />
                                            <asp:AsyncPostBackTrigger ControlID="buttonSubmitItem" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="divDetailsCompnent" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="buttonRaiseDetails" runat="server" Style="display: none" />
                                            <asp:Panel ID="panelDetails" runat="server" Style="display: block; width: 680px;
                                                border: solid 1px #808080; z-index: 15000; overflow: hidden;
                                                max-height: 480; min-height: 240">
                                             
                                                 <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">
        <asp:Panel ID="panelTitle" runat="server" Style="cursor: move;">
         <asp:Label ID="labelTitle" runat="server">Manage Billable Details</asp:Label> Billable:  
         <asp:Label ID="labelSelectedBillable" runat="server" Font-Bold="true"/>
                                                    
                                                </asp:Panel>
        </h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
	<div class="row" align="center">
         <br />    
          <asp:HiddenField ID="hdCustID" runat="server" />
                                                            <asp:HiddenField ID="hdItemType" runat="server" />
                                                            <asp:HiddenField ID="hdItemID" runat="server" />
                                                            <asp:HiddenField ID="hItemName" runat="server" />       
        </div>
        <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Select Item Type:</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
             <asp:DropDownList runat="server" ID="ddlBillingItemType" Width="99%" CssClass="form-control" OnSelectedIndexChanged="BillingGroupChanged"
                                                                AutoPostBack="true"></asp:DropDownList>
             </div>            
 </div>
 <div class="row">
 <div class="col-md-3 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Find Item:</label>
             </div>
             <div class="col-md-9 col-sm-12 col-xs-12 form-group">
              <asp:TextBox ID="textSearchName" runat="server" AutoPostBack="true" Width="99%" CssClass="form-control" OnTextChanged="SearchNameChanged"></asp:TextBox>
                                                            <div id="divwidthfooter" runat="server" style="z-index: 4500000" />
                                                            <ajaxToolkit:AutoCompleteExtender ID="aceSearchItems" runat="server" CompletionInterval="30"
                                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="10"
                                                                BehaviorID="AutoCompleteExFooter" EnableCaching="false" FirstRowSelected="false"
                                                                MinimumPrefixLength="3" OnClientItemSelected="ace1_itemSelected" ServiceMethod="SearchItems"
                                                                TargetControlID="textSearchName" CompletionListElementID="divwidthfooter">
                                                                <Animations>
                                                                      <OnShow>
                                                                          <Sequence>
                                                                              <OpacityAction Opacity="0" />
                                                                              <HideAction Visible="true" />
                                                                              <ScriptAction Script="var behavior = $find('AutoCompleteExFooter');if (!behavior._height) { var target = behavior.get_completionList(); behavior._height = target.offsetHeight - 2; target.style.height = '0px'; }" />
                                                                              <Parallel Duration=".4">
                                                                                <FadeIn />
                                                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteExFooter')._height" />
                                                                              </Parallel>
                                                                          </Sequence>
                                                                      </OnShow>
                                                                      <OnHide>
                                                                          <Parallel Duration=".4">
                                                                              <FadeOut />
                                                                              <Length PropertyKey="height" StartValueScript="$find('AutoCompleteExFooter')._height" EndValue="0" />
                                                                          </Parallel>
                                                                      </OnHide>
                                                                </Animations>
                                                            </ajaxToolkit:AutoCompleteExtender>
             </div>            
 </div>
 <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Billable Details:</label>
             </div>
                        
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
  <div id="divdetails" class="grid" style="padding-right:5px;">
                                                                <div class="rounded">
                                                                    <div class="mid-outer">
                                                                        <div class="mid-inner">
                                                                            <div class="mid" style="max-height: 200px; min-height: 120px; overflow: auto">
                                                                                <div id="divGridItems">
                                                                                    <asp:GridView ID="gridSelectedItems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                        BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="table table-bordered table-hover" PageIndex="1"
                                                                                        DataKeyNames="ItemID,ItemTypeID" ShowFooter="False" ShowHeaderWhenEmpty="True"
                                                                                        Width="100%" GridLines="None" EmptyDataText="No items for the billables" OnRowCommand="gridSelectedItems_RowCommand">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Item Name">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="labelItemName" runat="server" Text='<%# Bind("itemname") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="400px" />
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Item Type">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblItemType" runat="server" Text='<%# Bind("ItemTypeName") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="15%" Wrap="false" />
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ShowHeader="False">
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="DeleteItemButton" runat="server" CommandName="RemoveItem" ImageUrl="~/Images/del.gif"
                                                                                                        CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Remove Item" />
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="20px" Wrap="false" />
                                                                                            </asp:TemplateField>
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
   <asp:Button ID="buttonSaveBillable" runat="server" Text="Save" OnClick="SaveBillableDetails"
                                                                CssClass="btn btn-primary" Height="30px" Width="12%" Style="text-align: left;" />
                                                            <label class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                                                vertical-align: sub; color: #fff;">
                                                            </label>
                                                            <asp:Button ID="buttonHide" runat="server" Text="Close" CssClass="btn btn-primary"
                                                                Height="30px" Width="12%" Style="text-align: left;" />
                                                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                                                vertical-align: sub; color: #fff;">
             </div>
                        
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
                                                
                                            </asp:Panel>
                                            <ajaxToolkit:ModalPopupExtender ID="mpeBillableDetails" runat="server" TargetControlID="buttonRaiseDetails"
                                                PopupControlID="panelDetails" BackgroundCssClass="modalBackground" DropShadow="True"
                                                BehaviorID="billableDetails" PopupDragHandleControlID="panelTitle" Enabled="True"
                                                CancelControlID="buttonHide" DynamicServicePath="" X="340" Y="1">
                                            </ajaxToolkit:ModalPopupExtender>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="gridItemList" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlBillingItemType" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="buttonSaveBillable" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="5" AssociatedUpdatePanelID="divDetailsCompnent">
                                        <ProgressTemplate>
                                            <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; z-index: 300000;
                                                vertical-align: middle;">
                                                <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                                                    background-color: #FFFFC0; width: 110px; height: 24px;" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="right" valign="middle" style="width: 30px; height: 22px;">
                                                            <img src="../Images/loading.gif" height="16px" width="16px" alt="" />
                                                        </td>
                                                        <td align="left" valign="middle" style="font-weight: bold; color: #808080; width: 80px;
                                                            height: 22px; padding-left: 5px">
                                                            Processing....
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" ID="tabItemSubType">
                                <HeaderTemplate>
                                    <b>Items SubTypes</b>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:UpdatePanel runat="server" ID="divSubTypeComponent">
                                        <ContentTemplate>
                                            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="pad5 formbg border">
                                                            <div id="divSubType" class="grid">
                                                                <div class="mid-outer">
                                                                    <div class="mid-inner">
                                                                        <div class="mid" style="cursor: pointer; height: 280px; max-height: 480px; overflow: auto;
                                                                            border: 1px solid #666699;">
                                                                            <div id="div-gridview-2" class="whitebg">
                                                                                <asp:GridView ID="gridSubTypes" CssClass="table table-bordered table-hover" CellPadding="0" runat="server"
                                                                                    AutoGenerateColumns="False" PageSize="1" BorderWidth="0px" GridLines="None" DataKeyNames="SubItemTypeID,ItemTypeID"
                                                                                    EnableModelValidation="True" EmptyDataText="No Data to display" OnRowCommand="gridSubTypes_RowCommand"
                                                                                    OnRowDataBound="gridSubTypes_RowDataBound">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="SubTypeName" HeaderText="SubType Name" SortExpression="SubTypeName">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="itemtypename" HeaderText="Item Type" SortExpression="itemtypename">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left">
                                                                                    </HeaderStyle>
                                                                                   
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!-- SubType Popup -->
                                                            <asp:Button ID="btnRaiseSTPopup" runat="server" Style="display: none" />
                                                            <asp:Panel ID="divSubTypeData" runat="server" Style="display: block; width: 680px;
                                                                border: solid 1px #808080; background-color: #479ADA; z-index: 15000">
                                                                <asp:Panel ID="divSubTypeTitle" runat="server" Style="border: solid 1px #808080;
                                                                    margin: 0px 0px 0px 0px; cursor: move; height: 10px">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 14px">
                                                                        <tr>
                                                                            <td valign="top" colspan="2" style="font-weight: bold; padding: 3px">
                                                                                <asp:Label ID="labelSubTypeTitle" runat="server" Text="Add / Edit Items" ForeColor="White"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <table cellpadding="1" cellspacing="1" border="0" width="680px" style="border: solid 1px #808080;
                                                                    background-color: white; margin-bottom: 10px">
                                                                    <tr>
                                                                        <td colspan="2" align="left">
                                                                            <i>All of the fields in this section are required.</i>
                                                                        </td>
                                                                    </tr>
                                                                    <asp:Panel ID="panelError2" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                                                        HorizontalAlign="Left" Visible="true">
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:Label ID="labelError2" runat="server" Style="font-weight: bold; color: #800000"
                                                                                    Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <hr class="forms">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <b>Item Type:</b>
                                                                        </td>
                                                                        <td valign="top" colspan="1" style="font-weight: bold; padding: 3px" align="left">
                                                                            <asp:Label ID="labelItemTypeST" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="required">
                                                                            <b>Sub-Type Name:</b>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="textSubTypeName" runat="server" Width="180px" AutoComplete="false"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none">
                                                                        <td align="right" class="required">
                                                                            <b>Item Type:</b>
                                                                        </td>
                                                                        <td valign="top" colspan="1" style="font-weight: bold; padding: 3px" align="left">
                                                                            <asp:DropDownList ID="ddlItemType" runat="server" Width="180px" AutoPostBack="false" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="required">
                                                                            <b>Status:</b>
                                                                        </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rblSubTypeStatus" runat="server" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                                                                <asp:ListItem Value="0">InActive</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <hr style="height: 2px; color: #C0C0C0; margin: 1px; padding: 0px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" style="white-space: nowrap; padding: 5px; text-align: center; padding-top: 5px;
                                                                            padding-bottom: 5px">
                                                                            <asp:Button ID="buttonSubmitST" runat="server" Text="Save" Width="80px" OnClick="buttonSubmitST_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                            <asp:Button ID="buttonCloseST" runat="server" Text="Close" Width="80px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <ajaxToolkit:ModalPopupExtender ID="mpeSubTypePopup" runat="server" TargetControlID="btnRaiseSTPopup"
                                                                PopupControlID="divSubTypeData" BackgroundCssClass="modalBackground" DropShadow="True"
                                                                CancelControlID="buttonCloseST" BehaviorID="addeditsubtypes" PopupDragHandleControlID="divSubTypeTitle"
                                                                Enabled="True" DynamicServicePath="">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="form pad5 center" style="text-align: center">
                                                            <asp:Button ID="buttonAddSubType" runat="server" Text="Add Sub Type" OnClick="buttonAddSubType_Click"
                                                                CssClass="btn btn-primary" Height="30px" Width="14%" Style="text-align: left;" />
                                                            <label class="glyphicon glyphicon-plus" style="margin-left: -3%; margin-right: 2%;
                                                                vertical-align: sub; color: #fff;">
                                                            </label>
                                                            <asp:Button ID="Button1" runat="server" OnClick="btn_close_Click" Text="Close" CssClass="btn btn-primary"
                                                                Height="30px" Width="8%" Style="text-align: left;" />
                                                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%;
                                                                vertical-align: sub; color: #fff;">
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGenerate" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ChangeTabs" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlMainItem" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="gridSubTypes" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="buttonAddSubType" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" ID="tabItemType" Visible="False">
                                <HeaderTemplate>
                                    <b>Items Types</b>
                                </HeaderTemplate>
                                <ContentTemplate>
                                <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:UpdatePanel runat="server" ID="divTypeComponent">
                                                        <ContentTemplate>
                                                            <div id="divType" class="grid">
                                                                <div class="mid-outer">
                                                                    <div class="mid-inner">
                                                                        <div class="mid" style="cursor: pointer; height: 280px; max-height: 480px; overflow: auto;
                                                                            border: 1px solid #666699;">
                                                                            <div id="div-gridview-type" class="whitebg">
                                                                                <asp:GridView ID="gridItemTypes" CssClass="table table-bordered table-hover" CellPadding="0" runat="server"
                                                                                    AutoGenerateColumns="False" PageSize="1" BorderWidth="0px" GridLines="None" DataKeyNames="ItemTypeID"
                                                                                    EmptyDataText="No Data to display" OnRowCommand="gridItemTypes_RowCommand" OnRowDataBound="gridItemTypes_RowDataBound">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Type" SortExpression="ItemName">
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                        </asp:BoundField>
                                                                                        <asp:TemplateField HeaderText="Status">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="labelStatus" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="textstyle" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                   
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ChangeTabs" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
             </div>                 
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="Button2" runat="server" OnClick="btn_close_Click" Text="Close" CssClass="btn btn-primary" Height="30px" Width="11%" Style="text-align: left;"/>
             <label class="glyphicon glyphicon-remove-circle" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff;">
             </div>                 
 </div>
                                    
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                        <asp:UpdatePanel ID="notificationPanel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
                    z-index: 15000">
                    <asp:Panel ID="pnPopup_Title" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                        cursor: move; height: 18px; background-color: #F0F0F0;">
                        <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label class="control-label"><asp:Label ID="lblNotice" runat="server">Add Editing Item</asp:Label></label>
             </div>
                         
 </div>
                        
                    </asp:Panel>
                    <div class="row">
 <div class="col-md-6 col-sm-12 col-xs-12 form-group">
  <asp:Image ID="imgNotice" runat="server" ImageUrl="~/Images/mb_information.gif" Height="32px"
                                    Width="32px" />
            
             </div>
             <div class="col-md-6 col-sm-12 col-xs-12 form-group">
              <label class="control-label"><asp:Label ID="lblNoticeInfo" runat="server" Font-Bold="True"></asp:Label></label>
             </div>            
 </div>
                    <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <asp:Button ID="btnOkAction" runat="server" Text="OK" CssClass="btn btn-primary" Height="30px" Width="11%" Style="text-align: left;" />
             <label class="glyphicon glyphicon-ok" style="margin-left: -3%; margin-right: 2%; vertical-align: sub; color: #fff;"></label>
             </div>
                         
 </div>
                    
                </asp:Panel>
                <asp:Button ID="btn" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                    PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                    PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath=""
                    CancelControlID="btnOkAction">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
      <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
        <ProgressTemplate>
            <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; z-index: 300000;
                vertical-align: middle;">
                <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                    background-color: #FFFFC0; width: 120px; height: 24px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right" valign="middle" style="width: 30px; height: 22px;">
                            <img src="../Images/loading.gif" height="16px" width="16px" alt="" />
                        </td>
                        <td align="left" valign="middle" style="font-weight: bold; color: #808080; width: 90px;
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
   
</asp:Content>
