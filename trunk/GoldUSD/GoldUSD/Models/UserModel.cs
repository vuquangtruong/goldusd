﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoldUSD.Data.Models;

namespace GoldUSD.Models
{
    public class UserModel
    {
        public User User { get; set; }
        public List<PriceType> PriceTypes { get; set; }
        public string Content { get; set; }
    }
}