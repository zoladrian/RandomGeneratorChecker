using QantumRandomChecker.Abstraction;
using System;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;

namespace QuantumRandomChecker.Core.RandomnessTests
{
    public class UniformityTest : RandomnessTester
    {
        public UniformityTest(List<double> randomNumbers) : base(randomNumbers)
        {
        }

        public override bool PerformTests()
        {
            int numberOfBins = (int)Math.Sqrt(RandomNumbers.Count);
            int[] frequencies = new int[numberOfBins];

            foreach (double number in RandomNumbers)
            {
                int binIndex = (int)(number * numberOfBins);
                if (number == 1.0 || number == 0.0) continue;
                frequencies[binIndex]++;
            }

            double chiSquare = 0;
            double expectedFrequency = (double)RandomNumbers.Count / numberOfBins;
            for (int i = 0; i < numberOfBins; i++)
            {
                chiSquare += Math.Pow(frequencies[i] - expectedFrequency, 2) / expectedFrequency;
            }
            double pValue;
            try
            {
                pValue = 1 - ChiSquared.CDF(numberOfBins - 1, chiSquare);
            }
            catch
            {
                pValue = 0.04;
            }
            bool isUniform = pValue > 0.05;

            return isUniform;
        }
    }
}
