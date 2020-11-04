using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Entities.Weekdays;
using Nogupe.Web.Entities.Years;
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
            PopulateCareers(modelBuilder);
            PopulateYears(modelBuilder);
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
                    Name = "Admin",
                },
                new RoleType
                {
                    Id = 2,
                    Name = "Profesor",
                },
                new RoleType
                {
                    Id = 3,
                    Name = "Alumno",
                }
               );
        }

        private static void PopulateYears(ModelBuilder modelBuilder)
        {
            // Create years for matter
            modelBuilder.Entity<Year>().HasData(
                new Year
                {
                    Id = 1,
                    Name = "Primer Año"
                },
                new Year
                {
                    Id = 2,
                    Name = "Segundo Año"
                },
                new Year
                {
                    Id = 3,
                    Name = "Tercer Año"
                },
                new Year
                {
                    Id = 4,
                    Name = "Cuarto Año"
                },
                new Year
                {
                    Id = 5,
                    Name = "Quinto Año"
                }
            );
        }

        private static void PopulateCareers(ModelBuilder modelBuilder)
        {
            // Create career
            modelBuilder.Entity<Career>().HasData(
                new Career
                {
                    Id = 1,
                    Name = "Tecnicatura Universitaria en Desarrollo de Software"
                },
                new Career
                {
                    Id = 2,
                    Name = "Licenciatura en Higiene y Seguridad"
                },
                new Career
                {
                    Id = 3,
                    Name = "Licenciatura en Logística"
                },
                new Career
                {
                    Id = 4,
                    Name = "Licenciatura en Gestión Aeroportuaria"
                },
                new Career
                {
                    Id = 5,
                    Name = "Licenciatura en Comercio Internacional"
                },
                new Career
                {
                    Id = 6,
                    Name = "Licenciatura en Turismo"
                },
                new Career
                {
                    Id = 7,
                    Name = "Tecnicatura Universitaria en Higiene y Seguridad"
                },
                new Career
                {
                    Id = 8,
                    Name = "Tecnicatura Universitaria en Logística"
                },
                new Career
                {
                    Id = 9,
                    Name = "Tecnicatura Universitaria en Guía de Turismo"
                },
                new Career
                {
                    Id = 10,
                    Name = "Tecnicatura Universitaria en Hotelería y Turismo"
                },
                new Career
                {
                    Id = 11,
                    Name = "Tecnicatura Universitaria en Comercio Internacional y Despacho Aduana"
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
