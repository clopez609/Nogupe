using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Entities.Matter
{
    public class Matter : Entity<int>
    {
        public string Name { get; set; }
        public int CareerId { get; set; }
        public Career Career { get; set; }
    }
}
