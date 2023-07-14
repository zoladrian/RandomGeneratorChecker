using MathNet.Numerics.Distributions;
using QantumRandomChecker.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumRandomChecker.Core.RandomnessTests
{
    public class ChiSquaredTest : RandomnessTester
    {
        private readonly int _numberOfBins;

        public ChiSquaredTest(List<double> randomNumbers, int numberOfBins = 10) : base(randomNumbers)
        {
            _numberOfBins = numberOfBins;
        }

        public override bool PerformTests()
        {
            Dictionary<int, int> binCounts = new Dictionary<int, int>();

            foreach (double randomNumber in RandomNumbers)
            {
                int bin = (int)(randomNumber * _numberOfBins);
                if (!binCounts.ContainsKey(bin))
                {
                    binCounts[bin] = 0;
                }
                binCounts[bin]++;
            }

            double expectedCount = (double)RandomNumbers.Count / _numberOfBins;
            double chiSquared = 0;
            for (int i = 0; i < _numberOfBins; i++)
            {
                if (binCounts.ContainsKey(i))
                {
                    chiSquared += Math.Pow(binCounts[i] - expectedCount, 2) / expectedCount;
                }
            }

            double pValue = 1 - ChiSquared.CDF(_numberOfBins - 1, chiSquared);

            bool isRandom = pValue > 0.05;

            return isRandom;
        }
    }

}
