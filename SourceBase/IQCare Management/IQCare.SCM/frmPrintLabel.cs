using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using Application.Presentation;
using Application.Common;

namespace IQCare.SCM
{
    /// /////////////////////////////////////////////////////////////////////
    // Code Written By   : Jayant Kumar Das
    // Started  Date     : 23 Aug 2013
    // Description       : Label printing of dispensing window
    //
    /// /////////////////////////////////////////////////////////////////
    public partial class frmPrintLabel : Form
    {
        public frmPrintLabel()
        {
            InitializeComponent();
        }
        //Label finally gets printed through this 
        public void PassUserControl(Panel print, short pages)
        {
            try
            {
                foreach (Control ctl in print.Controls)
                {

                    if (ctl is CheckBox && ((CheckBox)(ctl)).Checked == true)
                    {
                        System.Drawing.Printing.PrintDocument doc = new System.Drawing.Printing.PrintDocument();
                        doc.PrintPage += new PrintPageEventHandler((sender, e) => pd_PrintPage(sender, e, print));
                        PrintDialog PrintSettings = new PrintDialog();
                        PrintSettings.Document = doc;
                        //PageSettings pgsetting = new PageSettings();
                        //PrintPreviewDialog dlgPreview = new PrintPreviewDialog();
                        //dlgPreview.Document = doc;
                        //dlgPreview.Show();
                        PrintSettings.PrinterSettings.Copies = pages;
                        //if (PrintSettings.ShowDialog() == DialogResult.OK)
                        doc.Print();
                    }

                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }
        //Creating Image file of the Label to be printed
        void pd_PrintPage(object sender, PrintPageEventArgs e, Panel print)
        {
            try
            {
                Bitmap bmp = new Bitmap(print.Width, print.Height, print.CreateGraphics());
                print.DrawToBitmap(bmp, new Rectangle(0, 0, print.Width, print.Height));
                RectangleF bounds = e.PageSettings.PrintableArea;
                e.Graphics.DrawImage(bmp, bounds.Left, bounds.Top, print.Width, print.Height);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        //Each label loaded separately.
        private void frmPrintLabel_Load(object sender, EventArgs e)
        {
            try
            {
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
                int locY = 0;
                pnlusercontrol.AutoScroll = true;
                DataTable theDT = GblIQCare.dtPrintLabel;
                foreach (DataRow theDR in theDT.Rows)
                {
                    UserPrintControl myCtrl = new UserPrintControl();
                    myCtrl.Name = "wucpnl" + theDR["ItemId"].ToString();
                    myCtrl.Location = new Point(pnlusercontrol.Location.X, locY);
                    myCtrl.Facility.Text = GblIQCare.AppLocation + "  Tel:" + GblIQCare.AppLocTelNo;
                    myCtrl.Store.Text = GblIQCare.StoreName + "  " + GblIQCare.CurrentDate;
                    myCtrl.PatientName.Text = GblIQCare.PatientName.ToUpper() ;
                    myCtrl.chkDName.Text = theDR["ItemName"].ToString();
                    myCtrl.lbldrugquantity.Text = theDR["qtydisp"].ToString() + " " + theDR["DispensingUnitName"].ToString() + "   Exp:" + theDR["ExpiryDate"].ToString();
                    myCtrl.txtInstruction.Text = theDR["PatientInstructions"].ToString();
                    pnlusercontrol.Controls.Add(myCtrl);
                    locY = locY + myCtrl.Height + 25;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        //closing the form
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //For printing labels commands are sent from here
        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control ctl in pnlusercontrol.Controls)
                {
                    if (ctl is UserControl)
                    {
                        short pages = 1;
                        foreach (Control ctl1 in ctl.Controls)
                        {
                            if (ctl1 is ComboBox)
                            {
                                pages = Convert.ToInt16((((System.Windows.Forms.ComboBox)(ctl1))).SelectedItem);
                            } 
                            if (ctl1 is Panel)
                            {
                                PassUserControl((Panel)ctl1, pages);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ToString());
            }
        }
    }
}
