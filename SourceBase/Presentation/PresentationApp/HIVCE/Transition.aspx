<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/IQCare.master" CodeBehind="Transition.aspx.cs"
    Inherits="HIVCE.Presentation.Transition" %>

<asp:Content ID="Data" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script src="<%=ResolveUrl("Scripts/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/Constants.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/Transition.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>" type="text/javascript"></script>
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
      <!-- Default box -->
      <div>
        <%--<div class="box-header with-border">
          <h3 class="box-title">Title</h3>
        </div>--%>
        <div class="box-body">
                <div class="row">
        <div class="col-xs-12">
          <div class="box box-primary box-solid">
            <div class="box-header with-border">
              <h3 class="box-title">Transition from Paediatric to Adolescent Services</h3>
              <!-- /.box-tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body">
            <!-- / sub box level G1 -->
              <div id="divG1" class="box box-default box-solid">
                <div class="box-header with-border">
                  <h3 class="box-title">GOAL 1: 10 -12 years</h3>

                  <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                    </button>
                  </div>
                  <!-- /.box-tools -->
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                <table class="table">
                <tr>
                        <td style="border-top: 0px;">
                            1. Full disclosure
                        </td>
                                                <td style="border-top: 0px;"><input id="G1Q1" name="G1Q1" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" ></td>
                                                <td style="border-top: 0px;"></td>
                           
                        </tr>
                                            <tr>
                        <td style="border-top: 0px;">
                            2. Understanding of HIV
                        </td>
                                                <td style="border-top: 0px;"><input id="G1Q2" name="G1Q2" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" ></td>
                                                <td style="border-top: 0px;"></td>
                           
                        </tr>
                                            <tr>
                        <td style="border-top: 0px;">
                          3. Understanding of HIV prevention measures
                        </td>
                                                <td style="border-top: 0px;"><input id="G1Q3" name="G1Q3" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" ></td>
                                                <td style="border-top: 0px;"></td>
                           
                        </tr>
                                            <tr>
                        <td style="border-top: 0px;">
                            4. Linked to support group
                        </td>
                                                <td style="border-top: 0px;"><input id="G1Q4" name="G1Q4" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" ></td>
                                                <td style="border-top: 0px;"></td>
                           
                        </tr>
                </table>
                </div>
                <!-- /.box-body -->
              </div>
              <!-- /.box sub box level G1 -->
                          <!-- / sub box level G2 -->
              <div id="divG2" class="box box-default box-solid">
                <div class="box-header with-border">
                  <h3 class="box-title">Goal 2: 13 -16 years</h3>

                  <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                    </button>
                  </div>
                  <!-- /.box-tools -->
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                <table class="table">
                <tr>
                        <td style="border-top: 0px; width:70%">
                            Understanding of medication and adherence
                        </td>
                                                <td style="border-top: 0px; width:10%">
                            <input id="G2Q1" name="G2Q1" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                         </td>
                         <td style="border-top: 0px; width:20%">
                         <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtG2Q1">
                            </div>
                            </td>
                        </tr>
                         <tr>
                        <td style="border-top: 0px; width:70%">
                            Encourage appointment keeping
                        </td>
                                                <td style="border-top: 0px; width:10%">
                            <input id="G2Q2" name="G2Q2" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                         </td>
                         <td style="border-top: 0px; width:20%">
                         <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtG2Q2">
                            </div>
                            </td>
                        </tr>
                         <tr>
                        <td style="border-top: 0px; width:70%">
                            Member of support group
                        </td>
                                                <td style="border-top: 0px; width:10%">
                            <input id="G2Q3" name="G2Q3" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                         </td>
                         <td style="border-top: 0px; width:20%">
                         <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtG2Q3">
                            </div>
                            </td>
                        </tr>
                </table>
                </div>
                <!-- /.box-body -->
              </div>
              <!-- /.box sub box level G2 -->
                          <!-- / sub box level G3 -->
              <div id="divG3" class="box box-default box-solid">
                <div class="box-header with-border">
                  <h3 class="box-title">Goal 3: 17 -19 years</h3>

                  <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                    </button>
                  </div>
                  <!-- /.box-tools -->
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                <table class="table">
                 <tr>
                        <td style="border-top: 0px; width:70%">
                            Demonstrated understanding of importance of medication adherence in the last 2 -3 visits
                        </td>
                                                <td style="border-top: 0px; width:10%">
                            <input id="G3Q1" name="G3Q1" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                         </td>
                         <td style="border-top: 0px; width:20%">
                         <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtG3Q1">
                            </div>
                            </td>
                        </tr>
                        <tr>
                        <td style="border-top: 0px; width:70%">
                            Prompt appointment keeping for 6 months
                        </td>
                                                <td style="border-top: 0px; width:10%">
                            <input id="G3Q2" name="G3Q2" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                         </td>
                         <td style="border-top: 0px; width:20%">
                         <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtG3Q2">
                            </div>
                            </td>
                        </tr>
                </table>
                </div>
                <!-- /.box-body -->
              </div>
              <!-- /.box sub box level G3 -->
                          <!-- / sub box level G4 -->
              <div id="divG4" class="box box-default box-solid">
                <div class="box-header with-border">
                  <h3 class="box-title">Goal 4:</h3>

                  <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                    </button>
                  </div>
                  <!-- /.box-tools -->
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                <table class="table">
                <tr>
                        <td style="border-top: 0px; width:70%">
                            Client Choses to transition
                        </td>
                                                <td style="border-top: 0px; width:10%">
                            <input id="G4Q1" name="G4Q1" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                         </td>
                         <td style="border-top: 0px; width:20%">
                         <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtG4Q1">
                            </div>
                            </td>
                        </tr>
                        <tr>
                        <td style="border-top: 0px; width:70%">
                            Medical records transferred to adult clinic?
                        </td>
                                                <td style="border-top: 0px; width:10%">
                            <input id="G4Q2" name="G4Q2" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                         </td>
                         <td style="border-top: 0px; width:20%">
                         <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtG4Q2">
                            </div>
                            </td>
                        </tr>
                        <tr>
                        <td style="border-top: 0px; width:70%">
                           Orientation done for adult clinic?
                        </td>
                                                <td style="border-top: 0px; width:10%">
                            <input id="G4Q3" name="G4Q3" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                         </td>
                         <td style="border-top: 0px; width:20%">
                         <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtG4Q3">
                            </div>
                            </td>
                        </tr>
                        <tr>
                        <td style="border-top: 0px; width:70%">
                           Follow up of transitioned adolesent
                        </td>
                                                <td style="border-top: 0px; width:10%">
                            <input id="G4Q4" name="G4Q4" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" >
                         </td>
                         <td style="border-top: 0px; width:20%">
                         <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtG4Q4">
                            </div>
                            </td>
                        </tr>
                </table>
                </div>
                <!-- /.box-body -->
              </div>
              <!-- /.box sub box level G4 -->
            </div>
            <!-- /.box-body -->
                    <div class="box-footer" align="center">
                             <input type="hidden" runat="server" id="hidId" clientidmode="Static" />
                             <input type="hidden" runat="server" id="hidDOB" clientidmode="Static" />
                  <button type="button" class="btn btn-primary" onclick="return SaveData();">
                Save<span class="glyphicon glyphicon-floppy-disk" style="padding-left:5px;"></span></button>
                <button type="button" class="btn btn-primary" onclick="ResetData();">Reset<span class="glyphicon glyphicon-remove-circle" style="padding-left:5px;"></span></button>
                      </div>
          </div>
          <!-- /.box -->
        </div>
      </div>
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box -->
    </section>
        <!-- /.content -->
    </div>
</asp:Content>
