namespace IQCare.IPT
{
    partial class frmIPTRapidTool
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbIdentifier = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbService = new System.Windows.Forms.ComboBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvPatients = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtOtherReasonDiscontinued = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtOtherReasonContraindication = new System.Windows.Forms.TextBox();
            this.lblOtherReason = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbIPTDiscontinued = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbIPTContraindication = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbDeclinedIpt = new System.Windows.Forms.RadioButton();
            this.rbDiscontinued = new System.Windows.Forms.RadioButton();
            this.rbCompletedIPT = new System.Windows.Forms.RadioButton();
            this.dtIPTEndDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.dtIPTStartDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTBFinding = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTBAssessment = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkEligibleForIPT = new MyCheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbIdentifier
            // 
            this.cbIdentifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIdentifier.FormattingEnabled = true;
            this.cbIdentifier.Location = new System.Drawing.Point(109, 53);
            this.cbIdentifier.Name = "cbIdentifier";
            this.cbIdentifier.Size = new System.Drawing.Size(203, 21);
            this.cbIdentifier.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Identifier";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbService);
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbIdentifier);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(727, 121);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // cbService
            // 
            this.cbService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbService.FormattingEnabled = true;
            this.cbService.Location = new System.Drawing.Point(109, 17);
            this.cbService.Name = "cbService";
            this.cbService.Size = new System.Drawing.Size(203, 21);
            this.cbService.TabIndex = 10;
            this.cbService.SelectedIndexChanged += new System.EventHandler(this.cbService_SelectedIndexChanged);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(264, 92);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(142, 23);
            this.btnFind.TabIndex = 9;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(511, 52);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(192, 20);
            this.textBox1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Service";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(357, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Identification Number";
            // 
            // dgvPatients
            // 
            this.dgvPatients.AllowUserToAddRows = false;
            this.dgvPatients.AllowUserToDeleteRows = false;
            this.dgvPatients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPatients.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPatients.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPatients.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPatients.Location = new System.Drawing.Point(26, 154);
            this.dgvPatients.MultiSelect = false;
            this.dgvPatients.Name = "dgvPatients";
            this.dgvPatients.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPatients.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPatients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatients.Size = new System.Drawing.Size(700, 150);
            this.dgvPatients.TabIndex = 5;
            this.dgvPatients.SelectionChanged += new System.EventHandler(this.dgvPatients_SelectionChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtOtherReasonDiscontinued);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtOtherReasonContraindication);
            this.groupBox2.Controls.Add(this.lblOtherReason);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.cbIPTDiscontinued);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cbIPTContraindication);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.dtIPTEndDate);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.dtIPTStartDate);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.chkEligibleForIPT);
            this.groupBox2.Controls.Add(this.txtTBFinding);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtTBAssessment);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(26, 325);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(700, 337);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IPT Details";
            // 
            // txtOtherReasonDiscontinued
            // 
            this.txtOtherReasonDiscontinued.Location = new System.Drawing.Point(462, 252);
            this.txtOtherReasonDiscontinued.Name = "txtOtherReasonDiscontinued";
            this.txtOtherReasonDiscontinued.ReadOnly = true;
            this.txtOtherReasonDiscontinued.Size = new System.Drawing.Size(177, 20);
            this.txtOtherReasonDiscontinued.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(344, 252);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 15);
            this.label10.TabIndex = 28;
            this.label10.Text = "Other Reason";
            // 
            // txtOtherReasonContraindication
            // 
            this.txtOtherReasonContraindication.Location = new System.Drawing.Point(135, 253);
            this.txtOtherReasonContraindication.Name = "txtOtherReasonContraindication";
            this.txtOtherReasonContraindication.ReadOnly = true;
            this.txtOtherReasonContraindication.Size = new System.Drawing.Size(177, 20);
            this.txtOtherReasonContraindication.TabIndex = 27;
            // 
            // lblOtherReason
            // 
            this.lblOtherReason.AutoSize = true;
            this.lblOtherReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherReason.Location = new System.Drawing.Point(17, 253);
            this.lblOtherReason.Name = "lblOtherReason";
            this.lblOtherReason.Size = new System.Drawing.Size(83, 15);
            this.lblOtherReason.TabIndex = 26;
            this.lblOtherReason.Text = "Other Reason";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(187, 297);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(142, 23);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(358, 297);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(142, 23);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbIPTDiscontinued
            // 
            this.cbIPTDiscontinued.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIPTDiscontinued.FormattingEnabled = true;
            this.cbIPTDiscontinued.Location = new System.Drawing.Point(453, 213);
            this.cbIPTDiscontinued.Name = "cbIPTDiscontinued";
            this.cbIPTDiscontinued.Size = new System.Drawing.Size(174, 21);
            this.cbIPTDiscontinued.TabIndex = 23;
            this.cbIPTDiscontinued.SelectedIndexChanged += new System.EventHandler(this.cbIPTDiscontinued_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(347, 213);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 15);
            this.label11.TabIndex = 22;
            this.label11.Text = "IPT Discontinued";
            // 
            // cbIPTContraindication
            // 
            this.cbIPTContraindication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIPTContraindication.FormattingEnabled = true;
            this.cbIPTContraindication.Location = new System.Drawing.Point(135, 212);
            this.cbIPTContraindication.Name = "cbIPTContraindication";
            this.cbIPTContraindication.Size = new System.Drawing.Size(174, 21);
            this.cbIPTContraindication.TabIndex = 19;
            this.cbIPTContraindication.SelectedIndexChanged += new System.EventHandler(this.cbIPTContraindication_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 214);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 15);
            this.label9.TabIndex = 18;
            this.label9.Text = "IPT Contraindication";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbDeclinedIpt);
            this.groupBox3.Controls.Add(this.rbDiscontinued);
            this.groupBox3.Controls.Add(this.rbCompletedIPT);
            this.groupBox3.Location = new System.Drawing.Point(15, 125);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(635, 42);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "IPT";
            // 
            // rbDeclinedIpt
            // 
            this.rbDeclinedIpt.AutoSize = true;
            this.rbDeclinedIpt.Location = new System.Drawing.Point(365, 16);
            this.rbDeclinedIpt.Name = "rbDeclinedIpt";
            this.rbDeclinedIpt.Size = new System.Drawing.Size(87, 17);
            this.rbDeclinedIpt.TabIndex = 2;
            this.rbDeclinedIpt.TabStop = true;
            this.rbDeclinedIpt.Text = "Declined IPT";
            this.rbDeclinedIpt.UseVisualStyleBackColor = true;
            this.rbDeclinedIpt.CheckedChanged += new System.EventHandler(this.rbDefaulted_CheckedChanged);
            // 
            // rbDiscontinued
            // 
            this.rbDiscontinued.AutoSize = true;
            this.rbDiscontinued.Location = new System.Drawing.Point(215, 16);
            this.rbDiscontinued.Name = "rbDiscontinued";
            this.rbDiscontinued.Size = new System.Drawing.Size(87, 17);
            this.rbDiscontinued.TabIndex = 1;
            this.rbDiscontinued.TabStop = true;
            this.rbDiscontinued.Text = "Discontinued";
            this.rbDiscontinued.UseVisualStyleBackColor = true;
            this.rbDiscontinued.CheckedChanged += new System.EventHandler(this.rbDiscontinued_CheckedChanged);
            // 
            // rbCompletedIPT
            // 
            this.rbCompletedIPT.AutoSize = true;
            this.rbCompletedIPT.Location = new System.Drawing.Point(41, 16);
            this.rbCompletedIPT.Name = "rbCompletedIPT";
            this.rbCompletedIPT.Size = new System.Drawing.Size(95, 17);
            this.rbCompletedIPT.TabIndex = 0;
            this.rbCompletedIPT.TabStop = true;
            this.rbCompletedIPT.Text = "Completed IPT";
            this.rbCompletedIPT.UseVisualStyleBackColor = true;
            this.rbCompletedIPT.CheckedChanged += new System.EventHandler(this.rbCompletedIPT_CheckedChanged);
            // 
            // dtIPTEndDate
            // 
            this.dtIPTEndDate.CustomFormat = "dd-MMM-yyyy";
            this.dtIPTEndDate.Location = new System.Drawing.Point(118, 173);
            this.dtIPTEndDate.Name = "dtIPTEndDate";
            this.dtIPTEndDate.Size = new System.Drawing.Size(314, 20);
            this.dtIPTEndDate.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "IPT End Date :";
            // 
            // dtIPTStartDate
            // 
            this.dtIPTStartDate.CustomFormat = "dd-MMM-yyyy";
            this.dtIPTStartDate.Location = new System.Drawing.Point(148, 96);
            this.dtIPTStartDate.Name = "dtIPTStartDate";
            this.dtIPTStartDate.Size = new System.Drawing.Size(284, 20);
            this.dtIPTStartDate.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "IPT Start Date :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(355, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "Eligible for IPT this Visit :";
            // 
            // txtTBFinding
            // 
            this.txtTBFinding.Location = new System.Drawing.Point(453, 23);
            this.txtTBFinding.Name = "txtTBFinding";
            this.txtTBFinding.ReadOnly = true;
            this.txtTBFinding.Size = new System.Drawing.Size(177, 20);
            this.txtTBFinding.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(354, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "TB Findings :";
            // 
            // txtTBAssessment
            // 
            this.txtTBAssessment.Location = new System.Drawing.Point(148, 24);
            this.txtTBAssessment.Multiline = true;
            this.txtTBAssessment.Name = "txtTBAssessment";
            this.txtTBAssessment.ReadOnly = true;
            this.txtTBAssessment.Size = new System.Drawing.Size(177, 63);
            this.txtTBAssessment.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "TB Assessment (ICF) : ";
            // 
            // chkEligibleForIPT
            // 
            this.chkEligibleForIPT.AutoSize = true;
            this.chkEligibleForIPT.Location = new System.Drawing.Point(507, 58);
            this.chkEligibleForIPT.Name = "chkEligibleForIPT";
            this.chkEligibleForIPT.Padding = new System.Windows.Forms.Padding(6);
            this.chkEligibleForIPT.Size = new System.Drawing.Size(106, 29);
            this.chkEligibleForIPT.TabIndex = 11;
            this.chkEligibleForIPT.Text = "myCheckBox1";
            this.chkEligibleForIPT.UseVisualStyleBackColor = true;
            // 
            // frmIPTRapidTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(783, 674);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgvPatients);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmIPTRapidTool";
            this.Text = "IPT Rapid Entry Tool";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbIdentifier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.DataGridView dgvPatients;
        private System.Windows.Forms.ComboBox cbService;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTBFinding;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTBAssessment;
        private System.Windows.Forms.Label label4;
        private MyCheckBox chkEligibleForIPT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtIPTStartDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtIPTEndDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbIPTDiscontinued;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbIPTContraindication;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbDeclinedIpt;
        private System.Windows.Forms.RadioButton rbDiscontinued;
        private System.Windows.Forms.RadioButton rbCompletedIPT;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtOtherReasonContraindication;
        private System.Windows.Forms.Label lblOtherReason;
        private System.Windows.Forms.TextBox txtOtherReasonDiscontinued;
        private System.Windows.Forms.Label label10;
    }
}

