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
    public partial class UserControl_VitalsExtruder : System.Web.UI.UserControl
    {
        DataSet theDS = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            //IKNHStaticForms ExtVitals = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            //theDS = ExtVitals.GetExtruderData(Convert.ToInt32(Session["PatientId"]));

            //if (theDS.Tables[0].Rows.Count > 0)
            //{
            //    lblSex.Text=theDS.Tables[0].Rows[0]["sex"].ToString();
            //    lblDOB.Text = theDS.Tables[0].Rows[0]["dob"].ToString();
            //    lblDistrict.Text = theDS.Tables[0].Rows[0]["districtname"].ToString();
            //    lblPhone.Text = theDS.Tables[0].Rows[0]["phone"].ToString();
            //    lblAge.Text=Session["patientageinyearmonth"].ToString();
            //}

            //if (theDS.Tables[8].Rows.Count > 0)
            //{
            //    lblBMI.Text = theDS.Tables[8].Rows[0]["BMI"].ToString();
            //}

        }
    }
}