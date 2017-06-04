using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMining
{
    public class AssociationRules
    {
        ItemsDictionary allFrequentItems;
        List<Item> frequentItems;

        public AssociationRules(List<Item> _frequentItems)
        {
            allFrequentItems = new ItemsDictionary(frequentItems = _frequentItems);
        }

        public Output GenerateRules(double minConfidence)
        {
            return new Output
            {
                StrongRules = GetStrongRules(minConfidence, generateRules()),
                MaximalItemSets = GetMaximalItemSets(GetClosedItemSets()),
                ClosedItemSets = GetClosedItemSets(),
                FrequentItems = frequentItems
            };
        }



        private IList<string> GetMaximalItemSets(Dictionary<string, Dictionary<string, double>> closedItemSets)
        {
            var maximalItemSets = new List<string>();

            foreach (var item in closedItemSets)
                if (item.Value.Count == 0)
                    maximalItemSets.Add(item.Key);

            return maximalItemSets;
        }

        private HashSet<Rule> generateRules()
        {
            var rulesList = new HashSet<Rule>();

            foreach (var item in allFrequentItems)
                if (item.Name.Length > 1)
                {
                    IEnumerable<string> subsetsList = GenerateSubsets(item.Name);

                    foreach (var subset in subsetsList)
                    {
                        string remaining = GetRemaining(subset, item.Name);
                        Rule rule = new Rule(subset, remaining, 0);

                        if (!rulesList.Contains(rule))
                            rulesList.Add(rule);
                    }
                }

            return rulesList;
        }

        private string GetRemaining(string child, string parent)
        {
            for (int i = 0; i < child.Length; i++)
                parent = parent.Remove(parent.IndexOf(child[i]), 1);

            return parent;
        }

        private IList<Rule> GetStrongRules(double minConfidence, HashSet<Rule> rules)
        {
            var strongRules = new List<Rule>();

            foreach (Rule rule in rules)
            {
                string xy = Apriori.Sort(rule.X + rule.Y);
                AddStrongRule(rule, xy, strongRules, minConfidence);
            }

            strongRules.Sort();
            return strongRules;
        }


        private IEnumerable<string> GenerateSubsets(string item)
        {
            IEnumerable<string> allSubsets = new string[] { };
            int subsetLength = item.Length / 2;

            for (int i = 1; i <= subsetLength; i++)
            {
                IList<string> subsets = new List<string>();
                GenerateSubsetsRecursive(item, i, new char[item.Length], subsets);
                allSubsets = allSubsets.Concat(subsets);
            }

            return allSubsets;
        }

        private void AddStrongRule(Rule rule, string XY, List<Rule> strongRules, double minConfidence)
        {
            double confidence = GetConfidence(rule.X, XY);

            if (confidence >= minConfidence)
                strongRules.Add(new Rule(rule.X, rule.Y, confidence));

            confidence = GetConfidence(rule.Y, XY);
            if (confidence >= minConfidence)
                strongRules.Add(new Rule(rule.Y, rule.X, confidence));
        }

        private double GetConfidence(string X, string XY)
        {
            return allFrequentItems[XY].Support / allFrequentItems[X].Support;
        }

        private Dictionary<string, Dictionary<string, double>> GetClosedItemSets()
        {
            var closedItemSets = new Dictionary<string, Dictionary<string, double>>();
            int i = 0;

            foreach (var item in allFrequentItems)
            {
                Dictionary<string, double> parents = GetItemParents(item.Name, ++i);

                if (CheckIsClosed(item.Name, parents))
                    closedItemSets.Add(item.Name, parents);
            }

            return closedItemSets;
        }

        private Dictionary<string, double> GetItemParents(string child, int index)
        {
            var parents = new Dictionary<string, double>();

            for (int j = index; j < allFrequentItems.Count; j++)
            {
                string parent = allFrequentItems[j].Name;

                if (parent.Length == child.Length + 1 && CheckIsSubset(child, parent))
                    parents.Add(parent, allFrequentItems[parent].Support);
            }

            return parents;
        }

        private bool CheckIsClosed(string child, Dictionary<string, double> parents)
        {
            foreach (string parent in parents.Keys)
                if (allFrequentItems[child].Support == allFrequentItems[parent].Support)
                    return false;

            return true;
        }

        private void GenerateSubsetsRecursive(string item, int subsetLength, char[] temp, IList<string> subsets, int q = 0, int r = 0)
        {
            if (q == subsetLength)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < subsetLength; i++)
                    sb.Append(temp[i]);

                subsets.Add(sb.ToString());
            }

            else
                for (int i = r; i < item.Length; i++)
                {
                    temp[q] = item[i];
                    GenerateSubsetsRecursive(item, subsetLength, temp, subsets, q + 1, i + 1);
                }
        }

        private bool CheckIsSubset(string child, string parent)
        {
            foreach (char c in child)
                if (!parent.Contains(c))
                    return false;

            return true;
        }
    }
}