using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contrats;

namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlServer(configuration.
                GetConnectionString("sqlConnection")));

        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
            => services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServicesManager(this IServiceCollection services)
            => services.AddScoped<IServicesManager, ServicesManager>();

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerService, LoggerManager>();
    }
}
