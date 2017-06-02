using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMining
{
    class Engine
    {
        private Parser parser;
        private Dictionary<string, User> extractedUsers;

        public Engine()
        {
            parser = new Parser();
            extractedUsers = new Dictionary<string, User>();
        }


        private Action<int, string> notifyer;
        public Engine setNotifyer(Action<int, string> n)
        {
            notifyer = n;
            return this;
        }


        public List<User> getExtractedUsers()
        {
            return extractedUsers.Values.ToList();
        }

        public Engine ProcessAll(List<string> logfiles)
        {
            notifyer(0, "start reading data");

            foreach (var f in logfiles)
                Process(File.ReadAllLines(f));

            return this;
        }

        public Engine Process(string[] logTexts)
        {
            notifyer(0, "start processing data");

            int i = 0, total = logTexts.Length, every = 1;
            if (total > 100)
                every = total / 100;
            foreach (var r in parser.ParseNextRecord(logTexts))
            {
                if (i % every == 0)
                    notifyer((int)(i *100.0/ total), i + " lines have proccesed");
                ++i;
                UserIdentification(r);
            }
            notifyer((int)(i * 100.0 / total), i + " lines have proccesed");
            return this;
        }

        private void UserIdentification(Record r)
        {
            if (extractedUsers.ContainsKey(r.CookieID) == false)
                extractedUsers.Add(r.CookieID, new User());

            Sessionization(r, extractedUsers[r.CookieID]);
        }

        private void Sessionization(Record r, User u)
        {
            Session s = findCurrectSession(r.Time, u.Sessions) ?? addNewSession(r, u);
            s.AddRecord(r);    
        }

        private Session findCurrectSession(DateTime time, List<Session> sessions)
        {
            foreach (var s in sessions)
                if (isBelongToSession(time, s))
                    return s;

            return null;
        }

        private bool isBelongToSession(DateTime time, Session s)
        {
            return (s.StartTime - time).Duration().TotalSeconds <= Session.TimeOutSec;
        }

        private Session addNewSession(Record r, User u)
        {
            Session s = new Session();
            u.AddSession(s);
            return s;
        }
    }
}
