using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Clinical;
using Application.Presentation;
using System.Data;
using Interface.Administration;

namespace IQCare.Web
{
    public partial class frmWaitingList : LogPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //Bind the userName drop down with all active users
            //DataTable theUserDt;
            //Iuser UManager = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser, BusinessProcess.Administration");
            //theUserDt = (UManager.GetUserList()).Tables[0];


            ddWaitingFor.Visible = false;
            lblWaitingfor.Visible = false;

            using (DataSet theDSXML = new DataSet())
            {
                theDSXML.ReadXml(MapPath(".\\XMLFiles\\AllMasters.con"));
                IQCareUtils theUtils = new IQCareUtils();
                BindFunctions BindManager = new BindFunctions();
                DataView theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=214";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    DataRow dr = theDT.NewRow();
                    dr["Name"] = "Consultation";
                    dr["ID"] = 1;
                    theDT.Rows.Add(dr);

                    dr = theDT.NewRow();
                    dr["Name"] = "Laboratory";
                    dr["ID"] = 3;
                    theDT.Rows.Add(dr);

                    dr = theDT.NewRow();
                    dr["Name"] = "Pharmacy";
                    dr["ID"] = 4;
                    theDT.Rows.Add(dr);

                    dr = theDT.NewRow();
                    dr["Name"] = "Triage";
                    dr["ID"] = 5;
                    theDT.Rows.Add(dr);

                    BindManager.BindCombo(ddwaitingList, theDT, "Name", "ID");
                    //BindUserDropdown(ddWList, string.Empty);
                    //BindUserDropdown(ddWList, Session["AppUserId"].ToString());
                    theDT.Clear();
                }
                theDV.Dispose();
            }


        }

        private void BindUserDropdown(DropDownList DropDownID, String userId)
        {
            Dictionary<int, string> userList = new Dictionary<int, string>();
            CustomFieldClinical.BindUserDropDown(DropDownID, out userList);
            if (!string.IsNullOrEmpty(userId))
            {
                if (userList.ContainsKey(Convert.ToInt32(userId)))
                {
                    DropDownID.SelectedValue = userId;
                    //SecurityPerTabSignature = userId;
                }
            }
        }
        /// <summary>
        /// Populates the users list.
        /// </summary>
        void PopulateUsersList()
        {
            //  if (ddWaitingFor.Items.Count > 0) return;
            DataTable theUserDt;
            Iuser UManager = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser, BusinessProcess.Administration");
            theUserDt = (UManager.GetUserList()).Tables[0];
            DataColumn newColumn;
            newColumn = new DataColumn("Name");
            newColumn.Expression = " UserFirstName +' '+ UserLastName";
            theUserDt.Columns.Add(newColumn);
            //Add system admin who was removed at query level

            DataRow theAdminRow = theUserDt.NewRow();
            theAdminRow.SetField("UserID", 1);
            theAdminRow.SetField("UserFirstName", "System");
            theAdminRow.SetField("UserLastName", "Admin");
            theAdminRow.SetField("Status", "Active");
            theAdminRow.SetField("Name", "System Admin");
            theUserDt.Rows.Add(theAdminRow);

            DataView dv = theUserDt.DefaultView;
            dv.RowFilter = "Status = 'Active'";
            dv.Sort = "Name Asc";
            dv.ToTable("Selected", true, "UserID", "Name");

            DataTable theDT = dv.ToTable("Selected", true, "UserID", "Name");

            BindFunctions bindFunctions = new BindFunctions();
            //bindFunctions.BindCombo(ddWaitingFor, theDT, "Name", "UserID");
            //BindUserDropdown(ddWaitingFor, string.Empty);
            BindUserDropdown(ddWaitingFor, Session["AppUserId"].ToString());

            ddWaitingFor.Visible = true;
            lblWaitingfor.Visible = true;
            //    ddWaitingFor.SelectedValue = base.Session["AppUserId"].ToString();
        }
        private void loadWaitList()
        {
            IPatientRegistration PManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            System.Data.DataTable theDt = PManager.GetPatientsOnWaitingList(Convert.ToInt32(ddwaitingList.SelectedItem.Value), Convert.ToInt32(Session["TechnicalAreaId"]));
            Session["WaitlistPatients"] = theDt;


            grdWaitingList.DataSource = Session["WaitlistPatients"];

            grdWaitingList.DataBind();

        }

        protected void grdWaitingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["TechnicalAreaId"]) == 0) return;//do nothing if we are in records
            string theUrl = string.Empty;
            int patientID = int.Parse(grdWaitingList.SelectedDataKey.Values["Ptn_PK"].ToString());
            HttpContext.Current.Session["PatientId"] = patientID;
            HttpContext.Current.Session["PatientVisitId"] = 0;
            HttpContext.Current.Session["ServiceLocationId"] = 0;
            HttpContext.Current.Session["LabId"] = 0;
            int WaitingListID = int.Parse(grdWaitingList.SelectedDataKey.Values["WaitingListID"].ToString());

            //remove patient from the waiting list
            IPatientRegistration PManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            PManager.ChangeWaitingListStatus(WaitingListID, 1, Convert.ToInt32(base.Session["AppUserId"]));


            // Added for bug ID 1062
            if (ddwaitingList.SelectedItem.Text == "Laboratory")
            {
                String theOrdScript;
                theOrdScript = "<script language='javascript' id='openPatient'>\n";
                theOrdScript += "window.opener.location.href = './Laboratory/frm_LabTestResults.aspx';\n";
                theOrdScript += "window.close();\n";
                theOrdScript += "</script>\n";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closePage", theOrdScript);
            }
            else// (ddwaitingList.SelectedItem.Text == "Pharmacy")
            {
                String theOrdScript;
                theOrdScript = "<script language='javascript' id='openPatient'>\n";
                //theOrdScript += "window.opener.location.href = './ClinicalForms/frmPatient_Home.aspx';\n"; Bug ID 1062
                theOrdScript += "window.opener.location.href = './ClinicalForms/frmPatient_History.aspx';\n";
                theOrdScript += "window.close();\n";
                theOrdScript += "</script>\n";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closePage", theOrdScript);
            }

            // End addition.
            //  theUrl = "./ClinicalForms/frmPatient_Home.aspx";
            //  Response.Redirect(theUrl, false);
        }

        protected void grdWaitingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdWaitingList, "Select$" + e.Row.RowIndex);
            }

        }

        protected void ddwaitingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddWaitingFor.Visible = false;
            lblWaitingfor.Visible = false;
            if (ddwaitingList.SelectedItem.Value == "0")
            {
                grdWaitingList.DataSource = null;
                grdWaitingList.DataBind();

            }
            else
            {
                loadWaitList();
                this.PopulateUsersList();
            }
        }

        protected void ddWaitingFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["WaitlistPatients"];
            DataView dv = dt.DefaultView;
            if (ddWaitingFor.SelectedValue != "0")
            {
                dv.RowFilter = String.Format("WaitingFor='{0}'", ddWaitingFor.SelectedItem.Text);
                grdWaitingList.DataSource = dv;
                grdWaitingList.DataBind();
            }
            else
                loadWaitList();


        }

    }
}