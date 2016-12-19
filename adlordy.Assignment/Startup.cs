using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

[assembly: OwinStartup(typeof(adlordy.Assignment.Startup))]

namespace adlordy.Assignment
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            SetCamelCase(config);
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
        }

        private void SetCamelCase(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
