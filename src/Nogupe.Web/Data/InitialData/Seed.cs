using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Weekdays;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Nogupe.Web.Data
{
    public class Seed
    {
        public static void Run(ModelBuilder modelBuilder)
        {
            PopulateRoleTypes(modelBuilder);
            PopulateUsers(modelBuilder);
            PopulateWeekdays(modelBuilder);
        }

        private static string CreateSalt()
        {
            var rng = RandomNumberGenerator.Create();
            var buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        private static string CreatePasswordHash(string pwd, string salt)
        {
            var pwdAndSalt = string.Concat(pwd, salt);
            var algorithm = SHA1.Create();

            var hashedPwd = string.Join("",
                algorithm.ComputeHash(Encoding.UTF8.GetBytes(pwdAndSalt)).Select(x => x.ToString("X2"))).ToLower();
            return hashedPwd;
        }

        private static void PopulateUsers(ModelBuilder modelBuilder)
        {
            // Create admin user
            var password = "admin";
            var salt = CreateSalt();
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    Password = CreatePasswordHash(password, salt),
                    FirstName = "admin",
                    LastName = "admin",
                    Salt = salt,
                    RoleId = 1
                });
        }

        private static void PopulateRoleTypes(ModelBuilder modelBuilder)
        {
            // Create admin role
            modelBuilder.Entity<RoleType>().HasData(
                new RoleType
                {
                    Id = 1,
                    Name = "admin",
                },
                new RoleType
                {
                    Id = 2,
                    Name = "profesor",
                },
                new RoleType
                {
                    Id = 3,
                    Name = "alumno",
                }
               );
        }

        private static void PopulateWeekdays(ModelBuilder modelBuilder)
        {
            // Create week days
            modelBuilder.Entity<Weekday>().HasData(
                new Weekday
                {
                    Id = 1,
                    Name = "Lunes 08hs-12hs",
                },
                new Weekday
                {
                    Id = 2,
                    Name = "Lunes 18hs-22hs",
                },
                new Weekday
                {
                    Id = 3,
                    Name = "Martes 08hs-12hs",
                },
                new Weekday
                {
                    Id = 4,
                    Name = "Martes 18hs-22hs",
                },
                new Weekday
                {
                    Id = 5,
                    Name = "Miércoles 08hs-12hs",
                },
                new Weekday
                {
                    Id = 6,
                    Name = "Miércoles 18hs-22hs",
                }, 
                new Weekday
                {
                    Id = 7,
                    Name = "Jueves 08hs-12hs",
                },
                new Weekday
                {
                    Id = 8,
                    Name = "Jueves 18hs-22hs",
                },
                new Weekday
                {
                    Id = 9,
                    Name = "Viernes 08hs-12hs",
                },
                new Weekday
                {
                    Id = 10,
                    Name = "Viernes 18hs-22hs",
                },
                new Weekday
                {
                    Id = 11,
                    Name = "Sábado 08hs-12hs",
                }
               );
        }

    }
}
