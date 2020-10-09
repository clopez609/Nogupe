using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Data;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;
using System;
using System.Linq;

namespace Nogupe.Web.Services.Walls
{
    public class WallService : Repository<Wall>, IWallService
    {
        private readonly DataContext _context;

        public WallService(DataContext context) : base(context)
        {
            _context = context;
        }

        public void CreateWall(Course course)
        {
            var walls = _context.Set<Wall>().AsNoTracking();
            if (walls.Any(x => x.Course.Equals(course)))
                throw new Exception("el muro ya tiene a una clase asociada");

            var wall = new Wall
            {
                CourseId = course.Id,
                Course = course,
            };
            Create(wall);
        }

        public void DeleteWall(Course course)
        {
            var wall = _context.Set<Wall>().Where(x => x.CourseId == course.Id).SingleOrDefault();
            if (wall == null) throw new Exception("no se encontro muro");

            Delete(wall);
        }
    }
}
