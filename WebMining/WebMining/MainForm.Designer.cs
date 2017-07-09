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
            this.button2 = new System.Windows.Forms.Button();
            this.txtboxMinSupp = new System.Windows.Forms.TextBox();
            this.txtboxMinConf = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtboxEpsilon = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txtboxClassification = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtboxClassificationRequest = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
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
            this.button1.Size = new System.Drawing.Size(202, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Clustring (DBSCAN)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(32, 242);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(202, 42);
            this.button2.TabIndex = 7;
            this.button2.Text = "Association Rule (Apriori)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtboxMinSupp
            // 
            this.txtboxMinSupp.Location = new System.Drawing.Point(302, 242);
            this.txtboxMinSupp.Name = "txtboxMinSupp";
            this.txtboxMinSupp.Size = new System.Drawing.Size(58, 20);
            this.txtboxMinSupp.TabIndex = 8;
            this.txtboxMinSupp.Text = "0.04";
            // 
            // txtboxMinConf
            // 
            this.txtboxMinConf.Location = new System.Drawing.Point(302, 264);
            this.txtboxMinConf.Name = "txtboxMinConf";
            this.txtboxMinConf.Size = new System.Drawing.Size(58, 20);
            this.txtboxMinConf.TabIndex = 9;
            this.txtboxMinConf.Text = "0.04";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(249, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "MinSupp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "MinConf";
            // 
            // txtboxEpsilon
            // 
            this.txtboxEpsilon.Location = new System.Drawing.Point(302, 210);
            this.txtboxEpsilon.Name = "txtboxEpsilon";
            this.txtboxEpsilon.Size = new System.Drawing.Size(58, 20);
            this.txtboxEpsilon.TabIndex = 12;
            this.txtboxEpsilon.Text = "1.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Epsilon";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(32, 175);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(202, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "Study Dateset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(32, 305);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(202, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "Classification (K-NN)";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtboxClassification
            // 
            this.txtboxClassification.Location = new System.Drawing.Point(302, 307);
            this.txtboxClassification.Name = "txtboxClassification";
            this.txtboxClassification.Size = new System.Drawing.Size(58, 20);
            this.txtboxClassification.TabIndex = 16;
            this.txtboxClassification.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(276, 310);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "K";
            // 
            // txtboxClassificationRequest
            // 
            this.txtboxClassificationRequest.Location = new System.Drawing.Point(32, 348);
            this.txtboxClassificationRequest.Multiline = true;
            this.txtboxClassificationRequest.Name = "txtboxClassificationRequest";
            this.txtboxClassificationRequest.Size = new System.Drawing.Size(328, 49);
            this.txtboxClassificationRequest.TabIndex = 18;
            this.txtboxClassificationRequest.Text = "0000014602 ofssobxxxmpdu1sr NONE 72.3.217.228 BM \'Opera\' \'Mac\' 01:47:53 21-12-201" +
    "7 \'PAGE1\' \'PAGE2\'";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 338);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Request";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(32, 440);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(328, 23);
            this.button5.TabIndex = 20;
            this.button5.Text = "Run Namedpipe Server";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 475);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtboxClassificationRequest);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtboxClassification);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtboxEpsilon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtboxMinConf);
            this.Controls.Add(this.txtboxMinSupp);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBarDataClean);
            this.Controls.Add(this.lblNotifications);
            this.Controls.Add(this.btnLoadAndCleanData);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtboxMinSupp;
        private System.Windows.Forms.TextBox txtboxMinConf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtboxEpsilon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtboxClassification;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtboxClassificationRequest;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button5;
    }
}

