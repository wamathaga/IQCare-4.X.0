#region Usings
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Telerik.Web.UI;
using Application.Presentation;
using Interface.Administration;
using BusinessProcess.Administration;
using System.Data;
#endregion

namespace Touch
{
    /// <summary>
    /// Handles the session per page
    /// </summary>
    public class TouchPageBase : System.Web.UI.Page
    {
        #region Local Variables
        private static string strPathAndQuery;
        private static string strUrl;
        public const int SessionLengthMinutes = 60;
        public string SessionExpireDestinationUrl = strUrl + "~/Touch/frmTouchLogin.aspx";
        #endregion

        #region Global Touch Page Events
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            IErrorLogging ErrManager = (IErrorLogging)ObjectFactory.CreateInstance("BusinessProcess.Administration.BErrorLogging, BusinessProcess.Administration");
            //ErrManager.LogError("TouchBasePage.cs", "The session count is " + Session.Count.ToString(), ErrorType.Error);


            if (Session["AppLocation"] == null)
            {
                //IErrorLogging ErrManager = (IErrorLogging)ObjectFactory.CreateInstance("BusinessProcess.Administration.BErrorLogging, BusinessProcess.Administration");
                ErrManager.LogError("TouchBasePage.cs", "Session['AppLocation'] has expired", ErrorType.Error);
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmLogOff.aspx?Touch=true");
            }
            string url = Request.RawUrl.ToString();
            Application["PrvFrm"] = url;
            System.IO.FileInfo fileinfo = new System.IO.FileInfo(Request.Url.AbsolutePath);
            string pageName = fileinfo.Name;

            if (Session.Count == 0)
            {
                //IErrorLogging ErrManager = (IErrorLogging)ObjectFactory.CreateInstance("BusinessProcess.Administration.BErrorLogging, BusinessProcess.Administration");
                ErrManager.LogError("TouchBasePage.cs", "Session.Count == 0", ErrorType.Error);
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmLogOff.aspx?Touch=true");
            }
            if (Session["AppUserID"].ToString() == "")
            {
                //IErrorLogging ErrManager = (IErrorLogging)ObjectFactory.CreateInstance("BusinessProcess.Administration.BErrorLogging, BusinessProcess.Administration");
                ErrManager.LogError("TouchBasePage.cs", "Session['AppUserID'].ToString() is empty", ErrorType.Error);
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmLogOff.aspx?Touch=true");
            }
            BUser ptrnLockUser = (BUser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser, BusinessProcess.Administration");
            DataSet record = ptrnLockUser.GetUserLock(Convert.ToInt32(Session["AppUserId"].ToString()));
            if (!string.IsNullOrEmpty(record.Tables[0].Rows[0].Field<string>("ptrnLock_code")))
            {
                HttpCookie flagcookie = new HttpCookie("ptrnLkd", "ptrnU_" + Session["AppUserId"].ToString());
                flagcookie.Expires = DateTime.Now.Add(new TimeSpan(0, 20, 0));
                Response.Cookies.Add(flagcookie);

                ptrnLockUser.SaveUserLock(record.Tables[0].Rows[0].Field<int>("UserID"), record.Tables[0].Rows[0].Field<int>("ptrnLock_locationID"), record.Tables[0].Rows[0].Field<string>("ptrnLock_code"), url);
            }


        }

        #endregion

    }
}
