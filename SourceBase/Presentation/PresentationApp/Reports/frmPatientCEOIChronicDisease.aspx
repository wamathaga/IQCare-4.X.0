<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master"
    CodeBehind="frmPatientCEOIChronicDisease.aspx.cs" Inherits="frmPatientCEOIChronicDisease" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script src="<%=ResolveUrl("~/Hivce/Scripts/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("ucOIChronicDisease.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <div class="content-wrapper">
        <div class="box-body">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title">
                                Clinical Encounter OI/Chronic Disease</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive no-padding" style="overflow: hidden; margin: 5px;">
                            <%--Main Content--%>
                            <br />
                            <div class="box box-default box-solid">
                                <div class="box-header with-border">
                                    <h3 class="box-title">
                                        Past Medical History</h3>
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <!-- /.row -->
                                    <div class="row">
                                        <div id="divChronicDisorders" class="col-md-12 col-sm-12 col-xs-12 form-group">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <table id="dtlChronicDisorders" class="table table-bordered table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th style="width: 65%;">
                                                                    Chronic Conditions
                                                                </th>
                                                                <th style="width: 20%;">
                                                                    Onset Date
                                                                </th>
                                                                <th style="width: 5%;">
                                                                    Active?
                                                                </th>
                                                                <th style="width: 5%;">
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                </div>
                                                <!-- /.box -->
                                            </div>
                                        </div>
                                        <!-- /.panel -->
                                    </div>
                                    <!-- /.row -->
                                </div>
                            </div>
                            <!-- /.box-body -->
                            <div class="box box-default box-solid">
                                <div class="box-header with-border">
                                    <h3 class="box-title">
                                        WHO Staging</h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                    <!-- /.box-tools -->
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <!-- / sub box level 2 -->
                                    <div class="box box-default box-solid">
                                        <!-- /.box-header -->
                                        <div class="box-body">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                    <div class="nav-tabs-custom">
                                                        <ul class="nav nav-tabs">
                                                            <li class="active"><a href="#Wtab_S1" data-toggle="tab">Stage 1</a></li>
                                                            <li><a href="#Wtab_S2" data-toggle="tab">Stage 2</a></li>
                                                            <li><a href="#Wtab_S3" data-toggle="tab">Stage 3</a></li>
                                                            <li><a href="#Wtab_S4" data-toggle="tab">Stage 4</a></li>
                                                        </ul>
                                                        <div class="tab-content">
                                                            <div class="tab-pane active" id="Wtab_S1">
                                                                <div style="min-height: 10px; max-height: 250px; overflow-y: auto; overflow-x: hidden;">
                                                                    <table id="dtlStageI" class="table table-bordered table-striped">
                                                                        <thead>
                                                                            <tr>
                                                                                <th style="width: 300px;">
                                                                                    WHO Stage I Conditions
                                                                                </th>
                                                                                <th style="width: 30px;">
                                                                                    Current
                                                                                </th>
                                                                                <th style="width: 50px;">
                                                                                    Historic
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <!-- /.tab-pane -->
                                                            <div class="tab-pane" id="Wtab_S2">
                                                                <div style="min-height: 10px; max-height: 250px; overflow-y: auto; overflow-x: hidden;">
                                                                    <table id="dtlStageII" class="table table-bordered table-striped">
                                                                        <thead>
                                                                            <tr>
                                                                                <th style="width: 300px;">
                                                                                    WHO Stage II Conditions
                                                                                </th>
                                                                                <th style="width: 30px;">
                                                                                    Current
                                                                                </th>
                                                                                <th style="width: 50px;">
                                                                                    Historic
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <!-- /.tab-pane -->
                                                            <div class="tab-pane" id="Wtab_S3">
                                                                <div style="min-height: 10px; max-height: 250px; overflow-y: auto; overflow-x: hidden;">
                                                                    <table id="dtlStageIII" class="table table-bordered table-striped">
                                                                        <thead>
                                                                            <tr>
                                                                                <th style="width: 300px;">
                                                                                    WHO Stage III Conditions
                                                                                </th>
                                                                                <th style="width: 30px;">
                                                                                    Current
                                                                                </th>
                                                                                <th style="width: 50px;">
                                                                                    Historic
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <!-- /.tab-pane -->
                                                            <div class="tab-pane" id="Wtab_S4">
                                                                <div style="min-height: 10px; max-height: 250px; overflow-y: auto; overflow-x: hidden;">
                                                                    <table id="dtlStageIV" class="table table-bordered table-striped">
                                                                        <thead>
                                                                            <tr>
                                                                                <th style="width: 300px;">
                                                                                    WHO Stage IV Conditions
                                                                                </th>
                                                                                <th style="width: 30px;">
                                                                                    Current
                                                                                </th>
                                                                                <th style="width: 50px;">
                                                                                    Historic
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <!-- /.tab-pane -->
                                                        </div>
                                                        <!-- /.tab-content -->
                                                    </div>
                                                    <!-- nav-tabs-custom -->
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                    <label id="lblCurrentWHOStaging" for="inputEmail3" class="control-label">
                                                        Current WHO Staging:&nbsp;&nbsp;</label>
                                                    <select class="form-control select2" data-placeholder="Select" id="ddlCurrentWhoStage"
                                                        style="width: 40%;" disabled>
                                                    </select>
                                                </div>
                                                <!-- /.box -->
                                            </div>
                                            <!-- /.box-body -->
                                        </div>
                                        <!-- /.box sub box level 2 -->
                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <!-- /.box sub box level G1 -->
                            </div>
                            <div id="divMgrDiagnosisB" class="box box-default box-solid">
                                <div class="box-header with-border">
                                    <h3 class="box-title">
                                        Diagnosis (ICD 10)</h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                    <!-- /.box-tools -->
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <!-- / sub box level 2 -->
                                    <div class="box box-default box-solid">
                                        <!-- /.box-header -->
                                        <div class="box-body">
                                            <!-- /.box-body -->
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12 form-group">
                                                    <table id="dtlICD10" class="table table-bordered table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th style="width: 50px;">
                                                                    ICD Id
                                                                </th>
                                                                <th style="width: 150px;">
                                                                    ICD Name
                                                                </th>
                                                                <th style="width: 150px;">
                                                                    Chapter Name
                                                                </th>
                                                                <th style="width: 200px;">
                                                                    Block Code - Name
                                                                </th>
                                                                <th style="width: 200px;">
                                                                    Sub Block Code - Name
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /.box sub box level 2 -->
                                    </div>
                                    <!-- /.box-body -->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
