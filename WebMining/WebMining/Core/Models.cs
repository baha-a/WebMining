﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMining
{

    public class User : IMeasurable
    {
        public int ID { get; private set; }
        public List<Session> Sessions { get; set; }

        public bool? Gender { get; set; }

        #region other code
        private static int counter = 0;
        public User()
        {
            Sessions = new List<Session>();
            ID = ++counter;
        }

        public void AddSession(Session s)
        {
            Sessions.Add(s);
            s.User = this;
        }

        public override string ToString()
        {
            string str = "USER_" + ID + " Sesssion(" + Sessions.Count + ")";
            Sessions.ForEach(s=> str += "\r\n\t" + s);
            return str;
        }

        public double Distance(IMeasurable t)
        {
            User u = t as User;
            if (u == null)
                return -1;

            double dis = Math.Abs(Sessions.Count() - u.Sessions.Count());

            foreach (var s in Sessions)
                foreach (var ss in u.Sessions)
                    dis += s.Distance(ss);

            return dis;
        }

        public IEnumerable<string> GetTransactions()
        {
            List<string> res = new List<string>();
            foreach (var s in Sessions)
                res.Add(s.GetTransaction());
            return res;
        }
        #endregion
    }


    public class Session : IMeasurable
    {
        public int ID { get; private set; }
        public User User { get; set; }

        public string CountryCode { get; set; }
        public string Browser { get; set; }
        public string OperatingSystem { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeSpan Duration { get { return (EndTime - StartTime); } }


        public List<Request> Requests { get; set; }



        public static int TimeOutSec = 1000;

        #region other code
        private static int counter = 0;

        public Session()
        {
            Requests = new List<Request>();
            ID = ++counter;
        }

        public Session(Session s) : this()
        {
            Browser = s.Browser;
            OperatingSystem = s.OperatingSystem;
            CountryCode = s.CountryCode;

            StartTime = s.StartTime;
            EndTime = s.EndTime;

            Requests = s.Requests;
        }

        public static List<Session> Clone(IEnumerable<Session> values)
        {
            List<Session> res = new List<Session>();
            foreach(var v in values)
                res.Add(new Session(v));
            return res;
        }

        public void AddRequest(Request r)
        {
            if (Requests.Count == 0)
                StartTime = r.Time;

            if (EndTime < r.Time)
                EndTime = r.Time;

            if (string.IsNullOrEmpty(CountryCode))
                CountryCode = r.CountryCode;
            if (string.IsNullOrEmpty(Browser))
                Browser = r.Browser;
            if (string.IsNullOrEmpty(OperatingSystem))
                OperatingSystem = r.OperatingSystem;

            Requests.Add(r);
            r.Session = this;
        }

        public override string ToString()
        {
            return "Ses_" + ID + " Time[" + StartTime.ToString("HH:mm:ss dd-MM-yyyy") + "] Requests(" + Requests.Count + ")";
        }


        public string GetTransaction()
        {
            string res = "";
            foreach (var r in Requests)
                res += getChar(r.RequstedPage);
            return res;
        }

        private char getChar(string page)
        {
            if (_items.ContainsKey(page) == false)
                _items.Add(page, generateUniqueLeter());
            return _items[page];
        }


        static Dictionary<string,char> _items = new Dictionary<string,char>();
        public static IEnumerable<char> GetItemsOfTransactions()
        {
            return _items.Values;
        }

        public static IEnumerable<string> GetAllPages()
        {
            return _items.Keys;
        }

        static Dictionary<char, string> _itemsreversed;
        public static void BuildReverseItemsOfTransactions()
        {
            _itemsreversed = new Dictionary<char, string>();
            foreach (var t in _items)
                _itemsreversed.Add(t.Value, t.Key);
        }

        public static string GetItemsByLetter(char c)
        {
            if (_itemsreversed == null)
                BuildReverseItemsOfTransactions();
            return _itemsreversed[c];
        }

        private static int charIndex = 'a';
        private static char generateUniqueLeter()
        {
            char c = '0';
            for (; charIndex < char.MaxValue;)
                if (!char.IsControl(c = Convert.ToChar(charIndex++)))
                    break;
            return c;
        }

        public double Distance(IMeasurable t)
        {
            Session s = t as Session;
            if (s == null)
                throw new Exception("The Argument must be of the type 'Session' only") ;

            double dis = 0;

            if (s.User.Gender != User.Gender)
                dis += 1;
            if (s.CountryCode != CountryCode)
                dis += 1;
            if (s.Browser != Browser)
                dis += 1;
            if (s.OperatingSystem != OperatingSystem)
                dis += 1;


            dis += Math.Abs(Duration.TotalMinutes - s.Duration.TotalMinutes);
            dis += Math.Abs(Requests.Count() - s.Requests.Count());
            dis += distanceTime(StartTime, s.StartTime);

            dis += similaritor.GetDissimilarity(GetTransaction(), s.GetTransaction()) * SCALE;

            return dis;
        }

        SmithWaterman similaritor = new SmithWaterman();
        static double SCALE = 10;

        double distanceTime(DateTime t, DateTime p)
        {
            return normalize(Math.Abs((t.Hour  - p.Hour ) % 24), 24) +
                   normalize(Math.Abs((t.Day   - p.Day  ) % 7 ),  7) +
                   normalize(Math.Abs((t.Month - p.Month) % 12), 12);
        }

        double normalize(double value, double max, double min = 0)
        {
            return (value - min) / (max - min);
        }

        #endregion
    }


    public class Request
    {
        public Session Session { get; set; }


        public int ID { get; set; }

        public string CookieID { get; set; }

        public bool? Gender { get; set; }

        public string IPaddress { get; set; }


        public string CountryCode { get; set; }

        public string Browser { get; set; }

        public string OperatingSystem { get; set; }

        public DateTime Time { get; set; }

        public string RequstedPage { get; set; }

        public string SourcePage { get; set; }


        #region other code
        public override string ToString()
        {
            return ID + " "  + CookieID + " " + Gender  + " " + IPaddress + " " + CountryCode + " " + Browser 
                + " " + OperatingSystem + " " + Time + " "  + RequstedPage + " " + SourcePage;
        }

        public Request CalcualetValues()
        {
            // I forget what I planned to do with this function
            return this;
        }


        #endregion
    }


    public class MinMax
    {
        public double MaxWeight { get; private set; }
        public double MinWeight { get; private set; }

        public void SetMinMaxValues(double v)
        {
            if (v > MaxWeight)
                MaxWeight = v;
            if (v < MinWeight)
                MinWeight = v;
        }
    }
}