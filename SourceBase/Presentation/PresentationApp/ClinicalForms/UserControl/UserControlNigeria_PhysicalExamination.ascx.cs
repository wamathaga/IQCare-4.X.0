using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Interface.Security;
using Application.Presentation;
using Application.Common;
using Interface.Clinical;
using System.Text;
using Interface.Administration;

namespace PresentationApp.ClinicalForms.UserControl
{
    public partial class UserControlNigeria_PhysicalExaminationascx : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindChkboxlst();
                
            }

            BindCheckUncheklogic(cblNigeriaPEGeneral);
            BindCheckUncheklogic(cblNigeriaPESkin);
            BindCheckUncheklogic(cblNigeriaPEHeadEyeEnt);
            BindCheckUncheklogic(cblNigeriaPECardiovascular);
            BindCheckUncheklogic(cblNigeriaPEBreast);
            BindCheckUncheklogic(cblNigeriaPEGenitalia);
            BindCheckUncheklogic(cblNigeriaPERespiratory);
            BindCheckUncheklogic(cblNigeriaPEGastrointestinal);
            BindCheckUncheklogic(cblNigeriaPENeurological);
            BindCheckUncheklogic(cblNigeriaPEMentalstatus);
        }
        public void BindChkboxlst()
        {
            BindChkboxlstControl(cblNigeriaPEGeneral, "NigeriaPEGeneral");
            BindChkboxlstControl(cblNigeriaPESkin, "NigeriaPESkin");
            BindChkboxlstControl(cblNigeriaPEHeadEyeEnt, "NigeriaPEHeadEyeEnt");
            BindChkboxlstControl(cblNigeriaPECardiovascular, "NigeriaPECardiovascular");
            BindChkboxlstControl(cblNigeriaPEBreast, "NigeriaPEBreast");
            BindChkboxlstControl(cblNigeriaPEGenitalia, "NigeriaPEGenitalia");
            BindChkboxlstControl(cblNigeriaPERespiratory, "NigeriaPERespiratory");
            BindChkboxlstControl(cblNigeriaPEGastrointestinal, "NigeriaPEGastrointestinal");
            BindChkboxlstControl(cblNigeriaPENeurological, "NigeriaPENeurological");
            BindChkboxlstControl(cblNigeriaPEMentalstatus, "NigeriaPEMentalstatus");


            cblNigeriaPEGeneral.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblNigeriaPEGeneral.ClientID + "','divNigeriaPEGeneralOther','Other (specify)','" + txtOtherNigeriaPEGeneral.ClientID + "')");
            cblNigeriaPESkin.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblNigeriaPESkin.ClientID + "','divOtherNigeriaPESkin','Other (specify)','" + txtOtherNigeriaPESkin.ClientID + "')");
            cblNigeriaPEHeadEyeEnt.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblNigeriaPEHeadEyeEnt.ClientID + "','divOtherNigeriaPEHeadEyeEnt','Other (specify)','" + txtOtherNigeriaPEHeadEyeEnt.ClientID + "')");
            cblNigeriaPECardiovascular.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblNigeriaPECardiovascular.ClientID + "','divOtherNigeriaPECardiovascular','Other (specify)','" + txtOtherNigeriaPECardiovascular.ClientID + "')");
            cblNigeriaPEBreast.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblNigeriaPEBreast.ClientID + "','divOtherNigeriaPEBreast','Other (specify)','" + txtOtherNigeriaPEBreast.ClientID + "')");
            cblNigeriaPEGenitalia.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblNigeriaPEGenitalia.ClientID + "','divOtherNigeriaPEGenitalia','Other (specify)','" + txtOtherNigeriaPEGenitalia.ClientID + "')");
            cblNigeriaPERespiratory.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblNigeriaPERespiratory.ClientID + "','divOtherNigeriaPERespiratory','Other (specify)','" + txtOtherNigeriaPERespiratory.ClientID + "')");
            cblNigeriaPEGastrointestinal.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblNigeriaPEGastrointestinal.ClientID + "','divOtherNigeriaPEGastrointestinal','Other (specify)','" + txtOtherNigeriaPEGastrointestinal.ClientID + "')");
            cblNigeriaPENeurological.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblNigeriaPENeurological.ClientID + "','divOtherNigeriaPENeurological','Other (specify)','" + txtOtherNigeriaPENeurological.ClientID + "')");
            cblNigeriaPEMentalstatus.Attributes.Add("OnClick", "CheckBoxToggleShowHidePE('" + cblNigeriaPEMentalstatus.ClientID + "','divOtherNigeriaPEMentalstatus','Other (specify)','" + txtOtherNigeriaPEMentalstatus.ClientID + "')");

            
        }
        public void BindChkboxlstControl(CheckBoxList chklst, string fieldname)
        {
            DataTable thedeCodeDT=new DataTable();
            IQCareUtils iQCareUtils = new IQCareUtils();
            BindFunctions BindManager = new BindFunctions();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\..\\XMLFiles\\AllMasters.con"));


            DataView theCodeDV = new DataView(theDSXML.Tables["MST_CODE"]);
            theCodeDV.RowFilter = "DeleteFlag=0 and Name='" + fieldname + "'";
            DataTable theCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theCodeDV);
             DataView theDV = new DataView(theDSXML.Tables["MST_DECODE"]);

             if (theCodeDT.Rows.Count > 0)
            {

                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + theCodeDT.Rows[0]["CodeId"];
                theDV.Sort = "SRNo ASC";
                thedeCodeDT = (DataTable)iQCareUtils.CreateTableFromDataView(theDV);
            }

            if (thedeCodeDT.Rows.Count > 0)
            {
                BindManager.BindCheckedList(chklst, thedeCodeDT, "Name", "ID");
            }
            

        }
        public void BindCheckUncheklogic(CheckBoxList chklst)
        {
            chklst.Attributes.Add("onchange", "togglePhyExamPC('" + chklst.ClientID + "');");
            //for (int i = 0; i < chklst.Items.Count; i++)
            //{
            //    ListItem li = chklst.Items[i];
            //    if (li.Text == "NSF")
            //    {
            //        li.Attributes.Add("onclick", "fnUncheckall('" + chklst.ClientID + "')");
            //    }
            //    else
            //    {
            //        li.Attributes.Add("onclick", "fnUncheckNormal('" + chklst.ClientID + "')");
            //    }

            //}
        }
       
    }
}