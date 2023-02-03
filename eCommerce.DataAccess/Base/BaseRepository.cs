using eCommerce.Common;
using eCommerce.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Data
{
    public class BaseRepository<TEntity>
                     where TEntity : class, IEntity
    {
        private readonly EcommerceContext Context;

        public BaseRepository(EcommerceContext context)
        {
            this.Context = context;
        }

        public IQueryable<TEntity> Get()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public TEntity Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public IEnumerable<TEntity> InsertList(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
            return entities;
        }

        public TEntity Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);

            return entity;
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void DeleteList(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
