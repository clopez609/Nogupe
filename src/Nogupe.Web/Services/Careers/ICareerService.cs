using Nogupe.Web.Common;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Models.DTOs.Career;

namespace Nogupe.Web.Services.Careers
{
    public interface ICareerService : IRepository<Career>
    {
        PagedListResult<Career> GetPaged(
            int page, 
            int pageSize,
            string search = null
        );
    }
}
