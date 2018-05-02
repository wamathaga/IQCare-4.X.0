using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

    public class BindFunctions
    {
        #region "Constructor"
        public BindFunctions()
        {
        }
        public void BindCombo(DataTable theDT, string theTextField, string theValueField, DropDownList theDropDown = null)
        {
            DataRow[] DR = theDT.Select("" + theValueField + " = 0");
            if (DR.Length < 1)
            {
                DataRow theDR = theDT.NewRow();
                theDR["" + theTextField + ""] = "Select";
                theDR["" + theValueField + ""] = 0;
                theDT.Rows.InsertAt(theDR, 0);
            }
            if (theDropDown != null)
            {
                theDropDown.DataSource = theDT;
                theDropDown.DataTextField = theTextField;
                theDropDown.DataValueField = theValueField;
                theDropDown.DataBind();
            }
        }

        #endregion
    }
