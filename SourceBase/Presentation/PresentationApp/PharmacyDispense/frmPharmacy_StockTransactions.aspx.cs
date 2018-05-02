using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PresentationApp.PharmacyDispense
{
    public partial class frmPharmacy_StockTransactions : LogPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("pnlExtruder") as Panel).Visible = false;
            (Master.FindControl("level2Navigation") as Control).Visible = true;
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Stock Management";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("patientLevelMenu") as Menu).Visible = false;
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PharmacyDispensingMenu") as Menu).Visible = true;
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("UserControl_Alerts1") as UserControl).Visible = false;
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
            (Master.FindControl("facilityBanner") as Control).Visible = false;
            (Master.FindControl("patientBanner") as Control).Visible = false;
            (Master.FindControl("username1") as Control).Visible = false;
            (Master.FindControl("currentdate1") as Control).Visible = false;
            (Master.FindControl("facilityName") as Control).Visible = false;
            (Master.FindControl("imageFlipLevel2") as Control).Visible = false;
        }
    }
}