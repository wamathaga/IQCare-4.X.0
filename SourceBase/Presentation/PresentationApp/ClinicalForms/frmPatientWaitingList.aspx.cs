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

namespace IQCare.Web.ClinicalForms
{

    public partial class frmPatientWaitingList : LogPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack) return;
            Session["dtWaitingList"] = null;
            Session["WLTechnicalArea"] = null;
            Session["WLTechnicalAreaName"] = null;
            Session["WLPatientID"] = 0;
            btnSubmit.Enabled = false;


            DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
            if (dtPatientInfo != null)
            {


                lblname.Text = String.Format("{0}, {1}", dtPatientInfo.Rows[0]["LastName"], dtPatientInfo.Rows[0]["FirstName"]);

                lblIQnumber.Text = dtPatientInfo.Rows[0]["IQNumber"].ToString();
                if (Request.QueryString["srvNm"] != null)
                {
                    lblTechnicalArea.Text = Request.QueryString["srvNm"];
                    Session["WLTechnicalArea"] = Request.QueryString["mod"];
                    Session["WLTechnicalAreaName"] = Request.QueryString["srvNm"];
                    Session["WLPatientID"] = Request.QueryString["PID"];
                }
                else
                {
                    lblTechnicalArea.Text = Session["TechnicalAreaName"].ToString();
                    Session["WLTechnicalArea"] = Session["TechnicalAreaId"];
                    Session["WLTechnicalAreaName"] = Session["TechnicalAreaName"];
                    Session["WLPatientID"] = HttpContext.Current.Session["PatientId"];
                }

                using (DataSet theDSXML = new DataSet())
                {
                    theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
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

                        BindManager.BindCombo(ddWList, theDT, "Name", "ID");
                        //BindUserDropdown(ddWList, string.Empty);
                        //BindUserDropdown(ddWList, Session["AppUserId"].ToString());
                        theDT.Clear();
                    }
                    theDV.Dispose();

                }

                loadPatientsWaitList(Convert.ToInt32(HttpContext.Current.Session["WLPatientID"]));
                PopulateUsersList();
            }






        }

        private void loadPatientsWaitList(int patientID)
        {
            IPatientHome PManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataTable theDt = PManager.GetPatientWaitList(patientID);

            grdWaitingList.DataSource = theDt;
            Session["dtWaitingList"] = theDt;
            grdWaitingList.DataBind();

        }
        protected void grdWaitingList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DataTable theDT = (DataTable)Session["dtWaitingList"];

            DataRow rowDelete = theDT.Rows[e.RowIndex];

            if (Convert.ToInt32(rowDelete["Persisted"]) == 1)
            {
                rowDelete["RowStatus"] = "1";
                rowDelete.AcceptChanges();

            }
            else
            {
                theDT.Rows.RemoveAt(e.RowIndex);
            }
            bindWaitingGrid();
            btnSubmit.Enabled = true;
        }

        void bindWaitingGrid()
        {
            DataTable theMainDT = (DataTable)Session["dtWaitingList"];
            DataView dv = theMainDT.DefaultView;
            dv.RowFilter = "RowStatus <> '1'";
            DataTable theDT = dv.ToTable();

            grdWaitingList.DataSource = theDT;
            grdWaitingList.DataBind();


        }


        protected void grdWaitingList_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void grdWaitingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            IPatientHome WListManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");

            WListManager.SavePatientWaitList((DataTable)Session["dtWaitingList"], Convert.ToInt32(Session["WLTechnicalArea"]), Convert.ToInt32(base.Session["AppUserId"]), Convert.ToInt32(HttpContext.Current.Session["WLPatientID"]));

            IQCareMsgBox.Show("Successfully saved ", "!", "", this);
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            DataRow[] foundRows;
            //validate waiting list item selected
            if (ddWList.SelectedItem.Text.Trim() == "Select")
            {
                //ddWList.BorderColor = System.Drawing.Color.Red;
                //ddWList.BackColor = System.Drawing.Color.Orange;
                IQCareMsgBox.Show("Select a waiting list item", "!", "", this);
                ddWList.Focus();
                return;
            }
            else
            {
                ddWList.BorderColor = System.Drawing.Color.Black;
                ddWList.BackColor = System.Drawing.Color.White;
                IQCareMsgBox.HideMessage(this);
            }




            DataTable theDT = new DataTable();
            theDT = (DataTable)Session["dtWaitingList"];
            foundRows = theDT.Select(String.Format("ListID='{0}' and ModuleID='{1}' and RowStatus=0", ddWList.SelectedItem.Value, Session["WLTechnicalArea"]));

            if (foundRows.Length < 1)
            {
                // a new list item is added since its not there


                DataRow theDR = theDT.NewRow();

                theDR["ListName"] = ddWList.SelectedItem.Text;
                theDR["ModuleName"] = Session["WLTechnicalAreaName"].ToString();
                theDR["ModuleID"] = int.Parse(Session["WLTechnicalArea"].ToString());
                theDR["AddedBy"] = Session["AppUserName"].ToString();
                theDR["ListID"] = ddWList.SelectedItem.Value;
                theDR["Priority"] = ddPriority.SelectedItem.Value;
                theDR["RowStatus"] = 0;
                theDR["Persisted"] = 0;
                theDR["WaitingFor"] = ddWaitingFor.SelectedItem.Value;
                theDR["WaitingForname"] = theDR["WaitingFor"].ToString() == "0" ? "" : ddWaitingFor.SelectedItem.Text;




                theDT.Rows.Add(theDR);

                Session["dtWaitingList"] = theDT;
                bindWaitingGrid();
                btnSubmit.Enabled = true;
                IQCareMsgBox.HideMessage(this);
            }
            else
            {
                IQCareMsgBox.Show("Patient is already in " + ddWList.SelectedItem.Text + " waiting list", "!", "", this);

                return;
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

            /*    DataRow theAdminRow = theUserDt.NewRow();
                theAdminRow.SetField("UserID", 1);
                theAdminRow.SetField("UserFirstName", "System");
                theAdminRow.SetField("UserLastName", "Admin");
                theAdminRow.SetField("Status", "Active");
                theAdminRow.SetField("Name", "System Admin");
                theUserDt.Rows.Add(theAdminRow);*/

            DataView dv = theUserDt.DefaultView;
            dv.RowFilter = "Status = 'Active'";
            dv.Sort = "Name Asc";
            dv.ToTable("Selected", true, "UserID", "Name");

            DataTable theDT = dv.ToTable("Selected", true, "UserID", "Name");

            BindFunctions bindFunctions = new BindFunctions();
            // bindFunctions.BindCombo(ddWaitingFor, theDT, "Name", "UserID");
            // ddWaitingFor.SelectedValue = base.Session["AppUserId"].ToString();
            //BindUserDropdown(ddWaitingFor, string.Empty);
            BindUserDropdown(ddWaitingFor, Session["AppUserId"].ToString());

        }

        protected void grdWaitingList_SelectedIndexChanged(object sender, EventArgs e)
        {

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
    }
}
