using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using GoldUSD.Data.Models;

namespace GoldUSD.Data.Services
{
    public class PriceTypeService : RepositoryBase<PriceType>, IPriceTypeService
    {
        public PriceTypeService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
