using System.Collections.Generic;

namespace WebMining
{
    public class Output
    {
        public IList<Rule> StrongRules { get; set; }

        public IList<string> MaximalItemSets { get; set; }

        public Dictionary<string, Dictionary<string, double>> ClosedItemSets { get; set; }

        public IList<Item> FrequentItems { get; set; }
    }
}