using System;
using System.Data;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.SCM;

namespace IQCare.Web.Billing
{
    public partial class frmBillingFindAddBill : LogPage
    {
        //int LocationID;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Init_Page();
        }

        /// <summary>
        /// Init_s the page.
        /// </summary>
        private void Init_Page()
        {
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Billing";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            Session["PatientId"] = 0;
            Session["TechnicalAreaId"] = null;
            Session["TechnicalAreaName"] = null;

            divsearch.Visible = false;
            ViewState["Facility"] = null;
            // LocationID = Convert.ToInt32(Session["AppLocationId"]);

            /// loadOpenBills();
            /// {
            if (!IsPostBack)
            {
                this.PopulateData();
            }

        }
        /// <summary>
        /// Gets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        int LocationID
        {
            get
            {
                return Convert.ToInt32(Session["AppLocationId"]);
            }
        }
        ///// <summary>
        ///// Loads the open bills.
        ///// </summary>
        //private void LoadOpenBills()
        //{
        //    IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
        //    grdPatienBill.DataSource = null;

        //    DataSet theDS = BillingManager.GetOpenBills("", this.LocationID);


        //    grdPatienBill.DataSource = theDS.Tables[0];
        //    grdPatienBill.DataBind();
        //}

        /// <summary>
        /// Populates the data.
        /// </summary>
        void PopulateData()
        {
            IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
            // int? _patientID = null;
            DateTime? _dateFrom = null;
            DateTime? _dateTo = null;

            DataTable dt = BillingManager.GetPatientWithUnpaidItems(this.LocationID, _dateFrom, _dateTo);
            grdPatienBill.DataSource = dt;
            grdPatienBill.DataBind();

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the grdPatienBill control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void grdPatienBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            string theUrl = string.Empty;
            //DataTable theDT = (DataTable)grdPatienBill.DataSource;
            //  DataRow theDR = theDT.Rows[grdPatienBill.SelectedIndex];
            int patientID = int.Parse(grdPatienBill.SelectedDataKey.Values["PatientID"].ToString());

            // patientID = Int32.Parse(theDR.ItemArray[0].ToString());
            base.Session["PatientId"] = patientID;
            theUrl = "./frmBilling_ClientBill.aspx";

            Response.Redirect(theUrl, false);


        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the rbtlst_findBill control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void rbtlst_findBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtlst_findBill.SelectedValue == "Patient")
            {
                string theUrl;
                theUrl = string.Format("{0}?FormName={1}&mnuClicked={2}", "..//billing//frmFindPatient.aspx", "FindAddBill", "FindAddBill");
                //string.Format("{0}?PatientId={1}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString());
                Response.Redirect(theUrl, false);

            }

            else
            {
                //this.LoadOpenBills();
                this.PopulateData();
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the grdPatienBill control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdPatienBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdPatienBill, "Select$" + e.Row.RowIndex);
            }
        }


    }
}