using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Interface.Administration;
using Application.Presentation;
using Application.Common;

namespace PresentationApp.AdminForms
{
    /// <summary>
    /// Manage Loab group items. 
    /// </summary>
    public partial class frmAdminLaboratoryGroupItems : LogPage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            {
                IQCareMsgBox.Show("SessionExpired", this);
                Response.Redirect("~/frmlogin.aspx",true);
            }
            if (IsPostBack) return;
            FillDropDowns();
            BindGrid();
            lblH2.Text = Request.QueryString["Name"];
        }
        protected void FillDropDowns()
        {
            ILabMst LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
            DataSet theDS = LabManager.GetDropDowns();
            BindFunctions BindManager = new BindFunctions();
            BindManager.BindCombo(ddDepartment, theDS.Tables[0], "LabDepartmentName", "LabDepartmentID");
        }
        /// <summary>
        /// Binds the grid.
        /// </summary>
        private void BindGrid()
        {
            int LabId = 0;
            if (Request.QueryString["LabId"] != "")
            {
                LabId = int.Parse(Request.QueryString["LabId"]);
            }
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
            //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Laboratory Group Items";
            ILabMst LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
            DataSet theMainDt = LabManager.GetLabGroupTests(LabId);
            DataView theDV = new DataView(theMainDt.Tables[0]);
            theDV.RowFilter = "DataType <>'Group' OR DataType IS NULL ";
            DataTable theDt = theDV.ToTable();
            grdLabs.DataSource = theDt;
            grdLabs.DataBind();
            if (LabId > 0 && theMainDt.Tables[1].Rows.Count>0)
            {
                txtLabName.Text = theMainDt.Tables[1].Rows[0]["LabName"].ToString();
                ddDepartment.SelectedValue = theMainDt.Tables[1].Rows[0]["LabDepartmentID"].ToString();
                ddStatus.SelectedValue = theMainDt.Tables[1].Rows[0]["DeleteFlag"].ToString();
                txtLabName.Enabled = false;
                ddDepartment.Enabled = false;
            }

        }
        private Boolean FieldValidation()
        {
            //Validate fields input values
            if (txtLabName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Laboratory Test";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtLabName.Focus();
                return false;
            }
            if (ddDepartment.SelectedValue == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Laboratory Department";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                ddDepartment.Focus();
                return false;
            }  

            return true;
        }
        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (FieldValidation())
            {
                using (DataTable theDT = new DataTable())
                {
                    theDT.Columns.AddRange(new DataColumn[2] { new DataColumn("LabgroupID", typeof(int)), new DataColumn("LabTestID", typeof(int)) });
                    foreach (GridViewRow row in grdLabs.Rows)
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[1].FindControl("chkLabTest") as CheckBox);
                            if (chkRow.Checked)
                            {
                                string LabID = (row.Cells[0].FindControl("lblLabTestID") as Label).Text;
                                string LabGroupID = Request.QueryString["LabId"];
                                theDT.Rows.Add(LabGroupID, LabID);
                            }
                        }
                    ILabMst LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
                    LabManager.SaveLabGroupItems(Convert.ToInt32(Session["AppUserId"]), theDT, int.Parse(Request.QueryString["LabId"]), txtLabName.Text, Convert.ToInt32(ddDepartment.SelectedValue));

                    string theUrl = "window.location.href='frmAdmin_LaboratoryGroups.aspx?Fid=" + Request.QueryString["Fid"]+"'";
                    IQCareMsgBox.NotifyAction("Saved successfully.", "Lab Groups", false, this, theUrl);

                }
            }

        }

        /// <summary>
        /// Handles the Click event of the btnExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAdmin_LaboratoryGroups.aspx?Fid=" + Request.QueryString["Fid"], false);
        }
    }
}