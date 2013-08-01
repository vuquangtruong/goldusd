using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldUSD.Data.Models;

namespace GoldUSD.Models
{
    public class PriceManagementModel
    {
        public List<SelectListItem> PriceTypes { get; set; }

        public int SelectedPriceType { get; set; }

        public int? DeletePriceId { get; set; }
    }
}