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
            DbscanAlgorithm.TEST(x => Console.WriteLine(x));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Thread(assicuationRuls) { IsBackground = true }.Start();
        }

        private void assicuationRuls()
        {
            double minsupport = double.Parse(textBox1.Text); // 1.0000000000000001
            double minconfidence = double.Parse(textBox2.Text);

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
    }
}

