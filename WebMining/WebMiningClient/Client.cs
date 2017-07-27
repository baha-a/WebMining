using System;
using System.Collections.Generic;
using System.Drawing;
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
            //|gender=True&cluster=2&arpages=['PAGE1' - ,'HOME' - ,'END' - ,'HOME' - 'PAGE1' - 'END' - ,'PAGE1' - 'END' - ,'HOME' - 'END' - ,'HOME' - 'PAGE1' - ]&markovpages=[ . 'PAGE1' - 99.9600798403194 %, .  . 'PAGE2' - 47.9002079002079 %, .  .  . 'PAGE1' - 99.9600798403194 %, .  .  . [END] - 0.0399201596806387 %, .  . 'PAGE3' - 47.9002079002079 %, .  .  . 'END' - 100 %, .  . 'END' - 4.17879417879418 %, .  .  . [END] - 99.8003992015968 %, .  .  . 'HOME' - 0.199600798403194 %, .  . 'PAGE1' - 0.0207900207900208 %, .  .  . 'PAGE2' - 47.9002079002079 %, .  .  . 'PAGE3' - 47.9002079002079 %, .  .  . 'END' - 4.17879417879418 %, .  .  . 'PAGE1' - 0.0207900207900208 %, . [END] - 0.0399201596806387 %]|   

            try
            {
                lblPredicatedGender.Text = bool.Parse(text.Substring("|gender=".Length, 4)) == true ? "MALE" : "FEMALE";
            }
            catch 
            {
                lblPredicatedGender.Text = "FEMALE";
            }
            try
            {
                lblSlelectedCluster.Text = text.Substring("|gender=True&cluster=".Length, 2).Replace("&", "").Replace("=","");
            }
            catch 
            {
                lblSlelectedCluster.Text = "0";
            }
            try
            {
                text = text.Substring((text.IndexOf("&markovpages=[") + "&markovpages=[".Length));
                text = text.Substring(0, text.Length - 2);
            }
            catch { }
            try
            {
                var t = text.Split(',');

                lstboxSuggestedPages.Items.Clear();
                foreach (var l in t)
                    lstboxSuggestedPages.Items.Add(l);
            }
            catch { }
            try
            {
                pictureBox1.BackgroundImage = adsImages[int.Parse(lblSlelectedCluster.Text) % adsImages.Count];
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch { }
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

        private void button2_Click(object sender, EventArgs e)
        {
            adsImages.Clear();
        }

        List<Image> adsImages = new List<Image>();
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog() { Filter = "jpg|*.jpg|all|*.*" };
            if (op.ShowDialog() == DialogResult.OK)
            {
                adsImages.Add(new Bitmap(op.FileName));
            }
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            try
            {
                buildTree(File.ReadAllLines("sitemap.txt"));
            }
            catch { }

            try
            {
                foreach (var p in Directory.GetFiles("images"))
                {
                    adsImages.Add(new Bitmap(p));
                    lblState.Text = "images loaded";
                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cookie = General.getCookie();
        }
    }
}
