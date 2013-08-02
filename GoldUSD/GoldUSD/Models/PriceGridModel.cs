using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoldUSD.Data.Models;

namespace GoldUSD.Models
{
    public class PriceGridModel
    {
        public List<Price> Prices { get; set; }
        public List<PriceType> PriceTypes { get; set; }
    }
}