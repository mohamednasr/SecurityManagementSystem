using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MNS.Repository
{
    public static class IQueryExtensions
    {
        public static async Task<QueryResult<T>> ToQueryResultAsync<T>(this IQueryable<T> dbQuery, int pageNumber = 1, int pageSize = 10) where T : class
        {
            try
            {
                int total = await dbQuery.CountAsync();

                if(pageNumber == 0 || pageSize == 0)
                {
                    var result = await dbQuery.ToListAsync();
                    return new QueryResult<T>(result, total, 0, 0);
                }

                dbQuery = dbQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                var data = await dbQuery.ToListAsync();

                return new QueryResult<T>(data, total, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, int, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }
    }
}
