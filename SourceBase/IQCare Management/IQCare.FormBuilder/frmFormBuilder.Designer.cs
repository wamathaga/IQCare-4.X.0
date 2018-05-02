namespace IQCare.FormBuilder
{
    partial class frmFormBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormBuilder));
            this.pnlPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtpaperversion = new System.Windows.Forms.TextBox();
            this.dtformrel = new System.Windows.Forms.DateTimePicker();
            this.lblReldate = new System.Windows.Forms.Label();
            this.txtlastupdatedby = new System.Windows.Forms.TextBox();
            this.lbllastupdatedby = new System.Windows.Forms.Label();
            this.txtlastupdatedate = new System.Windows.Forms.TextBox();
            this.lbllastupdatedate = new System.Windows.Forms.Label();
            this.lblpaperver = new System.Windows.Forms.Label();
            this.txtvernumber = new System.Windows.Forms.TextBox();
            this.btnformbusinessrules = new System.Windows.Forms.Button();
            this.txtTabCaptionPlaceHolder = new System.Windows.Forms.TextBox();
            this.btnManageTab = new System.Windows.Forms.Button();
            this.btnAddTab = new System.Windows.Forms.Button();
            this.tabFormBuilder = new System.Windows.Forms.TabControl();
            this.cmbTechArea = new System.Windows.Forms.ComboBox();
            this.lblTechArea = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddCustomField = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pnlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPanel
            // 
            this.pnlPanel.Controls.Add(this.label2);
            this.pnlPanel.Controls.Add(this.txtpaperversion);
            this.pnlPanel.Controls.Add(this.dtformrel);
            this.pnlPanel.Controls.Add(this.lblReldate);
            this.pnlPanel.Controls.Add(this.txtlastupdatedby);
            this.pnlPanel.Controls.Add(this.lbllastupdatedby);
            this.pnlPanel.Controls.Add(this.txtlastupdatedate);
            this.pnlPanel.Controls.Add(this.lbllastupdatedate);
            this.pnlPanel.Controls.Add(this.lblpaperver);
            this.pnlPanel.Controls.Add(this.txtvernumber);
            this.pnlPanel.Controls.Add(this.btnformbusinessrules);
            this.pnlPanel.Controls.Add(this.txtTabCaptionPlaceHolder);
            this.pnlPanel.Controls.Add(this.btnManageTab);
            this.pnlPanel.Controls.Add(this.btnAddTab);
            this.pnlPanel.Controls.Add(this.tabFormBuilder);
            this.pnlPanel.Controls.Add(this.cmbTechArea);
            this.pnlPanel.Controls.Add(this.lblTechArea);
            this.pnlPanel.Controls.Add(this.btnClose);
            this.pnlPanel.Controls.Add(this.btnAddCustomField);
            this.pnlPanel.Controls.Add(this.btnSave);
            this.pnlPanel.Controls.Add(this.txtFormName);
            this.pnlPanel.Controls.Add(this.label1);
            this.pnlPanel.Location = new System.Drawing.Point(2, 3);
            this.pnlPanel.Name = "pnlPanel";
            this.pnlPanel.Size = new System.Drawing.Size(864, 501);
            this.pnlPanel.TabIndex = 0;
            this.pnlPanel.Tag = "pnlPanel";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(494, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 34;
            this.label2.Tag = "lblLabel";
            this.label2.Text = "Paper Version :";
            // 
            // txtpaperversion
            // 
            this.txtpaperversion.Location = new System.Drawing.Point(580, 33);
            this.txtpaperversion.MaxLength = 50;
            this.txtpaperversion.Name = "txtpaperversion";
            this.txtpaperversion.Size = new System.Drawing.Size(54, 20);
            this.txtpaperversion.TabIndex = 33;
            this.txtpaperversion.Tag = "txtTextBox";
            // 
            // dtformrel
            // 
            this.dtformrel.CustomFormat = "";
            this.dtformrel.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtformrel.Location = new System.Drawing.Point(747, 32);
            this.dtformrel.Name = "dtformrel";
            this.dtformrel.Size = new System.Drawing.Size(108, 20);
            this.dtformrel.TabIndex = 32;
            this.dtformrel.Tag = "lblLabel";
            this.dtformrel.ValueChanged += new System.EventHandler(this.dtformrel_ValueChanged);
            this.dtformrel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtformrel_KeyDown);
            // 
            // lblReldate
            // 
            this.lblReldate.AutoSize = true;
            this.lblReldate.Location = new System.Drawing.Point(640, 36);
            this.lblReldate.Name = "lblReldate";
            this.lblReldate.Size = new System.Drawing.Size(104, 13);
            this.lblReldate.TabIndex = 31;
            this.lblReldate.Tag = "lblLabel";
            this.lblReldate.Text = "Form Release Date :";
            // 
            // txtlastupdatedby
            // 
            this.txtlastupdatedby.Location = new System.Drawing.Point(394, 33);
            this.txtlastupdatedby.MaxLength = 50;
            this.txtlastupdatedby.Name = "txtlastupdatedby";
            this.txtlastupdatedby.Size = new System.Drawing.Size(94, 20);
            this.txtlastupdatedby.TabIndex = 30;
            this.txtlastupdatedby.Tag = "txtTextBox";
            // 
            // lbllastupdatedby
            // 
            this.lbllastupdatedby.AutoSize = true;
            this.lbllastupdatedby.Location = new System.Drawing.Point(325, 36);
            this.lbllastupdatedby.Name = "lbllastupdatedby";
            this.lbllastupdatedby.Size = new System.Drawing.Size(68, 13);
            this.lbllastupdatedby.TabIndex = 29;
            this.lbllastupdatedby.Tag = "lblLabel";
            this.lbllastupdatedby.Text = "Updated by :";
            // 
            // txtlastupdatedate
            // 
            this.txtlastupdatedate.Location = new System.Drawing.Point(222, 33);
            this.txtlastupdatedate.MaxLength = 50;
            this.txtlastupdatedate.Name = "txtlastupdatedate";
            this.txtlastupdatedate.Size = new System.Drawing.Size(86, 20);
            this.txtlastupdatedate.TabIndex = 28;
            this.txtlastupdatedate.Tag = "txtTextBox";
            // 
            // lbllastupdatedate
            // 
            this.lbllastupdatedate.AutoSize = true;
            this.lbllastupdatedate.Location = new System.Drawing.Point(154, 36);
            this.lbllastupdatedate.Name = "lbllastupdatedate";
            this.lbllastupdatedate.Size = new System.Drawing.Size(69, 13);
            this.lbllastupdatedate.TabIndex = 27;
            this.lbllastupdatedate.Tag = "lblLabel";
            this.lbllastupdatedate.Text = "Updated on :";
            // 
            // lblpaperver
            // 
            this.lblpaperver.Location = new System.Drawing.Point(4, 36);
            this.lblpaperver.Name = "lblpaperver";
            this.lblpaperver.Size = new System.Drawing.Size(98, 15);
            this.lblpaperver.TabIndex = 26;
            this.lblpaperver.Tag = "lblLabel";
            this.lblpaperver.Text = "Electronic Version :";
            // 
            // txtvernumber
            // 
            this.txtvernumber.Location = new System.Drawing.Point(104, 33);
            this.txtvernumber.MaxLength = 50;
            this.txtvernumber.Name = "txtvernumber";
            this.txtvernumber.Size = new System.Drawing.Size(45, 20);
            this.txtvernumber.TabIndex = 25;
            this.txtvernumber.Tag = "txtTextBox";
            // 
            // btnformbusinessrules
            // 
            this.btnformbusinessrules.Location = new System.Drawing.Point(671, 0);
            this.btnformbusinessrules.Name = "btnformbusinessrules";
            this.btnformbusinessrules.Size = new System.Drawing.Size(163, 23);
            this.btnformbusinessrules.TabIndex = 16;
            this.btnformbusinessrules.Text = "Form Business Rules";
            this.btnformbusinessrules.UseVisualStyleBackColor = true;
            this.btnformbusinessrules.Click += new System.EventHandler(this.btnformbusinessrules_Click);
            // 
            // txtTabCaptionPlaceHolder
            // 
            this.txtTabCaptionPlaceHolder.Location = new System.Drawing.Point(629, 442);
            this.txtTabCaptionPlaceHolder.MaxLength = 50;
            this.txtTabCaptionPlaceHolder.Name = "txtTabCaptionPlaceHolder";
            this.txtTabCaptionPlaceHolder.Size = new System.Drawing.Size(226, 20);
            this.txtTabCaptionPlaceHolder.TabIndex = 15;
            this.txtTabCaptionPlaceHolder.Tag = "txtTextBox";
            this.txtTabCaptionPlaceHolder.Visible = false;
            this.txtTabCaptionPlaceHolder.Leave += new System.EventHandler(this.txtTabCaptionPlaceHolder_Leave);
            // 
            // btnManageTab
            // 
            this.btnManageTab.Location = new System.Drawing.Point(267, 474);
            this.btnManageTab.Name = "btnManageTab";
            this.btnManageTab.Size = new System.Drawing.Size(106, 23);
            this.btnManageTab.TabIndex = 14;
            this.btnManageTab.Tag = "btnH25WFlexi";
            this.btnManageTab.Text = "Manage &Tabs";
            this.btnManageTab.UseVisualStyleBackColor = true;
            this.btnManageTab.Click += new System.EventHandler(this.btnManageTab_Click);
            // 
            // btnAddTab
            // 
            this.btnAddTab.Location = new System.Drawing.Point(158, 474);
            this.btnAddTab.Name = "btnAddTab";
            this.btnAddTab.Size = new System.Drawing.Size(106, 23);
            this.btnAddTab.TabIndex = 13;
            this.btnAddTab.Tag = "btnH25WFlexi";
            this.btnAddTab.Text = "&Add Tab";
            this.btnAddTab.UseVisualStyleBackColor = true;
            this.btnAddTab.Click += new System.EventHandler(this.btnAddTab_Click);
            // 
            // tabFormBuilder
            // 
            this.tabFormBuilder.Location = new System.Drawing.Point(4, 57);
            this.tabFormBuilder.Name = "tabFormBuilder";
            this.tabFormBuilder.SelectedIndex = 0;
            this.tabFormBuilder.Size = new System.Drawing.Size(860, 410);
            this.tabFormBuilder.TabIndex = 12;
            this.tabFormBuilder.Tag = "";
            this.tabFormBuilder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabFormBuilder_MouseDoubleClick);
            // 
            // cmbTechArea
            // 
            this.cmbTechArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTechArea.FormattingEnabled = true;
            this.cmbTechArea.Location = new System.Drawing.Point(358, 3);
            this.cmbTechArea.Name = "cmbTechArea";
            this.cmbTechArea.Size = new System.Drawing.Size(274, 21);
            this.cmbTechArea.TabIndex = 9;
            this.cmbTechArea.Tag = "ddlDropDownList";
            this.cmbTechArea.SelectionChangeCommitted += new System.EventHandler(this.cmbTechArea_SelectionChangeCommitted);
            // 
            // lblTechArea
            // 
            this.lblTechArea.AutoSize = true;
            this.lblTechArea.Location = new System.Drawing.Point(312, 6);
            this.lblTechArea.Name = "lblTechArea";
            this.lblTechArea.Size = new System.Drawing.Size(49, 13);
            this.lblTechArea.TabIndex = 8;
            this.lblTechArea.Tag = "lblLabel";
            this.lblTechArea.Text = "Service :";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(485, 474);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Tag = "btnH25WFlexi";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddCustomField
            // 
            this.btnAddCustomField.Location = new System.Drawing.Point(594, 474);
            this.btnAddCustomField.Name = "btnAddCustomField";
            this.btnAddCustomField.Size = new System.Drawing.Size(106, 23);
            this.btnAddCustomField.TabIndex = 6;
            this.btnAddCustomField.Tag = "btnH25WFlexi";
            this.btnAddCustomField.Text = "Manage &Fields";
            this.btnAddCustomField.UseVisualStyleBackColor = true;
            this.btnAddCustomField.Click += new System.EventHandler(this.btnAddCustomField_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(376, 474);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Tag = "btnH25WFlexi";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtFormName
            // 
            this.txtFormName.Location = new System.Drawing.Point(80, 3);
            this.txtFormName.MaxLength = 50;
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Size = new System.Drawing.Size(226, 20);
            this.txtFormName.TabIndex = 1;
            this.txtFormName.Tag = "txtTextBox";
            this.txtFormName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFormName_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Form Name :";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(200, 100);
            this.tabPage1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(200, 100);
            this.tabPage2.TabIndex = 0;
            // 
            // frmFormBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 507);
            this.Controls.Add(this.pnlPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFormBuilder";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "IQCare Form Builder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFormBuilder_FormClosing);
            this.Load += new System.EventHandler(this.frmFormBuilder_Load);
            this.pnlPanel.ResumeLayout(false);
            this.pnlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFormName;
        private System.Windows.Forms.Button btnAddCustomField;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTechArea;
        private System.Windows.Forms.ComboBox cmbTechArea;
        private System.Windows.Forms.Button btnAddTab;
        private System.Windows.Forms.TabControl tabFormBuilder;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnManageTab;
        private System.Windows.Forms.TextBox txtTabCaptionPlaceHolder;
        private System.Windows.Forms.Button btnformbusinessrules;
        private System.Windows.Forms.DateTimePicker dtformrel;
        private System.Windows.Forms.Label lblReldate;
        private System.Windows.Forms.TextBox txtlastupdatedby;
        private System.Windows.Forms.Label lbllastupdatedby;
        private System.Windows.Forms.TextBox txtlastupdatedate;
        private System.Windows.Forms.Label lbllastupdatedate;
        private System.Windows.Forms.Label lblpaperver;
        private System.Windows.Forms.TextBox txtvernumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtpaperversion;

    }
}