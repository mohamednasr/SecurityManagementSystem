using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MNS.Repository
{
    public interface IRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        /// <summary>
        /// get first or default item based on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetFirstOrDefult(TId id);
        /// <summary>
        /// get all items
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();
        /// <summary>
        /// get all items async
        /// </summary>
        /// <returns></returns>
        Task<QueryResult<TEntity>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        /// <summary>
        /// search and get all items async
        /// </summary>
        /// <returns></returns>
        Task<QueryResult<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, int pageNumber = 1, int pageSize = 10);
        /// <summary>
        /// get all items that match the expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// Attach new item
        /// </summary>
        /// <param name="entity"></param>
        EntityEntry<TEntity> Add(TEntity entity);
        /// <summary>
        /// Update existing item
        /// </summary>
        /// <param name="entity"></param>
        EntityEntry<TEntity> Update(TEntity entity);
        /// <summary>
        /// delete item
        /// </summary>
        /// <param name="entity"></param>
        EntityEntry<TEntity> Delete(TEntity entity);
    }
}
