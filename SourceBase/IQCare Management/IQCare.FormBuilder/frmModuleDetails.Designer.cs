namespace IQCare.FormBuilder
{
    partial class frmModuleDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModuleDetails));
            this.pnlModuleDetails = new System.Windows.Forms.Panel();
            this.dgwFieldDetails = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlFieldDetails = new System.Windows.Forms.Panel();
            this.txtstartingnumber = new System.Windows.Forms.TextBox();
            this.lblstartingnumber = new System.Windows.Forms.Label();
            this.txtlabel = new System.Windows.Forms.TextBox();
            this.lblIdentifierLabel = new System.Windows.Forms.Label();
            this.lblFieldType = new System.Windows.Forms.Label();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.cmbFieldType = new System.Windows.Forms.ComboBox();
            this.btnSumit = new System.Windows.Forms.Button();
            this.pnlModuleName = new System.Windows.Forms.Panel();
            this.btnservicebusinessrules = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkHivCareTrmt = new System.Windows.Forms.CheckBox();
            this.lblModuleName = new System.Windows.Forms.Label();
            this.txtModuleName = new System.Windows.Forms.TextBox();
            this.pnlModuleDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFieldDetails)).BeginInit();
            this.pnlFieldDetails.SuspendLayout();
            this.pnlModuleName.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlModuleDetails
            // 
            this.pnlModuleDetails.Controls.Add(this.dgwFieldDetails);
            this.pnlModuleDetails.Controls.Add(this.btnClose);
            this.pnlModuleDetails.Controls.Add(this.btnSave);
            this.pnlModuleDetails.Controls.Add(this.pnlFieldDetails);
            this.pnlModuleDetails.Controls.Add(this.pnlModuleName);
            this.pnlModuleDetails.Location = new System.Drawing.Point(1, 1);
            this.pnlModuleDetails.Name = "pnlModuleDetails";
            this.pnlModuleDetails.Size = new System.Drawing.Size(855, 506);
            this.pnlModuleDetails.TabIndex = 0;
            this.pnlModuleDetails.Tag = "pnlPanel";
            // 
            // dgwFieldDetails
            // 
            this.dgwFieldDetails.AllowUserToResizeColumns = false;
            this.dgwFieldDetails.AllowUserToResizeRows = false;
            this.dgwFieldDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgwFieldDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwFieldDetails.Location = new System.Drawing.Point(13, 192);
            this.dgwFieldDetails.Name = "dgwFieldDetails";
            this.dgwFieldDetails.Size = new System.Drawing.Size(826, 266);
            this.dgwFieldDetails.TabIndex = 8;
            this.dgwFieldDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFieldDetails_CellDoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(442, 477);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(141, 21);
            this.btnClose.TabIndex = 7;
            this.btnClose.Tag = "btnH25WFlexi";
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(243, 477);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 21);
            this.btnSave.TabIndex = 6;
            this.btnSave.Tag = "btnH25WFlexi";
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlFieldDetails
            // 
            this.pnlFieldDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFieldDetails.Controls.Add(this.txtstartingnumber);
            this.pnlFieldDetails.Controls.Add(this.lblstartingnumber);
            this.pnlFieldDetails.Controls.Add(this.txtlabel);
            this.pnlFieldDetails.Controls.Add(this.lblIdentifierLabel);
            this.pnlFieldDetails.Controls.Add(this.lblFieldType);
            this.pnlFieldDetails.Controls.Add(this.txtFieldName);
            this.pnlFieldDetails.Controls.Add(this.lblFieldName);
            this.pnlFieldDetails.Controls.Add(this.cmbFieldType);
            this.pnlFieldDetails.Controls.Add(this.btnSumit);
            this.pnlFieldDetails.Location = new System.Drawing.Point(12, 68);
            this.pnlFieldDetails.Name = "pnlFieldDetails";
            this.pnlFieldDetails.Size = new System.Drawing.Size(826, 121);
            this.pnlFieldDetails.TabIndex = 5;
            this.pnlFieldDetails.Tag = "";
            // 
            // txtstartingnumber
            // 
            this.txtstartingnumber.Location = new System.Drawing.Point(616, 62);
            this.txtstartingnumber.Name = "txtstartingnumber";
            this.txtstartingnumber.Size = new System.Drawing.Size(100, 20);
            this.txtstartingnumber.TabIndex = 15;
            this.txtstartingnumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtstartingnumber_KeyPress);
            // 
            // lblstartingnumber
            // 
            this.lblstartingnumber.AutoSize = true;
            this.lblstartingnumber.Location = new System.Drawing.Point(510, 65);
            this.lblstartingnumber.Name = "lblstartingnumber";
            this.lblstartingnumber.Size = new System.Drawing.Size(83, 13);
            this.lblstartingnumber.TabIndex = 14;
            this.lblstartingnumber.Tag = "lblLabel";
            this.lblstartingnumber.Text = "Starting Number";
            // 
            // txtlabel
            // 
            this.txtlabel.Location = new System.Drawing.Point(144, 36);
            this.txtlabel.Name = "txtlabel";
            this.txtlabel.Size = new System.Drawing.Size(340, 20);
            this.txtlabel.TabIndex = 13;
            // 
            // lblIdentifierLabel
            // 
            this.lblIdentifierLabel.AutoSize = true;
            this.lblIdentifierLabel.Location = new System.Drawing.Point(12, 39);
            this.lblIdentifierLabel.Name = "lblIdentifierLabel";
            this.lblIdentifierLabel.Size = new System.Drawing.Size(76, 13);
            this.lblIdentifierLabel.TabIndex = 12;
            this.lblIdentifierLabel.Tag = "lblLabel";
            this.lblIdentifierLabel.Text = "Identifier Label";
            // 
            // lblFieldType
            // 
            this.lblFieldType.AutoSize = true;
            this.lblFieldType.Location = new System.Drawing.Point(12, 65);
            this.lblFieldType.Name = "lblFieldType";
            this.lblFieldType.Size = new System.Drawing.Size(74, 13);
            this.lblFieldType.TabIndex = 8;
            this.lblFieldType.Tag = "lblLabel";
            this.lblFieldType.Text = "Identifier Type";
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(144, 10);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(340, 20);
            this.txtFieldName.TabIndex = 8;
            this.txtFieldName.Tag = "txtTextBox";
            this.txtFieldName.Leave += new System.EventHandler(this.txtFieldName_Leave);
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(12, 10);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(78, 13);
            this.lblFieldName.TabIndex = 9;
            this.lblFieldName.Tag = "lblLabel";
            this.lblFieldName.Text = "Identifier Name";
            // 
            // cmbFieldType
            // 
            this.cmbFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFieldType.FormattingEnabled = true;
            this.cmbFieldType.Location = new System.Drawing.Point(144, 62);
            this.cmbFieldType.Name = "cmbFieldType";
            this.cmbFieldType.Size = new System.Drawing.Size(340, 21);
            this.cmbFieldType.TabIndex = 10;
            this.cmbFieldType.Tag = "ddlDropDownList";
            this.cmbFieldType.SelectedIndexChanged += new System.EventHandler(this.cmbFieldType_SelectedIndexChanged);
            // 
            // btnSumit
            // 
            this.btnSumit.Location = new System.Drawing.Point(230, 89);
            this.btnSumit.Name = "btnSumit";
            this.btnSumit.Size = new System.Drawing.Size(141, 21);
            this.btnSumit.TabIndex = 11;
            this.btnSumit.Tag = "btnH25WFlexi";
            this.btnSumit.Text = "Submit";
            this.btnSumit.UseVisualStyleBackColor = true;
            this.btnSumit.Click += new System.EventHandler(this.btnSumit_Click);
            // 
            // pnlModuleName
            // 
            this.pnlModuleName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlModuleName.Controls.Add(this.btnservicebusinessrules);
            this.pnlModuleName.Controls.Add(this.label1);
            this.pnlModuleName.Controls.Add(this.chkHivCareTrmt);
            this.pnlModuleName.Controls.Add(this.lblModuleName);
            this.pnlModuleName.Controls.Add(this.txtModuleName);
            this.pnlModuleName.Location = new System.Drawing.Point(12, 13);
            this.pnlModuleName.Name = "pnlModuleName";
            this.pnlModuleName.Size = new System.Drawing.Size(827, 52);
            this.pnlModuleName.TabIndex = 4;
            this.pnlModuleName.Tag = "";
            // 
            // btnservicebusinessrules
            // 
            this.btnservicebusinessrules.Location = new System.Drawing.Point(601, 13);
            this.btnservicebusinessrules.Name = "btnservicebusinessrules";
            this.btnservicebusinessrules.Size = new System.Drawing.Size(177, 23);
            this.btnservicebusinessrules.TabIndex = 4;
            this.btnservicebusinessrules.Tag = "btnH25WFlexi";
            this.btnservicebusinessrules.Text = "Service Area Business Rules";
            this.btnservicebusinessrules.UseVisualStyleBackColor = true;
            this.btnservicebusinessrules.Click += new System.EventHandler(this.btnservicebusinessrules_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(578, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Display additional Pharmacy fields";
            this.label1.Visible = false;
            // 
            // chkHivCareTrmt
            // 
            this.chkHivCareTrmt.AutoSize = true;
            this.chkHivCareTrmt.Location = new System.Drawing.Point(582, 21);
            this.chkHivCareTrmt.Name = "chkHivCareTrmt";
            this.chkHivCareTrmt.Size = new System.Drawing.Size(141, 17);
            this.chkHivCareTrmt.TabIndex = 2;
            this.chkHivCareTrmt.Text = "HIV Care and Treatment";
            this.chkHivCareTrmt.UseVisualStyleBackColor = true;
            this.chkHivCareTrmt.Visible = false;
            // 
            // lblModuleName
            // 
            this.lblModuleName.AutoSize = true;
            this.lblModuleName.Location = new System.Drawing.Point(12, 16);
            this.lblModuleName.Name = "lblModuleName";
            this.lblModuleName.Size = new System.Drawing.Size(74, 13);
            this.lblModuleName.TabIndex = 0;
            this.lblModuleName.Tag = "lblLabel";
            this.lblModuleName.Text = "Service Name";
            // 
            // txtModuleName
            // 
            this.txtModuleName.AcceptsReturn = true;
            this.txtModuleName.Location = new System.Drawing.Point(144, 16);
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.Size = new System.Drawing.Size(340, 20);
            this.txtModuleName.TabIndex = 1;
            this.txtModuleName.Tag = "txtTextBox";
            this.txtModuleName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtModuleName_KeyPress);
            // 
            // frmModuleDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 511);
            this.Controls.Add(this.pnlModuleDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModuleDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Service ";
            this.Load += new System.EventHandler(this.frmModuleDetails_Load);
            this.pnlModuleDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwFieldDetails)).EndInit();
            this.pnlFieldDetails.ResumeLayout(false);
            this.pnlFieldDetails.PerformLayout();
            this.pnlModuleName.ResumeLayout(false);
            this.pnlModuleName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlModuleDetails;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlFieldDetails;
        private System.Windows.Forms.Panel pnlModuleName;
        private System.Windows.Forms.TextBox txtModuleName;
        private System.Windows.Forms.Label lblModuleName;
        private System.Windows.Forms.Button btnSumit;
        private System.Windows.Forms.ComboBox cmbFieldType;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.Label lblFieldType;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.DataGridView dgwFieldDetails;
        private System.Windows.Forms.TextBox txtlabel;
        private System.Windows.Forms.Label lblIdentifierLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkHivCareTrmt;
        private System.Windows.Forms.TextBox txtstartingnumber;
        private System.Windows.Forms.Label lblstartingnumber;
        private System.Windows.Forms.Button btnservicebusinessrules;
    }
}