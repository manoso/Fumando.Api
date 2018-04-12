using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using u2.Umbraco.Contract;

namespace Fumando.Api.Models
{
    public class UmbracoConfig : IUmbracoConfig
    {
        public string ExamineSearcher { get; set; }
    }
}
