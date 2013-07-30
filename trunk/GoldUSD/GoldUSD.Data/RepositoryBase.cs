using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace GoldUSD.Data
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public DbContext DbContext { get; set; }
        public DbSet<TEntity> DbSet { get; set; }

        public RepositoryBase(DbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetQuery()
        {
            return this.DbSet;
        }

        public TEntity First()
        {
            return this.DbSet.FirstOrDefault();
        }

        public TEntity Find(params object[] keyValues)
        {
            return this.DbSet.Find(keyValues);
        }

        public TEntity Insert(TEntity entity)
        {
            Contract.Requires(entity != null);

            DbEntityEntry<TEntity> entityEntry = this.DbContext.Entry<TEntity>(entity);
            if (entityEntry.State != EntityState.Detached)
            {
                entityEntry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Contract.Requires(entity != null);

            DbEntityEntry<TEntity> entityEntry = this.DbContext.Entry<TEntity>(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }
            entityEntry.State = EntityState.Modified;

            return entity;
        }

        public TEntity Delete(params object[] keyValues)
        {
            TEntity entity = this.Find(keyValues);
            return Delete(entity);
        }

        public TEntity Delete(TEntity entity)
        {
            Contract.Requires(entity != null);

            DbEntityEntry<TEntity> entityEntry = this.DbContext.Entry<TEntity>(entity);
            if ((entityEntry.State == EntityState.Detached))
            {
                entityEntry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }

            return entity;
        }

        public int SaveChanges()
        {
            return this.DbContext.SaveChanges();
        }
    }
}
