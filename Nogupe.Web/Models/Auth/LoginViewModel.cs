using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.Models.Auth
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
