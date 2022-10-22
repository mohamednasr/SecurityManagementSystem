using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MNS.Repository
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }
        public TEntity GetFirstOrDefult(TId id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public async Task<QueryResult<TEntity>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Set<TEntity>().ToQueryResultAsync(pageNumber, pageSize);
        }

        public async Task<QueryResult<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Set<TEntity>().Where(expression).ToQueryResultAsync(pageNumber, pageSize);
        }

        public EntityEntry<TEntity> Add(TEntity entity)
        {
            return _context.Set<TEntity>().Add(entity);
        }

        public EntityEntry<TEntity> Update(TEntity entity)
        {
            return _context.Set<TEntity>().Update(entity);
        }

        public EntityEntry<TEntity> Delete(TEntity entity)
        {
            TEntity exist = _context.Set<TEntity>().Find(entity);
            if (exist != null)
            {
                return _context.Set<TEntity>().Remove(exist);
            }
            else
            {
                return null;
            }
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
