using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GoldUSD.Data.Services;

namespace GoldUSD.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPriceTypeService _priceTypeService;

        public HomeController(IPriceTypeService priceTypeService)
        {
            _priceTypeService = priceTypeService;
        }
        public ActionResult Login()
        {
            var result = _priceTypeService.DbSet.ToList();
            return View();
        }
    }
}
