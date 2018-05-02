#region "Namespace"
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Interface.Clinical;
using Application.Common;
using Application.Presentation;
using Interface.Security;
using Interface.Administration;
using System.Collections.Generic;
#endregion
public partial class frmClinical_NonARTFollowUp : BasePage, ICallbackEventHandler
{
    /////////////////////////////////////////////////////////////////////
    // Code Written By   : Rakhi Tyagi
    // Written Date      : 3th Nov 2006
    // Modification Date : 5th May 2007
    // Description       : Non-ART Follow-Up
    //
    /////////////////////////////////////////////////////////////////////

    #region "Variable Declaration"
    public DataTable theDrugTable, OtherDrugs, theExixtDrugDT, DT1, theDTHivAssoConditionleft, theDTHivAssoConditionright;
    public DataSet theDS1 = new DataSet();

    DataSet theDS;
    DataSet theExistDS, theExistDS2, theExistVisitDS;
    DataSet theExistDS1;
    INonARTFollowUp NonARTManager;
    Hashtable theHT;
    IIQCareSystem IQCareSecurity;
    int theDrugCount, rcol_left = 0, rcol_right = 0, VisitType = 3;
    int OrderType = 221;
    string theOrderByName, theDispensedByName, str, strCallback, strHeight;
    Boolean theHIVAssocDisease;
    int thepregnant = 9, theWHOStage = 99999, theWABStage = 99999, thephysHeight = 99999, thePain = 99999, theVisitPk = 0;
    Decimal thephysTemp = 99999, thephysRR = 99999, thephysHR = 99999, thephysBPDiastolic = 99999, thephysBPSystolic = 99999, thephysWeight = 99999;
    DateTime theLMP, theOrderedByDate, theDispensedByDate, theVisitDate, theNextAppDate, theTestResultDate, theCreateDate, theCurrentDate;
    //Amitava Sinha
    int icount;
    StringBuilder sbParameter;
    StringBuilder sbValues;
    string strmultiselect;
    String TableName;
    ArrayList arl;
    //Amitava Sinha  
    #endregion

    #region Validation Functions
    private Boolean TransferValidation(int PId)
    {
        IPatientTransfer IPTransferMgr;
        IPTransferMgr = (IPatientTransfer)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientTransfer, BusinessProcess.Clinical");
        //if (Request.QueryString["name"] == "Add")
        if (Convert.ToInt32(Session["PatientVisitId"]) < 1)
        {
            DataSet DS = IPTransferMgr.GetLatestTransferDate(PId, 0);
            if (DS.Tables[0].Rows[0]["NotExistTransferDate"].ToString() != "0")
            {
                if (Convert.ToDateTime(txtvisitDate.Value) < Convert.ToDateTime(DS.Tables[0].Rows[0]["TransferDate"]))
                {
                    IQCareMsgBox.Show("TransferDate_4", this);
                    txtvisitDate.Focus();
                    return false;
                }
            }
        }
        ////else if (Request.QueryString["name"] == "Edit")
        else if ((Convert.ToInt32(Session["PatientVisitId"])) > 1)
        {
            //int visitPK = Convert.ToInt32(Request.QueryString["visitid"]);
            int visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            DataSet DS = IPTransferMgr.GetLatestTransferDate(PId, visitPK);
            if (DS.Tables[0].Rows[0]["NotExistTransferDate"].ToString() != "0")
            {

                if (DS.Tables[1].Rows[0]["PrevDate"].ToString() == "0" && DS.Tables[2].Rows[0]["LaterDate"].ToString() != "0")
                {
                    if (Convert.ToDateTime(txtvisitDate.Value) > Convert.ToDateTime(DS.Tables[2].Rows[0]["LaterDate"]))
                    {
                        IQCareMsgBox.Show("TransferDate_5", this);
                        txtvisitDate.Focus();
                        return false;
                    }
                }
                //else if (DS.Tables[1].Rows[0]["PrevDate"].ToString() != "0" && DS.Tables[2].Rows[0]["LaterDate"].ToString() != "0")
                //{
                //    if (Convert.ToInt32(DS.Tables[3].Rows[0]["LocationID"]) != Convert.ToInt32(DS.Tables[1].Rows[0]["TransferredToID"]))
                //    {
                //        if (Convert.ToDateTime(txtvisitDate.Value) < Convert.ToDateTime(DS.Tables[1].Rows[0]["PrevDate"]) || Convert.ToDateTime(txtvisitDate.Value) > Convert.ToDateTime(DS.Tables[2].Rows[0]["LaterDate"]))
                //        {
                //            IQCareMsgBox.Show("TransferDate_6", this);
                //            txtvisitDate.Focus();
                //            return false;
                //        }
                //    }
                //}
                //else if (DS.Tables[1].Rows[0]["PrevDate"].ToString() != "0" && DS.Tables[2].Rows[0]["LaterDate"].ToString() == "0")
                //{
                //    if (Convert.ToDateTime(txtvisitDate.Value) < Convert.ToDateTime(DS.Tables[1].Rows[0]["PrevDate"]))
                //    {
                //        IQCareMsgBox.Show("TransferDate_7", this);
                //        txtvisitDate.Focus();
                //        return false;
                //    }
                //}
            }

        }
        return true;
    }
    private Boolean FieldValidationPaperless()
    {
        //int PatientId = Convert.ToInt32(Request.QueryString["PatientId"]);
        int PatientId = Convert.ToInt32(Session["PatientVisitId"]);
        AuthenticationManager auth = new AuthenticationManager();
        bool dateconstraint = auth.CheckDateConstriant(Convert.ToInt32(Session["AppLocationId"]));

        IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        DataTable theDT = MakeDrugTable(PnlAddOtherMedication);
        IQCareUtils theUtils = new IQCareUtils();
        if (txtvisitDate.Value == "")
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["Control"] = "VisitDate";
            IQCareMsgBox.Show("BlankTextBox", theMsg, this);
            return false;
        }

        if (TransferValidation(PatientId) == false)
        {
            return false;
        }

