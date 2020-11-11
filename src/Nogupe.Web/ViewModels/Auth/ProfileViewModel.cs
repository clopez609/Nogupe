using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Auth
{
    public class ProfileViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(50, ErrorMessage = "Debe tener entre 5 y 50 caracteres", MinimumLength = 5)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(50, ErrorMessage = "Debe tener entre 5 y 50 caracteres", MinimumLength = 5)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [EmailAddress(ErrorMessage = "Debe ser un correo electrónico válido")]
        public string Email { get; set; }
    }
}
