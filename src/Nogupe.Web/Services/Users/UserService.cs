using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Common;
using Nogupe.Web.Data;
using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Helpers.PredicateExtentions;
using Nogupe.Web.Helpers.QueryableExtentions;
using Nogupe.Web.Models;
using Nogupe.Web.Models.QueryFilters;
using Nogupe.Web.Services.Users.DTOs;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace Nogupe.Web.Services.Users
{
    public class UserService : Repository<User>, IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context) : base(context)
        {
            _context = context;
        }

        public PagedListResult<UserListDTO> GetListDTOPaged(
            int page,
            int pageSize,
            string search = null,
            IFilter customFilter = null)
        {
            //var includeProperties = "";
            Expression<Func<User, bool>> allFilters = null;
            if (search != null) allFilters = GetSearchFilter(search);
            if (customFilter != null)
                allFilters = allFilters == null
                    ? GetCustomFilter(customFilter)
                    : allFilters.AndAlso(GetCustomFilter(customFilter));
            //if (filter != null) allFilters = allFilters == null ? filter : allFilters.AndAlso(filter);

            var userQueryable = GetQueryable(allFilters);

            var query = from u in userQueryable
                        select new UserListDTO
                        {
                            Id = u.Id,
                            Username = u.UserName,
                            Firstname = u.FirstName,
                            Lastname = u.LastName,
                            Email = u.Email,
                            Rolename = u.RoleType.Name
                        };

            return query.GetPaged(page, pageSize);
        }

        public UserValidationResult ValidateUser(string username, string password)
        {
            var result = false;
            var userRecord = _context.Set<User>().FirstOrDefault(x => x.UserName == username);
            string errorCode = null;
            string messageData = null;

            if (userRecord != null)
            {
                if (userRecord.Password == CreatePasswordHash(password, userRecord.Salt))
                {
                    result = true;
                }
                else
                {
                    errorCode = "INVALID_PASSWORD";
                }
            }
            else
            {
                errorCode = "USER_NOT_FOUND";
            }
            return new UserValidationResult(result, userRecord, message: messageData, errorCode: errorCode);
        }

        public UserValidationResult ValidateRegister(string username, int roleId)
        {
            var result = false;
            var userRecord = _context.Set<User>().FirstOrDefault(x => x.UserName == username && x.RoleId == roleId);
            string errorCode = null;
            string messageData = null;

            if (userRecord != null)
            {
                errorCode = "USER_REGISTER";
            }
            else
            {
                result = true;
            }

            return new UserValidationResult(result, userRecord, message: messageData, errorCode: errorCode);
        }

        public UserValidationResult ValidationUserName(string username)
        {
            var result = false;
            var userRecord = _context.Set<User>().FirstOrDefault(x => x.UserName == username);
            string errorCode = null;
            string messageData = null;

            if (!string.IsNullOrEmpty(userRecord.FirstName))
            {
                errorCode = "THERE_IS_ALREADY_A_REGISTERED_USER";
            }
            else
            {
                result = true;
            }

            return new UserValidationResult(result, userRecord, message: messageData, errorCode: errorCode);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentException("user");

            var userRecord = _context.Set<User>().FirstOrDefault(x => x.UserName == user.UserName);

            var salt = CreateSalt();
            userRecord.Salt = salt;
            userRecord.Password = CreatePasswordHash(user.Password, salt);
            userRecord.FirstName = user.FirstName;
            userRecord.LastName = user.LastName;
            userRecord.Email = user.Email;

            base.Update(userRecord);
        }

        public static string CreateSalt()
        {
            var rng = RandomNumberGenerator.Create();
            var buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        public static string CreatePasswordHash(string pwd, string salt)
        {
            var pwdAndSalt = string.Concat(pwd, salt);
            var algorithm = SHA1.Create();

            var hashedPwd = string.Join("",
                algorithm.ComputeHash(Encoding.UTF8.GetBytes(pwdAndSalt)).Select(x => x.ToString("X2"))).ToLower();
            return hashedPwd;
        }

        public virtual IQueryable<User> GetQueryable(
           Expression<Func<User, bool>> filter = null,
           //Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null,
           string includeProperties = null,
           int? skip = null,
           int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<User> query = _context.Set<User>();

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            //if (orderBy != null) query = orderBy(query);

            if (skip.HasValue) query = query.Skip(skip.Value);

            if (take.HasValue) query = query.Take(take.Value);

            return query;
        }

        public Expression<Func<User, bool>> GetSearchFilter(string search)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<User, bool>> GetCustomFilter(IFilter customFilter)
        {
            throw new NotImplementedException();
        }
    }
}
