using MathNet.Numerics.Distributions;
using QantumRandomChecker.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumRandomChecker.Core.RandomnessTests
{
    public class MaurersUniversalTest : RandomnessTester
    {
        private int _blockLength;

        public MaurersUniversalTest(List<double> randomNumbers, int blockLength = 4) : base(randomNumbers)
        {
            _blockLength = blockLength;
        }

        public override bool PerformTests()
        {
            int n = RandomNumbers.Count;
            int initSequenceLength = (int)Math.Pow(2, _blockLength) * _blockLength;
            int remainingSequenceLength = n - initSequenceLength;

            if (remainingSequenceLength <= 0)
            {

                throw new ArgumentException("Not enough random numbers for the given block length.");
            }

            int[] Q = new int[(int)Math.Pow(2, _blockLength)];
            for (int i = 0; i < Q.Length; i++)
            {
                Q[i] = -1;
            }

            int currentIndex = 1;
            for (int i = 0; i < initSequenceLength; i += _blockLength)
            {
                int intValue = Convert.ToInt32(string.Concat(RandomNumbers.Skip(i).Take(_blockLength).Select(x => x > 0.5 ? "1" : "0")), 2);
                Q[intValue] = currentIndex;
                currentIndex++;
            }

            double sum = 0;
            for (int i = initSequenceLength; i < n; i += _blockLength)
            {
                int intValue = Convert.ToInt32(string.Concat(RandomNumbers.Skip(i).Take(_blockLength).Select(x => x > 0.5 ? "1" : "0")), 2);
                int lastIndex = Q[intValue];
                Q[intValue] = currentIndex;
                sum += Math.Log2(currentIndex - lastIndex);
                currentIndex++;
            }

            double avg = sum / (remainingSequenceLength / _blockLength);
            double[] expectedValues = { 0, 0, 1.5374383, 1.8378771, 2.2539746, 2.4002697, 2.5396003, 2.6629658, 2.7750022 };
            double[] variances = { 0, 0, 0.073671975, 0.080059958, 0.106563600, 0.108773000, 0.110246700, 0.112279000, 0.114244100 };

            if (_blockLength >= 1 && _blockLength <= 8)
            {
                double stdDev = Math.Sqrt(variances[_blockLength]);
                double z = (avg - expectedValues[_blockLength]) / stdDev;

                double pValue = 2 * (1 - Normal.CDF(0, 1, Math.Abs(z)));

                bool isRandom = pValue > 0.05;

                return isRandom;
            }
            else
            {
                throw new ArgumentException("Invalid block length. The block length should be between 1 and 8.");
            }
        }
    }
}
