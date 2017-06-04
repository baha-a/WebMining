using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMining
{
    public class StringInputParser: IInputParser
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

    public class SessionInputParser : IInputParser
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


    //public class RealInputParser : IInputParser
    //{
    //    IEnumerable<IEnumerable<string>> _transactions;
    //    IDictionary<string,char> _items;

    //    public RealInputParser(IEnumerable<IEnumerable<string>> transactions)
    //    {
    //        _transactions = transactions;
    //        _items = extractAllItems();
    //    }

    //    private IDictionary<string, char> extractAllItems()
    //    {
    //        Dictionary<string,char> res = new Dictionary<string,char>();
    //        foreach (var ts in _transactions)
    //            foreach (var t in ts)
    //                if (res.ContainsKey(t) == false)
    //                    res.Add(t, generateUniqueLeter());
    //        return res;
    //    }

    //    private static int charIndex = char.MinValue;
    //    private static char generateUniqueLeter()
    //    {
    //        char c = '0';
    //        for (; charIndex < char.MaxValue;)
    //            if (!char.IsControl(c = Convert.ToChar(charIndex++)))
    //                break;
    //        return c;
    //    }

    //    public IEnumerable<char> GetItems()
    //    {
    //        return _items.Values;
    //    }

    //    public IEnumerable<string> GetTransactions()
    //    {
    //        string trans;
    //        foreach (var tr in _transactions)
    //        {
    //            trans = "";
    //            foreach (var t in tr)
    //                trans += _items[t];
    //            yield return trans;
    //        }
    //    }

    //    public int Count()
    //    {
    //        return _transactions.Count();
    //    }
    //}
}