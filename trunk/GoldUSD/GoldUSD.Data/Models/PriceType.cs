using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldUSD.Data.Models
{
    public class PriceType : Entity
    {
        public string Name { get; set; }

        //Navigation properties
        public virtual List<Price> Prices { get; set; } 
    }
}
