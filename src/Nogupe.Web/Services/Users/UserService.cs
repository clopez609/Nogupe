using Nogupe.Web.Data;
using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Repository;
using Nogupe.Web.Models;
using System;
using System.Linq;
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
    }
}
