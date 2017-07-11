using System.Collections.Generic;
using System.Linq;

namespace WebMining
{
    public class Recommender
    {
        public int K { get; set; }
        public IEnumerable<Rule> Rules { get; set; }
        public IEnumerable<Cluster> Clusters { get; set; }

        Engine cacher = new Engine();
        KNN knn = new KNN();

        public Recommender(IEnumerable<User> extractedUsers)
        {
            knn.Initialize(extractedUsers);
        }

        public RecommendationResult Recommend(string request)
        {
            var user = cacher.ProcessLineWithoutAddAnything(request);
            string t = user.Sessions.Last().GetTransaction();

            return new RecommendationResult()
            {
                Gender = predicateGender(user),
                SuggestedPages = sugguestPageByUsingRules(t),
                Cluster = getNearestCluster(user),

                OriginalRequest = cacher.ParseToRequest(request)
            };
        }

        private bool? predicateGender(User user)
        {
            return knn.PredicateGender(K, user);
        }


        SessionOutputParser sessionOutputParser = new SessionOutputParser(" - ");
        private IEnumerable<string> sugguestPageByUsingRules(string t)
        {
            return sessionOutputParser.ParseAllToLines(Rules.Where(x => x.X == t).Select(x => x.Y));
        }

        private Cluster getNearestCluster(User user)
        {
            return Clusters.OrderByDescending(y => user.Distance(y.Center)).First();
        }
    }
}