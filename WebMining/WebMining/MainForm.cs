using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebMining
{
    public partial class MainForm : Form
    {
        public List<string> logfile { get; private set; }

        public MainForm()
        {
            InitializeComponent();

            logfile = new List<string>();
        }


        private void btnLoadLogFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog() { Multiselect = true, Filter = "Text File|*.txt|All Files|*.*" };
            if (d.ShowDialog() == DialogResult.OK)
                logfile = new List<string>(d.FileNames);
            lblLogfiles.Text = logfile.Count+ " logfiles selected";
        }


    }
}
