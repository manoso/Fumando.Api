using System.Collections.Generic;

namespace Fumando.Model.Config
{
    public class ApiConfig
    {
        public LogConfig Log { get; set; }
        public IList<SiteConfig> Sites { get; set; }    
    }
}