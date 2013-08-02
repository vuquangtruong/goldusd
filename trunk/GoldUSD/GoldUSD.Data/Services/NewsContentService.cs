using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using GoldUSD.Data.Models;

namespace GoldUSD.Data.Services
{
    public class NewsContentService : RepositoryBase<NewsContent>, INewsContentService
    {
        public NewsContentService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
