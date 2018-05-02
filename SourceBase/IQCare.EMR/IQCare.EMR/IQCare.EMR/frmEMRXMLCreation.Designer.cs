namespace IQCare.EMR
{
    partial class frmEMRXMLCreation
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
            this.pBar1 = new System.Windows.Forms.ProgressBar();
            this.btnGenerateXml = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pBar1
            // 
            this.pBar1.Location = new System.Drawing.Point(81, 35);
            this.pBar1.Name = "pBar1";
            this.pBar1.Size = new System.Drawing.Size(299, 23);
            this.pBar1.TabIndex = 5;
            // 
            // btnGenerateXml
            // 
            this.btnGenerateXml.Location = new System.Drawing.Point(119, 135);
            this.btnGenerateXml.Name = "btnGenerateXml";
            this.btnGenerateXml.Size = new System.Drawing.Size(206, 23);
            this.btnGenerateXml.TabIndex = 4;
            this.btnGenerateXml.Text = "Generate XML";
            this.btnGenerateXml.UseVisualStyleBackColor = true;
            this.btnGenerateXml.Click += new System.EventHandler(this.btnGenerateNDRXml_Click);
            // 
            // frmEMRXMLCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 192);
            this.Controls.Add(this.pBar1);
            this.Controls.Add(this.btnGenerateXml);
            this.Name = "frmEMRXMLCreation";
            this.Text = "frmEMRXMLCreation";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pBar1;
        private System.Windows.Forms.Button btnGenerateXml;
    }
}