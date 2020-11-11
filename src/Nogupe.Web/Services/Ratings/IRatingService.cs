using Nogupe.Web.Common;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Models.QueryFilters;
using Nogupe.Web.Services.Ratings.DTOs;

namespace Nogupe.Web.Services.Ratings
{
    public interface IRatingService : IRepository<Rating>
    {
        PagedListResult<RatingListDTO> GetListDTOPaged(
            int page,
            int pageSize,
            string search = null,
            IFilter customFilter = null);
    }
}
