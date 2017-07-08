using System;

namespace WebMining
{
    public class TestItem : IMeasurable
    {
        public double X;
        public double Y;
        public TestItem(double x, double y) { X = x; Y = y; }

        public double Distance(IMeasurable tp)
        {
            TestItem t = (TestItem)tp;
            return Math.Sqrt(((X - t.X) * (X - t.X)) + ((Y - t.Y) * (Y - t.Y)));
        }
    }
}