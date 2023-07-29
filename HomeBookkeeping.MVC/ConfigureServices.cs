using System.Text.Json.Serialization;
using HomeBookkeeping.Application.Common.Interfaces;
using HomeBookkeeping.Infrastructure.Services;
using Serilog;

namespace HomeBookkeeping.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            SerilogSettings(configuration);

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddEndpointsApiExplorer();
            services.AddAuthorization();
            services.AddHttpContextAccessor();

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
