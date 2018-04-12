using u2.Core.Contract;

namespace Fumando.Api.Models
{
    public class CacheConfig : ICacheConfig
    {
        public int CacheInSeconds { get; set; }
    }
}