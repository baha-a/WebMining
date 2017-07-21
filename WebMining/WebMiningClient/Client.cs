using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebMiningClient
{
    public partial class ClientForm : Form
    {
        Timer timer1;
        public ClientForm()
        {
            InitializeComponent();

            txtboxTime.Text = DateTime.Now.ToString("hh:mm:ss");
            txtboxTime.GotFocus += TxtboxTime_GotFocus;
            txtboxTime.LostFocus += TxtboxTime_LostFocus;
            timer1 = new Timer() { Interval = 1000 };
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        bool flag = false;
        private void TxtboxTime_LostFocus(object sender, EventArgs e)
        {
            flag = false;
        }

        private void TxtboxTime_GotFocus(object sender, EventArgs e)
        {
            flag = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag == false)
                txtboxTime.Text = DateTime.ParseExact(txtboxTime.Text, "hh:mm:ss", CultureInfo.InvariantCulture).AddSeconds(1).ToString("hh:mm:ss");
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }
    }
}
