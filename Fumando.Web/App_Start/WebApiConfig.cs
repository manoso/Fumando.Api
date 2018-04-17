using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Fumando.Model.Config;
using Fumando.Web.Extensions;
using Fumando.Web.Models;
using Microsoft.Extensions.Configuration;
using u2.Core.Contract;
using u2.Umbraco;
using u2.Umbraco.Contract;
using Fumando.Web.U2;

namespace Fumando.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    "DefaultApi",
            //    "api/{controller}/{id}",
            //    new { id = RouteParameter.Optional }
            //);

            ConfigAutofac(config);
        }

        private static void ConfigAutofac(HttpConfiguration config)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(HttpRuntime.AppDomainAppPath)
                .AddJsonFile("appSettings.json", false, true);

            var root = configurationBuilder.Build();

            var builder = new ContainerBuilder();
            builder.RegisterOptions();

            RegisterConfig(builder, root);
            var binder = new AutofacBinder(builder);
            binder.Register<MapBuild, CacheBuild, UmbracoQueryFactory, UmbracoFetcher>();

            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;

            var umbResolver = new AutofacResolver(container);
            binder.Setup(umbResolver);
        }

        private static void RegisterConfig(ContainerBuilder builder, IConfigurationRoot root)
        {
            builder.RegisterConfig<ApiConfig>(root.GetSection("ApiConfig"));
            builder.RegisterConfig<LogConfig>(root.GetSection("ApiConfig:Log"));
            builder.Register(context => root.GetSection("ApiConfig:Umbraco").Get<UmbracoConfig>()).As<IUmbracoConfig>();
            builder.Register(context => root.GetSection("ApiConfig:Cache").Get<CacheConfig>()).As<ICacheConfig>();

            //HttpContext.Current?.Request.Url.Host ?? string.Empty;
        }

    }
}
