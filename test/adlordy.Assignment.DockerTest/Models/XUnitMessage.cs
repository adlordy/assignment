namespace adlordy.Assignment.DockerTest.Models
{
    public class XUnitMessage
    {
        public string Message { get; set; }
        public string FlowId { get; set; }
        public double? ExecutionTime { get; set; }
        public string Output { get; set; }
        public string ErrorMessages { get; set; }
        public string TestName { get; set; }

        public string CollectionId { get; set; }
        public string Assembly { get; set; }
        public string CollectionName { get; set; }
        public int? TestsFailed { get; set; }
        public int? TestsRun { get; set; }
        public int? TestsSkipped { get; set; }
    }
}
