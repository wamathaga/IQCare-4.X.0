using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Interface.Security;
using Application.Common;
using Interface.Administration;
using Application.Presentation;
using Interface.Clinical;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControl_NigeriaPriorART : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if(!IsPostBack)
                Binddropdwn();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JavaScriptFunctionsOnLoad();
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    INigeriaARTCard NigAdultIE = (INigeriaARTCard)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNigeriaARTCard, BusinessProcess.Clinical");
                    DataSet dsGet = NigAdultIE.GetNigeriaPriorARTDetails(Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["PatientVisitId"].ToString()));
                    if (dsGet.Tables[0].Rows.Count > 0)
                    {
                        Session["PriorGridData"] = dsGet.Tables[0];
                        GrdPriorART.Columns.Clear();
                        BindGrid();
                        GrdPriorART.DataSource = dsGet.Tables[0];                        
                        GrdPriorART.DataBind();
                    }
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                rbtnknownNo.Checked = true;
                double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());
                if (age >= 15)
                {
                    if (rbtnknownNo.Checked == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "DisableN", "EnableDis('N');", true);
                    }
                }
            }
        }
        private void Binddropdwn()
        {
            IUser theLocationManager;
            theLocationManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
            DataTable theDT = theLocationManager.GetFacilityList();           
            BindFunctions theBindManger = new BindFunctions();
            theBindManger.BindCombo(ddlfacilityname, theDT, "FacilityName", "FacilityId");
            IQCareUtils util=new IQCareUtils();
            DataTable theEntryDT = util.GetDataTable("MST_CODE", "EntryType");
            theBindManger.BindCombo(ddlentrytype, theEntryDT, "Name", "Id");
        }
        private void JavaScriptFunctionsOnLoad()
        {
            rbtnknownNo.Checked = true;
            double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());

            if (age >= 15)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showHidePeadsPrior", "show_Priorhide('divshowpreviousexposure','visible');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showHidePeadsPrior", "show_Priorhide('divshowpreviousexposure','notvisible');", true);
            }
        }
        private Boolean ValidateLastUsed()
        {            
            IQCareUtils theUtil = new IQCareUtils();
            MsgBuilder totalMsgBuilder = new MsgBuilder();
            if (rbtnknownYes.Checked)
            {
                if (txtdurationfrom.Value == "")
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Enter Duration From Date";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    txtdurationfrom.Focus();
                    return false;
                }

                else if (txtdurationto.Value == "")
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Enter Duration To Date";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    txtdurationto.Focus();
                    return false;
                }

                else if (ddlfacilityname.SelectedValue == "0")
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Facility Name";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    ddlfacilityname.Focus();
                    return false;
                }
                else if (ddlentrytype.SelectedValue == "0")
                {
                    totalMsgBuilder.DataElements["MessageText"] = "Select Entry type";
                    IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                    ddlentrytype.Focus();
                    return false;
                }
            }
            return true;
        }
        protected void btnAddPriorART_Click(object sender, EventArgs e)
        {
            if (ValidateLastUsed() == false)
            {
                return;
            }
            int VisitId = Convert.ToInt32(Session["PatientVisitId"]) > 0 ? Convert.ToInt32(Session["PatientVisitId"]) : 0;
            DataTable theDT = new DataTable();
            if ((DataTable)Session["PriorGridData"] == null)
            {
                theDT.Columns.Add("ptn_pk", typeof(Int32));
                theDT.Columns.Add("VisitId", typeof(Int32));
                theDT.Columns.Add("FacilityId", typeof(Int32));
                theDT.Columns.Add("EntrytypeId", typeof(Int32));
                theDT.Columns.Add("Facilityname", typeof(string));
                theDT.Columns.Add("EntryType", typeof(string));
                theDT.Columns.Add("DurationFromDate", typeof(string));
                theDT.Columns.Add("DurationToDate", typeof(string));
                DataRow theDR = theDT.NewRow();
                theDR["ptn_pk"] = Session["PatientId"];
                theDR["VisitId"] = VisitId;
                theDR["Facilityname"] = ddlfacilityname.SelectedItem.Text;
                theDR["EntryType"] = ddlentrytype.SelectedItem.Text;
                theDR["DurationFromDate"] = "" + txtdurationfrom.Value + "";
                theDR["DurationToDate"] = "" + txtdurationto.Value + "";
                theDR["FacilityId"] = ddlfacilityname.SelectedValue;
                theDR["EntrytypeId"] = ddlentrytype.SelectedValue;
                theDT.Rows.Add(theDR);
                GrdPriorART.Columns.Clear();
                BindGrid();
                Refresh();
                GrdPriorART.DataSource = theDT;
                GrdPriorART.DataBind();
                Session["PriorGridData"] = theDT;
            }
            else
            {

                theDT = (DataTable)Session["PriorGridData"];
                if (Convert.ToInt32(ViewState["UpdateFlag"]) == 1)
                {
                    DataRow[] rows = theDT.Select("FacilityId=" + ViewState["SelectedFacilityId"] );
                    for (int i = 0; i < rows.Length; i++)
                    {
                        rows[i]["ptn_pk"] = Session["PatientId"];
                        rows[i]["VisitId"] = VisitId;
                        rows[i]["FacilityId"] = ddlfacilityname.SelectedValue;
                        rows[i]["Facilityname"] = ddlfacilityname.SelectedItem.Text;
                        rows[i]["EntryTypeId"] = ddlfacilityname.SelectedValue;
                        rows[i]["EntryType"] = ddlentrytype.SelectedItem.Text;
                        rows[i]["DurationFromDate"] = "" + txtdurationfrom.Value + "";
                        rows[i]["DurationToDate"] = "" + txtdurationto.Value + "";
                        theDT.AcceptChanges();
                    }
                    GrdPriorART.Columns.Clear();
                    BindGrid();
                    Refresh();
                    GrdPriorART.DataSource = theDT;
                    GrdPriorART.DataBind();
                    Session["PriorGridData"] = theDT;
                    ViewState["UpdateFlag"] = "0";
                    DataTable theGVDT = (DataTable)GrdPriorART.DataSource;
                }
                else
                {
                    DataRow[] duplicaterows = theDT.Select("FacilityId=" + ddlfacilityname.SelectedValue + " and EntrytypeId=" + ddlentrytype.SelectedValue + " and DurationFromDate='" + txtdurationfrom.Value + "' and DurationToDate='" + txtdurationto.Value + "'");
                    if (duplicaterows.Length == 0)
                    {
                        DataRow theDR = theDT.NewRow();
                        theDR["ptn_pk"] = Session["PatientId"];
                        theDR["VisitId"] = VisitId;
                        theDR["Facilityname"] = ddlfacilityname.SelectedItem.Text;
                        theDR["EntryType"] = ddlentrytype.SelectedItem.Text;
                        theDR["DurationFromDate"] = "" + txtdurationfrom.Value + "";
                        theDR["DurationToDate"] = "" + txtdurationto.Value + "";
                        theDR["FacilityId"] = ddlfacilityname.SelectedValue;
                        theDR["EntrytypeId"] = ddlentrytype.SelectedValue;
                        theDT.Rows.Add(theDR);
                        GrdPriorART.Columns.Clear();
                        BindGrid();
                        Refresh();
                        GrdPriorART.DataSource = theDT;
                        GrdPriorART.DataBind();
                        Session["PriorGridData"] = theDT;
                    }
                }
            }
        }
        protected void GrdPriorART_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[0].Text.ToString() != "0")
                    {
                        e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                        e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                        e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GrdPriorART, "Select$" + e.Row.RowIndex.ToString()));
                    }
                    if (e.Row.Cells[3].Text.ToString() == "01-Jan-1900")
                    {
                        e.Row.Cells[3].Text = "";
                    }
                    if (e.Row.Cells[4].Text.ToString() == "01-Jan-1900")
                    {
                        e.Row.Cells[4].Text = "";
                    }

                }
            }
        }
        protected void GrdPriorART_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = GrdPriorART.PageIndex;
            int thePageSize = GrdPriorART.PageSize;
            GridViewRow theRow = GrdPriorART.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)Session["PriorGridData"]);
            int r = theIndex;
            if (theDT.Rows.Count > 0)
            {
                ViewState["UpdateFlag"] = 1;
                ddlfacilityname.SelectedValue = theDT.Rows[r]["FacilityId"].ToString();
                ViewState["SelectedFacilityId"] = Convert.ToInt32(theDT.Rows[r]["FacilityId"]);
                ddlfacilityname.SelectedValue = theDT.Rows[r]["EntrytypeId"].ToString();
                ViewState["SelectedEntrytypeId"] = Convert.ToInt32(theDT.Rows[r]["EntrytypeId"]);

                txtdurationfrom.Value = String.Format("{0:dd-MMM-yyyy}", theDT.Rows[r]["DrationFromDate"]);
                ViewState["SelectedDrationFromDate"] = theDT.Rows[r]["DrationFromDate"].ToString();

                txtdurationto.Value = String.Format("{0:dd-MMM-yyyy}", theDT.Rows[r]["DrationToDate"]);
                ViewState["SelectedDrationToDate"] = theDT.Rows[r]["DrationToDate"].ToString();
                

            }
        }
        protected void GrdPriorART_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)Session["PriorGridData"]);
            int r = Convert.ToInt32(e.RowIndex.ToString());

            int Id = -1;
            try
            {
                if (theDT.Rows.Count > 0)
                {

                    if (theDT.Rows[r].HasErrors == false)
                    {
                        if ((theDT.Rows[r]["FacilityId"] != null) && (theDT.Rows[r]["FacilityId"] != DBNull.Value))
                        {
                            if (theDT.Rows[r]["FacilityId"].ToString() != "")
                            {
                                Id = Convert.ToInt32(theDT.Rows[r]["FacilityId"]);
                                theDT.Rows[r].Delete();
                                theDT.AcceptChanges();
                                Session["PriorGridData"] = theDT;
                                GrdPriorART.Columns.Clear();
                                BindGrid();
                                Refresh();
                                GrdPriorART.DataSource = (DataTable)Session["PriorGridData"];
                                GrdPriorART.DataBind();
                                //IQCareMsgBox.Show("DeleteSuccess", this);
                            }
                        }
                    }



                    if (((DataTable)Session["PriorGridData"]).Rows.Count == 0)
                        btnAddPriorART.Enabled = false;
                    else
                        btnAddPriorART.Enabled = true;
                }
                else
                {
                    GrdPriorART.Visible = false;
                    //IQCareMsgBox.Show("DeleteSuccess", this);
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "VisitId";
            theCol0.DataField = "VisitId";
            theCol0.ItemStyle.CssClass = "textstyle";
            GrdPriorART.Columns.Add(theCol0);

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Patientid";
            theCol1.DataField = "ptn_pk";
            theCol1.ItemStyle.CssClass = "textstyle";
            GrdPriorART.Columns.Add(theCol1);            

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Facility Name";
            theCol2.DataField = "FacilityName";
            theCol2.SortExpression = "FacilityName";
            theCol2.ReadOnly = true;
            GrdPriorART.Columns.Add(theCol2);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Duration of Care From";
            theCol3.DataField = "DurationFromDate";
            theCol3.DataFormatString = "{0:dd-MMM-yyyy}";
            theCol3.SortExpression = "DurationFromDate";
            theCol3.ReadOnly = true;
            GrdPriorART.Columns.Add(theCol3);

            BoundField theCol8 = new BoundField();
            theCol8.HeaderText = "Duration of Care To";
            theCol8.DataField = "DurationToDate";
            theCol8.DataFormatString = "{0:dd-MMM-yyyy}";
            theCol8.SortExpression = "DurationToDate";
            theCol8.ReadOnly = true;
            GrdPriorART.Columns.Add(theCol8);

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Entry type";
            theCol4.DataField = "Entrytype";
            theCol4.SortExpression = "Entrytype";
            theCol4.ReadOnly = true;
            GrdPriorART.Columns.Add(theCol4);

            CommandField theCol5 = new CommandField();
            theCol5.ButtonType = ButtonType.Link;
            theCol5.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
            theCol5.ShowDeleteButton = true;
            GrdPriorART.Columns.Add(theCol5);

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "FacilityId";
            theCol6.DataField = "FacilityId";
            GrdPriorART.Columns.Add(theCol6);

            BoundField theCol7 = new BoundField();
            theCol7.HeaderText = "EntryTypeId";
            theCol7.DataField = "EntryTypeId";
            GrdPriorART.Columns.Add(theCol7);

            GrdPriorART.Columns[0].Visible = false;
            GrdPriorART.Columns[1].Visible = false;
            GrdPriorART.Columns[7].Visible = false;
            GrdPriorART.Columns[8].Visible = false;
            
        }
        private void Refresh()
        {
            ddlfacilityname.SelectedIndex = -1;
            ddlentrytype.SelectedIndex = -1;
            txtdurationfrom.Value = "";
            txtdurationto.Value = "";

        }
    }
}