using Nogupe.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nogupe.Web.Helpers.QueryableExtentions
{
    public static class QueryableExtentions
    {
        public static PagedListResult<T> GetPaged<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = query.Count();
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return new PagedListResult<T>
            {
                Page = pageNumber,
                Count = count,
                Entities = query.ToList()
            };
        }

        //public static IQueryable<T> Sort<T>(this IQueryable<T> query, params string[] propertyNames)
        //{
        //    if (propertyNames.Empty()) return query;

        //    var list = new List<SortParameter>();
        //    foreach (var name in propertyNames)
        //    {
        //        SortParameter parameter;
        //        if (SortParameter.TryParse(name, out parameter)) list.Add(parameter);
        //    }

        //    var sortParameterList = new SortParameterList(list);
        //    return sortParameterList.Sort(query) as IQueryable<T>;
        //}

        public static IQueryable<T> If<T>(this IQueryable<T> query, bool should,
            params Func<IQueryable<T>, IQueryable<T>>[] transforms)
        {
            return should ? transforms.Aggregate(query, (current, transform) => transform.Invoke(current)) : query;
        }

        public static bool Empty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
    }
}
