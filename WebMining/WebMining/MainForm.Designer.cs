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
            this.txtboxClassificationRequest = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listboxState = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.txtboxClassification = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadLogFile
            // 
            this.btnLoadLogFile.Location = new System.Drawing.Point(18, 26);
            this.btnLoadLogFile.Name = "btnLoadLogFile";
            this.btnLoadLogFile.Size = new System.Drawing.Size(115, 23);
            this.btnLoadLogFile.TabIndex = 0;
            this.btnLoadLogFile.Text = "0 - Select Input Files";
            this.btnLoadLogFile.UseVisualStyleBackColor = true;
            this.btnLoadLogFile.Click += new System.EventHandler(this.btnLoadLogFile_Click);
            // 
            // lblLogfiles
            // 
            this.lblLogfiles.AutoSize = true;
            this.lblLogfiles.Location = new System.Drawing.Point(158, 31);
            this.lblLogfiles.Name = "lblLogfiles";
            this.lblLogfiles.Size = new System.Drawing.Size(83, 13);
            this.lblLogfiles.TabIndex = 1;
            this.lblLogfiles.Text = "No File Selected";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadLogFile);
            this.groupBox1.Controls.Add(this.lblLogfiles);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 63);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input data";
            // 
            // btnLoadAndCleanData
            // 
            this.btnLoadAndCleanData.Location = new System.Drawing.Point(18, 22);
            this.btnLoadAndCleanData.Name = "btnLoadAndCleanData";
            this.btnLoadAndCleanData.Size = new System.Drawing.Size(202, 23);
            this.btnLoadAndCleanData.TabIndex = 1;
            this.btnLoadAndCleanData.Text = "1 - Data Clean";
            this.btnLoadAndCleanData.UseVisualStyleBackColor = true;
            this.btnLoadAndCleanData.Click += new System.EventHandler(this.btnLoadAndCleanData_Click);
            // 
            // lblNotifications
            // 
            this.lblNotifications.AutoSize = true;
            this.lblNotifications.Location = new System.Drawing.Point(27, 436);
            this.lblNotifications.Name = "lblNotifications";
            this.lblNotifications.Size = new System.Drawing.Size(11, 13);
            this.lblNotifications.TabIndex = 4;
            this.lblNotifications.Text = "-";
            // 
            // progressBarDataClean
            // 
            this.progressBarDataClean.Location = new System.Drawing.Point(12, 452);
            this.progressBarDataClean.Name = "progressBarDataClean";
            this.progressBarDataClean.Size = new System.Drawing.Size(372, 11);
            this.progressBarDataClean.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(202, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "2 - Clustring (DBSCAN)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(18, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(202, 42);
            this.button2.TabIndex = 6;
            this.button2.Text = "2 - Association Rule (Apriori)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtboxMinSupp
            // 
            this.txtboxMinSupp.Location = new System.Drawing.Point(291, 92);
            this.txtboxMinSupp.Name = "txtboxMinSupp";
            this.txtboxMinSupp.Size = new System.Drawing.Size(58, 20);
            this.txtboxMinSupp.TabIndex = 4;
            this.txtboxMinSupp.Text = "0.04";
            // 
            // txtboxMinConf
            // 
            this.txtboxMinConf.Location = new System.Drawing.Point(291, 114);
            this.txtboxMinConf.Name = "txtboxMinConf";
            this.txtboxMinConf.Size = new System.Drawing.Size(58, 20);
            this.txtboxMinConf.TabIndex = 5;
            this.txtboxMinConf.Text = "0.04";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(238, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "MinSupp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "MinConf";
            // 
            // txtboxEpsilon
            // 
            this.txtboxEpsilon.Location = new System.Drawing.Point(291, 60);
            this.txtboxEpsilon.Name = "txtboxEpsilon";
            this.txtboxEpsilon.Size = new System.Drawing.Size(58, 20);
            this.txtboxEpsilon.TabIndex = 2;
            this.txtboxEpsilon.Text = "5.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Epsilon";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(18, 146);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(202, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Analyze Dateset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtboxClassificationRequest
            // 
            this.txtboxClassificationRequest.Location = new System.Drawing.Point(17, 39);
            this.txtboxClassificationRequest.Multiline = true;
            this.txtboxClassificationRequest.Name = "txtboxClassificationRequest";
            this.txtboxClassificationRequest.Size = new System.Drawing.Size(329, 49);
            this.txtboxClassificationRequest.TabIndex = 10;
            this.txtboxClassificationRequest.Text = "0000014602 ofssobxxxmpdu1sr NONE 72.3.217.228 BM \'Opera\' \'Mac\' 01:47:53 21-12-201" +
    "7 \'PAGE1\' \'PAGE2\'";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Request:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(18, 130);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(328, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "Run as Server";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.txtboxMinSupp);
            this.groupBox2.Controls.Add(this.txtboxMinConf);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtboxEpsilon);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnLoadAndCleanData);
            this.groupBox2.Location = new System.Drawing.Point(12, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 175);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Offline Algorithms";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.txtboxClassification);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtboxClassificationRequest);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(12, 262);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(372, 163);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Online Algorithms";
            // 
            // listboxState
            // 
            this.listboxState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listboxState.FormattingEnabled = true;
            this.listboxState.Location = new System.Drawing.Point(402, 34);
            this.listboxState.Name = "listboxState";
            this.listboxState.Size = new System.Drawing.Size(282, 433);
            this.listboxState.TabIndex = 1000;
            this.listboxState.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(399, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "State";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(18, 94);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(202, 23);
            this.button4.TabIndex = 21;
            this.button4.Text = "3 - Classification (K-NN)";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // txtboxClassification
            // 
            this.txtboxClassification.Location = new System.Drawing.Point(289, 96);
            this.txtboxClassification.Name = "txtboxClassification";
            this.txtboxClassification.Size = new System.Drawing.Size(58, 20);
            this.txtboxClassification.TabIndex = 20;
            this.txtboxClassification.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(269, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "K";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 475);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listboxState);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.progressBarDataClean);
            this.Controls.Add(this.lblNotifications);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "WebMining";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.TextBox txtboxClassificationRequest;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listboxState;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtboxClassification;
        private System.Windows.Forms.Label label4;
    }
}

