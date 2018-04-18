using u2.Core;
using u2.Core.Contract;
using u2.Umbraco.DataType;

namespace Fumando.Core.U2
{
    public class MapBuild : IMapBuild
    {
        public void Setup(IRegistry registry)
        {
            registry.Copy<CmsModel>()
                .Map(x => x.Name, "nodeName");

            registry.Register<Site>()
                .Map(x => x.Hosts, "hosts", x => x.Split(';'))
                ;
        }
    }
}