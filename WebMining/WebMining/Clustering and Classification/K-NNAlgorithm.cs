using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMining.Clustering
{
    public class KNN
    {
        public IEnumerable<User> TrainingModel { get; private set; }

        public KNN()
        {
            TrainingModel = new List<User>();
        }

        public KNN Initialize(IEnumerable<User> records)
        {
            TrainingModel = getTraningModel(records);
            return this;
        }

        public KNN AppendToModel(IEnumerable<User> records)
        {
            ((List<User>)TrainingModel).AddRange(getTraningModel(records));
            return this;
        }
        public KNN ResetModel()
        {
            TrainingModel = new List<User>();
            return this;
        }

        public bool? PredicateGender(int k, User neww)
        {
            return predicateValue(getNeighbors(k, calculateDistances(neww, TrainingModel)));
        }

        private IEnumerable<User> getTraningModel(IEnumerable<User> records)
        {
            return records.Where(u => u.Gender != null);
        }

        private static IEnumerable<User> getNeighbors(int k, IEnumerable<Pair<User>> distances)
        {
            return distances.OrderBy(x => x.Weight).Take(k).Select(x => x.Value);
        }

        private static IEnumerable<Pair<User>> calculateDistances(User newRecord, IEnumerable<User> records)
        {
            var distances = new List<Pair<User>>();
            foreach (var r in records)
                distances.Add(new Pair<User>(newRecord.Distance(r), r));
            return distances;
        }

        private static bool? predicateValue(IEnumerable<User> neighbors)
        {
            int TRUE = 0, FALSE = 0;

            foreach (var n in neighbors)
                if (n.Gender == true)
                    TRUE++;
                else
                    FALSE++;

            return (TRUE >= FALSE);
        }
    }
}
