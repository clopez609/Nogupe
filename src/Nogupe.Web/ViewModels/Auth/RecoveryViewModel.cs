using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Auth
{
    public class RecoveryViewModel
    {
        [Required(ErrorMessage = "Requerido")]
        [EmailAddress(ErrorMessage = "Debe ser un correo electrónico válido")]
        public string Email { get; set; }
    }
}
