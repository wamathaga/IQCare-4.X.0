using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Clinical;
using Application.Common;
using Application.Presentation;
using System.Data;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlKNH_WorkPlanExtruder : System.Web.UI.UserControl
    {
        DataSet WPDS = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            //IKNHStaticForms WorkPlan = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            //WPDS = WorkPlan.GetExtruderData(Convert.ToInt32(Session["PatientId"]));

            //if (WPDS.Tables[9].Rows.Count > 0)
            //{
            //    lblWorkPlan.Text = WPDS.Tables[9].Rows[0][0].ToString();
            //}

        }
    }
}