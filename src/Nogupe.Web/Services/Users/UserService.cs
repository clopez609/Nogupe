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
using System.Net;
using System.Net.Mail;
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
                    errorCode = "Contraseña Invalida";
                }
            }
            else
            {
                errorCode = "Usuario no encontrado";
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
                errorCode = "Usuario Registrado";
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
                errorCode = "Ya hay un usuario registrado";
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
        public void ChangePassword(string token, string password)
        {
            var userRecord = _context.Set<User>().FirstOrDefault(x => x.TokenRecovery == token);
            var salt = CreateSalt();
            userRecord.Salt = salt;
            userRecord.Password = CreatePasswordHash(password, salt);
            userRecord.TokenRecovery = null;

            base.Update(userRecord);
        }

        public bool ValidateToken(string token)
        {
            var userRecord = _context.Set<User>().FirstOrDefault(x => x.TokenRecovery == token);

            if (userRecord == null) return false;

            return true;
        }

        public bool GenerateTokenRecovery(string email)
        {
            var userRecord = _context.Set<User>().FirstOrDefault(x => x.Email == email);

            if (userRecord == null) return false;

            string Token = GetSha256(Guid.NewGuid().ToString());

            userRecord.TokenRecovery = Token;
            base.Update(userRecord);

            SendEmail(email, Token);

            return true;
        }

        private static void SendEmail(string email, string token)
        {
            string EmailOrigin = "christianlopez1605@gmail.com";
            string PasswordOrigin = "6G6ZQD2LJ6";
            string Url = "http://localhost:44399/Account/Recovery/?token="+token;

            MailMessage oMailMessage = new MailMessage(EmailOrigin, email, "Recuperar Contraseña",
                "<p>Correo para recuperación de contraseña</p><br>" +
                "<a href='" + Url + "'>Click para recuperar</a>");

            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new NetworkCredential(EmailOrigin, PasswordOrigin);

            oSmtpClient.Send(oMailMessage);

            oSmtpClient.Dispose();
        }

        private static string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
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
