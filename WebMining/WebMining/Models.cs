using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMining
{

    public class User
    {
        public int ID { get; private set; }
        public List<Session> Sessions { get; set; }


        #region other code
        private static int counter = 0;
        public User()
        {
            Sessions = new List<Session>();
            ID = ++counter;
        }

        public override string ToString()
        {
            string str = "USER_" + ID + " Sesssion(" + Sessions.Count + ")";
            Sessions.ForEach(s=> str += "\r\n\t" + s);
            return str;
        }
        #endregion
    }


    public class Session
    {
        public int ID { get; private set; }
        public User User { get; set; }

        public DateTime Time { get; set; }

        public List<Record> Records { get; set; }



        public static int TimeOutSec = 1000;

        #region other code
        private static int counter = 0;

        public Session()
        {
            Records = new List<Record>();
            ID = ++counter;
        }

        public override string ToString()
        {
            return "Ses_" + ID + " Time[" + Time + "] Records(" + Records.Count + ")";
        }
        #endregion
    }


    public class Record
    {
        public Session Session { get; set; }


        public int ID { get; set; }

        public string CookieID { get; set; }

        public bool? Gender { get; set; }

        public string IPaddress { get; set; }

        public string Browser { get; set; }

        public string OperatingSystem { get; set; }

        public DateTime Time { get; set; }

        public string RequstedPage { get; set; }

        public string SourcePage { get; set; }


        #region other code
        public override string ToString()
        {
            return ID + " "  + CookieID + " " + Gender  + " " + IPaddress + " " + Browser 
                + " " + OperatingSystem + " " + Time + " "  + RequstedPage + " " + SourcePage;
        }
        #endregion
    }
}