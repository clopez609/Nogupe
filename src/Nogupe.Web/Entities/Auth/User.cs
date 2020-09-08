using Nogupe.Web.Entities.Courses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nogupe.Web.Entities.Auth
{
    public class User : Entity<int>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public int RoleId { get; set; }
        public RoleType RoleType { get; set; }

        public virtual ICollection<Inscription> Inscriptions { get; set; }
        public virtual ICollection<Assistance> Assistances { get; set; }
    }
}
