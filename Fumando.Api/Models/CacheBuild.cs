using u2.Core.Contract;

namespace Fumando.Api.Models
{
    public class CacheBuild : ICacheBuild
    {
        public void Setup(ICacheRegistry registry)
        {
            // register your caching later on.
        }
    }
}