using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WebMining
{
    public class DbscanAlgorithm
    {
        private const int UNCLASSIFIED = 0;
        private const int NOISE = -1;

        private class DbscanPoint
        {
            public bool IsVisited;
            public int ClusterId;
            public IMeasurable ClusterPoint;

            public DbscanPoint(IMeasurable x)
            {
                ClusterPoint = x;
                IsVisited = false;
                ClusterId = UNCLASSIFIED;
            }
        }




        public double Epsilon { get; private set; }
        public int MinPts { get; private set; }

        public DbscanAlgorithm(double epsilon, int minPts)
        {
            Epsilon = epsilon;
            MinPts = minPts;
        }

        DbscanPoint[] _dataset;
        public IEnumerable<Cluster> Clustering(IEnumerable<IMeasurable> dataset)
        {
            return Clustering(dataset, x => x.First());
        }

        public IEnumerable<Cluster> Clustering(IEnumerable<IMeasurable> dataset, Func<IEnumerable<IMeasurable>, IMeasurable> marge)
        {
            _dataset = dataset.Select(x => new DbscanPoint(x)).ToArray();
            int clusterId = 0;
            foreach (var p in _dataset.Where(x => x.IsVisited == false))
            {
                p.IsVisited = true;

                var neighbors = neighbor(p.ClusterPoint);

                if (neighbors.Count() < MinPts)
                    p.ClusterId = NOISE;
                else
                    ExpandCluster(p, neighbors, ++clusterId);
            }

            return fillResultInClusters(marge);
        }

        private void ExpandCluster(DbscanPoint newPoint, IEnumerable<DbscanPoint> neighborPts, int clusterId)
        {
            newPoint.ClusterId = clusterId;
            var queue = new Queue<DbscanPoint>(neighborPts);
            while (queue.Count > 0)
            {
                var point = queue.Dequeue();
                if (point.ClusterId == UNCLASSIFIED)
                    point.ClusterId = clusterId;

                if (point.IsVisited)
                    continue;

                point.IsVisited = true;
                var neighbors = neighbor(point.ClusterPoint);
                if (neighbors.Count() >= MinPts)
                    foreach (var neighbor in neighbors.Where(neighbor => !neighbor.IsVisited))
                        queue.Enqueue(neighbor);
            }
        }

        private IEnumerable<Cluster> fillResultInClusters(Func<IEnumerable<IMeasurable>, IMeasurable> marge)
        {
            return _dataset.Where(x => x.ClusterId > 0).GroupBy(x => x.ClusterId).Select(x => new Cluster(x.Select(y => y.ClusterPoint), marge));
        }

        private IEnumerable<DbscanPoint> neighbor(IMeasurable point)
        {
            return _dataset.Where(x => point.Distance(x.ClusterPoint) <= Epsilon);
        }
    }
}