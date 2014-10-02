using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GoldUSD.Data.Models
{
    public class Price : Entity
    {
        public string Symbol { get; set; }

        public double BuyingPrice { get; set; }

        public double SellingPrice { get; set; }

        public int PriceTypeId { get; set; }

        //Navigation properties
        [ForeignKey("PriceTypeId")]
        public virtual PriceType PriceType { get; set; }
    }
}
