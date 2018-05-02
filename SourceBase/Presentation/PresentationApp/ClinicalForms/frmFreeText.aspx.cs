using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.VisualBasic;
using Interface.Clinical;
using Interface.Security;
using Application.Common;
using Interface.Administration;
using Application.Presentation;
using Interface.Pharmacy;
using System.Collections.Generic;
using AjaxControlToolkit;
using System.Web.Script.Serialization;
using System.Drawing;
using System.Xml.Serialization;

namespace PresentationApp.ClinicalForms
{
    public partial class frmFreeText : System.Web.UI.Page
    {
        IQCareUtils theUtils = new IQCareUtils();
        DataTable theDTGrid = new DataTable();
        DataView theDTView = new DataView();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                FillGrid(Convert.ToInt32(Session["PatientId"] == null ? "0" : Session["PatientId"]),
                    Convert.ToInt32(Session["PatientVisitId"] == null ? "0" : Session["PatientVisitId"]));
            }
        }
        void FillGrid(int Ptn_pk, int Visit_pk)
        {
            //Session["PatientId"];
            IFreeText FreeTextManager;           
            try
            {               
                FreeTextManager = (IFreeText)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFreeText, BusinessProcess.Clinical");
                DataSet theDs = FreeTextManager.GetFreeText(Ptn_pk, Visit_pk);
                if (theDs != null && theDs.Tables[0].Rows.Count > 0)
                {
                    btnsaveFreeText.Visible = true;
                    ViewState["GridFreeTextData"] = theDs.Tables[0];
                    GrdFreeText.DataSource = theDs.Tables[0];
                    GrdFreeText.DataBind();
                    ViewState["sortdr"] = "Asc";  
                }
                else
                {
                    btnsaveFreeText.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void GrdFreeText_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)ViewState["GridFreeTextData"]);
            int r = Convert.ToInt32(e.RowIndex.ToString());
            int Id = -1;
            try
            {
                if (theDT.Rows.Count > 0)
                {

                    if (theDT.Rows[r].HasErrors == false)
                    {
                        if ((theDT.Rows[r]["Freetext_id"] != null) && (theDT.Rows[r]["Freetext_id"] != DBNull.Value))
                        {
                            if (theDT.Rows[r]["Freetext_id"].ToString() != "")
                            {
                                Id = Convert.ToInt32(theDT.Rows[r]["Freetext_id"]);
                                theDT.Rows[r].Delete();
                                theDT.AcceptChanges();
                                ViewState["GridFreeTextData"] = theDT;                                                          
                                GrdFreeText.DataSource = (DataTable)ViewState["GridFreeTextData"];
                                GrdFreeText.DataBind();
                                //IQCareMsgBox.ShowfromPage("DeleteSuccess", this);
                            }
                        }
                    }
                }
                else
                {
                    //GrdFreeText.Visible = false;
                   
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        protected void btnAddFreeText_Click(object sender, EventArgs e)
        {
            int VisitId = Convert.ToInt32(Session["PatientVisitId"] == null ? "0" : Session["PatientVisitId"]);
            if (txtDate.Value.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Form filling date required.');", true);
                //IQCareMsgBox.ShowfromPage("NoRecordSelected", this);
                return;
            }
            if (txtDescriptiveHeading.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Descriptive Heading required');", true);
                return;
            }
            if (txtDescription.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Description required.');", true);
                return;
            }
            AddFreeText(VisitId);
        }
        private void AddFreeText(int visitID)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            try
            {
                dt.Columns.Add("ptn_pk");
                dt.Columns.Add("Visit_pk");
                dt.Columns.Add("Form_filling_date");
                dt.Columns.Add("DescriptiveHeading");
                dt.Columns.Add("Description");
                dt.Columns.Add("Freetext_id");
                dr = dt.NewRow();
                if (ViewState["GridFreeTextData"] == null)
                {
                    dr["ptn_pk"] = Convert.ToInt32(Session["PatientId"] == null ? "0" : Session["PatientId"]);
                    dr["Visit_pk"] = Convert.ToString(visitID);
                    dr["Form_filling_date"] = "" + txtDate.Value.Trim() + "";
                    dr["DescriptiveHeading"] = txtDescriptiveHeading.Text.Trim();
                    dr["Description"] = txtDescription.Text.Trim();
                    dr["Freetext_id"] = CreateUniqueNo();
                    dt.Rows.Add(dr);
                    ViewState["GridFreeTextData"] = dt;
                }
                else
                {
                    dr["ptn_pk"] = Convert.ToInt32(Session["PatientId"] == null ? "0" : Session["PatientId"]);
                    dr["Visit_pk"] = Convert.ToString(visitID);
                    dr["Form_filling_date"] = "" + txtDate.Value.Trim() + "";
                    dr["DescriptiveHeading"] = txtDescriptiveHeading.Text.Trim();
                    dr["Description"] = txtDescription.Text.Trim();
                    dr["Freetext_id"] = CreateUniqueNo();

                    DataTable dt1 = (DataTable)ViewState["GridFreeTextData"];
                    dt1.LoadDataRow(new object[] { dr["ptn_pk"], dr["Visit_pk"], dr["Form_filling_date"], dr["DescriptiveHeading"], dr["Description"], dr["Freetext_id"] }, true);
                    ViewState["GridFreeTextData"] = dt1;
                }
                //GrdFreeText.Columns.Clear();
                GrdFreeText.DataSource = ((DataTable)ViewState["GridFreeTextData"]);
                GrdFreeText.DataBind();
                if (GrdFreeText.Rows.Count > 0)
                    btnsaveFreeText.Visible = true;
                else
                    btnsaveFreeText.Visible = false;
                ClearData();
                ViewState["sortdr"] = "Asc";  
            }
            catch (Exception ex)
            {
            }
        }
       
        private void ClearData()
        {
            txtDate.Value = "";
            txtDescription.Text = "";
            txtDescriptiveHeading.Text = "";
            lblCharCounter.Text = "";
        }
       
        private Int32 CreateUniqueNo()
        {
            int Freetext_id = 0;
            Random generator = new Random();
            try
            {
                Freetext_id = generator.Next(100000, 999999);
            }
            catch (Exception ex) { }
            ViewState["Freetext_id"] = Freetext_id;
            return Freetext_id;
        }

        protected void GrdFreeText_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {                    
                    String strDes = Convert.ToString(GrdFreeText.DataKeys[e.Row.RowIndex].Values["Description"]);
                    Label lblDescription = e.Row.FindControl("lblDescription") as Label;
                    lblDescription.Text = strDes;
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void btnsaveFreeText_Click(object sender, EventArgs e)
        {
            if (GrdFreeText.Rows.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Add data into grid.');", true);
            }

            SaveUpdateFreeText();
        }
        private void SaveUpdateFreeText()
        {
            IFreeText FreeTextManager;
            Int32 rowAff = 0;
            FreeTextManager = (IFreeText)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFreeText, BusinessProcess.Clinical");
            try
            {
                if (!object.Equals(ViewState["GridFreeTextData"], null))
                {                   
                    rowAff = FreeTextManager.Save_Update_FreeText(Convert.ToInt32(Session["PatientId"] == null ? "0" : Session["PatientId"]),
                    Convert.ToInt32(Session["PatientVisitId"] == null ? "0" : Session["PatientVisitId"]),
                            ConvertDatatableToXML((DataTable)ViewState["GridFreeTextData"]),
                            Convert.ToInt32(Session["AppLocationId"] == null ? "0" : Session["AppLocationId"]),
                            Convert.ToInt32(Session["AppUserId"] == null ? "0" : Session["AppUserId"]));
                }
                if (rowAff > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Data save successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Data not save successfully.');", true);
                }
                FillGrid(Convert.ToInt32(Session["PatientId"] == null ? "0" : Session["PatientId"]),
                    Convert.ToInt32(Session["PatientVisitId"] == null ? "0" : Session["PatientVisitId"]));
            }
            catch (Exception ex)
            {

            }
        }
        // By using this method we can convert datatable to xml
        public string ConvertDatatableToXML(DataTable dt)
        {
            dt.TableName = "dtFreeText";
            string xmlstr = "";

            try
            {
                MemoryStream str = new MemoryStream();
                dt.WriteXml(str, true);
                str.Seek(0, SeekOrigin.Begin);
                StreamReader sr = new StreamReader(str);
                xmlstr = sr.ReadToEnd();
                return (xmlstr);
            }
            catch (Exception ex)
            {
                return (xmlstr);
            }
        }

        protected void GrdFreeText_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (!object.Equals(ViewState["GridFreeTextData"], null))
                {
                    DataTable dtrslt = (DataTable)ViewState["GridFreeTextData"];
                    if (dtrslt.Rows.Count > 0)
                    {
                        if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                        {
                            dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                            ViewState["sortdr"] = "Desc";
                        }
                        else
                        {
                            dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                            ViewState["sortdr"] = "Asc";
                        }
                        GrdFreeText.DataSource = dtrslt;
                        GrdFreeText.DataBind();
                    }
                }    
            }
            catch (Exception ex)
            {
            }
                    
        }

    }
}