        if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtils.MakeDate(txtvisitDate.Value)))
        {
            IQCareMsgBox.Show("CompareDate5", this);
            txtvisitDate.Focus();
            return false;
        }
        else if (txtvisitDate.Value != "")
        {
            theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtvisitDate.Value));

            if (Session["IEVisitDate"] != null)
            {
                DateTime theIEVisitDate = Convert.ToDateTime(Session["IEVisitDate"].ToString());
                if (theIEVisitDate > theVisitDate)
                {
                    IQCareMsgBox.Show("CompareIEVisitDate", this);
                    txtvisitDate.Focus();
                    return false;
                }
                else if (theVisitDate > theCurrentDate)
                {
                    IQCareMsgBox.Show("NonARTVisitDate", this);
                    txtvisitDate.Focus();
                    return false;
                }
            }
        }
        if (!dateconstraint)
        {
            if (txtLMP.Value != "")
            {
                theLMP = Convert.ToDateTime(theUtils.MakeDate(txtLMP.Value));
                if (theLMP > theCurrentDate)
                {
                    IQCareMsgBox.Show("NonARTLMPDate", this);
                    txtvisitDate.Focus();
                    return false;
                }

            }
        }

        if (txtSpecifyDate.Value != "")
        {
            theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtvisitDate.Value));
            theNextAppDate = Convert.ToDateTime(theUtils.MakeDate(txtSpecifyDate.Value));
            if (theNextAppDate <= theVisitDate)                ///theCurrentDate)
            {
                IQCareMsgBox.Show("NonARTNextAppDate", this);
                txtSpecifyDate.Focus();
                return false;
            }

        }
        /* Commented by Jayant on 12-05-08
        if (ddlPharmOrderedbyName.SelectedIndex == 0)
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["Control"] = "Ordered By";
            IQCareMsgBox.Show("BlankDropDown", theMsg, this);
            return false;
        }
        */

        if (Session["SaveOIDrug"] != null)
        {
            if (((DataTable)Session["SaveOIDrug"]).Rows.Count == 0)
            {
                theDrugCount = 0;
            }
            else
            {
                theDrugCount = 1;
            }
        }
        else
        {
            theDrugCount = 0;
        }


        if (lstpharmSulfaTMXDose.SelectedIndex == 0)// && ((DataTable)Session["SaveOIDrug"]).Rows.Count == 0)
        {
            if (lstpharmFluconazoleDose.SelectedIndex == 0 && theDrugCount == 0)
            {
                if (ddlPharmReportedbyName.SelectedIndex != 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Pharmacy";
                    IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                    return false;
                }
                if (!dateconstraint)
                {
                    if (txtpharmReportedbyDate.Value != "")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "Pharmacy";
                        IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                        return false;
                    }
                }
            }
            else
            {

                if (ddlPharmReportedbyName.SelectedIndex == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Dispensed By";
                    IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                    return false;
                }
                if (!dateconstraint)
                {
                    if (txtpharmReportedbyDate.Value == "")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "DispensedByDate";
                        IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                        return false;
                    }
                }


            }

        }
        else
        {

            if (ddlPharmReportedbyName.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Dispensed By";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }
            if (!dateconstraint)
            {
                if (txtpharmReportedbyDate.Value == "")
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "DispensedByDate";
                    IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                    return false;
                }
            }


        }
        //Order By Section
        if (lstpharmSulfaTMXDose.SelectedIndex == 0)// && ((DataTable)Session["SaveOIDrug"]).Rows.Count == 0)
        {
            if (lstpharmFluconazoleDose.SelectedIndex == 0 && theDrugCount == 0)
            {
                if (ddlPharmOrderedbyName.SelectedIndex != 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Pharmacy";
                    IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                    return false;
                }
                if (!dateconstraint)
                {
                    if (txtpharmOrderedbyDate.Value != "")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "Pharmacy";
                        IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                        return false;
                    }
                }
            }
            else
            {
                if (ddlPharmOrderedbyName.SelectedIndex == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Order By";
                    IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                    return false;
                }
                if (!dateconstraint)
                {
                    if (txtpharmOrderedbyDate.Value == "")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "OrderByDate";
                        IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                        return false;
                    }
                }

            }

        }
        else
        {
            if (ddlPharmOrderedbyName.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Order By";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }
            if (!dateconstraint)
            {
                if (txtpharmOrderedbyDate.Value == "")
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "OrderByDate";
                    IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                    return false;
                }
            }


        }

        //
        if (ddlPharmSignature.SelectedIndex == 3)
        {
            if (ddlCounselerName.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Counselor Name";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }
        }
        if (!dateconstraint)
        {
            if (txtpharmOrderedbyDate.Value != "")
            {
                if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value)))
                {
                    IQCareMsgBox.Show("NonARTOrderbyDate", this);
                    return false;
                }

                else
                {
                    if (Convert.ToDateTime(txtvisitDate.Value) > Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value)))
                    {
                        IQCareMsgBox.Show("OrderredVisitDate", this);
                        return false;
                    }
                }
            }
        }

        if (!dateconstraint)
        {
            if (txtpharmReportedbyDate.Value != "")
            {
                if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtils.MakeDate(txtpharmReportedbyDate.Value)))
                {
                    IQCareMsgBox.Show("NonARTDispensedbyDate", this);
                    return false;
                }
                else if (Convert.ToDateTime(txtvisitDate.Value) > Convert.ToDateTime(txtpharmReportedbyDate.Value))
                {
                    IQCareMsgBox.Show("DispensedVisitDate", this);
                    return false;
                }
            }
        }
        return true;
    }
    private Boolean FieldValidation()
    {
        //int PatientId = Convert.ToInt32(Request.QueryString["PatientId"]);
        AuthenticationManager auth = new AuthenticationManager();
        bool dateconstraint = auth.CheckDateConstriant(Convert.ToInt32(Session["AppLocationId"]));

        int PatientId = Convert.ToInt32(Session["PatientId"]);
        IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        DataTable theDT = MakeDrugTable(PnlAddOtherMedication);
        IQCareUtils theUtils = new IQCareUtils();
        if (txtvisitDate.Value == "")
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["Control"] = "VisitDate";
            IQCareMsgBox.Show("BlankTextBox", theMsg, this);
            return false;
        }

        if (TransferValidation(PatientId) == false)
        {
            return false;
        }

        if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtils.MakeDate(txtvisitDate.Value)))
        {
            IQCareMsgBox.Show("CompareDate5", this);
            txtvisitDate.Focus();
            return false;
        }
        else if (txtvisitDate.Value != "")
        {
            theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtvisitDate.Value));

            if (Session["IEVisitDate"] != null)
            {
                DateTime theIEVisitDate = Convert.ToDateTime(Session["IEVisitDate"].ToString());
                if (theIEVisitDate > theVisitDate)
                {
                    IQCareMsgBox.Show("CompareIEVisitDate", this);
                    txtvisitDate.Focus();
                    return false;
                }
                else if (theVisitDate > theCurrentDate)
                {
                    IQCareMsgBox.Show("NonARTVisitDate", this);
                    txtvisitDate.Focus();
                    return false;
                }
            }
        }
        if (!dateconstraint)
        {
            if (txtLMP.Value != "")
            {
                theLMP = Convert.ToDateTime(theUtils.MakeDate(txtLMP.Value));
                if (theLMP > theCurrentDate)
                {
                    IQCareMsgBox.Show("NonARTLMPDate", this);
                    txtvisitDate.Focus();
                    return false;
                }

            }
        }

        if (!dateconstraint)
        {
            if (txtSpecifyDate.Value != "")
            {
                theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtvisitDate.Value));
                theNextAppDate = Convert.ToDateTime(theUtils.MakeDate(txtSpecifyDate.Value));
                if (theNextAppDate <= theVisitDate)                ///theCurrentDate)
                {
                    IQCareMsgBox.Show("NonARTNextAppDate", this);
                    txtSpecifyDate.Focus();
                    return false;
                }

            }
        }
        /* Commented by Jayant on 12-05-08
        if (ddlPharmOrderedbyName.SelectedIndex == 0)
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["Control"] = "Ordered By";
            IQCareMsgBox.Show("BlankDropDown", theMsg, this);
            return false;
        }
        */

        if (Session["SaveOIDrug"] != null)
        {
            if (((DataTable)Session["SaveOIDrug"]).Rows.Count == 0)
            {
                theDrugCount = 0;
            }
            else
            {
                theDrugCount = 1;
            }
        }
        else
        {
            theDrugCount = 0;
        }


        if (lstpharmSulfaTMXDose.SelectedIndex == 0)// && ((DataTable)Session["SaveOIDrug"]).Rows.Count == 0)
        {
            if (lstpharmFluconazoleDose.SelectedIndex == 0 && theDrugCount == 0)
            {
                if (ddlPharmReportedbyName.SelectedIndex != 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Pharmacy";
                    IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                    return false;
                }
                if (!dateconstraint)
                {
                    if (txtpharmReportedbyDate.Value != "")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "Pharmacy";
                        IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                        return false;
                    }
                }
            }
            else
            {
                if (Session["Paperless"].ToString() == "0")
                {
                    if (ddlPharmReportedbyName.SelectedIndex == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "Dispensed By";
                        IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                        return false;
                    }
                    if (!dateconstraint)
                    {
                        if (txtpharmReportedbyDate.Value == "")
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["Control"] = "DispensedByDate";
                            IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                            return false;
                        }
                    }
                }

            }

        }
        else
        {
            if (Session["Paperless"].ToString() == "0")
            {
                if (ddlPharmReportedbyName.SelectedIndex == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Dispensed By";
                    IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                    return false;
                }
                if (!dateconstraint)
                {
                    if (txtpharmReportedbyDate.Value == "")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "DispensedByDate";
                        IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                        return false;
                    }
                }
            }

        }
        //Order By Section
        if (lstpharmSulfaTMXDose.SelectedIndex == 0)// && ((DataTable)Session["SaveOIDrug"]).Rows.Count == 0)
        {
            if (lstpharmFluconazoleDose.SelectedIndex == 0 && theDrugCount == 0)
            {
                if (ddlPharmOrderedbyName.SelectedIndex != 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Pharmacy";
                    IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                    return false;
                }
                if (!dateconstraint)
                {
                    if (txtpharmOrderedbyDate.Value != "")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "Pharmacy";
                        IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                        return false;
                    }
                }
            }
            else
            {
                if (ddlPharmOrderedbyName.SelectedIndex == 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Order By";
                    IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                    return false;
                }
                if (!dateconstraint)
                {
                    if (txtpharmOrderedbyDate.Value == "")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "OrderByDate";
                        IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                        return false;
                    }
                }

            }

        }
        else
        {
            if (ddlPharmOrderedbyName.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Order By";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }
            if (!dateconstraint)
            {
                if (txtpharmOrderedbyDate.Value == "")
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "OrderByDate";
                    IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                    return false;
                }
            }


        }

        //
        if (ddlPharmSignature.SelectedIndex == 3)
        {
            if (ddlCounselerName.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Counselor Name";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }
        }
        if (!dateconstraint)
        {
            if (txtpharmOrderedbyDate.Value != "")
            {
                if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value)))
                {
                    IQCareMsgBox.Show("NonARTOrderbyDate", this);
                    return false;
                }

                else
                {
                    if (Convert.ToDateTime(txtvisitDate.Value) > Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value)))
                    {
                        IQCareMsgBox.Show("OrderredVisitDate", this);
                        return false;
                    }
                }
            }
        }

        if (!dateconstraint)
        {
            if (txtpharmReportedbyDate.Value != "")
            {
                if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtils.MakeDate(txtpharmReportedbyDate.Value)))
                {
                    IQCareMsgBox.Show("NonARTDispensedbyDate", this);
                    return false;
                }
                else if (Convert.ToDateTime(txtvisitDate.Value) > Convert.ToDateTime(txtpharmReportedbyDate.Value))
                {
                    IQCareMsgBox.Show("DispensedVisitDate", this);
                    return false;
                }
            }
        }
        return true;
    }

    private Boolean DQFieldValidation1()
    {
        AuthenticationManager auth = new AuthenticationManager();
        bool dateconstraint = auth.CheckDateConstriant(Convert.ToInt32(Session["AppLocationId"]));
        int PatientId = Convert.ToInt32(Session["PatientId"]);
        if (TransferValidation(PatientId) == false)
        {
            return false;
        }

        IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
        theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
        IQCareUtils theUtils = new IQCareUtils();
        if (txtvisitDate.Value != "")
        {
            theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtvisitDate.Value));
            if (Session["IEVisitDate"] != null)
            {
                DateTime theIEVisitDate = Convert.ToDateTime(Session["IEVisitDate"].ToString());
                if (theIEVisitDate > theVisitDate)
                {
                    IQCareMsgBox.Show("CompareIEVisitDate", this);
                    txtvisitDate.Focus();
                    return false;
                }
                else if (theVisitDate > theCurrentDate)
                {
                    IQCareMsgBox.Show("NonARTVisitDate", this);
                    txtvisitDate.Focus();
                    return false;
                }
            }
        }

        if (txtLMP.Value != "")
        {
            theLMP = Convert.ToDateTime(theUtils.MakeDate(txtLMP.Value));
            if (theLMP > theCurrentDate)
            {
                IQCareMsgBox.Show("NonARTLMPDate", this);
                txtvisitDate.Focus();
                return false;
            }


        }

        if (txtSpecifyDate.Value != "")
        {
            theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtvisitDate.Value));
            theNextAppDate = Convert.ToDateTime(theUtils.MakeDate(txtSpecifyDate.Value));
            if (theNextAppDate <= theVisitDate)                ///theCurrentDate)
            {
                IQCareMsgBox.Show("NonARTNextAppDate", this);
                txtSpecifyDate.Focus();
                return false;
            }

        }

        if ((txtvisitDate.Value != "") && (txtpharmOrderedbyDate.Value != ""))
        {
            if (Convert.ToDateTime(txtvisitDate.Value) > Convert.ToDateTime(txtpharmOrderedbyDate.Value))
            {
                IQCareMsgBox.Show("OrderredVisitDate", this);
                return false;
            }
        }


        if (txtpharmReportedbyDate.Value != "")
        {
            if (Convert.ToDateTime(txtvisitDate.Value) > Convert.ToDateTime(txtpharmReportedbyDate.Value))
            {
                IQCareMsgBox.Show("DispensedVisitDate", this);
                return false;
            }
        }

        if (txtpharmOrderedbyDate.Value != "")
            theOrderedByDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value));

        if (txtpharmReportedbyDate.Value != "")
        {
            theDispensedByDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmReportedbyDate.Value));
            if (theOrderedByDate > theDispensedByDate)
            {
                IQCareMsgBox.Show("NonARTDateCompare", this);
                return false;
            }
        }

        return true;
    }

    private string DQFieldValidation()
    {
        string strmsg = "Following values are required to complete the data quality check:</br>";
        AuthenticationManager auth = new AuthenticationManager();
        bool dateconstraint = auth.CheckDateConstriant(Convert.ToInt32(Session["AppLocationId"]));
        if (txtvisitDate.Value == "")
        {
            string scriptVisitDate = "<script language = 'javascript' defer ='defer' id = 'ColorVisitDate'>\n";
            scriptVisitDate += "To_Change_Color('Vdate');\n";
            scriptVisitDate += "</script>\n";
            RegisterStartupScript("ColorVisitDate", scriptVisitDate);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "-Visit Date";
            strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
            strmsg = strmsg + "</br>";
        }
        if (txtphysHeight.Text == "")
        {
            string scriptphysHeight = "<script language = 'javascript' defer ='defer' id = 'ColorphysHeight'>\n";
            scriptphysHeight += "To_Change_Color('physHeightVal');\n";
            scriptphysHeight += "</script>\n";
            RegisterStartupScript("ColorphysHeight", scriptphysHeight);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "-HT";
            strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
            strmsg = strmsg + "</br>";
        }

        if (txtphysWeight.Text == "")
        {
            string scriptphysWeight = "<script language = 'javascript' defer ='defer' id = 'ColorphysWeight'>\n";
            scriptphysWeight += "To_Change_Color('physWeightVal');\n";
            scriptphysWeight += "</script>\n";
            RegisterStartupScript("ColorphysWeight", scriptphysWeight);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "-WT";
            strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
            strmsg = strmsg + "</br>";
        }

        if (ddlphysWABStage.SelectedValue == "0")
        {
            string scriptWABSt = "<script language = 'javascript' defer ='defer' id = 'ColorWABStage'>\n";
            scriptWABSt += "To_Change_Color('WABSt');\n";
            scriptWABSt += "</script>\n";
            RegisterStartupScript("ColorWABStage", scriptWABSt);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "-WAB Stage";
            strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
            strmsg = strmsg + "</br>";

        }

        if (ddlphysWHOStage.SelectedValue == "0")
        {
            string scriptWHOSt = "<script language = 'javascript' defer ='defer' id = 'ColorWHOStage'>\n";
            scriptWHOSt += "To_Change_Color('WHOSt');\n";
            scriptWHOSt += "</script>\n";
            RegisterStartupScript("ColorWHOStage", scriptWHOSt);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "-WHO Stage";
            strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
            strmsg = strmsg + "</br>";

        }
        if (rdoHIVassocNone.Checked == false && rdoHIVassociate.Checked == false && PrevHIVassocNotDocumented.Checked == false)
        {
            string scriptHIVassoc = "<script language = 'javascript' defer ='defer' id = 'HIVassoc_ID'>\n";
            scriptHIVassoc += "To_Change_Color('lblHIVassocNone');\n";
            scriptHIVassoc += "To_Change_Color('lblHIVassocNotdocumented');\n";
            scriptHIVassoc += "To_Change_Color('lblHIVassociate');\n";
            scriptHIVassoc += "</script>\n";
            RegisterStartupScript("HIVassoc_ID", scriptHIVassoc);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "-HIV Associated Diseases ";
            strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
            strmsg = strmsg + "</br>";
        }

        if (chkAssessment.SelectedValue == "" && rdoartAssessmentID.Checked == false && rdoartAssessmentID2.Checked == false)
        {
            string scriptAssessment = "<script language = 'javascript' defer ='defer' id = 'Assessment_Val'>\n";
            scriptAssessment += "To_Change_Color('AssessmentVal');\n";
            scriptAssessment += "To_Change_Color('ARTAssessmentVal');\n";
            scriptAssessment += "</script>\n";
            RegisterStartupScript("Assessment_Val", scriptAssessment);
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["Control"] = "-Clinical Assessment ";
            strmsg += IQCareMsgBox.GetMessage("BlankList", theBuilder, this);
            strmsg = strmsg + "</br>";
        }

        if (Session["SaveOIDrug"] != null)
        {
            if (((DataTable)Session["SaveOIDrug"]).Rows.Count != 0)
            {
                theDrugCount = 1;
            }
            else
            {
                theDrugCount = 0;
            }
        }
        else
        {
            theDrugCount = 0;
        }
        //Order by part
        if (lstpharmSulfaTMXDose.SelectedIndex == 0)// && ((DataTable)Session["SaveOIDrug"]).Rows.Count == 0)
        {
            if (lstpharmFluconazoleDose.SelectedIndex == 0 && theDrugCount == 0)
            {
                if (ddlPharmOrderedbyName.SelectedIndex != 0)
                {
                    string scriptOI = "<script language = 'javascript' defer ='defer' id = 'ColorOByph'>\n";
                    scriptOI += "To_Change_Color('OI');\n";
                    scriptOI += "</script>\n";
                    RegisterStartupScript("ColorOByph", scriptOI);
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Pharmacy";
                    strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                    strmsg = strmsg + "</br>";
                }
                if (!dateconstraint)
                {
                    if (txtpharmOrderedbyDate.Value != "")
                    {
                        string scriptOI = "<script language = 'javascript' defer ='defer' id = 'ColorOByph'>\n";
                        scriptOI += "To_Change_Color('OI');\n";
                        scriptOI += "</script>\n";
                        RegisterStartupScript("ColorOByph", scriptOI);
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "-Pharmacy";
                        strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                        strmsg = strmsg + "</br>";
                    }
                }
            }
            else
            {
                if (ddlPharmOrderedbyName.SelectedIndex == 0)
                {
                    string scriptOrderBy = "<script language = 'javascript' defer ='defer' id = 'ColorOrderBy'>\n";
                    scriptOrderBy += "To_Change_Color('OrderedBy');\n";
                    scriptOrderBy += "</script>\n";
                    RegisterStartupScript("ColorOrderBy", scriptOrderBy);
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Order By";
                    strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                    strmsg = strmsg + "</br>";
                }

                if (txtpharmOrderedbyDate.Value == "")
                {
                    string scriptOrderbyDate = "<script language = 'javascript' defer ='defer' id = 'ColorOrderbyDate'>\n";
                    scriptOrderbyDate += "To_Change_Color('OrderedByDate');\n";
                    scriptOrderbyDate += "</script>\n";
                    RegisterStartupScript("ColorOrderbyDate", scriptOrderbyDate);
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Dispensed By Date";
                    strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                    strmsg = strmsg + "</br>";
                }

            }

        }
        else
        {
            if (ddlPharmOrderedbyName.SelectedIndex == 0)
            {
                string scriptOrderBy = "<script language = 'javascript' defer ='defer' id = 'ColorOrderBy'>\n";
                scriptOrderBy += "To_Change_Color('OrderedBy');\n";
                scriptOrderBy += "</script>\n";
                RegisterStartupScript("ColorOrderBy", scriptOrderBy);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Order By";
                strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                strmsg = strmsg + "</br>";
            }

            if (txtpharmOrderedbyDate.Value == "")
            {
                string scriptOrderbyDate = "<script language = 'javascript' defer ='defer' id = 'ColorOrderbyDate'>\n";
                scriptOrderbyDate += "To_Change_Color('OrderedByDate');\n";
                scriptOrderbyDate += "</script>\n";
                RegisterStartupScript("ColorOrderbyDate", scriptOrderbyDate);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Order By Date";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg = strmsg + "</br>";
            }

        }

        //Dispense part
        if (lstpharmSulfaTMXDose.SelectedIndex == 0)// && ((DataTable)Session["SaveOIDrug"]).Rows.Count == 0)
        {
            if (lstpharmFluconazoleDose.SelectedIndex == 0 && theDrugCount == 0)
            {
                if (ddlPharmReportedbyName.SelectedIndex != 0)
                {
                    string scriptOI = "<script language = 'javascript' defer ='defer' id = 'ColorOI'>\n";
                    scriptOI += "To_Change_Color('OI');\n";
                    scriptOI += "</script>\n";
                    RegisterStartupScript("ColorOI", scriptOI);
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Pharmacy";
                    strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                    strmsg = strmsg + "</br>";
                }

                if (txtpharmReportedbyDate.Value != "")
                {
                    string scriptOI = "<script language = 'javascript' defer ='defer' id = 'ColorOI'>\n";
                    scriptOI += "To_Change_Color('OI');\n";
                    scriptOI += "</script>\n";
                    RegisterStartupScript("ColorOI", scriptOI);
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Pharmacy";
                    strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                    strmsg = strmsg + "</br>";
                }

            }
            else
            {
                if (ddlPharmReportedbyName.SelectedIndex == 0)
                {
                    string scriptDispensedBy = "<script language = 'javascript' defer ='defer' id = 'ColorDispensedBy'>\n";
                    scriptDispensedBy += "To_Change_Color('DispensedBy');\n";
                    scriptDispensedBy += "</script>\n";
                    RegisterStartupScript("ColorDispensedBy", scriptDispensedBy);
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Dispensed By";
                    strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                    strmsg = strmsg + "</br>";
                }

                if (txtpharmReportedbyDate.Value == "")
                {
                    string scriptDispensedbyDate = "<script language = 'javascript' defer ='defer' id = 'ColorDispensedbyDate'>\n";
                    scriptDispensedbyDate += "To_Change_Color('DispensedByDate');\n";
                    scriptDispensedbyDate += "</script>\n";
                    RegisterStartupScript("ColorDispensedbyDate", scriptDispensedbyDate);
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Dispensed By Date";
                    strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                    strmsg = strmsg + "</br>";
                }

            }

        }
        else
        {
            if (ddlPharmReportedbyName.SelectedIndex == 0)
            {
                string scriptDispensedBy = "<script language = 'javascript' defer ='defer' id = 'ColorDispensedBy'>\n";
                scriptDispensedBy += "To_Change_Color('DispensedBy');\n";
                scriptDispensedBy += "</script>\n";
                RegisterStartupScript("ColorDispensedBy", scriptDispensedBy);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Dispensed By";
                strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                strmsg = strmsg + "</br>";
            }

            if (txtpharmReportedbyDate.Value == "")
            {
                string scriptDispensedbyDate = "<script language = 'javascript' defer ='defer' id = 'ColorDispensedbyDate'>\n";
                scriptDispensedbyDate += "To_Change_Color('DispensedByDate');\n";
                scriptDispensedbyDate += "</script>\n";
                RegisterStartupScript("ColorDispensedbyDate", scriptDispensedbyDate);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Dispensed By Date";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg = strmsg + "</br>";
            }

        }

        //Signature part
        if (ddlPharmSignature.SelectedIndex == 3)
        {
            if (ddlCounselerName.SelectedIndex == 0)
            {
                string scriptCounSig = "<script language = 'javascript' defer ='defer' id = 'ColorCounslorSignature'>\n";
                scriptCounSig += "To_Change_Color('CounSign');\n";
                scriptCounSig += "</script>\n";
                RegisterStartupScript("ColorCounslorSignature", scriptCounSig);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Counselor Name";
                strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                strmsg = strmsg + "</br>";
            }
        }

        return strmsg;
    }

    /************* Check Enter Visit Date with Exist all Non-ART Visit Dates ***********/
    private Boolean CheckNonARTVisitDates()
    {
        if (txtvisitDate.Value != Session["VDate"].ToString())
        {
            theVisitDate = Convert.ToDateTime(txtvisitDate.Value.ToString());
            if (Session["ExistNonARTVisits"] != null)
            {
                DataTable theNonARTVisitDT = (DataTable)Session["ExistNonARTVisits"];
                for (int i = 0; i < theNonARTVisitDT.Rows.Count; i++)
                {

                    if (theVisitDate == Convert.ToDateTime(theNonARTVisitDT.Rows[i]["VisitDate"].ToString()))
                    {
                        IQCareMsgBox.Show("ClinicalRecordExist", this);
                        return false;
                    }
                }
            }
        }
        return true;
    }

    #endregion

    #region "User Defined Functions"
    protected void BMIAttributes()
    {
        txtphysHeight.Attributes.Add("OnBlur", "CalcualteBMI('" + txtanotherbmi.ClientID + "','" + txtphysWeight.ClientID + "','" + txtphysHeight.ClientID + "');");
        txtphysWeight.Attributes.Add("OnBlur", "CalcualteBMI('" + txtanotherbmi.ClientID + "','" + txtphysWeight.ClientID + "','" + txtphysHeight.ClientID + "');");
    }
    private void GetICallBackFunction()
    {
        ClientScriptManager m = Page.ClientScript;
        str = m.GetCallbackEventReference(this, "args", "ReceiveServerData", "'this is context from server'");
        strCallback = "function CallServer(args,context){" + str + ";}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", strCallback, true);
    }

    protected void Check_IE()
    {
        if (Session["VisitType"].ToString() == "0")
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans=true;\n";
            script += "alert('No IE Exist For Patient');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='frmPatient_Home.aspx';\n";
            script += "}\n";
            script += "</script>\n";
            RegisterStartupScript("confirm", script);
        }
    }

    private void NonARTFollowUpParameters()
    {

        theHT = new Hashtable();
        IQCareUtils theUtils = new IQCareUtils();

        if (txtvisitDate.Value == "")
        {
            theVisitDate = Convert.ToDateTime(theUtils.MakeDate("01-01-1900"));
        }
        else
        {
            theVisitDate = Convert.ToDateTime(txtvisitDate.Value);
        }

        /************** LastCD4 ***************/

        if (txtTestResultsDate.Value == "")
        {
            theTestResultDate = Convert.ToDateTime(theUtils.MakeDate("01-01-1900"));
        }
        else
        {
            theTestResultDate = Convert.ToDateTime(txtTestResultsDate.Value);
        }

        //if (rdopregnantYes.Checked == true)
        //{
        //    thepregnant = 1;
        //}
        //else if (rdopregnantNo.Checked == true)
        //{
        //    thepregnant = 0;
        //}

        int thepregnant = this.rdopregnantYes.Checked == true ? 1 : (this.rdopregnantNo.Checked == true ? 0 : 9);
        int Delivered = this.rdoDeliveredYes.Checked == true ? 1 : (this.rdoDeliveredNo.Checked == true ? 0 : 9);

        if (txtLMP.Value == "")
        {
            theLMP = Convert.ToDateTime(theUtils.MakeDate("01-01-1900"));
        }
        else
        {
            theLMP = Convert.ToDateTime(txtLMP.Value);
        }

        /************ Physical Exam Data ***************/
        //physTemp
        if (this.txtphysTemp.Text == "")
        {
            thephysTemp = 99999;
        }
        else
        {
            thephysTemp = Convert.ToDecimal(txtphysTemp.Text);
        }
        //physRR
        if (this.txtphysRR.Text == "")
        {
            thephysRR = 99999;
        }
        else
        {
            thephysRR = Convert.ToDecimal(txtphysRR.Text);
        }
        //physHR
        if (this.txtphysHR.Text == "")
        {
            thephysHR = 99999;
        }
        else
        {
            thephysHR = Convert.ToDecimal(txtphysHR.Text);
        }
        //BPDiastolic
        if (this.txtphysBPDiastolic.Text == "")
        {
            thephysBPDiastolic = 99999;
        }
        else
        {
            thephysBPDiastolic = Convert.ToDecimal(txtphysBPDiastolic.Text);
        }
        //BPSystolic
        if (this.txtphysBPSystolic.Text == "")
        {
            thephysBPSystolic = 99999;
        }
        else
        {
            thephysBPSystolic = Convert.ToDecimal(txtphysBPSystolic.Text);
        }
        //Height
        if (this.txtphysHeight.Text == "")
        {
            thephysHeight = 99999;
        }
        else
        {
            thephysHeight = Convert.ToInt32(txtphysHeight.Text);
        }

        //Weight
        if (this.txtphysWeight.Text == "")
        {
            thephysWeight = 99999;
        }
        else
        {
            thephysWeight = Convert.ToDecimal(txtphysWeight.Text);
        }
        //Pain
        if (ddlPain.Value == "")
        {
            thePain = 99999;
        }
        else
        {
            thePain = Convert.ToInt32(ddlPain.Value);
        }

        // WHOStage and WABStage

        if (ddlphysWHOStage.SelectedIndex == 0)
        {
            theWHOStage = 99999;
        }
        else
        {
            theWHOStage = Convert.ToInt32(ddlphysWHOStage.SelectedValue);
        }

        if (ddlphysWABStage.SelectedIndex == 0)
        {
            theWABStage = 99999;
        }
        else
        {
            theWABStage = Convert.ToInt32(ddlphysWABStage.SelectedValue);
        }
        //OrderBy and DispensedBy

        if (ddlPharmOrderedbyName.SelectedIndex == 0)
        {
            theOrderByName = "";
        }
        else
        {
            theOrderByName = ddlPharmOrderedbyName.SelectedValue;
        }

        if (ddlPharmReportedbyName.SelectedIndex == 0)
        {
            theDispensedByName = "";
        }
        else
        {
            theDispensedByName = ddlPharmReportedbyName.SelectedValue;
        }

        //Appointment Dates and Reason as per BusinessRules
        theHT.Add("OrderType", OrderType);
        theHT.Add("VisitDate", theVisitDate.ToString("dd-MMM-yyyy"));
        theHT.Add("VisitType", VisitType);
        theHT.Add("LastCD4", theTestResultDate.ToString("dd-MMM-yyyy"));
        //theHT.Add("Pregnant", thepregnant);
        theHT.Add("Delivered", Delivered);
        if (Delivered == 1)
        {
            theHT.Add("Pregnant", 1);
            theHT.Add("DelDate", txtDeliDate.Value);
            theHT.Add("EDDDate", "");
        }
        else if (Delivered == 0)
        {
            theHT.Add("Pregnant", 1);
            theHT.Add("EDDDate", txtEDDDate.Value);
            theHT.Add("DelDate", "");
        }
        else
        {
            theHT.Add("Pregnant", thepregnant);
            if (thepregnant == 1)
            {
                theHT.Add("DelDate", "");
                theHT.Add("EDDDate", txtEDDDate.Value);
            }
            else
            {
                theHT.Add("DelDate", "");
                theHT.Add("EDDDate", "");
            }
        }
        theHT.Add("LMP", theLMP.ToString("dd-MMM-yyyy"));
        theHT.Add("physTemp", thephysTemp);
        theHT.Add("physRR", thephysRR);
        theHT.Add("physHR", thephysHR);
        theHT.Add("physHeight", thephysHeight);
        theHT.Add("physBPSystolic", thephysBPSystolic);
        theHT.Add("physBPDiastolic", thephysBPDiastolic);
        theHT.Add("physWeight", thephysWeight);
        theHT.Add("phyPain", thePain);
        theHT.Add("physWHOStage", theWHOStage);
        theHT.Add("physWABStage", theWABStage);
        theHT.Add("ClinicalNotes", txtClinicalNotes.Text);

        //Code section for Appointment Business rules
        //Appointment Dates
        if (txtSpecifyDate.Value == "")
        {
            if (this.lstappPeriod.Value != "0" && txtvisitDate.Value != "")
            {
                txtSpecifyDate.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtvisitDate.Value).AddDays(Convert.ToInt32(this.lstappPeriod.Value)));
            }
            else
            {
                theHT.Add("AppExist", 0);
                theHT.Add("VisitIDApp", 0);
            }
        }
        //Code section for Appointment Business rules
        if (txtSpecifyDate.Value != "")
        {
            IInitialEval IEAppManager;
            IEAppManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
            DataSet theDSApp = IEAppManager.GetAppointment(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppLocationId"]), Convert.ToDateTime(txtSpecifyDate.Value), Convert.ToInt32(ddlAppReason.SelectedValue));
            if (Convert.ToInt32(theDSApp.Tables[0].Rows[0]["ExistFlag"]) == 1)
            {
                if (theDSApp.Tables[1].Rows.Count > 0)
                {
                    theHT.Add("AppExist", 1);
                    theHT.Add("VisitIDApp", Convert.ToInt32(theDSApp.Tables[1].Rows[0]["Visit_pk"]));
                }
                else
                {
                    theHT.Add("AppExist", 0);
                    theHT.Add("VisitIDApp", 0);
                }
            }
            else
            {
                theHT.Add("AppExist", 0);
                theHT.Add("VisitIDApp", 0);
            }
        }
        theHT.Add("AppDate", txtSpecifyDate.Value);
        theHT.Add("AppReason", ddlAppReason.SelectedValue);
        theHT.Add("OrderBy", theOrderByName);
        theHT.Add("Signatureid", theOrderByName);
        theHT.Add("DispensedBy", theDispensedByName);
        theHT.Add("UserID", Convert.ToInt32(Session["AppUserId"].ToString()));
        if (Convert.ToString(Session["VDate"]) == txtvisitDate.Value)
        {
            theHT.Add("Flag", 1);
        }
        else { theHT.Add("Flag", 0); }


        /*if (Session["theSaveDS"] == null)
          {
              theHT.Add("CreateDate", theCreateDate);
          }
          else
          {
              DataSet theViewDS1 = (DataSet)Session["theSaveDS"];
              theHT.Add("CreateDate", Convert.ToDateTime(theViewDS1.Tables[2].Rows[0][0]));
          }
       */
    }

    /********Table Creation **********/
    private void CreateExistDrugTable()
    {
        DataTable theDT = new DataTable();
        theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
        theDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
        OtherDrugs = theDT.Copy();
    }

    private void Init_Form()
    {
        INonARTFollowUp NonARTManager;
        try
        {

            //if (Request.QueryString["PatientId"] != null)
            if (Session["PatientId"] != null)
            {
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                //if (PatientID == 0)
                //{
                //    PatientID = Convert.ToInt32(Session["patientId"]);
                //}

                NonARTManager = (INonARTFollowUp)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNonARTFollowUp, BusinessProcess.Clinical");
                IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
                IQCareUtils theUtils = new IQCareUtils();

                AddFieldAttributes();
                //theIMG.Visible = false;

                theDS = (DataSet)NonARTManager.GetPatientNonARTFollowUp(PatientID);

                //---- Rupesh 02-Jan-08    handle when generic & drug have same id ( duplicate panel id error)  STARTS---

                foreach (DataRow theDR in theDS.Tables[0].Rows)
                {
                    if (Convert.ToInt32(theDR["Generic"]) == 0) // its drug
                        theDR["DrugId"] = theDR["DrugId"].ToString() + "8888";
                    else //---- its 1 (generic)
                        theDR["DrugId"] = theDR["DrugId"].ToString() + "9999";
                }
                //-------------------------------------------- ENDS------------------------------------------------

                theExistVisitDS = (DataSet)NonARTManager.GetExistVisitNonARTFollowUp(PatientID);
                ViewState["Pregnant"] = true;
                if (theDS.Tables[2].Rows.Count != 0)
                {
                    if (theDS.Tables[2].Rows[0]["Sex"] != System.DBNull.Value)
                    {
                        if (Convert.ToInt32(theDS.Tables[2].Rows[0]["Sex"].ToString()) == 16)
                        {
                            tdPregnant.Visible = false;
                            txtLMP.Visible = false;
                        }
                    }
                }

                if (theDS.Tables[24].Rows[0]["LMP"] != System.DBNull.Value)
                {
                    txtLMP.Value = string.Format("{0:dd-MMM-yyyy}", theDS.Tables[24].Rows[0]["LMP"]);
                }

                FillDropDownList(theDS);

                if (Request.QueryString["sts"] == "0")
                {
                    Session["PatientVisitId"] = 0;
                }

                //if (Request.QueryString["name"] == "Edit" || Request.QueryString["name"] == "Delete")
                if ((Convert.ToInt32(Session["PatientVisitId"])) > 1 || Request.QueryString["name"] == "Delete")
                {
                    IPatientHome IPatienthome;
                    IPatienthome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                    DataTable theDT = IPatienthome.GetPharmacyID(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppLocationId"]), Convert.ToInt32(Session["PatientVisitId"]));
                    Session["PharmaID"] = theDT.Rows[0]["PtnPharmacyID"].ToString();
                    theVisitPk = Convert.ToInt32(Session["PatientVisitId"]);
                    int PharmacyID = Convert.ToInt32(Session["PharmaID"]);
                    if (PharmacyID != 0)
                    {
                        theDS1 = (DataSet)NonARTManager.GetExistPharmacyDetail(PharmacyID);
                        //--- Rupesh 03-Jan-08 starts ---
                        foreach (DataRow theDR in theDS1.Tables[0].Rows)
                        {

                            if (Convert.ToInt32(theDR["GenericId"]) == 0) // its drug
                                theDR["drug_pk"] = theDR["drug_pk"].ToString() + "8888";
                            else //---- its 1 (generic)
                                theDR["GenericId"] = theDR["GenericId"].ToString() + "9999";

                        }
                        //--- Rupesh 03-Jan-08 ends ---
                        theExistDS2 = NonARTManager.GetExistNonARTFollowUpDrugDetails(PharmacyID);
                        //--- Rupesh 03-Jan-08 starts ---
                        foreach (DataRow theDR in theExistDS2.Tables[0].Rows)
                        {

                            if (Convert.ToInt32(theDR["GenericID"]) == 0) // its drug
                                theDR["Drug_Pk"] = theDR["Drug_Pk"].ToString() + "8888";
                            else //---- its 1 (generic)
                                theDR["GenericID"] = theDR["GenericID"].ToString() + "9999";

                        }
                        //Jayant - 09 - May - 2008 - start
                        if (theExistDS2.Tables[0].Rows.Count != 0)
                        {
                            /********************** NonART FollowUp Drug Details **********************/

                            for (int i = 0; i < theExistDS2.Tables[0].Rows.Count; i++)
                            {
                                if (Convert.ToInt32(theExistDS2.Tables[0].Rows[i]["GenericID"].ToString()) == 2779999)
                                {
                                    if (theExistDS2.Tables[0].Rows[i]["StrengthID"].ToString() != null)
                                    {
                                        lstpharmSulfaTMXDose.Value = Convert.ToString(theExistDS2.Tables[0].Rows[i]["StrengthID"]);
                                        lstpharmSulfaTMXFreq.Value = Convert.ToString(theExistDS2.Tables[0].Rows[i]["FrequencyID"]);

                                        txtpharmSulfaTMXDuration.Value = Convert.ToString(theExistDS2.Tables[0].Rows[i]["Duration"]);
                                        txtpharmSulfaTMXQty.Value = Convert.ToString(theExistDS2.Tables[0].Rows[i]["OrderedQuantity"]);
                                        txtpharmSulfaTMXDispensed.Value = Convert.ToString(theExistDS2.Tables[0].Rows[i]["DispensedQuantity"]);
                                    }
                                }

                                if (Convert.ToInt32(theExistDS2.Tables[0].Rows[i]["GenericID"].ToString()) == 1239999)
                                {
                                    if (theExistDS2.Tables[0].Rows[i]["StrengthID"].ToString() != null)
                                    {
                                        lstpharmFluconazoleDose.Value = Convert.ToString(theExistDS2.Tables[0].Rows[i]["StrengthID"]);
                                        lstpharmFluconazoleFreq.Value = Convert.ToString(theExistDS2.Tables[0].Rows[i]["FrequencyID"]);
                                        txtpharmFluconazoleDuration.Value = Convert.ToString(theExistDS2.Tables[0].Rows[i]["Duration"]);
                                        txtpharmFluconazoleQty.Value = Convert.ToString(theExistDS2.Tables[0].Rows[i]["OrderedQuantity"]);
                                        txtpharmFluconazoleDispensed.Value = Convert.ToString(theExistDS2.Tables[0].Rows[i]["DispensedQuantity"]);
                                    }
                                }
                            }
                        }

                        theExistDS1 = NonARTManager.GetExistNonARTFollowUpDetails(PharmacyID, theVisitPk, PatientID);
                        if (theExistDS1.Tables[0].Rows.Count != 0)
                        {
                            if (Convert.ToInt32(theExistDS1.Tables[0].Rows[0]["Signature"].ToString()) == 0)
                            {
                                ddlPharmSignature.Value = "1";
                            }
                            else if (Convert.ToInt32(theExistDS1.Tables[0].Rows[0]["EmployeeID"].ToString()) != 0)
                            {
                                ddlPharmSignature.Value = "3";
                                ddlCounselerName.SelectedValue = theExistDS1.Tables[0].Rows[0]["EmployeeID"].ToString();
                                string script = "<script language = 'javascript' defer ='defer' id = 'CounsellarName'>\n";
                                script += "show('Adherance_counsellor_signature');\n";
                                script += "</script>\n";
                                RegisterStartupScript("CounsellarName", script);
                                ddlCounselerName.Visible = true;
                            }
                            else
                            {
                                ddlPharmSignature.Value = "2";
                            }
                            //
                            BindDropdownOrderBy(theExistDS1.Tables[0].Rows[0]["OrderedBy"].ToString());
                            ddlPharmOrderedbyName.SelectedValue = theExistDS1.Tables[0].Rows[0]["OrderedBy"].ToString();

                            if (theExistDS1.Tables[0].Rows[0]["OrderedBy"].ToString() == "")
                            {
                                ddlPharmOrderedbyName.SelectedValue = "";
                            }
                            else
                            {
                                BindDropdownOrderBy(theExistDS1.Tables[0].Rows[0]["OrderedBy"].ToString());
                                ddlPharmOrderedbyName.SelectedValue = theExistDS1.Tables[0].Rows[0]["OrderedBy"].ToString();
                            }

                            if (theExistDS1.Tables[0].Rows[0]["OrderedByDate"].ToString() == "")
                            {
                                txtpharmOrderedbyDate.Value = "";
                            }
                            else
                            {
                                DateTime thepharmOrderByDate = Convert.ToDateTime(theExistDS1.Tables[0].Rows[0]["OrderedByDate"].ToString());
                                txtpharmOrderedbyDate.Value = thepharmOrderByDate.ToString(Session["AppDateFormat"].ToString());
                            }
                            //txtpharmOrderedbyDate.Value = theOrderedByDate.ToString(Session["AppDateFormat"].ToString());
                            BindDropdownReportedBy(theExistDS1.Tables[0].Rows[0]["DispensedBy"].ToString());
                            ddlPharmReportedbyName.SelectedValue = theExistDS1.Tables[0].Rows[0]["DispensedBy"].ToString();

                            if (theExistDS1.Tables[0].Rows[0]["DispensedByDate"].ToString() == "")
                            {
                                txtpharmReportedbyDate.Value = "";
                            }
                            else
                            {
                                DateTime thepharmReportedByDate = Convert.ToDateTime(theExistDS1.Tables[0].Rows[0]["DispensedByDate"].ToString());
                                txtpharmReportedbyDate.Value = thepharmReportedByDate.ToString(Session["AppDateFormat"].ToString());
                            }
                        }

                    }



                    theExistDS = NonARTManager.GetPatientExsistNonARTFollowUp(PatientID, theVisitPk);
                    //--- Rupesh 03-Jan-08 ends ---

                    if (theExistDS.Tables[0].Rows.Count != 0)
                    {
                        if (theExistDS.Tables[0].Rows[0]["VisitDate"] != System.DBNull.Value)
                        {
                            DateTime theTmpDt1 = Convert.ToDateTime(theExistDS.Tables[0].Rows[0]["VisitDate"]);
                            this.txtvisitDate.Value = theTmpDt1.ToString(Session["AppDateFormat"].ToString());
                            Session["VDate"] = txtvisitDate.Value;
                        }

                    }

                    if (theExistDS.Tables[1].Rows.Count != 0)
                    {
                        if (theExistDS.Tables[1].Rows[0]["Pregnant"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(theExistDS.Tables[1].Rows[0]["Pregnant"].ToString()) == 1)
                            {
                                this.rdopregnantYes.Checked = true;
                                if (theExistDS.Tables[1].Rows[0]["EDD"] != System.DBNull.Value)
                                {
                                    DateTime theTmpDt3 = Convert.ToDateTime(theExistDS.Tables[1].Rows[0]["EDD"]);
                                    this.txtEDDDate.Value = theTmpDt3.ToString(Session["AppDateFormat"].ToString());
                                }
                                string script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'pregnantyes'>\n";
                                script += "show('rdopregnantyesno');\n";
                                script += "hide('spdelivery');\n";
                                script += "show('spanEDD');\n";
                                script += "</script>\n";
                                RegisterStartupScript("pregnantyes", script);
                                ViewState["Pregstatus"] = "1";
                            }
                            else if (Convert.ToInt32(theExistDS.Tables[1].Rows[0]["Pregnant"].ToString()) == 0)
                            {
                                this.rdopregnantNo.Checked = true;
                                string script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'pregnantno'>\n";
                                script += "show('rdopregnantyesno');\n";
                                script += "hide('spdelivery');\n";
                                script += "</script>\n";
                                RegisterStartupScript("pregnantno", script);
                                ViewState["Pregstatus"] = "2";
                            }
                        }

                        if (theExistDS.Tables[1].Rows[0]["Delivered"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(theExistDS.Tables[1].Rows[0]["Delivered"]) == 0)
                            {
                                this.rdoDeliveredNo.Checked = true;
                                if (theExistDS.Tables[1].Rows[0]["EDD"] != System.DBNull.Value)
                                {
                                    DateTime theTmpDt3 = Convert.ToDateTime(theExistDS.Tables[1].Rows[0]["EDD"]);
                                    this.txtEDDDate.Value = theTmpDt3.ToString(Session["AppDateFormat"].ToString());
                                }
                                string script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'deliverno'>\n";
                                script += "hide('rdopregnantyesno');\n";
                                script += "show('spdelivery');\n";
                                script += "show('spanEDD');\n";
                                script += "</script>\n";
                                RegisterStartupScript("deliverno", script);
                                ViewState["Pregstatus"] = "3";
                            }

                            else if (Convert.ToInt32(theExistDS.Tables[1].Rows[0]["Delivered"]) == 1)
                            {
                                this.rdoDeliveredYes.Checked = true;
                                if (theExistDS.Tables[1].Rows[0]["DateofDelivery"] != System.DBNull.Value)
                                {
                                    DateTime theTmpDt3 = Convert.ToDateTime(theExistDS.Tables[1].Rows[0]["DateofDelivery"]);
                                    this.txtDeliDate.Value = theTmpDt3.ToString(Session["AppDateFormat"].ToString());
                                }
                                string script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'deliveryes'>\n";
                                script += "hide('rdopregnantyesno');\n";
                                script += "show('spdelivery');\n";
                                script += "show('spanDelDate');\n";
                                script += "hide('spanEDD');\n";
                                script += "</script>\n";
                                RegisterStartupScript("deliveryes", script);
                                ViewState["Pregstatus"] = "4";

                            }
                        }
                        if (theExistDS.Tables[1].Rows[0]["CreateDate"] != System.DBNull.Value)
                        {
                            theCreateDate = Convert.ToDateTime(theExistDS.Tables[1].Rows[0]["CreateDate"]);
                        }

                        if (theExistDS.Tables[1].Rows[0]["LMP"] != System.DBNull.Value)
                        {
                            DateTime theTmpDt2 = Convert.ToDateTime(theExistDS.Tables[1].Rows[0]["LMP"]);
                            if (theTmpDt2 != Convert.ToDateTime(theUtils.MakeDate("01-01-1900")))
                            {
                                this.txtLMP.Value = theTmpDt2.ToString(Session["AppDateFormat"].ToString());
                            }
                        }
                    }
                    if (theExistDS.Tables[2].Rows.Count != 0)
                    {

                        /*************** Physical Exam and Detail Patient Vitals Part[Table: dtl_PatientVitals]**************/
                        if (theExistDS.Tables[2].Rows[0]["Temp"] != System.DBNull.Value)
                        {
                            this.txtphysTemp.Text = Convert.ToInt32(theExistDS.Tables[2].Rows[0]["Temp"]).ToString();
                        }
                        if (theExistDS.Tables[2].Rows[0]["RR"] != System.DBNull.Value)
                        {
                            this.txtphysRR.Text = Convert.ToInt32(theExistDS.Tables[2].Rows[0]["RR"]).ToString();
                        }
                        if (theExistDS.Tables[2].Rows[0]["HR"] != System.DBNull.Value)
                        {
                            this.txtphysHR.Text = Convert.ToInt32(theExistDS.Tables[2].Rows[0]["HR"]).ToString();
                        }
                        if (theExistDS.Tables[2].Rows[0]["BPDiastolic"] != System.DBNull.Value)
                        {
                            this.txtphysBPDiastolic.Text = Convert.ToInt32(theExistDS.Tables[2].Rows[0]["BPDiastolic"]).ToString();
                        }
                        if (theExistDS.Tables[2].Rows[0]["BPSystolic"] != System.DBNull.Value)
                        {
                            this.txtphysBPSystolic.Text = Convert.ToInt32(theExistDS.Tables[2].Rows[0]["BPSystolic"]).ToString();
                        }
                        if (theExistDS.Tables[2].Rows[0]["Height"] != System.DBNull.Value)
                        {
                            this.txtphysHeight.Text = Convert.ToInt32(theExistDS.Tables[2].Rows[0]["Height"]).ToString();
                        }
                        if (theExistDS.Tables[2].Rows[0]["Weight"] != System.DBNull.Value)
                        {
                            this.txtphysWeight.Text = Convert.ToInt32(theExistDS.Tables[2].Rows[0]["Weight"]).ToString();
                        }

                        if (((theExistDS.Tables[2].Rows[0]["Weight"].ToString() != "") && (theExistDS.Tables[2].Rows[0]["Weight"] != System.DBNull.Value)) && ((theExistDS.Tables[2].Rows[0]["Height"].ToString() != "") && (theExistDS.Tables[2].Rows[0]["Height"] != System.DBNull.Value)))
                        {
                            decimal anotherWeight = Convert.ToDecimal(theExistDS.Tables[2].Rows[0]["Weight"].ToString());
                            decimal anotherHeight = Convert.ToDecimal(theExistDS.Tables[2].Rows[0]["Height"].ToString());
                            //decimal anotherBMI = anotherWeight / ((anotherHeight / 100) * (anotherHeight / 100));
                            //anotherBMI = Math.Round(anotherBMI, 2);
                            //txtanotherbmi.Value = Convert.ToString(anotherBMI);
                            if (anotherHeight > 0)
                            {
                                decimal anotherBMI = anotherWeight / ((anotherHeight / 100) * (anotherHeight / 100));
                                anotherBMI = Math.Round(anotherBMI, 2);
                                txtanotherbmi.Value = Convert.ToString(anotherBMI);
                            }
                        }
                        if (theExistDS.Tables[2].Rows[0]["Pain"] != System.DBNull.Value)
                        {
                            this.ddlPain.Value = theExistDS.Tables[2].Rows[0]["Pain"].ToString();
                        }
                    }

                    if (theExistDS.Tables[3].Rows.Count != 0)
                    {

                        /***************** Assessment Details Table: dtl_PatientStage ********************/
                        if (theExistDS.Tables[3].Rows[0]["WABStage"] != System.DBNull.Value)
                        {
                            this.ddlphysWABStage.SelectedValue = theExistDS.Tables[3].Rows[0]["WABStage"].ToString();
                        }
                        if (theExistDS.Tables[3].Rows[0]["WHOStage"] != System.DBNull.Value)
                        {
                            this.ddlphysWHOStage.SelectedValue = theExistDS.Tables[3].Rows[0]["WHOStage"].ToString();
                        }
                    }

                    if (theExistDS.Tables[4].Rows.Count != 0)
                    {
                        /***************** Appointment Reason Deatils Table:dtl_PatientAppointment ******************/
                        if (theExistDS.Tables[4].Rows[0]["AppReason"] != System.DBNull.Value)
                        {
                            this.ddlAppReason.SelectedValue = theExistDS.Tables[4].Rows[0]["AppReason"].ToString();
                        }
                        if (theExistDS.Tables[8].Rows[0]["No_of_Days"] != System.DBNull.Value)
                        {
                            lstappPeriod.Value = theExistDS.Tables[8].Rows[0]["No_of_Days"].ToString();
                        }

                        if (theExistDS.Tables[4].Rows[0]["Appdate"] != System.DBNull.Value)
                        {
                            DateTime theDate = Convert.ToDateTime(theExistDS.Tables[4].Rows[0]["Appdate"]);
                            txtSpecifyDate.Value = theDate.ToString(Session["AppDateFormat"].ToString());
                            if (this.txtSpecifyDate.Value == "01-Jan-1900")
                            {
                                this.txtSpecifyDate.Value = "";
                                lstappPeriod.SelectedIndex = 0;
                            }

                        }
                    }



                    /**************** Presenting Complaints Details******************/

                    if (theExistDS.Tables[5].Rows.Count > 0)
                    {
                        if (theExistDS.Tables[5].Rows[0]["SymptomID"] != System.DBNull.Value)
                        {
                            for (int i = 0; i < theExistDS.Tables[5].Rows.Count; i++)
                            {
                                for (int j = 0; j < cblPresentingComplaints.Items.Count; j++)
                                {
                                    if (cblPresentingComplaints.Items[j].Value == theExistDS.Tables[5].Rows[i]["SymptomID"].ToString())
                                    {
                                        cblPresentingComplaints.Items[j].Selected = true;
                                        chkpresentingComplaintsNone.Checked = false;
                                        string script = "";
                                        script = "<script language = 'javascript' defer ='defer' id = 'presentingComplaints'>\n";
                                        script += "document.getElementById('" + chkpresentingComplaintsNonehidden.ClientID + "').click();\n";
                                        script += "</script>\n";
                                        RegisterStartupScript("presentingComplaints", script);
                                    }
                                }
                            }
                        }
                    }


                    if (theExistDS.Tables[6].Rows.Count != 0)
                    {
                        /************************  grdHivAssoConditionleft Details ***********************/
                        for (int i = 0; i < theExistDS.Tables[6].Rows.Count; i++)
                        {
                            if (theExistDS.Tables[6].Rows[i]["Disease_Pk"].ToString() == "99")
                            {
                                rdoHIVassocNone.Checked = true;
                            }
                            else if (theExistDS.Tables[6].Rows[i]["Disease_Pk"].ToString() == "98")
                            {
                                PrevHIVassocNotDocumented.Checked = true;
                            }

                            foreach (HtmlTableRow r in tblOIsAIDsleft.Rows)
                            {
                                foreach (HtmlTableCell c in r.Cells)
                                {
                                    foreach (Control ct in c.Controls)
                                    {
                                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                        {
                                            foreach (DataRow dr in theDTHivAssoConditionleft.Rows)
                                            {
                                                if (((HtmlInputCheckBox)ct).Value == dr[1].ToString())
                                                {
                                                    if (dr[0].ToString() == theExistDS.Tables[6].Rows[i]["Disease_Pk"].ToString())
                                                    {
                                                        if (dr[1].ToString() == "Pulmonary TB")
                                                        {
                                                            rdoHIVassociate.Checked = true;
                                                            ((HtmlInputCheckBox)ct).Checked = true;
                                                            string script = "";
                                                            script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID'>\n";
                                                            script += "show('assocSelected');\n";
                                                            script += "show('pultb');\n";
                                                            script += "</script>\n";
                                                            RegisterStartupScript("cblOIsAIDs_ID", script);
                                                        }
                                                        else
                                                        {
                                                            rdoHIVassociate.Checked = true;
                                                            ((HtmlInputCheckBox)ct).Checked = true;
                                                            string script = "";
                                                            script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID'>\n";
                                                            script += "show('assocSelected');\n";
                                                            script += "</script>\n";
                                                            RegisterStartupScript("cblOIsAIDs_ID", script);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                                        {
                                            foreach (DataRow dr in theDTHivAssoConditionleft.Rows)
                                            {
                                                if (((HtmlInputRadioButton)ct).Value == dr[1].ToString())
                                                {
                                                    if (dr[0].ToString() == theExistDS.Tables[6].Rows[i]["Disease_Pk"].ToString())
                                                    {
                                                        rdoHIVassociate.Checked = true;
                                                        ((HtmlInputRadioButton)ct).Checked = true;
                                                        string script = "";
                                                        script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID1'>\n";
                                                        script += "show('assocSelected');\n";
                                                        script += "show('pultb');\n";
                                                        script += "</script>\n";
                                                        RegisterStartupScript("cblOIsAIDs_ID1", script);
                                                    }

                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }

                        string OI_AIDsOther = "";
                        for (int j = 0; j < theExistDS.Tables[6].Rows.Count; j++)
                        {
                            foreach (HtmlTableRow r in tblOIsAIDsright.Rows)
                            {
                                foreach (HtmlTableCell c in r.Cells)
                                {
                                    foreach (Control ct in c.Controls)
                                    {
                                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                        {
                                            foreach (DataRow drright in theDTHivAssoConditionright.Rows)
                                            {
                                                if (((HtmlInputCheckBox)ct).Value == drright[1].ToString())
                                                {
                                                    if (drright[0].ToString() == theExistDS.Tables[6].Rows[j]["Disease_Pk"].ToString())
                                                    {
                                                        if (drright[1].ToString() == "Other")
                                                        {
                                                            OI_AIDsOther = theExistDS.Tables[6].Rows[j]["DiseaseDesc"].ToString();
                                                            rdoHIVassociate.Checked = true;
                                                            ((HtmlInputCheckBox)ct).Checked = true;
                                                            string script = "";
                                                            script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID2'>\n";
                                                            script += "show('assocSelected');\n";
                                                            script += "show('otherOIsAIDs');\n";
                                                            script += "</script>\n";
                                                            RegisterStartupScript("cblOIsAIDs_ID2", script);
                                                        }
                                                        else
                                                        {
                                                            rdoHIVassociate.Checked = true;
                                                            ((HtmlInputCheckBox)ct).Checked = true;
                                                            string script = "";
                                                            script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID3'>\n";
                                                            script += "show('assocSelected');\n";
                                                            script += "</script>\n";
                                                            RegisterStartupScript("cblOIsAIDs_ID3", script);

                                                        }
                                                    }
                                                }
                                            }

                                        }
                                        else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                                        {
                                            if (OI_AIDsOther != "")
                                            {
                                                ((HtmlInputText)ct).Value = OI_AIDsOther;
                                                string script = "";
                                                script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID4'>\n";
                                                script += "show('assocSelected');\n";
                                                script += "show('otherOIsAIDs');\n";
                                                script += "</script>\n";
                                                RegisterStartupScript("cblOIsAIDs_ID4", script);
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }


                    if (theExistDS.Tables[7].Rows.Count != 0)
                    {
                        /**********************  Assessment Details ***************************/

                        for (int i = 0; i < theExistDS.Tables[7].Rows.Count; i++)
                        {
                            for (int j = 0; j < chkAssessment.Items.Count; j++)
                            {
                                if (chkAssessment.Items[j].Value == theExistDS.Tables[7].Rows[i]["AssessmentID"].ToString())
                                {
                                    chkAssessment.Items[j].Selected = true;
                                }

                                if (Convert.ToInt32(theExistDS.Tables[7].Rows[i]["AssessmentID"].ToString()) == 12)
                                {
                                    this.rdoartAssessmentID.Checked = true;
                                }
                                else
                                {
                                    this.rdoartAssessmentID.Checked = false;
                                }

                                if (Convert.ToInt32(theExistDS.Tables[7].Rows[i]["AssessmentID"].ToString()) == 13)
                                {
                                    this.rdoartAssessmentID2.Checked = true;
                                }
                                else
                                {
                                    this.rdoartAssessmentID2.Checked = false;
                                }
                            }
                        }

                    }
                    if (theExistDS.Tables[9].Rows.Count > 0)
                    {
                        txtClinicalNotes.Text = Convert.ToString(theExistDS.Tables[9].Rows[0]["ClinicalNotes"]);
                    }

                    #region "20-jun-07 - 1"
                    //lstappPeriod.Value = theExistDS.Tables[8].Rows[0]["No_of_Days"].ToString();
                    #endregion

                    /*   if (theExistDS1.Tables[1].Rows.Count != 0)
                        {
                            if (theExistDS1.Tables[1].Rows[0]["DataQuality"] != System.DBNull.Value && Convert.ToInt32(theExistDS1.Tables[1].Rows[0]["DataQuality"]) == 1)
                            {
                                //btncomplete.ControlStyle.BackColor = System.Drawing.Color.LightGreen;
                                btnQualityCheck.CssClass = "greenbutton";
                            }
                        }*/

                    //TB Symptoms
                    if (theExistDS.Tables[5].Rows.Count > 0)
                    {
                        if (theExistDS.Tables[5].Rows[0]["SymptomID"] != System.DBNull.Value)
                        {
                            for (int i = 0; i < theExistDS.Tables[5].Rows.Count; i++)
                            {
                                for (int j = 0; j < cblTBScreen.Items.Count; j++)
                                {
                                    if (cblTBScreen.Items[j].Value == theExistDS.Tables[5].Rows[i]["SymptomID"].ToString())
                                    {
                                        cblTBScreen.Items[j].Selected = true;
                                        rdoPerformed.Checked = true;
                                        string script = "";
                                        script = "<script language = 'javascript' defer ='defer' id = 'TBSymptoms'>\n";
                                        script += "show('TBSymptom');\n";
                                        script += "</script>\n";
                                        RegisterStartupScript("TBSymptoms", script);
                                    }
                                }
                                if (theExistDS.Tables[5].Rows[i]["SymptomID"].ToString() == "70")
                                {
                                    rdoPerformed.Checked = true;
                                    string script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'TBSymptoms1'>\n";
                                    script += "show('TBSymptom');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("TBSymptoms1", script);
                                }
                                else if (theExistDS.Tables[5].Rows[i]["SymptomID"].ToString() == "71")
                                {
                                    rdoNotDocumented.Checked = true;
                                    string script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'TBSymptoms2'>\n";
                                    script += "hide('TBSymptom');\n";
                                    script += "</script>\n";
                                    RegisterStartupScript("TBSymptoms2", script);
                                }
                                //else if (theExistDS.Tables[5].Rows[i]["SymptomID"].ToString() == "78")
                                //{
                                //    rdoTBNone.Checked = true;
                                //    string script = "";
                                //    script = "<script language = 'javascript' defer ='defer' id = 'TBSymptoms3'>\n";
                                //    script += "hide('TBSymptom');\n";
                                //    script += "</script>\n";
                                //    RegisterStartupScript("TBSymptoms3", script);
                                //}

                            }
                        }

                    }

                }

            }
        }
        catch
        {
            throw;
        }
        finally
        {
            NonARTManager = null;
        }
    }

    /******* Function To Fill All DropDownList ********/
    private void FillDropDownList(DataSet theDS)
    {
        INonARTFollowUp NonARTFollowUpManager;
        NonARTFollowUpManager = (INonARTFollowUp)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNonARTFollowUp, BusinessProcess.Clinical");
        BindFunctions theBindMgr = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        DataTable theDT = new DataTable();
        DataSet theDSXML = new DataSet();
        theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
        //if (Request.QueryString["name"] == "Add")
        if (Convert.ToInt32(Session["PatientVisitId"]) < 1)
        {
            DataTable theDTComplaints = new DataTable();
            DataView theDVComplaints = new DataView(theDSXML.Tables["Mst_Symptom"]);
            theDVComplaints.RowFilter = "DeleteFlag='0' and CategoryID='4'";
            if (theDVComplaints.Table != null)
            {
                theDTComplaints = (DataTable)theUtils.CreateTableFromDataView(theDVComplaints);
                theBindMgr.BindCheckedList(cblPresentingComplaints, theDTComplaints, "Name", "ID");
                theDVComplaints.Dispose();
                theDTComplaints.Clear();
            }

            DataTable theDTTBScreening = new DataTable();
            DataView theDVTBScreening = new DataView(theDSXML.Tables["Mst_Symptom"]);
            theDVTBScreening.RowFilter = "DeleteFlag='0' and CategoryID='14' and ID NOT IN('70', '71')";
            if (theDVTBScreening.Table != null)
            {
                theDTTBScreening = (DataTable)theUtils.CreateTableFromDataView(theDVTBScreening);
                theBindMgr.BindCheckedList(cblTBScreen, theDTTBScreening, "Name", "ID");
                theDVTBScreening.Dispose();
                theDTTBScreening.Clear();
            }
            if (theDSXML.Tables["Mst_Employee"] != null)
            {
                DataView theDVOrder = new DataView(theDSXML.Tables["Mst_Employee"]);
                theDVOrder.RowFilter = "DeleteFlag=0";
                if (theDVOrder.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDVOrder);
                    if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                    {
                        theDVOrder = new DataView(theDT);
                        theDVOrder.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
                        if (theDVOrder.Count > 0)
                            theDT = theUtils.CreateTableFromDataView(theDVOrder);
                    }
                    /* Bug ID 2707
                    theBindMgr.BindCombo(ddlPharmOrderedbyName, theDT, "EmployeeName", "EmployeeId");
                    theBindMgr.BindCombo(ddlPharmReportedbyName, theDT, "EmployeeName", "EmployeeId");
                    theBindMgr.BindCombo(ddlCounselerName, theDT, "EmployeeName", "EmployeeId");*/

                    //BindUserDropdown(ddlPharmOrderedbyName, string.Empty);
                    //BindUserDropdown(ddlPharmReportedbyName, string.Empty);
                    //BindUserDropdown(ddlCounselerName, string.Empty);

                    BindUserDropdown(ddlPharmOrderedbyName, Session["AppUserId"].ToString());
                    BindUserDropdown(ddlPharmReportedbyName, Session["AppUserId"].ToString());
                    BindUserDropdown(ddlCounselerName, Session["AppUserId"].ToString());

                    theDVOrder.Dispose();
                    theDT.Clear();
                }
            }
            DataView theDVAppReason = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDVAppReason.RowFilter = "DeleteFlag=0 and codeId=26";
            DataTable theDTAppReason = new DataTable();
            if (theDVAppReason.Table != null)
            {
                theDTAppReason = (DataTable)theUtils.CreateTableFromDataView(theDVAppReason);
                theBindMgr.BindCombo(ddlAppReason, theDTAppReason, "Name", "ID");
                theDVAppReason.Dispose();
                theDTAppReason.Clear();
            }

            DataView theDVAssessment = new DataView(theDSXML.Tables["Mst_Assessment"]);
            theDVAssessment.RowFilter = "DeleteFlag='0' and AssessmentCategoryId='2'";
            DataTable theDTAssessment = new DataTable();
            if (theDVAssessment.Table != null)
            {
                theDTAssessment = theUtils.CreateTableFromDataView(theDVAssessment);
                theBindMgr.BindCheckedList(chkAssessment, theDTAssessment, "AssessmentName", "AssessmentID");
                theDVAssessment.Dispose();
                theDTAssessment.Clear();
            }

            DataView theDVHivAssoConditionleft = new DataView();
            //DataTable theDTHivAssoConditionleft = new DataTable();
            theDVHivAssoConditionleft = new DataView(theDS.Tables[10]);
            theDVHivAssoConditionleft.RowFilter = "DeleteFlag=0";
            if (theDVHivAssoConditionleft.Table != null)
            {
                theDTHivAssoConditionleft = theUtils.CreateTableFromDataView(theDVHivAssoConditionleft);
                for (int i = 0; i < theDTHivAssoConditionleft.Rows.Count; i++)
                {
                    HtmlTableRow r = new HtmlTableRow();
                    if ((i != 1) && (i != 2))
                    {
                        HtmlTableCell c = new HtmlTableCell();
                        HtmlInputCheckBox chkOIsDsleft = new HtmlInputCheckBox();
                        chkOIsDsleft.ID = Convert.ToString(rcol_left);
                        chkOIsDsleft.Value = theDTHivAssoConditionleft.Rows[i][1].ToString();
                        c.Controls.Add(chkOIsDsleft);
                        c.Controls.Add(new LiteralControl(chkOIsDsleft.Value));
                        r.Cells.Add(c);
                        if (chkOIsDsleft.Value == "Pulmonary TB")
                        {
                            chkOIsDsleft.Attributes.Add("onclick", "toggle('pultb');");
                        }
                        rcol_left++;
                    }
                    if (i == 0)
                    {
                        HtmlTableCell d = new HtmlTableCell();
                        d.Controls.Add(new LiteralControl("<div id='pultb' style='display:none'>"));
                        HtmlInputRadioButton rdopultbsmplus = new HtmlInputRadioButton();
                        rdopultbsmplus.ID = Convert.ToString(11);
                        rdopultbsmplus.Value = theDTHivAssoConditionleft.Rows[1][1].ToString();
                        rdopultbsmplus.Attributes.Add("onfocus", "up(this);");
                        rdopultbsmplus.Attributes.Add("onclick", "down(this);");
                        d.Controls.Add(rdopultbsmplus);
                        d.Controls.Add(new LiteralControl(rdopultbsmplus.Value));

                        d.Controls.Add(new LiteralControl("</br>"));

                        HtmlInputRadioButton rdopultbsminus = new HtmlInputRadioButton();
                        rdopultbsminus.ID = Convert.ToString(12);
                        rdopultbsminus.Value = theDTHivAssoConditionleft.Rows[2][1].ToString();
                        rdopultbsminus.Attributes.Add("onfocus", "up(this);");
                        rdopultbsminus.Attributes.Add("onclick", "down(this);");
                        d.Controls.Add(rdopultbsminus);
                        d.Controls.Add(new LiteralControl(rdopultbsminus.Value));
                        d.Controls.Add(new LiteralControl("</div>"));
                        r.Cells.Add(d);
                    }
                    tblOIsAIDsleft.Rows.Add(r);
                }
            }

            DataView theDVHivAssoConditionright = new DataView();
            theDVHivAssoConditionright = new DataView(theDS.Tables[11]);
            theDVHivAssoConditionright.RowFilter = "DeleteFlag=0";
            //DataTable theDTHivAssoConditionright= new DataTable();
            if (theDVHivAssoConditionright.Table != null)
            {
                theDTHivAssoConditionright = theUtils.CreateTableFromDataView(theDVHivAssoConditionright);
                for (int i = 0; i < theDTHivAssoConditionright.Rows.Count; i++)
                {
                    HtmlTableRow trright = new HtmlTableRow();
                    HtmlTableCell tc2_0 = new HtmlTableCell();
                    HtmlInputCheckBox chkOIsDsright = new HtmlInputCheckBox();
                    chkOIsDsright.ID = Convert.ToString("right" + rcol_right);
                    chkOIsDsright.Value = theDTHivAssoConditionright.Rows[i][1].ToString();
                    tc2_0.Controls.Add(chkOIsDsright);
                    tc2_0.Controls.Add(new LiteralControl(chkOIsDsright.Value));
                    trright.Cells.Add(tc2_0);

                    if (chkOIsDsright.Value == "Other")
                    {
                        HtmlTableCell tc2_1 = new HtmlTableCell();
                        //tc2_1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' for='otherOIsAIDs'>"));
                        tc2_1.Controls.Add(new LiteralControl("<SPAN id='otherOIsAIDs' style='DISPLAY:none'>Specify: "));
                        HtmlInputText HTextOIsAIDsillness = new HtmlInputText();
                        HTextOIsAIDsillness.ID = "txtotherOIsAIDsillness";
                        HTextOIsAIDsillness.Size = 10;
                        tc2_1.Controls.Add(HTextOIsAIDsillness);
                        tc2_1.Controls.Add(new LiteralControl(HTextOIsAIDsillness.Value));
                        tc2_1.Controls.Add(new LiteralControl("</SPAN>"));
                        trright.Cells.Add(tc2_1);
                        chkOIsDsright.Attributes.Add("onclick", "toggle('otherOIsAIDs');");
                    }
                    rcol_right++;
                    tblOIsAIDsright.Rows.Add(trright);
                }
            }

            int WHOStage = 0;

            if (theDS.Tables[20].Rows.Count > 0)
                if (theDS.Tables[20].Rows[0]["WHOStage"] != System.DBNull.Value)
                {
                    WHOStage = Convert.ToInt32(theDS.Tables[20].Rows[0]["WHOStage"]);
                }
            int WhoStageSRNO = 0;
            DataView theDV = new DataView(theDSXML.Tables["mst_Decode"]);
            theDV.RowFilter = "Id=" + WHOStage.ToString();
            if (theDV.Count > 0)
            {
                if (theDV[0]["SRNO"] != System.DBNull.Value)
                {
                    WhoStageSRNO = Convert.ToInt32(theDV[0]["SRNO"]);
                }
            }
            theDV = null;

            theDV = new DataView(theDSXML.Tables["mst_Decode"]);
            theDV.RowFilter = "DeleteFlag=0 and CodeID=22 and SystemId = 1 and SRNO>=" + WhoStageSRNO.ToString();
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                theBindMgr.BindCombo(ddlphysWHOStage, theDT, "Name", "ID");
            }
            //////if (theDS.Tables[20].Rows.Count > 0)
            //////{
            //////    string strWHOState = theDS.Tables[20].Rows[0]["WHOStage"].ToString();
            //////    int WHOStage = 0;
            //////    if (strWHOState == "")
            //////    {
            //////        WHOStage = 0;
            //////    }
            //////    else
            //////    {
            //////        WHOStage = Convert.ToInt32(theDS.Tables[20].Rows[0]["WHOStage"].ToString());
            //////    }
            //////    if (WHOStage == 87 || WHOStage == 0)
            //////    {
            //////        ddlphysWHOStage.Items.Add("Select");
            //////        ddlphysWHOStage.Items.Add("I");
            //////        ddlphysWHOStage.Items.Add("II");
            //////        ddlphysWHOStage.Items.Add("III");
            //////        ddlphysWHOStage.Items.Add("IV");
            //////        ddlphysWHOStage.Items[0].Value = "0";
            //////        ddlphysWHOStage.Items[1].Value = "87";
            //////        ddlphysWHOStage.Items[2].Value = "88";
            //////        ddlphysWHOStage.Items[3].Value = "89";
            //////        ddlphysWHOStage.Items[4].Value = "90";
            //////    }
            //////    else if (WHOStage == 88)
            //////    {
            //////        ddlphysWHOStage.Items.Add("Select");
            //////        ddlphysWHOStage.Items.Add("II");
            //////        ddlphysWHOStage.Items.Add("III");
            //////        ddlphysWHOStage.Items.Add("IV");
            //////        ddlphysWHOStage.Items[0].Value = "0";
            //////        ddlphysWHOStage.Items[1].Value = "88";
            //////        ddlphysWHOStage.Items[2].Value = "89";
            //////        ddlphysWHOStage.Items[3].Value = "90";
            //////    }
            //////    else if (WHOStage == 89)
            //////    {
            //////        ddlphysWHOStage.Items.Add("Select");
            //////        ddlphysWHOStage.Items.Add("III");
            //////        ddlphysWHOStage.Items.Add("IV");
            //////        ddlphysWHOStage.Items[0].Value = "0";
            //////        ddlphysWHOStage.Items[1].Value = "89";
            //////        ddlphysWHOStage.Items[2].Value = "90";
            //////    }
            //////    else if (WHOStage == 90)
            //////    {
            //////        ddlphysWHOStage.Items.Add("Select");
            //////        ddlphysWHOStage.Items.Add("IV");
            //////        ddlphysWHOStage.Items[0].Value = "0";
            //////        ddlphysWHOStage.Items[1].Value = "90";
            //////    }

            //////}
            //////else
            //////{
            //////    ddlphysWHOStage.Items.Add("Select");
            //////    ddlphysWHOStage.Items.Add("I");
            //////    ddlphysWHOStage.Items.Add("II");
            //////    ddlphysWHOStage.Items.Add("III");
            //////    ddlphysWHOStage.Items.Add("IV");
            //////    ddlphysWHOStage.Items[0].Value = "0";
            //////    ddlphysWHOStage.Items[1].Value = "87";
            //////    ddlphysWHOStage.Items[2].Value = "88";
            //////    ddlphysWHOStage.Items[3].Value = "89";
            //////    ddlphysWHOStage.Items[4].Value = "90";
            //////}
        }
        //else if (Request.QueryString["name"] == "Edit" || Request.QueryString["name"] == "Delete")
        else if ((Convert.ToInt32(Session["PatientVisitId"])) > 1 || Request.QueryString["name"] == "Delete")
        {
            DataView theDVComplaints = new DataView(theDSXML.Tables["Mst_Symptom"]);
            theDVComplaints.RowFilter = "CategoryID=4";
            DataTable theDTComplaints = new DataTable();
            if (theDVComplaints.Table != null)
            {
                theDTComplaints = (DataTable)theUtils.CreateTableFromDataView(theDVComplaints);
                theBindMgr.BindCheckedList(cblPresentingComplaints, theDTComplaints, "Name", "ID");
            }

            DataTable theDTTBScreening = new DataTable();
            DataView theDVTBScreening = new DataView(theDSXML.Tables["Mst_Symptom"]);
            theDVTBScreening.RowFilter = "CategoryID='14' and ID NOT IN('70', '71')";
            if (theDVTBScreening.Table != null)
            {
                theDTTBScreening = (DataTable)theUtils.CreateTableFromDataView(theDVTBScreening);
                theBindMgr.BindCheckedList(cblTBScreen, theDTTBScreening, "Name", "ID");
                theDVTBScreening.Dispose();
                theDTTBScreening.Clear();
            }


            /**********/
            //if (theDSXML.Tables["Mst_Employee"] != null)
            //{
            //    DataView theDVOrder = new DataView(theDSXML.Tables["Mst_Employee"]);
            //    if (theDVOrder.Table != null)
            //    {
            //        theDT = (DataTable)theUtils.CreateTableFromDataView(theDVOrder);
            //        if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
            //        {
            //            theDVOrder = new DataView(theDT);
            //            theDVOrder.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
            //            if (theDVOrder.Count > 0)
            //                theDT = theUtils.CreateTableFromDataView(theDVOrder);
            //        }
            //        theBindMgr.BindCombo(ddlPharmOrderedbyName, theDT, "EmployeeName", "EmployeeId");
            //        theBindMgr.BindCombo(ddlPharmReportedbyName, theDT, "EmployeeName", "EmployeeId");
            //        theBindMgr.BindCombo(ddlCounselerName, theDT, "EmployeeName", "EmployeeId");
            //        theDVOrder.Dispose();
            //        theDT.Clear();
            //    }
            //}

            DataView theDVOrder = new DataView(theDSXML.Tables["Mst_Employee"]);
            if (theDVOrder.Table != null)
            {
                theDT = theUtils.CreateTableFromDataView(theDVOrder);
                /* Bug ID 2707.....
                theBindMgr.BindCombo(ddlPharmOrderedbyName, theDT, "EmployeeName", "EmployeeId");
                theBindMgr.BindCombo(ddlPharmReportedbyName, theDT, "EmployeeName", "EmployeeId");
                theBindMgr.BindCombo(ddlCounselerName, theDT, "EmployeeName", "EmployeeId");
                */
                //BindUserDropdown(ddlPharmOrderedbyName, string.Empty);
                //BindUserDropdown(ddlPharmReportedbyName, string.Empty);
                //BindUserDropdown(ddlCounselerName, string.Empty);

                BindUserDropdown(ddlPharmOrderedbyName, Session["AppUserId"].ToString());
                BindUserDropdown(ddlPharmReportedbyName, Session["AppUserId"].ToString());
                BindUserDropdown(ddlCounselerName, Session["AppUserId"].ToString());

                theDVOrder.Dispose();
                theDT.Clear();
            }
            /********/

            DataView theDVAssessment = new DataView(theDSXML.Tables["Mst_Assessment"]);
            theDVAssessment.RowFilter = "AssessmentCategoryId=2";
            DataTable theDTAssessment = new DataTable();
            if (theDVAssessment.Table != null)
            {
                theDTAssessment = theUtils.CreateTableFromDataView(theDVAssessment);
                if (theDVAssessment.Count > 0)
                {
                    theBindMgr.BindCheckedList(chkAssessment, theDTAssessment, "AssessmentName", "AssessmentID");
                }
            }
            /**********/
            DataView theDVAppReason = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDVAppReason.RowFilter = "codeId=26";
            DataTable theDTAppReason = new DataTable();
            if (theDVAppReason.Table != null)
            {

                if (theDVAppReason.Count > 0)
                {
                    theDTAppReason = theUtils.CreateTableFromDataView(theDVAppReason);
                    theBindMgr.BindCombo(ddlAppReason, theDTAppReason, "Name", "ID");
                }
            }

            ///********************* Function To Fill OI and AIDs Defining Illness ********************////
            ///Left
            theDTHivAssoConditionleft = theDS.Tables[10];
            for (int i = 0; i < theDTHivAssoConditionleft.Rows.Count; i++)
            {

                HtmlTableRow r = new HtmlTableRow();
                if ((i != 1) && (i != 2))
                {
                    HtmlTableCell c = new HtmlTableCell();
                    HtmlInputCheckBox chkOIsDsleft = new HtmlInputCheckBox();
                    chkOIsDsleft.ID = Convert.ToString(rcol_left);
                    chkOIsDsleft.Value = theDTHivAssoConditionleft.Rows[i][1].ToString();
                    c.Controls.Add(chkOIsDsleft);
                    c.Controls.Add(new LiteralControl(chkOIsDsleft.Value));
                    r.Cells.Add(c);
                    if (chkOIsDsleft.Value == "Pulmonary TB")
                    {
                        chkOIsDsleft.Attributes.Add("onclick", "toggle('pultb');");
                    }
                    rcol_left++;
                }
                if (i == 0)
                {
                    HtmlTableCell d = new HtmlTableCell();
                    d.Controls.Add(new LiteralControl("<div id='pultb' style='display:none'>"));
                    HtmlInputRadioButton rdopultbsmplus = new HtmlInputRadioButton();
                    rdopultbsmplus.ID = Convert.ToString(11); ;
                    rdopultbsmplus.Value = theDTHivAssoConditionleft.Rows[1][1].ToString();
                    d.Controls.Add(rdopultbsmplus);
                    d.Controls.Add(new LiteralControl(rdopultbsmplus.Value));
                    d.Controls.Add(new LiteralControl("</br>"));
                    HtmlInputRadioButton rdopultbsminus = new HtmlInputRadioButton();
                    rdopultbsminus.ID = Convert.ToString(12); ;
                    rdopultbsminus.Value = theDTHivAssoConditionleft.Rows[2][1].ToString();
                    d.Controls.Add(rdopultbsminus);
                    d.Controls.Add(new LiteralControl(rdopultbsminus.Value));
                    d.Controls.Add(new LiteralControl("</div>"));
                    r.Cells.Add(d);
                }
                tblOIsAIDsleft.Rows.Add(r);
            }

            //Coding for Saving HIV Associated Conditions - right
            theDTHivAssoConditionright = theDS.Tables[11];
            for (int i = 0; i < theDTHivAssoConditionright.Rows.Count; i++)
            {
                HtmlTableRow trright = new HtmlTableRow();
                HtmlTableCell tc2_0 = new HtmlTableCell();
                HtmlInputCheckBox chkOIsDsright = new HtmlInputCheckBox();
                chkOIsDsright.ID = Convert.ToString("right" + rcol_right);
                chkOIsDsright.Value = theDTHivAssoConditionright.Rows[i][1].ToString();
                tc2_0.Controls.Add(chkOIsDsright);
                tc2_0.Controls.Add(new LiteralControl(chkOIsDsright.Value));
                trright.Cells.Add(tc2_0);

                if (chkOIsDsright.Value == "Other")
                {
                    HtmlTableCell tc2_1 = new HtmlTableCell();
                    //tc2_1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' for='otherOIsAIDs'>"));
                    tc2_1.Controls.Add(new LiteralControl("<SPAN id='otherOIsAIDs' style='DISPLAY:none'>Specify: "));
                    HtmlInputText HTextOIsAIDsillness = new HtmlInputText();
                    HTextOIsAIDsillness.ID = "txtotherOIsAIDsillness";
                    HTextOIsAIDsillness.Size = 10;
                    tc2_1.Controls.Add(HTextOIsAIDsillness);
                    tc2_1.Controls.Add(new LiteralControl(HTextOIsAIDsillness.Value));
                    tc2_1.Controls.Add(new LiteralControl("</SPAN>"));
                    trright.Cells.Add(tc2_1);
                    chkOIsDsright.Attributes.Add("onclick", "toggle('otherOIsAIDs');");
                }
                rcol_right++;
                tblOIsAIDsright.Rows.Add(trright);
            }

            int WHOStage = 0;
            if (theDS.Tables[20].Rows.Count > 0)
                WHOStage = Convert.ToInt32(theDS.Tables[20].Rows[0]["WHOStage"]);

            int WhoStageSRNO = 0;
            DataView theDV = new DataView(theDSXML.Tables["mst_Decode"]);
            theDV.RowFilter = "Id=" + WHOStage.ToString();
            if (theDV.Count > 0)
            {
                if (theDV[0]["SRNO"] != System.DBNull.Value)
                {
                    WhoStageSRNO = Convert.ToInt32(theDV[0]["SRNO"]);
                }
            }
            theDV = null;

            theDV = new DataView(theDSXML.Tables["mst_Decode"]);
            theDV.RowFilter = "DeleteFlag=0 and CodeID=22 and SystemId = 1 and SRNO>=" + WhoStageSRNO.ToString();
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                theBindMgr.BindCombo(ddlphysWHOStage, theDT, "Name", "ID");
            }

            //////////ddlphysWHOStage.Items.Add("Select");
            //////////ddlphysWHOStage.Items.Add("I");
            //////////ddlphysWHOStage.Items.Add("II");
            //////////ddlphysWHOStage.Items.Add("III");
            //////////ddlphysWHOStage.Items.Add("IV");
            //////////ddlphysWHOStage.Items[0].Value = "0";
            //////////ddlphysWHOStage.Items[1].Value = "87";
            //////////ddlphysWHOStage.Items[2].Value = "88";
            //////////ddlphysWHOStage.Items[3].Value = "89";
            //////////ddlphysWHOStage.Items[4].Value = "90";

            //////////if (theDS.Tables[20].Rows[0]["WHOStage"] != System.DBNull.Value)
            //////////{
            //////////    int WHOStage = Convert.ToInt32(theDS.Tables[20].Rows[0]["WHOStage"]);
            //////////    ddlphysWHOStage.Items.Clear();
            //////////    if (WHOStage == 87 || WHOStage == 0)
            //////////    {
            //////////        ddlphysWHOStage.Items.Add("Select");
            //////////        ddlphysWHOStage.Items.Add("I");
            //////////        ddlphysWHOStage.Items.Add("II");
            //////////        ddlphysWHOStage.Items.Add("III");
            //////////        ddlphysWHOStage.Items.Add("IV");
            //////////        ddlphysWHOStage.Items[0].Value = "0";
            //////////        ddlphysWHOStage.Items[1].Value = "87";
            //////////        ddlphysWHOStage.Items[2].Value = "88";
            //////////        ddlphysWHOStage.Items[3].Value = "89";
            //////////        ddlphysWHOStage.Items[4].Value = "90";
            //////////    }
            //////////    if (WHOStage == 88)
            //////////    {

            //////////        ddlphysWHOStage.Items.Add("Select");
            //////////        ddlphysWHOStage.Items.Add("II");
            //////////        ddlphysWHOStage.Items.Add("III");
            //////////        ddlphysWHOStage.Items.Add("IV");
            //////////        ddlphysWHOStage.Items[0].Value = "0";
            //////////        ddlphysWHOStage.Items[1].Value = "88";
            //////////        ddlphysWHOStage.Items[2].Value = "89";
            //////////        ddlphysWHOStage.Items[3].Value = "90";
            //////////    }
            //////////    else if (WHOStage == 89)
            //////////    {

            //////////        ddlphysWHOStage.Items.Add("Select");
            //////////        ddlphysWHOStage.Items.Add("III");
            //////////        ddlphysWHOStage.Items.Add("IV");
            //////////        ddlphysWHOStage.Items[0].Value = "0";
            //////////        ddlphysWHOStage.Items[1].Value = "89";
            //////////        ddlphysWHOStage.Items[2].Value = "90";
            //////////    }
            //////////    else if (WHOStage == 90)
            //////////    {

            //////////        ddlphysWHOStage.Items.Add("Select");
            //////////        ddlphysWHOStage.Items.Add("IV");
            //////////        ddlphysWHOStage.Items[0].Value = "0";
            //////////        ddlphysWHOStage.Items[1].Value = "90";
            //////////    }

            //////////}
        }
    }

    /************** Boundary Values  ***************/
    private void AddFieldAttributes()
    {
        //txtvisitDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtvisitDate.ClientID + "', '" + txtvisitDate.ClientID + "'), addDays()");
        txtvisitDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDate('" + Session["AppCurrentDate"] + "', '" + txtvisitDate.ClientID + "', '" + txtvisitDate.ClientID + "'), addDays(), SendCodeName()");
        txtvisitDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");

        txtLMP.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtLMP.ClientID + "', '" + "LMP" + "', '" + txtLMP.ClientID + "')");
        txtLMP.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");

        txtSpecifyDate.Attributes.Add("onKeyup", "DateFormat(this,this.value,event,false,'3')");
        txtSpecifyDate.Attributes.Add("onBlur", "DateFormat(this,this.value,event,true,'3')");

        txtpharmOrderedbyDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
        txtpharmOrderedbyDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");

        txtpharmReportedbyDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
        txtpharmReportedbyDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");

        txtEDDDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
        txtEDDDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");

        txtDeliDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
        txtDeliDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");

        NonARTManager = (INonARTFollowUp)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNonARTFollowUp, BusinessProcess.Clinical");
        DataSet theBoundryValueDS = (DataSet)NonARTManager.GetNonARTBoundaryValues();

        if (theBoundryValueDS.Tables.Count != 0)
        {
            if (theBoundryValueDS.Tables[3].Rows.Count != 0)
            {
                DataView theDV = new DataView(theBoundryValueDS.Tables[3]);

                theDV.RowFilter = "SubTestId = 40";
                if (theDV.Count != 0)
                {
                    txtphysTemp.Attributes.Add("onkeyup", "chkDecimal('" + txtphysTemp.ClientID + "'); AddBoundary('" + txtphysTemp.ClientID + "','" + Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "','" + Convert.ToInt32(theDV[0]["MaxBoundaryValue"]) + "')");
                    txtphysTemp.Attributes.Add("onblur", "CheckValue('" + txtphysTemp.ClientID + "','" + Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "')");
                }
                /******************************************/

                txtphysRR.Attributes.Add("onkeyup", "chkInteger('" + txtphysRR.ClientID + "')");
                txtphysRR.Attributes.Add("onBlur", "isBetween('" + txtphysRR.ClientID + "', '" + "RR" + "', '" + 4 + "', '" + 100 + "')");
                /******************************************/


                theDV.RowFilter = "SubTestId = 41";
                if (theDV.Count != 0)
                {
                    txtphysHR.Attributes.Add("onkeyup", "chkInteger('" + txtphysHR.ClientID + "')");// AddBoundary('" + txtphysHR.ClientID + "')" );//+ Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "','" + Convert.ToInt32(theDV[0]["MaxBoundaryValue"]) + "')");
                    txtphysHR.Attributes.Add("onblur", "isBetween('" + txtphysHR.ClientID + "','" + "HR" + "', '" + Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "','" + Convert.ToInt32(theDV[0]["MaxBoundaryValue"]) + "')");
                }

                theDV.RowFilter = "SubTestId = 42";
                if (theDV.Count != 0)
                {
                    txtphysRR.Attributes.Add("onkeyup", "chkInteger('" + txtphysRR.ClientID + "')"); //AddBoundary('" + txtphysRR.ClientID + "')");// + Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "','" + Convert.ToInt32(theDV[0]["MaxBoundaryValue"]) + "')");
                    txtphysRR.Attributes.Add("onblur", "isBetween('" + txtphysRR.ClientID + "','" + "RR" + "','" + Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "','" + Convert.ToInt32(theDV[0]["MaxBoundaryValue"]) + "')");
                }

                theDV.RowFilter = "SubTestId = 43";
                if (theDV.Count != 0)
                {
                    txtphysBPSystolic.Attributes.Add("onkeyup", "chkInteger('" + txtphysBPSystolic.ClientID + "')"); //AddBoundary('" + txtphysBPSystolic.ClientID + "','" + Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "','" + Convert.ToInt32(theDV[0]["MaxBoundaryValue"]) + "')");
                    txtphysBPSystolic.Attributes.Add("onblur", "isBetween('" + txtphysBPSystolic.ClientID + "', '" + "physBPSystolic" + "','" + Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "','" + Convert.ToInt32(theDV[0]["MaxBoundaryValue"]) + "')");
                }

                theDV.RowFilter = "SubTestId = 48";
                if (theDV.Count != 0)
                {
                    txtphysBPDiastolic.Attributes.Add("onkeyup", "chkInteger('" + txtphysBPDiastolic.ClientID + "')"); // AddBoundary('" + txtphysBPSystolic.ClientID + "','" + Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "','" + Convert.ToInt32(theDV[0]["MaxBoundaryValue"]) + "')");
                    txtphysBPDiastolic.Attributes.Add("onblur", "isBetween('" + txtphysBPDiastolic.ClientID + "', '" + "physBPDiastolic" + "','" + Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "','" + Convert.ToInt32(theDV[0]["MaxBoundaryValue"]) + "')");
                }

                theDV.RowFilter = "SubTestId = 44";
                if (theDV.Count != 0)
                {
                    txtphysWeight.Attributes.Add("onkeyup", "chkDecimal('" + txtphysWeight.ClientID + "');AddBoundary('" + txtphysWeight.ClientID + "','" + Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "','" + Convert.ToInt32(theDV[0]["MaxBoundaryValue"]) + "')");
                    txtphysWeight.Attributes.Add("onblur", "CheckValue('" + txtphysWeight.ClientID + "','" + Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "');CalcualteBMI('" + txtanotherbmi.ClientID + "','" + txtphysWeight.ClientID + "','" + txtphysHeight.ClientID + "');");
                }

                theDV.RowFilter = "SubTestId = 45";
                if (theDV.Count != 0)
                {
                    txtphysHeight.Attributes.Add("onkeyup", "chkInteger('" + txtphysHeight.ClientID + "')"); //AddBoundary('" + txtphysHeight.ClientID + "','" + Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "','" + Convert.ToInt32(theDV[0]["MaxBoundaryValue"]) + "')");
                    txtphysHeight.Attributes.Add("onblur", "isBetween('" + txtphysHeight.ClientID + "','" + "physHeight" + "' ,'" + Convert.ToInt32(theDV[0]["MinBoundaryValue"]) + "','" + Convert.ToInt32(theDV[0]["MaxBoundaryValue"]) + "');CalcualteBMI('" + txtanotherbmi.ClientID + "','" + txtphysWeight.ClientID + "','" + txtphysHeight.ClientID + "');");
                }
            }
        }
    }

    /************* Hide InHide Function ************/
    private void CheckHideProperty()
    {
        chkpresentingComplaintsNone.Attributes.Add("OnClick", "display_chklist('" + chkpresentingComplaintsNone.ClientID + "', '" + presentingComplaintsShow.ClientID + "'); disableListItems('" + cblPresentingComplaints.ClientID + "', '" + cblPresentingComplaints.Items.Count + "') ");
        chkpresentingComplaintsNonehidden.Attributes.Add("OnClick", "display_chklist('" + chkpresentingComplaintsNonehidden.ClientID + "', '" + presentingComplaintsShow.ClientID + "')");
        rdoHIVassocNone.Attributes.Add("onclick", "down(this); hide('pultb'); hide('assocSelected'); hide('otherOIsAIDs'); disableChkRdoListItems_left('ctl00_IQCareContentPlaceHolder_', '" + tblOIsAIDsleft.Rows.Count + "'); disableChkRdoListItems_right('ctl00_IQCareContentPlaceHolder_right', '" + tblOIsAIDsright.Rows.Count + "');");
        PrevHIVassocNotDocumented.Attributes.Add("onclick", "down(this); hide('pultb'); hide('assocSelected'); hide('otherOIsAIDs'); disableChkRdoListItems_left('ctl00_IQCareContentPlaceHolder_', '" + tblOIsAIDsleft.Rows.Count + "'); disableChkRdoListItems_right('ctl00_IQCareContentPlaceHolder_right', '" + tblOIsAIDsright.Rows.Count + "');");
        rdoPerformed.Attributes.Add("onclick", "toggle('TBSymptom'); down(this); disableChkboxList('" + cblTBScreen.ClientID + "', '" + cblTBScreen.Items.Count + "')");
        rdoNotDocumented.Attributes.Add("onclick", "hide('TBSymptom'); down(this); disableChkboxList('" + cblTBScreen.ClientID + "', '" + cblTBScreen.Items.Count + "')");
        //rdoTBNone.Attributes.Add("onclick", "hide('TBSymptom'); down(this); disableChkboxList('" + cblTBScreen.ClientID + "', '" + cblTBScreen.Items.Count + "')");
        //rdoARVSideEffectsNone.Attributes.Add("OnClick", "down(this);hide('sideEffectsSelected'); disableChkboxList('" + cblARVSideEffectleft.ClientID + "', '" + cblARVSideEffectleft.Items.Count + "')");
        //rdoARVSideEffectsNone.Attributes.Add("OnChange", "disableChkboxList('" + cblARVSideEffectright.ClientID + "', '" + cblARVSideEffectright.Items.Count + "')");
        //PrevHIVassocNotDocumented.Attributes.Add("OnChange", " disableListItems('" + cblHivAssoConditionRight.ClientID + "', '" + cblHivAssoConditionRight.Items.Count + "')");
        //rdoHIVassocNone.Attributes.Add("OnChange", " disableListItems('" + cblHivAssoConditionRight.ClientID + "', '" + cblHivAssoConditionRight.Items.Count + "')");


        txtpharmFluconazoleDispensed.Attributes.Add("onkeyup", "chkDecimal('" + txtpharmFluconazoleDispensed.ClientID + "')");
        txtpharmFluconazoleDuration.Attributes.Add("onkeyup", "chkDecimal('" + txtpharmFluconazoleDuration.ClientID + "')");
        txtpharmFluconazoleQty.Attributes.Add("onkeyup", "chkDecimal('" + txtpharmFluconazoleQty.ClientID + "')");
        txtpharmSulfaTMXDispensed.Attributes.Add("onkeyup", "chkPostiveInteger('" + txtpharmSulfaTMXDispensed.ClientID + "')");
        txtpharmSulfaTMXDuration.Attributes.Add("onkeyup", "chkDecimal('" + txtpharmSulfaTMXDuration.ClientID + "')");
        txtpharmSulfaTMXQty.Attributes.Add("onkeyup", "chkDecimal('" + txtpharmSulfaTMXQty.ClientID + "')");
        //txtTestResults.Attributes.Add("onkeyup", "chkNumeric('" + txtTestResults.ClientID + "')");
        txtTestResults.Attributes.Add("onkeyup", "chkInteger('" + txtTestResults.ClientID + "')");

        //txtvisitDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'),isCheckValidDate('" + txtvisitDate.ClientID + "', '" + txtvisitDate.ClientID + "')");
        txtTestResultsDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
        txtLMP.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'),isCheckValidDate('" + txtLMP.ClientID + "', '" + txtLMP.ClientID + "')");
        txtSpecifyDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
        txtpharmOrderedbyDate.Attributes.Add("onBlur", "DateFormat(this,this.value,event,true,'3')");
        txtpharmReportedbyDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");

    }

    /************ Check For IE & Visit Date *********/
    private void CheckIEVisitDate()
    {
        DataTable theMasterDT = ((DataSet)Session["MasterData"]).Tables[1];
        if (theMasterDT.Rows.Count != 0)
        {
            if (theMasterDT.Rows[0]["VisitType"] != System.DBNull.Value)
            {
                if (theMasterDT.Rows[0]["VisitType"].ToString() == "1")
                {
                    Session["VisitType"] = theMasterDT.Rows[0]["VisitType"];
                    Session["IEVisitDate"] = theMasterDT.Rows[0]["VisitDate"];
                }

            }
        }
        else
        {
            Session["VisitType"] = 0;
        }
        if (Session["VisitType"].ToString() == "0")
        {
            Check_IE();
        }

        //if (((DataTable)Session["ExistData"]).Rows.Count != 0)
        //{
        //    if (((DataTable)Session["ExistData"]).Rows[0][0] != System.DBNull.Value)
        //    {
        //        Session["ExistVisitDate"] = ((DataTable)Session["ExistData"]).Rows[0]["VisitDate"].ToString();
        //    }
        //}
    }

    /*********** Show Hide Controls ************/
    private void ShowHideControls()
    {
        if (chkpresentingComplaintsNone.Checked == false)
        {
            string scriptpresentingComplaints = "<script language = 'javascript' defer ='defer' id = 'onpresentingComplaints'>\n";
            scriptpresentingComplaints += "display_chklist('" + chkpresentingComplaintsNone.ClientID + "', '" + presentingComplaintsShow.ClientID + "');\n";
            scriptpresentingComplaints += "</script>\n";
            RegisterStartupScript("onpresentingComplaints", scriptpresentingComplaints);
        }
        if (rdoHIVassocNone.Checked == false)
        {
            if (PrevHIVassocNotDocumented.Checked == false)
            {
                string script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'AssoCondition'>\n";
                script += "show('assocSelected');\n";
                script += "</script>\n";
                RegisterStartupScript("AssoCondition", script);
            }
        }
        if (ddlPharmSignature.SelectedIndex == 3)
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'CounsellarName'>\n";
            script += "show('Adherance_counsellor_signature');\n";
            script += "</script>\n";
            RegisterStartupScript("CounsellarName", script);
        }

        if (rdoPerformed.Checked == true)
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'TBSystem'>\n";
            script += "show('TBSymptom');\n";
            script += "</script>\n";
            RegisterStartupScript("TBSystem", script);

        }

        if (Convert.ToInt32(ViewState["Pregstatus"]) == 1)
        {
            string script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'Pregnant_1'>\n";
            script += "show('rdopregnantyesno');\n";
            script += "hide('spdelivery');\n";
            script += "show('spanEDD');\n";
            script += "</script>\n";
            RegisterStartupScript("Pregnant_1", script);
        }

        if (Convert.ToInt32(ViewState["Pregstatus"]) == 2)
        {
            string script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'Pregnant_2'>\n";
            script += "show('rdopregnantyesno');\n";
            script += "hide('spdelivery');\n";
            script += "hide('spanEDD');\n";
            script += "</script>\n";
            RegisterStartupScript("Pregnant_2", script);
        }

        if (Convert.ToInt32(ViewState["Pregstatus"]) == 3)
        {
            string script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'Pregnant_3'>\n";
            script += "show('spdelivery');\n";
            script += "hide('rdopregnantyesno');\n";
            script += "show('spanEDD');\n";
            script += "</script>\n";
            RegisterStartupScript("Pregnant_3", script);
        }
        if (Convert.ToInt32(ViewState["Pregstatus"]) == 4)
        {

            string script = "";
            script = "<script language = 'javascript' defer ='defer' id = 'Pregnant_4'>\n";
            script += "show('spdelivery');\n";
            script += "show('spanDelDate');\n";
            script += "hide('rdopregnantyesno');\n";
            script += "</script>\n";
            RegisterStartupScript("Pregnant_4", script);
        }

        if (rdopregnantNo.Checked == true)
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'rdoPregnantNo'>\n";
            script += "hide('spanEDD');\n";
            script += "</script>\n";
            RegisterStartupScript("rdoPregnantNo", script);
        }

        if (rdopregnantYes.Checked == true)
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'rdoPregnantYes'>\n";
            script += "show('spanEDD');\n";
            script += "</script>\n";
            RegisterStartupScript("rdoPregnantYes", script);
        }

        if (Convert.ToInt32(Session["PatientVisitId"]) == 0 && txtvisitDate.Value != "")
        {
            IInitialEval CallBackmgr;
            try
            {
                DataSet theDS = new DataSet();
                CallBackmgr = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
                theDS = CallBackmgr.GetPregnantStatus(Convert.ToInt32(Session["PatientId"]), txtvisitDate.Value);
                if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Pregnant"]) == 1)
                {
                    string script = "<script language = 'javascript' defer ='defer' id = 'Delivery_0'>\n";
                    script += "show('spdelivery')\n";
                    script += "hide('rdopregnantyesno')\n";
                    script += "</script>\n";
                    RegisterStartupScript("Delivery_0", script);

                    if (rdoDeliveredYes.Checked == true)
                    {
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'Delivery_1'>\n";
                        script += "show('spanDelDate')\n";
                        script += "</script>\n";
                        RegisterStartupScript("Delivery_1", script);
                    }
                    if (rdoDeliveredNo.Checked == true)
                    {
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'Delivery_2'>\n";
                        script += "hide('spanDelDate')\n";
                        script += "</script>\n";
                        RegisterStartupScript("Delivery_2", script);
                    }
                }

            }
            catch
            {
            }
            finally
            {
                CallBackmgr = null;
            }

        }

    }

    public DataSet NonARTDetails()
    {
        /************ Declare Local Variable **************/

        DataSet theDS = new DataSet();
        //DataSet thDS = new DataSet();
        DataRow theDRPresentComplaints;
        DataRow theDRAssessment;
        DataRow theDRTBSymptom;
        //DataRow theDRHIVAssoDiseaseLeft;
        //DataRow theDRHIVAssoDiseaseRight;
        DataRow theDR;

        int theGeneric = 99999;
        int theDrug = 99999;
        int theStrength = 99999;
        int theFrequency = 99999;
        decimal theDuration = 99999;
        decimal thePrescribedQty = 99999;
        decimal theDispensedQty = 99999;


        /************* Coding for saving Presenting Complaints ************/

        DataTable theDTPresentComplaints = new DataTable();
        theDTPresentComplaints.Columns.Add("PresentComplaintsID", System.Type.GetType("System.Int32"));

        for (int i = 0; i < cblPresentingComplaints.Items.Count; i++)
        {
            if (cblPresentingComplaints.Items[i].Selected)
            {
                theDRPresentComplaints = theDTPresentComplaints.NewRow();
                theDRPresentComplaints["PresentComplaintsID"] = Convert.ToInt32(cblPresentingComplaints.Items[i].Value);
                theDTPresentComplaints.Rows.Add(theDRPresentComplaints);
            }
        }
        theDS.Tables.Add(theDTPresentComplaints);


        /***********Code for Saving AssessmentID -dtl_PatientAssessment ***************/

        DataTable theDTAssessment = new DataTable();
        theDTAssessment.Columns.Add("AssessmentID", System.Type.GetType("System.Int32"));

        for (int i = 0; i < chkAssessment.Items.Count; i++)
        {
            if (chkAssessment.Items[i].Selected)
            {
                theDRAssessment = theDTAssessment.NewRow();
                theDRAssessment["AssessmentID"] = Convert.ToInt32(chkAssessment.Items[i].Value);
                theDTAssessment.Rows.Add(theDRAssessment);
            }

        }
        if (rdoartAssessmentID.Checked == true)
        {
            theDRAssessment = theDTAssessment.NewRow();
            theDRAssessment["AssessmentID"] = 12;    // thDS.Tables[21].Rows[0]["AssessmentID"];                //12;    //24;
            theDTAssessment.Rows.Add(theDRAssessment);
        }
        else if (rdoartAssessmentID2.Checked == true)
        {
            theDRAssessment = theDTAssessment.NewRow();
            theDRAssessment["AssessmentID"] = 13;  // thDS.Tables[21].Rows[1]["AssessmentID"];                         ////13;       //25;
            theDTAssessment.Rows.Add(theDRAssessment);
        }
        theDS.Tables.Add(theDTAssessment);


        /*************** Code for Saving OI and AIDs Defining Illness[left]-//dtl_PatientDisease *******************/
        //Code for Saving HIV Associated Conditions[left]-//dtl_PatientDisease

        DataTable dtOI_AIDSleft = new DataTable();

        DataColumn theOI_AIDS_ID1 = new DataColumn("OI_AIDS_ID1");
        theOI_AIDS_ID1.DataType = System.Type.GetType("System.Int32");
        dtOI_AIDSleft.Columns.Add(theOI_AIDS_ID1);

        DataColumn theOI_AIDS_Desc = new DataColumn("OI_AIDS_Desc");
        theOI_AIDS_Desc.DataType = System.Type.GetType("System.String");
        dtOI_AIDSleft.Columns.Add(theOI_AIDS_Desc);

        DataColumn theDTHIVAssoDiseaseLeft = new DataColumn("HIVAssoDiseasePresent");
        theDTHIVAssoDiseaseLeft.DataType = System.Type.GetType("System.Boolean");
        dtOI_AIDSleft.Columns.Add(theDTHIVAssoDiseaseLeft);

        DataRow drOI_AIDSleft;
        string strOI_AIDSValue;

        HtmlInputCheckBox chkPulTB = new HtmlInputCheckBox();

        if (rdoHIVassocNone.Checked == true)
        {
            drOI_AIDSleft = dtOI_AIDSleft.NewRow();
            drOI_AIDSleft["OI_AIDS_ID1"] = 99;//95;
            drOI_AIDSleft["OI_AIDS_Desc"] = "Blank";
            drOI_AIDSleft["HIVAssoDiseasePresent"] = false;
            dtOI_AIDSleft.Rows.Add(drOI_AIDSleft);
            theDS.Tables.Add(dtOI_AIDSleft);
            theHIVAssocDisease = false;
        }
        else if (PrevHIVassocNotDocumented.Checked == true)
        {
            drOI_AIDSleft = dtOI_AIDSleft.NewRow();
            drOI_AIDSleft["OI_AIDS_ID1"] = 98;//94;
            drOI_AIDSleft["OI_AIDS_Desc"] = "Blank";
            drOI_AIDSleft["HIVAssoDiseasePresent"] = true;
            dtOI_AIDSleft.Rows.Add(drOI_AIDSleft);
            theDS.Tables.Add(dtOI_AIDSleft);
            theHIVAssocDisease = false;
        }
        if (rdoHIVassocNone.Checked == false && PrevHIVassocNotDocumented.Checked == false)
        {

            foreach (HtmlTableRow r in tblOIsAIDsleft.Rows)
            {
                foreach (HtmlTableCell c in r.Cells)
                {
                    foreach (Control ct in c.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Checked == true)
                            {
                                strOI_AIDSValue = ((HtmlInputCheckBox)ct).Value;
                                foreach (DataRow dr in theDTHivAssoConditionleft.Rows)
                                {
                                    if (dr[1].ToString() == strOI_AIDSValue)
                                    {
                                        if (strOI_AIDSValue == "Pulmonary TB")
                                        {
                                            chkPulTB.Checked = true;
                                            drOI_AIDSleft = dtOI_AIDSleft.NewRow();
                                            drOI_AIDSleft["OI_AIDS_ID1"] = dr[0].ToString();
                                            drOI_AIDSleft["OI_AIDS_Desc"] = "Blank";
                                            drOI_AIDSleft["HIVAssoDiseasePresent"] = true;
                                            dtOI_AIDSleft.Rows.Add(drOI_AIDSleft);
                                        }
                                        else
                                        {
                                            drOI_AIDSleft = dtOI_AIDSleft.NewRow();
                                            drOI_AIDSleft["OI_AIDS_ID1"] = dr[0].ToString();
                                            drOI_AIDSleft["OI_AIDS_Desc"] = "Blank";
                                            drOI_AIDSleft["HIVAssoDiseasePresent"] = true;
                                            dtOI_AIDSleft.Rows.Add(drOI_AIDSleft);
                                        }
                                    }
                                }
                            }

                        }
                        else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                        {
                            if (((HtmlInputCheckBox)chkPulTB).Checked == true)
                            {
                                if (((HtmlInputRadioButton)ct).Checked == true)
                                {
                                    strOI_AIDSValue = ((HtmlInputRadioButton)ct).Value;
                                    foreach (DataRow dr in theDTHivAssoConditionleft.Rows)
                                    {
                                        if (dr[1].ToString() == strOI_AIDSValue)
                                        {
                                            drOI_AIDSleft = dtOI_AIDSleft.NewRow();
                                            drOI_AIDSleft["OI_AIDS_ID1"] = dr[0].ToString();
                                            drOI_AIDSleft["OI_AIDS_Desc"] = "Blank";
                                            drOI_AIDSleft["HIVAssoDiseasePresent"] = true;
                                            dtOI_AIDSleft.Rows.Add(drOI_AIDSleft);

                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            theDS.Tables.Add(dtOI_AIDSleft);
        }


        /*************** Code for Saving OI and AIDs Defining Illness[Right]-//dtl_PatientDisease *******************/

        int OI_AIDS_Desc_OtherID = 0;
        DataTable dtOI_AIDSright = new DataTable();

        DataColumn theOI_AIDS_ID2 = new DataColumn("OI_AIDS_ID2");
        theOI_AIDS_ID2.DataType = System.Type.GetType("System.Int32");
        dtOI_AIDSright.Columns.Add(theOI_AIDS_ID2);

        DataColumn theOI_AIDS_Desc_Other = new DataColumn("OI_AIDS_Desc_other");
        theOI_AIDS_Desc_Other.DataType = System.Type.GetType("System.String");
        dtOI_AIDSright.Columns.Add(theOI_AIDS_Desc_Other);

        DataColumn theDTHIVAssoDiseaseRight = new DataColumn("HIVAssoDiseasePresent1");
        theDTHIVAssoDiseaseRight.DataType = System.Type.GetType("System.Boolean");
        dtOI_AIDSright.Columns.Add(theDTHIVAssoDiseaseRight);

        DataRow drOI_AIDSright;
        if (rdoHIVassocNone.Checked == false && PrevHIVassocNotDocumented.Checked == false)
        {
            foreach (HtmlTableRow r in tblOIsAIDsright.Rows)
            {
                foreach (HtmlTableCell c in r.Cells)
                {
                    foreach (Control ct in c.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Checked == true)
                            {
                                strOI_AIDSValue = ((HtmlInputCheckBox)ct).Value;
                                foreach (DataRow dr in theDTHivAssoConditionright.Rows)
                                {
                                    if (dr[1].ToString() == strOI_AIDSValue)
                                    {
                                        if (dr[1].ToString() == "Other")
                                        {
                                            OI_AIDS_Desc_OtherID = Convert.ToInt32(dr[0].ToString());
                                        }
                                        else
                                        {
                                            drOI_AIDSright = dtOI_AIDSright.NewRow();
                                            drOI_AIDSright["OI_AIDS_ID2"] = dr[0].ToString();
                                            drOI_AIDSright["OI_AIDS_Desc_other"] = "Blank";
                                            drOI_AIDSright["HIVAssoDiseasePresent1"] = true;
                                            dtOI_AIDSright.Rows.Add(drOI_AIDSright);
                                            theHIVAssocDisease = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        {
                            if (((HtmlInputText)ct).Value == "")
                            {
                                drOI_AIDSright = dtOI_AIDSright.NewRow();
                                drOI_AIDSright["OI_AIDS_ID2"] = OI_AIDS_Desc_OtherID.ToString();
                                drOI_AIDSright["OI_AIDS_Desc_other"] = "Blank";
                                drOI_AIDSright["HIVAssoDiseasePresent1"] = true;
                                dtOI_AIDSright.Rows.Add(drOI_AIDSright);
                                theHIVAssocDisease = true;
                            }
                            else
                            {
                                drOI_AIDSright = dtOI_AIDSright.NewRow();
                                drOI_AIDSright["OI_AIDS_ID2"] = OI_AIDS_Desc_OtherID.ToString();
                                drOI_AIDSright["OI_AIDS_Desc_other"] = ((HtmlInputText)ct).Value;
                                drOI_AIDSright["HIVAssoDiseasePresent1"] = true;
                                dtOI_AIDSright.Rows.Add(drOI_AIDSright);
                                theHIVAssocDisease = true;
                            }
                        }
                    }
                }
            }
            theDS.Tables.Add(dtOI_AIDSright);
        }
        else
        {
            drOI_AIDSright = dtOI_AIDSright.NewRow();
            drOI_AIDSright["OI_AIDS_ID2"] = System.DBNull.Value;
            drOI_AIDSright["OI_AIDS_Desc_other"] = System.DBNull.Value;
            drOI_AIDSright["HIVAssoDiseasePresent1"] = System.DBNull.Value;
            dtOI_AIDSright.Rows.Add(drOI_AIDSright);
            theHIVAssocDisease = false;
            theDS.Tables.Add(dtOI_AIDSright);
        }

        /************************ Code To Save Drug Details ************************/

        DataTable theDT = new DataTable();
        theDT.Columns.Add("GenericID", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("DrugID", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("Strength", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("Frequency", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("Duration", System.Type.GetType("System.Decimal"));
        theDT.Columns.Add("QtyPrescribed", System.Type.GetType("System.Decimal"));
        theDT.Columns.Add("QtyDispensed", System.Type.GetType("System.Decimal"));

        if (lstpharmSulfaTMXDose.SelectedIndex != 0)
        {
            theGeneric = 277;
            theDrug = 0;
            theStrength = Convert.ToInt32(lstpharmSulfaTMXDose.Value);
        }
        if (lstpharmSulfaTMXFreq.SelectedIndex != 0)
        {
            theFrequency = Convert.ToInt32(lstpharmSulfaTMXFreq.Value);
        }
        if (txtpharmSulfaTMXDuration.Value != "")
        {
            theDuration = Convert.ToDecimal(txtpharmSulfaTMXDuration.Value);
        }
        if (txtpharmSulfaTMXQty.Value != "")
        {
            thePrescribedQty = Convert.ToDecimal(txtpharmSulfaTMXQty.Value);
        }
        if (txtpharmSulfaTMXDispensed.Value != "")
        {
            theDispensedQty = Convert.ToDecimal(txtpharmSulfaTMXDispensed.Value);
        }

        if (theStrength != 99999)                                          //(theDrug != 0 || theStrength != 0)
        {
            theDR = theDT.NewRow();
            theDR["GenericID"] = theGeneric;
            theDR["DrugID"] = theDrug;
            theDR["Strength"] = theStrength;
            theDR["Frequency"] = theFrequency;
            theDR["Duration"] = theDuration;
            theDR["QtyPrescribed"] = thePrescribedQty;
            theDR["QtyDispensed"] = theDispensedQty;

            theDT.Rows.Add(theDR);
            theGeneric = 99999;
            theDrug = 99999;
            theStrength = 99999;
            theFrequency = 99999;
            theDuration = 99999;
            thePrescribedQty = 99999;
            theDispensedQty = 99999;

        }

        if (lstpharmFluconazoleDose.SelectedIndex != 0)
        {
            theGeneric = 123;
            theDrug = 0;
            theStrength = Convert.ToInt32(lstpharmFluconazoleDose.Value);
        }
        if (lstpharmFluconazoleFreq.SelectedIndex != 0)
        {
            theFrequency = Convert.ToInt32(lstpharmFluconazoleFreq.Value);
        }
        if (txtpharmFluconazoleDuration.Value != "")
        {
            theDuration = Convert.ToDecimal(txtpharmFluconazoleDuration.Value);
        }
        if (txtpharmFluconazoleQty.Value != "")
        {
            thePrescribedQty = Convert.ToDecimal(txtpharmFluconazoleQty.Value);
        }
        if (txtpharmFluconazoleDispensed.Value != "")
        {
            theDispensedQty = Convert.ToDecimal(txtpharmFluconazoleDispensed.Value);
        }


        if (theStrength != 99999)
        {
            theDR = theDT.NewRow();
            theDR["GenericID"] = theGeneric;
            theDR["DrugID"] = theDrug;
            theDR["Strength"] = theStrength;
            theDR["Frequency"] = theFrequency;
            theDR["Duration"] = theDuration;
            theDR["QtyPrescribed"] = thePrescribedQty;
            theDR["QtyDispensed"] = theDispensedQty;

            theDT.Rows.Add(theDR);
        }
        theDS.Tables.Add(theDT);

        DataTable theDTTBSymptoms = new DataTable();
        theDTTBSymptoms.Columns.Add("TBSymptomsID", System.Type.GetType("System.Int32"));

        for (int i = 0; i < cblTBScreen.Items.Count; i++)
        {
            if (cblTBScreen.Items[i].Selected)
            {
                theDRTBSymptom = theDTTBSymptoms.NewRow();
                theDRTBSymptom["TBSymptomsID"] = Convert.ToInt32(cblTBScreen.Items[i].Value);
                theDTTBSymptoms.Rows.Add(theDRTBSymptom);
            }
        }
        if (rdoPerformed.Checked == true)
        {
            theDRTBSymptom = theDTTBSymptoms.NewRow();
            theDRTBSymptom["TBSymptomsID"] = 70;
            theDTTBSymptoms.Rows.Add(theDRTBSymptom);
        }
        else if (rdoNotDocumented.Checked == true)
        {
            theDRTBSymptom = theDTTBSymptoms.NewRow();
            theDRTBSymptom["TBSymptomsID"] = 71;
            theDTTBSymptoms.Rows.Add(theDRTBSymptom);
        }
        //else if (rdoTBNone.Checked == true)
        //{
        //    theDRTBSymptom = theDTTBSymptoms.NewRow();
        //    theDRTBSymptom["TBSymptomsID"] = 78;
        //    theDTTBSymptoms.Rows.Add(theDRTBSymptom);
        //}
        theDS.Tables.Add(theDTTBSymptoms);
        return theDS;

    }
    /*************************Maintain OI & AIDS status in case of field validation *******************/
    private void ShowOIAIDS()
    {
        //Left DIV Items
        foreach (HtmlTableRow r in tblOIsAIDsleft.Rows)
        {
            foreach (HtmlTableCell c in r.Cells)
            {
                foreach (Control ct in c.Controls)
                {
                    if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                    {
                        if (((HtmlInputCheckBox)ct).Value == "Pulmonary TB" && ((HtmlInputCheckBox)ct).Checked == true)
                        {
                            string script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'OIsAIDs_ID'>\n";
                            script += "show('pultb');\n";
                            script += "</script>\n";
                            RegisterStartupScript("OIsAIDs_ID", script);
                        }
                    }
                }
            }
        }




        //Right DIV Items
        foreach (HtmlTableRow r in tblOIsAIDsright.Rows)
        {
            foreach (HtmlTableCell c in r.Cells)
            {
                foreach (Control ct in c.Controls)
                {
                    if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                    {
                        if (((HtmlInputCheckBox)ct).Value == "Other")
                        {
                            string script = "";
                            script = "<script language = 'javascript' defer='defer' id='OIsAIDs_ID_1'>\n";
                            script += "show('otherOIsAIDs');\n";
                            script += "</script>\n";
                            RegisterStartupScript("OIsAIDs_ID_1", script);
                        }
                    }
                }
            }
        }
    }

    /************** Save Function ****************/
    private void SaveData()
    {
        //DataSet theDSNonARTFollowUp = new DataSet();
        int PatientID, PharmacyID = 0, VisitID = 0, EmployeeID = 0;
        //Naveen-UpdateMode 1 for No update in Visit date and 2 for Update Visit date

        DateTime theOrderedByDate, theReportedByDate;

        Boolean theSave, flag = false;

        NonARTFollowUpParameters();
        theDS = NonARTDetails();
        DataTable theDT = MakeDrugTable(PnlAddOtherMedication);


        IQCareUtils theUtils = new IQCareUtils();
        NonARTManager = (INonARTFollowUp)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNonARTFollowUp, BusinessProcess.Clinical");

        //try
        //{

        foreach (DataRow theDR in theDT.Rows)
        {
            if (theDR["DrugId"].ToString().IndexOf("8888") > 0)
                theDR["DrugId"] = theDR["DrugId"].ToString().Substring(0, theDR["DrugId"].ToString().Length - 4);
            if (theDR["GenericId"].ToString().IndexOf("9999") > 0)
                theDR["GenericId"] = theDR["GenericId"].ToString().Substring(0, theDR["GenericId"].ToString().Length - 4);
        }

        /********** Selected Date **********/
        if (txtpharmOrderedbyDate.Value == "")
        {
            theOrderedByDate = Convert.ToDateTime(theUtils.MakeDate("01-01-1900"));
        }
        else
        {
            theOrderedByDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value));
        }


        if (txtpharmReportedbyDate.Value == "")
        {
            theReportedByDate = Convert.ToDateTime(theUtils.MakeDate("01-01-1900"));
        }
        else
        {
            theReportedByDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmReportedbyDate.Value));
        }

        if (ddlPharmSignature.SelectedIndex == 3)
        {
            flag = true;
            EmployeeID = Convert.ToInt32(ddlCounselerName.SelectedValue);
        }
        else if (ddlPharmSignature.SelectedIndex == 2)
        {
            flag = true;
            EmployeeID = 0;
        }
        else
        {
            flag = false;
            EmployeeID = 0;
        }
        CustomFieldClinical theCustomManager = new CustomFieldClinical();
        DataTable theCustomDataDT;
        DataSet theDSNonARTFollowUp;
        if (Convert.ToInt32(Session["PatientVisitId"]) < 1)
        {
            Session["Redirect"] = "0";
            theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.NonARTFollowup, (DataSet)ViewState["CustomFieldsDS"]);
            //Function for saving new Data

            theDSNonARTFollowUp = (DataSet)NonARTManager.SaveNonARTFollowUp(Convert.ToInt32(Session["PatientId"]), PharmacyID, Convert.ToInt32(Session["LocationId"]),
                VisitID, theDS, theDT, theHT, theOrderedByDate, theReportedByDate, flag, EmployeeID, Convert.ToInt32(Session["UserID"]), false,
                theHIVAssocDisease, Convert.ToInt32(Session["DataQualityFlag"]), theCustomDataDT);
            if (theDSNonARTFollowUp.Tables[0].Rows.Count > 0)
                Session["PharmaId"] = theDSNonARTFollowUp.Tables[0].Rows[0][0];
            Session["PatientVisitId"] = theDSNonARTFollowUp.Tables[1].Rows[0]["VisitId"];


        }

        else
        {
            theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.NonARTFollowup, (DataSet)ViewState["CustomFieldsDS"]);
            //Updating Existing Data

            theDSNonARTFollowUp = (DataSet)NonARTManager.SaveNonARTFollowUp(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PharmaID"]),
                Convert.ToInt32(Session["LocationId"]), Convert.ToInt32(Session["PatientVisitId"]), theDS, theDT, theHT, theOrderedByDate, theReportedByDate, flag, EmployeeID,
                Convert.ToInt32(Session["UserID"]), true, theHIVAssocDisease, Convert.ToInt32(Session["DataQualityFlag"]), theCustomDataDT);
            if (Session["DataQualityFlag"] != null && Session["DataQualityFlag"].ToString() == "1")
            {

                btnQualityCheck.CssClass = "greenbutton";

            }

        }

        SaveCancel(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["PharmaID"]));

        /*  string script = "<script language = 'javascript' defer ='defer' id = 'aftersavefunction'>\n";
          script += "document.getElementById('" + theBtn1.ClientID + "').click();\n";
          script += "</script>\n";
          RegisterStartupScript("aftersavefunction", script);  */

        //---------------------------------------------




        ////////if (Request.QueryString["name"] == "Add")
        ////////{
        ////////    PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
        ////////    theSave = false;

        ////////    DataSet theViewDS = new DataSet();

        ////////    if (Session["theSaveDS"] != null)
        ////////    {
        ////////        theViewDS = (DataSet)Session["theSaveDS"];
        ////////    }
        ////////    if ((Session["theSaveDS"] == null) || (theViewDS.Tables.Count == 0))
        ////////    {
        ////////        if (Convert.ToInt32(Session["VisitType"]) == 1)
        ////////        {
        ////////            //Custom Field Insertion
        ////////            CustomFieldClinical theCustomManager = new CustomFieldClinical();
        ////////            DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.NonARTFollowup, (DataSet)ViewState["CustomFieldsDS"]);
        ////////            //Function for saving new Data

        ////////            theDSNonARTFollowUp = (DataSet)NonARTManager.SaveNonARTFollowUp(PatientID, PharmacyID, Convert.ToInt32(Session["LocationId"]), VisitID, theDS, theDT, theHT, theOrderedByDate, theReportedByDate, flag, EmployeeID, Convert.ToInt32(Session["UserID"]), theSave, theHIVAssocDisease, Convert.ToInt32(Session["DataQualityFlag"]), theCustomDataDT);
        ////////            Session["theSaveDS"] = theDSNonARTFollowUp;
        ////////            Session["DataQualityFlag"] = 0;
        ////////            if (theDSNonARTFollowUp.Tables[0].Rows[0][0].ToString() == "0")
        ////////            {
        ////////                ViewState["VisitID_add"] = Convert.ToInt32(theDSNonARTFollowUp.Tables[0].Rows[0][0].ToString());
        ////////                ViewState["VisitID_CC"] = Convert.ToInt32(theDSNonARTFollowUp.Tables[0].Rows[0]["VisitID"].ToString());
        ////////                //ViewState["Pharmacy_ID"] = Convert.ToInt32(theDSNonARTFollowUp.Tables[0].Rows[0]["PharmacyID"].ToString());
        ////////            }
        ////////            //Amitava Sinha
        ////////            else
        ////////            {
        ////////                ViewState["VisitID_add"] = Convert.ToInt32(theDSNonARTFollowUp.Tables[1].Rows[0]["VisitID"].ToString());
        ////////                ViewState["VisitID_CC"] = Convert.ToInt32(theDSNonARTFollowUp.Tables[1].Rows[0]["VisitID"].ToString());
        ////////               // ViewState["Pharmacy_ID"] = Convert.ToInt32(theDSNonARTFollowUp.Tables[1].Rows[0]["PharmacyID"].ToString());
        ////////            }

        ////////            if (Session["DataQualityFlag"] != null && Session["DataQualityFlag"].ToString() == "1")
        ////////            {


        ////////                SaveCancel(Convert.ToInt32(Request.QueryString["PatientId"].ToString()), Convert.ToInt32(ViewState["VisitID_add"]), PharmacyID);
        ////////                btnQualityCheck.CssClass = "greenbutton";
        ////////                return;
        ////////            }
        ////////            else if(Session["DataQualityFlag"] != null && Session["DataQualityFlag"].ToString() == "0")
        ////////            {
        ////////                //string script = "<script language = 'javascript' defer ='defer' id = 'aftersavefunction'>\n";
        ////////                //script += "document.getElementById('" + theBtn.ClientID + "').click();\n";
        ////////                //script += "</script>\n";
        ////////                //RegisterStartupScript("aftersavefunction", script);
        ////////                SaveCancel(Convert.ToInt32(Request.QueryString["PatientId"].ToString()), Convert.ToInt32(ViewState["VisitID_add"]), PharmacyID);
        ////////                return;
        ////////            }
        ////////            //SaveCancel(Convert.ToInt32(Request.QueryString["PatientId"].ToString()), Convert.ToInt32(ViewState["VisitID_add"]), PharmacyID);

        ////////        }
        ////////        else
        ////////        {
        ////////            IQCareMsgBox.Show("IECheckingForNonARTFollowUp", this);
        ////////            return;
        ////////        }
        ////////    }
        ////////    else
        ////////    {

        ////////        if (theViewDS.Tables[0].Rows[0][0].ToString() == "0")
        ////////        {
        ////////            VisitID = Convert.ToInt32(ViewState["VisitID_CC"].ToString());
        ////////         }
        ////////        else
        ////////        {
        ////////            PharmacyID = Convert.ToInt32(theViewDS.Tables[0].Rows[0][0].ToString());
        ////////            if (PharmacyID != 0)
        ////////            {
        ////////                VisitID = Convert.ToInt32(ViewState["VisitID_CC"].ToString());
        ////////            }
        ////////            else
        ////////             {
        ////////                VisitID = Convert.ToInt32(ViewState["VisitID_CC"].ToString());
        ////////             }
        ////////        }

        ////////        theSave = true;
        ////////        //CustomField 
        ////////        CustomFieldClinical theCustomManager = new CustomFieldClinical();
        ////////        DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.NonARTFollowup, (DataSet)ViewState["CustomFieldsDS"]);
        ////////        //Updating Existing Data

        ////////        theDSNonARTFollowUp = (DataSet)NonARTManager.SaveNonARTFollowUp(PatientID, PharmacyID, Convert.ToInt32(Session["LocationId"]), VisitID, theDS, theDT, theHT, theOrderedByDate, theReportedByDate, flag, EmployeeID, Convert.ToInt32(Session["UserID"]), theSave, theHIVAssocDisease, Convert.ToInt32(Session["DataQualityFlag"]), theCustomDataDT);
        ////////        Session["theSaveDS"] = theDSNonARTFollowUp;

        ////////        //Amitava Sinha
        ////////        ViewState["VisitID_add"] = VisitID;
        ////////        //Not required this section 08-07-2009
        ////////        //if (Session["ControlCreated"] != null)
        ////////        //    UpdateCustomFieldsValues();
        ////////        if (Session["DataQualityFlag"] != null && Session["DataQualityFlag"].ToString() == "1")
        ////////        {

        ////////            //string script = "<script language = 'javascript' defer ='defer' id = 'aftersavefunction'>\n";
        ////////            //script += "document.getElementById('" + theBtn1.ClientID + "').click();\n";
        ////////            //script += "</script>\n";
        ////////            //RegisterStartupScript("aftersavefunction", script);
        ////////            SaveCancel(Convert.ToInt32(Request.QueryString["PatientId"].ToString()), Convert.ToInt32(ViewState["VisitID_add"]), PharmacyID);
        ////////            btnQualityCheck.CssClass = "greenbutton";
        ////////            return;
        ////////        }
        ////////        else if(Session["DataQualityFlag"] != null && Session["DataQualityFlag"].ToString() == "0")
        ////////        {
        ////////            //string script = "<script language = 'javascript' defer ='defer' id = 'aftersavefunction'>\n";
        ////////            //script += "document.getElementById('" + theBtn.ClientID + "').click();\n";
        ////////            //script += "</script>\n";
        ////////            //RegisterStartupScript("aftersavefunction", script);
        ////////            SaveCancel(Convert.ToInt32(Request.QueryString["PatientId"].ToString()), Convert.ToInt32(ViewState["VisitID_add"]), PharmacyID);
        ////////            return;
        ////////        }

        ////////    }

        ////////}
        ////////else if (Request.QueryString["name"] == "Edit")
        ////////{
        ////////    PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
        ////////    PharmacyID = Convert.ToInt32(Request.QueryString["PharmacyID"]);
        ////////    VisitID = Convert.ToInt32(Request.QueryString["visitid"]);
        ////////    theSave = true;
        ////////    //CustomField 
        ////////    CustomFieldClinical theCustomManager = new CustomFieldClinical();
        ////////    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.NonARTFollowup, (DataSet)ViewState["CustomFieldsDS"]);
        ////////    //Updating Existing Data

        ////////    theDSNonARTFollowUp = (DataSet)NonARTManager.SaveNonARTFollowUp(PatientID, PharmacyID, Convert.ToInt32(Session["LocationId"]), VisitID, theDS, theDT, theHT, theOrderedByDate, theReportedByDate, flag, EmployeeID, Convert.ToInt32(Session["UserID"]), theSave, theHIVAssocDisease, Convert.ToInt32(Session["DataQualityFlag"]), theCustomDataDT);
        ////////    Session["DataQualityFlag"] = 0;                
        ////////    //Amitava Sinha
        ////////    ViewState["VisitID_add"] = VisitID;


        ////////    if (Session["DataQualityFlag"] != null && Session["DataQualityFlag"].ToString() == "1")
        ////////    {

        ////////        string script = "<script language = 'javascript' defer ='defer' id = 'aftersavefunction'>\n";
        ////////        script += "document.getElementById('" + theBtn1.ClientID + "').click();\n";
        ////////        script += "</script>\n";
        ////////        RegisterStartupScript("aftersavefunction", script);
        ////////        btnQualityCheck.CssClass = "greenbutton";
        ////////        return;
        ////////    }
        ////////    else if (Session["DataQualityFlag"] != null && Session["DataQualityFlag"].ToString() == "0")
        ////////    {
        ////////        string script = "<script language = 'javascript' defer ='defer' id = 'aftersavefunction'>\n";
        ////////        script += "document.getElementById('" + theBtn.ClientID + "').click();\n";
        ////////        script += "</script>\n";
        ////////        RegisterStartupScript("aftersavefunction", script);

        ////////    }

        ////////}

        //}

        //catch (Exception err)
        //{
        //    MsgBuilder theMsg = new MsgBuilder();
        //    theMsg.DataElements["MessageText"] = err.Message.ToString();
        //    IQCareMsgBox.Show("C1#", theMsg, this);
        //}
        //finally
        //{
        //    NonARTManager = null;
        //}


    }

    /***************** Delete Record *****************/

    private void DeleteForm()
    {
        INonARTFollowUp NonARTManager;
        int theResultRow, OrderNo;
        string FormName;
        OrderNo = Convert.ToInt32(Session["PatientVisitId"]);
        FormName = "Non-ART Follow-Up";

        NonARTManager = (INonARTFollowUp)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BNonARTFollowUp, BusinessProcess.Clinical");

        theResultRow = (int)NonARTManager.DeleteNonARTForms(FormName, OrderNo, Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["UserID"]));

        if (theResultRow == 0)
        {
            IQCareMsgBox.Show("RemoveFormError", this);
            return;
        }
        else
        {
            string theUrl;
            theUrl = string.Format("{0}", "frmPatient_Home.aspx?Func=Delete");
            Response.Redirect(theUrl);

        }

    }
    #region-Added by Naveen 20-Aug-2010
    private void SaveCancel(int PatientID, int visitPK, int Pharamacyid)
    {
        IQCareMsgBox.NotifyAction("Non ART FollowUp Form saved successfully. Do you want to close?", "Non ART FollowUp Form", false, this, "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';");
        ////string strPatientID = ViewState["PtnID"].ToString();
        //string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        //script += "var ans;\n";
        //script += "ans=window.confirm('Non ART FollowUp Form saved successfully. Do you want to close?');\n";
        //script += "if (ans==true)\n";
        //script += "{\n";
        //if (Session["Redirect"] == "0")
        //{
        //    script += "window.location.href='frmPatient_Home.aspx';\n";
        //}
        //else
        //{
        //    script += "window.location.href='frmPatient_History.aspx';\n";
        //}
        //script += "}\n";
        //script += "else \n";
        //script += "{\n";
        ////script += "window.location.href('frmClinical_NonARTFollowup.aspx');\n";
        //script += "}\n";
        //script += "</script>\n";
        //RegisterStartupScript("confirm", script);

    }

    #endregion
    # region "Amitava"
    // Create Custom Controls 
    // Creation Date : 16-Jan-2007 
    // Amitava Sinha
    private void PutCustomControl()
    {
        ICustomFields CustomFields;
        CustomFieldClinical theCustomField = new CustomFieldClinical();
        try
        {
            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
            DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.NonARTFollowup));
            if (theDS.Tables[0].Rows.Count != 0)
            {
                theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "NonART");
                //ViewState["CustomFieldsDS"] = theDS;
                //pnlCustomList.Visible = true;
            }
            //theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "NonART");
            ViewState["CustomFieldsDS"] = theDS;
            pnlCustomList.Visible = true;
        }

        //ICustomFields CustomFields;
        //string pnlName = "PnlCustomList";
        //CustomFieldClinical theCustomField = new CustomFieldClinical();
        //BindFunctions theBindMgr = new BindFunctions();
        //TableName = string.Empty;
        //Int32 ii = 0;
        //try
        //{

        //    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
        //    DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.NonARTFollowup));
        //    if (theDS != null && theDS.Tables[0].Rows.Count > 0)
        //    {
        //        sbParameter = new StringBuilder();
        //        TableName = theDS.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");

        //        pnlCustomList.Visible = true;
        //        pnlCustomList.Controls.Clear();
        //        arl = new ArrayList();
        //        pnlCustomList.Controls.Add(new LiteralControl("<TABLE border='1' cellpadding=6 cellspacing=0 width=100%>"));

        //        foreach (DataRow dr in theDS.Tables[0].Rows)
        //        {
        //            if (ii % 2 == 0)
        //                pnlCustomList.Controls.Add(new LiteralControl("<TR >"));
        //            if (dr[1].ToString() == "1")
        //                pnlCustomList.Controls.Add(new LiteralControl("<TD >"));
        //            else if (dr[1].ToString() == "6")
        //                pnlCustomList.Controls.Add(new LiteralControl("<TD align='left' nowrap='noWrap' >"));
        //            else if ((dr[1].ToString() == "3") || (dr[1].ToString() == "4") || (dr[1].ToString() == "5") || (dr[1].ToString() == "7") || (dr[1].ToString() == "8") || (dr[1].ToString() == "9"))
        //                pnlCustomList.Controls.Add(new LiteralControl("<TD align='left'>"));

        //            //Select List
        //            if (dr[1].ToString() == "4")
        //            {
        //                Label customLabel = new Label();
        //                customLabel.ID = pnlName + "lbl" + ii.ToString();
        //                customLabel.Text = dr[0].ToString().Replace("_", " ");
        //                customLabel.Text = customLabel.Text.Replace("NonART", "");
        //                sbParameter.Append(",[" + dr[0].ToString() + "]");
        //                customLabel.Width = 200;
        //                customLabel.CssClass = "labelright";
        //                customLabel.Font.Bold = true;

        //                pnlCustomList.Controls.Add(customLabel);

        //                pnlCustomList.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

        //                DropDownList ddlSelectList = new DropDownList();
        //                ddlSelectList.ID = pnlName + "Selectlist" + dr[0].ToString();
        //                ddlSelectList.Width = 180;

        //                DataSet dsSelectList = CustomFields.GetCustomList(Convert.ToInt32(dr[2].ToString()));
        //                if (dsSelectList != null && dsSelectList.Tables[0].Rows.Count > 0)
        //                {
        //                    if (Request.QueryString["name"] == "Add")
        //                    {
        //                        IQCareUtils theUtilsCF = new IQCareUtils();
        //                        DataView theDVCF = new DataView(dsSelectList.Tables[0]);
        //                        theDVCF.RowFilter = "DeleteFlag=0";
        //                        DataTable theDTCF = (DataTable)theUtilsCF.CreateTableFromDataView(theDVCF);
        //                        /*******/
        //                        theBindMgr.BindCombo(ddlSelectList, theDTCF, "Name", "ID");
        //                        theDVCF.Dispose();
        //                        theDTCF.Clear();
        //                    }
        //                    else
        //                    {
        //                        theBindMgr.BindCombo(ddlSelectList, dsSelectList.Tables[0], "Name", "Id");
        //                    }

        //                }
        //                pnlCustomList.Controls.Add(ddlSelectList);

        //            }
        //            //Multi Select List
        //            else if (dr[1].ToString() == "9")
        //            {
        //                if (arl.Count == 0)
        //                {
        //                    arl.Add("dtl_CustomField_" + TableName.Replace("-", "_").ToString() + "_" + dr[0].ToString());
        //                }
        //                foreach (object obj in arl)
        //                {
        //                    if (obj.ToString() != "dtl_CustomField_" + TableName.Replace("-", "_").ToString() + "_" + dr[0].ToString())
        //                    {
        //                        arl.Add("dtl_CustomField_" + TableName.Replace("-", "_").ToString() + "_" + dr[0].ToString());
        //                        break;
        //                    }
        //                }
        //                Label theMultiSelectlbl = new Label();
        //                theMultiSelectlbl.ID = pnlName + "lbl" + ii.ToString();
        //                theMultiSelectlbl.Text = dr[0].ToString().Replace("_", " ");
        //                theMultiSelectlbl.Text = theMultiSelectlbl.Text.Replace("NonART", "");
        //                theMultiSelectlbl.Width = 200;
        //                theMultiSelectlbl.CssClass = "labelright";
        //                theMultiSelectlbl.Font.Bold = true;
        //                pnlCustomList.Controls.Add(theMultiSelectlbl);

        //                pnlCustomList.Controls.Add(new LiteralControl("<div class = 'Customdivborder' nowrap='nowrap'>"));

        //                CheckBoxList chkMultiList = new CheckBoxList();
        //                chkMultiList.ID = pnlName + "Multiselectlist" + dr[0].ToString();
        //                chkMultiList.RepeatLayout = RepeatLayout.Flow;
        //                chkMultiList.CssClass = "check";
        //                chkMultiList.Width = 300;

        //                DataSet dsMultiSelectList = CustomFields.GetCustomList(Convert.ToInt32(dr[2].ToString()));
        //                if (dsMultiSelectList != null && dsMultiSelectList.Tables[0].Rows.Count > 0)
        //                {
        //                    if (Request.QueryString["name"] == "Add")
        //                    {
        //                        IQCareUtils theUtilsCF = new IQCareUtils();
        //                        DataView theDVCF = new DataView(dsMultiSelectList.Tables[0]);
        //                        theDVCF.RowFilter = "DeleteFlag=0";
        //                        DataTable theDTCF = (DataTable)theUtilsCF.CreateTableFromDataView(theDVCF);
        //                        /*******/
        //                        theBindMgr.BindCheckedList(chkMultiList, theDTCF, "Name", "Id");

        //                        theDVCF.Dispose();
        //                        theDTCF.Clear();
        //                    }
        //                    else
        //                    {
        //                        theBindMgr.BindCheckedList(chkMultiList, dsMultiSelectList.Tables[0], "Name", "Id");
        //                    }
        //                }
        //                pnlCustomList.Controls.Add(chkMultiList);

        //                pnlCustomList.Controls.Add(new LiteralControl("</div>"));

        //            }

        //            theCustomField.CreateCustomControls(pnlCustomList, pnlName, ref sbParameter, dr, ref TableName, "NonART", ii);

        //            ii++;
        //        }
        //    }
        //    Session["ControlCreated"] = "CC";
        //    pnlCustomList.Controls.Add(new LiteralControl("</TABLE>"));


        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
        finally
        {
            CustomFields = null;
        }

    }
    //Amitava Sinha
    //Generating full DML Statement 
    #region "Not required UpdateCustomFieldvalues"
    //private void UpdateCustomFieldsValues()
    //{
    //    GenerateCustomFieldsValues(pnlCustomList);
    //    string sqlstr = string.Empty;
    //    Int32 PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
    //    string sqlselect;
    //    string strdelete;
    //    Int32 visitID = 0;
    //    DateTime visitdate = System.DateTime.Now;
    //    ICustomFields CustomFields;
    //    if (ViewState["VisitID_CC"].ToString() != "")
    //    {
    //        visitID = Convert.ToInt32(ViewState["VisitID_CC"].ToString());
    //    }
    //    else { visitID = 0; }


    //    //if (ViewState["VisitID_CC"] != null)
    //    //    visitID = Convert.ToInt32(ViewState["VisitID_CC"]);
    //    if (txtvisitDate.Value.ToString() != "")
    //        visitdate = Convert.ToDateTime(txtvisitDate.Value.ToString());

    //    if (sbValues.ToString().Trim() != "")
    //    {
    //        if (ViewState["CustomFieldsData"] != null)
    //        {
    //            sbValues = sbValues.Remove(0, 1);
    //            sqlstr = "UPDATE dtl_CustomField_" + TableName.ToString().Replace("-", "_") + " SET ";
    //            sqlstr += sbValues.ToString() + " where Ptn_pk= " + PatientID.ToString() + " and Visit_pk=" + visitID.ToString();
    //        }
    //        else
    //        {
    //            sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
    //            sqlstr += " VALUES(" + PatientID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";
    //            ViewState["CustomFieldsData"] = 1;
    //        }

    //        try
    //        {
    //            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
    //            icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
    //            if (icount == -1)
    //            {
    //                return;
    //            }
    //        }
    //        catch
    //        {
    //        }
    //        finally
    //        {
    //            CustomFields = null;
    //        }
    //    }
    //    if (strmultiselect.ToString() != "")
    //    {
    //        string[] FieldValues = strmultiselect.Split(new char[] { '^' });
    //        if (arl.Count != 0)
    //        {
    //            int p = 0;
    //            foreach (object obj in arl)
    //            {
    //                sqlselect = "";
    //                strdelete = "";
    //                if (obj.ToString() != "")
    //                {
    //                    try
    //                    {
    //                        CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
    //                        strdelete = "DELETE from [" + obj.ToString() + "] where ptn_pk= " + PatientID.ToString() + " and LocationID=" + Session["AppLocationID"] + " and visit_pk=" + visitID;
    //                        icount = CustomFields.SaveCustomFieldValues(strdelete.ToString());

    //                        if (FieldValues[p].ToString() != "")
    //                        {
    //                            string[] mValues = FieldValues[p].Split(new char[] { ',' });

    //                            foreach (string str in mValues)
    //                            {
    //                                if (str.ToString() != "")
    //                                {
    //                                    string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
    //                                    Int32 ispos = Convert.ToInt32(strtab.Length);
    //                                    Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;

    //                                    sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,visit_pk,visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
    //                                    sqlselect += " VALUES (" + PatientID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";



    //                                    icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
    //                                    if (icount == -1)
    //                                    {
    //                                        return;
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }
    //                    catch
    //                    {
    //                    }
    //                    finally
    //                    {
    //                        CustomFields = null;
    //                    }
    //                }
    //                p += 1;
    //            }
    //        }
    //    }
    //}
    #endregion

    //Amitava Sinha
    //Generating full DML Statement 
    #region "Not required InsertCustomFieldsValues"
    //private void InsertCustomFieldsValues()
    //{
    //    GenerateCustomFieldsValues(pnlCustomList);
    //    string sqlstr = string.Empty;
    //    string sqlselect;
    //    Int32 visitID = 0;
    //    DateTime visitdate = System.DateTime.Now;

    //    Int32 PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
    //    ICustomFields CustomFields;
    //    if (ViewState["VisitID_CC"] != null)
    //    {
    //        if (ViewState["VisitID_CC"].ToString() != "")
    //        {
    //            visitID = Convert.ToInt32(ViewState["VisitID_CC"]);
    //        }
    //        else { visitID = 0; }
    //    }

    //    if (txtvisitDate.Value.ToString() != "")
    //        visitdate = Convert.ToDateTime(txtvisitDate.Value.ToString());

    //    if (sbValues.ToString().Trim() != "")
    //    {
    //        sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
    //        sqlstr += " VALUES(" + PatientID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";

    //        try
    //        {
    //            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
    //            icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
    //            if (icount == -1)
    //            {
    //                return;
    //            }
    //        }
    //        catch
    //        {
    //        }
    //        finally
    //        {
    //            CustomFields = null;
    //        }
    //    }
    //    if (strmultiselect.ToString() != "")
    //    {
    //        string[] FieldValues = strmultiselect.Split(new char[] { '^' });
    //        if (arl.Count != 0)
    //        {
    //            int p = 0;
    //            foreach (object obj in arl)
    //            {
    //                sqlselect = "";
    //                if (obj.ToString() != "")
    //                {
    //                    if (FieldValues[p].ToString() != "")
    //                    {
    //                        string[] mValues = FieldValues[p].Split(new char[] { ',' });
    //                        foreach (string str in mValues)
    //                        {
    //                            if (str.ToString() != "")
    //                            {
    //                                string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
    //                                Int32 ispos = Convert.ToInt32(strtab.Length);
    //                                Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;
    //                                sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,Visit_pk,Visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
    //                                sqlselect += " VALUES (" + PatientID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";
    //                                try
    //                                {
    //                                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
    //                                    icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
    //                                    if (icount == -1)
    //                                    {
    //                                        return;
    //                                    }

    //                                }
    //                                catch
    //                                {
    //                                }
    //                                finally
    //                                {
    //                                    CustomFields = null;
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //                p += 1;
    //            }
    //        }
    //    }
    //}

    #endregion
    //Amitava Sinha
    //Generate a string builder for Insert Query Values 
    //and Update Query Values 
    #region "Not required Generate Custom Field values"
    //private void GenerateCustomFieldsValues(Control Cntrl)
    //{
    //    string pnlName = Cntrl.ID;
    //    sbValues = new StringBuilder();
    //    strmultiselect = string.Empty;
    //    string strfName = string.Empty;
    //    Boolean radioflag = false;

    //    Int32 stpos = 0;
    //    Int32 enpos = 0;
    //    if (ViewState["CustomFieldsData"] != null)
    //    {
    //        foreach (Control x in Cntrl.Controls)
    //        {
    //            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
    //            {
    //                if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
    //                {
    //                    strfName = pnlName.ToUpper() + "TXT";
    //                    stpos = strfName.Length;
    //                    enpos = x.ID.Length - stpos;
    //                    strfName = x.ID.Substring(stpos, enpos).ToString();
    //                    if (((TextBox)x).Text != "")
    //                    {
    //                        sbValues.Append(",[" + strfName + "] = '" + ((TextBox)x).Text.ToString() + "'");
    //                    }
    //                    else
    //                    {
    //                        sbValues.Append(",[" + strfName + "] = ' " + "'");
    //                    }
    //                }
    //                else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
    //                {
    //                    strfName = pnlName.ToUpper() + "NUM";
    //                    stpos = strfName.Length;
    //                    enpos = x.ID.Length - stpos;
    //                    strfName = x.ID.Substring(stpos, enpos).ToString();
    //                    if (((TextBox)x).Text != "")
    //                    {
    //                        sbValues.Append(",[" + strfName + "]=" + ((TextBox)x).Text.ToString());
    //                    }
    //                    else
    //                    {
    //                        sbValues.Append("," + strfName + "=Null");
    //                    }

    //                }
    //                else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
    //                {
    //                    strfName = pnlName.ToUpper() + "DT";
    //                    stpos = strfName.Length;
    //                    enpos = x.ID.Length - stpos;
    //                    strfName = x.ID.Substring(stpos, enpos).ToString();
    //                    if (((TextBox)x).Text != "")
    //                    {
    //                        sbValues.Append(",[" + strfName + "]='" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
    //                    }
    //                    else
    //                    {
    //                        sbValues.Append(",[" + strfName + "]=" + "Null");
    //                    }
    //                }
    //            }
    //            if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
    //            {
    //                if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
    //                {
    //                    radioflag = false;
    //                    strfName = pnlName.ToUpper() + "RADIO1";
    //                    stpos = strfName.Length;
    //                    enpos = x.ID.Length - stpos;
    //                    strfName = x.ID.Substring(stpos, enpos).ToString();
    //                    if (((HtmlInputRadioButton)x).Checked == true)
    //                    {
    //                        sbValues.Append(",[" + strfName + "]=" + "1");
    //                        radioflag = true;
    //                    }
    //                }
    //                if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
    //                {
    //                    strfName = pnlName.ToUpper() + "RADIO2";
    //                    stpos = strfName.Length;
    //                    enpos = x.ID.Length - stpos;
    //                    strfName = x.ID.Substring(stpos, enpos).ToString();
    //                    if (((HtmlInputRadioButton)x).Checked == true)
    //                    {
    //                        sbValues.Append(",[" + strfName + "]=" + "0");
    //                        radioflag = true;
    //                    }
    //                    if (radioflag == false)
    //                    {
    //                        sbValues.Append(",[" + strfName + "]=" + "Null");
    //                    }
    //                }

    //            }
    //            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
    //            {
    //                if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
    //                {
    //                    strfName = pnlName.ToUpper() + "SELECTLIST";
    //                    stpos = strfName.Length;
    //                    enpos = x.ID.Length - stpos;
    //                    strfName = x.ID.Substring(stpos, enpos).ToString();
    //                    if (((DropDownList)x).SelectedValue != "0")
    //                    {
    //                        sbValues.Append(",[" + strfName + "] = " + ((DropDownList)x).SelectedValue.ToString() + " ");
    //                    }
    //                    else
    //                    {
    //                        sbValues.Append(",[" + strfName + "] =  " + "0");
    //                      //  sbValues.Append("," + "0");
    //                    }

    //                }
    //            }

    //        }
    //    }

    //    if (ViewState["CustomFieldsData"] == null)
    //    {
    //        foreach (Control x in Cntrl.Controls)
    //        {
    //            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
    //            {
    //                if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
    //                {
    //                    if (((TextBox)x).Text != "")
    //                    {
    //                        sbValues.Append(",'" + ((TextBox)x).Text.ToString() + "'");
    //                    }
    //                    else
    //                    {
    //                        sbValues.Append(",' " + "'");
    //                    }
    //                }
    //                else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
    //                {
    //                    if (((TextBox)x).Text != "")
    //                    {
    //                        sbValues.Append("," + ((TextBox)x).Text.ToString());
    //                    }
    //                    else
    //                    {
    //                        sbValues.Append(",0");
    //                    }

    //                }
    //                else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
    //                {
    //                    if (((TextBox)x).Text != "")
    //                    {
    //                        sbValues.Append(",'" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
    //                    }
    //                    else
    //                    {

    //                        sbValues.Append(",Null");
    //                    }
    //                }
    //            }
    //            if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
    //            {
    //                if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
    //                {
    //                    radioflag = false;
    //                    if (((HtmlInputRadioButton)x).Checked == true)
    //                    {
    //                        sbValues.Append(",1");
    //                        radioflag = true;
    //                    }
    //                }
    //                if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
    //                {
    //                    if (((HtmlInputRadioButton)x).Checked == true)
    //                    {
    //                        sbValues.Append(",0");
    //                        radioflag = true;
    //                    }
    //                    if (radioflag == false)
    //                    {
    //                        sbValues.Append(",Null");
    //                    }
    //                }
    //            }
    //            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
    //            {
    //                if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
    //                {
    //                    if (((DropDownList)x).SelectedValue != "0")
    //                    {
    //                        sbValues.Append("," + ((DropDownList)x).SelectedValue.ToString() + " ");
    //                    }
    //                    else
    //                    {
    //                        sbValues.Append(", " + "0");
    //                    }

    //                }
    //            }
    //        }
    //    }
    //    if (Session["CustomFieldsMulti"] != null || Session["CustomFieldsMulti"] == null)
    //    {
    //        foreach (Control x in Cntrl.Controls)
    //        {
    //            if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
    //            {

    //                if (x.ID.Substring(0, 28).ToString().ToUpper() == pnlName.ToUpper() + "MULTISELECTLIST")
    //                {

    //                    foreach (ListItem li in ((CheckBoxList)x).Items)
    //                    {
    //                        if (Convert.ToInt32(li.Selected) == 1)
    //                        {
    //                            strmultiselect += " " + li.Value.ToString() + ",";
    //                        }
    //                    }
    //                    strmultiselect += "^";
    //                }
    //            }
    //        }
    //    }
    //}

    #endregion
    //Amitava Sinha 
    //Populate Old Data in the Custom Field
    private void FillOldData(Int32 PatID)
    {
        DataSet dsvalues = null;
        ICustomFields CustomFields;
        try
        {
            DataSet theCustomFields = (DataSet)ViewState["CustomFieldsDS"];
            string theTblName = "";
            if (theCustomFields.Tables[0].Rows.Count > 0)
                theTblName = theCustomFields.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");
            string theColName = "";
            foreach (DataRow theDR in theCustomFields.Tables[0].Rows)
            {
                if (theDR["ControlId"].ToString() != "9")
                {
                    if (theColName == "")
                        theColName = theDR["Label"].ToString();
                    else
                        theColName = theColName + "," + theDR["Label"].ToString();
                }
            }

            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
            dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, Convert.ToInt32(Session["PatientVisitId"]), 0, 0, Convert.ToInt32(ApplicationAccess.NonARTFollowup));
            CustomFieldClinical theCustomManager = new CustomFieldClinical();
            theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "NonART");
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
        finally
        {
            CustomFields = null;
        }

        //string pnlName = Cntrl.ID;

        //DataSet dsvalues = null;
        //ICustomFields CustomFields;
        //Int32 visitID = 0;
        //if (Request.QueryString["visitID"] != null)
        //    visitID = Convert.ToInt32(Request.QueryString["visitid"]);

        //try
        //{
        //    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
        //    dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + TableName.ToString().Replace("-", "_"), sbParameter.ToString(), Convert.ToInt32(PatID.ToString()),0, Convert.ToInt32(visitID.ToString()),0,0,Convert.ToInt32(ApplicationAccess.NonARTFollowup));
        //}
        //catch
        //{
        //}
        //finally
        //{
        //    CustomFields = null;

        //}
        //try
        //{
        //    Boolean blnflag = false;
        //    foreach (DataTable dt in dsvalues.Tables)
        //    {
        //        blnflag = true;
        //    }

        //    if (dsvalues != null && blnflag && dsvalues.Tables[0].Rows.Count > 0)
        //    {
        //        //if any data exist then set the View State
        //        ViewState["CustomFieldsData"] = 1;
        //        foreach (Control x in Cntrl.Controls)
        //        {

        //            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
        //            {
        //                foreach (DataColumn dc in dsvalues.Tables[0].Columns)
        //                {
        //                    if (pnlName.ToUpper() + "SELECTLIST" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
        //                    {
        //                        if (((DropDownList)x).SelectedValue == "0")
        //                        {
        //                            ((DropDownList)x).SelectedValue = dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString();
        //                        }
        //                    }

        //                }
        //            }
        //            if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
        //            {
        //                foreach (DataColumn dc in dsvalues.Tables[0].Columns)
        //                {
        //                    if (pnlName.ToUpper() + "RADIO1" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
        //                    {
        //                        if (dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString() == "True")
        //                        {
        //                            ((HtmlInputRadioButton)x).Checked = true;
        //                        }
        //                    }
        //                    if (pnlName.ToUpper() + "RADIO2" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
        //                    {
        //                        if (dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString() == "False")
        //                        {
        //                            ((HtmlInputRadioButton)x).Checked = true;
        //                        }
        //                    }
        //                }
        //            }
        //            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
        //            {
        //                foreach (DataColumn dc in dsvalues.Tables[0].Columns)
        //                {
        //                    if (pnlName.ToUpper() + "TXT" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
        //                    {
        //                        if (((TextBox)x).Text == "")
        //                        {
        //                            ((TextBox)x).Text = dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString();
        //                            break;
        //                        }
        //                    }
        //                    if (pnlName.ToUpper() + "NUM" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
        //                    {
        //                        if (((TextBox)x).Text == "")
        //                        {
        //                            ((TextBox)x).Text = dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString();
        //                            break;
        //                        }
        //                    }
        //                    if (pnlName.ToUpper() + "DT" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
        //                    {
        //                        if (((TextBox)x).Text == "")
        //                        {
        //                            if (dsvalues.Tables[0].Rows[0][dc.ColumnName] != System.DBNull.Value)
        //                            {
        //                                ((TextBox)x).Text = ((DateTime)dsvalues.Tables[0].Rows[0][dc.ColumnName]).ToString(Session["AppDateFormat"].ToString());
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
        //            {
        //                DataSet dsmvalues = null;
        //                try
        //                {
        //                    string strfldName = pnlName.ToUpper() + "MULTISELECTLIST";
        //                    Int32 stpos = strfldName.Length;
        //                    Int32 enpos = x.ID.Length - stpos;
        //                    strfldName = x.ID.Substring(stpos, enpos).ToString();
        //                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
        //                    dsmvalues = CustomFields.GetCustomFieldValues("[dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_" + strfldName.ToString() + "]", ",[" + strfldName.ToString() + "]", Convert.ToInt32(PatID.ToString()),0, Convert.ToInt32(visitID.ToString()),0,0,Convert.ToInt32(ApplicationAccess.NonARTFollowup));
        //                    if (dsmvalues != null && dsmvalues.Tables[0].Rows.Count > 0)
        //                        Session["CustomFieldsMulti"] = 1;
        //                    foreach (DataRow dr in dsmvalues.Tables[0].Rows)
        //                    {
        //                        foreach (DataColumn dc in dsmvalues.Tables[0].Columns)
        //                        {
        //                            if (pnlName.ToUpper() + "MULTISELECTLIST" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
        //                            {
        //                                foreach (ListItem li in ((CheckBoxList)x).Items)
        //                                {
        //                                    if (li.Value == dr[dc.ColumnName].ToString())
        //                                    {
        //                                        li.Selected = true;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                catch
        //                {
        //                }
        //                finally
        //                {
        //                    CustomFields = null;
        //                    dsmvalues = null;
        //                }

        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach (Control x in Cntrl.Controls)
        //            if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
        //            {
        //                DataSet dsmvalues = null;
        //                try
        //                {
        //                    string strfldName = pnlName.ToUpper() + "MULTISELECTLIST";
        //                    Int32 stpos = strfldName.Length;
        //                    Int32 enpos = x.ID.Length - stpos;
        //                    strfldName = x.ID.Substring(stpos, enpos).ToString();
        //                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
        //                    dsmvalues = CustomFields.GetCustomFieldValues("[dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_" + strfldName.ToString() + "]", ",[" + strfldName.ToString() + "]", Convert.ToInt32(PatID.ToString()),0, Convert.ToInt32(visitID.ToString()),0,0,Convert.ToInt32(ApplicationAccess.NonARTFollowup));
        //                    if (dsmvalues != null && dsmvalues.Tables[0].Rows.Count > 0)
        //                        Session["CustomFieldsMulti"] = 1;


        //                    foreach (DataRow dr in dsmvalues.Tables[0].Rows)
        //                    {
        //                        foreach (DataColumn dc in dsmvalues.Tables[0].Columns)
        //                        {
        //                            if (pnlName.ToUpper() + "MULTISELECTLIST" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
        //                            {
        //                                foreach (ListItem li in ((CheckBoxList)x).Items)
        //                                {
        //                                    if (li.Value == dr[dc.ColumnName].ToString())
        //                                    {
        //                                        li.Selected = true;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                catch
        //                {
        //                }
        //                finally
        //                {
        //                    CustomFields = null;
        //                    dsmvalues = null;
        //                }

        //            }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ex.Message.ToString();
        //}

    }
    #endregion
    #endregion
    private void BindDropdownOrderBy(String EmployeeId)
    {
        DataSet theDS = new DataSet();
        theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        if (theDS.Tables["Mst_Employee"] != null)
        {
            DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
            if (theDV.Table != null)
            {
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                {
                    theDV = new DataView(theDT);
                    theDV.RowFilter = "EmployeeId IN(" + Session["AppUserEmployeeId"].ToString() + "," + EmployeeId + ")";
                    if (theDV.Count > 0)
                        theDT = theUtils.CreateTableFromDataView(theDV);
                }
                //BindManager.BindCombo(ddlPharmOrderedbyName, theDT, "EmployeeName", "EmployeeId"); Bug id 2707...
                //BindUserDropdown(ddlPharmOrderedbyName, string.Empty);
                BindUserDropdown(ddlPharmOrderedbyName, Session["AppUserId"].ToString());

            }
        }
    }

    private void BindUserDropdown(DropDownList DropDownID, String userId)
    {
        Dictionary<int, string> userList = new Dictionary<int, string>();
        CustomFieldClinical.BindUserDropDown(DropDownID, out userList);
        if (!string.IsNullOrEmpty(userId))
        {
            if (userList.ContainsKey(Convert.ToInt32(userId)))
            {
                DropDownID.SelectedValue = userId;
                //SecurityPerTabSignature = userId;
            }
        }
    }


    private void BindDropdownReportedBy(String EmployeeId)
    {
        DataSet theDS = new DataSet();
        theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        if (theDS.Tables["Mst_Employee"] != null)
        {
            DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
            if (theDV.Table != null)
            {
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                {
                    theDV = new DataView(theDT);
                    theDV.RowFilter = "EmployeeId IN(" + Session["AppUserEmployeeId"].ToString() + "," + EmployeeId + ")";
                    if (theDV.Count > 0)
                        theDT = theUtils.CreateTableFromDataView(theDV);
                }
                //BindManager.BindCombo(ddlPharmReportedbyName, theDT, "EmployeeName", "EmployeeId"); Bug ID 2707
                //BindUserDropdown(ddlPharmReportedbyName, string.Empty);
                BindUserDropdown(ddlPharmReportedbyName, Session["AppUserId"].ToString());

            }
        }
    }

    private void BindDropdownCounseler(String EmployeeId)
    {
        DataSet theDS = new DataSet();
        theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
        BindFunctions BindManager = new BindFunctions();
        IQCareUtils theUtils = new IQCareUtils();
        if (theDS.Tables["Mst_Employee"] != null)
        {
            DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
            if (theDV.Table != null)
            {
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                {
                    theDV = new DataView(theDT);
                    theDV.RowFilter = "EmployeeId IN(" + Session["AppUserEmployeeId"].ToString() + "," + EmployeeId + ")";

                    if (theDV.Count > 0)
                        theDT = theUtils.CreateTableFromDataView(theDV);
                }
                //BindManager.BindCombo(ddlCounselerName, theDT, "EmployeeName", "EmployeeId");
                //BindUserDropdown(ddlCounselerName, string.Empty);
                BindUserDropdown(ddlCounselerName, Session["AppUserId"].ToString());

            }
        }
    }

    #region "Create Controls For OI And Other Medication"
    private void LoadData()
    {
        #region CD4 values--Jayant 30-Jan-08

        IFollowup CallBackmgr;
        DataSet theDSCD4 = new DataSet();
        //Session["VDate"] = theExistDS1.Tables[1].Rows[0]["VisitDate"].ToString();
        CallBackmgr = (IFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowup, BusinessProcess.Clinical");
        theDSCD4 = CallBackmgr.GetLatestCD4ViralLoad(Convert.ToInt32(Session["PatientId"]), Convert.ToDateTime(Session["VDate"]));
        if (theDSCD4.Tables[0].Rows[0]["ExistCD4"].ToString() == "1")
        {
            this.txtTestResults.Text = theDSCD4.Tables[0].Rows[0]["CD4TestResult"].ToString();
            DateTime theTmpCD4dt = Convert.ToDateTime(theDSCD4.Tables[0].Rows[0]["LastCD4Date"]);
            this.txtTestResultsDate.Value = theTmpCD4dt.ToString(Session["AppDateFormat"].ToString());
        }
        if (Session["DataQualityFlag"] == null || Session["DataQualityFlag"].ToString() == "")
        {
            if (theDSCD4.Tables[6].Rows.Count > 0)
            {

                Session["DataQualityFlag"] = theDSCD4.Tables[6].Rows[0]["DataQuality"].ToString();
            }

        }
        #endregion

        #region Bind Exist OI Drug
        IQCareUtils theUtils = new IQCareUtils();
        CreateExistDrugTable();
        DataSet theDS2 = (DataSet)Session["MasterData"];
        if (Session["ExixstDS1"] != null)
        {
            DataSet theDS3 = (DataSet)Session["ExixstDS1"];
            foreach (DataRow theDR in theDS3.Tables[0].Rows)
            {
                if ((Convert.ToInt32(theDR["Drug_Pk"].ToString()) != 0))
                {
                    DataView theDV = new DataView(theDS2.Tables[0]);
                    theDV.RowFilter = "DrugId = " + theDR["Drug_Pk"].ToString() + " and DrugId Not IN (277,123)";
                    if (theDV.Count > 0)
                    {
                        DataRow DR = OtherDrugs.NewRow();
                        DR[0] = theDR["Drug_Pk"];
                        DR[1] = theDV[0]["DrugName"];
                        DR[2] = 0;
                        DR[3] = 0;
                        OtherDrugs.Rows.Add(DR);
                    }
                }
                else
                {
                    DataView theDV = new DataView(theDS2.Tables[0]);
                    theDV.RowFilter = "DrugId = " + theDR["GenericID"].ToString() + "and Generic = 1 ";
                    if (theDV.Count > 0)
                    {
                        DataRow DR = OtherDrugs.NewRow();
                        DR[0] = theDV[0]["DrugId"];                         //theDR["Drug_Pk"];
                        DR[1] = theDV[0]["DrugName"];
                        DR[2] = 1;
                        DR[3] = 1;
                        OtherDrugs.Rows.Add(DR);
                    }

                }
            }
        }
        Session["MasterData"] = theDS2;
        Session["OtherDrugs"] = OtherDrugs;
        LoadAdditionalDrugs(OtherDrugs, PnlAddOtherMedication);
        //Custom Fields
        //   if (ViewState["ControlCreated"] != null)
        FillOldData(Convert.ToInt32(Session["PatientId"]));


        if (theDS1.Tables.Count == 0)
        {
            //IQCareMsgBox.Show("NoPharmacyRecordExists", this);--commented by Jayant 09-05-2007
            return;
        }
        else
        {
            foreach (DataRow theDR in theDS1.Tables[0].Rows)
            {
                FillOldDrugData(PnlAddOtherMedication, theDR);
            }


        }
        #endregion


        if (Convert.ToInt32(theExistDS1.Tables[0].Rows[0]["EmployeeID"].ToString()) != 0)
        {
            string name1 = theExistDS1.Tables[0].Rows[0]["EmployeeID"].ToString();
            BindDropdownCounseler(theExistDS1.Tables[0].Rows[0]["EmployeeID"].ToString());
            ddlCounselerName.SelectedValue = theExistDS1.Tables[0].Rows[0]["EmployeeID"].ToString();
        }
        // else {
        // BindDropdown("");
        // }
    }

    private void LoadAdditionalDrugs(DataTable theDT, Panel thePanel)
    {
        thePanel.Controls.Clear();
        foreach (DataRow theDR in theDT.Rows)
        {
            if (thePanel.ID == "PnlAddOtherMedication")
            {
                BindCustomControls(Convert.ToInt32(theDR[0]), Convert.ToInt32(theDR[2]), thePanel);
            }
        }
    }

    private void BindCustomControls(int DrugId, int Generic, Panel MstPanel)
    {
        if (MstPanel.Controls.Count < 1)
        {
            #region "OI & Other Medication"
            Panel thelblPnl = new Panel();
            thelblPnl.ID = "pnlOtherDrug";
            thelblPnl.Height = 20;
            thelblPnl.Width = 880;
            thelblPnl.Controls.Clear();

            Label theLabel = new Label();
            theLabel.ID = "lblOtherDrug";
            theLabel.Text = "OI & Other Medication";
            theLabel.Font.Bold = true;
            thelblPnl.Controls.Add(theLabel);
            MstPanel.Controls.Add(thelblPnl);

            /////////////////////////////////////////////////
            Panel theheaderPnl = new Panel();
            theheaderPnl.ID = "pnlHeaderOtherDrug";
            theheaderPnl.Height = 20;
            theheaderPnl.Width = 880;
            theheaderPnl.Font.Bold = true;
            theheaderPnl.Controls.Clear();

            Label theSP = new Label();
            theSP.ID = "lblDrgSp";
            theSP.Width = 5;
            theSP.Text = "";
            theheaderPnl.Controls.Add(theSP);

            Label theLabel1 = new Label();
            theLabel1.ID = "lblDrgNm";
            theLabel1.Text = "Drug Name";
            theLabel1.Width = 270;
            theheaderPnl.Controls.Add(theLabel1);

            Label theLabel2 = new Label();
            theLabel2.ID = "lblDrgDose";
            theLabel2.Text = "Dose";
            theLabel2.Width = 90;
            theheaderPnl.Controls.Add(theLabel2);

            Label theLabel3 = new Label();
            theLabel3.ID = "lblDrgUnits";
            theLabel3.Text = "Unit";
            theLabel3.Width = 80;
            theheaderPnl.Controls.Add(theLabel3);

            Label theLabel4 = new Label();
            theLabel4.ID = "lblDrgFrequency";
            theLabel4.Text = "Frequency";
            theLabel4.Width = 80;
            theheaderPnl.Controls.Add(theLabel4);

            Label theLabel5 = new Label();
            theLabel5.ID = "lblDrgDuration";
            theLabel5.Text = "Duration";
            theLabel5.Width = 100;
            theheaderPnl.Controls.Add(theLabel5);

            Label theLabel6 = new Label();
            theLabel6.ID = "lblDrgPrescribed";
            theLabel6.Text = "Qty. Prescribed";
            theLabel6.Width = 105;
            theheaderPnl.Controls.Add(theLabel6);

            Label theLabel7 = new Label();
            theLabel7.ID = "lblDrgDispensed";
            theLabel7.Text = "Qty. Dispensed";
            theLabel7.Width = 105;
            theheaderPnl.Controls.Add(theLabel7);

            Label theLabel8 = new Label();
            theLabel8.ID = "lblDrgFinanced";
            theLabel8.Text = "AR";
            theLabel8.Width = 20;
            theheaderPnl.Controls.Add(theLabel8);

            MstPanel.Controls.Add(theheaderPnl);
            #endregion
        }

        Panel thePnl = new Panel();
        thePnl.ID = "pnl" + DrugId;
        thePnl.Height = 20;
        thePnl.Width = 880;
        thePnl.Controls.Clear();

        Label lblStSp = new Label();
        lblStSp.Width = 5;
        lblStSp.ID = "stSpace" + DrugId;
        lblStSp.Text = "";
        lblStSp.Height = 15;
        thePnl.Controls.Add(lblStSp);

        DataView theDV;
        DataSet theDS = (DataSet)Session["MasterData"];

        if (Generic == 0)
        {
            theDV = new DataView(theDS.Tables[0]);
            theDV.RowFilter = "DrugId = " + DrugId + " and Generic = 0";
        }
        else
        {
            theDV = new DataView(theDS.Tables[0]);
            theDV.RowFilter = "DrugId = " + DrugId + " and Generic = 1";
        }

        Label theDrugNm = new Label();
        theDrugNm.ID = "drgNm" + DrugId;
        theDrugNm.Text = theDV[0][1].ToString();
        theDrugNm.Width = 250;
        thePnl.Controls.Add(theDrugNm);

        /////// Space//////
        Label theSpace = new Label();
        theSpace.ID = "theSpace" + DrugId;
        theSpace.Width = 20;
        theSpace.Text = "";
        thePnl.Controls.Add(theSpace);
        ////////////////////

        TextBox theDose = new TextBox();
        theDose.ID = "theDose" + DrugId;
        theDose.Width = 70;
        //theDose.Text = Generic.ToString();
        theDose.Load += new EventHandler(Dose_Load);
        thePnl.Controls.Add(theDose);

        /////// Space//////
        Label theSpace1 = new Label();
        theSpace1.ID = "theSpace1" + DrugId;
        theSpace1.Width = 10;
        theSpace1.Text = "";
        thePnl.Controls.Add(theSpace1);
        ////////////////////

        BindFunctions theBindMgr = new BindFunctions();
        DropDownList theUnit = new DropDownList();
        theUnit.ID = "theUnit" + DrugId;
        theUnit.Width = 70;
        DataTable DTUnit = new DataTable();
        DTUnit = theDS.Tables[17];
        IQCareUtils theUtils = new IQCareUtils();
        //if (Request.QueryString["name"] == "Add")
        if (Convert.ToInt32(Session["PatientVisitId"]) < 1)
        {
            theDV = new DataView(theDS.Tables[17]);
            theDV.RowFilter = "deleteflag = 0";
            DTUnit = theUtils.CreateTableFromDataView(theDV);
        }
        theBindMgr.BindCombo(theUnit, DTUnit, "UnitName", "UnitId");
        thePnl.Controls.Add(theUnit);

        /////// Space//////
        Label theSpace2 = new Label();
        theSpace2.ID = "theSpace2" + DrugId;
        theSpace2.Width = 10;
        theSpace2.Text = "";
        thePnl.Controls.Add(theSpace2);
        ////////////////////

        DropDownList theFrequency = new DropDownList();
        theFrequency.ID = "drgFrequency" + DrugId;
        theFrequency.Width = 70;
        DataTable DTFreq = new DataTable();
        //if (Request.QueryString["name"] == "Add")
        if (Convert.ToInt32(Session["PatientVisitId"]) < 1)
        {
            DTFreq = theDS.Tables[15];
            theBindMgr.BindCombo(theFrequency, DTFreq, "FrequencyName", "FrequencyId");
            thePnl.Controls.Add(theFrequency);
        }
        //else if (Request.QueryString["name"] == "Edit" || Request.QueryString["name"] == "Delete")
        else if ((Convert.ToInt32(Session["PatientVisitId"])) > 1 || Request.QueryString["name"] == "Delete")
        {
            DTFreq = theDS.Tables[21];
            theBindMgr.BindCombo(theFrequency, DTFreq, "FrequencyName", "FrequencyId");
            thePnl.Controls.Add(theFrequency);
        }

        /////// Space//////
        Label theSpace3 = new Label();
        theSpace3.ID = "theSpace3" + DrugId;
        theSpace3.Width = 10;
        theSpace3.Text = "";
        thePnl.Controls.Add(theSpace3);
        ////////////////////

        TextBox theDuration = new TextBox();
        theDuration.ID = "drgDuration" + DrugId;
        theDuration.Width = 90;
        theDuration.Text = "";
        theDuration.Load += new EventHandler(DecimalText_Load);
        thePnl.Controls.Add(theDuration);

        ////////////Space////////////////////////
        Label theSpace4 = new Label();
        theSpace4.ID = "theSpace4" + DrugId;
        theSpace4.Width = 10;
        theSpace4.Text = "";
        thePnl.Controls.Add(theSpace4);
        ////////////////////////////////////////

        TextBox theQtyPrescribed = new TextBox();
        theQtyPrescribed.ID = "drgQtyPrescribed" + DrugId;
        theQtyPrescribed.Width = 90;
        theQtyPrescribed.Text = "";
        theQtyPrescribed.Load += new EventHandler(DecimalText_Load);
        thePnl.Controls.Add(theQtyPrescribed);

        ////////////Space////////////////////////
        Label theSpace5 = new Label();
        theSpace5.ID = "theSpace5" + DrugId;
        theSpace5.Width = 10;
        theSpace5.Text = "";
        thePnl.Controls.Add(theSpace5);
        ////////////////////////////////////////

        TextBox theQtyDispensed = new TextBox();
        theQtyDispensed.ID = "drgQtyDispensed" + DrugId;
        theQtyDispensed.Width = 90;
        theQtyDispensed.Text = "";
        theQtyDispensed.Load += new EventHandler(DecimalText_Load);
        thePnl.Controls.Add(theQtyDispensed);

        ////////////Space////////////////////////
        Label theSpace6 = new Label();
        theSpace6.ID = "theSpace6" + DrugId;
        theSpace6.Width = 10;
        theSpace6.Text = "";
        thePnl.Controls.Add(theSpace6);
        ////////////////////////////////////////

        CheckBox theFinChk = new CheckBox();
        theFinChk.ID = "FinChk" + DrugId;
        theFinChk.Width = 10;
        theFinChk.Text = "";
        thePnl.Controls.Add(theFinChk);

        ////////////Space////////////////////////
        Label theSpace7 = new Label();
        theSpace7.ID = "theSpace7" + DrugId;
        theSpace7.Width = 10;
        theSpace7.Text = "";
        thePnl.Controls.Add(theSpace7);
        ////////////////////////////////////////

        MstPanel.Controls.Add(thePnl);
        Panel thePnlspace = new Panel();
        thePnlspace.ID = "pnlspace_" + DrugId;
        thePnlspace.Height = 30;//was 3 prevoiusly
        thePnlspace.Width = 880;
        thePnlspace.Controls.Clear();
        MstPanel.Controls.Add(thePnlspace);

    }
    void DecimalText_Load(object sender, EventArgs e)
    {
        TextBox tbox = (TextBox)sender;
        tbox.MaxLength = 4;
        tbox.Attributes.Add("onkeyup", "chkDecimal('" + tbox.ClientID + "')");
    }
    void Control_Load(object sender, EventArgs e)
    {
        TextBox tbox = (TextBox)sender;
        tbox.MaxLength = 3;
        tbox.Attributes.Add("onkeyup", "chkPostiveInteger('" + tbox.ClientID + "')");
    }

    void Dose_Load(object sender, EventArgs e)
    {
        TextBox tbox = (TextBox)sender;
        tbox.MaxLength = 8;
        tbox.Attributes.Add("onkeyup", "chkNumeric('" + tbox.ClientID + "'); AddBoundary('" + tbox.ClientID + "','0','99999999')");
        //--Jayant comment 23Jan08

        //int DrugId = Convert.ToInt32(tbox.ID.Substring(7, tbox.ID.Length - 7));
        //DataView theDV = new DataView(((DataSet)Session["MasterData"]).Tables[18]);
        //if (Convert.ToDecimal(tbox.Text) == 0)                                               //if (Convert.ToInt32(tbox.Text) == 0)
        //{
        //    theDV.RowFilter = "GenericId= " + DrugId.ToString();

        //    //tbox.Text = "";
        //}
        //else
        //{
        //    theDV.RowFilter = "DrugId= " + DrugId.ToString();
        //}

        //------

        //------ Rupesh 19Dec07 starts----

        /* earlier it was coming fron lnk_nonARVDrugGeneric now from lnk_druggeneric
        if (theDV.Count > 0)
        {
            tbox.Attributes.Add("onkeyup", "chkNumeric('" + tbox.ClientID + "'); AddBoundary('" + tbox.ClientID + "','" + theDV[0]["MinDose"] + "','" + theDV[0]["MaxDose"] + "')");
        }
        else
        {
            tbox.Attributes.Add("onkeyup", "chkNumeric('" + tbox.ClientID + "'); AddBoundary('" + tbox.ClientID + "','0','99999999')");
        }
        */
        //---- Jayant 16Jan08            
        /*
        if (theDV.Count > 0)
        {
            tbox.Attributes.Add("onkeyup", "chkNumeric('" + tbox.ClientID + "'); AddBoundary('" + tbox.ClientID + "','0','99999999')");
        }*/
        //coment ends Jayant 16Jan08

        //------ Rupesh 19Dec07 ends----

    }

    private DataTable CreateTable()
    {
        DataTable theDT = new DataTable();
        theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("GenericId", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("Dose", System.Type.GetType("System.Decimal"));
        theDT.Columns.Add("UnitId", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("StrengthId", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("FrequencyId", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("Duration", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("QtyPrescribed", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("QtyDispensed", System.Type.GetType("System.Int32"));
        theDT.Columns.Add("Financed", System.Type.GetType("System.Int32"));

        return theDT;

    }

    /**************** Save OI Drug ********************/
    private DataTable MakeDrugTable(Control theContainer)
    {
        DataTable theDT = new DataTable();
        #region "Variables"
        decimal Dose = 0;
        int UnitId = 0;
        int theStrengthId = 0;
        int theFrequencyId = 0;
        decimal theDuration = 0;
        decimal theQtyPrescribed = 0;
        decimal theQtyDispensed = 0;
        //int theDuration = 0;
        //int theQtyPrescribed = 0;
        //int theQtyDispensed = 0;
        int theFinanced = 99;
        #endregion

        if (Session["Data"] == null)
        {
            theDT = CreateTable();
        }
        else
        {
            theDT = (DataTable)Session["Data"];
        }


        if (theContainer.ID == "PnlAddOtherMedication")
        {
            #region "Add Drugs"
            DataTable theOtherDrug = (DataTable)Session["OtherDrugs"];
            if (theOtherDrug == null)
                return theDT;
            foreach (DataRow theDR in theOtherDrug.Rows)
            {
                DataRow theRow;
                foreach (Control y in theContainer.Controls)
                {
                    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                    {
                        foreach (Control x in y.Controls)
                        {
                            if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
                            {
                                MakeDrugTable(x);
                            }
                            else
                            {
                                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                                {
                                    if (x.ID.EndsWith(theDR["DrugId"].ToString()) && x.ID.StartsWith("theUnit"))
                                    {
                                        UnitId = Convert.ToInt32(((DropDownList)x).SelectedValue);
                                    }
                                    if (x.ID.EndsWith(theDR["DrugId"].ToString()) && x.ID.StartsWith("drgFrequency"))
                                    {
                                        theFrequencyId = Convert.ToInt32(((DropDownList)x).SelectedValue);
                                    }
                                }
                                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                {
                                    if (x.ID.EndsWith(theDR["DrugId"].ToString()) && x.ID.StartsWith("theDose"))
                                    {
                                        if (((TextBox)x).Text != "")
                                        {
                                            Dose = Convert.ToDecimal(((TextBox)x).Text);
                                        }
                                    }

                                    if (x.ID.EndsWith(theDR["DrugId"].ToString()) && x.ID.StartsWith("drgDuration"))
                                    {
                                        if (((TextBox)x).Text != "")
                                        {
                                            theDuration = Convert.ToDecimal(((TextBox)x).Text);
                                            // theDuration = Convert.ToInt32(((TextBox)x).Text);
                                        }
                                    }
                                    if (x.ID.EndsWith(theDR["DrugId"].ToString()) && x.ID.StartsWith("drgQtyPrescribed"))
                                    {
                                        if (((TextBox)x).Text != "")
                                        {
                                            theQtyPrescribed = Convert.ToDecimal(((TextBox)x).Text);
                                            //theQtyPrescribed = Convert.ToInt32(((TextBox)x).Text);
                                        }
                                    }
                                    if (x.ID.EndsWith(theDR["DrugId"].ToString()) && x.ID.StartsWith("drgQtyDispensed"))
                                    {
                                        if (((TextBox)x).Text != "")
                                        {
                                            theQtyDispensed = Convert.ToDecimal(((TextBox)x).Text);
                                            //theQtyDispensed = Convert.ToInt32(((TextBox)x).Text);
                                        }
                                    }
                                }
                                if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                {
                                    if (x.ID.EndsWith(theDR["DrugId"].ToString()) && x.ID.StartsWith("FinChk"))
                                    {
                                        if (((CheckBox)x).Checked == true)
                                            theFinanced = 1;
                                        else
                                            theFinanced = 0;
                                    }
                                }
                            }
                        }
                    }
                }
                if (UnitId != 0 || theFrequencyId != 0 || Dose != 0 || theDuration != 0 || theQtyPrescribed != 0 || theQtyDispensed != 0 || theFinanced != 99)
                {
                    theRow = theDT.NewRow();
                    if (Convert.ToInt32(theDR["Generic"]) == 0)
                    {
                        theRow["DrugId"] = theDR["DrugId"];
                        theRow["GenericId"] = 0;
                    }
                    else
                    {
                        theRow["DrugId"] = 0;
                        theRow["GenericId"] = theDR["DrugId"];
                    }
                    theRow["Dose"] = Dose;
                    theRow["UnitId"] = UnitId;
                    theRow["StrengthId"] = 0;
                    theRow["FrequencyId"] = theFrequencyId;
                    theRow["Duration"] = theDuration;
                    theRow["QtyPrescribed"] = theQtyPrescribed;
                    theRow["QtyDispensed"] = theQtyDispensed;
                    theRow["Financed"] = theFinanced;
                    theDT.Rows.Add(theRow);
                    #region "Reset Variables
                    Dose = 0;
                    UnitId = 0;
                    theStrengthId = 0;
                    theFrequencyId = 0;
                    theDuration = 0;
                    theQtyPrescribed = 0;
                    theQtyDispensed = 0;
                    theFinanced = 99;
                    #endregion
                }
            }
        }
            #endregion
        Session["SaveOIDrug"] = theDT;
        return theDT;

    }

    /************* Fill Exist OI Drug Details ***************/
    private void FillOldDrugData(Control Cntrl, DataRow theDR)
    {
        int y = 0;
        int DrugId;
        if (Convert.ToInt32(theDR["Drug_PK"]) == 0)
        {
            DrugId = Convert.ToInt32(theDR["GenericId"]);
        }
        else
        {
            DrugId = Convert.ToInt32(theDR["Drug_PK"]);
        }
        foreach (Control x in Cntrl.Controls)
        {
            if (x.GetType() == typeof(System.Web.UI.WebControls.Panel))
            {
                FillOldDrugData(x, theDR);
            }
            else
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    if (x.ID.StartsWith("drgStrength"))
                    {
                        y = Convert.ToInt32(x.ID.Substring(11, x.ID.Length - 11));
                        if (y == DrugId)
                        {
                            ((DropDownList)x).SelectedValue = theDR["StrengthId"].ToString();
                        }
                    }
                    if (x.ID.StartsWith("theUnit"))
                    {
                        y = Convert.ToInt32(x.ID.Substring(7, x.ID.Length - 7));
                        if (y == DrugId)
                        {
                            ((DropDownList)x).SelectedValue = theDR["UnitId"].ToString();
                        }
                    }
                    if (x.ID.StartsWith("drgFrequency"))
                    {
                        y = Convert.ToInt32(x.ID.Substring(12, x.ID.Length - 12));
                        if (y == DrugId)
                        {
                            ((DropDownList)x).SelectedValue = theDR["FrequencyId"].ToString();
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    if (x.ID.StartsWith("theDose"))
                    {
                        y = Convert.ToInt32(x.ID.Substring(7, x.ID.Length - 7));
                        if (y == DrugId)
                        {

                            ((TextBox)x).Text = theDR["Dose"].ToString();

                        }
                    }
                    if (x.ID.StartsWith("drgDuration"))
                    {
                        y = Convert.ToInt32(x.ID.Substring(11, x.ID.Length - 11));
                        if (y == DrugId)
                        {
                            ((TextBox)x).Text = theDR["Duration"].ToString();
                        }
                    }
                    if (x.ID.StartsWith("drgQtyPrescribed"))
                    {
                        y = Convert.ToInt32(x.ID.Substring(16, x.ID.Length - 16));
                        if (y == DrugId)
                        {
                            ((TextBox)x).Text = theDR["OrderedQuantity"].ToString();
                        }
                    }
                    if (x.ID.StartsWith("drgQtyDispensed"))
                    {
                        y = Convert.ToInt32(x.ID.Substring(15, x.ID.Length - 15));
                        if (y == DrugId)
                        {
                            ((TextBox)x).Text = theDR["DispensedQuantity"].ToString();
                        }
                    }
                }

                if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                {
                    if (x.ID.StartsWith("FinChk"))
                    {
                        y = Convert.ToInt32(x.ID.Substring(6, x.ID.Length - 6));
                        if (y == DrugId)
                        {
                            if (theDR["Financed"].ToString() != "")
                            {
                                if (Convert.ToInt32(theDR["Financed"].ToString()) == 1)
                                {
                                    ((CheckBox)x).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox)x).Checked = false;
                                }
                            }
                            else
                            {
                                ((CheckBox)x).Checked = false;
                            }

                        }
                    }
                }
            }
        }
    }
    #endregion
    #region Events
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        if (Convert.ToInt32(Session["TechnicalAreaId"]) != 2)
        {
            btnSave.Enabled = false;
            btnQualityCheck.Enabled = false;
        }
        Init_Form();


        // txtvisitDate.Attributes.Add("onblur", "document.form(0).submit"); 
        //RTyagi..19Feb.07
        /***************** Check For User Rights ****************/
        AuthenticationManager Authentiaction = new AuthenticationManager();

        if (Authentiaction.HasFunctionRight(ApplicationAccess.NonARTFollowup, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
        {
            btnPrint.Enabled = false;

        }
        //if (Request.QueryString["name"] == "Add")
        if (Convert.ToInt32(Session["PatientVisitId"]) < 1)
        {

            if (Authentiaction.HasFunctionRight(ApplicationAccess.NonARTFollowup, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnSave.Enabled = false;
                btnQualityCheck.Enabled = false;
            }
        }

        //if (Request.QueryString["name"] == "Edit")
        if ((Convert.ToInt32(Session["PatientVisitId"])) > 1)
        {

            if (Authentiaction.HasFunctionRight(ApplicationAccess.NonARTFollowup, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                //int PatientID = Convert.ToInt32(Session["PatientId"]);
                string theUrl = "";
                theUrl = string.Format("{0}", "frmPatient_History.aspx");
                Response.Redirect(theUrl);
            }
            else if (Authentiaction.HasFunctionRight(ApplicationAccess.NonARTFollowup, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
            {
                btnSave.Enabled = false;
                btnQualityCheck.Enabled = false;
            }
            if (Session["HIVPatientStatus"].ToString() == "1")
            {
                btnSave.Enabled = false;
                btnQualityCheck.Enabled = false;
            }
            //Privilages for Care End
            if (Convert.ToString(Session["CareEndFlag"]) == "1" && Convert.ToString(Session["CareendedStatus"]) == "1")
            {
                btnSave.Enabled = true;
                btnQualityCheck.Enabled = true;
            }
        }

        if (Request.QueryString["name"] == "Delete")
        {
            if (Authentiaction.HasFunctionRight(ApplicationAccess.NonARTFollowup, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                string theUrl = "";
                theUrl = string.Format("{0}", "frmClinical_DeleteForm.aspx");
                Response.Redirect(theUrl);
            }
            else if (Authentiaction.HasFunctionRight(ApplicationAccess.NonARTFollowup, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
            {
                btnSave.Text = "Delete";
                btnSave.Enabled = false;
                btnQualityCheck.Visible = false;
            }
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
        {
            IQCareMsgBox.Show("SessionExpired", this);
            Response.Redirect("~/frmlogin.aspx", true);
        }
        Ajax.Utility.RegisterTypeForAjax(typeof(frmClinical_NonARTFollowUp));

        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
        //(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Non-ART Follow-Up";
        //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Non-ART Follow-Up";
        Session["PtnRegCTC"] = "";
        Session["CustomfrmDrug"] = "";
        Session["CustomfrmLab"] = "";


        //if (Request.QueryString["sts"] != null)
        if (Session["HIVPatientStatus"] != null)
        {
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["HIVPatientStatus"].ToString();
            if (Session["HIVPatientStatus"].ToString() == "1")
            {
                //btnSave.Enabled = false;
                //btnQualityCheck.Enabled = false;
            }
        }


        txtvisitDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDate('" + Session["AppCurrentDate"] + "', '" + txtvisitDate.ClientID + "', '" + txtvisitDate.ClientID + "'), addDays(), SendCD4()");
        GetICallBackFunction();
        //Amitava Sinha
        PutCustomControl();
        try
        {
            if (theDS != null)
            {
                Session["MasterData"] = theDS;
                //Session["DataQualityFlag"] = theDS.Tables[16].Rows[0]["DataQuality"];
            }

            if (theDS1 != null)
            {
                if (theDS1.Tables.Count != 0)
                {
                    Session["ExixstDS1"] = theDS1;
                }
            }

            if (theExistVisitDS != null)
            {
                Session["ExistData"] = theExistVisitDS.Tables[0];
            }

            if (theExistVisitDS != null)
            {
                if (theExistVisitDS.Tables[1].Rows.Count != 0)
                {
                    Session["ExistNonARTVisits"] = theExistVisitDS.Tables[1];
                }
            }

            MsgBuilder theBuilder = new MsgBuilder();
            CheckHideProperty();
            if (IsPostBack != true)
            {

                //BMIAttributes();
                //ViewState["VisitId"] = Request.QueryString["visitid"];
                //ViewState["PharmacyId"] = Request.QueryString["PharmacyID"];
                //ViewState["VisitId"] = Session["PatientVisitId"];
                //ViewState["PharmacyId"] = Session["PharmaID"];
                Session["SaveOIDrugs"] = null;
                Session["Data"] = null;
                Session["OtherDrugs"] = null;
                Session["DataQualityFlag"] = null;
                Session["BtnCompClicked"] = "0";
                ViewState["Pregstatus"] = "0";
                txtvisitDate.Focus();
                if (Session["PatientId"] != null)
                {
                    int PatientID = Convert.ToInt32(Session["PatientId"]);
                }
                Session["theSaveDS"] = null;
                if (theDS != null)
                {
                    Session["MasterDrugData"] = theDS.Tables[0];
                }
                if (Session["AppUserId"] != null)
                {
                    Session["UserID"] = Session["AppUserId"].ToString();
                }
                if (Session["AppLocationId"] != null)
                {
                    Session["LocationId"] = Convert.ToInt32(Session["AppLocationId"]);
                }

                string name = ddlCounselerName.SelectedValue;
                //if (Request.QueryString["name"] == "Add")
                if (Convert.ToInt32(Session["PatientVisitId"]) < 1)
                {
                    CheckIEVisitDate();
                    ///*********DQ Feb 19.. **************/

                    MsgBuilder theDQ = new MsgBuilder();
                    theDQ.DataElements["Name"] = "DQ Checked complete. Form Marked as DQ Checked. Do you want to close";
                    IQCareMsgBox.ShowConfirm("NonARTFollowUp", theDQ, theBtn1);

                    theBuilder.DataElements["FormName"] = "NonART FollowUp";
                    IQCareMsgBox.ShowConfirm("AddClinicalRecord", theBuilder, theBtn);
                }



                //else if (Request.QueryString["name"] == "Edit")
                if (((Convert.ToInt32(Session["PatientVisitId"])) > 1) || (Request.QueryString["name"] == ""))
                {

                    //Jayanta Kr. Das
                    (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["HIVPatientStatus"].ToString();
                    //Session["OtherDrugs"] = OtherDrugs; 
                    //ViewState["VisitID_CC"] = Session["PatientVisitId"];
                    if ((Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text == "1")
                    {
                        //btnSave.Enabled = false;
                        //btnQualityCheck.Enabled = false;
                    }
                    ///*********DQ Feb 19.. **************/
                    MsgBuilder theDQ = new MsgBuilder();
                    theDQ.DataElements["Name"] = "DQ Checked complete. Form Marked as DQ Checked. Do you want to close";
                    IQCareMsgBox.ShowConfirm("NonARTFollowUp", theDQ, theBtn1);

                    theBuilder.DataElements["FormName"] = "NonART FollowUp";
                    IQCareMsgBox.ShowConfirm("UpdateClinicalRecord", theBuilder, theBtn);
                    LoadData();
                    if (Session["DataQualityFlag"] != null && Session["DataQualityFlag"].ToString() == "1")
                    {

                        btnQualityCheck.CssClass = "greenbutton";

                    }
                }
                if (Request.QueryString["name"] == "Delete")
                {
                    //ViewState["VisitID_CC"] = Session["PatientVisitId"];
                    theBuilder.DataElements["FormName"] = "NonART FollowUp";
                    IQCareMsgBox.ShowConfirm("DeleteForm", theBuilder, btnSave);

                    btnSave.Text = "Delete";
                    btnQualityCheck.Visible = false;
                    LoadData();
                }

            }
            else
            {

                #region "Other Drugs"
                if ((DataTable)Application["OtherDrugs"] != null)
                {
                    Session["OtherDrugs"] = (DataTable)Application["OtherDrugs"];
                    Session["MasterDrugData"] = (DataTable)Application["MasterData"];
                    Application.Remove("OtherDrugs");
                    Application.Remove("MasterData");

                    //----Rupesh 03-Jan-08 start----
                    foreach (DataRow theDR in ((DataTable)Session["OtherDrugs"]).Rows)
                    {
                        if (Convert.ToInt32(theDR["Generic"]) == 0)
                            theDR["DrugId"] = theDR["DrugId"] + "8888";
                        else
                            theDR["DrugId"] = theDR["DrugId"] + "9999";
                    }
                    //----Rupesh 03-Jan-08 end----

                }
                #endregion

                if ((DataTable)Session["OtherDrugs"] != null)
                {
                    LoadAdditionalDrugs((DataTable)Session["OtherDrugs"], PnlAddOtherMedication);
                }
                ShowOIAIDS();
                ShowHideControls();
                if (Session["BtnCompClicked"].ToString() == "1")
                    DQFieldValidation();
            }

        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
            return;
        }
        finally
        {
            NonARTManager = null;
        }
    }

    protected void OtherMedication_Click(object sender, EventArgs e)
    {
        Application.Add("MasterData", (DataTable)Session["MasterDrugData"]);
        Application.Add("SelectedDrug", (DataTable)Session["OtherDrugs"]);
        string theScript;
        theScript = "<script language='javascript' id='DrgPopup'>\n";
        theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=0','DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
        theScript += "</script>\n";
        RegisterStartupScript("DrgPopup", theScript);
        ShowHideControls();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Session["BtnCompClicked"] = "0";
            Boolean blnQtyDispensed = false;
            Session["DataQualityFlag"] = "0";
            if (Request.QueryString["name"] == "Delete")
            {
                DeleteForm();
            }
            else
            {
                if (FieldValidation() == false)
                {
                    return;
                }
                theVisitDate = Convert.ToDateTime(txtvisitDate.Value.ToString());
                if (Session["Paperless"].ToString() == "1")
                {
                    theDS = NonARTDetails();
                    if (theDS.Tables[4].Rows.Count > 0)
                    {
                        for (int i = 0; i < theDS.Tables[4].Rows.Count; i++)
                        {
                            if (theDS.Tables[4].Rows[i]["QtyDispensed"].ToString() != "99999")
                            {
                                blnQtyDispensed = true;
                            }
                        }
                    }
                    if (blnQtyDispensed == true)
                    {
                        if (FieldValidationPaperless() == false)
                        {
                            return;
                        }
                    }
                }
                SaveData();





                //string script = "<script language = 'javascript' defer ='defer' id = 'aftersavefunction'>\n";
                //script += "document.getElementById('" + theBtn.ClientID + "').click();\n";
                //script += "</script>\n";
                //RegisterStartupScript("aftersavefunction", script);
                //return;
                ////if (Request.QueryString["name"] == "Add")
                ////{
                ////    Session["DataQualityFlag"] = 0;
                ////    if (Session["ExistVisitDate"] != null && (Session["theSaveDS"] == null))
                ////    {
                ////        int PatientId = Convert.ToInt32(Request.QueryString["PatientId"]);
                ////        //Naveen- Update Session
                ////        CheckIEVisitDate();
                ////        if (theVisitDate == Convert.ToDateTime(Session["ExistVisitDate"].ToString()))
                ////        {
                ////            /**********Check Later for Messages *************/
                ////            IQCareMsgBox.Show("ClinicalRecordExist", this);
                ////            return;
                ////        }
                ////        else
                ////        {
                ////            SaveData();

                ////        }
                ////    }
                ////    else
                ////    {
                ////        SaveData();
                ////    }
                ////}
                ////else if (Request.QueryString["name"] == "Edit")
                ////{
                ////    Session["DataQualityFlag"] = 0;

                ////    /************** To Check Non-ART Other Visit Dates ******************/
                ////    if (CheckNonARTVisitDates() == false)
                ////    {
                ////        return;
                ////    }
                ////    SaveData();
                ////}
            }
        }
        catch (Exception err)
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theMsg, this);
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        int PatientId = Convert.ToInt32(Session["PatientId"]);
        string theUrl;

        //if (Request.QueryString["name"] == "Add")
        if (Convert.ToInt32(Session["PatientVisitId"]) < 1)
        {
            theUrl = string.Format("{0}", "frmPatient_Home.aspx");
        }
        //else if (Request.QueryString["name"] == "Edit")
        else if ((Request.QueryString["name"] == "") || (Convert.ToInt32(Session["PatientVisitId"]) > 1))
        {
            theUrl = string.Format("{0}", "frmPatient_History.aspx");
        }
        else
        {
            theUrl = string.Format("{0}", "frmClinical_DeleteForm.aspx");
        }
        Response.Redirect(theUrl);
    }

    protected void btnQualityCheck_Click(object sender, EventArgs e)
    {
        try
        {
            Session["BtnCompClicked"] = "1";
            theDS = NonARTDetails();
            DataTable theDT = MakeDrugTable(PnlAddOtherMedication);

            string msg = DQFieldValidation();
            if (msg.Length > 69)
            {
                MsgBuilder theBuilder1 = new MsgBuilder();
                theBuilder1.DataElements["MessageText"] = msg;
                IQCareMsgBox.Show("#C1", theBuilder1, this);
                return;
            }
            else
            {
                Session["DataQualityFlag"] = "1";
            }
            if (DQFieldValidation1() == false)
            {
                return;
            }
            //Naveen- Update Message
            SaveData();
            //theVisitDate = Convert.ToDateTime(txtvisitDate.Value.ToString());

            //if (Request.QueryString["name"] == "Add")
            //{
            //    if (Session["ExistVisitDate"] != null && (Session["theSaveDS"] == null))
            //    {
            //        int PatientId = Convert.ToInt32(Request.QueryString["PatientId"]);
            //        if (theVisitDate == Convert.ToDateTime(Session["ExistVisitDate"].ToString()))
            //        {
            //            /**********Check Later for Messages *************/
            //            IQCareMsgBox.Show("ClinicalRecordExist", this);
            //            return;
            //        }
            //        else
            //        {
            //            SaveData();
            //        }
            //    }
            //    else
            //    {
            //        SaveData();
            //    }
            //}
            //else if (Request.QueryString["name"] == "Edit")
            //{
            //    /************** To Check Non-ART Other Visit Dates ******************/
            //    if (CheckNonARTVisitDates() == false)
            //    {
            //        return;
            //    }
            //    SaveData();
            //}
        }
        catch (Exception err)
        {
            MsgBuilder theMsg = new MsgBuilder();
            theMsg.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theMsg, this);
        }
    }

    protected void theBtn_Click(object sender, EventArgs e)
    {
        //int PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
        //string theUrl = "";
        //if (Request.QueryString["name"] == "Add")
        //{
        //   theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", PatientID);

        //}
        //else if (Request.QueryString["name"] == "Edit")
        //{
        //    theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", PatientID, Request.QueryString["sts"].ToString());
        //}
        Session.Remove("MasterDrugData");
        Session.Remove("OtherDrugs");
        Session.Remove("SaveOIDrug");
        Session.Remove("theSaveDS");
        Session.Remove("ExistVisitDate");
        Session.Remove("DataQualityFlag");
        Session.Remove("ExixstDS1");
        Session.Remove("ExistData");
        Session.Remove("ExistNonARTVisits");
        Session.Remove("OtherDrugs");
        Session.Remove("BtnCompClicked");
        Session.Remove("LocationId");
        //Response.Redirect(theUrl);  
    }

    protected void theBtn1_Click(object sender, EventArgs e)
    {
        //int PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
        //string theUrl = "";
        //if (Request.QueryString["name"] == "Add")
        //{
        //    theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", PatientID);

        //}
        //else
        //{
        //    theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", PatientID, Request.QueryString["sts"].ToString());
        //}
        Session.Remove("MasterDrugData");
        Session.Remove("OtherDrugs");
        Session.Remove("SaveOIDrug");
        Session.Remove("theSaveDS");
        Session.Remove("ExistVisitDate");
        Session.Remove("DataQualityFlag");
        Session.Remove("MasterDrugData");
        Session.Remove("OtherDrugs");
        Session.Remove("SaveOIDrug");
        Session.Remove("theSaveDS");
        Session.Remove("ExistVisitDate");
        Session.Remove("DataQualityFlag");
        Session.Remove("ExixstDS1");
        Session.Remove("ExistData");
        Session.Remove("ExistNonARTVisits");
        Session.Remove("OtherDrugs");
        Session.Remove("BtnCompClicked");
        Session.Remove("LocationId");
        //Response.Redirect(theUrl);
    }
    #endregion

    #region ICallbackEventHandler Members
    public string GetCallbackResult()
    {
        string thestr = str;
        return thestr;
    }
    public void RaiseCallbackEvent(string eventArgument)
    {
        int PatientID = Convert.ToInt32(Session["PatientId"]);
        IFollowup CallBackmgr;
        try
        {
            IInitialEval CallBackmgrIE = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
            CallBackmgr = (IFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowup, BusinessProcess.Clinical");
            DataSet theDSCallBack = CallBackmgr.GetLatestCD4ViralLoad(PatientID, Convert.ToDateTime(eventArgument.Trim()));
            DataSet theDSHeight = CallBackmgr.GetLatestHeight(PatientID, Convert.ToDateTime(eventArgument.Trim()));
            if (theDSCallBack != null && theDSCallBack.Tables[0].Rows.Count > 0)
            {
                str = theDSCallBack.GetXml();
            }
            str += "zz";
            if (theDSHeight != null && theDSHeight.Tables[0].Rows.Count > 0)
            {
                str += theDSHeight.GetXml();
            }

            str += "zz";
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                theDSCallBack.Clear();
                theDSCallBack = CallBackmgrIE.GetPregnantStatus(Convert.ToInt32(Session["PatientId"]), eventArgument);
                str += theDSCallBack.GetXml();
            }
            else
            {
                theDSCallBack.Clear();
                str += theDSCallBack.GetXml();
            }
        }
        catch
        {
        }
        finally
        {
            CallBackmgr = null;
        }
    }
    #endregion

}
