using Nogupe.Web.Common;
using Nogupe.Web.Entities.Matters;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Models.QueryFilters;

namespace Nogupe.Web.Services.Matters
{
    public interface IMatterService : IRepository<Matter>
    {
        public PagedResult<Matter> GetPagedList(int page, int pageSize, string search = null, IFilter filter = null);
    }
}
