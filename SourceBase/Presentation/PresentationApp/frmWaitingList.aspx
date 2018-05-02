<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmWaitingList.aspx.cs"
    Inherits="IQCare.Web.frmWaitingList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<link href="Style/bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="Style/bootstrap-3.3.6-dist/css/bootstrap-theme.css" rel="stylesheet"
    type="text/css" />
<link rel="stylesheet" type="text/css" href="./style/styles.css" />
<link href="../Style/styles.css" id="main" rel="stylesheet" type="text/css" />
<link href="../Style/Menu.css" id="menuStyle" rel="stylesheet" type="text/css" />
<link href="../Style/calendar.css" rel="stylesheet" type="text/css" />
<link href="../Style/_assets/css/grid.css" rel="stylesheet" type="text/css" />
<link href="../Style/_assets/css/round.css" rel="stylesheet" type="text/css" />
<link href="../Style/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="Style/bootstrap-3.3.6-dist/html5Shiv/html5shiv.js" type="text/javascript"></script>
<head runat="server">
    <title>Waiting List</title>
    <style type="text/css">
        .style1
        {
            width: 1195px;
        }
    </style>
</head>
<body class="container-fluid">
    <form id="form1" runat="server">
    <h3 class="left">
        Waiting List</h3>
    <div class="border formbg center">
        <table width="100%" class="table-condensed">
            <tbody>
                <tr>
                    <td class="border pad5 whitebg" valign="top" style="width: 1195px;">
                        <label for="lblSelectList">
                            Select List:</label>
                        <asp:DropDownList ID="ddwaitingList" runat="server" OnSelectedIndexChanged="ddwaitingList_SelectedIndexChanged"
                            AutoPostBack="True">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Consultation" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Laboratory" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Pharmacy" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Triage" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                        <label id="lblWaitingfor" runat="server">
                            Waiting For:</label>
                        <asp:DropDownList ID="ddWaitingFor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddWaitingFor_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="pad5 formbg border" style="width: 1195px">
                        <div class="grid" id="divWlist" style="width: 100%;">
                            <div class="rounded">
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid" style="height: 300px; overflow: auto">
                                            <div id="div-gridview" class="GridView whitebg">
                                                <asp:GridView ID="grdWaitingList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                    Width="100%" BorderColor="White" PageIndex="1" BorderWidth="1px" GridLines="None"
                                                    CssClass="table table-bordered table-hover" CellPadding="0" OnSelectedIndexChanged="grdWaitingList_SelectedIndexChanged"
                                                    DataKeyNames="Ptn_PK,WaitingListID" OnRowDataBound="grdWaitingList_RowDataBound"
                                                    ShowHeaderWhenEmpty="True">
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>                                                   
                                                    <Columns>
                                                        <asp:CommandField ShowSelectButton="True" SelectText="Serve" />
                                                        <asp:BoundField HeaderText="Patient ID" DataField="PatientFacilityID" />
                                                        <asp:BoundField HeaderText="PatientID" DataField="Ptn_PK" Visible="false" />
                                                        <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                                                        <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                                                        <asp:BoundField HeaderText="DOB" DataField="DOB" />
                                                        <asp:BoundField HeaderText="Gender" DataField="Sex" />
                                                        <asp:BoundField HeaderText="Waiting Time" DataField="TimeOnList" />
                                                        <asp:BoundField HeaderText="WaitingListID" DataField="WaitingListID" Visible="false" />
                                                        <asp:BoundField HeaderText="Added By" DataField="UserName" />
                                                         <asp:BoundField HeaderText="Module Name" DataField="ModuleName" />
                                                        <asp:BoundField HeaderText="Waiting For" DataField="WaitingFor" />
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
                    </td>
                </tr>
                <tr class="border pad5 whitebg">
                    <td class="style1">
                        <input type="button" onclick="window.close();" value="Close" style="text-align: left;
                            height: 30px; width: 8%;" class="btn btn-primary" />
                        <label class="glyphicon glyphicon-remove" style="margin-left: -2.5%; margin-right: 0%;
                            vertical-align: sub; color: #fff;">
                        </label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
