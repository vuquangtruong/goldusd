using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace GoldUSD.Infrastructure.Unity
{
    public static class UnityContainerExtension
    {
        public static IUnityContainer RegisterTypeForHttpContext<T, TX>(this IUnityContainer container) where TX : T
        {
            return container.RegisterType<T, TX>(new HttpContextLifetimeManager());
        }
    }
}
