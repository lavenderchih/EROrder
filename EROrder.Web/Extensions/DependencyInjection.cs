using EROrder.Core.Services;
using EROrder.Core.Services.Interfaces;

namespace EROrder.Web.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
