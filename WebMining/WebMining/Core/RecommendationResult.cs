using System;
using System.Collections.Generic;

namespace WebMining
{
    [Serializable]
    public class RecommendationResult
    {
        public bool? Gender { get; set; }

        public IEnumerable<string> SuggestedPages { get; set; }

        public IEnumerable<string> SuggestedPagesByMarkov { get; set; }

        public Cluster Cluster { get; set; }



        public User User { get; set; }
        public Request OriginalRequest { get; set; }


        public override string ToString()
        {
            return "|gender=" + Gender + "&" +
                "cluster=" + (Cluster != null ? Cluster.ID : 0) + "&" +
                "arpages=[" + singleLine(SuggestedPages) + "]&" +
                "markovpages=[" + singleLine(SuggestedPagesByMarkov) + "]|";
        }

        private string singleLine(IEnumerable<string> suggestedPages)
        {
            string line = "";
            foreach (var t in suggestedPages)
                line += t + ",";
            return deletelastLetter(line);
        }

        private static string deletelastLetter(string line)
        {
            if (string.IsNullOrEmpty(line))
                return line;
            return line.Remove(line.Length - 1);
        }
    }
}