using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Requerido")]
        //[RegularExpression("^[0-9]{0,8}$", ErrorMessage = "Debe ser un documento válido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
