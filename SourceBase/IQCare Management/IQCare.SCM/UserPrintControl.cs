using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
namespace IQCare.SCM
{
    /// /////////////////////////////////////////////////////////////////////
    // Code Written By   : Jayant Kumar Das
    // Written started Date      : 23 December 2013
    // Description       : User control for Label printing 
    //
    /// /////////////////////////////////////////////////////////////////
    public partial class UserPrintControl : UserControl
    {
        public UserPrintControl()
        {
            InitializeComponent();
        }
        //Properties to access the controls in forms
        public Panel print
        {
            get
            {
                return pnlprint;
            }

            set { pnlprint = value; }

        }
        
        public Label Facility
        {
            get
            {
                return lblfacility;
            }
            set
            {
                lblfacility = value;
            }

        }

        public Label Store
        {
            get
            {
                return lblStore;
            }
            set
            {
                lblStore = value;
            }

        }

        public Label PatientName
        {
            get
            {
                return lblPatientName;
            }
            set
            {
                lblPatientName = value;
            }
        }

        public CheckBox chkDName
        {
            get
            {
                return chkDrugName;
            }
            set
            {
                chkDrugName = value;
            }
        }

        public Label lbldrugquantity
        {
            get
            {
                return lbldrgquantity;
            }
            set
            {
                lbldrgquantity = value;
            }
        }

        public Label lblExpiry
        {
            get
            {

                return lblexpire1;
            }

            set
            {
                lblexpire1 = value;

            }

        }

        public TextBox txtInstruction
        {
            get
            {
                return txtinstruction;
            }
            set
            {
                txtinstruction = value;
            }

        }
        private void UserPrintControl_Load(object sender, EventArgs e)
        {
            try
            {
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
                cmbnocopies.SelectedIndex = 0;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
    }
}
