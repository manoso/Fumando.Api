
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Fumando.Api.Startup))]

namespace Fumando.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}