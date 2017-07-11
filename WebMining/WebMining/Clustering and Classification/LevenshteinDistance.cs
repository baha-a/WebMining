using System;

namespace WebMining
{
    public static class LevenshteinDistance
    {
        public static double Compute(string s, string t)
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
            System.Windows.Forms.MessageBox.Show("" + LevenshteinDistance.Compute("aunt", "aunt"));
            System.Windows.Forms.MessageBox.Show("" + LevenshteinDistance.Compute("Sam", "Samantha"));
            System.Windows.Forms.MessageBox.Show("" + LevenshteinDistance.Compute("flomax", "volmax"));
        }
    }
}