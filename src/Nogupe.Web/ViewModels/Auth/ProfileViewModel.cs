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

        public int? Phone { get; set; }

        public int? CellPhone { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int AdressNumber { get; set; }
    }
}
