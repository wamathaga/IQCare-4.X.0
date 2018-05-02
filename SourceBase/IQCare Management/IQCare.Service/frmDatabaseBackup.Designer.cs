namespace IQCare.Service
{
    partial class frmDatabaseBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDatabaseBackup));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDeidentified = new System.Windows.Forms.CheckBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRestoreDatabase = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRestorePath = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDeidentified);
            this.groupBox1.Controls.Add(this.btnBackup);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPath);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 116);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Back-Up Database";
            // 
            // chkDeidentified
            // 
            this.chkDeidentified.AutoSize = true;
            this.chkDeidentified.Location = new System.Drawing.Point(23, 70);
            this.chkDeidentified.Name = "chkDeidentified";
            this.chkDeidentified.Size = new System.Drawing.Size(201, 17);
            this.chkDeidentified.TabIndex = 4;
            this.chkDeidentified.Text = "De-identify database before backup?";
            this.chkDeidentified.UseVisualStyleBackColor = true;
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(356, 70);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(112, 21);
            this.btnBackup.TabIndex = 3;
            this.btnBackup.Text = "Backup Database";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(440, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 19);
            this.button1.TabIndex = 2;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Database backup path";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(142, 33);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(290, 20);
            this.txtPath.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(541, 301);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRestoreDatabase);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtRestorePath);
            this.groupBox2.Location = new System.Drawing.Point(12, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(527, 116);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Restore Database";
            // 
            // btnRestoreDatabase
            // 
            this.btnRestoreDatabase.Location = new System.Drawing.Point(356, 70);
            this.btnRestoreDatabase.Name = "btnRestoreDatabase";
            this.btnRestoreDatabase.Size = new System.Drawing.Size(112, 21);
            this.btnRestoreDatabase.TabIndex = 3;
            this.btnRestoreDatabase.TabStop = false;
            this.btnRestoreDatabase.Text = "Restore Database";
            this.btnRestoreDatabase.UseVisualStyleBackColor = true;
            this.btnRestoreDatabase.Click += new System.EventHandler(this.btnRestoreDatabase_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(440, 34);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(28, 19);
            this.button4.TabIndex = 2;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database restore path";
            // 
            // txtRestorePath
            // 
            this.txtRestorePath.Location = new System.Drawing.Point(142, 33);
            this.txtRestorePath.Name = "txtRestorePath";
            this.txtRestorePath.Size = new System.Drawing.Size(290, 20);
            this.txtRestorePath.TabIndex = 0;
            // 
            // frmDatabaseBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 336);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDatabaseBackup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "frmForm";
            this.Text = "IQCare Database Backup";
            this.Load += new System.EventHandler(this.frmDatabaseBackup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.CheckBox chkDeidentified;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRestoreDatabase;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRestorePath;



    }
}