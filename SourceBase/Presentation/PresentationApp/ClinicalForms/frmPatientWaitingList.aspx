<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="frmPatientWaitingList.aspx.cs"
    Inherits="IQCare.Web.ClinicalForms.frmPatientWaitingList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Style/styles.css" id="main" rel="stylesheet" type="text/css" />
    <link href="../Style/Menu.css" id="menuStyle" rel="stylesheet" type="text/css" />
    <link href="../Style/calendar.css" rel="stylesheet" type="text/css" />
    <link href="../Style/_assets/css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../Style/_assets/css/round.css" rel="stylesheet" type="text/css" />
    <link href="../Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../Style/bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../Style/bootstrap-3.3.6-dist/css/bootstrap-theme.css" rel="stylesheet"
        type="text/css" />
    <script src="../Style/bootstrap-3.3.6-dist/js/bootstrap.js" type="text/javascript"></script>
    <title>Patient Waiting List</title>
    <script language="javascript" type="text/javascript">
        function WindowPrint() {
            window.print();
        }
     
    </script>
</head>
<body class="container-fluid">
    <form id="form1" runat="server" class="form">
    <h4 class="left">
        <asp:Label ID="lblTechnicalArea" runat="server"></asp:Label><br />
        Patient Waiting List
    </h4>
    <table width="100%" class="table-condensed">
        <tbody>
            <tr>
                <td colspan="2">
                    <div id="divPatientDetails" runat="server">
                        <table width="100%" class="table-condensed">
                            <tr>
                                <td class="form" align="center">
                                    <label class="patientInfo">
                                        Patient Name :
                                        <asp:Label ID="lblname" runat="server" Text=""></asp:Label></label>
                                    <label class="bold">
                                        IQ Number:
                                        <asp:Label ID="lblIQnumber" runat="server"></asp:Label></label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <%--  <tr>
                            <td class="form bold" align="center">
                           
                                <asp:Panel ID="thePnlIdent" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>--%>
        </tbody>
    </table>
    <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
        HorizontalAlign="Left">
        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000;
            padding-left: 5px"></asp:Label>
    </asp:Panel>
    <div class="border center formbg">
        <table class="table-condensed" width="100%">
            <tbody>
                <tr>
                    <td class="border pad5 whitebg" width="40%">
                        <label class="required">
                            Select waiting list:</label>
                        <asp:DropDownList ID="ddWList" runat="server" class="form-control">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Consultation" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Laboratory" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Pharmacy" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Triage" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="border pad5 whitebg" width="20%">
                        <label>
                            Priority:</label>
                        <asp:DropDownList ID="ddPriority" runat="server" class="form-control">
                            <asp:ListItem Text="1 - Normal" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2 - Medium" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3 - High" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="border pad5 whitebg" width="40%">
                        <label>
                            Waiting for:</label>
                        <asp:DropDownList ID="ddWaitingFor" runat="server" class="form-control">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" align="center" colspan="3">
                        <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary"
                            Height="30px" Width="60" Style="text-align: left;" />
                        <label class="glyphicon glyphicon-plus" style="margin-left: -2.1%; padding-right:30px;vertical-align: sub; color: #fff;">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td class="border center whitebg" colspan="3">
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 formbg" colspan="3">
                        <div class="GridView whitebg" style="cursor: pointer;">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                    Patient Waiting Lists</h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="height: 200px; overflow: auto">
                                                <div id="div-gridview" class="GridView whitebg">
                                                    <asp:GridView ID="grdWaitingList" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                        Width="100%" BorderColor="White" PageIndex="1" BorderWidth="0px" GridLines="None"
                                                        CssClass="table table-bordered table-hover" CellPadding="0" OnSelectedIndexChanged="grdWaitingList_SelectedIndexChanged"
                                                        DataKeyNames="WaitingListID" OnRowDataBound="grdWaitingList_RowDataBound" AutoGenerateDeleteButton="True"
                                                        ShowHeaderWhenEmpty="True" OnRowDeleting="grdWaitingList_RowDeleting">
                                                        <HeaderStyle CssClass="searchresultfixedheader" Height="20px" VerticalAlign="Middle"
                                                HorizontalAlign="Left"></HeaderStyle>                                                      
                                                        <Columns>
                                                            <asp:BoundField HeaderText="List Name" DataField="ListName" />
                                                            <asp:BoundField HeaderText="Service Name" DataField="ModuleName" />
                                                            <asp:BoundField HeaderText="Time On List" DataField="TimeOnList" />
                                                            <asp:BoundField HeaderText="Waiting For" DataField="WaitingForName" />
                                                            <asp:BoundField HeaderText="Added by" DataField="AddedBy" />
                                                            <asp:BoundField HeaderText="ListID" DataField="ListID" Visible="False" />
                                                            <asp:BoundField HeaderText="WaitingListID" DataField="WaitingListID" Visible="False" />
                                                            <asp:BoundField HeaderText="Priority" DataField="Priority" />
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
                    </td>
                </tr>
                <tr>
                    <td class="border center whitebg" colspan="3">
                        <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" 
                            CssClass="btn btn-primary" Height="30px" Width="10%" Style="text-align: left;" />
                        <label class="glyphicon glyphicon-floppy-disk" style="vertical-align: middle; margin-left: -2.5%;
                            color: #fff; margin-top: .75%; margin-right: 2%;">
                        </label>
                        <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" OnClientClick="window.close()"
                            CssClass="btn btn-primary" Height="30px" Width="10%" Style="text-align: left;" />
                        <label class="glyphicon glyphicon-remove" style="margin-left: -2.5%; margin-right: 2%;
                            vertical-align: sub; color: #fff;">
                        </label>
                        <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()"
                            CssClass="btn btn-primary" Height="30px" Width="10%" Style="text-align: left;" />
                        <label class="glyphicon glyphicon-print" style="margin-left: -3%; vertical-align: sub;
                            color: #fff;">
                        </label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
