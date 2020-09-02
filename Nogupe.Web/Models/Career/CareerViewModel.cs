using System.ComponentModel.DataAnnotations;

namespace Nogupe.Web.Models.Career
{
    public class CareerViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
