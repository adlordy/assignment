using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using adlordy.Assignment.Contracts;

namespace adlordy.Assignment.IntegrationTests
{
    public class HttpClientFixture
    {
        private static string UrlKey = "Url";
        public HttpClientFixture()
        {
            var values = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>(UrlKey, "http://localhost:5000")
            };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(values)
                .AddEnvironmentVariables()
                .Build();
            var url = configuration[UrlKey];
            Client = new DiffClient(url);
            Service = new RemoteDiffService(Client, 10);
        }

        public DiffClient Client { get; }
        public IDiffService Service { get; }
    }
}