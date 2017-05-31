using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMining
{
    public class Parser
    {
        public List<Record> ParseLog(string[] logTexts)
        {
            List<Record> f = new List<Record>();
            foreach (var l in logTexts)
                f.Add(parseString(l));
            return f;
        }

        private Record parseString(string line)
        {
            // 000006 8vskqfr1mov00fh0 NONE 69.13.76.58	'Opera'	'Mac' 01:00:26 14-01-2017	'PAGE2'	'PAGE1'
            string[] cells = split(line);
            return new Record()
            {
                ID = int.Parse(cells[0]),
                CookieID = cells[1],
                Gender = parseGender(cells[2]),
                IPaddress = cells[3],
                Browser = cells[4],
                OperatingSystem = cells[5],
                Time = DateTime.ParseExact(cells[6] + " " + cells[7], "HH:mm:ss dd-MM-yyyy",null),
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
            return (d == "MALE" ? true : (d == "FMLE" ? false : (bool?)null));
        }
    }
}