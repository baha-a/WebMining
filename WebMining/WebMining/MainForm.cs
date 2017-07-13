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
        private readonly bool DEBUGGING = 1 == 0;

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
            Print(lblLogfiles.Text = logfiles.Count + " files selected");
        }

        private void btnLoadAndCleanData_Click(object sender, EventArgs e)
        {
            if (logfiles == null || logfiles.Count() == 0)
            {
                Print("no input files");
                return;
            }
            Print("loading and cleaing input data from files");
            btnLoadAndCleanData.Enabled = false;
            callback(loadAndCleanData, x => {
                btnLoadAndCleanData.Enabled = true;
                Print("Extracted Users count = " + extractedUsers.Count);
                Print("\t\tdone in " + (x / 1000) + " sec");
            });
        }

        List<User> extractedUsers;
        private void loadAndCleanData()
        {
            Print();
            extractedUsers = new Engine()
                .setNotifyer(Processbarhandler)
                .ProcessAll(logfiles)
                .getExtractedUsers();

            freeMemory();
        }

        private void freeMemory()
        {
            GC.Collect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (extractedUsers == null)
            {
                Print("no input data");
                return;
            }
            button1.Enabled = false;
            callback(clustering, x => { button1.Enabled = true; Print("\t\tdone in " + (x / 1000) + " sec"); });
        }

        IEnumerable<Cluster> clusters;
        private void clustering()
        {
            Print("_______________________");
            Print("Clustring . . .");
            Print();
            clusters = new DbscanAlgorithm(double.Parse(txtboxEpsilon.Text), int.Parse(txtboxMinPTS.Text))
                .setNotifyer(Processbarhandler)
                .Clustering(extractedUsers, Cluster.AvarageUser);

            Print("Count = " + clusters.Count());
            Print();
            foreach (var c in clusters)
                Print("ID: " + c.ID + "   -  center:" + c.Center.Distance(extractedUsers[0]));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (extractedUsers == null)
            {
                Print("no input data");
                return;
            }
            button2.Enabled = false;
            callback(assicuationRuls, x => { button2.Enabled = true; Print("\t\tdone in " + (x / 1000) + " sec"); });
        }

        private void assicuationRuls()
        {
            Print("_______________________");
            Print("Assicuation Rules . . .");
            Print();
            double minsupport = double.Parse(txtboxMinSupp.Text); // 1.0000000000000001
            double minconfidence = double.Parse(txtboxMinConf.Text);

            Stopwatch st = Stopwatch.StartNew();

            List<Session> sessions = new List<Session>();
            foreach (var u in extractedUsers)
                sessions.AddRange(u.Sessions);

            Output output = new AssociationRules(new Apriori(new SessionInputParser(sessions)).GenerateFrequentItemsets(minsupport))
                .GenerateRules(minconfidence); //.parse(new SessionOutputParser());

            associationRules = output.StrongRules;

            SessionOutputParser outer = new SessionOutputParser();
            try
            {
                Print("session count: " + sessions.Count + "  -  ex: " + outer.Parse(sessions.First().GetTransaction()));
                Print();
                Print("FrequentItems: " + output.FrequentItems.Count);
                Print("FrequentItems first: " + outer.Parse(output.FrequentItems.First().Name));
                Print();
                Print("ClosedItemSets: " + output.ClosedItemSets.Count);
                Print("ClosedItemSets first: " + outer.Parse(output.ClosedItemSets.First().Key));
                if (output.ClosedItemSets.First().Value.Count > 0)
                    Print("ClosedItemSets first first: " + outer.Parse(output.ClosedItemSets.First().Value.First().Key));
                Print();
                Print("MaximalItemSets: " + output.MaximalItemSets.Count);
                Print("MaximalItemSets first: " + outer.Parse(output.MaximalItemSets.First().ToString()));
                Print();
                Print("StrongRules: " + output.StrongRules.Count);
                Print("StrongRules first: " + outer.Parse(output.StrongRules.First().X) + "  ===>  " + outer.Parse(output.StrongRules.First().Y));
                Print("StrongRules midel: " + outer.Parse(output.StrongRules.ElementAt(output.StrongRules.Count / 2).X)
                    + "  ===>  " + outer.Parse(output.StrongRules.ElementAt(output.StrongRules.Count / 2).Y));
                Print("StrongRules last: " + outer.Parse(output.StrongRules.Last().X) + "  ===>  " + outer.Parse(output.StrongRules.Last().Y));
            }
            catch
            {
                Print("erorr because output has null values");
            }

            Print();
            Print("ElapsedMilliseconds: " + st.ElapsedMilliseconds);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (extractedUsers == null)
            {
                MessageBox.Show("no input data");
                return;
            }
            button3.Enabled = false;
            callback(analysisDataset, x => { button3.Enabled = true; Print("\t\tdone in " + (x / 1000) + " sec"); });
        }
        private void analysisDataset()
        {
            Print("_______________________");
            Print("Analyzing Dataset . . .");
            Print();
            double ava = 0;
            int count = 0;
            int totalcount = (extractedUsers.Count * (extractedUsers.Count + 1) / 2);
            Print("user count = " + extractedUsers.Count);
            Print("total count = " + totalcount);
            Print();
            Print();
            MinMax m = new MinMax();
            double tmp = 0;
            List<double> dics = new List<double>();
            for (int i = 0; i < extractedUsers.Count; i++)
            {
                for (int j = extractedUsers.Count - 1; j > i; j--)
                {
                    tmp = extractedUsers[i].Distance(extractedUsers[j]);
                    //dics.Add(tmp);
                    m.SetMinMaxValues(tmp);
                    ava += tmp;
                    count++;
                }
                Processbarhandler((int)((count * 1.0) / totalcount * 100), " wait ");
            }

            //dics.Sort();
            //System.IO.File.WriteAllLines("c:\\dics.txt", dics.Select(x => x + ""));

            Processbarhandler(100, " finish ");

            Print("min   = " + m.MinWeight);
            Print("max   = " + m.MaxWeight);
            Print("sum   = " + ava);
            Print("avarg = " + (ava / totalcount));
            Print();

            Print("----------------");
            Print(extractedUsers[0].ToString());
            Print("----------------");
            Print(extractedUsers[1].ToString());
            Print("----------------");
            Print(extractedUsers[0].Distance(extractedUsers[1]));
        }

        private void Print()
        {
            Print("");
        }
        private void Print(double v)
        {
            Print(v + "");
        }
        private void Print(string v)
        {
            if (DEBUGGING == false)
            {
                listboxState.Items.Add(v);
                listboxState.SelectedIndex = listboxState.Items.Count - 1;
            }
        }
        private void PrintInSameLine(string v)
        {
            listboxState.Items[listboxState.Items.Count - 1] = v;
        }

        private void Processbarhandler(int x, string y)
        {
            if (DEBUGGING == false)
            {
                lblNotifications.Text = (progressBarDataClean.Value = x) + " %  - " + y;
                PrintInSameLine(lblNotifications.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (clusters == null || associationRules == null)
            {
                Print("do 'Clustering' and 'Association Rule' first");
                return;
            }
            button4.Enabled = false;
            callback(classification, x => { button4.Enabled = true; Print("\t\tdone in " + x + " milisec"); });
        }

        Recommender recommender;
        private IList<Rule> associationRules;

        private void classification()
        {
            Print("_______________________");
            var result = predicate();

            string gender = "UNKNOWEN";
            if (result.Gender == true)
                gender = "MALE";
            else if (result.Gender == false)
                gender = "FEMALE";

            Print("Predicated gender is : ");
            Print(gender);

            Print();
            Print("Suggested Pages is: ");
            foreach (var p in result.SuggestedPages)
                Print("\t" + p);
            Print();

            Print("Clustered to Cluster: " + result.Cluster.ID);
            Print(result.User.Distance(result.Cluster.Center));
            Print();
        }

        private RecommendationResult predicate()
        {
            if (recommender == null)
                recommender = new Recommender(extractedUsers);

            recommender.Clusters = clusters;
            recommender.Rules = associationRules;
            recommender.K = int.Parse(txtboxClassification.Text);
            return recommender.Recommend(txtboxClassificationRequest.Text);
        }

        void callback(Action core, Action<long> after)
        {
            new Thread(() =>
            {
                Stopwatch st = Stopwatch.StartNew();
                core();
                st.Stop();
                if (DEBUGGING == false)
                after(st.ElapsedMilliseconds);
            })
            { IsBackground = true }.Start();
        }

        private void button5_Click(object sender1, EventArgs e1)
        {
            PipedServer server = new PipedServer("webminner", receive);
            Print("server is running . . . ");
        }
        string receive(string m)
        {
            Print("client :" + m);
            txtboxClassificationRequest.Text = m;
            var result = predicate();
            return "ok";
        }

        private void numberOnlyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                e.Handled = true;
        }

        private void numberOnlyTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(((TextBox)sender).Text) > 0)
                    return;
            }
            catch { }
            Print("only number (bigger than zero) in textbox");
        }
    }
}