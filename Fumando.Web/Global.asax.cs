using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Web;

namespace Fumando.Web
{
    public class WebApiApplication : UmbracoApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override void OnApplicationStarted(object sender, EventArgs e)
        {
            base.OnApplicationStarted(sender, e);

            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}
