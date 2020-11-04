using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Services.Walls.DTOs;

namespace Nogupe.Web.Services.Walls
{
    public interface IWallService : IRepository<Wall>
    {
        WallDTO GetbyIdDTO(int id);
        void CreateWall(Course course);
        void DeleteWall(Course course);

    }
}
