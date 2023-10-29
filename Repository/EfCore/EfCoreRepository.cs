using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UserService.Repository.EfCore
{
    public abstract class EfCoreRepository<Key, TEntity, DContext> : IRepository<TEntity, Key>
        
        where TEntity : class, IEntity<Key>
        where DContext : DbContext
    {

        private readonly DContext context;

        public EfCoreRepository(DContext context) { 
            this.context = context;
        }

        public async Task<Boolean> DeleteById(Key id)
        {

            var item = await context.Set<TEntity>().FindAsync(id);
            if (item == null) return false;

            context.Set<TEntity>().Remove(item);

            await context.SaveChangesAsync();
            
            return true;
        }

        public async Task<List<TEntity>> FindAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> FindOneById(Key id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Save(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }
    }
}