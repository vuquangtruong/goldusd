using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GoldUSD.Utilities.Common
{
    public class AuthorizeExAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// The roles
        /// </summary>
        private readonly string[] roles;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeExAttribute"/> class.
        /// </summary>
        /// <param name="roles">The roles.</param>
        public AuthorizeExAttribute(params string[] roles)
        {
            this.roles = roles;
        }

        /// <summary>
        /// When overridden, provides an entry point for custom authorization checks.
        /// </summary>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <returns>true if the user is authorized; otherwise, false.</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var valid = false;
            foreach (var role in this.roles)
            {
                valid = System.Web.Security.Roles.IsUserInRole(httpContext.User.Identity.Name, role);
                if (valid)
                {
                    break;
                }
            }

            return valid;
        }

        /// <summary>
        /// Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext">Encapsulates the information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />. The <paramref name="filterContext" /> object contains the controller, HTTP context, request context, action result, and route data.</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { redirectTo = FormsAuthentication.LoginUrl }
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
