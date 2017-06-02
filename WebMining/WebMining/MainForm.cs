using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebMining
{
    public partial class MainForm : Form
    {
        public List<string> logfiles { get; private set; }

        public MainForm()
        {
            InitializeComponent();

            logfiles = new List<string>();
        }


        private void btnLoadLogFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog() { Multiselect = true, Filter = "Text File|*.txt|All Files|*.*" };
            if (d.ShowDialog() == DialogResult.OK)
                logfiles = new List<string>(d.FileNames);
            lblLogfiles.Text = logfiles.Count+ " logfiles selected";
        }

        private void btnLoadAndCleanData_Click(object sender, EventArgs e)
        {
            new Thread(loadAndCleanData) { IsBackground = true }.Start();
        }

        private void loadAndCleanData()
        {
            btnLoadAndCleanData.Enabled = false;
            Stopwatch st = new Stopwatch();
            st.Start();

            Action<int, string> p = (x, y) =>
            {
                //Console.WriteLine(x + " %  - " + y);
                lblNotifications.Text = x + " %  - " + y;
                progressBarDataClean.Value = x;
            };

            new Engine().setNotifyer(p).ProcessAll(logfiles).getExtractedUsers();//.ForEach(t => Console.WriteLine(t));
            st.Stop();
            Console.WriteLine("done in " + (st.ElapsedMilliseconds / 1000) + " sec");

            btnLoadAndCleanData.Enabled = true;
            freeMemory();
        }

        private void freeMemory()
        {
            GC.Collect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbscanAlgorithm.TEST(x => Console.WriteLine(x));
        }
    }
}
