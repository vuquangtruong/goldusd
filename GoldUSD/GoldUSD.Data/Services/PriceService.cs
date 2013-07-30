using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using GoldUSD.Data.Models;

namespace GoldUSD.Data.Services
{
    public class PriceService : RepositoryBase<Price>, IPriceService
    {
        public PriceService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
