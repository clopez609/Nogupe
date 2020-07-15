using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.Models.Auth
{
    public class PreRegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
