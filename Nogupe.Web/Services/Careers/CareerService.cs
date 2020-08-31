using Nogupe.Web.Data;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Entities.Repository;

namespace Nogupe.Web.Services.Careers
{
    public class CareerService : Repository<Career>, ICareerService
    {
        private readonly DataContext _context;
        public CareerService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
