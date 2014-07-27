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
        public List<VcbCurrency> VcbCurrencies { get; set; } 
    }
    public class VcbCurrency
    {
        public string Code { get; set; }
        public string Buy { get; set; }
        public string Sell { get; set; }
        public string Transfer { get; set; }
    }
}