using System.Collections.Generic;

namespace WebMining
{
    public class RecommendationResult
    {
        public bool? Gender { get; set; }

        public IEnumerable<string> Pages { get; set; }

        public Cluster Cluster { get; set; }
    }
}