using adlordy.Assignment.DockerTest.Models;
using adlordy.Assignment.DockerTest.Options;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace adlordy.Assignment.DockerTest.Services
{
    public class RunTestService
    {
        private readonly DockerClient _client;
        private string _imageName;
        private readonly TestResultsParser _parser;

        public RunTestService(IOptions<DockerOptions> options, TestResultsParser parser)
        {
            var config = new DockerClientConfiguration(new Uri("unix:///var/run/docker.sock"));
            _client = config.CreateClient();
            _imageName = options.Value.ImageName;
            _parser = parser;
        }

        public async Task<TestResults> RunTest(string url)
        {
            CancellationTokenSource cancellation = new CancellationTokenSource();
            cancellation.CancelAfter(60000);

            var createParams = new CreateContainerParameters {
                 Image = _imageName,
                 AttachStdout = true,
                 Name = Guid.NewGuid().ToString(),
                 Tty = true,
                 Env = new[] {
                     $"URL={url}"
                 }
            };
            var createResponse = await _client.Containers.CreateContainerAsync(createParams);
            var containerId = createResponse.ID;

            try
            {
                var startParams = new ContainerStartParameters();
                var started = await _client.Containers.StartContainerAsync(containerId, startParams);
                if (!started)
                    throw new Exception($"Container {containerId} failed to start");

                var waitResponse = await _client.Containers.WaitContainerAsync(containerId, cancellation.Token);
                var statusCode = waitResponse.StatusCode;
                var logsResponse = await _client.Containers.GetContainerLogsAsync(containerId, 
                    new ContainerLogsParameters { ShowStdout = true }, cancellation.Token);
                using (var reader = new StreamReader(logsResponse))
                {
                    var logs = await reader.ReadToEndAsync();
                    return _parser.Parse(logs);
                }
            }
            catch(TaskCanceledException)
            {
                throw new Exception("Test execution timed out");
            }
            finally
            {
                await _client.Containers.RemoveContainerAsync(containerId, new ContainerRemoveParameters { Force = true });
            }
        }
    }
}
