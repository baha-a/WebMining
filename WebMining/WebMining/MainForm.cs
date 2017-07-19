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
        List<User> extractedUsers;

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
            Session.TimeOutSec = long.Parse(txtboxSessionTimeOut.Text);
            if (logfiles == null || logfiles.Count() == 0)
            {
                Print("no input files");
                return;
            }

            Print("loading and cleaing input data from files");
            btnLoadAndCleanData.Enabled = false;
            callback(loadAndCleanData, x => {
                btnLoadAndCleanData.Enabled = true;
                Print("Extracted Users count = " + extractedUsers.Count.ToString("N0"));
                Print("Session Count = " + getAllSessions().Count.ToString("N0"));
                Print("\t\tdone in " + (x / 1000) + " sec");
            });
        }

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
            callback(assicuationRuls, x => { button2.Enabled = true; Print("\t\tdone in " + (x / 1000.0) + " sec"); });
        }

        private void assicuationRuls()
        {
            Print("_______________________");
            Print("Assicuation Rules . . .");
            Print();
            double minsupport = double.Parse(txtboxMinSupp.Text);
            double minconfidence = double.Parse(txtboxMinConf.Text);


            Stopwatch st = Stopwatch.StartNew();

            List<Session> sessions = getAllSessions();

            Output output = new AssociationRules(new Apriori(new SessionInputParser(sessions))
                .GenerateFrequentItemsets(minsupport)).GenerateRules(minconfidence);

            associationRules = output.StrongRules;

            SessionOutputParser outer = new SessionOutputParser();
            try
            {
                Print("session count: " + sessions.Count + "  -  ex: " + outer.Parse(sessions.First().GetTransaction()));
                Print("Distinct session count: " + sessions.Select(x => x.GetTransaction()).Distinct().Count() + "  -  ex: " + outer.Parse(sessions.Select(x => x.GetTransaction()).Distinct().First()));
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

        private List<Session> getAllSessions()
        {
            List<Session> sessions = new List<Session>();
            foreach (var u in extractedUsers)
                sessions.AddRange(u.Sessions);
            return sessions;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (extractedUsers == null)
            {
                MessageBox.Show("no input data");
                return;
            }
            button3.Enabled = false;
            callback(analysisDataset, x => { button3.Enabled = true; Print("\t\tdone in " + (x / 1000.0) + " sec"); });
        }
        private void analysisDataset()
        {
            Print("_______________________");
            Print("Analyzing Dataset . . .");
            Print();



            Dictionary<string, int> countrycode = new Dictionary<string, int>();
            Dictionary<string, int> browser = new Dictionary<string, int>();
            Dictionary<string, int> OS = new Dictionary<string, int>();

            Dictionary<string, Pair<IEnumerable<Session>>> session = new Dictionary<string, Pair<IEnumerable<Session>>>();

            Dictionary<string, int> gender = new Dictionary<string, int>
            {
                { true.ToString(), 0 },
                { false.ToString(), 0 },
                { "unkowen", 0 }
            };

            Session tmpS;
            foreach (User u in extractedUsers)
            {
                if (u.Gender == null)
                    gender["unkowen"]++;
                else
                    gender[u.Gender.ToString()]++;
                if (u.Sessions != null && u.Sessions.Count > 0)
                {
                    tmpS = u.Sessions[0];

                    searchInDic(countrycode, tmpS.CountryCode);
                    searchInDic(browser, tmpS.Browser);
                    searchInDic(OS, tmpS.OperatingSystem);
                }
            }

            Print("Countries:");
            foreach (var c in countrycode)
                Print("\t" + c.Key + " = " + c.Value + " users (" + (int)(c.Value * 100.0 / extractedUsers.Count) + " %)");

            Print();
            Print("Browsers:");
            foreach (var c in browser)
                Print("\t" + c.Key + " = " + c.Value + " users (" + (int)(c.Value * 100.0 / extractedUsers.Count) + " %)");
                                                                                     
            Print();                                                                 
            Print("OperatingSystems:");                                              
            foreach (var c in OS)                                                    
                Print("\t" + c.Key + " = " + c.Value + " users (" + (int)(c.Value * 100.0 / extractedUsers.Count) + " %)");

            Print();
            Print("Gender:");
            foreach (var c in gender)
                Print("\t" + c.Key + " = " + c.Value + " users (" + (int)(c.Value * 100.0 / extractedUsers.Count) + " %)");

            Print("---------------------");
            //double ava = 0;
            //int count = 0;
            //int totalcount = (extractedUsers.Count * (extractedUsers.Count + 1) / 2);
            //Print("user count = " + extractedUsers.Count);
            //Print("total count = " + totalcount);
            //Print();
            //Print();
            //MinMax m = new MinMax();
            //double tmp = 0;
            //List<double> dics = new List<double>();
            //for (int i = 0; i < extractedUsers.Count; i++)
            //{
            //    for (int j = extractedUsers.Count - 1; j > i; j--)
            //    {
            //        tmp = extractedUsers[i].Distance(extractedUsers[j]);
            //        //dics.Add(tmp);
            //        m.SetMinMaxValues(tmp);
            //        ava += tmp;
            //        count++;
            //    }
            //    Processbarhandler((int)((count * 1.0) / totalcount * 100), " wait ");
            //}


            //Processbarhandler(100, " finish ");

            //Print("min   = " + m.MinWeight);
            //Print("max   = " + m.MaxWeight);
            //Print("sum   = " + ava);
            //Print("avarg = " + (ava / totalcount));
            //Print();

            //Print("----------------");
            //Print(extractedUsers[0].ToString());
            //Print("----------------");
            //Print(extractedUsers[1].ToString());
            //Print("----------------");
            //Print(extractedUsers[0].Distance(extractedUsers[1]));
        }
        private static void searchInDic(Dictionary<string, int> dic, string s)
        {
            if (dic.ContainsKey(s) == false)
                dic.Add(s, 0);
            dic[s]++;
        }


        private void Print()
        {
            Print("");
        }
        private void Print(double v)
        {
            Print(v + "");
        }
        private void PrintLines(string[] v)
        {
            foreach (var t in v)
                Print(t);
        }
        private void Print(string v)
        {
            if (DEBUGGING == false)
            {
                listboxState.Items.Add(v);
                listboxState.SelectedIndex = listboxState.Items.Count - 1;
            }
            else
                MessageBox.Show(v);
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
            if (extractedUsers == null)
            {
                MessageBox.Show("no input data");
                return;
            }

            if (clusters == null || associationRules == null)
            {
                Print("do 'Clustering' and 'Association Rule' first!!");
            }
            button4.Enabled = false;
            callback(classification, x => { button4.Enabled = true; Print("\t\tdone in " + (x / 1000.0) + " sec"); });
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
            Print("Suggested Pages: ");
            foreach (var p in result.SuggestedPages)
                Print("\t" + p);
            Print();


            Print();
            Print("Suggested Pages by Markov: ");
            foreach (var p in result.SuggestedPagesByMarkov)
                Print(" " + p);

            Print();
            Print();

            if (result.Cluster != null)
            {
                Print("Clustered to Cluster: " + result.Cluster.ID);
                Print(result.User.Distance(result.Cluster.Center));
                Print();
            }
        }

        private RecommendationResult predicate()
        {
            if (recommender == null)
                recommender = new Recommender(extractedUsers);

            recommender.Clusters = clusters;
            recommender.Rules = associationRules;
            recommender.K = int.Parse(txtboxClassification.Text);
            recommender.MarkovChain = markover;
            return recommender.Recommend(txtboxClassificationRequest.Text);
        }

        void callback(Action<string> core,string param, Action<long> after)
        {
            new Thread(() =>
            {
                Stopwatch st = Stopwatch.StartNew();
                core(param);
                st.Stop();
                if (DEBUGGING == false)
                    after(st.ElapsedMilliseconds);
            })
            { IsBackground = true }.Start();
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

        PipedServer server;
        private void button5_Click(object sender1, EventArgs e1)
        {
            if (server == null)
                server = new PipedServer("webminner", receive);
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

        private void btnMarkovBuild_Click(object sender, EventArgs e)
        {
            if (extractedUsers == null)
            {
                MessageBox.Show("no input data");
                return;
            }
            btnMarkovBuild.Enabled = false;
            callback(bulidMarkov, x => { btnMarkovBuild.Enabled = true; Print("\t\tdone in " + (x / 1000.0) + " sec"); });
        }

        MarkovChain<string> markover;
        private void bulidMarkov()
        {
            Print("_______________________");
            markover = new MarkovChain<string>();
            foreach (var e in extractedUsers)
                foreach (var s in e.Sessions)
                    markover.AddTransaction(s.Requests.Select(x => x.RequstedPage).ToList());
            Print("Markov model has been build");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string msg = "\r\n==============================\r\nthis Demo for our graduation project\r\n\t" +
                "at Damascus University (Syria)\r\n\tat year 2017/2016\r\nBy:\r\n\tBaha'a Alsharif (http://github.com/bhlshrf)\r\n\t" +
                "Ziad Hashem\r\n\tBasel Altoom \r\n\tBakr Damman\r\n==============================\r\n";
            
            PrintLines(msg.Split('\n'));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog() { Filter = "XML|*.xml|Compressed XML|*.gzip|Any|*.*", FileName = "data" };
            if (save.ShowDialog(this) == DialogResult.OK)
            {
                button7.Enabled = false;
                callback(saveData, save.FileName, x => { button7.Enabled = true; Print("\t\tdone in " + (x / 1000.0) + " sec"); });
            }
        }
        private void saveData(string path)
        {
            Print("saving . . .");
            try
            {
                var data = new IOData()
                {
                    Rules = associationRules == null ? new List<Rule>() : new List<Rule>(associationRules),
                    Clusters = clusters == null ? new List<SerializableClusterOfUsers>() : SerializableClusterOfUsers.Convert(clusters),
                    Users = extractedUsers == null ? new List<User>() : extractedUsers,
                    itemKeys = Session._items == null ? new List<string>() : Session._items.Keys.ToList(),
                    itemValues = Session._items == null ? new List<char>() : Session._items.Values.ToList(),
                };

                if (IsCompressed(path))
                    IOHandler.WriteToXmlGZIPFile<IOData>(path, data);
                else
                    IOHandler.WriteToXmlFile<IOData>(path, data);

                Print("done");
            }
            catch (Exception ex)
            {
                Print("Error : ");
                Print(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "XML|*.xml|Compressed XML|*.gzip|Any|*.*";
            if (open.ShowDialog(this) == DialogResult.OK)
            {
                button8.Enabled = false;
                callback(loadData, open.FileName, x => { button8.Enabled = true; Print("\t\tdone in " + (x / 1000.0) + " sec"); });
            }
        }

        private void loadData(string path)
        {
            try
            {
                Print("loading . . .");
                IOData d =
                    IsCompressed(path) ?
                    IOHandler.ReadFromXmlGZIPFile<IOData>(path) :
                    IOHandler.ReadFromXmlFile<IOData>(path);

                associationRules = d.Rules;
                clusters = SerializableClusterOfUsers.Convert(d.Clusters);
                extractedUsers = d.Users;

                foreach (var u in extractedUsers)
                    foreach (var s in u.Sessions)
                    {
                        s.User = u;
                        foreach (var r in s.Requests)
                            r.Session = s;
                    }

                var v = new Dictionary<string, char>();
                for (int i = 0; i < d.itemKeys.Count; i++)

                    v.Add(d.itemKeys[i], d.itemValues[i]);
                Session._items = v;
                Session.BuildReverseItemsOfTransactions();
                Print("done");

                Print();
                Print("users count = " + extractedUsers.Count);
                Print("clusters count = " + clusters.Count());
                Print("rules count = " + associationRules.Count());

                bulidMarkov();
            }
            catch (Exception ex)
            {
                Print("Error : ");
                Print(ex.Message);
            }
        }

        private static bool IsCompressed(string path)
        {
            return path.ToLower().EndsWith(".gzip");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            listboxState.Items.Clear();
        }
    }
}