using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMining
{
    public class StringInputParser: ITransactionInputParser
    {
        IEnumerable<string> _transactions;
        IEnumerable<char> _items;

        public StringInputParser(IEnumerable<string> transactions)
        {
            _transactions = transactions;
            _items = extractAllItems();
        }

        private IEnumerable<char> extractAllItems()
        {
            HashSet<char> res = new HashSet<char>();
            foreach (var ts in _transactions)
                foreach (var t in ts)
                    if (res.Contains(t) == false)
                        res.Add(t);
            return res;
        }

        public IEnumerable<char> GetItems()
        {
            return _items;
        }

        public IEnumerable<string> GetTransactions()
        {
            foreach (var t in _transactions)
                yield return t;
        }

        public int Count()
        {
            return _transactions.Count();
        }
    }

    public class SessionInputParser : ITransactionInputParser
    {
        IList<string> _transactions;
        IEnumerable<char> _items;

        public SessionInputParser(IEnumerable<Session> sessions)
        {
            _transactions = new List<string>();
            foreach (var s in sessions)
                _transactions.Add(s.GetTransaction());

            _items = Session.GetItemsOfTransactions();
        }

        public IEnumerable<char> GetItems()
        {
            return _items;
        }

        public IEnumerable<string> GetTransactions()
        {
            foreach (var t in _transactions)
                yield return t;
        }

        public int Count()
        {
            return _transactions.Count();
        }
    }
}