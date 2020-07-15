using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.Models.Auth
{
    public class RegisterViewModel
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
