using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

        private void button1_Click(object sender, EventArgs e)
        {
            new Engine().ProcessAll(logfiles).getExtractedUsers().ForEach(t => Console.WriteLine(t));
        }
    }
}
