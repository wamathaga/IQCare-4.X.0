using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Web.SessionState;

namespace PresentationApp.PharmacyDispense
{
    public partial class frmPharmacy_PrintLabels :LogPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public static void PrintWebControl(Control ctrl)
        {
            StringWriter stringWrite = new StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            if (ctrl is WebControl)
            {
                Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
            }
            Page pg = new Page();
            pg.EnableEventValidation = false;

            HtmlForm frm = new HtmlForm();
            pg.Controls.Add(frm);
            frm.Attributes.Add("runat", "server");
            frm.Controls.Add(ctrl);
            pg.DesignerInitialize();
            pg.RenderControl(htmlWrite);
            string strHTML = stringWrite.ToString();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(strHTML);
            HttpContext.Current.Response.Write("<script>window.print();</script>");
            HttpContext.Current.Response.End();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //PrintWebControl(Panel1);
            
        }
    }
}