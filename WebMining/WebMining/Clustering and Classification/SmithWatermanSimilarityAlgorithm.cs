using System;
using System.Collections.Generic;

namespace WebMining
{
    public class SmithWaterman
    {
        private const double DefaultGapCost = 0.5;
        private const double DefaultMismatchScore = 0.0;
        private const double DefaultPerfectMatchScore = 1.0;
        private const double EstimatedTimingConstant = 0.0001610000035725534;

        SubCostRange1ToMinus2X DCostFunction { get; set; }

        public double GapCost { get; set; }


        public SmithWaterman() : this(DefaultGapCost) { }

        public SmithWaterman(double costG)
        {
            GapCost = costG;
            DCostFunction = new SubCostRange1ToMinus2X();
        }

        public double GetDissimilarity(string firstWord, string secondWord)
        {
            return 1 - getSimilarity(firstWord, secondWord);
        }

        static Dictionary<string, double> cache = new Dictionary<string, double>();

        public double GetSimilarity(string firstWord, string secondWord)
        {
            string key = secondWord + firstWord;
            if (cache.ContainsKey(key))
                return cache[key];

            key = firstWord + secondWord;
            if (cache.ContainsKey(key))
                return cache[key];

            double v = getSimilarity(firstWord, secondWord);

            cache.Add(key, v);

            return v;
        }

        double getSimilarity(string firstWord, string secondWord)
        {
            if (firstWord == null || secondWord == null)
                return DefaultMismatchScore;
            
            double unnormalisedSimilarity = GetUnnormalisedSimilarity(firstWord, secondWord);
            double num2 = Math.Min(firstWord.Length, secondWord.Length);

            if (DCostFunction.MaxCost > -GapCost)
                num2 *= DCostFunction.MaxCost;
            else
                num2 *= -GapCost;

            if (num2 == 0.0)
                return DefaultPerfectMatchScore;

            return unnormalisedSimilarity / num2;
        }

        double GetSimilarityTimingEstimated(string firstWord, string secondWord)
        {
            if (firstWord != null && secondWord != null)
            {
                double length = firstWord.Length;
                double num2 = secondWord.Length;
                return (length * num2 + length + num2) * EstimatedTimingConstant;
            }
            return DefaultMismatchScore;
        }

        double GetUnnormalisedSimilarity(string firstWord, string secondWord)
        {
            if (firstWord == null || secondWord == null)
                return DefaultMismatchScore;
            int length = firstWord.Length;
            int num2 = secondWord.Length;
            if (length == 0)
                return num2;
            if (num2 == 0)
                return length;

            double[][] numArray = new double[length][];
            for (int i = 0; i < length; i++)
                numArray[i] = new double[num2];

            double num4 = 0.0;
            for (int j = 0; j < length; j++)
            {
                double thirdNumber = DCostFunction.GetCost(firstWord, j, secondWord, 0);
                if (j == 0)
                    numArray[0][0] = MaxOf3(0.0, -GapCost, thirdNumber);
                else
                    numArray[j][0] = MaxOf3(0.0, numArray[j - 1][0] - GapCost, thirdNumber);
                if (numArray[j][0] > num4)
                    num4 = numArray[j][0];
            }

            for (int k = 0; k < num2; k++)
            {
                double num8 = DCostFunction.GetCost(firstWord, 0, secondWord, k);
                if (k == 0)
                    numArray[0][0] = MaxOf3(0.0, -GapCost, num8);
                else
                    numArray[0][k] = MaxOf3(0.0, numArray[0][k - 1] - GapCost, num8);
                if (numArray[0][k] > num4)
                    num4 = numArray[0][k];
            }

            for (int m = 1; m < length; m++)
                for (int n = 1; n < num2; n++)
                {
                    double num11 = DCostFunction.GetCost(firstWord, m, secondWord, n);
                    numArray[m][n] = MaxOf4(0.0, numArray[m - 1][n] - GapCost, numArray[m][n - 1] - GapCost, numArray[m - 1][n - 1] + num11);
                    if (numArray[m][n] > num4)
                        num4 = numArray[m][n];
                }
            return num4;
        }


        static double MaxOf4(double firstNumber, double secondNumber, double thirdNumber, double fourthNumber)
        {
            var m = MaxOf3(secondNumber, thirdNumber, fourthNumber);
            if (firstNumber >= m)
                return firstNumber;
            return m;
        }

        static double MaxOf3(double firstNumber, double secondNumber, double thirdNumber)
        {
            if (firstNumber >= secondNumber && firstNumber >= thirdNumber)
                return firstNumber;

            if (secondNumber >= thirdNumber)
                return secondNumber;

            return thirdNumber;
        }

        class SubCostRange1ToMinus2X
        {
            private const int CharExactMatchScore = 1;
            private const double CharMismatchMatchScore = -2.0;

            public double GetCost(string firstWord, int firstWordIndex, string secondWord, int secondWordIndex)
            {
                if (firstWord == null || secondWord == null)
                    return CharMismatchMatchScore;

                if (firstWord.Length <= firstWordIndex || firstWordIndex < 0)
                    return CharMismatchMatchScore;

                if (secondWord.Length <= secondWordIndex || secondWordIndex < 0)
                    return CharMismatchMatchScore;

                return firstWord[firstWordIndex] != secondWord[secondWordIndex] ? CharMismatchMatchScore : CharExactMatchScore;
            }


            public double MaxCost => CharExactMatchScore;
            public double MinCost => CharMismatchMatchScore;
        }
    }
}