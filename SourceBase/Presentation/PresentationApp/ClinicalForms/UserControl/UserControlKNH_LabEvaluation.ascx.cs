using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlKNH_LabEvaluation : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLab_Click(object sender, EventArgs e)
        {
            IQCareUtils.Redirect("../Laboratory/frm_Laboratory.aspx", "_blank", "toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes");
        }

    }
}