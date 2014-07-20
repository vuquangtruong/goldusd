using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldUSD.Data.Services;
using GoldUSD.Utilities.Common;

namespace GoldUSD.Controllers
{
    public class SmsController : Controller
    {
        private readonly IPriceTypeService _priceTypeService;

        public SmsController(IPriceTypeService priceTypeService)
        {
            _priceTypeService = priceTypeService;
        }

        public void GetQuotation()
        {
            var message = Request.QueryString["message"];
            var phone = Request.QueryString["phone"];
            var serviceCode = Request.QueryString["service"];
            var port = Request.QueryString["port"];
            var main = Request.QueryString["main"];
            var sub = Request.QueryString["sub"];
            var id = "CL8579";
            var pass = "123456";
            string responseMessage = "";
            foreach (var priceCategory in _priceTypeService.DbSet.ToList())
            {
                responseMessage += "-" + priceCategory.Name.ToUpper() + "(Mua-Ban):\n";
                foreach (var price in priceCategory.Prices)
                {
                    responseMessage += string.Format("{0}:{1}-{2}\n", price.Symbol, price.BuyingPrice,
                                                     price.SellingPrice);
                }
            }
            responseMessage = StringUtil.RemoveSign4VietnameseString(responseMessage);
            Response.Write(
                string.Format(
                    "<ClientResponse><Message><PhoneNumber>{0}</PhoneNumber><Message>{1}</Message><SMSID>{2}</SMSID><ServiceNo>{3}</ServiceNo></Message></ClientResponse>",
                    phone, responseMessage, Guid.NewGuid().ToString(), serviceCode));
        }

    }
}