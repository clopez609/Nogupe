using System.Collections.Generic;

namespace Nogupe.Web.Common
{
    public class PagedListResult<TEntity>
    {
        /// <summary>
        ///     Page Number
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        ///     Total number of rows that could be possibly be retrieved.
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        ///     Result of the query.
        /// </summary>
        public IEnumerable<TEntity> Entities { get; set; }
    }
}
