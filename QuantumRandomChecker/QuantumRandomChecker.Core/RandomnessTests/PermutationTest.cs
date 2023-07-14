using MathNet.Numerics.Distributions;
using QantumRandomChecker.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumRandomChecker.Core.RandomnessTests
{
    public class PermutationTest : RandomnessTester
    {
        private int _tupleLength;

        public PermutationTest(List<double> randomNumbers, int tupleLength = 3) : base(randomNumbers)
        {
            _tupleLength = tupleLength;
        }

        public override bool PerformTests()
        {
            int numOfTuples = RandomNumbers.Count - _tupleLength + 1;
            int numOfPermutations = (int)Math.Pow(2, _tupleLength);
            Dictionary<string, int> permutationCounts = new Dictionary<string, int>();

            for (int i = 0; i < numOfTuples; i++)
            {
                StringBuilder tupleBuilder = new StringBuilder();
                for (int j = 0; j < _tupleLength; j++)
                {
                    tupleBuilder.Append(RandomNumbers[i + j] > 0.5 ? "1" : "0");
                }
                string tupleValue = tupleBuilder.ToString();

                if (!permutationCounts.ContainsKey(tupleValue))
                {
                    permutationCounts[tupleValue] = 0;
                }
                permutationCounts[tupleValue]++;
            }

            double chiSquared = 0;
            double expectedCount = (double)numOfTuples / numOfPermutations;
            foreach (int count in permutationCounts.Values)
            {
                chiSquared += Math.Pow(count - expectedCount, 2) / expectedCount;
            }

            double pValue = 1 - ChiSquared.CDF(numOfPermutations - 1, chiSquared);

            bool isRandom = pValue > 0.05;

            return isRandom;
        }
    }

}
