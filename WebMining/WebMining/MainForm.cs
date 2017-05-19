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
        public MainForm()
        {
            InitializeComponent();
        }

        public string[] logfile { get; private set; }

        private void btnLoadLogFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Text File|*.txt|All Files|*.*";
            d.Multiselect = true;
            if (d.ShowDialog() == DialogResult.OK)
            {
                logfile = d.FileNames;
                lblLogfiles.Text = logfile.Length + " logfiles selected";
            }
        }


    }
}
