namespace WebMining
{
    partial class MainForm
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
            this.btnLoadLogFile = new System.Windows.Forms.Button();
            this.lblLogfiles = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadLogFile
            // 
            this.btnLoadLogFile.Location = new System.Drawing.Point(20, 35);
            this.btnLoadLogFile.Name = "btnLoadLogFile";
            this.btnLoadLogFile.Size = new System.Drawing.Size(75, 23);
            this.btnLoadLogFile.TabIndex = 0;
            this.btnLoadLogFile.Text = "Load LogFile";
            this.btnLoadLogFile.UseVisualStyleBackColor = true;
            this.btnLoadLogFile.Click += new System.EventHandler(this.btnLoadLogFile_Click);
            // 
            // lblLogfiles
            // 
            this.lblLogfiles.AutoSize = true;
            this.lblLogfiles.Location = new System.Drawing.Point(101, 40);
            this.lblLogfiles.Name = "lblLogfiles";
            this.lblLogfiles.Size = new System.Drawing.Size(100, 13);
            this.lblLogfiles.TabIndex = 1;
            this.lblLogfiles.Text = "No LogFile Selected";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadLogFile);
            this.groupBox1.Controls.Add(this.lblLogfiles);
            this.groupBox1.Location = new System.Drawing.Point(12, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 82);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input data";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 472);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "WebMining";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadLogFile;
        private System.Windows.Forms.Label lblLogfiles;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

