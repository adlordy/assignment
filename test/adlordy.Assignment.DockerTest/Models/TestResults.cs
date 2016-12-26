using System.Collections.Generic;

namespace adlordy.Assignment.DockerTest.Models
{
    public class TestResults
    {
        public List<TestCollectionResult> Collections { get; set; }
    }

    public class TestResult
    {
        public string TestName { get; set; }
        public bool Passed { get; set; }
        public double? ExecutionTime { get; set; }
        public string Output { get; set; }
        public string ErrorMessages { get; set; }
    }

    public class TestCollectionResult {
        public string Assembly { get; set; }
        public string CollectionName { get; set; }
        public int? TestsFailed { get; set; }
        public int? TestsRun { get; set; }
        public int? TestsSkipped { get; set; }

        public List<TestResult> Tests { get; set; }
    }
}
