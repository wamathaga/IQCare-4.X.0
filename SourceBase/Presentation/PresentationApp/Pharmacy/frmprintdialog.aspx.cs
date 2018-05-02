using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Interface.Pharmacy;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace PresentationApp.Pharmacy
{
    public partial class frmprintdialog : LogPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx",true);
            }
            int PatientId = Convert.ToInt32(Request.QueryString["ptnpk"]);
            int VisitId = Convert.ToInt32(Request.QueryString["VisitId"]);

            IPediatric PrintManager;
            PrintManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
            DataSet theDSPrint = PrintManager.GetPharmacyDetailforLabelPrint(PatientId, VisitId);

            int i = 0;
            foreach (DataRow DR in theDSPrint.Tables[1].Rows)
            {
                System.Web.UI.UserControl uc = (System.Web.UI.UserControl)Page.LoadControl("usrctrlprintpharmacy.ascx");
                uc.ID = "" + i + "";
                Label lblFacility = ((Label)uc.FindControl("lblfacility"));
                lblFacility.Text = Convert.ToString(theDSPrint.Tables[0].Rows[0]["FacilityName"]);
                lblFacility.ID = "fac" + i + "";
                Label lblpName = ((Label)uc.FindControl("lblpName"));
                lblpName.Text = Convert.ToString(theDSPrint.Tables[0].Rows[0]["Name"]);
                lblpName.ID = "pname" + i + "";
                Label lbldrugName = ((Label)uc.FindControl("lbldrugName"));
                lbldrugName.Text = Convert.ToString(DR["DrugName"]);
                lbldrugName.ID = "dname" + i + "";
                Label lblunit = ((Label)uc.FindControl("lblunit"));
                lblunit.Text = Convert.ToString(DR["unitName"]);
                lblunit.ID = "uname" + i + "";
                TextBox txtprintInstruction = ((TextBox)uc.FindControl("txtprintInstruction"));
                txtprintInstruction.Text = Convert.ToString(DR["PatientInstructions"]);
                txtprintInstruction.ID = "PIns" + i + "";
                Button theButton = new Button();
                theButton.ID = "btn1" + i + "";
                theButton.Text = "Print Label";
                theButton.Click += new EventHandler(theButton_Click);
                Panel thepnl = new Panel();
                thepnl.ID = "pnl" + i + "";
                thepnl.BorderStyle = BorderStyle.Double;
                thepnl.BorderColor = System.Drawing.Color.Black;
                thepnl.Controls.Add(uc);
                thepnl.Controls.Add(theButton);
                thepnl.Controls.Add(new LiteralControl("<br /><br />"));
                pnlprintdialog.Controls.Add(thepnl);
                i++;
            }
            
        }

        void theButton_Click(object sender, EventArgs e)
        {
            string m = ((Button)sender).ID.Replace("btn1", "pnl");
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "fnprint('"+m+"');\n";
            script += "</script>\n";
            RegisterStartupScript("confirm", script);
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            string m = ((Button)sender).ID.Replace("btn1", "pnl");
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "doPrint();\n";
            script += "</script>\n";
            RegisterStartupScript("confirm", script);
        }

       
    }
}