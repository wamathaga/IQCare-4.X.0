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
    public partial class UserControl_ARVHistoryExtruder : System.Web.UI.UserControl
    {
        DataSet theDS = new DataSet();
        DataSet ARVHistoryDS = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            //IKNHStaticForms ARVHistory = (IKNHStaticForms)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BKNHStaticForms, BusinessProcess.Clinical");
            //theDS = ARVHistory.GetLastRegimenDispensed(Convert.ToInt32(Session["PatientId"]));

            //if (theDS.Tables[0].Rows.Count > 0)
            //{
            //    lblLastRegimen.Text = theDS.Tables[1].Rows[0][0].ToString();
            //}

            //ARVHistoryDS = ARVHistory.GetPatientDrugHistory(Convert.ToInt32(Session["PatientId"]));

            //BindGridARV(ARVHistoryDS.Tables[0]);
            
            
        }

        private void BindGridARV(DataTable theDT)
        {
            grdARVHistory.Columns.Clear();

            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Drug";
            theCol0.DataField = "Drug";
            theCol0.ItemStyle.CssClass = "textstyle";

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Date";
            theCol1.DataField = "Date";
            theCol1.ItemStyle.Width = 80;
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.DataFormatString = "{0:dd-MMM-yyyy}";


            grdARVHistory.Columns.Add(theCol0);

            grdARVHistory.Columns.Add(theCol1);
            
            grdARVHistory.DataSource = theDT;
            grdARVHistory.DataBind();

        }
    }
}