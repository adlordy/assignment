using System.ComponentModel.DataAnnotations;

namespace adlordy.Assignment.DockerTest.Models
{
    public class RunTestModel
    {
        [Required,StringLength(1000)]
        public string Url { get; set; }
    }
}
