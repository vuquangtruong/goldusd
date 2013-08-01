using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldUSD.Data.Services;
using GoldUSD.Models;
using GoldUSD.Utilities.Common;

namespace GoldUSD.Controllers
{
    [AuthorizeEx(new[] { AppConstant.RoleUser })]
    public class UserController : Controller
    {
        private readonly IPriceTypeService _priceTypeService;

        private readonly IUserService _userService;

        public UserController(IPriceTypeService priceTypeService, IUserService userService)
        {
            _priceTypeService = priceTypeService;
            _userService = userService;
        }
        public ActionResult Index()
        {
            var user = _userService.DbSet.FirstOrDefault(u => u.AspnetUser.UserName == User.Identity.Name);
            var model = new UserModel()
                            {
                                PriceTypes = _priceTypeService.DbSet.ToList(),
                                User = user
                            };
            return View(model);
        }

    }
}
