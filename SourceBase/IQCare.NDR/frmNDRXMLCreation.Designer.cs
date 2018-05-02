namespace IQCare.NDR
{
    partial class frmNDRXMLCreation
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
            this.btnGenerateNDRXml = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pBar1
            // 
            this.pBar1.Location = new System.Drawing.Point(55, 33);
            this.pBar1.Name = "pBar1";
            this.pBar1.Size = new System.Drawing.Size(299, 23);
            this.pBar1.TabIndex = 3;
            // 
            // btnGenerateNDRXml
            // 
            this.btnGenerateNDRXml.Location = new System.Drawing.Point(93, 133);
            this.btnGenerateNDRXml.Name = "btnGenerateNDRXml";
            this.btnGenerateNDRXml.Size = new System.Drawing.Size(206, 23);
            this.btnGenerateNDRXml.TabIndex = 2;
            this.btnGenerateNDRXml.Text = "Generate NDR XML";
            this.btnGenerateNDRXml.UseVisualStyleBackColor = true;
            this.btnGenerateNDRXml.Click += new System.EventHandler(this.btnGenerateNDRXml_Click);
            // 
            // frmNDRXMLCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 255);
            this.Controls.Add(this.pBar1);
            this.Controls.Add(this.btnGenerateNDRXml);
            this.Name = "frmNDRXMLCreation";
            this.Text = "frmNDRXMLGeneration";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pBar1;
        private System.Windows.Forms.Button btnGenerateNDRXml;
    }
}