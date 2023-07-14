using MathNet.Numerics.Distributions;
using QantumRandomChecker.Abstraction;
using System;
using System.Collections.Generic;


namespace QuantumRandomChecker.Core.RandomnessTests
{
    public class AutocorrelationTest : RandomnessTester
    {
        public AutocorrelationTest(List<double> randomNumbers) : base(randomNumbers)
        {
        }

        public override bool PerformTests()
        {
            int lag = 1; // Można dostosować do potrzeb, np. użyć kilku wartości opóźnienia (lag) i sprawdzić wyniki
            double autocorrelation = 0;
            for (int i = 0; i < RandomNumbers.Count - lag; i++)
            {
                autocorrelation += (RandomNumbers[i] - 0.5) * (RandomNumbers[i + lag] - 0.5);
            }
            autocorrelation /= (RandomNumbers.Count - lag);

            double variance = 1.0 / (12 * (RandomNumbers.Count - lag));
            double z = autocorrelation / Math.Sqrt(variance);

            double pValue = 1 - Normal.CDF(0, 1, Math.Abs(z)); // Używamy standardowego rozkładu normalnego (średnia = 0, stddev = 1)

            bool isRandom = pValue > 0.05;

            return isRandom;
        }

    }

}
