using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GoldUSD.Data.Models;
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

        private readonly INewsContentService _newsContentService;

        public UserController(IPriceTypeService priceTypeService, IUserService userService, INewsContentService newsContentService)
        {
            _priceTypeService = priceTypeService;
            _userService = userService;
            _newsContentService = newsContentService;
        }
        public ActionResult Index()
        {
            var user = UpdateUserStatus();
            var model = new UserModel
                            {
                                User = user
                            };

            return View(model);
        }

        public ActionResult MainContent()
        {
            UpdateUserStatus();
            var model = new MainContentModel()
            {
                PriceTypes = _priceTypeService.DbSet.ToList(),
                Content = _newsContentService.DbSet.First().Content
            };
            try
            {
                //Get world exchange rate
                var headers = new WebHeaderCollection { { "X-Requested-With", "XMLHttpRequest" } };
                using (var client = new WebClient { Headers = headers })
                {
                    client.Encoding = Encoding.UTF8;
                    var result = client.DownloadString(string.Format("http://taiem.com.vn/site/usdgoldoil.html?t={0}",
                                                    DateTime.Now.ToString("ddMMyyyyhhmmssffff")));
                    model.WorldCurrencyHtml = result;
                }
            }
            catch (Exception ex)
            {
            }
            return PartialView("_MainContentPartial", model);
        }

        private User UpdateUserStatus()
        {
            var user = _userService.DbSet.FirstOrDefault(u => u.AspnetUser.UserName == User.Identity.Name);
            user.LastUpdate = DateTime.Now;
            _userService.Update(user);
            _userService.SaveChanges();
            return user;
        }
    }
}
