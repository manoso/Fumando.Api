using u2.Core.Contract;

namespace Fumando.Web.U2
{
    public class CacheConfig : ICacheConfig
    {
        public int CacheInSeconds { get; set; }
    }
}