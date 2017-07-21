using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using Microsoft.Msagl.Drawing;

namespace WebMining
{
    public partial class MainForm : Form
    {
        private readonly bool DEBUGGING = 1 == 0;

        List<User> extractedUsers;

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
            callback(clustering, toggleButtonEnable(button1));
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
                Print("ID = " + c.ID + "   count = " + c.Dataset.Count() + "   -  center = " + c.Center.Distance(extractedUsers.First()));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (extractedUsers == null)
            {
                Print("no input data");
                return;
            }
            callback(assicuationRuls, toggleButtonEnable(button2));
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
                Print();
                Print("FrequentItems count : " + output.FrequentItems.Count);
                foreach (var f in output.FrequentItems)
                    Print("\t" + outer.Parse(f.Name));

                Print();
                Print("ClosedItemSets count: " + output.ClosedItemSets.Count);
                Print("ClosedItemSets: ");
                foreach (var c in output.ClosedItemSets)
                {
                    Print("\t" + outer.Parse(c.Key));
                    foreach (var cc in c.Value)
                        Print("\t\t" + outer.Parse(cc.Key));
                }

                Print();
                Print("MaximalItemSets count: " + output.MaximalItemSets.Count);
                Print("MaximalItemSets: ");
                foreach (var f in output.MaximalItemSets)
                    Print("\t" + outer.Parse(f));

