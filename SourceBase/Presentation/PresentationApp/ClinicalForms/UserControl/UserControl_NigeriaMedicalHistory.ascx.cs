using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Application.Common;
using Interface.Clinical;
//Third party Libs
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Linq;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControl_NigeriaMedicalHistory : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Init_Form();
            }
            

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                JavaScriptFunctionsOnLoad();
            }
            
        }
        private void Init_Form()
        {
            BindrcbmedicalCondition();
        }
        protected void BindrcbmedicalCondition()
        {
            IQCareUtils utils = new IQCareUtils();
            DataTable dt = utils.GetDataTable("Mst_Symptom", "NigeriaSymptoms");
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };
            if (Session["PCValue"] != null)
            {
                string[] pcRow = Session["PCValue"].ToString().Split('#');
                foreach (string val in pcRow)
                {
                    string[] pcCellvalue = val.Split(',');
                    if (dt.Rows.Find(pcCellvalue[0]) != null)
                    {
                        DataRow dr = dt.Rows.Find(pcCellvalue[0]);
                        dr["ChkVal"] = "1";
                        dr["ChkValText"] = pcCellvalue[2].ToString();
                    }
                }

            }


            gvMedicalHistory.DataSource = dt;
            gvMedicalHistory.DataBind();
           
        }
        private void JavaScriptFunctionsOnLoad()
        {
            
            double age = Convert.ToDouble(Session["patientageinyearmonth"].ToString());

            if (age >= 15)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showHideMedicalPeads", "show_Medhide('divshowMedicalHistory','visible');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showHideMedicalPeads", "show_Medhide('divshowMedicalHistory','notvisible');", true);
            }
        }
        protected DataTable GetDataTable(string flag, string fieldName)
        {
            BIQAdultIE objAdultIEFields = new BIQAdultIE();
            objAdultIEFields.Flag = flag;
            objAdultIEFields.PtnPk = 0;//Convert.ToInt32(Session["PatientID"].ToString());
            objAdultIEFields.LocationId = 0;// Int32.Parse(Session["AppLocationId"].ToString());
            objAdultIEFields.FieldName = fieldName;

            IKNHAdultIE theExpressManager;
            theExpressManager = (IKNHAdultIE)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAdultIE, BusinessProcess.Clinical");
            DataTable dt = theExpressManager.GetKnhAdultIEData(objAdultIEFields);
            return dt;

        }

        protected void gvPresentingComplaints_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblMedical");
                CheckBox ChkMedical = (CheckBox)e.Row.FindControl("ChkMedical");
                TextBox txtMedical = (TextBox)e.Row.FindControl("txtMedical");
                ((CheckBox)e.Row.FindControl("ChkMedical")).Attributes.Add("onclick", "toggleMedicalPC('" + ChkMedical.ClientID + "')");
                txtMedical.Attributes.Add("onkeyup", "chkDecimal('" + txtMedical.ClientID + "');");
                //txtPresenting.Attributes.Add("onkeydown", "chkInteger('" + ChkMedical.ClientID + "')");
                //txtPresenting.Visible = true;
                if (ChkMedical.Text == "Other")
                {
                    ChkMedical.Attributes.Add("onchange", "ShowHideDiv('DivtoggleMedicalPC');");
                }
                if (ChkMedical.Text.ToUpper() == "NONE")
                {
                    ChkMedical.Attributes.Add("class", "checkbox");
                    //ChkMedical.Attributes.Add("OnChange", "DisableCheckbox(" + ChkMedical.ClientID + ");");
                }
                else
                    ChkMedical.Attributes.Add("class", "selectAll");
            }
        }
    }
}