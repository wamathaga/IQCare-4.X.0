using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Interface.Clinical;
using Interface.Security;
using Application.Common;
using Interface.Administration;
using Application.Presentation;
using Interface.Pharmacy;
using System.Collections.Generic;
using AjaxControlToolkit;
using System.Web.Script.Serialization;

namespace PresentationApp.GreenCard.UserControls
{
    public partial class UC_Vitals : System.Web.UI.UserControl
    {
        IAllergyInfo PatientManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientManager = (IAllergyInfo)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BAllergyInfo, BusinessProcess.Clinical");
            DataSet theDS = PatientManager.GetAllAllergyData(Convert.ToInt32(Session["PatientId"]));
            grdAllergy.DataSource = theDS.Tables[0];
            BindGrid();
        }
        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Id";
            theCol0.DataField = "Id";
            //theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.HeaderStyle.CssClass = "visible-lg";
            theCol0.ItemStyle.CssClass = "visible-lg";
            grdAllergy.Columns.Add(theCol0);

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Patientid";
            theCol1.DataField = "ptn_pk";
            //theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.HeaderStyle.CssClass = "visible-lg";
            theCol1.ItemStyle.CssClass = "visible-lg";
            grdAllergy.Columns.Add(theCol1);

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "AllergyTypeID";
            theCol2.DataField = "AllergyTypeID";
            //theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.HeaderStyle.CssClass = "visible-lg";
            theCol2.ItemStyle.CssClass = "visible-lg";
            grdAllergy.Columns.Add(theCol2);

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Allergy Type";
            theCol3.DataField = "AllergyTypeDesc";
            theCol3.HeaderStyle.CssClass = "visible-lg";
            theCol3.ItemStyle.CssClass = "visible-lg";
            theCol3.ReadOnly = true;
            grdAllergy.Columns.Add(theCol3);

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "AllergenTypeID";
            theCol4.DataField = "AllergenTypeID";
            //theCol4.ItemStyle.CssClass = "textstyle";
            theCol4.HeaderStyle.CssClass = "visible-lg";
            theCol4.ItemStyle.CssClass = "visible-lg";
            grdAllergy.Columns.Add(theCol4);

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Allergen";
            theCol5.DataField = "AllergenDesc";
            //theCol5.ItemStyle.CssClass = "textstyle";
            theCol5.HeaderStyle.CssClass = "visible-lg";
            theCol5.ItemStyle.CssClass = "visible-lg";
            theCol5.ReadOnly = true;
            grdAllergy.Columns.Add(theCol5);


            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "Other Allergen";
            //theCol6.ItemStyle.CssClass = "textstyle";
            theCol6.DataField = "otherAllergen";
            theCol6.HeaderStyle.CssClass = "visible-lg";
            theCol6.ItemStyle.CssClass = "visible-lg";
            theCol6.ReadOnly = true;
            //theCol6.ItemStyle.Width = 200;
            grdAllergy.Columns.Add(theCol6);

            BoundField theCol7 = new BoundField();
            theCol7.HeaderText = "Reaction Type";
            //theCol7.ItemStyle.CssClass = "textstyle";
            theCol7.DataField = "typeReaction";
            theCol7.HeaderStyle.CssClass = "visible-lg";
            theCol7.ItemStyle.CssClass = "visible-lg";
            theCol7.ReadOnly = true;
            //theCol7.ItemStyle.Width = 200;
            grdAllergy.Columns.Add(theCol7);

            BoundField theCol8 = new BoundField(); // double
            theCol8.HeaderText = "SevrityTypeID";
            theCol8.DataField = "SevrityTypeID";
            //theCol8.ItemStyle.CssClass = "textstyle";
            theCol8.HeaderStyle.CssClass = "visible-lg";
            theCol8.ItemStyle.CssClass = "visible-lg";
            grdAllergy.Columns.Add(theCol8);

            BoundField theCol9 = new BoundField();
            theCol9.HeaderText = "Severity";
            //theCol9.ItemStyle.CssClass = "textstyle";
            theCol9.HeaderStyle.CssClass = "visible-lg";
            theCol9.ItemStyle.CssClass = "visible-lg";
            theCol9.DataField = "severityDesc";
            theCol9.ReadOnly = true;
            grdAllergy.Columns.Add(theCol9);

            BoundField theCol10 = new BoundField(); // double
            theCol10.HeaderText = "Date Allergy";
            theCol10.DataField = "dateAllergy";
            //theCol10.ItemStyle.CssClass = "textstyle";
            theCol10.HeaderStyle.CssClass = "visible-lg";
            theCol10.ItemStyle.CssClass = "visible-lg";
            theCol10.DataFormatString = "{0:dd-MMM-yyyy}";
            grdAllergy.Columns.Add(theCol10);

            //CommandField objfield = new CommandField();
            //objfield.ButtonType = ButtonType.Link;
            //objfield.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
            //objfield.ShowDeleteButton = true;
            //grdAllergy.Columns.Add(objfield);

            grdAllergy.DataBind();
            grdAllergy.Columns[0].Visible = false;
            grdAllergy.Columns[1].Visible = false;
            grdAllergy.Columns[2].Visible = false;
            grdAllergy.Columns[4].Visible = false;
            grdAllergy.Columns[8].Visible = false;
        }
    }
}