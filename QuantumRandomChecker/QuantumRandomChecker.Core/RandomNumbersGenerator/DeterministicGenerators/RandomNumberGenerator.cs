using QuantumRandomChecker.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumRandomChecker.Core.RandomNumbersGenerator.DeterministicGenerators
{
    public class RandomNumberGenerator : NumberGenerator
    {
        private const int ResetAfter = 10000;
        private const int SeedIncrement = 12345;

        private int seed = Environment.TickCount;
        private Random random = new Random(Environment.TickCount);
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
                    seed += SeedIncrement;
                    random = new Random(seed);
                    counter = 0;
                }
            }

            return Task.FromResult(randomNumbers);
        }
    }
}
