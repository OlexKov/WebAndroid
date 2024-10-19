using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAndroid.Data;
using WebAndroid.Interfaces;

namespace WebAndroid.Repositories
{
    public class Repository<TEntity>(AndroidDbContext context) : IRepository<TEntity> where TEntity : class
    {
        internal AndroidDbContext context = context;
        internal DbSet<TEntity> dbSet = context.Set<TEntity>();

        public async Task AddAsync(TEntity entity) => await dbSet.AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await dbSet.AddRangeAsync(entities);

        public void Delete(object id)
        {
            TEntity? entityToDelete = dbSet.Find(id);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public async Task DeleteAsync(object id)
        {
            TEntity? entityToDelete = await dbSet.FindAsync(id);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }

        public IQueryable<TEntity> GetAll() => dbSet.AsQueryable().AsNoTracking();

        public async Task<TEntity?> GetByIDAsync(object id) => await dbSet.FindAsync(id);

        public async Task SaveAsync() => await context.SaveChangesAsync();

        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
