using QuantumRandomChecker.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumRandomChecker.Core.RandomNumbersGenerator.DeterministicGenerators
{
    public class VeryPredictableNumberGenerator : NumberGenerator
    {
        private double currentValue = 0.0;

        public override Task<List<double>> GetRandomNumbers(int count)
        {
            List<double> randomNumbers = new List<double>(count);

            for (int i = 0; i < count; i++)
            {
                randomNumbers.Add(currentValue);
                currentValue += 3.0;
            }

            return Task.FromResult(randomNumbers);
        }
    }
}
