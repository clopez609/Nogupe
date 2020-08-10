using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Weekday;

namespace Nogupe.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        DbSet<User> Users { get; set; }
        DbSet<RoleType> RoleTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Auth

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.UserName).HasMaxLength(25).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.FirstName).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.LastName).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Password).HasColumnType("char(40)");
            modelBuilder.Entity<User>().Property(u => u.Salt).HasColumnType("char(44)");
            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(254);

            modelBuilder.Entity<User>()
                .HasOne(p => p.RoleType)
                .WithMany()
                .HasForeignKey(p => p.RoleId);

            modelBuilder.Entity<RoleType>().HasKey(u => u.Id);
            modelBuilder.Entity<RoleType>().Property(u => u.Name).HasMaxLength(100).IsRequired();

            #endregion Auth

            #region Weekday

            modelBuilder.Entity<Weekday>().HasKey(u => u.Id);
            modelBuilder.Entity<Weekday>().Property(u => u.Name).HasMaxLength(50).IsRequired();

            #endregion


            Seed.Run(modelBuilder);
        }
    }
}
