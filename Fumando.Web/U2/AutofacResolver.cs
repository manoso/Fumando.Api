using System;
using System.Web;
using Autofac;
using u2.Core.Contract;

namespace Fumando.Web.U2
{
    public class AutofacResolver : IResolver
    {
        private readonly IContainer _container;

        public AutofacResolver(IContainer container)
        {
            _container = container;
        }

        public T Get<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public Func<string> GetHost => () => HttpContext.Current?.Request.Url.Host ?? string.Empty;
    }
}