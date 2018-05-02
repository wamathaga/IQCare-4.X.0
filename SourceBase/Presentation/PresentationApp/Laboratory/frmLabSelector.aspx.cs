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

using Application.Presentation;
using Application.Common;
 
public partial class frmLabSelector : LogPage
{
    #region "User Functions"
    private void Init_Form()
    {
        BindList();
    }

    private void BindList()
    {
        DataTable theDT = new DataTable();
        DataTable theTmpDT = new DataTable();

        DataTable DTSelected = new DataTable();
        if (ViewState["SelectedLab"] == null)
        {
            DTSelected.Columns.Add("LabTestID", System.Type.GetType("System.Int32"));
            DTSelected.Columns.Add("LabName", System.Type.GetType("System.String"));
            DTSelected.Columns.Add("SubTestID", System.Type.GetType("System.Int32"));
            DTSelected.Columns.Add("SubTestName", System.Type.GetType("System.String"));
            DTSelected.Columns.Add("LabTypeId", System.Type.GetType("System.Int32"));
            DTSelected.Columns.Add("Flag", System.Type.GetType("System.Int32"));
            DTSelected.Constraints.Add("Con1", DTSelected.Columns["SubTestID"], true);
            ViewState["SelectedLab"] = DTSelected;
        }
        DTSelected = (DataTable)ViewState["SelectedLab"];
        IQCareUtils theUtils = new IQCareUtils();
        
        if (ViewState["LabData"] != null)
        {
            DataView theDV = new DataView((DataTable)ViewState["LabData"]);
            #region "14-jun-07 - 1"
                // theDV.RowFilter = "LabTypeId=1";
            if (Request.QueryString["Mode"] == "Add")
            { theDV.RowFilter = "LabTypeId=1 and DeleteFlag = 0"; }
            else if (Request.QueryString["Mode"] == "All")
            { theDV.RowFilter = "DeleteFlag = 0"; }
            else
            { theDV.RowFilter = "LabTypeId=1"; }
            #endregion

           theTmpDT = theUtils.CreateTableFromDataView(theDV);
           theDT = theTmpDT;
        }
        DataView theDV1 = new DataView(theDT);
        theDV1.Sort = "SubTestName Asc";
        theDT = theUtils.CreateTableFromDataView(theDV1);

        BindFunctions theBind = new BindFunctions();
        theBind.BindList(lstSelectedLab, DTSelected, "SubTestName", "SubTestID");
        //ajay changes begin
        for (int j = 0; j < lstSelectedLab.Items.Count; j++)
        {
            DataRow[] theDR = theDT.Select("SubTestID=" + lstSelectedLab.Items[j].Value);
            if (theDR.Length != 0)
                theDT.Rows.Remove(theDR[0]);
        }
        //ajay changes end
        ViewState["LabData"] = theDT;
        theBind.BindList(lstLabList, theDT, "SubTestName", "SubTestId");
        
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        if (this.IsPostBack != true)
        {
            ViewState["SelectedLab"] = (DataTable)Session["SelectedData"];
            ViewState["LabData"] = (DataTable)Session["MasterData"];
            txtSearch.Attributes.Add("onKeyUp", this.GetPostBackClientEvent(txtSearch, "txtSearch_TextChanged"));
            Init_Form();
            if (Convert.ToString(Session["CustomfrmLab"]) != "CustomfrmLab")
            {
                Session.Remove("MasterData");
                Session.Remove("SelectedData");
            }
        }
        btnAdd.Attributes.Add("onClick", "return listBox_selected('lstLabList')");
        btnRemove.Attributes.Add("onClick", "return listBox_selected('lstSelectedLab')");
    }

