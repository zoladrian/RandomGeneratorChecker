using MathNet.Numerics.Distributions;
using QantumRandomChecker.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumRandomChecker.Core.RandomnessTests
{
    public class SerialTest : RandomnessTester
    {
        public SerialTest(List<double> randomNumbers) : base(randomNumbers)
        {
        }

        public override bool PerformTests()
        {
            Dictionary<int, int> tupleCounts = new Dictionary<int, int>();

            for (int i = 0; i < RandomNumbers.Count - 1; i++)
            {
                int tupleValue = (RandomNumbers[i] > 0.5 ? 1 : 0) * 2 + (RandomNumbers[i + 1] > 0.5 ? 1 : 0);
                if (!tupleCounts.ContainsKey(tupleValue))
                {
                    tupleCounts[tupleValue] = 0;
                }
                tupleCounts[tupleValue]++;
            }

            double chiSquared = 0;
            double expectedCount = (RandomNumbers.Count - 1) / 4.0;
            foreach (int count in tupleCounts.Values)
            {
                chiSquared += Math.Pow(count - expectedCount, 2) / expectedCount;
            }

            double pValue = 1 - ChiSquared.CDF(3, chiSquared);

            bool isRandom = pValue > 0.05;

            return isRandom;
        }
    }

}
