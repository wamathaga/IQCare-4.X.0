using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Interface.Clinical;
using Interface.Security;
using Application.Common;
using Application.Presentation;

namespace PresentationApp.ClinicalForms.UserControl
{

    public partial class UserControlKNH_WHOStaging : System.Web.UI.UserControl
    {
        IKNHStaticForms WHOManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            WHOManager = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");

            if (!IsPostBack)
            {
                if (GblIQCare.FormId == 174 || GblIQCare.FormId == 177)
                {
                    BindGridView(gvWHO1, "WHOStageIConditions");
                    BindGridView(gvWHO2, "WHOStageIIConditions");
                    BindGridView(gvWHO3, "WHOStageIIICoditions");
                    BindGridView(gvWHO4, "WHOStageIVConditions");
                }
                else
                {
                    BindGridView(gvWHO1, "CurrentWHOStageIConditions");
                    BindGridView(gvWHO2, "CurrentWHOStageIIConditions");
                    BindGridView(gvWHO3, "CurrentWHOStageIIIConditions");
                    BindGridView(gvWHO4, "CurrentWHOStageIVConditions");
                }
                //CreateMultiSelectwithDate(0, PnlWHO1, "Presenting WHO Stage I Conditions", "Current", "Historic", "CurrentWHOStageIConditions");
                //CreateMultiSelectwithDate(0, PnlWHO2, "Presenting WHO Stage II conditions", "Current", "Historic", "CurrentWHOStageIIConditions");
                //CreateMultiSelectwithDate(0, PnlWHO3, "Presenting WHO Stage III conditions", "Current", "Historic", "CurrentWHOStageIIIConditions");
                //CreateMultiSelectwithDate(0, PnlWHO4, "Presenting Stage IV conditions", "Current", "Historic", "CurrentWHOStageIVConditions");
                //BindDropdown(ddlWABStage, "WABStage");
                BindDropdown(ddlWABStage, "WAB Stage");
                BindDropdown(ddltannerstaging, "TannerStaging");
                BindDropdown(ddlInitiationWHOstage, "InitiationWHOstage");
                BindDropdown(ddlwhostage1, "InitiationWHOstage");
                BindDropdown(ddlhivassociated, "HIVAssociatedConditionsPeads");
                BindDropdown(ddlPatFUstatus, "FollowUpStatus");

                DataSet dsLatestWHOStage = WHOManager.GetLatestWHOStage(Convert.ToInt32(Session["PatientId"]));

                if (dsLatestWHOStage.Tables[0].Rows.Count > 0)
                {
                    ddlwhostage1.SelectedValue = dsLatestWHOStage.Tables[0].Rows[0]["WHOStage"].ToString();
                }

                for (int i = 0; i < ddlwhostage1.SelectedIndex; i++)
                {
                    ddlwhostage1.Items[i].Attributes.Add("Disabled", "Disabled");
                }

            }

        }
        private void BindGridView(GridView gv, string fieldname)
        {
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\AllMasters.con"));
            DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
            DataTable theDT = new DataTable();
            if (theCodeDT.Rows.Count > 0)
            {
                DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + theCodeDT.Rows[0]["CodeId"];

                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            }
            gv.DataSource = theDT;
            gv.DataBind();
        }
        private void CreateMultiSelectwithDate(int reqflg, System.Web.UI.WebControls.Panel DIVCustomItem, string fieldlabel, string fieldlabel2, string fieldlabel3, string fieldname)
        {
            //DIVCustomItem.CssClass = "center formbg";
            //DIVCustomItem.Controls.Add(new LiteralControl("</br>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<table cellspacing='6' cellpadding='0' width='100%' border='0'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<div class='customdivborder leftallign' runat='server' nowrap='nowrap'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<table width=100%>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td width='30%' align='left'>"));

            if (reqflg == 1)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='left' id='lbl-" + fieldlabel + "'>" + fieldlabel + " </label>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='left' id='lbl-" + fieldlabel + "'>" + fieldlabel + " </label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td width='20%' align='left'>"));
            if (reqflg == 1)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='left' id='lbl-" + fieldlabel2 + "'>" + fieldlabel2 + " </label>"));
            }
            else
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='left' id='lbl-" + fieldlabel2 + "'>" + fieldlabel2 + " </label>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));

            if (hiddateshow.Value == "SHOW")
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                //start
                DIVCustomItem.Controls.Add(new LiteralControl("<td width='20%' align='left'>"));
                if (reqflg == 1)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='left' id='lbl-" + fieldlabel3 + "'>" + fieldlabel3 + " </label>"));
                }
                else
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl-" + fieldlabel3 + "'>" + fieldlabel3 + " </label>"));
                }
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            }
            //end
            if (hiddateshow.Value == "HIDE")
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            }
            DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td colspan='4' class='border'>"));

            //WithPanel
            System.Web.UI.WebControls.Panel PnlMulti = new System.Web.UI.WebControls.Panel();
            PnlMulti.ID = "Pnl_-" + fieldname;
            PnlMulti.ToolTip = fieldlabel;
            PnlMulti.Controls.Add(new LiteralControl("<div class='customdivborder1 leftallign' runat='server' nowrap='nowrap'>"));
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\AllMasters.con"));
            DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
            DataTable theDT = new DataTable();
            if (theCodeDT.Rows.Count > 0)
            {
                DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + theCodeDT.Rows[0]["CodeId"];

                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
            }
            BindFunctions BindManager = new BindFunctions();
            if (theDT != null)
            {
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    // Dates Control creation for multi Select list
                    //Date 1 Control
                    TextBox theDate1 = new TextBox();
                    theDate1.ID = "TXTDT1-" + theDT.Rows[i][0] + "-" + fieldname;
                    Control ctl = (TextBox)theDate1;
                    theDate1.Width = 83;
                    theDate1.MaxLength = 11;
                    theDate1.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");


                    //string thDTVar = "ctl00_IQCareContentPlaceHolder_" + DIVCustomItem.ID + "_" + theDate1.ClientID;
                    string thDTVar = theDate1.ClientID;
                    theDate1.Attributes.Add("onblur", "DateFormat(this, this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + thDTVar + "', '" + thDTVar + "')");

                    Image theDateImage1 = new Image();
                    theDateImage1.ID = "img" + theDate1.ID;
                    theDateImage1.Height = 22;
                    theDateImage1.Width = 22;
                    //theDateImage1.Visible = theEnable;
                    theDateImage1.ToolTip = "Date Helper";
                    theDateImage1.ImageUrl = "~/images/cal_icon.gif";
                    theDateImage1.Attributes.Add("onClick", "w_displayDatePicker('" + ((TextBox)ctl).ClientID + "');");
                    theDate1.Visible = false;
                    theDateImage1.Visible = false;

                    //date 2 start

                    TextBox theDate2 = new TextBox();
                    theDate2.ID = "TXTDT2-" + theDT.Rows[i][0] + "-" + fieldname;
                    Control ctl1 = (TextBox)theDate2;
                    theDate2.Width = 83;
                    theDate2.MaxLength = 11;
                    theDate2.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");


                    //string thDTVar = "ctl00_IQCareContentPlaceHolder_" + DIVCustomItem.ID + "_" + theDate1.ClientID;
                    string thDTVar1 = theDate2.ClientID;
                    theDate2.Attributes.Add("onblur", "DateFormat(this, this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + thDTVar1 + "', '" + thDTVar1 + "')");

                    Image theDateImage2 = new Image();
                    theDateImage2.ID = "img" + theDate2.ID;
                    theDateImage2.Height = 22;
                    theDateImage2.Width = 22;
                    //theDateImage1.Visible = theEnable;
                    theDateImage2.ToolTip = "Date Helper";
                    theDateImage2.ImageUrl = "~/images/cal_icon.gif";
                    theDateImage2.Attributes.Add("onClick", "w_displayDatePicker('" + ((TextBox)ctl1).ClientID + "');");
                    theDate2.Visible = false;
                    theDateImage2.Visible = false;


                    //
                    CheckBox chkbox = new CheckBox();
                    chkbox.ID = Convert.ToString("CHKMULTI-" + theDT.Rows[i][0] + "-" + fieldname);
                    chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);

                    PnlMulti.Controls.Add(chkbox);
                    PnlMulti.Controls.Add(theDate1);
                    PnlMulti.Controls.Add(new LiteralControl("&nbsp;"));
                    PnlMulti.Controls.Add(theDateImage1);
                    PnlMulti.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
                    chkbox.Width = 210;
                    if (hiddateshow.Value == "HIDE")
                    {
                        PnlMulti.Controls.Add(new LiteralControl("<br/>"));
                    }
                    theDate1.Visible = true;
                    theDateImage1.Visible = true;
                    //
                    if (hiddateshow.Value == "SHOW")
                    {
                        PnlMulti.Controls.Add(new LiteralControl("&nbsp;"));
                        PnlMulti.Controls.Add(new LiteralControl("&nbsp;"));
                        PnlMulti.Controls.Add(theDate2);
                        PnlMulti.Controls.Add(new LiteralControl("&nbsp;"));
                        PnlMulti.Controls.Add(theDateImage2);
                        PnlMulti.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
                        PnlMulti.Controls.Add(new LiteralControl("<br/>"));
                        theDate2.Visible = true;
                        theDateImage2.Visible = true;
                    }
                }
            }
            PnlMulti.Controls.Add(new LiteralControl("</div>"));

            DIVCustomItem.Controls.Add(PnlMulti);
            //ApplyBusinessRules(PnlMulti, ControlID, theEnable);
            //PnlMulti.Enabled = theEnable;
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</div>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
        }
        private void BindDropdown(DropDownList DropDownID, string fieldname)
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataView theCodeDV = new DataView(theDS.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);

            if (theDS.Tables["Mst_Decode"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Decode"]);
                if (theCodeDT.Rows.Count > 0)
                {
                    theDV.RowFilter = "DeleteFlag=0 and CodeId=" + theCodeDT.Rows[0]["CodeId"];
                    if (theDV.Table != null)
                    {
                        DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(DropDownID, theDT, "Name", "Id");
                    }
                }

            }
        }
    }
}