using Nogupe.Web.Data;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;

namespace Nogupe.Web.Services.Walls
{
    public class WallFileService : Repository<WallFile>, IWallFileService
    {
        private readonly DataContext _context;

        public WallFileService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
