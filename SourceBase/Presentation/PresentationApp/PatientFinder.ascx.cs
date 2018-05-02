using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Clinical;
using Interface.Security;
using System.Collections;


namespace IQCare.Web
{
    /// <summary>
    /// Find patient
    ///     The parent must set EnableEventValidation="false"
    ///     Properties:
    ///         IncludeEnrollement : If True, retrieve the services areas where the patient is enrolled and also allow for searching by Service area
    ///         AutoLoadRecords : If true, retrieve patient records on page load.
    ///         FilterByServiceLines: Search by service lines. The list of service lines is displayed if true
    ///         SelectedServiceLine: The service line to use to determine patient status
    ///         FilterByStatus: Filter by Patient status
    ///         CanAddPatient: Whether to show add patient button
    ///         NumberOfRecords: Number of records to be returned. The maximum is set to [MaxNumberofRecords]=200
    ///         SendErrorToParent: propagate the error to the parent page
    /// Events:
    ///     SelectedPatientChanged: Occurs when a patient record is selected/clicked[ exposes the patientid and locationid
    ///     PatientEnrollmentChanged: Occurs when a patient enrollement record is selected/clicked[ exposes the patientid , locationid and moduleid
    ///     NotifyParent : Occurs when the control need to notify/send message to the parent. exposes message as text or as msgbuilder
    ///     CancelFind : Occurs when the cancel button is clicked
    ///         
    /// </summary>
    public partial class PatientFinder : System.Web.UI.UserControl
    {
        #region "Subscriber Properties"
        /// <summary>
        /// Gets or sets a value indicating whether [include enrollement].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include enrollement]; otherwise, <c>false</c>.
        /// </value>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Include the patient enrollement details")]
        [System.ComponentModel.Bindable(true)]
        public bool IncludeEnrollement
        {
            private get
            {
                if (this.HIncludeEnrollment.Value == "")
                    this.HIncludeEnrollment.Value = "FALSE";
                return bool.Parse(this.HIncludeEnrollment.Value);

            }
            set
            {
                this.HIncludeEnrollment.Value = value.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic load records].
        /// </summary>
        /// <value>
        /// <c>true</c> if [automatic load records]; otherwise, <c>false</c>.
        /// </value>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Autoload patient without waiting for Find command")]
        [System.ComponentModel.Bindable(true)]
        public bool AutoLoadRecords
        {
            private get
            {
                if (this.HAutoLoad.Value == "")
                    this.HAutoLoad.Value = "FALSE";
                return bool.Parse(this.HAutoLoad.Value);

            }
            set
            {
                this.HAutoLoad.Value = value.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [filter by service lines].
        /// </summary>
        /// <value>
        /// <c>true</c> if [filter by service lines]; otherwise, <c>false</c>.
        /// </value>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Whether to filter by service line")]
        [System.ComponentModel.Bindable(true)]
        public bool FilterByServiceLines
        {

            private get
            {
                if (this.HFilterByServiceLine.Value == "")
                    this.HFilterByServiceLine.Value = "FALSE";
                return bool.Parse(this.HFilterByServiceLine.Value);

            }
            set
            {
                this.HFilterByServiceLine.Value = value.ToString().ToUpper();
            }
        }
        /// <summary>
        /// Gets or sets the selected service line.
        /// </summary>
        /// <value>
        /// The selected service line.
        /// </value>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("The Selected Service Line")]
        [System.ComponentModel.Bindable(true)]
        public int SelectedServiceLine
        {

            private get
            {
                if (this.HSelectedServiceLine.Value == "")
                    this.HSelectedServiceLine.Value = "0";
                return int.Parse(this.HSelectedServiceLine.Value);

            }
            set
            {
                this.HSelectedServiceLine.Value = value.ToString().ToUpper();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [filter by status].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [filter by status]; otherwise, <c>false</c>.
        /// </value>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Whether to filter by patient status")]
        [System.ComponentModel.Bindable(true)]
        public bool FilterByStatus
        {

            private get
            {
                if (this.HFilterByStatus.Value == "")
                    this.HFilterByStatus.Value = "FALSE";
                return bool.Parse(this.HFilterByStatus.Value);

            }
            set
            {
                this.HFilterByStatus.Value = value.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Gets or sets the number of records.
        /// </summary>
        /// <value>
        /// The number of records.
        /// </value>
        [System.ComponentModel.DefaultValue(100)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Maximum records to be returned")]
        [System.ComponentModel.Bindable(true)]
        public int NumberOfRecords
        {

            private get
            {
                if (this.HMaxRecord.Value == "")
                    this.HMaxRecord.Value = "100";
                return int.Parse(this.HMaxRecord.Value);

            }
            set
            {
                if (value > this.MaxNumberOfRecords) value = this.MaxNumberOfRecords;
                this.HMaxRecord.Value = value.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can add patient.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can add patient; otherwise, <c>false</c>.
        /// </value>
        /// <summary>
        /// Gets or sets a value indicating whether [filter by service lines].
        /// </summary>
        /// <value>
        /// <c>true</c> if [filter by service lines]; otherwise, <c>false</c>.
        /// </value>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Whether to show enable adding patient")]
        [System.ComponentModel.Bindable(true)]
        public bool CanAddPatient
        {
            private get
            {
                if (this.HCanAdd.Value == "")
                    this.HCanAdd.Value = "FALSE";
                return bool.Parse(this.HCanAdd.Value);
            }
            set
            {
                HCanAdd.Value = value.ToString().ToUpper();
            }
        }
        /// <summary>
        /// Gets the maximum number of records. This is a readonly property
        /// </summary>
        /// <value>
        /// The maximum number of records.
        /// </value>
        public int MaxNumberOfRecords
        {

            get
            {
                return 200;

            }

        }
        /// <summary>
        /// Gets a value indicating whether [send error to parent]. This is a readonly property
        /// </summary>
        /// <value>
        ///   <c>true</c> if [send error to parent]; otherwise, <c>false</c>.
        /// </value>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Whether to notify the parent on error")]
        [System.ComponentModel.Bindable(true)]
        public bool SendErrorToParent
        {

            get
            {
                return true;

            }

        }

        #endregion

        #region Subscriber events
        /// <summary>
        /// Occurs when [selected patient changed].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when a patient record is selected.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler SelectedPatientChanged;
        /// <summary>
        /// Occurs when [patient enrollment changed].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when a patient enrollement record is selected.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler PatientEnrollmentChanged;

        /// <summary>
        /// Occurs when [notify parent].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when a notification message need to be sent to the parent.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler NotifyParent;

        /// <summary>
        /// Occurs when [cancel find].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when cancel button is clicked")]
        [System.ComponentModel.Bindable(true)]
        public event EventHandler CancelFind;
        #endregion

        #region "local properties

        /// <summary>
        /// Gets a value indicating whether this <see cref="FindPatient"/> is debug.
        /// </summary>
        /// <value>
        ///   <c>true</c> if debug; otherwise, <c>false</c>.
        /// </value>
        bool Debug
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings.Get("DEBUG").ToLower().Equals("true");
            }
        }
        /// <summary>
        /// The is error
        /// </summary>
        bool isError = false;

        /// <summary>
        /// Gets the show service.
        /// </summary>
        /// <value>
        /// The show service.
        /// </value>
        public string showService
        {
            get
            {
                if (!this.FilterByServiceLines) return "";
                if (!this.IncludeEnrollement) return "";
                return "";
            }
        }
        /// <summary>
        /// Gets the show status.
        /// </summary>
        /// <value>
        /// The show status.
        /// </value>
        public string showStatus
        {
            get
            {
                if (!this.FilterByStatus) return "none";

                return "";
            }
        }

        /// <summary>
        /// Gets the show add.
        /// </summary>
        /// <value>
        /// The show add.
        /// </value>
        public string showAdd
        {
            get
            {
                if (!this.CanAddPatient) return "none";

                return "";
            }
        }
        #endregion

        #region page methods
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["TechnicalAreaId"] == null) { Session["TechnicalAreaId"] = "0"; }
                this.BindIdentifierDropdown(Session["TechnicalAreaId"].ToString());
                this.PopulateFacilityList();
                PopulateServiceDropdown();
                //if (this.FilterByServiceLines)
                //{
                //    this.PopulateServiceDropdown();
                //}
                if (this.AutoLoadRecords)
                {
                    this.PopulatePatientList();
                }
                labelNote.Text = String.Format("Searched Result: Only a maximum of {0} records can be displayed.", this.NumberOfRecords);
            }
            string script = "$(\"#" + this.grdSearchResult.ClientID + "\").tablesorter(); ";
            ScriptManager.RegisterClientScriptBlock(this.grdSearchResult, this.GetType(), "GridSorter", script, true);
        }


        void BindIdentifierDropdown(string strServiceArea)
        {
            try
            {
                IPatientRegistration identifierManager;
                identifierManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                DataTable theDT = identifierManager.GetIdentifierListByServiceName(strServiceArea);
                DataRow theDR = theDT.NewRow();
                theDR["IdentifierName"] = "All";
                theDR["IdentifierID"] = 9999;
                theDT.Rows.InsertAt(theDR, 0);
                theDR = theDT.NewRow();
                theDR["IdentifierName"] = "IQNumber";
                theDR["IdentifierID"] = 10000;
                theDT.Rows.InsertAt(theDR, 1);
                BindFunctions theBindManger = new BindFunctions();
                theBindManger.BindCombo(ddlIdentifier, theDT, "IdentifierName", "IdentifierId");
                ddlIdentifier.Items.RemoveAt(0);
                ddlIdentifier.SelectedValue = "9999";
                ddFacility.SelectedValue = "0";
                txtidentificationno.Text = string.Empty;
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }





        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            divError.Visible = isError;

        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.TemplateControl.Error" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnError(EventArgs e)
        {
            Exception ex = Server.GetLastError();
            this.showErrorMessage(ref ex);
        }
        #endregion

        #region Subscriber events handlers

        /// <summary>
        /// Called when [patient selected].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnPatientSelected(object sender, CommandEventArgs e)
        {
            if (this.SelectedPatientChanged != null)
            {
                this.SelectedPatientChanged(sender, e);
            }
        }

        /// <summary>
        /// Called when [notify parent].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnNotifyParent(object sender, CommandEventArgs e)
        {
            if (this.NotifyParent != null)
            {
                this.NotifyParent(sender, e);
            }
        }
        /// <summary>
        /// Called when [patient selected].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnPatientEnrollmentSelected(object sender, CommandEventArgs e)
        {
            if (this.PatientEnrollmentChanged != null)
            {
                this.PatientEnrollmentChanged(sender, e);
            }
        }
        #endregion

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void showErrorMessage(ref Exception ex)
        {
            this.isError = true;

            if (this.Debug)
            {
                lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            }
            else
            {
                lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team";
                this.isError = this.divError.Visible = true;
                Exception lastError = ex;


                lastError.Data.Add("Domain", "Patient Find Control Level");
                //  Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                //  logger.LogError(ex);


            }



        }

        #region Data methods
        /// <summary>
        /// Populates the patient list.
        /// </summary>
        /// <returns></returns>
        void PopulatePatientList()
        {
            IPatientRegistration PatientManager;
            try
            {

                string lName;
                lName = txtlastname.Text.Trim();
                //  txtlastname.Text = lName.Replace("'", "''").ToString();
                string mName;
                mName = txtmiddlename.Text.Trim();

                string fName;
                fName = txtfirstname.Text.Trim();

                //HtmlInputControl txtDOB = (HtmlInputControl)this.txtDOB;

                string _strIdentification = txtidentificationno.Text.Trim();

                DateTime? dateRegistration = null;
                if (!string.IsNullOrEmpty(textRegistrationDate.Value))
                    dateRegistration = Convert.ToDateTime(textRegistrationDate.Value);
                DateTime? dateOfBirth = null;
                if (!string.IsNullOrEmpty(txtDOB.Value.Trim())) dateOfBirth = Convert.ToDateTime(txtDOB.Value.Trim());
                PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                DataTable dtPatient = PatientManager.GetPatientSearchResults(Convert.ToInt32(ddFacility.SelectedValue), lName, mName, fName, ddlIdentifier.SelectedValue,
                        _strIdentification, ddSex.SelectedValue,
                        ddCareEndedStatus.SelectedValue,
                        dateOfBirth,
                        dateRegistration,
                        FilterByServiceLines ? Convert.ToInt32(ddlServices.SelectedValue) : (this.SelectedServiceLine > 0) ? this.SelectedServiceLine : 999,
                        this.NumberOfRecords);
                this.grdSearchResult.DataSource = dtPatient;
                this.grdSearchResult.DataBind();
                if (dtPatient.Rows.Count == 0)
                {
                    // IQCareMsgBox.Show("NoPatientExists", this);
                    string errorMsg = IQCareMsgBox.GetMessage("NoPatientExists", this);
                    this.lblError.Text = errorMsg;
                    isError = true;
                    //return;
                    // MsgBuilder theBuilder = new MsgBuilder();
                    //theBuilder.DataElements["MessageText"] = errorMsg;

                    this.OnNotifyParent(this, new CommandEventArgs("NoPatientExists", null));
                    return;
                    //return null;
                }

                grdSearchResult.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();

                string errorMsg = IQCareMsgBox.GetMessage("MessageText", theBuilder, this);
                this.lblError.Text = errorMsg;
                isError = true;
                //this.OnNotifyParent(this, new CommandEventArgs("#C1", theBuilder));
                // return null;
            }
            finally
            {
                PatientManager = null;
            }
        }



        /// <summary>
        /// Binds the service dropdown.
        /// </summary>
        void PopulateServiceDropdown()
        {
            BindFunctions BindManager = new BindFunctions();
            IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet DSModules = ptnMgr.GetModuleNames(Convert.ToInt32(Session["AppLocationId"]));

            DataTable theDT = new DataTable();
            theDT = DSModules.Tables[0];
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "ModuleId NOT IN(206,207,7)";
            IQCareUtils theModUtils = new IQCareUtils();
            DataTable theNewDT = new DataTable();
            theNewDT = theModUtils.CreateTableFromDataView(theDV);
            if (theNewDT.Rows.Count > 0)
            {
                BindManager.BindCombo(ddlServices, theNewDT, "ModuleName", "ModuleID");
                ptnMgr = null;
            }
        }

        /// <summary>
        /// Populates the facility list.
        /// </summary>
        void PopulateFacilityList()
        {
            try
            {
                IUser theLocationManager;
                theLocationManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
                DataTable theDT = theLocationManager.GetFacilityList();
                DataRow theDR = theDT.NewRow();
                theDR["FacilityName"] = "All";
                theDR["FacilityId"] = 9999;
                theDT.Rows.InsertAt(theDR, 0);
                BindFunctions theBindManger = new BindFunctions();
                theBindManger.BindCombo(ddFacility, theDT, "FacilityName", "FacilityId");
                ddFacility.SelectedValue = Convert.ToString(Session["AppLocationId"]);
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Gets the patient enrollement.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetPatientEnrollement(int patientID, int locationID)
        {
            DataTable dt = new DataTable();
            IPatientRegistration PatientManager;
            PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            dt = PatientManager.GetPatientServiceLines(patientID, locationID);

            base.Session["PTServLines"] = dt;
            string[] columnNames = { "PatientID", "LocationID", "ModuleID", "ModuleName", "EnrollmentDate", "CareStatus" };

            DataTable dv = dt.DefaultView.ToTable(true, columnNames);
            return dv;

        }
        /// <summary>
        /// Gets the service line identifiers.
        /// </summary>
        /// <param name="moduleID">The module identifier.</param>
        /// <returns></returns>
        DataTable GetServiceLineIdentifiers(int moduleID)
        {
            // DataTable dt = new DataTable();
            if (base.Session["PTServLines"] != null)
            {
                DataTable dt = (DataTable)base.Session["PTServLines"];

                //DataView dv = new DataView(dt);
                dt.DefaultView.RowFilter = string.Format(" ModuleID = {0}", moduleID);

                //dt.AsEnumerable().Where(row => Convert.ToInt32(row["moduleid"]) == moduleID);

                return dt.DefaultView.ToTable();
            }
            else return null;
        }

        #endregion /// <summary>

        #region grid events
        /// Handles the RowCommand event of the grdSearchResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void grdSearchResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string commandName = e.CommandName;
            int rowIndex, patientId, locationId;
            if (!this.IncludeEnrollement && commandName == "Expand")
            {
                commandName = "PatientClick";
            }
            if (commandName == "Expand")
            {
                rowIndex = Int32.Parse(e.CommandArgument.ToString());
                grdSearchResult.SelectedIndex = rowIndex;
                patientId = int.Parse(grdSearchResult.SelectedDataKey.Values["patientid"].ToString());
                locationId = int.Parse(grdSearchResult.SelectedDataKey.Values["locationid"].ToString());

                // GridViewRow selectedRow = grdSearchResult.SelectedRow;
                GridViewRow row = (grdSearchResult.Rows[rowIndex]);

                GridView nestedGrid = row.FindControl("gridPatientServiceList") as GridView;

                Panel containerDiv = row.FindControl("ContainerDiv") as Panel;

                ImageButton expandButton = row.FindControl("ExpandGridButton") as ImageButton;
                if (expandButton != null && expandButton.ImageUrl.IndexOf("minus") != -1)
                {
                    expandButton.ImageUrl = expandButton.ImageUrl.Replace("minus", "plus");

                    resultOpenITem.Value = "";

                    containerDiv.Style.Add(HtmlTextWriterStyle.Display, "block");
                }
                else if (expandButton != null && expandButton.ImageUrl.IndexOf("plus") != -1)
                {
                    expandButton.ImageUrl = expandButton.ImageUrl.Replace("plus", "minus");
                    DataTable dt = this.GetPatientEnrollement(patientId, locationId);
                    nestedGrid.DataSource = dt;
                    nestedGrid.DataBind();
                    containerDiv.Style.Add(HtmlTextWriterStyle.Display, "inline");
                    resultOpenITem.Value = containerDiv.ClientID;
                }

                return;

            }
            if (commandName == "PatientClick")
            {
                rowIndex = Int32.Parse(e.CommandArgument.ToString());
                grdSearchResult.SelectedIndex = rowIndex;
                patientId = int.Parse(grdSearchResult.SelectedDataKey.Values["patientid"].ToString());
                locationId = int.Parse(grdSearchResult.SelectedDataKey.Values["locationid"].ToString());
                var list = new List<KeyValuePair<string, int>>();
                list.Add(new KeyValuePair<string, int>("PatientID", patientId));
                list.Add(new KeyValuePair<string, int>("LocationID", locationId));
                this.OnPatientSelected(this, new CommandEventArgs("SelectedPatient", list));
                this.PopulatePatientList();
                return;
            }
        }

        /// <summary>
        /// Handles the RowCreated event of the grdSearchResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdSearchResult_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView enrollmentGrid = e.Row.FindControl("gridPatientServiceList") as GridView;
                enrollmentGrid.RowCommand += new GridViewCommandEventHandler(enrollmentGrid_RowCommand);
                enrollmentGrid.RowDataBound += new GridViewRowEventHandler(enrollmentGrid_RowDataBound);
                if (!this.IncludeEnrollement)
                {
                    ImageButton btn = e.Row.Cells[0].FindControl("ExpandGridButton") as ImageButton;
                    if (btn != null)
                    {
                        btn.ImageUrl = btn.ImageUrl.Replace("plus.png", "arrow.gif");
                        btn.CommandName = "PatientClick";
                    }
                }

                e.Row.Cells[0].Style.Add("display", (this.IncludeEnrollement) ? "" : "block");
                e.Row.Cells[9].Style.Add("display", (this.FilterByServiceLines || this.SelectedServiceLine > 0) ? "inline" : "none");
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {

                e.Row.Cells[0].Style.Add("display", (this.IncludeEnrollement) ? "" : "block");
                e.Row.Cells[9].Style.Add("display", (this.FilterByServiceLines || this.SelectedServiceLine > 0) ? "inline" : "none");
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the enrollmentGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void enrollmentGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                int moduleId = Convert.ToInt32(row["moduleid"]);
                Repeater repeater = e.Row.FindControl("repeaterIdentifiers") as Repeater;
                if (repeater != null)
                {
                    repeater.DataSource = this.GetServiceLineIdentifiers(moduleId);
                    repeater.DataBind();
                }
                GridView senderGrid = (GridView)e.Row.NamingContainer;
                GridView mySender = (GridView)sender;
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(senderGrid, "EnrollmentClick$" + e.Row.RowIndex);
            }
        }

        /// <summary>
        /// Handles the RowCommand event of the enrollmentGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void enrollmentGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string commandName = e.CommandName;
            int rowIndex, patientId, locationId, moduleId;
            if (commandName == "EnrollmentClick")
            {
                // GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                // GridView thisGrid = (GridView)gvr.NamingContainer;
                GridView thisGrid = (GridView)sender;
                rowIndex = Int32.Parse(e.CommandArgument.ToString());


                //GridViewRow row = (transactionGrid.Rows[index]);
                thisGrid.SelectedIndex = rowIndex;
                patientId = int.Parse(thisGrid.SelectedDataKey.Values["patientid"].ToString());
                locationId = int.Parse(thisGrid.SelectedDataKey.Values["locationid"].ToString());
                moduleId = int.Parse(thisGrid.SelectedDataKey.Values["moduleid"].ToString());
                var list = new List<KeyValuePair<string, int>>();
                list.Add(new KeyValuePair<string, int>("PatientID", patientId));
                list.Add(new KeyValuePair<string, int>("LocationID", locationId));
                list.Add(new KeyValuePair<string, int>("ModuleID", moduleId));
                this.OnPatientEnrollmentSelected(this, new CommandEventArgs("SelectedEnrollment", list));
                return;
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the grdSearchResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int x = e.Row.Cells.Count;
                if (this.IncludeEnrollement)
                {

                    e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdSearchResult, "Expand$" + e.Row.RowIndex);
                    for (int i = 1; i < x; i++)
                    {
                        e.Row.Cells[i].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdSearchResult, "PatientClick$" + e.Row.RowIndex);

                    }
                }
                else
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdSearchResult, "PatientClick$" + e.Row.RowIndex);
                }
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }

        #endregion
        /// <summary>
        /// Handles the Click event of the btnView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            if (Session["TechnicalAreaId"] != null)
            {
                if (Session["TechnicalAreaId"].ToString() == "206")
                {
                    grdSearchResult.Columns[9].Visible = false;
                }
            }
            this.PopulatePatientList();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.CancelFind != null)
            {
                base.Session["PTServLines"] = null;
                this.CancelFind(sender, e);


            }
            else
                Response.Redirect("~/frmFacilityHome.aspx");
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Hashtable theHT = EnrollParams();
            Session.Add("EnrollParams", theHT);
            string url;
            base.Session["PTServLines"] = null;
            url = string.Format("{0}&sts={1}", "~/frmPatientCustomRegistration.aspx?name=Add", 0);
            Response.Redirect(url);
        }
        /// <summary>
        /// Enrolls the parameters.
        /// </summary>
        /// <returns></returns>
        private Hashtable EnrollParams()
        {
            Hashtable theHT = new Hashtable();
            theHT.Add("FirstName", txtfirstname.Text);
            theHT.Add("LastName", txtlastname.Text);
            theHT.Add("MiddleName", txtmiddlename.Text);
            theHT.Add("EnrollmentNo", txtidentificationno.Text);
            //theHT.Add("EnrollmentNo", txtpatientenrolno.Text);
            //theHT.Add("ClinicNo", txtHospclinicno.Text);
            theHT.Add("Date of Birth", txtDOB.Value);
            theHT.Add("Sex", ddSex.SelectedValue);
            return theHT;
        }
    }
}