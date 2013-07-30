using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace GoldUSD.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        DbContext DbContext { get; set; }
        DbSet<TEntity> DbSet { get; set; }
        IQueryable<TEntity> GetQuery();
        TEntity First();
        TEntity Find(params object[] keyValues);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(params object[] keyValues);
        TEntity Delete(TEntity entity);
        int SaveChanges();
    }
}
