using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;

namespace Touch
{
    public class TouchUserControlBase : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (Control cntrl in this.Controls)
            {

                string ControlName = cntrl.GetType().Name;
                if (ControlName == "RadTextBox")
                {
                    RadTextBox TheCont = (RadTextBox)cntrl;
                    if (!TheCont.ID.Contains("ZZZ"))
                    {
                        if (int.Parse(TheCont.Width.Value.ToString()) < 200)
                            TheCont.Width = 200;
                    }
                }
                else if (ControlName == "RadComboBox")
                {
                    RadComboBox TheCont = (RadComboBox)cntrl;
                    if (int.Parse(TheCont.Width.Value.ToString()) < 200)
                        TheCont.Width = 200;
                }
                else if (ControlName == "RadDatePicker")
                {
                    RadDatePicker TheCont = (RadDatePicker)cntrl;
                    TheCont.Calendar.ShowRowHeaders = false;
                    if (int.Parse(TheCont.Width.Value.ToString()) < 200)
                        TheCont.Width = 200;
                }
                else if (ControlName == "UpdatePanel")
                {
                    UpdatePanel TheCont = (UpdatePanel)cntrl;
                    CheckControlCollection(TheCont);
                }
                else if (ControlName == "RadSplitter")
                {
                    RadSplitter TheCont = (RadSplitter)cntrl;
                    foreach (Control radpane in TheCont.Controls)
                    {
                        if (radpane.GetType().Name == "RadPane")
                        {
                            RadPane thePane = (RadPane)radpane;
                            foreach (Control paneItiem in thePane.Controls)
                            {
                                if (paneItiem.GetType().Name == "UpdatePanel")
                                {
                                    UpdatePanel ThePanel = (UpdatePanel)paneItiem;
                                    CheckControlCollection(ThePanel);
                                }
                            }
                        }
                    }
                    //UpdatePanel TheCont = (UpdatePanel)cntrl;
                    //CheckControlCollection(TheCont);
                }
            }
            System.Threading.Thread.Sleep(1000);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "loadpharm", "$(function(){parent.CloseLoading(); });", true);

        }

        private void CheckControlCollection(UpdatePanel TheCont)
        {
            foreach (Control item in TheCont.ContentTemplateContainer.Controls)
            {
                string cntrlNme = item.GetType().Name;
                if (cntrlNme == "RadTextBox")
                {
                    RadTextBox thecont = (RadTextBox)item;
                    if (!thecont.ID.Contains("ZZZ"))
                    {
                        if (int.Parse(thecont.Width.Value.ToString()) < 200)
                            thecont.Width = 200;
                    }
                }
                else if (cntrlNme == "RadComboBox")
                {
                    RadComboBox thecont = (RadComboBox)item;
                    if (int.Parse(thecont.Width.Value.ToString()) < 200)
                        thecont.Width = 200;
                }
                else if (cntrlNme == "RadDatePicker")
                {
                    RadDatePicker thecont = (RadDatePicker)item;
                    thecont.Calendar.ShowRowHeaders = false;
                    if (int.Parse(thecont.Width.Value.ToString()) < 200)
                        thecont.Width = 200;
                }
                else if (cntrlNme == "RadButton")
                {
                    RadButton thecont = (RadButton)item;
                    if (thecont.ToggleType == ButtonToggleType.Radio)
                    {
                        thecont.Attributes.Add("onmouseup", "up(this);");
                        thecont.Attributes.Add("onfocus", "up(this);");
                        thecont.OnClientClicked = "down";
                    }
                    
                }
                else if (cntrlNme == "UpdatePanel")
                {
                    UpdatePanel thecont = (UpdatePanel)item;
                    CheckControlCollection(thecont);
                }
            }
            if (TheCont.UpdateMode == UpdatePanelUpdateMode.Conditional) TheCont.Update();
        }
    }
}