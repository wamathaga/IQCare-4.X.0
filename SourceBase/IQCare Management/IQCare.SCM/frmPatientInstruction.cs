using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Presentation;

namespace IQCare.SCM
{
    public partial class frmPatientInstruction : Form
    {
        public frmPatientInstruction()
        {
            InitializeComponent();
        }
        //public string patientInstructions = "";

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //patientInstructions = "";
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GblIQCare.PatientInstructions = txtPatientInstructions.Text;
            this.Close();
        }

        private void frmPatientInstruction_Load(object sender, EventArgs e)
        {
            txtPatientInstructions.Text = GblIQCare.PatientInstructions;
        }
    }
}
