using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Models;

namespace Nogupe.Web.Services.Users
{
    public interface IUserService : IRepository<User>
    {
        UserValidationResult ValidateRegister(string username, int roleId);
        UserValidationResult ValidateUser(string username, string password);
        UserValidationResult ValidationUserName(string username);
        void UpdateUser(User user);
    }
}
