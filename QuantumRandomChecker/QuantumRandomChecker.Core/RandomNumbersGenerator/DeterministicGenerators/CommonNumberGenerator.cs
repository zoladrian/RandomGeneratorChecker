using QuantumRandomChecker.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumRandomChecker.Core.RandomNumbersGenerator.DeterministicGenerators
{
    public class CommonNumberGenerator : NumberGenerator
    {
        private readonly Random random = new Random();

        public override Task<List<double>> GetRandomNumbers(int count)
        {
            List<double> randomNumbers = new List<double>(count);

            for (int i = 0; i < count; i++)
            {
                randomNumbers.Add(random.NextDouble());
            }

            return Task.FromResult(randomNumbers);
        }
    }
}
