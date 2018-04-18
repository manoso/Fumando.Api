using System.Threading.Tasks;
using System.Web.Http;
using Fumando.Core.U2;
using Fumando.Model.Config;
using Microsoft.Extensions.Options;
using u2.Core.Contract;
using u2.Umbraco.Contract;

namespace Fumando.Web.Controllers
{
    [RoutePrefix("api/config")]
    public class ConfigController : ApiController
    {
        private readonly ApiConfig _config;
        private readonly IUmbracoConfig _umbracoConfig;
        private readonly ICacheConfig _cacheConfig;
        private readonly ICache _cache;

        public ConfigController(IOptions<ApiConfig> config, IUmbracoConfig umbraco, ICacheConfig cache, ICache cmsCache)
        {
            _config = config.Value;
            _umbracoConfig = umbraco;
            _cacheConfig = cache;
            _cache = cmsCache;
        }

        // GET api/config
        [HttpGet]
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

        // GET api/config/cache
        [Route("cache")]
        [HttpGet]
        public async Task<IHttpActionResult> Cache()
        {
            return await Task.FromResult(Json(_cacheConfig));
        }

        [Route("site")]
        [HttpGet]
        public async Task<IHttpActionResult> Site()
        {
            return Json(await _cache.FetchAsync<Site>());
        }
    }
}
