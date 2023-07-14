using System.Collections.Generic;

namespace QantumRandomChecker.Abstraction
{
    public abstract class RandomnessTester
    {
        protected List<double> RandomNumbers;

        public RandomnessTester(List<double> randomNumbers)
        {
            RandomNumbers = randomNumbers;
        }

        public abstract bool PerformTests();
    }
}