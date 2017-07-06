﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMining
{

    public class User : IDistancMeasurable
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

        public double Distance(IDistancMeasurable t)
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


    public class Session : IDistancMeasurable
    {
        public int ID { get; private set; }
        public User User { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime LastTime { get; set; }

        public List<Request> Records { get; set; }



        public static int TimeOutSec = 1000;

        #region other code
        private static int counter = 0;

        public Session()
        {
            Records = new List<Request>();
            ID = ++counter;
        }

        public void AddRecord(Request r)
        {
            if (Records.Count == 0)
                StartTime = r.Time;

            if (LastTime < r.Time)
                LastTime = r.Time;

            Records.Add(r);
            r.Session = this;
        }

        public override string ToString()
        {
            return "Ses_" + ID + " Time[" + StartTime.ToString("HH:mm:ss dd-MM-yyyy") + "] Records(" + Records.Count + ")";
        }


        public string GetTransaction()
        {
            string res = "";
            foreach (var r in Records)
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

        static Dictionary<char, string> _itemsreversed;
        static void BuildReverseItemsOfTransactions()
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

        public double Distance(IDistancMeasurable t)
        {
            Session s = t as Session;
            if (s == null)
                return -1;

            double dis = Math.Abs(Records.Count() - s.Records.Count()) +
                Math.Abs(StartTime.Ticks - s.StartTime.Ticks) +
                Math.Abs(LastTime.Ticks - s.LastTime.Ticks) +
                Math.Abs((LastTime - StartTime).TotalSeconds - (s.LastTime - s.StartTime).TotalSeconds);

            foreach (var r in Records)
                foreach (var rr in s.Records)
                    dis += r.Distance(rr);
            return dis;
        }
        #endregion
    }


    public class Request : IDistancMeasurable
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

        public double Distance(IDistancMeasurable t)
        {
            Request r = t as Request;
            if (r == null)
                return -1;

            double dis = 0;
            if (r.Gender != Gender)
                dis += 1;
            if (r.CountryCode != CountryCode)
                dis += 1;
            if (r.Browser != Browser)
                dis += 1;
            if (r.OperatingSystem != OperatingSystem)
                dis += 1;
            if (r.RequstedPage != RequstedPage)
                dis += 1;
            if (r.SourcePage != SourcePage)
                dis += 1;

            return dis + distanceTime(Time, r.Time);
        }

        double distanceTime(DateTime t, DateTime p)
        {
            return Math.Abs(t.Day - p.Day) +
                Math.Abs(t.Month - p.Month) +
                Math.Abs(t.Year - p.Year) +
                Math.Abs(t.Hour - p.Hour) +
                +Math.Abs(t.Minute - p.Minute) +
                +Math.Abs(t.Second - p.Second);
        }
        public Request CalcualetValues()
        {
            timeNormalization.SetMinMaxValues(Time.Ticks);
            return this;
        }


        static MinMax timeNormalization = new MinMax();
        public double NormalizationTime()
        {
            return (Time.Ticks - timeNormalization.MinWeight) / (timeNormalization.MaxWeight - timeNormalization.MinWeight);
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