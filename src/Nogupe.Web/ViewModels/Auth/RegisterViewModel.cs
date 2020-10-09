using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Requerido")]
        [RegularExpression("^[0-9]{0,8}$", ErrorMessage = "Documento invalido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Password { get; set; }
    }
}
