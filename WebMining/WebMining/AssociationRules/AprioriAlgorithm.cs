using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMining
{
    public class Apriori
    {
        IInputParser transactions;
        public Apriori(IInputParser _transactions)
        {
            transactions = _transactions;
        }

        public List<Item> GenerateFrequentItemsets(double minSupport)
        {
            List<Item> frequentItems = GetL1FrequentItems(minSupport);
            List<Item> allFrequentItems = new List<Item>(frequentItems);

            IDictionary<string, double> candidates = new Dictionary<string, double>();
            double transactionsCount = transactions.Count();

            do
            {
                candidates = GenerateCandidates(frequentItems);
                frequentItems = GetFrequentItems(candidates, minSupport, transactionsCount);
                allFrequentItems.AddRange(frequentItems);
            }
            while (candidates.Count != 0);

            return allFrequentItems;
        }


        private List<Item> GetL1FrequentItems(double minSupport)
        {
            var frequentItemsL1 = new List<Item>();
            double transactionsCount = transactions.Count();

            foreach (var item in transactions.GetItems())
            {
                double support = GetSupport(item+"");

                if (support / transactionsCount >= minSupport)
                    frequentItemsL1.Add(new Item { Name = item + "", Support = support });
            }
            frequentItemsL1.Sort();
            return frequentItemsL1;
        }

        private double GetSupport(string generatedCandidate)
        {
            double support = 0;

            foreach (string transaction in transactions.GetTransactions())
                if (CheckIsSubset(generatedCandidate, transaction))
                    support++;

            return support;
        }

        private bool CheckIsSubset(string child, string parent)
        {
            foreach (char c in child)
                if (!parent.Contains(c))
                    return false;

            return true;
        }

        private Dictionary<string, double> GenerateCandidates(IList<Item> frequentItems)
        {
            Dictionary<string, double> candidates = new Dictionary<string, double>();

            for (int i = 0; i < frequentItems.Count - 1; i++)
            {
                string firstItem = Apriori.Sort(frequentItems[i].Name);

                for (int j = i + 1; j < frequentItems.Count; j++)
                {
                    string secondItem = Apriori.Sort(frequentItems[j].Name);
                    string generatedCandidate = GenerateCandidate(firstItem, secondItem);

                    if (generatedCandidate != string.Empty)
                        candidates.Add(generatedCandidate, GetSupport(generatedCandidate));
                }
            }

            return candidates;
        }

        private string GenerateCandidate(string firstItem, string secondItem)
        {
            int length = firstItem.Length;

            if (length == 1)
                return firstItem + secondItem;

            string firstSubString = firstItem.Substring(0, length - 1);
            string secondSubString = secondItem.Substring(0, length - 1);

            if (firstSubString == secondSubString)
                return firstItem + secondItem[length - 1];

            return string.Empty;
        }

        private List<Item> GetFrequentItems(IDictionary<string, double> candidates, double minSupport, double transactionsCount)
        {
            var frequentItems = new List<Item>();

            foreach (var item in candidates)
                if (item.Value / transactionsCount >= minSupport)
                    frequentItems.Add(new Item { Name = item.Key, Support = item.Value });

            return frequentItems;
        }

        public static string Sort(string token)
        {
            char[] tokenArray = token.ToCharArray();
            Array.Sort(tokenArray);
            return new string(tokenArray);
        }
    }
}