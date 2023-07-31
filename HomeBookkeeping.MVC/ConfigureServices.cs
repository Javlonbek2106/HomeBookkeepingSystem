using System.Text.Json.Serialization;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Infrastructure.Persistence;
using HomeBookkeeping.Infrastructure.Persistence.Interceptors;
using HomeBookkeeping.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace HomeBookkeeping.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            // Add infrastructure services
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString: configuration.GetConnectionString("DbConnection"));
                options.UseLazyLoadingProxies();
            });

            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            // Add application services
            //services.AddScoped<ICurrentUserService, CurrentUserService>();

            // Add other API services
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddEndpointsApiExplorer();
            services.AddAuthorization();
            services.AddHttpContextAccessor();

            // Configure Serilog
            SerilogSettings(configuration);

            return services;
        }

        public static void SerilogSettings(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Information()
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
        }
    }
}
