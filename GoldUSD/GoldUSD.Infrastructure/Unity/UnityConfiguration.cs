using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using GoldUSD.Data;
using GoldUSD.Data.Services;
using Microsoft.Practices.Unity;

namespace GoldUSD.Infrastructure.Unity
{
    public class UnityConfiguration
    {
        public static void Setup()
        {
            // register object lifetime
            MvcUnityContainer.Container = new UnityContainer()
                // Dbcontext
                .RegisterTypeForHttpContext<DbContext, GoldUsdContext>()
                // Repositories
                .RegisterTypeForHttpContext<IPriceTypeService, PriceTypeService>()
                .RegisterTypeForHttpContext<IPriceService, PriceService>()
                .RegisterTypeForHttpContext<IUserService, UserService>();


            ControllerBuilder.Current.SetControllerFactory(typeof(UnityControllerFactory));
        }
    }
}
