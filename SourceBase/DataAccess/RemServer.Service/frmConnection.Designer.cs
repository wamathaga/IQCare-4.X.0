namespace RemServer
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
            this.SuspendLayout();
            // 
            // btnexit
            // 
            this.btnexit.Location = new System.Drawing.Point(278, 166);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(95, 23);
            this.btnexit.TabIndex = 19;
            this.btnexit.Text = "E&xit";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(177, 166);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtconfirmpass
            // 
            this.txtconfirmpass.BackColor = System.Drawing.SystemColors.Window;
            this.txtconfirmpass.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtconfirmpass.Location = new System.Drawing.Point(135, 120);
            this.txtconfirmpass.Name = "txtconfirmpass";
            this.txtconfirmpass.PasswordChar = '*';
            this.txtconfirmpass.Size = new System.Drawing.Size(216, 21);
            this.txtconfirmpass.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 15);
            this.label5.TabIndex = 20;
            this.label5.Text = "Verify Password";
            // 
            // txtDBnm
            // 
            this.txtDBnm.BackColor = System.Drawing.SystemColors.Window;
            this.txtDBnm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBnm.Location = new System.Drawing.Point(135, 39);
            this.txtDBnm.Name = "txtDBnm";
            this.txtDBnm.Size = new System.Drawing.Size(216, 21);
            this.txtDBnm.TabIndex = 11;
            // 
            // txtUsernm
            // 
            this.txtUsernm.BackColor = System.Drawing.SystemColors.Window;
            this.txtUsernm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsernm.Location = new System.Drawing.Point(135, 66);
            this.txtUsernm.Name = "txtUsernm";
            this.txtUsernm.Size = new System.Drawing.Size(216, 21);
            this.txtUsernm.TabIndex = 14;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtPassword.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(135, 93);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(216, 21);
            this.txtPassword.TabIndex = 16;
            // 
            // txtServernm
            // 
            this.txtServernm.BackColor = System.Drawing.SystemColors.Window;
            this.txtServernm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServernm.Location = new System.Drawing.Point(135, 12);
            this.txtServernm.Name = "txtServernm";
            this.txtServernm.Size = new System.Drawing.Size(216, 21);
            this.txtServernm.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "DataBase Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Server Name";
            // 
            // frmConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 202);
            this.Controls.Add(this.btnexit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtconfirmpass);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDBnm);
            this.Controls.Add(this.txtUsernm);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtServernm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmConnection";
            this.Text = "Connection ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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