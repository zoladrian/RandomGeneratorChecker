using QuantumRandomChecker.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumRandomChecker.Core.RandomNumbersGenerator.DeterministicGenerators
{
    public class SemiPredictableNumberGenerator : NumberGenerator
    {
        private const int Seed = 123456789;
        private const int ResetAfter = 10000;

        private Random random = new Random(Seed);
        private int counter = 0;

        public override Task<List<double>> GetRandomNumbers(int count)
        {
            List<double> randomNumbers = new List<double>(count);

            for (int i = 0; i < count; i++)
            {
                randomNumbers.Add(random.NextDouble());
                counter++;
                if (counter >= ResetAfter)
                {
                    random = new Random(Seed);
                    counter = 0;
                }
            }

            return Task.FromResult(randomNumbers);
        }
    }
}
