namespace IQCare.SCM
{
    partial class frmExpiryReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExpiryReport));
            this.label5 = new System.Windows.Forms.Label();
            this.dtpReportDate = new System.Windows.Forms.DateTimePicker();
            this.lblstore = new System.Windows.Forms.Label();
            this.cmbStore = new System.Windows.Forms.ComboBox();
            this.dgwExperyReport = new System.Windows.Forms.DataGridView();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbdays = new System.Windows.Forms.ComboBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DispensingUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalPurchasePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgwExperyReport)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(409, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 17;
            this.label5.Tag = "lblLabel";
            this.label5.Text = "Report As Of Date:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // dtpReportDate
            // 
            this.dtpReportDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReportDate.Location = new System.Drawing.Point(512, 6);
            this.dtpReportDate.Name = "dtpReportDate";
            this.dtpReportDate.Size = new System.Drawing.Size(129, 20);
            this.dtpReportDate.TabIndex = 3;
            this.dtpReportDate.Tag = "txtTextBoxSCM";
            // 
            // lblstore
            // 
            this.lblstore.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblstore.AutoSize = true;
            this.lblstore.Location = new System.Drawing.Point(7, 10);
            this.lblstore.Name = "lblstore";
            this.lblstore.Size = new System.Drawing.Size(66, 13);
            this.lblstore.TabIndex = 1;
            this.lblstore.Tag = "lblLabel";
            this.lblstore.Text = "Store Name:";
            // 
            // cmbStore
            // 
            this.cmbStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStore.FormattingEnabled = true;
            this.cmbStore.Location = new System.Drawing.Point(76, 5);
            this.cmbStore.Name = "cmbStore";
            this.cmbStore.Size = new System.Drawing.Size(318, 21);
            this.cmbStore.TabIndex = 2;
            this.cmbStore.Tag = "ddlDropDownList";
            // 
            // dgwExperyReport
            // 
            this.dgwExperyReport.AllowUserToAddRows = false;
            this.dgwExperyReport.AllowUserToDeleteRows = false;
            this.dgwExperyReport.AllowUserToResizeColumns = false;
            this.dgwExperyReport.AllowUserToResizeRows = false;
            this.dgwExperyReport.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwExperyReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwExperyReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemCode,
            this.itemDesc,
            this.BatchNo,
            this.Quantity,
            this.DispensingUnit,
            this.ExpiryDate,
            this.TotalPurchasePrice});
            this.dgwExperyReport.Location = new System.Drawing.Point(0, 77);
            this.dgwExperyReport.Name = "dgwExperyReport";
            this.dgwExperyReport.Size = new System.Drawing.Size(850, 379);
            this.dgwExperyReport.TabIndex = 10;
            this.dgwExperyReport.Tag = "dgwDataGridView";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(634, 45);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(80, 25);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Tag = "btnW100WFlexiSCM";
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button4);
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 20;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Window;
            this.button4.Location = new System.Drawing.Point(771, 12);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 25);
            this.button4.TabIndex = 21;
            this.button4.Tag = "btnSingleText";
            this.button4.Text = "&Close";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(659, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 61;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Expiry Days:";
            // 
            // cmbdays
            // 
            this.cmbdays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbdays.FormattingEnabled = true;
            this.cmbdays.Location = new System.Drawing.Point(727, 5);
            this.cmbdays.Name = "cmbdays";
            this.cmbdays.Size = new System.Drawing.Size(96, 21);
            this.cmbdays.TabIndex = 4;
            this.cmbdays.Tag = "ddlDropDownList";
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.SystemColors.Window;
            this.btnExport.Location = new System.Drawing.Point(720, 45);
            this.btnExport.Margin = new System.Windows.Forms.Padding(0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(89, 25);
            this.btnExport.TabIndex = 6;
            this.btnExport.Tag = "btnW100WFlexiSCM";
            this.btnExport.Text = "&Export to Excel";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Item Code";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 175;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Item Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Batch No.";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 130;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Quantity";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 83;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Dispensing Unit";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Expiry Date";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 125;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Total Purchase Price";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 110;
            // 
            // ItemCode
            // 
            this.ItemCode.HeaderText = "Item Code";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.Width = 120;
            // 
            // itemDesc
            // 
            this.itemDesc.HeaderText = "Item Name";
            this.itemDesc.Name = "itemDesc";
            this.itemDesc.Width = 200;
            // 
            // BatchNo
            // 
            this.BatchNo.HeaderText = "Batch No.";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.Width = 130;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.Width = 120;
            // 
            // DispensingUnit
            // 
            this.DispensingUnit.HeaderText = "Dispensing Unit";
            this.DispensingUnit.Name = "DispensingUnit";
            this.DispensingUnit.Width = 120;
            // 
            // ExpiryDate
            // 
            this.ExpiryDate.HeaderText = "Expiry Date";
            this.ExpiryDate.Name = "ExpiryDate";
            this.ExpiryDate.Width = 120;
            // 
            // TotalPurchasePrice
            // 
            this.TotalPurchasePrice.HeaderText = "Total Purchase Price";
            this.TotalPurchasePrice.Name = "TotalPurchasePrice";
            this.TotalPurchasePrice.Width = 133;
            // 
            // frmExpiryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.cmbdays);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dgwExperyReport);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpReportDate);
            this.Controls.Add(this.cmbStore);
            this.Controls.Add(this.lblstore);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExpiryReport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Expiry Report";
            this.Load += new System.EventHandler(this.frmExpiryReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwExperyReport)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpReportDate;
        private System.Windows.Forms.Label lblstore;
        private System.Windows.Forms.ComboBox cmbStore;
        private System.Windows.Forms.DataGridView dgwExperyReport;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbdays;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn DispensingUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpiryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPurchasePrice;
        private System.Windows.Forms.Button btnExport;
    }
}