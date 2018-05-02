namespace IQCare.Service
{
    partial class frmConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnection));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnexit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtconfirmpass = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDBnm = new System.Windows.Forms.TextBox();
            this.txtUsernm = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtServernm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnexit);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.txtconfirmpass);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtDBnm);
            this.panel1.Controls.Add(this.txtUsernm);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.txtServernm);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 198);
            this.panel1.TabIndex = 9;
            this.panel1.Tag = "pnlPanel";
            // 
            // btnexit
            // 
            this.btnexit.Location = new System.Drawing.Point(259, 165);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(95, 23);
            this.btnexit.TabIndex = 19;
            this.btnexit.Tag = "btnSingleText";
            this.btnexit.Text = "E&xit";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(158, 165);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Tag = "btnSingleText";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtconfirmpass
            // 
            this.txtconfirmpass.BackColor = System.Drawing.SystemColors.Window;
            this.txtconfirmpass.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtconfirmpass.Location = new System.Drawing.Point(141, 119);
            this.txtconfirmpass.Name = "txtconfirmpass";
            this.txtconfirmpass.PasswordChar = '*';
            this.txtconfirmpass.Size = new System.Drawing.Size(216, 21);
            this.txtconfirmpass.TabIndex = 17;
            this.txtconfirmpass.Tag = "txtTextBox";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 15);
            this.label5.TabIndex = 20;
            this.label5.Tag = "lblLabel";
            this.label5.Text = "Verify Password";
            // 
            // txtDBnm
            // 
            this.txtDBnm.BackColor = System.Drawing.SystemColors.Window;
            this.txtDBnm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBnm.Location = new System.Drawing.Point(141, 38);
            this.txtDBnm.Name = "txtDBnm";
            this.txtDBnm.Size = new System.Drawing.Size(216, 21);
            this.txtDBnm.TabIndex = 11;
            this.txtDBnm.Tag = "txtTextBox";
           
            // txtUsernm
            // 
            this.txtUsernm.BackColor = System.Drawing.SystemColors.Window;
            this.txtUsernm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsernm.Location = new System.Drawing.Point(141, 65);
            this.txtUsernm.Name = "txtUsernm";
            this.txtUsernm.Size = new System.Drawing.Size(216, 21);
            this.txtUsernm.TabIndex = 14;
            this.txtUsernm.Tag = "txtTextBox";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtPassword.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(141, 92);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(216, 21);
            this.txtPassword.TabIndex = 16;
            this.txtPassword.Tag = "txtTextBox";
            // 
            // txtServernm
            // 
            this.txtServernm.BackColor = System.Drawing.SystemColors.Window;
            this.txtServernm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServernm.Location = new System.Drawing.Point(141, 11);
            this.txtServernm.Name = "txtServernm";
            this.txtServernm.Size = new System.Drawing.Size(216, 21);
            this.txtServernm.TabIndex = 9;
            this.txtServernm.Tag = "txtTextBox";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 15);
            this.label4.TabIndex = 15;
            this.label4.Tag = "lblLabel";
            this.label4.Text = "DataBase Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 13;
            this.label3.Tag = "lblLabel";
            this.label3.Text = "User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 12;
            this.label2.Tag = "lblLabel";
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 10;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Server Name";
            // 
            // frmConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 200);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "frmForm";
            this.Text = "New Connection";
            this.Load += new System.EventHandler(this.frmConnection_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtconfirmpass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDBnm;
        private System.Windows.Forms.TextBox txtUsernm;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtServernm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

    }
}