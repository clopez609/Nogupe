using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.Models.Auth
{
    public class PreRegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        //[Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int RoleId { get; set; }

    }
}
