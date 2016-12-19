using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using adlordy.Assignment.Contracts;
using adlordy.Assignment.Services;
using SimpleInjector.Integration.WebApi;

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

            var container = new Container();
            
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            container.Register<IStateService, StateService>(Lifestyle.Singleton);
            container.Register<IDiffService, DiffService>(Lifestyle.Scoped);
            container.RegisterWebApiControllers(config);
            container.Verify();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            app.Use(async (context, next) => {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });
        }

        private void SetCamelCase(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
