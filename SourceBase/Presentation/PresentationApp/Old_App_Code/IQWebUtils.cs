using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

/// <summary>
/// Summary description for IQWebUtils
/// </summary>
public class IQWebUtils
{
    #region "Constructor"
    public IQWebUtils()
    {
    }
    #endregion

    public void ExporttoExcel(DataTable theDT, HttpResponse theRes)
    {
        DataGrid theDG = new DataGrid();
        theDG.DataSource = theDT;
        theDG.DataBind();
        theRes.Clear();
        theRes.Buffer = true;
        theRes.AddHeader("Content-Disposition", "attachment; filename=frmHIVCareFacilityStatistics.xls");
        theRes.ContentType = "application/vnd.ms-excel";
        theRes.Charset = "";
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        theDG.RenderControl(oHtmlTextWriter);
        theRes.Write(oStringWriter.ToString());
        theRes.End();
    }
    //IQTools Function
    public void ExportDocument(byte[] fileData, string contentType, string fileName, HttpResponse response)
    {
        response.Clear();
        response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        response.ContentType = contentType;
        BinaryWriter bw = new BinaryWriter(response.OutputStream);
        bw.Write(fileData);
        bw.Close();
        response.End();
    }

    public void ShowExcelFile(string theFile, HttpResponse theRes)
    {
        theRes.Clear();
        theRes.Buffer = true;
        theRes.ContentType = "application/vnd.ms-excel";
        theRes.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", theFile));
        theRes.WriteFile(theFile); 
        theRes.End();

    }

    public void ShowFile(string theFile, HttpResponse theRes)
    {
        theRes.Clear();
        theRes.Buffer = true;
        theRes.ContentType = "application/vnd.ms-htmlhelp";
        theRes.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", theFile));
        theRes.WriteFile(theFile);
        theRes.End();

    }

    public void HidePatientLevelheader()
    {
        var pageHandler = HttpContext.Current.CurrentHandler;
        if (pageHandler is System.Web.UI.Page)
        {
            (((System.Web.UI.Page)pageHandler).Master.FindControl("levelTwoNavigationUserControl1").FindControl("patientLevelMenu") as Menu).Visible = false;
            (((System.Web.UI.Page)pageHandler).Master.FindControl("levelTwoNavigationUserControl1").FindControl("PharmacyDispensingMenu") as Menu).Visible = false;
            (((System.Web.UI.Page)pageHandler).Master.FindControl("levelTwoNavigationUserControl1").FindControl("UserControl_Alerts1") as UserControl).Visible = false;
            (((System.Web.UI.Page)pageHandler).Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;

            (((System.Web.UI.Page)pageHandler).Master.FindControl("level2Navigation") as Control).Visible = false;
            //(((System.Web.UI.Page)pageHandler).Master.FindControl("facilityBanner") as Control).Visible = true;
            //(((System.Web.UI.Page)pageHandler).Master.FindControl("patientBanner") as Control).Visible = false;
            //(((System.Web.UI.Page)pageHandler).Master.FindControl("username1") as Control).Visible = true;
            //(((System.Web.UI.Page)pageHandler).Master.FindControl("currentdate1") as Control).Visible = true;
            //(((System.Web.UI.Page)pageHandler).Master.FindControl("facilityName") as Control).Visible = true;
            //(((System.Web.UI.Page)pageHandler).Master.FindControl("imageFlipLevel2") as Control).Visible = false;
        }
    }
}
