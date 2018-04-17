using System;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Fumando.Api.Extensions;
using Fumando.Api.Models;
using Fumando.Model.Config;
using Microsoft.Extensions.Configuration;
using u2.Core.Contract;
using u2.Umbraco;
using u2.Umbraco.Contract;

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
                .SetBasePath(HttpRuntime.AppDomainAppPath)
                .AddJsonFile("appSettings.json", false, true);

            var root = configurationBuilder.Build();

            var builder = new ContainerBuilder();
            builder.RegisterOptions();

            RegisterConfig(builder, root);
            var binder = new UmbracoBinder(builder);
            binder.Register<MapBuild, CacheBuild, UmbracoQueryFactory, UmbracoFetcher>();

            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;

            var umbResolver = new UmbracoResolver(container);
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

    public class UmbracoResolver : IResolver
    {
        private readonly IContainer _container;

        public UmbracoResolver(IContainer container)
        {
            _container = container;
        }

        public T Get<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public Func<string> GetHost => () => HttpContext.Current?.Request.Url.Host ?? string.Empty;
    }

    public class UmbracoBinder : Binder<Site>
    {
        private readonly ContainerBuilder _builder;

        public UmbracoBinder(ContainerBuilder builder)
        {
            _builder = builder;
        }

        public override void Bind<TContract, TConcrete>()
        {
            _builder.RegisterType<TConcrete>().As<TContract>().SingleInstance();
        }

        public override void Bind<TContract, TConcrete>(Func<TContract> func)
        {
            //_builder.RegisterType<TConcrete>().As<TContract>();
            _builder.Register(context => func());
        }
    }
}
