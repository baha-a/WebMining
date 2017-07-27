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
            this.label11 = new System.Windows.Forms.Label();
            this.txtboxRemovingThreshold = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtboxLevelOfProposedNewTopology = new System.Windows.Forms.TextBox();
            this.btnSuggestNewToopology = new System.Windows.Forms.Button();
            this.btnGraphReport = new System.Windows.Forms.Button();
            this.txtboxSessionTimeOut = new System.Windows.Forms.TextBox();
            this.btnMarkovBuild = new System.Windows.Forms.Button();
            this.txtboxMinPTS = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSpawnClient = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtboxMarkovDepth = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.txtboxClassification = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listboxState = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.btnZoomInListbox = new System.Windows.Forms.Button();
            this.btnZoomOuListbox = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadLogFile
            // 
            this.btnLoadLogFile.Location = new System.Drawing.Point(18, 19);
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
            this.lblLogfiles.Location = new System.Drawing.Point(158, 24);
            this.lblLogfiles.Name = "lblLogfiles";
            this.lblLogfiles.Size = new System.Drawing.Size(83, 13);
            this.lblLogfiles.TabIndex = 1;
            this.lblLogfiles.Text = "No File Selected";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadLogFile);
            this.groupBox1.Controls.Add(this.lblLogfiles);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 63);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input data";
            // 
            // btnLoadAndCleanData
            // 
            this.btnLoadAndCleanData.Location = new System.Drawing.Point(18, 15);
            this.btnLoadAndCleanData.Name = "btnLoadAndCleanData";
            this.btnLoadAndCleanData.Size = new System.Drawing.Size(202, 23);
            this.btnLoadAndCleanData.TabIndex = 2;
            this.btnLoadAndCleanData.Text = "1 - Data Clean";
            this.btnLoadAndCleanData.UseVisualStyleBackColor = true;
            this.btnLoadAndCleanData.Click += new System.EventHandler(this.btnLoadAndCleanData_Click);
            // 
            // lblNotifications
            // 
            this.lblNotifications.AutoSize = true;
            this.lblNotifications.Location = new System.Drawing.Point(27, 515);
            this.lblNotifications.Name = "lblNotifications";
            this.lblNotifications.Size = new System.Drawing.Size(11, 13);
            this.lblNotifications.TabIndex = 4;
            this.lblNotifications.Text = "-";
            // 
            // progressBarDataClean
            // 
            this.progressBarDataClean.Location = new System.Drawing.Point(12, 530);
            this.progressBarDataClean.Name = "progressBarDataClean";
            this.progressBarDataClean.Size = new System.Drawing.Size(372, 11);
            this.progressBarDataClean.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(202, 43);
            this.button1.TabIndex = 5;
            this.button1.Text = "2 - Clustring (DBSCAN)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(18, 106);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(202, 42);
            this.button2.TabIndex = 8;
            this.button2.Text = "2 - Association Rule (Apriori)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtboxMinSupp
            // 
            this.txtboxMinSupp.Location = new System.Drawing.Point(291, 104);
            this.txtboxMinSupp.Name = "txtboxMinSupp";
            this.txtboxMinSupp.Size = new System.Drawing.Size(58, 20);
            this.txtboxMinSupp.TabIndex = 6;
            this.txtboxMinSupp.Text = "0.5";
            this.txtboxMinSupp.TextChanged += new System.EventHandler(this.numberOnlyTextBox_TextChanged);
            this.txtboxMinSupp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberOnlyTextBox_KeyPress);
            // 
            // txtboxMinConf
            // 
            this.txtboxMinConf.Location = new System.Drawing.Point(291, 126);
            this.txtboxMinConf.Name = "txtboxMinConf";
            this.txtboxMinConf.Size = new System.Drawing.Size(58, 20);
            this.txtboxMinConf.TabIndex = 7;
            this.txtboxMinConf.Text = "0.5";
            this.txtboxMinConf.TextChanged += new System.EventHandler(this.numberOnlyTextBox_TextChanged);
            this.txtboxMinConf.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberOnlyTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(238, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "MinSupp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "MinConf";
            // 
            // txtboxEpsilon
            // 
            this.txtboxEpsilon.Location = new System.Drawing.Point(291, 51);
            this.txtboxEpsilon.Name = "txtboxEpsilon";
            this.txtboxEpsilon.Size = new System.Drawing.Size(58, 20);
            this.txtboxEpsilon.TabIndex = 3;
            this.txtboxEpsilon.Text = "0.8";
            this.txtboxEpsilon.TextChanged += new System.EventHandler(this.numberOnlyTextBox_TextChanged);
            this.txtboxEpsilon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberOnlyTextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Epsilon";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(18, 183);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(203, 62);
            this.button3.TabIndex = 10;
            this.button3.Text = "Analyze The Website";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtboxClassificationRequest
            // 
            this.txtboxClassificationRequest.Location = new System.Drawing.Point(17, 27);
            this.txtboxClassificationRequest.Multiline = true;
            this.txtboxClassificationRequest.Name = "txtboxClassificationRequest";
            this.txtboxClassificationRequest.Size = new System.Drawing.Size(329, 49);
            this.txtboxClassificationRequest.TabIndex = 11;
            this.txtboxClassificationRequest.Text = "0000014602 ofssobxxxmpdu1sr NONE 72.3.217.228 BM \'Opera\' \'Mac\' 01:47:53 21-12-201" +
    "7 \'HOME\' \'START\'";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Request:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(19, 132);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(239, 20);
            this.button5.TabIndex = 14;
            this.button5.Text = "Run the server";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtboxRemovingThreshold);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtboxLevelOfProposedNewTopology);
            this.groupBox2.Controls.Add(this.btnSuggestNewToopology);
            this.groupBox2.Controls.Add(this.btnGraphReport);
            this.groupBox2.Controls.Add(this.txtboxSessionTimeOut);
            this.groupBox2.Controls.Add(this.btnMarkovBuild);
            this.groupBox2.Controls.Add(this.txtboxMinPTS);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.txtboxMinSupp);
            this.groupBox2.Controls.Add(this.txtboxMinConf);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtboxEpsilon);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnLoadAndCleanData);
            this.groupBox2.Location = new System.Drawing.Point(12, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 252);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Offline Algorithms";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(253, 159);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 26);
            this.label11.TabIndex = 20;
            this.label11.Text = "Removing\r\nThreshold";
            // 
            // txtboxRemovingThreshold
            // 
            this.txtboxRemovingThreshold.Location = new System.Drawing.Point(314, 162);
            this.txtboxRemovingThreshold.Name = "txtboxRemovingThreshold";
            this.txtboxRemovingThreshold.Size = new System.Drawing.Size(47, 20);
            this.txtboxRemovingThreshold.TabIndex = 19;
            this.txtboxRemovingThreshold.Text = "5";
            this.txtboxRemovingThreshold.TextChanged += new System.EventHandler(this.numberOnlyTextBox_TextChanged);
            this.txtboxRemovingThreshold.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberOnlyTextBox_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(256, 191);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "menuSize";
            // 
            // txtboxLevelOfProposedNewTopology
            // 
            this.txtboxLevelOfProposedNewTopology.Location = new System.Drawing.Point(314, 188);
            this.txtboxLevelOfProposedNewTopology.Name = "txtboxLevelOfProposedNewTopology";
            this.txtboxLevelOfProposedNewTopology.Size = new System.Drawing.Size(47, 20);
            this.txtboxLevelOfProposedNewTopology.TabIndex = 17;
            this.txtboxLevelOfProposedNewTopology.Text = "3";
            this.txtboxLevelOfProposedNewTopology.TextChanged += new System.EventHandler(this.numberOnlyTextBox_TextChanged);
            this.txtboxLevelOfProposedNewTopology.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberOnlyTextBox_KeyPress);
            // 
            // btnSuggestNewToopology
            // 
            this.btnSuggestNewToopology.Location = new System.Drawing.Point(280, 211);
            this.btnSuggestNewToopology.Margin = new System.Windows.Forms.Padding(0);
            this.btnSuggestNewToopology.Name = "btnSuggestNewToopology";
            this.btnSuggestNewToopology.Size = new System.Drawing.Size(81, 34);
            this.btnSuggestNewToopology.TabIndex = 16;
            this.btnSuggestNewToopology.Text = "propose new topology";
            this.btnSuggestNewToopology.UseVisualStyleBackColor = true;
            this.btnSuggestNewToopology.Click += new System.EventHandler(this.btnSuggestNewToopology_Click);
            // 
            // btnGraphReport
            // 
            this.btnGraphReport.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnGraphReport.Location = new System.Drawing.Point(224, 211);
            this.btnGraphReport.Margin = new System.Windows.Forms.Padding(0);
            this.btnGraphReport.Name = "btnGraphReport";
            this.btnGraphReport.Size = new System.Drawing.Size(56, 34);
            this.btnGraphReport.TabIndex = 15;
            this.btnGraphReport.Text = "graph report";
            this.btnGraphReport.UseVisualStyleBackColor = true;
            this.btnGraphReport.Click += new System.EventHandler(this.btnGraphReport_Click);
            // 
            // txtboxSessionTimeOut
            // 
            this.txtboxSessionTimeOut.Location = new System.Drawing.Point(291, 13);
            this.txtboxSessionTimeOut.Name = "txtboxSessionTimeOut";
            this.txtboxSessionTimeOut.Size = new System.Drawing.Size(58, 20);
            this.txtboxSessionTimeOut.TabIndex = 1;
            this.txtboxSessionTimeOut.Text = "1800";
            this.txtboxSessionTimeOut.TextChanged += new System.EventHandler(this.numberOnlyTextBox_TextChanged);
            this.txtboxSessionTimeOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberOnlyTextBox_KeyPress);
            // 
            // btnMarkovBuild
            // 
            this.btnMarkovBuild.Location = new System.Drawing.Point(18, 154);
            this.btnMarkovBuild.Name = "btnMarkovBuild";
            this.btnMarkovBuild.Size = new System.Drawing.Size(202, 23);
            this.btnMarkovBuild.TabIndex = 9;
            this.btnMarkovBuild.Text = "2 - Build Markov Model";
            this.btnMarkovBuild.UseVisualStyleBackColor = true;
            this.btnMarkovBuild.Click += new System.EventHandler(this.btnMarkovBuild_Click);
            // 
            // txtboxMinPTS
            // 
            this.txtboxMinPTS.Location = new System.Drawing.Point(291, 74);
            this.txtboxMinPTS.Name = "txtboxMinPTS";
            this.txtboxMinPTS.Size = new System.Drawing.Size(58, 20);
            this.txtboxMinPTS.TabIndex = 4;
            this.txtboxMinPTS.Text = "10";
            this.txtboxMinPTS.TextChanged += new System.EventHandler(this.numberOnlyTextBox_TextChanged);
            this.txtboxMinPTS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberOnlyTextBox_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(244, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "minPTS";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(241, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 39);
            this.label8.TabIndex = 13;
            this.label8.Text = "Session\r\ntimeout\r\n(Sec)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSpawnClient);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtboxMarkovDepth);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.txtboxClassification);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtboxClassificationRequest);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(11, 348);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(372, 167);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Online Algorithms";
            // 
            // btnSpawnClient
            // 
            this.btnSpawnClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSpawnClient.Location = new System.Drawing.Point(264, 132);
            this.btnSpawnClient.Name = "btnSpawnClient";
            this.btnSpawnClient.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSpawnClient.Size = new System.Drawing.Size(83, 20);
            this.btnSpawnClient.TabIndex = 1006;
            this.btnSpawnClient.TabStop = false;
            this.btnSpawnClient.Text = "Spawn Client";
            this.btnSpawnClient.UseVisualStyleBackColor = true;
            this.btnSpawnClient.Click += new System.EventHandler(this.btnSpawnClient_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(227, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "MarkovDepth";
            // 
            // txtboxMarkovDepth
            // 
            this.txtboxMarkovDepth.Location = new System.Drawing.Point(303, 105);
            this.txtboxMarkovDepth.Name = "txtboxMarkovDepth";
            this.txtboxMarkovDepth.Size = new System.Drawing.Size(43, 20);
            this.txtboxMarkovDepth.TabIndex = 23;
            this.txtboxMarkovDepth.Text = "3";
            this.txtboxMarkovDepth.TextChanged += new System.EventHandler(this.numberOnlyTextBox_TextChanged);
            this.txtboxMarkovDepth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberOnlyTextBox_KeyPress);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(18, 80);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(202, 45);
            this.button4.TabIndex = 13;
            this.button4.Text = "3 - Classification (K-NN) & Predicate";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtboxClassification
            // 
            this.txtboxClassification.Location = new System.Drawing.Point(303, 81);
            this.txtboxClassification.Name = "txtboxClassification";
            this.txtboxClassification.Size = new System.Drawing.Size(44, 20);
            this.txtboxClassification.TabIndex = 12;
            this.txtboxClassification.Text = "1000";
            this.txtboxClassification.TextChanged += new System.EventHandler(this.numberOnlyTextBox_TextChanged);
            this.txtboxClassification.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberOnlyTextBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(269, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "K";
            // 
            // listboxState
            // 
            this.listboxState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listboxState.Font = new System.Drawing.Font("Tahoma", 8F);
            this.listboxState.FormattingEnabled = true;
            this.listboxState.Location = new System.Drawing.Point(402, 27);
            this.listboxState.Name = "listboxState";
            this.listboxState.Size = new System.Drawing.Size(282, 472);
            this.listboxState.TabIndex = 1000;
            this.listboxState.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(399, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "State";
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(622, 3);
            this.button6.Name = "button6";
            this.button6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button6.Size = new System.Drawing.Size(62, 20);
            this.button6.TabIndex = 15;
            this.button6.TabStop = false;
            this.button6.Text = "about";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(63, 326);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(137, 21);
            this.button7.TabIndex = 1001;
            this.button7.Text = "Save Results";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(206, 326);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(137, 21);
            this.button8.TabIndex = 1002;
            this.button8.Text = "Load Results";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.Location = new System.Drawing.Point(622, 511);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(62, 19);
            this.button9.TabIndex = 1003;
            this.button9.Text = "clear";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // btnZoomInListbox
            // 
            this.btnZoomInListbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomInListbox.Location = new System.Drawing.Point(541, 512);
            this.btnZoomInListbox.Name = "btnZoomInListbox";
            this.btnZoomInListbox.Size = new System.Drawing.Size(38, 19);
            this.btnZoomInListbox.TabIndex = 1004;
            this.btnZoomInListbox.Text = "+";
            this.btnZoomInListbox.UseVisualStyleBackColor = true;
            this.btnZoomInListbox.Click += new System.EventHandler(this.btnZoomInListbox_Click);
            // 
            // btnZoomOuListbox
            // 
            this.btnZoomOuListbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomOuListbox.Location = new System.Drawing.Point(497, 512);
            this.btnZoomOuListbox.Name = "btnZoomOuListbox";
            this.btnZoomOuListbox.Size = new System.Drawing.Size(38, 19);
            this.btnZoomOuListbox.TabIndex = 1005;
            this.btnZoomOuListbox.Text = "-";
            this.btnZoomOuListbox.UseVisualStyleBackColor = true;
            this.btnZoomOuListbox.Click += new System.EventHandler(this.btnZoomOuListbox_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 545);
            this.Controls.Add(this.btnZoomOuListbox);
            this.Controls.Add(this.btnZoomInListbox);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listboxState);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.progressBarDataClean);
            this.Controls.Add(this.lblNotifications);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "WebMining";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
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
        private System.Windows.Forms.TextBox txtboxMinPTS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnMarkovBuild;
        private System.Windows.Forms.TextBox txtboxSessionTimeOut;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtboxMarkovDepth;
        private System.Windows.Forms.Button btnZoomInListbox;
        private System.Windows.Forms.Button btnZoomOuListbox;
        private System.Windows.Forms.Button btnGraphReport;
        private System.Windows.Forms.Button btnSuggestNewToopology;
        private System.Windows.Forms.TextBox txtboxLevelOfProposedNewTopology;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnSpawnClient;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtboxRemovingThreshold;
    }
}

