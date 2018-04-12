using System.Threading.Tasks;
using System.Web.Http;
using Fumando.Api.Models;
using Fumando.Model.Config;
using Microsoft.Extensions.Options;
using u2.Core.Contract;
using u2.Umbraco.Contract;

namespace Fumando.Api.Controllers
{
    [RoutePrefix("api/config")]
    public class ConfigController : ApiController
    {
        private readonly ApiConfig _config;
        private readonly IUmbracoConfig _umbracoConfig;
        private readonly ICacheConfig _cacheConfig;

        public ConfigController(IOptions<ApiConfig> config, IOptions<UmbracoConfig> umbraco, IOptions<CacheConfig> cache)
        {
            _config = config.Value;
            _umbracoConfig = umbraco.Value;
            _cacheConfig = cache.Value;
        }

        // GET api/config
        public async Task<IHttpActionResult> Get()
        {
            return await Task.FromResult(Json(_config));
        }

        // GET api/config/umbraco
        [Route("umbraco")]
        [HttpGet]
        public async Task<IHttpActionResult> Umbraco()
        {
            return await Task.FromResult(Json(_umbracoConfig));
        }

        // GET api/config/umbraco
        [Route("cache")]
        [HttpGet]
        public async Task<IHttpActionResult> Cache()
        {
            return await Task.FromResult(Json(_cacheConfig));
        }
    }
}
