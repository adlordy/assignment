using adlordy.Assignment.DockerTest.Options;
using adlordy.Assignment.DockerTest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Newtonsoft.Json.Serialization;

namespace adlordy.Assignment.DockerTest
{
    public class Startup
    {
        protected readonly IConfigurationRoot configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            configuration = builder.Build();
        }
        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<DockerOptions>(configuration.GetSection("Docker"));
            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services.AddScoped<RunTestService>();
            services.AddScoped<TestResultsParser>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
