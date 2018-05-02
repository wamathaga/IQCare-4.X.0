using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using Telerik.Web.UI;


namespace Application.Presentation
{
    public class BindFunctions
    {

        #region "Constructor"
        public BindFunctions()
        {

        }
        #endregion

        public void Win_BindCombo(ComboBox theDropDown, DataTable theDT, string theTextField, string theValueField)
        {
            if (theDT.Rows.Count > 0)
            {
                DataRow[] DR = theDT.Select("" + theValueField + " = 0");
                if (DR.Length < 1)
                {
                    DataRow theDR = theDT.NewRow();
                    theDR["" + theTextField + ""] = "Select";
                    theDR["" + theValueField + ""] = 0;
                    theDT.Rows.InsertAt(theDR, 0);
                }

                theDropDown.DataSource = theDT;
                theDropDown.DisplayMember = theTextField;
                theDropDown.ValueMember = theValueField;
            }
        }

        public void Win_BindCheckListBox(CheckedListBox theCheckListBox, DataTable theDT, string theTextField, string theValueField)
        {
            theCheckListBox.DataSource = theDT;
            theCheckListBox.DisplayMember = theTextField;
            theCheckListBox.ValueMember = theValueField;
        }

        public void Win_BindListBox(System.Windows.Forms.ListBox theListBox, DataTable theDT, string theTextField, string theValueField)
        {
            theListBox.DataSource = theDT;
            theListBox.DisplayMember = theTextField;
            theListBox.ValueMember = theValueField;
        }
        public void BindCombo(RadComboBox theComboBox, DataTable theDT, string theTextField, string theValueField)
        {
            BindCombo(theDT, theTextField, theValueField, null, theComboBox);
        }

        public void BindCombo(DropDownList theDropDown, DataTable theDT, string theTextField, string theValueField)
        {
            BindCombo(theDT, theTextField, theValueField, theDropDown, null);
        }

        private void BindCombo(DataTable theDT, string theTextField, string theValueField, DropDownList theDropDown = null, RadComboBox theComboBox = null)
        {
            theDropDown.Items.Clear();
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
            else
            {
                theComboBox.DataSource = theDT;
                theComboBox.DataTextField = theTextField;
                theComboBox.DataValueField = theValueField;
                theComboBox.DataBind();
            }
        }

        public void BindList(System.Web.UI.WebControls.ListBox theListBox, DataTable theDT, string theTextField, string theValueField)
        {
            theListBox.DataSource = theDT;
            theListBox.DataTextField = theTextField;
            theListBox.DataValueField = theValueField;
            theListBox.DataBind();
        }

        public void BindCheckedList(CheckBoxList theListBox, DataTable theDT, string theTextField, string theValueField)
        {
            theListBox.DataSource = theDT;
            theListBox.DataTextField = theTextField;
            theListBox.DataValueField = theValueField;
            theListBox.DataBind();
        }

        public void RadioButtonList(RadioButtonList theRadioButton, DataTable theDT, string theTextField, string theValueField)
        {
            theRadioButton.DataSource = theDT;
            theRadioButton.DataTextField = theTextField;
            theRadioButton.DataValueField = theValueField;
            theRadioButton.DataBind();
        }

