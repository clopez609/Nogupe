using Nogupe.Web.Common;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Models.QueryFilters;
using Nogupe.Web.Services.Courses.DTOs;

namespace Nogupe.Web.Services.Courses
{
    public interface IInscriptionService : IRepository<Inscription>
    {
        void CreateSubcribe(Course course, int? userId);

        bool ValidateSubscribe(int courseId, int? userId);

        PagedResult<InscriptionListDTO> GetListDTOPaged(int page, int pageSize, string search = null, IFilter customFilter = null);

    }
}
