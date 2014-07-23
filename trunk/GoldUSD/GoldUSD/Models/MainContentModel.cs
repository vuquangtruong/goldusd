using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoldUSD.Data.Models;

namespace GoldUSD.Models
{
    public class MainContentModel
    {
        public List<PriceType> PriceTypes { get; set; }
        public string Content { get; set; }
        public string WorldCurrencyHtml { get; set; }
    }
}