using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GeoLib.Core
{
    public abstract class DataRepositoryBase<T, U> : IDataRepository<T> 
        where T : class, IIdentifiableEntity, new()
        where U : DbContext, new()
    {
        protected abstract DbSet<T> DbSet(U entityContext);
        protected abstract Expression<Func<T, bool>> IdentifierPredicate(U entityContext, int id);

        T AddEntity(U entityContext, T entity)
        {
            return DbSet(entityContext).Add(entity);
        }

        IEnumerable<T> GetEntities(U entityContext) 
        {
            return DbSet(entityContext).ToFullyLoaded();
        }

        T GetEntity(U entityContext, int id)
        {
            return DbSet(entityContext).Where(IdentifierPredicate(entityContext, id)).FirstOrDefault();
        }

        T UpdateEntity(U entityContext, T entity) 
        {
            var q = DbSet(entityContext).Where(IdentifierPredicate(entityContext, entity.EntityId));
            return q.FirstOrDefault();
        }

        public virtual T Add(T entity)
        {
            using (U entityContext = new U())
            {
                T addedEntity = AddEntity(entityContext, entity);
                entityContext.SaveChanges();
                return addedEntity;
            }
        }

        public virtual void Remove(T entity) 
        {
            using (U entitiesContext = new U())
            {
                entitiesContext.Entry<T>(entity).State = EntityState.Deleted;
                entitiesContext.SaveChanges();
            }
        }

        public virtual void Remove(int id)
        {
            using (U entitiesContext = new U())
            {
                T entity = GetEntity(entitiesContext, id);
                entitiesContext.Entry<T>(entity).State = EntityState.Deleted;
                entitiesContext.SaveChanges();
            }
        }

        public virtual T Update(T entity)
        {
            using (U entitiesContext = new U())
            {
                T existingEntity = UpdateEntity(entitiesContext, entity);

                SimpleMapper.PropertyMap(entity, existingEntity);

                entitiesContext.SaveChanges();
                return existingEntity;
            }
        }

        public virtual IEnumerable<T> Get()
        {
            using(U entitiesContext = new U())
            {
                return GetEntities(entitiesContext).ToArray().ToList();
            }
        }
        
        public virtual T Get(int id)
        {
            using(U entityContext = new U())
                return GetEntity(entityContext, id);
        }
    }
}
