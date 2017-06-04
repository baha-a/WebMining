using System;

namespace WebMining
{
    public class Rule : IComparable<Rule>
    {
        string combination, remaining;
        double confidence;

        public Rule(string combination, string remaining, double confidence)
        {
            this.combination = combination;
            this.remaining = remaining;
            this.confidence = confidence;
        }

        public string X { get { return combination; } }

        public string Y { get { return remaining; } }

        public double Confidence { get { return confidence; } }


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