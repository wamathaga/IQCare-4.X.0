<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    Inherits="frmClinical_DeleteForm" Title="Untitled Page" CodeBehind="frmClinical_DeleteForm.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <style type="text/css">
        .tvNodeLevel1
        {
            font-size: 18px; /*border:double 1px gray; 
background:red url(images/cyan.png) repeat-x top left; */
            height: 25px;
            width: 100px;
            padding-left: 5px;
            color: #222;
        }
        .tvNodeLevel2
        {
            font-size: 15px;
            margin: 0px;
            padding-left: 10px;
            color: #222; /*
border:double 1px gray;
background:red url(images/gradient_orange.JPG) repeat-x top left;*/
            height: 25px;
            width: 100px;
            text-decoration: none;
        }
        .tvNodeLevel3
        {
            font-size: 12px;
            margin: 0px;
            padding-left: 15px;
            color: #222; /*
border:double 1px gray; 
background:red url(images/grayMenuBar.PNG) repeat-x top left;*/
            height: 25px;
            width: 100px;
        }
    </style>
    <div class="box-body">
        <div class="row">
            <div class="col-xs-12">
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title">
                            Delete Form</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body table-responsive no-padding" style="overflow: hidden; margin-left: 5px;">
                        <%--Main Content--%>
                        <div class="row" align="center">
                            <br />
                            <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                <asp:TreeView ID="TreeViewExisForm" ForeColor="#000000" runat="server" Width="100%"
                                    Style="text-align: left; margin-left:25%;" OnSelectedNodeChanged="TreeViewExistingForm_SelectedNodeChanged">
                                    <LevelStyles>
                                        <asp:TreeNodeStyle CssClass="tvNodeLevel1" />
                                        <asp:TreeNodeStyle CssClass="tvNodeLevel2" />
                                        <asp:TreeNodeStyle CssClass="tvNodeLevel3" />
                                    </LevelStyles>
                                    <HoverNodeStyle Font-Underline="False" />
                                    <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                                    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                                        NodeSpacing="0px" VerticalPadding="0px" />
                                </asp:TreeView>
                            </div>
                        </div>
                        <div class="row text-center">
                            <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" CssClass="btn btn-primary"
                                Width="8%" Height="30px" Style="text-align: left;" />
                            <label class=" glyphicon glyphicon-backward" style="margin-left: -3%; vertical-align: sub;
                                color: #fff;">
                            </label>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
