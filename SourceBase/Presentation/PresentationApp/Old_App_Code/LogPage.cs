using System;
using System.IO;
using System.IO.Compression;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using System.Threading;
using System.Globalization;
using System.Text;
using System.Web;
using Application.Common;
using Application.Presentation;


public abstract class LogPage : System.Web.UI.Page
{
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
        if (Session["PatientId"] == null || Convert.ToInt32(Session["PatientId"]) != 0)
        {
            //IQCareMsgBox.NotifyAction("Application has an issue, Please contact Administrator!", "Application Error", false, this, "window.location.href='../frmLogin.aspx'");
            //Response.Write("<script>alert('Application has an issue, Please contact Administrator!') ; window.location.href='../frmFindAddCustom.aspx?srvNm=" + Session["TechnicalAreaName"] + "&mod=0'</script>");
            Response.Write("<script>alert('Application has an issue, Please contact Administrator!') ; window.location.href='../frmFacilityHome.aspx'</script>");
        }
        else
        {
            if (Session["TechnicalAreaId"] != null || Convert.ToInt16(Session["TechnicalAreaId"]) != 0)
            {
                //IQCareMsgBox.NotifyAction("Application has an issue, Please contact Administrator!", "Application Error", false, this, "window.location.href='../frmFacilityHome.aspx';");
                Response.Write("<script>alert('Application has an issue, Please contact Administrator!') ; window.location.href='../frmFacilityHome.aspx'</script>");

            }
            else
            {

                //IQCareMsgBox.NotifyAction("Application has an issue, Please contact Administrator!", "Application Error", false, this, "window.location.href='../frmLogin.aspx';");
                Response.Write("<script>alert('Application has an issue, Please contact Administrator!') ; window.location.href='../frmLogin.aspx'</script>");
            }
        }
        Server.ClearError();



        
    }
}
