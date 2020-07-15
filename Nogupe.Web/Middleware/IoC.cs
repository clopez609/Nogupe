using Microsoft.Extensions.DependencyInjection;
using Nogupe.Web.Services.RoleTypes;
using Nogupe.Web.Services.Users;

namespace Nogupe.Web.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleTypeService, RoleTypeService>();

            return services;
        }
    }
}
