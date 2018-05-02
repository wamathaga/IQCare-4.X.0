using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Application.Presentation;
using Application.Common;
using System.Text;
using Interface.Administration;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControl_NigeriaAdherence : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AddUIAttributes();
                BindControl();
            }
        }
        private void AddUIAttributes()
        {

            txtdtIntrupptedDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
            txtdtIntrupptedDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3');");
            txtStopedReasonDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            txtStopedReasonDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtintrpdays.Attributes.Add("onkeyup", "chkInteger('" + txtintrpdays.ClientID + "')");
            txtstoppeddays.Attributes.Add("onkeyup", "chkInteger('" + txtstoppeddays.ClientID + "')");
        }
        public void BindControl()
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            BindFunctions bindFunctions = new BindFunctions();
            DataTable dt = new DataTable();

            dt = iQCareUtils.GetDataTable("MST_REASON", "MissedReason-Nigeria");
            //Subsituations/Interruption            
            if (dt.Rows.Count > 0)
            {
                bindFunctions.BindCombo(ddlreasonInterrupted, dt, "Name", "ID");
                bindFunctions.BindCombo(ddlReasomMissed, dt, "Name", "ID");
                bindFunctions.BindCombo(ddlStopedReason, dt, "Name", "ID"); 
            }

            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\AllMasters.con"));
            gvdisclosed.DataSource = theDSXML.Tables["mst_HIVDisclosure"];
            gvdisclosed.DataBind();
            theDSXML.Dispose();
        }
        protected void gvdisclosed_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lbldisclosed");
                CheckBox Chkdisclosed = (CheckBox)e.Row.FindControl("Chkdisclosed");
                TextBox txtPresenting = (TextBox)e.Row.FindControl("txtPresenting");
                ((CheckBox)e.Row.FindControl("Chkdisclosed")).Attributes.Add("onclick", "toggleDisclosedPC('" + Chkdisclosed.ClientID + "')");
                //txtPresenting.Attributes.Add("onkeyup", "chkDecimal('" + txtPresenting.ClientID + "');");
                
                if (Chkdisclosed.Text == "Other")
                {
                    Chkdisclosed.Attributes.Add("onchange", "ShowHideDiv('DivDiscloseOther');");
                }
                if (Chkdisclosed.Text.ToUpper() == "NO ONE")
                {
                    Chkdisclosed.Attributes.Add("class", "checkbox");
                    //Chkdisclosed.Attributes.Add("OnChange", "DisableCheckbox(" + Chkdisclosed.ClientID + ");");
                }
                else
                    Chkdisclosed.Attributes.Add("class", "selectAll");
            }
        }
    }
}