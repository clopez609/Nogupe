using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Auth
{
    public class LoginViewModel
    {
        [Display(Name ="Usuario")]
        [Required(ErrorMessage = "Requerido")]
        public string Username { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Requerido")]
        public string Password { get; set; }
    }
}
