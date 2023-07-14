
namespace QuantumRandomChecker
{
    public class TestResult
    {
        public string TestName { get; set; }
        public bool Result { get; set; }

        public TestResult(string testName, bool testResult)
        {
            TestName = testName;
            Result = testResult;
        }
    }
}

