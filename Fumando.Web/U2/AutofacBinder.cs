using System;
using Autofac;
using Fumando.Web.Models;
using u2.Core;

namespace Fumando.Web.U2
{
    public class AutofacBinder : Binder<Site>
    {
        private readonly ContainerBuilder _builder;

        public AutofacBinder(ContainerBuilder builder)
        {
            _builder = builder;
        }

        public override void BindSingleton<TContract, TConcrete>()
        {
            _builder.RegisterType<TConcrete>().As<TContract>().SingleInstance();
        }

        public override void Bind<TContract, TConcrete>(Func<TContract> func)
        {
            //_builder.RegisterType<TConcrete>().As<TContract>();
            _builder.Register(context => func());
        }
    }
}