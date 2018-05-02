using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Interface.FormBuilder;
using Application.Presentation;

namespace IQCare.FormBuilder
{
    public partial class frmServiceBusinessRule : Form
    {
        public frmServiceBusinessRule()
        {
            InitializeComponent();
        }

        private void frmServiceBusinessRule_Load(object sender, EventArgs e)
        {
            BindBusinessRules();
        }
        public void BindBusinessRules()
        {
            if (GblIQCare.dtServiceBusinessValues.Rows.Count > 0)
            {
                foreach (DataRow r in GblIQCare.dtServiceBusinessValues.Rows)
                {
                    if (r["BusRuleId"].ToString() == "16" && r["SetType"].ToString() == "1")
                    {
                        chkset1currentage.Checked = true;
                        txtset1value.Text = r["Value"].ToString();
                        txtset1value1.Text = r["Value1"].ToString();
                    }
                    if (r["BusRuleId"].ToString() == "16" && r["SetType"].ToString() == "2")
                    {
                        chkset2currentage.Checked = true;
                        txtset2value.Text = r["Value"].ToString();
                        txtset2value1.Text = r["Value1"].ToString();
                    }
                    if (r["BusRuleId"].ToString() == "15" && r["SetType"].ToString() == "1")
                    {
                        chkset1female.Checked = true;
                    }
                    if (r["BusRuleId"].ToString() == "15" && r["SetType"].ToString() == "2")
                    {
                        chkset2female.Checked = true;
                    }
                    if (r["BusRuleId"].ToString() == "14" && r["SetType"].ToString() == "1")
                    {
                        chkset1male.Checked = true;
                    }
                    if (r["BusRuleId"].ToString() == "14" && r["SetType"].ToString() == "2")
                    {
                        chkset2male.Checked = true;
                    }

                }
            }
        }
        private bool FieldValidation()
        {
            if (chkset1currentage.Checked)
            {
                if ((txtset1value.Text == "")|| (txtset1value1.Text == ""))
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                    return false;
                }

            }
            if (chkset2currentage.Checked)
            {
                if ((txtset2value.Text == "") || (txtset2value1.Text == ""))
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                    return false;
                }

            }
            if (chkset1currentage.Checked)
            {
                if (Convert.ToDecimal(txtset1value.Text) >= Convert.ToDecimal(txtset1value1.Text))
                {
                    IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", this);
                    return false;
                }
            }
            if (chkset2currentage.Checked)
            {
                if (Convert.ToDecimal(txtset2value.Text) >= Convert.ToDecimal(txtset2value1.Text))
                {
                    IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", this);
                    return false;
                }
            }
            return true;
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                return;
            }

            DataTable btable = new DataTable();
            btable.Columns.Add("ServiceAreaName", typeof(string));
            btable.Columns.Add("BusRuleId", typeof(string));
            btable.Columns.Add("Value", typeof(string));
            btable.Columns.Add("Value1", typeof(string));
            btable.Columns.Add("SetType", typeof(string));

            if (chkset1currentage.Checked)
            {
                if((txtset1value.Text!="") &&(txtset1value1.Text!=""))
                {
                    DataRow theDR = btable.NewRow();
                    theDR["ServiceAreaName"] = GblIQCare.strserviceareaname;
                    theDR["BusRuleId"] = "16";
                    theDR["Value"] = txtset1value.Text;
                    theDR["Value1"] = txtset1value1.Text;
                    theDR["SetType"] = "1";
                    btable.Rows.Add(theDR);
                }
            }
            if (chkset2currentage.Checked)
            {
                if ((txtset2value.Text != "") && (txtset2value1.Text != ""))
                {
                    DataRow theDR = btable.NewRow();
                    theDR["ServiceAreaName"] = GblIQCare.strserviceareaname;
                    theDR["BusRuleId"] = "16";
                    theDR["Value"] = txtset2value.Text;
                    theDR["Value1"] = txtset2value1.Text;
                    theDR["SetType"] = "2";
                    btable.Rows.Add(theDR);
                }
            }
            if (chkset1female.Checked)
            {
                DataRow theDR = btable.NewRow();
                theDR["ServiceAreaName"] = GblIQCare.strserviceareaname;
                theDR["BusRuleId"] = "15";
                theDR["SetType"] = "1";
                btable.Rows.Add(theDR);
            }
            if (chkset2female.Checked)
            {
                DataRow theDR = btable.NewRow();
                theDR["ServiceAreaName"] = GblIQCare.strserviceareaname;
                theDR["BusRuleId"] = "15";
                theDR["SetType"] = "2";
                btable.Rows.Add(theDR);
            }
            if (chkset1male.Checked)
            {
                DataRow theDR = btable.NewRow();
                theDR["ServiceAreaName"] = GblIQCare.strserviceareaname;
                theDR["BusRuleId"] = "14";
                theDR["SetType"] = "1";
                btable.Rows.Add(theDR);
            }
            if (chkset2male.Checked)
            {
                DataRow theDR = btable.NewRow();
                theDR["ServiceAreaName"] = GblIQCare.strserviceareaname;
                theDR["BusRuleId"] = "14";
                theDR["SetType"] = "2";
                btable.Rows.Add(theDR);
            }
           
            GblIQCare.dtServiceBusinessValues = btable;
            
            this.Close();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtset1value_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtset1value.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                    return;
                }
                float iNumberEntered = float.Parse(txtset1value.Text);
                if (iNumberEntered < 0.0)
                {
                    e.Cancel = true;
                    IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", this);
                    return;
                }
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void txtset2value_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtset2value.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                    return;
                }
                float iNumberEntered = float.Parse(txtset2value.Text);
                if (iNumberEntered < 0.0)
                {
                    e.Cancel = true;
                    IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", this);
                    return;
                }
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void txtset1value1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtset1value1.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                    return;
                }
                float iNumberEntered = float.Parse(txtset1value1.Text);
                if (iNumberEntered < 1.0)
                {
                    e.Cancel = true;
                    IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", this);
                    return;
                }
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void txtset2value1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtset2value1.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", this);
                    return;
                }
                float iNumberEntered = float.Parse(txtset2value1.Text);
                if (iNumberEntered < 1.0)
                {
                    e.Cancel = true;
                    IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", this);
                    return;
                }
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
