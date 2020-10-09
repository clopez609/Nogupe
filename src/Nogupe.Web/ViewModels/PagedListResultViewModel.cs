using Nogupe.Web.Common;
using Nogupe.Web.Models.QueryFilters;
using System.Collections.Generic;

namespace Nogupe.Web.ViewModels
{
    public class PagedListResultViewModel<T> : PagedResultBase where T : class
    {
        public string Search { get; set; }
        public IList<T> Results { get; set; }

        public PagedListResultViewModel()
        {
            Search = null;
            Results = new List<T>();
        }
    }
}
