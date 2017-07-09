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

        private Action<int, string> notifyer;
        public DbscanAlgorithm setNotifyer(Action<int, string> n)
        {
            notifyer = n;
            return this;
        }

        public void notify(int d,string s)
        {
            if (notifyer != null)
                notifyer(d, s);
        }


        DbscanPoint[] _dataset;
        private int totalCountOfItem;

        public IEnumerable<Cluster> Clustering(IEnumerable<IMeasurable> dataset)
        {
            return Clustering(dataset, x => x.First());
        }

        public IEnumerable<Cluster> Clustering(IEnumerable<IMeasurable> dataset, Func<IEnumerable<IMeasurable>, IMeasurable> marge)
        {
            counter = 0;
            notify(0, "start clustering");

            int clusterId = 0;
            totalCountOfItem = dataset.Count();
            _dataset = dataset.Select(x => new DbscanPoint(x)).ToArray();

            foreach (var p in _dataset)
            {                
                if (p.IsVisited)
                    continue;

                increceCounterAndNotify();
                p.IsVisited = true;

                var neighbors = neighbor(p.ClusterPoint);

                if (neighbors.Count() < MinPts)
                    p.ClusterId = NOISE;
                else
                    ExpandCluster(p, neighbors, ++clusterId);
            }

            notify(90, "preparing clusters");
            var res = fillResultInClusters(marge);
            notify(100, "finish clustering");
            return res;
        }

        int counter = 0;

        private void ExpandCluster(DbscanPoint newPoint, IEnumerable<DbscanPoint> neighborPts, int clusterId)
        {
            newPoint.ClusterId = clusterId;
            var queue = new Queue<DbscanPoint>(neighborPts);
            while (queue.Count > 0)
            {
                increceCounterAndNotify();

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

        private void increceCounterAndNotify()
        {
            counter++;
            if (counter % 100 == 0)
                notify((int)((counter * 90.0 / totalCountOfItem) % 100), "clustering");
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