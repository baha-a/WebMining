using System;
using System.Collections.Generic;

namespace WebMining
{
    public class SessionOutputParser : ITransactionOutputParser
    {
        public SessionOutputParser(string delimiter = ">")
        {
            Delimiter = delimiter;
            Session.BuildReverseItemsOfTransactions();
        }

        public IEnumerable<string> GetAllPages()
        {
            return Session.GetAllPages();
        }

        public IEnumerable<string> ParseToLines(string transaction)
        {
            List<string> res = new List<string>();
            foreach (var c in transaction)
                res.Add(Session.GetItemsByLetter(c));
            return res;
        }

        public string Delimiter { get; set; }

        public string Parse(string transaction)
        {
            string res = "";
            foreach (var c in transaction)
                res += Session.GetItemsByLetter(c) + Delimiter;
            return res;
        }

        public IEnumerable<string> ParseAllToLines(IEnumerable<string> transaction)
        {
            List<string> res = new List<string>();
            foreach (var t in transaction)
                res.Add(Parse(t));
            return res;
        }
    }
}