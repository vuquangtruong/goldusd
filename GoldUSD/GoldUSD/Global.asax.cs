using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using GoldUSD.Data;
using GoldUSD.Infrastructure.Unity;
using GoldUSD.Utilities.Common;

namespace GoldUSD
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Login", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            UnityConfiguration.Setup();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        public void Session_OnStart()
        {
            if (User.Identity.IsAuthenticated && Roles.IsUserInRole(User.Identity.Name, AppConstant.RoleUser))
            {
                using (var context = new GoldUsdContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.AspnetUser.UserName == User.Identity.Name);
                    user.IsLoggedIn = false;
                    context.SaveChanges();
                }
                FormsAuthentication.SignOut();
            }
        }

        public void Session_OnEnd()
        {
            if (Session[AppConstant.SessionLogin] != null)
            {
                using (var context = new GoldUsdContext())
                {
                    var userId = Guid.Parse(Session[AppConstant.SessionLogin].ToString());
                    var user =
                        context.Users.FirstOrDefault(
                            u => u.Id == userId);
                    user.IsLoggedIn = false;
                    context.SaveChanges();
                }
                if (User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                }
            }
        }
    }
}