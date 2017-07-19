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
            string result = Gender + "";
            return base.ToString();
        }
    }
}