using Microsoft.Extensions.DependencyInjection;
using Nogupe.Web.Services.Careers;
using Nogupe.Web.Services.Courses;
using Nogupe.Web.Services.Matters;
using Nogupe.Web.Services.RoleTypes;
using Nogupe.Web.Services.Users;
using Nogupe.Web.Services.Weekdays;

namespace Nogupe.Web.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleTypeService, RoleTypeService>();
            services.AddTransient<ICareerService, CareerService>();
            services.AddTransient<IMatterService, MatterService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IWeekdayService, WeekdayService>();

            return services;
        }
    }
}
