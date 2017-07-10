using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            notifyer(0, "reading data");

            foreach (var f in logfiles)
            {
                preperingVariables(File.ReadLines(f).Count());
                foreach (var l in File.ReadLines(f))
                    ProcessLine(l);
                notify();
            }
            return this;
        }


        private static Dictionary<string, User> cache = new Dictionary<string, User>();
        public User ProcessLineWithoutAddAnything(string logline)
        {
            var request = parser.ParseLine(logline);

            if (cache.ContainsKey(request.CookieID) == false)
                cache.Add(request.CookieID, new User());

            var user = cache[request.CookieID];

            Sessionization(request, user);
            return user;
        }

        public Request ParseToRequest(string request)
        {
            return parser.ParseLine(request);
        }



        int processedLine,totalLine,checkEvery = 1;

        public Engine ProcessLine(string logTexts)
        {
            UserIdentification(parser.ParseLine(logTexts));

            ++processedLine;
            notifyEveryWhile();

            return this;
        }

        public Engine ProcessAllLines(string[] logTexts)
        {
            notifyer(0, "processing data");

            preperingVariables(logTexts.Length);

            foreach (var r in parser.ParseNextRecord(logTexts))
            {
                notifyEveryWhile();

                UserIdentification(r);

                ++processedLine;
            }

            notify();

            return this;
        }

        private void notifyEveryWhile()
        {
            if (processedLine % checkEvery == 0 && notifyer != null)
                notify();
        }

        private void notify()
        {
            notifyer((int)(processedLine * 100.0 / totalLine), processedLine.ToString("N0") + " lines proccesed");
        }

        private void preperingVariables(int totalsize)
        {
            processedLine = 0;
            totalLine = totalsize;
            checkEvery = 1;

            if (totalLine > 100)
                checkEvery = totalLine / 100;

            if (checkEvery == 0)
                checkEvery = 1;
        }

        private void UserIdentification(Request r)
        {
            if (r == null)
                return;

            if (extractedUsers.ContainsKey(r.CookieID) == false)
                extractedUsers.Add(r.CookieID, new User());

            Sessionization(r, Profilezation(r));
        }

        private User Profilezation(Request r)
        {
            var u = extractedUsers[r.CookieID];
            if (u.Gender == null && r.Gender != null)
                u.Gender = r.Gender;
            return u;
        }

        private void Sessionization(Request r, User u)
        {
            (findCurrectSession(r.Time, u.Sessions) ?? addNewSession(r, u)).AddRequest(r);
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

        private Session addNewSession(Request r, User u)
        {
            Session s = new Session();
            u.AddSession(s);
            return s;
        }
    }
}
