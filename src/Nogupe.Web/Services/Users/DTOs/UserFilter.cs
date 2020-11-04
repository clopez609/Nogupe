using Nogupe.Web.Models.QueryFilters;

namespace Nogupe.Web.Services.Users.DTOs
{
    public class UserFilter : IFilter
    {
        public string Search { get; set; }
    }
}
