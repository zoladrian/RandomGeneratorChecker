using Prism.Commands;
using Prism.Mvvm;
using QuantumRandomChecker.Abstraction;
using QuantumRandomChecker.Core.RandomnessTests;
using QuantumRandomChecker.Core.RandomNumbersGenerator.DeterministicGenerators;
using QuantumRandomChecker.Core.RandomNumbersGenerator.QuantumGenerators;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumRandomChecker.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<TestResult> _testResults;
        private List<NumberGenerator> _generators;
        private NumberGenerator _selectedGenerator;

        public ObservableCollection<TestResult> TestResults
        {
            get { return _testResults; }
            set { SetProperty(ref _testResults, value); }
        }


        public List<NumberGenerator> Generators
        {
            get { return _generators; }
            set { SetProperty(ref _generators, value); }
        }

        public NumberGenerator SelectedGenerator
        {
            get { return _selectedGenerator; }
            set { SetProperty(ref _selectedGenerator, value); }
        }

        public DelegateCommand RunTestsCommand { get; private set; }

        public MainWindowViewModel()
        {
            Generators = new List<NumberGenerator>
            {
                new RandomNumberGenerator(),
                new SemiPredictableNumberGenerator(),
                new VeryPredictableNumberGenerator(),
                new QuantumRandomNumberGenerator(),
                new OpenQURandomNumberGenerator()
            };
            SelectedGenerator = Generators.FirstOrDefault();
            TestResults = new ObservableCollection<TestResult>();
            RunTestsCommand = new DelegateCommand(async () => await RunTestsAsync());
        }

        private async Task RunTestsAsync()
        {
            TestResults.Clear();

            if (SelectedGenerator == null)
            {
                return;
            }

            var randomNumbers = await SelectedGenerator.GetRandomNumbers(10000);

            var uniformityTest = new UniformityTest(randomNumbers);
            var autocorrelationTest = new AutocorrelationTest(randomNumbers);
            var runsTest = new RunsTest(randomNumbers);
            var serialTest = new SerialTest(randomNumbers);
            var gapTest = new GapTest(randomNumbers);
            var permutationTest = new PermutationTest(randomNumbers);
            var maurersUniversalTest = new MaurersUniversalTest(randomNumbers);

            TestResults.Add(new TestResult("Test jednorodności", uniformityTest.PerformTests()));
            TestResults.Add(new TestResult("Test autokorelacji", autocorrelationTest.PerformTests()));
            TestResults.Add(new TestResult("Test biegnących", runsTest.PerformTests()));
            TestResults.Add(new TestResult("Test seryjny", serialTest.PerformTests()));
            TestResults.Add(new TestResult("Test odstępów", gapTest.PerformTests()));
            TestResults.Add(new TestResult("Test permutacyjny", permutationTest.PerformTests()));
            TestResults.Add(new TestResult("Test uniwersalny Maurera", maurersUniversalTest.PerformTests()));

        }

    }
}
