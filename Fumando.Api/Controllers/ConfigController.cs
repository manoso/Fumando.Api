using System.Threading.Tasks;
using System.Web.Http;
using Fumando.Model.Config;
using Microsoft.Extensions.Options;

namespace Fumando.Api.Controllers
{
    [RoutePrefix("api/config")]
    public class ConfigController : ApiController
    {
        private readonly ApiConfig _config;
        private readonly LogConfig _logConfig;

        public ConfigController(IOptions<ApiConfig> config, IOptions<LogConfig> log)
        {
            _config = config.Value;
            _logConfig = log.Value;
        }

        // GET api/config
        public async Task<IHttpActionResult> Get()
        {
            return await Task.FromResult(Json(_config));
        }

        // GET api/config
        [Route("log")]
        [HttpGet]
        public async Task<IHttpActionResult> Log()
        {
            return await Task.FromResult(Json(_logConfig));
        }
    }
}
