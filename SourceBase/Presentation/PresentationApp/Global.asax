<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.IO.Compression" %>
<%@ Import Namespace="Application.Logger" %>
<%@ Import Namespace="Application.Common" %>
<script RunAt="server">

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void Application_PreRequestHandlerExecute(object sender, EventArgs e)
    {

        HttpApplication app = sender as HttpApplication;
        string acceptEncoding = app.Request.Headers["Accept-Encoding"];
        Stream prevUncompressedStream = app.Response.Filter;

        if (!(app.Context.CurrentHandler is Page) ||
            app.Request["HTTP_X_MICROSOFTAJAX"] != null)
            return;

        if (acceptEncoding == null || acceptEncoding.Length == 0)
            return;

        acceptEncoding = acceptEncoding.ToLower();

        if (acceptEncoding.Contains("deflate") || acceptEncoding == "*")
        {
            // defalte
            app.Response.Filter = new DeflateStream(prevUncompressedStream,
                CompressionMode.Compress);
            app.Response.AppendHeader("Content-Encoding", "deflate");
        }
        else if (acceptEncoding.Contains("gzip"))
        {
            // gzip
            app.Response.Filter = new GZipStream(prevUncompressedStream,
                CompressionMode.Compress);
            app.Response.AppendHeader("Content-Encoding", "gzip");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Application_Error(object sender, EventArgs e)
    {

        Exception lastError = base.Server.GetLastError();
        if (lastError != null && lastError.Message != "File does not exist.")
        {
            Exception ex = lastError.GetBaseException();
            if (ex != null)
            {
                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
            }
        }
        base.Server.ClearError();
        base.Response.Clear();
        
        //Exception lastError = base.Server.GetLastError();
        //if (lastError != null)
        //{
        //    Exception baseException = lastError.GetBaseException();
        //    if (baseException != null)
        //    {
        //        lastError = baseException;
        //    }
        //}
        //EventLogger logger = new EventLogger();
        //logger.LogError(lastError);
        //try
        //{
        //    if (ConfigurationManager.AppSettings.Get("DEBUG").ToUpper() == "TRUE")
        //    {
        //        HttpContext.Current.Session["IQCARE_ERROR"] = lastError.Message + lastError.StackTrace;
        //    }
        //    else
        //    {
        //        HttpContext.Current.Session.Remove("IQCARE_ERROR");
        //    }
        //}
        //catch { }
        //base.Server.ClearError();
        //base.Response.Clear();
        ////handle endless loop ERR_TOO_MANY_REDIRECTS
        //if (!HttpContext.Current.Request.Path.EndsWith("Error.aspx", StringComparison.InvariantCultureIgnoreCase))
        //    base.Response.Redirect("~/Error.aspx");

    }
    
    
</script>
