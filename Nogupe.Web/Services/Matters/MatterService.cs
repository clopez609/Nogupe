using Nogupe.Web.Data;
using Nogupe.Web.Entities.Matters;
using Nogupe.Web.Entities.Repository;

namespace Nogupe.Web.Services.Matters
{
    public class MatterService : Repository<Matter>, IMatterService
    {
        private readonly DataContext _context;
        public MatterService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
