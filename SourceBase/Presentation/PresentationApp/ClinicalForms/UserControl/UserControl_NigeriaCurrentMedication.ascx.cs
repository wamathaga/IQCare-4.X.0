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

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControl_NigeriaCurrentMedication : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindrcbmedicalCondition();
            }

        }
        protected void gvcurrentmedication_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblmedication");
                CheckBox Chkmedication = (CheckBox)e.Row.FindControl("Chkmedication");
                ((CheckBox)e.Row.FindControl("Chkmedication")).Attributes.Add("onclick", "toggleCurrentPC('" + Chkmedication.ClientID + "')");

                if (Chkmedication.Text == "Other")
                {
                    Chkmedication.Attributes.Add("onchange", "ShowHideDiv('DivOtherComplaint');");
                }
                if (Chkmedication.Text.ToUpper() == "NONE")
                {
                    Chkmedication.Attributes.Add("class", "checkbox");
                    
                }
                else
                    Chkmedication.Attributes.Add("class", "selectAll");
            }
        }
        protected void BindrcbmedicalCondition()
        {
            IQCareUtils util = new IQCareUtils();
            DataTable dt = util.GetDataTable("MST_CODE", "NigeriaCurrentMedication");
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };
           
            gvcurrentmedication.DataSource = dt;
            gvcurrentmedication.DataBind();

        }
    }
}