        public void CreateCheckedList(System.Web.UI.WebControls.Panel thePannel, DataTable theDT, string Attribute, string Event)
        {
            int i = 0;
            bool FlgOther = false;
            System.Web.UI.WebControls.TextBox theTxtBox = new System.Web.UI.WebControls.TextBox();
            for (i = 0; i < theDT.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox theChkBox = new System.Web.UI.WebControls.CheckBox();
                theChkBox.ID = thePannel.ID + '-' + theDT.Rows[i][0].ToString() + '-' + i;
                theChkBox.Text = theDT.Rows[i][1].ToString();
                theChkBox.Style.Add("margin-left", "25px");

                if (Attribute != "")
                {
                    string Attr = "";
                    string[] theAttr = Attribute.Split('%');
                    if (theAttr.Length > 0)
                        Attr = theAttr[0] + "%" + theChkBox.ClientID + theAttr[1];
                    else
                        Attr = theAttr[0];
                    theChkBox.Attributes.Add(Event, Attr);
                }
                else
                {
                    theChkBox.Attributes.Add("onclick", "GetCheckboxId('" + thePannel.ClientID + '-' + theDT.Rows[i][0].ToString() + '-' + i + "')");
                }
                if (theChkBox.Text == "Other" || theChkBox.Text.Contains("Other"))
                {
                    theTxtBox = new System.Web.UI.WebControls.TextBox();
                    string[] theId = thePannel.ID.Split('-');
                    if (theId.Length == 1)
                    {
                        theTxtBox.ID = "OtherTXT-" + theId.GetValue(0).ToString() + "-" + theDT.Rows[i][0].ToString();
                    }
                    else
                    {
                        theTxtBox.ID = "OtherTXT-" + theId.GetValue(1).ToString() + "-" + theId.GetValue(2).ToString() + "-" + theId.GetValue(3) + "-" + theDT.Rows[i][0].ToString();
                    }
                    theTxtBox.CssClass = "form-control";
                    theTxtBox.Width = 350;

                    theChkBox.Attributes.Add("onclick", "toggle('" + thePannel.ID + "');GetCheckboxId('" + thePannel.ClientID + '-' + theDT.Rows[i][0].ToString() + '-' + i + "')");
                    FlgOther = true;
                }
                theChkBox.Width = 300;
                if (FlgOther == false)
                {
                    thePannel.Controls.AddAt(i, theChkBox);
                }
                else
                {
                    System.Web.UI.WebControls.Panel theOtPnl = new System.Web.UI.WebControls.Panel();
                    theOtPnl.ID = "other-" + i + "-" + thePannel.ID;
                    theOtPnl.Controls.Add(new LiteralControl("<span>"));
                    theOtPnl.Controls.Add(theChkBox);
                    theOtPnl.Controls.Add(new LiteralControl("<span id='" + thePannel.ID + "' style='display:none'>"));
                    theOtPnl.Controls.Add(theTxtBox);
                    theOtPnl.Controls.Add(new LiteralControl("</span>"));
                    theOtPnl.Controls.Add(new LiteralControl("</span>"));
                    thePannel.Controls.AddAt(i, theOtPnl);
                    FlgOther = false;
                }
            }
        }
        public void CreateBlueCheckedList(System.Web.UI.WebControls.Panel thePannel, DataTable theDT, string Attribute, String Event)
        {
            int i = 0;
            bool FlgOther = false;
            System.Web.UI.WebControls.TextBox theTxtBox = new System.Web.UI.WebControls.TextBox();
            for (i = 0; i < theDT.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox theChkBox = new System.Web.UI.WebControls.CheckBox();
                //theChkBox.ID = thePannel.ID + '-' + theDT.Rows[i][0].ToString() + '-' + theDT.Rows[i][1].ToString();
                theChkBox.ID = "Chk-" + theDT.Rows[i][0].ToString() + '-' + theDT.Rows[i][1].ToString();
                theChkBox.Text = theDT.Rows[i][1].ToString();
                //theChkBox.AutoPostBack = true;
                theChkBox.LabelAttributes.CssStyle.Add(HtmlTextWriterStyle.FontWeight, "normal");
                theChkBox.Style.Add("font-weight", "400");
                theChkBox.Style.Add("font-size", "14px");                
                //theChkBox.Style.Value = "font-weight:normal";
                //theChkBox.LabelAttributes.CssStyle.Remove(
                if (Attribute != "")
                {
                    string Attr = "";
                    string[] theAttr = Attribute.Split('%');
                    if (theAttr.Length > 0)
                        Attr = theAttr[0] + "%" + theChkBox.ClientID + theAttr[1];
                    else
                        Attr = theAttr[0];
                    theChkBox.Attributes.Add(Event, Attr);
                }
                if (theChkBox.Text.Contains("Other") == true)
                {
                    theTxtBox = new System.Web.UI.WebControls.TextBox();
                    //string[] theId = thePannel.ID.Split('-');
                    theTxtBox.ID = "OtherTXT" + "-" + theDT.Rows[i][0].ToString() + "-" + theDT.Rows[i][1].ToString();   ////+ theId.GetValue(1).ToString() + "-" + theId.GetValue(2).ToString() + "-" + theId.GetValue(3) + "-" + theDT.Rows[i][0].ToString();
                    theTxtBox.Width = Unit.Percentage(20);
                    theChkBox.Attributes.Add("onclick", "ChkHideUnhide('txt" + theDT.Rows[i][0].ToString() + "','ctl00_IQCareContentPlaceHolder_" + theChkBox.ClientID + "')");
                    //theChkBox.Attributes.Add("onload", "ChkHideUnhide('txt" + theDT.Rows[i][0].ToString() + "','ctl00_clinicalheaderfooter_" + theChkBox.ClientID + "')");
                    theTxtBox.CssClass = "form-control";
                    theTxtBox.Width = 400;
                    FlgOther = true;
                }
                theChkBox.Width = 400;               
                if (FlgOther == false)
                {
                    thePannel.Controls.AddAt(i, theChkBox);
                }
                else
                {
                    System.Web.UI.WebControls.Panel theOtPnl = new System.Web.UI.WebControls.Panel();
                    theOtPnl.ID = "Pnl" + theDT.Rows[i][0].ToString();
                    theOtPnl.Controls.Add(new LiteralControl("<span>"));
                    theChkBox.Width = 200;
                    theOtPnl.Controls.Add(theChkBox);
                    theOtPnl.Controls.Add(new LiteralControl("<span id='txt" + theDT.Rows[i][0].ToString() + "' style='display:none'>"));
                    theOtPnl.Controls.Add(theTxtBox);
                    theOtPnl.Controls.Add(new LiteralControl("</span>"));
                    theOtPnl.Controls.Add(new LiteralControl("</span>"));
                    thePannel.Controls.AddAt(i, theOtPnl);

                    FlgOther = false;
                }
            }
        }

