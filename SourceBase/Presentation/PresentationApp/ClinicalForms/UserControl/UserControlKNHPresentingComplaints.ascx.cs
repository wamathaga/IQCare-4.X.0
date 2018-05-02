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
    public partial class UserControlKNHPresentingComplaints : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Init_Form();
            }

        }

        private void Init_Form()
        {
            BindrcbmedicalCondition();
        }
        protected void BindrcbmedicalCondition()
        {
            DataTable dt = GetDataTable("Mst_Code", "PresentingComplaints");
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


            gvPresentingComplaints.DataSource = dt;
            gvPresentingComplaints.DataBind();

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
                Label lbl = (Label)e.Row.FindControl("lblPresenting");
                CheckBox chkPresenting = (CheckBox)e.Row.FindControl("ChkPresenting");
                TextBox txtPresenting = (TextBox)e.Row.FindControl("txtPresenting");
                ((CheckBox)e.Row.FindControl("ChkPresenting")).Attributes.Add("onclick", "togglePC('" + chkPresenting.ClientID + "')");
                txtPresenting.Attributes.Add("onkeyup", "chkDecimal('" + txtPresenting.ClientID + "');");
                //txtPresenting.Attributes.Add("onkeydown", "chkInteger('" + chkPresenting.ClientID + "')");
                //txtPresenting.Visible = true;
                if (chkPresenting.Text == "Other")
                {
                    chkPresenting.Attributes.Add("onchange", "ShowHideDiv('DivOther');");
                }
                if (chkPresenting.Text.ToUpper() == "NONE")
                {
                    chkPresenting.Attributes.Add("class", "checkbox");
                    chkPresenting.Style.Add("margin-left", "5%");
                    //chkPresenting.Attributes.Add("OnChange", "DisableCheckbox(" + chkPresenting.ClientID + ");");
                }
                else
                    chkPresenting.Attributes.Add("class", "selectAll");

            }
        }


    }
}