using System.Collections.Generic;
using u2.Core;
using u2.Core.Contract;

namespace Fumando.Web.Models
{
    public class Site : CmsModel, ISite
    {
        public string SiteName { get; set; }
        public IEnumerable<string> Hosts { get; set; }
    }
}