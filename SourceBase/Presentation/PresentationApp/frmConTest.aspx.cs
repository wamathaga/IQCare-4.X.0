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
using System.Diagnostics;
using System.IO;
using System.Net;
//using Interface.Clinical;

public partial class frmConTest : LogPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //IQCareMsgBox.ShowConfirm("UserSaveRecord", Button1);
        txtCheck.Attributes.Add("onkeypress", "return CheckInterger()");
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
   

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        
    }

    protected void Button3_Click(object sender, EventArgs e)
    {

        //string sIQToolsPath = @"C:\Program Files (x86)\Futures Group International\IQTools V3\IQTools.exe";
        string sIQToolsPath = @"C:\Projects\IQToolsV3\IQTools\IQTools\bin\Debug\IQTools.exe";

        if (!File.Exists(sIQToolsPath))
        {
            ClientScript.RegisterStartupScript(typeof(Page),"FileNotFoundError",@"<script type='text/javascript'>alert('IQTools is not installed, please download and install.');</script>");
        }
        else
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(sIQToolsPath);
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            Process pross = new Process();
            pross.StartInfo.FileName = sIQToolsPath;
            pross.StartInfo.Arguments = "IQTools Temp ATKX/HfEp22Te54tVQK4ArcTzCSSRbjiEHkm8upvgqEKQ7PUT4kHayyDGk0q5t99NvCDRgHE0IZrhZN4a4JR6QCAEHSKngDnWnpaZklQlos=";
            pross.Start();

        }
    }

}
