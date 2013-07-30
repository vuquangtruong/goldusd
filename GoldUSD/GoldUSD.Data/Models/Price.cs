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

        public int BuyingPrice { get; set; }

        public int SellingPrice { get; set; }

        public int PriceTypeId { get; set; }

        //Navigation properties
        [ForeignKey("PriceTypeId")]
        public virtual PriceType PriceType { get; set; }
    }
}
