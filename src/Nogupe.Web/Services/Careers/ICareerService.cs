using Nogupe.Web.Common;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Entities.Repository;

namespace Nogupe.Web.Services.Careers
{
    public interface ICareerService : IRepository<Career>
    {
        //PagedListResult<Career> GetPaged(
        //    int page, 
        //    int pageSize,
        //    string search = null
        //);
        public PagedResult<Career> GetPagedList(int page, int pageSize, string search = null);
    }
}
