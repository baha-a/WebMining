using System;

namespace WebMining
{
    [Serializable]
    public class Rule : IComparable<Rule>
    {
        public Rule(string combination, string remaining, double confidence)
        {
            X = combination;
            Y = remaining;
            Confidence = confidence;
        }
        public Rule()
        {

        }

        public string X { get; set; }

        public string Y { get; set; }

        public double Confidence { get; set; }


        public int CompareTo(Rule other)
        {
            return X.CompareTo(other.X);
        }


        public override int GetHashCode()
        {
            return Apriori.Sort(X + Y).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Rule;
            if (other == null)
                return false;

            return other.X == X && other.Y == Y || other.X == Y && other.Y == X;
        }
    }
}