using MathNet.Numerics.Distributions;
using QantumRandomChecker.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumRandomChecker.Core.RandomnessTests
{
    public class GapTest : RandomnessTester
    {
        public GapTest(List<double> randomNumbers) : base(randomNumbers)
        {
        }

        public override bool PerformTests()
        {
            double lowerBound = 0.25;
            double upperBound = 0.75;
            Dictionary<int, int> gapCounts = new Dictionary<int, int>();
            int currentGap = 0;

            for (int i = 0; i < RandomNumbers.Count; i++)
            {
                if (RandomNumbers[i] >= lowerBound && RandomNumbers[i] <= upperBound)
                {
                    if (!gapCounts.ContainsKey(currentGap))
                    {
                        gapCounts[currentGap] = 0;
                    }
                    gapCounts[currentGap]++;
                    currentGap = 0;
                }
                else
                {
                    currentGap++;
                }
            }

            int maxLength = gapCounts.Keys.Max();
            double[] expectedCounts = new double[maxLength + 1];
            double p = upperBound - lowerBound;
            for (int i = 0; i < expectedCounts.Length; i++)
            {
                expectedCounts[i] = Math.Pow(1 - p, i) * p * (RandomNumbers.Count - 1);
            }

            double chiSquared = 0;
            for (int i = 0; i < gapCounts.Count; i++)
            {
                if (gapCounts.ContainsKey(i))
                {
                    chiSquared += Math.Pow(gapCounts[i] - expectedCounts[i], 2) / expectedCounts[i];
                }
            }

            double pValue = 1 - ChiSquared.CDF(maxLength, chiSquared);

            bool isRandom = pValue > 0.05;

            return isRandom;
        }
    }


}
