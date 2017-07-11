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
            public IDistancable ClusterPoint;

            public DbscanPoint(IDistancable x)
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

        public void notify(int d, string s)
        {
            if (notifyer != null)
                notifyer(d, s);
        }


        DbscanPoint[] _dataset;
        long totalCountOfItem;

        public IEnumerable<Cluster> Clustering(IEnumerable<IDistancable> dataset)
        {
            return Clustering(dataset, x => x.First());
        }

        int counter = 0;
        public IEnumerable<Cluster> Clustering(IEnumerable<IDistancable> dataset, Func<IEnumerable<IDistancable>, IDistancable> marge)
        {
            counter = 0;
            notify(0, "prapering clustering");
            int clusterId = 0;
            totalCountOfItem = dataset.Count() ;
            _dataset = dataset.Select(x => new DbscanPoint(x)).ToArray();


            notify(0, "start clustering");


            foreach (var p in _dataset)
            {
                if (p.IsVisited)
                    continue;

                visit(p);

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

        private void visit(DbscanPoint p)
        {
            p.IsVisited = true;
            increceCounterAndNotify();
        }

        private void increceCounterAndNotify()
        {
            if (++counter % 100 == 0)
                notify((int)((counter * 90.0 / totalCountOfItem) % 100), "clustering");
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

                visit(point);

                var neighbors = neighbor(point.ClusterPoint);
                if (neighbors.Count() >= MinPts)
                    foreach (var neighbor in neighbors.Where(neighbor => !neighbor.IsVisited))
                        queue.Enqueue(neighbor);
            }
        }

        private IEnumerable<Cluster> fillResultInClusters(Func<IEnumerable<IDistancable>, IDistancable> marge)
        {
            return _dataset.Where(x => x.ClusterId > 0).GroupBy(x => x.ClusterId).Select(x => new Cluster(x.Select(y => y.ClusterPoint), marge));
        }

        private IEnumerable<DbscanPoint> neighbor(IDistancable point)
        {
            return _dataset.Where(x => point.Distance(x.ClusterPoint) <= Epsilon);
        }
    }
}