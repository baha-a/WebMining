using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace WebMining
{
    [Serializable]
    public class SerializableClusterOfUsers
    {
        public int ID { get; set; }
        public List<User> Dataset { get; set; }

        public User Center{ get; set; }

        public SerializableClusterOfUsers()
        {

        }

        public static List<Cluster> Convert(List<SerializableClusterOfUsers> list)
        {
            var r = new List<Cluster>();
            foreach (var t in list)
                r.Add(new Cluster() { ID = t.ID, Center = sessionization(t.Center), Dataset = sessionization(t.Dataset) });
            return r;
        }

        private static IDistancable sessionization(User center)
        {
            foreach (var s in center.Sessions)
            {
                s.User = center;
                foreach (var r in s.Requests)
                    r.Session = s;
            }
            return center;
        }

        private static IEnumerable<IDistancable> sessionization(List<User> dataset)
        {
            foreach (var d in dataset)
                foreach (var s in d.Sessions)
                {
                    s.User = d;
                    foreach (var r in s.Requests)
                        r.Session = s;
                }
            return dataset;
        }

        public static List<SerializableClusterOfUsers> Convert(IEnumerable<Cluster> list)
        {
            var r = new List<SerializableClusterOfUsers>();
            List<User> a = null;
            foreach (var t in list)
            {
                a = new List<User>();
                foreach (var d in t.Dataset)
                    a.Add((User)d);
                r.Add(new SerializableClusterOfUsers() { ID = t.ID, Center = (User)t.Center, Dataset = a });
            }
            return r;
        }
    }

    public class Cluster
    {
        public int ID { get; set; }
        public IEnumerable<IDistancable> Dataset { get; set; }

        private IDistancable center = null;
        public IDistancable Center
        {
            get
            {
                if (center == null)
                    center = CalculateCenter();
                return center;
            }
            set
            {
                center = value;
            }
        }

        public Func<IEnumerable<IDistancable>, IDistancable> Marger { get; set; }

        private static int IDer = 0;
        public Cluster(IEnumerable<IDistancable> e, Func<IEnumerable<IDistancable>, IDistancable> marge)
        {
            ID = IDer++;
            Dataset = e;
            Marger = marge;
        }

        public Cluster()
        {
            ID = IDer++;
            Marger = AvarageUser;
        }

        
        public static IDistancable First(IEnumerable<IDistancable> arg)
        {
            return arg.First();
        }

        public static IDistancable AvarageUser(IEnumerable<IDistancable> arg)
        {
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
        private IDistancable CalculateCenter()
        {
            if (Marger == null)
                return null;
            return Marger(Dataset);
        }
    }
}