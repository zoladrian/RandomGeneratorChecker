using MathNet.Numerics.Distributions;
using QantumRandomChecker.Abstraction;
using System;
using MathNet.Numerics.Statistics;
using System.Collections.Generic;


namespace QuantumRandomChecker.Core.RandomnessTests
{
    public class RunsTest : RandomnessTester
    {
        public RunsTest(List<double> randomNumbers) : base(randomNumbers)
        {
        }

        public override bool PerformTests()
        {
            int runsCount = 0;
            bool previousBit = RandomNumbers[0] > 0.5;
            for (int i = 1; i < RandomNumbers.Count; i++)
            {
                bool currentBit = RandomNumbers[i] > 0.5;
                if (currentBit != previousBit)
                {
                    runsCount++;
                    previousBit = currentBit;
                }
            }

            double mean = (2.0 * RandomNumbers.Count - 1) / 3.0;
            double variance = (16.0 * RandomNumbers.Count - 29) / 90.0;
            double z = (runsCount - mean) / Math.Sqrt(variance);

            double stddev = RandomNumbers.StandardDeviation();
            double pValue = 2.0 * (1.0 - Normal.CDF(Math.Abs(z), 0, stddev));

            bool isRandom = pValue > 0.05;

            return isRandom;
        }

    }

}
