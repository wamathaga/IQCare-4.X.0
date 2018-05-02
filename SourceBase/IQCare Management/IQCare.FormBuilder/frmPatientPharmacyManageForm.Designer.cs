namespace IQCare.FormBuilder
{
    partial class frmPatientPharmacyManageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientPharmacyManageForm));
            this.cmbFormStatus = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblFormStatus = new System.Windows.Forms.Label();
            this.dgwFormDetails = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFormDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbFormStatus
            // 
            this.cmbFormStatus.BackColor = System.Drawing.Color.White;
            this.cmbFormStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormStatus.Items.AddRange(new object[] {
            "In Process",
            "Published",
            "Unpublished",
            "All"});
            this.cmbFormStatus.Location = new System.Drawing.Point(190, 20);
            this.cmbFormStatus.Name = "cmbFormStatus";
            this.cmbFormStatus.Size = new System.Drawing.Size(181, 21);
            this.cmbFormStatus.TabIndex = 19;
            this.cmbFormStatus.Tag = "ddlDropDownList";
            this.cmbFormStatus.SelectedIndexChanged += new System.EventHandler(this.cmbFormStatus_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(377, 462);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Tag = "btnH25WFlexi";
            this.btnCancel.Text = "&Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(489, 433);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(106, 23);
            this.btnPreview.TabIndex = 17;
            this.btnPreview.Tag = "btnH25WFlexi";
            this.btnPreview.Text = "Pre&view Form";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(377, 433);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(106, 23);
            this.btnEdit.TabIndex = 16;
            this.btnEdit.Tag = "btnH25WFlexi";
            this.btnEdit.Text = "&Edit Form";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(265, 433);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(106, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Tag = "btnH25WFlexi";
            this.btnAdd.Text = "C&reate New Form";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblFormStatus
            // 
            this.lblFormStatus.AutoSize = true;
            this.lblFormStatus.Location = new System.Drawing.Point(105, 23);
            this.lblFormStatus.Name = "lblFormStatus";
            this.lblFormStatus.Size = new System.Drawing.Size(63, 13);
            this.lblFormStatus.TabIndex = 14;
            this.lblFormStatus.Tag = "lblLabel";
            this.lblFormStatus.Text = "Form Status";
            // 
            // dgwFormDetails
            // 
            this.dgwFormDetails.AllowUserToDeleteRows = false;
            this.dgwFormDetails.AllowUserToResizeColumns = false;
            this.dgwFormDetails.AllowUserToResizeRows = false;
            this.dgwFormDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgwFormDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwFormDetails.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgwFormDetails.Location = new System.Drawing.Point(15, 47);
            this.dgwFormDetails.Name = "dgwFormDetails";
            this.dgwFormDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgwFormDetails.Size = new System.Drawing.Size(823, 368);
            this.dgwFormDetails.TabIndex = 13;
            this.dgwFormDetails.Tag = "";
            this.dgwFormDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFormDetails_CellClick);
            this.dgwFormDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFormDetails_CellDoubleClick);
            this.dgwFormDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwFormDetails_CellFormatting);
            // 
            // frmPatientPharmacyManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.cmbFormStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblFormStatus);
            this.Controls.Add(this.dgwFormDetails);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPatientPharmacyManageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Pharmacy Form";
            this.Load += new System.EventHandler(this.frmPatientPharmacyManageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwFormDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbFormStatus;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblFormStatus;
        private System.Windows.Forms.DataGridView dgwFormDetails;
    }
}