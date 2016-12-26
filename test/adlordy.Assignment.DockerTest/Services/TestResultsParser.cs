using adlordy.Assignment.DockerTest.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;

namespace adlordy.Assignment.DockerTest.Services
{
    public class TestResultsParser
    {
        private readonly JsonSerializerSettings _settings;

        public TestResultsParser(IOptions<MvcJsonOptions> options)
        {
            _settings = options.Value.SerializerSettings;
        }
        public TestResults Parse(string output)
        {
            var collections = new Dictionary<string, TestCollectionResult>();
            var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (line.StartsWith("{"))
                {
                    var message = JsonConvert.DeserializeObject<XUnitMessage>(line, _settings);
                    TestCollectionResult collection;
                    switch (message.Message)
                    {
                        case "testCollectionStarting":
                            collection = new TestCollectionResult
                            {
                                Assembly = message.Assembly,
                                CollectionName = message.CollectionName,
                                Tests = new List<TestResult>()
                            };
                            collections.Add(message.FlowId, collection);
                            break;
                        case "testCollectionFinished":
                            collection = collections[message.FlowId];
                            collection.TestsFailed = message.TestsFailed;
                            collection.TestsRun = message.TestsRun;
                            collection.TestsSkipped = message.TestsSkipped;
                            break;
                        case "testPassed":
                            collection = collections[message.FlowId];
                            collection.Tests.Add(new TestResult
                            {
                                TestName = message.TestName,
                                Passed = true,
                                Output = message.Output,
                                ExecutionTime = message.ExecutionTime
                            });
                            break;
                        case "testFailed":
                            collection = collections[message.FlowId];
                            collection.Tests.Add(new TestResult
                            {
                                TestName = message.TestName,
                                Passed = false,
                                Output = message.Output,
                                ExecutionTime = message.ExecutionTime,
                                ErrorMessages = message.ErrorMessages
                            });
                            break;
                    }
                }
            }
            return new TestResults
            {
                Collections = collections.Values.ToList()
            };
        }
    }
}
