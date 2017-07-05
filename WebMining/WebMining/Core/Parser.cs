using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMining
{
    public class Parser
    {
        public List<Request> ParseAll(string[] logTexts)
        {
            List<Request> f = new List<Request>();
            foreach (var l in logTexts)
                    f.Add(ParseLine(l));
            return f;
        }

        public IEnumerable<Request> ParseNextRecord(string[] logTexts)
        {
            foreach (var l in logTexts)
                    yield return ParseLine(l);
        }

        public Request ParseLine(string line)
        {
            // 0000000006 8vskqfr1mov00fh0 NONE 69.13.76.58 SY 'Opera' 'Mac' 01:00:26 14-01-2017 'PAGE2' 'PAGE1'

            if (isCommentOrEmpty(line))
                return null;

            string[] cells = split(line);
            return new Request()
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
            }.CalcualetValues();
        }

        private static bool isCommentOrEmpty(string line)
        {
            return isComment(line) || isEmpty(line);

        }

        private static bool isEmpty(string line)
        {
            return string.IsNullOrWhiteSpace(line);
        }

        private static bool isComment(string line)
        {
            return line.StartsWith("#");
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