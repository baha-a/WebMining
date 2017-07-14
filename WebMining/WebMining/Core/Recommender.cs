using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMining
{
    public class Recommender
    {
        public int K { get; set; }
        public IEnumerable<Rule> Rules { get; set; }
        public IEnumerable<Cluster> Clusters { get; set; }
        public MarkovChain<string> MarkovChain { get; internal set; }

        Engine cacher = new Engine();
        KNN knn = new KNN();

        public Recommender(IEnumerable<User> extractedUsers)
        {
            knn.Initialize(extractedUsers);
        }

        public RecommendationResult Recommend(string request)
        {
            var res = cacher.ProcessLineWithoutAddAnything(request);
            var user = res.Key;

            string transaction = res.Value.GetTransaction();
            string requestedPage = res.Value.Requests.Last().RequstedPage;

            return new RecommendationResult()
            {
                User = user,

                Gender = predicateGender(user),
                SuggestedPages = sugguestPageByUsingRules(transaction),
                SuggestedPagesByMarkov = suggestPagesByMarkov(requestedPage),
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
            if (Rules == null)
                return new List<string>();
            IEnumerable<string> res = sessionOutputParser.ParseAllToLines(Rules.Where(x => x.X == t).Select(x => x.Y));
            if(res.Count() == 0)
                res = sessionOutputParser.ParseAllToLines(Rules.Where(x => x.X.Contains(t)).Select(x => x.Y));
            return res;
        }

        private Cluster getNearestCluster(User user)
        {
            if (Clusters == null)
                return null;
            return Clusters.OrderByDescending(y => user.Distance(y.Center)).First();
        }


        private IEnumerable<string> suggestPagesByMarkov(string t)
        {
            var pages = new List<string>();

            if (MarkovChain == null)
                return pages;

            foreach (var m in MarkovChain.PredicteNextWithhProbabilities(t))
            {
                pages.Add(printPage(m));
                foreach (var v in m.Key.GetNextsWithProbabilities())
                    pages.Add(printPage(v, "\t"));
            }
            return pages;
        }

        private static string printPage(KeyValuePair<MarkovNode<string>, double> m, string prefix = "")
        {
            if (m.Key.State == MarkovState.End)
                return prefix + "[END] - " + m.Value * 100 + " %";
            return prefix + m.Key.Value + " - " + m.Value * 100 + " %";
        }
    }
}