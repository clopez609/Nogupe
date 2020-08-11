using Nogupe.Web.Entities.Careers;

namespace Nogupe.Web.Entities.Matters
{
    public class Matter : Entity<int>
    {
        public string Name { get; set; }
        public int CareerId { get; set; }
        public Career Career { get; set; }
    }
}
