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

using Interface.Reports;
using Application.Presentation;
using Application.Common;
using Interface.Administration;

public partial class frmAdmin_FieldGroupSelector : LogPage 
{
    private DataTable MakeSelectedTable()
    {
        DataTable theDT = new DataTable();
        theDT.Columns.Add("GroupId",System.Type.GetType("System.Int32"));
        theDT.Columns.Add("GroupName", System.Type.GetType("System.String"));
        return theDT;
    }
    private void BindList()
    {
        IReports ReportManager = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
        BindFunctions BindManager = new BindFunctions();
        
        if (ViewState["AllFieldGroup"] == null)
        {
            DataTable dtAllData1 = MakeSelectedTable();
            dtAllData1 = ReportManager.GetAllFieldGroups(Convert.ToInt32(Session["SystemId"].ToString())).Tables[0];


            DataTable dtAllData = MakeSelectedTable();
            foreach (DataRow theDR1 in dtAllData1.Rows)
            {
                if (theDR1["GroupId"].ToString() !="1" )
                {
                    DataRow theDR = dtAllData.NewRow();
                    theDR[0]=theDR1[0];
                    theDR[1]=theDR1[1];
                    dtAllData.Rows.Add(theDR);
                }
            }

            
           ViewState["AllFieldGroup"] = dtAllData;
            
        }

        if (ViewState["AvailableData"] == null)
        {
            DataTable dtAvailable = MakeSelectedTable();
            ViewState["AvailableData"] = ViewState["AllFieldGroup"];
            BindManager.BindList(lstAvailable, (DataTable)ViewState["AvailableData"], "GroupName", "GroupId");
        }
        else
        {
            BindManager.BindList(lstAvailable, (DataTable)ViewState["AvailableData"], "GroupName", "GroupId");
        }
        
        if (ViewState["SelectedData"] == null)
        {
            DataTable dtSelected = MakeSelectedTable();
            ViewState["SelectedData"] = dtSelected;
            BindManager.BindList(lstAvailable, (DataTable)ViewState["AvailableData"], "GroupName", "GroupId");
        }
        else
        {
            SortDataTable((DataTable)ViewState["SelectedData"], "GroupId asc");
            BindManager.BindList(lstSelected, (DataTable)ViewState["SelectedData"], "GroupName", "GroupId");
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        try
        {
            if (IsPostBack != true)
            {

                if (Session["SelectedData"] != null)
                    ViewState["SelectedData"] = (DataTable)Session["SelectedData"];
                Session.Remove("SelectedData");

                if (Session["AvailableData"] != null)
                    ViewState["AvailableData"] = (DataTable)Session["AvailableData"];
                Session.Remove("AvailableData");
                BindList();
            }
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
            return;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Session.Add("SelectedData", (DataTable)ViewState["SelectedData"]);
        Session.Add("AvailableData", (DataTable)ViewState["AvailableData"]);
        string theScript;
        theScript = "<script language='javascript' id='DrgPopup'>\n";
        theScript += "window.opener.GetControl();\n";
        theScript += "window.close();\n";
        theScript += "</script>\n";
        Page.RegisterClientScriptBlock("Done", theScript);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //update lstSelected 
            //update lstAvailable
            //update ViewState["SelectedData"]
            //update ViewState["AvailableData"]

            if (lstAvailable.SelectedValue != "")
            {
                BindFunctions BindManager = new BindFunctions();

                //----update lstSelected------
                DataTable theDTSel = (DataTable)ViewState["SelectedData"];
                DataTable theDTAvail = (DataTable)ViewState["AvailableData"];

                DataView theDV = new DataView((DataTable)theDTAvail);
                theDV.RowFilter = "GroupId =" + lstAvailable.SelectedValue;
                DataRow theDR = theDTSel.NewRow();
                theDR[0] = Convert.ToInt32(theDV[0][0]);                    ////(lstAvailable.SelectedValue);
                theDR[1] = theDV[0][1].ToString();
                theDTSel.Rows.Add(theDR);
                ViewState["SelectedData"] = theDTSel; //----update ViewState["SelectedData"]

                SortDataTable((DataTable)ViewState["SelectedData"], "GroupId asc");
                BindManager.BindList(lstSelected, (DataTable)ViewState["SelectedData"], "GroupName", "GroupId");

                //----update ViewState["AvailableData"]------
                foreach (DataRow theDR1 in ((DataTable)ViewState["AvailableData"]).Rows)
                {
                    if (theDR1[0].ToString() == lstAvailable.SelectedValue.ToString())
                    {
                        ((DataTable)ViewState["AvailableData"]).Rows.Remove(theDR1);
                        break;
                    }
                }
                //----update lstAvailable------
                SortDataTable((DataTable)ViewState["AvailableData"], "GroupId asc");
                BindManager.BindList(lstAvailable, (DataTable)ViewState["AvailableData"], "GroupName", "GroupId");
                Session["AvailableData"] = ViewState["AvailableData"];
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
            //update ViewState["SelectedData"]
            //update ViewState["AvailableData"]
            //update lstSelected
            //update lstAvailable

            if (lstSelected.SelectedValue != "")
            {

                BindFunctions BindManager = new BindFunctions();

                //update ViewState["AvailableData"]
                DataTable theDTSel = (DataTable)ViewState["SelectedData"];
                DataTable theDTAvail = (DataTable)ViewState["AvailableData"];

                DataView theDV = new DataView(theDTSel);
                theDV.RowFilter = "GroupId =" + lstSelected.SelectedValue;
                DataRow theDR = theDTAvail.NewRow();
                theDR[0] = Convert.ToInt32(theDV[0][0]);                    ////(lstAvailable.SelectedValue);
                theDR[1] = theDV[0][1].ToString();
                theDTAvail.Rows.Add(theDR);
                ViewState["AvailableData"] = theDTAvail;
                //update lstSelected

                SortDataTable((DataTable)ViewState["AvailableData"], "GroupId asc");
                BindManager.BindList(lstAvailable, (DataTable)ViewState["AvailableData"], "GroupName", "GroupId");

                //----update ViewState["SelectedData"]------
                foreach (DataRow theDR1 in ((DataTable)ViewState["SelectedData"]).Rows)
                {
                    if (theDR1[0].ToString() == lstSelected.SelectedValue.ToString())
                    {
                        ((DataTable)ViewState["SelectedData"]).Rows.Remove(theDR1);
                        break;
                    }
                }
                //----update lstSelected------
                SortDataTable((DataTable)ViewState["SelectedData"], "GroupId asc");
                BindManager.BindList(lstSelected, (DataTable)ViewState["SelectedData"], "GroupName", "GroupId");
                Session["AvailableData"] = ViewState["AvailableData"];
            }
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }
    private static void SortDataTable(DataTable dt, string sort)
    {
        DataTable newDT = dt.Clone();
        int rowCount = dt.Rows.Count;

        DataRow[] foundRows = dt.Select(null, sort);
        for (int i = 0; i < rowCount; i++)
        {
            object[] arr = new object[dt.Columns.Count];
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                arr[j] = foundRows[i][j];
            }
            DataRow data_row = newDT.NewRow();
            data_row.ItemArray = arr;
            newDT.Rows.Add(data_row);
        }

        //clear the incoming dt 
        dt.Rows.Clear();

        for (int i = 0; i < newDT.Rows.Count; i++)
        {
            object[] arr = new object[dt.Columns.Count];
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                arr[j] = newDT.Rows[i][j];
            }

            DataRow data_row = dt.NewRow();
            data_row.ItemArray = arr;
            dt.Rows.Add(data_row);
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        string theScript;
        theScript = "<script language='javascript' id='DrgPopup'>\n";
        theScript += "window.close();\n";
        theScript += "</script>\n";
       RegisterStartupScript ("DrgPopup", theScript);
    }

}
