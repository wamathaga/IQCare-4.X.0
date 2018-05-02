<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    EnableEventValidation="false" Inherits="ClinicalForms_frmNAFDAC" CodeBehind="frmNAFDAC.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ Register Src="../ClinicalForms/UserControl/UserControl_Loading.ascx" TagName="UserControl_Loading"
    TagPrefix="act" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtAllergyDateStart').datepicker({ autoclose: true });
            $('#txtAllergyDateEnd').datepicker({ autoclose: true });
            $('#txtDrugStartDate').datepicker({ autoclose: true });
            $('#txtDrugStopDate').datepicker({ autoclose: true });
            $('#txtCMDateStart').datepicker({ autoclose: true });
            $('#txtCMDateStop').datepicker({ autoclose: true });
        }
        //Calling RegisterJQuery when document is ready (Page loaded first time)
        $(document).ready(RegisterJQuery);

        //Calling RegisterJQuery when the page is doing postback (asp.net)
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RegisterJQuery);

        function getRBLValue() {
            var selected = $('#<%=rblOutcome.ClientID %> input[type=radio]:checked').val();
            if (selected == "2") {
                document.getElementById("txtORDesc").style.visibility = "visible";
                document.getElementById("lblORDesc").innerHTML = "(Recovered with disability Description)";
            }
            else if (selected == "4") {
                document.getElementById("txtORDesc").style.visibility = "visible";
                document.getElementById("lblORDesc").innerHTML = "(Life Threaterning Description)";
            }
            else if (selected == "6") {
                document.getElementById("txtORDesc").style.visibility = "visible";
                document.getElementById("lblORDesc").innerHTML = "(Others Description)";
            }
            else {
                document.getElementById("txtORDesc").style.visibility = "hidden";
                document.getElementById("lblORDesc").innerHTML = "";
            }
        }

        function getProlongedValue() {
            var selected = $('#<%=rblProlonged.ClientID %> input[type=radio]:checked').val();
            if (selected == "1") {
                document.getElementById('txtDuration').disabled = '';
            } else { document.getElementById('txtDuration').disabled = 'false'; }

        }

        $(document).ready(function ($) {
            var max = 450;

            $('textarea.max').keypress(function (e) {
                if (e.which < 0x20) {
                    // e.which < 0x20, then it's not a printable character
                    // e.which === 0 - Not a character
                    return;     // Do nothing
                }
                if (this.value.length == max) {
                    e.preventDefault();
                } else if (this.value.length > max) {
                    // Maximum exceeded
                    this.value = this.value.substring(0, max);
                }
            });
            getRBLValue();
        }); //end if ready(fn)
        
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
        function ace1_itemSelectedCM(source, e) {
            var results = eval('(' + e.get_value() + ')');
            var index = source._selectIndex;
            if (index != -1) {
                var hdCustIDCM = $get('<%= hdCustIDCM.ClientID %>');
                hdCustIDCM.value = results.DrugId;
            }
        }
        //function onClientPopulatedCM(sender, e) {
        //    var propertyPeople = sender.get_completionList().childNodes;
        //    for (var i = 0; i < propertyPeople.length; i++) {
        //        var div = document.createElement("span");
        //        var results = eval('(' + propertyPeople[i]._value + ')');
        //    }
        //}
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
        
        #divwidthCM div
        {
            width: 400px !important;
        }
        
        .RBL label
        {
            display: block;
        }
        
        .RBL td
        {
            text-align: left;
            width: 10%;
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
                                    <h3 class="box-title">
                                        <asp:Label ID="lblADR" runat="server" Text="Adverse Drug Reaction">
                                        </asp:Label></h3>
                                </div>
                                <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                                    <div class="row" style="margin-top: 1%;">
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="requiredFieldColor control-label">
                                                Description:</label>
                                        </div>
                                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                            <asp:TextBox ID="txtDescription" MaxLength="4" TextMode="MultiLine" runat="server"
                                                Style="height: 60px; vertical-align: top; resize: none;" CssClass="form-control max"
                                                placeholder="450 character maximum" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 1%;">
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="requiredFieldColor control-label">
                                                Date Reaction Started:</label>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtAllergyDateStart" clientidmode="Static"
                                                        onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                        onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="AllergyDateStart"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="requiredFieldColor control-label">
                                                Date Reaction Stopped:</label>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtAllergyDateEnd" clientidmode="Static"
                                                        onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                        onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="AllergyDateEnd"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 1%;">
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="requiredFieldColor control-label">
                                                Outcome of Reaction:</label>
                                        </div>
                                        <div class="col-md-9 col-sm-12 col-xs-12 form-group">
                                            <asp:RadioButtonList ID="rblOutcome" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Table" RepeatColumns="6" CssClass="RBL" TextAlign="Right" onclick="getRBLValue()">
                                                <asp:ListItem Text="Recovered fully" Value="1" />
                                                <asp:ListItem Text="Recovered with disability" Value="2" />
                                                <asp:ListItem Text="Congenital Abnormality" Value="3" />
                                                <asp:ListItem Text="Life Threaterning" Value="4" />
                                                <asp:ListItem Text="Death" Value="5" />
                                                <asp:ListItem Text="Others" Value="6" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 1%;">
                                        <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                            <asp:TextBox ID="txtORDesc" runat="server" ClientIDMode="Static" Style="visibility: hidden;
                                                margin-left: 20%; width: 50%;"></asp:TextBox>
                                            <asp:Label ID="lblORDesc" runat="server" ClientIDMode="Static" Style="width: 30%;
                                                margin-left: 1%;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 1%;">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Was Patient Admitted Due to ADR:</label>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <asp:RadioButtonList ID="rblReason" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table"
                                                RepeatColumns="2" CssClass="RBL" TextAlign="Right">
                                                <asp:ListItem Text="Yes" Value="1" />
                                                <asp:ListItem Text="No" Value="2" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 1%;">
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                If already Hospitalized, Was it Prolonged Due to ADR:</label>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <asp:RadioButtonList ID="rblProlonged" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Table" RepeatColumns="2" CssClass="RBL" TextAlign="Right" onclick="getProlongedValue()">
                                                <asp:ListItem Text="Yes" Value="1" />
                                                <asp:ListItem Text="No" Value="2" />
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Duration of Admission(days):</label>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <asp:TextBox ID="txtDuration" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 1%;">
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Treatment of Reaction:</label>
                                        </div>
                                        <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                            <asp:TextBox ID="txtReaction" MaxLength="450" TextMode="MultiLine" runat="server"
                                                Style="height: 60px; vertical-align: top; resize: none;" CssClass="form-control max"
                                                placeholder="450 character maximum"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-header">
                                    <h3 class="box-title">
                                        <asp:Label ID="lblFormName" runat="server" Text="Suspected Drug">
                                        </asp:Label></h3>
                                </div>
                                <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                                    <div class="row" style="margin-top: 1%;">
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="requiredFieldColor control-label">
                                                Drug:</label>
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
                                                Visible="false" Width="99%" runat="server">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtAllergen" MaxLength="50" Width="400px" AutoPostBack="true" Visible="true"
                                                runat="server" AutoComplete="off" Font-Names="Courier New" OnTextChanged="txtAllergen_TextChanged"
                                                class="form-control"></asp:TextBox>
                                            <div id="divwidth">
                                            </div>
                                            <asp:HiddenField ID="hdCustID" runat="server" />
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Generic Name</label>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <asp:Label ID="lblGeneric" class="form-control" runat="server" readonly="readonly"
                                                disabled="disabled"></asp:Label>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Batch #</label>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <asp:Label ID="lblBatchNo" class="form-control" runat="server" placeholder="N/A"
                                                readonly="readonly" disabled="disabled"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Manufacturer</label>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <asp:Label ID="lblManufacturer" class="form-control" runat="server" placeholder="N/A"
                                                readonly="readonly" disabled="disabled"></asp:Label>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Expiry Date</label>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtExpiryDate" clientidmode="Static"
                                                        onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                        onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="ExpiryDate"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" readonly="readonly"
                                                        disabled="disabled" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                NAFDAC #</label>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <asp:TextBox ID="txtNafdac" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Indication for Use:</label>
                                        </div>
                                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                                            <asp:TextBox ID="txtIndication" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Dosage</label>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <asp:TextBox ID="txtDosage" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Route</label>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <asp:TextBox ID="txtRoute" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Date Started:</label>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtDrugStartDate" clientidmode="Static"
                                                        onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                        onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="DrugStartDate"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Date Stopped:</label>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtDrugStopDate" clientidmode="Static"
                                                        onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                        onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="DrugStopDate"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" align="center">
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                            <asp:Button ID="btnadd" runat="server" OnClientClick="return fnValidate();" Text="Add Suspected Drug"
                                                OnClick="btnAdd" CssClass="btn btn-primary" Height="30px" Width="14%" Style="text-align: left;" />
                                            <label class="glyphicon glyphicon-plus" style="margin-left: -2.5%; margin-right: 2%;
                                                vertical-align: sub; color: #fff;">
                                            </label>
                                        </div>
                                    </div>
                                    <div class="row" align="center">
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                            <div class="GridView whitebg" style="cursor: pointer;">
                                                <div class="grid" style="padding-right: 20px; padding-left: 15px;">
                                                    <div class="rounded">
                                                        <div class="top-outer">
                                                            <div class="top-inner">
                                                                <div class="top">
                                                                    <h2>
                                                                        Suspected Drug Details</h2>
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
                                </div>
                                <div class="box-header">
                                    <h3 class="box-title">
                                        <asp:Label ID="lblCM" runat="server" Text="Concomitant Medicines">
                                        </asp:Label></h3>
                                </div>
                                <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                                    <div class="row" style="margin-top: 1%;">
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="requiredFieldColor control-label">
                                                Drug:</label>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                            <act:AutoCompleteExtender ServiceMethod="SearchDrugs" MinimumPrefixLength="2" CompletionInterval="30"
                                                EnableCaching="false" CompletionSetCount="10" TargetControlID="txtAllergenCM"
                                                BehaviorID="AutoCompleteExCM" OnClientItemSelected="ace1_itemSelectedCM" ID="AutoCompleteExtender2"
                                                runat="server" OnClientPopulated="onClientPopulated" FirstRowSelected="false"
                                                CompletionListCssClass="autoextender" CompletionListItemCssClass="autoextenderlist"
                                                CompletionListHighlightedItemCssClass="autoextenderhighlight" CompletionListElementID="divwidthCM">
                                                <Animations>
                                              <OnShow>
                                              <Sequence>
                                              <%-- Make the completion list transparent and then show it --%>
                                              <OpacityAction Opacity="0" />
                                              <HideAction Visible="true" />

                                              <%--Cache the original size of the completion list the first time
                                                the animation is played and then set it to zero --%>
                                              <ScriptAction Script="// Cache the size and setup the initial size
                                                                            var behavior = $find('AutoCompleteExCM');
                                                                            if (!behavior._height) {
                                                                                var target = behavior.get_completionList();
                                                                                behavior._height = target.offsetHeight - 2;
                                                                                target.style.height = '0px';
                                                                            }" />
                                              <%-- Expand from 0px to the appropriate size while fading in --%>
                                              <Parallel Duration=".4">
                                              <FadeIn />
                                              <Length PropertyKey="height" StartValue="0" 
	                                            EndValueScript="$find('AutoCompleteExCM')._height" />
                                              </Parallel>
                                              </Sequence>
                                              </OnShow>
                                              <OnHide>
                                              <%-- Collapse down to 0px and fade out --%>
                                              <Parallel Duration=".4">
                                              <FadeOut />
                                              <Length PropertyKey="height" StartValueScript="$find('AutoCompleteExCM')._height" EndValue="0" />
                                              </Parallel>
                                              </OnHide>
                                                </Animations>
                                            </act:AutoCompleteExtender>
                                            <asp:DropDownList ID="ddlAllergenCM" name="regthisclinic" class="form-control" AutoPostBack="true"
                                                Visible="false" Width="99%" runat="server">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtAllergenCM" MaxLength="50" Width="400px" AutoPostBack="true"
                                                Visible="true" runat="server" AutoComplete="off" Font-Names="Courier New" OnTextChanged="txtAllergenCM_TextChanged"
                                                class="form-control"></asp:TextBox>
                                            <div id="divwidthCM">
                                            </div>
                                            <asp:HiddenField ID="hdCustIDCM" runat="server" />
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Reason for Use</label>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12 form-group">
                                            <asp:TextBox ID="txtReasonForUse" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Date Started:</label>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtCMDateStart" clientidmode="Static"
                                                        onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                        onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="CMStartDate"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Date Stopped:</label>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <div class="form-group">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <input type="text" class="form-control pull-left" id="txtCMDateStop" clientidmode="Static"
                                                        onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                        onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="CMStopDate"
                                                        runat="server" data-date-format="dd-M-yyyy" style="width: 120px;" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Dosage</label>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <asp:TextBox ID="txtCMDosage" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <label for="inputEmail3" class="control-label">
                                                Route</label>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                                            <asp:TextBox ID="txtCMRoute" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" align="center">
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                            <asp:Button ID="btnaddcm" runat="server" Text="Add Concomitant Medicines" OnClick="btnAddCM"
                                                CssClass="btn btn-primary" Height="30px" Width="17%" Style="text-align: left;" />
                                            <label class="glyphicon glyphicon-plus" style="margin-left: -2.5%; margin-right: 2%;
                                                vertical-align: sub; color: #fff;">
                                            </label>
                                        </div>
                                    </div>
                                    <div class="row" align="center">
                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                            <div class="GridView whitebg" style="cursor: pointer;">
                                                <div class="grid" style="padding-right: 20px; padding-left: 15px;">
                                                    <div class="rounded">
                                                        <div class="top-outer">
                                                            <div class="top-inner">
                                                                <div class="top">
                                                                    <h2>
                                                                        Concomitant Medicines Details</h2>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="mid-outer">
                                                            <div class="mid-inner">
                                                                <div class="mid" style="height: 300px; overflow: auto">
                                                                    <div id="div-gridviewCM" class="GridView whitebg">
                                                                        <asp:GridView ID="grdCM" runat="server" OnRowDataBound="grdCM_RowDataBound" AutoGenerateColumns="False"
                                                                            Width="100%" BorderWidth="0" GridLines="None" CssClass="table table-bordered table-hover"
                                                                            CellPadding="0" CellSpacing="0" OnSelectedIndexChanging="grdCM_SelectedIndexChanging"
                                                                            OnRowDeleting="grdCM_RowDeleting">
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
                                </div>
                                <div class="row" style="margin-left: 1%">
                                    <div class="col-md-1 text-nowrap">
                                        <label for="inputEmail3" class="control-label">
                                            <asp:Label ID="Label17" runat="server" Font-Bold="True" Text="Signature:" CssClass="required"></asp:Label>
                                        </label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlSignature" runat="server" Width="98%" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row" align="center">
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                        <asp:Button ID="btnSubmit" runat="server" Font-Size="12px" Text="Save" OnClick="btnSubmit_Click"
                                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                        <label id="Label1" class="glyphicon glyphicon-floppy-disk" style="margin-left: -3%;
                                            margin-right: 2%; vertical-align: sub; color: #fff;" runat="server">
                                        </label>
                                        <asp:Button ID="btnBack" runat="server" Font-Size="12px" Text="Back" OnClick="btnBack_Click"
                                            CssClass="btn btn-primary" Height="30px" Width="8%" Style="text-align: left;" />
                                        <label id="lblBtnBack" runat="server" class="glyphicon glyphicon-backward" style="margin-left: -3%;
                                            margin-right: 2%; vertical-align: sub; color: #fff;">
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
