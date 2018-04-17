using u2.Core.Contract;

namespace Fumando.Core.U2
{
    public class MapBuild : IMapBuild
    {
        public void Setup(IRegistry registry)
        {
            //register your mapping later on.
            registry.Register<Site>()
                ;
        }
    }
}