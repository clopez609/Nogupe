using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;

namespace Nogupe.Web.Services.Walls
{
    public interface IWallService : IRepository<Wall>
    {
        void CreateWall(Course course);
        void DeleteWall(Course course);

    }
}
