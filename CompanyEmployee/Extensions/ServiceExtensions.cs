using Contracts;
using LoggerService;
using Repository;

namespace CompanyEmployee.Extensions;

public static class ServiceExtensions
{
    // CORS extensions
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(option =>
        {
            option.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    // IIS extensions
    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(option => { });
    // Logger extensions
    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();
    // Repository Manager extensions
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager,RepositoryManager>();
}