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
//using Microsoft.SqlServer.Management.Smo;
using System.IO;

using Application.Common;
using Application.Presentation;
using Interface.Security;
  
public partial class frmBackupset : BasePage
{
    /////////////////////////////////////////////////////////////////////
    // Code Written By   : Sanjay Rana
    // Written Date      : 09th Dec 2006
    // Modification Date : 
    // Description       : BackupSet Form (Restore DataBase)
    //
    /////////////////////////////////////////////////////////////////////
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx",true);
        }
        if (Page.IsPostBack == false)
        {
            FillList();
        }
        this.Title = "Choose Backup-Sets"; 
    }

    private void FillList()
    {
        IIQCareSystem DataManager;
        DataManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        DataSet theDS = DataManager.GetBackupFiles(Request.QueryString["drv"].ToString());
        BindFunctions theBind = new BindFunctions();
        theBind.BindList(lstBackupFile, theDS.Tables[0], "output", "output"); 
        
    }

    protected void btnLoadBkp_Click(object sender, EventArgs e)
    {
        try
        {
            if (!object.Equals(lstBackupFile.SelectedItem, null))
            {
                if (System.IO.File.Exists(Request.QueryString["drv"].ToString() + "\\" + lstBackupFile.SelectedItem.Text))
                {
                    IIQCareSystem DataManager;
                    DataManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                    DataSet theDS = DataManager.GetBackupSets(Request.QueryString["drv"].ToString() + "\\" + lstBackupFile.SelectedItem.Text);
                    Bind_Grid(theDS);
                }
                else
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "Invalid file. Please select a valid path.";
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                }
            }
            else
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "Select option from the list.";
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }
        catch(Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }

    #region "User Functions"
    private void Bind_Grid(DataSet theDS)
    {
        grdBackupset.DataSource = theDS;

        BoundField Col1 = new BoundField();
        Col1.HeaderText = "BackUp Name";
        Col1.SortExpression = "BackupName";
        Col1.ItemStyle.CssClass = "textstyle";
        Col1.DataField = "BackupName";
        Col1.ReadOnly = true;

        BoundField Col2 = new BoundField();
        Col2.HeaderText = "Server Name";
        Col2.SortExpression = "ServerName";
        Col2.ItemStyle.CssClass = "textstyle";
        Col2.DataField = "ServerName";
        Col2.ReadOnly = true;

        BoundField Col3 = new BoundField();
        Col3.HeaderText = "DataBase";
        Col3.SortExpression = "DatabaseName";
        Col3.ItemStyle.CssClass = "textstyle";
        Col3.DataField = "DatabaseName";
        Col3.ReadOnly = true;

        BoundField Col4 = new BoundField();
        Col4.HeaderText = "Start Date";
        Col4.SortExpression = "BackupStartDate";
        Col4.ItemStyle.CssClass = "textstyle";
        Col4.DataField = "BackupStartDate";
        Col4.ReadOnly = true;

        BoundField Col5 = new BoundField();
        Col5.HeaderText = "End Date";
        Col5.SortExpression = "BackupFinishDate";
        Col5.ItemStyle.CssClass = "textstyle";
        Col5.DataField = "BackupFinishDate";
        Col5.ReadOnly = true;

        BoundField Col6 = new BoundField();
        Col6.HeaderText = "Position";
        Col6.ItemStyle.CssClass = "textstyle";
        Col6.DataField = "Position";
        Col6.ReadOnly = true;

        ButtonField theBtn = new ButtonField();
        theBtn.ButtonType = ButtonType.Link;
        theBtn.CommandName = "Select";
        theBtn.HeaderStyle.CssClass = "textstylehidden";
        theBtn.ItemStyle.CssClass = "textstylehidden";

        grdBackupset.Columns.Add(Col1);
        grdBackupset.Columns.Add(Col2);
        grdBackupset.Columns.Add(Col3);
        grdBackupset.Columns.Add(Col4);
        grdBackupset.Columns.Add(Col5);
        grdBackupset.Columns.Add(Col6);
        grdBackupset.Columns.Add(theBtn);

        grdBackupset.DataBind();
        grdBackupset.Columns[5].Visible = false;
        grdBackupset.Rows[0].BorderStyle = BorderStyle.Outset; 

    }
    #endregion

    public void grdBackupsetDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdBackupset, "Select$" + e.Row.RowIndex.ToString()));
            //e.Row.Attributes.Add("onclick", );
        }
    }

    protected void grdBackupset_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow theRow = grdBackupset.Rows[e.NewSelectedIndex];
        int BackupSetPosition = Convert.ToInt32(theRow.Cells[5].Text);
        string theFileName = Request.QueryString["drv"].ToString() + "\\" + lstBackupFile.SelectedItem.Text;
        Application.Add("BackupSetFile",theFileName);
        Application.Add("Position",BackupSetPosition);

        string theScript;
        theScript = "<script language='javascript' id='DrgPopup'>\n";
        theScript += "window.opener.GetControl();\n";
        theScript += "window.close();\n";
        theScript += "</script>\n";
        Page.RegisterClientScriptBlock("Done", theScript);

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string theScript;
        theScript = "<script language='javascript' id='DrgPopup'>\n";
        theScript += "window.close();\n";
        theScript += "</script>\n";
        Page.RegisterClientScriptBlock("Done", theScript);
    }
}
