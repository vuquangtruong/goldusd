using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
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

            var lstVCB = new List<VcbCurrency>();
            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load("https://www.vietcombank.com.vn/exchangerates/ExrateXML.aspx");
                foreach (XmlNode xmlNode in xmlDoc.GetElementsByTagName("Exrate"))
                {
                    var code = xmlNode.Attributes["CurrencyCode"].Value;
                    if (xmlNode.Name == "Exrate" &&
                        (code == "AUD" || code == "CAD" || code == "CHF" || code == "EUR" || code == "GBP" ||
                         code == "HKD" ||
                         code == "JPY" || code == "SGD" || code == "THB" || code == "USD"))
                    {
                        lstVCB.Add(new VcbCurrency()
                                       {
                                           Code = code,
                                           Buy = float.Parse(xmlNode.Attributes["Buy"].Value).ToString("0,0.00",
                                                                                                       CultureInfo.
                                                                                                           InvariantCulture),
                                           Sell = float.Parse(xmlNode.Attributes["Sell"].Value).ToString("0,0.00",
                                                                                                         CultureInfo.
                                                                                                             InvariantCulture),
                                           Transfer =
                                               float.Parse(xmlNode.Attributes["Transfer"].Value).ToString("0,0.00",
                                                                                                          CultureInfo.
                                                                                                              InvariantCulture)
                                       });
                    }
                }
                model.VcbCurrencies = lstVCB;
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
