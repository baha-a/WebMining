using System;
using System.Collections.Generic;

namespace WebMining
{
    public static class LevenshteinDistance
    {
        static Dictionary<string, double> cacher = new Dictionary<string, double>();
        static string tmpKey;
        static double tmpValue;
        public static double Compute(string s, string t)
        {
            tmpKey = (s + "," + t);
            if (cacher.ContainsKey(tmpKey))
                return cacher[tmpKey];

            cacher.Add(tmpKey, tmpValue = compute(s, t) * 1.0 / Math.Max(s.Length, t.Length));
            return tmpValue;
        }

        public static double ComputeWithoutCache(string s, string t)
        {
            return compute(s, t) * 1.0 / Math.Max(s.Length, t.Length);
        }

        static int[,] d;
        static int n, m;

        public static int compute(string s, string t)
        {
            n = s.Length;
            m = t.Length;
            d = new int[n + 1, m + 1];

            if (n == 0) return m;
            if (m == 0) return n;

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + ((t[j - 1] == s[i - 1]) ? 0 : 1));
            return d[n, m];
        }

        public static void Test()
        {
            System.Windows.Forms.MessageBox.Show(
                Compute("aunt", "aunt") + "\r\n" +
                Compute("Sam", "Samantha") + "\r\n" +
                Compute("Samantha", "Sam") + "\r\n" +
                Compute("flomax", "volmax") + "\r\n" +
                Compute("volmax", "flomax"));


            System.Diagnostics.Stopwatch st = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < 4000000; i++)
                Compute("volmax", "flomax");
            st.Stop();
            long time1 = st.ElapsedMilliseconds;

            st = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < 4000000; i++)
                ComputeWithoutCache("volmax", "flomax");
            st.Stop();
            long time2 = st.ElapsedMilliseconds;


            System.Windows.Forms.MessageBox.Show("with cache = " + time1 + "\r\nwithout cache = " + time2);
        }
    }
}