using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace adlordy.Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("hosting.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
