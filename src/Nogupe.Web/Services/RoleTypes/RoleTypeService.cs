using Nogupe.Web.Data;
using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Repository;

namespace Nogupe.Web.Services.RoleTypes
{
    public class RoleTypeService : Repository<RoleType>, IRoleTypeService
    {
        private readonly DataContext _context;
        public RoleTypeService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
