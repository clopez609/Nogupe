using Nogupe.Web.Entities.Auth;

namespace Nogupe.Web.Models
{
    public class UserValidationResult : GenericValidationResult
    {
        public UserValidationResult(bool result, User user, string message = null, string errorCode = null)
            : base(result, message, errorCode)
        {
            User = user;
        }

        public User User { get; set; }
    }
}
