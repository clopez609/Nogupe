using Nogupe.Web.Data;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Entities.Years;

namespace Nogupe.Web.Services.Years
{
    public class YearService : Repository<Year>, IYearService
    {
        private readonly DataContext _context;

        public YearService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
