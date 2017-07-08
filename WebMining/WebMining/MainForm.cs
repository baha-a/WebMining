using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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

        List<User> extractedUsers;
        private void loadAndCleanData()
        {
            btnLoadAndCleanData.Enabled = false;
            Stopwatch st = Stopwatch.StartNew();

            Action<int, string> p = (x, y) =>
            {
                lblNotifications.Text = x + " %  - " + y;
                progressBarDataClean.Value = x;
            };

            extractedUsers = new Engine().setNotifyer(p).ProcessAll(logfiles).getExtractedUsers();//.ForEach(t => Console.WriteLine(t));

            Console.WriteLine("done in " + (st.ElapsedMilliseconds / 1000) + " sec");

            btnLoadAndCleanData.Enabled = true;
            freeMemory();
            st.Stop();
        }

        private void freeMemory()
        {
            GC.Collect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch st = Stopwatch.StartNew();
            var clusters = new DbscanAlgorithm(double.Parse(txtboxEpsilon.Text), 1).Clustering(extractedUsers.Take(100));
            st.Stop();

            Console.WriteLine("Time = " + (st.ElapsedMilliseconds / 1000) + " sec");
            Console.WriteLine("Count = " + clusters.Count());

            Console.WriteLine();
            foreach (var c in clusters)
                Console.WriteLine(c.Center.Distance(extractedUsers[0]) + "");

            //DbscanAlgorithm.TEST(x => Console.WriteLine(x));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Thread(assicuationRuls) { IsBackground = true }.Start();
        }

        private void assicuationRuls()
        {
            double minsupport = double.Parse(txtboxMinSupp.Text); // 1.0000000000000001
            double minconfidence = double.Parse(txtboxMinConf.Text);

            Stopwatch st = Stopwatch.StartNew();

            List<Session> sessions = new List<Session>();
            foreach (var u in extractedUsers)
                sessions.AddRange(u.Sessions);

            Output output = new AssociationRules(new Apriori(new SessionInputParser(sessions)).GenerateFrequentItemsets(minsupport)).GenerateRules(minconfidence);

            Console.WriteLine();
            Console.WriteLine("session: " + sessions.Count + "  -  " + sessions.First().GetTransaction());
            Console.WriteLine("FrequentItems: " + output.FrequentItems.Count);
            Console.WriteLine("ClosedItemSets: " + output.ClosedItemSets.Count);
            Console.WriteLine("MaximalItemSets: " + output.MaximalItemSets.Count);
            Console.WriteLine("StrongRules: " + output.StrongRules.Count);

            Console.WriteLine(st.ElapsedMilliseconds);

            //foreach (var s in sessions)
            //{
            //    Console.WriteLine(s.GetTransaction());
            //    Console.ReadKey();
            //}

            //File.WriteAllLines("c:\\apiroir.txt", sessions.Select(x => x.GetTransaction()));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Statical();
        }
        private void Statical()
        {
            double ava = 0;
            int count = 0;
            int totalcount = (extractedUsers.Count * (extractedUsers.Count + 1) / 2);
            Console.WriteLine("user count = " + extractedUsers.Count);
            Console.WriteLine("total count = " + totalcount);
            Console.WriteLine();
            MinMax m = new MinMax();
            double tmp = 0;
            for (int i = 0; i < extractedUsers.Count; i++)
            {
                for (int j = extractedUsers.Count - 1; j > i; j--)
                {
                    tmp = extractedUsers[i].Distance(extractedUsers[j]);
                    m.SetMinMaxValues(tmp);
                    ava += tmp;
                    count++;
                }
                Console.Title = (((count * 1.0) / totalcount * 100) + " %");
            }

            Console.WriteLine("min   = " + m.MinWeight);
            Console.WriteLine("max   = " + m.MaxWeight);
            Console.WriteLine("sum   = " + ava);
            Console.WriteLine("avarg = " + (ava / totalcount));
            Console.WriteLine();

            Console.WriteLine("----------------");
            Console.WriteLine(extractedUsers[0].ToString());
            Console.WriteLine("----------------");
            Console.WriteLine(extractedUsers[1].ToString());
            Console.WriteLine("----------------");
            Console.WriteLine(extractedUsers[0].Distance(extractedUsers[1]));
        }
    }
}

