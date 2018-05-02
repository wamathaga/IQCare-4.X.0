using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.Laboratory;
using System.Data;

namespace PresentationApp.Laboratory
{
    public partial class frmLaboratoryHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx", true);
            }

            ILabFunctions labManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            DataSet theDS = labManager.GetLabHistory(Convert.ToInt32(Session["PatientID"]), Convert.ToInt32(Session["AppLocationId"]));
            ViewState["LabHistory"] = theDS;

            DataView view = new DataView(theDS.Tables[0]);
            DataTable distinctValues = view.ToTable(true, "LabID", "LabNumber");

            gvLabHistory.DataSource = distinctValues;
            gvLabHistory.DataBind();
        }


        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string labID = gvLabHistory.DataKeys[e.Row.RowIndex].Value.ToString();

                GridView gvLabResult = e.Row.FindControl("gvLabResult") as GridView;

                DataSet ds = (DataSet)ViewState["LabHistory"];

                string filterExp = "LabID = " + labID;
                DataView theDV = new DataView(ds.Tables[0]);
                IQCareUtils theUtils = new IQCareUtils();
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                theDV.RowFilter = "LabID IN(" + labID + ")";
                if (theDV.Count > 0)
                    theDT = theUtils.CreateTableFromDataView(theDV);

                gvLabResult.DataSource = theDT;
                gvLabResult.DataBind();
            }
        }

    }
}