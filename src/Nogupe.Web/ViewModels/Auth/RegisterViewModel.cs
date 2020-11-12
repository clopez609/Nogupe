using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Requerido")]
        [RegularExpression("^[0-9]{0,8}$", ErrorMessage = "Debe ser un documento válido")]
        public string UserName { get; set; }

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

        [Required(ErrorMessage = "Requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Ambas contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
    }
}
