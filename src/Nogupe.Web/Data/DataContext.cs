using Microsoft.EntityFrameworkCore;
using Nogupe.Web.Entities;
using Nogupe.Web.Entities.Auth;
using Nogupe.Web.Entities.Careers;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Enums;
using Nogupe.Web.Entities.Matters;
using Nogupe.Web.Entities.Weekdays;
using Nogupe.Web.Entities.Years;
using System;
using System.Linq;

namespace Nogupe.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        // Auth
        DbSet<User> Users { get; set; }
        DbSet<RoleType> RoleTypes { get; set; }
        
        // File
        DbSet<File> Files { get; set; }

        // Career
        DbSet<Career> Careers { get; set; }

        // Matter
        DbSet<Matter> Matters { get; set; }

        // Course
        DbSet<Course> Courses { get; set; }
        DbSet<Wall> Walls { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<WallFile> Documents { get; set; }
        DbSet<Token> Tokens { get; set; }
        DbSet<Rating> Ratings { get; set; }
        DbSet<Inscription> Inscriptions { get; set; }
        DbSet<Assistance> Assistances { get; set; }

        // Weekday
        DbSet<Weekday> Weekdays { get; set; }

        // Year
        DbSet<Year> Years { get; set; }

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

            #region Files

            modelBuilder.Entity<File>().HasKey(u => u.Id);
            modelBuilder.Entity<File>().Property(u => u.Name).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<File>().Property(u => u.DirName).HasMaxLength(260).IsRequired();
            modelBuilder.Entity<File>().Property(u => u.UIdFileName).HasMaxLength(260).IsRequired();

            //modelBuilder.Entity<File>().HasIndex(u => u.UIdFileName).IsUnique();

            #endregion Files

            #region Weekday

            modelBuilder.Entity<Weekday>().HasKey(u => u.Id);
            modelBuilder.Entity<Weekday>().Property(u => u.Name).HasMaxLength(50).IsRequired();

            #endregion

            #region Year

            modelBuilder.Entity<Year>().HasKey(u => u.Id);
            modelBuilder.Entity<Year>().Property(u => u.Name).HasMaxLength(50).IsRequired();

            #endregion

            #region Career

            modelBuilder.Entity<Career>().HasKey(u => u.Id);
            modelBuilder.Entity<Career>().Property(u => u.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Career>()
                .HasMany(c => c.Matters)
                .WithOne(e => e.Career);

            #endregion

            #region Matter

            modelBuilder.Entity<Matter>().HasKey(u => u.Id);
            modelBuilder.Entity<Matter>().Property(u => u.Name).HasMaxLength(100).IsRequired();
            
            modelBuilder.Entity<Matter>()
                .HasOne(e => e.Career)
                .WithMany(c => c.Matters);

            modelBuilder.Entity<Matter>()
                .HasMany(e => e.Courses)
                .WithOne(c => c.Matter);

            modelBuilder.Entity<Matter>()
                .HasOne(e => e.Year)
                .WithMany(c => c.Matters);

            #endregion

            #region Course

            modelBuilder.Entity<Course>().HasKey(u => u.Id);

            modelBuilder.Entity<Course>()
                .HasOne(e => e.Matter)
                .WithMany(c => c.Courses);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Tokens)
                .WithOne(c => c.Course);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Ratings)
                .WithOne(c => c.Course);

            modelBuilder.Entity<Course>()
                .HasOne(e => e.Wall)
                .WithOne(c => c.Course)
                .HasForeignKey<Wall>(a => a.CourseId);

            #endregion

            #region Wall

            modelBuilder.Entity<Wall>().HasKey(u => u.Id);

            //modelBuilder.Entity<Wall>()
            //    .HasOne(e => e.Course)
            //    .WithOne(c => c.Wall)
            //    .HasForeignKey<Course>(e => e.WallId);

            modelBuilder.Entity<Wall>()
                .HasMany(e => e.Comments)
                .WithOne(c => c.Wall);

            #endregion

            #region Rating

            modelBuilder.Entity<Rating>().HasKey(u => u.Id);

            var ratingStatus = Enum.GetValues(typeof(RatingStatus))
                .Cast<RatingStatus>()
                .Select(v => "'" + v + "'")
                .ToList();

            modelBuilder.Entity<Rating>().Property(u => u.Status)
                .HasColumnType("ENUM(" + string.Join(",", ratingStatus) + ")")
                .HasDefaultValue(RatingStatus.None)
                .HasConversion(
                    v => v.ToString(),
                    v => (RatingStatus)Enum.Parse(typeof(RatingStatus), v));

            modelBuilder.Entity<Rating>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Ratings)
                .HasForeignKey(e => e.CourseId);

            #endregion

            #region Token

            modelBuilder.Entity<Token>().HasKey(u => u.Id);
            modelBuilder.Entity<Token>().Property(t => t.CreatedDate).IsRequired();

            modelBuilder.Entity<Token>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Tokens)
                .HasForeignKey(e => e.CourseId);

            #endregion

            #region Inscription

            modelBuilder.Entity<Inscription>()
                .ToTable("Inscriptions")
                .HasKey(u => u.Id);

            var status = Enum.GetValues(typeof(EnrollmentStatus))
               .Cast<EnrollmentStatus>()
               .Select(v => "'" + v + "'")
               .ToList();

            modelBuilder.Entity<Inscription>().Property(u => u.Status)
                .HasColumnType("ENUM(" + string.Join(",", status) + ")")
                .HasDefaultValue(EnrollmentStatus.Pending)
                .HasConversion(
                    v => v.ToString(),
                    v => (EnrollmentStatus)Enum.Parse(typeof(EnrollmentStatus), v));

            modelBuilder.Entity<Inscription>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Inscriptions)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<Inscription>()
                .HasOne(bc => bc.Course)
                .WithMany(c => c.Inscriptions)
                .HasForeignKey(bc => bc.CourseId);

            #endregion

            #region Assistance

            modelBuilder.Entity<Assistance>()
                .ToTable("Assistances")
                .HasKey(u => u.Id);

            modelBuilder.Entity<Assistance>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Assistances)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<Assistance>()
                .HasOne(bc => bc.Course)
                .WithMany(b => b.Assistances)
                .HasForeignKey(bc => bc.CourseId);

            #endregion

            #region Document

            modelBuilder.Entity<WallFile>().ToTable("Documents").HasKey(u => u.Id);

            modelBuilder.Entity<WallFile>()
                .HasOne(e => e.Wall)
                .WithMany(e => e.Documents)
                .HasForeignKey(e => e.WallId);

            modelBuilder.Entity<WallFile>()
                .HasOne(e => e.File)
                .WithMany()
                .HasForeignKey(e => e.FileId);

            #endregion

            Seed.Run(modelBuilder);
        }
    }
}
