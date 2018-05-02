namespace IQCare.FormBuilder
{
    partial class frmDrugListBusinessRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDrugListBusinessRule));
            this.btnClose = new System.Windows.Forms.Button();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.lblSelectItems = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.chkItemList = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Window;
            this.btnClose.Location = new System.Drawing.Point(774, 476);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 73;
            this.btnClose.Tag = "btnSingleText";
            this.btnClose.Text = "&Cancel";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(6, 17);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(15, 14);
            this.chkAll.TabIndex = 77;
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // lblSelectItems
            // 
            this.lblSelectItems.AutoSize = true;
            this.lblSelectItems.Location = new System.Drawing.Point(27, 18);
            this.lblSelectItems.Name = "lblSelectItems";
            this.lblSelectItems.Size = new System.Drawing.Size(119, 13);
            this.lblSelectItems.TabIndex = 76;
            this.lblSelectItems.Tag = "lblLabelRequired";
            this.lblSelectItems.Text = "Select All [Maximum 25]";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.SystemColors.Window;
            this.btnSubmit.Location = new System.Drawing.Point(695, 476);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(80, 25);
            this.btnSubmit.TabIndex = 70;
            this.btnSubmit.Tag = "btnSingleText";
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // chkItemList
            // 
            this.chkItemList.CheckOnClick = true;
            this.chkItemList.FormattingEnabled = true;
            this.chkItemList.Location = new System.Drawing.Point(2, 37);
            this.chkItemList.Name = "chkItemList";
            this.chkItemList.Size = new System.Drawing.Size(852, 439);
            this.chkItemList.TabIndex = 75;
            this.chkItemList.Tag = "frmForm";
            // 
            // frmDrugListBusinessRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 501);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.lblSelectItems);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.chkItemList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDrugListBusinessRule";
            this.Text = "Drug List Selector";
            this.Load += new System.EventHandler(this.frmDrugListBusinessRule_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Label lblSelectItems;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.CheckedListBox chkItemList;
    }
}