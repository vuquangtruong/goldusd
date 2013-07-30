using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using GoldUSD.Data.Models;

namespace GoldUSD.Data
{
    public class GoldUsdContext : DbContext
    {
        public DbSet<AspnetUser> AspnetUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PriceType> PriceTypes { get; set; }
        public DbSet<Price> Prices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
