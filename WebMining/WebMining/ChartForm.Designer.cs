namespace WebMining
{
    partial class ChartForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pagesChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.browserChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.osChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.genderChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.countryChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pagesChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.browserChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.osChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.genderChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryChart)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.browserChart);
            this.panel1.Controls.Add(this.osChart);
            this.panel1.Controls.Add(this.genderChart);
            this.panel1.Controls.Add(this.countryChart);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.pagesChart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 558);
            this.panel1.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label5.Location = new System.Drawing.Point(8, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 19);
            this.label5.TabIndex = 9;
            this.label5.Text = "Pages";
            // 
            // pagesChart
            // 
            this.pagesChart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea5.Name = "ChartArea1";
            this.pagesChart.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.pagesChart.Legends.Add(legend5);
            this.pagesChart.Location = new System.Drawing.Point(12, 12);
            this.pagesChart.Name = "pagesChart";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.pagesChart.Series.Add(series5);
            this.pagesChart.Size = new System.Drawing.Size(762, 377);
            this.pagesChart.TabIndex = 4;
            title3.Name = "Title1";
            this.pagesChart.Titles.Add(title3);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label4.Location = new System.Drawing.Point(397, 717);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 19);
            this.label4.TabIndex = 17;
            this.label4.Text = "OS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label3.Location = new System.Drawing.Point(12, 717);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 19);
            this.label3.TabIndex = 16;
            this.label3.Text = "Browsers";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label2.Location = new System.Drawing.Point(397, 392);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 19);
            this.label2.TabIndex = 15;
            this.label2.Text = "Registered and Gender";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label1.Location = new System.Drawing.Point(12, 396);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 19);
            this.label1.TabIndex = 14;
            this.label1.Text = "Countries";
            // 
            // browserChart
            // 
            chartArea1.Name = "ChartArea1";
            this.browserChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.browserChart.Legends.Add(legend1);
            this.browserChart.Location = new System.Drawing.Point(12, 720);
            this.browserChart.Name = "browserChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.browserChart.Series.Add(series1);
            this.browserChart.Size = new System.Drawing.Size(378, 314);
            this.browserChart.TabIndex = 12;
            this.browserChart.Text = "Gender";
            // 
            // osChart
            // 
            chartArea2.Name = "ChartArea1";
            this.osChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.osChart.Legends.Add(legend2);
            this.osChart.Location = new System.Drawing.Point(396, 720);
            this.osChart.Name = "osChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.osChart.Series.Add(series2);
            this.osChart.Size = new System.Drawing.Size(378, 314);
            this.osChart.TabIndex = 13;
            title1.Name = "Title1";
            this.osChart.Titles.Add(title1);
            // 
            // genderChart
            // 
            chartArea3.Name = "ChartArea1";
            this.genderChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.genderChart.Legends.Add(legend3);
            this.genderChart.Location = new System.Drawing.Point(396, 400);
            this.genderChart.Name = "genderChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.genderChart.Series.Add(series3);
            this.genderChart.Size = new System.Drawing.Size(378, 314);
            this.genderChart.TabIndex = 10;
            this.genderChart.Text = "Gender";
            // 
            // countryChart
            // 
            chartArea4.Name = "ChartArea1";
            this.countryChart.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.countryChart.Legends.Add(legend4);
            this.countryChart.Location = new System.Drawing.Point(12, 400);
            this.countryChart.Name = "countryChart";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.countryChart.Series.Add(series4);
            this.countryChart.Size = new System.Drawing.Size(378, 314);
            this.countryChart.TabIndex = 11;
            title2.Name = "Title1";
            this.countryChart.Titles.Add(title2);
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 558);
            this.Controls.Add(this.panel1);
            this.Name = "ChartForm";
            this.Text = "ChartForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pagesChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.browserChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.osChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.genderChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart browserChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart osChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart genderChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart countryChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart pagesChart;
    }
}