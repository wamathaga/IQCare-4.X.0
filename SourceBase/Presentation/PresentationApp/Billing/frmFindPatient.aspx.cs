using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;

namespace IQCare.Web.Billing
{
    public partial class frmFindPatient : LogPage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["FormName"] != null)
                {
                    this.HFormName.Value = Request.QueryString["FormName"];
                }
            }
        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.FindPatient.CancelFind += new EventHandler(FindPatient_CancelFind);
            this.FindPatient.NotifyParent += new CommandEventHandler(FindPatient_NotifyParent);
            this.FindPatient.SelectedPatientChanged += new CommandEventHandler(FindPatient_SelectedPatientChanged);
            this.FindPatient.PatientEnrollmentChanged += new CommandEventHandler(FindPatient_PatientEnrollmentChanged);
        }

        /// <summary>
        /// Handles the PatientEnrollmentChanged event of the FindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void FindPatient_PatientEnrollmentChanged(object sender, CommandEventArgs e)
        {
            List<KeyValuePair<string, int>> param = e.CommandArgument as List<KeyValuePair<string, int>>;
            this.PatientID = param.Find(l => l.Key == "PatientID").Value;
            this.LocationID = param.Find(l => l.Key == "LocationID").Value;
            this.ModuleID = param.Find(l => l.Key == "ModuleID").Value;
        }

        /// <summary>
        /// Handles the SelectedPatientChanged event of the FindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void FindPatient_SelectedPatientChanged(object sender, CommandEventArgs e)
        {
            List<KeyValuePair<string, int>> param = e.CommandArgument as List<KeyValuePair<string, int>>;
            this.PatientID = param.Find(l=>  l.Key=="PatientID").Value;
            this.LocationID = param.Find(l => l.Key == "LocationID").Value;

            if (this.LocationID == Convert.ToInt32(base.Session["AppLocationId"]))
            {
                this.Redirect();
            }
            else
            {
                string script = "NotifyMessage('This Patient belongs to a different Location. Please log-in with the patient\\'s location.'); return false;";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FindPatientAlert", script, true);
            }
        }

        /// <summary>
        /// Handles the NotifyParent event of the FindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void FindPatient_NotifyParent(object sender, CommandEventArgs e)
        {
            string commandName = e.CommandName;
            if (e.CommandArgument != null)
            {
                MsgBuilder theBuilder = (MsgBuilder)e.CommandArgument;
                IQCareMsgBox.Show(commandName, theBuilder, this);
            }
            else
            {
                IQCareMsgBox.Show(commandName, this);
            }
            return;
        }

        /// <summary>
        /// Handles the CancelFind event of the FindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void FindPatient_CancelFind(object sender, EventArgs e)
        {
            if (HFormName.Value == "") return;

            string formName = HFormName.Value;
            base.Session["PTServLines"]=null;
      
            if (formName == "FindAddBill")
            {

                Response.Redirect("~/Billing/frmBillingFindAddBill.aspx");
            }
            else if (formName == "Consumables")
            {
              
                Response.Redirect("~/frmFacilityHome.aspx");

            }
        }
        /// <summary>
        /// Gets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        protected int PatientID
        {
            get
            {
                return int.Parse(HPatientID.Value);
            }
            private set
            {
                base.Session["PatientId"] = value;
                HPatientID.Value = value.ToString();
            }

        }

        /// <summary>
        /// Gets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        protected int ModuleID
        {
            get
            {
                return int.Parse(HModuleID.Value);
            }
            private set
            {

                HModuleID.Value = value.ToString();
            }

        }
        /// <summary>
        /// Gets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        protected int LocationID
        {
            get
            {
                return int.Parse(HLocationID.Value);
            }
            private set
            {

                HLocationID.Value = value.ToString();
            }

        }
        /// <summary>
        /// Redirects this instance.
        /// </summary>
        void Redirect()
        {
            if (HFormName.Value == "") return;

            string formName = HFormName.Value;
            string theUrl = "";
            if (formName == "FindAddBill")
            {
                theUrl = string.Format("{0}?PatientId={1}", "./frmBilling_ClientBill.aspx", PatientID);
                Response.Redirect(theUrl);
            }
            else if (Request.QueryString["FormName"] != null && Request.QueryString["FormName"] == "Consumables")
            {
                theUrl = string.Format("{0}?PatientId={1}&mode=billing", "~/ClinicalForms/frmPatientConsumables.aspx", PatientID);
                Response.Redirect(theUrl);

            }
        }
    }
}