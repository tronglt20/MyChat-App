using Domain.Interfaces.Base;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using MyChat_App.Services;
using Utilities;

namespace MyChat_App.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with Scoped lifetime  
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {

                    });
            });

            return services;
        }

        public static IServiceCollection AddRepositoriesBase(this IServiceCollection services)
        {
            return 
                services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
                        .AddImplementationInterfaces(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return
                services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return 
                services.AddScoped<IAMService>()
                        .AddScoped<ChatHubService>();
        }

    }
}
