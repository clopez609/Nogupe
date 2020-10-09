using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.ViewModels.Auth
{
    public class PreRegisterViewModel
    {
        [Required(ErrorMessage = "Requerido")]
        [RegularExpression("^[0-9]{0,8}$", ErrorMessage = "Documento invalido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int RoleId { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
