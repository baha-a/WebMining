using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMining
{

    public class User : IWeightable
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

        public double Distance(IWeightable t)
        {
            return Math.Abs(GetWeight() - t.GetWeight());
        }

        public IEnumerable<string> GetTransactions()
        {
            List<string> res = new List<string>();
            foreach (var s in Sessions)
                res.Add(s.GetTransaction());
            return res;
        }

        public double GetWeight()
        {
            double r = Sessions.Count();
            foreach (var s in Sessions)
                r += s.GetWeight();
            return r;
        }
        #endregion
    }


    public class Session : IWeightable
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

        private static int charIndex = 'a';
        private static char generateUniqueLeter()
        {
            char c = '0';
            for (; charIndex < char.MaxValue;)
                if (!char.IsControl(c = Convert.ToChar(charIndex++)))
                    break;
            return c;
        }

        public double Distance(IWeightable t)
        {
            return Math.Abs(GetWeight() - t.GetWeight());
        }

        public double GetWeight()
        {
            double r = Records.Count();
            r += (LastTime - StartTime).TotalSeconds;
            foreach (var s in Records)
                r += s.GetWeight();
            return r;
        }


        #endregion
    }


    public class Request : IWeightable
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

        public double Distance(IWeightable t)
        {
            return Math.Abs(GetWeight() - t.GetWeight());
        }

        public double GetWeight()
        {
            throw new NotImplementedException();
        }


        public Request CalcualetValues()
        {
            // to do later
            // calculate time for request
            //
            return this;
        }


        public double Normalization(double v)
        {
            return (v - MinWeight) / (MaxWeight - MinWeight);
        }

        public static double MaxWeight { get; private set; }
        public static double MinWeight { get; private set; }

        public static void SetMinMaxValues(double v)
        {
            if (v > MaxWeight)
                MaxWeight = v;
            if (v < MinWeight)
                MinWeight = v;
        }

        #endregion
    }
}