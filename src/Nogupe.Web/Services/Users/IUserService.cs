using Nogupe.Web.Common;
using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Models;
using Nogupe.Web.Models.QueryFilters;
using Nogupe.Web.Services.Users.DTOs;

namespace Nogupe.Web.Services.Users
{
    public interface IUserService : IRepository<User>
    {
        PagedListResult<UserListDTO> GetListDTOPaged(
            int page,
            int pageSize,
            string search = null,
            IFilter customFilter = null);
        UserValidationResult ValidateRegister(string username, int roleId);
        UserValidationResult ValidateUser(string username, string password);
        UserValidationResult ValidationUserName(string username);
        void UpdateUser(User user);

        bool ValidateToken(string token);

        string GenerateTokenRecovery(string email);

        void ChangePassword(string token, string password);
    }
}
