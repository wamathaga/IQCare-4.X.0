<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    EnableEventValidation="false" Inherits="ClinicalForms_frmAllergy" CodeBehind="frmAllergy.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
<script type="text/javascript">
    function RegisterJQuery() {
        $('#txtAllergyDate').datepicker({ autoclose: true });
    }
    //Calling RegisterJQuery when document is ready (Page loaded first time)
    $(document).ready(RegisterJQuery);
    //Calling RegisterJQuery when the page is doing postback (asp.net)
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);
</script>

    <script type="text/javascript">
        function ace1_itemSelected(source, e) {
            var results = eval('(' + e.get_value() + ')');
            var index = source._selectIndex;
            if (index != -1) {
                var hdCustID = $get('<%= hdCustID.ClientID %>');
                hdCustID.value = results.DrugId;
            }
        }
        function onClientPopulated(sender, e) {
            var propertyPeople = sender.get_completionList().childNodes;
            for (var i = 0; i < propertyPeople.length; i++) {
                var div = document.createElement("span");
                var results = eval('(' + propertyPeople[i]._value + ')');
            }
        }
        function WindowPrint() {
            window.print();
        }

    </script>
    <style type="text/css">
        .autoextender
        {
            font-family: Courier New, Arial, sans-serif;
            font-size: 11px;
            font-weight: 100;
            border: solid 1px #006699;
            line-height: 15px;
            padding: 0px;
            background-color: White;
            margin-left: 0px;
            width: 800px;
        }
        .autoextenderlist
        {
            cursor: pointer;
            color: black;
        }
        .autoextenderhighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        #divwidth
        {
            width: 400px !important;
        }
        #divwidth div
        {
            width: 400px !important;
        }
    </style>
   
      <asp:UpdatePanel ID="Updatepanel" runat="server">
            <ContentTemplate>
            <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title"><asp:Label ID="lblFormName" runat="server" Text="Allergy Information">
        </asp:Label></h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content--%>
        
               
            <div class="row" align="center">
            <div id="divART" runat="server">
            <label class="patientInfo">
                                                            Patient Name :
                                                            <asp:Label ID="lblname" runat="server" Text=""></asp:Label></label>
                                                        <label class="bold">
                                                            IQ Number:
                                                            <asp:Label ID="lblIQnumber" runat="server"></asp:Label></label>
                                                            </div>
            </div>
             <div class="row" align="center">
             <div id="pmtct" runat="server">
             <div id="pmtctname" runat="server">
             <label class="bold">
                                                                            Patient Name :
                                                                            <asp:Label ID="lblpatientnamepmtct" runat="server" Text=""></asp:Label></label>
                                                                        <label class="bold">
                                                                            IQ Number:
                                                                            <asp:Label ID="lblIQnumberpmtct" runat="server"></asp:Label></label>
                                                                            </div>
                                                                            </div>
             </div>


             <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Allergy Type:</label>
             </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:DropDownList ID="ddlAllergyType" AutoPostBack="true" runat="server" class="form-control" OnSelectedIndexChanged="ddlAllergyType_SelectedIndexChanged">
                                        </asp:DropDownList>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">Allergen:</label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <act:AutoCompleteExtender ServiceMethod="SearchDrugs" MinimumPrefixLength="2" CompletionInterval="30"
                                            EnableCaching="false" CompletionSetCount="10" TargetControlID="txtAllergen" BehaviorID="AutoCompleteEx"
                                            OnClientItemSelected="ace1_itemSelected" ID="AutoCompleteExtender1" runat="server"
                                            OnClientPopulated="onClientPopulated" FirstRowSelected="false" CompletionListCssClass="autoextender"
                                            CompletionListItemCssClass="autoextenderlist" CompletionListHighlightedItemCssClass="autoextenderhighlight"
                                            CompletionListElementID="divwidth">
                                            <Animations>
                                              <OnShow>
                                              <Sequence>
                                              <%-- Make the completion list transparent and then show it --%>
                                              <OpacityAction Opacity="0" />
                                              <HideAction Visible="true" />

                                              <%--Cache the original size of the completion list the first time
                                                the animation is played and then set it to zero --%>
                                              <ScriptAction Script="// Cache the size and setup the initial size
                                                                            var behavior = $find('AutoCompleteEx');
                                                                            if (!behavior._height) {
                                                                                var target = behavior.get_completionList();
                                                                                behavior._height = target.offsetHeight - 2;
                                                                                target.style.height = '0px';
                                                                            }" />
                                              <%-- Expand from 0px to the appropriate size while fading in --%>
                                              <Parallel Duration=".4">
                                              <FadeIn />
                                              <Length PropertyKey="height" StartValue="0" 
	                                            EndValueScript="$find('AutoCompleteEx')._height" />
                                              </Parallel>
                                              </Sequence>
                                              </OnShow>
                                              <OnHide>
                                              <%-- Collapse down to 0px and fade out --%>
                                              <Parallel Duration=".4">
                                              <FadeOut />
                                              <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                                              </Parallel>
                                              </OnHide>
                                            </Animations>
                                        </act:AutoCompleteExtender>
            <asp:DropDownList ID="ddlAllergen" name="regthisclinic" class="form-control" AutoPostBack="true"
                                            Visible="false" Width="99%" runat="server" OnSelectedIndexChanged="ddlAllergen_SelectedIndexChanged">
                                        </asp:DropDownList>
            <label runat="server" id="lblOtherALlegian" visible="false">
                                            Other Allergen:</label>
                                        <asp:TextBox ID="txtOtherAllegian" MaxLength="50" Visible="false" Width="150px" class="form-control" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtAllergenOther" runat="server" MaxLength="50" Width="250px" class="form-control"></asp:TextBox>
                                        <asp:TextBox ID="txtAllergen" MaxLength="50" Width="400px" AutoPostBack="true" Visible="false"
                                            runat="server" AutoComplete="off" Font-Names="Courier New" OnTextChanged="txtAllergen_TextChanged" class="form-control"></asp:TextBox>
                                        <div id="divwidth">
                                        </div>
                                        <asp:HiddenField ID="hdCustID" runat="server" />
            </div>
             </div>
             <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Severity:</label>
             </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group"> <asp:DropDownList ID="ddlSeverity" Width="150px" class="form-control" runat="server">
                                        </asp:DropDownList>
            </div>
            <div class="col-md-2 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label">Date of allergy:</label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtAllergyDate" clientidmode="Static"
                                                       onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="AllergyDate"
                                            runat="server" data-date-format="dd-M-yyyy" style="width:120px;"/>
                                                </div>
                                            </div>
            </div>
             </div>
             <div class="row">
             <div class="col-md-2 col-sm-12 col-xs-12 form-group">
             <label for="inputEmail3" class="control-label">Type of Reaction:</label>
             </div>
            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <asp:TextBox ID="txtTypeOfReaction" MaxLength="50" TextMode="MultiLine" runat="server"
                                            Style="height: 60px; vertical-align: top; resize:none;" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group"><label for="inputEmail3" class="control-label"></label>
            </div>
            <div class="col-md-3 col-sm-12 col-xs-12 form-group">
            </div>
             </div>
             <div class="row" align="center">
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
               <asp:Button ID="btnadd" runat="server" OnClientClick="return fnValidate();" Text="Add Allergy"
                                            OnClick="btnAdd" CssClass="btn btn-primary" Height="30px" Width="13.5%" style="text-align:left;" />
                                        <label class="glyphicon glyphicon-plus" style="margin-left: -2.5%; margin-right: 2%;
                                            vertical-align: sub; color: #fff;">
                                        </label>
             </div>
             </div>
              <div class="row" align="center">
             <div class="col-md-12 col-sm-12 col-xs-12 form-group">
              <div class="GridView whitebg" style="cursor: pointer;">
                                            <div class="grid" style="padding-right:20px;padding-left:15px;">
                                                <div class="rounded">
                                                    <div class="top-outer">
                                                        <div class="top-inner">
                                                            <div class="top">
                                                                <h2>
                                                                    Allergy Details</h2>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="mid-outer">
                                                        <div class="mid-inner">
                                                            <div class="mid" style="height: 300px; overflow: auto">
                                                                <div id="div-gridview" class="GridView whitebg">
                                                                    <asp:GridView ID="grdAllergy" runat="server" OnRowDataBound="grdAllergy_RowDataBound"
                                                                        AutoGenerateColumns="False" Width="100%" BorderWidth="0" GridLines="None" CssClass="table table-bordered table-hover"
                                                                        CellPadding="0" CellSpacing="0" OnSelectedIndexChanging="grdAllergy_SelectedIndexChanging"
                                                                        OnRowDeleting="grdAllergy_RowDeleting">                                                                       
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
              <div class="row" align="center">
               <div class="col-md-12 col-sm-12 col-xs-12 form-group">
              <asp:Button ID="btnSubmit" runat="server" Font-Size="12px" Text="Save" OnClick="btnSubmit_Click"
                                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                        <label id="Label1" class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%; margin-right: 2%;
                                            vertical-align: sub; color: #fff;" runat="server">
                                        </label>
                                        <asp:Button ID="btnBack" runat="server" Font-Size="12px" Text="Back" OnClick="btnBack_Click"
                                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                        <label id="lblBtnBack" runat ="server" class="glyphicon glyphicon-backward" style="margin-left: -3%; margin-right: 2%;
                                            vertical-align: sub; color: #fff;">
                                        </label>
                                        <asp:Button ID="btnPrint" Text="Print" Font-Size="12px" runat="server" OnClientClick="WindowPrint()"
                                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                        <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                                            color: #fff;">
                                        </label>
              </div>
              </div>
                </div>
      </div>
      </div>
      </div>
     </div>
     </div>

            
          
         
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="txtAllergen"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnadd"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnSubmit"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnBack"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnPrint"></asp:PostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
     

</asp:Content>
