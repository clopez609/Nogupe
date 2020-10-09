using System.Collections.Generic;

namespace Nogupe.Web.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public string Search { get; set; }
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Search = null;
            Results = new List<T>();
        }
    }
}
