<%@ Page Language="C#" AutoEventWireup="true" Inherits="frmLogin" EnableViewState="true"
    CodeBehind="frmLogin.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html lang="en-US" xml:lang="en-US" xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>International Quality Care Patient Management and Monitoring System</title>

    <link rel="stylesheet" type="text/css" href="../Style/styles.css" />
    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon" />
    <link rel="icon" href="/favicon.ico" type="image/x-icon" />
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Bootstrap/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Bootstrap/css/parsley.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .quote-container
        {
            margin-top: 10px;
            position: relative;
        }
        
        .note
        {
            color: #333;
            position: relative;
            width: 500px;
            margin: 0 auto;
            padding: 10px;
            font-family: Satisfy;
            font-size: 20px;
            box-shadow: 0 10px 10px 2px rgba(0,0,0,0.3);
        }
        
        .note .author
        {
            display: block;
            margin: 40px 0 0 0;
            text-align: right;
        }
        .recommended
        {
            color: #0E105F;
            font-weight: bold;
            margin-right: -60px;
        }
    </style>
</head>
<body>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-6">
                <img src="Images/iqSolutions.png" width="477" height="82" class="pull-left" alt="IQCare logo" />
            </div>
            <!-- .col-md-6 -->
            <div class="col-md-6">
                <a class="utility pull-right" href="frmLogin.aspx" onclick="window.open('./IQCareHelp/index.html'); return false;">
                    <span class="fa-stack fa-lg"><i class="fa fa-circle fa-stack-2x"></i><i class="fa fa-question-circle-o fa-stack-1x fa-inverse">
                    </i></span><strong>Help</strong>
                    <br />
                </a>
            </div>
            <!-- .col-md-6 -->
        </div>
        <!-- .row -->
        <hr style="border-bottom: 1px solid #6CF" />
    </div>
    <!-- .container-fluid -->
    <div class="container">
        <div class="row">
            <%--<div class="=col-md-1"></div>--%>
            <div class="col-md-12 jumbotron">
                <div class="row" style="padding-top: 3%">
                    <div class="row">
                    </div>
                    <!-- .row -->
                    <div class="row">
                        <div class="col-md-9 col-sm-12 col-xs-12 label label-primary">
                            <span class="bold h5" style="float: left">IQCARE AUTHENTICATION</span>
                        </div>
                        <div class="col-md-3 col-sm-12 col-xs-12 label label-primary">
                            <asp:Label ID="lblDate" CssClass="bold h5" Style="float: right" runat="server" Text="30 September 2016">Date</asp:Label>
                        </div>
                    </div>
                    <!-- .row -->
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                        HorizontalAlign="Left" Visible="false">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                    </asp:Panel>
                    <asp:Label ID="lblLocation" runat="server" Text="Go Hospital and Medical Center"></asp:Label>
                    <asp:Label ID="lblUserName" CssClass="control-label" runat="server" Text="User name"></asp:Label>
                    <%--<span class="text-capitalize text-primary pull-left"><span class="fa fa-sign-in pull-left"> AUTHENTICATION </span><hr/>--%>
                    <div class="col-md-6">
                        <%--<asp:Image ID="imgLogin" runat="server" ImageUrl="~/Images/signin.jpg"--%>
                        <asp:Image ID="imgLogin" runat="server" ImageUrl="/Images/signin.jpg" Height="305px" />
                        <%--                    <div class="pull-left">
                    <a href="http://creativecommons.org/licenses/by-nc-sa/3.0/" onclick="window.open('http://creativecommons.org/licenses/by-nc-sa/3.0/'); return false;">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/CreativeCommon.jpg"/>
                                            </a></div>--%>
                    </div>
                    <!-- .col-md-6-->
                    <div class="col-md-6">
                        <form id="signIn" enableviewstate="true" runat="server" defaultbutton="btnLogin"
                        defaultfocus="txtuname" data-parsley-validate>
                        <asp:ScriptManager ID="mst" runat="server" EnablePageMethods="true" ScriptMode="Auto">
                            <%--OnAsyncPostBackError="ActionScriptManager_AsyncPostBackError"--%>
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdateMasterLink" runat="server">
                            <ContentTemplate>
                                <br />
                                <br />
                                <br />
                                <div class="form-group  col-md-10 box box-info">
                                    <%--<label class="control-label pull-left" for="txtuname" style="height: inherit;">
                                        Username</label>--%>
                                    <asp:TextBox ID="txtuname" Name="txtuname" CssClass="form-control" runat="server"
                                        required="true" placeholder="Username" Style="height: inherit;"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="form-group col-md-10">
                                    <%--<label class="control-label pull-left" for="txtpassword" style="height: inherit;">
                                        Password</label>--%>
                                    <asp:TextBox ID="txtpassword" CssClass="form-control" Name="txtpassword" TextMode="Password"
                                        required="true" runat="server" Style="height: inherit;" placeholder="Password"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="form-group col-md-12">
                                    <asp:CheckBox ID="chkPref" CssClass="pull-left" runat="server" Text=" Prefered Location"
                                        AutoPostBack="true" OnCheckedChanged="chkPref_CheckedChanged" />
                                </div>
                                <%-- <asp:CheckBox ID="chkPref" CssClass="pull-left col-md-12" runat="server" Text=" Prefered Location"  AutoPostBack="true" OnCheckedChanged="chkPref_CheckedChanged" /> --%>
                                <div class="form-group col-md-10">
                                    <label class="control-label pull-left" for="ddLocation">
                                        Facility/Satellite</label>
                                    <asp:DropDownList CssClass="form-control" ID="ddLocation" Name="ddLocation" required="true"
                                        runat="server" Style="height: inherit;">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="form-group col-md-10">
                                    <div class="btn btn-primary glyphicon glyphicon-search">
                                        <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" BackColor="Transparent"
                                            BorderWidth="0" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="form-group col-md-12 text-right">
                                    <br />
                                    <div class="recommended">
                                        <span class="glyphicon glyphicon-info-sign"></span>We recommend latest Chrome!
                                    </div>
                                    <%--<div class="quote-container"> 
                      <blockquote class="note yellow">
                       <span class="glyphicon glyphicon-info-sign"></span> We recommended latest Chrome!
                      </blockquote>
                        </div>--%>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="chkPref" />
                                <asp:PostBackTrigger ControlID="btnLogin" />
                            </Triggers>
                        </asp:UpdatePanel>
                        </form>
                        <!-- end content area -->
                    </div>
                    <!-- </div>-->
                </div>
                <!-- .col-md-6-->
            </div>
            <!-- .row -->
            <hr />
            <div class="row">
                <div class="col-md-1 pull-left">
                    <a href="http://thepalladiumgroup.com/ " onclick="window.open('http://thepalladiumgroup.com/ '); return false;">
                        <img src="./Images/FGI.jpg" width="99" style="margin-left: 10.5%" alt="" /></a>
                </div>
                <div class="col-md-1">
                </div>
                <div class="col-md-1">
                    <a href="http://creativecommons.org/licenses/by-nc-sa/3.0/" onclick="window.open('http://creativecommons.org/licenses/by-nc-sa/3.0/'); return false;">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/CreativeCommon.jpg" /></a>
                </div>
                <div class="col-md-4">
                </div>
                <div class="col-md-5 pull-right">
                    <label>
                        Version :<asp:Label CssClass="blue11 nomargin pull-right" ID="lblversion" Text="Version B1.0"
                            runat="server"></asp:Label></label>
                    |
                    <label>
                        Release Date :<asp:Label CssClass="blue11 nomargin" ID="lblrelDate" Text="Date" runat="server"></asp:Label></label>
                    <br />
                    <br />
                </div>
                <div class="col-md-5 pull-right">
                    <asp:Label CssClass="blue11 nomargin" ID="lblTestRelDate" Text="" runat="server"></asp:Label>
                </div>
            </div>
            <!-- .row -->
        </div>
        <!-- .row -->
    </div>
    <!-- .col-md-12-->
    <!-- .container-->
    <script type="text/javascript" src="./Incl/jquery-1.10.1.js">  </script>
    <script src="Bootstrap/js/bootstrap.js" type="text/javascript"></script>
    <script src="Bootstrap/js/parsley.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function pageLoad() {

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndAJAXRequestHandler);
        }

        function EndAJAXRequestHandler(sender, args) {

            if (args.get_error() != undefined && args.get_error().httpStatusCode == '500') {
                var errorMessage = args.get_error().message;
                args.set_errorHandled(true);
                alert("IQCare Application Framework encountered an unrecoverable error:\n" + errorMessage + "\n\nPlease report this error to the support team.");
            }

        }
        function OnSuccess(path) {
            window.location.href = path;
        }
        function OnError(error) {

        }
        function fnHelp() {
            // var path = frmLogin.CallHelp().value;
            PageMethods.CallHelp(OnSuccess, OnError);
            // window.location.href = path;
            //window.showHelp("./IQCareHelp/IQCareARUserManualSep2010.chm")
        }
        function CloseWindow() {
            window.focus();
        }

        $(document)
        .ready(function () {
            $("#signIn").parsley();
        });
   
    </script>
</body>
</html>
