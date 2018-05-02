namespace IQCare.SCM
{
    partial class frmPrintLabel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintLabel));
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.btn_print = new System.Windows.Forms.Button();
            this.pnlusercontrol = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // printDialog1
            // 
            this.printDialog1.AllowSelection = true;
            this.printDialog1.AllowSomePages = true;
            this.printDialog1.UseEXDialog = true;
            // 
            // btn_print
            // 
            this.btn_print.Location = new System.Drawing.Point(294, 466);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(144, 23);
            this.btn_print.TabIndex = 4;
            this.btn_print.Tag = "lblLabel";
            this.btn_print.Text = "Print Label";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // pnlusercontrol
            // 
            this.pnlusercontrol.Location = new System.Drawing.Point(12, 12);
            this.pnlusercontrol.Name = "pnlusercontrol";
            this.pnlusercontrol.Size = new System.Drawing.Size(828, 448);
            this.pnlusercontrol.TabIndex = 5;
            this.pnlusercontrol.Tag = "";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(444, 466);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(153, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Tag = "lblLabel";
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmPrintLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 537);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlusercontrol);
            this.Controls.Add(this.btn_print);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrintLabel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Print Label";
            this.Load += new System.EventHandler(this.frmPrintLabel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Panel pnlusercontrol;
        private System.Windows.Forms.Button btnClose;
    }
}