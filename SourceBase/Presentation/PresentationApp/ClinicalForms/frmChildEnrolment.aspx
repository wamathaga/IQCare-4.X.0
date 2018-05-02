<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage/IQCare.master"
    AutoEventWireup="true" Inherits="ClinicalForms_frmChildEnrolment" Title="Untitled Page"
    EnableViewState="true" CodeBehind="frmChildEnrolment.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div class="container-fluid">
        <%--<form id="frmChildEnrollment" method="post" runat="server">--%>
        <div id="DivPMTCT" runat="server">
            <h4 class="h4 margin">
                Child Enrollment</h4>
            <div class="border center formbg">
                <table width="100%" class="table-condensed">
                    <tbody>
                        <tr>
                            <td style="width: 500px" class="border pad5 whitebg">
                                <label id="lblPName" class="required" for="patientname">
                                    Patient Name:</label>
                                <span id="FName" class="smallerlabel">First: </span>
                                <asp:TextBox ID="TxtFirstName" runat="server" Width="118px" MaxLength="50" OnTextChanged="TxtFirstName_TextChanged"></asp:TextBox>
                                <span id="LName" class="smallerlabel">Last: </span>
                                <asp:TextBox ID="TxtLastName" runat="server" Width="118px" MaxLength="50" OnTextChanged="TxtLastName_TextChanged"></asp:TextBox>
                            </td>
                            <td class="border pad5 whitebg" width="50%">
                                <label id="lblregistrationdate" class="required" for="RegistrationDate">
                                    Registration Date:</label>
                                <asp:TextBox ID="TxtRegistrationDate" runat="server" Width="25%" MaxLength="11"></asp:TextBox>
                                <img onclick="w_displayDatePicker('<%= TxtRegistrationDate.ClientID %>');" height="22"
                                    alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0"
                                    style="vertical-align: bottom; margin-bottom: 2px;" />
                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 500px" class="border pad5 whitebg" nowrap="nowrap">
                                <label id="lblgender" class="required" for="gender">
                                    Sex:</label>
                                <asp:DropDownList ID="DDGender" runat="server">
                                    <asp:ListItem Value="" Selected="True">-Select-</asp:ListItem>
                                    <asp:ListItem Value="16">Male</asp:ListItem>
                                    <asp:ListItem Value="17">Female</asp:ListItem>
                                </asp:DropDownList>
                                <label id="lblDOB" class="required margin15" for="DOB">
                                    Date of Birth:</label>
                                <asp:TextBox ID="TxtDOB" runat="server" Width="70px" MaxLength="11"></asp:TextBox>
                                <img onclick="w_displayDatePicker('<%=TxtDOB.ClientID %>');" height="22" alt="Date Helper"
                                    hspace="3" src="../images/cal_icon.gif" width="20" border="0" style="vertical-align: bottom;
                                    margin-bottom: 2px;" />
                                <span class="smallerlabel">DD-MMM-YYYY </span>
                                <br />
                                <br />
                            </td>
                            <td class="border pad5 whitebg" width="50%">
                                <label id="Label1" class="required" for="RegistrationDate">
                                    Admission No:</label>
                                <asp:TextBox ID="TxtAdmissionNo" AutoPostBack="true" runat="server" Width="25%" MaxLength="11"
                                    OnTextChanged="TxtAdmissionNo_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="table-condensed" width="100%">
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnAdd" Text="Add Child" runat="server" OnClick="btnAdd_Click1" CssClass="btn btn-primary"
                                                Height="30px" Style="text-align: left;" width="20%"></asp:Button>
                                            <span class="glyphicon glyphicon-plus-sign" style="vertical-align: sub; margin-left: -5%;
                                                color: #fff"></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">
                            </td>
                        </tr>
                        <tr>
                            <td class="whitebg border" valign="top" colspan="2">
                                <div id="div-gridview" class="GridView whitebg">
                                    <asp:GridView ID="grdChildInfo" runat="server" EnableViewState="true" Width="100%"
                                        BorderColor="#666699" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True"
                                        OnRowDataBound="grdChildInfo_RowDataBound" OnSorting="grdChildInfo_Sorting" OnRowDeleting="grdChildInfo_RowDeleting"
                                        OnSelectedIndexChanging="grdChildInfo_SelectedIndexChanging" OnRowCommand="grdChildInfo_RowCommand">
                                        <HeaderStyle CssClass="tableheaderstyle" HorizontalAlign="Center"></HeaderStyle>
                                        <AlternatingRowStyle BackColor="White" BorderColor="Silver" />
                                        <Columns>
                                            <asp:BoundField HeaderText="First Name" DataField="FirstName" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Middle Name" DataField="MiddleName" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue" Visible="false">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Last Name" DataField="LastName" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Registration Date" DataField="RegistrationDate" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Sex" DataField="Sex" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="DOB" DataField="DOB" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Admission No" DataField="AdmissionNumber" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Delete" DataField="Id" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="false" ItemStyle-Width="0%" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue" Visible="true">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="0%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Link" DeleteText="<img src='../Images/del.gif' alt='Delete this' border='0' />"
                                                ShowDeleteButton="true" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="cmdBind" runat="server" ID="LbGridChildEnrol">Show</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="border center formbg">
                <table cellspacing="6" cellpadding="0" width="100%">
                    <tbody>
                        <tr>
                            <asp:TextBox ID="txtSysDate" runat="server" CssClass="textstylehidden"></asp:TextBox>
                            <td align="center">
                                <asp:Button ID="btnsave" Text="Save" runat="server" OnClick="btnsave_Click" CssClass="btn btn-primary"
                                    Height="30px" Style="text-align: left;" Width="8%"></asp:Button>
                                <span class="glyphicon glyphicon-floppy-disk" style="vertical-align: sub; margin-left: -3%;
                                    margin-right: 2%; color: #fff"></span>
                                <asp:Button ID="btnCancel" Text="Close" runat="server" OnClick="btnCancel_Click"
                                    CssClass="btn btn-primary" Height="30px" Style="text-align: left;" width="8%"></asp:Button>
                                <span class="glyphicon glyphicon-remove" style="vertical-align: sub; margin-left: -3%;
                                    margin-right: 2%; color: #fff"></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
