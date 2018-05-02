namespace IQCare.SCM
{
    partial class UserPrintControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblexpire1 = new System.Windows.Forms.Label();
            this.pnlprint = new System.Windows.Forms.Panel();
            this.lblfacility = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.txtinstruction = new System.Windows.Forms.TextBox();
            this.lbldrgquantity = new System.Windows.Forms.Label();
            this.chkDrugName = new System.Windows.Forms.CheckBox();
            this.cmbnocopies = new System.Windows.Forms.ComboBox();
            this.lblnoofcopies = new System.Windows.Forms.Label();
            this.lblStore = new System.Windows.Forms.Label();
            this.pnlprint.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblexpire1
            // 
            this.lblexpire1.AutoSize = true;
            this.lblexpire1.Location = new System.Drawing.Point(110, 68);
            this.lblexpire1.Name = "lblexpire1";
            this.lblexpire1.Size = new System.Drawing.Size(0, 16);
            this.lblexpire1.TabIndex = 7;
            // 
            // pnlprint
            // 
            this.pnlprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlprint.Controls.Add(this.lblStore);
            this.pnlprint.Controls.Add(this.lblfacility);
            this.pnlprint.Controls.Add(this.lblPatientName);
            this.pnlprint.Controls.Add(this.txtinstruction);
            this.pnlprint.Controls.Add(this.lbldrgquantity);
            this.pnlprint.Controls.Add(this.chkDrugName);
            this.pnlprint.Location = new System.Drawing.Point(-1, -1);
            this.pnlprint.Name = "pnlprint";
            this.pnlprint.Size = new System.Drawing.Size(286, 142);
            this.pnlprint.TabIndex = 12;
            // 
            // lblfacility
            // 
            this.lblfacility.AutoSize = true;
            this.lblfacility.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfacility.Location = new System.Drawing.Point(8, 1);
            this.lblfacility.Name = "lblfacility";
            this.lblfacility.Size = new System.Drawing.Size(60, 16);
            this.lblfacility.TabIndex = 15;
            this.lblfacility.Text = "facilityName";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.Location = new System.Drawing.Point(8, 40);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(31, 16);
            this.lblPatientName.TabIndex = 14;
            this.lblPatientName.Tag = "";
            this.lblPatientName.Text = "name";
            // 
            // txtinstruction
            // 
            this.txtinstruction.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtinstruction.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtinstruction.Location = new System.Drawing.Point(8, 116);
            this.txtinstruction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtinstruction.Name = "txtinstruction";
            this.txtinstruction.Size = new System.Drawing.Size(273, 21);
            this.txtinstruction.TabIndex = 13;
            // 
            // lbldrgquantity
            // 
            this.lbldrgquantity.AutoSize = true;
            this.lbldrgquantity.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldrgquantity.Location = new System.Drawing.Point(8, 96);
            this.lbldrgquantity.Name = "lbldrgquantity";
            this.lbldrgquantity.Size = new System.Drawing.Size(66, 16);
            this.lbldrgquantity.TabIndex = 12;
            this.lbldrgquantity.Text = "Drug Quantity";
            // 
            // chkDrugName
            // 
            this.chkDrugName.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDrugName.Location = new System.Drawing.Point(8, 60);
            this.chkDrugName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkDrugName.Name = "chkDrugName";
            this.chkDrugName.Size = new System.Drawing.Size(273, 38);
            this.chkDrugName.TabIndex = 11;
            this.chkDrugName.Text = "chkDrugName";
            this.chkDrugName.UseVisualStyleBackColor = true;
            // 
            // cmbnocopies
            // 
            this.cmbnocopies.DisplayMember = "1";
            this.cmbnocopies.FormattingEnabled = true;
            this.cmbnocopies.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbnocopies.Location = new System.Drawing.Point(99, 148);
            this.cmbnocopies.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbnocopies.Name = "cmbnocopies";
            this.cmbnocopies.Size = new System.Drawing.Size(101, 24);
            this.cmbnocopies.TabIndex = 14;
            this.cmbnocopies.ValueMember = "1";
            // 
            // lblnoofcopies
            // 
            this.lblnoofcopies.AutoSize = true;
            this.lblnoofcopies.Location = new System.Drawing.Point(8, 149);
            this.lblnoofcopies.Name = "lblnoofcopies";
            this.lblnoofcopies.Size = new System.Drawing.Size(85, 16);
            this.lblnoofcopies.TabIndex = 13;
            this.lblnoofcopies.Text = "Number of copies:";
            // 
            // lblStore
            // 
            this.lblStore.AutoSize = true;
            this.lblStore.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStore.Location = new System.Drawing.Point(8, 20);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(56, 16);
            this.lblStore.TabIndex = 16;
            this.lblStore.Text = "StoreName";
            // 
            // UserPrintControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbnocopies);
            this.Controls.Add(this.lblnoofcopies);
            this.Controls.Add(this.pnlprint);
            this.Controls.Add(this.lblexpire1);
            this.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UserPrintControl";
            this.Size = new System.Drawing.Size(287, 180);
            this.Tag = "frmForm";
            this.Load += new System.EventHandler(this.UserPrintControl_Load);
            this.pnlprint.ResumeLayout(false);
            this.pnlprint.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblexpire1;
        private System.Windows.Forms.Panel pnlprint;
        private System.Windows.Forms.Label lblfacility;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.TextBox txtinstruction;
        private System.Windows.Forms.Label lbldrgquantity;
        private System.Windows.Forms.CheckBox chkDrugName;
        private System.Windows.Forms.ComboBox cmbnocopies;
        private System.Windows.Forms.Label lblnoofcopies;
        private System.Windows.Forms.Label lblStore;
    }
}
