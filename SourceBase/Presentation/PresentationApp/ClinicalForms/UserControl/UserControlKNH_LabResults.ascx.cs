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
    public partial class UserControlKNH_LabResults : System.Web.UI.UserControl
    {
        DataSet theDS = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            IKNHStaticForms ExtVitals = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            theDS = ExtVitals.GetExtruderData(Convert.ToInt32(Session["PatientId"]));

            if (theDS.Tables[1].Rows.Count > 0)
            {
                lblHighestCD4.Text = theDS.Tables[1].Rows[0][0].ToString();
                lblHighestCD4Date.Text = theDS.Tables[1].Rows[0][1].ToString();
            }

            if (theDS.Tables[2].Rows.Count > 0)
            {
                lblLowestCD4.Text = theDS.Tables[2].Rows[0][0].ToString();
                lblLowestCD4Date.Text = theDS.Tables[2].Rows[0][1].ToString();
            }

            if (theDS.Tables[3].Rows.Count > 0)
            {
                grdCD4.DataSource = theDS.Tables[3];
                grdCD4.DataBind();
            }

            if (theDS.Tables[4].Rows.Count > 0)
            {
                lblHighestViralLoad.Text = theDS.Tables[4].Rows[0][0].ToString();
                lblHighestVLDate.Text = theDS.Tables[4].Rows[0][1].ToString();
            }

            if (theDS.Tables[5].Rows.Count > 0)
            {
                lblLowestViralLoad.Text = theDS.Tables[5].Rows[0][0].ToString();
                lblLowestVLDate.Text = theDS.Tables[5].Rows[0][1].ToString();
            }

            if (theDS.Tables[6].Rows.Count > 0)
            {
                grdViralLoad.DataSource = theDS.Tables[6];
                grdViralLoad.DataBind();
            }

            if (theDS.Tables[7].Rows.Count > 0)
            {
                grdLatestResults.DataSource = theDS.Tables[7];
                grdLatestResults.DataBind();
            }
        }
    }
}