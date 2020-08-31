using Nogupe.Web.Data;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Entities.Weekdays;

namespace Nogupe.Web.Services.Weekdays
{
    public class WeekdayService : Repository<Weekday>, IWeekdayService
    {
        private readonly DataContext _context;
        public WeekdayService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
