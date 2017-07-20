using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMining
{
    public enum MarkovState
    {
        Start,
        End,
        Normal
    }

    public class MarkovNode<T>
    {
        public IDictionary<MarkovNode<T>, int> Nexts { get; private set; }

        public int Occurrence { get; private set; }

        public T Value { get; set; }

        public MarkovState State { get; set; }

        public MarkovNode()
        {
            Nexts = new Dictionary<MarkovNode<T>, int>();
            
            State = MarkovState.Normal;
        }
        public MarkovNode(T value) : this()
        {
            Value = value;
        }

        public MarkovNode<T> AddNext(MarkovNode<T> next)
        {
            if(Nexts.ContainsKey(next))
                Nexts[next]++;
            else
                Nexts.Add(next, 1);

            next.Occurrence++;

            return this;
        }

        public IEnumerable<MarkovNode<T>> GetNexts()
        {
            return Nexts.OrderByDescending(x => (x.Value * 1.0 / Occurrence)).Select(x => x.Key);
        }

        public IEnumerable<T> GetNextsValues()
        {
            return GetNexts().Select(x => x.Value);
        }

        public IEnumerable<KeyValuePair<MarkovNode<T>,double>> GetNextsWithProbabilities()
        {
            return Nexts.Select(x => new KeyValuePair<MarkovNode<T>, double>(x.Key, (x.Value * 1.0 / Occurrence))).OrderByDescending(x => x.Value);
        }

        public double ProbabilityOf(MarkovNode<T> next)
        {
            if (Nexts.ContainsKey(next) == false)
                throw new NotImplementedException("one next step only, more than one not implemented yet");
            return (Nexts[next] * 1.0 / Occurrence); 
        }
    }
}
