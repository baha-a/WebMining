using System.Collections.Generic;

namespace WebMining
{
    public class Output
    {
        public IList<Rule> StrongRules { get; set; }

        public IList<string> MaximalItemSets { get; set; }

        public Dictionary<string, Dictionary<string, double>> ClosedItemSets { get; set; }

        public IList<Item> FrequentItems { get; set; }


        public Output parse(ITransactionOutputParser parser)
        {
            Output res = new Output() {
                StrongRules = new List<Rule>(),
                MaximalItemSets = new List<string>(),
                FrequentItems = new List<Item>(),
                ClosedItemSets = new Dictionary<string, Dictionary<string, double>>()
            };

            foreach (var r in StrongRules)
                res.StrongRules.Add(new Rule(parser.Parse(r.X), parser.Parse(r.Y), r.Confidence));

            foreach (var m in MaximalItemSets)
                res.MaximalItemSets.Add(parser.Parse(m));

            foreach (var f in FrequentItems)
                res.FrequentItems.Add(new Item() { Name = parser.Parse(f.Name), Support = f.Support });


            Dictionary<string, double> tmp;
            foreach (var c in ClosedItemSets)
            {
                tmp = new Dictionary<string, double>();
                foreach (var v in c.Value)
                    tmp.Add(parser.Parse(v.Key), v.Value);
                res.ClosedItemSets.Add(parser.Parse(c.Key), tmp);
            }

            return res;
        }
    }
}