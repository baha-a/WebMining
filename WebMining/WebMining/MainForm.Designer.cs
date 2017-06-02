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
            this.btnLoadAndCleanData = new System.Windows.Forms.Button();
            this.lblNotifications = new System.Windows.Forms.Label();
            this.progressBarDataClean = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
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
            // btnLoadAndCleanData
            // 
            this.btnLoadAndCleanData.Location = new System.Drawing.Point(32, 124);
            this.btnLoadAndCleanData.Name = "btnLoadAndCleanData";
            this.btnLoadAndCleanData.Size = new System.Drawing.Size(75, 23);
            this.btnLoadAndCleanData.TabIndex = 3;
            this.btnLoadAndCleanData.Text = "Data Clean";
            this.btnLoadAndCleanData.UseVisualStyleBackColor = true;
            this.btnLoadAndCleanData.Click += new System.EventHandler(this.btnLoadAndCleanData_Click);
            // 
            // lblNotifications
            // 
            this.lblNotifications.AutoSize = true;
            this.lblNotifications.Location = new System.Drawing.Point(138, 134);
            this.lblNotifications.Name = "lblNotifications";
            this.lblNotifications.Size = new System.Drawing.Size(11, 13);
            this.lblNotifications.TabIndex = 4;
            this.lblNotifications.Text = "-";
            // 
            // progressBarDataClean
            // 
            this.progressBarDataClean.Location = new System.Drawing.Point(12, 158);
            this.progressBarDataClean.Name = "progressBarDataClean";
            this.progressBarDataClean.Size = new System.Drawing.Size(372, 11);
            this.progressBarDataClean.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 210);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(328, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "test Clustring DBSCAN";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 265);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBarDataClean);
            this.Controls.Add(this.lblNotifications);
            this.Controls.Add(this.btnLoadAndCleanData);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "WebMining";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadLogFile;
        private System.Windows.Forms.Label lblLogfiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLoadAndCleanData;
        private System.Windows.Forms.Label lblNotifications;
        private System.Windows.Forms.ProgressBar progressBarDataClean;
        private System.Windows.Forms.Button button1;
    }
}

