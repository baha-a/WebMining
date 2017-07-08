using System;
using System.Collections.Generic;

namespace WebMining
{
    public class Cluster
    {
        public int ID { get; set; }
        public IEnumerable<IMeasurable> Dataset { get; set; }

        public IMeasurable Center { get; private set; }

        public Func<IEnumerable<IMeasurable>, IMeasurable> Marger { get; set; }

        static int IDer = 0;
        public Cluster(IEnumerable<IMeasurable> e, Func<IEnumerable<IMeasurable>, IMeasurable> marge)
        {
            ID = IDer++;
            Dataset = e;
            Marger = marge;
            RecalcuateCenter();
        }

        public void RecalcuateCenter()
        {
            Center = Marger(Dataset);
        }
    }
}