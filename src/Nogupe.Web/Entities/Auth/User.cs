using Nogupe.Web.Entities.Courses;
using System.Collections.Generic;

namespace Nogupe.Web.Entities.Auth
{
    public class User : Entity<int>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Phone { get; set; }
        public int? CellPhone { get; set; }
        public string Address { get; set; }
        public int AdressNumber { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string TokenRecovery { get; set; }

        public int RoleId { get; set; }
        public RoleType RoleType { get; set; }

        public virtual ICollection<Inscription> Inscriptions { get; set; }
        public virtual ICollection<Assistance> Assistances { get; set; }
    }
}
