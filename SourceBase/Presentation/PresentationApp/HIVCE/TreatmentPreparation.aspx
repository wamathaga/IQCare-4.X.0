<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TreatmentPreparation.aspx.cs"
    MasterPageFile="~/MasterPage/IQCare.master" Inherits="HIVCE.Presentation.TreatmentPreparation" %>

<asp:Content ID="Data" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script src="<%=ResolveUrl("Scripts/Common.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/Constants.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <script src="<%=ResolveUrl("Scripts/TreatmentPreparation.js") %>?n=<%=string.Format("{0:yyyyMMddhhmmss}",DateTime.Now)%>"
        type="text/javascript"></script>
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
      <!-- Default box -->
      <div>
        <div class="box-body">
                <div class="row">
        <div class="col-xs-12">
          <div class="box box-primary box-solid">
            <div class="box-header">
              <h3 class="box-title">HIV Education and Adherence Counselling</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body table-responsive no-padding">
                   <table class="table">
                    <tr>
                        <th>
                            Component
                        </th>
                        <th>
                            Questions Covered
                        </th>
                        <th>
                            Initial Training
                        </th>
                        <th>
                            Refresher
                        </th>
                    </tr>
                    <tr>
                        <td>
                            HIV
                        </td>
                        <td>
                            <ul>
                                <li>What is HIV</li>
                                <li>How is HIV transmitted</li>
                                <li>Why should family members be tested for HIV</li>
                            </ul>
                        </td>
                        <td>
                        <div class="form-group">
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" data-date-format="mm/dd/yyyy" id="dtHIVIT" style="padding: 13px 12px;">
                            </div>
                            </div>
                        </td>
                        <td>
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtHIVR" style="padding: 13px 12px;">
                            </div>
                        </td>
                    </tr>
                                        <tr>
                        <td>
                            Viral Load
                        </td>
                        <td>
                            <ul>
                                <li>What is viral load</li>
                                <li>How often is viral load measured</li>
                                <li>What do viral load measurements mean</li>
                            </ul>
                        </td>
                        <td>
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtVLIT" style="padding: 13px 12px;">
                            </div>
                        </td>
                        <td>
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtVLR" style="padding: 13px 12px;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            CD4 Cells
                        </td>
                        <td>
                            <ul>
                                <li>What are CD4 cells</li>
                                <li>How are CD4 cells affected by HIV</li>
                                <li>What happens when CD4 cells decrease</li>
                            </ul>
                        </td>
                        <td>
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtCDIT" style="padding: 13px 12px;">
                            </div>
                        </td>
                        <td>
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtCDR" style="padding: 13px 12px;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Antiretroviral therapy
                        </td>
                        <td>
                            <ul>
                                <li>How often is CD4 cell count measured </li>
                                <li>What is ART</li>
                                <li>What are the benefits of ART</li>
                                <li>When is ART started</li>
                                <li>Does ART cure HIV</li>
                                <li>Can you still give HIV to others while taking ART</li>
                                <li>How long is ART taken</li>
                            </ul>
                        </td>
                        <td>
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtATIT" style="padding: 13px 12px;">
                            </div>
                        </td>
                        <td>
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtATR" style="padding: 13px 12px;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Treatment failure
                        </td>
                        <td>
                            <ul>
                                <li>What happens if you stop taking ART</li>
                                <li>What happens if you do not take ART regularly </li>
                                <li>What happens if the viral load increases</li>
                                <li>What happens in treatment failure </li>
                                <li>What are the side-effects of ART </li>
                            </ul>
                        </td>
                        <td>
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtTFIT" style="padding: 13px 12px;">
                            </div>
                        </td>
                        <td>
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="dtTFR" style="padding: 13px 12px;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                <td>
                    ART side effects 
                </td>
                <td>
                    <ul>
                        <li>What should you do if you notice any side effects</li>
                    </ul>
                </td>
                <td>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="dtARTIT" style="padding: 13px 12px;">
                    </div>
                </td>
                <td>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="dtARTR" style="padding: 13px 12px;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Adherence
                </td>
                <td>
                    <ul>
                        <li>What is adherence</li>
                        <li>How should ART be taken</li>
                        <li>What usually interferes with good adherence</li>
                        <li>What might make it difficult for you individually to take your ART as prescribed</li>
                        <li>What can help you take ART as prescribed</li>
                        <li>What happens if you miss an appointment</li>
                    </ul>
                </td>
                <td>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="dtAIT" style="padding: 13px 12px;">
                    </div>
                </td>
                <td>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="dtAIR" style="padding: 13px 12px;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Other Medications
                </td>
                <td>
                    <ul>
                        <li>What should you do if you notice any side effects</li>
                    </ul>
                </td>
                <td>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="dtOMIT" style="padding: 13px 12px;">
                    </div>
                </td>
                <td>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="dtOMR" style="padding: 13px 12px;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Nutrition
                </td>
                <td>
                    <ul>
                        <li>Why is nutrition important</li>
                        <li>What can you do to improve your nutrition</li>
                    </ul>
                </td>
                <td>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="dtNIT" style="padding: 13px 12px;">
                    </div>
                </td>
                <td>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="dtNR" style="padding: 13px 12px;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Follow-up 
                </td>
                <td>
                    <ul>
                        <li>How often will you need to come to clinic</li>
                        <li>What will we be checking for during your clinic visits</li>
                    </ul>
                </td>
                <td>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="dtFUIT" style="padding: 13px 12px;">
                    </div>
                </td>
                <td>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="dtFUR" style="padding: 13px 12px;">
                    </div>
                </td>
            </tr>
             <tr>
                <td>
                    ART readiness assessment
                </td>
                <td>
                    <ul>
                        <li>Are you ready to start ART today</li>
                    </ul>
                </td>
                <td>
                    <p>
                        <input id="switch-size-Art" name="switch-size-Art" type="checkbox" checked data-size="small"
                         data-on-text="Yes" 
                         data-off-text="No" id="swArt">
                    </p>
                </td>
                <td>
                   
                </td>
            </tr>
            <tr>
                <td>
                    Management plan
                </td>
                <td>
                    <ul>
                        <li>Which investigations will you have today</li>
                        <li>Which medications will you start today</li>
                        <li>What else is required as you start or as</li>
                    </ul>
                </td>
                <td>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="dtMPIT" style="padding: 13px 12px;">
                    </div>
                </td>
                <td>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="dtMPR" style="padding: 13px 12px;">
                    </div>
                </td>
            </tr>
             <tr>
                <td colspan="4">
                   Notes:
                                <textarea class="form-control" rows="3" placeholder="Notes"
                                    id="txtNotes"></textarea>
                </td>
                </tr>

                </table>
            </div>
            <!-- /.box-body -->
                     <div class="box-footer" align="center">
                     <input type="hidden" runat="server" id="hidId" clientidmode="Static" />
                  <button type="button" class="btn btn-primary" onclick="SaveData();">
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