                Print();
                Print("StrongRules count: " + output.StrongRules.Count);
                Print("StrongRules: ");
                foreach (var s in output.StrongRules)
                    Print("\t" + outer.Parse(s.X) + "   ===>   " + outer.Parse(s.Y));
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
                Print("no input data");
                return;
            }
            callback(analysisDataset, toggleButtonEnable(button3));
        }
        private void analysisDataset()
        {
            Print("_______________________");
            Print("Analyzing Dataset . . .");
            Print();



            int sessionCount = 0;
            long totalSpentTime = 0;
            long totalPageHits = 0;
            Dictionary<string, Pair<long>> pages = new Dictionary<string, Pair<long>>();

            Dictionary<string, int> countrycode = new Dictionary<string, int>();
            Dictionary<string, int> browser = new Dictionary<string, int>();
            Dictionary<string, int> OS = new Dictionary<string, int>();


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

                    sessionCount += u.Sessions.Count;
                    totalSpentTime += (long)u.Sessions.Sum(x => x.Duration.TotalSeconds);
                    foreach (var s in u.Sessions)
                    {
                        //sessionCount++;
                        //totalSpentTime += (long) s.Duration.TotalSeconds;

                        foreach (var r in s.Requests)
                        {
                            totalPageHits++;
                            if (pages.ContainsKey(r.RequstedPage) == false)
                                pages.Add(r.RequstedPage, new Pair<long>(0, 0));

                            pages[r.RequstedPage].Weight++;
                            pages[r.RequstedPage].Value += r.SpentTime;
                        }
                    }
                }
            }

            Series genderChart = new Series() { ChartType = SeriesChartType.Pie, IsValueShownAsLabel = true };
            Series countryChart = new Series() { ChartType = SeriesChartType.Pie, IsValueShownAsLabel = true };
            Series browsersChart = new Series() { ChartType = SeriesChartType.Pie, IsValueShownAsLabel = true };
            Series osChart = new Series() { ChartType = SeriesChartType.Pie, IsValueShownAsLabel = true };

            Series pagesChart = new Series() { ChartType = SeriesChartType.Column, Color = System.Drawing.Color.SeaGreen, Name = "Page Hits" };
            Series pagesChart2 = new Series() { ChartType = SeriesChartType.Column, Color = System.Drawing.Color.Tomato, Name = "Spent Time" };

            Print("Countries:");
            foreach (var c in countrycode)
            {
                countryChart.Points.AddXY(c.Key, (int)(c.Value * 100.0 / extractedUsers.Count));
                Print("\t" + c.Key + " = " + c.Value + " users (" + (int)(c.Value * 100.0 / extractedUsers.Count) + " %)");
            }

            Print();
            Print("Browsers:");
            foreach (var c in browser)
            {
                browsersChart.Points.AddXY(c.Key, (int)(c.Value * 100.0 / extractedUsers.Count));
                Print("\t" + c.Key + " = " + c.Value + " users (" + (int)(c.Value * 100.0 / extractedUsers.Count) + " %)");
            }

            Print();
            Print("OperatingSystems:");
            foreach (var c in OS)
            {
                osChart.Points.AddXY(c.Key, (int)(c.Value * 100.0 / extractedUsers.Count));
                Print("\t" + c.Key + " = " + c.Value + " users (" + (int)(c.Value * 100.0 / extractedUsers.Count) + " %)");
            }

            Print();
            Print("Gender:");
            foreach (var c in gender)
            {
                genderChart.Points.AddXY(getGender(c.Key), (int)(c.Value * 100.0 / extractedUsers.Count));
                Print("\t" + getGender(c.Key) + " = " + c.Value + " users (" + (int)(c.Value * 100.0 / extractedUsers.Count) + " %)");
            }

            Print();
            Print("Pages:");
            var orderPages = pages.OrderByDescending(x => ranking(totalSpentTime, totalPageHits, x));
            foreach (var c in orderPages)
            {
                Print("\t" + c.Key + "\t-\thits = " + c.Value.Weight.ToString("N0") + " (" + (int)(c.Value.Weight * 100.0 / totalPageHits) +
                    " %) ,\tspentTime = " + c.Value.Value.ToString("N0") + " (" + (int)(c.Value.Value * 100.0 / totalSpentTime) + " %)" +
                    "\tRank = " + ranking(totalSpentTime, totalPageHits, c) + " points");

                pagesChart.Points.AddXY(c.Key, (int)(c.Value.Weight * 100.0 / totalPageHits));
                pagesChart2.Points.AddXY(c.Key, (int)(c.Value.Value * 100.0 / totalSpentTime));
            }

            Print("---------------------");

            Print();
            Print("Total Spent time\t= " + totalSpentTime.ToString("N0") + " sec");
            Print("Total Pages Hits\t= " + totalPageHits.ToString("N0") + " hits");
            Print("Total Session count\t= " + sessionCount.ToString("N0") + " session");

            Print("Unique Visitors\t= " + extractedUsers.Count.ToString("N0") + " visitors");

            Print("Avarage Spent time = " + (totalSpentTime / extractedUsers.Count).ToString("N0") + "  sec/visitor");
            Print("Avarage Pages Hits = " + (totalPageHits / extractedUsers.Count).ToString("N0") + "  hits/visitor");

            Print("Avarage Session count\t= " + (sessionCount * 1.0 / extractedUsers.Count));


            Print("---------------------");
            chartForm = new ChartForm().SetChart(countryChart, browsersChart, osChart, genderChart, pagesChart, pagesChart2);





            Print();
            Print("orginal Topology:");
            Graph graphOrginal = new Graph();
            foreach (var p in buildOrginalTopology(graphOrginal))
                Print(p);


            Print();
            Graph suggestedOrginal = new Graph();
            List<string> suggestedWebsiteTopology = new List<string>(new string[] { "NEW HOME" });
            suggestedWebsiteTopology.AddRange(buildSuggestedTopology(suggestedOrginal, "NEW HOME", orderPages.Select(x => x.Key), 3 , "", int.Parse(txtboxLevelOfProposedNewTopology.Text)));
            Print("Suggested Topology:");
            foreach (var p in suggestedWebsiteTopology)
                Print(p);

            graphForm = new GraphForm().AddGraphs(suggestedOrginal, graphOrginal);

            //MinMax m = new MinMax(); int count = 0, totalcount = (extractedUsers.Count * (extractedUsers.Count + 1) / 2); double tmp = 0, ava = 0; for (int i = 0; i < extractedUsers.Count; i++) { for (int j = extractedUsers.Count - 1; j > i; j--) { m.SetMinMaxValues(tmp = extractedUsers[i].Distance(extractedUsers[j])); ava += tmp; count++; } Processbarhandler((int)((count * 1.0) / totalcount * 100), " wait "); } Print("min   = " + m.MinWeight); Print("max   = " + m.MaxWeight); Print("sum   = " + ava); Print("avarg = " + (ava / totalcount));
        }

        ChartForm chartForm = null;
        private void btnGraphReport_Click(object sender, EventArgs e)
        {
            if (chartForm == null)
                Print("no data");
            else
                Invoke((Action)delegate
                {
                    chartForm.CloneMe().Show(this);
                });
        }
        GraphForm graphForm = null;
        private void btnSuggestNewToopology_Click(object sender, EventArgs e)
        {
            if (graphForm == null)
                Print("no data");
            else
                Invoke((Action)delegate
                {
                    graphForm.CloneMe().Show(this);
                });
        }

        private IList<string> buildOrginalTopology(Graph g)
        {
            var r = new List<MarkovNode<string>>();
            if (markover == null)
                return new List<string>();
            markover.setStartNodeName("[Start]");
            markover.setEndNodeName("[END]");
            r.Add(markover.getStartNode());
            return searchForNode(g, markover.getStartNode(), r, " . ");
        }
        private List<string> searchForNode(Graph g,MarkovNode<string> start, List<MarkovNode<string>> visited, string prefix2 = "", string prefix = "")
        {
            List<string> res = new List<string>();
            foreach (var f in start.GetNexts())
            {
                if (string.IsNullOrEmpty(f.Value))
                    continue;
                if (visited.Contains(f))
                    continue;

                if(start.State != MarkovState.Start)
                AddToGraph(g, start.Value, f.Value);

                res.Add(prefix + f.Value);

                visited.Add(f);
                res.AddRange(searchForNode(g, f, new List<MarkovNode<string>>(visited), prefix2, prefix + prefix2));
                visited.Remove(f);
            }
            return res;
        }

        private IList<string> buildSuggestedTopology(Graph g,string last, IEnumerable<string> orderPages , int level = 3, string prefix = "", int linkCountEachLevel = 3)
        {
            var res = new List<string>();
            int order = 1;
            int locallinkCountEachLevel = linkCountEachLevel;
            if (level > 0)
                foreach (var p in orderPages)
                {
                    if (string.IsNullOrEmpty(p))
                        continue;
                    if (locallinkCountEachLevel-- == 0)
                        break;
                    res.Add(prefix + order + " - " + p);

                    AddToGraph(g, last, p);

                    if (markover != null)
                        res.AddRange(buildSuggestedTopology(g, p, markover.PredicteNextValues(p), level - 1, prefix + order + " - ", linkCountEachLevel));
                    order++;
                }
            return res;
        }

        private void AddToGraph(Graph g, string start, string end)
        {
            try
            {
                if (g.Edges.SingleOrDefault(x => x.Source == start && x.Target == end) == null)
                {
                    var n = g.AddEdge(start, end);

                    n.SourceNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Tomato;
                    n.SourceNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Tomato;

                    foreach (var d in n.SourceNode.SelfEdges)
                        n.SourceNode.RemoveSelfEdge(d);
                    foreach (var d in n.TargetNode.SelfEdges)
                        n.TargetNode.RemoveSelfEdge(d);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static string getGender(string c)
        {
            return c == true.ToString() ? "MALE" : c == false.ToString() ? "FEMALE" : "UNKOWEM";
        }

        private static double ranking(long totalSpentTime, long totalPageHits, KeyValuePair<string, Pair<long>> c)
        {
            return ((c.Value.Weight * 100.0 / totalPageHits) * (c.Value.Value * 100.0/ totalSpentTime)) /100;
        }

        private static void searchInDic(Dictionary<string, int> dic, string s)
        {
            if (dic.ContainsKey(s) == false)
                dic.Add(s, 0);
            dic[s]++;
        }
        private static T searchInDic<T>(Dictionary<string, T> dic, string s,T NEW)
        {
            if (dic.ContainsKey(s) == false)
                dic.Add(s, NEW);
            return dic[s];
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
            //else
            //    MessageBox.Show(v);
        }
        private void PrintInSameLine(string v)
        {
            if (listboxState.Items.Count == 0)
                listboxState.Items.Add("");
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
                Print("no input data");
                return;
            }

            if (clusters == null && associationRules == null && markover == null)
                Print("do 'Clustering', 'Association Rule' or 'Markov' first!!");

            callback(classification, toggleButtonEnable(button4));
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
            return predicate(txtboxClassificationRequest.Text);
        }

        private int getMarkovDepthForClassification()
        {
            return int.Parse(txtboxMarkovDepth.Text);
        }

        private int getKforClassification()
        {
            return int.Parse(txtboxClassification.Text);
        }

        private RecommendationResult predicate(string request)
        {
            return predicate(request, getKforClassification(), getMarkovDepthForClassification());

        }

        private RecommendationResult predicate(string request, int k, int markovdepth)
        {
            if (recommender == null)
                recommender = new Recommender(extractedUsers);

            recommender.Clusters = clusters;
            recommender.Rules = associationRules;
            recommender.K = k;
            recommender.MarkovDepth = markovdepth;
            recommender.MarkovChain = markover;
            return recommender.Recommend(request);
        }

        void callback(Action<string> core, string param, Action<long> after)
        {
            new Thread(() =>
            {
                Stopwatch st = Stopwatch.StartNew();
                core(param);
                st.Stop();
                if (DEBUGGING == false)
                    after(st.ElapsedMilliseconds);
            }) { IsBackground = true }.Start();
        }
        void callback(Action core, Action<long> after)
        {
            new Thread(() =>
            {
                Stopwatch st = Stopwatch.StartNew();
                core();
                st.Stop();
                if(DEBUGGING == false)
                    after(st.ElapsedMilliseconds);
            })
            { IsBackground = true }.Start();
        }

        Server server;
        private void button5_Click(object sender1, EventArgs e1)
        {
            if (server == null)
            {
                server = new Server(receive)
                {
                    OnClientConnected = () => { Print("client connected . . ."); },
                    OnPauseOrResume = x => {
                        Print("server is " + (x ? "paused" : "resumed") + " . . . ");
                        button5.Text = (x ? "Resume" : "Pasue") + " Server";
                    }
                };

                Print("server is running . . . ");
                button5.Text = "Pasue Server";
                return;
            }

            server.PauseOrResume();
        }
        string receive(string m)
        {
            Print("client :" + m);
            try
            {
                return predicate(m).ToString();
            }
            catch { }

            if (m != "exit")
                return "unkowen command";
            return m;
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
            callback(bulidMarkov, toggleButtonEnable(btnMarkovBuild));
        }

        private Action<long> toggleButtonEnable(Button b)
        {
            b.Enabled = false;
            return x => { b.Enabled = true; Print("\t\tdone in " + (x / 1000.0) + " sec"); };
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
            string msg = "\r\n=================================\r\n" +
                "|  This Demo for our graduation project\r\n|\tat Damascus University (Syria)\r\n|\t" +
                "at year 2017/2016\r\n|  By:\r\n|\tBaha'a Alsharif (http://github.com/bhlshrf)\r\n|\t" +
                " Ziad Hashem\r\n|\tBasel Altoom \r\n|\tBakr Damman" +
                "\r\n=================================\r\n";
            PrintLines(msg.Split('\n'));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog() { Filter = "XML|*.xml|Compressed XML|*.gzip|Any|*.*", FileName = "data" };
            if (save.ShowDialog(this) == DialogResult.OK)
                callback(saveData, save.FileName, toggleButtonEnable(button7));
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
                callback(loadData, open.FileName, toggleButtonEnable(button8));
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
                Print("users count = " + extractedUsers.Count.ToString("N0"));
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
                server.Dispose();
        }

        private void btnZoomInListbox_Click(object sender, EventArgs e)
        {
            if (listboxState.Font.Size + 2 < 60)
                listboxState.Font = new System.Drawing.Font(listboxState.Font.FontFamily, listboxState.Font.Size + 2);
        }

        private void btnZoomOuListbox_Click(object sender, EventArgs e)
        {
            if (listboxState.Font.Size - 2 > 0)
                listboxState.Font = new System.Drawing.Font(listboxState.Font.FontFamily, listboxState.Font.Size - 2);
        }
    }
}