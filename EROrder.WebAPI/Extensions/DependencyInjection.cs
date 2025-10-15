using EROrder.Core.Services;
using EROrder.Core.Services.Interfaces;

namespace EROrder.WebAPI.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IHomeService, HomeService>();

            return services;
        }
    }
}