        public void BindHTMLCheckedList(HtmlInputCheckBox theCheckBox, DataTable theDT)
        {

            int i = 0;
            for (i = 0; i < theDT.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox theChkBox = new System.Web.UI.WebControls.CheckBox();
                theCheckBox.ID = theDT.Rows[i][0].ToString();
                theCheckBox.Value = theDT.Rows[i][1].ToString();
                theCheckBox.DataBind();
                theCheckBox.Controls.AddAt(i, theCheckBox);
            }

        }

        public DataTable GetYears(int CurrentYear, string theTextField, string theValueField)
        {
            int i = 0;
            DataRow theDR;
            DataTable theDT = new DataTable();
            theDT.Columns.Add("id");
            theDT.Columns.Add("Name");

            for (i = CurrentYear + 1; i >= 1930; i--)
            {
                theDR = theDT.NewRow();
                if (i == CurrentYear + 1)
                {
                    theDR["" + theTextField + ""] = "Select";
                    theDR["" + theValueField + ""] = 0;
                    theDT.Rows.InsertAt(theDR, 0);
                }
                else
                {
                    theDR[0] = i;
                    theDR[1] = i;
                    theDT.Rows.Add(theDR);
                }
            }
            return theDT;
        }

        public DataTable GetMonths()
        {
            string[] theMonth = new string[13];
            theMonth.SetValue("January", 1);
            theMonth.SetValue("February", 2);
            theMonth.SetValue("March", 3);
            theMonth.SetValue("April", 4);
            theMonth.SetValue("May", 5);
            theMonth.SetValue("June", 6);
            theMonth.SetValue("July", 7);
            theMonth.SetValue("August", 8);
            theMonth.SetValue("September", 9);
            theMonth.SetValue("October", 10);
            theMonth.SetValue("November", 11);
            theMonth.SetValue("December", 12);

            DataTable theDT = new DataTable();
            theDT.Columns.Add("Id", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Name", System.Type.GetType("System.String"));

            for (int i = 0; i < 12; i++)
            {
                DataRow theDr = theDT.NewRow();
                theDr[0] = i + 1;
                theDr[1] = theMonth[i + 1];
                theDT.Rows.InsertAt(theDr, i);
            }
            return theDT;

        }

        public void Win_Numeric(KeyPressEventArgs e)
        {
            string strRestrictCharList = "0123456789\b";

            if (strRestrictCharList.IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }

        }

        public void Win_Integer(KeyPressEventArgs e)
        {
            string strRestrictCharList = "-0123456789\b";

            if (strRestrictCharList.IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }

        }


        public void Win_decimal(KeyPressEventArgs e)
        {
            string strRestrictCharList = "0123456789.\b";

            if (strRestrictCharList.IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }

        }
        public void Win_decimalNagetive(KeyPressEventArgs e)
        {
            string strRestrictCharList = "-0123456789.\b";

            if (strRestrictCharList.IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }

        }
        public void Win_String(KeyPressEventArgs e)
        {
            string strRestrictCharList = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-?\b";

            if (strRestrictCharList.IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }

        }



    }
}