    private DataTable MakeNamesTable()
    {
        // Create a new DataTable titled 'Names.'
        DataTable namesTable = new DataTable("Names");

        // Add three column objects to the table.
        DataColumn idColumn = new DataColumn();
        idColumn.DataType = System.Type.GetType("System.Int32");
        idColumn.ColumnName = "id";
        namesTable.Columns.Add(idColumn);

        // Return the new DataTable.
        return namesTable;
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        DataView theDV = new DataView((DataTable)ViewState["LabData"]);
        theDV.RowFilter = "SubTestName like '" + txtSearch.Text + "%'";
        IQCareUtils theUtil = new IQCareUtils(); 
        BindFunctions theBind = new BindFunctions();
        theBind.BindList(lstLabList, theUtil.CreateTableFromDataView(theDV), "SubTestName", "SubTestID");
        //txtSearch.Focus();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataSet theDS = new DataSet();
        DataTable DT1 = ((DataTable)ViewState["LabData"]).Copy();
        DataTable DT2 = ((DataTable)ViewState["SelectedLab"]).Copy();
        DT1.TableName = "DT1";
        DT2.TableName = "DT2"; 
        theDS.Tables.Add(DT1);
        theDS.Tables.Add(DT2);

        Session["AddLab"] = theDS;
        string theScript;
        theScript = "<script language='javascript' id='LabPopup'>\n";
        theScript += "window.opener.GetControl();\n";
        theScript += "window.close();\n";
        theScript += "</script>\n";
        Page.RegisterClientScriptBlock("Done", theScript);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable theDT = (DataTable)ViewState["SelectedLab"];
            if (lstLabList.SelectedValue !="")
            {
                DataView theDV = new DataView((DataTable)ViewState["LabData"]);
                theDV.RowFilter = "SubTestId = " + lstLabList.SelectedValue;
                DataRow theDR = theDT.NewRow();
                theDR[0] = Convert.ToInt32(theDV[0][0]);
                theDR[1] = theDV[0][1].ToString();
                theDR[2] = Convert.ToInt32(theDV[0][2]);
                theDR[3] = theDV[0][3].ToString();
                theDR[4] = Convert.ToInt32(theDV[0][4]);

                theDT.Rows.Add(theDR);
                lstSelectedLab.DataSource = theDT;
                lstSelectedLab.DataBind();
                ViewState["SelectedLab"] = theDT;

                DataTable theDT1 = (DataTable)ViewState["LabData"];
                DataRow[] theDR1 = theDT1.Select("SubTestId=" + lstLabList.SelectedValue);
                theDT1.Rows.Remove(theDR1[0]);
                lstLabList.DataSource = theDT1;
                lstLabList.DataBind();
                ViewState["LabData"] = theDT1;
                txtSearch.Focus();
                txtSearch_TextChanged(sender, e);
            }
        }
        catch(Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstSelectedLab.SelectedValue != "")
            {
                DataTable theDT = (DataTable)ViewState["LabData"];
                DataView theDV = new DataView((DataTable)ViewState["SelectedLab"]);
                theDV.RowFilter = "SubTestId = " + lstSelectedLab.SelectedValue;

                DataRow theDR = theDT.NewRow();
                theDR[0] = Convert.ToInt32(theDV[0][0]);
                theDR[1] = theDV[0][1].ToString();
                theDR[2] = Convert.ToInt32(theDV[0][2]);
                theDR[3] = theDV[0][3].ToString();
                theDR[4] = Convert.ToInt32(theDV[0][4]);


                theDT.Rows.Add(theDR);
                IQCareUtils theUtils = new IQCareUtils();
                theDV = theUtils.GridSort(theDT, "SubTestName", "asc");
                theDT = theUtils.CreateTableFromDataView(theDV);
                lstLabList.DataSource = theDT;
                lstLabList.DataBind();
                ViewState["LabData"] = theDT;

                DataTable theDT1 = (DataTable)ViewState["SelectedLab"];
                DataRow[] theDR1 = theDT1.Select("SubTestID=" + lstSelectedLab.SelectedValue);
                theDT1.Rows.Remove(theDR1[0]);
                lstSelectedLab.DataSource = theDT1;
                lstSelectedLab.DataBind();
                ViewState["SelectedLab"] = theDT1;
            }
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string theScript;
        theScript = "<script language='javascript' id='LabPopup'>\n";
        theScript += "window.close();\n";
        theScript += "</script>\n";
        RegisterStartupScript("LabPopup", theScript);
    }
}
