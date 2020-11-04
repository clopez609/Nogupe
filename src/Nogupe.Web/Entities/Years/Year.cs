using Nogupe.Web.Entities.Matters;
using System.Collections.Generic;

namespace Nogupe.Web.Entities.Years
{
    public class Year : Entity<int>
    {
        public string Name { get; set; }
        public ICollection<Matter> Matters { get; set; }
    }
}
