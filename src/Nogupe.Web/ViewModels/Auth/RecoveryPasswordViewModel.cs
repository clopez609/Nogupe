using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Auth
{
    public class RecoveryPasswordViewModel
    {
        public string Token { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Ambas contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
    }
}
