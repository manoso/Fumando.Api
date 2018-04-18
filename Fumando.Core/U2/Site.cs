using System.Collections.Generic;
using u2.Core;
using u2.Core.Contract;

namespace Fumando.Core.U2
{
    public class Site : CmsModel, ISite
    {
        public string SiteId { get; set; }
        public IEnumerable<string> Hosts { get; set; }
    }
}