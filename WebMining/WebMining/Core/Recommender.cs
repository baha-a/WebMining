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
            var user = cacher.ProcessLineWithoutAddAnything(request);
            string t = user.Sessions.Last().GetTransaction();
            string m = user.Sessions.Last().Requests.Last().RequstedPage;

            return new RecommendationResult()
            {
                User = user,

                Gender = predicateGender(user),
                SuggestedPages = sugguestPageByUsingRules(t),
                SuggestedPagesByMarkov = suggestPagesByMarkov(m),
                Cluster = getNearestCluster(user),

                OriginalRequest = cacher.ParseToRequest(request)
            };
        }

        private IEnumerable<string> suggestPagesByMarkov(string t)
        {
            var pages = new List<string>();
            foreach (var m in MarkovChain.PredicteNextWithhProbabilities(t))
            {
                if (m.Key.State == MarkovState.End)
                    pages.Add("[END] - " + m.Value * 100 + " %");
                else
                    pages.Add(m.Key.Value + " - " + m.Value * 100 + " %");
                foreach (var v in m.Key.GetNextsWithProbabilities())
                    if (v.Key.State == MarkovState.End)
                        pages.Add("\t[END] - " + v.Value * 100 + " %");
                    else
                        pages.Add("\t" + v.Key.Value + " - " + v.Value * 100 + " %");
            }
            return pages;
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
            return sessionOutputParser.ParseAllToLines(Rules.Where(x => x.X == t).Select(x => x.Y));
        }

        private Cluster getNearestCluster(User user)
        {
            if (Clusters == null)
                return null;
            return Clusters.OrderByDescending(y => user.Distance(y.Center)).First();
        }
    }
}