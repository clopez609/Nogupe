using Nogupe.Web.Common;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Models.QueryFilters;

namespace Nogupe.Web.Services.Careers
{
    public interface ICareerService : IRepository<Career>
    {
        public PagedListResult<Career> GetPagedList(
            int page, 
            int pageSize, 
            string search = null, 
            IFilter customFilter = null);
    }
}
