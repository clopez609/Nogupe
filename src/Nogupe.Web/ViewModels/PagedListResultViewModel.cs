using Nogupe.Web.Common;
using System.Collections.Generic;

namespace Nogupe.Web.ViewModels
{
    public class PagedListResultViewModel<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedListResultViewModel()
        {
            Results = new List<T>();
        }
    }
}
