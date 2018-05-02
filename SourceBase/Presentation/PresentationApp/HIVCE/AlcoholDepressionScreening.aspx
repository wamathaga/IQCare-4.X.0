<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    CodeBehind="AlcoholDepressionScreening.aspx.cs" Inherits="HIVCE.Presentation.AlcoholDepressionScreening" %>

<%@ Register Src="Controls/ucAlcoholDepressionScreening.ascx" TagName="ucAlcoholDepressionScreening"
    TagPrefix="uc1" %>
<asp:Content ID="Data" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script src="<%=ResolveUrl("Scripts/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/Constants.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/AlcoholDepressionScreening.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <div class="content-wrapper">
        <!-- Main content -->
        <%--<section class="content">--%>
        <!-- Default box -->
        <div>
            <div class="box-body">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box box-primary box-solid">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    Alcohol Depression Screening</h3>
                                <!-- /.box-tools -->
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <uc1:ucAlcoholDepressionScreening ID="ucAlcoholDepressionScreening1" runat="server" />
                            </div>
                            <!-- / sub box level 1 -->
                            <!-- /.box-body -->
                            <div class="box-footer" align="center">
                                <input type="hidden" runat="server" id="hidId" clientidmode="Static" />
                                <input type="hidden" runat="server" id="hidDOB" clientidmode="Static" value="0" />
                                <button type="button" class="btn btn-primary" onclick="SaveData();">
                                    Save<span class="glyphicon glyphicon-floppy-disk" style="padding-left: 5px;"></span></button>
                                <button type="button" class="btn btn-primary" onclick="ResetData();">
                                    Reset<span class="glyphicon glyphicon-remove-circle" style="padding-left: 5px;"></span></button>
                            </div>
                        </div>
                        <!-- /.box -->
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
        </div>
        <!-- /.box -->
        <%-- </section>--%>
        <!-- /.content -->
    </div>
</asp:Content>
