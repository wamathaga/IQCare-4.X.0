using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Management;
using Interface.Administration;
using Application.Common;
using Application.Presentation;
using Interface.Security;
using System.IO;



public partial class AdminForms_frmAdmin_AuditTrail : LogPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        IIQCareSystem MgrSecurity;
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
        //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
        try
        {
            if (!IsPostBack)
            {
                Session["PatientID"] = null;
                DataTable theDT = (DataTable)Session["AppModule"];
                String ModuleId = Convert.ToString(theDT.Rows[0]["ModuleId"]);
                BindFunctions BindManager = new BindFunctions();
                MgrSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                DataSet theDSVisitForm = MgrSecurity.GetVisitForms();
                DataView theDV = new DataView(theDSVisitForm.Tables[0]);
                theDV.RowFilter = "DeleteFlag = 0 and SystemID IN(" + Session["SystemId"] + ")"; 
                BindManager.BindCombo(ddAuditTrail, theDV.ToTable(), "VisitName", "VisitTypeID");
                DataSet theDS = MgrSecurity.GetMySQLAuditTrailData();
                Session["theDS"] = theDS.Tables[0];
            }
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
            return;
        }
        finally
        {
            MgrSecurity = null;
        }

    }
    private Boolean Validate()
    {
        if (Convert.ToInt32(ddAuditTrail.Text) < 0)
        {
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Select Form";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                return false;
            }

        }

        if (txtFromDate.Value == "")
        {
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "From Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtFromDate.Focus();
                return false;
            }
        }
        if (txtToDate.Value == "")
        {
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "To Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtToDate.Focus();
                return false;
            }
        }
        return true;
    }
    private void BindGrid()
    {

        BoundField theCol0 = new BoundField();
        theCol0.HeaderText = "Patientid";
        theCol0.DataField = "Ptn_Pk";
        theCol0.ItemStyle.Width = Unit.Percentage(7);
        //theCol0.ItemStyle.CssClass = "textstyle";
        theCol0.ReadOnly = true;

        BoundField theCol1 = new BoundField();
        theCol1.HeaderText = "VisitId";
        theCol1.DataField = "VisitId";
        //theCol1.ItemStyle.CssClass = "textstyle";
        theCol1.ItemStyle.Width = Unit.Percentage(7);
        theCol1.ReadOnly = true;

        BoundField theCol2 = new BoundField();
        theCol2.HeaderText = "Location";
        theCol2.DataField = "LocationId";
        //theCol2.SortExpression = "LocationId";
        //theCol2.ItemStyle.CssClass = "textstyle";
        theCol2.ItemStyle.Width = Unit.Percentage(7);
        theCol2.ReadOnly = true;

        BoundField theCol3 = new BoundField();
        theCol3.HeaderText = "visitdate";
        theCol3.DataField = "visitdate";
        //theCol3.ItemStyle.CssClass = "textstyle";
        //theCol3.ItemStyle.Width = Unit.Percentage(10);
        //theCol3.SortExpression = "visitdate";
        theCol3.ReadOnly = true;

        BoundField theCol4 = new BoundField();
        theCol4.HeaderText = "Operation";
        //theCol4.ItemStyle.CssClass = "textstyle";
        theCol4.DataField = "OperationType";
        //theCol4.SortExpression = "OperationType";
        theCol4.ItemStyle.Width = Unit.Percentage(10);
        //theCol4.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
        //theCol4.ItemStyle.VerticalAlign = VerticalAlign.Top;
        theCol4.ReadOnly = true;

        BoundField theCol5 = new BoundField();
        theCol5.HeaderText = "TableName";
        //theCol5.ItemStyle.CssClass = "textstyle";
        theCol5.DataField = "TableName";
        //theCol5.SortExpression = "TableName";
        //theCol5.ItemStyle.Width = Unit.Percentage(15);
        //theCol5.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
        //theCol5.ItemStyle.VerticalAlign = VerticalAlign.Top;
        theCol5.ReadOnly = true;

        BoundField theCol6 = new BoundField();
        theCol6.HeaderText = "User";
        //theCol5.ItemStyle.CssClass = "textstyle";
        theCol6.DataField = "UserId";
        //theCol6.SortExpression = "UserId";
        theCol6.ItemStyle.Width = Unit.Percentage(7);
        //theCol6.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
        //theCol6.ItemStyle.VerticalAlign = VerticalAlign.Top;
        theCol6.ReadOnly = true;

        BoundField theCol7 = new BoundField();
        theCol7.HeaderText = "FieldName";
        //theCol7.ItemStyle.CssClass = "textstyle";
        theCol7.DataField = "FieldName";
        //theCol7.SortExpression = "FieldName";
        //theCol7.ItemStyle.Width = Unit.Percentage(10);
        theCol7.ReadOnly = true;

        BoundField theCol8 = new BoundField();
        theCol8.HeaderText = "OldValue";
        theCol8.DataField = "OldValue";
        //theCol8.ItemStyle.CssClass = "textstyle";
        //theCol8.SortExpression = "OldValue";
        //theCol8.ItemStyle.Width = Unit.Percentage(10);
        theCol8.ReadOnly = true;

        BoundField theCol9 = new BoundField();
        theCol9.HeaderText = "NewValue";
        theCol9.DataField = "NewValue";
        //theCol9.ItemStyle.CssClass = "textstyle";
        //theCol9.SortExpression = "NewValue";
        //theCol9.ItemStyle.Width = Unit.Percentage(10);
        theCol9.ReadOnly = true;

        BoundField theCol10 = new BoundField();
        theCol10.HeaderText = "createdate";
        //theCol10.ItemStyle.CssClass = "textstyle";
        theCol10.DataField = "createdate";
        //theCol10.SortExpression = "createdate";
        //theCol10.ItemStyle.Width = Unit.Percentage(10);
        theCol10.ReadOnly = true;

        GrdAuditTrail.Columns.Add(theCol0);
        GrdAuditTrail.Columns.Add(theCol1);
        GrdAuditTrail.Columns.Add(theCol2);
        GrdAuditTrail.Columns.Add(theCol3);
        GrdAuditTrail.Columns.Add(theCol4);
        GrdAuditTrail.Columns.Add(theCol5);
        GrdAuditTrail.Columns.Add(theCol6);
        GrdAuditTrail.Columns.Add(theCol7);
        GrdAuditTrail.Columns.Add(theCol8);
        GrdAuditTrail.Columns.Add(theCol9);
        GrdAuditTrail.Columns.Add(theCol10);
        GrdAuditTrail.DataBind();
       
    }
   
    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            if (Validate() == false)
            {
                return;
            }
            DataView theDV = new DataView((DataTable)Session["theDS"]);
            if (Session["theDS"] != null)
            {
                
               if (ddAuditTrail.SelectedItem.Text == "User Administration")
                {
                    theDV.RowFilter = "(TableName = 'mst_User' or TableName = 'lnk_usergroup') and (createdate >= '" + Convert.ToDateTime(txtFromDate.Value).ToString("dd-MMM-yyyy") + "' and createdate <= '" + Convert.ToDateTime(txtToDate.Value).ToString("dd-MMM-yyyy") + "')";
                    GrdAuditTrail.Dispose();
                    GrdAuditTrail.DataSource = null;
                    GrdAuditTrail.Columns.Clear();
                    GrdAuditTrail.DataSource = theDV.ToTable();
                    BindGrid();
                }
                else if (ddAuditTrail.SelectedItem.Text == "User Group Administration")
                {
                    theDV.RowFilter = "(TableName = 'mst_Groups' or TableName = 'lnk_GroupFeatures') and (createdate >= '" + Convert.ToDateTime(txtFromDate.Value).ToString("dd-MMM-yyyy") + "' and createdate <= '" + Convert.ToDateTime(txtToDate.Value).ToString("dd-MMM-yyyy") + "')";
                    GrdAuditTrail.Dispose();
                    GrdAuditTrail.DataSource = null;
                    GrdAuditTrail.Columns.Clear();
                    GrdAuditTrail.DataSource = theDV.ToTable();
                    BindGrid();
                }
                else if (ddAuditTrail.SelectedItem.Text == "Care Ended")
                {
                    theDV.RowFilter = "(TableName = 'dtl_PatientCareEnded') and (createdate >= '" + Convert.ToDateTime(txtFromDate.Value).ToString("dd-MMM-yyyy") + "' and createdate <= '" + Convert.ToDateTime(txtToDate.Value).ToString("dd-MMM-yyyy") + "')";
                    GrdAuditTrail.Dispose();
                    GrdAuditTrail.DataSource = null;
                    GrdAuditTrail.Columns.Clear();
                    GrdAuditTrail.DataSource = theDV.ToTable();
                    BindGrid();
                }
                else
                {
                    theDV.RowFilter = "VisitType="+ddAuditTrail.SelectedValue+" and (createdate >= '" + Convert.ToDateTime(txtFromDate.Value).ToString("dd-MMM-yyyy") + "' and createdate <= '" + Convert.ToDateTime(txtToDate.Value).ToString("dd-MMM-yyyy") + "')";
                    GrdAuditTrail.DataSource = null;
                    GrdAuditTrail.Columns.Clear();
                    GrdAuditTrail.DataSource = theDV.ToTable();
                    BindGrid();
                }
                
            }
            else
            {
                IQCareMsgBox.Show("AuditTrailRecord", this);
            }
            if (GrdAuditTrail.Rows.Count > 0)
            {
                btnExporttoExcel.Enabled = true;
            }
        }
        catch (Exception err)
        {
            throw new ApplicationException("Either configuration missing or connection String not properly set", err);
            //MsgBuilder theBuilder = new MsgBuilder();
            //theBuilder.DataElements["MessageText"] = err.Message.ToString();
            ////theBuilder.DataElements["MessageText"] = "Either configuration missing or connection String not properly set";
            //IQCareMsgBox.Show("#C1", theBuilder, this);
            //return;
        }
        finally
        {

        }
    }
    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel(GrdAuditTrail, "AuditTrail.xls");  
    }
    public void ExportGridToExcel(GridView grdGridView, string fileName)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Audittrail.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter ht = new HtmlTextWriter(sw);
        grdGridView.RenderControl(ht);
        Response.Write(sw.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void GrdAuditTrail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //GrdAuditTrail.Columns[1].ItemStyle.Width = 1;
            //GrdAuditTrail.Columns[2].ItemStyle.Width = 1;
            //GrdAuditTrail.Columns[3].ItemStyle.Width = 1;
            //GrdAuditTrail.Columns[4].ItemStyle.Width = 1;
            //GrdAuditTrail.Columns[5].ItemStyle.Width = 1;
            //GrdAuditTrail.Columns[6].ItemStyle.Width = 1;
            //GrdAuditTrail.Columns[7].ItemStyle.Width = 1;
            //GrdAuditTrail.Columns[8].ItemStyle.Width = 1;
            //GrdAuditTrail.Columns[9].ItemStyle.Width = 1;
            //GrdAuditTrail.Columns[10].ItemStyle.Width = 1;
        }
    }
    }