using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace GoldUSD.Infrastructure.Unity
{
    public static class MvcUnityContainer
    {
        public static IUnityContainer Container { get; set; }
    }
}
