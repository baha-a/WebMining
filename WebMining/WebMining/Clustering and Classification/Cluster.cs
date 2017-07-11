using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMining
{
    public class Cluster
    {
        public int ID { get; set; }
        public IEnumerable<IDistancable> Dataset { get; set; }

        IDistancable center = null;
        public IDistancable Center
        {
            get
            {
                if (center == null)
                    center = CalculateCenter();
                return center;
            }
        }

        public Func<IEnumerable<IDistancable>, IDistancable> Marger { get; set; }

        static int IDer = 0;
        public Cluster(IEnumerable<IDistancable> e, Func<IEnumerable<IDistancable>, IDistancable> marge)
        {
            ID = IDer++;
            Dataset = e;
            Marger = marge;
        }

        private IDistancable CalculateCenter()
        {
            return Marger(Dataset);
        }

        public static IDistancable AvarageUser(IEnumerable<IDistancable> arg)
        {
            System.Windows.Forms.MessageBox.Show("Test");
            Dictionary<string, int> countrycode = new Dictionary<string, int>();
            Dictionary<string, int> browser = new Dictionary<string, int>();
            Dictionary<string, int> OS = new Dictionary<string, int>();

            Dictionary<string, Pair<IEnumerable<Session>>> session = new Dictionary<string, Pair<IEnumerable<Session>>>();

            Dictionary<string, int> gender = new Dictionary<string, int>
            {
                { true.ToString(), 0 },
                { false.ToString(), 0 },
                { "null", 0 }
            };

            Session tmpS;
            string tmpT;
            foreach (User u in arg)
            {
                if (u.Gender == null)
                    gender["null"]++;
                else
                    gender[u.Gender.ToString()]++;
                if (u.Sessions != null && u.Sessions.Count > 0)
                {
                    tmpS = u.Sessions[0];

                    searchInDic(countrycode, tmpS.CountryCode);
                    searchInDic(browser, tmpS.Browser);
                    searchInDic(OS, tmpS.OperatingSystem);
                }

                tmpT = "";
                foreach (var s in u.Sessions)
                    tmpT += s.GetTransaction();

                if (session.ContainsKey(tmpT) == false)
                    session.Add(tmpT, new Pair<IEnumerable<Session>>(0, null));
                session[tmpT].Weight++;
                session[tmpT].Value = u.Sessions;
            }

            User avarage = new User()
            {
                Sessions = Session.Clone(session.OrderByDescending(x => x.Value.Weight).First().Value.Value)
            };
            string country = countrycode.OrderByDescending(x => x.Value).First().Key;
            string browsr = browser.OrderByDescending(x => x.Value).First().Key;
            string os = OS.OrderByDescending(x => x.Value).First().Key;

            foreach (var s in avarage.Sessions)
            {
                s.Browser = browsr;
                s.OperatingSystem = os;
                s.CountryCode = country;

                s.User = avarage;
            }

            if (gender[true.ToString()] > gender[false.ToString()] && gender[true.ToString()] > gender["null"])
                avarage.Gender = true;
            else if (gender[false.ToString()] > gender[true.ToString()] && gender[false.ToString()] > gender["null"])
                avarage.Gender = false;
            else
                avarage.Gender = null;

            return avarage;
        }

        private static void searchInDic(Dictionary<string, int> dic, string s)
        {
            if (dic.ContainsKey(s) == false)
                dic.Add(s, 0);
            dic[s]++;
        }
    }
}