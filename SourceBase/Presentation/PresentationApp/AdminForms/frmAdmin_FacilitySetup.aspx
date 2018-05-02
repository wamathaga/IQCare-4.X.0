<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    EnableViewState="true" Inherits="frmAdmin_FacilitySetup" Title="Untitled Page"
    CodeBehind="frmAdmin_FacilitySetup.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script type="text/javascript">
        function RegisterJQuery() {
            $('#txtPEPFAR_Fund').datepicker({ autoclose: true });
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
                            <h3 class="box-title">
                                <asp:Label ID="lblForm" runat="server" Text="a"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                            <%--Main Content Start--%>
                            <div class="row">
                                <br />
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label class="control-label required">
                                        Facility/Satellite Name:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="txtfacilityname" runat="server" MaxLength="50" CssClass="form-control"
                                        Width="350px"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                   <%-- <label class="control-label required">
                                        National Id:</label>--%>
                                         <label id="lblPOS" runat="server" class="required" for="lblPOS">                                    
                                       Facility MFL Code:
                                        </label>

                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                   <%-- <asp:TextBox ID="txtNationalId" runat="server" MaxLength="50" class="form-control"
                                        Width="350px"></asp:TextBox>--%>
                                         <asp:TextBox ID="txtLPTF" MaxLength="5" runat="server" class="form-control" Width="350px">11111</asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label id="lblCountry" runat="server" class="required" for="lblcountry">
                                        Country:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <asp:DropDownList ID="ddCountry" runat="server" class="form-control" Style="width: 85%"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="txtcountryno" MaxLength="4" runat="server" CssClass="form-control"
                                        Width="350px">695</asp:TextBox>--%>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                  <label id="lblFaclogo" runat="server" for="lblPOS">
                                        Facility Logo:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                   <input id="Filelogo" style="width: 350px" type="file" runat="server" class="form-control" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label id="lblSatellite" runat="server" class="required" for="lblsatellite">
                                        Satellite number:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="txtSatelliteID" runat="server" CssClass="form-control" Width="350px"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                 <label id="lbltele" runat="server" for="lbltele" class="required">
                                        Facility Telephone:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                   <asp:TextBox ID="txtFactele" runat="server" CssClass="form-control" Width="350"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label id="lblFacAddress" runat="server" class="required" for="lblFacAddress">
                                        Facility Address:</label>
                                </div>
                                <div class="col-md-10 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="txtFacAddress" Rows="2" TextMode="MultiLine" Width="96%" runat="server"
                                        Style="resize: none;" CssClass="form-control"></asp:TextBox>
                                </div>
                               
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label id="lblFacCell" runat="server" for="lblsatellite">
                                        Facility Cell:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="txtFacCell" runat="server" CssClass="form-control" Width="350"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label id="lblFacFax" runat="server" for="lblPOS">
                                        Facility Fax:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="txtFacFax" runat="server" class="form-control" Width="350"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label id="lblFacEmail" runat="server" for="lblsatellite">
                                        Facility Email:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="txtFacEmail" runat="server" CssClass="form-control" Width="350"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label id="lblFacURL" runat="server" for="lblPOS">
                                        Facility URL:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="txtFacURL" runat="server" CssClass="form-control" Width="350"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label id="lblpharmfoottext" runat="server" for="lblpharmfoottext">
                                        Pharmacy Footer Text:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <asp:TextBox ID="txtpharmfoottext" Rows="5" TextMode="MultiLine" CssClass="form-control"
                                        Style="width: 350px; resize: none;" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                    <label id="lblprestemp" runat="server" for="lblprestemp" class="required">
                                        Pharmacy Prescription Template:</label>
                                </div>
                                <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                    <input id="Radio1" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                        checked="true" type="radio" value="0" name="templete" runat="server" />
                                    <label style="vertical-align: text-top">
                                        Simple Template</label>
                                    <input id="Radio2" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                        type="radio" value="1" name="templete" runat="server" />
                                    <label style="vertical-align: text-top">
                                        Expanded Template</label>
                                </div>
                            </div>
                            <div class="row">
                                <asp:UpdatePanel runat="server" ID="upProvince" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <label id="Label1" runat="server" for="lblPOS" style="width: 350px">
                                                Province/State:</label>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                            <asp:DropDownList ID="ddlprovince" runat="server" CssClass="form-control" Style="width: 350px"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                            <label id="Label2" runat="server" for="lblsatellite">
                                                District:</label>
                                        </div>
                                        <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                            <asp:DropDownList ID="ddldistrict" runat="server" CssClass="form-control" Style="width: 350px">
                                            </asp:DropDownList>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlprovince" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddldistrict" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="lblimage">
                                    Facility login image:</label>
                            </div>
                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                <input id="FileLoad" style="width: 350px" type="file" runat="server" class="form-control" />
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="lblSatellitenumber" class="required">
                                    Appointment grace period:</label>
                            </div>
                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                <asp:TextBox ID="txtGrace" MaxLength="3" runat="server" class="form-control" Style="width: 350px">00</asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <label for="lblPEPFAR">
                                    PEPFAR funding start date:</label>
                            </div>
                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                <div class="form-group">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-left" id="txtPEPFAR_Fund" clientidmode="Static"
                                            maxlength="11" runat="server" data-date-format="dd-M-yyyy" style="width: 110px;" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <asp:XmlDataSource ID="CurrencyDT" runat="server" DataFile="~/XMLFiles/Currency.xml">
                                </asp:XmlDataSource>
                                <label class="required" for="lblcurrency">
                                    Currency:
                                </label>
                            </div>
                            <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                <asp:DropDownList ID="cmbCurrency" runat="server" CssClass="form-control" Style="width: 350px"
                                    DataSourceID="CurrencyDT" DataTextField="Name" DataValueField="Id">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-6">
                                <%--left--%>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <label style="width: 100%">
                                            Preferred Location:</label>
                                    </div>
                                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                        <input type="checkbox" id="chkPreferred" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <label>
                                            Status:</label>
                                    </div>
                                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                        <asp:DropDownList ID="ddStatus" runat="server" Width="100px" CssClass="form-control">
                                            <asp:ListItem Value="0">Active</asp:ListItem>
                                            <asp:ListItem Value="1">InActive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <div id="paperless" runat="server">
                                            <label>
                                                Paperless clinic:
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                        <input type="checkbox" id="chkPaperlessclinic" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <div id="strongpassword" runat="server">
                                            <label>
                                                Strong Password:
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                        <input type="checkbox" id="chkStrongPwd" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <label>
                                            Expire Password:</label>
                                    </div>
                                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                        <div style="width: 100%; margin: 0px auto;">
                                            <div style="float: left; margin-right: 5px;">
                                                <input type="checkbox" id="chkexpPwd" onclick="toggle('divnoofdays');" runat="server" />
                                            </div>
                                            <div id="divnoofdays" style="display: none">
                                                <div style="float: left; margin-right: 5px;">
                                                    <label class="margin5">
                                                        Number of Days:</label>
                                                </div>
                                                <div style="float: left">
                                                    <asp:TextBox ID="txtnoofdays" MaxLength="4" size="7" runat="server" Width="70" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <div class="whitebg" id="divdateconstraint" runat="server">
                                            <label>
                                                Turn off Date Constraint:
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                        <input type="checkbox" id="chkdateconstraint" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <div class="whitebg" id="div1" runat="server">
                                            <label class="center">
                                                Billing (Check to Activate):
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                        <input type="checkbox" id="chkbilling" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <div class="whitebg" id="div2" runat="server">
                                            <label>
                                                PM/SCM (Check to Activate):
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                        <input type="checkbox" id="chkpmscm" runat="server" />
                                    </div>
                                </div>
                                <div class="row" style="display: none">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <div class="whitebg" id="divrecords" runat="server">
                                            <label>
                                                Records (Check to Activate):
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                        <input type="checkbox" id="chkrecords" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <div class="whitebg" id="divwards" runat="server">
                                            <label>
                                                Wards (Check to Activate):
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                        <input type="checkbox" id="chkwards" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 col-sm-12 col-xs-12 form-group">
                                        <div class="whitebg" id="divLMIS" runat="server">
                                            <label>
                                                LMIS (Check to Activate):
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-8 col-sm-12 col-xs-12 form-group">
                                        <input type="checkbox" id="chkLMIS" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6">
                                <%--right--%>
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                        <div nowrap='nowrap' class="divBorder border left whitebg" id="PMTCTdiv" runat="server"
                                            width="1%" style="cursor: pointer; height: 280px; overflow: auto;">
                                            <asp:CheckBoxList ID="cblPMTCT" runat="server" RepeatLayout="Flow" Width="70%">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2 col-sm-12 col-xs-12 form-group">
                                <div id="div3" runat="server">
                                    <label class="center">
                                        Drug Stores:
                                    </label>
                                </div>
                            </div>
                            <div class="col-md-7 col-sm-12 col-xs-12 form-group">
                                <div nowrap='nowrap' align="left" id="divDrugStore" runat="server" width="1%" style="cursor: pointer;
                                    height: 120px; overflow: auto;">
                                    <asp:CheckBoxList ID="CBL_DrugStore" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal"
                                        RepeatColumns="5" Width="95%">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                        <div class="row" align="center">
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="9%" Style="text-align: left; margin-left: 2%;" /><span class="glyphicon glyphicon-floppy-disk"
                                        style="margin-left: -2.5%; vertical-align: middle; color: #fff; margin-top: .25%;"></span>
                                <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="btn btn-primary"
                                    Height="30px" Width="8%" Style="text-align: left; margin-left: 2%;" /><span class="glyphicon glyphicon-check"
                                        style="margin-left: -2%; vertical-align: middle; color: #fff; margin-top: .25%;"></span>
                                <asp:Button ID="btnExit" runat="server" Text="Close" OnClick="btnExit_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="8%" Style="text-align: left; margin-left: 2%;" /><span class="glyphicon glyphicon-remove"
                                        style="margin-left: -2%; vertical-align: middle; color: #fff; margin-right: 0%;"></span>
                                <asp:Button ID="btnOk" Text="OK" CssClass="btn btn-primary" runat="server" OnClick="btnOk_Click"
                                    Height="30" Width="9%" Style="text-align: left; margin-left: 20px; display: none;" />
                                <%-- <span class="glyphicon glyphicon-ok" style="margin-left: -2%; vertical-align: middle;
                                        color: #fff; margin-left: -3%;"></span>--%>
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
