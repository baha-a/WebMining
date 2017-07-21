using System;
using System.Globalization;
using System.IO;
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
            cookie = General.getCookie();
        }

        string cookie;

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

        private void btnRequest_Click(object sender, EventArgs e)
        {
            new System.Threading.Thread(() =>
            {
                try
                {
                    using (Client client = new Client())
                    {
                        lblState.Text = client.command(gethttprequest());
                        lstboxRequestedPages.Items.Add(lastone);

                        tryhandleResponse(lblState.Text);
                    }
                }
                catch (Exception ex)
                {
                    lblState.Text = ex.Message;
                }
            })
            {
                IsBackground = true
            }.Start();
        }

        private void tryhandleResponse(string text)
        {
            
        }

        private string gethttprequest()
        {
            //0000014602 ofssobxxxmpdu1sr NONE 72.3.217.228 BM 'Opera' 'Mac' 01:47:53 21-12-2017 'PAGE1' 'PAGE2'
            return
                General.getID() + " " +
                cookie + " " +
                General.getGender(cboxProfile.SelectedItem + "") + " " +
                General.getIPAndCountryCode(cboxCountry.SelectedItem + "") + " '" +
                cboxBrowser.SelectedItem + "' '" +
                cboxOS.SelectedItem + "' " +
                txtboxTime.Text + " " +
                txtboxDate.Text +
                getRequstedPage();
        }

        string lastone = "'START'";
        private string getRequstedPage()
        {
            string r = treeSiteMap.SelectedNode.Text;
            string tmp = " " + r + " " + lastone + "";
            lastone = r;
            return tmp;
        }

        private void btnLoadMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog() { Filter = "Text|*.txt|All Files|*.*" };
            if(op.ShowDialog(this) == DialogResult.OK)
            {
                buildTree(File.ReadAllLines(op.FileName));
            }
        }

        int index = 0;
        private void buildTree(string[] v, int lvl = 0, TreeNode node = null)
        {
            if (lvl == 0)
            {
                index = 0;
                treeSiteMap.Nodes.Clear();
            }
            TreeNode n;
            for (; index < v.Length && lvl < v[index].Length; index++)
            {
                if (v[index][lvl] != '>' && (lvl == 0 || (lvl - 1 >= 0 && v[index][lvl - 1] == '>')))
                {
                    if (lvl == 0)
                        n = treeSiteMap.Nodes.Add(v[index].Substring(lvl));
                    else
                        n = node.Nodes.Add(v[index].Substring(lvl));

                    index++;
                    buildTree(v, lvl + 1, n);
                    index--;
                }
                else
                    return;
            }
            treeSiteMap.ExpandAll();
        }

        private void btnNewSession_Click(object sender, EventArgs e)
        {
            lstboxRequestedPages.Items.Add("__________________________");
            txtboxDate.Text = DateTime.ParseExact(txtboxDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).AddDays(1).ToString("dd-MM-yyyy");
            lastone = "'START'";
        }
    }
}
