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

public partial class frmLogOff : LogPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        #region "Session Clear"

        Session["UserRight"] = null;
        Session["UserFeatures"] = null;
        Session["SystemId"] = 1;
            
        #endregion
        
        if (Request.QueryString["Touch"] != null)
        {
            if (Request.QueryString["Touch"].ToString() == "true")
                Response.Redirect("~/Touch/frmTouchLogin.aspx", true);
            else
                Response.Redirect("~/frmLogin.aspx", true);
        }
        else
            Response.Redirect("~/frmLogin.aspx", true);

    }

}