using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GoldUSD.Data.Services;
using GoldUSD.Models;
using GoldUSD.Utilities.Common;

namespace GoldUSD.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPriceTypeService _priceTypeService;

        private readonly IUserService _userService;

        public HomeController(IPriceTypeService priceTypeService, IUserService userService)
        {
            _priceTypeService = priceTypeService;
            _userService = userService;
        }
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Roles.IsUserInRole(User.Identity.Name, AppConstant.RoleAdministrator))
                {
                    return RedirectToAction("PriceManagement", "Admin");
                }
                if (Roles.IsUserInRole(User.Identity.Name, AppConstant.RoleUser))
                {
                    return RedirectToAction("Index", "User");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            if (Membership.ValidateUser(model.Username, model.Password))
            {
                var user = _userService.DbSet.FirstOrDefault(u => u.AspnetUser.UserName == model.Username);
                var spanTime = DateTime.Now - user.LastUpdate;
                if (Roles.IsUserInRole(model.Username, AppConstant.RoleUser))
                {
                    if (user.ExpireDate <= DateTime.Now)
                    {
                        ModelState.AddModelError(string.Empty, "Tài khoản đã hết hạn sử dụng");
                        return View(model);
                    }
                    if (user.IsLoggedIn && spanTime.TotalMinutes <= 1)
                    {
                        ModelState.AddModelError(string.Empty, "Tài khoản này đã đăng nhập rồi");
                        return View(model);
                    }
                    else
                    {
                        user.IsLoggedIn = true;
                        _userService.Update(user);
                        _userService.SaveChanges();
                    }
                }
                FormsAuthentication.SetAuthCookie(model.Username, false);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                if (Roles.IsUserInRole(model.Username, AppConstant.RoleAdministrator))
                {
                    return RedirectToAction("PriceManagement","Admin");
                }
                if (Roles.IsUserInRole(model.Username, AppConstant.RoleUser))
                {
                    return RedirectToAction("Index", "User");
                }
            }
            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu sai");
            return View(model);
        }

        public ActionResult Logout()
        {
            var user = _userService.DbSet.FirstOrDefault(u => u.AspnetUser.UserName == User.Identity.Name);
            user.IsLoggedIn = false;
            _userService.SaveChanges();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
