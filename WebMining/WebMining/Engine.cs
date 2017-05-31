using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMining
{
    class Engine
    {
        public List<Record> ParseLog(string[] logTexts)
        {
            List<Record> f = new List<Record>();
            foreach (var l in logTexts)
                f.Add(parseString(l));
            return f;
        }

        public List<User> Magic(string[] logTexts)
        {
            return UserIdentification(ParseLog(logTexts));
        }

        public List<User> UserIdentification(List<Record> records)
        {
            Dictionary<string, User> d = new Dictionary<string, User>();

            foreach (var r in records)
            {
                if (d.ContainsKey(r.CookieID) == false)
                    d.Add(r.CookieID, new User());

                Sessionization(r, d[r.CookieID]);
            }

            return d.Values.ToList();
        }

        public void Sessionization(Record r, User u)
        {
            Session s = findCurrectSession(r.Time, u.Sessions);

            if (s != null)
                connectSessionWithRecord(s, r);
            else
                addNewSession(r, u);
        }

        public Session findCurrectSession(DateTime time, List<Session> sessions)
        {
            foreach (var s in sessions)
                if ((s.Time - time).Duration().TotalSeconds <= Session.TimeOutSec)
                    return s;

            return null;
        }

        private Session addNewSession(Record r, User u)
        {
            Session s = new Session() { User = u, Time = r.Time };
            connectSessionWithUser(s, u);
            connectSessionWithRecord(s, r);
            return s;
        }

        private void connectSessionWithUser(Session s,User u)
        {
            u.Sessions.Add(s);
            s.User = u;
        }

        private void connectSessionWithRecord(Session s,Record r)
        {
            s.Records.Add(r);
            r.Session = s;
        }

        private Record parseString(string line)
        {
            // 000006 8vskqfr1mov00fh0 NONE 69.13.76.58	'Opera'	'Mac' 01:00:26 01-01-2017	'PAGE2'	'PAGE1'
            string[] cells = split(line);
            return new Record()
            {
                ID = int.Parse(cells[0]),
                CookieID = cells[1],
                Gender = parseGender(cells[2]),
                IPaddress = cells[3],
                Browser = cells[4],
                OperatingSystem = cells[5],
                Time = DateTime.Parse(cells[6] + " " + cells[7]),
                RequstedPage = cells[8],
                SourcePage = cells[9]
            };
        }

        private string[] split(string line)
        {
            return line.Split(' ', '\t').Where(x => string.IsNullOrWhiteSpace(x) == false).ToArray();
        }

        private static bool? parseGender(string d)
        {
            return (d == "MALE" ? true : (d == "FMLE" ? false : (bool?) null ));
        }
    }
}
