using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Interface.Clinical;
using Interface.Security;
using Application.Common;
using Application.Presentation;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlKNH_Allergies : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList("DrugAllergiesToxicitiesPaeds");
                cblDrugAllergiesToxicitiesPaeds.Attributes.Add("onclick", "CheckBoxToggleShowHide1('" + cblDrugAllergiesToxicitiesPaeds.ClientID + "','divspecifyarvallergyshowhide','ARV');CheckBoxToggleShowHide1('" + cblDrugAllergiesToxicitiesPaeds.ClientID + "','divspecifyantibioticshowhide','Antibiotic');CheckBoxToggleShowHide1('" + cblDrugAllergiesToxicitiesPaeds.ClientID + "','divspecifyotherdrugshowhide','Other');");
                //cblDrugAllergiesToxicitiesPaeds.Attributes.Add("onclick","CheckBoxToggleShowHide1('"+ cblDrugAllergiesToxicitiesPaeds.ClientID +"','divspecifyantibioticshowhide','Antibiotic')");
                //cblDrugAllergiesToxicitiesPaeds.Attributes.Add("onclick","CheckBoxToggleShowHide1('"+ cblDrugAllergiesToxicitiesPaeds.ClientID +"','divspecifyotherdrugshowhide','Other')");
                
            }
        }
        public void BindList(string fieldname)
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\AllMasters.con"));
            DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)theUtils.CreateTableFromDataView(theCodeDV);
            DataTable theDT = new DataTable();
            if (theCodeDT.Rows.Count > 0)
            {
                DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);
                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + theCodeDT.Rows[0]["CodeId"];

                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCheckedList(cblDrugAllergiesToxicitiesPaeds, theDT, "Name", "ID");
            }
        }
    }
}