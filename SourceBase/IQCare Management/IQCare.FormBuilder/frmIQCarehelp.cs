using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Presentation;


namespace IQCare.FormBuilder
{
    public partial class frmIQCarehelp : Form
    {
        public frmIQCarehelp()
        {
            InitializeComponent();
        }

        private void frmIQCarehelp_Load(object sender, EventArgs e)
        {
            webBrwIQhelp.Navigate(GblIQCare.IQCareHelpweburl());
        }
    }
}
