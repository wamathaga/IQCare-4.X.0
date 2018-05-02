using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Interface.SCM;
//using Entities.Billing;
using Application.Presentation;


namespace IQCare.SCM
{
    public partial class frmItemCostConfiguration : Form
    {
        Boolean enableSelection = false;
        public frmItemCostConfiguration()
        {
            InitializeComponent();
        }

        private void frmItemCostConfiguration_Load(object sender, EventArgs e)
        {
            Init_Form();

        }
          
        private void Init_Form()
        {
            BindItemTypeDropdown();
            ddlItemType.SelectedIndex = -1;
            enableSelection = true;
          

        }
    
        private void BindItemTypeDropdown()
        {
            IQCareUtils theUtils = new IQCareUtils();
            ddlItemType.Items.Clear();
            IMasterList objProgramlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataTable theDT = objProgramlist.GetBillingGroups();
            
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "DeleteFlag = 0";
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(ddlItemType, theDV.ToTable(), "Name", "BillingTypeID");
        }

        private void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!enableSelection) return;
            bs_ItemCost.DataSource = null;
            txtSearch.Text = "";
       
            if (ddlItemType.SelectedIndex >-1)
            {
                 loadData((int)ddlItemType.SelectedValue);
                 
            }
            
           
           
        }

        private void loadData(int itemType)
        {
            dgvItemSubitemDetails.Columns.Clear();
            IBilling objProgramlist = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
            DataTable theDT = objProgramlist.GetPriceList(itemType);
            bs_ItemCost.DataSource = theDT;
            bnvItemCost.BindingSource = bs_ItemCost;
            dgvItemSubitemDetails.DataSource = bs_ItemCost;
            dgvItemSubitemDetails.Columns["ID"].Visible = false;
            dgvItemSubitemDetails.Columns["BillingTypeId"].Visible = false;
            dgvItemSubitemDetails.Columns["Name"].ReadOnly = true;
           // dgvItemSubitemDetails.Columns["Effective Date"].ReadOnly = true;
            
            dgvItemSubitemDetails.Columns["PharmacyPriceType"].Visible = false;

            

            //Check if it is drugs select. if not hide the price type column
            if (ddlItemType.Text.ToLower() == "drugs")
            {
                DataGridViewComboBoxColumn dgvc = new DataGridViewComboBoxColumn();
                dgvc.DataPropertyName = "PharmacyPriceType";
                DataTable data = new DataTable();
                data.Columns.Add(new DataColumn("Value", typeof(Int32)));
                data.Columns.Add(new DataColumn("Description", typeof(string)));

                data.Rows.Add("0", "Item");
                data.Rows.Add("1", "Dose");


                dgvc.DataSource = data;
                dgvc.ValueMember = "Value";
                dgvc.DisplayMember = "Description";
                dgvc.HeaderText = "Price Type";
                dgvItemSubitemDetails.Columns.Add(dgvc);
            }
            
         
            //dgvItemSubitemDetails.Columns["PharmacyPriceType"]

        //    DgvFilterManager filterManager = new DgvFilterManager(dgvItemSubitemDetails);

        }

        private void dgvItemSubitemDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dgvItemSubitemDetails.CurrentCell.Style.BackColor=Color.Yellow;
           // int row=dgvItemSubitemDetails.CurrentCell.RowIndex;

            DataRowView theRow= (DataRowView)dgvItemSubitemDetails.CurrentRow.DataBoundItem;
           // theRow["Effective Date"] = DateTime.Now;
            if (theRow["Item Selling Price"].ToString() == "")
                theRow["Item Selling Price"] = 0;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if (bs_ItemCost.DataSource == null)
            {
                closeform();
                return;
            }
            DataTable theDT=((DataTable)bs_ItemCost.DataSource).GetChanges();
            if (theDT != null)
            {
                if (MessageBox.Show("Close without saving?", "Confirm Close", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    closeform();
                }
            }
            else
                closeform();
        }
        private void closeform()
        {
            /*Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList,IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();*/
            this.Close();

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            loadData((int)ddlItemType.SelectedValue);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable theDT=((DataTable)bs_ItemCost.DataSource).GetChanges();
            if (theDT != null)
            {
                IBilling objProgramlist = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
                int i = objProgramlist.SavePriceList(theDT, GblIQCare.AppUserId);
                if (i < 1) return;
                loadData((int)ddlItemType.SelectedValue);
                MessageBox.Show("Saved Successfully.", "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
           
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            bs_ItemCost.Filter = String.Format("Name like '%{0}%'", txtSearch.Text);
        }
        DateTimePicker oDateTimePicker;
        private void dgvItemSubitemDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                //Initialized a new DateTimePicker Control  
                oDateTimePicker = new DateTimePicker();
                oDateTimePicker.MinDate = DateTime.Today;
                try
                {

                    oDateTimePicker.Text = dgvItemSubitemDetails.CurrentCell.Value.ToString();
                }
                catch (Exception ex)
                {

                }
                //Adding DateTimePicker control into DataGridView   
                dgvItemSubitemDetails.Controls.Add(oDateTimePicker);

                // Setting the format (i.e. 2014-10-10)  
                oDateTimePicker.Format = DateTimePickerFormat.Short;

                // It returns the retangular area that represents the Display area for a cell  
                Rectangle oRectangle = dgvItemSubitemDetails.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                //Setting area for DateTimePicker Control  
                oDateTimePicker.Size = new Size(oRectangle.Width, oRectangle.Height);

                // Setting Location  
                oDateTimePicker.Location = new Point(oRectangle.X, oRectangle.Y);

                // An event attached to dateTimePicker Control which is fired when DateTimeControl is closed  
                oDateTimePicker.CloseUp += oDateTimePicker_CloseUp;

                // An event attached to dateTimePicker Control which is fired when any date is selected  
                oDateTimePicker.TextChanged += dateTimePicker_OnTextChange;

                // Now make it visible  
                oDateTimePicker.Visible = true; 
            }

        }


        private void dateTimePicker_OnTextChange(object sender, EventArgs e)
        {
            // Saving the 'Selected Date on Calendar' into DataGridView current cell  
            dgvItemSubitemDetails.CurrentCell.Value = oDateTimePicker.Text;
        }


        private void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            // Hiding the control after use   
            oDateTimePicker.Visible = false;
            dgvItemSubitemDetails.Controls.Remove(oDateTimePicker);
        }

        private void dgvItemSubitemDetails_Scroll(object sender, ScrollEventArgs e)
        {
            dgvItemSubitemDetails.Controls.Remove(oDateTimePicker);
        }

        private void dgvItemSubitemDetails_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvItemSubitemDetails.Controls.Remove(oDateTimePicker);
        }

        private void dgvItemSubitemDetails_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItemSubitemDetails.CurrentRow.Cells[3].Value == null) return;
            if (dgvItemSubitemDetails.CurrentRow.Cells[3].Value.ToString() != "")
                if (dgvItemSubitemDetails.CurrentRow.Cells[4].Value.ToString() == "")
                {
                    dgvItemSubitemDetails.CurrentRow.Cells[4].Value = DateTime.Today;
                    dgvItemSubitemDetails.CurrentRow.Cells[4].Style.BackColor = Color.Yellow;
                }
        }

        private void dgvItemSubitemDetails_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dgvItemSubitemDetails.Controls.Remove(oDateTimePicker);
        }  
    }

}
