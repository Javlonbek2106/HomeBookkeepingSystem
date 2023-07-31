using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Infrastructure.Persistence;
using HomeBookkeeping.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeBookkeeping.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString: configuration.GetConnectionString("DbConnection"));

                options.UseLazyLoadingProxies();
            });

            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
