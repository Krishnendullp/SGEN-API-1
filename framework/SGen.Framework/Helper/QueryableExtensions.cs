using Framework.Common;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Common;
using System.Linq.Expressions;


namespace SGen.Framework.Helper
{
    public static class QueryableExtensions
    {
        // ✅ Apply filters dynamically
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, Dictionary<string, string>? filters)
        {
            if (filters == null || !filters.Any()) return query;

            foreach (var filter in filters)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, filter.Key);
                var constant = Expression.Constant(filter.Value);

                // Contains for string
                Expression body;
                if (property.Type == typeof(string))
                {
                    var method = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
                    body = Expression.Call(property, method, constant);
                }
                else
                {
                    body = Expression.Equal(property, Expression.Convert(constant, property.Type));
                }

                var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);
                query = query.Where(predicate);
            }

            return query;
        }

        // ✅ Apply sorting dynamically
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, Dictionary<string, string>? sorts)
        {
            if (sorts == null || !sorts.Any()) return query;

            IOrderedQueryable<T>? orderedQuery = null;
            bool first = true;

            foreach (var sort in sorts)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, sort.Key);
                var lambda = Expression.Lambda(property, parameter);

                string methodName = (sort.Value.ToLower() == "desc")
                    ? (first ? "OrderByDescending" : "ThenByDescending")
                    : (first ? "OrderBy" : "ThenBy");

                orderedQuery = (IOrderedQueryable<T>)typeof(Queryable)
                    .GetMethods()
                    .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), property.Type)
                    .Invoke(null, new object[] { orderedQuery ?? query, lambda })!;

                first = false;
            }

            return orderedQuery ?? query;
        }

        // ✅ Apply pagination
        public static async Task<PagedResult<IEnumerable<T>>> ToPagedResultAsync<T>(
        this IQueryable<T> query, int pageNo, int pageSize)
        where T : class
        {
            //if (pageNo <= 0) pageNo = 1;
            //if (pageSize <= 0) pageSize = 10;

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<IEnumerable<T>>
            {
                Data = items,
                PageNo = pageNo,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}

