using Microsoft.Extensions.DependencyInjection;
using Nogupe.Web.Services.Assistances;
using Nogupe.Web.Services.Careers;
using Nogupe.Web.Services.Courses;
using Nogupe.Web.Services.Email;
using Nogupe.Web.Services.Files;
using Nogupe.Web.Services.Matters;
using Nogupe.Web.Services.Ratings;
using Nogupe.Web.Services.RoleTypes;
using Nogupe.Web.Services.Tokens;
using Nogupe.Web.Services.Users;
using Nogupe.Web.Services.Walls;
using Nogupe.Web.Services.Weekdays;
using Nogupe.Web.Services.Years;

namespace Nogupe.Web.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleTypeService, RoleTypeService>();
            services.AddTransient<ICareerService, CareerService>();
            services.AddTransient<IMatterService, MatterService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IWeekdayService, WeekdayService>();
            services.AddTransient<IWallService, WallService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IInscriptionService, InscriptionService>();
            services.AddTransient<IYearService, YearService>();
            services.AddTransient<IAssistanceService, AssistanceService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IWallFileService, WallFileService>();
            services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
