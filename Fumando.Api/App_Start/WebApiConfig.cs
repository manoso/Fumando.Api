using System.IO;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Fumando.Api.Extensions;
using Fumando.Model.Config;
using Microsoft.Extensions.Configuration;

namespace Fumando.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            ConfigAutofac(config);
        }

        private static void ConfigAutofac(HttpConfiguration config)
        {

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(HttpRuntime.AppDomainAppPath, "config"))
                .AddJsonFile("appSettings.json", false, true);

            var root = configurationBuilder.Build();

            var builder = new ContainerBuilder();
            builder.RegisterOptions();

            RegisterConfig(builder, root);

            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;
        }

        private static void RegisterConfig(ContainerBuilder builder, IConfigurationRoot root)
        {
            builder.RegisterConfig<ApiConfig>(root.GetSection("ApiConfig"));
            //builder.RegisterConfig<FeedOptions>(configuration.GetSection("feed"));

        }
    }
}
