using GIH.Interfaces.Managers;
using GIH.Repositories;
using GIH.Services;
using Microsoft.EntityFrameworkCore;

namespace GIH.WebApi.Extension;

public static class ServicesExtensions
{
    public static void ConfigurePostgreSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager,RepositoryManager>();
    }
    public static void ConfigureServiceManager (this IServiceCollection services)
    {
        services.AddScoped<IServiceManager,ServiceManager>();
    }
}