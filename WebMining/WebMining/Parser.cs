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
                if (isValidLine(l))
                    f.Add(parseString(l));
            return f;
        }

        public IEnumerable<Record> ParseNextRecord(string[] logTexts)
        {
            foreach (var l in logTexts)
                if (isValidLine(l))
                    yield return parseString(l);
        }

        private Record parseString(string line)
        {
            // 0000000006 8vskqfr1mov00fh0 NONE 69.13.76.58 SY 'Opera' 'Mac' 01:00:26 14-01-2017 'PAGE2' 'PAGE1'

            string[] cells = split(line);
            return new Record()
            {
                ID = int.Parse(cells[0]),
                CookieID = cells[1],
                Gender = parseGender(cells[2]),
                IPaddress = cells[3],
                CountryCode = cells[4],
                Browser = cells[5],
                OperatingSystem = cells[6],
                Time = parseData(cells[7], cells[8]),
                RequstedPage = cells[9],
                SourcePage = cells[10]
            };
        }

        private static bool isValidLine(string line)
        {
            return isNotComment(line) && isNotEmpty(line);

        }

        private static bool isNotEmpty(string line)
        {
            return string.IsNullOrWhiteSpace(line) == false;
        }

        private static bool isNotComment(string line)
        {
            return line.StartsWith("#") == false;
        }

        private static DateTime parseData(string time,string data)
        {
            return DateTime.ParseExact(time + " " + data, "HH:mm:ss dd-MM-yyyy", null);
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