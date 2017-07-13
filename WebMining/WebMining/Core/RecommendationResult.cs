using System.Collections.Generic;

namespace WebMining
{
    public class RecommendationResult
    {
        public bool? Gender { get; set; }

        public IEnumerable<string> SuggestedPages { get; set; }

        public Cluster Cluster { get; set; }

        public Request OriginalRequest { get; set; }

        public User User { get; set; }
    }
}